using UnityEngine;
using System.Collections.Generic;
using System;

namespace Common.CG
{
	/// <summary>
	/// 线段排序
	/// </summary>
	internal class Vector2Comparer : IComparer<Vector2>
	{
		public int Compare (Vector2 v0, Vector2 v1)
		{
			return Tool.Compare (v0, v1);
		}
	}

	public class Tool
	{
		public const float eps = 1e-6f;  

		/// <summary>
		/// 比较位置
		/// </summary>
		/// <param name="v0">V0.</param>
		/// <param name="v1">V1.</param>
		public static int Compare (Vector2 v0, Vector2 v1)
		{
			if (v0.y < v1.y) {
				return 1;
			} else if (v0.y > v1.y) {
				return -1;
			} else {
				if (v0.x < v1.x) {
					return -1;
				} else if (v0.x == v1.x) {
					return 0;
				} else {
					return 1;
				}
			}
		}

		/// <summary>
		/// Sgn the specified x.
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		public static int Signal(double x)  
		{  
			if (x < 0) {
				return -1;
			} else if (x == 0) {
				return 0;
			} else {
				return 1;
			}
		}  
		
		/// <summary>
		/// 交积
		/// </summary>
		/// <param name="p0">P0.</param>
		/// <param name="p1">P1.</param>
		/// <param name="p2">P2.</param>
		/// <param name="p3">P3.</param>
		public static float Cross(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3) 
		{
			Vector2 l01 = p1 - p0;
			Vector2 l23 = p3 - p2;
			return l01.x * l23.y - l01.y * l23.x;
		}

		/// <summary>
		/// 面积
		/// </summary>
		/// <param name="p0">P0.</param>
		/// <param name="p1">P1.</param>
		/// <param name="p2">P2.</param>
		public static float Area(Vector2 p0, Vector2 p1, Vector2 p2) {
			return Cross (p0, p1, p0, p2);
		}

		/// <summary>
		/// p0是否在p1上方
		/// </summary>
		/// <param name="p0">P0.</param>
		/// <param name="p1">P1.</param>
		public static bool Upper(Vector2 p0, Vector2 p1) {
			if (p0.y > p1.y) {
				return true;
			}

			if (p0.y == p1.y && p0.x < p1.x) {
				return true;
			}

			return false;
		}

		/// <summary>
		/// p0是否在p1下方
		/// </summary>
		/// <param name="p0">P0.</param>
		/// <param name="p1">P1.</param>
		public static bool Lower(Vector2 p0, Vector2 p1) {
			if (p0.y < p1.y) {
				return true;
			}

			if (p0.y == p1.y && p0.x > p1.x) {
				return true;
			}

			return false;
		}

		/// <summary>
		/// 求线段os和线段oe的夹角
		/// </summary>
		/// <param name="o">O.</param>
		/// <param name="s">S.</param>
		/// <param name="e">E.</param>
		public static float Angle (Vector2 o, Vector2 s, Vector2 e)
		{    
			float cosfi = 0, fi = 0, norm = 0;    
			float dsx = s.x - o.x;    
			float dsy = s.y - o.y;    
			float dex = e.x - o.x;    
			float dey = e.y - o.y;    
    
			cosfi = dsx * dex + dsy * dey;    
			norm = (dsx * dsx + dsy * dsy) * (dex * dex + dey * dey);    
			cosfi /= Mathf.Sqrt (norm);    
    
			if (cosfi >= 1.0) {
				return 0;    
			}
			if (cosfi <= -1.0) {
				return 180;    
			}
				
			fi = Mathf.Acos (cosfi);    
    
			if (180 * fi / Math.PI < 180) {    
				return 180 * fi / Mathf.PI;    
			} else {    
				return 360 - 180 * fi / Mathf.PI;    
			}    
		}
	}
}

