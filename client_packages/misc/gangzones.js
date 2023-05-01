let allGangzones = [];

mp.events.add("Client:GetGangzones", (gangzones) => {
    allGangzones = JSON.parse(gangzones);
    if (gangzones.length > 0) {
        allGangzones.forEach((gangzone) => {
            gangzone.blip = mp.game.ui.addBlipForRadius(parseFloat(gangzone.posx), parseFloat(gangzone.posy), 1, gangzone.radius);
            natives.SET_BLIP_SPRITE(gangzone.blip, 5);
            natives.SET_BLIP_ALPHA(gangzone.blip, 95);
            natives.SET_BLIP_COLOUR(gangzone.blip, gangzone.color);
            natives.SET_BLIP_ROTATION(gangzone.blip, parseFloat(gangzone.heading));
            gangzone.colShape = mp.colshapes.newSphere(parseFloat(gangzone.posx), parseFloat(gangzone.posy), parseFloat(gangzone.posz), gangzone.radius, 0);
        });
    }
});

mp.events.add('Client:RemoveGangzones', () => {
    if (allGangzones.length > 0) {
        allGangzones.forEach((gangzone) => {
            natives.SET_BLIP_SPRITE(gangzone.blip, 0);
            natives.SET_BLIP_ALPHA(gangzone.blip, 255);
        });
    }
    allGangzones = [];
});

var natives = {};

mp.game.graphics.clearDrawOrigin = () => mp.game.invoke('0xFF0B610F6BE0D7AF'); // 26.07.2018 // GTA 1.44 
natives.SET_BLIP_SPRITE = (blip, sprite) => mp.game.invoke("0xDF735600A4696DAF", blip, sprite); // SET_BLIP_SPRITE
natives.SET_BLIP_ALPHA = (blip, a) => mp.game.invoke("0x45FF974EEE1C8734", blip, a); // SET_BLIP_ALPHA
natives.SET_BLIP_COLOUR = (blip, c) => mp.game.invoke("0x03D7FB09E75D6B7E", blip, c); // SET_BLIP_COLOUR
natives.SET_BLIP_ROTATION = (blip, r) => mp.game.invoke("0xF87683CDF73C3F6E", blip, r); // SET_BLIP_ROTATION


mp.events.add('playerEnterColshape', (shape) => {
    if (allGangzones.length > 0) {
        allGangzones.forEach((gangzone) => {
            if(gangzone.colShape == shape)
            {
                mp.events.callRemote('Server:InGangZone', gangzone.id);
            }
        });
    }
});

mp.events.add('playerExitColshape', (shape) => {
    let spawned = mp.players.local.getVariable('Player:Spawned');
    if (!spawned) return;
    if (allGangzones.length > 0) {
        allGangzones.forEach((gangzone) => {
            if(gangzone.colShape == shape)
            {
                mp.events.callRemote('Server:InGangZone', -1);
            }
        });
    }
});