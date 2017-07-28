using UnityEngine;

namespace Football.Actions
{
	/// <summary>
	/// 抛物线运动
	/// </summary>
	public class Parabola : Action
	{
		/// <summary>
		/// 时间
		/// </summary>
		protected float _Time;
		/// <summary>
		/// 速度
		/// </summary>
		private Vector3 _Speed;
		/// <summary>
		/// 初始速度
		/// </summary>
		private Vector3 _InitSpeed;
		/// <summary>
		/// 最低高度
		/// </summary>
		private float _MinHeight;
		
		public Parabola (Vector3 speed, float minHeight)
		{
			_InitSpeed = speed;
			_MinHeight = minHeight;
		}

		public override void Init ()
		{
			base.Init ();
			_Speed = _InitSpeed;
		}

		/// <summary>
		/// 执行动作
		/// </summary>
		/// <param name="dt">Dt.</param>
		protected override void RunAction(float dt) {
			Vector3 pos = Entity.transform.position;

			Vector3 vecotr = _Speed * dt;

			Entity.transform.position += vecotr;

			// 速度改变
			Vector3 gravity = Physics.gravity;
			_Speed += gravity * dt;

			if (Entity.transform.position.y <= _MinHeight) {
				Finish = true;
			}
		}
	}
}

