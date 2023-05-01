function calcDist(v1, v2) {
    return mp.game.system.vdist(
        v1.x,
        v1.y,
        v1.z,
        v2.x,
        v2.y,
        v2.z
    );
}

mp.game.controls.useDefaultVehicleEntering = true;

mp.keys.bind(71, false, () => {
    let spawned = mp.players.local.getVariable('Player:Spawned');
    if (!spawned || mp.players.local.isTypingInTextChat) return;
    let seat = nearestSeat();
    let playerPos = mp.players.local.position;
    let closestVeh = getClosestVehicle(playerPos);
    let vehicle = closestVeh.vehicle;
    if (vehicle != null && seat != -1 && vehicle.isSeatFree(seat)) {
        mp.players.local.taskEnterVehicle(closestVeh.vehicle.handle, 5000, seat, 2.0, 1, 0);
    }
});

mp.events.add("Client:GetNearestSeat", (flag) => {
    if (hudWindow != null) {
        mp.events.callRemote('Server:NearestSeat', flag, nearestSeat());
    }
});

function nearestSeat() {
    let seat = -1;
    if (mp.players.local.vehicle === null) {
        let playerPos = mp.players.local.position;
        let closestVeh = getClosestVehicle(playerPos);
        let vehicle = closestVeh.vehicle;
        if (vehicle != null) {
            let seat_pside_r = vehicle.getWorldPositionOfBone(vehicle.getBoneIndexByName("seat_pside_r"));
            let seat_pside_f = vehicle.getWorldPositionOfBone(vehicle.getBoneIndexByName("seat_pside_f"));
            let seat_dside_r = vehicle.getWorldPositionOfBone(vehicle.getBoneIndexByName("seat_dside_r"));
            let seat_r = vehicle.getWorldPositionOfBone(vehicle.getBoneIndexByName("seat_r"));
            let platelight = vehicle.getWorldPositionOfBone(vehicle.getBoneIndexByName("platelight"));
            let vehiclename = vehicle.getVariable('Vehicle:Name');
            let distance = 9999;
            if (vehiclename.toLowerCase() == "trash") {
                if (calcDist(playerPos, platelight) < distance) {
                    distance = calcDist(playerPos, platelight);
                    if (vehicle.isSeatFree(2)) {
                        seat = 2;
                    } else {
                        seat = 3;
                    }
                }
            }
            if (vehicle.isSeatFree(0) && calcDist(playerPos, seat_pside_f) < distance) {
                distance = calcDist(playerPos, seat_pside_f);
                seat = 0;
            }
            if (vehicle.isSeatFree(1) && calcDist(playerPos, seat_dside_r) < distance) {
                distance = calcDist(playerPos, seat_dside_r);
                seat = 1;
            }
            if (vehicle.isSeatFree(2) && calcDist(playerPos, seat_pside_r) < distance) {
                distance = calcDist(playerPos, seat_pside_r);
                seat = 2;
            }
            if (vehicle.isSeatFree(3) && calcDist(playerPos, seat_r) < distance) {
                distance = calcDist(playerPos, seat_r);
                seat = 3;
            }
        }
    }
    return seat;
}

function getClosestVehicle(position) {
    try {
        let closest = 6.5;
        let closestVeh = null;

        mp.vehicles.forEachInStreamRange(v => {
            let dist = mp.game.system.vdist(position.x, position.y, position.z, v.position.x, v.position.y, v.position.z);

            if (dist < closest) {
                closest = dist;
                closestVeh = v;
            }
        });

        return {
            distance: closest,
            vehicle: closestVeh
        };
    } catch {}
}