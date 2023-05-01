mp.events.add('syncLight', (_, vehicle, sirens, sound, code, sirenCode) => {
    mp.players.call('syncLight', [vehicle, sirens, sound, code, sirenCode])
});

mp.events.add('syncSiren', (_, vehicle, playing, sound, id) => {
    mp.players.call('syncSiren', [vehicle, playing, sound, id])
})

mp.events.add('syncHorn', (_, vehicle, playing, sound, id) => {
    mp.players.call('syncHorn', [vehicle, playing, sound, id])
})

mp.events.add("vehicleSirenToggle", (vehicle, toggle) => {
    mp.players.call('vehicleSirenToggle', [vehicle])
});
