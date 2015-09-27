using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResumeApp.RegularExpressions
{
	class Choice : RegEx
	{
		private RegEx thisOne;
		private RegEx thatOne;

		public Choice (RegEx thisOne, RegEx thatOne)
		{
			this.thisOne = thisOne;
			this.thatOne = thatOne;
		}
	}
}