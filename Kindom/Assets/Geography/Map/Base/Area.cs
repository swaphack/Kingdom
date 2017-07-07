using UnityEngine;
using System.Collections;

namespace Geography.Map
{
	/// <summary>
	/// 区域
	/// </summary>
	public class Area : Background
	{
		/// <summary>
		/// 顶点坐标
		/// </summary>
		public Vector2[] Vectices;
		/// <summary>
		/// 旋转
		/// </summary>
		public float[] Rotations;
		/// <summary>
		/// 高位图
		/// </summary>
		public float[] Heights;
		/// <summary>
		/// 颜色
		/// </summary>
		public Color Color;

		protected string matUrl = "Materials/AreaMat";
		
		public override void Initialize() {
			base.Initialize ();

			Material mat = ResourceManger.Instance.Get<Material> (matUrl);
			meshRenderer.material = mat;

			Mesh2D.CreateVecticeMesh (meshFilter, Vectices);
			Mesh2D.CreateColorMaterial (meshRenderer, Color);
			meshCollider.sharedMesh = meshFilter.mesh;
		}
	}
}