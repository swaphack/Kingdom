using UnityEngine;
using System.Collections;

namespace Common.Utility
{

	public class Mesh2D
	{
		#region static data

		/// <summary>
		/// 顶点数
		/// </summary>
		public const int VerticeCount = 4;

		/// <summary>
		/// 三角形索引
		/// </summary>
		internal static int[] Triangles = new int[] {  
			0, 2, 1,  
			0, 3, 2,
		};

		/// <summary>
		/// 纹理索引
		/// </summary>
		internal static Vector2[] UV = new Vector2[] {  
			new Vector2 (0, 0),  
			new Vector2 (1, 0),  
			new Vector2 (1, 1),  
			new Vector2 (0, 1), 
		};

		/// <summary>
		/// 顶点坐标
		/// </summary>
		internal static Vector3[] Vertices = new Vector3[] {  
			new Vector3 (-0.5f, 0, -0.5f), 
			new Vector3 (0.5f, 0, -0.5f),
			new Vector3 (0.5f, 0, 0.5f),
			new Vector3 (-0.5f, 0, 0.5f),
		};

		#endregion

		/// <summary>
		/// 创建Mesh
		/// </summary>
		/// <param name="meshFilter">Mesh filter.</param>
		/// <param name="size">Size.</param>
		public static void CreateTextureMesh (MeshFilter meshFilter, Vector3 size)
		{
			if (meshFilter == null) {
				return;
			}

			Mesh mesh = new Mesh ();
			mesh.Clear ();
			mesh.name = meshFilter.name;
			Vector3[] vertices = new Vector3[VerticeCount];
			for (int i = 0; i < VerticeCount; i++) {
				vertices [i] = Vertices [i];
				vertices [i].x *= size.x;
				vertices [i].z *= size.z;
			}

			mesh.vertices = vertices;
			mesh.uv = UV;
			mesh.triangles = Triangles;
			meshFilter.mesh = mesh;
		}


		/// <summary>
		/// 创建材质
		/// </summary>
		/// <param name="meshRenderer">Mesh renderer.</param>
		/// <param name="texture">Texture.</param>
		public static void CreateTextureMaterial (MeshRenderer meshRenderer, Texture texture)
		{
			if (meshRenderer == null || texture == null) {
				return;
			}

			Material mat = new Material (Shader.Find ("Unlit/Transparent"));
			mat.name = meshRenderer.name;
			mat.mainTexture = texture;
			meshRenderer.material = mat;
		}

		/// <summary>
		/// 用顶点创建网格
		/// </summary>
		/// <param name="meshFilter">Mesh filter.</param>
		/// <param name="vecticeDatas">Vectice datas.</param>
		public static void CreateVecticeMesh (MeshFilter meshFilter, Vector2[] vecticeDatas)
		{
			if (meshFilter == null || vecticeDatas == null || vecticeDatas.Length < 3) {
				return;
			}

			int length = vecticeDatas.Length;
			Vector3[] vertices = new Vector3[length];
			for (int i = 0; i < length; i++) {
				vertices [i] = new Vector3 ();
				vertices [i].x = vecticeDatas [i].x;
				vertices [i].z = vecticeDatas [i].y;
			}

			int count = (length - 2) * 3;
			int[] triangles = new int[count];
			for (int i = 0; i < length - 2; i++) {
				triangles [i * 3] = 0;
				triangles [i * 3 + 1] = i + 2;
				triangles [i * 3 + 2] = i + 1;
			}


			Mesh mesh = new Mesh ();
			mesh.Clear ();
			mesh.name = meshFilter.name;
			mesh.vertices = vertices;
			mesh.triangles = triangles;
			meshFilter.mesh = mesh;
		}

		/// <summary>
		/// 创建材质
		/// </summary>
		/// <param name="meshRenderer">Mesh renderer.</param>
		/// <param name="color">Color.</param>
		public static void CreateColorMaterial (MeshRenderer meshRenderer, Color color)
		{
			if (meshRenderer == null) {
				return;
			}

			Material mat = new Material (Shader.Find ("Unlit/Color"));
			mat.name = meshRenderer.name;
			mat.color = color;
			meshRenderer.material = mat;
		}

	}

}