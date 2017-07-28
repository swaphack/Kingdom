using UnityEngine;
using System.Collections;
using Football.AI;
using Football.Actions;

namespace Football
{
	/// <summary>
	/// 球员
	/// </summary>
	public class Player : Human
	{		
		public Player()
		{
			
		}

		/// <summary>
		/// 推送命令
		/// </summary>
		/// <param name="type">Type.</param>
		public void PutCommand(PlayerCommand type) {
			Agent.Actor.CommandQueue.PutCommand ((int)type);
		}

		/// <summary>
		/// 推送命令
		/// </summary>
		/// <param name="type">Type.</param>
		/// <param name="param0">Param0.</param>
		public void PutCommand(PlayerCommand type, object param0) {
			Agent.Actor.CommandQueue.PutCommand ((int)type, param0);
		}

		/// <summary>
		/// 设置属性值
		/// </summary>
		/// <param name="type">Type.</param>
		/// <param name="value">Value.</param>
		public void SetProperty(PlayerAttribute type, float value) {
			Property [(int)type] = value;
		}

		/// <summary>
		/// 获取属性值
		/// </summary>
		/// <returns>The property.</returns>
		/// <param name="type">Type.</param>
		public float GetProperty(PlayerAttribute type) {
			return Property [(int)type];
		}

		/// <summary>
		/// 添加动作
		/// </summary>
		/// <param name="action">Action.</param>
		/// <param name="postFinishMessage">If set to <c>true</c> post finish message.</param>
		public void AddAction(Action action, bool postFinishMessage = false) {
			if (action == null) {
				return;
			}
			this.Performer.AddAction (action);
			if (postFinishMessage) {
				this.Performer.AddAction (new CallFunc (() => {
					this.Agent.Actor.Finish ();
				}));
			}
		}

		/// <summary>
		/// 播放动作
		/// </summary>
		/// <param name="animationName">Animation name.</param>
		/// <param name="postFinishMessage">If set to <c>true</c> post finish message.</param>
		public void PlayAction(string animationName, bool postFinishMessage = false) {
			if (this.Model == null) {
				if (postFinishMessage) {
					this.Agent.Actor.Finish ();
				}
			} else {
				this.Model.Play (animationName);
				if (postFinishMessage) {
					this.Model.RegisterAnimationEventCallback (animationName, (string name, float time, bool EndAnimation) => {
						this.Agent.Actor.Finish ();
					});
				}
			}
		}

		/// <summary>
		/// 转身
		/// </summary>
		/// <param name="angle">Angle.</param>
		public void Turn(Vector3 angle) {
			Quaternion q0 = Quaternion.Euler (angle);
			Quaternion q1 = this.transform.rotation;
			float time =  Quaternion.Angle(q0, q1) / GetProperty (PlayerAttribute.EPA_TURN_SPEED);
			AddAction(new RotateTo(angle, time), true);
		}

		/// <summary>
		/// 移动到目标
		/// </summary>
		/// <param name="dest">Destination.</param>
		public void Move(Vector3 dest) {
			PlayAction (Constants.ActionName[0]);

			float time = (dest - this.transform.position).magnitude / GetProperty (PlayerAttribute.EPA_SPEED);

			AddAction (new MoveTo (dest, time), true);
		}

		/// <summary>
		/// 冲刺到目标
		/// </summary>
		/// <param name="dest">Destination.</param>
		public void Dash(Vector3 dest) {
			PlayAction (Constants.ActionName[1]);

			float time = (dest - this.transform.position).magnitude / GetProperty (PlayerAttribute.EPA_DASH_SPEED);

			AddAction (new MoveTo (dest, time), true);
		}

		/// <summary>
		/// 踢球
		/// </summary>
		/// <param name="point">Point.</param>
		public void Kick(Vector3 point) {
			PlayAction (Constants.ActionName[2], true);
		}

		/// <summary>
		/// 拦截
		/// </summary>
		public void Intercept() {
			PlayAction (Constants.ActionName[3], true);
		}

		/// <summary>
		/// 讲话
		/// </summary>
		/// <param name="words">Words.</param>
		public void Say(string words) {

		}

		void Start() {
			AddAgent<PlayerAI> ();
		}
	}	
}
