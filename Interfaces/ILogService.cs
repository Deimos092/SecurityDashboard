using System;

namespace SecurityDashboard.Interfaces
{
	public interface ILogService
	{
		string FileName { get; set; }

		void WriteLog(string source);

		void WriteLog(string source, Exception ee);
	}
}
