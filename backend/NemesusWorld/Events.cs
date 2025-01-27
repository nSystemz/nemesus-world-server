/*
	#     #
	##    #  ######  #    #  ######   ####   #    #   ####
	# #   #  #       ##  ##  #       #       #    #  #
	#  #  #  #####   # ## #  #####    ####   #    #   ####
	#   # #  #       #    #  #            #  #    #       #
	#    ##  #       #    #  #       #    #  #    #  #    #
	#     #  ######  #    #  ######   ####    ####    ####

 	______________________________________________________________________________________________

	Nemesus World Gamemode

	Nemesus-World.de

	Selfmade Gamemode by Nemesus - Nemesus.de

	Weitere Helfer/Credits: /credits

	(C) by Nemesus World.de Credits 2021-2024

	______________________________________________________________________________________________
	
	Stand: 02.2024

*/

using GTANetworkAPI;
using NemesusWorld.Utils;
using NemesusWorld.Database;
using NemesusWorld.Controllers;
using System.Threading;
using System;
using NemesusWorld.Models;
using MySqlConnector;
using System.Linq;
using System.Data;
using System.IO;
using Newtonsoft.Json.Linq;
using Player = GTANetworkAPI.Player;
using Vehicle = GTANetworkAPI.Vehicle;
using Vector3 = GTANetworkAPI.Vector3;
using Ped = GTANetworkAPI.Ped;
using Blip = GTANetworkAPI.Blip;
using System.Threading.Tasks;

namespace NemesusWorld
{
    public class Events : Script
    {
        public static int tenMinutesTimerCheck = 0;
        public static int vehicleTimerCheck = 0;
        public static int halfMinuteCounter = 0;
        public static int halfHourCounter = 0;
        public static int fullHourTimer = 0;
        public static Timer halfOneMinuteTimer = null;
        public static Timer secondTimer = null;
        public static Timer saveTimer = null;
        //ToDo: Speicherzeit einstellen (Es wird alles gespeichert)
        public static int saveMinutes = 30; //Speicherzeit in Minuten
        //ToDo: Restartzeit einstellen
        public static int RestartHour = -1; //Um wieviel Uhr soll der Server neustarten (-1 = garnicht), bitte Service einrichten welcher den Server automatisch wieder startet
        public static bool InitRestart = false;
        public static GTANetworkAPI.ColShape ammuCol = null;
        public static GTANetworkAPI.TextLabel busLabel = null;
        public static int busCount = 0;
        public static GTANetworkAPI.Blip closesZone = null;
        public static bool lottoStart = false;
        public static GTANetworkAPI.TextLabel labelCheck = null;

        [ServerEvent(Event.ResourceStart)]
        public async Task OnResourceStartAsync()
        {
            try
            {
                //Load server settings
                if (Settings.LoadServerSettings())
                {
                    if (General.DatabaseConnectionCheck == false)
                    {
                        //Start database connection
                        if (General.InitConnection() == true)
                        {
                            //Delete old logs and other stuff
                            Helper.DeleteOldLogs();
                            //Load adminsettings
                            Helper.GetAdminSettings();
                            //Get all bus routes
                            Helper.GetAllBusRoutes();
                            //Get all garbage routes
                            Helper.GetAllGarbageRoutes();
                            //Get all taxi routes
                            Helper.GetAllTaxiRoutes();
                            //Rathaus Position
                            if (Helper.adminSettings.mlosloaded == true)
                            {
                                Helper.RathausPosition = new Vector3(-555.7711, -185.85564, 38.22111);
                                NAPI.Ped.CreatePed(NAPI.Util.GetHashKey("ig_jewelass"), new Vector3(-555.7711, -185.85564, 38.22111), -151.82767f, true, true, true, false, 4294967295);
                            }
                            else
                            {
                                Helper.RathausPosition = new Vector3(-546.38464, -204.60997, 38.214985);
                                NAPI.TextLabel.CreateTextLabel("~y~Hinweis: Bitte füge noch die restlichen MLOs ein (s. Github Readme.md)\n~w~Setze anschließend den 'mlosloaded' Wert in der Adminsettings.cs auf 'true'!", new Vector3(-541.3104, -204.3746, 38.026764 + 0.3), 7.5f, 0.5f, 4, new Color(255, 255, 255), true, 0);
                                NAPI.Ped.CreatePed(NAPI.Util.GetHashKey("ig_jewelass"), new Vector3(-546.38464, -204.60997, 38.214985), -109.52619f, true, true, true, false, 4294967295);
                            }
                            //Load stuff like blips,textlabel
                            LoadStuff();
                            if (!labelCheck.Text.Contains("Nem"))
                            {
                                Helper.ConsoleLog("error", "[CREDITS]: Bitte die Credits wieder eintragen - Nemesus.de!");
                                NAPI.Task.Run(() =>
                                {
                                    System.Environment.Exit(1);
                                }, delayTime: 5000);
                                return;
                            }
                            //Load whitelist
                            Helper.GetWhitelist();
                            //Load all factions
                            Helper.GetAllFactions();
                            //Load all faction rangs
                            FactionController.GetAllFactionRangs();
                            //Load all faction salarys
                            FactionController.GetAllFactionsSalary();
                            //Load all faction budgets
                            FactionController.GetAllFactionBudgets();
                            //Load all animations
                            Animations.GetAllAnimations();
                            //Load all itemmodels
                            ItemsController.GetAllItemModels();
                            //Load all global items
                            ItemsController.GetAllGlobalItems();
                            //Load all groups
                            GroupsController.GetAllGroups();
                            //Load all doors
                            DoorsController.GetAllDoors();
                            //Load house interiors
                            House.GetAllInteriors();
                            //Load all houses
                            House.GetAllHouses();
                            //Load furniture models categories
                            House.GetAllFurnitureModelCategories();
                            //Load furniture models
                            House.GetAllFurnitureModels();
                            //Load all furniture
                            House.GetAllFurniture();
                            //Check Houses for inactive
                            House.HouseInActiveCheck();
                            //Load all business
                            Business.GetAllBusiness();
                            //Check Business for inactive
                            Business.BizzInActiveCheck();
                            //Load VehicleShops
                            DealerShipController.GetAllVehicleShop(true);
                            //Load all banks
                            BankController.GetAllBanks();
                            //Bank standingorders
                            BankController.RunStandingOrder();
                            //Bank transfers
                            BankController.RunTransfer();
                            //Bank invoices
                            BankController.GetAllInvoices();
                            //Load Spedition Vehicle
                            Helper.LoadSpedVehicles();
                            //Create Spedition Orders
                            SpedOrders.CreateSpedOrders();
                            //Load all shop items
                            Helper.GetAllShopItems();
                            //Load all Smartphones
                            SmartphoneController.GetAllSmartPhones();
                            //Load VehicleShops
                            DealerShipController.GetAllVehicles();
                            //Load all services
                            GroupsController.GetAllServices();
                            //Speedcameras
                            Helper.GetAllSpeedCameras();
                            //CCTVs
                            Helper.GetAllCCTVs();
                            //ATM Jobs
                            Helper.CreateNewATMSpots();
                            //Load all Fahndungen
                            MDCController.GetAllFahndungen();
                            //Gangzones
                            GangController.GetAllGangzones();
                            //Drugplants
                            DrugController.GetAllDrugPlants();
                            //Animals
                            HuntingController.InitAnimals();
                            //Load EUP Outfits
                            Events.LoadEUPOutfits(true, false);
                            //Best Torso
                            Events.LoadBestTorsos();
                            //Reset Factionduty + Groupduty Count
                            System.DateTime moment = new System.DateTime(Helper.UnixTimestamp());
                            if (moment.DayOfWeek == DayOfWeek.Monday)
                            {
                                MySqlCommand command2 = General.Connection.CreateCommand();
                                command2.CommandText = "UPDATE characters SET faction_dutytime = 0 WHERE faction_dutytime > 0";
                                command2.ExecuteNonQuery();
                                command2.CommandText = "UPDATE groups_members SET duty_time = 0 WHERE duty_time > 0";
                                command2.ExecuteNonQuery();
                            }
                            //Load all lifeinvaderads (WIP)
                            //FactionController.GetAllLifeInvaderAds();
                            //General settings
                            NAPI.Server.SetAutoRespawnAfterDeath(false);
                            NAPI.Server.SetGlobalServerChat(false);
                            NAPI.Server.SetGlobalDefaultCommandMessages(true);
                            NAPI.Server.SetCommandErrorMessage("~r~Ungültiger Befehl!");
                            NAPI.Server.SetDefaultSpawnLocation(new Vector3(-542.2647, -208.96895, 37.6498), -154.3716f);
                            //Set time
                            var time = DateTime.Now;
                            NAPI.World.SetTime(time.Hour, time.Minute, 0);
                            //Login zurücksetzen
                            MySqlCommand command = General.Connection.CreateCommand();
                            command.CommandText = "UPDATE characters SET online = 0 WHERE online = 1";
                            command.ExecuteNonQuery();
                            command.CommandText = "UPDATE users SET online = 0 WHERE online = 1";
                            command.ExecuteNonQuery();
                            //Materialversteck befüllen
                            Helper.MatsImVersteck += 20;
                            //Waffendealer
                            GangController.dealerSpawned = false;
                            //Call2Home, kann für Statistiken aktiviert werden
                            //Helper.Call2Home();
                            //Weather
                            try
                            {
                                await Helper.SetAndGetWeather("https://wetterapi.nemesus-world.de", true);
                            }
                            catch (Exception)
                            {
                                Helper.ConsoleLog("success", $"[WETTER-API]: clear sky");
                                Helper.weatherstring = "clear sky";
                                Helper.SetWeather();
                            }
                            //Console
                            Helper.ConsoleLog("info", "-------------------------------------------------------------------------------------------------------");
                            Helper.ConsoleLog("info", "#     #");
                            Helper.ConsoleLog("info", "##    #  ######  #    #  ######   ####   #    #   ####");
                            Helper.ConsoleLog("info", "# #   #  #       ##  ##  #       #       #    #  #");
                            Helper.ConsoleLog("info", "#  #  #  #####   # ## #  #####    ####   #    #   ####");
                            Helper.ConsoleLog("info", "#   # #  #       #    #  #            #  #    #       #");
                            Helper.ConsoleLog("info", "#    ##  #       #    #  #       #    #  #    #  #    #");
                            Helper.ConsoleLog("info", "#     #  ######  #    #  ######   ####    ####    ####");
                            Helper.ConsoleLog("info", "");
                            Helper.ConsoleLog("info", $"Gamemode: Nemesus World RageMP by Nemesus.de");
                            Helper.ConsoleLog("info", $"Lizenz: CC-BY-NC-SA-4.0");
                            Helper.ConsoleLog("info", $"Support: https://discord.nemesus.de");
                            Helper.ConsoleLog("info", $"Version: {Helper.gamemodeVersion}");
                            if (Helper.adminSettings.voicerp == 1)
                            {
                                Helper.ConsoleLog("info", $"Roleplay-Art: Voice-RP (SaltyChat)");
                            }
                            else if (Helper.adminSettings.voicerp == 2)
                            {
                                Helper.ConsoleLog("info", $"Roleplay-Art: Voice-RP (Ingame-Voice)");
                            }
                            else
                            {
                                Helper.ConsoleLog("info", $"Roleplay-Art: Text-RP");
                            }
                            Helper.ConsoleLog("info", $"Status: Alle Systeme wurden erfolgreich geladen!");
                            Helper.ConsoleLog("info", "-------------------------------------------------------------------------------------------------------");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnResourceStart]: " + e.ToString());
            }
        }

        //Timer
        public static void OnHalfHourTimer(object state)
        {
            try
            {
                System.Threading.Tasks.Task.Run(() =>
                {
                    NAPI.Task.Run(async () =>
                    {
                        halfHourCounter++;
                        //Onlinebanking transfer
                        BankController.RunTransfer();
                        //Smartphone capacity
                        foreach (Smartphone smartphone in SmartphoneController.smartphoneList)
                        {
                            if (smartphone != null && Helper.GetPlayerByCharacterName(smartphone.owner) != null)
                            {
                                if (smartphone.akku > 0)
                                {
                                    smartphone.akku--;
                                }
                                SmartphoneController.OnSaveSmartphone(null, smartphone.phoneprops, smartphone.contacts, "0189" + smartphone.phonenumber);
                            }
                        }
                        //Animals
                        foreach (Ped ped in HuntingController.SpawnedAnimals.ToList())
                        {
                            if (ped.GetSharedData<int>("Ped:Death") == 0 && !Helper.IsInRangeOfPoint(ped.Position, new Vector3(1714.7175, -571.90814, 144.50644), 105))
                            {
                                HuntingController.CreateNewAnimal(ped);
                            }
                        }
                        //Create new dirt
                        Helper.CreateNewGarbage();
                        //Save cars
                        DealerShipController.SaveVehicleData();
                        //Save bank
                        foreach (Bank bank in BankController.bankList)
                        {
                            if (bank != null)
                            {
                                BankController.SaveBank(bank);
                            }
                        }
                        //Spieler
                        //Essen/Trinken/Leben abziehen
                        foreach (Player player in NAPI.Pools.GetAllPlayers())
                        {
                            if (player != null && player.GetOwnSharedData<bool>("Player:Spawned") == true && player.GetSharedData<bool>("Player:Death") == false)
                            {
                                Character character = Helper.GetCharacterData(player);
                                Account account = Helper.GetAccountData(player);
                                TempData tempData = Helper.GetCharacterTempData(player);
                                if (character != null && account != null && tempData != null)
                                {
                                    if (account.prison == 0 && character.afk == 0)
                                    {
                                        if (character.thirst >= 0 || character.hunger >= 0)
                                        {
                                            if (character.arrested == 0)
                                            {
                                                if (character.thirst >= 3)
                                                {
                                                    character.thirst -= 3;
                                                }
                                                else
                                                {
                                                    character.thirst = 0;
                                                }
                                                if (character.hunger >= 3)
                                                {
                                                    character.hunger -= 3;
                                                }
                                                else
                                                {
                                                    character.hunger = 0;
                                                }
                                                player.SetOwnSharedData("Player:Needs", character.hunger + "," + character.thirst + "," + character.afk);
                                            }
                                        }
                                        //Krankheit
                                        if (character.disease == 0)
                                        {
                                            if ((Helper.weatherstring.Contains("rain") || Helper.weatherstring.Contains("thunderstorm") || Helper.weatherstring.Contains("snow")))
                                            {
                                                if (Helper.GetRandomPercentage(4))
                                                {
                                                    character.disease = 2;
                                                }
                                            }
                                            else
                                            {
                                                if (Helper.GetRandomPercentage(2))
                                                {
                                                    character.disease = 2;
                                                }
                                            }
                                        }
                                        //Robmoney
                                        if (halfHourCounter >= 2 && character.abusemoney > 0)
                                        {
                                            if (character.abusemoney > 2500)
                                            {
                                                character.abusemoney = character.abusemoney / 2;
                                            }
                                            else
                                            {
                                                character.abusemoney = 0;
                                            }
                                        }
                                    }
                                }
                                if (FireController.startFire == false && character.faction == 2 && character.factionduty == true && Helper.GetRandomPercentage(55) && Helper.GetFactionCountDuty(2) >= 2)
                                {
                                    FireController.BuildFire(player);
                                }
                            }
                        }
                        //Hour timer
                        if (halfHourCounter >= 2)
                        {
                            fullHourTimer++;
                            System.DateTime moment = new System.DateTime(Helper.UnixTimestamp());
                            if (moment.Hour == 18 && moment.DayOfWeek == DayOfWeek.Wednesday && lottoStart == false)
                            {
                                StartLotto();
                            }
                            //Material Versteck
                            Helper.MatsImVersteck += 20;
                            //Factions reloaden
                            Helper.GetAllFactions(1);
                            //Waffendealer
                            DateTime dateTime = DateTime.Now;
                            if (GangController.dealerSpawned == false && (dateTime.Hour == 10 || dateTime.Hour == 13 || dateTime.Hour == 16 || dateTime.Hour == 18 || dateTime.Hour == 20 || dateTime.Hour == 23))
                            {
                                GangController.CreateWaffendealer();
                            }
                            if (dateTime.Hour == 11 || dateTime.Hour == 14 || dateTime.Hour == 17 || dateTime.Hour == 19 || dateTime.Hour == 21 || dateTime.Hour == 24 || dateTime.Hour == 0)
                            {
                                GangController.DeleteWaffendealer();
                            }
                            //Drogendealer
                            if (GangController.dealerPosition2 == null && (dateTime.Hour == 11 || dateTime.Hour == 14 || dateTime.Hour == 17 || dateTime.Hour == 19 || dateTime.Hour == 21 || dateTime.Hour == 24 || dateTime.Hour == 0))
                            {
                                GangController.CreateDrugDealer();
                            }
                            if (dateTime.Hour == 12 || dateTime.Hour == 15 || dateTime.Hour == 18 || dateTime.Hour == 20 || dateTime.Hour == 22 || dateTime.Hour == 1)
                            {
                                GangController.DeleteDrugDealer();
                            }
                            if (fullHourTimer >= 4)
                            {
                                //Gangzonen
                                foreach (GangZones gangZones in GangController.gangzoneList)
                                {
                                    if (gangZones.percentages.Length > 3 && gangZones.percentages != "n/A")
                                    {
                                        if (gangZones.value == "Geld")
                                        {
                                            gangZones.things += 71 * 4;
                                        }
                                        else if (gangZones.value == "Materialien")
                                        {
                                            gangZones.things += 25;
                                        }
                                        else if (gangZones.value == "Drogen")
                                        {
                                            Random random = new Random();
                                            gangZones.things += random.Next(0, 2);
                                        }
                                    }
                                }
                                //KFZ Steuer Gruppierungen
                                int kfzsteuer = 0;
                                foreach (Groups groups in GroupsController.groupList)
                                {
                                    kfzsteuer = 0;
                                    if (groups != null && groups.banknumber != "n/A")
                                    {
                                        Bank bank = BankController.GetBankByBankNumber(groups.banknumber);
                                        if (bank != null)
                                        {
                                            foreach (Cars car in Cars.carList)
                                            {
                                                if (car != null && car.vehicleData != null && car.vehicleData.plate.Length > 0 && car.vehicleData.owner == "group-" + groups.id)
                                                {
                                                    VehicleShop vehicleShop = DealerShipController.GetVehicleShopByVehicleName(car.vehicleData.vehiclename);
                                                    kfzsteuer += (int)(vehicleShop.price / 100 * Helper.adminSettings.ksteuer);
                                                }
                                            }
                                            if (kfzsteuer > 0)
                                            {
                                                bank.bankvalue -= kfzsteuer / 2;
                                                Helper.BankSettings(bank.banknumber, "KFZ-Steuern bezahlt", "" + kfzsteuer / 2, "Firma");
                                                Helper.SetGovMoney(kfzsteuer / 2, "KFZ-Steuer Einzahlung von " + groups.name);
                                            }
                                        }
                                    }
                                }
                                fullHourTimer = 0;
                                //Auto Restart
                                if (Events.RestartHour > -1 && moment.Hour == Events.RestartHour)
                                {
                                    if (Events.InitRestart == false)
                                    {
                                        Helper.SendAdminMessageToAll($"Der Server wird in ca. 5 Sekunden neugestartet ...");
                                        Events.InitRestart = true;
                                        NAPI.Task.Run(() =>
                                        {
                                            System.Environment.Exit(1);
                                        }, delayTime: 5000);
                                    }
                                }
                            }
                            //halfHourCounter Reset
                            halfHourCounter = 0;
                        }
                        //Weather jede 30 Minuten checken
                        try
                        {
                            await Helper.SetAndGetWeather("https://wetterapi.nemesus-world.de", true);
                        }
                        catch (Exception)
                        {
                            Helper.weatherstring = "clear sky";
                            Helper.SetWeather();
                        }
                    });
                });
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnHalfHourTimer]: " + e.ToString());
            }
        }

        public static void OnServerSave(object state)
        {
            try
            {
                NAPI.Task.Run(() =>
                {
                    Helper.SendAdminMessage3($"Alle Daten vom Server wurden gespeichert!");
                    Events.SaveAll();
                });
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnServerSave]: " + e.ToString());
            }
        }

