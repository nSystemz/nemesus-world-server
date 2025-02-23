let devModus = false;
let renderCounter = 0;
let flyCd = 0;

const controlsIds = {
    F6: 167,
    W: 32, // 232
    S: 33, // 31, 219, 233, 268, 269
    A: 34, // 234
    D: 35, // 30, 218, 235, 266, 267
    Space: 321,
    LCtrl: 326,
};

global.fly = {
    flying: false,
    f: 2.0,
    w: 2.0,
    h: 2.0,
    point_distance: 1000,
};

global.gameplayCam = mp.cameras.new('gameplay');

let direction = null;
let coords = null;

function pointingAtDev(distance) {
    const farAway = new mp.Vector3((direction.x * distance) + (coords.x), (direction.y * distance) + (coords.y), (direction.z * distance) + (coords.z));

    const result = mp.raycasting.testPointToPoint(coords, farAway, [1, 16]);
    if (result === undefined) {
        return 'undefined';
    }
    return result;
}

mp.events.add('render', () => {
    const controls = mp.game.controls;
    const fly = global.fly;
    renderCounter++;
    direction = global.gameplayCam.getDirection();
    coords = global.gameplayCam.getCoord();
    let pointing = pointingAtDev(fly.point_distance);
    if (devModus == true) {
        mp.game.graphics.drawText(`Entity: ${JSON.stringify(pointing.entity)} - Coords: ${JSON.stringify(coords)}`, [0.5, 0.035], {
            font: 0,
            color: [255, 255, 255, 185],
            scale: [0.3, 0.3],
            outline: true,
        });
        mp.game.graphics.drawText(`pointAtCoord: ${JSON.stringify(pointing.position)}`, [0.5, 0.055], {
            font: 0,
            color: [255, 255, 255, 185],
            scale: [0.3, 0.3],
            outline: true,
        });

        if (controls.isControlJustPressed(0, controlsIds.F6) && (flyCd == 0 || (Date.now() / 1000) > flyCd)) {
            flyCd = Date.now() / 1000 + (2);
            fly.flying = !fly.flying;

            const player = mp.players.local;

            player.weapon = mp.game.joaat('weapon_unarmed');

            player.freezePosition(fly.flying);

            fly.flying == true ? mp.players.local.setAlpha(0) : mp.players.local.setAlpha(125);

            if (!fly.flying && !controls.isControlPressed(0, controlsIds.Space)) {
                const position = mp.players.local.position;
                position.z = mp.game.gameplay.getGroundZFor3dCoord(position.x, position.y, position.z, 0.0, false);
                mp.players.local.setCoordsNoOffset(position.x, position.y, position.z, false, false, false);
            }
        } else if (fly.flying) {
            let updated = false;
            const position = mp.players.local.position;

            if (controls.isControlPressed(0, controlsIds.W)) {
                if (fly.f < 8.0) {
                    fly.f *= 1.025;
                }

                position.x += direction.x * fly.f;
                position.y += direction.y * fly.f;
                position.z += direction.z * fly.f;
                updated = true;
            } else if (controls.isControlPressed(0, controlsIds.S)) {
                if (fly.f < 8.0) {
                    fly.f *= 1.025;
                }

                position.x -= direction.x * fly.f;
                position.y -= direction.y * fly.f;
                position.z -= direction.z * fly.f;
                updated = true;
            } else {
                fly.f = 2.0;
            }

            if (controls.isControlPressed(0, controlsIds.A)) {
                if (fly.l < 8.0) {
                    fly.l *= 1.025;
                }

                position.x += (-direction.y) * fly.l;
                position.y += direction.x * fly.l;
                updated = true;
            } else if (controls.isControlPressed(0, controlsIds.D)) {
                if (fly.l < 8.0) {
                    fly.l *= 1.05;
                }

                position.x -= (-direction.y) * fly.l;
                position.y -= direction.x * fly.l;
                updated = true;
            } else {
                fly.l = 2.0;
            }

            if (controls.isControlPressed(0, controlsIds.Space)) {
                if (fly.h < 8.0) {
                    fly.h *= 1.025;
                }

                position.z += fly.h;
                updated = true;
            } else if (controls.isControlPressed(0, controlsIds.LCtrl)) {
                if (fly.h < 8.0) {
                    fly.h *= 1.05;
                }

                position.z -= fly.h;
                updated = true;
            } else {
                fly.h = 2.0;
            }

            if (updated) {
                mp.players.local.setCoordsNoOffset(position.x, position.y, position.z, false, false, false);
            }
        }
    }
});

mp.events.addDataHandler("Player:DevModus", (entity, value, oldValue) => {
    devModus = value;
    if (devModus == false) {
        if (fly.flying) {
            const position = mp.players.local.position;
            position.z = mp.game.gameplay.getGroundZFor3dCoord(position.x, position.y, position.z, 0.0, false);
            mp.players.local.setCoordsNoOffset(position.x, position.y, position.z, false, false, false);
            fly.flying = false;
            mp.players.local.weapon = mp.game.joaat('weapon_unarmed');
            mp.players.local.freezePosition(false);
        }
    }
});

let doorStates = [];

mp.keys.bind(0x4A, true, function () { 
    if (devModus == false) return;
    let cameraPos = mp.game.cam.getGameplayCamCoord();
    let direction = mp.game.cam.getGameplayCamRot(2);

    let farPoint = {
        x: cameraPos.x + direction.x * 1000,
        y: cameraPos.y + direction.y * 1000,
        z: cameraPos.z * 1000
    };

    let result = mp.raycasting.testPointToPoint(cameraPos, farPoint, [1]);

    if (result && result.entity) {
        let doorHash = mp.game.entity.getEntityModel(result.entity);
        let pos = result.position;

        let doorKey = `${doorHash}_${pos.x.toFixed(2)}_${pos.y.toFixed(2)}_${pos.z.toFixed(2)}`;

        if (!(doorKey in doorStates)) {
            doorStates[doorKey] = false;
        }

        doorStates[doorKey] = !doorStates[doorKey];

        mp.game.object.doorControl(doorHash, pos.x, pos.y, pos.z, doorStates[doorKey], 0.0, 0.0, 0.0);

        let stateText = doorStates[doorKey] ? "offen" : "geschlossen";
        let output = `Status ${stateText}: Hash ${doorHash}, Position: { x: ${pos.x.toFixed(2)}, y: ${pos.y.toFixed(2)}, z: ${pos.z.toFixed(2)} }`;
        mp.console.logInfo(output, true, true);
    } else {
        mp.gui.chat.push("Keine Tür gefunden.");
    }
});