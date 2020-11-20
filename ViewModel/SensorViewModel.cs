using GalaSoft.MvvmLight;
using System.Collections.Generic;

using SecurityDashboard.Model;

using System;
using System.Linq;

namespace SecurityDashboard.ViewModel
{
	public class SensorViewModel : ViewModelBase
	{
		internal Sensor sensor;
		public List<DateTime> dateTimes;

		/// <summary>
		/// Initializes a new instance of the <see cref="SensorViewModel"/> class.
		/// </summary>
		/// <param name="sensor">The sensor.</param>
		public SensorViewModel(Sensor sensor)
		{
			this.sensor = sensor;
			dateTimes = Enumerable.Range(0, 24).Select(x => DateTime.Parse(x.ToString("HH:mm:ss"))).ToList();
		}

		public string Name => sensor.Name;
		public List<double> Temperature => sensor.Temperature;

		public List<DateTime> DateTimes
		{
			get{ return dateTimes; }
		}
	}
}
