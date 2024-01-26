using System;
using System.Collections.Generic;
using GTANetworkAPI;
using MySqlConnector;
using NemesusWorld.Database;
using NemesusWorld.Models;
using NemesusWorld.Utils;

namespace NemesusWorld.Controllers
{
    class FireController : Script
    {
        public static bool startFire = false;
        public static Vector3 firePosition = null;
        public static ColShape fireColshape = null;
        public static Fire publicFire = null;
        public static Vector3[] fireObjectPositions;
        public static Vehicle fireVehicle = null;
        public static GTANetworkAPI.Object fireObject = null;
        public static GTANetworkAPI.TextLabel fireLabel = null;
        public static int objectHealth = 100;

        [RemoteEvent("Server:FireComplete")]
        public static void FiresCompleteEvent(Player ownPlayer)
        {
            try
            {
                if (startFire == false) return;
                if (fireObject != null)
                {
                    Random random = new Random();
                    objectHealth -= random.Next(1, 5);
                    if(objectHealth > 0)
                    {
                        fireLabel.Text = $"~b~Umgefallener Baum\n~w~Zustand: {objectHealth}%";
                        return;
                    }
                    else
                    {
                        objectHealth = 0;
                        fireLabel.Text = $"~b~Umgefallener Baum\n~w~Zustand: 0%";
                    }
                }
                startFire = false;
                firePosition = null;
                publicFire = null;
                if (fireColshape != null)
                {
                    fireColshape.Delete();
                    fireColshape = null;
                }
                if (fireVehicle != null || fireObject != null)
                {
                    MDCController.SendMedicMDCMessage(ownPlayer, "Die Unfallstelle wurde gesäubert!");
                }
                else
                {
                    MDCController.SendMedicMDCMessage(ownPlayer, "Das Feuer wurde gelöscht!");
                }
                if (fireObject != null)
                {
                    fireObject.Delete();
                    fireObject = null;
                }
                if (fireLabel != null)
                {
                    fireLabel.Delete();
                    fireLabel = null;
                }
                if (fireVehicle != null)
                {
                    fireVehicle.Delete();
                    fireVehicle = null;
                }
                foreach (Player player in NAPI.Pools.GetAllPlayers())
                {
                    if (player != null && player.Exists && Account.IsPlayerLoggedIn(player) && player.GetOwnSharedData<bool>("Player:Spawned") == true)
                    {
                        if (player.GetData<bool>("Player:Fire") == true && player.Dimension == 0)
                        {
                            NAPI.ClientEvent.TriggerClientEvent(player, "Client:StopFireById");
                            player.SetData<bool>("Player:Fire", false);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[FiresCompleteEvent]: " + e.ToString());
            }
        }

        public static string BuildFire(Player player, string name = "n/A")
        {
            try
            {
                MySqlCommand command = null;
                int count = 0;
                float angle = 0.0f;

                Random amount = new Random();

                if (fireColshape != null)
                {
                    fireColshape.Delete();
                    fireColshape = null;
                }

                List<string> randomFirePosition = new List<String>();

                command = General.Connection.CreateCommand();
                command.CommandText = "SELECT DISTINCT(name) FROM fire";

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        randomFirePosition.Add(reader.GetString("name"));
                    }
                    reader.Close();
                }

                string[] randomVehicles = { "sultan", "club", "zion2", "chino2", "ruiner", "fq2", "gresley", "glendale2", "buffalo", "deveste" };
                int randomFirePositionIndex = amount.Next(randomFirePosition.Count);
                int fireCount = 0;

                string ort = randomFirePosition[randomFirePositionIndex];
                if (name != "n/A")
                {
                    ort = name;
                }

                command = General.Connection.CreateCommand();
                command.CommandText = "SELECT * FROM fire WHERE name = @name LIMIT 55";
                command.Parameters.AddWithValue("@name", ort);

                fireObjectPositions = new Vector3[55];
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        fireObjectPositions[fireCount] = new Vector3(reader.GetFloat("posx"), reader.GetFloat("posy"), reader.GetFloat("posZ"));
                        angle = reader.GetFloat("posa");
                        fireCount++;
                    }
                    reader.Close();
                }

                if (fireCount == 0)
                {
                    ort = randomFirePosition[randomFirePositionIndex];
                }

                firePosition = fireObjectPositions[0];

                if (ort.Contains("Fahrzeugunfall"))
                {
                    SmartphoneController.OnSendDispatch(player, $"Unfallmeldung - {ort}", "Anonym", 2, "Anonym", false);
                    string vehicleName = randomVehicles[amount.Next(randomVehicles.Length)];
                    fireVehicle = Cars.createNewCar(vehicleName, new Vector3(firePosition.X, firePosition.Y, firePosition.Z), amount.Next(0, 180), amount.Next(0, 159), amount.Next(0, 159), "LS-S-300", "Anonym", true, false, false);
                    NAPI.Vehicle.SetVehicleBodyHealth(fireVehicle, 415);
                    NAPI.Vehicle.SetVehicleEngineHealth(fireVehicle, 415);
                    NAPI.Vehicle.SetVehicleHealth(fireVehicle, 415);
                }
                else if (ort.Contains("Baum"))
                {
                    objectHealth = 100;
                    SmartphoneController.OnSendDispatch(player, $"{ort}", "Anonym", 2, "Anonym", false);
                    fireObject = NAPI.Object.CreateObject(1993764676, new Vector3(firePosition.X, firePosition.Y, firePosition.Z), new Vector3(0, 0, angle+90), 255, 0);
                    Vector3 frontPosition = Helper.GetPositionInFrontOfPosition(new Vector3(firePosition.X, firePosition.Y, firePosition.Z), angle, 4.75f);
                    fireLabel = NAPI.TextLabel.CreateTextLabel($"~b~Umgefallener Baum\n~w~Zustand: 100%", new Vector3(frontPosition.X, frontPosition.Y, frontPosition.Z + 1.5), 12.5f, 0.5f, 4, new Color(255, 255, 255), true, 0);
                }
                else
                {
                    SmartphoneController.OnSendDispatch(player, $"Brandmeldung bei/m {ort}", "Anonym", 2, "Anonym", false);
                }

                fireColshape = NAPI.ColShape.CreatCircleColShape(firePosition.X, firePosition.Y, 55.5f, 0);

                startFire = true;

                foreach (Player p in NAPI.Pools.GetAllPlayers())
                {
                    if (p != null && p.GetOwnSharedData<bool>("Player:Spawned") == true)
                    {
                        Character character = Helper.GetCharacterData(p);
                        if (character != null && (Helper.IsInRangeOfPoint(p.Position, firePosition, 55) || character.faction == 2 || p == player))
                        {
                            count = 0;
                            p.SetData<bool>("Player:Fire", true);
                            NAPI.ClientEvent.TriggerClientEvent(p, "Client:BlipFireLocation", firePosition.X, firePosition.Y, firePosition.Z);
                            if (fireObject == null)
                            {
                                while (count < fireObjectPositions.Length && fireObjectPositions[count] != null)
                                {
                                    NAPI.ClientEvent.TriggerClientEvent(p, "Client:CheckIfReachable", fireObjectPositions[count].X, fireObjectPositions[count].Y, fireObjectPositions[count].Z, 15, true);
                                    count++;
                                }
                            }
                            NAPI.Task.Run(() =>
                            {
                                if (fireObject == null)
                                {
                                    NAPI.ClientEvent.TriggerClientEvent(player, "Client:FiresAliveTimer", FireController.firePosition.X, FireController.firePosition.Y, FireController.firePosition.Z, 1);
                                }
                                else
                                {
                                    NAPI.ClientEvent.TriggerClientEvent(player, "Client:FiresAliveTimer", FireController.firePosition.X, FireController.firePosition.Y, FireController.firePosition.Z, 2);
                                }
                            }, delayTime: 1050);
                        }
                    }
                }

                return ort;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[BuildFire]: " + e.ToString());
            }
            return "n/A";
        }
    }
}
