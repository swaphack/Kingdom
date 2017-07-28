using UnityEngine;

namespace Football.Actions
{
	/// <summary>
	/// 带间隔
	/// </summary>
	public class IntervalAction : TimeAction
	{
		public IntervalAction ()
		{
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="dt">Dt.</param>
		protected override void RunAction(float dt) {
			if (_Time + dt > TotalTime) {
				dt = TotalTime - _Time;
			}
			DoIntervalEvent(dt);
			if (_Time < TotalTime) {
				_Time += dt;
			} else {
				Finish = true;
			}
		}

		protected virtual void DoIntervalEvent(float dt) {
			
		}
	}

	/// <summary>
	/// 移动到
	/// </summary>
	public class MoveBy : IntervalAction
	{
		/// <summary>
		/// 方向
		/// </summary>
		protected Vector3 _PosVector;

		public MoveBy(Vector3 vector, float time) {
			_PosVector = vector;
			TotalTime = time;
		}

		/// <summary>
		/// 执行动作
		/// </summary>
		protected override void DoIntervalEvent(float dt) {
			Entity.transform.position += dt / TotalTime * _PosVector;
		}
	}

	public class MoveTo : IntervalAction 
	{
		protected Vector3 _DestPos;
		/// <summary>
		/// 方向
		/// </summary>
		protected Vector3 _PosVector;

		public MoveTo(Vector3 dest, float time)
		{
			_DestPos = dest;
			TotalTime = time;
		}

		public override void Init ()
		{
			base.Init ();
			_PosVector = _DestPos - Entity.transform.position;
		}

		/// <summary>
		/// 执行动作
		/// </summary>
		protected override void DoIntervalEvent(float dt) {
			Entity.transform.position += dt / TotalTime * _PosVector;
		}
	}

	/// <summary>
	/// 旋转
	/// </summary>
	public class RotateBy : IntervalAction
	{
		/// <summary>
		/// 方向
		/// </summary>
		protected Vector3 _AngleVector;

		public RotateBy(Vector3 vector, float time) {
			_AngleVector = vector;
			TotalTime = time;
		}


		/// <summary>
		/// 执行动作
		/// </summary>
		protected override void DoIntervalEvent(float dt) {
			Entity.transform.Rotate (dt / TotalTime * _AngleVector);
		}
	}

	public class RotateTo : IntervalAction 
	{
		protected Vector3 _DestAngle;
		/// <summary>
		/// 方向
		/// </summary>
		protected Vector3 _AngleVector;

		public RotateTo(Vector3 dest, float time)
		{
			_DestAngle = dest;
			TotalTime = time;
		}

		public override void Init ()
		{
			base.Init ();
			_AngleVector = _DestAngle - Entity.transform.rotation.eulerAngles;
		}

		/// <summary>
		/// 执行动作
		/// </summary>
		protected override void DoIntervalEvent(float dt) {
			Entity.transform.Rotate(dt / TotalTime * _DestAngle);
		}
	}
}

