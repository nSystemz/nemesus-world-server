using GTANetworkAPI;
using MySql.Data.MySqlClient;
using NemesusWorld.Controllers;
using NemesusWorld.Database;
using NemesusWorld.Models;
using NemesusWorld.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NemesusWorld
{
    class Commands : Script
    {
        public static List<Vehicle> adminVehicles = new List<Vehicle>();

        //Admin commands
        [Command("giveitem", "Befehl: /giveitem [Spieler/ID] [Itemname] [Menge] [Props*]")]
        public void CMD_giveitem(Player player, string target, string itemname, int menge, string props = "n/A")
        {
            NAPI.Task.Run(() =>
            {
                try
                {
                    if (!Account.IsPlayerLoggedIn(player)) return;
                    Account account = Helper.GetAccountData(player);
                    if (player.GetOwnSharedData<bool>("Player:Testmodus") == false && !Account.IsAdminOnDuty(player, (int)Account.AdminRanks.HighAdministrator))
                    {
                        Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                        return;
                    }
                    Player ntarget = Helper.GetPlayerByNameOrID(target);
                    if (ntarget == null || !Account.IsPlayerLoggedIn(ntarget))
                    {
                        Helper.SendNotificationWithoutButton(player, "Ungültiger Spieler!", "error", "top-end");
                        return;
                    }
                    Account accounttarget = Helper.GetAccountData(ntarget);
                    Character charactertarget = Helper.GetCharacterData(ntarget);
                    if (accounttarget == null || charactertarget == null)
                    {
                        Helper.SendNotificationWithoutButton(player, "Ungültiger Spieler!", "error", "top-end");
                        return;
                    }
                    if (account.adminlevel < accounttarget.adminlevel)
                    {
                        Helper.SendNotificationWithoutButton(player, "Der angegebene Spieler hat ein höheren Adminlevel als du!", "error", "top-end");
                        return;
                    }
                    if (!ItemsController.AvailableItems(itemname) || itemname == "Smartphone")
                    {
                        Helper.SendNotificationWithoutButton(player, "Ungültiges Item!", "error", "top-end");
                        return;
                    }
                    if (menge <= 0 || menge > 250)
                    {
                        Helper.SendNotificationWithoutButton(player, "Ungültige Menge!", "error", "top-end");
                        return;
                    }
                    itemname = ItemsController.GetItemNameByItemName(itemname);
                    if (ItemsController.IsItemAWeapon(itemname.ToLower()) && itemname.ToLower() != "granate" && itemname.ToLower() != "bzgas" && itemname.ToLower() != "rauchgranate" && itemname.ToLower() != "molotowcocktail" && itemname.ToLower() != "snowball")
                    {
                        menge = 1;
                    }
                    if (props.Length > 3 && props != "n/A" && ItemsController.IsItemAWeapon(itemname.ToLower()))
                    {
                        Helper.SendNotificationWithoutButton(player, "Ungültige Props Länge!", "error", "top-end");
                        return;
                    }
                    if (props.Length > 3 && props != "n/A" && !ItemsController.IsItemAWeapon(itemname.ToLower()))
                    {
                        if (props.Length <= 3 || props.Length > 50)
                        {
                            Helper.SendNotificationWithoutButton(player, "Ungültige Props Länge!", "error", "top-end");
                            return;
                        }
                        props = props.Trim();
                    }
                    TempData tempData = Helper.GetCharacterTempData(ntarget);
                    if (tempData == null)
                    {
                        Helper.SendNotificationWithoutButton(player, "Ungültige Interaktion!", "error", "top-end");
                        return;
                    }
                    if (!ItemsController.CanPlayerHoldItem(player, (ItemsController.GetItemWeight(itemname) * menge)))
                    {
                        Helper.SendNotificationWithoutButton(player, "Der Spieler kann das Item nichtmehr tragen!", "error", "top-end");
                        return;
                    }
                    Items snowBallItem = ItemsController.GetItemByItemName(player, "Snowball");
                    if(itemname.ToLower() == "snowball" && snowBallItem != null && snowBallItem.amount + menge > 10)
                    {
                        Helper.SendNotificationWithoutButton(player, "Der Spieler kann nur max. 10 Snowballs tragen!", "error", "top-end");
                        return;
                    }
                    if (itemname.ToLower() == "snowball" && snowBallItem == null && menge > 10)
                    {
                        Helper.SendNotificationWithoutButton(player, "Der Spieler kann nur max. 10 Snowballs tragen!", "error", "top-end");
                        return;
                    }
                    Items newitem = ItemsController.CreateNewItem(ntarget, charactertarget.id, itemname, "Player", menge, ItemsController.GetFreeItemID(ntarget), "n/A", "Administrativ", charactertarget.name);
                    if (newitem != null)
                    {
                        if (props.Length > 0 && !ItemsController.IsItemAWeapon(itemname.ToLower()))
                        {
                            newitem.props = props;
                        }
                        tempData.itemlist.Add(newitem);
                    }
                    Helper.SendNotificationWithoutButton(player, "Du hast " + accounttarget.name + " " + menge + "x, das Item " + itemname + " gegeben!", "success", "top-end", 5500);
                    Helper.SendNotificationWithoutButton(ntarget, "Du hast von " + account.name + " das Item " + itemname + ", " + menge + "x bekommen!", "success", "top-end", 5500);
                    Helper.CreateAdminLog($"adminlog", account.name + " hat " + accounttarget.name + "" + menge + "x das Item " + itemname + " gegeben!");
                    ItemsController.UpdateInventory(ntarget);
                }
                catch (Exception)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültige Operation!", "error", "top-end");
                }
                return;
            });
        }

        [Command("testcloth", "Befehl: /testcloth [Component-ID] [Drawable] [Color*]")]
        public void CMD_testcloth(Player player, int componentid, int drawable, int color = 0)
        {
            NAPI.Task.Run(() =>
            {
                try
                {
                    if (!Account.IsPlayerLoggedIn(player)) return;
                    Account account = Helper.GetAccountData(player);
                    if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Supporter))
                    {
                        Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                        return;
                    }
                    if (componentid == 0)
                    {
                        NAPI.Player.SetPlayerAccessory(player, 0, drawable, color);
                    }
                    else
                    {
                        NAPI.Player.SetPlayerClothes(player, componentid, drawable, color);
                    }
                    Helper.SendNotificationWithoutButton(player, "Testkleidung gesetzt!", "success", "top-end", 5500);
                }
                catch (Exception e)
                {
                    Helper.ConsoleLog("error", $"[CMD_testcloths]: " + e.ToString());
                }
                return;
            });
        }

        [Command("testeupoutfit", "Befehl: /testeupoutfit [Outfit-Name]", GreedyArg = true)]
        public void CMD_testeupoutfit(Player player, String outfitname)
        {
            NAPI.Task.Run(() =>
            {
                try
                {
                    if (!Account.IsPlayerLoggedIn(player)) return;
                    Account account = Helper.GetAccountData(player);
                    if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Supporter))
                    {
                        Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                        return;
                    }
                    if(outfitname.Length < 5 || outfitname.Length > 35)
                    {
                        Helper.SendNotificationWithoutButton(player, "Ungültiger Outfitname!", "error", "top-end");
                        return;
                    }
                    PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                    Outfits outfit = null;
                    outfit = db.Single<Outfits>("WHERE name = @0", outfitname);
                    if(outfit == null)
                    {
                        Helper.SendNotificationWithoutButton(player, "Ungültiges Outfit!", "error", "top-end");
                        return;
                    }
                    if(outfit.owner != "EUP")
                    {
                        Helper.SendNotificationWithoutButton(player, "Du kannst nur EUP Outfits testen!", "error", "top-end");
                        return;
                    }

                    String json1 = outfit.json1;
                    String json2 = outfit.json2;

                    string[] json1Array = new string[14];
                    string[] json2Array = new string[14];

                    json1 = json1.Substring(1, json1.Length - 2);
                    json2 = json2.Substring(1, json2.Length - 2);

                    json1Array = json1.Split(",");
                    json2Array = json2.Split(",");

                    NAPI.Player.ClearPlayerAccessory(player, 0);
                    NAPI.Player.ClearPlayerAccessory(player, 1);
                    NAPI.Player.ClearPlayerAccessory(player, 2);
                    NAPI.Player.ClearPlayerAccessory(player, 6);
                    NAPI.Player.ClearPlayerAccessory(player, 7);
                    NAPI.Player.SetPlayerClothes(player, 10, 0, 0);
                    NAPI.Player.SetPlayerClothes(player, 5, 0, 0);
                    NAPI.Player.SetPlayerClothes(player, 7, 0, 0);
                    NAPI.Player.SetPlayerClothes(player, 1, 0, 0);
                    NAPI.Player.SetPlayerClothes(player, 9, 0, 0);

                    //Top
                    NAPI.Player.SetPlayerClothes(player, 11, Convert.ToInt32(json1Array[5]) - 1, Convert.ToInt32(json2Array[5]) - 1);
                    //Torso
                    NAPI.Player.SetPlayerClothes(player, 3, Convert.ToInt32(json1Array[6]) - 1, Convert.ToInt32(json2Array[6]) - 1);
                    //Legs
                    NAPI.Player.SetPlayerClothes(player, 4, Convert.ToInt32(json1Array[9]) - 1, Convert.ToInt32(json2Array[9]) - 1);
                    //Shoes
                    NAPI.Player.SetPlayerClothes(player, 6, Convert.ToInt32(json1Array[10]) - 1, Convert.ToInt32(json2Array[10]) - 1);
                    //Undershirt
                    NAPI.Player.SetPlayerClothes(player, 8, Convert.ToInt32(json1Array[8]) - 1, Convert.ToInt32(json2Array[8]) - 1);
                    //Bag
                    NAPI.Player.SetPlayerClothes(player, 5, Convert.ToInt32(json1Array[13]) - 1, Convert.ToInt32(json2Array[13]) - 1);
                    //Glasses
                    NAPI.Player.SetPlayerAccessory(player, 1, Convert.ToInt32(json1Array[1]) - 1, Convert.ToInt32(json2Array[1]) - 1);
                    //Hat
                    NAPI.Player.SetPlayerAccessory(player, 0, Convert.ToInt32(json1Array[0]) - 1, Convert.ToInt32(json2Array[0]) - 1);
                    //Mask
                    NAPI.Player.SetPlayerClothes(player, 1, Convert.ToInt32(json1Array[4]) - 1, Convert.ToInt32(json2Array[4]) - 1);
                    //Ears
                    NAPI.Player.SetPlayerAccessory(player, 2, Convert.ToInt32(json1Array[2]) - 1, Convert.ToInt32(json2Array[2]) - 1);
                    //Watches
                    NAPI.Player.SetPlayerClothes(player, 6, Convert.ToInt32(json1Array[3]) - 1, Convert.ToInt32(json2Array[3]) - 1);
                    //Bracelets
                    NAPI.Player.SetPlayerAccessory(player, 7, Convert.ToInt32(json1Array[7]) - 1, Convert.ToInt32(json2Array[7]) - 1);
                    //Accessories
                    NAPI.Player.SetPlayerClothes(player, 7, Convert.ToInt32(json1Array[11]) - 1, Convert.ToInt32(json2Array[11]) - 1);
                    //Armor
                    NAPI.Player.SetPlayerClothes(player, 9, Convert.ToInt32(json1Array[12]) - 1, Convert.ToInt32(json2Array[12]) - 1);
                    Helper.SendNotificationWithoutButton(player, "Testoutfit (EUP) gesetzt!", "success", "top-end", 5500);
                }
                catch (Exception e)
                {
                    Helper.ConsoleLog("error", $"[CMD_testeupoutfit]: " + e.ToString());
                }
                return;
            });
        }

        [Command("testacc", "Befehl: /testacc [Component-ID] [Drawable] [Color*]")]
        public void CMD_testacc(Player player, int componentid, int drawable, int color = 0)
        {
            NAPI.Task.Run(() =>
            {
                try
                {
                    if (!Account.IsPlayerLoggedIn(player)) return;
                    Account account = Helper.GetAccountData(player);
                    if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Supporter))
                    {
                        Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                        return;
                    }
                    NAPI.Player.SetPlayerAccessory(player, componentid, drawable, color);
                    Helper.SendNotificationWithoutButton(player, "Testaccessoire gesetzt!", "success", "top-end", 5500);
                }
                catch (Exception e)
                {
                    Helper.ConsoleLog("error", $"[CMD_testass]: " + e.ToString());
                }
                return;
            });
        }

        [Command("itemcheck", "Befehl: /itemcheck")]
        public void CMD_itemcheck(Player player)
        {
            if (!Account.IsPlayerLoggedIn(player)) return;
            if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Administrator))
            {
                Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                return;
            }
            foreach (ItemsGlobal globalitem in ItemsController.itemListGlobal)
            {
                if (globalitem != null && globalitem.owneridentifier == "Ground")
                {
                    if (Helper.IsInRangeOfPoint(player.Position, new Vector3(globalitem.posx, globalitem.posy, globalitem.posz), 2.0f) && player.Dimension == globalitem.dimension)
                    {
                        Helper.SendChatMessage(player, $"~b~[Item-Info]: Name/ID: {globalitem.description}[{globalitem.itemid}], Menge: {globalitem.amount}, Props: {globalitem.props}");
                        return;
                    }
                }
            }
            Helper.SendNotificationWithoutButton(player, "Hier liegt kein Item!", "error", "top-end");
            return;
        }

        [Command("deleteitem", "Befehl: /deleteitem [Item-ID] [Spieler/ID*]", GreedyArg = true)]
        public void CMD_deleteitem(Player player, int itemid, string target = "null")
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                bool found = false;
                Account account = Helper.GetAccountData(player);
                MySqlCommand command = General.Connection.CreateCommand();
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Administrator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                if (target == "null")
                {
                    ItemsGlobal itemsGlobal = null;
                    foreach (ItemsGlobal globalitem in ItemsController.itemListGlobal)
                    {
                        if (globalitem != null && globalitem.itemid == itemid)
                        {
                            itemsGlobal = globalitem;
                            ItemsController.itemListGlobal.Remove(globalitem);
                            found = true;
                            break;
                        }
                    }
                    if (found == true)
                    {
                        if (itemsGlobal.objectHandle != null)
                        {
                            if (itemsGlobal.description == "Smartphone")
                            {
                                command.CommandText = "DELETE FROM smartphones WHERE phonenumber=@phonenumber LIMIT 1";
                                command.Parameters.AddWithValue("@phonenumber", itemsGlobal.props.Remove(0, 4));
                                command.ExecuteNonQuery();
                            }
                            if (itemsGlobal.type == 5 && itemsGlobal.props.Length > 0)
                            {
                                string[] weaponArray = new string[7];
                                weaponArray = itemsGlobal.props.Split(",");
                                command.CommandText = "DELETE FROM weapons WHERE ident=@ident LIMIT 1";
                                command.Parameters.AddWithValue("@ident", weaponArray[3]);
                                command.ExecuteNonQuery();
                            }
                            itemsGlobal.objectHandle.Delete();
                            itemsGlobal.objectHandle = null;
                            itemsGlobal.textHandle.Delete();
                            itemsGlobal.textHandle = null;
                        }
                        itemsGlobal = null;
                        Helper.SendNotificationWithoutButton(player, "Das Item wurde gelöscht!", "success", "top-end");
                    }
                    else
                    {
                        Helper.SendNotificationWithoutButton(player, "Kein Item gefunden!", "error", "top-end");
                        return;
                    }
                    Helper.CreateAdminLog($"adminlog", account.name + " hat das Item " + itemsGlobal.description + " gelöscht!");
                    return;
                }
                else
                {
                    Items itemDelete = null;
                    Player ntarget = Helper.GetPlayerByNameOrID(target);
                    if (ntarget == null || !Account.IsPlayerLoggedIn(ntarget))
                    {
                        Helper.SendNotificationWithoutButton(player, "Ungültiger Spieler!", "error", "top-end");
                        return;
                    }
                    Account accounttarget = Helper.GetAccountData(ntarget);
                    if (accounttarget == null)
                    {
                        Helper.SendNotificationWithoutButton(player, "Ungültiger Spieler!", "error", "top-end");
                        return;
                    }
                    Character charactertarget = Helper.GetCharacterData(ntarget);
                    if (account.adminlevel < accounttarget.adminlevel)
                    {
                        Helper.SendNotificationWithoutButton(player, "Der angegebene Spieler hat ein höheren Adminlevel als du!", "error", "top-end");
                        return;
                    }
                    TempData tempData = Helper.GetCharacterTempData(ntarget);
                    foreach (Items item in tempData.itemlist)
                    {
                        if (item != null && item.itemid == itemid)
                        {
                            itemDelete = item;
                            tempData.itemlist.Remove(item);
                            found = true;
                            break;
                        }
                    }
                    if (found == true)
                    {
                        if (itemDelete.type == 5)
                        {
                            string[] weaponArray = new string[7];
                            weaponArray = itemDelete.props.Split(",");
                            if (weaponArray[1] == "1")
                            {
                                if (!itemDelete.description.ToLower().Contains("schutzweste"))
                                {
                                    NAPI.Player.SetPlayerWeaponAmmo(ntarget, (WeaponHash)WeaponController.GetWeaponHashFromName(itemDelete.description), 0);
                                    NAPI.Player.RemovePlayerWeapon(ntarget, (WeaponHash)WeaponController.GetWeaponHashFromName(itemDelete.description));
                                    tempData.weaponTints.Remove($"{WeaponController.GetWeaponHash2FromName(itemDelete.description).ToLower()}");
                                    tempData.weaponComponents.Remove($"{WeaponController.GetWeaponHash2FromName(itemDelete.description).ToLower()}");
                                    if (NAPI.Player.GetPlayerCurrentWeapon(player) == (WeaponHash)WeaponController.GetWeaponHashFromName(itemDelete.description))
                                    {
                                        player.SetSharedData("Player:WeaponTint", -1);
                                        player.SetSharedData("Player:WeaponComponents", "-1");
                                    }
                                }
                                else
                                {
                                    Helper.SendNotificationWithoutButton(player, "Die Schutzweste kann nicht gelöscht werden!", "error", "top-end");
                                }
                            }
                        }
                        if (itemDelete.description == "Smartphone")
                        {
                            command.CommandText = "DELETE FROM smartphones WHERE phonenumber=@phonenumber LIMIT 1";
                            command.Parameters.AddWithValue("@phonenumber", itemDelete.props.Remove(0, 4));
                            command.ExecuteNonQuery();
                        }
                        if (itemDelete.type == 5 && itemDelete.props.Length > 0)
                        {
                            string[] weaponArray = new string[7];
                            weaponArray = itemDelete.props.Split(",");
                            command.CommandText = "DELETE FROM weapons WHERE ident=@ident LIMIT 1";
                            command.Parameters.AddWithValue("@ident", weaponArray[3]);
                            command.ExecuteNonQuery();
                        }
                        Helper.CreateAdminLog($"adminlog", account.name + " hat das Item " + itemDelete.description + " von " + charactertarget.name + " gelöscht!");
                        Helper.SendNotificationWithoutButton(player, "Das Item vom Spieler wurde gelöscht!", "success", "top-end", 3500);
                        Helper.SendNotificationWithoutButton(ntarget, account.name + " hat das Item " + itemDelete.description + " von dir gelöscht!", "success", "top-end", 3500);
                        ItemsController.UpdateInventory(ntarget);
                        itemDelete = null;
                    }
                    else
                    {
                        Helper.SendNotificationWithoutButton(player, "Kein Item beim Spieler gefunden!", "error", "top-end");
                    }
                    return;
                }
            }
            catch
            {
                player.SendChatMessage("~w~Befehl: /deleteitem [Item-ID] [Spieler/ID*]");
            }
        }

        [Command("saveall", "Befehl: /saveall")]
        public void CMD_saveall(Player player)
        {
            try
            {
                Account account = Helper.GetAccountData(player);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.HighAdministrator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                Events.SaveAll();
                Helper.SendNotificationWithoutButton(player, "Du hast den Server gespeichert", "success", "top-end");
                Helper.SendAdminMessage3($"{account.name} hat den kompletten Server gespeichert!");
                Helper.CreateAdminLog($"adminlog", account.name + " hat den kompletten Server gespeichert!");
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CMD_saveall]: " + e.ToString());
            }
        }


        [Command("makeadmin", "Befehl: /makeadmin [Spieler/ID] [Adminlevel]")]
        public void CMD_makeadmin(Player player, string target, int adminlevel)
        {
            if (!Account.IsPlayerLoggedIn(player)) return;
            Player ntarget = Helper.GetPlayerByNameOrID(target);
            if (ntarget == null || !Account.IsPlayerLoggedIn(ntarget) || ntarget == player)
            {
                Helper.SendNotificationWithoutButton(player, "Ungültiger Spieler!", "error", "top-end");
                return;
            }
            Account accounttarget = Helper.GetAccountData(ntarget);
            Account account = Helper.GetAccountData(player);
            if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.HighAdministrator))
            {
                Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                return;
            }
            if (account.adminlevel < accounttarget.adminlevel)
            {
                Helper.SendNotificationWithoutButton(player, "Der angegebene Spieler hat ein höheres Adminlevel als du!", "error", "top-end");
                return;
            }
            if (accounttarget.adminlevel == adminlevel)
            {
                Helper.SendNotificationWithoutButton(player, "Der angegebene Spieler hat dieses Adminlevel bereits!", "error", "top-end");
                return;
            }
            if (account.adminlevel < adminlevel || adminlevel > 8 || adminlevel < 0 || accounttarget.adminlevel == adminlevel)
            {
                Helper.SendNotificationWithoutButton(player, "Ungültiges Adminlevel!", "error", "top-end");
                return;
            }
            if (accounttarget.adminlevel == 0 && adminlevel > 0)
            {
                accounttarget.admin_since = Helper.UnixTimestamp();
            }
            accounttarget.adminlevel = adminlevel;
            Helper.SendNotificationWithoutButton(player, "Du hast " + accounttarget.name + " zum " + Account.AdminNames[adminlevel] + ", Stufe " + adminlevel + " gemacht!", "success", "top-end", 5500);
            Helper.SendNotificationWithoutButton(ntarget, "Du wurdest von " + account.name + " zum " + Account.AdminNames[adminlevel] + ", Stufe " + adminlevel + " gemacht!", "success", "top-end", 5500);
            Helper.SendAdminMessage2($"{accounttarget.name} wurde von {account.name} zum {Account.AdminNames[adminlevel]}, Stufe {adminlevel} gemacht!", 1, true);
            Helper.CreateAdminLog($"adminlog", account.name + " hat " + accounttarget.name + " zum " + Account.AdminNames[adminlevel] + " gemacht!");
            if (adminlevel <= 0)
            {
                TempData tempData = Helper.GetCharacterTempData(ntarget);
                tempData.adminduty = false;
                NAPI.Data.SetEntitySharedData(player, "Player:AdminLogin", 0);
                accounttarget.admin_since = 1609462861;
                if (tempData.adminvehicle != null)
                {
                    tempData.adminvehicle.Delete();
                    tempData.adminvehicle = null;
                }
                account.admin_rang = "n/A";
                ntarget.ResetSharedData("Player:AdminRang");
            }
            NAPI.Data.SetEntitySharedData(ntarget, "Player:Adminlevel", adminlevel);
            return;
        }

        [Command("groupcheck", "Befehl: /groupcheck [Gruppierungsname]", GreedyArg = true)]
        public void cmd_groupcheck(Player player, string groupname)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Administrator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                Groups group = GroupsController.GetGroupByName(groupname);
                if (group == null)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültige Gruppierung!", "error", "center");
                    return;
                }
                string leadername = Helper.GetCharacterName(group.leader);
                Helper.SendChatMessage(player, $"~b~[Gruppierungscheck]: ID: {group.id}, Name: {group.name}, Leader: {leadername}");
                return;
            }
            catch (Exception)
            {
                player.SendChatMessage("~w~Befehl: /editgroup [Gruppierungs-ID] [Option] [Menge]");
                player.SendChatMessage("~y~Verfügbare Aktionen: Mitglieder, Kontoentfernen, Leader, Status, Provision");
            }
        }

        [Command("editgroup", "Befehl: /editgroup [Gruppierungs-ID] [Option] [Menge]")]
        public void CMD_editgroup(Player player, int groupid = -1, string option = "n/A", int menge = 1)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Administrator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                Account account = Helper.GetAccountData(player);
                if (account == null)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültige Interaktion!", "error", "top-end");
                    return;
                }
                if (groupid == -1 && option == "n/A")
                {
                    player.SendChatMessage("~w~Befehl: /editgroup [Gruppierungs-ID] [Option] [Menge]");
                    player.SendChatMessage("~y~Verfügbare Aktionen: Mitglieder, Kontoentfernen, Leader, Status, Provision, Fahrzeuge");
                    return;
                }
                Groups group = GroupsController.GetGroupById(groupid);
                if (group == null)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültige Gruppierung!", "error", "top-end");
                    return;
                }
                switch (option.ToLower())
                {
                    case "mitglieder":
                        {
                            if (menge < 0)
                            {
                                Helper.SendNotificationWithoutButton(player, "Ungültige Menge!", "error", "top-end");
                                return;
                            }
                            group.members = menge;
                            break;
                        }
                    case "kontoentfernen":
                        {
                            if (group.banknumber == "n/A")
                            {
                                Helper.SendNotificationWithoutButton(player, "Die Gruppierung hat kein Firmenkonto!", "error", "top-end");
                                return;
                            }
                            menge = -1;
                            group.banknumber = "n/A";
                            break;
                        }
                    case "leader":
                        {
                            if (menge < 0)
                            {
                                Helper.SendNotificationWithoutButton(player, "Ungültige Menge!", "error", "top-end");
                                return;
                            }
                            group.leader = menge;
                            break;
                        }
                    case "status":
                        {
                            if (menge < 0 || menge > 2)
                            {
                                Helper.SendNotificationWithoutButton(player, "Ungültige Menge!", "error", "top-end");
                                return;
                            }
                            group.status = menge;
                            break;
                        }
                    case "fahrzeuge":
                        {
                            if (menge < 0 || menge > 100)
                            {
                                Helper.SendNotificationWithoutButton(player, "Ungültige Menge!", "error", "top-end");
                                return;
                            }
                            group.maxplusvehicles = menge;
                            break;
                        }
                    case "provision":
                        {
                            if (menge < 0 || menge > 100)
                            {
                                Helper.SendNotificationWithoutButton(player, "Ungültige Menge!", "error", "top-end");
                                return;
                            }
                            group.provision = menge;
                            break;
                        }
                    default:
                        {
                            player.SendChatMessage("~w~Befehl: /editgroup [Gruppierungs-ID] [Option] [Menge]");
                            player.SendChatMessage("~y~Verfügbare Aktionen: Mitglieder, Kontoentfernen, Leader, Status, Provision, Fahrzeuge");
                            break;
                        }
                }
                option = option[0].ToString().ToUpper() + option.Substring(1);
                Helper.SendNotificationWithoutButton(player, $"Du hast den {option} Wert der Gruppierung {group.name}[{group.id}] auf {menge} gesetzt!", "success", "top-end", 5500);
                Helper.CreateAdminLog("adminlog", $"{account.name} hat den {option} Wert der Gruppierung {group.name}[{group.id}] auf {menge} gesetzt!");
                GroupsController.SaveGroup(group);
            }
            catch (Exception)
            {
                player.SendChatMessage("~w~Befehl: /editgroup [Gruppierungs-ID] [Option] [Menge]");
                player.SendChatMessage("~y~Verfügbare Aktionen: Mitglieder, Kontoentfernen, Leader, Status, Provision");
            }
            return;
        }

        [Command("aggl", "Befehl: /aggl [Gruppierungs-ID] [Lizenz]")]
        public void cmd_aggl(Player player, int groupid = -1, string lizenz = "n/A")
        {
            try
            {
                bool success = false;
                int value = 0;
                Account account = Helper.GetAccountData(player);
                if (!Account.IsPlayerLoggedIn(player)) return;
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Administrator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                if (groupid == -1 && lizenz == "n/A")
                {
                    player.SendChatMessage("~w~Befehl: /aggl [Gruppierungs-ID] [Lizenz]");
                    player.SendChatMessage("~y~Verfügbare Aktionen: Speditionslizenz, Tuninglizenz, Mechatronikerlizenz, Personenbeförderungslizenz, Sicherheitslizenz");
                    return;
                }
                Groups group = GroupsController.GetGroupById(groupid);
                if (group == null)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültige Gruppierung!", "error", "top-end");
                    return;
                }
                string[] licArray = new string[9];
                licArray = group.licenses.Split("|");
                switch (lizenz.ToLower())
                {
                    case "speditionslizenz":
                        {
                            if (Convert.ToInt32(licArray[0]) == 1)
                            {
                                success = true;
                                value = 0;
                                licArray[0] = "" + 0;
                            }
                            else
                            {
                                value = 1;
                                success = true;
                                licArray[0] = "" + 1;
                            }
                            break;
                        }
                    case "tuninglizenz":
                        {
                            if (Convert.ToInt32(licArray[1]) == 1)
                            {
                                success = true;
                                value = 0;
                                licArray[1] = "" + 0;
                            }
                            else
                            {
                                value = 1;
                                success = true;
                                licArray[1] = "" + 1;
                            }
                            break;
                        }
                    case "mechatronikerlizenz":
                        {
                            if (Convert.ToInt32(licArray[2]) == 1)
                            {
                                success = true;
                                value = 0;
                                licArray[2] = "" + 0;
                            }
                            else
                            {
                                value = 1;
                                success = true;
                                licArray[2] = "" + 1;
                            }
                            break;
                        }
                    case "personenbeförderungslizenz":
                        {
                            if (Convert.ToInt32(licArray[3]) == 1)
                            {
                                success = true;
                                value = 0;
                                licArray[3] = "" + 0;
                            }
                            else
                            {
                                value = 1;
                                success = true;
                                licArray[3] = "" + 1;
                            }
                            break;
                        }
                    case "sicherheitslizenz":
                        {
                            if (Convert.ToInt32(licArray[4]) == 1)
                            {
                                success = true;
                                value = 0;
                                licArray[4] = "" + 0;
                            }
                            else
                            {
                                value = 1;
                                success = true;
                                licArray[4] = "" + 1;
                            }
                            break;
                        }
                    default:
                        {
                            player.SendChatMessage("~w~Befehl: /aggl [Gruppierungs-ID] [Lizenz]");
                            player.SendChatMessage("~y~Verfügbare Aktionen: Speditionslizenz, Tuninglizenz, Mechatronikerlizenz, Personenbeförderungslizenz, Sicherheitslizenz");
                            break;
                        }
                }
                if (!success)
                {
                    player.SendChatMessage("~w~Befehl: /aggl [Gruppierungs-ID] [Lizenz]");
                    player.SendChatMessage("~y~Verfügbare Aktionen: Speditionslizenz, Tuninglizenz, Mechatronikerlizenz, Personenbeförderungslizenz, Sicherheitslizenz");
                    return;
                }
                lizenz = lizenz[0].ToString().ToUpper() + lizenz.Substring(1);
                Helper.SendNotificationWithoutButton(player, $"Du hast den Wert der Lizenz {lizenz} der Gruppierung {group.name}[{group.id}] auf {value} gesetzt!", "success", "top-end", 5500);
                Helper.CreateAdminLog("adminlog", $"{account.name} hat den Wert der Lizenz {lizenz} der Gruppierung {group.name}[{group.id}] auf {value} gesetzt!");
                group.licenses = String.Join("|", licArray);
                GroupsController.SaveGroup(group);
            }
            catch (Exception)
            {
                player.SendChatMessage("~w~Befehl: /aggl [Gruppierungs-ID] [Lizenz]");
                player.SendChatMessage("~y~Verfügbare Aktionen: Speditionslizenz, Tuninglizenz, Mechatronikerlizenz, Personenbeförderungslizenz, Sicherheitslizenz");
            }
        }

        [Command("setskin", "/setskin [Spieler/ID] [Model]")]
        public void CMD_setskin(Player player, string target = "n/A", string model = "Standard")
        {
            NAPI.Task.Run(() =>
            {
                try
                {
                    Account account = Helper.GetAccountData(player);
                    if (!Account.IsPlayerLoggedIn(player)) return;
                    if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Supporter))
                    {
                        Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                        return;
                    }
                    if (target == "n/A")
                    {
                        player.SendChatMessage("~w~Befehl: /setskin [Spieler/ID] [Model]");
                        player.SendChatMessage("~y~Model = Standard für den normalen Charakter Skin");
                        return;
                    }
                    Player ntarget = Helper.GetPlayerByNameOrID(target);
                    if (ntarget == null || !ntarget.Exists || !Account.IsPlayerLoggedIn(ntarget))
                    {
                        Helper.SendNotificationWithoutButton(player, "Ungültiger Spieler!", "error", "top-end");
                        return;
                    }
                    Account accounttarget = Helper.GetAccountData(ntarget);
                    Character character = Helper.GetCharacterData(ntarget);
                    TempData tempData = Helper.GetCharacterTempData(ntarget);
                    if (accounttarget == null || character == null || tempData == null)
                    {
                        Helper.SendNotificationWithoutButton(player, "Ungültiger Spieler!", "error", "top-end");
                        return;
                    }
                    foreach (Items iteminlist in tempData.itemlist)
                    {
                        if (iteminlist != null && iteminlist.type == 5 && !iteminlist.description.ToLower().Contains("schutzweste"))
                        {
                            string[] weaponArray = new string[7];
                            weaponArray = iteminlist.props.Split(",");
                            if (weaponArray[1] == "1")
                            {
                                Helper.SendNotificationWithoutButton(player, "Der Spieler darf keine Waffen/Schutzwesten ausgerüstet haben!", "error", "top-end");
                                return;
                            }
                        }
                    }
                    if (account.adminlevel < accounttarget.adminlevel)
                    {
                        Helper.SendNotificationWithoutButton(player, "Der angegebene Spieler hat ein höheres Adminlevel als du!", "error", "top-end");
                        return;
                    }
                    if (model.ToLower() != "standard")
                    {
                        uint skinhash = NAPI.Util.GetHashKey(model);
                        if (skinhash <= 0)
                        {
                            Helper.SendNotificationWithoutButton(player, "Ungültiges Model!", "error", "top-end");
                            return;
                        }
                        NAPI.Player.SetPlayerSkin(ntarget, skinhash);
                    }
                    else
                    {
                        if (character.gender == 1)
                        {
                            NAPI.Player.SetPlayerSkin(ntarget, NAPI.Util.GetHashKey("mp_m_freemode_01"));
                        }
                        else
                        {
                            NAPI.Player.SetPlayerSkin(ntarget, NAPI.Util.GetHashKey("mp_f_freemode_01"));
                        }
                        NAPI.Task.Run(() =>
                        {
                            JObject obj = JObject.Parse(character.json);
                            CharacterController.SetCharacterJson(ntarget, obj, character.clothing);
                            Helper.GetCharacterTattoos(ntarget, character.id);
                        }, delayTime: 165);
                    }
                    model = model[0].ToString().ToUpper() + model.Substring(1);
                    Helper.SendNotificationWithoutButton(player, $"Du hast den Skin vom Spieler {accounttarget.name} erfolgreich auf {model} gesetzt!", "success", "top-end", 5500);
                    Helper.SendNotificationWithoutButton(ntarget, $"Dein Skin wurde von {account.name} erfolgreich auf {model} gesetzt!", "success", "top-end", 5500);
                }
                catch (Exception e)
                {
                    Helper.ConsoleLog("error", $"[CMD_setskin]: " + e.ToString());
                }
            });
        }

        [Command("whitelist", "/whitelist [Name] [Socialclub-ID]")]
        public void CMD_whitelist(Player player, string name, ulong socialclubid)
        {
            try
            {
                Account account = Helper.GetAccountData(player);
                if (!Account.IsPlayerLoggedIn(player)) return;
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Administrator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                foreach (Whitelist whitelist2 in Helper.whitelistList.ToList())
                {
                    if (whitelist2.socialclubid == socialclubid)
                    {
                        Helper.SendNotificationWithoutButton(player, "Der Spieler wurde von der Whitelist entfernt!", "success", "top-end");
                        Helper.whitelistList.Remove(whitelist2);
                        db.Delete<Whitelist>(whitelist2);
                        return;
                    }
                }
                if (name.Length < 3 || name.Length > 35 || socialclubid <= 5000)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültige Eingaben!", "error", "top-end");
                    return;
                }
                Whitelist whitelist = new Whitelist();
                whitelist.name = name;
                whitelist.socialclubid = socialclubid;
                whitelist.timestamp = Helper.UnixTimestamp();
                Helper.whitelistList.Add(whitelist);
                db.Insert(whitelist);
                Helper.SendNotificationWithoutButton(player, "Der Spieler wurde auf die Whitelist gesetzt!", "success", "top-end");
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CMD_whitelist]: " + e.ToString());
            }
        }

        [Command("agl", "Befehl: /agl [Spieler/ID] [Lizenz]")]
        public void cmd_agl(Player player, string target = "n/A", string lizenz = "n/A")
        {
            try
            {
                int value = 0;
                bool error = true;
                Account account = Helper.GetAccountData(player);
                if (!Account.IsPlayerLoggedIn(player)) return;
                if (player.GetOwnSharedData<bool>("Player:Testmodus") == false && !Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Administrator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                if (target == "n/A" || lizenz == "n/A")
                {
                    player.SendChatMessage("~w~Befehl: /agl [Spieler/ID] [Lizenz]");
                    player.SendChatMessage("~y~Verfügbare Lizenzen: Führerschein, Motorradschein, Truckerschein, Bootsschein, Flugschein, Waffenschein");
                    return;
                }
                Player ntarget = Helper.GetPlayerByAccountName(target);
                if (ntarget == null || !ntarget.Exists || !Account.IsPlayerLoggedIn(ntarget))
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Spieler!", "error", "top-end");
                    return;
                }
                Account accounttarget = Helper.GetAccountData(ntarget);
                Character character = Helper.GetCharacterData(ntarget);
                TempData tempData = Helper.GetCharacterTempData(ntarget);
                if (accounttarget == null || character == null || tempData == null)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Spieler!", "error", "top-end");
                    return;
                }
                string[] licArray = new string[6];
                licArray = character.licenses.Split("|");
                switch (lizenz.ToLower())
                {
                    case "führerschein":
                        {
                            if (Convert.ToInt32(licArray[0]) == 1)
                            {
                                licArray[0] = "" + 0;
                                value = 0;
                                error = false;
                            }
                            else
                            {
                                licArray[0] = "" + 1;
                                value = 1;
                                error = false;
                            }
                            break;
                        }
                    case "motorradschein":
                        {
                            if (Convert.ToInt32(licArray[1]) == 1)
                            {
                                licArray[1] = "" + 0;
                                value = 0;
                                error = false;
                            }
                            else
                            {
                                licArray[1] = "" + 1;
                                value = 1;
                                error = false;
                            }
                            break;
                        }
                    case "truckerschein":
                        {
                            if (Convert.ToInt32(licArray[2]) == 1)
                            {
                                licArray[2] = "" + 0;
                                value = 0;
                                error = false;
                            }
                            else
                            {
                                licArray[2] = "" + 1;
                                value = 1;
                                error = false;
                            }
                            break;
                        }
                    case "bootsschein":
                        {
                            if (Convert.ToInt32(licArray[3]) == 1)
                            {
                                licArray[3] = "" + 0;
                                value = 0;
                                error = false;
                            }
                            else
                            {
                                licArray[3] = "" + 1;
                                value = 1;
                                error = false;
                            }
                            break;
                        }
                    case "flugschein":
                        {
                            if (Convert.ToInt32(licArray[4]) == 1)
                            {
                                licArray[4] = "" + 0;
                                value = 0;
                                error = false;
                            }
                            else
                            {
                                licArray[4] = "" + 1;
                                value = 1;
                                error = false;
                            }
                            break;
                        }
                    case "waffenschein":
                        {
                            if (Convert.ToInt32(licArray[5]) == 1)
                            {
                                licArray[5] = "" + 0;
                                value = 0;
                                error = false;
                            }
                            else
                            {
                                licArray[5] = "" + 1;
                                value = 1;
                                error = false;
                            }
                            break;
                        }
                    default:
                        {
                            player.SendChatMessage("~w~Befehl: /agl [Spieler/ID] [Lizenz]");
                            player.SendChatMessage("~y~Verfügbare Lizenzen: Führerschein, Motorradschein, Truckerschein, Bootsschein, Flugschein, Waffenschein");
                            break;
                        }
                }
                if (error == true)
                {
                    player.SendChatMessage("~w~Befehl: /agl [Spieler/ID] [Lizenz]");
                    player.SendChatMessage("~y~Verfügbare Lizenzen: Führerschein, Motorradschein, Truckerschein, Bootsschein, Flugschein, Waffenschein");
                    return;
                }
                lizenz = lizenz[0].ToString().ToUpper() + lizenz.Substring(1);
                Helper.SendNotificationWithoutButton(player, $"Wert des {lizenz}s des Spielers {accounttarget.name} auf {value} gesetzt!", "success", "top-end", 5500);
                Helper.SendNotificationWithoutButton(ntarget, $"{account.name} hat den Wert des {lizenz}s auf {value} gesetzt!", "success", "top-end", 5500);
                Helper.CreateAdminLog("adminlog", $"{account.name} hat den Wert des {lizenz}s des Spielers {accounttarget.name} auf {value} gesetzt!");
                character.licenses = String.Join("|", licArray);
            }
            catch (Exception)
            {
                player.SendChatMessage("~w~Befehl: /agl [Spieler/ID] [Lizenz]");
                player.SendChatMessage("~y~Verfügbare Lizenzen: Führerschein, Motorradschein, Truckerschein, Bootsschein, Flugschein, Waffenschein");
            }
        }

        [Command("set", "Befehl: /set [Spieler/ID] [Option] [Menge]")]
        public void CMD_set(Player player, string target = "n/A", string option = "n/A", string menge = "1")
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Player ntarget = Helper.GetPlayerByNameOrID(target);
                if (player.GetOwnSharedData<bool>("Player:Testmodus") == false && !Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Administrator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                if (target == "n/A")
                {
                    player.SendChatMessage("~w~Befehl: /set [Spieler/ID] [Option] [Menge]");
                    player.SendChatMessage("~y~Verfügbare Aktionen: Leben, Geld, Dimension, GruppierungsMember, GruppierungsLeader, FraktionsMember, FraktionsLeader, FraktionsKick, Truckerskill, Diebesskill, Craftingskill, Angelskill, Busskill, Landwirtskill, Premium-Bronze, Premium-Silber, Premium-Gold, Level, Spielstunden, Erfahrungspunkte, Krankheit, Coins");
                    return;
                }
                if (ntarget == null || !ntarget.Exists || !Account.IsPlayerLoggedIn(ntarget))
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Spieler!", "error", "top-end");
                    return;
                }
                Account accounttarget = Helper.GetAccountData(ntarget);
                Account account = Helper.GetAccountData(player);
                Character character = Helper.GetCharacterData(ntarget);
                TempData tempData = Helper.GetCharacterTempData(ntarget);
                if (character == null)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Spieler!", "error", "top-end");
                    return;
                }
                if (account.adminlevel < accounttarget.adminlevel)
                {
                    Helper.SendNotificationWithoutButton(player, "Der angegebene Spieler hat ein höheres Adminlevel als du!", "error", "top-end");
                    return;
                }
                if (option.ToLower() == "vw")
                {
                    option = "dimension";
                }
                int number = Convert.ToInt32(menge);
                switch (option.ToLower())
                {
                    case "dimension":
                        {
                            if (number < 0 || number > 1000000)
                            {
                                Helper.SendNotificationWithoutButton(player, "Ungültige Dimension!", "error", "top-end");
                                return;
                            }
                            ntarget.Dimension = (uint)number;
                            break;
                        }
                    case "geld":
                        {
                            if (number < -25000000 || number > 25000000)
                            {
                                Helper.SendNotificationWithoutButton(player, "Ungültige Menge!", "error", "top-end");
                                return;
                            }
                            CharacterController.SetMoney(ntarget, number);
                            break;
                        }
                    case "level":
                        {
                            if (number < 1 || number > 999)
                            {
                                Helper.SendNotificationWithoutButton(player, "Ungültige Menge!", "error", "top-end");
                                return;
                            }
                            accounttarget.level = number;
                            break;
                        }
                    case "spielstunden":
                        {
                            if (number < 1 || number > 999)
                            {
                                Helper.SendNotificationWithoutButton(player, "Ungültige Menge!", "error", "top-end");
                                return;
                            }
                            accounttarget.play_time = number;
                            break;
                        }
                    case "erfahrungspunkte":
                        {
                            if (number < 0 || number > 9999)
                            {
                                Helper.SendNotificationWithoutButton(player, "Ungültige Menge!", "error", "top-end");
                                return;
                            }
                            accounttarget.play_points += number;
                            Helper.CheckLevelUp(ntarget);
                            break;
                        }
                    case "leben":
                        {
                            if (number < 0 || number > 275)
                            {
                                Helper.SendNotificationWithoutButton(player, "Ungültier Wert!", "error", "top-end");
                                return;
                            }
                            Helper.SetPlayerHealth(ntarget, number);
                            break;
                        }
                    case "truckerskill":
                        {
                            if (number < 0 || number > 225)
                            {
                                Helper.SendNotificationWithoutButton(player, "Der max. Wert ist 225 (Truckerskill Level 5)!", "error", "top-end");
                                return;
                            }
                            character.truckerskill = number; ;
                            break;
                        }
                    case "angelskill":
                        {
                            if (number < 0 || number > 175)
                            {
                                Helper.SendNotificationWithoutButton(player, "Der max. Wert ist 175 (Angelskill Level 5)!", "error", "top-end");
                                return;
                            }
                            character.fishingskill = number;
                            break;
                        }
                    case "busskill":
                        {
                            if (number < 0 || number > 175)
                            {
                                Helper.SendNotificationWithoutButton(player, "Der max. Wert ist 175 (Busskill Level 5)!", "error", "top-end");
                                return;
                            }
                            character.busskill = number;
                            break;
                        }
                    case "diebesskill":
                        {
                            if (number < 0 || number > 150)
                            {
                                Helper.SendNotificationWithoutButton(player, "Der max. Wert ist 150 (Diebesskill Level 5)!", "error", "top-end");
                                return;
                            }
                            character.thiefskill = number;
                            break;
                        }
                    case "craftingskill":
                        {
                            if (number < 0 || number > 75)
                            {
                                Helper.SendNotificationWithoutButton(player, "Der max. Wert ist 75 (Craftingskill Level 3)!", "error", "top-end");
                                return;
                            }
                            character.craftingskill = number;
                            break;
                        }
                    case "landwirtskill":
                        {
                            if (number < 0 || number > 150)
                            {
                                Helper.SendNotificationWithoutButton(player, "Der max. Wert ist 150 (Landwirtskill Level 5)!", "error", "top-end");
                                return;
                            }
                            character.farmingskill = number;
                            break;
                        }
                    case "premium-bronze":
                        {
                            if (number < 0 || number > 365)
                            {
                                Helper.SendNotificationWithoutButton(player, "Der Wert muss zwischen 0 und 365 liegen!", "error", "top-end");
                                return;
                            }
                            if (number == 0)
                            {
                                accounttarget.premium = 0;
                                accounttarget.premium_time = Helper.UnixTimestamp();
                            }
                            else
                            {
                                accounttarget.premium = 1;
                                accounttarget.premium_time = Helper.UnixTimestamp() + (number * 86400);
                            }
                            break;
                        }
                    case "premium-silber":
                        {
                            if (number < 0 || number > 365)
                            {
                                Helper.SendNotificationWithoutButton(player, "Der Wert muss zwischen 0 und 365 liegen!", "error", "top-end");
                                return;
                            }
                            if (number == 0)
                            {
                                accounttarget.premium = 0;
                                accounttarget.premium_time = Helper.UnixTimestamp();
                            }
                            else
                            {
                                accounttarget.premium = 2;
                                accounttarget.premium_time = Helper.UnixTimestamp() + (number * 86400);
                            }
                            break;
                        }
                    case "premium-gold":
                        {
                            if (number < 0 || number > 365)
                            {
                                Helper.SendNotificationWithoutButton(player, "Der Wert muss zwischen 0 und 365 liegen!", "error", "top-end");
                                return;
                            }
                            if (number == 0)
                            {
                                accounttarget.premium = 0;
                                accounttarget.premium_time = Helper.UnixTimestamp();
                            }
                            else
                            {
                                accounttarget.premium = 3;
                                accounttarget.premium_time = Helper.UnixTimestamp() + (number * 86400);
                            }
                            break;
                        }
                    case "coins":
                        {
                            if (number < 0 || number > 300)
                            {
                                Helper.SendNotificationWithoutButton(player, "Der Wert muss zwischen 0 und 300 liegen!", "error", "top-end");
                                return;
                            }
                            accounttarget.coins += number;
                            Account.SaveAccount(ntarget);
                            break;
                        }
                    case "krankheit":
                        {
                            if (number < 0 || number > 2)
                            {
                                Helper.SendNotificationWithoutButton(player, "Der Wert muss zwischen 0 und 2 liegen!", "error", "top-end");
                                return;
                            }
                            character.disease = number;
                            break;
                        }
                    case "job":
                        {
                            if (number < 0 || number > 8)
                            {
                                Helper.SendNotificationWithoutButton(player, "Es gibt nur 8 Job/s!", "error", "top-end");
                                return;
                            }
                            character.job = number;
                            player.SetOwnSharedData("Player:Job", menge);
                            break;
                        }
                    case "gruppierungsmember":
                        {
                            if (tempData.order != null)
                            {
                                Helper.SendNotificationWithoutButton(player, "Dieser Wert kann beim Spieler jetzt nicht gesetzt werden!", "error", "top-end");
                                return;
                            }
                            Groups group = GroupsController.GetGroupById(number);
                            if (group != null)
                            {
                                List<GroupsMembers> groupMembers = GroupsController.GetGroupMembers(group.id);
                                if (groupMembers != null)
                                {
                                    foreach (GroupsMembers groupMemberInList in groupMembers)
                                    {
                                        if (groupMemberInList.name == character.name)
                                        {
                                            Helper.SendNotificationWithoutButton(player, "Der Spieler ist schon in der Gruppierung und muss diese erst verlassen!", "error", "top-end");
                                            return;
                                        }
                                    }
                                }
                                group.members++;
                                character.mygroup = group.id;
                                MySqlCommand command = General.Connection.CreateCommand();
                                command.CommandText = "INSERT INTO groups_members (groupsid, charid, rang, duty_time, payday, payday_day, since) VALUES (@groupsid, @charid, @rang, @duty_time, @payday, @payday_day, @since)";
                                command.Parameters.AddWithValue("@groupsid", group.id);
                                command.Parameters.AddWithValue("@charid", character.id);
                                command.Parameters.AddWithValue("@rang", 1);
                                command.Parameters.AddWithValue("@duty_time", 0);
                                command.Parameters.AddWithValue("@payday", 0);
                                command.Parameters.AddWithValue("@payday_day", 0);
                                command.Parameters.AddWithValue("@since", Helper.UnixTimestamp());
                                command.ExecuteNonQuery();
                                ntarget.SetOwnSharedData("Player:Group", group.id);
                                ntarget.SetOwnSharedData("Player:GroupRang", 12);
                                Helper.CreateUserTimeline(accounttarget.id, character.id, $"Gruppierung {group.name} beigetreten", 4);
                                Helper.CreateGroupLog(group.id, $"{account.name} hat {accounttarget.name} administrativ in die Gruppierung gesetzt!");
                                GroupsController.UpdateGroupStats(ntarget);
                                GroupsController.SaveGroup(group);
                            }
                            else
                            {
                                Helper.SendNotificationWithoutButton(player, "Ungültige Gruppierung!", "error", "top-end");
                                return;
                            }
                            break;
                        }
                    case "gruppierungsleader":
                        {
                            if (tempData.order != null)
                            {
                                Helper.SendNotificationWithoutButton(player, "Dieser Wert kann beim Spieler jetzt nicht gesetzt werden!", "error", "top-end");
                                return;
                            }
                            Groups group = GroupsController.GetGroupById(number);
                            if (group != null)
                            {
                                List<GroupsMembers> groupMembers = GroupsController.GetGroupMembers(group.id);
                                if (groupMembers != null)
                                {
                                    foreach (GroupsMembers groupMemberInList in groupMembers)
                                    {
                                        if (groupMemberInList.name == character.name)
                                        {
                                            Helper.SendNotificationWithoutButton(player, "Der Spieler ist schon in der Gruppierung und muss diese erst verlassen!", "error", "top-end");
                                            return;
                                        }
                                    }
                                }
                                group.members++;
                                group.leader = character.id;
                                character.mygroup = group.id;
                                MySqlCommand command = General.Connection.CreateCommand();
                                command.CommandText = "INSERT INTO groups_members (groupsid, charid, rang, duty_time, payday, payday_day, since) VALUES (@groupsid, @charid, @rang, @duty_time, @payday, @payday_day, @since)";
                                command.Parameters.AddWithValue("@groupsid", group.id);
                                command.Parameters.AddWithValue("@charid", character.id);
                                command.Parameters.AddWithValue("@rang", 12);
                                command.Parameters.AddWithValue("@duty_time", 0);
                                command.Parameters.AddWithValue("@payday", 0);
                                command.Parameters.AddWithValue("@payday_day", 0);
                                command.Parameters.AddWithValue("@since", Helper.UnixTimestamp());
                                command.ExecuteNonQuery();
                                ntarget.SetOwnSharedData("Player:Group", group.id);
                                ntarget.SetOwnSharedData("Player:GroupRang", 12);
                                Helper.CreateUserTimeline(accounttarget.id, character.id, $"Gruppierung {group.name} beigetreten", 4);
                                Helper.CreateGroupLog(group.id, $"{account.name} hat {accounttarget.name} administrativ als Leader in die Gruppierung gesetzt!");
                                GroupsController.UpdateGroupStats(ntarget);
                                GroupsController.SaveGroup(group);
                            }
                            else
                            {
                                Helper.SendNotificationWithoutButton(player, "Ungültige Gruppierung!", "error", "top-end");
                                return;
                            }
                            break;
                        }
                    case "fraktionsmember":
                        {
                            if (tempData.order != null)
                            {
                                Helper.SendNotificationWithoutButton(player, "Dieser Wert kann beim Spieler jetzt nicht gesetzt werden!", "error", "top-end");
                                return;
                            }
                            FactionsModel faction = FactionController.GetFactionById(number);
                            if (faction != null)
                            {
                                if (character.faction == number)
                                {
                                    Helper.SendNotificationWithoutButton(player, "Der Spieler ist schon in der Fraktion und muss diese erst verlassen!", "error", "top-end");
                                    return;
                                }
                                if (character.factionduty == true)
                                {
                                    Helper.SendNotificationWithoutButton(player, "Der Spieler muss zuerst seinen aktuellen Dienst beenden!", "error", "top-end");
                                    return;
                                }
                                faction.members++;
                                character.faction = faction.id;
                                character.rang = 1;
                                character.faction_dutytime = 0;
                                character.swat = 0;
                                character.dutyjson = "{\"clothing\":[-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],\"clothingColor\":[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]}";

                                MySqlCommand command = General.Connection.CreateCommand();
                                command.CommandText = "DELETE FROM outfits WHERE owner = @owner";
                                command.Parameters.AddWithValue("@owner", "faction-" + character.id);
                                command.ExecuteNonQuery();

                                if (character.faction == 1)
                                {
                                    character.armor = 4;
                                    character.armorcolor = 0;
                                    if (NAPI.Player.GetPlayerArmor(ntarget) > 0)
                                    {
                                        NAPI.Player.SetPlayerClothes(ntarget, 9, character.armor, character.armorcolor);
                                    }
                                }
                                else
                                {
                                    character.armor = 7;
                                    character.armorcolor = 0;
                                    if (NAPI.Player.GetPlayerArmor(ntarget) > 0)
                                    {
                                        NAPI.Player.SetPlayerClothes(ntarget, 9, character.armor, character.armorcolor);
                                    }
                                }

                                character.faction_since = Helper.UnixTimestamp();
                                ntarget.SetOwnSharedData("Player:Faction", faction.id);
                                ntarget.SetOwnSharedData("Player:FactionRang", 1);
                                Helper.CreateUserTimeline(accounttarget.id, character.id, $"Fraktion {faction.name} beigetreten", 4);
                                Helper.CreateFactionLog(faction.id, $"{account.name} hat {accounttarget.name} administrativ in die Fraktion gesetzt!");
                                FactionController.UpdateFactionStats(ntarget);
                                CharacterController.SaveCharacter(ntarget);
                            }
                            else
                            {
                                Helper.SendNotificationWithoutButton(player, "Ungültige Fraktion!", "error", "top-end");
                                return;
                            }
                            break;
                        }
                    case "fraktionsleader":
                        {
                            if (tempData.order != null)
                            {
                                Helper.SendNotificationWithoutButton(player, "Dieser Wert kann beim Spieler jetzt nicht gesetzt werden!", "error", "top-end");
                                return;
                            }
                            FactionsModel faction = FactionController.GetFactionById(number);
                            if (faction != null)
                            {
                                if (character.faction == number && faction.leader == accounttarget.id)
                                {
                                    Helper.SendNotificationWithoutButton(player, "Der Spieler ist schon in der Fraktion (als Leader) und muss diese erst verlassen!", "error", "top-end");
                                    return;
                                }
                                if (character.factionduty == true)
                                {
                                    Helper.SendNotificationWithoutButton(player, "Der Spieler muss zuerst seinen Dienst beenden!", "error", "top-end");
                                    return;
                                }
                                faction.members++;
                                faction.leader = accounttarget.id;
                                character.faction = faction.id;
                                character.rang = 12;
                                character.swat = 0;
                                character.dutyjson = "{\"clothing\":[-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],\"clothingColor\":[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]}";
                                character.faction_dutytime = 0;
                                character.faction_since = Helper.UnixTimestamp();

                                MySqlCommand command = General.Connection.CreateCommand();
                                command.CommandText = "DELETE FROM outfits WHERE owner = @owner";
                                command.Parameters.AddWithValue("@owner", "faction-" + character.id);
                                command.ExecuteNonQuery();

                                if (character.faction == 1)
                                {
                                    character.armor = 4;
                                    character.armorcolor = 0;
                                    if (NAPI.Player.GetPlayerArmor(ntarget) > 0)
                                    {
                                        NAPI.Player.SetPlayerClothes(ntarget, 9, character.armor, character.armorcolor);
                                    }
                                }
                                else
                                {
                                    character.armor = 7;
                                    character.armorcolor = 0;
                                    if (NAPI.Player.GetPlayerArmor(ntarget) > 0)
                                    {
                                        NAPI.Player.SetPlayerClothes(ntarget, 9, character.armor, character.armorcolor);
                                    }
                                }
                                if (character.faction == 4)
                                {
                                    Bank bank = BankController.GetBankByBankNumber("SA3701-100000");
                                    if (bank != null)
                                    {
                                        bank.ownercharid = character.id;
                                    }
                                    bank.pincode = Helper.GeneratePin(4);

                                    command = General.Connection.CreateCommand();

                                    command.CommandText = "UPDATE characters SET items = REPLACE(items, '" + bank.banknumber + "', 'n/A') WHERE items LIKE @banknumber";
                                    command.Parameters.AddWithValue("@banknumber", "%" + bank.banknumber + "%");

                                    command.ExecuteNonQuery();

                                    foreach (Player p in NAPI.Pools.GetAllPlayers())
                                    {
                                        if (p != null && p.GetOwnSharedData<bool>("Player:Spawned") == true)
                                        {
                                            Character character2 = Helper.GetCharacterData(p);
                                            if (character2 != null)
                                            {
                                                ItemsController.DeleteItemWithProp(p, "" + bank.banknumber);
                                                ItemsController.UpdateInventory(p);
                                            }
                                        }
                                    }

                                    Helper.SendNotificationWithoutButton(player, "Pin vom Staatskonto: " + bank.pincode, "info", "top-end", 7500);

                                    Items newitem = ItemsController.CreateNewItem(ntarget, character.id, "EC-Karte", "Player", 1, ItemsController.GetFreeItemID(ntarget));
                                    if (newitem != null)
                                    {
                                        newitem.props = "SA3701-100000";
                                        tempData.itemlist.Add(newitem);
                                    }
                                }
                                ntarget.SetOwnSharedData("Player:Faction", faction.id);
                                ntarget.SetOwnSharedData("Player:FactionRang", 12);
                                Helper.CreateUserTimeline(accounttarget.id, character.id, $"Fraktion {faction.name} beigetreten", 4);
                                Helper.CreateFactionLog(faction.id, $"{account.name} hat {accounttarget.name} administrativ als Leader in die Fraktion gesetzt!");
                                FactionController.UpdateFactionStats(ntarget);
                                Helper.SaveFactions();
                                CharacterController.SaveCharacter(ntarget);
                            }
                            else
                            {
                                Helper.SendNotificationWithoutButton(player, "Ungültige Fraktion!", "error", "top-end");
                                return;
                            }
                            break;
                        }
                    case "fraktionskick":
                        {
                            if (tempData.order != null)
                            {
                                Helper.SendNotificationWithoutButton(player, "Dieser Wert kann beim Spieler jetzt nicht gesetzt werden!", "error", "top-end");
                                return;
                            }
                            if (character.factionduty == true)
                            {
                                Helper.SendNotificationWithoutButton(player, "Der Spieler muss zuerst seinen Dienst beenden!", "error", "top-end");
                                return;
                            }
                            FactionsModel faction = FactionController.GetFactionById(character.faction);
                            if (faction != null)
                            {
                                faction.leader = 0;
                                if (character.faction == 1)
                                {
                                    character.armor = 2;
                                    character.armorcolor = 0;
                                    if (NAPI.Player.GetPlayerArmor(ntarget) > 0)
                                    {
                                        NAPI.Player.SetPlayerClothes(ntarget, 9, character.armor, character.armorcolor);
                                    }
                                }
                                TempData tempData2 = Helper.GetCharacterTempData(ntarget);
                                if (tempData.radio != "")
                                {
                                    ntarget.TriggerEvent("Client:Leaveradio", tempData2.radio);
                                    tempData2.radio = "";
                                    tempData2.radiols = false;
                                }
                                character.faction = 0;
                                character.rang = 0;
                                character.faction_dutytime = 0;
                                character.dutyjson = "{\"clothing\":[-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],\"clothingColor\":[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]}";
                                character.faction_since = Helper.UnixTimestamp();

                                MySqlCommand command = General.Connection.CreateCommand();
                                command.CommandText = "DELETE FROM outfits WHERE owner = @owner";
                                command.Parameters.AddWithValue("@owner", "faction-" + character.id);
                                command.ExecuteNonQuery();

                                ntarget.SetOwnSharedData("Player:Faction", 0);
                                ntarget.SetOwnSharedData("Player:FactionRang", 0);
                                Helper.CreateUserTimeline(accounttarget.id, character.id, $"Fraktion {faction.name} verlassen", 4);
                                Helper.CreateFactionLog(faction.id, $"{account.name} hat {accounttarget.name} administrativ aus der Fraktion rausgeworfen!");
                                FactionController.UpdateFactionStats(ntarget);
                                CharacterController.SaveCharacter(ntarget);
                            }
                            else
                            {
                                Helper.SendNotificationWithoutButton(player, "Der Spieler befindet sich in keiner Fraktion!", "error", "top-end");
                            }
                            break;
                        }
                    default:
                        {
                            player.SendChatMessage("~w~Befehl: /set [Spieler/ID] [Option] [Menge]");
                            player.SendChatMessage("~y~Verfügbare Aktionen: Leben, Geld, Dimension, GruppierungsMember, GruppierungsLeader, FraktionsMember, FraktionsLeader, FraktionsKick, Truckerskill, Diebesskill, Angelskill, Busskill, Landwirtskill, Craftingskill, Premium-Bronze, Premium-Silber, Premium-Gold, Level, Spielstunden, Erfahrungspunkte, Krankheit, Coins");
                            return;
                        }
                }
                option = option[0].ToString().ToUpper() + option.Substring(1);
                Helper.SendNotificationWithoutButton(player, "Du hast den Wert " + option + " des Spielers, auf " + menge + " gesetzt!", "success", "top-end", 5500);
                Helper.SendNotificationWithoutButton(ntarget, "Dein/e " + option + " Wert wurde von " + account.name + ", auf " + menge + "  gesetzt!", "success", "top-end", 5500);
                Helper.CreateAdminLog("adminlog", $"{accounttarget.name}'s {option} Wert wurde von {account.name} auf {menge} gesetzt!");
            }
            catch (Exception)
            {
                player.SendChatMessage("~w~Befehl: /set [Spieler/ID] [Option] [Menge]");
                player.SendChatMessage("~y~Verfügbare Aktionen: Leben, Geld, Dimension, GruppierungsMember, GruppierungsLeader, FraktionsMember, FraktionsLeader, FraktionsKick, Truckerskill, Diebesskill, Angelskill, Busskill, Landwirtskill, Premium-Bronze, Premium-Silber, Premium-Gold, Level, Spielstunden, Erfahrungspunkte, Krankheit, Coins");
            }
        }

        [Command("prison", "Befehl: /prison [Spieler/ID] [Checkpoints] [Grund]", GreedyArg = true)]
        public void cmd_prison(Player player, string target, int checkpoints, string text)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Player ntarget = Helper.GetPlayerByNameOrID(target);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.ProbeModerator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                if (ntarget == null || !Account.IsPlayerLoggedIn(ntarget))
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Spieler!", "error", "top-end");
                    return;
                }
                if (text.Length < 3 || text.Length > 35)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Grund!", "error", "top-end");
                    return;
                }
                if (checkpoints < 1 || checkpoints > 999999)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültige Anzahl an Checkpoints!", "error", "top-end");
                    return;
                }
                Account account = Helper.GetAccountData(player);
                Account account2 = Helper.GetAccountData(ntarget);
                Character character = Helper.GetCharacterData(ntarget);
                if (account == null || account2 == null || character == null)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Spieler!", "error");
                    return;
                }
                if (account.adminlevel < account2.adminlevel)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Spieler!", "error");
                    return;
                }
                if (ntarget == player || account.name == "Nemesus")
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Spieler!", "error");
                    return;
                }
                if (account2.prison > 0)
                {
                    Helper.SendNotificationWithoutButton(player, "Der Spieler befindet sich schon im Prison!", "error");
                    return;
                }
                if (account.adminlevel == (int)Account.AdminRanks.ProbeModerator && checkpoints > 150)
                {
                    Helper.SendNotificationWithoutButton(player, "Du kannst den Spieler für max. 150 Checkpoints ins Prison stecken!", "error");
                }
                if (account.adminlevel == (int)Account.AdminRanks.Moderator && checkpoints > 250)
                {
                    Helper.SendNotificationWithoutButton(player, "Du kannst den Spieler für max. 250 Checkpoints ins Prison stecken!", "error");
                }
                if (account.adminlevel == (int)Account.AdminRanks.Supporter && checkpoints > 375)
                {
                    Helper.SendNotificationWithoutButton(player, "Du kannst den Spieler für max. 375 Checkpoints ins Prison stecken!", "error");
                }
                if (account.adminlevel == (int)Account.AdminRanks.Administrator && checkpoints > 500)
                {
                    Helper.SendNotificationWithoutButton(player, "Du kannst den Spieler für max. 500 Checkpoints ins Prison stecken!", "error");
                }
                if (account.adminlevel == (int)Account.AdminRanks.HighAdministrator && checkpoints > 1000)
                {
                    Helper.SendNotificationWithoutButton(player, "Du kannst den Spieler für max. 1000 Checkpoints ins Prison stecken!", "error");
                }
                if (account.adminlevel == (int)Account.AdminRanks.Manager && checkpoints > 5000)
                {
                    Helper.SendNotificationWithoutButton(player, "Du kannst den Spieler für max. 5000 Checkpoints ins Prison stecken!", "error");
                }
                if (account.adminlevel == (int)Account.AdminRanks.Projektleiter && checkpoints > 999999)
                {
                    Helper.SendNotificationWithoutButton(player, "Du kannst den Spieler für max. 999999 Checkpoints ins Prison stecken!", "error");
                }
                if (ntarget.GetData<bool>("Player:Spectate") == true || ntarget.GetOwnSharedData<bool>("Player:DevModus") == true)
                {
                    Helper.SendNotificationWithoutButton(player, "Dieser Spieler kann jetzt nicht ins Prison gesteckt werden!", "error", "top-end");
                    return;
                }
                character.lastpos = $"{ntarget.Position.X}|{ntarget.Position.Y}|{ntarget.Position.Z}|{ntarget.Rotation.Z}|{ntarget.Dimension}";
                NAPI.Player.SetPlayerCurrentWeapon(ntarget, WeaponHash.Unarmed);
                Helper.SetPlayerPosition(ntarget, new Vector3(-1999.6619, 1113.5583, -27.363804));
                ntarget.Dimension = (uint)(player.Id + 500);
                ntarget.TriggerEvent("Client:StartPrison", checkpoints, ntarget.Dimension);
                account2.prison = checkpoints;
                string prisonMessage = $"wurde von/m {account.name} für {checkpoints} Checkpoints ins Prison gesteckt";
                string message2 = $"~r~Server: {account2.name} {prisonMessage}, Grund: {text}!";
                string discordMessage = $"{account2.name} {prisonMessage}, Grund: {text}!";
                foreach (Player p in NAPI.Pools.GetAllPlayers())
                {
                    if (p != null && p.GetOwnSharedData<bool>("Player:Spawned") == true)
                    {
                        Helper.SendChatMessage(p, message2);
                    }
                }

                string message = $"~r~Server: Du wurdest von/m {account.name} für {checkpoints} Checkpoints ins Prison gesteckt, Grund: {text}!";
                Helper.SendChatMessage(ntarget, message);

                message2 = message2.Remove(0, 3);

                Helper.SendAdminMessage3($"{account2.name} wurde ins Prison gesteckt, Grund: {text}!");

                Helper.DiscordWebhook(Helper.AdminNotificationWebHook, discordMessage, "Gameserver");

                string message3 = $"{account2.name} wurde von/m {account.name} für {checkpoints} Checkpoints ins Prison gesteckt, Grund: {text}!";
                Helper.CreateAdminLog("prisonlog", message3);

                Helper.SendNotificationWithoutButton(player, $"Du hast {account2.name} für {checkpoints} Checkpoints ins Prison gesteckt, Grund: {text}!", "success", "top-end", 5500);
                Helper.SendNotificationWithoutButton(ntarget, $"Du wurdest von {account.name} für {checkpoints} Checkpoints ins Prison gesteckt, Grund: {text}!", "success", "top-end", 5500);

                Helper.CreateUserFile(account2.id, account.name, text, $"{checkpoints} Checkpoints");
                Helper.adminSettings.punishments++;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CMD_prison]: " + e.ToString());
            }
            return;
        }

        [RemoteEvent("Server:StartSpec")]
        [Command("spectate", "Befehl: /spectate [Spieler/ID]", GreedyArg = true, Alias = "spec")]
        public void cmd_spec(Player player, string target, int mod = 0)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Player ntarget = Helper.GetPlayerByNameOrID(target);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.ProbeModerator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    if (mod > 0) { cmd_stopspec(player); }
                    return;
                }
                if (ntarget == null || !Account.IsPlayerLoggedIn(ntarget) || ntarget.GetOwnSharedData<bool>("Player:DevModus") == true || ntarget.GetOwnSharedData<bool>("Player:Spawned") == false)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Spieler!", "error", "top-end");
                    if (mod > 0) { cmd_stopspec(player); }
                    return;
                }
                Account account = Helper.GetAccountData(player);
                Account account2 = Helper.GetAccountData(ntarget);
                Character character = Helper.GetCharacterData(player);
                Character character2 = Helper.GetCharacterData(ntarget);
                if (account.adminlevel < account2.adminlevel)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Spieler!", "error");
                    if (mod > 0) { cmd_stopspec(player); }
                    return;
                }
                if (ntarget == player)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Spieler!", "error");
                    if (mod > 0) { cmd_stopspec(player); }
                    return;
                }
                if (player.GetOwnSharedData<bool>("Player:DevModus") == true)
                {
                    Helper.SendNotificationWithoutButton(player, "Du musst zuerst den Devmodus beenden!", "error");
                    if (mod > 0) { cmd_stopspec(player); }
                    return;
                }
                if (player.IsInVehicle)
                {
                    Helper.SendNotificationWithoutButton(player, "Du musst zuerst aus deinem Fahrzeug aussteigen!", "error", "top-end");
                    if (mod > 0) { cmd_stopspec(player); }
                    return;
                }
                if (account.prison > 0)
                {
                    Helper.SendNotificationWithoutButton(player, "Du kannst jetzt keinen beobachten!", "error", "top-end");
                    if (mod > 0) { cmd_stopspec(player); }
                    return;
                }
                if (ntarget.GetData<bool>("Player:Spectate") == true && mod == 0)
                {
                    Helper.SendNotificationWithoutButton(player, "Du kannst den Spieler jetzt nicht beobachten!", "error", "top-end");
                    return;
                }
                if (mod == 0 && player.GetData<bool>("Player:Spectate") == false)
                {
                    player.SetSharedData("Player:Adminsettings", "1,1,1");
                    character.lastpos = $"{player.Position.X}|{player.Position.Y}|{player.Position.Z}|{player.Rotation.Z}|{player.Dimension}";
                }
                if (mod == 0)
                {
                    NAPI.Player.SetPlayerCurrentWeapon(player, WeaponHash.Unarmed);
                    Helper.SendNotificationWithoutButton(player, $"Du beobachtest jetzt {account2.name}!", "success", "top-end", 5500);
                }
                player.Dimension = ntarget.Dimension;
                if (character2.inhouse != -1)
                {
                    House house = House.GetHouseById(character2.inhouse);
                    if (house != null)
                    {
                        player.TriggerEvent("Client:LoadIPL", House.GetInteriorIPL(house.interior));
                        Furniture.UpdateMöbelList(player, House.GetFurnitureForHouse(house.id));
                    }
                }
                if (mod == 0)
                {
                    player.TriggerEvent("Client:PlayerFreeze", true);
                }
                Helper.SetPlayerPosition(player, ntarget.Position.Add(new Vector3(0, 4.5, -4.5)));
                NAPI.Task.Run(() =>
                {
                    player.TriggerEvent("Client:StartSpectate", ntarget.Id, target);
                    player.SetData<bool>("Player:Spectate", true);
                }, delayTime: 25);
            }
            catch (Exception)
            {
                Helper.SendNotificationWithoutButton(player, "Ungültiger Spieler!", "error");
            }
            return;
        }

        [RemoteEvent("Server:StopSpectate")]
        [Command("stopspec", "Befehl: /stopspec", Alias = "specoff")]
        public static void cmd_stopspec(Player player)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Character character = Helper.GetCharacterData(player);
                if (character == null) return;
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.ProbeModerator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                if (player.GetData<bool>("Player:Spectate") == false)
                {
                    Helper.SendNotificationWithoutButton(player, "Du beobachtest gerade keinen Spieler!", "error", "top-end");
                    return;
                }
                player.TriggerEvent("Client:StopSpectate", 2);
                player.SetData<bool>("Player:Spectate", false);
                player.SetSharedData("Player:Adminsettings", "1,0,0");
                NAPI.Player.SetPlayerCurrentWeapon(player, WeaponHash.Unarmed);
                string[] spawnCharAfterReconnect = new string[5];
                spawnCharAfterReconnect = character.lastpos.Split("|");
                player.Dimension = Convert.ToUInt32(spawnCharAfterReconnect[4]);
                Helper.SetPlayerPosition(player, new Vector3(float.Parse(spawnCharAfterReconnect[0]), float.Parse(spawnCharAfterReconnect[1]), float.Parse(spawnCharAfterReconnect[2]) + 0.15));
                player.Dimension = Convert.ToUInt32(spawnCharAfterReconnect[4]);
                Helper.SendNotificationWithoutButton(player, $"Beobachtung abgebrochen!", "success", "top-end", 3500);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[cmd_stopspec]: " + e.ToString());
            }
            return;
        }

        [Command("unprison", "Befehl: /unprison [Spieler/ID] [Grund]", GreedyArg = true)]
        public void cmd_unprison(Player player, string target, string text)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Player ntarget = Helper.GetPlayerByNameOrID(target);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.ProbeModerator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                if (ntarget == null || !Account.IsPlayerLoggedIn(ntarget))
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Spieler!", "error", "top-end");
                    return;
                }
                if (text.Length < 3 || text.Length > 35)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Grund!", "error", "top-end");
                    return;
                }
                Account account = Helper.GetAccountData(player);
                Account account2 = Helper.GetAccountData(ntarget);
                Character character = Helper.GetCharacterData(ntarget);
                if (account.adminlevel < account2.adminlevel)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Spieler!", "error");
                    return;
                }
                if (account2.prison == 0)
                {
                    Helper.SendNotificationWithoutButton(player, "Der Spieler befindet sich nicht im Prison!", "error");
                    return;
                }
                ntarget.TriggerEvent("Client:StopPrison");
                string prisonMessage = $"wurde von/m {account.name} aus dem Prison geholt";
                string message2 = $"~r~Server: {account.name} {prisonMessage}, Grund: {text}!";
                string discordMessage = $"{account.name} {prisonMessage}, Grund: {text}!";

                foreach (Player p in NAPI.Pools.GetAllPlayers())
                {
                    if (p != null && p.GetOwnSharedData<bool>("Player:Spawned") == true)
                    {
                        Helper.SendChatMessage(p, message2);
                    }
                }

                string message = $"~r~Server: Du wurdest von/m {account.name} aus dem Prison geholt, Grund: {text}!";
                Helper.SendChatMessage(ntarget, message);

                message2 = message2.Remove(0, 3);

                Helper.SendAdminMessage3($"{account.name} wurde aus dem Prison geholt, Grund: {text}!");

                Helper.DiscordWebhook(Helper.AdminNotificationWebHook, discordMessage, "Gameserver");

                string message3 = $"{account2.name} wurde von/m {account.name} aus dem Prison geholt, Grund: {text}!";
                Helper.CreateAdminLog("prisonlog", message3);

                Helper.SendNotificationWithoutButton(player, $"Du hast {account2.name} aus dem Prison geholt, Grund: {text}!", "success", "top-end", 5500);
                Helper.SendNotificationWithoutButton(ntarget, $"{account.name} hat dich aus dem Prison geholt , Grund: {text}!", "success", "top-end", 5500);

                Helper.CreateUserFile(account2.id, account.name, text, $"Aus dem Prison geholt");
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[cmd_unprison]: " + e.ToString());
            }
            return;
        }

        [Command("ban", "Befehl: /ban [Spieler/ID] [Zeit in Minuten/1=Permanent] [Grund]")]
        public void CMD_ban(Player player, string target, int time, string text)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Player ntarget = Helper.GetPlayerByNameOrID(target);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Supporter))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                if (ntarget == null || !Account.IsPlayerLoggedIn(ntarget) || player == ntarget)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Spieler!", "error", "top-end");
                    return;
                }
                if (text.Length < 3 || text.Length > 35)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Grund!", "error", "top-end");
                    return;
                }
                if (time < 0 || time > 525600)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültige Zeitangabe!", "error", "top-end");
                    return;
                }
                Account account = Helper.GetAccountData(player);
                Account account2 = Helper.GetAccountData(ntarget);
                if (account2 == null)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Spieler!", "error");
                    return;
                }
                if (account.adminlevel < account2.adminlevel || account2.name == "Nemesus")
                {
                    Helper.SendNotificationWithoutButton(player, "Du kannst diesen Spieler nicht bannen!", "error");
                    return;
                }
                if (account.adminlevel < (int)Account.AdminRanks.Administrator && time == 1)
                {
                    Helper.SendNotificationWithoutButton(player, "Du kannst den Spieler für max. 1440 Minuten (1 Tag) bannen!", "error");
                }
                if (account.adminlevel < (int)Account.AdminRanks.Administrator && time > 1440)
                {
                    Helper.SendNotificationWithoutButton(player, "Du kannst den Spieler für max. 1440 Minuten (1 Tag) bannen!", "error");
                }
                if (account.adminlevel < (int)Account.AdminRanks.HighAdministrator && time > 10080)
                {
                    Helper.SendNotificationWithoutButton(player, "Du kannst den Spieler für max. 1440 Minuten (1 Woche) bannen!", "error");
                }
                if (account.adminlevel < (int)Account.AdminRanks.Manager && time > 30240)
                {
                    Helper.SendNotificationWithoutButton(player, "Du kannst den Spieler für max. 1440 Minuten (3 Wochen) bannen!", "error");
                }
                if (account.adminlevel < (int)Account.AdminRanks.Projektleiter && time > 1440)
                {
                    Helper.SendNotificationWithoutButton(player, "Du kannst den Spieler für max. 525600 Minuten (1 Jahr) bannen!", "error");
                }
                string banstring = (time == 1) ? "permanent" : $"für {time} Minuten";
                Helper.SendNotificationWithoutButton(player, $"Du hast {account2.name} {banstring} vom Server gebannt, Grund: {text}!", "success", "top-end", 5500);
                AntiCheatController.AddBan(ntarget, text, account.name, time, true);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CMD_ban]: " + e.ToString());
            }
            return;
        }

        [Command("aban", "Befehl: /ban [Spieler/ID] [Zeit in Minuten/1=Permanent] [Grund]")]
        public void CMD_aban(Player player, string target, int time, string text)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Player ntarget = Helper.GetPlayerByNameOrID(target);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Supporter))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                if (ntarget == null || !Account.IsPlayerLoggedIn(ntarget) || player == ntarget)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Spieler!", "error", "top-end");
                    return;
                }
                if (text.Length < 3 || text.Length > 35)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Grund!", "error", "top-end");
                    return;
                }
                if (time < 0 || time > 525600)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültige Zeitangabe!", "error", "top-end");
                    return;
                }
                Account account = Helper.GetAccountData(player);
                Account account2 = Helper.GetAccountData(ntarget);
                if (account2 == null)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Spieler!", "error");
                    return;
                }
                if (account.adminlevel < account2.adminlevel || account2.name == "Nemesus")
                {
                    Helper.SendNotificationWithoutButton(player, "Du kannst diesen Spieler nicht bannen!", "error");
                    return;
                }
                if (account.adminlevel < (int)Account.AdminRanks.HighAdministrator && time > 10080)
                {
                    Helper.SendNotificationWithoutButton(player, "Du kannst den Spieler für max. 1440 Minuten (1 Woche) bannen!", "error");
                }
                if (account.adminlevel < (int)Account.AdminRanks.Manager && time > 30240)
                {
                    Helper.SendNotificationWithoutButton(player, "Du kannst den Spieler für max. 1440 Minuten (3 Wochen) bannen!", "error");
                }
                if (account.adminlevel < (int)Account.AdminRanks.Projektleiter && time > 1440)
                {
                    Helper.SendNotificationWithoutButton(player, "Du kannst den Spieler für max. 525600 Minuten (1 Jahr) bannen!", "error");
                }
                string banstring = (time == 1) ? "permanent" : $"für {time} Minuten";
                Helper.SendNotificationWithoutButton(player, $"Du hast {account2.name} {banstring} vom nAnticheat bannen lassen, Grund: {text}!", "success", "top-end", 5500);
                AntiCheatController.AddBan(ntarget, text, "nAnticheat", time, true);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CMD_ban]: " + e.ToString());
            }
            return;
        }

        [Command("unban", "Befehl: /unban [Name/Identifizierer] [Grund]")]
        public void CMD_unban(Player player, string text, string text2)
        {
            try
            {
                int id = -1;
                int forumid = -1;

                Account account = Helper.GetAccountData(player);

                if (account == null) return;
                if (!Account.IsPlayerLoggedIn(player)) return;

                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Administrator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                if (text2.Length < 3 || text2.Length > 35)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Name/Identifizierer!", "error", "top-end");
                    return;
                }
                if (text2.Length < 3 || text2.Length > 35)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Grund!", "error", "top-end");
                    return;
                }
                MySqlCommand command = General.Connection.CreateCommand();
                command = General.Connection.CreateCommand();
                command.CommandText = "SELECT userid FROM bans where banname = @banname or identifier = @identifier LIMIT 1";
                command.Parameters.AddWithValue("@banname", text);
                command.Parameters.AddWithValue("@identifier", text);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        id = reader.GetInt32("userid");
                    }
                    reader.Close();
                }
                if (id != -1)
                {
                    command.CommandText = "DELETE FROM bans WHERE userid=@userid";
                    command.Parameters.AddWithValue("@userid", id);
                    command.ExecuteNonQuery();

                    command.CommandText = "DELETE FROM inactiv WHERE userid=@id";
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();

                    command.CommandText = "UPDATE users SET ban = 0, bantext='n/A' WHERE id = @id2";
                    command.Parameters.AddWithValue("@id2", id);
                    command.ExecuteNonQuery();

                    command = General.Connection.CreateCommand();
                    command.CommandText = "SELECT forumaccount FROM users where id=@id LIMIT 1";
                    command.Parameters.AddWithValue("@id", id);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            forumid = reader.GetInt32("forumaccount");
                        }
                        reader.Close();
                    }

                    if (forumid != -1)
                    {
                        Helper.ForumUpdate(player, "unban", forumid);
                    }

                    Helper.SendNotificationWithoutButton(player, "Du hast den Spieler erfolgreich entbannt!", "success", "top-end");
                    Helper.SendAdminMessage2($"{account.name} hat {text} entbannt, Grund: {text2}!", 1, true);
                    Helper.CreateAdminLog("adminlog", $"{account.name} hat {text} enbannt, Grund {text2}!");
                    Helper.CreateUserFile(id, account.name, text2, "Entbannt");
                }
                else
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Name/Identifizierer!", "error", "top-end");
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CMD_unban]: " + e.ToString());
            }
            return;
        }

        [Command("resetweapons", "Befehl: /resetweapons [Spieler/ID] [Grund]")]
        public void CMD_resetweapons(Player player, string target, string text)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Player ntarget = Helper.GetPlayerByNameOrID(target);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Moderator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                if (ntarget == null || !Account.IsPlayerLoggedIn(ntarget) || player == ntarget)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Spieler!", "error", "top-end");
                    return;
                }
                if (text.Length < 3 || text.Length > 35)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Grund!", "error", "top-end");
                    return;
                }
                Account account = Helper.GetAccountData(player);
                Account account2 = Helper.GetAccountData(ntarget);
                Character character = Helper.GetCharacterData(ntarget);
                TempData tempData = Helper.GetCharacterTempData(ntarget);
                if (account2 == null)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Spieler!", "error");
                    return;
                }
                if (account.adminlevel < account2.adminlevel || account2.name == "Nemesus")
                {
                    Helper.SendNotificationWithoutButton(player, "Du kannst diesem Spieler nicht die Waffen reseten!", "error");
                    return;
                }
                Helper.CreateAdminLog("adminlog", $"{account.name} hat {account2.name} die Waffen resetet!");
                Helper.SendNotificationWithoutButton(player, $"Du hast {account2.name} Waffen resetet, Grund: {text}!", "success", "top-end", 5500);
                if (text.ToLower() != "silent")
                {
                    Helper.SendNotificationWithoutButton(ntarget, $"{account2.name} hat deine Waffen resetet, Grund: {text}!", "error", "top-end", 5500);
                }
                NAPI.Player.RemoveAllPlayerWeapons(ntarget);
                foreach (Items iteminlist in tempData.itemlist.ToList())
                {
                    if (iteminlist != null && iteminlist.type == 5)
                    {
                        NAPI.Player.SetPlayerWeaponAmmo(player, (WeaponHash)WeaponController.GetWeaponHashFromName(iteminlist.description), 0);
                        NAPI.Player.RemovePlayerWeapon(player, (WeaponHash)WeaponController.GetWeaponHashFromName(iteminlist.description));
                        tempData.weaponTints.Remove($"{WeaponController.GetWeaponHash2FromName(iteminlist.description).ToLower()}");
                        player.SetSharedData("Player:WeaponTint", -1);
                        tempData.weaponComponents.Remove($"{WeaponController.GetWeaponHash2FromName(iteminlist.description).ToLower()}");
                        player.SetSharedData("Player:WeaponComponents", "-1");
                        iteminlist.amount = 0;
                        ItemsController.RemoveItem(player, iteminlist.itemid);
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CMD_resetweapons]: " + e.ToString());
            }
            return;
        }

        [Command("kick", "Befehl: /kick [Spieler/ID] [Grund]")]
        public void CMD_kick(Player player, string target, string text)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Player ntarget = Helper.GetPlayerByNameOrID(target);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Moderator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                if (ntarget == null || !Account.IsPlayerLoggedIn(ntarget) || player == ntarget)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Spieler!", "error", "top-end");
                    return;
                }
                if (text.Length < 3 || text.Length > 35)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Grund!", "error", "top-end");
                    return;
                }
                Account account = Helper.GetAccountData(player);
                Account account2 = Helper.GetAccountData(ntarget);
                if (account2 == null)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Spieler!", "error");
                    return;
                }
                if (account.adminlevel < account2.adminlevel || account2.name == "Nemesus")
                {
                    Helper.SendNotificationWithoutButton(player, "Du kannst diesen Spieler nicht kicken!", "error");
                    return;
                }
                Helper.SendNotificationWithoutButton(player, $"Du hast {account2.name} vom Server gekickt, Grund: {text}!", "success", "top-end", 5500);
                AntiCheatController.SetKick(ntarget, text, account.name, true);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CMD_kick]: " + e.ToString());
            }
            return;
        }

        [Command("freeze", "Befehl: /un/freeze [Spieler/ID]", Alias = "unfreeze")]
        public void CMD_freeze(Player player, string target)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Player ntarget = Helper.GetPlayerByNameOrID(target);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.ProbeModerator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                if (ntarget == null || !Account.IsPlayerLoggedIn(ntarget))
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Spieler!", "error", "top-end");
                    return;
                }
                Account account = Helper.GetAccountData(player);
                Account account2 = Helper.GetAccountData(ntarget);
                TempData tempData = Helper.GetCharacterTempData(ntarget);
                if (account.adminlevel < account2.adminlevel || account2.name == "Nemesus")
                {
                    Helper.SendNotificationWithoutButton(player, "Du kannst diesen Spieler nicht un/freezen!", "error");
                    return;
                }
                string freezestring = (tempData.freezed == false) ? "eingefroren" : "entfroren";
                Helper.SendNotificationWithoutButton(player, $"Du hast {account2.name} {freezestring}!", "success", "top-end", 5500);
                Helper.SendNotificationWithoutButton(ntarget, $"Du wurdest von {account.name} {freezestring}!", "success", "top-end", 5500);
                Helper.CreateAdminLog("adminlog", $"{account.name} hat {account2.name} {freezestring}!");
                Helper.SendAdminMessage2($"{account.name} hat {account2.name} {freezestring}!", 1, false);
                tempData.freezed = !tempData.freezed;
                if (tempData.freezed == true)
                {
                    NAPI.Player.SetPlayerCurrentWeapon(ntarget, WeaponHash.Unarmed);
                }
                ntarget.TriggerEvent("Client:BlockPlayer", tempData.freezed);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CMD_freeze]: " + e.ToString());
            }
            return;
        }

        [Command("reports", "Befehl: /reports")]
        public void CMD_reports(Player player)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.ProbeModerator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                if (Helper.reportList.Count <= 0)
                {
                    Helper.SendNotificationWithoutButton(player, "Keine Meldungen vorhanden!", "error", "top-end");
                    return;
                }
                List<CenterMenu> centerMenu = new List<CenterMenu>();
                foreach (Reports report in Helper.reportList)
                {
                    CenterMenu cMenu = new CenterMenu();
                    cMenu.var2 = report.text;
                    cMenu.var3 = report.timestamp;
                    centerMenu.Add(cMenu);
                }
                String rules = "ID,Meldung,Zeitpunkt";
                NAPI.Player.SetPlayerCurrentWeapon(player, WeaponHash.Unarmed);
                player.TriggerEvent("Client:ShowCenterMenu", rules, NAPI.Util.ToJson(centerMenu), "Meldungen");
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CMD_reports]: " + e.ToString());
            }
            return;
        }

        [Command("setweather", "Befehl: /setweather [Wetterart] [Dauer in h]")]
        public void CMD_setweather(Player player, string wetter = "n/A", int hour = 1)
        {
            try
            {
                Account account = Helper.GetAccountData(player);
                if (!Account.IsPlayerLoggedIn(player)) return;
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Administrator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                if (hour < 1 || hour > 24)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültige Dauer!", "error", "top-end");
                    return;
                }
                switch (wetter.ToLower())
                {
                    case "sonnig":
                        {
                            Helper.weatherstring = "CLEAR";
                            NAPI.World.SetWeather("CLEAR");
                            break;
                        }
                    case "bewölkt":
                        {
                            Helper.weatherstring = "CLOUDS";
                            NAPI.World.SetWeather("CLOUDS");
                            break;
                        }
                    case "bewölkt2":
                        {
                            Helper.weatherstring = "OVERCAST";
                            NAPI.World.SetWeather("OVERCAST");
                            break;
                        }
                    case "regen":
                        {
                            Helper.weatherstring = "RAIN";
                            NAPI.World.SetWeather("RAIN");
                            break;
                        }
                    case "gewitter":
                        {
                            Helper.weatherstring = "THUNDERSTORM";
                            NAPI.World.SetWeather("THUNDERSTORM");
                            break;
                        }
                    case "schnee":
                        {
                            Helper.weatherstring = "SNOW";
                            NAPI.World.SetWeather("SNOW");
                            break;
                        }
                    case "Nebel":
                        {
                            Helper.weatherstring = "FOGGY";
                            NAPI.World.SetWeather("FOGGY");
                            break;
                        }
                    case "weihnachten":
                        {
                            Helper.weatherstring = "XMAS";
                            NAPI.World.SetWeather("XMAS");
                            break;
                        }
                    case "normal":
                        {
                            Helper.weatherTimestamp = 0;
                            Helper.SetAndGetWeather("https://nemesus-world.de/WetterInfo.php");
                            break;
                        }
                    default:
                        {
                            player.SendChatMessage("~w~Befehl: /setweather [Wetterart] [Dauer in h]");
                            player.SendChatMessage("~y~Verfügbare Wetterarten: Sonnig, Bewölkt, Bewölkt2, Regen, Gewitter, Schnee, Nebel, Weihnachten, Normal");
                            return;
                        }
                }
                wetter = wetter[0].ToString().ToUpper() + wetter.Substring(1);
                if (wetter == "Normal")
                {
                    hour = 24;
                }
                Helper.SendNotificationWithoutButton(player, $"Du hast das Wetter für {hour}h auf {wetter} gesetzt!", "success", "top-end", 5500);
                Helper.CreateAdminLog("adminlog", $"{account.name} hat das Wetter für {hour}h auf {wetter} gesetzt!");
                Helper.SendAdminMessage3($"{account.name} hat das Wetter für {hour}h auf {wetter} gesetzt!");
                Helper.SendAdminMessageToAll($"{account.name} hat das Wetter für {hour}h auf {wetter} gesetzt!", false);
                if (wetter != "Normal")
                {
                    Helper.weatherTimestamp = Helper.UnixTimestamp() + (60 * 60 * hour);
                }
                else
                {
                    Helper.weatherTimestamp = 0;
                }
            }
            catch (Exception)
            {
                player.SendChatMessage("~w~Befehl: /setweather [Wetterart] [Dauer in h]");
                player.SendChatMessage("~y~Verfügbare Wetterarten: Sonnig, Bewölkt, Bewölkt2, Regen, Gewitter, Schnee, Nebel, Weihnachten, Normal");
            }
            return;
        }

        [Command("teleport", "Befehl: /teleport [Position] [Spieler/ID]", GreedyArg = true, Alias = "tele")]
        public void CMD_teleport(Player player, string option = "n/A", string target = "n/A")
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Account account = Helper.GetAccountData(player);
                if (player.GetOwnSharedData<bool>("Player:Testmodus") == false && !Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Moderator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                Player ntarget = null;
                if (target == "n/A")
                {
                    ntarget = player;
                }
                else
                {
                    ntarget = Helper.GetPlayerByAccountName(target);
                    ntarget = Helper.GetPlayerByAccountName(target);
                }
                if (ntarget == null || !Account.IsPlayerLoggedIn(ntarget))
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Spieler!", "error", "top-end");
                    return;
                }
                Character character = Helper.GetCharacterData(ntarget);
                Account account2 = Helper.GetAccountData(ntarget);
                if (account.adminlevel < account2.adminlevel)
                {
                    Helper.SendNotificationWithoutButton(player, "Du kannst diesen Spieler nicht teleportieren!", "error");
                    return;
                }
                ntarget.Dimension = 0;
                switch (option.ToLower())
                {
                    case "rathaus":
                        {
                            Helper.SetPlayerPosition(ntarget, new Vector3(-544.19556, -214.8591, 37.64877));
                            ntarget.Heading = -4.6634617f;
                            break;
                        }
                    case "stadthalle":
                        {
                            Helper.SetPlayerPosition(ntarget, new Vector3(-544.19556, -214.8591, 37.64877));
                            ntarget.Heading = -4.6634617f;
                            option = "rathaus";
                            break;
                        }
                    case "spediteur":
                        {
                            Helper.SetPlayerPosition(ntarget, new Vector3(-1099.1411, -2029.1793, 13.201088));
                            ntarget.Heading = -175.33612f;
                            break;
                        }
                    case "jäger":
                        {
                            Helper.SetPlayerPosition(ntarget, new Vector3(1094.9083, -260.15283, 69.28121));
                            ntarget.Heading = -179.02597f;
                            break;
                        }
                    case "ammunation":
                        {
                            Helper.SetPlayerPosition(ntarget, new Vector3(17.69249, -1122.0366, 28.906603));
                            ntarget.Heading = -16.626823f;
                            break;
                        }
                    case "bank":
                        {
                            Helper.SetPlayerPosition(ntarget, new Vector3(228.84166, 207.4824, 105.477104));
                            ntarget.Heading = -15.4522505f;
                            break;
                        }
                    case "24/7-tankstelle":
                        {
                            Helper.SetPlayerPosition(ntarget, new Vector3(-52.115276, -1760.553, 29.136871));
                            ntarget.Heading = -2.2742207f;
                            break;
                        }
                    case "kleidungsladen":
                        {
                            Helper.SetPlayerPosition(ntarget, new Vector3(-1210.859, -778.86163, 17.281788));
                            ntarget.Heading = -123.26572f;
                            break;
                        }
                    case "autohaus":
                        {
                            Helper.SetPlayerPosition(ntarget, new Vector3(-52.297993, -1678.9119, 29.403315));
                            ntarget.Heading = 41.132217f;
                            break;
                        }
                    case "garage":
                        {
                            Helper.SetPlayerPosition(ntarget, new Vector3(201.45505, -810.3924, 31.009977));
                            ntarget.Heading = -78.35156f;
                            break;
                        }
                    case "jagtgebiet":
                        {
                            Helper.SetPlayerPosition(ntarget, new Vector3(1714.7175, -571.90814, 144.50644));
                            ntarget.Heading = -134.65039f;
                            break;
                        }
                    case "angelmeister":
                        {
                            Helper.SetPlayerPosition(ntarget, new Vector3(-2187.0142, -409.1438, 13.11738));
                            ntarget.Heading = 133.71156f;
                            break;
                        }
                    case "barber-shop":
                        {
                            Helper.SetPlayerPosition(ntarget, new Vector3(134.00227, -1716.0375, 29.267176));
                            ntarget.Heading = 2.5901852f;
                            break;
                        }
                    case "tattoo-laden":
                        {
                            Helper.SetPlayerPosition(ntarget, new Vector3(1316.6708, -1648.952, 52.145542));
                            ntarget.Heading = -103.38408f;
                            break;
                        }
                    case "juwelier":
                        {
                            Helper.SetPlayerPosition(ntarget, new Vector3(-635.2666, -235.78891, 37.9689337));
                            ntarget.Heading = -121.145775f;
                            break;
                        }
                    case "busdepot":
                        {
                            Helper.SetPlayerPosition(ntarget, new Vector3(433.1037, -640.70184, 28.727196));
                            ntarget.Heading = -167.18866f;
                            break;
                        }
                    case "mülldeponie":
                        {
                            Helper.SetPlayerPosition(ntarget, new Vector3(-342.78073, -1570.6143, 25.227007));
                            ntarget.Heading = 77.85392f;
                            break;
                        }
                    case "taxizentrale":
                        {
                            Helper.SetPlayerPosition(ntarget, new Vector3(903.04895, -175.56384, 74.06179));
                            ntarget.Heading = 109.888f;
                            break;
                        }
                    case "kanalreiniger":
                        {
                            Helper.SetPlayerPosition(ntarget, new Vector3(410.27667, -2075.62, 21.203129));
                            ntarget.Heading = -70.62809f;
                            break;
                        }
                    case "landwirt":
                        {
                            Helper.SetPlayerPosition(ntarget, new Vector3(2237.463, 5154.361, 57.19951));
                            ntarget.Heading = -99.327545f;
                            break;
                        }
                    case "geldlieferant":
                        {
                            Helper.SetPlayerPosition(ntarget, new Vector3(-1406.3676, -456.03113, 34.48318));
                            ntarget.Heading = -43.017254f;
                            break;
                        }
                    case "strand":
                        {
                            Helper.SetPlayerPosition(ntarget, new Vector3(-1391.1207, -1431.2648, 3.930646));
                            ntarget.Heading = -77.09678f;
                            break;
                        }
                    case "fahrschule":
                        {
                            Helper.SetPlayerPosition(ntarget, new Vector3(-713.5807, -1292.5724, 5.101825));
                            ntarget.Heading = 170.18605f;
                            break;
                        }
                    case "maskenhändler":
                        {
                            Helper.SetPlayerPosition(ntarget, new Vector3(-1580.4333, -943.56573, 13.017395));
                            ntarget.Heading = 167.67076f;
                            break;
                        }
                    case "lspd":
                        {
                            Helper.SetPlayerPosition(ntarget, new Vector3(644.2941, 8.478147, 82.786674));
                            ntarget.Heading = 137.34984f;
                            break;
                        }
                    case "lspd2":
                        {
                            Helper.SetPlayerPosition(ntarget, new Vector3(427.79004, -974.62396, 30.71008));
                            ntarget.Heading = -143.45676f;
                            break;
                        }
                    case "lsrc":
                        {
                            Helper.SetPlayerPosition(ntarget, new Vector3(-666.33936, 305.3127, 83.084114));
                            ntarget.Heading = 41.820602f;
                            break;
                        }
                    case "acls":
                        {
                            Helper.SetPlayerPosition(ntarget, new Vector3(-360.8035, -147.89775, 38.249393));
                            ntarget.Heading = 165.48781f;
                            break;
                        }
                    case "verwahrplatz":
                        {
                            Helper.SetPlayerPosition(ntarget, new Vector3(408.72903, -1627.8516, 29.29195));
                            ntarget.Heading = -7.333437f;
                            break;
                        }
                    case "schatzsucher":
                        {
                            Helper.SetPlayerPosition(ntarget, new Vector3(1440.3531, 3745.343, 32.239727));
                            ntarget.Heading = -7.333437f;
                            break;
                        }
                    default:
                        {
                            player.SendChatMessage("~w~Befehl: /teleport [Position]");
                            player.SendChatMessage("~y~Verfügbare Positionen: Rathaus, Spediteur, Jäger, Ammunation, Bank, 24/7-Tankstelle, Kleidungsladen, Autohaus, Garage, Jagtgebiet, Angelmeister, Barber-Shop, Tattoo-Laden, Juwelier, Busdepot, Mülldeponie, Taxizentrale, Kanalreiniger, Landwirt, Geldlieferant, Strand, Fahrschule, Maskenhändler, LSPD, LSPD2, LSRC, ACLS, Verwahrplatz, Schatzsucher");
                            return;
                        }
                }
                if (character.inhouse > -1)
                {
                    character.inhouse = -1;
                    ntarget.SetOwnSharedData("Player:InHouse", -1);
                }
                option = option[0].ToString().ToUpper() + option.Substring(1);
                Helper.SendNotificationWithoutButton(ntarget, $"Du wurdest von {account.name} zum {option} teleportiert!", "success", "top-end", 3500);
                Helper.SendNotificationWithoutButton(player, $"Du hast {account2.name} zum {option} teleportiert!", "success", "top-end", 3500);
                Helper.CreateAdminLog("teleportlog", $"{account.name} hat {account2.name} zum {option} teleportiert!");
            }
            catch (Exception)
            {
                player.SendChatMessage("~w~Befehl: /teleport [Position]");
                player.SendChatMessage("~y~Verfügbare Positionen: Rathaus, Spediteur, Jäger, Ammunation, Bank, 24/7 & Tankstelle, Kleidungsladen, Autohaus, Garage, Jagtgebiet, Angelmeister, Barber-Shop, Tattoo-Laden, Juwelier, Busdepot, Mülldeponie, Taxizentrale, Kanalreiniger, Landwirt, Geldlieferant, Strand, Fahrschule, Maskenhändler, LSPD, LSPD2, LSRC, ACLS, Verwahrplatz, Schatzsucher");
            }
            return;
        }

        [Command("createhouseinterior", "Befehl: /createhouseinterior [IPL] [Klasse (-1 = Nur administrativ setzbar) (Spielerid+100 = Privates Interior)]")]
        public void CMD_createhouseinterior(Player player, string ipl, int classify = 0)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Account account = Helper.GetAccountData(player);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Administrator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                if (classify < -1)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültige Klasse!", "error", "top-end");
                    return;
                }
                if (ipl.Length < 5)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültige IPL!", "error", "top-end");
                    return;
                }
                MySqlCommand command = General.Connection.CreateCommand();
                command.CommandText = "INSERT INTO houseinteriors (ipl, posx, posy, posz, classify) VALUES (@ipl, @posx, @posy, @posz, @classify)";
                command.Parameters.AddWithValue("@ipl", ipl.ToLower());
                command.Parameters.AddWithValue("@posx", player.Position.X);
                command.Parameters.AddWithValue("@posY", player.Position.Y);
                command.Parameters.AddWithValue("@posZ", player.Position.Z);
                command.Parameters.AddWithValue("@classify", classify);
                command.ExecuteNonQuery();
                int id = (int)command.LastInsertedId;

                HouseInteriorModel houseinteriorModelTemp = new HouseInteriorModel();
                houseinteriorModelTemp.id = id;
                houseinteriorModelTemp.ipl = ipl;
                houseinteriorModelTemp.posx = player.Position.X;
                houseinteriorModelTemp.posy = player.Position.Y;
                houseinteriorModelTemp.posz = player.Position.Z;
                houseinteriorModelTemp.classify = classify;

                House.houseListInteriors.Add(houseinteriorModelTemp);

                Helper.SendNotificationWithoutButton(player, "Das neue Hausinterior wurde erstellt und kann benutzt werden!", "success", "top-end");
                Helper.CreateAdminLog($"hauslog", account.name + $" hat ein neues Hausinterior erstellt - (Interior: {id})!");
            }
            catch (Exception e)
            {
                Helper.SendNotificationWithoutButton(player, "Ungültige Option!", "error", "top-end");
                Helper.ConsoleLog("error", $"[CMD_createhouseinterior]: " + e.ToString());
            }
        }

        [Command("createhousedoor", "Befehl: /createhousedoor [Hash] [Haus-ID]", Alias = "chd")]
        public void CMD_createhousedoor(Player player, string hash, int house)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Account account = Helper.GetAccountData(player);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Administrator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }

                if (house <= 0 || house > Business.businessList.Count)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültige Haus-ID!", "error", "top-end");
                    return;
                }

                Doors checkDoor = DoorsController.GetClosestDoor(player, 0.45f);
                if (checkDoor != null)
                {
                    Helper.SendNotificationWithoutButton(player, "Hier befindet sich schon eine Haus-Tür in der Nähe!", "error", "top-end");
                    return;
                }

                Doors door = new Doors();
                door.id = DoorsController.GetFreeDoorsID();
                door.hash = hash;
                door.posx = player.Position.X;
                door.posy = player.Position.Y;
                door.posz = player.Position.Z;
                door.dimension = 0;
                door.toogle = true;
                door.props = "house-" + house;
                door.save = false;

                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                db.Insert(door);

                DoorsController.AddDoor(door, true);

                Helper.SendNotificationWithoutButton(player, "Haus-Tür wurde erstellt!", "success", "top-end");
            }
            catch (Exception)
            {
                Helper.SendNotificationWithoutButton(player, "Haus-Tür konnte nicht erstellt werden!", "error", "top-end");
            }
        }

        [Command("createtaxiposition", "Befehl: /createtaxiposition")]
        public void CMD_createtaxiposition(Player player)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Account account = Helper.GetAccountData(player);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Administrator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                if (player.Vehicle != null)
                {
                    Helper.SendNotificationWithoutButton(player, "Du kannst diesen Befehl nur zu Fuß benutzen!", "error", "top-end");
                    return;
                }
                player.TriggerEvent("Client:CreateTaxiPosition");
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[createtaxiposition]: " + e.ToString());
            }
        }

        [Command("creategarbageroute", "Befehl: /creategarbageroute [Routenname]", GreedyArg = true)]
        public void CMD_creategarbageroute(Player player, string name)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Account account = Helper.GetAccountData(player);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Administrator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                if (name.Length < 3 || name.Length > 64)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Routenname!", "error", "top-end");
                    return;
                }
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                GarbageRoutes gbRoute = null;
                foreach (GarbageRoutes garbageRoutes in Helper.garbageRoutesList)
                {
                    if (garbageRoutes.name.ToLower() == name.ToLower())
                    {
                        gbRoute = garbageRoutes;
                        break;
                    }
                }

                if (gbRoute == null)
                {
                    name = name[0].ToString().ToUpper() + name.Substring(1);
                    gbRoute = new GarbageRoutes();
                    gbRoute.name = name;
                    gbRoute.id = Helper.garbageRoutesList.Count + 1;
                    gbRoute.routes = gbRoute.routes + $"{player.Position.X.ToString(new CultureInfo("en-US"))},{player.Position.Y.ToString(new CultureInfo("en-US"))},{player.Position.Z.ToString(new CultureInfo("en-US"))}|";
                    db.Insert(gbRoute);
                    Helper.garbageRoutesList.Add(gbRoute);
                }
                else
                {
                    gbRoute.routes = gbRoute.routes + $"{player.Position.X.ToString(new CultureInfo("en-US"))},{player.Position.Y.ToString(new CultureInfo("en-US"))},{player.Position.Z.ToString(new CultureInfo("en-US"))}|";
                    db.Save(gbRoute);
                }
                if (name.ToLower() != "garbage")
                {
                    Helper.SendNotificationWithoutButton(player, $"Position zur Müllroute {name} hinzugefügt!", "success", "top-end");
                }
                else
                {
                    if (player.IsInVehicle)
                    {
                        Helper.SendNotificationWithoutButton(player, $"Du darfst in keinem Fahrzeug sitzen!", "error", "top-end");
                        return;
                    }
                    Helper.SendNotificationWithoutButton(player, $"Müllposition auf dem Boden erfolgreich erstellt!", "success", "top-end");
                    if (Helper.GetRandomPercentage(35))
                    {
                        NAPI.Object.CreateObject(NAPI.Util.GetHashKey("prop_rub_litter_03b"), new Vector3(player.Position.X, player.Position.Y, player.Position.Z - 0.352), new Vector3(0.0f, 0.0f, 0.0f), 255, 0);
                    }
                    else
                    {
                        NAPI.Object.CreateObject(NAPI.Util.GetHashKey("prop_rub_litter_03c"), new Vector3(player.Position.X, player.Position.Y, player.Position.Z - 0.375), new Vector3(0.0f, 0.0f, 0.0f), 255, 0);
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CMD_creategarbageroute]: " + e.ToString());
            }
        }

        [Command("createbusroute", "Befehl: /createbusroute [Skill] [Routenname]", GreedyArg = true)]
        public void CMD_createbusroute(Player player, int skill, string name)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Account account = Helper.GetAccountData(player);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Administrator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                if (name.Length < 3 || name.Length > 64)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Routenname!", "error", "top-end");
                    return;
                }
                if (skill < 1 || skill > 5)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Skill!", "error", "top-end");
                    return;
                }
                if (!player.HasData("Player:BusRoutes"))
                {
                    foreach (BusRoutes busRoutes1 in Helper.busRoutesList)
                    {
                        if (busRoutes1.name.ToLower() == name.ToLower())
                        {
                            Helper.SendNotificationWithoutButton(player, "Dieser Routenname existiert bereits!", "error", "top-end");
                            return;
                        }
                    }
                    name = name[0].ToString().ToUpper() + name.Substring(1);
                    BusRoutes busRoutes = new BusRoutes();
                    busRoutes.name = name;
                    busRoutes.skill = skill;
                    busRoutes.id = Helper.busRoutesList.Count + 1;
                    player.SetData<BusRoutes>("Player:BusRoutes", busRoutes);
                    Helper.SendNotificationWithoutButton(player, "Neue Busroute erstellt, benutze /createbusstation um eine neue Haltestelle hinzuzufügen!", "success", "top-end");
                }
                else
                {
                    BusRoutes busRoutesNew = player.GetData<BusRoutes>("Player:BusRoutes");
                    if (busRoutesNew.routes.Length <= 0 || busRoutesNew.advert.Length <= 0)
                    {
                        Helper.SendNotificationWithoutButton(player, "Du musst zuerst Busstationen erstellen!", "error", "top-end");
                        return;
                    }
                    busRoutesNew.routes = busRoutesNew.routes.Substring(0, busRoutesNew.routes.Length - 1);
                    busRoutesNew.advert = busRoutesNew.advert.Substring(0, busRoutesNew.advert.Length - 1);
                    Helper.busRoutesList.Add(busRoutesNew);
                    PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                    db.Insert(player.GetData<BusRoutes>("Player:BusRoutes"));
                    Helper.SendNotificationWithoutButton(player, "Busrouten Bearbeitung abgeschlossen!", "success", "top-end");
                    player.ResetData("Player:BusRoutes");
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CMD_createbusroute]: " + e.ToString());
            }
        }

        [Command("createbusstation", "Befehl: /createbusstation [Art=(CP|Anzeige)] [Stationsname]", GreedyArg = true)]
        public void CMD_createbusstation(Player player, string art, String name)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Account account = Helper.GetAccountData(player);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Administrator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                if (player.HasData("Player:BusRoutes"))
                {
                    if (name.Length < 3 || name.Length > 35)
                    {
                        Helper.SendNotificationWithoutButton(player, "Ungültiger Stationsname!", "error", "top-end");
                        return;
                    }
                    name = name[0].ToString().ToUpper() + name.Substring(1);
                    BusRoutes busRoutes = player.GetData<BusRoutes>("Player:BusRoutes");
                    if (art.ToLower() == "cp")
                    {
                        if (!player.IsInVehicle)
                        {
                            Helper.SendNotificationWithoutButton(player, "Du sitzt in keinem Fahrzeug!", "error", "top-end");
                            return;
                        }
                        busRoutes.routes = busRoutes.routes + $"{player.Position.X.ToString(new CultureInfo("en-US"))},{player.Position.Y.ToString(new CultureInfo("en-US"))},{player.Position.Z.ToString(new CultureInfo("en-US"))},{name}|";
                        Helper.SendNotificationWithoutButton(player, "Haltestelle (Checkpoint) hinzugefügt!", "success", "top-end");
                    }
                    else
                    {
                        if (player.IsInVehicle)
                        {
                            Helper.SendNotificationWithoutButton(player, "Du darfst in keinem Fahrzeug sitzen!", "error", "top-end");
                            return;
                        }
                        busRoutes.advert = busRoutes.advert + $"{player.Position.X.ToString(new CultureInfo("en-US"))},{player.Position.Y.ToString(new CultureInfo("en-US"))},{player.Position.Z.ToString(new CultureInfo("en-US"))},{name}|";
                        Helper.SendNotificationWithoutButton(player, "Haltestelle (Anzeige) hinzugefügt!", "success", "top-end");
                    }
                }
                else
                {
                    Helper.SendNotificationWithoutButton(player, "Du musst zuerst die Busrouten Bearbeitung starten - /createbusroute!", "success", "top-end");
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[createbusstation]: " + e.ToString());
            }
        }

        [Command("createfire", "Befehl: /createfire [Ort]", GreedyArg = true)]
        public void CMD_createfire(Player player, String name)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Account account = Helper.GetAccountData(player);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.HighAdministrator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }

                if (name.Length < 3 || name.Length > 35)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Ort!", "error", "top-end");
                    return;
                }
                name = name[0].ToString().ToUpper() + name.Substring(1);
                MySqlCommand command;
                command = General.Connection.CreateCommand();
                command.CommandText = "INSERT INTO fire (name,posx,posy,posz,posa) VALUES (@name,@posx,@posy,@posz,@posa)";
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@posx", player.Position.X);
                command.Parameters.AddWithValue("@posy", player.Position.Y);
                command.Parameters.AddWithValue("@posz", player.Position.Z);
                if (name.ToLower().Contains("baum"))
                {
                    command.Parameters.AddWithValue("@posa", player.Heading);
                }
                else
                {
                    command.Parameters.AddWithValue("@posa", 0.0f);
                }
                command.ExecuteNonQuery();
                Helper.SendNotificationWithoutButton(player, "Neues Feuer wurde erstellt!", "success", "top-end");
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CMD_createfire]: " + e.ToString());
            }
        }

        [Command("createfurniture", "Befehl: /createfurniture [Kategorie] [Name] [Hash] [Preis] [Extra]")]
        public void CMD_createfurniture(Player player, string kategorie, string name, string hash, int preis, int extra)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Account account = Helper.GetAccountData(player);
                int categorie = -1, furnitureid = -1;
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Administrator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                if (preis < 0 || preis > 500000)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Preis!", "error", "top-end");
                    return;
                }
                if (kategorie.Length < 3 || kategorie.Length > 64)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültige Kategorie!", "error", "top-end");
                    return;
                }
                if (name.Length < 3 || name.Length > 64)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Name!", "error", "top-end");
                    return;
                }
                if (hash.Length < 3 || hash.Length > 64)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Hash!", "error", "top-end");
                    return;
                }
                if (extra < 0 || extra > 25)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiges Extra!", "error", "top-end");
                    return;
                }
                if (kategorie.Contains("_"))
                {
                    kategorie = kategorie.Replace('_', ' ');
                }
                foreach (FurnitureModel furnitureModel in House.furnitureModelList)
                {
                    if (furnitureModel != null)
                    {
                        if (furnitureModel.hash.ToLower() == hash.ToLower())
                        {
                            Helper.SendNotificationWithoutButton(player, "Dieses Möbelstück existiert bereits!", "error", "top-end");
                            return;
                        }
                        if (furnitureModel.name.ToLower() == name.ToLower())
                        {
                            Helper.SendNotificationWithoutButton(player, "Dieser Möbelname wird schon benutzt!", "error", "top-end");
                            return;
                        }
                    }
                }

                foreach (FurnitureModelCategories furnitureModelCategorie in House.furnitureModelCategorieList)
                {
                    if (furnitureModelCategorie != null)
                    {
                        if (furnitureModelCategorie.name.ToLower() == kategorie.ToLower())
                        {
                            categorie = furnitureModelCategorie.id;
                        }
                    }
                }

                MySqlCommand command;
                kategorie = kategorie[0].ToString().ToUpper() + kategorie.Substring(1);
                name = name[0].ToString().ToUpper() + name.Substring(1);

                if (categorie == -1)
                {
                    command = General.Connection.CreateCommand();
                    command.CommandText = "INSERT INTO furniturecategories (name) VALUES (@name)";
                    command.Parameters.AddWithValue("@name", kategorie);
                    command.ExecuteNonQuery();
                    categorie = (int)command.LastInsertedId;

                    FurnitureModelCategories furnitureModelCategorie = new FurnitureModelCategories();
                    furnitureModelCategorie.id = categorie;
                    furnitureModelCategorie.name = kategorie;
                    House.furnitureModelCategorieList.Add(furnitureModelCategorie);
                }

                FurnitureModel furnitureModelNew = new FurnitureModel();

                command = General.Connection.CreateCommand();
                command.CommandText = "INSERT INTO furniture (hash, name, categorie, extra, price) VALUES (@hash, @name, @categorie, @extra, @price)";
                command.Parameters.AddWithValue("@hash", hash);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@categorie", categorie);
                command.Parameters.AddWithValue("@extra", extra);
                command.Parameters.AddWithValue("@price", preis);
                command.ExecuteNonQuery();
                furnitureid = (int)command.LastInsertedId;

                furnitureModelNew.id = furnitureid;
                furnitureModelNew.hash = hash;
                furnitureModelNew.name = name;
                furnitureModelNew.categorie = categorie;
                furnitureModelNew.extra = extra;
                furnitureModelNew.price = preis;

                House.furnitureModelList.Add(furnitureModelNew);

                Helper.SendNotificationWithoutButton(player, "Das neue Möbelstück wurde erstellt!", "success", "top-end");
                Helper.CreateAdminLog($"hauslog", account.name + $" hat das neue Möbelstück {name} für {preis}$ erstellt!");
            }
            catch (Exception)
            {
                Helper.SendNotificationWithoutButton(player, "Das Möbelstück konnte nicht erstellt werden!", "error", "top-end");
            }
        }

        [Command("furniturecheck", "Befehl: /furniturecheck")]
        public void cmd_furniturecheck(Player player)
        {
            if (!Account.IsPlayerLoggedIn(player)) return;
            Account account = Helper.GetAccountData(player);
            if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Administrator))
            {
                Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                return;
            }
            House house = House.GetClosestHouse(player);
            if (house == null)
            {
                Helper.SendNotificationWithoutButton(player, "Du bist nicht in der Nähe von einem Haus!", "error", "center");
                return;
            }
            FurnitureSetHouse furniture = Furniture.GetClosestFurniture(player, house.id, 2.65f);
            if (furniture == null)
            {
                Helper.SendNotificationWithoutButton(player, "Keine Möbel gefunden!", "error", "center");
                return;
            }
            Helper.SendChatMessage(player, $"~b~[Möbelcheck]: Hausnummer: {house.id}, Besitzer: {house.owner}, Möbel-ID: {furniture.id}, Möbelname: {furniture.name}, Hash: {furniture.hash}, Preis: {furniture.price}$");
            return;
        }

        [Command("deletefurniture", "Befehl: /deletefurniture [0 = Abbauen | 1 = Löschen]")]
        public void cmd_deletefurniture(Player player, int aktion = 0)
        {
            if (!Account.IsPlayerLoggedIn(player)) return;
            Account account = Helper.GetAccountData(player);
            if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Administrator))
            {
                Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                return;
            }
            House house = House.GetClosestHouse(player);
            if (house == null)
            {
                Helper.SendNotificationWithoutButton(player, "Du bist nicht in der Nähe von einem Haus!", "error", "center");
                return;
            }
            FurnitureSetHouse furniture = Furniture.GetClosestFurniture(player, house.id, 2.65f);
            if (furniture == null)
            {
                Helper.SendNotificationWithoutButton(player, "Keine Möbel gefunden!", "error", "center");
                return;
            }
            if (aktion == 0)
            {
                furniture.position = "0.0|0.0|0.0|0.0|0.0|0.0|0";
                if (furniture.objectHandle != null)
                {
                    furniture.objectHandle.Delete();
                    furniture.objectHandle = null;
                }
                Helper.SendNotificationWithoutButton(player, "Möbelstück administrativ abgebaut!", "success", "top-left");
                House.SaveFurniture(furniture);
                Helper.CreateAdminLog($"hauslog", account.name + $" hat das Möbelstück {furniture.name}[{furniture.id}] des Hauses {house.id} abgebaut!");
            }
            else
            {
                furniture.position = "0.0|0.0|0.0|0.0|0.0|0.0|0";
                if (furniture.objectHandle != null)
                {
                    furniture.objectHandle.Delete();
                    furniture.objectHandle = null;
                }
                Helper.SendNotificationWithoutButton(player, "Möbelstück administrativ gelöscht!", "success", "top-left");
                House.DeleteFurniture(furniture);
                Helper.CreateAdminLog($"hauslog", account.name + $" hat das Möbelstück {furniture.name}[{furniture.id}] des Hauses {house.id} gelöscht!");
            }
            return;
        }

        [Command("asellhouse", "Befehl: /asellhouse")]
        public void CMD_asellhouse(Player player)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Account account = Helper.GetAccountData(player);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Administrator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                House house = House.GetClosestHouse(player);
                if (house == null)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiges Haus!", "error", "top-left");
                    return;
                }
                if (house.owner == "n/A")
                {
                    Helper.SendNotificationWithoutButton(player, "Dieses Haus kann nicht verkauft werden!", "error", "top-left");
                    return;
                }
                bool found = false;
                foreach (Player p in NAPI.Pools.GetAllPlayers())
                {
                    if (p != null && p.Dimension > 0 && p.GetOwnSharedData<bool>("Player:Spawned") == true)
                    {
                        Character character = Helper.GetCharacterData(p);
                        if (character != null && character.inhouse == house.id)
                        {
                            found = true;
                            break;
                        }
                    }
                }
                if (found == true)
                {
                    Helper.SendNotificationWithoutButton(player, "Es befinden sich noch Spieler im Haus!", "error", "top-left");
                    return;
                }
                if (Cars.IsCarInGarage("house-" + house.id))
                {
                    Helper.SendNotificationWithoutButton(player, "Es befinden sich noch Fahrzeuge in der Garage!", "error", "top-left");
                    return;
                }
                house.status = 0;
                house.owner = "n/A";
                house.locked = 0;
                house.tenants = 0;
                house.rental = 0;
                house.noshield = 0;
                house.blip = 40;
                house.housegroup = -1;
                house.stock = 0;
                house.stockprice = 30;
                if (House.GetInteriorClassify(house.interior) >= 4)
                {
                    house.interior = House.GetRandomHouseInterior(house.classify);
                }
                House.KickAllTenants(house.id, true);
                House.SetHouseHandle(house);
                House.NewHouseKey(house.id, "n/A");
                int money = Furniture.SellAllFurniture(house.id);
                House.SaveHouse(house);
                Helper.CreateAdminLog($"hauslog", account.name + $" hat das Haus {house.id} administrativ verkauft!");
                Helper.SendNotificationWithoutButton(player, "Du hast das Haus administrativ verkauft!", "success", "top-left", 3500);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CMD_asellhouse]: " + e.ToString());
            }
        }

        [Command("housecheck", "Befehl: /housecheck")]
        public void cmd_housecheck(Player player)
        {
            if (!Account.IsPlayerLoggedIn(player)) return;
            Account account = Helper.GetAccountData(player);
            if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Administrator))
            {
                Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                return;
            }
            House house = House.GetClosestHouse(player);
            if (house == null)
            {
                Helper.SendNotificationWithoutButton(player, "Ungültiges Haus!", "error", "center");
                return;
            }
            Helper.SendChatMessage(player, $"~b~[Hauscheck]: Hausnummer: {house.id}, Besitzer: {house.owner}, Preis: {house.price}$, Interior: {house.interior}, Möbel: {Furniture.CountFurniture(house.id)}, Mieter: {house.tenants}, Mietpreis: {house.rental}$");
            return;
        }

        [Command("edithouseinterior", "Befehl: /edithouseinterior [Interior] [IPL] [Klasse (-1 = Nur administrativ setzbar) (Spielerid+100 = Privates Interior)]")]
        public void CMD_edithouseinterior(Player player, int interior, string ipl, int classify = 0)
        {
            try
            {
                int id = 0;
                if (!Account.IsPlayerLoggedIn(player)) return;
                Account account = Helper.GetAccountData(player);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Administrator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                if (interior < 0 || interior > House.houseListInteriors.Count)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiges Interior!", "error", "top-end");
                    return;
                }
                if (classify < -1)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültige Klasse!", "error", "top-end");
                    return;
                }
                if (ipl.Length < 5)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültige IPL!", "error", "top-end");
                    return;
                }

                MySqlCommand command = General.Connection.CreateCommand();
                command.CommandText = "UPDATE houseinteriors SET ipl=@ipl, posx=@posx, posy=@posy, posz=@posz, classify@classify WHERE id=@id";
                command.Parameters.AddWithValue("@ipl", ipl.ToLower());
                command.Parameters.AddWithValue("@posx", player.Position.X);
                command.Parameters.AddWithValue("@posY", player.Position.Y);
                command.Parameters.AddWithValue("@posZ", player.Position.Z);
                command.Parameters.AddWithValue("@classify", classify);
                command.Parameters.AddWithValue("@id", interior);
                command.ExecuteNonQuery();

                foreach (HouseInteriorModel houseinteriorModelTemp in House.houseListInteriors)
                {
                    if (houseinteriorModelTemp.id == interior)
                    {
                        id = houseinteriorModelTemp.id;
                        houseinteriorModelTemp.ipl = ipl;
                        houseinteriorModelTemp.posx = player.Position.X;
                        houseinteriorModelTemp.posy = player.Position.Y;
                        houseinteriorModelTemp.posz = player.Position.Z;
                        houseinteriorModelTemp.classify = classify;
                        break;
                    }
                }

                Helper.SendNotificationWithoutButton(player, "Hausinterior aktualisiert!", "success", "top-end");
                Helper.CreateAdminLog($"hauslog", account.name + $" hat das Hausinterior {id} aktualisiert!");
            }
            catch (Exception e)
            {
                Helper.SendNotificationWithoutButton(player, "Ungültige Option!", "error", "top-end");
                Helper.ConsoleLog("error", $"[CMD_createhouseinterior]: " + e.ToString());
            }
        }

        [Command("createhouse", "Befehl: /createhouse [Klein, Mittel, Gross, Villa, Kein-Interior]")]
        public void CMD_createhouse(Player player, string aktion = "n/A")
        {
            try
            {
                int interior, preis, classify = 0;
                Random rnd = new Random();
                if (!Account.IsPlayerLoggedIn(player)) return;
                Account account = Helper.GetAccountData(player);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Administrator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                House house = House.GetClosestHouse(player);
                if (house != null)
                {
                    Helper.SendNotificationWithoutButton(player, "Hier steht bereits ein Haus!", "error", "top-end");
                    return;
                }
                switch (aktion.ToLower())
                {
                    case "klein":
                        {
                            interior = House.GetRandomHouseInterior(0);
                            preis = Helper.Round(rnd.Next(50000, 65000));
                            classify = 0;
                            break;
                        }
                    case "mittel":
                        {
                            interior = House.GetRandomHouseInterior(1);
                            preis = Helper.Round(rnd.Next(150000, 200000));
                            classify = 1;
                            break;
                        }
                    case "gross":
                        {
                            interior = House.GetRandomHouseInterior(2);
                            preis = Helper.Round(rnd.Next(400000, 425000));
                            classify = 2;
                            break;
                        }
                    case "villa":
                        {
                            interior = House.GetRandomHouseInterior(3);
                            preis = Helper.Round(rnd.Next(800000, 815000));
                            classify = 3;
                            break;
                        }
                    case "kein-interior":
                        {
                            interior = 0;
                            preis = Helper.Round(rnd.Next(400000, 425000));
                            classify = 0;
                            break;
                        }
                    default:
                        {
                            Helper.SendChatMessage(player, "~w~Befehl: /createhouse [Klein, Mittel, Gross, Villa, Kein-Interior]");
                            return;
                        }
                }
                string houseLabel = string.Empty;
                house = new House();
                house.position = player.Position;
                house.dimension = (int)player.Dimension;
                house.price = preis;
                house.owner = "n/A";
                house.status = 0;
                house.tenants = 0;
                house.rental = 0;
                house.locked = 0;
                house.interior = interior;
                house.blip = 40;
                house.housegroup = -1;
                house.stock = 0;
                house.stockprice = 30;
                house.classify = classify;
                house.id = House.AddHouse(house);

                player.TriggerEvent("Client:SetHouseStreet", house.id);
                House.houseList.Add(house);

                Helper.SendNotificationWithoutButton(player, "Das Haus wurde erstellt!", "success", "top-end");
                Helper.CreateAdminLog($"hauslog", account.name + $" hat ein ein neues Haus erstellt - (Hausnummer: {house.id})!");
                NAPI.Task.Run(() =>
                {
                    House.SetHouseHandle(house);
                }, delayTime: 315);
                return;
            }
            catch (Exception e)
            {
                Helper.SendChatMessage(player, "~w~Befehl: /createhouse [Klein, Mittel, Gross, Villa]");
                Helper.ConsoleLog("error", $"[CMD_createhouse]: " + e.ToString());
            }
        }

        [Command("gotohouse", "Befehl: /gotohouse [Haus-ID]")]
        public void CMD_gotohouse(Player player, int hausid)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Account account = Helper.GetAccountData(player);
                Character character = Helper.GetCharacterData(player);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.ProbeModerator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                if (hausid < 0 || hausid > House.houseList.Count)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiges Haus!", "error", "top-end");
                    return;
                }
                House house = House.GetHouseById(hausid);
                if (house == null)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiges Haus!", "error", "top-end");
                    return;
                }
                if (character.inhouse > -1)
                {
                    character.inhouse = -1;
                    player.SetOwnSharedData("Player:InHouse", -1);
                }
                player.Dimension = (uint)house.dimension;
                Helper.SetPlayerPosition(player, house.position);
                Helper.SendNotificationWithoutButton(player, $"Du hast dich zum Haus ({hausid}) teleportiert!", "success", "top-end", 3500);
                return;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CMD_gotohouse]: " + e.ToString());
            }
        }

        [Command("gethouse", "Befehl: /gethouse [Haus-ID]")]
        public void CMD_gethouse(Player player, int hausid)
        {
            if (!Account.IsPlayerLoggedIn(player)) return;

            Account account = Helper.GetAccountData(player);
            if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Administrator))
            {
                Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                return;
            }
            House house = House.GetHouseById(hausid);
            if (house == null || hausid < 0 || hausid > House.houseList.Count)
            {
                Helper.SendNotificationWithoutButton(player, "Ungültiges Haus!", "error", "top-end");
                return;
            }

            house.position = player.Position;
            house.dimension = (int)player.Dimension;

            player.TriggerEvent("Client:SetHouseStreet", house.id);

            NAPI.Task.Run(() =>
            {
                House.SetHouseHandle(house);
                Helper.SendNotificationWithoutButton(player, "Die Position vom Haus wurde aktualisiert!", "success", "top-end");
                Task.Factory.StartNew(() =>
                {
                    House.SaveHouse(house);
                });
            }, delayTime: 315);
            return;
        }

        [Command("edithouse", "Befehl: /edithouse [Aktion] [Wert]")]
        public void CMD_edithouse(Player player, String aktion = "n/A", int wert = -1)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;

                Account account = Helper.GetAccountData(player);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Administrator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                House house = House.GetClosestHouse(player);
                if (house == null)
                {
                    Helper.SendNotificationWithoutButton(player, "Du bist nicht in der Nähe von einem Haus!", "error", "top-end");
                    return;
                }
                switch (aktion.ToLower())
                {
                    case "interior":
                        {
                            if (wert < 0 || wert > House.houseListInteriors.Count)
                            {
                                Helper.SendNotificationWithoutButton(player, "Ungültiges Interior!", "error", "top-end");
                                return;
                            }
                            house.interior = wert;
                            House.SetHouseHandle(house);
                            break;
                        }
                    case "preis":
                        {
                            if (wert < 0 || wert > 15000000)
                            {
                                Helper.SendNotificationWithoutButton(player, "Der Preis muss zwischen 0$ und 15000000$ sein!", "error", "top-end");
                                return;
                            }
                            if (house.owner != "n/A" && wert == 0)
                            {
                                Helper.SendNotificationWithoutButton(player, "Der Preis kann nur auf 0 gesetzt werden, sofern das Haus keinem gehört!", "error", "top-end");
                                return;
                            }
                            house.price = wert;
                            if (house.price == 0)
                            {
                                if (house.owner == "n/A")
                                {
                                    house.status = 5;
                                }
                            }
                            else
                            {
                                if (house.owner == "n/A")
                                {
                                    house.status = 0;
                                }
                            }
                            House.SetHouseHandle(house);
                            break;
                        }
                    case "lockstatus":
                        {
                            if (wert < 0 || wert > 1)
                            {
                                Helper.SendNotificationWithoutButton(player, "Ungültiger Lockstatus!", "error", "top-end");
                                return;
                            }
                            house.locked = wert;
                            break;
                        }
                    case "mietstatus":
                        {
                            if (wert < 0 || wert > 100000)
                            {
                                Helper.SendNotificationWithoutButton(player, "Ungültiger Mietstatus!", "error", "top-end");
                                return;
                            }
                            house.rental = wert;
                            if (wert > 0)
                            {
                                if (house.noshield == 1)
                                {
                                    house.status = 2;
                                }
                                else
                                {
                                    house.status = 3;
                                }
                            }
                            else
                            {
                                if (house.noshield == 1)
                                {
                                    house.status = 2;
                                }
                                else
                                {
                                    house.status = 1;
                                }
                            }
                            break;
                        }
                    case "klingelschildwert":
                        {
                            if (wert < 0 || wert > 1)
                            {
                                Helper.SendNotificationWithoutButton(player, "Ungültiger Klingelschildwert!", "error", "top-end");
                                return;
                            }
                            house.noshield = wert;
                            house.status = 2;
                            break;
                        }
                    case "lager":
                        {
                            if (wert < 0 || wert > 3500)
                            {
                                Helper.SendNotificationWithoutButton(player, "Ungültiger Lagerwert!", "error", "top-end");
                                return;
                            }
                            house.stock = wert;
                            break;
                        }
                    case "gruppierung":
                        {
                            Groups group = GroupsController.GetGroupById(wert);
                            if (group == null)
                            {
                                Helper.SendNotificationWithoutButton(player, "Ungültiger Gruppierungswert!", "error", "top-end");
                                return;
                            }
                            house.housegroup = wert;
                            House.SetHouseHandle(house);
                            break;
                        }
                    case "blip":
                        {
                            if (wert < 1 || wert > 826 || wert == 60 || wert == 420)
                            {
                                Helper.SendNotificationWithoutButton2(player, "Ungültiges Blipwert!", "error", "center");
                                return;
                            }
                            house.blip = wert;
                            House.SetHouseHandle(house);
                            break;
                        }
                    case "produktpreis":
                        {
                            if (wert < 50 || wert > 250)
                            {
                                Helper.SendNotificationWithoutButton(player, "Ungültiger Produktpreis!", "error", "top-end");
                                return;
                            }
                            house.housegroup = wert;
                            House.SetHouseHandle(house);
                            break;
                        }
                    case "klasse":
                        {
                            if (wert < 0 || wert > 4)
                            {
                                Helper.SendNotificationWithoutButton(player, "Ungültiger Wert!", "error", "top-end");
                                return;
                            }
                            house.interior = House.GetRandomHouseInterior(wert);
                            Random rnd = new Random();
                            if (wert == 0)
                            {
                                house.price = Helper.Round(rnd.Next(50000, 65000));
                            }
                            else if (wert == 1)
                            {
                                house.price = Helper.Round(rnd.Next(150000, 200000));
                            }
                            else if (wert == 2)
                            {
                                house.price = Helper.Round(rnd.Next(400000, 425000));
                            }
                            else if (wert == 3)
                            {
                                house.price = Helper.Round(rnd.Next(800000, 815000));
                            }
                            house.classify = wert;
                            break;
                        }
                    default:
                        {
                            player.SendChatMessage("~w~Befehl: /edithouse [Aktion] [Wert]");
                            player.SendChatMessage("~y~Verfügbare Aktionen: Interior, Preis, Lockstatus, Mietstatus, Klingelschildwert, Gruppierung, Lager, Klasse");
                            return;
                        }
                }
                aktion = aktion[0].ToString().ToUpper() + aktion.Substring(1);
                House.SetHouseHandle(house);
                Helper.SendNotificationWithoutButton(player, $"Der angegebene Wert des Hauses {house.id} wurde bearbeitet!", "success", "top-end", 4500);
                House.SaveHouse(house);
                Helper.CreateAdminLog($"hauslog", account.name + $" hat den Wert {aktion} des Hauses {house.id} auf {wert} gesetzt!");
            }
            catch (Exception e)
            {
                player.SendChatMessage("~w~Befehl: /edithouse [Aktion] [Wert]");
                player.SendChatMessage("~y~Verfügbare Aktionen: Interior, Preis, Lockstatus, Mietstatus, Klingelschildwert, Gruppierung, Lager, Klasse");
                Helper.ConsoleLog("error", $"[CMD_edithouse]: " + e.ToString());
            }
            return;
        }

        [Command("creategangzone", "Befehl: /creategangzone [Name] [Ertrag: Materialien, Geld, Drogen] *[Radius: 50]")]
        public void CMD_creategangzone(Player player, string name, string value, int radius = 50)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Account account = Helper.GetAccountData(player);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Administrator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                if (name.Length < 5 || name.Length > 65)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Name!", "error", "top-end");
                    return;
                }
                if (value != "Materialien" && value != "Geld" && value != "Drogen")
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Ertrag!", "error", "top-end");
                    return;
                }
                if (radius < 25 || radius > 275)
                {
                    Helper.SendNotificationWithoutButton(player, "Der Radius muss zwischen 25 und 275 liegen!", "error", "top-end");
                    return;
                }
                GangController.CreateNewGangZone(player, name, value, radius);
                return;
            }
            catch (Exception e)
            {
                Helper.SendChatMessage(player, "~w~Befehl: /creategangzone [Name] [Ertrag: Materialien, Geld, Drogen]");
                Helper.ConsoleLog("error", $"[CMD_creategangzone]: " + e.ToString());
            }
        }

        [Command("updategangzone", "Befehl: /updategangzone [Name] [Ertrag: Materialien, Geld, Drogen] *[Radius: 50]")]
        public void CMD_updategangzone(Player player, string name, string value, int radius = 50)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Account account = Helper.GetAccountData(player);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Administrator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                bool found = false;
                foreach (GangZones gangzone in GangController.gangzoneList)
                {
                    if (gangzone.name == name)
                    {
                        found = true;
                        break;
                    }
                }
                if (found == false)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültige Gangzone!", "error", "top-end");
                    return;
                }
                if (value != "Materialien" && value != "Geld" && value != "Drogen")
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Ertrag!", "error", "top-end");
                    return;
                }
                if (radius < 25 || radius > 275)
                {
                    Helper.SendNotificationWithoutButton(player, "Der Radius muss zwischen 25 und 275 liegen!", "error", "top-end");
                    return;
                }
                GangController.UpdateGangZone(player, name, value, radius);
                return;
            }
            catch (Exception e)
            {
                Helper.SendChatMessage(player, "~w~Befehl: /updategangzone [Name] [Ertrag: Materialien, Geld, Drogen] *[Radius: 50]");
                Helper.ConsoleLog("error", $"[CMD_creategangzone]: " + e.ToString());
            }
        }

        [Command("createcoupon", "Befehl: /creategangzone *[Max-Anwendungen: 1]")]
        public void CMD_createcoupon(Player player, int uses = 1)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Account account = Helper.GetAccountData(player);
                TempData tempData = Helper.GetCharacterTempData(player);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Supporter))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                if (uses < 1 || uses > 50)
                {
                    Helper.SendNotificationWithoutButton(player, "Die Max-Anwendung muss zwischen 1 und 50 liegen!", "error", "top-end");
                    return;
                }
                string text1 = "+1 Erfahrungspunkt,+3 Erfahrungspunkte,+5 Erfahrungspunkte,+1 Level,1x Namechange,+24h Erfahrungspunkte Boost,+1 Hausslot,+1 Fahrzeugslot,Premium Bronze 30 Tage,Premium Silber 30 Tage,Premium Gold 30 Tage,15000$,35000$,50000$,100000$,Abbrechen";
                string text2 = "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0";
                tempData.lastShop = "Gutschein erstellen";
                tempData.tempValue = uses;
                player.TriggerEvent("Client:ShowShop2", text1, text2, "Gutschein erstellen", 0, 1, 1, false);
                return;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CMD_createcoupon]: " + e.ToString());
            }
        }

        [Command("createvehicle", "Befehl: /createvehicle [Business-ID] [Preis]")]
        public void CMD_createvehicle(Player player, int bizzid, int preis)
        {
            try
            {
                bool found = false;
                VehicleShop vehicleShop = null;
                if (!Account.IsPlayerLoggedIn(player)) return;
                Account account = Helper.GetAccountData(player);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Administrator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                if (!player.IsInVehicle)
                {
                    Helper.SendNotificationWithoutButton(player, "Du sitzt in keinem Fahrzeug!", "error", "top-end");
                    return;
                }
                if (bizzid < 0 || bizzid > Business.businessList.Count)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültige Business-ID!", "error", "top-end");
                    return;
                }
                if (preis < 1 || preis > 99999999)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Preis!", "error", "top-end");
                    return;
                }

                PetaPoco.Database db = new PetaPoco.Database(General.Connection);

                String vehicleName = player.Vehicle.GetSharedData<string>("Vehicle:Name")[0].ToString().ToUpper() + player.Vehicle.GetSharedData<string>("Vehicle:Name").Substring(1);
                uint vehash = NAPI.Util.GetHashKey(vehicleName.ToLower());

                if (DealerShipController.dealerShipList.Count > 0)
                {
                    foreach (VehicleShop vehicleShopInList in DealerShipController.dealerShipList)
                    {
                        if (vehicleShopInList.vehiclename == vehicleName)
                        {
                            vehicleShopInList.bizzid = bizzid;
                            vehicleShopInList.vehiclename = vehicleName;
                            vehicleShopInList.price = preis;
                            vehicleShopInList.maxspeed = Convert.ToInt32(NAPI.Vehicle.GetVehicleMaxSpeed((VehicleHash)vehash) * 3.6);
                            vehicleShopInList.maxacceleration = NAPI.Vehicle.GetVehicleMaxAcceleration((VehicleHash)vehash);
                            vehicleShopInList.maxbraking = NAPI.Vehicle.GetVehicleMaxBraking((VehicleHash)vehash);
                            vehicleShopInList.maxhandling = NAPI.Vehicle.GetVehicleMaxTraction((VehicleHash)vehash);
                            vehicleShopInList.trunk = Cars.GetVehicleTrunk(player.Vehicle);
                            vehicleShopInList.fuel = Convert.ToInt32(player.Vehicle.GetSharedData<float>("Vehicle:MaxFuel"));
                            found = true;
                            Helper.SendNotificationWithoutButton(player, "Das Fahrzeug wurde aktualisiert und kann im Business verkauft werden!", "success", "top-end", 3750);
                            db.Save(vehicleShopInList);
                            break;
                        }
                    }
                }

                if (found == false)
                {
                    vehicleShop = new VehicleShop();
                    vehicleShop.id = DealerShipController.dealerShipList.Count;
                    vehicleShop.bizzid = bizzid;
                    vehicleShop.vehiclename = vehicleName;
                    vehicleShop.price = preis;
                    vehicleShop.maxspeed = Convert.ToInt32(player.Vehicle.MaxSpeed * 3.6);
                    vehicleShop.maxacceleration = player.Vehicle.MaxAcceleration;
                    vehicleShop.maxbraking = player.Vehicle.MaxBraking;
                    vehicleShop.maxhandling = player.Vehicle.MaxTraction;
                    vehicleShop.trunk = Cars.GetVehicleTrunk(player.Vehicle);
                    vehicleShop.fuel = Convert.ToInt32(player.Vehicle.GetSharedData<float>("Vehicle:MaxFuel"));
                    DealerShipController.dealerShipList.Add(vehicleShop);
                    Helper.SendNotificationWithoutButton(player, "Das Fahrzeug wurde erstellt und kann im Business verkauft werden!", "success", "top-end", 3750);
                    db.Insert(vehicleShop);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CMD_createvehicle]: " + e.ToString());
            }
        }

        [Command("createvehicle2", "Befehl: /createvehicle2 [Name]")]
        public void CMD_createvehicle(Player player, string vehiclename)
        {
            try
            {
                VehicleShop vehicleShop = null;
                if (!Account.IsPlayerLoggedIn(player)) return;
                Account account = Helper.GetAccountData(player);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Administrator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                if (vehiclename.Length < 3 || vehiclename.Length > 60)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Fahrzeugname!", "error", "top-end");
                    return;
                }
                Business bizz = Business.GetClosestBusiness(player, 40.5f);
                if (bizz == null)
                {
                    Helper.SendNotificationWithoutButton(player, "Du bist nicht in der Nähe von einem Business!", "error", "top-end");
                    return;
                }
                if (bizz.id < 22 || bizz.id > 32)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiges Business!", "error", "top-end");
                    return;
                }

                PetaPoco.Database db = new PetaPoco.Database(General.Connection);

                uint vehash = NAPI.Util.GetHashKey(vehiclename.ToLower());
                if (vehash <= 0)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiges Fahrzeug!", "error", "top-end");
                    return;
                }

                string vehicleName2 = vehiclename = vehiclename[0].ToString().ToUpper() + vehiclename.Substring(1);

                Vehicle tempCar = Cars.createNewCar(vehicleName2, new Vector3(player.Position.X, player.Position.Y, player.Position.Z - 0.1), player.Heading, 0, 0, "Tempcar", "Tempcar", false, true, false, player.Dimension + 25); ;

                NAPI.Task.Run(() =>
                {
                    vehicleShop = new VehicleShop();
                    vehicleShop.id = DealerShipController.dealerShipList.Count;
                    vehicleShop.bizzid = bizz.id;
                    vehicleShop.vehiclename = vehicleName2.ToLower();
                    vehicleShop.price = 1;
                    vehicleShop.maxspeed = 0;
                    vehicleShop.maxacceleration = 0.0f;
                    vehicleShop.maxbraking = 0.0f;
                    vehicleShop.maxhandling = 0.0f;
                    vehicleShop.trunk = Cars.GetVehicleTrunk(tempCar);
                    vehicleShop.fuel = (int)Cars.GetVehicleFuel(tempCar);
                    Helper.SendNotificationWithoutButton(player, "Das Fahrzeug wurde erstellt und kann ab dem nächsten Restart im Business verkauft werden!", "success", "top-end", 3750);
                    db.Insert(vehicleShop);
                }, delayTime: 255);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CMD_createvehicle2]: " + e.ToString());
            }
        }

        [Command("createanimation", "Befehl: /createanimation [Kategorie] [Animationsname] [Dict] [Name] [Flag*] [Länge*]", Alias = "createanim")]
        public void CMD_createanimation(Player player, string kategorie, string name, string dict, string name2, int flag = 1, int duration = -1)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Account account = Helper.GetAccountData(player);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Administrator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                if (kategorie.Length < 3 || kategorie.Length > 35)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültige Kategorie!", "error", "top-end");
                    return;
                }
                if (dict.Length < 3 || dict.Length > 64 || name.Length < 3 || name.Length > 64 || name2.Length < 3 || name2.Length > 64)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültige Animation!", "error", "top-end");
                    return;
                }

                foreach (Animations anim in Helper.animList)
                {
                    if (anim.name == name)
                    {
                        Helper.SendNotificationWithoutButton(player, "Diese Animation ist bereits vorhanden!", "error", "top-end");
                        return;
                    }
                }

                Animations animation = new Animations();
                animation.name = name.ToLower();
                animation.animation = $"{dict.ToLower()}%{name2.ToLower()}%{flag}";
                animation.category = kategorie.ToLower();
                animation.id = Helper.animList.Count;
                animation.duration = duration;

                Helper.animList.Add(animation);

                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                db.Insert(animation);

                kategorie = kategorie[0].ToString().ToUpper() + kategorie.Substring(1);
                name = name[0].ToString().ToUpper() + name.Substring(1);
                Helper.SendNotificationWithoutButton(player, "Neue Animation wurde erfolgreich hinzugefügt!", "success", "top-end");
                Helper.CreateAdminLog($"adminlog", account.name + $" hat die Animation {dict} {name2} für die Kategorie {kategorie} mit dem Namen {name} hinzugefügt!");
            }
            catch (Exception e)
            {
                Helper.SendNotificationWithoutButton(player, "Die Animation konnte nicht erstellt werden!", "error", "top-left", 1500);
                Helper.ConsoleLog("error", $"[CMD_createanimation]: " + e.ToString());
            }
        }

        [Command("createszenario", "Befehl: /createszenario [Kategorie] [Szenarioname]", Alias = "createscene")]
        public void CMD_createanimation(Player player, string kategorie, string name)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Account account = Helper.GetAccountData(player);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Administrator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                if (kategorie.Length < 3 || kategorie.Length > 35)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültige Kategorie!", "error", "top-end");
                    return;
                }
                if (name.Length < 3 || name.Length > 64)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültige Szenario!", "error", "top-end");
                    return;
                }

                foreach (Animations anim in Helper.animList)
                {
                    if (anim.name == name)
                    {
                        Helper.SendNotificationWithoutButton(player, "Dieses Szenario ist bereits vorhanden!", "error", "top-end");
                        return;
                    }
                }
                Animations animation = new Animations();
                animation.name = name.ToLower();
                animation.animation = $"{name.ToUpper()}";
                animation.category = kategorie.ToLower();
                animation.id = Helper.animList.Count;

                Helper.animList.Add(animation);

                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                db.Insert(animation);

                kategorie = kategorie[0].ToString().ToUpper() + kategorie.Substring(1);
                name = name[0].ToString().ToUpper() + name.Substring(1);
                Helper.SendNotificationWithoutButton(player, "Neues Szenario wurde erfolgreich hinzugefügt!", "success", "top-end");
                Helper.CreateAdminLog($"adminlog", account.name + $" hat ein neues Szenario für die Kategorie {kategorie} mit dem Namen {name} hinzugefügt!");
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CMD_createszenario]: " + e.ToString());
            }
        }

        [Command("editanimation", "Befehl: /editanimation [Name] [Kategorie] [Animationsname] [Dict] [Neuer Name] [Flag*]", Alias = "editanim")]
        public void CMD_editanimation(Player player, string oldname, string kategorie, string name, string dict, string name2, int flag = 1)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Account account = Helper.GetAccountData(player);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Administrator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                if (kategorie.Length < 3 || kategorie.Length > 64)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültige Kategorie!", "error", "top-end");
                    return;
                }
                if (dict.Length < 3 || dict.Length > 64 || name.Length < 3 || name.Length > 64 || name2.Length < 3 || name2.Length > 35 || oldname.Length < 3 || oldname.Length > 64)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültige Animation!", "error", "top-end");
                    return;
                }

                Animations animation = null;

                foreach (Animations anim in Helper.animList)
                {
                    if (anim.name == name)
                    {
                        animation = anim;
                        break;
                    }
                }

                if (animation == null)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültige Animation!", "error", "top-end");
                    return;
                }

                animation.name = name.ToLower();
                animation.animation = $"{dict.ToLower()}%{name2.ToLower()}%{flag}";
                animation.category = kategorie.ToLower();

                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                db.Save(animation);

                kategorie = kategorie[0].ToString().ToUpper() + kategorie.Substring(1);
                name = name[0].ToString().ToUpper() + name.Substring(1);
                Helper.SendNotificationWithoutButton(player, "Die Animation wurde erfolgreich bearbeitet!", "success", "top-end");
                Helper.CreateAdminLog($"adminlog", account.name + $" hat die Animation {dict} {name2} für die Kategorie {kategorie} mit dem Namen {name} bearbeitet!");
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CMD_editanimation]: " + e.ToString());
            }
        }

        [Command("testanim", "Befehl: /testanim [Dict] [Name] [Flag*] [Länge*]")]
        public void CMD_testanim(Player player, string dict, string name, int flag = 1, int duration = -1)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Account account = Helper.GetAccountData(player);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Administrator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                if (dict.Length < 3 || dict.Length > 64 || name.Length < 3 || name.Length > 64)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültige Animation!", "error", "top-end");
                    return;
                }
                if (duration <= 32)
                {
                    player.SetData<bool>("Player:PlayCustomAnimation", true);
                    player.SetSharedData("Player:AnimData", $"{dict}%{name}%{flag}");
                }
                else
                {
                    Helper.PlayShortAnimation(player, dict, name, duration);
                }
                Helper.SendNotificationWithoutButton(player, "Neue Animation wird abgespielt, benutze /stopanim um diese zu beenden!", "success", "top-end");
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CMD_testanim]: " + e.ToString());
            }
        }

        [Command("testobject", "Befehl: /testobject [Objektname]")]
        public void CMD_testobject(Player player, string name)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Account account = Helper.GetAccountData(player);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Administrator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                if (name.Length < 1 || name.Length > 64)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiges Objekt!", "error", "top-end");
                    return;
                }
                Vector3 newPosition = Helper.GetPositionInFrontOfPlayer(player, 0.65f);
                NAPI.Object.CreateObject(NAPI.Util.GetHashKey(name), new Vector3(newPosition.X, newPosition.Y, newPosition.Z - 0.3), new Vector3(0.0f, 0.0f, player.Heading + 90), 255, 0);
                Helper.SendNotificationWithoutButton(player, "Das Objekt (sofern vorhanden) wurde erstellt!", "success", "top-end");
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CMD_testobject]: " + e.ToString());
            }
        }

        [Command("createbizzdoor", "Befehl: /createbizzdoor [Hash] [Business-ID]", Alias = "cbd")]
        public void CMD_createbizzdoor(Player player, string hash, int bizz)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Account account = Helper.GetAccountData(player);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Administrator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }

                if (bizz <= 0 || bizz > Business.businessList.Count)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültige Business-ID!", "error", "top-end");
                    return;
                }

                Doors checkDoor = DoorsController.GetClosestDoor(player, 0.45f);
                if (checkDoor != null)
                {
                    Helper.SendNotificationWithoutButton(player, "Hier befindet sich schon eine Business-Tür in der Nähe!", "error", "top-end");
                    return;
                }

                Doors door = new Doors();
                door.id = DoorsController.GetFreeDoorsID();
                door.hash = hash;
                door.posx = player.Position.X;
                door.posy = player.Position.Y;
                door.posz = player.Position.Z;
                door.dimension = 0;
                door.toogle = true;
                door.props = "bizz-" + bizz;
                door.save = false;

                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                db.Insert(door);

                DoorsController.AddDoor(door, true);

                Helper.SendNotificationWithoutButton(player, "Business-Tür wurde erstellt!", "success", "top-end");
            }
            catch (Exception)
            {
                Helper.SendNotificationWithoutButton(player, "Business-Tür konnte nicht erstellt werden!", "error", "top-end");
            }
        }


        [Command("createbizz", "Befehl: /createbizz [Preis] [Blip-ID] [Business-Name]", GreedyArg = true)]
        public void CMD_createbizz(Player player, int price, int blipid, string name)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Account account = Helper.GetAccountData(player);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Administrator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                if (price < 0 || price > 50000000)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Preis!", "error", "top-end");
                    return;
                }
                if (blipid < 1 || blipid > 826)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültige Blip-ID!", "error", "top-end");
                    return;
                }
                if (name.Length < 5 || name.Length > 64)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültige Business-Name!", "error", "top-end");
                    return;
                }
                Business bizz = Business.GetClosestBusiness(player);
                if (bizz != null)
                {
                    Helper.SendNotificationWithoutButton(player, "Hier steht bereits ein Business!", "error", "top-end");
                    return;
                }
                string bizzLabel = string.Empty;
                bizz = new Business();
                bizz.position = player.Position;
                bizz.price = price;
                bizz.blipid = blipid;
                bizz.name = name;
                bizz.products = 2000;
                bizz.prodprice = 30;
                bizz.id = Business.AddBusiness(bizz);
                bizz.name2 = bizz.name;

                Business.businessList.Add(bizz);

                Helper.SendNotificationWithoutButton(player, "Das Business wurde erstellt!", "success", "top-end");
                Helper.CreateAdminLog($"bizzlog", account.name + $" hat ein ein neues Busness erstellt - ({bizz.name}[{bizz.id})!");
                NAPI.Task.Run(() =>
                {
                    Business.SetBusinessHandle(bizz);
                }, delayTime: 315);
                return;
            }
            catch (Exception e)
            {
                Helper.SendChatMessage(player, "~w~Befehl: /createhouse [Klein, Mittel, Gross, Villa]");
                Helper.ConsoleLog("error", $"[CMD_createhouse]: " + e.ToString());
            }
        }

        [Command("getbizz", "Befehl: /getbizz [Business-ID]")]
        public void CMD_getbizz(Player player, int bizzid)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;

                Account account = Helper.GetAccountData(player);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Administrator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                Business bizz = Business.GetBusinessById(bizzid);
                int businessCount = Business.businessList.Count;
                if (bizz == null || bizzid < 0 || bizzid > businessCount)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiges Business!", "error", "top-end");
                    return;
                }

                bizz.position = player.Position;

                NAPI.Task.Run(() =>
                {
                    Business.SetBusinessHandle(bizz);
                    Helper.SendNotificationWithoutButton(player, "Die Position vom Business wurde aktualisiert!", "success", "top-end");
                    Task.Factory.StartNew(() =>
                    {
                        Business.SaveBusiness(bizz);
                    });
                }, delayTime: 315);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CMD_getbizz]: " + e.ToString());
            }
            return;
        }

        [Command("gotobizz", "Befehl: /gotobizz [Business-ID]")]
        public void CMD_gotobizz(Player player, int bizzid)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Account account = Helper.GetAccountData(player);
                Character character = Helper.GetCharacterData(player);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.ProbeModerator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                if (bizzid < 0 || bizzid > Business.businessList.Count)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiges Business!", "error", "top-end");
                    return;
                }
                Business bizz = Business.GetBusinessById(bizzid);
                if (bizz == null)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiges Business!", "error", "top-end");
                    return;
                }
                if (character.inhouse > -1)
                {
                    character.inhouse = -1;
                    player.SetOwnSharedData("Player:InHouse", -1);
                }
                player.Dimension = 0;
                Helper.SetPlayerPosition(player, bizz.position);
                Helper.SendNotificationWithoutButton(player, $"Du hast dich zum Business ({bizz.name}) teleportiert!", "success", "top-end", 3500);
                return;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CMD_gotobizz]: " + e.ToString());
            }
        }

        [Command("asellbizz", "Befehl: /asellbizz")]
        public void CMD_asellbizz(Player player)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Account account = Helper.GetAccountData(player);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Administrator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                Business bizz = Business.GetClosestBusiness(player);
                if (bizz == null)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiges Business!", "error", "top-left");
                    return;
                }
                if (bizz.owner == "n/A")
                {
                    Helper.SendNotificationWithoutButton(player, "Dieses Business kann nicht verkauft werden!", "error", "top-left");
                    return;
                }
                bizz.owner = "n/A";
                if (bizz.cash > bizz.price)
                {
                    bizz.cash = bizz.price / 2;
                    bizz.govcash = 0;
                }
                if (bizz.products < 0)
                {
                    bizz.products = 0;
                }
                bizz.multiplier = 1.0f;
                bizz.prodprice = 30;
                bizz.buyproducts = 0;
                bizz.selled = 0;
                bizz.name = bizz.name2;
                Business.SetBusinessHandle(bizz);
                Business.DeleteAllKeys(bizz.id, "n/A");
                Business.SaveBusiness(bizz);
                Helper.SendNotificationWithoutButton(player, "Du hast das Business administrativ verkauft!", "success", "top-left", 4500);
                Helper.CreateAdminLog($"bizzlog", account.name + $" hat das Business {bizz.name}[{bizz.id}] administrativ verkauft!");
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CMD_asellbizz]: " + e.ToString());
            }
        }

        [Command("editbizz", "Befehl: /editbizz [Aktion] [Wert]")]
        public void CMD_editbizz(Player player, String aktion = "n/A", string wertstring = "-1")
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;

                Account account = Helper.GetAccountData(player);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Administrator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                Business bizz = Business.GetClosestBusiness(player);
                if (bizz == null)
                {
                    Helper.SendNotificationWithoutButton(player, "Du bist nicht in der Nähe von einem Business!", "error", "top-end");
                    return;
                }
                if (wertstring == "-1")
                {
                    player.SendChatMessage("~w~Befehl: /editbizz [Aktion] [Wert]");
                    player.SendChatMessage("~y~Verfügbare Aktionen: Preis, Produkte, Blipid, Produktpreis, Steuern, Kasse, ProdukteKaufen");
                    return;
                }
                int wert = Convert.ToInt32(wertstring);
                switch (aktion.ToLower())
                {
                    case "preis":
                        {
                            if (wert < 0 || wert > 50000000)
                            {
                                Helper.SendNotificationWithoutButton(player, "Der Preis muss zwischen 0$ und 50.000.000$ sein!", "error", "top-end");
                                return;
                            }
                            if (bizz.owner != "n/A" && wert == 0)
                            {
                                Helper.SendNotificationWithoutButton(player, "Der Preis kann nur auf 0 gesetzt werden, sofern das Business keinem gehört!", "error", "top-end");
                                return;
                            }
                            bizz.price = wert;
                            Business.SetBusinessHandle(bizz);
                            break;
                        }
                    case "produkte":
                        {
                            if (wert < 0 || wert > 2000)
                            {
                                Helper.SendNotificationWithoutButton(player, "Das Lager umfasst max. 2000 und min. 0 Produkte Platz!", "error", "top-end");
                                return;
                            }
                            bizz.products = wert;
                            break;
                        }
                    case "blipid":
                        {
                            if (wert < 1 || wert > 826)
                            {
                                Helper.SendNotificationWithoutButton(player, "Ungültige Blipid!", "error", "top-end");
                                return;
                            }
                            bizz.blipid = wert;
                            break;
                        }
                    case "produktpreis":
                        {
                            if ((wert < 30 || wert > 1000) && wert != 0)
                            {
                                Helper.SendNotificationWithoutButton(player, "Ungültiger Produktpreis!", "error", "top-end");
                                return;
                            }
                            bizz.prodprice = wert;
                            break;
                        }
                    case "steuern":
                        {
                            if (wert < 0 || wert > 50000000)
                            {
                                Helper.SendNotificationWithoutButton(player, "Ungültige Steuern!", "error", "top-end");
                                return;
                            }
                            bizz.govcash = wert;
                            break;
                        }
                    case "kasse":
                        {
                            if (wert < 0 || wert > 50000000)
                            {
                                Helper.SendNotificationWithoutButton(player, "Ungültiger Kassenwert!", "error", "top-end");
                                return;
                            }
                            bizz.cash = wert;
                            break;
                        }
                    case "produktekaufen":
                        {
                            if (wert < 0 || wert > 1)
                            {
                                Helper.SendNotificationWithoutButton(player, "Ungültiger Wert: 0 = Produkte kaufen | 1 = Keine Produkte kaufen!", "error", "top-end");
                                return;
                            }
                            bizz.buyproducts = wert;
                            break;
                        }
                    default:
                        {
                            player.SendChatMessage("~w~Befehl: /editbizz [Aktion] [Wert]");
                            player.SendChatMessage("~y~Verfügbare Aktionen: Preis, Produkte, Blipid, Produktpreis, Steuern, Kasse, ProdukteKaufen");
                            return;
                        }
                }
                aktion = aktion[0].ToString().ToUpper() + aktion.Substring(1);
                Business.SetBusinessHandle(bizz);
                Helper.SendNotificationWithoutButton(player, $"Der angegebene Wert des Business {bizz.name}[{bizz.id}] wurde bearbeitet!", "success", "top-end", 4500);
                Business.SaveBusiness(bizz);
                Helper.CreateAdminLog($"bizzlog", account.name + $" hat den Wert {aktion} des Business {bizz.name}[{bizz.id}] auf {wert} gesetzt!");
            }
            catch (Exception e)
            {
                player.SendChatMessage("~w~Befehl: /editbizz [Aktion] [Wert]");
                player.SendChatMessage("~y~Verfügbare Aktionen: Preis, Produkte, Blipid, Produktpreis, Steuern, Kasse, ProdukteKaufen");
                Helper.ConsoleLog("error", $"[CMD_editbizz]: " + e.ToString());
            }
            return;
        }

        [Command("editbizzname", "Befehl: /editbizzname [Name]", GreedyArg = true)]
        public void CMD_editbizz(Player player, String name)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;

                Account account = Helper.GetAccountData(player);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Administrator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                Business bizz = Business.GetClosestBusiness(player);
                if (bizz == null)
                {
                    Helper.SendNotificationWithoutButton(player, "Du bist nicht in der Nähe von einem Business!", "error", "top-end");
                    return;
                }
                if (name.Length < 5 || name.Length > 64)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültige Business-Name!", "error", "top-end");
                    return;
                }
                bizz.name = name;
                bizz.name2 = name;
                Business.SetBusinessHandle(bizz);
                Helper.SendNotificationWithoutButton(player, "Der Business-Name wurde bearbeitet!", "success", "top-end", 4500);
                Business.SaveBusiness(bizz);
                Helper.CreateAdminLog($"bizzlog", account.name + $" hat den Business-Namen des Business {bizz.id} auf {name} gesetzt!");
            }
            catch (Exception e)
            {
                player.SendChatMessage("~w~Befehl: /editbizz [Aktion] [Wert]");
                player.SendChatMessage("~y~Verfügbare Aktionen: Preis, Produkte, Blipid, Produktpreis, Steuern, Kasse");
                Helper.ConsoleLog("error", $"[CMD_editbizz]: " + e.ToString());
            }
            return;
        }

        [Command("bizzcheck", "Befehl: /bizzcheck")]
        public void cmd_bizzcheck(Player player)
        {
            if (!Account.IsPlayerLoggedIn(player)) return;
            Account account = Helper.GetAccountData(player);
            if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Administrator))
            {
                Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                return;
            }
            Business bizz = Business.GetClosestBusiness(player);
            if (bizz == null)
            {
                Helper.SendNotificationWithoutButton(player, "Ungültiges Business!", "error", "center");
                return;
            }
            Helper.SendChatMessage(player, $"~b~[Businesscheck]: {bizz.name}[{bizz.id}], Besitzer: {bizz.owner}, Preis: {bizz.price}$, Produkte: {bizz.products}/2000, Produktpreis: {bizz.prodprice}$, Kasse: {bizz.cash}$, Steuern: {bizz.govcash}$");
            return;
        }

        [Command("reconnect", "Befehl: /reconnect [Spieler/ID]")]
        public void CMD_reconnect(Player player, string target)
        {
            if (!Account.IsPlayerLoggedIn(player)) return;
            Account account = Helper.GetAccountData(player);
            if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Supporter))
            {
                Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                return;
            }
            Player ntarget = Helper.GetPlayerByNameOrID(target);
            if (ntarget == null)
            {
                Helper.SendNotificationWithoutButton(player, "Ungültiger Spieler!", "error");
                return;
            }
            Account account2 = Helper.GetAccountData(ntarget);
            if (!Account.IsPlayerLoggedIn(ntarget))
            {
                Helper.SendNotificationWithoutButton(player, "Ungültiger Spieler!", "error");
                return;
            }
            if (account.adminlevel < account2.adminlevel)
            {
                Helper.SendNotificationWithoutButton(player, "Ungültiger Spieler!", "error");
                return;
            }
            Helper.SendNotificationWithoutButton(ntarget, $"Du wurdest von {account.name} reconnected!", "success", "top-end", 3500);
            Helper.SendNotificationWithoutButton(player, $"Du hast {account2.name} reconnected!", "success", "top-end", 3500);
            Helper.SendAdminMessage2($"{account2.name} wurde von {account.name} reconnected!", 1, false);
            Helper.CreateAdminLog($"adminlog", account.name + " hat " + account2.name + " reconnected!");
            NAPI.Task.Run(() =>
            {
                ntarget.KickSilent();
            }, delayTime: 515);
            return;
        }

        [Command("revive", "Befehl: /revive [Spieler/ID]")]
        public void CMD_revive(Player player, string target)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Account account = Helper.GetAccountData(player);
                Character character = Helper.GetCharacterData(player);
                if (player.GetOwnSharedData<bool>("Player:Testmodus") == false && !Account.IsAdminOnDuty(player, (int)Account.AdminRanks.ProbeModerator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                Player ntarget = Helper.GetPlayerByNameOrID(target);
                if (ntarget == null || !ntarget.Exists)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Spieler!", "error");
                    return;
                }
                Account account2 = Helper.GetAccountData(ntarget);
                Character character2 = Helper.GetCharacterData(ntarget);
                TempData tempData2 = Helper.GetCharacterTempData(ntarget);
                if (character2.death == false)
                {
                    Helper.SendNotificationWithoutButton(player, "Dieser Spieler ist nicht Tod!", "error");
                    return;
                }
                if (!Account.IsPlayerLoggedIn(ntarget))
                {
                    Helper.SendNotificationWithoutButton(player, "Du kannst diesen Spieler nicht wiederbeleben!", "error");
                    return;
                }
                NAPI.Task.Run(() =>
                {
                    Helper.SetPlayerPosition(ntarget, new Vector3(ntarget.Position.X, ntarget.Position.Y, ntarget.Position.Z + 0.15f));
                }, delayTime: 55);
                ntarget.TriggerEvent("Client:UnsetDeath");
                character2.death = false;
                ntarget.SetOwnSharedData("Player:Death", false);
                ntarget.SetSharedData("Player:Adminsettings", "0,0,0");
                Helper.SendNotificationWithoutButton(player, $"Du hast { account2.name } wiederbelebt!", "success", "top-end", 3500);
                Helper.SendNotificationWithoutButton(ntarget, $"{ account.name } hat dich wiederbelebt", "success", "top-end", 3500);
                Helper.SendAdminMessage2($"{account.name} hat {account2.name} wiederbelebt!", 1, false);
                Helper.SetPlayerHealth(ntarget, 100);
                Helper.SpawnPlayer(ntarget, ntarget.Position, ntarget.Heading);
                Helper.OnStopAnimation2(ntarget);
                if (tempData2.cuffed > 0)
                {
                    ntarget.SetSharedData("Player:AnimData", $"mp_arresting%idle%{49}");
                }
                return;
            }
            catch (Exception)
            {
                Helper.SendNotificationWithoutButton(player, "Ungültiger Spieler!", "error");
            }
        }

        [Command("goto", "Befehl: /goto [Spieler/ID]")]
        public void CMD_Goto(Player player, string target)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Account account = Helper.GetAccountData(player);
                Character character = Helper.GetCharacterData(player);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.ProbeModerator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                Player ntarget = Helper.GetPlayerByNameOrID(target);
                if (ntarget == null || !ntarget.Exists)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Spieler!", "error");
                    return;
                }
                Account account2 = Helper.GetAccountData(ntarget);
                Character character2 = Helper.GetCharacterData(ntarget);
                if (account2 == null || character2 == null)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Spieler!", "error");
                    return;
                }
                if (!Account.IsPlayerLoggedIn(ntarget) || ntarget == player)
                {
                    Helper.SendNotificationWithoutButton(player, "Du kannst dich zu diesem Spieler nicht teleportieren!", "error");
                    return;
                }
                if (account.adminlevel < account2.adminlevel)
                {
                    Helper.SendNotificationWithoutButton(player, "Du kannst dich zu diesem Spieler nicht teleportieren!", "error");
                    return;
                }
                if (ntarget.GetData<bool>("Player:Spectate") == true)
                {
                    Helper.SendNotificationWithoutButton(player, "Du kannst dich jetzt nicht zu dem Spieler teleportieren!", "error", "top-end");
                    return;
                }
                if (player.HasData("Player:MusicBizz"))
                {
                    Helper.SendNotificationWithoutButton(player, "Du musst zuerst die Musik beenden!", "error", "top-end");
                    return;
                }
                if (character.inhouse > -1)
                {
                    character.inhouse = -1;
                    player.SetOwnSharedData("Player:InHouse", -1);
                }
                if (character2.inhouse != -1)
                {
                    House house = House.GetHouseById(character2.inhouse);
                    if (house != null)
                    {
                        player.TriggerEvent("Client:LoadIPL", House.GetInteriorIPL(house.interior));
                        Furniture.UpdateMöbelList(player, House.GetFurnitureForHouse(house.id));
                        player.SetOwnSharedData("Player:InHouse", house.id);
                    }
                }
                character.inhouse = character2.inhouse;
                Helper.SetPlayerPosition(player, new Vector3(ntarget.Position.X + 1f, ntarget.Position.Y, ntarget.Position.Z + 1f));
                player.Dimension = ntarget.Dimension;
                Helper.SendNotificationWithoutButton(player, $"Du hast dich zu { account2.name } teleportiert!", "success", "top-end", 3500);
                return;
            }
            catch (Exception)
            {
                Helper.SendNotificationWithoutButton(player, "Ungültiger Spieler!", "error");
            }
        }

        [Command("gethere", "Befehl: /gethere [Spieler/ID]")]
        public void CMD_Gethere(Player player, string target)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Account account = Helper.GetAccountData(player);
                Character character = Helper.GetCharacterData(player);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.ProbeModerator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                Player ntarget = Helper.GetPlayerByNameOrID(target);
                if (ntarget == null || !ntarget.Exists)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Spieler!", "error");
                    return;
                }
                Account account2 = Helper.GetAccountData(ntarget);
                Character character2 = Helper.GetCharacterData(ntarget);
                if (account2 == null || character2 == null)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Spieler!", "error");
                    return;
                }
                if (!Account.IsPlayerLoggedIn(ntarget) || ntarget == player)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Spieler!", "error");
                    return;
                }
                if (account.adminlevel < account2.adminlevel)
                {
                    Helper.SendNotificationWithoutButton(player, "Du kannst diesen Spieler nicht zu dir teleportieren!", "error");
                    return;
                }
                if (ntarget.GetData<bool>("Player:Spectate") == true)
                {
                    Helper.SendNotificationWithoutButton(player, "Du kannst den Spieler jetzt nicht zu dir teleportieren!", "error", "top-end");
                    return;
                }
                if (ntarget.HasData("Player:MusicBizz"))
                {
                    Helper.SendNotificationWithoutButton(player, "Der Spieler spielt gerade Musik ab!", "error", "top-end");
                    return;
                }
                if (account2.prison > 0)
                {
                    Helper.SendNotificationWithoutButton(player, "Der Spieler befindet sich im Prison!", "error", "top-end");
                    return;
                }
                if (character2.death == true)
                {
                    Helper.SendNotificationWithoutButton(player, "Der Spieler ist Tod!", "error", "top-end");
                    return;
                }
                TempData tempData = Helper.GetCharacterTempData(ntarget);
                if (tempData != null)
                {
                    if (tempData.interiorswitch == true)
                    {
                        tempData.interiorswitch = false;
                        player.TriggerEvent("SwitchHouseInterior", "null", "null", "null", "null", "null", "null", "null", 0);
                    }
                }
                if (character.inhouse != -1)
                {
                    House house = House.GetHouseById(character.inhouse);
                    if (house != null)
                    {
                        ntarget.TriggerEvent("Client:LoadIPL", House.GetInteriorIPL(house.interior));
                        Furniture.UpdateMöbelList(ntarget, House.GetFurnitureForHouse(house.id));
                        ntarget.SetOwnSharedData("Player:InHouse", character.inhouse);
                    }
                }
                character2.inhouse = character.inhouse;
                Helper.SetPlayerPosition(ntarget, new Vector3(player.Position.X + 1f, player.Position.Y, player.Position.Z + 1f));
                ntarget.Dimension = player.Dimension;
                Helper.SendNotificationWithoutButton(player, $"Du hast { account2.name } zu dir teleportiert!", "success", "top-end", 3500);
                Helper.SendNotificationWithoutButton(ntarget, $"{ account.name } hat dich zu sich teleportiert!", "success", "top-end", 3500);
                return;
            }
            catch (Exception)
            {
                Helper.SendNotificationWithoutButton(player, "Ungültiger Spieler!", "error");
            }
        }

        [Command("getcar", "Befehl: /getcar [Fahrzeug-ID]")]
        public void CMD_getcar(Player player, int vehicleid)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Account account = Helper.GetAccountData(player);
                Character character = Helper.GetCharacterData(player);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.ProbeModerator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                Vehicle vehicle = Helper.GetVehicleById(vehicleid);
                if (vehicle == null || vehicle.Dimension == 150)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiges Fahrzeug!", "error");
                    return;
                }
                if (vehicle.HasData("Vehicle:Jacked") && vehicle.GetData<bool>("Vehicle:Jacked") == true)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiges Fahrzeug!", "error");
                    return;
                }
                Player ntarget = (Player)NAPI.Vehicle.GetVehicleDriver(vehicle);
                if (ntarget != null)
                {
                    Account account2 = Helper.GetAccountData(ntarget);
                    Character character2 = Helper.GetCharacterData(ntarget);
                    if (account.adminlevel < account2.adminlevel || character2.inhouse != -1)
                    {
                        Helper.SendNotificationWithoutButton(player, "Du kannst dieses Fahrzeug nicht zu dir teleportieren!", "error");
                        return;
                    }
                }
                vehicle.Dimension = 1;
                NAPI.Task.Run(() =>
                {
                    vehicle.Position = new Vector3(player.Position.X + 1f,
                                                  player.Position.Y,
                                                  player.Position.Z + 1f);
                    vehicle.Dimension = player.Dimension;
                }, delayTime: 215);
                Helper.SendNotificationWithoutButton(player, $"Du hast das Fahrzeug mit der ID { vehicleid } zu dir teleportiert!", "success", "top-end", 3500);
                if (ntarget != null)
                {
                    Helper.SendNotificationWithoutButton(ntarget, $"{ account.name } hat dich zu sich teleportiert!", "success", "top-end", 3500);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CMD_getcar]: " + e.ToString());
            }
            return;
        }

        [Command("getvehicleid", "Befehl: /getvehicleid [Fahrzeug-ID]")]
        public void cmd_getvehicleid(Player player, int id)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Account account = Helper.GetAccountData(player);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.ProbeModerator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                foreach (Cars car in Cars.carList)
                {
                    if (car.vehicleData != null && car.vehicleHandle != null && car.vehicleData.id == id)
                    {
                        Helper.SendNotificationWithoutButton(player, $"Fahrzeug-ID: {car.vehicleHandle.Id}", "info", "top-end", 4500);
                        return;
                    }
                }
                Helper.SendNotificationWithoutButton(player, "Ungültige Fahrzeug-ID!", "error", "top-end", 2250);
                return;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[cmd_getvehicleid]: " + e.ToString());
            }
        }

        [Command("vehiclegarage", "Befehl: /vehiclegarage [Fahrzeug-ID] [Garage]")]
        public void cmd_vehiclegarage(Player player, int id, string garage)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Account account = Helper.GetAccountData(player);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.ProbeModerator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                if (garage == "n/A")
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültige Garage!", "error", "top-end");
                    return;
                }
                foreach (Cars car in Cars.carList)
                {
                    if (car.vehicleData != null && car.vehicleHandle != null && car.vehicleHandle.Id == id)
                    {
                        if (car.vehicleData.garage != "n/A")
                        {
                            Helper.SendNotificationWithoutButton(player, "Das Fahrzeug befindest sich schon in einer Garage!", "error", "center");
                            return;
                        }

                        foreach (Player p in NAPI.Pools.GetAllPlayers())
                        {
                            if (p.Vehicle == car.vehicleHandle)
                            {
                                p.WarpOutOfVehicle();
                            }
                        }

                        car.vehicleData.garage = garage;
                        if (car.vehicleData.garage == "towed-1")
                        {
                            car.vehicleData.towed = Helper.adminSettings.towedcash;
                        }
                        Helper.SetVehicleEngine(car.vehicleHandle, false);
                        DealerShipController.SaveOneVehicleData(car);
                        car.vehicleHandle.Dimension = 150;
                        if (car.vehicleHandle != null)
                        {
                            if (car.vehicleHandle.HasSharedData("Vehicle:Text3D"))
                            {
                                car.vehicleHandle.ResetSharedData("Vehicle:Text3D");
                            }
                            car.vehicleHandle.Delete();
                            car.vehicleHandle = null;
                        }
                        player.Dimension = 0;
                        Helper.SendNotificationWithoutButton(player, "Das Fahrzeug wurde erfolgreich eingeparkt!", "success", "center");
                        return;
                    }
                }
                Helper.SendNotificationWithoutButton(player, "Ungültige Fahrzeug-ID/Dieses Fahrzeug kann in keine Garage eingeparkt werden!", "error", "top-end", 2250);
                return;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[cmd_vehiclegarage]: " + e.ToString());
            }
        }

        [Command("createfactiondoor", "Befehl: /createfactiondoor [Hash] [Fraktion]", Alias = "cfd")]
        public void CMD_createfactiondoor(Player player, string hash, int faction)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Account account = Helper.GetAccountData(player);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Administrator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }

                if (faction <= 0 || faction > Helper.factionList.Count)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültige Fraktion!", "error", "top-end");
                    return;
                }

                Doors checkDoor = DoorsController.GetClosestDoor(player, 0.45f);
                if (checkDoor != null)
                {
                    Helper.SendNotificationWithoutButton(player, "Hier befindet sich schon eine Fraktionstür in der Nähe!", "error", "top-end");
                    return;
                }

                Doors door = new Doors();
                door.id = DoorsController.GetFreeDoorsID();
                door.hash = hash;
                door.posx = player.Position.X;
                door.posy = player.Position.Y;
                door.posz = player.Position.Z;
                door.dimension = 0;
                door.toogle = true;
                door.props = "faction-" + faction;
                door.save = false;

                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                db.Insert(door);

                DoorsController.AddDoor(door, true);

                Helper.SendNotificationWithoutButton(player, "Fraktionstür wurde erstellt!", "success", "top-end");
            }
            catch (Exception)
            {
                Helper.SendNotificationWithoutButton(player, "Fraktionstür konnte nicht erstellt werden!", "error", "top-end");
            }
        }

        [Command("closestdoorhash", "Befehl: /closestdoorhash", Alias = "cdh")]
        public void CMD_closestdoorhash(Player player)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Account account = Helper.GetAccountData(player);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Administrator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                player.TriggerEvent("Client:GetDoorHash");
            }
            catch (Exception)
            {
                Helper.SendNotificationWithoutButton(player, "Ungültiges Objekt in der Nähe!", "error", "top-end");
            }
        }

        [Command("gotodoor", "Befehl: /gotodoor [Tür-ID]")]
        public void CMD_gotodoor(Player player, int doorid)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Account account = Helper.GetAccountData(player);
                Character character = Helper.GetCharacterData(player);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Administrator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                if (doorid <= 0 || doorid > DoorsController.doorsList.Count)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültige Tür!", "error", "top-end");
                    return;
                }
                Doors door = DoorsController.GetDoorByID(doorid);
                if (door == null)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültige Tür!", "error", "top-end");
                    return;
                }
                if (character.inhouse > -1)
                {
                    character.inhouse = -1;
                    player.SetOwnSharedData("Player:InHouse", -1);
                }
                player.Dimension = 0;
                Helper.SetPlayerPosition(player, new Vector3(door.posx, door.posy, door.posz));
                Helper.SendNotificationWithoutButton(player, $"Du hast dich zur Tür ({door.id} - {door.hash}) teleportiert!", "success", "top-end", 3500);
                return;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CMD_gotodoor]: " + e.ToString());
            }
        }

        [Command("createfactionvehicle", "Befehl: /createfactionvehicle [Fahrzeugname] [Farbe 1] [Farbe 2] [Fraktions-ID]", Alias = "cfv")]
        public void cmd_createfactionvehicle(Player player, string vehname, int color1, int color2, int fraktion)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Account account = Helper.GetAccountData(player);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Administrator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                if (player.IsInVehicle)
                {
                    Helper.SendNotificationWithoutButton(player, "Du musst zuerst aus deinem jetzigen Fahrzeug aussteigen!", "error", "top-end");
                    return;
                }
                if (color1 < 0 || color2 < 0 || color1 > 159 || color2 > 159 || fraktion < 0 || fraktion > Helper.factionList.Count)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültige Farbauswahl oder Fraktionsauswahl!", "error", "top-end");
                    return;
                }
                if (vehname.Length >= 3)
                {
                    uint vehash = NAPI.Util.GetHashKey(vehname.Trim());
                    if (vehash <= 0)
                    {
                        Helper.SendNotificationWithoutButton(player, "Ungültiges Fahrzeug!", "error", "top-end");
                        return;
                    }
                }
                else
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiges Fahrzeug!", "error", "top-end");
                    return;
                }
                FactionsModel faction = FactionController.GetFactionById(fraktion);
                if (faction != null)
                {
                    vehname = vehname[0].ToString().ToUpper() + vehname.Substring(1);
                    VehicleData vehicleData = new VehicleData();
                    vehicleData.id = Cars.carList.Count + 1;
                    vehicleData.owner = "faction-" + faction.id;
                    vehicleData.vehiclename = vehname;
                    vehicleData.plate = faction.tag + " " + FactionController.GetFreeFactionPlate(faction.id);
                    vehicleData.fuel = -1;
                    vehicleData.engine = 0;
                    vehicleData.status = 1;
                    Vehicle vehicle = null;
                    string vColor = $"{color1},{color2},-1,-1";
                    vehicleData.color = vColor;
                    vehicleData.position = $"{player.Position.X.ToString(new CultureInfo("en-US"))}|{player.Position.Y.ToString(new CultureInfo("en-US"))}|{player.Position.Z.ToString(new CultureInfo("en-US"))}|{player.Heading.ToString(new CultureInfo("en-US"))}|{player.Dimension}";
                    vehicle = Cars.createNewCar(vehname.ToLower(), player.Position, player.Heading, color1, color2, vehicleData.owner, "n/A", true, false, true, player.Dimension, vehicleData, true);
                    if (vehicle.Class != 13)
                    {
                        vehicleData.tuev = Helper.UnixTimestamp() + (93 * 86400);
                    }
                    else
                    {
                        vehicleData.tuev = -50;
                    }
                    player.SetIntoVehicle(vehicle, (int)VehicleSeat.Driver);
                    Helper.SetVehicleEngine(vehicle, false);
                    Helper.SendNotificationWithoutButton(player, "Fraktionsfahrzeug erfolgreich erstellt!", "success", "top-left");
                    Helper.CreateFactionLog(faction.id, $"{account.name} hat ein neues Fahrzeug ({vehname}) für die Fraktion erstellt!");
                    Helper.CreateAdminLog("adminlog", $"{account.name} hat ein {vehname} für die Fraktion {faction.name} erstellt!", Helper.GetIP(player), account.identifier);
                }
                else
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültige Fraktion!", "error", "top-end");
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[cmd_createfactionvehicle]: " + e.ToString());
            }
        }

        [Command("deletefactionvehicle", "Befehl: /deletefactionvehicle", Alias = "dfv")]
        public void cmd_deletefactionvehicle(Player player)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Account account = Helper.GetAccountData(player);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Administrator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                if (!player.IsInVehicle)
                {
                    Helper.SendNotificationWithoutButton(player, "Du sitzt in keinem Fahrzeug!", "error", "top-end");
                    return;
                }
                foreach (Cars car in Cars.carList)
                {
                    if (car.vehicleHandle != null && car.vehicleHandle == player.Vehicle && car.vehicleData.owner.Contains("faction"))
                    {
                        player.WarpOutOfVehicle();
                        NAPI.Task.Run(() =>
                        {
                            int factionid = Convert.ToInt32(car.vehicleData.owner.Split("-")[1]);
                            Helper.CreateFactionLog(factionid, $"{account.name} hat das Fraktionsfahrzeug {car.vehicleData.vehiclename} - {car.vehicleData.plate} gelöscht!");
                            Helper.CreateAdminLog("adminlog", $"{account.name} hat das Fraktionsfahrzeug {car.vehicleData.vehiclename} - {car.vehicleData.plate} gelöscht!", Helper.GetIP(player), account.identifier);
                            Cars.carList.Remove(car);
                            PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                            db.Delete(car.vehicleData);
                            car.vehicleData = null;
                            car.vehicleHandle.Delete();
                            car.vehicleHandle = null;
                            Helper.SendNotificationWithoutButton(player, "Das Fraktionsfahrzeug wurde gelöscht!", "success", "top-end");
                        }, delayTime: 95);
                        return;
                    }
                }
                Helper.SendNotificationWithoutButton(player, "Ungültiges Fahrzeug!", "error", "top-end");
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[cmd_deletefactionvehicle]: " + e.ToString());
            }
        }

        [Command("changefactionvehiclecolor", "Befehl: /changefactionvehiclecolor [Farbe 1] [Farbe 2]", Alias = "cfvc")]
        public void cmd_changefactionvehiclecolor(Player player, int c1, int c2)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Account account = Helper.GetAccountData(player);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Administrator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                if (!player.IsInVehicle)
                {
                    Helper.SendNotificationWithoutButton(player, "Du sitzt in keinem Fahrzeug!", "error", "top-end");
                    return;
                }
                if (c1 < 0 || c2 < 0 || c1 > 159 || c2 > 159)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültige Farbauswahl!", "error", "top-end");
                    return;
                }
                foreach (Cars car in Cars.carList)
                {
                    if (car.vehicleHandle != null && car.vehicleHandle == player.Vehicle && car.vehicleData.owner.Contains("faction"))
                    {
                        NAPI.Vehicle.SetVehiclePrimaryColor(car.vehicleHandle, c1);
                        NAPI.Vehicle.SetVehicleSecondaryColor(car.vehicleHandle, c2);
                        string color = $"{NAPI.Vehicle.GetVehiclePrimaryColor(car.vehicleHandle)},{NAPI.Vehicle.GetVehicleSecondaryColor(car.vehicleHandle)},{NAPI.Vehicle.GetVehiclePearlescentColor(car.vehicleHandle)},{NAPI.Vehicle.GetVehicleWheelColor(car.vehicleHandle)}";
                        car.vehicleHandle.SetSharedData("Vehicle:Color", color);
                        Helper.SendNotificationWithoutButton(player, "Die Farbe des Fraktionfahrzeuges wurde angepasst!", "success", "top-end");
                    }
                }
                Helper.SendNotificationWithoutButton(player, "Ungültiges Fahrzeug!", "error", "top-end");
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[cmd_changefactionvehiclecolor]: " + e.ToString());
            }
        }

        [Command("checkvehicles", "Befehl: /checkvehicles [Name/ID/Gruppierungsname/Fraktionsname]", GreedyArg = true)]
        public void cmd_checkvehicles(Player player, string target)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Account account = Helper.GetAccountData(player);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.ProbeModerator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                Groups group = GroupsController.GetGroupByName(target);
                if (group == null)
                {
                    FactionsModel factionsModel = FactionController.GetFactionByName(target);
                    if (factionsModel == null)
                    {
                        Player ntarget = Helper.GetPlayerByAccountName(target);
                        if (ntarget == null)
                        {
                            Helper.SendNotificationWithoutButton(player, "Ungültiger Spieler/Ungültige Gruppierung/Ungültige Fraktion!", "error");
                            return;
                        }
                        Account account2 = Helper.GetAccountData(ntarget);
                        Character character = Helper.GetCharacterData(ntarget);
                        if (account2 == null || character == null)
                        {
                            Helper.SendNotificationWithoutButton(player, "Ungültiger Spieler/Ungültige Gruppierung/Ungültige Fraktion!", "error");
                            return;
                        }
                        if (account.adminlevel < account2.adminlevel)
                        {
                            Helper.SendNotificationWithoutButton(player, "Du kannst diesen Spieler nicht auschecken!", "error");
                            return;
                        }
                        Menu.OnStartCars(player, "character", character.id);
                        return;
                    }
                    else
                    {
                        Menu.OnStartCars(player, "faction", factionsModel.id);
                        return;
                    }
                }
                else
                {
                    Menu.OnStartCars(player, "group", group.id);
                    return;
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[cmd_checkvehicles]: " + e.ToString());
            }
        }

        [Command("checkplayer", "Befehl: /checkplayer [Name/ID]", GreedyArg = true)]
        public void cmd_checkplayer(Player player, string target)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Account account = Helper.GetAccountData(player);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.ProbeModerator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                Player ntarget = Helper.GetPlayerByAccountName(target);
                if (ntarget == null)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Spieler!", "error");
                    return;
                }
                Account account2 = Helper.GetAccountData(ntarget);
                Character character = Helper.GetCharacterData(ntarget);
                if (account2 == null || character == null)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Spieler!", "success", "top-end", 4250);
                    return;
                }
                if (account.adminlevel < account2.adminlevel)
                {
                    Helper.SendNotificationWithoutButton(player, "Du kannst diesen Spieler nicht auschecken!", "error");
                    return;
                }
                player.TriggerEvent("Client:PressF2");
                Menu.OnStartStats(ntarget, player);
                return;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[cmd_checkplayer]: " + e.ToString());
            }
        }

        [Command("checkinventory", "Befehl: /checkinventory [Name/ID]", GreedyArg = true)]
        public void cmd_checkinventory(Player player, string target)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Account account = Helper.GetAccountData(player);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.ProbeModerator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                Player ntarget = Helper.GetPlayerByAccountName(target);
                if (ntarget == null)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Spieler!", "error");
                    return;
                }
                Account account2 = Helper.GetAccountData(ntarget);
                Character character = Helper.GetCharacterData(ntarget);
                if (account2 == null || character == null)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Spieler!", "success", "top-end", 4250);
                    return;
                }
                if (account.adminlevel < account2.adminlevel)
                {
                    Helper.SendNotificationWithoutButton(player, "Du kannst diesen Spieler nicht auschecken!", "error");
                    return;
                }
                player.SetData<Player>("Player:GetPlayer", ntarget);
                ItemsController.OnShowInventory(player, 3);
                return;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[cmd_checkinventory]: " + e.ToString());
            }
        }

        [Command("gointocar", "Befehl: /gointocar [Fahrzeug-ID] [Sitz*]")]
        public void CMD_gointocar(Player player, int vehicleid, int seatid = 0)
        {
            if (!Account.IsPlayerLoggedIn(player)) return;
            Account account = Helper.GetAccountData(player);
            Character character = Helper.GetCharacterData(player);
            if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.ProbeModerator))
            {
                Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                return;
            }
            Vehicle vehicle = Helper.GetVehicleById(vehicleid);
            if (vehicle == null || vehicle.Dimension == 150)
            {
                Helper.SendNotificationWithoutButton(player, "Ungültiges Fahrzeug!", "error");
                return;
            }
            if (seatid < 0 || seatid > 3)
            {
                Helper.SendNotificationWithoutButton(player, "Ungültiger Sitz!", "error");
                return;
            }
            Player ntarget = (Player)NAPI.Vehicle.GetVehicleDriver(vehicle);
            if (ntarget != null)
            {
                Helper.SendNotificationWithoutButton(player, "In diesem Fahrzeug sitzt bereits jemand anderes!", "error");
                return;
            }
            if (player.IsInVehicle)
            {
                Helper.SendNotificationWithoutButton(player, "Du musst zuerst aus deinem jetzigen Fahrzeug aussteigen!", "error");
                return;
            }
            player.Dimension = vehicle.Dimension;
            Helper.SetPlayerPosition(player, new Vector3(vehicle.Position.X + 2f, vehicle.Position.Y, vehicle.Position.Z + 2.5f));
            NAPI.Task.Run(() =>
            {
                NAPI.Player.SetPlayerIntoVehicle(player, vehicle, seatid);
            }, delayTime: 425);
            Helper.SendNotificationWithoutButton(player, $"Du hast dich in das Fahrzeug mit der ID { vehicleid } teleportiert!", "success", "top-end", 3500);
            return;
        }

        [Command("vehicleengine", "Befehl: /vehicleengine [Fahrzeug-ID]")]
        public void CMD_vehicleengine(Player player, int vehicleid)
        {
            if (!Account.IsPlayerLoggedIn(player)) return;
            if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Administrator))
            {
                Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                return;
            }
            Vehicle vehicle = Helper.GetVehicleById(vehicleid);
            if (vehicle == null || vehicle.Dimension == 150)
            {
                Helper.SendNotificationWithoutButton(player, "Ungültiges Fahrzeug!", "error");
                return;
            }
            if (NAPI.Vehicle.GetVehicleEngineHealth(vehicle) <= 215)
            {
                Helper.SendNotificationWithoutButton(player, "Das Fahrzeug hat schon einen Motorschaden!", "error");
                return;
            }
            if (vehicle.Class == 13)
            {
                Helper.SendNotificationWithoutButton(player, "Ungültiges Fahrzeug!", "error");
                return;
            }
            Player driver = (Player)NAPI.Vehicle.GetVehicleDriver(vehicle);
            if (driver != null)
            {
                Helper.SendNotificationWithoutButton(driver, "Der Motor des Fahrzeuges macht komische Geräusche ...", "info", "top-left", 3500);
            }
            NAPI.Vehicle.SetVehicleEngineHealth(vehicle, 50);
            vehicle.SetSharedData("Vehicle:Oel", 0);
            Helper.SetVehicleEngine(vehicle, false);
            Helper.SendNotificationWithoutButton(player, $"Du hast dem Fahrzeug einen Motorschaden (ÖL = 0% + Motorschaden) gegeben!", "success", "top-end", 5500);
            return;
        }

        [Command("telexyz", "Befehl: /telexyz  [X] [Y] [Z] [IPL*]")]
        public void CMD_telexyz(Player player, float x, float y, float z, string ipl = "n/A")
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Account account = Helper.GetAccountData(player);
                Character character = Helper.GetCharacterData(player);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Moderator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                if (ipl.Trim() != "n/A")
                {
                    player.TriggerEvent("Client:LoadIPL", ipl.Trim());
                }
                if (character.inhouse > -1)
                {
                    character.inhouse = -1;
                    player.SetOwnSharedData("Player:InHouse", -1);
                }
                NAPI.Task.Run(() =>
                {
                    Vector3 posi = new Vector3(x, y, z + 0.2);
                    Helper.SetPlayerPosition(player, posi);
                    Helper.SendNotificationWithoutButton(player, "Du hast dich zu den Koordinaten teleportiert!", "success");
                }, delayTime: 155);
            }
            catch (Exception)
            {
                Helper.SendNotificationWithoutButton(player, "Ungültige Position!", "error", "top-end");
            }
        }

        [Command("rtune", "Befehl: /rtune", Alias = "ztune")]
        public void CMD_rtunen(Player player)
        {
            if (!Account.IsPlayerLoggedIn(player)) return;
            Account account = Helper.GetAccountData(player);
            if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.HighAdministrator))
            {
                Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                return;
            }
            if (!player.IsInVehicle)
            {
                Helper.SendNotificationWithoutButton(player, "Du sitzt in keinem Fahrzeug!", "error", "top-end");
                return;
            }
            if (player.Vehicle.NumberPlate != "Admin")
            {
                Helper.SendNotificationWithoutButton(player, "Dieser Befehl kann nur bei Adminfahrzeugen verwendet werden!", "error", "top-end");
                return;
            }
            if (player.Vehicle.NumberPlate != "Admin")
            {
                Helper.SendNotificationWithoutButton(player, "Dieser Befehl kann nur bei Adminfahrzeugen verwendet werden!", "error", "top-end");
                return;
            }
            if (player.HasData("Player:TuneCooldown") && player.GetData<int>("Player:TuneCooldown") > 0)
            {
                if (Helper.UnixTimestamp() < player.GetData<int>("Player:TuneCooldown"))
                {
                    Helper.SendNotificationWithoutButton(player, "Du musst noch kurz warten, bevor du diesen Befehl wieder ausführen kannst!", "error", "top-end");
                    return;
                }
            }
            player.SetData<int>("Player:TuneCooldown", Helper.UnixTimestamp() + (6));
            player.TriggerEvent("Client:RandomTuning");
            Helper.SendNotificationWithoutButton(player, "Das Fahrzeug wurde zufällig getuned!", "success", "top-end");
            player.TriggerEvent("Client:PlaySoundSuccessExtra");
            NAPI.Task.Run(() =>
            {
                string color = $"{NAPI.Vehicle.GetVehiclePrimaryColor(player.Vehicle)},{NAPI.Vehicle.GetVehicleSecondaryColor(player.Vehicle)},{NAPI.Vehicle.GetVehiclePearlescentColor(player.Vehicle)},{NAPI.Vehicle.GetVehicleWheelColor(player.Vehicle)}";
                player.Vehicle.SetSharedData("Vehicle:Color", color);
            }, delayTime: 215);
            return;
        }

        [Command("startfire", "Befehl: /startfire [Einsatz*]", GreedyArg = true)]
        public void CMD_startfire(Player player, string name = "n/A")
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.HighAdministrator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                if (FireController.startFire == true)
                {
                    FireController.FiresCompleteEvent(player);
                    Helper.SendNotificationWithoutButton(player, "Das aktuelle Feuer wurde gelöscht!", "success", "top-end");
                    return;
                }
                if (name.Length < 3 || name.Length > 35)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Einsatz!", "error", "top-end");
                    return;
                }
                string ort = FireController.BuildFire(player, name);
                if (ort == "n/A")
                {
                    Helper.SendNotificationWithoutButton(player, "Das Feuer konnte nicht erstellt werden!", "error", "top-end");
                    return;
                }
                Helper.SendNotificationWithoutButton(player, $"Neues Feuer ({ort}) wurde gestartet!", "success", "top-left");
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CMD_startfire]: " + e.ToString());
            }
            return;
        }

        [Command("startlotto", "Befehl: /startlotto")]
        public void CMD_startlotto(Player player)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.HighAdministrator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                Events.StartLotto();
                Helper.SendNotificationWithoutButton(player, "Neue Lottoausschüttung gestartet!", "success", "top-left");
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CMD_startlotto]: " + e.ToString());
            }
            return;
        }

        [Command("fixveh", "Befehl: /fixveh")]
        public void CMD_fixveh(Player player)
        {
            if (!Account.IsPlayerLoggedIn(player)) return;
            if (player.GetOwnSharedData<bool>("Player:Testmodus") == false && !Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Moderator))
            {
                Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                return;
            }
            if (!player.IsInVehicle)
            {
                Helper.SendNotificationWithoutButton(player, "Du sitzt in keinem Fahrzeug!", "error", "top-end");
                return;
            }
            NAPI.Vehicle.RepairVehicle(NAPI.Player.GetPlayerVehicle(player));
            string[] vehicleArray = new string[7];
            vehicleArray = NAPI.Player.GetPlayerVehicle(player).GetSharedData<string>("Vehicle:Sync").Split(",");
            NAPI.Player.GetPlayerVehicle(player).SetSharedData("Vehicle:Sync", $"{vehicleArray[0]},{vehicleArray[1]},0,0,0,{vehicleArray[5]},{vehicleArray[6]}");
            if (player.Vehicle.GetSharedData<int>("Vehicle:Oel") <= 0)
            {
                player.Vehicle.SetSharedData("Vehicle:Oel", 100);
            }
            Helper.SendNotificationWithoutButton(player, "Das Fahrzeug wurde repariert!", "success", "top-end");
            return;
        }

        [Command("fillveh", "Befehl: /fillveh")]
        public void CMD_fillveh(Player player)
        {
            if (!Account.IsPlayerLoggedIn(player)) return;
            if (player.GetOwnSharedData<bool>("Player:Testmodus") == false && !Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Moderator))
            {
                Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                return;
            }
            if (!player.IsInVehicle)
            {
                Helper.SendNotificationWithoutButton(player, "Du sitzt in keinem Fahrzeug!", "error", "top-end");
                return;
            }
            if (player.Vehicle.GetSharedData<float>("Vehicle:Fuel") >= player.Vehicle.GetSharedData<float>("Vehicle:MaxFuel"))
            {
                Helper.SendNotificationWithoutButton(player, "Der Tank des Fahrzeuges ist bereits voll!", "error", "top-end");
                return;
            }
            player.Vehicle.SetSharedData("Vehicle:Fuel", player.Vehicle.GetSharedData<float>("Vehicle:MaxFuel"));
            player.Vehicle.SetSharedData("Vehicle:Oel", 100);
            player.Vehicle.SetSharedData("Vehicle:Battery", 100);
            Helper.SendNotificationWithoutButton(player, "Der Tank des Fahrzeuges wurde befüllt!", "success", "top-end");
            return;
        }

        [Command("fillveh2", "Befehl: /fillveh2")]
        public void CMD_fillveh2(Player player)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Account account = Helper.GetAccountData(player);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Moderator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                if (!player.IsInVehicle)
                {
                    Helper.SendNotificationWithoutButton(player, "Du sitzt in keinem Fahrzeug!", "error", "top-end");
                    return;
                }
                player.Vehicle.SetSharedData("Vehicle:Fuel", player.Vehicle.GetSharedData<float>("Vehicle:MaxFuel") / 2);
                player.Vehicle.SetSharedData("Vehicle:Oel", player.Vehicle.GetSharedData<int>("Vehicle:Oel") / 2);
                player.Vehicle.SetSharedData("Vehicle:Battery", player.Vehicle.GetSharedData<int>("Vehicle:Battery") / 2);
                Helper.SendNotificationWithoutButton(player, "Der Tank des Fahrzeuges wurde halbiert!", "success", "top-end");
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CMD_fillveh2]: " + e.ToString());
            }
            return;
        }

        [Command("loadipl", "Befehl: /loadip [IPL]")]
        public void CMD_loadipl(Player player, string ipl)
        {
            if (!Account.IsPlayerLoggedIn(player)) return;
            Account account = Helper.GetAccountData(player);
            if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Moderator))
            {
                Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                return;
            }
            if (ipl.Trim() != "n/A")
            {
                player.TriggerEvent("Client:LoadIPL", ipl.Trim());
            }
            NAPI.Task.Run(() =>
            {
                Helper.SendNotificationWithoutButton(player, "IPL geladen!", "success", "top-end");
            }, delayTime: 155);
            return;
        }

        [Command("veh", "Befehl: /veh [Fahrzeugname] [Farbe 1] [Farbe 2]", Alias = "car")]
        public void cmd_veh(Player player, string vehname = "nrg500sa", int color1 = 1, int color2 = 1)
        {
            try
            {
                Account account = Helper.GetAccountData(player);
                TempData tempData = Helper.GetCharacterTempData(player);
                if (tempData.adminvehicle == null)
                {
                    if (player.GetOwnSharedData<bool>("Player:Testmodus") == false && !Account.IsAdminOnDuty(player, (int)Account.AdminRanks.ProbeModerator))
                    {
                        Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                        return;
                    }
                    if (player.IsInVehicle)
                    {
                        Helper.SendNotificationWithoutButton(player, "Du musst zuerst aus deinem jetzigen Fahrzeug aussteigen!", "error", "top-end");
                        return;
                    }
                    if (color1 < 0 || color2 < 0 || color1 > 159 || color2 > 159)
                    {
                        Helper.SendNotificationWithoutButton(player, "Ungültige Farbauswahl!", "error", "top-end");
                        return;
                    }
                    if (vehname.Length >= 3)
                    {
                        uint vehash = NAPI.Util.GetHashKey(vehname.Trim());
                        if (vehash <= 0)
                        {
                            Helper.SendNotificationWithoutButton(player, "Ungültiges Fahrzeug!", "error", "top-end");
                            return;
                        }
                    }
                    else
                    {
                        Helper.SendNotificationWithoutButton(player, "Ungültiges Fahrzeug!", "error", "top-end");
                        return;
                    }
                    tempData.adminvehicle = Cars.createNewCar(vehname, new Vector3(player.Position.X, player.Position.Y, player.Position.Z - 0.1), player.Heading, color1, color2, "Admin", "Admin", false, true, false, player.Dimension);
                    NAPI.Task.Run(() =>
                    {
                        if (tempData.adminvehicle != null)
                        {
                            player.SetIntoVehicle(tempData.adminvehicle, (int)VehicleSeat.Driver);
                        }
                    }, delayTime: 315);
                    Helper.CreateAdminLog($"vehlog", account.name + " hat ein/e " + vehname + " gespawned!");
                    Helper.SendNotificationWithoutButton(player, "Adminfahrzeug gespawned!", "success");
                }
                else
                {
                    if (tempData.adminvehicle != null && player.IsInVehicle && player.Vehicle == tempData.adminvehicle && Helper.IsTrailerAttached(player))
                    {
                        Helper.SendNotificationWithoutButton(player, "Bring zuerst den Anhänger wieder zurück!", "error", "top-end");
                        return;
                    }
                    if (tempData.adminvehicle.HasData("Vehicle:JackedObject"))
                    {
                        tempData.adminvehicle.SetData<bool>("Vehicle:Jacked", false);
                        tempData.adminvehicle.ResetData("Vehicle:Jacked");
                        tempData.adminvehicle.GetData<GTANetworkAPI.Object>("Vehicle:JackedObject").Delete();
                        tempData.adminvehicle.ResetData("Vehicle:JackedObject");
                    }
                    string[] vehicleArray = new string[7];
                    vehicleArray = tempData.adminvehicle.GetSharedData<string>("Vehicle:Sync").Split(",");
                    tempData.adminvehicle.SetSharedData("Vehicle:Sync", $"0,0,{vehicleArray[2]},{vehicleArray[3]},{vehicleArray[4]},0");
                    Helper.SetVehicleEngine(tempData.adminvehicle, false);
                    tempData.adminvehicle.Delete();
                    tempData.adminvehicle = null;
                    Helper.SendNotificationWithoutButton(player, "Adminfahrzeug gelöscht!", "success");
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[cmd_veh]: " + e.ToString());
            }
        }

        [Command("veh2", "Befehl: /veh2 [Fahrzeugname] [Farbe 1*] [Farbe 2*] [Kennzeichen*]", Alias = "car2")]
        public void cmd_veh2(Player player, string vehname = "nrg500sa", int color1 = 1, int color2 = 1, string placeholder = "Admin")
        {
            try
            {
                Account account = Helper.GetAccountData(player);
                TempData tempData = Helper.GetCharacterTempData(player);

                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.ProbeModerator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                if (player.IsInVehicle)
                {
                    Helper.SendNotificationWithoutButton(player, "Du musst zuerst aus deinem jetzigen Fahrzeug aussteigen!", "error", "top-end");
                    return;
                }
                if (color1 < 0 || color2 < 0 || color1 > 159 || color2 > 159)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültige Farbauswahl!", "error", "top-end");
                    return;
                }
                if (placeholder.Length <= 0 || placeholder.Length > 10)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiges Kennzeichen!", "error", "top-end");
                    return;
                }
                if (vehname.Length >= 3)
                {
                    uint vehash = NAPI.Util.GetHashKey(vehname.Trim());
                    if (vehash <= 0)
                    {
                        Helper.SendNotificationWithoutButton(player, "Ungültiges Fahrzeug!", "error", "top-end");
                        return;
                    }
                }
                else
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiges Fahrzeug!", "error", "top-end");
                    return;
                }
                Vehicle tempVehicle = null;
                tempVehicle = Cars.createNewCar(vehname, new Vector3(player.Position.X + 2.15, player.Position.Y + 2.15, player.Position.Z - 0.1), player.Heading, color1, color2, placeholder, placeholder, false, true, false, player.Dimension);
                tempVehicle.Dimension = player.Dimension;
                adminVehicles.Add(tempVehicle);
                Helper.CreateAdminLog($"vehlog", account.name + " hat ein/e " + vehname + " gespawned!");
                Helper.SendNotificationWithoutButton(player, "Adminfahrzeug (2) gespawned!", "success");
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[cmd_veh2]: " + e.ToString());
            }
        }

        [Command("deleteallveh2", "Befehl: /deleteallveh2", Alias = "dav2")]
        public void cmd_deleteallveh2(Player player)
        {
            try
            {
                Account account = Helper.GetAccountData(player);
                TempData tempData = Helper.GetCharacterTempData(player);

                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Administrator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                if (adminVehicles.Count <= 0)
                {
                    Helper.SendNotificationWithoutButton(player, "Es wurden keine Adminfahrzeuge erstellt!", "error", "top-end");
                    return;
                }
                foreach (Vehicle adminVehicle in adminVehicles)
                {
                    if (adminVehicle != null)
                    {
                        if (NAPI.Vehicle.GetVehicleDriver(adminVehicle) != null)
                        {
                            NAPI.Player.WarpPlayerOutOfVehicle((Player)NAPI.Vehicle.GetVehicleDriver(adminVehicle));
                        }
                        NAPI.Task.Run(() =>
                        {
                            if (adminVehicle.HasData("Vehicle:JackedObject"))
                            {
                                adminVehicle.SetData<bool>("Vehicle:Jacked", false);
                                adminVehicle.ResetData("Vehicle:Jacked");
                                adminVehicle.GetData<GTANetworkAPI.Object>("Vehicle:JackedObject").Delete();
                                adminVehicle.ResetData("Vehicle:JackedObject");
                            }
                            string[] vehicleArray = new string[7];
                            vehicleArray = adminVehicle.GetSharedData<string>("Vehicle:Sync").Split(",");
                            adminVehicle.SetSharedData("Vehicle:Sync", $"0,0,{vehicleArray[2]},{vehicleArray[3]},{vehicleArray[4]},0");
                            Helper.SetVehicleEngine(adminVehicle, false);
                            adminVehicles.Remove(adminVehicle);
                            adminVehicle.Delete();
                        }, delayTime: 115);
                    }
                }
                Helper.CreateAdminLog($"vehlog", account.name + " hat alle Adminfahrzeuge gelöscht!");
                Helper.SendNotificationWithoutButton(player, "Du hast alle Adminfahrzeuge gelöscht!", "success");
                Helper.SendAdminMessage2(account.name + " hat alle Adminfahrzeuge gelöscht!", 1, false);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[cmd_deleteallveh2]: " + e.ToString());
            }
        }

        [Command("save", "Befehl: /save [Position]", GreedyArg = true)]
        public void CMD_save(Player player, string position)
        {
            if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Moderator))
            {
                Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                return;
            }
            string status = (player.IsInVehicle) ? "Im Fahrzeug" : "Zu Fuß";
            Vector3 pos = (player.IsInVehicle) ? player.Vehicle.Position : player.Position;
            Vector3 rot = (player.IsInVehicle) ? player.Vehicle.Rotation : player.Rotation;

            string message =
            $"{status} -> {position}: {pos.X.ToString(new CultureInfo("en-US")):N3}, {pos.Y.ToString(new CultureInfo("en-US")):N3}, {pos.Z.ToString(new CultureInfo("en-US")):N3}, {rot.X.ToString(new CultureInfo("en-US")):N3}, {rot.Y.ToString(new CultureInfo("en-US")):N3}, {rot.Z.ToString(new CultureInfo("en-US")):N3} ({(player.Heading - 90).ToString(new CultureInfo("en-US")):N3})";

            Helper.SendChatMessage(player, message);

            using (StreamWriter file = new StreamWriter(@"./serverdata/savedpositions.txt", true))
            {
                file.WriteLine(message);
            }
            Helper.SendNotificationWithoutButton(player, "Position gespeichert!", "success", "top-end");
        }

        [Command("adminduty", "Befehl: /adminduty", Alias = "aduty")]
        public void CMD_adminduty(Player player)
        {
            try
            {
                Account account = Helper.GetAccountData(player);
                TempData tempData = Helper.GetCharacterTempData(player);
                Character character = Helper.GetCharacterData(player);
                if (!Account.IsAdmin(player, (int)Account.AdminRanks.Moderator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                if (tempData.adminduty == false)
                {
                    if (player.GetData<bool>("Player:AdminDuty") == true)
                    {
                        player.TriggerEvent("Client:ResetTabCD");
                        tempData.adminduty = true;
                        Helper.SendNotificationWithoutButton(player, "Der Adminlogin war erfolgreich!", "success", "top-end");
                        NAPI.Data.SetEntitySharedData(player, "Player:AdminLogin", 1);
                        player.SetData<int>("Player:OldHealth", NAPI.Player.GetPlayerHealth(player));
                        Helper.SetPlayerHealth(player, 100);
                        player.SetSharedData("Player:Adminsettings", "1,0,0");
                        JObject obj = JObject.Parse(character.json);
                        CharacterController.SetCharacterCloths(player, obj, character.clothing);
                        NAPI.Player.SetPlayerCurrentWeapon(player, WeaponHash.Unarmed);
                        return;
                    }
                    player.TriggerEvent("Client:PlayerFreeze", true);
                    player.TriggerEvent("Client:CallInput", "Adminlogin", "Bitte das Adminpasswort eingeben, alle Interaktionen werden geloggt!", "password", "**********", 15, "Adminlogin");
                }
                else
                {
                    if (player.GetData<bool>("Player:Spectate") == true)
                    {
                        Helper.SendNotificationWithoutButton(player, "Du musst zuerst die Beobachtung beenden!", "error", "top-end");
                        return;
                    }
                    if (player.GetData<bool>("Player:DevModus") == true)
                    {
                        Helper.SendNotificationWithoutButton(player, "Du musst zuerst den Devmodus beenden!", "error", "top-end");
                        return;
                    }
                    player.TriggerEvent("Client:ResetTabCD");
                    player.SetOwnSharedData("Player:Funmodus", false);
                    Helper.SendNotificationWithoutButton(player, "Admindienst beendet!", "success", "top-end");
                    NAPI.Player.SetPlayerCurrentWeapon(player, WeaponHash.Unarmed);
                    tempData.adminduty = false;
                    if (tempData.adminvehicle != null)
                    {
                        tempData.adminvehicle.Delete();
                        tempData.adminvehicle = null;
                    }
                    if (player.GetData<int>("Player:OldHealth") >= 25)
                    {
                        Helper.SetPlayerHealth(player, player.GetData<int>("Player:OldHealth"));
                    }
                    else
                    {
                        Helper.SetPlayerHealth(player, 25);
                    }
                    player.SetSharedData("Player:Adminsettings", "0,0,0");
                    NAPI.Data.SetEntitySharedData(player, "Player:AdminLogin", 0);
                    JObject obj = null;
                    if (character.factionduty == false)
                    {
                        obj = JObject.Parse(character.json);
                        CharacterController.SetCharacterCloths(player, obj, character.clothing);
                    }
                    else
                    {
                        obj = JObject.Parse(character.dutyjson);
                        CharacterController.SetCharacterCloths(player, obj, character.clothing);
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CMD_adminduty]: " + e.ToString());
            }
        }

        [Command("screenshot", "Befehl: /screenshot [Spieler/ID] [Screenshotname] [Nach Discord senden = Ja/Nein]", Alias = "takepicture")]
        public void CMD_takepicture(Player player, string target, string screenname, string discord = "nein")
        {
            try
            {
                Account account = Helper.GetAccountData(player);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.HighAdministrator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                Player ntarget = Helper.GetPlayerByNameOrID(target);
                if (ntarget == null || !Account.IsPlayerLoggedIn(ntarget))
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Spieler!", "error", "top-end");
                    return;
                }
                if (screenname.Length < 3 || screenname.Length > 35)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Screenshotname", "error", "top-end");
                    return;
                }
                Account account2 = Helper.GetAccountData(ntarget);
                if (!Account.IsPlayerLoggedIn(ntarget))
                {
                    Helper.SendNotificationWithoutButton(player, "Du kannst zu diesem Spieler kein Screenshot anfertigen!", "error");
                    return;
                }
                if (discord.ToLower() == "ja")
                {
                    ntarget.SetData<string>("Player:DiscordUpload", screenname);
                }
                else
                {
                    ntarget.SetData<string>("Player:DiscordUpload", "");
                }
                Helper.SendNotificationWithoutButton(player, "Screenshot wurde erstellt und kann im UCP eingesehen werden!", "success", "top-end");
                ntarget.TriggerEvent("Client:TakePicture", "Admin-" + screenname, 0);
                Helper.CreateAdminLog($"adminlog", account.name + " hat ein Screenshot von " + account2.name + " gemacht (" + screenname + ")!");
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CMD_takepicture]: " + e.ToString());
            }
        }

        [Command("devmodus", "Befehl: /devmodus")]
        public void CMD_devmodus(Player player)
        {
            try
            {
                Account account = Helper.GetAccountData(player);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.HighAdministrator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                if (account.prison > 0 || player.IsInVehicle)
                {
                    Helper.SendNotificationWithoutButton(player, "Du kannst den Devmodus jetzt nicht aktivieren!", "error", "top-end");
                    return;
                }
                string[] adminSettings = new string[3];
                adminSettings = player.GetSharedData<string>("Player:Adminsettings").Split(",");
                player.SetOwnSharedData("Player:DevModus", !player.GetOwnSharedData<bool>("Player:DevModus"));
                if (player.GetOwnSharedData<bool>("Player:DevModus"))
                {
                    if (player.GetData<bool>("Player:Spectate") == true)
                    {
                        Helper.SendNotificationWithoutButton(player, "Du kannst diesen Befehl jetzt nicht benutzen!", "error", "top-end");
                        return;
                    }
                    Helper.SendNotificationWithoutButton(player, "Devmodus wurde aktiviert!", "success", "top-end");
                    player.SetSharedData("Player:Adminsettings", $"{adminSettings[0]},1,0");
                }
                else
                {
                    Helper.SendNotificationWithoutButton(player, "Devmodus wurde deaktiviert!", "success", "top-end");
                    player.SetSharedData("Player:Adminsettings", $"{adminSettings[0]},0,0");
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CMD_devmodus]: " + e.ToString());
            }
        }

        [Command("invisible", "Befehl: /invisible")]
        public void CMD_invisible(Player player)
        {
            try
            {
                Account account = Helper.GetAccountData(player);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Administrator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                if (account.prison > 0)
                {
                    Helper.SendNotificationWithoutButton(player, "Du kannst den Devmodus jetzt nicht aktivieren!", "error", "top-end");
                    return;
                }
                if (player.GetOwnSharedData<bool>("Player:DevModus") == true)
                {
                    Helper.SendNotificationWithoutButton(player, "Du musst zuerst den Devmodus deaktivieren!", "error", "top-end");
                    return;
                }
                string[] adminSettings = new string[3];
                adminSettings = player.GetSharedData<string>("Player:Adminsettings").Split(",");
                if (adminSettings[1] == "0")
                {
                    if (player.GetData<bool>("Player:Spectate") == true || player.GetOwnSharedData<bool>("Player:DevModus") == true)
                    {
                        Helper.SendNotificationWithoutButton(player, "Du kannst dich jetzt nicht unsichtbar machen!", "error", "top-end");
                        return;
                    }
                    adminSettings[1] = "1";
                    Helper.SendNotificationWithoutButton(player, "Du bist jetzt unsichtbar!", "success", "top-end");
                }
                else
                {
                    adminSettings[1] = "0";
                    Helper.SendNotificationWithoutButton(player, "Du bist nichtmehr unsichtbar!", "success", "top-end");
                }
                NAPI.Player.SetPlayerCurrentWeapon(player, WeaponHash.Unarmed);
                player.SetSharedData("Player:Adminsettings", $"{adminSettings[0]},{adminSettings[1]},0");
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CMD_invisible]: " + e.ToString());
            }
        }

        [Command("sonic", "Befehl: /sonic")]
        public void CMD_sonic(Player player)
        {
            try
            {
                Account account = Helper.GetAccountData(player);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Administrator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                if (account.prison > 0)
                {
                    Helper.SendNotificationWithoutButton(player, "Du kannst den Sonicmodus jetzt nicht aktivieren!", "error", "top-end");
                    return;
                }
                if (player.HasData("Player:Sonic"))
                {
                    player.ResetData("Player:Sonic");
                    player.TriggerEvent("Client:Sonic", 1.0f);
                    Helper.SendNotificationWithoutButton(player, "Sonicmodus deaktiviert!", "success", "top-end");
                }
                else
                {
                    player.SetData<bool>("Player:Sonic", true);
                    player.TriggerEvent("Client:Sonic", 1.49f);
                    Helper.SendNotificationWithoutButton(player, "Sonicmodus aktiviert!", "success", "top-end");
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CMD_sonic]: " + e.ToString());
            }
        }

        [Command("nametag", "Befehl: /nametag")]
        public void CMD_nametag(Player player)
        {
            try
            {
                Account account = Helper.GetAccountData(player);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.ProbeModerator))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                if (account.prison > 0)
                {
                    Helper.SendNotificationWithoutButton(player, "Du kannst dein Nametag jetzt nicht verstecken!", "error", "top-end");
                    return;
                }
                if (player.GetOwnSharedData<bool>("Player:DevModus") == true)
                {
                    Helper.SendNotificationWithoutButton(player, "Du musst zuerst den Devmodus deaktivieren!", "error", "top-end");
                    return;
                }
                string[] adminSettings = new string[3];
                adminSettings = player.GetSharedData<string>("Player:Adminsettings").Split(",");
                if (adminSettings[2] == "0")
                {
                    if (adminSettings[1] == "1" || player.GetData<bool>("Player:Spectate") == true)
                    {
                        Helper.SendNotificationWithoutButton(player, "Du kannst dein Nametag jetzt nicht verstecken!", "error", "top-end");
                        return;
                    }
                    adminSettings[2] = "1";
                    Helper.SendNotificationWithoutButton(player, "Dein Nametag wurde versteckt!", "success", "top-end");
                }
                else
                {
                    adminSettings[2] = "0";
                    Helper.SendNotificationWithoutButton(player, "Dein Nametag ist wieder sichtbar", "success", "top-end");
                }
                player.SetSharedData("Player:Adminsettings", $"{adminSettings[0]},{adminSettings[1]},{adminSettings[2]}");
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CMD_nametag]: " + e.ToString());
            }
        }

        [Command("soundviewer", "Befehl: /soundviewer")]
        public void CMD_soundviewer(Player player)
        {
            if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.HighAdministrator))
            {
                Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                return;
            }
            player.TriggerEvent("Client:ShowSoundViewer");
        }

        [Command("funmodus", "Befehl: /funmodus")]
        public void CMD_funmodus(Player player)
        {
            if (player.GetOwnSharedData<bool>("Player:Testmodus") == false && !Account.IsAdminOnDuty(player, (int)Account.AdminRanks.HighAdministrator))
            {
                Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                return;
            }
            player.SetOwnSharedData("Player:Funmodus", !player.GetOwnSharedData<bool>("Player:Funmodus"));
            if (player.GetOwnSharedData<bool>("Player:Funmodus"))
            {
                Helper.SendNotificationWithoutButton(player, "Funmodus wurde aktiviert!", "success", "top-end");
            }
            else
            {
                Helper.SendNotificationWithoutButton(player, "Funmodus wurde deaktiviert!", "success", "top-end");
            }
        }

        [Command("testmodus", "Befehl: /testmodus")]
        public void CMD_testmodus(Player player)
        {
            player.SetOwnSharedData("Player:Testmodus", !player.GetOwnSharedData<bool>("Player:Testmodus"));
            if (player.GetOwnSharedData<bool>("Player:Testmodus"))
            {
                Helper.SendNotificationWithoutButton(player, "Testmodus wurde aktiviert!", "success", "top-end");
            }
            else
            {
                Helper.SendNotificationWithoutButton(player, "Testmodus wurde deaktiviert!", "success", "top-end");
            }
        }

        [Command("restart", "Befehl: /restart")]
        public void CMD_restart(Player player)
        {
            try
            {
                Account account = Helper.GetAccountData(player);
                if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Manager))
                {
                    Helper.SendNotificationWithoutButton(player, "Unzureichende Adminrechte!", "error", "top-end");
                    return;
                }
                Helper.CreateAdminLog($"adminlog", account.name + " hat den Server neugestartet!");
                Helper.SendNotificationWithoutButton(player, "Server wird neugestartet ...", "success", "top-end");
                Helper.SendAdminMessage2($"{account.name} hat den Server neugestartet, Neustart in ca. 10 Sekunden ...", 1, true);
                NAPI.Task.Run(() =>
                {
                    Helper.SendAdminMessage3($"Server wird in ca. 10 Sekunden neugestartet ...");
                }, delayTime: 500);
                NAPI.Task.Run(() =>
                {
                    Events.OnResourceStop();
                }, delayTime: 2300);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CMD_restart]: " + e.ToString());
            }
        }

        //Hausbefehle
        [Command("möbelmodus", "Befehl: /möbelmodus", Alias = "moebelmodus")]
        public void CMD_moebelmodus(Player player)
        {
            if (!Account.IsPlayerLoggedIn(player)) return;
            TempData tempData = Helper.GetCharacterTempData(player);
            Character character = Helper.GetCharacterData(player);
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
            if (house != null && House.HasPlayerHouseKey(player, house.id))
            {
                player.TriggerEvent("Client:FurnitureSettings", "moebelmodus", 1);
                Furniture.UpdateMöbelList(player, House.GetFurnitureForHouse(house.id));
            }
            else
            {
                Helper.SendNotificationWithoutButton(player, "Du bist nicht in oder in der Nähe von einem deiner Häuser!", "error", "top-end");
            }
        }

        [Command("position", "Befehl: /position [Möbel-ID] [X] [Y] [Z]")]
        public void CMD_position(Player player, int id, float x, float y, float z)
        {
            if (!Account.IsPlayerLoggedIn(player)) return;
            TempData tempData = Helper.GetCharacterTempData(player);
            Character character = Helper.GetCharacterData(player);
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
            if (house != null && House.HasPlayerHouseKey(player, house.id))
            {
                if (Furniture.CheckOwnFurnitureById(id, house.id))
                {
                    FurnitureSetHouse furniture = Furniture.GetFurnitureById(id, house.id);
                    furniture.objectHandle.Position = new Vector3(x, y, z);
                    furniture.position = $"{furniture.objectHandle.Position.X}|{furniture.objectHandle.Position.Y}|{furniture.objectHandle.Position.Z}|{furniture.objectHandle.Rotation.X}|{furniture.objectHandle.Rotation.Y}|{furniture.objectHandle.Rotation.Z}|{furniture.objectHandle.Dimension}";
                    Helper.SendNotificationWithoutButton(player, "Möbelposition angepasst!", "success", "top-end");
                    Furniture.UpdateMöbelList(player, House.GetFurnitureForHouse(house.id), true);

                }
                else
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiges Möbelstück!", "error", "top-end");
                }
            }
            else
            {
                Helper.SendNotificationWithoutButton(player, "Du bist nicht in oder in der Nähe von einem deiner Häuser1!", "error", "top-end");
            }
        }

        [Command("rotation", "Befehl: /rotation [Möbel-ID] [RX] [RY] [RZ]")]
        public void CMD_rotation(Player player, int id, float x, float y, float z)
        {
            if (!Account.IsPlayerLoggedIn(player)) return;
            TempData tempData = Helper.GetCharacterTempData(player);
            Character character = Helper.GetCharacterData(player);
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
            if (house != null && House.HasPlayerHouseKey(player, house.id))
            {
                if (Furniture.CheckOwnFurnitureById(id, house.id))
                {
                    FurnitureSetHouse furniture = Furniture.GetFurnitureById(id, house.id);
                    furniture.objectHandle.Rotation = new Vector3(x, y, z);
                    furniture.position = $"{furniture.objectHandle.Position.X}|{furniture.objectHandle.Position.Y}|{furniture.objectHandle.Position.Z}|{furniture.objectHandle.Rotation.X}|{furniture.objectHandle.Rotation.Y}|{furniture.objectHandle.Rotation.Z}|{furniture.objectHandle.Dimension}";
                    Helper.SendNotificationWithoutButton(player, "Möbelrotation angepasst!", "success", "top-end");
                    Furniture.UpdateMöbelList(player, House.GetFurnitureForHouse(house.id), true);

                }
                else
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiges Möbelstück!", "error", "top-end");
                }
            }
            else
            {
                Helper.SendNotificationWithoutButton(player, "Du bist nicht in oder in der Nähe von einem deiner Häuser2!", "error", "top-end");
            }
        }

        [Command("möbel", "Befehl: /möbel [Möbel-ID]", Alias = "moebel")]
        public static void cmd_moebel(Player player, int moebelid = -1)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                TempData tempData = Helper.GetCharacterTempData(player);
                Character character = Helper.GetCharacterData(player);
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
                if (house != null && House.HasPlayerHouseKey(player, house.id))
                {
                    if (moebelid == -1)
                    {
                        if (tempData.editfurniture == true)
                        {
                            Helper.SendNotificationWithoutButton(player, "Du bewegst gerade schon ein anderes Möbelstück!", "error", "top-end");
                            return;
                        }
                        Menu.OnStartMoebel(player, true);
                    }
                    else
                    {
                        if (tempData.editfurniture == true)
                        {
                            Helper.SendNotificationWithoutButton(player, "Du bewegst gerade schon ein anderes Möbelstück!", "error", "top-end");
                            return;
                        }
                        Furniture.OnFurnitureSettings(player, "move", moebelid, true);
                    }
                }
                else
                {
                    Helper.SendNotificationWithoutButton(player, "Du bist nicht in oder in der Nähe von einem deiner Häuser!", "error", "top-end");
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[cmd_moebel]: " + e.ToString());
            }
        }

        //Sonstige Befehle
        [RemoteEvent("Server:PlayAnimation")]
        [Command("animation", "Befehl: /animation [Name]", Alias = "a")]
        public static void cmd_animation(Player player, string name, bool hidemessage = false)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Animations animation = null;
                TempData tempData = Helper.GetCharacterTempData(player);
                if (tempData == null || tempData.cuffed != 0) return;
                if (player.HasData("Player:Mikrofon") || player.HasData("Player:Filmkamera")) return;
                if (name == "radio2" && hidemessage == true)
                {
                    player.SetSharedData("Player:AnimData", $"random@arrests%generic_radio_chatter%50");
                    return;
                }
                foreach (Animations anim in Helper.animList)
                {
                    if (anim.name.ToLower() == name.ToLower())
                    {
                        animation = anim;
                        break;
                    }
                }
                if (animation == null)
                {
                    if (hidemessage == false)
                    {
                        Helper.SendNotificationWithoutButton(player, "Diese Animation existiert nicht!", "error", "top-left", 1500);
                    }
                    return;
                }
                if (player.HasData("Player:PlayCustomAnimation"))
                {
                    if (hidemessage == false)
                    {
                        Helper.SendNotificationWithoutButton(player, "Du musst zuerst deine aktuelle Animation beenden!", "error", "top-left", 1500);
                    }
                    return;
                }
                if (!animation.animation.Contains("vehicle") && player.Vehicle != null)
                {
                    return;
                }
                if (animation.animation.Contains("vehicle") && player.Vehicle == null)
                {
                    return;
                }
                player.SetData<bool>("Player:PlayCustomAnimation", true);
                name = name[0].ToString().ToUpper() + name.Substring(1);
                if (animation.duration <= 32 || animation.duration == 600)
                {
                    player.SetSharedData("Player:AnimData", $"{animation.animation}");
                }
                else
                {
                    string[] animArray = new string[3];
                    animArray = animation.animation.Split("%");
                    Helper.PlayShortAnimation(player, animArray[0], animArray[1], animation.duration);
                }
                if (hidemessage == false)
                {
                    Helper.SendNotificationWithoutButton(player, $"Animation {name} wird abgespielt!", "success", "top-left", 1500);
                }
            }
            catch (Exception e)
            {
                if (hidemessage == false)
                {
                    Helper.SendNotificationWithoutButton(player, "Diese Animation existiert nicht!", "error", "top-left", 1500);
                }
                Helper.ConsoleLog("error", $"[cmd_animation]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:StopCommandAnimation")]
        [Command("stopanimation", "Befehl: /stopanimation", Alias = "stopanim")]
        public void cmd_stopanimation(Player player)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                if (player.GetSharedData<string>("Player:AnimData") == "0")
                {
                    Helper.SendNotificationWithoutButton(player, "Zurzeit wird keine Animation abgespielt!", "error", "top-left", 1500);
                    return;
                }
                if (!player.HasData("Player:PlayCustomAnimation"))
                {
                    Helper.SendNotificationWithoutButton(player, "Zurzeit wird keine Animation abgespielt!", "error", "top-left", 1500);
                    return;
                }
                player.ResetData("Player:PlayCustomAnimation");
                Helper.OnStopAnimation2(player);
                Helper.SendNotificationWithoutButton(player, "Animation beendet!", "success", "top-left", 1500);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[cmd_stopanimation]: " + e.ToString());
            }
        }

        [Command("fuel", "Befehl: /fuel")]
        public void cmd_fuel(Player player)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                if (!player.IsInVehicle)
                {
                    Helper.SendNotificationWithoutButton(player, "Du sitzt in keinem Fahrzeug!", "error", "top-left");
                    return;
                }
                if (player.Vehicle.GetSharedData<String>("Vehicle:Name") == "iak_wheelchair")
                {
                    Helper.SendNotificationWithoutButton(player, "Dieses Fahrzeug hat keinen Tank!", "error", "top-left");
                    return;
                }
                Helper.SendNotificationWithoutButton(player, $"Tank: {player.Vehicle.GetSharedData<float>("Vehicle:Fuel").ToString("0.0")}l/{player.Vehicle.GetSharedData<float>("Vehicle:MaxFuel").ToString("0.0")}l", "info", "top-end", 4500);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[cmd_fuel]: " + e.ToString());
            }
        }

        [Command("test0", "Befehl: /test")]
        public void cmd_test(Player player)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Character character = Helper.GetCharacterData(player);
                character.factionduty = true;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[cmd_test]: " + e.ToString());
            }
        }

        [Command("test", "Befehl: /test")]
        public void cmd_test1(Player player)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                NAPI.Player.SetPlayerWeaponAmmo(player, WeaponHash.Microsmg, 55);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[cmd_test]: " + e.ToString());
            }
        }

        [Command("test2", "Befehl: /test2")]
        public void cmd_test2(Player player)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                NAPI.Player.GivePlayerWeapon(player, WeaponHash.Pistol, 5);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[cmd_test]: " + e.ToString());
            }
        }

        [Command("test3", "Befehl: /test3")]
        public void cmd_test3(Player player)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                player.Position = new Vector3(1713.9882, -563.03455, 148.33177);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[cmd_test]: " + e.ToString());
            }
        }

        [Command("test4", "Befehl: /test3")]
        public void cmd_test4(Player player)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                NAPI.Player.SetPlayerHealth(player, 115);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[cmd_test]: " + e.ToString());
            }
        }

        [Command("checktüv", "Befehl: /checktüv")]
        public void cmd_checktuev(Player player)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                if (player.IsInVehicle)
                {
                    Helper.SendNotificationWithoutButton(player, "Du musst zuerst aus deinem aktuellen Fahrzeug aussteigen!", "error", "top-left");
                    return;
                }
                Vehicle vehicle = Helper.GetClosestVehicle(player, 3.55f);
                if (vehicle != null && vehicle.HasSharedData("Vehicle:Tuev") && vehicle.GetSharedData<int>("Vehicle:Tuev") != -50)
                {
                    Helper.SendNotificationWithoutButton(player, $"TÜV: {Cars.GetTuev(vehicle.GetSharedData<int>("Vehicle:Tuev"))}", "info", "top-end", 4750);
                }
                else
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiges Fahrzeug/kein TÜV vorhanden!", "error", "top-left");
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[cmd_checktuev]: " + e.ToString());
            }
        }

        [Command("resync", "Befehl: /resync", Alias = "ragdoll")]
        public void cmd_resync(Player player)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                if (!player.IsInVehicle)
                {
                    if (player.HasData("Player:TuneCooldown") && player.GetData<int>("Player:TuneCooldown") > 0)
                    {
                        if (Helper.UnixTimestamp() < player.GetData<int>("Player:TuneCooldown"))
                        {
                            Helper.SendNotificationWithoutButton(player, "Du kannst diesen Befehl nur alle 15 Sekunden verwenden!", "error", "top-end");
                            return;
                        }
                    }
                    player.SetData<int>("Player:TuneCooldown", Helper.UnixTimestamp() + (15));
                    Helper.SetPlayerPosition(player, new Vector3(player.Position.X + 0.01, player.Position.Y + 0.01, player.Position.Z + 0.01));
                    if (player.Health > 1)
                    {
                        Helper.SetPlayerHealth(player, player.Health - 1);
                    }
                    Helper.SendNotificationWithoutButton(player, "Resync war erfolgreich!", "info", "top-left");
                }
                else
                {
                    Helper.SendNotificationWithoutButton(player, "Dieser Befehl kann jetzt nicht benutzt werden!", "error", "top-left");
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[cmd_checktuev]: " + e.ToString());
            }
        }

        [Command("credits", "Befehl: /credits")]
        public static void cmd_credits(Player player)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                if (player.HasData("Player:TuneCooldown") && player.GetData<int>("Player:TuneCooldown") > 0)
                {
                    if (Helper.UnixTimestamp() < player.GetData<int>("Player:TuneCooldown"))
                    {
                        Helper.SendNotificationWithoutButton(player, "Du kannst diesen Befehl nur alle 15 Sekunden ausführen!", "error", "top-end");
                        return;
                    }
                }
                player.SetData<int>("Player:TuneCooldown", Helper.UnixTimestamp() + (15));
                Helper.SendNotificationWithTimer(player, "Credits", "Der Gamemode/das UCP wurde erstellt von Nemesus - (Nemesus.de)<br /><br />Großes Dankeschön an: Hrred, Yunalable, Marth und Spaex für die großartige Unterstützung!<br /><br />Weiterer Dank geht an: Felix, flaticon.com", 15500);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[cmd_credits]: " + e.ToString());
            }
        }

        [Command("hof", "Befehl: /hof")]
        public static void cmd_hof(Player player)
        {
            try
            {
                if (Helper.IsInRangeOfPoint(player.Position, new Vector3(-509.94046, -208.40746, 39.37905), 3.75f))
                {
                    if (player.HasData("Player:TuneCooldown") && player.GetData<int>("Player:TuneCooldown") > 0)
                    {
                        if (Helper.UnixTimestamp() < player.GetData<int>("Player:TuneCooldown"))
                        {
                            Helper.SendNotificationWithoutButton(player, "Du kannst dir die Hall of Fame nur alle 30 Sekunden anschauen!", "error", "top-end");
                            return;
                        }
                    }
                    player.SetData<int>("Player:TuneCooldown", Helper.UnixTimestamp() + (30));

                    if (Helper.GetRandomPercentage(45))
                    {
                        MySqlCommand command = General.Connection.CreateCommand();
                        command = General.Connection.CreateCommand();
                        command.CommandText = "SELECT name,premium FROM users WHERE premium > 0 LIMIT 275";

                        string names = "";
                        string name = "n/A";

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                name = reader.GetString("name");
                                names += $", {name}";
                            }
                            reader.Close();
                        }

                        if (names.Length == 0)
                        {
                            names = "Leider noch keiner :(";
                        }
                        else
                        {
                            names = names.Remove(0, 2);
                        }

                        if (!Account.IsPlayerLoggedIn(player)) return;
                        Helper.SendNotificationWithTimer(player, "Hall of Fame", $"Großes Dankeschön an unsere Unterstützer: <br /><br />{names}", 15500);
                    }
                    else
                    {
                        cmd_credits(player);
                    }
                }
                else
                {
                    Helper.SendNotificationWithoutButton(player, "Du bist nicht bei der Hall of Fame Statue!", "error", "top-left");
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[cmd_hof]: " + e.ToString());
            }
        }

        [Command("vehicleinfo", "Befehl: /vehicleinfo", Alias = "dl")]
        public void CMD_vehicleinfo(Player player)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                player.TriggerEvent("Client:SetDL");
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CMD_vehicleinfo]: " + e.ToString());
            }
        }

        [Command("sellvehicle", "Befehl: /sellvehicle", Alias = "sellcar")]
        public void CMD_sellvehicle(Player player)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                if (character == null) return;
                Groups group = GroupsController.GetGroupById(character.mygroup);
                if (!Account.IsPlayerLoggedIn(player)) return;
                if (!player.IsInVehicle)
                {
                    Helper.SendNotificationWithoutButton(player, "Du sitzt in keinem Fahrzeug!", "error", "top-end", 2500);
                    return;
                }
                VehicleData vehicleData = DealerShipController.GetVehicleDataByVehicle(player.Vehicle);
                if (vehicleData == null)
                {
                    Helper.SendNotificationWithoutButton(player, "Dieses Fahrzeug kann nicht verkauft werden!", "error", "top-end", 2500);
                    return;
                }
                if (vehicleData.owner.Contains("character"))
                {
                    if (vehicleData.owner != "character-" + character.id)
                    {
                        Helper.SendNotificationWithoutButton(player, "Dieses Fahrzeug kann nicht verkauft werden!", "error", "top-end", 2500);
                        return;
                    }
                }
                else if (vehicleData.owner.Contains("group"))
                {
                    if (group == null || vehicleData.owner != "group-" + group.id)
                    {
                        Helper.SendNotificationWithoutButton(player, "Dieses Fahrzeug kann nicht verkauft werden!", "error", "top-end", 2500);
                        return;
                    }
                    GroupsMembers groupMembers = GroupsController.GetGroupMemberById(character.id, character.mygroup);
                    if (groupMembers == null || groupMembers.rang < 10)
                    {
                        Helper.SendNotificationWithoutButton(player, "Keine Berechtigung!", "error", "top-end", 2500);
                        return;
                    }
                }
                VehicleShop vehicleShop = DealerShipController.GetVehicleShopByVehicleName(vehicleData.vehiclename);
                if (vehicleShop == null)
                {
                    Helper.SendNotificationWithoutButton(player, "Das Autohaus kauft dieses Fahrzeug nicht!", "error", "top-end", 2500);
                    return;
                }
                Business bizz = Business.GetBusinessById(vehicleShop.bizzid);
                if (bizz != null)
                {
                    if (bizz.cash < (vehicleShop.price / 3))
                    {
                        Helper.SendNotificationWithoutButton(player, "Das Autohaus hat nicht genügend Geld um dir das Fahrzeug abzukaufen!", "error", "top-end", 2500);
                        return;
                    }
                    if (player.Vehicle.Position.DistanceTo(bizz.position) <= 6.25)
                    {
                        player.SetData<int>("Player:VehiclePrice", vehicleShop.price / 3);
                        player.TriggerEvent("Client:CallInput2", "Fahrzeug verkaufen", $"Möchtest du das Fahrzeug für {vehicleShop.price / 3}$ verkaufen?", "SellVehicle", "Ja", "Nein");
                    }
                    else
                    {
                        Helper.SendNotificationWithoutButton(player, "Das Autohaus kauft dieses Fahrzeug nicht!", "error", "top-end", 2500);
                    }
                }

            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CMD_sellvehicle]: " + e.ToString());
            }
        }


        [Command("dimension", "Befehl: /dimension", Alias = "vw")]
        public void cmd_dimension(Player player)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Helper.SendNotificationWithoutButton(player, "Deine Dimension: " + player.Dimension, "info", "top-end", 4500);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[cmd_dimension]: " + e.ToString());
            }
        }

        [Command("health", "Befehl: /health")]
        public void cmd_health(Player player)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Helper.SendNotificationWithoutButton(player, "Dein Leben: " + NAPI.Player.GetPlayerHealth(player) + "%", "info", "top-end", 4500);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[cmd_health]: " + e.ToString());
            }
        }

        [Command("lastvehicle", "Befehl: /lastvehicle", Alias = "lv")]
        public void cmd_lastvehicle(Player player)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                TempData tempData = Helper.GetCharacterTempData(player);
                if (tempData.lastvehicle > 0)
                {
                    Helper.SendNotificationWithoutButton(player, "Letzte Fahrzeug-ID: " + tempData.lastvehicle, "info", "top-end", 4500);
                }
                else
                {
                    Helper.SendNotificationWithoutButton(player, "Du warst in noch keine Fahrzeug!", "error", "top-end", 4500);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[cmd_lastvehicle]: " + e.ToString());
            }
        }

        [Command("reloadhud", "Befehl: /reloadhud")]
        public void cmd_reloadhud(Player player)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                if (player.IsInVehicle)
                {
                    Helper.SendNotificationWithoutButton(player, "Du musst zuerst aus deinem Fahrzeug aussteigen!", "error", "top-end", 2500);
                    return;
                }
                player.TriggerEvent("Client:ReloadHud");
                Helper.SetPlayerHealth(player, player.Health);
                Helper.SendNotificationWithoutButton(player, "Das Hud wurde reloaded!", "info", "top-end", 2500);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[cmd_reloadhud]: " + e.ToString());
            }
        }

        [Command("socialclubid", "Befehl: /rockstarid")]
        public void cmd_rockstarid(Player player)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Helper.SendNotificationWithoutButton(player, "Deine Socialclub-ID: " + player.SocialClubId, "info", "top-end", 10500);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[cmd_rockstarid]: " + e.ToString());
            }
        }

        [Command("vehicleclass", "Befehl: /vehicleclass")]
        public void cmd_vehicleclass(Player player)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                TempData tempData = Helper.GetCharacterTempData(player);
                if (player.IsInVehicle)
                {
                    Helper.SendNotificationWithoutButton(player, $"Fahrzeug Klasse: {player.Vehicle.Class}", "info", "top-end", 4500);
                }
                else
                {
                    Helper.SendNotificationWithoutButton(player, "Du sitzt in keinem Fahrzeug!", "error", "top-end", 4500);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[cmd_vehicleclass]: " + e.ToString());
            }
        }

        [Command("hashkey", "Befehl: /hashkey [Objekt]")]
        public void cmd_hashkey(Player player, string obj)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Helper.SendNotificationWithoutButton(player, $"Hashkey: {NAPI.Util.GetHashKey(obj)}", "info", "top-end", 7500);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[cmd_hashkey]: " + e.ToString());
            }
        }


        [Command("limitspeed", "Befehl: /limitspeed [Geschwindigkeit]")]
        public void cmd_vehicleclass(Player player, int speed)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                TempData tempData = Helper.GetCharacterTempData(player);
                if (!player.IsInVehicle)
                {
                    Helper.SendNotificationWithoutButton(player, "Du sitzt in keinem Fahrzeug!", "error", "top-end", 4500);
                    return;
                }
                if (player.Vehicle.Class == 13 || player.Vehicle.EngineStatus == false)
                {
                    Helper.SendNotificationWithoutButton(player, "Der Motor läuft nicht/dieses Fahrzeug hat keinen Geschwindigkeitsregler!", "error", "top-end", 4500);
                    return;
                }
                if ((speed < 5 || speed > 500) && speed != -1 && player.Vehicle.GetSharedData<int>("Vehicle:Speedlimit") > 0)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültige Geschwindigkeit!", "error", "top-end", 4500);
                    return;
                }
                if (player.Vehicle.GetSharedData<int>("Vehicle:Speedlimit") == 0)
                {
                    Helper.SendNotificationWithoutButton(player, $"Geschwindigskeitsregler auf {speed} KM/H gestellt!", "success", "top-end", 4500);
                    player.TriggerEvent("Client:Speedlimit", speed);
                    player.Vehicle.SetSharedData("Vehicle:Speedlimit", speed);
                }
                else
                {
                    Helper.SendNotificationWithoutButton(player, $"Geschwindigskeitsregler resetet!", "success", "top-end", 4500);
                    player.TriggerEvent("Client:Speedlimit", 999);
                    player.Vehicle.SetSharedData("Vehicle:Speedlimit", 0);
                }
            }
            catch (Exception e)
            {
                Helper.SendNotificationWithoutButton(player, "Ungültige Geschwindigkeit!", "error", "top-end", 4500);
                Helper.ConsoleLog("error", $"[cmd_vehicleclass]: " + e.ToString());
            }
        }

        [Command("deletemarker", "Befehl: /deletemarker", Alias = "dm")]
        public void cmd_deletemarker(Player player)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Helper.SendNotificationWithoutButton(player, "Markierung gelöscht!", "info", "top-end", 4500);
                player.TriggerEvent("Client:RemoveWaypoint");
                player.TriggerEvent("Client:RemoveWaypoint2");
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[cmd_deletemarker]: " + e.ToString());
            }
        }

        [Command("abort", "Befehl: /abort", Alias = "abbrechen")]
        public void cmd_abort(Player player)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                TempData tempData = Helper.GetCharacterTempData(player);
                if (tempData == null) return;
                //Speditionsauftrag
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
                        Helper.SendNotificationWithoutButton(player, "Auftrag abgebrochen!", "success", "top-left", 2500);
                        return;
                    }
                }
                //Taxiauftrag
                if (tempData.jobColshape != null && tempData.order2 != null && Helper.IsATaxiDriver(player) > -1)
                {
                    if (tempData.jobColshape != null)
                    {
                        player.TriggerEvent("Client:DeleteMarker");
                        player.TriggerEvent("Client:DeletePed");
                        tempData.jobColshape.Delete();
                        tempData.jobColshape = null;
                        tempData.jobstatus = 0;
                        tempData.order2 = null;
                        player.TriggerEvent("Client:RemoveWaypoint");
                        Helper.SendNotificationWithoutButton(player, "Auftrag abgebrochen!", "success", "top-left", 2500);
                        return;
                    }
                }
                //ATM
                if (tempData.furniturePosition != null && player.Vehicle != null && Helper.IsASecruity(player) > -1)
                {
                    player.TriggerEvent("Client:RemoveWaypoint");
                    tempData.furniturePosition = null;
                    tempData.tempValue = 0;
                    tempData.jobstatus = 0;
                    Helper.SendNotificationWithoutButton(player, "Auftrag abgebrochen!", "success", "top-left", 2500);
                    return;
                }
                //Waffenscheinsystem
                if (tempData.jobColshape != null && player.HasData("Player:AmmuQuiz"))
                {
                    if (player.GetData<int>("Player:AmmuQuiz") == 2)
                    {
                        NAPI.Player.SetPlayerWeaponAmmo(player, WeaponHash.Pistol, 0);
                        NAPI.Player.RemovePlayerWeapon(player, WeaponHash.Pistol);
                        if (tempData.jobColshape != null)
                        {
                            tempData.jobColshape.Delete();
                            tempData.jobColshape = null;
                        }
                        player.SetData<int>("Player:AmmuQuiz", 0);
                        player.ResetData("Player:AmmuQuiz");
                        player.TriggerEvent("Client:StopRange");
                        Helper.SendNotificationWithoutButton(player, "Praktische Waffenscheinprüfung abgebrochen!", "error", "top-left", 4000);
                        return;
                    }
                }
                //Shootingrange
                if (player.GetData<int>("Player:AmmuQuiz") == 4)
                {
                    if (tempData.jobColshape != null)
                    {
                        tempData.jobColshape.Delete();
                        tempData.jobColshape = null;
                    }
                    player.SetData<int>("Player:AmmuQuiz", 0);
                    player.ResetData("Player:AmmuQuiz");
                    player.TriggerEvent("Client:StopRange");
                    Helper.SendNotificationWithoutButton(player, "Vorgang abgebrochen!", "error", "top-left", 4000);
                    return;
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
                Helper.SendNotificationWithoutButton(player, "Du kannst jetzt nichts abbrechen!", "error", "top-left", 2500);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[cmd_abort]: " + e.ToString());
            }
        }

        [Command("afk", "Befehl: /afk")]
        public void cmd_afk(Player player)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Character character = Helper.GetCharacterData(player);
                TempData tempData = Helper.GetCharacterTempData(player);
                if (character == null || tempData == null) return;

                if (tempData.inCall == true)
                {
                    Helper.SendNotificationWithoutButton(player, "Du kannst jetzt nicht AFK gehen!", "error", "top-left");
                    return;
                }

                if (character.afk == 0)
                {
                    player.TriggerEvent("Client:SetAFK");
                }
                else
                {
                    Helper.SendNotificationWithoutButton(player, "Du bist bereits AFK!", "error", "top-left");
                }

            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[cmd_afk]: " + e.ToString());
            }
        }

        [Command("updateforum", "Befehl: /updateforum")]
        public void cmd_updateforum(Player player)
        {
            try
            {
                Account account = Helper.GetAccountData(player);
                if (account == null) return;
                if (!Account.IsPlayerLoggedIn(player)) return;
                if (account.forumaccount == -1)
                {
                    Helper.SendNotificationWithoutButton(player, "Du hast deinen Account noch nicht mit dem Forum verifiziert!", "error", "top-left");
                    return;
                }
                if (Helper.UnixTimestamp() > account.forumupdate)
                {
                    Helper.SendNotificationWithoutButton(player, "Du kannst deine Forumrechte nur alle 25 Minuten updaten!", "error", "top-left");
                    return;
                }
                Helper.ForumUpdate(player, "all");
                Helper.SendNotificationWithoutButton(player, "Forumrechte erfolgreich aktualisiert!", "success", "top-left", 3500);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[cmd_fuel]: " + e.ToString());
            }
        }

        [Command("settorso", "Befehl: /settorso [Torso-ID] [Variation]")]
        public void cmd_settorso(Player player, int torso, int variation)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Character character = Helper.GetCharacterData(player);
                if (character != null)
                {
                    if (player.HasData("Player:TorsoCD") && player.GetData<int>("Player:TorsoCD") > 0)
                    {
                        if (Helper.UnixTimestamp() < player.GetData<int>("Player:TorsoCD"))
                        {
                            Helper.SendNotificationWithoutButton(player, "Du musst noch kurz warten, bevor du diesen Befehl wieder ausführen kannst!", "error", "top-end");
                            return;
                        }
                    }
                    if (character.gender == 1)
                    {
                        if (torso < 0 || torso > 196)
                        {
                            Helper.SendNotificationWithoutButton(player, "Ungültiger Torso!", "error", "top-left", 2500);
                            return;
                        }
                    }
                    else
                    {
                        if (torso < 0 || torso > 241)
                        {
                            Helper.SendNotificationWithoutButton(player, "Ungültiger Torso!", "error", "top-left", 2500);
                            return;
                        }
                    }
                    player.SetData<int>("Player:TorsoCD", Helper.UnixTimestamp() + (2));
                    JObject obj = JObject.Parse(character.json);
                    obj["clothing"][1] = torso;
                    obj["clothingColor"][1] = variation;
                    character.json = NAPI.Util.ToJson(obj);
                    NAPI.Player.SetPlayerClothes(player, 3, (int)obj["clothing"][1], (int)obj["clothingColor"][1]);
                    Helper.SendNotificationWithoutButton(player, "Torso erfolgreich gesetzt!", "success", "top-left", 2500);
                }
            }
            catch (Exception e)
            {
                Helper.SendNotificationWithoutButton(player, "Ungültiger Torso!", "error", "top-left", 2500);
                Helper.ConsoleLog("error", $"[cmd_settorso]: " + e.ToString());
            }
        }

        //LSPD
        [Command("createcctv", "Befehl: /createcctv [Standort]", GreedyArg = true)]
        public void cmd_createcctv(Player player, string who)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                if (!Account.IsPlayerLoggedIn(player)) return;
                if (character.faction != 1)
                {
                    Helper.SendNotificationWithoutButton(player, $"Du bist kein Mitglied vom {Helper.factionList[character.faction - 1].name}!", "error", "top-left", 2500);
                    return;
                }
                if (character.factionduty == false)
                {
                    Helper.SendNotificationWithoutButton(player, "Du bist nicht im Dienst!", "error", "top-left", 2500);
                    return;
                }
                if (Helper.cctvList.Count >= 15)
                {
                    Helper.SendNotificationWithoutButton(player, "Es können max. 15 CCTVs aufgebaut werden!", "error", "top-left", 2500);
                    return;
                }
                if (player.Dimension != 0 || player.IsInVehicle)
                {
                    Helper.SendNotificationWithoutButton(player, "Du kannst jetzt keinen CCTV aufstellen!", "error", "top-left", 2500);
                    return;
                }
                if (who.Length < 3 || who.Length > 35)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Standort!", "error", "top-left", 2500);
                    return;
                }
                Helper.PlayShortAnimation(player, "amb@world_human_gardener_plant@male@idle_a", "idle_b", 2250);
                Vector3 newPosition = Helper.GetPositionInFrontOfPlayer(player, 0.85f);
                CCTV cctv = new CCTV();
                cctv.who = who;
                cctv.from = character.name;
                cctv.position = $"{newPosition.X.ToString(new CultureInfo("en-US"))},{newPosition.Y.ToString(new CultureInfo("en-US"))},{newPosition.Z.ToString(new CultureInfo("en-US"))}";
                cctv.heading = player.Heading - 90;
                cctv.cctvobject = NAPI.Object.CreateObject(NAPI.Util.GetHashKey("prop_cctv_pole_02"), new Vector3(newPosition.X, newPosition.Y, newPosition.Z - 1.15), new Vector3(0.0f, 0.0f, player.Heading - 90), 255, 0);
                Helper.cctvList.Add(cctv);
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                db.Insert(cctv);
                Helper.CreateFactionLog(character.faction, $"{character.name} hat einen CCTV[{cctv.id}] erstellt - ({cctv.who})!");
                Helper.SendNotificationWithoutButton(player, "Der CCTV wurde erfolgreich erstellt und ist absofort einsatzbereit!", "success", "top-left", 2500);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[createcctv]: " + e.ToString());
            }
        }

        [Command("deletecctv", "Befehl: /deletecctv")]
        public void cmd_deletecctv(Player player)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                if (!Account.IsPlayerLoggedIn(player)) return;
                if (character.faction != 1)
                {
                    Helper.SendNotificationWithoutButton(player, $"Du bist kein Mitglied vom {Helper.factionList[character.faction - 1].name}!", "error", "top-left", 2500);
                    return;
                }
                if (character.factionduty == false)
                {
                    Helper.SendNotificationWithoutButton(player, "Du bist nicht im Dienst!", "error", "top-left", 2500);
                    return;
                }
                if (Helper.cctvList.Count <= 0)
                {
                    Helper.SendNotificationWithoutButton(player, "Es wurden noch keine CCTVs erstellt!", "error", "top-left", 2500);
                    return;
                }
                if (player.Dimension != 0 || player.IsInVehicle)
                {
                    Helper.SendNotificationWithoutButton(player, "Du kannst jetzt keine CCTVs abbauen!", "error", "top-left", 2500);
                    return;
                }
                string[] positionsArray = new string[3];
                foreach (CCTV cctv in Helper.cctvList.ToList())
                {
                    positionsArray = cctv.position.Split(",");
                    if (player.Position.DistanceTo(new Vector3(float.Parse(positionsArray[0], new CultureInfo("en-US")), float.Parse(positionsArray[1], new CultureInfo("en-US")), float.Parse(positionsArray[2], new CultureInfo("en-US")))) <= 2.65)
                    {
                        Helper.PlayShortAnimation(player, "amb@world_human_gardener_plant@male@idle_a", "idle_b", 2250);
                        cctv.cctvobject.Delete();
                        cctv.cctvobject = null;
                        Helper.cctvList.Remove(cctv);
                        Helper.SendNotificationWithoutButton(player, "Der CCTV wurde erfolgreich abgebaut!", "success", "top-left", 2500);
                        Helper.CreateFactionLog(character.faction, $"{character.name} hat den CCTV[{cctv.id}] abgebaut!");
                        PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                        db.Delete(cctv);
                        return;
                    }
                }
                Helper.SendNotificationWithoutButton(player, "Du bist nicht in der Nähe von einem CCTV!", "error", "top-left", 2500);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[deletecctv]: " + e.ToString());
            }
        }

        [Command("createspeedcam", "Befehl: /createspeedcam [Max Geschwindigkeit] [Standort]", GreedyArg = true)]
        public void cmd_createspeedcam(Player player, int maxspeed, string who)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                if (!Account.IsPlayerLoggedIn(player)) return;
                if (character.faction != 1)
                {
                    Helper.SendNotificationWithoutButton(player, $"Du bist kein Mitglied vom {Helper.factionList[character.faction - 1].name}!", "error", "top-left", 2500);
                    return;
                }
                if (character.factionduty == false)
                {
                    Helper.SendNotificationWithoutButton(player, "Du bist nicht im Dienst!", "error", "top-left", 2500);
                    return;
                }
                if (Helper.blitzerList.Count >= 25)
                {
                    Helper.SendNotificationWithoutButton(player, "Es können max. 25 Blitzer aufgebaut werden!", "error", "top-left", 2500);
                    return;
                }
                if (player.Dimension != 0 || player.IsInVehicle)
                {
                    Helper.SendNotificationWithoutButton(player, "Du kannst jetzt keinen Blitzer aufstellen!", "error", "top-left", 2500);
                    return;
                }
                if (maxspeed < 50 || maxspeed > 500)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültige max. Geschwindigkeit!", "error", "top-left", 2500);
                    return;
                }
                if (who.Length < 3 || who.Length > 35)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Standort!", "error", "top-left", 2500);
                    return;
                }
                Helper.PlayShortAnimation(player, "amb@world_human_gardener_plant@male@idle_a", "idle_b", 2250);
                Vector3 newPosition = Helper.GetPositionInFrontOfPlayer(player, 0.85f);
                Blitzer blitzer = new Blitzer();
                blitzer.maxspeed = maxspeed;
                blitzer.who = who;
                blitzer.from = character.name;
                blitzer.heading = player.Heading - 105;
                blitzer.position = $"{newPosition.X.ToString(new CultureInfo("en-US"))},{newPosition.Y.ToString(new CultureInfo("en-US"))},{newPosition.Z.ToString(new CultureInfo("en-US"))}";
                blitzer.colshape = NAPI.ColShape.CreatCircleColShape(newPosition.X, newPosition.Y, 7f, 0);
                blitzer.speedobject = NAPI.Object.CreateObject(NAPI.Util.GetHashKey("prop_cctv_pole_01a"), new Vector3(newPosition.X, newPosition.Y, newPosition.Z - 3.95), new Vector3(0.0f, 0.0f, player.Heading - 105), 255, 0);
                Helper.blitzerList.Add(blitzer);
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                db.Insert(blitzer);
                Helper.CreateFactionLog(character.faction, $"{character.name} hat einen Blitzer[{blitzer.id}] erstellt - ({blitzer.who},{blitzer.maxspeed} KM/H)!");
                Helper.SendNotificationWithoutButton(player, "Der Blitzer wurde erfolgreich erstellt und ist absofort einsatzbereit!", "success", "top-left", 2500);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[cmd_createspeedcam]: " + e.ToString());
            }
        }

        [Command("deletespeedcam", "Befehl: /deletespeedcam")]
        public void cmd_deletespeedcam(Player player)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                if (!Account.IsPlayerLoggedIn(player)) return;
                if (character.faction != 1)
                {
                    Helper.SendNotificationWithoutButton(player, $"Du bist kein Mitglied vom {Helper.factionList[character.faction - 1].name}!", "error", "top-left", 2500);
                    return;
                }
                if (character.factionduty == false)
                {
                    Helper.SendNotificationWithoutButton(player, "Du bist nicht im Dienst!", "error", "top-left", 2500);
                    return;
                }
                if (Helper.blitzerList.Count <= 0)
                {
                    Helper.SendNotificationWithoutButton(player, "Es wurden noch keine Blitzer erstellt!", "error", "top-left", 2500);
                    return;
                }
                if (player.Dimension != 0 || player.IsInVehicle)
                {
                    Helper.SendNotificationWithoutButton(player, "Du kannst jetzt keine Blitzer abbauen!", "error", "top-left", 2500);
                    return;
                }
                string[] positionsArray = new string[3];
                foreach (Blitzer blitzer2 in Helper.blitzerList.ToList())
                {
                    positionsArray = blitzer2.position.Split(",");
                    if (player.Position.DistanceTo(new Vector3(float.Parse(positionsArray[0], new CultureInfo("en-US")), float.Parse(positionsArray[1], new CultureInfo("en-US")), float.Parse(positionsArray[2], new CultureInfo("en-US")))) <= 2.65)
                    {
                        Helper.PlayShortAnimation(player, "amb@world_human_gardener_plant@male@idle_a", "idle_b", 2250);
                        blitzer2.colshape.Delete();
                        blitzer2.colshape = null;
                        blitzer2.speedobject.Delete();
                        blitzer2.speedobject = null;
                        Helper.blitzerList.Remove(blitzer2);
                        Helper.SendNotificationWithoutButton(player, "Der Blitzer wurde erfolgreich abgebaut!", "success", "top-left", 2500);
                        Helper.CreateFactionLog(character.faction, $"{character.name} hat den Blitzer[{blitzer2.id}] abgebaut!");
                        PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                        db.Delete(blitzer2);
                        return;
                    }
                }
                Helper.SendNotificationWithoutButton(player, "Du bist nicht in der Nähe von einem Blitzer!", "error", "top-left", 2500);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[cmd_deletespeedcam]: " + e.ToString());
            }
        }

        [Command("createspikestrip", "Befehl: /createspikestrip")]
        public static void cmd_createspikestrip(Player player)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                if (!Account.IsPlayerLoggedIn(player)) return;
                if (character.faction != 1)
                {
                    Helper.SendNotificationWithoutButton(player, $"Du bist kein Mitglied vom {Helper.factionList[character.faction - 1].name}!", "error", "top-left", 2500);
                    return;
                }
                if (character.factionduty == false)
                {
                    Helper.SendNotificationWithoutButton(player, "Du bist nicht im Dienst!", "error", "top-left", 2500);
                    return;
                }
                Vehicle vehicle = Helper.GetClosestVehicle(player, 25.85f);
                VehicleData vehicleData = DealerShipController.GetVehicleDataByVehicle(vehicle);
                if (vehicle == null || vehicleData == null || vehicleData.owner != "faction-" + character.faction || vehicle.Class == 13 || vehicle.Class == 8 || vehicle.Class == 14 || vehicle.Class == 15)
                {
                    Helper.SendNotificationWithoutButton(player, "Du bist nicht in der Nähe von einem Fraktionsfahrzeug!", "error", "top-left", 2500);
                    return;
                }
                if (Helper.spikeStripList.Count > 0)
                {
                    foreach (SpikeStrip spikestrips in Helper.spikeStripList.ToList())
                    {
                        if (player.Position.DistanceTo(spikestrips.spikeobject.Position) <= 1.75)
                        {
                            Helper.PlayShortAnimation(player, "amb@world_human_gardener_plant@male@idle_a", "idle_b", 2250);
                            spikestrips.colshape.Delete();
                            spikestrips.colshape = null;
                            spikestrips.spikeobject.Delete();
                            spikestrips.spikeobject = null;
                            Helper.spikeStripList.Remove(spikestrips);
                            Helper.SendNotificationWithoutButton(player, "Das Nagelband wurde erfolgreich entfernt!", "success", "top-left", 2500);
                            return;
                        }
                    }
                }
                if (Helper.spikeStripList.Count >= 20)
                {
                    Helper.SendNotificationWithoutButton(player, "Es können max. 20 Nagelbänder aufgebaut werden!", "error", "top-left", 2500);
                    return;
                }
                if (player.Dimension != 0)
                {
                    Helper.SendNotificationWithoutButton(player, "Du kannst jetzt keine Nagelbänder aufstellen!", "error", "top-left", 2500);
                    return;
                }
                Random rand = new Random();
                Helper.PlayShortAnimation(player, "amb@world_human_gardener_plant@male@idle_a", "idle_b", 2250);
                Vector3 newPosition = Helper.GetPositionInFrontOfPlayer(player, 0.65f);
                SpikeStrip spikeStrip = new SpikeStrip();
                spikeStrip.id = rand.Next(1, 99999);
                spikeStrip.colshape = NAPI.ColShape.CreateSphereColShape(new Vector3(newPosition.X, newPosition.Y, newPosition.Z - 0.973), 2.85f, player.Dimension);
                spikeStrip.spikeobject = NAPI.Object.CreateObject(NAPI.Util.GetHashKey("p_stinger_03"), new Vector3(newPosition.X, newPosition.Y, newPosition.Z - 0.960), new Vector3(0.0f, 0.0f, player.Heading + 90), 255, 0);
                Helper.spikeStripList.Add(spikeStrip);
                Helper.SendNotificationWithoutButton(player, "Das Nagelband wurde erfolgreich aufgestellt!", "success", "top-left", 2500);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[cmd_createspikestrip]: " + e.ToString());
            }
        }

        [Command("createpoliceprop", "Befehl: /createpoliceprop")]
        public static void cmd_createpoliceprop(Player player, string prop = "prop_mp_cone_01")
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                if (!Account.IsPlayerLoggedIn(player)) return;
                if (character.faction != 1 && character.faction != 2 && character.faction != 3)
                {
                    Helper.SendNotificationWithoutButton(player, $"Du hast keine Berechtigung, diese Funktion auszuführen!", "error", "top-left", 2500);
                    return;
                }
                if (character.factionduty == false)
                {
                    Helper.SendNotificationWithoutButton(player, "Du bist nicht im Dienst!", "error", "top-left", 2500);
                    return;
                }
                Vehicle vehicle = Helper.GetClosestVehicle(player, 25.85f);
                VehicleData vehicleData = DealerShipController.GetVehicleDataByVehicle(vehicle);
                if (vehicle == null || vehicleData == null || vehicleData.owner != "faction-" + character.faction || vehicle.Class == 13 || vehicle.Class == 8 || vehicle.Class == 14 || vehicle.Class == 15)
                {
                    Helper.SendNotificationWithoutButton(player, "Du bist nicht in der Nähe von einem Fraktionsfahrzeug!", "error", "top-left", 2500);
                    return;
                }
                if (Helper.policePropList.Count > 0)
                {
                    foreach (PoliceProps policeProp in Helper.policePropList.ToList())
                    {
                        if (policeProp != null && policeProp.obj != null && player.Position.DistanceTo(policeProp.obj.Position) <= 1.75)
                        {
                            Helper.PlayShortAnimation(player, "amb@world_human_gardener_plant@male@idle_a", "idle_b", 2250);
                            policeProp.obj.Delete();
                            policeProp.obj = null;
                            Helper.policePropList.Remove(policeProp);
                            Helper.SendNotificationWithoutButton(player, "Das Objekt wurde erfolgreich entfernt!", "success", "top-left", 2500);
                            return;
                        }
                    }
                }
                if (Helper.policePropList.Count >= 50)
                {
                    Helper.SendNotificationWithoutButton(player, "Es können max. 50 Objekte aufgebaut werden!", "error", "top-left", 2500);
                    return;
                }
                if (player.Dimension != 0)
                {
                    Helper.SendNotificationWithoutButton(player, "Du kannst jetzt keine Objekte aufstellen!", "error", "top-left", 2500);
                    return;
                }
                Random rand = new Random();
                Helper.PlayShortAnimation(player, "amb@world_human_gardener_plant@male@idle_a", "idle_b", 2250);
                Vector3 newPosition = Helper.GetPositionInFrontOfPlayer(player, 1.1f);
                PoliceProps policeProps = new PoliceProps();
                policeProps.id = rand.Next(1, 99999);
                float heading = player.Heading + 90;
                if (prop != "prop_mp_cone_01")
                {
                    heading = player.Heading;
                }
                if (prop == "prop_mp_cone_01")
                {
                    policeProps.obj = NAPI.Object.CreateObject(NAPI.Util.GetHashKey(prop), new Vector3(newPosition.X, newPosition.Y, newPosition.Z - 1.085), new Vector3(0.0f, 0.0f, heading), 255, 0);
                }
                else
                {
                    policeProps.obj = NAPI.Object.CreateObject(NAPI.Util.GetHashKey(prop), new Vector3(newPosition.X, newPosition.Y, newPosition.Z - 1.035), new Vector3(0.0f, 0.0f, heading), 255, 0);
                }
                Helper.policePropList.Add(policeProps);
                Helper.SendNotificationWithoutButton(player, "Das Objekt wurde erfolgreich aufgebaut!", "success", "top-left", 2500);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[cmd_createpoliceprop]: " + e.ToString());
            }
        }

        //Spediteur
        [Command("products", "Befehl: /products")]
        public void CMD_products(Player player)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                if (!player.IsInVehicle)
                {
                    Helper.SendNotificationWithoutButton2(player, "Du sitzt in keinem Fahrzeug!", "error", "top-end");
                    return;
                }
                SpedVehicles spedVehicle = Helper.GetSpedVehicleByModel(player.Vehicle.GetSharedData<string>("Vehicle:Name"));
                if (spedVehicle == null)
                {
                    Helper.SendNotificationWithoutButton2(player, "Keine Produkte geladen!", "error", "top-end");
                    return;
                }
                Helper.SendNotificationWithoutButton(player, $"Geladene Produkte: {player.Vehicle.GetData<int>("Vehicle:Products")} Produkte", "info", "top-end", 4500);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[cmd_products]: " + e.ToString());
            }
        }

        [Command("share", "Befehl: /share [Name/ID]")]
        public void cmd_share(Player player, string target)
        {
            try
            {
                TempData tempData = Helper.GetCharacterTempData(player);
                Character character = Helper.GetCharacterData(player);
                if (!Account.IsPlayerLoggedIn(player)) return;
                Player ntarget = Helper.GetPlayerByNameOrID(target);
                if (ntarget == null || ntarget == player || !Account.IsPlayerLoggedIn(ntarget))
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Spieler!", "error", "top-end");
                    return;
                }
                TempData tempData2 = Helper.GetCharacterTempData(ntarget);
                Character character2 = Helper.GetCharacterData(ntarget);
                if (player.Position.DistanceTo(ntarget.Position) > 10.5)
                {
                    Helper.SendNotificationWithoutButton(player, "Der Spieler befindet sich nicht in deiner Nähe!", "error", "top-left");
                    return;
                }
                if (character.job == 4 && tempData.jobduty == true)
                {
                    if (!player.HasData("Player:GarbageRoute"))
                    {
                        Helper.SendNotificationWithoutButton(player, $"Du fährst aktuell keine Müllroute!", "error", "top-end", 2500);
                        return;
                    }
                    if (player.GetData<int>("Player:GarbageStation") > 1)
                    {
                        Helper.SendNotificationWithoutButton(player, $"Du kannst die Müllroute jetzt nichtmehr sharen!", "error", "top-end", 2500);
                        return;
                    }
                    if (character.job != 4)
                    {
                        Helper.SendNotificationWithoutButton(player, "Der angegebene Spieler ist kein Müllmann!", "error", "top-left");
                        return;
                    }
                    if (tempData2.jobduty == false)
                    {
                        Helper.SendNotificationWithoutButton(player, "Der angegebene Spieler hat seinen Jobdienst noch nicht gestartet!", "error", "top-left");
                        return;
                    }
                    ntarget.SetData<Player>("Player:GroupInvitePlayer", player);
                    Helper.SendNotificationWithoutButton(player, "Müllauftrag Share Anfrage geschickt!", "success", "top-left");
                    ntarget.TriggerEvent("Client:CallInput2", "Müllauftrag Share", $"{character.name} möchte seine Müllroute mit dir teilen, annehmen?", "AcceptShare2", "Ja", "Nein");
                    return;
                }
                if (Helper.IsASpediteur(player) != -1)
                {
                    if (Helper.IsASpediteur(ntarget) == -1)
                    {
                        Helper.SendNotificationWithoutButton(player, "Der angegebene Spieler ist kein Spediteur!", "error", "top-left");
                        return;
                    }
                    if (!player.IsInVehicle)
                    {
                        Helper.SendNotificationWithoutButton(player, "Du sitzt in keinem Fahrzeug!", "error", "top-left");
                        return;
                    }
                    if (tempData.order == null)
                    {
                        Helper.SendNotificationWithoutButton(player, "Du hast keinen aktiven Auftrag!", "error", "top-left");
                        return;
                    }
                    if (!ntarget.IsInVehicle)
                    {
                        Helper.SendNotificationWithoutButton(player, "Der angegebene Spieler sitzt in keinem Fahrzeug!", "error", "top-left");
                        return;
                    }
                    SpedVehicles spedVehicle = Helper.GetSpedVehicleByModel(ntarget.Vehicle.GetSharedData<string>("Vehicle:Name"));
                    if (spedVehicle == null)
                    {
                        Helper.SendNotificationWithoutButton(player, "Der Spieler kann keinen Auftrag annehmen!", "error", "top-left");
                        return;
                    }
                    if (tempData2.order != null)
                    {
                        Helper.SendNotificationWithoutButton(player, "Der Spieler fährt schon einen anderen Auftrag!", "error", "top-left");
                        return;
                    }
                    ntarget.SetData<Player>("Player:GroupInvitePlayer", player);
                    Helper.SendNotificationWithoutButton(player, "Speditionsauftrag Share Anfrage geschickt!", "success", "top-left");
                    ntarget.TriggerEvent("Client:CallInput2", "Speditionsauftrag Share", $"{character.name} möchte seinen Speditions Auftrag mit dir teilen, annehmen?", "AcceptShare", "Ja", "Nein");
                    return;
                }
                Helper.SendNotificationWithoutButton(player, "Du kannst diesen Befehl jetzt nicht nutzen!", "error", "top-left");
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[cmd_products]: " + e.ToString());
            }
        }

        [Command("report", "Befehl: /report [Name/ID]")]
        public void cmd_report(Player player, string target)
        {
            try
            {
                TempData tempData = Helper.GetCharacterTempData(player);
                if (!Account.IsPlayerLoggedIn(player)) return;
                Player ntarget = Helper.GetPlayerByNameOrID(target);
                if (ntarget == null || ntarget == player || !Account.IsPlayerLoggedIn(ntarget))
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Spieler!", "error", "top-end");
                    return;
                }
                player.SetData<int>("Player:Report", ntarget.Id);
                player.TriggerEvent("Client:CallInput", "Spieler melden", "Warum möchtest du diesen Spieler melden?", "text", "Der Spieler cheatet!", 30, "ReportPlayer");
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[cmd_report]: " + e.ToString());
            }
        }

        [Command("logout", "Befehl: /logout")]
        public void cmd_logout(Player player)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Account account = Helper.GetAccountData(player);
                TempData tempData = Helper.GetCharacterTempData(player);
                Character character = Helper.GetCharacterData(player);
                if (account == null || tempData == null) return;
                if (tempData.freezed == true || player.Vehicle != null)
                {
                    Helper.SendNotificationWithoutButton(player, "Du kannst dich jetzt nicht ausloggen!", "error", "top-end");
                    return;
                }
                if(tempData.adminduty == true)
                {
                    Helper.SendNotificationWithoutButton(player, "Du musst zuerst deinen Admindienst beenden!", "error", "top-end");
                    return;
                }
                CharacterController.SaveCharacter(player);
                Account.SaveAccount(player);
                Events.OnPlayerDisconnected(player, DisconnectionType.Left, "Charakterswitch");
                player.TriggerEvent("Client:PlayerFreeze", true);
                player.TriggerEvent("Client:ShowHud");
                NAPI.Task.Run(() =>
                {
                    CharacterController.GetAvailableCharacters(player, account.id);
                }, delayTime: 215);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[cmd_logout]: " + e.ToString());
            }
        }

        //Text RP Befehle
        [Command("addfriend", "Befehl: /addfriend [Name]", GreedyArg = true)]
        public void cmd_addfriend(Player player, string name)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Character character = Helper.GetCharacterData(player);
                Player tempPlayer = Helper.GetPlayerByCharacterName(name);
                if (Helper.adminSettings.nametag != 1)
                {
                    Helper.SendNotificationWithoutButton(player, "Das Nametagsystem steht nicht auf 1!", "error", "top-left", 2500);
                    return;
                }
                if ((tempPlayer == null && tempPlayer != player) || character.name == name)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiger Spieler!", "error", "top-left", 2500);
                    return;
                }
                if (character.friends.Contains(tempPlayer.Name))
                {
                    Helper.SendNotificationWithoutButton(player, "Dieser Spieler steht schon auf deiner Freundesliste!", "error", "top-left", 2500);
                    return;
                }
                character.friends = character.friends + tempPlayer.Name + ",";
                player.TriggerEvent("Client:UpdateFriends", character.friends);
                Helper.SendNotificationWithoutButton(player, $"{tempPlayer.Name} zur Freundesliste hinzugefügt!", "success", "top-left", 2500);
            }
            catch (Exception e)
            {
                Helper.SendNotificationWithoutButton(player, "Ungültiger Spieler!", "error", "top-left", 2500);
                Helper.ConsoleLog("error", $"[cmd_addfriend]: " + e.ToString());
            }
        }

        [Command("deletefriend", "Befehl: /deletefriend [Name]", GreedyArg = true)]
        public void cmd_deletefriend(Player player, string name)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Character character = Helper.GetCharacterData(player);
                Player tempPlayer = Helper.GetPlayerByCharacterName(name);
                if(Helper.adminSettings.nametag != 1)
                {
                    Helper.SendNotificationWithoutButton(player, "Das Nametagsystem steht nicht auf 1!", "error", "top-left", 2500);
                    return;
                }
                if (!character.friends.Contains(tempPlayer.Name))
                {
                    Helper.SendNotificationWithoutButton(player, "Dieser Spieler steht nicht auf deiner Freundesliste!", "error", "top-left", 2500);
                    return;
                }
                character.friends = character.friends.Replace(tempPlayer.Name + ",", "");
                player.TriggerEvent("Client:UpdateFriends", character.friends);
                Helper.SendNotificationWithoutButton(player, $"{tempPlayer.Name} von der Freundesliste entfernt!", "success", "top-left", 2500);
            }
            catch (Exception e)
            {
                Helper.SendNotificationWithoutButton(player, "Ungültiger Spieler!", "error", "top-left", 2500);
                Helper.ConsoleLog("error", $"[cmd_deletefriend]: " + e.ToString());
            }
        }

        [Command("me", "Befehl: /me [Nachricht]", GreedyArg = true)]
        public void CMD_me(Player player, string nachricht)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                if(Helper.adminSettings.voicerp == 1)
                {
                    Helper.SendNotificationWithoutButton(player, "Der Text-RP Modus muss zuerst aktiviert werden!", "error", "top-end");
                    return;
                }
                if (nachricht.Length < 3)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültige Nachricht!", "error", "top-end");
                    return;
                }
                Helper.SendRadiusMessage("!{#EE82EE}* " + player.Name + " " + nachricht, 8, player);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CMD_me]: " + e.ToString());
            }
        }

        [Command("do", "Befehl: /do [Nachricht]", GreedyArg = true)]
        public void CMD_do(Player player, string nachricht)
        {
            if (!Account.IsPlayerLoggedIn(player)) return;
            if (Helper.adminSettings.voicerp == 1)
            {
                Helper.SendNotificationWithoutButton(player, "Der Text-RP Modus muss zuerst aktiviert werden!", "error", "top-end");
                return;
            }
            if (nachricht.Length < 3)
            {
                Helper.SendNotificationWithoutButton(player, "Ungültige Nachricht!", "error", "top-end");
                return;
            }
            Helper.SendRadiusMessage("!{#42b6f5}* " + nachricht + " (( " + player.Name + " ))", 8, player);
        }

        [Command("speakquit", "Befehl: /speakquit [Nachricht]", GreedyArg = true, Alias = "sq")]
        public void CMD_speakquit(Player player, string nachricht)
        {
            if (!Account.IsPlayerLoggedIn(player)) return;
            if (Helper.adminSettings.voicerp == 1)
            {
                Helper.SendNotificationWithoutButton(player, "Der Text-RP Modus muss zuerst aktiviert werden!", "error", "top-end");
                return;
            }
            if (nachricht.Length <= 1)
            {
                Helper.SendNotificationWithoutButton(player, "Ungültige Nachricht!", "error", "top-end");
                return;
            }
            Helper.SendRadiusMessage("!{#FFFFFF}* " + player.Name + " sagt (leise): " + nachricht, 3, player);
            return;
        }

        [Command("radio", "Befehl: /radio [Nachricht]", GreedyArg = true, Alias = "r")]
        public void CMD_radio(Player player, string nachricht)
        {
            TempData tempData = Helper.GetCharacterTempData(player);
            if (!Account.IsPlayerLoggedIn(player) || tempData == null) return;
            if (Helper.adminSettings.voicerp == 1)
            {
                Helper.SendNotificationWithoutButton(player, "Der Text-RP Modus muss zuerst aktiviert werden!", "error", "top-end");
                return;
            }
            if(tempData.radio == "-1" || tempData.radio.Length <= 0)
            {
                Helper.SendNotificationWithoutButton(player, "Du musst zuerst dein Funkgerät anschalten und eine Frequenz auswählen!", "error", "top-end");
                return;
            }
            if (nachricht.Length <= 1)
            {
                Helper.SendNotificationWithoutButton(player, "Ungültige Nachricht!", "error", "top-end");
                return;
            }
            Helper.SendRadioMessage("!{#6fbbd3}[Funk-(FREQ-" + tempData.radio + ")] " + player.Name + ": " + nachricht, tempData.radio);
            Helper.SendRadiusMessage("!{#EE82EE}* " + player.Name + " [Funk]: " + nachricht, 8, player);
            if (!player.IsInVehicle)
            {
                Helper.PlayShortAnimation(player, "random@arrests", "generic_radio_enter", 2500);
            }
            return;
        }

        [Command("CMD_getwatercar", "Befehl: /CMD_getwatercar [Fahrzeug-ID]", Alias = "gwc")]
        public void CMD_getwatercar(Player player, int vehicleid)
        {
            try
            {
                if (!Account.IsPlayerLoggedIn(player)) return;
                Account account = Helper.GetAccountData(player);
                Character character = Helper.GetCharacterData(player);
                if(!player.IsInVehicle)
                {
                    Helper.SendNotificationWithoutButton(player, "Du sitzt in keinem Einsatzfahrzeug!", "error");
                    return;
                }
                if (!player.Vehicle.GetSharedData<string>("Vehicle:Name").ToLower().Contains("firetruk"))
                {
                    Helper.SendNotificationWithoutButton(player, "Du sitzt in keinem Einsatzfahrzeug!", "error");
                    return;
                }
                Vehicle vehicle = Helper.GetVehicleById(vehicleid);
                if (vehicle == null || vehicle.Dimension == 150)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiges Fahrzeug!", "error");
                    return;
                }
                if (vehicle.HasData("Vehicle:Jacked") && vehicle.GetData<bool>("Vehicle:Jacked") == true)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiges Fahrzeug!", "error");
                    return;
                }
                if (vehicle.Position.DistanceTo(player.Vehicle.Position) >= 100f)
                {
                    Helper.SendNotificationWithoutButton(player, "Das Fahrzeug ist zu weit weg!", "error");
                    return;
                }
                player.TriggerEvent("Client:CheckIfEntityIsInWater", vehicle, vehicleid);
                
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CMD_getwatercar]: " + e.ToString());
            }
        }
    }
}
