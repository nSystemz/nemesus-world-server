using GTANetworkAPI;
using System;
using MySql.Data.MySqlClient;
using NemesusWorld.Utils;
using NemesusWorld.Controllers;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections;
using System.Security.Principal;

namespace NemesusWorld.Database
{
    class Account : Script
    {
        public enum AdminRanks { KeinAdmin, ProbeModerator, Moderator, Supporter, Administrator, HighAdministrator, Manager, Development, Projektleiter };
        public static string[] AdminNames = { "Kein Admin", "Probe Moderator", "Moderator", "Supporter", "Administrator", "High Administrator", "Manager", "Development", "Projektleiter" };

        public int id { get; set; }
        public string name { get; set; }
        public int adminlevel { get; set; }
        public int admin_since { get; set; }
        public int selectedcharacter { get; set; }
        public int selectedcharacterintern { get; set; }
        public int last_login { get; set; }
        public int account_created { get; set; }
        public int last_save { get; set; }
        public int level { get; set; }
        public int play_time { get; set; }
        public int play_points { get; set; }
        public int kills { get; set; }
        public int deaths { get; set; }
        public int crimes { get; set; }
        public int premium { get; set; }
        public int premium_time { get; set; }
        public int coins { get; set; }
        public int warns { get; set; }
        public string warns_text { get; set; }
        public int online { get; set; }
        public int namechanges { get; set; }
        public string geworben { get; set; }
        public string theme { get; set; }
        public int ban { get; set; }
        public string bantext { get; set; }
        public string admin_rang { get; set; }
        public int prison { get; set; }
        public string last_ip { get; set; }
        public ulong identifier { get; set; }
        public int login_bonus { get; set; }
        public string login_bonus_before { get; set; }
        public int dsgvo_closed { get; set; }
        public int forumaccount { get; set; }
        public int forumcode { get; set; }
        public int forumupdate { get; set; }
        public int autologin { get; set; }
        public int rpquizfinish { get; set; }
        public int crosshair { get; set; }
        public int shootingrange { get; set; }
        public string faq { get; set; }
        public string[] faqarray { get; set; } //Nicht speichern
        public int givepremium { get; set; }
        public int houseslots { get; set; }
        public int vehicleslots { get; set; }
        public int epboost { get; set; }
        //Nicht speichern
        public Player _Player;
        public bool logged_in { get; set; }

        public Account()
        {
            id = 0;
            name = "";
            adminlevel = 0;
            admin_since = 1609462861;
            selectedcharacter = -1;
            selectedcharacterintern = -1;
            last_login = 1609462861;
            account_created = 1609462861;
            last_save = 1609462861;
            level = 1;
            play_time = 1;
            play_points = 0;
            kills = 0;
            deaths = 0;
            crimes = 0;
            premium = 0;
            premium_time = 1609462861;
            coins = 0;
            warns = 0;
            warns_text = "n/A|n/A|n/A|n/A|n/A";
            online = 0;
            namechanges = 0;
            geworben = "Keiner";
            theme = "dark";
            ban = 0;
            bantext = "n/A";
            admin_rang = "n/A";
            prison = 0;
            last_ip = "n/A";
            identifier = 10;
            login_bonus = 0;
            login_bonus_before = "n/A";
            dsgvo_closed = 0;
            forumaccount = -1;
            forumcode = 0;
            forumupdate = 1609462861;
            autologin = 0;
            rpquizfinish = 0;
            crosshair = 17;
            shootingrange = 0;
            faq = "1,0,0,0,0,0,0,0,0,0";
            faqarray = faq.Split(",");
            givepremium = 0;
            houseslots = 0;
            epboost = 0;
            _Player = null;
            logged_in = false;

        }

