using UnityEngine;
using UnityEditor;

public class ClockWindow : EditorWindow
{
	[MenuItem("Window/ClockWindow")]
	public static void OpenWindow ()
	{
		EditorWindow.GetWindow<ClockWindow> ();
	}
	
	void OnEnable ()
	{
		titleContent = new GUIContent("時計");
	}
	
	void OnGUI ()
	{
		EditorGUILayout.LabelField (System.DateTime.Now.ToLongDateString ());
		EditorGUILayout.LabelField (System.DateTime.Now.ToLongTimeString ());
	}
}
