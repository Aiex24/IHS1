using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Setup_Manager.ViewModels
{
	class Initialize : ObservableObject
	{
		private string _source;
		private string _database;
		private string _password;
		private string _user;
		private bool _trustedConnection;
		private readonly ObservableCollection<string> _log = new ObservableCollection<string>();

		public ICommand StartProcessCommand
		{
			get { return new DelegateCommand(StartProgress); }
		}

		public void StartProgress()
		{
			Properties.Settings.Default.Reload();
			SetSource = Properties.Settings.Default.Source;
			SetDatabase = Properties.Settings.Default.Database;

			SetUser = "anka";
			SetTrustedConnection = true;

		}

		public string SetSource
		{
			get { return _source; }
			set
			{
				_source = value;
				RaisePropertyChangedEvent("SetSource");
			}
		}

		public string SetDatabase
		{
			get { return _database; }
			set
			{
				_database = value;
				RaisePropertyChangedEvent("SetDatabase");
			}
		}

		public string SetPassword
		{
			get { return _password; }
			set
			{
				_password = value;
				RaisePropertyChangedEvent("SetPassword");
			}
		}

		public string SetUser
		{
			get { return _user; }
			set
			{
				_user = value;
				RaisePropertyChangedEvent("SetUser");
			}
		}

		public bool SetTrustedConnection
		{
			get { return _trustedConnection; }
			set
			{
				if (_trustedConnection == value) return;
				_trustedConnection = value;
				RaisePropertyChangedEvent("SetTrustedConnection");
			}
		}

		public void Check()
		{
			_log.Add("Checking installed components...");
			var connString = ConfigurationManager.ConnectionStrings["CreateDbConnection"].ToString();

			SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connString);
			builder.InitialCatalog = "master";
			builder.DataSource = ".";
			builder.IntegratedSecurity = true;
			_log.Add(builder.ConnectionString);

			string commandText = "CREATE DATABASE test44";

			SqlConnection connection = new SqlConnection(builder.ConnectionString);

			if (CheckDatabaseExists(builder.ConnectionString, "test44"))
			{
				_log.Add("Database already exist");
			}
			else
			{
				_log.Add("Creating Database");
			}
			//using (connection)
			//{
			//	SqlCommand command = new SqlCommand(commandText, connection);
			//	//command.Parameters.Add("%dataBase", SqlDbType.NVarChar);
			//	//command.Parameters["%dataBase"].Value = database;

			//	try
			//	{
			//		connection.Open();
			//		Int32 rowsAffected = command.ExecuteNonQuery();
			//	}
			//	catch (Exception ex)
			//	{
			//		_log.Add(ex.ToString());
			//	}
			//}
		}


		public void Start()
		{
			MessageBox.Show("Application started");
		}

		public IEnumerable<string> Log
		{
			get { return _log; }
		}

		public ICommand StartCommand
		{
			get { return new DelegateCommand(Check); }
		}

		public ICommand SaveChangesCommand
		{
			get { return new DelegateCommand(SaveChanges); }
		}

		private void SaveChanges()
		{
			_log.Add("Saving...");
			Properties.Settings.Default.Source = _source;
			Properties.Settings.Default.Save();
			Properties.Settings.Default.Reload();
			_log.Add("Saving complete...");
		}

		/*  public object DataContext { get; private set; */

		public static bool CheckDatabaseExists(string connectionString, string databaseName)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				using (var command = new SqlCommand($"SELECT db_id('{databaseName}')", connection))
				{
					connection.Open();
					return (command.ExecuteScalar() != DBNull.Value);
				}
			}
		}
	}


}
