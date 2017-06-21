using System;
using System.IO;

public class ByteWriter
{
	MemoryStream _MemorySteam;

	public ByteWriter (int capacity)
	{
		_MemorySteam = new MemoryStream (capacity); 
	}

	private void Write(byte[] bytes) 
	{
		if (bytes == null || bytes.Length == 0) {
			return;
		}

		_MemorySteam.Write (bytes, 0, (int)bytes.Length);
	}

	/// <summary>
	/// 获取内存数据
	/// </summary>
	/// <returns>The array.</returns>
	public byte[] ToArray()
	{
		return _MemorySteam.ToArray ();
	}

	/// <summary>
	/// 关闭
	/// </summary>
	public void Close()
	{
		_MemorySteam.Close ();
	}

	public void Write(bool value)
	{
		Write (BitConverter.GetBytes (value));
	}

	public void Write(char value)
	{
		Write (BitConverter.GetBytes (value));
	}

	public void Write(sbyte value)
	{
		Write (BitConverter.GetBytes (value));
	}

	public void Write(byte value)
	{
		Write (BitConverter.GetBytes (value));
	}

	public void Write(short value)
	{
		Write (BitConverter.GetBytes (value));
	}

	public void Write(ushort value)
	{
		Write (BitConverter.GetBytes (value));
	}

	public void Write(int value)
	{
		Write (BitConverter.GetBytes (value));
	}

	public void Write(uint value)
	{
		Write (BitConverter.GetBytes (value));
	}

	public void Write(long value)
	{
		Write (BitConverter.GetBytes (value));
	}

	public void Write(ulong value)
	{
		Write (BitConverter.GetBytes (value));
	}

	public void Write(float value)
	{
		Write (BitConverter.GetBytes (value));
	}

	public void Write(double value)
	{
		Write (BitConverter.GetBytes (value));
	}
}

