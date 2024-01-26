using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using GTANetworkAPI;
using MySqlConnector;
using NemesusWorld.Database;
using NemesusWorld.Models;
using NemesusWorld.Utils;
using Newtonsoft.Json.Linq;

namespace NemesusWorld.Controllers
{
    class MDCController : Script
    {
        public static List<Fahndungen> fahndungList = new List<Fahndungen>();
        public static List<Dispatch> dispatchList = new List<Dispatch>();
        public static List<FahndungsKommentare> dispatchKommentareList = new List<FahndungsKommentare>();
        public static int dispatchCount = 0;
        public static int klingelCooldown = 0;
        public static string weaponOrder = "n/A";
        public static string weaponOrderStatus = "n/A";
        public static int weaponOrderFaction = 0;
        public static int messageCooldown = 0;

        public static void GetAllFahndungen()
        {
            try
            {
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                foreach (Fahndungen fahndung in db.Fetch<Fahndungen>("SELECT * FROM fahndungen ORDER BY timestamp DESC LIMIT 75"))
                {
                    fahndungList.Add(fahndung);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetAllFahndungen]: " + e.ToString());
            }
        }

        public static Fahndungen GetFahndungById(int id)
        {
            try
            {
                foreach (Fahndungen fahndung in fahndungList)
                {
                    if (fahndung.id == id)
                    {
                        return fahndung;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetFahndungById]: " + e.ToString());
            }
            return null;
        }

        public static void CreateFahndung(string data)
        {
            try
            {
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                Fahndungen fahndungen = new Fahndungen();
                fahndungen.text = data;
                fahndungen.creator = "Überwachungskamera";
                fahndungen.timestamp = Helper.UnixTimestamp();
                fahndungen.editor = "n/A";
                fahndungen.status = 0;

                fahndungList.Insert(0, fahndungen);
                db.Insert(fahndungen);

                SendPoliceMDCMessage(null, $"Neue Fahndung verfügbar - FHDNG-{fahndungen.id}!");
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CreateFahndung]: " + e.ToString());
            }
        }

        public static void CreateFahndungsKommentar(int fahndungsid, string text, string creator, int id)
        {
            try
            {
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                FahndungsKommentare fahndungsKommentare = new FahndungsKommentare();
                fahndungsKommentare.fahndungsid = fahndungsid;
                fahndungsKommentare.creator = creator;
                string[] characterNameArray = new string[2];
                characterNameArray = creator.Split(' ');
                fahndungsKommentare.text = text + $" [{characterNameArray[0][0]}{characterNameArray[1][0]}-{id}]";
                fahndungsKommentare.timestamp = Helper.UnixTimestamp();
                db.Insert(fahndungsKommentare);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CreateFahndungsKommentar]: " + e.ToString());
            }
        }

