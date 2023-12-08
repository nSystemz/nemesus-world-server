using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using GTANetworkAPI;
using MySql.Data.MySqlClient;
using NemesusWorld.Controllers;
using NemesusWorld.Models;
using NemesusWorld.Utils;

namespace NemesusWorld.Database
{
    class House : Script
    {
        public static List<House> houseList = new List<House>();
        public static List<HouseInteriorModel> houseListInteriors = new List<HouseInteriorModel>();
        public static List<FurnitureModelCategories> furnitureModelCategorieList = new List<FurnitureModelCategories>();
        public static List<FurnitureModel> furnitureModelList = new List<FurnitureModel>();
        public static List<FurnitureSetHouse> furnitureList = new List<FurnitureSetHouse>();

        public int id { get; set; }
        public Vector3 position { get; set; }
        public int dimension { get; set; }
        public int price { get; set; }
        public int interior { get; set; }
        public string owner { get; set; }
        public int status { get; set; }
        public int tenants { get; set; }
        public int rental { get; set; }
        public int locked { get; set; }
        public int noshield { get; set; }
        public string streetname { get; set; }
        public int blip { get; set; }
        public int housegroup { get; set; }
        public int stock { get; set; }
        public int stockprice { get; set; }
        public int classify { get; set; }
        public int elec { get; set; }
        public GTANetworkAPI.Marker markerHandle { get; set; }
        public GTANetworkAPI.TextLabel textHandle { get; set; }
        public GTANetworkAPI.Blip blipHandle { get; set; }

        public House()
        {
            id = 0;
            position = null;
            dimension = 0;
            price = 0;
            interior = 0;
            owner = "n/A";
            status = 0;
            tenants = 0;
            rental = 0;
            locked = 0;
            noshield = 0;
            streetname = "n/A";
            blip = 40;
            housegroup = -1;
            stock = 0;
            stockprice = 30;
            classify = 0;
            elec = 50;
            markerHandle = null;
            textHandle = null;
            blipHandle = null;
        }

        public static House GetClosestHouse(Player player, float distance = 1.5f)
        {
            try
            {
                House retHouse = null;
                if (houseList == null) return retHouse;
                foreach (House house in houseList)
                {
                    if (house != null && player.Position.DistanceTo(house.position) < distance)
                    {
                        retHouse = house;
                        distance = player.Position.DistanceTo(house.position);
                    }
                }
                return retHouse;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetClosestHouse]: " + e.ToString());
            }
            return null;
        }

