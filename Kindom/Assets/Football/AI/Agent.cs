using System;
using UnityEngine;
using System.Collections.Generic;

namespace Football.AI
{
	/// <summary>
	/// 智能体
	/// </summary>
	public class Agent : MonoBehaviour
	{
		/// <summary>
		/// 命令执行者
		/// </summary>
		private Actor _Actor;
		/// <summary>
		/// 有限状态机
		/// </summary>
		private FiniteStateMachine _FSM;

		/// <summary>
		/// 动作执行者
		/// </summary>
		/// <value>The actor.</value>
		public Actor Actor {
			get { 
				return _Actor;
			}
		}

		/// <summary>
		/// 有限状态机
		/// </summary>
		/// <value>The FS.</value>
		public FiniteStateMachine FSM {
			get { 
				return _FSM;
			}
		}
		
		public Agent ()
		{
			_Actor = new Actor ();
			_FSM = new FiniteStateMachine ();
		}

		/// <summary>
		/// 设置实体
		/// </summary>
		/// <param name="entity">Entity.</param>
		public void SetEntity(Entity entity) {
			_Actor.Entity = entity;
		}

		void Update() {
			RunCommand ();
		}

		/// <summary>
		/// 执行命令
		/// </summary>
		private void RunCommand() {
			_Actor.RunCommand ();
		}

		/// <summary>
		/// 动作执行完毕
		/// </summary>
		public void FinishAct() {
			_Actor.Finish ();
		}
	}
}

