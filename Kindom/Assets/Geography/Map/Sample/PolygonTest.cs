using UnityEngine;
using System.Collections;
using Common.CG;

public class PolygonTest : MonoBehaviour
{
	public Vector2[] vertexes = new Vector2[] {
		new Vector2(0,0),
		new Vector2(5,0),
		new Vector2(5,5),
		new Vector2(2.5f, 2.5f),
		new Vector2(0,5),
	};

	// Use this for initialization
	void Start ()
	{
		Polygon p = new Polygon ();
		p.SetVertexes (vertexes);
		Polygon[] ps = p.MakeMonotone ();

		for (int i = 0 ; i < ps.Length; i++) {
			Vector2[] vs = ps [i].Vertexes.ToArray ();
			for (int j = 0; j < vs.Length; j++) {
				Debug.Log (vs [i].x + "," + vs [i].y);
			}
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

