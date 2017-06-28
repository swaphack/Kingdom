using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 配置文件加载
/// </summary>
public class ConfigReader
{
	/// <summary>
	/// 单项
	/// </summary>
	public struct ConfigData
	{
		public string Name;
		public string Value;
		public ConfigData[] Table;
	}

	public ConfigData[] Datas;

	public ConfigReader()
	{
		
	}


	/// <summary>
	/// 生成解析器
	/// </summary>
	/// <param name="filepath">Filepath.</param>
	public static ConfigReader Load(string filepath)
	{
		string filedata = ResourceManger.Instance.GetString (filepath);
		if (string.IsNullOrEmpty (filedata)) {
			return null;
		}
	}
}

