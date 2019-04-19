using System;
using System.Text.RegularExpressions;
using UtilitiesMisc;
using InstallationUtils;

namespace Installer
{
    class Program
    {
		private const string connStringFirstInstall = @"Server=localhost;Database=master;Integrated Security=True; MultipleActiveResultSets=True;";
		private const string connString = @"Server=localhost;Database=IHS1;Integrated Security=True;MultipleActiveResultSets=True;";
		private const string currentVersion = "1.0.0";

        static void Main(string[] args)
        {
            ConsoleUtils.Print("Create new database? [Y]: ", MessageOriginColor.Text, false);
            var answer = Console.ReadKey().Key;
            Console.WriteLine();
            var dbName = "ihs1";
            switch (answer)
            {
                case ConsoleKey.Y:
                    ConsoleUtils.Print("Starting installation...", MessageOriginColor.System, true);
                    Install(dbName);
                    break;
                default:
                    CheckVersion(dbName);
                    break;
            }
        }

        private static void CheckVersion(string dbName)
        {
            ConsoleUtils.Print($"The currently installed version is {currentVersion}", MessageOriginColor.System, true);
            var versionInfo = InstallationUtils.Installer.CheckVersion(dbName, connString, currentVersion);
            if (versionInfo != null)
            {
                var usingLatestVersion = versionInfo.Item1;
                var versionUsedOnServer = versionInfo.Item2;
                if (!usingLatestVersion)
                {
                    ConsoleUtils.Print($"WARNING: The currently installed version {versionUsedOnServer} is not the latest one available. Please upgrade to the latest version, {currentVersion}.", MessageOriginColor.System, true);
                }
            }

            else
            {
                ConsoleUtils.Print("ERROR: The version number could not be found.", MessageOriginColor.Error, true);
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
            var installationSucceeded = InstallationUtils.Installer.Install(dbName, connString);
            if (installationSucceeded)
            {
                ConsoleUtils.Print($"Successfully created database \"{dbName}\".", MessageOriginColor.System, true);
            }
            else
            {
                ConsoleUtils.Print($"Could not create database \"{dbName}\"!", MessageOriginColor.Error, true);
            }
        }
    }
}