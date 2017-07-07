using System;

namespace Document
{
	/// <summary>
	/// 属性
	/// </summary>
	public class Attribute
	{
		/// <summary>
		/// 名称
		/// </summary>
		private string _Key;
		/// <summary>
		/// 值
		/// </summary>
		private string _Value;

		/// <summary>
		/// 名称
		/// </summary>
		/// <value>The key.</value>
		public string Key { 
			get { 
				return _Key;
			}
			set { 
				_Key = value;
			}
		}	

		/// <summary>
		/// 值
		/// </summary>
		/// <value>The value.</value>
		public string Value { 
			get { 
				return _Value;
			}
			set { 
				_Value = value;
			}
		}
	}
}

