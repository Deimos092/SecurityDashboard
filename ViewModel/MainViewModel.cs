using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using SecurityDashboard.Model;
using SecurityDashboard.Utils;

using System;
using System.Collections.ObjectModel;
using System.Windows;
using SecurityDashboard.Interfaces;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Defaults;
using System.Windows.Media;
using LiveCharts.Wpf;

namespace SecurityDashboard.ViewModel
{

	public class MainViewModel : BaseViewModel
	{
		List<SmokeSensor> smokeSensors;
		List<FireSensor> fireSensors;
		List<CombiSensor> combiSensors;
		ILogService Log => Service.CreateLog();
		IExceptionHandler ExceptionHandler => Service.CreateExeptionHandler();

		OpenFileDialog openFileDialog;
		SaveFileDialog saveFileDialog;
		public ObservableCollection<SensorViewModel> Sensors { get; set; }

		/// <summary>
		/// Initializes a new instance of the MainViewModel class.
		/// </summary>
		public MainViewModel()
		{
			ServiceConfig.Initialization();
			FileManager = JSONReader.Create();
			Sensors = new ObservableCollection<SensorViewModel>();

			Generate = new RelayCommand(GenerateCollection);
			SaveFile = new RelayCommand(SaveCollection);
			ReadFile = new RelayCommand(ReadCollection);
		}

		public RelayCommand Generate { get; set; }

		public RelayCommand SaveFile { get; set; }

		public RelayCommand ReadFile { get; set; }

		private JSONReader FileManager { get; set; }

		public SeriesCollection SmokeSensors { get; set; }

		//public SeriesCollection FireSensors
		//{
		//	get { return fireSensors; }
		//	set
		//	{
		//		fireSensors = value;
		//		OnPropertyChanged("FireSensors");
		//	}
		//}
		//public SeriesCollection CombiSensors
		//{
		//	get { return combiSensors; }
		//	set
		//	{
		//		combiSensors = value;
		//		OnPropertyChanged("CombiSensors");
		//	}
		//}


		private void SaveCollection()
		{
			try
			{
				saveFileDialog = new SaveFileDialog();
				// -----------------------------------
				saveFileDialog.Filter = "Json file (.json)|*.json|All Files(.*)|*.*";
				saveFileDialog.FileName = "Путь до файла";
				saveFileDialog.Title = "Сохранение данных в файл";
				saveFileDialog.FilterIndex = 0;
				// -----------------------------------
				bool? isOk = saveFileDialog.ShowDialog() ?? false;
				if (isOk.Value)
				{
					List<Sensor> Collection = Sensors.Select(x => x.sensor).ToList();
					FileManager.FilePath = Path.GetDirectoryName(openFileDialog.FileName);
					FileManager.Save(Collection);
				}
				else
					throw new Exception("Указан не правильный путь файла");
			}
			catch (Exception ex)
			{
				var log = ExceptionHandler.Handler(ex);
				Log.WriteLog(log);
				MessageBox.Show(log, "Error! ", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}
		private void ReadCollection()
		{
			try
			{
				openFileDialog = new OpenFileDialog();
				// ------------------------------------
				openFileDialog.Filter = "Json file (.json)|*.json|All Files(.*)|*.*";
				openFileDialog.FileName = "Путь до файла";
				openFileDialog.Title = "Загрузить данные с файла";
				openFileDialog.FilterIndex = 0;
				openFileDialog.DefaultExt = FileManager.FilePath;
				// ------------------------------------

				bool? isOk = openFileDialog.ShowDialog() ?? false;
				if (isOk.Value)
				{
					Sensors.Clear();
					FileManager.FilePath = Path.GetDirectoryName(openFileDialog.FileName);
					List<Sensor> collection = FileManager.Read(FileManager.FilePath);

					collection.ForEach(x => Sensors.Add(new SensorViewModel(x)));
				}
			}
			catch (Exception ex)
			{
				var log = ExceptionHandler.Handler(ex);
				Log.WriteLog(log);
				MessageBox.Show(log, "Error! ", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}
		private void GenerateCollection()
		{
			try
			{
				Sensors.Clear();
				Generator.GetSensors().ForEach(item => Sensors.Add(new SensorViewModel(item)));

		//		CartesianMapper<SensorViewModel> mapper = Mappers.Xy<SensorViewModel>()
		//		 .X((item) => (double)item.DateTimes[11].Ticks / TimeSpan.FromMinutes(5).Ticks)
		//		 .Y(item => item.Temperature.Max());

		//		var series = new ColumnSeries()
		//		{
		//			Configuration = mapper,
		//			Values = new ChartValues<SmokeSensor>
	 // {
		//new SmokeSensor() {Temperature = 10, Value = 100},
		//new SmokeSensor() {Timestamp = DateTime.Now.AddMinutes(15), Value = 78},
		//new SmokeSensor() {Timestamp = DateTime.Now.AddMinutes(30), Value = 21}
	 // }
		//		};



				//Sensors
				//.Where(item => item.sensor.GetType() == typeof(SmokeSensor)).Select(item => (SmokeSensor)item.sensor)
				//.ToList();

				//FireSensors = Sensors
				//	.Where(item => item.sensor.GetType() == typeof(FireSensor)).Select(item => (FireSensor)item.sensor)
				//	.ToList();

				//CombiSensors = Sensors
				//	 .Where(item => item.sensor.GetType() == typeof(CombiSensor)).Select(item => (CombiSensor)item.sensor)
				//	 .ToList();

				OnPropertyChanged("FireSensors");
				OnPropertyChanged("SmokeSensors");
				OnPropertyChanged("CombiSensors");

			}
			catch (Exception ex)
			{
				var log = ExceptionHandler.Handler(ex);
				Log.WriteLog(log);
				MessageBox.Show(log, "Error! ", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}
	}
}