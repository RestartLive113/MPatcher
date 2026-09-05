using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using ExitGames.Client.Photon;
using MPatchrMain;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

internal class Class30 : MonoBehaviour
{
	internal enum Hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU
	{
		disconnected,
		viewingRooms,
		hostingRoom,
		connectedToRoom,
		connectingToRoom,
		startingToHost,
		connectingToPhoton,
		verifyingPWD
	}

	[Serializable]
	[CompilerGenerated]
	private sealed class eXmdbxoweAxhAyVJD_NghlOeg3mZ9hE3zOHuzYHu53prz2w1SCi1gvsW4FagbdDG9SsqaO_G4j5JonO_GuUL77U
	{
		public static readonly eXmdbxoweAxhAyVJD_NghlOeg3mZ9hE3zOHuzYHu53prz2w1SCi1gvsW4FagbdDG9SsqaO_G4j5JonO_GuUL77U _003C_003E9 = new eXmdbxoweAxhAyVJD_NghlOeg3mZ9hE3zOHuzYHu53prz2w1SCi1gvsW4FagbdDG9SsqaO_G4j5JonO_GuUL77U();

		public static Predicate<Transform> _003C_003E9__45_0;

		public static Predicate<Transform> _003C_003E9__45_1;

		internal bool HY9WWeGDmNcIe_j_16UXb8pwhWiyKTSsNdqdDotSYfrt(Transform item)
		{
			return smethod_0((UnityEngine.Object)item, (UnityEngine.Object)null);
		}

		internal bool HsZqKnSdKPOwQJ2Tf80Is9vrCewaDmPm9QwG3gLZqkVk(Transform item)
		{
			return smethod_0((UnityEngine.Object)item, (UnityEngine.Object)null);
		}

		internal static bool smethod_0(UnityEngine.Object object_0, UnityEngine.Object object_1)
		{
			return object_0 == object_1;
		}
	}

	internal static Class30 xcBvxcM_0024ckBeZyvdSoAkJoM;

	private readonly int int_0 = 250;

	private readonly int SfUlRb81w0TzI2Cx05i_Ky0 = 4;

	private readonly float _M_tgdNQBt7jKh3UmYRBo5Y = 0.1f;

	private readonly string PrthA5W3yQtx6doFwW_0024OZILfZZZw6xqyA0BuxYU3EaqL = global::_003CModule_003E.smethod_29<string>(3985877739u);

	private readonly Color[] gglchrqroE9SNUU7Fu65V5Y = new Color[6]
	{
		Color.red,
		Color.green,
		Color.blue,
		Color.magenta,
		Color.cyan,
		Color.yellow
	};

	private readonly string[] string_0 = new string[6]
	{
		global::_003CModule_003E.smethod_25<string>(2982091682u),
		global::_003CModule_003E.smethod_26<string>(2750535589u),
		global::_003CModule_003E.smethod_25<string>(3550941285u),
		global::_003CModule_003E.smethod_25<string>(1885707795u),
		global::_003CModule_003E.smethod_29<string>(2311687379u),
		global::_003CModule_003E.smethod_29<string>(537437785u)
	};

	private bool yKOE_0024Rr0al95lOeas6MNnnY;

	internal int pLE8WTgZE0t0Om1V6BWOGpQ;

	private float M1SzAg9yMno83lUpmF7Dx_00244 = 2f;

	private string Xzao_Hv76x9BNPYdMQB_g0E;

	private string jzex5TvAqs_0024v8PYs2a4Ebfs;

	private Vector3 Sl3qb7_zclIQRMX7sWCu5EE;

	private Vector3 vector3_0;

	private Quaternion B3qVW_bnOJvAqOC4Ed_0024_0024OYA;

	private float a1LHFL2YbSc0jehiQB0D4c4;

	internal List<OPLNFKECCLE> list_0 = new List<OPLNFKECCLE>();

	private List<BlockData> nm38U0evfhUbRbnE17ex1vE = new List<BlockData>();

	internal List<Transform> list_1 = new List<Transform>();

	private List<Transform> sgF4gmvJXFpS6IC8o9iQ7AE = new List<Transform>();

	internal Hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU_0;

	private string YqLeG_00246_0024ROp_00242YS9Z3_5De8 = global::_003CModule_003E.smethod_29<string>(3945280284u);

	private string GRs0NKS9rPFS44RIL__wdg4 = "";

	internal BkEYtCXOGBC5a3sGZc1baFBEZsix9sHaNV7UHBoeB8aKxFK0bpuW1JKVsL3eaRtFkQ bkEYtCXOGBC5a3sGZc1baFBEZsix9sHaNV7UHBoeB8aKxFK0bpuW1JKVsL3eaRtFkQ_0;

	private List<lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw> Qr2FKmPcn_0024bxep1Rg4yhWNs = new List<lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw>();

	public void Start()
	{
		xcBvxcM_0024ckBeZyvdSoAkJoM = this;
		smethod_1((UnityEngine.Object)smethod_0((Component)this));
	}

