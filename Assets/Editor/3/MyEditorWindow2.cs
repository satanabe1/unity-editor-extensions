using UnityEditor;

public class MyEditorWindow2 : EditorWindow
{
	[MenuItem("Window/MyEditorWindow2")]
	public static void OpenWindow()
	{
		MyEditorWindow2 window = EditorWindow.CreateInstance<MyEditorWindow2>();
		window.Show();
	}
}
