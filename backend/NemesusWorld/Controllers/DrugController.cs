using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using GTANetworkAPI;
using NemesusWorld.Database;
using NemesusWorld.Models;
using NemesusWorld.Utils;
using Color = GTANetworkAPI.Color;
using Player = GTANetworkAPI.Player;
using Vector3 = GTANetworkAPI.Vector3;

namespace NemesusWorld.Controllers
{
    public class DrugController : Script
    {
        public static List<DrugsPlants> drugList = new List<DrugsPlants>();
        public static int waterCheck = 0;

        public DrugController() { }

        public static void CreateDrugPlant(Player player, string drugname)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                if (character == null) return;

                if(character.inhouse == -1 || player.Dimension == 0)
                {
                    Helper.SendNotificationWithoutButton(player, "Du befindest dich in keinem Haus!", "error");
                    return;
                }

                if (GetClosestDrugPlant(player, 1.5f) != null)
                {
                    Helper.SendNotificationWithoutButton(player, "Hier steht schon eine Drogenpflanze!", "error");
                    return;
                }

                int count = 0;
                foreach (DrugsPlants drugPlant in drugList)
                {
                    if (drugPlant.owner == character.id)
                    {
                        count++;
                    }
                }

                if (count >= 12)
                {
                    Helper.SendNotificationWithoutButton(player, "Du kannst max. 12 Drogenpflanzen pflanzen!", "error");
                    return;
                }

