using UnityEditor;

public class MyEditorWindow : EditorWindow
{
	[MenuItem("Window/MyEditorWindow")]
	public static void OpenWindow()
	{
		EditorWindow.GetWindow<MyEditorWindow>();
	}
}
