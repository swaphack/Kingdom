using UnityEngine;
using System;
using System.IO;
using System.Collections;

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

	public ByteReader(byte[] data)
	{
		if (data == null) {
			return;
		}
		_Data = data;
	}
		
	public T Read<T> () where T : struct, IConvertible
	{
		int size = System.Runtime.InteropServices.Marshal.SizeOf(typeof(T));
		if (_Data == null || _Position + size > Length) {
			return default(T);
		}

		IConvertible t = default(T);
		switch (t.GetTypeCode ()) {
		case TypeCode.Boolean:
			t = BitConverter.ToBoolean (_Data, _Position);
			break;
		case TypeCode.Char:
			t = BitConverter.ToChar (_Data, _Position);
			break;
		case TypeCode.SByte:
			t = _Data[_Position];
			break;
		case TypeCode.Byte:
			t = _Data[_Position];
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
			return default(T);
		}
		_Position += size;
		return (T)t;
	}
}

