using UnityEditor;
using UnityEngine;
using System.Collections;

/// <summary>
/// 重新设置文件格式
/// </summary>
public class ChangeFileFormat : EditorWindow
{
	/// <summary>
	/// 新的文件格式
	/// </summary>
	private string newFileFormat;
	/// <summary>
	/// 创建、显示窗体
	/// </summary>
	[@MenuItem("Custom/Window/Reset File Format")]
	private static void Init()
	{    
		ChangeFileFormat window = (ChangeFileFormat)EditorWindow.GetWindow(typeof(ChangeFileFormat), true, "RegexTestWindow");
		window.minSize = new Vector2 (400, 530);
		window.Show ();
	}

	/// <summary>
	/// 显示窗体里面的内容
	/// </summary>
	private void OnGUI()
	{
		Rect rect = this.position;

		EditorGUILayout.LabelField ("Select Directory To Change File Format.");
		newFileFormat = EditorGUILayout.TextField ("New Format", newFileFormat);
		if (GUILayout.Button ("Match")) {
			Change ();	
		}
	}


	/// <summary>
	/// 获取选择的贴图
	/// </summary>
	/// <returns></returns>
	private Object[] GetSelectedTextures()
	{
		return Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
	}

	/// <summary>
	/// 改变文件格式
	/// </summary>
	private void Change()
	{
		Object[] textures = GetSelectedTextures();
		Selection.objects = new Object[0];
	}
}

