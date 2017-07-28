using Football;
using Football.Actions;
using System.Collections.Generic;

namespace Football.Actions
{
	/// <summary>
	/// 动作表演者
	/// </summary>
	public class Performer : Proxy
	{
		/// <summary>
		/// 动作队列
		/// </summary>
		private Queue<Action> _ActionQueue;

		public Performer ()
		{
			_ActionQueue = new Queue<Action> ();
		}

		/// <summary>
		/// 添加动作
		/// </summary>
		/// <param name="action">Action.</param>
		public void AddAction(Action action) {
			if (action == null) {
				return;
			}

			action.Entity = Entity;
			action.Init ();
			_ActionQueue.Enqueue (action);
		}

		/// <summary>
		/// 执行动作
		/// </summary>
		/// <param name="dt">Dt.</param>
		public void RunAction(float dt) {
			if (_ActionQueue.Count == 0) {
				return;
			}

			Action action = _ActionQueue.Peek (); 
			if (action.Finish) {
				_ActionQueue.Dequeue ();
				return;
			}
			action.Update (dt);
		}
	}
}

