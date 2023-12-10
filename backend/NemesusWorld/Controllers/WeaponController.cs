using GTANetworkAPI;
using NemesusWorld.Database;
using NemesusWorld.Models;
using NemesusWorld.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NemesusWorld.Controllers
{
    class WeaponController : Script
    {
        [ServerEvent(Event.PlayerWeaponSwitch)]
        public static void OnPlayerWeaponSwitch(Player player, WeaponHash oldWeapon, WeaponHash newWeapon)
        {
            try
            {
                if (newWeapon == WeaponHash.Grenade || newWeapon == WeaponHash.Bzgas || newWeapon == WeaponHash.Smokegrenade || newWeapon == WeaponHash.Molotov || newWeapon == WeaponHash.Snowball) return;
                TempData tempData = Helper.GetCharacterTempData(player);
                if (tempData == null) return;
                string whash = Convert.ToString(newWeapon).ToLower();
                String tintString = "";
                String weaponComponents = "";
                if (tempData.weaponTints.Count > 0 && tempData.weaponTints.ContainsKey(whash) && whash != "unarmed")
                {
                    tintString = tempData.weaponTints[whash];
                    if (tintString != null && tintString.Length > 0)
                    {
                        player.SetSharedData("Player:WeaponTint", Convert.ToInt32(tintString));
                    }
                }
                else
                {
                    player.SetSharedData("Player:WeaponTint", -1);
                }
                if (tempData.weaponComponents.Count > 0 && tempData.weaponComponents.ContainsKey(whash) && whash != "unarmed")
                {
                    weaponComponents = tempData.weaponComponents[whash];
                    if (weaponComponents != null && weaponComponents != "|")
                    {
                        player.SetSharedData("Player:WeaponComponents", $"{weaponComponents}");
                    }
                }
                else
                {
                    player.SetSharedData("Player:WeaponComponents", "-1");
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnPlayerWeaponSwitch]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:WeaponSet")]
        public static void OnWeaponSet(Player player, int index, string component, int choose)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                TempData tempData = Helper.GetCharacterTempData(player);
                if (tempData == null || character == null) return;

                int number = 0;
                int price = 0;
                string csv = "";

                Business bizz = Business.GetClosestBusiness(player, 40.5f);
                if (choose != 3)
                {
                    if (bizz == null)
                    {
                        Helper.SendNotificationWithoutButton(player, "Du kannst hier nichts kaufen!", "error");
                        return;
                    }
                }

                Bank bank = BankController.GetDefaultBank(player, character.defaultbank);

                Items item = ItemsController.GetItemFromWeaponHash(player, NAPI.Player.GetPlayerCurrentWeapon(player));
                if (item != null)
                {
                    if (index == 1)
                    {
                        number = Convert.ToInt32(component);

                        if (choose != 3)
                        {
                            price = Convert.ToInt32(1250 * bizz.multiplier);
                            if (bizz.products < (price / 25) / 4)
                            {
                                Helper.SendNotificationWithoutButton(player, "Unsere Lager sind leider leer!", "error");
                                return;
                            }
                            if (choose == 0)
                            {
                                if (character.cash < price)
                                {
                                    Helper.SendNotificationWithoutButton(player, $"Du hast nicht genügend Geld dabei {price}$!", "error");
                                    return;
                                }
                                CharacterController.SetMoney(player, -price);
                            }
                            else
                            {
                                if (bank == null)
                                {
                                    Helper.SendNotificationWithoutButton(player, "Es wurde kein Standardkonto gefunden!", "error", "top-left");
                                    return;
                                }
                                if (bank.bankvalue < price)
                                {
                                    Helper.SendNotificationWithoutButton(player, $"Soviel Geld liegt nicht auf dem Konto - {price}$!", "error", "top-left");
                                    return;
                                }
                                bank.bankvalue -= price;
                                Helper.BankSettings(bank.banknumber, "Ammunation Rechnung bezahlt", price.ToString(), character.name);
                            }
                            Business.ManageBizzCash(bizz, price);
                            Helper.SendNotificationWithoutButton(player, $"Waffenfarbton erfolgreich für {price}$ gesetzt!", "success", "top-left", 3500);
                        }
                        else
                        {
                            Helper.SendNotificationWithoutButton(player, $"Waffenfarbton erfolgreich ausgewählt!", "success", "top-left", 3500);
                        }
                        tempData.weaponTints.Remove($"{Convert.ToString(NAPI.Player.GetPlayerCurrentWeapon(player)).ToLower()}");
                        tempData.weaponTints.Add($"{Convert.ToString(NAPI.Player.GetPlayerCurrentWeapon(player)).ToLower()}", $"{number}");
                        player.SetSharedData("Player:WeaponTint", Convert.ToInt32(number));
                        string[] weaponArray = new string[7];
                        weaponArray = item.props.Split(",");
                        item.props = $"{NAPI.Player.GetPlayerWeaponAmmo(player, (WeaponHash)WeaponController.GetWeaponHashFromName(item.description))},{weaponArray[1]},{weaponArray[2]},{weaponArray[3]},{weaponArray[4]},{number},{weaponArray[6]}";
                        item.props.Trim();
                    }
                    else
                    {
                        if (index == 2)
                        {
                            if (choose != 3)
                            {
                                price = Convert.ToInt32(1950 * bizz.multiplier);

                                if (bizz.products < (price / 25) / 4)
                                {
                                    Helper.SendNotificationWithoutButton(player, "Unsere Lager sind leider leer!", "error");
                                    return;
                                }
                                if (choose == 0)
                                {
                                    if (character.cash < price)
                                    {
                                        Helper.SendNotificationWithoutButton(player, $"Du hast nicht genügend Geld dabei {price}$!", "error");
                                        return;
                                    }
                                    CharacterController.SetMoney(player, -price);
                                }
                                else
                                {
                                    if (bank == null)
                                    {
                                        Helper.SendNotificationWithoutButton(player, "Es wurde kein Standardkonto gefunden!", "error", "top-left");
                                        return;
                                    }
                                    if (bank.bankvalue < price)
                                    {
                                        Helper.SendNotificationWithoutButton(player, $"Soviel Geld liegt nicht auf dem Konto - {price}$!", "error", "top-left");
                                        return;
                                    }
                                    bank.bankvalue -= price;
                                    Helper.BankSettings(bank.banknumber, "Ammunation Rechnung bezahlt", price.ToString(), character.name);
                                }
                                Business.ManageBizzCash(bizz, price);
                            }
                            string[] weaponArray = new string[7];
                            weaponArray = item.props.Split(",");
                            if (weaponArray[6] == "|")
                            {
                                weaponArray[6] = component + "|";
                                csv = weaponArray[6];
                            }
                            else if (weaponArray[6] != "|")
                            {
                                List<String> weaponComponents = new List<String>();
                                weaponComponents = weaponArray[6].Split("|").ToList();
                                weaponComponents.RemoveAll(s => String.IsNullOrWhiteSpace(s));
                                weaponComponents.Add(component + "|");

                                csv = String.Join("|", weaponComponents.Select(x => x.ToString()).ToArray());
                                weaponArray[6] = csv;
                            }
                            item.props = $"{NAPI.Player.GetPlayerWeaponAmmo(player, (WeaponHash)WeaponController.GetWeaponHashFromName(item.description))},{weaponArray[1]},{weaponArray[2]},{weaponArray[3]},{weaponArray[4]},{weaponArray[5]},{weaponArray[6]}";
                            item.props.Trim();
                            tempData.weaponComponents.Remove($"{Convert.ToString(NAPI.Player.GetPlayerCurrentWeapon(player)).ToLower()}");
                            if (choose != 3)
                            {
                                Helper.SendNotificationWithoutButton(player, $"Waffenkomponente erfolgreich für {price}$ erworben!", "success", "top-left", 3500);
                            }
                            else
                            {
                                Helper.SendNotificationWithoutButton(player, $"Waffenkomponente erfolgreich ausgewählt!", "success", "top-left", 3500);
                            }
                            tempData.weaponComponents.Add($"{Convert.ToString(NAPI.Player.GetPlayerCurrentWeapon(player)).ToLower()}", $"{csv}");
                            player.SetSharedData("Player:WeaponComponents", $"{csv}");
                        }
                        else
                        {
                            string[] weaponArray = new string[7];
                            weaponArray = item.props.Split(",");

                            List<String> weaponComponents = new List<String>();
                            weaponComponents = weaponArray[6].Split("|").ToList();
                            weaponComponents.RemoveAll(s => s == component);
                            weaponComponents.RemoveAll(s => String.IsNullOrWhiteSpace(s));

                            if (weaponComponents.Count > 1)
                            {
                                csv = String.Join("|", weaponComponents.Select(x => x.ToString()).ToArray());
                            }
                            else if (weaponComponents.Count == 1)
                            {
                                csv = weaponComponents[0] + "|";
                            }
                            else
                            {
                                csv = "|";
                            }
                            weaponArray[6] = csv;
                            item.props = $"{NAPI.Player.GetPlayerWeaponAmmo(player, (WeaponHash)WeaponController.GetWeaponHashFromName(item.description))},{weaponArray[1]},{weaponArray[2]},{weaponArray[3]},{weaponArray[4]},{weaponArray[5]},{weaponArray[6]}";
                            item.props.Trim();
                            tempData.weaponComponents.Remove($"{Convert.ToString(NAPI.Player.GetPlayerCurrentWeapon(player)).ToLower()}");
                            Helper.SendNotificationWithoutButton(player, $"Waffenkomponente erfolgreich abgebaut!", "success", "top-left", 3500);
                            if (csv == "|")
                            {
                                player.SetSharedData("Player:WeaponComponents", "-1");
                            }
                            else
                            {
                                tempData.weaponComponents.Add($"{Convert.ToString(NAPI.Player.GetPlayerCurrentWeapon(player)).ToLower()}", $"{csv}");
                                player.SetSharedData("Player:WeaponComponents", $"{csv}");
                            }
                        }
                    }
                    player.TriggerEvent("Client:UpdateWeaponComponent", index, component, csv);
                }
                else
                {
                    Helper.SendNotificationWithoutButton(player, "Ungültige Waffe!", "success", "top-left", 3500);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnWeaponSet]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:CheckForEmptyWeapon")]
        public static void OnCheckForEmptyWeapon(Player player)
        {
            try
            {
                TempData tempData = Helper.GetCharacterTempData(player);
                if (tempData == null) return;

                bool found = false;
                foreach (Items iteminlist in tempData.itemlist.ToList())
                {
                    if (iteminlist != null && iteminlist.type == 5)
                    {
                        string[] propArray = new string[7];
                        propArray = iteminlist.props.Split(",");
                        if (propArray.Length > 0 && propArray[1] == "1")
                        {
                            if (iteminlist.description.ToLower() == "granate" || iteminlist.description.ToLower() == "bzgas" || iteminlist.description.ToLower() == "rauchgranate" || iteminlist.description.ToLower() == "molotowcocktail" || iteminlist.description.ToLower() == "snowball")
                            {
                                if (NAPI.Player.GetPlayerWeaponAmmo(player, (WeaponHash)GetWeaponHashFromName(iteminlist.description)) <= 0)
                                {
                                    ItemsController.RemoveItem(player, iteminlist.itemid);
                                    NAPI.Player.SetPlayerCurrentWeapon(player, WeaponHash.Unarmed);
                                    found = true;
                                    break;
                                }
                            }
                            if (iteminlist.description.ToLower().Contains("schutzweste"))
                            {
                                if (NAPI.Player.GetPlayerArmor(player) <= 0)
                                {
                                    ItemsController.RemoveItem(player, iteminlist.itemid);
                                    Helper.SetPlayerArmor(player, 0);
                                    NAPI.Player.SetPlayerClothes(player, 9, 0, 0);
                                    found = true;
                                    break;
                                }
                            }
                        }
                    }
                }
                if (found == true)
                {
                    ItemsController.UpdateInventory(player);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnCheckForEmptyWeaponchon]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:SelectGun")]
        public static void OnSelectGun(Player player, int itemid, int status, bool hidemessage = false)
        {
            try
            {
                TempData tempData = Helper.GetCharacterTempData(player);
                Character character = Helper.GetCharacterData(player);
                if (tempData == null) return;

                Items item = ItemsController.GetItemFromID(player, itemid);
                if (item != null)
                {
                    string props = item.props;
                    string[] propArray = new string[7];
                    propArray = props.Split(",");
                    int newstatus = status;
                    bool clear = false;
                    if (status == 0)
                    {
                        if (!item.description.ToLower().Contains("schutzweste"))
                        {
                            string whash = GetWeaponHash2FromName(item.description);
                            newstatus = 1;
                            if (hidemessage == false)
                            {
                                Helper.SendNotificationWithoutButton(player, "Waffe ausgerüstet!", "success", "top-left", 3500);
                            }
                            NAPI.Player.GivePlayerWeapon(player, (WeaponHash)GetWeaponHashFromName(item.description), Convert.ToInt32(propArray[0]));
                            WeaponHash weaponHash = NAPI.Player.GetPlayerCurrentWeapon(player);
                            tempData.weaponTints.Add($"{whash.ToLower()}", $"{propArray[5]}");
                            player.SetSharedData("Player:WeaponTint", Convert.ToInt32(propArray[5]));
                            if (propArray[6] != "|")
                            {
                                tempData.weaponComponents.Add($"{whash.ToLower()}", $"{propArray[6]}");
                                player.SetSharedData("Player:WeaponComponents", $"{propArray[6]}");
                            }
                            if (item.description.ToLower() == "granate" || item.description.ToLower() == "rauchgranate" || item.description.ToLower() == "bz-gas" || item.description.ToLower() == "molotowcocktail" || item.description.ToLower() == "snowball")
                            {
                                NAPI.Player.SetPlayerCurrentWeapon(player, weaponHash);
                            }
                        }
                        else
                        {
                            newstatus = 1;
                            Helper.SendNotificationWithoutButton(player, $"{item.description} ausgerüstet!", "success", "top-left", 3500);
                            Helper.SetPlayerArmor(player, Convert.ToInt32(propArray[0]));
                            NAPI.Player.SetPlayerClothes(player, 9, Convert.ToInt32(character.armor), 0);
                        }
                    }
                    else
                    {
                        clear = true;
                        newstatus = 0;
                        if (!item.description.ToLower().Contains("schutzweste"))
                        {
                            if (hidemessage == false)
                            {
                                Helper.SendNotificationWithoutButton(player, "Waffe abgelegt!", "success", "top-left", 3500);
                            }
                        }
                        else
                        {
                            if (hidemessage == false)
                            {
                                Helper.SendNotificationWithoutButton(player, $"{item.description} abgelegt!", "success", "top-left", 3500);
                            }
                            NAPI.Player.SetPlayerClothes(player, 9, 0, 0);
                            Helper.SetPlayerArmor(player, 0);
                        }
                    }
                    if (!item.description.ToLower().Contains("schutzweste"))
                    {
                        item.props = $"{NAPI.Player.GetPlayerWeaponAmmo(player, (WeaponHash)GetWeaponHashFromName(item.description))},{newstatus},{propArray[2]},{propArray[3]},{propArray[4]},{propArray[5]},{propArray[6]}";
                        item.props.Trim();
                        if (clear == true)
                        {
                            NAPI.Player.SetPlayerWeaponAmmo(player, (WeaponHash)GetWeaponHashFromName(item.description), 0);
                            NAPI.Player.RemovePlayerWeapon(player, (WeaponHash)GetWeaponHashFromName(item.description));
                            tempData.weaponTints.Remove($"{GetWeaponHash2FromName(item.description).ToLower()}");
                            player.SetSharedData("Player:WeaponTint", -1);
                            tempData.weaponComponents.Remove($"{GetWeaponHash2FromName(item.description).ToLower()}");
                            player.SetSharedData("Player:WeaponComponents", "-1");
                        }
                    }
                    else
                    {
                        item.props = $"{propArray[0]},{newstatus},{propArray[2]},{propArray[3]},{propArray[4]},{propArray[5]},{propArray[6]}";
                        item.props.Trim();
                    }
                    ItemsController.UpdateInventory(player);
                }
                else
                {
                    if (hidemessage == false)
                    {
                        Helper.SendNotificationWithoutButton(player, "Ungültiges Item!", "error", "top-left", 3500);
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnSelectGun]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:GetWeaponDamage")]
        public static void OnGetWeaponDamage(Player player, int damage)
        {
            try
            {
                TempData tempData = Helper.GetCharacterTempData(player);
                if(tempData != null)
                {
                    tempData.tempValue = damage;
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnGetWeaponDamage]: " + e.ToString());
            }
        }

            [RemoteEvent("Server:LastDamage")]
        public static void GetActualWeaponID(Player ownPlayer, int remoteId)
        {
            try
            {
                if (remoteId == -1) return;
                Player player = Helper.GetPlayerByNameOrID("" + remoteId);
                TempData tempData2 = Helper.GetCharacterTempData(ownPlayer);
                if (player != null)
                {
                    if (tempData2 != null)
                    {
                        TempData tempData = Helper.GetCharacterTempData(player);
                        Character character = Helper.GetCharacterData(player);

                        if (character != null)
                        {
                            tempData2.killed = character.name;

                            foreach (Items iteminlist in tempData.itemlist)
                            {
                                if (iteminlist != null && iteminlist.type == 5 && !iteminlist.description.ToLower().Contains("schutzweste"))
                                {
                                    string[] weaponArray = new string[7];
                                    weaponArray = iteminlist.props.Split(",");
                                    if (weaponArray[1] == "1" && (WeaponHash)WeaponController.GetWeaponHashFromName(iteminlist.description) == NAPI.Player.GetPlayerCurrentWeapon(player))
                                    {
                                        if (GetWeaponClass(iteminlist.description) == "0" || GetWeaponClass(iteminlist.description) == "3" || GetWeaponClass(iteminlist.description) == "4" || GetWeaponClass(iteminlist.description) == "5" || GetWeaponClass(iteminlist.description) == "6" || GetWeaponClass(iteminlist.description) == "7")
                                        {
                                            tempData2.killed = weaponArray[3];
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            tempData2.killed = "";
                        }
                    }
                }
                else
                {
                    tempData2.killed = "";
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetActualWeaponID]: " + e.ToString());
            }
        }

        public static Int64 GetWeaponHashFromName(string weaponName)
        {
            try
            {
                switch (weaponName.ToLower())
                {
                    case "faust":
                        {
                            return 0xA2719263;
                        }
                    case "pistole":
                        {
                            return 0x1B06D571;
                        }
                    case "pistole-mk2":
                        {
                            return 0xBFE256D4;
                        }
                    case "pistole-50":
                        {
                            return 0x99AEEB3B;
                        }
                    case "combat-pistole":
                        {
                            return 0x5EF9FEC4;
                        }
                    case "heavy-pistole":
                        {
                            return 0xD205520E;
                        }
                    case "keramik-pistole":
                        {
                            return 0x2B5EF5EC;
                        }
                    case "flaregun":
                        {
                            return 0x47757124;
                        }
                    case "revolver":
                        {
                            return 0xC1B3C3D1;
                        }
                    case "revolver-mk2":
                        {
                            return 0xCB96392F;
                        }
                    case "sns-pistole":
                        {
                            return 0xBFD21232;
                        }
                    case "sns-pistole-mk2":
                        {
                            return 0x88374054;
                        }
                    case "taser":
                        {
                            return 0x3656C8C1;
                        }
                    case "dolch":
                        {
                            return 0x92A27487;
                        }
                    case "baseballschläger":
                        {
                            return 0x958A4A8F;
                        }
                    case "brechstange":
                        {
                            return 0x84BD7BFD;
                        }
                    case "taschenlampe":
                        {
                            return 0x8BB05FD7;
                        }
                    case "golfschläger":
                        {
                            return 0x440E4788;
                        }
                    case "axt":
                        {
                            return 0xF9DCBF2D;
                        }
                    case "schlagring":
                        {
                            return 0xD8DF3C3C;
                        }
                    case "messer":
                        {
                            return 0x99B507EA;
                        }
                    case "machete":
                        {
                            return 0xDD5DF8D9;
                        }
                    case "klappmesser":
                        {
                            return 0xDFE37640;
                        }
                    case "schlagstock":
                        {
                            return 0x678B81B1;
                        }
                    case "poolcue":
                        {
                            return 0x94117305;
                        }
                    case "micro-smg":
                        {
                            return 0x13532244;
                        }
                    case "mp5":
                        {
                            return 0x2BE6766B;
                        }
                    case "mp5-mk2":
                        {
                            return 0x78A97CD0;
                        }
                    case "assault-smg":
                        {
                            return 0xEFE7E2DF;
                        }
                    case "combat-pdw":
                        {
                            return 0x0A3D4D34;
                        }
                    case "tec9":
                        {
                            return 0xDB1AA450;
                        }
                    case "mini-smg":
                        {
                            return 0xBD248B55;
                        }
                    case "pumpshotgun":
                        {
                            return 0x1D073A89;
                        }
                    case "pumpshotgun-mk2":
                        {
                            return 0x555AF99A;
                        }
                    case "sawnoffshotgun":
                        {
                            return 0x7846A318;
                        }
                    case "combatshotgun":
                        {
                            return 0x5A96BA4;
                        }
                    case "musket":
                        {
                            return 0xA89CB99E;
                        }
                    case "angriffsgewehr":
                        {
                            return 0xBFEFFF6D;
                        }
                    case "angriffsgewehr-mk2":
                        {
                            return 0x394F415C;
                        }
                    case "m16":
                        {
                            return 0xD1D5F52B;
                        }
                    case "karabiner-gewehr":
                        {
                            return 0x83BF0278;
                        }
                    case "karabiner-gewehr-mk2":
                        {
                            return 0xFAD1F1C9;
                        }
                    case "spezial-karabiner":
                        {
                            return 0xC0A3098D;
                        }
                    case "spezial-karabiner-mk2":
                        {
                            return 0x969C3D67;
                        }
                    case "kompaktgewehr":
                        {
                            return 0x624FE830;
                        }
                    case "maschinengewehr":
                        {
                            return 0x9D07F764;
                        }
                    case "gusenberg":
                        {
                            return 0x61012683;
                        }
                    case "sniper":
                        {
                            return 0x05FC3C11;
                        }
                    case "rpg":
                        {
                            return 0xB1CA77B1;
                        }
                    case "granate":
                        {
                            return 0x93E220BD;
                        }
                    case "snowball":
                        {
                            return 0x787F0BB;
                        }
                    case "bzgas":
                        {
                            return 0xA0973D5E;
                        }
                    case "molotowcocktail":
                        {
                            return 0x24B17070;
                        }
                    case "rauchgranate":
                        {
                            return 0xFDBC8A50;
                        }
                    case "feuerlöscher":
                        {
                            return 0x060EC506;
                        }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnSelectGun]: " + e.ToString());
            }
            return 0x1B06D571;
        }

        public static String GetWeaponHash2FromName(string weaponName)
        {
            switch (weaponName.ToLower())
            {
                case "faust":
                    {
                        return "unarmed";
                    }
                case "pistole":
                    {
                        return "pistol";
                    }
                case "pistole-mk2":
                    {
                        return "pistol_mk2";
                    }
                case "pistole-50":
                    {
                        return "pistol50";
                    }
                case "combat-pistole":
                    {
                        return "combatpistol";
                    }
                case "heavy-pistole":
                    {
                        return "heavvypistol";
                    }
                case "keramik-pistole":
                    {
                        return "ceramicpistol";
                    }
                case "flaregun":
                    {
                        return "flaregun";
                    }
                case "revolver":
                    {
                        return "revolver";
                    }
                case "revolver-mk2":
                    {
                        return "revolver_mk2";
                    }
                case "sns-pistole":
                    {
                        return "snspistol";
                    }
                case "sns-pistole-mk2":
                    {
                        return "snspistol_mk2";
                    }
                case "taser":
                    {
                        return "stungun";
                    }
                case "dolch":
                    {
                        return "dagger";
                    }
                case "baseballschläger":
                    {
                        return "bat";
                    }
                case "brechstange":
                    {
                        return "crowbar";
                    }
                case "taschenlampe":
                    {
                        return "flashlight";
                    }
                case "golfschläger":
                    {
                        return "golfclub";
                    }
                case "axt":
                    {
                        return "hatchet";
                    }
                case "schlagring":
                    {
                        return "knuckle";
                    }
                case "messer":
                    {
                        return "knife";
                    }
                case "machete":
                    {
                        return "machete";
                    }
                case "klappmesser":
                    {
                        return "switchblade";
                    }
                case "schlagstock":
                    {
                        return "nightstick";
                    }
                case "poolcue":
                    {
                        return "poolcue";
                    }
                case "micro-smg":
                    {
                        return "microsmg";
                    }
                case "mp5":
                    {
                        return "smg";
                    }
                case "mp5-mk2":
                    {
                        return "smg-mk2";
                    }
                case "assault-smg":
                    {
                        return "assaultsmg";
                    }
                case "combat-pdw":
                    {
                        return "combatpdw";
                    }
                case "tec9":
                    {
                        return "machinepistol";
                    }
                case "mini-smg":
                    {
                        return "minismg";
                    }
                case "pumpshotgun":
                    {
                        return "pumpshotgun";
                    }
                case "pumpshotgun-mk2":
                    {
                        return "pumpshotgun-mk2";
                    }
                case "sawnoffshotgun":
                    {
                        return "sawnoffshotgun";
                    }
                case "combatshotgun":
                    {
                        return "comatshotgun";
                    }
                case "musket":
                    {
                        return "musket";
                    }
                case "angriffsgewehr":
                    {
                        return "assaultrifle";
                    }
                case "angriffsgewehr-mk2":
                    {
                        return "assaultrifle_mk2";
                    }
                case "karabiner-gewehr":
                    {
                        return "carbinerifle";
                    }
                case "karabiner-gewehr-mk2":
                    {
                        return "carbinerifle_mk2";
                    }
                case "spezial-karabiner":
                    {
                        return "specialcarbine";
                    }
                case "spezial-karabiner-mk2":
                    {
                        return "specialcarbine_mk2";
                    }
                case "m16":
                    {
                        return "tacticalrifle";
                    }
                case "kompaktgewehr":
                    {
                        return "compactrifle";
                    }
                case "maschinengewehr":
                    {
                        return "mg";
                    }
                case "gusenberg":
                    {
                        return "gusenberg";
                    }
                case "sniper":
                    {
                        return "sniperrifle";
                    }
                case "rpg":
                    {
                        return "rpg";
                    }
                case "granate":
                    {
                        return "grenade";
                    }
                case "snowball":
                    {
                        return "snowball";
                    }
                case "bzgas":
                    {
                        return "bzgas";
                    }
                case "molotowcocktail":
                    {
                        return "molotov";
                    }
                case "rauchgranate":
                    {
                        return "smokegrenade";
                    }
                case "feuerlöscher":
                    {
                        return "fireextinguisher";
                    }
            }
            return "unarmed";
        }

        public static string GetWeaponClass(string weaponName)
        {
            switch (weaponName.ToLower())
            {
                case "faust":
                    {
                        return "-1";
                    }
                case "pistole":
                    {
                        return "0";
                    }
                case "pistole-mk2":
                    {
                        return "0";
                    }
                case "pistole-50":
                    {
                        return "0";
                    }
                case "combat-pistole":
                    {
                        return "0";
                    }
                case "heavy-pistole":
                    {
                        return "0";
                    }
                case "keramik-pistole":
                    {
                        return "0";
                    }
                case "flaregun":
                    {
                        return "1";
                    }
                case "revolver":
                    {
                        return "0";
                    }
                case "revolver-mk2":
                    {
                        return "0";
                    }
                case "sns-pistole":
                    {
                        return "0";
                    }
                case "sns-pistole-mk2":
                    {
                        return "0";
                    }
                case "taser":
                    {
                        return "1";
                    }
                case "dolch":
                    {
                        return "2";
                    }
                case "baseballschläger":
                    {
                        return "2";
                    }
                case "brechstange":
                    {
                        return "2";
                    }
                case "taschenlampe":
                    {
                        return "2";
                    }
                case "golfschläger":
                    {
                        return "2";
                    }
                case "axt":
                    {
                        return "2";
                    }
                case "schlagring":
                    {
                        return "2";
                    }
                case "messer":
                    {
                        return "2";
                    }
                case "machete":
                    {
                        return "2";
                    }
                case "klappmesser":
                    {
                        return "2";
                    }
                case "schlagstock":
                    {
                        return "2";
                    }
                case "poolcue":
                    {
                        return "2";
                    }
                case "feuerlöscher":
                    {
                        return "2";
                    }
                case "micro-smg":
                    {
                        return "3";
                    }
                case "mp5":
                    {
                        return "3";
                    }
                case "mp5-mk2":
                    {
                        return "3";
                    }
                case "assault-smg":
                    {
                        return "3";
                    }
                case "combat-pdw":
                    {
                        return "3";
                    }
                case "tec9":
                    {
                        return "3";
                    }
                case "mini-smg":
                    {
                        return "3";
                    }
                case "pumpshotgun":
                    {
                        return "4";
                    }
                case "pumpshotgun-mk2":
                    {
                        return "4";
                    }
                case "sawnoffshotgun":
                    {
                        return "4";
                    }
                case "combatshotgun":
                    {
                        return "4";
                    }
                case "musket":
                    {
                        return "6";
                    }
                case "angriffsgewehr":
                    {
                        return "5";
                    }
                case "angriffsgewehr-mk2":
                    {
                        return "5";
                    }
                case "karabiner-gewehr":
                    {
                        return "5";
                    }
                case "karabiner-gewehr-mk2":
                    {
                        return "5";
                    }
                case "spezial-karabiner":
                    {
                        return "5";
                    }
                case "spezial-karabiner-mk2":
                    {
                        return "5";
                    }
                case "m16":
                    {
                        return "5";
                    }
                case "kompaktgewehr":
                    {
                        return "5";
                    }
                case "maschinengewehr":
                    {
                        return "6";
                    }
                case "gusenberg":
                    {
                        return "6";
                    }
                case "sniper":
                    {
                        return "7";
                    }
                case "rpg":
                    {
                        return "8";
                    }
                case "granate":
                    {
                        return "9";
                    }
                case "snowball":
                    {
                        return "9";
                    }
                case "bzgas":
                    {
                        return "9";
                    }
                case "molotowcocktail":
                    {
                        return "9";
                    }
                case "rauchgranate":
                    {
                        return "9";
                    }
                case "kleine-schutzweste":
                    {
                        return "10";
                    }
                case "schutzweste":
                    {
                        return "10";
                    }
                case "große-schutzweste":
                    {
                        return "10";
                    }
            }
            return "-1";
        }
    }
}
