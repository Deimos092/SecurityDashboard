using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityDashboard.Model
{
	public class FireSensor : Sensor
	{
		double _temperature;

		public FireSensor()
		{ }

		public FireSensor(string name) : base(name)
		{
			
		}
		
		public static int MaxTemp { get => 46; }

		public static int MinTemp { get => -40; }

		public override string ToString()
		{
			string result = string.Format($"{Name} {string.Join(" ",Temperature)}");
			return result;
		}
	}
}
