// Keybinds
const keybindsVK = {
    Stage: 81, // Light stage modifier
    Tone1: 49, // Siren tone 1
    Tone2: 50, // Siren tone 2
    Tone3: 51, // Siren tone 3
    Tone4: 52, // Siren tone 4
    Tone5: 53, // Random siren tone every 5 seconds
}
const keybindsControls = {
    Horn: [13, 51] // Horn
}

// Siren tones
const sirenTones = {
    tone1: 'VEHICLES_HORNS_SIREN_1', // Siren tone 1
    tone2: 'VEHICLES_HORNS_SIREN_2', // Siren tone 2
    tone3: 'VEHICLES_HORNS_POLICE_WARNING', // Siren tone 3
    tone4: 'VEHICLES_HORNS_AMBULANCE_WARNING', // Siren tone 4
    aux: 'VEHICLES_HORNS_SIREN_1', // Auxiliary Siren
    horn: 'SIRENS_AIRHORN' // Horn
}

// Default timeout to prevent key spamming
const keyTimeout = 350 // Milliseconds

// Handle VK clicks
var wasSent = false
mp.events.add('render', () => {
    for (const key in keybindsVK) {
        if (mp.keys.isDown(keybindsVK[key]) === true && mp.keys.isDown(17) === false) {
            if (wasSent == false) {
                mp.events.call('input' + key)
                wasSent = true
                setTimeout(() => {
                    wasSent = false
                }, keyTimeout)
            }
        } else if (mp.keys.isDown(keybindsVK[key]) === true && mp.keys.isDown(17) === true) {
            if (wasSent == false) {
                mp.events.call('inputWithModif' + key)
                wasSent = true
                setTimeout(() => {
                    wasSent = false
                }, keyTimeout)
            }
        }
    }
})

// Handle Contorl clicks
mp.events.add('render', () => {
    for (const key in keybindsControls) {
        if (mp.game.controls.isControlJustPressed(keybindsControls[key][0], keybindsControls[key][1]))
            mp.events.call('input' + key)
    }
})

// Disable default behaviour of sirens and horns for emergency vehicles
mp.events.add('render', _ => {
    if (mp.players.local.vehicle && mp.players.local.vehicle.getClass() == 18 && mp.game.controls.isControlJustReleased(13, 51)) {
        let entity = mp.players.local.vehicle
        mp.events.callRemote('syncHorn', entity, false, "", entity.hornID)
        entity.hornOnce = false
    }

    if (mp.players.local.vehicle && mp.players.local.vehicle.getClass() == 18) {
        mp.game.controls.disableControlAction(27, 86, true)
    }
})

// Sync lights to other players
mp.events.add('syncLight', (vehicle, siren, sound, code, sirenCode) => {
    vehicle.setSiren(siren)
    vehicle.setSirenSound(sound)
    vehicle.currentCode = code
    vehicle.currentSiren = sirenCode
})

// Sync sirens to other players
mp.events.add('syncSiren', (vehicle, playing, sound, id) => {
    if (!playing) {
        mp.game.invoke('0xA3B0C41BA5CC0BB5', id)
    } else {
        mp.game.audio.playSoundFromEntity(id, sound, vehicle.handle, '', true, 0)
    }
})

// Sync horn to other players
mp.events.add('syncHorn', (vehicle, playing, sound, id) => {
    if (!playing) {
        mp.game.invoke('0xA3B0C41BA5CC0BB5', id)
    } else {
        mp.game.audio.playSoundFromEntity(id, sound, vehicle.handle, '', true, 0)
    }
})

mp.events.add('inputStage', _ => {
    let entity = mp.players.local.vehicle
    if (entity && entity.type === 'vehicle' && entity.getClass() === 18) {

        entity.currentCode += 1
        if (!entity.currentCode) {
            entity.currentCode = 2
        }
        if (entity.currentCode > 3) {
            entity.currentCode = 3
        }
        switch (entity.currentCode) {
            case 1: {
                mp.events.callRemote('syncSiren', entity, false, '', entity.sirenID)
                mp.events.callRemote('syncSiren', entity, false, '', entity.auxSirenID)
                clearInterval(entity.scanSiren)
                entity.scanSiren = null
                mp.events.callRemote('syncLight', entity, false, false, entity.currentCode, 0)
                break
            }
            case 2: {
                mp.events.callRemote('syncSiren', entity, false, '', entity.sirenID)
                mp.events.callRemote('syncSiren', entity, false, '', entity.auxSirenID)
                clearInterval(entity.scanSiren)
                entity.scanSiren = null
                mp.events.callRemote('syncLight', entity, true, true, entity.currentCode, 0)
                break
            }
            case 3: {
                mp.events.callRemote('syncLight', entity, true, true, entity.currentCode, 0)
                break
            }
        }
    }
})

