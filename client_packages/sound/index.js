const maxRange = 60.0;
const localPlayer = mp.players.local;
let musicPlayer = mp.browsers.new('package://sound/musicPlayer/index.html');
let id = 0;
let activeSounds = {},
    hostSounds = {},
    soundInterval;
let playedSound = null;
let playedSoundStatus = 0;
let playedSoundRange = 5;

const soundManager = {
    add: function (player, id) {
        if (player && hostSounds[id]) {
            hostSounds[id].listeners.push(player);
            if (localPlayer.handle !== player.handle) return mp.events.callRemote('addListener', player.remoteId, JSON.stringify(hostSounds[id]));
            if (musicPlayer) musicPlayer.execute(`playAudio("${id}", "${hostSounds[id].url}", "${hostSounds[id].volume}")`);
        }
    },
    remove: function (player, id) {
        if (player) {
            if (hostSounds[id]) {
                let idx = hostSounds[id].listeners.indexOf(player);
                if (idx !== -1) {
                    hostSounds[id].listeners.splice(idx, 1);
                    if (localPlayer.handle !== player.handle) return mp.events.callRemote('removeListener', player.remoteId, id);
                }
            }
            if (musicPlayer) {
                if (activeSounds[id]) activeSounds[id] = null;
                musicPlayer.execute(`stopAudio("${id}")`);
            }
        }
    },
    setVolume: function (player, id, volume) {
        if (player) {
            if (hostSounds[id] && localPlayer.handle !== player.handle) return mp.events.callRemote('changeSoundVolume', player.remoteId, id, volume);
            if (musicPlayer) musicPlayer.execute(`setVolume("${id}", "${volume}")`);
        }
    },
    pauseToggle: function (player, id, pause) {
        if (player) {
            if (hostSounds[id]) {
                hostSounds[id].paused = pause;
                if (localPlayer.handle !== player.handle) return mp.events.callRemote('pauseToggleSound', player.remoteId, id, pause);
            }
            if (musicPlayer) {
                if (activeSounds[id]) activeSounds[id].paused = pause;
                musicPlayer.execute(`setPaused("${id}", "${pause}")`);
            }
        }
    }
}

mp.events.add({
    'createSound': (soundObj) => {
        soundObj = JSON.parse(soundObj);
        id = soundObj.id;
        if (!activeSounds[soundObj.id] && !hostSounds[soundObj.id]) {
            activeSounds[soundObj.id] = {
                id: soundObj.id,
                url: soundObj.url,
                pos: soundObj.pos,
                volume: soundObj.volume,
                range: soundObj.range,
                dimension: soundObj.dimension,
                listeners: soundObj.listeners,
                host: soundObj.host, // Possibility of host change in future maybe
                paused: soundObj.paused
            }
            if (musicPlayer) musicPlayer.execute(`playAudio("${soundObj.id}", "${activeSounds[soundObj.id].url}", "${activeSounds[soundObj.id].volume}")`);
        }
    },
    /*
    'soundPosition': (id, pos) => { // Could be used in future for host changing sync.
        pos = JSON.parse(pos);
        if (activeSounds[id]) {
            activeSounds[id].pos = pos;
        }
    },
    'soundRange': (id, range) => { // Could be used in future for host changing sync.
        if (activeSounds[id]) {
            activeSounds[id].range = range;
        }
    },
    */
    'setSoundVolume': (id, volume) => {
        soundManager.setVolume(localPlayer, id, volume);
    },
    'destroySound': (soundID) => {
        if (activeSounds[soundID]) {
            soundManager.remove(localPlayer, soundID);
            activeSounds[soundID] = null;
        } else if (hostSounds[soundID]) {
            hostSounds[soundID].listeners.forEach(listener => {
                soundManager.remove(listener, soundID);
            });
            hostSounds[soundID] = null;
        }
        if (Object.keys(hostSounds).length < 1) {
            if (soundInterval) clearInterval(soundInterval);
        }
    },
    'destroySoundHost': (hostID) => {
        Object.keys(activeSounds).forEach(soundID => {
            if (hostID == activeSounds[soundID].host) {
                soundManager.remove(localPlayer, soundID);
            }
        });
    },
    'pauseSound': (soundID) => {
        soundManager.pauseToggle(localPlayer, id, true);
    },
    'resumeSound': (soundID) => {
        soundManager.pauseToggle(localPlayer, id, false);
    },
    'audioFinish': (soundID) => {
        mp.events.call('destroySound', soundID);
        mp.events.callRemote('soundFinish', soundID);
    },
    'audioError': (soundID, err) => {
        mp.events.call('destroySound', soundID);
        mp.events.callRemote('soundError', soundID, err);
    }
});

mp.game.audio.playSound3D = function (url, pos, range = maxRange, volume = 1, dimension = 0) {
    id += 1;
    hostSounds[id] = {
        id: id,
        url: url,
        pos: pos,
        volume: volume,
        range: range,
        dimension: dimension,
        listeners: [],
        host: localPlayer.remoteId,
        paused: false,
    };
    hostSounds[id].destroy = function () {
        return mp.events.callRemote('soundState', 'destroySound', this.id);
    };
    hostSounds[id].pause = function () {
        return mp.events.callRemote('soundState', 'pauseSound', this.id);
    };
    hostSounds[id].resume = function () {
        return mp.events.callRemote('soundState', 'resumeSound', this.id)
    };
    if (!soundInterval) initSoundInterval();
    return hostSounds[id];
};