	public void Update()
	{
		Xzao_Hv76x9BNPYdMQB_g0E = smethod_2().name;
		if (Xzao_Hv76x9BNPYdMQB_g0E != global::_003CModule_003E.smethod_27<string>(3514760917u) && Xzao_Hv76x9BNPYdMQB_g0E != global::_003CModule_003E.smethod_28<string>(1333418371u) && Xzao_Hv76x9BNPYdMQB_g0E != global::_003CModule_003E.smethod_28<string>(3076781748u))
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		if (aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.QEcOzq1h48SV8MxfAnba82U != null && aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.QEcOzq1h48SV8MxfAnba82U.FLSdXom6uNTfN55f5nxTsH8 != (hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU_0 == Hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU.viewingRooms && aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.iOXrQ3ou11UqRX_0024NnkXQnmI.pZEKY5TzLd4S3z2lXESoRnw != string.Empty))
		{
			aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.QEcOzq1h48SV8MxfAnba82U.FLSdXom6uNTfN55f5nxTsH8 = hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU_0 == Hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU.viewingRooms && aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.iOXrQ3ou11UqRX_0024NnkXQnmI.pZEKY5TzLd4S3z2lXESoRnw != string.Empty;
		}
		if (!fYYx8vN1tah7ZgfygXMH9eJU0YzjaUsqtv9tADffziMcu1k1It8P_0024ZciTdZuxN09bA.PafCCA77BFEJYaAdgSJv7Ys && hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU_0 == Hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU.disconnected)
		{
			aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.GjjztfvuViq0_Q_00249_BUsoYY.FLSdXom6uNTfN55f5nxTsH8 = true;
			aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.GjjztfvuViq0_Q_00249_BUsoYY.pZEKY5TzLd4S3z2lXESoRnw = global::_003CModule_003E.smethod_27<string>(4208795040u);
		}
		else if (fYYx8vN1tah7ZgfygXMH9eJU0YzjaUsqtv9tADffziMcu1k1It8P_0024ZciTdZuxN09bA.PafCCA77BFEJYaAdgSJv7Ys && aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.GjjztfvuViq0_Q_00249_BUsoYY != null)
		{
			switch (hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU_0)
			{
			default:
				aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.GjjztfvuViq0_Q_00249_BUsoYY.FLSdXom6uNTfN55f5nxTsH8 = false;
				break;
			case Hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU.hostingRoom:
			case Hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU.connectedToRoom:
				aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.GjjztfvuViq0_Q_00249_BUsoYY.FLSdXom6uNTfN55f5nxTsH8 = true;
				aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.GjjztfvuViq0_Q_00249_BUsoYY.FLSdXom6uNTfN55f5nxTsH8 = true;
				aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.GjjztfvuViq0_Q_00249_BUsoYY.pZEKY5TzLd4S3z2lXESoRnw = global::_003CModule_003E.smethod_27<string>(1751873949u);
				break;
			case Hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU.viewingRooms:
				aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.GjjztfvuViq0_Q_00249_BUsoYY.FLSdXom6uNTfN55f5nxTsH8 = true;
				aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.GjjztfvuViq0_Q_00249_BUsoYY.FLSdXom6uNTfN55f5nxTsH8 = true;
				aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.GjjztfvuViq0_Q_00249_BUsoYY.pZEKY5TzLd4S3z2lXESoRnw = global::_003CModule_003E.smethod_28<string>(1149371878u);
				break;
			}
		}
		if (aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.Nsgp7ukveOtfQVdR4hYjDAI != null && aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.zmh9Gea8K2d3y7BCuea6ouU.interactable != (!fYYx8vN1tah7ZgfygXMH9eJU0YzjaUsqtv9tADffziMcu1k1It8P_0024ZciTdZuxN09bA.PafCCA77BFEJYaAdgSJv7Ys && hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU_0 == Hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU.disconnected))
		{
			aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.zmh9Gea8K2d3y7BCuea6ouU.interactable = !fYYx8vN1tah7ZgfygXMH9eJU0YzjaUsqtv9tADffziMcu1k1It8P_0024ZciTdZuxN09bA.PafCCA77BFEJYaAdgSJv7Ys && hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU_0 == Hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU.disconnected;
		}
		if (!fYYx8vN1tah7ZgfygXMH9eJU0YzjaUsqtv9tADffziMcu1k1It8P_0024ZciTdZuxN09bA.PafCCA77BFEJYaAdgSJv7Ys)
		{
			return;
		}
		if (hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU_0 != Hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU.viewingRooms)
		{
			while (Qr2FKmPcn_0024bxep1Rg4yhWNs.Count > 0)
			{
				UnityEngine.Object.Destroy(Qr2FKmPcn_0024bxep1Rg4yhWNs[Qr2FKmPcn_0024bxep1Rg4yhWNs.Count - 1].gameObject);
				Qr2FKmPcn_0024bxep1Rg4yhWNs.RemoveAt(Qr2FKmPcn_0024bxep1Rg4yhWNs.Count - 1);
			}
		}
		switch (hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU_0)
		{
		case Hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU.viewingRooms:
		{
			OEHMOENBFPA[] array = fYYx8vN1tah7ZgfygXMH9eJU0YzjaUsqtv9tADffziMcu1k1It8P_0024ZciTdZuxN09bA.W0rfbhNEVb6dg_5P3oF936Q();
			while (Qr2FKmPcn_0024bxep1Rg4yhWNs.Count > array.Length)
			{
				UnityEngine.Object.Destroy(Qr2FKmPcn_0024bxep1Rg4yhWNs[Qr2FKmPcn_0024bxep1Rg4yhWNs.Count - 1].gameObject);
				Qr2FKmPcn_0024bxep1Rg4yhWNs.RemoveAt(Qr2FKmPcn_0024bxep1Rg4yhWNs.Count - 1);
			}
			for (int k = 0; k < array.Length; k++)
			{
				UjCoH5zXWVzYg0MXTOOvepyp0n3CWqd5yFc07HzK9iCsTQH9a5ApFinzqXMIYg_Euw ujCoH5zXWVzYg0MXTOOvepyp0n3CWqd5yFc07HzK9iCsTQH9a5ApFinzqXMIYg_Euw = new UjCoH5zXWVzYg0MXTOOvepyp0n3CWqd5yFc07HzK9iCsTQH9a5ApFinzqXMIYg_Euw(array[k]);
				string text = string.Format(global::_003CModule_003E.smethod_25<string>(1137348937u), ujCoH5zXWVzYg0MXTOOvepyp0n3CWqd5yFc07HzK9iCsTQH9a5ApFinzqXMIYg_Euw.T87T6htsXcJd1O4EqRlT6cc, ujCoH5zXWVzYg0MXTOOvepyp0n3CWqd5yFc07HzK9iCsTQH9a5ApFinzqXMIYg_Euw.Int32_0, ujCoH5zXWVzYg0MXTOOvepyp0n3CWqd5yFc07HzK9iCsTQH9a5ApFinzqXMIYg_Euw.jm9bAoN3Z4KRnFTRx6iFQKk);
				bool flag = false;
				if (ujCoH5zXWVzYg0MXTOOvepyp0n3CWqd5yFc07HzK9iCsTQH9a5ApFinzqXMIYg_Euw.bDL22TQxAdGVLPWswOwgdOWuG2AEuTx3OXpQHzZV6eVL.TryGetValue(global::_003CModule_003E.smethod_26<string>(3773312688u), out var value2))
				{
					if ((int)value2 > SfUlRb81w0TzI2Cx05i_Ky0)
					{
						text = string.Format(global::_003CModule_003E.smethod_25<string>(4030849885u), ujCoH5zXWVzYg0MXTOOvepyp0n3CWqd5yFc07HzK9iCsTQH9a5ApFinzqXMIYg_Euw.T87T6htsXcJd1O4EqRlT6cc, ujCoH5zXWVzYg0MXTOOvepyp0n3CWqd5yFc07HzK9iCsTQH9a5ApFinzqXMIYg_Euw.Int32_0, ujCoH5zXWVzYg0MXTOOvepyp0n3CWqd5yFc07HzK9iCsTQH9a5ApFinzqXMIYg_Euw.jm9bAoN3Z4KRnFTRx6iFQKk);
						flag = true;
					}
					else if ((int)value2 < SfUlRb81w0TzI2Cx05i_Ky0)
					{
						text = string.Format(global::_003CModule_003E.smethod_27<string>(1212084302u), ujCoH5zXWVzYg0MXTOOvepyp0n3CWqd5yFc07HzK9iCsTQH9a5ApFinzqXMIYg_Euw.T87T6htsXcJd1O4EqRlT6cc, ujCoH5zXWVzYg0MXTOOvepyp0n3CWqd5yFc07HzK9iCsTQH9a5ApFinzqXMIYg_Euw.Int32_0, ujCoH5zXWVzYg0MXTOOvepyp0n3CWqd5yFc07HzK9iCsTQH9a5ApFinzqXMIYg_Euw.jm9bAoN3Z4KRnFTRx6iFQKk);
						flag = true;
					}
				}
				else
				{
					text = string.Format(global::_003CModule_003E.smethod_25<string>(1928650363u), ujCoH5zXWVzYg0MXTOOvepyp0n3CWqd5yFc07HzK9iCsTQH9a5ApFinzqXMIYg_Euw.T87T6htsXcJd1O4EqRlT6cc, ujCoH5zXWVzYg0MXTOOvepyp0n3CWqd5yFc07HzK9iCsTQH9a5ApFinzqXMIYg_Euw.Int32_0, ujCoH5zXWVzYg0MXTOOvepyp0n3CWqd5yFc07HzK9iCsTQH9a5ApFinzqXMIYg_Euw.jm9bAoN3Z4KRnFTRx6iFQKk);
					flag = true;
				}
				if (!flag && ujCoH5zXWVzYg0MXTOOvepyp0n3CWqd5yFc07HzK9iCsTQH9a5ApFinzqXMIYg_Euw.bDL22TQxAdGVLPWswOwgdOWuG2AEuTx3OXpQHzZV6eVL.TryGetValue(global::_003CModule_003E.smethod_28<string>(2879509997u), out var value3) && (bool)value3)
				{
					text = string.Format(global::_003CModule_003E.smethod_28<string>(2545574291u), ujCoH5zXWVzYg0MXTOOvepyp0n3CWqd5yFc07HzK9iCsTQH9a5ApFinzqXMIYg_Euw.T87T6htsXcJd1O4EqRlT6cc, ujCoH5zXWVzYg0MXTOOvepyp0n3CWqd5yFc07HzK9iCsTQH9a5ApFinzqXMIYg_Euw.Int32_0, ujCoH5zXWVzYg0MXTOOvepyp0n3CWqd5yFc07HzK9iCsTQH9a5ApFinzqXMIYg_Euw.jm9bAoN3Z4KRnFTRx6iFQKk);
				}
				if (k > Qr2FKmPcn_0024bxep1Rg4yhWNs.Count - 1)
				{
					int bYd2YF16HlL34MrXNeklBTY = k;
					Qr2FKmPcn_0024bxep1Rg4yhWNs.Add(Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.P3tLroX6fcPfQha_0024JdbpYXg(global::_003CModule_003E.smethod_27<string>(2896562371u) + k, new Vector3(50f, int_0 - 50 * k, 0f), text, delegate
					{
						mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(bYd2YF16HlL34MrXNeklBTY, bool_0: true);
						TRpztJkOOsxNWmBuVtxmhAE(bYd2YF16HlL34MrXNeklBTY);
					}, tx5ezQjnJ588lHZb2y8_uGsRPxt8Gday9OvZVymtg997MmWK3bB9ajZOlUrRC07uCQhupP7i68Fu_hxQVh_EgfY.aYlo3OWZet2pLFg7wtl7K8s.transform).GetComponent<lcJ_0024o4QlCJj779lbTD_VTnIy5sO2aIoxJoTC_00249HYbtFHskS9uvU9Grlzn58YAeCbpw>());
					((RectTransform)Qr2FKmPcn_0024bxep1Rg4yhWNs[k].transform).sizeDelta += new Vector2(200f, 0f);
				}
				else
				{
					Qr2FKmPcn_0024bxep1Rg4yhWNs[k].pZEKY5TzLd4S3z2lXESoRnw = text;
					Qr2FKmPcn_0024bxep1Rg4yhWNs[k].FLSdXom6uNTfN55f5nxTsH8 = ujCoH5zXWVzYg0MXTOOvepyp0n3CWqd5yFc07HzK9iCsTQH9a5ApFinzqXMIYg_Euw.Int32_0 >= ujCoH5zXWVzYg0MXTOOvepyp0n3CWqd5yFc07HzK9iCsTQH9a5ApFinzqXMIYg_Euw.Int32_0;
				}
				Qr2FKmPcn_0024bxep1Rg4yhWNs[k].FLSdXom6uNTfN55f5nxTsH8 = !flag;
			}
			aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.sc3Wu5ekd0q_0024RAkWvkAECNI.FLSdXom6uNTfN55f5nxTsH8 = false;
			break;
		}
		case Hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU.hostingRoom:
		case Hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU.connectedToRoom:
		{
			if (M1SzAg9yMno83lUpmF7Dx_00244 <= 0f)
			{
				yKOE_0024Rr0al95lOeas6MNnnY = true;
				MPatchr.ShowDebugMsg(0, "");
				if (hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU_0 == Hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU.connectedToRoom)
				{
					aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.sc3Wu5ekd0q_0024RAkWvkAECNI.FLSdXom6uNTfN55f5nxTsH8 = true;
				}
			}
			else
			{
				M1SzAg9yMno83lUpmF7Dx_00244 -= Time.deltaTime;
				aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.sc3Wu5ekd0q_0024RAkWvkAECNI.FLSdXom6uNTfN55f5nxTsH8 = false;
			}
			if (Xzao_Hv76x9BNPYdMQB_g0E == global::_003CModule_003E.smethod_25<string>(1691553299u))
			{
				if (!aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.OwUd7bq6TgB1A4bFX_0024KLOmY && aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.VHzdJCHle0_WDdePHYCc8AhqzhWCxC0OPs26PKN15EZeVBCdnWO9jLHew1ubRXEy4NK_Omoj8gX2DsiXOUSGiTIWO9KIjLEIFV8_0024N5jD2IB99u864TUjCYn7tH6mXPiRjA_0 != aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.vHzdJCHle0_WDdePHYCc8AhqzhWCxC0OPs26PKN15EZeVBCdnWO9jLHew1ubRXEy4NK_Omoj8gX2DsiXOUSGiTIWO9KIjLEIFV8_0024N5jD2IB99u864TUjCYn7tH6mXPiRjA.Group && nm38U0evfhUbRbnE17ex1vE.Count > 0)
				{
					foreach (BlockData item in nm38U0evfhUbRbnE17ex1vE)
					{
						qQiH_ZjX8Up6taiejYAgY10(item, refresh: false);
					}
					nm38U0evfhUbRbnE17ex1vE.Clear();
					aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.GqSH_0024GjFZ_wAxTK4jePCv5hrQ4zJEGQBZ87jE_jTtN8M();
				}
				if (a1LHFL2YbSc0jehiQB0D4c4 > 0f)
				{
					a1LHFL2YbSc0jehiQB0D4c4 -= Time.deltaTime;
				}
				else
				{
					Vector3 vector = ((aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.BlockData_0 != null) ? new Vector3(aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.BlockData_0.x, aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.BlockData_0.y, aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.BlockData_0.z) : ((!(aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.Sl3qb7_zclIQRMX7sWCu5EE != null)) ? Vector3.zero : aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.Sl3qb7_zclIQRMX7sWCu5EE.transform.position));
					if (vector != Sl3qb7_zclIQRMX7sWCu5EE || vector3_0 != Camera.main.transform.position || B3qVW_bnOJvAqOC4Ed_0024_0024OYA != Camera.main.transform.rotation)
					{
						Sl3qb7_zclIQRMX7sWCu5EE = vector;
						vector3_0 = Camera.main.transform.position;
						B3qVW_bnOJvAqOC4Ed_0024_0024OYA = Camera.main.transform.rotation;
						bkEYtCXOGBC5a3sGZc1baFBEZsix9sHaNV7UHBoeB8aKxFK0bpuW1JKVsL3eaRtFkQ_0.nGT7VUZTN5zsOGBEDOWNDC4.RPC(global::_003CModule_003E.smethod_25<string>(1393178465u), BFDCHLBGJHF.Others, Sl3qb7_zclIQRMX7sWCu5EE, Camera.main.transform.position, Camera.main.transform.rotation, aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.BlockData_0 != null);
					}
					a1LHFL2YbSc0jehiQB0D4c4 = _M_tgdNQBt7jKh3UmYRBo5Y;
				}
			}
			for (int i = 0; i < fYYx8vN1tah7ZgfygXMH9eJU0YzjaUsqtv9tADffziMcu1k1It8P_0024ZciTdZuxN09bA.MCJUb7mzEcz9seOWPbquoos.jm9bAoN3Z4KRnFTRx6iFQKk - 1; i++)
			{
				MPatchr.ShowDebugMsg(i + 1, "");
			}
			if (Xzao_Hv76x9BNPYdMQB_g0E != global::_003CModule_003E.smethod_25<string>(1691553299u) || (!aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.sv6WiQ0SPlxR07h3vryVBDY.CheckPNL(global::_003CModule_003E.smethod_29<string>(3893445195u)) && !aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.sv6WiQ0SPlxR07h3vryVBDY.CheckPNL(global::_003CModule_003E.smethod_26<string>(2294466100u)) && !aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.sv6WiQ0SPlxR07h3vryVBDY.CheckPNL(global::_003CModule_003E.smethod_27<string>(872393466u)) && !aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.sv6WiQ0SPlxR07h3vryVBDY.CheckPNL(global::_003CModule_003E.smethod_28<string>(3000873796u)) && !aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.sv6WiQ0SPlxR07h3vryVBDY.CheckPNL(global::_003CModule_003E.smethod_28<string>(2727694093u)) && !aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.sv6WiQ0SPlxR07h3vryVBDY.CheckPNL(global::_003CModule_003E.smethod_26<string>(3215104880u)) && !aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.sv6WiQ0SPlxR07h3vryVBDY.CheckPNL(global::_003CModule_003E.smethod_25<string>(651129972u)) && !aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.sv6WiQ0SPlxR07h3vryVBDY.CheckPNL(global::_003CModule_003E.smethod_28<string>(527104520u)) && !aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.sv6WiQ0SPlxR07h3vryVBDY.CheckPNL(global::_003CModule_003E.smethod_28<string>(253924817u)) && !aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.sv6WiQ0SPlxR07h3vryVBDY.CheckPNL(global::_003CModule_003E.smethod_27<string>(2649932885u)) && !aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.sv6WiQ0SPlxR07h3vryVBDY.CheckPNL(global::_003CModule_003E.smethod_27<string>(193011794u)) && !aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.sv6WiQ0SPlxR07h3vryVBDY.CheckPNL(global::_003CModule_003E.smethod_25<string>(1046780685u))))
			{
				for (int j = 0; j < fYYx8vN1tah7ZgfygXMH9eJU0YzjaUsqtv9tADffziMcu1k1It8P_0024ZciTdZuxN09bA.Ko3u23nEqc9jjxdC06T65gk.Length; j++)
				{
					if (!new Class15(fYYx8vN1tah7ZgfygXMH9eJU0YzjaUsqtv9tADffziMcu1k1It8P_0024ZciTdZuxN09bA.Ko3u23nEqc9jjxdC06T65gk[j]).bDL22TQxAdGVLPWswOwgdOWuG2AEuTx3OXpQHzZV6eVL.TryGetValue(global::_003CModule_003E.smethod_25<string>(2843897746u), out var value))
					{
						value = global::_003CModule_003E.smethod_26<string>(1409220437u);
					}
					if (j <= string_0.Length - 1)
					{
						MPatchr.ShowDebugMsg(j + 1, global::_003CModule_003E.smethod_26<string>(2708872055u) + string_0[j] + global::_003CModule_003E.smethod_28<string>(3015877538u) + value);
					}
					else
					{
						MPatchr.ShowDebugMsg(j + 1, global::_003CModule_003E.smethod_29<string>(3383879665u) + value);
					}
				}
			}
			if (aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.iOXrQ3ou11UqRX_0024NnkXQnmI != null && aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.KtAkTPvF6e8D9Ss2KoAUKAQ != null)
			{
				aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.iOXrQ3ou11UqRX_0024NnkXQnmI.FLSdXom6uNTfN55f5nxTsH8 = false;
				aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.KtAkTPvF6e8D9Ss2KoAUKAQ.FLSdXom6uNTfN55f5nxTsH8 = false;
			}
			while (list_1.Count > fYYx8vN1tah7ZgfygXMH9eJU0YzjaUsqtv9tADffziMcu1k1It8P_0024ZciTdZuxN09bA.Ko3u23nEqc9jjxdC06T65gk.Length)
			{
				UnityEngine.Object.Destroy(list_1[0].gameObject);
				list_1.RemoveAt(0);
			}
			while (sgF4gmvJXFpS6IC8o9iQ7AE.Count > fYYx8vN1tah7ZgfygXMH9eJU0YzjaUsqtv9tADffziMcu1k1It8P_0024ZciTdZuxN09bA.Ko3u23nEqc9jjxdC06T65gk.Length)
			{
				UnityEngine.Object.Destroy(sgF4gmvJXFpS6IC8o9iQ7AE[0].gameObject);
				sgF4gmvJXFpS6IC8o9iQ7AE.RemoveAt(0);
			}
			if (Xzao_Hv76x9BNPYdMQB_g0E != jzex5TvAqs_0024v8PYs2a4Ebfs)
			{
				if (jzex5TvAqs_0024v8PYs2a4Ebfs == global::_003CModule_003E.smethod_29<string>(2961909598u))
				{
					bkEYtCXOGBC5a3sGZc1baFBEZsix9sHaNV7UHBoeB8aKxFK0bpuW1JKVsL3eaRtFkQ_0.nGT7VUZTN5zsOGBEDOWNDC4.RPC(global::_003CModule_003E.smethod_26<string>(1704906207u), BFDCHLBGJHF.Others, Y0682_EmDASRYkXiQKRSXHE.Hnc3QYGDRf1W8TRMMLwwZ48(JKGKJLLFMLE.MIIGKEBFKKD.EB00nJF8bfT7ocPR4HRIIqc(), aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.int_0));
				}
				jzex5TvAqs_0024v8PYs2a4Ebfs = Xzao_Hv76x9BNPYdMQB_g0E;
			}
			break;
		}
		}
	}

