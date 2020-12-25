using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SecurityDashboard.Interfaces;
using SecurityDashboard.Utils;

namespace SecurityDashboard.Model
{
	public abstract class Sensor : ISensor
	{
		string _name;
		List<double> _temperatures;
		ILogService Log => Service.CreateLog();
		IExceptionHandler ExceptionHandler => Service.CreateExeptionHandler();

		/// <summary>
		/// Initializes a new instance of the <see cref="Alarm"/> class.
		/// </summary>
		public Sensor()
		{
			_name = string.Empty;
			_temperatures = new List<double>();
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Alarm"/> class.
		/// </summary>
		/// <param name="name">Имя датчика</param>
		public Sensor(string name)
		{
			Name = name;
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
		public List<double> Temperatures
		{
			get { return _temperatures; }
			set 
			{
				if (value != null)
					_temperatures = value;
				else
					_temperatures = new List<double>();
			}
		}

		public override string ToString()
		{
			string result = string.Format($"{Name} {Temperatures}");
			return result;
		}
	}
}
