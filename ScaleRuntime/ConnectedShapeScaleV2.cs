using MPatcherFork.CustomPatches;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

namespace MPatcherFork.ScaleRuntime
{
    // Selection follows native Coupler descendants. The placement grid stays fixed.
    // Render presentation can omit ancestors without changing the assembled edit math.
    internal sealed class ConnectedShapeScaleV2 : MonoBehaviour
    {
        private const string PatchId = "local.moddev.machinecraft.connected-shape-scale.v2";
        private const float Tolerance = 0.002f;
        private const float ContactTolerance = 0.002f;
        private static readonly FieldInfo CameraControl = AccessTools.Field(typeof(Build), "BOIEJCIBHKI");
        private static readonly FieldInfo SelectedController = AccessTools.Field(typeof(Build), "MLJHGDBPMEA");
        private static readonly FieldInfo Graph = AccessTools.Field(typeof(Build), "EPGELCMKKOC");
        private static readonly FieldInfo Preview = AccessTools.Field(typeof(Build), "FFJDGJFPLAD");
        private static readonly FieldInfo Changed = AccessTools.Field(typeof(Build), "BHCOKCDPDNB");
        private static readonly FieldInfo CapsuleMesh = AccessTools.Field(typeof(CapGenController), "NGLBLAGMBLN");
        private static readonly FieldInfo CouplerBases = AccessTools.Field(typeof(HDBLLPODNLN), "DHHGHEONCBE");
        private static readonly FieldInfo GraphCells = AccessTools.Field(typeof(HDBLLPODNLN), "FCFCNCMFGPI");
        private static readonly FieldInfo GraphRoot = AccessTools.Field(typeof(HDBLLPODNLN), "GEGCCNJLMEE");
        private static readonly MethodInfo SaveUndo = AccessTools.Method(typeof(Build), "ANDINIMKBLL", new Type[] { typeof(bool), typeof(bool) });
        private static bool registered;

        private sealed class Member
        {
            internal int Index;
            internal BlockController Controller;
            internal BlockData Source;
            internal GameObject Primitive;
            internal Vector3 ShapePosition;
            internal Vector3 ShapeUp;
            internal Bounds ShapeBounds;
            internal bool HasShape;
            internal int Group;
            internal PreviewFrame Frame;
            internal bool ParameterFixedCoupler;
            internal readonly List<Ghost> PreviewMeshes = new List<Ghost>();
            internal readonly List<ConnectedShapeContact.Hull> ContactMeshes = new List<ConnectedShapeContact.Hull>();
        }
        private sealed class Edit
        {
            internal Member Member;
            internal int[] Slots;
            internal float[] Values;
        }
        private sealed class Ghost
        {
            internal Mesh Mesh;
            internal Vector3 Position;
            internal Quaternion Rotation;
            internal Vector3 Scale;
            internal GameObject Object;
            internal Material[] Materials;
            internal bool PaintTemplate;
            internal Color Paint;
            internal Mesh RenderMesh;
        }

        private sealed class PreviewFrame
        {
            internal int ParentGroup;
            internal Vector3 ParentPosition;
            internal Quaternion InverseParentRotation;
            internal Vector3 Translation;
            internal bool UsesClickedAnchor;
        }

        private sealed class GroupPose
        {
            internal int Parent;
            internal BlockController Driver;
            internal BlockData CouplerBase;
            internal BlockController CouplerBaseController;
            internal Vector3 LocalPosition;
            internal Quaternion LocalRotation;
            internal Vector3 WorldPosition;
            internal Quaternion WorldRotation;
        }

        private sealed class PoseSnapshot
        {
            internal GroupPose[] Groups;
            internal readonly Dictionary<BlockController, int> GroupByController = new Dictionary<BlockController, int>();
            internal readonly HashSet<BlockController> Detached = new HashSet<BlockController>();
        }

        private struct OffsetBound
        {
            internal Vector3 Normal;
            internal float Limit;
        }

        private sealed class ControllerState
        {
            internal BlockController Controller;
            internal BlockData Data, ProbeData;
            internal int Group, Parent;
            internal bool Lost, Selected, Enabled;
            internal CouplerController Pair;
            internal int X, Y, Z, Gid;
            internal int[] Parameters, Actions;
            internal int[] PrecisionValues;
        }
        private sealed class JointState
        {
            internal JointController Controller;
            internal Transform First, Second;
            internal Vector3 FirstPosition, SecondPosition;
            internal Quaternion FirstRotation, SecondRotation;
            internal bool Flipped;
        }

        private Build build;
        private BuildData selectedBuild;
        private int selectedBlockCount;
        private Member seed;
        private readonly List<Member> members = new List<Member>();
        private readonly HashSet<BlockController> selectedCarriers = new HashSet<BlockController>();
        private readonly List<BlockController> selectionAnchors = new List<BlockController>();
        private readonly HashSet<BlockController> parameterFixedCouplers = new HashSet<BlockController>();
        private int selectionRevision;
        private readonly List<Edit> edits = new List<Edit>();
        private readonly List<Ghost> ghosts = new List<Ghost>();
        private readonly Dictionary<BlockController, int> groupByController = new Dictionary<BlockController, int>();
        private GroupPose[] originalGroups;
        private GroupPose[] plannedGroups;
        private PoseSnapshot originalPhysicalPose;
        private PoseSnapshot originalVisualPose;
        private BlockController sessionPoseRoot;
        private HashSet<BlockController> sessionAllowedControllers;
        private int previewParentGroup = -1;
        private Vector3 previewParentPosition = Vector3.zero;
        private Quaternion previewInverseParentRotation = Quaternion.identity;
        private Vector3 previewTranslation = Vector3.zero;
        private bool previewUsesClickedAnchor;
        internal bool VisualPoseAvailable(BlockController controller)
        { return originalVisualPose != null && originalVisualPose.GroupByController.ContainsKey(controller)
            && !originalVisualPose.Detached.Contains(controller); }
        private Vector3 centerPivot;
        private Vector3 pivot;
        private string scaleText = "1";
        private string plannedText;
        private string status = "";
        private bool anchorPivot;
        private bool plannedAnchor;
        private bool singleCouplersOnly = true;
        private bool plannedSingleCouplersOnly;
        private bool showPreview = true;
        private bool open;
        private bool valid;
        private bool identityPreview;
        internal bool NumericInputFocused { get { return SculptScaleUi.NumericInputFocused(build); } }
        internal bool HasSelection { get { return open; } }
        internal int SelectionRevision { get { return selectionRevision; } }
        internal bool IsSelectedCarrier(BlockController controller) { return open && selectedCarriers.Contains(controller); }
        internal bool ReplacementVisible(BlockController controller)
        {
            HashSet<BlockController> visible = new HashSet<BlockController>();
            CollectVisibleReplacementOwners(visible);
            return visible.Contains(controller);
        }
        internal void CollectVisibleReplacementOwners(HashSet<BlockController> visibleOwners)
        {
            visibleOwners.Clear();
            if (!open) return;
            bool any = false;
            foreach (Member member in members)
                foreach (Ghost visual in member.PreviewMeshes)
                {
                    bool visible = visual.Object != null && visual.Object.activeInHierarchy;
                    if (!visible) continue;
                    any = true;
                    visibleOwners.Add(member.Controller);
                }
            if (any)
                foreach (Member member in members)
                    if (member.Source.type == BlockData.AAHMDBHDCDK.Coupler) visibleOwners.Add(member.Controller);
        }
        internal string OpeningStatus { get { return status; } }
        internal bool CanApply { get { return open && valid; } }
        internal string ScaleText { get { return scaleText; } set { scaleText = value ?? ""; } }
        internal bool UseAnchorPivot { get { return anchorPivot; } set { anchorPivot = value; } }
        internal bool PreviewVisible { get { return showPreview; } set { showPreview = value; } }
        internal bool SingleCouplersOnly { get { return singleCouplersOnly; } set { singleCouplersOnly = value; } }
        internal string SummaryText
        {
            get { return open ? "Box: " + boxCount + "  Capsule: " + capCount + "  Coupler: " + couplerCount : ScaleLocalization.Source("No shape selected", "形状が未選択です"); }
        }
        internal string StatusText { get { return status; } }
        internal string CouplerSummary
        {
            get { return ScaleLocalization.Source("Couplers to change: ", "変更する Coupler: ") + scaledCouplerCount + " / " + couplerCount
                + (fixedBoundaryCouplerCount + fixedNonDriverCouplerCount > 0 ? ScaleLocalization.Source("\nCouplers with fixed offsets: ", "\nオフセットを変更しない Coupler: ") + (fixedBoundaryCouplerCount + fixedNonDriverCouplerCount) : "")

                + (unsupportedCount > 0 ? ScaleLocalization.Source("\nOther unchanged blocks: ", "\n変更しないその他のブロック: ") + unsupportedCount : ""); }
        }
        internal void ApplyFromInterface() { if (CanApply) Apply(); }
        private float factor;
        private int boxCount, sculptBoxCount, capCount, couplerCount, singleCouplerCount, scaledCouplerCount, unsupportedCount;
        private float maxError;
        private int limitedCouplerCount;
        private int fixedBoundaryCouplerCount;
        private int fixedNonDriverCouplerCount;
        private float maxCouplerResidual;

        internal static void InitializeRuntime()
        {
            if (registered) return;
            if (CameraControl == null || Graph == null || Preview == null || Changed == null || CapsuleMesh == null
                || CouplerBases == null || GraphCells == null || GraphRoot == null || SaveUndo == null)
                throw new MissingMemberException(ScaleLocalization.Source("Scale module native FORM members", "Scale モジュールが必要とする標準 FORM メンバー"));
            registered = true;
            Log("RUNTIME_READY api=1 hooks=core grid=unchanged");
        }

        internal static void AttachRuntime(Build __instance)
        {
            if (!registered || __instance == null || __instance.GetComponent<ConnectedShapeScaleV2>() != null) return;
            ConnectedShapeScaleV2 control = __instance.gameObject.AddComponent<ConnectedShapeScaleV2>();
            control.build = __instance;
            SculptScaleUi.Attach(__instance, control);
            Log("BUILD_READY entry=Sculpt-Scale key=2");
        }

        private void Update()
        {
            if (build == null) return;
            if (!open) return;
            if (!SessionCurrent())
            {
                Log("SESSION_CANCELLED reason=build-changed");
                Close();
                SculptScaleUi.ReturnToSculpt(build, "build-changed");
                return;
            }
            if (plannedText != scaleText || plannedAnchor != anchorPivot
                || plannedSingleCouplersOnly != singleCouplersOnly) Replan();
            RefreshGhosts();
        }

        internal void BeginManualSelection()
        {
            Close();
            scaleText = "1";
            status = ScaleLocalization.Source("Select a mesh: LMB; add: Ctrl + LMB; remove: Ctrl + RMB.", "メッシュを選択: 左クリック。追加: Ctrl + 左クリック。除外: Ctrl + 右クリック。");
            Log("SELECTION_READY entry=Scale automaticSelection=false source=manual-click");
        }

        internal void SelectFromPointer() { SelectFromPointer(ShapeSelectionOperation.Replace); }

        internal void SelectFromPointer(ShapeSelectionOperation operation)
        {
            HDBLLPODNLN graph = Graph.GetValue(build) as HDBLLPODNLN;
            FreeCameraController camera = CameraControl.GetValue(build) as FreeCameraController;
            if (graph == null || camera == null) return;
            camera.DJADOKFCOOB();
            string details;
            BlockController picked = PickUnderCursor(camera, graph, out details);
            if (operation == ShapeSelectionOperation.Replace || !open && operation == ShapeSelectionOperation.Add)
            {
                Close();
                OpenSelection(false, picked, details);
                return;
            }
            if (!open) return; // Removing from an empty set keeps Scale active.
            int previousRevision = selectionRevision;
            try
            {
                if (!SessionCurrent()) throw new InvalidOperationException(ScaleLocalization.Source("The machine has changed.", "マシンが変更されました。"));
                Log("CURSOR_PICK operation=" + operation + " " + details);
                if (picked == null) throw new InvalidOperationException(ScaleLocalization.Source("No selectable mesh is under the cursor.", "カーソルの下に選択可能なメッシュがありません。"));
                if (picked.JNKEKNOAPHO == null || !ShapeScaleVisibility.DisplayType(picked.JNKEKNOAPHO.type))
                    throw new InvalidOperationException(ScaleLocalization.Source("Select a Box, Capsule or Coupler.", "Box、Capsule、Coupler のいずれかを選択してください。"));
                List<Member> candidate = ResolveSelectionCandidate(graph, picked);
                CommitManualSelection(candidate, picked, operation);
            }
            catch (Exception error)
            {
                status = (selectionRevision == previousRevision ? ScaleLocalization.Source("Selection unchanged: ", "選択を変更しませんでした: ") : ScaleLocalization.Source("Could not update the preview: ", "プレビューを更新できませんでした: ")) + error.Message;
                Log("MANUAL_SELECTION_REJECTED operation=" + operation + " retainedBlocks=" + members.Count + " " + error);
            }
        }

        private List<Member> ResolveSelectionCandidate(HDBLLPODNLN graph, BlockController picked)
        {
            PreviewFrame savedFrame = SnapshotPreviewFrame();
            HashSet<BlockController> savedFixed = new HashSet<BlockController>(parameterFixedCouplers);
            try
            {
                parameterFixedCouplers.Clear();
                int contacts, components, unreadable;
                return SelectTouchingGroup(graph, picked, out contacts, out components, out unreadable);
            }
            finally
            {
                RestorePreviewFrame(savedFrame);
                parameterFixedCouplers.Clear();
                foreach (BlockController controller in savedFixed) parameterFixedCouplers.Add(controller);
            }
        }

        private void CommitManualSelection(List<Member> candidate, BlockController clicked, ShapeSelectionOperation operation)
        {
            Dictionary<int, Member> available = new Dictionary<int, Member>();
            int[] currentIds = new int[members.Count], candidateIds = new int[candidate.Count];
            for (int i = 0; i < members.Count; i++) { currentIds[i] = members[i].Index; available.Add(members[i].Index, members[i]); }
            for (int i = 0; i < candidate.Count; i++)
            {
                candidateIds[i] = candidate[i].Index;
                // An overlap retains its existing source and display frame.
                if (!available.ContainsKey(candidate[i].Index)) available.Add(candidate[i].Index, candidate[i]);
            }
            int[] nextIds = ManualShapeSelection.Apply(currentIds, candidateIds, operation);
            HashSet<BlockController> nextControllers = new HashSet<BlockController>();
            List<Member> next = new List<Member>();
            bool hasShape = false;
            foreach (int id in nextIds)
            {
                Member member = available[id];
                next.Add(member); nextControllers.Add(member.Controller);
                hasShape |= member.HasShape;
            }
            if (!hasShape)
            {
                Close();
                status = ScaleLocalization.Source("No shape selected. Add a mesh with Ctrl + LMB.", "形状が未選択です。Ctrl + 左クリックでメッシュを追加してください。");
                Log("MANUAL_SELECTION_EMPTY operation=" + operation + " mode=Scale-kept");
                return;
            }
            if (next.Count == members.Count && nextControllers.SetEquals(selectedCarriers))
            {
                Log("MANUAL_SELECTION_UNCHANGED operation=" + operation + " blocks=" + members.Count);
                return;
            }
            foreach (Member member in members)
                if (!nextControllers.Contains(member.Controller))
                    foreach (Ghost ghost in member.PreviewMeshes) DisposeGhost(ghost);
            members.Clear(); members.AddRange(next);
            if (operation == ShapeSelectionOperation.Add)
                foreach (Member member in candidate)
                    if (member.ParameterFixedCoupler && nextControllers.Contains(member.Controller))
                        available[member.Index].ParameterFixedCoupler = true;
            selectedCarriers.Clear(); parameterFixedCouplers.Clear(); ghosts.Clear();
            foreach (Member member in members)
            {
                selectedCarriers.Add(member.Controller);
                if (member.ParameterFixedCoupler) parameterFixedCouplers.Add(member.Controller);
                ghosts.AddRange(member.PreviewMeshes);
            }
            selectionAnchors.RemoveAll(delegate(BlockController controller) { return !selectedCarriers.Contains(controller); });
            if (operation == ShapeSelectionOperation.Add && selectedCarriers.Contains(clicked) && !selectionAnchors.Contains(clicked))
                selectionAnchors.Add(clicked);
            seed = null;
            if (selectionAnchors.Count > 0)
                foreach (Member member in members) if (member.Controller == selectionAnchors[0]) { seed = member; break; }
            if (seed == null) { seed = members[0]; selectionAnchors.Add(seed.Controller); }
            RecountManualSelection();
            selectionRevision++;
            Replan(); RefreshGhosts();
            Log("MANUAL_SELECTION_CHANGED operation=" + operation + " blocks=" + members.Count
                + " shapes=" + (boxCount + capCount) + " anchors=" + selectionAnchors.Count
                + " firstAnchor=" + seed.Index + " nativePose=one-session membership=explicit-controller-set");
        }

