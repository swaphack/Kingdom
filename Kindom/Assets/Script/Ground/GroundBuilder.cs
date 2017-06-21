using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 创建者
/// </summary>
public class GroundBuilder : MonoBehaviour 
{
	public string Url = "Prefabs/Medieval_Building_19";

	public Ground Ground;

	void Start() {
		if (!ResourceManger.Instance.LoadGameObject(Url)) {
			Debug.LogError ("null prefabs, url : " + Url);
			return;
		}
		if (Ground != null) {
			Ground.AddClickListener ();
		}

	}

	void OnDestroy() {
		if (Ground != null) {
			Ground.RemoveClickListener ();
		}
	}
}
