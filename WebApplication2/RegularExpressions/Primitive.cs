using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResumeApp.RegularExpressions
{
	class Primitive : RegEx
	{
		private char c;

		public Primitive(char c)
		{
			this.c = c;
		}
	}
}