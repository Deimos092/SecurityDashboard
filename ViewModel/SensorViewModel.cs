using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Collections;
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
			var time = DateTime.Now;
			this.sensor = sensor;
			dateTimes = Enumerable.Range(0, 24)
				.Select(hour => new DateTime(time.Year, time.Month, time.Day, hour, 0, 0))
				.ToList();
		}

		public string Name => sensor.Name;
		public List<double> Temperature => sensor.Temperature;

		public List<DateTime> DateTimes
		{
			get{ return dateTimes; }
		}
	}
}