        public Account(string name, Player player)
        {
            id = 0;
            this.name = name;
            adminlevel = 0;
            admin_since = 1609462861;
            selectedcharacter = -1;
            last_login = 1609462861;
            account_created = 1609462861;
            last_save = 1609462861;
            level = 1;
            play_time = 1;
            play_points = 0;
            kills = 0;
            deaths = 0;
            crimes = 0;
            premium = 0;
            premium_time = 1609462861;
            coins = 0;
            warns = 0;
            warns_text = "n/A|n/A|n/A|n/A|n/A";
            online = 0;
            namechanges = 0;
            geworben = "Keiner";
            theme = "dark";
            ban = 0;
            bantext = "n/A";
            admin_rang = "n/A";
            prison = 0;
            last_ip = "n/A";
            identifier = 10;
            login_bonus = 0;
            login_bonus_before = "n/A";
            dsgvo_closed = 0;
            forumaccount = -1;
            forumcode = 0;
            forumupdate = 1609462861;
            autologin = 0;
            rpquizfinish = 0;
            crosshair = 17;
            shootingrange = 0;
            faq = "1,0,0,0,0,0,0,0,0,0";
            faqarray = faq.Split(",");
            givepremium = 0;
            vehicleslots = 0;
            epboost = 0;
            _Player = player;
            logged_in = false;
        }

        public static bool IsAdmin(Player player, int alvl)
        {
            Account account = Helper.GetAccountData(player);
            if (account == null) return false;
            return account.adminlevel >= alvl;
        }

        public static bool IsAdminOnDuty(Player player, int alvl)
        {
            try
            {
                Account account = Helper.GetAccountData(player);
                TempData tempData = Helper.GetCharacterTempData(player);
                if (account == null || tempData == null) return false;
                if (tempData.adminduty == false) return false;
                return account.adminlevel >= alvl;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[IsAdminOnDuty]: " + e.ToString());
            }
            return false;
        }

        public static string GetAdminRangName(Player player)
        {
            string adminRangName = AdminNames[0];
            Account account = Helper.GetAccountData(player);
            if (account == null) return adminRangName;
            return AdminNames[account.adminlevel];
        }

        //Database
        public static bool IsAccountAvailable(string name)
        {
            try
            {
                MySqlCommand command = General.Connection.CreateCommand();
                command.CommandText = "SELECT id FROM users WHERE name=@name LIMIT 1";
                command.Parameters.AddWithValue("@name", name);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Close();
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[IsAccountAvailable]: " + e.ToString());
            }
            return false;
        }

        public static bool CreateNewAccount(Account account, string name, string password, string playerip, ulong identifier)
        {
            string saltedpw = BCrypt.Net.BCrypt.HashPassword(password + "(8wgwWoRld136=");

            account.name = name;
            account.last_ip = playerip;
            account.last_login = Helper.UnixTimestamp();
            account.last_save = Helper.UnixTimestamp();

            try
            {
                MySqlCommand command = General.Connection.CreateCommand();
                command.CommandText = "INSERT INTO users (password, name, adminlevel, last_ip, identifier, account_created, last_login, last_save) VALUES (@password, @name, @adminlevel, @last_ip, @identifier, @account_created, @last_login, @last_save)";

                command.Parameters.AddWithValue("@password", saltedpw);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@adminlevel", account.adminlevel);
                command.Parameters.AddWithValue("@last_ip", playerip);
                command.Parameters.AddWithValue("@identifier", identifier);
                command.Parameters.AddWithValue("@account_created", Helper.UnixTimestamp());
                command.Parameters.AddWithValue("@last_login", account.last_login);
                command.Parameters.AddWithValue("@last_save", account.last_save);

                command.ExecuteNonQuery();
                account.id = (int)command.LastInsertedId;

                Helper.CreateAdminLog($"registerlog", name + " hat sich einen neuen Account (ID: " + account.id + ") erstellt!", playerip, identifier);

                Helper.CreateUserLog(account.id, "Account erstellt");

                Helper.CreateUserTimeline(account.id, -1, "Account erstellt", 0);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CreateNewAccount]: " + e.ToString());
                return false;
            }
            return true;
        }

