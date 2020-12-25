using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Collections;
using SecurityDashboard.Model;

using System;
using System.Linq;

namespace SecurityDashboard.ViewModel
{
	public class FireSensorViewModel : ViewModelBase
	{
		public List<DateTime> dateTimes;

		/// <summary>
		/// Initializes a new instance of the <see cref="SensorViewModel"/> class.
		/// </summary>
		/// <param name="sensor">The sensor.</param>
		public FireSensorViewModel(FireSensor sensor)
		{
			var time = DateTime.Now;
			Sensor = sensor;
		}

		public string Name => Sensor.Name;

		public List<double> Temperatures => Sensor.Temperatures;

		public FireSensor Sensor { get; set; }

		public List<DateTime> DateTimes
		{
			get{ return dateTimes; }
		}
	}
}
