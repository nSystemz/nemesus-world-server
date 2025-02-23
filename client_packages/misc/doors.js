let door = [];

mp.events.add("Client:GetDoors", (doors) => {
    door = JSON.parse(doors);
    if (door.length > 0) {
        setTimeout(function () {
            door.forEach((dor) => {
                mp.game.object.doorControl(parseInt(dor.hash), dor.posx, dor.posy, dor.posz, dor.toogle, 0.0, 0.0, 0);
            });
        }, 3250);
    }
});

mp.events.add("Client:UpdateDoors", (jsondoor) => {
    mp.events.call("Client:RemoveDoor", jsondoor);
    setTimeout(function () {
        mp.events.call("Client:AddDoor", jsondoor);
    }, 155);
});

mp.events.add("Client:AddDoor", (ndoor) => {
    let newdoor = JSON.parse(ndoor);
    door.push(newdoor);
    mp.game.object.doorControl(parseInt(newdoor.hash), newdoor.posx, newdoor.posy, newdoor.posz, newdoor.toogle, 0.0, 0.0, 0);
});

mp.events.add("Client:RemoveDoor", (rdoorpara) => {
    let rdoor = JSON.parse(rdoorpara);
    door = door.filter(function (element) {
        return element != rdoor;
    });
});