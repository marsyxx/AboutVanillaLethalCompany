using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace LCCompatFixes;

internal static class RtlcFast
{
	private static ManualLogSource log;

	private static bool validate;

	private static bool loggedFirst;

	private static bool skipCompressed;

	private static int validated;

	private static int mismatches;

	private static int failures;

	private static readonly Dictionary<MethodBase, FieldInfo> bundleFields = new Dictionary<MethodBase, FieldInfo>();

	internal static void Start(Harmony harmony, ManualLogSource logger, bool validateResults, bool skipNonReadable)
	{
		log = logger;
		validate = validateResults;
		skipCompressed = skipNonReadable;
		Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
		foreach (Assembly assembly in assemblies)
		{
			string name = assembly.GetName().Name;
			if (name != "RTLC" && name != "RTLC.Mods")
			{
				continue;
			}
			Type[] source;
			try
			{
				source = assembly.GetTypes();
			}
			catch (ReflectionTypeLoadException ex)
			{
				source = ex.Types.Where((Type t) => t != null).ToArray();
			}
			Type type = source.FirstOrDefault((Type t) => t.Name == "BundleLoader");
			MethodInfo methodInfo = ((type != null) ? AccessTools.Method(type, "TrySetTexture") : null);
			FieldInfo fieldInfo = ((type != null) ? AccessTools.Field(type, "RTLCBundle") : null);
			if (methodInfo == null || fieldInfo == null || fieldInfo.FieldType != typeof(AssetBundle))
			{
				log.LogInfo(name + ": TrySetTexture / RTLCBundle not found; the fast texture copy is off.");
				continue;
			}
			bundleFields[methodInfo] = fieldInfo;
			harmony.Patch(methodInfo, new HarmonyMethod(typeof(RtlcFast), "Prefix"));
			log.LogInfo(name + ": texture replacement uses a raw pixel copy where it can.");
		}
	}

	private static bool Prefix(ref Texture2D __0, string __1, ref bool __result, MethodBase __originalMethod)
	{
		try
		{
			Texture2D texture2D = __0;
			if (texture2D == null || string.IsNullOrEmpty(__1) || !bundleFields.TryGetValue(__originalMethod, out var value))
			{
				return true;
			}
			if (skipCompressed && !texture2D.isReadable)
			{
				__result = false;
				return false;
			}
			AssetBundle assetBundle = value.GetValue(null) as AssetBundle;
			if (assetBundle == null)
			{
				return true;
			}
			Texture2D texture2D2 = assetBundle.LoadAsset<Texture2D>(__1);
			if (texture2D2 == null)
			{
				__result = false;
				return false;
			}
			if (!texture2D2.isReadable || !texture2D.isReadable)
			{
				return true;
			}
			TextureFormat format = texture2D2.format;
			if (format != TextureFormat.RGBA32 && format != TextureFormat.ARGB32 && format != TextureFormat.BGRA32 && format != TextureFormat.RGB24)
			{
				return true;
			}
			if (texture2D2.mipmapCount != 1 || texture2D.mipmapCount != 1)
			{
				return true;
			}
			if (validate && validated < 40)
			{
				Compare(texture2D, texture2D2, __1);
			}
			else
			{
				Copy(texture2D, texture2D2);
			}
			__result = true;
			if (!loggedFirst)
			{
				loggedFirst = true;
				log.LogInfo("RTLC fast texture copy in use (first: '" + __1 + "', " + texture2D2.width + "x" + texture2D2.height + " " + format.ToString() + ").");
			}
			return false;
		}
		catch (Exception ex)
		{
			if (++failures <= 3)
			{
				log.LogWarning("RTLC fast texture copy failed, RTLC's own code runs instead: " + ex.Message + ((failures == 3) ? " (further failures are not logged)" : ""));
			}
			return true;
		}
	}

	private static void Copy(Texture2D target, Texture2D source)
	{
		if (target.width != source.width || target.height != source.height || target.format != source.format)
		{
			target.Reinitialize(source.width, source.height, source.format, hasMipMap: false);
		}
		target.SetPixelData(source.GetRawTextureData<byte>(), 0);
		target.Apply(updateMipmaps: false, makeNoLongerReadable: false);
		target.mipMapBias = -1f;
	}

	private static void Compare(Texture2D target, Texture2D source, string name)
	{
		Texture2D texture2D = new Texture2D(2, 2, TextureFormat.RGBA32, mipChain: false);
		texture2D.LoadImage(source.EncodeToPNG());
		Copy(target, source);
		Color32[] pixels = texture2D.GetPixels32();
		Color32[] pixels2 = target.GetPixels32();
		int num = 0;
		int num2 = 0;
		if (pixels.Length != pixels2.Length)
		{
			num = -1;
		}
		else
		{
			for (int i = 0; i < pixels.Length; i++)
			{
				int num3 = Math.Max(Math.Max(Math.Abs(pixels[i].r - pixels2[i].r), Math.Abs(pixels[i].g - pixels2[i].g)), Math.Max(Math.Abs(pixels[i].b - pixels2[i].b), Math.Abs(pixels[i].a - pixels2[i].a)));
				if (num3 > 0)
				{
					num++;
				}
				if (num3 > num2)
				{
					num2 = num3;
				}
			}
		}
		validated++;
		if (num != 0)
		{
			mismatches++;
		}
		string text = ((num == 0) ? "identical" : ((num < 0) ? "size differs" : (num + " pixels differ, worst " + num2)));
		log.LogInfo("RTLC fast copy check '" + name + "' " + source.width + "x" + source.height + " " + source.format.ToString() + ": " + text + " (" + mismatches + " mismatches of " + validated + ").");
		UnityEngine.Object.Destroy(texture2D);
	}
}