        public static void LoadAccount(Account account)
        {
            try
            {
                MySqlCommand command = General.Connection.CreateCommand();
                command.CommandText = "SELECT * FROM users WHERE name=@name LIMIT 1";
                command.Parameters.AddWithValue("@name", account.name);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        account.id = reader.GetInt16("id");
                        account.adminlevel = reader.GetInt16("adminlevel");
                        account.name = reader.GetString("name");
                        account.admin_since = reader.GetInt32("admin_since");
                        account.selectedcharacter = reader.GetInt32("selectedcharacter");
                        account.selectedcharacterintern = reader.GetInt32("selectedcharacterintern");
                        account.last_login = reader.GetInt32("last_login");
                        account.account_created = reader.GetInt32("account_created");
                        account.last_save = reader.GetInt32("last_save");
                        account.level = reader.GetInt16("level");
                        account.play_time = reader.GetInt16("play_time");
                        account.play_points = reader.GetInt16("play_points");
                        account.kills = reader.GetInt16("kills");
                        account.deaths = reader.GetInt16("deaths");
                        account.crimes = reader.GetInt16("crimes");
                        account.premium = reader.GetInt16("premium");
                        account.premium_time = reader.GetInt32("premium_time");
                        account.coins = reader.GetInt16("coins");
                        account.warns = reader.GetInt16("warns");
                        account.warns_text = reader.GetString("warns_text");
                        account.online = reader.GetInt16("online");
                        account.namechanges = reader.GetInt16("namechanges");
                        account.geworben = reader.GetString("geworben");
                        account.theme = reader.GetString("theme");
                        account.ban = reader.GetInt32("ban");
                        account.bantext = reader.GetString("bantext");
                        account.admin_rang = reader.GetString("admin_rang");
                        account.prison = reader.GetInt16("prison");
                        account.last_ip = reader.GetString("last_ip");
                        account.identifier = reader.GetUInt32("identifier");
                        account.login_bonus = reader.GetInt16("login_bonus");
                        account.login_bonus_before = reader.GetString("login_bonus_before");
                        account.dsgvo_closed = reader.GetInt16("dsgvo_closed");
                        account.forumaccount = reader.GetInt32("forumaccount");
                        account.forumcode = reader.GetInt16("forumcode");
                        account.forumupdate = reader.GetInt32("forumupdate");
                        account.autologin = reader.GetInt16("autologin");
                        account.rpquizfinish = reader.GetInt32("rpquizfinish");
                        account.crosshair = reader.GetInt16("crosshair");
                        account.shootingrange = reader.GetInt32("shootingrange");
                        account.faq = reader.GetString("faq");
                        account.givepremium = reader.GetInt16("givepremium");
                        account.houseslots = reader.GetInt16("houseslots");
                        account.vehicleslots = reader.GetInt16("vehicleslots");
                        account.epboost = reader.GetInt32("epboost");
                        reader.Close();
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[LoadAccount]: " + e.ToString());
            }
        }

