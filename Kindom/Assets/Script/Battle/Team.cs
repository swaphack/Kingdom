using UnityEngine;
using System.Collections;

/// <summary>
/// 队伍
/// </summary>
public class Team : BNode
{
	public delegate void OnForeachUnitHandler(Unit unit);

	private Formation _Formation;

	public Formation Formation {
		get {
			return _Formation;
		}
	}

	public Team() {
		_Formation = new Formation (this);
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

		Foreach ((Component child) => {
			handler(child.GetComponent<Unit>());
		});
	}
}

