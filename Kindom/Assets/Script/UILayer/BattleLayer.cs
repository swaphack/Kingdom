using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleLayer : UILayer
{
	private int _TeamID;
	private int _unitID;
	private int _FormationID;

	private List<string> _Teams;
	private List<string> _Units;
	private List<KeyValuePair<string, string>> _Formations;
	/// <summary>
	/// 当前队伍
	/// </summary>
	private Team _Team;
	/// <summary>
	/// 地图
	/// </summary>
	public Transform Field;

	public BattleLayer()
	{
		_Teams = new List<string>();
		_Teams.Add("红队");
		_Teams.Add("绿队");

		_Units = new List<string>();
		_Units.Add("战士");
		_Units.Add("骑兵");
		_Units.Add("射手");
		_Units.Add("投石车");

		_Formations = new List<KeyValuePair<string, string>> ();
		_Formations.Add (new KeyValuePair<string, string> ("鱼鳞阵", "Formation/f1"));
		_Formations.Add (new KeyValuePair<string, string> ("锋矢阵", "Formation/f2"));
		_Formations.Add (new KeyValuePair<string, string> ("鹤翼阵", "Formation/f3"));
		_Formations.Add (new KeyValuePair<string, string> ("偃月阵", "Formation/f4"));
		_Formations.Add (new KeyValuePair<string, string> ("方圆阵", "Formation/f5"));
		_Formations.Add (new KeyValuePair<string, string> ("雁形阵", "Formation/f6"));
		_Formations.Add (new KeyValuePair<string, string> ("长蛇阵", "Formation/f7"));
		_Formations.Add (new KeyValuePair<string, string> ("衡轭阵", "Formation/f8"));
	}

	// Use this for initialization
	void Start ()
	{
		UIDropdown dropDown = FindControlByName<UIDropdown> ("DTeam");
		dropDown.RemoveAllOptions ();
		dropDown.AddOption ("红队");
		dropDown.AddOption ("绿队");
		dropDown.OnValueChanged.AddListener((int arg0) => {
			SetTeamID(arg0);
		});

		dropDown.EnableOption (0);

		dropDown = FindControlByName<UIDropdown> ("DUnit");
		dropDown.RemoveAllOptions ();
		dropDown.AddOption ("战士");
		dropDown.AddOption ("骑兵");
		dropDown.AddOption ("射手");
		dropDown.AddOption ("投石车");
		dropDown.OnValueChanged.AddListener((int arg0) => {
			SetUnitID(arg0);
		});

		dropDown.EnableOption (0);

		dropDown = FindControlByName<UIDropdown> ("DFormation");
		dropDown.RemoveAllOptions ();
		for (int i = 0; i < _Formations.Count; i++) {
			dropDown.AddOption (_Formations [i].Key);
		}
		dropDown.OnValueChanged.AddListener((int arg0) => {
			SetFormationID(arg0);
		});

		dropDown.EnableOption (0);

		UIButton button = FindControlByName<UIButton> ("Button");
		button.OnClick.AddListener (() => {
			RemoveAllChildren(Field);
			_Team = CreateTeam();
		});

		TouchListener.Instance.AddDispatch (Field.gameObject, OnTouchHandler);
	}

	void OnTouchHandler(Vector3 pos)
	{
		if (_Team == null) {
			return;
		}

		_Team.MoveTo (pos);
	}

	private void SetTeamID(int teamID) {
		_TeamID = teamID;	
	}

	private void SetUnitID(int unitID) {
		_unitID = unitID;
	}

	private void SetFormationID(int formationID) {
		_FormationID = formationID;
	}

	/// <summary>
	/// 创建队伍
	/// </summary>
	private Team CreateTeam()
	{
		if (Field == null) {
			return null;
		}

		Formation f = CreateForamtion (_Formations [_FormationID].Value);
		if (f == null) {
			return null;
		}

		GameObject go = new GameObject ();
		Team team = go.AddComponent<Team> ();
		team.name = _TeamID.ToString();
		team.Formation.Copy (f);
		UILayer.AddChild (Field, team);

		for (int i = 0; i < f.Count; i++) {
			GameObject child = GameObject.CreatePrimitive (PrimitiveType.Sphere);
			Unit unit = child.AddComponent<Unit> ();
			unit.Initialize ();
			child.transform.position = f.GetPoint (i);
			UILayer.AddChild (team, unit);
		}

		return team;
	}

	/// <summary>
	/// 创建一个新阵型
	/// </summary>
	/// <returns>The foramtion.</returns>
	/// <param name="url">URL.</param>
	public static Formation CreateForamtion(string url)
	{
		byte[] bytes = ResourceManger.Instance.GetBytes (url);
		if (bytes == null || bytes.Length == 0) {
			return null;
		}

		ByteReader reader = new ByteReader (bytes);
		int childCount = reader.Read<int> ();

		Formation f = new Formation ();
		for (int i = 0; i < childCount; i++) {
			Vector3 pos = reader.ReadVector3 ();
			f.AddPoint (pos);
		}

		return f;
	}
}

