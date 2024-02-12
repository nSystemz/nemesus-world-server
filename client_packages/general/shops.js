//Kleidungsladen
mp.events.add("Client:ChangeShopClothes", (cloth, id, color) => {
    if (cloth == 'Schuh') {
        mp.players.local.setComponentVariation(6, id, Number(color), 0);
    } else if (cloth == 'Hosen') {
        mp.players.local.setComponentVariation(4, id, Number(color), 0);
    } else if (cloth == 'Torso') {
        mp.players.local.setComponentVariation(3, id, Number(color), 0);
    } else if (cloth == 'Oberbekleidung') {
        mp.players.local.setComponentVariation(11, id, Number(color), 0);
        mp.events.callRemote('Server:GetBestTorso', id, Number(color), false, false);
    } else if (cloth == 'T-Shirt') {
        mp.players.local.setComponentVariation(8, id, Number(color), 0);
    } else if (cloth == 'Rucksack') {
        mp.players.local.setComponentVariation(5, id, Number(color), 0);
    } else if (cloth == 'Maske') {
        mp.players.local.setComponentVariation(1, id, Number(color), 0);
    } else if (cloth == 'Brillen') {
        if (id == -1 || id == 255) {
            mp.players.local.clearProp(1);
        } else {
            mp.players.local.setPropIndex(1, id, Number(color), true);
        }
    } else if (cloth == 'Kopfbedeckung') {
        if (id == -1 || id == 255) {
            mp.players.local.clearProp(0);
        } else {
            mp.players.local.setPropIndex(0, id, Number(color), true);
        }
    } else if (cloth == 'Ohrring') {
        if (id == -1 || id == 255) {
            mp.players.local.clearProp(2);
        } else {
            mp.players.local.setPropIndex(2, id, Number(color), true);
        }
    } else if (cloth == 'Armbanduhr') {
        if (id == -1 || id == 255) {
            mp.players.local.clearProp(6);
        } else {
            mp.players.local.setPropIndex(6, id, Number(color), true);
        }
    } else if (cloth == 'Armring') {
        if (id == -1 || id == 255) {
            mp.players.local.clearProp(7);
        } else {
            mp.players.local.setPropIndex(7, id, Number(color), true);
        }
    } else if (cloth == 'Accessoires') {
        mp.players.local.setComponentVariation(7, id, Number(color), 0);
    }
});

mp.events.add("Client:AbortCloth", () => {
    mp.events.callRemote('Server:HideClothMenu', true, false);
});

mp.events.add("Client:AbortCloth2", (status) => {
    mp.events.callRemote('Server:HideClothMenu', true, status);
});

mp.events.add("Client:BuyCloths", (json1, json2, clothprice, faction) => {
    mp.events.callRemote('Server:BuyNewCloths', json1, json2, clothprice, faction);
});

//Barber shop
mp.events.add("Client:ChangeShopHair", (selected, id, color, color2) => {
    if (selected == 1) {
        mp.players.local.setComponentVariation(2, id, 0, 0);
        mp.players.local.setHairColor(parseInt(color), parseInt(color2));
        mp.players.local.applyHairOverlay();
    } else if (selected == 2) {
        mp.players.local.setHeadOverlay(1, id, 1.0, parseInt(color), parseInt(color));
    } else {
        mp.events.callRemote('Server:BuyNewHairs', -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1);
    }
});

mp.events.add("Client:BuyShopHair", (id, color, id2, color2, color3, price, overlay0, overlaycolor0, overlay1, overlaycolor1, overlay2, overlaycolor2) => {
    mp.events.callRemote('Server:BuyNewHairs', id, color, id2, color2, color3, price, overlay0, overlaycolor0, overlay1, overlaycolor1, overlay2, overlaycolor2);
});

mp.events.add("Client:ChangeOverlay", (overlay, id, color) => {
    if (id == -1) {
        id = 255;
    }
    mp.players.local.setHeadOverlay(overlay, parseInt(id), 1.0, parseInt(color), parseInt(color));
});

//Tattoo shop
mp.events.add("Client:ChangeTattoo", (oldTattoosTemp, name, dlcName) => {
    try {
        mp.players.local.clearDecorations();
        mp.players.local.setDecoration(mp.game.joaat(dlcName), mp.game.joaat(name));
    } catch (e) {
        mp.console.logInfo(JSON.stringify(e), false, false);
    }
    try {
        var oldTattoos = JSON.parse(oldTattoosTemp);
        for (let i = 0; i < oldTattoos.length; i++) {
            mp.players.local.setDecoration(mp.game.joaat(oldTattoos[i].dlcname), mp.game.joaat(oldTattoos[i].name));
        }
    } catch (e) {
        mp.console.logInfo(JSON.stringify(e), false, false);
    }
});

mp.events.add("Client:BuyTattoo", (name, dlcName, zone) => {
    mp.players.local.clearDecorations();
    setTimeout(function () {
        mp.events.callRemote('Server:BuyTattoo', name, dlcName, zone);
    }, 125);
});

mp.events.add("Client:ResetTattooDirect", () => {
    try {
        mp.players.local.clearDecorations();
        mp.events.callRemote('Server:ResetAllTattoos');
    } catch (e) {
        mp.console.logInfo(JSON.stringify(e), false, false);
    }
});

mp.events.add("Client:ResetTattoo2", () => {
    mp.players.local.clearDecorations();
    mp.events.callRemote('Server:ResetAllTattoos2');
});

mp.events.add("Client:SetCloth", (compid, drawableid, textureid, paletteid) => {
    mp.players.local.setComponentVariation(compid, drawableid, textureid, paletteid);
})