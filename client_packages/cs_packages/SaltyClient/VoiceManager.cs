using System;
using System.Linq;
using System.Collections.Generic;
using RAGE.Elements;
using SaltyShared;

namespace SaltyClient
{
    public class VoiceManager : RAGE.Events.Script
    {
        #region Props/Fields

        public string ServerUniqueIdentifier { get; private set; }
        public string SoundPack { get; private set; }
        public ulong IngameChannel { get; private set; }
        public string IngameChannelPassword { get; private set; }
        public ulong[] SwissChannels { get; private set; }
        public string TeamSpeakName { get; private set; }
        public float VoiceRange { get; private set; } = SharedData.VoiceRanges[1];
        public string RadioChannel { get; private set; }

        private RAGE.Ui.HtmlWindow _htmlWindow = default;
        private bool _isConnected { get; set; }
        private bool _isIngame { get; set; }
        private DateTime _nextUpdate = DateTime.Now;
        private DateTime _lastTick = DateTime.Now;

        public VoiceClient[] VoiceClients => this._voiceClients.Values.ToArray();
        private Dictionary<ushort, VoiceClient> _voiceClients = new Dictionary<ushort, VoiceClient>();

        public TSVector[] RadioTowers { get; private set; }

        public bool IsEnabled { get; private set; } = false;
        public bool IsConnected => this._htmlWindow != default && this._isConnected;
        public bool IsReady => this.IsConnected && this._isIngame;

        public bool IsMicrophoneMuted { get; private set; }
        public bool IsMicrophoneEnabled { get; private set; }
        public bool IsSoundMuted { get; private set; }
        public bool IsSoundEnabled { get; private set; }
        #endregion

        #region CTOR
        public VoiceManager()
        {
            // RAGEMP Events
            RAGE.Events.Tick += this.OnTick;

            // Project Events
            RAGE.Events.Add(Event.SaltyChat_Initialize, this.OnInitialize);
            RAGE.Events.Add(Event.SaltyChat_UpdateClient, this.OnUpdateVoiceClient);
            RAGE.Events.Add("SaltyChat_InitToTalkClient", this.InitToTalkClient);

            RAGE.Events.Add(Event.SaltyChat_Disconnected, this.OnPlayerDisconnect);

            RAGE.Events.Add(Event.SaltyChat_SetVoiceRange, this.OnUpdateVoiceRange);
            RAGE.Events.Add(Event.SaltyChat_PlayerDied, this.OnPlayerDied);
            RAGE.Events.Add(Event.SaltyChat_PlayerRevived, this.OnPlayerRevived);

            RAGE.Events.Add(Event.SaltyChat_EstablishedCall, this.OnEstablishCall);
            RAGE.Events.Add(Event.SaltyChat_EstablishedCallRelayed, this.OnEstablishCallRelayed);
            RAGE.Events.Add(Event.SaltyChat_EndCall, this.OnEndCall);

            RAGE.Events.Add(Event.SaltyChat_SetRadioChannel, this.OnSetRadioChannel);
            RAGE.Events.Add(Event.SaltyChat_IsSending, this.OnPlayerIsSending);
            RAGE.Events.Add(Event.SaltyChat_IsSendingRelayed, this.OnPlayerIsSendingRelayed);
            RAGE.Events.Add(Event.SaltyChat_UpdateRadioTowers, this.OnUpdateRadioTowers);

            // Salty Chat Events
            RAGE.Events.Add("SaltyChat_OnConnected", this.OnPluginConnected);
            RAGE.Events.Add("SaltyChat_OnDisconnected", this.OnPluginDisconnected);
            RAGE.Events.Add("SaltyChat_OnMessage", this.OnPluginMessage);
            RAGE.Events.Add("SaltyChat_OnError", this.OnPluginError);
        }
        #endregion

        #region Events (Handling)
        /// <summary>
        /// Trigger if plugin should be initialized
        /// </summary>
        /// <param name="args">args[0] - teamSpeakName | args[1] - serverUniqueIdentifier | args[2] - soundPack | args[3] - channelId | args[4] - channelPassword | args[5] - swissChannels | args[6] - towerPositions</param>
        public void OnInitialize(object[] args)
        {
            this.TeamSpeakName = (string)args[0];
            this.ServerUniqueIdentifier = (string)args[1];
            this.SoundPack = (string)args[2];
            this.IngameChannel = Convert.ToUInt64((string)args[3]);
            this.IngameChannelPassword = (string)args[4];
            this.SwissChannels = Newtonsoft.Json.JsonConvert.DeserializeObject<ulong[]>((string)args[5]);
            this.RadioTowers = Newtonsoft.Json.JsonConvert.DeserializeObject<TSVector[]>((string)args[6]);

            this.IsEnabled = true;

            this._htmlWindow = new RAGE.Ui.HtmlWindow("package://Voice/SaltyWebSocket.html");
            this._htmlWindow.ExecuteJs("connect('127.0.0.1:38088')");
            this._htmlWindow.Active = false;
        }

