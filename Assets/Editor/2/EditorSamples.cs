using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class EditorSamples
{

	/// <summary>
	/// リスト11 Prefab一覧を取得するスクリプト
	/// </summary>
	[MenuItem("MyCommand/Find Prefabs")]
	public static void FindPrefabs ()
	{
		// まずはPrefabのGUIDの一覧を取得する
		string[] guids = AssetDatabase.FindAssets ("t:Prefab", new [] { "Assets" });
		foreach (var guid in guids) {
			// GUIDからPrefabのパスを取得する
			string prefabPath = AssetDatabase.GUIDToAssetPath (guid);
			Debug.Log (prefabPath);
		}
	}

	/// <summary>
	/// リスト12 アセットのパスを直接検索するためのメソッドFindAssetPaths
	/// AssetDatabase.FindAssetsにfilterとsearchInFoldersを渡し、帰ってきたGUID配列をパスに変換して返す関数
	/// </summary>
	public static string[] FindAssetPaths(string filter, params string[] searchInFolders)
	{
		string[] guids = AssetDatabase.FindAssets(filter, searchInFolders);
		string[] paths = new string[guids.Length];
		for(var i = 0; i < guids.Length; ++i)
		{
			paths[i] = AssetDatabase.GUIDToAssetPath(guids[i]);
		}
		return paths;
	}

	/// <summary>
	/// リスト13 すべてのシーンからアクティブなGameObjectを取得するスクリプト
	/// </summary>
	public static List<GameObject> FindGameObjectsInScenes ()
	{
		// シーン一覧のGUIDを取得
		string[] guids = AssetDatabase.FindAssets ("t:Scene", new [] { "Assets" });
		var list = new List<GameObject>();
		foreach (var guid in guids) {
			// GUIDからアセットのパスを取得する
			string scenePath = AssetDatabase.GUIDToAssetPath (guid);
			// 取得したシーンのパスを使って作業シーンを開く
			EditorApplication.OpenScene (scenePath);
			// シーン中の全てのアクティブなオブジェクトを取得する
			GameObject[] gameObjects = GameObject.FindObjectsOfType<GameObject> ();
			list.AddRange(gameObjects);
		}
		return list;
	}

	/// <summary>
	/// リスト14 現在のシーンからすべてのGameObjectを取得するスクリプト
	/// </summary>
	public static List<T> FindObjectsInCurrentScene<T> () where T : Object
	{
		// Unityエディタ上に読み込まれているすべてのオブジェクトを取得する
		T[] loadedObjects = Resources.FindObjectsOfTypeAll<T> ();
		var list = new List<T> (loadedObjects.Length);
		foreach (T obj in loadedObjects) {
			// AssetDatabaseからオブジェクトの存在するパスを取得する
			string path = AssetDatabase.GetAssetOrScenePath (obj);
			// オブジェクトのパスが、シーンの拡張子であれば、それはシーンに直置きされているオブジェクトということになる
			if (path.EndsWith (".unity")) {
				list.Add (obj);
			}
		}
		return list;
	}

	/// <summary>
	/// リスト15(0) Missingを検出するスクリプト
	/// </summary>
	[MenuItem("Assets/Find Missing Scripts")]
	public static void SearchAllPrefabs ()
	{
		string[] guids = AssetDatabase.FindAssets ("t:Prefab", new [] { "Assets" });
		foreach (var guid in guids) {
			string path = AssetDatabase.GUIDToAssetPath (guid);
			GameObject go = AssetDatabase.LoadAssetAtPath (path, typeof(GameObject)) as GameObject;
			if (HasMissingScript (go)) {
				Debug.LogError (go.name, go);
			}
		}
	}

	/// <summary>
	/// リスト15(1) Missingを検出するスクリプト
	/// </summary>
	public static bool HasMissingScript (GameObject go)
	{
		Component[] components = go.GetComponentsInChildren<Component> (true);
		for (int k = 0; k < components.Length; k++) {
			if (components [k] == null) {
				return true;
			}
		}
		return false;
	}
}
