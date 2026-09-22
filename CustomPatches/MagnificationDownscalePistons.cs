using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	internal static class MagnificationDownscalePistons
	{
		internal static void Register(Harmony harmony)
		{
			foreach (string name in new[] { "Start", "Update", "FixedUpdate" })
			{
				MethodInfo target = AccessTools.Method(typeof(PistonController), name, Type.EmptyTypes);
				if (target == null) throw new MissingMethodException("PistonController", name);
				HarmonyMethod postfix = name == "Start"
					? new HarmonyMethod(AccessTools.Method(typeof(MagnificationDownscalePistons), "StartPostfix")) : null;
				harmony.Patch(target, null, postfix,
					new HarmonyMethod(AccessTools.Method(typeof(MagnificationDownscalePistons), name + "Transpiler")), null);
				MagnificationDownscale.Log("HOOK target=PistonController." + name + " units=downscale-only");
			}
		}

		internal static float DownscaleFactor(float scale)
		{
			return scale > 0f && scale < 1f ? scale : 1f;
		}
		private static float Factor(PistonController piston)
		{
			return piston.FPEBEEJGFPI == null ? 1f : DownscaleFactor(piston.FPEBEEJGFPI.DJCDFLDHPHK);
		}
		private static float ToWorldLength(float value, PistonController piston) { return value * Factor(piston); }
		private static float ToCraftLength(float value, PistonController piston) { return value / Factor(piston); }
		private static void StartPostfix(PistonController __instance)
		{
			float factor = Factor(__instance);
			if (factor < 1f)
				MagnificationDownscale.Log("PISTON_INITIALIZED scale=" + factor.ToString("R", System.Globalization.CultureInfo.InvariantCulture)
					+ " armOffset=scaled feedback=craft-units correction=world-units");
		}

		internal static IEnumerable<CodeInstruction> StartTranspiler(IEnumerable<CodeInstruction> instructions)
		{
			List<CodeInstruction> codes = MagnificationDownscale.Copy(instructions);
			int positive = 0, negative = 0;
			for (int i = codes.Count - 1; i >= 0; i--)
			{
				if (MagnificationDownscale.Float(codes[i], 0.49f)) positive++;
				else if (MagnificationDownscale.Float(codes[i], -0.49f)) negative++;
				else continue;
				InsertConversion(codes, i + 1, "ToWorldLength");
			}
			MagnificationDownscale.Require(positive == 1 && negative == 1, "one signed piston arm offset", positive + negative);
			return codes;
		}

		internal static IEnumerable<CodeInstruction> UpdateTranspiler(IEnumerable<CodeInstruction> instructions)
		{
			List<CodeInstruction> codes = MagnificationDownscale.Copy(instructions);
			int found = 0;
			for (int i = codes.Count - 2; i >= 1; i--)
			{
				if (!MagnificationDownscale.Float(codes[i], 0.98f)) continue;
				if (!Call(codes[i - 1], typeof(Vector3), "Distance") || codes[i + 1].opcode != OpCodes.Add)
					throw new InvalidOperationException("Piston arm length layout changed");
				InsertConversion(codes, i + 1, "ToWorldLength");
				found++;
			}
			MagnificationDownscale.Require(found == 1, "one piston arm length offset", found);
			return codes;
		}

		internal static IEnumerable<CodeInstruction> FixedUpdateTranspiler(IEnumerable<CodeInstruction> instructions)
		{
			List<CodeInstruction> codes = MagnificationDownscale.Copy(instructions);
			int feedback = 0, correction = 0, steppedLimit = 0;
			for (int i = codes.Count - 3; i >= 2; i--)
			{
				if (Call(codes[i], typeof(Vector3), "get_magnitude"))
				{
					// Free-mode feedback compares this world distance with craft-unit
					// 5 - lower/upper limits. Normalize only the measurement.
					InsertConversion(codes, i + 1, "ToCraftLength");
					feedback++;
				}
				else if (Call(codes[i], typeof(SoftJointLimit), "get_limit")
					&& codes[i + 1].opcode == OpCodes.Ldarg_0
					&& MagnificationDownscale.Field(codes[i + 2], OpCodes.Ldfld, typeof(PistonController), "EADPHECOPIL"))
				{
					InsertConversion(codes, i + 1, "ToCraftLength");
					steppedLimit++;
				}
				else if (Call(codes[i], typeof(Vector3), "op_Multiply")
					&& Call(codes[i - 2], typeof(Transform), "get_up"))
				{
					// The clamped craft distance is converted back before MovePosition.
					// Move the original call's branch labels to the conversion entry.
					InsertConversion(codes, i, "ToWorldLength", true);
					correction++;
				}
			}
			MagnificationDownscale.Require(feedback == 1 && correction == 1 && steppedLimit == 1,
				"piston free feedback/correction and stepped limit", feedback + correction + steppedLimit);
			return codes;
		}

		private static void InsertConversion(List<CodeInstruction> codes, int index, string name, bool transferLabels = false)
		{
			CodeInstruction argument = new CodeInstruction(OpCodes.Ldarg_0);
			if (transferLabels)
			{
				argument.labels.AddRange(codes[index].labels);
				codes[index].labels.Clear();
			}
			codes.InsertRange(index, new[] { argument,
				new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(MagnificationDownscalePistons), name)) });
		}
		private static bool Call(CodeInstruction code, Type type, string name)
		{
			MethodInfo method = code.operand as MethodInfo;
			return (code.opcode == OpCodes.Call || code.opcode == OpCodes.Callvirt)
				&& method != null && method.DeclaringType == type && method.Name == name;
		}
	}
}
