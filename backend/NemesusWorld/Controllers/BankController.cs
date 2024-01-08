using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using GTANetworkAPI;
using MySql.Data.MySqlClient;
using NemesusWorld.Database;
using NemesusWorld.Models;
using NemesusWorld.Utils;
using Newtonsoft.Json.Linq;

namespace NemesusWorld.Controllers
{
    class BankController : Script
    {
        public static List<Bank> bankList = new List<Bank>();

        public static void GetAllBanks()
        {
            try
            {
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                foreach (Bank bank in db.Fetch<Bank>("SELECT * FROM bank"))
                {
                    bankList.Add(bank);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetAllBanks]: " + e.ToString());
            }
        }

        public static void GetAllInvoices()
        {
            try
            {
                MySqlCommand command = General.Connection.CreateCommand();
                command.CommandText = "DELETE FROM invoices WHERE banknumber != 'SA3701-100000' AND timestamp <= UNIX_TIMESTAMP(DATE(NOW() - INTERVAL 31 DAY))";
                command.ExecuteNonQuery();

                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                foreach (Invoices invoice in db.Fetch<Invoices>("SELECT * FROM invoices"))
                {
                    Helper.invoicesList.Add(invoice);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetAllInvoices]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:LoadInvoices")]
        public static void OnLoadInvoices(Player player, bool groupcheck = true)
        {
            try
            {
                List<Invoices> tempInvoicesList = new List<Invoices>();
                Character character = Helper.GetCharacterData(player);
                Groups group = null;
                GroupsMembers groupsMembers = null;
                if (groupcheck == true)
                {
                    group = GroupsController.GetGroupById(character.mygroup);
                }
                if(group != null)
                {
                    groupsMembers = GroupsController.GetGroupMemberById(character.id, group.id);
                }
                foreach (Invoices invoice in Helper.invoicesList)
                {
                    if (invoice != null)
                    {
                        if (invoice.empfänger == character.name)
                        {
                            tempInvoicesList.Add(invoice);
                        }
                        else if(group != null && groupsMembers != null && group.name.ToLower() == invoice.empfänger.ToLower() && groupsMembers.rang >= 10)
                        {
                            tempInvoicesList.Add(invoice);
                        }
                        else if(character.faction == 4 && character.factionduty == true && character.rang >= 10 && invoice.empfänger.ToLower() == "staat")
                        {
                            tempInvoicesList.Add(invoice);
                        }
                    }
                }
                player.TriggerEvent("Client:ShowInvoices", NAPI.Util.ToJson(tempInvoicesList.OrderByDescending(x => x.timestamp).Take(25)));
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnLoadInvoices]: " + e.ToString());
            }
        }

        public static Bank GetBankByBankNumber(string banknumber)
        {
            try
            {
                foreach (Bank bank in bankList)
                {
                    if(bank != null && bank.banknumber.ToLower() == banknumber.ToLower())
                    {
                        return bank;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetBankByBankNumber]: " + e.ToString());
            }
            return null;
        }

        public static void SaveBank(Bank bank)
        {
            try
            {
                if (bank == null) return;
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                db.Save(bank);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[SaveBank]: " + e.ToString());
            }
        }

        public static Bank GetDefaultBank(Player player, string defaultbank)
        {
            TempData tempData = Helper.GetCharacterTempData(player);
            Character character = Helper.GetCharacterData(player);
            if(defaultbank.Length <= 3 || tempData == null || character == null) return null;
            try
            {
                foreach(Bank bank in bankList)
                {
                    if(bank != null && bank.banknumber == defaultbank)
                    {
                        foreach (Items item in tempData.itemlist)
                        {
                            if (item != null && item.description.Contains("EC-Karte") && item.props.Length > 3 && item.props == defaultbank)
                            {
                                return bank;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetDefaultBank]: " + e.ToString());
            }
            character.defaultbank = "n/A";
            return null;
        }

        public static bool HasBankRights(Player player, string banknumber)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                TempData tempData = Helper.GetCharacterTempData(player);
                foreach (Items item in tempData.itemlist)
                {
                    if (item != null && item.description.Contains("EC-Karte") && item.props.Length > 3)
                    {
                        foreach (Bank bank in bankList)
                        {
                            if (bank != null)
                            {
                                if (banknumber.ToLower() == bank.banknumber.ToLower() && bank.banknumber.ToLower() == item.props.ToLower())
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[HasBankRights]: " + e.ToString());
            }
            return false;
        }

        public static void UpdateBankAccounts(Player player)
        {
            try
            {
                List<Bank> tempBankList = GetMyBankAccounts(player);
                List<Bank> distinctTempBankList = tempBankList.Distinct().ToList();
                player.TriggerEvent("Client:UpdateBankMenu", NAPI.Util.ToJson(distinctTempBankList));
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[UpdateBankAccounts]: " + e.ToString());
            }
        }

        public static void RunStandingOrder()
        {
            try
            {
                MySqlCommand command = General.Connection.CreateCommand();
                command = General.Connection.CreateCommand();
                command.CommandText = "UPDATE standingorder SET daysrun=daysrun+1";
                command.ExecuteNonQuery();

                Bank bank1 = null;
                Bank bank2 = null;

                List<Standingorder> standingOrderList = new List<Standingorder>();

                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                foreach (Standingorder standingorder in db.Fetch<Standingorder>("SELECT * FROM standingorder WHERE daysrun>=days"))
                {
                    standingOrderList.Add(standingorder);
                }
                foreach (Standingorder standingorder1 in standingOrderList)
                {
                    if (standingorder1 != null)
                    {
                        foreach (Bank bank in bankList)
                        {
                            if (bank != null && bank.banknumber == standingorder1.bankto)
                            {
                                bank2 = bank;
                            }
                            if (bank != null && bank.banknumber == standingorder1.bankfrom)
                            {
                                bank1 = bank;
                            }
                        }
                        if (bank1 != null && bank2 != null)
                        {
                            if (bank1.bankvalue >= standingorder1.bankvalue)
                            {
                                bank1.bankvalue -= standingorder1.bankvalue;
                                bank2.bankvalue += standingorder1.bankvalue;
                                Helper.BankSettings(standingorder1.bankfrom, "Überweisung getätigt", standingorder1.bankvalue.ToString(), "Dauerauftrag");

                                Helper.Bankfile(bank1, bank2, standingorder1.banktext, standingorder1.bankvalue);
                                Helper.Bankfile(bank1, bank2, standingorder1.banktext, standingorder1.bankvalue, true);

                                string text = $"Überweisung von {standingorder1.bankfrom} nach {standingorder1.bankto}, Summe: {standingorder1.bankvalue}, Verwendungszweck: {standingorder1.banktext}";
                                Helper.CreateAdminLog("banklog", text);

                                db.Delete(standingorder1);

                                BankController.SaveBank(bank1);
                                BankController.SaveBank(bank2);
                            }
                        }
                    }
                }
                standingOrderList = null;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[RunStandingOrder]: " + e.ToString());
            }
        }

        public static void RunTransfer()
        {
            try
            {
                Bank bank1 = null;
                Bank bank2 = null;

                List<Transfer> transferList = new List<Transfer>();

                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                foreach (Transfer transfer in db.Fetch<Transfer>("SELECT * FROM transfer"))
                {
                    transferList.Add(transfer);
                }
                foreach(Transfer transfer in transferList)
                {
                    if(transfer != null)
                    {
                        foreach (Bank bank in bankList)
                        {
                            if (bank != null && bank.banknumber == transfer.bankto)
                            {
                                bank2 = bank;
                            }
                            if (bank != null && bank.banknumber == transfer.bankfrom)
                            {
                                bank1 = bank;
                            }
                        }
                        if (bank1 != null && bank2 != null)
                        {
                            if (bank1.bankvalue >= transfer.bankvalue)
                            {
                                bank1.bankvalue -= transfer.bankvalue;
                                bank2.bankvalue += transfer.bankvalue;
                                Helper.BankSettings(transfer.bankfrom, "Überweisung getätigt", transfer.bankvalue.ToString(), transfer.bankname);

                                Helper.Bankfile(bank1, bank2, transfer.banktext, transfer.bankvalue);
                                Helper.Bankfile(bank1, bank2, transfer.banktext, transfer.bankvalue, true);

                                string text = $"Überweisung von {transfer.bankfrom} nach {transfer.bankto}, Summe: {transfer.bankvalue}, Verwendungszweck: {transfer.banktext}";
                                Helper.CreateAdminLog("banklog", text);

                                db.Delete(transfer);

                                BankController.SaveBank(bank1);
                                BankController.SaveBank(bank2);
                            }
                        }
                    }
                }
                transferList = null;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[RunTransfer]: " + e.ToString());
            }
        }

        public static List<Bank> GetMyBankAccounts(Player player)
        {
            List<Bank> tempBankList = new List<Bank>();
            try
            {
                Character character = Helper.GetCharacterData(player);
                TempData tempData = Helper.GetCharacterTempData(player);
                foreach (Items item in tempData.itemlist)
                {
                    if(item != null && item.description.Contains("EC-Karte") && item.props.Length > 3)
                    {
                        foreach(Bank bank in bankList)
                        {
                            if(bank != null)
                            {
                                if(bank.banknumber.ToLower() == item.props.ToLower())
                                {
                                    tempBankList.Add(bank);
                                }
                            }
                        }
                    }
                }
                Bank bank2 = BankController.GetBankByBankNumber("SA3701-100000");
                if (character.faction == 4 && character.rang >= 10 && bank2 != null)
                {
                    tempBankList.Add(bank2);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetMyBankAccounts]: " + e.ToString());
            }
            return tempBankList;
        }

        public static void ShowBankMenu(Player player, int atbank)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                List<Bank> tempBankList = GetMyBankAccounts(player);
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
                if (atbank == 2 && distinctTempBankList.Count <= 0)
                {
                    Helper.SendNotificationWithoutButton(player, "Du hast keine EC-Karte dabei!", "error");
                    return;
                }
                player.TriggerEvent("Client:ShowBankMenu", NAPI.Util.ToJson(distinctTempBankList), atbank, character.defaultbank, character.id);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[ShowBankMenu]: " + e.ToString());
            }
        }

        public static List<BankFile> GetBankFile(int bankid)
        {
            try
            {
                if (bankid <= 0) return null;
                List<BankFile> bankFiles = new List<BankFile>();

                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                foreach (BankFile bankFile in db.Fetch<BankFile>("SELECT * FROM bankfile WHERE bankid = @0 ORDER BY id DESC LIMIT 25", bankid))
                {
                    bankFiles.Add(bankFile);
                }
                return bankFiles;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetBankFile]: " + e.ToString());
            }
            return null;
        }

        public static List<BankSetting> GetBankSetting(string banknumber)
        {
            try
            {
                if (banknumber.Length < 13) return null;
                List<BankSetting> bankSetting = new List<BankSetting>();

                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                foreach (BankSetting bankFile in db.Fetch<BankSetting>("SELECT * FROM banksettings WHERE banknumber = @0 ORDER BY id DESC LIMIT 25", banknumber))
                {
                    bankSetting.Add(bankFile);
                }
                return bankSetting;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetBankSetting]: " + e.ToString());
            }
            return null;
        }

        public static List<Standingorder> GetStandingOrder(string banknumber)
        {
            try
            {
                if (banknumber.Length < 13) return null;
                List<Standingorder> standingOrder = new List<Standingorder>();

                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                foreach (Standingorder standingOrderTemp in db.Fetch<Standingorder>("SELECT * FROM standingorder WHERE bankfrom = @0 ORDER BY timestamp DESC LIMIT 25", banknumber))
                {
                    standingOrder.Add(standingOrderTemp);
                }
                return standingOrder;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetStandingOrder]: " + e.ToString());
            }
            return null;
        }

        [RemoteEvent("Server:BankSettings")]
        public static void BankSettings(Player player, string setting, string json)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                TempData tempData = Helper.GetCharacterTempData(player);
                Account account = Helper.GetAccountData(player);
                JObject obj = JObject.Parse(json);
                int value = 0;
                int gebuehr = 0;
                string banknumber = "n/A";
                Bank bank = null;
                if (obj == null)
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültige Interaktion!", "error");
                    return;
                }
                switch (setting.ToLower())
                {
                    case "new":
                        {
                            List<Bank> tempBankList = GetMyBankAccounts(player);
                            if(tempBankList.Count >= 5)
                            {
                                Helper.SendNotificationWithoutButton(player, "Du kannst nur max. 5 Konten besitzen!", "error");
                                return;
                            }
                            Bank newBank = new Bank();
                            newBank.banknumber = "SA3701-"+Helper.GeneratePin(6);
                            foreach(Bank tempBank in bankList)
                            {
                                if(bank != null && bank.banknumber == newBank.banknumber)
                                {
                                    Helper.SendNotificationWithoutButton(player, "Fehler, bitte nochmal probieren!", "error");
                                    newBank = null;
                                    return;
                                }
                            }
                            newBank.bankvalue = 0;
                            newBank.banktype = (int)obj["bankat"];
                            newBank.pincode = Helper.GeneratePin(4);
                            newBank.ownercharid = character.id;
                            newBank.groupid = 0;
                            if (tempBankList.Count <= 0)
                            {
                                character.defaultbank = newBank.banknumber;
                            }

                            Items newitem = ItemsController.CreateNewItem(player, character.id, "EC-Karte", "Player", 1, ItemsController.GetFreeItemID(player));
                            if (!ItemsController.CanPlayerHoldItem(player, newitem.weight))
                            {
                                newitem = null;
                                newBank = null;
                                Helper.SendNotificationWithoutButton(player, "Du hast keinen Platz mehr im Inventar für die EC-Karte!", "error", "top-end");
                                return;
                            }
                            if (newitem != null)
                            {
                                newitem.props = "" + newBank.banknumber;
                                tempData.itemlist.Add(newitem);

                                MySqlCommand command = General.Connection.CreateCommand();

                                command.CommandText = "INSERT INTO bank (banknumber, bankvalue, pincode, ownercharid, groupid, banktype) VALUES (@banknumber, @bankvalue, @pincode, @ownercharid, @groupid, @banktype)";
                                command.Parameters.AddWithValue("@banknumber", newBank.banknumber);
                                command.Parameters.AddWithValue("@bankvalue", newBank.bankvalue);
                                command.Parameters.AddWithValue("@pincode", newBank.pincode);
                                command.Parameters.AddWithValue("@ownercharid", newBank.ownercharid);
                                command.Parameters.AddWithValue("@groupid", newBank.groupid);
                                command.Parameters.AddWithValue("@banktype", newBank.banktype);

                                command.ExecuteNonQuery();
                                newBank.id = (int)command.LastInsertedId;
                                bankList.Add(newBank);

                                if (account.faqarray[2] == "0")
                                {
                                    account.faqarray[2] = "1";
                                }

                                Helper.SendNotificationWithoutButton(player, $"Neues Konto {newBank.banknumber} mit dem Pin {newBank.pincode} wurde erstellt!", "success", "top-left", 11500);
                            }
                            else
                            {
                                Helper.SendNotificationWithoutButton(player, "Es konnte kein neues Konto erstellt werden!", "error", "top-end");
                            }
                            break;
                        }
                    case "newpin":
                        {

                            banknumber = (string)obj["banknumber"];
                            bank = GetBankByBankNumber(banknumber);
                            if (bank == null || bank.ownercharid != character.id)
                            {
                                Helper.SendNotificationWithoutButton(player, "Du bist nicht der Inhaber von diesem Konto!", "error");
                                return;
                            }
                            player.SetData<Bank>("Player:Bank", bank);
                            player.TriggerEvent("Client:CallInput", "Neuer Pin", "Bitte gebe deinen neuen Pin ein:", "password", "****", 4, "NewPin");
                            break;
                        }
                    case "defaultkonto":
                        {
                            banknumber = (string)obj["banknumber"];
                            bank = GetBankByBankNumber(banknumber);
                            if (bank == null)
                            {
                                Helper.SendNotificationWithoutButton(player, "Ungültige Interaktion!", "error");
                                return;
                            }
                            character.defaultbank = banknumber;
                            Helper.SendNotificationWithoutButton(player, $"Das Konto {banknumber} wurde erfolgreich als Standardkonto gesetzt!", "success", "top-left", 6500);
                            break;
                        }
                    case "setgroupbank":
                        {
                            banknumber = (string)obj["banknumber"];
                            string owner = (string)obj["owner"];
                            Groups group = GroupsController.GetGroupById(character.mygroup);
                            if(group == null)
                            {
                                Helper.SendNotificationWithoutButton(player, "Du hast keine Gruppierung ausgewählt!", "error");
                                return;
                            }
                            if(group.leader != character.id)
                            {
                                Helper.SendNotificationWithoutButton(player, "Du bist nicht der Leader der Gruppierung!", "error");
                                return;
                            }
                            if(banknumber == "SA3701-100000")
                            {
                                Helper.SendNotificationWithoutButton(player, "Dieses Konto kann nicht als Gruppierungskonto gesetzt werden!", "error");
                                return;
                            }
                            if (group.status != 1)
                            {
                                Helper.SendNotificationWithoutButton(player, "Deine Gruppierung ist keine Firma!", "error");
                                return;
                            }
                            bank = GetBankByBankNumber(banknumber);
                            if (bank == null || bank.ownercharid != character.id)
                            {
                                Helper.SendNotificationWithoutButton(player, "Du bist nicht der Inhaber von diesem Konto!", "error");
                                return;
                            }
                            if(group.banknumber == banknumber)
                            {
                                Helper.SendNotificationWithoutButton(player, "Dieses Konto ist bereits das Firmenkonto!", "error");
                                return;
                            }
                            group.banknumber = banknumber;
                            bank.groupid = group.id;
                            Helper.SendNotificationWithoutButton(player, "Firmenkonto gesetzt!", "success");
                            GroupsController.UpdateGroupStats(player);
                            GroupsController.SaveGroup(group);
                            SaveBank(bank);
                            break;
                        }
                    case "deletegroupbank":
                        {
                            banknumber = (string)obj["banknumber"];
                            string owner = (string)obj["owner"];
                            Groups group = GroupsController.GetGroupById(character.mygroup);
                            if (group == null)
                            {
                                Helper.SendNotificationWithoutButton(player, "Du hast keine Gruppierung ausgewählt!", "error");
                                return;
                            }
                            if (group.leader != character.id)
                            {
                                Helper.SendNotificationWithoutButton(player, "Du bist nicht der Leader der Gruppierung!", "error");
                                return;
                            }
                            bank = GetBankByBankNumber(banknumber);
                            if (bank == null || bank.ownercharid != character.id)
                            {
                                Helper.SendNotificationWithoutButton(player, "Du bist nicht der Inhaber von diesem Konto!", "error");
                                return;
                            }
                            if(group.banknumber == "n/A")
                            {
                                Helper.SendNotificationWithoutButton(player, "Deine Gruppierung besitzt kein Firmenkonto!", "error");
                                return;
                            }
                            if (bank.groupid == group.id)
                            {
                                group.banknumber = "n/A";
                                bank.groupid = 0;
                                Helper.SendNotificationWithoutButton(player, "Firmenkonto entfernt!", "success");
                                GroupsController.UpdateGroupStats(player);
                                GroupsController.SaveGroup(group);
                                SaveBank(bank);
                            }
                            else
                            {
                                Helper.SendNotificationWithoutButton(player, "Dieses Konto ist nicht das Firmenkonto deiner Gruppierung!", "error");
                            }
                            break;
                        }
                    case "newowner":
                        {
                            banknumber = (string)obj["banknumber"];
                            string owner = (string)obj["owner"];
                            bank = GetBankByBankNumber(banknumber);
                            if (bank == null || bank.ownercharid != character.id)
                            {
                                Helper.SendNotificationWithoutButton(player, "Du bist nicht der Inhaber von diesem Konto!", "error");
                                return;
                            }
                            if (owner == "n/A" || owner.Length <= 3)
                            {
                                Helper.SendNotificationWithoutButton(player, "Ungültiger Eigentümername!", "error", "top-left");
                                return;
                            }
                            Player newPlayer = Helper.GetPlayerByCharacterName(owner);
                            if (newPlayer == null || newPlayer == player)
                            {
                                Helper.SendNotificationWithoutButton(player, "Ungültiger neuer Eigentümer!", "error", "top-left");
                                return;
                            }
                            Character nCharacter = Helper.GetCharacterData(newPlayer);
                            if(nCharacter == null)
                            {
                                Helper.SendNotificationWithoutButton(player, "Ungültiger neuer Eigentümer!", "error", "top-left");
                                return;
                            }
                            bank.ownercharid = nCharacter.id;
                            Helper.SendNotificationWithoutButton(player, "Neuer Eigentümer wurde gesetzt, die EC-Karte kannst du über das Inventar vergeben!", "success", "top-left");
                            player.TriggerEvent("Client:HideBankMenu");
                            break;
                        }
                    case "closebank":
                        {
                            banknumber = (string)obj["banknumber"];
                            bank = GetBankByBankNumber(banknumber);
                            if (bank == null || bank.ownercharid != character.id)
                            {
                                Helper.SendNotificationWithoutButton(player, "Du bist nicht der Inhaber von diesem Konto!", "error");
                                return;
                            }
                            if (banknumber == "SA3701-100000")
                            {
                                Helper.SendNotificationWithoutButton(player, "Dieses Konto kann nicht geschlossen werden!", "error");
                                return;
                            }
                            if (bank.bankvalue > 0)
                            {
                                Helper.SendNotificationWithoutButton(player, "Du musst dir zuerst das ganze Guthaben vom Konto auszahlen lassen!", "error");
                                return;
                            }
                            if(bank.groupid > 0)
                            {
                                Helper.SendNotificationWithoutButton(player, "Es muss zuerst das Firmenkonto entfernt werden!", "error");
                                return;
                            }
                            MySqlCommand command = General.Connection.CreateCommand();

                            command.CommandText = "DELETE FROM bank WHERE id=@id LIMIT 1";
                            command.Parameters.AddWithValue("@id", bank.id);
                            command.ExecuteNonQuery();

                            command.CommandText = "DELETE FROM standingorder WHERE bankfrom=@bankfrom";
                            command.Parameters.AddWithValue("@bankfrom", bank.banknumber);
                            command.ExecuteNonQuery();

                            command.CommandText = "DELETE FROM banksettings WHERE banknumber=@banknumber";
                            command.Parameters.AddWithValue("@banknumber", bank.banknumber);
                            command.ExecuteNonQuery();

                            command.CommandText = "DELETE FROM bankfile WHERE bankto=@bankto";
                            command.Parameters.AddWithValue("@bankto", bank.banknumber);
                            command.ExecuteNonQuery();

                            command.CommandText = "UPDATE characters SET items = REPLACE(items, '" + bank.banknumber + "', 'n/A') WHERE items LIKE @cbanknumber";
                            command.Parameters.AddWithValue("@cbanknumber", "%" + bank.banknumber + "%");
                            command.ExecuteNonQuery();

                            foreach (Player p in NAPI.Pools.GetAllPlayers())
                            {
                                if (p != null && p.GetOwnSharedData<bool>("Player:Spawned") == true)
                                {
                                    Character character2 = Helper.GetCharacterData(p);
                                    if (character2 != null)
                                    {
                                        ItemsController.DeleteItemWithProp(p, "" + bank.banknumber);
                                        ItemsController.UpdateInventory(p);
                                    }
                                }
                            }

                            bankList.Remove(bank);
                            bank = null;
                            Helper.SendNotificationWithoutButton(player, "Das Konto wurde erfolgreich geschlossen!", "success", "top-left");
                            player.TriggerEvent("Client:HideBankMenu");
                            break;
                        }
                    case "newpartnercard":
                        {
                            banknumber = (string)obj["banknumber"];
                            int atbank = (int)obj["bankat"];
                            bank = GetBankByBankNumber(banknumber);
                            if (bank == null || bank.ownercharid != character.id)
                            {
                                Helper.SendNotificationWithoutButton(player, "Du bist nicht der Inhaber von diesem Konto!", "error");
                                return;
                            }
                            if (character.cash < 85 && atbank == 0)
                            {
                                Helper.SendNotificationWithoutButton(player, "Du hast nicht genügend Geld Dabei - 85$!", "error");
                                return;
                            }
                            if (character.cash < 115 && atbank == 1)
                            {
                                Helper.SendNotificationWithoutButton(player, "Du hast nicht genügend Geld Dabei - 115$!", "error");
                                return;
                            }

                            Items newitem = ItemsController.CreateNewItem(player, character.id, "EC-Karte", "Player", 1, ItemsController.GetFreeItemID(player));
                            if (!ItemsController.CanPlayerHoldItem(player, newitem.weight))
                            {
                                newitem = null;
                                Helper.SendNotificationWithoutButton(player, "Du hast keinen Platz mehr im Inventar für die EC-Karte!", "error", "top-end");
                                return;
                            }
                            if (newitem != null)
                            {
                                newitem.props = "" + bank.banknumber;
                                tempData.itemlist.Add(newitem);
                                if (atbank == 0)
                                {
                                    CharacterController.SetMoney(player, -85);
                                }
                                else
                                {
                                    CharacterController.SetMoney(player, -115);
                                }
                                Helper.SendNotificationWithoutButton(player, "Die Partnerkarte (EC-Karte) wurde ausgehändigt!", "success", "top-left", 3500);
                            }
                            else
                            {
                                Helper.SendNotificationWithoutButton(player, "Die Partnerkarte (EC-Karte) konnte nicht ausgehändigt werden!", "error", "top-left", 3500);
                            }
                            break;
                        }
                    case "deleteallpartnercard":
                        {
                            banknumber = (string)obj["banknumber"];
                            bank = GetBankByBankNumber(banknumber);
                            if (bank == null || bank.ownercharid != character.id || bank.banknumber == "n/A")
                            {
                                Helper.SendNotificationWithoutButton(player, "Du bist nicht der Inhaber von diesem Konto!", "error");
                                return;
                            }

                            MySqlCommand command = General.Connection.CreateCommand();

                            command.CommandText = "UPDATE characters SET items = REPLACE(items, '" + bank.banknumber + "', 'n/A') WHERE items LIKE @banknumber AND name != @name";
                            command.Parameters.AddWithValue("@name", character.name);
                            command.Parameters.AddWithValue("@banknumber", "%" + bank.banknumber + "%");

                            command.ExecuteNonQuery();

                            foreach (Player p in NAPI.Pools.GetAllPlayers())
                            {
                                if (p != null && p.GetOwnSharedData<bool>("Player:Spawned") == true)
                                {
                                    Character character2 = Helper.GetCharacterData(p);
                                    if (character2 != null && character2.name != character.name)
                                    {
                                        ItemsController.DeleteItemWithProp(p, "" + bank.banknumber);
                                        ItemsController.UpdateInventory(p);
                                    }
                                }
                            }
                            Helper.SendNotificationWithoutButton(player, "Es wurden alle Partnerkarten gesperrt!", "success", "top-left", 3500);
                            break;
                        }
                    case "wrongpin":
                        {
                            Helper.SendNotificationWithoutButton(player, "Ungültiger Pin!", "error", "top-left", 2500);
                            break;
                        }
                    case "transfer":
                        {
                            value = (int)obj["value"];
                            if (value <= 0)
                            {
                                Helper.SendNotificationWithoutButton(player, "Ungültige Summe!", "error");
                                return;
                            }
                            string tage = (string)obj["tage"];
                            if (tage.Length <= 0)
                            {
                                Helper.SendNotificationWithoutButton(player, "Ungültige Tagesanzahl!", "error");
                            }
                            banknumber = (string)obj["banknumber"];
                            bank = GetBankByBankNumber(banknumber);
                            string empfänger = (string)obj["empfänger"];
                            string verwendungszweck = (string)obj["verwendungszweck"];
                            int valuetage = Convert.ToInt16(tage);
                            if (bank != null && HasBankRights(player, banknumber))
                            {
                                if (bank.bankvalue >= value)
                                {
                                    if(empfänger.Length < 10 || value <= 0 || verwendungszweck.Length <= 0 || valuetage < 0)
                                    {
                                        Helper.SendNotificationWithoutButton(player, $"Ungültige Eingaben!", "error");
                                        return;
                                    }
                                    if(bank.banknumber == banknumber)
                                    {
                                        Bank transferBank = GetBankByBankNumber(empfänger);
                                        if(transferBank != null)
                                        {
                                            if(transferBank.banknumber == bank.banknumber)
                                            {
                                                Helper.SendNotificationWithoutButton(player, $"Ungültiges Empfängerkonto!", "error");
                                                return;
                                            }
                                            bank.bankvalue -= value;
                                            transferBank.bankvalue += value;
                                            Helper.Bankfile(bank, transferBank, verwendungszweck, value);
                                            Helper.Bankfile(bank, transferBank, verwendungszweck, value, true);
                                            Helper.BankSettings(bank.banknumber, "Überweisung getätigt", value.ToString(), character.name);
                                            Helper.SendNotificationWithoutButton(player, "Überweisung getätigt, diese wird in kürze verbucht!", "success", "top-left", 4500);
                                            if (valuetage > 0)
                                            {
                                                MySqlCommand command = General.Connection.CreateCommand();
                                                command.CommandText = "INSERT INTO standingorder (ownercharid, bankfrom, bankto, bankvalue, banktext, days, timestamp) VALUES (@ownercharid, @bankfrom, @bankto, @bankvalue, @banktext, @days, @timestamp)";
                                                command.Parameters.AddWithValue("@ownercharid", character.id);
                                                command.Parameters.AddWithValue("@bankfrom", bank.banknumber);
                                                command.Parameters.AddWithValue("@bankto", transferBank.banknumber);
                                                command.Parameters.AddWithValue("@bankvalue", value);
                                                command.Parameters.AddWithValue("@banktext", verwendungszweck);
                                                command.Parameters.AddWithValue("@days", valuetage);
                                                command.Parameters.AddWithValue("@timestamp", Helper.UnixTimestamp());
                                                command.ExecuteNonQuery();
                                                Helper.BankSettings(bank.banknumber, "Dauerauftrag eingestellt", value.ToString(), character.name);
                                                Helper.SendNotificationWithoutButton(player, "Zusätzlich wurde ein Dauerauftrag eingestellt!", "success", "top-left", 4500);
                                            }
                                            string text = $"Überweisung von {bank.banknumber} nach {transferBank.banknumber}, Summe: {value}, Verwendungszweck: {verwendungszweck}";
                                            Helper.CreateAdminLog("banklog", text);
                                            SaveBank(transferBank);
                                        }
                                        else
                                        {
                                            Helper.SendNotificationWithoutButton(player, $"Ungültiges Empfängerkonto!", "error");
                                        }
                                    }
                                    else
                                    {
                                        Helper.SendNotificationWithoutButton(player, $"Ungültige Interaktion!", "error");
                                    }
                                }
                                else
                                {
                                    Helper.SendNotificationWithoutButton(player, $"Soviel Geld liegt nicht auf dem Konto - {value}$!", "error");
                                }
                            }
                            else
                            {
                                Helper.SendNotificationWithoutButton(player, "Ungültige Interaktion!", "error");
                            }
                            break;
                        }
                    case "transfer2":
                        {
                            value = (int)obj["value"];
                            if (value <= 0)
                            {
                                player.TriggerEvent("Client:SmartphoneInfoMessage", "Ungültiger Betrag!");
                                return;
                            }
                            banknumber = (string)obj["banknumber"];
                            bank = GetBankByBankNumber(banknumber);
                            string empfänger = (string)obj["empfänger"];
                            string verwendungszweck = (string)obj["verwendungszweck"];
                            string tage = (string)obj["tage"];
                            int valuetage = Convert.ToInt16(tage);
                            if (bank != null && HasBankRights(player, banknumber))
                            {
                                if (bank.bankvalue >= value)
                                {
                                    if (empfänger.Length < 10 || value <= 0 || verwendungszweck.Length <= 0 || valuetage < 0)
                                    {
                                        player.TriggerEvent("Client:SmartphoneInfoMessage", "Ungültige Eingaben!");
                                        return;
                                    }
                                    if (bank.banknumber == banknumber)
                                    {
                                        Bank transferBank = GetBankByBankNumber(empfänger);
                                        if (transferBank != null)
                                        {
                                            if (transferBank.banknumber == bank.banknumber)
                                            {
                                                player.TriggerEvent("Client:SmartphoneInfoMessage", "Ungültiges Empfängerkonto!");
                                                return;
                                            }
                                            bank.bankvalue -= value;
                                            transferBank.bankvalue += value;
                                            Helper.Bankfile(bank, transferBank, verwendungszweck, value);
                                            Helper.Bankfile(bank, transferBank, verwendungszweck, value, true);
                                            Helper.BankSettings(bank.banknumber, "Überweisung getätigt", value.ToString(), character.name);
                                            if (valuetage > 0)
                                            {
                                                MySqlCommand command = General.Connection.CreateCommand();
                                                command.CommandText = "INSERT INTO standingorder (ownercharid, bankfrom, bankto, bankvalue, banktext, days, timestamp) VALUES (@ownercharid, @bankfrom, @bankto, @bankvalue, @banktext, @days, @timestamp)";
                                                command.Parameters.AddWithValue("@ownercharid", character.id);
                                                command.Parameters.AddWithValue("@bankfrom", bank.banknumber);
                                                command.Parameters.AddWithValue("@bankto", transferBank.banknumber);
                                                command.Parameters.AddWithValue("@bankvalue", value);
                                                command.Parameters.AddWithValue("@banktext", verwendungszweck);
                                                command.Parameters.AddWithValue("@days", valuetage);
                                                command.Parameters.AddWithValue("@timestamp", Helper.UnixTimestamp());
                                                command.ExecuteNonQuery();
                                                Helper.BankSettings(bank.banknumber, "Dauerauftrag eingestellt", value.ToString(), character.name);
                                                player.TriggerEvent("Client:SmartphoneInfoMessage", "Überweisung + Dauerauftrag getätigt!");
                                            }
                                            else
                                            {
                                                player.TriggerEvent("Client:SmartphoneInfoMessage", "Überweisung getätigt!");
                                            }
                                            string text = $"Überweisung von {bank.banknumber} nach {transferBank.banknumber}, Summe: {value}, Verwendungszweck: {verwendungszweck}";
                                            Helper.CreateAdminLog("banklog", text);
                                            SaveBank(transferBank);
                                        }
                                        else
                                        {
                                            player.TriggerEvent("Client:SmartphoneInfoMessage", "Ungültiges Empfängerkonto!");
                                        }
                                    }
                                    else
                                    {
                                        player.TriggerEvent("Client:SmartphoneInfoMessage", "Ungültige Interaktion!");
                                    }
                                }
                                else
                                {
                                    player.TriggerEvent("Client:SmartphoneInfoMessage", $"Soviel Geld liegt nicht auf dem Konto - {value}$!");
                                }
                            }
                            else
                            {
                                player.TriggerEvent("Client:SmartphoneInfoMessage", "Ungültige Interaktion!");
                            }
                            break;
                        }
                    case "invoice":
                        {
                            Groups group = null;
                            Groups groupEmpf = null;
                            Player newPlayer = null;
                            string ivtext = (string)obj["group"];
                            value = (int)obj["value"];
                            if (value <= 0)
                            {
                                player.TriggerEvent("Client:SmartphoneInfoMessage", "Ungültiger Betrag!");
                                return;
                            }                       
                            else
                            {
                                if (ivtext == "Privatrechnung")
                                {
                                    newPlayer = Helper.GetPlayerByCharacterName((string)obj["empfänger"]);
                                    if (newPlayer == null || newPlayer == player)
                                    {
                                        player.TriggerEvent("Client:SmartphoneInfoMessage", "Keine Privatperson als Empfänger gefunden!");
                                        return;
                                    }
                                }
                                else if (ivtext == "Firmen/Gruppierungsrechnung")
                                {
                                    groupEmpf = GroupsController.GetGroupByName((string)obj["empfänger"]);
                                    if (groupEmpf == null || groupEmpf.id == character.mygroup)
                                    {
                                        player.TriggerEvent("Client:SmartphoneInfoMessage", "Keine Firma/Gruppierung als Empfänger gefunden!");
                                        return;
                                    }
                                }
                                else if (ivtext == "Staatsrechnung")
                                {
                                    if ((string)obj["empfänger"] != "Staat")
                                    {
                                        player.TriggerEvent("Client:SmartphoneInfoMessage", "Als Empfänger muss Staat eingegeben werden!");
                                        return;
                                    }
                                }
                                if (ivtext == "Privatrechnung")
                                {
                                    bank = BankController.GetDefaultBank(player, character.defaultbank);
                                    if (bank == null)
                                    {
                                        player.TriggerEvent("Client:SmartphoneInfoMessage", "Es wurde kein Standardkonto gefunden!");
                                        return;
                                    }
                                    banknumber = character.defaultbank;
                                }
                                else if (ivtext == "Firmen/Gruppierungsrechnung")
                                {
                                    group = GroupsController.GetGroupById(character.mygroup);
                                    if(group == null)
                                    {
                                        player.TriggerEvent("Client:SmartphoneInfoMessage", "Keine Gruppierung ausgewählt!");
                                        return;
                                    }
                                    bank = BankController.GetDefaultBank(player, group.banknumber);
                                    if (bank == null)
                                    {
                                        player.TriggerEvent("Client:SmartphoneInfoMessage", "Es wurde kein Gruppierungs/Firmenkonto gefunden!");
                                        return;
                                    }
                                    banknumber = group.banknumber;
                                }
                                else
                                {
                                    banknumber = "SA3701-100000";
                                }
                                Invoices invoice = new Invoices();
                                invoice.value = value;
                                invoice.empfänger = (string)obj["empfänger"];
                                invoice.modus = (string)obj["group"];
                                invoice.text = (string)obj["banktext"];
                                invoice.banknumber = banknumber;
                                invoice.timestamp = Helper.UnixTimestamp();

                                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                                db.Insert(invoice);

                                Helper.invoicesList.Add(invoice);

                                if (group != null)
                                {
                                    Helper.CreateGroupLog(group.id, $"{character.name} hat eine Rechnungs[{invoice.id}] an {invoice.empfänger} in Höhe von {invoice.value}$, Rechnungstext: {invoice.text} ausgestellt!");
                                }

                                if (invoice.modus == "Privatrechnung")
                                {
                                    Smartphone smartphone = SmartphoneController.GetSmartPhoneByNumber(character.lastsmartphone);
                                    if (smartphone != null && smartphone.akku > 0)
                                    {
                                        BankController.OnLoadInvoices(newPlayer);
                                        newPlayer.TriggerEvent("Client:PlaySoundPeep2");
                                        Helper.SendNotificationWithoutButton(newPlayer, $"Neue Rechnung[{invoice.id}] in Höhe von {invoice.value}$ erhalten!", "info", "top-left", 4500);
                                    }
                                }
                                else if (invoice.modus == "Firmen/Gruppierungsrechnung")
                                {
                                    Helper.CreateGroupLog(groupEmpf.id, $"{character.name} hat der Firma eine Rechnung[{invoice.id}] in Höhe von {invoice.value}$, Rechnungstext: {invoice.text} ausgestellt!");
                                }
                                else
                                {
                                    Helper.CreateFactionLog(4, $"{character.name} hat dem Staat eine Rechnung[{invoice.id}] in Höhe von {invoice.value}$, Rechnungstext: {invoice.text} ausgestellt!");
                                }

                                player.TriggerEvent("Client:SmartphoneInfoMessage", "Rechnung wurde ausgestellt!");
                            }
                            break;
                        }
                    case "withdraw":
                        {
                            value = (int)obj["value"];
                            if(value <= 0)
                            {
                                Helper.SendNotificationWithoutButton(player, "Ungültige Summe!", "success");
                                return;
                            }
                            banknumber = (string)obj["banknumber"];
                            bank = GetBankByBankNumber(banknumber);
                            if(bank != null && HasBankRights(player, banknumber))
                            {
                                if ((int)obj["bankat"] != bank.banktype)
                                {
                                    if (bank.banktype == 0)
                                    {
                                        gebuehr = value / 100 * 3;
                                    }
                                    else
                                    {
                                        gebuehr = value / 100 * 5;
                                    }
                                    if (gebuehr <= 0) gebuehr = 1;
                                    if(gebuehr > 0 && bank.banktype == 0 && (int)obj["bankat"] == 2)
                                    {
                                        gebuehr = 0;
                                    }
                                }
                                if (bank.bankvalue >= (value+gebuehr))
                                {
                                    bank.bankvalue -= value + gebuehr;
                                    CharacterController.SetMoney(player, value);
                                    Helper.SendNotificationWithoutButton(player, $"Du hast {value}$ von dem Konto abgehoben!", "success");
                                    if(gebuehr > 0)
                                    {
                                        Helper.SendNotificationWithoutButton(player, $"Außerdem musstest du {gebuehr}$ Gebühr bezahlen!", "info");
                                    }
                                    Helper.BankSettings(bank.banknumber, "Geld abgehoben", "" + value, character.name);
                                }
                                else
                                {
                                    Helper.SendNotificationWithoutButton(player, $"Soviel Geld liegt nicht auf dem Konto - {value + gebuehr}$!", "error");
                                }
                            }
                            else
                            {
                                Helper.SendNotificationWithoutButton(player, "Ungültige Interaktion!", "error");
                            }
                            break;
                        }
                    case "deposit":
                        {
                            value = (int)obj["value"];
                            if (value <= 0)
                            {
                                Helper.SendNotificationWithoutButton(player, "Ungültige Summe!", "success");
                                return;
                            }
                            banknumber = (string)obj["banknumber"];
                            bank = GetBankByBankNumber(banknumber);
                            if (bank != null && HasBankRights(player, banknumber))
                            {
                                if (character.cash >= value)
                                {
                                    bank.bankvalue += value;
                                    CharacterController.SetMoney(player, -value);
                                    Helper.SendNotificationWithoutButton(player, $"Du hast {value}$ auf das Konto eingezahlt!", "success");
                                    Helper.BankSettings(bank.banknumber, "Geld eingezahlt", "" + value, character.name);
                                }
                                else
                                {
                                    Helper.SendNotificationWithoutButton(player, "Du hast nicht genügend Geld dabei!", "error");
                                }
                            }
                            else
                            {
                                Helper.SendNotificationWithoutButton(player, "Ungültige Interaktion!", "error");
                            }
                            break;
                        }
                    case "getbankfiles":
                        {
                            banknumber = (string)obj["banknumber"];
                            bank = GetBankByBankNumber(banknumber);
                            if (bank != null)
                            {
                                player.TriggerEvent("Client:ShowBankFiles", NAPI.Util.ToJson(GetBankFile(bank.id)), NAPI.Util.ToJson(GetBankSetting(banknumber)));
                            }
                            break;
                        }
                    case "deletestandingorder":
                        {
                            value = (int)obj["value"];
                            if(value <= 0)
                            {
                                Helper.SendNotificationWithoutButton(player, "Ungültige Transaktion!", "error");
                                return;
                            }
                            MySqlCommand command = General.Connection.CreateCommand();
                            command.CommandText = "DELETE FROM standingorder WHERE id=@id LIMIT 1";
                            command.Parameters.AddWithValue("@id", value);
                            command.ExecuteNonQuery();

                            Helper.BankSettings(bank.banknumber, "Dauerauftrag gelöscht", value.ToString(), character.name);
                            Helper.SendNotificationWithoutButton(player, "Dauerauftrag wurde gelöscht!", "success");
                            break;
                        }
                    case "getstandingorder":
                        {
                            banknumber = (string)obj["banknumber"];
                            player.TriggerEvent("Client:ShowStandingOrder", NAPI.Util.ToJson(GetStandingOrder(banknumber)));
                            break;
                        }
                }
                if (bank != null || setting == "new")
                {
                    if (bank != null)
                    {
                        SaveBank(bank);
                        CharacterController.SaveCharacter(player);
                    }
                    if (setting != "transfer2")
                    {
                        UpdateBankAccounts(player);
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[BankSettings]: " + e.ToString());
            }
        }
    }
}