        public static void SaveAccount(Player player)
        {
            try
            {
                if (General.DatabaseConnectionCheck == true)
                {
                    Account account = Helper.GetAccountData(player);
                    if (account == null) return;

                    account.last_save = Helper.UnixTimestamp();
                    account.faq = String.Join(",", account.faqarray);

                    MySqlCommand command = General.Connection.CreateCommand();
                    command.CommandText = "UPDATE users SET name=@name,adminlevel=@adminlevel,admin_since=@admin_since,selectedcharacter=@selectedcharacter,selectedcharacterintern=@selectedcharacterintern,last_login=@last_login,account_created=@account_created," +
                    "last_save=@last_save,level=@level,play_time=@play_time,play_points=@play_points,kills=@kills,deaths=@deaths,crimes=@crimes,premium=@premium,premium_time=@premium_time,coins=@coins,warns=@warns,warns_text=@warns_text," +
                    "online=@online,namechanges=@namechanges,theme=@theme,ban=@ban,bantext=@bantext,admin_rang=@admin_rang,prison=@prison,last_ip=@last_ip,identifier=@identifier,login_bonus=@login_bonus,login_bonus_before=@login_bonus_before," +
                    "dsgvo_closed=@dsgvo_closed,forumaccount=@forumaccount,forumcode=@forumcode,forumupdate=@forumupdate,autologin=@autologin,rpquizfinish=@rpquizfinish,crosshair=@crosshair,shootingrange=@shootingrange,faq=@faq,givepremium=@givepremium," +
                    "vehicleslots=@vehicleslots,epboost=@epboost" +
                    " WHERE id=@id";

                    command.Parameters.AddWithValue("@name", account.name);
                    command.Parameters.AddWithValue("@adminlevel", account.adminlevel);
                    command.Parameters.AddWithValue("@admin_since", account.admin_since);
                    command.Parameters.AddWithValue("@selectedcharacter", account.selectedcharacter);
                    command.Parameters.AddWithValue("@selectedcharacterintern", account.selectedcharacterintern);
                    command.Parameters.AddWithValue("@last_login", account.last_login);
                    command.Parameters.AddWithValue("@account_created", account.account_created);
                    command.Parameters.AddWithValue("@last_save", account.last_save);
                    command.Parameters.AddWithValue("@level", account.level);
                    command.Parameters.AddWithValue("@play_time", account.play_time);
                    command.Parameters.AddWithValue("@play_points", account.play_points);
                    command.Parameters.AddWithValue("@kills", account.kills);
                    command.Parameters.AddWithValue("@deaths", account.deaths);
                    command.Parameters.AddWithValue("@crimes", account.crimes);
                    command.Parameters.AddWithValue("@premium", account.premium);
                    command.Parameters.AddWithValue("@premium_time", account.premium_time);
                    command.Parameters.AddWithValue("@coins", account.coins);
                    command.Parameters.AddWithValue("@warns", account.warns);
                    command.Parameters.AddWithValue("@warns_text", account.warns_text);
                    command.Parameters.AddWithValue("@online", account.online);
                    command.Parameters.AddWithValue("@namechanges", account.namechanges);
                    command.Parameters.AddWithValue("@theme", account.theme);
                    command.Parameters.AddWithValue("@ban", account.ban);
                    command.Parameters.AddWithValue("@bantext", account.bantext);
                    command.Parameters.AddWithValue("@admin_rang", account.admin_rang);
                    command.Parameters.AddWithValue("@prison", account.prison);
                    command.Parameters.AddWithValue("@last_ip", account.last_ip);
                    command.Parameters.AddWithValue("@identifier", account.identifier);
                    command.Parameters.AddWithValue("@login_bonus", account.login_bonus);
                    command.Parameters.AddWithValue("@login_bonus_before", account.login_bonus_before);
                    command.Parameters.AddWithValue("@dsgvo_closed", account.dsgvo_closed);
                    command.Parameters.AddWithValue("@forumaccount", account.forumaccount);
                    command.Parameters.AddWithValue("@forumcode", account.forumcode);
                    command.Parameters.AddWithValue("@forumupdate", account.forumupdate);
                    command.Parameters.AddWithValue("@autologin", account.autologin);
                    command.Parameters.AddWithValue("@rpquizfinish", account.rpquizfinish);
                    command.Parameters.AddWithValue("@crosshair", account.crosshair);
                    command.Parameters.AddWithValue("@shootingrange", account.shootingrange);
                    command.Parameters.AddWithValue("@faq", account.faq);
                    command.Parameters.AddWithValue("@givepremium", account.givepremium);
                    command.Parameters.AddWithValue("@vehicleslots", account.vehicleslots);
                    command.Parameters.AddWithValue("@epboost", account.epboost);
                    command.Parameters.AddWithValue("@id", account.id);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[SaveAccount]: " + e.ToString());
            }
        }

        public static bool CheckPassword(string name, string passwordinput)
        {
            string password = null;

            MySqlCommand command = General.Connection.CreateCommand();
            command.CommandText = "SELECT password FROM users WHERE name=@name LIMIT 1";
            command.Parameters.AddWithValue("@name", name);

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    password = reader.GetString("password");
                    reader.Close();
                }
            }

            string oldpassword = passwordinput + "(8wgwWoRld136=";

