using TimeTable.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTable.Data
{
	public interface ISqlDataProvider
	{
		ISqlContext SqlContext { get; }
		IDisposable CreateContext();
	}
}
