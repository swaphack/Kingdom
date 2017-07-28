using System;
using Football.AI;

namespace Football
{
	public class TeamAI : Agent
	{
		/// <summary>
		/// 注册状态转换规则
		/// </summary>
		/// <param name="curState">Current state.</param>
		/// <param name="trigger">Trigger.</param>
		/// <param name="nextState">Next state.</param>
		public void RegisterFSMRule(TeamState curState, TeamState nextState, TeamStateTrigger[] triggers)
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
		public void RegisterFSMState(TeamState state, FiniteStateMachine.StateChangeDelegate handler)
		{
			FSM.AddStateHandler ((int)state, handler);
		}
		
		public TeamAI ()
		{
		}
	}
}

