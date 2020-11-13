using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityDashboard.Model
{
	class SmokeSensor : Sensor
	{
		int _smokelevel;
		double _temperature;

		public SmokeSensor()
		{ }

		public SmokeSensor(string name, double temp, int smokelevel) : base(name)
		{
			SmokeLevel = smokelevel;
			Temperature = temp;
		}

		public double Temperature
		{
			get { return Temperature; }
			set
			{
				if (value >= -30 && value <= 55)
					_temperature = value;
				else
					_temperature = double.NaN;
			}
		}
		public int SmokeLevel
		{
			get { return _smokelevel; }
			set
			{
				if (value >= 0 && value <= 100)
					_smokelevel = value;
				else 
					_smokelevel = -1;
			}
		}

		public override string ToString()
		{
			string result = string.Format($"{Name} {Temperature} {SmokeLevel}");
			return result;
		}
	}
}
