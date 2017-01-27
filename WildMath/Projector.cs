using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WildMath
{
	public class PrimeProjector
	{
		public static double Project(Vexel vex)
		{
			double val = 1;

			foreach(KeyValuePair<int, int> kvp in vex.Elements)
				val *= Math.Pow(kvp.Key, kvp.Value);

			return val;
		}
	}
}
