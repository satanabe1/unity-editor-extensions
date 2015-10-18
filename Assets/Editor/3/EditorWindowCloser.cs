using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class EditorWindowCloser : EditorWindow
{
	[MenuItem("Window/EditorWindowCloser")]
	public static void OpenWindow()
	{
		EditorWindow.GetWindow<EditorWindowCloser>();
	}
	
	Vector2 _scrollPosition;
	
	void OnGUI()
	{
		_scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);
		foreach (var window in Resources.FindObjectsOfTypeAll<EditorWindow>())
		{
			if (GUILayout.Button("Close " + window.GetType().Name))
			{
				window.Close();
			}
		}
		EditorGUILayout.EndScrollView();
	}
}
