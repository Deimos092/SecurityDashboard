using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityDashboard.Model
{
	class FireSensor : Sensor
	{
		double _temperature;

		public FireSensor()
		{ }

		public FireSensor(string name, double templevel) : base(name)
		{
			Temperature = templevel;
		}


		/// <summary>
		/// Gets or sets the temperature.
		/// </summary>
		public double Temperature
		{
			get { return _temperature; }
			set
			{
				if (value >= -40 && value <= 46)
					_temperature = value;
				else
					_temperature = double.NaN;
			}
		}

		public override string ToString()
		{
			string result = string.Format($"{Name} {Temperature}");
			return result;
		}
	}
}
