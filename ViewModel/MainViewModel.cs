using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;
using System.Windows.Input;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using SecurityDashboard.Model;
using SecurityDashboard.Utils;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using SecurityDashboard.Interfaces;
using System.ComponentModel;

namespace SecurityDashboard.ViewModel
{

	public class MainViewModel : BaseViewModel
	{
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

		public List<SmokeSensor> SmokeSensors { get; set; }
		public List<FireSensor> FireSensors { get; set; }
		public List<CombiSensor> CombiSensors { get; set; }


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
				SmokeSensors = Sensors.Where(x =>
				{
					

				}).ToList();

				OnPropertyChanged("SmokeSensors");
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