        private void RecountManualSelection()
        {
            boxCount = sculptBoxCount = capCount = couplerCount = singleCouplerCount = unsupportedCount = 0;
            Bounds bounds = default(Bounds);
            bool first = true;
            foreach (Member member in members)
            {
                BlockData data = member.Source;
                if (data.type == BlockData.AAHMDBHDCDK.BoxGen) { boxCount++; if (data.actionID[0] > 0) sculptBoxCount++; }
                else if (data.type == BlockData.AAHMDBHDCDK.CapGen) capCount++;
                else if (data.type == BlockData.AAHMDBHDCDK.Coupler) { couplerCount++; if (IsSingleCoupler(data)) singleCouplerCount++; }
                else unsupportedCount++;
                if (!member.HasShape) continue;
                if (first) { bounds = member.ShapeBounds; first = false; }
                else bounds.Encapsulate(member.ShapeBounds);
            }
            centerPivot = bounds.center;
        }

        private void OpenSelection(bool preferSculptSelection, BlockController pointerPick = null, string pointerDetails = null)
        {
            try
            {
                HIPBCCKFFAG assembly = Preview.GetValue(build) as HIPBCCKFFAG;
                if (assembly == null || assembly.HCMMJPFOIHD || Build.GFJLEEJELOL == null)
                    throw new InvalidOperationException(ScaleLocalization.Source("Exit the assembly preview.", "組み立てプレビューを終了してください。"));
                FreeCameraController camera = CameraControl.GetValue(build) as FreeCameraController;
                if (camera == null) throw new InvalidOperationException(ScaleLocalization.Source("The BUILD camera is unavailable.", "BUILD カメラを利用できません。"));
                // Build.FBGCMCPNDAH is the previously selected block. The native camera hit
                // can also miss generated FORM meshes, so inspect their visible geometry.
                camera.DJADOKFCOOB();
                HDBLLPODNLN graph = Graph.GetValue(build) as HDBLLPODNLN;
                if (graph == null || graph.GEGOAHPBGKB == null)
                    throw new InvalidOperationException(ScaleLocalization.Source("The FORM block graph is unavailable.", "FORM のブロックグラフを利用できません。"));
                string pickDetails;
                BlockController controller = pointerDetails != null ? pointerPick : !preferSculptSelection || SelectedController == null
                    ? null : SelectedController.GetValue(build) as BlockController;
                if (pointerDetails != null) pickDetails = pointerDetails;
                else if (controller != null && controller.JNKEKNOAPHO != null
                    && ShapeScaleVisibility.DisplayType(controller.JNKEKNOAPHO.type)
                    && graph.GEGOAHPBGKB.Contains(controller))
                    pickDetails = "route=sculpt-selected selectedType=" + controller.JNKEKNOAPHO.type;
                else controller = PickUnderCursor(camera, graph, out pickDetails);
                Log("CURSOR_PICK " + pickDetails);
                if (controller == null)
                    throw new InvalidOperationException(ScaleLocalization.Source("Select a Box or Capsule in Sculpt, then open Scale.", "Sculpt で Box または Capsule を選択してから Scale を開いてください。"));
                Log("OPEN_INPUT blockType=" + controller.JNKEKNOAPHO.type
                    + " position=" + controller.JNKEKNOAPHO.GetPosString());
                selectedBuild = Build.GFJLEEJELOL;
                selectedBlockCount = selectedBuild.blockData.Count;
                members.Clear(); selectedCarriers.Clear(); parameterFixedCouplers.Clear(); edits.Clear(); ghosts.Clear(); seed = null;
                ConfigureSessionPoseRoot(graph, controller);
                CaptureAssembledPose(graph);
                boxCount = sculptBoxCount = capCount = couplerCount = singleCouplerCount = unsupportedCount = 0;
                int physicalContacts, nativeComponents, unreadableMeshes;
                List<Member> component = SelectTouchingGroup(graph, controller,
                    out physicalContacts, out nativeComponents, out unreadableMeshes);
                Bounds bounds = default(Bounds);
                bool hasBounds = false;
                foreach (Member member in component)
                {
                    BlockData data = member.Source;
                    members.Add(member);
                    selectedCarriers.Add(member.Controller);
                    if (member.Controller == controller) seed = member;
                    if (data.type == BlockData.AAHMDBHDCDK.BoxGen)
                    {
                        boxCount++;
                        if (data.actionID[0] > 0) sculptBoxCount++;
                    }
                    else if (data.type == BlockData.AAHMDBHDCDK.CapGen) capCount++;
                    else if (data.type == BlockData.AAHMDBHDCDK.Coupler)
                    {
                        couplerCount++;
                        if (IsSingleCoupler(data)) singleCouplerCount++;
                    }
                    else unsupportedCount++;
                    if (member.HasShape)
                    {
                        ghosts.AddRange(member.PreviewMeshes);
                        if (!hasBounds) { bounds = member.ShapeBounds; hasBounds = true; }
                        else bounds.Encapsulate(member.ShapeBounds);
                    }
                }
                if (seed == null || !hasBounds || boxCount + capCount == 0)
                {
                    Log("GROUP_GEOMETRY_MISSING blocks=" + members.Count + " box=" + boxCount + " cap=" + capCount
                        + " coupler=" + couplerCount + " ghosts=" + ghosts.Count + " seed=" + (seed == null ? -1 : seed.Index));
                    throw new InvalidOperationException(ScaleLocalization.Source("The group has no available Box/Capsule geometry.", "グループ内に利用可能な Box/Capsule の形状がありません。"));
                }
                centerPivot = bounds.center;
                selectionAnchors.Clear(); selectionAnchors.Add(controller);
                anchorPivot = false;
                singleCouplersOnly = true;
                scaleText = "1";
                showPreview = true;
                open = true;
                selectionRevision++;
                Replan();
                RefreshGhosts();
                Log("GROUP_SELECTED blocks=" + members.Count + " box=" + boxCount + " sculptBox=" + sculptBoxCount + " cap=" + capCount
                    + " coupler=" + couplerCount + " singleCoupler=" + singleCouplerCount
                    + " unsupported=" + unsupportedCount + " seed=" + seed.Index
                    + " assemblyGroups=" + originalGroups.Length
                    + " nativeComponents=" + nativeComponents + " physicalContacts=" + physicalContacts
                    + " unreadableMeshes=" + unreadableMeshes);
            }
            catch (Exception error)
            {
                Log("OPEN_FAILED " + error);
                Close();
                status = error.Message;
            }
        }

        private static List<BlockController> SelectNativeGroup(HDBLLPODNLN graph, BlockController start)
        {
            List<BlockController> all = graph.GEGOAHPBGKB;
            bool[] previous = new bool[all.Count];
            for (int i = 0; i < all.Count; i++) previous[i] = all[i].MKHLOPIFNDI;
            try
            {
                graph.HKIECNBJOFP(start);
                List<BlockController> result = new List<BlockController>();
                for (int i = 0; i < all.Count; i++) if (!all[i].MKHLOPIFNDI) result.Add(all[i]);
                return result;
            }
            finally
            {
                for (int i = 0; i < all.Count && i < previous.Length; i++) all[i].MKHLOPIFNDI = previous[i];
            }
        }

        private List<Member> SelectTouchingGroup(HDBLLPODNLN graph, BlockController start,
            out int physicalContacts, out int nativeComponents, out int unreadableMeshes)
        {
            List<Member> all = new List<Member>();
            Dictionary<BlockController, Member> byController = new Dictionary<BlockController, Member>();
            unreadableMeshes = 0;
            foreach (BlockController controller in graph.GEGOAHPBGKB)
            {
                BlockData data = controller.JNKEKNOAPHO;
                int index = selectedBuild.blockData.IndexOf(data);
                if (index < 0) throw new InvalidOperationException(ScaleLocalization.Source("The graph contains a block outside the current machine.", "グラフに現在のマシンに属さないブロックがあります。"));
                Member member = new Member { Index = index, Controller = controller, Source = data,
                    Primitive = PrimitiveOf(controller) };
                if (!groupByController.TryGetValue(controller, out member.Group))
                    throw new InvalidOperationException(ScaleLocalization.Source("No assembly group was found for block ", "次のブロックの組み立てグループがありません: ") + index);
                Bounds ignored;
                member.HasShape = PrepareShape(member, out ignored);
                if (member.HasShape && member.ContactMeshes.Count == 0) unreadableMeshes++;
                all.Add(member);
                byController.Add(controller, member);
            }
            HashSet<BlockController> selected = new HashSet<BlockController>();
            physicalContacts = nativeComponents = 0;
            Member initial = byController[start];
            int[] parents;
            bool[] couplerLinks;
            BranchGraph(out parents, out couplerLinks);
            int[] driverIds = new int[parents.Length], pairBaseIds = new int[parents.Length];
            bool[] detachedGroups = new bool[parents.Length];
            for (int i = 0; i < parents.Length; i++)
            {
                driverIds[i] = pairBaseIds[i] = -1;
                Member owner;
                if (originalGroups[i].Driver != null && byController.TryGetValue(originalGroups[i].Driver, out owner))
                    driverIds[i] = owner.Index;
                if (originalGroups[i].CouplerBaseController != null
                    && byController.TryGetValue(originalGroups[i].CouplerBaseController, out owner))
                    pairBaseIds[i] = owner.Index;
            }
            foreach (BlockController detached in originalVisualPose.Detached)
            {
                int group;
                if (groupByController.TryGetValue(detached, out group)) detachedGroups[group] = true;
            }
            ConnectedShapePoseHierarchy.Result branch = ConnectedShapePoseHierarchy.Resolve(parents,
                couplerLinks, driverIds, pairBaseIds, detachedGroups, initial.Index, initial.Group,
                initial.Source.type == BlockData.AAHMDBHDCDK.Coupler);
            if (!branch.Success)
            {
                Log("BRANCH_REJECTED seed=" + initial.Source.GetPosString() + " reason=" + branch.Reason);
                throw new InvalidOperationException(branch.Reason);
            }
            ConfigurePreviewFrame(branch, initial);
            HashSet<BlockController> rootDrivers = new HashSet<BlockController>();
            foreach (int root in branch.RootGroups)
                if (originalGroups[root].Driver != null && couplerLinks[root])
                    rootDrivers.Add(originalGroups[root].Driver);
            int nativeShapes = 0;
            foreach (Member member in all)
                if ((branch.SelectedGroups[member.Group] || member.Controller == start || rootDrivers.Contains(member.Controller))
                    && (sessionAllowedControllers == null || sessionAllowedControllers.Contains(member.Controller)))
                {
                    selected.Add(member.Controller);
                    if (member.HasShape) nativeShapes++;
                }
            if (ShouldUseForwardGeometryFallback(initial.Source.type == BlockData.AAHMDBHDCDK.Coupler, nativeShapes))
            {
                // A private coreless anchor establishes only this native driver
                // subtree. The shared-form fallback needs a real Core to prove
                // its direction; never pass the mounting block off as a Core.
                if (sessionAllowedControllers != null)
                    throw new InvalidOperationException(ScaleLocalization.Source("Could not assemble this branch from the selected anchor. Select its initial Single Coupler or add a Core.", "選択した基準から枝の形状を組み立てられません。枝の根元の Single Coupler を選択するか、Core を追加してください。"));
                // Two neighboring Coupler can reach the same rigid forms. Native
                // traversal gives that geometry to one driver and leaves the other
                // empty. Select the forward forms individually, retaining their
                // actual native frames and every Coupler offset in this fallback.
                ConnectedShapeTopology topology = ConnectedShapeTopology.Build(graph,
                    GraphRoot.GetValue(graph) as BlockController);
                ConnectedShapeHierarchy.Result contact = topology.Hierarchy.SelectForwardGeometryCandidate(topology.NodeOf(start));
                if (!contact.Success)
                    throw new InvalidOperationException(contact.Reason);
                int contactShapes = 0;
                foreach (Member member in all)
                    if (contact.SelectedNodes[topology.NodeOf(member.Controller)]
                        && (member.HasShape || member.Source.type == BlockData.AAHMDBHDCDK.Coupler)
                        && (sessionAllowedControllers == null || sessionAllowedControllers.Contains(member.Controller)))
                    {
                        selected.Add(member.Controller);
                        if (member.HasShape) contactShapes++;
                    }
                foreach (Member member in all)
                    if (selected.Contains(member.Controller) && member.Source.type == BlockData.AAHMDBHDCDK.Coupler)
                        parameterFixedCouplers.Add(member.Controller);
                Log("FORWARD_GEOMETRY_FALLBACK seed=" + initial.Source.GetPosString()
                    + " nativeShapes=" + nativeShapes + " forwardShapes=" + contactShapes
                    + " fixedCouplers=" + parameterFixedCouplers.Count
                    + " ownerGroups=native-unchanged parentAndSiblingCarriers=excluded");
                if (contactShapes == 0)
                    throw new InvalidOperationException(ScaleLocalization.Source("No available Box/Capsule geometry is downstream of the selected Coupler.", "選択した Coupler の先に利用可能な Box/Capsule の形状がありません。"));
            }
            int selectedShapes = 0, previewMeshes = 0;
            foreach (Member member in all)
                if (selected.Contains(member.Controller) && member.HasShape)
                {
                    if (!VisualPoseAvailable(member.Controller))
                        throw new InvalidOperationException(ScaleLocalization.Source("Could not calculate the selected shape attachment: ", "選択した形状の取り付けを計算できませんでした: ")
                            + member.Source.GetPosString());
                    PrepareAssembledGeometry(member);
                    selectedShapes++;
                    previewMeshes += member.PreviewMeshes.Count;
                }
            Log("GROUP_GEOMETRY_READY seed=" + initial.Source.GetPosString()
                + " shapes=" + selectedShapes + " previewMeshes=" + previewMeshes
                + " source=native-frames individualControllers=true");
            if (selectedShapes == 0 || previewMeshes == 0)
                throw new InvalidOperationException(ScaleLocalization.Source("The group has no available Box/Capsule geometry.", "グループ内に利用可能な Box/Capsule の形状がありません。"));
            for (int i = 0; i < branch.SelectedGroups.Length; i++)
                if (branch.SelectedGroups[i]) nativeComponents++;
            Log("BRANCH_SELECTED roots=" + string.Join(",", Array.ConvertAll(branch.RootGroups,
                    delegate(int value) { return value.ToString(CultureInfo.InvariantCulture); }))
                + " seed=" + initial.Source.GetPosString() + " nodes=" + nativeComponents
                + " blocks=" + selected.Count
                + " route=" + branch.Route + " displayParent=" + branch.DisplayParent
                + " direction=native-driver-descendants edges=Coupler-only selection=native-editor-visual-parents");
            List<Member> result = new List<Member>();
            PreviewFrame frame = SnapshotPreviewFrame();
            foreach (Member member in all) if (selected.Contains(member.Controller))
            {
                member.Frame = frame;
                member.ParameterFixedCoupler = parameterFixedCouplers.Contains(member.Controller);
                result.Add(member);
            }
            return result;
        }

