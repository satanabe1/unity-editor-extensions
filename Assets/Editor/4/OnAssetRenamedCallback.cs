using UnityEngine;
using UnityEditor;

/// <summary>
/// 名前入力が完了した時点で呼び出されるコールバック
/// ProjectWindowCallback.EndNameEditActionを継承して定義する
/// </summary>
public class OnAssetRenamedCallback : UnityEditor.ProjectWindowCallback.EndNameEditAction
{
	private System.Action<string> _editNameCallback;

	public static OnAssetRenamedCallback CreateEventInstance (System.Action<string> editNameCallback)
	{
		var renamedEvent = ScriptableObject.CreateInstance<OnAssetRenamedCallback> ();
		renamedEvent._editNameCallback = editNameCallback;
		return renamedEvent;
	}

	// 名前入力完了時に呼び出されるコールバックの実体
	public override void Action (int instanceId, string pathName, string resourceFile)
	{
		if (_editNameCallback != null) {
			_editNameCallback.Invoke (pathName);
		}
	}
}

public class OnAssetRenamedCallbackUsage
{
	/// <summary>
	/// 実際に名前入力モードに入るためのメソッド
	/// </summary>
	public static void EditAssetName (string defaultName, Texture2D icon, System.Action<string> onEditName)
	{
		if (onEditName == null) {
			return;
		}
		OnAssetRenamedCallback renamedCallback = OnAssetRenamedCallback.CreateEventInstance (onEditName);
		UnityEditor.ProjectWindowUtil.StartNameEditingIfProjectWindowExists (0, renamedCallback, defaultName, icon, string.Empty);
	}
}
