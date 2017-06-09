using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour 
{
	public string Url = "Prefabs/Medieval_Building_19";

	public Ground Ground;

	void Start() {
		if (!ResourceManger.Instance.LoadGameObject(Url)) {
			Debug.LogError ("null prefabs, url : " + Url);
			return;
		}
		if (Ground != null) {
			TouchListener.Instance.AddDispatch (Ground.gameObject, this.OnHit);
		}

	}

	void OnDestroy() {
		if (Ground != null) {
			TouchListener.Instance.RemoveDispatch (Ground.gameObject);
		}
	}

	/// <summary>
	/// 按照点击点，获取地图块，越上面的优先级越高
	/// </summary>
	/// <returns>The touch tile.</returns>
	/// <param name="position">Position.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public T GetTouchTile<T>(Vector3 position) where T : GroundTile 
	{
		if (Ground == null) {
			return null;
		}

		GroundLayer[] layers = Ground.GetComponentsInChildren<GroundLayer> ();
		if (layers == null || layers.Length == 0) {
			return null;
		}

		for (int i = layers.Length - 1; i >= 0 ; i--) {
			GroundLayer layer = layers[i];
			Vector3 centerPos = layer.ConvertToCenterPosition(position);
			T tile = layer.GetTile<T>(centerPos);
			if (tile != null) {
				return tile;
			}
		}

		return null;
	}

	void OnHit(Vector3 hitPos) {
		if (Ground == null) {
			return;
		}

		GroundTile tile = GetTouchTile<GroundTile> (hitPos);
		if (tile is Turf) {
			if (!tile.IsTouched) {
				tile.PlayHighlight ();
			} else {
				tile.CancelHighlight ();
			}
		}

		tile.IsTouched = !tile.IsTouched;
	}
}
