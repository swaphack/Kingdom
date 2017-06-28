using UnityEngine;
using System.Collections;
using Geography.Ground;

namespace Geography.Ground.Sample
{
	

	public class Ground : GroundBase
	{
		protected override void Init ()
		{
			base.Init ();
			this.AddLayer<TurfLayer> (GROUND_OFFSET);
			this.AddLayer<BuildingLayer> (2 * GROUND_OFFSET);
			this.AddLayer<RoleLayer> (2 * GROUND_OFFSET);
		}
	}

}