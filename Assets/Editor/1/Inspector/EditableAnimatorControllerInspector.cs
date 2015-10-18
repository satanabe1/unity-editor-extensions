using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(UnityEditor.Animations.AnimatorController))]
public class EditableAnimatorControllerInspector : Editor
{
	private List<UnityEditor.Animations.AnimatorState> stateList;
	private List<UnityEngine.AnimatorControllerParameter> parameterList;
	
	private void OnEnable ()
	{
		var animatorController = target as UnityEditor.Animations.AnimatorController;
		// ステートを取得する
		stateList = FindAnimationClips (animatorController);
		stateList.Sort ((state1, state2) => state1.name.CompareTo (state2.name));
		// パラメータを取得する
		parameterList = new List<AnimatorControllerParameter> (animatorController.parameters);
	}
	
	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();
		EditorGUILayout.LabelField ("AnimationClips:");
		EditorGUI.BeginChangeCheck (); // インスペクタへの操作を監視する
		if (stateList != null) {
			EditorGUI.indentLevel++;
			foreach (var state in stateList) {
				// 以降、横並びにGUIを描画する
				EditorGUILayout.BeginHorizontal ();
				// まず、ステート名を表示する
				EditorGUILayout.PrefixLabel (state.name);
				// ステート名の横にAnimationClipを表示する
				Object clip = EditorGUILayout.ObjectField (state.motion as AnimationClip, typeof(AnimationClip), false);
				if (clip != state.motion) {
					state.motion = clip as Motion;
				}
				EditorGUILayout.EndHorizontal ();
				// 横並びを解除する
			}
			EditorGUI.indentLevel--;
		}
		EditorGUILayout.Space ();
		
		// パラメータを表示する
		EditorGUILayout.LabelField ("Parameters:");
		EditorGUI.indentLevel++;
		foreach (var param in parameterList) {
			EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.PrefixLabel (param.name);
			// パラメータの種類毎に、表示の仕方を変える
			switch (param.type) {
			case AnimatorControllerParameterType.Bool: // BoolはToggleボタン
				param.defaultBool = EditorGUILayout.Toggle (param.defaultBool);
				break;
			case AnimatorControllerParameterType.Float:
				param.defaultFloat = EditorGUILayout.FloatField (param.defaultFloat);
				break;
			case AnimatorControllerParameterType.Int:
				param.defaultInt = EditorGUILayout.IntField (param.defaultInt);
				break;
			case AnimatorControllerParameterType.Trigger: // TriggerもToggle
				GUI.enabled = false;
				EditorGUILayout.Toggle (false);
				GUI.enabled = true;
				break;
			}
			EditorGUILayout.EndHorizontal ();
		}
		EditorGUI.indentLevel--;
		if (EditorGUI.EndChangeCheck ()) {
			// インスペクタの値が変更されていたら、アセットを更新する
			EditorUtility.SetDirty (target);
		}
	}
	
	// AnimatorController内のステートを取得する
	private List<UnityEditor.Animations.AnimatorState> FindAnimationClips (
		UnityEditor.Animations.AnimatorController animatorController)
	{
		var stateList = new List<UnityEditor.Animations.AnimatorState> ();
		foreach (var layer in animatorController.layers) {
			foreach (var state in layer.stateMachine.states) {
				stateList.Add (state.state);
			}
		}
		return stateList;
	}
}