mp.events.add('inputHorn', _ => {
    if (mp.players.local.vehicle && mp.players.local.vehicle.getClass() == 18) {
        let entity = mp.players.local.vehicle
        if (entity.hornID == null) {
            let ID = mp.game.invoke('0x430386FE9BF80B45')
            entity.hornID = ID
        }
        if (entity.hornOnce != true || entity.hornOnce != false) {
            entity.hornOnce = false
        }
        if (!entity.hornOnce) {
            mp.events.callRemote('syncHorn', entity, true, sirenTones.horn, entity.hornID)
            entity.hornOnce = true
        }
    }
})

mp.events.add('inputWithModifStage', _ => {
    let entity = mp.players.local.vehicle
    if (entity && entity.type === 'vehicle' && entity.getClass() === 18) {

        entity.currentCode -= 1
        if (!entity.currentCode) {
            entity.currentCode = 0
        }
        if (entity.currentCode < 0) {
            entity.currentCode = 0
        }
        switch (entity.currentCode) {
            case 1: {
                mp.events.callRemote('syncSiren', entity, false, '', entity.sirenID)
                mp.events.callRemote('syncSiren', entity, false, '', entity.auxSirenID)
                clearInterval(entity.scanSiren)
                entity.scanSiren = null
                mp.events.callRemote('syncLight', entity, false, false, entity.currentCode, 0)
                break
            }
            case 2: {
                mp.events.callRemote('syncSiren', entity, false, '', entity.sirenID)
                mp.events.callRemote('syncSiren', entity, false, '', entity.auxSirenID)
                clearInterval(entity.scanSiren)
                entity.scanSiren = null
                mp.events.callRemote('syncLight', entity, true, true, entity.currentCode, 0)
                break
            }
            case 3: {
                mp.events.callRemote('syncLight', entity, true, true, entity.currentCode, 0)
                break
            }
        }
    }
})

mp.events.add('inputTone1', _ => {
    let entity = mp.players.local.vehicle
    if (entity && entity.type === 'vehicle' && entity.getClass() === 18) {

        if (entity.currentCode >= 3) {
            if (!entity.currentSiren) {
                entity.currentSiren = 0
            }
            if (entity.currentSiren == 1) {
                entity.currentSiren = 0
                mp.events.callRemote('syncSiren', entity, false, '', entity.sirenID)
                clearInterval(entity.scanSiren)
                entity.scanSiren = null
            } else {
                entity.currentSiren = 1
                if (entity.sirenID == null) {
                    let ID = mp.game.invoke('0x430386FE9BF80B45')
                    entity.sirenID = ID
                }
                mp.events.callRemote('syncSiren', entity, false, '', entity.sirenID)
                clearInterval(entity.scanSiren)
                entity.scanSiren = null
                mp.events.callRemote('syncSiren', entity, true, sirenTones.tone1, entity.sirenID)
            }
        }
    }
})

mp.events.add('inputTone2', _ => {
    let entity = mp.players.local.vehicle
    if (entity && entity.type === 'vehicle' && entity.getClass() === 18) {

        if (entity.currentCode >= 3) {
            if (!entity.currentSiren) {
                entity.currentSiren = 0
            }
            if (entity.currentSiren == 2) {
                entity.currentSiren = 0
                mp.events.callRemote('syncSiren', entity, false, '', entity.sirenID)
                clearInterval(entity.scanSiren)
                entity.scanSiren = null
            } else {
                entity.currentSiren = 2
                if (entity.sirenID == null) {
                    let ID = mp.game.invoke('0x430386FE9BF80B45')
                    entity.sirenID = ID
                }
                mp.events.callRemote('syncSiren', entity, false, '', entity.sirenID)
                clearInterval(entity.scanSiren)
                entity.scanSiren = null
                mp.events.callRemote('syncSiren', entity, true, sirenTones.tone2, entity.sirenID)
            }
        }
    }
})

