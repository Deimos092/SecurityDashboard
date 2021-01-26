using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Controls;
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
using System.Text;

namespace SecurityDashboard.ViewModel
{

	public class MainViewModel : BaseViewModel
	{
		private StringBuilder logview;
		ILogService Log => Service.CreateLog();
		IExceptionHandler ExceptionHandler => Service.CreateExeptionHandler();

		OpenFileDialog openFileDialog;
		SaveFileDialog saveFileDialog;

		public SeriesCollection SmokeSensorCollection { get; set; }
		public SeriesCollection FireSensorCollection { get; set; }
		public SeriesCollection CombiSensorCollection { get; set; }
		/// <summary>
		/// Initializes a new instance of the MainViewModel class.
		/// </summary>
		public MainViewModel()
		{
			ServiceConfig.Initialization();
			FileManager = JSONReader.Create();

			Sensors = new List<SensorViewModel>();
			logview = new StringBuilder();

			SmokeSensorCollection = new SeriesCollection();
			FireSensorCollection = new SeriesCollection();
			CombiSensorCollection = new SeriesCollection();

			Generate = new RelayCommand(GenerateCollection);
			SaveFile = new RelayCommand(SaveCollection);
			LoadFile = new RelayCommand(LoadCollection);

			LogView = $"Services Initialization";
		}

		public string LogView
		{
			get { return logview.ToString(); }
			set
			{
				if (!string.IsNullOrEmpty(value))
					logview.AppendLine($"{DateTime.Now} : {value}");
				OnPropertyChanged("LogView");
			}
		}
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
				saveFileDialog.FileName = "DataOfSensors";
				saveFileDialog.Title = "Сохранение данных в файл";
				saveFileDialog.FilterIndex = 0;
				// -----------------------------------
				bool? isOk = saveFileDialog.ShowDialog() ?? false;
				if (isOk.Value)
				{
					FileManager.FilePath = Path.GetDirectoryName(saveFileDialog.FileName);
					List<Sensor> collection = Sensors.Select(x => x.Sensor).ToList();
					FileManager.Save(collection);
					LogView = $"Save data to {FileManager.FilePath}";
				}
				
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
					LogView = $"Load data from {FileManager.FilePath}";

					List<Sensor> collection = FileManager.Read(FileManager.FilePath);
					collection.ForEach(sensor => Sensors.Add(new SensorViewModel(sensor)));
					UpdateCharts();
				}

			}
			catch (Exception ex)
			{
				var log = ExceptionHandler.Handler(ex);
				Log.WriteLog(log);
				MessageBox.Show(log, "Error! ", MessageBoxButton.OK, MessageBoxImage.Error);
				LogView = $"Error! {log}";
			}
		}
		private void GenerateCollection()
		{
			try
			{
				LogView = $"Generate new Charts";
				Sensors.Clear();
				Generator.GetSensors().ForEach(item => Sensors.Add(new SensorViewModel(item)));
				UpdateCharts(); //Строим графики 				
			}
			catch (Exception ex)
			{
				var log = ExceptionHandler.Handler(ex);
				Log.WriteLog(log);
				MessageBox.Show(log, "Error! ", MessageBoxButton.OK, MessageBoxImage.Error);
				LogView = $"Error! {log}";
			}
		}
		private void UpdateCharts()
		{
			try
			{
				LogView = $"Update Charts";

				SmokeSensorCollection.Clear();
				SmokeSensorCollection.AddRange(GetChartsOfType<SmokeSensor>());

				FireSensorCollection.Clear();
				FireSensorCollection.AddRange(GetChartsOfType<FireSensor>());

				CombiSensorCollection.Clear();
				CombiSensorCollection.AddRange(GetChartsOfType<CombiSensor>());

				OnPropertyChanged("FireSensorCollection");
				OnPropertyChanged("SmokeSensorCollection");
				OnPropertyChanged("CombiSensorCollection");
			}
			catch (Exception ex)
			{
				var log = ExceptionHandler.Handler(ex);
				Log.WriteLog(log);
				MessageBox.Show(log, "Error! ", MessageBoxButton.OK, MessageBoxImage.Error);
				LogView = $"Error! {log}";

			}
		}
		private List<LineSeries> GetChartsOfType<T>()
		{
			List<LineSeries> lineSeries = new List<LineSeries>();
			var collection = GetSensorsOfType<T>();

			foreach (var item in collection)
				lineSeries.Add(BuildChartFor((Sensor)item));

			return lineSeries;
		}
		private List<object> GetSensorsOfType<T>()
		{
			var collection = Sensors
				.Where(item => item.Sensor.GetType() == typeof(T))
				.Select(item => Convert.ChangeType(item.Sensor, typeof(T)))
				.ToList();
			return collection;
		}
		private LineSeries BuildChartFor(Sensor sensor)
		{
			IEnumerable<ObservablePoint> collection = sensor.Temperatures.Select((x, i) => new ObservablePoint(i++, x));

			var LineSeries = new LineSeries
			{
				Values = new ChartValues<ObservablePoint>(collection),
				PointGeometrySize = 10, //Толщина точек
				Title = sensor.Name, //Имя датчика
				LineSmoothness = 0.5, // Плавность перехода графика от точки к точке
			};
			return LineSeries;
		}
	}
}