        internal static bool ShouldUseForwardGeometryFallback(bool seedIsCoupler, int nativeShapeCount)
        { return seedIsCoupler && nativeShapeCount == 0; }

        private void ConfigureSessionPoseRoot(HDBLLPODNLN graph, BlockController selected)
        {
            sessionPoseRoot = GraphRoot.GetValue(graph) as BlockController;
            sessionAllowedControllers = null;
            if (sessionPoseRoot != null)
            {
                Log("POSE_ROOT source=machine-Core");
                return;
            }
            if (selected == null || !IsSingleCoupler(selected.JNKEKNOAPHO))
                throw new InvalidOperationException(ScaleLocalization.Source("Without a Core, select the Single Coupler at the start of the branch.", "Core がない場合は、目的の枝の根元にある Single Coupler を選択してください。"));
            ConnectedShapeTopology topology = ConnectedShapeTopology.Build(graph, null, true);
            bool[] geometry = new bool[topology.Kinds.Length];
            foreach (KeyValuePair<BlockController, int> entry in topology.BlockNode)
            {
                BlockData.AAHMDBHDCDK type = entry.Key.JNKEKNOAPHO.type;
                if (type == BlockData.AAHMDBHDCDK.BoxGen || type == BlockData.AAHMDBHDCDK.CapGen)
                    geometry[entry.Value] = true;
            }
            List<RootAnchorSelectionPolicy.Contact> contacts = new List<RootAnchorSelectionPolicy.Contact>();
            Dictionary<int, BlockController> byId = new Dictionary<int, BlockController>();
            foreach (BlockController contact in topology.ContactsOf(selected))
            {
                int id = selectedBuild.blockData.IndexOf(contact.JNKEKNOAPHO);
                if (id < 0) throw new InvalidOperationException(ScaleLocalization.Source("The anchor is outside the current machine.", "基準が現在のマシンに属していません。"));
                contacts.Add(new RootAnchorSelectionPolicy.Contact(topology.NodeOf(contact), id));
                byId[id] = contact;
            }
            RootAnchorSelectionPolicy.Result result = RootAnchorSelectionPolicy.Resolve(topology.Kinds,
                topology.Adjacent, geometry, topology.NodeOf(selected), contacts.ToArray());
            if (!result.Success) throw new InvalidOperationException(result.Reason);
            sessionPoseRoot = byId[result.AnchorControllerId];
            sessionAllowedControllers = new HashSet<BlockController>();
            foreach (KeyValuePair<BlockController, int> entry in topology.BlockNode)
                if (result.SelectedNodes[entry.Value]) sessionAllowedControllers.Add(entry.Key);
            Log("POSE_ROOT source=coreless-real-anchor seed=" + selected.JNKEKNOAPHO.GetPosString()
                + " anchor=" + sessionPoseRoot.JNKEKNOAPHO.GetPosString()
                + " allowedBlocks=" + sessionAllowedControllers.Count
                + " connections=native-CheckLinkG machineCore=not-added");
        }

        private int FindCouplerBranchRoot(Member initial)
        {
            if (initial.Source.type == BlockData.AAHMDBHDCDK.Coupler)
            {
                // A precise driver match wins over every paired-base position.
                // Otherwise an earlier base can steal a later driver's branch.
                for (int i = 0; i < originalGroups.Length; i++)
                    if (originalGroups[i].Driver == initial.Controller) return i;
                for (int i = 0; i < originalGroups.Length; i++)
                {
                    GroupPose group = originalGroups[i];
                    BlockData basis = group.CouplerBase;
                    if (basis != null && basis.type == BlockData.AAHMDBHDCDK.Coupler
                        && basis.x == initial.Source.x && basis.y == initial.Source.y && basis.z == initial.Source.z)
                        return i;
                }
            }
            GroupPose own = originalGroups[initial.Group];
            return own.Driver != null && own.Driver.JNKEKNOAPHO.type == BlockData.AAHMDBHDCDK.Coupler
                ? initial.Group : -1;
        }

        private void BranchGraph(out int[] parents, out bool[] couplerLinks)
        {
            parents = new int[originalGroups.Length];
            couplerLinks = new bool[parents.Length];
            for (int i = 0; i < parents.Length; i++)
            {
                GroupPose group = originalGroups[i];
                parents[i] = group.Parent < 0 ? -1 : group.Parent;
                couplerLinks[i] = group.Driver != null
                    && group.Driver.JNKEKNOAPHO.type == BlockData.AAHMDBHDCDK.Coupler;
            }
        }

        private void CaptureAssembledPose(HDBLLPODNLN source, List<Edit> staged = null)
        {
            if (staged != null) throw new InvalidOperationException(ScaleLocalization.Source("Use explicit staged visual/physical snapshots.", "表示と物理の検証用スナップショットを明示的に使用してください。"));
            // Native group boundaries determine the actual Coupler parents.
            // Removing the gid filter can produce the same number of bodies but
            // attach their geometry to another driver (Q_7103). The render and
            // inverse offset frames must use precisely the native assembly.
            PoseSnapshot native = CapturePose(source, null, false);
            originalPhysicalPose = native;
            originalVisualPose = native;
            originalGroups = native.Groups;
            groupByController.Clear();
            foreach (KeyValuePair<BlockController, int> entry in native.GroupByController)
                groupByController.Add(entry.Key, entry.Value);
            Log("EDITOR_POSE_SELECTED source=native-CheckLinkG visualAndPhysical=same-snapshot"
                + " looseGroupBypass=false unresolvedParts=explicit-rejection");
        }

        private PoseSnapshot CapturePose(HDBLLPODNLN source, List<Edit> staged, bool visual)
        {
            if (ConnectedShapePoseScope.Active) throw new InvalidOperationException(ScaleLocalization.Source("A pose calculation is already in progress.", "姿勢の計算が既に実行中です。"));
            // The editor graph has no reliable pose arrays. Run the game's assembly
            // traversal on a separate graph. It can edit BlockData and joint visuals,
            // so give it disposable BlockData copies and restore every controller.
            List<ControllerState> saved = new List<ControllerState>();
            List<JointState> joints = new List<JointState>();
            Dictionary<BlockController, Edit> stagedByController = new Dictionary<BlockController, Edit>();
            if (staged != null)
                foreach (Edit edit in staged) stagedByController.Add(edit.Member.Controller, edit);
            foreach (BlockController controller in source.GEGOAHPBGKB)
            {
                BlockData original = controller.JNKEKNOAPHO;
                if (original == null) throw new InvalidOperationException(ScaleLocalization.Source("A block in the FORM graph has no data.", "FORM グラフ内にデータのないブロックがあります。"));
                BlockData copy = original.Clone();
                Edit edit;
                if (stagedByController.TryGetValue(controller, out edit))
                {
                    ShapeDetailRetention.RetainForScaleShrink(copy, original, factor);
                    for (int i = 0; i < edit.Slots.Length; i++)
                        SetupPrecisionData.Set(copy, edit.Slots[i], edit.Values[i]);
                }
                saved.Add(new ControllerState { Controller = controller, Data = original, ProbeData = copy,
                    Group = controller.DCNIOOFAOMB,
                    Parent = controller.ILFEIIFBHMP, Lost = controller.LJOAMOJGJIL,
                    Selected = controller.MKHLOPIFNDI, Enabled = controller.enabled,
                    X = original.x, Y = original.y, Z = original.z, Gid = original.gid,
                    Parameters = original.actionParam == null ? null : (int[])original.actionParam.Clone(),
                    Actions = original.actionID == null ? null : (int[])original.actionID.Clone(),
                    PrecisionValues = PrecisionValues(original),
                    Pair = controller is CouplerController ? ((CouplerController)controller).OCICOKJOCKB : null });
                JointController joint = controller as JointController;
                if (joint != null)
                {
                    Transform first = joint.EKDOBAJILBI == null ? null : joint.EKDOBAJILBI.transform;
                    Transform second = joint.NDFBMKMPJHA == null ? null : joint.NDFBMKMPJHA.transform;
                    joints.Add(new JointState { Controller = joint, First = first, Second = second,
                        FirstPosition = first == null ? Vector3.zero : first.localPosition,
                        SecondPosition = second == null ? Vector3.zero : second.localPosition,
                        FirstRotation = first == null ? Quaternion.identity : first.localRotation,
                        SecondRotation = second == null ? Quaternion.identity : second.localRotation,
                        Flipped = joint.EHICOGEOIIB });
                }
            }
            HDBLLPODNLN probe = new HDBLLPODNLN();
            GraphCells.SetValue(probe, GraphCells.GetValue(source));
            // The optional coreless root is an existing mounting block, stored
            // for the complete session and used identically by staged validation.
            GraphRoot.SetValue(probe, sessionPoseRoot);
            probe.GEGOAHPBGKB = new List<BlockController>(source.GEGOAHPBGKB);
            probe.CJBGADKMKIC = true;
            PoseSnapshot result = new PoseSnapshot();
            try
            {
                foreach (ControllerState state in saved)
                {
                    state.Controller.JNKEKNOAPHO = state.ProbeData;
                    state.Controller.MKHLOPIFNDI = false;
                }
                int count;
                if (visual)
                {
                    using (ConnectedShapePoseScope.Enter(probe.GEGOAHPBGKB)) count = probe.JKAJGAGDMAJ();
                }
                else count = probe.JKAJGAGDMAJ();
                if (count <= 0 || probe.PKBPJPCJAID == null || probe.MJBDKMNEKML == null
                    || probe.NNNBCKKNONF == null || probe.AJKCGCCJHDJ == null
                    || probe.PKBPJPCJAID.Length != count || probe.MJBDKMNEKML.Length != count
                    || probe.NNNBCKKNONF.Length != count || probe.AJKCGCCJHDJ.Length != count)
                    throw new InvalidOperationException(ScaleLocalization.Source("The native Coupler assembly graph is not ready.", "標準の Coupler 組み立てグラフが準備できていません。"));
                int detached = 0;
                foreach (ControllerState state in saved)
                    if (state.Controller.DCNIOOFAOMB < 0) detached++;
                result.Groups = new GroupPose[count + (detached > 0 ? 1 : 0)];
                Dictionary<int, CouplerController> bases = CouplerBases.GetValue(probe)
                    as Dictionary<int, CouplerController>;
                for (int i = 0; i < count; i++)
                {
                    int parent = probe.PKBPJPCJAID[i];
                    if (parent >= count || parent == i)
                        throw new InvalidOperationException(ScaleLocalization.Source("Invalid parent for group ", "親が不正なグループ: ") + i);
                    result.Groups[i] = new GroupPose { Parent = parent,
                        Driver = probe.AJKCGCCJHDJ[i], LocalPosition = parent < 0 ? Vector3.zero : probe.NNNBCKKNONF[i],
                        LocalRotation = parent < 0 ? Quaternion.identity
                            : BDLEJBBJJOI.APHLFOGDCBI(BDLEJBBJJOI.GKCKPLGPDFK(probe.MJBDKMNEKML[i])) };
                    BlockController driver = result.Groups[i].Driver;
                    CouplerController baseController;
                    if (driver != null && driver.JNKEKNOAPHO.type == BlockData.AAHMDBHDCDK.Coupler
                        && driver.JNKEKNOAPHO.actionParam[7] == 0 && bases != null
                        && bases.TryGetValue(driver.JNKEKNOAPHO.rgbI, out baseController)
                        && baseController != null)
                    {
                        result.Groups[i].CouplerBase = baseController.JNKEKNOAPHO;
                        result.Groups[i].CouplerBaseController = baseController;
                    }
                }
                if (detached > 0)
                    result.Groups[count] = new GroupPose { Parent = -1,
                        LocalPosition = Vector3.zero, LocalRotation = Quaternion.identity };
                foreach (ControllerState state in saved)
                {
                    int group = state.Controller.DCNIOOFAOMB;
                    if (group < 0) { result.Detached.Add(state.Controller); group = count; }
                    if (group >= result.Groups.Length)
                        throw new InvalidOperationException(ScaleLocalization.Source("Invalid block assembly group: ", "ブロックの組み立てグループが不正です: ")
                            + state.Controller.JNKEKNOAPHO.GetPosString());
                    result.GroupByController.Add(state.Controller, group);
                }
                int[] visiting = new int[result.Groups.Length];
                for (int i = 0; i < result.Groups.Length; i++) ResolveSnapshotGroup(result.Groups, i, visiting);
                int rotated = 0, couplerDrivers = 0;
                for (int i = 0; i < count; i++)
                    if (result.Groups[i].Driver != null
                        && result.Groups[i].Driver.JNKEKNOAPHO.type == BlockData.AAHMDBHDCDK.Coupler)
                    {
                        couplerDrivers++;
                        if (Quaternion.Angle(result.Groups[i].LocalRotation, Quaternion.identity) > 0.01f)
                            rotated++;
                    }
                Log((visual ? "EDITOR_VISUAL_POSE" : "PHYSICAL_POSE") + " phase=" + (staged == null ? "open" : "verify")
                    + " groups=" + count + " detached=" + detached + " blocks=" + saved.Count
                    + " couplerDrivers=" + couplerDrivers + " rotatedCouplers=" + rotated);
                for (int i = 0; i < count; i++)
                    Log((visual ? "EDITOR_VISUAL_GROUP" : "PHYSICAL_GROUP") + " index=" + i + " parent=" + result.Groups[i].Parent
                        + " driver=" + (result.Groups[i].Driver == null ? "none"
                            : result.Groups[i].Driver.JNKEKNOAPHO.GetPosString())
                        + " local=" + result.Groups[i].LocalPosition.ToString("F3")
                        + " world=" + result.Groups[i].WorldPosition.ToString("F3")
                        + " rotation=" + result.Groups[i].WorldRotation.eulerAngles.ToString("F2"));
                return result;
            }
            finally
            {
                foreach (JointState joint in joints)
                {
                    if (joint.First != null)
                    {
                        joint.First.localPosition = joint.FirstPosition;
                        joint.First.localRotation = joint.FirstRotation;
                    }
                    if (joint.Second != null)
                    {
                        joint.Second.localPosition = joint.SecondPosition;
                        joint.Second.localRotation = joint.SecondRotation;
                    }
                    joint.Controller.EHICOGEOIIB = joint.Flipped;
                }
                foreach (ControllerState state in saved)
                {
                    state.Controller.JNKEKNOAPHO = state.Data;
                    state.Controller.DCNIOOFAOMB = state.Group;
                    state.Controller.ILFEIIFBHMP = state.Parent;
                    state.Controller.LJOAMOJGJIL = state.Lost;
                    state.Controller.MKHLOPIFNDI = state.Selected;
                    state.Controller.enabled = state.Enabled;
                    CouplerController coupler = state.Controller as CouplerController;
                    if (coupler != null) coupler.OCICOKJOCKB = state.Pair;
                }
                bool preserved = !ConnectedShapePoseScope.Active;
                foreach (ControllerState state in saved)
                {
                    bool controllerPreserved = ReferenceEquals(state.Controller.JNKEKNOAPHO, state.Data)
                        && state.Controller.DCNIOOFAOMB == state.Group && state.Controller.ILFEIIFBHMP == state.Parent
                        && state.Controller.LJOAMOJGJIL == state.Lost && state.Controller.MKHLOPIFNDI == state.Selected
                        && state.Controller.enabled == state.Enabled
                        && state.Data.x == state.X && state.Data.y == state.Y && state.Data.z == state.Z && state.Data.gid == state.Gid
                        && SameParameters(state.Data.actionParam, state.Parameters) && SameParameters(state.Data.actionID, state.Actions)
                        && SamePrecisionValues(state.Data, state.PrecisionValues)
                        && (!(state.Controller is CouplerController)
                            || ReferenceEquals(((CouplerController)state.Controller).OCICOKJOCKB, state.Pair));
                    preserved &= controllerPreserved;
                    if (!controllerPreserved)
                        Log("POSE_RESTORE_MISMATCH block=" + saved.IndexOf(state)
                            + " position=" + state.Data.GetPosString() + " " + RestoredControllerMismatch(state));
                }
                foreach (JointState joint in joints)
                {
                    bool jointPreserved = joint.Controller.EHICOGEOIIB == joint.Flipped
                        && (joint.First == null || (joint.First.localPosition == joint.FirstPosition
                            && joint.First.localRotation == joint.FirstRotation))
                        && (joint.Second == null || (joint.Second.localPosition == joint.SecondPosition
                            && joint.Second.localRotation == joint.SecondRotation));
                    preserved &= jointPreserved;
                    if (!jointPreserved)
                        Log("POSE_RESTORE_MISMATCH position=" + joint.Controller.JNKEKNOAPHO.GetPosString()
                            + " " + RestoredJointMismatch(joint));
                }
                Log("POSE_PROBE_RESTORED kind=" + (visual ? "editor-visual" : "physical")
                    + " controllers=" + saved.Count + " joints=" + joints.Count + " preserved=" + preserved
                    + " data=original-references-and-parameter-values grid=unchanged physics=unchanged scopeActive=" + ConnectedShapePoseScope.Active);
                if (!preserved) throw new InvalidOperationException(ScaleLocalization.Source("Could not restore the state after calculating the pose.", "姿勢の計算後に状態を復元できませんでした。"));
            }
        }

