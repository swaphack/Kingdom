using System;
using UnityEngine;
using Football.AI;

namespace Football
{
	/// <summary>
	/// 球员AI
	/// </summary>
	public class PlayerAI : Agent
	{	
		/*
		/// <summary>
		/// 带球
		/// </summary>
		/// <param name="vector">朝向</param>
		/// <param name="type">带球方式</param>
		public void Dribble(Vector3 vector, DribbleType type)
		{
			float speed = Player.Property [(int)PlayerProperty.EPP_PACE];
			if (type == DribbleType.EDT_SLOW) {
				speed *= Constants.SLOW_DRIBBLE_RATIO;
			} else if (type == DribbleType.EDT_NORMAL) {
				speed *= Constants.NORMAL_DRIBBLE_RATIO;
			} else if (type == DribbleType.EDT_FAST) {
				speed *= Constants.FAST_DRIBBLE_RATIO;
			}

			Vector3 realVector = vector.normalized * speed;
			Ball.transform.position += vector;
		}

		/// <summary>
		/// 踢球
		/// </summary>
		/// <param name="angle">方向</param>
		/// <param name="speed">速度</param>
		public void KickTo(Vector3 angle, float speed)
		{

		}

		/// <summary>
		/// 盯防
		/// </summary>
		public void Mark()
		{

		}

		/// <summary>
		/// 转身到某点
		/// </summary>
		public void TurnBodyToPoint(Vector3 position)
		{

		}

		/// <summary>
		/// 截球
		/// </summary>
		public void Intercept() 
		{

		}
		*/

		/// <summary>
		/// 球
		/// </summary>
		private Ball _Ball;

		public Ball Ball {
			get { 
				return _Ball;
			}
			set { 
				_Ball = value;
			}
		}

		/// <summary>
		/// 球员
		/// </summary>
		/// <value>The player.</value>
		public Player Player {
			get { 
				return this.GetComponent<Player>();
			}
		}

		/// <summary>
		/// 注册动作事件
		/// </summary>
		/// <param name="type">Type.</param>
		/// <param name="methodName">Method name.</param>
		public void RegisterActorMethod(PlayerCommand type, string methodName) {
			Actor.AddMethod ((int)type, methodName);
		}

		/// <summary>
		/// 注册状态转换规则
		/// </summary>
		/// <param name="curState">Current state.</param>
		/// <param name="trigger">Trigger.</param>
		/// <param name="nextState">Next state.</param>
		public void RegisterFSMRule(PlayerState curState, PlayerState nextState, PlayerStateTrigger[] triggers)
		{
			int[] conditions = new int[triggers.Length];
			for (int i = 0; i < triggers.Length; i++) {
				conditions [i] = (int)triggers [i];
			}
			FSM.AddRule ((int)curState, conditions, (int)nextState);
		}

		/// <summary>
		/// 注册状态绑定事件
		/// </summary>
		/// <param name="state">State.</param>
		/// <param name="handler">Handler.</param>
		public void RegisterFSMState(PlayerState state, FiniteStateMachine.StateChangeDelegate handler)
		{
			FSM.AddStateHandler ((int)state, handler);
		}

		public PlayerAI ()
		{
			RegisterActorMethod (PlayerCommand.EPC_TURN, "Turn");
			RegisterActorMethod (PlayerCommand.EPC_INTERCEPT, "Intercept");
			RegisterActorMethod (PlayerCommand.EPC_MOVE, "Move");
			RegisterActorMethod (PlayerCommand.EPC_DASH, "Dash");
			RegisterActorMethod (PlayerCommand.EPC_KICK, "Kick");
			RegisterActorMethod (PlayerCommand.EPC_SAY, "Say");

			/*
			RegisterFSMRule (PlayerState.EPS_WAIT, PlayerState.EPS_WAIT, waitToWait0);
			RegisterFSMRule (PlayerState.EPS_WAIT, PlayerState.EPS_MOVE, waitToMove0);
			RegisterFSMRule (PlayerState.EPS_WAIT, PlayerState.EPS_KICK, waitToWait0);
			RegisterFSMRule (PlayerState.EPS_MOVE, PlayerState.EPS_WAIT, waitToWait0);
			RegisterFSMRule (PlayerState.EPS_MOVE, PlayerState.EPS_MOVE, waitToWait0);
			RegisterFSMRule (PlayerState.EPS_MOVE, PlayerState.EPS_KICK, waitToWait0);
			RegisterFSMRule (PlayerState.EPS_KICK, PlayerState.EPS_WAIT, waitToWait0);
			RegisterFSMRule (PlayerState.EPS_KICK, PlayerState.EPS_MOVE, waitToWait0);
			RegisterFSMRule (PlayerState.EPS_KICK, PlayerState.EPS_KICK, waitToWait0);
			*/
		}

		void Start() {
			Player.SetProperty (PlayerAttribute.EPA_SPEED_WITH_BALL, 5);
			Player.SetProperty (PlayerAttribute.EPA_SPEED, 1.5f);
			Player.SetProperty (PlayerAttribute.EPA_DASH_SPEED, 2);
			Player.SetProperty (PlayerAttribute.EPA_TURN_SPEED, 20);
			Player.PutCommand (PlayerCommand.EPC_TURN, new Vector3 (0, 45, 0));
			Player.PutCommand (PlayerCommand.EPC_TURN, new Vector3 (0, -45, 0));
			Player.PutCommand (PlayerCommand.EPC_SAY, "Hello");
		}
	}
}

