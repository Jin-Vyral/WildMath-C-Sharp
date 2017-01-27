using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WildMath
{
	public class PrimeProjector
	{
		///<summary>
		/// Projects a (prime-factored) Vexel to a floating-point value
		///</summary>
		public static double Project(Vexel vex)
		{
			double val = 1;

			foreach(KeyValuePair<int, int> kvp in vex.Elements)
				val *= Math.Pow(kvp.Key, kvp.Value);

			return val;
		}

		///<summary>
		/// Projects a (prime-factored) Vexel to a rational number (as a Pixel)
		///</summary>
		public static Pixel ProjectRational(Vexel vex)
		{
			int x = 1;
			int y = 1;

			foreach(KeyValuePair<int, int> kvp in vex.Elements)
			{
				if(kvp.Value > 0)
					x *= (int)Math.Pow(kvp.Key, kvp.Value);
				else
					y *= (int)Math.Pow(kvp.Key, -kvp.Value);
			}

			return new Pixel(x, y);
		}

		///<summary>
		/// Prints a (prime-fectored) Vexel to a string
		///</summary>
		public static string ToString(Vexel vex)
		{
			string str = "[ ";

			foreach(KeyValuePair<int, int> elem in vex.Elements)
				str += elem.Key + "^" + elem.Value + " x ";

			return str.Substring(0, str.Length - 2) + "]";
		}
	}
}