        private void ResolveOriginalGroup(int index, int[] visiting)
        { ResolveSnapshotGroup(originalGroups, index, visiting); }

        private static string RestoredControllerMismatch(ControllerState state)
        {
            BlockController controller = state.Controller;
            if (!ReferenceEquals(controller.JNKEKNOAPHO, state.Data))
                return "field=DataReference expected=saved-reference actual=different-reference";
            if (controller.DCNIOOFAOMB != state.Group) return RestoreMismatch("Group", state.Group, controller.DCNIOOFAOMB);
            if (controller.ILFEIIFBHMP != state.Parent) return RestoreMismatch("Parent", state.Parent, controller.ILFEIIFBHMP);
            if (controller.LJOAMOJGJIL != state.Lost) return RestoreMismatch("Lost", state.Lost, controller.LJOAMOJGJIL);
            if (controller.MKHLOPIFNDI != state.Selected) return RestoreMismatch("Selected", state.Selected, controller.MKHLOPIFNDI);
            if (controller.enabled != state.Enabled) return RestoreMismatch("Enabled", state.Enabled, controller.enabled);
            if (state.Data.x != state.X) return RestoreMismatch("x", state.X, state.Data.x);
            if (state.Data.y != state.Y) return RestoreMismatch("y", state.Y, state.Data.y);
            if (state.Data.z != state.Z) return RestoreMismatch("z", state.Z, state.Data.z);
            if (state.Data.gid != state.Gid) return RestoreMismatch("gid", state.Gid, state.Data.gid);
            string array = ArrayRestoreMismatch("actionParam", state.Parameters, state.Data.actionParam);
            if (array != null) return array;
            array = ArrayRestoreMismatch("actionID", state.Actions, state.Data.actionID);
            if (array != null) return array;
            for (int i = 0; i < state.PrecisionValues.Length; i++)
            {
                if (state.PrecisionValues[i] == int.MinValue) continue;
                int actual = PrecisionSnapshotValue(state.Data, i);
                if (actual != state.PrecisionValues[i])
                    return "field=PrecisionValues slot=" + i
                        + " expectedScaled=" + state.PrecisionValues[i].ToString(CultureInfo.InvariantCulture)
                        + " actualScaled=" + actual.ToString(CultureInfo.InvariantCulture)
                        + " scale=" + SetupPrecisionData.Scale
                        + " supported=" + SetupPrecisionData.Supports(state.Data, i);
            }
            if (controller is CouplerController
                && !ReferenceEquals(((CouplerController)controller).OCICOKJOCKB, state.Pair))
                return "field=PairReference expected=saved-reference actual=different-reference";
            return "field=unknown expected=preserved actual=mismatch";
        }

        private static string RestoreMismatch(string field, object expected, object actual)
        {
            return "field=" + field + " expected=" + Convert.ToString(expected, CultureInfo.InvariantCulture)
                + " actual=" + Convert.ToString(actual, CultureInfo.InvariantCulture);
        }

        private static string ArrayRestoreMismatch(string field, int[] expected, int[] actual)
        {
            if (expected == null || actual == null)
                return expected == actual ? null : "field=" + field + " expected="
                    + (expected == null ? "null" : "array") + " actual=" + (actual == null ? "null" : "array");
            if (expected.Length != actual.Length) return RestoreMismatch(field + ".Length", expected.Length, actual.Length);
            for (int i = 0; i < expected.Length; i++)
                if (expected[i] != actual[i]) return RestoreMismatch(field + "[" + i + "]", expected[i], actual[i]);
            return null;
        }

        private static string RestoredJointMismatch(JointState state)
        {
            if (state.Controller.EHICOGEOIIB != state.Flipped)
                return RestoreMismatch("Joint.Flipped", state.Flipped, state.Controller.EHICOGEOIIB);
            if (state.First != null && state.First.localPosition != state.FirstPosition)
                return "field=Joint.FirstPosition expected=" + state.FirstPosition.ToString("F9")
                    + " actual=" + state.First.localPosition.ToString("F9");
            if (state.First != null && state.First.localRotation != state.FirstRotation)
                return "field=Joint.FirstRotation expected=" + QuaternionComponents(state.FirstRotation)
                    + " actual=" + QuaternionComponents(state.First.localRotation);
            if (state.Second != null && state.Second.localPosition != state.SecondPosition)
                return "field=Joint.SecondPosition expected=" + state.SecondPosition.ToString("F9")
                    + " actual=" + state.Second.localPosition.ToString("F9");
            if (state.Second != null && state.Second.localRotation != state.SecondRotation)
                return "field=Joint.SecondRotation expected=" + QuaternionComponents(state.SecondRotation)
                    + " actual=" + QuaternionComponents(state.Second.localRotation);
            return "field=Joint.Unknown expected=preserved actual=mismatch";
        }

        private static int PrecisionSnapshotValue(BlockData data, int slot)
        {
            return SetupPrecisionData.Supports(data, slot)
                ? SetupPrecisionData.ReadScaled(data, slot) : int.MinValue;
        }

        private static int[] PrecisionValues(BlockData data)
        {
            // Mono x86 can compare an extended FPU return from Read() with a
            // rounded float-array value as unequal. Preserve the exact encoded
            // thousandths, including native integer fallback, without tolerance.
            int[] values = new int[8];
            for (int i = 0; i < values.Length; i++)
                values[i] = PrecisionSnapshotValue(data, i);
            return values;
        }

        private static bool SamePrecisionValues(BlockData data, int[] values)
        {
            for (int i = 0; i < values.Length; i++)
                if (values[i] != int.MinValue && PrecisionSnapshotValue(data, i) != values[i]) return false;
            return true;
        }

        private static bool SameParameters(int[] actual, int[] before)
        {
            if (actual == null || before == null) return actual == before;
            if (actual.Length != before.Length) return false;
            for (int i = 0; i < actual.Length; i++) if (actual[i] != before[i]) return false;
            return true;
        }

        private static void ResolveSnapshotGroup(GroupPose[] groups, int index, int[] visiting)
        {
            if (visiting[index] == 2) return;
            if (visiting[index] == 1) throw new InvalidOperationException(ScaleLocalization.Source("Cycle in the Coupler group graph.", "Coupler のグループグラフに循環があります。"));
            visiting[index] = 1;
            GroupPose group = groups[index];
            if (group.Parent < 0)
            {
                group.WorldPosition = Vector3.zero;
                group.WorldRotation = Quaternion.identity;
            }
            else
            {
                ResolveSnapshotGroup(groups, group.Parent, visiting);
                GroupPose parent = groups[group.Parent];
                group.WorldPosition = parent.WorldPosition + parent.WorldRotation * group.LocalPosition;
                group.WorldRotation = parent.WorldRotation * group.LocalRotation;
            }
            visiting[index] = 2;
        }

        private static void AddNativeComponent(HDBLLPODNLN graph, BlockController start,
            Dictionary<BlockController, Member> byController, HashSet<BlockController> selected, Queue<Member> frontier)
        {
            foreach (BlockController controller in SelectNativeGroup(graph, start))
            {
                if (!selected.Add(controller)) continue;
                Member member;
                if (!byController.TryGetValue(controller, out member))
                    throw new InvalidOperationException(ScaleLocalization.Source("The graph changed while selecting touching shapes.", "接触する形状の選択中にグラフが変更されました。"));
                frontier.Enqueue(member);
            }
        }

        private static bool ShapesTouch(Member a, Member b)
        {
            foreach (ConnectedShapeContact.Hull left in a.ContactMeshes)
                foreach (ConnectedShapeContact.Hull right in b.ContactMeshes)
                    if (ConnectedShapeContact.Touches(left, right, ContactTolerance)) return true;
            return false;
        }

        private BlockController PickUnderCursor(FreeCameraController camera, HDBLLPODNLN graph, out string details)
        {
            List<BlockController> all = graph.GEGOAHPBGKB;
            GameObject nativeHit = camera.FBGCMCPNDAH;
            BlockController native = nativeHit == null ? null : SculptScaleUi.PickOwner(nativeHit.transform);
            Camera world = camera.EBOMCDGDKCP != null ? camera.EBOMCDGDKCP : Build.EBOMCDGDKCP;
            if (world == null)
            {
                details = "route=none camera=null";
                return null;
            }
            Ray ray = world.ScreenPointToRay(Input.mousePosition);
            BlockController selected = null;
            float selectedDistance = float.PositiveInfinity;
            string route = "none";
            int carrierSurfaces = 0, exactMeshes = 0;
            foreach (BlockController candidate in all)
            {
                if (candidate == null || candidate.JNKEKNOAPHO == null) continue;
                BlockData.AAHMDBHDCDK type = candidate.JNKEKNOAPHO.type;
                if (!ShapeScaleVisibility.DisplayType(type)) continue;
                float carrierDistance;
                // The native cursor/collider hit is only diagnostic. Selection
                // requires a displayed triangle, including for editor Coupler.
                if (SculptScaleUi.TryCarrierSurface(candidate, ray, out carrierDistance))
                {
                    carrierSurfaces++;
                    if (carrierDistance < selectedDistance)
                    { selected = candidate; selectedDistance = carrierDistance; route = "carrier-surface"; }
                }
                if (type != BlockData.AAHMDBHDCDK.BoxGen && type != BlockData.AAHMDBHDCDK.CapGen) continue;
                GameObject primitive = PrimitiveOf(candidate);
                if (primitive == null) continue;
                foreach (MeshFilter filter in primitive.GetComponentsInChildren<MeshFilter>(true))
                {
                    MeshRenderer renderer = filter.GetComponent<MeshRenderer>();
                    if (SculptScaleUi.PickOwner(filter.transform) != candidate) continue;
                    float distance;
                    if (ShapeScalePicking.TrySurface(renderer, ray, false, out distance))
                    {
                        exactMeshes++;
                        if (distance < selectedDistance)
                        { selected = candidate; selectedDistance = distance; route = "shape-surface"; }
                    }
                }
            }
            int previewMeshes = 0;
            foreach (Member member in members)
                foreach (Ghost ghost in member.PreviewMeshes)
                {
                    if (ghost.Object == null || !ghost.Object.activeInHierarchy) continue;
                    MeshRenderer renderer = ghost.Object.GetComponent<MeshRenderer>();
                    float distance;
                    if (ShapeScalePicking.TrySurface(renderer, ray, false, out distance))
                    {
                        previewMeshes++;
                        if (distance < selectedDistance)
                        { selected = member.Controller; selectedDistance = distance; route = "preview-surface"; }
                    }
                }
            details = "route=" + route + " nativeHit=" + (nativeHit == null ? "none" : nativeHit.name)
                + " nativeType=" + (native == null || native.JNKEKNOAPHO == null ? "none" : native.JNKEKNOAPHO.type.ToString())
                + " carrierSurfaces=" + carrierSurfaces + " exactMeshes=" + exactMeshes
                + " previewMeshes=" + previewMeshes
                + " surfaceDistance=" + selectedDistance.ToString("F3", CultureInfo.InvariantCulture)
                + " hiddenColliders=ignored boundsFallback=false"
                + " selectedType=" + (selected == null ? "none" : selected.JNKEKNOAPHO.type.ToString());
            return selected;
        }

        private static GameObject PrimitiveOf(BlockController controller)
        {
            BoxGenController box = controller as BoxGenController;
            if (box != null) return box.NGLBLAGMBLN;
            CapGenController cap = controller as CapGenController;
            return cap == null ? null : CapsuleMesh.GetValue(cap) as GameObject;
        }

        internal static bool IsGeneratedShapeRenderer(BlockController controller, Renderer renderer)
        {
            GameObject primitive = PrimitiveOf(controller);
            return primitive != null && renderer != null
                && (renderer.transform == primitive.transform || renderer.transform.IsChildOf(primitive.transform));
        }

        private static Material[] NativePreviewMaterials(BlockData data, MeshRenderer prefab, int subMeshes)
        {
            Material native = (data.rgbI & 0x1000000) != 0 ? PAEHEMJNPND.ILDEEHCIALM
                : PAEHEMJNPND.HFKPCBGBOAF != null && PAEHEMJNPND.HFKPCBGBOAF.Contains(data.rgbI)
                    ? PAEHEMJNPND.IODPGEFPKPL : PAEHEMJNPND.EGEGMNIIHCD;
            if (native == null)
            {
                Material[] original = prefab == null ? null : prefab.sharedMaterials;
                if (original == null || original.Length == 0)
                    throw new InvalidOperationException(ScaleLocalization.Source("The native shape material is unavailable.", "標準の形状マテリアルを利用できません。"));
                return original;
            }
            Material[] materials = new Material[Math.Max(1, subMeshes)];
            for (int i = 0; i < materials.Length; i++) materials[i] = native;
            return materials;
        }

