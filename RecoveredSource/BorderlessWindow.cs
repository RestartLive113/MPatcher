using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class BorderlessWindow
{
	private struct AuKtf1Dl6AAglLIZ20ZSpclf6uedDCNqcFX0mF3y9Ta_0024
	{
		public int l1yZiFEyWE3QMNuXpPqhlJo;

		public int int_0;

		public int cNyaYHLK5mh9YLsGWG1pS_k;

		public int L79PRza7004OrgBBTX9I3Uk;
	}

	public static bool framed = true;

	private const int int_0 = -16;

	private const int hTminDCRBZRq3M4rO8CWGT8 = 6;

	private const int O4X3SOyfQMxYnSAZf4M4Wzk = 3;

	private const int B5WgP2az_9tcP7b9nNe88SU = 9;

	private const uint j2l_3x1ZimHZTqZ3q4a5ut8 = 268435456u;

	private const uint ifOh2gnG11dmvuJM7eh1FqM = 2147483648u;

	private const uint H_ExWQpfP1jJ6D10c21Ixyc = 8388608u;

	private const uint Ix9swQSoMSVU2IMqTqU9KSE = 0u;

	private const uint Y6Df50_fQKzEjQALMg2Xk9c = 12582912u;

	private const uint uint_0 = 524288u;

	private const uint o29uj_0024eoE1bTs_Tj_PGM874 = 262144u;

	private const uint Jg8DgdyIXZQSnMohN8ymwgQ = 131072u;

	private const uint WLQX28_COKwxeSe5hOlOSt8 = 65536u;

	private const uint O6nj7BIy4_0024QdOrAv8Xa2mXgz6CgLFRwEyfWL_Uibhp7Q = 13565952u;

	[DllImport("user32.dll")]
	private static extern IntPtr GetActiveWindow();

	[DllImport("user32.dll")]
	private static extern int SetWindowLong(IntPtr intptr_0, int int_1, uint uint_1);

	[DllImport("user32.dll")]
	private static extern uint GetWindowLong(IntPtr intptr_0, int int_1);

	[DllImport("user32.dll")]
	private static extern bool ShowWindow(IntPtr intptr_0, int int_1);

	[DllImport("user32.dll")]
	private static extern bool MoveWindow(IntPtr intptr_0, int int_1, int int_2, int int_3, int int_4, bool bool_0);

	[DllImport("user32.dll")]
	private static extern bool GetWindowRect(IntPtr intptr_0, out AuKtf1Dl6AAglLIZ20ZSpclf6uedDCNqcFX0mF3y9Ta_0024 auKtf1Dl6AAglLIZ20ZSpclf6uedDCNqcFX0mF3y9Ta_0024_0);

	public static void InitializeOnLoad()
	{
	}

	public static void SetFramelessWindow()
	{
		SetWindowLong(GetActiveWindow(), -16, 2415919104u);
		framed = false;
	}

	public static uint getFramedWindow()
	{
		return GetWindowLong(GetActiveWindow(), -16);
	}

	public static void SetFramedWindow()
	{
		SetWindowLong(GetActiveWindow(), -16, 282001408u);
		framed = true;
	}

	public static void MinimizeWindow()
	{
		ShowWindow(GetActiveWindow(), 6);
	}

	public static void MaximizeWindow()
	{
		ShowWindow(GetActiveWindow(), 3);
	}

	public static void RestoreWindow()
	{
		ShowWindow(GetActiveWindow(), 9);
	}

	public static void MoveWindowPos(Vector2 posDelta, int newWidth, int newHeight)
	{
		IntPtr activeWindow = GetActiveWindow();
		GetWindowRect(activeWindow, out var auKtf1Dl6AAglLIZ20ZSpclf6uedDCNqcFX0mF3y9Ta_0024_);
		int int_ = auKtf1Dl6AAglLIZ20ZSpclf6uedDCNqcFX0mF3y9Ta_0024_.l1yZiFEyWE3QMNuXpPqhlJo + (int)posDelta.x;
		int int_2 = auKtf1Dl6AAglLIZ20ZSpclf6uedDCNqcFX0mF3y9Ta_0024_.int_0 - (int)posDelta.y;
		MoveWindow(activeWindow, int_, int_2, newWidth, newHeight, bool_0: false);
	}
}
