using GTANetworkAPI;
using MySqlConnector;
using NemesusWorld.Database;
using NemesusWorld.Models;
using NemesusWorld.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NemesusWorld.Controllers
{
    class FactionController : Script
    {
        public static List<FactionRangs> factionRangList = new List<FactionRangs>();
        public static List<FactionSalary> factionSalaryList = new List<FactionSalary>();
        public static List<FactionBudgets> factionBudgetsList = new List<FactionBudgets>();
        public static List<LifeInvaderAds> lifeInvaderAdsList = new List<LifeInvaderAds>();
        public static bool OperatorBlip = false;

        [RemoteEvent("Server:StartFaction")]
        public static void OnStartFaction(Player player)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                Account account = Helper.GetAccountData(player);

                if (character == null || character.faction <= 0) return;

                FactionsModel faction = GetFactionById(character.faction);

                if (faction == null) return;

                string leadername = "n/A";

                leadername = Helper.GetCharacterName(faction.leader);

                MySqlCommand command = General.Connection.CreateCommand();
                command.CommandText = "SELECT COUNT(*) FROM vehicles WHERE owner=@owner";
                command.Parameters.AddWithValue("@owner", "faction-" + faction.id);
                Int64 count2 = (Int64)command.ExecuteScalar();

                List<FactionMember> factionListMember = GetFactionMembers(faction.id);

                player.TriggerEvent("Client:ShowFactionStats", NAPI.Util.ToJson(faction), NAPI.Util.ToJson(factionListMember), factionListMember.Count, count2, leadername, NAPI.Util.ToJson(GetFactionRangsById(faction.id)), character.id);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnStartFaction]: " + e);
            }
        }

        public static void UpdateFactionStats(Player player)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                Account account = Helper.GetAccountData(player);

                if (character == null || character.mygroup == -1) return;

                FactionsModel faction = GetFactionById(character.faction);

                if (faction == null) return;

                string leadername = "n/A";

                leadername = Helper.GetCharacterName(faction.leader);

                MySqlCommand command = General.Connection.CreateCommand();
                command.CommandText = "SELECT COUNT(*) FROM vehicles WHERE owner=@owner";
                command.Parameters.AddWithValue("@owner", "faction-" + faction.id);
                Int64 count2 = (Int64)command.ExecuteScalar();

                List<FactionMember> factionListMember = GetFactionMembers(faction.id);

                player.TriggerEvent("Client:UpdateFactionStats", NAPI.Util.ToJson(faction), NAPI.Util.ToJson(factionListMember), factionListMember.Count, count2, leadername, NAPI.Util.ToJson(GetFactionRangsById(faction.id)), character.id);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[UpdateFactionStats]: " + e);
            }
        }

        public static FactionsModel GetFactionById(int faction)
        {
            try
            {
                foreach (FactionsModel factionModel in Helper.factionList)
                {
                    if (factionModel.id == faction)
                    {
                        return factionModel;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetFactionById]: " + e);
            }
            return null;
        }

        public static FactionsModel GetFactionByName(string factionName)
        {
            try
            {
                foreach (FactionsModel factionModel in Helper.factionList)
                {
                    if (factionModel.name.ToLower() == factionName.ToLower())
                    {
                        return factionModel;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetFactionByName]: " + e);
            }
            return null;
        }

        public static List<FactionMember> GetFactionMembers(int faction)
        {
            List<FactionMember> factionMemberList = new List<FactionMember>();
            try
            {
                MySqlCommand command = General.Connection.CreateCommand();
                command = General.Connection.CreateCommand();
                command.CommandText = "SELECT id,name,faction,rang,faction_dutytime,faction_since,online,swat FROM characters WHERE faction=@faction ORDER BY rang DESC, name ASC";
                command.Parameters.AddWithValue("@faction", faction);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        FactionMember factionMember = new FactionMember();
                        factionMember.charid = reader.GetInt32("id");
                        factionMember.name = reader.GetString("name");
                        factionMember.membersince = reader.GetInt32("faction_since");
                        factionMember.rang = reader.GetInt32("rang");
                        factionMember.dutytime = reader.GetInt32("faction_dutytime");
                        factionMember.online = reader.GetInt32("online");
                        factionMember.swat = reader.GetInt32("swat");
                        factionMemberList.Add(factionMember);
                    }
                    reader.Close();
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetFactionMembers]: " + e);
            }
            return factionMemberList;
        }

        public static FactionRangs GetFactionRangsById(int factionId)
        {
            try
            {
                foreach (FactionRangs factionRangs in factionRangList)
                {
                    if (factionRangs != null && factionRangs.factionid == factionId)
                    {
                        return factionRangs;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetFactionRangsById]: " + e.ToString());
            }
            return null;
        }

        public static FactionSalary GetFactionSalarysById(int factionId)
        {
            try
            {
                foreach (FactionSalary factionSalary in factionSalaryList)
                {
                    if (factionSalary != null && factionSalary.factionid == factionId)
                    {
                        return factionSalary;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetFactionSalarysById]: " + e.ToString());
            }
            return null;
        }

        public static int GetFactionSalaryByRang(FactionSalary factionSalary, int faction, int rang)
        {
            int salary = 0;
            try
            {
                if (factionSalary != null && factionSalary.factionid == faction)
                {
                    switch(rang)
                    {
                        case 0:
                            {
                                salary = 0;
                                break;
                            }
                        case 1:
                            {
                                salary = factionSalary.rang1;
                                break;
                            }
                        case 2:
                            {
                                salary = factionSalary.rang2;
                                break;
                            }
                        case 3:
                            {
                                salary = factionSalary.rang3;
                                break;
                            }
                        case 4:
                            {
                                salary = factionSalary.rang4;
                                break;
                            }
                        case 5:
                            {
                                salary = factionSalary.rang5;
                                break;
                            }
                        case 6:
                            {
                                salary = factionSalary.rang6;
                                break;
                            }
                        case 7:
                            {
                                salary = factionSalary.rang7;
                                break;
                            }
                        case 8:
                            {
                                salary = factionSalary.rang8;
                                break;
                            }
                        case 9:
                            {
                                salary = factionSalary.rang9;
                                break;
                            }
                        case 10:
                            {
                                salary = factionSalary.rang10;
                                break;
                            }
                        case 11:
                            {
                                salary = factionSalary.rang11;
                                break;
                            }
                        case 12:
                            {
                                salary = factionSalary.rang12;
                                break;
                            }
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetFactionSalaryByRang]: " + e.ToString());
            }
            return salary;
        }

        public static int GetFactionBudgetById(int factionId)
        {
            try
            {
                foreach (FactionBudgets factionBudget in factionBudgetsList)
                {
                    if (factionBudget != null && factionBudget.faction == factionId)
                    {
                        return factionBudget.budget;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetFactionSalarysById]: " + e.ToString());
            }
            return 0;
        }

        public static void GetAllFactionRangs()
        {
            try
            {
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                foreach (FactionRangs factionRangs in db.Fetch<FactionRangs>("SELECT * FROM factionsrangs"))
                {
                    factionRangList.Add(factionRangs);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetAllFactionRangs]: " + e.ToString());
            }
        }

        public static void GetAllFactionsSalary()
        {
            try
            {
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                foreach (FactionSalary factionSalary in db.Fetch<FactionSalary>("SELECT * FROM factionsalary"))
                {
                    factionSalaryList.Add(factionSalary);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetAllFactionsSalary]: " + e.ToString());
            }
        }

        public static void GetAllFactionBudgets()
        {
            try
            {
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                foreach (FactionBudgets factionBudgets in db.Fetch<FactionBudgets>("SELECT * FROM factionbudgets"))
                {
                    factionBudgetsList.Add(factionBudgets);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetAllFactionBudgets]: " + e.ToString());
            }
        }

        public static int GetFreeFactionPlate(int factionid)
        {
            int count = 0;
            try
            {
                foreach (Cars car in Cars.carList)
                {
                    if (car.vehicleData != null && car.vehicleData.owner == "faction-" + factionid)
                    {
                        count++;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetFreeFactionPlate]: " + e.ToString());
            }
            return count + 1;
        }

        [RemoteEvent("Server:FactionSettings")]
        public static void OnFactionSettings(Player player, string settings, string json)
        {
            try
            {
                Account account = Helper.GetAccountData(player);
                Character character = Helper.GetCharacterData(player);
                TempData tempData = Helper.GetCharacterTempData(player);
                FactionsModel faction = null;
                if (account == null || character == null || tempData == null) return;
                switch (settings.ToLower())
                {
                    case "wheel":
                        {
                            int number = Convert.ToInt32(json);
                            if (number == 1)
                            {
                                if (character.faction == 1)
                                {
                                    if (character.factionduty == false)
                                    {
                                        Helper.SendNotificationWithoutButton(player, "Du hast keine Handschellen dabei!", "error");
                                        return;
                                    }
                                    Player getPlayer = Helper.GetClosestPlayer(player, 2.5f);
                                    if (getPlayer != null && getPlayer != player)
                                    {
                                        Vector3 behindPlayer = Helper.GetPositionBehindOfPlayer(getPlayer, 1.95f);
                                        if (player.Position.DistanceTo(behindPlayer) <= 1.95)
                                        {
                                            TempData tempData2 = Helper.GetCharacterTempData(getPlayer);
                                            if(tempData2.adminduty == true)
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Ungültiger Spieler!", "error");
                                                return;
                                            }
                                            if (tempData2.cuffed == 2)
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Der Spieler trägt schon Kabelbinder, diese müssen erst abgenommen werden!", "error");
                                                return;
                                            }
                                            if (tempData2.cuffed == 0)
                                            {
                                                string[] weaponArray = new string[7];
                                                foreach (Items iteminlist in tempData2.itemlist.ToList())
                                                {
                                                    if (iteminlist != null && iteminlist.type == 5)
                                                    {
                                                        weaponArray = iteminlist.props.Split(",");
                                                        if (weaponArray[1] == "1")
                                                        {
                                                            WeaponController.OnSelectGun(getPlayer, iteminlist.itemid, 1, true);
                                                        }
                                                    }
                                                }
                                                tempData2.cuffed = 1;
                                                NAPI.Player.SetPlayerCurrentWeapon(getPlayer, WeaponHash.Unarmed);
                                                Helper.SendNotificationWithoutButton(player, "Du hast jemanden Handschellen angelegt!", "success");
                                                Helper.SendNotificationWithoutButton(getPlayer, "Dir wurden Handschellen angelegt!", "success");
                                                Commands.cmd_animation(player, "uncuff", true);
                                                NAPI.Task.Run(() =>
                                                {
                                                    getPlayer.SetSharedData("Player:AnimData", $"mp_arresting%idle%{49}");
                                                }, delayTime: 215);
                                                getPlayer.TriggerEvent("Client:HideMenus");
                                                getPlayer.TriggerEvent("Client:SetCuff", true);
                                            }
                                            else
                                            {
                                                if (tempData2.follow == true)
                                                {
                                                    Helper.SendNotificationWithoutButton(player, "Die Handschellen können jetzt nicht abgenommen werden!", "error");
                                                    return;
                                                }
                                                tempData2.cuffed = 0;
                                                Commands.cmd_animation(player, "uncuff", true);
                                                Helper.SendNotificationWithoutButton(player, "Du hast jemanden die Handschellen abgenommen!", "success");
                                                Helper.SendNotificationWithoutButton(getPlayer, "Dir wurden die Handschellen abgenommen!", "success");
                                                getPlayer.TriggerEvent("Client:SetCuff", false);
                                                Helper.OnStopAnimation(getPlayer);
                                            }
                                        }
                                        else
                                        {
                                            Helper.SendNotificationWithoutButton(player, "Du stehst nicht hinter dem Spieler!", "error");
                                        }
                                    }
                                    else
                                    {
                                        if (GangController.dealerPosition != null && Helper.IsInRangeOfPoint(player.Position, GangController.dealerPosition, 2.75f) && player.Dimension == 0)
                                        {
                                            GangController.DeleteWaffendealer();
                                            MDCController.SendPoliceMDCMessage(player, $"{character.name} hat den illegalen Waffedealer überführt!");
                                        }
                                        else if (GangController.dealerPosition2 != null && Helper.IsInRangeOfPoint(player.Position, GangController.dealerPosition2, 2.75f) && player.Dimension == 0)
                                        {
                                            GangController.DeleteDrugDealer();
                                            MDCController.SendPoliceMDCMessage(player, $"{character.name} hat den illegalen Waffedealer überführt!");
                                        }
                                        else
                                        {
                                            Helper.SendNotificationWithoutButton(player, "Es befindet sich kein Spieler in der Nähe!", "error");
                                        }
                                    }
                                }
                                else if (character.faction == 2)
                                {
                                    Items item = ItemsController.GetItemByItemName(player, "Defribrilator");            
                                    if (item == null)
                                    {
                                        Helper.SendNotificationWithoutButton(player, "Du hast keinen Defribrilator dabei!", "error");
                                        return;
                                    }
                                    Player getPlayer = Helper.GetClosestPlayer(player, 2.5f);
                                    if (getPlayer != null && getPlayer != player)
                                    {
                                        Character character2 = Helper.GetCharacterData(getPlayer);
                                        if(character2 != null && character2.death == true)
                                        {
                                            player.SetSharedData("Player:AnimData", $"mini@cpr@char_a@cpr_str%cpr_pumpchest%{1}");
                                            player.TriggerEvent("Client:StartLockpicking", 22, "reanim", "Person wird reanimiert...");
                                            return;
                                        }
                                        Helper.SendNotificationWithoutButton(player, "Der Spieler ist nicht Tod!", "error");
                                    }
                                    else
                                    {
                                        Helper.SendNotificationWithoutButton(player, "Es befindet sich kein Spieler in der Nähe!", "error");
                                    }
                                }
                            }
                            else if (number == 2)
                            {
                                if (character.faction == 1)
                                {
                                    Player getPlayer = Helper.GetClosestPlayer(player, 2.5f);
                                    if (getPlayer != null && getPlayer != player && !getPlayer.IsInVehicle)
                                    {
                                        TempData tempData2 = Helper.GetCharacterTempData(getPlayer);
                                        Character character2 = Helper.GetCharacterData(getPlayer);
                                        if (tempData2.cuffed == 0)
                                        {
                                            Helper.SendNotificationWithoutButton(player, "Der Spieler ist nicht gefesselt!", "error");
                                            return;
                                        }
                                        if (tempData2.cuffed == 1)
                                        {
                                            if (tempData2.follow == false && tempData2.followed == false)
                                            {
                                                if (tempData.followed == true)
                                                {
                                                    Helper.SendNotificationWithoutButton(player, "Du ziehst schon jemanden vor dir her!", "error");
                                                    return;
                                                }
                                                Commands.cmd_animation(player, "give", true);
                                                Helper.SendNotificationWithoutButton(player, "Du ziehst jemanden vor dir her!", "success");
                                                Helper.SendNotificationWithoutButton(getPlayer, "Du wirst von jemanden gezogen!", "success");
                                                getPlayer.TriggerEvent("Client:PlayerFreeze", true);
                                                player.SetSharedData("Player:Follow", getPlayer.Id);
                                                NAPI.Task.Run(() =>
                                                {
                                                    getPlayer.Heading = player.Heading + 90;
                                                    player.SetSharedData("Player:FollowStatus", 1);
                                                    tempData.followed = true;
                                                    tempData2.follow = true;
                                                }, delayTime: 115);
                                            }
                                            else
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Du ziehst keinen mehr vor dir her!", "success");
                                                Helper.SendNotificationWithoutButton(getPlayer, "Du wirst nicht mehr gezogen!", "success");
                                                player.SetSharedData("Player:FollowStatus", 0);
                                                tempData.followed = false;
                                                NAPI.Task.Run(() =>
                                                {
                                                    player.SetSharedData("Player:Follow", "n/A");
                                                    player.ResetSharedData("Player:Follow");
                                                    player.ResetSharedData("Player:FollowStatus");
                                                    Vector3 newPosition = Helper.GetPositionInFrontOfPlayer(player, 1.15f);
                                                    Helper.SetPlayerPosition(getPlayer, newPosition);
                                                    getPlayer.Heading = player.Heading + 90;
                                                    tempData2.follow = false;
                                                    if (tempData2.cuffed > 0)
                                                    {
                                                        getPlayer.SetSharedData("Player:AnimData", $"mp_arresting%idle%{49}");
                                                    }
                                                    else if (character2.death == true)
                                                    {
                                                        getPlayer.SetSharedData("Player:AnimData", $"random@dealgonewrong%idle_a%{2}");
                                                    }
                                                    else
                                                    {
                                                        Helper.OnStopAnimation(getPlayer);
                                                    }
                                                    getPlayer.TriggerEvent("Client:PlayerFreeze", false);
                                                }, delayTime: 115);
                                            }
                                        }
                                        else
                                        {
                                            Helper.SendNotificationWithoutButton(player, "Dem Spieler liegen keine Handschellen an!", "error");
                                        }
                                    }
                                    else
                                    {
                                        Helper.SendNotificationWithoutButton(player, "Es befindet sich kein Spieler in der Nähe!", "error");
                                    }
                                }
                                else if(character.faction == 2)
                                {
                                    Items item = ItemsController.GetItemByItemName(player, "Stethoskop");
                                    if (item == null)
                                    {
                                        Helper.SendNotificationWithoutButton(player, "Du hast kein Stethoskop dabei!", "error");
                                        return;
                                    }
                                    Player getPlayer = Helper.GetClosestPlayer(player, 2.5f);
                                    if (getPlayer != null)
                                    {
                                        Character character2 = Helper.GetCharacterData(getPlayer);
                                        if (character2 != null && character2.death == false)
                                        {
                                            getPlayer.TriggerEvent("Client:GetWeaponDamage");
                                            player.SetSharedData("Player:AnimData", $"random@train_tracks%idle_e%{1}");
                                            player.TriggerEvent("Client:StartLockpicking", 11, "check", "Person wird untersucht...");
                                            return;
                                        }
                                        Helper.SendNotificationWithoutButton(player, "Die Person kann nicht untersucht werden!", "error");
                                    }
                                    else
                                    {
                                        Helper.SendNotificationWithoutButton(player, "Es befindet sich kein Spieler in der Nähe!", "error");
                                    }
                                }
                            }
                            else if (number == 3)
                            {
                                if (character.faction == 1)
                                {
                                    Player getPlayer = Helper.GetClosestPlayer(player, 2.5f);
                                    if (getPlayer != null)
                                    {
                                        TempData tempData2 = Helper.GetCharacterTempData(getPlayer);
                                        if (getPlayer.GetSharedData<bool>("Player:Death") == false)
                                        {
                                            if (tempData2.cuffed != 1)
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Der Spieler muss Handschellen anhaben, damit du Ihn durchsuchen kannst!", "error");
                                                return;
                                            }
                                            Helper.SendNotificationWithoutButton(player, "Du dursuchst jemanden!", "success");
                                            Helper.SendNotificationWithoutButton(getPlayer, "Du wirst durchsucht!", "success");
                                            Commands.cmd_animation(player, "give", true);
                                            ItemsController.OnShowInventory(player, 2);
                                        }
                                        else
                                        {
                                            player.SetData<Player>("Player:NearestPlayer", getPlayer);
                                            player.TriggerEvent("Client:StartLockpicking", 12, "searchdeath", "Tatort wird untersucht...");
                                        }
                                    }
                                    else
                                    {
                                        Helper.SendNotificationWithoutButton(player, "Es befindet sich kein Spieler in der Nähe!", "error");
                                    }
                                }
                                else if(character.faction == 2)
                                {
                                    if (character.factionduty == false)
                                    {
                                        Helper.SendNotificationWithoutButton(player, "Du hast keinen Rezeptblock dabei!", "error");
                                        return;
                                    }
                                    FactionController.OnFactionSettings(player, "wheel", "9");
                                }
                            }
                            else if (number == 4)
                            {
                                if (character.faction == 1)
                                {
                                    Commands.cmd_createspikestrip(player);
                                }
                                else if (character.faction == 2)
                                {
                                    if(character.factionduty == false)
                                    {
                                        Helper.SendNotificationWithoutButton(player, "Du hast keine Schmerztabletten dabei!", "error");
                                        return;
                                    }
                                    Player getPlayer = Helper.GetClosestPlayer(player, 2.5f);
                                    if (getPlayer != null)
                                    {
                                        TempData tempData2 = Helper.GetCharacterTempData(getPlayer);
                                        if (getPlayer.GetSharedData<bool>("Player:Death") == false)
                                        {
                                            if(getPlayer.Health < 100)
                                            {
                                                Commands.cmd_animation(player, "give", true);
                                                Helper.SetPlayerHealth(getPlayer, 100);
                                                Helper.SendNotificationWithoutButton(player, "Du hast der Person Schmerzmittel verabreicht!", "success");
                                                Helper.SendNotificationWithoutButton(getPlayer, "Dir wurden Schmerzmittel verabreicht!", "success");
                                                return;
                                            }
                                        }
                                        Helper.SendNotificationWithoutButton(player, "Die Person benötigt keine Schmerztabletten!", "error");
                                    }
                                    else
                                    {
                                        Helper.SendNotificationWithoutButton(player, "Es befindet sich kein Spieler in der Nähe!", "error");
                                    }
                                }
                            }
                            else if (number == 5)
                            {
                                Commands.cmd_createpoliceprop(player, "prop_mp_cone_01");
                            }
                            else if (number == 6)
                            {
                                Commands.cmd_createpoliceprop(player, "prop_barrier_work06a");
                            }
                            else if (number == 7)
                            {
                                Commands.cmd_createpoliceprop(player, "prop_barrier_work02a");
                            }
                            else if (number == 8)
                            {
                                if (character.faction == 1)
                                {
                                    if (Helper.IsInRangeOfPoint(player.Position, new Vector3(455.32404, -986.94196, 34.2013), 17.75f) || Helper.IsInRangeOfPoint(player.Position, new Vector3(583.8088, -12.374935, 76.62804), 25.5f))
                                    {
                                        Player getPlayer = Helper.GetClosestPlayer(player, 2.5f);
                                        if (getPlayer != null && getPlayer != player)
                                        {
                                            Character character2 = Helper.GetCharacterData(getPlayer);
                                            if (character2 != null && character2.faction != 1)
                                            {
                                                TempData tempData2 = Helper.GetCharacterTempData(getPlayer);
                                                if (tempData2.adminduty == false)
                                                {
                                                    if (character2.arrested == 0)
                                                    {
                                                        player.TriggerEvent("Client:ShowArrest", character2.name);
                                                    }
                                                    else
                                                    {
                                                        player.TriggerEvent("Client:PlayerFreeze", true);
                                                        player.TriggerEvent("Client:CallInput", "Haftentlassung", "Bitte gebe einen Haftentlassungsgrund ein!", "text", "Grund", 64, "ExitArrest");
                                                    }
                                                    return;
                                                }
                                            }
                                        }
                                        Helper.SendNotificationWithoutButton(player, "Es befindet sich keine Person in der Nähe, welche du inhaftieren könntest!", "error");
                                    }
                                    else
                                    {
                                        Helper.SendNotificationWithoutButton(player, "Du bist nicht in der Nähe von den Haftzellen!", "error");
                                    }
                                }
                                else if(character.faction == 3)
                                {
                                    if (player.IsInVehicle)
                                    {
                                        Helper.SendNotificationWithoutButton(player, "Du musst zuerst aus deinem Fahrzeug aussteigen!", "error");
                                        return;
                                    }
                                    Vehicle vehicle = Helper.GetClosestVehicle(player, 2.55f);
                                    if (vehicle == null)
                                    {
                                        Helper.SendNotificationWithoutButton(player, "Es befindet sich kein Fahrzeug in der Nähe!", "error");
                                        return;
                                    }
                                    Items item = ItemsController.GetItemByItemName(player, "Parkkralle");
                                    if (vehicle.GetSharedData<string>("Vehicle:Sync").Split(",")[6] == "0")
                                    {
                                        if (item == null)
                                        {
                                            Helper.SendNotificationWithoutButton(player, "Du hast keine Parkkralle dabei!", "error");
                                            return;
                                        }
                                        Helper.SendNotificationWithoutButton(player, "Parkkralle wird angebracht ...", "success", "top-left", 3445);
                                    }
                                    else
                                    {
                                        Helper.SendNotificationWithoutButton(player, "Parkkralle wird entfernt ...", "success", "top-left", 3445);
                                    }
                                    player.TriggerEvent("Client:PlayerFreeze", true);
                                    player.SetSharedData("Player:AnimData", $"anim@amb@clubhouse@tutorial@bkr_tut_ig3@%machinic_loop_mechandplayer%{1}");
                                    NAPI.Task.Run(() =>
                                    {
                                        int set = 0;
                                        Helper.SetVehicleEngine(vehicle, false);
                                        if (vehicle.GetSharedData<string>("Vehicle:Sync").Split(",")[6] == "0")
                                        {
                                            set = 1;
                                            Helper.SendNotificationWithoutButton(player, $"Parkkralle wurde angebracht!", "success", "top-left", 3500);
                                        }
                                        else
                                        {
                                            set = 0;
                                            Helper.SendNotificationWithoutButton(player, $"Parkkralle wurde entfernt!", "success", "top-left", 3500);
                                        }
                                        string[] vehicleArray = new string[7];
                                        vehicleArray = vehicle.GetSharedData<string>("Vehicle:Sync").Split(",");
                                        vehicle.SetSharedData("Vehicle:Sync", $"{vehicleArray[0]},{vehicleArray[1]},{vehicleArray[2]},{vehicleArray[3]},{vehicleArray[4]},{vehicleArray[5]},{set}");
                                        player.TriggerEvent("Client:PlayerFreeze", false);
                                        Helper.OnStopAnimation2(player);
                                        if (set == 1)
                                        {
                                            ItemsController.RemoveItem(player, item.itemid);
                                        }
                                    }, delayTime: 3450);
                                }
                            }
                            else if (number == 9)
                            {
                                if (!player.IsInVehicle)
                                {
                                    if (character.faction == 1)
                                    {
                                        Player getPlayer = Helper.GetClosestPlayer(player, 2.5f);
                                        if (getPlayer != null && getPlayer != player)
                                        {
                                            Character character2 = Helper.GetCharacterData(getPlayer);
                                            if (character2 != null && character2.faction != 1)
                                            {
                                                TempData tempData2 = Helper.GetCharacterTempData(getPlayer);
                                                if (tempData2.adminduty == false)
                                                {
                                                    player.TriggerEvent("Client:ShowTicket", character2.name);
                                                    return;
                                                }
                                            }
                                        }
                                        Helper.SendNotificationWithoutButton(player, "Es befindet sich keine Person in der Nähe, der du einen Strafzettel austellen kannst!", "error");
                                    }
                                    else
                                    {
                                        Player getPlayer = Helper.GetClosestPlayer(player, 2.5f);
                                        if(getPlayer == null)
                                        {
                                            getPlayer = player;
                                        }
                                        if (getPlayer != null)
                                        {
                                            Character character2 = Helper.GetCharacterData(getPlayer);
                                            if (character2 != null)
                                            {
                                                TempData tempData2 = Helper.GetCharacterTempData(getPlayer);
                                                if (tempData2.adminduty == false)
                                                {
                                                    player.TriggerEvent("Client:ShowRezept", character2.name);
                                                    return;
                                                }
                                            }
                                        }
                                        Helper.SendNotificationWithoutButton(player, "Es befindet sich keine Person in der Nähe, der du ein Rezept ausstellen kannst!", "error");
                                    }
                                }
                            }
                            break;
                        }
                    case "destroydrone":
                        {
                            if (player.IsInVehicle)
                            {
                                player.ResetData("Player:Drohne");
                                NAPI.Player.WarpPlayerOutOfVehicle(player);
                                NAPI.Task.Run(() =>
                                {                           
                                    Helper.SetPlayerPosition(player, tempData.furniturePosition);
                                    tempData.jobVehicle2.Delete();
                                    tempData.jobVehicle2 = null;
                                }, delayTime: 25);
                                Helper.SendNotificationWithoutButton(player, "Drohne wurde zerstört!", "error");
                            }
                            break;
                        }
                    case "factionphone":
                        {
                            FactionsModel factionsModel = Helper.GetFactionById(character.faction);
                            if (character.lastsmartphone == "")
                            {
                                Helper.SendNotificationWithoutButton2(player, "Du musst zuerst ein Smartphone ausrüsten!", "error", "center");
                                return;
                            }
                            if (factionsModel.number != character.lastsmartphone)
                            {
                                if (ItemsController.GetItemByItemName(player, "Smartphone") == null)
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Du besitzt kein Smartphone!", "error", "center");
                                    return;
                                }
                                factionsModel.number = character.lastsmartphone;
                                factionsModel.numbername = character.name;
                                Helper.SendNotificationWithoutButton2(player, "Du hast den Leitstellendienst übernommen!", "success", "center");
                            }
                            else
                            {
                                factionsModel.number = "";
                                factionsModel.numbername = "";
                                Helper.SendNotificationWithoutButton2(player, "Du hast den Leitstellendienst beendet!", "success", "center");
                            }
                            UpdateFactionStats(player);
                            break;
                        }
                    case "swat":
                        {
                            faction = FactionController.GetFactionById(character.faction);
                            int number = Convert.ToInt32(json);
                            Player newPlayer = Helper.GetPlayerByCharacterId(number);
                            if (faction != null)
                            {
                                Character character2 = Helper.GetCharacterData(newPlayer);
                                if (character2 == null)
                                {
                                    character2 = Helper.GetCharacterDataOffline(number);
                                }
                                if (character2 == null || character.faction != character2.faction)
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Ungültige Interaktion!", "error", "center");
                                    return;
                                }
                                if ((character.rang >= 10 && character.rang > character2.rang && character.id != faction.leader) || character.id == faction.leader)
                                {
                                    string charactername = Helper.GetCharacterName(number);
                                    if (character2.swat == 0)
                                    {
                                        character2.swat = 1;
                                        Helper.SendNotificationWithoutButton2(player, "SWAT-Status gesetzt!", "success", "center");
                                        Helper.CreateGroupLog(faction.id, $"{character.name} hat {charactername} den SWAT-Status gesetzt!");

                                    }
                                    else
                                    {
                                        character2.swat = 0;
                                        Helper.SendNotificationWithoutButton2(player, "SWAT-Status entzogen!", "success", "center");
                                        Helper.CreateGroupLog(faction.id, $"{character.name} hat {charactername} den SWAT-Status entzogen!");
                                    }

                                    PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                                    db.Save(character2);

                                    if (newPlayer != null)
                                    {
                                        newPlayer.SetOwnSharedData("Player:FactionRang", character2.rang);
                                        if (player != newPlayer)
                                        {
                                            newPlayer.TriggerEvent("Client:UpdateFactionRang");
                                            UpdateFactionStats(newPlayer);
                                        }
                                    }

                                    UpdateFactionStats(player);
                                }
                                else
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Keine Berechtigung!", "error", "center");
                                }
                            }
                            else
                            {
                                Helper.SendNotificationWithoutButton2(player, "Ungültige Interaktion!", "error", "center");
                            }
                            break;
                        }
                    case "uprank":
                        {
                            faction = FactionController.GetFactionById(character.faction);
                            int number = Convert.ToInt32(json);
                            Player newPlayer = Helper.GetPlayerByCharacterId(number);
                            if (faction != null)
                            {
                                Character character2 = Helper.GetCharacterData(newPlayer);
                                if (character2 == null)
                                {
                                    character2 = Helper.GetCharacterDataOffline(number);
                                }
                                if (character2 == null || character.faction != character2.faction)
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Ungültige Interaktion!", "error", "center");
                                    return;
                                }
                                if ((character.rang >= 10 && character.rang > character2.rang && character.id != faction.leader) || character.id == faction.leader)
                                {
                                    if (character2.rang < 12)
                                    {
                                        character2.rang++;
                                        Helper.SendNotificationWithoutButton2(player, "Der Spieler wurde befördert!", "success", "center");

                                        string charactername = Helper.GetCharacterName(number);
                                        Helper.CreateGroupLog(faction.id, $"{character.name} hat {charactername} befördert - (Neuer Rang: {character2.rang})!");

                                        PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                                        db.Save(character2);

                                        if (newPlayer != null)
                                        {
                                            newPlayer.SetOwnSharedData("Player:FactionRang", character2.rang);
                                            newPlayer.TriggerEvent("Client:UpdateFactionRang");
                                            if (player != newPlayer)
                                            {
                                                UpdateFactionStats(newPlayer);
                                            }
                                        }

                                        UpdateFactionStats(player);

                                    }
                                    else
                                    {
                                        Helper.SendNotificationWithoutButton2(player, "Ungültige Interaktion!", "error", "center");
                                    }
                                }
                                else
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Keine Berechtigung!", "error", "center");
                                }
                            }
                            else
                            {
                                Helper.SendNotificationWithoutButton2(player, "Ungültige Interaktion!", "error", "center");
                            }
                            break;
                        }
                    case "kick":
                        {
                            faction = FactionController.GetFactionById(character.faction);
                            int number = Convert.ToInt32(json);
                            Player newPlayer = Helper.GetPlayerByCharacterId(number);
                            if (faction != null)
                            {
                                Character character2 = Helper.GetCharacterData(newPlayer);
                                if (character2 == null)
                                {
                                    character2 = Helper.GetCharacterDataOffline(number);
                                }
                                if (character2.id == character.id)
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Du kannst dich nicht selber aus der Fraktion werfen!", "success", "center");
                                    return;
                                }
                                if (character2 == null || character.faction != character2.faction)
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Ungültige Interaktion!", "error", "center");
                                    return;
                                }
                                if ((character.rang >= 10 && character.rang > character2.rang && character.id != faction.leader) || character.id == faction.leader)
                                {
                                    if (character2.id == faction.leader)
                                    {
                                        Helper.SendNotificationWithoutButton2(player, "Der Leader kann nicht rausgeworfen werden!", "success", "center");
                                        return;
                                    }
                                    if(character2.factionduty == true)
                                    {
                                        Helper.SendNotificationWithoutButton2(player, "Der Spieler muss erst seinen Dienst beenden!", "success", "center");
                                        return;
                                    }

                                    Helper.SendNotificationWithoutButton2(player, "Der Spieler wurde aus der Fraktion geworfen!", "success", "center");

                                    string charactername = Helper.GetCharacterName(number);
                                    Helper.CreateGroupLog(faction.id, $"{character.name} hat {charactername} aus der Fraktion geworfen!");

                                    character2.faction = 0;
                                    character2.rang = 0;
                                    character2.swat = 0;
                                    character2.faction_dutytime = 0;
                                    character2.dutyjson = "{\"clothing\":[-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],\"clothingColor\":[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]}";
                                    character2.faction_since = Helper.UnixTimestamp();

                                    MySqlCommand command = General.Connection.CreateCommand();
                                    command.CommandText = "DELETE FROM outfits WHERE owner = @owner";
                                    command.Parameters.AddWithValue("@owner", "faction-" + character2.id);
                                    command.ExecuteNonQuery();

                                    if (newPlayer != null)
                                    {
                                        newPlayer.SetOwnSharedData("Player:Faction", 0);
                                        newPlayer.SetOwnSharedData("Player:FactionRang", 0);
                                        newPlayer.TriggerEvent("Client:UpdateFactionRang");
                                        TempData tempData2 = Helper.GetCharacterTempData(newPlayer);
                                        if (tempData.radio != "")
                                        {
                                            newPlayer.TriggerEvent("Client:Leaveradio", tempData2.radio);
                                            tempData2.radio = "";
                                            tempData2.radiols = false;
                                        }
                                        if (player != newPlayer)
                                        {
                                            UpdateFactionStats(newPlayer);
                                        }
                                    }

                                    PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                                    db.Save(character2);

                                    UpdateFactionStats(player);
                                }
                                else
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Keine Berechtigung!", "error", "center");
                                }
                            }
                            else
                            {
                                Helper.SendNotificationWithoutButton2(player, "Ungültige Interaktion!", "error", "center");
                            }
                            break;
                        }
                    case "downrank":
                        {
                            faction = FactionController.GetFactionById(character.faction);
                            int number = Convert.ToInt32(json);
                            Player newPlayer = Helper.GetPlayerByCharacterId(number);
                            if (faction != null)
                            {
                                Character character2 = Helper.GetCharacterData(newPlayer);
                                if (character2 == null)
                                {
                                    character2 = Helper.GetCharacterDataOffline(number);
                                }
                                if (character2 == null || character.faction != character2.faction)
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Ungültige Interaktion!", "error", "center");
                                    return;
                                }
                                if ((character.rang >= 10 && character.rang > character2.rang && character.id != faction.leader) || character.id == faction.leader)
                                {
                                    if (character2.rang > 1)
                                    {
                                        character2.rang--;
                                        Helper.SendNotificationWithoutButton2(player, "Der Spieler wurde degradiert!", "success", "center");

                                        string charactername = Helper.GetCharacterName(number);
                                        Helper.CreateGroupLog(faction.id, $"{character.name} hat {charactername} degradiert - (Neuer Rang: {character2.rang})!");

                                        PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                                        db.Save(character2);

                                        if (newPlayer != null)
                                        {
                                            newPlayer.SetOwnSharedData("Player:FactionRang", character2.rang);
                                            newPlayer.TriggerEvent("Client:UpdateFactionRang");
                                            if (player != newPlayer)
                                            {
                                                UpdateFactionStats(newPlayer);
                                            }
                                        }

                                        UpdateFactionStats(player);
                                    }
                                    else
                                    {
                                        Helper.SendNotificationWithoutButton2(player, "Ungültige Interaktion!", "error", "center");
                                    }
                                }
                                else
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Keine Berechtigung!", "error", "center");
                                }
                            }
                            else
                            {
                                Helper.SendNotificationWithoutButton2(player, "Ungültige Interaktion!", "error", "center");
                            }
                            break;
                        }
                    case "leave":
                        {
                            faction = FactionController.GetFactionById(character.faction);
                            if (faction != null)
                            {
                                if (character.id == faction.leader)
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Du kannst die Fraktion nicht verlassen!", "error", "center");
                                    player.TriggerEvent("Client:HideCursor");
                                    player.TriggerEvent("Client:ShowHud");
                                    return;
                                }

                                if(character.factionduty == true)
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Du musst zuerst deinen Dienst beenden!", "error", "center");
                                    player.TriggerEvent("Client:HideCursor");
                                    player.TriggerEvent("Client:ShowHud");
                                    return;
                                }

                                if (faction.number != "" && faction.numbername == character.name)
                                {
                                    faction.number = "";
                                    faction.numbername = "";
                                }

                                if (character.faction == 1)
                                {
                                    if (character.gender == 1)
                                    {
                                        character.armor = 2;
                                        character.armorcolor = 0;
                                    }
                                    else
                                    {
                                        character.armor = 2;
                                        character.armorcolor = 0;
                                    }
                                    if (NAPI.Player.GetPlayerArmor(player) > 0)
                                    {
                                        NAPI.Player.SetPlayerClothes(player, 9, character.armor, character.armorcolor);
                                    }
                                }

                                character.faction = 0;
                                character.rang = 0;
                                character.swat = 0;
                                character.faction_dutytime = 0;
                                character.dutyjson = "{\"clothing\":[-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],\"clothingColor\":[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]}";
                                character.faction_since = Helper.UnixTimestamp();

                                MySqlCommand command = General.Connection.CreateCommand();
                                command.CommandText = "DELETE FROM outfits WHERE owner = @owner";
                                command.Parameters.AddWithValue("@owner", "faction-"+character.id);
                                command.ExecuteNonQuery();

                                Helper.CreateFactionLog(faction.id, $"{character.name} hat die Fraktion verlassen!");

                                Helper.CreateUserTimeline(account.id, character.id, $"Fraktion {faction.name} verlassen", 4);

                                Helper.SendNotificationWithoutButton2(player, "Du hast die Fraktion verlassen!", "success", "center");

                                if (tempData.radio != "")
                                {
                                    player.TriggerEvent("Client:Leaveradio", tempData.radio);
                                    tempData.radio = "";
                                    tempData.radiols = false;
                                }

                                CharacterController.SaveCharacter(player);

                                player.TriggerEvent("Client:HideCursor");
                                player.TriggerEvent("Client:ShowHud");


                                player.SetOwnSharedData("Player:Faction", 0);
                                player.SetOwnSharedData("Player:FactionRang", 0);
                            }
                            else
                            {
                                Helper.SendNotificationWithoutButton2(player, "Ungültige Interaktion!", "error", "center");
                                player.TriggerEvent("Client:HideCursor");
                                player.TriggerEvent("Client:ShowHud");
                            }
                            break;
                        }
                    case "invite":
                        {
                            faction = GetFactionById(character.faction);
                            if (faction != null)
                            {
                                if (character.rang >= 10)
                                {

                                    player.TriggerEvent("Client:CallInput", "Fraktionseinladung", "Wen möchtest du einladen?", "text", "Fraktionseinladung", 35, "invitefaction");
                                }
                                else
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Keine Berechtigung!", "error", "center");
                                }
                            }
                            else
                            {
                                Helper.SendNotificationWithoutButton2(player, "Ungültige Interaktion!", "error", "center");
                            }
                            break;
                        }
                    case "factionlog":
                        {
                            faction = GetFactionById(character.faction);
                            if (faction != null)
                            {
                                if (character.rang >= 10)
                                {
                                    string loglabel = "faction-" + faction.id;
                                    player.TriggerEvent("Client:ShowLog", NAPI.Util.ToJson(Helper.GetLogEntries(loglabel)), "Fraktionslog");
                                }
                                else
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Keine Berechtigung!", "error", "center");
                                }
                            }
                            break;
                        }
                    case "weaponlog":
                        {
                            faction = GetFactionById(character.faction);
                            if (faction != null)
                            {
                                if (character.rang >= 10)
                                {
                                    string loglabel = "weapon-" + faction.id;
                                    if(character.faction == 4)
                                    {
                                        loglabel = "govmoney";
                                    }
                                    player.TriggerEvent("Client:ShowLog", NAPI.Util.ToJson(Helper.GetLogEntries(loglabel)), "Waffenkammerlog");
                                }
                                else
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Keine Berechtigung!", "error", "center");
                                }
                            }
                            break;
                        }
                    case "asservatenlog":
                        {
                            faction = GetFactionById(character.faction);
                            if (faction != null)
                            {
                                if (character.rang >= 10)
                                {
                                    string loglabel = "evidence";
                                    player.TriggerEvent("Client:ShowLog", NAPI.Util.ToJson(Helper.GetLogEntries(loglabel)), "Asservatenkammerlog");
                                }
                                else
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Keine Berechtigung!", "error", "center");
                                }
                            }
                            break;
                        }
                    case "updaterangs":
                        {
                            faction = GetFactionById(character.faction);
                            if (faction != null)
                            {
                                if (character.rang >= 10)
                                {
                                    try
                                    {
                                        FactionRangs factionRangsTemp = new FactionRangs();
                                        FactionRangs factionRangs = new FactionRangs();

                                        JObject obj = JObject.Parse(json);

                                        if (obj == null)
                                        {
                                            Helper.SendNotificationWithoutButton2(player, "Ungültige Eingabe!", "error", "center");
                                            return;
                                        }

                                        factionRangs.factionid = faction.id;
                                        factionRangs.rang1 = (string)obj["rang1"];
                                        factionRangs.rang2 = (string)obj["rang2"];
                                        factionRangs.rang3 = (string)obj["rang3"];
                                        factionRangs.rang4 = (string)obj["rang4"];
                                        factionRangs.rang5 = (string)obj["rang5"];
                                        factionRangs.rang6 = (string)obj["rang6"];
                                        factionRangs.rang7 = (string)obj["rang7"];
                                        factionRangs.rang8 = (string)obj["rang8"];
                                        factionRangs.rang9 = (string)obj["rang9"];
                                        factionRangs.rang10 = (string)obj["rang10"];
                                        factionRangs.rang11 = (string)obj["rang11"];
                                        factionRangs.rang12 = (string)obj["rang12"];

                                        if (factionRangs.rang1.Length < 3 || factionRangs.rang1.Length > 50 || factionRangs.rang2.Length < 3 || factionRangs.rang2.Length > 50 || factionRangs.rang3.Length < 3 || factionRangs.rang3.Length > 50 || factionRangs.rang4.Length < 3 || factionRangs.rang4.Length > 50
                                        || factionRangs.rang5.Length < 3 || factionRangs.rang5.Length > 50 || factionRangs.rang6.Length < 3 || factionRangs.rang6.Length > 50 || factionRangs.rang7.Length < 3 || factionRangs.rang7.Length > 50 || factionRangs.rang8.Length < 3 || factionRangs.rang8.Length > 50
                                        || factionRangs.rang9.Length < 3 || factionRangs.rang9.Length > 50 || factionRangs.rang10.Length < 3 || factionRangs.rang10.Length > 50 || factionRangs.rang11.Length < 3 || factionRangs.rang11.Length > 50 || factionRangs.rang12.Length < 3 || factionRangs.rang12.Length > 50)
                                        {
                                            Helper.SendNotificationWithoutButton2(player, "Ungültige Eingabe!", "error", "center");
                                            return;
                                        }

                                        int tempid = 0;
                                        int groupid = 0;
                                        foreach (FactionRangs factionRangs2 in FactionController.factionRangList)
                                        {
                                            if (factionRangs2 != null && factionRangs2.factionid == faction.id)
                                            {
                                                factionRangsTemp = factionRangs2;
                                                tempid = factionRangs2.id;
                                                groupid = factionRangs2.factionid;
                                                break;
                                            }
                                        }

                                        factionRangs.id = tempid;
                                        factionRangs.factionid = groupid;

                                        factionRangList.Remove(factionRangsTemp);
                                        factionRangsTemp = null;
                                        factionRangList.Add(factionRangs);

                                        PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                                        db.Save(factionRangs);

                                        Helper.CreateFactionLog(faction.id, $"{character.name} hat hat die Rangnamen aktualisiert!");

                                        player.TriggerEvent("Client:ShowFactionSettings", NAPI.Util.ToJson(GetFactionRangsById(faction.id)), NAPI.Util.ToJson(GetFactionSalarysById(faction.id)), FactionController.GetFactionBudgetById(faction.id), 1);

                                        Helper.SendNotificationWithoutButton2(player, "Die Rangnamen wurden aktualisiert!", "success", "center");
                                    }
                                    catch (Exception)
                                    {
                                        Helper.SendNotificationWithoutButton2(player, "Ungültige Eingabe!", "error", "center");
                                    }
                                }
                                else
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Keine Berechtigung!", "error", "center");
                                }
                            }
                            break;
                        }
                    case "factionsalary":
                        {
                            faction = GetFactionById(character.faction);
                            if (faction != null)
                            {
                                if (character.rang >= 10)
                                {
                                    try
                                    {
                                        FactionSalary factionSalaryTemp = new FactionSalary();
                                        FactionSalary factionSalary = new FactionSalary();

                                        JObject obj = JObject.Parse(json);

                                        if (obj == null)
                                        {
                                            Helper.SendNotificationWithoutButton2(player, "Ungültige Eingabe!", "error", "center");
                                            return;
                                        }

                                        factionSalary.factionid = faction.id;
                                        factionSalary.rang1 = (int)obj["rang1"];
                                        factionSalary.rang2 = (int)obj["rang2"];
                                        factionSalary.rang3 = (int)obj["rang3"];
                                        factionSalary.rang4 = (int)obj["rang4"];
                                        factionSalary.rang5 = (int)obj["rang5"];
                                        factionSalary.rang6 = (int)obj["rang6"];
                                        factionSalary.rang7 = (int)obj["rang7"];
                                        factionSalary.rang8 = (int)obj["rang8"];
                                        factionSalary.rang9 = (int)obj["rang9"];
                                        factionSalary.rang10 = (int)obj["rang10"];
                                        factionSalary.rang11 = (int)obj["rang11"];
                                        factionSalary.rang12 = (int)obj["rang12"];

                                        if (factionSalary.rang1 < 0 || factionSalary.rang1 > 999999 || factionSalary.rang2 < 0 || factionSalary.rang2 > 999999 || factionSalary.rang3 < 0 || factionSalary.rang3 > 999999 || factionSalary.rang4 < 0 || factionSalary.rang4 > 999999 || factionSalary.rang5 < 0 || factionSalary.rang5 > 999999 || factionSalary.rang6 < 0 || factionSalary.rang6 > 999999 || factionSalary.rang7 < 0 || factionSalary.rang7 > 999999
                                        || factionSalary.rang8 < 0 || factionSalary.rang8 > 999999 || factionSalary.rang9 < 0 || factionSalary.rang9 > 999999 || factionSalary.rang10 < 0 || factionSalary.rang10 > 999999 || factionSalary.rang11 < 0 || factionSalary.rang11 > 999999 || factionSalary.rang12 < 0 || factionSalary.rang12 > 999999)
                                        {
                                            Helper.SendNotificationWithoutButton2(player, "Ungültige Eingabe!", "error", "center");
                                            return;
                                        }

                                        int factionBudget = FactionController.GetFactionBudgetById(character.faction);
                                        int count = factionSalary.rang1 + factionSalary.rang2 + factionSalary.rang3 + factionSalary.rang4 + factionSalary.rang5 + factionSalary.rang6 + factionSalary.rang7 + factionSalary.rang8 + factionSalary.rang9 + factionSalary.rang10 + factionSalary.rang11 + factionSalary.rang12;

                                        if (count > factionBudget)
                                        {
                                            Helper.SendNotificationWithoutButton2(player, "Lohnbudget überschritten!", "error", "center", 3500);
                                            return;
                                        }

                                        int tempid = 0;
                                        int groupid = 0;
                                        foreach (FactionSalary factionSalary2 in FactionController.factionSalaryList)
                                        {
                                            if (factionSalary2 != null && factionSalary2.factionid == faction.id)
                                            {
                                                factionSalaryTemp = factionSalary2;
                                                tempid = factionSalary2.id;
                                                groupid = factionSalary2.factionid;
                                                break;
                                            }
                                        }

                                        factionSalary.id = tempid;
                                        factionSalary.factionid = groupid;

                                        factionSalaryList.Remove(factionSalaryTemp);
                                        factionSalaryTemp = null;
                                        factionSalaryList.Add(factionSalary);

                                        PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                                        db.Save(factionSalary);

                                        Helper.CreateFactionLog(faction.id, $"{character.name} hat hat die Lohneinstellungen aktualisiert!");

                                        player.TriggerEvent("Client:ShowFactionSettings", NAPI.Util.ToJson(GetFactionRangsById(faction.id)), NAPI.Util.ToJson(GetFactionSalarysById(faction.id)), FactionController.GetFactionBudgetById(faction.id), 1);

                                        Helper.SendNotificationWithoutButton2(player, "Die Lohneinstellungen wurden aktualisiert!", "success", "center");
                                    }
                                    catch (Exception)
                                    {
                                        Helper.SendNotificationWithoutButton2(player, "Ungültige Eingabe!", "error", "center");
                                    }
                                }
                                else
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Keine Berechtigung!", "error", "center");
                                }
                            }
                            break;
                        }
                    case "showfactionrangs":
                        {
                            faction = GetFactionById(character.faction);
                            if (faction != null)
                            {
                                if (character.rang >= 10)
                                {
                                    player.TriggerEvent("Client:ShowFactionSettings", NAPI.Util.ToJson(GetFactionRangsById(faction.id)), NAPI.Util.ToJson(GetFactionSalarysById(faction.id)), FactionController.GetFactionBudgetById(faction.id), 0);
                                }
                                else
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Keine Berechtigung!", "error", "center");
                                }
                            }
                            break;
                        }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnFactionSettings]: " + e.ToString());
            }
        }

        public static void SendFactionInfoMessage(string message, int faction, int extra)
        {
            try
            {
                foreach (Player p in NAPI.Pools.GetAllPlayers())
                {
                    if (p != null && p.GetOwnSharedData<bool>("Player:Spawned") == true && p.GetSharedData<bool>("Player:Death") == false)
                    {
                        Character character = Helper.GetCharacterData(p);
                        if (character != null && character.faction == faction && character.factionduty == true && character.arrested == 0)
                        {
                            Helper.SendNotificationWithoutButton(p, message, "info", "top-left", 3250);
                            if(character.faction == 5 && extra == 1)
                            {
                                OnLoadLifeInvader(p, 0);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[SendFactionInfoMessage]: " + e.ToString());
            }
        }

        public static void GetAllLifeInvaderAds()
        {
            try
            {
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                foreach (LifeInvaderAds ads in db.Fetch<LifeInvaderAds>("SELECT * FROM lifeinvaderads"))
                {
                    lifeInvaderAdsList.Add(ads);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetAllLifeInvaderAds]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:LoadLifeInvader")]
        public static void OnLoadLifeInvader(Player player, int show = 1)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                if (character == null) return;

                int isInFaction = -1;
                int online = Helper.GetFactionCountDuty(5);
                if(character.faction == 5 && character.factionduty == true)
                {
                    isInFaction = 1;
                }

                player.TriggerEvent("Client:ShowLifeInvader", NAPI.Util.ToJson(lifeInvaderAdsList.OrderByDescending(l => l.editor).ThenBy(l => l.timestamp).TakeLast(35).ToList()), show, isInFaction, character.name, online, Helper.adminSettings.adprice);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnLoadLifeInvader]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:CreateAd")]
        public static void OnCreateAd(Player player, string text)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                if (character == null) return;

                int price = Helper.adminSettings.adprice;

                Bank bank = BankController.GetDefaultBank(player, character.defaultbank);
                if (bank == null)
                {
                    Helper.SendNotificationWithoutButton2(player, "Es wurde kein Standardkonto gefunden!", "error", "center");
                    return;
                }
                if (bank.bankvalue < Convert.ToInt32(price))
                {
                    Helper.SendNotificationWithoutButton2(player, $"Soviel Geld liegt nicht auf dem Konto - {price}$!", "error", "center");
                    return;
                }

                LifeInvaderAds lifeInvaderAds = new LifeInvaderAds();
                lifeInvaderAds.name = character.name;
                lifeInvaderAds.number = character.lastsmartphone;
                lifeInvaderAds.status = "Offen";
                lifeInvaderAds.text = text;
                lifeInvaderAds.timestamp = Helper.UnixTimestamp();
                lifeInvaderAds.editor = "AAA AAA";

                bank.bankvalue -= price;
                Helper.adminSettings.adcount ++;
                Helper.adminSettings.govvalue = price/2;
                Helper.adminSettings.admoney += price/2;

                SendFactionInfoMessage("Neue Anzeige/n vorhanden!", 5, 1);

                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                db.Insert(lifeInvaderAds);

                lifeInvaderAdsList.Add(lifeInvaderAds);

                Helper.SendNotificationWithoutButton(player, "Anzeige aufgegeben, diese wird jetzt durch das Lifeinvader Team freigegeben!", "info", "top-left", 4750);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnCreateAd]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:ClaimAd")]
        public static void OnClaimAd(Player player, int adnumber)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                if (character == null) return;

                LifeInvaderAds lifeInvaderAds = GetLifeinvaderAdById(adnumber);
                if (lifeInvaderAds != null)
                {
                    if(lifeInvaderAds.editor == "AAA AAA")
                    {
                        PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                        character.adcount++;
                        Helper.SendNotificationWithoutButton(player, "Anzeigen Bearbeitung übernommen, du kannst die Anzeige ist bearbeiten!", "info", "top-left", 4750);
                        lifeInvaderAds.editor = character.name;
                        lifeInvaderAds.status = "Bearbeitung";
                        db.Insert(lifeInvaderAds);
                        OnLoadLifeInvader(player, 0);
                    }
                    else
                    {
                        Helper.SendNotificationWithoutButton(player, "Diese Anzeige wird schon bearbeitet!", "error", "top-left", 4750);
                    }
                }
                else
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültige Anzeige!", "error", "top-left", 4750);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnClaimAd]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:AcceptAd")]
        public static void OnAcceptAd(Player player, String text, int adnumber)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                if (character == null) return;

                LifeInvaderAds lifeInvaderAds = GetLifeinvaderAdById(adnumber);
                if (lifeInvaderAds != null)
                {
                    if (lifeInvaderAds.editor == character.name)
                    {
                        PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                        Helper.SendNotificationWithoutButton(player, "Anzeige erfolgreich bearbeitet, diese wird in wenigen Sekunden freigegeben!", "success", "top-left", 4750);
                        lifeInvaderAds.status = "Freigegeben";
                        lifeInvaderAds.text = text;
                        db.Update(lifeInvaderAds);
                        SmartphoneController.OnSmartphoneMessage(player, Helper.UnixTimestamp(), lifeInvaderAds.number, "0189914", "Lifeinvader: Deine Anzeige wurde bearbeitet und wird in wenigen Sekunden freigegeben!");
                        OnLoadLifeInvader(player, 0);
                    }
                    else
                    {
                        Helper.SendNotificationWithoutButton(player, "Du kannst diese Anzeige nicht freigeben!", "error", "top-left", 4750);
                    }
                }
                else
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültige Anzeige!", "error", "top-left", 4750);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnAcceptAd]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:DeleteAd")]
        public static void OnDeleteAd(Player player, int adnumber)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                if (character == null) return;

                LifeInvaderAds lifeInvaderAds = GetLifeinvaderAdById(adnumber);
                if (lifeInvaderAds != null)
                {
                    if (lifeInvaderAds.editor == character.name)
                    {
                        PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                        Helper.SendNotificationWithoutButton(player, "Die Anzeige wurde gelöscht!", "success", "top-left", 4750);
                        SmartphoneController.OnSmartphoneMessage(player, Helper.UnixTimestamp(), lifeInvaderAds.number, "0189914", "Lifeinvader: Deine Anzeige wurde gelöscht, diese entsprach nicht unseren Richtlinien!");
                        lifeInvaderAds.status = "Gelöscht";
                        db.Delete(lifeInvaderAds);
                        lifeInvaderAdsList.Remove(lifeInvaderAds);
                        OnLoadLifeInvader(player, 0);
                    }
                    else
                    {
                        Helper.SendNotificationWithoutButton(player, "Du kannst diese Anzeige nicht löschen!", "error", "top-left", 4750);
                    }
                }
                else
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültige Anzeige!", "error", "top-left", 4750);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnDeleteAd]: " + e.ToString());
            }
        }

        public static LifeInvaderAds GetLifeinvaderAdById(int adnumber)
        {
            foreach(LifeInvaderAds lifeInvaderAds in lifeInvaderAdsList)
            {
                if(lifeInvaderAds.id == adnumber)
                {
                    return lifeInvaderAds;
                }
            }
            return null;
        }
    }
}
