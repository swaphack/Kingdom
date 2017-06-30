using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Geography.Map
{
	/// <summary>
	/// 数据读取
	/// </summary>
	public class MapData
	{
		public class DataItem
		{
			private string _Key;
			private string _Value;
			private Dictionary<string, DataItem> _Children;

			public string Key {
				get { 
					return _Key; 
				}
			}
			public string Value {
				get { 
					return _Value; 
				}
				set { 
					_Value = value;
				}
			}

			public Dictionary<string, DataItem> Children {
				get { 
					return _Children;
				}
			}

			public DataItem(string key) {
				_Key = key;
				_Children = new Dictionary<string, DataItem>();
			}
		}

		private Dictionary<string, DataItem> _DataItems;

		public MapData()
		{
			_DataItems = new Dictionary<string, DataItem> ();
		}

		/// <summary>
		/// 从文件加载数据
		/// </summary>
		/// <returns><c>true</c>, if from file was loaded, <c>false</c> otherwise.</returns>
		/// <param name="filepath">Filepath.</param>
		public bool LoadFromFile(string filepath)
		{
			if (string.IsNullOrEmpty (filepath)) {
				return false;
			}

			string data = ResourceManger.Instance.GetString (filepath);
			if (string.IsNullOrEmpty (data)) {
				return false;
			}

			return Load (data);
		}

		/// <summary>
		/// 加载数据
		/// </summary>
		/// <param name="data">Data.</param>
		public bool Load(string data)
		{
			if (string.IsNullOrEmpty (data)) {
				return false;
			}

			return new DataParser ().Load (data, _DataItems);
		}
	}

}