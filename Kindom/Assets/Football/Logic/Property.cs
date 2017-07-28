using System;
using System.Collections.Generic;

namespace Football
{
	/// <summary>
	/// 属性
	/// </summary>
	public class Property
	{
		/// <summary>
		/// 属性值
		/// </summary>
		private Dictionary<int, float> _Values;

		public Property ()
		{
			_Values = new Dictionary<int, float> ();
		}

		/// <summary>
		/// Gets or sets the <see cref="Football.Property"/> with the specified type.
		/// </summary>
		/// <param name="type">Type.</param>
		public float this[int type]
		{
			get { 
				if (!_Values.ContainsKey (type)) {
					return 0;
				}
				return _Values [type];
			}
			set { 
				_Values [type] = value;
			}
		}

		/// <summary>
		/// 加载数据
		/// </summary>
		/// <param name="propertyTable">Property table.</param>
		public void Load(Dictionary<int, float> propertyTable)
		{
			foreach (var item in propertyTable) {
				_Values [item.Key] = item.Value;
			}
		}

		/// <summary>
		/// 清空所有属性
		/// </summary>
		public void Clear()
		{
			_Values.Clear ();
		}
	}
}

