using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityDashboard.Interfaces
{
	public interface IExceptionHandler
	{
		string AllExeptions();
		string Handler(Exception exception);
		string Handler(string source);
	}
}
