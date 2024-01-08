using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using GTANetworkAPI;
using MySql.Data.MySqlClient;
using NemesusWorld.Database;
using NemesusWorld.Models;
using NemesusWorld.Utils;
using Newtonsoft.Json;

namespace NemesusWorld.Controllers
{
    class SmartphoneController : Script
    {
        public static List<Smartphone> smartphoneList = new List<Smartphone>();

        public static void ShowSmartphone(Player player, Items getitem = null, string number = "0", int hide = 0)
        {
            try
            {
                TempData tempData = Helper.GetCharacterTempData(player);
                Character character = Helper.GetCharacterData(player);
                Account account = Helper.GetAccountData(player);
                if (tempData == null || character == null || account == null) return;
                Items item = null;
                if (getitem == null)
                {
                    if (character.lastsmartphone.Length > 3)
                    {
                        foreach (Items iitem in tempData.itemlist)
                        {
                            if (iitem != null && iitem.description.Contains("Smartphone") && iitem.props == ((number == "0") ? character.lastsmartphone : number))
                            {
                                item = iitem;
                                break;
                            }
                        }
                    }
                    if (item == null && number == "0")
                    {
                        item = ItemsController.GetFirstSmartphone(player);
                    }
                }
                else
                {
                    item = getitem;
                }
                if (item != null)
                {
                    if (player.HasData("Player:Filmkamera") || player.HasData("Player:Mikrofon"))
                    {
                        Helper.SendNotificationWithoutButton(player, "Du musst zuerst die Filmkamera/das Mikrofon weglegen!", "error");
                        return;
                    }
                    Smartphone smartphone = SmartphoneController.GetSmartPhoneByNumber(item.props);
                    if (smartphone != null)
                    {
                        if ("0189" + smartphone.phonenumber == player.GetData<string>("Player:LastNumber") || player.GetData<string>("Player:LastNumber") == "0" || tempData.showSmartphone == true)
                        {
                            if (tempData.showSmartphone == false)
                            {
                                character.lastsmartphone = item.props;
                                if (hide == 0)
                                {
                                    if (tempData.showSmartphone == false)
                                    {
                                        tempData.showSmartphone = true;
                                    }
                                    else
                                    {
                                        tempData.showSmartphone = false;
                                    }
                                }
                                if (hide == 0)
                                {
                                    Helper.PlayPhoneAnim(player);
                                }
                            }
                            else
                            {
                                Helper.PlayPhoneAnim(player);
                            }
                            NAPI.Player.SetPlayerCurrentWeapon(player, WeaponHash.Unarmed);
                            List<Messages> messageList = new List<Messages>();
                            List<PhoneCalls> phoneCallList = new List<PhoneCalls>();
                            PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                            foreach (Messages messages in db.Fetch<Messages>("SELECT * FROM smartphonemessages WHERE (tomessage = @0 or frommessage = @0) LIMIT 550", "0189" + smartphone.phonenumber))
                            {
                                messageList.Add(messages);
                            }
                            foreach (PhoneCalls phoneCalls in db.Fetch<PhoneCalls>("SELECT * FROM smartphonecalls WHERE (tonumber = @0 OR fromnumber = @0) ORDER BY timestamp DESC LIMIT 35", "0189" + smartphone.phonenumber))
                            {
                                phoneCallList.Add(phoneCalls);
                            }
                            if(smartphone.phonenumber != player.GetData<string>("Player:LastNumber"))
                            {
                                tempData.speaker = false;
                            }
                            player.TriggerEvent("Client:ShowSmartphone", smartphone.phoneprops, smartphone.contacts, NAPI.Util.ToJson(messageList), NAPI.Util.ToJson(phoneCallList), smartphone.akku, hide, smartphone.prepaid, account.premium);
                        }
                        else
                        {
                            Helper.SendNotificationWithoutButton(player, "Während dem Telefonprozess, kannst du kein anderes Handy benutzen!", "error", "top-left", 3500);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[ShowSmartphone]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:HideSmartphone")]
        public static void OnHideSmartphone(Player player)
        {
            try
            {
                TempData tempData = Helper.GetCharacterTempData(player);
                if (tempData != null)
                {
                    tempData.showSmartphone = false;
                    if (tempData.inCall == false)
                    {
                        Helper.PlayPhoneAnim(player);
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnHideSmartphone]: " + e.ToString());
            }
        }

        public static void GetAllSmartPhones()
        {
            try
            {
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                foreach (Smartphone smartphone in db.Fetch<Smartphone>("SELECT * FROM smartphones"))
                {
                    smartphoneList.Add(smartphone);
                }

                MySqlCommand command = General.Connection.CreateCommand();
                command.CommandText = "DELETE FROM smartphonemessages WHERE timestamp <= UNIX_TIMESTAMP(DATE(NOW() - INTERVAL 21 DAY))";
                command.ExecuteNonQuery();

                MySqlCommand command2 = General.Connection.CreateCommand();
                command2.CommandText = "DELETE FROM smartphonecalls WHERE timestamp <= UNIX_TIMESTAMP(DATE(NOW() - INTERVAL 31 DAY))";
                command2.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetAllSmartPhones]: " + e.ToString());
            }
        }

        public static void SaveSmartphone(Smartphone smartphone)
        {
            try
            {
                if (smartphone != null)
                {
                    PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                    db.Save(smartphone);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[SaveSmartphone]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:StartSmartphoneWeather")]
        public static void OnStartSmartphoneWeather(Player player)
        {
            try
            {
                player.TriggerEvent("Client:ShowWeather", Helper.weatherObj.ToString(Newtonsoft.Json.Formatting.None));
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[StartSmartphoneWeather]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:LoadCapa")]
        public static void LoadCapa(Player player)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                TempData tempData = Helper.GetCharacterTempData(player);
                Account account = Helper.GetAccountData(player);
                if (character != null && tempData != null)
                {
                    Smartphone smartphone = SmartphoneController.GetSmartPhoneByNumber(character.lastsmartphone);
                    if (smartphone == null) return;
                    if (tempData.lasthouse > 0)
                    {
                        Helper.SendNotificationWithoutButton(player, "Akku erfolgreich aufgeladen!", "success", "top-left", 3000);
                        player.TriggerEvent("Client:SetCapa");
                        if(account.premium > 0)
                        {
                            if (account.premium == 1)
                            {
                                smartphone.akku = 53;
                            }
                            else if (account.premium == 2)
                            {
                                smartphone.akku = 58;
                            }
                            else if(account.premium == 3)
                            {
                                smartphone.akku = 63;
                            }
                        }
                        else
                        {
                            smartphone.akku = 48;
                        }
                        SmartphoneController.ShowSmartphone(player, null, character.lastsmartphone);
                        NAPI.Task.Run(() =>
                        {
                            SmartphoneController.ShowSmartphone(player, null, character.lastsmartphone);
                        }, delayTime: 215);
                        return;
                    }
                    else if (player.IsInVehicle && player.Vehicle.Class != 13 && player.Vehicle.EngineStatus == true)
                    {
                        Helper.SendNotificationWithoutButton(player, "Akku erfolgreich aufgeladen!", "success", "top-left", 3000);
                        player.TriggerEvent("Client:SetCapa");
                        if (account.premium > 0)
                        {
                            if (account.premium == 1)
                            {
                                smartphone.akku = 53;
                            }
                            else if (account.premium == 2)
                            {
                                smartphone.akku = 58;
                            }
                            else if (account.premium == 3)
                            {
                                smartphone.akku = 63;
                            }
                        }
                        else
                        {
                            smartphone.akku = 48;
                        }
                        SmartphoneController.ShowSmartphone(player, null, character.lastsmartphone);
                        NAPI.Task.Run(() =>
                        {
                            SmartphoneController.ShowSmartphone(player, null, character.lastsmartphone);
                        }, delayTime: 150);
                        return;
                    }
                    Helper.SendNotificationWithoutButton(player, "Du kannst dein Handy hier nicht aufladen!", "error", "top-left", 3000);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[LoadCapa]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:SmartphoneMessage")]
        public static void OnSmartphoneMessage(Player player, int timestamp, string to, string from, string text)
        {
            try
            {
                Messages msg = new Messages();
                msg.tomessage = to;
                msg.frommessage = from;
                msg.text = text;
                msg.timestamp = timestamp;

                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                db.Insert(msg);

                if (to.Length >= 3)
                {
                    Player p = GetPlayerFromSmartPhone(msg.tomessage);
                    if (p != null)
                    {
                        Smartphone smartphone = SmartphoneController.GetSmartPhoneByNumber(msg.tomessage);
                        Account account = Helper.GetAccountData(p);
                        Character character = Helper.GetCharacterData(p);
                        if (smartphone != null && account.prison == 0 && character.arrested == 0)
                        {
                            p.TriggerEvent("Client:UpdateSmartphone", NAPI.Util.ToJson(msg), 1);
                        }
                    }
                }
                if (player != null)
                {
                    Items item2 = ItemsController.GetFirstSmartphone(player);
                    player.TriggerEvent("Client:UpdateSmartphone", NAPI.Util.ToJson(msg), 0, 0);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnSmartphoneMessage]: " + e.ToString());
            }
        }


        [RemoteEvent("Server:ShowSmartphoneBanking")]
        public static void OnShowSmartphoneBanking(Player player)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                List<Bank> tempBankList = BankController.GetMyBankAccounts(player);
                List<Bank> distinctTempBankList = tempBankList.Distinct().ToList();
                bool bankfound = false;
                if (distinctTempBankList.Count > 0)
                {
                    foreach (Bank bank in distinctTempBankList)
                    {
                        if (character.defaultbank == bank.banknumber)
                        {
                            bankfound = true;
                            break;
                        }
                    }
                    if (bankfound == false)
                    {
                        character.defaultbank = distinctTempBankList[0].banknumber;
                    }
                }
                player.TriggerEvent("Client:ShowBanking", NAPI.Util.ToJson(distinctTempBankList), character.defaultbank);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnShowSmartphoneBanking]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:LoadTaxi")]
        public static void OnLoadTaxi(Player player)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                if (character != null)
                {
                    int taxiDriver = 0;
                    foreach (Player p in NAPI.Pools.GetAllPlayers())
                    {
                        if (p != null && p.GetOwnSharedData<bool>("Player:Spawned") == true && p.GetSharedData<bool>("Player:Death") == false && p.HasData("Player:Fare") && Helper.IsATaxiDriver(p) > -1)
                        {
                            taxiDriver++;
                        }
                    }
                    player.TriggerEvent("Client:ShowTaxi", NAPI.Util.ToJson(Helper.taxiJobs), character.name, taxiDriver, Helper.IsATaxiDriver(player));
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnLoadTaxi]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:CreateTaxi")]
        public static void OnCreateTaxi(Player player, string msg)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                if (character == null) return;

                Taxi taxi = new Taxi();
                taxi.id = character.id;
                taxi.playerid = player.Id;
                taxi.name = character.name;
                taxi.nummer = character.lastsmartphone;
                taxi.text = msg;
                taxi.done = "n/A";
                taxi.standort = $"{player.Position.X.ToString(new CultureInfo("en-US")):N3},{player.Position.Y.ToString(new CultureInfo("en-US")):N3},{player.Position.Z.ToString(new CultureInfo("en-US")):N3}";
                if (taxi != null)
                {
                    Helper.taxiJobs.Insert(0, taxi);
                }

                foreach (Player p in NAPI.Pools.GetAllPlayers())
                {
                    if (p != null && p.GetOwnSharedData<bool>("Player:Spawned") == true && p.GetSharedData<bool>("Player:Death") == false && p.HasData("Player:Fare") && Helper.IsATaxiDriver(p) > -1)
                    {
                        Helper.SendNotificationWithoutButton(p, "Neuer Taxiauftrag verfügbar!", "info", "top-left", 4750);
                        p.TriggerEvent("Client:PlaySoundPeep2");
                        if (p != player)
                        {
                            p.TriggerEvent("Client:UpdateTaxiJobs", NAPI.Util.ToJson(Helper.taxiJobs));
                        }
                    }
                }
                player.TriggerEvent("Client:UpdateTaxiJobs", NAPI.Util.ToJson(Helper.taxiJobs));
                Helper.SendNotificationWithoutButton(player, "Taxiauftrag versendet, in Kürze wird sich ein Taxifahrer bei dir melden!", "info", "top-left", 5150);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnCreateTaxi]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:DeclineTaxi")]
        public static void OnDeclineTaxi(Player player, int taxiid)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                if (character == null) return;
                Player getPlayer = null;
                foreach (Taxi taxi in Helper.taxiJobs)
                {
                    if (taxi.id == taxiid)
                    {
                        if (taxi.id == character.id && character.id != 1)
                        {
                            Helper.SendNotificationWithoutButton(player, $"Du kannst deinen eigenen Taxiauftrag nicht verwalten!", "success", "top-left", 4550);
                            return;
                        }
                        if (taxi.done != "n/A")
                        {
                            Helper.SendNotificationWithoutButton(player, "Du hast den Taxijob erfolgreich beendet!", "success", "top-left", 3150);
                        }
                        else
                        {
                            Helper.SendNotificationWithoutButton(player, "Du hast den Taxijob abgebrochen!", "success", "top-left", 3150);
                        }
                        getPlayer = Helper.GetPlayerFromID(taxi.playerid);
                        if (getPlayer != null)
                        {
                            if (taxi.done != "n/A")
                            {
                                Helper.SendNotificationWithoutButton(getPlayer, "Dein Taxiauftrag wurde erfolgreich beendet!", "success", "top-left", 3150);
                            }
                            else
                            {
                                Helper.SendNotificationWithoutButton(getPlayer, "Dein Taxiauftrag wurde abgebrochen!", "error", "top-left", 3150);
                            }
                        }
                        Helper.taxiJobs.Remove(taxi);
                        break;
                    }
                }
                player.TriggerEvent("Client:UpdateTaxiJobs", NAPI.Util.ToJson(Helper.taxiJobs));
                if (getPlayer != null)
                {
                    getPlayer.TriggerEvent("Client:UpdateTaxiJobs", NAPI.Util.ToJson(Helper.taxiJobs));
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnDeclineTaxi]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:UpdateKilometreTaxi")]
        public static void OnUpdateKilometreTaxi(Player player, float kilometre, int first = 0)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                if (character == null || !player.IsInVehicle) return;
                player.Vehicle.SetSharedData("Vehicle:Kilometre", Math.Round(kilometre, 2));
                int newPrice = (int)((Math.Round(kilometre, 2) - player.GetData<float>("Player:Taxakilometer")) * player.GetData<int>("Player:Fare"))*3;
                if (player.HasData("Player:Taxameter"))
                {
                    player.SetData<int>("Player:Taxameter", newPrice);
                }
                if (first == 0)
                {
                    if (Helper.IsATaxiDriver(player) == 2)
                    {
                        player.Vehicle.SetSharedData("Vehicle:Text3D", $"~w~Yellow Cab Co.\n~y~Dienstpreis: {player.GetData<int>("Player:Fare")}$ | ~y~Taxameter: {player.GetData<int>("Player:Taxameter")}$");
                    }
                    else
                    {
                        player.Vehicle.SetSharedData("Vehicle:Text3D", $"~w~{GroupsController.GetGroupNameById(character.mygroup)}\n~y~Dienstpreis: {player.GetData<int>("Player:Fare")}$ | ~y~Taxameter: {player.GetData<int>("Player:Taxameter")}$");
                    }
                }
                if (first == 1)
                {
                    player.SetData<float>("Player:Taxakilometer", player.Vehicle.GetSharedData<float>("Vehicle:Kilometre"));
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnUpdateKilometreTaxi]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:AcceptTaxi")]
        public static void OnAcceptTaxi(Player player, int taxiid)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                if (character == null) return;
                Player getPlayer = null;
                foreach (Taxi taxi in Helper.taxiJobs)
                {
                    if (taxi.id == taxiid)
                    {
                        if(taxi.id == character.id && character.id != 1)
                        {
                            Helper.SendNotificationWithoutButton(player, $"Du kannst deinen eigenen Taxiauftrag nicht verwalten!", "success", "top-left", 4550);
                            return;
                        }
                        Helper.SendNotificationWithoutButton(player, $"Du hast den Taxiauftrag angenommen, bitte fahre jetzt zu deinem Kunden. Rückrufnummer: {taxi.nummer}!", "success", "top-left", 4550);
                        getPlayer = Helper.GetPlayerFromID(taxi.playerid);
                        if (getPlayer != null)
                        {
                            Helper.SendNotificationWithoutButton(getPlayer, $"Dein Taxiauftrag wurde angenommen, ein Taxifahrer wird in kürze bei dir sein. Rückrufnummer: {character.lastsmartphone}!", "success", "top-left", 4550);
                            getPlayer.TriggerEvent("Client:PlaySoundPeep2");
                        }
                        player.TriggerEvent("Client:CreateWaypoint", taxi.standort.Split(",")[0], taxi.standort.Split(",")[1]);
                        taxi.done = character.name;
                        break;
                    }
                }
                player.TriggerEvent("Client:UpdateTaxiJobs", NAPI.Util.ToJson(Helper.taxiJobs));
                if (getPlayer != null)
                {
                    getPlayer.TriggerEvent("Client:UpdateTaxiJobs", NAPI.Util.ToJson(Helper.taxiJobs));
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnAcceptTaxi]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:AcceptInvoice")]
        public static void OnAcceptInvoice(Player player, int id, bool hide = false)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                if (character == null) return;
                foreach (Invoices invoice in Helper.invoicesList.ToList())
                {
                    if (invoice.id == id)
                    {
                        Groups group = GroupsController.GetGroupByName(invoice.empfänger);
                        if (group != null)
                        {
                            Helper.CreateGroupLog(group.id, $"{character.name} hat die Rechnung[{invoice.id}] (intern) als bezahlt markiert!");
                        }
                        if (invoice.modus == "Firmen/Gruppierungsrechnung")
                        {
                            group = GroupsController.GetGroupByBankNumber(invoice.banknumber);
                            if(group != null)
                            {
                                Helper.CreateGroupLog(group.id, $"{character.name} hat die Rechnung[{invoice.id}] (extern) als bezahlt markiert!");
                            }
                        }
                        Helper.invoicesList.Remove(invoice);
                        BankController.OnLoadInvoices(player);
                        PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                        db.Delete(invoice);
                        if (hide == false)
                        {
                            Helper.SendNotificationWithoutButton(player, "Rechnung als bezahlt markiert!", "info");
                        }
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnAcceptInvoice]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:PayInvoice")]
        public static void OnPayInvoice(Player player, int id)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                if (character == null) return;
                foreach (Invoices invoice in Helper.invoicesList.ToList())
                {
                    if (invoice.id == id)
                    {
                        Bank bank1 = null;
                        Groups group = GroupsController.GetGroupById(character.mygroup);
                        if (invoice.modus == "Privatrechnung" && character.name == invoice.empfänger)
                        {
                            bank1 = BankController.GetBankByBankNumber(character.defaultbank);
                        }
                        if (group != null && invoice.modus == "Firmen/Gruppierungsrechnung" && group.name == invoice.empfänger)
                        {
                            bank1 = BankController.GetBankByBankNumber(group.banknumber);
                        }
                        if (group != null && invoice.modus == "Staatsrechnung")
                        {
                            bank1 = BankController.GetBankByBankNumber("SA3701-100000");
                        }
                        Bank bank2 = BankController.GetBankByBankNumber(invoice.banknumber);
                        if (bank1 != null && bank2 != null)
                        {
                            if (bank1.bankvalue >= invoice.value)
                            {
                                bank1.bankvalue -= invoice.value;
                                bank2.bankvalue += invoice.value;
                                Helper.BankSettings(character.defaultbank, "Rechnung bezahlt", ""+invoice.value, "nPayments");

                                Helper.Bankfile(bank1, bank2, $"Rechnung[{invoice.id}] bezahlt", invoice.value);
                                Helper.Bankfile(bank2, bank1, $"Rechnung[{invoice.id}] bezahlt", invoice.value, true);

                                string text = $"Überweisung von {character.defaultbank} nach {invoice.banknumber}, Summe: {invoice.value}, Verwendungszweck: Rechnung[{invoice.id}] bezahlt";
                                Helper.CreateAdminLog("banklog", text);

                                BankController.SaveBank(bank1);
                                BankController.SaveBank(bank2);

                                OnAcceptInvoice(player, invoice.id, true);
                                Helper.SendNotificationWithoutButton(player, "Rechnung wurde via Überweisung bezahlt und als erledigt markiert!", "info", "top-left", 4500);
                                return;
                            }
                            else
                            {
                                Helper.SendNotificationWithoutButton(player, "Es ist nicht genügend Geld auf dem Konto vorhanden!", "info", "top-left", 4500);
                                return;
                            }
                        }
                        Helper.SendNotificationWithoutButton(player, "Die Rechnung konnte nicht automatisch bezahlt werden!", "info", "top-left", 4500);
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnPayInvoice]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:SmartphoneCall")]
        public static void OnSmartphoneCall(Player player, string number1, string number2, int hidden)
        {
            try
            {
                int emergency = 0;
                Character character = Helper.GetCharacterData(player);
                TempData tempData = Helper.GetCharacterTempData(player);
                if (character != null && tempData != null && tempData.inCall == false)
                {
                    if (number2 == "911" || number2 == "0189911")
                    {
                        FactionsModel factionsModel = Helper.GetFactionById(1);
                        if (factionsModel != null && factionsModel.number.Length > 3)
                        {
                            number2 = factionsModel.number;
                            emergency = 1;
                        }
                    }
                    else if (number2 == "912" || number2 == "0189912")
                    {
                        FactionsModel factionsModel = Helper.GetFactionById(2);
                        if (factionsModel != null && factionsModel.number.Length > 3)
                        {
                            number2 = factionsModel.number;
                            emergency = 1;
                        }
                    }
                    else if (number2 == "913" || number2 == "0189913")
                    {
                        FactionsModel factionsModel = Helper.GetFactionById(3);
                        if (factionsModel != null && factionsModel.number.Length > 3)
                        {
                            number2 = factionsModel.number;
                            emergency = 1;
                        }
                    }
                    tempData.inCall = true;
                    tempData.inCall2 = true;
                    Helper.PlayPhoneAnim(player);
                    player.SetData<string>("Player:InCall", number2);
                    player.SetData<string>("Player:LastNumber", number1);
                    Smartphone smartphone = SmartphoneController.GetSmartPhoneByNumber(number2);
                    Smartphone smartphone1 = SmartphoneController.GetSmartPhoneByNumber(number1);
                    PhoneCalls phoneCall = new PhoneCalls();
                    phoneCall.fromnumber = number1;
                    phoneCall.tonumber = number2;
                    phoneCall.timestamp = Helper.UnixTimestamp();
                    PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                    db.Insert(phoneCall);
                    if (smartphone != null)
                    {
                        foreach (Player p in NAPI.Pools.GetAllPlayers())
                        {
                            if (p != null && p.GetOwnSharedData<bool>("Player:Spawned") == true)
                            {
                                Character character2 = Helper.GetCharacterData(p);
                                Account account2 = Helper.GetAccountData(p);
                                if (character2 != null && account2 != null)
                                {
                                    TempData tempData2 = Helper.GetCharacterTempData(p);
                                    if (tempData2 != null)
                                    {
                                        foreach (Items item in tempData2.itemlist)
                                        {
                                            if (item != null && item.description.Contains("Smartphone") && item.props == number2)
                                            {
                                                if (player != p)
                                                {
                                                    if (tempData2.inCall == true || account2.prison > 0 || character2.arrested > 0)
                                                    {
                                                        player.SetData<bool>("Client:Besetzt", true);
                                                        player.TriggerEvent("Client:PlaySound", "besetzt.mp3", 0);
                                                        return;
                                                    }
                                                    player.SetData<bool>("Client:Besetzt", false);
                                                    tempData2.inCall = true;
                                                    p.SetData<string>("Player:InCall", number1);
                                                    p.SetData<string>("Player:LastNumber", number2);
                                                    CallNumber(p, number1, number2, hidden, smartphone.phoneprops, emergency);
                                                }
                                                else
                                                {
                                                    player.SetData<bool>("Client:Besetzt", true);
                                                }
                                                return;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnSmartphoneCall]: " + e.ToString());
            }
        }

        public static void CallNumber(Player player, string number1, string number2, int hidden, string phoneprops, int emergency = 0)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                TempData tempData = Helper.GetCharacterTempData(player);
                if (character != null && tempData != null)
                {
                    Smartphone smartphone = SmartphoneController.GetSmartPhoneByNumber(number2);
                    if (smartphone != null)
                    {
                        if (character.lastsmartphone != number2)
                        {
                            if (tempData.showSmartphone == true)
                            {
                                SmartphoneController.ShowSmartphone(player, null, number2);
                                NAPI.Task.Run(() =>
                                {
                                    SmartphoneController.ShowSmartphone(player, null, number2);
                                }, delayTime: 150);
                            }
                            else
                            {
                                SmartphoneController.ShowSmartphone(player, null, number2, 1);
                            }
                        }
                        NAPI.Task.Run(() =>
                        {
                            player.TriggerEvent("Client:SmartphoneGetCall", number1, number2, hidden, phoneprops, emergency);
                        }, delayTime: 550);
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CallNumber]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:AcceptCall")]
        public static void OnAcceptCall(Player player)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                TempData tempData = Helper.GetCharacterTempData(player);
                if (character != null && tempData != null)
                {
                    if (tempData.inCall == true)
                    {
                        Player target = GetPlayerFromSmartPhone(player.GetData<string>("Player:InCall"));
                        if (target != null)
                        {
                            TempData tempData2 = Helper.GetCharacterTempData(target);
                            Character character2 = Helper.GetCharacterData(target);
                            tempData.inCall2 = true;
                            tempData2.inCall2 = true;
                            if (Helper.adminSettings.voicerp == 1)
                            {
                                player.TriggerEvent("SaltyChat_EstablishedCall", target.Id);
                                target.TriggerEvent("SaltyChat_EstablishedCall", player.Id);
                            }
                            else if (Helper.adminSettings.voicerp == 2)
                            {
                                Helper.OnAdd_Voice_Listener(player, target);
                                Helper.OnAdd_Voice_Listener(target, player);
                            }
                            Helper.PlayPhoneAnim(player);
                            if(Helper.adminSettings.voicerp == 0)
                            {
                                player.SetData<bool>("Player:AcceptCall", true);
                                target.SetData<bool>("Player:AcceptCall", true);
                            }
                            Helper.SendNotificationWithoutButton(player, "Du hast das Telefonat angenommen!");
                            Helper.SendNotificationWithoutButton(target, "Dein Gesprächspartner hat das Telefonat angenommen!");
                            SmartphoneController.ShowSmartphone(player, null, character.lastsmartphone);
                            SmartphoneController.ShowSmartphone(target, null, character2.lastsmartphone);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnAcceptCall]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:DeclineCall")]
        public static void OnDeclineCall(Player player)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                TempData tempData = Helper.GetCharacterTempData(player);
                if (character != null && tempData != null)
                {
                    if (tempData.inCall == true)
                    {
                        Player target = GetPlayerFromSmartPhone(player.GetData<string>("Player:InCall"));
                        tempData.inCall = false;
                        tempData.inCall2 = false;
                        player.SetData<string>("Player:InCall", "0");
                        player.SetData<string>("Player:LastNumber", "0");
                        Helper.PlayPhoneAnim(player);
                        if (target != null)
                        {
                            target.TriggerEvent("Client:EndCall");
                            target.SetData<string>("Player:InCall", "0");
                            target.SetData<string>("Player:LastNumber", "0");
                            TempData tempData2 = Helper.GetCharacterTempData(target);
                            if (tempData2 != null)
                            {
                                tempData2.inCall = false;
                                tempData2.inCall2 = false;
                                Helper.PlayPhoneAnim(target);
                            }
                            if (Helper.adminSettings.voicerp == 0)
                            {
                                player.SetData<bool>("Player:AcceptCall", false);
                                target.SetData<bool>("Player:AcceptCall", false);
                                Helper.SendNotificationWithoutButton(target, "~w~Dein Gesprächspartner hat das Telefonat abgelehnt!", "error");
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnDeclineCall]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:MuteMicro")]
        public static void OnMuteMicro(Player player, bool state)
        {
            try
            {
                Player target = GetPlayerFromSmartPhone(player.GetData<string>("Player:InCall"));
                if (target == null) return;
                if (Helper.adminSettings.voicerp == 1)
                {
                    if (state == true)
                    {
                        target.TriggerEvent("SaltyChat_EndCall", player.Id);
                    }
                    else
                    {
                        target.TriggerEvent("SaltyChat_EstablishedCall", player.Id);
                    }
                }
                if (Helper.adminSettings.voicerp == 2)
                {
                    if (state == true)
                    {
                        Helper.OnRemove_Voice_Listener(target, player);
                    }
                    else
                    {
                        Helper.OnAdd_Voice_Listener(target, player);
                    }
                }
                else if (Helper.adminSettings.voicerp == 0)
                {
                    Commands.cmd_speaker(player);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnMuteMicro]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:CloseHandy")]
        public static void OnCloseHandy(Player player)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                if (character == null) return;
                Smartphone smartphone = SmartphoneController.GetSmartPhoneByNumber(character.lastsmartphone);
                if (smartphone == null) return;
                Helper.SendNotificationWithoutButton(player, "Vertrag bei nMobile gekündigt, du läufst jetzt wieder auf Prepaid!", "success", "top-left", 3500);
                smartphone.prepaid = 0;
                smartphone.owner = "n/A";
                Helper.OnPlayerPressF5(player, 0);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnCloseHandy]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:SendDispatch")]
        public static void OnSendDispatch(Player player, string text, string name, int to, string phonenumber, bool showtext = true)
        {
            try
            {
                Dispatch dispatch = new Dispatch();
                MDCController.dispatchCount++;
                dispatch.id = MDCController.dispatchCount;
                dispatch.text = text;
                dispatch.name = name;
                dispatch.to = to;
                dispatch.phonenumber = phonenumber;
                dispatch.position = $"{player.Position.X.ToString(new CultureInfo("en-US")):N3},{player.Position.Y.ToString(new CultureInfo("en-US")):N3},{player.Position.Z.ToString(new CultureInfo("en-US")):N3}";
                dispatch.timestamp = Helper.UnixTimestamp();
                MDCController.dispatchList.Add(dispatch);

                if (showtext == true)
                {
                    Helper.SendNotificationWithoutButton(player, "Dispatch erstellt und versendet!", "info", "top-left", 2500);
                }

                if (dispatch.to == 1)
                {
                    MDCController.SendPoliceMDCMessage(player, $"Neuer Dispatch verfügbar - DSPTH-{dispatch.id}!");
                }
                else if (dispatch.to == 2)
                {
                    MDCController.SendMedicMDCMessage(player, $"Neuer Dispatch verfügbar - DSPTH-{dispatch.id}!");
                }
                else if (dispatch.to == 3)
                {
                    MDCController.SendACLSMDCMessage(player, $"Neuer Dispatch verfügbar - DSPTH-{dispatch.id}!");
                }
            }
            catch (Exception e)
            {
                Helper.SendNotificationWithoutButton(player, "Dispatch konnte nicht erstellt werden!", "error", "top-left", 2500);
                Helper.ConsoleLog("error", $"[OnSendDispatch]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:SetStatus")]
        public static void OnSetStatus(Player player, string status)
        {
            try
            {
                TempData tempData = Helper.GetCharacterTempData(player);
                if(tempData != null)
                {
                    if (status.Length > 0)
                    {
                        tempData.status = status;
                    }
                    else
                    {
                        tempData.status = "n/A";
                    }
                    Helper.SendNotificationWithoutButton(player, "Der Status wurde gesetzt!", "info", "top-left", 2500);
                }
            }
            catch (Exception e)
            {
                Helper.SendNotificationWithoutButton(player, "Der Status konnte nicht gesetzt werden!", "error", "top-left", 2500);
                Helper.ConsoleLog("error", $"[OnSendDispatch]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:EndCall")]
        public static void OnEndCall(Player player)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                TempData tempData = Helper.GetCharacterTempData(player);
                player.TriggerEvent("Client:StopSound");
                if (player.GetData<bool>("Client:Besetzt") == true)
                {
                    player.SetData<bool>("Client:Besetzt", false);
                    player.SetData<string>("Player:InCall", "0");
                    player.SetData<string>("Player:LastNumber", "0");
                    tempData.inCall = false;
                    tempData.inCall2 = false;
                    Helper.PlayPhoneAnim(player);
                    return;
                }
                if (character != null && tempData != null)
                {
                    if (tempData.inCall == true)
                    {
                        Player target = GetPlayerFromSmartPhone(player.GetData<string>("Player:InCall"));
                        if (target != null)
                        {
                            if (Helper.adminSettings.voicerp == 1)
                            {
                                player.TriggerEvent("SaltyChat_EndCall", target.Id);
                            }
                            else if (Helper.adminSettings.voicerp == 2)
                            {
                                Helper.OnRemove_Voice_Listener(player, target);
                            }
                            target.TriggerEvent("Client:EndCall");
                            target.SetData<string>("Player:LastNumber", "0");
                            TempData tempData2 = Helper.GetCharacterTempData(target);
                            if (tempData2 != null)
                            {
                                tempData2.inCall = false;
                                tempData2.inCall2 = false;
                                Helper.PlayPhoneAnim(target);
                                if (Helper.adminSettings.voicerp == 1)
                                {
                                    target.TriggerEvent("SaltyChat_EndCall", player.Id);
                                }
                                else if (Helper.adminSettings.voicerp == 2)
                                {
                                    Helper.OnRemove_Voice_Listener(target, player);
                                }
                            }
                            target.SetData<string>("Player:InCall", "0");
                        }
                        player.SetData<string>("Player:InCall", "0");
                        player.SetData<string>("Player:LastNumber", "0");
                        tempData.inCall = false;
                        tempData.inCall2 = false;
                        Helper.PlayPhoneAnim(player);
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnEndCall]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:ShowSmartphoneBankFiles")]
        public static void OnShowSmartphoneBankFiles(Player player, string banknumber)
        {
            try
            {
                Bank bank = BankController.GetBankByBankNumber(banknumber);
                if (bank != null)
                {
                    player.TriggerEvent("Client:GetSmartphoneBankFiles", NAPI.Util.ToJson(BankController.GetBankSetting(banknumber)));
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnShowSmartphoneBankFiles]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:ShowSmartphoneBankSettings")]
        public static void OnShowSmartphoneBankSettings(Player player, string banknumber)
        {
            try
            {
                Bank bank = BankController.GetBankByBankNumber(banknumber);
                if (bank != null)
                {
                    player.TriggerEvent("Client:GetSmartphoneBankSettings", NAPI.Util.ToJson(BankController.GetBankFile(bank.id)));
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnShowSmartphoneBankSettings]: " + e.ToString());
            }
        }

        public static Player GetPlayerFromSmartPhone(string number)
        {
            try
            {
                foreach (Player player in NAPI.Pools.GetAllPlayers())
                {
                    if (player != null && player.GetOwnSharedData<bool>("Player:Spawned") == true)
                    {
                        Character character = Helper.GetCharacterData(player);
                        if (character != null)
                        {
                            TempData tempData = Helper.GetCharacterTempData(player);
                            if (tempData != null)
                            {
                                foreach (Items item in tempData.itemlist)
                                {
                                    if (item != null && item.description.ToLower() == "smartphone" && item.props == number)
                                    {
                                        return player;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetPlayerFromSmartPhone]: " + e.ToString());
            }
            return null;
        }

        public static Smartphone GetSmartPhoneByNumber(string number)
        {
            try
            {
                foreach (Smartphone smartphone in smartphoneList)
                {
                    if (smartphone != null && ("0189" + smartphone.phonenumber) == number)
                    {
                        return smartphone;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetSmartPhoneByNumber]: " + e.ToString());
            }
            return null;
        }

        [RemoteEvent("Server:SaveSmartphone")]
        public static void OnSaveSmartphone(Player player, string json, string json2, string phonenumber)
        {
            try
            {
                Smartphone smartphone = GetSmartPhoneByNumber(phonenumber);
                if (smartphone != null)
                {
                    smartphone.phoneprops = json;
                    smartphone.contacts = json2;
                    SaveSmartphone(smartphone);

                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnSaveSmartphone]: " + e.ToString());
            }
        }
    }
}
