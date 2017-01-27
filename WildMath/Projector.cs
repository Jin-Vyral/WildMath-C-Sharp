using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WildMath
{
	public class Projector
	{
		public virtual void ProjectVexel(Vexel vex)
		{
			throw new NotImplementedException();
		}

		public virtual void ProjectMaxel(Maxel max)
		{
			throw new NotImplementedException();
		}
	}

	public class PrimeProjector
	{
		public PrimeProjector()
		{
			screen = 0;
		}

		public virtual void ProjectVexel(Vexel vex)
		{
			double val = 1;

			foreach(KeyValuePair<int, int> kvp in vex.Elements)
				val *= Math.Pow(kvp.Key, kvp.Value);

			screen = val;
		}

		public double screen;
	}
}
