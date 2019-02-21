using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IHS1.Data.Interfaces
{
	public interface IRepository<T> where T : class
	{
		IEnumerable<T> GetSettings();
	}
}
