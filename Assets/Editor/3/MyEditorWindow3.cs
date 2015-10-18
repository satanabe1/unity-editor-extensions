using UnityEditor;

public class MyEditorWindow3 : EditorWindow
{
	private static MyEditorWindow3 window;

	[MenuItem("Window/MyEditorWindow3")]
	public static void OpenWindow ()
	{
		if (window == null) {
			window = EditorWindow.CreateInstance<MyEditorWindow3> ();
		}
		window.Show ();
	}
}
