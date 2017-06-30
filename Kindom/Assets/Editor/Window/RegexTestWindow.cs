using UnityEditor;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Collections;
using System;
using System.Text;

/// <summary>
/// 正则表达式测试窗口
/// </summary>
public class RegexTestWindow : EditorWindow
{
	/// <summary>
	/// 待检测文本
	/// </summary>
	private string text;
	/// <summary>
	/// 正则表达式
	/// </summary>
	private string pattern;
	/// <summary>
	/// 替换
	/// </summary>
	private string replacement;
	/// <summary>
	/// 匹配结果
	/// </summary>
	private string matchResult;
	/// <summary>
	/// 替换结果
	/// </summary>
	private string replaceResult;
	/// <summary>
	/// 创建、显示窗体
	/// </summary>
	[@MenuItem("Custom/Window/Regex Test Window")]
	private static void Init()
	{    
		RegexTestWindow window = (RegexTestWindow)EditorWindow.GetWindow(typeof(RegexTestWindow), true, "RegexTestWindow");
		window.minSize = new Vector2 (400, 530);
		window.Show ();
	}

	/// <summary>
	/// 显示窗体里面的内容
	/// </summary>
	private void OnGUI()
	{
		Rect rect = this.position;

		EditorGUILayout.LabelField ("Text:");
		text = EditorGUILayout.TextArea (text, GUILayout.Height(200));

		EditorGUILayout.Space ();
		EditorGUILayout.BeginHorizontal ();

		EditorGUILayout.BeginVertical ();
		EditorGUILayout.LabelField ("Regex Pattern:");
		pattern = EditorGUILayout.TextArea (pattern);
		if (GUILayout.Button ("Match")) {
			Match ();	
		}
		EditorGUILayout.LabelField ("Result:");
		matchResult = EditorGUILayout.TextArea (matchResult, GUILayout.Height(200));
		EditorGUILayout.EndVertical ();

		EditorGUILayout.BeginVertical ();
		EditorGUILayout.LabelField ("Replace Pattern:");
		replacement = EditorGUILayout.TextArea (replacement);
		if (GUILayout.Button ("Replace")) {
			Replace ();	
		}
		EditorGUILayout.LabelField ("Result:");
		replaceResult = EditorGUILayout.TextArea (replaceResult, GUILayout.Height(200));
		EditorGUILayout.EndHorizontal ();
	}

	/// <summary>
	/// 替换
	/// </summary>
	private	void Replace() {
		if (string.IsNullOrEmpty (text) || string.IsNullOrEmpty (pattern)) {
			return;
		}

		try {
			replaceResult = Regex.Replace (text, pattern, replacement);
		} catch(Exception e) {
			replaceResult = e.Message;
		}
	}

	/// <summary>
	/// 查找
	/// </summary>
	private void Match() {
		if (string.IsNullOrEmpty (text) || string.IsNullOrEmpty (pattern)) {
			return;
		}

		StringBuilder builder = new StringBuilder ();

		try {
			Match match = Regex.Match (text, pattern);
			while (match.Success) {
				System.Console.WriteLine("Match=[" + match + "]");
				CaptureCollection cc = match.Captures;
				foreach (Capture c in cc)
				{
					builder.Append("Capture=[" + c + "]");
				}
				builder.Append("\n");
				for (int i = 0; i < match.Groups.Count; i++)
				{
					System.Text.RegularExpressions.Group group = match.Groups[i];
					System.Console.WriteLine("\tGroups[{0}]=[{1}]", i, group);
					builder.Append("\n");
					for (int j = 0; j < group.Captures.Count; j++)
					{
						Capture capture = group.Captures[j];
						builder.AppendFormat("\t\tCaptures[{0}]=[{1}]", j, capture);
					}
				}

				builder.Append("\n");

				match = match.NextMatch();
			}
			matchResult = builder.ToString();
		} catch(Exception e) {
			matchResult = e.Message;
		}
	}
}

