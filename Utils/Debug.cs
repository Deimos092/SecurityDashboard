using System;
using System.IO;
using System.Security;
using System.Text;
using System.Threading;

using SecurityDashboard.Interfaces;

namespace SecurityDashboard.Utils
{
	[SecuritySafeCritical]
	public class Debug : ILogService
	{
		private const int MaxLine = 1000;
		private static readonly StringBuilder StringBuilder = new StringBuilder();
		private static readonly object Obj = new object();
		private static string _path = string.Empty;
		private static Debug _instance;

		// -------------------------- Конструкторы ----------------------------
		//---------------------------------------------------------------------
		private Debug() { }

		/// <summary>
		/// Метод применение паттерна Singleton для инициализации обьекта
		/// Нужно для того что бы все логи собирались в одном месте
		/// И был всего 1 обьект по всей программе
		/// </summary>
		/// <returns></returns>
		public static Debug Create()
		{
			return _instance ?? (_instance = new Debug());
		}

		// -------------------------- Публичные поля --------------------------
		//---------------------------------------------------------------------
		/// <summary>
		/// Путь где будут собираться логи программы
		/// Если путь не задан, то по умолчанию будет собираться в папке где и была запущена программа.
		/// Если задано пустое значение или ошибка, то собираться будет по пути
		/// "C:\Debug (название программы)\LogInfo.txt"
		/// </summary>
		public string FilePath
		{
			get
			{
				if (string.IsNullOrEmpty(_path))
					_path = Path.Combine($@"{Directory.GetCurrentDirectory()}\", FileName);
				return _path;
			}
			set
			{
				if (!string.IsNullOrEmpty(_path))
					_path = string.Format($@"{Path.GetDirectoryName(value)}\{FileName}");
				else
					_path = Path.Combine($@"C:\Debug ({AppDomain.CurrentDomain.FriendlyName.Replace(".exe", "")})\", FileName);
			}
		}
		public string FileName { get; set; } = $"Log {DateTime.Now.ToShortDateString()}.txt";

		//---------------------------------------------------------------------
		// -------------------------- Методы ----------------------------------
		//---------------------------------------------------------------------

		private void WriteFileLog(string content)
		{
			bool lockWasTaken = false;
			try
			{
				Monitor.Enter(Obj, ref lockWasTaken);
				{
					if (!File.Exists(FilePath))
						File.Create(FilePath);
					File.AppendAllText(FilePath, StringBuilder.ToString());
				}
			}
			finally
			{
				if (lockWasTaken) Monitor.Exit(Obj);
			}
		}

		public void WriteLog(string source)
		{
			try
			{
				StringBuilder.AppendLine($"{DateTime.Now:dd.MM.yyyy HH:mm:ss} {source}");
				if (StringBuilder.Length <= MaxLine)
					WriteFileLog(StringBuilder.ToString());
				StringBuilder.Clear();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message); //Выбрасываем ошибку на верхний уровень
			}
		}
		public void WriteLog(string source, Exception ex)
		{
			StringBuilder.AppendLine(ex != null
				? $"{DateTime.Now:dd.MM.yyyy HH:mm:ss} {source} {ex.GetBaseException()} {Environment.NewLine}"
				: $"{DateTime.Now:dd.MM.yyyy HH:mm:ss} {source} {Environment.NewLine}");
			WriteFileLog(StringBuilder.ToString());
			StringBuilder.Clear();
		}
	}
}
