//Animation preload
mp.game.streaming.requestAnimDict("anim@mp_player_intmenu@key_fob@");
mp.game.streaming.requestAnimDict("missminuteman_1ig_2");
mp.game.streaming.requestAnimDict("mp_car_bomb");

//Animationsync
mp.events.add('entityStreamIn', (entity) => {
    if ((entity.type == 'player' && mp.players.exists(entity) || (entity.type == 'ped' && mp.peds.exists(entity)))  && entity.remoteId != mp.players.local.remoteId && entity.dimension == mp.players.local.dimension) {
        if (entity.hasVariable("Player:AnimData")) {
            value = entity.getVariable("Player:AnimData");
            if (value != "0") {
                if (value == "WORLD_HUMAN_CLIPBOARD" || value == "PROP_HUMAN_BUM_BIN" || value == "WORLD_HUMAN_AA_SMOKE" || value == "WORLD_HUMAN_STAND_FISHING" || value == "WORLD_HUMAN_WELDING" || value == "CODE_HUMAN_MEDIC_TEND_TO_DEAD" || value == "WORLD_HUMAN_SEAT_WALL_TABLET" || value == "WORLD_HUMAN_GARDENER_PLANT") {
                    setTimeout(function () {
                        mp.game.invoke("0x142A02425FF02BD9", entity.handle, value, -1, false)
                    }, 215)
                } else {
                    let anim = value.split("%");
                    mp.game.streaming.requestAnimDict(anim[0]);
                    setTimeout(function () {
                        entity.taskPlayAnim(anim[0], anim[1], 3, 3, -1, parseInt(anim[2]), 1, false, false, false);
                    }, 215)
                }
            }
        }
    }
});

//Animations
mp.events.addDataHandler("Player:AnimData", function (a, b) {
    if ((!mp.players.exists(a) && !mp.peds.exists(a))) return;
    if (0 !== a.handle && a.hasVariable("Player:AnimData"))
        if (b != "0") {
            if (b == "WORLD_HUMAN_CLIPBOARD" || b == "PROP_HUMAN_BUM_BIN" || b == "WORLD_HUMAN_AA_SMOKE" || b == "WORLD_HUMAN_STAND_FISHING" || b == "WORLD_HUMAN_WELDING" || b == "CODE_HUMAN_MEDIC_TEND_TO_DEAD" || b == "WORLD_HUMAN_SEAT_WALL_TABLET" || b == "WORLD_HUMAN_GARDENER_PLANT") {
                setTimeout(function () {
                    mp.game.invoke("0x142A02425FF02BD9", a.handle, b, -1, false)
                }, 150)
            } else {
                c = b.split("%");
                mp.game.streaming.requestAnimDict(c[0]);
                setTimeout(function () {
                    a.taskPlayAnim(c[0], c[1], 3, 3, -1, parseInt(c[2]), 1, false, false, false);
                }, 150)
            }
        }
});