mp.events.add('inputTone3', _ => {
    let entity = mp.players.local.vehicle
    if (entity && entity.type === 'vehicle' && entity.getClass() === 18) {
        if (entity.currentCode >= 3) {
            if (!entity.currentSiren) {
                entity.currentSiren = 0
            }
            if (entity.currentSiren == 3) {
                entity.currentSiren = 0
                mp.events.callRemote('syncSiren', entity, false, '', entity.sirenID)
                clearInterval(entity.scanSiren)
                entity.scanSiren = null
            } else {
                entity.currentSiren = 3
                if (entity.sirenID == null) {
                    let ID = mp.game.invoke('0x430386FE9BF80B45')
                    entity.sirenID = ID
                }
                mp.events.callRemote('syncSiren', entity, false, '', entity.sirenID)
                clearInterval(entity.scanSiren)
                entity.scanSiren = null
                mp.events.callRemote('syncSiren', entity, true, sirenTones.tone3, entity.sirenID)
            }
        }
    }
})

mp.events.add('inputTone4', _ => {
    let entity = mp.players.local.vehicle
    if (entity && entity.type === 'vehicle' && entity.getClass() === 18) {

        if (entity.currentCode >= 3) {
            if (!entity.currentSiren) {
                entity.currentSiren = 0
            }
            if (entity.currentSiren == 4) {
                entity.currentSiren = 0
                mp.events.callRemote('syncSiren', entity, false, '', entity.sirenID)
                clearInterval(entity.scanSiren)
                entity.scanSiren = null
            } else {
                entity.currentSiren = 4
                if (entity.sirenID == null) {
                    let ID = mp.game.invoke('0x430386FE9BF80B45')
                    entity.sirenID = ID
                }
                mp.events.callRemote('syncSiren', entity, false, '', entity.sirenID)
                clearInterval(entity.scanSiren)
                entity.scanSiren = null
                mp.events.callRemote('syncSiren', entity, true, sirenTones.tone4, entity.sirenID)
            }
        }
    }
})

mp.events.add('inputTone5', _ => {
    let entity = mp.players.local.vehicle
    if (entity && entity.type === 'vehicle' && entity.getClass() === 18) {
        if (entity.currentCode >= 3) {
            if (!entity.currentSiren) {
                entity.currentSiren = 0
            }
            if (entity.currentSiren == 5) {
                entity.currentSiren = 0
                mp.events.callRemote('syncSiren', entity, false, '', entity.sirenID)
                clearInterval(entity.scanSiren)
                entity.scanSiren = null
            } else {
                entity.currentSiren = 5
                let tones = Object.values(sirenTones)
                tones.splice(4, 2)
                if (entity.sirenID == null) {
                    let ID = mp.game.invoke('0x430386FE9BF80B45')
                    entity.sirenID = ID
                }
                mp.events.callRemote('syncSiren', entity, false, '', entity.sirenID)
                clearInterval(entity.scanSiren)
                entity.scanSiren = null
                let random = Math.floor(Math.random() * tones.length)
                mp.events.callRemote('syncSiren', entity, true, tones[random], entity.sirenID)
                entity.scanSiren = setInterval(_ => {
                    mp.game.invoke('0xA3B0C41BA5CC0BB5', entity.sirenID)
                    random = Math.floor(Math.random() * tones.length)
                    mp.events.callRemote('syncSiren', entity, true, tones[random], entity.sirenID)
                }, 5000)
            }
        }
    }
})

mp.events.add("playerLeaveVehicle", (vehicle) => {
    if (vehicle && vehicle.getClass() === 18 && vehicle.currentCode > 0) {
        mp.events.callRemote('syncLight', vehicle, true, true, vehicle.currentCode, 0);
        if (vehicle.currentSiren == 1) {
            vehicle.currentSiren = 0
            mp.events.callRemote('syncSiren', vehicle, false, '', vehicle.sirenID)
            clearInterval(vehicle.scanSiren)
            vehicle.scanSiren = null
        }
    }
})

mp.events.add("playerEnterVehicle", (vehicle, seat) => {
    if (vehicle && vehicle.getClass() === 18 && vehicle.currentCode > 0) {
        mp.events.callRemote('syncLight', vehicle, true, true, vehicle.currentCode, 0);
    }
})