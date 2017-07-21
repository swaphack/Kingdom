using System;
using Common.Document;
using System.Collections.Generic;

namespace Geography.Map.Document.JSON
{
	/// <summary>
	/// 文档筛选
	/// </summary>
	public class JSONFilter : DocFilter
	{
		public JSONFilter()
		{
			this.AddParser (new JSONParser ());
		}

		private static JSONFilter s_Instance;

		public static JSONFilter Instance {
			get { 
				if (s_Instance == null) {
					s_Instance = new JSONFilter ();
				}
				return s_Instance;
			}
		}
	}
}

