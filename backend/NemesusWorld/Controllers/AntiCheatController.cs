using GTANetworkAPI;
using MySql.Data.MySqlClient;
using NemesusWorld.Database;
using NemesusWorld.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NemesusWorld.Controllers
{
    class AntiCheatController : Script
    {
        [RemoteEvent("Server:CallAntiCheat")]
        public static void OnCallAntiCheat(Player player, string cheatname, string props, bool screenshot)
        {
            try
            {
                Account account = Helper.GetAccountData(player);
                TempData tempData = Helper.GetCharacterTempData(player);
                Character character = Helper.GetCharacterData(player);

                if (account == null || tempData == null || character == null) return;

                if (player.HasData("Player:Anticheat")) return;

                string text = "";
                if (props != "n/A")
                {
                    if (cheatname == "Waffen Cheat")
                    {
                        if (props == "966099553") return;
                        text = $"{account.name} benutzt einen {cheatname}!";
                    }
                    else
                    {
                        text = $"{account.name} benutzt einen {cheatname}, weitere Infos: {props}!";
                    }
                }
                else
                {
                    text = $"{account.name} benutzt einen {cheatname}!";
                }

                player.SetData<bool>("Player:Anticheat", true);

                Helper.CreateAdminLog("cheatlog", text);

                if(cheatname == "Speedhack")
                {
                    NAPI.Player.WarpPlayerOutOfVehicle(player);
                }

                if (cheatname == "Schutzwesten Cheat")
                {
                    Helper.SetPlayerArmor(player, 0);
                    NAPI.Player.SetPlayerClothes(player, 9, 0, 0);
                    if (character.items.Length > 5)
                    {
                        foreach (Items item in tempData.itemlist)
                        {
                            if (item != null)
                            {
                                if (item.type == 5)
                                {
                                    if (item.description.ToLower().Contains("schutzweste"))
                                    {
                                        ItemsController.RemoveItem(player, item.itemid);
                                    }
                                }
                            }
                        }
                        ItemsController.UpdateInventory(player);
                    }
                }
                else if (cheatname == "Masskill Cheat")
                {
                    NAPI.Player.RemoveAllPlayerWeapons(player);
                }
                else if (cheatname == "Lebens Cheat")
                {
                    if (NAPI.Player.GetPlayerHealth(player) > 175)
                    {
                        Helper.SetPlayerHealth(player, 175);
                    }
                    else
                    {
                        Helper.SetPlayerHealth(player, 100);
                    }
                }
                else if (cheatname == "Godmode Cheat")
                {
                    Helper.SetPlayerHealth(player, 100);
                }
                else if (cheatname == "Munitions Cheat" || cheatname == "Waffen Cheat")
                {
                    foreach (Items iteminlist in tempData.itemlist.ToList())
                    {
                        if (iteminlist != null && iteminlist.type == 5 && !iteminlist.description.ToLower().Contains("schutzweste"))
                        {
                            NAPI.Player.SetPlayerWeaponAmmo(player, (WeaponHash)WeaponController.GetWeaponHashFromName(iteminlist.description), 0);
                            NAPI.Player.RemovePlayerWeapon(player, (WeaponHash)WeaponController.GetWeaponHashFromName(iteminlist.description));
                            tempData.weaponTints.Remove($"{WeaponController.GetWeaponHash2FromName(iteminlist.description).ToLower()}");
                            player.SetSharedData("Player:WeaponTint", -1);
                            tempData.weaponComponents.Remove($"{WeaponController.GetWeaponHash2FromName(iteminlist.description).ToLower()}");
                            player.SetSharedData("Player:WeaponComponents", "-1");
                            ItemsController.RemoveItem(player, iteminlist.itemid);
                        }
                    }
                    NAPI.Player.RemoveAllPlayerWeapons(player);
                    ItemsController.UpdateInventory(player);
                }
                if (cheatname != "Geld Cheat" && cheatname != "Speedhack" && account.adminlevel <= 0 && player.GetOwnSharedData<bool>("Player:Testmodus") == false)
                {
                    if (screenshot == true)
                    {
                        player.TriggerEvent("Client:TakePicture", cheatname, 0);
                    }
                    AddBan(player, cheatname, "nAnticheat", 1, true);
                }
                else
                {
                    player.ResetData("Player:Anticheat");
                    player.TriggerEvent("Client:BlockPlayer", false);
                    Helper.SendAdminMessage2(text, 1, true);
                    Helper.SendChatMessage(player, $"~r~nAnticheat: {cheatname} entdeckt, cheaten ist sinnlos. Schalte deinen Cheat aus, die Administration wurde benachrichtigt!");
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnCallAntiCheat]: " + e.ToString());
            }
        }

        public static string CheckBan(string name, ulong socialclubid)
        {
            string ret = "n/A";
            try
            {
                MySqlCommand command = General.Connection.CreateCommand();
                command.CommandText = "SELECT ban,bantext,coalesce((SELECT id from bans WHERE banname = @name or identifier = @identifier),0) as banlist FROM users WHERE name = @name LIMIT 1";
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@identifier", socialclubid);

                int ban = 0;
                string bantext = "n/A";
                int banlist = -1;
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        ban = reader.GetInt32("ban");
                        bantext = reader.GetString("bantext");
                        try
                        {
                            banlist = reader.GetInt32("banlist");
                        }
                        catch (Exception)
                        {
                            banlist = -1;
                        }
                        reader.Close();
                    }
                }
                ret = $"{ban},{bantext},{banlist}";
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CheckBan]: " + e.ToString());
            }
            return ret;
        }

        public static void SetKick(Player player, string text, string from, bool log = true, bool announce = true)
        {
            try
            {
                Account account = Helper.GetAccountData(player);
                TempData tempData = Helper.GetCharacterTempData(player);

                if (player.HasData("Player:Kicked")) return;

                String name = "";

                if (account != null)
                {
                    name = account.name;
                }
                else
                {
                    name = player.Name;
                }

                player.SetData<bool>("Player:Kicked", true);

                player.TriggerEvent("Client:BlockPlayer", true);
                if (tempData != null)
                {
                    tempData.freezed = true;
                }

                player.TriggerEvent("SaltyChat_EndTalkClient", player.Id);

                string kickMessage = announce == true ? $"wurde von/m {from} vom Server gekickt" : "wurde vom Server gekickt";
                string message2 = $"~r~Server: {name} {kickMessage}, Grund: {text}!";
                string discordMessage = $"{name} {kickMessage}, Grund: {text}!";

                if (announce == true)
                {
                    foreach (Player p in NAPI.Pools.GetAllPlayers())
                    {
                        if (p != null && p.GetOwnSharedData<bool>("Player:Spawned") == true)
                        {
                            Helper.SendChatMessage(p, message2);
                        }
                    }
                }

                string message = $"~r~Server: Du wurdest von/m {from} vom Server gekickt, Grund: {text}!";
                Helper.SendChatMessage(player, message);

                message2 = message2.Remove(0, 3);

                Helper.SendAdminMessage3($"{name} wurde vom Server gekickt, Grund: {text}!");

                Helper.DiscordWebhook(Helper.AdminNotificationWebHook, discordMessage, "Gameserver");

                if (log == true)
                {
                    if (text.Contains("auf der Whitelist"))
                    {
                        text = "Keine Whitelist";
                    }
                    string message3 = $"{name} wurde von/m {from} vom Server gekickt, Grund: {text}!";
                    Helper.CreateAdminLog("kicklog", message3);
                }

                if (account != null)
                {
                    Helper.CreateUserFile(account.id, from, text, "Kick");
                    Helper.adminSettings.punishments++;
                }

                player.TriggerEvent("Client:PlayerFreeze", true);
                NAPI.Task.Run(() =>
                {
                    player.Kick(text);
                }, delayTime: 4250);

            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[SetKick]: " + e.ToString());
            }
        }

        public static void AddBan(Player player, string text, string from, int time = 1, bool log = true)
        {
            try
            {
                Account account = Helper.GetAccountData(player);
                TempData tempData = Helper.GetCharacterTempData(player);
                if (account == null) return;

                if (player.HasData("Player:Kicked")) return;
                player.SetData<bool>("Player:Kicked", true);

                player.TriggerEvent("Client:BlockPlayer", true);
                tempData.freezed = true;

                player.TriggerEvent("SaltyChat_EndTalkClient", player.Id);

                MySqlCommand command = General.Connection.CreateCommand();
                command = General.Connection.CreateCommand();
                command.CommandText = "INSERT INTO bans (banname, banfrom, text, ip, identifier, timestamp) VALUES (@banname, @banfrom, @text, @ip, @identifier, @timestamp)";
                command.Parameters.AddWithValue("@banname", account.name);
                command.Parameters.AddWithValue("@banfrom", from);
                command.Parameters.AddWithValue("@text", text);
                command.Parameters.AddWithValue("@ip", player.Address);
                command.Parameters.AddWithValue("@identifier", player.SocialClubId);
                command.Parameters.AddWithValue("@timestamp", Helper.UnixTimestamp());
                command.ExecuteNonQuery();

                string banstring = (time == 1) ? "permanent" : $"für {time} Minuten";

                string message2 = $"~r~Server: {account.name} wurde {banstring} von/m {from} vom Server gebannt, Grund: {text}!";
                string discordMessage = $"{account.name} wurde {banstring} von/m {from} vom Server gebannt, Grund: {text}!";

                foreach (Player p in NAPI.Pools.GetAllPlayers())
                {
                    if (p != null && p.GetOwnSharedData<bool>("Player:Spawned") == true)
                    {
                        Helper.SendChatMessage(p, message2);
                    }
                }

                string message = $"~r~Server: Du wurdest {banstring} von/m {from} vom Server gebannt, Grund: {text}!";
                Helper.SendChatMessage(player, message);

                message2 = message2.Remove(0, 3);

                Helper.SendAdminMessage3($"{account.name} wurde vom Server gebannt, Grund: {text}");

                Helper.DiscordWebhook(Helper.AdminNotificationWebHook, discordMessage, "Gameserver");

                if (time > 1)
                {
                    account.ban = Helper.UnixTimestamp() + (time);
                }
                else
                {
                    account.ban = time;
                }
                account.bantext = text;
                command = General.Connection.CreateCommand();
                command.CommandText = "UPDATE users SET remember_token = null WHERE id = @id";
                command.Parameters.AddWithValue("@id", account.id);
                command.ExecuteNonQuery();


                if (log == true)
                {
                    string message3 = $"{account.name} wurde {banstring} von/m {from} vom Server gebannt, Grund: {text}!";
                    Helper.CreateAdminLog("banlog", message3);
                }

                Helper.CreateUserFile(account.id, from, text, (time > 1) ? $"{time} Minuten Timeban" : "Permanenter Ban");
                Helper.adminSettings.punishments++;

                player.TriggerEvent("Client:PlayerFreeze", true);

                NAPI.Task.Run(() =>
                {
                    if (account.forumaccount != -1)
                    {
                        Helper.ForumUpdate(player, "ban", -1, text, time);
                    }
                    player.Kick(text);
                }, delayTime: 4250);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[AddBan]: " + e.ToString());
            }
        }
    }
}
