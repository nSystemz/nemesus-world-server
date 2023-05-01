using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using NemesusWorld.Utils;

namespace NemesusWorld
{
    class DiscordBot
    {
        private static DiscordSocketClient _client;
        private static CommandService _commands;
        private static IServiceProvider _services;

        public DiscordBot() { }

        public static async Task RunBotAsync()
        {
            try
            {
                _client = new DiscordSocketClient();
                _commands = new CommandService();

                _services = new ServiceCollection()
                        .AddSingleton(_client)
                        .AddSingleton(_commands)
                        .BuildServiceProvider();

                string token = "MTAwMTU2OTc4MjQ1MTE0Njg4NA.GBX6c_.hvaz4nUdvPbYDjLRrqr5NpvBhyiRSR6eqywE7k";

                _client.Log += _client_Log;

                await RegisterCommandsAsync();

                await _client.LoginAsync(TokenType.Bot, token);

                await _client.StartAsync();

                await _client.SetGameAsync("mit 2 anderen auf Nemesus World!");

                await Task.Delay(-1);

                await changeUserCount();
            }
            catch (Exception) { }
        }

        public static async Task changeUserCount()
        {
            SocketVoiceChannel MemberCount = _client.GetGuild(972530426956644383).GetVoiceChannel(1001577205782040606);

            await MemberCount.ModifyAsync(prop => prop.Name = $"👥 Gameserver: 2");
        }

        private static Task _client_Log(LogMessage arg)
        {
            Helper.ConsoleLog("debug", arg.ToString());
            return Task.CompletedTask;
        }

        public static async Task RegisterCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
        }

        private static async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;
            var context = new SocketCommandContext(_client, message);
            if (message.Author.IsBot) return;

            int argPos = 0;
            if(message.HasStringPrefix("!", ref argPos))
            {
                var result = await _commands.ExecuteAsync(context, argPos, _services);
                if (!result.IsSuccess) Helper.ConsoleLog("error", result.ErrorReason, true);
            }

        }

    }
}
