using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityDashboard.Model
{
	abstract class Sensor
	{
		string _name;
		double _temperature;

		/// <summary>
		/// Initializes a new instance of the <see cref="Alarm"/> class.
		/// </summary>
		public Sensor()
		{
			_name = string.Empty;
			_temperature = 0;
		}

		public Sensor(string name)
		{
			Name = name;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Alarm"/> class.
		/// </summary>
		/// <param name="name">Имя датчика</param>
		/// <param name="temp">Температура</param>
		public Sensor(string name, double temp)
		{
			Name = name;
			Temperature = temp;
		}

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		/// <summary>
		/// Gets or sets the temperature.
		/// </summary>
		public double Temperature
		{
			get { return _temperature; }
			set 
			{
				if (value >= -50 && value <= 100)
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
