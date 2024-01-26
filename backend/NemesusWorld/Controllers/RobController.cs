using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using GTANetworkAPI;
using NemesusWorld.Database;
using NemesusWorld.Models;
using NemesusWorld.Utils;

namespace NemesusWorld.Controllers
{
    class RobController : Script
    {
        public static Dictionary<int, int> robCooldown = new Dictionary<int, int>();

        [RemoteEvent("Server:CheckRob")]
        public void OnCheckRob(Player player)
        {
            try
            {
                int cash = 0;
                TempData tempData = Helper.GetCharacterTempData(player);
                Character character = Helper.GetCharacterData(player);
                Business bizz = Business.GetClosestBusiness(player, 75.5f);
                Ped ped = Helper.GetClosestPed(player, 5.5f);

                if (bizz == null && Helper.IsAtBank(player) == -1) return;

                if (ped == null || ped.HasSharedData("Ped:Name") || tempData.inrob == true) return;

                if (bizz != null)
                {
                    if (Helper.GetFactionCountDuty(1) <= 3 && tempData.adminduty == false)
                    {
                        Helper.SendNotificationWithoutButton(player, "Dieser/s Laden/Geschäft kann jetzt nicht überfallen werden!", "error");
                        return;
                    }
                }

                if(Helper.IsAtBank(player) == 0 || Helper.IsAtBank(player) == 1)
                {
                    if (Helper.GetFactionCountDuty(1) <= 5 && tempData.adminduty == false)
                    {
                        Helper.SendNotificationWithoutButton(player, "Die Bank kann jetzt nicht überfallen werden!", "error");
                        return;
                    }
                }

                foreach (var item in robCooldown)
                {
                    if (item.Key == ped.Id && Helper.UnixTimestamp() < item.Value)
                    {
                        Helper.SendNotificationWithoutButton(player, "Dieser/s Laden/Geschäft kann noch nicht wieder überfallen werden!", "error");
                        return;
                    }
                }

                if (robCooldown != null)
                {
                    if (robCooldown.ContainsKey(ped.Id))
                    {
                        robCooldown.Remove(ped.Id);
                    }
                }

                if (character.robcooldown > 0 && Helper.UnixTimestamp() < character.robcooldown)
                {
                    Helper.SendNotificationWithoutButton(player, "Du kannst jetzt noch keinen weiteren Laden/Geschäft überfallen!", "error");
                    return;
                }

                //Allgemeines
                ped.SetSharedData("Player:AnimData", $"missminuteman_1ig_2%handsup_base%{50}");
                if (bizz != null)
                {
                    Helper.CreateAdminLog("roblog", $"{character.name} überfällt {bizz.name}.");
                }
                else
                {
                    Helper.CreateAdminLog("roblog", $"{character.name} überfällt Bank.");
                }

                if (bizz != null)
                {
                    bizz.nobuy = true;
                }
                tempData.inrob = true;

                int randomPercentage = 100;

                if (Helper.IsAtBank(player) > -1)
                {
                    if (ped.Id != 27)
                    {
                        randomPercentage = 90;
                    }
                    else
                    {
                        randomPercentage = 100;
                    }
                    if (tempData.adminduty == false)
                    {
                        character.robcooldown = Helper.UnixTimestamp() + 60 * 45;
                        robCooldown.Add(ped.Id, Helper.UnixTimestamp() + 60 * 480);
                    }
                }
                else if (bizz!= null && bizz.id >= 5 && bizz.id <= 15)
                {
                    randomPercentage = 25;
                    if (tempData.adminduty == false)
                    {
                        character.robcooldown = Helper.UnixTimestamp() + 60 * 25;
                        robCooldown.Add(ped.Id, Helper.UnixTimestamp() + 60 * 180);
                    }
                }
                else if (bizz != null && ((bizz.id >= 1 && bizz.id <= 4) || bizz.id == 37 || bizz.id == 38 || bizz.id == 39 || bizz.id == 40 || bizz.id == 41))
                {
                    randomPercentage = 15;
                    if (tempData.adminduty == false)
                    {
                        character.robcooldown = Helper.UnixTimestamp() + 60 * 15;
                        robCooldown.Add(ped.Id, Helper.UnixTimestamp() + 60 * 180);
                    }
                }
                else if (bizz != null && bizz.id >= 17 && bizz.id <= 21)
                {
                    randomPercentage = 30;
                    if (tempData.adminduty == false)
                    {
                        character.robcooldown = Helper.UnixTimestamp() + 60 * 35;
                        robCooldown.Add(ped.Id, Helper.UnixTimestamp() + 60 * 180);
                    }
                }

                if (Helper.GetRandomPercentage(randomPercentage))
                {
                    if (Helper.GetRandomPercentage(randomPercentage/4))
                    {
                        //ToDo: Sound Link anpassen
                        player.TriggerEvent("Client:Play3DSound", "https://nemesus-world.de/nwsounds/alarm.wav", -2);
                    }
                    Dispatch dispatch = new Dispatch();
                    MDCController.dispatchCount++;
                    dispatch.id = MDCController.dispatchCount;
                    if (bizz != null)
                    {
                        dispatch.text = $"Neuer Überfall - {bizz.name}, Täterbeschreibung: {character.size}, {character.appearance}";
                    }
                    else
                    {
                        dispatch.text = $"Neuer Banküberfall, Täterbeschreibung: {character.size}, {character.appearance}";
                    }
                    dispatch.name = "Sicherheitssystem";
                    dispatch.phonenumber = "0189911";
                    dispatch.to = 1;
                    dispatch.position = $"{player.Position.X.ToString(new CultureInfo("en-US")):N3},{player.Position.Y.ToString(new CultureInfo("en-US")):N3},{player.Position.Z.ToString(new CultureInfo("en-US")):N3}";
                    dispatch.timestamp = Helper.UnixTimestamp();
                    MDCController.dispatchList.Add(dispatch);

                    MDCController.SendPoliceMDCMessage(null, $"Neuer Dispatch verfügbar - DSPTH-{dispatch.id}!");
                }

                //Hauptbank
                if (ped.Id == 27 && (Helper.IsAtBank(player) == 0 || Helper.IsAtBank(player) == 1))
                {
                    Helper.SendNotificationWithoutButton(player, "Überfall gestartet, die Tresortür wird geöffnet. Dieser Vorgang dauert 1 Minute ...", "info", "top-left", 4750);
                    NAPI.Task.Run(() =>
                    {
                        if (player.Exists)
                        {
                            if (player.Position.DistanceTo(ped.Position) <= 12.35 && tempData.inrob == true)
                            {
                                Helper.SendNotificationWithoutButton(player, $"Die Tresortür ist offen, geh jetzt runter und spreng den Tresor!", "info", "top-left", 4750);
                                player.TriggerEvent("Client:PlaySoundSuccessExtra");
                            }
                            else
                            {
                                Helper.SendNotificationWithoutButton(player, $"Überfall abgebrochen!", "error", "top-left", 4750);
                                //ToDo: Sound Link anpassen
                                player.TriggerEvent("Client:Play3DSound", "https://nemesus-world.de/nwsounds/alarm.wav", -3);
                                tempData.inrob = false;
                            }
                        }
                    }, delayTime: 10500);
                }
                //Bank
                if (ped.Id != 27 && (Helper.IsAtBank(player) == 0 || Helper.IsAtBank(player) == 1))
                {
                    Random random = new Random();
                    cash = random.Next(52500, 65000);

                    Helper.SendNotificationWithoutButton(player, "Überfall gestartet, das Geld wird vorbereitet. Dieser Vorgang dauert 6 Minuten ...", "info", "top-left", 4750);

                    NAPI.Task.Run(() =>
                    {
                        if (player.Exists)
                        {
                            if (player.Position.DistanceTo(ped.Position) <= 8.35 && tempData.inrob == true)
                            {
                                CharacterController.SetMoney(player, cash);
                                character.abusemoney += cash;
                                Helper.SendNotificationWithoutButton(player, $"Überfall erfolgreich du erhältst {cash}$, verschwinde schnell!", "success", "top-left", 4750);
                                player.TriggerEvent("Client:PlaySoundSuccessExtra");
                                Helper.CreateAdminLog("roblog", $"{character.name} hat beim Überfall von einer Bank {cash}$ bekommen.");
                            }
                            else
                            {
                                Helper.SendNotificationWithoutButton(player, $"Überfall abgebrochen!", "error", "top-left", 4750);
                            }
                            //ToDo: Sound Link anpassen
                            player.TriggerEvent("Client:Play3DSound", "https://nemesus-world.de/nwsounds/alarm.wav", -3);
                            tempData.inrob = false;
                        }
                        ped.SetSharedData("Player:AnimData", "0");
                    }, delayTime: 10500);
                }

                //24/7-Tankstelle
                if (bizz != null && bizz.id >= 5 && bizz.id <= 15)
                {
                    cash = bizz.cash / 100 * 35;
                    if (cash <= 0)
                    {
                        cash = 3500;
                    }
                    if (cash >= 32500)
                    {
                        cash = 32500;
                    }          

                    Helper.SendNotificationWithoutButton(player, "Überfall gestartet, das Geld wird vorbereitet. Dieser Vorgang dauert 5 Minuten ...", "info", "top-left", 4750);

                    NAPI.Task.Run(() =>
                    {
                        if (player.Exists)
                        {
                            if (player.Position.DistanceTo(ped.Position) <= 8.35 && tempData.inrob == true)
                            {
                                CharacterController.SetMoney(player, cash);
                                character.abusemoney += cash;
                                Helper.SendNotificationWithoutButton(player, $"Überfall erfolgreich du erhältst {cash}$, verschwinde schnell!", "success", "top-left", 4750);
                                player.TriggerEvent("Client:PlaySoundSuccessExtra");
                                Helper.CreateAdminLog("roblog", $"{character.name} hat beim Überfall von {bizz.name} {cash}$ bekommen.");
                            }
                            else
                            {
                                Helper.SendNotificationWithoutButton(player, $"Überfall abgebrochen!", "error", "top-left", 4750);
                            }
                            //ToDo: Sound Link anpassen
                            player.TriggerEvent("Client:Play3DSound", "https://nemesus-world.de/nwsounds/alarm.wav", -3);
                            tempData.inrob = false;
                        }
                        bizz.nobuy = false;
                        ped.SetSharedData("Player:AnimData", "0");
                    }, delayTime: 10500);
                }

                //Ammunation
                if (bizz != null && bizz.id >= 17 && bizz.id <= 21)
                {
                    Random random = new Random();
                    String things = "";
                    int randomCount = random.Next(1, 3);
                    Items newitem = null;

                    Helper.SendNotificationWithoutButton(player, "Überfall gestartet, die Ware wird vorbereitet. Dieser Vorgang dauert 5 Minuten ...", "info", "top-left", 4750);

                    NAPI.Task.Run(() =>
                    {
                        if (player.Exists)
                        {
                            if (player.Position.DistanceTo(ped.Position) <= 8.35 && tempData.inrob == true)
                            {
                                if (Helper.GetRandomPercentage(95))
                                {
                                    for (int i = 0; i < randomCount; i++)
                                    {
                                        newitem = ItemsController.CreateNewItem(player, character.id, "Pistole", "Player", 1, ItemsController.GetFreeItemID(player), "n/A", "Ammunation", "n/A");
                                        if (newitem != null && ItemsController.CanPlayerHoldItem(player, newitem.weight * randomCount))
                                        {
                                            tempData.itemlist.Add(newitem);
                                        }
                                    }
                                    things += $"{randomCount}x Pistole,";
                                    randomCount = random.Next(25, 60);
                                    newitem = ItemsController.CreateNewItem(player, character.id, "9mm-Munition", "Player", randomCount, ItemsController.GetFreeItemID(player), "n/A", "Ammunation", "n/A");
                                    if (newitem != null && ItemsController.CanPlayerHoldItem(player, newitem.weight * randomCount))
                                    {
                                        tempData.itemlist.Add(newitem);
                                    }
                                    things += $" {randomCount}x 9mm-Munition,";
                                    if (Helper.GetRandomPercentage(10))
                                    {
                                        newitem = ItemsController.CreateNewItem(player, character.id, "Mini-SMG", "Player", 1, ItemsController.GetFreeItemID(player), "n/A", "Ammunation", "n/A");
                                        if (newitem != null && ItemsController.CanPlayerHoldItem(player, newitem.weight))
                                        {
                                            tempData.itemlist.Add(newitem);
                                            things += $" 1x Mini-SMG,";
                                        }
                                    }
                                    if (Helper.GetRandomPercentage(35))
                                    {
                                        newitem = ItemsController.CreateNewItem(player, character.id, "Kleine-Schutzweste", "Player", 1, ItemsController.GetFreeItemID(player), "n/A", "Ammunation", "n/A");
                                        if (newitem != null && ItemsController.CanPlayerHoldItem(player, newitem.weight))
                                        {
                                            tempData.itemlist.Add(newitem);
                                            things += $" 1x Kleine Schutzweste,";
                                        }
                                    }

                                    things = things.Remove(things.Length - 1);
                                    Helper.SendNotificationWithoutButton(player, $"Überfall erfolgreich du erhältst: {things}, verschwinde schnell!", "success", "top-left", 7750);
                                    player.TriggerEvent("Client:PlaySoundSuccessExtra");
                                    Helper.CreateAdminLog("roblog", $"{character.name} hat beim Überfall von {bizz.name}: {things} bekommen.");
                                }
                                else
                                {
                                    NAPI.Player.SetPlayerHealth(player, 1);
                                    Helper.SendNotificationWithoutButton(player, $"Der Verkäufer hat dich überwältigt und ausgeknockt!", "error", "top-left", 4750);
                                    ped.SetSharedData("Player:AnimData", "gestures@m@standing@casual%gesture_damn%0");
                                }
                            }
                            else
                            {
                                Helper.SendNotificationWithoutButton(player, $"Überfall abgebrochen!", "error", "top-left", 4750);
                            }
                            //ToDo: Sound Link anpassen
                            player.TriggerEvent("Client:Play3DSound", "https://nemesus-world.de/nwsounds/alarm.wav", -3);
                            tempData.inrob = false;
                        }
                        bizz.nobuy = false;
                        ped.SetSharedData("Player:AnimData", "0");
                    }, delayTime: 10500);
                }

                //Kleidungsladen + Juwelier + Tattoo-Laden + Barber-Shop
                if (bizz != null && ((bizz.id >= 1 && bizz.id <= 4) || bizz.id == 37 || bizz.id == 38 || bizz.id == 39 || bizz.id == 40 || bizz.id == 41))
                {
                    cash = bizz.cash / 100 * 35;
                    if (cash <= 0)
                    {
                        cash = 1500;
                    }
                    if (cash >= 17500)
                    {
                        cash = 17500;
                    }

                    Helper.SendNotificationWithoutButton(player, "Überfall gestartet, das Geld wird vorbereitet. Dieser Vorgang dauert 3 Minuten ...", "info", "top-left", 4750);

                    NAPI.Task.Run(() =>
                    {
                        if (player.Exists)
                        {
                            if (player.Position.DistanceTo(ped.Position) <= 8.35 && tempData.inrob == true)
                            {
                                CharacterController.SetMoney(player, cash);
                                character.abusemoney += cash;
                                Helper.SendNotificationWithoutButton(player, $"Überfall erfolgreich du erhältst {cash}$, verschwinde schnell!", "success", "top-left", 4750);
                                player.TriggerEvent("Client:PlaySoundSuccessExtra");
                                Helper.CreateAdminLog("roblog", $"{character.name} hat beim Überfall von {bizz.name} {cash}$ bekommen.");
                            }
                            else
                            {
                                Helper.SendNotificationWithoutButton(player, $"Überfall abgebrochen!", "error", "top-left", 4750);
                            }
                            //ToDo: Sound Link anpassen
                            player.TriggerEvent("Client:Play3DSound", "https://nemesus-world.de/nwsounds/alarm.wav", -3);
                            tempData.inrob = false;
                        }
                        bizz.nobuy = false;
                        ped.SetSharedData("Player:AnimData", "0");
                    }, delayTime: 10500);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnCheckRob]: " + e.ToString());
            }
        }
    }
}
