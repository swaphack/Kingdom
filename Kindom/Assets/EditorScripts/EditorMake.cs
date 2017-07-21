using UnityEngine;
using System.Collections;
using System.IO;
using Common.Utility;

/// <summary>
/// 编辑制作
/// </summary>
public class EditorMake : MonoBehaviour
{
	/// <summary>
	/// 路径
	/// </summary>
	public string Url;
	/// <summary>
	/// 路径
	/// </summary>
	/// <value>The path.</value>
	public string Path {
		get {
			return Application.dataPath + "/" + Url;		
		}
	}

	/// <summary>
	/// 创建一个对象
	/// </summary>
	/// <returns>The game object.</returns>
	/// <param name="type">Type.</param>
	public GameObject CreateGameObject(PrimitiveType type) {
		GameObject go = GameObject.CreatePrimitive (type);
		go.transform.SetParent (this.transform);
		return go;
	}

	/// <summary>
	/// 移除所有子节点
	/// </summary>
	public void RemoveAllChildren() {
		int count = this.transform.childCount;
		for (int i = count - 1; i >= 0; i--) {
			Transform child = this.transform.GetChild (i);
			DestroyImmediate (child.gameObject);
		}
	}

	/// <summary>
	/// 保存数据
	/// </summary>
	/// <param name="writer">Writer.</param>
	public void SaveData(ByteWriter writer)
	{
		if (writer == null) {
			return;
		}

		if (string.IsNullOrEmpty (Url)) {
			Debug.Log ("Filename is Empty!");
			return;
		}

		byte[] bytes = writer.ToArray ();
		FileStream fileStream = new FileStream (Path, FileMode.Create, FileAccess.Write);
		fileStream.Write (bytes, 0, bytes.Length);
		fileStream.Close ();
		writer.Close ();

		Debug.Log ("Successful save!");
	}

	/// <summary>
	/// 加载数据
	/// </summary>
	public ByteReader LoadData() {
		if (string.IsNullOrEmpty (Url)) {
			Debug.Log ("Filename is empty!");
			return null;
		}

		if (!File.Exists (Path)) {
			Debug.Log ("Filepath don't exists!");
			return null;
		}

		FileStream fileStream = new FileStream (Path, FileMode.Open, FileAccess.Read);
		if (fileStream == null || fileStream.Length == 0) {
			Debug.Log ("Filepath don't exists!");
			return null;
		}

		int length = (int)fileStream.Length;
		byte[] bytes = new byte[length];
		int count = fileStream.Read (bytes, 0, length);
		if (count <= 0) {
			Debug.Log ("File is empty!");
			return null;
		}

		return new ByteReader (bytes);
	}

	/// <summary>
	/// 加载
	/// </summary>
	public virtual void Load() {}
	/// <summary>
	/// 保存
	/// </summary>
	public virtual void Save() {}
}

