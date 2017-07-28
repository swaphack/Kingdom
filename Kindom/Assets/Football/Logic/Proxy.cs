using System;

namespace Football
{
	public class Proxy
	{
		/// <summary>
		/// 实体
		/// </summary>
		private Entity _Entity;

		/// <summary>
		/// 实体
		/// </summary>
		public Entity Entity {
			get { 
				return _Entity;
			}
			set { 
				_Entity = value;
			}
		}
	}
}

