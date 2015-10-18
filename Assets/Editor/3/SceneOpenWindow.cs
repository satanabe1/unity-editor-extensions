using UnityEngine;
using UnityEditor;

public class SceneOpenWindow : EditorWindow
{
	[MenuItem("Window/SceneOpenWindow")]
	public static void OpenWindow ()
	{
		EditorWindow.GetWindow<SceneOpenWindow> ();
	}
	
	private string[] _scenePaths;
	
	private void OnEnable ()
	{
		EditorApplication.projectWindowItemOnGUI += RefreshSceneList;
		RefreshSceneList (string.Empty, new Rect ());
	}
	
	private void OnDisable ()
	{
		EditorApplication.projectWindowItemOnGUI -= RefreshSceneList;
	}
	
	private void OnGUI ()
	{
		foreach (var scenePath in _scenePaths) {
			if (GUILayout.Button (scenePath)) {
				EditorApplication.OpenScene (scenePath);
			}
		}
		EditorGUILayout.Space ();
		if (GUILayout.Button ("New Scene")) {
			EditorApplication.NewScene ();
		}
	}
	
	private void RefreshSceneList (string unusedArg1, Rect unusedArg2)
	{
		string[] guids = AssetDatabase.FindAssets ("t:Scene");
		_scenePaths = new string[guids.Length];
		for (var i = 0; i < guids.Length; ++i) {
			_scenePaths [i] = AssetDatabase.GUIDToAssetPath (guids [i]);
		}
		Repaint();
	}
}
