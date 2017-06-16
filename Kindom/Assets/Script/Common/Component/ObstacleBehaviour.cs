using UnityEngine;
using UnityEngine.AI;
using System.Collections;

/// <summary>
/// 障碍物
/// </summary>
[RequireComponent(typeof(NavMeshObstacle))]
public class ObstacleBehaviour : MonoBehaviour
{
	private NavMeshObstacle _Obstacle;

	// Use this for initialization
	void Awake ()
	{
		_Obstacle = this.GetComponent<NavMeshObstacle> ();
		_Obstacle.shape = NavMeshObstacleShape.Box;
	}

	/// <summary>
	/// 中心点
	/// </summary>
	/// <value>The center.</value>
	public Vector3 Center {
		get { 
			return _Obstacle.center;
		}
		set { 
			_Obstacle.center = value;
		}
	}

	/// <summary>
	/// 大小
	/// </summary>
	/// <value>The size.</value>
	public Vector3 Size {
		get { 
			return _Obstacle.size;
		}
		set { 
			_Obstacle.size = value;
		}
	}
}

