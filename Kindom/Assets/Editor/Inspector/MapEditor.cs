using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapMake))]
public class MapEditor : DataInspectorEditor 
{
	/// <summary>
	/// 所在目录
	/// </summary>
	/// <value>The dir.</value>
	public static string Dir {
		get { 
			return "Resources/Map/";
		}
	}

	[MenuItem("Custom/Inspector/Map Design")]
	public static void AddMapInspector()
	{
		Transform firstChild = GetFirstSelectChild();
		if (firstChild == null) {
			return;
		}

		MapMake mm = AppendComponent<MapMake>(firstChild);
		if (mm == null) {
			return;
		}

		mm.Url = Dir + firstChild.name + ".bytes";
	}

	public override void OnInspectorGUI ()
	{
		MapMake make = GetTarget<MapMake> ();
		EditorGUILayout.BeginVertical ();
		make.Width = EditorGUILayout.IntField ("Width", make.Width);
		make.Length = EditorGUILayout.IntField ("Length", make.Length);
		make.Persistence = EditorGUILayout.Slider ("Persistence", make.Persistence, 0, 32);
		make.Octaves = EditorGUILayout.IntSlider ("Octaves", make.Octaves, 0, 32);
		EditorGUILayout.EndVertical ();
		EditorGUILayout.Space ();
		if (GUILayout.Button ("Generate")) {
			Generate ();
		}
		base.OnInspectorGUI ();
	}

	private void Generate() {
		GetTarget<MapMake> ().Generate ();
	}
}
