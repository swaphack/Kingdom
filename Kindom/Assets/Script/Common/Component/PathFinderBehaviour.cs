using UnityEngine;
using UnityEngine.AI;
using System.Collections;

/// <summary>
/// 寻路组件
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class PathFinderBehaviour : MonoBehaviour
{
	private NavMeshAgent _Agent;
	/// <summary>
	/// 目标
	/// </summary>
	public Transform Target;
	/// <summary>
	/// 目的地
	/// </summary>
	public Vector3 Destination;
	/// <summary>
	/// 是否正在运行
	/// </summary>
	private bool _Running;

	public bool Running {
		get {
			return _Running;
		}
	}

	void Awake()
	{
		_Agent = this.GetComponent<NavMeshAgent> ();
	}

	void Update() {
		if (_Running == false) {
			return;
		}
		if (Target == null) {
			_Agent.SetDestination (Destination);
			return;
		} else {
			_Agent.SetDestination (Target.position);
		}
	}

	/// <summary>
	/// 移动速度
	/// </summary>
	/// <value>The speed.</value>
	public float Speed {
		get { 
			return _Agent.speed;
		}
		set { 
			_Agent.speed = value;
		}
	}

	/// <summary>
	/// 角速度
	/// </summary>
	/// <value>The angular speed.</value>
	public float AngularSpeed {
		get { 
			return _Agent.angularSpeed;
		}
		set { 
			_Agent.angularSpeed = value;
		}
	}

	/// <summary>
	/// 加速度
	/// </summary>
	/// <value>The acceleration.</value>
	public float Acceleration {
		get { 
			return _Agent.acceleration;
		}
		set { 
			_Agent.acceleration = value;
		}
	}

	/// <summary>
	/// 停止距离
	/// </summary>
	/// <value>The stopping distance.</value>
	public float StoppingDistance {
		get { 
			return _Agent.stoppingDistance;
		}
		set { 
			_Agent.stoppingDistance = value;
		}
	}

	/// <summary>
	/// 自动停止
	/// </summary>
	/// <value><c>true</c> if auto braking; otherwise, <c>false</c>.</value>
	public bool AutoBraking {
		get { 
			return _Agent.autoBraking;
		}
		set { 
			_Agent.autoBraking = value;
		}
	}

	/// <summary>
	/// 对象半径
	/// </summary>
	/// <value>The radius.</value>
	public float Radius {
		get { 
			return _Agent.radius;
		}
		set { 
			_Agent.radius = value;
		}
	}

	/// <summary>
	/// 对象高度
	/// </summary>
	/// <value>The height.</value>
	public float Height {
		get { 
			return _Agent.height;
		}
		set { 
			_Agent.height = value;
		}
	}

	/// <summary>
	/// 优先级
	/// </summary>
	/// <value>The height.</value>
	public int Priority {
		get { 
			return _Agent.avoidancePriority;
		}
		set { 
			_Agent.avoidancePriority = value;
		}
	}

	/// <summary>
	/// 是否重新自动寻路
	/// </summary>
	/// <value><c>true</c> if auto repath; otherwise, <c>false</c>.</value>
	public bool AutoRepath {
		get { 
			return _Agent.autoRepath;
		}
		set { 
			_Agent.autoRepath = value;
		}
	}

	/// <summary>
	/// 是否可自动穿越网格线，如果开启的话，在起始点会开始向终点移动，否则不移动
	/// </summary>
	/// <value><c>true</c> if auto traverse off mesh link; otherwise, <c>false</c>.</value>
	public bool AutoTraverseOffMeshLink {
		get { 
			return _Agent.autoTraverseOffMeshLink;
		}
		set { 
			_Agent.autoTraverseOffMeshLink = value;
		}
	}

	/// <summary>
	/// 是否停止移动
	/// </summary>
	/// <value><c>true</c> if this instance is stopped; otherwise, <c>false</c>.</value>
	public bool IsStopped {
		get { 
			return _Agent.isStopped;
		}
		set { 
			_Agent.isStopped = value;
		}
	}

	/// <summary>
	/// 是否正在移动
	/// </summary>
	/// <value><c>true</c> if this instance is moving; otherwise, <c>false</c>.</value>
	public bool IsMoving {
		get { 
			return _Agent.pathPending
			|| _Agent.remainingDistance > _Agent.stoppingDistance
			|| _Agent.velocity != Vector3.zero;
		}
	}

	/// <summary>
	/// 停止寻路代理
	/// </summary>
	public void Stop() {
		_Running = false;
	}

	/// <summary>
	/// 恢复寻路代理
	/// </summary>
	public void Resume() {
		_Running = true;
	}
}

