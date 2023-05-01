let screenshotBrowser = null;
let screenName = null;

mp.events.add("Client:ScreenshotTaken", (name) => {
    screenName = name;
    screenshotBrowser = mp.browsers.new("package://screenshots/index.html");
});

mp.events.add('browserDomReady', (browser) => {
    if(browser != screenshotBrowser) return;
    screenshotBrowser.call("recieveImage", "http://screenshots/nWorld-Screen.jpg");
});

mp.events.add("Client:UploadScreenshot", (screenshot) => {
    mp.events.callRemote("Server:UploadScreenshot", screenshot, screenName);
    screenshotBrowser.destroy();
    screenshotBrowser = null;
});