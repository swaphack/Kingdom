using UnityEngine;
using System.Collections;

/// <summary>
/// 单位
/// </summary>
[RequireComponent(typeof(PathFinderBehaviour))]
public class Unit : MonoBehaviour, IInitialization
{
	private PathFinderBehaviour _PathFinder;

	public virtual void Initialize() {
		_PathFinder = this.GetComponent<PathFinderBehaviour> ();
	}

	/// <summary>
	/// 移动到指定位置
	/// </summary>
	/// <param name="destination">Destination.</param>
	public void WalkTo(Vector3 destination) {
		_PathFinder.Destination = destination;
		_PathFinder.Target = null;
		_PathFinder.Resume();
	}

	/// <summary>
	/// 朝目标方向移动指定位移
	/// </summary>
	/// <param name="vector">Vector.</param>
	public void WalkDirection(Vector3 vector) {
		Vector3 destination = vector + this.transform.position;
		WalkTo (destination);
	}

	/// <summary>
	/// 移动到目标
	/// </summary>
	/// <param name="target">Target.</param>
	public void WalkTo(Component target) {
		if (target == null) {
			return;
		}
		_PathFinder.Target = target.transform;
		_PathFinder.Resume();
	}

	/// <summary>
	/// 停止移动
	/// </summary>
	public void StopWalk() {
		_PathFinder.Stop();
	}
}

