using System;
using System.Collections.Generic;
using System.Text;

namespace SaltyShared
{
    public static class Event
    {
        #region Plugin
        public const string SaltyChat_Initialize = "SaltyChat_Initialize";
        public const string SaltyChat_CheckVersion = "SaltyChat_CheckVersion";
        public const string SaltyChat_UpdateClient = "SaltyChat_UpdateClient";
        public const string SaltyChat_Disconnected = "SaltyChat_Disconnected";
        #endregion

        #region State Change
        public const string SaltyChat_TalkStateChanged = "SaltyChat_TalkStateChanged";
        public const string SaltyChat_MicStateChanged = "SaltyChat_MicStateChanged";
        public const string SaltyChat_MicEnabledChanged = "SaltyChat_MicEnabledChanged";
        public const string SaltyChat_SoundStateChanged = "SaltyChat_SoundStateChanged";
        public const string SaltyChat_SoundEnabledChanged = "SaltyChat_SoundEnabledChanged";
        #endregion

        #region Player State
        public const string SaltyChat_PlayerDied = "SaltyChat_PlayerDied";
        public const string SaltyChat_PlayerRevived = "SaltyChat_PlayerRevived";
        #endregion

        #region Proximity
        public const string SaltyChat_SetVoiceRange = "SaltyChat_SetVoiceRange";
        #endregion

        #region Phone
        public const string SaltyChat_EstablishedCall = "SaltyChat_EstablishedCall";
        public const string SaltyChat_EstablishedCallRelayed = "SaltyChat_EstablishedCallRelayed";
        public const string SaltyChat_EndCall = "SaltyChat_EndCall";
        #endregion

        #region Radio
        public const string SaltyChat_SetRadioChannel = "SaltyChat_SetRadioChannel";
        public const string SaltyChat_IsSending = "SaltyChat_IsSending";
        public const string SaltyChat_IsSendingRelayed = "SaltyChat_IsSendingRelayed";
        public const string SaltyChat_UpdateRadioTowers = "SaltyChat_UpdateRadioTowers";
        #endregion

        #region Megaphone
        public const string SaltyChat_IsUsingMegaphone = "SaltyChat_IsUsingMegaphone ";
        #endregion
    }
}