        /// <summary>
        /// Update Voice Client
        /// </summary>
        /// <param name="args">args[0] - handle | args[1] - teamSpeakName | args[2] voiceRange</param>
        private void OnUpdateVoiceClient(object[] args)
        {
            ushort handle = Convert.ToUInt16(args[0]);
            string teamSpeakName = (string)args[1];
            float voiceRange = (float)args[2];
            Player player = Entities.Players.GetAtRemote(handle);

            if (player == null)
                return;

            lock (this._voiceClients)
            {
                if (this._voiceClients.TryGetValue(handle, out VoiceClient voiceClient))
                {
                    voiceClient.TeamSpeakName = teamSpeakName;
                    voiceClient.VoiceRange = voiceRange;
                }
                else
                {
                    this._voiceClients.Add(handle, new VoiceClient(player, teamSpeakName, voiceRange));
                }
            }
        }

        private void InitToTalkClient(object[] args)
        {
            ushort handle = Convert.ToUInt16(args[0]);
            Player player = Entities.Players.GetAtRemote(handle);

            if (player == null)
                return;

            RAGE.Events.CallRemote("SaltyChat_InitToTalk");
        }

        /// <summary>
        /// Remove a disconnected player
        /// </summary>
        /// <param name="args">args[0] - handle</param>
        private void OnPlayerDisconnect(object[] args)
        {
            ushort handle = Convert.ToUInt16(args[0]);

            lock (this._voiceClients)
            {
                if (this._voiceClients.TryGetValue(handle, out VoiceClient voiceClient))
                {
                    this._voiceClients.Remove(handle);

                    this.ExecuteCommand(
                        new PluginCommand(
                            Command.RemovePlayer,
                            this.ServerUniqueIdentifier,
                            new PlayerState(voiceClient.TeamSpeakName)
                        )
                    );
                }
            }
        }

        /// <summary>
        /// Update Voice Client
        /// </summary>
        /// <param name="args">args[0] - handle | args[1] voiceRange</param>
        private void OnUpdateVoiceRange(object[] args)
        {
            ushort handle = Convert.ToUInt16(args[0]);
            float voiceRange = (float)args[1];

            if (Player.LocalPlayer.RemoteId == handle)
            {
                this.VoiceRange = voiceRange;

                RAGE.Events.CallLocal("Client:SendNotificationWithoutButton", $"Voice-Range auf {voiceRange}m eingestellt!", "info", "top-left", 2000);
                RAGE.Events.CallLocal("Client:DrawMarkerWithTime", voiceRange);
            }
            else
            {
                if (this._voiceClients.TryGetValue(handle, out VoiceClient voiceClient))
                    voiceClient.VoiceRange = voiceRange;
            }
        }

        /// <summary>
        /// Tell plugin the player is dead, so we don't hear him anymore
        /// </summary>
        /// <param name="args">args[0] - handle</param>
        private void OnPlayerDied(object[] args)
        {
            ushort handle = Convert.ToUInt16(args[0]);

            Player player = Entities.Players.GetAtRemote(handle);

            if (player == null || !this.TryGetVoiceClient(handle, out VoiceClient voiceClient))
                return;

            voiceClient.IsAlive = false;
        }

        /// <summary>
        /// Tell plugin the player is alive again, se we can hear him
        /// </summary>
        /// <param name="args">[0] - handle</param>
        public void OnPlayerRevived(object[] args)
        {
            ushort handle = Convert.ToUInt16(args[0]);

            Player player = Entities.Players.GetAtRemote(handle);

            if (player == null || !this.TryGetVoiceClient(handle, out VoiceClient voiceClient))
                return;

            voiceClient.IsAlive = true;
        }
        #endregion

