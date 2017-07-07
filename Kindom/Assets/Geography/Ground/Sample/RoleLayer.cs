using UnityEngine;
using System.Collections;
using Geography.Ground;

namespace Geography.Ground.Sample
{

	/// <summary>
	/// 角色层
	/// </summary>
	public class RoleLayer : Layer
	{
		private Team _team;

		// Use this for initialization
		void Start ()
		{		
			GameObject go = new GameObject ();
			Team team = go.AddComponent<Team> ();
			team.AddTo (this.transform);
			team.Formation.AddPoint (new Vector3 (2, 1, 2));
			team.Formation.AddPoint (new Vector3 (2, 1, 4));
			team.Formation.AddPoint (new Vector3 (2, 1, 6));
			team.Formation.AddPoint (new Vector3 (4, 1, 2));
			team.Formation.AddPoint (new Vector3 (4, 1, 4));
			team.Formation.AddPoint (new Vector3 (4, 1, 6));
			team.Formation.AddPoint (new Vector3 (6, 1, 2));
			team.Formation.AddPoint (new Vector3 (6, 1, 4));
			team.Formation.AddPoint (new Vector3 (6, 1, 6));

			team.Add<Unit> (CreateRole (new Vector3 (10, 1, 2)));
			team.Add<Unit> (CreateRole (new Vector3 (-40, 1, 1)));
			team.Add<Unit> (CreateRole (new Vector3 (-40, 1, 40)));
			team.Add<Unit> (CreateRole (new Vector3 (10, 1, 22)));
			team.Add<Unit> (CreateRole (new Vector3 (30, 1, 2)));
			team.Add<Unit> (CreateRole (new Vector3 (10, 1, 42)));
			team.Add<Unit> (CreateRole (new Vector3 (10, 1, 12)));

			team.BuildUp ();

			_team = team;
		}

		private Unit CreateRole (Vector3 pos)
		{
			GameObject go = GameObject.CreatePrimitive (PrimitiveType.Sphere);
			go.transform.position = pos;
			go.AddComponent<ModelBehaviour> ();
			Unit unit = go.AddComponent<Unit> ();
			unit.Initialize ();
			return unit;
		}

		public override bool OnTouchModel (Vector3 touchPosition)
		{
			_team.MoveTo (touchPosition);

			return true;
		}
	}


}