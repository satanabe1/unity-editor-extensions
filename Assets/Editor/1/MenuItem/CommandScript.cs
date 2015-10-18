using UnityEngine;
using UnityEditor;

public static class CommandScript
{
	[MenuItem("MyCommand/HelloWorld!")]
	public static void HellowWorld ()
	{
		Debug.Log ("HelloWorld!");
	}

	[MenuItem("MyCommand/Open MyScene")]
	public static void OpenMyScene()
	{
		UnityEditor.EditorApplication.OpenScene("MyScene"); // シーンを開く。UnityEngine空間やUnityEditor空間のAPIも使えます。
	}
}
