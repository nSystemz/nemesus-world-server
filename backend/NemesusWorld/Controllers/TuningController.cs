using GTANetworkAPI;
using NemesusWorld.Database;
using NemesusWorld.Models;
using NemesusWorld.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Controllers
{
    class TuningController : Script
    {
        [RemoteEvent("Server:ShowTuningMenu")]
        public static void ShowTuningMenu(Player player)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                Account account = Helper.GetAccountData(player);
                if (character == null || account == null || !player.IsInVehicle) return;

                float dist = 45.5f;
                Groups group = GroupsController.GetGroupById(character.mygroup);
                House house = null;

                if (group == null && character.faction != 3) return;

                if (character.faction != 3 || character.factionduty == false)
                {
                    if (group == null) return;

                    string[] licArray = new string[9];
                    licArray = group.licenses.Split("|");
                    if (Convert.ToInt32(licArray[1]) == 0)
                    {
                        Helper.SendNotificationWithoutButton(player, "Deine Gruppierung besitzt keine Tuninglizenz!", "error", "top-left", 3500);
                        return;
                    }

                    house = character.inhouse != -1 ? House.GetHouseById(character.inhouse) : House.GetClosestHouse(player, 25.5f);
                }
                else
                {
                    house = House.GetHouseById(2);
                    dist = 105.5f;
                }

                if (house == null) return;

                if ((house.position.DistanceTo(player.Vehicle.Position) <= dist || character.inhouse == house.id))
                {
                    if (house.id != 2)
                    {
                        if (house.housegroup != character.mygroup)
                        {
                            Helper.SendNotificationWithoutButton(player, "Du bist nicht in der Nähe/in deinem Firmensitz!", "error", "top-left", 3500);
                            return;
                        }
                    }
                    if (player.Vehicle.EngineStatus == false)
                    {
                        Helper.SendNotificationWithoutButton(player, "Du musst zuerst den Motor vom Fahrzeug anschalten!", "error");
                        return;
                    }
                    player.TriggerEvent("Client:ShowTuningMenu", house.stock);
                    return;
                }
                Helper.SendNotificationWithoutButton(player, "Du bist nicht in der Nähe/in deinem Firmen/Fraktionssitz!", "error", "top-left", 3500);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[ShowTuningMenu]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:TuningSync")]
        public static void OnTuningSync(Player player, string sync)
        {
            try
            {
                if (player.IsInVehicle)
                {
                    player.Vehicle.SetSharedData("Vehicle:Tuning", sync);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnTuningSync]: " + e.ToString());

            }
        }

        [RemoteEvent("Server:NitroSetStatus")]
        public static void OnNitroSetStatus(Player player, int nitro)
        {
            try
            {
                if (player.IsInVehicle)
                {
                    player.Vehicle.SetSharedData("Vehicle:NitroStatus", nitro);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnNitroStatus]: " + e.ToString());

            }
        }

        [RemoteEvent("Server:TuningSet")]
        public static void OnTuningSet(Player player, string sync, int stock)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);

                if (character == null) return;

                if (player.IsInVehicle)
                {
                    House house = character.inhouse != -1 ? House.GetHouseById(character.inhouse) : House.GetClosestHouse(player, 25.5f);

                    if (house == null) return;

                    if(house.stock < stock)
                    {
                        Helper.SendNotificationWithoutButton(player, "Es sind nicht genügend Tuningteile auf Lager!", "error", "top-left", 3500);
                        return;
                    }

                    house.stock -= stock;
                    player.Vehicle.SetSharedData("Vehicle:Tuning", sync);
                    player.TriggerEvent("Client:ShowTuningMenu", 0);
                    Helper.SendNotificationWithoutButton(player, "Das Tuning wurde erfolgreich angebracht!", "success", "top-left", 3500);
                    if (character.faction == 3 && character.factionduty == true)
                    {
                        Helper.CreateFactionLog(character.faction, $"{character.name} hat ein Fahrzeug für {stock} Tuningteile getuned!");
                    }
                    else
                    {
                        Helper.CreateGroupMoneyLog(character.mygroup, $"{character.name} hat ein Fahrzeug für {stock} Tuningteile getuned!");
                    }
                    player.TriggerEvent("Client:PlaySoundSuccessExtra");
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnTuningSet]: " + e.ToString());

            }
        }

        [RemoteEvent("Server:TuningPreview")]
        public static void OnTuningPreview(Player player, int tuning, int component, bool perlm = false, Vehicle getVehicle = null)
        {
            try
            {
                Vehicle vehicle = null;
                if(player == null)
                {
                    vehicle = getVehicle;
                }
                else
                {
                    vehicle = player.Vehicle;
                }
                if (player == null || player.IsInVehicle)
                {
                    if(tuning == 53)
                    {
                        NAPI.Vehicle.SetVehicleNumberPlateStyle(vehicle, component);
                    }
                    else if(tuning == 56)
                    {
                        if (component > -1)
                        {
                            NAPI.Vehicle.SetVehicleNeonState(vehicle, true);
                            if (component == 0)
                            {
                                NAPI.Vehicle.SetVehicleNeonColor(vehicle, 230, 16, 34);
                            }
                            else if (component == 1)
                            {
                                NAPI.Vehicle.SetVehicleNeonColor(vehicle, 2, 35, 250);
                            }
                            else if (component == 2)
                            {
                                NAPI.Vehicle.SetVehicleNeonColor(vehicle, 44, 196, 10);
                            }
                            else if (component == 3)
                            {
                                NAPI.Vehicle.SetVehicleNeonColor(vehicle, 245, 208, 0);
                            }
                            else if (component == 4)
                            {
                                NAPI.Vehicle.SetVehicleNeonColor(vehicle, 128, 14, 124);
                            }
                            else if (component == 5)
                            {
                                NAPI.Vehicle.SetVehicleNeonColor(vehicle, 240, 115, 48);
                            }
                            else if (component == 6)
                            {
                                NAPI.Vehicle.SetVehicleNeonColor(vehicle, 14, 168, 207);
                            }
                            else if (component == 7)
                            {
                                NAPI.Vehicle.SetVehicleNeonColor(vehicle, 209, 82, 207);
                            }
                            else if (component == 8)
                            {
                                NAPI.Vehicle.SetVehicleNeonColor(vehicle, 95, 158, 160);
                            }
                            else if (component == 9)
                            {
                                NAPI.Vehicle.SetVehicleNeonColor(vehicle, 255, 255, 255);
                            }
                            else if (component == 10)
                            {
                                NAPI.Vehicle.SetVehicleNeonColor(vehicle, 1, 4, 10);
                            }
                        }
                        else
                        {
                            NAPI.Vehicle.SetVehicleNeonState(vehicle, false);
                            NAPI.Vehicle.SetVehicleNeonColor(vehicle, 0, 0, 0);
                        }
                    }
                    else if(tuning == 60)
                    {
                        vehicle.SetSharedData("Vehicle:Transmission", component);
                    }
                    else if (tuning == 66)
                    {
                        if (perlm == false)
                        {
                            NAPI.Vehicle.SetVehiclePrimaryColor(vehicle, component);
                            string color = $"{NAPI.Vehicle.GetVehiclePrimaryColor(vehicle)},{NAPI.Vehicle.GetVehicleSecondaryColor(vehicle)},{NAPI.Vehicle.GetVehiclePearlescentColor(vehicle)},{NAPI.Vehicle.GetVehicleWheelColor(vehicle)}";
                            vehicle.SetSharedData("Vehicle:Color", color);
                        }
                        else
                        {
                            NAPI.Vehicle.SetVehiclePearlescentColor(vehicle, component);
                            string color = $"{NAPI.Vehicle.GetVehiclePrimaryColor(vehicle)},{NAPI.Vehicle.GetVehicleSecondaryColor(vehicle)},{NAPI.Vehicle.GetVehiclePearlescentColor(vehicle)},{NAPI.Vehicle.GetVehicleWheelColor(vehicle)}";
                            vehicle.SetSharedData("Vehicle:Color", color);
                        }
                    }
                    else if (tuning == 67)
                    {
                        NAPI.Vehicle.SetVehicleSecondaryColor(vehicle, component);
                        string color = $"{NAPI.Vehicle.GetVehiclePrimaryColor(vehicle)},{NAPI.Vehicle.GetVehicleSecondaryColor(vehicle)},{NAPI.Vehicle.GetVehiclePearlescentColor(vehicle)},{NAPI.Vehicle.GetVehicleWheelColor(vehicle)}";
                        vehicle.SetSharedData("Vehicle:Color", color);
                    }
                    else if (tuning == 68)
                    {
                        NAPI.Vehicle.SetVehicleWheelColor(vehicle, component);
                        string color = $"{NAPI.Vehicle.GetVehiclePrimaryColor(vehicle)},{NAPI.Vehicle.GetVehicleSecondaryColor(vehicle)},{NAPI.Vehicle.GetVehiclePearlescentColor(vehicle)},{NAPI.Vehicle.GetVehicleWheelColor(vehicle)}";
                        vehicle.SetSharedData("Vehicle:Color", color);
                    }
                    else if (tuning == 69)
                    {
                        NAPI.Vehicle.SetVehiclePearlescentColor(vehicle, component);
                        string color = $"{NAPI.Vehicle.GetVehiclePrimaryColor(vehicle)},{NAPI.Vehicle.GetVehicleSecondaryColor(vehicle)},{NAPI.Vehicle.GetVehiclePearlescentColor(vehicle)},{NAPI.Vehicle.GetVehicleWheelColor(vehicle)}";
                        vehicle.SetSharedData("Vehicle:Color", color);
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnTuningPreview]: " + e.ToString());
            }
        }
    }
}
