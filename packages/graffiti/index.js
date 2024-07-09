mp.events.addCommand('graffiti', (player, fullText) => {
    // Speichere den Text und sende ihn an den Client
    player.setVariable('graffitiText', fullText);
    player.call('createGraffiti');
});

// Event zum Laden der Textur auf dem Client
mp.events.add('loadGraffitiTexture', (player, textureDict, textureName) => {
    player.call('loadGraffitiTexture', [textureDict, textureName]);
});