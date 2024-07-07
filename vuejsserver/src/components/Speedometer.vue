<template>
    <div class="speedometershow">
        <div style="height: 2px; background-color: transparent" v-if="speedometershow">
            <div class="info vehicle">
                <vue-ellipse-progress v-if="windowHeight >= 900 && windowHeight < 1300" :progress="speedProgress"
                    :angle="360" color="#3F6791" emptyColor="rgba(0, 0, 0, 0.5)" :size="203" :thickness="15"
                    emptyThickness="7%" animation="reverse 700 400" lineMode="out-over" :legend="true"
                    legendClass="legend-custom-style" dash="60 0.9" :noData="false" :loading="false" fontColor="white"
                    :half="true" fontSize="1.5rem">
                    <p class="kmhtext">
                        {{ speed2 }} KM/H</p>
                </vue-ellipse-progress>
                <vue-ellipse-progress v-if="windowHeight < 900 && windowHeight > 800" :progress="speedProgress"
                    :angle="360" color="#3F6791" emptyColor="rgba(0, 0, 0, 0.5)" :size="108.5" :thickness="8"
                    emptyThickness="7%" animation="reverse 700 400" lineMode="out-over" :legend="true"
                    legendClass="legend-custom-style" dash="60 0.9" :noData="false" :loading="false" fontColor="white"
                    :half="true" fontSize="1.5rem">
                    <p class="kmhtext">
                        {{ speed2 }} KM/H</p>
                </vue-ellipse-progress>
                <vue-ellipse-progress v-if="windowHeight <= 800" :progress="speedProgress" :angle="360" color="#3F6791"
                    emptyColor="rgba(0, 0, 0, 0.5)" :size="108.5" :thickness="8" emptyThickness="7%"
                    animation="reverse 700 400" lineMode="out-over" :legend="true" legendClass="legend-custom-style"
                    dash="60 0.9" :noData="false" :loading="false" fontColor="white" :half="true" fontSize="1.5rem">
                    <p class="kmhtext">
                        {{ speed2 }} KM/H</p>
                </vue-ellipse-progress>
                <vue-ellipse-progress v-if="windowHeight >= 1300 && windowHeight < 1500" :progress="speedProgress"
                    :angle="360" color="#3F6791" emptyColor="rgba(0, 0, 0, 0.5)" :size="335" :thickness="22.5"
                    emptyThickness="7%" animation="reverse 700 400" lineMode="out-over" :legend="true"
                    legendClass="legend-custom-style" dash="60 0.9" :noData="false" :loading="false" fontColor="white"
                    :half="true" fontSize="1.5rem">
                    <p class="kmhtext">
                        {{ speed2 }} KM/H</p>
                </vue-ellipse-progress>
                <vue-ellipse-progress v-if="windowHeight >= 1500" :progress="speedProgress" :angle="360" color="#3F6791"
                    emptyColor="rgba(0, 0, 0, 0.5)" :size="335" :thickness="21.5" emptyThickness="7%"
                    animation="reverse 700 400" lineMode="out-over" :legend="true" legendClass="legend-custom-style"
                    dash="60 0.9" :noData="false" :loading="false" fontColor="white" :half="true" fontSize="1.5rem">
                    <p class="kmhtext">
                        {{ speed2 }} KM/H</p>
                </vue-ellipse-progress>
            </div>
            <div class="info vehicle others">
                <div class="col-md-12">
                    <div class="row">
                        <div class="icon2">
                            <i class="fas fa-car" style="color:green;text-shadow: 0 0 2px #000;"
                                v-if="vehiclengine == 1"></i><i class="fas fa-car"
                                style="color:red;text-shadow: 0 0 2px #000;" v-if="vehiclengine != 1"></i><i
                                class="fas fa-key ml-2" style="color:green;text-shadow: 0 0 2px #000;"
                                v-if="locked == 1"></i><i class="fas fa-key ml-2"
                                style="color:red;text-shadow: 0 0 2px #000;" v-else></i><i
                                class="fas fa-user-slash ml-2" style="color:red;text-shadow: 0 0 2px #000;"
                                v-if="belt == 1"></i><i class="fas fa-user-slash ml-2"
                                style="color:green;text-shadow: 0 0 2px #000;" v-if="belt != 1"></i><i
                                class="fas fa-tint ml-2" style="color:green;text-shadow: 0 0 2px #000;"
                                v-if="oel >= 35"></i><i class="fas fa-tint ml-2"
                                style="color:yellow;text-shadow: 0 0 2px #000;" v-if="oel <= 35 && oel > 5"></i>
                            <i class="fas fa-tint ml-2" style="color:red;text-shadow: 0 0 2px #000;"
                                v-if="!oel || oel <= 5"></i>
                        </div>
                        <div class="icon">
                            <i class="fas fa-car-crash" style="color:#3F6791;text-shadow: 0 0 2px #000;"><span
                                    class="ml-3"
                                    style="font-family: 'Exo', sans-serif;color:white">{{ health }}%</span></i>
                        </div>
                        <div class="icon" v-if="battery > 0">
                            <i class="fas fa-battery-quarter" style="color:#3F6791;text-shadow: 0 0 2px #000;"><span
                                    class="ml-3"
                                    style="font-family: 'Exo', sans-serif;color:white">{{ setbattery }}%</span></i>
                        </div>
                        <div class="icon" v-if="battery <= 0">
                            <i class="fas fa-battery-quarter" style="color:#3F6791;text-shadow: 0 0 2px #000;"><span
                                    class="ml-3" style="font-family: 'Exo', sans-serif;color:white">/</span></i>
                        </div>
                        <div class="icon" v-if="maxfuel > 0">
                            <i class="fas fa-gas-pump" style="color:#3F6791;text-shadow: 0 0 2px #000;"><span
                                    class="ml-3"
                                    style="font-family: 'Exo', sans-serif;color:white">{{ setfuel }}%</span></i>
                        </div>
                        <div class="icon" v-if="maxfuel <= 0">
                            <i class="fas fa-gas-pump" style="color:#3F6791;text-shadow: 0 0 2px #000;"><span
                                    class="ml-3" style="font-family: 'Exo', sans-serif;color:white">/</span></i>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="weapon mb-4" style="margin-top: -0.9vw" v-if="showhud4">
            <div class="row">
                <img src="../assets/images/inventory/Faust.png" style="max-width: 4vw"
                    :class="[(actualWeapon == 'faust') ? 'weaponimg1 mr-3' : 'weaponimg2 mr-3']">
                <img v-for="(weapon, index) in weapons" :key="index" :src="getImgUrl(weapon.description)" style="max-width: 4vw"
                    :class="[(actualWeapon == weapon.description.toLowerCase()) ? [(IsMelee(weapon.description.toLowerCase())) ? 'weaponimg3 mr-3' : 'weaponimg1 mr-3'] : [(IsMelee(weapon.description.toLowerCase())) ? 'weaponimg4 mr-3' : 'weaponimg2 mr-3']]">
            </div>
        </div>
        <div v-if="showhud4">
            <div class="weapon2"
                v-if="weapons && weapons.length > 0 && !IsMelee(actualWeapon) && actualWeapon != 'schlagring' && actualWeapon != 'faust' && actualWeapon != 'n/A' && actualAmmo < 5000">
                <span class="weapontext1">{{ actualAmmo }}</span>
            </div>
            <div class="weapon mt-3" v-else>
                <span style="font-size: 9px; font-family: 'Exo', sans-serif; text-shadow: 0 0 2px #000;">Nemesus-World
                    Gamemode by Nemesus.de</span>
            </div>
        </div>
        <div style="height: 100%; Width: 100%; background-color: transparent;" v-if="showhud2">
            <div class="info server others">
                <div class="row">
                    <div class="iconnw iconnw2 icon3 text-center">
                        <i class="fas fa-server" style="color:#3F6791;text-shadow: 0 0 2px #000;"><span
                                class="ml-2 text-center"
                                style="font-family: 'Exo', sans-serif;color:white">Nemesus-World.de</span></i>
                    </div>
                    <div class="icon3 text-center">
                        <i class="fas fa-portrait" style="color:#3F6791;text-shadow: 0 0 2px #000;"><span
                                class="ml-2 text-center" style="font-family: 'Exo', sans-serif;color:white;">ID:
                                {{ id }}</span></i>
                    </div>
                    <div class="icon3 text-center">
                        <i class="fas fa-clock" style="color:#3F6791;text-shadow: 0 0 2px #000;"><span
                                class="ml-2 text-center" style="font-family: 'Exo', sans-serif;color:white">Zeit:
                                {{ time }} Uhr</span></i>
                    </div>
                    <div class="iconnw icon3 text-center">
                        <i class="fas fa-map-marker-alt" style="color:#3F6791;text-shadow: 0 0 2px #000;"><span
                                class="ml-2 text-center"
                                style="font-family: 'Exo', sans-serif;color:white">{{ ort }}</span></i>
                    </div>
                    <div style="margin-left: 0.22vw;margin-top: 1.131vh" v-if="voicerp == 1 || voicerp == 2">
                        <i v-if="voicerp == 1 && talkstate == 0" class="fas fa-microphone bordericon"
                            style="color:white;text-shadow: 0 0 2px #000;font-size:0.7vw"></i>
                        <i v-if="(voicerp == 1 && talkstate == 1)" class="fas fa-microphone bordericon"
                            style="color:#3F6791;text-shadow: 0 0 2px #000;font-size:0.7vw"></i>
                        <i v-if="(voicerp == 1 && (talkstate == 2 || talkstate == -1 || talkstate == -2))"
                            class="fas fa-microphone bordericon"
                            style="color:red;text-shadow: 0 0 2px #000;font-size:0.7vw"></i>
                        <i v-if="(voicerp == 2 && talkstate2 == 1)" class="fas fa-microphone bordericon"
                            style="color:#3F6791;text-shadow: 0 0 2px #000;font-size:0.7vw"></i>
                        <i v-if="(voicerp == 2 && talkstate2 >= 2)" class="fas fa-microphone bordericon"
                            style="color:#08db36;text-shadow: 0 0 2px #000;font-size:0.7vw"></i>
                        <i v-if="(voicerp == 2 && talkstate2 == 0)"
                            class="fas fa-microphone bordericon"
                            style="color:red;text-shadow: 0 0 2px #000;font-size:0.7vw"></i>
                    </div>
                </div>
            </div>
            <div class="crosshair" v-if="crosshairshow && crosshair > 0">
                <img :src="getCrosshair(crosshair)" alt="Crosshair">
            </div>
        </div>
        <div v-if="infomessage && showhud4" class="text-center"
            style="width: 42vw;max-height:3.55vw;background-color: rgba(0, 0, 0, 0.5);border-radius: 0.5vw;margin-top:-0.6vw;margin-left:30vw;text-shadow: 0 0 0.3px #000">
            <div
                style="display: flex; justify-content: center; align-items: center;margin-top:-0.5vw;font-family: 'Exo', sans-serif;">
                <span style="margin-top:0.4vw;color:white;font-size:1vw"><span style="color:#3F6791">Info:</span>
                    {{ infomessage }}</span>
            </div>
        </div>
        <div style="height: 100%; Width: 100%; background-color: transparent;" v-if="showhud3">
            <div class="leftinfo">
                <i class="textstyle" style="color:#3F6791;text-shadow: 0 0 2px #000;margin-top:4.65vw"><span
                        class="ml-2 text-center" style="font-family: 'Exo', sans-serif;color:white"><i
                            style="font-size: 0.9vw; margin-left: 1.2vw;margin-top: 0.15vw;text-shadow: 0 0 2px #000;color:#3F6791"
                            class="fa-solid fa-solid fa-bars float-left"></i><span
                            style="margin-left: 1vw;font-size:0.75vw">[F2]</span></span></i>
                <i class="textstyle" style="color:#3F6791;text-shadow: 0 0 2px #000"><span class="ml-2 text-center"
                        style="font-family: 'Exo', sans-serif;color:white"><i
                            style="font-size: 0.9vw; margin-left: 1.2vw;margin-top: 0.15vw;text-shadow: 0 0 2px #000;color:#3F6791"
                            class="fa-solid fa-solid fa-mobile-screen float-left"></i><span
                            style="margin-left: 1.1vw;font-size:0.75vw">[F5]</span></span></i>
                <i class="textstyle" style="color:#3F6791;text-shadow: 0 0 2px #000"><span class="ml-2 text-center"
                        style="font-family: 'Exo', sans-serif;color:white"><i
                            style="font-size: 0.9vw; margin-left: 1.2vw;margin-top: 0.15vw;text-shadow: 0 0 2px #000;color:#3F6791"
                            class="fa-solid fa-solid fa-hand-dots float-left"></i><span
                            style="margin-left: 1.05vw;font-size:0.75vw">[X]</span></span></i>
                <i class="textstyle" style="color:#3F6791;text-shadow: 0 0 2px #000"><span class="ml-2 text-center"
                        style="font-family: 'Exo', sans-serif;color:white"><i
                            style="font-size: 0.9vw; margin-left: 1.2vw;margin-top: 0.15vw;text-shadow: 0 0 2px #000;color:#3F6791"
                            class="fa-solid fa-solid fa-user-pen float-left"></i><span
                            style="margin-left: 0.9vw;font-size:0.75vw">[I]</span></span></i>
                <i v-if="voicerp == 1" class="textstyle" style="color:#3F6791;text-shadow: 0 0 2px #000"><span
                        class="ml-2 text-center" style="font-family: 'Exo', sans-serif;color:white"><i
                            style="font-size: 0.9vw; margin-left: 1.2vw;margin-top: 0.15vw;text-shadow: 0 0 2px #000;color:#3F6791"
                            class="fa-solid fa-solid fa-microphone-lines float-left"></i><span
                            style="margin-left: 1.32vw;font-size:0.75vw">[^]</span></span></i>
                <i class="textstyle" style="color:#3F6791;text-shadow: 0 0 2px #000"><span class="ml-2 text-center"
                        style="font-family: 'Exo', sans-serif;color:white"><i
                            style="font-size: 0.9vw; margin-left: 1.2vw;margin-top: 0.16vw;text-shadow: 0 0 2px #000;color:#3F6791"
                            class="fa-solid fa-solid fa-fingerprint float-left"></i><span
                            style="margin-left: 1.02vw;font-size:0.75vw">[F]</span></span></i>
                <i class="textstyle" style="color:#3F6791;text-shadow: 0 0 2px #000"><span class="ml-2 text-center"
                        style="font-family: 'Exo', sans-serif;color:white"><i
                            style="font-size: 0.9vw; margin-left: 1.525vw;margin-top: 0.16vw;text-shadow: 0 0 2px #000;color:#3F6791"
                            class="fa-solid fa-solid fa-arrow-pointer float-left"></i><span
                            style="margin-left: 0.64vw;font-size:0.75vw">[F10]</span></span></i>
            </div>
        </div>
    </div>