        #region Events (Phone)
        /// <summary>
        /// Tell the plugin we have a new call partner
        /// </summary>
        /// <param name="args">args[0] - handle</param>
        private void OnEstablishCall(object[] args)
        {
            ushort handle = Convert.ToUInt16(args[0]);

            Player player = Entities.Players.GetAtRemote(handle);

            if (player == null || !this.TryGetVoiceClient(handle, out VoiceClient voiceClient))
                return;

            RAGE.Vector3 ownPosition = Player.LocalPlayer.Position;
            RAGE.Vector3 playerPosition = player.Position;

            this.ExecuteCommand(
                new PluginCommand(
                    Command.PhoneCommunicationUpdate,
                    this.ServerUniqueIdentifier,
                    new PhoneCommunication(
                        voiceClient.TeamSpeakName,
                        RAGE.Game.Zone.GetZoneScumminess(RAGE.Game.Zone.GetZoneAtCoords(ownPosition.X, ownPosition.Y, ownPosition.Z)) +
                        RAGE.Game.Zone.GetZoneScumminess(RAGE.Game.Zone.GetZoneAtCoords(playerPosition.X, playerPosition.Y, playerPosition.Z))
                    )
                )
            );
        }

        /// <summary>
        /// Tell the plugin we have a new call partner
        /// </summary>
        /// <param name="args">args[0] - handle | args[1] - bool | args[2] - string[]</param>
        private void OnEstablishCallRelayed(object[] args)
        {
            ushort handle = Convert.ToUInt16(args[0]);
            bool direct = (bool)args[1];
            string[] relays = Newtonsoft.Json.JsonConvert.DeserializeObject<string[]>((string)args[2]);

            Player player = Entities.Players.GetAtRemote(handle);

            if (player == null || !this.TryGetVoiceClient(handle, out VoiceClient voiceClient))
                return;

            RAGE.Vector3 ownPosition = Player.LocalPlayer.Position;
            RAGE.Vector3 playerPosition = player.Position;

            this.ExecuteCommand(
                new PluginCommand(
                    Command.PhoneCommunicationUpdate,
                    this.ServerUniqueIdentifier,
                    new PhoneCommunication(
                        voiceClient.TeamSpeakName,
                        RAGE.Game.Zone.GetZoneScumminess(RAGE.Game.Zone.GetZoneAtCoords(ownPosition.X, ownPosition.Y, ownPosition.Z)) +
                        RAGE.Game.Zone.GetZoneScumminess(RAGE.Game.Zone.GetZoneAtCoords(playerPosition.X, playerPosition.Y, playerPosition.Z)),
                        direct,
                        relays
                    )
                )
            );
        }

        /// <summary>
        /// Tell the plugin to end the call
        /// </summary>
        /// <param name="args">args[0] - handle</param>
        private void OnEndCall(object[] args)
        {
            ushort handle = Convert.ToUInt16(args[0]);

            Player player = Entities.Players.GetAtRemote(handle);

            if (player == null || !this.TryGetVoiceClient(handle, out VoiceClient voiceClient))
                return;

            this.ExecuteCommand(
                new PluginCommand(
                    Command.StopPhoneCommunication,
                    this.ServerUniqueIdentifier,
                    new PhoneCommunication(
                        voiceClient.TeamSpeakName
                    )
                )
            );
        }
        #endregion

        #region Events (Phone)
        /// <summary>
        /// Sets players radio channel
        /// </summary>
        /// <param name="args">args[0] - radioChannel</param>
        private void OnSetRadioChannel(object[] args)
        {
            string radioChannel = (string)args[0];

            if (String.IsNullOrWhiteSpace(radioChannel))
            {
                this.RadioChannel = null;
                this.PlaySound("leaveRadioChannel", false, "radio");
            }
            else
            {
                this.RadioChannel = radioChannel;
                this.PlaySound("enterRadioChannel", false, "radio");
            }
        }

        /// <summary>
        /// When someone is talking on our radio channel
        /// </summary>
        /// <param name="args">args[0] - handle | args[1] - isOnRadio</param>
        private void OnPlayerIsSending(object[] args)
        {
            this.OnPlayerIsSendingRelayed(new object[] { args[0], args[1], args[2], true, "[]" });
        }

