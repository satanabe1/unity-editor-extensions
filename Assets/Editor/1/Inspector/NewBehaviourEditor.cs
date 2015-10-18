// Editor/NewBehaviourEditor.cs
using UnityEditor;
[CustomEditor(typeof(NewBehaviourScript))]
public class NewBehaviourEditor : Editor
{
	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();
		// targetで、現在選択中のオブジェクトを取得する
		// targetには、選択中のオブジェクトにアタッチされているコンポーネントが入っている
		NewBehaviourScript targetComponent = (NewBehaviourScript)target;
		EditorGUILayout.LabelField ("nonSerializable.name", targetComponent.nonSerializable.name);
	}
}
