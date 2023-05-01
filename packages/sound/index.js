mp.events.add({
    'soundState': (player, state, soundID) => {
        mp.players.call(state, [soundID]);
    },
    'changeSoundVolume': (player, listenerID, soundID, volume) => {
        let listener = mp.players.at(listenerID);
        listener.call('setSoundVolume', [soundID, volume]);
    },
    'addListener': (player, listenerID, soundData) => {
        let listener = mp.players.at(listenerID);
        listener.call('createSound', [soundData]);
    },
    'removeListener': (player, listenerID, soundID) => {
        let listener = mp.players.at(listenerID);
        listener.call('destroySound', [soundID]);
    },
    'soundPosition': (player, soundID, pos) => {
        mp.players.call('soundPosition', [soundID, pos]);
    },
    'soundRange': (player, soundID, range) => {
        mp.players.call('soundRange', [soundID, range]);
    },
    'soundError': (player, soundID, err) => {
        err = JSON.parse(err);
    },
    'playerQuit': (player) => {
        mp.players.call('destroyHostSound', player.id)
    },
    'playerDeath': (player) => {
        mp.players.call('destroyHostSound', player.id)
    }
});