        private bool PrepareShape(Member member, out Bounds shapeBounds)
        {
            shapeBounds = default(Bounds);
            BlockData data = member.Source;
            if (data.type != BlockData.AAHMDBHDCDK.BoxGen && data.type != BlockData.AAHMDBHDCDK.CapGen) return false;
            GameObject primitive = member.Primitive;
            if (primitive != null)
            {
                bool found = false;
                member.ShapePosition = primitive.transform.position;
                // A sculpted Box has an extra prefab rotation inside the Box root.
                // The height offset follows the root, not that prefab rotation.
                Quaternion actualPrefabRotation = BoxPrefabRotation(data);
                member.ShapeUp = primitive.transform.rotation * Quaternion.Inverse(actualPrefabRotation) * Vector3.up;
                foreach (MeshFilter filter in primitive.GetComponentsInChildren<MeshFilter>(true))
                {
                    MeshRenderer renderer = filter.GetComponent<MeshRenderer>();
                    if (renderer == null || filter.sharedMesh == null) continue;
                    Ghost visual = new Ghost { Mesh = filter.sharedMesh, Position = filter.transform.position,
                        Rotation = filter.transform.rotation, Scale = filter.transform.lossyScale,
                        Materials = renderer.sharedMaterials };
                    member.PreviewMeshes.Add(visual);
                    ConnectedShapeContact.Hull hull = ConnectedShapeContact.Create(visual.Mesh,
                        visual.Position, visual.Rotation, visual.Scale);
                    if (hull != null) member.ContactMeshes.Add(hull);
                    if (!found) { shapeBounds = renderer.bounds; found = true; }
                    else shapeBounds.Encapsulate(renderer.bounds);
                }
                if (found) return true;
            }
            // BUILD normally keeps only the editor block; MakeBox/MakeCapsule create the
            // shape object later. Reproduce their pose from saved data without mutating it.
            GameObject template = ShapeTemplate(member.Controller, data);
            MeshFilter mesh = template == null ? null : template.GetComponent<MeshFilter>();
            if (mesh == null || mesh.sharedMesh == null)
            {
                Log("SHAPE_TEMPLATE_MISSING block=" + member.Index + " type=" + data.type
                    + " chassis=" + (data.type == BlockData.AAHMDBHDCDK.BoxGen ? data.actionID[0] : 0));
                return false;
            }
            Transform block = member.Controller.transform;
            Quaternion q = Quaternion.Euler(0f, -data.actionID[7], 0f)
                * Quaternion.Euler(-data.actionParam[6], 0f, -data.actionParam[7]);
            Quaternion rotation = block.rotation * BDLEJBBJJOI.PJHDCFEPAOP(Matrix4x4.TRS(Vector3.zero, q, Vector3.one));
            float width = SetupPrecisionData.Read(data, 0);
            float height = SetupPrecisionData.Read(data, 1);
            float depth = data.type == BlockData.AAHMDBHDCDK.BoxGen ? SetupPrecisionData.Read(data, 2) : width;
            Vector3 offset = new Vector3(SetupPrecisionData.Read(data, 3), SetupPrecisionData.Read(data, 4), SetupPrecisionData.Read(data, 5));
            member.ShapeUp = rotation * Vector3.up;
            member.ShapePosition = ShapeCenter(block, member.ShapeUp, height, offset);
            float axialHeight = height;
            if (data.type == BlockData.AAHMDBHDCDK.BoxGen) axialHeight = Mathf.Max(height, 0.01f);
            // Native mode 0 includes the caps even when cylinder height is zero:
            // that case is a full sphere, rather than a mesh flattened on Y.
            else axialHeight = CapsuleAxialHeight(data.actionParam[2], height, width);
            float unit = data.gid == 7 ? 0.0998f : 0.1f;
            Vector3 scale = new Vector3(width, axialHeight, depth) * unit;
            // MakeBox keeps the chassis prefab as a child of the sized Box root.
            // Regular Box and chassis 255 receive the size on their own root.
            bool nestedBoxPrefab = data.type == BlockData.AAHMDBHDCDK.BoxGen
                && data.actionID[0] > 0 && data.actionID[0] != 255;
            Quaternion prefabRotation = BoxPrefabRotation(data);
            Ghost templateVisual = new Ghost { Mesh = mesh.sharedMesh, Position = member.ShapePosition,
                Rotation = rotation * prefabRotation,
                Scale = nestedBoxPrefab ? NestedBoxScale(scale, prefabRotation, template.transform.localScale) : scale,
                Materials = NativePreviewMaterials(data, template.GetComponent<MeshRenderer>(), mesh.sharedMesh.subMeshCount),
                PaintTemplate = true, Paint = data.GetEmitColor() };
            member.PreviewMeshes.Add(templateVisual);
            ConnectedShapeContact.Hull templateHull = ConnectedShapeContact.Create(templateVisual.Mesh,
                templateVisual.Position, templateVisual.Rotation, templateVisual.Scale);
            if (templateHull != null) member.ContactMeshes.Add(templateHull);
            shapeBounds = WorldBounds(mesh.sharedMesh.bounds, templateVisual.Position,
                templateVisual.Rotation, templateVisual.Scale);
            return true;
        }

        internal static float CapsuleAxialHeight(int kind, float height, float width)
        { return kind == 0 ? height + width : height; }

        private void PrepareAssembledGeometry(Member member)
        {
            GroupPose group = originalGroups[member.Group];
            member.ContactMeshes.Clear();
            bool found = false;
            foreach (Ghost visual in member.PreviewMeshes)
            {
                Vector3 position = group.WorldPosition + group.WorldRotation * visual.Position;
                Quaternion rotation = group.WorldRotation * visual.Rotation;
                ConnectedShapeContact.Hull hull = ConnectedShapeContact.Create(visual.Mesh,
                    position, rotation, visual.Scale);
                if (hull != null) member.ContactMeshes.Add(hull);
                Bounds bounds = WorldBounds(visual.Mesh.bounds, position, rotation, visual.Scale);
                if (!found) { member.ShapeBounds = bounds; found = true; }
                else member.ShapeBounds.Encapsulate(bounds);
            }
            if (!found) throw new InvalidOperationException(ScaleLocalization.Source("No assembled geometry for block ", "組み立てた形状のないブロック: ") + member.Index);
        }

        private static GameObject ShapeTemplate(BlockController controller, BlockData data)
        {
            BoxGenController box = controller as BoxGenController;
            if (box != null)
            {
                // MakeBox uses a chassis prefab for sculpted BoxGen (actionID[0] > 0).
                // It is still a Box and its size/offset fields use the same root pose.
                int chassis = data.actionID[0];
                if (chassis > 0) return PAEHEMJNPND.HNKGAMLHJJH(chassis);
                int bucket = ShapeDetailRetention.EffectiveBucket(data);
                return box.EADJPJNLLHP == null || box.EADJPJNLLHP.Length <= bucket ? null : box.EADJPJNLLHP[bucket];
            }
            CapGenController cap = controller as CapGenController;
            if (cap == null) return null;
            int bucketCap = ShapeDetailRetention.EffectiveBucket(data);
            int kind = data.actionParam[2];
            if (kind == 0 && SetupPrecisionData.Read(data, 1) <= 0f) kind = 99;
            kind = ShapeDetailRetention.RetainedCapsuleKind(kind, data);
            GameObject[] prefabs = kind == 3 ? cap.GEPNLNGHOBN : kind == 2 ? cap.IBOPGJLENBL
                : kind == 1 ? cap.JPLJEMBOJBL : kind == 0 ? cap.HAJGDCHIJAK : cap.EADJPJNLLHP;
            return prefabs == null || prefabs.Length <= bucketCap ? null : prefabs[bucketCap];
        }

        private static Quaternion BoxPrefabRotation(BlockData data)
        {
            int chassis = data.type == BlockData.AAHMDBHDCDK.BoxGen ? data.actionID[0] : 0;
            // MakeBox instantiates chassis 255 without the additional rotation.
            return chassis > 0 && chassis != 255 ? PAEHEMJNPND.MFJKHIMACAP(chassis) : Quaternion.identity;
        }

        private static Vector3 NestedBoxScale(Vector3 rootScale, Quaternion childRotation, Vector3 childScale)
        {
            // MakeBox reparents the rotated chassis mesh out of the nonuniformly
            // scaled root. Its final scale follows the transformed basis lengths.
            return new Vector3(
                Vector3.Scale(rootScale, childRotation * Vector3.right).magnitude * childScale.x,
                Vector3.Scale(rootScale, childRotation * Vector3.up).magnitude * childScale.y,
                Vector3.Scale(rootScale, childRotation * Vector3.forward).magnitude * childScale.z);
        }

        private static Bounds WorldBounds(Bounds local, Vector3 position, Quaternion rotation, Vector3 scale)
        {
            Matrix4x4 matrix = Matrix4x4.TRS(position, rotation, scale);
            Vector3 x = matrix.MultiplyVector(new Vector3(local.extents.x, 0f, 0f));
            Vector3 y = matrix.MultiplyVector(new Vector3(0f, local.extents.y, 0f));
            Vector3 z = matrix.MultiplyVector(new Vector3(0f, 0f, local.extents.z));
            Vector3 extents = new Vector3(Mathf.Abs(x.x) + Mathf.Abs(y.x) + Mathf.Abs(z.x),
                Mathf.Abs(x.y) + Mathf.Abs(y.y) + Mathf.Abs(z.y),
                Mathf.Abs(x.z) + Mathf.Abs(y.z) + Mathf.Abs(z.z));
            return new Bounds(matrix.MultiplyPoint3x4(local.center), extents * 2f);
        }

        private bool SessionCurrent()
        {
            HIPBCCKFFAG assembly = Preview.GetValue(build) as HIPBCCKFFAG;
            if (Build.GFJLEEJELOL != selectedBuild || selectedBuild.blockData.Count != selectedBlockCount
                || assembly == null || assembly.HCMMJPFOIHD) return false;
            foreach (Member member in members)
                if (member.Controller == null || member.Index >= selectedBuild.blockData.Count
                    || selectedBuild.blockData[member.Index] != member.Source
                    || member.Controller.JNKEKNOAPHO != member.Source) return false;
            return true;
        }

        private void Replan()
        {
            if (plannedSingleCouplersOnly != singleCouplersOnly)
                Log("COUPLER_POLICY singleOnly=" + singleCouplersOnly);
            plannedText = scaleText;
            plannedAnchor = anchorPivot;
            plannedSingleCouplersOnly = singleCouplersOnly;
            valid = false;
            identityPreview = false;
            edits.Clear();
            scaledCouplerCount = 0;
            fixedBoundaryCouplerCount = 0;
            fixedNonDriverCouplerCount = 0;
            maxError = 0f;
            limitedCouplerCount = 0;
            maxCouplerResidual = 0f;
            if (!SetupPrecision.Enabled) { status = ScaleLocalization.Source("Enable Setup Precision.", "Setup Precision を有効にしてください。"); return; }
            if (!float.TryParse(scaleText.Trim().Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out factor)
                || float.IsNaN(factor) || float.IsInfinity(factor) || factor <= 0f || factor > 100f)
            { status = ScaleLocalization.Source("Factor must be greater than 0 and at most 100.", "倍率は 0 より大きく、100 以下にしてください。"); return; }
            if (factor == 1f)
            {
                identityPreview = true;
                Log("PREVIEW_IDENTITY shapes=" + (boxCount + capCount)
                    + " factor=1 sourceParameters=unchanged apply=allowed membership=explicit-controller-set");
            }
            pivot = anchorPivot ? AnchorPoint(seed) : centerPivot;
            try
            {
                HashSet<BlockController> adjustableCouplers = new HashSet<BlockController>();
                int[] parents;
                bool[] couplerLinks;
                BranchGraph(out parents, out couplerLinks);
                bool[] selectedGroups = new bool[parents.Length];
                bool[] protectedShapes = new bool[parents.Length];
                HashSet<BlockController> selected = new HashSet<BlockController>();
                foreach (Member member in members) { selected.Add(member.Controller); selectedGroups[member.Group] = true; }
                foreach (KeyValuePair<BlockController, int> entry in groupByController)
                {
                    BlockData.AAHMDBHDCDK type = entry.Key.JNKEKNOAPHO.type;
                    if (!selected.Contains(entry.Key) && (type == BlockData.AAHMDBHDCDK.BoxGen || type == BlockData.AAHMDBHDCDK.CapGen))
                    { protectedShapes[entry.Value] = true; selectedGroups[entry.Value] = false; }
                }
                foreach (Member member in members)
                {
                    BlockData data = member.Source;
                    if (identityPreview) continue; // No offset solver or Coupler edit for an identity operation.
                    if (data.type == BlockData.AAHMDBHDCDK.Coupler
                        && (!singleCouplersOnly || IsSingleCoupler(data)))
                    {
                        if (parameterFixedCouplers.Contains(member.Controller))
                        {
                            fixedBoundaryCouplerCount++;
                            Log("COUPLER_BOUNDARY_FIXED block=" + member.Source.GetPosString()
                                + " reason=shared-forward-geometry offsets=unchanged");
                            continue;
                        }
                        int driverGroup = -1;
                        for (int i = 0; i < originalGroups.Length; i++)
                            if (originalGroups[i].Driver == member.Controller) { driverGroup = i; break; }
                        if (driverGroup < 0 || originalGroups[driverGroup].Parent < 0)
                        {
                            fixedNonDriverCouplerCount++;
                            if (fixedNonDriverCouplerCount <= 8)
                                Log("COUPLER_FIXED block=" + member.Source.GetPosString()
                                    + " reason=" + (driverGroup < 0 ? "no-native-driver" : "native-root")
                                    + " logicalSelection=kept offsets=unchanged");
                            continue;
                        }
                        if (driverGroup >= 0 && (protectedShapes[driverGroup]
                            || CouplerBranchSelection.HasUnselectedDescendant(parents, selectedGroups, driverGroup, protectedShapes)))
                        {
                            fixedBoundaryCouplerCount++;
                            Log("COUPLER_BOUNDARY_FIXED block=" + member.Source.GetPosString()
                                + " group=" + driverGroup + " reason=unselected-descendant");
                            continue;
                        }
                        adjustableCouplers.Add(member.Controller);
                    }
                }
                plannedGroups = new GroupPose[originalGroups.Length];
                int[] visiting = new int[plannedGroups.Length];
                if (identityPreview) Array.Copy(originalGroups, plannedGroups, originalGroups.Length);
                else for (int i = 0; i < plannedGroups.Length; i++)
                        ResolvePlannedGroup(i, visiting, adjustableCouplers);
                HashSet<BlockController> selectedControllers = new HashSet<BlockController>();
                foreach (Member member in members) selectedControllers.Add(member.Controller);
                foreach (KeyValuePair<BlockController, int> entry in groupByController)
                {
                    BlockData.AAHMDBHDCDK type = entry.Key.JNKEKNOAPHO.type;
                    if (selectedControllers.Contains(entry.Key)
                        || (type != BlockData.AAHMDBHDCDK.BoxGen && type != BlockData.AAHMDBHDCDK.CapGen)) continue;
                    if (Vector3.Distance(originalGroups[entry.Value].WorldPosition,
                        plannedGroups[entry.Value].WorldPosition) > Tolerance)
                        throw new InvalidOperationException(ScaleLocalization.Source("A Coupler would move a shape outside the selected group: ", "Coupler により未選択のグループの形状が移動します: ")
                            + entry.Key.JNKEKNOAPHO.GetPosString());
                }
                foreach (Member member in members)
                    if (member.HasShape) edits.Add(identityPreview ? PlanIdentityShape(member) : PlanShape(member));
                if (fixedNonDriverCouplerCount > 0)
                    Log("COUPLER_NON_DRIVER count=" + fixedNonDriverCouplerCount + " explicitFixed=true");
                // Run the real precision writer on disposable copies before creating Undo.
                foreach (Edit edit in edits)
                {
                    BlockData probe = edit.Member.Source.Clone();
                    ShapeDetailRetention.RetainForScaleShrink(probe, edit.Member.Source, factor);
                    for (int i = 0; i < edit.Slots.Length; i++)
                    {
                        if (!SetupPrecisionData.Supports(probe, edit.Slots[i]))
                            throw new InvalidOperationException(ScaleLocalization.Source("Precision parameters are unavailable for block ", "精密パラメーターを利用できないブロック: ") + edit.Member.Index);
                        SetupPrecisionData.Set(probe, edit.Slots[i], edit.Values[i]);
                    }
                }
                valid = true;
                status = identityPreview ? ScaleLocalization.Source("Ready: factor 1 keeps the shape and parameters unchanged.", "準備完了。倍率 1 では形状とパラメーターを変更しません。")
                    : ScaleLocalization.Source("Ready; maximum shape error: ", "準備完了。形状の最大誤差: ") + maxError.ToString("0.######", CultureInfo.InvariantCulture)
                    + (limitedCouplerCount > 0 ? ScaleLocalization.Source("; Couplers at limit: ", "。上限に達した Coupler: ") + limitedCouplerCount : "");
                Log("PLAN_READY factor=" + factor.ToString("0.###", CultureInfo.InvariantCulture)
                    + " neutral=" + identityPreview
                    + " scaledCoupler=" + scaledCouplerCount + " shapes=" + (boxCount + capCount)
                    + " limitedCoupler=" + limitedCouplerCount
                    + " maxCouplerResidual=" + maxCouplerResidual.ToString("0.######", CultureInfo.InvariantCulture)
                    + " maxError=" + maxError.ToString("0.######", CultureInfo.InvariantCulture));
            }
            catch (Exception error)
            {
                edits.Clear();
                plannedGroups = null;
                scaledCouplerCount = 0;
                status = error.Message;
                Log("PLAN_REJECTED " + error);
            }
        }

