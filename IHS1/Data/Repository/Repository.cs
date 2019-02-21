using IHS1.Data.Interfaces;
using IHS1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IHS1.Data.Repository
{
		public abstract class Repository<T> : IRepository<T> where T : class
		{
			protected readonly IHS1DbContext _context;
			public Repository(IHS1DbContext context)
			{
				_context = context;
			}

			public IEnumerable<T> GetSettings()
			{
				return _context.Set<T>();
			}
		}
}
