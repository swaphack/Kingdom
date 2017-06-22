using UnityEngine;
using System.Collections;

/// <summary>
/// 方块
/// 	
///     y+		  
///     | /z+
///     |/
/// 	----- x+
/// 	         _____
/// 			/top /|	
/// 		   /___	/ |
/// 	left  |    |  | right         
/// 		  |back| /
/// 		  | ___|/	
/// </summary>
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(BoxCollider))]
public class Cube : ModelBehaviour
{
	/// <summary>
	/// 面
	/// </summary>
	public enum CubeSide 
	{
		Front,
		Back,
		Top,
		Bottom,
		Left,
		Right,

	}
	/// <summary>
	/// 点击面回调
	/// </summary>
	public delegate void OnTouchSideHandler(Cube target, Vector3 position);

	/// <summary>
	/// 碰撞体
	/// </summary>
	private Collider _Collider;
	/// <summary>
	/// 点击面回调
	/// </summary>
	private OnTouchSideHandler _OnTouchSideHandler;

	/// <summary>
	/// 六面纹理
	/// </summary>
	public Texture[] SideTextures = new Texture[6];

	/// <summary>
	/// 面数
	/// </summary>
	public const int SideCount = 6;

	/// <summary>
	/// 体积大小
	/// </summary>
	/// <value>The size.</value>
	public Vector3 Size {
		get { 
			if (_Collider == null) {
				return Vector3.one;
			}
			return _Collider.bounds.size;
		}
	}

	/// <summary>
	/// 点击面回调
	/// </summary>
	public OnTouchSideHandler OnTouchHandler {
		set { 
			_OnTouchSideHandler = value;
		}
	}

	/// <summary>
	/// 位置
	/// </summary>
	/// <value>The position.</value>
	public Vector3 Position {
		get { 
			return this.transform.position;
		}
	}

	/// <summary>
	/// 初始化
	/// </summary>
	public override void Init() {
		base.Init ();

		_Collider = this.GetComponent<Collider> ();

		MeshRenderer renderer = this.GetComponent<MeshRenderer> ();
		InitMaterials (renderer);
	}
	
	/// <summary>
	/// 点击事件
	/// </summary>
	public override bool OnTouchModel(Vector3 hitInfo) 
	{ 
		Vector3 position = this.Position;
		Vector3 size = this.Size;

		Vector3 sidePos = position;


		if (hitInfo.y == position.y + size.y * 0.5f) {// 上方
			sidePos.y += size.y;
		} else if (hitInfo.y == position.y - size.y * 0.5f) { // 下方
			sidePos.y -= size.y;
		} else if (hitInfo.x == position.x + size.x * 0.5f) { // 右边
			sidePos.x += size.x;
		} else if (hitInfo.x == position.x - size.x * 0.5f) { // 左边
			sidePos.x -= size.x;
		} else if (hitInfo.z == position.z + size.z * 0.5f) { // 前面
			sidePos.z += size.z;
		} else { // 背面
			sidePos.z -= size.z;
		}

		if (_OnTouchSideHandler != null) {
			_OnTouchSideHandler (this, sidePos);
		}
		
		return true; 
	}

	void Start() {
		DrawCube ();
	}

	/// <summary>
	/// 初始化顶点
	/// </summary>
	/// <param name="mesh">Mesh.</param>
	private void InitVertices(Mesh mesh) {
		if (mesh == null) {
			return;
		}

		Vector3 front_left_bottom = new Vector3(0.5f, -0.5f, 0.5f);
		Vector3 front_right_bottom = new Vector3(-0.5f, -0.5f, 0.5f);
		Vector3 front_left_top = new Vector3(0.5f, 0.5f, 0.5f);
		Vector3 front_right_top = new Vector3(-0.5f, 0.5f, 0.5f);

		Vector3 back_left_bottom = new Vector3(-0.5f, -0.5f, -0.5f);
		Vector3 back_right_bottom = new Vector3(0.5f, -0.5f, -0.5f);
		Vector3 back_left_top = new Vector3(-0.5f, 0.5f, -0.5f);
		Vector3 back_right_top = new Vector3(0.5f, 0.5f, -0.5f);


		mesh.vertices = new Vector3[]  
		{  
			//front  
			front_left_bottom, front_right_bottom, front_right_top, front_left_top,

			//back  
			back_left_bottom, back_right_bottom, back_right_top, back_left_top,

			//top  
			back_left_top, back_right_top, front_left_top, front_right_top,

			//bottom  
			front_right_bottom, front_left_bottom, back_right_bottom, back_left_bottom,

			//left 
			front_right_bottom, back_left_bottom, back_left_top, front_right_top,

			//right
			back_right_bottom, front_left_bottom, front_left_top, back_right_top,
		};
	}

