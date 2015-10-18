using UnityEngine;
using UnityEditor;
using System.Collections;

public class CommandScript4
{
	public static string GetScriptPath (System.Type type)
	{
		string[] guids = AssetDatabase.FindAssets (type.Name);
		foreach (string guid in guids) {
			string assetPath = AssetDatabase.GUIDToAssetPath (guid);
			if (assetPath.EndsWith (type.Name + ".cs")) {
				MonoScript script = AssetDatabase.LoadAssetAtPath (assetPath, typeof(MonoScript)) as MonoScript;
				if ((script != null) && (script.GetClass () == type)) {
					return assetPath;
				}
			}
		}
		return string.Empty;
	}
}
