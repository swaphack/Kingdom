using System;
using UnityEngine;

namespace Football.Actions
{
	/// <summary>
	/// 动作
	/// </summary>
	public class Action
	{
		/// <summary>
		/// 是否结束
		/// </summary>
		private bool _Finish;
		/// <summary>
		/// 实体
		/// </summary>
		private Entity _Entity;

		/// <summary>
		/// 动作是否结束
		/// </summary>
		/// <value><c>true</c> if finish; otherwise, <c>false</c>.</value>
		public bool Finish {
			get { 
				return _Finish;
			}
			set { 
				_Finish = value;
			}
		}

		/// <summary>
		/// 实体
		/// </summary>
		/// <value><c>true</c> if entity; otherwise, <c>false</c>.</value>
		public Entity Entity {
			get { 
				return _Entity;
			}
			set { 
				_Entity = value;
			}
		}

		/// <summary>
		/// 初始化
		/// </summary>
		public virtual void Init() {
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="dt">Dt.</param>
		public virtual void Update(float dt) {
			if (Finish) {
				return;
			}

			RunAction(dt);
		}

		/// <summary>
		/// 执行动作
		/// </summary>
		/// <param name="dt">Dt.</param>
		protected virtual void RunAction(float dt) {
			
		}
	}
}

