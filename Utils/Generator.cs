using SecurityDashboard.Interfaces;
using SecurityDashboard.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityDashboard.Utils
{
	public class Generator
	{
		static string Smokename = "Дымовой #";
		static string Firename = "Тепловой #";
		static string Combiname = "Комбинированый #";
		static Random lvl = new Random();
		/// <summary>
		/// Generates the sensors.
		/// </summary>
		/// <param name="size">The size.</param>
		/// <returns>A list of Sensors.</returns>
		public static List<Sensor> GetSensors(int count = 3)
		{
			List<Sensor> Collection = new List<Sensor>();
			Random Temp = new Random();

			for (int i = 0; i < count; i++)
			{
				SmokeSensor smokeSensor = new SmokeSensor($"{Smokename}{i + 1}", lvl.Next(0, 99));
				CombiSensor combiSensor = new CombiSensor($"{Combiname}{i + 1}", lvl.Next(CombiSensor.MinTemp, CombiSensor.MaxTemp));
				FireSensor fireSensor = new FireSensor($"{Firename}{i + 1}");

				smokeSensor.Temperatures = GenTemperatureFor(smokeSensor, Temp);
				combiSensor.Temperatures = GenTemperatureFor(combiSensor, Temp);
				fireSensor.Temperatures = GenTemperatureFor(fireSensor, Temp);

				Collection.Add(smokeSensor);
				Collection.Add(combiSensor);
				Collection.Add(fireSensor);
			}

			return Collection;
		}


		/// <summary>
		/// Gets the collection of temp.
		/// </summary>
		/// <param name="sensor">The sensor.</param>
		/// <returns>A list of double.</returns>
		public static List<double> GenTemperatureFor(Sensor sensor, Random temprnd)
		{
			switch (sensor)
			{
				case SmokeSensor s:
					return Enumerable.Range(0, 24)
						.Select(r => (double)temprnd.Next(SmokeSensor.MinTemp, SmokeSensor.MaxTemp))
						.ToList();
				case CombiSensor c:
					return Enumerable.Range(0, 24)
						.Select(r => (double)temprnd.Next(CombiSensor.MinTemp, CombiSensor.MaxTemp))
						.ToList();
				case FireSensor f:
					return Enumerable.Range(0, 24)
						.Select(r => (double)temprnd.Next(FireSensor.MinTemp, FireSensor.MaxTemp))
						.ToList();
			}
			return new List<double>();
		}
	}
}
