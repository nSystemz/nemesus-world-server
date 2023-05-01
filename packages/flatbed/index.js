let streamRange = mp.config['stream-distance'] || 500

mp.events.add({
  fbSetState(player, fb, state) {
    fb.setVariable('fbState', state)
  },

  fbAttachRope(player, fb, vehOrFalse) {
    vehOrFalse = vehOrFalse == false? false : vehOrFalse.id
    
    let strVal = vehOrFalse === false? false : `mp.vehicles.atRemoteId(${vehOrFalse})`
    mp.players.forEachInRange(player.position, streamRange, player.dimension, (_player)=> {
      if (player != _player) {
        _player.eval(`attachRope(mp.vehicles.atRemoteId(${fb.id}), ${strVal})`)
      }
    })
  
    if (vehOrFalse !== false) {
      let veh = mp.vehicles.at(vehOrFalse)
      // all exit vehicle and lock, to prevent desync bug (Cant attachTo with a driver inside)
      veh.getOccupants().forEach(p => p.eval(`mp.players.local.taskLeaveVehicle(mp.players.local.vehicle.handle, 0)`))
      veh.controller = null
      veh.wasLocked = veh.locked
      veh.locked = true
    } else {
      let veh = mp.vehicles.at(fb.getVariable('fbAttachRope'))
      veh.locked = veh.wasLocked
      delete veh.wasLocked
    }
    fb.setVariable('fbAttachRope', vehOrFalse)
  },

  fbWindRope(player, flatbed) {
    mp.players.forEachInRange(player.position, streamRange, player.dimension, (_player)=> {
      if (player != _player) {
        _player.eval(`windRope(mp.vehicles.atRemoteId(${flatbed.id}))`)
      }
    })
  },

  fbAttachVehicle(player, fbID, vehID) {
    let fb = mp.vehicles.at(fbID)
  
    if (vehID !== false) {
      let veh = mp.vehicles.at(vehID)
      veh.wasLocked = veh.locked
      veh.locked = true
      
    } else {
      let veh = mp.vehicles.at(fb.getVariable('fbAttachVehicle'))
      veh.locked = !!veh.wasLocked
      delete veh.wasLocked
    }
  
    let str = vehID === false? false : `mp.vehicles.atRemoteId(${vehID})`
    mp.players.forEachInRange(player.position, streamRange, player.dimension, (_player)=> {
      if (player != _player) {
        _player.eval(`attachToBed(mp.vehicles.atRemoteId(${fbID}), ${str})`)
      }
    })
  
    fb.setVariable('fbAttachVehicle', vehID)
  },

  fbSyncPosition(player, vehID, pos, rot) {
    let veh = mp.vehicles.at(vehID)
    if (veh) {
      veh.position = JSON.parse(pos)
      veh.rotation = JSON.parse(rot)
    }
  }
})
