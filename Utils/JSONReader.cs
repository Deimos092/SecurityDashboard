using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Newtonsoft.Json;
using SecurityDashboard.Model;

namespace SecurityDashboard.Utils
{
	class JSONReader
	{
		List<Sensor> SensorsCollection = new List<Sensor>();
		private static readonly string _fileName = "DataJSON.txt";
		private static string _path = string.Empty;

		/// <summary>
		/// Saves the.
		/// </summary>
		/// <param name="sensorsCollection">The sensors collection.</param>
		/// <returns>A bool.</returns>
		public bool Save(List<Sensor> sensorsCollection )
		{
			using(StreamWriter streamWriter = new StreamWriter(FileName))
			{
				string result = JsonConvert.SerializeObject(SensorsCollection);

				streamWriter.WriteAsync(result);
			}
			return true;
		}

		/// <summary>
		/// Reads the.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns>A bool.</returns>
		public bool Read(string path)
		{
			using (StreamReader streamReader = new StreamReader(path))
			{
				string jsonfile = streamReader.ReadToEnd();

				var collection = JsonConvert.DeserializeObject<List<Sensor>>(jsonfile);

				SensorsCollection = collection;
			}
			return true;
		}
		/// <summary>
		/// Путь где будут собираться данные программы
		/// Если путь не задан, то по умолчанию будет собираться в папке где и была запущена программа.
		/// Если задано пустое значение или ошибка, то собираться будет по пути
		/// "C:\Data (название программы)\DataJSON.txt"
		/// </summary>
		public string FileName
		{
			get
			{
				if (string.IsNullOrEmpty(_path))
					_path = Path.Combine($@"{Directory.GetCurrentDirectory()}\", _fileName);
				return _path;
			}
			set
			{
				if (!string.IsNullOrEmpty(_path))
					_path = string.Format($@"{value}\{_fileName}");
				else
					_path = Path.Combine($@"C:\Data ({AppDomain.CurrentDomain.FriendlyName.Replace(".exe", "")})\", _fileName);
			}
		}
	}
}
