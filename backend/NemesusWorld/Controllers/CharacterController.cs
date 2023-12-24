using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using GTANetworkAPI;
using MySql.Data.MySqlClient;
using NemesusWorld.Controllers;
using NemesusWorld.Database;
using NemesusWorld.Models;
using NemesusWorld.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NemesusWorld.Controllers
{
    class CharacterController : Script
    {
        public static void GetAvailableCharacters(Player player, int userid, bool delete = false)
        {
            try
            {
                int count = 0;

                MySqlCommand command = General.Connection.CreateCommand();
                command.CommandText = "SELECT id,name,cash,bank,job,faction,screen,closed FROM characters WHERE userid=@userid AND closed = 0 LIMIT 3";
                command.Parameters.AddWithValue("@userid", userid);

                List<LoadCharactersModel> loadedCharactersList = new List<LoadCharactersModel>();

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        LoadCharactersModel loadCharacter = new LoadCharactersModel();
                        loadCharacter.ID = reader.GetInt32("id");
                        loadCharacter.Name = reader.GetString("name");
                        loadCharacter.Cash = reader.GetInt32("cash");
                        loadCharacter.Bank = reader.GetInt32("bank");
                        loadCharacter.Job = Helper.GetJobName(reader.GetInt32("job"));
                        loadCharacter.Faction = Helper.GetFactionTag(reader.GetInt32("faction"));
                        loadCharacter.Closed = reader.GetInt16("closed");
                        loadCharacter.Screen = reader.GetString("screen");
                        loadedCharactersList.Add(loadCharacter);
                        count++;
                    }
                    reader.Close();
                }
                player.SetOwnSharedData("Player:Spawned", false);
                player.TriggerEvent("Client:ShowCharacterSwitch", NAPI.Util.ToJson(loadedCharactersList), count);
                player.TriggerEvent("Client:ShowLoading");

                if (delete == true)
                {
                    player.TriggerEvent("Client:CharacterDelete");
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetAvailableCharacters]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:CharacterChangeGender")]
        public static void OnCharacterChangeGender(Player player, bool model, bool setClothes = true)
        {
            try
            {
                if (model == true)
                {
                    NAPI.Player.SetPlayerSkin(player, 0x705E61F2);
                }
                else
                {
                    NAPI.Player.SetPlayerSkin(player, 0x9C9EFFD8);
                }
                if (setClothes == true)
                {
                    NAPI.Task.Run(() =>
                    {
                        player.TriggerEvent("Client:CharacterChangeClothes");
                    }, delayTime: 125);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnCharacterChangeGender]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:CreateCharacter")]
        public void OnCreateCharacter(Player player)
        {
            try
            {
                Account account = Helper.GetAccountData(player);
                if (account == null) return;
                int charcount = CountCharacterCount(player, account.id);
                if (account.level < 3 && charcount > 1)
                {
                    Helper.SendNotificationWithoutButton2(player, "Du benötigst mind. Level 3 um mehr als einen Charakter spielen zu können!", "error", "center");
                    return;
                }
                player.TriggerEvent("Client:DestroyLoginCamera");
                Helper.SetPlayerPosition(player, new Vector3(-1239.7445, -761.4464, 20.826468 + 0.1));
                player.Heading = -146;
                player.Dimension = Convert.ToUInt32(player.Id) + 1;
                player.TriggerEvent("Client:HideCharacterSwitch");
                NAPI.Task.Run(() =>
                {
                    player.TriggerEvent("Client:ShowCharacterCreator");
                }, delayTime: 500);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnCreateCharacter]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:FinishEinreise")]
        public static void OnFinishEinreise(Player player, string eyecolor, string education, string skills, string appearance, string size)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                Account account = Helper.GetAccountData(player);
                if (character == null || account == null)
                {
                    AntiCheatController.SetKick(player, "Ungültige Registrierung", "Server", false, false);
                    return;
                }
                character.eyecolor = eyecolor;
                character.education = education;
                character.skills = skills;
                character.appearance = appearance;
                character.size = size;
                character.tutorialstep += 3;
                player.TriggerEvent("Client:PlayerFreeze", false);
                character.health = 100;
                if (character.gender == 1)
                {
                    character.armor = 4;
                }
                else
                {
                    character.armor = 5;
                }
                character.hunger = 100;
                character.thirst = 100;
                NAPI.Task.Run(() =>
                {
                    player.SetOwnSharedData("Player:Needs", character.hunger + "," + character.thirst + "," + character.afk);

                    Helper.SetPlayerHealth(player, character.health);
                }, delayTime: 250);
                Random rand = new Random();
                if (character.tutorialstep == 3 || character.tutorialstep == 4)
                {
                    Vector3[] spawnArray = new Vector3[]
                    {
                        new Vector3(-537.8234, -204.58244, 37.649708),
                        new Vector3(-543.2053, -207.19386, 37.64981),
                        new Vector3(-548.3059, -210.55812, 37.649796),
                        new Vector3(-541.04584, -211.49368, 37.64975)
                    };
                    int index = rand.Next(spawnArray.Length);
                    Helper.SetPlayerPosition(player, spawnArray[index]);
                    player.Heading = -141.01595f;
                    player.Rotation = new Vector3(0.0, 0.0, -141.01595);
                    player.Dimension = 0;
                    NAPI.Task.Run(() =>
                    {
                        Helper.SyncThings(player);
                        player.TriggerEvent("Client:ShowHud", player.Id);
                        Helper.SendChatMessage(player, "~b~Willkommen auf Nemesus World " + account.name + ", schau dir doch mal das F2 Menü an, dort findest du auch wichtige Tipps und den Reiter - Erste Schritte für deinen Anfang auf diesem Server!");
                        Helper.SendChatMessage(player, "~b~Dieser Gamemode wurde von Nemesus.de entwickelt!");
                        if (Helper.adminSettings.voicerp == 1)
                        {
                            Helper.CheckSaltyChat(player);
                            player.TriggerEvent("SaltyChat_InitToTalkClient", player.Id);
                        }
                        player.SetOwnSharedData("Player:Spawned", true);
                        CharacterController.SaveCharacter(player);
                    }, delayTime: 11000);
                }
                else
                {
                    Helper.SyncThings(player);
                    Vector3[] spawnArray = new Vector3[]
                    {
                        new Vector3(159.84827, -3322.966, 5.4844017-0.1),
                        new Vector3(160.03633, -3318.4407, 5.4802113-0.1),
                        new Vector3(160.30829, -3313.08, 5.5006375-0.1),
                        new Vector3(161.14297, -3307.1133, 5.4560013-0.1),
                        new Vector3(167.70392, -3307.311, 5.3469024-0.1)
                    };
                    int index = rand.Next(spawnArray.Length);
                    Random rand2 = new Random();
                    Helper.SetPlayerPosition(player, spawnArray[index]);
                    player.Dimension = 0;
                    TempData tempData = Helper.GetCharacterTempData(player);
                    tempData.rentVehicle = Cars.createNewCar("faggio", spawnArray[index], -133.2003f, rand2.Next(0, 159), rand2.Next(0, 159), "LS-S-100" + player.Id, "Rollerverleih", true, true, false, 0);
                    tempData.rentVehicle.Dimension = 0;
                    player.TriggerEvent("Client:PlayerFreeze", false);
                    player.SetIntoVehicle(tempData.rentVehicle, (int)VehicleSeat.Driver);
                    CharacterController.SetMoney(player, 2500);
                    NAPI.Task.Run(() =>
                    {
                        player.TriggerEvent("Client:ShowHud", player.Id);
                        Helper.SendChatMessage(player, "~b~Willkommen auf Nemesus World " + account.name + ", schau dir doch mal das F2 Menü an, dort findest du auch wichtige Tipps und den Reiter - Erste Schritte für deinen Anfang auf diesem Server!");
                        Helper.SendChatMessage(player, "~b~Dieser Gamemode wurde von Nemesus.de entwickelt!");
                        player.SetOwnSharedData("Player:Spawned", true);
                        CharacterController.SaveCharacter(player);
                        if (Helper.adminSettings.voicerp == 1)
                        {
                            Helper.CheckSaltyChat(player);
                            player.TriggerEvent("SaltyChat_InitToTalkClient", player.Id);
                        }
                    }, delayTime: 11000);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnFinishEinreise]: " + e.ToString());
            }
        }

        public static int GetRowFromCharacter(Player player, int characterid)
        {
            try
            {
                Account account = Helper.GetAccountData(player);
                if (account == null) return 0;
                MySqlCommand command = General.Connection.CreateCommand();
                command.CommandText = "SELECT id FROM characters WHERE userid=@userid ORDER BY id ASC";
                command.Parameters.AddWithValue("@userid", account.id);
                int count = 0, id = 0;

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        reader.Read();
                        id = reader.GetInt32("id");
                        if (id == characterid)
                        {
                            reader.Close();
                            return count;
                        }
                        count++;
                    }
                }
                return count;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetRowFromCharacter]: " + e.ToString());
            }
            return 0;
        }

        public static int CountCharacterCount(Player player, int accid)
        {
            try
            {
                MySqlCommand command = General.Connection.CreateCommand();
                command.CommandText = "SELECT COUNT(*) as count FROM characters WHERE userid=@userid AND closed = 0";
                command.Parameters.AddWithValue("@userid", accid);
                int count = 0;

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if(reader.HasRows)
                    {
                        reader.Read();
                        count = reader.GetInt32("count");
                    }
                    reader.Close();
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CountCharacterCount]: " + e.ToString());
            }
            return 0;
        }

        [RemoteEvent("Server:SelectCharacter")]
        public static void OnSelectCharacter(Player player, int characterid)
        {
            try
            {
                Account account = Helper.GetAccountData(player);

                TempData tempData = new TempData();
                player.SetData(Helper.GetTempData(), tempData);

                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                Character character = db.Single<Character>("WHERE id = @0", characterid);

                string weaponHash = "";

                character.lastonline = Helper.UnixTimestamp();

                player.Name = character.name;
                player.SetData(Helper.GetCharacterKey(), character);

                account.selectedcharacter = GetRowFromCharacter(player, characterid);
                account.selectedcharacterintern = character.id;

                if (character.items.Length > 5)
                {
                    tempData.itemlist = JsonConvert.DeserializeObject<List<Items>>(character.items);
                    foreach (Items item in tempData.itemlist)
                    {
                        if (item != null)
                        {
                            item.itemid = ItemsController.GetFreeItemID(player);
                            if (item.type == 5)
                            {
                                string[] weaponArray = new string[7];
                                weaponArray = item.props.Split(",");
                                if (weaponArray[1] == "1")
                                {
                                    if (!item.description.ToLower().Contains("schutzweste"))
                                    {
                                        weaponHash = WeaponController.GetWeaponHash2FromName(item.description);
                                        NAPI.Player.GivePlayerWeapon(player, (WeaponHash)WeaponController.GetWeaponHashFromName(item.description), Convert.ToInt32(weaponArray[0]));
                                        tempData.weaponTints.Add($"{weaponHash.ToLower()}", $"{weaponArray[5]}");
                                        if (weaponArray[6].Length > 1)
                                        {
                                            List<String> weaponComponents = new List<String>();
                                            weaponComponents = weaponArray[6].Split("|").ToList();
                                            weaponComponents.RemoveAll(s => String.IsNullOrWhiteSpace(s));
                                            if (weaponArray[6] != "|")
                                            {
                                                tempData.weaponComponents.Add($"{weaponHash.ToLower()}", $"{weaponArray[6]}");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        int armor = Convert.ToInt32(weaponArray[0]);
                                        if (armor > 99)
                                        {
                                            armor = 99;
                                        }
                                        Helper.SetPlayerArmor(player, armor);
                                        NAPI.Player.SetPlayerClothes(player, 9, Convert.ToInt32(character.armor), 0);
                                    }
                                }
                            }
                        }
                    }
                    NAPI.Player.SetPlayerCurrentWeapon(player, WeaponHash.Unarmed);
                }

                JObject obj = JObject.Parse(character.json);

                bool gender = (bool)obj["gender"];

                OnCharacterChangeGender(player, gender, false);

                character.gender = gender == true ? 1 : 2;

                SetCharacterJson(player, obj, character.clothing);

                Helper.GetCharacterTattoos(player, character.id);

                player.SetSharedData("Player:WalkingStyle", character.walkingstyle);
                player.SetOwnSharedData("Player:CharacterName", character.name);
                player.SetOwnSharedData("Player:Job", character.job);

                if (character.mygroup != -1)
                {
                    Groups group = GroupsController.GetGroupById(character.mygroup);
                    if (group != null)
                    {
                        player.SetOwnSharedData("Player:Group", group.id);
                        player.SetOwnSharedData("Player:GroupRang", GroupsController.GetGroupRangByCharacterID(character.id, group.id));
                    }
                    else
                    {
                        player.SetOwnSharedData("Player:Group", -1);
                        player.SetOwnSharedData("Player:GroupRang", -1);
                    }
                }

                player.SetOwnSharedData("Player:Faction", character.faction);
                player.SetOwnSharedData("Player:FactionRang", character.rang);

                character.online = 1;

                MySqlCommand command = General.Connection.CreateCommand();
                command.CommandText = "UPDATE characters SET online = 1 WHERE id=@id LIMIT 1";
                command.Parameters.AddWithValue("@id", character.id);
                command.ExecuteNonQuery();

                SetMoney(player, 0, true);

                if (character.health <= 0)
                {
                    character.health = 5;
                }

                NAPI.Task.Run(() =>
                {
                    player.TriggerEvent("Client:HideCharacterSwitch");
                    player.TriggerEvent("Client:DestroyLoginCamera");
                    player.TriggerEvent("Client:ActivateControls");

                    player.SetOwnSharedData("Player:Needs", character.hunger + "," + character.thirst + "," + character.afk);

                    if (GangController.gangzoneList.Count > 0)
                    {
                        player.TriggerEvent("Client:GetGangzones", NAPI.Util.ToJson(GangController.gangzoneList));
                    }

                    Helper.SetPlayerHealth(player, character.health);

                    Helper.SyncThings(player);

                    player.TriggerEvent("Client:UpdateFriends", character.friends);

                    if (character.tutorialstep < 3)
                    {
                        tempData.itemlist = ItemsController.ConvertPlayerItemsToList(player);
                        if (character.tutorialstep == 1 || character.tutorialstep == 0)
                        {
                            //Legal
                            character.tutorialstep = 1;
                            Helper.SetPlayerPosition(player, new Vector3(-1375.683, -817.50415f, 19.111004 - 1.5));
                            player.Dimension = 1;
                            player.TriggerEvent("Client:ShowBlackFadeIn", "Du bist in Los-Santos angekommen und fährst mit dem Taxi zum Rathaus, um dich um deine neuen Papiere zu kümmern ...", character.tutorialstep);
                            NAPI.Task.Run(() =>
                            {
                                Helper.SetPlayerPosition(player, new Vector3(-554.9115, -186.94072, 38.22092 + 0.1));
                                player.Heading = 31.438608f;
                                player.Rotation = new Vector3(0.0, 0.0, 24.786564);
                                player.TriggerEvent("Client:ShowBlackFadeOut", character.tutorialstep);
                            }, delayTime: 15500);
                        }
                        else
                        {
                            character.tutorialstep = 2;
                            Helper.SetPlayerPosition(player, new Vector3(73.97, -3396.195, 11.157 - 1.5));
                            player.Dimension = 1;
                            player.TriggerEvent("Client:ShowBlackFadeIn", "Als blinder Passagier bist du in Los-Santos angekommen und triffst dich mit deiner Kontaktperson ...", character.tutorialstep);
                            NAPI.Task.Run(() =>
                            {
                                Helper.SetPlayerPosition(player, new Vector3(156.36731, -3308.6594, 6.0219297 + 0.1));
                                player.Heading = 26.385115f;
                                player.Rotation = new Vector3(0.0, 0.0, 24.786564);
                                player.TriggerEvent("Client:ShowBlackFadeOut", character.tutorialstep);
                            }, delayTime: 15500);
                        }
                    }
                    else
                    {
                        player.TriggerEvent("Client:PrepareCharacterLoad");
                        player.TriggerEvent("Client:GetDoors", NAPI.Util.ToJson(DoorsController.doorsList));
                        tempData.itemlist = ItemsController.ConvertPlayerItemsToList(player);
                        if (character.lastpos == "0|0|0|0|0")
                        {
                            player.Dimension = 0;
                            Helper.SetPlayerPosition(player, new Vector3(-540.5015, -205.55278, 3022 + 0.1));
                            player.Heading = -141.01595f;
                            player.Rotation = new Vector3(0.0, 0.0, -141.01595);
                        }
                        else
                        {
                            if (account.prison == 0)
                            {
                                if (character.inhouse != -1)
                                {
                                    player.TriggerEvent("Client:LoadIPL", House.GetInteriorIPL(House.GetHouseById(character.inhouse).interior));
                                    player.SetOwnSharedData("Player:InHouse", character.inhouse);
                                }
                                string[] spawnCharAfterReconnect = new string[5];
                                spawnCharAfterReconnect = character.lastpos.Split("|");
                                player.Dimension = Convert.ToUInt32(spawnCharAfterReconnect[4]);
                                NAPI.Task.Run(() =>
                                {
                                    Helper.SetPlayerPosition(player, new Vector3(float.Parse(spawnCharAfterReconnect[0]), float.Parse(spawnCharAfterReconnect[1]), float.Parse(spawnCharAfterReconnect[2])));
                                }, delayTime: 155);
                                player.Heading = float.Parse(spawnCharAfterReconnect[3]);
                                player.Rotation = new Vector3(0.0, 0.0, float.Parse(spawnCharAfterReconnect[3]));
                            }
                            else
                            {
                                Helper.SetPlayerPosition(player, new Vector3(-1999.6619, 1113.5583, -27.363804));
                                player.Dimension = (uint)(player.Id + 500);
                                player.TriggerEvent("Client:StartPrison", account.prison);
                            }
                        }
                        if (character.arrested > 0)
                        {
                            player.TriggerEvent("Client:SetArrested", true);
                        }
                        if (Helper.adminSettings.voicerp == 1)
                        {
                            Helper.CheckSaltyChat(player);
                            player.TriggerEvent("SaltyChat_InitToTalkClient", player.Id);
                        }
                        ItemsController.UpdateInventory(player);
                        if (account.forumaccount > -1 && (account.forumupdate + 432000) < Helper.UnixTimestamp())
                        {
                            Helper.ForumUpdate(player, "all");
                        }
                        NAPI.Task.Run(() =>
                        {
                            player.SetOwnSharedData("Player:Spawned", true);
                            player.TriggerEvent("Client:Waiting", 0);
                            player.TriggerEvent("Client:PlayerFreeze", false);
                            Helper.OnPlayerPressF5(player, 1);
                            if (character.faction == 2 && character.death == false && FireController.startFire == true)
                            {
                                NAPI.ClientEvent.TriggerClientEvent(player, "Client:BlipFireLocation", FireController.firePosition.X, FireController.firePosition.Y, FireController.firePosition.Z);
                            }
                            if (character.death == true)
                            {
                                player.SetOwnSharedData("Player:Death", true);
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
                            }
                            else if (account.prison > 0)
                            {
                                Helper.SendNotificationWithoutButton(player, $"Du bist noch für {account.prison} Checkpoints im Prison!", "info", "top-left", 3550);
                            }
                            else
                            {
                                DateTime timestamp = DateTime.Today.AddDays(-1);
                                string yesterday = timestamp.ToString("dd-MM-yyyy");
                                string today = DateTime.Today.ToString("dd-MM-yyyy");
                                if (account.login_bonus_before != today)
                                {
                                    if (account.login_bonus > 0)
                                    {
                                        if (account.login_bonus_before == yesterday)
                                        {
                                            account.login_bonus++;
                                            account.login_bonus_before = today;
                                            if (account.login_bonus % 5 == 0)
                                            {
                                                if (Helper.GetRandomPercentage(45))
                                                {
                                                    account.coins += 25;
                                                    Helper.SendNotificationWithoutButton(player, $"Loginbonus Tag {account.login_bonus} - 25 Coins!", "info", "top-left", 6750);
                                                }
                                                else if (Helper.GetRandomPercentage(15))
                                                {
                                                    account.epboost += Helper.UnixTimestamp() + (3 * 3600);
                                                    Helper.SendNotificationWithoutButton(player, $"Loginbonus Tag {account.login_bonus} - +3h Erfahrungspunkte Boost!", "info", "top-left", 6750);
                                                }
                                                else if (Helper.GetRandomPercentage(15))
                                                {
                                                    account.play_points++;
                                                    Helper.SendNotificationWithoutButton(player, $"Loginbonus Tag {account.login_bonus} - +2 Erfahrungspunkte!", "info", "top-left", 6750);
                                                }
                                                else if (Helper.GetRandomPercentage(20))
                                                {
                                                    CharacterController.SetMoney(player, 3750);
                                                    Helper.SendNotificationWithoutButton(player, $"Loginbonus Tag {account.login_bonus} - 3750$!", "info", "top-left", 6750);
                                                }
                                                else
                                                {
                                                    account.coins += 10;
                                                    Helper.SendNotificationWithoutButton(player, $"Loginbonus Tag {account.login_bonus} - 10 Coins!", "info", "top-left", 6750);
                                                }
                                                Account.SaveAccount(player);
                                            }
                                            else
                                            {
                                                Helper.SendNotificationWithoutButton(player, $"Loginbonus Tag {account.login_bonus}, Bonus in {5 - account.login_bonus % 5} Tag/en!", "info", "top-left", 6750);
                                            }
                                        }
                                        else
                                        {
                                            account.login_bonus = 1;
                                            account.login_bonus_before = today;
                                        }
                                    }
                                    else
                                    {
                                        account.login_bonus++;
                                        account.login_bonus_before = today;
                                    }
                                }
                                Helper.SendNotificationWithoutButton(player, "Willkommen zurück " + account.name + "!", "info", "top-left", 1850);
                            }
                        }, delayTime: 3550);
                    }

                }, delayTime: 615);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnSelectCharacter]: " + e.ToString());
            }
        }

        public static void SetCharacterCloths(Player player, JObject obj, string clothes)
        {
            try
            {
                TempData tempData = Helper.GetCharacterTempData(player);
                Character character = Helper.GetCharacterData(player);
                Account account = Helper.GetAccountData(player);

                if (tempData == null || character == null) return;

                if (obj == null)
                {
                    obj = JObject.Parse(character.json);
                }

                string[] clothingArray = new string[8];
                clothingArray = clothes.Split(",");

                if (tempData.adminduty == false)
                {
                    if (clothingArray[2] == "1")
                    {
                        NAPI.Player.SetPlayerClothes(player, 11, (int)obj["clothing"][0], (int)obj["clothingColor"][0]);
                    }
                    else
                    {
                        NAPI.Player.SetPlayerClothes(player, 11, 15, 0);
                    }
                    NAPI.Player.SetPlayerClothes(player, 3, (int)obj["clothing"][1], (int)obj["clothingColor"][1]);
                    if (clothingArray[5] == "1")
                    {
                        NAPI.Player.SetPlayerClothes(player, 4, (int)obj["clothing"][2], (int)obj["clothingColor"][2]);
                    }
                    else
                    {
                        NAPI.Player.SetPlayerClothes(player, 4, 14, 0);
                    }
                    if (clothingArray[4] == "1")
                    {
                        NAPI.Player.SetPlayerClothes(player, 6, (int)obj["clothing"][3], (int)obj["clothingColor"][3]);
                    }
                    else
                    {
                        if (character.gender == 1)
                        {
                            NAPI.Player.SetPlayerClothes(player, 6, 34, 0);
                        }
                        else
                        {
                            NAPI.Player.SetPlayerClothes(player, 6, 35, 0);
                        }
                    }
                    if (clothingArray[3] == "1")
                    {
                        NAPI.Player.SetPlayerClothes(player, 8, (int)obj["clothing"][4], (int)obj["clothingColor"][4]);
                    }
                    else
                    {
                        NAPI.Player.SetPlayerClothes(player, 8, 15, 0);
                    }
                    if (clothingArray[1] == "1")
                    {
                        NAPI.Player.SetPlayerAccessory(player, 1, (int)obj["clothing"][6], (int)obj["clothingColor"][6]);
                    }
                    else
                    {
                        NAPI.Player.ClearPlayerAccessory(player, 1);
                    }

                    if (clothingArray[0] == "1")
                    {
                        NAPI.Player.SetPlayerAccessory(player, 0, (int)obj["clothing"][7], (int)obj["clothingColor"][7]);
                    }
                    else
                    {
                        NAPI.Player.ClearPlayerAccessory(player, 0);
                    }

                    if (clothingArray[7] == "1")
                    {
                        NAPI.Player.SetPlayerClothes(player, 1, (int)obj["clothing"][8], (int)obj["clothingColor"][8]);
                    }
                    else
                    {
                        NAPI.Player.SetPlayerClothes(player, 1, 0, 0);
                    }

                    if (clothingArray[6] == "1")
                    {
                        NAPI.Player.SetPlayerAccessory(player, 2, (int)obj["clothing"][9], (int)obj["clothingColor"][9]);
                        NAPI.Player.SetPlayerAccessory(player, 6, (int)obj["clothing"][10], (int)obj["clothingColor"][10]);
                        NAPI.Player.SetPlayerAccessory(player, 7, (int)obj["clothing"][11], (int)obj["clothingColor"][11]);
                        NAPI.Player.SetPlayerClothes(player, 5, (int)obj["clothing"][5], (int)obj["clothingColor"][5]);
                        NAPI.Player.SetPlayerClothes(player, 7, (int)obj["clothing"][12], (int)obj["clothingColor"][12]);
                    }
                    else
                    {
                        NAPI.Player.ClearPlayerAccessory(player, 2);
                        NAPI.Player.ClearPlayerAccessory(player, 6);
                        NAPI.Player.ClearPlayerAccessory(player, 7);
                        NAPI.Player.SetPlayerClothes(player, 5, 0, 0);
                        NAPI.Player.SetPlayerClothes(player, 7, 0, 0);
                    }

                    if (character.factionduty == true)
                    {
                        NAPI.Player.SetPlayerClothes(player, 5, 0, 0);
                        if (character.faction != 2)
                        {
                            NAPI.Player.SetPlayerClothes(player, 1, 0, 0);
                        }
                    }
                }
                else
                {
                    NAPI.Player.SetPlayerAccessory(player, 2, (int)obj["clothing"][9], (int)obj["clothingColor"][9]);
                    if (clothingArray[0] == "1")
                    {
                            NAPI.Player.SetPlayerAccessory(player, 0, (int)obj["clothing"][7], (int)obj["clothingColor"][7]);
                    }
                    else
                    {
                        NAPI.Player.ClearPlayerAccessory(player, 0);
                    }

                    if (clothingArray[7] == "1")
                    {
                        NAPI.Player.SetPlayerClothes(player, 1, (int)obj["clothing"][8], (int)obj["clothingColor"][8]);
                    }
                    else
                    {
                        NAPI.Player.SetPlayerClothes(player, 1, 0, 0);
                    }

                    NAPI.Player.SetPlayerAccessory(player, 2, (int)obj["clothing"][9], (int)obj["clothingColor"][9]);
                    if (clothingArray[6] == "1")
                    {
                        NAPI.Player.SetPlayerAccessory(player, 6, (int)obj["clothing"][10], (int)obj["clothingColor"][10]);
                        NAPI.Player.SetPlayerAccessory(player, 7, (int)obj["clothing"][11], (int)obj["clothingColor"][11]);
                    }
                    else
                    {
                        NAPI.Player.ClearPlayerAccessory(player, 2);
                        NAPI.Player.ClearPlayerAccessory(player, 6);
                        NAPI.Player.ClearPlayerAccessory(player, 7);
                    }

                    NAPI.Player.SetPlayerClothes(player, 5, 0, 0);
                    NAPI.Player.SetPlayerClothes(player, 7, 0, 0);
                    NAPI.Player.SetPlayerClothes(player, 10, 0, 0);
                    NAPI.Player.SetPlayerClothes(player, 1, 0, 0);
                    //ToDo: Adminkleidung setzen
                    if (character.gender == 1)
                    {
                        NAPI.Player.SetPlayerClothes(player, 8, 15, 0);
                        NAPI.Player.SetPlayerClothes(player, 11, 131, 0);
                        NAPI.Player.SetPlayerClothes(player, 3, 0, 0);
                        NAPI.Player.SetPlayerClothes(player, 4, 4, 0);
                        NAPI.Player.SetPlayerClothes(player, 6, 57, 9);
                    }
                    else
                    {
                        NAPI.Player.SetPlayerClothes(player, 8, 1, 0);
                        NAPI.Player.SetPlayerClothes(player, 11, 129, 0);
                        NAPI.Player.SetPlayerClothes(player, 3, 14, 0);
                        NAPI.Player.SetPlayerClothes(player, 4, 75, 0);
                        NAPI.Player.SetPlayerClothes(player, 6, 103, 0);
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[SetCharacterCloths]: " + e.ToString());
            }
        }

        public static void SetCharacterJson(Player player, JObject obj, string clothing)
        {
            try
            {
                if (obj == null) return;

                Character character = Helper.GetCharacterData(player);

                //Hair
                NAPI.Player.SetPlayerClothes(player, 2, (int)obj["hair"][0], 0);
                NAPI.Player.SetPlayerHairColor(player, (byte)obj["hair"][1], (byte)obj["hair"][2]);
                player.TriggerEvent("hairOverlay::update", player, (int)obj["hair"][0]);

                //Eyecolor
                if (obj["eyeColor"] == null || (byte)obj["eyeColor"] <= 0)
                {
                    obj["eyeColor"] = (byte)1;
                }
                NAPI.Player.SetPlayerEyeColor(player, (byte)obj["eyeColor"]);


                //FaceFeatures
                for (int i = 0; i < 19; i++)
                {
                    NAPI.Player.SetPlayerFaceFeature(player, i, (int)obj["faceFeatures"][i]);
                }

                //Headblend
                HeadBlend heahblend = new HeadBlend();
                heahblend.ShapeFirst = (byte)obj["blendData"][0];
                heahblend.ShapeSecond = (byte)obj["blendData"][1];
                heahblend.ShapeThird = 0;
                heahblend.SkinFirst = (byte)obj["blendData"][2];
                heahblend.SkinSecond = (byte)obj["blendData"][3];
                heahblend.SkinThird = 0;
                heahblend.ShapeMix = (byte)obj["blendData"][4];
                heahblend.SkinMix = (byte)obj["blendData"][5];
                heahblend.ThirdMix = 0;
                NAPI.Player.SetPlayerHeadBlend(player, heahblend);

                //HeadOverlays
                for (int i = 0; i < 12; i++)
                {
                    HeadOverlay headoverlay = new HeadOverlay();
                    int headoverlaycheck = (int)obj["headOverlays"][i];
                    if (headoverlaycheck == -1)
                    {
                        headoverlaycheck = 255;
                    }
                    if (i != 1)
                    {
                        headoverlay.Index = (byte)headoverlaycheck;
                        headoverlay.Opacity = 1.0f;
                        headoverlay.Color = (byte)obj["headOverlaysColors"][i];
                        headoverlay.SecondaryColor = (byte)obj["headOverlaysColors"][i];
                    }
                    else
                    {
                        headoverlaycheck = (int)obj["beard"][0];
                        if (headoverlaycheck == -1)
                        {
                            headoverlaycheck = 255;
                        }
                        headoverlay.Index = (byte)headoverlaycheck;
                        headoverlay.Opacity = 1.0f;
                        headoverlay.Color = (byte)obj["beard"][1];
                        headoverlay.SecondaryColor = (byte)obj["beard"][1];
                    }
                    NAPI.Player.SetPlayerHeadOverlay(player, i, headoverlay);
                }

                //Clothing 
                if (character.factionduty == true & character.faction != 4)
                {
                    obj = JObject.Parse(character.dutyjson);
                }

                CharacterController.SetCharacterCloths(player, obj, clothing);

            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[SetCharacterJson]: " + e.ToString());
            }
        }

        public static void SetMoney(Player player, int money, bool check = false)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                if (character == null) return;
                if (check == false)
                {
                    character.cash += money;
                    player.SetOwnSharedData("Player:Money", character.cash);
                }
                else
                {
                    player.SetOwnSharedData("Player:Money", character.cash);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[SetMoney]: " + e.ToString());
            }
        }

        public static void SaveCharacter(Player player)
        {
            try
            {
                if (General.DatabaseConnectionCheck == true)
                {
                    Character character = Helper.GetCharacterData(player);
                    TempData tempData = Helper.GetCharacterTempData(player);
                    Account account = Helper.GetAccountData(player);

                    if (character == null || tempData == null || account == null || character.name == null) return;

                    PetaPoco.Database db = new PetaPoco.Database(General.Connection);

                    if (tempData.itemlist != null)
                    {
                        foreach (Items iteminlist in tempData.itemlist)
                        {
                            if (iteminlist != null && iteminlist.type == 5)
                            {
                                string[] weaponArray = new string[7];
                                weaponArray = iteminlist.props.Split(",");
                                if (weaponArray.Length > 0 && weaponArray[1] == "1")
                                {
                                    if (!iteminlist.description.ToLower().Contains("schutzweste"))
                                    {
                                        iteminlist.props = $"{NAPI.Player.GetPlayerWeaponAmmo(player, (WeaponHash)WeaponController.GetWeaponHashFromName(iteminlist.description))},1,{weaponArray[2]},{weaponArray[3]},{weaponArray[4]},{weaponArray[5]},{weaponArray[6]}";
                                    }
                                    else
                                    {
                                        int armor = NAPI.Player.GetPlayerArmor(player);
                                        if (armor == 99)
                                        {
                                            armor = 100;
                                        }
                                        iteminlist.props = $"{armor},1,{weaponArray[2]},{weaponArray[3]},{weaponArray[4]},{weaponArray[5]},{weaponArray[6]}";
                                    }
                                    iteminlist.props.Trim();
                                }
                            }
                        }
                    }
                    character.items = NAPI.Util.ToJson(tempData.itemlist);
                    character.health = NAPI.Player.GetPlayerHealth(player);
                    if ((player.Dimension != 125000 && player.GetData<bool>("Player:InShop") == false && account.prison == 0 && player.GetData<bool>("Player:Spectate") == false) || (player.IsInVehicle && !player.Vehicle.GetSharedData<String>("Vehicle:Name").Contains("rcbandito")))
                    {
                        character.lastpos = $"{player.Position.X}|{player.Position.Y}|{player.Position.Z}|{player.Rotation.Z}|{player.Dimension}";
                    }
                    if (character.inhouse != -1 && player.Dimension == 0)
                    {
                        character.inhouse = -1;
                        player.SetOwnSharedData("Player:InHouse", -1);
                    }
                    if (character.json != null && character.json.Length > 5)
                    {
                        db.Save(character);
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[SaveCharacter]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:DeleteCharacter")]
        public void OnDeleteCharacter(Player player, int characterid)
        {
            try
            {
                Account account = Helper.GetAccountData(player);
                bool checkCharacter = false;
                if (account == null || characterid == -1)
                {
                    player.TriggerEvent("Client:CharacterError");
                    return;
                }
                String name = "n/A";
                MySqlCommand command = General.Connection.CreateCommand();
                command.CommandText = "SELECT id,name FROM characters WHERE id=@id LIMIT 1";
                command.Parameters.AddWithValue("@id", characterid);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        name = reader.GetString("name");
                        checkCharacter = true;
                        reader.Close();
                    }
                }
                if (checkCharacter == true)
                {
                    command.CommandText = "UPDATE users SET selectedcharacter = -1,selectedcharacterintern = -1 WHERE id=@userid2";
                    command.Parameters.AddWithValue("@userid2", account.id);
                    command.ExecuteNonQuery();

                    command.CommandText = "UPDATE characters SET closed = 1,ucp_privat = 1 WHERE id=@id2 LIMIT 1";
                    command.Parameters.AddWithValue("@id2", characterid);
                    command.ExecuteNonQuery();

                    Helper.CreateAdminLog($"characterlog", account.name + " hat den Charakter (Name: " + name + ") zur Löschung markiert!");
                    Helper.CreateUserLog(account.id, "Charakter " + name + " gelöscht");
                    account.selectedcharacter = -1;
                    account.selectedcharacterintern = -1;
                    player.TriggerEvent("Client:HideCharacterSwitch");
                    GetAvailableCharacters(player, account.id, true);
                }
                else
                {
                    player.TriggerEvent("Client:CharacterError");
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnDeleteCharacter]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:CheckCharacter")]
        public static bool OnCheckCharacter(Player player, string name)
        {
            try
            {
                if (!Regex.IsMatch(name, "^([A-Z][a-z]+[ ][A-Z][a-z]+)$"))
                {
                    player.TriggerEvent("Client:CheckCharacterNameAnswer", 1);
                    return false;
                }

                MySqlCommand command = General.Connection.CreateCommand();
                command.CommandText = "SELECT id FROM characters WHERE name=@name LIMIT 1";
                command.Parameters.AddWithValue("@name", name);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        player.TriggerEvent("Client:CheckCharacterNameAnswer", 1);
                        reader.Close();
                        return false;
                    }
                }
                player.TriggerEvent("Client:CheckCharacterNameAnswer", 0);
                return true;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnCheckCharacter]: " + e.ToString());
            }
            return false;
        }

        [RemoteEvent("Server:CreateNewCharacterFinish")]
        public static void OnCreateNewCharacterFinish(Player player, string json, string name, int legal)
        {
            try
            {
                if (!OnCheckCharacter(player, name))
                {
                    Helper.SendNotificationWithoutButton(player, "Dieser Charaktername ist bereits belegt oder ungültig!", "error", "center");
                    return;
                }
                if (!Regex.IsMatch(name, "^([A-Z][a-z]+[ ][A-Z][a-z]+)$"))
                {
                    Helper.SendNotificationWithoutButton(player, "Der Charaktername befindet sich nicht im folgenden Format: Vorname Nachname und die jeweils ersten Buchstaben müssen groß sein!", "error", "center");
                    return;
                }
                Account account = Helper.GetAccountData(player);
                if (account == null)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültige Interaktion!", "error", "center");
                    return;
                }
                if (json.ToUpper().Contains("SELECT") || json.ToUpper().Contains("UPDATE") || json.ToUpper().Contains("DELETE") || json.ToUpper().Contains("DROP") || json.ToUpper().Contains("TRUNCATE"))
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Angaben!", "error", "center");
                    return;
                }

                JObject obj = JObject.Parse(json);

                bool gender = (bool)obj["gender"];
                string origin = (string)obj["origin"];
                string birth = (string)obj["birth"];
                int id = 0;

                if (birth.Length < 10 || birth.Length > 10)
                {
                    Helper.SendNotificationWithoutButton(player, "Geburtsdatum bitte korrgieren!", "error", "center");
                    return;
                }

                if (origin.Length < 5 || origin.Length > 30)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Wohnort!", "error", "center");
                    return;
                }

                if (legal < 1 || legal > 2)
                {
                    legal = 1;
                }

                player.TriggerEvent("Client:DeleteCharacterWindow");

                int tutorialstep = legal;

                Character character = new Character();
                character.name = name;
                character.userid = account.id;
                character.origin = origin;
                character.birth = birth.Trim();
                character.json = json;
                character.gender = Convert.ToInt16(gender);
                character.tutorialstep = legal;
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                db.Insert(character);

                id = character.id;

                player.SetData("Player:Screenshot", id);
                player.TriggerEvent("Client:TakePicture", "Char-" + id, 1);

                int rowid = GetRowFromCharacter(player, id);

                MySqlCommand command2 = General.Connection.CreateCommand();
                command2.CommandText = "UPDATE users SET selectedcharacter = @selectedcharacter, selectedcharacterintern = @selectedcharacterintern WHERE id = @userid2";

                command2.Parameters.AddWithValue("@selectedcharacter", rowid);
                command2.Parameters.AddWithValue("@selectedcharacterintern", character.id);
                command2.Parameters.AddWithValue("@userid2", account.id);
                command2.ExecuteNonQuery();

                Helper.CreateAdminLog($"characterlog", account.name + " hat sich einen neuen Charakter (Name: " + name + ") erstellt!");
                Helper.CreateUserLog(account.id, "Charakter " + name + " erstellt");
                Helper.CreateUserTimeline(account.id, id, "Charakter " + name + " erstellt");

                NAPI.Task.Run(() =>
                {
                    OnSelectCharacter(player, id);
                }, delayTime: 600);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[Server:CreateNewCharacterFinish]: " + e.ToString());
            }
        }
    }
}