	internal bool TRpztJkOOsxNWmBuVtxmhAE(int index)
	{
		UjCoH5zXWVzYg0MXTOOvepyp0n3CWqd5yFc07HzK9iCsTQH9a5ApFinzqXMIYg_Euw ujCoH5zXWVzYg0MXTOOvepyp0n3CWqd5yFc07HzK9iCsTQH9a5ApFinzqXMIYg_Euw = new UjCoH5zXWVzYg0MXTOOvepyp0n3CWqd5yFc07HzK9iCsTQH9a5ApFinzqXMIYg_Euw(fYYx8vN1tah7ZgfygXMH9eJU0YzjaUsqtv9tADffziMcu1k1It8P_0024ZciTdZuxN09bA.W0rfbhNEVb6dg_5P3oF936Q()[index]);
		if (ujCoH5zXWVzYg0MXTOOvepyp0n3CWqd5yFc07HzK9iCsTQH9a5ApFinzqXMIYg_Euw == null)
		{
			return false;
		}
		hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU_0 = Hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU.connectingToRoom;
		return fYYx8vN1tah7ZgfygXMH9eJU0YzjaUsqtv9tADffziMcu1k1It8P_0024ZciTdZuxN09bA.Bdz4Mi8NzXgiGMyaKlWrEy4(ujCoH5zXWVzYg0MXTOOvepyp0n3CWqd5yFc07HzK9iCsTQH9a5ApFinzqXMIYg_Euw.T87T6htsXcJd1O4EqRlT6cc);
	}

