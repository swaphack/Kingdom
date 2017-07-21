using System;
using Common.Document;

namespace Geography.Map.Document.JavaScript
{
	public class JSFilter : DocFilter
	{
		public JSFilter()
		{
			this.AddParser (new JSParser ());
		}

		private static JSFilter s_Instance;

		public static JSFilter Instance {
			get { 
				if (s_Instance == null) {
					s_Instance = new JSFilter ();
				}
				return s_Instance;
			}
		}
	}
}

