using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

//[CustomEditor(typeof(UnityEditor.Animations.AnimatorController))]
public class AnimatorControllerInspector : Editor
{
	private List<AnimationClip> animationClipList;
	
	private void OnEnable ()
	{
		// AnimatorControllerが選択されたら、AnimationClipへの参照を取得する
		animationClipList = FindAnimationClips (target);
		animationClipList.Sort ((clip1, clip2) => clip1.name.CompareTo (clip2.name)); //　sort
	}
	
	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();
		if (animationClipList != null) {
			EditorGUILayout.LabelField ("AnimationClips:");
			EditorGUI.indentLevel++; // インデントを一段付ける
			for (var i = 0; i < animationClipList.Count; ++i) {
				// AnimationClipへの参照が存在したら、インスペクタ上に表示する
				EditorGUILayout.ObjectField (animationClipList [i], typeof(AnimationClip), false);
			}
			EditorGUI.indentLevel--; // インデントを戻す
		}
	}
	// assetがAnimatorControllerだったのならば、そのAnimatorControllerが参照するAnimationClipを取得する
	private List<AnimationClip> FindAnimationClips (Object asset)
	{
		var animationClipList = new List<AnimationClip> ();
		var animatorController = asset as UnityEditor.Animations.AnimatorController;
		if (animatorController != null)
		{
			// animationClipsから、そのAnimatorControllerが参照するAnimationClipを取得する
			animationClipList.AddRange(animatorController.animationClips);
		}
		return animationClipList;
	}
}
