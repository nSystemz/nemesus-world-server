let graffitiBrowser = null;

mp.events.add('showGraffitiBrowser', (text) => {
    if (graffitiBrowser) {
        graffitiBrowser.destroy();
        graffitiBrowser = null;
    }

    // Debugging-Meldung hinzufügen
    mp.console.logInfo("Erstelle headless Browser...");

    graffitiBrowser = mp.browsers.newHeadless('package://graffiti/index.html');

    if (!graffitiBrowser) {
        mp.console.logError("Konnte headless Browser nicht erstellen!");
        return;
    }

    graffitiBrowser.on('ready', () => {
        // Debugging-Meldung hinzufügen
        mp.console.logInfo("Browser ist bereit, rendere Graffiti...");
        graffitiBrowser.execute(`setGraffitiText('${text}'); renderGraffiti();`);
    });

    graffitiBrowser.on('graffitiRendered', (imageData) => {
        // Debugging-Meldung hinzufügen
        mp.console.logInfo("Graffiti wurde gerendert, zeichne es...");

        const canvas = document.createElement('canvas');
        const img = new Image();
        img.src = imageData;

        img.onload = () => {
            canvas.width = img.width;
            canvas.height = img.height;
            const ctx = canvas.getContext('2d');
            ctx.drawImage(img, 0, 0);

            const dataURL = canvas.toDataURL();
            mp.events.callRemote('saveGraffitiTexture', dataURL, 'graffitiTextures', 'graffitiTexture');
        };

        graffitiBrowser.destroy();
        graffitiBrowser = null;
    });
});

mp.events.add('loadGraffitiTexture', (filePath, textureDict, textureName) => {
    mp.game.graphics.requestStreamedTextureDict(textureDict, true);

    const interval = setInterval(() => {
        if (mp.game.graphics.hasStreamedTextureDictLoaded(textureDict)) {
            clearInterval(interval);

            const position = mp.players.local.position;
            const forwardVector = mp.players.local.getForwardVector();
            const wallPosition = {
                x: position.x + forwardVector.x * 2,
                y: position.y + forwardVector.y * 2,
                z: position.z
            };

            mp.game.graphics.drawSprite(textureDict, textureName, wallPosition.x, wallPosition.y, wallPosition.z, 2.0, 2.0, 0, 255, 255, 255, 255);
        }
    }, 100);
});