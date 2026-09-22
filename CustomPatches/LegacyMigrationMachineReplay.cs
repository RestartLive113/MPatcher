using System;
using System.Collections.Generic;
using McnCraft;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
    internal sealed partial class LegacyTransientReconnectController
    {
        // Buffered construction RPCs are initializers, not state updates. Native
        // MakeStructureNB discards EPGELCMKKOC and SyncColliderNB deletes some
        // chunk roots after construction. Replaying either on a retained clone
        // therefore dereferences disposed construction data (and can duplicate
        // colliders). Keep the host's RPC buffer for genuinely new clients.
        private sealed class MigrationMachine
        {
            internal MachineController Machine;
            internal MachineSerializer Serializer;
            internal NetworkView View;
            internal NetworkViewID ViewId;
            internal List<NMLMDCCDFPN> Groups;
            internal NMLMDCCDFPN[] GroupItems;
            internal HashSet<int> Renderers, Colliders;
            internal int StructurePackets, ColliderPackets;
        }
        private readonly List<MigrationMachine> migrationMachines = new List<MigrationMachine>();
        private bool migrationMachineReplayActive;
        private string machineReplayEpoch;

        private void BeginRetainedMachineReplay()
        {
            // A normal connection loss retains the same completed remote clones
            // as a planned port move. Capture before any new connection replays RPCs.
            if (!MigrationArmed) CaptureMigrationMachines();
            migrationMachineReplayActive = true;
        }

        private void CaptureMigrationMachines()
        {
            migrationMachines.Clear();
            migrationMachineReplayActive = false;
            machineReplayEpoch = migrationEpoch ?? Guid.NewGuid().ToString("N");
            foreach (MachineController machine in UnityEngine.Object.FindObjectsOfType<MachineController>())
            {
                if (machine == null || machine == game.FICMBCLEFDL
                    || machine.GEOICBCHPNG != EntityController.OCFNGMHHLLM.NetClone) continue;
                MachineSerializer serializer = machine.GetComponent<MachineSerializer>();
                NetworkView view = machine.GetComponent<NetworkView>();
                // Only completed clones may bypass construction. An unfinished
                // machine, a replacement instance or a different view builds normally.
                if (serializer == null || view == null || (Network.isClient && view.isMine) || view.viewID == NetworkViewID.unassigned
                    || serializer.MCFJINLEIIK || NativeStructureReadyField == null
                    || !(bool)NativeStructureReadyField.GetValue(serializer)
                    || machine.BPKNDFJCENJ == null || machine.BPKNDFJCENJ.Count == 0) continue;
                MigrationMachine saved = new MigrationMachine {
                    Machine = machine, Serializer = serializer, View = view, ViewId = view.viewID,
                    Groups = machine.BPKNDFJCENJ, GroupItems = machine.BPKNDFJCENJ.ToArray(),
                    Renderers = MigrationComponentIds<Renderer>(machine),
                    Colliders = MigrationComponentIds<Collider>(machine)
                };
                migrationMachines.Add(saved);
                int missingChunks = 0;
                foreach (NMLMDCCDFPN group in saved.Groups) if (group == null || group.NGLBLAGMBLN == null) missingChunks++;
                LegacyHostMigration.Log("REMOTE_REPLAY_CAPTURE epoch=" + machineReplayEpoch + " instance=" + machine.GetInstanceID()
                    + " view=" + ViewLabel(view) + " groups=" + saved.Groups.Count + " renderers=" + saved.Renderers.Count
                    + " colliders=" + saved.Colliders.Count + " buildDataNull=" + (machine.EPGELCMKKOC == null)
                    + " missingChunkRoots=" + missingChunks + " mode=" + (MigrationArmed ? "migration" : "connection-loss"));
            }
        }

        private bool ShouldSuppressMigrationMachineReplay(MachineSerializer serializer, string source)
        {
            if ((!MigrationArmed && !recovering) || !migrationMachineReplayActive || serializer == null
                || (source != "RPC_SyncStructure" && source != "RPC_SyncCollider")) return false;
            foreach (MigrationMachine saved in migrationMachines)
            {
                if (saved.Serializer != serializer || saved.Machine == null || saved.View == null
                    || saved.View.viewID != saved.ViewId || serializer.GetComponent<NetworkView>() != saved.View
                    || serializer.GetComponent<MachineController>() != saved.Machine
                    || !MigrationGroupsUnchanged(saved)) continue;
                if (source == "RPC_SyncStructure") saved.StructurePackets++; else saved.ColliderPackets++;
                LegacyHostMigration.Log("REMOTE_REPLAY_SUPPRESSED epoch=" + machineReplayEpoch + " source=" + source
                    + " instance=" + saved.Machine.GetInstanceID() + " view=" + ViewLabel(saved.View)
                    + " structure=" + saved.StructurePackets + " collider=" + saved.ColliderPackets);
                return true;
            }
            return false;
        }

        private static bool MigrationGroupsUnchanged(MigrationMachine saved)
        {
            if (!object.ReferenceEquals(saved.Machine.BPKNDFJCENJ, saved.Groups)
                || saved.Groups.Count != saved.GroupItems.Length) return false;
            for (int i = 0; i < saved.GroupItems.Length; i++)
                if (!object.ReferenceEquals(saved.GroupItems[i], saved.Groups[i])) return false;
            return true;
        }
        private static HashSet<int> MigrationComponentIds<T>(MachineController machine) where T : Component
        {
            HashSet<int> ids = new HashSet<int>();
            foreach (T component in machine.GetComponentsInChildren<T>(true)) if (component != null) ids.Add(component.GetInstanceID());
            return ids;
        }
        private void ReleaseMigrationMachines(string reason)
        {
            foreach (MigrationMachine saved in migrationMachines)
            {
                bool alive = saved.Machine != null;
                LegacyHostMigration.Log("REMOTE_REPLAY_RELEASE epoch=" + machineReplayEpoch + " reason=" + reason
                    + " instance=" + (alive ? saved.Machine.GetInstanceID() : 0)
                    + " groupsUnchanged=" + (alive && MigrationGroupsUnchanged(saved))
                    + " renderersUnchanged=" + (alive && saved.Renderers.SetEquals(MigrationComponentIds<Renderer>(saved.Machine)))
                    + " collidersUnchanged=" + (alive && saved.Colliders.SetEquals(MigrationComponentIds<Collider>(saved.Machine)))
                    + " structure=" + saved.StructurePackets + " collider=" + saved.ColliderPackets);
            }
            migrationMachines.Clear();
            migrationMachineReplayActive = false;
            machineReplayEpoch = null;
        }
    }
}