            if (BCrypt.Net.BCrypt.Verify(oldpassword, password)) return true;
            return false;
        }

        public static bool CheckAccount(string name)
        {
            MySqlCommand command = General.Connection.CreateCommand();
            command.CommandText = "SELECT id FROM users WHERE name=@name LIMIT 1";
            command.Parameters.AddWithValue("@name", name);

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Close();
                    return true;
                }
            }
            return false;
        }

        public static void CheckRockstarIdentifier(Player player)
        {
            try
            {
                player.Dimension = 50;
                NAPI.Task.Run(() =>
                {
                    if (player.GetData<bool>("Client:Login") == false && player.Name.Contains("Spieler-"))
                    {
                        player.Name = player.GetData<string>("Player:ConnectName");
                        Helper.SendNotificationWithoutButton(player, "Du wurdest gekickt, weil du dich nicht eingeloggt hast!", "error", "top-end");
                        AntiCheatController.SetKick(player, "Nicht eingeloggt", "Server", false, false);
                        return;
                    }
                }, delayTime: 300000);

                int autologin = 0;
                int checkid = -1;
                int ban = 0;
                string name = "n/A";

                MySqlCommand command = General.Connection.CreateCommand();
                command.CommandText = "SELECT autologin,name,id,ban FROM users WHERE identifier=@identifier LIMIT 1";
                command.Parameters.AddWithValue("@identifier", player.SocialClubId);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        autologin = reader.GetInt16("autologin");
                        name = reader.GetString("name");
                        checkid = reader.GetInt32("id");
                        ban = reader.GetInt32("ban");
                    }
                    reader.Close();
                }
                if (checkid != -1)
                {
                    if (autologin == 1 && ban == 0)
                    {
                        Account account = new Account(name, player);
                        if (!IsPlayerLoggedIn(player) && !isPlayerLoggedInOnce(name))
                        {
                            account.Login(player, false);
                        }
                        else
                        {
                            player.TriggerEvent("Client:ShowLogin");
                        }
                    }
                    else
                    {
                        player.TriggerEvent("Client:ShowLogin");
                    }
                }
                else
                {
                    player.TriggerEvent("Client:ShowRegister");
                }
                player.TriggerEvent("Client:ShowLoading");
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CheckRockstarIdentifier]: " + e.ToString());
            }
        }

        //General
        public static bool IsPlayerLoggedIn(Player player)
        {
            if (player != null) return player.HasData(Helper.GetAccountKey());
            return false;
        }

        public static bool isPlayerLoggedInOnce(string name)
        {
            foreach (Player p in NAPI.Pools.GetAllPlayers())
            {
                Account account = Helper.GetAccountData(p);
                if (account != null && account.name.ToLower() == name.ToLower())
                {
                    return true;
                }
            }
            return false;
        }

        public void Login(Player player, bool firstlogin)
        {
            try
            {
                LoadAccount(this);
                logged_in = true;
                player.SetData(Helper.GetAccountKey(), this);
                NAPI.Data.SetEntitySharedData(player, "Player:Adminlevel", adminlevel);
                NAPI.Data.SetEntitySharedData(player, "Player:Name", name);
                if(admin_rang != "n/A")
                {
                    player.SetSharedData("Player:AdminRang", admin_rang);
                }
                player.TriggerEvent("Client:StopSound");
                player.TriggerEvent("Client:HideAuth");
                player.SetData("Client:WrongPW", 0);
                identifier = player.SocialClubId;
                online = 1;
                MySqlCommand command2 = General.Connection.CreateCommand();
                command2.CommandText = "UPDATE users SET online = 1 WHERE id=@id LIMIT 1";
                command2.Parameters.AddWithValue("@id", id);
                command2.ExecuteNonQuery();
                last_login = Helper.UnixTimestamp();
                last_ip = Helper.GetIP(player);
                string logtext = $"{name} hat sich erfolgreich in den Account (ID: {id}) eingeloggt!";
                Helper.CreateAdminLog("loginlog", logtext, Helper.GetIP(player), identifier);

                MySqlCommand command = General.Connection.CreateCommand();
                command.CommandText = "DELETE FROM inactiv WHERE userid=@userid LIMIT 1";
                command.Parameters.AddWithValue("@userid", id);
                command.ExecuteNonQuery();

                if (!firstlogin)
                {
                    if (rpquizfinish == 0 && Helper.adminSettings.rpquiz == true)
                    {
                        player.TriggerEvent("Client:ShowRPQuestions");
                    }
                    else
                    {
                        if(givepremium == 0 && faq == "1,1,1,1,1,1,1,1,1,1")
                        {
                            if(premium == 0)
                            {
                                premium = 1;
                                premium_time = Helper.UnixTimestamp() + (3 * 86400);
                            }
                            else
                            {
                                premium_time += Helper.UnixTimestamp() + (3 * 86400);
                            }
                            givepremium = 1;
                            Helper.CreateUserLog(id, "Auf dem Server eingelebt + Willkommensgeschenk erhalten!");
                            Helper.CreateUserTimeline(id, -1, "Auf dem Server eingelebt", 0);
                        }
                        CharacterController.GetAvailableCharacters(player, id);
                    }
                }
                else
                {
                    if (Helper.adminSettings.rpquiz == true)
                    {
                        player.TriggerEvent("Client:ShowRPQuestions");
                    }
                    else
                    {
                        CharacterController.GetAvailableCharacters(player, id);
                    }
                }
                player.SetData("Client:Login", true);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[Login]: " + e.ToString());
            }
        }

        public void Register(string name, string password, string playerip, ulong identifier)
        {
            if (CreateNewAccount(this, name, password, playerip, identifier))
            {
                Login(_Player, true);
            }
        }

        public bool IsPlayerAdmin(int adminlevel)
        {
            return adminlevel >= this.adminlevel;
        }

        //Events
        [RemoteEvent("Server:OnLogin")]
        public void OnLogin(Player player, string name, string password)
        {
            try
            {
                if (player.HasData("Player:CheckBan")) return;
                if (player.GetData<int>("Client:WrongPW") > 3) return;
                if (name.Length < 3 || name.Length > 35 || password.Length < 6 || password.Length > 35)
                {
                    player.TriggerEvent("Client:SetWarningLogin", "Ungültige Angaben!");
                    return;
                }
                if (CheckAccount(name))
                {
                    string checkBan = AntiCheatController.CheckBan(name, player.SocialClubId);
                    if (checkBan != "n/A")
                    {
                        bool found = false;
                        string[] ban = new string[3];
                        ban = checkBan.Trim().Split(",");
                        if (Convert.ToInt32(ban[0]) == 1)
                        {
                            player.TriggerEvent($"Client:SetWarningLogin", $"Du bist permanent gebannt, Grund: {ban[1]}!");
                            found = true;
                        }
                        else if (Convert.ToInt32(ban[0]) > 1)
                        {
                            player.TriggerEvent($"Client:SetWarningLogin", $"Du bist noch bis zum {Helper.UnixTimeStampToDateTime(Convert.ToInt32(ban[0]))} Uhr gebannt, Grund: {ban[1]}!");
                            found = true;
                        }
                        else if (Convert.ToInt32(ban[2]) > 0)
                        {
                            player.TriggerEvent($"Client:SetWarningLogin", $"Du bist permanent gebannt, bitte melde dich bei uns im Support!");
                            found = true;
                        }
                        if (found == true)
                        {
                            player.SetData<bool>("Player:CheckBan", true);
                            Helper.CreateAdminLog($"loginlog", name + " hat versucht sich einzuloggen, obwohl er gebannt ist!", Helper.GetIP(player), player.SocialClubId);
                            return;
                        }
                    }
                    if (!CheckPassword(name, password))
                    {
                        player.TriggerEvent("Client:SetWarningLogin", "Falsches Passwort!");
                        if (player.GetData<int>("Client:WrongPW") >= 3)
                        {
                            int id = Helper.SelectIDFromName(name);
                            player.TriggerEvent("Client:SetWarningLogin", "Du hast das Passwort zu oft falsch eingegeben und wurdest vom Server gekickt!");
                            Helper.CreateAdminLog($"loginlog", name + " hat das Accountpasswort 3x falsch eingegeben und wurde gekickt!", Helper.GetIP(player), player.SocialClubId);
                            Helper.CreateUserLog(id, "Ungültiger Gameserver Login");
                            AntiCheatController.SetKick(player, "3x falsches Passwort eingegeben", "Server", false, false);
                        }
                        return;
                    }
                    if (IsPlayerLoggedIn(player) || isPlayerLoggedInOnce(name))
                    {
                        player.TriggerEvent("Client:SetWarningLogin", "Du bist bereits eingeloggt!");
                        return;
                    }
                    Account account = new Account(name, player);
                    account.Login(player, false);
                }
                else
                {
                    player.TriggerEvent("Client:SetWarningLogin", "Ungültiger Account!");
                    return;
                }
            }
            catch (Exception e)
            {
                player.TriggerEvent("Client:SetWarningLogin", "Ungültige Angaben!");
                Helper.ConsoleLog("error", $"[Server:OnLogin]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:OnRegister")]
        public void OnRegister(Player player, string name, string password)
        {
            try
            {
                if (player.HasData("Player:CheckBan")) return;
                if (name.Length < 3 || name.Length > 35 || password.Length < 6 || password.Length > 35)
                {
                    player.TriggerEvent("Client:SetWarningRegister", "Ungültige Angaben!");
                    return;
                }
                if (!Regex.IsMatch(name, "^[A-Z][a-zA-Z]{2,35}"))
                {
                    player.TriggerEvent("Client:SetWarningRegister", "Für den Accountnamen dürfen nur Buchstaben benutzt werden und der erste Buchstabe muss groß geschrieben werden!");
                    return;
                }
                if (IsAccountAvailable(name))
                {
                    player.TriggerEvent("Client:SetWarningRegister", "Dieser Account existiert bereits!");
                    return;
                }
                MySqlCommand command = General.Connection.CreateCommand();
                command.CommandText = "SELECT autologin,name,id,ban FROM users WHERE identifier=@identifier LIMIT 1";
                command.Parameters.AddWithValue("@identifier", player.SocialClubId);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        player.TriggerEvent("Client:SetWarningRegister", "Du kannst nur einen Account erstellen!");
                        reader.Close();
                        return;
                    }
                }
                string checkBan = AntiCheatController.CheckBan(name, player.SocialClubId);
                if (checkBan != "n/A")
                {
                    bool found = false;
                    string[] ban = new string[3];
                    ban = checkBan.Trim().Split(",");
                    if (Convert.ToInt32(ban[0]) == 1)
                    {
                        player.TriggerEvent($"Client:SetWarningLogin", $"Du bist permanent gebannt, Grund: {ban[1]}!");
                        found = true;
                    }
                    else if (Convert.ToInt32(ban[0]) > 1)
                    {
                        player.TriggerEvent($"Client:SetWarningLogin", $"Du bist noch bis zum {Helper.UnixTimeStampToDateTime(Convert.ToInt32(ban[0]))} Uhr gebannt, Grund: {ban[1]}!");
                        found = true;
                    }
                    else if (Convert.ToInt32(ban[2]) > 0)
                    {
                        player.TriggerEvent($"Client:SetWarningLogin", $"Du bist vom Server gebannt, bitte melde dich bei uns im Support!");
                        found = true;
                    }
                    if (found == true)
                    {
                        player.SetData<bool>("Player:CheckBan", true);
                        Helper.CreateAdminLog($"loginlog", name + " hat versucht sich einzuloggen, obwohl er gebannt ist!", Helper.GetIP(player), player.SocialClubId);
                        return;
                    }
                }
                player.TriggerEvent("Client:ShowLoading");
                player.TriggerEvent("Client:ShowRegister");
                NAPI.Task.Run(() =>
                {
                    Account accounts = new Account(name, player);
                    accounts.Register(name, password, Helper.GetIP(player), player.SocialClubId);
                }, delayTime: 2500);
            }
            catch (Exception e)
            {
                player.TriggerEvent("Client:SetWarningLogin", "Ungültige Angaben!");
                Helper.ConsoleLog("error", $"[Server:OnRegister]: " + e.ToString());
            }
        }

        public static bool GetAdminLogin(Player player)
        {
            int admincheck = (int)NAPI.Data.GetEntitySharedData(player, "Player:AdminLogin");
            if (admincheck == 1)
            {
                return true;
            }
            return false;
        }
    }
}
