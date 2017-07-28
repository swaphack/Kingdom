using System;
using UnityEngine;

namespace Football
{
	public class Field : Entity
	{
		public Field ()
		{
		}

		public void AddTeam(Team team)
		{
			if (team == null) {
				return;
			}

			this.AddChild (team);
		}
	}
}