        public static void CreateDispatchKommentar(int dispatchid, string text, string creator, int id)
        {
            try
            {
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                FahndungsKommentare fahndungsKommentare = new FahndungsKommentare();
                fahndungsKommentare.fahndungsid = dispatchid;
                fahndungsKommentare.creator = creator;
                string[] characterNameArray = new string[2];
                characterNameArray = creator.Split(' ');
                fahndungsKommentare.text = text + $" [{characterNameArray[0][0]}{characterNameArray[1][0]}-{id}]";
                fahndungsKommentare.timestamp = Helper.UnixTimestamp();
                dispatchKommentareList.Add(fahndungsKommentare);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CreateDispatchKommentar]: " + e.ToString());
            }
        }

        public static Dispatch GetDispatchById(int id)
        {
            try
            {
                foreach (Dispatch dispatch in dispatchList)
                {
                    if (dispatch.id == id)
                    {
                        return dispatch;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetDispatchById]: " + e.ToString());
            }
            return null;
        }

        public static void SendPoliceMDCMessage(Player player, string message)
        {
            try
            {
                foreach (Player p in NAPI.Pools.GetAllPlayers())
                {
                    if (p != null && p.GetOwnSharedData<bool>("Player:Spawned") == true && p.GetSharedData<bool>("Player:Death") == false)
                    {
                        Character character = Helper.GetCharacterData(p);
                        if (character != null && character.faction == 1 && character.factionduty == true && character.arrested == 0)
                        {
                            Helper.SendNotificationWithoutButton(p, message, "info", "top-left", 3250);
                            if (message.ToLower().Contains("neuer Dispatch"))
                            {
                                p.TriggerEvent("Client:MDCSettings", "resetdispatches", "n/A");
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[SendPoliceMDCMessage]: " + e.ToString());
            }
        }

        public static void SendMedicMDCMessage(Player player, string message)
        {
            try
            {
                foreach (Player p in NAPI.Pools.GetAllPlayers())
                {
                    if (p != null && player.Exists && Account.IsPlayerLoggedIn(player) && p.GetOwnSharedData<bool>("Player:Spawned") == true && p.GetSharedData<bool>("Player:Death") == false)
                    {
                        Character character = Helper.GetCharacterData(p);
                        if (character != null && character.faction == 2 && character.factionduty == true && character.arrested == 0)
                        {
                            Helper.SendNotificationWithoutButton(p, message, "info", "top-left", 3250);
                            if(message.ToLower().Contains("neuer Dispatch"))
                            {
                                p.TriggerEvent("Client:MDCSettings", "resetdispatches", "n/A");
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[SendMedicMDCMessage]: " + e.ToString());
            }
        }

        public static void SendACLSMDCMessage(Player player, string message)
        {
            try
            {
                foreach (Player p in NAPI.Pools.GetAllPlayers())
                {
                    if (p != null && player.Exists && Account.IsPlayerLoggedIn(player) && p.GetOwnSharedData<bool>("Player:Spawned") == true && p.GetSharedData<bool>("Player:Death") == false)
                    {
                        Character character = Helper.GetCharacterData(p);
                        if (character != null && character.faction == 3 && character.factionduty == true && character.arrested == 0)
                        {
                            Helper.SendNotificationWithoutButton(p, message, "info", "top-left", 3250);
                            if (message.ToLower().Contains("neuer Dispatch"))
                            {
                                p.TriggerEvent("Client:MDCSettings", "resetdispatches", "n/A");
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[SenACLSMDCMessage]: " + e.ToString());
            }
        }

        public static string GetLicName(int lic)
        {
            string licName = "Führerschein";
            try
            {
                switch (lic)
                {
                    case 0: licName = "Führerschein"; break;
                    case 1: licName = "Motorradschein"; break;
                    case 2: licName = "Truckerschein"; break;
                    case 3: licName = "Bootsschein"; break;
                    case 4: licName = "Flugschein"; break;
                    case 5: licName = "Waffenschein"; break;
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetLicName]: " + e.ToString());
            }
            return licName;
        }

        [RemoteEvent("Server:MDCSettings")]
        public static void OnMDCSettings(Player player, string flag, string data = "n/A")
        {
            try
            {
                Account account = Helper.GetAccountData(player);
                Character character = Helper.GetCharacterData(player);
                TempData tempData = Helper.GetCharacterTempData(player);
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);

                if (account == null || character == null) return;

                string[] mdcArray = new string[5];

                switch (flag.ToLower())
                {
                    case "updatelivemap":
                        {
                            if (character.faction != 1 && character.faction != 2 && character.faction != 3)
                            {
                                Helper.SendNotificationWithoutButton(player, $"Keine Berechtigung!", "error", "top-left", 2500);
                                return;
                            }
                            List<LoadCharactersModel> loadedCharactersList = new List<LoadCharactersModel>();
                            LoadCharactersModel characterModel = new LoadCharactersModel();
                            foreach (Player p in NAPI.Pools.GetAllPlayers())
                            {
                                if (p != null && p.GetOwnSharedData<bool>("Player:Spawned") == true)
                                {
                                    Character getCharacter2 = Helper.GetCharacterData(p);
                                    TempData tempData2 = Helper.GetCharacterTempData(p);
                                    if (getCharacter2 != null && tempData2 != null && getCharacter2.faction == character.faction && getCharacter2.factionduty == true)
                                    {
                                        string[] characterNameArray = new string[2];
                                        characterNameArray = character.name.Split(' ');
                                        string name = $" [{characterNameArray[0][0]}{characterNameArray[1][0]}-{getCharacter2.id}]";
                                        if (p.IsInVehicle)
                                        {
                                            VehicleData vehicleData = DealerShipController.GetVehicleDataByVehicle(player.Vehicle);
                                            if (vehicleData != null && vehicleData.owner == "faction-" + character.faction)
                                            {
                                                characterModel.Name = $"{vehicleData.plate} -{name}[{tempData2.status}]";
                                                characterModel.Screen = player.Position.X.ToString(new CultureInfo("en-US")) + "," + player.Position.Y.ToString(new CultureInfo("en-US")) + player.Position.Z.ToString(new CultureInfo("en-US"));
                                                characterModel.Closed = 1;
                                                loadedCharactersList.Add(characterModel);
                                                continue;
                                            }
                                        }
                                        characterModel.Name = $"{name}[{tempData2.status}]";
                                        characterModel.Screen = player.Position.X.ToString(new CultureInfo("en-US")) + "," + player.Position.Y.ToString(new CultureInfo("en-US")) + player.Position.Z.ToString(new CultureInfo("en-US"));
                                        characterModel.Closed = 2;
                                        loadedCharactersList.Add(characterModel);
                                    }
                                }
                            }
                            player.TriggerEvent("Client:MDCSettings", "setupdatelivemap", NAPI.Util.ToJson(loadedCharactersList));
                            break;
                        }
                    case "setarrest":
                        {
                            mdcArray = data.Split(",");
                            if (character.faction != 1)
                            {
                                Helper.SendNotificationWithoutButton(player, $"Keine Berechtigung!", "error", "top-left", 2500);
                                return;
                            }
                            Player getPlayer = Helper.GetClosestPlayer(player, 2.5f);
                            if (getPlayer != null && getPlayer != player)
                            {
                                Character character2 = Helper.GetCharacterData(getPlayer);
                                if (character2.arrested > 0)
                                {
                                    Helper.SendNotificationWithoutButton(player, $"Die Person befindet sich schon in Haft!", "error", "top-left", 2500);
                                    return;
                                }
                                character2.arrested = Convert.ToInt32(mdcArray[1]);
                                character2.cell = Convert.ToInt32(mdcArray[2]);
                                MySqlCommand command = General.Connection.CreateCommand();
                                command.CommandText = "INSERT INTO policefile (name, police, text, timestamp, commentary) VALUES (@name, @police, @text, @timestamp, @commentary)";
                                command.Parameters.AddWithValue("@name", character2.name);
                                command.Parameters.AddWithValue("@police", character.name);
                                command.Parameters.AddWithValue("@text", $"Inhaftierung für {mdcArray[1]} Minuten, Grund: {mdcArray[0]}");
                                command.Parameters.AddWithValue("@timestamp", Helper.UnixTimestamp());
                                command.Parameters.AddWithValue("@commentary", 0);
                                command.ExecuteNonQuery();
                                Helper.SendNotificationWithoutButton(player, $"Inhaftierung von {character2.name} erfolgreich vollzogen!", "success", "top-left", 3500);
                                Helper.SendNotificationWithoutButton(getPlayer, $"Du wurdest für {mdcArray[1]} Minuten inhaftiert, Grund: {mdcArray[0]}!", "success", "top-left", 7500);
                                Helper.CreateFactionLog(character.faction, $"{character.name} hat {character2.name} für {mdcArray[1]} Minuten inhaftiert, Grund: {mdcArray[0]}");
                                player.TriggerEvent("Client:ShowArrest", "n/A");
                                getPlayer.TriggerEvent("Client:SetArrested", true);
                                if (character.abusemoney > 0)
                                {
                                    Invoices invoice = new Invoices();
                                    invoice.value = Convert.ToInt32(mdcArray[1]);
                                    invoice.empfänger = character.name;
                                    invoice.modus = "Privatrechnung";
                                    invoice.text = $"Rückzahlung geklautes Geld {character.abusemoney}$";
                                    invoice.banknumber = "SA3701-100000";
                                    invoice.timestamp = Helper.UnixTimestamp();
                                    db.Insert(invoice);
                                    Helper.invoicesList.Add(invoice);
                                    character.abusemoney = 0;
                                }
                            }
                            else
                            {
                                Helper.SendNotificationWithoutButton(player, $"Ungültiger Spieler!", "error", "top-left", 2500);
                            }
                            break;
                        }
                    case "setticket":
                        {
                            mdcArray = data.Split(",");
                            if (character.faction != 1 && character.faction != 3)
                            {
                                Helper.SendNotificationWithoutButton(player, $"Keine Berechtigung!", "error", "top-left", 2500);
                                return;
                            }
                            if(character.faction == 1)
                            {
                                Player getPlayer = Helper.GetClosestPlayer(player, 2.5f);
                                if (getPlayer != null && getPlayer != player)
                                {
                                    Character character2 = Helper.GetCharacterData(getPlayer);

                                    MySqlCommand command = General.Connection.CreateCommand();
                                    command.CommandText = "INSERT INTO policefile (name, police, text, timestamp, commentary) VALUES (@name, @police, @text, @timestamp, @commentary)";
                                    command.Parameters.AddWithValue("@name", character2.name);
                                    command.Parameters.AddWithValue("@police", character.name);
                                    command.Parameters.AddWithValue("@text", $"Strafzettel {mdcArray[1]}$, {mdcArray[0]}");
                                    command.Parameters.AddWithValue("@timestamp", Helper.UnixTimestamp());
                                    command.Parameters.AddWithValue("@commentary", 0);
                                    command.ExecuteNonQuery();

                                    Invoices invoice = new Invoices();
                                    invoice.value = Convert.ToInt32(mdcArray[1]);
                                    invoice.empfänger = character2.name;
                                    invoice.modus = "Privatrechnung";
                                    invoice.text = $"Strafzettel {mdcArray[1]}$, {mdcArray[0]}";
                                    invoice.banknumber = "SA3701-100000";
                                    invoice.timestamp = Helper.UnixTimestamp();

                                    db.Insert(invoice);

                                    Helper.invoicesList.Add(invoice);

                                    Smartphone smartphone = SmartphoneController.GetSmartPhoneByNumber(character2.lastsmartphone);
                                    if (smartphone != null && smartphone.akku > 0)
                                    {
                                        BankController.OnLoadInvoices(getPlayer, false);
                                        Helper.SendNotificationWithoutButton(getPlayer, $"Du hast einen Strafzettel und eine neue Rechnung[{invoice.id}] in Höhe von {invoice.value}$ erhalten!", "info", "top-left", 6500);
                                    }
                                    Commands.cmd_animation(player, "give", true);
                                    player.TriggerEvent("Client:ShowTicket", "n/A");
                                    Helper.SendNotificationWithoutButton(player, $"Du hast einen Strafzettel ausgestellt!", "success", "top-left", 2500);
                                    return;
                                }
                                else
                                {
                                    Helper.SendNotificationWithoutButton(player, $"Ungültiger Spieler!", "error", "top-left", 2500);
                                }
                            }
                            else
                            {
                                MySqlCommand command = General.Connection.CreateCommand();
                                command.CommandText = "INSERT INTO policefile (name, police, text, timestamp, commentary) VALUES (@name, @police, @text, @timestamp, @commentary)";
                                command.Parameters.AddWithValue("@name", tempData.undercover);
                                command.Parameters.AddWithValue("@police", character.name);
                                command.Parameters.AddWithValue("@text", $"Strafzettel {mdcArray[1]}$, {mdcArray[0]}");
                                command.Parameters.AddWithValue("@timestamp", Helper.UnixTimestamp());
                                command.Parameters.AddWithValue("@commentary", 0);
                                command.ExecuteNonQuery();

                                Invoices invoice = new Invoices();
                                invoice.value = Convert.ToInt32(mdcArray[1]);
                                Groups group = GroupsController.GetGroupById(tempData.tempValue);
                                if (tempData.tempValue != -1 && group != null)
                                {
                                    invoice.modus = "Firmen/Gruppierungsrechnung";
                                    invoice.empfänger = group.name;
                                }
                                else
                                {
                                    invoice.empfänger = tempData.undercover;
                                    invoice.modus = "Privatrechnung";
                                }
                                invoice.text = $"Strafzettel {mdcArray[1]}$, {mdcArray[0]}";
                                invoice.banknumber = "SA3701-100000";
                                invoice.timestamp = Helper.UnixTimestamp();

                                db.Insert(invoice);
                                Helper.invoicesList.Add(invoice);

                                Player getPlayer = Helper.GetPlayerByCharacterName(character.name);
                                if (getPlayer != null)
                                {
                                    Character character2 = Helper.GetCharacterData(getPlayer);
                                    Smartphone smartphone = SmartphoneController.GetSmartPhoneByNumber(character2.lastsmartphone);
                                    if (smartphone != null && smartphone.akku > 0)
                                    {
                                        BankController.OnLoadInvoices(getPlayer, false);
                                        Helper.SendNotificationWithoutButton(getPlayer, $"Du hast einen Strafzettel und eine neue Rechnung[{invoice.id}] in Höhe von {invoice.value}$ erhalten!", "info", "top-left", 6500);
                                    }
                                }

                                Commands.cmd_animation(player, "give", true);
                                player.TriggerEvent("Client:ShowTicket", "n/A");
                                Helper.SendNotificationWithoutButton(player, $"Du hast einen Strafzettel ausgestellt!", "success", "top-left", 2500);
                            }
                            break;
                        }
                    case "setrecept":
                        {
                            if (character.faction != 2)
                            {
                                Helper.SendNotificationWithoutButton(player, $"Keine Berechtigung!", "error", "top-left", 2500);
                                return;
                            }
                            Player getPlayer = Helper.GetClosestPlayer(player, 2.5f);
                            if (getPlayer == null)
                            {
                                getPlayer = player;
                            }
                            if (getPlayer != null)
                            {
                                TempData tempData2 = Helper.GetCharacterTempData(getPlayer);
                                Character character2 = Helper.GetCharacterData(getPlayer);

                                MySqlCommand command = General.Connection.CreateCommand();
                                command.CommandText = "INSERT INTO policefile (name, police, text, timestamp, commentary) VALUES (@name, @police, @text, @timestamp, @commentary)";
                                command.Parameters.AddWithValue("@name", character2.name);
                                command.Parameters.AddWithValue("@police", character.name);
                                command.Parameters.AddWithValue("@text", $"Rezept für {data} ausgestellt");
                                command.Parameters.AddWithValue("@timestamp", Helper.UnixTimestamp());
                                command.Parameters.AddWithValue("@commentary", 2);
                                command.ExecuteNonQuery();
                                string props = data;       
                                Items newitem = ItemsController.CreateNewItem(getPlayer, character2.id, "Rezept", "Player", 1, ItemsController.GetFreeItemID(getPlayer), props);
                                if (newitem != null)
                                {
                                    tempData2.itemlist.Add(newitem);
                                }
                                Commands.cmd_animation(player, "give", true);
                                Helper.SendNotificationWithoutButton(getPlayer, $"Dir wurde ein Rezept für {data} ausgestellt!", "success", "top-left", 2500);
                                Helper.SendNotificationWithoutButton(player, $"Du hast ein Rezept für {data} ausgestellt!", "success", "top-left", 2500);
                                player.TriggerEvent("Client:ShowRezept", "n/A");
                                return;
                            }
                            else
                            {
                                Helper.SendNotificationWithoutButton(player, $"Ungültiger Spieler!", "error", "top-left", 2500);
                            }
                            break;
                        }
                    case "showmdc":
                        {
                            if (character.faction != 1 && character.faction != 2 && character.faction != 3)
                            {
                                Helper.SendNotificationWithoutButton(player, $"Keine Berechtigung!", "error", "top-left", 2500);
                                return;
                            }
                            string param = "n/A";
                            if (data != "off")
                            {
                                int facionMember = 0;
                                FactionsModel faction = Helper.GetFactionById(character.faction);
                                if (faction != null)
                                {
                                    MySqlCommand command = General.Connection.CreateCommand();
                                    command.CommandText = "SELECT COUNT(*) as count FROM characters where faction = @faction";
                                    command.Parameters.AddWithValue("@faction", character.faction);

                                    using (MySqlDataReader reader = command.ExecuteReader())
                                    {
                                        reader.Read();
                                        if(reader.HasRows)
                                        {
                                            facionMember = reader.GetInt32("count");
                                        }
                                        reader.Close();
                                    }
                                    List<Dispatch> dispatchListTemp = new List<Dispatch>();
                                    foreach (Dispatch dispatch in dispatchList)
                                    {
                                        if (dispatch.to == character.faction)
                                        {
                                            dispatchListTemp.Add(dispatch);
                                        }
                                    }
                                    if (character.faction == 1)
                                    {
                                        param = $"{facionMember},{Helper.GetFactionCountDuty(character.faction)},{fahndungList.Count},{dispatchListTemp.Count}";
                                    }
                                    else if(character.faction == 2)
                                    {
                                        param = $"{facionMember},{Helper.GetFactionCountDuty(character.faction)},0,{dispatchListTemp.Count}";
                                    }
                                    else if (character.faction == 3)
                                    {
                                        param = $"{facionMember},{Helper.GetFactionCountDuty(character.faction)},0,{dispatchListTemp.Count}";
                                    }
                                }
                            }
                            player.TriggerEvent("Client:MDCSettings", "showmdcclient", param);
                            break;
                        }
                    case "orderweapons":
                        {
                            if (character.rang >= 10)
                            {
                                if (weaponOrderStatus == "DNE")
                                {
                                    Helper.SendNotificationWithoutButton(player, $"Du kannst pro Tag nur eine Bestellung aufgeben!", "error", "top-left", 3500);
                                    return;
                                }
                                if (weaponOrderStatus == "DLY")
                                {
                                    Helper.SendNotificationWithoutButton(player, $"Ihr habt doch eben erst eine Lieferung abgeholt?", "error", "top-left", 3500);
                                    return;
                                }
                                if (weaponOrderStatus != "n/A")
                                {
                                    Helper.SendNotificationWithoutButton(player, $"Es gibt schon eine offene Bestellung, du musst zuerst die alte abholen!", "error", "top-left", 3500);
                                    return;
                                }
                                weaponOrder = data;
                                weaponOrderStatus = "RDY";
                                weaponOrderFaction = character.faction;
                                Helper.CreateFactionLog(character.faction, $"{character.name} hat eine Waffenbestellung aufgegeben: {data}!");
                                Helper.SendNotificationWithoutButton(player, $"Bestellung aufgegeben, die Bestellung kann bei der Spedition Spandex abgeholt werden!", "success", "top-left", 3500);
                            }
                            else
                            {
                                Helper.SendNotificationWithoutButton(player, $"Keine Berechtigung!", "error", "top-left", 2500);
                            }
                            break;
                        }
                    case "getdocuments":
                        {
                            List<Documents> documentList = new List<Documents>();
                            foreach (Documents documents in db.Fetch<Documents>("SELECT * FROM documents WHERE owner = @0 ORDER BY timestamp ASC LIMIT 50", "faction-"+character.faction))
                            {
                                documentList.Add(documents);
                            }
                            player.TriggerEvent("Client:MDCSettings", "showdocuments", NAPI.Util.ToJson(documentList));
                            break;
                        }
                    case "createdocument":
                        {
                            if (character.rang >= 10)
                            {
                                mdcArray = data.Split("|");
                                if (!mdcArray[0].Contains("docs.google.com") && !mdcArray[0].Contains("nemesus-world.de") && !mdcArray[0].Contains("nemesus.de"))
                                {
                                    Helper.SendNotificationWithoutButton(player, $"Ungültiger Link!", "error", "top-left", 2500);
                                    return;
                                }
                                Documents document = new Documents();
                                document.title = mdcArray[1];
                                document.link = mdcArray[0];
                                document.creator = character.name;
                                document.timestamp = Helper.UnixTimestamp();
                                document.owner = "faction-" + character.faction;
                                db.Insert(document);
                                Helper.SendNotificationWithoutButton(player, "Das Dokument wurde eingetragen!", "success", "top-left", 2500);
                                OnMDCSettings(player, "getdocuments", "n/A");
                                Helper.CreateFactionLog(character.faction, $"{character.name} hat ein neues Dokument({document.id}) erstellt - {mdcArray[1]}!");
                            }
                            else
                            {
                                Helper.SendNotificationWithoutButton(player, $"Keine Berechtigung!", "error", "top-left", 2500);
                            }
                            break;
                        }
                    case "deletedocument":
                        {
                            if (character.rang >= 10)
                            {
                                MySqlCommand command = General.Connection.CreateCommand();
                                command.CommandText = "DELETE FROM documents WHERE id = @id LIMIT 1";
                                command.Parameters.AddWithValue("@id", Convert.ToInt32(data));
                                command.ExecuteNonQuery();

                                Helper.CreateFactionLog(character.faction, $"{character.name} hat das Dokument mit der ID {data} gelöscht!");

                                Helper.SendNotificationWithoutButton(player, "Das Dokument wurde gelöscht!", "success", "top-left", 2500);
                                OnMDCSettings(player, "getdocuments", "n/A");
                            }
                            else
                            {
                                Helper.SendNotificationWithoutButton(player, $"Keine Berechtigung!", "error", "top-left", 2500);
                            }
                            break;
                        }
                    case "getweapons":
                        {
                            string weaponCount = "";
                            foreach (ShopItems shopItem in Helper.shopItemList)
                            {
                                if (shopItem != null && shopItem.shoplabel == "Waffenkammer-" + character.faction)
                                {
                                    weaponCount += "" + shopItem.itemprice + ",";
                                }
                            }
                            weaponCount = weaponCount.Substring(0, weaponCount.Length - 1);
                            player.TriggerEvent("Client:MDCSettings", "showweapons", weaponCount);
                            break;
                        }
                    case "setlic":
                        {
                            mdcArray = data.Split(",");

                            Character getCharacter = null;
                            bool found = false;

                            foreach (Player p in NAPI.Pools.GetAllPlayers())
                            {
                                if (p != null && p.GetOwnSharedData<bool>("Player:Spawned") == true && p.GetSharedData<bool>("Player:Death") == false)
                                {
                                    Character getCharacter2 = Helper.GetCharacterData(p);
                                    if (getCharacter2 != null && getCharacter2.name == mdcArray[2])
                                    {
                                        getCharacter = getCharacter2;
                                        found = true;
                                        break;
                                    }
                                }
                            }

                            if (getCharacter == null)
                            {
                                getCharacter = db.Single<Character>("WHERE name = @0", mdcArray[2]);
                            }
                            if (getCharacter != null)
                            {
                                int value = Convert.ToInt32(mdcArray[1]);
                                int value2 = Convert.ToInt32(mdcArray[0]);
                                if (value == 0)
                                {
                                    MySqlCommand command = General.Connection.CreateCommand();
                                    if (Convert.ToInt32(Helper.SetAndGetCharacterLicense(player, value2, 1337, getCharacter)) <= 1)
                                    {
                                        Helper.SendNotificationWithoutButton(player, $"14 Tage Sperre für den {GetLicName(value2)} gesetzt!", "success", "top-left", 2500);
                                        Helper.SetAndGetCharacterLicense(player, value2, Helper.UnixTimestamp() + (14 * 86400), getCharacter);

                                        command.CommandText = "INSERT INTO policefile (name, police, text, timestamp, commentary) VALUES (@name, @police, @text, @timestamp, @commentary)";
                                        command.Parameters.AddWithValue("@name", mdcArray[2]);
                                        command.Parameters.AddWithValue("@police", character.name);
                                        command.Parameters.AddWithValue("@text", $"14 Tage Sperre für den {GetLicName(value2)}, Grund: {mdcArray[3]}");
                                        command.Parameters.AddWithValue("@timestamp", Helper.UnixTimestamp());
                                        command.Parameters.AddWithValue("@commentary", 0);
                                        command.ExecuteNonQuery();
                                    }
                                    else
                                    {
                                        Helper.SendNotificationWithoutButton(player, $"Sperre für den den {GetLicName(value2)} aufgehoben!", "success", "top-left", 2500);
                                        Helper.SetAndGetCharacterLicense(player, value2, Helper.UnixTimestamp(), getCharacter);

                                        command.CommandText = "INSERT INTO policefile (name, police, text, timestamp, commentary) VALUES (@name, @police, @text, @timestamp, @commentary)";
                                        command.Parameters.AddWithValue("@name", mdcArray[2]);
                                        command.Parameters.AddWithValue("@police", character.name);
                                        command.Parameters.AddWithValue("@text", $"Sperre für den {GetLicName(value2)} aufgehoben, Grund: {mdcArray[3]}");
                                        command.Parameters.AddWithValue("@timestamp", Helper.UnixTimestamp());
                                        command.Parameters.AddWithValue("@commentary", 0);
                                        command.ExecuteNonQuery();
                                    }
                                }
                                else if (value == 1)
                                {
                                    if (getCharacter.sapoints >= 10)
                                    {
                                        Helper.SendNotificationWithoutButton(player, "Die Person hat schon 10 Punkte in SA!", "error", "top-left", 2500);
                                        return;
                                    }
                                    else
                                    {
                                        Helper.SendNotificationWithoutButton(player, $"Punkt in SA gesetzt!", "success", "top-left", 3500);
                                        getCharacter.sapoints++;
                                        MySqlCommand command = General.Connection.CreateCommand();
                                        command.CommandText = "INSERT INTO policefile (name, police, text, timestamp, commentary) VALUES (@name, @police, @text, @timestamp, @commentary)";
                                        command.Parameters.AddWithValue("@name", mdcArray[2]);
                                        command.Parameters.AddWithValue("@police", character.name);
                                        command.Parameters.AddWithValue("@text", $"+1 Punkt in SA, Grund: {mdcArray[3]}");
                                        command.Parameters.AddWithValue("@timestamp", Helper.UnixTimestamp());
                                        command.Parameters.AddWithValue("@commentary", 0);
                                        command.ExecuteNonQuery();

                                        if (getCharacter.sapoints >= 10)
                                        {
                                            command.CommandText = "INSERT INTO policefile (name, police, text, timestamp, commentary) VALUES (@name, @police, @text, @timestamp, @commentary)";
                                            command.Parameters.AddWithValue("@name", mdcArray[2]);
                                            command.Parameters.AddWithValue("@police", character.name);
                                            command.Parameters.AddWithValue("@text", $"Führer/Motorradsch/Truckereinsperre (14 Tage), wegen zu vieler Punkte in SA");
                                            command.Parameters.AddWithValue("@timestamp", Helper.UnixTimestamp());
                                            command.Parameters.AddWithValue("@commentary", 0);
                                            command.ExecuteNonQuery();

                                            Helper.SetAndGetCharacterLicense(player, 0, Helper.UnixTimestamp() + (14 * 86400), getCharacter);
                                            Helper.SetAndGetCharacterLicense(player, 1, Helper.UnixTimestamp() + (14 * 86400), getCharacter);
                                            Helper.SetAndGetCharacterLicense(player, 2, Helper.UnixTimestamp() + (14 * 86400), getCharacter);
                                        }
                                    }
                                }
                                else if (value == 2)
                                {
                                    if (getCharacter.sapoints <= 0)
                                    {
                                        Helper.SendNotificationWithoutButton(player, "Die Person hat keine Punkte in SA!", "error", "top-left", 2500);
                                        return;
                                    }
                                    else
                                    {
                                        Helper.SendNotificationWithoutButton(player, $"Punkt in SA gelöscht!", "success", "top-left", 3500);
                                        getCharacter.sapoints--;
                                        MySqlCommand command = General.Connection.CreateCommand();
                                        command.CommandText = "INSERT INTO policefile (name, police, text, timestamp, commentary) VALUES (@name, @police, @text, @timestamp, @commentary)";
                                        command.Parameters.AddWithValue("@name", mdcArray[2]);
                                        command.Parameters.AddWithValue("@police", character.name);
                                        command.Parameters.AddWithValue("@text", $"-1 Punkt in SA, Grund: {mdcArray[3]}");
                                        command.Parameters.AddWithValue("@timestamp", Helper.UnixTimestamp());
                                        command.Parameters.AddWithValue("@commentary", 0);
                                        command.ExecuteNonQuery();
                                    }
                                }
                                if (found == false)
                                {
                                    db.Save(getCharacter);
                                }
                                List<Policefile> policefiles = new List<Policefile>();
                                foreach (Policefile policefile in db.Fetch<Policefile>("SELECT * FROM policefile WHERE name = @0 ORDER BY timestamp DESC LIMIT 125", mdcArray[2]))
                                {
                                    policefiles.Add(policefile);
                                }
                                player.TriggerEvent("Client:MDCSettings", "showpolicefile", NAPI.Util.ToJson(policefiles));
                                string jsonbackup = getCharacter.json;
                                string jsonbackup2 = getCharacter.items;
                                string jsonbackup3 = getCharacter.animations;
                                string jsonbackup4 = getCharacter.dutyjson;
                                getCharacter.json = "null";
                                getCharacter.items = "null";
                                getCharacter.animations = "null";
                                getCharacter.dutyjson = "null";
                                player.TriggerEvent("Client:MDCSettings", "showpersonal", NAPI.Util.ToJson(getCharacter));
                                List<LoadCharactersModel> loadedCharactersList = new List<LoadCharactersModel>();
                                NAPI.Task.Run(() =>
                                {
                                    getCharacter.json = jsonbackup;
                                    getCharacter.items = jsonbackup2;
                                    getCharacter.animations = jsonbackup3;
                                    getCharacter.dutyjson = jsonbackup4;
                                }, delayTime: 105);
                            }
                            break;
                        }
                    case "closedzone":
                        {
                            String msg = "";
                            String msg2 = "";
                            float zonesize = 0.65f;
                            mdcArray = data.Split(",");
                            if(mdcArray[1] == "Klein")
                            {
                                zonesize = 0.65f;
                            }
                            else if (mdcArray[1] == "Mittel")
                            {
                                zonesize = 0.9f;
                            }
                            else
                            {
                                zonesize = 1.2f;
                            }
                            if (Events.closesZone == null)
                            {
                                Events.closesZone = NAPI.Blip.CreateBlip(9, player.Position, zonesize, 1);
                                NAPI.Blip.SetBlipShortRange(Events.closesZone, true);
                                NAPI.Blip.SetBlipScale(Events.closesZone, zonesize);
                                NAPI.Blip.SetBlipName(Events.closesZone, "Sperrzone");
                                NAPI.Blip.SetBlipTransparency(Events.closesZone, 175);
                                msg = $"Es wurde eine neue Sperrzone, für den Bereich um {mdcArray[0]} ausgerufen!";
                                msg2 = $"Neue Sperrzone bei/beim: {mdcArray[0]}!";
                            }
                            else
                            {
                                Events.closesZone.Delete();
                                Events.closesZone = null;
                                msg = $"Die Sperrzone wurde aufgehoben!";
                                msg2 = $"Die Sperrzone wurde aufgehoben!";
                            }
                            //Helper.SendAdminMessage3("" + msg2, 180000);
                            foreach (Player p in NAPI.Pools.GetAllPlayers())
                            {
                                if (p != null && p.GetOwnSharedData<bool>("Player:Spawned") == true)
                                {
                                    Character c = Helper.GetCharacterData(p);
                                    if (c != null)
                                    {
                                        if (c.lastsmartphone != "")
                                        {
                                            SmartphoneController.OnSmartphoneMessage(player, Helper.UnixTimestamp(), c.lastsmartphone, "0189911", "" + msg2);
                                        }
                                    }
                                }
                            }
                            break;
                        }
                    case "messagetoall":
                        {
                            if (character.rang < 3)
                            {
                                Helper.SendNotificationWithoutButton(player, "Keine Berechtigung!", "error", "top-left", 2750);
                                return;
                            }
                            if (MDCController.messageCooldown > 0)
                            {
                                if (Helper.UnixTimestamp() < MDCController.messageCooldown)
                                {
                                    Helper.SendNotificationWithoutButton(player, "Du kannst nur alle 2 Minuten eine Staatsankündigung erstellen!", "error", "top-end");
                                    return;
                                }
                            }
                            string msgnew = "Staatskündigung: " + data;
                            MDCController.messageCooldown = Helper.UnixTimestamp() + (120);
                            if (character.faction == 1)
                            {
                                SendPoliceMDCMessage(player, $"{character.name} hat eine Staatsankündigung erstellt!");
                            }
                            else if(character.faction == 2)
                            {
                                SendMedicMDCMessage(player, $"{character.name} hat eine Staatsankündigung erstellt!");
                            }
                            foreach (Player p in NAPI.Pools.GetAllPlayers())
                            {
                                if (p != null && p.GetOwnSharedData<bool>("Player:Spawned") == true)
                                {
                                    Character c = Helper.GetCharacterData(p);
                                    if (c != null)
                                    {
                                        if (c.lastsmartphone != "")
                                        {
                                            if(msgnew.Contains("/n") || msgnew.Contains("\n"))
                                            {
                                                msgnew = msgnew.Substring(0, msgnew.Length - 1);
                                            }
                                            SmartphoneController.OnSmartphoneMessage(player, Helper.UnixTimestamp(), c.lastsmartphone, "0189911", ""+ msgnew);
                                        }
                                    }
                                }
                            }
                            //Helper.SendAdminMessage3(msgnew, 180000);
                            break;
                        }
                    case "locateweapon":
                        {
                            try
                            {
                                Weapon weapon = db.Single<Weapon>("WHERE ident = @0", data);
                                player.TriggerEvent("Client:MDCSettings", "showweapon", NAPI.Util.ToJson(weapon));
                            }
                            catch (Exception)
                            {
                                MySqlCommand command = General.Connection.CreateCommand();
                                command.CommandText = "DELETE FROM weapons WHERE ident = @ident LIMIT 1";
                                command.Parameters.AddWithValue("ident", data);
                                command.ExecuteNonQuery();
                                Helper.SendNotificationWithoutButton(player, "Die Waffe wurde nicht in der Waffendatenbank gefunden!", "error", "top-left", 2750);
                            }
                            break;
                        }
                    case "undercover":
                        {
                            try
                            {
                                if (tempData.undercover == "")
                                {
                                    if (!Regex.IsMatch(data, "^([A-Z][a-z]+[ ][A-Z][a-z]+)$"))
                                    {
                                        Helper.SendNotificationWithoutButton(player, "Ungültige Identität!", "error", "top-left", 2750);
                                        return;
                                    }
                                    MySqlCommand command = General.Connection.CreateCommand();
                                    command.CommandText = "SELECT id FROM characters WHERE name=@name LIMIT 1";
                                    command.Parameters.AddWithValue("@name",data);

                                    using (MySqlDataReader reader = command.ExecuteReader())
                                    {
                                        if (reader.HasRows)
                                        {
                                            Helper.SendNotificationWithoutButton(player, "Ungültige Identität!", "error", "top-left", 2750);
                                            return;
                                        }
                                    }
                                    character.ucp_privat = 1;
                                    Helper.SendNotificationWithoutButton(player, "Undercover Identität angenommen!", "success", "top-left", 2750);
                                    tempData.undercover = data;
                                    player.SetData<string>("Client:OldName", character.name);
                                    character.name = data;
                                    CharacterController.SaveCharacter(player);
                                }
                                else
                                {
                                    Helper.SendNotificationWithoutButton(player, "Undercover Identität abgelegt!", "success", "top-left", 2750);
                                    tempData.undercover = "";
                                    character.name = player.GetData<string>("Client:OldName");
                                    player.ResetData("Client:OldName");
                                    CharacterController.SaveCharacter(player);
                                }
                            }
                            catch (Exception)
                            {
                                Helper.SendNotificationWithoutButton(player, "Ungültige Identität!", "error", "top-left", 2750);
                            }
                            break;
                        }
                    case "locatehandy":
                        {
                            Smartphone smartphone = SmartphoneController.GetSmartPhoneByNumber(data);
                            if (smartphone != null && smartphone.akku > 0)
                            {
                                JObject obj = JObject.Parse(smartphone.phoneprops);
                                if ((int)obj["phonestatus"] != 0)
                                {
                                    Player locatedPlayer = SmartphoneController.GetPlayerFromSmartPhone(data);
                                    if (locatedPlayer != null)
                                    {
                                        Helper.SendNotificationWithoutButton(player, $"Lokalisierung erfolgreich!", "success", "top-left", 2500);
                                        player.TriggerEvent("Client:CreateWaypoint", locatedPlayer.Position.X, locatedPlayer.Position.Y, -1);
                                    }
                                    else
                                    {
                                        Helper.SendNotificationWithoutButton(player, $"Lokalisierung fehlgeschlagen!", "error", "top-left", 2500);
                                    }
                                }
                                else
                                {
                                    Helper.SendNotificationWithoutButton(player, $"Lokalisierung fehlgeschlagen!", "error", "top-left", 2500);
                                }
                            }
                            else
                            {
                                Helper.SendNotificationWithoutButton(player, $"Lokalisierung fehlgeschlagen!", "error", "top-left", 2500);
                            }
                            break;
                        }
                    case "deleteakteneintrag":
                        {
                            if (character.rang < 5)
                            {
                                Helper.SendNotificationWithoutButton(player, $"Du benötigst mind. Rang 5 um Akteneinträge löschen zu können!", "error", "top-left", 2500);
                                return;
                            }

                            mdcArray = data.Split("|");

                            MySqlCommand command = General.Connection.CreateCommand();
                            command.CommandText = "DELETE FROM policefile WHERE id = @id LIMIT 1";
                            command.Parameters.AddWithValue("@id", Convert.ToInt32(mdcArray[0]));
                            command.ExecuteNonQuery();

                            Helper.CreateFactionLog(character.faction, $"{character.name} hat den Akteneintrag[{mdcArray[0]}] ({mdcArray[2]}) von {mdcArray[1]} gelöscht!");
                            List<Policefile> policefiles = new List<Policefile>();
                            foreach (Policefile policefile in db.Fetch<Policefile>("SELECT * FROM policefile WHERE name = @0 ORDER BY timestamp DESC LIMIT 125", mdcArray[1]))
                            {
                                policefiles.Add(policefile);
                            }
                            player.TriggerEvent("Client:MDCSettings", "showpolicefile", NAPI.Util.ToJson(policefiles));
                            Helper.SendNotificationWithoutButton(player, $"Der Akteneintrag wurde gelöscht!", "success", "top-left", 2500);
                            break;
                        }
                    case "createakteneintrag":
                        {
                            mdcArray = data.Split("|");

                            MySqlCommand command = General.Connection.CreateCommand();
                            command.CommandText = "INSERT INTO policefile (name, police, text, timestamp, commentary) VALUES (@name, @police, @text, @timestamp, @commentary)";
                            command.Parameters.AddWithValue("@name", mdcArray[0]);
                            command.Parameters.AddWithValue("@police", character.name);
                            command.Parameters.AddWithValue("@text", mdcArray[1]);
                            command.Parameters.AddWithValue("@timestamp", Helper.UnixTimestamp());
                            command.Parameters.AddWithValue("@commentary", Convert.ToInt32(mdcArray[2]));
                            command.ExecuteNonQuery();

                            List<Policefile> policefiles = new List<Policefile>();
                            foreach (Policefile policefile in db.Fetch<Policefile>("SELECT * FROM policefile WHERE name = @0 ORDER BY timestamp DESC LIMIT 125", mdcArray[0]))
                            {
                                policefiles.Add(policefile);
                            }
                            player.TriggerEvent("Client:MDCSettings", "showpolicefile", NAPI.Util.ToJson(policefiles));
                            Helper.SendNotificationWithoutButton(player, $"Der Akteneintrag wurde hinzugefügt!", "success", "top-left", 2500);
                            break;
                        }
                    case "searchplate":
                        {
                            if (data.ToLower().Contains("select") || data.ToLower().Contains("delete") || data.ToLower().Contains("drop") || data.ToLower().Contains("update"))
                            {
                                player.TriggerEvent("Client:MDCSettings", "showplate", "n/A");
                            }
                            else
                            {
                                MySqlCommand command = General.Connection.CreateCommand();
                                command.CommandText = "SELECT id,owner,vehiclename,plate,tuev FROM vehicles WHERE plate = @plate LIMIT 1";
                                command.Parameters.AddWithValue("@plate", data);

                                List<LoadCharactersModel> loadedCharactersList = new List<LoadCharactersModel>();
                                LoadCharactersModel characterModel = new LoadCharactersModel();

                                using (MySqlDataReader reader = command.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        characterModel.ID = reader.GetInt32("tuev");
                                        characterModel.Name = reader.GetString("owner");
                                        characterModel.Screen = reader.GetString("vehiclename");
                                        characterModel.Job = reader.GetString("plate");
                                        characterModel.Closed = reader.GetInt32("id");
                                        loadedCharactersList.Add(characterModel);
                                    }
                                    reader.Close();
                                }
                                if (characterModel.Closed > 0)
                                {
                                    string[] plateArray = new string[2];
                                    plateArray = characterModel.Name.Trim().Split("-");
                                    string ownerName = "n/A";
                                    if (plateArray[0] == "faction")
                                    {
                                        ownerName = Helper.GetFactionName(Convert.ToInt32(plateArray[1]));
                                    }
                                    else if (plateArray[0] == "group")
                                    {
                                        ownerName = GroupsController.GetGroupNameById(Convert.ToInt32(plateArray[1]));
                                    }
                                    else if (plateArray[0] == "character")
                                    {
                                        ownerName = Helper.GetCharacterName(Convert.ToInt32(plateArray[1]));
                                    }
                                    characterModel.Name = ownerName;
                                    player.TriggerEvent("Client:MDCSettings", "showplate", NAPI.Util.ToJson(loadedCharactersList));
                                }
                                else
                                {
                                    Helper.SendNotificationWithoutButton(player, "Kein Fahrzeug mit diesem Nummernschild gefunden!", "error", "top-left", 3500);
                                    player.TriggerEvent("Client:MDCSettings", "showplate", "n/A");
                                }
                            }
                            break;
                        }
                    case "searchplayer":
                        {
                            if (data.ToLower().Contains("select") || data.ToLower().Contains("delete") || data.ToLower().Contains("drop") || data.ToLower().Contains("update"))
                            {
                                player.TriggerEvent("Client:MDCSettings", "showplayer", "n/A");
                            }
                            else
                            {
                                MySqlCommand command = General.Connection.CreateCommand();
                                command.CommandText = "SELECT id,name,screen FROM characters WHERE LOWER(name) LIKE @searchname LIMIT 8";
                                command.Parameters.AddWithValue("@searchname", "%" + data.ToLower() + "%");

                                List<LoadCharactersModel> loadedCharactersList = new List<LoadCharactersModel>();

                                using (MySqlDataReader reader = command.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        LoadCharactersModel characterModel = new LoadCharactersModel();
                                        characterModel.ID = reader.GetInt32("id");
                                        characterModel.Name = reader.GetString("name");
                                        characterModel.Screen = reader.GetString("screen");
                                        loadedCharactersList.Add(characterModel);
                                    }
                                    reader.Close();
                                }
                                player.TriggerEvent("Client:MDCSettings", "showplayer", NAPI.Util.ToJson(loadedCharactersList));
                            }
                            break;
                        }
                    case "selectpersonal":
                        {
                            Character getCharacter = db.Single<Character>("WHERE id = @0", Convert.ToInt32(data));

                            if (getCharacter.faction == character.faction && getCharacter.rang >= character.rang && getCharacter.id != character.id && character.rang != 12)
                            {
                                Helper.SendNotificationWithoutButton(player, "Keine Berechtigung!", "error", "top-left", 2500);
                                return;
                            }

                            string jsonbackup = getCharacter.json;
                            string jsonbackup2 = getCharacter.items;
                            string jsonbackup3 = getCharacter.animations;
                            string jsonbackup4 = getCharacter.dutyjson;

                            getCharacter.json = "null";
                            getCharacter.items = "null";
                            getCharacter.animations = "null";
                            getCharacter.dutyjson = "null";
                            player.TriggerEvent("Client:MDCSettings", "showpersonal", NAPI.Util.ToJson(getCharacter));

                            NAPI.Task.Run(() =>
                            {
                                getCharacter.json = jsonbackup;
                                getCharacter.items = jsonbackup2;
                                getCharacter.animations = jsonbackup3;
                                getCharacter.dutyjson = jsonbackup4;
                            }, delayTime: 105);

                            List<Policefile> policefiles = new List<Policefile>();
                            foreach (Policefile policefile in db.Fetch<Policefile>("SELECT * FROM policefile WHERE name = @0 ORDER BY timestamp DESC LIMIT 125", getCharacter.name))
                            {
                                policefiles.Add(policefile);
                            }
                            player.TriggerEvent("Client:MDCSettings", "showpolicefile", NAPI.Util.ToJson(policefiles));

                            List<LoadCharactersModel> loadedCharactersList = new List<LoadCharactersModel>();
                            foreach (Smartphone smartphone in SmartphoneController.smartphoneList)
                            {
                                if (smartphone != null && smartphone.prepaid == -1 && smartphone.owner == getCharacter.name)
                                {
                                    LoadCharactersModel smartphoneToAdd = new LoadCharactersModel();
                                    smartphoneToAdd.ID = smartphone.id;
                                    smartphoneToAdd.Name = "0189" + smartphone.phonenumber;
                                    loadedCharactersList.Add(smartphoneToAdd);
                                }
                            }
                            player.TriggerEvent("Client:MDCSettings", "showhandy", NAPI.Util.ToJson(loadedCharactersList));
                            break;
                        }
                    case "selectusers":
                        {
                            List<LoadCharactersModel> loadedCharactersList = new List<LoadCharactersModel>();
                            string[] characterNameArray = new string[2];
                            foreach (Player p in NAPI.Pools.GetAllPlayers())
                            {
                                if (p != null && p.GetOwnSharedData<bool>("Player:Spawned") == true)
                                {
                                    Character getCharacter2 = Helper.GetCharacterData(p);
                                    TempData tempData2 = Helper.GetCharacterTempData(p);
                                    if (getCharacter2 != null && tempData2 != null && getCharacter2.faction == character.faction && getCharacter2.factionduty == true)
                                    {
                                        LoadCharactersModel loadedCharacter = new LoadCharactersModel();
                                        loadedCharacter.Name = getCharacter2.name;
                                        loadedCharacter.ID = getCharacter2.id;
                                        loadedCharacter.Faction = tempData2.status;
                                        characterNameArray = getCharacter2.name.Split(' ');
                                        loadedCharacter.Job = $" [{characterNameArray[0][0]}{characterNameArray[1][0]}-{getCharacter2.id}]";
                                        loadedCharacter.Screen = $"{p.Position.X.ToString(new CultureInfo("en-US"))},{p.Position.Y.ToString(new CultureInfo("en-US"))}";
                                        loadedCharactersList.Add(loadedCharacter);
                                    }
                                }
                            }
                            player.TriggerEvent("Client:MDCSettings", "showusers", NAPI.Util.ToJson(loadedCharactersList));
                            break;
                        }
                    case "selectblitzer":
                        {
                            List<BlitzerLoaded> loadedBlitzerList = new List<BlitzerLoaded>();
                            foreach (Blitzer blitzer in Helper.blitzerList)
                            {
                                if (blitzer != null && blitzer.colshape != null)
                                {
                                    BlitzerLoaded blitzerLoaded = new BlitzerLoaded();
                                    blitzerLoaded.id = blitzer.id;
                                    blitzerLoaded.who = blitzer.who;
                                    blitzerLoaded.maxspeed = blitzer.maxspeed;
                                    blitzerLoaded.from = blitzer.from;
                                    blitzerLoaded.position = $"{blitzer.speedobject.Position.X.ToString(new CultureInfo("en-US"))},{blitzer.speedobject.Position.Y.ToString(new CultureInfo("en-US"))}";
                                    loadedBlitzerList.Add(blitzerLoaded);
                                }
                            }
                            player.TriggerEvent("Client:MDCSettings", "showblitzer", NAPI.Util.ToJson(loadedBlitzerList));
                            break;
                        }
                    case "selectcctv":
                        {
                            List<BlitzerLoaded> cctvList = new List<BlitzerLoaded>();
                            foreach (CCTV cctv in Helper.cctvList)
                            {
                                if (cctv != null)
                                {
                                    BlitzerLoaded blitzerLoaded = new BlitzerLoaded();
                                    blitzerLoaded.id = cctv.id;
                                    blitzerLoaded.who = cctv.who;
                                    blitzerLoaded.from = cctv.from;
                                    blitzerLoaded.position = $"{cctv.cctvobject.Position.X.ToString(new CultureInfo("en-US"))},{cctv.cctvobject.Position.Y.ToString(new CultureInfo("en-US"))}";
                                    cctvList.Add(blitzerLoaded);
                                }
                            }
                            player.TriggerEvent("Client:MDCSettings", "showcctv", NAPI.Util.ToJson(cctvList));
                            break;
                        }
                    case "lookcctv":
                        {
                            List<BlitzerLoaded> cctvList = new List<BlitzerLoaded>();
                            if (player.IsInVehicle)
                            {
                                Helper.SendNotificationWithoutButton(player, $"Der CCTV Empfang ist im Fahrzeug zu schwach!", "error", "top-left", 2500);
                                return;
                            }
                            foreach (CCTV cctv in Helper.cctvList)
                            {
                                if (cctv != null && cctv.id == Convert.ToInt32(data))
                                {
                                    tempData.furnitureOldPosition = player.Position;
                                    Helper.SendNotificationWithoutButton(player, $"CCTV Überwachung gestartet, benutze Taste [ESC] um diese zu beenden!", "success", "top-left", 2500);
                                    player.TriggerEvent("Client:StartStopCCTV");
                                    string[] adminSettings = new string[3];
                                    adminSettings = player.GetSharedData<string>("Player:Adminsettings").Split(",");
                                    player.SetSharedData("Player:Adminsettings", $"{adminSettings[0]},2,{adminSettings[2]}");
                                    Helper.SetPlayerPosition(player, new Vector3(cctv.cctvobject.Position.X, cctv.cctvobject.Position.Y, cctv.cctvobject.Position.Z + 7.25));
                                    return;
                                }
                            }
                            break;
                        }
                    case "stopcctv":
                        {
                            Helper.SetPlayerPosition(player, tempData.furnitureOldPosition);
                            string[] adminSettings = new string[3];
                            adminSettings = player.GetSharedData<string>("Player:Adminsettings").Split(",");
                            player.SetSharedData("Player:Adminsettings", $"{adminSettings[0]},0,{adminSettings[2]}");
                            if (player.GetSharedData<bool>("Player:Death") == false)
                            {
                                Helper.SendNotificationWithoutButton(player, $"CCTV Überwachung beendet!", "success", "top-left", 2500);
                                Helper.PlayPhoneAnim(player, true);
                            }
                            break;
                        }
                    case "selectcars":
                        {
                            List<LoadCharactersModel> loadedCharactersList = new List<LoadCharactersModel>();

                            MySqlCommand command = General.Connection.CreateCommand();
                            command.CommandText = "SELECT id,vehiclename,plate,tuev FROM vehicles WHERE owner = @owner AND plate != 'n/A' LIMIT 15";
                            command.Parameters.AddWithValue("@owner", "character-" + Convert.ToInt32(data));

                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    LoadCharactersModel characterModel = new LoadCharactersModel();
                                    characterModel.ID = reader.GetInt32("id");
                                    characterModel.Name = reader.GetString("vehiclename");
                                    characterModel.Screen = reader.GetString("plate");
                                    characterModel.Faction = reader.GetString("tuev");
                                    loadedCharactersList.Add(characterModel);
                                }
                                reader.Close();
                            }
                            player.TriggerEvent("Client:MDCSettings", "showcars", NAPI.Util.ToJson(loadedCharactersList));
                            break;
                        }
                    case "selecthouses":
                        {
                            List<LoadCharactersModel> loadedCharactersList = new List<LoadCharactersModel>();

                            MySqlCommand command = General.Connection.CreateCommand();
                            command.CommandText = "SELECT id,streetname FROM houses WHERE owner = @owner";
                            command.Parameters.AddWithValue("@owner", data);

                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    LoadCharactersModel characterModel = new LoadCharactersModel();
                                    characterModel.ID = reader.GetInt32("id");
                                    characterModel.Name = reader.GetString("streetname");
                                    loadedCharactersList.Add(characterModel);
                                }
                                reader.Close();
                            }
                            player.TriggerEvent("Client:MDCSettings", "showhouses", NAPI.Util.ToJson(loadedCharactersList));
                            break;
                        }
                    case "selectbizz":
                        {
                            List<LoadCharactersModel> loadedCharactersList = new List<LoadCharactersModel>();

                            MySqlCommand command = General.Connection.CreateCommand();
                            command.CommandText = "SELECT id,name FROM business WHERE owner = @owner";
                            command.Parameters.AddWithValue("@owner", data);

                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    LoadCharactersModel characterModel = new LoadCharactersModel();
                                    characterModel.ID = reader.GetInt32("id");
                                    characterModel.Name = reader.GetString("name");
                                    loadedCharactersList.Add(characterModel);
                                }
                                reader.Close();
                            }
                            player.TriggerEvent("Client:MDCSettings", "showbizz", NAPI.Util.ToJson(loadedCharactersList));
                            break;
                        }
                    case "selectweapon":
                        {
                            List<LoadCharactersModel> loadedCharactersList = new List<LoadCharactersModel>();

                            MySqlCommand command = General.Connection.CreateCommand();
                            command.CommandText = "SELECT ident,shop,weaponname,timestamp FROM weapons WHERE lower(name) = @name ORDER BY timestamp DESC LIMIT 55";
                            command.Parameters.AddWithValue("@name", data.ToLower());

                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    LoadCharactersModel characterModel = new LoadCharactersModel();
                                    characterModel.Bank = reader.GetInt64("ident");
                                    characterModel.Name = reader.GetString("shop");
                                    characterModel.Job = reader.GetString("weaponname");
                                    characterModel.Closed = reader.GetInt32("timestamp");
                                    loadedCharactersList.Add(characterModel);
                                }
                                reader.Close();
                            }
                            player.TriggerEvent("Client:MDCSettings", "showweapon2", NAPI.Util.ToJson(loadedCharactersList));
                            break;
                        }
                    case "selectinvoice":
                        {
                            List<Invoices> invoiceList = new List<Invoices>();

                            MySqlCommand command = General.Connection.CreateCommand();
                            command.CommandText = "SELECT id,value,text,timestamp FROM invoices WHERE empfänger = @empfänger AND banknumber = 'SA3701-100000' ORDER BY timestamp DESC LIMIT 25";
                            command.Parameters.AddWithValue("@empfänger", data);

                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    Invoices invoices = new Invoices();
                                    invoices.id = reader.GetInt32("id");
                                    invoices.value = reader.GetInt32("value");
                                    invoices.text = reader.GetString("text");
                                    invoices.timestamp = reader.GetInt32("timestamp");
                                    invoiceList.Add(invoices);
                                }
                                reader.Close();
                            }
                            player.TriggerEvent("Client:MDCSettings", "showinvoice", NAPI.Util.ToJson(invoiceList));
                            break;
                        }
                    case "createnewfahndung":
                        {
                            mdcArray = data.Split("|");
                            Fahndungen fahndungen = new Fahndungen();
                            fahndungen.text = mdcArray[0];
                            fahndungen.creator = character.name;
                            fahndungen.timestamp = Helper.UnixTimestamp();
                            fahndungen.editor = "n/A";
                            fahndungen.status = 0;

                            fahndungList.Insert(0, fahndungen);
                            db.Insert(fahndungen);

                            SendPoliceMDCMessage(player, $"Neue Fahndung verfügbar - FHDNG-{fahndungen.id}!");

                            player.TriggerEvent("Client:MDCSettings", "showfahndungen", NAPI.Util.ToJson(fahndungList.Take(35)));
                            break;
                        }
                    case "createfahndungskommentar":
                        {
                            mdcArray = data.Split("|");
                            Fahndungen fahndung = GetFahndungById(Convert.ToInt32(mdcArray[1]));
                            if (fahndung != null)
                            {
                                CreateFahndungsKommentar(fahndung.id, mdcArray[0], character.name, character.id);
                                Helper.SendNotificationWithoutButton(player, $"Kommentar hinzugefügt!", "success", "top-left", 2500);
                                OnMDCSettings(player, "loadfahndungenkommentare", "" + fahndung.id);
                            }
                            break;
                        }
                    case "updatefahndungsstatus":
                        {
                            mdcArray = data.Split(",");
                            Fahndungen fahndung = GetFahndungById(Convert.ToInt32(mdcArray[1]));
                            if (fahndung != null)
                            {
                                fahndung.status = Convert.ToInt32(mdcArray[0]);
                                if (fahndung.status == 1)
                                {
                                    if (fahndung.editor == character.name)
                                    {
                                        Helper.SendNotificationWithoutButton(player, $"Du bearbeitest diese Fahndung bereits!", "error", "top-left", 2500);
                                        return;
                                    }
                                    fahndung.editor = character.name;
                                    CreateFahndungsKommentar(fahndung.id, $"{character.name} hat die Bearbeitung übernommen!", character.name, character.id);
                                    player.TriggerEvent("Client:MDCSettings", "updateeditor", character.name + "," + 1);
                                }
                                else if (fahndung.status == 2)
                                {
                                    CreateFahndungsKommentar(fahndung.id, $"{character.name} hat die Fahndung abgeschlossen!", character.name, character.id);
                                }
                                Helper.SendNotificationWithoutButton(player, $"Der Status wurde aktualisiert!", "success", "top-left", 2500);
                                db.Save(fahndung);
                                OnMDCSettings(player, "loadfahndungenkommentare", "" + fahndung.id);
                            }
                            else
                            {
                                Helper.SendNotificationWithoutButton(player, $"Der Status konnte nicht aktualisiert werden!", "error", "top-left", 2500);
                            }
                            break;
                        }
                    case "loadfahndungen":
                        {
                            player.TriggerEvent("Client:MDCSettings", "showfahndungen", NAPI.Util.ToJson(fahndungList.Take(35)));
                            break;
                        }
                    case "loaddispatches":
                        {
                            List<Dispatch> dispatchListTemp = new List<Dispatch>();
                            foreach(Dispatch dispatch in dispatchList)
                            {
                                if(dispatch.to == character.faction)
                                {
                                    dispatchListTemp.Add(dispatch);
                                }
                            }
                            player.TriggerEvent("Client:MDCSettings", "showdispatches", NAPI.Util.ToJson(dispatchListTemp.Take(35)));
                            break;
                        }
                    case "selectfirmen":
                        {
                            List<Groups> firmenList = new List<Groups>();
                            foreach (Groups group in GroupsController.groupList)
                            {
                                if (group.status == 1)
                                {
                                    group.leadername = Helper.GetCharacterName(group.leader);
                                    firmenList.Add(group);
                                }
                            }
                            player.TriggerEvent("Client:MDCSettings", "showfirmen", NAPI.Util.ToJson(firmenList.Take(75).OrderBy(x => x.name)));
                            break;
                        }
                    case "selectarrested":
                        {
                            List<LoadCharactersModel> loadedCharactersList = new List<LoadCharactersModel>();
                            foreach (Player p in NAPI.Pools.GetAllPlayers())
                            {
                                if (p != null && p.GetOwnSharedData<bool>("Player:Spawned") == true && p.GetSharedData<bool>("Player:Death") == false)
                                {
                                    Character character2 = Helper.GetCharacterData(p);
                                    if (character2 != null && character2.cell > 0)
                                    {
                                        LoadCharactersModel characterModel = new LoadCharactersModel();
                                        characterModel.ID = character2.arrested;
                                        characterModel.Name = character2.name;
                                        characterModel.Screen = "" + character2.cell;
                                        loadedCharactersList.Add(characterModel);
                                    }
                                }
                            }
                            player.TriggerEvent("Client:MDCSettings", "showarrested", NAPI.Util.ToJson(loadedCharactersList));
                            break;
                        }
                    case "updatedispatchstatus":
                        {
                            mdcArray = data.Split("|");
                            Dispatch dispatch = GetDispatchById(Convert.ToInt32(mdcArray[1]));
                            if (dispatch != null)
                            {
                                dispatch.status = mdcArray[0];
                                if (dispatch.status == "In Bearbeitung")
                                {
                                    if (dispatch.editor == character.name)
                                    {
                                        Helper.SendNotificationWithoutButton(player, $"Du bearbeitest diesen Dispatch bereits!", "error", "top-left", 2500);
                                        return;
                                    }
                                    dispatch.editor = character.name;
                                    CreateDispatchKommentar(dispatch.id, $"{character.name} hat die Bearbeitung übernommen!", character.name, character.id);
                                    player.TriggerEvent("Client:MDCSettings", "updateeditor", character.name + "," + 2);
                                }
                                else if (dispatch.status == "Abgeschlossen")
                                {
                                    CreateDispatchKommentar(dispatch.id, $"{character.name} hat das Dispatch abgeschlossen!", character.name, character.id);
                                }
                                Helper.SendNotificationWithoutButton(player, $"Der Status wurde aktualisiert!", "success", "top-left", 2500);
                                OnMDCSettings(player, "loaddispatchkommentare", "" + dispatch.id);
                            }
                            else
                            {
                                Helper.SendNotificationWithoutButton(player, $"Der Status konnte nicht aktualisiert werden!", "error", "top-left", 2500);
                            }
                            break;
                        }
                    case "deletedispatch":
                        {
                            if (character.rang < 5)
                            {
                                Helper.SendNotificationWithoutButton(player, $"Du benötigst mind. Rang 5 um Dispatches löschen zu können!", "error", "top-left", 2500);
                                return;
                            }
                            Dispatch dispatch = GetDispatchById(Convert.ToInt32(data));
                            if (dispatch != null)
                            {
                                foreach (FahndungsKommentare fahndungsKommentar in dispatchKommentareList.ToList())
                                {
                                    if (fahndungsKommentar.fahndungsid == dispatch.id)
                                    {
                                        dispatchKommentareList.Remove(fahndungsKommentar);
                                    }
                                }
                                foreach (Dispatch dispatch2 in dispatchList.ToList())
                                {
                                    if (dispatch2.id == dispatch.id)
                                    {
                                        dispatchList.Remove(dispatch2);
                                        break;
                                    }
                                }
                                if (character.faction == 1)
                                {
                                    SendPoliceMDCMessage(player, $"{character.name} hat das Dispatch[DSPTH-{dispatch.id}] von {dispatch.name} gelöscht!");
                                }
                                else if(character.faction == 2)
                                {
                                    SendMedicMDCMessage(player, $"{character.name} hat das Dispatch[DSPTH-{dispatch.id}] von {dispatch.name} gelöscht!");
                                }
                                else if (character.faction == 3)
                                {
                                    SendACLSMDCMessage(player, $"{character.name} hat das Dispatch[DSPTH-{dispatch.id}] von {dispatch.name} gelöscht!");
                                }
                                Helper.CreateFactionLog(character.faction, $"{character.name} hat das Dispatch[{dispatch.id}] von {dispatch.name} gelöscht!");
                                OnMDCSettings(player, "loaddispatches", "");
                            }
                            else
                            {
                                Helper.SendNotificationWithoutButton(player, $"Das Dispatch konnte nicht gelöscht werden!", "error", "top-left", 2500);
                            }
                            break;
                        }
                    case "createdispatchkommentar":
                        {
                            mdcArray = data.Split("|");
                            Dispatch dispatch = GetDispatchById(Convert.ToInt32(mdcArray[1]));
                            if (dispatch != null)
                            {
                                CreateDispatchKommentar(dispatch.id, mdcArray[0], character.name, character.id);
                                Helper.SendNotificationWithoutButton(player, $"Kommentar hinzugefügt!", "success", "top-left", 2500);
                                OnMDCSettings(player, "loaddispatchkommentare", "" + dispatch.id);
                            }
                            break;
                        }
                    case "loaddispatchkommentare":
                        {
                            List<FahndungsKommentare> kommentarList = new List<FahndungsKommentare>();
                            foreach (FahndungsKommentare fahndungKommentar in dispatchKommentareList)
                            {
                                if (Convert.ToInt32(data) == fahndungKommentar.fahndungsid)
                                {
                                    kommentarList.Add(fahndungKommentar);
                                }
                            }
                            player.TriggerEvent("Client:MDCSettings", "showkommentare", NAPI.Util.ToJson(kommentarList.OrderByDescending(x => x.timestamp)));
                            break;
                        }
                    case "loadfahndungenkommentare":
                        {
                            List<FahndungsKommentare> kommentarList = new List<FahndungsKommentare>();
                            foreach (FahndungsKommentare fahndungKommentar in db.Fetch<FahndungsKommentare>("SELECT * FROM fahndungskommentare WHERE fahndungsid = @0 ORDER BY timestamp DESC LIMIT 25", Convert.ToInt32(data)))
                            {
                                kommentarList.Add(fahndungKommentar);
                            }
                            player.TriggerEvent("Client:MDCSettings", "showkommentare", NAPI.Util.ToJson(kommentarList));
                            break;
                        }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnMDCSettings]: " + e.ToString());
            }
        }
    }
}
