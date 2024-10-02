using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTANetworkAPI;
using MySqlConnector;
using NemesusWorld.Database;
using NemesusWorld.Models;
using NemesusWorld.Utils;
using Newtonsoft.Json.Linq;

namespace NemesusWorld.Controllers
{
    class GroupsController : Script
    {
        public static List<Groups> groupList = new List<Groups>();
        public static List<GroupsRangs> groupRangList = new List<GroupsRangs>();
        public static List<Services> servicesList = new List<Services>();

        [RemoteEvent("Server:LoadService")]
        public static void OnLoadService(Player player)
        {
            try
            {
                List<Services> tempServiceList = new List<Services>();
                Groups mygroup = null;
                foreach (Services service in servicesList)
                {
                    mygroup = GetGroupById(service.groupid);
                    if (mygroup != null)
                    {
                        service.name = mygroup.name;
                        service.number = mygroup.number;
                        tempServiceList.Add(service);
                    }
                    else
                    {
                        PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                        db.Delete(service);
                    }
                }
                player.TriggerEvent("Client:ShowService", NAPI.Util.ToJson(tempServiceList));
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnLoadService]: " + e.ToString());
            }
        }

        public static void GetAllServices()
        {
            try
            {
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                foreach (Services group in db.Fetch<Services>("SELECT * FROM services"))
                {
                    servicesList.Add(group);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetAllServices]: " + e.ToString());
            }
        }

