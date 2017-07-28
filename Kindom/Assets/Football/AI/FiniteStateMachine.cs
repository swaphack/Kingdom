using System;
using System.Collections.Generic;

namespace Football.AI
{
	/// <summary>
	/// 状态机
	/// </summary>
	public class FiniteStateMachine
	{
		/// <summary>
		/// 状态改变委托
		/// </summary>
		public delegate void StateChangeDelegate ();

		/// <summary>
		/// 当前状态
		/// </summary>
		private int _State;
		/// <summary>
		/// 规则
		/// 0-int 当前状态
		/// 1-int 转换条件
		/// 2-int 下一个状态
		/// </summary>
		private Dictionary<int, FSMRule> _Rules;
		/// <summary>
		/// 状态改变处理
		/// </summary>
		private Dictionary<int, StateChangeDelegate> _StateChangeHandlers;

		/// <summary>
		/// 当前状态
		/// </summary>
		/// <value>The state.</value>
		public int State {
			get { 
				return _State;
			}
			set { 
				_State = value;
			}
		}

		public FiniteStateMachine()
		{
			_Rules = new Dictionary<int, FSMRule> ();
			_StateChangeHandlers = new Dictionary<int, StateChangeDelegate> ();
		}

		/// <summary>
		/// 添加规则
		/// </summary>
		/// <param name="curState">Current state.</param>
		/// <param name="conditions">Condition.</param>
		/// <param name="nextState">Next state.</param>
		public void AddRule(int curState, int[] conditions, int nextState)
		{
			FSMRule rule = null;
			if (!_Rules.ContainsKey (curState)) {
				_Rules [curState] = new FSMRule ();
			}

			rule = _Rules [curState];
			rule.AddRule (conditions, nextState);
		}

		/// <summary>
		/// 移除规则
		/// </summary>
		/// <param name="curState">Current state.</param>
		/// <param name="conditions">Condition.</param>
		/// <param name="nextState">Next state.</param>
		public void RemoveRule(int curState, int[] conditions)
		{
			if (!_Rules.ContainsKey (curState)) {
				return;
			}

			_Rules [curState].RemoveRule (conditions);
		}

		/// <summary>
		/// 移除规则
		/// </summary>
		public void ClearRule() {
			_Rules.Clear ();
		}

		/// <summary>
		/// 条件改变
		/// </summary>
		/// <param name="conditions">Condition.</param>
		public bool Change(int[] conditions) {
			if (!_Rules.ContainsKey (_State)) {
				return false;
			}

			int result = _Rules [_State].Match (conditions);
			if (result == FSMRule.INVALID_RESULT) {
				return false;
			}

			_State = result;

			OnStateChange (_State);

			return true;
		}

		/// <summary>
		/// 添加状态处理
		/// </summary>
		/// <param name="state">State.</param>
		/// <param name="handler">Handler.</param>
		public void AddStateHandler(int state, StateChangeDelegate handler) {
			_StateChangeHandlers [state] = handler;
		}

		/// <summary>
		/// 状态改变处理
		/// </summary>
		/// <param name="state">State.</param>
		private void OnStateChange(int state) {
			if (!_StateChangeHandlers.ContainsKey (state)) {
				return;
			}

			_StateChangeHandlers [state] ();
		}
	}
}

