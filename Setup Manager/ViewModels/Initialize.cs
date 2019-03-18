using Setup_Manager.Models;
using Setup_Manager.Models.Elements;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Documents;
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
		private EButton _cb= new EButton();
		private readonly ObservableCollection<string> _log = new ObservableCollection<string>();
		private int _selectedIndex;

		public ICommand StartProcessCommand
		{
			get { return new DelegateCommand(StartProcess); }
		}

		public ICommand InstallCommand
		{
			get { return new DelegateCommand(Check); }
		}

		public void StartProcess()
		{
			SelectedIndex = 1;
			CB.Content = "< Back";
			CB.IsEnabled = false;
			var settings = new ReadSettings();
			ConnectionSettings conSettings = new ReadSettings().Get();
			
			settings.Reload();
				
			SetSource = conSettings.Source;
			SetDatabase = conSettings.Database;
			SetUser = conSettings.User;
			SetPassword = conSettings.Password;
			SetTrustedConnection = conSettings.TrustedConnection;
		}

		public EButton CB
		{
			get { return _cb; }
			set
			{
				_cb = value;
				RaisePropertyChangedEvent("CB");
			}
		}

		public int SelectedIndex
		{
			get { return _selectedIndex; }
			set
			{
				_selectedIndex = value;
				RaisePropertyChangedEvent("SelectedIndex");
			}
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
			var connString = ConfigurationManager.ConnectionStrings["CreateDbConnection"].ToString();
			SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connString);
			builder.InitialCatalog = "master";
			builder.DataSource = SetSource;
			builder.IntegratedSecurity = SetTrustedConnection;

			string commandText = $"CREATE DATABASE {SetDatabase}";

			SqlConnection connection = new SqlConnection(builder.ConnectionString);

			if (CheckDatabaseExists(builder.ConnectionString, SetDatabase))
			{
				MessageBox.Show("Database already exist");
			}
			else
			{
				using (connection)
				{
					SqlCommand command = new SqlCommand(commandText, connection);
					try
					{
						connection.Open();
						Int32 rowsAffected = command.ExecuteNonQuery();
						MessageBox.Show("Install complete");
					}
					catch (Exception ex)
					{
						_log.Add(ex.ToString());
					}
				}
			}


		}

		public IEnumerable<string> Log
		{
			get { return _log; }
		}

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