	public void OnDestroy()
	{
		if (fYYx8vN1tah7ZgfygXMH9eJU0YzjaUsqtv9tADffziMcu1k1It8P_0024ZciTdZuxN09bA.PafCCA77BFEJYaAdgSJv7Ys)
		{
			fYYx8vN1tah7ZgfygXMH9eJU0YzjaUsqtv9tADffziMcu1k1It8P_0024ZciTdZuxN09bA.AA0Uc_IZxCzkuW6s0ItCfaA();
		}
	}

	internal void method_0()
	{
		hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU_0 = Hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU.connectingToPhoton;
		fYYx8vN1tah7ZgfygXMH9eJU0YzjaUsqtv9tADffziMcu1k1It8P_0024ZciTdZuxN09bA.wRKLPpK_0024M9Qsoos8QQC_00243xI((ABLDGOOGJIF)smethod_5(smethod_3(typeof(ABLDGOOGJIF).TypeHandle), smethod_4(aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.Nsgp7ukveOtfQVdR4hYjDAI), bool_0: true), global::_003CModule_003E.smethod_29<string>(1467747245u));
	}

	internal void AA0Uc_IZxCzkuW6s0ItCfaA()
	{
		hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU_0 = Hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU.disconnected;
		fYYx8vN1tah7ZgfygXMH9eJU0YzjaUsqtv9tADffziMcu1k1It8P_0024ZciTdZuxN09bA.AA0Uc_IZxCzkuW6s0ItCfaA();
	}

	internal bool uctMOjOKl0cnSRlnCCJ08CI(int players, string name, bool pwd, string pasw)
	{
		if (!fYYx8vN1tah7ZgfygXMH9eJU0YzjaUsqtv9tADffziMcu1k1It8P_0024ZciTdZuxN09bA.PafCCA77BFEJYaAdgSJv7Ys)
		{
			return false;
		}
		hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU_0 = Hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU.startingToHost;
		lK5oaNEKpgPwCxJ6s74woV74UddrTvXqHc_2jApM_0024aHQbM7D7DZBi7juF5tu_f3KzQ lK5oaNEKpgPwCxJ6s74woV74UddrTvXqHc_2jApM_0024aHQbM7D7DZBi7juF5tu_f3KzQ2 = lK5oaNEKpgPwCxJ6s74woV74UddrTvXqHc_2jApM_0024aHQbM7D7DZBi7juF5tu_f3KzQ.waSO9h6IK713sLfjGYPON1o();
		lK5oaNEKpgPwCxJ6s74woV74UddrTvXqHc_2jApM_0024aHQbM7D7DZBi7juF5tu_f3KzQ2.jm9bAoN3Z4KRnFTRx6iFQKk = (byte)players;
		ExitGames.Client.Photon.Hashtable hashtable = smethod_6();
		hashtable.Add(global::_003CModule_003E.smethod_25<string>(1401116079u), SfUlRb81w0TzI2Cx05i_Ky0);
		hashtable.Add(global::_003CModule_003E.smethod_28<string>(2879509997u), pwd);
		lK5oaNEKpgPwCxJ6s74woV74UddrTvXqHc_2jApM_0024aHQbM7D7DZBi7juF5tu_f3KzQ2.String_0 = new string[2]
		{
			global::_003CModule_003E.smethod_27<string>(1937996649u),
			global::_003CModule_003E.smethod_26<string>(2288195683u)
		};
		lK5oaNEKpgPwCxJ6s74woV74UddrTvXqHc_2jApM_0024aHQbM7D7DZBi7juF5tu_f3KzQ2.Hashtable_0 = hashtable;
		if (pwd)
		{
			GRs0NKS9rPFS44RIL__wdg4 = pasw;
		}
		return fYYx8vN1tah7ZgfygXMH9eJU0YzjaUsqtv9tADffziMcu1k1It8P_0024ZciTdZuxN09bA.gyI5U1CARLqIONGr6rMczLM(name, lK5oaNEKpgPwCxJ6s74woV74UddrTvXqHc_2jApM_0024aHQbM7D7DZBi7juF5tu_f3KzQ2, null);
	}

