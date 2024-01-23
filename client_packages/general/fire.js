let xPos = undefined;
let yPos = undefined;
let zPos = undefined;
let updatetimer = undefined;
let firesAlive = undefined;
let pos = undefined;
let FireLocationBlip = null;
let FireEntityIdArray = [];
let firemod = 0;

mp.events.add('Client:StartFire', (posX, posY, posZ, maxChilderen, gasPowerd) => {
    let fireId = mp.game.fire.startScriptFire(posX, posY, posZ, maxChilderen, gasPowerd);
    FireEntityIdArray.push(JSON.stringify(fireId));
});


mp.events.add('Client:FiresAliveTimer', (posX, posY, posZ, mod) => {
    xPos = posX;
    yPos = posY;
    zPos = posZ;
    firemod = mod;
    if (mod == 1) {
        firesAlive = mp.game.fire.getNumberOfFiresInRange(xPos, yPos, zPos, 25);
    }
    updatetimer = setInterval(fireTimer, 2137);
});

mp.events.add('Client:BlipFireLocation', (x, y, z) => {
    if (FireLocationBlip != null) {
        FireLocationBlip.destroy();
        FireLocationBlip = null;
    }
    FireLocationBlip = mp.blips.new(436, new mp.Vector3(x, y, z), {
        name: 'Einsatzort',
        color: 1,
        shortRange: false,
        dimension: 0
    });
});

mp.events.add('Client:RemoveBlipFireLocation', () => {
    if (FireLocationBlip != null) {
        FireLocationBlip.destroy();
        FireLocationBlip = null;
    }
});

mp.events.add('Client:StopFireById', () => {
    mp.events.call('Client:RemoveBlipFireLocation');

    mp.game.fire.stopFireInRange(mp.players.local.position.x, mp.players.local.position.y, mp.players.local.position.z, 3500);

    if (FireEntityIdArray.length !== 0) {
        if (updatetimer && updatetimer !== undefined) {
            clearInterval(updatetimer);
            updatetimer = undefined;
        }
        let arrayLength = FireEntityIdArray.length;
        for (let i = 0; i < arrayLength; i++) {
            mp.game.fire.removeScriptFire(Number(FireEntityIdArray[i]));
        }
        FireEntityIdArray = [];

        xPos = undefined;
        yPos = undefined;
        zPos = undefined;
        firesAlive = undefined;
        pos = undefined;
        firemod = 0;
    }
});

mp.events.add('Client:CheckIfReachable', (posX, posY, posZ, maxChilderen, gasPowerd) => {
    mp.events.call('Client:StartFire', parseFloat(posX), parseFloat(posY), (parseFloat(posZ) - 0.55), maxChilderen, gasPowerd);
});

function fireTimer() {
    if (xPos) {
        if (firemod == 1) {
            firesAlive = mp.game.fire.getNumberOfFiresInRange(xPos, yPos, zPos, 25);
            if (firesAlive < 1) {
                if (updatetimer && updatetimer !== undefined) {
                    clearInterval(updatetimer);
                    updatetimer = undefined;
                }
                mp.events.callRemote('Server:FireComplete');
            }
        } else {
            let dist = distanceVector(mp.players.local.position, new mp.Vector3(xPos, yPos, zPos));
            if (dist <= 4.85 && mp.players.local.isInMeleeCombat() && mp.players.local.weapon == mp.game.joaat('weapon_hatchet')) {
                mp.events.callRemote('Server:FireComplete');
            }
        }
    }
}

mp.events.add("playerDeath", (player, reason, killer) => {
    if (player == mp.players.local) {
        if (FireLocationBlip != null) {
            FireLocationBlip.destroy();
            FireLocationBlip = null;
        }
        if (updatetimer && updatetimer !== undefined) {
            clearInterval(updatetimer);
            updatetimer = undefined;
        }
        FireEntityIdArray = [];
        xPos = undefined;
        yPos = undefined;
        zPos = undefined;
        firesAlive = undefined;
        pos = undefined;
        firemod = 0;
    }
})

function playerQuitHandler(player, exitType, reason) {
    if (player == mp.players.local) {
        if (FireLocationBlip != null) {
            FireLocationBlip.destroy();
            FireLocationBlip = null;
        }
        if (updatetimer && updatetimer !== undefined) {
            clearInterval(updatetimer);
            updatetimer = undefined;
        }
        FireEntityIdArray = [];
        xPos = undefined;
        yPos = undefined;
        zPos = undefined;
        firesAlive = undefined;
        pos = undefined;
        firemod = 0;
    }
}

mp.events.add("playerQuit", playerQuitHandler);

function distanceVector(v1, v2) {
    let dx = v1.x - v2.x;
    let dy = v1.y - v2.y;
    let dz = v1.z - v2.z;
    return Math.sqrt(dx * dx + dy * dy + dz * dz);
}