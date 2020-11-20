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
		static string Smokename = "Дымовой датчик ";
		static string Firename = "Тепловой датчик ";
		static string Combiname = "Комбинированый датчик ";
		ILogService Log => Service.CreateLog();
		IExceptionHandler ExceptionHandler => Service.CreateExeptionHandler();
		/// <summary>
		/// Generates the sensors.
		/// </summary>
		/// <param name="size">The size.</param>
		/// <returns>A list of Sensors.</returns>
		public static List<Sensor> GetSensors(int size = 9)
		{
			List<Sensor> Collection = new List<Sensor>();
			
			Random level = new Random();
			Random Temp = new Random();

				for(int i = 0; i < size; i++)
				{
					SmokeSensor smokeSensor = new SmokeSensor($"{Smokename}{i}", level.Next(0,99));
					CombiSensor combiSensor = new CombiSensor($"{Combiname}{i}", level.Next(CombiSensor.MinTemp, CombiSensor.MaxTemp));
					FireSensor fireSensor = new FireSensor($"{Firename}{i}");

					smokeSensor.Temperature = GetCollectionOfTemp(smokeSensor);
					combiSensor.Temperature = GetCollectionOfTemp(combiSensor);
					fireSensor.Temperature = GetCollectionOfTemp(fireSensor);

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
		private static List<double> GetCollectionOfTemp(Sensor sensor)
		{
			Random _rand = new Random();

			switch (sensor)
			{
				case SmokeSensor s:
					return Enumerable.Range(0, 24)
						.Select(r => (double)_rand.Next(SmokeSensor.MinTemp,SmokeSensor.MaxTemp))
						.ToList();
				case CombiSensor c:
					return Enumerable.Range(0, 24)
						.Select(r => (double)_rand.Next(CombiSensor.MinTemp, CombiSensor.MaxTemp))
						.ToList();
				case FireSensor f:
					return Enumerable.Range(0, 24)
						.Select(r => (double)_rand.Next(FireSensor.MinTemp, FireSensor.MaxTemp))
						.ToList();
			}
			return new List<double>();
		}
	}
}