	internal void qQiH_ZjX8Up6taiejYAgY10(BlockData bd, bool refresh = true)
	{
		if (smethod_7(Xzao_Hv76x9BNPYdMQB_g0E, global::_003CModule_003E.smethod_27<string>(3514760917u)) && !aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.OwUd7bq6TgB1A4bFX_0024KLOmY && aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.VHzdJCHle0_WDdePHYCc8AhqzhWCxC0OPs26PKN15EZeVBCdnWO9jLHew1ubRXEy4NK_Omoj8gX2DsiXOUSGiTIWO9KIjLEIFV8_0024N5jD2IB99u864TUjCYn7tH6mXPiRjA_0 != aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.vHzdJCHle0_WDdePHYCc8AhqzhWCxC0OPs26PKN15EZeVBCdnWO9jLHew1ubRXEy4NK_Omoj8gX2DsiXOUSGiTIWO9KIjLEIFV8_0024N5jD2IB99u864TUjCYn7tH6mXPiRjA.Group)
		{
			aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.HAUjeOv9NpGdCjgGsLAf_0024_0024w(bd, addtobd: true, refresh);
		}
		else
		{
			nm38U0evfhUbRbnE17ex1vE.Add(bd);
		}
	}

	internal void gmPmm0_0024KxN_uMsSz7NC6R6jL66n51ObGabU3qvvo6qeg()
	{
		if (hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU_0 != Hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU.hostingRoom)
		{
			hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU_0 = Hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU.connectedToRoom;
			aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.a1j0jjN_0024MJMaHvEbQb12VkSxN0YF1la8dglN_0024kcX56bR();
			string string_ = smethod_8(global::_003CModule_003E.smethod_27<string>(4198575846u), PrthA5W3yQtx6doFwW_0024OZILfZZZw6xqyA0BuxYU3EaqL);
			if (!smethod_10((FileSystemInfo)smethod_9(string_)))
			{
				smethod_11(string_);
			}
			aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.smethod_0(PrthA5W3yQtx6doFwW_0024OZILfZZZw6xqyA0BuxYU3EaqL, YqLeG_00246_0024ROp_00242YS9Z3_5De8);
			aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.z3n2bV4jTuYqWHBSISaM9vo();
			Build.GFJLEEJELOL = smethod_12(bool_0: true);
			aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.a1j0jjN_0024MJMaHvEbQb12VkSxN0YF1la8dglN_0024kcX56bR();
			aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.vO_KQIaPS1OoKibvTAx3G6g(global::_003CModule_003E.smethod_25<string>(1691553299u), loading: true);
		}
	}

	[BBNLOHJIPHJ]
	public void Flash(aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.JVDvlJAU_UpBfFlkSS4dz_00248kv5abfDJ2__002485ml_QUV8f_QRNWaHa5H5V3A4r06LroP9HTbhPsjhXYZvZn3LwqA4ocLFaSz_CXXamUCpUMYfS action, bool ud, bool fb, bool isEven)
	{
		aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.smethod_2(action, ud, fb, isEven);
	}

	[BBNLOHJIPHJ]
	public void Flash(int a, int b, int c, int x, int y, int z)
	{
		if (smethod_7(Xzao_Hv76x9BNPYdMQB_g0E, global::_003CModule_003E.smethod_29<string>(2573180164u)))
		{
			aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.smethod_1(a, b, c, x, y, z);
		}
	}

	[BBNLOHJIPHJ]
	public void Polo(byte[] bdbytes)
	{
		BlockData blockData = aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.OWtbIufJWOmFnUwp2L_jUuo();
		blockData.oypAWJI0VRhHUhWe0HST7q0(Y0682_EmDASRYkXiQKRSXHE.T6FSlkYzsIbsmI_74jwTbog(bdbytes));
		qQiH_ZjX8Up6taiejYAgY10(blockData);
	}

	internal void SuzfiH0WfgY0ASDyAR80ptY(Vector3 v)
	{
		BlockData blockData = smethod_13();
		blockData.x = (int)v.x;
		blockData.y = (int)v.y;
		blockData.z = (int)v.z;
		blockData.type = (BlockData.AAHMDBHDCDK)(-1);
		qQiH_ZjX8Up6taiejYAgY10(blockData);
	}

	[BBNLOHJIPHJ]
	public void AnotherRPCMethod(Vector3[] rm, byte[] add, int len)
	{
		foreach (Vector3 v in rm)
		{
			SuzfiH0WfgY0ASDyAR80ptY(v);
		}
		Stream stream_ = H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.jr_Hlgy0dyj6anbBs_hccvs(Y0682_EmDASRYkXiQKRSXHE.T6FSlkYzsIbsmI_74jwTbog(add));
		for (int j = 0; j < len; j++)
		{
			BlockData blockData = smethod_13();
			blockData.oypAWJI0VRhHUhWe0HST7q0(stream_, bool_0: false);
			qQiH_ZjX8Up6taiejYAgY10(blockData, refresh: false);
		}
	}

	[BBNLOHJIPHJ]
	public void RPC_Attach(byte[] bdbytes)
	{
		JKGKJLLFMLE.MIIGKEBFKKD.oypAWJI0VRhHUhWe0HST7q0(Y0682_EmDASRYkXiQKRSXHE.T6FSlkYzsIbsmI_74jwTbog(bdbytes));
		smethod_14((string)null);
	}

	[BBNLOHJIPHJ]
	public void RequestForPickupItems(int[] chunks, DBMLFPDNFAB info)
	{
		smethod_15((MonoBehaviour)this, aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.FGAOESvRy6bCNgeQ0J8Oo0E(info.FMIBNEKIIKA, chunks));
	}

	[BBNLOHJIPHJ]
	public void Polo(Vector3[] poses)
	{
		foreach (Vector3 v in poses)
		{
			SuzfiH0WfgY0ASDyAR80ptY(v);
		}
	}

	[BBNLOHJIPHJ]
	public void Marco(string machineName)
	{
		YqLeG_00246_0024ROp_00242YS9Z3_5De8 = machineName;
		aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.smethod_0(PrthA5W3yQtx6doFwW_0024OZILfZZZw6xqyA0BuxYU3EaqL, machineName);
	}

	[BBNLOHJIPHJ]
	public void Marco(int amount, byte[] dat, bool clear, int total)
	{
		if (amount != 0)
		{
			if (clear)
			{
				aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.z3n2bV4jTuYqWHBSISaM9vo();
			}
			Stream stream_ = H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.jr_Hlgy0dyj6anbBs_hccvs(Y0682_EmDASRYkXiQKRSXHE.T6FSlkYzsIbsmI_74jwTbog(dat));
			for (int i = 0; i < amount; i++)
			{
				BlockData blockData = smethod_13();
				blockData.oypAWJI0VRhHUhWe0HST7q0(stream_, bool_0: false);
				qQiH_ZjX8Up6taiejYAgY10(blockData, refresh: false);
			}
			aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.GqSH_0024GjFZ_wAxTK4jePCv5hrQ4zJEGQBZ87jE_jTtN8M();
			pLE8WTgZE0t0Om1V6BWOGpQ += amount;
			if (total != -1)
			{
				MPatchr.ShowDebugMsg(0, smethod_17(global::_003CModule_003E.smethod_27<string>(1281964857u), (object)smethod_16((double)((float)pLE8WTgZE0t0Om1V6BWOGpQ * 1f / (float)total * 100f))));
				yKOE_0024Rr0al95lOeas6MNnnY = pLE8WTgZE0t0Om1V6BWOGpQ == total;
				M1SzAg9yMno83lUpmF7Dx_00244 = 2f;
			}
		}
	}