        private static bool IsSingleCoupler(BlockData data)
        {
            // Build's native "Single" toggle writes -1 to Coupler actionParam[7].
            return data != null && data.type == BlockData.AAHMDBHDCDK.Coupler
                && data.actionParam != null && data.actionParam.Length > 7 && data.actionParam[7] < 0;
        }

        private void ResolvePlannedGroup(int index, int[] visiting, HashSet<BlockController> adjustable)
        {
            if (visiting[index] == 2) return;
            if (visiting[index] == 1) throw new InvalidOperationException(ScaleLocalization.Source("Cycle in the Coupler group plan.", "Coupler のグループ計画に循環があります。"));
            visiting[index] = 1;
            GroupPose old = originalGroups[index];
            GroupPose next = new GroupPose { Parent = old.Parent, Driver = old.Driver,
                CouplerBase = old.CouplerBase,
                CouplerBaseController = old.CouplerBaseController,
                LocalPosition = old.LocalPosition, LocalRotation = old.LocalRotation };
            if (old.Parent < 0)
            {
                next.WorldPosition = Vector3.zero;
                next.WorldRotation = Quaternion.identity;
            }
            else
            {
                ResolvePlannedGroup(old.Parent, visiting, adjustable);
                GroupPose parent = plannedGroups[old.Parent];
                Member driver = null;
                bool limited = false;
                Vector3 value = Vector3.zero, nextOffset = Vector3.zero;
                if (old.Driver != null && adjustable.Contains(old.Driver))
                    foreach (Member member in members)
                        if (member.Controller == old.Driver) { driver = member; break; }
                if (driver != null)
                {
                    Vector3 raw = new Vector3(SetupPrecisionData.Read(driver.Source, 0),
                        SetupPrecisionData.Read(driver.Source, 1), SetupPrecisionData.Read(driver.Source, 2));
                    Vector3 fixedPart = old.LocalPosition - raw * 0.01f;
                    // WorldPosition is the assembly offset, not the Coupler's
                    // attachment point. Its fixed grid position also contributes
                    // to the point being scaled around the chosen pivot.
                    Vector3 gridPosition = driver.Controller.transform.position;
                    Vector3 oldAnchor = old.WorldPosition + old.WorldRotation * gridPosition;
                    Vector3 targetAnchor = pivot + (oldAnchor - pivot) * factor;
                    Quaternion nextRotation = parent.WorldRotation * old.LocalRotation;
                    Vector3 targetOrigin = targetAnchor - nextRotation * gridPosition;
                    Vector3 neededLocal = Quaternion.Inverse(parent.WorldRotation)
                        * (targetOrigin - parent.WorldPosition);
                    value = (neededLocal - fixedPart) * 100f;
                    if (float.IsNaN(value.x) || float.IsNaN(value.y) || float.IsNaN(value.z)
                        || float.IsInfinity(value.x) || float.IsInfinity(value.y) || float.IsInfinity(value.z))
                        throw new InvalidOperationException(ScaleLocalization.Source("Non-finite offset for Coupler ", "有限値ではないオフセットの Coupler: ") + driver.Index);
                    nextOffset = SolveCouplerOffset(index, old, parent, fixedPart, value);
                    limited = Vector3.Distance(value, nextOffset) > 0.001f;
                    next.LocalPosition = fixedPart + nextOffset * 0.01f;
                    BlockData probe = driver.Source.Clone();
                    SetupPrecisionData.Set(probe, 0, nextOffset.x);
                    SetupPrecisionData.Set(probe, 1, nextOffset.y);
                    SetupPrecisionData.Set(probe, 2, nextOffset.z);
                    Vector3 nativeRotation = Vector3.zero, nativePosition = Vector3.zero, nativeOffset = Vector3.zero;
                    probe.GetCouplerOffset(ref nativeRotation, ref nativePosition, ref nativeOffset, old.CouplerBase);
                    if (Vector3.Distance(nativePosition, next.LocalPosition) > Tolerance)
                        throw new InvalidOperationException(ScaleLocalization.Source("Native pose of Coupler ", "標準の姿勢が不正な Coupler: ") + driver.Index + ScaleLocalization.Source(" does not match the plan.", " は計画と一致しません。"));
                    edits.Add(new Edit { Member = driver, Slots = new int[] { 0, 1, 2 },
                        Values = new float[] { nextOffset.x, nextOffset.y, nextOffset.z } });
                    scaledCouplerCount++;
                }
                next.WorldPosition = parent.WorldPosition + parent.WorldRotation * next.LocalPosition;
                next.WorldRotation = parent.WorldRotation * next.LocalRotation;
                if (driver != null)
                {
                    Vector3 oldAnchor = old.WorldPosition
                        + old.WorldRotation * driver.Controller.transform.position;
                    Vector3 targetAnchor = pivot + (oldAnchor - pivot) * factor;
                    Vector3 actualAnchor = next.WorldPosition
                        + next.WorldRotation * driver.Controller.transform.position;
                    float error = Vector3.Distance(actualAnchor, targetAnchor);
                    if (limited)
                    {
                        limitedCouplerCount++;
                        maxCouplerResidual = Mathf.Max(maxCouplerResidual, error);
                        Log("COUPLER_LIMIT block=" + driver.Index + " group=" + index
                            + " wanted=" + value.ToString("F3") + " applied=" + nextOffset.ToString("F3")
                            + " anchorResidual=" + error.ToString("0.######", CultureInfo.InvariantCulture));
                    }
                    if (error > Tolerance)
                        throw new InvalidOperationException(ScaleLocalization.Source("Coupler point ", "Coupler の点: ") + driver.Index
                            + ScaleLocalization.Source(" exceeds the offset range; error ", " はオフセット範囲外です。誤差: ")
                            + error.ToString("0.###", CultureInfo.InvariantCulture) + ".");
                }
            }
            plannedGroups[index] = next;
            visiting[index] = 2;
        }

        private static void AddOffsetBound(List<OffsetBound> bounds, Vector3 normal, float limit)
        {
            bounds.Add(new OffsetBound { Normal = normal, Limit = limit });
        }

        private static void AddShapeOffsetBounds(List<OffsetBound> bounds, Vector3 normal, float wanted, float maximum)
        {
            // Leave room for the 0.001 cm precision writer and float operations.
            float safeLimit = maximum - 0.005f;
            AddOffsetBound(bounds, -normal, safeLimit - wanted);
            AddOffsetBound(bounds, normal, safeLimit + wanted);
        }

        private static float WorstOffsetViolation(List<OffsetBound> bounds, Vector3 delta)
        {
            float worst = 0f;
            foreach (OffsetBound bound in bounds)
                worst = Mathf.Max(worst, Vector3.Dot(bound.Normal, delta) - bound.Limit);
            return worst;
        }

        private Vector3 SolveCouplerOffset(int index, GroupPose old, GroupPose parent,
            Vector3 fixedPart, Vector3 desired)
        {
            // Work in centimetres relative to the exact similarity pose. A Coupler
            // translation changes every shape offset in this group by a rotated,
            // linear amount; clamping its axes independently can strand a shape
            // beyond the Box/Capsule +/-1000 cm parameter limit.
            List<OffsetBound> bounds = new List<OffsetBound>();
            const float maximum = SetupPrecisionData.CouplerOffsetMaximum;
            AddOffsetBound(bounds, Vector3.right, maximum - desired.x);
            AddOffsetBound(bounds, -Vector3.right, maximum + desired.x);
            AddOffsetBound(bounds, Vector3.up, maximum - desired.y);
            AddOffsetBound(bounds, -Vector3.up, maximum + desired.y);
            AddOffsetBound(bounds, Vector3.forward, maximum - desired.z);
            AddOffsetBound(bounds, -Vector3.forward, maximum + desired.z);
            GroupPose exact = new GroupPose {
                WorldPosition = parent.WorldPosition + parent.WorldRotation * (fixedPart + desired * 0.01f),
                WorldRotation = parent.WorldRotation * old.LocalRotation
            };
            int shapes = 0;
            foreach (Member member in members)
            {
                if (member.Group != index || !member.HasShape) continue;
                float height = Quantized(CheckedSize(member.Source, 1,
                    SetupPrecisionData.Read(member.Source, 1) * factor));
                Vector3 wanted = WantedShapeOffset(member, exact, height);
                Transform block = member.Controller.transform;
                float maximumOffset = SetupPrecisionData.SlotMaximum(member.Source, 3);
                AddShapeOffsetBounds(bounds, old.LocalRotation * block.right, wanted.x, maximumOffset);
                AddShapeOffsetBounds(bounds, old.LocalRotation * block.up, wanted.y, maximumOffset);
                AddShapeOffsetBounds(bounds, old.LocalRotation * block.forward, wanted.z, maximumOffset);
                shapes++;
            }
            Vector3 delta = new Vector3(Mathf.Clamp(desired.x, -maximum, maximum) - desired.x,
                Mathf.Clamp(desired.y, -maximum, maximum) - desired.y,
                Mathf.Clamp(desired.z, -maximum, maximum) - desired.z);
            Vector3[] corrections = new Vector3[bounds.Count];
            float violation = WorstOffsetViolation(bounds, delta);
            for (int pass = 0; violation > 0.0001f && pass < 300; pass++)
            {
                for (int i = 0; i < bounds.Count; i++)
                {
                    OffsetBound bound = bounds[i];
                    Vector3 trial = delta + corrections[i];
                    float excess = Vector3.Dot(bound.Normal, trial) - bound.Limit;
                    Vector3 projected = excess > 0f
                        ? trial - bound.Normal * (excess / bound.Normal.sqrMagnitude) : trial;
                    corrections[i] = trial - projected;
                    delta = projected;
                }
                violation = WorstOffsetViolation(bounds, delta);
            }
            Vector3 result = new Vector3(Quantized(Mathf.Clamp(desired.x + delta.x, -maximum, maximum)),
                Quantized(Mathf.Clamp(desired.y + delta.y, -maximum, maximum)),
                Quantized(Mathf.Clamp(desired.z + delta.z, -maximum, maximum)));
            violation = WorstOffsetViolation(bounds, result - desired);
            if (violation > 0.001f)
                throw new InvalidOperationException(ScaleLocalization.Source("No valid Coupler position for group ", "Coupler の有効な位置がないグループ: ") + index
                    + ScaleLocalization.Source(" (shapes: ", " （形状数: ") + shapes + ScaleLocalization.Source(", excess: ", "、超過量: ")
                    + violation.ToString("0.###", CultureInfo.InvariantCulture) + ").");
            return result;
        }

        private Vector3 WantedShapeOffset(Member member, GroupPose group, float height)
        {
            GroupPose oldGroup = originalGroups[member.Group];
            Vector3 oldWorldCenter = oldGroup.WorldPosition + oldGroup.WorldRotation * member.ShapePosition;
            Vector3 targetWorldCenter = pivot + (oldWorldCenter - pivot) * factor;
            Vector3 targetCenter = Quaternion.Inverse(group.WorldRotation)
                * (targetWorldCenter - group.WorldPosition);
            Transform block = member.Controller.transform;
            Vector3 needed = targetCenter - block.position + block.up * 0.5f
                - member.ShapeUp * (height * 0.05f);
            return new Vector3(Vector3.Dot(needed, block.right),
                Vector3.Dot(needed, block.up), Vector3.Dot(needed, block.forward)) * 100f;
        }

        private static Edit PlanIdentityShape(Member member)
        {
            int[] slots = member.Source.type == BlockData.AAHMDBHDCDK.BoxGen
                ? new int[] { 0, 1, 2, 3, 4, 5 } : new int[] { 0, 1, 3, 4, 5 };
            float[] values = new float[slots.Length];
            for (int i = 0; i < slots.Length; i++) values[i] = SetupPrecisionData.Read(member.Source, slots[i]);
            return new Edit { Member = member, Slots = slots, Values = values };
        }

        private Edit PlanShape(Member member)
        {
            BlockData data = member.Source;
            if (member.ShapeUp == Vector3.zero) throw new InvalidOperationException(ScaleLocalization.Source("No geometry for block ", "形状のないブロック: ") + member.Index);
            Transform block = member.Controller.transform;
            float oldHeight = SetupPrecisionData.Read(data, 1);
            Vector3 oldOffset = new Vector3(SetupPrecisionData.Read(data, 3), SetupPrecisionData.Read(data, 4), SetupPrecisionData.Read(data, 5));
            if (Vector3.Distance(ShapeCenter(block, member.ShapeUp, oldHeight, oldOffset), member.ShapePosition) > Tolerance)
                throw new InvalidOperationException(ScaleLocalization.Source("Position of block ", "位置が不正なブロック: ") + member.Index + ScaleLocalization.Source(" does not match the Box/Capsule formula.", " は Box/Capsule の計算式と一致しません。"));
            GroupPose newGroup = plannedGroups[member.Group];
            GroupPose oldGroup = originalGroups[member.Group];
            Vector3 oldWorldCenter = oldGroup.WorldPosition + oldGroup.WorldRotation * member.ShapePosition;
            Vector3 targetWorldCenter = pivot + (oldWorldCenter - pivot) * factor;
            float height = Quantized(CheckedSize(data, 1, oldHeight * factor));
            Vector3 wantedOffset = WantedShapeOffset(member, newGroup, height);
            float maximum = SetupPrecisionData.SlotMaximum(data, 3);
            if (wantedOffset.x < -maximum || wantedOffset.x > maximum || wantedOffset.y < -maximum
                || wantedOffset.y > maximum || wantedOffset.z < -maximum || wantedOffset.z > maximum)
                throw new InvalidOperationException(ScaleLocalization.Source("Offset of shape ", "オフセットが範囲外の形状: ") + member.Index + ScaleLocalization.Source(" (group ", " （グループ ") + member.Group
                    + ScaleLocalization.Source(") is out of range: ", "）は範囲外です: ") + wantedOffset.ToString("F3"));
            Vector3 offset = new Vector3(
                Quantized(CheckedOffset(data, wantedOffset.x)),
                Quantized(CheckedOffset(data, wantedOffset.y)),
                Quantized(CheckedOffset(data, wantedOffset.z)));
            Vector3 actualCenter = ShapeCenter(block, member.ShapeUp, height, offset);
            float error = Vector3.Distance(newGroup.WorldPosition + newGroup.WorldRotation * actualCenter,
                targetWorldCenter);
            maxError = Mathf.Max(maxError, error);
            if (error > Tolerance) throw new InvalidOperationException(ScaleLocalization.Source("Error for block ", "誤差があるブロック: ") + member.Index + ScaleLocalization.Source(" exceeds 0.002.", " が 0.002 を超えています。"));
            if (data.type == BlockData.AAHMDBHDCDK.BoxGen)
                return new Edit { Member = member, Slots = new int[] { 0, 1, 2, 3, 4, 5 }, Values = new float[] {
                    Quantized(CheckedSize(data, 0, SetupPrecisionData.Read(data, 0) * factor)), height,
                    Quantized(CheckedSize(data, 2, SetupPrecisionData.Read(data, 2) * factor)), offset.x, offset.y, offset.z } };
            return new Edit { Member = member, Slots = new int[] { 0, 1, 3, 4, 5 }, Values = new float[] {
                Quantized(CheckedSize(data, 0, SetupPrecisionData.Read(data, 0) * factor)), height,
                offset.x, offset.y, offset.z } };
        }

