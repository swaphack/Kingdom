using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(CameraBehaviour))]
public class CameraEditor : BaseEditor
{
	private CameraBehaviour _CameraBehaviour;

	private int RespondType = 0;
	private string[] RespondTypeString = new string[]{ "Transfer", "Rotation" };

	private int RotationType = 0;
	private string[] RotationTypeString = new string[]{"Self", "World"};


	private int[] Options = new int[] {0, 1, 2};

	void OnEnable() {
		_CameraBehaviour = (CameraBehaviour)target as CameraBehaviour;
	}

	/// <summary>
	/// 显示面板
	/// </summary>
	public override void OnInspectorGUI ()
	{
		EditorGUILayout.Space ();

		RespondType = EditorGUILayout.IntPopup ("Respond Type", RespondType, RespondTypeString, Options);
		_CameraBehaviour.RespondType = (CameraBehaviour.TouchRespondType)RespondType;
		switch (_CameraBehaviour.RespondType) {
		case CameraBehaviour.TouchRespondType.Rotation:
			RotationType = EditorGUILayout.IntPopup ("Transfer Rate", RotationType, RotationTypeString, Options);
			_CameraBehaviour.FixedHorizontalRotation = EditorGUILayout.Toggle ("Fixed Horizontal Rotation", _CameraBehaviour.FixedHorizontalRotation);
			_CameraBehaviour.FixedVerticalRotation = EditorGUILayout.Toggle ("Fixed Vertical Rotation", _CameraBehaviour.FixedVerticalRotation);
			_CameraBehaviour.ScrollRate = EditorGUILayout.FloatField ("Transfer Rate", _CameraBehaviour.ScrollRate);
			break;
		case CameraBehaviour.TouchRespondType.Transfer:
			_CameraBehaviour.TransferRate = EditorGUILayout.FloatField ("Transfer Rate", _CameraBehaviour.TransferRate);
			break;
		}

		Flush ();
	}
}

