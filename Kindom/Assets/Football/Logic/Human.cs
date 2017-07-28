using System;
using System.Collections.Generic;
using UnityEngine;
using Common.Utility;

namespace Football
{
	/// <summary>
	/// 人
	/// </summary>
	public class Human : Entity
	{
		/// <summary>
		/// 出生日期
		/// </summary>
		private DateTime _BirthDay;
		/// <summary>
		/// 属性
		/// </summary>
		private Property _Property;
		/// <summary>
		/// 模型
		/// </summary>
		private Model _Model;

		/// <summary>
		/// 出生日期
		/// </summary>
		/// <value>The birth day.</value>
		public DateTime BirthDay {
			get { 
				return _BirthDay;
			}
			set { 
				_BirthDay = value;	
			}
		}

		/// <summary>
		/// 属性
		/// </summary>
		public Property Property {
			get { 
				return _Property;
			}
		}
			
		/// <summary>
		/// 模型
		/// </summary>
		public Model Model {
			get { 
				return _Model;
			}
		}

		public Human ()
		{
			_Property = new Property ();
			_BirthDay = new DateTime ();
		}

		/// <summary>
		/// 添加模型数据
		/// </summary>
		/// <param name="url">URL.</param>
		public void AddModel(string url) {
			Component child = transform.FindChild ("Model");
			if (child == null) {
				GameObject go = Resources.Load<GameObject> (url);
				if (go == null) {
					return;
				}
				go.transform.SetParent (this.transform);
				_Model = go.AddComponent<Model> ();
			} else {
				_Model = child.GetComponent<Model> ();
			}
		}

		/// <summary>
		/// 加载数据
		/// </summary>
		/// <param name="data">Data.</param>
		public void LoadProperty(CSVData data) {
			if (data == null) {
				return;
			}

			for (int i = 0; i < data.Count; i++) {
				Property [i] = data.Read<float> (i);
			}
		}
	}
}

