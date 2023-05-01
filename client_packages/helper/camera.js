const CLEAR_FOCUS = "0x31B73D1EA9F01DA2";

class Camera {
    static Current_Cam = null;

    constructor(identifier, position, pointAtCoord) {
        this.identifier = identifier;
        this.position = position;
        this.pointAtCoord = pointAtCoord;
        this.camera = null;
        this.isMoving = false;
        this.range = 0.0;
        this.speed = 0.1;
        this.create();
        Camera.Current_Cam = this;
    }

    create() {
        if(Camera.Current_Cam !== null) {
            camera.delete();
        }

        this.camera = mp.cameras.new(this.identifier, this.position, new mp.Vector3(0, 0, 0), 40);
        this.camera.pointAtCoord(this.pointAtCoord.x, this.pointAtCoord.y, this.pointAtCoord.z);
        this.setActive();
    }

    setActive() {
        this.camera.setActive(true);
        mp.game.cam.renderScriptCams(true, false, 0, false, false);
        mp.game.streaming.setFocusArea(this.position.x, this.position.y, this.position.z, 0, 0, 0);
    }

    startMoving(range) {
        this.isMoving = true;
        this.range = range;
    }

    stopMoving() {
        this.isMoving = false;
        this.range = 0.0;
    }

    delete() {
        this.camera.destroy();
        mp.game.cam.renderScriptCams(false, false, 0, false, false);
        mp.game.invoke(CLEAR_FOCUS);
        Camera.Current_Cam = null;
    }
}

mp.events.add('render', () => {
    if(Camera.Current_Cam === null || !Camera.Current_Cam.isMoving) return;

    let position = Camera.Current_Cam.camera.getCoord();
    
    Camera.Current_Cam.camera.setCoord(position.x + Camera.Current_Cam.speed, position.y, position.z);

    if(position.x + Camera.Current_Cam.speed >= Camera.Current_Cam.position.x + (Camera.Current_Cam.range/2)
        || position.x + Camera.Current_Cam.speed <= Camera.Current_Cam.position.x - (Camera.Current_Cam.range/2)) {

            Camera.Current_Cam.speed *= -1;
    }
})

exports = Camera;