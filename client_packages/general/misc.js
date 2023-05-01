//Activate controls
mp.events.add("Client:ActivateControls", () => {
    mp.gui.cursor.show(false, false);
    mp.game.ui.displayHud(true);
    mp.gui.chat.show(true);
    mp.game.ui.displayRadar(true);
})

//Freeze
mp.events.add("Client:PlayerFreeze", (toogle) => {
    mp.players.local.freezePosition(toogle);
})

//Radio
let radioKillTimer = null;
mp.game.audio.setUserRadioControlEnabled(false);
mp.events.add("Client:RadioOff", () => {
    setTimeout(function () { 
      mp.players.local.setConfigFlag(429, true); // Animations problems for cars
    },125);
    mp.game.cam.renderScriptCams(false, false, 0, true, false);
    mp.players.local.setConfigFlag(32, true); //Seatbelt
    if (radioKillTimer == null) {
        radioKillTimer = setInterval(() => {
            if (mp.players.local.isSittingInAnyVehicle()) {
                mp.game.audio.setRadioToStationName("OFF");
                clearInterval(radioKillTimer);
                radioKillTimer = null;
                return;
            }
        }, 205);
    }
});

//Misc
mp.game.gameplay.disableAutomaticRespawn(true);
mp.game.gameplay.setFadeInAfterDeathArrest(false);
mp.game.gameplay.setFadeInAfterLoad(false);
mp.game.gameplay.setFadeOutAfterArrest(false);
mp.game.gameplay.setFadeOutAfterDeath(false);