using GTANetworkAPI;
using System;
using System.IO;

namespace NemesusWorld.Utils
{
    class Settings
    {
        public static Settings _Settings;
        public string Servername { get; set; }
        public string Servertag { get; set; }
        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }
        public string AdminNotificationWebHook { get; set; }
        public string ErrorWebhook { get; set; }
        public string ScreenshotWebhook { get; set; }

        //ToDo: settings.json anpassen im Ordner serverdata
        public static bool LoadServerSettings()
        {
            try
            {
                string directory = "./serverdata/settings.json";
                if (File.Exists(directory))
                {
                    string settings = File.ReadAllText(directory);
                    if(!settings.ToLower().Contains("webhook"))
                    {
                        Helper.ConsoleLog("error", "[SETTINGS]: Fehler - Bitte lade die neuste settings.json aus dem Github Repo runter!");
                        return false;
                    }
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
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[LoadServerSettings]: " + e.ToString(), false);
            }
            return false;
        }
    }
}
