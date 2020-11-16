using SecurityDashboard.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityDashboard.Interfaces
{
	public interface IJSONReader
	{
		bool Save(List<Sensor> sensorsCollection);

		List<Sensor> Read(string path);
	}
}
