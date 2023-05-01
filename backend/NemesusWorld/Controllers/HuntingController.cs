using GTANetworkAPI;
using NemesusWorld.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Controllers
{
    public class HuntingController : Script
    {
        public HuntingController() { }

        public static List<Ped> SpawnedAnimals = new List<Ped>();
        public static Random Rnd = new Random();
        public static Player animalsController = null;

        public enum Animals
        {
            Deer,
            Boar
        }

        public static void UpdateAnimalsController()
        {
            try
            {
                foreach (Ped ped in SpawnedAnimals)
                {
                    ped.Controller = animalsController;
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[UpdateAnimalsController]: " + e.ToString());
            }
        }

        public static void CreateNewAnimal(Ped ped)
        {
            try
            {
                if (ped != null)
                {
                    SpawnedAnimals.Remove(ped);
                    ped.Delete();
                    ped = NAPI.Ped.CreatePed(Rnd.Next(0, 2) == 0 ? (0xD86B5A95) : (0xCE5FF074), AnimalSpawnPoints[Rnd.Next(0, AnimalSpawnPoints.Length)], 0, true, true, false, false, 0);
                    ped.Controller = animalsController;
                    ped.SetSharedData("Ped:Death", 0);
                    SpawnedAnimals.Add(ped);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CreateNewAnimal]: " + e.ToString());
            }
        }

        public static void SetNewAnimalsController()
        {
            try
            {
                if (animalsController == null || !animalsController.Exists || animalsController.Dimension > 0 || animalsController.Position.DistanceTo(new Vector3(1714.7175, -571.90814, 144.50644)) > 105)
                {
                    float dist = 375f;
                    float newDist = 375f;
                    float checkdist = 375f;
                    Player getPlayer = null;
                    foreach(Player player in NAPI.Pools.GetAllPlayers())
                    {
                        checkdist = player.Position.DistanceTo(new Vector3(1714.7175, -571.90814, 144.50644));
                        if (player.Exists && player.Dimension == 0 && checkdist <= 100)
                        {
                            newDist = checkdist;
                            if (newDist < dist)
                            {
                                dist = newDist;
                                getPlayer = player;
                            }
                        }
                    }
                    if (getPlayer != null)
                    {
                        animalsController = getPlayer;
                        UpdateAnimalsController();
                    }
                    else
                    {
                        animalsController = null;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[SetNewAnimalsController]: " + e.ToString());
            }
        }

        public static void InitAnimals()
        {
            try
            {
                foreach (var spawn in AnimalSpawnPoints)
                {
                    Ped ped = NAPI.Ped.CreatePed(Rnd.Next(0, 2) == 0 ? (0xD86B5A95) : (0xCE5FF074), spawn, 0, true, false, false, false, 0);
                    ped.Controller = animalsController;
                    ped.SetSharedData("Ped:Death", 0);
                    SpawnedAnimals.Add(ped);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[InitAnimals]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:UpdatePedStateOfHunting")]
        public static void OnUpdatePedStateOfHunting(Player player, int remoteid, int pedstate, Vector3 position)
        {
            try
            {
                foreach (Ped ped in HuntingController.SpawnedAnimals)
                {
                    if (ped.Id == remoteid)
                    {
                        ped.SetSharedData("Ped:Death", pedstate);
                        ped.Position = position;
                        ped.Rotation = ped.Rotation;
                        return;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[UpdatePedState]: " + e.ToString());
            }
        }

        public static Vector3[] AnimalSpawnPoints =
        {
            new Vector3(1713.9882, -563.03455, 148.33177),
            new Vector3(1709.1924, -525.1786, 154.1249),
            new Vector3(1662.2157, -521.4746, 163.89975),
            new Vector3(1608.1688, -542.5705, 172.20906),
            new Vector3(1589.0094, -578.1644, 158.12683),
            new Vector3(1567.5553, -611.0773, 145.20512),
            new Vector3(1548.5409, -669.8783, 118.79825),
            new Vector3(1544.0807, -742.232, 103.70901),
            new Vector3(1547.0284, -773.0183, 101.8855),
            new Vector3(1581.5372, -805.16956, 99.181046),
            new Vector3(1645.7397, -784.22205, 113.110245),
            new Vector3(1688.4742, -728.74005, 119.03842),
            new Vector3(1717.7264, -692.0604, 141.51602),
            new Vector3(1710.1886, -662.98645, 134.43361),
            new Vector3(1711.5187, -607.84564, 141.55287),
            new Vector3(1764.6046, -633.7431, 159.31703),
            new Vector3(1704.5247, -599.64905, 136.95311),
            new Vector3(1673.5636, -533.7431, 160.47623),
            new Vector3(1799.3373, -563.2692, 186.0177),
            new Vector3(1806.1074, -639.8828, 147.0653)
        };
    }
}
