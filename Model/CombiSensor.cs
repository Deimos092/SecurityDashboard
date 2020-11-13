using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityDashboard.Model
{
	class CombiSensor : Sensor
	{
		double _carbonMonoxid;
		double _temperature;

		public CombiSensor()
		{

		}

		public CombiSensor(double carbonLevel, double temp, string name):base(name)
		{
			CarbonMonoxidLevel = carbonLevel;
			Temperature = temp;
		}


		public double Temperature
		{
			get { return _temperature; }
			set
			{
				if (value >= -10 && value <= 50)
					_temperature = value;
				else
					_temperature = double.NaN;
			}
		}

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
