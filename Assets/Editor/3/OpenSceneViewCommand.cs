using UnityEngine;
using UnityEditor;
using System.Collections;

public class OpenSceneViewCommand
{
	[MenuItem("Window/MultiOpen")]
	static void OpenSceneView ()
	{
		SceneView sceneView = EditorWindow.CreateInstance<SceneView> ();
		sceneView.Show ();
	}
}
