using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using GTANetworkAPI;
using MySqlConnector;
using NemesusWorld.Controllers;
using NemesusWorld.Models;
using NemesusWorld.Utils;
using Newtonsoft.Json.Linq;

namespace NemesusWorld.Database
{
    class Business : Script
    {
        public static List<Business> businessList = new List<Business>();

        public int id { get; set; }
        public Vector3 position { get; set; }
        public int price { get; set; }
        public string name { get; set; }
        public string name2 { get; set; }
        public string owner { get; set; }
        public int funds { get; set; }
        public int products { get; set; }
        public float multiplier { get; set; }
        public int cash { get; set; }
        public int govcash { get; set; }
        public int prodprice { get; set; }
        public int blipid { get; set; }
        public int buyproducts { get; set; }
        public int selled { get; set; }
        public int getmoney { get; set; }
        public GTANetworkAPI.Marker markerHandle { get; set; }
        public GTANetworkAPI.TextLabel textHandle { get; set; }
        public GTANetworkAPI.Blip blipHandle { get; set; }
        public GTANetworkAPI.Player musicPlayer { get; set; }
        public int elec { get; set; }
        public bool nobuy { get; set; }

        public Business()
        {
            id = 0;
            position = null;
            price = 0;
            name = "Business";
            name2 = "Business";
            owner = "n/A";
            funds = 0;
            products = 2000;
            multiplier = 1.0f;
            cash = 0;
            govcash = 0;
            prodprice = 0;
            blipid = 0;
            buyproducts = 0;
            selled = 0;
            getmoney = 25000;
            markerHandle = null;
            textHandle = null;
            blipHandle = null;
            musicPlayer = null;
            elec = 50;
            nobuy = false;
        }

