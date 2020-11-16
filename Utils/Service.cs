using System;

using SecurityDashboard.Interfaces;

namespace SecurityDashboard.Utils
{
	public static class Service
	{
		/// <summary>
		/// Сервис логов
		/// </summary>
		public static Func<ILogService> CreateLog { get; set; } = () => throw new NotImplementedException();

		/// <summary>
		/// Сервис обработки ошибок
		/// </summary>
		public static Func<IExceptionHandler> CreateExeptionHandler { get; set; } = () => throw new NotImplementedException();

		/// <summary>
		/// Сервис сбора и считывание данных
		/// </summary>
		public static Func<IJSONReader> CreateJSONReader { get; set; } = () => throw new NotImplementedException();
	}
}