        /// <summary>
        /// When someone is talking on our radio channel
        /// </summary>
        /// <param name="args">args[0] - handle | args[1] - isOnRadio | args[2] - stateChange | args[3] - direct | args[4] - relays</param>
        private void OnPlayerIsSendingRelayed(object[] args)
        {
            ushort handle = Convert.ToUInt16(args[0]);
            bool isSending = (bool)args[1];
            bool stateChange = (bool)args[2];
            bool direct = (bool)args[3];
            string[] relays = Newtonsoft.Json.JsonConvert.DeserializeObject<string[]>((string)args[4]);

            string playerName = null;
            Player player = Entities.Players.GetAtRemote(handle);

            if (player == null)
                return;

            if (Player.LocalPlayer == player)
            {
                playerName = this.TeamSpeakName;
            }
            else if (this.TryGetVoiceClient(handle, out VoiceClient voiceClient))
            {
                playerName = voiceClient.TeamSpeakName;
            }

            if (playerName != null)
            {
                this.ExecuteCommand(
                    new PluginCommand(
                        isSending ? Command.RadioCommunicationUpdate : Command.StopRadioCommunication,
                        this.ServerUniqueIdentifier,
                        new RadioCommunication(
                            playerName,
                            RadioType.LongRange,
                            RadioType.LongRange,
                            stateChange,
                            direct,
                            false,
                            relays
                        )
                    )
                );
            }
        }

        /// <summary>
        /// Tell plugin where all radio towers are
        /// </summary>
        /// <param name="args">[0] - towerPositions</param>
        private void OnUpdateRadioTowers(object[] args)
        {
            this.RadioTowers = Newtonsoft.Json.JsonConvert.DeserializeObject<TSVector[]>((string)args[0]);

            this.ExecuteCommand(
                new PluginCommand(
                    Command.RadioTowerUpdate,
                    this.ServerUniqueIdentifier,
                    new RadioTower(
                        this.RadioTowers
                    )
                )
            );
        }
        #endregion

        #region Plugin Events
        /// <summary>
        /// Plugin connected to WebSocket
        /// </summary>
        /// <param name="args"></param>
        public void OnPluginConnected(object[] args)
        {
            this._isConnected = true;

            this.InitiatePlugin();
        }

        /// <summary>
        /// Plugin disconnected from WebSocket
        /// </summary>
        /// <param name="args"></param>
        public void OnPluginDisconnected(object[] args)
        {
            this._isConnected = false;

            // need that weird lastTick workaround, because tick is the only event that isn't fired after a disconnect and we don't want to reconnect to the plugin
            if (this._lastTick.AddSeconds(1) > DateTime.Now)
                this._htmlWindow.ExecuteJs("connect()");
        }

        /// <summary>
        /// Plugin state update
        /// </summary>
        /// <param name="args">[0] - <see cref="PluginCommand"/> as json</param>
        public void OnPluginMessage(object[] args)
        {
            PluginCommand pluginCommand = Newtonsoft.Json.JsonConvert.DeserializeObject<PluginCommand>((string)args[0]);

            if (pluginCommand.ServerUniqueIdentifier != this.ServerUniqueIdentifier)
                return;

            switch (pluginCommand.Command)
            {
                case Command.PluginState:
                    {
                        if (pluginCommand.TryGetPayload(out PluginState pluginState))
                        {
                            RAGE.Events.CallRemote(Event.SaltyChat_CheckVersion, pluginState.Version);

                            this.ExecuteCommand(
                                new PluginCommand(
                                    Command.RadioTowerUpdate,
                                    this.ServerUniqueIdentifier,
                                    new RadioTower(this.RadioTowers)
                                )
                            );
                        }

                        break;
                    }
                case Command.Reset:
                    {
                        // need that weird lastTick workaround, because tick is the only event that isn't fired after a disconnect and we want the plugin to time out
                        if (DateTime.Now > this._lastTick.AddSeconds(1))
                            break;

                        this._isIngame = false;

                        this.InitiatePlugin();

                        break;
                    }
                case Command.Ping:
                    {
                        // need that weird lastTick workaround, because tick is the only event that isn't fired after a disconnect and we want the plugin to time out
                        if (this._lastTick.AddSeconds(1) > DateTime.Now)
                            this.ExecuteCommand(new PluginCommand(this.ServerUniqueIdentifier));

                        break;
                    }
                case Command.InstanceState:
                    {
                        if (pluginCommand.TryGetPayload(out InstanceState instanceState))
                        {
                            this._isIngame = instanceState.IsReady;
                        }

                        break;
                    }
                case Command.SoundState:
                    {
                        if (pluginCommand.TryGetPayload(out SoundState soundState))
                        {
                            if (soundState.IsMicrophoneMuted != this.IsMicrophoneMuted)
                            {
                                this.IsMicrophoneMuted = soundState.IsMicrophoneMuted;

                                RAGE.Events.CallRemote(Event.SaltyChat_MicStateChanged, this.IsMicrophoneMuted);

                            }

                            if (soundState.IsMicrophoneEnabled != this.IsMicrophoneEnabled)
                            {
                                this.IsMicrophoneEnabled = soundState.IsMicrophoneEnabled;

                                RAGE.Events.CallRemote(Event.SaltyChat_MicEnabledChanged, this.IsMicrophoneEnabled);

                            }

                            if (soundState.IsSoundMuted != this.IsSoundMuted)
                            {
                                this.IsSoundMuted = soundState.IsSoundMuted;

                                RAGE.Events.CallRemote(Event.SaltyChat_SoundStateChanged, this.IsSoundMuted);
                            }

                            if (soundState.IsSoundEnabled != this.IsSoundEnabled)
                            {
                                this.IsSoundEnabled = soundState.IsSoundEnabled;

                                RAGE.Events.CallRemote(Event.SaltyChat_SoundEnabledChanged, this.IsSoundEnabled);

                            }
                        }

                        break;
                    }
                case Command.TalkState:
                    {
                        if (pluginCommand.TryGetPayload(out TalkState talkState))
                            this.SetPlayerTalking(talkState.Name, talkState.IsTalking);

                        break;
                    }
            }
        }

