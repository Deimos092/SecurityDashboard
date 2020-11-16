using System;
using System.Collections.Generic;
using System.IO;

using Newtonsoft.Json;
using SecurityDashboard.Model;
using SecurityDashboard.Interfaces;

namespace SecurityDashboard.Utils
{
	public class JSONReader : IJSONReader
	{
		 static JSONReader _instance;
		
		 static readonly string _fileName = "DataOfSensors.json";
		 static string _path = string.Empty;

		ILogService Log => Service.CreateLog();
		IExceptionHandler ExceptionHandler => Service.CreateExeptionHandler();

		/// <summary>
		/// Saves the.
		/// </summary>
		/// <param name="sensorsCollection">The sensors collection.</param>
		/// <returns>A bool.</returns>
		public bool Save(List<Sensor> sensorsCollection )
		{
			using(StreamWriter streamWriter = new StreamWriter(FilePath))
			{
				string result = JsonConvert.SerializeObject(sensorsCollection, Formatting.Indented, new JsonSerializerSettings
				{
					TypeNameHandling = TypeNameHandling.All
				});

				streamWriter.WriteLine(result);
			}
			return true;
		}

		/// <summary>
		/// Reads the.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns>A bool.</returns>
		public List<Sensor> Read(string path)
		{
			using (StreamReader streamReader = new StreamReader(path))
			{
				string jsonfile = streamReader.ReadToEnd();

				var collection = JsonConvert.DeserializeObject<List<Sensor>>(jsonfile, new JsonSerializerSettings
				{
					TypeNameHandling = TypeNameHandling.Auto
				});

				return collection;
			}
		}
		/// <summary>
		/// Путь где будут собираться данные программы
		/// Если путь не задан, то по умолчанию будет собираться в папке где и была запущена программа.
		/// Если задано пустое значение или ошибка, то собираться будет по пути
		/// "C:\Data (название программы)\DataJSON.txt"
		/// </summary>
		public string FilePath
		{
			get
			{
				if (string.IsNullOrEmpty(_path))
					return _path = Path.Combine($@"{Directory.GetCurrentDirectory()}\", _fileName);
				else
					return _path;
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
					_path = string.Format($@"{value}\{_fileName}");
				else
					_path = Path.Combine($@"C:\Data ({AppDomain.CurrentDomain.FriendlyName.Replace(".exe", "")})\", _fileName);
			}
		}
	}
}
