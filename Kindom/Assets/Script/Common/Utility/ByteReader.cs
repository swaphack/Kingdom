using UnityEngine;
using System;
using System.IO;
using System.Collections;

namespace Common.Utility
{

	public class ByteReader
	{
		private byte[] _Data;

		private int _Position;

		/// <summary>
		/// 读取位置
		/// </summary>
		/// <value>The position.</value>
		public int Position {
			get { 
				return _Position;
			}
			set {
				_Position = value;	
			}
		}

		/// <summary>
		/// 数据长度
		/// </summary>
		/// <value>The length.</value>
		public int Length {
			get { 
				if (_Data == null) {
					return 0;
				}
				return _Data.Length;
			}
		}

		public ByteReader (byte[] data)
		{
			if (data == null) {
				return;
			}
			_Data = data;
		}

		/// <summary>
		/// 查看大小
		/// </summary>
		/// <returns>The of.</returns>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		private int SizeOf<T> () where T : struct, IConvertible
		{
			int size = System.Runtime.InteropServices.Marshal.SizeOf (typeof(T));
			if (_Data == null || _Position + size > Length) {
				Debug.Assert (false, "ByteReader : Read " + typeof(T).ToString () + "overflow!");
				return 0;
			}

			return size;
		}

		/// <summary>
		/// 读取数据
		/// </summary>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public T Read<T> () where T : struct, IConvertible
		{
			IConvertible t = default(T);
			TypeCode typeCode = t.GetTypeCode ();
			if (typeCode == TypeCode.Empty
			   || typeCode == TypeCode.Object
			   || typeCode == TypeCode.DBNull
			   || typeCode == TypeCode.String) {
				Debug.Assert (false, "ByteReader : Read " + typeof(T).ToString () + " failure!");
				return default(T);
			}

			int size = SizeOf<T> ();
			switch (typeCode) {
			case TypeCode.Boolean:
				t = BitConverter.ToBoolean (_Data, _Position);
				break;
			case TypeCode.Char:
				t = BitConverter.ToChar (_Data, _Position);
				break;
			case TypeCode.SByte:
				t = _Data [_Position];
				break;
			case TypeCode.Byte:
				t = _Data [_Position];
				break;
			case TypeCode.Int16:
				t = BitConverter.ToInt16 (_Data, _Position);
				break;
			case TypeCode.UInt16:
				t = BitConverter.ToUInt16 (_Data, _Position);
				break;
			case TypeCode.Int32:
				t = BitConverter.ToInt32 (_Data, _Position);
				break;
			case TypeCode.UInt32:
				t = BitConverter.ToUInt32 (_Data, _Position);
				break;
			case TypeCode.Int64:
				t = BitConverter.ToInt64 (_Data, _Position);
				break;
			case TypeCode.UInt64:
				t = BitConverter.ToUInt64 (_Data, _Position);
				break;
			case TypeCode.Single:
				t = BitConverter.ToSingle (_Data, _Position);
				break;
			case TypeCode.Double:
				t = BitConverter.ToDouble (_Data, _Position);
				break;
			default:
				Debug.Assert (false, "ByteReader : Unknow Type " + typeof(T).ToString () + "!");
				return default(T);
			}
			_Position += size;
			return (T)t;
		}

		/// <summary>
		/// 读取字符串
		/// </summary>
		/// <returns>The string.</returns>
		public string ReadString ()
		{
			int length = Read<int> ();
			if (_Position + length > Length) {
				Debug.Assert (false, "ByteReader : Read " + typeof(string).ToString () + "overflow!");
				return null;
			}

			String value = null;
			if (length != 0) {
				value = BitConverter.ToString (_Data, _Position, length);
			}
			_Position += length;

			return value;
		}

		/// <summary>
		/// 读取向量
		/// </summary>
		/// <returns>The vector3.</returns>
		public Vector3 ReadVector3 ()
		{
			float x = Read<float> ();
			float y = Read<float> ();
			float z = Read<float> ();

			return new Vector3 (x, y, z);
		}
	}

}