        public static void GetAllGroups()
        {
            try
            {
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                foreach (Groups group in db.Fetch<Groups>("SELECT * FROM groups"))
                {
                    groupList.Add(group);
                }
                GetAllGroupsRangs();
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetAllGroups]: " + e.ToString());
            }
        }

        public static List<Groups> GetAllGroupsByCharacterId(int characterid)
        {
            try
            {
                List<Groups> tempGroupList = new List<Groups>();
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                foreach (Groups group in db.Fetch<Groups>("SELECT * FROM groups WHERE id IN (SELECT groupsid FROM groups_members WHERE charid = @0) ORDER BY groups.id ASC", characterid))
                {
                    tempGroupList.Add(group);
                }
                return tempGroupList;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetAllGroupsByCharacterId]: " + e.ToString());
            }
            return null;
        }

        public static void SaveGroupMember(GroupsMembers groupMember)
        {
            try
            {
                if (groupMember != null)
                {
                    PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                    db.Save(groupMember);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[SaveGroupMember]: " + e.ToString());
            }
        }

        public static GroupsMembers GetGroupMemberById(int charid, int groupid)
        {
            try
            {
                if (charid <= 0 || groupid == -1) return null;
                GroupsMembers groupMember = new GroupsMembers();
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                groupMember = db.Single<GroupsMembers>("SELECT * FROM groups_members WHERE charid = @0 AND groupsid = @1 LIMIT 1", charid, groupid);
                if (groupMember == null)
                {
                    return null;
                }
                return groupMember;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetGroupMemberById]: " + e.ToString());
            }
            return null;
        }

        public static List<GroupsMembers> GetGroupMembers(int groupid)
        {
            try
            {
                List<GroupsMembers> groupListMember = new List<GroupsMembers>();
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                foreach (GroupsMembers groupMember in db.Fetch<GroupsMembers>("SELECT * FROM groups_members WHERE groupsid = @0 ORDER BY rang desc", groupid))
                {
                    MySqlCommand command = General.Connection.CreateCommand();
                    command.CommandText = "SELECT name,online from characters WHERE id=@id LIMIT 1";
                    command.Parameters.AddWithValue("@id", groupMember.charid);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            groupMember.name = reader.GetString("name");
                            groupMember.online = reader.GetInt16("online");
                            reader.Close();
                        }
                    }
                    groupListMember.Add(groupMember);
                }
                return groupListMember;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetGroupMembers]: " + e.ToString());
            }
            return null;
        }

        public static void SaveGroup(Groups group)
        {
            try
            {
                if (group == null) return;
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                db.Save(group);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[SaveGroup]: " + e.ToString());
            }
        }

        public static Groups GetGroupById(int groupid)
        {
            try
            {
                foreach (Groups group in groupList)
                {
                    if (group != null && group.id == groupid)
                    {
                        return group;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetGroupById]: " + e.ToString());
            }
            return null;
        }

        public static Groups GetGroupByName(string groupname)
        {
            try
            {
                foreach (Groups group in groupList)
                {
                    if (group != null && group.name.ToLower() == groupname.ToLower())
                    {
                        return group;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetGroupByName]: " + e.ToString());
            }
            return null;
        }

        public static Groups GetGroupByBankNumber(string banknumber)
        {
            try
            {
                foreach (Groups group in groupList)
                {
                    if (group != null && group.banknumber.ToLower() == banknumber.ToLower())
                    {
                        return group;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetGroupByBankNumber]: " + e.ToString());
            }
            return null;
        }

        public static string GetGroupNameById(int groupId)
        {
            try
            {
                foreach (Groups group in groupList)
                {
                    if (group != null && group.id == groupId)
                    {
                        return group.name;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetGroupNameById]: " + e.ToString());
            }
            return "n/A";
        }

        public static GroupsRangs GetGroupRangsByID(int groupid)
        {
            try
            {
                foreach (GroupsRangs grouprangs in groupRangList)
                {
                    if (grouprangs != null && grouprangs.groupsid == groupid)
                    {
                        return grouprangs;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetGroupRangsByID]: " + e.ToString());
            }
            return null;
        }

        public static void GetAllGroupsRangs()
        {
            try
            {
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                foreach (GroupsRangs groupr in db.Fetch<GroupsRangs>("SELECT * FROM groupsrangs"))
                {
                    groupRangList.Add(groupr);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetAllGroupsRangs]: " + e.ToString());
            }
        }

        public static int GetGroupRangByCharacterID(int id, int groupid)
        {
            int rang = 1;
            try
            {
                MySqlCommand command = General.Connection.CreateCommand();
                command.CommandText = "SELECT rang from groups_members WHERE charid=@charid AND groupsid=@groupsid LIMIT 1";
                command.Parameters.AddWithValue("@charid", id);
                command.Parameters.AddWithValue("@groupsid", groupid);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        rang = reader.GetInt16("rang");
                        reader.Close();
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetGroupRangByCharacterID]: " + e);
            }
            return rang;
        }

        [RemoteEvent("Server:StartGroup")]
        public static void OnStartGroup(Player player)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                Account account = Helper.GetAccountData(player);

                if (character == null || character.mygroup == -1) return;

                Groups group = GetGroupById(character.mygroup);

                if (group == null) return;

                string status = (group.status == 0) ? "Gruppierung" : "Firma";
                string leadername = "n/A";

                leadername = Helper.GetCharacterName(group.leader);

                MySqlCommand command = General.Connection.CreateCommand();
                command.CommandText = "SELECT COUNT(*) FROM vehicles WHERE owner=@owner";
                command.Parameters.AddWithValue("@owner", "group-" + group.id);
                Int64 count2 = (Int64)command.ExecuteScalar();

                List<GroupsMembers> groupListMember = GetGroupMembers(group.id);

                player.TriggerEvent("Client:ShowGroupStats", NAPI.Util.ToJson(group), NAPI.Util.ToJson(groupListMember), groupListMember.Count, count2, leadername, status, NAPI.Util.ToJson(GetGroupRangsByID(group.id)), character.id);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[StartGroup]: " + e);
            }
        }

        public static void UpdateGroupStats(Player player)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                Account account = Helper.GetAccountData(player);

                if (character == null || character.mygroup == -1) return;

                Groups group = GetGroupById(character.mygroup);

                if (group == null) return;

                string status = (group.status == 0) ? "Gruppierung" : "Firma";
                string leadername = "n/A";

                leadername = Helper.GetCharacterName(group.leader);

                MySqlCommand command = General.Connection.CreateCommand();
                command.CommandText = "SELECT COUNT(*) FROM vehicles WHERE owner=@owner";
                command.Parameters.AddWithValue("@owner", "group-" + group.id);
                Int64 count2 = (Int64)command.ExecuteScalar();

                List<GroupsMembers> groupListMember = GetGroupMembers(group.id);

                player.TriggerEvent("Client:UpdateGroupStats", NAPI.Util.ToJson(group), NAPI.Util.ToJson(groupListMember), groupListMember.Count, count2, leadername, status, NAPI.Util.ToJson(GetGroupRangsByID(group.id)), character.id);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[UpdateGroupStats]: " + e);
            }
        }

        [RemoteEvent("Server:GroupSettings")]
        public static void OnGroupSettings(Player player, string settings, string json)
        {
            try
            {
                Account account = Helper.GetAccountData(player);
                Character character = Helper.GetCharacterData(player);
                TempData tempData = Helper.GetCharacterTempData(player);
                switch (settings.ToLower())
                {
                    case "getlic":
                        {
                            if (character.mygroup == -1)
                            {
                                player.TriggerEvent("Client:ShowStadthalle");
                                Helper.SendNotificationWithoutButton(player, "Du befindest dich in keiner Gruppierung!", "error", "top-left");
                                return;
                            }
                            Groups mygroupCheck = GetGroupById(character.mygroup);
                            if(mygroupCheck == null)
                            {
                                Helper.SendNotificationWithoutButton(player, "Ungültige Gruppierung!", "error", "top-left");
                                return;
                            }
                            if (mygroupCheck.status != 1)
                            {
                                Helper.SendNotificationWithoutButton(player, "Deine Gruppierung ist keine Firma!", "error", "top-left");
                                return;
                            }
                            int number = Convert.ToInt32(json);
                            if (number == 1)
                            {
                                Groups mygroup = GetGroupById(character.mygroup);
                                GroupsMembers groupMember1 = GetGroupMemberById(character.id, mygroup.id);
                                if (groupMember1 != null)
                                {
                                    if (groupMember1.rang >= 10 || character.id == mygroup.leader)
                                    {
                                        string[] licArray = new string[9];
                                        licArray = mygroup.licenses.Split("|");
                                        if (Convert.ToInt32(licArray[0]) == 1)
                                        {
                                            Helper.SendNotificationWithoutButton2(player, "Deine Firma hat schon eine Speditionslizenz!", "error", "center");
                                            return;
                                        }
                                        if (Convert.ToInt32(licArray[1]) == 1)
                                        {
                                            Helper.SendNotificationWithoutButton2(player, "Du hast bereits eine Tuninglizenz, du kannst keine Speditionslizenz mehr besitzen!", "error", "center");
                                            return;
                                        }
                                        if (Convert.ToInt32(licArray[3]) == 1)
                                        {
                                            Helper.SendNotificationWithoutButton2(player, "Du hast bereits eine Personenbeförderungslizenz, du kannst keine Speditionslizenz mehr besitzen!", "error", "center");
                                            return;
                                        }
                                        if (Convert.ToInt32(licArray[4]) == 1)
                                        {
                                            Helper.SendNotificationWithoutButton2(player, "Du hast bereits eine Sicherheitslizenz, du kannst keine Speditionslizenz mehr besitzen!", "error", "center");
                                            return;
                                        }
                                        if (character.cash < Convert.ToInt32(Helper.adminSettings.grouparray[9]))
                                        {
                                            Helper.SendNotificationWithoutButton(player, $"Du hast nicht genügend Geld dabei - {Convert.ToInt32(Helper.adminSettings.grouparray[9])}$!", "error", "top-left");
                                            return;
                                        }
                                        licArray[0] = "" + 1;
                                        mygroup.licenses = $"{licArray[0]}|{licArray[1]}|{licArray[2]}|{licArray[3]}|{licArray[4]}|{licArray[5]}|{licArray[6]}|{licArray[7]}|{licArray[8]}";
                                        CharacterController.SetMoney(player, -Convert.ToInt32(Helper.adminSettings.grouparray[9]));
                                        Helper.SetGovMoney(Convert.ToInt32(Helper.adminSettings.grouparray[9]), "Lizenzverkauf");
                                        player.TriggerEvent("Client:HideStadthalle");
                                        CharacterController.SaveCharacter(player);
                                        GroupsController.SaveGroup(mygroup);
                                        Helper.SendNotificationWithoutButton2(player, "Speditionslizenz erfolgreich erworben, alle weiteren Infos findest du im F2 Menü!", "success", "center", 5000);
                                    }
                                    else
                                    {
                                        Helper.SendNotificationWithoutButton2(player, "Keine Berechtigung!", "error", "center");
                                    }
                                }
                                else
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Ungültige Interaktion!", "error", "center");
                                }
                                break;
                            }
                            if (number == 2)
                            {
                                Groups mygroup = GetGroupById(character.mygroup);
                                GroupsMembers groupMember1 = GetGroupMemberById(character.id, mygroup.id);
                                if (groupMember1 != null)
                                {
                                    if (groupMember1.rang >= 10 || character.id == mygroup.leader)
                                    {
                                        string[] licArray = new string[9];
                                        licArray = mygroup.licenses.Split("|");
                                        if (Convert.ToInt32(licArray[1]) == 1)
                                        {
                                            Helper.SendNotificationWithoutButton2(player, "Deine Firma hat schon eine Tuninglizenz!", "error", "center");
                                            return;
                                        }
                                        if (Convert.ToInt32(licArray[0]) == 1)
                                        {
                                            Helper.SendNotificationWithoutButton2(player, "Du hast bereits eine Speditionslizenz, du kannst keine Tuninglizenz mehr besitzen!", "error", "center");
                                            return;
                                        }
                                        if (Convert.ToInt32(licArray[3]) == 1)
                                        {
                                            Helper.SendNotificationWithoutButton2(player, "Du hast bereits eine Personenbeförderungslizenz, du kannst keine Tuninglizenz mehr besitzen!", "error", "center");
                                            return;
                                        }
                                        if (Convert.ToInt32(licArray[4]) == 1)
                                        {
                                            Helper.SendNotificationWithoutButton2(player, "Du hast bereits eine Sicherheitslizenz, du kannst keine Tuninglizenz mehr besitzen!", "error", "center");
                                            return;
                                        }
                                        if (character.cash < Convert.ToInt32(Helper.adminSettings.grouparray[10]))
                                        {
                                            Helper.SendNotificationWithoutButton(player, $"Du hast nicht genügend Geld dabei - {Convert.ToInt32(Helper.adminSettings.grouparray[10])}$!", "error", "top-left");
                                            return;
                                        }
                                        licArray[1] = "" + 1;
                                        mygroup.licenses = $"{licArray[0]}|{licArray[1]}|{licArray[2]}|{licArray[3]}|{licArray[4]}|{licArray[5]}|{licArray[6]}|{licArray[7]}|{licArray[8]}";
                                        CharacterController.SetMoney(player, -Convert.ToInt32(Helper.adminSettings.grouparray[10]));
                                        Helper.SetGovMoney(Convert.ToInt32(Helper.adminSettings.grouparray[10]), "Lizenzverkauf");
                                        player.TriggerEvent("Client:HideStadthalle");
                                        CharacterController.SaveCharacter(player);
                                        GroupsController.SaveGroup(mygroup);
                                        Helper.SendNotificationWithoutButton2(player, "Tuninglizenz erfolgreich erworben, alle weiteren Infos findest du im F2 Menü!", "success", "center", 5000);
                                    }
                                    else
                                    {
                                        Helper.SendNotificationWithoutButton2(player, "Keine Berechtigung!", "error", "center");
                                    }
                                }
                            }
                            if (number == 3)
                            {
                                Groups mygroup = GetGroupById(character.mygroup);
                                GroupsMembers groupMember1 = GetGroupMemberById(character.id, mygroup.id);
                                if (groupMember1 != null)
                                {
                                    if (groupMember1.rang >= 10 || character.id == mygroup.leader)
                                    {
                                        string[] licArray = new string[9];
                                        licArray = mygroup.licenses.Split("|");
                                        if (Convert.ToInt32(licArray[2]) == 1)
                                        {
                                            Helper.SendNotificationWithoutButton2(player, "Deine Firma hat schon eine Mechatronikerlizenz!", "error", "center");
                                            return;
                                        }
                                        if (Convert.ToInt32(licArray[3]) == 1)
                                        {
                                            Helper.SendNotificationWithoutButton2(player, "Du hast bereits eine Personenbeförderungslizenz, du kannst keine Mechatronikerlizenz mehr besitzen!", "error", "center");
                                            return;
                                        }
                                        if (Convert.ToInt32(licArray[4]) == 1)
                                        {
                                            Helper.SendNotificationWithoutButton2(player, "Du hast bereits eine Sicherheitslizenz, du kannst keine Mechatronikerlizenz mehr besitzen!", "error", "center");
                                            return;
                                        }
                                        if (character.cash < Convert.ToInt32(Helper.adminSettings.grouparray[11]))
                                        {
                                            Helper.SendNotificationWithoutButton(player, $"Du hast nicht genügend Geld dabei - {Convert.ToInt32(Helper.adminSettings.grouparray[11])}$!", "error", "top-left");
                                            return;
                                        }
                                        licArray[2] = "" + 1;
                                        mygroup.licenses = $"{licArray[0]}|{licArray[1]}|{licArray[2]}|{licArray[3]}|{licArray[4]}|{licArray[5]}|{licArray[6]}|{licArray[7]}|{licArray[8]}";
                                        CharacterController.SetMoney(player, -Convert.ToInt32(Helper.adminSettings.grouparray[11]));
                                        Helper.SetGovMoney(Convert.ToInt32(Helper.adminSettings.grouparray[11]), "Lizenzverkauf");
                                        player.TriggerEvent("Client:HideStadthalle");
                                        CharacterController.SaveCharacter(player);
                                        GroupsController.SaveGroup(mygroup);
                                        Helper.SendNotificationWithoutButton2(player, "Mechatronikerlizenz erfolgreich erworben, alle weiteren Infos findest du im F2 Menü!", "success", "center", 5000);
                                    }
                                    else
                                    {
                                        Helper.SendNotificationWithoutButton2(player, "Keine Berechtigung!", "error", "center");
                                    }
                                }
                                else
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Ungültige Interaktion!", "error", "center");
                                }
                                break;
                            }
                            if (number == 4)
                            {
                                Groups mygroup = GetGroupById(character.mygroup);
                                GroupsMembers groupMember1 = GetGroupMemberById(character.id, mygroup.id);
                                if (groupMember1 != null)
                                {
                                    if (groupMember1.rang >= 10 || character.id == mygroup.leader)
                                    {
                                        string[] licArray = new string[9];
                                        licArray = mygroup.licenses.Split("|");
                                        if (Convert.ToInt32(licArray[3]) == 1)
                                        {
                                            Helper.SendNotificationWithoutButton2(player, "Deine Firma hat schon eine Personenbeförderungslizenz!", "error", "center");
                                            return;
                                        }
                                        if (Convert.ToInt32(licArray[0]) == 1)
                                        {
                                            Helper.SendNotificationWithoutButton2(player, "Du hast bereits eine Speditionslizenz, du kannst keine Personenbeförderungslizenz mehr besitzen!", "error", "center");
                                            return;
                                        }
                                        if (Convert.ToInt32(licArray[1]) == 1)
                                        {
                                            Helper.SendNotificationWithoutButton2(player, "Du hast bereits eine Tuninglizenz, du kannst keine Personenbeförderungslizenz mehr besitzen!", "error", "center");
                                            return;
                                        }
                                        if (Convert.ToInt32(licArray[2]) == 1)
                                        {
                                            Helper.SendNotificationWithoutButton2(player, "Du hast bereits eine Mechatronikerlizenz, du kannst keine Personenbeförderungslizenz mehr besitzen!", "error", "center");
                                            return;
                                        }
                                        if (Convert.ToInt32(licArray[4]) == 1)
                                        {
                                            Helper.SendNotificationWithoutButton2(player, "Du hast bereits eine Sicherheitslizenz, du kannst keine Personenbeförderungslizenz mehr besitzen!", "error", "center");
                                            return;
                                        }
                                        if (character.cash < Convert.ToInt32(Helper.adminSettings.grouparray[12]))
                                        {
                                            Helper.SendNotificationWithoutButton(player, $"Du hast nicht genügend Geld dabei - {Convert.ToInt32(Helper.adminSettings.grouparray[12])}$!", "error", "top-left");
                                            return;
                                        }
                                        licArray[3] = "" + 1;
                                        mygroup.licenses = $"{licArray[0]}|{licArray[1]}|{licArray[2]}|{licArray[3]}|{licArray[4]}|{licArray[5]}|{licArray[6]}|{licArray[7]}|{licArray[8]}";
                                        CharacterController.SetMoney(player, -Convert.ToInt32(Helper.adminSettings.grouparray[12]));
                                        Helper.SetGovMoney(Convert.ToInt32(Helper.adminSettings.grouparray[12]), "Lizenzverkauf");
                                        player.TriggerEvent("Client:HideStadthalle");
                                        CharacterController.SaveCharacter(player);
                                        GroupsController.SaveGroup(mygroup);
                                        Helper.SendNotificationWithoutButton2(player, "Personenbeförderungslizenz erfolgreich erworben, alle weiteren Infos findest du im F2 Menü!", "success", "center", 5000);
                                    }
                                    else
                                    {
                                        Helper.SendNotificationWithoutButton2(player, "Keine Berechtigung!", "error", "center");
                                    }
                                }
                                else
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Ungültige Interaktion!", "error", "center");
                                }
                                break;
                            }
                            if (number == 5)
                            {
                                Groups mygroup = GetGroupById(character.mygroup);
                                GroupsMembers groupMember1 = GetGroupMemberById(character.id, mygroup.id);
                                if (groupMember1 != null)
                                {
                                    if (groupMember1.rang >= 10 || character.id == mygroup.leader)
                                    {
                                        string[] licArray = new string[9];
                                        licArray = mygroup.licenses.Split("|");
                                        if (Convert.ToInt32(licArray[4]) == 1)
                                        {
                                            Helper.SendNotificationWithoutButton2(player, "Deine Firma hat schon eine Sicherheitslizenz!", "error", "center");
                                            return;
                                        }
                                        if (Convert.ToInt32(licArray[0]) == 1)
                                        {
                                            Helper.SendNotificationWithoutButton2(player, "Du hast bereits eine Speditionslizenz, du kannst keine Sicherheitslizenz mehr besitzen!", "error", "center");
                                            return;
                                        }
                                        if (Convert.ToInt32(licArray[1]) == 1)
                                        {
                                            Helper.SendNotificationWithoutButton2(player, "Du hast bereits eine Tuninglizenz, du kannst keine Sicherheitslizenz mehr besitzen!", "error", "center");
                                            return;
                                        }
                                        if (Convert.ToInt32(licArray[2]) == 1)
                                        {
                                            Helper.SendNotificationWithoutButton2(player, "Du hast bereits eine Mechatronikerlizenz, du kannst keine Sicherheitslizenz mehr besitzen!", "error", "center");
                                            return;
                                        }
                                        if (character.cash < Convert.ToInt32(Helper.adminSettings.grouparray[13]))
                                        {
                                            Helper.SendNotificationWithoutButton(player, $"Du hast nicht genügend Geld dabei - {Convert.ToInt32(Helper.adminSettings.grouparray[13])}$!", "error", "top-left");
                                            return;
                                        }
                                        licArray[4] = "" + 1;
                                        mygroup.licenses = $"{licArray[0]}|{licArray[1]}|{licArray[2]}|{licArray[3]}|{licArray[4]}|{licArray[5]}|{licArray[6]}|{licArray[7]}|{licArray[8]}";
                                        CharacterController.SetMoney(player, -Convert.ToInt32(Helper.adminSettings.grouparray[13]));
                                        Helper.SetGovMoney(Convert.ToInt32(Helper.adminSettings.grouparray[13]), "Lizenzverkauf");
                                        player.TriggerEvent("Client:HideStadthalle");
                                        CharacterController.SaveCharacter(player);
                                        GroupsController.SaveGroup(mygroup);
                                        Helper.SendNotificationWithoutButton2(player, "Sicherheitslizenz erfolgreich erworben, alle weiteren Infos findest du im F2 Menü!", "success", "center", 5000);
                                    }
                                    else
                                    {
                                        Helper.SendNotificationWithoutButton2(player, "Keine Berechtigung!", "error", "center");
                                    }
                                }
                                else
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Ungültige Interaktion!", "error", "center");
                                }
                                break;
                            }
                            break;
                        }
                    case "switchgroup":
                        {
                            if (character == null) return;
                            int number = Convert.ToInt32(json);
                            if (number != -1)
                            {
                                if (character.job == 1 && tempData.jobduty == true)
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Du kannst deine Gruppierung jetzt nichts wechseln!", "error", "center");
                                    return;
                                }
                                if (tempData.order != null)
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Du kannst deine Gruppierung jetzt nichts wechseln!", "error", "center");
                                    return;
                                }
                                Groups group = GroupsController.GetGroupById(number);
                                if (group != null)
                                {
                                    if (player.HasData("Player:Fare"))
                                    {
                                        player.ResetData("Player:Fare");
                                        player.ResetData("Player:Taxameter");
                                        player.ResetData("Player:TaxameterOn");
                                        if (player.Vehicle.HasSharedData("Vehicle:Text3D"))
                                        {
                                            player.Vehicle.ResetSharedData("Vehicle:Text3D");
                                        }
                                    }
                                    if (player.HasData("Player:GarbageRoute"))
                                    {
                                        player.ResetData("Player:GarbageRoute");
                                        player.ResetData("Player:GarbageStation");
                                        player.ResetData("Player:GarbageTime");
                                        if (player.HasData("Player:GarbagePlayer2"))
                                        {
                                            player.ResetData("Player:GarbagePlayer2");
                                        }
                                        player.TriggerEvent("Client:RemoveGarbageDriverCP");
                                        if (player.HasData("Player:GarbageGetPlayer") && player.GetData<Player>("Player:GarbageGetPlayer") != null)
                                        {
                                            Player garbagePlayer = player.GetData<Player>("Player:GarbageGetPlayer");
                                            garbagePlayer.ResetData("Player:GarbageRoute");
                                            if (garbagePlayer.HasData("Player:GarbagePlayer2"))
                                            {
                                                garbagePlayer.ResetData("Player:GarbagePlayer2");
                                            }
                                            garbagePlayer.ResetData("Player:GarbageStation");
                                            garbagePlayer.ResetData("Player:GarbageTime");
                                            garbagePlayer.TriggerEvent("Client:RemoveGarbageDriverCP");
                                            player.ResetData("Player:GarbageGetPlayer");
                                            garbagePlayer.ResetData("Player:GarbageGetPlayer");
                                        }
                                    }
                                    if (tempData.jobColshape != null && tempData.order != null)
                                    {
                                        if (tempData.jobColshape != null)
                                        {
                                            player.TriggerEvent("Client:DeleteMarker");
                                            tempData.jobColshape.Delete();
                                            tempData.jobColshape = null;
                                            tempData.jobstatus = 0;
                                            tempData.order = null;
                                            player.TriggerEvent("Client:RemoveWaypoint");
                                            return;
                                        }
                                    }
                                    character.mygroup = number;
                                    player.SetOwnSharedData("Player:Group", group.id);
                                    player.SetOwnSharedData("Player:GroupRang", GroupsController.GetGroupRangByCharacterID(character.id, group.id));
                                    GroupsController.UpdateGroupStats(player);
                                    Helper.SendNotificationWithoutButton2(player, "Gruppierung gewechselt!", "success", "center");
                                    return;
                                }
                                Helper.SendNotificationWithoutButton2(player, "Ungültige Gruppierung!", "error", "center");
                            }
                            break;
                        }
                    case "uprank":
                        {
                            Groups mygroup = GetGroupById(character.mygroup);
                            GroupsMembers groupMember1 = GetGroupMemberById(character.id, mygroup.id);
                            GroupsMembers groupMember2 = GetGroupMemberById(Convert.ToInt32(json), mygroup.id);
                            int number = Convert.ToInt32(json);
                            if (groupMember1 != null && groupMember2 != null)
                            {
                                if ((groupMember1.rang >= 10 && groupMember1.rang > groupMember2.rang && character.id != mygroup.leader) || character.id == mygroup.leader)
                                {
                                    if (groupMember2.rang < 12)
                                    {
                                        groupMember2.rang++;
                                        SaveGroupMember(groupMember2);

                                        string charactername = Helper.GetCharacterName(number);
                                        Helper.CreateGroupLog(mygroup.id, $"{character.name} hat {charactername} befördert - (Neuer Rang: {groupMember2.rang})!");

                                        UpdateGroupStats(player);

                                        Player newPlayer = Helper.GetPlayerByCharacterId(number);
                                        if (newPlayer != null)
                                        {
                                            newPlayer.SetOwnSharedData("Player:GroupRang", groupMember2.rang);
                                            if (player != newPlayer)
                                            {
                                                newPlayer.TriggerEvent("Client:UpdateGroupRang");
                                                UpdateGroupStats(newPlayer);
                                            }
                                        }

                                        Helper.SendNotificationWithoutButton2(player, "Der Spieler wurde befördert!", "success", "center");
                                    }
                                    else
                                    {
                                        Helper.SendNotificationWithoutButton2(player, "Ungültige Interaktion!", "error", "center");
                                    }
                                }
                                else
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Keine Berechtigung!", "error", "center");
                                }
                            }
                            else
                            {
                                Helper.SendNotificationWithoutButton2(player, "Ungültige Interaktion!", "error", "center");
                            }
                            break;
                        }
                    case "downrank":
                        {
                            Groups mygroup = GetGroupById(character.mygroup);
                            GroupsMembers groupMember1 = GetGroupMemberById(character.id, mygroup.id);
                            GroupsMembers groupMember2 = GetGroupMemberById(Convert.ToInt32(json), mygroup.id);
                            int number = Convert.ToInt32(json);
                            if (groupMember1 != null && groupMember2 != null)
                            {
                                if ((groupMember1.rang >= 10 && groupMember1.rang > groupMember2.rang && character.id != mygroup.leader) || character.id == mygroup.leader)
                                {
                                    if (groupMember2.rang > 1)
                                    {
                                        groupMember2.rang--;
                                        SaveGroupMember(groupMember2);

                                        string charactername = Helper.GetCharacterName(number);
                                        Helper.CreateGroupLog(mygroup.id, $"{character.name} hat {charactername} degradiert - (Neuer Rang: {groupMember2.rang})!");

                                        UpdateGroupStats(player);

                                        Player newPlayer = Helper.GetPlayerByCharacterId(number);
                                        if (newPlayer != null)
                                        {
                                            newPlayer.SetOwnSharedData("Player:GroupRang", groupMember2.rang);
                                            if (player != newPlayer)
                                            {
                                                newPlayer.TriggerEvent("Client:UpdateGroupRang");
                                                UpdateGroupStats(newPlayer);
                                            }
                                        }

                                        Helper.SendNotificationWithoutButton2(player, "Der Spieler wurde degradiert!", "success", "center");
                                    }
                                    else
                                    {
                                        Helper.SendNotificationWithoutButton2(player, "Ungültige Interaktion!", "error", "center");
                                    }
                                }
                                else
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Keine Berechtigung!", "error", "center");
                                }
                            }
                            else
                            {
                                Helper.SendNotificationWithoutButton2(player, "Ungültige Interaktion!", "error", "center");
                            }
                            break;
                        }
                    case "invite":
                        {
                            Groups mygroup = GetGroupById(character.mygroup);
                            GroupsMembers groupMember1 = GetGroupMemberById(character.id, mygroup.id);
                            if (groupMember1 != null)
                            {
                                if (groupMember1.rang >= 10)
                                {

                                    player.TriggerEvent("Client:CallInput", "Gruppierungseinladung", "Wen möchtest du einladen?", "text", "Gruppierungseinladung", 35, "invitegroup");
                                }
                                else
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Keine Berechtigung!", "error", "center");
                                }
                            }
                            else
                            {
                                Helper.SendNotificationWithoutButton2(player, "Ungültige Interaktion!", "error", "center");
                            }
                            break;
                        }
                    case "provision":
                        {
                            Groups mygroup = GetGroupById(character.mygroup);
                            GroupsMembers groupMember1 = GetGroupMemberById(character.id, mygroup.id);
                            int number = Convert.ToInt32(json);
                            if (groupMember1 != null)
                            {
                                if (groupMember1.rang >= 10)
                                {
                                    mygroup.provision = number;                        
                                    SaveGroup(mygroup);

                                    Helper.CreateGroupLog(mygroup.id, $"{character.name} hat die Provision angepasst!");

                                    UpdateGroupStats(player);

                                    Helper.SendNotificationWithoutButton2(player, "Neue Provision eingestellt!", "success", "center");
                                }
                                else
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Keine Berechtigung!", "error", "center");
                                }
                            }
                            else
                            {
                                Helper.SendNotificationWithoutButton2(player, "Ungültige Interaktion!", "error", "center");
                            }
                            break;
                        }
                    case "money":
                        {
                            Groups mygroup = GetGroupById(character.mygroup);
                            GroupsMembers groupMember1 = GetGroupMemberById(character.id, mygroup.id);
                            GroupsMembers groupMember2 = GetGroupMemberById(Convert.ToInt32(json), mygroup.id);
                            int number = Convert.ToInt32(json);
                            if (groupMember1 != null && groupMember2 != null)
                            {
                                if ((groupMember1.rang >= 10 && groupMember1.rang > groupMember2.rang && character.id != mygroup.leader) || character.id == mygroup.leader)
                                {
                                    player.SetData<int>("Player:Tempid", number);
                                    player.TriggerEvent("Client:CallInput", "Lohneinstellung", "Wieviel Geld soll der Spieler bei jedem xten Payday erhalten? Eingabe: 1(Geld), 5(Paydays)", "text", "Lohneinstellung", 35, "moneygroup");
                                }
                                else
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Keine Berechtigung!", "error", "center");
                                }
                            }
                            else
                            {
                                Helper.SendNotificationWithoutButton2(player, "Ungültige Interaktion!", "error", "center");
                            }
                            break;
                        }
                    case "leave":
                        {
                            Groups mygroup = GetGroupById(character.mygroup);
                            GroupsMembers groupMember1 = GetGroupMemberById(character.id, mygroup.id);
                            int number = Convert.ToInt32(json);
                            if (groupMember1 != null)
                            {
                                if (character.id == mygroup.leader)
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Du kannst die Gruppierung nicht verlassen!", "error", "center");
                                    player.TriggerEvent("Client:HideCursor");
                                    player.TriggerEvent("Client:ShowHud");
                                    return;
                                }

                                character.mygroup = -1;

                                Helper.CreateGroupLog(mygroup.id, $"{character.name} hat die Gruppierung verlassen!");

                                Helper.CreateUserTimeline(account.id, character.id, $"Gruppierung {mygroup.name} verlassen", 4);

                                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                                db.Delete(groupMember1);

                                CharacterController.SaveCharacter(player);

                                player.TriggerEvent("Client:HideCursor");
                                player.TriggerEvent("Client:ShowHud");


                                Player newPlayer = Helper.GetPlayerByCharacterId(number);
                                if (newPlayer != null)
                                {
                                    newPlayer.SetOwnSharedData("Player:GroupRang", -1);
                                }

                                Helper.SendNotificationWithoutButton2(player, "Du hast die Gruppierung verlassen!", "success", "center");
                            }
                            else
                            {
                                Helper.SendNotificationWithoutButton2(player, "Ungültige Interaktion!", "error", "center");
                                player.TriggerEvent("Client:HideCursor");
                                player.TriggerEvent("Client:ShowHud");
                            }
                            break;
                        }
                    case "kick":
                        {
                            Groups mygroup = GetGroupById(character.mygroup);
                            GroupsMembers groupMember1 = GetGroupMemberById(character.id, mygroup.id);
                            GroupsMembers groupMember2 = GetGroupMemberById(Convert.ToInt32(json), mygroup.id);
                            int number = Convert.ToInt32(json);
                            if (groupMember1 != null && groupMember2 != null)
                            {
                                if ((groupMember1.rang >= 10 && groupMember1.rang > groupMember2.rang && character.id != mygroup.leader) || character.id == mygroup.leader)
                                {
                                    if (number == mygroup.leader)
                                    {
                                        Helper.SendNotificationWithoutButton2(player, "Der Leader kann nicht rausgeworfen werden!", "error", "center");
                                        return;
                                    }

                                    string charactername = Helper.GetCharacterName(number);

                                    Helper.CreateGroupLog(mygroup.id, $"{character.name} hat {charactername} aus der Gruppierung geworfen!");

                                    MySqlCommand command = General.Connection.CreateCommand();
                                    command.CommandText = "UPDATE characters SET mygroup = -1 WHERE id=@id AND mygroup=@mygroup LIMIT 1";
                                    command.Parameters.AddWithValue("@id", number);
                                    command.Parameters.AddWithValue("@mygroup", mygroup.id);
                                    command.ExecuteNonQuery();

                                    PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                                    db.Delete(groupMember2);

                                    UpdateGroupStats(player);

                                    Player newPlayer = Helper.GetPlayerByCharacterId(number);
                                    if (newPlayer != null)
                                    {
                                        Character pCharacter = Helper.GetCharacterData(newPlayer);
                                        if (pCharacter.id == number && pCharacter.mygroup == mygroup.id)
                                        {
                                            pCharacter.mygroup = -1;
                                        }
                                        newPlayer.SetOwnSharedData("Player:GroupRang", -1);
                                        newPlayer.SetOwnSharedData("Player:Group", -1);
                                        newPlayer.TriggerEvent("Client:UpdateGroupRang");
                                    }
                                    Helper.SendNotificationWithoutButton2(player, "Der Spieler wurde erfolgreich aus der Gruppierung geworfen!", "success", "center");
                                }
                                else
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Keine Berechtigung!", "error", "center");
                                }
                            }
                            else
                            {
                                Helper.SendNotificationWithoutButton2(player, "Ungültige Interaktion!", "error", "center");
                            }
                            break;
                        }
                    case "leader":
                        {
                            Groups mygroup = GetGroupById(character.mygroup);
                            GroupsMembers groupMember1 = GetGroupMemberById(character.id, mygroup.id);
                            GroupsMembers groupMember2 = GetGroupMemberById(Convert.ToInt32(json), mygroup.id);
                            int number = Convert.ToInt32(json);
                            if (groupMember1 != null && groupMember2 != null)
                            {
                                if (character.id == mygroup.leader)
                                {
                                    if (number == mygroup.leader)
                                    {
                                        Helper.SendNotificationWithoutButton2(player, "Der Spieler ist schon Leader der Gruppierung!", "error", "center");
                                        return;
                                    }

                                    string charactername = Helper.GetCharacterName(number);

                                    Helper.CreateGroupLog(mygroup.id, $"{character.name} hat {charactername} zum Leader der Gruppierung gemacht!");

                                    mygroup.leader = number;
                                    groupMember2.rang = 12;
                                    SaveGroupMember(groupMember2);
                                    SaveGroup(mygroup);

                                    UpdateGroupStats(player);

                                    Player newPlayer = Helper.GetPlayerByCharacterId(number);
                                    if (newPlayer != null)
                                    {
                                        newPlayer.SetOwnSharedData("Player:GroupRang", 12);
                                        newPlayer.TriggerEvent("Client:UpdateGroupRang");
                                        if (player != newPlayer)
                                        {
                                            UpdateGroupStats(newPlayer);
                                        }
                                    }
                                    Helper.SendNotificationWithoutButton2(player, "Der Spieler wurde erfolgreich zum Leader der Gruppierung gemacht!", "success", "center");
                                }
                                else
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Keine Berechtigung!", "error", "center");
                                }
                            }
                            else
                            {
                                Helper.SendNotificationWithoutButton2(player, "Ungültige Interaktion!", "error", "center");
                            }
                            break;
                        }
                    case "creategroupbefore":
                        {
                            if (character.cash < 2500)
                            {
                                player.TriggerEvent("Client:ShowStadthalle");
                                Helper.SendNotificationWithoutButton(player, "Du hast nicht genügend Geld dabei - 2500$!", "error", "top-left");
                                return;
                            }
                            player.TriggerEvent("Client:CallInput", "Gruppierungsname", "Wie soll deine Gruppierung heissen?", "text", "Gruppierungsname", 64, "CreateGroup");
                            break;
                        }
                    case "namecompany":
                        {
                            Groups mygroup = GetGroupById(character.mygroup);
                            if (mygroup == null)
                            {
                                player.TriggerEvent("Client:ShowStadthalle");
                                Helper.SendNotificationWithoutButton(player, "Du befindest dich in keiner Gruppierung!", "error", "top-left");
                                return;
                            }
                            GroupsMembers groupMember1 = GetGroupMemberById(character.id, mygroup.id);
                            if (groupMember1 != null)
                            {
                                if (mygroup.leader == character.id)
                                {
                                    if (character.cash < Convert.ToInt32(Helper.adminSettings.grouparray[6]))
                                    {
                                        player.TriggerEvent("Client:ShowStadthalle");
                                        Helper.SendNotificationWithoutButton(player, $"Du hast nicht genügend Geld dabei - {Convert.ToInt32(Helper.adminSettings.grouparray[6])}$!", "error", "top-left");
                                        return;
                                    }
                                    player.TriggerEvent("Client:CallInput", "Gruppierungsname", "Wie soll deine Gruppierung heissen?", "text", "Gruppierungsname", 64, "NameGroup");
                                }
                                else
                                {
                                    player.TriggerEvent("Client:ShowStadthalle");
                                    Helper.SendNotificationWithoutButton2(player, "Keine Berechtigung!", "error", "center");
                                }
                            }
                            else
                            {
                                player.TriggerEvent("Client:ShowStadthalle");
                                Helper.SendNotificationWithoutButton(player, "Ungültige Interaktion!", "error", "top-left");
                            }
                            break;
                        }
                    case "showgrouprangs":
                        {
                            Groups mygroup = GetGroupById(character.mygroup);
                            GroupsMembers groupMember1 = GetGroupMemberById(character.id, mygroup.id);
                            if (groupMember1 != null)
                            {
                                if (groupMember1.rang >= 10)
                                {
                                    player.TriggerEvent("Client:ShowGroupSettings", NAPI.Util.ToJson(GetGroupRangsByID(mygroup.id)), 0, mygroup.provision);
                                }
                                else
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Keine Berechtigung!", "error", "center");
                                }
                            }
                            break;
                        }
                    case "updaterangs":
                        {
                            Groups mygroup = GetGroupById(character.mygroup);
                            GroupsMembers groupMember1 = GetGroupMemberById(character.id, mygroup.id);
                            if (groupMember1 != null)
                            {
                                if (groupMember1.rang >= 10)
                                {
                                    try
                                    {
                                        GroupsRangs grouprangtemp = new GroupsRangs();
                                        GroupsRangs grouprang = new GroupsRangs();
                                        JObject obj = JObject.Parse(json);

                                        if (obj == null)
                                        {
                                            Helper.SendNotificationWithoutButton2(player, "Ungültige Eingabe!", "error", "center");
                                            return;
                                        }

                                        grouprang.groupsid = mygroup.id;
                                        grouprang.rang1 = (string)obj["rang1"];
                                        grouprang.rang2 = (string)obj["rang2"];
                                        grouprang.rang3 = (string)obj["rang3"];
                                        grouprang.rang4 = (string)obj["rang4"];
                                        grouprang.rang5 = (string)obj["rang5"];
                                        grouprang.rang6 = (string)obj["rang6"];
                                        grouprang.rang7 = (string)obj["rang7"];
                                        grouprang.rang8 = (string)obj["rang8"];
                                        grouprang.rang9 = (string)obj["rang9"];
                                        grouprang.rang10 = (string)obj["rang10"];
                                        grouprang.rang11 = (string)obj["rang11"];
                                        grouprang.rang12 = (string)obj["rang12"];

                                        if (grouprang.rang1.Length < 3 || grouprang.rang1.Length > 50 || grouprang.rang2.Length < 3 || grouprang.rang2.Length > 50 || grouprang.rang3.Length < 3 || grouprang.rang3.Length > 50 || grouprang.rang4.Length < 3 || grouprang.rang4.Length > 50
                                        || grouprang.rang5.Length < 3 || grouprang.rang5.Length > 50 || grouprang.rang6.Length < 3 || grouprang.rang6.Length > 50 || grouprang.rang7.Length < 3 || grouprang.rang7.Length > 50 || grouprang.rang8.Length < 3 || grouprang.rang8.Length > 50
                                        || grouprang.rang9.Length < 3 || grouprang.rang9.Length > 50 || grouprang.rang10.Length < 3 || grouprang.rang10.Length > 50 || grouprang.rang11.Length < 3 || grouprang.rang11.Length > 50 || grouprang.rang12.Length < 3 || grouprang.rang12.Length > 50)
                                        {
                                            Helper.SendNotificationWithoutButton2(player, "Ungültige Eingabe!", "error", "center");
                                            return;
                                        }

                                        int tempid = 0;
                                        int groupid = 0;
                                        foreach (GroupsRangs grouprangs in groupRangList)
                                        {
                                            if (grouprangs != null && grouprangs.groupsid == mygroup.id)
                                            {
                                                grouprangtemp = grouprangs;
                                                tempid = grouprangs.id;
                                                groupid = grouprangs.groupsid;
                                                break;
                                            }
                                        }

                                        grouprang.id = tempid;
                                        grouprang.groupsid = groupid;

                                        groupRangList.Remove(grouprangtemp);
                                        grouprangtemp = null;
                                        groupRangList.Add(grouprang);

                                        PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                                        db.Save(grouprang);

                                        Helper.CreateGroupLog(mygroup.id, $"{character.name} hat hat die Rangnamen aktualisiert!");

                                        player.TriggerEvent("Client:ShowGroupSettings", NAPI.Util.ToJson(GetGroupRangsByID(mygroup.id)), 1, mygroup.provision);

                                        Helper.SendNotificationWithoutButton2(player, "Die Rangnamen wurden aktualisiert!", "success", "center");
                                    }
                                    catch (Exception)
                                    {
                                        Helper.SendNotificationWithoutButton2(player, "Ungültige Eingabe!", "error", "center");
                                    }
                                }
                                else
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Keine Berechtigung!", "error", "center");
                                }
                            }
                            break;
                        }
                    case "groupphone":
                        {
                            Groups mygroup = GetGroupById(character.mygroup);
                            GroupsMembers groupMember1 = GetGroupMemberById(character.id, mygroup.id);
                            if (groupMember1 != null)
                            {
                                if (groupMember1.rang >= 1)
                                {
                                    if (character.lastsmartphone == "")
                                    {
                                        Helper.SendNotificationWithoutButton2(player, "Du musst zuerst ein Smartphone ausrüsten!", "error", "center");
                                        return;
                                    }
                                    if (mygroup.number != character.lastsmartphone)
                                    {
                                        mygroup.number = character.lastsmartphone;
                                        mygroup.numbername = character.name;
                                        Helper.SendNotificationWithoutButton2(player, "Du hast den Telefondienst übernommen!", "success", "center");
                                    }
                                    else
                                    {
                                        mygroup.number = "";
                                        mygroup.numbername = "";
                                        Helper.SendNotificationWithoutButton2(player, "Du hast den Telefondienst beendet!", "success", "center");
                                    }
                                    GroupsController.UpdateGroupStats(player);
                                }
                                else
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Keine Berechtigung!", "error", "center");
                                }
                            }
                            break;
                        }
                    case "grouplog1":
                        {
                            Groups mygroup = GetGroupById(character.mygroup);
                            GroupsMembers groupMember1 = GetGroupMemberById(character.id, mygroup.id);
                            if (groupMember1 != null)
                            {
                                if (groupMember1.rang >= 10)
                                {
                                    string loglabel = "group-" + mygroup.id;
                                    player.TriggerEvent("Client:ShowLog", NAPI.Util.ToJson(Helper.GetLogEntries(loglabel)), "Gruppierungslog");
                                }
                                else
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Keine Berechtigung!", "error", "center");
                                }
                            }
                            break;
                        }
                    case "grouplog2":
                        {
                            Groups mygroup = GetGroupById(character.mygroup);
                            GroupsMembers groupMember1 = GetGroupMemberById(character.id, mygroup.id);
                            if (groupMember1 != null)
                            {
                                if (groupMember1.rang >= 10)
                                {
                                    List<Logs> logList = Helper.GetLogEntries("groupmoney-" + mygroup.id);
                                    player.TriggerEvent("Client:ShowLog", NAPI.Util.ToJson(logList), "Wirtschaftslog");
                                }
                                else
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Keine Berechtigung!", "error", "center");
                                }
                            }
                            break;
                        }
                    case "firmacompany":
                        {
                            Groups mygroup = GetGroupById(character.mygroup);
                            GroupsRangs myGroupsRangs = GetGroupRangsByID(character.mygroup);
                            player.TriggerEvent("Client:HideStadthalle");
                            if (mygroup != null)
                            {
                                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                                if (mygroup.status == 0)
                                {
                                    if (character.cash < Convert.ToInt32(Helper.adminSettings.grouparray[7]))
                                    {
                                        player.TriggerEvent("Client:ShowStadthalle");
                                        Helper.SendNotificationWithoutButton(player, $"Du hast nicht genügend Geld dabei - {Convert.ToInt32(Helper.adminSettings.grouparray[7])}$!", "error", "top-left");
                                        return;
                                    }
                                    CharacterController.SetMoney(player, -Convert.ToInt32(Helper.adminSettings.grouparray[7]));
                                    Helper.SetGovMoney(Convert.ToInt32(Helper.adminSettings.grouparray[7]), "Firmenstatus erworben");
                                    mygroup.status = 1;
                                    Helper.SendNotificationWithoutButton2(player, "Der Firmenstatus wurde gesetzt!", "success", "center", 3500);
                                }
                                else
                                {
                                    foreach (Services service in servicesList.ToList())
                                    {
                                        if (service.groupid == mygroup.id)
                                        {
                                            db.Delete(service);
                                            servicesList.Remove(service);
                                            break;
                                        }
                                    }
                                    mygroup.status = 0;
                                    Helper.SendNotificationWithoutButton2(player, "Der Firmenstatus wurde entfernt!", "success", "center", 3500);
                                }
                                SaveGroup(mygroup);
                            }
                            else
                            {
                                player.TriggerEvent("Client:ShowStadthalle");
                                Helper.SendNotificationWithoutButton(player, "Du befindest dich in keiner Gruppierung!", "error", "top-left");
                            }
                            break;
                        }
                    case "firmaservice":
                        {
                            PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                            Groups mygroup = GetGroupById(character.mygroup);
                            if(mygroup == null || character.mygroup == 1)
                            {
                                player.TriggerEvent("Client:ShowStadthalle");
                                Helper.SendNotificationWithoutButton(player, "Du befindest dich in keiner Gruppierung!", "error", "top-left");
                                return;
                            }
                            GroupsRangs myGroupsRangs = GetGroupRangsByID(character.mygroup);
                            GroupsMembers groupMember1 = GetGroupMemberById(character.id, mygroup.id);
                            if (groupMember1 != null)
                            {
                                if (groupMember1.rang >= 10)
                                {
                                    player.TriggerEvent("Client:HideStadthalle");
                                    if (mygroup != null)
                                    {
                                        if (mygroup.service == 0)
                                        {
                                            if (mygroup.status != 1)
                                            {
                                                player.TriggerEvent("Client:ShowStadthalle");
                                                Helper.SendNotificationWithoutButton(player, "Deine Gruppierung ist keine Firma!", "error", "top-left");
                                                return;
                                            }
                                            if (character.cash < Convert.ToInt32(Helper.adminSettings.grouparray[8]))
                                            {
                                                player.TriggerEvent("Client:ShowStadthalle");
                                                Helper.SendNotificationWithoutButton(player, $"Du hast nicht genügend Geld dabei -{Convert.ToInt32(Helper.adminSettings.grouparray[8])}$!", "error", "top-left");
                                                return;
                                            }
                                            player.TriggerEvent("Client:CallInput", "Als Service eintragen", $"Welchen Service bietet deine Firma an?", "text", "64", 64, "ServiceFirma");
                                        }
                                        else
                                        {
                                            foreach (Services service in servicesList.ToList())
                                            {
                                                if (service.groupid == mygroup.id)
                                                {
                                                    db.Delete(service);
                                                    servicesList.Remove(service);
                                                    break;
                                                }
                                            }
                                            mygroup.service = 0;
                                            Helper.SendNotificationWithoutButton2(player, "Firma erfolgreich als Service ausgetragen!", "success", "center", 3500);
                                        }
                                        SaveGroup(mygroup);
                                    }
                                }
                                else
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Keine Berechtigung!", "error", "center");
                                }
                            }
                            else
                            {
                                player.TriggerEvent("Client:ShowStadthalle");
                                Helper.SendNotificationWithoutButton(player, "Du befindest dich in keiner Gruppierung!", "error", "top-left");
                            }
                            break;
                        }
                    case "hidemenu":
                        {
                            player.TriggerEvent("Client:HideStadthalle");
                            break;
                        }
                    case "deletecompany":
                        {
                            if (character.mygroup == -1)
                            {
                                player.TriggerEvent("Client:ShowStadthalle");
                                Helper.SendNotificationWithoutButton(player, "Du befindest dich in keiner Gruppierung!", "error", "top-left");
                                return;
                            }
                            Groups mygroup = GetGroupById(character.mygroup);
                            GroupsRangs myGroupsRangs = GetGroupRangsByID(character.mygroup);
                            if (mygroup != null)
                            {
                                foreach (Invoices invoices in Helper.invoicesList)
                                {
                                    if (invoices.empfänger == mygroup.name)
                                    {
                                        player.TriggerEvent("Client:ShowStadthalle");
                                        Helper.SendNotificationWithoutButton(player, "Es sind noch Rechnungen offen, diese müssen zuerst beglichen werden!", "error", "top-left");
                                        return;
                                    }
                                }
                                if (mygroup.leader == character.id)
                                {
                                    if (mygroup.banknumber != "n/A")
                                    {
                                        player.TriggerEvent("Client:ShowStadthalle");
                                        Helper.SendNotificationWithoutButton(player, "Es muss zuerst das Firmenkonto entfernt werden!", "error", "top-left");
                                        return;
                                    }
                                    MySqlCommand command = General.Connection.CreateCommand();
                                    command.CommandText = "DELETE FROM groupsrangs WHERE groupsid=@groupsid LIMIT 1";
                                    command.Parameters.AddWithValue("@groupsid", mygroup.id);
                                    command.ExecuteNonQuery();

                                    MySqlCommand command2 = General.Connection.CreateCommand();
                                    command2.CommandText = "DELETE FROM groups_members WHERE groupsid=@groupsid LIMIT 1";
                                    command2.Parameters.AddWithValue("@groupsid", mygroup.id);
                                    command2.ExecuteNonQuery();

                                    MySqlCommand command3 = General.Connection.CreateCommand();
                                    command3.CommandText = "DELETE FROM groups WHERE id=@id LIMIT 1";
                                    command3.Parameters.AddWithValue("@id", mygroup.id);
                                    command3.ExecuteNonQuery();

                                    MySqlCommand command4 = General.Connection.CreateCommand();
                                    command4.CommandText = "UPDATE characters SET mygroup = -1 WHERE mygroup=@mygroup";
                                    command4.Parameters.AddWithValue("@mygroup", mygroup.id);
                                    command4.ExecuteNonQuery();

                                    MySqlCommand command5 = General.Connection.CreateCommand();
                                    command5.CommandText = "DELETE FROM logs WHERE loglabel=@loglabel";
                                    command5.Parameters.AddWithValue("@loglabel", "group-" + mygroup.id);
                                    command5.ExecuteNonQuery();

                                    MySqlCommand command6 = General.Connection.CreateCommand();
                                    command6.CommandText = "DELETE FROM logs WHERE loglabel=@loglabel";
                                    command6.Parameters.AddWithValue("@loglabel", "groupmoney-" + mygroup.id);
                                    command6.ExecuteNonQuery();

                                    foreach (Player p in NAPI.Pools.GetAllPlayers())
                                    {
                                        if (p != null && player != p && p.GetOwnSharedData<bool>("Player:Spawned") == true)
                                        {
                                            Character character2 = Helper.GetCharacterData(player);
                                            if (character2 != null && character2.mygroup == mygroup.id)
                                            {
                                                character2.mygroup = -1;
                                            }
                                        }
                                    }

                                    foreach (House house in House.houseList)
                                    {
                                        if (house != null && house.housegroup == mygroup.id)
                                        {
                                            house.housegroup = -1;
                                            house.blip = 40;
                                            house.stock = 0;
                                            house.stockprice = 30;
                                            if (House.GetInteriorClassify(house.interior) >= 4)
                                            {
                                                house.interior = House.GetRandomHouseInterior(house.classify);
                                                foreach (Player p in NAPI.Pools.GetAllPlayers())
                                                {
                                                    if (p != null && p.GetOwnSharedData<bool>("Player:Spawned") == true)
                                                    {
                                                        Character character2 = Helper.GetCharacterData(p);
                                                        if (character2 != null && character2.inhouse == house.id)
                                                        {
                                                            Helper.SetPlayerPosition(p, House.GetHouseExitPoint(house.interior));
                                                        }
                                                    }
                                                }
                                            }
                                            House.SetHouseHandle(house);
                                            House.SaveHouse(house);
                                        }
                                    }

                                    PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                                    foreach (Services service in servicesList.ToList())
                                    {
                                        if (service.groupid == mygroup.id)
                                        {
                                            db.Delete(service);
                                            servicesList.Remove(service);
                                            break;
                                        }
                                    }

                                    Helper.CreateUserTimeline(account.id, character.id, $"Gruppierung {mygroup.name} geschlossen", 4);

                                    groupList.Remove(mygroup);
                                    mygroup = null;
                                    groupRangList.Remove(myGroupsRangs);
                                    myGroupsRangs = null;

                                    character.mygroup = -1;
                                    player.SetOwnSharedData("Player:Group", -1);
                                    player.SetOwnSharedData("Player:GroupRang", -1);
                                    player.TriggerEvent("Client:HideStadthalle");
                                    CharacterController.SaveCharacter(player);

                                    Helper.SendNotificationWithoutButton2(player, "Die Gruppierung wurde geschlossen!", "success", "center", 3500);

                                }
                                else
                                {
                                    player.TriggerEvent("Client:ShowStadthalle");
                                    Helper.SendNotificationWithoutButton(player, "Du bist nicht der Inhaber der Gruppierung!", "error", "top-left");
                                }
                            }
                            else
                            {
                                player.TriggerEvent("Client:ShowStadthalle");
                                Helper.SendNotificationWithoutButton(player, "Ungültige Interaktion!", "error", "top-left");
                            }
                            break;
                        }
                    case "setgroupname":
                        {
                            if (character.cash < Convert.ToInt32(Helper.adminSettings.grouparray[6]))
                            {
                                player.TriggerEvent("Client:ShowStadthalle");
                                Helper.SendNotificationWithoutButton(player, $"Du hast nicht genügend Geld dabei - {Convert.ToInt32(Helper.adminSettings.grouparray[6])}!", "error", "top-left");
                                return;
                            }
                            string groupname = json;
                            if (groupname.Length < 5)
                            {
                                player.TriggerEvent("Client:ShowStadthalle");
                                Helper.SendNotificationWithoutButton(player, "Ungültiger Gruppierungsname!", "error", "top-left");
                                return;
                            }
                            foreach (Groups getgroup in groupList)
                            {
                                if (getgroup != null && getgroup.name.ToLower() == groupname.ToLower())
                                {
                                    player.TriggerEvent("Client:ShowStadthalle");
                                    Helper.SendNotificationWithoutButton(player, "Dieser Gruppierungsname wird bereits verwendet!", "error", "top-left");
                                    return;
                                }
                            }
                            Groups group = GetGroupById(character.mygroup);
                            if (group == null)
                            {
                                player.TriggerEvent("Client:ShowStadthalle");
                                Helper.SendNotificationWithoutButton(player, "Ungültige Interaktion!", "error", "top-left");
                                return;
                            }
                            foreach(Invoices invoices in Helper.invoicesList)
                            {
                                if(invoices.empfänger == group.name)
                                {
                                    player.TriggerEvent("Client:ShowStadthalle");
                                    Helper.SendNotificationWithoutButton(player, "Es sind noch Rechnungen offen, diese müssen zuerst beglichen werden!", "error", "top-left");
                                    return;
                                }
                            }
                            group.name = groupname;
                            CharacterController.SetMoney(player, -Convert.ToInt32(Helper.adminSettings.grouparray[6]));
                            Helper.CreateGroupLog(group.id, $"{character.name} hat den Gruppierungsnamen auf {group.name} geändert!");
                            GroupsController.UpdateGroupStats(player);
                            SaveGroup(group);
                            CharacterController.SaveCharacter(player);
                            Helper.SetGovMoney(Convert.ToInt32(Helper.adminSettings.grouparray[6]), "Gruppierungsname geändert");
                            player.TriggerEvent("Client:HideStadthalle");
                            Helper.SendNotificationWithoutButton2(player, "Der Gruppierungsname wurde geändert!", "success", "center", 4500);
                            break;
                        }
                    case "creategroup":
                        {
                            if (character.cash < Convert.ToInt32(Helper.adminSettings.grouparray[5]))
                            {
                                player.TriggerEvent("Client:ShowStadthalle");
                                Helper.SendNotificationWithoutButton(player, $"Du hast nicht genügend Geld dabei - {Convert.ToInt32(Helper.adminSettings.grouparray[5])}$!", "error", "top-left");
                                return;
                            }
                            string groupname = json;
                            if (groupname.Length < 5)
                            {
                                player.TriggerEvent("Client:ShowStadthalle");
                                Helper.SendNotificationWithoutButton(player, "Ungültiger Gruppierungsname!", "error", "top-left");
                                return;
                            }
                            foreach (Groups group in groupList)
                            {
                                if (group != null && group.name.ToLower() == groupname.ToLower())
                                {
                                    player.TriggerEvent("Client:ShowStadthalle");
                                    Helper.SendNotificationWithoutButton(player, "Dieser Gruppierungsname wird bereits verwendet!", "error", "top-left");
                                    return;
                                }
                            }
                            player.TriggerEvent("Client:HideStadthalle");
                            Groups newgroup = new Groups();
                            newgroup.name = groupname;
                            newgroup.created = Helper.UnixTimestamp();
                            newgroup.banknumber = "n/A";
                            newgroup.leader = character.id;
                            newgroup.members = 1;
                            newgroup.status = 0;
                            newgroup.provision = 0;
                            newgroup.licenses = "0|0|0|0|0|0|0|0|0";

                            //Create group
                            MySqlCommand command = General.Connection.CreateCommand();
                            command.CommandText = "INSERT INTO groups (name, created, banknumber, leader, members, status, licenses) VALUES (@name, @created, @banknumber, @leader, @members, @status, @licenses)";
                            command.Parameters.AddWithValue("@name", newgroup.name);
                            command.Parameters.AddWithValue("@created", newgroup.created);
                            command.Parameters.AddWithValue("@banknumber", newgroup.banknumber);
                            command.Parameters.AddWithValue("@leader", newgroup.leader);
                            command.Parameters.AddWithValue("@members", newgroup.members);
                            command.Parameters.AddWithValue("@status", newgroup.status);
                            command.Parameters.AddWithValue("@licenses", newgroup.licenses);
                            command.ExecuteNonQuery();

                            newgroup.id = (int)command.LastInsertedId;

                            //Insert group rangs
                            MySqlCommand command2 = General.Connection.CreateCommand();
                            command2.CommandText = "INSERT INTO groupsrangs (groupsid, rang1, rang2, rang3, rang4, rang5, rang6, rang7, rang8, rang9, rang10, rang11, rang12) VALUES (@groupsid, @emptyrang, @emptyrang, @emptyrang, @emptyrang, @emptyrang, @emptyrang, @emptyrang, @emptyrang, @emptyrang, @emptyrang, @emptyrang, @emptyrang)";
                            command2.Parameters.AddWithValue("@groupsid", newgroup.id);
                            command2.Parameters.AddWithValue("@emptyrang", "n/A");
                            command2.ExecuteNonQuery();

                            GroupsRangs groupRangs = new GroupsRangs();
                            groupRangs.id = (int)command2.LastInsertedId;
                            groupRangs.groupsid = newgroup.id;
                            groupRangs.rang1 = "n/A";
                            groupRangs.rang2 = "n/A";
                            groupRangs.rang3 = "n/A";
                            groupRangs.rang4 = "n/A";
                            groupRangs.rang5 = "n/A";
                            groupRangs.rang6 = "n/A";
                            groupRangs.rang7 = "n/A";
                            groupRangs.rang8 = "n/A";
                            groupRangs.rang9 = "n/A";
                            groupRangs.rang10 = "n/A";
                            groupRangs.rang11 = "n/A";
                            groupRangs.rang12 = "n/A";

                            groupRangList.Add(groupRangs);

                            //Insert Membership
                            MySqlCommand command3 = General.Connection.CreateCommand();
                            command3.CommandText = "INSERT INTO groups_members (groupsid, charid, rang, duty_time, payday, payday_day, since) VALUES (@groupsid, @charid, @rang, @duty_time, @payday, @payday_day, @since)";
                            command3.Parameters.AddWithValue("@groupsid", newgroup.id);
                            command3.Parameters.AddWithValue("@charid", character.id);
                            command3.Parameters.AddWithValue("@rang", 12);
                            command3.Parameters.AddWithValue("@duty_time", 0);
                            command3.Parameters.AddWithValue("@payday", 0);
                            command3.Parameters.AddWithValue("@payday_day", 0);
                            command3.Parameters.AddWithValue("@since", Helper.UnixTimestamp());
                            command3.ExecuteNonQuery();

                            groupList.Add(newgroup);

                            character.mygroup = newgroup.id;
                            player.SetOwnSharedData("Player:Group", newgroup.id);
                            player.SetOwnSharedData("Player:GroupRang", 12);
                            CharacterController.SetMoney(player, -Convert.ToInt32(Helper.adminSettings.grouparray[5]));
                            CharacterController.SaveCharacter(player);
                            Helper.SetGovMoney(Convert.ToInt32(Helper.adminSettings.grouparray[5]), "Gruppierung erstellt");
                            Helper.CreateUserTimeline(account.id, character.id, $"Gruppierung {newgroup.name} erstellt", 4);
                            Helper.SendNotificationWithoutButton2(player, "Die neue Gruppierung wurde erstellt, du kannst diese im F2 Menü verwalten!", "success", "center", 4500);
                            break;
                        }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnGroupSettings]: " + e.ToString());
            }
        }
    }
}