</template>

<script>
    import Vue from 'vue'
    import VueEllipseProgress from 'vue-ellipse-progress';
    Vue.use(VueEllipseProgress);
    export default {
        name: 'Speedometer',
        data: function() {
            return {
                voicerp: 1,
                crosshairshow: false,
                crosshair: 0,
                speedometershow: false,
                showhud2: false,
                showhud3: false,
                showhud4: false,
                checkspeedo: false,
                speedProgress: 0,
                speed: 0,
                speed2: 0,
                health: 100,
                fuel: 100,
                oel: 100,
                battery: 100,
                maxfuel: 100,
                setfuel: 100,
                setBattery: 100,
                maxSpeed: 490,
                locked: 2,
                belt: 1,
                vehiclengine: 0,
                id: 0,
                ort: 'Ort',
                time: '00:00',
                windowHeight: window.innerHeight,
                windowWidth: window.innerWidth,
                talkstate: -1,
                talkstate2: 0,
                infomessage: '',
                infotimeout: null,
                weapons: [],
                actualWeapon: '',
                actualAmmo: 0
            }
        },
        watch: {
            windowHeight: function() {
                this.$forceUpdate();
            },
        },
        mounted() {
            this.$nextTick(() => {
                window.addEventListener('resize', this.onResize);
            })
        },
        beforeDestroy() {
            window.removeEventListener('resize', this.onResize);
        },
        methods: {
            getImgUrl(pic) {
                return require('../assets/images/inventory/' + pic + '.png')
            },
            setvoicerp: function(voicerp) {
                this.voicerp = voicerp;
            },
            updateWeaponList: function(weapons, actualWeapon, actualAmmo) {
                this.weapons = JSON.parse(weapons);
                if (this.weapons) {
                    this.weapons = this.weapons.filter(weapon => weapon.type == 5 && weapon.props.split(',')[1] ==
                        1 && !weapon.description.includes("Schutzweste"));
                }
                this.actualWeapon = actualWeapon;
                this.actualAmmo = actualAmmo;
            },
            IsMelee: function(itemname) {
                switch (itemname.toLowerCase()) {
                    case "dolch": {
                        return 1;
                    }
                    case "baseballschläger": {
                        return 1;
                    }
                    case "brechstange": {
                        return 1;
                    }
                    case "taschenlampe": {
                        return 1;
                    }
                    case "golfschläger": {
                        return 1;
                    }
                    case "axt": {
                        return 1;
                    }
                    case "messer": {
                        return 1;
                    }
                    case "machete": {
                        return 1;
                    }
                    case "klappmesser": {
                        return 1;
                    }
                    case "schlagstock": {
                        return 1;
                    }
                    case "poolcue": {
                        return 1;
                    }
                }
                return 0;
            },
            showCrosshair: function(crosshair) {
                this.crosshair = crosshair;
                this.crosshairshow = true;
            },
            hideCrosshair: function() {
                this.crosshairshow = false;
            },
            getCrosshair(crosshair) {
                return require('../assets/images/crosshair/' + crosshair + '.png')
            },
            setInfomessage: function(info, time) {
                if (this.infotimeout != null) {
                    clearTimeout(this.infotimeout);
                }
                this.infomessage = info;
                var self = this;
                this.infotimeout = setTimeout(function() {
                    self.infomessage = '';
                    self.infotimeout = null;
                }, time);
            },
            setTalkState(settalkstate) {
                this.talkstate = settalkstate;
            },
            setTalkState2(settalkstate2) {
                this.talkstate2 = settalkstate2;
            },
            onResize() {
                this.windowHeight = window.innerHeight
                this.windowWidth = window.innerWidth
            },
            showHud2: function(id, ort) {
                this.id = id;
                var replacedort = ort;
                if (this.windowWidth <= 850) {
                    if (ort.length >= 8) {
                        replacedort = ort.substring(0, 7);
                        replacedort = replacedort + '.';
                    }
                } else {
                    if (ort.length >= 21) {
                        replacedort = ort.substring(0, 20);
                        replacedort = replacedort + '.';
                    }
                }
                this.ort = replacedort;
                var a = new Date();
                var b = a.getHours();
                var c = a.getMinutes();
                if (b < 10) b = '0' + b;
                if (c < 10) c = '0' + c;
                var zeit = b + ':' + c;
                this.time = zeit;
                this.showhud2 = !this.showhud2;
                this.showhud3 = !this.showhud3;
                this.showhud4 = !this.showhud4;
                if (this.speedometershow == true) {
                    this.checkspeedo = true;
                    this.speedometershow = false;
                } else {
                    if (this.checkspeedo == true) {
                        this.checkspeedo = false;
                        this.speedometershow = true;
                    }
                }
            },
            hideHud: function() {
                this.showhud2 = false;
                this.showhud3 = false;
                this.showhud4 = false;
                this.speedometershow = false;
                this.checkspeedo = false;
            },
            reloadHud: function() {
                var oldspeedoShow = this.speedometershow;
                this.showhud2 = false;
                this.showhud3 = false;
                this.showhud4 = false;
                this.speedometershow = false;
                var self = this;
                setTimeout(function() {
                    self.showhud2 = true;
                    self.showhud3 = true;
                    self.showhud4 = true;
                    self.speedometershow = oldspeedoShow;
                    self.$forceUpdate();
                }, 555);
            },
            updateHud3: function() {
                this.showhud3 = !this.showhud3;
                this.showhud4 = !this.showhud4;
            },
            updateHud2: function(ort) {
                var replacedort = ort;
                if (this.windowWidth <= 850) {
                    if (ort.length >= 8) {
                        replacedort = ort.substring(0, 7);
                        replacedort = replacedort + '.';
                    }
                } else {
                    if (ort.length >= 21) {
                        replacedort = ort.substring(0, 20);
                        replacedort = replacedort + '.';
                    }
                }
                this.ort = replacedort;
                var a = new Date();
                var b = a.getHours();
                var c = a.getMinutes();
                if (b < 10) b = '0' + b;
                if (c < 10) c = '0' + c;
                var zeit = b + ':' + c;
                this.time = zeit;
            },
            showSpeedometer: function(maxspeed) {
                this.speed = 0;
                this.speedProgress = 0;
                this.speedometershow = !this.speedometershow;
                this.maxspeed = (maxspeed - 2);
            },
            updateMaxSpeed: function(maxspeed) {
                this.maxspeed = (maxspeed - 2);
            },
            updateSpeedometerSpeed: function(speed, health, locked, engine, belt, fuel, maxfuel, battery, oel) {
                speed = parseInt(speed);
                health = parseInt(health);
                if (speed < 0) {
                    speed = 0;
                }
                this.speed2 = speed;
                if (speed > this.maxspeed) {
                    speed = parseInt(this.maxspeed);
                }
                this.speed = speed;
                this.speedProgress = parseInt((100 / this.maxspeed) * speed);
                this.health = parseInt(100 / 1000 * health);
                this.locked = locked;
                this.vehiclengine = parseInt(engine);
                this.belt = parseInt(belt);
                this.fuel = Math.ceil(fuel);
                this.oel = parseInt(oel);
                this.battery = parseInt(battery);
                this.maxfuel = parseInt(maxfuel);
                if (this.maxfuel > 0) {
                    this.setfuel = parseInt((this.fuel * 100) / this.maxfuel);
                } else {
                    this.setfuel = 0;
                }
                if (this.battery > 0) {
                    this.setbattery = parseInt((this.battery * 100) / 100);
                } else {
                    this.setbattery = 0;
                }
            },
        }
    }
