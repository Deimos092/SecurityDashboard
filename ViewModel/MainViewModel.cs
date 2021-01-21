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

		ILogService Log => Service.CreateLog();
		IExceptionHandler ExceptionHandler => Service.CreateExeptionHandler();

		OpenFileDialog openFileDialog;
		SaveFileDialog saveFileDialog;
		public ObservableCollection<SmokeSensorViewModel> SmokeSensorCollection { get; set; }
		public ObservableCollection<FireSensorViewModel> FireSensorCollection { get; set; }
		public ObservableCollection<CombiSensorViewModel> CombiSensorCollection { get; set; }

		/// <summary>
		/// Initializes a new instance of the MainViewModel class.
		/// </summary>
		public MainViewModel()
		{
			ServiceConfig.Initialization();
			FileManager = JSONReader.Create();
			SeriesCollection = new SeriesCollection();
			Sensors = new List<SensorViewModel>();

			SmokeSensorCollection = new ObservableCollection<SmokeSensorViewModel>();
			FireSensorCollection = new ObservableCollection<FireSensorViewModel>();
			CombiSensorCollection = new ObservableCollection<CombiSensorViewModel>();

			Generate = new RelayCommand(GenerateCollection);
			SaveFile = new RelayCommand(SaveCollection);
			LoadFile = new RelayCommand(LoadCollection);
		}
		public SeriesCollection SeriesCollection { get; set; }

		public List<SensorViewModel> Sensors { get; set; }

		public RelayCommand Generate { get; set; }

		public RelayCommand SaveFile { get; set; }

		public RelayCommand LoadFile { get; set; }

		private JSONReader FileManager { get; set; }

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
					List<Sensor> Collection = Sensors.Select(x => x.Sensor).ToList();
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
		private void LoadCollection()
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

					collection.ForEach(sensor => Sensors.Add(new SensorViewModel(sensor)));
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

				var collection = GetSensorsOfType<SmokeSensor>();
				foreach (var item in collection)
				{
					SmokeSensorCollection.Add(new SmokeSensorViewModel(item as SmokeSensor));
					SeriesCollection.Add(BuildChartFor(item as SmokeSensor));
				}

				collection = GetSensorsOfType<FireSensor>();
				foreach (var item in collection)
				{
					FireSensorCollection.Add(new FireSensorViewModel(item as FireSensor));
					SeriesCollection.Add(BuildChartFor(item as FireSensor));
				}

				collection = GetSensorsOfType<CombiSensor>();
				foreach (var item in collection)
				{
					CombiSensorCollection.Add(new CombiSensorViewModel(item as CombiSensor));
					SeriesCollection.Add(BuildChartFor(item as CombiSensor));
				}

				OnPropertyChanged("SeriesCollection");
				OnPropertyChanged("FireSensorCollection");
				OnPropertyChanged("SmokeSensorCollection");
				OnPropertyChanged("CombiSensorCollection");
			}
			catch (Exception ex)
			{
				var log = ExceptionHandler.Handler(ex);
				Log.WriteLog(log);
				MessageBox.Show(log, "Error! ", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}
		private List<object> GetSensorsOfType<T>()
		{
			var collection = Sensors
				.Where(item => item.Sensor.GetType() == typeof(T))
				.Select(item => Convert.ChangeType(item.Sensor, typeof(T)))
				.ToList();
			return collection;
		}

		private ColumnSeries BuildChartFor(Sensor sensor)
		{
			CartesianMapper<SensorViewModel> mapper = Mappers.Xy<SensorViewModel>()
			 .X((item) => 10) 
			 .Y(item => 100)
			 .Fill((item) => item.Temperatures.Max() > 99 ? Brushes.Red : Brushes.Blue);
			

			var series = new ColumnSeries
			{
				Title = sensor.Name,
				Configuration = mapper,
				Values = new ChartValues<double>(sensor.Temperatures)
			};
			return series;
		}
	}
}