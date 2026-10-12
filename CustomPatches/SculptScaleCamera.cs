using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
    // Keep the native movement/collision path; restrict only its UI camera input.
    internal static class SculptScaleCamera
    {
        private const string PatchId = "local.moddev.machinecraft.sculpt-scale-camera.v1";
        private static readonly FieldInfo PointerInside = AccessTools.Field(typeof(CameraController), "JOLDPHCNPCH");
        private static readonly MethodInfo NativeAxis = AccessTools.Method(typeof(Input), "GetAxis", new Type[] { typeof(string) });
        private static bool registered;
        private static CameraController lastCamera;
        private static bool lastKeyboard, lastLook, lastInside;

        internal static void TryRegister()
        {
            if (registered) return;
            Harmony patcher = new Harmony(PatchId);
            try
            {
                MethodInfo lateUpdate = AccessTools.Method(typeof(FreeCameraController), "LateUpdate", Type.EmptyTypes);
                MethodInfo onGui = AccessTools.Method(typeof(CameraController), "OnGUI", Type.EmptyTypes);
                if (PointerInside == null || PointerInside.FieldType != typeof(bool) || NativeAxis == null
                    || lateUpdate == null || onGui == null)
                    throw new MissingMemberException("Native Scale camera members");
                // The recovered QOL camera patch edits native instruction indices.
                // Insert our receiver arguments only after it has changed those constants.
                HarmonyMethod axes = new HarmonyMethod(typeof(SculptScaleCamera), "CameraAxisTranspiler");
                axes.priority = Priority.Last;
                patcher.Patch(lateUpdate, new HarmonyMethod(typeof(SculptScaleCamera), "LateUpdatePrefix"), null, axes);
                patcher.Patch(onGui, new HarmonyMethod(typeof(SculptScaleCamera), "CameraGuiPrefix"));
                registered = true;
                Log("CAMERA_REGISTERED targets=FreeCamera.LateUpdate,Camera.OnGUI movement=native"
                    + " mouse=owned-scale-LMB-look-only cursor=free-unless-look otherCameras=native");
            }
            catch (Exception error)
            {
                try { patcher.UnpatchAll(PatchId); } catch (Exception) { }
                Log("CAMERA_REGISTER_FAILED " + error);
            }
        }

        private static void LateUpdatePrefix(FreeCameraController __instance)
        {
            if (!SculptScaleUi.OwnsCamera(__instance))
            {
                if (lastCamera == __instance) lastCamera = null;
                return;
            }
            bool keyboard = SculptScaleUi.CameraKeyboardAllowed;
            Vector3 pointer = Input.mousePosition;
            bool inside = keyboard && pointer.x > 0f && pointer.y > 0f
                && pointer.x < Screen.width && pointer.y < Screen.height;
            __instance.FMGOKAGJMJH = keyboard;
            __instance.PMMPGDPLCCL = false;
            PointerInside.SetValue(__instance, inside);
            bool look = keyboard && SculptScaleUi.CameraLookHeld;
            if (lastCamera != __instance || lastKeyboard != keyboard || lastLook != look || lastInside != inside)
            {
                lastCamera = __instance;
                lastKeyboard = keyboard;
                lastLook = look;
                lastInside = inside;
                Log("CAMERA_GATE keyboard=" + keyboard + " look=" + look + " pointerInside=" + inside);
            }
        }

        private static bool CameraGuiPrefix(CameraController __instance)
        {
            if (!SculptScaleUi.OwnsCamera(__instance)) return true;
            bool look = SculptScaleUi.CameraKeyboardAllowed && SculptScaleUi.CameraLookHeld;
            __instance.PMMPGDPLCCL = false;
            Cursor.lockState = look ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !look;
            return false;
        }

        private static IEnumerable<CodeInstruction> CameraAxisTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            MethodInfo replacement = AccessTools.Method(typeof(SculptScaleCamera), "CameraMouseAxis",
                new Type[] { typeof(string), typeof(FreeCameraController) });
            if (replacement == null) throw new MissingMethodException("Scale camera mouse axis helper");
            List<CodeInstruction> input = new List<CodeInstruction>();
            foreach (CodeInstruction instruction in instructions) input.Add(new CodeInstruction(instruction));
            List<CodeInstruction> result = new List<CodeInstruction>();
            int replaced = 0;
            for (int i = 0; i < input.Count; i++)
            {
                CodeInstruction instruction = input[i];
                MethodInfo called = instruction.operand as MethodInfo;
                if (called == NativeAxis)
                {
                    string axis = i == 0 ? null : input[i - 1].operand as string;
                    if (i == 0 || input[i - 1].opcode != OpCodes.Ldstr || (axis != "MouseX" && axis != "MouseY"))
                        throw new InvalidOperationException("Native free camera mouse axes changed");
                    // Preserve branch/exception boundaries on the inserted camera argument.
                    CodeInstruction cameraArgument = new CodeInstruction(OpCodes.Ldarg_0);
                    cameraArgument.labels.AddRange(instruction.labels);
                    cameraArgument.blocks.AddRange(instruction.blocks);
                    instruction.labels.Clear();
                    instruction.blocks.Clear();
                    result.Add(cameraArgument);
                    instruction.opcode = OpCodes.Call;
                    instruction.operand = replacement;
                    replaced++;
                }
                result.Add(instruction);
            }
            if (replaced != 4) throw new InvalidOperationException("Native free camera mouse axis reads changed: " + replaced);
            Log("CAMERA_AXIS_GUARD replaced=" + replaced + " axes=MouseX,MouseY scope=owned-Scale-camera");
            return result;
        }

        private static float CameraMouseAxis(string axis, FreeCameraController camera)
        {
            if ((axis == "MouseX" || axis == "MouseY") && SculptScaleUi.OwnsCamera(camera)
                && (!SculptScaleUi.CameraKeyboardAllowed || !SculptScaleUi.CameraLookHeld)) return 0f;
            return Input.GetAxis(axis);
        }

        private static void Log(string message)
        {
            try { global::mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng("[SCULPT-SCALE-UI] " + message); }
            catch (Exception) { }
        }
    }
}
