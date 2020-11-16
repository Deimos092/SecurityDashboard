using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityDashboard.Model
{
	public class CombiSensor : Sensor
	{
		double _carbonMonoxid;
		double _temperature;

		public CombiSensor()
		{

		}

		public CombiSensor(string name, double carbonLevel) :base(name)
		{
			CarbonMonoxidLevel = carbonLevel;
		}

		public static int MaxTemp { get => 50; }

		public static int MinTemp { get => -10; }

		public static double CriticalLevel { get => 50001.0; }

		public double CarbonMonoxidLevel
		{
			get { return _carbonMonoxid; }
			set
			{
				if (value >= 0 && value <= 100000.0)
					_carbonMonoxid = value;
				else
					_carbonMonoxid = -1;
			}
		}
	}
}
