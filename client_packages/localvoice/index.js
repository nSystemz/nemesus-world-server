const Use3d = false;
const UseAutoVolume = true;

let pressedRollen = 0;

let playerOnPhone = -1;

mp.keys.bind(0x91, true, function () {
    if (pressedRollen == 0 || (Date.now() / 1000) > pressedRollen) {
        let spawned = mp.players.local.getVariable('Player:Spawned');
		let death = mp.players.local.getVariable('Player:Death');
		let maxRange = mp.players.local.getVariable('Player:VoiceRangeLocal');
		if(spawned && !death)
		{
			if(maxRange == 12.0)
			{
				maxRange = 25.0;
			}
			else if(maxRange == 25.0)
			{
				maxRange = 36.0;
			}
			else if(maxRange == 36)
			{
				maxRange = 12.0;
			}
			if(maxRange)
			{
				mp.events.callRemote('Server:SetVoiceRangeLocal', maxRange);
				mp.events.call('Client:DrawMarkerWithTime', maxRange);
				mp.events.call('Client:SendNotificationWithoutButton', 'Voice-Range auf ' + maxRange + 'm, umgestellt!', 'info', 'top-left', 2500);
			}
		}
        pressedRollen = (Date.now() / 1000) + (3);
    }
});

let g_voiceMgr = {
	listeners: [],

	add: function (player) {
		this.listeners.push(player);

		player.isListening = true;
		mp.events.callRemote("Server:Add_Voice_Listener", player);

		if (UseAutoVolume) {
			player.voiceAutoVolume = true;
		} else {
			player.voiceVolume = 1.0;
		}

		if (Use3d) {
			player.voice3d = true;
		}
	},

	remove: function (player, notify) {
		let idx = this.listeners.indexOf(player);

		if (idx !== -1)
			this.listeners.splice(idx, 1);

		player.isListening = false;

		if (notify) {
			mp.events.callRemote("Server:Remove_Voice_Listener", player);
		}
	}
};

mp.events.add("playerQuit", (player) => {
	if (player.isListening) {
		g_voiceMgr.remove(player, false);
	}
});

setInterval(() => {
	let localPlayer = mp.players.local;
	let localPos = localPlayer.position;
	playerOnPhone = localPlayer.getVariable('Player:LocalVoiceHandyPlayer');

	mp.players.forEachInStreamRange(player => {
		if (player != localPlayer && player.remoteId != playerOnPhone) {
			if (!player.isListening) {
				const playerPos = player.position;
				let dist = mp.game.system.vdist(playerPos.x, playerPos.y, playerPos.z, localPos.x, localPos.y, localPos.z);

				if (dist <= player.getVariable(getVariable('Player:VoiceRangeLocal'))) {
					g_voiceMgr.add(player);
				}
			}
		}
	});

	g_voiceMgr.listeners.forEach((player) => {
		if (player.handle !== 0 && player.remoteId != playerOnPhone) {
			const playerPos = player.position;
			let dist = mp.game.system.vdist(playerPos.x, playerPos.y, playerPos.z, localPos.x, localPos.y, localPos.z);

			if (dist > player.getVariable(getVariable('Player:VoiceRangeLocal'))) {
				g_voiceMgr.remove(player, true);
			} else if (!UseAutoVolume) {
				player.voiceVolume = 1 - (dist / player.getVariable(getVariable('Player:VoiceRangeLocal')));
			}
		} else {
			g_voiceMgr.remove(player, true);
		}
	});
}, 500);