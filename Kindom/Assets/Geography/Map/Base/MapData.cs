using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Geography.Map.Document;
using Document;

namespace Geography.Map
{
	/// <summary>
	/// 数据读取
	/// </summary>
	public class MapData
	{
		private IElement _Root;

		public Node Root {
			get { 
				return (Node)_Root;
			}
		}

		public MapData()
		{
		}

		/// <summary>
		/// 获取值
		/// </summary>
		/// <returns>The node.</returns>
		/// <param name="key">Key.</param>
		public Node GetNode(string key) {
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

			return (Node)node;
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

			Node node = GetNode (key);
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

			Node node = GetNode (key);
			if (node == null) {
				return null;
			}

			return node.ValueAry;
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
		private bool Load(string data)
		{
			if (string.IsNullOrEmpty (data)) {
				return false;
			}

			MapFile doc = new MapFile ();
			bool result = doc.Load (data);
			if (!result) {
				return false;
			}

			_Root = doc.Root;
			return true;
		}
	}
}