using System;
using System.Collections.Generic;
using NemesusWorld.Database;
using NemesusWorld.Models;
using NemesusWorld.Utils;
using GTANetworkAPI;
using System.Globalization;
using Player = GTANetworkAPI.Player;
using System.Linq;
using Newtonsoft.Json;
using Vector3 = GTANetworkAPI.Vector3;
using Vehicle = GTANetworkAPI.Vehicle;
using Ped = GTANetworkAPI.Ped;
using TextLabel = GTANetworkAPI.TextLabel;

namespace NemesusWorld.Controllers
{
    public class GangController : Script
    {
        public static List<GangZones> gangzoneList = new List<GangZones>();
        public static Vector3 dealerPosition = null;
        public static Vehicle dealerVehicle = null;
        public static TextLabel dealerLabel = null;
        public static Ped dealerPed = null;
        public static Vector3 dealerPosition2 = null;
        public static TextLabel dealerLabel2 = null;
        public static Ped dealerPed2 = null;
        public static int[] dealerAmount;
        public static int drugAmount = 0;

        public GangController() { }

        public static void GetAllGangzones()
        {
            try
            {
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                int count = 0;
                foreach (GangZones gangzone in db.Fetch<GangZones>("SELECT * FROM gangzones"))
                {
                    gangzoneList.Add(gangzone);
                    List<GangzoneProps> gangzonePropList = new List<GangzoneProps>();
                    if (gangzone.percentages.Length > 3 && gangzone.percentages != "n/A")
                    {
                        gangzonePropList = JsonConvert.DeserializeObject<List<GangzoneProps>>(gangzone.percentages);
                    }
                    foreach (GangzoneProps gangzoneProps in gangzonePropList.ToList())
                    {
                        gangzoneProps.groupname = GroupsController.GetGroupNameById(gangzoneProps.groupid);
                        if (count <= 0)
                        {
                            gangzoneProps.respect --;
                        }
                        else
                        {
                            gangzoneProps.respect -= 2;
                        }
                        if (gangzoneProps.respect <= 0)
                        {
                            gangzoneProps.respect = 0;
                            gangzonePropList.Remove(gangzoneProps);
                        }
                    }
                    if(gangzonePropList.Count <= 0)
                    {
                        gangzone.things = 0;
                        gangzone.color = 40;
                    }
                    gangzone.percentages = NAPI.Util.ToJson(gangzonePropList);
                }

            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetAllGangzones]: " + e.ToString());
            }
        }

        public static void SaveAllGangzones()
        {
            try
            {
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                foreach (GangZones gangzone in gangzoneList)
                {
                    db.Save(gangzone);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[SaveAllGangzones]: " + e.ToString());
            }
        }

        public static GangZones GetGangZoneById(int gangzoneid)
        {
            GangZones gangZones = null;
            try
            {
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                foreach (GangZones gangzone in gangzoneList)
                {
                    if(gangzone.id == gangzoneid)
                    {
                        gangZones = gangzone;
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetGangZoneById]: " + e.ToString());
            }
            return gangZones;
        }

        public static void CreateNewGangZone(Player player, string name, string value, int radius)
        {
            try
            {
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);

                GangZones gangzone = new GangZones();

                gangzone.name = name;
                gangzone.value = value;
                gangzone.heading = player.Heading.ToString("#.##", new CultureInfo("en-US"));
                gangzone.color = 40;
                gangzone.radius = radius;
                gangzone.posx = player.Position.X.ToString("#.##", new CultureInfo("en-US"));
                gangzone.posy = player.Position.Y.ToString("#.##", new CultureInfo("en-US"));
                gangzone.posz = player.Position.Z.ToString("#.##", new CultureInfo("en-US"));

                db.Insert(gangzone);

                gangzoneList.Add(gangzone);

                Helper.SendNotificationWithoutButton(player, "Die Gangzone wurde erfolgreich erstellt!", "success", "top-left");

                NAPI.ClientEvent.TriggerClientEventForAll("Client:RemoveGangzones");
                NAPI.ClientEvent.TriggerClientEventForAll("Client:GetGangzones", NAPI.Util.ToJson(GangController.gangzoneList));
            }
            catch (Exception e)
            {
                Helper.SendNotificationWithoutButton(player, "Die Gangzone konnte nicht erstellt werden!", "error", "top-left");
                Helper.ConsoleLog("error", $"[CreateNewGangZone]: " + e.ToString());
            }
        }

        public static void UpdateGangZone(Player player, string name, string value, int radius)
        {
            try
            {
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);

                GangZones getZone = null;
                foreach (GangZones gangzone in gangzoneList)
                {
                    if (gangzone.name == name)
                    {
                        getZone = gangzone;
                        break;
                    }
                }

                if (getZone == null)
                {
                    Helper.SendNotificationWithoutButton(player, "Die Gangzone konnte nicht aktualisiert werden!", "error", "top-left");
                    return;
                }

                getZone.value = value;
                getZone.heading = player.Heading.ToString("#.##", new CultureInfo("en-US"));
                getZone.radius = radius;
                getZone.posx = player.Position.X.ToString("#.##", new CultureInfo("en-US"));
                getZone.posy = player.Position.Y.ToString("#.##", new CultureInfo("en-US"));
                getZone.posz = player.Position.Z.ToString("#.##", new CultureInfo("en-US"));

                db.Save(getZone);

                Helper.SendNotificationWithoutButton(player, "Die Gangzone wurde erfolgreich aktualisiert!", "success", "top-left");

                NAPI.ClientEvent.TriggerClientEventForAll("Client:RemoveGangzones");
                NAPI.ClientEvent.TriggerClientEventForAll("Client:GetGangzones", NAPI.Util.ToJson(GangController.gangzoneList));
            }
            catch (Exception e)
            {
                Helper.SendNotificationWithoutButton(player, "Die Gangzone konnte nicht aktualisiert werden!", "error", "top-left");
                Helper.ConsoleLog("error", $"[UpdateGangZone]: " + e.ToString());
            }
        }