</script>

<style scoped>
html,
body,
template,
* {
    -webkit-user-select: none;
    -khtml-user-select: none;
    -moz-user-select: none;
    -o-user-select: none;
    user-select: none;
}

.weaponimg1 {
    height: 2.75vh;
}

.weaponimg2 {
    height: 2.75vh;
    opacity: 0.5;
}

.weaponimg3 {
    height: 2.75vh;
    width: 11.5vh;
}

.weaponimg4 {
    height: 2.75vh;
    width: 11.5vh;
    opacity: 0.5;
}

.weapontext1 {
    font-family: 'Exo', sans-serif;
    z-index: -1;
    text-shadow: 1px 0 0 #000, 0 -1px 0 #000, 0 1px 0 #000, -1px 0 0 #000;
    font-size: 1.015vw;
    color: #689149;
}

.bordericon {
    display: inline-block;
    border-radius: 5vw;
    color: rgba(0, 0, 0, 0.5);
    background-color: rgba(0, 0, 0, 0.5);
    box-shadow: 0 0 2px rgba(0, 0, 0, 0.5);
    padding: 0.5em 0.6em;
}

.kmhtext {
    color: white;
    font-size: 29.5px;
    font-family: 'Exo', sans-serif;
    margin-top: -1.3em;
    text-shadow: 0 0 9px #000;
    text-align: center;
}

