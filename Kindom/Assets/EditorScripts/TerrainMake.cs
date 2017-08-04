using UnityEngine;
using Geography.Terrian;
using Common.Utility;
using Common.CG;

/// <summary>
/// 地形制作
/// </summary>
public class TerrainMake : EditorMake
{
	/// <summary>
	/// 随机种子
	/// </summary>
	public int Seed = 512;
	/// <summary>
	/// 宽度
	/// </summary>
	public int Width = 20;
	/// <summary>
	/// 长度
	/// </summary>
	public int Length = 20;
	/// <summary>
	/// 持久度
	/// </summary>
	public float Persistence = 3;
	/// <summary>
	/// 倍数
	/// </summary>
	public int Octaves = 5;

	/// <summary>
	/// 创建一个对象
	/// </summary>
	/// <returns>The game object.</returns>
	public GameObject CreateGameObject ()
	{
		GameObject go = CreateGameObject (PrimitiveType.Cube);
		go.AddComponent<Cube> ();
		return go;
	}

	/// <summary>
	/// 保存
	/// </summary>
	public override void Save ()
	{
		int childCount = this.transform.childCount;
		if (childCount == 0) {
			Debug.Log ("Node must has child!");
			return;
		}

		ByteWriter writer = new ByteWriter (512);
		writer.Write (childCount);
		for (int i = 0; i < childCount; i++) {
			Vector3 pos = this.transform.GetChild (i).position;
			writer.Write (pos);
			//Cube
			//writer.Write (0);
		}
		SaveData (writer);
	}

	/// <summary>
	/// 加载
	/// </summary>
	public override void Load ()
	{
		ByteReader reader = LoadData ();
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

	/// <summary>
	/// 生成地图
	/// </summary>
	public void Generate ()
	{
		RemoveAllChildren ();
		Transform parent = this.transform;

		Vector3 pos = Vector3.zero;
		float h = 0;

		PerlinNoise.Seed = Seed;

		h = PerlinNoise.PerlinNoise2D (0, 0, Persistence, Octaves);
		float minH = h;
		float maxH = h;

		for (int i = 0; i < Width; i++) {
			for (int j = 0; j < Length; j++) {
				h = PerlinNoise.PerlinNoise2D (i, j, Persistence, Octaves);
				pos.x = i;
				pos.y = h;
				pos.z = j;
				GameObject go = CreateGameObject ();
				go.transform.position = pos;
				go.transform.SetParent (parent);
				maxH = Mathf.Max (h, maxH);
				minH = Mathf.Min (h, minH);
			}
		}

		float avgH = (maxH + minH) * 0.5f;
		int childrenCount = parent.childCount;
		for (int i = 0; i < childrenCount; i++) {
			Transform transform = parent.GetChild (i);
			pos = transform.position;
			pos.y -= avgH;
			pos.y = (int)pos.y;
			transform.position = pos;
		}
	}
}