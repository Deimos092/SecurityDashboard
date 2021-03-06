﻿using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Collections;
using SecurityDashboard.Model;

using System;
using System.Linq;

namespace SecurityDashboard.ViewModel
{
	public class SensorViewModel : ViewModelBase
	{
		public List<DateTime> dateTimes;

		/// <summary>
		/// Initializes a new instance of the <see cref="SensorViewModel"/> class.
		/// </summary>
		/// <param name="sensor">The sensor.</param>
		public SensorViewModel(Sensor sensor)
		{
			var time = DateTime.Now;
			Sensor = sensor;
			dateTimes = Enumerable.Range(0, 24)
				.Select(hour => new DateTime(time.Year, time.Month, time.Day, hour, 0, 0))
				.ToList();
		}

		public string Name => Sensor.Name;

		public List<double> Temperatures => Sensor.Temperatures;

		public Sensor Sensor { get; set; }

		public List<DateTime> DateTimes
		{
			get{ return dateTimes; }
		}
	}
}
