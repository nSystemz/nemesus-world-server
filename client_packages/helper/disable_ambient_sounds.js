function disableAmbientSounds () {
    mp.game.audio.setAmbientZoneListStatePersistent("AZL_DLC_Hei4_Island_Disabled_Zones", false, true);
    mp.game.audio.setAmbientZoneListStatePersistent("AZL_DLC_Hei4_Island_Zones", true, true);
    mp.game.audio.clearAmbientZoneState("AZ_COUNTRYSIDE_PRISON_01_ANNOUNCER_GENERAL", false);
    mp.game.audio.clearAmbientZoneState("AZ_COUNTRYSIDE_PRISON_01_ANNOUNCER_WARNING", false);
    mp.game.audio.clearAmbientZoneState("AZ_COUNTRYSIDE_PRISON_01_ANNOUNCER_ALARM", false);
    mp.game.audio.setAmbientZoneState("", false, false);
    mp.game.audio.setAudioFlag("DisableFlightMusic", true);
    mp.game.audio.setAudioFlag("PoliceScannerDisabled", true);
    mp.game.audio.setFlag("LoadMPData", true);
    mp.game.audio.setFlag("DisableFlightMusic", true);
    mp.game.water.setDeepOceanScaler(0.0);
    mp.game.misc.setRandomEventFlag(false);
    mp.game.task.setScenarioTypeEnabled("WORLD_VEHICLE_BIKE_OFF_ROAD_RACE", false);
    mp.game.task.setScenarioTypeEnabled("WORLD_VEHICLE_BUSINESSMEN", false);
    mp.game.task.setScenarioTypeEnabled("WORLD_VEHICLE_EMPTY", false);
    mp.game.task.setScenarioTypeEnabled("WORLD_VEHICLE_MECHANIC", false);
    mp.game.task.setScenarioTypeEnabled("WORLD_VEHICLE_MILITARY_PLANES_BIG", false);
    mp.game.task.setScenarioTypeEnabled("WORLD_VEHICLE_MILITARY_PLANES_SMALL", false);
    mp.game.task.setScenarioTypeEnabled("WORLD_VEHICLE_POLICE_BIKE", false);
    mp.game.task.setScenarioTypeEnabled("WORLD_VEHICLE_POLICE_CAR", false);
    mp.game.task.setScenarioTypeEnabled("WORLD_VEHICLE_POLICE_NEXT_TO_CAR", false);
    mp.game.task.setScenarioTypeEnabled("WORLD_VEHICLE_SALTON_DIRT_BIKE", false);
    mp.game.task.setScenarioTypeEnabled("WORLD_VEHICLE_SALTON", false);
    mp.game.task.setScenarioTypeEnabled("WORLD_VEHICLE_STREETRACE", false);
    mp.game.audio.setStaticEmitterEnabled("LOS_SANTOS_VANILLA_UNICORN_01_STAGE", false);
    mp.game.audio.setStaticEmitterEnabled("LOS_SANTOS_VANILLA_UNICORN_02_MAIN_ROOM", false);
    mp.game.audio.setStaticEmitterEnabled("LOS_SANTOS_VANILLA_UNICORN_03_BACK_ROOM", false);
    mp.game.audio.setStaticEmitterEnabled("se_dlc_aw_arena_construction_01", false);
    mp.game.audio.setStaticEmitterEnabled("se_dlc_aw_arena_crowd_background_main", false);
    mp.game.audio.setStaticEmitterEnabled("se_dlc_aw_arena_crowd_exterior_lobby", false);
    mp.game.audio.setStaticEmitterEnabled("se_dlc_aw_arena_crowd_interior_lobby", false);
    mp.game.audio.startAudioScene("CHARACTER_CHANGE_IN_SKY_SCENE");
    mp.game.audio.startAudioScene("DLC_MPHEIST_TRANSITION_TO_APT_FADE_IN_RADIO_SCENE");
    mp.game.audio.startAudioScene("FBI_HEIST_H5_MUTE_AMBIENCE_SCENE");
}

mp.events.add("playerReady", disableAmbientSounds);