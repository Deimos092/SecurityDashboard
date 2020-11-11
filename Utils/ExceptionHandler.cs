using System;
using System.Security;
using System.Text;

using SecurityDashboard.Interfaces;

namespace SecurityDashboard.Utils
{
	[SecuritySafeCritical]
	public class ExceptionHandler : IExceptionHandler
	{
		private static ExceptionHandler _instance;


		// -------------------------- Конструкторы ----------------------------
		//---------------------------------------------------------------------
		public ExceptionHandler()
		{ }
		/// <summary>
		/// Метод применение паттерна Singleton для инициализации обьекта
		/// Нужно для того что бы все ошибки собирались в одном месте
		/// И был всего 1 обьект по всей программе
		/// </summary>
		/// <returns></returns>
		public static ExceptionHandler Create()
		{
			return _instance ?? (_instance = new ExceptionHandler());
		}

		// -------------------------- Публичные поля --------------------------
		//---------------------------------------------------------------------
		public delegate Exception ExeptionDelegate();
		public ExeptionDelegate ChainExeptionDelegate { get; set; } = null;
		/// <summary>
		/// Current Time hh.mm.ss
		/// </summary>
		public string ExTime => DateTime.Now.ToShortTimeString();
		/// <summary>
		/// Current Date dd.mm.yyyy
		/// </summary>
		public string ExDate => DateTime.Now.ToString("dd.MM.yyyy");


		//---------------------------------------------------------------------
		// -------------------------- Методы ----------------------------------
		//---------------------------------------------------------------------
		/// <summary>
		/// Обработка сообщения об ошибке
		/// </summary>
		/// <param name="message">Возвращает строковую переменную </param>
		/// <returns></returns>
		public string Handler(string message)
		{
			return $"{ExDate}:{ExTime} Message {message}";
		}

		/// <summary>
		/// Обработка ошибки в формат {Сообщение} {Источиник} {Метод вызвавший ошибку}
		/// </summary>
		/// <param name="ex">Обьект ошибки</param>
		/// <returns></returns>
		public string Handler(Exception ex)
		{
			return $"\n\t {ex.Message}\n\tSource: {ex.Source}\n\tTarget: {ex.TargetSite}";
		}

		/// <summary>
		/// Возвращает цепочку ошибок из делегата в формате {Сообщение} {Источиник} {Метод вызвавший ошибку}
		/// </summary>
		/// <returns></returns>
		public string AllExeptions()
		{
			StringBuilder stringBuilder = new StringBuilder("Errors");

			foreach (var exception in ChainExeptionDelegate.GetInvocationList())
			{
				if (exception.Target is Exception ex)
				{
					stringBuilder.AppendLine($"Message: {ex.Message}, Source: {ex.Source}, Target:{ex.TargetSite}");
				}
			}

			//=== Подчеркнем ===
			stringBuilder.AppendLine($"{new string('=', 10),-12} {DateTime.Now.ToShortDateString()} {new string('=', 10),-12}");

			return stringBuilder.ToString();
		}
	}
}