                DrugsPlants newPlant = new DrugsPlants();
                newPlant.owner = character.id;
                newPlant.vw = (int)player.Dimension;
                newPlant.water = 100;
                newPlant.time = 60;
                newPlant.value = 0;
                newPlant.drugname = drugname;
                Vector3 newPosition = Helper.GetPositionInFrontOfPlayer(player, 0.75f);
                newPlant.posx = newPosition.X.ToString(new CultureInfo("en-US"));
                newPlant.posy = newPosition.Y.ToString(new CultureInfo("en-US"));
                newPlant.posz = newPosition.Z.ToString(new CultureInfo("en-US"));
                newPlant.posa = player.Heading.ToString(new CultureInfo("en-US"));
                newPlant.vw = (int)player.Dimension;
                Helper.PlayShortAnimation(player, "amb@world_human_gardener_plant@male@idle_a", "idle_b", 2250);
                if (drugname == "Marihuana")
                {
                    newPlant.obj = NAPI.Object.CreateObject(NAPI.Util.GetHashKey("bkr_prop_weed_01_small_01b"), new Vector3(newPosition.X, newPosition.Y, newPosition.Z - 1.0), new Vector3(0.0f, 0.0f, player.Heading + 90), 255, player.Dimension);
                    newPlant.textLabel = NAPI.TextLabel.CreateTextLabel($"~b~{drugname}pflanze\n~b~0g - Wasserzustand: 100%\n\n~b~[E]~w~ zum ernten\n~b~[G]~w~ zum giessen\n~b~[P]~w~ zum zerstören", new Vector3(newPosition.X, newPosition.Y, newPosition.Z + 0.5), 5.0f, 0.5f, 4, new Color(255, 255, 255), false, player.Dimension);
                }
                else
                {
                    newPlant.obj = NAPI.Object.CreateObject(NAPI.Util.GetHashKey("prop_plant_int_01b"), new Vector3(newPosition.X, newPosition.Y, newPosition.Z - 1.05), new Vector3(0.0f, 0.0f, player.Heading + 90), 255, player.Dimension);
                    newPlant.textLabel = NAPI.TextLabel.CreateTextLabel($"~b~{drugname}pflanze\n~b~0g - Wasserzustand: 100%\n\n~b~[E]~w~ zum ernten\n~b~[G]~w~ zum giessen\n~b~[P]~w~ zum zerstören", new Vector3(newPosition.X, newPosition.Y, newPosition.Z + 0.5), 5.0f, 0.5f, 4, new Color(255, 255, 255), false, player.Dimension);
                }
                Helper.SendNotificationWithoutButton(player, "Neue Drogenpflanze aufgestellt!", "success");
                Items getItem = null;
                if (drugname == "Marihuana")
                {
                    getItem = ItemsController.GetItemByItemName(player, "Marihuanasamen");
                }
                else
                {
                    getItem = ItemsController.GetItemByItemName(player, "Kokainsamen");
                }
                if(getItem != null)
                {
                    getItem.amount -= 2;
                    if(getItem.amount <= 0)
                    {
                        ItemsController.RemoveItem(player, getItem.itemid);
                    }
                }
                drugList.Add(newPlant);
                db.Insert(newPlant);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CreateDrugPlant]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:DrugPlantInteract")]
        public static void InteractWithDrugPlant(Player player, int interact = 0)
        {
            try
            {
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                Character character = Helper.GetCharacterData(player);
                if (character == null) return;

                if (!player.IsInVehicle)
                {
                    DrugsPlants drugPlant = DrugController.GetClosestDrugPlant(player, 1.65f);
                    if (drugPlant != null)
                    {
                        if (interact == 0)
                        {
                            if (drugPlant.water == 100)
                            {
                                Helper.SendNotificationWithoutButton(player, "Die Drogenpflanze muss noch nicht gegossen werden!", "error");
                                return;
                            }
                            Helper.PlayShortAnimation(player, "amb@world_human_gardener_plant@male@idle_a", "idle_b", 2250);
                            drugPlant.water = 100;
                            Helper.SendNotificationWithoutButton(player, "Drogenpflanze erfolgreich gegossen!", "success");
                        }
                        else if (interact == 1)
                        {
                            Helper.PlayShortAnimation(player, "amb@world_human_gardener_plant@male@idle_a", "idle_b", 2250);
                            NAPI.Task.Run(() =>
                            {
                                db.Delete(drugPlant);
                                drugList.Remove(drugPlant);
                                drugPlant.obj.Delete();
                                drugPlant.textLabel.Delete();
                                drugPlant.owner = -1;
                                Helper.SendNotificationWithoutButton(player, "Drogenpflanze erfolgreich zerstört!", "success");
                                Helper.OnStopAnimation2(player);
                            }, delayTime: 2251);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[InteractWithDrugPlant]: " + e.ToString());
            }
        }

        public static void DrugPlantsCheck()
        {
            try
            {
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                Random random = new Random();
                bool found = false;
                if (drugList.Count > 0)
                {
                    foreach (DrugsPlants drugPlants in drugList.ToList())
                    {
                        if (drugPlants.owner != -1)
                        {
                            waterCheck++;
                            drugPlants.time--;
                            if (drugPlants.time <= 0)
                            {
                                if (Helper.GetRandomPercentage(95) && drugPlants.water >= 25)
                                {
                                    foreach (FurnitureSetHouse furniture in House.furnitureList.ToList())
                                    {
                                        if (furniture != null && furniture.house == drugPlants.vw)
                                        {
                                            if(furniture.name.Contains("Wärmelampe") && furniture.objectHandle.Position.DistanceTo(drugPlants.obj.Position) <= 10.5)
                                            {
                                                found = true;
                                                break;
                                            }
                                        }
                                    }
                                    if (found == true)
                                    {
                                        if(drugPlants.drugname == "Marihuana")
                                        {
                                            drugPlants.value += random.Next(0, 2);
                                        }
                                        else
                                        {
                                            drugPlants.value += random.Next(0, 1);
                                        }
                                    }
                                }
                                drugPlants.time = 60;
                            }
                            if (Helper.GetRandomPercentage(50) && waterCheck >= 20)
                            {
                                drugPlants.water -= random.Next(1, 3);
                                waterCheck = 0;
                            }
                            if (drugPlants.water <= 0)
                            {
                                db.Delete(drugPlants);
                                drugList.Remove(drugPlants);
                                drugPlants.obj.Delete();
                                drugPlants.textLabel.Delete();
                                drugPlants.owner = -1;
                            }
                            else
                            {
                                drugPlants.textLabel.Text = $"~b~{drugPlants.drugname}pflanze\n~b~{drugPlants.value}g - Wasserzustand: {drugPlants.water}%\n\n~b~[E]~w~ zum ernten\n~b~[G]~w~ zum giessen\n~b~[P]~w~ zum zerstören";
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[DrugPlantsCheck]: " + e.ToString());
            }
        }

        public static void GetAllDrugPlants()
        {
            try
            {
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                foreach (DrugsPlants drugPlants in db.Fetch<DrugsPlants>("SELECT * FROM drugs"))
                {
                    drugList.Add(drugPlants);
                    if (drugPlants.drugname == "Marihuana")
                    {
                        drugPlants.obj = NAPI.Object.CreateObject(NAPI.Util.GetHashKey("bkr_prop_weed_01_small_01b"), new Vector3(float.Parse(drugPlants.posx, new CultureInfo("en-US")), float.Parse(drugPlants.posy, new CultureInfo("en-US")), float.Parse(drugPlants.posz, new CultureInfo("en-US")) - 1.0), new Vector3(0.0f, 0.0f, float.Parse(drugPlants.posa, new CultureInfo("en-US")) + 90), 255, (uint)drugPlants.vw);
                    }
                    else
                    {
                        drugPlants.obj = NAPI.Object.CreateObject(NAPI.Util.GetHashKey("prop_plant_int_01b"), new Vector3(float.Parse(drugPlants.posx, new CultureInfo("en-US")), float.Parse(drugPlants.posy, new CultureInfo("en-US")), float.Parse(drugPlants.posz, new CultureInfo("en-US")) - 1.05), new Vector3(0.0f, 0.0f, float.Parse(drugPlants.posa, new CultureInfo("en-US")) + 90), 255, (uint)drugPlants.vw);
                    }
                    drugPlants.textLabel = NAPI.TextLabel.CreateTextLabel($"~b~{drugPlants.drugname}\n~b~0g - Wasserzustand: 100%\n\n~b~[E]~w~ zum ernten\n~b~[G]~w~ zum giessen\n~b~[P]~w~ zum zerstören", new Vector3(float.Parse(drugPlants.posx, new CultureInfo("en-US")), float.Parse(drugPlants.posy, new CultureInfo("en-US")), float.Parse(drugPlants.posz, new CultureInfo("en-US")) + 0.5), 5.0f, 0.5f, 4, new Color(255, 255, 255), false, (uint)drugPlants.vw);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetAllDrugPlants]: " + e.ToString());
            }
        }

        public static void SaveAllDrugPlants()
        {
            try
            {
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                foreach(DrugsPlants drugPlants in drugList)
                {
                    db.Save(drugPlants);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[SaveAllDrugPlants]: " + e.ToString());
            }
        }

        public static DrugsPlants GetClosestDrugPlant(Player player, float distance = 1.5f)
        {
            try
            {
                DrugsPlants drugsPlants = null;
                if (drugList == null) return drugsPlants;
                foreach (DrugsPlants drugPlant in drugList)
                {
                    if (drugPlant != null && player.Position.DistanceTo(drugPlant.obj.Position) < distance && drugPlant.vw == player.Dimension)
                    {
                        drugsPlants = drugPlant;
                        distance = player.Position.DistanceTo(drugPlant.obj.Position);
                    }
                }
                return drugsPlants;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetClosestDrugPlant]: " + e.ToString());
            }
            return null;
        }
    }
}

