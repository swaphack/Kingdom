using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Common.Document;

namespace Geography.Map.Document.JSON
{
	/// <summary>
	/// 数据读取
	/// 用于解析类似json的数据 a = { b = 1 c = { } }
	/// </summary>
	public class JSONData
	{
		private IElement _Root;

		public JSONNode Root {
			get { 
				return (JSONNode)_Root;
			}
		}

		public JSONData()
		{
		}

		/// <summary>
		/// 获取节点
		/// </summary>
		/// <returns>The node.</returns>
		/// <param name="key">Key.</param>
		public JSONNode GetNode(string key) {
			if (string.IsNullOrEmpty (key)) {
				return null;
			}

			if (_Root == null) {
				return null;
			}

			string[] names = key.Split('.');

			IElement node = _Root;

			for (int i = 0; i < names.Length; i++) {
				IElement child = node.GetChild (names [i]);
				if (child == null) {
					return null;
				}
				node = child;
			}

			return (JSONNode)node;
		}

		/// <summary>
		///  获取值
		/// </summary>
		/// <returns>The value.</returns>
		/// <param name="key">Key.</param>
		public string GetValue(string key){
			if (string.IsNullOrEmpty (key)) {
				return null;
			}

			if (Root == null) {
				return null;
			}

			JSONNode node = GetNode (key);
			if (node == null) {
				return null;
			}

			return node.Value;
		}

		/// <summary>
		///  获取值
		/// </summary>
		/// <returns>The value.</returns>
		/// <param name="key">Key.</param>
		public string[] GetValueArray(string key){
			if (string.IsNullOrEmpty (key)) {
				return null;
			}

			if (Root == null) {
				return null;
			}

			JSONNode node = GetNode (key);
			if (node == null) {
				return null;
			}

			return node.ValueAry;
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

			JSONFile doc = new JSONFile ();
			bool result = doc.Load (data);
			if (!result) {
				return false;
			}

			_Root = doc.Root;
			return true;
		}
	}
}