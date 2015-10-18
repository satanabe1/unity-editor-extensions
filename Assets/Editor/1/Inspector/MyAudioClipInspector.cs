using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(AudioClip))]
public class MyAudioClipInspector : Editor
{
	private AudioSource audioSource;
	// 選択中のAudioClipを再生する
	private void PlayClip ()
	{
		DisposeAudioSource ();
		AudioClip clip = target as AudioClip;
		GameObject tmpObj = new GameObject (clip.name);
		audioSource = tmpObj.AddComponent<AudioSource> ();
		tmpObj.hideFlags = HideFlags.DontSave;
		// tmpObj.hideFlags = HideFlags.HideAndDontSave; // ヒエラルキー上にも表示したくない場合はこちらを使う
		audioSource.clip = clip;
		audioSource.Play ();
		EditorApplication.update += CheckAndDispose; // 毎フレーム再生状態を確認する
	}
	// AudioClipを試聴するために一時的に作成したオブジェクトを破棄する
	private void DisposeAudioSource ()
	{
		if (audioSource != null) {
			DestroyImmediate (audioSource.gameObject, false);
			audioSource = null;
		}
	}
	// AudioClipが選択状態でなくなったら再生を終了する
	private void OnDisable ()
	{
		if (audioSource != null) {
			audioSource.Stop ();
			CheckAndDispose ();
		}
	}
	// AudioClipが再生中か判定し、再生していなければ、一時的に作成したオブジェクトを破棄する
	private void CheckAndDispose ()
	{
		if (audioSource == null) {
			return;
		}
		if (audioSource.isPlaying == false) {
			DisposeAudioSource ();
			EditorApplication.update -= CheckAndDispose;
		}
	}
	// カスタムエディタを表示する
	public override void OnInspectorGUI ()
	{
		GUI.enabled = true; // AudioClipのインスペクタには、標準ではGUI.enabledにfalseが入っている
		if (GUILayout.Button ("Play")) {
			PlayClip ();
		}
		base.OnInspectorGUI ();
		GUI.enabled = false;
	}
}
