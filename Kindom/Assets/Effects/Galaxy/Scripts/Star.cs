using UnityEngine;
using System.Collections;

/// <summary>
/// 星星
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class Star : MonoBehaviour
{
	/// <summary>
	/// 自转轴
	/// </summary>
	public Vector3 Axis;
	/// <summary>
	/// 自转速度
	/// </summary>
	public float Speed;
	/// <summary>
	/// 初始运动速度
	/// </summary>
	public Vector3 InitMoveSpeed;
	/// <summary>
	/// 当前速度
	/// </summary>
	private Vector3 _MoveSpeed;

	void Awake() {
		Rigidbody rigidbody = this.GetComponent<Rigidbody> ();
		rigidbody.useGravity = false;
		_MoveSpeed = InitMoveSpeed;
	}
	
	// Update is called once per frame
	void Update ()
	{
		AutoRotate();
		AutoRevolute ();
	}

	/// <summary>
	/// 自转
	/// </summary>
	private void AutoRotate() {
		this.transform.Rotate (Axis, Speed * Time.deltaTime);
	}

	/// <summary>
	/// 公转
	/// </summary>
	private void AutoRevolute() {
		Galaxy galaxy = this.GetComponentInParent<Galaxy> ();
		if (galaxy == null) {
			return;
		}

		Rigidbody rigidbody = this.GetComponent<Rigidbody> ();

		// 万有引力
		Vector3 gravity = galaxy.GetStarForce (this);

		float moveSpeed2 = Vector3.SqrMagnitude (_MoveSpeed);

		// 动能
		Vector3 kineticEnergy = _MoveSpeed.normalized * moveSpeed2 * rigidbody.mass * 0.5f;

		Vector3 force = gravity + kineticEnergy;

		//float moveForce = Vector3.Magnitude (force);

		_MoveSpeed = Vector3.zero;

		//force *= Time.deltaTime;
		this.GetComponent<Rigidbody> ().AddForce (force);
	}
}

