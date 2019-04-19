using DbHandler;
using System;
using UtilitiesMisc;

namespace InstallationUtils
{
    public class Installer
    {
        public static Tuple<bool, string> CheckVersion(string dbName, string connString, string currentVersion)
        {
            var query = "GetCurrentVersion";
            var versionReader = DbHandler.DbHandler.ExecuteReader(connString, query, true);
            if (versionReader != null)
            {
                while (versionReader.Read())
                {
                    var versionUsedOnServer = (string)versionReader["VersionNumber"];
                    var usingLatestVersion = int.Parse(versionUsedOnServer.Replace(".", "")) == int.Parse(currentVersion.Replace(".", ""));
                    return new Tuple<bool, string>(usingLatestVersion, currentVersion);     
                }
            }

            return null;
        }

        public static bool Install(string dbName, string connString)
        {
            var result = DbHandler.DbHandler.ExecuteNonQuery(connString, "CREATE DATABASE {dbName}", false);
            var installationSucceeded = result > -1;
            return installationSucceeded;
        }
    }
}