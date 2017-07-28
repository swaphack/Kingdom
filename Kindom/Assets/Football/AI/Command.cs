using System;

namespace Football.AI
{
	/// <summary>
	/// 命令
	/// </summary>
	public class Command
	{
		/// <summary>
		/// 编号
		/// </summary>
		private int _ID;
		/// <summary>
		/// 参数
		/// </summary>
		private object[] _paramter;

		/// <summary>
		/// 编号
		/// </summary>
		/// <value>The I.</value>
		public int ID {
			get { 
				return _ID;
			}
			set { 
				_ID = value;
			}
		}

		/// <summary>
		/// 参数
		/// </summary>
		/// <value>The paramter.</value>
		public object[] Paramter {
			get { 
				return _paramter;
			}
			set { 
				_paramter = value;
			}
		}

		public Command ()
		{
		}
	}
}