        private static Vector3 ShapeCenter(Transform block, Vector3 up, float height, Vector3 offset)
        {
            return block.position + block.right * offset.x * 0.01f + block.up * (offset.y * 0.01f - 0.5f)
                + block.forward * offset.z * 0.01f + up * height * 0.05f;
        }

        private Vector3 AnchorPoint(Member member)
        {
            GroupPose group = originalGroups[member.Group];
            return group.WorldPosition + group.WorldRotation * member.Controller.transform.position;
        }

        private static float CheckedSize(BlockData block, int slot, float value)
        {
            if (float.IsNaN(value) || float.IsInfinity(value) || value < SetupPrecisionData.SizeMinimum(block, slot)
                || value > SetupPrecisionData.ShapeSizeMaximum)
                throw new InvalidOperationException(ScaleLocalization.Source("Size is out of range for block ", "サイズが範囲外のブロック: ") + block.GetPosString());
            return value;
        }

        private static float CheckedOffset(BlockData data, float value)
        {
            float maximum = SetupPrecisionData.SlotMaximum(data, 3);
            if (float.IsNaN(value) || float.IsInfinity(value) || value < -maximum || value > maximum)
                throw new InvalidOperationException(ScaleLocalization.Source("Offset is outside the range -", "オフセットが次の範囲外です: -") + maximum.ToString("0", CultureInfo.InvariantCulture)
                    + ".." + maximum.ToString("0", CultureInfo.InvariantCulture) + ".");
            return value;
        }

        private static float Quantized(float value) { return SetupPrecisionData.Quantize(value) / (float)SetupPrecisionData.Scale; }

        private void ValidatePlannedPose(HDBLLPODNLN graph)
        {
            GroupPose[] before = originalGroups;
            Dictionary<BlockController, int> beforeMap = new Dictionary<BlockController, int>(groupByController);
            HashSet<BlockController> selected = new HashSet<BlockController>();
            Dictionary<BlockController, Edit> byController = new Dictionary<BlockController, Edit>();
            foreach (Member member in members) selected.Add(member.Controller);
            foreach (Edit edit in edits) byController.Add(edit.Member.Controller, edit);
            try
            {
                // Render, inverse offset math and verification all use the same
                // native group predicate. A loose visual probe must never flatten
                // Merge/red/default boundaries or change the driving parent.
                PoseSnapshot native = CapturePose(graph, edits, false);
                PoseSnapshot visual = native;
                PoseSnapshot physical = native;
                float worst = 0f;
                foreach (Member member in members)
                {
                    if (!member.HasShape) continue;
                    Edit edit;
                    int newGroup, oldGroup;
                    if (!byController.TryGetValue(member.Controller, out edit)
                        || !visual.GroupByController.TryGetValue(member.Controller, out newGroup)
                        || !beforeMap.TryGetValue(member.Controller, out oldGroup))
                        throw new InvalidOperationException(ScaleLocalization.Source("The shape is missing from the validation assembly: ", "形状が検証用の組み立てにありません: ") + member.Index);
                    if (visual.Detached.Contains(member.Controller))
                        throw new InvalidOperationException(ScaleLocalization.Source("The visual assembly did not resolve the shape pose: ", "表示用の組み立てで形状の姿勢を計算できませんでした: ") + member.Index);
                    BlockData data = member.Source.Clone();
                    ShapeDetailRetention.RetainForScaleShrink(data, member.Source, factor);
                    for (int i = 0; i < edit.Slots.Length; i++)
                        SetupPrecisionData.Set(data, edit.Slots[i], edit.Values[i]);
                    float height = SetupPrecisionData.Read(data, 1);
                    Vector3 offset = new Vector3(SetupPrecisionData.Read(data, 3),
                        SetupPrecisionData.Read(data, 4), SetupPrecisionData.Read(data, 5));
                    Vector3 localCenter = ShapeCenter(member.Controller.transform, member.ShapeUp, height, offset);
                    GroupPose assembled = visual.Groups[newGroup];
                    Vector3 actual = assembled.WorldPosition + assembled.WorldRotation * localCenter;
                    GroupPose prior = before[oldGroup];
                    Vector3 oldCenter = prior.WorldPosition + prior.WorldRotation * member.ShapePosition;
                    Vector3 expected = PreviewScalePosition(oldCenter, pivot, factor, identityPreview);
                    float error = Vector3.Distance(actual, expected);
                    worst = Mathf.Max(worst, error);
                    float rotationError = ShapeRotationComparison.AngleDegrees(assembled.WorldRotation, prior.WorldRotation);
                    if (error > Tolerance || rotationError > 0.01f)
                    {
                        Log("POSE_SHAPE_MISMATCH block=" + member.Index + " oldGroup=" + oldGroup + " newGroup=" + newGroup
                            + " positionResidual=" + error.ToString("R", CultureInfo.InvariantCulture)
                            + " rotationDegrees=" + rotationError.ToString("R", CultureInfo.InvariantCulture)
                            + " actual=" + actual.ToString("F6") + " expected=" + expected.ToString("F6"));
                        throw new InvalidOperationException(ScaleLocalization.Source("Assembled shape ", "組み立て後の形状: ") + member.Index
                            + ScaleLocalization.Source(" will move away from its preview by ", " はプレビューから移動します。距離: ")
                            + error.ToString("0.######", CultureInfo.InvariantCulture) + ".");
                    }
                }
                int protectedShapes = 0;
                foreach (KeyValuePair<BlockController, int> entry in beforeMap)
                {
                    if (selected.Contains(entry.Key)) continue;
                    BlockData.AAHMDBHDCDK type = entry.Key.JNKEKNOAPHO.type;
                    if (type != BlockData.AAHMDBHDCDK.BoxGen && type != BlockData.AAHMDBHDCDK.CapGen) continue;
                    int newGroup;
                    if (!visual.GroupByController.TryGetValue(entry.Key, out newGroup))
                        throw new InvalidOperationException(ScaleLocalization.Source("A shape outside the group is missing from the validation assembly.", "検証用の組み立てで、グループ外の形状が消えました。"));
                    GroupPose oldPose = before[entry.Value];
                    GroupPose newPose = visual.Groups[newGroup];
                    if (Vector3.Distance(oldPose.WorldPosition, newPose.WorldPosition) > Tolerance
                        || ShapeRotationComparison.AngleDegrees(oldPose.WorldRotation, newPose.WorldRotation) > 0.01f)
                        throw new InvalidOperationException(ScaleLocalization.Source("Scaling would move a shape outside the selected group: ", "拡大縮小すると選択したグループ以外の形状が移動します: ")
                            + entry.Key.JNKEKNOAPHO.GetPosString());
                    protectedShapes++;
                }
                int couplerAnchors = 0;
                foreach (Edit edit in edits)
                {
                    Member member = edit.Member;
                    if (member.Source.type != BlockData.AAHMDBHDCDK.Coupler) continue;
                    int oldGroup, newGroup;
                    if (!beforeMap.TryGetValue(member.Controller, out oldGroup)
                        || !visual.GroupByController.TryGetValue(member.Controller, out newGroup)
                        || visual.Detached.Contains(member.Controller))
                        throw new InvalidOperationException(ScaleLocalization.Source("The Coupler point is missing from the visual assembly: ", "Coupler の点が表示用の組み立てにありません: ") + member.Index);
                    GroupPose oldPose = before[oldGroup], nextPose = visual.Groups[newGroup];
                    Vector3 grid = member.Controller.transform.position;
                    Vector3 oldAnchor = oldPose.WorldPosition + oldPose.WorldRotation * grid;
                    Vector3 expectedAnchor = PreviewScalePosition(oldAnchor, pivot, factor, identityPreview);
                    Vector3 actualAnchor = nextPose.WorldPosition + nextPose.WorldRotation * grid;
                    float positionError = Vector3.Distance(actualAnchor, expectedAnchor);
                    float rotationError = ShapeRotationComparison.AngleDegrees(nextPose.WorldRotation, oldPose.WorldRotation);
                    Log("COUPLER_ANCHOR_CHECK block=" + member.Index + " anchorOldGroup=" + oldGroup + " anchorNewGroup=" + newGroup
                        + " oldGroupDriverMatches=" + (oldPose.Driver == member.Controller)
                        + " newGroupDriverMatches=" + (nextPose.Driver == member.Controller)
                        + " positionResidual=" + positionError.ToString("R", CultureInfo.InvariantCulture)
                        + " rotationDegrees=" + rotationError.ToString("R", CultureInfo.InvariantCulture)
                        + " unityFloatAngle=" + Quaternion.Angle(nextPose.WorldRotation, oldPose.WorldRotation).ToString("R", CultureInfo.InvariantCulture)
                        + " actual=" + actualAnchor.ToString("F6") + " expected=" + expectedAnchor.ToString("F6")
                        + " oldQuaternion=" + QuaternionComponents(oldPose.WorldRotation)
                        + " newQuaternion=" + QuaternionComponents(nextPose.WorldRotation));
                    if (positionError > Tolerance || rotationError > 0.01f)
                        throw new InvalidOperationException(ScaleLocalization.Source("The assembled Coupler point would not match the plan: ", "組み立て後の Coupler の点が計画と一致しません: ") + member.Index);
                    couplerAnchors++;
                }
                ValidatePhysicalPose(physical, selected, byController);
                Log("POSE_VERIFIED selectedShapes=" + (boxCount + capCount)
                    + " protectedShapes=" + protectedShapes + " couplerAnchors=" + couplerAnchors
                    + " frames=native-visual-and-physical samePredicate=CheckLinkG maxError="
                    + worst.ToString("0.######", CultureInfo.InvariantCulture));
            }
            finally
            {
                originalGroups = before;
                groupByController.Clear();
                foreach (KeyValuePair<BlockController, int> entry in beforeMap)
                    groupByController.Add(entry.Key, entry.Value);
            }
        }

        private void ValidatePhysicalPose(PoseSnapshot staged, HashSet<BlockController> selected,
            Dictionary<BlockController, Edit> byController)
        {
            if (originalPhysicalPose == null) throw new InvalidOperationException(ScaleLocalization.Source("The original physical pose is unavailable.", "元の物理姿勢を利用できません。"));
            int protectedShapes = 0, checkedSelected = 0;
            float worst = 0f;
            foreach (KeyValuePair<BlockController, int> entry in originalPhysicalPose.GroupByController)
            {
                BlockController controller = entry.Key;
                BlockData.AAHMDBHDCDK type = controller.JNKEKNOAPHO.type;
                if (type != BlockData.AAHMDBHDCDK.BoxGen && type != BlockData.AAHMDBHDCDK.CapGen) continue;
                int nextGroup;
                if (!staged.GroupByController.TryGetValue(controller, out nextGroup))
                    throw new InvalidOperationException(ScaleLocalization.Source("A shape is missing from the physical validation.", "物理検証で形状が消えました。"));
                bool oldDetached = originalPhysicalPose.Detached.Contains(controller);
                bool nextDetached = staged.Detached.Contains(controller);
                if (oldDetached != nextDetached)
                    throw new InvalidOperationException(ScaleLocalization.Source("Scaling would change the physical connectivity of the shape: ", "拡大縮小すると形状の物理的な接続が変わります: ")
                        + controller.JNKEKNOAPHO.GetPosString());
                // Detached editor parts have no native physics pose to promise.
                // Their geometry is checked explicitly by the visual snapshot above.
                if (oldDetached) continue;
                GroupPose oldPose = originalPhysicalPose.Groups[entry.Value], nextPose = staged.Groups[nextGroup];
                if (!selected.Contains(controller))
                {
                    if (Vector3.Distance(oldPose.WorldPosition, nextPose.WorldPosition) > Tolerance
                        || ShapeRotationComparison.AngleDegrees(oldPose.WorldRotation, nextPose.WorldRotation) > 0.01f)
                        throw new InvalidOperationException(ScaleLocalization.Source("Scaling would move a physical shape outside the selection: ", "拡大縮小すると未選択の物理形状が移動します: ")
                            + controller.JNKEKNOAPHO.GetPosString());
                    protectedShapes++;
                    continue;
                }
                Member member = null;
                foreach (Member candidate in members) if (candidate.Controller == controller) { member = candidate; break; }
                Edit edit;
                if (member == null || !member.HasShape || !byController.TryGetValue(controller, out edit))
                    throw new InvalidOperationException(ScaleLocalization.Source("The selected physical shape is missing from the plan.", "選択した物理形状が計画にありません。"));
                BlockData data = member.Source.Clone();
                for (int i = 0; i < edit.Slots.Length; i++) SetupPrecisionData.Set(data, edit.Slots[i], edit.Values[i]);
                Vector3 offset = new Vector3(SetupPrecisionData.Read(data, 3), SetupPrecisionData.Read(data, 4),
                    SetupPrecisionData.Read(data, 5));
                Vector3 local = ShapeCenter(controller.transform, member.ShapeUp, SetupPrecisionData.Read(data, 1), offset);
                Vector3 oldCenter = oldPose.WorldPosition + oldPose.WorldRotation * member.ShapePosition;
                Vector3 actual = nextPose.WorldPosition + nextPose.WorldRotation * local;
                Vector3 expected = PreviewScalePosition(oldCenter, pivot, factor, identityPreview);
                float error = Vector3.Distance(actual, expected);
                worst = Mathf.Max(worst, error);
                float rotationError = ShapeRotationComparison.AngleDegrees(oldPose.WorldRotation, nextPose.WorldRotation);
                if (error > Tolerance || rotationError > 0.01f)
                {
                    Log("PHYSICAL_SHAPE_MISMATCH block=" + member.Index
                        + " positionResidual=" + error.ToString("R", CultureInfo.InvariantCulture)
                        + " rotationDegrees=" + rotationError.ToString("R", CultureInfo.InvariantCulture)
                        + " actual=" + actual.ToString("F6") + " expected=" + expected.ToString("F6"));
                    throw new InvalidOperationException(ScaleLocalization.Source("The visual plan differs from the physical shape: ", "表示の計画が物理形状と一致しません: ") + member.Index
                        + ScaleLocalization.Source(" (error ", " （誤差 ") + error.ToString("0.######", CultureInfo.InvariantCulture) + ").");
                }
                checkedSelected++;
            }
            Log("PHYSICAL_POSE_VERIFIED selectedRepresentable=" + checkedSelected + " protectedShapes=" + protectedShapes
                + " maxError=" + worst.ToString("0.######", CultureInfo.InvariantCulture) + " physicsParameters=unchanged");
        }

