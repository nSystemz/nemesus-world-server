let charWindow = mp.browsers.new("package://web/index.html");
let bodyCam = null;
let bodyCamStart;
const player = mp.players.local;

getCameraOffset = (pos, angle, dist) => {
    angle = angle * 0.0174533;
    pos.y = pos.y + dist * Math.sin(angle);
    pos.x = pos.x + dist * Math.cos(angle);
    return pos;
}

mp.events.add("Client:ShowCharacterSwitch", (characters, count) => {
    mp.gui.cursor.show(true, true);
    mp.game.ui.displayHud(false);
    mp.gui.chat.show(false);
    mp.game.ui.displayRadar(false);
    if (charWindow != null) {
        charWindow.execute(`gui.characterswitch.showCharacterSwitch('${characters}','${count}');`)
    }
})

mp.events.add("Client:HideCharacterSwitch", () => {
    if (charWindow != null) {
        charWindow.execute(`gui.characterswitch.hideCharacterSwitch();`)
    }
})

mp.events.add("Client:CreateNewCharacter", () => {
    if (charWindow != null) {
        mp.events.callRemote('Server:CreateCharacter');
    }
})

mp.events.add("Client:CreateNewCharacterFinish", (json, name, legal) => {
    if (charWindow != null) {
        mp.events.callRemote('Server:CreateNewCharacterFinish', json, name, legal);
    }
})

mp.events.add("Client:DeleteCharacter", (characterid) => {
    if (charWindow != null) {
        mp.events.callRemote('Server:DeleteCharacter', characterid);
    }
})

mp.events.add("Client:DeleteCharacterWindow", () => {
    if (charWindow != null) {
        charWindow.destroy();
        charWindow = null;
    }
    bodyCam.setActive(false);
})

mp.events.add("Client:CharacterCameraOn", (setDist=2.6) => {
    bodyCamStart = player.position;
    let camValues = {
        Angle: player.getRotation(2).z + 90,
        Dist: setDist,
        Height: 0.2
    };
    let pos = getCameraOffset(new mp.Vector3(bodyCamStart.x, bodyCamStart.y, bodyCamStart.z + camValues.Height), camValues.Angle, camValues.Dist);
    bodyCam = mp.cameras.new('default', pos, new mp.Vector3(0, 0, 0), 50);
    bodyCam.pointAtCoord(bodyCamStart.x, bodyCamStart.y, bodyCamStart.z + camValues.Height);
    bodyCam.setActive(true);
    mp.game.cam.renderScriptCams(true, false, 500, true, false);
})

mp.events.add("Client:CharacterCameraOff", () => {
    bodyCam.setActive(false);
    mp.game.cam.renderScriptCams(false, false, 0, false, false);
})

mp.events.add("Client:ShowCharacterCreator", () => {
    if (charWindow != null) {
        charWindow.execute(`gui.charactercreator.showCharacterCreator();`)
        bodyCamStart = player.position;
        let camValues = {
            Angle: player.getRotation(2).z + 90,
            Dist: 2.6,
            Height: 0.2
        };
        let pos = getCameraOffset(new mp.Vector3(bodyCamStart.x, bodyCamStart.y, bodyCamStart.z + camValues.Height), camValues.Angle, camValues.Dist);
        bodyCam = mp.cameras.new('default', pos, new mp.Vector3(0, 0, 0), 50);
        bodyCam.pointAtCoord(bodyCamStart.x, bodyCamStart.y, bodyCamStart.z + camValues.Height);
        bodyCam.setActive(true);
        mp.game.cam.renderScriptCams(true, false, 500, true, false);
        player.setComponentVariation(11, 15, 0, 0);
        player.setComponentVariation(3, 15, 0, 0);
        player.setComponentVariation(8, 15, 0, 0);
        player.setComponentVariation(6, 1, 0, 0);
        player.setComponentVariation(8, 15, 0, 0);
    }
})

mp.events.add("Client:SelectCharacter", (characterid) => {
    if (charWindow != null) {
        mp.events.callRemote('Server:SelectCharacter', characterid);
    }
})

mp.events.add("Client:CharacterError", () => {
    if (charWindow != null) {
        charWindow.execute(`gui.characterswitch.characterError();`)
    }
})

