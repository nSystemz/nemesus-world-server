using GTANetworkAPI;
using MySqlConnector;
using NemesusWorld.Database;
using NemesusWorld.Models;
using NemesusWorld.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NemesusWorld.Controllers
{
    class TicketController : Script
    {
        [RemoteEvent("Server:LoadAllTickets")]
        public static void OnLoadAllTickets(Player player, int check = 1)
        {
            try
            {
                Account account = Helper.GetAccountData(player);

                if (account == null) return;

                int count = 0;

                MySqlCommand command = General.Connection.CreateCommand();
                command = General.Connection.CreateCommand();
                if (account.adminlevel > 0)
                {
                    if (account.adminlevel >= (int)Account.AdminRanks.HighAdministrator)
                    {
                        command.CommandText = "SELECT * FROM tickets as ts JOIN ticket_user as tu ON ts.id = tu.ticketid WHERE ts.status != 9 GROUP BY ts.id ORDER by ts.timestamp asc LIMIT 25";
                    }
                    else
                    {
                        command.CommandText = "SELECT * FROM tickets as ts JOIN ticket_user as tu ON ts.id = tu.ticketid WHERE ts.status != 9 AND (tu.userid = @userid or ts.admin = -1) GROUP BY ts.id ORDER by ts.timestamp asc LIMIT 25";
                    }
                }
                else
                {
                    command.CommandText = "SELECT * FROM tickets as ts JOIN ticket_user as tu ON ts.id = tu.ticketid WHERE ts.status != 9 AND tu.userid = @userid GROUP BY ts.id ORDER by ts.timestamp asc LIMIT 25";
                }
                command.Parameters.AddWithValue("@userid", account.id);

                List<Tickets> ticketList = new List<Tickets>();

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Tickets ticket = new Tickets();
                        ticket.id = reader.GetInt32("id");
                        ticket.user = "" + reader.GetInt32("userid");
                        ticket.title = Helper.ReplaceUmlauts(reader.GetString("title"));
                        ticket.prio = reader.GetString("prio");
                        ticket.text = Helper.Base64Encode(Helper.ReplaceUmlauts(reader.GetString("text")));
                        ticket.timestamp = reader.GetInt32("timestamp");
                        ticket.admin = "" + reader.GetInt32("admin");
                        ticket.status = reader.GetInt32("status");
                        ticketList.Add(ticket);
                        count++;
                    }
                    reader.Close();
                }


                foreach (Tickets ticket in ticketList)
                {
                    if (ticket != null)
                    {
                        ticket.user = Helper.GetAccountName(Convert.ToInt32(ticket.user));
                        ticket.admin = Helper.GetAccountName(Convert.ToInt32(ticket.admin));
                    }
                }

                player.TriggerEvent("Client:GetAllTickets", NAPI.Util.ToJson(ticketList.GroupBy(x => x).Select(d => d.First()).ToList()), count, check);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnLoadAllTickets]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:LoadTicketAnswers")]
        public static void OnLoadTicketAnswers(Player player, int ticketid)
        {
            try
            {
                Account account = Helper.GetAccountData(player);

                if (account == null) return;

                MySqlCommand command = General.Connection.CreateCommand();
                command = General.Connection.CreateCommand();
                command.CommandText = "SELECT * FROM ticket_answers WHERE ticketid = @ticketid ORDER BY timestamp asc";
                command.Parameters.AddWithValue("@ticketid", ticketid);

                List<TicketAnswers> ticketAnswers = new List<TicketAnswers>();

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TicketAnswers ticketAnswer = new TicketAnswers();
                        ticketAnswer.id = reader.GetInt32("id");
                        ticketAnswer.ticketid = reader.GetInt32("ticketid");
                        ticketAnswer.user = "" + reader.GetInt32("userid");
                        ticketAnswer.text = Helper.Base64Encode(Helper.ReplaceUmlauts(reader.GetString("text")));
                        ticketAnswer.timestamp = reader.GetInt32("timestamp");
                        ticketAnswers.Add(ticketAnswer);
                    }
                    reader.Close();
                }

                foreach (TicketAnswers ticketAnswer in ticketAnswers)
                {
                    if (ticketAnswer != null)
                    {
                        ticketAnswer.user = Helper.GetAccountName(Convert.ToInt32(ticketAnswer.user));
                    }
                }

                player.TriggerEvent("Client:GetAllAnswers", NAPI.Util.ToJson(ticketAnswers), account.name);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnLoadTicketAnswers]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:UpdateTicket")]
        public static void OnUpdateTicket(Player player, int ticketid)
        {
            try
            {
                Account account = Helper.GetAccountData(player);

                if (account == null) return;

                MySqlCommand command = General.Connection.CreateCommand();
                command = General.Connection.CreateCommand();
                command.CommandText = "SELECT * FROM tickets WHERE id = @ticketid LIMIT 1";
                command.Parameters.AddWithValue("@ticketid", ticketid);

                Tickets ticket = new Tickets();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        ticket.id = reader.GetInt32("id");
                        ticket.user = "" + reader.GetInt32("userid");
                        ticket.title = reader.GetString("title");
                        ticket.prio = reader.GetString("prio");
                        ticket.text = Helper.Base64Encode(reader.GetString("text"));
                        ticket.timestamp = reader.GetInt32("timestamp");
                        ticket.admin = "" + reader.GetInt32("admin");
                        ticket.status = reader.GetInt32("status");
                    }
                    reader.Close();
                }

                ticket.user = Helper.GetAccountName(Convert.ToInt32(ticket.user));
                ticket.admin = Helper.GetAccountName(Convert.ToInt32(ticket.admin));

                MySqlCommand command2 = General.Connection.CreateCommand();
                command2 = General.Connection.CreateCommand();
                command2.CommandText = "SELECT * FROM ticket_answers WHERE ticketid = @ticketid ORDER BY timestamp asc";
                command2.Parameters.AddWithValue("@ticketid", ticketid);

                List<TicketAnswers> ticketAnswers = new List<TicketAnswers>();

                using (MySqlDataReader reader = command2.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TicketAnswers ticketAnswer = new TicketAnswers();
                        ticketAnswer.id = reader.GetInt32("id");
                        ticketAnswer.ticketid = reader.GetInt32("ticketid");
                        ticketAnswer.user = "" + reader.GetInt32("userid");
                        ticketAnswer.text = Helper.Base64Encode(Helper.ReplaceUmlauts(reader.GetString("text")));
                        ticketAnswer.timestamp = reader.GetInt32("timestamp");
                        ticketAnswers.Add(ticketAnswer);
                    }
                    reader.Close();
                }

                foreach (TicketAnswers ticketAnswer in ticketAnswers)
                {
                    if (ticketAnswer != null)
                    {
                        ticketAnswer.user = Helper.GetAccountName(Convert.ToInt32(ticketAnswer.user));
                    }
                }

                player.TriggerEvent("Client:TicketUpdate", NAPI.Util.ToJson(ticketAnswers), NAPI.Util.ToJson(ticket));
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnUpdateTicket]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:TicketSystem")]
        public static void OnTicketSystem(Player player, string setting, string text)
        {
            Account account = Helper.GetAccountData(player);

            if (account == null) return;

            string[] ticketArray = new string[6];
            if (text != "finish")
            {
                ticketArray = text.Split("|");
            }
            switch (setting.ToLower())
            {
                case "changestatus":
                    {
                        if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.ProbeModerator))
                        {
                            Helper.SendNotificationWithoutButton2(player, "Unzureichende Adminrechte!", "error", "center");
                            return;
                        }
                        if (ticketArray[1] == ticketArray[2])
                        {
                            Helper.SendNotificationWithoutButton2(player, "Das Ticket hat diesen Status bereits!", "error", "center");
                            return;
                        }
                        if (ticketArray[2] == "9")
                        {
                            Helper.SendNotificationWithoutButton2(player, "Das Ticket wurde bereits archiviert, es kann nicht mehr verändert werden!", "error", "center");
                            return;
                        }
                        if (ticketArray[1] == "2" && ticketArray[2] != "1")
                        {
                            Helper.SendNotificationWithoutButton2(player, "Das Ticket muss zuerst in Bearbeitung gesetzt werden!", "error", "center");
                            return;
                        }
                        if (ticketArray[1] == "9" && ticketArray[2] != "2")
                        {
                            Helper.SendNotificationWithoutButton2(player, "Das Ticket muss zuerst geschlossen werden!", "error", "center");
                            return;
                        }

                        string status = ticketArray[1] == "0" ? "Offen" : ticketArray[1] == "1" ? "In Bearbeitung" : ticketArray[1] == "2" ? "Geschlossen" : "Archiviert";

                        if (ticketArray[3] == "Keiner" || ticketArray[3] == "-1")
                        {
                            MySqlCommand command4 = General.Connection.CreateCommand();
                            command4 = General.Connection.CreateCommand();
                            command4.CommandText = "INSERT INTO ticket_answers (ticketid, userid, text, timestamp) VALUES (@ticketid, @userid, @text, @timestamp)";
                            command4.Parameters.AddWithValue("@ticketid", Convert.ToInt32(ticketArray[0]));
                            command4.Parameters.AddWithValue("@userid", account.id);
                            command4.Parameters.AddWithValue("@text", $"<p>{account.name} hat {account.name} als Bearbeiter gesetzt!</p>");
                            command4.Parameters.AddWithValue("@timestamp", Helper.UnixTimestamp());
                            command4.ExecuteNonQuery();
                        }

                        MySqlCommand command3 = General.Connection.CreateCommand();
                        command3 = General.Connection.CreateCommand();
                        command3.CommandText = "INSERT INTO ticket_answers (ticketid, userid, text, timestamp) VALUES (@ticketid, @userid, @text, @timestamp)";
                        command3.Parameters.AddWithValue("@ticketid", Convert.ToInt32(ticketArray[0]));
                        command3.Parameters.AddWithValue("@userid", account.id);
                        command3.Parameters.AddWithValue("@text", $"<p>{account.name} hat den Status des Tickets auf {status} gesetzt!</p>");
                        command3.Parameters.AddWithValue("@timestamp", Helper.UnixTimestamp());
                        command3.ExecuteNonQuery();

                        MySqlCommand command5 = General.Connection.CreateCommand();
                        command5 = General.Connection.CreateCommand();
                        if (ticketArray[3] == "Keiner" || ticketArray[3] == "-1")
                        {
                            command5.CommandText = "UPDATE tickets SET status = @status, admin = @admin WHERE id = @id LIMIT 1";
                            command5.Parameters.AddWithValue("@admin", account.id);
                        }
                        else
                        {
                            command5.CommandText = "UPDATE tickets SET status = @status WHERE id = @id LIMIT 1";
                        }
                        command5.Parameters.AddWithValue("@status", Convert.ToInt32(ticketArray[1]));
                        command5.Parameters.AddWithValue("@id", Convert.ToInt32(ticketArray[0]));
                        command5.ExecuteNonQuery();

                        Helper.SendNotificationWithoutButton2(player, "Status erfolgreich geändert!", "success", "center", 3500);
                        break;
                    }
                case "seteditor":
                    {
                        if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.ProbeModerator))
                        {
                            Helper.SendNotificationWithoutButton2(player, "Unzureichende Adminrechte!", "error", "center");
                            return;
                        }

                        MySqlCommand command = General.Connection.CreateCommand();
                        command = General.Connection.CreateCommand();
                        command.CommandText = "SELECT id FROM users where name = @name AND adminlevel > 0 LIMIT 1";
                        command.Parameters.AddWithValue("@name", ticketArray[1]);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                Helper.SendNotificationWithoutButton2(player, "Es konnte kein Admin mit diesem Namen gefunden werden!", "error", "center");
                                return;
                            }
                        }

                        int accId = Helper.GetAccountID(ticketArray[1]);

                        MySqlCommand command4 = General.Connection.CreateCommand();
                        command4 = General.Connection.CreateCommand();
                        command4.CommandText = "SELECT id FROM ticket_user WHERE ticketid = @ticketid AND userid = @userid LIMIT 1";
                        command4.Parameters.AddWithValue("@ticketid", Convert.ToInt32(ticketArray[0]));
                        command4.Parameters.AddWithValue("@userid", accId);

                        using (MySqlDataReader reader = command4.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                Helper.SendNotificationWithoutButton2(player, "Der Admin befindet sich nicht in diesem Ticket!", "error", "center");
                                return;
                            }
                        }

                        if (accId != -1 && accId == account.id)
                        {
                            Helper.SendNotificationWithoutButton2(player, "Dieser Admin ist bereits der Bearbeiter des Tickets!", "error", "center");
                            return;
                        }

                        MySqlCommand command3 = General.Connection.CreateCommand();
                        command3 = General.Connection.CreateCommand();
                        command3.CommandText = "INSERT INTO ticket_answers (ticketid, userid, text, timestamp) VALUES (@ticketid, @userid, @text, @timestamp)";
                        command3.Parameters.AddWithValue("@ticketid", Convert.ToInt32(ticketArray[0]));
                        command3.Parameters.AddWithValue("@userid", account.id);
                        command3.Parameters.AddWithValue("@text", $"<p>{account.name} hat {ticketArray[1]} als Bearbeiter gesetzt!</p>");
                        command3.Parameters.AddWithValue("@timestamp", Helper.UnixTimestamp());
                        command3.ExecuteNonQuery();

                        MySqlCommand command5 = General.Connection.CreateCommand();
                        command5 = General.Connection.CreateCommand();
                        command5.CommandText = "UPDATE tickets set admin = @admin WHERE id = @id LIMIT 1";
                        command5.Parameters.AddWithValue("@admin", accId);
                        command5.Parameters.AddWithValue("@id", Convert.ToInt32(ticketArray[0]));
                        command5.ExecuteNonQuery();

                        Helper.SendNotificationWithoutButton2(player, "Der Admin wurde erfolgreich als Bearbeiter gesetzt!", "success", "center", 3500);
                        break;
                    }
                case "answer":
                    {
                        if (ticketArray[1].Length < 15 || ticketArray[1].Length > 3500)
                        {
                            Helper.SendNotificationWithoutButton2(player, "Ungültige Beschreibungslänge!", "error", "center");
                            return;
                        }

                        MySqlCommand command = General.Connection.CreateCommand();
                        command = General.Connection.CreateCommand();
                        command.CommandText = "INSERT INTO ticket_answers (ticketid, userid, text, timestamp) VALUES (@ticketid, @userid, @text, @timestamp)";
                        command.Parameters.AddWithValue("@ticketid", Convert.ToInt32(ticketArray[0]));
                        command.Parameters.AddWithValue("@userid", account.id);
                        command.Parameters.AddWithValue("@text", ticketArray[1]);
                        command.Parameters.AddWithValue("@timestamp", Helper.UnixTimestamp());
                        command.ExecuteNonQuery();

                        Helper.SendNotificationWithoutButton2(player, "Du hast erfolgreich auf das Ticket geantwortet!", "success", "center", 1250);
                        break;
                    }
                case "adduser":
                    {
                        if (!Account.IsAdminOnDuty(player, (int)Account.AdminRanks.ProbeModerator))
                        {
                            Helper.SendNotificationWithoutButton2(player, "Unzureichende Adminrechte!", "error", "center");
                            return;
                        }

                        bool found = false;

                        MySqlCommand command = General.Connection.CreateCommand();
                        command = General.Connection.CreateCommand();
                        command.CommandText = "SELECT id FROM users where name = @name LIMIT 1";
                        command.Parameters.AddWithValue("@name", ticketArray[1]);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                Helper.SendNotificationWithoutButton2(player, "Es konnte kein Spieler mit diesem Namen gefunden werden!", "error", "center");
                                return;
                            }
                        }

                        int accId = Helper.GetAccountID(ticketArray[1]);

                        MySqlCommand command2 = General.Connection.CreateCommand();
                        command2 = General.Connection.CreateCommand();
                        command2.CommandText = "SELECT id FROM ticket_user WHERE ticketid = @ticketid AND userid = @userid LIMIT 1";
                        command2.Parameters.AddWithValue("@ticketid", Convert.ToInt32(ticketArray[0]));
                        command2.Parameters.AddWithValue("@userid", accId);

                        using (MySqlDataReader reader = command2.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                found = true;
                            }
                        }

                        if (found == true)
                        {
                            Helper.SendNotificationWithoutButton2(player, "Der Spieler befindet sich schon in diesem Ticket!", "error", "center");
                            return;
                        }
                        else
                        {
                            MySqlCommand command3 = General.Connection.CreateCommand();
                            command3 = General.Connection.CreateCommand();
                            command3.CommandText = "INSERT INTO ticket_answers (ticketid, userid, text, timestamp) VALUES (@ticketid, @userid, @text, @timestamp)";
                            command3.Parameters.AddWithValue("@ticketid", Convert.ToInt32(ticketArray[0]));
                            command3.Parameters.AddWithValue("@userid", account.id);
                            command3.Parameters.AddWithValue("@text", $"<p>{account.name} hat {ticketArray[1]} zum Ticket hinzugef&uuml;gt!</p>");
                            command3.Parameters.AddWithValue("@timestamp", Helper.UnixTimestamp());
                            command3.ExecuteNonQuery();

                            MySqlCommand command4 = General.Connection.CreateCommand();
                            command4 = General.Connection.CreateCommand();
                            command4.CommandText = "INSERT INTO ticket_user (ticketid, userid, timestamp) VALUES (@ticketid, @userid, @timestamp)";
                            command4.Parameters.AddWithValue("@ticketid", Convert.ToInt32(ticketArray[0]));
                            command4.Parameters.AddWithValue("@userid", accId);
                            command4.Parameters.AddWithValue("@timestamp", Helper.UnixTimestamp());
                            command4.ExecuteNonQuery();

                            Helper.SendNotificationWithoutButton2(player, "Der Benutzer wurde erfolgreich zum Ticket hinzugefügt!", "success", "center", 3500);
                        }
                        break;
                    }
                case "finish":
                    {

                        MySqlCommand command = General.Connection.CreateCommand();
                        command = General.Connection.CreateCommand();
                        command.CommandText = "UPDATE tickets SET status = 2 WHERE id = @id";
                        command.Parameters.AddWithValue("@id", Convert.ToInt32(text));
                        command.ExecuteNonQuery();

                        MySqlCommand command2 = General.Connection.CreateCommand();
                        command2 = General.Connection.CreateCommand();
                        command2.CommandText = "INSERT INTO ticket_answers (ticketid, userid, text, timestamp) VALUES (@ticketid, @userid, @text, @timestamp)";
                        command2.Parameters.AddWithValue("@ticketid", Convert.ToInt32(text));
                        command2.Parameters.AddWithValue("@userid", account.id);
                        command2.Parameters.AddWithValue("@text", $"{account.name} hat das Ticket als 'erledigt' markiert!");
                        command2.Parameters.AddWithValue("@timestamp", Helper.UnixTimestamp());
                        command2.ExecuteNonQuery();

                        Helper.SendNotificationWithoutButton2(player, "Du hast das Ticket erfolgreich als erledigt markiert!", "success", "center", 3500);
                        break;
                    }
                case "create":
                    {
                        if (account.adminlevel > 0)
                        {
                            Helper.SendNotificationWithoutButton2(player, "Du kannst als Admin kein Ticket eröffnen!", "error", "center");
                            return;
                        }
                        if (ticketArray[0].Length < 3 || ticketArray[0].Length > 64)
                        {
                            Helper.SendNotificationWithoutButton2(player, "Ungültige Titellänge!", "error", "center");
                            return;
                        }
                        if (ticketArray[1] != "low" && ticketArray[1] != "middle" && ticketArray[1] != "high")
                        {
                            Helper.SendNotificationWithoutButton2(player, "Ungültige Priorität!", "error", "center");
                            return;
                        }
                        if (ticketArray[2].Length < 15 || ticketArray[2].Length > 3500)
                        {
                            Helper.SendNotificationWithoutButton2(player, "Ungültige Beschreibungslänge!", "error", "center");
                            return;
                        }

                        int ticketCount = 0;

                        MySqlCommand command = General.Connection.CreateCommand();
                        command = General.Connection.CreateCommand();
                        command.CommandText = "SELECT COUNT(userid) as tickets FROM tickets WHERE userid=@userid AND status = 1";
                        command.Parameters.AddWithValue("@userid", account.id);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                ticketCount = reader.GetInt32("tickets");
                            }
                            reader.Close();
                        }

                        if (ticketCount >= 3)
                        {
                            Helper.SendNotificationWithoutButton2(player, "Du kannst nur max. 3 Tickets gleichzeitig erstellen!", "error", "center");
                            return;
                        }

                        int ticketId = -1;

                        MySqlCommand command2 = General.Connection.CreateCommand();
                        command2.CommandText = "INSERT INTO tickets (userid, title, prio, text, timestamp, status, admin) VALUES (@userid, @title, @prio, @text, @timestamp, 0, -1)";
                        command2.Parameters.AddWithValue("@userid", account.id);
                        command2.Parameters.AddWithValue("@title", ticketArray[0]);
                        command2.Parameters.AddWithValue("@prio", ticketArray[1]);
                        command2.Parameters.AddWithValue("@text", ticketArray[2]);
                        command2.Parameters.AddWithValue("@timestamp", Helper.UnixTimestamp());
                        command2.ExecuteNonQuery();

                        ticketId = (int)command2.LastInsertedId;

                        MySqlCommand command3 = General.Connection.CreateCommand();
                        command3.CommandText = "INSERT INTO ticket_user (ticketid, userid, timestamp) VALUES (@ticketid, @userid, @timestamp)";
                        command3.Parameters.AddWithValue("@ticketid", ticketId);
                        command3.Parameters.AddWithValue("@userid", account.id);
                        command3.Parameters.AddWithValue("@timestamp", Helper.UnixTimestamp());
                        command3.ExecuteNonQuery();

                        Helper.SendNotificationWithoutButton2(player, "Das Ticket wurde erfolgreich angelegt, in kürze wird sich ein Teammitglied bei dir melden!", "success", "center", 3500);

                        Helper.SendAdminMessage2($"{account.name} hat ein neues Ticket[{ticketId}] ingame erstellt - {ticketArray[0]}", 1, false);
                        Helper.DiscordWebhook(Settings._Settings.AdminNotificationWebHook, $"{account.name} hat ein neues Ticket[{ticketId}] ingame erstellt - {ticketArray[0]} | https://ucp.nemesus-world.de/showTicket/"+ticketId);
                        break;
                    }
            }
        }
    }
}