        private static string QuaternionComponents(Quaternion value)
        {
            return "(" + value.x.ToString("R", CultureInfo.InvariantCulture) + ","
                + value.y.ToString("R", CultureInfo.InvariantCulture) + ","
                + value.z.ToString("R", CultureInfo.InvariantCulture) + ","
                + value.w.ToString("R", CultureInfo.InvariantCulture) + ")";
        }

        private void Apply()
        {
            Replan();
            if (!valid || !SessionCurrent()) return;
            try
            {
                HDBLLPODNLN selectionGraph = Graph.GetValue(build) as HDBLLPODNLN;
                if (selectionGraph == null) throw new InvalidOperationException(ScaleLocalization.Source("The group is no longer available.", "グループを利用できなくなりました。"));
                ValidateManualSelection(selectionGraph);
                ValidatePlannedPose(selectionGraph);
                SaveUndo.Invoke(build, new object[] { false, true });
                BuildData current = Build.GFJLEEJELOL;
                foreach (Edit edit in edits)
                {
                    Member member = edit.Member;
                    if (member.Index >= current.blockData.Count) throw new InvalidOperationException(ScaleLocalization.Source("The machine changed during Undo.", "Undo 中にマシンが変更されました。"));
                    BlockData target = current.blockData[member.Index];
                    if (target.type != member.Source.type || target.x != member.Source.x
                        || target.y != member.Source.y || target.z != member.Source.z)
                        throw new InvalidOperationException(ScaleLocalization.Source("The machine changed during Undo.", "Undo 中にマシンが変更されました。"));
                }
                foreach (Edit edit in edits)
                {
                    Member member = edit.Member;
                    BlockData target = current.blockData[member.Index];
                    if (!identityPreview)
                    {
                        ShapeDetailRetention.RetainForScaleShrink(target, member.Source, factor);
                        for (int i = 0; i < edit.Slots.Length; i++) SetupPrecisionData.Set(target, edit.Slots[i], edit.Values[i]);
                    }
                    member.Controller.JNKEKNOAPHO = target;
                }
                if (!identityPreview) foreach (Edit edit in edits)
                {
                    BoxGenController box = edit.Member.Controller as BoxGenController;
                    CapGenController cap = edit.Member.Controller as CapGenController;
                    if (box != null && edit.Member.Primitive != null) box.AdjustBox(true);
                    else if (cap != null && edit.Member.Primitive != null) cap.MakeCapsule();
                }
                // Build's selection graph has no live Coupler pose arrays. The edited
                // BlockData is authoritative here; the native pose is built later.
                Changed.SetValue(build, true);
                HIPBCCKFFAG assembly = Preview.GetValue(build) as HIPBCCKFFAG;
                if (assembly != null) assembly.BGKPIEPJLON();
                Log("APPLIED factor=" + factor.ToString("0.###", CultureInfo.InvariantCulture)
                    + " neutral=" + identityPreview
                    + " pivot=" + (anchorPivot ? "anchor" : "center") + " group=" + members.Count
                    + " box=" + boxCount + " cap=" + capCount + " coupler=" + couplerCount
                    + " scaledCoupler=" + scaledCouplerCount + " singleOnly=" + singleCouplersOnly
                    + " unsupported=" + unsupportedCount + " maxError=" + maxError.ToString("0.######", CultureInfo.InvariantCulture)
                    + " undo=native");
                if (factor < 1f)
                    foreach (Edit edit in edits)
                    {
                        int retained = ShapeDetailRetention.ReadMinimum(edit.Member.Controller.JNKEKNOAPHO);
                        if (ShapeDetailRetention.HasRetainedDetail(edit.Member.Controller.JNKEKNOAPHO))
                            Log("DETAIL_RETAINED block=" + edit.Member.Index + " type=" + edit.Member.Source.type
                                + " minimum=" + retained + " persistence=block-props preview=original-template");
                    }
                Close();
                SculptScaleUi.ReturnToSculpt(build, "apply");
            }
            catch (Exception error)
            {
                Log("APPLY_REJECTED " + error);
                status = ScaleLocalization.Source("Apply cancelled: ", "適用を中止しました: ") + error.Message;
            }
        }

        private void ValidateManualSelection(HDBLLPODNLN graph)
        {
            if (originalVisualPose == null || graph.GEGOAHPBGKB == null)
                throw new InvalidOperationException(ScaleLocalization.Source("The original block set is no longer available.", "元のブロックの集合を利用できなくなりました。"));
            HashSet<BlockController> actual = new HashSet<BlockController>(graph.GEGOAHPBGKB);
            if (!actual.SetEquals(originalVisualPose.GroupByController.Keys))
                throw new InvalidOperationException(ScaleLocalization.Source("The machine graph has changed.", "マシンのグラフが変更されました。"));
            HashSet<BlockController> explicitSet = new HashSet<BlockController>();
            foreach (Member member in members)
            {
                if (!actual.Contains(member.Controller) || member.Controller.JNKEKNOAPHO != member.Source
                    || member.Index < 0 || member.Index >= selectedBuild.blockData.Count
                    || selectedBuild.blockData[member.Index] != member.Source || !explicitSet.Add(member.Controller))
                    throw new InvalidOperationException(ScaleLocalization.Source("The selected set has changed.", "選択した集合が変更されました。"));
            }
            if (!explicitSet.SetEquals(selectedCarriers))
                throw new InvalidOperationException(ScaleLocalization.Source("The selected block set does not match the plan.", "選択したブロックの集合が計画と一致しません。"));
            foreach (Edit edit in edits)
                if (!explicitSet.Contains(edit.Member.Controller))
                    throw new InvalidOperationException(ScaleLocalization.Source("The plan contains a block outside the selected set.", "計画に未選択のブロックが含まれています。"));
            Log("MANUAL_SELECTION_VERIFIED blocks=" + explicitSet.Count + " edits=" + edits.Count
                + " membership=explicit-union removedDescendants=not-regrown");
        }

        private void RefreshGhosts()
        {
            bool active = open && (valid || identityPreview) && showPreview;
            foreach (Member member in members)
                foreach (Ghost ghost in member.PreviewMeshes)
                {
                    if (active && ghost.Object == null)
                    {
                        GameObject overlay = new GameObject("MPatcher.ShapeScalePreview");
                        overlay.layer = 2; // Explicit mesh picking handles this visual without collider side effects.
                        Mesh renderMesh = ghost.Mesh;
                        if (ghost.PaintTemplate)
                        {
                            renderMesh = UnityEngine.Object.Instantiate(ghost.Mesh);
                            // Native vertex alpha encodes emission; keep GetEmitColor unchanged.
                            Color[] colors = new Color[renderMesh.vertexCount];
                            for (int i = 0; i < colors.Length; i++) colors[i] = ghost.Paint;
                            renderMesh.colors = colors;
                            ghost.RenderMesh = renderMesh;
                        }
                        overlay.AddComponent<MeshFilter>().sharedMesh = renderMesh;
                        overlay.AddComponent<MeshRenderer>().sharedMaterials = ghost.Materials;
                        ghost.Object = overlay;
                        Log("PREVIEW_NATIVE_PAINT block=" + member.Index + " vertexColor=" + ghost.PaintTemplate
                            + " materials=" + ghost.Materials.Length);
                    }
                    if (ghost.Object == null) continue;
                    ghost.Object.SetActive(active);
                    if (!active) continue;
                    GroupPose group = originalGroups[member.Group];
                    PreviewFrame frame = member.Frame;
                    if (frame == null) throw new InvalidOperationException(ScaleLocalization.Source("The selected mesh display pose is unavailable.", "選択したメッシュの表示姿勢を利用できません。"));
                    Vector3 assembled = group.WorldPosition + group.WorldRotation * ghost.Position;
                    Vector3 scaled = PreviewScalePosition(assembled, pivot, factor, identityPreview);
                    ghost.Object.transform.position = frame.UsesClickedAnchor ? scaled + frame.Translation
                        : ProjectPreviewPosition(scaled, frame.ParentPosition, frame.InverseParentRotation);
                    ghost.Object.transform.rotation = frame.UsesClickedAnchor ? group.WorldRotation * ghost.Rotation
                        : ProjectPreviewRotation(group.WorldRotation * ghost.Rotation, frame.InverseParentRotation);
                    ghost.Object.transform.localScale = identityPreview ? ghost.Scale : ghost.Scale * factor;
                }
        }

        internal static Vector3 PreviewScalePosition(Vector3 assembled, Vector3 scalePivot, float coefficient, bool identity)
        { return identity ? assembled : scalePivot + (assembled - scalePivot) * coefficient; }

        private PreviewFrame SnapshotPreviewFrame()
        {
            return new PreviewFrame { ParentGroup = previewParentGroup, ParentPosition = previewParentPosition,
                InverseParentRotation = previewInverseParentRotation, Translation = previewTranslation,
                UsesClickedAnchor = previewUsesClickedAnchor };
        }

        private void RestorePreviewFrame(PreviewFrame frame)
        {
            previewParentGroup = frame.ParentGroup;
            previewParentPosition = frame.ParentPosition;
            previewInverseParentRotation = frame.InverseParentRotation;
            previewTranslation = frame.Translation;
            previewUsesClickedAnchor = frame.UsesClickedAnchor;
        }

        private void ConfigurePreviewFrame(ConnectedShapePoseHierarchy.Result branch, Member clicked)
        {
            int parentGroup = branch.DisplayParent;
            previewParentGroup = parentGroup;
            previewParentPosition = Vector3.zero;
            previewInverseParentRotation = Quaternion.identity;
            previewTranslation = Vector3.zero;
            previewUsesClickedAnchor = false;
            if (parentGroup >= 0)
            {
                if (originalGroups == null || parentGroup >= originalGroups.Length)
                    throw new InvalidOperationException(ScaleLocalization.Source("The preview parent pose is unavailable.", "プレビューの親の姿勢を利用できません。"));
                GroupPose parent = originalGroups[parentGroup];
                previewParentPosition = parent.WorldPosition;
                previewInverseParentRotation = Quaternion.Inverse(parent.WorldRotation);
            }
            if (clicked.HasShape)
            {
                GroupPose shapeGroup = originalGroups[clicked.Group];
                Vector3 assembledAnchor = shapeGroup.WorldPosition + shapeGroup.WorldRotation * clicked.ShapePosition;
                previewTranslation = ClickedPreviewTranslation(clicked.ShapePosition, assembledAnchor);
                previewUsesClickedAnchor = true;
            }
            else if (branch.Route == "exact-driver" && branch.RootGroups.Length == 1
                && clicked.Source.type == BlockData.AAHMDBHDCDK.Coupler
                && SetupPrecisionData.Read(clicked.Source, 0) == 0f
                && SetupPrecisionData.Read(clicked.Source, 1) == 0f
                && SetupPrecisionData.Read(clicked.Source, 2) == 0f)
            {
                // Paired drivers relocate to their remote base even with zero
                // own offset. Use the exact driver frame, not its carrier group.
                GroupPose driverGroup = originalGroups[branch.RootGroups[0]];
                Vector3 buildAnchor = clicked.Controller.transform.position;
                Vector3 assembledAnchor = driverGroup.WorldPosition + driverGroup.WorldRotation * buildAnchor;
                previewTranslation = ClickedPreviewTranslation(buildAnchor, assembledAnchor);
                previewUsesClickedAnchor = true;
            }
            if (previewUsesClickedAnchor)
            {
                Log("PREVIEW_FRAME basis=clicked-detail translation=" + previewTranslation.ToString("F3")
                    + " nativeAngles=kept relativeOffsets=kept editCoordinates=assembled-world");
                return;
            }
            Log("PREVIEW_FRAME basis=clicked-coupler parentGroup=" + previewParentGroup
                + " ancestorTransforms=omitted ownAndDescendantOffsets=kept editCoordinates=assembled-world");
        }

        internal static Vector3 ClickedPreviewTranslation(Vector3 buildAnchor, Vector3 assembledAnchor)
        { return buildAnchor - assembledAnchor; }

        // Apply this rigid frame only to rendered ghost transforms. In particular,
        // bounds/pivot/offset planning and pre-Undo validation stay in assembly space.
        internal static Vector3 ProjectPreviewPosition(Vector3 worldPosition, Vector3 parentPosition,
            Quaternion inverseParentRotation)
        { return inverseParentRotation * (worldPosition - parentPosition); }

        internal static Quaternion ProjectPreviewRotation(Quaternion worldRotation, Quaternion inverseParentRotation)
        { return inverseParentRotation * worldRotation; }

        // A shared empty branch has no shapes inheriting the selected driver.
        // Present that driver's native local transform as the front branch basis,
        // preserving the actual helper/descendant frames inside the rendered view.
        internal static Vector3 ComposeSelectedPreviewPosition(Vector3 parentLocal, Vector3 selectedLocalPosition,
            Quaternion selectedLocalRotation, bool compose)
        { return compose ? selectedLocalPosition + selectedLocalRotation * parentLocal : parentLocal; }

        internal static Quaternion ComposeSelectedPreviewRotation(Quaternion parentLocalRotation,
            Quaternion selectedLocalRotation, bool compose)
        { return compose ? selectedLocalRotation * parentLocalRotation : parentLocalRotation; }

        internal void CloseFromMode() { Close(); }

        private void Close()
        {
            open = false;
            valid = false;
            identityPreview = false;
            boxCount = sculptBoxCount = capCount = couplerCount = singleCouplerCount = unsupportedCount = 0;
            scaledCouplerCount = fixedBoundaryCouplerCount = fixedNonDriverCouplerCount = limitedCouplerCount = 0;
            maxError = maxCouplerResidual = 0f;
            centerPivot = pivot = Vector3.zero;
            selectedBuild = null;
            seed = null;
            members.Clear(); selectedCarriers.Clear(); selectionAnchors.Clear(); parameterFixedCouplers.Clear(); edits.Clear();
            selectionRevision++;
            groupByController.Clear();
            originalGroups = plannedGroups = null;
            originalPhysicalPose = originalVisualPose = null;
            sessionPoseRoot = null;
            sessionAllowedControllers = null;
            previewParentGroup = -1;
            previewParentPosition = Vector3.zero;
            previewInverseParentRotation = Quaternion.identity;
            previewTranslation = Vector3.zero;
            previewUsesClickedAnchor = false;
            foreach (Ghost ghost in ghosts)
                DisposeGhost(ghost);
            ghosts.Clear();
        }

        private static void DisposeGhost(Ghost ghost)
        {
            if (ghost.Object != null) { ghost.Object.SetActive(false); UnityEngine.Object.Destroy(ghost.Object); ghost.Object = null; }
            if (ghost.RenderMesh != null) { UnityEngine.Object.Destroy(ghost.RenderMesh); ghost.RenderMesh = null; }
        }

        private void OnDestroy() { Close(); }

        private static void Log(string message)
        {
            try { ScaleRuntimeHost.Log("[CONNECTED-SHAPE-SCALE] " + message); }
            catch (Exception) { }
        }
    }
}
