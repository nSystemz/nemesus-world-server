using Discord.Net;
using GTANetworkAPI;
using MySql.Data.MySqlClient;
using NemesusWorld.Controllers;
using NemesusWorld.Database;
using NemesusWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NemesusWorld.Utils
{
    class Menu : Script
    {
        [RemoteEvent("Server:StartStats")]
        public static void OnStartStats(Player player, Player showPlayer = null)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                Account account = Helper.GetAccountData(player);

                if (character == null) return;

                string factionName = Helper.GetFactionName(character.faction);
                string rangName = Helper.GetFactionRangName(player, character.faction, character.rang);
                string jobName = Helper.GetJobName(character.job);

                string jsonbackup = character.json;
                string jsonbackup2 = character.items;
                string jsonbackup3 = character.animations;
                string jsonbackup4 = character.dutyjson;
                character.json = "null";
                character.items = "null";
                character.animations = "null";
                character.dutyjson = "null";
                account._Player = null;

                character.bank = CharacterController.GetMaxBankCash(character.id);

                if (showPlayer == null)
                {
                    player.TriggerEvent("Client:ShowCharacterStats", NAPI.Util.ToJson(character), NAPI.Util.ToJson(account), factionName, rangName, jobName, NAPI.Util.ToJson(GroupsController.GetAllGroupsByCharacterId(character.id)));
                }
                else
                {
                    showPlayer.TriggerEvent("Client:ShowCharacterStats", NAPI.Util.ToJson(character), NAPI.Util.ToJson(account), factionName, rangName, jobName, NAPI.Util.ToJson(GroupsController.GetAllGroupsByCharacterId(character.id)));
                }

                NAPI.Task.Run(() =>
                {
                    character.json = jsonbackup;
                    account._Player = player;
                    character.items = jsonbackup2;
                    character.animations = jsonbackup3;
                    character.dutyjson = jsonbackup4;
                }, delayTime: 135);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnStartStats]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:StartFAQ")]
        public static void OnStartFAQ(Player player)
        {
            try
            {
                Account account = Helper.GetAccountData(player);

                if (account == null) return;

                int percentage = 0;

                if (account.faqarray[0] == "1") percentage += 10;
                if (account.faqarray[1] == "1") percentage += 10;
                if (account.faqarray[2] == "1") percentage += 10;
                if (account.faqarray[3] == "1") percentage += 10;
                if (account.faqarray[4] == "1") percentage += 10;
                if (account.faqarray[5] == "1") percentage += 10;
                if (account.faqarray[6] == "1") percentage += 10;
                if (account.faqarray[7] == "1") percentage += 10;
                if (account.faqarray[8] == "1") percentage += 10;
                if (account.faqarray[9] == "1") percentage += 10;

                account.faq = String.Join(",", account.faqarray);

                player.TriggerEvent("Client:ShowFAQ", account.faq, percentage);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnStartFAQ]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:StartPaydays")]
        public static void OnStartPaydays(Player player)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                Account account = Helper.GetAccountData(player);

                if (character == null) return;

                List<PaydayList> paydays = new List<PaydayList>();

                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                foreach (PaydayList paydaylist in db.Fetch<PaydayList>("SELECT * FROM paydays WHERE characterid = @0 ORDER by timestamp DESC LIMIT 25", character.id))
                {
                    paydays.Add(paydaylist);
                }

                foreach(PaydayList paydaylist in paydays)
                {
                    paydaylist.text = "n/A";
                }

                player.TriggerEvent("Client:ShowPaydays", NAPI.Util.ToJson(paydays));
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnStartPaydays]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:SelectPaydayText")]
        public static void OnSelectPaydayText(Player player, int id)
        {
            try
            {
                string text = "n/A";

                MySqlCommand command = General.Connection.CreateCommand();
                command = General.Connection.CreateCommand();
                command.CommandText = "SELECT text FROM paydays WHERE id = @id LIMIT 1";
                command.Parameters.AddWithValue("@id", id);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        text = reader.GetString("text");
                    }
                    reader.Close();
                }

                player.TriggerEvent("Client:ShowPaydayText", text);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[SelectPaydayText]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:StartCars")]
        public static void OnStartCars(Player player, string input = "character", int set = 0)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                Account account = Helper.GetAccountData(player);

                if (character == null) return;

                List<VehicleData> vehicleDataList = new List<VehicleData>();

                int groupid = 0;
                int characterid = 0;
                int factionid = 0;

                if (set == 0)
                {
                    characterid = character.id;
                    groupid = character.mygroup;
                    factionid = character.faction;
                }
                else
                {
                    characterid = set;
                    groupid = set;
                    factionid = set;
                }

                if(input == "character")
                {
                    foreach (Cars car in Cars.carList)
                    {
                        if (car.vehicleData != null && car.vehicleData.owner == "character-" + characterid)
                        {
                            if (car.vehicleHandle != null)
                            {
                                car.vehicleData.sync = car.vehicleHandle.GetSharedData<string>("Vehicle:Sync");
                                car.vehicleData.doors = car.vehicleHandle.GetSharedData<string>("Vehicle:Doors");
                                car.vehicleData.windows = car.vehicleHandle.GetSharedData<string>("Vehicle:Windows");
                                car.vehicleData.health = $"{NAPI.Vehicle.GetVehicleBodyHealth(car.vehicleHandle)}|{NAPI.Vehicle.GetVehicleEngineHealth(car.vehicleHandle)}|{NAPI.Vehicle.GetVehicleHealth(car.vehicleHandle)}";
                                car.vehicleData.fuel = car.vehicleHandle.GetSharedData<float>("Vehicle:Fuel") > 0 ? (float)Math.Round(car.vehicleHandle.GetSharedData<float>("Vehicle:Fuel"), 2) : 0;
                                car.vehicleData.oel = car.vehicleHandle.GetSharedData<int>("Vehicle:Oel");
                                car.vehicleData.battery = car.vehicleHandle.GetSharedData<int>("Vehicle:Battery");
                                car.vehicleData.tuning = car.vehicleHandle.GetSharedData<string>("Vehicle:Tuning");
                                car.vehicleData.kilometre = car.vehicleHandle.GetSharedData<float>("Vehicle:Kilometre");
                                car.vehicleData.position = $"{car.vehicleHandle.Position.X}|{car.vehicleHandle.Position.Y}|{car.vehicleHandle.Position.Z}|{car.vehicleHandle.Rotation.Z}|{car.vehicleHandle.Dimension}";
                                car.vehicleData.color = car.vehicleHandle.GetSharedData<string>("Vehicle:Color");
                                if (car.vehicleHandle.HasSharedData("Vehicle:Tuev"))
                                {
                                    car.vehicleData.tuev = car.vehicleHandle.GetSharedData<int>("Vehicle:Tuev");
                                }
                                car.vehicleData.status = car.vehicleHandle.Locked ? 1 : 0;
                                car.vehicleData.engine = car.vehicleHandle.EngineStatus ? 1 : 0;
                            }
                            vehicleDataList.Add(car.vehicleData);
                        }
                    }
                }
                else if (input == "group")
                {
                    foreach (Cars car in Cars.carList)
                    {
                        if (car.vehicleData != null && car.vehicleData.owner == "group-" + groupid)
                        {
                            if (car.vehicleHandle != null)
                            {
                                car.vehicleData.sync = car.vehicleHandle.GetSharedData<string>("Vehicle:Sync");
                                car.vehicleData.doors = car.vehicleHandle.GetSharedData<string>("Vehicle:Doors");
                                car.vehicleData.windows = car.vehicleHandle.GetSharedData<string>("Vehicle:Windows");
                                car.vehicleData.health = $"{NAPI.Vehicle.GetVehicleBodyHealth(car.vehicleHandle)}|{NAPI.Vehicle.GetVehicleEngineHealth(car.vehicleHandle)}|{NAPI.Vehicle.GetVehicleHealth(car.vehicleHandle)}";
                                car.vehicleData.fuel = car.vehicleHandle.GetSharedData<float>("Vehicle:Fuel") > 0 ? (float)Math.Round(car.vehicleHandle.GetSharedData<float>("Vehicle:Fuel"), 2) : 0;
                                car.vehicleData.oel = car.vehicleHandle.GetSharedData<int>("Vehicle:Oel");
                                car.vehicleData.battery = car.vehicleHandle.GetSharedData<int>("Vehicle:Battery");
                                car.vehicleData.tuning = car.vehicleHandle.GetSharedData<string>("Vehicle:Tuning");
                                car.vehicleData.kilometre = car.vehicleHandle.GetSharedData<float>("Vehicle:Kilometre");
                                car.vehicleData.position = $"{car.vehicleHandle.Position.X}|{car.vehicleHandle.Position.Y}|{car.vehicleHandle.Position.Z}|{car.vehicleHandle.Rotation.Z}|{car.vehicleHandle.Dimension}";
                                car.vehicleData.color = car.vehicleHandle.GetSharedData<string>("Vehicle:Color");
                                if (car.vehicleHandle.HasSharedData("Vehicle:Tuev"))
                                {
                                    car.vehicleData.tuev = car.vehicleHandle.GetSharedData<int>("Vehicle:Tuev");
                                }
                                car.vehicleData.status = car.vehicleHandle.Locked ? 1 : 0;
                                car.vehicleData.engine = car.vehicleHandle.EngineStatus ? 1 : 0;
                            }
                            vehicleDataList.Add(car.vehicleData);
                        }
                    }
                }
                else if (input == "faction")
                {
                    foreach (Cars car in Cars.carList)
                    {
                        if (car.vehicleData != null && car.vehicleData.owner == "faction-" + factionid)
                        {
                            if (car.vehicleHandle != null)
                            {
                                car.vehicleData.sync = car.vehicleHandle.GetSharedData<string>("Vehicle:Sync");
                                car.vehicleData.doors = car.vehicleHandle.GetSharedData<string>("Vehicle:Doors");
                                car.vehicleData.windows = car.vehicleHandle.GetSharedData<string>("Vehicle:Windows");
                                car.vehicleData.health = $"{NAPI.Vehicle.GetVehicleBodyHealth(car.vehicleHandle)}|{NAPI.Vehicle.GetVehicleEngineHealth(car.vehicleHandle)}|{NAPI.Vehicle.GetVehicleHealth(car.vehicleHandle)}";
                                car.vehicleData.fuel = car.vehicleHandle.GetSharedData<float>("Vehicle:Fuel") > 0 ? (float)Math.Round(car.vehicleHandle.GetSharedData<float>("Vehicle:Fuel"), 2) : 0;
                                car.vehicleData.oel = car.vehicleHandle.GetSharedData<int>("Vehicle:Oel");
                                car.vehicleData.battery = car.vehicleHandle.GetSharedData<int>("Vehicle:Battery");
                                car.vehicleData.tuning = car.vehicleHandle.GetSharedData<string>("Vehicle:Tuning");
                                car.vehicleData.kilometre = car.vehicleHandle.GetSharedData<float>("Vehicle:Kilometre");
                                car.vehicleData.position = $"{car.vehicleHandle.Position.X}|{car.vehicleHandle.Position.Y}|{car.vehicleHandle.Position.Z}|{car.vehicleHandle.Rotation.Z}|{car.vehicleHandle.Dimension}";
                                car.vehicleData.color = car.vehicleHandle.GetSharedData<string>("Vehicle:Color");
                                if (car.vehicleHandle.HasSharedData("Vehicle:Tuev"))
                                {
                                    car.vehicleData.tuev = car.vehicleHandle.GetSharedData<int>("Vehicle:Tuev");
                                }
                                car.vehicleData.status = car.vehicleHandle.Locked ? 1 : 0;
                                car.vehicleData.engine = car.vehicleHandle.EngineStatus ? 1 : 0;
                            }
                            vehicleDataList.Add(car.vehicleData);
                        }
                    }
                }
                player.TriggerEvent("Client:ShowCars", NAPI.Util.ToJson(vehicleDataList.OrderBy(x => x.plate.Length).ThenBy(x => x.plate).ToList()), set);
                if(set > 0 && vehicleDataList.Count == 0)
                {
                    Helper.SendNotificationWithoutButton(player, "Keine Fahrzeuge vorhanden!", "error");
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnStartCars]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:SetCarRang")]
        public void OnSetCarRang(Player player, int id, int rang, string owner)
        {
            try
            {
                int found = 0;
                Groups group = null;
                FactionsModel faction = null;
                Character character = Helper.GetCharacterData(player);
                if(owner.Contains("group"))
                {
                    group = GroupsController.GetGroupById(character.mygroup);
                    if (group == null) return;
                    GroupsMembers groupMember = GroupsController.GetGroupMemberById(character.id, group.id);
                    if (groupMember == null) return;
                    if (groupMember.rang < 10)
                    {
                        Helper.SendNotificationWithoutButton2(player, "Keine Berechtigung!", "error", "Center");
                        return;
                    }                 
                    found = 1;
                }
                else if (owner.Contains("faction"))
                {
                    faction = FactionController.GetFactionById(character.faction);
                    if (faction == null) return;
                    if (character.rang < 10)
                    {
                        Helper.SendNotificationWithoutButton2(player, "Keine Berechtigung!", "error", "Center");
                        return;
                    }
                    found = 2;
                }
                VehicleData vehicleData = DealerShipController.GetVehicleDataById(id);
                if(vehicleData != null)
                {
                    vehicleData.rang = rang;
                    Helper.SendNotificationWithoutButton2(player, "Rang wurde gesetzt!", "success", "Center");
                    if (found == 1)
                    {
                        Helper.CreateGroupLog(group.id, $"{character.name} den benötigten Rang des Fahrzeuges {vehicleData.vehiclename}[{vehicleData.id}] auf {rang} gesetzt!");
                    }
                    else if (found == 2)
                    {
                        Helper.CreateFactionLog(faction.id, $"{character.name} den benötigten Rang des Fahrzeuges {vehicleData.vehiclename}[{vehicleData.id}] auf {rang} gesetzt!");
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnSetCarRang]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:SetCarName")]
        public void OnSetCarName(Player player, int id, string name, string owner)
        {
            try
            {
                int found = 0;
                Groups group = null;
                FactionsModel faction = null;
                Character character = Helper.GetCharacterData(player);
                if (owner.Contains("group"))
                {
                    group = GroupsController.GetGroupById(character.mygroup);
                    if (group == null) return;
                    GroupsMembers groupMember = GroupsController.GetGroupMemberById(character.id, group.id);
                    if (groupMember == null) return;
                    if (groupMember.rang < 10)
                    {
                        Helper.SendNotificationWithoutButton2(player, "Keine Berechtigung!", "error", "Center");
                        return;
                    }
                    found = 1;
                }
                else if (owner.Contains("faction"))
                {
                    faction = FactionController.GetFactionById(character.faction);
                    if (faction == null) return;
                    if (character.rang < 10)
                    {
                        Helper.SendNotificationWithoutButton2(player, "Keine Berechtigung!", "error", "Center");
                        return;
                    }
                    found = 2;
                }
                VehicleData vehicleData = DealerShipController.GetVehicleDataById(id);
                if (vehicleData != null)
                {
                    vehicleData.ownname = name;
                    Helper.SendNotificationWithoutButton2(player, "Ind. Name wurde gesetzt!", "success", "Center");
                    if (found == 1)
                    {
                        Helper.CreateGroupLog(group.id, $"{character.name} den Ind. Namen des Fahrzeuges {vehicleData.vehiclename}[{vehicleData.id}] auf {name} gesetzt!");
                    }
                    else if (found == 2)
                    {
                        Helper.CreateFactionLog(faction.id, $"{character.name} den Ind. Namen des Fahrzeuges {vehicleData.vehiclename}[{vehicleData.id}] auf {name} gesetzt!");
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[SetCarName]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:StartSettings")]
        public void OnStartSettings(Player player)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                Account account = Helper.GetAccountData(player);

                if (account == null || character == null) return;

                if(character.animations == null)
                {
                    character.animations = "[\"n/A\",\"n/A\",\"n/A\",\"n/A\",\"n/A\",\"n/A\",\"n/A\",\"n/A\",\"n/A\"]";
                }

                player.TriggerEvent("Client:ShowSettings", account.crosshair, account.autologin, character.armor, character.animations);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnStartSettings]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:SelectSetting")]
        public void OnSelectSetting(Player player, int setting, string getsettingsvalue)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                Account account = Helper.GetAccountData(player);

                if (account == null || character == null) return;

                int settingsvalue = 0;

                if (setting != 4)
                {
                    settingsvalue = Convert.ToInt32(getsettingsvalue);
                }

                if (setting == 1)
                {
                    account.autologin = settingsvalue;
                }
                else if (setting == 3)
                {
                    if ((character.faction == 1 || character.faction == 2 || character.faction == 3) && character.factionduty == true)
                    {
                        Helper.SendNotificationWithoutButton2(player, "Bitte wähle deine visuelle Schutzweste in der Umkleidekabine", "error", "Center");
                        return;
                    }
                    if ((settingsvalue < 0 || settingsvalue > 3) && character.gender == 1)
                    {
                        Helper.SendNotificationWithoutButton2(player, "Ungültiger Wert!", "error", "Center");
                        return;
                    }
                    if ((settingsvalue < 0 || settingsvalue > 2) && character.gender == 2)
                    {
                        Helper.SendNotificationWithoutButton2(player, "Ungültiger Wert!", "error", "Center");
                        return;
                    }
                    character.armor = settingsvalue;
                    character.armorcolor = 0;
                    if (NAPI.Player.GetPlayerArmor(player) > 0 && ((character.faction != 1 && character.faction != 2 && character.faction != 3) || character.factionduty == false))
                    {
                        NAPI.Player.SetPlayerClothes(player, 9, settingsvalue, 0);
                    }
                }
                else if (setting == 4)
                {
                    if (getsettingsvalue.Length != 8)
                    {
                        Helper.SendNotificationWithoutButton2(player, "Ungültiger Gutschein!", "error", "Center");
                        return;
                    }
                    MySqlCommand command2 = General.Connection.CreateCommand();
                    command2 = General.Connection.CreateCommand();
                    command2.CommandText = "SELECT coupontext FROM coupons WHERE coupon = @coupon AND usages > 0 LIMIT 1";
                    command2.Parameters.AddWithValue("@coupon", getsettingsvalue);

                    bool found = false;
                    string text = "";

                    using (MySqlDataReader reader = command2.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            text = reader.GetString("coupontext");
                            found = true;
                        }
                        reader.Close();
                    }
                    if (found == false)
                    {
                        Helper.SendNotificationWithoutButton2(player, "Ungültiger Gutschein!", "error", "Center");
                        return;
                    }
                    else
                    {                     
                        switch (text)
                        {
                            case "+1 Erfahrungspunkt":
                                {
                                    account.play_points++;
                                    break;
                                }
                            case "+3 Erfahrungspunkt":
                                {
                                    account.play_points+=3;
                                    break;
                                }
                            case "+5 Erfahrungspunkt":
                                {
                                    account.play_points += 5;
                                    break;
                                }
                            case "+1 Level":
                                {
                                    account.level ++;
                                    break;
                                }
                            case "+1 Namechage":
                                {
                                    account.namechanges++;
                                    break;
                                }
                            case "+24 Erfahrungspunkte Boost":
                                {
                                    if (account.epboost == 0)
                                    {
                                        account.epboost = Helper.UnixTimestamp() + (3600 * 24);
                                    }
                                    else
                                    {
                                        account.epboost += Helper.UnixTimestamp() + (3600 * 24);
                                    }
                                    break;
                                }
                            case "+1 Hausslot":
                                {
                                    if(account.houseslots > 2)
                                    {
                                        Helper.SendNotificationWithoutButton2(player, "Du hast die max. Anzahl an Hausslots erreicht!", "error", "Center");
                                        return;
                                    }
                                    account.houseslots++;
                                    break;
                                }
                            case "+1 Fahrzeugslot":
                                {
                                    if (account.vehicleslots > 3)
                                    {
                                        Helper.SendNotificationWithoutButton2(player, "Du hast die max. Anzahl an Fahrzeugslots erreicht!", "error", "Center");
                                        return;
                                    }
                                    account.vehicleslots++;
                                    break;
                                }
                            case "Premium Bronze 30 Tage":
                                {
                                    if (account.premium > 0)
                                    {
                                        Helper.SendNotificationWithoutButton2(player, "Du hast bereits Premium!", "error", "Center");
                                        return;
                                    }
                                    account.premium = 1;
                                    account.premium_time += Helper.UnixTimestamp() + (30*86400);
                                    break;
                                }
                            case "Premium Silber 30 Tage":
                                {
                                    if (account.premium > 0)
                                    {
                                        Helper.SendNotificationWithoutButton2(player, "Du hast bereits Premium!", "error", "Center");
                                        return;
                                    }
                                    account.premium = 2;
                                    account.premium_time += Helper.UnixTimestamp() + (30 * 86400);
                                    break;
                                }
                            case "Premium Gold 30 Tage":
                                {
                                    if (account.premium > 0)
                                    {
                                        Helper.SendNotificationWithoutButton2(player, "Du hast bereits Premium!", "error", "Center");
                                        return;
                                    }
                                    account.premium = 3;
                                    account.premium_time += Helper.UnixTimestamp() + (30 * 86400);
                                    break;
                                }
                            case "15000$":
                                {
                                    CharacterController.SetMoney(player, 15000);
                                    break;
                                }
                            case "35000$":
                                {
                                    CharacterController.SetMoney(player, 35000);
                                    break;
                                }
                            case "50000$":
                                {
                                    CharacterController.SetMoney(player, 50000);
                                    break;
                                }
                            case "100000":
                                {
                                    CharacterController.SetMoney(player, 100000);
                                    break;
                                }
                        }

                        MySqlCommand command = General.Connection.CreateCommand();
                        command.CommandText = "UPDATE coupons SET usages = usages - 1 WHERE coupon = @getsettingsvalue";
                        command.Parameters.AddWithValue("@getsettingsvalue", getsettingsvalue);
                        command.ExecuteNonQuery();

                        Helper.SendNotificationWithoutButton2(player, $"Gutschein mit {text} erfolgreich eingelöst!", "success", "Center", 3750);
                    }
                }
                else if (setting != 4)
                {
                    Helper.SendNotificationWithoutButton2(player, "Einstellung übernommen!", "success", "Center");
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnSelectSetting]: " + e.ToString());
            }
        }

        public static void UpdateHouseHud(Player player)
        {
            OnStartHouse(player, true);
        }

        [RemoteEvent("Server:StartHouse")]
        public static void OnStartHouse(Player player, bool updatehud = false)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                Account account = Helper.GetAccountData(player);

                if (character == null) return;

                House house;
                if (character.inhouse == -1)
                {
                    house = House.GetClosestHouse(player);
                }
                else
                {
                    house = House.GetHouseById(character.inhouse);
                }
                if (house != null && House.HasPlayerHouseKey(player, house.id))
                {
                    int houseclassify = House.GetInteriorClassify(house.interior);
                    string classify = "Klein";
                    if (houseclassify == 1)
                    {
                        classify = "Mittel";
                    }
                    else if (houseclassify == 2)
                    {
                        classify = "Gross";
                    }
                    else if (houseclassify == 3)
                    {
                        classify = "Villa";
                    }
                    else if (houseclassify >= 4)
                    {
                        classify = "Individuell";
                    }
                    HouseModel houseModel = new HouseModel();
                    houseModel.id = house.id;
                    houseModel.interior = house.interior;
                    houseModel.price = house.price;
                    houseModel.owner = house.owner;
                    houseModel.status = house.status;
                    houseModel.tenants = house.tenants;
                    houseModel.rental = house.rental;
                    houseModel.locked = house.locked;
                    houseModel.noshield = house.noshield;
                    houseModel.housegroup = house.housegroup;
                    houseModel.blip = house.blip;
                    houseModel.stock = house.stock;
                    houseModel.stockprice = house.stockprice;
                    houseModel.elec = house.elec;
                    houseModel.elecname = Business.GetBusinessById(houseModel.elec).name;

                    List<LoadCharactersModel> loadedCharactersList = new List<LoadCharactersModel>();

                    MySqlCommand command = General.Connection.CreateCommand();
                    command.CommandText = "SELECT id,name,cash,bank,job,faction,screen,closed,defaultbank FROM characters WHERE items LIKE '%Miethausnummer: " + house.id + "%' LIMIT 15";

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            LoadCharactersModel loadCharacter = new LoadCharactersModel();
                            loadCharacter.ID = reader.GetInt32("id");
                            loadCharacter.Name = reader.GetString("name");
                            loadCharacter.Cash = reader.GetInt32("cash");
                            Bank bank = BankController.GetBankByBankNumber(reader.GetString("defaultbank"));
                            if(bank != null)
                            {
                                loadCharacter.Bank = bank.bankvalue;
                            }
                            else
                            {
                                loadCharacter.Bank = 0;
                            }
                            loadCharacter.Job = Helper.GetJobName(reader.GetInt32("job"));
                            loadCharacter.Faction = Helper.GetFactionTag(reader.GetInt32("faction"));
                            loadCharacter.Closed = reader.GetInt16("closed");
                            loadCharacter.Screen = reader.GetString("screen");
                            loadedCharactersList.Add(loadCharacter);
                        }
                        reader.Close();
                    }
                    if (updatehud == false)
                    {
                        player.TriggerEvent("Client:ShowHouseStats", NAPI.Util.ToJson(houseModel), classify, character.name, NAPI.Util.ToJson(loadedCharactersList));
                    }
                    else
                    {
                        player.TriggerEvent("Client:UpdateHouseStats", NAPI.Util.ToJson(houseModel), classify, character.name, NAPI.Util.ToJson(loadedCharactersList));
                    }
                    NAPI.Task.Run(() =>
                    {
                        houseModel = null;
                    }, delayTime: 515);
                }
                else
                {
                    Helper.SendNotificationWithoutButton2(player, "Du bist nicht in oder in der Nähe von einem deiner Häuser!", "error", "Center");
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnStartHouse]: " + e);
            }
        }

        [RemoteEvent("Server:StartMoebel")]
        public static void OnStartMoebel(Player player, bool hidehud = false)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                Account account = Helper.GetAccountData(player);
                TempData tempData = Helper.GetCharacterTempData(player);

                if (character == null) return;

                House house;
                if (character.inhouse == -1)
                {
                    house = House.GetClosestHouse(player, 25.5f);
                }
                else
                {
                    house = House.GetHouseById(character.inhouse);
                }
                if (house != null)
                {
                    int checkhouse = House.HasPlayerHouseKey2(player, house.id);
                    if (checkhouse != 1)
                    {
                        Helper.SendNotificationWithoutButton2(player, "Du kannst hier keine Möbel aufstellen!", "error", "center", 3500);
                        return;
                    }
                    int limit = House.GetFurnitureLimit(player);
                    int count = Furniture.CountFurniture(house.id);
                    player.TriggerEvent("Client:PlayerFreeze", true);
                    player.TriggerEvent("Client:ShowMoebel", NAPI.Util.ToJson(House.furnitureModelCategorieList), NAPI.Util.ToJson(House.furnitureModelList), NAPI.Util.ToJson(House.GetFurnitureForHouse(house.id)), count, limit);
                    if (hidehud == true)
                    {
                        player.TriggerEvent("Client:UpdateHud3");
                    }
                }
                else
                {
                    player.TriggerEvent("Client:HideCursor");
                    player.TriggerEvent("Client:PlayerFreeze", false);
                    Helper.SendNotificationWithoutButton2(player, "Du bist nicht in oder in der Nähe von einem deiner Häuser!", "error", "center", 3500);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[StartMoebel]: " + e);
            }
        }
    }
}
