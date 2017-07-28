using System;
using System.Collections.Generic;

namespace Football.AI
{
	/// <summary>
	/// 命令队列
	/// </summary>
	public class CommandQueue
	{
		/// <summary>
		/// 命令队列
		/// </summary>
		private Queue<Command> _CommandQueue;

		public CommandQueue ()
		{
			_CommandQueue = new Queue<Command> ();
		}

		/// <summary>
		/// 传入命令
		/// </summary>
		/// <param name="id">Identifier.</param>
		public void PutCommand(int id) {
			Command command = new Command ();
			command.ID = id;
			command.Paramter = null;
			_CommandQueue.Enqueue (command);
		}

		/// <summary>
		/// 传入命令
		/// </summary>
		/// <param name="id">Identifier.</param>
		/// <param name="param0">Param0.</param>
		public void PutCommand(int id, object param0) {
			Command command = new Command ();
			command.ID = id;
			command.Paramter = new object[] { param0 };
			_CommandQueue.Enqueue (command);
		}

		/// <summary>
		/// 传入命令
		/// </summary>
		/// <param name="id">Identifier.</param>
		/// <param name="param0">Param0.</param>
		/// <param name="param1">Param1.</param>
		public void PutCommand(int id, object param0, object param1) {
			Command command = new Command ();
			command.ID = id;
			command.Paramter = new object[] { param0, param1 };
			_CommandQueue.Enqueue (command);
		}

		/// <summary>
		/// 传入命令
		/// </summary>
		/// <param name="id">Identifier.</param>
		/// <param name="param0">Param0.</param>
		/// <param name="param1">Param1.</param>		
		/// <param name="param2">Param2.</param>	
		public void PutCommand(int id, object param0, object param1, object param2) {
			Command command = new Command ();
			command.ID = id;
			command.Paramter = new object[] { param0, param1, param2 };
			_CommandQueue.Enqueue (command);
		}

		/// <summary>
		/// 获取当前要执行的命令
		/// </summary>
		public Command Peek() {
			if (_CommandQueue.Count == 0) {
				return null;
			}

			return _CommandQueue.Dequeue ();
		}

		/// <summary>
		/// 清空命令
		/// </summary>
		public void Clear()
		{
			_CommandQueue.Clear ();
		}
	}
}

