using GTANetworkAPI;
using System;
using System.IO;

namespace NemesusWorld.Utils
{
    class Settings
    {
        public static Settings      _Settings;
        public string Servername    { get; set; }
        public string Servertag     { get; set; }
        public string Host          { get; set; }
        public string Username      { get; set; }
        public string Password      { get; set; }
        public string Database      { get; set; }

        public static bool LoadServerSettings()
        {
            string directory = "./serverdata/settings.json";
            if (File.Exists(directory))
            {
                string settings = File.ReadAllText(directory);
                _Settings = NAPI.Util.FromJson<Settings>(settings);
                Helper.ConsoleLog("success", "[SETTINGS]: Die Server Settings wurden erfolgreich geladen!");
                return true;
            }
            else
            {
                Helper.ConsoleLog("error", "[SETTINGS]: Die Server Settings konnten nicht geladen werden!");
                NAPI.Task.Run(() =>
                {
                    Environment.Exit(0);
                }, delayTime: 5000);
                return false;
            }
        }
    }
}
