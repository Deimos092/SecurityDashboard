using GalaSoft.MvvmLight;
using SecurityDashboard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityDashboard.ViewModel
{
	public class CombiSensorViewModel:ViewModelBase
	{
		public List<DateTime> dateTimes;

		public CombiSensorViewModel(CombiSensor smokeSensor)
		{
			var time = DateTime.Now;
			CombiSensor = smokeSensor;
		}
		public double CriticalLvl { get => CombiSensor.CriticalLevel; }

		public double Level { get { return CombiSensor.Level; } set { CombiSensor.Level = value; } }

		public List<double> Temperatures => CombiSensor.Temperatures;

		public CombiSensor CombiSensor { get; private set; }
	}
}