        public static void OnSecondTimer(object state)
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                NAPI.Task.Run(() =>
                {
                    try
                    {
                        //Vehicle Health System
                        foreach (Vehicle vehicle in NAPI.Pools.GetAllVehicles())
                        {
                            if (vehicle != null)
                            {
                                if (vehicle.GetSharedData<String>("Vehicle:Name") == "iak_wheelchair") continue;
                                Player driver = (Player)NAPI.Vehicle.GetVehicleDriver(vehicle);
                                vehicle.SetSharedData("Vehicle:EngineHealth", NAPI.Vehicle.GetVehicleEngineHealth(vehicle));
                                vehicle.SetSharedData("Vehicle:BodyHealth", NAPI.Vehicle.GetVehicleBodyHealth(vehicle));
                                vehicle.SetSharedData("Vehicle:Health", NAPI.Vehicle.GetVehicleHealth(vehicle));
                                if (vehicle.EngineStatus == true)
                                {
                                    if (NAPI.Vehicle.GetVehicleEngineHealth(vehicle) <= 215 || NAPI.Vehicle.GetVehicleHealth(vehicle) <= 215)
                                    {
                                        Helper.SetVehicleEngine(vehicle, false);
                                        NAPI.Vehicle.SetVehicleEngineHealth(vehicle, 125);
                                        NAPI.Vehicle.SetVehicleHealth(vehicle, 125);
                                        if (driver != null && driver.Vehicle != null)
                                        {
                                            Helper.SendNotificationWithoutButton(driver, "Der Motor ist ausgefallen!", "error");
                                            driver.SetOwnSharedData("Player:VehicleEngine", false);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Helper.ConsoleLog("error", $"[OnSecondTimer]: " + e.ToString());
                    }
                });
            });
        }

        public static void OnHalfOneMinuteTimer(object state)
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                NAPI.Task.Run(() =>
                {
                    try
                    {
                        //Half minute
                        halfMinuteCounter++;
                        foreach (Player player in NAPI.Pools.GetAllPlayers())
                        {
                            if (player != null && Account.IsPlayerLoggedIn(player) && player.IsInVehicle && player.GetOwnSharedData<bool>("Player:Spawned") == true)
                            {
                                //Taxi Taxameter
                                if (Helper.IsATaxiDriver(player) > -1 && (player.Vehicle.GetSharedData<String>("Vehicle:Name") == "taxi" || player.Vehicle.GetSharedData<String>("Vehicle:Name") == "tourbus"))
                                {
                                    if (player.HasData("Player:Fare") && player.HasData("Player:TaxameterOn"))
                                    {
                                        player.TriggerEvent("Client:UpdateKilometreTaxi", 0);
                                    }
                                }
                            }
                        }
                        if (halfMinuteCounter >= 2)
                        {
                            halfMinuteCounter = 0;
                            //One minute
                            //AddinfoBox
                            if (Helper.infoTextList.Count > 0)
                            {
                                foreach (AddInfoBox addInfoBox in Helper.infoTextList.ToList())
                                {
                                    if(addInfoBox.created <= Helper.UnixTimestamp())
                                    {
                                        addInfoBox.label.Delete();
                                        addInfoBox.marker.Delete();
                                        Helper.infoTextList.Remove(addInfoBox);
                                    }
                                }
                            }
                            //Drugplants
                            DrugController.DrugPlantsCheck();
                            //Animals
                            HuntingController.SetNewAnimalsController();
                            //All players
                            foreach (Player player in NAPI.Pools.GetAllPlayers())
                            {
                                if (player != null && Account.IsPlayerLoggedIn(player) && player.GetOwnSharedData<bool>("Player:Spawned") == true)
                                {
                                    Character character = Helper.GetCharacterData(player);
                                    Account account = Helper.GetAccountData(player);
                                    TempData tempData = Helper.GetCharacterTempData(player);
                                    if (character != null && account != null && tempData != null)
                                    {
                                        //Hunger Thirst
                                        if ((character.hunger <= 0 || character.thirst <= 0) && Helper.GetRandomPercentage(65) && character.afk == 0 && character.death == false && tempData.adminduty == false)
                                        {
                                            Helper.SetPlayerHealth(player, (player.GetOwnSharedData<int>("Player:Health") - 100) - 3);
                                        }
                                        //Crystal-Meth
                                        if (player.HasData("Player:HealBonus") && player.GetData<int>("Player:HealBonus") > -1)
                                        {
                                            if (NAPI.Player.GetPlayerHealth(player) < 105)
                                            {
                                                Helper.SetPlayerHealth(player, (player.GetOwnSharedData<int>("Player:Health") - 100) + 15);
                                            }
                                            if (NAPI.Player.GetPlayerHealth(player) > 105)
                                            {
                                                Helper.SetPlayerHealth(player, 105);
                                            }
                                            player.SetData("Player:HealBonus", player.GetData<int>("Player:HealBonus") - 1);
                                            if (player.GetData<int>("Player:HealBonus") <= 0)
                                            {
                                                player.SetData("Player:HealBonus", -1);
                                                player.ResetData("Player:HealBonus");
                                            }
                                        }
                                        //Arrestsystem
                                        if (character.arrested > 0)
                                        {
                                            character.arrested--;
                                            if (character.arrested == 0 && Helper.GetFactionCountDuty(1) <= 0)
                                            {
                                                character.arrested = 0;
                                                character.cell = 0;
                                                MySqlCommand command = General.Connection.CreateCommand();
                                                command.CommandText = "INSERT INTO policefile (name, police, text, timestamp, commentary) VALUES (@name, @police, @text, @timestamp, @commentary)";
                                                command.Parameters.AddWithValue("@name", character.name);
                                                command.Parameters.AddWithValue("@police", "System");
                                                command.Parameters.AddWithValue("@text", $"Aus Inhaftierung freigelassen, Grund: Haftzeit abgelaufen");
                                                command.Parameters.AddWithValue("@timestamp", Helper.UnixTimestamp());
                                                command.Parameters.AddWithValue("@commentary", 0);
                                                command.ExecuteNonQuery();
                                                Helper.SendNotificationWithoutButton(player, $"Du wurdest aus der Inhaftierung automatisch freigelassen!", "success", "top-left", 7500);
                                                Helper.CreateFactionLog(character.faction, $"{character.name} wurde automatisch aus der Inhaftierung freigelassen, Grund: Haftzeit abgelaufen");
                                                player.TriggerEvent("Client:SetArrested", false);
                                                player.Dimension = 0;
                                                Helper.SetPlayerPosition(player, new Vector3(639.8409, -3.0207262, 82.788246));
                                                player.Heading = -163.85979f;
                                            }
                                        }
                                        //Fuelsystem
                                        if (player.HasData("Player:FuelCooldown") && player.GetData<int>("Player:FuelCooldown") > 0)
                                        {
                                            if (Helper.UnixTimestamp() > player.GetData<int>("Player:FuelCooldown"))
                                            {
                                                character.abusemoney += player.GetData<int>("Player:FuelPrice");
                                                player.SetData<int>("Player:FuelPrice", 0);
                                                player.SetData<int>("Player:FuelCooldown", 0);
                                                Helper.SendNotificationWithoutButton(player, "Du hast deine Tankrechnung nicht bezahlt, der Besitzer der Tankstelle hat dich bei der Polizei gemeldet!", "info", "top-left", 5500);
                                                Business bizz = Business.GetBusinessById(player.GetData<int>("Player:FuelBizz"));
                                                if (bizz != null)
                                                {
                                                    string data = "Gemeldeter Tankdiebstahl vom Fahrzeug mit dem Kennzeichen: " + player.GetData<int>("Player:FuelCooldown") + ", beim Business: " + bizz.name + "!";
                                                    MDCController.CreateFahndung(data);
                                                }
                                                player.SetData<int>("Player:FuelBizz", 0);
                                                player.SetData<string>("Player:NumberPlate", "n/A");
                                            }
                                        }
                                        //Payday
                                        if (player.GetOwnSharedData<string>("Player:Needs").Split(",")[2] == "0" && tempData.adminduty == false)
                                        {
                                            character.payday_points++;
                                            if (character.payday_points >= 60)
                                            {
                                                Helper.CheckPayday(player);
                                            }
                                        }
                                        //Probefahrt
                                        if (tempData.dealerShip != null && player.HasData("Player:TestDrive") && Helper.UnixTimestamp() > player.GetData<int>("Player:TestDrive"))
                                        {
                                            Helper.SetPlayerPosition(player, tempData.furnitureOldPosition);
                                            player.Dimension = 0;
                                            player.ResetData("Player:TestDrive");
                                            player.SetData<int>("Player:TestDriveCD", Helper.UnixTimestamp() + (60 * 5));
                                            Helper.SendNotificationWithoutButton2(player, "Probefahrt beendet, hat Ihnen das Fahrzeug gefallen?", "success", "center", 3250);
                                            tempData.dealerShip.Delete();
                                            tempData.dealerShip = null;
                                        }
                                        //Premium
                                        if (account.premium > 0 && Helper.UnixTimestamp() > account.premium_time)
                                        {
                                            account.premium = 0;
                                            account.premium_time = Helper.UnixTimestamp();
                                            Helper.SendNotificationWithoutButton(player, "Dein Premiumaccount ist abgelaufen!", "info", "center", 3250);
                                        }
                                        //Animals
                                        if (HuntingController.animalsController != null && player.Id == HuntingController.animalsController.Id)
                                        {
                                            foreach (Ped ped in HuntingController.SpawnedAnimals.ToList())
                                            {
                                                if (ped.Controller == null)
                                                {
                                                    ped.Controller = player;
                                                }
                                                if (ped.GetSharedData<int>("Ped:Death") == 0)
                                                {
                                                    player.TriggerEvent("Client:UpdateAnimals", ped);
                                                }
                                            }
                                        }
                                        //Pet
                                        if (tempData.pet != null)
                                        {
                                            if (!Helper.IsInRangeOfPoint(player.Position, tempData.pet.Position, 25.5f) || player.Dimension != tempData.pet.Dimension)
                                            {
                                                Helper.PetRefresh(player);
                                            }
                                        }
                                        //Drunk
                                        if (tempData.drunk > 0)
                                        {
                                            if (tempData.drunked == true && tempData.drunk < 5)
                                            {
                                                tempData.drunked = false;
                                                player.TriggerEvent("Client:SetDrunk", false);
                                                player.SetSharedData("Player:WalkingStyle", character.walkingstyle);
                                            }
                                            if (tempData.drunk >= 5 && tempData.drunked == false)
                                            {
                                                tempData.drunked = true;
                                                player.TriggerEvent("Client:SetDrunk", true);
                                                player.SetSharedData("Player:WalkingStyle", "move_m@drunk@a");
                                            }
                                            if (tempData.drunk > 0) tempData.drunk--;
                                        }
                                        //Ping Check
                                        if (player.Ping >= 200 && tempData.isping == false)
                                        {
                                            tempData.pingcheck++;
                                            if (tempData.pingcheck >= 3)
                                            {
                                                tempData.isping = true;
                                                tempData.pingcheck = 0;
                                                player.TriggerEvent("Client:SetPing");
                                            }
                                        }
                                    }
                                }
                            }
                            //Time + Weather
                            var time = DateTime.Now;
                            NAPI.World.SetTime(time.Hour, time.Minute, 0);
                            //Spedorders
                            if (SpedOrders.spedOrderList.Count < 9)
                            {
                                SpedOrders.CreateNewSpedOrder();
                            }
                            //tenMinutesTimerCheck
                            tenMinutesTimerCheck++;
                            //Fuel + oil + battery system
                            foreach (Vehicle vehicle in NAPI.Pools.GetAllVehicles())
                            {
                                Player driver = (Player)NAPI.Vehicle.GetVehicleDriver(vehicle);
                                if (vehicle.GetSharedData<String>("Vehicle:Name") == "iak_wheelchair") continue;
                                if (vehicle != null && vehicle.EngineStatus == true)
                                {
                                    if (vehicle.Class != 13 && vehicle.EngineStatus == true)
                                    {
                                        //Fuel
                                        Random rand = new Random();
                                        double minusfuel = (vehicle.MaxSpeed / 100) * 0.75;
                                        if (minusfuel > 3)
                                        {
                                            minusfuel = 3.0;
                                        }
                                        if (minusfuel < 0.1)
                                        {
                                            minusfuel = 0.1;
                                        }
                                        if (vehicle.GetSharedData<float>("Vehicle:Fuel") >= minusfuel && vehicle.GetSharedData<float>("Vehicle:Fuel") > 0)
                                        {
                                            vehicle.SetSharedData("Vehicle:Fuel", (vehicle.GetSharedData<float>("Vehicle:Fuel") - minusfuel));
                                        }
                                        else
                                        {
                                            vehicle.SetSharedData("Vehicle:Fuel", 0.0);
                                            Helper.SetVehicleEngine(vehicle, false);
                                            if (driver != null)
                                            {
                                                Helper.SendNotificationWithoutButton(driver, "Der Motor ist ausgefallen!", "error");
                                                driver.SetOwnSharedData("Player:VehicleEngine", false);
                                            }
                                        }
                                        vehicleTimerCheck++;
                                        if (vehicleTimerCheck == 8)
                                        {
                                            //Battery
                                            if (vehicle.GetSharedData<int>("Vehicle:Battery") > 0)
                                            {
                                                vehicle.SetSharedData("Vehicle:Battery", (vehicle.GetSharedData<int>("Vehicle:Battery") - 1));
                                            }
                                            else
                                            {
                                                vehicle.SetSharedData("Vehicle:Battery", 0);
                                                Helper.SetVehicleEngine(vehicle, false);
                                                if (driver != null && driver.Vehicle != null)
                                                {
                                                    Helper.SendNotificationWithoutButton(driver, "Der Motor ist ausgefallen!", "error");
                                                    driver.SetOwnSharedData("Player:VehicleEngine", false);
                                                }
                                            }
                                            //Oil
                                            if (vehicle.GetSharedData<int>("Vehicle:Oel") > 0)
                                            {
                                                vehicle.SetSharedData("Vehicle:Oel", (vehicle.GetSharedData<int>("Vehicle:Oel") - 1));
                                            }
                                            else
                                            {
                                                vehicle.SetSharedData("Vehicle:Oel", 0);
                                                Helper.SetVehicleEngine(vehicle, false);
                                                if (driver != null && driver.Vehicle != null)
                                                {
                                                    Helper.SendNotificationWithoutButton(driver, "Der Motor ist ausgefallen!", "error");
                                                    driver.SetOwnSharedData("Player:VehicleEngine", false);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            //After five minutes
                            if (tenMinutesTimerCheck % 5 == 0)
                            {
                                while (Helper.taxiBotList.Count < 6)
                                {
                                    Helper.CreateTaxiBot();
                                }
                                Helper.CreateNewATMSpots();
                            }
                            //After fifteen minutes
                            if (tenMinutesTimerCheck == 15 || tenMinutesTimerCheck == 30)
                            {
                                //MYSQL Check
                                if (General.Connection.State != ConnectionState.Open)
                                {
                                    Helper.ConsoleLog("error", "[MYSQL]: Verbindung wurde unerwartet beendet!");
                                    General.InitConnection();
                                }
                                //Trailer reseten + Authaus Fahrzeuge
                                foreach (Vehicle veh in NAPI.Pools.GetAllVehicles())
                                {
                                    if (veh != null && veh.Exists)
                                    {
                                        if ((Helper.IsATrailer(veh) && !Helper.IsTraileredBy(veh)) && veh.HasData("Vehicle:Position"))
                                        {
                                            veh.Position = veh.GetData<Vector3>("Vehicle:Position");
                                            veh.Rotation = new Vector3(0.0, 0.0, veh.GetData<float>("Vehicle:Rotation"));
                                            veh.EngineStatus = false;
                                        }
                                    }
                                }
                                foreach (Player player in NAPI.Pools.GetAllPlayers())
                                {
                                    if (player != null && player.GetOwnSharedData<bool>("Player:Spawned") == true)
                                    {
                                        Character character = Helper.GetCharacterData(player);
                                        Account account = Helper.GetAccountData(player);
                                        TempData tempData = Helper.GetCharacterTempData(player);
                                        if (character != null && account != null)
                                        {
                                            if (character.disease > 0 && Helper.GetRandomPercentage(45) && character.death == false)
                                            {
                                                NAPI.Task.Run(() =>
                                                {
                                                    if (!player.IsInVehicle)
                                                    {
                                                        Helper.PlayShortAnimation(player, "timetable@gardener@smoking_joint", "idle_cough", 4500);
                                                    }
                                                    Helper.SendNotificationWithoutButton(player, "Du fühlst dich nicht gut ...", "info");
                                                }, delayTime: 60000);
                                            }
                                            //Gangzones
                                            if (character.afk == 0 && tempData.ingangzone != -1 && character.mygroup != -1)
                                            {
                                                Groups group = GroupsController.GetGroupById(character.mygroup);
                                                if (group != null && group.status == 0 && tempData.adminduty == false)
                                                {
                                                    GangController.UpdateGangZoneValues(tempData.ingangzone, group.id, 1);
                                                }
                                            }
                                            //Ping reset
                                            if (tempData.isping == false)
                                            {
                                                tempData.pingcheck = 0;
                                            }
                                        }
                                    }
                                }
                            }
                            //After thirty minutes
                            if (tenMinutesTimerCheck >= 30)
                            {
                                OnHalfHourTimer(state);
                                tenMinutesTimerCheck = 0;
                            }

                        }
                    }
                    catch (Exception e)
                    {
                        Helper.ConsoleLog("error", $"[OnHalfOneMinuteTimer]: " + e.ToString());
                    }
                });
            });
        }

        public static void StartLotto()
        {
            try
            {
                lottoStart = true;
                string lottoEnd = "";
                int count = 0;
                Random random = new Random();
                for (int i = 0; i < 5; i++)
                {
                    lottoEnd = lottoEnd + $"{random.Next(1, 49)},";
                }
                lottoEnd = lottoEnd.Remove(lottoEnd.Length - 1);
                string[] lottoArray1 = new string[6];
                lottoArray1 = lottoEnd.Split(",");
                string[] lottoArray2 = new string[6];
                foreach (Player player in NAPI.Pools.GetAllPlayers())
                {
                    count = 0;
                    if (player != null && player.GetOwnSharedData<bool>("Player:Spawned") == true)
                    {
                        Character character = Helper.GetCharacterData(player);
                        if (character != null && character.defaultbank != "n/A")
                        {
                            Items item = ItemsController.GetItemByItemName(player, "L-Schein");
                            if (item != null)
                            {
                                lottoArray2 = item.props.Split(",");
                                if (lottoArray1[0] == lottoArray2[0])
                                {
                                    count++;
                                }
                                if (lottoArray1[1] == lottoArray2[1])
                                {
                                    count++;
                                }
                                if (lottoArray1[2] == lottoArray2[2])
                                {
                                    count++;
                                }
                                if (lottoArray1[3] == lottoArray2[3])
                                {
                                    count++;
                                }
                                if (lottoArray1[4] == lottoArray2[4])
                                {
                                    count++;
                                }
                                if (count > 0)
                                {
                                    Bank bank = BankController.GetBankByBankNumber(character.defaultbank);
                                    if (bank != null)
                                    {
                                        Helper.BankSettings(character.defaultbank, "Lottogewinn", "" + count * 12500, "Lotto");
                                        bank.bankvalue += count * 12500;
                                    }
                                    else
                                    {
                                        count = 0;
                                    }
                                }
                                SmartphoneController.OnSmartphoneMessage(null, Helper.UnixTimestamp(), character.lastsmartphone, "01896337", $"Lottoziehung {lottoEnd}: Du hast {count}x Richtige - Gewinn: {count * 12500}$");
                                ItemsController.RemoveItem(player, item.itemid);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[StartLotto]: " + e.ToString());
            }
        }

        [ServerEvent(Event.ResourceStopEx)]
        public static void OnResourceStopEx()
        {
            try
            {
                OnResourceStop();
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnResourceStopEx]: " + e.ToString());
            }
        }

        [ServerEvent(Event.ResourceStop)]
        public static void OnResourceStop()
        {
            try
            {
                SaveAll();
                if (Helper.infoTextList.Count > 0)
                {
                    foreach (AddInfoBox addInfoBox in Helper.infoTextList.ToList())
                    {
                        addInfoBox.label.Delete();
                        addInfoBox.marker.Delete();
                        Helper.infoTextList.Remove(addInfoBox);
                    }
                }
                foreach (Player player in NAPI.Pools.GetAllPlayers())
                {
                    //Secruity check
                    if (player.HasData("Player:MoneyInfos"))
                    {
                        string[] moneyArray = new string[3];
                        moneyArray = player.GetData<string>("Player:MoneyInfos").Split(",");
                        if (moneyArray[0].Length > 0 && moneyArray[1].Length > 0)
                        {
                            SmartphoneController.OnSmartphoneMessage(null, Helper.UnixTimestamp(), moneyArray[0], "01897337", $"Der Geldtransport in Höhe von {player.GetData<int>("Player:Money")}$ wurde abgebrochen, das Geld wurde zurückgebracht!");
                            Business bizz = Business.GetBusinessById(Convert.ToInt32(moneyArray[2]));
                            if (bizz != null)
                            {
                                bizz.cash += player.GetData<int>("Player:Money");
                            }
                            Helper.AddRemoveAttachments(player, "moneyBag", false);
                            player.ResetData("Player:Money");
                            player.ResetData("Player:MoneyInfos");
                        }
                    }
                    TempData tempData = Helper.GetCharacterTempData(player);
                    Character character = Helper.GetCharacterData(player);
                    if (tempData != null && character != null)
                    {
                        //Undercover
                        if (tempData.undercover != "")
                        {
                            tempData.undercover = "";
                            character.name = player.GetSharedData<string>("Client:OldName");
                            player.Name = character.name;
                            player.SetSharedData("Client:OldName", "n/A");
                        }
                    }
                }
                foreach (Vehicle vehicle in NAPI.Pools.GetAllVehicles())
                {
                    //Secruity Check
                    if (vehicle.HasData("Vehicle:MoneyInfos"))
                    {
                        string[] moneyArray = new string[3];
                        moneyArray = vehicle.GetData<string>("Vehicle:MoneyInfos").Split(",");
                        if (moneyArray[0].Length > 0 && moneyArray[1].Length > 0)
                        {
                            SmartphoneController.OnSmartphoneMessage(null, Helper.UnixTimestamp(), moneyArray[0], "01897337", $"Der Geldtransport in Höhe von {vehicle.GetData<int>("Vehicle:Money")}$ wurde abgebrochen, das Geld wurde zurückgebracht!");
                            Business bizz = Business.GetBusinessById(Convert.ToInt32(moneyArray[2]));
                            if (bizz != null)
                            {
                                bizz.cash += vehicle.GetData<int>("Vehicle:Money");
                            }
                            vehicle.ResetData("Vehicle:Money");
                            vehicle.ResetData("Vehicle:MoneyInfos");
                        }
                    }
                    //Jacked
                    NAPI.Task.Run(() =>
                    {
                        if (vehicle.HasData("Vehicle:JackedObject"))
                        {
                            vehicle.SetData<bool>("Vehicle:Jacked", false);
                            vehicle.ResetData("Vehicle:Jacked");
                            vehicle.GetData<GTANetworkAPI.Object>("Vehicle:JackedObject").Delete();
                            vehicle.ResetData("Vehicle:JackedObject");

                            string[] vehicleArray = new string[7];
                            vehicleArray = vehicle.GetSharedData<string>("Vehicle:Sync").Split(",");
                            vehicle.SetSharedData("Vehicle:Sync", $"0,0,{vehicleArray[2]},{vehicleArray[3]},{vehicleArray[4]},0,{vehicleArray[6]}");
                            Helper.SetVehicleEngine(vehicle, false);
                        }
                    }, delayTime: 50);
                }
                //Delete global item handler
                foreach (ItemsGlobal globalitem in ItemsController.itemListGlobal)
                {
                    if (globalitem != null && globalitem.owneridentifier == "Ground")
                    {
                        if (globalitem.objectHandle != null)
                        {
                            globalitem.objectHandle.Delete();
                            globalitem.objectHandle = null;
                            globalitem.textHandle.Delete();
                            globalitem.textHandle = null;
                        }
                    }
                }
                //Delete closedzone
                if (Events.closesZone != null)
                {
                    Events.closesZone.Delete();
                    Events.closesZone = null;
                }
                //Delete /veh2
                if (Commands.adminVehicles.Count <= 0)
                {
                    foreach (Vehicle adminVehicle in Commands.adminVehicles)
                    {
                        if (adminVehicle != null)
                        {
                            if (NAPI.Vehicle.GetVehicleDriver(adminVehicle) != null)
                            {
                                NAPI.Player.WarpPlayerOutOfVehicle((Player)NAPI.Vehicle.GetVehicleDriver(adminVehicle));
                            }
                            NAPI.Task.Run(() =>
                            {
                                Helper.SetVehicleEngine(adminVehicle, false);
                                Commands.adminVehicles.Remove(adminVehicle);
                                adminVehicle.Delete();
                            }, delayTime: 115);
                        }
                    }
                }
                //Leistellen Hinweis
                FactionController.OperatorBlip = false;
                //Delete police props
                if (Helper.policePropList.Count > 0)
                {
                    foreach (PoliceProps policeProp in Helper.policePropList.ToList())
                    {
                        policeProp.obj.Delete();
                        policeProp.obj = null;
                        Helper.policePropList.Remove(policeProp);
                    }
                }
                //Delete spike strips
                if (Helper.spikeStripList.Count > 0)
                {
                    foreach (SpikeStrip spikestrips in Helper.spikeStripList.ToList())
                    {
                        spikestrips.colshape.Delete();
                        spikestrips.colshape = null;
                        spikestrips.spikeobject.Delete();
                        spikestrips.spikeobject = null;
                        Helper.spikeStripList.Remove(spikestrips);
                    }
                }
                Helper.ConsoleLog("success", "[Server]: Du brauchst Support? Schau gerne mal auf dem Nemesus.de Discord vorbei - (https://discord.nemesus.de)!");
                Helper.ConsoleLog("success", "[Server]: Server wird beendet ...");
                NAPI.Task.Run(() =>
                {
                    //Close database connection
                    if (General.DatabaseConnectionCheck == true)
                    {
                        Helper.ConsoleLog("success", "[MYSQL]: Verbindung wurde beendet!");
                        General.Connection.Close();
                        General.DatabaseConnectionCheck = false;
                    }
                    Environment.Exit(0);
                }, delayTime: 7500);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnResourceStop]: " + e.ToString());
            }
        }

        public static void SaveAll()
        {
            try
            {
                System.Threading.Tasks.Task.Run(() =>
                {
                    NAPI.Task.Run(() =>
                    {
                        if (General.Connection.State != ConnectionState.Open) return;

                        PetaPoco.Database db = new PetaPoco.Database(General.Connection);

                        //Save accounts/characters
                        foreach (Player p in NAPI.Pools.GetAllPlayers())
                        {
                            if (p != null && Account.IsPlayerLoggedIn(p))
                            {
                                Character character = Helper.GetCharacterData(p);
                                Account account2 = Helper.GetAccountData(p);
                                if (character != null && character.name.Length > 3)
                                {
                                    CharacterController.SaveCharacter(p);
                                }
                                if (account2 != null && account2.name.Length > 3)
                                {
                                    Account.SaveAccount(p);
                                }
                            }
                        }
                        //Adminsettings
                        Helper.SaveAdminSettings();
                        //Smartphones
                        foreach (Smartphone smartphone in SmartphoneController.smartphoneList)
                        {
                            SmartphoneController.SaveSmartphone(smartphone);
                        }
                        //Save Factions
                        Helper.SaveFactions();
                        //Save globale items
                        ItemsController.SaveAllGlobalItems();
                        //Save houses
                        foreach (House house in House.houseList)
                        {
                            if (house != null)
                            {
                                House.SaveHouse(house);
                            }
                        }
                        //Save Furniture
                        foreach (FurnitureSetHouse furniture in House.furnitureList)
                        {
                            if (furniture != null)
                            {
                                House.SaveFurniture(furniture);
                            }
                        }
                        //Save bizz
                        foreach (Business bizz in Business.businessList)
                        {
                            if (bizz != null)
                            {
                                Business.SaveBusiness(bizz);
                            }
                        }
                        //Save cars
                        DealerShipController.SaveVehicleData();
                        //Save bank
                        foreach (Bank bank in BankController.bankList)
                        {
                            if (bank != null)
                            {
                                BankController.SaveBank(bank);
                            }
                        }
                        //Waffenkammer
                        foreach (ShopItems shopItems in Helper.shopItemList)
                        {
                            if (shopItems != null && shopItems.shoplabel.Contains("Waffenkammer-"))
                            {
                                db.Save(shopItems);
                            }
                        }
                        //Gangzones
                        GangController.SaveAllGangzones();
                        //Drugplants
                        DrugController.SaveAllDrugPlants();
                    });
                });
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[SaveAll]: " + e.ToString());
            }
        }

        [ServerEvent(Event.PlayerConnected)]
        public void OnPlayerConnect(Player player)
        {
            try
            {
                Helper.SetPlayerArmor(player, 0);
                Helper.SetPlayerHealth(player, 100);
                player.SetData("Player:ConnectName", player.Name);
                player.Dimension = 50;
                player.Name = "Spieler-" + player.Id;
                Helper.SetPlayerPosition(player, new Vector3(226.29927, 7452.966, 22.658815 + 0.1));
                player.Rotation = new Vector3(0, 0, 6.1539865);
                player.TriggerEvent("Client:HideHud");
                player.SetData("Player:AdminDuty", false);
                NAPI.Data.SetEntitySharedData(player, "Player:AdminLogin", 0);
                player.SetOwnSharedData("Player:Job", 0);
                player.SetOwnSharedData("Player:Group", -1);
                player.SetOwnSharedData("Player:GroupRang", -1);
                player.SetOwnSharedData("Player:Faction", 0);
                player.SetOwnSharedData("Player:FactionRang", 0);
                NAPI.Data.SetEntitySharedData(player, "Player:Adminlevel", 0);
                player.SetOwnSharedData("Player:Spawned", false);
                player.SetSharedData("Player:Death", false);
                player.SetOwnSharedData("Player:Money", -1);
                player.SetData("Client:WrongPW", 0);
                player.TriggerEvent("Client:ShowLoading");
                player.TriggerEvent("Client:PlayerFreeze", true);
                player.SetOwnSharedData("Player:DevModus", false);
                player.SetOwnSharedData("Player:Funmodus", false);
                player.SetOwnSharedData("Player:Tester", 0);
                player.SetData<string>("Player:DiscordUpload", "");
                player.SetOwnSharedData("Player:Needs", "-1,-1,0");
                player.SetData("Player:MoebelModus", false);
                player.SetData<bool>("Player:InShop", false);
                player.SetOwnSharedData("Player:Voice", -2);
                player.SetData<string>("Player:InCall", "0");
                player.SetData<string>("Player:LastNumber", "0");
                player.SetData<bool>("Player:SoundEnabled", false);
                player.SetData<bool>("Player:MicEnabled", false);
                player.SetData<int>("Player:PhoneAnim", 0);
                player.SetData<bool>("Player:Spectate", false);
                player.SetSharedData("Player:Adminsettings", "0,0,0");
                player.SetOwnSharedData("Player:InHouse", -1);
                player.SetSharedData("Player:Attachments", "0");
                player.SetSharedData("Player:HealthSync", 100);
                player.SetData<bool>("Player:AcceptCall", false);
                player.ResetData("Player:HealBonus");
                player.SetOwnSharedData("Player:Spawned", false);
                player.SetSharedData("Player:Death", false);
                player.SetSharedData("Player:Tattoos", "n/A");
                player.SetSharedData("Player:AFK", 0);
                player.SetSharedData("Client:OldName", "n/A");
                player.SetSharedData("Client:Condition", "n/A");
                player.SetSharedData("Player:VoiceRangeLocal", 25);
                player.SetOwnSharedData("Player:TalkingOnRadio", false);
                if (Helper.adminSettings != null && Helper.adminSettings.voicerp == 2)
                {
                    player.SetSharedData("Player:LocalVoiceHandyPlayer", -1);
                }
                Helper.SetPlayerHealth(player, 100);
                Helper.SetPlayerArmor(player, 0);
                NAPI.Task.Run(() =>
                {
                    player.TriggerEvent("Client:PlaySound", Settings._Settings.SoundUrl + "login.wav", 1);
                }, delayTime: 85);
                NAPI.Task.Run(() =>
                {
                    if (Helper.whitelist == false || Helper.IsWhitelisted(player.SocialClubId) || player.SocialClubId == 0)
                    {
                        Account.CheckRockstarIdentifier(player);
                    }
                    else
                    {
                        player.Name = player.GetData<string>("Player:ConnectName");
                        player.TriggerEvent("Client:StopSound");
                        AntiCheatController.SetKick(player, $"Du stehst nicht auf der Whitelist - {player.SocialClubId}", "Server", true, false);
                    }
                }, delayTime: 3250);
            }
            catch (MySqlException)
            {
                if (General.Connection.State != ConnectionState.Open)
                {
                    Helper.ConsoleLog("error", "[MYSQL]: Verbindung wurde unerwartet beendet!");
                    General.InitConnection();
                    NAPI.Task.Run(() =>
                    {
                        Account.CheckRockstarIdentifier(player);
                    }, delayTime: 1250);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnPlayerConnect]: " + e.ToString());
            }
        }

        [ServerEvent(Event.PlayerDisconnected)]
        public static void OnPlayerDisconnected(Player player, DisconnectionType type, string reason)
        {
            try
            {
                Account account = Helper.GetAccountData(player);
                Character character = Helper.GetCharacterData(player);
                if (account != null)
                {
                    account.online = 0;
                    Account.SaveAccount(player);
                    TempData tempData = Helper.GetCharacterTempData(player);
                    if (tempData != null)
                    {
                        //Undercover
                        if (tempData.undercover != "")
                        {
                            tempData.undercover = "";
                            character.name = player.GetSharedData<string>("Client:OldName");
                            player.Name = character.name;
                            player.SetSharedData("Client:OldName", "n/A");
                        }
                        //Filmkamera
                        if (player.HasData("Player:Filmkamera"))
                        {
                            player.TriggerEvent("Client:ToggleFilmCamera", player.Id, 0);
                        }
                        //Sonic
                        player.TriggerEvent("Client:Sonic", 1.0f);
                        //Crouching
                        tempData.crouching = false;
                        player.ResetSharedData("Player:Crouching");
                        //Pet
                        if (tempData.pet != null)
                        {
                            player.TriggerEvent("Client:DeletePet");
                            tempData.pet.ResetSharedData("Ped:Name");
                            tempData.pet.Delete();
                            tempData.pet = null;
                            tempData.petTask = 0;
                        }
                        //Drunk
                        if (tempData.drunked == true)
                        {
                            if (tempData.drunked == true)
                            {
                                tempData.drunked = false;
                                tempData.drunk = 0;
                                player.SetSharedData("Player:WalkingStyle", character.walkingstyle);
                                player.TriggerEvent("Client:SetDrunk", false);
                            }
                        }
                        //Funk LocalVoice
                        if(Helper.adminSettings.voicerp == 2)
                        {
                            foreach (var channel in Helper.radioChannels.Keys)
                            {
                                if (Helper.radioChannels[channel].Contains(player))
                                {
                                    Helper.RemoveFromRadioChannel(player, channel);

                                    foreach (var target in Helper.radioChannels[channel])
                                    {
                                        Helper.OnRemove_Voice_Listener(player, target);
                                        Helper.OnRemove_Voice_Listener(target, player);
                                    }
                                }
                            }
                        }
                        //Bizzmusic
                        if (player.HasData("Player:MusicBizz"))
                        {
                            Business bizz = Business.GetBusinessById(player.GetData<int>("Player:MusicBizz"));
                            if (bizz != null)
                            {
                                bizz.musicPlayer = null;
                            }
                            player.ResetData("Player:MusicBizz");
                        }
                        //Ghettoblaster
                        if (tempData.ghettoblaster != null)
                        {
                            tempData.ghettoblaster.Delete();
                            tempData.ghettoblaster = null;
                        }
                        //Radiosystem
                        if (tempData.radio != "")
                        {
                            player.TriggerEvent("Client:Leaveradio", tempData.radio);
                            tempData.radio = "";
                            tempData.radiols = false;
                        }
                        //Drivingschool
                        if (player.GetData<int>("Player:CarQuiz") == 1)
                        {
                            player.TriggerEvent("Client:StopCar");
                            player.ResetData("Player:CarQuiz");
                        }
                        //Dealership
                        if (tempData.dealerShip != null && player.HasData("Player:TestDrive"))
                        {
                            player.ResetData("Player:TestDrive");
                            player.SetData<int>("Player:TestDriveCD", Helper.UnixTimestamp() + (60 * 5));
                            tempData.dealerShip.Delete();
                            tempData.dealerShip = null;
                        }
                        //Furniture
                        if (tempData.editfurniture == true && character != null)
                        {
                            House house = null;
                            if (tempData.lasthouse == 0)
                            {
                                if (character.inhouse == -1)
                                {
                                    house = House.GetClosestHouse(player, 25.5f);
                                }
                                else
                                {
                                    house = House.GetHouseById(character.inhouse);
                                }
                            }
                            else
                            {
                                house = House.GetHouseById(tempData.lasthouse);
                            }
                            FurnitureSetHouse furniture = null;
                            if (house != null)
                            {
                                player.TriggerEvent("Client:ShowInfobox", "Pfeiltasten -> Position/Rotation ändern", "Shift/ALT - Position/Rotation & X/Y switchen", "B -> Möbelstück bewegen/aufbauen", "Backspace -> Abbrechen", "Möbelaufbau", "Möbelstück: n/A", "null", 0);
                                player.TriggerEvent("Client:ShowMoebel", null, null, null, 0, 0);
                                player.TriggerEvent("Client:UpdateHud3");
                                if (tempData.furnitureNew == false)
                                {
                                    furniture = Furniture.GetFurnitureById(tempData.furnitureID, house.id);
                                    if (furniture != null)
                                    {
                                        furniture.objectHandle = NAPI.Object.CreateObject(Convert.ToInt32(furniture.hash), tempData.furnitureOldPosition, tempData.furnitureOldRotation, 255, player.Dimension);
                                        if (tempData.furnitureObject != null)
                                        {
                                            tempData.furnitureObject.Delete();
                                            tempData.furnitureObject = null;
                                        }
                                    }
                                }
                                else
                                {
                                    if (tempData.furnitureObject != null)
                                    {
                                        tempData.furnitureObject.Delete();
                                        tempData.furnitureObject = null;
                                    }
                                }
                            }
                        }
                        if (tempData.furniturePosition != null && Helper.IsASecruity(player) > -1)
                        {
                            tempData.furniturePosition = null;
                            tempData.tempValue = 0;
                            tempData.jobstatus = 0;
                        }
                        if (player.HasData("Player:MoneyInfos"))
                        {
                            string[] moneyArray = new string[3];
                            moneyArray = player.GetData<string>("Player:MoneyInfos").Split(",");
                            if (moneyArray[0].Length > 0 && moneyArray[1].Length > 0)
                            {
                                SmartphoneController.OnSmartphoneMessage(null, Helper.UnixTimestamp(), moneyArray[0], "01897337", $"Der Geldtransport in Höhe von {player.GetData<int>("Player:Money")}$ wurde abgebrochen, das Geld wurde zurückgebracht!");
                                Business bizz = Business.GetBusinessById(Convert.ToInt32(moneyArray[2]));
                                if (bizz != null)
                                {
                                    bizz.cash += player.GetData<int>("Player:Money");
                                }
                                Helper.AddRemoveAttachments(player, "moneyBag", false);
                                player.ResetData("Player:Money");
                                player.ResetData("Player:MoneyInfos");
                            }
                        }
                        if (player.HasData("Player:Money"))
                        {
                            player.ResetData("Player:Money");
                            Helper.AddRemoveAttachments(player, "moneyBag", false);
                        }
                        if (player.HasData("Player:GarbageBag"))
                        {
                            Helper.SendNotificationWithoutButton(player, "Schade, wenn du mir wieder helfen willst, sag einfach bescheid!", "success", "top-left", 4150);
                            Helper.AddRemoveAttachments(player, "garbageBag", false);
                            player.TriggerEvent("Client:StopBeachGarbage");
                            player.ResetData("Player:GarbageBag");
                        }
                        if (tempData.followed == true)
                        {
                            Player getPlayer = Helper.GetClosestPlayer(player, 2.5f);
                            if (getPlayer != null && getPlayer != player)
                            {
                                TempData tempData2 = Helper.GetCharacterTempData(getPlayer);
                                Character character2 = Helper.GetCharacterData(getPlayer);
                                Helper.SendNotificationWithoutButton(getPlayer, "Du wirst nicht mehr gezogen/getragen!", "success");
                                player.SetSharedData("Player:FollowStatus", 0);
                                tempData.followed = false;
                                Helper.OnStopAnimation(getPlayer);
                                NAPI.Task.Run(() =>
                                {
                                    player.ResetSharedData("Player:Follow");
                                    player.ResetSharedData("Player:FollowStatus");
                                    Vector3 newPosition = Helper.GetPositionInFrontOfPlayer(player, 1.15f);
                                    Helper.SetPlayerPosition(getPlayer, newPosition);
                                    getPlayer.Heading = player.Heading + 90;
                                    tempData2.follow = false;
                                    getPlayer.TriggerEvent("Client:PlayerFreeze", false);
                                    if (tempData2.cuffed > 0)
                                    {
                                        getPlayer.SetSharedData("Player:AnimData", $"mp_arresting%idle%{49}");
                                    }
                                    else if (character2.death == true)
                                    {
                                        Helper.OnPlayDeathAnim(getPlayer);
                                    }
                                    else
                                    {
                                        Helper.OnStopAnimation(getPlayer);
                                    }
                                }, delayTime: 115);
                            }
                        }
                        if (tempData.follow == true)
                        {
                            Player getPlayer = Helper.GetClosestPlayer(player, 2.5f);
                            if (getPlayer != null && getPlayer != player)
                            {
                                TempData tempData2 = Helper.GetCharacterTempData(getPlayer);
                                Character character2 = Helper.GetCharacterData(getPlayer);
                                Helper.SendNotificationWithoutButton(getPlayer, "Du ziehst keinen mehr vor dir her/trägst keinen mehr!", "success");
                                getPlayer.SetSharedData("Player:FollowStatus", 0);
                                tempData2.followed = false;
                                Helper.OnStopAnimation(getPlayer);
                                NAPI.Task.Run(() =>
                                {
                                    getPlayer.ResetSharedData("Player:Follow");
                                    getPlayer.ResetSharedData("Player:FollowStatus");
                                    tempData.follow = false;
                                    if (tempData2.cuffed > 0)
                                    {
                                        getPlayer.SetSharedData("Player:AnimData", $"mp_arresting%idle%{49}");
                                    }
                                    else if (character2.death == true)
                                    {
                                        Helper.OnPlayDeathAnim(getPlayer);
                                    }
                                    else
                                    {
                                        Helper.OnStopAnimation(getPlayer);
                                    }
                                }, delayTime: 115);
                            }
                        }
                        if (tempData.jobColshape != null && tempData.order != null && Helper.IsASpediteur(player) > -1)
                        {
                            if (tempData.jobColshape != null)
                            {
                                player.TriggerEvent("Client:DeleteMarker");
                                tempData.jobColshape.Delete();
                                tempData.jobColshape = null;
                                tempData.jobstatus = 0;
                                tempData.order = null;
                                player.TriggerEvent("Client:RemoveWaypoint");
                            }
                        }
                        if (tempData.jobColshape != null && tempData.order2 != null && Helper.IsATaxiDriver(player) > -1)
                        {
                            if (tempData.jobColshape != null)
                            {
                                player.TriggerEvent("Client:DeletePed");
                                tempData.jobColshape.Delete();
                                tempData.jobColshape = null;
                                tempData.jobstatus = 0;
                                tempData.order2 = null;
                                player.ResetData("Player:BusTime");
                                player.TriggerEvent("Client:RemoveWaypoint");
                            }
                        }
                        if (tempData.showinventory == true)
                        {
                            tempData.inventoysetting = "nothing";
                            tempData.showinventory = false;
                            player.TriggerEvent("Client:ShowInventory", NAPI.Util.ToJson(tempData.itemlist), ItemsController.MAX_INVENTORY_PLAYER, false, null, null, null);
                        }
                        if (tempData.interiorswitch == true)
                        {
                            tempData.interiorswitch = false;
                            tempData.lasthouse = 0;
                            tempData.counter = 0;
                            player.TriggerEvent("SwitchHouseInterior", "null", "null", "null", "null", "null", "null", "null", 0);
                        }
                        if (tempData.adminvehicle != null)
                        {
                            tempData.adminvehicle.Delete();
                            tempData.adminvehicle = null;
                        }
                        if (tempData.rentVehicle != null)
                        {
                            tempData.rentVehicle.Delete();
                            tempData.rentVehicle = null;
                        }
                        if (tempData.jobVehicle2 != null)
                        {
                            if (tempData.jobVehicle2.HasSharedData("Vehicle:Text3D"))
                            {
                                tempData.jobVehicle2.ResetSharedData("Vehicle:Text3D");
                            }
                            tempData.jobVehicle2.Delete();
                            tempData.jobVehicle2 = null;
                        }
                        if (tempData.jobVehicle != null)
                        {
                            if (tempData.jobVehicle.HasSharedData("Vehicle:Text3D"))
                            {
                                tempData.jobVehicle.ResetSharedData("Vehicle:Text3D");
                            }
                            tempData.jobVehicle.Delete();
                            tempData.jobVehicle = null;
                        }
                        if (tempData.inCall == true)
                        {
                            SmartphoneController.OnEndCall(player);
                        }
                        //Ammuquiz
                        if (player.HasData("Player:AmmuQuiz"))
                        {
                            if (player.GetData<int>("Player:AmmuQuiz") == 1)
                            {
                                player.TriggerEvent("Client:ShowHud");
                                player.TriggerEvent("Client:StopAmmuQuiz");
                                player.SetData<int>("Player:AmmuQuiz", 0);
                            }
                            else if (player.GetData<int>("Player:AmmuQuiz") == 2)
                            {
                                NAPI.Player.SetPlayerWeaponAmmo(player, WeaponHash.Pistol, 0);
                                NAPI.Player.RemovePlayerWeapon(player, WeaponHash.Pistol);
                                if (tempData.jobColshape != null)
                                {
                                    tempData.jobColshape = null;
                                }
                                player.SetData<int>("Player:AmmuQuiz", 0);
                            }
                            else if (player.GetData<int>("Player:AmmuQuiz") == 4)
                            {
                                if (tempData.jobColshape != null)
                                {
                                    tempData.jobColshape = null;
                                }
                                player.SetData<int>("Player:AmmuQuiz", 0);
                            }
                        }
                        if (tempData.showSmartphone == true)
                        {
                            Helper.AddRemoveAttachments(player, "smartphone", false);
                            tempData.showSmartphone = false;
                        }
                        tempData.adminduty = false;
                        //Save smartphones
                        if (tempData.itemlist != null && tempData.itemlist.Count > 0)
                        {
                            foreach (Items item in tempData.itemlist)
                            {
                                if (item != null && item.description.Contains("Smartphone") && item.props.Length > 3)
                                {
                                    Smartphone smartphone = SmartphoneController.GetSmartPhoneByNumber(item.props);
                                    if (smartphone != null)
                                    {
                                        SmartphoneController.OnSaveSmartphone(player, smartphone.phoneprops, smartphone.contacts, "0189" + smartphone.phonenumber);
                                    }
                                }
                            }
                        }
                    }
                    if (character != null)
                    {
                        //Group phone
                        if (character.mygroup != -1)
                        {
                            Groups group = GroupsController.GetGroupById(character.mygroup);
                            if (group != null && group.number != "" && group.numbername == character.name)
                            {
                                group.number = "";
                                group.numbername = "";
                            }
                        }
                        //Faction phone
                        if (character.faction > 0)
                        {
                            FactionsModel faction = FactionController.GetFactionById(character.faction);
                            if (faction != null && faction.number != "" && faction.numbername == character.name)
                            {
                                faction.number = "";
                                faction.numbername = "";
                            }
                        }
                        //Fuelsystem
                        if (player.HasData("Player:FuelCooldown") && player.GetData<int>("Player:FuelCooldown") > 0)
                        {
                            if (Helper.UnixTimestamp() > player.GetData<int>("Player:FuelCooldown"))
                            {
                                character.abusemoney += player.GetData<int>("Player:FuelPrice");
                                player.SetData<int>("Player:FuelPrice", 0);
                                player.SetData<int>("Player:FuelCooldown", 0);
                                Business bizz = Business.GetBusinessById(player.GetData<int>("Player:FuelBizz"));
                                if (bizz != null)
                                {
                                    string data = "Gemeldeter Tankdiebstahl vom Fahrzeug mit dem Kennzeichen: " + player.GetData<int>("Player:FuelCooldown") + ", beim Business: " + bizz.name + "!";
                                    MDCController.CreateFahndung(data);
                                }
                                player.SetData<int>("Player:FuelBizz", 0);
                                player.SetData<string>("Player:NumberPlate", "n/A");
                            }
                        }
                        //Save character
                        character.online = 0;
                        CharacterController.SaveCharacter(player);
                    }
                }
                //Busdriver
                if (player.HasData("Player:BusRoute"))
                {
                    if (Events.busCount > 0)
                    {
                        Events.busCount--;
                    }
                    Events.busLabel.Text = $"~b~Busexperte JinJong\n~w~Benutze Taste ~b~[F]~w~ um mit Ihm zu kummunizieren\nZurzeit sind {Events.busCount} Busfahrer auf einer Route unterwegs!";
                    player.ResetData("Player:BusRoute");
                    player.ResetData("Player:BusStation");
                    player.ResetData("Player:BusTime");
                }
                //Garbage
                if (player.HasData("Player:GarbageGetPlayer") && player.GetData<Player>("Player:GarbageGetPlayer") != null)
                {
                    Player garbagePlayer = player.GetData<Player>("Player:GarbageGetPlayer");
                    garbagePlayer.ResetData("Player:GarbageRoute");
                    garbagePlayer.ResetData("Player:GarbageStation");
                    garbagePlayer.ResetData("Player:GarbageTime");
                    garbagePlayer.TriggerEvent("Client:RemoveGarbageDriverCP");
                    if (player.HasData("Player:GarbagePlayer2"))
                    {
                        player.ResetData("Player:GarbagePlayer2");
                    }
                    if (garbagePlayer.HasData("Player:GarbagePlayer2"))
                    {
                        garbagePlayer.ResetData("Player:GarbagePlayer2");
                    }
                    player.ResetData("Player:GarbageRoute");
                    player.ResetData("Player:GarbageStation");
                    player.ResetData("Player:GarbageTime");
                    player.TriggerEvent("Client:RemoveGarbageDriverCP");
                    player.ResetData("Player:GarbageGetPlayer");
                    garbagePlayer.ResetData("Player:GarbageGetPlayer");
                    Helper.SendNotificationWithoutButton(garbagePlayer, $"Müllroute beendet, dein Partner hat den Server verlassen!", "success", "top-end", 3250);
                }
                if (player.HasData("Player:GarbageRoute"))
                {
                    if (player.HasData("Player:GarbagePlayer2"))
                    {
                        player.ResetData("Player:GarbagePlayer2");
                    }
                    player.ResetData("Player:GarbageRoute");
                    player.ResetData("Player:GarbageStation");
                    player.ResetData("Player:GarbageTime");
                    player.TriggerEvent("Client:RemoveGarbageDriverCP");
                    player.ResetData("Player:GarbageGetPlayer");
                }
                //Text3D
                if (player.HasSharedData("Vehicle:Text3D"))
                {
                    player.ResetSharedData("Vehicle:Text3D");
                }
                //Farmer
                if (player.HasData("Player:FarmerRoute"))
                {
                    player.ResetData("Player:FarmerRoute");
                }
                //Taxijobs
                if (player.HasData("Player:TaxiJob"))
                {
                    player.ResetData("Player:TaxiJob");
                    if (Helper.taxiJobs.Count > 0)
                    {
                        foreach (Taxi taxi in Helper.taxiJobs.ToList())
                        {
                            if (taxi.id == character.id || taxi.done == character.name)
                            {
                                Helper.taxiJobs.Remove(taxi);
                                break;
                            }
                        }
                    }
                }
                //Data
                player.ResetData("Player:HealBonus");
                player.ResetData("Player:Sonic");
                player.SetData<bool>("Player:Fire", false);
                player.ResetData("Player:Pickaxe");
                player.ResetData("Player:HorseStick");
                player.ResetData("Player:GlowStick");
                player.ResetData("Player:RunStock");
                player.ResetData("Player:Mikrofon");
                player.ResetData("Player:Filmkamera");
                player.ResetData("Player:PlayCustomAnimation");
                player.SetData("Player:AdminDuty", false);
                player.TriggerEvent("Client:StopSound");
                player.SetData("Client:WrongPW", 0);
                player.SetOwnSharedData("Player:Spawned", false);
                player.SetSharedData("Player:Death", false);
                player.SetOwnSharedData("Player:Money", -1);
                NAPI.Data.SetEntitySharedData(player, "Player:AdminLogin", 0);
                player.SetOwnSharedData("Player:Job", 0);
                player.SetOwnSharedData("Player:Group", -1);
                player.SetOwnSharedData("Player:GroupRang", -1);
                player.SetOwnSharedData("Player:Faction", 0);
                player.SetOwnSharedData("Player:FactionRang", 0);
                NAPI.Data.SetEntitySharedData(player, "Player:AdminLevel", 0);
                player.SetOwnSharedData("Player:DevModus", false);
                player.SetOwnSharedData("Player:Funmodus", false);
                player.SetOwnSharedData("Player:Tester", 0);
                player.SetData<string>("Player:DiscordUpload", "");
                player.SetData<bool>("Player:InShop", false);
                player.SetOwnSharedData("Player:Voice", -2);
                player.SetData<string>("Player:InCall", "0");
                player.SetData<string>("Player:LastNumber", "0");
                player.SetData<bool>("Player:SoundEnabled", true);
                player.SetData<bool>("Player:MicEnabled", true);
                player.SetData<int>("Player:PhoneAnim", 0);
                player.SetData<bool>("Player:Spectate", false);
                player.SetOwnSharedData("Player:InHouse", -1);
                player.SetSharedData("Player:Attachments", "0");
                player.SetSharedData("Player:HealthSync", 100);
                player.SetSharedData("Player:Tattoos", "n/A");
                player.SetSharedData("Player:LocalVoiceHandyPlayer", -1);
                player.SetSharedData("Player:AFK", 0);
                player.SetSharedData("Client:OldName", "n/A");
                player.SetSharedData("Client:Condition", "n/A");
                player.SetSharedData("Player:VoiceRangeLocal", 25);
                player.SetOwnSharedData("Player:TalkingOnRadio", false);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnPlayerDisconnected]: " + e.ToString());
            }
        }

        [ServerEvent(Event.PlayerSpawn)]
        public static void OnPlayerSpawn(Player player)
        {
            try
            {
                Account account = Helper.GetAccountData(player);
                TempData tempData = Helper.GetCharacterTempData(player);
                Character character = Helper.GetCharacterData(player);
                if (account == null || tempData == null) return;
                if (tempData.adminduty == true)
                {
                    player.SetSharedData("Player:Adminsettings", "1,0,0");
                    Helper.SetPlayerHealth(player, 100);
                }
                if (character.death == true)
                {
                    Helper.SetPlayerHealth(player, 1);
                    player.SetSharedData("Player:Adminsettings", "1,0,0");
                    NAPI.Task.Run(() =>
                    {
                        Helper.OnPlayDeathAnim(player);
                    }, delayTime: 5);
                    return;
                }
                //Prison
                if (account.prison > 0)
                {
                    Helper.SetPlayerPosition(player, new Vector3(-1999.6619, 1113.5583, -27.363804));
                    player.Dimension = (uint)(player.Id + 500);
                    Helper.SendNotificationWithoutButton(player, $"Du musst noch {account.prison} Checkpoints ablaufen!", "info", "top-left", 3550);
                    return;
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnPlayerSpawn]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:SetDeath")]
        [ServerEvent(Event.PlayerDeath)]
        public static void OnPlayerDeath(Player player, Player killer, uint reason)
        {
            try
            {
                TempData tempData = Helper.GetCharacterTempData(player);
                Character character = Helper.GetCharacterData(player);
                Account account = Helper.GetAccountData(player);
                if (character != null && character.death == true) return;
                Account accountkiller = null;
                TempData tempkiller = null;
                if (killer != null && killer != player)
                {
                    accountkiller = Helper.GetAccountData(killer);
                    tempkiller = Helper.GetCharacterTempData(killer);
                }
                if (killer != null && accountkiller != null && killer != player)
                {
                    if (tempkiller.killsintime == 0 || Helper.UnixTimestamp() > tempkiller.killsintime)
                    {
                        tempkiller.killsintime = Helper.UnixTimestamp() + (15);
                        tempkiller.kills = 0;
                    }
                    tempkiller.kills++;
                    Helper.SendAdminMessage2($"{accountkiller.name}({killer.Name}) hat {account.name}({player.Name}) umgebracht!", 1, true);
                    Helper.CreateAdminLog($"killlog", $" {accountkiller.name}({killer.Name}) hat {account.name}({player.Name}) umgebracht!");
                    accountkiller.kills++;
                    if (tempkiller.kills >= 5)
                    {
                        AntiCheatController.OnCallAntiCheat(player, $"Masskill Cheat", $"{accountkiller.kills}/Kills", true);
                    }
                }
                if (player.IsInVehicle)
                {
                    player.WarpOutOfVehicle();
                }
                if (account != null)
                {
                    account.deaths++;
                }
                if (tempData != null)
                {
                    //TalkOnRadio
                    player.SetOwnSharedData("Player:TalkingOnRadio", false);
                    //Condition
                    player.SetSharedData("Client:Condition", "n/A");
                    //Pet
                    if (tempData.pet != null)
                    {
                        player.TriggerEvent("Client:DeletePet");
                        NAPI.Task.Run(() =>
                        {
                            tempData.pet.ResetSharedData("Ped:Name");
                            tempData.pet.Delete();
                            tempData.pet = null;
                            tempData.petTask = 0;
                        }, delayTime: 515);
                    }
                    //Filmkamera
                    if (player.HasData("Player:Filmkamera"))
                    {
                        player.TriggerEvent("Client:ToggleFilmCamera", player.Id, 0);
                    }
                    //Gangzone
                    if (tempData.ingangzone != -1 && character.mygroup != -1 && killer != null && killer.Exists)
                    {
                        Groups group = GroupsController.GetGroupById(character.mygroup);
                        if (group != null && group.status == 0)
                        {
                            Character character2 = Helper.GetCharacterData(killer);
                            if (character2 != null)
                            {
                                Groups group2 = GroupsController.GetGroupById(character2.mygroup);
                                if (group2 != null && group.id != group2.id && group2.status == 0)
                                {
                                    GangController.UpdateGangZoneValues(tempData.ingangzone, group.id, 5);
                                    GangController.UpdateGangZoneValues(tempData.ingangzone, group2.id, -5);
                                }
                            }
                        }
                    }
                    //Deathsystem
                    if (tempData.adminduty == false)
                    {
                        player.SetSharedData("Player:Death", true);
                        character.death = true;
                        if (Helper.GetFactionCountDuty(2) <= 1)
                        {
                            player.TriggerEvent("Client:SetDeath", 300);
                        }
                        else
                        {
                            player.TriggerEvent("Client:SetDeath", 300 * 3);
                        }
                        player.SetSharedData("Player:Adminsettings", "1,0,0");
                        player.TriggerEvent("Client:LastDamage");
                        Helper.SetPlayerHealth(player, 1);
                        if (reason != 7)
                        {
                            Helper.SetPlayerPosition(player, player.Position);
                        }
                    }
                    else
                    {
                        if (character.death == false)
                        {
                            player.SetSharedData("Player:Death", false);
                            character.death = false;
                            Helper.SetPlayerPosition(player, player.Position);
                        }
                    }
                    //Drunk
                    if (tempData.drunked == true)
                    {
                        if (tempData.drunked == true)
                        {
                            tempData.drunked = false;
                            tempData.drunk = 0;
                            player.SetSharedData("Player:WalkingStyle", character.walkingstyle);
                            player.TriggerEvent("Client:SetDrunk", false);
                        }
                    }
                    //Bizzmusic
                    if (player.HasData("Player:MusicBizz"))
                    {
                        Business bizz = Business.GetBusinessById(player.GetData<int>("Player:MusicBizz"));
                        if (bizz != null)
                        {
                            bizz.musicPlayer = null;
                        }
                        player.ResetData("Player:MusicBizz");
                    }
                    //Radiosystem
                    if (tempData.radio != "")
                    {
                        player.TriggerEvent("Client:Leaveradio", tempData.radio);
                        tempData.radio = "";
                        tempData.radiols = false;
                    }
                    //Drivingschool
                    if (player.GetData<int>("Player:CarQuiz") == 1)
                    {
                        if (tempData.jobVehicle != null)
                        {
                            if (tempData.jobVehicle.HasSharedData("Vehicle:Text3D"))
                            {
                                tempData.jobVehicle.ResetSharedData("Vehicle:Text3D");
                            }
                            tempData.jobVehicle.Delete();
                            tempData.jobVehicle = null;
                        }
                        player.TriggerEvent("Client:StopCar");
                        player.ResetData("Player:CarQuiz");
                    }
                    //Testdrive
                    if (tempData.dealerShip != null && player.HasData("Player:TestDrive"))
                    {
                        player.ResetData("Player:TestDrive");
                        player.SetData<int>("Player:TestDriveCD", Helper.UnixTimestamp() + (60 * 5));
                        tempData.dealerShip.Delete();
                        tempData.dealerShip = null;
                    }
                    if (tempData.editfurniture == true && character != null)
                    {
                        House house = null;
                        if (tempData.lasthouse == 0)
                        {
                            if (character.inhouse == -1)
                            {
                                house = House.GetClosestHouse(player, 25.5f);
                            }
                            else
                            {
                                house = House.GetHouseById(character.inhouse);
                            }
                        }
                        else
                        {
                            house = House.GetHouseById(tempData.lasthouse);
                        }
                        FurnitureSetHouse furniture = null;
                        if (house != null)
                        {
                            player.TriggerEvent("Client:ShowInfobox", "Pfeiltasten -> Position/Rotation ändern", "Shift/ALT - Position/Rotation & X/Y switchen", "B -> Möbelstück bewegen/aufbauen", "Backspace -> Abbrechen", "Möbelaufbau", "Möbelstück: n/A", "null", 0);
                            player.TriggerEvent("Client:ShowMoebel", null, null, null, 0, 0);
                            player.TriggerEvent("Client:UpdateHud3");
                            if (tempData.furnitureNew == false)
                            {
                                furniture = Furniture.GetFurnitureById(tempData.furnitureID, house.id);
                                if (furniture != null)
                                {
                                    furniture.objectHandle = NAPI.Object.CreateObject(Convert.ToInt32(furniture.hash), tempData.furnitureOldPosition, tempData.furnitureOldRotation, 255, player.Dimension);
                                    if (tempData.furnitureObject != null)
                                    {
                                        tempData.furnitureObject.Delete();
                                        tempData.furnitureObject = null;
                                    }
                                }
                            }
                            else
                            {
                                if (tempData.furnitureObject != null)
                                {
                                    tempData.furnitureObject.Delete();
                                    tempData.furnitureObject = null;
                                }
                            }
                        }
                    }
                    if (tempData.furniturePosition != null && Helper.IsASecruity(player) > -1)
                    {
                        tempData.furniturePosition = null;
                        tempData.tempValue = 0;
                        tempData.jobstatus = 0;
                    }
                    if (player.HasData("Player:MoneyInfos"))
                    {
                        string[] moneyArray = new string[3];
                        moneyArray = player.GetData<string>("Player:MoneyInfos").Split(",");
                        if (moneyArray[0].Length > 0 && moneyArray[1].Length > 0)
                        {
                            SmartphoneController.OnSmartphoneMessage(null, Helper.UnixTimestamp(), moneyArray[0], "01897337", $"Der Geldtransport in Höhe von {player.GetData<int>("Player:Money")}$ wurde abgebrochen, das Geld wurde zurückgebracht!");
                            Business bizz = Business.GetBusinessById(Convert.ToInt32(moneyArray[2]));
                            if (bizz != null)
                            {
                                bizz.cash += player.GetData<int>("Player:Money");
                            }
                            Helper.AddRemoveAttachments(player, "moneyBag", false);
                            player.ResetData("Player:Money");
                            player.ResetData("Player:MoneyInfos");
                        }
                    }
                    if (player.HasData("Player:Money"))
                    {
                        player.ResetData("Player:Money");
                        Helper.AddRemoveAttachments(player, "moneyBag", false);
                    }
                    if (player.HasData("Player:GarbageBag"))
                    {
                        Helper.SendNotificationWithoutButton(player, "Schade, wenn du mir wieder helfen willst, sag einfach bescheid!", "success", "top-left", 4150);
                        Helper.AddRemoveAttachments(player, "garbageBag", false);
                        player.TriggerEvent("Client:StopBeachGarbage");
                        player.ResetData("Player:GarbageBag");
                    }
                    if (tempData.jobColshape != null && tempData.order != null && Helper.IsASpediteur(player) > -1)
                    {
                        if (tempData.jobColshape != null)
                        {
                            player.TriggerEvent("Client:DeleteMarker");
                            tempData.jobColshape.Delete();
                            tempData.jobColshape = null;
                            tempData.jobstatus = 0;
                            tempData.order = null;
                            player.TriggerEvent("Client:RemoveWaypoint");
                        }
                    }
                    if (tempData.jobColshape != null && tempData.order2 != null && Helper.IsATaxiDriver(player) > -1)
                    {
                        if (tempData.jobColshape != null)
                        {
                            player.TriggerEvent("Client:DeletePed");
                            tempData.jobColshape.Delete();
                            tempData.jobColshape = null;
                            tempData.jobstatus = 0;
                            tempData.order2 = null;
                            player.ResetData("Player:BusTime");
                            player.TriggerEvent("Client:RemoveWaypoint");
                        }
                    }
                    if (tempData.showinventory == true)
                    {
                        tempData.inventoysetting = "nothing";
                        tempData.showinventory = false;
                        player.TriggerEvent("Client:ShowInventory", NAPI.Util.ToJson(tempData.itemlist), ItemsController.MAX_INVENTORY_PLAYER, false, null, null, null);
                    }
                    if (tempData.interiorswitch == true)
                    {
                        tempData.interiorswitch = false;
                        tempData.lasthouse = 0;
                        tempData.counter = 0;
                        player.TriggerEvent("Client:SwitchHouseInterior", "null", "null", "null", "null", "Interiorausbau", "Preis: 0$", "Interior-ID: 0", "Hausnummer: 0", "null", 0);
                    }
                    if (tempData.adminvehicle != null)
                    {
                        tempData.adminvehicle.Delete();
                        tempData.adminvehicle = null;
                    }
                    if (tempData.jobVehicle2 != null)
                    {
                        if (tempData.jobVehicle2.HasSharedData("Vehicle:Text3D"))
                        {
                            tempData.jobVehicle2.ResetSharedData("Vehicle:Text3D");
                        }
                        tempData.jobVehicle2.Delete();
                        tempData.jobVehicle2 = null;
                        player.ResetData("Player:Drohne");
                    }
                    if (tempData.inCall == true)
                    {
                        SmartphoneController.OnEndCall(player);
                    }
                    tempData.inventoysetting = "nothing";
                    if (tempData.followed == true)
                    {
                        Player getPlayer = Helper.GetClosestPlayer(player, 2.5f);
                        if (getPlayer != null && getPlayer != player)
                        {
                            TempData tempData2 = Helper.GetCharacterTempData(getPlayer);
                            Character character2 = Helper.GetCharacterData(getPlayer);
                            Helper.SendNotificationWithoutButton(getPlayer, "Du wirst nicht mehr gezogen/getragen!", "success");
                            player.SetSharedData("Player:FollowStatus", 0);
                            tempData.followed = false;
                            Helper.OnStopAnimation(getPlayer);
                            NAPI.Task.Run(() =>
                            {
                                player.ResetSharedData("Player:Follow");
                                player.ResetSharedData("Player:FollowStatus");
                                Vector3 newPosition = Helper.GetPositionInFrontOfPlayer(player, 1.15f);
                                Helper.SetPlayerPosition(getPlayer, newPosition);
                                getPlayer.Heading = player.Heading + 90;
                                tempData2.follow = false;
                                getPlayer.TriggerEvent("Client:PlayerFreeze", false);
                                if (tempData2.cuffed > 0)
                                {
                                    getPlayer.SetSharedData("Player:AnimData", $"mp_arresting%idle%{49}");
                                }
                                else if (character2.death == true)
                                {
                                    Helper.OnPlayDeathAnim(getPlayer);
                                }
                                else
                                {
                                    Helper.OnStopAnimation(getPlayer);
                                }
                            }, delayTime: 115);
                        }
                    }
                    if (tempData.follow == true)
                    {
                        Player getPlayer = Helper.GetClosestPlayer(player, 2.5f);
                        if (getPlayer != null && getPlayer != player)
                        {
                            Character character2 = Helper.GetCharacterData(getPlayer);
                            TempData tempData2 = Helper.GetCharacterTempData(getPlayer);
                            Helper.SendNotificationWithoutButton(getPlayer, "Du ziehst keinen mehr vor dir her/trägst keinen mehr!", "success");
                            getPlayer.SetSharedData("Player:FollowStatus", 0);
                            tempData2.followed = false;
                            Helper.OnStopAnimation(getPlayer);
                            NAPI.Task.Run(() =>
                            {
                                getPlayer.ResetSharedData("Player:Follow");
                                getPlayer.ResetSharedData("Player:FollowStatus");
                                tempData.follow = false;
                                if (tempData2.cuffed > 0)
                                {
                                    getPlayer.SetSharedData("Player:AnimData", $"mp_arresting%idle%{49}");
                                }
                                else if (character2.death == true)
                                {
                                    Helper.OnPlayDeathAnim(getPlayer);
                                }
                                else
                                {
                                    Helper.OnStopAnimation(getPlayer);
                                }
                            }, delayTime: 115);
                        }
                    }
                    //Weapon drop
                    if (tempData.adminduty == false)
                    {
                        if (tempData.itemlist.Count > 0)
                        {
                            Random rand = new Random();
                            NAPI.Task.Run(() =>
                            {
                                foreach (Items iteminlist in tempData.itemlist.ToList())
                                {
                                    if (iteminlist != null && iteminlist.type == 5 && !iteminlist.description.ToLower().Contains("schutzweste"))
                                    {
                                        string[] weaponArray = new string[7];
                                        weaponArray = iteminlist.props.Split(",");
                                        if (weaponArray[1] == "1")
                                        {
                                            ItemsGlobal itemglobal = new ItemsGlobal();
                                            itemglobal.itemid = ItemsController.GetFreeItemIDGlobal();
                                            itemglobal.description = iteminlist.description;
                                            itemglobal.ownerid = -1;
                                            itemglobal.owneridentifier = "Ground";
                                            itemglobal.lastupdate = Helper.UnixTimestamp();
                                            itemglobal.hash = iteminlist.hash;
                                            itemglobal.amount = iteminlist.amount;
                                            itemglobal.weight = iteminlist.weight;
                                            itemglobal.type = iteminlist.type;
                                            string[] weaponArray2 = new string[7];
                                            weaponArray2 = iteminlist.props.Split(",");
                                            if (weaponArray[1] == "1")
                                            {
                                                iteminlist.props = $"{NAPI.Player.GetPlayerWeaponAmmo(player, (WeaponHash)WeaponController.GetWeaponHashFromName(iteminlist.description))},0,{weaponArray2[2]},{weaponArray2[3]},{weaponArray2[4]},{weaponArray2[5]},{weaponArray2[6]}";
                                                iteminlist.props.Trim();
                                            }
                                            NAPI.Player.SetPlayerWeaponAmmo(player, (WeaponHash)WeaponController.GetWeaponHashFromName(iteminlist.description), 0);
                                            NAPI.Player.RemovePlayerWeapon(player, (WeaponHash)WeaponController.GetWeaponHashFromName(iteminlist.description));
                                            tempData.weaponTints.Remove($"{WeaponController.GetWeaponHash2FromName(iteminlist.description).ToLower()}");
                                            player.SetSharedData("Player:WeaponTint", -1);
                                            tempData.weaponComponents.Remove($"{WeaponController.GetWeaponHash2FromName(iteminlist.description).ToLower()}");
                                            player.SetSharedData("Player:WeaponComponents", "-1");
                                            itemglobal.props = iteminlist.props;
                                            itemglobal.posx = (float)(player.Position.X + (rand.NextDouble() * 2) + 0.45);
                                            itemglobal.posy = (float)(player.Position.Y + (rand.NextDouble() * 2) + 0.45);
                                            itemglobal.posz = player.Position.Z - 0.92f;
                                            itemglobal.dimension = player.Dimension;
                                            itemglobal.objectHandle = NAPI.Object.CreateObject((uint)Convert.ToInt64(itemglobal.hash), new Vector3(itemglobal.posx, itemglobal.posy, itemglobal.posz), new Vector3(0.0f, 0.0f, 0.0f), 255, itemglobal.dimension);
                                            itemglobal.textHandle = NAPI.TextLabel.CreateTextLabel("" + itemglobal.amount + "x " + itemglobal.description + "", new Vector3(itemglobal.posx, itemglobal.posy, itemglobal.posz + 0.3f), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, itemglobal.dimension);
                                            ItemsController.itemListGlobal.Add(itemglobal);
                                            iteminlist.amount = 0;
                                            ItemsController.RemoveItem(player, iteminlist.itemid);
                                        }
                                    }
                                }
                                ItemsController.UpdateInventory(player);
                            }, delayTime: 55);
                        }
                        NAPI.Player.RemoveAllPlayerWeapons(player);
                    }
                }
                //Ammuquiz
                if (player.HasData("Player:AmmuQuiz"))
                {
                    if (player.GetData<int>("Player:AmmuQuiz") == 1)
                    {
                        player.TriggerEvent("Client:ShowHud");
                        player.TriggerEvent("Client:StopAmmuQuiz");
                        player.SetData<int>("Player:AmmuQuiz", 0);
                        player.TriggerEvent("Client:HideCursor");
                    }
                    else if (player.GetData<int>("Player:AmmuQuiz") == 2)
                    {
                        NAPI.Player.SetPlayerWeaponAmmo(player, WeaponHash.Pistol, 0);
                        NAPI.Player.RemovePlayerWeapon(player, WeaponHash.Pistol);
                        if (tempData.jobColshape != null)
                        {
                            tempData.jobColshape = null;
                        }
                        player.SetData<int>("Player:AmmuQuiz", 0);
                    }
                    else if (player.GetData<int>("Player:AmmuQuiz") == 4)
                    {
                        if (tempData.jobColshape != null)
                        {
                            tempData.jobColshape = null;
                        }
                        player.SetData<int>("Player:AmmuQuiz", 0);
                    }
                }
                //Fishing
                if (player.HasData("Player:FishingRod"))
                {
                    player.ResetData("Player:FishingRod");
                }
                //InShop
                if (player.GetData<bool>("Player:InShop") == true)
                {
                    player.Dimension = 0;
                    player.SetData<bool>("Player:InShop", false);
                }
                //Horse & Glowstick & Runstock & Shovel
                if (player.HasData("Player:HorseStick"))
                {
                    player.ResetData("Player:HorseStick");
                    Helper.AddRemoveAttachments(player, "horseStick", false);
                }
                if (player.HasData("Player:GlowStick"))
                {
                    player.ResetData("Player:GlowStick");
                    Helper.AddRemoveAttachments(player, "glowStick", false);
                }
                if (player.HasData("Player:RunStock"))
                {
                    player.ResetData("Player:RunStock");
                    Helper.AddRemoveAttachments(player, "runStock", false);
                }
                if (player.HasData("Player:Pickaxe"))
                {
                    player.ResetData("Player:Pickaxe");
                    Helper.AddRemoveAttachments(player, "pickaxe", false);
                }
                if (player.HasData("Player:Mikrofon"))
                {
                    player.ResetData("Player:Mikrofon");
                    Helper.AddRemoveAttachments(player, "microfon", false);
                }
                if (player.HasData("Player:Filmkamera"))
                {
                    player.ResetData("Player:Filmkamera");
                    Helper.AddRemoveAttachments(player, "filmcamera", false);
                }
                //InRob
                if (tempData.inrob == true)
                {
                    tempData.inrob = false;
                    player.TriggerEvent("Client:Play3DSound", Settings._Settings.SoundUrl + "alarm.wav", -3);
                }
                //Accept Call + Smartphone
                player.SetData<bool>("Player:AcceptCall", false);
                if (tempData.showSmartphone == true)
                {
                    Helper.AddRemoveAttachments(player, "smartphone", false);
                    tempData.showSmartphone = false;
                }
                //Local Voice RP HandyPlayer
                if (Helper.adminSettings.voicerp == 2)
                {
                    player.SetSharedData("Player:LocalVoiceHandyPlayer", -1);
                }
                //Crystal-Meth
                player.ResetData("Player:HealBonus");
                //Crouching
                tempData.crouching = false;
                player.ResetSharedData("Player:Crouching");
                //Feuersystem
                player.SetData<bool>("Player:Fire", false);
                //Funmodus
                player.SetOwnSharedData("Player:Funmodus", false);
                //Smartphone
                player.SetData<int>("Player:PhoneAnim", 0);
                //Animations
                player.ResetData("Player:PlayCustomAnimation");
                player.SetSharedData("Player:AnimData", "0");
                //Attachments
                player.SetSharedData("Player:Attachments", "0");
                //Farmer
                if (player.HasData("Player:FarmerRoute"))
                {
                    player.ResetData("Player:FarmerRoute");
                }
                //Taxijobs
                if (player.HasData("Player:TaxiJob"))
                {
                    player.ResetData("Player:TaxiJob");
                    if (Helper.taxiJobs.Count > 0)
                    {
                        foreach (Taxi taxi in Helper.taxiJobs.ToList())
                        {
                            if (taxi.id == character.id || taxi.done == character.name)
                            {
                                Helper.taxiJobs.Remove(taxi);
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnPlayerDeath]: " + e.ToString());
            }
        }

        [ServerEvent(Event.VehicleDeath)]
        public static void OnVehicleDeath(Vehicle vehicle)
        {
            try
            {
                if (vehicle != null)
                {
                    if (vehicle.HasSharedData("Vehicle:Text3D"))
                    {
                        vehicle.ResetSharedData("Vehicle:Text3D");
                    }
                    vehicle.SetSharedData("Vehicle:Sync", "0,0,0,0,0,0,0");
                    if (vehicle.HasData("Vehicle:Products"))
                    {
                        vehicle.SetData<int>("Vehicle:Products", 0);
                    }
                    vehicle.SetSharedData("Vehicle:Speedlimit", 0);
                    if (vehicle.HasData("Vehicle:MoneyInfos"))
                    {
                        string[] moneyArray = new string[3];
                        moneyArray = vehicle.GetData<string>("Vehicle:MoneyInfos").Split(",");
                        if (moneyArray[0].Length > 0 && moneyArray[1].Length > 0)
                        {
                            SmartphoneController.OnSmartphoneMessage(null, Helper.UnixTimestamp(), moneyArray[0], "01897337", $"Der Geldtransport in Höhe von {vehicle.GetData<int>("Vehicle:Money")}$ wurde abgebrochen, das Geld wurde zurückgebracht!");
                            Business bizz = Business.GetBusinessById(Convert.ToInt32(moneyArray[2]));
                            if (bizz != null)
                            {
                                bizz.cash += vehicle.GetData<int>("Vehicle:Money");
                            }
                            vehicle.ResetData("Vehicle:Money");
                            vehicle.ResetData("Vehicle:MoneyInfos");
                        }
                    }
                    if (vehicle.HasData("Vehicle:Money"))
                    {
                        vehicle.ResetData("Vehicle:Money");
                    }
                    if (vehicle.HasData("Vehicle:WaffenTransport"))
                    {
                        MDCController.weaponOrderStatus = "DNE";
                        MDCController.weaponOrder = "n/A";
                        MDCController.weaponOrderFaction = 0;
                        vehicle.ResetData("Vehicle:WaffenTransport");
                    }
                    if (vehicle.HasData("Vehicle:AsservatenTransport"))
                    {
                        vehicle.ResetData("Vehicle:AsservatenTransport");
                    }
                    if (Helper.IsATrailer(vehicle))
                    {
                        vehicle.Spawn(vehicle.GetData<Vector3>("Vehicle:Position"));
                        NAPI.Task.Run(() =>
                        {
                            vehicle.Rotation = new Vector3(0.0, 0.0, vehicle.GetData<float>("Vehicle:Rotation"));
                            vehicle.EngineStatus = false;
                        }, delayTime: 215);
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnVehicleDeath]: " + e.ToString());
            }
        }

        [ServerEvent(Event.PlayerEnterVehicle)]
        public static void OnPlayerEnterVehicle(Player player, Vehicle vehicle, sbyte seatId)
        {
            try
            {
                if (vehicle != null)
                {
                    //Radio + Enginesystem
                    if (vehicle.HasSharedData("Vehicle:Sync") && vehicle.GetSharedData<string>("Vehicle:Sync").Split(",")[6] == "0")
                    {
                        if (vehicle.HasData("Vehicle:EngineStatus"))
                        {
                            if (vehicle.GetSharedData<float>("Vehicle:Fuel") > 0)
                            {
                                Helper.SetVehicleEngine(vehicle, vehicle.GetData<bool>("Vehicle:EngineStatus"));
                            }
                            else
                            {
                                Helper.SetVehicleEngine(vehicle, false);
                            }
                        }
                        else
                        {
                            Helper.SetVehicleEngine(vehicle, false);
                        }
                    }
                    else
                    {
                        Helper.SetVehicleEngine(vehicle, false);
                    }
                    player.SetOwnSharedData("Player:VehicleEngine", vehicle.EngineStatus);
                    player.TriggerEvent("Client:RadioOff");
                    TempData tempData = Helper.GetCharacterTempData(player);
                    if (tempData != null)
                    {
                        //Pet
                        if (tempData.pet != null)
                        {
                            tempData.pet.Delete();
                            tempData.pet = null;
                            tempData.petTask = 0;
                        }
                        //Lastvehicle
                        tempData.lastvehicle = vehicle.Id;
                        tempData.lastSeat = seatId;
                        //Speedometer
                        if (vehicle.GetSharedData<string>("Vehicle:Name") != "iak_wheelchair" && (seatId == 0 || (seatId == 1 && (vehicle.GetSharedData<string>("Vehicle:Name") != "bus") && vehicle.GetSharedData<string>("Vehicle:Name") != "coach" && vehicle.GetSharedData<string>("Vehicle:Name") != "rentalbus")) && vehicle.Class != 13)
                        {
                            player.TriggerEvent("Client:ShowSpeedometer");
                            tempData.showspeedo = true;
                        }
                        //Bus
                        if (seatId != 0 && (vehicle.GetSharedData<string>("Vehicle:Name") == "bus" || vehicle.GetSharedData<string>("Vehicle:Name") == "coach" || vehicle.GetSharedData<string>("Vehicle:Name") == "rentalbus"))
                        {
                            Helper.ShowPreShop(player, "Ticketverkauf", 0, 1, 1);
                        }
                    }
                    //Filmkamera
                    if (player.HasData("Player:Filmkamera"))
                    {
                        player.TriggerEvent("Client:ToggleFilmCamera", player.Id, 0);
                    }
                    //Invisible + Devmodus
                    string[] adminSettings = new string[3];
                    adminSettings = player.GetSharedData<string>("Player:Adminsettings").Split(",");
                    if (adminSettings[1] != "0")
                    {
                        player.SetSharedData("Player:Adminsettings", $"{adminSettings[0]},0,{adminSettings[2]}");
                        player.SetOwnSharedData("Player:DevModus", false);
                    }
                    //Jacked
                    if (vehicle.HasData("Vehicle:Jacked") && vehicle.GetData<bool>("Vehicle:Jacked") == true)
                    {
                        Helper.OnStopAnimation2(player);
                    }
                    //Fishing
                    if (player.HasData("Player:FishingRod"))
                    {
                        player.ResetData("Player:FishingRod");
                        Helper.AddRemoveAttachments(player, "fishingRod", false);
                    }
                    //Grabbing
                    if (tempData.followed == true)
                    {
                        Player getPlayer = Helper.GetClosestPlayer(player, 2.5f);
                        if (getPlayer != null && getPlayer != player)
                        {
                            TempData tempData2 = Helper.GetCharacterTempData(getPlayer);
                            Character character2 = Helper.GetCharacterData(getPlayer);
                            Commands.cmd_animation(player, "give", true);
                            Helper.SendNotificationWithoutButton(player, "Du ziehst keinen mehr vor dir her/trägst keinen mehr!", "success");
                            Helper.SendNotificationWithoutButton(getPlayer, "Du wirst nicht mehr gezogen/getragen!", "success");
                            player.SetSharedData("Player:FollowStatus", 0);
                            tempData.followed = false;
                            Helper.OnStopAnimation(player);
                            Helper.OnStopAnimation(getPlayer);
                            NAPI.Task.Run(() =>
                            {
                                player.ResetSharedData("Player:Follow");
                                player.ResetSharedData("Player:FollowStatus");
                                Vector3 newPosition = Helper.GetPositionInFrontOfPlayer(player, 1.15f);
                                Helper.SetPlayerPosition(getPlayer, newPosition);
                                getPlayer.Heading = player.Heading + 90;
                                tempData2.follow = false;
                                getPlayer.TriggerEvent("Client:PlayerFreeze", false);
                                if (tempData2.cuffed > 0)
                                {
                                    getPlayer.SetSharedData("Player:AnimData", $"mp_arresting%idle%{49}");
                                }
                                else if (character2.death == true)
                                {
                                    Helper.OnPlayDeathAnim(getPlayer);
                                }
                                else
                                {
                                    Helper.OnStopAnimation(getPlayer);
                                }
                            }, delayTime: 115);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnPlayerEnterVehicle]: " + e.ToString());
            }
        }

        [ServerEvent(Event.PlayerExitVehicle)]
        public static void OnPlayerExitVehicle(Player player, Vehicle vehicle)
        {
            try
            {
                if (vehicle != null)
                {
                    //Radio + Enginesystem
                    if (vehicle.HasData("Vehicle:EngineStatus"))
                    {
                        vehicle.EngineStatus = vehicle.GetData<bool>("Vehicle:EngineStatus");
                    }
                    player.SetOwnSharedData("Player:VehicleEngine", vehicle.EngineStatus);
                    TempData tempData = Helper.GetCharacterTempData(player);
                    Character character = Helper.GetCharacterData(player);
                    if (tempData != null && character != null)
                    {
                        //Kanalreiniger
                        if (tempData.jobVehicle2 == vehicle)
                        {
                            player.SetIntoVehicle(tempData.jobVehicle2, 0);
                        }
                        //Speedometer
                        if (vehicle.GetSharedData<string>("Vehicle:Name") != "iak_wheelchair" && tempData.showspeedo == true)
                        {
                            player.TriggerEvent("Client:ShowSpeedometer");
                            tempData.showspeedo = false;
                        }
                        //Probefahrt
                        if (tempData.dealerShip != null && player.HasData("Player:TestDrive"))
                        {
                            Helper.SetPlayerPosition(player, tempData.furnitureOldPosition);
                            player.Dimension = 0;
                            player.ResetData("Player:TestDrive");
                            player.SetData<int>("Player:TestDriveCD", Helper.UnixTimestamp() + (60 * 5));
                            Helper.SendNotificationWithoutButton2(player, "Probefahrt beendet, hat Ihnen das Fahrzeug gefallen?", "success", "center", 3250);
                            tempData.dealerShip.Delete();
                            tempData.dealerShip = null;
                            return;
                        }
                        //Drohne
                        if (tempData.jobVehicle2 != null && player.HasData("Player:Drohne"))
                        {
                            player.ResetData("Player:Drohne");
                            Helper.SetPlayerPosition(player, tempData.furniturePosition);
                            if (tempData.jobVehicle2.Health >= 250)
                            {
                                Items newitem = ItemsController.CreateNewItem(player, character.id, "Drohne", "Player", 1, ItemsController.GetFreeItemID(player));
                                if (newitem != null)
                                {
                                    tempData.itemlist.Add(newitem);
                                }
                                Helper.SendNotificationWithoutButton(player, "Drohne abgebaut!", "success");
                            }
                            else
                            {
                                Helper.SendNotificationWithoutButton(player, "Drohne wurde zerstört!", "error");
                            }
                            tempData.jobVehicle2.Delete();
                            tempData.jobVehicle2 = null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnPlayerExitVehicle]: " + e.ToString());
            }
        }

        //Load stuff like blips,textlabel
        public static void LoadStuff()
        {
            try
            {
                //Blips
                //Rathaus
                Blip Rathaus = NAPI.Blip.CreateBlip(498, Helper.RathausPosition, 0.7f, 4);
                NAPI.Blip.SetBlipShortRange(Rathaus, true);
                NAPI.Blip.SetBlipScale(Rathaus, 0.8f);
                NAPI.Blip.SetBlipName(Rathaus, "Rathaus");
                //Rollerverleih Rathaus
                Blip RollerverleihRathaus = NAPI.Blip.CreateBlip(430, new Vector3(-523.0017, -256.7837, 35.6499), 0.7f, 20);
                NAPI.Blip.SetBlipShortRange(RollerverleihRathaus, true);
                NAPI.Blip.SetBlipScale(RollerverleihRathaus, 0.8f);
                NAPI.Blip.SetBlipName(RollerverleihRathaus, "Rollerverleih am Rathaus");
                //Rollerverleih Rancho
                Blip RollerverleihRancho = NAPI.Blip.CreateBlip(430, new Vector3(479.0879, -1861.0743, 27.460703), 0.7f, 20);
                NAPI.Blip.SetBlipShortRange(RollerverleihRancho, true);
                NAPI.Blip.SetBlipScale(RollerverleihRancho, 0.8f);
                NAPI.Blip.SetBlipName(RollerverleihRancho, "Rollerverleih Rancho");
                //Angelmeister Otto
                Blip AngelmeisterOtto = NAPI.Blip.CreateBlip(68, new Vector3(-2195.372, -418.9289, 13.095015), 0.7f, 38);
                NAPI.Blip.SetBlipShortRange(AngelmeisterOtto, true);
                NAPI.Blip.SetBlipScale(AngelmeisterOtto, 0.8f);
                NAPI.Blip.SetBlipName(AngelmeisterOtto, "Angelmeister Otto");
                //Maskenhändler
                Blip MaskenHaendler = NAPI.Blip.CreateBlip(102, new Vector3(-1579.3323, -951.5237, 13.017388), 0.7f, 48);
                NAPI.Blip.SetBlipShortRange(MaskenHaendler, true);
                NAPI.Blip.SetBlipScale(MaskenHaendler, 0.8f);
                NAPI.Blip.SetBlipName(MaskenHaendler, "Maskenhändler");
                //Bank
                Blip Bank1 = NAPI.Blip.CreateBlip(500, new Vector3(234.8648, 217.0689, 106.2867), 0.7f, 25);
                NAPI.Blip.SetBlipName(Bank1, "Maze Bank"); NAPI.Blip.SetBlipShortRange(Bank1, true);
                Blip Bank2 = NAPI.Blip.CreateBlip(500, new Vector3(-1212.882, -330.5203, 37.78701), 0.7f, 25);
                NAPI.Blip.SetBlipName(Bank2, "Fleeca Bank"); NAPI.Blip.SetBlipShortRange(Bank2, true);
                Blip Bank3 = NAPI.Blip.CreateBlip(500, new Vector3(-350.8686, -49.62464, 49.04257), 0.7f, 25);
                NAPI.Blip.SetBlipName(Bank3, "Fleeca Bank"); NAPI.Blip.SetBlipShortRange(Bank3, true);
                Blip Bank4 = NAPI.Blip.CreateBlip(500, new Vector3(314.1704, -278.6919, 54.17079), 0.7f, 25);
                NAPI.Blip.SetBlipName(Bank4, "Fleeca Bank"); NAPI.Blip.SetBlipShortRange(Bank4, true);
                Blip Bank5 = NAPI.Blip.CreateBlip(500, new Vector3(149.6047, -1040.399, 29.37408), 0.7f, 25);
                NAPI.Blip.SetBlipName(Bank5, "Fleeca Bank"); NAPI.Blip.SetBlipShortRange(Bank5, true);
                Blip Bank6 = NAPI.Blip.CreateBlip(500, new Vector3(-2962.524, 482.8735, 15.7031), 0.7f, 25);
                NAPI.Blip.SetBlipName(Bank6, "Fleeca Bank"); NAPI.Blip.SetBlipShortRange(Bank6, true);
                Blip Bank7 = NAPI.Blip.CreateBlip(500, new Vector3(1175.184, 2706.114, 38.09402), 0.7f, 25);
                NAPI.Blip.SetBlipName(Bank7, "Fleeca Bank"); NAPI.Blip.SetBlipShortRange(Bank7, true);
                //ATMs
                Blip Atm1 = NAPI.Blip.CreateBlip(108, new Vector3(155, 6642, 31), 0.4f, 25);
                NAPI.Blip.SetBlipName(Atm1, "Bankautomat"); NAPI.Blip.SetBlipShortRange(Atm1, true); NAPI.Blip.SetBlipScale(Atm1, 0.4f);
                Blip Atm2 = NAPI.Blip.CreateBlip(108, new Vector3(132, 6366, 31), 0.4f, 25);
                NAPI.Blip.SetBlipName(Atm2, "Bankautomat"); NAPI.Blip.SetBlipShortRange(Atm2, true); NAPI.Blip.SetBlipScale(Atm2, 0.5f);
                Blip Atm3 = NAPI.Blip.CreateBlip(108, new Vector3(-282, 6225, 31), 0.4f, 25);
                NAPI.Blip.SetBlipName(Atm3, "Bankautomat"); NAPI.Blip.SetBlipShortRange(Atm3, true); NAPI.Blip.SetBlipScale(Atm3, 0.4f);
                Blip Atm4 = NAPI.Blip.CreateBlip(108, new Vector3(-386, 6045, 31), 0.4f, 25);
                NAPI.Blip.SetBlipName(Atm4, "Bankautomat"); NAPI.Blip.SetBlipShortRange(Atm4, true); NAPI.Blip.SetBlipScale(Atm4, 0.4f);
                Blip Atm5 = NAPI.Blip.CreateBlip(108, new Vector3(1701, 6426, 32), 0.4f, 25);
                NAPI.Blip.SetBlipName(Atm5, "Bankautomat"); NAPI.Blip.SetBlipShortRange(Atm5, true); NAPI.Blip.SetBlipScale(Atm5, 0.4f);
                Blip Atm6 = NAPI.Blip.CreateBlip(108, new Vector3(1735, 6410, 35), 0.4f, 25);
                NAPI.Blip.SetBlipName(Atm6, "Bankautomat"); NAPI.Blip.SetBlipShortRange(Atm6, true); NAPI.Blip.SetBlipScale(Atm6, 0.4f);
                Blip Atm7 = NAPI.Blip.CreateBlip(108, new Vector3(1703, 4933, 42), 0.4f, 25);
                NAPI.Blip.SetBlipName(Atm7, "Bankautomat"); NAPI.Blip.SetBlipShortRange(Atm7, true); NAPI.Blip.SetBlipScale(Atm7, 0.4f);
                Blip Atm8 = NAPI.Blip.CreateBlip(108, new Vector3(1686, 4816, 42), 0.4f, 25);
                NAPI.Blip.SetBlipName(Atm8, "Bankautomat"); NAPI.Blip.SetBlipShortRange(Atm8, true); NAPI.Blip.SetBlipScale(Atm8, 0.4f);
                Blip Atm9 = NAPI.Blip.CreateBlip(108, new Vector3(1822, 3683, 34), 0.4f, 25);
                NAPI.Blip.SetBlipName(Atm9, "Bankautomat"); NAPI.Blip.SetBlipShortRange(Atm9, true); NAPI.Blip.SetBlipScale(Atm9, 0.5f);
                Blip Atm10 = NAPI.Blip.CreateBlip(108, new Vector3(1968, 3743, 32), 0.4f, 25);
                NAPI.Blip.SetBlipName(Atm10, "Bankautomat"); NAPI.Blip.SetBlipShortRange(Atm10, true); NAPI.Blip.SetBlipScale(Atm10, 0.4f);
                Blip Atm11 = NAPI.Blip.CreateBlip(108, new Vector3(-258, -723, 33), 0.4f, 25);
                NAPI.Blip.SetBlipName(Atm11, "Bankautomat"); NAPI.Blip.SetBlipShortRange(Atm11, true); NAPI.Blip.SetBlipScale(Atm11, 0.4f);
                Blip Atm12 = NAPI.Blip.CreateBlip(108, new Vector3(-256, -715, 33), 1.0f, 25);
                NAPI.Blip.SetBlipName(Atm12, "Bankautomat"); NAPI.Blip.SetBlipShortRange(Atm12, true); NAPI.Blip.SetBlipScale(Atm12, 0.5f);
                Blip Atm14 = NAPI.Blip.CreateBlip(108, new Vector3(-258, -723, 44), 0.4f, 25);
                NAPI.Blip.SetBlipName(Atm14, "Bankautomat"); NAPI.Blip.SetBlipShortRange(Atm14, true); NAPI.Blip.SetBlipScale(Atm14, 0.4f);
                Blip Atm15 = NAPI.Blip.CreateBlip(108, new Vector3(1078, -776, 57), 0.4f, 25);
                NAPI.Blip.SetBlipName(Atm15, "Bankautomat"); NAPI.Blip.SetBlipShortRange(Atm15, true); NAPI.Blip.SetBlipScale(Atm15, 0.4f);
                Blip Atm16 = NAPI.Blip.CreateBlip(108, new Vector3(1138, -468, 66), 0.4f, 25);
                NAPI.Blip.SetBlipName(Atm16, "Bankautomat"); NAPI.Blip.SetBlipShortRange(Atm16, true); NAPI.Blip.SetBlipScale(Atm16, 0.4f);
                Blip Atm17 = NAPI.Blip.CreateBlip(108, new Vector3(1166, -456, 66), 0.4f, 25);
                NAPI.Blip.SetBlipName(Atm17, "Bankautomat"); NAPI.Blip.SetBlipShortRange(Atm17, true); NAPI.Blip.SetBlipScale(Atm17, 0.4f);
                Blip Atm18 = NAPI.Blip.CreateBlip(108, new Vector3(1153, -326, 69), 0.4f, 25);
                NAPI.Blip.SetBlipName(Atm18, "Bankautomat"); NAPI.Blip.SetBlipShortRange(Atm18, true); NAPI.Blip.SetBlipScale(Atm18, 0.4f);
                Blip Atm19 = NAPI.Blip.CreateBlip(108, new Vector3(285, 143, 104), 0.4f, 25);
                NAPI.Blip.SetBlipName(Atm19, "Bankautomat"); NAPI.Blip.SetBlipShortRange(Atm19, true); NAPI.Blip.SetBlipScale(Atm19, 0.4f);
                Blip Atm20 = NAPI.Blip.CreateBlip(108, new Vector3(89, 2, 67), 0.4f, 25);
                NAPI.Blip.SetBlipName(Atm20, "Bankautomat"); NAPI.Blip.SetBlipShortRange(Atm20, true); NAPI.Blip.SetBlipScale(Atm20, 0.4f);
                Blip Atm21 = NAPI.Blip.CreateBlip(108, new Vector3(-56, -1752, 29), 0.4f, 25);
                NAPI.Blip.SetBlipName(Atm21, "Bankautomat"); NAPI.Blip.SetBlipShortRange(Atm21, true); NAPI.Blip.SetBlipScale(Atm21, 0.4f);
                Blip Atm22 = NAPI.Blip.CreateBlip(108, new Vector3(33, -1348, 29), 0.4f, 25);
                NAPI.Blip.SetBlipName(Atm22, "Bankautomat"); NAPI.Blip.SetBlipShortRange(Atm22, true); NAPI.Blip.SetBlipScale(Atm22, 0.4f);
                Blip Atm23 = NAPI.Blip.CreateBlip(108, new Vector3(288, -1282, 29), 0.4f, 25);
                NAPI.Blip.SetBlipName(Atm23, "Bankautomat"); NAPI.Blip.SetBlipShortRange(Atm23, true); NAPI.Blip.SetBlipScale(Atm23, 0.4f);
                Blip Atm24 = NAPI.Blip.CreateBlip(108, new Vector3(289, -1256, 29), 0.4f, 25);
                NAPI.Blip.SetBlipName(Atm24, "Bankautomat"); NAPI.Blip.SetBlipShortRange(Atm24, true); NAPI.Blip.SetBlipScale(Atm24, 0.4f);
                Blip Atm25 = NAPI.Blip.CreateBlip(108, new Vector3(146, -1035, 29), 0.4f, 25);
                NAPI.Blip.SetBlipName(Atm25, "Bankautomat"); NAPI.Blip.SetBlipShortRange(Atm25, true); NAPI.Blip.SetBlipScale(Atm25, 0.4f);
                Blip Atm26 = NAPI.Blip.CreateBlip(108, new Vector3(199, -883, 31), 0.4f, 25);
                NAPI.Blip.SetBlipName(Atm26, "Bankautomat"); NAPI.Blip.SetBlipShortRange(Atm26, true); NAPI.Blip.SetBlipScale(Atm26, 0.4f);
                Blip Atm27 = NAPI.Blip.CreateBlip(108, new Vector3(112, -775, 31), 0.4f, 25);
                NAPI.Blip.SetBlipName(Atm27, "Bankautomat"); NAPI.Blip.SetBlipShortRange(Atm27, true); NAPI.Blip.SetBlipScale(Atm27, 0.4f);
                Blip Atm28 = NAPI.Blip.CreateBlip(108, new Vector3(112, -819, 31), 0.4f, 25);
                NAPI.Blip.SetBlipName(Atm28, "Bankautomat"); NAPI.Blip.SetBlipShortRange(Atm28, true); NAPI.Blip.SetBlipScale(Atm28, 0.4f);
                Blip Atm29 = NAPI.Blip.CreateBlip(108, new Vector3(296, -895, 29), 0.4f, 25);
                NAPI.Blip.SetBlipName(Atm29, "Bankautomat"); NAPI.Blip.SetBlipShortRange(Atm29, true); NAPI.Blip.SetBlipScale(Atm29, 0.4f);
                Blip Atm30 = NAPI.Blip.CreateBlip(108, new Vector3(-1827, 784, 138), 0.4f, 25);
                NAPI.Blip.SetBlipName(Atm30, "Bankautomat"); NAPI.Blip.SetBlipShortRange(Atm30, true); NAPI.Blip.SetBlipScale(Atm30, 0.4f);
                Blip Atm31 = NAPI.Blip.CreateBlip(108, new Vector3(-1410, -99, 52), 0.4f, 25);
                NAPI.Blip.SetBlipName(Atm31, "Bankautomat"); NAPI.Blip.SetBlipShortRange(Atm31, true); NAPI.Blip.SetBlipScale(Atm31, 0.4f);
                Blip Atm33 = NAPI.Blip.CreateBlip(108, new Vector3(2683, 3286, 55), 0.4f, 25);
                NAPI.Blip.SetBlipName(Atm33, "Bankautomat"); NAPI.Blip.SetBlipShortRange(Atm33, true); NAPI.Blip.SetBlipScale(Atm33, 0.4f);
                Blip Atm34 = NAPI.Blip.CreateBlip(108, new Vector3(2564, 2584, 38), 0.4f, 25);
                NAPI.Blip.SetBlipName(Atm34, "Bankautomat"); NAPI.Blip.SetBlipShortRange(Atm34, true); NAPI.Blip.SetBlipScale(Atm34, 0.4f);
                Blip Atm35 = NAPI.Blip.CreateBlip(108, new Vector3(1171, 2702, 38), 0.4f, 25);
                NAPI.Blip.SetBlipName(Atm35, "Bankautomat"); NAPI.Blip.SetBlipShortRange(Atm35, true); NAPI.Blip.SetBlipScale(Atm35, 0.4f);
                Blip Atm36 = NAPI.Blip.CreateBlip(108, new Vector3(-1091, 2708, 18), 0.4f, 25);
                NAPI.Blip.SetBlipName(Atm36, "Bankautomat"); NAPI.Blip.SetBlipShortRange(Atm36, true); NAPI.Blip.SetBlipScale(Atm36, 0.4f);
                Blip Atm37 = NAPI.Blip.CreateBlip(108, new Vector3(-1827, 784, 138), 0.4f, 25);
                NAPI.Blip.SetBlipName(Atm37, "Bankautomat"); NAPI.Blip.SetBlipShortRange(Atm37, true); NAPI.Blip.SetBlipScale(Atm37, 0.4f);
                Blip Atm38 = NAPI.Blip.CreateBlip(108, new Vector3(-1410, -99, 52), 0.4f, 25);
                NAPI.Blip.SetBlipName(Atm38, "Bankautomat"); NAPI.Blip.SetBlipShortRange(Atm38, true); NAPI.Blip.SetBlipScale(Atm38, 0.4f);
                Blip Atm39 = NAPI.Blip.CreateBlip(108, new Vector3(-1540, -546, 34), 0.4f, 25);
                NAPI.Blip.SetBlipName(Atm39, "Bankautomat"); NAPI.Blip.SetBlipShortRange(Atm39, true); NAPI.Blip.SetBlipScale(Atm39, 0.4f);
                Blip Atm40 = NAPI.Blip.CreateBlip(108, new Vector3(-254, -692, 33), 0.4f, 25);
                NAPI.Blip.SetBlipName(Atm40, "Bankautomat"); NAPI.Blip.SetBlipShortRange(Atm40, true); NAPI.Blip.SetBlipScale(Atm40, 0.4f);
                Blip Atm41 = NAPI.Blip.CreateBlip(108, new Vector3(-302, -829, 32), 0.4f, 25);
                NAPI.Blip.SetBlipName(Atm41, "Bankautomat"); NAPI.Blip.SetBlipShortRange(Atm41, true); NAPI.Blip.SetBlipScale(Atm41, 0.4f);
                Blip Atm42 = NAPI.Blip.CreateBlip(108, new Vector3(-526, -1222, 18), 0.4f, 25);
                NAPI.Blip.SetBlipName(Atm42, "Bankautomat"); NAPI.Blip.SetBlipShortRange(Atm42, true); NAPI.Blip.SetBlipScale(Atm42, 0.4f);
                Blip Atm43 = NAPI.Blip.CreateBlip(108, new Vector3(-537, -854, 29), 0.4f, 25);
                NAPI.Blip.SetBlipName(Atm43, "Bankautomat"); NAPI.Blip.SetBlipShortRange(Atm43, true); NAPI.Blip.SetBlipScale(Atm43, 0.4f);
                Blip Atm44 = NAPI.Blip.CreateBlip(108, new Vector3(-613, -704, 31), 0.4f, 25);
                NAPI.Blip.SetBlipName(Atm44, "Bankautomat"); NAPI.Blip.SetBlipShortRange(Atm44, true); NAPI.Blip.SetBlipScale(Atm44, 0.4f);
                Blip Atm45 = NAPI.Blip.CreateBlip(108, new Vector3(-660, -854, 24), 0.4f, 25);
                NAPI.Blip.SetBlipName(Atm45, "Bankautomat"); NAPI.Blip.SetBlipShortRange(Atm45, true); NAPI.Blip.SetBlipScale(Atm45, 0.4f);
                Blip Atm46 = NAPI.Blip.CreateBlip(108, new Vector3(-711, -818, 23), 0.4f, 25);
                NAPI.Blip.SetBlipName(Atm46, "Bankautomat"); NAPI.Blip.SetBlipShortRange(Atm46, true); NAPI.Blip.SetBlipScale(Atm46, 0.4f);
                Blip Atm47 = NAPI.Blip.CreateBlip(108, new Vector3(-717, -915, 19), 0.4f, 25);
                NAPI.Blip.SetBlipName(Atm47, "Bankautomat"); NAPI.Blip.SetBlipShortRange(Atm47, true); NAPI.Blip.SetBlipScale(Atm47, 0.4f);
                Blip Atm48 = NAPI.Blip.CreateBlip(108, new Vector3(-3241.2097, 997.58295, 12.550397), 0.4f, 25);
                NAPI.Blip.SetBlipName(Atm48, "Bankautomat"); NAPI.Blip.SetBlipShortRange(Atm48, true); NAPI.Blip.SetBlipScale(Atm48, 0.4f);
                Blip Atm49 = NAPI.Blip.CreateBlip(108, new Vector3(-549.6389, -204.38058, 38.223045), 0.4f, 25);
                NAPI.Blip.SetBlipName(Atm49, "Bankautomat"); NAPI.Blip.SetBlipShortRange(Atm49, true); NAPI.Blip.SetBlipScale(Atm49, 0.4f);
                Blip Atm50 = NAPI.Blip.CreateBlip(108, new Vector3(2558.4365, 389.4997, 108.62296), 0.4f, 25);
                NAPI.Blip.SetBlipName(Atm50, "Bankautomat"); NAPI.Blip.SetBlipShortRange(Atm50, true); NAPI.Blip.SetBlipScale(Atm50, 0.4f);
                Blip Atm51 = NAPI.Blip.CreateBlip(108, new Vector3(380.80334, 323.38132, 103.56637), 0.4f, 25);
                NAPI.Blip.SetBlipName(Atm51, "Bankautomat"); NAPI.Blip.SetBlipShortRange(Atm51, true); NAPI.Blip.SetBlipScale(Atm51, 0.4f);
                Blip Atm52 = NAPI.Blip.CreateBlip(108, new Vector3(540.4012, 2671.142, 42.156494), 0.4f, 25);
                NAPI.Blip.SetBlipName(Atm52, "Bankautomat"); NAPI.Blip.SetBlipShortRange(Atm52, true); NAPI.Blip.SetBlipScale(Atm52, 0.4f);
                Blip Atm53 = NAPI.Blip.CreateBlip(108, new Vector3(-3240.577, 1008.6651, 12.830707), 0.4f, 25);
                NAPI.Blip.SetBlipName(Atm53, "Bankautomat"); NAPI.Blip.SetBlipShortRange(Atm53, true); NAPI.Blip.SetBlipScale(Atm53, 0.4f);
                //Jobs
                Blip job1 = NAPI.Blip.CreateBlip(67, new Vector3(-1077.0637, -2072.4856, 13.291507), 0.7f, 39);
                NAPI.Blip.SetBlipName(job1, "Spediteur Job"); NAPI.Blip.SetBlipShortRange(job1, true);
                Blip job2 = NAPI.Blip.CreateBlip(119, new Vector3(1714.7175, -571.90814, 144.50644), 0.7f, 52);
                NAPI.Blip.SetBlipName(job2, "Jagtgebiet"); NAPI.Blip.SetBlipShortRange(job2, true);
                Blip job3 = NAPI.Blip.CreateBlip(119, new Vector3(1094.8888, -265.38666, 69.314156), 0.7f, 39);
                NAPI.Blip.SetBlipName(job3, "Jäger Job"); NAPI.Blip.SetBlipShortRange(job3, true);
                Blip job4 = NAPI.Blip.CreateBlip(513, new Vector3(440.02197, -646.4714, 28.520739), 0.7f, 51);
                NAPI.Blip.SetBlipName(job4, "Busdepot"); NAPI.Blip.SetBlipShortRange(job4, true);
                Blip job5 = NAPI.Blip.CreateBlip(318, new Vector3(-350.10602, -1569.9631, 25.221148), 0.7f, 12);
                NAPI.Blip.SetBlipName(job5, "Mülldeponie"); NAPI.Blip.SetBlipShortRange(job5, true);
                Blip job6 = NAPI.Blip.CreateBlip(198, new Vector3(895.20514, -179.2199, 74.700356), 0.7f, 46);
                NAPI.Blip.SetBlipName(job6, "Yellow Cab Co."); NAPI.Blip.SetBlipShortRange(job6, true);
                Blip job7 = NAPI.Blip.CreateBlip(479, new Vector3(415.26788, -2072.553, 21.47752), 0.7f, 54);
                NAPI.Blip.SetBlipName(job7, "Kanalreiniger Job"); NAPI.Blip.SetBlipShortRange(job7, true);
                Blip job8 = NAPI.Blip.CreateBlip(140, new Vector3(2243.3335, 5154.16, 57.88714), 0.7f, 2);
                NAPI.Blip.SetBlipName(job8, "Landwirt Job"); NAPI.Blip.SetBlipShortRange(job8, true);
                Blip job9 = NAPI.Blip.CreateBlip(431, new Vector3(-1376.8951, -1424.4462, 3.5720022), 0.7f, 82);
                NAPI.Blip.SetBlipName(job9, "Müll sammeln"); NAPI.Blip.SetBlipShortRange(job9, true);
                Blip job10 = NAPI.Blip.CreateBlip(616, new Vector3(-1402.6819, -451.97513, 34.482605), 0.7f, 56);
                NAPI.Blip.SetBlipName(job10, "Geldlieferant Job"); NAPI.Blip.SetBlipShortRange(job10, true);
                //Fraktionen
                Blip frak1 = NAPI.Blip.CreateBlip(60, new Vector3(443.5972, -984.0872, 30.689312), 0.7f, 3);
                NAPI.Blip.SetBlipName(frak1, "Los Santos Police-Department - Zweigstelle"); NAPI.Blip.SetBlipShortRange(frak1, true);
                Blip frak11 = NAPI.Blip.CreateBlip(60, new Vector3(618.4254, -14.090905, 82.62816), 0.7f, 3);
                NAPI.Blip.SetBlipName(frak11, "Los Santos Police-Department"); NAPI.Blip.SetBlipShortRange(frak11, true);
                Blip frak2 = NAPI.Blip.CreateBlip(61, new Vector3(-673.85614, 324.5422, 83.083206), 0.7f, 49);
                NAPI.Blip.SetBlipName(frak2, "Los Santos Rescue-Center"); NAPI.Blip.SetBlipShortRange(frak2, true);
                Blip frak3 = NAPI.Blip.CreateBlip(446, new Vector3(-338.33298, -130.46397, 39.012974), 0.7f, 39);
                NAPI.Blip.SetBlipName(frak3, "Los Santos Customs"); NAPI.Blip.SetBlipShortRange(frak3, true);
                Blip frak33 = NAPI.Blip.CreateBlip(810, new Vector3(408.9756, -1622.715, 29.291948), 0.7f, 39);
                NAPI.Blip.SetBlipName(frak33, "Verwahrplatz"); NAPI.Blip.SetBlipShortRange(frak33, true);
                //Spandex
                Blip spandex = NAPI.Blip.CreateBlip(67, new Vector3(155.02916, -3290.8066, 7.030337), 0.7f, 3);
                NAPI.Blip.SetBlipShortRange(spandex, true);
                NAPI.Blip.SetBlipScale(spandex, 0.8f);
                NAPI.Blip.SetBlipName(spandex, "Spedition Spandex");
                //Shovel
                Blip shovel = NAPI.Blip.CreateBlip(431, new Vector3(1441.0588, 3749.4375, 32.193043), 0.7f, 71);
                NAPI.Blip.SetBlipShortRange(shovel, true);
                NAPI.Blip.SetBlipScale(shovel, 0.8f);
                NAPI.Blip.SetBlipName(shovel, "Schatzsucher Billy");
                //TextLabel
                //Spandex
                NAPI.TextLabel.CreateTextLabel("~b~Spedition Spandex\n~w~Benutze Taste ~b~[F6]~w~ um deine Lieferung abzuholen!", new Vector3(163.66289, -3290.7888, 5.928894 + 1.1), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.Marker.CreateMarker(39, new Vector3(163.66289, -3290.7888, 5.928894 - 0.1), new Vector3(12.569119, -1105.6268, 29.797026 - 1.1), new Vector3(), 1.0f, new Color(63, 103, 145), false, 0);
                //Waffenschein
                NAPI.TextLabel.CreateTextLabel("~b~Waffenschein(12500$) beantragen\n~w~Benutze Taste ~b~[F]~w~ um einen Waffenschein zu beantragen!", new Vector3(12.569119, -1105.6268, 29.797026 + 1.1), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.Marker.CreateMarker(1, new Vector3(12.569119, -1105.6268, 29.797026 - 1.1), new Vector3(12.569119, -1105.6268, 29.797026 - 1.1), new Vector3(), 1.0f, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Shootingrange\n~w~Benutze Taste ~b~[F]~w~ um die Shootingrange zu benutzen!", new Vector3(6.5058265, -1100.1274, 29.797022 + 0.5), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                //Credits
                labelCheck = NAPI.TextLabel.CreateTextLabel("~w~Danke an ~b~Nemesus ~w~- (Nemesus.de)", new Vector3(-524.97723, -238.83641, 36.29891 + 0.4), 10.0f, 0.5f, 4, new Color(255, 255, 255), true, 0);
                NAPI.TextLabel.CreateTextLabel("~w~Danke an ~b~YunaLable", new Vector3(-524.78235, -240.84464, 36.07903 + 0.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), true, 0);
                NAPI.TextLabel.CreateTextLabel("~w~Danke an ~b~Hrred", new Vector3(-523.1048, -240.06908, 36.07903 + 0.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), true, 0);
                //Musicsystem
                NAPI.TextLabel.CreateTextLabel("~b~Musikstation\n~w~Benutze Taste ~b~[F]~w~ um diese zu benutzen!", new Vector3(1987.5565, 3051.1028, 47.215157 + 0.7), 10.0f, 0.5f, 4, new Color(255, 255, 255), true, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Musikstation\n~w~Benutze Taste ~b~[F]~w~ um diese zu benutzen!", new Vector3(121.39574, -1279.8123, 29.6533 + 0.5), 10.0f, 0.5f, 4, new Color(255, 255, 255), true, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Musikstation\n~w~Benutze Taste ~b~[F]~w~ um diese zu benutzen!", new Vector3(840.6783, -118.2715, 79.77466 + 0.7), 10.0f, 0.5f, 4, new Color(255, 255, 255), true, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Musikstation\n~w~Benutze Taste ~b~[F]~w~ um diese zu benutzen!", new Vector3(-456.60736, 274.12994, 84.22368 + 0.5), 10.0f, 0.5f, 4, new Color(255, 255, 255), true, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Musikstation\n~w~Benutze Taste ~b~[F]~w~ um diese zu benutzen!", new Vector3(-561.8999, 281.58398, 85.67638 + 0.5), 10.0f, 0.5f, 4, new Color(255, 255, 255), true, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Musikstation\n~w~Benutze Taste ~b~[F]~w~ um diese zu benutzen!", new Vector3(-1381.4324, -616.371, 31.497929 + 0.5), 10.0f, 0.5f, 4, new Color(255, 255, 255), true, 0);
                //Rollerverleih
                NAPI.TextLabel.CreateTextLabel("~b~Rollerverleih(150$) am Rathaus\n~w~Benutze Taste ~b~[F]~w~ um mit dem Rollerverleiher zu interagieren!", new Vector3(-523.0017, -256.7837, 35.6499 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), true, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Rollerverleih(150$) Rancho\n~w~Benutze Taste ~b~[F]~w~ um mit dem Rollerverleiher zu interagieren!", new Vector3(479.0879, -1861.0743, 27.460703 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), true, 0);
                //RIP
                NAPI.TextLabel.CreateTextLabel("~y~Rest in Peace\n~w~J.W.S", new Vector3(-1765.8063, -259.54675, 49.32865 + 0.35), 10.0f, 0.5f, 4, new Color(255, 255, 255), true, 0);
                //Rathaus
                NAPI.TextLabel.CreateTextLabel("~b~Los Santos Rathaus\n~w~Benutze Taste ~b~[F]~w~ um mit der Sekretärin zu interagieren!", new Vector3(Helper.RathausPosition.X, Helper.RathausPosition.Y, Helper.RathausPosition.Z + 1.3), 7.5f, 0.5f, 4, new Color(255, 255, 255), true, 0);
                //Kleidungsläden
                NAPI.TextLabel.CreateTextLabel("~b~Kleidungsladen\n~w~Benutze Taste ~b~[F]~w~ um mit der Verkäuferin zu interagieren!", new Vector3(77.48367, -1387.6338, 29.376139 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Kleidungsladen\n~w~Benutze Taste ~b~[F]~w~ um mit der Verkäuferin zu interagieren!", new Vector3(-1194.0197, -767.0693, 17.316254 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Kleidungsladen\n~w~Benutze Taste ~b~[F]~w~ um mit der Verkäuferin zu interagieren!", new Vector3(-710.3996, -152.13515, 37.41514 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Kleidungsladen\n~w~Benutze Taste ~b~[F]~w~ um mit der Verkäuferin zu interagieren!", new Vector3(-3169.3755, 1043.192, 20.863214 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                //Bank
                NAPI.TextLabel.CreateTextLabel("~b~Maze Bank Schalter\n~w~Benutze Taste ~b~[F]~w~ um deine Konten zu verwalten!", new Vector3(243.1547, 224.7177, 106.2868 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Fleeca Bank Schalter\n~w~Benutze Taste ~b~[F]~w~ um deine Konten zu verwalten!", new Vector3(-1212.084, -330.4282, 37.78704 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Fleeca Bank Schalter\n~w~Benutze Taste ~b~[F]~w~ um deine Konten zu verwalten!", new Vector3(-350.551, -50.09611, 49.04259 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Fleeca Bank Schalter\n~w~Benutze Taste ~b~[F]~w~ um deine Konten zu verwalten!", new Vector3(314.8576, -279.327, 54.17081 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Fleeca Bank Schalter\n~w~Benutze Taste ~b~[F]~w~ um deine Konten zu verwalten!", new Vector3(150.402, -1040.878, 29.3741 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Fleeca Bank Schalter\n~w~Benutze Taste ~b~[F]~w~ um deine Konten zu verwalten!", new Vector3(-2962.552, 483.3518, 15.70312 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Fleeca Bank Schalter\n~w~Benutze Taste ~b~[F]~w~ um deine Konten zu verwalten!", new Vector3(1174.971, 2706.805, 38.09408 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                //Angelmeister Otto
                NAPI.TextLabel.CreateTextLabel("~b~Angelmeister Otto\n~w~Benutze Taste ~b~[F]~w~ um mit Ihm zu interagieren!", new Vector3(-2195.372, -418.9289, 13.095015 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                //Bars
                NAPI.TextLabel.CreateTextLabel("~b~Barkeeperin\n~w~Benutze Taste ~b~[F]~w~ um mit Ihm zu interagieren!", new Vector3(-561.7678, 287.1561, 82.17647 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Barkeeper\n~w~Benutze Taste ~b~[F]~w~ um mit Ihm zu interagieren!", new Vector3(129.46414, -1283.6378, 29.272814 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Barkeeperin\n~w~Benutze Taste ~b~[F]~w~ um mit Ihm zu interagieren!", new Vector3(1984.1924, 3054.6648, 47.215168 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Barkeeper Lukas Koch\n~w~Benutze Taste ~b~[F]~w~ um mit Ihm zu interagieren!", new Vector3(836.1573, -115.343544, 79.77466 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Barkeeper\n~w~Benutze Taste ~b~[F]~w~ um mit Ihm zu interagieren!", new Vector3(-434.2632, 273.92822, 83.42211 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Barkeeper\n~w~Benutze Taste ~b~[F]~w~ um mit Ihm zu interagieren!", new Vector3(-1391.7916, -605.65985, 30.319567 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Barkeeper\n~w~Benutze Taste ~b~[F]~w~ um mit Ihm zu interagieren!", new Vector3(-1377.5298, -629.2274, 30.819584 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), true, 0);
                //Shovel
                NAPI.TextLabel.CreateTextLabel("~b~Schatzsucher Billy\n~w~Benutze Taste ~b~[F]~w~ um mit Ihm zu interagieren!", new Vector3(1441.0588, 3749.4375, 32.193043 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                //Hall of Fame
                NAPI.TextLabel.CreateTextLabel("~b~Hall of Fame Statue\n~w~Benutze Taste ~b~[F]~w~ um dir die Inschrift anzusehen!", new Vector3(-512.5338, -209.81868, 38.338326 + 1.25), 6.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                //Jobs
                //Spedition
                NAPI.TextLabel.CreateTextLabel("~b~Speditionsvorarbeiter Toni\n~w~Benutze Taste ~b~[F]~w~ um mit Ihm zu komunizieren!", new Vector3(-1107.5642, -2040.5759, 13.291501 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Fuhrparkverwalter Nils\n~w~Benutze Taste ~b~[F]~w~ um mit Ihm zu komunizieren!", new Vector3(-1077.2032, -2079.2852, 13.291501 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Aufladestelle\n~w~Benutze Taste ~b~[F3]~w~ um Produkte aufzuladen", new Vector3(-1154.1656, -2173.3665, 12.749134 + 1.5), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                //Busfahrer
                NAPI.TextLabel.CreateTextLabel("~b~Ticketschalter\n~w~Benutze Taste ~b~[F]~w~ um dir ein Busticket zu kaufen!", new Vector3(440.02197, -646.4714, 28.520739 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), true, 0);
                busLabel = NAPI.TextLabel.CreateTextLabel("~b~Busexperte JinJong\n~w~Benutze Taste ~b~[F]~w~ um mit Ihm zu kummunizieren\nZurzeit sind 0 Busfahrer auf einer Route unterwegs!", new Vector3(441.61496, -628.094, 28.52075 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), true, 0);
                NAPI.TextLabel.CreateTextLabel($"~b~Haltestelle: Busdepot\n~w~Abfahrtspunkt folgender Routen:\n{Helper.GetBusRoutes()}", new Vector3(429.0873, -658.635, 28.78579 + 0.70), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                //Kanalreiniger
                NAPI.TextLabel.CreateTextLabel("~b~Kanalreiniger Marcel\n~w~Benutze Taste ~b~[F]~w~ um mit Ihm zu kummunizieren!", new Vector3(415.26788, -2072.553, 21.47752 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), true, 0);
                //Kanalreiniger
                NAPI.TextLabel.CreateTextLabel("~b~Landwirt Mike\n~w~Benutze Taste ~b~[F]~w~ um mit Ihm zu kummunizieren!", new Vector3(2243.3335, 5154.16, 57.88714 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), true, 0);
                //Taxifahrer
                NAPI.TextLabel.CreateTextLabel("~b~Yellow Cab Co. CvD Mike\n~w~Benutze Taste ~b~[F]~w~ um mit Ihm zu kummunizieren!", new Vector3(895.20514, -179.2199, 74.700356 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), true, 0);
                //Jäger
                NAPI.TextLabel.CreateTextLabel("~b~Revierjäger Wilfried\n~w~Benutze Taste ~b~[F]~w~ um mit Ihm zu komunizieren!", new Vector3(1094.8888, -265.38666, 69.314156 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                //Müllmann
                NAPI.TextLabel.CreateTextLabel("~b~Müllexperte Julian\n~w~Benutze Taste ~b~[F]~w~ um mit Ihm zu komunizieren!", new Vector3(-350.10602, -1569.9631, 25.221148 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                //Müllmann
                NAPI.TextLabel.CreateTextLabel("~b~Geldlieferant Justin\n~w~Benutze Taste ~b~[F]~w~ um mit Ihm zu komunizieren!", new Vector3(-1402.6819, -451.97513, 34.482605 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                //Fahrschule
                NAPI.TextLabel.CreateTextLabel("~b~Fahrlehrer Manfred\n~w~Benutze Taste ~b~[F]~w~ um mit Ihm zu komunizieren!", new Vector3(-711.8821, -1307.4515, 5.113356 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                //Müll sammeln am Strand
                NAPI.TextLabel.CreateTextLabel("~b~Müllbeauftragte Tanja\n~w~Benutze Taste ~b~[F]~w~ um mit Ihr zu komunizieren!", new Vector3(-1376.8951, -1424.4462, 3.5720022 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                //Müllmann
                NAPI.TextLabel.CreateTextLabel("~b~Maskenhändler Joshi\n~w~Benutze Taste ~b~[F]~w~ um mit Ihm zu komunizieren!", new Vector3(-1579.3323, -951.5237, 13.017388 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                //LSPD
                NAPI.TextLabel.CreateTextLabel("~b~Los Santos Police-Department\n~w~Benutze Taste ~b~[F]~w~ um mit der Sekretärin zu komunizieren!", new Vector3(443.5972, -984.0872, 30.689312 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Los Santos Police-Department\n~w~Benutze Taste ~b~[F]~w~ um mit der Sekretärin zu komunizieren!", new Vector3(633.46796, 8.740156, 82.628075 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Klingel\n~w~Benutze Taste ~b~[E]~w~ um zu klingeln!", new Vector3(441.58102, -985.00275, 30.68931 + 0.5), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Klingel\n~w~Benutze Taste ~b~[E]~w~ um zu klingeln!", new Vector3(631.1063, 7.9442596, 82.62809 + 0.5), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Fuhrparkmeister Otto\n~w~Benutze Taste ~b~[F4]~w~ um mit Ihm zu komunizieren!", new Vector3(445.07547, -972.24146, 25.78847 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Fuhrparkmeister Kalle\n~w~Benutze Taste ~b~[F4]~w~ um mit Ihm zu komunizieren!", new Vector3(463.60544, -982.2816, 43.692 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Fuhrparkmeister Sascha\n~w~Benutze Taste ~b~[F4]~w~ um mit Ihm zu komunizieren!", new Vector3(608.4999, -2.2341008, 70.62814 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Fuhrparkmeister Oli\n~w~Benutze Taste ~b~[F4]~w~ um mit Ihm zu komunizieren!", new Vector3(569.6448, 10.3835535, 103.22986 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Waffenkammer\n~w~Benutze Taste ~b~[F]~w~ um auf diese zuzugreifen!", new Vector3(609.0513, -14.120579, 76.62814 + 0.825), 10.0f, 0.5f, 4, new Color(255, 255, 255), true, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Waffenkammer\n~w~Benutze Taste ~b~[F]~w~ um auf diese zuzugreifen!", new Vector3(484.23672, -1002.12396, 25.734667 + 0.825), 10.0f, 0.5f, 4, new Color(255, 255, 255), true, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Asservatenkammer\n~w~Benutze Taste ~b~[I]~w~ um auf diese zuzugreifen!", new Vector3(474.7032, -1007.7393, 34.217087 + 0.85), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Asservatenkammer\n~w~Benutze Taste ~b~[I]~w~ um auf diese zuzugreifen!", new Vector3(620.27234, -11.100246, 76.628174 + 0.85), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Umkleidekabine\n~w~Benutze Taste ~b~[F]~w~ um deinen Dienst zu beginnen/beenden!", new Vector3(471.16223, -988.9328, 25.734646 + 0.85), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Umkleidekabine\n~w~Benutze Taste ~b~[F]~w~ um deinen Dienst zu beginnen/beenden!", new Vector3(629.6149, 3.6072264, 76.628044 + 0.85), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Umkleidekabine\n~w~Benutze Taste ~b~[F]~w~ um deinen Dienst zu beginnen/beenden!", new Vector3(624.4518, -3.4003117, 76.628136 + 0.85), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~w~Auslieferungspunkt", new Vector3(597.40106, -33.440994, 70.628044 + 0.85), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Snackpoint\n~w~Benutze Taste ~b~[F]~w~ um dir ein Snack zu kaufen!", new Vector3(606.85767, -4.1742187, 82.62812 + 0.85), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~w~Zum Dach", new Vector3(604.84576, 5.519227, 97.87246 + 0.85), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~w~Ins Gebäude", new Vector3(565.7387, 4.9065294, 103.233215 + 0.85), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.Marker.CreateMarker(30, new Vector3(474.7032, -1007.7393, 34.217087 - 0.1), new Vector3(474.7032, -1007.7393, 34.217087 - 1.1), new Vector3(), 0.5f, new Color(63, 103, 145), false, 0);
                NAPI.Marker.CreateMarker(30, new Vector3(620.27234, -11.100246, 76.628174 - 0.1), new Vector3(620.27234, -11.100246, 76.628174 - 1.1), new Vector3(), 0.5f, new Color(63, 103, 145), false, 0);
                NAPI.Marker.CreateMarker(30, new Vector3(471.16223, -988.9328, 25.734646 - 0.1), new Vector3(471.16223, -988.9328, 25.734646 - 1.1), new Vector3(), 0.5f, new Color(63, 103, 145), false, 0);
                NAPI.Marker.CreateMarker(30, new Vector3(629.6149, 3.6072264, 76.628044 - 0.1), new Vector3(629.6149, 3.6072264, 76.628044 - 1.1), new Vector3(), 0.5f, new Color(63, 103, 145), false, 0);
                NAPI.Marker.CreateMarker(30, new Vector3(624.4518, -3.4003117, 76.628136 - 0.1), new Vector3(624.4518, -3.4003117, 76.628136 - 1.1), new Vector3(), 0.5f, new Color(63, 103, 145), false, 0);
                NAPI.Marker.CreateMarker(30, new Vector3(597.40106, -33.440994, 70.628044 - 0.1), new Vector3(597.40106, -33.440994, 70.628044 - 1.1), new Vector3(), 0.5f, new Color(63, 103, 145), false, 0);
                NAPI.Marker.CreateMarker(29, new Vector3(606.85767, -4.1742187, 82.62812 - 0.1), new Vector3(606.85767, -4.1742187, 82.62812 - 1.1), new Vector3(), 0.5f, new Color(63, 103, 145), false, 0);
                NAPI.Marker.CreateMarker(2, new Vector3(604.84576, 5.519227, 97.87246 - 0.1), new Vector3(604.84576, 5.519227, 97.87246 - 1.1), new Vector3(), 0.5f, new Color(63, 103, 145), false, 0);
                NAPI.Marker.CreateMarker(2, new Vector3(565.7387, 4.9065294, 103.233215 - 0.1), new Vector3(565.7387, 4.9065294, 103.233215 - 1.1), new Vector3(), 0.5f, new Color(63, 103, 145), false, 0);
                //LSRC
                NAPI.TextLabel.CreateTextLabel("~w~Auslieferungspunkt", new Vector3(-687.32886, 338.13617, 78.118355 + 0.85), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Lager\n~w~Benutze Taste ~b~[F]~w~ um auf diese zuzugreifen!", new Vector3(-679.80237, 329.18146, 88.017006 + 0.85), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Apotheker\n~w~Benutze Taste ~b~[F]~w~ um mit Ihm zu interagieren!", new Vector3(-665.72156, 322.29745, 83.08319 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Willkommen im Los Santos Rescue-Center!", new Vector3(-680.49585, 326.75024, 83.08318 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Klingel\n~w~Benutze Taste ~b~[E]~w~ um zu klingeln!", new Vector3(-676.01, 325.56766, 83.08313 + 0.5), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Fuhrparkmeister Adam\n~w~Benutze Taste ~b~[F4]~w~ um mit Ihm zu komunizieren!", new Vector3(-676.8967, 336.86328, 78.11836 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Fuhrparkmeister Joshi\n~w~Benutze Taste ~b~[F4]~w~ um mit Ihm zu komunizieren!", new Vector3(-651.331, 328.43048, 140.14816 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Snackpoint\n~w~Benutze Taste ~b~[F]~w~ um dir ein Snack zu kaufen!", new Vector3(-691.39923, 322.45074, 83.08313 + 0.85), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Snackpoint\n~w~Benutze Taste ~b~[F]~w~ um dir ein Snack zu kaufen!", new Vector3(-667.6538, 342.01093, 83.08318 + 0.85), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Umkleidekabine\n~w~Benutze Taste ~b~[F]~w~ um deinen Dienst zu beginnen/beenden!", new Vector3(-663.262, 321.58627, 92.74433 + 0.85), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Rollstuhl\n~w~Benutze Taste ~b~[F]~w~ um dir einen Rollstuhl auszuleihen!", new Vector3(-682.462, 319.9259, 83.083145 + 0.8), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Fahrstuhl\n~w~Benutze Taste ~b~[F]~w~ um diesen zu benutzen!", new Vector3(-664.159, 328.23718, 83.08322 + 0.4), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Fahrstuhl\n~w~Benutze Taste ~b~[F]~w~ um diesen zu benutzen!", new Vector3(-664.0749, 328.1962, 88.01673 + 0.4), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Fahrstuhl\n~w~Benutze Taste ~b~[F]~w~ um diesen zu benutzen!", new Vector3(-664.0938, 328.36017, 92.7444 + 0.4), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Fahrstuhl\n~w~Benutze Taste ~b~[F]~w~ um diesen zu benutzen!", new Vector3(-664.0335, 328.23676, 78.12267 + 0.4), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Fahrstuhl\n~w~Benutze Taste ~b~[F]~w~ um diesen zu benutzen!", new Vector3(-664.07245, 328.3244, 140.12306 + 0.4), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.Marker.CreateMarker(29, new Vector3(-691.39923, 322.45074, 83.08313 - 0.1), new Vector3(-691.39923, 322.45074, 83.08313 - 1.1), new Vector3(), 0.5f, new Color(63, 103, 145), false, 0);
                NAPI.Marker.CreateMarker(29, new Vector3(-667.6538, 342.01093, 83.08318 - 0.1), new Vector3(-667.6538, 342.01093, 83.08318 - 1.1), new Vector3(), 0.5f, new Color(63, 103, 145), false, 0);
                NAPI.Marker.CreateMarker(30, new Vector3(-679.80237, 329.18146, 88.017006 - 0.1), new Vector3(-667.6538, 342.01093, 83.08318 - 1.1), new Vector3(), 0.5f, new Color(63, 103, 145), false, 0);
                NAPI.Marker.CreateMarker(30, new Vector3(-687.32886, 338.13617, 78.118355 - 0.1), new Vector3(-687.32886, 338.13617, 78.118355 - 1.1), new Vector3(), 0.5f, new Color(63, 103, 145), false, 0);
                NAPI.Marker.CreateMarker(30, new Vector3(-663.262, 321.58627, 92.74433 - 0.1), new Vector3(-663.262, 321.58627, 92.74433 - 1.1), new Vector3(), 0.5f, new Color(63, 103, 145), false, 0);
                //ACLS
                NAPI.TextLabel.CreateTextLabel("~b~Lager\n~w~Benutze Taste ~b~[F]~w~ um auf diese zuzugreifen!", new Vector3(-350.75766, -155.40205, 39.013554 + 0.85), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.Marker.CreateMarker(30, new Vector3(-350.75766, -155.40205, 39.013554 - 0.1), new Vector3(-350.75766, -155.40205, 39.013554 - 1.1), new Vector3(), 0.5f, new Color(63, 103, 145), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Umkleidekabine\n~w~Benutze Taste ~b~[F]~w~ um deinen Dienst zu beginnen/beenden!", new Vector3(-340.87454, -161.04536, 44.58743 + 0.85), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.Marker.CreateMarker(30, new Vector3(-340.87454, -161.04536, 44.58743 - 0.1), new Vector3(-340.87454, -161.04536, 44.58743 - 1.1), new Vector3(), 0.5f, new Color(63, 103, 145), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Fuhrparkmeister Hermann\n~w~Benutze Taste ~b~[F4]~w~ um mit Ihm zu komunizieren!", new Vector3(-333.11682, -146.52608, 60.445873 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Fuhrparkmeister Werner\n~w~Benutze Taste ~b~[F4]~w~ um mit Ihm zu komunizieren!", new Vector3(-354.90842, -166.38257, 39.015373 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Verwahrplatzbeauftragter Lukas\n~w~Benutze Taste ~b~[F4]~w~ um mit Ihm zu komunizieren!", new Vector3(408.9756, -1622.715, 29.291948 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                //GOV
                NAPI.TextLabel.CreateTextLabel("~b~Allgemeine Verwaltung\n~w~Benutze Taste ~b~[F]~w~ um die allgemeine Verwaltung zu öffnen!", new Vector3(-585.1373, -211.43108, 42.836597 + 0.85), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.Marker.CreateMarker(30, new Vector3(-585.1373, -211.43108, 42.836597 - 0.1), new Vector3(-585.1373, -211.43108, 42.836597 - 1.1), new Vector3(), 0.5f, new Color(63, 103, 145), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Dienstverwaltung\n~w~Benutze Taste ~b~[F]~w~ um deinen Dienst zu beginnen!", new Vector3(-553.10834, -182.35042, 38.43551 + 0.85), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.Marker.CreateMarker(30, new Vector3(-553.10834, -182.35042, 38.43551 - 0.1), new Vector3(-553.10834, -182.35042, 38.43551 - 1.1), new Vector3(), 0.5f, new Color(63, 103, 145), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Fuhrparkmeister Venthay\n~w~Benutze Taste ~b~[F4]~w~ um mit Ihm zu komunizieren!", new Vector3(-506.12866, -199.77043, 34.215195 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                //Zapfsäulen
                for (int i = 0; i < Helper.fuelPositions.Length; i++)
                {
                    NAPI.TextLabel.CreateTextLabel("~b~Zapfsäule\n~w~Benutze Taste ~b~[E]~w~ um dein Fahrzeug zu betanken!", Helper.fuelPositions[i], 5.5f, 0.5f, 4, new Color(255, 255, 255));
                }
                //24/7
                NAPI.TextLabel.CreateTextLabel("~b~24/7\n~w~Benutze Taste ~b~[F]~w~ um mit dem Verkäufer zu interagieren!", new Vector3(1164.9735, -324.46362, 69.20506 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~24/7\n~w~Benutze Taste ~b~[F]~w~ um mit der Verkäuferin zu interagieren!", new Vector3(-47.60335, -1758.9049, 29.420994 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~24/7\n~w~Benutze Taste ~b~[F]~w~ um mit der Verkäuferin zu interagieren!", new Vector3(2555.3213, 380.8955, 108.62296 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~24/7\n~w~Benutze Taste ~b~[F]~w~ um mit dem Verkäufer zu interagieren!", new Vector3(373.00906, 328.21713, 103.56637 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~24/7\n~w~Benutze Taste ~b~[F]~w~ um mit dem Verkäufer zu interagieren!", new Vector3(-3244.1152, 1000.16394, 12.830706 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~24/7\n~w~Benutze Taste ~b~[F]~w~ um mit dem Verkäufer zu interagieren!", new Vector3(-549.2832, 2669.496, 42.156525 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~24/7\n~w~Benutze Taste ~b~[F]~w~ um mit dem Verkäufer zu interagieren!", new Vector3(-1221.5775, -908.0929, 12.326356 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~24/7\n~w~Benutze Taste ~b~[F]~w~ um mit dem Verkäufer zu interagieren!", new Vector3(1728.6013, 6416.684, 35.037224 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~24/7\n~w~Benutze Taste ~b~[F]~w~ um mit dem Verkäufer zu interagieren!", new Vector3(1134.2794, -982.855, 46.415848 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~24/7\n~w~Benutze Taste ~b~[F]~w~ um mit dem Verkäufer zu interagieren!", new Vector3(1959.2566, 3741.4988, 32.343742 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~24/7\n~w~Benutze Taste ~b~[F]~w~ um mit dem Verkäufer zu interagieren!", new Vector3(-706.1636, -914.2992, 19.215591 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                //Ammunation
                NAPI.TextLabel.CreateTextLabel("~b~Ammunation\n~w~Benutze Taste ~b~[F]~w~ um mit dem Verkäufer zu interagieren!", new Vector3(810.24054, -2158.9868, 29.618994 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Ammunation\n~w~Benutze Taste ~b~[F]~w~ um mit dem Verkäufer zu interagieren!", new Vector3(842.216, -1035.252, 28.194868 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Ammunation\n~w~Benutze Taste ~b~[F]~w~ um mit dem Verkäufer zu interagieren!", new Vector3(22.616953, -1105.5131, 29.797026 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Ammunation\n~w~Benutze Taste ~b~[F]~w~ um mit dem Verkäufer zu interagieren!", new Vector3(-1304.1355, -394.63843, 36.69577 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Ammunation\n~w~Benutze Taste ~b~[F]~w~ um mit dem Verkäufer zu interagieren!", new Vector3(-331.62704, 6084.902, 31.45477 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                //Friseur
                NAPI.TextLabel.CreateTextLabel("~b~Barber Shop\n~w~Benutze Taste ~b~[F]~w~ um mit dem Barbier zu interagieren!", new Vector3(134.57858, -1707.9354, 29.291605 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Kosmetikerin & Friseurin Raima\n~w~Benutze Taste ~b~[F]~w~ um mit Raima zu interagieren!", new Vector3(-822.01215, -183.46446, 37.5689 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                //Juwelier
                NAPI.TextLabel.CreateTextLabel("~b~Juwelier Abu\n~w~Benutze Taste ~b~[F]~w~ um mit Abu zu interagieren!", new Vector3(-622.2842, -229.88474, 38.057053 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                //Tattooladen
                NAPI.TextLabel.CreateTextLabel("~b~Tattooladen\n~w~Benutze Taste ~b~[F]~w~ um mit dem Tätowierer zu interagieren!", new Vector3(1324.506, -1650.2559, 52.275093 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Tattooladen\n~w~Benutze Taste ~b~[F]~w~ um mit dem Tätowierer zu interagieren!", new Vector3(319.85056, 180.91638, 103.58651 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                //Cardealer
                NAPI.TextLabel.CreateTextLabel("~b~Fahrzeugverkäufer\n~w~Benutze Taste ~b~[F]~w~ um mit dem Verkäufer zu interagieren!", new Vector3(117.880554, -139.82031, 54.85013 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Fahrzeugverkäufer\n~w~Benutze Taste ~b~[F]~w~ um mit dem Verkäufer zu interagieren!", new Vector3(280.92404, -1164.9056, 29.27299 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Fahrzeugverkäuferin\n~w~Benutze Taste ~b~[F]~w~ um mit der Verkäuferin zu interagieren!", new Vector3(-31.012304, -1106.4995, 26.422335 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Fahrzeugverkäuferin\n~w~Benutze Taste ~b~[F]~w~ um mit der Verkäuferin zu interagieren!", new Vector3(-53.97707, 68.108345, 71.95995 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Fahrzeugverkäufer\n~w~Benutze Taste ~b~[F]~w~ um mit dem Verkäufer zu interagieren!", new Vector3(1231.8083, 2713.3254, 38.005795 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Fahrzeugverkäufer\n~w~Benutze Taste ~b~[F]~w~ um mit dem Verkäufer zu interagieren!", new Vector3(-17.720493, -1651.1118, 29.52027 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), true, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Fahrzeugverkäufer\n~w~Benutze Taste ~b~[F]~w~ um mit dem Verkäufer zu interagieren!", new Vector3(671.6452, -2672.725, 6.287953 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Fahrzeugverkäufer\n~w~Benutze Taste ~b~[F]~w~ um mit dem Verkäufer zu interagieren!", new Vector3(-211.62364, 6218.96, 31.491283 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Fahrradhändler\n~w~Benutze Taste ~b~[F]~w~ um mit dem Verkäufer zu interagieren!", new Vector3(-1136.9968, -199.24275, 37.96 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Bootshändler\n~w~Benutze Taste ~b~[F]~w~ um mit dem Verkäufer zu interagieren!", new Vector3(-764.9401, -1316.4093, 5.150273 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Flugzeughändler\n~w~Benutze Taste ~b~[F]~w~ um mit dem Verkäufer zu interagieren!", new Vector3(-941.12964, -2954.1677, 13.945079 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                //Garage
                NAPI.TextLabel.CreateTextLabel("~b~Garagenverwalter\n~w~Benutze Taste ~b~[F4]~w~ um mit dem Verwalter zu interagieren!", new Vector3(214.0354, -808.4536, 31.014893 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Bootsgaragenverwalter\n~w~Benutze Taste ~b~[F4]~w~ um mit dem Verwalter zu interagieren!", new Vector3(-918.12463, -1364.8055, 1.59539 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Fluggaragenverwalter\n~w~Benutze Taste ~b~[F4]~w~ um mit dem Verwalter zu interagieren!", new Vector3(-950.207, -3056.4653, 13.945073 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                NAPI.TextLabel.CreateTextLabel("~b~Garagenverwalter\n~w~Benutze Taste ~b~[F4]~w~ um mit dem Verwalter zu interagieren!", new Vector3(67.724884, 12.313736, 69.21442 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                //Achmed
                NAPI.TextLabel.CreateTextLabel("~b~Achmed der Fahrzeugexporteur\n~w~Benutze Taste ~b~[E]~w~ um mit Achmed interagieren!", new Vector3(801.72723, -2924.453, 5.9188385 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                //Materialversteck
                NAPI.TextLabel.CreateTextLabel("~b~Materialenversteck\n~w~Benutze Taste ~b~[F]~w~ um Materialien rauszunehmen!", new Vector3(-2070.9304, -1020.88715, 5.884131 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                //Haustierverkauf
                NAPI.TextLabel.CreateTextLabel("~b~Haustierverkäuferin Julia\n~w~Benutze Taste ~b~[F]~w~ um mit Ihr zu interagieren!", new Vector3(-1326.8887, -268.52347, 41.652397 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                //Colshapes
                ammuCol = NAPI.ColShape.CreatCircleColShape(7.2232156f, -1098.8546f, 0.85f, 0);
                //Timer
                halfOneMinuteTimer = new Timer(OnHalfOneMinuteTimer, null, 30000, 30000);
                secondTimer = new Timer(OnSecondTimer, null, 1000, 1000);
                saveTimer = new Timer(OnServerSave, null, saveMinutes * 60000, saveMinutes * 60000);
                //Vehicles
                //Tutorial Taxi
                Cars.createNewCar("taxi", new Vector3(-1375.683, -817.50415f, 19.111004), -138f, 42, 42, "Taxi", "Tutorial", true, false, false, 1);
                //Rollerverleih
                Cars.createNewCar("faggio", new Vector3(-522.10754, -258.09152, 35.10415), -32.411964f, 1, 1, "LS-S-98", "Rollerverleih", true, false, false, 0);
                Cars.createNewCar("faggio", new Vector3(477.5916, -1861.3844, 27.042665), 3.3062532f, 66, 66, "LS-S-99", "Rollerverleih", true, false, false, 0);
                //Spediteur Job
                Cars.createNewCar("trailers", new Vector3(-1062.9143, -2009.853, 13.228431), 136.42274f, 1, 1, "LS-S-151", "Spedition", true, false, false, 0);
                Cars.createNewCar("trailers2", new Vector3(-1059.2396, -2013.5122, 13.228942), 136.42274f, 1, 1, "LS-S-152", "Spedition", true, false, false, 0);
                Cars.createNewCar("trailers3", new Vector3(-1051.9065, -2020.8984, 13.227922), 136.42274f, 1, 1, "LS-S-153", "Spedition", true, false, false, 0);
                Cars.createNewCar("trailers4", new Vector3(-1048.1555, -2024.7548, 13.2291765), 136.42274f, 1, 1, "LS-S-154", "Spedition", true, false, false, 0);
                Cars.createNewCar("tanker", new Vector3(-1056.9392, -2086.9587, 13.380569), -46.03f, 1, 1, "LS-S-155", "Spedition", true, false, false, 0);
                //Autohaus
                //Luxus
                Cars.createNewCar("rmodbugatti", new Vector3(126.78131, -156.92279, 54.070538 + 0.020), 146.60889f, 143, 143, "Autohaus", "Autohaus", true, false, false, 0);
                Cars.createNewCar("infernus", new Vector3(134.43681, -160.00989, 54.068844 + 0.175), 152.11954f, 145, 145, "Autohaus", "Autohaus", true, false, false, 0);
                Cars.createNewCar("gt63samg", new Vector3(142.07198, -162.87733, 54.06755 + 0.125), -178.45845f, 146, 146, "Autohaus", "Autohaus", true, false, false, 0);
                Cars.createNewCar("f812", new Vector3(118.04321, -154.3204, 60.041595 + 0.0825), 140.27165f, 150, 150, "Autohaus", "Autohaus", true, false, false, 0);
                Cars.createNewCar("torero2", new Vector3(114.55072, -146.79385, 60.04179 + 0.0845), 136.99521f, 157, 157, "Autohaus", "Autohaus", true, false, false, 0);
                //Motorräder
                Cars.createNewCar("bati2", new Vector3(294.4864, -1157.8894, 28.618795 + 0.024), 152.23343f, 5, 5, "Autohaus", "Autohaus", true, false, false, 0);
                Cars.createNewCar("daemon", new Vector3(282.5216, -1151.017, 28.620173 + 0.17), -154.56293f, 63, 63, "Autohaus", "Autohaus", true, false, false, 0);
                Cars.createNewCar("faggio3", new Vector3(292.9171, -1151.005, 28.619864 + 0.18), 176.56482f, 115, 115, "Autohaus", "Autohaus", true, false, false, 0);
                Cars.createNewCar("nrg500sa", new Vector3(297.2131, -1150.8544, 28.61993 + 0.023), 178.37889f, 108, 108, "Autohaus", "Autohaus", true, false, false, 0);
                Cars.createNewCar("hakuchou", new Vector3(302.27847, -1154.08, 28.619078 + 0.17), 91.81911f, 76, 76, "Autohaus", "Autohaus", true, false, false, 0);
                Cars.createNewCar("hexer", new Vector3(302.33344, -1158.6295, 28.618572 + 0.15), 94.89825f, 1, 1, "Autohaus", "Autohaus", true, false, false, 0);
                Cars.createNewCar("manchez", new Vector3(300.2005, -1166.3243, 28.6251 + 0.15), -0.41378784f, 131, 131, "Autohaus", "Autohaus", true, false, false, 0);
                Cars.createNewCar("wolfsbane", new Vector3(295.43637, -1166.4766, 28.61888 + 0.15), -1.7155194f, 99, 99, "Autohaus", "Autohaus", true, false, false, 0);
                Cars.createNewCar("sanchez", new Vector3(291.29916, -1166.2517, 28.61962 + 0.026), -1.7462608f, 62, 62, "Autohaus", "Autohaus", true, false, false, 0);
                //Sport
                Cars.createNewCar("turismo2", new Vector3(-46.58986, -1101.4457, 26.041613 + 0.015), 101.608635f, 153, 153, "Autohaus", "Autohaus", true, false, false, 0);
                Cars.createNewCar("jester2", new Vector3(-46.86995, -1093.5579, 25.741695 - 0.08), -161.48355f, 26, 26, "Autohaus", "Autohaus", true, false, false, 0);
                Cars.createNewCar("cypher", new Vector3(-40.08569, -1095.5552, 25.7762 + 0.023), -166.17195f, 63, 63, "Autohaus", "Autohaus", true, false, false, 0);
                Cars.createNewCar("locust", new Vector3(-56.579407, -1097.5371, 25.683722 + 0.15), 110.54099f, 117, 117, "Autohaus", "Autohaus", true, false, false, 0);
                //SUV
                Cars.createNewCar("toros", new Vector3(-75.717545, 74.41533, 71.70417 - 0.12), -99.167114f, 161, 161, "Autohaus", "Autohaus", true, false, false, 0);
                //Mosley
                Cars.createNewCar("felon", new Vector3(-43.40239, -1666.9574, 29.210888 + 0.018), -106.65183f, 161, 161, "Autohaus", "Autohaus", true, false, false, 0);
                Cars.createNewCar("zion2", new Vector3(-32.864532, -1654.0728, 28.983671 - 0.07), -104.15185f, 55, 55, "Autohaus", "Autohaus", true, false, false, 0);
                Cars.createNewCar("issi5", new Vector3(-25.3515, -1645.5342, 29.212292 - 0.10), -128.7981f, 11, 11, "Autohaus", "Autohaus", true, false, false, 0);
                Cars.createNewCar("rhapsody", new Vector3(-18.393559, -1657.6031, 29.21066 + 0.020), 21.401958f, 33, 33, "Autohaus", "Autohaus", true, false, false, 0);
                //Lowrider
                Cars.createNewCar("chino2", new Vector3(1241.536, 2706.9846, 37.453217 - 0.12), 123.738266f, 99, 99, "Autohaus", "Autohaus", true, false, false, 0);
                Cars.createNewCar("dukes", new Vector3(1237.8563, 2712.6086, 37.50494 + 0.045), 148.0699f, 66, 66, "Autohaus", "Autohaus", true, false, false, 0);
                Cars.createNewCar("gauntlet3", new Vector3(1228.4575, 2710.3582, 37.439392 - 0.15), -124.813095f, 111, 111, "Autohaus", "Autohaus", true, false, false, 0);
                Cars.createNewCar("ruiner2", new Vector3(1227.2529, 2706.166, 37.367336 + 0.30), -118.72846f, 41, 41, "Autohaus", "Autohaus", true, false, false, 0);
                //Offroad
                Cars.createNewCar("bifta", new Vector3(-205.33284, 6222.247, 31.022148 + 0.015), -135.77289f, 1, 1, "Autohaus", "Autohaus", true, false, false, 0);
                Cars.createNewCar("brawler", new Vector3(-200.46141, 6226.8247, 31.026072 + 0.27), -134.48964f, 113, 113, "Autohaus", "Autohaus", true, false, false, 0);
                Cars.createNewCar("bfinjection", new Vector3(-195.94846, 6231.1543, 31.028376 + 0.025), -138.79005f, 22, 22, "Autohaus", "Autohaus", true, false, false, 0);
                //Truck
                Cars.createNewCar("burrito2", new Vector3(665.1115, -2659.8381, 6.1021323 - 0.10), 147.22615f, 5, 5, "Autohaus", "Autohaus", true, false, false, 0);
                //Fahrrad
                Cars.createNewCar("tribike", new Vector3(-1143.4869, -201.54604, 37.571514 + 0.015), -76.24602f, 176, 176, "Autohaus", "Autohaus", true, false, false, 0);
                //Peds
                //24/7 - Tankstelle
                NAPI.Ped.CreatePed(NAPI.Util.GetHashKey("a_m_y_beachvesp_02"), new Vector3(1164.9735, -324.46362, 69.20506), 91.9143f, true, true, true, false, 0);
                NAPI.Ped.CreatePed(NAPI.Util.GetHashKey("csb_anita"), new Vector3(-47.60335, -1758.9049, 29.420994), 42.221672f, true, true, true, false, 0);
                NAPI.Ped.CreatePed(NAPI.Util.GetHashKey("cs_ashley"), new Vector3(2555.3213, 380.8955, 108.62296), -8.674162f, true, true, true, false, 0);
                NAPI.Ped.CreatePed(NAPI.Util.GetHashKey("ig_money"), new Vector3(373.00906, 328.21713, 103.56637), -116.7741f, true, true, true, false, 0);
                NAPI.Ped.CreatePed(NAPI.Util.GetHashKey("u_m_y_chip"), new Vector3(-3244.1152, 1000.16394, 12.830706), -7.986622f, true, true, true, false, 0);
                NAPI.Ped.CreatePed(NAPI.Util.GetHashKey("cs_dale"), new Vector3(-549.2832, 2669.496, 42.156525), 91.33562f, true, true, true, false, 0);
                NAPI.Ped.CreatePed(NAPI.Util.GetHashKey("a_m_y_cyclist_01"), new Vector3(549.2482, 2669.6978, 42.156494), 92.36693f, true, true, true, false, 0);
                NAPI.Ped.CreatePed(NAPI.Util.GetHashKey("mp_m_famdd_01"), new Vector3(-1221.5775, -908.0929, 12.326356), 34.045567f, true, true, true, false, 0);
                NAPI.Ped.CreatePed(NAPI.Util.GetHashKey("a_m_m_bevhills_02"), new Vector3(1728.6013, 6416.684, 35.037224), -118.13775f, true, true, true, false, 0);
                NAPI.Ped.CreatePed(NAPI.Util.GetHashKey("ig_car3guy2"), new Vector3(1134.2794, -982.855, 46.415848), -90.767555f, true, true, true, false, 0);
                NAPI.Ped.CreatePed(NAPI.Util.GetHashKey("u_m_m_doa_01"), new Vector3(1959.2566, 3741.4988, 32.343742), -66.22777f, true, true, true, false, 0);
                NAPI.Ped.CreatePed(NAPI.Util.GetHashKey("a_m_y_indian_01"), new Vector3(-706.1636, -914.2992, 19.215591), 83.824135f, true, true, true, false, 0);
                //Kleidungsladen
                NAPI.Ped.CreatePed(NAPI.Util.GetHashKey("cs_ashley"), new Vector3(77.48367, -1387.6338, 29.376139), 172.43777f, true, true, true, false, 0);
                NAPI.Ped.CreatePed(NAPI.Util.GetHashKey("csb_anita"), new Vector3(-1194.0197, -767.0693, 17.316254), -150.87082f, true, true, true, false, 0);
                NAPI.Ped.CreatePed(NAPI.Util.GetHashKey("a_f_y_bevhills_01"), new Vector3(-709.1608, -151.32368, 37.41514), 110.45521f, true, true, true, false, 0);
                NAPI.Ped.CreatePed(NAPI.Util.GetHashKey("a_f_y_business_04"), new Vector3(-3169.3755, 1043.192, 20.863214), 59.39894f, true, true, true, false, 0);
                //Juwelier
                NAPI.Ped.CreatePed(NAPI.Util.GetHashKey("g_m_m_armboss_01"), new Vector3(-622.2842, -229.88474, 38.057053), -54.717052f, true, true, true, false, 0);
                //Tattoo-Laden
                NAPI.Ped.CreatePed(NAPI.Util.GetHashKey("u_m_y_tattoo_01"), new Vector3(1324.506, -1650.2559, 52.275093), 127.368935f, true, true, true, false, 0);
                NAPI.Ped.CreatePed(NAPI.Util.GetHashKey("cs_terry"), new Vector3(319.85056, 180.91638, 103.58651), -108.334435f, true, true, true, false, 0);
                //Barber-Laden
                NAPI.Ped.CreatePed(NAPI.Util.GetHashKey("a_m_m_indian_01"), new Vector3(134.57858, -1707.9354, 29.291605), 131.56297f, true, true, true, false, 0);
                NAPI.Ped.CreatePed(NAPI.Util.GetHashKey("u_f_y_bikerchic"), new Vector3(-822.01215, -183.46446, 37.5689), -154.59505f, true, true, true, false, 0);
                //Ammunation
                NAPI.Ped.CreatePed(NAPI.Util.GetHashKey("s_m_y_ammucity_01"), new Vector3(810.24054, -2158.9868, 29.618994), -4.096228f, true, true, true, false, 0);
                NAPI.Ped.CreatePed(NAPI.Util.GetHashKey("s_m_y_ammucity_01"), new Vector3(842.216, -1035.252, 28.194868), -6.233367f, true, true, true, false, 0);
                NAPI.Ped.CreatePed(NAPI.Util.GetHashKey("s_m_m_ammucountry"), new Vector3(22.616953, -1105.5131, 29.797026), 152.2582f, true, true, true, false, 0);
                NAPI.Ped.CreatePed(NAPI.Util.GetHashKey("s_m_m_ammucountry"), new Vector3(-1304.1355, -394.63843, 36.69577), 67.99332f, true, true, true, false, 0);
                NAPI.Ped.CreatePed(NAPI.Util.GetHashKey("s_m_y_ammucity_01"), new Vector3(-331.62704, 6084.902, 31.45477), -144.1672f, true, true, true, false, 0);
                NAPI.Ped.CreatePed(NAPI.Util.GetHashKey("u_m_m_aldinapoli"), new Vector3(12.069575, -1106.9514, 29.797009), -25.61079f, true, true, true, false, 0);
                //Bank
                NAPI.Ped.CreatePed(NAPI.Util.GetHashKey("u_f_y_princess"), new Vector3(243.6488, 226.25098, 106.28752), 153.68489f, true, true, true, false, 0);
                NAPI.Ped.CreatePed(NAPI.Util.GetHashKey("s_m_m_pilot_01"), new Vector3(-1211.9916, -331.97122, 37.78094), 20.879f, true, true, true, false, 0);
                NAPI.Ped.CreatePed(NAPI.Util.GetHashKey("u_f_m_miranda"), new Vector3(-351.36057, -51.23828, 49.036484), -26.395742f, true, true, true, false, 0);
                NAPI.Ped.CreatePed(NAPI.Util.GetHashKey("cs_josh"), new Vector3(313.77863, -280.52676, 54.164692), -19.961432f, true, true, true, false, 0);
                NAPI.Ped.CreatePed(NAPI.Util.GetHashKey("ig_janet"), new Vector3(149.40121, -1042.143, 29.367987), -25.037832f, true, true, true, false, 0);
                NAPI.Ped.CreatePed(NAPI.Util.GetHashKey("ig_manuel"), new Vector3(-2961.169, 482.9263, 15.697004), 84.36845f, true, true, true, false, 0);
                NAPI.Ped.CreatePed(NAPI.Util.GetHashKey("cs_gurk"), new Vector3(1175.0502, 2708.2053, 38.087936), 175.24529f, true, true, true, false, 0);
                //Haustierverkauf
                NAPI.Ped.CreatePed(NAPI.Util.GetHashKey("g_m_m_chigoon_02"), new Vector3(-1326.8887, -268.52347, 41.652397), 116.49419f, true, true, true, false, 0);

            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[LoadStuff]: " + e.ToString());
            }
        }

        //Colshapes
        [ServerEvent(Event.PlayerEnterColshape)]
        public void OnPlayerEnterColshape(GTANetworkAPI.ColShape shape, Player player)
        {
            try
            {
                if (player.GetOwnSharedData<bool>("Player:Spawned") == true && player.GetSharedData<bool>("Player:Death") == false)
                {
                    TempData tempData = Helper.GetCharacterTempData(player);
                    Character character = Helper.GetCharacterData(player);
                    //Blitzer
                    if (player.IsInVehicle && player.VehicleSeat == (int)VehicleSeat.Driver && Helper.blitzerList.Count > 0 && tempData.adminduty == false)
                    {
                        if (player.HasData("Player:WireCooldown"))
                        {
                            if (Helper.UnixTimestamp() < player.GetData<int>("Player:WireCooldown"))
                            {
                                return;
                            }
                            player.ResetData("Player:Wirecooldown");
                        }
                        VehicleData vehicleData = DealerShipController.GetVehicleDataByVehicle(player.Vehicle);
                        if (vehicleData != null && vehicleData.owner != "faction-1")
                        {
                            foreach (Blitzer blitzer in Helper.blitzerList)
                            {
                                if (blitzer != null && blitzer.colshape != null && blitzer.colshape == shape)
                                {
                                    player.SetData<int>("Player:WireCooldown", Helper.UnixTimestamp() + (10));
                                    player.TriggerEvent("Client:CheckSpeed", blitzer.maxspeed);
                                    return;
                                }
                            }
                        }
                    }
                    //Spikestrips
                    if (player.IsInVehicle && Helper.spikeStripList.Count > 0)
                    {
                        foreach (SpikeStrip spikeStrip in Helper.spikeStripList)
                        {
                            if (spikeStrip != null && spikeStrip.colshape != null && spikeStrip.colshape == shape && tempData.adminduty == false)
                            {
                                string[] vehicleArray = new string[7];
                                vehicleArray = player.Vehicle.GetSharedData<string>("Vehicle:Sync").Split(",");
                                player.Vehicle.SetSharedData("Vehicle:Sync", $"{vehicleArray[0]},{vehicleArray[1]},{vehicleArray[2]},1,{vehicleArray[4]},{vehicleArray[5]},{vehicleArray[6]}");
                                player.TriggerEvent("Client:VehicleTyreBurst");
                                return;
                            }
                        }
                    }
                    //Feuersystem
                    if (FireController.startFire == true && FireController.fireColshape != null && shape == FireController.fireColshape && player.GetData<bool>("Player:Fire") == false)
                    {
                        int count = 0;
                        player.SetData<bool>("Player:Fire", true);
                        NAPI.ClientEvent.TriggerClientEvent(player, "Client:BlipFireLocation", FireController.firePosition.X, FireController.firePosition.Y, FireController.firePosition.Z);
                        if (FireController.fireObject == null)
                        {
                            while (count < FireController.fireObjectPositions.Length && FireController.fireObjectPositions[count] != null)
                            {
                                NAPI.ClientEvent.TriggerClientEvent(player, "Client:CheckIfReachable", FireController.fireObjectPositions[count].X, FireController.fireObjectPositions[count].Y, FireController.fireObjectPositions[count].Z, 15, true);
                                count++;
                            }
                            if (character.faction == 2)
                            {
                                NAPI.Task.Run(() =>
                                {
                                    if (FireController.fireObject == null)
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
                    //Waffenscheinsystem
                    if (tempData.jobColshape != null && shape == tempData.jobColshape && player.HasData("Player:AmmuQuiz"))
                    {
                        if (player.GetData<int>("Player:AmmuQuiz") == 2)
                        {
                            NAPI.Player.SetPlayerWeaponAmmo(player, WeaponHash.Pistol, 0);
                            NAPI.Player.RemovePlayerWeapon(player, WeaponHash.Pistol);
                            if (tempData.jobColshape != null)
                            {
                                tempData.jobColshape = null;
                            }
                            player.SetData<int>("Player:AmmuQuiz", 0);
                            player.ResetData("Player:AmmuQuiz");
                            player.TriggerEvent("Client:StopRange");
                            Helper.SendNotificationWithoutButton(player, "Praktische Waffenscheinprüfung abgebrochen!", "error", "top-left", 4000);
                            return;
                        }
                        else if (player.GetData<int>("Player:AmmuQuiz") == 4)
                        {
                            if (tempData.jobColshape != null)
                            {
                                tempData.jobColshape = null;
                            }
                            player.SetData<int>("Player:AmmuQuiz", 0);
                            player.ResetData("Player:AmmuQuiz");
                            player.TriggerEvent("Client:StopRange");
                            Helper.SendNotificationWithoutButton(player, "Vorgang abgebrochen!", "error", "top-left", 4000);
                            return;
                        }
                    }
                    //Taxi Botaufträge
                    if (tempData.jobColshape != null && tempData.order2 != null && Helper.IsATaxiDriver(player) > -1)
                    {
                        if (shape == tempData.jobColshape && (player.Vehicle.GetSharedData<String>("Vehicle:Name") == "taxi" || player.Vehicle.GetSharedData<String>("Vehicle:Name") == "tourbus"))
                        {
                            if (tempData.jobstatus == 1)
                            {
                                player.TriggerEvent("Client:CreatePed", tempData.order2.v2.X, tempData.order2.v2.Y, tempData.order2.v2.Z, 2);
                                Helper.SendNotificationWithoutButton(player, $"Fahrgast abgeholt, bringe diesen jetzt bitte zu/m/r {tempData.order2.to}!", "success", "top-left", 4000);
                                if (tempData.jobColshape != null)
                                {
                                    tempData.jobColshape.Delete();
                                    tempData.jobColshape = null;
                                }
                                tempData.jobColshape = NAPI.ColShape.CreatCircleColShape(tempData.order2.v2.X, tempData.order2.v2.Y, 4.25f, player.Dimension);
                                tempData.jobstatus = 2;
                                return;
                            }
                            else
                            {
                                if (Helper.UnixTimestamp() < player.GetData<int>("Player:BusTime"))
                                {
                                    AntiCheatController.OnCallAntiCheat(player, "Teleport to Checkpoint Cheat", "Taxifahrer", false);
                                    return;
                                }
                                player.ResetData("Player:BusTime");
                                if (tempData.jobColshape != null)
                                {
                                    tempData.jobColshape.Delete();
                                    tempData.jobColshape = null;
                                }
                                tempData.jobstatus = 0;
                                player.TriggerEvent("Client:CreatePed", tempData.order2.v1.X, tempData.order2.v1.Y, tempData.order2.v1.Z, 3);
                                if (Helper.IsATaxiDriver(player) == 2)
                                {
                                    int money = tempData.order2.money;
                                    character.nextpayday += money;
                                    Helper.SendNotificationWithoutButton(player, $"Fahrgast abgeliefert, du bekommst für deinen nächsten Gehaltscheck {money}$ gutgeschrieben!", "success", "top-left", 5500);
                                }
                                else
                                {
                                    int money = tempData.order2.money;
                                    if (character.mygroup != -1)
                                    {
                                        Groups mygroup = GroupsController.GetGroupById(character.mygroup);
                                        Bank bank = BankController.GetBankByBankNumber(mygroup.banknumber);
                                        if (bank != null)
                                        {
                                            int prov = 0;
                                            if (mygroup.provision > 0)
                                            {
                                                prov = money / 100 * mygroup.provision;
                                            }
                                            if (prov > 0 && character.defaultbank != "n/A")
                                            {
                                                Bank bank2 = BankController.GetDefaultBank(player, character.defaultbank);
                                                bank.bankvalue += money;
                                                bank.bankvalue -= prov;
                                                if (bank2 != null)
                                                {
                                                    bank2.bankvalue += prov;
                                                }
                                                Helper.SendNotificationWithoutButton(player, $"Fahrgast abgeliefert, {money}$ werden dem Konto deiner Firma gutgeschrieben. Du erhälst {prov}$ Provision!", "success", "top-left", 5500);
                                                Helper.CreateGroupMoneyLog(mygroup.id, $"{character.name} hat einen Taxiauftrag (Bot) erledigt und {money - prov}$ erwirtschaftet!");
                                            }
                                            else
                                            {
                                                bank.bankvalue += money;
                                                Helper.SendNotificationWithoutButton(player, $"Fahrgast abgeliefert, {money}$ werden dem Konto deiner Firma gutgeschrieben!", "success", "top-left", 5500);
                                                Helper.CreateGroupMoneyLog(mygroup.id, $"{character.name} hat einen Taxiauftrag (Bot) erledigt und {money}$ erwirtschaftet!");
                                            }
                                        }
                                        else
                                        {
                                            character.nextpayday += money;
                                            Helper.SendNotificationWithoutButton(player, $"Fahrgast abgeliefert, du bekommst für deinen nächsten Gehaltscheck {money}$ gutgeschrieben!", "success", "top-left", 5500);
                                        }
                                    }
                                    else
                                    {
                                        character.nextpayday += money;
                                        Helper.SendNotificationWithoutButton(player, $"Fahrgast abgeliefert, du bekommst für deinen nächsten Gehaltscheck {money}$ gutgeschrieben!", "success", "top-left", 5500);
                                    }
                                }
                                tempData.order2 = null;
                                return;
                            }
                        }
                    }
                    //Spedition Auftragsliste
                    if (tempData.jobColshape != null && tempData.order != null && Helper.IsASpediteur(player) > -1)
                    {
                        if (shape == tempData.jobColshape && Helper.IsASpeditionsVehicle(player.Vehicle))
                        {
                            SpedVehicles spedVehicles = Helper.GetSpedVehicleById(player.Vehicle.GetData<int>("Vehicle:Jobid"));
                            if (spedVehicles == null)
                            {
                                Helper.SendNotificationWithoutButton(player, "Mit diesem Fahrzeug kannst du keine Produkte kaufen/einladen!", "error");
                                return;
                            }
                            SpedOrders spedOrder = tempData.order;
                            if (tempData.jobstatus == 1)
                            {
                                Helper.SendNotificationWithoutButton(player, $"Ware abgeholt, bringe diese jetzt bitte zu/m/r {spedOrder.to}!", "success", "top-left", 4000);
                                player.TriggerEvent("Client:CreateWaypoint", spedOrder.position2.X, spedOrder.position2.Y, -1);
                                player.TriggerEvent("Client:CreateMarker", spedOrder.position2.X, spedOrder.position2.Y, spedOrder.position2.Z, 39);
                                if (tempData.jobColshape != null)
                                {
                                    tempData.jobColshape.Delete();
                                    tempData.jobColshape = null;
                                }
                                tempData.jobColshape = NAPI.ColShape.CreatCircleColShape(spedOrder.position2.X, spedOrder.position2.Y, 3.5f, player.Dimension);
                                tempData.jobstatus = 2;
                            }
                            else
                            {
                                player.TriggerEvent("Client:DeleteMarker");
                                if (tempData.jobColshape != null)
                                {
                                    tempData.jobColshape.Delete();
                                    tempData.jobColshape = null;
                                }
                                tempData.jobstatus = 0;
                                tempData.order = null;
                                if (Helper.IsASpediteur(player) == 2)
                                {
                                    int bonus = 0;
                                    bonus = (int)((character.truckerskill / 45) * 0.04);
                                    int money = (int)(spedOrder.dist * 0.325);
                                    money = money + bonus;
                                    int geb = money / 100 * 10;
                                    character.nextpayday += money;
                                    character.nextpayday -= geb;
                                    if (character.truckerskill < 225)
                                    {
                                        character.truckerskill++;
                                    }
                                    Helper.SendNotificationWithoutButton(player, $"Ware gelefert, du bekommst für deinen nächsten Gehaltscheck {money}$ abzüglich {geb}$ Gebühren gutgeschrieben!", "success", "top-left", 5500);
                                }
                                else
                                {
                                    if (character.mygroup != -1)
                                    {
                                        int money = 0;
                                        Groups mygroup = GroupsController.GetGroupById(character.mygroup);
                                        Bank bank = BankController.GetBankByBankNumber(mygroup.banknumber);
                                        if (bank != null)
                                        {
                                            int bonus = 0;
                                            bonus = (int)((character.truckerskill / 45) * 0.04);
                                            money = (int)(spedOrder.dist * 0.325);
                                            money = money + bonus;
                                            int prov = 0;
                                            if (mygroup.provision > 0)
                                            {
                                                prov = money / 100 * mygroup.provision;
                                            }
                                            if (character.truckerskill < 225)
                                            {
                                                character.truckerskill++;
                                            }
                                            if (prov > 0 && character.defaultbank != "n/A")
                                            {
                                                Bank bank2 = BankController.GetDefaultBank(player, character.defaultbank);
                                                bank.bankvalue += money;
                                                bank.bankvalue -= prov;
                                                if (bank2 != null)
                                                {
                                                    bank2.bankvalue += prov;
                                                }
                                                Helper.SendNotificationWithoutButton(player, $"Ware gelefert, {money}$ werden dem Konto deiner Firma gutgeschrieben. Du erhälst {prov}$ Provision!", "success", "top-left", 5500);
                                                Helper.CreateGroupMoneyLog(mygroup.id, $"{character.name} hat einen Auftrag erledigt und {money - prov}$ erwirtschaftet!");
                                            }
                                            else
                                            {
                                                bank.bankvalue += money;
                                                Helper.SendNotificationWithoutButton(player, $"Ware gelefert, {money}$ werden dem Konto deiner Firma gutgeschrieben!", "success", "top-left", 5500);
                                                Helper.CreateGroupMoneyLog(mygroup.id, $"{character.name} hat einen Auftrag erledigt und {money}$ erwirtschaftet!");
                                            }
                                        }
                                        else
                                        {
                                            int bonus = 0;
                                            bonus = (int)((character.truckerskill / 45) * 0.04);
                                            money = (int)(spedOrder.dist * 0.325);
                                            money = money + bonus;
                                            character.nextpayday += money;
                                            if (character.truckerskill < 225)
                                            {
                                                character.truckerskill++;
                                            }
                                            CharacterController.SaveCharacter(player);
                                            Helper.SendNotificationWithoutButton(player, $"Ware gelefert, du bekommst für deinen nächsten Gehaltscheck {money}$ gutgeschrieben!", "success", "top-left", 5500);
                                        }
                                    }
                                    else
                                    {
                                        Helper.SendNotificationWithoutButton(player, "Ungültige Interaktion!", "error", "top-left", 2550);
                                    }
                                }
                            }
                            return;
                        }
                    }
                    //Doorsystem
                    foreach (DoorsColshapes doorShapes in DoorsController.doorsColshapesList)
                    {
                        if (doorShapes != null && doorShapes.doorshape == shape)
                        {
                            DoorsController.UpdateDoor(player, DoorsController.GetDoorByID(doorShapes.doorid), true);
                            return;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnPlayerEnterColshape]: " + e.ToString());
            }
        }

        //LoadBestTorsos
        public static void LoadBestTorsos()
        {
            try
            {
                string directory = "./serverdata/besttorso/besttorso_female.json";
                if (File.Exists(directory))
                {
                    string tempWoman = File.ReadAllText(directory);
                    Helper.TorsosWoman = JObject.Parse(tempWoman);
                }
                string directory2 = "./serverdata/besttorso/besttorso_male.json";
                if (File.Exists(directory2))
                {
                    string tempMen = File.ReadAllText(directory2);
                    Helper.TorsosMen = JObject.Parse(tempMen);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[LoadBestTorsos]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:GetBestTorso")] //Besser Clientside machen
        public static void GetBestTorso(Player player, int drawable, int variation, bool show = false, bool permanent = true)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                if (character == null) return;
                JObject obj = Helper.TorsosMen;
                if (character.gender != 1)
                {
                    obj = Helper.TorsosWoman;
                }
                int draw = -1;
                int vari = 0;
                try
                {
                    draw = (int)obj[$"{drawable}"][$"{variation}"]["BestTorsoDrawable"];
                    vari = (int)obj[$"{drawable}"][$"{variation}"]["BestTorsoTexture"];
                }
                catch (Exception) { }
                if (draw == -1)
                {
                    if (show == true)
                    {
                        Helper.SendNotificationWithoutButton(player, $"Es wurde kein geeigneter Torso gefunden, bitte wähle einen manuell aus!", "success", "top-left", 2500);
                    }
                    return;
                }
                NAPI.Player.SetPlayerClothes(player, 3, draw, vari);
                if (show == true)
                {
                    Helper.SendNotificationWithoutButton(player, $"Es wurde ein geeigneter Torso ({draw}/{vari}) gesetzt!", "success", "top-left", 2500);
                }
                if (permanent == true)
                {
                    JObject obj2 = null;
                    if (character.factionduty == false)
                    {
                        obj2 = JObject.Parse(character.json);
                    }
                    else
                    {
                        obj2 = JObject.Parse(character.dutyjson);
                    }
                    obj2["clothing"][1] = draw;
                    obj2["clothingColor"][1] = vari;
                    character.json = NAPI.Util.ToJson(obj2);
                }
                player.SetData<int>("Player:TorsoCD", Helper.UnixTimestamp() + (2));
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetBestTorso]: " + e.ToString());
            }
        }

        //LoadEUPOutfits
        public static void LoadEUPOutfits(bool insert = true, bool delete = false, bool log = false)
        {
            try
            {
                String concat = "";
                MySqlCommand command = null;
                if (delete == true)
                {
                    command = General.Connection.CreateCommand();
                    command.CommandText = "DELETE FROM outfits WHERE owner = 'EUP'";
                    command.ExecuteNonQuery();
                }

                if (insert == true)
                {
                    command = General.Connection.CreateCommand();
                    command.CommandText = "SELECT COUNT(*) FROM outfits";
                    Int64 outfitCount = (Int64)command.ExecuteScalar();

                    if (outfitCount >= 890) return;

                    String line;
                    String json1 = "[";
                    String json2 = "[";
                    String jsonTemp = "";
                    String headline = "Male LSPD Class A";
                    String category1 = "LSPD";
                    String category2 = "Patrol Division";
                    String gender = "male";
                    int count = 0;
                    PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                    StreamReader sr = new StreamReader(@"./serverdata/outfits/eup.txt");
                    line = sr.ReadLine();
                    if (sr != null && line != null && line != "//EUP Menu by PieRGud | Edit by Nemesus.de")
                    {
                        Helper.ConsoleLog("error", $"[LoadEUPOutfits]: Ungültige EUP Liste gefunden oder keine EUP Liste gefunden!");
                        return;
                    }
                    while (!sr.EndOfStream)
                    {
                        line = sr.ReadLine();
                        if (line.StartsWith("[Male") || line.StartsWith("[Female"))
                        {
                            count = 0;
                            json1 = "[";
                            json2 = "[";
                            jsonTemp = "";
                            category1 = "n/A";
                            category2 = "n/A";
                            headline = line;
                            headline = headline.Replace("[", "");
                            headline = headline.Replace("]", "");
                            if(line.StartsWith("[Male"))
                            {
                                gender = "male";
                            }
                            else
                            {
                                gender = "female";
                            }
                        }
                        else
                        {
                            if (!String.IsNullOrWhiteSpace(line))
                            {
                                if (line.Contains(":"))
                                {
                                    if (line.Contains("UnderCoat") && gender == "male" && (category1 == "LSSD" || category1 == "LSSD" || category1 == "SAHP" || category1 == "LSFD" || category1 == "Medical Services" || category1 == "SanFire"))
                                    {
                                        var temp = line.Split("=")[1];
                                        var temp2 = temp.Split(":")[0];
                                        if (Convert.ToInt32(temp2) > 0)
                                        {
                                            concat = concat + $"{temp2},";
                                        }
                                    }
                                    jsonTemp = line.Split("=")[1];
                                    if (json1 == "[")
                                    {
                                        json1 = json1 + $"{jsonTemp.Split(":")[0]}";
                                        json2 = json2 + $"{jsonTemp.Split(":")[1]}";
                                    }
                                    else
                                    {
                                        json1 = json1 + $",{jsonTemp.Split(":")[0]}";
                                        json2 = json2 + $",{jsonTemp.Split(":")[1]}";
                                    }
                                    if (line.Contains("Parachute"))
                                    {
                                        Outfits outfit = new Outfits();
                                        outfit.name = headline;
                                        outfit.owner = "EUP";
                                        outfit.json1 = (json1 + "]");
                                        outfit.json2 = (json2 + "]");
                                        outfit.category1 = category1;
                                        outfit.category2 = category2;
                                        db.Save(outfit);
                                    }
                                }
                                else
                                {
                                    if (line.StartsWith("Category2"))
                                    {
                                        category1 = line.Split("=")[1];
                                    }
                                    if (line.StartsWith("Category3"))
                                    {
                                        category2 = line.Split("=")[1];
                                    }
                                }
                                count++;
                            }
                        }
                    }
                    sr.Close();
                }
                if (log == true)
                {
                    var concatArray = concat.Split(",");
                    var distinctConcatArray = concatArray.Distinct().ToArray();
                    Helper.ConsoleLog("info", String.Join(",", distinctConcatArray));
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[LoadEUPOutfits]: " + e.ToString());
            }
        }

        //Chats
        [ServerEvent(Event.ChatMessage)]
        public void OnPlayerChat(Player player, string message)
        {
            try
            {
                Account account = Helper.GetAccountData(player);
                Character character = Helper.GetCharacterData(player);
                TempData tempData = Helper.GetCharacterTempData(player);
                if (account == null || character == null) return;
                //Prison
                if (account.prison > 0) return;
                //AFK Tick Reset
                player.TriggerEvent("Client:AFKTickReset");
                //Adminchat
                if (message.StartsWith("@"))
                {
                    if (Account.IsAdmin(player, (int)Account.AdminRanks.ProbeModerator))
                    {
                        if (message.Length <= 1) return;
                        if (tempData.achat == false)
                        {
                            Helper.SendNotificationWithoutButton(player, "Aktiviere zuerst die Adminbenachrichtigungen!");
                            return;
                        }
                        Helper.SendAdminMessage(message, player);
                        return;
                    }
                }
                //Testchat
                if (message.StartsWith("?"))
                {
                    if (player.GetOwnSharedData<int>("Player:Tester") == 1 || Account.IsAdmin(player, (int)Account.AdminRanks.ProbeModerator))
                    {
                        if (message.Length <= 1) return;
                        Helper.SendTestMessage(message, player);
                        return;
                    }
                }
                //Tod
                if (player.GetSharedData<bool>("Player:Death") == true) return;
                //Schreien
                if (message.StartsWith("!"))
                {
                    if (message.Length <= 1) return;
                    Helper.SendRadiusMessage("!{#FFFFFF}* " + player.Name + " sagt (schreit): " + message.Remove(0, 1), 22, player);
                    return;
                }
                //Premiumchat
                if (message.StartsWith("$"))
                {
                    if (account.premium > 0)
                    {
                        if (message.Length <= 1) return;
                        Helper.SendPremiumMessage(message, account.premium, player);
                        return;
                    }
                }
                if (Helper.adminSettings.voicerp == 0)
                {
                    //Handychat
                    if (tempData.inCall == true)
                    {
                        Player handyPlayer = SmartphoneController.GetPlayerFromSmartPhone(player.GetData<string>("Player:InCall"));
                        if (handyPlayer != null)
                        {
                            Character character2 = Helper.GetCharacterData(handyPlayer);
                            TempData tempData2 = Helper.GetCharacterTempData(handyPlayer);
                            if (tempData2 != null && tempData2.inCall2 == true && handyPlayer.GetData<bool>("Player:AcceptCall") == true && player.GetData<bool>("Player:AcceptCall") == true)
                            {
                                Helper.SendRadiusMessage("!{#FFFFFF}* " + character.name + " sagt (Handy): " + message, 13, player, true);
                                Helper.SendChatMessage(player, "!{#EE82EE}* " + character.name + " sagt (Handy): " + message);
                                if (tempData2.speaker == false)
                                {
                                    if (Helper.adminSettings.nametag != 1)
                                    {
                                        Helper.SendChatMessage(handyPlayer, "!{#EE82EE}* " + character.name + " sagt (Handy): " + message);
                                    }
                                    else
                                    {
                                        if (character2.friends.ToLower().Contains(player.Name.ToLower()))
                                        {
                                            Helper.SendChatMessage(handyPlayer, "!{#EE82EE}* " + character.name + " sagt (Handy): " + message);
                                        }
                                        else
                                        {
                                            Helper.SendChatMessage(handyPlayer, "!{#EE82EE}* Unbekannt sagt (Handy): " + message);
                                        }
                                    }
                                }
                                else
                                {
                                    Helper.SendRadiusMessage("!{#EE82EE}* " + character.name + " sagt (Handylautsprecher): " + message, 13, handyPlayer);
                                }
                                player.TriggerEvent("Client:SpeakAnim");
                                return;
                            }
                        }
                        Helper.SendChatMessage(player, "!{#FF0000}TÜT - TÜT - TÜT");
                        return;
                    }
                    //Normaler Chat
                    if (tempData.adminduty == true)
                    {
                        Helper.SendRadiusMessage("!{#FF0000}* " + account.name + "!{#FFFFFF} sagt: " + message, 13, player);
                        player.TriggerEvent("Client:SpeakAnim");
                    }
                    else
                    {
                        Helper.SendRadiusMessage("!{#FFFFFF}* " + character.name + " sagt: " + message, 13, player);
                        player.TriggerEvent("Client:SpeakAnim");
                    }
                    return;
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnPlayerChat]: " + e.ToString());
            }
        }

        //UnhandledException
        [ServerEvent(Event.UnhandledException)]
        public static void OnUnhandledException(Exception e)
        {
            try
            {
                if (!e.ToString().ToLower().Contains("setandgetweather"))
                {
                    Helper.ConsoleLog("error", $"[OnUnhandledException]: " + e.ToString());
                    Helper.DiscordWebhook(Settings._Settings.ErrorWebhook, e.ToString(), "nMonitoring");
                }
            }
            catch (Exception e2) {
                Helper.ConsoleLog("error", $"[OnUnhandledException2]: " + e2.ToString());
            }
        }
    }
}
