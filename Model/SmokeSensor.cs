using SecurityDashboard.Interfaces;
using SecurityDashboard.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityDashboard.Model
{
	public class SmokeSensor : Sensor
	{
		int _smokelevel;
		public SmokeSensor()
		{ }

		public SmokeSensor(string name,int smokelevel) : base(name)
		{
			Level = smokelevel;
		}

		public static int MaxTemp { get => 55; }

		public static int MinTemp { get => -30; }
		
		public static double CriticalLevel { get => 75; }

		public int Level
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
			string result = string.Format($"{Name} {string.Join(" ", Temperatures)} {Level}");
			return result;
		}
	}
}