.icon {
    font-size: 1.75vh;
    display: block;
    position: relative;
    padding: 0.03vh 1vh;
    margin: 10px 3px 0 5px;
    width: 11.1vh;
    max-width: 11.2;
    height: 2.6vh;
    max-height: 2.7vh;
    border-radius: 10px;
    overflow: hidden;
    background: rgba(0, 0, 0, 0.5);
    text-align: center;
}

.info.vehicle {
    margin: 0;
    position: absolute;
    bottom: 0;
    left: 50%;
    margin-right: -50%;
    transform: translate(-50%, 27%);
    z-index: 3;
    text-align: center;
}

@media (max-height: 900px) {
    .info.vehicle {
        margin: 0;
        position: absolute;
        bottom: 0;
        left: 50%;
        margin-right: -50%;
        transform: translate(-50%, 25%);
        z-index: 3;
        text-align: center;
    }

    .kmhtext {
        color: white;
        font-size: 17px;
        font-family: 'Exo', sans-serif;
        margin-top: -0.6em;
        text-shadow: 0 0 9px #000;
        text-align: center;
    }

    .icon {
        font-size: 1.75vh;
        display: block;
        position: relative;
        padding: 0.13vh 1vh;
        margin: 10px 3px 0 5px;
        width: 11.1vh;
        max-width: 11.2;
        height: 2.6vh;
        max-height: 2.7vh;
        border-radius: 10px;
        overflow: hidden;
        background: rgba(0, 0, 0, 0.5);
        text-align: center;
    }
}

