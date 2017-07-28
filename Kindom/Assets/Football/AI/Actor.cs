using System.Collections.Generic;
using Football;

namespace Football.AI
{
	/// <summary>
	/// 动作执行者
	/// </summary>
	public class Actor : Proxy
	{
		/// <summary>
		/// 命令队列
		/// </summary>
		private CommandQueue _CommandQueue;

		/// <summary>
		/// 方法字典
		/// </summary>
		private Dictionary<int, string> _Methods;
		/// <summary>
		/// 是否正在执行任务
		/// </summary>
		private bool _Running;

		/// <summary>
		/// 命令队列
		/// </summary>
		/// <value>The commander.</value>
		public CommandQueue CommandQueue {
			get { 
				return _CommandQueue;
			}
		}

		public Actor ()
		{
			_CommandQueue = new CommandQueue ();
			_Methods = new Dictionary<int, string> ();
		}

		/// <summary>
		/// 添加方法
		/// </summary>
		/// <param name="id">Identifier.</param>
		/// <param name="methodName">Method name.</param>
		public void AddMethod(int id, string methodName) {
			_Methods [id] = methodName;
		}

		/// <summary>
		/// 获取方法
		/// </summary>
		/// <returns>The method.</returns>
		/// <param name="id">Identifier.</param>
		public string GetMethod(int id) {
			if (_Methods.ContainsKey (id)) {
				return _Methods [id];
			}

			return null;
		}

		/// <summary>
		/// 执行命令
		/// </summary>
		/// <param name="entity">Entity.</param>
		/// <param name="command">Command.</param>
		private bool ExecuteCommand(Entity entity, Command command) {
			if (entity == null || command == null) {
				return false;
			}

			_Running = true;

			string methodName = GetMethod (command.ID);
			if (methodName == null) {
				return false;
			}

			if (command.Paramter == null) {
				entity.SendMessage (methodName);
			} else if (command.Paramter.Length == 1) {
				entity.SendMessage (methodName, command.Paramter[0]);
			} else {
				entity.SendMessage (methodName, command.Paramter);
			}

			return true;
		}

		/// <summary>
		/// 执行命令
		/// </summary>
		public void RunCommand() {
			if (IsRunning || Entity == null) {
				return;
			}

			Command command = _CommandQueue.Peek ();
			if (command == null) {
				return;
			}

			ExecuteCommand (Entity, command);
		}

		/// <summary>
		/// 是否执行完毕
		/// </summary>
		/// <value><c>true</c> if finish execute; otherwise, <c>false</c>.</value>
		public bool IsRunning {
			get { 
				return _Running;
			}
		}
		/// <summary>
		/// 执行完毕
		/// </summary>
		public void Finish() {
			_Running = false;
		}
	}
}

