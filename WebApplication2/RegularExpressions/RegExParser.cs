using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace ResumeApp.RegularExpressions
{
	// Data type to represent a regular expression
	public abstract class RegEx
	{
		internal static RegEx blank;
	}

	// Parser class
	public class RegExParser
	{

		public String input;

		public RegExParser(String input)
		{
			this.input = input;
		}
		
		public RegEx parse()
		{
			return(this.regex());
		}

		// Recursive descent parsing literals
		private char peek()
		{
			return input[0];
		}

		private void eat(char c)
		{
			if (peek() == c)
			{
				this.input = this.input.Substring(1);
			}
			else
			{
				throw new Exception("Expected: " + c + "; got: " + peek());
			}
		}

		private char next()
		{
			char c = peek();
			eat(c);
			return c;
		}

		private bool more()
		{
			if (input.Length > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		// Regular expression term types
		private RegEx regex()
		{
			RegEx term = this.term();
			
			if (more() && peek() == '|')
			{
				eat('|');
				RegEx regex = this.regex();
				return new Choice(term, regex);
			}
			else
			{
				return term;
			}
		}

		private RegEx term()
		{
			RegEx factor = RegEx.blank;
			while (more() && peek() != ')' && peek() != '|')
			{
				RegEx nextFactor = this.factor();
				factor = new Sequence(factor, nextFactor);
			}
			return factor;
		}

		private RegEx factor()
		{
			RegEx regexBase = this.regexBase();
			while (more() && peek() == '*')
			{
				eat('*');
				regexBase = new Repetition(regexBase);
			}
			return regexBase;
		}

		private RegEx regexBase()
		{
			switch (peek())
			{
				case '(':
					eat('(');
					RegEx r = regex();
					eat(')');
					return r;
				case '\\':
					eat('\\');
					char esc = next();
					return new Primitive(esc);
				default:
					return new Primitive(next());
			}
		}
	}
}