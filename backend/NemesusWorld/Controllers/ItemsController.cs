using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using GTANetworkAPI;
using GTANetworkMethods;
using MySql.Data.MySqlClient;
using NemesusWorld.Database;
using NemesusWorld.Models;
using NemesusWorld.Utils;
using Newtonsoft.Json;
using Ped = GTANetworkAPI.Ped;
using Player = GTANetworkAPI.Player;
using Vehicle = GTANetworkAPI.Vehicle;

namespace NemesusWorld.Controllers
{
    class ItemsController : Script
    {
        public static int MAX_INVENTORY_PLAYER = 16500;
        public static List<ItemModel> itemModelList = new List<ItemModel>();
        public static List<ItemsGlobal> itemListGlobal = new List<ItemsGlobal>();
        public static int itemGlobalId = 0;

        public static List<Items> ConvertPlayerItemsToList(Player player)
        {
            List<Items> itemList = new List<Items>();
            try
            {
                Character character = Helper.GetCharacterData(player);
                if (character == null) return itemList;

                if (character.items.Length > 5)
                {
                    itemList = JsonConvert.DeserializeObject<List<Items>>(character.items);
                    foreach (Items item in itemList)
                    {
                        if (item != null)
                        {
                            item.itemid = ItemsController.GetFreeItemID(player);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[LoadCharacterCharacter]: " + e.ToString());
            }
            return itemList;
        }

        public static Items CreateNewItem(Player player, int ownerid, string itemname, string owneridentifier = "Player", int amount = 1, int id = 0, string props = "n/A", string herkunft = "Administrativ", string charname = "n/A")
        {
            try
            {
                if (player != null)
                {
                    TempData tempData = Helper.GetCharacterTempData(player);
                    if (tempData == null) return null;
                    if (!CheckDoubleItems(itemname))
                    {
                        foreach (Items iteminlist in tempData.itemlist)
                        {
                            if (iteminlist != null && itemname.ToLower() == iteminlist.description.ToLower())
                            {
                                iteminlist.amount += amount;
                                return null;
                            }
                        }
                    }
                }
                Items item = new Items();
                item.ownerid = ownerid;
                item.owneridentifier = owneridentifier;
                GetItemByName(item, itemname);
                if (IsItemAWeapon(itemname))
                {
                    if (itemname.ToLower() != "granate" && itemname.ToLower() != "bzgas" && itemname.ToLower() != "rauchgranate" && itemname.ToLower() != "molotowcocktail" && itemname.ToLower() != "snowball")
                    {
                        item.amount = 1;
                        if (!itemname.ToLower().Contains("schutzweste") && !itemname.ToLower().Contains("feuerlöscher"))
                        {
                            string code = Helper.GeneratePin(12);
                            item.props = $"0,0,{WeaponController.GetWeaponClass(itemname.ToLower())},{code},{herkunft}|{charname},0,|";
                            if (herkunft != "n/A")
                            {
                                MySqlCommand command = General.Connection.CreateCommand();
                                command.CommandText = "INSERT INTO weapons (ident, name, shop, weaponname, timestamp) VALUES (@ident, @name, @shop, @weaponname, @timestamp)";
                                command.Parameters.AddWithValue("@ident", code);
                                command.Parameters.AddWithValue("@name", charname);
                                command.Parameters.AddWithValue("@shop", herkunft);
                                command.Parameters.AddWithValue("@weaponname", itemname);
                                command.Parameters.AddWithValue("@timestamp", Helper.UnixTimestamp());
                                command.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            int armorAmount = 0;
                            if (itemname.ToLower() == "kleine-schutzweste")
                            {
                                armorAmount = 25;
                            }
                            else if (itemname.ToLower() == "schutzweste")
                            {
                                armorAmount = 50;
                            }
                            else
                            {
                                armorAmount = 100;
                            }
                            string code = Helper.GeneratePin(12);
                            item.props = $"{armorAmount},0,{WeaponController.GetWeaponClass(itemname.ToLower())},{code},{herkunft}|{charname},0,|";
                        }
                        item.props.Trim();
                    }
                    else
                    {
                        item.amount = NAPI.Player.GetPlayerWeaponAmmo(player, (WeaponHash)WeaponController.GetWeaponHashFromName(itemname.ToLower())) + amount;
                        item.props = $"{NAPI.Player.GetPlayerWeaponAmmo(player, (WeaponHash)WeaponController.GetWeaponHashFromName(itemname.ToLower())) + amount},0,{WeaponController.GetWeaponClass(itemname.ToLower())},{Helper.GeneratePin(12)},{herkunft}|{charname},0,|";
                        item.props.Trim();
                    }
                }
                else
                {
                    item.amount = amount;
                    item.props = props;
                }
                item.itemid = id;
                return item;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CreateNewItem]: " + e.ToString());
            }
            return null;
        }

        public static bool AvailableItems(string itemname)
        {
            try
            {
                foreach (ItemModel itemmodel in itemModelList)
                {
                    if (itemmodel.description.ToLower() == itemname.ToLower())
                    {
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[AvailableItems]: " + e.ToString());
            }
            return false;
        }

        public static string GetItemNameByItemName(string itemname)
        {
            try
            {
                foreach (ItemModel itemmodel in itemModelList)
                {
                    if (itemmodel.description.ToLower() == itemname.ToLower())
                    {
                        return itemmodel.description;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetItemNameByItemName]: " + e.ToString());
            }
            return "n/A";
        }

        public static int GetItemWeightFromList(string itemname)
        {
            try
            {
                foreach (ItemModel itemmodel in itemModelList)
                {
                    if (itemmodel.description.ToLower() == itemname.ToLower())
                    {
                        return itemmodel.weight;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetItemWeight]: " + e.ToString());
            }
            return 0;
        }

        public static void GetItemByName(Items item, string itemname)
        {
            try
            {
                foreach (ItemModel itemmodel in itemModelList)
                {
                    if (itemmodel.description.ToLower() == itemname.ToLower())
                    {
                        item.hash = itemmodel.hash;
                        item.description = itemmodel.description;
                        item.type = itemmodel.type;
                        item.weight = itemmodel.weight;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetItemByName]: " + e.ToString());
            }
        }

        public static bool CanPlayerHoldItem(Player player, int newweight)
        {
            try
            {
                float weight = 0.0f;
                TempData tempData = Helper.GetCharacterTempData(player);
                foreach (Items item in tempData.itemlist)
                {
                    if (item != null)
                    {
                        if (item.type != 5)
                        {
                            weight += item.weight * item.amount;
                        }
                        else
                        {
                            string[] weaponArray = new string[7];
                            weaponArray = item.props.Split(",");
                            if (Convert.ToInt32(weaponArray[0]) >= 5000)
                            {
                                weaponArray[0] = "0";
                            }
                            if (item.description == "Flaregun")
                            {
                                weight += item.weight + (Convert.ToInt32(weaponArray[0]) * 85);
                            }
                            else
                            {
                                weight += item.weight + (Convert.ToInt32(weaponArray[0]) * 3);
                            }
                        }
                    }
                }
                weight += newweight;
                if (weight > MAX_INVENTORY_PLAYER)
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CanPlayerHoldItem]: " + e.ToString());
            }
            return true;
        }

        public static bool FurnitureCanHoldItem(FurnitureSetHouse furniture, int newweight)
        {
            try
            {
                if (furniture == null) return false;
                float weight = 0.0f;
                foreach (ItemsGlobal item in ItemsController.itemListGlobal)
                {
                    if (item != null && item.owneridentifier == "Furniture-" + furniture.props)
                    {
                        weight += item.weight;
                    }
                }
                weight += newweight;
                if (weight > GetFurnitureWeight(furniture.extra))
                {
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[FurnitureCanHoldItem]: " + e.ToString());
            }
            return false;
        }

        public static bool EvidenceCanHoldItem(int newweight)
        {
            try
            {
                float weight = 0.0f;
                foreach (ItemsGlobal item in ItemsController.itemListGlobal)
                {
                    if (item != null && item.owneridentifier == "evidence")
                    {
                        weight += item.weight;
                    }
                }
                weight += newweight;
                if (weight > 85000)
                {
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[EvidenceCanHoldItem]: " + e.ToString());
            }
            return false;
        }

        public static bool VehicleCanHoldItem(Vehicle vehicle, int vehicleDataId, int newweight, bool useTrunk = true)
        {
            try
            {
                float weight = 0.0f;
                foreach (ItemsGlobal item in ItemsController.itemListGlobal)
                {
                    if (useTrunk == true)
                    {
                        if (item != null && item.owneridentifier == "Trunk1-" + vehicleDataId)
                        {
                            weight += item.weight;
                        }
                    }
                    else
                    {
                        if (item != null && item.owneridentifier == "Trunk2-" + vehicleDataId)
                        {
                            weight += item.weight;
                        }
                    }
                }
                weight += newweight;
                if (useTrunk == true)
                {
                    int vehicleTrunk = Cars.GetVehicleTrunk(vehicle) * 1000;
                    if (weight > vehicleTrunk)
                    {
                        return false;
                    }
                }
                else
                {
                    if (weight > 5000)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[VehicleCanHoldItem]: " + e.ToString());
            }
            return false;
        }

        public static Items GetItemFromID(Player player, int itemid)
        {
            try
            {
                TempData tempData = Helper.GetCharacterTempData(player);

                if (tempData == null) return null;

                foreach (Items item in tempData.itemlist)
                {
                    if (item != null && item.itemid == itemid)
                    {
                        return item;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetItemFromID]: " + e.ToString());
            }
            return null;
        }

        public static Items GetItemFromProp(Player player, string prop)
        {
            try
            {
                TempData tempData = Helper.GetCharacterTempData(player);

                foreach (Items item in tempData.itemlist)
                {
                    if (item != null && item.props == prop)
                    {
                        return item;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetItemFromProp]: " + e.ToString());
            }
            return null;
        }

        public static Items GetItemFromWeaponHash(Player player, WeaponHash hash)
        {
            try
            {
                TempData tempData = Helper.GetCharacterTempData(player);

                foreach (Items item in tempData.itemlist)
                {
                    if (item != null && item.type == 5 && Convert.ToInt32(item.props.Split(",")[1]) == 1 && (WeaponHash)WeaponController.GetWeaponHashFromName(item.description.ToLower()) == hash)
                    {
                        return item;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetItemFromWeaponHash]: " + e.ToString());
            }
            return null;
        }

        public static Items GetItemByItemName(Player player, string name)
        {
            try
            {
                TempData tempData = Helper.GetCharacterTempData(player);

                foreach (Items item in tempData.itemlist)
                {
                    if (item != null && item.description.ToLower() == name.ToLower())
                    {
                        return item;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetItemByItemName]: " + e.ToString());
            }
            return null;
        }

        public static Items GetItemById(Player player, int id)
        {
            try
            {
                TempData tempData = Helper.GetCharacterTempData(player);
                foreach (Items item in tempData.itemlist)
                {
                    if (item != null && item.itemid == id)
                    {
                        return item;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetItemById]: " + e.ToString());
            }
            return null;
        }

        public static ItemsGlobal GetGlobalItemFromID(int itemid)
        {
            try
            {

                foreach (ItemsGlobal item in itemListGlobal)
                {
                    if (item != null && item.itemid == itemid)
                    {
                        return item;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetGlobalItemFromID]: " + e.ToString());
            }
            return null;
        }

        public static Items GetFirstSmartphone(Player player)
        {
            try
            {
                TempData tempData = Helper.GetCharacterTempData(player);

                foreach (Items item in tempData.itemlist.ToList())
                {
                    if (item != null && item.description.ToLower() == "smartphone")
                    {
                        return item;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetFirstSmartphone]: " + e.ToString());
            }
            return null;
        }

        public static Items DeleteItemWithProp(Player player, string propname)
        {
            try
            {
                TempData tempData = Helper.GetCharacterTempData(player);

                foreach (Items item in tempData.itemlist.ToList())
                {
                    if (item != null && item.props == propname)
                    {
                        RemoveItem(player, item.itemid);
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[DeleteItemWithProp]: " + e.ToString());
            }
            return null;
        }

        public static int GetFreeItemID(Player player)
        {
            try
            {
                TempData tempData = Helper.GetCharacterTempData(player);
                tempData.inventoryid++;
                int nextItemId = tempData.inventoryid;
                while (GetItemById(player, nextItemId) != null)
                {
                    tempData.inventoryid++;
                    nextItemId = tempData.inventoryid;
                }
                return nextItemId;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetFreeItemID]: " + e.ToString());
            }
            return -1;
        }

        public static ItemsGlobal CreateGlobalItemFromItem(Items item)
        {
            ItemsGlobal newItem = null;
            try
            {
                newItem = new ItemsGlobal();
                newItem.itemid = item.itemid;
                newItem.ownerid = item.ownerid;
                newItem.owneridentifier = item.owneridentifier;
                newItem.lastupdate = Helper.UnixTimestamp();
                newItem.description = item.description;
                newItem.amount = item.amount;
                newItem.weight = item.weight;
                newItem.props = item.props;
                newItem.type = item.type;
                newItem.hash = item.hash;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CreateGlobalItemFromItem]: " + e.ToString());
            }
            return newItem;
        }

        public static int GetFreeItemIDGlobal()
        {
            try
            {
                return ItemsController.itemGlobalId++;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetFreeItemIDGlobal]: " + e.ToString());
            }
            return -1;
        }

        public static bool CheckItemWithProp(Player player, string propname)
        {
            try
            {
                TempData tempData = Helper.GetCharacterTempData(player);

                foreach (Items item in tempData.itemlist.ToList())
                {
                    if (item != null && item.props == propname)
                    {
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CheckItemWithProp]: " + e.ToString());
            }
            return false;
        }

        public static void RemoveItem(Player player, int itemid)
        {
            try
            {
                TempData tempData = Helper.GetCharacterTempData(player);

                Items deleteitem = null;
                foreach (Items item in tempData.itemlist)
                {
                    if (item != null && item.itemid == itemid)
                    {
                        tempData.itemlist.Remove(item);
                        deleteitem = item;
                        break;
                    }
                }
                deleteitem = null;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[RemoveItem]: " + e.ToString());
            }
        }

        public static void RemoveItemByName(Player player, string itemname)
        {
            try
            {
                TempData tempData = Helper.GetCharacterTempData(player);

                Items deleteitem = null;
                foreach (Items item in tempData.itemlist)
                {
                    if (item != null && item.description == itemname)
                    {
                        tempData.itemlist.Remove(item);
                        deleteitem = item;
                        break;
                    }
                }
                deleteitem = null;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[RemoveItemByName]: " + e.ToString());
            }
        }

        public static void RemoveItemByProp(Player player, string prop)
        {
            try
            {
                TempData tempData = Helper.GetCharacterTempData(player);

                Items deleteitem = null;
                foreach (Items item in tempData.itemlist.ToList())
                {
                    if (item != null && item.props == prop)
                    {
                        tempData.itemlist.Remove(item);
                        deleteitem = item;
                    }
                }
                deleteitem = null;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[RemoveItemByProp]: " + e.ToString());
            }
        }

        public static void RemoveItemById(Player player, int itemid)
        {
            try
            {
                TempData tempData = Helper.GetCharacterTempData(player);

                Items deleteitem = null;
                foreach (Items item in tempData.itemlist.ToList())
                {
                    if (item != null && item.itemid == itemid)
                    {
                        tempData.itemlist.Remove(item);
                        deleteitem = item;
                        break;
                    }
                }
                deleteitem = null;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[RemoveItemById]: " + e.ToString());
            }
        }

        public static bool PlayerHasItem(Player player, string itemname)
        {
            bool hasItem = false;
            try
            {
                TempData tempData = Helper.GetCharacterTempData(player);
                foreach (Items item in tempData.itemlist)
                {
                    if (item != null && item.description == itemname && item.amount > 0)
                    {
                        hasItem = true;
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[PlayerHasItem]: " + e.ToString());
            }
            return hasItem;
        }

        public static void UpdateInventory(Player player)
        {
            try
            {
                TempData tempData = Helper.GetCharacterTempData(player);
                Character character = Helper.GetCharacterData(player);
                if (tempData.showinventory == true)
                {
                    if (!tempData.inventoysetting.Contains("furniture-") && !tempData.inventoysetting.Contains("trunk1-") && !tempData.inventoysetting.Contains("trunk2-") && !tempData.inventoysetting.Contains("evidence") && !tempData.inventoysetting.Contains("search"))
                    {
                        player.TriggerEvent("Client:UpdateInventory", NAPI.Util.ToJson(tempData.itemlist), null);
                        return;
                    }
                    else
                    {
                        TempData tempData2 = null;
                        if (tempData.inventoysetting.Contains("furniture-"))
                        {
                            //Möbelversteck
                            House house = null;
                            FurnitureSetHouse furniture = null;
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
                                furniture = Furniture.GetClosestFurniture(player, house.id, 2.65f);
                                if (furniture != null)
                                {
                                    if (furniture.extra == 1 || furniture.extra == 2 || furniture.extra == 3)
                                    {
                                        List<Items> itemListFurniture = GetAllFurnitureItems(furniture.props);
                                        player.TriggerEvent("Client:UpdateInventory", NAPI.Util.ToJson(tempData.itemlist), NAPI.Util.ToJson(itemListFurniture));
                                        return;
                                    }
                                }
                            }
                        }
                        //Durchsuchen
                        else if (tempData.inventoysetting.Contains("search"))
                        {
                            Player getPlayer = Helper.GetClosestPlayer(player, 2.5f);
                            if (getPlayer != null)
                            {
                                Character character2 = Helper.GetCharacterData(getPlayer);
                                tempData2 = Helper.GetCharacterTempData(getPlayer);
                                if (tempData2.cuffed == 1 || character2.death == true)
                                {
                                    player.TriggerEvent("Client:UpdateInventory", NAPI.Util.ToJson(tempData.itemlist), NAPI.Util.ToJson(tempData2.itemlist));
                                    return;
                                }
                            }
                        }
                        //Asservatenkammer
                        else if (tempData.inventoysetting.Contains("evidence") && character.faction == 1)
                        {
                            if (Helper.IsInRangeOfPoint(player.Position, new Vector3(474.7032, -1007.7393, 34.217087), 3.25f) || Helper.IsInRangeOfPoint(player.Position, new Vector3(620.27234, -11.100246, 76.628174), 3.25f))
                            {
                                List<Items> itemListFurniture = GetAllEvidenceItems();
                                player.TriggerEvent("Client:UpdateInventory", NAPI.Util.ToJson(tempData.itemlist), NAPI.Util.ToJson(itemListFurniture));
                                return;
                            }
                        }
                        //Kofferraum
                        else if (tempData.inventoysetting.Contains("trunk1-") || tempData.inventoysetting.Contains("trunk2-"))
                        {
                            Vehicle vehicle = null;
                            VehicleData vehicleData = null;
                            if (!player.IsInVehicle)
                            {
                                vehicle = Helper.GetClosestVehicle(player, 5.25f);
                                if (vehicle != null && vehicle.Class != 8 && vehicle.Class != 13 && vehicle.Class != 14 && vehicle.Class != 15 && vehicle.Class != 16 && vehicle.Class != 21 && vehicle.Class != 22)
                                {
                                    vehicleData = DealerShipController.GetVehicleDataByVehicle(vehicle);
                                    if (vehicleData != null)
                                    {
                                        List<Items> itemListVehicle = GetAllVehicleItems(vehicleData.id, "Trunk1");
                                        player.TriggerEvent("Client:UpdateInventory", NAPI.Util.ToJson(tempData.itemlist), NAPI.Util.ToJson(itemListVehicle));
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                vehicle = player.Vehicle;
                                if (vehicle != null && vehicle.Class != 8 && vehicle.Class != 13 && vehicle.Class != 14 && vehicle.Class != 15 && vehicle.Class != 16 && vehicle.Class != 21 && vehicle.Class != 22)
                                {
                                    vehicleData = DealerShipController.GetVehicleDataByVehicle(vehicle);
                                    if (vehicleData != null)
                                    {
                                        List<Items> itemListVehicle = GetAllVehicleItems(vehicleData.id, "Trunk2");
                                        player.TriggerEvent("Client:UpdateInventory", NAPI.Util.ToJson(tempData.itemlist), NAPI.Util.ToJson(itemListVehicle));
                                        return;
                                    }
                                }
                            }
                        }
                    }
                    player.TriggerEvent("Client:UpdateInventory", NAPI.Util.ToJson(tempData.itemlist), null);
                    return;
                }
                else
                {
                    player.TriggerEvent("Client:UpdateInventory", NAPI.Util.ToJson(tempData.itemlist), null);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[UpdateInventory]: " + e.ToString());
            }
        }

        public static int GetItemWeight(string itemname)
        {
            try
            {
                foreach (ItemsGlobal item in itemListGlobal)
                {
                    if (item.description.ToLower() == itemname.ToLower())
                    {
                        return item.weight;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetItemWeight]: " + e.ToString());
            }
            return -1;
        }

        public static void GetAllItemModels()
        {
            try
            {
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                foreach (ItemModel itemModel in db.Fetch<ItemModel>("SELECT * FROM itemmodels"))
                {
                    itemModelList.Add(itemModel);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetAllItemModels]: " + e.ToString());
            }
        }

        public static List<Items> GetAllFurnitureItems(string props)
        {
            List<Items> itemListFurniture = new List<Items>();
            try
            {
                foreach (ItemsGlobal globalitem in itemListGlobal.ToList())
                {
                    if (globalitem != null && globalitem.owneridentifier == "Furniture-" + props)
                    {
                        Items item = new Items();
                        item.itemid = globalitem.itemid;
                        item.ownerid = globalitem.ownerid;
                        item.owneridentifier = globalitem.owneridentifier;
                        item.description = globalitem.description;
                        item.amount = globalitem.amount;
                        item.weight = globalitem.weight;
                        item.props = globalitem.props;
                        item.type = globalitem.type;
                        item.hash = globalitem.hash;
                        itemListFurniture.Add(item);
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetAllFurnitureItems]: " + e.ToString());
            }
            return itemListFurniture;
        }

        public static List<Items> GetAllVehicleItems(int props, string check)
        {
            List<Items> itemListVehicle = new List<Items>();
            try
            {
                foreach (ItemsGlobal globalitem in itemListGlobal.ToList())
                {
                    if (globalitem != null && globalitem.owneridentifier == check + "-" + props)
                    {
                        Items item = new Items();
                        item.itemid = globalitem.itemid;
                        item.ownerid = globalitem.ownerid;
                        item.owneridentifier = globalitem.owneridentifier;
                        item.description = globalitem.description;
                        item.amount = globalitem.amount;
                        item.weight = globalitem.weight;
                        item.props = globalitem.props;
                        item.type = globalitem.type;
                        item.hash = globalitem.hash;
                        itemListVehicle.Add(item);
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetAllVehicleItems]: " + e.ToString());
            }
            return itemListVehicle;
        }

        public static List<Items> GetAllEvidenceItems()
        {
            List<Items> itemListEvidence = new List<Items>();
            try
            {
                foreach (ItemsGlobal globalitem in itemListGlobal.ToList())
                {
                    if (globalitem != null && globalitem.owneridentifier == "evidence")
                    {
                        Items item = new Items();
                        item.itemid = globalitem.itemid;
                        item.ownerid = globalitem.ownerid;
                        item.owneridentifier = globalitem.owneridentifier;
                        item.description = globalitem.description;
                        item.amount = globalitem.amount;
                        item.weight = globalitem.weight;
                        item.props = globalitem.props;
                        item.type = globalitem.type;
                        item.hash = globalitem.hash;
                        itemListEvidence.Add(item);
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetAllEvidenceItems]: " + e.ToString());
            }
            return itemListEvidence;
        }

        public static int CountEvidenceItems()
        {
            int count = 0;
            try
            {
                foreach (ItemsGlobal globalitem in itemListGlobal.ToList())
                {
                    if (globalitem != null && globalitem.owneridentifier == "evidence")
                    {
                        count++;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetAllEvidenceItems]: " + e.ToString());
            }
            return count;
        }

        public static int GetFurnitureWeight(int extra)
        {
            int weight = 10000;
            try
            {
                if (extra == 2)
                {
                    weight = 20000;
                }
                else
                {
                    weight = 30000;
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetFurnitureWeight]: " + e.ToString());
            }
            return weight;
        }

        [RemoteEvent("Server:ShowInventory")]
        public static void OnShowInventory(Player player, int trunk = 0, Vehicle vehicle = null)
        {
            try
            {
                TempData tempData = Helper.GetCharacterTempData(player);
                Character character = Helper.GetCharacterData(player);
                Character character2 = null;
                VehicleData vehicleData = null;
                if (tempData.freezed == true) return;
                if (tempData.showinventory == false)
                {
                    //Wenn ein Item benutzt wird
                    if (player.HasData("Player:Use") && player.GetData<bool>("Player:Use") == true) return;
                    //Möbelversteck
                    House house = null;
                    FurnitureSetHouse furniture = null;
                    bool furnitreItems = false;
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
                        furniture = Furniture.GetClosestFurniture(player, house.id, 2.65f);
                        if (furniture != null)
                        {
                            if (furniture.extra == 1 || furniture.extra == 2 || furniture.extra == 3)
                            {
                                furnitreItems = true;
                            }
                        }
                    }
                    //Durchsuchen
                    TempData tempData2 = null;
                    bool search = false;
                    if (!player.IsInVehicle && trunk == 2)
                    {
                        Player getPlayer = Helper.GetClosestPlayer(player, 2.5f);
                        if (getPlayer != null)
                        {
                            tempData2 = Helper.GetCharacterTempData(getPlayer);
                            character2 = Helper.GetCharacterData(getPlayer);
                            if (tempData2.cuffed == 1 || character2.death == true)
                            {
                                search = true;
                            }
                        }
                    }
                    //Durchsuchen (Admin)
                    bool search2 = false;
                    if (!player.IsInVehicle && trunk == 3)
                    {
                        Player getPlayer = player.GetData<Player>("Player:GetPlayer");
                        if (getPlayer != null)
                        {
                            tempData2 = Helper.GetCharacterTempData(getPlayer);
                            character2 = Helper.GetCharacterData(getPlayer);
                            search2 = true;
                        }
                    }
                    //Asservatenkammer
                    bool evidence = false;
                    if ((Helper.IsInRangeOfPoint(player.Position, new Vector3(474.7032, -1007.7393, 34.217087), 3.25f) || Helper.IsInRangeOfPoint(player.Position, new Vector3(620.27234, -11.100246, 76.628174), 3.25f)) && character.faction == 1)
                    {
                        evidence = true;
                    }
                    //Kofferraum
                    bool trunk1Items = false;
                    bool trunk2Items = false;
                    if (!player.IsInVehicle && trunk == 1)
                    {
                        if (vehicle != null && vehicle.Class != 8 && vehicle.Class != 13 && vehicle.Class != 14 && vehicle.Class != 15 && vehicle.Class != 16 && vehicle.Class != 21 && vehicle.Class != 22)
                        {
                            vehicleData = DealerShipController.GetVehicleDataByVehicle(vehicle);
                            if (vehicleData != null)
                            {
                                trunk1Items = true;
                            }
                        }
                    }
                    if (trunk1Items == false && vehicle != null && vehicle.Locked == false && (vehicle.GetSharedData<string>("Vehicle:Name").ToLower() == "stockade" || vehicle.GetSharedData<string>("Vehicle:Name").ToLower() == "firetruk") && player.Position.DistanceTo(Helper.GetPositionBehindOfVehicle(vehicle, 3.15f)) <= 2.15f)
                    {
                        vehicleData = DealerShipController.GetVehicleDataByVehicle(vehicle);
                        if (vehicleData != null)
                        {
                            trunk1Items = true;
                        }
                    }
                    if (trunk1Items == false && player.IsInVehicle && (player.VehicleSeat == 0 || player.VehicleSeat == 1))
                    {
                        vehicle = player.Vehicle;
                        if (vehicle != null && vehicle.Class != 8 && vehicle.Class != 13 && vehicle.Class != 14 && vehicle.Class != 15 && vehicle.Class != 16 && vehicle.Class != 21 && vehicle.Class != 22)
                        {
                            vehicleData = DealerShipController.GetVehicleDataByVehicle(vehicle);
                            if (vehicleData != null)
                            {
                                trunk2Items = true;
                            }
                        }
                    }
                    foreach (Items iteminlist in tempData.itemlist)
                    {
                        if (iteminlist != null && iteminlist.type == 5)
                        {
                            string[] weaponArray = new string[7];
                            weaponArray = iteminlist.props.Split(",");
                            if (weaponArray[1] == "1")
                            {
                                if (!iteminlist.description.ToLower().Contains("schutzweste"))
                                {
                                    iteminlist.props = $"{NAPI.Player.GetPlayerWeaponAmmo(player, (WeaponHash)WeaponController.GetWeaponHashFromName(iteminlist.description))},1,{weaponArray[2]},{weaponArray[3]},{weaponArray[4]},{weaponArray[5]},{weaponArray[6]}";
                                    if (iteminlist.description.ToLower() == "granate" || iteminlist.description.ToLower() == "bzgas" || iteminlist.description.ToLower() == "rauchgranate" || iteminlist.description.ToLower() == "molotowcocktail" || iteminlist.description.ToLower() == "snowball")
                                    {
                                        iteminlist.amount = NAPI.Player.GetPlayerWeaponAmmo(player, (WeaponHash)WeaponController.GetWeaponHashFromName(iteminlist.description));
                                    }
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
                    if (furnitreItems == false && trunk1Items == false && trunk2Items == false && evidence == false && search == false && search2 == false)
                    {
                        player.TriggerEvent("Client:ShowInventory", NAPI.Util.ToJson(tempData.itemlist), MAX_INVENTORY_PLAYER, true, null, null, null);
                        tempData.showinventory = true;
                    }
                    else
                    {
                        if (furnitreItems == true)
                        {
                            foreach (Player p in NAPI.Pools.GetAllPlayers())
                            {
                                if (p != null && p != player && p.GetOwnSharedData<bool>("Player:Spawned") == true)
                                {
                                    tempData2 = Helper.GetCharacterTempData(p);
                                    if (tempData2 != null && tempData2.inventoysetting == "furniture-" + furniture.id)
                                    {
                                        Helper.SendNotificationWithoutButton(player, "Dieses Versteck wird gerade schon benutzt!", "error", "top-end");
                                        furnitreItems = false;
                                        return;
                                    }
                                }
                            }
                            tempData.inventoysetting = "furniture-" + furniture.id;
                            List<Items> itemListFurniture = GetAllFurnitureItems(furniture.props);
                            int weight2 = GetFurnitureWeight(furniture.extra);
                            player.TriggerEvent("Client:ShowInventory", NAPI.Util.ToJson(tempData.itemlist), MAX_INVENTORY_PLAYER, true, NAPI.Util.ToJson(itemListFurniture), weight2, "Möbelversteck");
                            tempData.showinventory = true;
                        }
                        else if (evidence == true)
                        {
                            foreach (Player p in NAPI.Pools.GetAllPlayers())
                            {
                                if (p != null && p != player && p.GetOwnSharedData<bool>("Player:Spawned") == true)
                                {
                                    tempData2 = Helper.GetCharacterTempData(p);
                                    if (tempData2 != null && tempData2.inventoysetting == "evidence")
                                    {
                                        Helper.SendNotificationWithoutButton(player, "Die Asservatenkammer wird gerade schon benutzt!", "error", "top-end");
                                        evidence = false;
                                        return;
                                    }
                                }
                            }
                            tempData.inventoysetting = "evidence";
                            List<Items> itemListEvidence = GetAllEvidenceItems();
                            int weight2 = 85000;
                            player.TriggerEvent("Client:ShowInventory", NAPI.Util.ToJson(tempData.itemlist), MAX_INVENTORY_PLAYER, true, NAPI.Util.ToJson(itemListEvidence), weight2, "Asservatenkammer");
                            tempData.showinventory = true;
                        }
                        else if (search == true)
                        {
                            tempData.inventoysetting = "search";
                            player.TriggerEvent("Client:ShowInventory", NAPI.Util.ToJson(tempData.itemlist), MAX_INVENTORY_PLAYER, true, NAPI.Util.ToJson(tempData2.itemlist), MAX_INVENTORY_PLAYER, "Andere Taschen");
                            tempData.showinventory = true;
                        }
                        else if (search2 == true)
                        {
                            tempData.inventoysetting = "search2";
                            player.TriggerEvent("Client:ShowInventory", NAPI.Util.ToJson(tempData.itemlist), MAX_INVENTORY_PLAYER, true, NAPI.Util.ToJson(tempData2.itemlist), MAX_INVENTORY_PLAYER, $"Taschen von {character2.name}");
                            tempData.showinventory = true;
                        }
                        else if (trunk1Items == true)
                        {
                            foreach (Player p in NAPI.Pools.GetAllPlayers())
                            {
                                if (p != null && p != player && p.GetOwnSharedData<bool>("Player:Spawned") == true)
                                {
                                    tempData2 = Helper.GetCharacterTempData(p);
                                    if (tempData2 != null && tempData2.inventoysetting == "trunk1-" + vehicle.Id)
                                    {
                                        Helper.SendNotificationWithoutButton(player, "Dieser Kofferraum wird gerade schon benutzt!", "error", "top-end");
                                        trunk1Items = false;
                                        return;
                                    }
                                }
                            }
                            tempData.inventoysetting = "trunk1-" + vehicle.Id;
                            List<Items> itemListVehicle = GetAllVehicleItems(vehicleData.id, "Trunk1");
                            int weight2 = Cars.GetVehicleTrunk(vehicle) * 1000;
                            player.TriggerEvent("Client:ShowInventory", NAPI.Util.ToJson(tempData.itemlist), MAX_INVENTORY_PLAYER, true, NAPI.Util.ToJson(itemListVehicle), weight2, "Kofferraum");
                            tempData.showinventory = true;
                        }
                        else if (trunk2Items == true)
                        {
                            foreach (Player p in NAPI.Pools.GetAllPlayers())
                            {
                                if (p != null && p != player && p.GetOwnSharedData<bool>("Player:Spawned") == true)
                                {
                                    tempData2 = Helper.GetCharacterTempData(p);
                                    if (tempData2 != null && tempData2.inventoysetting == "trunk2-" + vehicle.Id)
                                    {
                                        Helper.SendNotificationWithoutButton(player, "Die Mittelkonsole wird gerade schon benutzt!", "error", "top-end");
                                        trunk2Items = false;
                                        return;

                                    }
                                }
                            }
                            tempData.inventoysetting = "trunk2-" + vehicle.Id;
                            List<Items> itemListVehicle = GetAllVehicleItems(vehicleData.id, "Trunk2");
                            int weight2 = 5000;
                            player.TriggerEvent("Client:ShowInventory", NAPI.Util.ToJson(tempData.itemlist), MAX_INVENTORY_PLAYER, true, NAPI.Util.ToJson(itemListVehicle), weight2, "Mittelkonsole");
                            tempData.showinventory = true;
                        }
                    }
                }
                else
                {
                    tempData.inventoysetting = "nothing";
                    player.TriggerEvent("Client:ShowInventory", NAPI.Util.ToJson(tempData.itemlist), MAX_INVENTORY_PLAYER, false, null, null, null);
                    tempData.showinventory = false;
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[ShowInventory]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:PlayerPickUp")]
        public static void OnPlayerPickUp(Player player)
        {
            try
            {
                bool found = false;
                ItemsGlobal deleteGlobalItem = null;
                if (NAPI.Player.GetPlayerCurrentWeapon(player) != WeaponHash.Knife)
                {
                    foreach (ItemsGlobal globalitem in itemListGlobal)
                    {
                        if (globalitem != null && globalitem.owneridentifier == "Ground")
                        {
                            if (Helper.IsInRangeOfPoint(player.Position, new Vector3(globalitem.posx, globalitem.posy, globalitem.posz), 2.0f) && player.Dimension == globalitem.dimension)
                            {
                                if (!ItemsController.CanPlayerHoldItem(player, (globalitem.weight * globalitem.amount)))
                                {
                                    Helper.SendNotificationWithoutButton(player, "Du kannst das Item nichtmehr tragen!", "error", "top-end");
                                    return;
                                }
                                Items snowBallItem = ItemsController.GetItemByItemName(player, "Snowball");
                                if (snowBallItem != null && globalitem.description == "Snowball" && snowBallItem.amount + globalitem.amount > 10)
                                {
                                    Helper.SendNotificationWithoutButton(player, "Soviele Snowballs kannst du nicht mehr tragen!", "error", "top-end");
                                    return;
                                }
                                itemListGlobal.Remove(globalitem);
                                if (globalitem.objectHandle != null)
                                {
                                    globalitem.objectHandle.Delete();
                                    globalitem.objectHandle = null;
                                    globalitem.textHandle.Delete();
                                    globalitem.textHandle = null;
                                }
                                globalitem.posx = 0.0f;
                                globalitem.posy = 0.0f;
                                globalitem.posz = 0.0f;
                                globalitem.dimension = 0;
                                TempData tempData = Helper.GetCharacterTempData(player);
                                Character character = Helper.GetCharacterData(player);
                                if (!CheckDoubleItems(globalitem.description))
                                {
                                    foreach (Items iteminlist in tempData.itemlist)
                                    {
                                        if (iteminlist != null && globalitem.description.ToLower() == iteminlist.description.ToLower())
                                        {
                                            iteminlist.amount += globalitem.amount;
                                            if (iteminlist.description.ToLower() == "granate" || iteminlist.description.ToLower() == "bzgas" || iteminlist.description.ToLower() == "rauchgranate" || iteminlist.description.ToLower() == "molotowcocktail" || iteminlist.description.ToLower() == "snowball")
                                            {
                                                string[] weaponArray = new string[7];
                                                weaponArray = iteminlist.props.Split(",");
                                                if (weaponArray[1] == "1")
                                                {
                                                    weaponArray[0] = Convert.ToString(NAPI.Player.GetPlayerWeaponAmmo(player, (WeaponHash)WeaponController.GetWeaponHashFromName(iteminlist.description)));
                                                }
                                                iteminlist.props = $"{Convert.ToInt32(weaponArray[0]) + globalitem.amount},{weaponArray[1]},{weaponArray[2]},{weaponArray[3]},{weaponArray[4]},{weaponArray[5]},{weaponArray[6]}";
                                            }
                                            found = true;
                                            break;
                                        }
                                    }
                                }
                                if (found == false)
                                {
                                    Items item = new Items();
                                    item.itemid = ItemsController.GetFreeItemID(player);
                                    item.ownerid = character.id;
                                    item.owneridentifier = "Player";
                                    item.description = globalitem.description;
                                    item.amount = globalitem.amount;
                                    item.weight = globalitem.weight;
                                    item.props = globalitem.props;
                                    item.type = globalitem.type;
                                    item.hash = globalitem.hash;
                                    tempData.itemlist.Add(item);
                                }
                                UpdateInventory(player);
                                deleteGlobalItem = globalitem;
                                Helper.SendNotificationWithoutButton(player, "Du hast " + globalitem.amount + "x  " + globalitem.description + " aufgehoben!", "success");
                                Helper.PlayShortAnimation(player, "random@domestic", "pickup_low", 1650);
                                deleteGlobalItem = null;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    foreach (Ped ped in HuntingController.SpawnedAnimals)
                    {
                        if (ped.Position.DistanceTo(player.Position) < 1.95 && ped.GetSharedData<int>("Ped:Death") == 1)
                        {
                            player.SetData<Ped>("Player:TempPed", ped);
                            ped.SetSharedData("Ped:Death", 4);
                            player.TriggerEvent("Client:StartLockpicking", 9, "animal1", "Fleisch wird gesammelt...");
                            return;
                        }
                    }
                }
                DrugController.InteractWithDrugPlant(player, 1);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnPlayerPickUp]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:UseInventory")]
        public static void OnUseInventory(Player player, string usage, int itemid, int amount = 1, string selection = "left")
        {
            try
            {
                if (itemid < 0)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiges Item!", "error");
                    return;
                }
                Items item = GetItemFromID(player, itemid);
                if (usage != "move" && item == null)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültiges Item!", "error");
                    return;
                }
                if (usage != "move" && amount > item.amount || amount <= 0)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültige Menge!", "error");
                    return;
                }
                Character character = Helper.GetCharacterData(player);
                TempData tempData = Helper.GetCharacterTempData(player);
                Character character2 = null;
                switch (usage)
                {
                    case "move":
                        {
                            House house = null;
                            FurnitureSetHouse furniture = null;
                            bool furnitreItems = false;
                            if (tempData.inventoysetting.Contains("furniture-"))
                            {
                                //Möbelversteck
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
                                    furniture = Furniture.GetClosestFurniture(player, house.id, 2.65f);
                                    if (furniture != null)
                                    {
                                        if (furniture.extra == 1 || furniture.extra == 2 || furniture.extra == 3)
                                        {
                                            furnitreItems = true;
                                        }
                                    }

                                }
                            }
                            //Durchsuchen
                            TempData tempData2 = null;
                            Player getPlayer = null;
                            bool search = false;
                            if (tempData.inventoysetting.Contains("search"))
                            {
                                getPlayer = Helper.GetClosestPlayer(player, 2.5f);
                                if (getPlayer != null)
                                {
                                    character2 = Helper.GetCharacterData(getPlayer);
                                    tempData2 = Helper.GetCharacterTempData(getPlayer);
                                    if (tempData2.cuffed == 1 || character2.death == true)
                                    {
                                        search = true;
                                    }
                                }
                            }
                            //Durchsuchen Admin
                            bool search2 = false;
                            if (tempData.inventoysetting.Contains("search2"))
                            {
                                getPlayer = player.GetData<Player>("Player:GetPlayer");
                                if (getPlayer != null)
                                {
                                    character2 = Helper.GetCharacterData(getPlayer);
                                    tempData2 = Helper.GetCharacterTempData(getPlayer);
                                    search2 = true;
                                }
                            }
                            //Asservatenkammer
                            bool evidence = false;
                            if (tempData.inventoysetting.Contains("evidence") && character.faction == 1)
                            {
                                if ((Helper.IsInRangeOfPoint(player.Position, new Vector3(474.7032, -1007.7393, 34.217087), 3.25f) || Helper.IsInRangeOfPoint(player.Position, new Vector3(620.27234, -11.100246, 76.628174), 3.25f)) && character.faction == 1)
                                {
                                    evidence = true;
                                }
                            }
                            Cars getCar = null;
                            bool trunk1Items = false;
                            bool trunk2Items = false;
                            Vehicle vehicle = null;
                            VehicleData vehicleData = null;
                            if (tempData.inventoysetting.Contains("trunk1-") || tempData.inventoysetting.Contains("trunk2-"))
                            {
                                //Kofferraum
                                if (!player.IsInVehicle)
                                {
                                    vehicle = Helper.GetClosestVehicle(player, 5.25f);
                                    if (vehicle != null && vehicle.Class != 8 && vehicle.Class != 13 && vehicle.Class != 14 && vehicle.Class != 15 && vehicle.Class != 16 && vehicle.Class != 21 && vehicle.Class != 22)
                                    {
                                        vehicleData = DealerShipController.GetVehicleDataByVehicle(vehicle);
                                        if (vehicleData != null)
                                        {
                                            trunk1Items = true;
                                        }
                                    }
                                }
                                //Mittelkonsole
                                else
                                {
                                    vehicle = player.Vehicle;
                                    if (vehicle != null && vehicle.Class != 8 && vehicle.Class != 13 && vehicle.Class != 14 && vehicle.Class != 15 && vehicle.Class != 16 && vehicle.Class != 21 && vehicle.Class != 22)
                                    {
                                        vehicleData = DealerShipController.GetVehicleDataByVehicle(vehicle);
                                        if (vehicleData != null)
                                        {
                                            trunk2Items = true;
                                        }
                                    }
                                }
                                foreach (Cars car in Cars.carList)
                                {
                                    if (car.vehicleHandle != null && car.vehicleHandle == vehicle && car.vehicleData != null)
                                    {
                                        getCar = car;
                                        break;
                                    }
                                }
                            }
                            if (selection == "right")
                            {
                                ItemsGlobal itemGlobal = null;
                                Items getItem = null;
                                if (tempData.inventoysetting.Contains("search"))
                                {
                                    getPlayer = Helper.GetClosestPlayer(player, 2.5f);
                                    getItem = ItemsController.GetItemFromID(getPlayer, itemid);
                                }
                                else
                                {
                                    itemGlobal = GetGlobalItemFromID(itemid);
                                }
                                if (tempData.inventoysetting.Contains("furniture-") && (itemGlobal == null || furnitreItems == false))
                                {
                                    Helper.SendNotificationWithoutButton(player, "Ungültiges Item!", "error");
                                    return;
                                }
                                if (tempData.inventoysetting.Contains("trunk1-") && (itemGlobal == null || trunk1Items == false))
                                {
                                    Helper.SendNotificationWithoutButton(player, "Ungültiges Item!", "error");
                                    return;
                                }
                                if (tempData.inventoysetting.Contains("trunk2-") && (itemGlobal == null || trunk2Items == false))
                                {
                                    Helper.SendNotificationWithoutButton(player, "Ungültiges Item!", "error");
                                    return;
                                }
                                if (tempData.inventoysetting.Contains("evidence") && (itemGlobal == null || evidence == false))
                                {
                                    Helper.SendNotificationWithoutButton(player, "Ungültiges Item!", "error");
                                    return;
                                }
                                if (tempData.inventoysetting.Contains("search") && (getItem == null || search == false))
                                {
                                    Helper.SendNotificationWithoutButton(player, "Ungültiges Item!", "error");
                                    return;
                                }
                                if (tempData.inventoysetting.Contains("search2") && (getItem == null || search2 == false))
                                {
                                    Helper.SendNotificationWithoutButton(player, "Ungültiges Item!", "error");
                                    return;
                                }
                                if (tempData.inventoysetting.Contains("search"))
                                {
                                    if (getItem.description.ToLower() == "smartphone" && tempData2.inCall == true)
                                    {
                                        Helper.SendNotificationWithoutButton(player, $"Während des Telefonprozesses kannst du keine Handys bewegen!", "error", "top-end");
                                        return;
                                    }
                                }
                                else
                                {
                                    if (itemGlobal.description.ToLower() == "smartphone" && tempData.inCall == true)
                                    {
                                        Helper.SendNotificationWithoutButton(player, $"Während des Telefonprozesses kannst du keine Handys bewegen!", "error", "top-end");
                                        return;
                                    }
                                }
                                if (search == false && search2 == false)
                                {
                                    if (itemGlobal.type == 5)
                                    {
                                        string[] weaponArray = new string[7];
                                        weaponArray = itemGlobal.props.Split(",");
                                        if (weaponArray[1] == "1")
                                        {
                                            Helper.SendNotificationWithoutButton(player, $"Dieses Item kann jetzt nicht bewegt werden!", "error", "top-end");
                                            return;
                                        }
                                    }
                                    if (amount > itemGlobal.amount || amount <= 0)
                                    {
                                        Helper.SendNotificationWithoutButton(player, "Ungültige Menge!", "error");
                                        return;
                                    }
                                    if (!CanPlayerHoldItem(player, (itemGlobal.weight * itemGlobal.amount)))
                                    {
                                        Helper.SendNotificationWithoutButton(player, "Du kannst das Item nichtmehr tragen!", "error", "top-end");
                                        return;
                                    }
                                    bool setdouble = false;
                                    if (!CheckDoubleItems(itemGlobal.description))
                                    {
                                        foreach (Items iteminlist in tempData.itemlist)
                                        {
                                            if (iteminlist != null && itemGlobal.description.ToLower() == iteminlist.description.ToLower())
                                            {
                                                iteminlist.amount += amount;
                                                itemGlobal.amount -= amount;
                                                if (itemGlobal.description.ToLower() == "granate" || itemGlobal.description.ToLower() == "bzgas" || itemGlobal.description.ToLower() == "rauchgranate" || itemGlobal.description.ToLower() == "molotowcocktail" || itemGlobal.description.ToLower() == "snowball")
                                                {
                                                    string[] weaponArray = new string[7];
                                                    weaponArray = itemGlobal.props.Split(",");
                                                    itemGlobal.props = $"{itemGlobal.amount},{weaponArray[1]},{weaponArray[2]},{weaponArray[3]},{weaponArray[4]},{weaponArray[5]},{weaponArray[6]}";
                                                    weaponArray = iteminlist.props.Split(",");
                                                    iteminlist.props = $"{iteminlist.amount},{weaponArray[1]},{weaponArray[2]},{weaponArray[3]},{weaponArray[4]},{weaponArray[5]},{weaponArray[6]}";
                                                }
                                                setdouble = true;
                                                break;
                                            }
                                        }
                                    }
                                    if (setdouble == false)
                                    {
                                        Items newItem = new Items();
                                        newItem.itemid = GetFreeItemID(player);
                                        newItem.ownerid = character.id;
                                        newItem.owneridentifier = "Player";
                                        newItem.description = itemGlobal.description;
                                        newItem.amount = amount;
                                        itemGlobal.amount -= amount;
                                        newItem.weight = itemGlobal.weight;
                                        if (itemGlobal.description.ToLower() == "granate" || itemGlobal.description.ToLower() == "bzgas" || itemGlobal.description.ToLower() == "rauchgranate" || itemGlobal.description.ToLower() == "molotowcocktail" || itemGlobal.description.ToLower() == "snowball")
                                        {
                                            string[] weaponArray = new string[7];
                                            weaponArray = itemGlobal.props.Split(",");
                                            itemGlobal.props = $"{itemGlobal.amount},{weaponArray[1]},{weaponArray[2]},{weaponArray[3]},{weaponArray[4]},{weaponArray[5]},{weaponArray[6]}";
                                            newItem.props = $"{newItem.amount},{weaponArray[1]},{weaponArray[2]},{weaponArray[3]},{weaponArray[4]},{weaponArray[5]},{weaponArray[6]}";
                                        }
                                        else
                                        {
                                            newItem.props = itemGlobal.props;
                                        }
                                        newItem.type = itemGlobal.type;
                                        newItem.hash = itemGlobal.hash;
                                        tempData.itemlist.Add(newItem);
                                    }
                                    if (tempData.inventoysetting.Contains("evidence"))
                                    {
                                        Helper.CreateEvidenceLog($"{character.name} hat {amount}x {itemGlobal.description} aus der Asservatenkammer genommen!");
                                    }
                                    if (tempData.inventoysetting.Contains("search"))
                                    {
                                        Helper.CreateAdminLog("searchlog", $"{character.name} hat {character2.name} {amount}x {itemGlobal.description} entnommen!");
                                    }
                                    if (itemGlobal.amount <= 0)
                                    {
                                        itemListGlobal.Remove(itemGlobal);
                                        itemGlobal = null;
                                    }
                                }
                                else
                                {
                                    if (getItem.type == 5)
                                    {
                                        string[] weaponArray = new string[7];
                                        weaponArray = getItem.props.Split(",");
                                        if (weaponArray[1] == "1")
                                        {
                                            Helper.SendNotificationWithoutButton(player, $"Dieses Item kann jetzt nicht bewegt werden!", "error", "top-end");
                                            return;
                                        }
                                    }
                                    if (amount > getItem.amount || amount <= 0)
                                    {
                                        Helper.SendNotificationWithoutButton(player, "Ungültige Menge!", "error");
                                        return;
                                    }
                                    if (!CanPlayerHoldItem(player, (getItem.weight * getItem.amount)))
                                    {
                                        Helper.SendNotificationWithoutButton(player, "Du kannst das Item nichtmehr tragen!", "error", "top-end");
                                        return;
                                    }
                                    bool setdouble = false;
                                    if (!CheckDoubleItems(getItem.description))
                                    {
                                        foreach (Items iteminlist in tempData.itemlist)
                                        {
                                            if (iteminlist != null && getItem.description.ToLower() == iteminlist.description.ToLower())
                                            {
                                                iteminlist.amount += amount;
                                                getItem.amount -= amount;
                                                if (getItem.description.ToLower() == "granate" || getItem.description.ToLower() == "bzgas" || getItem.description.ToLower() == "rauchgranate" || getItem.description.ToLower() == "molotowcocktail" || getItem.description.ToLower() == "snowball")
                                                {
                                                    string[] weaponArray = new string[7];
                                                    weaponArray = getItem.props.Split(",");
                                                    getItem.props = $"{getItem.amount},{weaponArray[1]},{weaponArray[2]},{weaponArray[3]},{weaponArray[4]},{weaponArray[5]},{weaponArray[6]}";
                                                    weaponArray = iteminlist.props.Split(",");
                                                    iteminlist.props = $"{iteminlist.amount},{weaponArray[1]},{weaponArray[2]},{weaponArray[3]},{weaponArray[4]},{weaponArray[5]},{weaponArray[6]}";
                                                }
                                                setdouble = true;
                                                break;
                                            }
                                        }
                                    }
                                    if (setdouble == false)
                                    {
                                        Items newItem = new Items();
                                        newItem.itemid = GetFreeItemID(player);
                                        newItem.ownerid = character.id;
                                        newItem.owneridentifier = "Player";
                                        newItem.description = getItem.description;
                                        newItem.amount = amount;
                                        getItem.amount -= amount;
                                        newItem.weight = getItem.weight;
                                        if (getItem.description.ToLower() == "granate" || getItem.description.ToLower() == "bzgas" || getItem.description.ToLower() == "rauchgranate" || getItem.description.ToLower() == "molotowcocktail" || getItem.description.ToLower() == "snowball")
                                        {
                                            string[] weaponArray = new string[7];
                                            weaponArray = getItem.props.Split(",");
                                            getItem.props = $"{getItem.amount},{weaponArray[1]},{weaponArray[2]},{weaponArray[3]},{weaponArray[4]},{weaponArray[5]},{weaponArray[6]}";
                                            newItem.props = $"{newItem.amount},{weaponArray[1]},{weaponArray[2]},{weaponArray[3]},{weaponArray[4]},{weaponArray[5]},{weaponArray[6]}";
                                        }
                                        else
                                        {
                                            newItem.props = getItem.props;
                                        }
                                        newItem.type = getItem.type;
                                        newItem.hash = getItem.hash;
                                        tempData.itemlist.Add(newItem);
                                    }
                                    if (tempData.inventoysetting.Contains("evidence"))
                                    {
                                        Helper.CreateEvidenceLog($"{character.name} hat {amount}x {getItem.description} aus der Asservatenkammer genommen!");
                                    }
                                    if (tempData.inventoysetting.Contains("search"))
                                    {
                                        Helper.CreateAdminLog("searchlog", $"{character.name} hat {character2.name} {amount}x {getItem.description} gegeben!");
                                    }
                                    if (getItem.description == "Smartphone" && getItem.props == character.lastsmartphone)
                                    {
                                        character.lastsmartphone = "n/A";
                                    }
                                    if (tempData.inventoysetting.Contains("trunk") && getCar != null)
                                    {
                                        DealerShipController.SaveOneVehicleData(getCar);
                                    }
                                    if (getItem.amount <= 0)
                                    {
                                        tempData2.itemlist.Remove(getItem);
                                        getItem = null;
                                    }
                                }
                                Commands.cmd_animation(player, "give", true);
                                Helper.SendNotificationWithoutButton(player, "Das Item wurde verschoben!", "success", "top-end");
                                CharacterController.SaveCharacter(player);

                            }
                            else
                            {
                                if (tempData.inventoysetting.Contains("search"))
                                {
                                    Helper.SendNotificationWithoutButton(player, "Deine eigenen Items kannst du jetzt nicht verschieben!", "error");
                                    return;
                                }
                                if (tempData.inventoysetting.Contains("furniture-") && (item == null || furniture == null || house == null || furnitreItems == false))
                                {
                                    Helper.SendNotificationWithoutButton(player, "Ungültiges Item!", "error");
                                    return;
                                }
                                if (tempData.inventoysetting.Contains("trunk1-") && (item == null || vehicle == null || vehicleData == null || trunk1Items == false))
                                {
                                    Helper.SendNotificationWithoutButton(player, "Ungültiges Item!", "error");
                                    return;
                                }
                                if (tempData.inventoysetting.Contains("trunk2-") && (item == null || vehicle == null || vehicleData == null || trunk2Items == false))
                                {
                                    Helper.SendNotificationWithoutButton(player, "Ungültiges Item!", "error");
                                    return;
                                }
                                if (tempData.inventoysetting.Contains("evidence") && (item == null || evidence == false))
                                {
                                    Helper.SendNotificationWithoutButton(player, "Ungültiges Item!", "error");
                                    return;
                                }
                                if (tempData.inventoysetting.Contains("search") && (item == null || search == false))
                                {
                                    Helper.SendNotificationWithoutButton(player, "Ungültiges Item!", "error");
                                    return;
                                }
                                if (tempData.inventoysetting.Contains("search2") && (item == null || search2 == false))
                                {
                                    Helper.SendNotificationWithoutButton(player, "Ungültiges Item!", "error");
                                    return;
                                }
                                if (item.description.ToLower() == "smartphone" && tempData.inCall == true)
                                {
                                    Helper.SendNotificationWithoutButton(player, $"Während des Telefonprozesses kannst du keine Handys bewegen!", "error", "top-end");
                                    return;
                                }
                                if (item.type == 5)
                                {
                                    string[] weaponArray = new string[7];
                                    weaponArray = item.props.Split(",");
                                    if (weaponArray[1] == "1")
                                    {
                                        Helper.SendNotificationWithoutButton(player, $"Dieses Item kann jetzt nicht bewegt werden!", "error", "top-end");
                                        return;
                                    }
                                }
                                if (amount > item.amount || amount <= 0)
                                {
                                    Helper.SendNotificationWithoutButton(player, "Ungültige Menge!", "error");
                                    return;
                                }
                                if (tempData.inventoysetting.Contains("furniture-") && !FurnitureCanHoldItem(furniture, (item.weight * amount)))
                                {
                                    Helper.SendNotificationWithoutButton(player, "Das Item passt nicht mehr rein!", "error", "top-end");
                                    return;
                                }
                                if (tempData.inventoysetting.Contains("trunk1-") && !VehicleCanHoldItem(vehicle, vehicleData.id, (item.weight * amount), true))
                                {
                                    Helper.SendNotificationWithoutButton(player, "Das Item passt nicht mehr rein!", "error", "top-end");
                                    return;
                                }
                                if (tempData.inventoysetting.Contains("trunk2-") && !VehicleCanHoldItem(vehicle, vehicleData.id, (item.weight * amount), false))
                                {
                                    Helper.SendNotificationWithoutButton(player, "Das Item passt nicht mehr rein!", "error", "top-end");
                                    return;
                                }
                                if (tempData.inventoysetting.Contains("evidence") && !EvidenceCanHoldItem(item.weight * amount))
                                {
                                    Helper.SendNotificationWithoutButton(player, "Das Item passt nicht mehr rein!", "error", "top-end");
                                    return;
                                }
                                bool setdouble = false;
                                if (tempData.inventoysetting.Contains("furniture-") || tempData.inventoysetting.Contains("trunk1-") || tempData.inventoysetting.Contains("trunk2-") || tempData.inventoysetting.Contains("evidence") || tempData.inventoysetting.Contains("search"))
                                {
                                    string identifier = "";
                                    string props = "";
                                    if (tempData.inventoysetting.Contains("furniture-"))
                                    {
                                        identifier = "Furniture-";
                                        props = furniture.props;
                                    }
                                    else if (tempData.inventoysetting.Contains("trunk1-"))
                                    {
                                        identifier = "Trunk1-";
                                        props = "" + vehicleData.id;
                                    }
                                    else if (tempData.inventoysetting.Contains("trunk2-"))
                                    {
                                        identifier = "Trunk2-";
                                        props = "" + vehicleData.id;
                                    }
                                    else if (tempData.inventoysetting.Contains("evidence"))
                                    {
                                        identifier = "evidence";
                                        props = "";
                                    }
                                    if (!CheckDoubleItems(item.description))
                                    {
                                        foreach (ItemsGlobal iteminlist in ItemsController.itemListGlobal)
                                        {
                                            if (iteminlist != null && iteminlist.owneridentifier == identifier + props && item.description.ToLower() == iteminlist.description.ToLower())
                                            {
                                                iteminlist.amount += amount;
                                                item.amount -= amount;
                                                if (item.description.ToLower() == "granate" || item.description.ToLower() == "bzgas" || item.description.ToLower() == "rauchgranate" || item.description.ToLower() == "molotowcocktail" || item.description.ToLower() == "snowball")
                                                {
                                                    string[] weaponArray = new string[7];
                                                    weaponArray = iteminlist.props.Split(",");
                                                    iteminlist.props = $"{iteminlist.amount},{weaponArray[1]},{weaponArray[2]},{weaponArray[3]},{weaponArray[4]},{weaponArray[5]},{weaponArray[6]}";
                                                    weaponArray = item.props.Split(",");
                                                    item.props = $"{item.amount},{weaponArray[1]},{weaponArray[2]},{weaponArray[3]},{weaponArray[4]},{weaponArray[5]},{weaponArray[6]}";
                                                }
                                                setdouble = true;
                                                break;
                                            }
                                        }
                                    }
                                    if (setdouble == false)
                                    {
                                        ItemsGlobal newItem = new ItemsGlobal();
                                        newItem.itemid = GetFreeItemIDGlobal();
                                        newItem.ownerid = 0;
                                        newItem.owneridentifier = identifier + props;
                                        newItem.lastupdate = Helper.UnixTimestamp();
                                        newItem.description = item.description;
                                        newItem.amount = amount;
                                        item.amount -= amount;
                                        newItem.weight = item.weight;
                                        if (item.description.ToLower() == "granate" || item.description.ToLower() == "bzgas" || item.description.ToLower() == "rauchgranate" || item.description.ToLower() == "molotowcocktail" || item.description.ToLower() == "snowball")
                                        {
                                            string[] weaponArray = new string[7];
                                            weaponArray = item.props.Split(",");
                                            item.props = $"{item.amount},{weaponArray[1]},{weaponArray[2]},{weaponArray[3]},{weaponArray[4]},{weaponArray[5]},{weaponArray[6]}";
                                            newItem.props = $"{newItem.amount},{weaponArray[1]},{weaponArray[2]},{weaponArray[3]},{weaponArray[4]},{weaponArray[5]},{weaponArray[6]}";
                                        }
                                        else
                                        {
                                            newItem.props = item.props;
                                        }
                                        newItem.type = item.type;
                                        newItem.hash = item.hash;
                                        itemListGlobal.Add(newItem);
                                    }
                                    if (tempData.inventoysetting.Contains("evidence"))
                                    {
                                        Helper.CreateEvidenceLog($"{character.name} hat {amount}x {item.description} in die Asservatenkammer gelegt!");
                                        string[] weaponArray = new string[7];
                                        weaponArray = item.props.Split(",");

                                        item.props = $"{weaponArray[0]},{weaponArray[1]},{weaponArray[2]},{weaponArray[3]},Asservatenkammer|LSPD,0,|";

                                        MySqlCommand command = General.Connection.CreateCommand();
                                        command = General.Connection.CreateCommand();
                                        command.CommandText = "UPDATE weapons SET name = 'LSPD', shop = 'Asservatenkammer' WHERE ident=@ident";
                                        command.Parameters.AddWithValue("@name", "LSPD");
                                        command.Parameters.AddWithValue("@shop", "Asservatenkammer");
                                        command.Parameters.AddWithValue("@ident", weaponArray[3]);

                                        command.ExecuteNonQuery();
                                    }
                                    if (item.description == "Smartphone" && item.props == character.lastsmartphone)
                                    {
                                        character.lastsmartphone = "n/A";
                                    }
                                    if(tempData.inventoysetting.Contains("trunk") && getCar != null)
                                    {
                                        DealerShipController.SaveOneVehicleData(getCar);
                                    }
                                    if (item.amount <= 0)
                                    {
                                        tempData.itemlist.Remove(item);
                                        item = null;
                                    }
                                    Commands.cmd_animation(player, "give", true);
                                    Helper.SendNotificationWithoutButton(player, "Das Item wurde verschoben!", "success", "top-end");
                                    CharacterController.SaveCharacter(player);
                                }
                            }
                            break;
                        }
                    case "use":
                        {
                            if (item.type != 0 && item.type != 3 && item.type != 5 && item.type != 6)
                            {
                                if (item.type != 4)
                                {
                                    item.amount--;
                                }
                                if (item.type == 1)
                                {
                                    if (NAPI.Player.GetPlayerHealth(player) <= 95)
                                    {
                                        Helper.SetPlayerHealth(player, NAPI.Player.GetPlayerHealth(player) + 5);
                                    }
                                    if (item.description != "Fleisch")
                                    {
                                        if (character.hunger <= 85)
                                        {
                                            character.hunger += 25;
                                        }
                                        else
                                        {
                                            character.hunger = 100;
                                        }
                                        if (character.disease == 0 && Helper.GetRandomPercentage(3))
                                        {
                                            character.disease = 1;
                                        }
                                        Helper.SendNotificationWithoutButton(player, "Du hast ein/e " + item.description + " gegessen!", "success");
                                    }
                                    else
                                    {
                                        if (character.hunger > 10)
                                        {
                                            character.hunger -= 10;
                                        }
                                        else
                                        {
                                            character.hunger = 0;
                                        }
                                        if (character.disease == 0 && Helper.GetRandomPercentage(45))
                                        {
                                            character.disease = 1;
                                        }
                                        Helper.SendNotificationWithoutButton(player, "Du hast rohes Fleisch gegessen!", "success");
                                    }
                                    player.SetOwnSharedData("Player:Needs", character.hunger + "," + character.thirst + "," + character.afk);
                                    Helper.PlayShortAnimation(player, "amb@code_human_wander_smoking@male@idle_a", "idle_a", 1650);
                                }
                                else if (item.type == 2)
                                {
                                    if (NAPI.Player.GetPlayerHealth(player) <= 95)
                                    {
                                        Helper.SetPlayerHealth(player, NAPI.Player.GetPlayerHealth(player) + 5);
                                    }
                                    if (item.description != "Pfandflasche")
                                    {
                                        if (character.thirst <= 85)
                                        {
                                            character.thirst += 25;
                                        }
                                        else
                                        {
                                            character.thirst = 100;
                                        }
                                    }
                                    else
                                    {
                                        if (character.thirst > 5)
                                        {
                                            character.thirst -= 5;
                                        }
                                        else
                                        {
                                            character.thirst = 0;
                                        }
                                    }
                                    player.SetOwnSharedData("Player:Needs", character.hunger + "," + character.thirst + "," + character.afk);
                                    if(item.description == "Vodka")
                                    {
                                        tempData.drunk += 3;
                                    }
                                    if (item.description == "Bier" || item.description == "Wein" || item.description == "Champagne" || item.description == "Sekt")
                                    {
                                        tempData.drunk += 2;
                                    }
                                    if(tempData.drunked == false && tempData.drunk >= 6)
                                    {
                                        player.TriggerEvent("Client:SetDrunk", true);
                                        player.SetSharedData("Player:WalkingStyle", "move_m@drunk@a");
                                        tempData.drunked = true;
                                    }
                                    if (item.description != "Pfandflasche")
                                    {
                                        Helper.SendNotificationWithoutButton(player, "Du hast ein/e " + item.description + " getrunken!", "success");
                                    }
                                    else
                                    {
                                        Helper.SendNotificationWithoutButton(player, "Du hast aus einer dreckigen Pfandflasche getrunken!", "success");
                                    }
                                    Helper.PlayShortAnimation(player, "mini@sprunk", "PLYR_BUY_DRINK_PT2", 1750);
                                }
                                else if (item.type == 4)
                                {
                                    if (item.description == "Smartphone")
                                    {
                                        OnShowInventory(player, 1);
                                        NAPI.Task.Run(() =>
                                        {
                                            SmartphoneController.ShowSmartphone(player, item);
                                        }, delayTime: 125);
                                        return;
                                    }
                                    else if (item.description == "F-Zeugnis")
                                    {
                                        if (Helper.ShowFührungsZeugnis(player, character.name, false) <= 0)
                                        {
                                            Helper.SendNotificationWithoutButton(player, "Keine Einträge vorhanden!", "error");
                                            return;
                                        }
                                        Helper.ShowFührungsZeugnis(player, item.props);
                                        return;
                                    }
                                    else if (item.description == "Benzinkanister")
                                    {
                                        if (player.IsInVehicle)
                                        {
                                            Helper.SendNotificationWithoutButton(player, "Du musst zuerst aus deinem Fahrzeug aussteigen!", "error");
                                            return;
                                        }
                                        Vehicle vehicle = Helper.GetClosestVehicle(player, 1.95f);
                                        if (vehicle == null)
                                        {
                                            Helper.SendNotificationWithoutButton(player, "Es befindet sich kein Fahrzeug in der Nähe!", "error");
                                            return;
                                        }
                                        String props = item.props.Substring(0, 2);
                                        if (float.Parse(props) <= 0)
                                        {
                                            Helper.SendNotificationWithoutButton(player, "Der Benzinkanister ist leer!", "error");
                                            return;
                                        }
                                        if (vehicle.EngineStatus == true)
                                        {
                                            Helper.SendNotificationWithoutButton(player, "Du musst zuerst den Motor vom Fahrzeug ausschalten!", "error");
                                            return;
                                        }
                                        float fuel = vehicle.GetSharedData<float>("Vehicle:MaxFuel") - vehicle.GetSharedData<float>("Vehicle:Fuel");
                                        if (fuel <= 0)
                                        {
                                            Helper.SendNotificationWithoutButton(player, "Das Fahrzeug muss nicht betankt werden!", "error");
                                            return;
                                        }
                                        if (Convert.ToInt32(props) < fuel)
                                        {
                                            fuel = Convert.ToInt32(props);
                                        }
                                        Helper.SendNotificationWithoutButton(player, "Das Fahrzeug wird betankt ...", "success", "top-left", 4700);
                                        player.TriggerEvent("Client:PlayerFreeze", true);
                                        //player.PlayAnimation("timetable@gardener@filling_can", "gar_ig_5_filling_can", 50);
                                        player.SetSharedData("Player:AnimData", $"timetable@gardener@filling_can%gar_ig_5_filling_can%{50}");
                                        player.SetData<bool>("Player:Use", true);
                                        NAPI.Task.Run(() =>
                                        {
                                            Helper.SendNotificationWithoutButton(player, $"Das Fahrzeuge wurde erfolgreich mit {fuel}l betankt", "success", "top-left", 3500);
                                            vehicle.SetSharedData("Vehicle:Fuel", vehicle.GetSharedData<float>("Vehicle:Fuel") + fuel);
                                            Helper.OnStopAnimation(player);
                                            player.TriggerEvent("Client:PlayerFreeze", false);
                                            item.props = (float.Parse(props, CultureInfo.InvariantCulture.NumberFormat) - fuel).ToString("0.##") + " Liter";
                                            player.ResetData("Player:Use");
                                        }, delayTime: 4750);
                                        return;
                                    }
                                    else if (item.description == "Reparaturwerkzeug")
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
                                        player.SetSharedData("Player:AnimData", $"anim@amb@clubhouse@tutorial@bkr_tut_ig3@%machinic_loop_mechandplayer%{1}");
                                        Helper.SendNotificationWithoutButton(player, "Das Fahrzeug wird repariert ...", "success", "top-left", 7700);
                                        player.TriggerEvent("Client:PlayerFreeze", true);
                                        RemoveItem(player, itemid);
                                        OnShowInventory(player, 1);
                                        player.SetData<bool>("Player:Use", true);
                                        NAPI.Task.Run(() =>
                                        {
                                            player.TriggerEvent("Client:RepairVehicleClientside", vehicle);                                           
                                            Helper.OnStopAnimation2(player);
                                            player.TriggerEvent("Client:PlayerFreeze", false);
                                            player.ResetData("Player:Use");
                                            vehicle.Dimension = player.Dimension+1;
                                            NAPI.Task.Run(() =>
                                            {
                                                vehicle.Position = vehicle.Position;
                                                vehicle.Rotation.Z = vehicle.Rotation.Z;
                                                vehicle.Dimension = player.Dimension;
                                                NAPI.Vehicle.RepairVehicle(vehicle);
                                                Helper.SendNotificationWithoutButton(player, $"Das Fahrzeug wurde erfolgreich repariert!", "success", "top-left", 3500);
                                            }, delayTime: 215);
                                        }, delayTime: 7750);
                                        return;
                                    }
                                    else if (item.description == "55$-Prepaidkarte")
                                    {
                                        Smartphone smartphone = SmartphoneController.GetSmartPhoneByNumber(character.lastsmartphone);
                                        if (smartphone != null)
                                        {
                                            if (smartphone.akku <= 0)
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Der Akku ist leer!", "error");
                                                return;
                                            }
                                            if (smartphone.prepaid == -1)
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Es ist kein Prepaidhandy!", "error");
                                                return;
                                            }
                                            smartphone.prepaid += 55;
                                            item.amount -= 1;
                                            Helper.SendNotificationWithoutButton(player, $"Du hast dein Handy um 55$ aufgeladen!", "success", "top-left", 3500);
                                        }
                                        else
                                        {
                                            Helper.SendNotificationWithoutButton(player, "Du hast kein Handy ausgewählt!", "error");
                                        }
                                    }
                                    else if (item.description == "Handyvertrag")
                                    {
                                        Smartphone smartphone = SmartphoneController.GetSmartPhoneByNumber(character.lastsmartphone);
                                        if (smartphone != null)
                                        {
                                            if (smartphone.akku <= 0)
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Der Akku ist leer!", "error");
                                                return;
                                            }
                                            if (smartphone.prepaid == -1)
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Du hast schon einen Vertrag!", "error");
                                                return;
                                            }
                                            item.amount -= 1;
                                            smartphone.prepaid = -1;
                                            smartphone.owner = character.name;
                                            Helper.SendNotificationWithoutButton(player, $"Du hast den nMobile Handyvertrag für 415$ pro Zahltag aktiviert!", "success", "top-left", 3500);
                                        }
                                        else
                                        {
                                            Helper.SendNotificationWithoutButton(player, "Du hast kein Handy ausgewählt!", "error");
                                        }
                                    }
                                    else if (item.description == "Zigaretten")
                                    {
                                        if (Helper.CheckForAttachment(player, "handJoint") || Helper.CheckForAttachment(player, "vehicleJoint"))
                                        {
                                            Helper.SendNotificationWithoutButton(player, "Du musst zuerst den Joint wegwerfen!", "error");
                                            return;
                                        }
                                        Items fire = ItemsController.GetItemByItemName(player, "Feuerzeug");
                                        if (fire == null)
                                        {
                                            Helper.SendNotificationWithoutButton(player, "Du hast kein Feuerzeug dabei!", "error");
                                            return;
                                        }
                                        if (player.IsInVehicle)
                                        {
                                            Helper.AddRemoveAttachments(player, "vehicleCiga", true);
                                        }
                                        else
                                        {
                                            Helper.AddRemoveAttachments(player, "handCiga", true);
                                        }
                                        Helper.SendNotificationWithoutButton(player, $"Du hast dir eine Zigarette angezündet!", "success", "top-left", 3500);
                                        if (tempData.showinventory == true)
                                        {
                                            tempData.inventoysetting = "nothing";
                                            tempData.showinventory = false;
                                            player.TriggerEvent("Client:ShowInventory", NAPI.Util.ToJson(tempData.itemlist), ItemsController.MAX_INVENTORY_PLAYER, false, null, null, null);
                                        }
                                        item.amount -= 1;
                                        try
                                        {
                                            fire.props = "" + (Convert.ToInt32(fire.props) - 1);
                                            if (Convert.ToInt32(fire.props) <= 0)
                                            {
                                                ItemsController.RemoveItem(player, fire.itemid);
                                            }
                                        }
                                        catch(Exception)
                                        {
                                            fire.props = "1";
                                        }
                                        return;
                                    }
                                    else if (item.description == "Angel")
                                    {
                                        if (player.IsInVehicle)
                                        {
                                            Helper.SendNotificationWithoutButton(player, "Du musst zuerst aus deinem Fahrzeug aussteigen!", "error");
                                            return;
                                        }
                                        if (!player.HasData("Player:FishingRod"))
                                        {
                                            player.SetData<bool>("Player:FishingRod", true);
                                            Helper.SendNotificationWithoutButton(player, "Angel vorbereitet, mit der Taste [F4] kannst du die Angel auswerfen!", "success", "top-left", 4250);
                                        }
                                        else
                                        {
                                            player.ResetData("Player:FishingRod");
                                            Helper.SendNotificationWithoutButton(player, "Angel abgebaut!", "success", "top-left", 2250);
                                            Helper.AddRemoveAttachments(player, "fishingRod", false);
                                        }
                                        ItemsController.OnShowInventory(player, 1);
                                        return;
                                    }
                                    else if (item.description == "Schweissgerät")
                                    {
                                        if (player.IsInVehicle)
                                        {
                                            Helper.SendNotificationWithoutButton(player, "Du musst zuerst aus deinem Fahrzeug aussteigen!", "error");
                                            return;
                                        }
                                        Vehicle vehicle = Helper.GetClosestVehicle(player, 5.25f);
                                        if (vehicle == null)
                                        {
                                            Helper.SendNotificationWithoutButton(player, "Du bist nicht in der Nähe von einem Transporter!", "error");
                                            return;
                                        }
                                        Vector3 behindVehicle = Helper.GetPositionBehindOfVehicle(vehicle, 4.75f);
                                        if (behindVehicle == null)
                                        {
                                            Helper.SendNotificationWithoutButton(player, "Du musst hinter dem Transporter stehen!", "error");
                                            return;
                                        }
                                        var transport = "Waffentransporter";
                                        if (vehicle.HasData("Vehicle:AsservatenTransport") || vehicle.HasData("Vehicle:WaffenTransport"))
                                        {
                                            if (NAPI.Vehicle.GetVehicleDriver(vehicle) != null)
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Es darf sich kein Fahrer mehr im Fahrzeug befinden!", "error");
                                                return;
                                            }
                                            if (vehicle.HasData("Vehicle:AsservatenTransport"))
                                            {
                                                transport = "Asservatentransporter";
                                            }
                                            Dispatch dispatch = new Dispatch();
                                            MDCController.dispatchCount++;
                                            dispatch.id = MDCController.dispatchCount;
                                            dispatch.text = $"Der {transport} wird überfallen!";
                                            dispatch.name = "Überwachungssystem";
                                            dispatch.phonenumber = "0189911";
                                            dispatch.to = 1;
                                            dispatch.position = $"{player.Position.X.ToString(new CultureInfo("en-US")):N3},{player.Position.Y.ToString(new CultureInfo("en-US")):N3},{player.Position.Z.ToString(new CultureInfo("en-US")):N3}";
                                            dispatch.timestamp = Helper.UnixTimestamp();
                                            MDCController.dispatchList.Add(dispatch);

                                            MDCController.SendPoliceMDCMessage(player, $"Neuer Dispatch verfügbar - DSPTH-{dispatch.id}!");

                                            ItemsController.RemoveItem(player, item.itemid);
                                            OnShowInventory(player, 1);
                                            player.SetSharedData("Player:AnimData", "WORLD_HUMAN_WELDING");
                                            player.TriggerEvent("Client:StartLockpicking", 45, "welding", "Schweißvorgang läuft...");
                                        }
                                        else
                                        {
                                            Helper.SendNotificationWithoutButton(player, "Dieser Transporter kann nicht ausgeraubt werden!", "error");
                                            return;
                                        }
                                    }
                                    else if (item.description == "Drohne")
                                    {
                                        if (player.IsInVehicle)
                                        {
                                            Helper.SendNotificationWithoutButton(player, "Du musst zuerst aus deinem Fahrzeug aussteigen!", "error");
                                            return;
                                        }
                                        if (tempData.jobVehicle2 != null)
                                        {
                                            Helper.SendNotificationWithoutButton(player, "Du kannst jetzt keine Drohne benutzen!", "error");
                                            return;
                                        }
                                        ItemsController.RemoveItem(player, item.itemid);
                                        OnShowInventory(player, 1);
                                        player.SetData<bool>("Player:Drohne", true);
                                        tempData.furniturePosition = player.Position;
                                        tempData.jobVehicle2 = Cars.createNewCar("rcmavic", new Vector3(player.Position.X, player.Position.Y, player.Position.Z - 0.55), player.Heading, 0, 0, "Drohne", "n/A", false, false, true, player.Dimension, null, true);
                                        NAPI.Player.SetPlayerIntoVehicle(player, tempData.jobVehicle2, 0);
                                        Helper.SendNotificationWithoutButton(player, "Drohne aufgebaut!", "success");
                                    }
                                    else if (item.description == "Kabelbinder")
                                    {
                                        Player getPlayer = Helper.GetClosestPlayer(player, 2.5f);
                                        if (getPlayer != null)
                                        {
                                            Vector3 behindPlayer = Helper.GetPositionBehindOfPlayer(getPlayer, 1.95f);
                                            if (player.Position.DistanceTo(behindPlayer) <= 1.95)
                                            {
                                                item.amount -= 1;
                                                Commands.cmd_animation(player, "give", true);
                                                TempData tempData2 = Helper.GetCharacterTempData(getPlayer);
                                                if (tempData2.cuffed == 1)
                                                {
                                                    Helper.SendNotificationWithoutButton(player, "Der Spieler trägt schon Handschellen, diese müssen erst abgenommen werden!", "error");
                                                    return;
                                                }
                                                if (tempData2.cuffed == 2)
                                                {
                                                    Helper.SendNotificationWithoutButton(player, "Der Spieler trägt schon Kabelbinder!", "error");
                                                    return;
                                                }
                                                if (tempData2.cuffed == 0)
                                                {
                                                    OnShowInventory(player, 1);
                                                    NAPI.Player.SetPlayerCurrentWeapon(getPlayer, WeaponHash.Unarmed);
                                                    getPlayer.TriggerEvent("Client:HideMenus");
                                                    tempData2.cuffed = 2;
                                                    Helper.SendNotificationWithoutButton(player, "Du hast jemanden Kabelbinder angelegt!", "success");
                                                    Helper.SendNotificationWithoutButton(getPlayer, "Dir wurden Kabelbinder angelegt!", "success");
                                                    getPlayer.TriggerEvent("Client:SetCuff", true);
                                                    NAPI.Task.Run(() =>
                                                    {
                                                        getPlayer.SetSharedData("Player:AnimData", $"mp_arresting%idle%{49}");
                                                    }, delayTime: 215);
                                                }
                                            }
                                            else
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Du stehst nicht hinter dem Spieler!", "error");
                                            }
                                        }
                                        else
                                        {
                                            Helper.SendNotificationWithoutButton(player, "Es befindet sich kein Spieler in der Nähe!", "error");
                                        }
                                    }
                                    else if (item.description == "Bandage")
                                    {
                                        if (NAPI.Player.GetPlayerHealth(player) >= 15 && NAPI.Player.GetPlayerHealth(player) < 50)
                                        {
                                            if (player.HasData("Player:HealCooldown2"))
                                            {
                                                if (Helper.UnixTimestamp() < player.GetData<int>("Player:HealCooldown2"))
                                                {
                                                    Helper.SendNotificationWithoutButton(player, "Du kannst nur alle 3 Minuten ein weiteres Bandage benutzen!", "error");
                                                    return;
                                                }
                                                player.ResetData("Player:HealCooldown2");
                                            }
                                            player.SetSharedData("Player:AnimData", $"anim@heists@box_carry@%base%{49}");
                                            Helper.SetPlayerHealth(player, NAPI.Player.GetPlayerHealth(player) + 25);
                                            if (NAPI.Player.GetPlayerHealth(player) >= 50)
                                            {
                                                player.SetData<int>("Player:HealCooldown2", Helper.UnixTimestamp() + (180));
                                                Helper.SetPlayerHealth(player, 50);
                                            }
                                            Helper.SendNotificationWithoutButton(player, "Du hast eine Bandage genutzt!", "success");
                                            item.amount -= 1;
                                            Helper.PlayShortAnimation(player, "missmic4", "michael_tux_fidget", 2500);
                                        }
                                        else
                                        {
                                            Helper.SendNotificationWithoutButton(player, "Die Bandage hätte jetzt keine Wirkung!", "error");
                                        }
                                    }
                                    else if (item.description == "Ibuprofee-400mg")
                                    {
                                        if (NAPI.Player.GetPlayerHealth(player) >= 25 && NAPI.Player.GetPlayerHealth(player) < 100)
                                        {
                                            if (player.HasData("Player:HealCooldown"))
                                            {
                                                if (Helper.UnixTimestamp() < player.GetData<int>("Player:HealCooldown"))
                                                {
                                                    Helper.SendNotificationWithoutButton(player, "Du kannst nur alle 3 Minuten ein weiteres Medikament zu dir nehmen!", "error");
                                                    return;
                                                }
                                                player.ResetData("Player:HealCooldown");
                                            }
                                            Helper.PlayShortAnimation(player, "amb@code_human_wander_smoking@male@idle_a", "idle_a", 1650);
                                            Helper.SetPlayerHealth(player, NAPI.Player.GetPlayerHealth(player) + 25);
                                            if (NAPI.Player.GetPlayerHealth(player) >= 100)
                                            {
                                                player.SetData<int>("Player:HealCooldown", Helper.UnixTimestamp() + (180));
                                                Helper.SetPlayerHealth(player, 100);
                                            }
                                            Helper.SendNotificationWithoutButton(player, "Du hast das Medikament eingenommen!", "success");
                                            item.amount -= 1;
                                        }
                                        else
                                        {
                                            Helper.SendNotificationWithoutButton(player, "Das Medikament hätte jetzt keine Wirkung!", "error");
                                        }
                                    }
                                    else if (item.description == "Ibuprofee-800mg")
                                    {
                                        if (NAPI.Player.GetPlayerHealth(player) < 100)
                                        {
                                            if (player.HasData("Player:HealCooldown"))
                                            {
                                                if (Helper.UnixTimestamp() < player.GetData<int>("Player:HealCooldown"))
                                                {
                                                    Helper.SendNotificationWithoutButton(player, "Du kannst nur alle 3 Minuten ein weiteres Medikament zu dir nehmen!", "error");
                                                    return;
                                                }
                                                player.ResetData("Player:HealCooldown");
                                            }
                                            Helper.PlayShortAnimation(player, "amb@code_human_wander_smoking@male@idle_a", "idle_a", 1650);
                                            Helper.SetPlayerHealth(player, NAPI.Player.GetPlayerHealth(player) + 50);
                                            if (NAPI.Player.GetPlayerHealth(player) >= 100)
                                            {
                                                player.SetData<int>("Player:HealCooldown", Helper.UnixTimestamp() + (180));
                                                Helper.SetPlayerHealth(player, 100);
                                            }
                                            Helper.SendNotificationWithoutButton(player, "Du hast das Medikament eingenommen!", "success");
                                            item.amount -= 1;
                                        }
                                        else
                                        {
                                            Helper.SendNotificationWithoutButton(player, "Das Medikament hätte jetzt keine Wirkung!", "error");
                                        }
                                    }
                                    else if (item.description == "Morphin")
                                    {
                                        if (NAPI.Player.GetPlayerHealth(player) < 100)
                                        {
                                            if (player.HasData("Player:HealCooldown"))
                                            {
                                                if (Helper.UnixTimestamp() < player.GetData<int>("Player:HealCooldown"))
                                                {
                                                    Helper.SendNotificationWithoutButton(player, "Du kannst nur alle 3 Minuten ein weiteres Medikament zu dir nehmen!", "error");
                                                    return;
                                                }
                                                player.ResetData("Player:HealCooldown");
                                            }
                                            player.SetData<int>("Player:HealCooldown", Helper.UnixTimestamp() + (180));
                                            Helper.PlayShortAnimation(player, "amb@code_human_wander_smoking@male@idle_a", "idle_a", 1650);
                                            Helper.SetPlayerHealth(player, NAPI.Player.GetPlayerHealth(player) + 50);
                                            Helper.SetPlayerHealth(player, 100);
                                            Helper.SendNotificationWithoutButton(player, "Du hast das Medikament eingenommen!", "success");
                                            item.amount -= 1;
                                        }
                                        else
                                        {
                                            Helper.SendNotificationWithoutButton(player, "Das Medikament hätte jetzt keine Wirkung!", "error");
                                        }
                                    }
                                    else if (item.description == "Grippofein-C")
                                    {
                                        if (character.disease != 1)
                                        {
                                            if (player.HasData("Player:HealCooldown"))
                                            {
                                                if (Helper.UnixTimestamp() < player.GetData<int>("Player:HealCooldown"))
                                                {
                                                    Helper.SendNotificationWithoutButton(player, "Du kannst nur alle 3 Minuten ein weiteres Medikament zu dir nehmen!", "error");
                                                    return;
                                                }
                                                player.ResetData("Player:HealCooldown");
                                            }
                                            player.SetData<int>("Player:HealCooldown", Helper.UnixTimestamp() + (180));
                                            Helper.PlayShortAnimation(player, "amb@code_human_wander_smoking@male@idle_a", "idle_a", 1650);
                                            Helper.SendNotificationWithoutButton(player, "Du hast das Medikament eingenommen!", "success");
                                            item.amount -= 1;
                                            character.disease = 0;
                                        }
                                        else
                                        {
                                            Helper.SendNotificationWithoutButton(player, "Das Medikament hätte jetzt keine Wirkung!", "error");
                                        }
                                    }
                                    else if (item.description == "Antibiotika")
                                    {
                                        if (character.disease != 2)
                                        {
                                            if (player.HasData("Player:HealCooldown"))
                                            {
                                                if (Helper.UnixTimestamp() < player.GetData<int>("Player:HealCooldown"))
                                                {
                                                    Helper.SendNotificationWithoutButton(player, "Du kannst nur alle 3 Minuten ein weiteres Medikament zu dir nehmen!", "error");
                                                    return;
                                                }
                                                player.ResetData("Player:HealCooldown");
                                            }
                                            player.SetData<int>("Player:HealCooldown", Helper.UnixTimestamp() + (180));
                                            Helper.PlayShortAnimation(player, "amb@code_human_wander_smoking@male@idle_a", "idle_a", 1650);
                                            Helper.SendNotificationWithoutButton(player, "Du hast das Medikament eingenommen!", "success");
                                            item.amount -= 1;
                                            character.disease = 0;
                                        }
                                        else
                                        {
                                            Helper.SendNotificationWithoutButton(player, "Das Medikament hätte jetzt keine Wirkung!", "error");
                                        }
                                    }
                                    else if (item.description == "Ghettoblaster")
                                    {
                                        if (tempData.ghettoblaster != null)
                                        {
                                            if (tempData.ghettoblaster.Position.DistanceTo(player.Position) < 2.75)
                                            {
                                                Helper.PlayShortAnimation(player, "anim@heists@narcotics@trash", "throw_b", 1250);
                                                Helper.SendNotificationWithoutButton(player, "Ghettoblaster abgebaut!", "success");
                                                tempData.ghettoblaster.Delete();
                                                tempData.ghettoblaster = null;
                                            }
                                            else
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Du bist nicht in der Nähe von deinem Ghettoblaster!", "error");
                                            }
                                        }
                                        else
                                        {
                                            if ((Helper.IsInRangeOfPoint(player.Position, new Vector3(1987.5565, 3051.1028, 47.215157), 65f) || Helper.IsInRangeOfPoint(player.Position, new Vector3(121.39574, -1279.8123, 29.6533), 65f) || Helper.IsInRangeOfPoint(player.Position, new Vector3(840.6783, -118.2715, 79.77466), 65f) || Helper.IsInRangeOfPoint(player.Position, new Vector3(-456.60736, 274.12994, 84.22368), 65f) || Helper.IsInRangeOfPoint(player.Position, new Vector3(-561.8999, 281.58398, 85.67638), 65f) || Helper.IsInRangeOfPoint(player.Position, new Vector3(-1381.4324, -616.371, 31.497929), 65f)) && player.Dimension == 0)
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Du kannst hier keinen Ghettoblaster aufbauen!", "error");
                                                return;
                                            }
                                            Helper.PlayShortAnimation(player, "anim@heists@narcotics@trash", "throw_b", 1250);
                                            Helper.SendNotificationWithoutButton(player, "Ghettoblaster aufgebaut, benutze Taste [F6] um die Musik zu steuern!", "success", "top-left", 4750);
                                            Vector3 newPosition = Helper.GetPositionInFrontOfPlayer(player, 1.50f);
                                            tempData.ghettoblaster = NAPI.Object.CreateObject(NAPI.Util.GetHashKey("prop_boombox_01"), new Vector3(newPosition.X, newPosition.Y, newPosition.Z - 0.81), new Vector3(0.0f, 0.0f, player.Heading), 255, 0);
                                        }
                                    }
                                    else if (item.description == "Kleines-Messer")
                                    {
                                        Player getPlayer = Helper.GetClosestPlayer(player, 2.5f);
                                        if (getPlayer != null)
                                        {
                                            Vector3 behindPlayer = Helper.GetPositionBehindOfPlayer(getPlayer, 1.95f);
                                            if (player.Position.DistanceTo(behindPlayer) <= 1.95)
                                            {
                                                Commands.cmd_animation(player, "give", true);
                                                TempData tempData2 = Helper.GetCharacterTempData(getPlayer);
                                                if (tempData2.cuffed == 2)
                                                {
                                                    OnShowInventory(player, 1);
                                                    tempData2.cuffed = 0;
                                                    Helper.SendNotificationWithoutButton(player, "Du hast die Kabelbinder aufgeschnitten!", "success");
                                                    Helper.SendNotificationWithoutButton(getPlayer, "Deine angelegten Kabelbinder wurden aufgeschnitten!", "success");
                                                    getPlayer.TriggerEvent("Client:SetCuff", false);
                                                    Helper.OnStopAnimation(getPlayer);
                                                }
                                                else
                                                {
                                                    Helper.SendNotificationWithoutButton(player, "Der Spieler trägt keine Kabelbinder!", "error");
                                                }
                                            }
                                            else
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Du stehst nicht hinter dem Spieler!", "error");
                                            }
                                        }
                                        else
                                        {
                                            Helper.SendNotificationWithoutButton(player, "Es befindet sich kein Spieler in der Nähe!", "error");
                                        }
                                    }
                                    else if (item.description == "Hackwerkzeug")
                                    {
                                        if (Helper.UnixTimestamp() < player.GetData<int>("Player:VendingCooldown"))
                                        {
                                            Helper.SendNotificationWithoutButton(player, "Du kannst nur alle 10 Minuten einen Automaten hacken!", "error");
                                            return;
                                        }
                                        player.ResetData("Player:VendingCooldown");
                                        OnShowInventory(player, 1);
                                        player.TriggerEvent("Client:PrepareHack");
                                        return;
                                    }
                                    else if (item.description == "Marihuanasamen")
                                    {
                                        if (item.amount < 2)
                                        {
                                            Helper.SendNotificationWithoutButton(player, "Du benötigst mind. 2 Marihuanasamen um eine Drogenpflanze zu pflanzen!", "error");
                                            return;
                                        }
                                        OnShowInventory(player, 1);
                                        DrugController.CreateDrugPlant(player, "Marihuana");
                                        return;
                                    }
                                    else if (item.description == "Kokainsamen")
                                    {
                                        if (item.amount < 3)
                                        {
                                            Helper.SendNotificationWithoutButton(player, "Du benötigst mind. 3 Kokainsamen um eine Drogenpflanze zu pflanzen!", "error");
                                            return;
                                        }
                                        OnShowInventory(player, 1);
                                        DrugController.CreateDrugPlant(player, "Kokain");
                                        return;
                                    }
                                    else if (item.description == "Marihuana")
                                    {
                                        Items papes = ItemsController.GetItemByItemName(player, "Papes");
                                        if (papes == null)
                                        {
                                            Helper.SendNotificationWithoutButton(player, "Du hast keine Papes dabei!", "error");
                                            return;
                                        }
                                        if (item.amount < 3)
                                        {
                                            Helper.SendNotificationWithoutButton(player, "Du benötigst mind. 3g Marihuana um dir einen Joint bauen zu können!", "error");
                                            return;
                                        }
                                        Items newitem = ItemsController.CreateNewItem(player, character.id, "Joint", "Player", 1, ItemsController.GetFreeItemID(player));
                                        if (!ItemsController.CanPlayerHoldItem(player, newitem.weight))
                                        {
                                            newitem = null;
                                            Helper.SendNotificationWithoutButton(player, "Du hast keinen Platz mehr für den Joint!", "success", "center");
                                            return;
                                        }
                                        if (newitem != null)
                                        {
                                            tempData.itemlist.Add(newitem);
                                        }
                                        OnShowInventory(player, 1);
                                        item.amount -= 3;
                                        papes.amount -= 1;
                                        if (papes.amount <= 0)
                                        {
                                            ItemsController.RemoveItem(player, papes.itemid);
                                        }
                                        player.SetSharedData("Player:AnimData", $"anim@amb@clubhouse@tutorial@bkr_tut_ig3@%machinic_loop_mechandplayer%{17}");
                                        NAPI.Task.Run(() =>
                                        {
                                            Helper.OnStopAnimation2(player);
                                            Helper.SendNotificationWithoutButton(player, "Du hast einen Joint gebaut!", "success", "top-left");
                                        }, delayTime: 2150);
                                    }
                                    else if (item.description == "Joint")
                                    {
                                        if (player.HasData("Player:HealCooldown"))
                                        {
                                            if (Helper.UnixTimestamp() < player.GetData<int>("Player:HealCooldown"))
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Du kannst erst in 5 Minuten weitere Drogen nehmen!", "error");
                                                return;
                                            }
                                            player.ResetData("Player:HealCooldown");
                                        }
                                        player.SetData<int>("Player:HealCooldown", Helper.UnixTimestamp() + (300));
                                        if (Helper.CheckForAttachment(player, "handCiga") || Helper.CheckForAttachment(player, "vehicleCiga"))
                                        {
                                            Helper.SendNotificationWithoutButton(player, "Du musst zuerst die Zigarette wegwerfen!", "error");
                                            return;
                                        }
                                        Items fire = ItemsController.GetItemByItemName(player, "Feuerzeug");
                                        if (fire == null)
                                        {
                                            Helper.SendNotificationWithoutButton(player, "Du hast kein Feuerzeug dabei!", "error");
                                            return;
                                        }
                                        OnShowInventory(player, 1);
                                        item.amount -= 1;
                                        fire.props = "" + (Convert.ToInt32(fire.props) - 1);
                                        if (Convert.ToInt32(fire.props) <= 0)
                                        {
                                            ItemsController.RemoveItem(player, fire.itemid);
                                        }
                                        if (player.IsInVehicle)
                                        {
                                            Helper.AddRemoveAttachments(player, "vehicleJoint", true);
                                        }
                                        else
                                        {
                                            Helper.AddRemoveAttachments(player, "handJoint", true);
                                        }
                                        if (NAPI.Player.GetPlayerHealth(player) < 150)
                                        {
                                            Helper.SetPlayerHealth(player, NAPI.Player.GetPlayerHealth(player) + 50);
                                        }
                                        if (NAPI.Player.GetPlayerHealth(player) > 150)
                                        {
                                            Helper.SetPlayerHealth(player, 150);
                                        }
                                        if (character.thirst > 10)
                                        {
                                            character.thirst -= 10;
                                        }
                                        if (character.hunger > 10)
                                        {
                                            character.hunger -= 10;
                                        }
                                        player.SetOwnSharedData("Player:Needs", character.hunger + "," + character.thirst + "," + character.afk);
                                        Helper.SendNotificationWithoutButton(player, "Du hast dir einen Joint angezündet mit /a joint kannst du eine Joint Animation abspielen!", "success", "top-left", 4750);
                                    }
                                    else if (item.description == "Kokain")
                                    {
                                        if (player.HasData("Player:HealCooldown"))
                                        {
                                            if (Helper.UnixTimestamp() < player.GetData<int>("Player:HealCooldown"))
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Du kannst erst in 5 Minuten weitere Drogen nehmen!", "error");
                                                return;
                                            }
                                            player.ResetData("Player:HealCooldown");
                                        }
                                        player.SetData<int>("Player:HealCooldown", Helper.UnixTimestamp() + (300));
                                        if (Helper.CheckForAttachment(player, "handCiga") || Helper.CheckForAttachment(player, "vehicleCiga"))
                                        {
                                            Helper.SendNotificationWithoutButton(player, "Du musst zuerst die Zigarette wegwerfen!", "error");
                                            return;
                                        }
                                        if (Helper.CheckForAttachment(player, "handJoint") || Helper.CheckForAttachment(player, "vehicleJoint"))
                                        {
                                            Helper.SendNotificationWithoutButton(player, "Du musst zuerst den Joint wegwerfen!", "error");
                                            return;
                                        }
                                        OnShowInventory(player, 1);
                                        item.amount -= 1;
                                        if (NAPI.Player.GetPlayerHealth(player) < 115)
                                        {
                                            Helper.SetPlayerHealth(player, 115);
                                        }
                                        if (character.thirst > 15)
                                        {
                                            character.thirst -= 15;
                                        }
                                        if (character.hunger > 15)
                                        {
                                            character.hunger -= 15;
                                        }
                                        player.SetOwnSharedData("Player:Needs", character.hunger + "," + character.thirst + "," + character.afk);
                                        Helper.SendNotificationWithoutButton(player, "Du hast Kokain geschnupft, du fühlst dich fitter und gesund!", "success", "top-left", 4750);
                                        Helper.PlayShortAnimation(player, "amb@code_human_wander_smoking@male@idle_a", "idle_a", 1650);
                                        player.TriggerEvent("Client:DrugEvent");
                                        player.TriggerEvent("Client:SprintMultiplier", 1.20f);
                                    }
                                    else if (item.description == "Crystal-Meth")
                                    {
                                        if (player.HasData("Player:HealCooldown"))
                                        {
                                            if (Helper.UnixTimestamp() < player.GetData<int>("Player:HealCooldown"))
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Du kannst erst in 5 Minuten weitere Drogen nehmen!", "error");
                                                return;
                                            }
                                            player.ResetData("Player:HealCooldown");
                                        }
                                        player.SetData<int>("Player:HealCooldown", Helper.UnixTimestamp() + (300));
                                        if (Helper.CheckForAttachment(player, "handCiga") || Helper.CheckForAttachment(player, "vehicleCiga"))
                                        {
                                            Helper.SendNotificationWithoutButton(player, "Du musst zuerst die Zigarette wegwerfen!", "error");
                                            return;
                                        }
                                        if (Helper.CheckForAttachment(player, "handJoint") || Helper.CheckForAttachment(player, "vehicleJoint"))
                                        {
                                            Helper.SendNotificationWithoutButton(player, "Du musst zuerst den Joint wegwerfen!", "error");
                                            return;
                                        }
                                        OnShowInventory(player, 1);
                                        item.amount -= 1;
                                        if (NAPI.Player.GetPlayerHealth(player) < 105)
                                        {
                                            Helper.SetPlayerHealth(player, NAPI.Player.GetPlayerHealth(player) + 50);
                                        }
                                        if (NAPI.Player.GetPlayerHealth(player) > 105)
                                        {
                                            Helper.SetPlayerHealth(player, 105);
                                        }
                                        if (character.thirst > 20)
                                        {
                                            character.thirst -= 20;
                                        }
                                        if (character.hunger > 20)
                                        {
                                            character.hunger -= 20;
                                        }
                                        player.SetOwnSharedData("Player:Needs", character.hunger + "," + character.thirst + "," + character.afk);
                                        Helper.SendNotificationWithoutButton(player, "Du hast Crystal-Meth geschnupft und spürst jetzt weniger Schmerz!", "success", "top-left", 4750);
                                        Helper.PlayShortAnimation(player, "amb@code_human_wander_smoking@male@idle_a", "idle_a", 1650);
                                        player.TriggerEvent("Client:DrugEvent");
                                        player.TriggerEvent("Client:CrystalMeth");
                                    }
                                    else if (item.description == "Space-Cookies")
                                    {
                                        if (player.HasData("Player:HealCooldown"))
                                        {
                                            if (Helper.UnixTimestamp() < player.GetData<int>("Player:HealCooldown"))
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Du kannst erst in 5 Minuten weitere Drogen nehmen!", "error");
                                                return;
                                            }
                                            player.ResetData("Player:HealCooldown");
                                        }
                                        player.SetData<int>("Player:HealCooldown", Helper.UnixTimestamp() + (300));
                                        if (Helper.CheckForAttachment(player, "handCiga") || Helper.CheckForAttachment(player, "vehicleCiga"))
                                        {
                                            Helper.SendNotificationWithoutButton(player, "Du musst zuerst die Zigarette wegwerfen!", "error");
                                            return;
                                        }
                                        if (Helper.CheckForAttachment(player, "handJoint") || Helper.CheckForAttachment(player, "vehicleJoint"))
                                        {
                                            Helper.SendNotificationWithoutButton(player, "Du musst zuerst den Joint wegwerfen!", "error");
                                            return;
                                        }
                                        OnShowInventory(player, 1);
                                        item.amount -= 1;
                                        if (NAPI.Player.GetPlayerHealth(player) <= 50)
                                        {
                                            Helper.SetPlayerHealth(player, NAPI.Player.GetPlayerHealth(player) + 50);
                                        }
                                        else
                                        {
                                            Helper.SetPlayerHealth(player, 100);
                                        }
                                        if (NAPI.Player.GetPlayerHealth(player) > 100)
                                        {
                                            Helper.SetPlayerHealth(player, 100);
                                        }
                                        if (character.thirst > 15)
                                        {
                                            character.thirst -= 15;
                                        }
                                        if (character.hunger > 15)
                                        {
                                            character.hunger -= 15;
                                        }
                                        player.SetOwnSharedData("Player:Needs", character.hunger + "," + character.thirst + "," + character.afk);
                                        Helper.SendNotificationWithoutButton(player, "Du hast einen Space-Cookie gegessen und erhältst einen Lebenpush für 5 Minuten sowie + 50 Leben!", "success", "top-left", 4750);
                                        Helper.PlayShortAnimation(player, "amb@code_human_wander_smoking@male@idle_a", "idle_a", 1650);
                                        player.TriggerEvent("Client:DrugEvent");
                                        player.SetData("Player:HealBonus", 5);
                                    }
                                    else if (item.description == "Funkgerät")
                                    {
                                        OnShowInventory(player, 1);
                                        player.TriggerEvent("Client:ShowRadioSystem", tempData.radio);
                                        return;
                                    }
                                    else if (item.description == "Filmkamera")
                                    {
                                        if (player.IsInVehicle)
                                        {
                                            Helper.SendNotificationWithoutButton(player, "Du musst zuerst aus deinem Fahrzeug aussteigen!", "error");
                                            return;
                                        }
                                        if (!player.HasData("Player:Filmkamera"))
                                        {
                                            if(tempData.showSmartphone == true)
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Du musst zuerst das Smartphone weglegen!", "error");
                                                return;
                                            }
                                            if (player.HasData("Player:GlowStick"))
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Du musst zuerst den Glowstick weglegen!", "error");
                                                return;
                                            }
                                            if (player.HasData("Player:HorseStick"))
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Du musst zuerst den Horsestick weglegen!", "error");
                                                return;
                                            }
                                            if (player.HasData("Player:RunStock"))
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Du musst zuerst den Gehstock weglegen!", "error");
                                                return;
                                            }
                                            if (player.HasData("Player:Mikrofon"))
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Du musst zuerst das Mikrofon weglegen!", "error");
                                                return;
                                            }
                                            player.SetData<bool>("Player:Filmkamera", true);
                                            Helper.AddRemoveAttachments(player, "filmcamera", true);
                                            NAPI.Player.SetPlayerCurrentWeapon(player, WeaponHash.Unarmed);
                                            player.SetSharedData("Player:AnimData", $"missfinale_c2mcs_1%fin_c2_mcs_1_camman%{50}");
                                            Helper.SendNotificationWithoutButton(player, "Filmkamera ausgerüstet, du kannst jetzt über die Lifeinvader App einen Livestream starten!", "success", "top-left", 4150);
                                        }
                                        else
                                        {
                                            player.ResetData("Player:Filmkamera");
                                            Helper.AddRemoveAttachments(player, "filmcamera", false);
                                            NAPI.Player.SetPlayerCurrentWeapon(player, WeaponHash.Unarmed);
                                            Helper.SendNotificationWithoutButton(player, "Filmkamera weggelegt!", "success");
                                            Helper.OnStopAnimation2(player);
                                        }
                                        OnShowInventory(player, 1);
                                    }
                                    else if (item.description == "Mikrofon")
                                    {
                                        if (player.IsInVehicle)
                                        {
                                            Helper.SendNotificationWithoutButton(player, "Du musst zuerst aus deinem Fahrzeug aussteigen!", "error");
                                            return;
                                        }
                                        if (!player.HasData("Player:Mikrofon"))
                                        {
                                            if (tempData.showSmartphone == true)
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Du musst zuerst das Smartphone weglegen!", "error");
                                                return;
                                            }
                                            if (player.HasData("Player:GlowStick"))
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Du musst zuerst den Glowstick weglegen!", "error");
                                                return;
                                            }
                                            if (player.HasData("Player:HorseStick"))
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Du musst zuerst den Horsestick weglegen!", "error");
                                                return;
                                            }
                                            if (player.HasData("Player:RunStock"))
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Du musst zuerst den Gehstock weglegen!", "error");
                                                return;
                                            }
                                            if (player.HasData("Player:Filmkamera"))
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Du musst zuerst die Filmkamera weglegen!", "error");
                                                return;
                                            }
                                            player.SetData<bool>("Player:Mikrofon", true);
                                            Helper.AddRemoveAttachments(player, "microphone", true);
                                            NAPI.Player.SetPlayerCurrentWeapon(player, WeaponHash.Unarmed);
                                            player.SetSharedData("Player:AnimData", $"missheistdocksprep1hold_cellphone%hold_cellphone%{50}");
                                            Helper.SendNotificationWithoutButton(player, "Mikrofon ausgerüstet!", "success", "top-left", 4150);
                                        }
                                        else
                                        {
                                            player.ResetData("Player:Mikrofon");
                                            Helper.AddRemoveAttachments(player, "microphone", false);
                                            NAPI.Player.SetPlayerCurrentWeapon(player, WeaponHash.Unarmed);
                                            Helper.SendNotificationWithoutButton(player, "Mikrofon weggelegt!", "success");
                                            Helper.OnStopAnimation2(player);
                                        }
                                        OnShowInventory(player, 1);
                                    }
                                    else if (item.description == "Spitzhacke")
                                    {
                                        if (player.IsInVehicle)
                                        {
                                            Helper.SendNotificationWithoutButton(player, "Du musst zuerst aus deinem Fahrzeug aussteigen!", "error");
                                            return;
                                        }
                                        if (!player.HasData("Player:Pickaxe"))
                                        {
                                            if (player.HasData("Player:GlowStick"))
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Du musst zuerst den Glowstick weglegen!", "error");
                                                return;
                                            }
                                            if (player.HasData("Player:HorseStick"))
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Du musst zuerst den Horsestick weglegen!", "error");
                                                return;
                                            }
                                            if (player.HasData("Player:RunStock"))
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Du musst zuerst den Gehstock weglegen!", "error");
                                                return;
                                            }
                                            if (player.HasData("Player:Mikrofon"))
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Du musst zuerst das Mikrofon weglegen!", "error");
                                                return;
                                            }
                                            if (player.HasData("Player:Filmkamera"))
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Du musst zuerst die Filmkamera weglegen!", "error");
                                                return;
                                            }
                                            player.SetData<bool>("Player:Pickaxe", true);
                                            Helper.AddRemoveAttachments(player, "pickaxe", true);
                                            NAPI.Player.SetPlayerCurrentWeapon(player, WeaponHash.Unarmed);
                                            Helper.SendNotificationWithoutButton(player, "Spitzhacke ausgerüstet, du kannst jetzt mkit der Taste [F4] buddeln!", "success", "top-left", 4150);
                                        }
                                        else
                                        {
                                            player.ResetData("Player:Pickaxe");
                                            Helper.AddRemoveAttachments(player, "pickaxe", false);
                                            NAPI.Player.SetPlayerCurrentWeapon(player, WeaponHash.Unarmed);
                                            Helper.SendNotificationWithoutButton(player, "Spitzhacke weggelegt!", "success");
                                        }
                                        OnShowInventory(player, 1);
                                    }
                                    else if (item.description == "Glowstick")
                                    {
                                        if (player.IsInVehicle)
                                        {
                                            Helper.SendNotificationWithoutButton(player, "Du musst zuerst aus deinem Fahrzeug aussteigen!", "error");
                                            return;
                                        }
                                        if (!player.HasData("Player:GlowStick"))
                                        {
                                            if (player.HasData("Player:HorseStick"))
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Du musst zuerst den Horsestick weglegen!", "error");
                                                return;
                                            }
                                            if (player.HasData("Player:RunStock"))
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Du musst zuerst den Gehstock weglegen!", "error");
                                                return;
                                            }
                                            if (player.HasData("Player:Pickaxe"))
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Du musst zuerst die Spitzhacke weglegen!", "error");
                                                return;
                                            }
                                            if (player.HasData("Player:Mikrofon"))
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Du musst zuerst das Mikrofon weglegen!", "error");
                                                return;
                                            }
                                            if (player.HasData("Player:Filmkamera"))
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Du musst zuerst die Filmkamera weglegen!", "error");
                                                return;
                                            }
                                            player.SetData<bool>("Player:GlowStick", true);
                                            Helper.AddRemoveAttachments(player, "glowStick", true);
                                            Helper.SendNotificationWithoutButton(player, "Glowstick ausgerüstet, verfügbare Animation: /a danceglowstick2/3!", "success", "top-left", 4150);
                                        }
                                        else
                                        {
                                            player.ResetData("Player:GlowStick");
                                            Helper.AddRemoveAttachments(player, "glowStick", false);
                                            Helper.SendNotificationWithoutButton(player, "Glowstick weggelegt!", "success");
                                        }
                                        OnShowInventory(player, 1);
                                        return;
                                    }
                                    else if (item.description == "Horsestick")
                                    {
                                        if (player.IsInVehicle)
                                        {
                                            Helper.SendNotificationWithoutButton(player, "Du musst zuerst aus deinem Fahrzeug aussteigen!", "error");
                                            return;
                                        }
                                        if (!player.HasData("Player:HorseStick"))
                                        {
                                            if (player.HasData("Player:GlowStick"))
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Du musst zuerst den Glowstick weglegen!", "error");
                                                return;
                                            }
                                            if (player.HasData("Player:RunStock"))
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Du musst zuerst den Gehstock weglegen!", "error");
                                                return;
                                            }
                                            if (player.HasData("Player:Pickaxe"))
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Du musst zuerst die Spitzhacke weglegen!", "error");
                                                return;
                                            }
                                            if (player.HasData("Player:Mikrofon"))
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Du musst zuerst das Mikrofon weglegen!", "error");
                                                return;
                                            }
                                            if (player.HasData("Player:Filmkamera"))
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Du musst zuerst die Filmkamera weglegen!", "error");
                                                return;
                                            }
                                            player.SetData<bool>("Player:HorseStick", true);
                                            Helper.AddRemoveAttachments(player, "horseStick", true);
                                            Helper.SendNotificationWithoutButton(player, "Horsestick ausgerüstet, verfügbare Animation: /a dancehorse2/3!", "success", "top-left", 4150);
                                        }
                                        else
                                        {
                                            player.ResetData("Player:HorseStick");
                                            Helper.AddRemoveAttachments(player, "horseStick", false);
                                            Helper.SendNotificationWithoutButton(player, "Horsestick weggelegt!", "success");
                                        }
                                        OnShowInventory(player, 1);
                                        return;
                                    }
                                    else if (item.description == "Gehstock")
                                    {
                                        if (player.IsInVehicle)
                                        {
                                            Helper.SendNotificationWithoutButton(player, "Du musst zuerst aus deinem Fahrzeug aussteigen!", "error");
                                            return;
                                        }
                                        if (!player.HasData("Player:RunStock"))
                                        {
                                            if (player.HasData("Player:HorseStick"))
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Du musst zuerst den Horsestick weglegen!", "error");
                                                return;
                                            }
                                            if (player.HasData("Player:GlowStick"))
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Du musst zuerst den Glowstick weglegen!", "error");
                                                return;
                                            }
                                            if (player.HasData("Player:Pickaxe"))
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Du musst zuerst die Spitzhacke weglegen!", "error");
                                                return;
                                            }
                                            if (player.HasData("Player:Mikrofon"))
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Du musst zuerst das Mikrofon weglegen!", "error");
                                                return;
                                            }
                                            if (player.HasData("Player:Filmkamera"))
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Du musst zuerst die Filmkamera weglegen!", "error");
                                                return;
                                            }
                                            player.SetData<bool>("Player:RunStock", true);
                                            Helper.AddRemoveAttachments(player, "runStock", true);
                                            Helper.SendNotificationWithoutButton(player, "Gehstock ausgerüstet, wähle dazu am besten den Laufstil: Lester/Lester2!", "success", "top-left", 4150);
                                        }
                                        else
                                        {
                                            player.ResetData("Player:RunStock");
                                            Helper.AddRemoveAttachments(player, "runStock", false);
                                            Helper.SendNotificationWithoutButton(player, "Gehstock weggelegt!", "success");
                                        }
                                        OnShowInventory(player, 1);
                                        return;
                                    }
                                    else if (item.description == "Dietrich")
                                    {
                                        if (player.IsInVehicle)
                                        {
                                            Helper.SendNotificationWithoutButton(player, "Du musst zuerst aus deinem Fahrzeug aussteigen!", "error");
                                            return;
                                        }
                                        Doors door = null;
                                        Vehicle vehicle = Helper.GetClosestVehicle(player, 3.55f);
                                        House house = House.GetClosestHouse(player, 2.85f);
                                        if (house == null)
                                        {
                                            door = DoorsController.GetClosestDoor(player, 2.15f);
                                        }
                                        if (vehicle != null && vehicle.Exists)
                                        {
                                            if (vehicle.Locked == false)
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Das Fahrzeug ist bereits offen!", "error");
                                                return;
                                            }
                                            OnShowInventory(player, 1);
                                            player.TriggerEvent("Client:StartLockpicking", 12, "vehicle", "Fahrzeug wird aufgeknackt...");
                                            return;
                                        }
                                        if (house != null)
                                        {
                                            if (house.locked == 0)
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Das Haus ist bereits offen!", "error");
                                                return;
                                            }
                                            OnShowInventory(player, 1);
                                            player.TriggerEvent("Client:StartLockpicking", 15, "house", "Haustür wird aufgeknackt...");
                                            return;
                                        }
                                        if (door != null)
                                        {
                                            if (door.toogle == false)
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Die Türe ist bereits offen!", "error");
                                                return;
                                            }
                                            OnShowInventory(player, 1);
                                            player.TriggerEvent("Client:StartLockpicking", 15, "door", "Tür wird aufgeknackt...");
                                            return;
                                        }
                                        Player getPlayer = Helper.GetClosestPlayer(player, 2.5f);
                                        TempData tempData2 = Helper.GetCharacterTempData(getPlayer);
                                        if (tempData2.cuffed == 1)
                                        {
                                            OnShowInventory(player, 1);
                                            player.TriggerEvent("Client:StartLockpicking", 12, "player", "Handschellen wird aufgeknackt...");
                                            return;
                                        }
                                        Helper.SendNotificationWithoutButton(player, "Du kannst hier nichts aufknacken!", "error");
                                    }
                                    else
                                    {
                                        Helper.SendNotificationWithoutButton(player, "Dieses Item kann nicht benutzt werden!", "error");
                                    }
                                }
                            }
                            else if (item.type == 6)
                            {
                                bool found = false;
                                string weaponClass = "-1";
                                string weaponClass2 = "-1";
                                Items itemfound = null;
                                if (item.description == "9mm-Munition" || item.description == "Flare" || item.description == "Shotgun-Munition" || item.description == "5.56-Munition" || item.description == "7.62-Munition" || item.description == "Rakete")
                                {
                                    if (item.description == "9mm-Munition")
                                    {
                                        weaponClass = "0";
                                        weaponClass2 = "3";
                                    }
                                    else if (item.description == "Shotgun-Munition")
                                    {
                                        weaponClass = "4";
                                    }
                                    else if (item.description == "5.56-Munition")
                                    {
                                        weaponClass = "5";
                                        weaponClass2 = "6";
                                    }
                                    else if (item.description == "7.62-Munition")
                                    {
                                        weaponClass2 = "7";
                                    }
                                    else if (item.description == "Rakete")
                                    {
                                        weaponClass2 = "8";
                                    }
                                    else if (item.description == "Flare")
                                    {
                                        weaponClass = "1";
                                    }
                                    foreach (Items iteminlist in tempData.itemlist)
                                    {
                                        if (iteminlist != null && iteminlist.type == 5 && !iteminlist.description.ToLower().Contains("schutzweste"))
                                        {
                                            string[] weaponArray = new string[7];
                                            weaponArray = iteminlist.props.Split(",");
                                            if (weaponArray[1] == "1" && (weaponArray[2] == weaponClass || weaponArray[2] == weaponClass2))
                                            {
                                                if (item.description == "Flare" && iteminlist.description != "Flaregun") continue;
                                                found = true;
                                                itemfound = iteminlist;
                                                break;
                                            }
                                        }
                                    }
                                    if (found == true && (WeaponHash)WeaponController.GetWeaponHashFromName(itemfound.description) == NAPI.Player.GetPlayerCurrentWeapon(player))
                                    {
                                        if (NAPI.Player.GetPlayerWeaponAmmo(player, (WeaponHash)WeaponController.GetWeaponHashFromName(itemfound.description.ToLower())) >= 325)
                                        {
                                            Helper.SendNotificationWithoutButton(player, "Die Waffe ist voll!", "error");
                                            return;
                                        }
                                        if (NAPI.Player.GetPlayerWeaponAmmo(player, (WeaponHash)WeaponController.GetWeaponHashFromName(itemfound.description)) > 0)
                                        {
                                            Helper.PlayShortAnimation(player, "weapons@first_person@aim_rng@generic@pistol@ap_pistol@str", "reload_aim_l_empty", 1350);
                                        }
                                        NAPI.Player.SetPlayerWeaponAmmo(player, (WeaponHash)WeaponController.GetWeaponHashFromName(itemfound.description), NAPI.Player.GetPlayerWeaponAmmo(player, (WeaponHash)WeaponController.GetWeaponHashFromName(itemfound.description)) + amount);
                                        Helper.SendNotificationWithoutButton(player, $"Du hast deine Waffe mit {amount} Schuss nachgeladen!", "success");
                                        item.amount -= amount;
                                        string[] weaponArray = new string[7];
                                        weaponArray = itemfound.props.Split(",");
                                        itemfound.props = $"{NAPI.Player.GetPlayerWeaponAmmo(player, (WeaponHash)WeaponController.GetWeaponHashFromName(itemfound.description))},1,{weaponArray[2]},{weaponArray[3]},{weaponArray[4]},{weaponArray[5]},{weaponArray[6]}";
                                        UpdateInventory(player);
                                    }
                                    else
                                    {
                                        Helper.SendNotificationWithoutButton(player, "Du hast keine passende Waffe ausgerüstet!", "error");
                                    }
                                }
                                else
                                {
                                    Helper.SendNotificationWithoutButton(player, "Du hast keine passende Waffe ausgerüstet!", "error");
                                }
                            }
                            else
                            {
                                Helper.SendNotificationWithoutButton(player, "Dieses Item kann nicht benutzt werden!", "error");
                            }
                            break;
                        }
                    case "trash":
                        {
                            if (tempData.inCall == true && item.description.ToLower() == "smartphone")
                            {
                                Helper.SendNotificationWithoutButton(player, $"Während des Telefonprozesses kannst du keine Handys wegwerfen!", "error", "top-end");
                                return;
                            }
                            foreach (ItemsGlobal globalitem in itemListGlobal)
                            {
                                if (globalitem != null && globalitem.owneridentifier == "Ground")
                                {
                                    if (Helper.IsInRangeOfPoint(player.Position, new Vector3(globalitem.posx, globalitem.posy, globalitem.posz), 0.375f) && player.Dimension == globalitem.dimension)
                                    {
                                        Helper.SendNotificationWithoutButton(player, "Hier liegt schon etwas anderes!", "error");
                                        return;
                                    }
                                }
                            }
                            ItemsGlobal itemglobal = new ItemsGlobal();
                            itemglobal.itemid = GetFreeItemIDGlobal();
                            itemglobal.description = item.description;
                            itemglobal.ownerid = -1;
                            itemglobal.owneridentifier = "Ground";
                            itemglobal.lastupdate = Helper.UnixTimestamp();
                            itemglobal.hash = item.hash;
                            itemglobal.amount = amount;
                            itemglobal.weight = item.weight;
                            itemglobal.type = item.type;
                            if (itemglobal.description.ToLower() == "granate" || itemglobal.description.ToLower() == "bzgas" || itemglobal.description.ToLower() == "rauchgranate" || itemglobal.description.ToLower() == "molotowcocktail" || itemglobal.description.ToLower() == "snowball")
                            {
                                string[] weaponArray = new string[7];
                                weaponArray = item.props.Split(",");
                                itemglobal.props = $"{amount},{weaponArray[1]},{weaponArray[2]},{weaponArray[3]},{weaponArray[4]},{weaponArray[5]},{weaponArray[6]}";
                            }
                            else
                            {
                                itemglobal.props = item.props;
                            }
                            if (item.description.ToLower() == "granate" || item.description.ToLower() == "bzgas" || item.description.ToLower() == "rauchgranate" || item.description.ToLower() == "molotowcocktail" || itemglobal.description.ToLower() == "snowball")
                            {
                                string[] weaponArray = new string[7];
                                weaponArray = item.props.Split(",");
                                item.props = $"{Convert.ToInt32(weaponArray[0]) - amount},{weaponArray[1]},{weaponArray[2]},{weaponArray[3]},{weaponArray[4]},{weaponArray[5]},{weaponArray[6]}";
                            }
                            itemglobal.posx = player.Position.X;
                            itemglobal.posy = player.Position.Y;
                            itemglobal.posz = player.Position.Z - 0.92f;
                            itemglobal.dimension = player.Dimension;
                            itemglobal.objectHandle = NAPI.Object.CreateObject((uint)Convert.ToInt64(itemglobal.hash), new Vector3(itemglobal.posx, itemglobal.posy, itemglobal.posz), new Vector3(0.0f, 0.0f, 0.0f), 255, itemglobal.dimension);
                            itemglobal.textHandle = NAPI.TextLabel.CreateTextLabel("" + itemglobal.amount + "x " + itemglobal.description + "", new Vector3(itemglobal.posx, itemglobal.posy, itemglobal.posz + 0.3f), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, itemglobal.dimension);
                            itemListGlobal.Add(itemglobal);
                            if(item.description == "Smartphone" && item.props == character.lastsmartphone)
                            {
                                character.lastsmartphone = "n/A";
                            }
                            Helper.SendNotificationWithoutButton(player, "Du hast " + amount + "x  " + item.description + " weggeworfen!", "success");
                            Helper.PlayShortAnimation(player, "random@domestic", "pickup_low", 1650);
                            item.amount -= amount;
                            break;
                        }
                    case "give":
                        {
                            if (tempData.inventoysetting.Contains("search"))
                            {
                                Helper.SendNotificationWithoutButton(player, $"Du kannst jetzt keine Items vergeben!", "error", "top-end");
                                return;
                            }
                            Player target = Helper.GetClosestPlayer(player, 2.75f);
                            if (target == null)
                            {
                                Helper.SendNotificationWithoutButton(player, "Ungültiger Spieler!", "error", "top-end");
                                return;
                            }
                            if (!CanPlayerHoldItem(target, (item.weight * amount)))
                            {
                                Helper.SendNotificationWithoutButton(player, $"Der Spieler kann {item.amount}x {item.description} nicht mehr tragen!", "error", "top-end");
                                Helper.SendNotificationWithoutButton(target, $"Du kannst {item.amount}x {item.description} nicht mehr tragen!", "error", "top-end");
                                return;
                            }
                            if (item.description.ToLower() == "smartphone" && tempData.inCall == true)
                            {
                                Helper.SendNotificationWithoutButton(player, $"Während des Telefonprozesses kannst du keine Handys vergeben!", "error", "top-end");
                                return;
                            }
                            if (player.HasData("Player:FishingRod") && item.description.ToLower() == "angel")
                            {
                                Helper.SendNotificationWithoutButton(player, $"Du musst deine Angel zuerst abbauen!", "error", "top-end");
                                return;
                            }
                            if(item.description.ToLower() == "snowball")
                            {
                                Helper.SendNotificationWithoutButton(player, $"Du kannst keine Snowballs vergeben!", "error", "top-end");
                                return;
                            }
                            if (item.type == 5)
                            {
                                string[] weaponArray = new string[7];
                                weaponArray = item.props.Split(",");
                                if (weaponArray[1] == "1")
                                {
                                    Helper.SendNotificationWithoutButton(player, $"Dieses Item kann jetzt nicht vergeben werden!", "error", "top-end");
                                    return;
                                }
                            }
                            bool found = false;
                            TempData tempData2 = Helper.GetCharacterTempData(target);
                            Character getCharacter = Helper.GetCharacterData(target);
                            foreach (Items iteminlist in tempData2.itemlist)
                            {
                                if (iteminlist != null && !CheckDoubleItems(item.description.ToLower()) && item.description.ToLower() == iteminlist.description.ToLower())
                                {
                                    iteminlist.amount += item.amount;
                                    item.amount -= amount;
                                    if (item.description.ToLower() == "granate" || item.description.ToLower() == "bzgas" || item.description.ToLower() == "rauchgranate" || item.description.ToLower() == "molotowcocktail" || item.description.ToLower() == "snowball")
                                    {
                                        string[] weaponArray = new string[7];
                                        weaponArray = iteminlist.props.Split(",");
                                        iteminlist.props = $"{iteminlist.amount},{weaponArray[1]},{weaponArray[2]},{weaponArray[3]},{weaponArray[4]},{weaponArray[5]},{weaponArray[6]}";
                                        weaponArray = item.props.Split(",");
                                        item.props = $"{item.amount},{weaponArray[1]},{weaponArray[2]},{weaponArray[3]},{weaponArray[4]},{weaponArray[5]},{weaponArray[6]}";
                                    }
                                    found = true;
                                    break;
                                }
                            }
                            if (found == false)
                            {
                                Items newItem = new Items();
                                newItem.itemid = GetFreeItemIDGlobal();
                                newItem.ownerid = getCharacter.id;
                                newItem.owneridentifier = "Player";
                                newItem.description = item.description;
                                newItem.amount = amount;
                                item.amount -= amount;
                                newItem.weight = item.weight;
                                if (item.description.ToLower() == "granate" || item.description.ToLower() == "bzgas" || item.description.ToLower() == "rauchgranate" || item.description.ToLower() == "molotowcocktail" || item.description.ToLower() == "snowball")
                                {
                                    string[] weaponArray = new string[7];
                                    weaponArray = item.props.Split(",");
                                    item.props = $"{item.amount},{weaponArray[1]},{weaponArray[2]},{weaponArray[3]},{weaponArray[4]},{weaponArray[5]},{weaponArray[6]}";
                                    newItem.props = $"{newItem.amount},{weaponArray[1]},{weaponArray[2]},{weaponArray[3]},{weaponArray[4]},{weaponArray[5]},{weaponArray[6]}";
                                }
                                else
                                {
                                    newItem.props = item.props;
                                }
                                newItem.type = item.type;
                                newItem.hash = item.hash;
                                tempData2.itemlist.Add(newItem);
                            }
                            if (item.description == "Smartphone" && item.props == character.lastsmartphone && character.faction > 0)
                            {
                                character.lastsmartphone = "";
                                FactionsModel faction = FactionController.GetFactionById(character.faction);
                                if (faction != null && faction.number != "" && faction.number == item.props)
                                {
                                    faction.number = "";
                                    faction.numbername = "";
                                }
                            }
                            if (item.description == "Smartphone" && item.props == character.lastsmartphone)
                            {
                                character.lastsmartphone = "n/A";
                            }
                            Commands.cmd_animation(player, "give", true);
                            Helper.SendNotificationWithoutButton(player, $"Du hast dem Spieler {amount}x {item.description} gegeben!", "success", "top-end");
                            Helper.SendNotificationWithoutButton(target, $"Du hast {amount}x {item.description} erhalten!", "success", "top-end");
                            UpdateInventory(target);
                            if(item.type == 5 || item.type == 6 || item.type == 7)
                            {
                                if (tempData.ingangzone != -1 && character.mygroup != -1)
                                {
                                    Groups group = GroupsController.GetGroupById(character.mygroup);
                                    if (group != null && group.status == 0)
                                    {
                                        Groups group2 = GroupsController.GetGroupById(character2.mygroup);
                                        if((group2 != null && group2.id != group.id) || group2 == null)
                                        {
                                            GangController.UpdateGangZoneValues(tempData.ingangzone, group.id, 3);
                                        }
                                    }
                                }
                            }
                            break;
                        }
                }
                if (usage != "move" && item.amount <= 0)
                {
                    RemoveItem(player, itemid);
                }
                UpdateInventory(player);
                if (usage != "move")
                {
                    CharacterController.SaveCharacter(player);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnUseInventory]: " + e.ToString());
            }
        }

        public static void GetAllGlobalItems()
        {
            try
            {
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                ItemsGlobalModel itemsGlobalModel = db.Single<ItemsGlobalModel>("WHERE id = 1");
                ItemsGlobal itemGlobal = null;
                if (itemsGlobalModel.json.Length > 5)
                {
                    itemListGlobal = JsonConvert.DeserializeObject<List<ItemsGlobal>>(itemsGlobalModel.json);

                    foreach (ItemsGlobal globalitem in itemListGlobal.ToList())
                    {
                        if (globalitem != null && globalitem.lastupdate > 0 && globalitem.owneridentifier == "Ground")
                        {
                            if ((globalitem.lastupdate + 432000) < Helper.UnixTimestamp())
                            {
                                if (globalitem.description == "Smartphone")
                                {
                                    MySqlCommand command = General.Connection.CreateCommand();
                                    command.CommandText = "DELETE FROM smartphones WHERE phonenumber=@phonenumber LIMIT 1";
                                    command.Parameters.AddWithValue("@phonenumber", globalitem.props.Remove(0, 4));
                                    command.ExecuteNonQuery();
                                }
                                if (globalitem.type == 5 && globalitem.props.Length > 0)
                                {
                                    string[] weaponArray = new string[7];
                                    weaponArray = globalitem.props.Split(",");
                                    MySqlCommand command2 = General.Connection.CreateCommand();
                                    command2.CommandText = "DELETE FROM weapons WHERE ident=@identificationtype LIMIT 1";
                                    command2.Parameters.AddWithValue("@identificationtype", weaponArray[3]);
                                    command2.ExecuteNonQuery();
                                }
                                itemListGlobal.Remove(globalitem);
                                itemGlobal = globalitem;
                                itemGlobal = null;
                            }
                        }
                    }

                    foreach (ItemsGlobal globalitem in itemListGlobal)
                    {
                        if (globalitem != null)
                        {
                            globalitem.itemid = GetFreeItemIDGlobal();
                            if (globalitem.owneridentifier == "Ground")
                            {
                                globalitem.objectHandle = NAPI.Object.CreateObject((int)Convert.ToInt64(globalitem.hash), new Vector3(globalitem.posx, globalitem.posy, globalitem.posz), new Vector3(0.0f, 0.0f, 0.0f), 255, globalitem.dimension);
                                globalitem.textHandle = NAPI.TextLabel.CreateTextLabel("" + globalitem.amount + "x " + globalitem.description + "", new Vector3(globalitem.posx, globalitem.posy, globalitem.posz + 0.3f), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, globalitem.dimension);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetAllGlobalItems]: " + e.ToString());
            }
        }

        public static void SaveAllGlobalItems()
        {
            try
            {
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                ItemsGlobalModel itemsGlobalModel = new ItemsGlobalModel();

                List<ItemsGlobal> saveItemData = new List<ItemsGlobal>();
                foreach (ItemsGlobal globalItem in itemListGlobal)
                {
                    saveItemData.Add(new ItemsGlobal()
                    {
                        ownerid = globalItem.ownerid,
                        owneridentifier = globalItem.owneridentifier,
                        itemid = globalItem.itemid,
                        hash = globalItem.hash,
                        description = globalItem.description,
                        amount = globalItem.amount,
                        type = globalItem.type,
                        weight = globalItem.weight,
                        props = globalItem.props,
                        posx = globalItem.posx,
                        posy = globalItem.posy,
                        posz = globalItem.posz,
                        dimension = globalItem.dimension,
                        lastupdate = globalItem.lastupdate
                    });
                }

                itemsGlobalModel.id = 1;
                itemsGlobalModel.json = NAPI.Util.ToJson(saveItemData);

                db.Save(itemsGlobalModel);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[SaveAllGlobalItems]: " + e.ToString());
            }
        }

        public static bool CheckDoubleItems(string name)
        {
            try
            {
                if (name.ToLower().Contains("ec-karte") || name.ToLower().Contains("hausschlüssel") || name.ToLower().Contains("mietschlüssel") || name.ToLower().Contains("fahrzeugschlüssel") || name.ToLower().Contains("bizzschlüssel") || name.ToLower().Contains("benzinkanister") || name.ToLower().Contains("smartphone") || name.ToLower().Contains("f-zeugnis") || name.ToLower().Contains("feuerzeug") || name.ToLower().Contains("angel")
                || name.ToLower().Contains("schweissgerät") || name.ToLower().Contains("drohne") || name.ToLower().Contains("kleines-messer") || name.ToLower().Contains("funkgerät") || name.ToLower().Contains("rezept") || name.ToLower().Contains("glowstick") || name.ToLower().Contains("horsestick") || name.ToLower().Contains("gehstock") || name.ToLower().Contains("l-schein") || name.ToLower().Contains("parkkralle") || name.ToLower().Contains("schwefelsäure") 
                || name.ToLower().Contains("frostschutzmittel") || name.ToLower().Contains("backmischung") || name.ToLower().Contains("spitzhacke") || name.ToLower().Contains("kleine-schaufel") || name.ToLower().Contains("busticket") || name.ToLower().Contains("filmkamera") || name.ToLower().Contains("mikrofon") || name.ToLower().Contains("defribrilator") || name.ToLower().Contains("stethoskop"))
                {
                    return true;
                }
                if (IsItemAWeapon(name) && name.ToLower() != "granate" && name.ToLower() != "bzgas" && name.ToLower() != "rauchgranate" && name.ToLower() != "molotowcocktail" && name.ToLower() != "snowball")
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CheckDoubleItems]: " + e.ToString());
            }
            return false;
        }

        public static bool IsItemAWeapon(string name)
        {
            try
            {
                if (WeaponController.GetWeaponClass(name.ToLower()) != "-1" || name.ToLower().Contains("schutzweste"))
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[IsItemAWeapon]: " + e.ToString());
            }
            return false;
        }

        [RemoteEvent("Server:PrepareHack")]
        public static void OnPrepareHack(Player player, int hack = 0)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                TempData tempData = Helper.GetCharacterTempData(player);
                if (character == null || tempData == null) return;

                if(hack == 0)
                {
                    Helper.SendNotificationWithoutButton(player, "Du kannst das Hackwerkzeug hier nicht benutzen!", "error", "top-end");
                    return;
                }
                Items getItem = ItemsController.GetItemByItemName(player, "Hackwerkzeug");
                if(getItem != null)
                {
                    getItem.amount--;
                    if(getItem.amount <= 0)
                    {
                        ItemsController.RemoveItem(player, getItem.itemid);
                    }
                    Commands.cmd_animation(player, "give2", true);
                    player.SetData<int>("Player:VendingCooldown", Helper.UnixTimestamp()+(60*10));
                    tempData.tempValue = hack;
                    if (hack == 1)
                    {
                        player.TriggerEvent("Client:StartHack", 3000);
                    }
                    else
                    {
                        player.TriggerEvent("Client:StartHack", 4500);
                        if(Helper.GetRandomPercentage(25))
                        {
                            Dispatch dispatch = new Dispatch();
                            MDCController.dispatchCount++;
                            dispatch.id = MDCController.dispatchCount;
                            dispatch.text = $"Stiller Alarm bei einem Bankautomaten. Der Bankautomat wird gehackt!";
                            dispatch.name = "Bankatuomat";
                            dispatch.phonenumber = "0189911";
                            dispatch.to = 1;
                            dispatch.position = $"{player.Position.X.ToString(new CultureInfo("en-US")):N3},{player.Position.Y.ToString(new CultureInfo("en-US")):N3},{player.Position.Z.ToString(new CultureInfo("en-US")):N3}";
                            dispatch.timestamp = Helper.UnixTimestamp();
                            MDCController.dispatchList.Add(dispatch);
                            MDCController.SendPoliceMDCMessage(player, $"Neuer Dispatch verfügbar - DSPTH-{dispatch.id}!");
                        }
                    }
                    Helper.AddRemoveAttachments(player, "tablet", true);
                    player.SetSharedData("Player:AnimData", $"amb@code_human_in_bus_passenger_idles@female@tablet@base%base%{17}");
                }
                else
                {
                    Helper.SendNotificationWithoutButton(player, "Du hast kein Hackwerkzeug dabei!", "error", "top-end");
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnPrepareHack]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:StopHack")]
        public static void OnStopHack(Player player, int set = 0)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                TempData tempData = Helper.GetCharacterTempData(player);
                if (character == null || tempData == null) return;

                bool success = false;
                Random rand = new Random();
                int money = 0;

                Helper.OnStopAnimation2(player);
                Helper.AddRemoveAttachments(player, "tablet", false);

                if (set == -1) return;

                if (tempData.tempValue == 1)
                {
                    if(Helper.GetRandomPercentage(75))
                    {
                        success = true;
                        money = rand.Next(4000, 7550);
                    }
                }
                else if (tempData.tempValue == 2)
                {
                    if (Helper.GetRandomPercentage(45))
                    {
                        success = true;
                        money = rand.Next(8500, 18500);
                    }
                }
                if(success == true)
                {
                    player.TriggerEvent("Client:PlaySoundPeep2");
                    Helper.SendNotificationWithoutButton(player, $"Du hast den Gerätecode erfolgreich geknackt, du erhältst {money}$!", "success", "top-end", 4500);
                    CharacterController.SetMoney(player, money);
                }
                else
                {
                    if (Helper.GetRandomPercentage(35))
                    {
                        Helper.SendNotificationWithoutButton(player, "Du konntest den Gerätecode nicht knacken, du hast einen Elektroschock bekommen!", "error", "top-end", 4500);
                        player.TriggerEvent("Player:Tase");
                        Helper.SetPlayerHealth(player, NAPI.Player.GetPlayerHealth(player) / 2);
                    }
                    else
                    {
                        Helper.SendNotificationWithoutButton(player, "Du konntest den Gerätecode nicht knacken, Hackvorgang fehlgeschlagen!", "error", "top-end", 4500);
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnStopHack]: " + e.ToString());
            }
        }
    }
}
