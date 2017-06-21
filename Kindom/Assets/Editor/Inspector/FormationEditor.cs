using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FormationMake))]
public class FormationEditor : DataInspectorEditor 
{
	/// <summary>
	/// 所在目录
	/// </summary>
	/// <value>The dir.</value>
	public static string Dir {
		get { 
			return "Resources/Formation/";
		}
	}

	[MenuItem("Custom/Inspector/Formation Design")]
	private static void AddFormationInspector()
	{
		Transform firstChild = GetFirstSelectChild();
		if (firstChild == null) {
			return;
		}

		FormationMake fm = AppendComponent<FormationMake>(firstChild);
		if (fm == null) {
			return;
		}

		fm.Url = Dir + firstChild.name + ".bytes";
	}
}