	/// <summary>
	/// 初始化三角形
	/// </summary>
	/// <param name="mesh">Mesh.</param>
	private void InitTriangles(Mesh mesh) {
		if (mesh == null) {
			return;
		}
		int[] triangles = new int[] {  
			0, 2, 1,  
			0, 3, 2,  

			4, 6, 5,  
			4, 7, 6,  

			8, 10, 9,  
			8, 11, 10,  

			12, 14, 13,  
			12, 15, 14,  

			16, 18, 17,  
			16, 19, 18,  

			20, 22, 21,  
			20, 23, 22
		};
			
		mesh.SetTriangles (GetRangeArray (triangles, 0, 5), 0);
		mesh.SetTriangles (GetRangeArray (triangles, 6, 11), 1);
		mesh.SetTriangles (GetRangeArray (triangles, 12, 17), 2);
		mesh.SetTriangles (GetRangeArray (triangles, 18, 23), 3);
		mesh.SetTriangles (GetRangeArray (triangles, 24, 29), 4);
		mesh.SetTriangles (GetRangeArray (triangles, 30, 35), 5);
	}

	public int[] GetRangeArray(int[] srcArray, int startIndex, int endIndex)
	{
		if (srcArray == null || srcArray.Length == 0) {
			return null;
		} 
		if (startIndex < 0 || endIndex < 0
		    || endIndex < startIndex
		    || startIndex >= srcArray.Length || endIndex >= srcArray.Length) {
			return null;
		}

		int[] result = new int[endIndex - startIndex + 1];
		for (int i = 0; i <= endIndex - startIndex; i++) {
			result[i] = srcArray[i + startIndex];
		}
			
		return result;
	}

	/// <summary>
	/// 纹理坐标
	/// </summary>
	/// <param name="mesh">Mesh.</param>
	private void InitUVs(Mesh mesh) {
		if (mesh == null) {
			return;
		}
		mesh.uv = new Vector2[]  
		{  
			new Vector2(0, 0),  
			new Vector2(1, 0),  
			new Vector2(1, 1),  
			new Vector2(0, 1), 

			new Vector2(0, 0),  
			new Vector2(1, 0),  
			new Vector2(1, 1),  
			new Vector2(0, 1), 

			new Vector2(0, 0),  
			new Vector2(1, 0),  
			new Vector2(1, 1),  
			new Vector2(0, 1), 

			new Vector2(0, 0),  
			new Vector2(1, 0),  
			new Vector2(1, 1),  
			new Vector2(0, 1), 

			new Vector2(0, 0),  
			new Vector2(1, 0),  
			new Vector2(1, 1),  
			new Vector2(0, 1), 

			new Vector2(0, 0),  
			new Vector2(1, 0),  
			new Vector2(1, 1),  
			new Vector2(0, 1), 
		};  
	}

	/// <summary>
	/// 初始化材质
	/// </summary>
	/// <param name="renderer">Renderer.</param>
	private void InitMaterials(MeshRenderer renderer) {
		if (renderer == null) {
			return;
		}

		renderer.materials = new Material[SideCount];

		for (int i = 0; i < SideCount; i++) {
			Material mat = renderer.materials [i];
			mat.shader = Shader.Find ("Unlit/Transparent");
			mat.name = ((CubeSide)i).ToString ();
		}
	}

	/// <summary>
	/// 绘制立方体
	/// </summary>
	private void DrawCube() {
		Mesh mesh = this.GetComponent<MeshFilter> ().mesh;
		mesh.Clear ();
		mesh.subMeshCount = SideCount;

		InitVertices (mesh);
		InitTriangles (mesh);
		InitUVs (mesh);

		if (SideTextures == null || SideTextures.Length == 0) {
			return;
		}

		MeshRenderer renderer = this.GetComponent<MeshRenderer> ();
		Texture texture = null;
		for (int i = 0; i < SideCount; i++) {
			texture = i < SideTextures.Length ? SideTextures [i] : SideTextures [SideTextures.Length - 1];
			renderer.materials[i].mainTexture = texture;
		}
	}

	/// <summary>
	/// 替换纹理
	/// </summary>
	/// <param name="side">Side.</param>
	/// <param name="texture">Texture.</param>
	public void ReplaceTexture(CubeSide side, Texture texture) {
		if (texture == null) {
			return;
		}
		int index = (int)side;
		SideTextures [index] = texture;

		MeshRenderer renderer = this.gameObject.GetComponent<MeshRenderer> ();
		if (renderer == null
			|| renderer.materials == null
			|| renderer.materials.Length != SideCount) {
			return;
		}

		renderer.materials[index].mainTexture = texture;
	}

	/// <summary>
	/// 设置颜色
	/// </summary>
	/// <param name="side">Side.</param>
	/// <param name="color">Color.</param>
	public void SetColor(CubeSide side, Color color)
	{
		MeshRenderer renderer = this.gameObject.GetComponent<MeshRenderer> ();
		if (renderer == null
			|| renderer.materials == null
			|| renderer.materials.Length != SideCount) {
			return;
		}
		int index = (int)side;
		renderer.materials[index].color = color;
	}

	/// <summary>
	/// 设置透明度
	/// </summary>
	/// <param name="side">Side.</param>
	/// <param name="alpha">Alpha.</param>
	public void SetAlpha(CubeSide side, float alpha)
	{
		MeshRenderer renderer = this.gameObject.GetComponent<MeshRenderer> ();
		if (renderer == null
			|| renderer.materials == null
			|| renderer.materials.Length != SideCount) {
			return;
		}

		int index = (int)side;
		Color color = renderer.materials [index].color;
		color.a = alpha;
		renderer.materials[index].color = color;
	}
}

