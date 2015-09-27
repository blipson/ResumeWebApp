using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResumeApp.RegularExpressions
{
	class Repetition : RegEx
	{
		private RegEx internalRegex;

		public Repetition(RegEx internalRegex)
		{
			this.internalRegex = internalRegex;
		}
	}
}