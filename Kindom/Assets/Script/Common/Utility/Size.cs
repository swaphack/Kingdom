using UnityEngine;
using System.Collections;

[System.Serializable]
public struct Size
{
	public float Width;
	public float Height;

	public Size(float w, float h)
	{
		Width = w;
		Height = h;
	}

	public override bool Equals(object o) {
		if (o is Size) {
			Size s = (Size)o;
			return this.Width == s.Width && this.Height == s.Height;
		} else {
			return base.Equals (o);
		}
	}

	public override int GetHashCode ()
	{
		return base.GetHashCode ();
	}


	public static bool operator==(Size s1, Size s2) {
		return s1.Width == s2.Width && s1.Height == s2.Height;
	}

	public static bool operator!= (Size s1, Size s2) {
		return s1.Width != s2.Width || s1.Height != s2.Height;
	}
}