@media (min-width: 1070px) {
    .info.vehicle {
        margin: 0;
        position: absolute;
        bottom: 0;
        left: 50%;
        margin-right: -50%;
        transform: translate(-50%, 25%);
        z-index: -1;
        text-align: center;
    }

    .kmhtext {
        color: white;
        font-size: 1.35vw;
        font-family: 'Exo', sans-serif;
        margin-top: -2.5vw;
        text-shadow: 0 0 9px #000;
        text-align: center;
    }
}

@media (max-height: 720px) and (max-width: 1280px) {
    .info.vehicle {
        margin: 0;
        position: absolute;
        bottom: 0;
        left: 50%;
        margin-right: -50%;
        transform: translate(-50%, 23%);
        z-index: -1;
        text-align: center;
    }

    .kmhtext {
        color: white;
        font-size: 15.5px;
        font-family: 'Exo', sans-serif;
        margin-top: -0.8em;
        text-shadow: 0 0 9px #000;
        text-align: center;
    }
}

@media (min-width: 2600px) {
    .info.vehicle {
        margin: 0;
        position: absolute;
        bottom: 0;
        left: 50%;
        margin-right: -50%;
        transform: translate(-50%, 24%);
        z-index: -1;
        text-align: center;
    }

    .kmhtext {
        color: white;
        font-size: 45px;
        font-family: 'Exo', sans-serif;
        margin-top: -1.8em;
        text-shadow: 0 0 9px #000;
        text-align: center;
    }
}

