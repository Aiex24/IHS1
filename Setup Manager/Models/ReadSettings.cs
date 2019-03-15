using Setup_Manager.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setup_Manager.Models
{
	class ReadSettings
	{
		public ConnectionSettings Get()
		{
			return new ConnectionSettings(
				Settings.Default.Source,
				Settings.Default.Database,
				Settings.Default.User,
				Settings.Default.Password,
				Settings.Default.TrustedConnection
			);
		}

		public void Reload()
		{
			Settings.Default.Reload();
		}

		public void Save(ConnectionSettings userSettings)
		{
			Settings.Default.Source = userSettings.Source;
			Settings.Default.Database = userSettings.Database;
			Settings.Default.User = userSettings.User;
			Settings.Default.TrustedConnection = userSettings.TrustedConnection;
			Settings.Default.Save();
		}
	}
}
