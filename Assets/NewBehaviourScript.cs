// NewBehaviourScript.cs
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
	// Inspectorに表示される
	public int intValue;
	// Inspectorに表示される
	[SerializeField]
	private int intValue2;
	
	// Inspectorに表示されない
	private int intValue3;
	
	// Inspectorに表示される
	public SerializableClass serializable = new SerializableClass() { name = "hoge" };
	
	// Inspectorに表示されない
	public NonSerializableClass nonSerializable = new NonSerializableClass() { name = "moge" };
}

// Inspectorに表示できるクラス
[System.Serializable]
public class SerializableClass
{
	public string name;
}

// Inspectorに表示できないクラス
public class NonSerializableClass
{
	public string name;
}
