using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Geography.Ground;

namespace Geography.Ground.Sample
{
	/// <summary>
	/// 创建者
	/// </summary>
	public class Builder : MonoBehaviour
	{
		public string Url = "Prefabs/Medieval_Building_19";

		public GroundBase Ground;

		void Start ()
		{
			if (!ResourceManger.Instance.LoadGameObject (Url)) {
				Debug.LogError ("null prefabs, url : " + Url);
				return;
			}
			if (Ground != null) {
				Ground.AddClickListener ();
			}

		}

		void OnDestroy ()
		{
			if (Ground != null) {
				Ground.RemoveClickListener ();
			}
		}
	}
}