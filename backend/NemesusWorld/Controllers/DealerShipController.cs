using Google.Protobuf.WellKnownTypes;
using GTANetworkAPI;
using GTANetworkMethods;
using MySql.Data.MySqlClient;
using NemesusWorld.Database;
using NemesusWorld.Models;
using NemesusWorld.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Player = GTANetworkAPI.Player;
using Vehicle = GTANetworkAPI.Vehicle;

namespace NemesusWorld.Controllers
{
    class DealerShipController : Script
    {
        public static List<VehicleShop> dealerShipList = new List<VehicleShop>();
        public static String maxSpeeds = "";

        [RemoteEvent("Server:DealerShipSettings")]
        public static void DealerShopSettings(Player player, string settings, string data)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Account account = Helper.GetAccountData(player);
                TempData tempData = Helper.GetCharacterTempData(player);

                if (tempData == null || account == null) return;

                if (player.HasData("Player:LastBizz"))
                {
                    Business bizz = Business.GetBusinessById(player.GetData<int>("Player:LastBizz"));
                    switch (settings.ToLower())
                    {
                        case "abort":
                            {
                                Helper.SetPlayerPosition(player, tempData.furnitureOldPosition);
                                player.Dimension = 0;
                                player.SetData<int>("Player:LastBizz", 0);
                                player.TriggerEvent("Client:ShowDealerShip", "n/A", "n/A", null, null, -1, 1);
                                player.TriggerEvent("Client:PlayerFreeze", false);
                                break;
                            }
                        case "testdrive":
                            {
                                string[] dealerArray = new string[2];
                                dealerArray = data.Split(",");
                                if (account.play_time < 1)
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Du benötigst mind. 1 Spielstunde um eine Probefahrt machen zu können!", "error", "center");
                                    return;
                                }
                                if (account.level < 3 && bizz.id == 32)
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Du benötigst mind. Level 3 um hier eine Probefahrt machen zu können!", "error", "center");
                                    return;
                                }
                                if (tempData.dealerShip != null)
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Du bist schon in einer Probefahrt!", "error", "center");
                                    return;
                                }
                                if (player.HasData("Player:TestDriveCD") && Helper.UnixTimestamp() < player.GetData<int>("Player:TestDriveCD"))
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Du kannst nur alle fünf Minuten eine Probefahrt machen!", "error", "center");
                                    return;
                                }
                                Helper.SetPlayerPosition(player, tempData.furnitureOldPosition);
                                player.Dimension = (uint)(player.Id + 5);
                                if (bizz.id == 22)
                                {
                                    tempData.dealerShip = Cars.createNewCar(dealerArray[0].ToLower(), new Vector3(145.23679, -140.08275, 54.24724 + 0.25), -20.416449f, Convert.ToInt32(dealerArray[1]), Convert.ToInt32(dealerArray[1]), "LS-S-100" + player.Id, "Autohaus", false, true, false, player.Dimension);
                                }
                                else if (bizz.id == 23)
                                {
                                    tempData.dealerShip = Cars.createNewCar(dealerArray[0].ToLower(), new Vector3(274.08023, -1159.84, 28.617239 + 0.25), 87.37559f, Convert.ToInt32(dealerArray[1]), Convert.ToInt32(dealerArray[1]), "LS-S-100" + player.Id, "Autohaus", false, true, false, player.Dimension);
                                }
                                else if (bizz.id == 24)
                                {
                                    tempData.dealerShip = Cars.createNewCar(dealerArray[0].ToLower(), new Vector3(-31.80824, -1091.3527, 25.65422 + 0.25), -31.949799f, Convert.ToInt32(dealerArray[1]), Convert.ToInt32(dealerArray[1]), "LS-S-100" + player.Id, "Autohaus", false, true, false, player.Dimension);
                                }
                                else if (bizz.id == 25)
                                {
                                    tempData.dealerShip = Cars.createNewCar(dealerArray[0].ToLower(), new Vector3(-68.83505, 82.71136, 71.28684 + 0.25), 63.46722f, Convert.ToInt32(dealerArray[1]), Convert.ToInt32(dealerArray[1]), "LS-S-100" + player.Id, "Autohaus", false, true, false, player.Dimension);
                                }
                                else if (bizz.id == 26)
                                {
                                    tempData.dealerShip = Cars.createNewCar(dealerArray[0].ToLower(), new Vector3(-23.408432, -1678.2253, 29.160381 + 0.25), 110.107635f, Convert.ToInt32(dealerArray[1]), Convert.ToInt32(dealerArray[1]), "LS-S-100" + player.Id, "Autohaus", false, true, false, player.Dimension);
                                }
                                else if (bizz.id == 27)
                                {
                                    tempData.dealerShip = Cars.createNewCar(dealerArray[0].ToLower(), new Vector3(1214.5273, 2708.1516, 37.477882 + 0.25), 156.33295f, Convert.ToInt32(dealerArray[1]), Convert.ToInt32(dealerArray[1]), "LS-S-100" + player.Id, "Autohaus", false, true, false, player.Dimension);
                                }
                                else if (bizz.id == 28)
                                {
                                    tempData.dealerShip = Cars.createNewCar(dealerArray[0].ToLower(), new Vector3(-201.7464, 6204.731, 31.017431 + 0.25), 46.196392f, Convert.ToInt32(dealerArray[1]), Convert.ToInt32(dealerArray[1]), "LS-S-100" + player.Id, "Autohaus", false, true, false, player.Dimension);
                                }
                                else if (bizz.id == 29)
                                {
                                    tempData.dealerShip = Cars.createNewCar(dealerArray[0].ToLower(), new Vector3(663.9333, -2687.8196, 6.147993 + 0.25), 90.25697f, Convert.ToInt32(dealerArray[1]), Convert.ToInt32(dealerArray[1]), "LS-S-100" + player.Id, "Autohaus", false, true, false, player.Dimension);
                                }
                                else if (bizz.id == 30)
                                {
                                    tempData.dealerShip = Cars.createNewCar(dealerArray[0].ToLower(), new Vector3(-1139.5806, -211.71169, 37.537098 + 0.25), 74.32476f, Convert.ToInt32(dealerArray[1]), Convert.ToInt32(dealerArray[1]), "LS-S-100" + player.Id, "Autohaus", false, true, false, player.Dimension);
                                }
                                if (player.HasData("Player:TestDriveCD"))
                                {
                                    player.ResetData("Player:TestDriveCD");
                                }
                                player.TriggerEvent("Client:PlayerFreeze", false);
                                player.TriggerEvent("Client:ShowDealerShip", "n/A", "n/A", null, null, -1, 1);
                                Helper.SendNotificationWithoutButton2(player, "Viel Spass bei der Probefahrt, das Fahrzeug steht direkt hier vorne, du hast 5 Minuten Zeit (oder /abort zum beenden benutzen)!", "success", "center", 3750);
                                player.SetData<int>("Player:TestDrive", Helper.UnixTimestamp() + (60 * 5));
                                player.TriggerEvent("Client:CreateWaypoint", tempData.dealerShip.Position.X, tempData.dealerShip.Position.Y);
                                player.SetData<int>("Player:LastBizz", 0);
                                break;
                            }
                        case "buyvehicle":
                            {
                                if (tempData.dealerShip != null)
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Du bist noch in einer Probefahrt!", "error", "center");
                                    return;
                                }
                                player.SetData<string>("Player:VehicleBuyData", data);
                                player.TriggerEvent("Client:PlayerFreeze", true);
                                player.TriggerEvent("Client:CallInput2", "Fahrzeug kaufen", "Für welchen Besitz möchtest du das Fahrzeug kaufen?", "BuyVehicle", "Privat", "Gruppierung");
                                break;
                            }
                    }

                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[ShowDealerShip]: " + e.ToString());
            }
        }

        public static int CountVehicles(string identifier)
        {
            int count = 0;
            try
            {
                foreach (Cars car in Cars.carList)
                {
                    if (car.id > -1)
                    {
                        if (car.vehicleData != null && car.vehicleData.owner.ToLower() == identifier)
                        {
                            count++;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CountVehicles]: " + e.ToString());
            }
            return count;
        }

        public static int MaxVehicles(Player player, int mod)
        {
            int count = 0;
            try
            {
                Character character = Helper.GetCharacterData(player);
                Account account = Helper.GetAccountData(player);
                if (character == null) return count;
                if (mod == 1)
                {
                    count = 3;
                    if (account.premium == 1)
                    {
                        count++;
                    }
                    else if (account.premium == 2)
                    {
                        count += 2;
                    }
                    else if (account.premium == 3)
                    {
                        count += 3;
                    }
                    count += account.vehicleslots;
                }
                else
                {
                    Groups group = GroupsController.GetGroupById(character.mygroup);
                    if (group != null)
                    {
                        count = 15;
                        if (group.maxplusvehicles > 0)
                        {
                            count += group.maxplusvehicles;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[MaxVehicles]: " + e.ToString());
            }
            return count;
        }

        public static void ShowDealerShip(Player player)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                TempData tempData = Helper.GetCharacterTempData(player);

                if (tempData == null) return;

                Business bizz = Business.GetClosestBusiness(player, 40.5f);
                if (bizz != null)
                {
                    List<VehicleShop> vehicleShopList = GetVehicleShopByBizzId(bizz.id);

                    if (vehicleShopList.Count <= 0)
                    {
                        Helper.SendNotificationWithoutButton2(player, "Wir verkaufen aktuell keine Fahrzeuge!", "error", "center");
                        return;
                    }

                    vehicleShopList.Sort((x, y) => string.Compare(x.vehiclename, y.vehiclename));

                    VehicleShop vehicleShop = vehicleShopList[0];

                    NAPI.Player.SetPlayerCurrentWeapon(player, WeaponHash.Unarmed);
                    tempData.furnitureOldPosition = player.Position;
                    player.SetData<int>("Player:LastBizz", bizz.id);
                    player.Dimension = (uint)(1000 + player.Id);
                    if (bizz.id != 29 && bizz.id != 31 && bizz.id != 32)
                    {
                        Helper.SetPlayerPosition(player, new Vector3(221.35622, -994.12335, -98.99989));
                    }
                    else
                    {
                        if (bizz.id == 29)
                        {
                            Helper.SetPlayerPosition(player, new Vector3(609.8058, -2674.2212, 6.081178));
                        }
                        else if (bizz.id == 31)
                        {
                            Helper.SetPlayerPosition(player, new Vector3(-724.4176, -1334.2098, 1.5962913));
                        }
                        else if (bizz.id == 32)
                        {
                            Helper.SetPlayerPosition(player, new Vector3(-989.6739, -2947.5237, 13.945066));
                        }
                    }

                    NAPI.Task.Run(() =>
                    {
                        player.TriggerEvent("Client:ShowDealerShip", bizz.name, vehicleShop.vehiclename, NAPI.Util.ToJson(vehicleShopList), DealerShipController.maxSpeeds, bizz.id, bizz.multiplier);
                    }, delayTime: 500);                  
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[ShowDealerShip]: " + e.ToString());
            }
        }

        public static List<VehicleShop> GetVehicleShopByBizzId(int bizzId)
        {
            List<VehicleShop> vehicleShopList = new List<VehicleShop>();
            try
            {
                foreach (VehicleShop vehicleShop in DealerShipController.dealerShipList)
                {
                    if (vehicleShop != null && vehicleShop.bizzid == bizzId)
                    {
                        vehicleShopList.Add(vehicleShop);
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetVehicleShopByBizzId]: " + e.ToString());
            }
            return vehicleShopList;
        }

        public static VehicleShop GetVehicleShopByVehicleName(string vehicleName)
        {
            VehicleShop retVehicleShop = null;
            try
            {
                foreach (VehicleShop vehicleShop in DealerShipController.dealerShipList)
                {
                    if (vehicleShop != null && vehicleShop.vehiclename.ToLower() == vehicleName.ToLower())
                    {
                        retVehicleShop = vehicleShop;
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetVehicleShopByVehicleName]: " + e.ToString());
            }
            return retVehicleShop;
        }

        public static void GetAllVehicles()
        {
            try
            {
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                foreach (VehicleData vehicleData in db.Fetch<VehicleData>("SELECT * FROM vehicles"))
                {
                    if (vehicleData != null)
                    {
                        if(vehicleData.towed > 0)
                        {
                            vehicleData.towed += Helper.adminSettings.towedcash;
                            if (vehicleData.towed > 35000)
                            {
                                vehicleData.towed = 35000;
                            }
                        }
                        Cars.createNewCar(vehicleData.vehiclename, new Vector3(), 0f, 0, 0, "n/A", "n/A", false, false, true, 0, vehicleData, false);
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetAllVehicles]: " + e.ToString());
            }
        }

        public static VehicleData GetVehicleDataById(int id)
        {
            try
            {
                foreach (Cars car in Cars.carList)
                {
                    if (car.vehicleData != null && car.vehicleData.id == id)
                    {
                        return car.vehicleData;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetVehicleDataById]: " + e.ToString());
            }
            return null;
        }

        public static VehicleData GetVehicleDataByVehicle(Vehicle vehicle)
        {
            try
            {
                if (vehicle == null) return null;
                foreach (Cars car in Cars.carList)
                {
                    if (car.vehicleHandle != null && car.vehicleHandle == vehicle)
                    {
                        return car.vehicleData;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetVehicleDataByVehicle]: " + e.ToString());
            }
            return null;
        }

        [RemoteEvent("Server:VehicleControl")]
        public static VehicleData OnVehicleControl(Player player, Vehicle vehicle, string vehicleData, int doors = 1)
        {
            try
            {
                if (vehicle != null)
                {
                    if(doors == 1)
                    {
                        vehicle.SetSharedData("Vehicle:Doors", vehicleData);
                    }
                    else
                    {
                        vehicle.SetSharedData("Vehicle:Windows", vehicleData);
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnVehicleControl]: " + e.ToString());
            }
            return null;
        }

        public static void SaveVehicleData()
        {
            try
            {
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                foreach (Cars car in Cars.carList)
                {
                    if (car.vehicleData != null && car.vehicleData != null)
                    {
                        SaveOneVehicleData(car);
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[SaveVehicleData]: " + e.ToString());
            }
        }

        public static void SaveOneVehicleData(Cars car)
        {
            try
            {
                if (car != null)
                {
                    PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                    if (car.vehicleData != null && car.vehicleHandle != null)
                    {
                        if (car.vehicleHandle.Dimension == 0 && !car.vehicleHandle.HasData("Vehicle:Jacked"))
                        {
                            car.vehicleData.position = $"{car.vehicleHandle.Position.X.ToString(new CultureInfo("en-US"))}|{car.vehicleHandle.Position.Y.ToString(new CultureInfo("en-US"))}|{car.vehicleHandle.Position.Z.ToString(new CultureInfo("en-US"))}|{car.vehicleHandle.Rotation.Z.ToString(new CultureInfo("en-US"))}|{car.vehicleHandle.Dimension}";
                            car.vehicleData.position = car.vehicleData.position.Replace(",", ".");
                        }
                        car.vehicleData.sync = car.vehicleHandle.GetSharedData<string>("Vehicle:Sync");
                        car.vehicleData.doors = car.vehicleHandle.GetSharedData<string>("Vehicle:Doors");
                        car.vehicleData.windows = car.vehicleHandle.GetSharedData<string>("Vehicle:Windows");
                        car.vehicleData.health = $"{NAPI.Vehicle.GetVehicleBodyHealth(car.vehicleHandle)}|{NAPI.Vehicle.GetVehicleEngineHealth(car.vehicleHandle)}|{NAPI.Vehicle.GetVehicleHealth(car.vehicleHandle)}";
                        car.vehicleData.fuel = car.vehicleHandle.GetSharedData<float>("Vehicle:Fuel") > 0 ? (float)Math.Round(car.vehicleHandle.GetSharedData<float>("Vehicle:Fuel"), 2) : 0;
                        car.vehicleData.oel = car.vehicleHandle.GetSharedData<int>("Vehicle:Oel");
                        car.vehicleData.battery = car.vehicleHandle.GetSharedData<int>("Vehicle:Battery");
                        car.vehicleData.tuning = car.vehicleHandle.GetSharedData<string>("Vehicle:Tuning");
                        car.vehicleData.kilometre = car.vehicleHandle.GetSharedData<float>("Vehicle:Kilometre");
                        car.vehicleData.color = car.vehicleHandle.GetSharedData<string>("Vehicle:Color");
                        car.vehicleData.status = car.vehicleHandle.Locked ? 1 : 0;
                        car.vehicleData.engine = car.vehicleHandle.EngineStatus ? 1 : 0;
                        car.vehicleData.products = car.vehicleHandle.GetData<int>("Vehicle:Products");
                        car.vehicleData.vlock = car.vehicleHandle.GetData<int>("Vehicle:VLock");
                        if (car.vehicleHandle.HasSharedData("Vehicle:Tuev"))
                        {
                            car.vehicleData.tuev = car.vehicleHandle.GetSharedData<int>("Vehicle:Tuev");
                        }
                        db.Save(car.vehicleData);
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[SaveOneVehicleData]: " + e.ToString());
            }
        }

        public static void GetAllVehicleShop(bool generate = false)
        {
            try
            {
                bool change = false;
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                if (generate == false)
                {
                    foreach (VehicleShop vehicleShop in db.Fetch<VehicleShop>("SELECT * FROM vehicleshop"))
                    {
                        dealerShipList.Add(vehicleShop);
                    }
                }
                else
                {
                    foreach (VehicleShop vehicleShop in db.Fetch<VehicleShop>("SELECT * FROM vehicleshop"))
                    {
                        dealerShipList.Add(vehicleShop);
                    }

                    string file = "./serverdata/vehicles.json";
                    if (File.Exists(file))
                    {
                        string settings = File.ReadAllText(file);
                        var vehicleList = (JArray)JsonConvert.DeserializeObject(settings);
                        int i = 0;
                        change = true;
                        while (i < vehicleList.Count)
                        {
                            foreach (VehicleShop vehicleShop in DealerShipController.dealerShipList)
                            {
                                if (!vehicleShop.vehiclename.ToLower().Contains("trailer") && vehicleShop.vehiclename.ToLower() != "tanker")
                                {
                                    var element = (JObject)vehicleList[i];
                                    if (vehicleShop.vehiclename.ToLower() == Convert.ToString(element["Name"]).ToLower())
                                    {
                                        vehicleShop.maxspeed = (int)(Convert.ToInt32(element["MaxSpeed"]) * 3.6);
                                        vehicleShop.maxacceleration = float.Parse((string)element["Acceleration"], new CultureInfo("en-US"));
                                        vehicleShop.maxbraking = float.Parse((string)element["MaxBraking"], new CultureInfo("en-US"));
                                        vehicleShop.maxhandling = float.Parse((string)element["MaxTraction"], new CultureInfo("en-US"));
                                    }
                                }
                                else
                                {
                                    vehicleShop.maxspeed = 1;
                                    vehicleShop.maxacceleration = 0.001f;
                                    vehicleShop.maxbraking = 0.01f;
                                    vehicleShop.maxhandling = 0.01f;
                                }
                                if (vehicleShop.maxspeed <= 1)
                                {
                                    vehicleShop.maxspeed = 50;
                                }
                                if (vehicleShop.price == 1 || vehicleShop.price == 0)
                                {
                                    vehicleShop.price = Helper.Round(vehicleShop.maxspeed * 200);
                                    if (vehicleShop.bizzid == 22)
                                    {
                                        vehicleShop.price = vehicleShop.price * 10;
                                    }
                                    else if (vehicleShop.bizzid == 23)
                                    {
                                        vehicleShop.price = vehicleShop.price;
                                    }
                                    else if (vehicleShop.bizzid == 24)
                                    {
                                        vehicleShop.price = vehicleShop.price * 7;
                                    }
                                    else if (vehicleShop.bizzid == 25)
                                    {
                                        vehicleShop.price = vehicleShop.price * 4;
                                    }
                                    else if (vehicleShop.bizzid == 26)
                                    {
                                        vehicleShop.price = vehicleShop.price * 5;
                                    }
                                    else if (vehicleShop.bizzid == 27)
                                    {
                                        vehicleShop.price = vehicleShop.price * 5;
                                    }
                                    else if (vehicleShop.bizzid == 28)
                                    {
                                        vehicleShop.price = vehicleShop.price * 6;
                                    }
                                    else if (vehicleShop.bizzid == 29)
                                    {
                                        vehicleShop.price = vehicleShop.price * 6;
                                    }
                                    else if (vehicleShop.bizzid == 30)
                                    {
                                        vehicleShop.price = vehicleShop.price / 4;
                                    }
                                    else if (vehicleShop.bizzid == 31)
                                    {
                                        vehicleShop.price = vehicleShop.price * 8;
                                    }
                                    else if (vehicleShop.bizzid == 32)
                                    {
                                        vehicleShop.price = vehicleShop.price * 17;
                                    }
                                }
                                if (vehicleShop.maxspeed == 50)
                                {
                                    vehicleShop.maxspeed = 1;
                                }
                            }
                            i++;
                        }
                    }
                }

                if (change == true)
                {
                    foreach (VehicleShop vehicleShop in DealerShipController.dealerShipList)
                    {
                        if (vehicleShop != null)
                        {
                            db.Save(vehicleShop);
                        }
                    }
                }

                MySqlCommand command = General.Connection.CreateCommand();
                command.CommandText = "SELECT max(maxspeed) as maxspeed, max(maxacceleration) as maxacceleration, max(maxbraking) as maxbraking, max(maxhandling) as maxhandling from vehicleshop WHERE bizzid < 31 LIMIT 1";

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        LoadCharactersModel loadCharacter = new LoadCharactersModel();
                        maxSpeeds = $"{reader.GetInt32("maxspeed")},{reader.GetFloat("maxacceleration").ToString(new CultureInfo("en-US")):N3},{reader.GetFloat("maxbraking").ToString(new CultureInfo("en-US")):N3},{reader.GetFloat("maxhandling").ToString(new CultureInfo("en-US")):N3}";
                    }
                    reader.Close();
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetAllVehicleShop]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:ShowMechaMenu")]
        public static void ShowMechaMenu(Player player)
        {
            try
            {
                bool found = false;
                Character character = Helper.GetCharacterData(player);
                Account account = Helper.GetAccountData(player);
                if (character == null || account == null || player.IsInVehicle) return;

                Groups group = GroupsController.GetGroupById(character.mygroup);
                House house = null;
                float dist = 45.5f;

                if (group == null && character.faction != 3) return;

                if (character.faction != 3 || character.factionduty == false)
                {
                    if (group == null) return;

                    string[] licArray = new string[9];
                    licArray = group.licenses.Split("|");
                    if (Convert.ToInt32(licArray[2]) == 0) { return; }

                    house = character.inhouse != -1 ? House.GetHouseById(character.inhouse) : House.GetClosestHouse(player, 25.5f);
                }
                else
                {
                    house = House.GetHouseById(2);
                    dist = 105.5f;
                }

                Vehicle vehicle2 = Helper.GetClosestVehicleByName(player, 15.55f, "utillitruck");

                if (house == null && vehicle2 == null) return;

                Vehicle vehicle = Helper.GetClosestVehicle(player, 3.15f);
                if (vehicle == null)
                {
                    Helper.SendNotificationWithoutButton(player, "Du bist nicht in der Nähe von einem Fahrzeug!", "error");
                    return;
                }
                VehicleData vehicleData = null;
                if (vehicle2 != null)
                {
                    vehicleData = DealerShipController.GetVehicleDataByVehicle(vehicle2);
                }
                if (house != null)
                {
                    if ((house.position.DistanceTo(vehicle.Position) <= dist || character.inhouse == house.id))
                    {
                        found = true;
                        if (house.id != 2 && group != null && house.housegroup != character.mygroup)
                        {
                            found = false;
                        }
                    }
                }
                else
                {
                    if (vehicle2 != null && vehicleData != null && (vehicleData.owner == "group-" + character.mygroup || vehicleData.owner == "faction-" + character.faction))
                    {
                        found = true;
                    }
                }
                if (found == true)
                {
                    if (vehicle.EngineStatus == true)
                    {
                        Helper.SendNotificationWithoutButton(player, "Du musst zuerst den Motor vom Fahrzeug ausschalten!", "error");
                        return;
                    }
                    Helper.ShowPreShop(player, "Mechatronikermenü", 0, 1, 1);
                    return;
                }
                Helper.SendNotificationWithoutButton(player, "Du bist nicht in der Nähe/in deinem Firmensitz/einem Utilitytruck!", "error", "top-left", 3500);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[ShowMechaMenu]: " + e.ToString());
            }
        }
    }
}
