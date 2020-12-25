using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecurityDashboard.Model;

namespace SecurityDashboard.ViewModel
{
	public class SmokeSensorViewModel : ViewModelBase
	{
		public List<DateTime> dateTimes;

		public SmokeSensorViewModel(SmokeSensor smokeSensor)
		{
			var time = DateTime.Now;
			SmokeSensor = smokeSensor;
		}

		public double CriticalLvl { get => SmokeSensor.CriticalLevel; }

		public int Level { get { return SmokeSensor.Level; } set { SmokeSensor.Level = value; } }

		public string Name { get => SmokeSensor.Name; set { SmokeSensor.Name = value; } }

		public List<double> Temperatures => SmokeSensor.Temperatures;

		public SmokeSensor SmokeSensor { get; private set; }
	}
}