        public static string GetBusinessLabelText(Business bizz)
        {
            string label = string.Empty;
            try
            {
                if (bizz.price > 0)
                {
                    if (bizz.owner != "n/A")
                    {
                        label = $"~w~{bizz.name}[{bizz.id}]\nInhaber: {bizz.owner}\n~y~Benutze 'F2' um das Business zu verwalten!";
                    }
                    else
                    {
                        label = $"~w~{bizz.name}[{bizz.id}]\nPreis: {bizz.price}$\n~y~Benutze Taste 'F' das Business zu kaufen!";
                    }
                }
                else
                {
                    label = $"~w~{bizz.name}[{bizz.id}]";
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", "[GetBusinessLabelText]: " + e.ToString());
            }
            return label;
        }

        public static void SetBusinessHandle(Business bizz)
        {
            try
            {
                if (bizz != null)
                {
                    if (bizz.textHandle != null)
                    {
                        bizz.textHandle.Delete();
                        bizz.textHandle = null;
                    }
                    if (bizz.blipHandle != null)
                    {
                        bizz.blipHandle.Delete();
                        bizz.blipHandle = null;
                    }
                    if (bizz.markerHandle != null)
                    {
                        bizz.markerHandle.Delete();
                        bizz.markerHandle = null;
                    }
                    string bizzLabelText = GetBusinessLabelText(bizz);
                    bizz.textHandle = NAPI.TextLabel.CreateTextLabel(bizzLabelText, new Vector3(bizz.position.X, bizz.position.Y, bizz.position.Z + 0.8), 5.0f, 0.75f, 4, new Color(255, 255, 255), false, 0);
                    if (bizz.owner == "n/A")
                    {
                        bizz.blipHandle = NAPI.Blip.CreateBlip(bizz.blipid, bizz.position, 0.7f, 2);
                        bizz.markerHandle = NAPI.Marker.CreateMarker(1, new Vector3(bizz.position.X, bizz.position.Y, bizz.position.Z - 1.1), bizz.position, new Vector3(), 1.0f, new Color(55, 118, 189), false, 0);
                    }
                    else
                    {
                        bizz.markerHandle = NAPI.Marker.CreateMarker(1, new Vector3(bizz.position.X, bizz.position.Y, bizz.position.Z - 1.1), bizz.position, new Vector3(), 1.0f, new Color(255, 255, 255), false, 0);
                        bizz.blipHandle = NAPI.Blip.CreateBlip(bizz.blipid, bizz.position, 0.7f, 1);
                    }
                    NAPI.Blip.SetBlipName(bizz.blipHandle, bizz.name); NAPI.Blip.SetBlipShortRange(bizz.blipHandle, true); NAPI.Blip.SetBlipColor(bizz.blipHandle, 38);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", "[SetBusinessHandle]: " + e.ToString());
            }
        }

        public static bool HasPlayerBusinessKey(Player player, int bizzid)
        {
            try
            {
                if (Account.IsAdminOnDuty(player, (int)Account.AdminRanks.Administrator)) return true;
                TempData tempData = Helper.GetCharacterTempData(player);
                if (tempData != null)
                {
                    if (tempData.itemlist.Count > 0)
                    {
                        foreach (Items item in tempData.itemlist)
                        {
                            if (item != null && ((item.description == "Bizzschlüssel" && item.props.Contains("Bizzschlüssel: " + bizzid)) || (item.description == "Bizzschlüssel" && item.props.Contains("Bizzschlüssel: " + bizzid))))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", "[HasPlayerBusinessKey]: " + e.ToString());
            }
            return false;
        }

        public static void GetAllBusiness()
        {
            try
            {
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                foreach (BusinessLoad business in db.Fetch<BusinessLoad>("SELECT * FROM business"))
                {
                    Business bizz = new Business();
                    bizz.id = business.id;
                    bizz.position = new Vector3(business.posx, business.posy, business.posz);
                    bizz.price = business.price;
                    bizz.name = business.name;
                    bizz.name2 = business.name2;
                    bizz.owner = business.owner;
                    bizz.funds = business.funds;
                    bizz.funds = 0;
                    bizz.elec = business.elec;
                    if(bizz.elec != 50 && bizz.elec != 51)
                    {
                        bizz.elec = 50;
                    }
                    bizz.products = business.products;
                    if (business.multiplier < 1.0 || business.multiplier > 3.0)
                    {
                        business.multiplier = 1.0f;
                    }
                    bizz.multiplier = business.multiplier;
                    bizz.cash = business.cash;
                    bizz.govcash = business.govcash;
                    bizz.prodprice = business.prodprice;
                    if (bizz.prodprice <= 0)
                    {
                        bizz.prodprice = 30;
                    }
                    bizz.blipid = business.blipid;
                    bizz.buyproducts = business.buyproducts;
                    bizz.selled = business.selled;
                    bizz.getmoney = business.getmoney;
                    SetBusinessHandle(bizz);
                    businessList.Add(bizz);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetAllBusiness]: " + e.ToString());
            }
        }

        public static void SaveBusiness(Business business)
        {
            try
            {
                if (business != null)
                {
                    PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                    BusinessLoad bizz = new BusinessLoad();
                    bizz.id = business.id;
                    bizz.posx = business.position.X;
                    bizz.posy = business.position.Y;
                    bizz.posz = business.position.Z;
                    bizz.price = business.price;
                    bizz.name = business.name;
                    bizz.name2 = business.name2;
                    bizz.owner = business.owner;
                    bizz.funds = business.funds;
                    bizz.products = business.products;
                    bizz.multiplier = business.multiplier;
                    bizz.cash = business.cash;
                    bizz.govcash = business.govcash;
                    bizz.prodprice = business.prodprice;
                    bizz.blipid = business.blipid;
                    bizz.buyproducts = business.buyproducts;
                    bizz.selled = business.selled;
                    bizz.getmoney = business.getmoney;
                    bizz.elec = business.elec;
                    db.Save(bizz);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[SaveBusiness]: " + e.ToString());
            }
        }

        public static List<BusinessLoad> GetBusinessWithNeeds()
        {
            List<BusinessLoad> bizzList = new List<BusinessLoad>();
            try
            {
                foreach (Business business in businessList)
                {
                    if (business != null && business.buyproducts == 0 && business.cash > 0 && business.products < 1800 && bizzList.Count < 10)
                    {
                        BusinessLoad bizz = new BusinessLoad();
                        bizz.id = business.id;
                        bizz.posx = business.position.X;
                        bizz.posy = business.position.Y;
                        bizz.posz = business.position.Z;
                        bizz.price = business.price;
                        bizz.name = business.name;
                        bizz.name2 = business.name2;
                        bizz.owner = business.owner;
                        bizz.funds = business.funds;
                        bizz.products = business.products;
                        bizz.multiplier = business.multiplier;
                        bizz.cash = business.cash;
                        bizz.govcash = business.govcash;
                        bizz.prodprice = business.prodprice;
                        bizz.blipid = business.blipid;
                        bizz.buyproducts = business.buyproducts;
                        bizz.selled = business.selled;
                        bizz.getmoney = business.getmoney;
                        bizz.elec = business.elec;
                        bizzList.Add(bizz);
                    }
                }
                return bizzList;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetBusinessWithNeeds]: " + e.ToString());
            }
            return null;
        }

        public static Business GetClosestBusiness(Player player, float distance = 1.5f)
        {
            try
            {
                Business retBizz = null;
                if (businessList == null) return retBizz;
                foreach (Business business in businessList)
                {
                    if (business != null && player.Position.DistanceTo(business.position) < distance)
                    {
                        retBizz = business;
                        distance = player.Position.DistanceTo(business.position);
                    }
                }
                return retBizz;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetClosestBusiness]: " + e.ToString());
            }
            return null;
        }

        public static int AddBusiness(Business bizz)
        {
            int bizzid = -1;

            try
            {
                MySqlCommand command = General.Connection.CreateCommand();

                command.CommandText = "INSERT INTO business (name, price, blipid, posx, posy, posz, products) VALUES (@name, @price, @blipid, @posx, @posy, @posz, @products)";
                command.Parameters.AddWithValue("@name", bizz.name);
                command.Parameters.AddWithValue("@price", bizz.price);
                command.Parameters.AddWithValue("@blipid", bizz.blipid);
                command.Parameters.AddWithValue("@posx", bizz.position.X);
                command.Parameters.AddWithValue("@posy", bizz.position.Y);
                command.Parameters.AddWithValue("@posz", bizz.position.Z);
                command.Parameters.AddWithValue("@products", bizz.products);

                command.ExecuteNonQuery();
                bizzid = (int)command.LastInsertedId;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", "[AddBusiness]: " + e.ToString());
            }
            return bizzid;
        }

        public static Business GetBusinessById(int id)
        {
            Business bizzTemp = null;
            try
            {
                foreach (Business bizz in businessList)
                {
                    if (bizz != null && bizz.id == id)
                    {
                        bizzTemp = bizz;
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", "[GetBusinessById]: " + e.ToString());
            }
            return bizzTemp;
        }

        public static void DeleteAllKeys(int bizzid, string myname = "n/A")
        {
            try
            {
                MySqlCommand command = General.Connection.CreateCommand();

                command.CommandText = "UPDATE characters SET items = REPLACE(items, 'Bizzschlüssel: " + bizzid + "', 'n/A') WHERE items LIKE @bizzid2 AND name != @name";
                command.Parameters.AddWithValue("@name", myname);
                command.Parameters.AddWithValue("@bizzid2", "%Bizzschlüssel: " + bizzid + "%");

                command.ExecuteNonQuery();

                foreach (Player p in NAPI.Pools.GetAllPlayers())
                {
                    if (p != null && p.GetOwnSharedData<bool>("Player:Spawned") == true)
                    {
                        Character character = Helper.GetCharacterData(p);
                        if (character != null && character.name != myname)
                        {
                            ItemsController.DeleteItemWithProp(p, "Bizzschlüssel: " + bizzid);
                            ItemsController.UpdateInventory(p);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", "[DeleteAllKeys]: " + e.ToString());
            }
        }

        public static Vector3 GetGaragePosition(int bizzid)
        {
            try
            {
                if (bizzid == 33)
                {
                    Vector3[] garagePositions = new Vector3[]
                    {
                        new Vector3(220.47763, -791.8197, 30.25607),
                        new Vector3(222.12898, -786.8258, 30.270803),
                        new Vector3(215.88982, -776.1881, 30.35343),
                        new Vector3(227.62427, -771.67053, 30.285803),
                        new Vector3(243.88724, -774.84155, 30.192902),
                        new Vector3(231.66956, -779.11346, 30.212461),
                        new Vector3(237.48253, -792.60925, 30.020803),
                        new Vector3(223.26329, -801.84564, 30.162672),
                        new Vector3(230.78276, -810.28357, 29.950712),
                        new Vector3(220.17114, -809.2122, 30.174448),
                        new Vector3(239.31107, -805.0945, 29.846323),
                        new Vector3(244.65996, -789.8065, 29.989233),
                        new Vector3(248.64418, -779.5449, 30.112862)
                    };

                    Random rand = new Random();
                    int index = rand.Next(garagePositions.Length - 1);

                    return garagePositions[index];
                }
                else if (bizzid == 34)
                {
                    Vector3[] garagePositions = new Vector3[]
                    {
                        new Vector3(-920.01416, -1372.0569, 0.037952274),
                        new Vector3(-922.51135, -1373.4573, 0.08387974),
                        new Vector3(-927.7647, -1374.9261, 0.1322779),
                        new Vector3(-931.712, -1375.6439, 0.03213477),
                        new Vector3(-936.4825, -1378.2966, 0.058403105),
                        new Vector3(-940.2137, -1378.955, 0.036344618)
                    };

                    Random rand = new Random();
                    int index = rand.Next(garagePositions.Length - 1);

                    return garagePositions[index];
                }
                else if (bizzid == 35)
                {
                    Vector3[] garagePositions = new Vector3[]
                    {
                        new Vector3(-987.733, -3013.5703, 14.714275)
                    };

                    return garagePositions[0];
                }
                else if (bizzid == 36)
                {
                    Vector3[] garagePositions = new Vector3[]
                    {
                        new Vector3(64.216324, 17.14985, 68.60541),
                        new Vector3(61.160503, 18.784561, 68.6894),
                        new Vector3(57.987823, 19.311274, 68.77781),
                        new Vector3(54.93279, 20.567413, 69.01674)
                    };

                    Random rand = new Random();
                    int index = rand.Next(garagePositions.Length - 1);

                    return garagePositions[index];
                }

            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", "[GetGaragePosition]: " + e.ToString());
            }
            return new Vector3(64.216324, 17.14985, 68.60541);
        }

        public static void ShowJuweMenu(Player player)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                TempData tempData = Helper.GetCharacterTempData(player);
                if (player.GetData<bool>("Player:InShop") == true) return;
                Business bizz = Business.GetClosestBusiness(player, 40.5f);
                if (bizz != null)
                {
                    if (bizz.nobuy == true)
                    {
                        Helper.SendNotificationWithoutButton(player, "Du kannst jetzt hier nichts kaufen!", "error");
                        return;
                    }
                    if (bizz.products < 35)
                    {
                        Helper.SendNotificationWithoutButton(player, "Unsere Lager sind leider leer!", "error");
                        return;
                    }
                    Helper.SetPlayerPosition(player, new Vector3(-626.1529, -235.16693, 38.057053));
                    player.Heading = -149.54729f;
                    player.Dimension = (uint)(player.Id + 5);
                    player.SetData<bool>("Player:InShop", true);
                    player.TriggerEvent("Client:PlayerFreeze", true);
                    JObject obj = null;
                    if (character.factionduty == false)
                    {
                        obj = JObject.Parse(character.json);
                    }
                    else
                    {
                        obj = JObject.Parse(character.dutyjson);
                    }
                    player.TriggerEvent("Client:ShowJuweMenu", NAPI.Util.ToJson(obj["clothing"]), NAPI.Util.ToJson(obj["clothingColor"]), character.gender, bizz.multiplier);
                    NAPI.Task.Run(() =>
                    {
                        player.TriggerEvent("Client:CharacterCameraOn");
                    }, delayTime: 515);
                }
                else
                {
                    Helper.SendNotificationWithoutButton(player, "Wir verkaufen hier nichts!", "error");
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", "[ShowJuweMenu]: " + e.ToString());
            }
        }

        public static void ShowMaskMenu(Player player)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                TempData tempData = Helper.GetCharacterTempData(player);
                if (player.GetData<bool>("Player:InShop") == true) return;
                Helper.SetPlayerPosition(player, new Vector3(-1584.7634, -957.51544, 13.017393));
                player.Heading = 46.67355f;
                player.Dimension = (uint)(player.Id + 5);
                player.SetData<bool>("Player:InShop", true);
                player.TriggerEvent("Client:PlayerFreeze", true);
                JObject obj = null;
                if (character.factionduty == false)
                {
                    obj = JObject.Parse(character.json);
                }
                else
                {
                    obj = JObject.Parse(character.dutyjson);
                }
                player.TriggerEvent("Client:ShowMaskMenu", NAPI.Util.ToJson(obj["clothing"]), NAPI.Util.ToJson(obj["clothingColor"]), character.gender);
                NAPI.Task.Run(() =>
                {
                    player.TriggerEvent("Client:CharacterCameraOn");
                }, delayTime: 500);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", "[ShowMaskMenu]: " + e.ToString());
            }
        }

        public static void ShowClothMenu(Player player)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                TempData tempData = Helper.GetCharacterTempData(player);
                if (player.GetData<bool>("Player:InShop") == true) return;
                Business bizz = Business.GetClosestBusiness(player, 55.5f);
                if (bizz != null)
                {
                    if (bizz.nobuy == true)
                    {
                        Helper.SendNotificationWithoutButton(player, "Du kannst jetzt hier nichts kaufen!", "error");
                        return;
                    }
                    if (bizz.products < 35)
                    {
                        Helper.SendNotificationWithoutButton(player, "Unsere Lager sind leider leer!", "error");
                        return;
                    }
                    if (bizz.id == 1)
                    {
                        Helper.SetPlayerPosition(player, new Vector3(71.30064, -1387.6963, 29.376078));
                        player.Heading = 165.48781f;
                        player.Dimension = (uint)(player.Id + 5);
                    }
                    else if (bizz.id == 2)
                    {
                        Helper.SetPlayerPosition(player, new Vector3(-1182.5214, -765.30554, 17.326475));
                        player.Heading = 119.63973f;
                        player.Dimension = (uint)(player.Id + 5);
                    }
                    else if (bizz.id == 3)
                    {
                        Helper.SetPlayerPosition(player, new Vector3(-705.8955, -151.22855, 37.41514));
                        player.Heading = -88.051735f;
                        player.Dimension = (uint)(player.Id + 5);
                    }
                    else if (bizz.id == 4)
                    {
                        Helper.SetPlayerPosition(player, new Vector3(-3178.1228, 1036.7129, 20.863214));
                        player.Heading = -29.661612f;
                        player.Dimension = (uint)(player.Id + 5);
                    }
                    player.SetData<bool>("Player:InShop", true);
                    player.TriggerEvent("Client:PlayerFreeze", true);
                    JObject obj = null;
                    if (character.factionduty == false)
                    {
                        obj = JObject.Parse(character.json);
                    }
                    else
                    {
                        obj = JObject.Parse(character.dutyjson);
                    }
                    player.TriggerEvent("Client:ShowClothMenu", NAPI.Util.ToJson(obj["clothing"]), NAPI.Util.ToJson(obj["clothingColor"]), character.gender, bizz.multiplier);
                    NAPI.Task.Run(() =>
                    {
                        player.TriggerEvent("Client:CharacterCameraOn");
                    }, delayTime: 500);
                }
                else
                {
                    Helper.SendNotificationWithoutButton(player, "Wir verkaufen hier nichts!", "error");
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", "[ShowClothMenu]: " + e.ToString());
            }
        }

        public static void ManageBizzCash(Business bizz, int getmoney, bool noproducts = false)
        {
            if (bizz == null) return;
            bizz.funds++;
            if (noproducts == false)
            {
                bizz.products -= (getmoney / 30) / 4;
            }
            if (bizz.products <= 0)
            {
                bizz.products = 0;
            }
            float govvalue = 1;
            if (noproducts == false || bizz.id == 50 || bizz.id == 51)
            {
                if (bizz.id == 50 || bizz.id == 51)
                {
                    bizz.govcash += Convert.ToInt32(govvalue / 2);
                }
                else
                {
                    govvalue = ((getmoney / 100) * Helper.adminSettings.gsteuer);
                    if (govvalue <= 0)
                    {
                        govvalue = 1;
                    }
                    bizz.govcash += Convert.ToInt32(govvalue);
                }
            }
            bizz.cash += getmoney;
            bizz.cash -= Convert.ToInt32(govvalue);
        }

        [RemoteEvent("Server:StartBizz")]
        public static void OnStartBizz(Player player, bool updatehud = false)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                Account account = Helper.GetAccountData(player);

                if (character == null) return;

                Business bizz;
                bizz = Business.GetClosestBusiness(player);
                if (bizz != null && Business.HasPlayerBusinessKey(player, bizz.id))
                {
                    BusinessLoad bizzLoad = new BusinessLoad();
                    bizzLoad.id = bizz.id;
                    bizzLoad.posx = bizz.position.X;
                    bizzLoad.posy = bizz.position.Y;
                    bizzLoad.posz = bizz.position.Z;
                    bizzLoad.price = bizz.price;
                    bizzLoad.name = bizz.name;
                    bizzLoad.name2 = bizz.name2;
                    bizzLoad.owner = bizz.owner;
                    bizzLoad.funds = bizz.funds;
                    bizzLoad.products = bizz.products;
                    bizzLoad.multiplier = bizz.multiplier;
                    bizzLoad.cash = bizz.cash;
                    bizzLoad.govcash = bizz.govcash;
                    bizzLoad.prodprice = bizz.prodprice;
                    bizzLoad.blipid = bizz.blipid;
                    bizzLoad.buyproducts = bizz.buyproducts;
                    bizzLoad.selled = bizz.selled;
                    bizzLoad.getmoney = bizz.getmoney;
                    bizzLoad.elec = bizz.elec;
                    bizzLoad.elecname = Business.GetBusinessById(bizz.elec) != null ? Business.GetBusinessById(bizz.elec).name : "Kein Strombieter";

                    if (updatehud == false)
                    {
                        player.TriggerEvent("Client:ShowBizzStats", NAPI.Util.ToJson(bizzLoad), character.name, 1);
                    }
                    else
                    {
                        player.TriggerEvent("Client:ShowBizzStats", NAPI.Util.ToJson(bizzLoad), character.name, 0);
                    }
                    NAPI.Task.Run(() =>
                    {
                        bizzLoad = null;
                    }, delayTime: 515);
                }
                else
                {
                    Helper.SendNotificationWithoutButton2(player, "Du bist nicht in oder in der Nähe von einem deiner Businesse!", "error", "center");
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnStartBizz]: " + e);
            }
        }

        public static void BizzInActiveCheck()
        {
            try
            {
                int id;
                int lastonline;
                bool inactive;
                foreach (Business bizz in businessList)
                {
                    id = -1;
                    lastonline = 0;
                    inactive = false;
                    if (bizz != null && bizz.owner != "n/A" && bizz.price > 0)
                    {
                        MySqlCommand command = General.Connection.CreateCommand();
                        command.CommandText = "SELECT id,lastonline FROM characters WHERE name = @name LIMIT 1";
                        command.Parameters.AddWithValue("@name", bizz.owner);

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
                            if ((lastonline + (31 * 86400)) < Helper.UnixTimestamp())
                            {
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
                                Helper.CreateAdminLog($"bizzlog", $"Das Business {bizz.id} wurde durch den Inaktivitätscheck verkauft!");
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[BizzInActiveCheck]: " + e.ToString());
            }
        }

        //BizzSettings
        [RemoteEvent("Server:BizzSettings")]
        public static void OnBizzSettings(Player player, string setting, string value)
        {
            try
            {
                Business bizz = null;
                int number = 0;
                Character character = Helper.GetCharacterData(player);
                TempData tempData = Helper.GetCharacterTempData(player);
                bizz = GetClosestBusiness(player);
                if (bizz != null)
                {
                    switch (setting.ToLower())
                    {
                        case "insert":
                            {
                                player.TriggerEvent("Client:CallInput", "Businesskasse", "Wieviel Dollar möchtest du reinlegen?", "text", "500", 7, "BizzInsert");
                                break;
                            }
                        case "cashout":
                            {
                                player.TriggerEvent("Client:CallInput", "Businesskasse", "Wieviel Dollar möchtest du rausnehmen?", "text", "500", 7, "BizzCashOut");
                                break;
                            }
                        case "newbizzdoor":
                            {
                                if (bizz.owner != character.name)
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Du bist nicht der Besitzer von diesem Business!", "error", "center");
                                    return;
                                }
                                if (character.cash < 1250)
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Du hast nicht genügend Geld!", "error", "center");
                                    return;
                                }
                                CharacterController.SetMoney(player, -1250);
                                DeleteAllKeys(bizz.id, character.name);
                                Helper.SendNotificationWithoutButton2(player, "Du hast das Schloss auswechseln lassen!", "success", "center", 3500);
                                break;
                            }
                        case "newbizzkey":
                            {
                                if (bizz.owner != character.name)
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Du bist nicht der Besitzer von diesem Business!", "error", "center");
                                    return;
                                }
                                if (character.cash < 250)
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Du hast nicht genügend Geld!", "error", "center");
                                    return;
                                }
                                Items newitem = ItemsController.CreateNewItem(player, character.id, "Bizzschlüssel", "Player", 1, ItemsController.GetFreeItemID(player));
                                if (!ItemsController.CanPlayerHoldItem(player, newitem.weight))
                                {
                                    newitem = null;
                                    Helper.SendNotificationWithoutButton(player, "Du hast keinen Platz mehr im Inventar für den Bizzschlüssel!", "error", "top-end");
                                    return;
                                }
                                if (newitem != null)
                                {
                                    CharacterController.SetMoney(player, -250);
                                    newitem.props = "Bizzschlüssel: " + bizz.id;
                                    tempData.itemlist.Add(newitem);
                                    Helper.SendNotificationWithoutButton2(player, "Du hast einen Bizzschlüssel nachmachen lassen!", "success", "center", 3500);
                                }
                                else
                                {
                                    Helper.SendNotificationWithoutButton(player, "Es konnte kein Bizzschlüssel nachgemacht werden!", "error", "top-end");
                                }
                                break;
                            }
                        case "bizzname":
                            {
                                if (value == "n/A" || value.Length < 5 || value.Length > 64)
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Ungültiger Businessname!", "error", "center");
                                    return;
                                }
                                bizz.name = value;
                                SetBusinessHandle(bizz);
                                Helper.SendNotificationWithoutButton2(player, "Neuer Business Name wurde gesetzt!", "success", "center");
                                break;
                            }
                        case "getmoney":
                            {
                                number = Convert.ToInt32(value);
                                if (number < 7500 || number > 999999999)
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Ungültiges Limit, mind. 7500$ max. 999999999$!", "error", "center");
                                    return;
                                }
                                bizz.getmoney = number;
                                Helper.SendNotificationWithoutButton2(player, "Geldabholungs Limit wurde gesetzt!", "success", "center");
                                break;
                            }
                        case "bizzowner":
                            {
                                if (bizz.owner != character.name)
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Du bist nicht der Besitzer von diesem Business!", "error", "center");
                                    return;
                                }
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
                                bizz.owner = value;
                                SetBusinessHandle(bizz);
                                Helper.SendNotificationWithoutButton2(player, "Neuer Eigentümer wurde gesetzt, den Schlüssel kannst du über das Inventar vergeben!", "success", "center");
                                break;
                            }
                        case "produktpreis":
                            {
                                number = Convert.ToInt32(value);
                                if (number < 30 || number > 1250)
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Der Produktpreis muss zwischen 30$ und 1250$ liegen!", "error", "center");
                                    return;
                                }
                                bizz.prodprice = number;
                                Helper.SendNotificationWithoutButton2(player, "Der Produktpreis wurde angepasst!", "success", "center");
                                break;
                            }
                        case "produktstatus":
                            {
                                if (bizz.buyproducts == 0)
                                {
                                    bizz.buyproducts = 1;
                                }
                                else
                                {
                                    bizz.buyproducts = 0;
                                }
                                Helper.SendNotificationWithoutButton2(player, "Der Produktstatus wurde angepasst!", "success", "center");
                                break;
                            }
                        case "multiplier":
                            {
                                float number2 = float.Parse(value, CultureInfo.InvariantCulture);
                                if (bizz.id == 42 || bizz.id == 43)
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Der Preismultiplizierer kann für dieses Business nicht eingestellt werden!", "error", "center");
                                    return;
                                }
                                if (number2 < 1.0f || number2 > 3.0f)
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Der Preismultiplizierer muss zwischen 1.0 und 3.0 liegen!", "error", "center");
                                    return;
                                }
                                bizz.multiplier = number2;
                                Helper.SendNotificationWithoutButton2(player, "Der Preismultiplizierer wurde angepasst!", "success", "center");
                                foreach (Player p in NAPI.Pools.GetAllPlayers())
                                {
                                    if (p != null && p.GetOwnSharedData<bool>("Player:Spawned") == true)
                                    {
                                        Helper.SyncThings(p);
                                    }
                                }
                                break;
                            }
                        case "sellbizz":
                            {
                                if (bizz.owner != character.name)
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Du bist nicht der Besitzer von diesem Business!", "error", "center");
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
                                Helper.SendNotificationWithoutButton2(player, $"Du hast das Business für {bizz.price / 2}$ verkauft!", "success", "center", 4500);
                                CharacterController.SetMoney(player, (bizz.price / 2));
                                CharacterController.SaveCharacter(player);
                                break;
                            }
                    }
                    if (bizz != null)
                    {
                        SaveBusiness(bizz);
                    }
                    if (setting != "sellbizz")
                    {
                        OnStartBizz(player, true);
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnBizzSettings]: " + e);
            }
        }


        [RemoteEvent("Server:HideClothMenu")]
        public static void OnHideClothMenu(Player player, bool setcloth = true, bool faction = false)
        {
            Character character = Helper.GetCharacterData(player);
            TempData tempData = Helper.GetCharacterTempData(player);
            try
            {
                bool unset = false;

                string[] clothingArray = new string[8];
                clothingArray = character.clothing.Split(",");

                if (faction == true && character.factionduty == false)
                {
                    Helper.SendNotificationWithoutButton(player, $"Du bist nicht im Dienst!", "error", "top-left", 2500);
                    return;
                }

                if (Helper.IsInRangeOfPoint(player.Position, new Vector3(-622.2842, -229.88474, 38.057053), 65.0f))
                {
                    player.TriggerEvent("Client:ShowJuweMenu", "null", "null", 0);
                    clothingArray[6] = "1";
                }
                else if (Helper.IsInRangeOfPoint(player.Position, new Vector3(-1579.3323, -951.5237, 13.017388), 65.0f))
                {
                    player.TriggerEvent("Client:ShowMaskMenu", "null", "null", 0);
                    clothingArray[7] = "1";
                }
                else
                {
                    Business bizz = Business.GetClosestBusiness(player, 45.5f);
                    if (faction == false && bizz != null && tempData.adminduty == false && !Helper.IsInRangeOfPoint(player.Position, new Vector3(471.16223, -988.9328, 25.734646), 12.5f) && !Helper.IsInRangeOfPoint(player.Position, new Vector3(629.6149, 3.6072264, 76.628044), 12.5f) && !Helper.IsInRangeOfPoint(player.Position, new Vector3(624.4518, -3.4003117, 76.628136), 12.5f) && !Helper.IsInRangeOfPoint(player.Position, new Vector3(624.4518, -3.4003117, 76.628136), 7.5f) && !Helper.IsInRangeOfPoint(player.Position, new Vector3(-663.262, 321.58627, 92.74433), 12.5f) && !Helper.IsInRangeOfPoint(player.Position, new Vector3(-339.501, -161.42168, 44.5875), 12.5f))
                    {
                        player.TriggerEvent("Client:ShowClothMenu", "null", "null", 0);
                    }
                    else
                    {
                        player.TriggerEvent("Client:ShowFactionClothing", "null", "null", 0, 0, "null");
                    }
                    clothingArray[0] = "1";
                    clothingArray[1] = "1";
                    clothingArray[2] = "1";
                    clothingArray[3] = "1";
                    clothingArray[4] = "1";
                    clothingArray[5] = "1";
                    clothingArray[6] = "1";
                }
                character.clothing = String.Join(",", clothingArray);

                player.Dimension = 0;
                player.TriggerEvent("Client:CharacterCameraOff");
                player.SetData<bool>("Player:InShop", false);

                JObject obj = null;

                if (faction == true)
                {
                    character.factionduty = false;
                    Helper.SendNotificationWithoutButton(player, $"Dienst beendet!", "success", "top-left", 2500);

                    Items radio = ItemsController.GetItemByItemName(player, "Funkgerät");
                    if (radio != null)
                    {
                        ItemsController.RemoveItem(player, radio.itemid);
                    }

                    if (tempData.undercover != "")
                    {
                        tempData.undercover = "";
                        character.name = player.GetSharedData<string>("Client:OldName");
                        player.Name = character.name;
                        player.SetSharedData("Client:OldName", "n/A");
                    }

                    obj = JObject.Parse(character.json);

                    CharacterController.SetCharacterCloths(player, obj, character.clothing);

                    unset = true;
                }

                if (setcloth == true && unset == false)
                {
                    if ((!Helper.IsInRangeOfPoint(player.Position, new Vector3(471.16223, -988.9328, 25.734646), 7.5f) && !Helper.IsInRangeOfPoint(player.Position, new Vector3(629.6149, 3.6072264, 76.628044), 7.5f) && !Helper.IsInRangeOfPoint(player.Position, new Vector3(624.4518, -3.4003117, 76.628136), 7.5f) && !Helper.IsInRangeOfPoint(player.Position, new Vector3(-663.262, 321.58627, 92.74433), 7.5f) && !Helper.IsInRangeOfPoint(player.Position, new Vector3(-339.501, -161.42168, 44.5875), 7.5f)) || character.factionduty == false)
                    {
                        if (character.factionduty == false)
                        {
                            obj = JObject.Parse(character.json);
                        }
                        else
                        {
                            obj = JObject.Parse(character.dutyjson);
                        }
                        CharacterController.SetCharacterCloths(player, obj, character.clothing);
                    }
                    else
                    {
                        obj = JObject.Parse(character.dutyjson);

                        CharacterController.SetCharacterCloths(player, obj, character.clothing);
                    }
                }

                player.TriggerEvent("Client:PlayerFreeze", false);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", "[OnHideClothMenu]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:BuyNewCloths")]
        public static void OnBuyNewCloths(Player player, string json1, string json2, int value, bool faction = false)
        {
            Character character = Helper.GetCharacterData(player);
            TempData tempData = Helper.GetCharacterTempData(player);
            try
            {

                if (faction == true && (character.faction == 1 || character.faction == 2 || character.faction == 3 || tempData.adminduty == true))
                {
                    if (character.factionduty == false && tempData.adminduty == false)
                    {
                        Helper.SendNotificationWithoutButton(player, $"Dienst begonnen, vergesse nicht wichtige Fraktionsutensilien auszurüsten!", "success");
                        character.factionduty = true;
                        Items radio = ItemsController.GetItemByItemName(player, "Funkgerät");
                        if (radio == null)
                        {
                            Items newitem = ItemsController.CreateNewItem(player, character.id, "Funkgerät", "Player", 1, ItemsController.GetFreeItemID(player));
                            if (newitem != null && ItemsController.CanPlayerHoldItem(player, newitem.weight))
                            {
                                tempData.itemlist.Add(newitem);
                            }
                        }
                    }
                    else
                    {
                        if (tempData.adminduty == false)
                        {
                            Helper.SendNotificationWithoutButton(player, $"Dienstoutfit gewechselt!", "success");
                        }
                        else
                        {
                            Helper.SendNotificationWithoutButton(player, $"Testoutfit gesetzt!", "success");
                        }
                    }
                    if (tempData.adminduty == false)
                    {
                        CharacterController.SaveCharacter(player);
                    }
                    OnHideClothMenu(player, false);
                    return;
                }

                if (faction == false)
                {
                    if (value <= 0 || value > 25000)
                    {
                        Helper.SendNotificationWithoutButton(player, "Du hast keine neuen Kleidungsteile ausgewählt!", "error");
                        return;
                    }
                    if (faction == false && character.cash < value)
                    {
                        Helper.SendNotificationWithoutButton(player, $"Du hast nicht genügend Geld dabei - {value}$!", "error");
                        return;
                    }
                }

                json1 = json1.Substring(1);
                json1 = json1.Substring(0, json1.Length - 1);

                json2 = json2.Substring(1);
                json2 = json2.Substring(0, json2.Length - 1);

                string[] clothArray = new string[13];
                clothArray = json1.Split(",");

                string[] clothColorArray = new string[13];
                clothColorArray = json2.Split(",");

                JObject obj = null;

                if (faction == false && character.factionduty == false)
                {
                    obj = JObject.Parse(character.json);
                }
                else
                {
                    obj = JObject.Parse(character.dutyjson);
                }

                for (int i = 0; i < clothArray.Length; i++)
                {
                    obj["clothing"][i] = Convert.ToInt32(clothArray[i]);
                }

                obj["clothingColor"][1] = 0;
                for (int i = 0; i < clothColorArray.Length; i++)
                {
                    if (i == 1) continue;
                    obj["clothingColor"][i] = Convert.ToInt32(clothColorArray[i]);
                }

                string[] clothingArray = new string[8];
                clothingArray = character.clothing.Split(",");

                if (Helper.IsInRangeOfPoint(player.Position, new Vector3(-622.2842, -229.88474, 38.057053), 65.0f))
                {
                    Helper.SendNotificationWithoutButton(player, $"Accessoires für {value}$ erworben!", "success");
                    clothingArray[6] = "1";
                }
                else if (Helper.IsInRangeOfPoint(player.Position, new Vector3(-1579.3323, -951.5237, 13.017388), 65.0f))
                {
                    Helper.SendNotificationWithoutButton(player, $"Maske für {value}$ erworben!", "success");
                    clothingArray[7] = "1";
                }
                else
                {
                    clothingArray[0] = "1";
                    clothingArray[1] = "1";
                    clothingArray[2] = "1";
                    clothingArray[3] = "1";
                    clothingArray[4] = "1";
                    clothingArray[5] = "1";
                    clothingArray[6] = "1";
                    if (faction == false)
                    {
                        Helper.SendNotificationWithoutButton(player, $"Outfit für {value}$ erworben!", "success");
                    }
                    else
                    {
                        if (character.factionduty == false)
                        {
                            Helper.SendNotificationWithoutButton(player, $"Dienst begonnen!", "success");
                            character.factionduty = true;
                            Items radio = ItemsController.GetItemByItemName(player, "Funkgerät");
                            if (radio == null)
                            {
                                Items newitem = ItemsController.CreateNewItem(player, character.id, "Funkgerät", "Player", 1, ItemsController.GetFreeItemID(player));
                                if (newitem != null && ItemsController.CanPlayerHoldItem(player, newitem.weight))
                                {
                                    tempData.itemlist.Add(newitem);
                                }
                            }
                        }
                        else
                        {
                            if (tempData.adminduty == false)
                            {
                                Helper.SendNotificationWithoutButton(player, $"Dienstoutfit gewechselt!", "success");
                            }
                            else
                            {
                                Helper.SendNotificationWithoutButton(player, $"Testoutfit gesetzt!", "success");
                            }
                        }
                    }
                }

                character.clothing = String.Join(",", clothingArray);

                if (faction == true)
                {
                    if (tempData.adminduty == false)
                    {
                        character.dutyjson = NAPI.Util.ToJson(obj);
                    }

                    CharacterController.SetCharacterCloths(player, obj, character.clothing);
                }
                else
                {

                    CharacterController.SetMoney(player, -value);

                    Business bizz = Business.GetClosestBusiness(player, 25.5f);
                    if (bizz != null)
                    {
                        ManageBizzCash(bizz, value);
                    }

                    if (tempData.adminduty == false)
                    {
                        if (character.factionduty == false)
                        {
                            character.json = NAPI.Util.ToJson(obj);
                        }
                        else
                        {
                            character.dutyjson = NAPI.Util.ToJson(obj);
                        }
                    }

                    CharacterController.SetCharacterCloths(player, obj, character.clothing);
                }
                if (tempData.adminduty == false)
                {
                    CharacterController.SaveCharacter(player);
                }
                OnHideClothMenu(player, false);
            }
            catch (Exception e)
            {
                Helper.SendNotificationWithoutButton(player, $"Ungültige Kleidungsauswahl!", "error");
                Helper.ConsoleLog("error", "[OnBuyNewCloths]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:HideMaskMenu")]
        public static void OnHideMaskMenu(Player player)
        {
            try
            {
                NAPI.Player.SetPlayerClothes(player, 1, 0, 0);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", "[OnHideMaskMenu]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:BuyNewHairs")]
        public static void OnBuyNewHairs(Player player, int id, int color, int id2, int color2, int color3, int price, int overlay0, int overlaycolor0, int overlay1, int overlaycolor1, int overlay2, int overlaycolor2)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                if (player.GetData<bool>("Player:InShop") == false || character == null) return;

                if (character.cash < price && id != -1 && id2 != -1)
                {
                    Helper.SendNotificationWithoutButton(player, $"Du hast nicht genügend Geld dabei - {price}$!", "error");
                    return;
                }

                if (id == -1 && id2 == -1)
                {
                    Helper.ShowBarberShop(player, true);
                    return;
                }

                JObject obj = JObject.Parse(character.json);

                obj["hair"][0] = id;
                obj["hair"][1] = (byte)color;
                obj["hair"][2] = (byte)color3;

                NAPI.Player.SetPlayerClothes(player, 2, (int)obj["hair"][0], 0);
                NAPI.Player.SetPlayerHairColor(player, (byte)obj["hair"][1], (byte)obj["hair"][2]);

                HeadOverlay headoverlay = new HeadOverlay();
                obj["beard"][0] = (byte)id2;
                obj["beard"][1] = (byte)color2;
                headoverlay.Index = (byte)obj["beard"][0];
                headoverlay.Opacity = 1.0f;
                headoverlay.Color = (byte)obj["beard"][1];
                headoverlay.SecondaryColor = (byte)obj["beard"][1];
                NAPI.Player.SetPlayerHeadOverlay(player, 1, headoverlay);

                obj["headOverlays"][2] = (byte)overlay0;
                obj["headOverlaysColors"][2] = (byte)overlaycolor0;
                obj["headOverlays"][4] = (byte)overlay1;
                obj["headOverlaysColors"][4] = (byte)overlaycolor1;
                obj["headOverlays"][8] = (byte)overlay2;
                obj["headOverlaysColors"][8] = (byte)overlaycolor2;

                headoverlay = new HeadOverlay();
                headoverlay.Index = (byte)obj["headOverlays"][2];
                headoverlay.Opacity = 1.0f;
                headoverlay.Color = (byte)obj["headOverlaysColors"][2];
                headoverlay.SecondaryColor = (byte)obj["headOverlaysColors"][2];
                NAPI.Player.SetPlayerHeadOverlay(player, 2, headoverlay);

                headoverlay = new HeadOverlay();
                headoverlay.Index = (byte)obj["headOverlays"][4];
                headoverlay.Opacity = 1.0f;
                headoverlay.Color = (byte)obj["headOverlaysColors"][4];
                headoverlay.SecondaryColor = (byte)obj["headOverlaysColors"][4];
                NAPI.Player.SetPlayerHeadOverlay(player, 4, headoverlay);

                headoverlay = new HeadOverlay();
                headoverlay.Index = (byte)obj["headOverlays"][8];
                headoverlay.Opacity = 1.0f;
                headoverlay.Color = (byte)obj["headOverlaysColors"][8];
                headoverlay.SecondaryColor = (byte)obj["headOverlaysColors"][8];
                NAPI.Player.SetPlayerHeadOverlay(player, 8, headoverlay);

                player.TriggerEvent("hairOverlay::update", player, (int)obj["hair"][0]);

                Helper.SendNotificationWithoutButton(player, $"Dienstleistung für {price}$ durchgeführt!", "success");

                CharacterController.SetMoney(player, -price);

                Business bizz = Business.GetClosestBusiness(player, 25.5f);
                if (bizz != null)
                {
                    ManageBizzCash(bizz, price);
                }

                character.json = NAPI.Util.ToJson(obj);

                CharacterController.SaveCharacter(player);

                Helper.ShowBarberShop(player, true);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", "[OnBuyNewHairs]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:BuyTattooAfter")]
        public static void OnBuyTattooAfter(Player player)
        {
            TempData tempData = Helper.GetCharacterTempData(player);
            Character character = Helper.GetCharacterData(player);
            if (tempData == null || character == null) return;
            JObject obj;
            obj = JObject.Parse(character.json);
            player.Dimension = 0;
            NAPI.Player.SetPlayerClothes(player, 2, (int)obj["hair"][0], 0);
            NAPI.Player.SetPlayerHairColor(player, (byte)obj["hair"][1], (byte)obj["hair"][2]);
            player.TriggerEvent("hairOverlay::update", player, (int)obj["hair"][0]);
            player.TriggerEvent("Client:HideTattoShop");
            NAPI.Task.Run(() =>
            {
                Helper.GetCharacterTattoos(player, character.id);
                NAPI.Task.Run(() =>
                {
                    CharacterController.SetCharacterCloths(player, obj, character.clothing);
                }, delayTime: 155);
            }, delayTime: 155);
        }

        [RemoteEvent("Server:BuyTattoo")]
        public static void OnBuyTattoo(Player player, string name, string dlcName, int zoneid)
        {
            try
            {
                if (zoneid == -1) return;

                TempData tempData = Helper.GetCharacterTempData(player);
                Character character = Helper.GetCharacterData(player);
                if (tempData == null || character == null) return;
                MySqlCommand command = General.Connection.CreateCommand();
                Business bizz = GetClosestBusiness(player, 55.5f);
                if (bizz == null && zoneid != -2) return;
                int price = 0;
                if (zoneid != -2)
                {
                    price = (int)(750 * bizz.multiplier);
                }
                int lastid = 0;
                Tattoos tattooFound = null;

                if (character.cash < price && zoneid != -2)
                {
                    Helper.SendNotificationWithoutButton(player, $"Du hast nicht genügend Geld dabei - {price}$!", "error");
                    return;
                }

                player.SetData<bool>("Player:InShop", false);

                if (zoneid != -2)
                {
                    Tattoos tattoo = new Tattoos();
                    tattoo.name = name;
                    tattoo.dlcname = dlcName;
                    tattoo.zoneid = zoneid;

                    foreach (Tattoos tattooTemp in tempData.tattoos)
                    {
                        if (tattooTemp.name == name && tattooTemp.dlcname == dlcName)
                        {
                            tattooFound = tattooTemp;
                            break;
                        }
                    }

                    Decoration decoration = new Decoration();
                    if (tattooFound != null)
                    {
                        NAPI.Player.ClearPlayerDecorations(player);
                        decoration.Collection = NAPI.Util.GetHashKey(dlcName);
                        decoration.Overlay = NAPI.Util.GetHashKey(name);
                        tempData.tattoos.Remove(tattooFound);
                        command.CommandText = "DELETE FROM tattoos WHERE characterid=@characterid AND name=@name AND dlcname=@dlcname";
                        command.Parameters.AddWithValue("@characterid", character.id);
                        command.Parameters.AddWithValue("@name", name);
                        command.Parameters.AddWithValue("@dlcname", dlcName);
                        command.ExecuteNonQuery();
                        Helper.SendNotificationWithoutButton(player, $"Du hast dir für {price}$ ein Tattoo entfernen lassen!", "success", "top-left", 3750);
                    }
                    else
                    {
                        if (tempData.tattoos.Count >= 10)
                        {
                            Helper.SendNotificationWithoutButton(player, $"Du kannst nur max. 10 Tattoos besitzen!", "error", "top-left", 3750);
                            return;
                        }

                        command.CommandText = "INSERT INTO tattoos (characterid, name, dlcname, zoneid) VALUES (@characterid, @name, @dlcname, @zoneid)";
                        command.Parameters.AddWithValue("@characterid", character.id);
                        command.Parameters.AddWithValue("@name", name);
                        command.Parameters.AddWithValue("@dlcname", dlcName);
                        command.Parameters.AddWithValue("@zoneid", zoneid);
                        command.ExecuteNonQuery();
                        lastid = (int)command.LastInsertedId;

                        tattoo.id = lastid;

                        decoration.Collection = NAPI.Util.GetHashKey(dlcName);
                        decoration.Overlay = NAPI.Util.GetHashKey(name);

                        tempData.tattoos.Add(tattoo);

                        Helper.SendNotificationWithoutButton(player, $"Du hast dir für {price}$ ein neues Tattoo stechen lassen!", "success", "top-left", 3750);
                    }


                    if (bizz != null)
                    {
                        ManageBizzCash(bizz, price);
                    }

                    CharacterController.SetMoney(player, -price);
                }

                if (zoneid != -2)
                {
                    player.TriggerEvent("Client:BuyTattooAfter", NAPI.Util.ToJson(tempData.tattoos));
                }

                if (zoneid == -2)
                {
                    JObject obj;
                    obj = JObject.Parse(character.json);
                    player.Dimension = 0;
                    NAPI.Player.SetPlayerClothes(player, 2, (int)obj["hair"][0], 0);
                    NAPI.Player.SetPlayerHairColor(player, (byte)obj["hair"][1], (byte)obj["hair"][2]);
                    player.TriggerEvent("hairOverlay::update", player, (int)obj["hair"][0]);
                    player.TriggerEvent("Client:HideTattoShop");
                    NAPI.Task.Run(() =>
                    {
                        Helper.GetCharacterTattoos(player, character.id);
                        NAPI.Task.Run(() =>
                        {
                            CharacterController.SetCharacterCloths(player, obj, character.clothing);
                        }, delayTime: 155);
                    }, delayTime: 155);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", "[OnBuyTattoo]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:ResetAllTattoos")]
        public static void OnResetAllTattoos(Player player)
        {
            try
            {
                TempData tempData = Helper.GetCharacterTempData(player);
                Character character = Helper.GetCharacterData(player);
                if (tempData == null || character == null) return;
                Business bizz = GetClosestBusiness(player, 55.5f);
                if (bizz == null) return;
                int price = (int)(1250 * bizz.multiplier);

                if (tempData.tattoos.Count <= 0)
                {
                    Helper.SendNotificationWithoutButton(player, $"Du hast keine Tattoos!", "error");
                    return;
                }

                if (character.cash < price)
                {
                    Helper.SendNotificationWithoutButton(player, $"Du hast nicht genügend Geld dabei - {price}$!", "error");
                    return;
                }

                player.SetData<bool>("Player:InShop", false);

                Decoration decoration = new Decoration();

                PetaPoco.Database db = new PetaPoco.Database(General.Connection);

                foreach (Tattoos tattooTemp in tempData.tattoos.ToList())
                {
                    decoration.Collection = NAPI.Util.GetHashKey(tattooTemp.dlcname);
                    decoration.Overlay = NAPI.Util.GetHashKey(tattooTemp.name);
                    player.RemoveDecoration(decoration);
                    tempData.tattoos.Remove(tattooTemp);
                    db.Delete(tattooTemp);
                }

                tempData.tattoos = new List<Tattoos>();

                Helper.SendNotificationWithoutButton(player, $"Du hast dir alle Tattoos für {price}$ entfernen lassen!", "success", "top-left", 3750);


                if (bizz != null)
                {
                    ManageBizzCash(bizz, price);
                }

                CharacterController.SetMoney(player, -price);

                JObject obj;
                obj = JObject.Parse(character.json);
                player.Dimension = 0;
                NAPI.Player.SetPlayerClothes(player, 2, (int)obj["hair"][0], 0);
                NAPI.Player.SetPlayerHairColor(player, (byte)obj["hair"][1], (byte)obj["hair"][2]);
                player.TriggerEvent("hairOverlay::update", player, (int)obj["hair"][0]);
                player.TriggerEvent("Client:HideTattoShop");
                NAPI.Task.Run(() =>
                {
                    NAPI.Task.Run(() =>
                    {
                        CharacterController.SetCharacterCloths(player, obj, character.clothing);
                    }, delayTime: 155);
                }, delayTime: 155);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", "[OnResetAllTattoos]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:ResetAllTattoos2")]
        public static void OnResetAllTattoos2(Player player)
        {
            try
            {
                TempData tempData = Helper.GetCharacterTempData(player);
                Character character = Helper.GetCharacterData(player);
                if (tempData == null || character == null) return;
                Business bizz = GetClosestBusiness(player, 25.5f);
                if (bizz == null) return;

                player.ClearDecorations();

                JObject obj = JObject.Parse(character.json);

                CharacterController.SetCharacterJson(player, obj, character.clothing);

                player.Dimension = 0;

                NAPI.Task.Run(() =>
                {
                    if (tempData.tattoos.Count > 0)
                    {
                        Decoration decoration = new Decoration();
                        foreach (Tattoos tattooTemp in tempData.tattoos.ToList())
                        {
                            if (tattooTemp != null)
                            {
                                decoration.Collection = NAPI.Util.GetHashKey(tattooTemp.dlcname);
                                decoration.Overlay = NAPI.Util.GetHashKey(tattooTemp.name);
                                player.SetDecoration(decoration);
                            }
                        }
                    }
                }, delayTime: 255);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", "[OnResetAllTattoos2]: " + e.ToString());
            }
        }
    }
}