.info.vehicle.others {
    position: absolute;
    transform: translate(-50%, -5%);
    padding-top: 5%;
    z-index: -1;
    text-align: center;
}

.info.server.others {
    margin: 0;
    position: absolute;
    top: 0.2vh;
    left: 50%;
    margin-bottom: 0.5%;
    transform: translate(-50%, -18%);
    z-index: -1;
    text-align: center;
}

.icon2 {
    font-size: 1.5vh;
    display: block;
    position: relative;
    padding: 0.18vh 1.7vh;
    margin: 10px 0.5px 0 5px;
    width: 12.3vh;
    max-width: 12.4vh;
    height: 2.6vh;
    max-height: 2.7vh;
    border-radius: 10px;
    overflow: hidden;
    background: rgba(0, 0, 0, 0.5);
    text-align: center;
}

@media (max-height: 803px) {
    .icon2 {
        font-size: 1.5vh;
        display: block;
        position: relative;
        padding: 0.165vh 2.0vh;
        margin: 10px 0.5px 0 5px;
        width: 14.1vh;
        max-width: 14.2vh;
        height: 2.6vh;
        max-height: 2.7vh;
        border-radius: 10px;
        overflow: hidden;
        background: rgba(0, 0, 0, 0.5);
        text-align: center;
    }
}

