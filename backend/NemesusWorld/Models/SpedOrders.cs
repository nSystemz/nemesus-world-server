using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using NemesusWorld.Utils;

namespace NemesusWorld.Models
{
    class SpedOrders : Script
    {
        public static List<SpedOrders> spedOrderListAll = new List<SpedOrders>();
        public static List<SpedOrders> spedOrderList = new List<SpedOrders>();
        public int id { get; set; }
        public int capa { get; set; }
        public float dist { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string what { get; set; }
        public Vector3 position1 { get; set; }
        public Vector3 position2 { get; set; }

        public SpedOrders()
        {
            id = 0;
            from = "n/A";
            to = "n/A";
            what = "n/A";
            position1 = null;
            position2 = null;
        }

        public static void CreateNewSpedOrder()
        {
            try
            {
                Random rand = new Random(spedOrderListAll.Count);
                spedOrderList.Add(spedOrderListAll[rand.Next(spedOrderListAll.Count)]);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", "[CreateNewSpedOrder]: " + e.ToString());
            }
        }

        public static SpedOrders GetSpedOrderById(int id)
        {
            try
            {
                foreach (SpedOrders spedOrder in spedOrderList)
                {
                    if (spedOrder != null && spedOrder.id == id)
                    {
                        return spedOrder;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetSpedOrderById]: " + e.ToString());
            }
            return null;
        }

        public static void CreateSpedOrders()
        {
            try
            {
                SpedOrders spedOrders = new SpedOrders();
                spedOrders.id = 1;
                spedOrders.capa = 175;
                spedOrders.from = "Kleidungsstücke Lagerhalle";
                spedOrders.to = "Strawberry Kleidungsladen";
                spedOrders.what = "Neue Kleidungsstücke";
                spedOrders.position1 = new Vector3(854.40594, -2094.9443, 30.074247);
                spedOrders.position2 = new Vector3(86.046455, -1396.5159, 29.036547);
                spedOrders.dist = spedOrders.position1.DistanceTo(spedOrders.position2);
                spedOrderListAll.Add(spedOrders);

                SpedOrders spedOrders2 = new SpedOrders();
                spedOrders2.id = 2;
                spedOrders2.capa = 175;
                spedOrders2.from = "Allgemeine Lagerhalle";
                spedOrders2.to = "Strawberry Kleidungsladen";
                spedOrders2.what = "Beleuchtung";
                spedOrders2.position1 = new Vector3(1179.7677, -3298.4373, 5.458104);
                spedOrders2.position2 = new Vector3(86.046455, -1396.5159, 29.036547);
                spedOrders2.dist = spedOrders2.position1.DistanceTo(spedOrders2.position2);
                spedOrderListAll.Add(spedOrders2);

                SpedOrders spedOrders3 = new SpedOrders();
                spedOrders3.id = 3;
                spedOrders3.capa = 175;
                spedOrders3.from = "Allgemeine Lagerhalle";
                spedOrders3.to = "Strawberry Kleidungsladen";
                spedOrders3.what = "Regale";
                spedOrders3.position1 = new Vector3(1244.2648, -3142.279, 5.390594);
                spedOrders3.position2 = new Vector3(86.046455, -1396.5159, 29.036547);
                spedOrders3.dist = spedOrders3.position1.DistanceTo(spedOrders3.position2);
                spedOrderListAll.Add(spedOrders3);

                Random rand = new Random(spedOrderListAll.Count);
                for (int i = 0; i < 5; i++)
                {
                    spedOrderList.Add(spedOrderListAll[rand.Next(spedOrderListAll.Count)]);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", "[CreateSpedOrders]: " + e.ToString());
            }
        }
    }
}
