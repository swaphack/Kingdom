using UnityEngine;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// 阵型
/// </summary>
public class Formation
{
	private Team _Team;

	/// <summary>
	/// 目标点
	/// </summary>
	private List<Vector3> _Points;

	public Formation(Team team)
	{
		_Team = team;
		_Points = new List<Vector3> ();
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
	/// 清空
	/// </summary>
	public void Clear()
	{
		_Points.Clear ();
	}


	/// <summary>
	/// 集结
	/// </summary>
	public void BuildUp()
	{
		if (_Team == null) {
			return;
		}

		int i = 0;
		_Team.ForeachUnit ((Unit child) => {
			Vector3 point = _Points[i] + _Team.transform.position;
			child.WalkTo(point);
			i++;
		});
	} 

	/// <summary>
	/// 移动到目的地
	/// </summary>
	/// <param name="destination">Destination.</param>
	public void MoveTo(Vector3 destination)
	{
		//this._Team.transform.position = destination;

		int i = 0;
		_Team.ForeachUnit ((Unit child) => {
			child.WalkTo(_Points[i] + destination);
			i++;
		});
	}

	/// <summary>
	/// 自动攻击
	/// </summary>
	public void AutoAttack() {
		
	}
}

