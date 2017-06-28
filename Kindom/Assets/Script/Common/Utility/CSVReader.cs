using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

/// <summary>
/// CSV 文件解析
/// </summary>
public class CSVReader 
{
	/// <summary>
	/// CSV 数据
	/// </summary>
	public class CSVData 
	{
		private string[] _Items;

		public int Count {
			get { 
				return _Items.Length;
			}
		}

		/// <summary>
		/// 索引
		/// </summary>
		/// <param name="index">Index.</param>
		public string this[int index] {
			get { 
				if (index < 0 || _Items.Length <= index) {
					return null;
				}

				return _Items [index];
			}
		}

		public CSVData(string data) 
		{
			_Items = data.Split(';');
		}

		/// <summary>
		/// 读取数据
		/// </summary>
		/// <param name="index">Index.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public T Read<T>(int index) where T : struct, IConvertible {
			if (index < 0 || _Items.Length <= index) {
				return default(T);
			}
			IConvertible t = default(T);
			TypeCode typeCode = t.GetTypeCode ();
			if (typeCode == TypeCode.Empty
				|| typeCode == TypeCode.Object
				|| typeCode == TypeCode.DBNull
				|| typeCode == TypeCode.String) {
				Debug.Assert (false, "CSVData : Read " + typeof(T).ToString () + " failure!");
				return default(T);
			}

			switch (typeCode) {
			case TypeCode.Boolean:
				bool boolValue;
				if (bool.TryParse (this [index], out boolValue)) {
					t = boolValue;
				}
				break;
			case TypeCode.Char:
				char charValue;
				if (char.TryParse (this [index], out charValue)) {
					t = charValue;
				}
				break;
			case TypeCode.SByte:
				sbyte sbyteValue;
				if (sbyte.TryParse (this [index], out sbyteValue)) {
					t = sbyteValue;
				}
				break;
			case TypeCode.Byte:
				byte byteValue;
				if (byte.TryParse (this [index], out byteValue)) {
					t = byteValue;
				}
				break;
			case TypeCode.Int16:
				short shortValue;
				if (short.TryParse (this [index], out shortValue)) {
					t = shortValue;
				}
				break;
			case TypeCode.UInt16:
				ushort ushortValue;
				if (ushort.TryParse (this [index], out ushortValue)) {
					t = ushortValue;
				}
				break;
			case TypeCode.Int32:
				int intValue;
				if (int.TryParse (this [index], out intValue)) {
					t = intValue;
				}
				break;
			case TypeCode.UInt32:
				uint uintValue;
				if (uint.TryParse (this [index], out uintValue)) {
					t = uintValue;
				}
				break;
			case TypeCode.Int64:
				long longValue;
				if (long.TryParse (this [index], out longValue)) {
					t = longValue;
				}
				break;
			case TypeCode.UInt64:
				ulong ulongValue;
				if (ulong.TryParse (this [index], out ulongValue)) {
					t = ulongValue;
				}
				break;
			case TypeCode.Single:
				float floatValue;
				if (float.TryParse (this [index], out floatValue)) {
					t = floatValue;
				}
				break;
			case TypeCode.Double:
				double doubleValue;
				if (double.TryParse (this [index], out doubleValue)) {
					t = doubleValue;
				}
				break;
			default:
				Debug.Assert (false, "CSVData : Unknow Type " + typeof(T).ToString () + "!");
				return default(T);
			}
			return (T)t;
		}
		
	}

	/// <summary>
	/// 文件格式
	/// </summary>
	private CSVData _Header;
	/// <summary>
	/// 文件数据
	/// </summary>
	private List<CSVData> _Datas;

	public CSVData Header {
		get { 
			return _Header;
		}
	}

	/// <summary>
	/// 数据数量
	/// </summary>
	/// <value>The count.</value>
	public int Count {
		get { 
			return _Datas.Count;
		}
	}

	public CSVReader(string header) {
		_Header = new CSVData (header);
		_Datas = new List<CSVData> ();
	}

	/// <summary>
	/// 添加数据
	/// </summary>
	/// <param name="item">Item.</param>
	public void Append(string item)
	{
		if (string.IsNullOrEmpty (item)) {
			return;
		}

		_Datas.Add(new CSVData(item));
	}

	/// <summary>
	/// 获取数据
	/// </summary>
	/// <param name="index">Index.</param>
	public CSVData this[int index] {
		get { 
			if (index < 0 || _Datas.Count <= index) {
				return null;
			}

			return _Datas [index];
		}
	}


	/// <summary>
	/// 生成解析器
	/// </summary>
	/// <param name="filepath">Filepath.</param>
	public static CSVReader Load(string filepath)
	{
		string filedata = ResourceManger.Instance.GetString (filepath);
		if (string.IsNullOrEmpty (filedata)) {
			return null;
		}

		StringReader stringReader = new StringReader (filedata);
		string data = stringReader.ReadLine ();
		if (string.IsNullOrEmpty (data)) {
			return null;
		}

		CSVReader reader = new CSVReader(data);
		while (true) {
			data = stringReader.ReadLine();
			if (string.IsNullOrEmpty (data)) {
				break;
			}

			reader.Append(data);
		}
		return reader;
	}
}

