using UnityEngine;
using System.Collections;

/// <summary>
/// 队伍
/// </summary>
public class Team : BNode
{
	public delegate void OnForeachUnitHandler(int index, Unit unit);

	/// <summary>
	/// 阵型
	/// </summary>
	private Formation _Formation;

	public Formation Formation {
		get {
			return _Formation;
		}
	}

	public Team() {
		_Formation = new Formation ();
	}

	/// <summary>
	/// 设置阵型
	/// </summary>
	/// <param name="formation">Formation.</param>
	public void SetFormation(Formation formation) {
		if (formation == null) {
			return;
		}
		_Formation.Copy (formation);
	}

	/// <summary>
	/// 遍历每个单位
	/// </summary>
	/// <param name="handler">Handler.</param>
	public void ForeachUnit(OnForeachUnitHandler handler)
	{
		if (handler == null) {
			return;
		}

		Unit[] units = this.GetComponentsInChildren<Unit>();
		if (units == null || units.Length == 0) {
			return;
		}

		int unitCount = units.Length;

		for (int i = 0; i < unitCount; i++) {
			handler (i, units[i]);
		}
	}

	/// <summary>
	/// 集结
	/// </summary>
	public void BuildUp()
	{
		ForeachUnit ((int i, Unit child) => {
			Vector3 point = _Formation.GetPoint(i) + transform.position;
			child.WalkTo(point);
		});
	} 

	/// <summary>
	/// 移动到目的地
	/// </summary>
	/// <param name="destination">Destination.</param>
	public void MoveTo(Vector3 destination)
	{
		//this._Team.transform.position = destination;
		ForeachUnit ((int i, Unit child) => {
			child.WalkTo(_Formation.GetPoint(i) + destination);
		});
	}
}

