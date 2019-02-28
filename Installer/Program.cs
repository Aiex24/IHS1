using System;
using System.Text.RegularExpressions;
using UtilitiesMisc;

namespace Installer
{
    class Program
    {
		private const string connStringFirstInstall = @"Server=localhost;Database=master;Integrated Security=True; MultipleActiveResultSets=True;";
		private const string connString = @"Server=localhost;Database=IHS1;Integrated Security=True;MultipleActiveResultSets=True;";
		private const string currentVersion = "1.0.0";

        static void Main(string[] args)
        {
            ConsoleUtils.Print("Create new database? [Y]: ", ConsoleUtils.MessageOriginColor.Text, false);
            var answer = Console.ReadKey().Key;
            Console.WriteLine();
            var dbName = "ihs1";
            switch (answer)
            {
                case ConsoleKey.Y:
                    ConsoleUtils.Print("Starting installation...", ConsoleUtils.MessageOriginColor.System, true);
                    Install(dbName);
                    break;
                default:
                    CheckVersion(dbName);
                    break;
            }
        }

        private static void CheckVersion(string dbName)
        {
            var query = "GetCurrentVersion";
            var versionReader = DbHandler.DbHandler.ExecuteReader(connString, query, true);
            if (versionReader != null)
            {
                while (versionReader.Read())
                {
                    var versionUsedOnServer = (string)versionReader["VersionNumber"];
                    ConsoleUtils.Print($"The currently installed version is {currentVersion}", ConsoleUtils.MessageOriginColor.System, true);
                    var usingLatestVersion = int.Parse(versionUsedOnServer.Replace(".", "")) == int.Parse(currentVersion.Replace(".", ""));
                    if (!usingLatestVersion)
                    {
                        ConsoleUtils.Print($"WARNING: The currently installed version {versionUsedOnServer} is not the latest one available. Please upgrade to the latest version, {currentVersion}.", ConsoleUtils.MessageOriginColor.System, true);
                    }
                }
            }

            else
            {
                ConsoleUtils.Print("ERROR: The version number could not be found.", ConsoleUtils.MessageOriginColor.Error, true);
            }
        }

        private static bool SqlIsSafeString(string stringToCheck)
        {
            Regex r = new Regex(@"^[a-zA-Z0-9\_]*$");
            return (r.IsMatch(stringToCheck));
            //return Regex.Match(stringToCheck, //"/^[a-zA-Z][a-zA-Z0-9]*$/-gm").Success;
        }

        private static void Install(string dbName)
        {
            var result = DbHandler.DbHandler.ExecuteNonQuery(connString, "CREATE DATABASE {dbName}", false);
            if (result > -1)
            {
                ConsoleUtils.Print($"Successfully created database \"{dbName}\".", ConsoleUtils.MessageOriginColor.System, true);
            }
            else
            {
                ConsoleUtils.Print($"Could not create database \"{dbName}\"!", ConsoleUtils.MessageOriginColor.Error, true);
            }
        }
    }
}