mp.events.add("Client:CharacterDelete", () => {
    if (charWindow != null) {
        charWindow.execute(`gui.hud.sendNotificationWithoutButton2('Charakter wurde gelöscht!','success', 'center', 2500);`)
    }
})

mp.events.add("Client:CharacterCamera", (flag) => {
    let camera = {
        Angle: 0,
        Dist: 1,
        Height: 0.2
    };
    switch (flag) {
        case 0: //Torso
        {
            camera = {
                Angle: 0,
                Dist: 2.5,
                Height: 0.2
            };
            break;
        }
        case 1: //Kopf
        {
            camera = {
                Angle: 0,
                Dist: 1,
                Height: 0.5
            };
            break;
        }
        case 2: //Gesicht
        {
            camera = {
                Angle: 0,
                Dist: 0.5,
                Height: 0.58
            };
            break;
        }
        case 3: //Körper/Brust
        {
            camera = {
                Angle: 0,
                Dist: 1,
                Height: 0.2
            };
            break;
        }
        case 4: //HeadOverlays
        {
            camera = {
                Angle: 0,
                Dist: 1.25,
                Height: 0.35
            };
            break;
        }
        case 5: //Kleidung
        {
            camera = {
                Angle: 0,
                Dist: 3.25,
                Height: 0.2
            };
            break;
        }
        case 6: //Kopf 2
        {
            camera = {
                Angle: 0,
                Dist: 1,
                Height: 0.825
            };
            break;
        }
    }
    bodyCamStart = player.position;
    const cameraPos = getCameraOffset(new mp.Vector3(bodyCamStart.x, bodyCamStart.y, bodyCamStart.z + camera.Height), player.getRotation(2).z + 90 + camera.Angle, camera.Dist);
    bodyCam.setCoord(cameraPos.x, cameraPos.y, cameraPos.z);
    bodyCam.pointAtCoord(bodyCamStart.x, bodyCamStart.y, bodyCamStart.z + camera.Height);
});

mp.events.add("Client:SetCharacterWoman", () => {
    setTimeout(() => {
        player.setHeadBlendData(36, 0, 0, 0, 0, 0, 0, 0, 0.0, true);
    }, 125);
});

mp.events.add("Client:SetCharacterMen", () => {
    setTimeout(() => {
        player.setHeadBlendData(0, 0, 0, 0, 0, 0, 0, 0, 0.0, true);
    }, 125);
});

mp.events.add("Client:ResetCharacterCamera", () => {
    mp.events.call('Client:CharacterCamera', 5);
});

mp.events.add("Client:CharacterPreview", (x, data) => {
    data = JSON.parse(data);
    switch (x) {
        case 'hair': {
            mp.events.call('Client:CharacterCamera', 1);
            player.setComponentVariation(2, parseInt(data[0]), 0, 0);
            player.setHairColor(parseInt(data[1]), parseInt(data[2]));
            mp.players.local.applyHairOverlay();
            break;
        }
        case 'beard': {
            mp.events.call('Client:CharacterCamera', 1);
            player.setHeadOverlay(1, parseInt(data[0]), 1.0, parseInt(data[1]), parseInt(data[1]));
            break;
        }
        case 'blendData': {
            mp.events.call('Client:CharacterCamera', 2);
            player.setHeadBlendData(parseInt(data[0]), parseInt(data[1]), 0, parseInt(data[2]), parseInt(data[3]), 0, parseFloat(data[4]), parseFloat(data[5]), 0.0, true);
        }
        case 'faceFeatures': {
            mp.events.call('Client:CharacterCamera', 2)
            player.setFaceFeature(0, parseFloat(data[0]));
            player.setFaceFeature(1, parseFloat(data[1]));
            player.setFaceFeature(2, parseFloat(data[2]));
            player.setFaceFeature(3, parseFloat(data[3]));
            player.setFaceFeature(4, parseFloat(data[4]));
            player.setFaceFeature(5, parseFloat(data[5]));
            player.setFaceFeature(6, parseFloat(data[6]));
            player.setFaceFeature(7, parseFloat(data[7]));
            player.setFaceFeature(8, parseFloat(data[8]));
            player.setFaceFeature(9, parseFloat(data[9]));
            player.setFaceFeature(10, parseFloat(data[10]));
            player.setFaceFeature(11, parseFloat(data[11]));
            player.setFaceFeature(12, parseFloat(data[12]));
            player.setFaceFeature(13, parseFloat(data[13]));
            player.setFaceFeature(14, parseFloat(data[14]));
            player.setFaceFeature(15, parseFloat(data[15]));
            player.setFaceFeature(16, parseFloat(data[16]));
            player.setFaceFeature(17, parseFloat(data[17]));
            player.setFaceFeature(18, parseFloat(data[18]));
            player.setFaceFeature(19, parseFloat(data[19]));
            break;
        }
        case 'clothing': {
            mp.events.call('Client:CharacterCamera', 5);
            player.setComponentVariation(11, parseInt(data[0]), 0, 0);
            player.setComponentVariation(3, parseInt(data[1]), 0, 0);
            player.setComponentVariation(4, parseInt(data[2]), 0, 0);
            player.setComponentVariation(6, parseInt(data[3]), 0, 0);
            player.setComponentVariation(8, parseInt(data[4]), 0, 0);
            break;
        }
    }
});