.icon3 {
    font-size: 1.15vh;
    display: block;
    position: relative;
    padding: 0.32vh 0.3vh;
    margin: 0.6vh 0.15vh 0.3vh 0.25vh;
    width: 16vh;
    height: 2.25vh;
    border-radius: 10px;
    overflow: hidden;
    background: rgba(0, 0, 0, 0.5);
    text-align: center;
}

@media (max-width: 1635px) {
    .icon3 {
        font-size: 1.15vh;
        display: block;
        position: relative;
        padding: 0.3vh 7px;
        margin: 0.6vh 0.15vh 0.3vh 0.25vh;
        width: 16vh;
        height: 2.25vh;
        border-radius: 10px;
        overflow: hidden;
        background: rgba(0, 0, 0, 0.5);
        text-align: center;
    }
}

@media (max-width: 1165px) {
    .icon3 {
        font-size: 1.15vh;
        display: block;
        position: relative;
        padding: 0.24vh 7px;
        margin: 0.6vh 0.15vh 0.3vh 0.25vh;
        width: 16vh;
        height: 2.15vh;
        border-radius: 10px;
        overflow: hidden;
        background: rgba(0, 0, 0, 0.5);
        text-align: center;
    }

    .iconnw {
        display: none;
    }
}

@media (max-width: 800px) {
    .icon3 {
        font-size: 1.15vh;
        display: block;
        position: relative;
        padding: 0.30vh 7px;
        margin: 0.6vh 0.15vh 0.3vh 0.25vh;
        width: 16vh;
        height: 2.15vh;
        border-radius: 10px;
        overflow: hidden;
        background: rgba(0, 0, 0, 0.5);
        text-align: center;
    }

    .iconnw {
        display: none;
    }
}

