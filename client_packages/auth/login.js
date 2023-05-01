mp.game.audio.startAudioScene("CHARACTER_CHANGE_IN_SKY_SCENE");

const Camera = require('./helper/camera');

let loginCam = new Camera('loginCam', new mp.Vector3(167.958, -814.943, 70.170), new mp.Vector3(-51.976, -812.603, 193.491));

let loginWindow = mp.browsers.new("package://web/index.html");

setTimeout(() => {
    mp.gui.cursor.show(true, true);
    mp.game.ui.displayHud(false);
    mp.gui.chat.show(false);
    mp.game.ui.displayRadar(false);
}, 150)

mp.events.add("Client:ShowLogin", () => {
    if (loginWindow != null) {
        loginWindow.execute(`gui.login.showLogin();`)
    }
})

mp.events.add("Client:ShowRegister", () => {
    if (loginWindow != null) {
        loginWindow.execute(`gui.register.showRegister();`)
    }
})

mp.events.add("Client:ShowLoading", () => {
    if (loginWindow != null) {
        loginWindow.execute(`gui.loading.showLoading();`)
    }
})

mp.events.add("Client:HideAuth", () => {
    if (loginWindow != null) {
        mp.gui.cursor.show(false, false);
        mp.game.ui.displayHud(true);
        mp.gui.chat.show(true);
        mp.game.ui.displayRadar(true);
        loginWindow.destroy();
        loginWindow = null;
    }
})

mp.events.add("Client:AccountRegister", (name, password) => {
    if (loginWindow != null) {
        mp.events.callRemote('Server:OnRegister', name, password);
    }
})

mp.events.add("Client:AccountLogin", (name, password) => {
    mp.events.callRemote('Server:OnLogin', name, password);
})

mp.events.add("Client:SetWarningLogin", (text) => {
    if (loginWindow != null) {
        loginWindow.execute(`gui.login.setWarning('${text}');`)
    }
})

mp.events.add("Client:SetWarningRegister", (text) => {
    if (loginWindow != null) {
        loginWindow.execute(`gui.register.setWarning('${text}');`)
    }
})

mp.events.add("Client:DestroyLoginCamera", () => {
    if (loginCam != null) {
        loginCam.delete();
        loginCam = null;
    }
})