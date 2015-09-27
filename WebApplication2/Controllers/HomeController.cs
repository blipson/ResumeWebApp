using System.Web.Mvc;
using System.Text.RegularExpressions;
using System;
using ResumeApp.RegularExpressions;

namespace WebApplication2.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult About()
		{
			return View();
		}

		public ActionResult Contact()
		{
			return View();
		}

		public ActionResult Hamming(string inputBits)
		{
			if (inputBits != null)
			{
				// Copy string into an int array
				var inputCode = new int[inputBits.Length];
				for (int i = 0; i < inputBits.Length; i++)
				{
					inputCode[i] = inputBits[i] - '0';
				}

				// Calculate the length of the hamming code
				int numberOfParityBits = 1;
				while ((inputBits.Length + numberOfParityBits + 1) >= Math.Pow(2, numberOfParityBits))
				{
					numberOfParityBits++;
				}
				int hammingLength = inputBits.Length + numberOfParityBits;
				var intermediateCode = new int[hammingLength];
				var hammingCode = new int[hammingLength];

				// Insert the parity bits (denoted as 2 because we don't know their value yet)
				int inputBitsPosition = 0;
				int parityBitsPosition = 0;
				for (var hammingPosition = 0; hammingPosition < hammingLength; hammingPosition++)
				{
					var currentParity = Math.Pow(2, parityBitsPosition);
					if (hammingPosition+1 == currentParity)
					{
						intermediateCode[hammingPosition] = 2;
						parityBitsPosition++;
					}
					else
					{
						intermediateCode[hammingPosition] = inputCode[inputBitsPosition];
						inputBitsPosition++;
					}
				}

				// Calculate the correct values for the parity bits
				var parityCalculator = 0;
				var parityBitsPositionCalculator = 1;
				parityBitsPosition = 1;
				int bitsConsumed = 0;
				for (int i=0; i<hammingLength; i++)
				{
					if (intermediateCode[i] == 2)
					{
						int j = i;
						while (j<hammingLength)
						{
							if (bitsConsumed >= parityBitsPosition)
							{
								bitsConsumed -= parityBitsPosition;
								j += parityBitsPosition - 1; // This is my favorite line
							}
							else
							{
								if (intermediateCode[j] == 1)
								{
									parityCalculator += 1;
								}
								bitsConsumed++;
							}
							j++;
						}
						if (parityCalculator % 2 == 0)
						{
							intermediateCode[i] = 0;
						}
						else
						{
							intermediateCode[i] = 1;
						}
						parityBitsPosition = (int)Math.Pow(2, parityBitsPositionCalculator);
						parityBitsPositionCalculator++;
						bitsConsumed = 0;
						parityCalculator = 0;
					}
				}

				ViewBag.str = string.Join("", intermediateCode);
			}
			else
			{
				ViewBag.str = "";
			}
			return View();
		}

		public ActionResult Regexp(string regexp)
		{
			ViewBag.description = "This page checks to see if a string is a valid regular expression.";

			if (IsRegexPatternValid(regexp))
			{
				RegExParser rep = new RegExParser(regexp);
				rep.parse();
				ViewBag.str = "Yes, " + regexp + " is a valid regular expression, " + rep.input;
			}
			else
			{
				ViewBag.str = "That isn't a valid regular expression. Backslashes break it.";
			}
			return View();
		}

		public static bool IsRegexPatternValid(string pattern)
		{
			try
			{
				pattern = @"\" + pattern;
				RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Compiled;
				new Regex(pattern, options);
				return true;
			}
			catch { }
			return false;
		}
	}
}