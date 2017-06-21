using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 阵型
/// </summary>
public class Formation
{
	/// <summary>
	/// 目标点
	/// </summary>
	public List<Vector3> _Points;

	public Formation()
	{
		_Points = new List<Vector3> ();
	}
	/// <summary>
	/// 点数
	/// </summary>
	/// <value>The count.</value>
	public int Count {
		get { 
			return _Points.Count;
		}
	}

	/// <summary>
	/// 添加点
	/// </summary>
	/// <param name="point">Point.</param>
	public void AddPoint(Vector3 point)
	{
		if (_Points.Contains (point)) {
			return;
		}

		_Points.Add (point);
	}

	/// <summary>
	/// 移除点
	/// </summary>
	/// <param name="point">Point.</param>
	public void RemovePoint(Vector3 point)
	{
		if (!_Points.Contains (point)) {
			return;
		}

		_Points.Remove (point);
	}

	/// <summary>
	/// 获取位置
	/// </summary>
	/// <returns>The point.</returns>
	/// <param name="index">Index.</param>
	public Vector3 GetPoint(int index) {
		if (index < 0 || index >= _Points.Count) {
			return Vector3.zero;
		}

		return _Points [index];
	}

	/// <summary>
	/// 复制阵型
	/// </summary>
	/// <param name="formation">Formation.</param>
	public void Copy(Formation formation)
	{
		if (formation == null) {
			return;
		}

		this.Clear ();

		int count = formation.Count;
		for (int i = 0; i < count; i++) {
			this.AddPoint (formation.GetPoint (i));
		}
	}

	/// <summary>
	/// 清空
	/// </summary>
	public void Clear()
	{
		_Points.Clear ();
	}
}

