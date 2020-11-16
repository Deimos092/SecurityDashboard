using GalaSoft.MvvmLight;
using System.Collections.Generic;

using SecurityDashboard.Model;

using System;
using System.Collections.Generic;
using System.Linq;

namespace SecurityDashboard.ViewModel
{
	public class SensorViewModel : ViewModelBase
	{
		private readonly Sensor _sensor;
		public List<DateTime> dateTimes;

		public SensorViewModel(Sensor sensor)
		{
			_sensor = sensor;
			dateTimes = Enumerable.Range(0, 24).Select(x => DateTime.Parse(x.ToString("HH:mm:ss"))).ToList();
		}

		public string SensorName
		{
			get { return _sensor.Name; }
		}

		public List<DateTime> DateTimes
		{
			get{ return dateTimes; }
		}
	}
}
