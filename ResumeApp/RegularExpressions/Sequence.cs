using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResumeApp.RegularExpressions
{
	class Sequence : RegEx
	{
		private RegEx first;
		private RegEx second;

		public Sequence(RegEx first, RegEx second)
		{
			this.first = first;
			this.second = second;
		}
	}
}