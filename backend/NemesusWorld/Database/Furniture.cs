using System;
using System.Collections.Generic;
using System.Linq;
using GTANetworkAPI;
using MySqlConnector;
using NemesusWorld.Controllers;
using NemesusWorld.Models;
using NemesusWorld.Utils;

namespace NemesusWorld.Database
{
    class Furniture : Script
    {
        //Furniture functions
        public static FurnitureModel GetFurnitureModelById(int id)
        {
            try
            {
                foreach (FurnitureModel furnitureModel in House.furnitureModelList)
                {
                    if (furnitureModel != null && furnitureModel.id == id)
                    {
                        return furnitureModel;
                    }
                }
            }            
            catch (Exception e)
            {
                Helper.ConsoleLog("error", "[GetFurnitureModelById]: " + e.ToString());
            }
            return null;
        }

        public static FurnitureModel GetFurnitureByHash(string hash)
        {
            try
            {
                foreach (FurnitureModel furnitureModel in House.furnitureModelList)
                {
                    if (furnitureModel != null && furnitureModel.hash == hash)
                    {
                        return furnitureModel;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", "[GetFurnitureByHash]: " + e.ToString());
            }
            return null;
        }

        public static FurnitureSetHouse GetFurnitureById(int id, int houseid)
        {
            try
            {
                foreach (FurnitureSetHouse furniture in House.furnitureList)
                {
                    if (furniture != null && furniture.id == id && furniture.house == houseid)
                    {
                        return furniture;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", "[GetFurnitureById]: " + e.ToString());
            }
            return null;
        }

        [RemoteEvent("Server:SetMoebelModus")]
        public static void OnSetMoebelModus(Player player, bool flag)
        {
            try
            {
                player.SetData("Player:MoebelModus", flag);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", "[OnSetMoebelModus]: " + e.ToString());
            }
        }

        public static void UpdateMöbelList(Player player, List<FurnitureFrontend> furnitureFrontendList, bool all = false)
        {
            try
            {
                NAPI.Task.Run(() =>
                {
                    if (player.GetData<bool>("Player:MoebelModus") == true)
                    {
                        player.TriggerEvent("Client:UpdateMoebelList", NAPI.Util.ToJson(furnitureFrontendList));
                        if (all == true)
                        {
                            foreach (Player p in NAPI.Pools.GetAllPlayers())
                            {
                                if (p != null && p != player && p.GetOwnSharedData<bool>("Player:Spawned") == true && p.GetData<bool>("Player:MoebelModus") == true && player.Dimension == p.Dimension && player.Position.DistanceTo(p.Position) <= 25.5)
                                {
                                    p.TriggerEvent("Client:UpdateMoebelList", NAPI.Util.ToJson(furnitureFrontendList));
                                }
                            }
                        }
                    }
                }, delayTime: 375);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", "[UpdateMöbelList]: " + e.ToString());
            }
        }

        public static int SellAllFurniture(int houseid)
        {
            int money = 0;
            try
            {
                foreach (FurnitureSetHouse furniture in House.furnitureList.ToList())
                {
                    if (furniture != null && furniture.house == houseid)
                    {
                        FurnitureModel furnitureModel = GetFurnitureByHash(furniture.hash);
                        if (furnitureModel != null)
                        {
                            money += (int)(furnitureModel.price / 2);
                            if (furniture.extra == 1 || furniture.extra == 2 || furniture.extra == 3)
                            {
                                foreach (ItemsGlobal globalitem in ItemsController.itemListGlobal.ToList())
                                {
                                    if (globalitem != null && globalitem.owneridentifier == "Furniture-" + furniture.hash)
                                    {
                                        ItemsController.itemListGlobal.Remove(globalitem);
                                    }
                                }
                            }
                            if (furniture.extra == 5)
                            {
                                furniture.props = "false";
                                Doors door = new Doors();
                                door = DoorsController.GetDoorByPosition(furniture.position);
                                if (door != null)
                                {
                                    DoorsController.RemoveDoor(door);
                                }
                            }
                            if(furniture.name.Contains("Kleiderschrank"))
                            {
                                MySqlCommand command = General.Connection.CreateCommand();
                                command.CommandText = "DELETE FROM outfits WHERE owner = @owner";
                                command.Parameters.AddWithValue("@owner", "furniture-" + furniture.id);
                                command.ExecuteNonQuery();
                            }
                            furniture.position = "0.0|0.0|0.0|0.0|0.0|0.0|0";
                            if (furniture.objectHandle != null)
                            {
                                furniture.objectHandle.Delete();
                                furniture.objectHandle = null;
                            }
                            House.furnitureList.Remove(furniture);
                            House.DeleteFurniture(furniture);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", "[SellAllFurniture]: " + e.ToString());
            }
            return money;
        }

        public static void CancelAllFurniture(int houseid)
        {
            try
            {
                foreach (FurnitureSetHouse furniture in House.furnitureList.ToList())
                {
                    if (furniture != null && furniture.house == houseid)
                    {
                        if (furniture.extra == 1 || furniture.extra == 2 || furniture.extra == 3)
                        {
                            foreach (ItemsGlobal globalitem in ItemsController.itemListGlobal.ToList())
                            {
                                if (globalitem != null && globalitem.owneridentifier == "Furniture-" + furniture.hash)
                                {
                                    ItemsController.itemListGlobal.Remove(globalitem);
                                }
                            }
                        }
                        if (furniture.extra == 5)
                        {
                            furniture.props = "false";
                            Doors door = new Doors();
                            door = DoorsController.GetDoorByPosition(furniture.position);
                            if (door != null)
                            {
                                DoorsController.RemoveDoor(door);
                            }
                        }
                        if (furniture.name.Contains("Kleiderschrank"))
                        {
                            MySqlCommand command = General.Connection.CreateCommand();
                            command.CommandText = "DELETE FROM outfits WHERE owner = @owner";
                            command.Parameters.AddWithValue("@owner", "furniture-" + furniture.id);
                            command.ExecuteNonQuery();
                        }
                        furniture.position = "0.0|0.0|0.0|0.0|0.0|0.0|0";
                        if (furniture.objectHandle != null)
                        {
                            furniture.objectHandle.Delete();
                            furniture.objectHandle = null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", "[CancelAllFurniture]: " + e.ToString());
            }
        }

        public static int CountFurniture(int houseid)
        {
            int count = 0;
            try
            {
                foreach (FurnitureSetHouse furniture in House.furnitureList.ToList())
                {
                    if (furniture != null && furniture.house == houseid)
                    {
                        count++;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", "[CountFurniture]: " + e.ToString());
            }
            return count;
        }

        public static int CountFurnitureRealWorld(int houseid)
        {
            int count = 0;
            try
            {
                foreach (FurnitureSetHouse furniture in House.furnitureList.ToList())
                {
                    if (furniture != null && furniture.house == houseid && furniture.objectHandle != null && furniture.objectHandle.Dimension == 0)
                    {
                        count++;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", "[CountFurniture]: " + e.ToString());
            }
            return count;
        }

        public static int CountFurnitureWithProp(int houseid, string prop)
        {
            int count = 0;
            try
            {
                foreach (FurnitureSetHouse furniture in House.furnitureList.ToList())
                {
                    if (furniture != null && furniture.house == houseid && furniture.props == prop)
                    {
                        count++;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", "[CountFurnitureWithProp]: " + e.ToString());
            }
            return count;
        }

        public static bool CheckOwnFurnitureById(int id, int houseid)
        {
            try
            {
                foreach (FurnitureSetHouse furniture in House.furnitureList)
                {
                    if (furniture != null && furniture.id == id && furniture.house == houseid)
                    {
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", "[CheckOwnFurnitureById]: " + e.ToString());
            }
            return false;
        }

        public static FurnitureSetHouse GetClosestFurniture(Player player, int hausid, float distance = 1.5f)
        {
            try
            {
                FurnitureSetHouse retFurniture = null;
                if (House.furnitureList == null) return retFurniture;
                foreach (FurnitureSetHouse furniture in House.furnitureList)
                {
                    if (furniture != null && furniture.objectHandle != null && player.Position.DistanceTo(furniture.objectHandle.Position) < distance && player.Dimension == furniture.objectHandle.Dimension)
                    {
                        retFurniture = furniture;
                        distance = player.Position.DistanceTo(furniture.objectHandle.Position);
                    }
                }
                return retFurniture;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", "[GetClosestFurniture]: " + e.ToString());
            }
            return null;
        }

        //Furniture Settings
        [RemoteEvent("Server:FurnitureSettings")]
        public static void OnFurnitureSettings(Player player, string setting, int id, bool flag = false)
        {
            try
            {
                House house = null;
                Character character = Helper.GetCharacterData(player);
                TempData tempData = Helper.GetCharacterTempData(player);
                if (tempData == null) return;
                player.TriggerEvent("Client:PlayerFreeze", false);
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
                if (house != null && House.HasPlayerHouseKey(player, house.id) && id >= 0)
                {
                    FurnitureModel furnitureModel = null;
                    FurnitureSetHouse furniture = null;
                    if (setting == "abortmoebel")
                    {
                        if (tempData.editfurniture == false) return;
                        if (tempData.furnitureNew == false)
                        {
                            furniture = GetFurnitureById(tempData.furnitureID, house.id);
                            if (furniture.objectHandle != null)
                            {
                                furniture.objectHandle.Delete();
                                furniture.objectHandle = null;
                            }
                            furniture.objectHandle = NAPI.Object.CreateObject(Convert.ToInt32(furniture.hash), tempData.furnitureOldPosition, tempData.furnitureOldRotation, 255, player.Dimension);
                            furniture.objectHandle.Position = tempData.furnitureOldPosition;
                            furniture.objectHandle.Rotation = tempData.furnitureOldRotation;
                            if (tempData.furnitureObject != null)
                            {
                                tempData.furnitureObject.Delete();
                                tempData.furnitureObject = null;
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
                        tempData.editfurniture = false;
                        tempData.furnitureID = 0;
                        tempData.furnitureNew = false;
                        Helper.SendNotificationWithoutButton(player, "Möbelaufbau abgebrochen!", "success", "top-left");
                        Menu.OnStartMoebel(player);
                        return;
                    }
                    else if (setting == "set")
                    {
                        if (tempData.editfurniture == false) return;
                        Business bizz = Business.GetBusinessById(42);
                        if (tempData.furnitureNew == true)
                        {
                            if (house.position.DistanceTo(tempData.furniturePosition) > 25.5 && player.Dimension != house.id)
                            {
                                Helper.SendNotificationWithoutButton(player, "Ungültige Möbelposition!", "error", "top-left");
                                tempData.furnitureRotation = null;
                                tempData.furniturePosition = null;
                                if (tempData.furnitureObject != null)
                                {
                                    tempData.furnitureObject.Delete();
                                    tempData.furnitureObject = null;
                                }
                                tempData.furnitureNew = false;
                                tempData.editfurniture = false;
                                tempData.furnitureID = 0;
                                player.TriggerEvent("Client:UpdateHud3");
                                return;
                            }
                            if(CountFurnitureRealWorld(house.id) >= 50 && player.Dimension == 0)
                            {
                                Helper.SendNotificationWithoutButton(player, "Außerhalb des Hauses können nur max. 50 Möbelstücke aufgebaut werden!", "error", "top-left");
                                tempData.furnitureRotation = null;
                                tempData.furniturePosition = null;
                                if (tempData.furnitureObject != null)
                                {
                                    tempData.furnitureObject.Delete();
                                    tempData.furnitureObject = null;
                                }
                                tempData.furnitureNew = false;
                                tempData.editfurniture = false;
                                tempData.furnitureID = 0;
                                player.TriggerEvent("Client:UpdateHud3");
                                return;
                            }
                            Bank bank = BankController.GetDefaultBank(player, character.defaultbank);
                            if (bank == null)
                            {
                                Helper.SendNotificationWithoutButton(player, "Es wurde kein Standardkonto gefunden!", "error", "top-left");
                                tempData.furnitureRotation = null;
                                tempData.furniturePosition = null;
                                if (tempData.furnitureObject != null)
                                {
                                    tempData.furnitureObject.Delete();
                                    tempData.furnitureObject = null;
                                }
                                tempData.furnitureNew = false;
                                tempData.editfurniture = false;
                                tempData.furnitureID = 0;
                                player.TriggerEvent("Client:UpdateHud3");
                                return;
                            }
                            furnitureModel = GetFurnitureModelById(tempData.furnitureID);
                            if (bank.bankvalue < furnitureModel.price)
                            {
                                Helper.SendNotificationWithoutButton(player, $"Soviel Geld liegt nicht auf dem Konto - {furnitureModel.price}$!", "error", "top-left");
                                tempData.furnitureRotation = null;
                                tempData.furniturePosition = null;
                                if (tempData.furnitureObject != null)
                                {
                                    tempData.furnitureObject.Delete();
                                    tempData.furnitureObject = null;
                                }
                                tempData.furnitureNew = false;
                                tempData.editfurniture = false;
                                tempData.furnitureID = 0;
                                player.TriggerEvent("Client:UpdateHud3");
                                return;
                            }
                            if(bizz.products < ((int)furnitureModel.price / 3)/bizz.prodprice)
                            {
                                Helper.SendNotificationWithoutButton(player, $"Das Möbellager ist leer!", "error", "top-left");
                                tempData.furnitureRotation = null;
                                tempData.furniturePosition = null;
                                if (tempData.furnitureObject != null)
                                {
                                    tempData.furnitureObject.Delete();
                                    tempData.furnitureObject = null;
                                }
                                tempData.furnitureNew = false;
                                tempData.editfurniture = false;
                                tempData.furnitureID = 0;
                                player.TriggerEvent("Client:UpdateHud3");
                                return;
                            }
                            furniture = new FurnitureSetHouse();
                            furniture.house = house.id;
                            furniture.extra = furnitureModel.extra;
                            furniture.name = furnitureModel.name;
                            furniture.hash = furnitureModel.hash;
                            furniture.position = $"{tempData.furnitureObject.Position.X}|{tempData.furnitureObject.Position.Y}|{tempData.furnitureObject.Position.Z}|{tempData.furnitureObject.Rotation.X}|{tempData.furnitureObject.Rotation.Y}|{tempData.furnitureObject.Rotation.Z}|{tempData.furnitureObject.Dimension}";
                            furniture.props = "n/A";
                            furniture.price = (int)furnitureModel.price;
                            string[] furPosition = new string[7];
                            furPosition = furniture.position.Split("|");
                            furniture.objectHandle = NAPI.Object.CreateObject(Convert.ToInt32(furniture.hash), new Vector3(float.Parse(furPosition[0]), float.Parse(furPosition[1]), float.Parse(furPosition[2])), new Vector3(float.Parse(furPosition[3]), float.Parse(furPosition[4]), float.Parse(furPosition[5])), 255, uint.Parse(furPosition[6]));
                            Helper.SendNotificationWithoutButton(player, "Möbelstück gekauft und aufgebaut!", "success", "top-left");
                            Business.ManageBizzCash(bizz, furniture.price);
                            bank.bankvalue -= furniture.price;
                            tempData.furnitureID = 0;
                            tempData.furnitureRotation = null;
                            tempData.furniturePosition = null;
                            if (tempData.furnitureObject != null)
                            {
                                tempData.furnitureObject.Delete();
                                tempData.furnitureObject = null;
                            }
                            tempData.furnitureNew = false;
                            if (furniture.extra == 1 || furniture.extra == 2 || furniture.extra == 3)
                            {
                                string UUID = Guid.NewGuid().ToString();
                                furniture.props = UUID;
                            }
                            if (furniture.extra == 5)
                            {
                                furniture.props = "false";
                                Doors door = new Doors();
                                door.id = DoorsController.GetFreeDoorsID();
                                door.hash = furniture.hash;
                                door.posx = float.Parse(furPosition[0]);
                                door.posy = float.Parse(furPosition[1]);
                                door.posz = float.Parse(furPosition[2]);
                                door.dimension = (int)float.Parse(furPosition[6]);
                                door.toogle = false;
                                door.save = false;

                                DoorsController.AddDoor(door, true);
                            }
                            House.AddFurniture(furniture);
                            House.furnitureList.Add(furniture);
                            UpdateMöbelList(player, House.GetFurnitureForHouse(house.id), true);
                        }
                        else
                        {
                            if (house.position.DistanceTo(tempData.furniturePosition) > 25.5 && player.Dimension != house.id)
                            {
                                Helper.SendNotificationWithoutButton(player, "Ungültige Möbelposition!", "error", "top-left");
                                furniture = GetFurnitureById(tempData.furnitureID, house.id);
                                if (furniture.objectHandle != null)
                                {
                                    furniture.objectHandle.Delete();
                                    furniture.objectHandle = null;
                                }
                                furniture.objectHandle = NAPI.Object.CreateObject(Convert.ToInt32(furniture.hash), tempData.furnitureOldPosition, tempData.furnitureOldRotation, 255, player.Dimension);
                                furniture.objectHandle.Position = tempData.furnitureOldPosition;
                                furniture.objectHandle.Rotation = tempData.furnitureOldRotation;
                                if (tempData.furnitureObject != null)
                                {
                                    tempData.furnitureObject.Delete();
                                    tempData.furnitureObject = null;
                                }
                                tempData.editfurniture = false;
                                tempData.furnitureID = 0;
                                tempData.furnitureNew = false;
                                Menu.OnStartMoebel(player);
                                UpdateMöbelList(player, House.GetFurnitureForHouse(house.id), true);
                                return;
                            }
                            if (CountFurnitureRealWorld(house.id) >= 50 && player.Dimension == 0)
                            {
                                Helper.SendNotificationWithoutButton(player, "Außerhalb des Hauses können nur max. 50 Möbelstücke aufgebaut werden!", "error", "top-left");
                                furniture = GetFurnitureById(tempData.furnitureID, house.id);
                                if (furniture.objectHandle != null)
                                {
                                    furniture.objectHandle.Delete();
                                    furniture.objectHandle = null;
                                }
                                furniture.objectHandle = NAPI.Object.CreateObject(Convert.ToInt32(furniture.hash), tempData.furnitureOldPosition, tempData.furnitureOldRotation, 255, player.Dimension);
                                furniture.objectHandle.Position = tempData.furnitureOldPosition;
                                furniture.objectHandle.Rotation = tempData.furnitureOldRotation;
                                if (tempData.furnitureObject != null)
                                {
                                    tempData.furnitureObject.Delete();
                                    tempData.furnitureObject = null;
                                }
                                tempData.editfurniture = false;
                                tempData.furnitureID = 0;
                                tempData.furnitureNew = false;
                                Menu.OnStartMoebel(player);
                                UpdateMöbelList(player, House.GetFurnitureForHouse(house.id), true);
                                return;
                            }
                            bool movingFur = false;
                            furniture = GetFurnitureById(tempData.furnitureID, house.id);
                            Doors door2 = null;
                            if (furniture.extra == 5)
                            {
                                door2 = DoorsController.GetDoorByPosition(tempData.doorPosition);
                            }
                            if (furniture.position == "0.0|0.0|0.0|0.0|0.0|0.0|0")
                            {
                                movingFur = true;
                            }
                            furniture.position = $"{tempData.furnitureObject.Position.X}|{tempData.furnitureObject.Position.Y}|{tempData.furnitureObject.Position.Z}|{tempData.furnitureObject.Rotation.X}|{tempData.furnitureObject.Rotation.Y}|{tempData.furnitureObject.Rotation.Z}|{tempData.furnitureObject.Dimension}";
                            string[] furPosition = new string[7];
                            furPosition = furniture.position.Split("|");
                            furniture.objectHandle = NAPI.Object.CreateObject(Convert.ToInt32(furniture.hash), new Vector3(float.Parse(furPosition[0]), float.Parse(furPosition[1]), float.Parse(furPosition[2])), new Vector3(float.Parse(furPosition[3]), float.Parse(furPosition[4]), float.Parse(furPosition[5])), 255, uint.Parse(furPosition[6]));
                            if (tempData.furnitureObject != null)
                            {
                                tempData.furnitureObject.Delete();
                                tempData.furnitureObject = null;
                            }
                            Helper.SendNotificationWithoutButton(player, "Möbelstück aufgebaut/umgebaut!", "success", "top-left");
                            tempData.furnitureID = 0;
                            tempData.furnitureNew = false;
                            Doors door = null;
                            if (furniture.extra == 5)
                            {
                                furniture.props = "false";
                                if (movingFur == true)
                                {
                                    door = new Doors();
                                    door.id = DoorsController.GetFreeDoorsID();
                                    door.hash = furniture.hash;
                                    door.posx = float.Parse(furPosition[0]);
                                    door.posy = float.Parse(furPosition[1]);
                                    door.posz = float.Parse(furPosition[2]);
                                    door.dimension = (int)float.Parse(furPosition[6]);
                                    door.toogle = false;
                                    door.save = false;
                                }
                                else
                                {
                                    if (door2 != null)
                                    {
                                        door2.posx = float.Parse(furPosition[0]);
                                        door2.posy = float.Parse(furPosition[1]);
                                        door2.posz = float.Parse(furPosition[2]);
                                        door2.dimension = (int)float.Parse(furPosition[6]);
                                    }
                                }

                                if (movingFur == true)
                                {
                                    DoorsController.AddDoor(door, true);
                                }
                                else
                                {
                                    DoorsController.UpdateDoor(player, door2);
                                }
                            }
                            House.SaveFurniture(furniture);
                            UpdateMöbelList(player, House.GetFurnitureForHouse(house.id), true);
                        }
                        player.TriggerEvent("Client:UpdateHud3");
                        tempData.editfurniture = false;
                        return;
                    }
                    else if(setting == "close")
                    {
                        if (tempData.editfurniture == true) return;
                        if (tempData.furnitureNew == false)
                        {
                            furniture = GetFurnitureById(id, house.id);
                            if (furniture.extra == 1 || furniture.extra == 2 || furniture.extra == 3)
                            {
                                int itemscount = CountFurnitureWithProp(id, furniture.props);
                                if(itemscount > 0)
                                {
                                    tempData.furnitureID = 0;
                                    tempData.furnitureRotation = null;
                                    tempData.furniturePosition = null;
                                    tempData.furnitureNew = false;
                                    Helper.SendNotificationWithoutButton(player, "Es befinden sich noch Items in dem Möbelstück!", "error", "top-left");
                                    tempData.editfurniture = false;
                                    player.TriggerEvent("Client:HideCursor");
                                    player.TriggerEvent("Client:UpdateHud3");
                                    return;
                                }
                            }
                            if (furniture.extra == 5)
                            {
                                furniture.props = "false";
                                Doors door = new Doors();
                                door = DoorsController.GetDoorByPosition(furniture.position);
                                if (door != null)
                                {
                                    DoorsController.RemoveDoor(door);
                                }
                            }
                            if (furniture.name.Contains("Kleiderschrank"))
                            {
                                MySqlCommand command = General.Connection.CreateCommand();
                                command.CommandText = "DELETE FROM outfits WHERE owner = @owner";
                                command.Parameters.AddWithValue("@owner", "furniture-" + furniture.id);
                                command.ExecuteNonQuery();
                            }
                            furniture.position = "0.0|0.0|0.0|0.0|0.0|0.0|0";
                            if (furniture.objectHandle != null)
                            {
                                furniture.objectHandle.Delete();
                                furniture.objectHandle = null;
                            }
                            Helper.SendNotificationWithoutButton(player, "Möbelstück abgebaut!", "success", "top-left");
                            tempData.furnitureID = 0;
                            tempData.furnitureNew = false;
                            House.SaveFurniture(furniture);
                            Menu.OnStartMoebel(player);
                        }
                        return;
                    }
                    else if(setting == "delete")
                    {
                        if (tempData.editfurniture == true) return;
                        if (tempData.furnitureNew == false)
                        {
                            furniture = GetFurnitureById(id, house.id);
                            if (furniture.extra == 1 || furniture.extra == 2 || furniture.extra == 3)
                            {
                                int itemscount = CountFurnitureWithProp(id, furniture.props);
                                if (itemscount > 0)
                                {
                                    tempData.furnitureID = 0;
                                    tempData.furnitureRotation = null;
                                    tempData.furniturePosition = null;
                                    tempData.furnitureNew = false;
                                    Helper.SendNotificationWithoutButton(player, "Es befinden sich noch Items in dem Möbelstück!", "error", "top-left");
                                    tempData.editfurniture = false;
                                    player.TriggerEvent("Client:HideCursor");
                                    player.TriggerEvent("Client:UpdateHud3");
                                    return;
                                }
                            }
                            int price = (int)(furniture.price/2);
                            if (furniture != null)
                            {
                                if (furniture.extra == 5)
                                {
                                    furniture.props = "false";
                                    Doors door = new Doors();
                                    door = DoorsController.GetDoorByPosition(furniture.position);
                                    if (door != null)
                                    {
                                        DoorsController.RemoveDoor(door);
                                    }
                                }
                                if (furniture.name.Contains("Kleiderschrank"))
                                {
                                    MySqlCommand command = General.Connection.CreateCommand();
                                    command.CommandText = "DELETE FROM outfits WHERE owner = @owner";
                                    command.Parameters.AddWithValue("@owner", "furniture-" + furniture.id);
                                    command.ExecuteNonQuery();
                                }
                                furniture.position = "0.0|0.0|0.0|0.0|0.0|0.0|0";
                                if (furniture.objectHandle != null)
                                {
                                    furniture.objectHandle.Delete();
                                    furniture.objectHandle = null;
                                }
                                Helper.SendNotificationWithoutButton(player, $"Möbelstück für {price}$ verkauft!", "success", "top-left");
                                tempData.furnitureID = 0;
                                tempData.furnitureNew = false;
                                House.furnitureList.Remove(furniture);
                                House.DeleteFurniture(furniture);
                                Menu.OnStartMoebel(player);
                            }
                        }
                        return;
                    }
                    else if(setting == "move")
                    {
                        if (tempData.editfurniture == true) return;
                        furniture = GetFurnitureById(id, house.id);
                        if (furniture != null)
                        {
                            if (furniture.objectHandle != null && (furniture.objectHandle.Position.DistanceTo(player.Position) > 4.15 || furniture.objectHandle.Position.DistanceTo(player.Position) < 0 || furniture.objectHandle.Dimension != player.Dimension) && furniture.position != "0.0|0.0|0.0|0.0|0.0|0.0|0")
                            {
                                tempData.editfurniture = false;
                                tempData.furnitureID = 0;
                                tempData.furnitureRotation = null;
                                tempData.furniturePosition = null;
                                if (tempData.furnitureObject != null)
                                {
                                    tempData.furnitureObject.Delete();
                                    tempData.furnitureObject = null;
                                }
                                tempData.furnitureNew = false;
                                Helper.SendNotificationWithoutButton(player, "Du bist nicht in der Nähe von dem Möbelstück!", "error", "top-left");
                                player.TriggerEvent("Client:HideCursor");
                                player.TriggerEvent("Client:UpdateHud3");
                                return;
                            }
                            tempData.editfurniture = true;
                            if (flag == true)
                            {
                                player.TriggerEvent("Client:SetFurnitureThings");
                            }
                            player.TriggerEvent("Client:ShowInfobox", "Pfeiltasten -> Position/Rotation ändern", "Shift/ALT - Position/Rotation & X/Y switchen", "B -> Möbelstück bewegen/aufbauen", "Backspace -> Abbrechen", "Möbelaufbau", "Möbelstück: " + furniture.name, "null", 0);
                            tempData.furnitureNew = false;
                            tempData.furnitureID = id;
                            if (furniture.position == "0.0|0.0|0.0|0.0|0.0|0.0|0")
                            {
                                tempData.furnitureRotation = new Vector3(0.0, 0.0, 0.0);
                                tempData.furniturePosition = Helper.GetPositionInFrontOfPlayer(player);
                                tempData.furnitureOldPosition = tempData.furniturePosition;
                                tempData.furnitureOldRotation = tempData.furnitureRotation;
                                tempData.furnitureObject = NAPI.Object.CreateObject(Convert.ToInt32(furniture.hash), tempData.furniturePosition, tempData.furnitureRotation, 155, player.Dimension);
                                tempData.furnitureRotation = new Vector3(tempData.furnitureObject.Rotation.X, tempData.furnitureObject.Rotation.Y, tempData.furnitureObject.Rotation.Z);
                            }
                            else
                            {
                                tempData.doorPosition = furniture.position;
                                UpdateMöbelList(player, House.GetFurnitureForHouse(house.id, furniture.id), true);
                                tempData.furnitureOldPosition = furniture.objectHandle.Position;
                                tempData.furnitureOldRotation = furniture.objectHandle.Rotation;
                                tempData.furnitureRotation = furniture.objectHandle.Rotation;
                                tempData.furniturePosition = furniture.objectHandle.Position;
                                if (tempData.furnitureObject != null)
                                {
                                    tempData.furnitureObject.Delete();
                                    tempData.furnitureObject = null;
                                }
                                tempData.furnitureObject = NAPI.Object.CreateObject(Convert.ToInt32(furniture.hash), tempData.furniturePosition, tempData.furnitureRotation, 155, player.Dimension);
                                tempData.furnitureRotation = new Vector3(tempData.furnitureObject.Rotation.X, tempData.furnitureObject.Rotation.Y, tempData.furnitureObject.Rotation.Z);
                                if (furniture.objectHandle != null)
                                {
                                    furniture.objectHandle.Delete();
                                    furniture.objectHandle = null;
                                }
                            }
                        }
                        else
                        {
                            tempData.furnitureID = 0;
                            tempData.furnitureRotation = null;
                            tempData.furniturePosition = null;
                            if (tempData.furnitureObject != null)
                            {
                                tempData.furnitureObject.Delete();
                                tempData.furnitureObject = null;
                            }
                            tempData.furnitureNew = false;
                            tempData.editfurniture = false;
                            Helper.SendNotificationWithoutButton(player, "Ungültiges Möbelstück!", "error", "top-left");
                            if (flag == false)
                            {
                                Menu.OnStartMoebel(player);
                            }
                            else
                            {
                                player.TriggerEvent("Client:HideCursor");
                            }
                        }
                        return;
                    }
                    else if (setting == "showmenu")
                    {
                        if (tempData.editfurniture == true) return;
                        Commands.cmd_moebel(player);
                        return;
                    }
                    else if(setting == "editmoebel")
                    {
                        if (tempData.editfurniture == true) return;
                        furniture = GetClosestFurniture(player, house.id, 2.65f);
                        if (furniture != null)
                        {
                            OnFurnitureSettings(player, "move", furniture.id, true);
                        }
                        return;
                    }
                    else if (setting == "editmoebelm")
                    {
                        if (tempData.editfurniture == true) return;
                        furniture = GetClosestFurniture(player, house.id, 2.65f);
                        if (furniture != null)
                        {
                            player.TriggerEvent("Client:UpdateHud3");
                            OnFurnitureSettings(player, "move", furniture.id, true);
                        }
                        return;
                    }
                    else if(setting == "buy")
                    {
                        int limit = House.GetFurnitureLimit(player);
                        int count = Furniture.CountFurniture(house.id);
                        if(count >= limit)
                        {
                            Helper.SendNotificationWithoutButton(player, "Du hast das Möbellimit für dieses Haus erreicht!", "error", "top-left");
                            Menu.OnStartMoebel(player);
                            return;
                        }
                        tempData.editfurniture = true;
                        furnitureModel = GetFurnitureModelById(id);
                        player.TriggerEvent("Client:ShowInfobox", "Pfeiltasten -> Position/Rotation ändern", "Shift/ALT - Position/Rotation & X/Y switchen", "B -> Möbelstück kaufen und aufbauen", "Backspace -> Abbrechen", "Möbelaufbau", "Möbelstück: " + furnitureModel.name, "Preis: " + furnitureModel.price + "$", 0);
                        tempData.furnitureNew = true;
                        tempData.furnitureID = id;
                        tempData.furnitureRotation = new Vector3(0.0, 0.0, 0.0);
                        tempData.furniturePosition = Helper.GetPositionInFrontOfPlayer(player);
                        tempData.furnitureObject = NAPI.Object.CreateObject(Convert.ToInt32(furnitureModel.hash), tempData.furniturePosition, tempData.furnitureRotation, 155, player.Dimension);
                        tempData.furnitureRotation = new Vector3(tempData.furnitureObject.Rotation.X, tempData.furnitureObject.Rotation.Y, tempData.furnitureObject.Rotation.Z);
                        UpdateMöbelList(player, House.GetFurnitureForHouse(house.id), true);
                        return;
                    }
                    else if (setting == "changeSpeed")
                    {
                        if (tempData.editfurniture == false) return;
                        if (tempData.furnitureSpeed == 0.05f)
                        {
                            tempData.furnitureSpeed = 0.30f;
                        }
                        else if (tempData.furnitureSpeed == 0.3f)
                        {
                            tempData.furnitureSpeed = 0.45f;
                        }
                        else if (tempData.furnitureSpeed == 0.45f)
                        {
                            tempData.furnitureSpeed = 0.05f;
                        }
                        return;
                    }
                    else if(setting == "movingx")
                    {
                        if (tempData.editfurniture == false) return;
                        if(house.position.DistanceTo(tempData.furniturePosition) > 25.5 && player.Dimension != house.id)
                        {
                            Helper.SendNotificationWithoutButton(player, "Ungültige Möbelposition!", "error", "top-left");
                        }
                        tempData.furniturePosition = new Vector3(tempData.furniturePosition.X + tempData.furnitureSpeed, tempData.furniturePosition.Y, tempData.furniturePosition.Z);
                        tempData.furnitureObject.Position = tempData.furniturePosition;
                        return;
                    }
                    else if(setting == "movingx2")
                    {
                        if (house.position.DistanceTo(tempData.furniturePosition) > 25.5 && player.Dimension != house.id)
                        {
                            Helper.SendNotificationWithoutButton(player, "Ungültige Möbelposition!", "error", "top-left");
                        }
                        if (tempData.editfurniture == false) return;
                        tempData.furniturePosition = new Vector3(tempData.furniturePosition.X - tempData.furnitureSpeed, tempData.furniturePosition.Y, tempData.furniturePosition.Z);
                        tempData.furnitureObject.Position = tempData.furniturePosition;
                        return;
                    }
                    else if(setting == "rotatingx")
                    {
                        if (house.position.DistanceTo(tempData.furniturePosition) > 25.5 && player.Dimension != house.id)
                        {
                            Helper.SendNotificationWithoutButton(player, "Ungültige Möbelposition!", "error", "top-left");
                        }
                        if (tempData.editfurniture == false) return;
                        tempData.furnitureRotation = new Vector3(tempData.furnitureRotation.X + tempData.furnitureSpeed*6, tempData.furnitureRotation.Y, tempData.furnitureRotation.Z);
                        tempData.furnitureObject.Rotation = tempData.furnitureRotation;
                        return;
                    }
                    else if(setting == "rotatingx2")
                    {
                        if (house.position.DistanceTo(tempData.furniturePosition) > 25.5 && player.Dimension != house.id)
                        {
                            Helper.SendNotificationWithoutButton(player, "Ungültige Möbelposition!", "error", "top-left");
                        }
                        if (tempData.editfurniture == false) return;
                        tempData.furnitureRotation = new Vector3(tempData.furnitureRotation.X - tempData.furnitureSpeed*6, tempData.furnitureRotation.Y, tempData.furnitureRotation.Z);
                        tempData.furnitureObject.Rotation = tempData.furnitureRotation;
                        return;
                    }
                    else if (setting == "movingy")
                    {
                        if (house.position.DistanceTo(tempData.furniturePosition) > 25.5 && player.Dimension != house.id)
                        {
                            Helper.SendNotificationWithoutButton(player, "Ungültige Möbelposition!", "error", "top-left");
                        }
                        if (tempData.editfurniture == false) return;
                        tempData.furniturePosition = new Vector3(tempData.furniturePosition.X, tempData.furniturePosition.Y + tempData.furnitureSpeed, tempData.furniturePosition.Z);
                        tempData.furnitureObject.Position = tempData.furniturePosition;
                        return;
                    }
                    else if(setting == "movingy2")
                    {
                        if (house.position.DistanceTo(tempData.furniturePosition) > 25.5 && player.Dimension != house.id)
                        {
                            Helper.SendNotificationWithoutButton(player, "Ungültige Möbelposition!", "error", "top-left");
                        }
                        if (tempData.editfurniture == false) return;
                        tempData.furniturePosition = new Vector3(tempData.furniturePosition.X, tempData.furniturePosition.Y - tempData.furnitureSpeed, tempData.furniturePosition.Z);
                        tempData.furnitureObject.Position = tempData.furniturePosition;
                        return;
                    }
                    else if(setting == "rotatingy")
                    {
                        if (house.position.DistanceTo(tempData.furniturePosition) > 25.5 && player.Dimension != house.id)
                        {
                            Helper.SendNotificationWithoutButton(player, "Ungültige Möbelposition!", "error", "top-left");
                        }
                        if (tempData.editfurniture == false) return;
                        tempData.furnitureRotation = new Vector3(tempData.furnitureRotation.X, tempData.furnitureRotation.Y + tempData.furnitureSpeed*6, tempData.furnitureRotation.Z);
                        tempData.furnitureObject.Rotation = tempData.furnitureRotation;
                        return;
                    }
                    else if(setting == "rotatingy2")
                    {
                        if (house.position.DistanceTo(tempData.furniturePosition) > 25.5 && player.Dimension != house.id)
                        {
                            Helper.SendNotificationWithoutButton(player, "Ungültige Möbelposition!", "error", "top-left");
                        }
                        if (tempData.editfurniture == false) return;
                        tempData.furnitureRotation = new Vector3(tempData.furnitureRotation.X, tempData.furnitureRotation.Y - tempData.furnitureSpeed*6, tempData.furnitureRotation.Z);
                        tempData.furnitureObject.Rotation = tempData.furnitureRotation;
                        return;
                    }
                    else if(setting == "movingz")
                    {
                        if (house.position.DistanceTo(tempData.furniturePosition) > 25.5 && player.Dimension != house.id)
                        {
                            Helper.SendNotificationWithoutButton(player, "Ungültige Möbelposition!", "error", "top-left");
                        }
                        if (tempData.editfurniture == false) return;
                        tempData.furniturePosition = new Vector3(tempData.furniturePosition.X, tempData.furniturePosition.Y, tempData.furniturePosition.Z + tempData.furnitureSpeed);
                        tempData.furnitureObject.Position = tempData.furniturePosition;
                        return;
                    }
                    else if(setting == "movingz2")
                    {
                        if (house.position.DistanceTo(tempData.furniturePosition) > 25.5 && player.Dimension != house.id)
                        {
                            Helper.SendNotificationWithoutButton(player, "Ungültige Möbelposition!", "error", "top-left");
                        }
                        if (tempData.editfurniture == false) return;
                        tempData.furniturePosition = new Vector3(tempData.furniturePosition.X, tempData.furniturePosition.Y, tempData.furniturePosition.Z - tempData.furnitureSpeed);
                        tempData.furnitureObject.Position = tempData.furniturePosition;
                        return;
                    }
                    else if(setting == "rotatingz")
                    {
                        if (house.position.DistanceTo(tempData.furniturePosition) > 25.5 && player.Dimension != house.id)
                        {
                            Helper.SendNotificationWithoutButton(player, "Ungültige Möbelposition!", "error", "top-left");
                        }
                        if (tempData.editfurniture == false) return;
                        tempData.furnitureRotation = new Vector3(tempData.furnitureRotation.X, tempData.furnitureRotation.Y, tempData.furnitureRotation.Z + tempData.furnitureSpeed*6);
                        tempData.furnitureObject.Rotation = tempData.furnitureRotation;
                        return;
                    }
                    else if(setting == "rotatingz2")
                    {
                        if (house.position.DistanceTo(tempData.furniturePosition) > 25.5 && player.Dimension != house.id)
                        {
                            Helper.SendNotificationWithoutButton(player, "Ungültige Möbelposition!", "error", "top-left");
                        }
                        if (tempData.editfurniture == false) return;
                        tempData.furnitureRotation = new Vector3(tempData.furnitureRotation.X, tempData.furnitureRotation.Y, tempData.furnitureRotation.Z - tempData.furnitureSpeed*6);
                        tempData.furnitureObject.Rotation = tempData.furnitureRotation;
                        return;
                    }
                }
                else
                {
                    if (tempData.editfurniture == true)
                    {
                        FurnitureSetHouse furniture = null;
                        if (furniture != null)
                        {
                            if (tempData.furnitureNew == false)
                            {
                                furniture = GetFurnitureById(tempData.furnitureID, house.id);
                                furniture.objectHandle = NAPI.Object.CreateObject(Convert.ToInt32(furniture.hash), tempData.furnitureOldPosition, tempData.furnitureOldRotation, 255, player.Dimension);
                                if (tempData.furnitureObject != null)
                                {
                                    tempData.furnitureObject.Delete();
                                    tempData.furnitureObject = null;
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
                    Helper.SendNotificationWithoutButton(player, "Du bist nicht in oder in der Nähe von einem deiner Häuser!", "error", "top-left");
                }   
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", "[FurnitureSettings]: " + e.ToString());
            }
        }
    }
}