        public static Vector3 GetHouseExitPoint(int interior)
        {
            try
            {
                Vector3 exit = null;
                foreach (HouseInteriorModel houseInterior in houseListInteriors)
                {
                    if (houseInterior.id == interior)
                    {
                        return new Vector3(houseInterior.posx, houseInterior.posy, houseInterior.posz);
                    }
                }
                return exit;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetHouseExitPoint]: " + e.ToString());
            }
            return null;
        }

        public static string GetInteriorIPL(int interior)
        {
            try
            {
                foreach (HouseInteriorModel houseInterior in houseListInteriors)
                {
                    if (houseInterior.id == interior)
                    {
                        return houseInterior.ipl;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetInteriorIPL]: " + e.ToString());
            }
            return null;
        }

        public static string GetHouseLabelText(House house)
        {
            string label = string.Empty;
            try
            {
                if (house.id == 2)
                {
                    return $"~w~Lager für Utensilien/Tuningteile";
                }

                string houseowner = "n/A";
                if (house.housegroup == -1)
                {
                    if (house.owner != "n/A")
                    {
                        houseowner = house.owner.Split(" ")[1];
                    }
                }
                else
                {
                    Groups group = GroupsController.GetGroupById(house.housegroup);
                    if (group != null)
                    {
                        houseowner = group.name;
                    }
                }

                switch (house.status)
                {
                    case 1:
                        if (house.interior > 0)
                        {
                            label = $"~w~{house.streetname}\nHausnummer: {house.id}\nKlingelschild: {houseowner}\n~y~Benutze Taste 'F' um dieses zu betreten!";
                        }
                        else
                        {
                            label = $"~w~{house.streetname}\nHausnummer: {house.id}\nKlingelschild: {houseowner}";
                        }
                        break;
                    case 2:
                        if (house.interior > 0)
                        {
                            label = $"~w~{house.streetname}\nHausnummer: {house.id}\n~y~Benutze Taste 'F' um dieses zu betreten!";
                        }
                        else
                        {
                            label = $"~w~{house.streetname}\nHausnummer: {house.id}";
                        }
                        break;
                    case 3:
                        if (house.interior > 0)
                        {
                            label = $"~w~{house.streetname}\nHausnummer: {house.id}\nKlingelschild: {houseowner}\nDas Haus hat noch freie Mietplätze!\nMietpreis: {house.rental}$\n~y~Drücke F um mit dem Haus zu interagieren!";
                        }
                        else
                        {
                            label = $"~w~{house.streetname}\nHausnummer: {house.id}\nKlingelschild: {houseowner}\nDas Haus hat noch freie Mietplätze!\nMietpreis: {house.rental}$";
                        }
                        break;
                    case 4:
                        label = $"~w~{house.streetname}\nHausnummer: {house.id}\nDas Haus hat noch freie Mietplätze!\nMietpreis: {house.rental}$\n~y~Drücke F um mit dem Haus zu interagieren!";
                        break;
                    case 5:
                        label = $"~w~{house.streetname}\nHausnummer: {house.id}\nDieses Haus steht nicht zum Verkauf!";
                        break;
                    case 0:
                        label = $"~w~{house.streetname}\nHausnummer: {house.id}\nSteht zum Verkauf für {house.price}$!\n~y~Drücke F um mit dem Haus zu interagieren!";
                        break;
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", "[GetHouseLabelText]: " + e.ToString());
            }
            return label;
        }

        public static void SetHouseHandle(House house)
        {
            try
            {
                bool blipStatus = false;
                if (house != null)
                {
                    if (house.textHandle != null)
                    {
                        house.textHandle.Delete();
                        house.textHandle = null;
                    }
                    if (house.blipHandle != null)
                    {
                        house.blipHandle.Delete();
                        house.blipHandle = null;
                    }
                    if (house.markerHandle != null)
                    {
                        house.markerHandle.Delete();
                        house.markerHandle = null;
                    }
                    string houseLabelText = GetHouseLabelText(house);
                    house.textHandle = NAPI.TextLabel.CreateTextLabel(houseLabelText, new Vector3(house.position.X, house.position.Y, house.position.Z + 0.8), 5.0f, 0.75f, 4, new Color(255, 255, 255), false, (uint)house.dimension);
                    if (house.housegroup == -1)
                    {
                        if (house.status == 0)
                        {
                            blipStatus = false;
                            house.blipHandle = NAPI.Blip.CreateBlip(house.blip, house.position, 0.4f, 2);
                            house.markerHandle = NAPI.Marker.CreateMarker(1, new Vector3(house.position.X, house.position.Y, house.position.Z - 1.1), house.position, new Vector3(), 1.0f, new Color(38, 230, 0), false, (uint)house.dimension);
                        }
                        else
                        {
                            blipStatus = true;
                            house.markerHandle = NAPI.Marker.CreateMarker(1, new Vector3(house.position.X, house.position.Y, house.position.Z - 1.1), house.position, new Vector3(), 1.0f, new Color(255, 255, 255), false, (uint)house.dimension);
                            house.blipHandle = NAPI.Blip.CreateBlip(house.blip, house.position, 0.4f, 1);
                        }
                        NAPI.Blip.SetBlipScale(house.blipHandle, 0.5f);
                        if (blipStatus == false)
                        {
                            NAPI.Blip.SetBlipName(house.blipHandle, "Freies Haus");
                        }
                        else
                        {
                            NAPI.Blip.SetBlipName(house.blipHandle, "Belegtes Haus");
                        }
                        NAPI.Blip.SetBlipShortRange(house.blipHandle, true);
                    }
                    else
                    {
                        Groups group = GroupsController.GetGroupById(house.housegroup);
                        if (group != null)
                        {
                            if (house.id != 2)
                            {
                                house.blipHandle = NAPI.Blip.CreateBlip(house.blip, house.position, 0.7f, 39);
                                house.markerHandle = NAPI.Marker.CreateMarker(1, new Vector3(house.position.X, house.position.Y, house.position.Z - 1.1), house.position, new Vector3(), 1.0f, new Color(158, 158, 158), false, (uint)house.dimension);
                                NAPI.Blip.SetBlipScale(house.blipHandle, 0.5f);
                                NAPI.Blip.SetBlipName(house.blipHandle, group.name); NAPI.Blip.SetBlipShortRange(house.blipHandle, true);
                            }
                            else
                            {
                                house.blipHandle = null;
                                house.markerHandle = NAPI.Marker.CreateMarker(1, new Vector3(house.position.X, house.position.Y, house.position.Z - 1.1), house.position, new Vector3(), 1.0f, new Color(158, 158, 158), false, (uint)house.dimension);
                            }
                        }
                        else
                        {
                            house.housegroup = -1;
                            house.blip = 40;
                            house.stock = 0;
                            house.stockprice = 30;
                            SetHouseHandle(house);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", "[SetHouseHandle]: " + e.ToString());
            }
        }

        public static void GetAllHouses()
        {
            try
            {
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                foreach (HouseModel houseModel in db.Fetch<HouseModel>("SELECT * FROM houses"))
                {
                    House house = new House();
                    house.id = houseModel.id;
                    house.position = new Vector3(houseModel.posx, houseModel.posy, houseModel.posz);
                    house.price = houseModel.price;
                    house.interior = houseModel.interior;
                    house.owner = houseModel.owner;
                    house.status = houseModel.status;
                    house.tenants = houseModel.tenants;
                    house.rental = houseModel.rental;
                    house.locked = houseModel.locked;
                    house.noshield = houseModel.noshield;
                    house.streetname = houseModel.streetname;
                    house.blip = houseModel.blip;
                    house.housegroup = houseModel.housegroup;
                    house.stock = houseModel.stock;
                    house.stockprice = houseModel.stockprice;
                    house.classify = houseModel.classify;
                    house.elec = houseModel.elec;
                    SetHouseHandle(house);
                    houseList.Add(house);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetAllHouses]: " + e.ToString());
            }
        }

        public static void HouseInActiveCheck()
        {
            try
            {
                int id;
                int lastonline;
                bool inactive;
                foreach (House house in houseList)
                {
                    id = -1;
                    lastonline = 0;
                    inactive = false;
                    if (house != null && house.owner != "n/A" && house.price > 0)
                    {
                        MySqlCommand command = General.Connection.CreateCommand();
                        command.CommandText = "SELECT id,lastonline FROM characters WHERE name = @name LIMIT 1";
                        command.Parameters.AddWithValue("@name", house.owner);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                LoadCharactersModel loadCharacter = new LoadCharactersModel();
                                id = reader.GetInt32("id");
                                lastonline = reader.GetInt32("lastonline");
                            }
                            reader.Close();
                        }

                        if (id == -1) continue;

                        command = General.Connection.CreateCommand();
                        command.CommandText = "SELECT id FROM inactiv WHERE id = @id LIMIT 1";
                        command.Parameters.AddWithValue("@id", id);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                inactive = true;
                            }
                            reader.Close();
                        }

                        if (inactive == true) continue;

                        if (lastonline > 0)
                        {
                            if ((lastonline + (90 * 86400)) < Helper.UnixTimestamp())
                            {
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

                                command.CommandText = $"UPDATE vehicles SET garage = 'n/A' WHERE garage = @garage";
                                command.Parameters.AddWithValue("@garage", house.id);
                                command.ExecuteNonQuery();

                                int money = Furniture.SellAllFurniture(house.id);
                                House.SaveHouse(house);
                                Helper.CreateAdminLog($"hauslog", $"Das Haus {house.id} wurde durch den Inaktivitätscheck verkauft!");
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[HouseInActiveCheck]: " + e.ToString());
            }
        }

        public static void GetAllInteriors()
        {
            try
            {
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                foreach (HouseInteriorModel houseInterior in db.Fetch<HouseInteriorModel>("SELECT * FROM houseinteriors"))
                {
                    houseListInteriors.Add(houseInterior);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetAllInteriors]: " + e.ToString());
            }
        }

        public static void GetAllFurnitureModelCategories()
        {
            try
            {
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                foreach (FurnitureModelCategories furnitureModelCategorie in db.Fetch<FurnitureModelCategories>("SELECT * FROM furniturecategories"))
                {
                    furnitureModelCategorieList.Add(furnitureModelCategorie);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetAllFurnitureModelCategories]: " + e.ToString());
            }
        }

        public static void GetAllFurnitureModels()
        {
            try
            {
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                foreach (FurnitureModel furnitureModel in db.Fetch<FurnitureModel>("SELECT * FROM furniture"))
                {
                    furnitureModelList.Add(furnitureModel);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetAllFurnitureModels]: " + e.ToString());
            }
        }

        public static void GetAllFurniture()
        {
            try
            {
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                foreach (FurnitureHouse furnitureModel in db.Fetch<FurnitureHouse>("SELECT * FROM furniturehouse"))
                {
                    FurnitureSetHouse furniture = new FurnitureSetHouse();
                    furniture.id = furnitureModel.id;
                    furniture.house = furnitureModel.house;
                    furniture.extra = furnitureModel.extra;
                    furniture.name = furnitureModel.name;
                    furniture.hash = furnitureModel.hash;
                    furniture.position = furnitureModel.position;
                    furniture.props = furnitureModel.props;
                    furniture.price = furnitureModel.price;

                    if (furniture.position != "0.0|0.0|0.0|0.0|0.0|0.0|0")
                    {
                        string[] furPosition = new string[7];
                        furPosition = furniture.position.Split("|");
                        furniture.objectHandle = NAPI.Object.CreateObject(Convert.ToInt32(furniture.hash), new Vector3(float.Parse(furPosition[0]), float.Parse(furPosition[1]), float.Parse(furPosition[2])), new Vector3(float.Parse(furPosition[3]), float.Parse(furPosition[4]), float.Parse(furPosition[5])), 255, uint.Parse(furPosition[6]));
                        if (furniture.extra == 5)
                        {
                            Doors door = new Doors();
                            door.id = DoorsController.GetFreeDoorsID();
                            door.hash = furniture.hash;
                            door.posx = float.Parse(furPosition[0]);
                            door.posy = float.Parse(furPosition[1]);
                            door.posz = float.Parse(furPosition[2]);
                            door.dimension = (int)float.Parse(furPosition[6]);
                            door.toogle = furniture.props.ToLower() == "false" ? false : true;
                            furniture.props = "" + door.toogle;
                            door.save = false;

                            DoorsController.AddDoor(door, false);
                        }
                    }

                    furnitureList.Add(furniture);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetAllFurniture]: " + e.ToString());
            }
        }

        public static List<FurnitureFrontend> GetFurnitureForHouse(int houseid, int exception = -1)
        {
            List<FurnitureFrontend> ownFurnitureList = new List<FurnitureFrontend>();
            try
            {
                foreach (FurnitureSetHouse furniture in furnitureList)
                {
                    if (furniture != null && furniture.house == houseid && furniture.id != exception)
                    {
                        FurnitureFrontend furnitureFrontend = new FurnitureFrontend();
                        furnitureFrontend.id = furniture.id;
                        furnitureFrontend.name = furniture.name;
                        furnitureFrontend.position = furniture.position;
                        furnitureFrontend.price = furniture.price;
                        ownFurnitureList.Add(furnitureFrontend);
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetFurnitureForHouse]: " + e.ToString());
            }
            return ownFurnitureList;
        }

        public static void SaveFurniture(FurnitureSetHouse furnitureModel)
        {
            try
            {
                if (furnitureModel == null) return;

                FurnitureHouse furniture = new FurnitureHouse();
                furniture.id = furnitureModel.id;
                furniture.house = furnitureModel.house;
                furniture.extra = furnitureModel.extra;
                furniture.name = furnitureModel.name;
                furniture.hash = furnitureModel.hash;
                furniture.position = furnitureModel.position;
                furniture.props = furnitureModel.props;
                furniture.price = furnitureModel.price;

                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                db.Save(furniture);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[SaveFurniture]: " + e.ToString());
            }
        }

        public static void DeleteFurniture(FurnitureSetHouse furnitureModel)
        {
            try
            {
                if (furnitureModel == null) return;

                FurnitureHouse furniture = new FurnitureHouse();
                furniture.id = furnitureModel.id;
                furniture.house = furnitureModel.house;
                furniture.extra = furnitureModel.extra;
                furniture.name = furnitureModel.name;
                furniture.hash = furnitureModel.hash;
                furniture.position = furnitureModel.position;
                furniture.props = furnitureModel.props;
                furniture.price = furnitureModel.price;

                if (furniture.extra == 1 || furniture.extra == 2 || furniture.extra == 3)
                {
                    foreach (ItemsGlobal globalitem in ItemsController.itemListGlobal)
                    {
                        if (globalitem != null && globalitem.owneridentifier == "Furniture-" + furniture.hash)
                        {
                            ItemsController.itemListGlobal.Remove(globalitem);
                        }
                    }
                }

                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                db.Delete(furniture);

                furnitureModel = null;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[DeleteFurniture]: " + e.ToString());
            }
        }

        public static int AddFurniture(FurnitureSetHouse furniture)
        {
            try
            {
                MySqlCommand command = General.Connection.CreateCommand();

                command.CommandText = "INSERT INTO furniturehouse (house, extra, name, hash, position, props, price) VALUES (@house, @extra, @name, @hash, @position, @props, @price)";
                command.Parameters.AddWithValue("@house", furniture.house);
                command.Parameters.AddWithValue("@extra", furniture.extra);
                command.Parameters.AddWithValue("@name", furniture.name);
                command.Parameters.AddWithValue("@hash", furniture.hash);
                command.Parameters.AddWithValue("@position", furniture.position);
                command.Parameters.AddWithValue("@props", furniture.props);
                command.Parameters.AddWithValue("@price", furniture.price);

                command.ExecuteNonQuery();
                furniture.id = (int)command.LastInsertedId;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", "[AddFurniture]: " + e.ToString());
            }
            return -1;
        }

        public static int GetFurnitureLimit(Player player)
        {
            int limit = 75;
            try
            {
                Account account = Helper.GetAccountData(player);
                if (account == null) return limit;
                if (account.premium == 1)
                {
                    limit += 125;
                }
                else if (account.premium == 2)
                {
                    limit += 175;
                }
                else if (account.premium == 3)
                {
                    limit += 215;
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", "[GetFurnitureLimit]: " + e.ToString());
            }
            return limit;
        }

        public static void SaveHouse(House house)
        {
            try
            {
                if (house == null) return;

                HouseModel houseModel = new HouseModel();
                houseModel.id = house.id;
                houseModel.posx = house.position.X;
                houseModel.posy = house.position.Y;
                houseModel.posz = house.position.Z;
                houseModel.price = house.price;
                houseModel.interior = house.interior;
                houseModel.owner = house.owner;
                houseModel.status = house.status;
                houseModel.rental = house.rental;
                houseModel.locked = house.locked;
                houseModel.noshield = house.noshield;
                houseModel.streetname = house.streetname;
                houseModel.blip = house.blip;
                houseModel.housegroup = house.housegroup;
                houseModel.stock = house.stock;
                houseModel.stockprice = house.stockprice;
                houseModel.classify = house.classify;
                houseModel.tenants = house.tenants;
                houseModel.elec = house.elec;

                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                db.Save(houseModel);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[SaveHouse]: " + e.ToString());
                if (General.Connection.State != ConnectionState.Open)
                {
                    Helper.ConsoleLog("error", "[MYSQL]: Verbindung wurde unerwartet beendet!");
                    General.InitConnection();
                }
            }
        }

        public static int AddHouse(House house)
        {
            int houseId = -1;

            try
            {
                MySqlCommand command = General.Connection.CreateCommand();

                command.CommandText = "INSERT INTO houses (interior, posX, posY, posZ, dimension, price, status) VALUES (@interior, @posX, @posY, @posZ, @dimension, @price, @status)";
                command.Parameters.AddWithValue("@interior", house.interior);
                command.Parameters.AddWithValue("@posX", house.position.X);
                command.Parameters.AddWithValue("@posY", house.position.Y);
                command.Parameters.AddWithValue("@posZ", house.position.Z);
                command.Parameters.AddWithValue("@dimension", house.dimension);
                command.Parameters.AddWithValue("@price", house.price);
                command.Parameters.AddWithValue("@status", house.status);

                command.ExecuteNonQuery();
                houseId = (int)command.LastInsertedId;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", "[AddHouse]: " + e.ToString());
            }
            return houseId;
        }

        public static void KickAllTenants(int hausid, bool deleteall = false)
        {
            try
            {
                MySqlCommand command = General.Connection.CreateCommand();

                command.CommandText = $"UPDATE characters SET items = REPLACE(items, 'Miethausnummer: " + hausid + "', 'n/A') WHERE items LIKE @hausid2";
                command.Parameters.AddWithValue("@hausid", hausid);
                command.Parameters.AddWithValue("@hausid2", "%Miethausnummer: " + hausid + "%");

                command.ExecuteNonQuery();

                foreach (Player p in NAPI.Pools.GetAllPlayers())
                {
                    if (p != null && p.GetOwnSharedData<bool>("Player:Spawned") == true)
                    {
                        Character character = Helper.GetCharacterData(p);
                        if (character != null)
                        {
                            if (deleteall == false)
                            {
                                ItemsController.DeleteItemWithProp(p, "Miethausnummer: " + hausid);
                            }
                            else
                            {
                                ItemsController.DeleteItemWithProp(p, "Miethausnummer: " + hausid);
                                ItemsController.DeleteItemWithProp(p, "Hausnummer: " + hausid);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", "[KickAllTenants]: " + e.ToString());
            }
        }

        public static void KickTenant(int hausid, int id)
        {
            try
            {
                string hausid2 = "Miethausnummer: " + hausid;
                MySqlCommand command = General.Connection.CreateCommand();

                command.CommandText = "UPDATE characters SET items = REPLACE(items, 'Miethausnummer: " + hausid + "','n/A') WHERE items LIKE '%" + hausid2 + "%' AND id = @id";
                command.Parameters.AddWithValue("@hausid1", hausid);
                command.Parameters.AddWithValue("@id", id);

                command.ExecuteNonQuery();

                foreach (Player p in NAPI.Pools.GetAllPlayers())
                {
                    if (p != null && p.GetOwnSharedData<bool>("Player:Spawned") == true)
                    {
                        Character character = Helper.GetCharacterData(p);
                        if (character != null && character.id == id)
                        {
                            ItemsController.DeleteItemWithProp(p, "Miethausnummer: " + hausid);
                            return;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", "[KickTenant]: " + e.ToString());
            }
        }

        public static void NewHouseKey(int hausid, string myname)
        {
            try
            {
                MySqlCommand command = General.Connection.CreateCommand();

                command.CommandText = "UPDATE characters SET items = REPLACE(items, 'Hausnummer: " + hausid + "', 'n/A') WHERE items LIKE @hausid2 AND name != @name";
                command.Parameters.AddWithValue("@name", myname);
                command.Parameters.AddWithValue("@hausid2", "%Hausnummer: " + hausid + "%");

                command.ExecuteNonQuery();

                foreach (Player p in NAPI.Pools.GetAllPlayers())
                {
                    if (p != null && p.GetOwnSharedData<bool>("Player:Spawned") == true)
                    {
                        Character character = Helper.GetCharacterData(p);
                        if (character != null && character.name != myname)
                        {
                            ItemsController.DeleteItemWithProp(p, "Hausnummer: " + hausid);
                            ItemsController.UpdateInventory(p);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", "[NewHouseKey]: " + e.ToString());
            }
        }

        public static int GetRandomHouseInterior(int housesize)
        {
            try
            {
                HouseInteriorModel houseinteriorModelTemp = new HouseInteriorModel();
                var random = new Random();
                List<HouseInteriorModel> houseInteriorModelTemp = new List<HouseInteriorModel>();
                foreach (HouseInteriorModel houseInteriorModel in houseListInteriors)
                {
                    if (houseInteriorModel != null && houseInteriorModel.classify == housesize)
                    {
                        houseInteriorModelTemp.Add(houseInteriorModel);
                    }
                }
                int index = random.Next(houseInteriorModelTemp.Count);
                return houseInteriorModelTemp[index].id;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", "[GetRandomHouseInterior]: " + e.ToString());
            }
            return 1;
        }

        public static List<HouseInteriorModel> GetHouseInterior(int houseinteriorsize, int id, House house)
        {
            try
            {
                bool tuningfound = false;
                bool mechafound = false;
                HouseInteriorModel houseinteriorModelTemp = new HouseInteriorModel();
                List<HouseInteriorModel> HouseInteriorModelTemp = new List<HouseInteriorModel>();
                Groups group = GroupsController.GetGroupById(house.housegroup);
                if (group != null)
                {
                    string[] licArray = new string[9];
                    licArray = group.licenses.Split("|");
                    if (Convert.ToInt32(licArray[1]) == 1)
                    {
                        tuningfound = true;
                    }
                    if (Convert.ToInt32(licArray[2]) == 1)
                    {
                        mechafound = true;
                    }
                }
                foreach (HouseInteriorModel houseInteriorModel in houseListInteriors)
                {
                    if ((houseInteriorModel.classify == houseinteriorsize || houseInteriorModel.classify == house.classify || houseInteriorModel.classify == (id + 100)) && houseInteriorModel.classify != 4)
                    {
                        HouseInteriorModelTemp.Add(houseInteriorModel);
                    }
                    if ((tuningfound == true || mechafound == true) && houseInteriorModel.classify == 4)
                    {
                        HouseInteriorModelTemp.Add(houseInteriorModel);
                    }
                }
                return HouseInteriorModelTemp;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", "[GetHouseInterior]: " + e.ToString());
            }
            return null;
        }

        public static int GetInteriorClassify(int interior)
        {
            int ret = 0;
            try
            {
                foreach (HouseInteriorModel houseInteriorModel in houseListInteriors)
                {
                    if (houseInteriorModel.id == interior)
                    {
                        ret = houseInteriorModel.classify;
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", "[GetInteriorClassify]: " + e.ToString());
            }
            return ret;
        }

        public static House GetHouseById(int id)
        {
            House houseTemp = null;
            try
            {
                foreach (House house in houseList)
                {
                    if (house.id == id)
                    {
                        houseTemp = house;
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", "[GetHouseById]: " + e.ToString());
            }
            return houseTemp;
        }

        public static House GetHouseByGroupId(int id)
        {
            House houseTemp = null;
            try
            {
                if (id != -1)
                {
                    foreach (House house in houseList)
                    {
                        if (house.housegroup == id)
                        {
                            houseTemp = house;
                            break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", "[GetHouseByGroupId]: " + e.ToString());
            }
            return houseTemp;
        }

        public static bool HasPlayerHouseKey(Player player, int houseid)
        {
            try
            {
                if (Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Administrator)) return true;
                TempData tempData = Helper.GetCharacterTempData(player);
                if (tempData != null)
                {
                    foreach (Items item in tempData.itemlist)
                    {
                        if (item != null && (item.description == "Hausschlüssel" && item.props.Contains("Hausnummer: " + houseid)) || item.description == "Mietschlüssel" && item.props.Contains("Miethausnummer: " + houseid))
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", "[HasPlayerHouseKey]: " + e.ToString());
            }
            return false;
        }

        public static int HasPlayerHouseKey2(Player player, int houseid)
        {
            try
            {
                TempData tempData = Helper.GetCharacterTempData(player);
                if (tempData != null)
                {
                    foreach (Items item in tempData.itemlist)
                    {
                        if (item != null && (Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Administrator) || (item.description == "Hausschlüssel" && item.props.Contains("Hausnummer: " + houseid))))
                        {
                            return 1;
                        }
                        if (item != null && item.description == "Mietschlüssel" && item.props.Contains("Miethausnummer: " + houseid))
                        {
                            return 2;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", "[HasPlayerHouseKey]: " + e.ToString());
            }
            return 0;
        }

        [RemoteEvent("Server:SetHouseStreet")]
        public static void OnSetHouseStreet(Player player, int houseid, string street)
        {
            try
            {
                House house = House.GetHouseById(houseid);
                if (house != null)
                {
                    house.streetname = street;
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", "[OnSetHouseStreet]: " + e.ToString());
            }
        }

        //HouseSettings
        [RemoteEvent("Server:HouseSettings")]
        public static void OnHouseSettings(Player player, string setting, string value)
        {
            try
            {
                House house = null;
                float dist = 1.5f;
                int number = 0;
                Character character = Helper.GetCharacterData(player);
                TempData tempData = Helper.GetCharacterTempData(player);
                Business bizz = Business.GetClosestBusiness(player, 55.5f);
                if (tempData.lasthouse == 0)
                {
                    if (character.inhouse == -1)
                    {
                        if (setting.Contains("garage"))
                        {
                            dist = 10.15f;
                        }
                        house = House.GetClosestHouse(player, dist);
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
                if (setting == "endrent" || setting.Contains("garage") || house != null || house.owner == character.name || bizz != null)
                {
                    List<HouseInteriorModel> tempList = null;
                    if (house != null)
                    {
                        tempList = House.GetHouseInterior(House.GetInteriorClassify(house.interior), character.id, house);
                    }
                    switch (setting.ToLower())
                    {
                        case "entergarage":
                            {
                                if (player.Dimension == 0)
                                {
                                    Cars getCar = null;
                                    if (!Helper.IsInRangeOfPoint(player.Position, new Vector3(408.9756, -1622.715, 29.291948), 6.75f))
                                    {
                                        foreach (Cars car in Cars.carList)
                                        {
                                            if (car.vehicleHandle != null && car.vehicleHandle == player.Vehicle && car.vehicleData != null)
                                            {
                                                getCar = car;
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Vehicle veh = Helper.GetClosestVehicle(player, 10.75f);
                                        foreach (Cars car in Cars.carList)
                                        {
                                            if (car.vehicleHandle != null && car.vehicleHandle == veh && car.vehicleData != null)
                                            {
                                                getCar = car;
                                                break;
                                            }
                                        }
                                    }
                                    if (getCar == null || getCar.vehicleData == null)
                                    {
                                        Helper.SendNotificationWithoutButton(player, "Dieses Fahrzeug kann nicht eingeparkt werden/Ungültiges Fahrzeug!", "error", "center");
                                        return;
                                    }
                                    if (!Helper.IsInRangeOfPoint(player.Position, new Vector3(408.9756, -1622.715, 29.291948), 6.75f))
                                    {
                                        if (player.VehicleSeat != (int)VehicleSeat.Driver)
                                        {
                                            Helper.SendNotificationWithoutButton(player, "Du kannst das Fahrzeug nicht einparken!", "error", "center");
                                            return;
                                        }
                                        if (!Helper.GetVehicleRights(player, getCar.vehicleHandle))
                                        {
                                            Helper.SendNotificationWithoutButton(player, "Du kannst das Fahrzeug nicht einparken!", "error", "center");
                                            return;
                                        }
                                    }
                                    if (bizz != null)
                                    {
                                        if ((bizz.id == 33 || bizz.id == 36) && (player.Vehicle.Class == 14 || player.Vehicle.Class == 15 || player.Vehicle.Class == 16 || player.Vehicle.Class == 17 || player.Vehicle.Class == 18 || player.Vehicle.Class == 21 || player.Vehicle.Class == 22))
                                        {
                                            return;
                                        }
                                        if (bizz.id == 34 && player.Vehicle.Class != 14)
                                        {
                                            return;
                                        }
                                        if (bizz.id == 35 && player.Vehicle.Class != 15 && player.Vehicle.Class != 16)
                                        {
                                            return;
                                        }
                                        if (bizz != null && (bizz.id == 33 || bizz.id == 34 || bizz.id == 35 || bizz.id == 36))
                                        {
                                            if (bizz.products < 1)
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Wir können keine weiteren Fahrzeuge mehr aufnehmen!", "error", "center");
                                                return;
                                            }
                                        }
                                    }
                                    if (house != null)
                                    {
                                        if (house.owner == "n/A")
                                        {
                                            Helper.SendNotificationWithoutButton(player, "Du kannst dein Fahrzeug hier nicht einparken!", "error", "center");
                                            return;
                                        }
                                        if (!Cars.CanCarInGarage("house-" + house.id, house.classify))
                                        {
                                            Helper.SendNotificationWithoutButton(player, "Es passen keine weiteren Fahrzeuge mehr in die Garage!", "error", "center");
                                            return;
                                        }
                                    }
                                    NAPI.Task.Run(() =>
                                    {
                                        if (Helper.IsInRangeOfPoint(player.Position, new Vector3(445.048, -972.2439, 25.788462), 6.75f) || (Helper.IsInRangeOfPoint(player.Position, new Vector3(463.60544, -982.2816, 43.692), 37.75f) && player.Position.Z >= 38))
                                        {
                                            if(getCar.vehicleData.owner != "faction-"+character.faction)
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Dieses Fahrzeug kann hier nicht eingeparkt werden!", "error", "center");
                                                return;
                                            }
                                            if (Helper.IsInRangeOfPoint(player.Position, new Vector3(463.60544, -982.2816, 43.692), 37.75f) && player.Position.Z >= 38 && getCar.vehicleData.vehiclename.ToLower() != "polmav" && getCar.vehicleData.vehiclename.ToLower() != "buzzard2" && getCar.vehicleData.vehiclename.ToLower() != "swift")
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Du kannst hier nur Helikopter einparken!", "error", "center");
                                                return;
                                            }
                                            getCar.vehicleData.garage = "faction-" + character.faction;
                                        }
                                        else if (Helper.IsInRangeOfPoint(player.Position, new Vector3(608.4999, -2.2341008, 70.62814), 7.75f) || (Helper.IsInRangeOfPoint(player.Position, new Vector3(569.6448, 10.3835535, 103.22986), 37.75f) && player.Position.Z >= 102))
                                        {
                                            if (getCar.vehicleData.owner != "faction-" + character.faction)
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Dieses Fahrzeug kann hier nicht eingeparkt werden!", "error", "center");
                                                return;
                                            }
                                            if (Helper.IsInRangeOfPoint(player.Position, new Vector3(569.6448, 10.3835535, 103.22986), 37.75f) && player.Position.Z >= 102 && getCar.vehicleData.vehiclename.ToLower() != "polmav" && getCar.vehicleData.vehiclename.ToLower() != "buzzard2" && getCar.vehicleData.vehiclename.ToLower() != "swift")
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Du kannst hier nur Helikopter einparken!", "error", "center");
                                                return;
                                            }
                                            getCar.vehicleData.garage = "faction-" + character.faction;
                                        }
                                        else if (Helper.IsInRangeOfPoint(player.Position, new Vector3(-676.8967, 336.86328, 78.11836), 6.75f) || (Helper.IsInRangeOfPoint(player.Position, new Vector3(-651.331, 328.43048, 140.14816), 37.75f) && player.Position.Z >= 139))
                                        {
                                            if (getCar.vehicleData.owner != "faction-" + character.faction)
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Dieses Fahrzeug kann hier nicht eingeparkt werden!", "error", "center");
                                                return;
                                            }
                                            if (Helper.IsInRangeOfPoint(player.Position, new Vector3(-651.331, 328.43048, 140.14816), 37.75f) && player.Position.Z >= 139 && getCar.vehicleData.vehiclename.ToLower() != "swift" && getCar.vehicleData.vehiclename.ToLower() != "seasparrow" && getCar.vehicleData.vehiclename.ToLower() != "frogger")
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Du kannst hier nur Helikopter einparken!", "error", "center");
                                                return;
                                            }
                                            getCar.vehicleData.garage = "faction-" + character.faction;
                                        }
                                        else if (Helper.IsInRangeOfPoint(player.Position, new Vector3(-354.90842, -166.38257, 39.015373), 6.75f) || (Helper.IsInRangeOfPoint(player.Position, new Vector3(-333.11682, -146.52608, 60.445873), 37.75f) && player.Position.Z >= 59))
                                        {
                                            if (getCar.vehicleData.owner != "faction-" + character.faction)
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Dieses Fahrzeug kann hier nicht eingeparkt werden!", "error", "center");
                                                return;
                                            }
                                            if (Helper.IsInRangeOfPoint(player.Position, new Vector3(-333.11682, -146.52608, 60.445873), 37.75f) && player.Position.Z >= 59 && getCar.vehicleData.vehiclename.ToLower() != "cargobob2" && getCar.vehicleData.vehiclename.ToLower() != "frogger")
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Du kannst hier nur Helikopter einparken!", "error", "center");
                                                return;
                                            }
                                            getCar.vehicleData.garage = "faction-" + character.faction;
                                        }
                                        else if (Helper.IsInRangeOfPoint(player.Position, new Vector3(408.9756, -1622.715, 29.291948), 6.75f))
                                        {
                                            if(character.faction != 3 || character.factionduty == false)
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Du kannst hier nichts einparken!", "error", "center");
                                                return;
                                            }
                                            if (getCar.vehicleData.owner.Contains("faction-"))
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Dieses Fahrzeug kann hier nicht eingeparkt werden!", "error", "center");
                                                return;
                                            }
                                            getCar.vehicleData.garage = "towed-1";
                                            getCar.vehicleData.towed = Helper.adminSettings.towedcash;
                                            string carname = getCar.vehicleData.ownname != "n/A" ? getCar.vehicleData.ownname : getCar.vehicleData.vehiclename;
                                            Helper.CreateFactionLog(character.faction, $"{character.name} hat das Fahrzeug {carname}({getCar.vehicleData.plate}) im Verwahrplatz geparkt!");
                                        }
                                        else if (Helper.IsInRangeOfPoint(player.Position, new Vector3(-506.12866, -199.77043, 34.215195), 18.65f))
                                        {
                                            if (getCar.vehicleData.owner != "faction-" + character.faction)
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Dieses Fahrzeug kann hier nicht eingeparkt werden!", "error", "center");
                                                return;
                                            }
                                            getCar.vehicleData.garage = "faction-" + character.faction;
                                        }
                                        else
                                        {
                                            if (bizz != null || house != null)
                                            {
                                                if (bizz != null && (bizz.id == 33 || bizz.id == 34 || bizz.id == 35 || bizz.id == 36))
                                                {
                                                    getCar.vehicleData.garage = "bizz-" + bizz.id;
                                                }
                                                else
                                                {
                                                    getCar.vehicleData.garage = "house-" + house.id;
                                                }
                                            }
                                            else
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Das Fahrzeug konnte nicht eingeparkt werden!", "error", "center");
                                                return;
                                            }
                                        }

                                        foreach (Player p in NAPI.Pools.GetAllPlayers())
                                        {
                                            if (player.Vehicle == p.Vehicle)
                                            {
                                                p.WarpOutOfVehicle();
                                            }
                                        }

                                        Helper.SetVehicleEngine(getCar.vehicleHandle, false);
                                        DealerShipController.SaveOneVehicleData(getCar);
                                        getCar.vehicleHandle.Dimension = 150;
                                        if (getCar.vehicleHandle != null)
                                        {
                                            if (getCar.vehicleHandle.HasSharedData("Vehicle:Text3D"))
                                            {
                                                getCar.vehicleHandle.ResetSharedData("Vehicle:Text3D");
                                            }
                                            getCar.vehicleHandle.Delete();
                                            getCar.vehicleHandle = null;
                                        }
                                        player.Dimension = 0;
                                        Helper.SendNotificationWithoutButton(player, "Das Fahrzeug wurde erfolgreich eingeparkt!", "success", "center");
                                    }, delayTime: 425);
                                }
                                else
                                {
                                    Helper.SendNotificationWithoutButton(player, "Du kannst jetzt keine Fahrzeuge einparken!", "error", "center");
                                }
                                return;
                            }
                        case "exitgarage":
                            {
                                if (!player.IsInVehicle)
                                {
                                    number = Convert.ToInt32(value);
                                    Cars getCar = null;
                                    if (house != null)
                                    {
                                        if (player.Position.DistanceTo(house.position) <= 1.75f)
                                        {
                                            Helper.SendNotificationWithoutButton(player, "Du darfst nicht so nah am Haus stehen!", "error", "center");
                                            return;
                                        }
                                    }
                                    foreach (Cars car in Cars.carList)
                                    {
                                        if (car.vehicleData != null && car.vehicleHandle == null && car.vehicleData.id == number)
                                        {
                                            getCar = car;
                                            break;
                                        }
                                    }
                                    if (getCar == null || getCar.vehicleData == null)
                                    {
                                        Helper.SendNotificationWithoutButton(player, "Dieses Fahrzeug kann nicht ausgeparkt werden!", "error", "center");
                                        return;
                                    }
                                    if (getCar.vehicleData.garage == "n/A")
                                    {
                                        Helper.SendNotificationWithoutButton(player, "Du kannst dieses Fahrzeug nicht ausparken!", "error", "center");
                                        return;
                                    }
                                    if (getCar.vehicleData.garage.Contains("faction-") && character.rang < getCar.vehicleData.rang)
                                    {
                                        Helper.SendNotificationWithoutButton(player, "Du darfst dieses Fahrzeug nicht ausparken!", "error", "center");
                                        return;
                                    }
                                    if(Helper.IsInRangeOfPoint(player.Position, new Vector3(463.60544, -982.2816, 43.692), 37.75f) && player.Position.Z >= 38 && getCar.vehicleData.vehiclename.ToLower() !=  "polmav" && getCar.vehicleData.vehiclename.ToLower() != "buzzard2" && getCar.vehicleData.vehiclename.ToLower() != "swift")
                                    {
                                        Helper.SendNotificationWithoutButton(player, "Du kannst hier nur Helikopter ausparken!", "error", "center");
                                        return;
                                    }
                                    if (Helper.IsInRangeOfPoint(player.Position, new Vector3(445.048, -972.2439, 25.788462), 6.75f) && (getCar.vehicleData.vehiclename.ToLower() == "polmav" || getCar.vehicleData.vehiclename.ToLower() == "buzzard2" || getCar.vehicleData.vehiclename.ToLower() == "swift"))
                                    {
                                        Helper.SendNotificationWithoutButton(player, "Du kannst hier keine Helikopter ausparken!", "error", "center");
                                        return;
                                    }
                                    if (Helper.IsInRangeOfPoint(player.Position, new Vector3(569.6448, 10.3835535, 103.22986), 37.75f) && player.Position.Z >= 102 && getCar.vehicleData.vehiclename.ToLower() != "polmav" && getCar.vehicleData.vehiclename.ToLower() != "buzzard2" && getCar.vehicleData.vehiclename.ToLower() != "swift")
                                    {
                                        Helper.SendNotificationWithoutButton(player, "Du kannst hier nur Helikopter ausparken!", "error", "center");
                                        return;
                                    }
                                    if (Helper.IsInRangeOfPoint(player.Position, new Vector3(608.4999, -2.2341008, 70.62814), 7.75f) && (getCar.vehicleData.vehiclename.ToLower() == "polmav" || getCar.vehicleData.vehiclename.ToLower() == "buzzard2" || getCar.vehicleData.vehiclename.ToLower() == "swift"))
                                    {
                                        Helper.SendNotificationWithoutButton(player, "Du kannst hier keine Helikopter ausparken!", "error", "center");
                                        return;
                                    }
                                    if (Helper.IsInRangeOfPoint(player.Position, new Vector3(-651.331, 328.43048, 140.14816), 37.75f) && player.Position.Z >= 139 && getCar.vehicleData.vehiclename.ToLower() != "swift" && getCar.vehicleData.vehiclename.ToLower() != "seasparrow" && getCar.vehicleData.vehiclename.ToLower() != "frogger")
                                    {
                                        Helper.SendNotificationWithoutButton(player, "Du kannst hier nur Helikopter ausparken!", "error", "center");
                                        return;
                                    }
                                    if (Helper.IsInRangeOfPoint(player.Position, new Vector3(-676.8967, 336.86328, 78.11836), 6.75f) && (getCar.vehicleData.vehiclename.ToLower() == "swift" || getCar.vehicleData.vehiclename.ToLower() == "seasparrow" || getCar.vehicleData.vehiclename.ToLower() == "frogger"))
                                    {
                                        Helper.SendNotificationWithoutButton(player, "Du kannst hier keine Helikopter ausparken!", "error", "center");
                                        return;
                                    }
                                    if (getCar.vehicleData.plate.Contains("S-W-A-T") && character.swat == 0 && character.rang < 10)
                                    {
                                        Helper.SendNotificationWithoutButton(player, "Du darfst dieses Fahrzeug nicht ausparken!", "error", "center");
                                        return;
                                    }
                                    if (Helper.IsInRangeOfPoint(player.Position, new Vector3(-354.90842, -166.38257, 39.015373), 6.75f) && (getCar.vehicleData.vehiclename.ToLower() == "cargobob2" || getCar.vehicleData.vehiclename.ToLower() == "frogger"))
                                    {
                                        Helper.SendNotificationWithoutButton(player, "Du kannst hier keine Helikopter ausparken!", "error", "center");
                                        return;
                                    }
                                    if(getCar.vehicleData.garage == "towed-1")
                                    {
                                        bool owner = false;
                                        if(getCar.vehicleData.owner == "character-"+ character.id || getCar.vehicleData.owner == "group-" + character.mygroup || (character.faction == 3 && character.factionduty == true))
                                        {
                                            owner = true;
                                        }
                                        if(owner == false)
                                        {
                                           Helper.SendNotificationWithoutButton(player, "Du kannst dieses Fahrzeug nicht freikaufen!", "error", "center");
                                           return;
                                        }
                                        if (character.faction != 3 || character.factionduty == false)
                                        {
                                            if(character.cash < getCar.vehicleData.towed)
                                            {
                                                Helper.SendNotificationWithoutButton(player, $"Du hast nicht genügend Geld dabei - {getCar.vehicleData.towed}$!", "error", "center");
                                                return;
                                            }
                                        }
                                    }
                                    getCar.vehicleData.garage = "n/A";
                                    Vehicle retCar = Cars.createNewCar(getCar.vehicleData.vehiclename, new Vector3(), 0f, 0, 0, "n/A", "n/A", false, false, true, player.Dimension, getCar.vehicleData, false, false);
                                    NAPI.Task.Run(() =>
                                    {
                                        if (Helper.IsInRangeOfPoint(player.Position, new Vector3(445.048, -972.2439, 25.788462), 6.75f))
                                        {
                                            Vector3[] spawnPD = new Vector3[6]
                                                               {  new Vector3(441.18842, -979.3753, 25.485493),
                                                                  new Vector3(441.24408, -985.0216, 25.485113),
                                                                  new Vector3(441.3216, -990.73065, 25.484877),
                                                                  new Vector3(426.6244, -987.8079, 25.484583),
                                                                  new Vector3(426.5274, -982.2405, 25.48492),
                                                                  new Vector3(426.49677, -976.4785, 25.484594),};
                                            Vector3[] spawnPDRotation = new Vector3[6]
                                                               {  new Vector3(0.0013745355, 0.055669256, 90.09856),
                                                                  new Vector3(0.008190986, 0.0033707616, 90.510216),
                                                                  new Vector3(0.016559457, 0.0030192886, 89.367874),
                                                                  new Vector3(-0.00068132574, 0.004955888, -90.36821),
                                                                  new Vector3(0.016152218, 0.0029003185, -91.61219),
                                                                  new Vector3(-0.021698274, 0.0015126165, -90.0997)};

                                            Random rand = new Random();
                                            int index = rand.Next(6);
                                            retCar.Position = spawnPD[index];
                                            retCar.Rotation = spawnPDRotation[index];

                                            if(getCar.vehicleData.plate.Contains("LS-"))
                                            {
                                                getCar.vehicleData.plate = "LS-C-1" + rand.Next(10, 99);
                                                retCar.NumberPlate = getCar.vehicleData.plate;
                                            }

                                        }
                                        else if (Helper.IsInRangeOfPoint(player.Position, new Vector3(463.60544, -982.2816, 43.692), 37.75f) && player.Position.Z >= 38)
                                        {
                                            retCar.Position = new Vector3(449.27942, -981.3177, 44.08361);
                                            retCar.Rotation = new Vector3(0.1176293, -0.0036671718, 91.15162);
                                        }
                                        if (Helper.IsInRangeOfPoint(player.Position, new Vector3(608.4999, -2.2341008, 70.62814), 7.75f))
                                        {
                                            Vector3[] spawnPD = new Vector3[4]
                                                               {  new Vector3(594.7514, 3.416574, 70.383766),
                                                                  new Vector3(590.36163, 3.8411708, 70.383354),
                                                                  new Vector3(592.27264, -10.559145, 70.38388),
                                                                  new Vector3(588.05664, -9.917313, 70.383804)};
                                            Vector3[] spawnPDRotation = new Vector3[4]
                                                               {  new Vector3(0.045548, 0.0019151475, 170.01077),
                                                                  new Vector3(0.09942807, 0.0018863127, 170.18188),
                                                                  new Vector3(0.039226905, 0.0021090768, -9.325145),
                                                                  new Vector3(0.068524025, 0.0027348043, -7.9693737)};

                                            Random rand = new Random();
                                            int index = rand.Next(4);
                                            retCar.Position = spawnPD[index];
                                            retCar.Rotation = spawnPDRotation[index];

                                            if (getCar.vehicleData != null && getCar.vehicleData.plate.Contains("LS-"))
                                            {
                                                getCar.vehicleData.plate = "LS-C-1" + rand.Next(10, 99);
                                                retCar.NumberPlate = getCar.vehicleData.plate;
                                            }
                                        }
                                        else if (Helper.IsInRangeOfPoint(player.Position, new Vector3(569.6448, 10.3835535, 103.22986), 37.75f) && player.Position.Z >= 102)
                                        {
                                            retCar.Position = new Vector3(579.83307, 12.119286, 103.61984);
                                            retCar.Rotation = new Vector3(0.15559249, 0.0010522096, 179.66924);
                                        }
                                        else if (Helper.IsInRangeOfPoint(player.Position, new Vector3(-651.331, 328.43048, 140.14816), 37.75f) && player.Position.Z >= 139)
                                        {
                                            retCar.Position = new Vector3(-687.2395, 321.867, 140.54016);
                                            retCar.Rotation = new Vector3(0.11989029, -0.0038079454, 175.1633);
                                        }
                                        else if (Helper.IsInRangeOfPoint(player.Position, new Vector3(-676.6648, 337.3723, 78.118355), 7.75f))
                                        {
                                            Vector3[] spawnRC = new Vector3[5]
                                                               {  new Vector3(-670.29785, 365.51654, 77.885704),
                                                                  new Vector3(-656.75433, 364.29013, 77.88596),
                                                                  new Vector3(-694.59033, 367.62875, 77.88681),
                                                                  new Vector3(-685.5505, 349.61353, 77.886665),
                                                                  new Vector3(-664.9774, 347.84595, 77.88579)};
                                            Vector3[] spawnRCRotation = new Vector3[5]
                                                               {  new Vector3(0.010716025, -0.0001952204, 176.0292),
                                                                  new Vector3(0.015568655, -1.4044012, 174.2474),
                                                                  new Vector3(-0.054473776, 0.015042058, 174.72296),
                                                                  new Vector3(0.0004271262, 0.04291616, 173.66754),
                                                                  new Vector3(0.0029564204, 0.011523152, 174.79047)};

                                            Random rand = new Random();
                                            int index = rand.Next(4);
                                            retCar.Position = spawnRC[index];
                                            retCar.Rotation = spawnRCRotation[index];
                                        }
                                        else if (Helper.IsInRangeOfPoint(player.Position, new Vector3(-333.11682, -146.52608, 60.445873), 37.75f) && player.Position.Z >= 59)
                                        {
                                            retCar.Position = new Vector3(-342.51685, -142.5949, 60.608845);
                                            retCar.Rotation = new Vector3(0, 0, -63.660927);
                                        }
                                        else if (Helper.IsInRangeOfPoint(player.Position, new Vector3(-354.90842, -166.38257, 39.015373), 7.75f))
                                        {
                                            retCar.Position = new Vector3(-364.3486, -147.34644, 38.482533+ 0.015);
                                            retCar.Rotation = new Vector3(2.6853187, -0.10310078, 30.188915);
                                        }
                                        else if (Helper.IsInRangeOfPoint(player.Position, new Vector3(408.9756, -1622.715, 29.291948), 6.75f))
                                        {
                                            string carname = getCar.vehicleData.ownname != "n/A" ? getCar.vehicleData.ownname : getCar.vehicleData.vehiclename;
                                            if (character.faction != 3 || character.factionduty == false)
                                            {
                                                CharacterController.SetMoney(player, -getCar.vehicleData.towed);
                                                Helper.SetGovMoney(getCar.vehicleData.towed, "Verwahrplatz Freikauf");
                                                Helper.CreateFactionLog(character.faction, $"{character.name} hat das Fahrzeug {carname}({getCar.vehicleData.plate}) aus dem Verwahrplatz für {getCar.vehicleData.towed}$ ausgeparkt!");
                                            }
                                            else
                                            {
                                                Helper.CreateFactionLog(character.faction, $"{character.name} hat das Fahrzeug {carname}({getCar.vehicleData.plate}) aus dem Verwahrplatz ausgeparkt!");                                            
                                            }
                                            getCar.vehicleData.towed = 0;
                                            retCar.Position = new Vector3(402.02396, -1630.9695, 28.685005);
                                            retCar.Rotation = new Vector3(-0.011387486, 0.0621425, 169.69243);
                                        }
                                        else if (Helper.IsInRangeOfPoint(player.Position, new Vector3(-506.12866, -199.77043, 34.215195), 18.65f))
                                        {
                                            retCar.Position = new Vector3(-484.0911, -194.79396, 36.533215 + 0.015);
                                            retCar.Rotation = new Vector3(-1.3784541, 1.5712129, -149.6499);
                                        }
                                        else
                                        {
                                            if (bizz != null)
                                            {
                                                retCar.Position = Business.GetGaragePosition(bizz.id);
                                                if (bizz.id == 33)
                                                {
                                                    retCar.Rotation = new Vector3(0.0, 0.0, -111.8506f);
                                                }
                                                else if (bizz.id == 34)
                                                {
                                                    retCar.Rotation = new Vector3(0.0, 0.0, 21.412857f);
                                                }
                                                else if (bizz.id == 35)
                                                {
                                                    retCar.Rotation = new Vector3(0.0, 0.0, 58.211884f);
                                                }
                                                else if (bizz.id == 35)
                                                {
                                                    retCar.Rotation = new Vector3(0.0, 0.0, -20.475378f);
                                                }
                                            }
                                            else
                                            {
                                                retCar.Rotation = new Vector3(0.0, 0.0, player.Heading);
                                            }
                                        }
                                        retCar.Dimension = player.Dimension;
                                        retCar.SetData<bool>("Vehicle:EngineStatus", false);
                                        retCar.EngineStatus = false;
                                        if (retCar.Position.DistanceTo(player.Position) >= 750 && !getCar.vehicleData.owner.Contains("faction-"))
                                        {
                                            retCar.Position = player.Position;
                                        }
                                        player.SetIntoVehicle(retCar, (int)VehicleSeat.Driver);
                                        Helper.SetVehicleEngine(retCar, false);
                                        Helper.SendNotificationWithoutButton(player, "Das Fahrzeug wurde erfolgreich ausgeparkt!", "success", "center");
                                    }, delayTime: 55);
                                    return;
                                }
                                else
                                {
                                    Helper.SendNotificationWithoutButton(player, "Du musst zuerst dein aktuelles Fahrzeug verlassen!", "error", "center");
                                    return;
                                }
                            }
                        case "mietpreis":
                            {
                                number = Convert.ToInt32(value);
                                if (number < 0 || number > 999999)
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Ungültiger Mietpreis!", "error", "center");
                                    return;
                                }
                                house.rental = number;
                                if (number > 0)
                                {
                                    if (house.noshield == 1)
                                    {
                                        house.status = 4;
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
                                SetHouseHandle(house);
                                Helper.SendNotificationWithoutButton2(player, "Mietpreis gesetzt!", "success", "center");
                                break;
                            }
                        case "newowner":
                            {
                                if (value == "n/A" || value.Length <= 3)
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Ungültiger Eigentümername!", "error", "center");
                                    return;
                                }
                                Player newPlayer = Helper.GetPlayerByCharacterName(value);
                                if (newPlayer == null || newPlayer == player)
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Ungültiger neuer Eigentümer!", "error", "center");
                                    return;
                                }
                                if (ItemsController.CheckItemWithProp(newPlayer, "Mietschlüssel: " + house.id))
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Der neue Eigentümer kann nicht Mieter und Eigentümer gleichzeitig sein!", "error", "center");
                                    return;
                                }
                                house.owner = value;
                                SetHouseHandle(house);
                                Helper.SendNotificationWithoutButton2(player, "Neuer Eigentümer wurde gesetzt, den Schlüssel kannst du über das Inventar vergeben!", "success", "center");
                                break;
                            }
                        case "endrent":
                            {
                                if (ItemsController.CheckItemWithProp(player, "Mietschlüssel: " + house.id))
                                {

                                    ItemsController.DeleteItemWithProp(player, "Miethausnummer: " + house.id);
                                    if (house.tenants > 0)
                                    {
                                        house.tenants--;
                                    }
                                    SetHouseHandle(house);
                                    Helper.SendNotificationWithoutButton2(player, "Du hast die Miete in diesem Haus beendet!", "success", "center");
                                }
                                else
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Du bist hier nicht eingemietet!", "error", "center");
                                }
                                break;
                            }
                        case "sellhouse":
                            {
                                if (house.owner == "n/A")
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Dieses Haus kann nicht verkauft werden!", "error", "center");
                                    return;
                                }
                                bool found = false;
                                foreach (Player p in NAPI.Pools.GetAllPlayers())
                                {
                                    if (p != null && p.Dimension > 0 && p.GetOwnSharedData<bool>("Player:Spawned") == true)
                                    {
                                        Character character2 = Helper.GetCharacterData(p);
                                        if (character2 != null && character2.inhouse == house.id)
                                        {
                                            found = true;
                                            break;
                                        }
                                    }
                                }
                                if (found == true)
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Es befinden sich noch Spieler im Haus!", "error", "center");
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
                                int money = Furniture.SellAllFurniture(house.id);
                                CharacterController.SetMoney(player, (house.price / 2) + money);
                                House.KickAllTenants(house.id, true);
                                House.SetHouseHandle(house);
                                House.SaveHouse(house);
                                ItemsController.DeleteItemWithProp(player, "Hausnummer: " + house.id);
                                ItemsController.DeleteItemWithProp(player, "Miethausnummer: " + house.id);
                                CharacterController.SaveCharacter(player);
                                Helper.SendNotificationWithoutButton2(player, $"Du hast das Haus für {(house.price / 2) + money}$ verkauft!", "success", "center", 4500);
                                break;
                            }
                        case "kicktenants":
                            {
                                if (house.tenants == 0)
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Dieses Haus hat keine Mieter!", "error", "center");
                                    return;
                                }
                                house.tenants = 0;
                                House.KickAllTenants(house.id, false);
                                Helper.SendNotificationWithoutButton2(player, "Du hast alle Mieter rausgeworfen!", "success", "center", 3500);
                                break;
                            }
                        case "kicktenant":
                            {
                                number = Convert.ToInt32(value);
                                if (!value.All(char.IsDigit) || number < 1)
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Ungültige Interaktion!", "error", "center");
                                    return;
                                }
                                if (house.tenants > 0)
                                {
                                    house.tenants--;
                                }
                                House.KickTenant(house.id, number);
                                Helper.SendNotificationWithoutButton2(player, "Du hast den Mieter rausgeworfen!", "success", "center", 3500);
                                break;
                            }
                        case "newhousedoor":
                            {
                                if (character.cash < 1250)
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Du hast nicht genügend Geld!", "error", "center");
                                    return;
                                }
                                CharacterController.SetMoney(player, -1250);
                                House.NewHouseKey(house.id, character.name);
                                Helper.SendNotificationWithoutButton2(player, "Du hast das Schloss auswechseln lassen!", "success", "center", 3500);
                                break;
                            }
                        case "newkey":
                            {
                                if (character.cash < 250)
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Du hast nicht genügend Geld!", "error", "center");
                                    return;
                                }
                                Items newitem = ItemsController.CreateNewItem(player, character.id, "Hausschlüssel", "Player", 1, ItemsController.GetFreeItemID(player));
                                if (!ItemsController.CanPlayerHoldItem(player, newitem.weight))
                                {
                                    newitem = null;
                                    Helper.SendNotificationWithoutButton2(player, "Du hast keinen Platz mehr im Inventar für den Hausschlüssel!", "success", "center");
                                    return;
                                }
                                if (newitem != null)
                                {
                                    CharacterController.SetMoney(player, -250);
                                    newitem.props = "Hausnummer: " + house.id;
                                    tempData.itemlist.Add(newitem);
                                    Helper.SendNotificationWithoutButton2(player, "Du hast einen Hausschlüssel nachmachen lassen!", "success", "center", 3500);
                                }
                                else
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Es konnte kein Hausschlüssel nachgemacht werden!", "error", "center", 3500);
                                }
                                break;
                            }
                        case "grouphouse":
                            {
                                if (house.housegroup != -1)
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Gruppierungsstatus entfernt!", "success", "center");
                                    house.housegroup = -1;
                                    house.blip = 40;
                                    house.stock = 0;
                                    house.stockprice = 30;
                                    if (House.GetInteriorClassify(house.interior) >= 4)
                                    {
                                        house.interior = House.GetRandomHouseInterior(house.classify);
                                        foreach (Player p in NAPI.Pools.GetAllPlayers())
                                        {
                                            if (p != null && p.GetOwnSharedData<bool>("Player:Spawned") == true)
                                            {
                                                Character character2 = Helper.GetCharacterData(p);
                                                if (character2 != null && character2.inhouse == house.id)
                                                {
                                                    Helper.SetPlayerPosition(p, House.GetHouseExitPoint(house.interior));
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    Groups group = GroupsController.GetGroupById(character.mygroup);
                                    if (group == null)
                                    {
                                        Helper.SendNotificationWithoutButton2(player, "Du hast keine Gruppierung ausgewählt!", "error");
                                        return;
                                    }
                                    if (group.leader != character.id)
                                    {
                                        Helper.SendNotificationWithoutButton2(player, "Du bist nicht der Leader der Gruppierung!", "error");
                                        return;
                                    }
                                    Helper.SendNotificationWithoutButton2(player, "Gruppierungsstatus gesetzt!", "success", "center");
                                    house.housegroup = group.id;
                                    SetHouseHandle(house);
                                }
                                break;
                            }
                        case "setblip":
                            {
                                if (house.housegroup != -1)
                                {
                                    number = Convert.ToInt32(value);
                                    if (number < 1 || number > 826 || number == 60 || number == 420)
                                    {
                                        Helper.SendNotificationWithoutButton2(player, "Ungültiges Blip!", "error", "center");
                                        return;
                                    }
                                    house.blip = number;
                                    SetHouseHandle(house);
                                    Helper.SendNotificationWithoutButton2(player, "Blip gesetzt!", "success", "center");
                                }
                                else
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Diese Funktion steht nicht zur Verfügung!", "error", "center");
                                }
                                break;
                            }
                        case "setstock":
                            {
                                if (house.housegroup != -1)
                                {
                                    number = Convert.ToInt32(value);
                                    if ((number < 30 || number > 1250) && number != 0)
                                    {
                                        Helper.SendNotificationWithoutButton2(player, "Der Produktpreis muss zwischen 30$ und 1250$, oder 0$ zum deaktivieren liegen!", "error", "center");
                                        return;
                                    }
                                    house.stockprice = number;
                                    Helper.SendNotificationWithoutButton2(player, "Produktpreis gesetzt!", "success", "center");
                                }
                                else
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Diese Funktion steht nicht zur Verfügung!", "error", "center");
                                }
                                break;
                            }
                        case "newinterior":
                            {
                                house = House.GetClosestHouse(player);
                                if (house == null)
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Du musst vor deinem Haus stehen!", "error", "center");
                                    return;
                                }
                                if (house.interior <= 0)
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Du kannst kein neues Hausinterior auswählen!", "error", "center");
                                    return;
                                }
                                int count = 0;
                                foreach (Player p in NAPI.Pools.GetAllPlayers())
                                {
                                    if (p != null && p.GetOwnSharedData<bool>("Player:Spawned") == true)
                                    {
                                        Character character2 = Helper.GetCharacterData(p);
                                        if (character2 != null && character.inhouse == house.id)
                                        {
                                            count++;
                                        }
                                    }
                                }
                                if (count > 0)
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Es müssen zuerst alle Spieler das Haus verlassen!", "error", "center");
                                    return;
                                }
                                int cash = (House.GetInteriorClassify(house.interior) + 1) * 7500;
                                if (House.GetInteriorClassify(house.interior) > 3)
                                {
                                    cash = 35000;
                                }
                                tempData.lasthouse = house.id;
                                tempData.counter = 0;
                                tempData.interiorswitch = true;
                                CharacterController.SaveCharacter(player);
                                player.Dimension = 125000;
                                player.TriggerEvent("Client:LoadIPL", House.GetInteriorIPL(tempList[0].id));
                                NAPI.Task.Run(() =>
                                {
                                    Helper.SetPlayerPosition(player, new Vector3(tempList[0].posx, tempList[0].posy, tempList[0].posz));
                                }, delayTime: 375);
                                player.TriggerEvent("Client:SwitchHouseInterior", "Rechte Pfeiltaste -> Nächstes Interior", "Linke Pfeiltaste -> Letztes Interior", "B -> Interior kaufen", "Backspace -> Abbrechen", "Interiorausbau", "Preis: " + cash + "$", "Interior-ID: " + tempList[tempData.counter].id, "Hausnummer: " + house.id, "Interior-Name: " + tempList[tempData.counter].ipl, 0);
                                player.TriggerEvent("Client:UpdateInfoBox", "null", "null", "null", "null", "Interiorausbau", "Preis: " + cash + "$", "Interior-ID: " + tempList[tempData.counter].id, "Hausnummer: " + house.id, "Interior-Name: " + tempList[tempData.counter].ipl, 0);
                                break;
                            }
                        case "nextinterior":
                            {
                                if (tempData.counter < (tempList.Count - 1))
                                {
                                    int cash = (House.GetInteriorClassify(house.interior) + 1) * 7500;
                                    if (House.GetInteriorClassify(house.interior) > 3)
                                    {
                                        cash = 35000;
                                    }
                                    tempData.counter++;
                                    player.Dimension = 125000;
                                    player.TriggerEvent("Client:LoadIPL", House.GetInteriorIPL(tempList[tempData.counter].id));
                                    NAPI.Task.Run(() =>
                                    {
                                        Helper.SetPlayerPosition(player, new Vector3(tempList[tempData.counter].posx, tempList[tempData.counter].posy, tempList[tempData.counter].posz));
                                    }, delayTime: 375);
                                    player.TriggerEvent("Client:UpdateInfoBox", "null", "null", "null", "null", "Interiorausbau", "Preis: " + cash + "$", "Interior-ID: " + tempList[tempData.counter].id, "Hausnummer: " + house.id, "Interior-Name: " + tempList[tempData.counter].ipl, 0);
                                }
                                break;
                            }
                        case "lastinterior":
                            {
                                if (tempData.counter > 0)
                                {
                                    int cash = (House.GetInteriorClassify(house.interior) + 1) * 7500;
                                    if (House.GetInteriorClassify(house.interior) > 3)
                                    {
                                        cash = 35000;
                                    }
                                    tempData.counter--;
                                    player.Dimension = 125000;

                                    player.TriggerEvent("Client:LoadIPL", House.GetInteriorIPL(tempList[tempData.counter].id));
                                    NAPI.Task.Run(() =>
                                    {
                                        Helper.SetPlayerPosition(player, new Vector3(tempList[tempData.counter].posx, tempList[tempData.counter].posy, tempList[tempData.counter].posz));
                                    }, delayTime: 375);
                                    player.TriggerEvent("Client:UpdateInfoBox", "null", "null", "null", "null", "Interiorausbau", "Preis: " + cash + "$", "Interior-ID: " + tempList[tempData.counter].id, "Hausnummer: " + house.id, "Interior-Name: " + tempList[tempData.counter].ipl, 0);
                                }
                                break;
                            }
                        case "abortinterior":
                            {
                                tempData.counter = 0;
                                tempData.lasthouse = 0;
                                tempData.interiorswitch = false;
                                string[] spawnCharAfterReconnect = new string[5];
                                spawnCharAfterReconnect = character.lastpos.Split("|");
                                player.Dimension = Convert.ToUInt32(spawnCharAfterReconnect[4]);
                                Helper.SetPlayerPosition(player, new Vector3(float.Parse(spawnCharAfterReconnect[0]), float.Parse(spawnCharAfterReconnect[1]), float.Parse(spawnCharAfterReconnect[2])));
                                player.Heading = float.Parse(spawnCharAfterReconnect[3]);
                                player.Rotation = new Vector3(0.0, 0.0, float.Parse(spawnCharAfterReconnect[3]));
                                player.TriggerEvent("Client:SwitchHouseInterior", "null", "null", "null", "null", "Interiorausbau", "Preis: 0$", "Interior-ID: 0", "Hausnummer: 0", "null", 0);
                                Helper.SendNotificationWithoutButton(player, "Interiorausbau abgebrochen!", "success", "top-left");
                                break;
                            }
                        case "buyinterior":
                            {
                                int cash = (House.GetInteriorClassify(house.interior) + 1) * 7500;
                                if (House.GetInteriorClassify(house.interior) > 3)
                                {
                                    cash = 35000;
                                }
                                Bank bank = BankController.GetDefaultBank(player, character.defaultbank);
                                if (bank == null)
                                {
                                    Helper.SendNotificationWithoutButton(player, "Es wurde kein Standardkonto gefunden!", "error", "top-left");
                                    return;
                                }
                                if (bank.bankvalue < cash)
                                {
                                    Helper.SendNotificationWithoutButton(player, $"Soviel Geld liegt nicht auf dem Konto - {cash}$!", "error", "top-left");
                                    return;
                                }
                                if (house.interior == tempList[tempData.counter].id)
                                {
                                    Helper.SendNotificationWithoutButton(player, $"Das Haus hat dieses Interior bereits!", "error", "top-left");
                                    return;
                                }
                                bank.bankvalue -= cash;
                                Furniture.CancelAllFurniture(house.id);
                                house.interior = tempList[tempData.counter].id;
                                tempData.counter = 0;
                                tempData.lasthouse = 0;
                                tempData.interiorswitch = false;
                                string[] spawnCharAfterReconnect = new string[5];
                                spawnCharAfterReconnect = character.lastpos.Split("|");
                                player.Dimension = Convert.ToUInt32(spawnCharAfterReconnect[4]);
                                Helper.SetPlayerPosition(player, new Vector3(float.Parse(spawnCharAfterReconnect[0]), float.Parse(spawnCharAfterReconnect[1]), float.Parse(spawnCharAfterReconnect[2])));
                                player.Heading = float.Parse(spawnCharAfterReconnect[3]);
                                player.Rotation = new Vector3(0.0, 0.0, float.Parse(spawnCharAfterReconnect[3]));
                                player.TriggerEvent("Client:SwitchHouseInterior", "null", "null", "null", "null", "Interiorausbau", "Preis: 0$", "Interior-ID: 0", "Hausnummer: 0", "null", 0);
                                Helper.SendNotificationWithoutButton(player, "Interiorausbau erfolgreich vollzogen!", "success", "top-left");
                                BankController.SaveBank(bank);
                                break;
                            }
                        case "klingelschild":
                            {
                                number = Convert.ToInt32(value);
                                if (number < 0 || number > 1)
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Ungültiger Wert!", "error", "center");
                                    return;
                                }
                                house.noshield = number;
                                if (house.noshield == 0)
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Klingelschild angebracht!", "success", "center");
                                }
                                else
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Klingelschild abgebaut!", "success", "center");
                                }
                                if (house.rental > 0)
                                {
                                    if (house.noshield == 1)
                                    {
                                        house.status = 4;
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
                                House.SetHouseHandle(house);
                                break;
                            }
                        default:
                            {
                                Helper.SendNotificationWithoutButton2(player, "Ungültige Interaktion!", "error", "center");
                                break;
                            }
                    }
                    if (setting != "nextinterior" && setting != "lastinterior" && setting != "abortinterior" && setting != "buyinterior")
                    {
                        Menu.UpdateHouseHud(player);
                    }
                    House.SaveHouse(house);
                }
                else
                {
                    Helper.SendNotificationWithoutButton2(player, "Ungültige Interaktion!", "error", "center");
                    Helper.SendNotificationWithoutButton2(player, "Du bist nicht der Eigentümer dieses Hauses!", "error", "center");
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", "[OnHouseSettings]: " + e.ToString());
            }
        }
    }
}