mp.game.audio.setSoundVolume = function (id, volume = 1) {
    if (hostSounds[id]) {
        hostSounds[id].volume = volume;
    }
};

mp.game.audio.setSoundPosition = function (id, pos) {
    if (hostSounds[id]) {
        hostSounds[id].pos = pos;
        // mp.events.callRemote('soundPosition', id, JSON.stringify(pos)); // Could be used in future for host changing sync.
    }
}

mp.game.audio.setSoundRange = function (id, range) {
    if (range < 0 || range > maxRange) range = maxRange;
    hostSounds[id].range = range;
    // mp.events.callRemote('soundRange', id, range); // Could be used in future for host changing sync.
}

function initSoundInterval() {
    soundInterval = setInterval(() => {
        Object.keys(hostSounds).forEach(sound => {
            if (hostSounds[sound] && !hostSounds[sound].paused) {
                const soundPosition = hostSounds[sound].pos;
                const maxRange = hostSounds[sound].range;
                mp.players.forEach(player => { // I wish there was inRange like in server-side.
                    if (player && player.dimension === hostSounds[sound].dimension && !hostSounds[sound].listeners.includes(player)) {
                        const playerPos = player.position;
                        let dist = mp.game.system.vdist(playerPos.x, playerPos.y, playerPos.z, soundPosition.x, soundPosition.y, soundPosition.z);
                        if (dist <= maxRange) {
                            soundManager.add(player, sound);
                        }
                    }
                });

                if (hostSounds[sound].listeners && hostSounds[sound].listeners.length > 0)
                    hostSounds[sound].listeners.forEach(player => {
                        if (player) {
                            const playerPos = player.position;
                            let dist = mp.game.system.vdist(playerPos.x, playerPos.y, playerPos.z, soundPosition.x, soundPosition.y, soundPosition.z);
                            if (dist > maxRange || hostSounds[sound].dimension !== player.dimension) {
                                soundManager.remove(player, sound);
                            } else {
                                let volume = (hostSounds[sound].volume - (dist / hostSounds[sound].range)).toFixed(2); // Credits to George....
                                soundManager.setVolume(player, sound, volume);
                            }
                        }
                    });
            }
        });
    }, 500);
};

//Sound
mp.events.add("Client:SetSoundRange", (range) => {
    playedSoundRange = range;
});

mp.events.add("Client:Play3DSound", (url, status) => {
    if (status == -1) {
        if (playedSound == null) {
            mp.events.callRemote('Server:AbortSound');
        }
    }
    else if (status == -2) {
        let pos = localPlayer.position;
        playedSoundRange = 20;
        playedSound = mp.game.audio.playSound3D(url, pos, playedSoundRange, 0.175);
    }
    else if (status == -3) {
        if (playedSound != null) {
            playedSound.destroy();
            playedSound = null;
        }
    }
    else if (status == 0) {
        if (playedSound != null) {
            mp.events.call("Client:SendNotificationWithoutButton", 'Du spielst schon Musik ab!', 'error', 'top-left', 2500);
            return;
        }
        // eslint-disable-next-line no-undef
        mp.events.call("Client:SendNotificationWithoutButton", 'Musik gestartet!', 'success', 'top-left', 2500);
        let pos = localPlayer.position;
        playedSound = mp.game.audio.playSound3D(url, pos, playedSoundRange, 0.175);
    } else if (status == 1) {
        if (playedSound != null) {
            if (playedSoundStatus == 0) {
                // eslint-disable-next-line no-undef
                mp.events.call("Client:SendNotificationWithoutButton", 'Musik pausiert!', 'success', 'top-left', 2500);
                playedSound.pause();
                playedSoundStatus = 1;
            } else {
                // eslint-disable-next-line no-undef
                mp.events.call("Client:SendNotificationWithoutButton", 'Musik gestartet!', 'success', 'top-left', 2500);
                playedSound.resume();
                playedSoundStatus = 0;
            }
        } else {
            mp.events.call("Client:SendNotificationWithoutButton", 'Du spielst keine Musik ab!', 'error', 'top-left', 2500);
        }
    } else if (status == 2) {
        if (playedSound != null) {
            playedSound.destroy();
            playedSound = null;
            mp.events.call("Client:SendNotificationWithoutButton", 'Musik beendet!', 'success', 'top-left', 2500);
        } else {
            mp.events.call("Client:SendNotificationWithoutButton", 'Du spielst keine Musik ab!', 'error', 'top-left', 2500);
        }
    }
})

mp.events.add("playerQuit", playerQuitHandler);

function playerQuitHandler(player, exitType, reason) {
    if (mp.players.local == player) {
        if (playedSound != null) {
            playedSound.pause();
            playedSound.destroy();
            playedSound = null;
            mp.events.callRemote('Server:AbortSound');
        }
    }
}

mp.events.add("playerDeath", (player, reason, killer) => {
    if (mp.players.local == player) {
        if (playedSound != null) {
            playedSound.pause();
            playedSound.destroy();
            playedSound = null;
            mp.events.callRemote('Server:AbortSound');
        }
    }
})