        /// <summary>
        /// Plugin error
        /// </summary>
        /// <param name="args">[0] - <see cref="PluginCommand"/> as json</param>
        public void OnPluginError(object[] args)
        {
            try
            {
                PluginError pluginError = Newtonsoft.Json.JsonConvert.DeserializeObject<PluginError>((string)args[0]);

                if (pluginError.Error == Error.AlreadyInGame)
                    this.InitiatePlugin(); // try again an hope that the game instance was reset on plugin side
                else
                    RAGE.Chat.Output($"Salty Chat -- Error: {pluginError.Error} - Message: {pluginError.Message}");
            }
            catch
            {
                RAGE.Chat.Output($"Salty Chat -- We got an error, but couldn't deserialize it...");
            }
        }
        #endregion

        #region Tick
        private void OnTick(List<RAGE.Events.TickNametagData> nametags)
        {
            RAGE.Game.Pad.DisableControlAction(1, (int)RAGE.Game.Control.EnterCheatCode, true);
            RAGE.Game.Pad.DisableControlAction(1, (int)RAGE.Game.Control.PushToTalk, true);

            this._lastTick = DateTime.Now;

            // Calculate player states
            if (this.IsReady && this._lastTick > this._nextUpdate)
            {
                this.PlayerStateUpdate();

                this._nextUpdate = DateTime.Now.AddMilliseconds(300);
            }

            // Lets the player talk on his radio channel with "N"
            if (!String.IsNullOrWhiteSpace(this.RadioChannel))
            {
                if (RAGE.Game.Pad.IsDisabledControlJustPressed(1, (int)RAGE.Game.Control.PushToTalk))
                {
                    RAGE.Events.CallLocal("Client:SendOnRadio", this.RadioChannel, true);
                }
                else if (RAGE.Game.Pad.IsDisabledControlJustReleased(1, (int)RAGE.Game.Control.PushToTalk))
                {
                    RAGE.Events.CallLocal("Client:SendOnRadio", this.RadioChannel, false);
                }
            }

            // Lets the player change his voice range with "^"
            if (RAGE.Game.Pad.IsDisabledControlJustPressed(1, (int)RAGE.Game.Control.EnterCheatCode))
            {
                this.ToggleVoiceRange();
            }
        }
        #endregion

        #region Methods (Proximity)
        private void SetPlayerTalking(string teamSpeakName, bool isTalking)
        {
            Player player = null;
            VoiceClient voiceClient = this.VoiceClients.FirstOrDefault(v => v.TeamSpeakName == teamSpeakName);

            if (voiceClient != null)
            {
                player = voiceClient.Player;
            }
            else if (teamSpeakName == this.TeamSpeakName)
            {
                player = Player.LocalPlayer;
            }

            if (player != null)
            {
                if (isTalking)
                {
                    player.PlayFacialAnim("mic_chatter", "mp_facial");
                    RAGE.Events.CallLocal("Client:SetTalkstate", 1);
                }
                else
                {
                    player.PlayFacialAnim("mood_normal_1", "facials@gen_male@variations@normal");
                    RAGE.Events.CallLocal("Client:SetTalkstate", 5);
                }
            }
        }

