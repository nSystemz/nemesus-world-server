using GTANetworkAPI;
using System;
using NemesusWorld.Utils;
using System.Data;
using MySqlConnector;

namespace NemesusWorld.Database
{
    class General
    {
        public static bool DatabaseConnectionCheck = false;
        public static MySqlConnection Connection = null;
        public String Host { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }
        public String Database { get; set; }

        public General()
        {
            this.Host = Settings._Settings.Host;
            this.Username = Settings._Settings.Username;
            this.Password = Settings._Settings.Password;
            this.Database = Settings._Settings.Database;
        }

        public static bool InitConnection()
        {
            if(Connection != null && Connection.State == ConnectionState.Open)
            {
                Helper.ConsoleLog("success", "[MYSQL]: Verbindung wurde beendet!");
                Connection.Close();
            }

            General sql = new General();
            String SQLConnection = $"SERVER={sql.Host}; DATABASE={sql.Database}; UID={sql.Username}; PASSWORD={sql.Password}";
            Connection = new MySqlConnection(SQLConnection);

            try
            {
                Connection.Open();
                DatabaseConnectionCheck = true;
                Helper.ConsoleLog("success", "[MYSQL]: Verbindung erfolgreich aufgebaut!");
                return true;
            }
            catch (Exception e)
            {
                DatabaseConnectionCheck = false;
                Helper.ConsoleLog("error", "[MYSQL]: Verbindung konnte nicht aufgebaut werden!");
                Helper.ConsoleLog("error", "[MYSQL]: " + e.ToString());
                NAPI.Task.Run(() =>
                {
                    System.Environment.Exit(1);
                }, delayTime: 5000);
                return false;
            }
        }
    }
}