        public static int CountGangZoneByGroupId(int groupid)
        {
            int count = 0;
            try
            {
                foreach(GangZones gangzone in gangzoneList)
                {
                    if (gangzone.percentages.Length > 3 && gangzone.percentages != "n/A")
                    {
                        List<GangzoneProps> gangzonePropList = new List<GangzoneProps>();
                        if (gangzone.percentages.Length > 3 && gangzone.percentages != "n/A")
                        {
                            gangzonePropList = JsonConvert.DeserializeObject<List<GangzoneProps>>(gangzone.percentages);
                        }
                        if(gangzonePropList.Count == 0)
                        {
                            count = 0;
                            break;
                        }
                        GangzoneProps gangzoneProps = (GangzoneProps)gangzonePropList.Take(1);
                        if(gangzoneProps != null && gangzoneProps.groupid == groupid)
                        {
                            count++;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CountGangZoneByGroupId]: " + e.ToString());
            }
            return count;
        }

        [RemoteEvent("Server:GetGangzoneInfos")]
        public static void OnGetGangzoneInfos(Player player)
        {
            try
            {
                TempData tempData = Helper.GetCharacterTempData(player);
                Character character = Helper.GetCharacterData(player);
                if (tempData != null && character != null && tempData.ingangzone != -1)
                {
                    GangZones gangzone = GetGangZoneById(tempData.ingangzone);
                    if(gangzone != null)
                    {
                        List<GangzoneProps> gangzonePropList = new List<GangzoneProps>();
                        if (gangzone.percentages.Length > 3 && gangzone.percentages != "n/A")
                        {
                            gangzonePropList = JsonConvert.DeserializeObject<List<GangzoneProps>>(gangzone.percentages);
                        }
                        GangZones gangzone2 = new GangZones();
                        gangzone2.id = gangzone.id;
                        gangzone2.name = gangzone.name;
                        gangzone2.percentages = "n/A";
                        gangzone2.value = gangzone.value;
                        player.TriggerEvent("Client:ShowGangzoneInfos", NAPI.Util.ToJson(gangzone2), NAPI.Util.ToJson(gangzonePropList), character.mygroup);
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnGetGangzoneInfos]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:GetGangzone")]
        public static void OnGetGangzone(Player player)
        {
            try
            {
                TempData tempData = Helper.GetCharacterTempData(player);
                Character character = Helper.GetCharacterData(player);
                if (tempData != null && character != null && tempData.ingangzone != -1 && character.mygroup != -1)
                {
                    GangZones gangzone = GetGangZoneById(tempData.ingangzone);
                    if(gangzone != null)
                    {
                        if(gangzone.things > 0 && gangzone.percentages.Length > 3 && gangzone.percentages != "n/A")
                        {
                            List<GangzoneProps> gangzonePropList = new List<GangzoneProps>();
                            gangzonePropList = JsonConvert.DeserializeObject<List<GangzoneProps>>(gangzone.percentages);
                            if (gangzonePropList[0].groupid == character.mygroup)
                            {
                                player.TriggerEvent("Client:HideGangzone");
                                Helper.SendNotificationWithoutButton(player, $"Du hast {gangzone.value} ({gangzone.things}) aus der Gangzone geholt!", "success", "top-left", 6500);
                                Helper.CreateGroupMoneyLog(character.mygroup, $"{character.name} hat {gangzone.value} ({gangzone.things}) aus der Gangzone {gangzone.name} geholt!");
                                if (gangzone.value == "Geld")
                                {
                                    CharacterController.SetMoney(player, gangzone.things);
                                }
                                else if (gangzone.value == "Materialien")
                                {
                                    Items newitem = ItemsController.CreateNewItem(player, character.id, "Materialien", "Player", gangzone.things, ItemsController.GetFreeItemID(player));
                                    if (!ItemsController.CanPlayerHoldItem(player, newitem.weight))
                                    {
                                        newitem = null;
                                        player.TriggerEvent("Client:HideGangzone");
                                        Helper.SendNotificationWithoutButton(player, "Du hast keinen Platz mehr im Inventar für die Materialien!", "success", "center");
                                        return;
                                    }
                                    if (newitem != null)
                                    {
                                        tempData.itemlist.Add(newitem);
                                    }
                                }
                                else if (gangzone.value == "Drogen")
                                {
                                    string drogenName = "";
                                    if (Helper.GetRandomPercentage(25))
                                    {
                                        drogenName = "Kokain";
                                        gangzone.things = gangzone.things / 2;
                                    }
                                    else if (Helper.GetRandomPercentage(15))
                                    {
                                        drogenName = "Crystal-Meth";
                                        gangzone.things = gangzone.things / 3;
                                    }
                                    else
                                    {
                                        drogenName = "Marihuana";
                                    }
                                    Items newitem = ItemsController.CreateNewItem(player, character.id, drogenName, "Player", gangzone.things, ItemsController.GetFreeItemID(player));
                                    if (!ItemsController.CanPlayerHoldItem(player, newitem.weight))
                                    {
                                        newitem = null;
                                        player.TriggerEvent("Client:HideGangzone");
                                        Helper.SendNotificationWithoutButton(player, "Du hast keinen Platz mehr im Inventar für die Drogen!", "success", "center");
                                        return;
                                    }
                                    if (newitem != null)
                                    {
                                        tempData.itemlist.Add(newitem);
                                    }
                                }
                                gangzone.things = 0;
                            }
                            else
                            {
                                player.TriggerEvent("Client:HideGangzone");
                                Helper.SendNotificationWithoutButton(player, "Deine Gruppierung hält diese Gangzone nicht mehr!", "success", "center");
                            }
                        }
                        else
                        {
                            player.TriggerEvent("Client:HideGangzone");
                            Helper.SendNotificationWithoutButton(player, "Es befindet sich kein Ertrag in der Gangzone!", "error", "top-left");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnGetGangzone]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:InGangZone")]
        public static void OnInGangZone(Player player, int gangzoneid)
        {
            try
            {
                TempData tempData = Helper.GetCharacterTempData(player);
                if (tempData != null)
                {
                    tempData.ingangzone = gangzoneid;
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnInGangZone]: " + e.ToString());
            }
        }

        public static void UpdateGangZoneValues(int gangzoneid, int group, int respect)
        {
            try
            {
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);

                bool found = false;
                GangZones gangzone = GetGangZoneById(gangzoneid);
                if (gangzone != null && group != -1)
                {
                    List<GangzoneProps> gangzonePropList = new List<GangzoneProps>();
                    if(gangzone.percentages.Length > 3 && gangzone.percentages != "n/A")
                    {
                        gangzonePropList = JsonConvert.DeserializeObject<List<GangzoneProps>>(gangzone.percentages);
                    }
                    foreach (GangzoneProps gangzoneProps in gangzonePropList.ToList())
                    {
                        if (gangzoneProps.groupid == group)
                        {
                            found = true;
                            gangzoneProps.groupname = GroupsController.GetGroupNameById(gangzoneProps.groupid);
                            gangzoneProps.respect += respect;
                            if (gangzoneProps.respect > 100)
                            {
                                gangzoneProps.respect = 100;
                            }
                            if (gangzoneProps.respect <= 0)
                            {
                                gangzoneProps.respect = 0;
                                gangzonePropList.Remove(gangzoneProps);
                            }
                            foreach (GangzoneProps gangzoneProps2 in gangzonePropList.ToList())
                            {
                                if (gangzoneProps2.groupid != group)
                                {
                                    if(gangzoneProps.respect == 100 && gangzoneProps2.respect == 100)
                                    {
                                        gangzoneProps2.respect -= 5;
                                    }
                                }
                            }
                            break;
                        }
                    }
                    if (found == false && respect > 0)
                    {
                        if(CountGangZoneByGroupId(group) > 0 && gangzonePropList.Count == 0) { return; }
                        GangzoneProps gangzoneProps = new GangzoneProps();
                        gangzoneProps.groupid = group;
                        gangzoneProps.groupname = GroupsController.GetGroupNameById(group);
                        gangzoneProps.respect = respect;
                        gangzonePropList.Add(gangzoneProps);
                    }
                    gangzone.percentages = NAPI.Util.ToJson(gangzonePropList.OrderByDescending(x => x.respect).ThenBy(x => x.groupid).Take(3));
                    if (gangzonePropList.Count > 0)
                    {
                        if (gangzone.color != 49)
                        {
                            NAPI.ClientEvent.TriggerClientEventForAll("Client:RemoveGangzones");
                            NAPI.ClientEvent.TriggerClientEventForAll("Client:GetGangzones", NAPI.Util.ToJson(GangController.gangzoneList));
                            gangzone.color = 49;
                        }
                    }
                    else
                    {
                        if (gangzone.color != 40)
                        {
                            NAPI.ClientEvent.TriggerClientEventForAll("Client:RemoveGangzones");
                            NAPI.ClientEvent.TriggerClientEventForAll("Client:GetGangzones", NAPI.Util.ToJson(GangController.gangzoneList));
                            gangzone.color = 40;
                        }
                    }
                }
                db.Save(gangzone);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[UpdateGangZoneValues]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:CraftItem")]
        public static void OnCraftItem(Player player, string craftsettings)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                TempData tempData = Helper.GetCharacterTempData(player);
                if (character == null) return;

                string[] craftArray = new string[4];
                craftArray = craftsettings.Split(",");

                Items craftingMats = ItemsController.GetItemByItemName(player, "Materialien");
                Items schwefel = null;
                Items koka = null;
                Items frost = null;
                Items ibu = null;
                if (craftArray[0] != "Kokain" && craftArray[0] != "Crystal-Meth" && craftArray[0] != "Space-Cookies" && (craftingMats == null || craftingMats.amount < Convert.ToInt32(craftArray[1])))
                {
                    Helper.SendNotificationWithoutButton(player, "Du hast nicht genügend Materialien dabei!", "error");
                    return;
                }
                if (craftArray[0] == "Kokain")
                {
                    schwefel = ItemsController.GetItemByItemName(player, "Schwefelsäure");
                    koka = ItemsController.GetItemByItemName(player, "Kokablatt");
                    if(schwefel != null && koka != null)
                    {
                        if(schwefel.amount < 1 || koka.amount < 5)
                        {
                            Helper.SendNotificationWithoutButton(player, "Du hast nicht alle Materialien für die Herstellung von Kokain!", "error");
                            return;
                        }
                    }
                    else
                    {
                        Helper.SendNotificationWithoutButton(player, "Du hast nicht alle Materialien für die Herstellung von Kokain!", "error");
                        return;
                    }
                }
                else if (craftArray[0] == "Crystal-Meth")
                {
                    ibu = ItemsController.GetItemByItemName(player, "Ibuprofee-400mg");
                    koka = ItemsController.GetItemByItemName(player, "Kokain");
                    frost = ItemsController.GetItemByItemName(player, "Frostschutzmittel");
                    if (koka != null && ibu != null && frost != null)
                    {
                        if (ibu.amount < 1 || koka.amount < 3 || frost.amount < 1)
                        {
                            Helper.SendNotificationWithoutButton(player, "Du hast nicht alle Materialien für die Herstellung von Crystal-Meth!", "error");
                            return;
                        }
                    }
                    else
                    {
                        Helper.SendNotificationWithoutButton(player, "Du hast nicht alle Materialien für die Herstellung von Crystal-Meth!", "error");
                        return;
                    }
                }
                else if (craftArray[0] == "Space-Cookies")
                {
                    koka = ItemsController.GetItemByItemName(player, "Marihuana");
                    frost = ItemsController.GetItemByItemName(player, "Backmischung");
                    if (koka != null && frost != null)
                    {
                        if (koka.amount < 5 || frost.amount < 1)
                        {
                            Helper.SendNotificationWithoutButton(player, "Du hast nicht alle Materialien für die Herstellung von Space-Cookies!", "error");
                            return;
                        }
                    }
                    else
                    {
                        Helper.SendNotificationWithoutButton(player, "Du hast nicht alle Materialien für die Herstellung von Space-Cookies!", "error");
                        return;
                    }
                }
                int skill = character.craftingskill / 25;
                if (skill < Convert.ToInt32(craftArray[3]))
                {
                    Helper.SendNotificationWithoutButton(player, $"Du benötigst mind. Craftingskill {Convert.ToInt32(craftArray[3])}!", "error");
                    return;
                }
                int amount = 1;
                if(craftArray[0].Contains("Munition"))
                {
                    amount = 35;
                }
                else if (craftArray[0] == "Kokain")
                {
                    amount = 10;
                }
                else if(craftArray[0] == "Crystal-Meth")
                {
                    amount = 5;
                }
                else if (craftArray[0] == "Space-Cookies")
                {
                    amount = 10;
                }
                else
                {
                    amount = 5;
                }
                if (!ItemsController.CanPlayerHoldItem(player, ItemsController.GetItemWeightFromList(craftArray[0]) * amount))
                {
                    Helper.SendNotificationWithoutButton(player, "Du hast keinen Platz mehr im Inventar für diesen Gegenstand!", "error", "top-left");
                    return;
                }
                if (craftArray[0] != "Kokain" && craftArray[0] != "Crystal-Meth" && craftArray[0] != "Space-Cookies")
                {
                    craftingMats.amount -= amount;
                    if (craftingMats.amount <= 0)
                    {
                        ItemsController.RemoveItem(player, craftingMats.itemid);
                    }
                }
                else
                {
                    if (craftArray[0] == "Kokain")
                    {
                        koka.amount -= 5;
                        if (koka.amount <= 0)
                        {
                            ItemsController.RemoveItem(player, koka.itemid);
                        }
                        schwefel.amount -= 1;
                        if (schwefel.amount <= 0)
                        {
                            ItemsController.RemoveItem(player, schwefel.itemid);
                        }
                    }
                    else if (craftArray[0] == "Crystal-Meth")
                    {
                        koka.amount -= 3;
                        if (koka.amount <= 0)
                        {
                            ItemsController.RemoveItem(player, koka.itemid);
                        }
                        ibu.amount -= 1;
                        if (ibu.amount <= 0)
                        {
                            ItemsController.RemoveItem(player, ibu.itemid);
                        }
                        frost.amount -= 1;
                        if (frost.amount <= 0)
                        {
                            ItemsController.RemoveItem(player, frost.itemid);
                        }
                    }
                    else if (craftArray[0] == "Space-Cookies")
                    {
                        koka.amount -= 5;
                        if (koka.amount <= 0)
                        {
                            ItemsController.RemoveItem(player, koka.itemid);
                        }
                        frost.amount -= 1;
                        if (frost.amount <= 0)
                        {
                            ItemsController.RemoveItem(player, frost.itemid);
                        }
                    }
                }
                player.TriggerEvent("Client:PressedEscape");
                NAPI.Task.Run(() =>
                {
                    player.SetData<string>("Player:Craftsettings", craftsettings);
                    player.SetSharedData("Player:AnimData", $"anim@amb@clubhouse@tutorial@bkr_tut_ig3@%machinic_loop_mechandplayer%{17}");
                    player.TriggerEvent("Client:StartLockpicking", Convert.ToInt32(craftArray[2]), "crafting", "Gegenstand wird gecrafted...");
                }, delayTime: 415);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnCraftItem]: " + e.ToString());
            }
        }

        public static void CreateWaffendealer()
        {
            try
            {
                if (dealerPosition != null)
                {
                    DeleteWaffendealer();
                }

                Random rnd = new Random();
                int random = rnd.Next(1, 5);

                string position = "";

                if (random == 1)
                {
                    position = "Elysian Island";

                    dealerPosition = new Vector3(-529.42584, -2785.0215, 6.0003843);

                    dealerVehicle = Cars.createNewCar("burrito", new Vector3(-527.498, -2781.6392, 5.8162427), -3.9613156f, 1, 1, "LS-S-15", "Anton", true, false, false, 0);
                    dealerVehicle.SetSharedData("Vehicle:Doors", "[false,false,true,true,false,true]");

                    dealerPed = NAPI.Ped.CreatePed(0x22911304, new Vector3(-529.42584, -2785.0215, 6.0003843), -144.15002f, true, true, true, false, 0);

                    dealerLabel = NAPI.TextLabel.CreateTextLabel("~b~A. der Waffendealer\n~w~Benutze Taste ~b~[E]~w~ um mit Ihm zu interagieren!", new Vector3(-529.42584, -2785.0215, 6.0003843 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                }
                else if (random == 2)
                {
                    position = "Terminal";

                    dealerPosition = new Vector3(1288.8188, -3287.7305, 5.904679);

                    dealerVehicle = Cars.createNewCar("burrito", new Vector3(1292.1821, -3289.47, 5.72079), -89.21847f, 1, 1, "LS-S-15", "Anton", true, false, false, 0);
                    dealerVehicle.SetSharedData("Vehicle:Doors", "[false,false,true,true,false,true]");

                    dealerPed = NAPI.Ped.CreatePed(0x22911304, new Vector3(1288.8188, -3287.7305, 5.904679), 118.78030f, true, true, true, false, 0);

                    dealerLabel = NAPI.TextLabel.CreateTextLabel("~b~A. der Waffendealer\n~w~Benutze Taste ~b~[E]~w~ um mit Ihm zu interagieren!", new Vector3(1288.8188, -3287.7305, 5.904679 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                }
                else if(random == 3)
                {
                    position = "La Mesa";

                    dealerPosition = new Vector3(1006.1442, -1483.7458, 31.144484);

                    dealerVehicle = Cars.createNewCar("burrito", new Vector3(1008.0442, -1480.6041, 30.935709), -0.37263325f, 1, 1, "LS-S-15", "Anton", true, false, false, 0);
                    dealerVehicle.SetSharedData("Vehicle:Doors", "[false,false,true,true,false,true]");

                    dealerPed = NAPI.Ped.CreatePed(0x22911304, new Vector3(1006.1442, -1483.7458, 31.144484), -153.55226f, true, true, true, false, 0);

                    dealerLabel = NAPI.TextLabel.CreateTextLabel("~b~A. der Waffendealer\n~w~Benutze Taste ~b~[E]~w~ um mit Ihm zu interagieren!", new Vector3(1006.1442, -1483.7458, 31.144484 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                }
                else if(random == 4)
                {
                    position = "Vinewood Mitte";

                    dealerPosition = new Vector3(651.5467, 279.04935, 103.29543);

                    dealerVehicle = Cars.createNewCar("burrito", new Vector3(654.6239, 277.28802, 103.10935), -94.0338f, 1, 1, "LS-S-15", "Anton", true, false, false, 0);
                    dealerVehicle.SetSharedData("Vehicle:Doors", "[false,false,true,true,false,true]");

                    dealerPed = NAPI.Ped.CreatePed(0x22911304, new Vector3(651.5467, 279.04935, 103.29543), 140.09431f, true, true, true, false, 0);

                    dealerLabel = NAPI.TextLabel.CreateTextLabel("~b~A. der Waffendealer\n~w~Benutze Taste ~b~[E]~w~ um mit Ihm zu interagieren!", new Vector3(651.5467, 279.04935, 103.29543 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                }
                else
                {
                    position = "Bonning";

                    dealerPosition = new Vector3(-38.65145, -2235.7646, 7.811672);

                    dealerVehicle = Cars.createNewCar("burrito", new Vector3(-41.03407, -2238.73, 7.628603), 159.05925f, 1, 1, "LS-S-15", "Anton", true, false, false, 0);
                    dealerVehicle.SetSharedData("Vehicle:Doors", "[false,false,true,true,false,true]");

                    dealerPed = NAPI.Ped.CreatePed(0x22911304, new Vector3(-38.65145, -2235.7646, 7.811672), 15.710923f, true, true, true, false, 0);

                    dealerLabel = NAPI.TextLabel.CreateTextLabel("~b~A. der Waffendealer\n~w~Benutze Taste ~b~[E]~w~ um mit Ihm zu interagieren!", new Vector3(-38.65145, -2235.7646, 7.811672 + 1.2), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                }

                dealerAmount = new int[22];

                dealerAmount[0] = rnd.Next(6, 15);
                dealerAmount[1] = rnd.Next(5, 12);
                dealerAmount[2] = rnd.Next(4, 10);
                dealerAmount[3] = rnd.Next(0, 1);
                dealerAmount[4] = rnd.Next(0, 1);
                if (Helper.GetRandomPercentage(15))
                {
                    dealerAmount[5] = 1;
                }
                else
                {
                    dealerAmount[5] = 0;
                }
                if (Helper.GetRandomPercentage(5))
                {
                    dealerAmount[6] = 1;
                }
                else
                {
                    dealerAmount[6] = 0;
                }
                dealerAmount[7] = rnd.Next(0, 3);
                dealerAmount[8] = rnd.Next(5, 25);
                dealerAmount[9] = rnd.Next(1, 3);
                dealerAmount[10] = rnd.Next(5, 15);
                dealerAmount[11] = rnd.Next(0, 3);
                if (Helper.GetRandomPercentage(5))
                {
                    dealerAmount[12] = 1;
                }
                else
                {
                    dealerAmount[12] = 0;
                }
                dealerAmount[13] = rnd.Next(0, 1);
                dealerAmount[14] = rnd.Next(0, 3);
                dealerAmount[15] = rnd.Next(0, 1);
                dealerAmount[16] = rnd.Next(12, 25);
                dealerAmount[17] = rnd.Next(3, 10);
                dealerAmount[18] = rnd.Next(1, 6);
                dealerAmount[19] = rnd.Next(1, 5);
                dealerAmount[20] = rnd.Next(200, 475);
                dealerAmount[21] = rnd.Next(1, 3);

                foreach (GangZones gangZones in gangzoneList)
                {
                    if (gangZones.percentages != "n/A" && gangZones.percentages.Length > 3)
                    {
                        List<GangzoneProps> gangzonePropList = new List<GangzoneProps>();
                        gangzonePropList = JsonConvert.DeserializeObject<List<GangzoneProps>>(gangZones.percentages);
                        foreach(GangzoneProps gangzoneProps in gangzonePropList)
                        {
                            foreach (Player player in NAPI.Pools.GetAllPlayers())
                            {
                                if (player != null && player.GetOwnSharedData<bool>("Player:Spawned") == true && player.GetSharedData<bool>("Player:Death") == false)
                                {
                                    Character character = Helper.GetCharacterData(player);
                                    if(character != null && character.mygroup == gangzoneProps.groupid)
                                    {
                                        Smartphone smartphone = SmartphoneController.GetSmartPhoneByNumber(character.lastsmartphone);
                                        if (smartphone != null && smartphone.akku > 0)
                                        {
                                            SmartphoneController.OnSmartphoneMessage(null, Helper.UnixTimestamp(), character.lastsmartphone, "01896667", $"Ich bin wieder da ({position}) und habe neue Ware dabei - A");
                                        }
                                    }                                                               
                                }
                            }
                        }
                    }
                }

                if(Helper.GetRandomPercentage(3))
                {
                    Dispatch dispatch = new Dispatch();
                    MDCController.dispatchCount++;
                    dispatch.id = MDCController.dispatchCount;
                    dispatch.text = $"Es wurde ein illegaler Waffendealer gesichtet - {position}!";
                    dispatch.name = "Anonym";
                    dispatch.phonenumber = "0189911";
                    dispatch.to = 1;
                    dispatch.position = $"{dealerPosition.X.ToString(new CultureInfo("en-US")):N3},{dealerPosition.Y.ToString(new CultureInfo("en-US")):N3},{dealerPosition.Z.ToString(new CultureInfo("en-US")):N3}";
                    dispatch.timestamp = Helper.UnixTimestamp();
                    MDCController.dispatchList.Add(dispatch);

                    MDCController.SendPoliceMDCMessage(null, $"Neuer Dispatch verfügbar - DSPTH-{dispatch.id}!");
                }

            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CreateWaffendealer]: " + e.ToString());
            }
        }

        public static void DeleteWaffendealer()
        {
            try
            {
                if (dealerPosition == null) return;
                dealerPosition = null;
                dealerVehicle.Delete();
                dealerVehicle = null;
                dealerPed.Delete();
                dealerPed = null;
                dealerLabel.Delete();
                dealerLabel = null;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[DeleteWaffendealer]: " + e.ToString());
            }
        }

        public static void CreateDrugDealer()
        {
            try
            {
                if (dealerPosition2 != null)
                {
                    DeleteDrugDealer();
                }

                Random rnd = new Random();
                int random = rnd.Next(1, 5);
                float posa = 0.0f;

                string position = "";

                if (random == 1)
                {
                    position = "Mirror Park";

                    dealerPosition2 = new Vector3(1143.8572, -313.77368, 67.40675);

                    posa = 138.3869f;
                }
                else if (random == 2)
                {
                    position = "Davis";

                    dealerPosition2 = new Vector3(-41.83749, -1755.966, 29.491667);

                    posa = -76.10556f;
                }
                else if (random == 3)
                {
                    position = "Flughafen";

                    dealerPosition2 = new Vector3(-742.5269, -2486.7112, 15.5939);

                    posa = 57.193066f;
                }
                else if (random == 4)
                {
                    position = "Little Seoul";

                    dealerPosition2 = new Vector3(-716.80334, -905.5677, 19.992228);

                    posa = 30.968786f;
                }
                else
                {
                    position = "Burton";

                    dealerPosition2 = new Vector3(-566.14813, 306.33322, 83.173096);

                    posa = -133.28674f;
                }

                drugAmount = rnd.Next(8,15);

                dealerPed2 = NAPI.Ped.CreatePed(0x9D0087A8, new Vector3(dealerPosition2.X, dealerPosition2.Y, dealerPosition2.Z), posa, true, true, true, false, 0);

                dealerLabel2 = NAPI.TextLabel.CreateTextLabel("~b~Big. D\n~w~Benutze Taste ~b~[E]~w~ um mit Ihm zu interagieren!", new Vector3(dealerPosition2.X, dealerPosition2.Y, dealerPosition2.Z + 1.2), 6.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);

                Helper.SendAdminMessage2($"Big.D wurde bei/m {position} gespawned!", 1, false);

                foreach (GangZones gangZones in gangzoneList)
                {
                    if (gangZones.percentages != "n/A" && gangZones.percentages.Length > 3)
                    {
                        List<GangzoneProps> gangzonePropList = new List<GangzoneProps>();
                        gangzonePropList = JsonConvert.DeserializeObject<List<GangzoneProps>>(gangZones.percentages);
                        foreach (GangzoneProps gangzoneProps in gangzonePropList)
                        {
                            foreach (Player player in NAPI.Pools.GetAllPlayers())
                            {
                                if (player != null && player.GetOwnSharedData<bool>("Player:Spawned") == true && player.GetSharedData<bool>("Player:Death") == false)
                                {
                                    Character character = Helper.GetCharacterData(player);
                                    if (character != null && character.mygroup == gangzoneProps.groupid)
                                    {
                                        Smartphone smartphone = SmartphoneController.GetSmartPhoneByNumber(character.lastsmartphone);
                                        if (smartphone != null && smartphone.akku > 0)
                                        {
                                            SmartphoneController.OnSmartphoneMessage(null, Helper.UnixTimestamp(), character.lastsmartphone, "01891117", $"Ich hab wieder neue Sachen am Start komm vorbei ({position}) - Big. D");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (Helper.GetRandomPercentage(5))
                {
                    Dispatch dispatch = new Dispatch();
                    MDCController.dispatchCount++;
                    dispatch.id = MDCController.dispatchCount;
                    dispatch.text = $"Es wurde ein illegaler Drogendealer gesichtet - {position}!";
                    dispatch.name = "Anonym";
                    dispatch.phonenumber = "0189911";
                    dispatch.to = 1;
                    dispatch.position = $"{dealerPosition2.X.ToString(new CultureInfo("en-US")):N3},{dealerPosition2.Y.ToString(new CultureInfo("en-US")):N3},{dealerPosition2.Z.ToString(new CultureInfo("en-US")):N3}";
                    dispatch.timestamp = Helper.UnixTimestamp();
                    MDCController.dispatchList.Add(dispatch);

                    MDCController.SendPoliceMDCMessage(null, $"Neuer Dispatch verfügbar - DSPTH-{dispatch.id}!");
                }

            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CreateDrugdealer]: " + e.ToString());
            }
        }

        public static void DeleteDrugDealer()
        {
            try
            {
                if (dealerPosition2 == null) return;
                dealerPosition2 = null;
                dealerPed2.Delete();
                dealerPed2 = null;
                dealerLabel2.Delete();
                dealerLabel2 = null;
                drugAmount = 0;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[DeleteDrugDealer]: " + e.ToString());
            }
        }
    }
}

