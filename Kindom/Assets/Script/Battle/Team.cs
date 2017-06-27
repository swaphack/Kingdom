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
	/// <summary>
	/// 位置
	/// </summary>
	private Vector3 _Position;

	/// <summary>
	/// 阵型
	/// </summary>
	/// <value>The formation.</value>
	public Formation Formation {
		get {
			return _Formation;
		}
	}

	/// <summary>
	/// 位置
	/// </summary>
	/// <value>The position.</value>
	public Vector3 Position {
		get { 
			return _Position;
		}
	}

	public Team() {
		_Formation = new Formation ();
	}


	void Start() {
		this.InvokeRepeating ("Timer", 0, 1);
	}

	void OnDestory() {
		this.CancelInvoke ();
	}

	private void Timer() {
		_Position = Vector3.zero;
		int count = 0;
		ForeachUnit ((int index, Unit unit) => {
			_Position += unit.transform.position;
			count = index + 1;
		});

		if (count == 0) {
			return;
		}

		_Position /= count;
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