@media (max-height: 1024px) and (max-width: 1280px) {
    .info.vehicle {
        margin: 0;
        position: absolute;
        bottom: 0;
        left: 50%;
        margin-right: -50%;
        transform: translate(-50%, 25%);
        z-index: -1;
        text-align: center;
    }

    .icon3 {
        font-size: 1.15vh;
        display: block;
        position: relative;
        padding: 0.3vh 7px;
        margin: 0.9vh 0.15vh 0.3vh 0.25vh;
        width: 16vh;
        height: 2.25vh;
        border-radius: 10px;
        overflow: hidden;
        background: rgba(0, 0, 0, 0.5);
        text-align: center;
    }

    .iconnw2 {
        display: none;
    }
}

@media (max-height: 768px) and (max-width: 1366px) {
    .kmhtext {
        color: white;
        font-size: 17.5px;
        font-family: 'Exo', sans-serif;
        margin-top: -0.7em;
        text-shadow: 0 0 9px #000;
        text-align: center;
    }
}

.leftinfo {
    margin: 0;
    position: absolute;
    top: 21.5%;
    right: 0.5vw;
    margin-bottom: 0.5%;
    transform: translate(-1%, 21.5%);
    z-index: -1;
    text-align: left;
}

.textstyle {
    background-color: rgba(0, 0, 0, 0.5);
    font-weight: bold;
    border-radius: 2vw;
    width: 6vw;
    height: auto;
    font-size: 0.8vw;
    display: block;
    position: relative;
    margin: 1.2vh 0.15vh 0.2vh 0.15vh;
}

@media (max-width: 800px) {
    .textstyle {
        background-color: rgba(0, 0, 0, 0.5);
        font-weight: bold;
        border-radius: 2vw;
        width: 6vw;
        height: auto;
        font-size: 0.8vw;
        display: block;
        position: relative;
        margin: 1.2vh 0.15vh 0.2vh 0.15vh;
        margin-top: 0.45vw;
    }
}

.crosshair {
    position: absolute;
    right: 50%;
    top: 50%;
    transform: translate(50%, -50%);
}

.weapon {
    position: absolute;
    right: 1.3%;
    top: 94%;
    padding-top: 0.5vw;
}

.weapon2 {
    position: absolute;
    right: 0.2%;
    top: 96.5%;
    padding-top: 0.5vw;
}
</style>
