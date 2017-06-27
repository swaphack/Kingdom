using UnityEngine;

/// <summary>
/// 阵型制作
/// </summary>
public class FormationMake : EditorMake
{
	/// <summary>
	/// 创建一个对象
	/// </summary>
	/// <returns>The game object.</returns>
	private GameObject CreateGameObject() {
		return CreateGameObject (PrimitiveType.Sphere);
	}

	/// <summary>
	/// 保存
	/// </summary>
	public override void Save() 
	{
		int childCount = this.transform.childCount;
		if (childCount == 0) {
			Debug.Log ("Node must has child!");
			return;
		}

		ByteWriter writer = new ByteWriter (512);
		writer.Write (childCount);
		for (int i = 0; i < childCount; i++) {
			Vector3 pos = this.transform.GetChild(i).position;
			writer.Write (pos);
		}
		SaveData (writer);
	} 

	/// <summary>
	/// 加载
	/// </summary>
	public override void Load() 
	{
		ByteReader reader = LoadData();
		if (reader == null) {
			return;
		}

		this.RemoveAllChildren ();

		int childCount = reader.Read<int> ();
		for (int i = 0; i < childCount; i++) {
			GameObject go = CreateGameObject ();
			Vector3 pos = reader.ReadVector3 ();
			go.transform.position = pos;
		}

		Debug.Log ("Successful load!");
	}
}

