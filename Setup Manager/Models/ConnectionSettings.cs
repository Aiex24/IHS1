using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setup_Manager.Models
{
	public class ConnectionSettings
	{
		public string Source { get; set; }
		public string Database { get; set; }
		public string User { get; set; }
		public string Password { get; set; }
		public bool TrustedConnection { get; set; }

		public ConnectionSettings(string source, string database, string user, string password, bool trustedConnection)
		{
			Source = source;
			Database = database;
			User = user;
			Password = password;
			TrustedConnection = trustedConnection;
		}
	}
}