	[BBNLOHJIPHJ]
	public void RPC_StartWatching(Vector3 cursorPos, Vector3 headPos, Quaternion headRot, bool iblock, DBMLFPDNFAB info)
	{
		if (smethod_18(Xzao_Hv76x9BNPYdMQB_g0E, global::_003CModule_003E.smethod_25<string>(1691553299u)))
		{
			return;
		}
		while (list_1.Count < fYYx8vN1tah7ZgfygXMH9eJU0YzjaUsqtv9tADffziMcu1k1It8P_0024ZciTdZuxN09bA.Ko3u23nEqc9jjxdC06T65gk.Length)
		{
			GameObject gameObject = smethod_19(PrimitiveType.Cube);
			smethod_22((Renderer)gameObject.GetComponent<MeshRenderer>(), smethod_21(smethod_20(global::_003CModule_003E.smethod_27<string>(4032046109u))));
			smethod_23((Renderer)gameObject.GetComponent<MeshRenderer>(), bool_0: false);
			smethod_24(gameObject, 15);
			gameObject.AddComponent<c2wojWPyeJ90X8UxbVw8Ie7t2iSXT4W6AM5UctjLMkqq>().u5ER09FBgDoEuNjNt6mdw_k = true;
			list_1.Add(smethod_25(gameObject));
			if (smethod_26((UnityEngine.Object)aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.eMeHtB0nEBTQAy_Ed4AHx1M, (UnityEngine.Object)null))
			{
				GameObject gameObject_ = UnityEngine.Object.Instantiate(aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.eMeHtB0nEBTQAy_Ed4AHx1M.LoadAsset<GameObject>(global::_003CModule_003E.smethod_29<string>(1045777178u)));
				smethod_24(gameObject_, 15);
				smethod_24(smethod_28((Component)smethod_27(smethod_25(gameObject_), global::_003CModule_003E.smethod_29<string>(2398056705u))), 15);
				sgF4gmvJXFpS6IC8o9iQ7AE.Add(smethod_25(gameObject_));
			}
		}
		list_1.RemoveAll((Transform item) => eXmdbxoweAxhAyVJD_NghlOeg3mZ9hE3zOHuzYHu53prz2w1SCi1gvsW4FagbdDG9SsqaO_G4j5JonO_GuUL77U.smethod_0((UnityEngine.Object)item, (UnityEngine.Object)null));
		int num = -1;
		for (int num2 = 0; num2 < fYYx8vN1tah7ZgfygXMH9eJU0YzjaUsqtv9tADffziMcu1k1It8P_0024ZciTdZuxN09bA.Ko3u23nEqc9jjxdC06T65gk.Length; num2++)
		{
			if (fYYx8vN1tah7ZgfygXMH9eJU0YzjaUsqtv9tADffziMcu1k1It8P_0024ZciTdZuxN09bA.Ko3u23nEqc9jjxdC06T65gk[num2] == info.FMIBNEKIIKA)
			{
				num = num2;
				break;
			}
		}
		if (num == -1)
		{
			return;
		}
		Color k9jTQ33irMfqZyYWrqgwpFA = ((num > gglchrqroE9SNUU7Fu65V5Y.Length - 1) ? Color.white : gglchrqroE9SNUU7Fu65V5Y[num]);
		Color color_ = new Color(k9jTQ33irMfqZyYWrqgwpFA.r, k9jTQ33irMfqZyYWrqgwpFA.g, k9jTQ33irMfqZyYWrqgwpFA.b, 0.2f);
		smethod_30(smethod_29((Component)list_1[num]), cursorPos);
		if (!iblock)
		{
			smethod_32(smethod_31((Renderer)list_1[num].GetComponent<MeshRenderer>()), color_);
			smethod_23((Renderer)list_1[num].GetComponent<MeshRenderer>(), bool_0: true);
		}
		else
		{
			smethod_23((Renderer)list_1[num].GetComponent<MeshRenderer>(), bool_0: false);
		}
		list_1[num].GetComponent<c2wojWPyeJ90X8UxbVw8Ie7t2iSXT4W6AM5UctjLMkqq>().k9jTQ33irMfqZyYWrqgwpFA = k9jTQ33irMfqZyYWrqgwpFA;
		if (smethod_26((UnityEngine.Object)aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.eMeHtB0nEBTQAy_Ed4AHx1M, (UnityEngine.Object)null))
		{
			sgF4gmvJXFpS6IC8o9iQ7AE.RemoveAll((Transform item) => eXmdbxoweAxhAyVJD_NghlOeg3mZ9hE3zOHuzYHu53prz2w1SCi1gvsW4FagbdDG9SsqaO_G4j5JonO_GuUL77U.smethod_0((UnityEngine.Object)item, (UnityEngine.Object)null));
			smethod_30(smethod_29((Component)sgF4gmvJXFpS6IC8o9iQ7AE[num]), headPos);
			smethod_33(smethod_29((Component)sgF4gmvJXFpS6IC8o9iQ7AE[num]), headRot);
			MeshRenderer component = smethod_27(sgF4gmvJXFpS6IC8o9iQ7AE[num], global::_003CModule_003E.smethod_26<string>(1984010111u)).GetComponent<MeshRenderer>();
			smethod_34((Renderer)component)[0].color = new Color(k9jTQ33irMfqZyYWrqgwpFA.r, k9jTQ33irMfqZyYWrqgwpFA.g, k9jTQ33irMfqZyYWrqgwpFA.b, smethod_35(smethod_34((Renderer)component)[0]).a);
			component.materials[1].color = new Color(k9jTQ33irMfqZyYWrqgwpFA.r, k9jTQ33irMfqZyYWrqgwpFA.g, k9jTQ33irMfqZyYWrqgwpFA.b, component.materials[1].color.a);
			component.materials[2].color = new Color(k9jTQ33irMfqZyYWrqgwpFA.r, k9jTQ33irMfqZyYWrqgwpFA.g, k9jTQ33irMfqZyYWrqgwpFA.b, component.materials[2].color.a);
		}
	}

	[BBNLOHJIPHJ]
	public void RPC_SyncWinner(bool winner, bool dinner)
	{
		if (winner && dinner)
		{
			gmPmm0_0024KxN_uMsSz7NC6R6jL66n51ObGabU3qvvo6qeg();
			return;
		}
		fYYx8vN1tah7ZgfygXMH9eJU0YzjaUsqtv9tADffziMcu1k1It8P_0024ZciTdZuxN09bA.GlN8KncaZ8T7GjfnyKt2zB4();
		MPatchr.ShowDebugMsg(global::_003CModule_003E.smethod_26<string>(659276825u));
	}

	[BBNLOHJIPHJ]
	public void RPC_SyncWinner(string pwdverify, DBMLFPDNFAB info)
	{
		if (hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU_0 == Hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU.hostingRoom)
		{
			if (smethod_7(GRs0NKS9rPFS44RIL__wdg4, string.Empty) || smethod_7(pwdverify, GRs0NKS9rPFS44RIL__wdg4))
			{
				list_0.Add(info.FMIBNEKIIKA);
				smethod_36(bkEYtCXOGBC5a3sGZc1baFBEZsix9sHaNV7UHBoeB8aKxFK0bpuW1JKVsL3eaRtFkQ_0.nGT7VUZTN5zsOGBEDOWNDC4, global::_003CModule_003E.smethod_26<string>(700940359u), info.FMIBNEKIIKA, new object[2] { true, true });
				smethod_36(bkEYtCXOGBC5a3sGZc1baFBEZsix9sHaNV7UHBoeB8aKxFK0bpuW1JKVsL3eaRtFkQ_0.nGT7VUZTN5zsOGBEDOWNDC4, global::_003CModule_003E.smethod_28<string>(2909665688u), info.FMIBNEKIIKA, new object[1] { JKGKJLLFMLE.IGOBPLOLHEP.machineName });
				smethod_37(bkEYtCXOGBC5a3sGZc1baFBEZsix9sHaNV7UHBoeB8aKxFK0bpuW1JKVsL3eaRtFkQ_0.nGT7VUZTN5zsOGBEDOWNDC4, global::_003CModule_003E.smethod_25<string>(3509625966u), BFDCHLBGJHF.Others, new object[1] { Y0682_EmDASRYkXiQKRSXHE.Hnc3QYGDRf1W8TRMMLwwZ48(JKGKJLLFMLE.MIIGKEBFKKD.EB00nJF8bfT7ocPR4HRIIqc(), aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.int_0) });
				smethod_15((MonoBehaviour)this, aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.FGAOESvRy6bCNgeQ0J8Oo0E(info.FMIBNEKIIKA));
			}
			else
			{
				smethod_36(bkEYtCXOGBC5a3sGZc1baFBEZsix9sHaNV7UHBoeB8aKxFK0bpuW1JKVsL3eaRtFkQ_0.nGT7VUZTN5zsOGBEDOWNDC4, global::_003CModule_003E.smethod_28<string>(3364965193u), info.FMIBNEKIIKA, new object[2] { false, false });
			}
		}
	}

