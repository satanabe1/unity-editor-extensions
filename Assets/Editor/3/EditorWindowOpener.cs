using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class EditorWindowOpener : EditorWindow
{
	[MenuItem("Window/EditorWindowOpener")]
	public static void OpenWindow ()
	{
		EditorWindow.GetWindow<EditorWindowOpener> ();
	}
	
	List<System.Type> windowTypeList;
	Vector2 scrollPos;
	
	void PickUpEditorWindow (System.Type[] types)
	{
		foreach (var type in types) {
			if (typeof(EditorWindow).IsAssignableFrom (type)) {
				windowTypeList.Add (type);
			}
		}
	}
	
	void OnEnable ()
	{
		windowTypeList = new List<System.Type> ();
		PickUpEditorWindow (typeof(EditorWindow).Assembly.GetTypes ());
		PickUpEditorWindow (this.GetType ().Assembly.GetTypes ());
	}
	
	void OnGUI ()
	{
		scrollPos = EditorGUILayout.BeginScrollView (scrollPos);
		foreach (var editorWindow in windowTypeList) {
			if (GUILayout.Button (editorWindow.Name)) {
				EditorWindow window = EditorWindow.CreateInstance (editorWindow) as EditorWindow;
				window.Show ();
			}
		}
		EditorGUILayout.EndScrollView ();
	}
}