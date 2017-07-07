using UnityEngine;
using System.Collections;

namespace Geography.Map
{
	/// <summary>
	/// 背景
	/// </summary>
	[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
	public class Background : MonoBehaviour, IInitialization
	{
		protected MeshFilter meshFilter;
		protected MeshRenderer meshRenderer;
		protected MeshCollider meshCollider;

		public virtual void Initialize() {
			meshFilter = this.GetComponent<MeshFilter> ();
			meshRenderer = this.GetComponent<MeshRenderer> ();
			meshCollider = this.GetComponent<MeshCollider> ();
		}
	}
}