	[BBNLOHJIPHJ]
	public void RPC_ReSync(DBMLFPDNFAB info)
	{
		if (info.FMIBNEKIIKA != fYYx8vN1tah7ZgfygXMH9eJU0YzjaUsqtv9tADffziMcu1k1It8P_0024ZciTdZuxN09bA.mSC9Nl_0024CzW4EDuXyt6DfwUs)
		{
			smethod_36(bkEYtCXOGBC5a3sGZc1baFBEZsix9sHaNV7UHBoeB8aKxFK0bpuW1JKVsL3eaRtFkQ_0.nGT7VUZTN5zsOGBEDOWNDC4, global::_003CModule_003E.smethod_29<string>(1229618530u), info.FMIBNEKIIKA, new object[1] { JKGKJLLFMLE.IGOBPLOLHEP.machineName });
			smethod_37(bkEYtCXOGBC5a3sGZc1baFBEZsix9sHaNV7UHBoeB8aKxFK0bpuW1JKVsL3eaRtFkQ_0.nGT7VUZTN5zsOGBEDOWNDC4, global::_003CModule_003E.smethod_28<string>(2560578033u), BFDCHLBGJHF.Others, new object[1] { Y0682_EmDASRYkXiQKRSXHE.Hnc3QYGDRf1W8TRMMLwwZ48(JKGKJLLFMLE.MIIGKEBFKKD.EB00nJF8bfT7ocPR4HRIIqc(), aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.int_0) });
			smethod_15((MonoBehaviour)this, aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.FGAOESvRy6bCNgeQ0J8Oo0E(info.FMIBNEKIIKA));
		}
	}

	public void OnConnectedToMaster()
	{
		fYYx8vN1tah7ZgfygXMH9eJU0YzjaUsqtv9tADffziMcu1k1It8P_0024ZciTdZuxN09bA.DGVijAeYURJ4Od_0024dBWRTwIk();
	}

	public void OnJoinedLobby()
	{
		hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU_0 = Hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU.viewingRooms;
	}

	public void OnCreatedRoom()
	{
		hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU_0 = Hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU.hostingRoom;
	}

	public void OnJoinedRoom()
	{
		if (bkEYtCXOGBC5a3sGZc1baFBEZsix9sHaNV7UHBoeB8aKxFK0bpuW1JKVsL3eaRtFkQ_0 == null)
		{
			PhotonView photonView_ = smethod_0((Component)this).AddComponent<PhotonView>();
			bkEYtCXOGBC5a3sGZc1baFBEZsix9sHaNV7UHBoeB8aKxFK0bpuW1JKVsL3eaRtFkQ_0 = new BkEYtCXOGBC5a3sGZc1baFBEZsix9sHaNV7UHBoeB8aKxFK0bpuW1JKVsL3eaRtFkQ(photonView_);
			bkEYtCXOGBC5a3sGZc1baFBEZsix9sHaNV7UHBoeB8aKxFK0bpuW1JKVsL3eaRtFkQ_0.nxch5NN7yn_2gEoq38N_tzo = 1;
		}
		if (hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU_0 != Hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU.hostingRoom)
		{
			hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU_0 = Hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU.verifyingPWD;
			smethod_37(bkEYtCXOGBC5a3sGZc1baFBEZsix9sHaNV7UHBoeB8aKxFK0bpuW1JKVsL3eaRtFkQ_0.nGT7VUZTN5zsOGBEDOWNDC4, global::_003CModule_003E.smethod_29<string>(3890992921u), BFDCHLBGJHF.MasterClient, new object[1] { aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.KtAkTPvF6e8D9Ss2KoAUKAQ.pZEKY5TzLd4S3z2lXESoRnw });
		}
		Class15 @class = new Class15(fYYx8vN1tah7ZgfygXMH9eJU0YzjaUsqtv9tADffziMcu1k1It8P_0024ZciTdZuxN09bA.mSC9Nl_0024CzW4EDuXyt6DfwUs);
		ExitGames.Client.Photon.Hashtable bDL22TQxAdGVLPWswOwgdOWuG2AEuTx3OXpQHzZV6eVL = @class.bDL22TQxAdGVLPWswOwgdOWuG2AEuTx3OXpQHzZV6eVL;
		if (bDL22TQxAdGVLPWswOwgdOWuG2AEuTx3OXpQHzZV6eVL.ContainsKey(global::_003CModule_003E.smethod_27<string>(2170650024u)))
		{
			smethod_38(bDL22TQxAdGVLPWswOwgdOWuG2AEuTx3OXpQHzZV6eVL, (object)global::_003CModule_003E.smethod_26<string>(2733953723u), (object)JKGKJLLFMLE.IGOBPLOLHEP.userName);
		}
		else
		{
			bDL22TQxAdGVLPWswOwgdOWuG2AEuTx3OXpQHzZV6eVL.Add(global::_003CModule_003E.smethod_27<string>(2170650024u), JKGKJLLFMLE.IGOBPLOLHEP.userName);
		}
		if (bDL22TQxAdGVLPWswOwgdOWuG2AEuTx3OXpQHzZV6eVL.ContainsKey(global::_003CModule_003E.smethod_28<string>(2636485985u)))
		{
			smethod_38(bDL22TQxAdGVLPWswOwgdOWuG2AEuTx3OXpQHzZV6eVL, (object)global::_003CModule_003E.smethod_26<string>(3991941807u), (object)JKGKJLLFMLE.AKAFEPJIFKC);
		}
		else
		{
			bDL22TQxAdGVLPWswOwgdOWuG2AEuTx3OXpQHzZV6eVL.Add(global::_003CModule_003E.smethod_28<string>(2636485985u), JKGKJLLFMLE.AKAFEPJIFKC);
		}
		@class.bDL22TQxAdGVLPWswOwgdOWuG2AEuTx3OXpQHzZV6eVL = bDL22TQxAdGVLPWswOwgdOWuG2AEuTx3OXpQHzZV6eVL;
	}

	public void OnPhotonJoinRoomFailed()
	{
		smethod_39((object)global::_003CModule_003E.smethod_26<string>(2667208521u), 1);
		hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU_0 = Hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU.viewingRooms;
	}

	public void OnLeftRoom()
	{
	}

	public void OnDisconnectedFromPhoton()
	{
		MPatchr.ShowDebugMsg(0, global::_003CModule_003E.smethod_25<string>(1226289940u));
		MPatchr.ShowDebugMsg(1, "");
		MPatchr.ShowDebugMsg(2, "");
		MPatchr.ShowDebugMsg(3, "");
		MPatchr.ShowDebugMsg(4, "");
		while (Qr2FKmPcn_0024bxep1Rg4yhWNs.Count > 0)
		{
			smethod_40((UnityEngine.Object)smethod_28((Component)Qr2FKmPcn_0024bxep1Rg4yhWNs[Qr2FKmPcn_0024bxep1Rg4yhWNs.Count - 1]));
			Qr2FKmPcn_0024bxep1Rg4yhWNs.RemoveAt(Qr2FKmPcn_0024bxep1Rg4yhWNs.Count - 1);
		}
		fYYx8vN1tah7ZgfygXMH9eJU0YzjaUsqtv9tADffziMcu1k1It8P_0024ZciTdZuxN09bA.AA0Uc_IZxCzkuW6s0ItCfaA();
		hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU_0 = Hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU.disconnected;
		aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.a1j0jjN_0024MJMaHvEbQb12VkSxN0YF1la8dglN_0024kcX56bR();
		if (smethod_26((UnityEngine.Object)aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.iOXrQ3ou11UqRX_0024NnkXQnmI, (UnityEngine.Object)null) && smethod_26((UnityEngine.Object)aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.KtAkTPvF6e8D9Ss2KoAUKAQ, (UnityEngine.Object)null))
		{
			aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.iOXrQ3ou11UqRX_0024NnkXQnmI.FLSdXom6uNTfN55f5nxTsH8 = true;
			aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.KtAkTPvF6e8D9Ss2KoAUKAQ.FLSdXom6uNTfN55f5nxTsH8 = true;
			aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.GjjztfvuViq0_Q_00249_BUsoYY.FLSdXom6uNTfN55f5nxTsH8 = true;
		}
		aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.GjjztfvuViq0_Q_00249_BUsoYY.pZEKY5TzLd4S3z2lXESoRnw = global::_003CModule_003E.smethod_28<string>(2803602045u);
		smethod_41((Selectable)aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.zmh9Gea8K2d3y7BCuea6ouU, bool_0: true);
		aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.QEcOzq1h48SV8MxfAnba82U.FLSdXom6uNTfN55f5nxTsH8 = false;
		smethod_40((UnityEngine.Object)smethod_0((Component)this));
	}

