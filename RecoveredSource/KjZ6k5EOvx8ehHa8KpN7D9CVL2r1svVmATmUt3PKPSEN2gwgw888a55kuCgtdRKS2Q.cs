using System.IO;

internal static class KjZ6k5EOvx8ehHa8KpN7D9CVL2r1svVmATmUt3PKPSEN2gwgw888a55kuCgtdRKS2Q
{
	private const int sxoLaSDAwkuf85JAkIskgm31MYUtkG9eg_0024KifgbzGKJT = 1;

	internal static void S5T_0024i6sgT114cTBqa5QJ_lg(this BinaryWriter binaryWriter_0, string string_0)
	{
		if (string_0 != null)
		{
			smethod_1(binaryWriter_0, string_0);
		}
		else
		{
			smethod_1(binaryWriter_0, global::_003CModule_003E.smethod_27<string>(1909706257u));
		}
	}

	internal static string _vLDoSg_0024v8_hEGZOgGaUZwg(this BinaryReader binaryReader_0)
	{
		string text = smethod_2(binaryReader_0);
		if (smethod_3(text, global::_003CModule_003E.smethod_27<string>(1909706257u)))
		{
			return null;
		}
		return text;
	}

	internal static BuildData smethod_0(byte[] byte_0)
	{
		byte[] byte_1 = Y0682_EmDASRYkXiQKRSXHE.T6FSlkYzsIbsmI_74jwTbog(byte_0);
		BuildData buildData = smethod_4(bool_0: false);
		BinaryReader binaryReader = smethod_6((Stream)smethod_5(byte_1));
		smethod_8(smethod_7(binaryReader), 0L, SeekOrigin.Begin);
		int num = smethod_9(binaryReader);
		if (num > 1)
		{
			mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_26<string>(3374235385u) + num + global::_003CModule_003E.smethod_29<string>(3748975161u) + 1);
			return null;
		}
		buildData.version = binaryReader.ReadString();
		buildData.physVersion = binaryReader.ReadSingle();
		buildData.speedLimit = binaryReader.ReadInt16();
		buildData.isHardJoint = binaryReader.ReadBoolean();
		buildData.isTakeoffAssist = binaryReader.ReadBoolean();
		buildData.moverSound = (BuildData.DIAEDBEAJEB)binaryReader.ReadByte();
		buildData.thrusterSound = (BuildData.MKIBMAONIFG)binaryReader.ReadByte();
		buildData.jointSound = (BuildData.AAINHGFANJE)binaryReader.ReadByte();
		buildData.isWalkSoundEnabled = binaryReader.ReadBoolean();
		buildData.isFoldSoundEnabled = binaryReader.ReadBoolean();
		buildData.isStretchSoundEnabled = binaryReader.ReadBoolean();
		buildData.isSmokeSoundEnabled = binaryReader.ReadBoolean();
		buildData.jointOrder = (BuildData.CGNBFFGFBLE)binaryReader.ReadByte();
		buildData.isPatchJoint = binaryReader.ReadBoolean();
		buildData.imageName = binaryReader._vLDoSg_0024v8_hEGZOgGaUZwg();
		buildData.offsetX = binaryReader.ReadSingle();
		buildData.offsetZ = binaryReader.ReadSingle();
		buildData.spawnAltOffset = binaryReader.ReadInt32();
		buildData.comment = binaryReader._vLDoSg_0024v8_hEGZOgGaUZwg();
		buildData.isMixedAction = binaryReader.ReadBoolean();
		buildData.actionProcessing = (BuildData.EMBMFGCDBAM)binaryReader.ReadByte();
		buildData.invMask = binaryReader.ReadInt32();
		buildData.sndDefName = binaryReader._vLDoSg_0024v8_hEGZOgGaUZwg();
		buildData.luaName = binaryReader._vLDoSg_0024v8_hEGZOgGaUZwg();
		buildData.crossName = binaryReader._vLDoSg_0024v8_hEGZOgGaUZwg();
		buildData.angleName = binaryReader._vLDoSg_0024v8_hEGZOgGaUZwg();
		buildData.rollName = binaryReader._vLDoSg_0024v8_hEGZOgGaUZwg();
		buildData.circleName = binaryReader._vLDoSg_0024v8_hEGZOgGaUZwg();
		buildData.circleOffset = binaryReader.ReadInt32();
		buildData.isEven = binaryReader.ReadBoolean();
		buildData.syncSpeed = binaryReader.ReadByte();
		buildData.syncSpeedA = binaryReader.ReadByte();
		buildData.followV = binaryReader.ReadByte();
		buildData.followH = binaryReader.ReadByte();
		buildData.pfID = binaryReader.ReadUInt64();
		buildData.isRedistributionAllowed = binaryReader.ReadBoolean();
		buildData.uploadTitle = binaryReader._vLDoSg_0024v8_hEGZOgGaUZwg();
		buildData.previewName = binaryReader._vLDoSg_0024v8_hEGZOgGaUZwg();
		buildData.tag0 = binaryReader._vLDoSg_0024v8_hEGZOgGaUZwg();
		buildData.tag1 = binaryReader._vLDoSg_0024v8_hEGZOgGaUZwg();
		buildData.tag2 = binaryReader._vLDoSg_0024v8_hEGZOgGaUZwg();
		buildData.tag3 = binaryReader._vLDoSg_0024v8_hEGZOgGaUZwg();
		buildData.size = binaryReader.ReadInt32();
		buildData.texName = binaryReader._vLDoSg_0024v8_hEGZOgGaUZwg();
		buildData.texHash = binaryReader.ReadInt32();
		buildData.texURL = binaryReader._vLDoSg_0024v8_hEGZOgGaUZwg();
		buildData.smokeFrame = binaryReader.ReadByte() - 1;
		buildData.orthoSize = binaryReader.ReadSingle();
		buildData.nearClip = binaryReader.ReadByte();
		buildData.isAntiPen = binaryReader.ReadBoolean();
		buildData.planName = binaryReader._vLDoSg_0024v8_hEGZOgGaUZwg();
		buildData.planX = binaryReader.ReadSingle();
		buildData.planY = binaryReader.ReadSingle();
		buildData.planZ = binaryReader.ReadSingle();
		buildData.planSize = binaryReader.ReadSingle();
		buildData.jointSta = binaryReader.ReadInt32();
		buildData.antiBou = binaryReader.ReadInt32();
		buildData.mirrorX = binaryReader.ReadByte();
		buildData.mirrorY = binaryReader.ReadByte();
		buildData.mirrorZ = binaryReader.ReadByte();
		buildData.surfaceType = (BuildData.LEGHEEKCJAF)binaryReader.ReadByte();
		buildData.antiSSAO = new int[4];
		buildData.antiSSAO[0] = binaryReader.ReadInt32();
		buildData.antiSSAO[1] = binaryReader.ReadInt32();
		buildData.antiSSAO[2] = binaryReader.ReadInt32();
		buildData.antiSSAO[3] = binaryReader.ReadInt32();
		buildData.gridColor = binaryReader.ReadInt32();
		buildData.isOriginalCollider = binaryReader.ReadBoolean();
		buildData.stabilizer = binaryReader.ReadInt32();
		buildData.magnification = binaryReader.ReadInt16();
		buildData.useScopeOrientation = binaryReader.ReadBoolean();
		buildData.isReady = binaryReader.ReadBoolean();
		int num2 = binaryReader.ReadInt32();
		for (int i = 0; i < num2; i++)
		{
			BlockData blockData = new BlockData();
			blockData.type = (BlockData.AAHMDBHDCDK)binaryReader.ReadByte();
			blockData.x = binaryReader.ReadByte() - 49;
			blockData.y = binaryReader.ReadByte();
			blockData.z = binaryReader.ReadByte() - 49;
			blockData.rgbI = binaryReader.ReadInt32();
			blockData.index = binaryReader.ReadInt32();
			uint num3 = binaryReader.ReadUInt32();
			blockData.actionID = new int[num3];
			blockData.actionParam = new int[num3];
			for (int j = 0; j < num3; j++)
			{
				blockData.actionID[j] = binaryReader.ReadInt32();
				blockData.actionParam[j] = binaryReader.ReadInt32();
			}
			blockData.flag = binaryReader.ReadBoolean();
			blockData.gid = binaryReader.ReadByte();
			blockData.press = binaryReader.ReadInt32();
			buildData.blockData.Add(blockData);
		}
		return buildData;
	}

	internal static byte[] TeFH4ifj99LlUm32GP4kcVk(BuildData buildData_0)
	{
		MemoryStream stream_ = smethod_10();
		BinaryWriter binaryWriter_ = smethod_11((Stream)stream_);
		smethod_12(binaryWriter_, 1);
		binaryWriter_.S5T_0024i6sgT114cTBqa5QJ_lg(buildData_0.version);
		smethod_13(binaryWriter_, buildData_0.physVersion);
		smethod_14(binaryWriter_, (short)buildData_0.speedLimit);
		smethod_15(binaryWriter_, buildData_0.isHardJoint);
		smethod_15(binaryWriter_, buildData_0.isTakeoffAssist);
		smethod_16(binaryWriter_, (byte)buildData_0.moverSound);
		smethod_16(binaryWriter_, (byte)buildData_0.thrusterSound);
		smethod_16(binaryWriter_, (byte)buildData_0.jointSound);
		smethod_15(binaryWriter_, buildData_0.isWalkSoundEnabled);
		smethod_15(binaryWriter_, buildData_0.isFoldSoundEnabled);
		smethod_15(binaryWriter_, buildData_0.isStretchSoundEnabled);
		smethod_15(binaryWriter_, buildData_0.isSmokeSoundEnabled);
		smethod_16(binaryWriter_, (byte)buildData_0.jointOrder);
		smethod_15(binaryWriter_, buildData_0.isPatchJoint);
		binaryWriter_.S5T_0024i6sgT114cTBqa5QJ_lg(buildData_0.imageName);
		smethod_13(binaryWriter_, buildData_0.offsetX);
		smethod_13(binaryWriter_, buildData_0.offsetZ);
		smethod_12(binaryWriter_, buildData_0.spawnAltOffset);
		binaryWriter_.S5T_0024i6sgT114cTBqa5QJ_lg(buildData_0.comment);
		smethod_15(binaryWriter_, buildData_0.isMixedAction);
		smethod_16(binaryWriter_, (byte)buildData_0.actionProcessing);
		smethod_12(binaryWriter_, buildData_0.invMask);
		binaryWriter_.S5T_0024i6sgT114cTBqa5QJ_lg(buildData_0.sndDefName);
		binaryWriter_.S5T_0024i6sgT114cTBqa5QJ_lg(buildData_0.luaName);
		binaryWriter_.S5T_0024i6sgT114cTBqa5QJ_lg(buildData_0.crossName);
		binaryWriter_.S5T_0024i6sgT114cTBqa5QJ_lg(buildData_0.angleName);
		binaryWriter_.S5T_0024i6sgT114cTBqa5QJ_lg(buildData_0.rollName);
		binaryWriter_.S5T_0024i6sgT114cTBqa5QJ_lg(buildData_0.circleName);
		smethod_12(binaryWriter_, buildData_0.circleOffset);
		smethod_15(binaryWriter_, buildData_0.isEven);
		smethod_16(binaryWriter_, (byte)buildData_0.syncSpeed);
		smethod_16(binaryWriter_, (byte)buildData_0.syncSpeedA);
		smethod_16(binaryWriter_, (byte)buildData_0.followV);
		smethod_16(binaryWriter_, (byte)buildData_0.followH);
		smethod_17(binaryWriter_, buildData_0.pfID);
		smethod_15(binaryWriter_, buildData_0.isRedistributionAllowed);
		binaryWriter_.S5T_0024i6sgT114cTBqa5QJ_lg(buildData_0.uploadTitle);
		binaryWriter_.S5T_0024i6sgT114cTBqa5QJ_lg(buildData_0.previewName);
		binaryWriter_.S5T_0024i6sgT114cTBqa5QJ_lg(buildData_0.tag0);
		binaryWriter_.S5T_0024i6sgT114cTBqa5QJ_lg(buildData_0.tag1);
		binaryWriter_.S5T_0024i6sgT114cTBqa5QJ_lg(buildData_0.tag2);
		binaryWriter_.S5T_0024i6sgT114cTBqa5QJ_lg(buildData_0.tag3);
		smethod_12(binaryWriter_, buildData_0.size);
		binaryWriter_.S5T_0024i6sgT114cTBqa5QJ_lg(buildData_0.texName);
		smethod_12(binaryWriter_, buildData_0.texHash);
		binaryWriter_.S5T_0024i6sgT114cTBqa5QJ_lg(buildData_0.texURL);
		smethod_16(binaryWriter_, (byte)(buildData_0.smokeFrame + 1));
		smethod_13(binaryWriter_, buildData_0.orthoSize);
		smethod_16(binaryWriter_, (byte)buildData_0.nearClip);
		smethod_15(binaryWriter_, buildData_0.isAntiPen);
		binaryWriter_.S5T_0024i6sgT114cTBqa5QJ_lg(buildData_0.planName);
		smethod_13(binaryWriter_, buildData_0.planX);
		smethod_13(binaryWriter_, buildData_0.planY);
		smethod_13(binaryWriter_, buildData_0.planZ);
		smethod_13(binaryWriter_, buildData_0.planSize);
		smethod_12(binaryWriter_, buildData_0.jointSta);
		smethod_12(binaryWriter_, buildData_0.antiBou);
		smethod_16(binaryWriter_, (byte)buildData_0.mirrorX);
		smethod_16(binaryWriter_, (byte)buildData_0.mirrorY);
		smethod_16(binaryWriter_, (byte)buildData_0.mirrorZ);
		smethod_16(binaryWriter_, (byte)buildData_0.surfaceType);
		smethod_12(binaryWriter_, buildData_0.antiSSAO[0]);
		smethod_12(binaryWriter_, buildData_0.antiSSAO[1]);
		smethod_12(binaryWriter_, buildData_0.antiSSAO[2]);
		smethod_12(binaryWriter_, buildData_0.antiSSAO[3]);
		smethod_12(binaryWriter_, buildData_0.gridColor);
		smethod_15(binaryWriter_, buildData_0.isOriginalCollider);
		smethod_12(binaryWriter_, buildData_0.stabilizer);
		smethod_14(binaryWriter_, (short)buildData_0.magnification);
		smethod_15(binaryWriter_, buildData_0.useScopeOrientation);
		smethod_15(binaryWriter_, buildData_0.isReady);
		smethod_12(binaryWriter_, buildData_0.blockData.Count);
		foreach (BlockData blockDatum in buildData_0.blockData)
		{
			smethod_16(binaryWriter_, (byte)blockDatum.type);
			smethod_16(binaryWriter_, (byte)(blockDatum.x + 49));
			smethod_16(binaryWriter_, (byte)blockDatum.y);
			smethod_16(binaryWriter_, (byte)(blockDatum.z + 49));
			smethod_12(binaryWriter_, blockDatum.rgbI);
			smethod_12(binaryWriter_, blockDatum.index);
			smethod_18(binaryWriter_, (uint)blockDatum.actionID.Length);
			for (int i = 0; i < blockDatum.actionID.Length; i++)
			{
				smethod_12(binaryWriter_, blockDatum.actionID[i]);
				smethod_12(binaryWriter_, blockDatum.actionParam[i]);
			}
			smethod_15(binaryWriter_, blockDatum.flag);
			smethod_16(binaryWriter_, (byte)blockDatum.gid);
			smethod_12(binaryWriter_, blockDatum.press);
		}
		smethod_8((Stream)stream_, 0L, SeekOrigin.Begin);
		return Y0682_EmDASRYkXiQKRSXHE.Hnc3QYGDRf1W8TRMMLwwZ48(H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.lD8ilO2D57aRcy0P8Iq13zE(stream_), 3);
	}

	internal static void smethod_1(BinaryWriter binaryWriter_0, string string_0)
	{
		binaryWriter_0.Write(string_0);
	}

	internal static string smethod_2(BinaryReader binaryReader_0)
	{
		return binaryReader_0.ReadString();
	}

	internal static bool smethod_3(string string_0, string string_1)
	{
		return string_0 == string_1;
	}

	internal static BuildData smethod_4(bool bool_0)
	{
		return new BuildData(bool_0);
	}

	internal static MemoryStream smethod_5(byte[] byte_0)
	{
		return new MemoryStream(byte_0);
	}

	internal static BinaryReader smethod_6(Stream stream_0)
	{
		return new BinaryReader(stream_0);
	}

	internal static Stream smethod_7(BinaryReader binaryReader_0)
	{
		return binaryReader_0.BaseStream;
	}

	internal static long smethod_8(Stream stream_0, long long_0, SeekOrigin seekOrigin_0)
	{
		return stream_0.Seek(long_0, seekOrigin_0);
	}

	internal static int smethod_9(BinaryReader binaryReader_0)
	{
		return binaryReader_0.ReadInt32();
	}

	internal static MemoryStream smethod_10()
	{
		return new MemoryStream();
	}

	internal static BinaryWriter smethod_11(Stream stream_0)
	{
		return new BinaryWriter(stream_0);
	}

	internal static void smethod_12(BinaryWriter binaryWriter_0, int int_0)
	{
		binaryWriter_0.Write(int_0);
	}

	internal static void smethod_13(BinaryWriter binaryWriter_0, float float_0)
	{
		binaryWriter_0.Write(float_0);
	}

	internal static void smethod_14(BinaryWriter binaryWriter_0, short short_0)
	{
		binaryWriter_0.Write(short_0);
	}

	internal static void smethod_15(BinaryWriter binaryWriter_0, bool bool_0)
	{
		binaryWriter_0.Write(bool_0);
	}

	internal static void smethod_16(BinaryWriter binaryWriter_0, byte byte_0)
	{
		binaryWriter_0.Write(byte_0);
	}

	internal static void smethod_17(BinaryWriter binaryWriter_0, ulong ulong_0)
	{
		binaryWriter_0.Write(ulong_0);
	}

	internal static void smethod_18(BinaryWriter binaryWriter_0, uint uint_0)
	{
		binaryWriter_0.Write(uint_0);
	}
}