        /// <summary>
        /// Toggles voice range through <see cref="SharedData.VoiceRanges"/>
        /// </summary>
        public void ToggleVoiceRange()
        {
            int index = Array.IndexOf(SharedData.VoiceRanges, this.VoiceRange);

            if (index < 0)
            {
                RAGE.Events.CallRemote(Event.SaltyChat_SetVoiceRange, SharedData.VoiceRanges[1]);
            }
            else if (index + 1 >= SharedData.VoiceRanges.Length)
            {
                RAGE.Events.CallRemote(Event.SaltyChat_SetVoiceRange, SharedData.VoiceRanges[0]);
            }
            else
            {
                RAGE.Events.CallRemote(Event.SaltyChat_SetVoiceRange, SharedData.VoiceRanges[index + 1]);
            }
        }
        #endregion

        #region Methods (Plugin)
        /// <summary>
        /// Initiates the plugin
        /// </summary>
        private void InitiatePlugin()
        {
            if (String.IsNullOrWhiteSpace(this.TeamSpeakName))
                return;

            this.ExecuteCommand(
                new PluginCommand(
                    Command.Initiate,
                    new GameInstance(
                        this.ServerUniqueIdentifier,
                        this.TeamSpeakName,
                        this.IngameChannel,
                        String.IsNullOrEmpty(this.IngameChannelPassword) ? String.Empty : this.IngameChannelPassword,
                        this.SoundPack,
                        this.SwissChannels
                    )
                )
            );
        }

        /// <summary>
        /// Plays a file from soundpack specified in <see cref="VoiceManager.SoundPack"/>
        /// </summary>
        /// <param name="fileName">filename (without .wav) of the soundfile</param>
        /// <param name="loop">use <see cref="true"/> to let the plugin loop the sound</param>
        /// <param name="handle">use your own handle instead of the filename, so you can play the sound multiple times</param>
        public void PlaySound(string fileName, bool loop = false, string handle = null)
        {
            if (String.IsNullOrWhiteSpace(handle))
                handle = fileName;

            this.ExecuteCommand(
                new PluginCommand(
                    Command.PlaySound,
                    this.ServerUniqueIdentifier,
                    new Sound(
                        fileName,
                        loop,
                        handle
                    )
                )
            );
        }

        /// <summary>
        /// Stops and dispose the sound
        /// </summary>
        /// <param name="handle">filename or handle of the sound</param>
        public void StopSound(string handle)
        {
            this.ExecuteCommand(
                new PluginCommand(
                    Command.StopSound,
                    this.ServerUniqueIdentifier,
                    new Sound(handle)
                )
            );
        }

        /// <summary>
        /// Sends the plugin an update on all players
        /// </summary>
        private void PlayerStateUpdate()
        {
            RAGE.Vector3 playerPosition = Player.LocalPlayer.Position;

            foreach (var voiceClient in this.VoiceClients)
            {
                RAGE.Vector3 nPlayerPosition = voiceClient.Player.Position;

                this.ExecuteCommand(
                    new PluginCommand(
                        Command.PlayerStateUpdate,
                        this.ServerUniqueIdentifier,
                        new PlayerState(
                            voiceClient.TeamSpeakName,
                            nPlayerPosition,
                            voiceClient.VoiceRange,
                            voiceClient.IsAlive
                        )
                    )
                );
            }

            this.ExecuteCommand(
                new PluginCommand(
                    Command.SelfStateUpdate,
                    this.ServerUniqueIdentifier,
                    new SelfState(
                        playerPosition,
                        RAGE.Game.Cam.GetGameplayCamRot(0).Z
                    )
                )
            );
        }
        #endregion

        #region Helper
        private bool TryGetVoiceClient(ushort handle, out VoiceClient voiceClient)
        {
            lock (this._voiceClients)
            {
                if (this._voiceClients.TryGetValue(handle, out voiceClient))
                    return true;
            }

            voiceClient = null;
            return false;
        }

        private void ExecuteCommand(PluginCommand pluginCommand)
        {
            if (!this.IsEnabled || !this.IsConnected || pluginCommand == default)
                return;

            this._htmlWindow.ExecuteJs($"runCommand('{pluginCommand.Serialize()}')");
        }
        #endregion
    }
}
