using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SecurityDashboard.Utils;

namespace SecurityDashboard.Utils
{
	class ServiceConfig
	{
		public static void Initialization()
		{
			Service.CreateLog = () => Debug.Create();
			Service.CreateExeptionHandler = () => ExceptionHandler.Create();
		}
	}
}
