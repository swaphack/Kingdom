using UnityEngine;
using System.Collections;
using UnityEditor;

/// <summary>
/// 数据面板编辑器
/// 所有二进制文件均要.bytes结尾
/// </summary>
public class DataInspectorEditor : BaseEditor
{
	/// <summary>
	/// 所在目录
	/// </summary>
	/// <value>The dir.</value>
	public string Url;
	/// <summary>
	/// 编辑器生成器
	/// </summary>
	protected EditorMake _EditorMake;
	/// <summary>
	/// 获取对象
	/// </summary>
	/// <returns>The target.</returns>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public T GetTarget<T>() where T : EditorMake {
		return (T)target;
	}

	/// <summary>
	/// 默认存储目录
	/// </summary>
	/// <value>The dir.</value>
	public virtual string Dir {
		get { 
			return "";
		}
	}

	void OnEnable() {
		_EditorMake = (EditorMake)target as EditorMake;
		if (string.IsNullOrEmpty (_EditorMake.Url)) {
			_EditorMake.Url = Dir + _EditorMake.name + ".bytes";
		}
		Url = _EditorMake.Url;
	}

	/// <summary>
	/// 显示面板
	/// </summary>
	public override void OnInspectorGUI ()
	{
		EditorGUILayout.Space ();

		EditorGUILayout.BeginHorizontal ();
		EditorGUILayout.LabelField ("Scene");
		if (GUILayout.Button ("Remove All GameObjects")) {
			Clear ();
		}
		EditorGUILayout.EndHorizontal ();

		EditorGUILayout.Space ();

		Url = EditorGUILayout.TextField ("Filepath:", Url);
		EditorGUILayout.BeginHorizontal ();
		if (GUILayout.Button ("Save")) {
			Save ();
		}
		if (GUILayout.Button ("Load")) {
			Load ();
		}
		EditorGUILayout.EndHorizontal ();

		Flush ();
	}

	/// <summary>
	/// 清空数据
	/// </summary>
	private void Clear() {
		if (_EditorMake == null) {
			return;
		}
		_EditorMake.RemoveAllChildren ();
	}

	/// <summary>
	/// 保存数据
	/// </summary>
	private void Save() 
	{
		if (_EditorMake == null || string.IsNullOrEmpty(Url)) {
			return;
		}

		_EditorMake.Url = Url;
		_EditorMake.Save ();
	}

	/// <summary>
	/// 加载数据
	/// </summary>
	private void Load() 
	{
		if (_EditorMake == null || string.IsNullOrEmpty(Url)) {
			return;
		}

		_EditorMake.Url = Url;
		_EditorMake.Load ();
	}
}