mp.events.add("Client:CharacterPreview2", (x, data) => {
    switch (x) {
        case 'eyeColor': {
            mp.events.call('Client:CharacterCamera', 2);
            player.setEyeColor(parseInt(data));
            break;
        }
        case 'gender': {
            mp.events.call('Client:CharacterCamera', 0);
            mp.events.callRemote('Server:CharacterChangeGender', data, true);
            break;
        }
    }
});

mp.events.add("Client:CharacterPreview3", (x, data, data2) => {
    data = JSON.parse(data);
    data2 = JSON.parse(data2);
    switch (x) {
        case 'headOverlays': {
            mp.events.call('Client:CharacterCamera', 4);
            player.setHeadOverlay(0, parseInt(data[0]), 1.0, parseInt(data2[0]), parseInt(data2[0]));
            player.setHeadOverlay(2, parseInt(data[1]), 1.0, parseInt(data2[1]), parseInt(data2[1]));
            player.setHeadOverlay(3, parseInt(data[2]), 1.0, parseInt(data2[2]), parseInt(data2[2]));
            player.setHeadOverlay(4, parseInt(data[3]), 1.0, parseInt(data2[3]), parseInt(data2[3]));
            player.setHeadOverlay(5, parseInt(data[4]), 1.0, parseInt(data2[4]), parseInt(data2[4]));
            player.setHeadOverlay(6, parseInt(data[5]), 1.0, parseInt(data2[5]), parseInt(data2[5]));
            player.setHeadOverlay(7, parseInt(data[6]), 1.0, parseInt(data2[6]), parseInt(data2[6]));
            player.setHeadOverlay(8, parseInt(data[7]), 1.0, parseInt(data2[7]), parseInt(data2[7]));
            player.setHeadOverlay(9, parseInt(data[8]), 1.0, parseInt(data2[8]), parseInt(data2[8]));
            player.setHeadOverlay(10, parseInt(data[9]), 1.0, parseInt(data2[9]), parseInt(data2[9]));
            player.setHeadOverlay(11, parseInt(data[10]), 1.0, parseInt(data2[10]), parseInt(data2[10]));
            player.setHeadOverlay(12, parseInt(data[11]), 1.0, parseInt(data2[11]), parseInt(data2[11]));
            break;
        }
    }
});

mp.events.add("Client:CharacterChangeClothes", () => {
    player.setComponentVariation(11, 15, 0, 0);
    player.setComponentVariation(3, 15, 0, 0);
    player.setComponentVariation(8, 15, 0, 0);
    player.setComponentVariation(6, 1, 0, 0);
    player.setComponentVariation(8, 15, 0, 0);
});

mp.events.add("Client:CheckCharacterName", (name) => {
    mp.events.callRemote('Server:CheckCharacter', name);
});

mp.events.add("Client:CheckCharacterNameAnswer", (answer) => {
    if (charWindow != null) {
        charWindow.execute(`gui.charactercreator.characterAnswer(${answer});`)
    }
});