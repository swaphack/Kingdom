using System;
using Document;
using System.Collections.Generic;

namespace Geography.Map.Document
{
	/// <summary>
	/// 节点
	/// </summary>
	public class Node : Element
	{
		private string _Value;
		private string[] _ValueAry;

		public Node() 
		{
		}

		public void SetValue(string value) {
			_Value = value;
		}

		public void SetValue(string[] value)
		{
			_ValueAry = value;
		}

		public override string ToString ()
		{
			string val = Key + " : ";
			if (!string.IsNullOrEmpty (_Value)) {
				val += _Value;
			} else if (_ValueAry != null) {
				val += string.Join (",", _ValueAry);
			}
			return val;
		}
	}
}