	public void OnMasterClientSwitched()
	{
		MPatchr.ShowDebugMsg(0, global::_003CModule_003E.smethod_26<string>(2631815404u));
		MPatchr.ShowDebugMsg(1, "");
		MPatchr.ShowDebugMsg(2, "");
		MPatchr.ShowDebugMsg(3, "");
		MPatchr.ShowDebugMsg(4, "");
		fYYx8vN1tah7ZgfygXMH9eJU0YzjaUsqtv9tADffziMcu1k1It8P_0024ZciTdZuxN09bA.AA0Uc_IZxCzkuW6s0ItCfaA();
		hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU_0 = Hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU.disconnected;
		aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.a1j0jjN_0024MJMaHvEbQb12VkSxN0YF1la8dglN_0024kcX56bR();
		if (smethod_26((UnityEngine.Object)aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.iOXrQ3ou11UqRX_0024NnkXQnmI, (UnityEngine.Object)null) && smethod_26((UnityEngine.Object)aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.KtAkTPvF6e8D9Ss2KoAUKAQ, (UnityEngine.Object)null))
		{
			aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.iOXrQ3ou11UqRX_0024NnkXQnmI.FLSdXom6uNTfN55f5nxTsH8 = true;
			aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.KtAkTPvF6e8D9Ss2KoAUKAQ.FLSdXom6uNTfN55f5nxTsH8 = true;
			aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.GjjztfvuViq0_Q_00249_BUsoYY.FLSdXom6uNTfN55f5nxTsH8 = true;
		}
		smethod_40((UnityEngine.Object)smethod_0((Component)this));
	}

	public void OnPhotonPlayerDisconnected(OPLNFKECCLE player)
	{
		if (list_0.Contains(player))
		{
			list_0.Remove(player);
		}
	}

	public void OnPhotonCreateRoomFailed()
	{
		smethod_42((object)global::_003CModule_003E.smethod_27<string>(3925008648u), 1);
		hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU_0 = Hn0l0GNFrsPTtJLnYPdC5kufoQdroygW0c_0024coK_0024bqiHUyUKuNGxxEWa6iWJgwKxAk5py8zqyEY7YQFC_01oXiVU.viewingRooms;
	}

	internal static GameObject smethod_0(Component component_0)
	{
		return component_0.gameObject;
	}

	internal static void smethod_1(UnityEngine.Object object_0)
	{
		UnityEngine.Object.DontDestroyOnLoad(object_0);
	}

	internal static Scene smethod_2()
	{
		return SceneManager.GetActiveScene();
	}

	internal static Type smethod_3(RuntimeTypeHandle runtimeTypeHandle_0)
	{
		return Type.GetTypeFromHandle(runtimeTypeHandle_0);
	}

	internal static string smethod_4(ListController listController_0)
	{
		return listController_0.GetSelectedItem();
	}

	internal static object smethod_5(Type type_0, string string_1, bool bool_0)
	{
		return Enum.Parse(type_0, string_1, bool_0);
	}

	internal static ExitGames.Client.Photon.Hashtable smethod_6()
	{
		return new ExitGames.Client.Photon.Hashtable();
	}

	internal static bool smethod_7(string string_1, string string_2)
	{
		return string_1 == string_2;
	}

	internal static string smethod_8(string string_1, string string_2)
	{
		return Path.Combine(string_1, string_2);
	}

	internal static DirectoryInfo smethod_9(string string_1)
	{
		return new DirectoryInfo(string_1);
	}

	internal static bool smethod_10(FileSystemInfo fileSystemInfo_0)
	{
		return fileSystemInfo_0.Exists;
	}

	internal static DirectoryInfo smethod_11(string string_1)
	{
		return Directory.CreateDirectory(string_1);
	}

	internal static BuildData smethod_12(bool bool_0)
	{
		return new BuildData(bool_0);
	}

	internal static BlockData smethod_13()
	{
		return new BlockData();
	}

	internal static void smethod_14(string string_1)
	{
		JKGKJLLFMLE.IEBPGHNPODP(string_1);
	}

	internal static Coroutine smethod_15(MonoBehaviour monoBehaviour_0, IEnumerator ienumerator_0)
	{
		return monoBehaviour_0.StartCoroutine(ienumerator_0);
	}

	internal static double smethod_16(double double_0)
	{
		return Math.Floor(double_0);
	}

	internal static string smethod_17(string string_1, object object_0)
	{
		return string.Format(string_1, object_0);
	}

	internal static bool smethod_18(string string_1, string string_2)
	{
		return string_1 != string_2;
	}

	internal static GameObject smethod_19(PrimitiveType primitiveType_0)
	{
		return GameObject.CreatePrimitive(primitiveType_0);
	}

	internal static Shader smethod_20(string string_1)
	{
		return Shader.Find(string_1);
	}

	internal static Material smethod_21(Shader shader_0)
	{
		return new Material(shader_0);
	}

	internal static void smethod_22(Renderer renderer_0, Material material_0)
	{
		renderer_0.material = material_0;
	}

	internal static void smethod_23(Renderer renderer_0, bool bool_0)
	{
		renderer_0.enabled = bool_0;
	}

	internal static void smethod_24(GameObject gameObject_0, int int_1)
	{
		gameObject_0.layer = int_1;
	}

	internal static Transform smethod_25(GameObject gameObject_0)
	{
		return gameObject_0.transform;
	}

	internal static bool smethod_26(UnityEngine.Object object_0, UnityEngine.Object object_1)
	{
		return object_0 != object_1;
	}

	internal static Transform smethod_27(Transform transform_0, string string_1)
	{
		return transform_0.Find(string_1);
	}

	internal static GameObject smethod_28(Component component_0)
	{
		return component_0.gameObject;
	}

	internal static Transform smethod_29(Component component_0)
	{
		return component_0.transform;
	}

	internal static void smethod_30(Transform transform_0, Vector3 vector3_1)
	{
		transform_0.position = vector3_1;
	}

	internal static Material smethod_31(Renderer renderer_0)
	{
		return renderer_0.material;
	}

	internal static void smethod_32(Material material_0, Color color_0)
	{
		material_0.color = color_0;
	}

	internal static void smethod_33(Transform transform_0, Quaternion quaternion_0)
	{
		transform_0.rotation = quaternion_0;
	}

	internal static Material[] smethod_34(Renderer renderer_0)
	{
		return renderer_0.materials;
	}

	internal static Color smethod_35(Material material_0)
	{
		return material_0.color;
	}

	internal static void smethod_36(PhotonView photonView_0, string string_1, OPLNFKECCLE oplnfkeccle_0, object[] object_0)
	{
		photonView_0.RPC(string_1, oplnfkeccle_0, object_0);
	}

	internal static void smethod_37(PhotonView photonView_0, string string_1, BFDCHLBGJHF bfdchlbgjhf_0, object[] object_0)
	{
		photonView_0.RPC(string_1, bfdchlbgjhf_0, object_0);
	}

	internal static void smethod_38(ExitGames.Client.Photon.Hashtable hashtable_0, object object_0, object object_1)
	{
		hashtable_0[object_0] = object_1;
	}

	internal static void smethod_39(object object_0, int int_1)
	{
		DP.C(object_0, int_1);
	}

	internal static void smethod_40(UnityEngine.Object object_0)
	{
		UnityEngine.Object.Destroy(object_0);
	}

	internal static void smethod_41(Selectable selectable_0, bool bool_0)
	{
		selectable_0.interactable = bool_0;
	}

	internal static void smethod_42(object object_0, int int_1)
	{
		DP.D(object_0, int_1);
	}
}
