<template>
<div class="hud" style="overflow-y: auto;background-color:transparent; height:100%; width:100%;scrollbar-width: none;" v-if="showsmartphoneset">
    <div class="centering" style="height: 100%;">
        <div class="centering3 handybox">
            <div class="handybox2" v-if="setting == -2">
                <div class="mt-1" style="display: flex; justify-content: center; align-items: center;">
                    <button v-if="capacity > 0" @click="turnOn()" type="button" class="btn btn-success" style="border-radius: 1vw;margin-top:24.5vw">Anschalten</button>
                    <button v-else @click="loadCapa()" type="button" class="btn btn-success" style="border-radius: 1vw;margin-top:24.5vw">Akku laden</button>
                </div>
            </div>
            <div class="handybox2" v-if="setting == -3">
                <div class="mt-1" style="display: flex; justify-content: center; align-items: center;">
                    <img class="animate__animated animate__heartBeat" src="../assets/images/smartphone/robot.png" style="width: 5vw;padding-top: 8.5vw">
                </div>
                <div class="mt-1" style="display: flex; justify-content: center; align-items: center;">
                    <span class="animate__animated animate__heartBeat" style="font-family: 'Exo', sans-serif;">Nemesus-OS 1.0 loading
                        ...</span>
                </div>
            </div>
            <div class="handybox2" v-if="setting != -2 && setting != -3">
                <div style="margin-top: 0.2vh">
                    <span style="font-family: 'Exo', sans-serif;text-align: left;font-size:0.7vw;margin-top: 0.5v" v-if="smartphone.phonenumber">nMobile</span>
                    <span style="font-family: 'Exo', sans-serif;text-align: left;font-size:0.7vw" v-else>Keine Dienste</span>
                    <img v-if="smartphone.phonenumber" src="../assets/images/smartphone/Empfang.png" style="width: 0.8vw;margin-left:0.4vw;padding-bottom:0.44vh">
                    <span v-if="this.capacity <= 48" style="font-family: 'Exo', sans-serif;float:right;font-size:0.7vw;padding-top:0.8vh">{{parseInt((this.capacity/48)*100)}}%</span>
                    <span v-else style="font-family: 'Exo', sans-serif;float:right;font-size:0.7vw;padding-top:0.8vh">100%</span>
                    <i v-if="smartphone.phonenumber && newmessages == 1" class="fas fa-envelope-open-text" style="float:right;margin-right:0.4vw;font-size:0.7vw;padding-top:0.9vh"></i>
                    <i v-if="smartphone.phonenumber && phonecount == 1" class="fas fa-phone-slash" style="float:right;margin-right:0.4vw;font-size:0.7vw;padding-top:0.9vh"></i>
                    <i v-if="smartphone.phonenumber && inCall" class="fas fa-phone" style="float:right;margin-right:0.4vw;font-size:0.7vw;padding-top:0.945vh;color:green"></i>
                </div>
            </div>
            <div class="handybox3" v-if="setting == 0">
                <div class="handybox3icons" style="margin-top: 1vw">
                    <div v-if="!smartphone.phonenumber" class="animate__animated animate__fadeIn alert alert-dark mr-2 text-center" role="alert" style="border-radius: 2vw">
                        Keine SIM-Karte eingelegt!
                    </div>
                    <div v-if="handyinfo" class="animate__animated animate__fadeIn alert alert-dark mr-2 text-center" role="alert" style="border-radius: 2vw">
                        {{handyinfo}}
                    </div>
                    <img v-if="smartphone.phonenumber" class="appicon2" src="../assets/images/smartphone/Phone.png" @click="showPhone()">
                    <img v-if="smartphone.phonenumber" class="appicon2" src="../assets/images/smartphone/Kontakte.png" @click="showContacts()">
                    <img v-if="smartphone.phonenumber" class="appicon2" src="../assets/images/smartphone/Chat.png" @click="showChatContacts()">
                    <img v-if="smartphone.phonenumber" class="appicon2" src="../assets/images/smartphone/Settings.png" @click="showSettings()">
                    <img v-if="smartphone.phonenumber" class="appicon2" src="../assets/images/smartphone/Banking.png" style="margin-top:0.95vw" @click="showBanking()">
                    <img v-if="smartphone.phonenumber" class="appicon2" src="../assets/images/smartphone/Rechner.png" style="margin-top:0.95vw" @click="showCalculator()">
                    <img v-if="smartphone.phonenumber" class="appicon2" style="margin-top: 1vw;" src="../assets/images/smartphone/Wetter.png" @click="showWeather()">
                    <img v-if="smartphone.phonenumber" class="appicon2" style="margin-top: 1vw;" src="../assets/images/smartphone/Lifeinvader.png" @click="loadLifeInvader()">
                    <img v-if="smartphone.phonenumber" class="appicon2" style="margin-top: 1vw;" src="../assets/images/smartphone/Taxi.png" @click="loadTaxi()">
                    <img v-if="smartphone.phonenumber" class="appicon2" style="margin-top: 1vw;" src="../assets/images/smartphone/Services.png" @click="loadService()">
                    <img v-if="smartphone.phonenumber" class="appicon2" style="margin-top: 1vw;" src="../assets/images/smartphone/Payment.png" @click="loadInvoices()">
                    <img v-if="smartphone.phonenumber" class="appicon2" style="margin-top: 1vw;" src="../assets/images/smartphone/Dispatch.png" @click="loadDispatch()">
                    <img v-if="smartphone.phonenumber && (faction == 1 || faction == 2 || faction == 3)" class="appicon2" style="margin-top: 1vw;" src="../assets/images/smartphone/MDC.png" @click="loadMDC()">
                </div>
            </div>
            <div class="handybox3" v-if="setting == -1" style="margin-top: 1.5vw">
                <h6 style="font-size: 2.2vw; font-family: 'Exo', sans-serif;" class="text-center">{{time}} Uhr</h6>
                <h6 style="font-size: 1vw; font-family: 'Exo', sans-serif" class="text-center">{{date}}</h6>
                <div v-if="newmessages == 1" class="animate__animated animate__fadeIn alert alert-dark mr-4 ml-4 text-center" role="alert" style="border-radius: 2vw; margin-top: 2.2vw;">
                    Neue Nachrichten vorhanden!
                </div>
                <div v-if="phonecount == 1" class="animate__animated animate__fadeIn alert alert-dark mr-4 ml-4 text-center" role="alert" style="border-radius: 2vw; margin-top: 0.5vw;">
                    Anrufe in Abwesenheit vorhanden!
                </div>
                <div v-if="smartphone.prepaid != -1 && smartphone.prepaid < 55" class="animate__animated animate__fadeIn alert alert-dark mr-4 ml-4 text-center" role="alert" style="border-radius: 2vw; margin-top: 0.5vw;">
                    Wenig Guthaben vorhanden!
                </div>
                <img class="appicon2 animate__animated animate__heartBeat" v-if="newmessages == 0 && phonecount == 0" style="width: 5vw;margin-top: 10vw;margin-left:5.7vw;filter: invert(76%) sepia(0%) saturate(3207%) hue-rotate(130deg) brightness(95%) contrast(80%);" src="../assets/images/smartphone/Unlock.png" @click="unlockScreen()">
                <img class="appicon2 animate__animated animate__heartBeat" v-if="(newmessages == 1 && phonecount == 0) || (newmessages == 0 && phonecount == 1)" style="width: 5vw;margin-top: 7vw;margin-left:5.7vw;filter: invert(76%) sepia(0%) saturate(3207%) hue-rotate(130deg) brightness(95%) contrast(80%);" src="../assets/images/smartphone/Unlock.png" @click="unlockScreen()">
                <img class="appicon2 animate__animated animate__heartBeat" v-if="(newmessages == 1 && phonecount == 1)" style="width: 5vw;margin-top: 3.5vw;margin-left:5.7vw;filter: invert(76%) sepia(0%) saturate(3207%) hue-rotate(130deg) brightness(95%) contrast(80%);" src="../assets/images/smartphone/Unlock.png" @click="unlockScreen()">
            </div>
            <div class="handybox3" v-if="setting == 1" style="overflow: auto;scrollbar-width: none;height:24.5vw;margin-top:-0.55vw">
                <div class="content" style="margin-top: 0.15vw">
                    <div class="card-header text-center" style="font-family: 'Exo', sans-serif;background-color:#485159;margin-left:0.14vw;border-color:#3F6791">
                        <div style="margin-top:0.25vw">
                            Einstellungen
                            <i class="fas fa-arrow-left float-left iconback" style="margin-top: 0.2vw" @click="backToHome()"></i>
                            <i class="fas fa-power-off float-right iconoff" @click="turnOff()" style="margin-top: 0.20vw"></i>
                            <i v-if="capacity < 48" class="fas fa-battery-full float-right hovericon" @click="loadCapa()" style="margin-top: 0.20vw; margin-left: 0.3vw"></i>
                            <i v-if="this.prepaid == -1" class="fa-solid fa-file-circle-xmark float-right hovericon" @click="closeHandy()" style="margin-top: 0.20vw; margin-left: 0.05vw"></i>
                        </div>
                    </div>
                    <div class="card-body">
                        <ul class="list-group list-group-bordered mt-1" style="font-size: 0.7vw">
                            <li>
                                <b>Nummer</b> <a class="float-right">{{smartphone.phonenumber}}</a>
                            </li>
                            <li v-if="this.capacity <= 48">
                                <b>Akkustatus</b> <a class="float-right">{{parseInt((this.capacity/48)*100)}}%</a>
                            </li>
                            <li v-else>
                                <b>Akkustatus</b> <a class="float-right">100%</a>
                            </li>
                            <li v-if="this.prepaid == -1">
                                <b>Anbieter</b> <a class="float-right">nMobile</a>
                            </li>
                            <li v-else>
                                <b>Anbieter</b> <a class="float-right">Prepaid</a>
                            </li>
                            <li v-if="smartphone.shownumber">
                                <b>Nummer</b> <a class="float-right" @click="showSetNumber()">Versteckt</a>
                            </li>
                            <li v-else>
                                <b>Nummer</b> <a class="float-right" @click="showSetNumber()">Öffentlich</a>
                            </li>
                            <li v-if="smartphone.notificationmessages">
                                <b>Benachrichtigungen</b> <a class="float-right" @click="showSetNotification()">An</a>
                            </li>
                            <li v-else>
                                <b>Benachrichtigungen</b> <a class="float-right" @click="showSetNotification()">Aus</a>
                            </li>
                            <li v-if="smartphone.phonestatus == 3 || smartphone.phonestatus == 0">
                                <b>Status</b> <a class="float-right" @click="setPhoneStatus()">Lautlos</a>
                            </li>
                            <li v-if="smartphone.phonestatus == 2">
                                <b>Status</b> <a class="float-right" @click="setPhoneStatus()">Vibration</a>
                            </li>
                            <li v-if="smartphone.phonestatus == 1">
                                <b>Status</b> <a class="float-right" @click="setPhoneStatus()">Ton</a>
                            </li>
                            <hr />
                            <li class="mt-1">
                                <b>Hintergrundbild</b> <a class="float-right badge badge-primary" style="border-radius: 1vw;margin-right: 0.6vw;font-size: 0.9vw" @click="showWallpaper()">Auswahl</a>
                            </li>
                            <li class="mt-2">
                                <b>Klingelton</b> <a class="float-right badge badge-primary" style="border-radius: 1vw;margin-right: 0.6vw;font-size: 0.9vw" @click="showMusic()">Auswahl</a>
                            </li>
                            <li class="mt-2" v-if="faction == 1 || faction == 2 || faction == 3">
                                <b>Status</b>
                                <div class="col-6 float-right"><input type="text" maxlength="10" class="float-right form-control text-center" placeholder="Status" style="border-radius: 1vw" v-model="playerStatus" v-on:keyup.enter="setStatus()">
                                </div>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
            <div class="handybox3" v-if="setting == 2" style="overflow-x: auto;scrollbar-width: none;height:25.5vw;">
                <div class="content" style="margin-top: 0.15vw">
                    <div class="card-header text-center" style="font-family: 'Exo', sans-serif;background-color:#485159;margin-left:0.15vw;border-color:#3F6791">
                        <div style="margin-top:0.2vw">
                            Hintergrundauswahl
                            <i class="fas fa-arrow-left float-left iconback" style="margin-top: 0.2vw" @click="showSettings()"></i>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="mt-1" style="display: flex; justify-content: center; align-items: center;">
                            <div class="row mt-2" style="padding-left: 1.4vw;">
                                <img style="width: 5vw; height: 6vw; border-radius: 1vw; padding: 10px 10px 10px 10px;" @click="selectWallpaper(1)" src="../assets/images/smartphone/Wallpaper1.png">
                                <img style="width: 5vw; height: 6vw; border-radius: 1vw; padding: 10px 10px 10px 10px;" @click="selectWallpaper(2)" src="../assets/images/smartphone/Wallpaper2.png">
                                <img style="width: 5vw; height: 6vw; border-radius: 1vw; padding: 10px 10px 10px 10px;" @click="selectWallpaper(3)" src="../assets/images/smartphone/Wallpaper3.png">
                                <img style="width: 5vw; height: 6vw; border-radius: 1vw; padding: 10px 10px 10px 10px;" @click="selectWallpaper(4)" src="../assets/images/smartphone/Wallpaper4.png">
                                <img style="width: 5vw; height: 6vw; border-radius: 1vw; padding: 10px 10px 10px 10px;" @click="selectWallpaper(5)" src="../assets/images/smartphone/Wallpaper5.png">
                                <img style="width: 5vw; height: 6vw; border-radius: 1vw; padding: 10px 10px 10px 10px;" @click="selectWallpaper(6)" src="../assets/images/smartphone/Wallpaper6.png">
                                <img style="width: 5vw; height: 6vw; border-radius: 1vw; padding: 10px 10px 10px 10px;" @click="selectWallpaper(7)" src="../assets/images/smartphone/Wallpaper7.png">
                                <img style="width: 5vw; height: 6vw; border-radius: 1vw; padding: 10px 10px 10px 10px;" @click="selectWallpaper(8)" src="../assets/images/smartphone/Wallpaper8.png">
                                <img style="width: 5vw; height: 6vw; border-radius: 1vw; padding: 10px 10px 10px 10px;" @click="selectWallpaper(9)" src="../assets/images/smartphone/Wallpaper9.png">
                                <img style="width: 5vw; height: 6vw; border-radius: 1vw; padding: 10px 10px 10px 10px;" @click="selectWallpaper(10)" src="../assets/images/smartphone/Wallpaper10.gif">
                                <img style="width: 5vw; height: 6vw; border-radius: 1vw; padding: 10px 10px 10px 10px;" @click="selectWallpaper(11)" src="../assets/images/smartphone/Wallpaper11.gif">
                                <img style="width: 5vw; height: 6vw; border-radius: 1vw; padding: 10px 10px 10px 10px;" @click="selectWallpaper(12)" src="../assets/images/smartphone/Wallpaper12.gif">
                                <img style="width: 5vw; height: 6vw; border-radius: 1vw; padding: 10px 10px 10px 10px;" @click="selectWallpaper(13)" src="../assets/images/smartphone/Wallpaper13.gif">
                                <img style="width: 5vw; height: 6vw; border-radius: 1vw; padding: 10px 10px 10px 10px;" @click="selectWallpaper(14)" src="../assets/images/smartphone/Wallpaper14.gif">
                                <img style="width: 5vw; height: 6vw; border-radius: 1vw; padding: 10px 10px 10px 10px;" @click="selectWallpaper(15)" src="../assets/images/smartphone/Wallpaper15.gif">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="handybox3" v-if="setting == 10" style="overflow-x: auto;scrollbar-width: none;height:25.5vw;">
                <div class="content" style="margin-top: 0.15vw">
                    <div class="card-header text-center" style="font-family: 'Exo', sans-serif;background-color:#485159;margin-left:0.15vw;border-color:#3F6791">
                        <div style="margin-top:0.2vw">
                            Tonauswahl
                            <i class="fas fa-arrow-left float-left iconback" style="margin-top: 0.2vw" @click="showSettings2()"></i>
                        </div>
                    </div>
                    <div class="col-md-12" style="margin-top: 0.7vw">
                        <span style="margin-left: 1vw;font-size:1.1vw;font-size:1.1vw;font-family: 'Exo', sans-serif;">Benachrichtigung (Ton:
                            {{this.smartphone.notification}}):</span>
                        <div class="row" style="margin-left: 2.5vw;margin-top: 0.5vw">
                            <span>Ton 1:</span>
                            <i class="fas fa-play float-left iconback" style="margin-top: 0.2vw;margin-left: 6.8vw" @click="playMessageSound('notification1.mp3')"></i>
                            <i class="fas fa-check float-left iconback" style="margin-left: 0.8vw;margin-top: 0.25vw" @click="setNotification(1)"></i>
                        </div>
                        <div class="row" style="margin-left: 2.5vw;margin-top: 0.5vw">
                            <span>Ton 2:</span>
                            <i class="fas fa-play float-left iconback" style="margin-top: 0.2vw;margin-left: 6.8vw" @click="playMessageSound('notification2.mp3')"></i>
                            <i class="fas fa-check float-left iconback" style="margin-left: 0.8vw;margin-top: 0.25vw" @click="setNotification(2)"></i>
                        </div>
                        <div class="row" style="margin-left: 2.5vw;margin-top: 0.5vw">
                            <span>Ton 3:</span>
                            <i class="fas fa-play float-left iconback" style="margin-top: 0.2vw;margin-left: 6.8vw" @click="playMessageSound('notification3.mp3')"></i>
                            <i class="fas fa-check float-left iconback" style="margin-left: 0.8vw;margin-top: 0.25vw" @click="setNotification(3)"></i>
                        </div>
                        <div class="row" style="margin-left: 2.5vw;margin-top: 0.5vw">
                            <span>Ton 4:</span>
                            <i class="fas fa-play float-left iconback" style="margin-top: 0.2vw;margin-left: 6.8vw" @click="playMessageSound('notification4.mp3')"></i>
                            <i class="fas fa-check float-left iconback" style="margin-left: 0.8vw;margin-top: 0.25vw" @click="setNotification(4)"></i>
                        </div>
                        <div class="row" style="margin-left: 2.5vw;margin-top: 0.5vw">
                            <span>Ton 5:</span>
                            <i class="fas fa-play float-left iconback" style="margin-top: 0.2vw;margin-left: 6.8vw" @click="playMessageSound('notification5.mp3')"></i>
                            <i class="fas fa-check float-left iconback" style="margin-left: 0.8vw;margin-top: 0.25vw" @click="setNotification(5)"></i>
                        </div>
                    </div>
                    <div class="col-md-12" style="margin-top: 0.7vw">
                        <span style="margin-left: 1vw;font-size:1.1vw;font-size:1.1vw;font-family: 'Exo', sans-serif;">Klingelton (Ton: {{this.smartphone.ringtone}}):</span>
                        <div class="row" style="margin-left: 2.5vw;margin-top: 0.5vw">
                            <span>Ton 1:</span>
                            <i class="fas fa-play float-left iconback" style="margin-top: 0.2vw;margin-left: 6.8vw" @click="playRingtone('ringtone1.mp3', 1)"></i>
                            <i class="fas fa-check float-left iconback" style="margin-left: 0.8vw;margin-top: 0.25vw" @click="setRingtone(1)"></i>
                        </div>
                        <div class="row" style="margin-left: 2.5vw;margin-top: 0.5vw">
                            <span>Ton 2:</span>
                            <i class="fas fa-play float-left iconback" style="margin-top: 0.2vw;margin-left: 6.8vw" @click="playRingtone('ringtone2.mp3', 1)"></i>
                            <i class="fas fa-check float-left iconback" style="margin-left: 0.8vw;margin-top: 0.25vw" @click="setRingtone(2)"></i>
                        </div>
                        <div class="row" style="margin-left: 2.5vw;margin-top: 0.5vw">
                            <span>Ton 3:</span>
                            <i class="fas fa-play float-left iconback" style="margin-top: 0.2vw;margin-left: 6.8vw" @click="playRingtone('ringtone3.mp3', 1)"></i>
                            <i class="fas fa-check float-left iconback" style="margin-left: 0.8vw;margin-top: 0.25vw" @click="setRingtone(3)"></i>
                        </div>
                        <div class="row" style="margin-left: 2.5vw;margin-top: 0.5vw">
                            <span>Ton 4:</span>
                            <i class="fas fa-play float-left iconback" style="margin-top: 0.2vw;margin-left: 6.8vw" @click="playRingtone('ringtone4.mp3', 1)"></i>
                            <i class="fas fa-check float-left iconback" style="margin-left: 0.8vw;margin-top: 0.25vw" @click="setRingtone(4)"></i>
                        </div>
                    </div>
                </div>
            </div>
            <div class="handybox3" v-if="setting == 11" style="overflow: auto;scrollbar-width: none;height:25.5vw;">
                <div class="content" style="margin-top: 0.15vw">
                    <div class="card-header text-center" style="font-family: 'Exo', sans-serif;background-color:#485159;margin-left:0.14vw;border-color:#3F6791">
                        Telefon
                        <i class="fas fa-arrow-left float-left iconback" style="margin-top: 0.20vw" @click="backToHome()"></i>
                        <i class="fas fa-user float-right iconback" style="margin-top: 0.20vw" @click="loadCallLogs()"></i>
                        <i class="fas fa-phone float-right iconback" style="margin-right: 0.6vw;margin-top: 0.20vw" @click="setting = 11"></i>
                    </div>
                    <div class="col-md-12">
                        <input type="text" v-model="callnumber" maxlength="10" class="form-control text-center" placeholder="Nummer" style="border-radius: 1vw;margin-top: 0.85vw;font-family: 'Exo', sans-serif;width: 15vw;margin-left:0.8vw" v-on:keyup.enter="setCall()">
                        <div class="handybox3icons2" style="margin-top: 1vw; margin-left: 1vw">
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="orderphone">
                                        <button @click="addSign(1)" type="button" class="btn btn-primary calculatoritem">1</button>
                                        <button @click="addSign(2)" type="button" class="btn btn-primary calculatoritem">2</button>
                                        <button @click="addSign(3)" type="button" class="btn btn-primary calculatoritem">3</button>
                                        <button @click="addSign(4)" type="button" class="btn btn-primary calculatoritem">4</button>
                                        <button @click="addSign(5)" type="button" class="btn btn-primary calculatoritem">5</button>
                                        <button @click="addSign(6)" type="button" class="btn btn-primary calculatoritem">6</button>
                                        <button @click="addSign(7)" type="button" class="btn btn-primary calculatoritem">7</button>
                                        <button @click="addSign(8)" type="button" class="btn btn-primary calculatoritem">8</button>
                                        <button @click="addSign(9)" type="button" class="btn btn-primary calculatoritem">9</button>
                                        <button @click="addSign('*')" type="button" class="btn btn-primary calculatoritem">*</button>
                                        <button @click="addSign(0)" type="button" class="btn btn-primary calculatoritem">0</button>
                                        <button @click="addSign('#')" type="button" class="btn btn-primary calculatoritem">#</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row orderphone" style="display: flex; justify-content: center; align-items: center;">
                            <button @click="setCall()" type="button" class="btn btn-success" style="border-radius: 1vw; margin-top:2vw;">Anrufen</button>
                            <button @click="deleteSign()" type="button" class="btn btn-danger" style="border-radius: 1vw; margin-top:2vw;margin-left: 0.5vw">Löschen</button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="handybox3" v-if="setting == 18" style="overflow: auto;scrollbar-width: none;height:25.5vw;">
                <div class="content" style="margin-top: 0.15vw">
                    <div class="card-header text-center" style="font-family: 'Exo', sans-serif;background-color:#485159;margin-left:0.14vw;border-color:#3F6791">
                        Anruf <span class="badge badge-danger ml-2" style="font-size: 0.7vw" v-if="emergency == 1">Notruf</span>
                        <i class="fas fa-arrow-left float-left iconback" style="margin-top: 0.20vw" @click="backToHome()"></i>
                        <i v-if="!muted" class="fa-solid fa-phone-slash float-right" style="margin-top: 0.20vw;color:green" @click="muteMicro()"></i>
                        <i v-else class="fa-solid fa-phone-slash float-right" style="margin-top: 0.20vw;color:red" @click="muteMicro()"></i>
                    </div>
                    <div class="col-md-12">
                        <div style="display: flex; justify-content: center; align-items: center;margin-top:1.8vw;font-family: 'Exo', sans-serif;">
                            <span style="font-size: 1.12vw">Anruf</span>
                        </div>
                        <div style="display: flex; justify-content: center; align-items: center;font-family: 'Exo', sans-serif;">
                            <span class="text-muted" style="font-size: 0.8vw">Dauer: {{getCallTime()}}</span>
                        </div>
                        <div style="display: flex; justify-content: center; align-items: center;margin-top:1.1vw;font-family: 'Exo', sans-serif;">
                            <img style="width:7vw" src="https://cdn-icons-png.flaticon.com/512/1177/1177568.png">
                        </div>
                        <div style="display: flex; justify-content: center; align-items: center;margin-top:1.5vw;font-family: 'Exo', sans-serif;">
                            <span style="font-size: 1.5vw;margin-right:0.05vw" v-if="hidden == 0">{{getPhoneContact(getcallnumber)}}</span>
                            <span style="font-size: 1.5vw;margin-right:0.05vw" v-if="hidden == 1">Unbekannt</span>
                        </div>
                        <div class="row orderphone" style="display: flex; justify-content: center; align-items: center;margin-top:0.3vw">
                            <button v-if="inCall.length >= 3" type="button" class="btn btn-danger" style="border-radius: 1vw; margin-top:2vw; margin-right: 0.3vw" @click="endcallServer()">Auflegen</button>
                            <button v-if="inCall == 1" @click="acceptCall()" type="button" class="btn btn-success" style="border-radius: 1vw; margin-top:2vw;">Annehmen</button>
                            <button v-if="inCall == 1" @click="declineCall()" type="button" class="btn btn-danger" style="border-radius: 1vw; margin-top:2vw;margin-left: 0.9vw">Ablehnen</button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="handybox3" v-if="setting == 24" style="overflow: auto;scrollbar-width: none;height:25.5vw;">
                <div class="content" style="margin-top: 0.15vw">
                    <div class="card-header text-center" style="font-family: 'Exo', sans-serif;background-color:#485159;margin-left:0.14vw;border-color:#3F6791">
                        nPayment
                        <i class="fas fa-arrow-left float-left iconback" style="margin-top: 0.20vw" @click="backToHome()"></i>
                        <i class="fa-brands fa-creative-commons-nd float-right iconback" @click="setting = 25" style="margin-top: 0.20vw"></i>
                    </div>
                    <div class="col-md-12">
                        <div v-if="handyinfo" class="animate__animated animate__fadeIn alert alert-primary mr-2 text-center" role="alert" style="border-radius: 2vw; margin-top:1.2vw; margin-bottom:1.5vw">
                            {{handyinfo}}
                        </div>
                        <div class="form-group" style="margin-top: 0.3vw" v-if="!handyinfo">
                            <label>Rechnungsempfänger</label>
                            <input type="text" class="form-control" name="empfänger" id="empfänger" placeholder="Privat/Firma/Staat" maxlength="64" autocomplete="off" style="border-radius: 1vw" v-model="banknumber">
                        </div>
                        <div class="form-group" v-if="!handyinfo">
                            <label>Betrag in $</label>
                            <input type="text" class="form-control" name="betrag" placeholder="500" maxlength="10" autocomplete="off" style="border-radius: 1vw" v-model="bankvalue">
                        </div>
                        <div class="form-group" v-if="!handyinfo">
                            <label>Rechnungstext</label>
                            <textarea class="form-control" style="border-radius: 1vw" v-model="banktext" maxlength="64" rows="2" placeholder="Autoverkauf"></textarea>
                        </div>
                        <div style="display: flex; justify-content: center; align-items: center;">
                            <button v-if="!handyinfo" type="submit" class="btn btn-block btn-outline-primary mt-1" style="border-radius: 1vw" @click="inVoiceText()">{{ivText}}</button>
                        </div>
                        <button v-if="!handyinfo" type="submit" class="btn btn-block btn-primary btn-lg mt-2" style="border-radius: 1vw" @click="startInvoice()">Rechnung ausstellen</button>
                    </div>
                </div>
            </div>
            <div class="handybox3" v-if="setting == 25" style="overflow: auto;scrollbar-width: none;height:25.5vw;">
                <div class="content" style="margin-top: 0.15vw">
                    <div class="card-header text-center" style="font-family: 'Exo', sans-serif;background-color:#485159;margin-left:0.14vw;border-color:#3F6791">
                        Rechnungsübersicht
                        <i class="fas fa-arrow-left float-left iconback" style="margin-top: 0.20vw" @click="setting = 24"></i>
                        <div class="input-group mb-3" style="margin-top: 0.7vw">
                            <div class="input-group-prepend">
                                <span class="input-group-text" style="border-radius: 1vw"><i class="fas fa-search"></i></span>
                            </div>
                            <input type="text" v-model="searchelement" v-on:input="searchelement = $event.target.value" maxlength="18" class="form-control" placeholder="Suche..." style="border-radius: 1vw;margin-left: 0.45vw">
                        </div>
                    </div>
                    <div class="col-md-12 mt-4">
                        <div v-if="invoices.length > 0">
                            <div class="card" v-for="invoice in filteredList5" :key="invoice.id">
                                <div class="card-header">
                                    <h3 class="card-title">Rechnung: {{invoice.id}}, Summe: {{invoice.value}}$</h3>
                                    <div class="card-tools">
                                        <i class="fa-solid fa-user-vneck" style="margin-top: 0.20vw"></i>
                                    </div>
                                </div>
                                <div class="card-body">
                                    {{invoice.text}}
                                    <div class="mt-1">
                                        Empfängerkonto: {{invoice.banknumber}}
                                    </div>
                                    <div class="mt-1">
                                        Ausgestellt am: {{timeConverter(invoice.timestamp)}}
                                    </div>
                                </div>
                                <div class="card-footer">
                                    <div style="display: flex; justify-content: center; align-items: center;">
                                        <i class="fa-solid fa-file-invoice-dollar float-left iconback" style="margin-top: 0.20vw; margin-right: 0.35vw" @click="payInvoice(invoice.id)"></i>
                                        <i v-if="invoice.banknumber != 'SA3701-100000'" class="fa-solid fa-square-check float-left iconback" style="margin-top: 0.20vw; margin-right: 0.35vw" @click="acceptInvoice(invoice.id)"></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card" v-if="invoices.length <= 0">
                            <div class="text-center" style="display: flex; justify-content: center; align-items: center;font-family: 'Exo', sans-serif;margin-top:0.1vw">
                                Keine Rechnungen vorhanden!
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="handybox3" v-if="setting == 26" style="overflow: auto;scrollbar-width: none;height:25.5vw;">
                <div class="content" style="margin-top: 0.15vw">
                    <div class="card-header text-center" style="font-family: 'Exo', sans-serif;background-color:#485159;margin-left:0.14vw;border-color:#3F6791">
                        Dispatch (Notruf)
                        <i class="fas fa-arrow-left float-left iconback" style="margin-top: 0.20vw" @click="backToHome()"></i>
                        <img style="width: 2vw; z-index:" class="float-right" src="../assets/images/lslogo.png">
                    </div>
                    <div class="text-center mt-3">
                        <strong class="text-center">Bitte auswählen:</strong>
                    </div>
                    <div class="col-md-12 mt-4">
                        <div class="info-box">
                            <span class="info-box-icon bg-primary"><img style="width: 2vw; z-index:" class="float-right" src="../assets/images/police.png"></span>
                            <div class="info-box-content">
                                <span class="info-box-text"><strong>Police-Department</strong></span>
                                <span class="info-box-number text-muted"><a @click="sendDispatch(1)">Dispatch senden</a></span>
                            </div>
                        </div>
                        <div class="info-box">
                            <span class="info-box-icon bg-danger"><img style="width: 2vw; z-index:" class="float-right" src="../assets/images/medic.png"></span>
                            <div class="info-box-content">
                                <span class="info-box-text"><strong>Rescue-Center</strong></span>
                                <span class="info-box-number text-muted"><a @click="sendDispatch(2)">Dispatch senden</a></span>
                            </div>
                        </div>
                        <div class="info-box">
                            <span class="info-box-icon bg-warning"><img style="width: 2vw; z-index:" class="float-right" src="../assets/images/towing.png"></span>
                            <div class="info-box-content">
                                <span class="info-box-text"><strong>Los Santos Customs</strong></span>
                                <span class="info-box-number text-muted"><a @click="sendDispatch(3)">Dispatch senden</a></span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="handybox3" v-if="setting == 27" style="overflow: auto;scrollbar-width: none;height:25.5vw;">
                <div class="content" style="margin-top: 0.15vw">
                    <div class="card-header text-center" style="font-family: 'Exo', sans-serif;background-color:#485159;margin-left:0.14vw;border-color:#3F6791">
                        Dispatch (Notruf)
                        <i class="fas fa-arrow-left float-left iconback" style="margin-top: 0.20vw" @click="setting = 26"></i>
                        <img style="width: 2vw; z-index:" class="float-right" src="../assets/images/lslogo.png">
                    </div>
                    <div class="col-md-12 mt-4">
                        <div class="form-group" style="margin-top: 0.05vw" v-if="!handyinfo">
                            <label>Ihr Name</label>
                            <input type="text" class="form-control" name="empfänger" id="empfänger" value="SA3701-" placeholder="Name" maxlength="35" autocomplete="off" style="border-radius: 1vw" v-model="banknumber">
                            <label class="mt-2">Sachverhalt</label>
                            <textarea style="resize: none;border-radius: 1vw" class="form-control" maxlength="225" rows="6" v-model="fahndungText" placeholder="Text ..."></textarea>
                            <div style="display: flex; justify-content: center; align-items: center;margin-top: 1vw">
                                <button @click="createDispatch()" type="button" class="btn btn-primary mr-3 ml-3 mb-2 mt-2" style="border-radius: 1vw">Senden</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="handybox3" v-if="setting == 21" style="overflow: auto;scrollbar-width: none;height:25.5vw;">
                <div class="content" style="margin-top: 0.15vw">
                    <div class="card-header text-center" style="font-family: 'Exo', sans-serif;background-color:#485159;margin-left:0.14vw;border-color:#3F6791">
                        nTaxi
                        <i class="fas fa-arrow-left float-left iconback" style="margin-top: 0.20vw" @click="backToHome()"></i>
                        <i v-if="istaxi > -1" class="fa-brands fa-creative-commons-nd float-right iconback" @click="setting = 22" style="margin-top: 0.20vw"></i>
                    </div>
                    <div class="col-md-12">
                        <div v-if="handyinfo" class="animate__animated animate__fadeIn alert alert-primary mr-2 text-center" role="alert" style="border-radius: 2vw; margin-top:1.2vw">
                            {{handyinfo}}
                        </div>
                        <div style="display: flex; justify-content: center; align-items: center;">
                            <img class="weathericon" style="width:3vw;margin-top:2.25vw" src="../assets/images/smartphone/TaxiStock.png">
                        </div>
                        <div style="display: flex; justify-content: center; align-items: center;margin-top: 1vw">
                            <strong>Verfügbare Taxifahrer: {{taxidrivers}}</strong>
                        </div>
                        <div v-if="taxidrivers > 0">
                            <div v-if="found1 == false">
                                <div style="display: flex; justify-content: center; align-items: center;margin-top: 1vw">
                                    <div class="input-group mb-3" style="margin-top: 0.5vw">
                                        <textarea class="form-control" style="border-radius: 1vw" v-model="contactname" maxlength="64" rows="3" v-on:keyup.enter="createTaxi()" placeholder="Text ..."></textarea>
                                    </div>
                                </div>
                                <div style="display: flex; justify-content: center; align-items: center;margin-top: 0.5vw">
                                    <button @click="createTaxi()" type="button" class="btn btn-primary mr-3 ml-3 mb-2 mt-2" style="border-radius: 1vw">Taxi bestellen</button>
                                </div>
                            </div>
                            <div v-else>
                                <div class="card mt-4 text-center">
                                    <div style="display: flex; justify-content: center; align-items: center;font-family: 'Exo', sans-serif;margin-top:0.1vw">
                                        Dein aktueller Auftrag muss zuerst beendet werden, damit du einen neuen Auftrag stellen kannst ...
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div v-else>
                            <div class="card mt-4 text-center">
                                <div style="display: flex; justify-content: center; align-items: center;font-family: 'Exo', sans-serif;margin-top:0.1vw">
                                    Zurzeit sind keine Taxifahrer im Dienst!
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="handybox3" v-if="setting == 22" style="overflow: auto;scrollbar-width: none;height:25.5vw;">
                <div class="content" style="margin-top: 0.15vw">
                    <div class="card-header text-center" style="font-family: 'Exo', sans-serif;background-color:#485159;margin-left:0.14vw;border-color:#3F6791">
                        Taxiaufträge
                        <i class="fas fa-arrow-left float-left iconback" style="margin-top: 0.20vw" @click="loadTaxi()"></i>
                    </div>
                    <div class="col-md-12 mt-4">
                        <div v-if="taxijobs.length > 0">
                            <div class="card" v-for="taxi in taxijobs" :key="taxi.id">
                                <div v-if="taxi.done == 'n/A' || taxi.done == charname">
                                    <div class="card-header">
                                        <h3 class="card-title">{{taxi.name}}</h3>
                                        <div class="card-tools">
                                            <img style="width:1.2vw;margin-top:-0.35vw" src="../assets/images/smartphone/TaxiStock.png">
                                        </div>
                                    </div>
                                    <div class="card-body">
                                        <div v-if="taxi.done == charname">
                                            <span class="badge badge-success float-right" style="border-radius: 1vw;font-size:0.70vw">Aktiver
                                                Auftrag</span>
                                        </div>
                                        {{taxi.text}}
                                    </div>
                                    <div class="card-footer">
                                        <div style="display: flex; justify-content: center; align-items: center;">
                                            <i class="fa-solid fa-phone float-left iconback" style="margin-top: 0.20vw;margin-right:1vw" @click="setCall(taxi.nummer)"></i>
                                            <i v-if="taxi.done == 'n/A'" class="fa-solid fa-square-check float-left iconback" style="margin-top: 0.20vw;margin-right:1vw" @click="acceptTaxi(taxi.id)"></i>
                                            <i class="fa-solid fa-square-minus float-left iconoff" style="margin-top: 0.20vw;margin-right:1vw" @click="declineTaxi(taxi.id)"></i>
                                        </div>
                                    </div>
                                </div>
                                <div v-else>
                                    <div class="text-center" style="display: flex; justify-content: center; align-items: center;font-family: 'Exo', sans-serif;margin-top:0.1vw">
                                        Alle Aufträge sind aktuell in Bearbeitung, warte auf neue Aufträge ...
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card" v-if="taxijobs.length <= 0">
                            <div style="display: flex; justify-content: center; align-items: center;font-family: 'Exo', sans-serif;margin-top:0.1vw">
                                Keine Aufträge vorhanden!
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="handybox3" v-if="setting == 28" style="overflow: auto;scrollbar-width: none;height:25.5vw;">
                <div class="content" style="margin-top: 0.15vw">
                    <div class="card-header text-center" style="font-family: 'Exo', sans-serif;background-color:#485159;margin-left:0.14vw;border-color:#3F6791">
                        Offene Anzeigen
                        <i class="fa-brands fa-creative-commons-nd float-right iconback" @click="setting = 28" style="margin-top: 0.20vw"></i>
                        <i class="fa-solid fa-circle-plus float-right iconback mr-1" @click="setting = 29" style="margin-top: 0.20vw"></i>
                        <i v-if="islifeinvader > -1" class="fa-solid fa-lock float-right iconback mr-1" @click="setting = 30" style="margin-top: 0.20vw"></i>
                        <i class="fas fa-arrow-left float-left iconback" style="margin-top: 0.20vw" @click="backToHome()"></i>
                        <div class="input-group mb-3" style="margin-top: 0.7vw">
                            <div class="input-group-prepend">
                                <span class="input-group-text" style="border-radius: 1vw"><i class="fas fa-search"></i></span>
                            </div>
                            <input type="text" v-model="searchelement" v-on:input="searchelement = $event.target.value" maxlength="18" class="form-control" placeholder="Suche..." style="border-radius: 1vw;margin-left: 0.45vw">
                        </div>
                    </div>
                    <div class="col-md-12 mt-4">
                        <div v-if="ads.length > 0">
                            <div class="card" v-for="ad in filteredList6" :key="ad.id">
                                <div v-if="ad.status == 'Freigegeben'">
                                    <div class="card-header">
                                        <h3 class="card-title">Anzeige von {{ad.name}}</h3>
                                        <div class="card-tools">
                                            <i class="fa-solid fa-user-vneck" style="margin-top: 0.20vw"></i>
                                        </div>
                                    </div>
                                    <div class="card-body">
                                        {{ad.text}}
                                        <div class="mt-2">
                                            Telefonnummer: {{ad.number}}
                                        </div>
                                    </div>
                                    <div class="card-footer">
                                        <div style="display: flex; justify-content: center; align-items: center;">
                                            <i class="fa-solid fa-phone float-left iconback" style="margin-top: 0.20vw;" @click="setCall(ad.number)"></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div v-else>
                            <div class="text-center" style="display: flex; justify-content: center; align-items: center;font-family: 'Exo', sans-serif;margin-top:0.1vw">
                                Keine offenen Anzeigen vorhanden!
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="handybox3" v-if="setting == 30" style="overflow: auto;scrollbar-width: none;height:25.5vw;">
                <div class="content" style="margin-top: 0.15vw">
                    <div class="card-header text-center" style="font-family: 'Exo', sans-serif;background-color:#485159;margin-left:0.14vw;border-color:#3F6791">
                        Anzeigen verwalten
                        <i class="fa-brands fa-creative-commons-nd float-right iconback" @click="setting = 28" style="margin-top: 0.20vw"></i>
                        <i class="fa-solid fa-circle-plus float-right iconback mr-1" @click="setting = 29" style="margin-top: 0.20vw"></i>
                        <i v-if="islifeinvader > -1" class="fa-solid fa-lock float-right iconback mr-1" @click="setting = 30" style="margin-top: 0.20vw"></i>
                        <i class="fas fa-arrow-left float-left iconback" style="margin-top: 0.20vw" @click="backToHome()"></i>
                    </div>
                    <div class="col-md-12 mt-4">
                        <div v-if="ads.length > 0">
                            <div class="card" v-for="ad in filteredList6" :key="ad.id">
                                <div v-if="ad.status == 'Offen' || (ad.editor == myname && ad.status != 'Freigegeben')">
                                    <div class="card-header">
                                        <h3 class="card-title">Anzeige von {{ad.name}}</h3>
                                        <div class="card-tools">
                                            <i class="fa-solid fa-user-vneck" style="margin-top: 0.20vw"></i>
                                        </div>
                                    </div>
                                    <div class="card-body" v-if="ad.editor == myname">
                                        <textarea class="form-control" style="border-radius: 1vw" v-model="ad.text" maxlength="128" rows="3" v-on:keyup.enter="createAd()" placeholder="Anzeigentext ..."></textarea>
                                        <div class="mt-2">
                                            Telefonnummer: {{ad.number}}
                                        </div>
                                    </div>
                                    <div class="card-body" v-else>
                                        {{ad.text}}
                                        <div class="mt-2">
                                            Telefonnummer: {{ad.number}}
                                        </div>
                                    </div>
                                    <div v-if="ad.status == 'Offen'" class="card-footer">
                                        <div style="display: flex; justify-content: center; align-items: center;">
                                            <i class="fa-solid fa-hand float-left iconback mr-3" style="margin-top: 0.20vw;" @click="claimAd(ad.id)"></i>
                                            <i class="fa-solid fa-phone float-left iconback" style="margin-top: 0.20vw;" @click="setCall(ad.id)"></i>
                                        </div>
                                    </div>
                                    <div v-if="ad.status == 'Bearbeitung'" class="card-footer" style="border: solid; border-color: red; border-width: thin;">
                                        <div style="display: flex; justify-content: center; align-items: center;">
                                            <i class="fa-solid fa-check float-left iconback mr-3" style="margin-top: 0.20vw;" @click="acceptAd(ad.text, ad.id)"></i>
                                            <i class="fa-solid fa-trash float-left iconback mr-3" style="margin-top: 0.20vw;" @click="deleteAd(ad.id)"></i>
                                            <i class="fa-solid fa-phone float-left iconback" style="margin-top: 0.20vw;" @click="setCall(ad.number)"></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div v-else>
                            <div class="text-center" style="display: flex; justify-content: center; align-items: center;font-family: 'Exo', sans-serif;margin-top:0.1vw">
                                Keine Anzeigenanfragen vorhanden!
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="handybox3" v-if="setting == 29" style="overflow: auto;scrollbar-width: none;height:25.5vw;">
                <div class="content" style="margin-top: 0.15vw">
                    <div class="card-header text-center" style="font-family: 'Exo', sans-serif;background-color:#485159;margin-left:0.14vw;border-color:#3F6791">
                        Neue Anzeige
                        <i class="fa-brands fa-creative-commons-nd float-right iconback" @click="setting = 28" style="margin-top: 0.20vw"></i>
                        <i class="fa-solid fa-circle-plus float-right iconback mr-1" @click="setting = 29" style="margin-top: 0.20vw"></i>
                        <i v-if="islifeinvader > -1" class="fa-solid fa-lock float-right iconback mr-1" @click="setting = 30" style="margin-top: 0.20vw"></i>
                        <i class="fas fa-arrow-left float-left iconback" style="margin-top: 0.20vw" @click="backToHome()"></i>
                    </div>
                    <div class="col-md-12">
                        <div v-if="handyinfo" class="animate__animated animate__fadeIn alert alert-primary mr-2 text-center" role="alert" style="border-radius: 2vw; margin-top:1.2vw">
                            {{handyinfo}}
                        </div>
                        <div style="display: flex; justify-content: center; align-items: center;">
                            <img class="weathericon" style="width:3vw;margin-top:2.25vw" src="../assets/images/smartphone/Lifeinvader.png" />
                        </div>
                        <div style="display: flex; justify-content: center; align-items: center;margin-top: 1vw">
                            <strong>Verfügbare Mitarbeiter: {{lifeinvaders}}</strong>
                        </div>
                        <div style="display: flex; justify-content: center; align-items: center;margin-top: 1vw">
                            <strong>Preis pro Anzeige: {{lifeinvaderprice}}$</strong>
                        </div>
                        <div v-if="lifeinvaders > 0">
                            <div v-if="found1 == false">
                                <div style="display: flex; justify-content: center; align-items: center;margin-top: 1vw">
                                    <div class="input-group mb-3" style="margin-top: 0.5vw">
                                        <textarea class="form-control" style="border-radius: 1vw" v-model="contactname" maxlength="128" rows="4" v-on:keyup.enter="createAd()" placeholder="Anzeigentext ..."></textarea>
                                    </div>
                                </div>
                                <div style="display: flex; justify-content: center; align-items: center;margin-top: 0.5vw">
                                    <button @click="createAd()" type="button" class="btn btn-primary mr-3 ml-3 mb-2 mt-1" style="border-radius: 1vw">Anzeige erstellen</button>
                                </div>
                            </div>
                        </div>
                        <div v-else>
                            <div class="card mt-4 text-center">
                                <div style="display: flex; justify-content: center; align-items: center;font-family: 'Exo', sans-serif;margin-top:0.1vw">
                                    Zurzeit sind keine Lifeinvader Mitarbeiter im Dienst!
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="handybox3" v-if="setting == 23" style="overflow: auto;scrollbar-width: none;height:25.5vw;">
                <div class="content" style="margin-top: 0.15vw">
                    <div class="card-header text-center" style="font-family: 'Exo', sans-serif;background-color:#485159;margin-left:0.14vw;border-color:#3F6791">
                        Eingetragene Services
                        <i class="fas fa-arrow-left float-left iconback" style="margin-top: 0.20vw" @click="backToHome()"></i>
                        <div class="input-group mb-3" style="margin-top: 0.7vw">
                            <div class="input-group-prepend">
                                <span class="input-group-text" style="border-radius: 1vw"><i class="fas fa-search"></i></span>
                            </div>
                            <input type="text" v-model="searchelement" v-on:input="searchelement = $event.target.value" maxlength="18" class="form-control" placeholder="Suche..." style="border-radius: 1vw;margin-left: 0.45vw">
                        </div>
                    </div>
                    <div class="col-md-12 mt-4">
                        <div v-if="services.length > 0">
                            <div class="card" v-for="service in filteredList3" :key="service.id">
                                <div class="card-header">
                                    <h3 class="card-title">{{service.name}}</h3>
                                    <div class="card-tools">
                                        <i class="fa-solid fa-user-vneck" style="margin-top: 0.20vw"></i>
                                    </div>
                                </div>
                                <div class="card-body">
                                    {{service.text}}
                                    <div class="mt-2" v-if="service.number">
                                        Telefonnummer: {{service.number}}
                                    </div>
                                </div>
                                <div class="card-footer" v-if="service.number">
                                    <div style="display: flex; justify-content: center; align-items: center;">
                                        <i class="fa-solid fa-phone float-left iconback" style="margin-top: 0.20vw;" @click="setCall(service.number)"></i>
                                    </div>
                                </div>
                                <div class="card-footer" v-else>
                                    <div style="display: flex; justify-content: center; align-items: center;">
                                        Kein Mitarbeiter im Dienst!
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card" v-if="services.length <= 0">
                            <div class="text-center" style="display: flex; justify-content: center; align-items: center;font-family: 'Exo', sans-serif;margin-top:0.1vw">
                                Es wurden noch keine Services eingetragen!
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="handybox3" v-if="setting == 12" style="overflow: auto;scrollbar-width: none;height:25.5vw;">
                <div class="content" style="margin-top: 0.15vw">
                    <div class="card-header text-center" style="font-family: 'Exo', sans-serif;background-color:#485159;margin-left:0.14vw;border-color:#3F6791">
                        Telefon
                        <i class="fas fa-arrow-left float-left iconback" style="margin-top: 0.2vw" @click="backToHome()"></i>
                        <i class="fas fa-user float-right iconback" style="margin-top: 0.2vw" @click="loadCallLogs()"></i>
                        <i class="fas fa-phone float-right iconback" style="margin-right: 0.5vw;margin-top: 0.2vw" @click="setting = 11"></i>
                    </div>
                    <div class="col-md-12">
                        <div v-if="calllogs.length <= 0" style="margin-top: 1vw">
                            <div style="display: flex; justify-content: center; align-items: center;font-family: 'Exo', sans-serif">
                                Keine Anrufe vorhanden!
                            </div>
                        </div>
                        <div style="margin-top: 0.7vw" v-for="call in calllogs" :key="call.id">
                            <div>
                                <i class="fas fa-phone float-left bordericongreen" v-if="call.tonumber == smartphone.phonenumber" style="margin-left: 0.2vw;margin-top: 0.63vw:color:green"></i>
                                <i class="fas fa-phone float-left bordericonred" v-else style="margin-left: 0.2vw;margin-top: 0.63vw:color:red"></i>
                            </div>
                            <div style="margin-top: 0.5vw">
                                <span v-if="call.tonumber == smartphone.phonenumber" style="margin-top: 1.1vw;margin-left: 1vw">{{getPhoneContact(call.fromnumber)}}</span>
                                <span v-else style="margin-top: 1.1vw;margin-left: 1vw">{{getPhoneContact(call.tonumber)}}</span>
                                <i v-if="call.tonumber == smartphone.phonenumber" class="hovericon fas fa-phone float-right" @click="setCall(call.fromnumber)"></i>
                                <i v-else class="hovericon fas fa-phone float-right" @click="setCall(call.tonumber)"></i>
                                <div>
                                    <span class="text-muted" style="margin-left: 1.1vw; color:white">{{timeConverter(call.timestamp)}}</span>
                                </div>
                            </div>
                            <hr />
                        </div>
                    </div>
                </div>
            </div>
            <div class="handybox3" v-if="setting == 13" style="overflow: auto;scrollbar-width: none;height:25.5vw;">
                <div class="content" style="margin-top: 0.15vw">
                    <div class="card-header text-center" style="font-family: 'Exo', sans-serif;background-color:#485159;margin-left:0.14vw;border-color:#3F6791">
                        Onlinebanking
                        <i class="fas fa-arrow-left float-left iconback" style="margin-top: 0.2vw" @click="backToHome()"></i>
                    </div>
                    <div class="col-md-12">
                        <div style="margin-top: 0.7vw">
                            <div style="display: flex; justify-content: center; align-items: center;">
                                <label v-if="bankaccounts.length > 0">Kontoauswahl:</label>
                                <label v-if="bankaccounts.length <= 0">Keine Konten vorhanden!</label>
                            </div>
                            <div v-for="bank in bankaccounts" :key="bank.id">
                                <button v-if="bank.banknumber == defaultbank" type="submit" class="btn btn-block btn-primary" style="margin-top:0.5vw;border-radius: 1vw" id="banknumber" name="banknumber" @click="selectBank(bank)">{{bank.banknumber}}
                                    -
                                    {{bank.bankvalue}}$ <span class="badge badge-primary" style="border-radius: 1vw">S</span></button>
                                <button v-else type="submit" class="btn btn-block btn-secondary" id="banknumber" name="banknumber" style="margin-top:0.5vw;border-radius: 1vw" @click="selectBank(bank)">{{bank.banknumber}}
                                    -
                                    {{bank.bankvalue}}$</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="handybox3" v-if="setting == 15" style="overflow: auto;scrollbar-width: none;height:25.5vw;">
                <div class="content" style="margin-top: 0.15vw">
                    <div class="card-header text-center" style="font-family: 'Exo', sans-serif;background-color:#485159;margin-left:0.14vw;border-color:#3F6791">
                        Kontoaktionen <span class="badge badge-info right" style="border-radius: 1vw">{{selectedbank.banknumber}}
                            -
                            {{selectedbank.bankvalue}}$</span>
                        <i class="fas fa-arrow-left float-left iconback" style="margin-top: 0.2vw" @click="showBanking()"></i>
                    </div>
                    <div class="col-md-12">
                        <div style="display: flex; justify-content: center; align-items: center;">
                            <button @click="setting = 14" type="button" style="border-radius: 1vw;margin-left: 0.15vw;margin-top: 1.1vw" :class="[(setting == 14) ? 'btn btn-primary btn-sm':'btn btn-secondary btn-sm']"><i class="fas fa-money float-left iconback"></i></button>
                            <button @click="showBanksettings()" type="button" class="btn btn-primary" style="border-radius: 1vw;margin-left: 0.15vw;margin-top: 1.1vw" :class="[(setting == 16) ? 'btn btn-primary btn-sm':'btn btn-secondary btn-sm']"><i class="fas fa-list-ol float-left iconback"></i></button>
                            <button @click="showBankfiles()" type="button" class="btn btn-primary" style="border-radius: 1vw;margin-left: 0.15vw;margin-top: 1.1vw" :class="[(setting == 15) ? 'btn btn-primary btn-sm':'btn btn-secondary btn-sm']"><i class="fas fa-list float-left iconback"></i></button>
                        </div>
                        <hr />
                        <label>Bankaktionen:</label>
                        <div v-for="(banksetting, index) in banksettings" :key="index">
                            {{banksetting.setting}}
                            <span class="float-right" style="color:white">{{banksetting.name}}</span>
                            <span class="float-left text-muted">{{timeConverter(banksetting.timestamp)}}</span>
                            <span class="float-right text-muted"><span class="badge badge-success" style="border-radius: 0.9vw">{{banksetting.value}}$</span> </span>
                            <hr style="margin-top: 2vw" />
                        </div>
                        <div v-if="!banksettings || banksettings.length <= 0">
                            <span>Kein Bankaktionen vorhanden!</span>
                        </div>
                    </div>
                </div>
            </div>
            <div class="handybox3" v-if="setting == 16" style="overflow: auto;scrollbar-width: none;height:25.5vw;">
                <div class="content" style="margin-top: 0.15vw">
                    <div class="card-header text-center" style="font-family: 'Exo', sans-serif;background-color:#485159;margin-left:0.14vw;border-color:#3F6791">
                        Kontoauszug <span class="badge badge-info right" style="border-radius: 1vw">{{selectedbank.banknumber}} -
                            {{selectedbank.bankvalue}}$</span>
                        <i class="fas fa-arrow-left float-left iconback" style="margin-top: 0.2vw" @click="showBanking()"></i>
                    </div>
                    <div class="col-md-12">
                        <div style="display: flex; justify-content: center; align-items: center;">
                            <button @click="setting = 14" type="button" style="border-radius: 1vw;margin-left: 0.15vw;margin-top: 1.1vw" :class="[(setting == 14) ? 'btn btn-primary btn-sm':'btn btn-secondary btn-sm']"><i class="fas fa-money float-left iconback"></i></button>
                            <button @click="showBanksettings()" type="button" class="btn btn-primary" style="border-radius: 1vw;margin-left: 0.15vw;margin-top: 1.1vw" :class="[(setting == 16) ? 'btn btn-primary btn-sm':'btn btn-secondary btn-sm']"><i class="fas fa-list-ol float-left iconback"></i></button>
                            <button @click="showBankfiles()" type="button" class="btn btn-primary" style="border-radius: 1vw;margin-left: 0.15vw;margin-top: 1.1vw" :class="[(setting == 15) ? 'btn btn-primary btn-sm':'btn btn-secondary btn-sm']"><i class="fas fa-list float-left iconback"></i></button>
                        </div>
                        <hr />
                        <label>Kontoauszug:</label>
                        <div v-for="(bankfile, index) in bankfiles" :key="index">
                            <span class="text-muted">{{bankfile.banktext}}</span>
                            <br />
                            {{bankfile.bankfrom}}
                            <span class="float-right" style="color:white">{{bankfile.bankto}}</span>
                            <span class="float-left text-muted">{{timeConverter(bankfile.banktime)}}</span>
                            <span class="float-right text-muted"><span v-if="bankfile.bankto == selectedbank.banknumber" class="badge badge-success" style="border-radius: 0.9vw">{{bankfile.bankvalue}}$</span><span v-else class="badge badge-danger" style="border-radius: 0.9vw">{{bankfile.bankvalue}}$</span></span>
                            <hr style="margin-top: 2vw" />
                        </div>
                        <div v-if="!bankfiles || bankfiles.length <= 0">
                            <span>Keine Kontoauszüge vorhanden!</span>
                        </div>
                    </div>
                </div>
            </div>
            <div class="handybox3" v-if="setting == 17" style="overflow: auto;scrollbar-width: none;height:25.5vw;">
                <div class="content" style="margin-top: 0.15vw">
                    <div class="card-header text-center" style="font-family: 'Exo', sans-serif;background-color:#485159;margin-left:0.14vw;border-color:#3F6791">
                        Taschenrechner
                        <i class="fas fa-arrow-left float-left iconback" style="margin-top: 0.2vw" @click="backToHome()"></i>
                        <i class="fa-solid fa-delete-left float-right iconback" style="margin-top: 0.20vw" @click="addSign2('C')"></i>
                    </div>
                    <div class="col-md-12">
                        <div class="row" style="margin-left: 0.3vw">
                            <input type="text" v-model="calculator" maxlength="20" class="form-control text-center" style="border-radius: 1vw;margin-top:1.5vw;margin-right: 1vw;margin-left:1vw;font-family: 'Exo', sans-serif;">
                        </div>
                        <hr style="margin-top: 2vw" />
                        <div class="col-md-12">
                            <div class="row">
                                <div class="ordercalculator">
                                    <button @click="addSign2(1)" type="button" class="btn btn-primary calculatoritem">1</button>
                                    <button @click="addSign2(2)" type="button" class="btn btn-primary calculatoritem">2</button>
                                    <button @click="addSign2(3)" type="button" class="btn btn-primary calculatoritem">3</button>
                                    <button @click="addSign2('/')" type="button" class="btn btn-primary calculatoritem">/</button>
                                    <button @click="addSign2(4)" type="button" class="btn btn-primary calculatoritem">4</button>
                                    <button @click="addSign2(5)" type="button" class="btn btn-primary calculatoritem">5</button>
                                    <button @click="addSign2(6)" type="button" class="btn btn-primary calculatoritem">6</button>
                                    <button @click="addSign2('-')" type="button" class="btn btn-primary calculatoritem">-</button>
                                    <button @click="addSign2(7)" type="button" class="btn btn-primary calculatoritem">7</button>
                                    <button @click="addSign2(8)" type="button" class="btn btn-primary calculatoritem">8</button>
                                    <button @click="addSign2(9)" type="button" class="btn btn-primary calculatoritem">9</button>
                                    <button @click="addSign2('+')" type="button" class="btn btn-primary calculatoritem">+</button>
                                    <button @click="addSign2('.')" type="button" class="btn btn-primary calculatoritem">.</button>
                                    <button @click="addSign2(0)" type="button" class="btn btn-primary calculatoritem">0</button>
                                    <button @click="addSign2('=')" type="button" class="btn btn-primary calculatoritem">=</button>
                                    <button @click="addSign2('*')" type="button" class="btn btn-primary calculatoritem">*</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="handybox3" v-if="setting == 14" style="overflow: auto;scrollbar-width: none;height:25.5vw;">
                <div class="content" style="margin-top: 0.15vw">
                    <div class="card-header text-center" style="font-family: 'Exo', sans-serif;background-color:#485159;margin-left:0.14vw;border-color:#3F6791">
                        Überweisung <span class="badge badge-info right" style="border-radius: 1vw">{{selectedbank.banknumber}} -
                            {{selectedbank.bankvalue}}$</span>
                        <i class="fas fa-arrow-left float-left iconback" style="margin-top: 0.2vw" @click="showBanking()"></i>
                    </div>
                    <div class="col-md-12">
                        <div style="display: flex; justify-content: center; align-items: center;">
                            <button @click="setting = 14" type="button" style="border-radius: 1vw;margin-left: 0.15vw;margin-top: 1.1vw" :class="[(setting == 14) ? 'btn btn-primary btn-sm':'btn btn-secondary btn-sm']"><i class="fas fa-money float-left iconback"></i></button>
                            <button @click="showBanksettings()" type="button" class="btn btn-primary" style="border-radius: 1vw;margin-left: 0.15vw;margin-top: 1.1vw" :class="[(setting == 16) ? 'btn btn-primary btn-sm':'btn btn-secondary btn-sm']"><i class="fas fa-list-ol float-left iconback"></i></button>
                            <button @click="showBankfiles()" type="button" class="btn btn-primary" style="border-radius: 1vw;margin-left: 0.15vw;margin-top: 1.1vw" :class="[(setting == 15) ? 'btn btn-primary btn-sm':'btn btn-secondary btn-sm']"><i class="fas fa-list float-left iconback"></i></button>
                        </div>
                        <hr />
                        <div class="form-group" style="margin-top: 0.3vw" v-if="!handyinfo">
                            <label>Empfängerkonto</label>
                            <input type="text" class="form-control" name="empfänger" id="empfänger" value="SA3701-" placeholder="Kontonummer" maxlength="20" autocomplete="off" style="border-radius: 1vw" v-model="banknumber">
                        </div>
                        <div class="form-group" v-if="!handyinfo">
                            <label>Betrag in $</label>
                            <input type="text" class="form-control" name="betrag" placeholder="500" maxlength="10" autocomplete="off" style="border-radius: 1vw" v-model="bankvalue">
                        </div>
                        <div class="form-group" v-if="!handyinfo">
                            <label>Verwendungszweck</label>
                            <input type="text" autocomplete="off" class="form-control" name="verwendungszweck" id="verwendungszweck" placeholder="Hausverkauf" maxlength="64" style="border-radius: 1vw" v-model="banktext">
                        </div>
                        <div class="form-group" v-if="!handyinfo">
                            <label>Bankpin</label>
                            <input type="password" autocomplete="off" class="form-control" name="bankpin" id="bankpin" placeholder="Pin" maxlength="4" style="border-radius: 1vw" v-model="bankpin">
                        </div>
                        <div class="form-group" v-if="!handyinfo">
                            <label>Dauerauftrag? (Für einen Dauerauftrag, die Anzahl der
                                Tage in das Feld schreiben!)</label>
                            <input type="text" class="form-control" name="tage" id="tage" placeholder="0" maxlength="3" autocomplete="off" style="border-radius: 1vw" v-model="banksto">
                        </div>
                        <div v-if="handyinfo" class="animate__animated animate__fadeIn alert alert-dark mr-4 ml-4 text-center" role="alert" style="border-radius: 2vw; margin-top: 2.2vw;margin-top:1.2vw">
                            {{handyinfo}}
                        </div>
                        <button v-if="!handyinfo" type="submit" class="btn btn-block btn-primary btn-lg" style="border-radius: 1vw" @click="startTransfer()">Überweisung
                            tätigen</button>
                    </div>
                </div>
            </div>
            <div class="handybox3" v-if="setting == 4" style="overflow: auto;scrollbar-width: none;height:25.5vw;">
                <div class="content" style="margin-top: 0.15vw">
                    <div class="card-header text-center" style="font-family: 'Exo', sans-serif;background-color:#485159;margin-left:0.14vw;border-color:#3F6791">
                        Kontakte hinzufügen
                        <i class="fas fa-arrow-left float-left iconback" style="margin-top: 0.2vw" @click="showContacts()"></i>
                    </div>
                    <div class="col-md-12">
                        <div v-if="!handyinfo" class="alert alert-primary mr-2 text-center" role="alert" style="border-radius: 2vw; margin-top:0.8vw; margin-left: 0.5vw">
                            Du hast Speicherplatz für insg. 50 Kontakte!
                        </div>
                        <div v-if="handyinfo" class="alert alert-primary mr-2 text-center" role="alert" style="border-radius: 2vw; margin-top:0.8vw; margin-left: 0.5vw">
                            {{handyinfo}}
                        </div>
                        <div class="input-group mb-3" style="margin-top: 1.7vw">
                            <div class="input-group-prepend">
                                <span class="input-group-text" style="border-radius: 1vw"><i class="fas fa-user"></i></span>
                            </div>
                            <input type="text" v-model="contactname" maxlength="18" class="form-control" placeholder="Kontaktname" style="border-radius: 1vw;margin-left: 0.45vw" v-on:keyup.enter="createContact()">
                        </div>
                        <div class="input-group mb-3" style="margin-top: 0.7vw">
                            <div class="input-group-prepend">
                                <span class="input-group-text" style="border-radius: 1vw"><i class="fas fa-phone-volume"></i></span>
                            </div>
                            <input type="text" v-model="contactnumber" maxlength="18" class="form-control" placeholder="Nummer" style="border-radius: 1vw;margin-left: 0.45vw" v-on:keyup.enter="createContact()">
                        </div>
                    </div>
                    <div style="display: flex; justify-content: center; align-items: center;margin-top:2vw">
                        <button @click="createContact()" type="button" class="btn btn-primary mr-3 ml-3 mb-2 mt-1" style="border-radius: 1vw">Kontakt anlegen</button>
                    </div>
                </div>
            </div>
            <div class="handybox3" v-if="setting == 5" style="overflow: hidden;scrollbar-width: none;height:26.2vw">
                <div class="content" style="margin-top: 0.15vw">
                    <div class="card-header text-center" style="font-family: 'Exo', sans-serif;background-color:#485159;margin-left:0.14vw;border-color:#3F6791">
                        nChat
                        <i class="fas fa-arrow-left float-left iconback" style="margin-top: 0.2vw" @click="showChatContacts()"></i>
                        <i class="fas fa-location-dot float-right iconback" style="margin-top: 0.2vw" @click="sendGPS()"></i>
                    </div>
                    <div class="direct-chat direct-chat-primary ml-2 mr-2">
                        <div class="card-body">
                            <div class="direct-chat-messages" style="height:15vw; overflow-y: scroll;z-index:inherit" id="message-box">
                                <div v-for="msg in tempArray" :key="msg.timestamp">
                                    <div class="direct-chat-msg" v-if="msg.frommessage != smartphone.phonenumber">
                                        <div class="direct-chat-infos clearfix">
                                            <span class="direct-chat-name float-left" style="color:white">{{getPhoneContact(msg.frommessage)}}</span>
                                            <span class="direct-chat-timestamp float-right">{{timeConverter(msg.timestamp)}}</span>
                                        </div>
                                        <div class="direct-chat-text" v-if="msg.text.includes('Standort:')">
                                            {{msg.text}}
                                            <i class="fas fa-location-dot float-right iconback2" style="margin-top: 0.2vw" @click="getGPS(msg.text)"></i>
                                        </div>
                                        <div class="direct-chat-text" v-else>
                                            {{msg.text}}
                                        </div>
                                    </div>
                                    <div class="direct-chat-msg right" v-if="msg.frommessage == smartphone.phonenumber">
                                        <div class="direct-chat-infos clearfix">
                                            <span class="direct-chat-name float-right" style="color:white">{{getPhoneContact(msg.frommessage)}}</span>
                                            <span class="direct-chat-timestamp float-left">{{timeConverter(msg.timestamp)}}</span>
                                        </div>
                                        <div class="direct-chat-text" v-if="msg.text.includes('Standort:')">
                                            {{msg.text}}
                                            <i class="fas fa-location-dot float-right iconback2" style="margin-top: 0.2vw" @click="getGPS(msg.text)"></i>
                                        </div>
                                        <div class="direct-chat-text" v-else>
                                            {{msg.text}}
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div style="display: flex; justify-content: center; align-items: center;">
                            <div style="bottom: 0;position: absolute;margin-bottom:0.5vw">
                                <div class="input-group">
                                    <div style="display: flex; justify-content: center; align-items: center;">
                                        <input type="text" v-model="newmessage" name="message" placeholder="Nachricht" class="form-control" style="border-radius: 1vw;width: 100%" maxlength="64" v-on:keyup.enter="sendMessage()">
                                    </div>
                                </div>
                                <div style="display: flex; justify-content: center; align-items: center;">
                                    <div style="display: flex; justify-content: center; align-items: center;">
                                        <button @click="sendMessage()" type="button" class="btn btn-primary" style="border-radius: 1vw; margin-top: 0.5vw">Senden</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="handybox3" v-if="setting == 6" style="overflow: auto;scrollbar-width: none;height:25.5vw;">
                <div class="content" style="margin-top: 0.15vw">
                    <div class="card-header text-center" style="font-family: 'Exo', sans-serif;background-color:#485159;margin-left:0.14vw;border-color:#3F6791">
                        Offene Chats
                        <i class="fas fa-arrow-left float-left iconback" style="margin-top: 0.2vw" @click="backToHome()"></i>
                        <i class="fas fa-comment-dots float-right hovericon" @click="showContacts()"></i>
                        <div class="input-group mb-3" style="margin-top: 0.7vw">
                            <div class="input-group-prepend">
                                <span class="input-group-text" style="border-radius: 1vw"><i class="fas fa-search"></i></span>
                            </div>
                            <input type="text" v-model="searchelement" v-on:input="searchelement = $event.target.value" maxlength="18" class="form-control" placeholder="Suche..." style="border-radius: 1vw;margin-left: 0.45vw">
                        </div>
                    </div>
                    <div class="col-md-12" style="margin-top: 0.70vh;">
                        <ul class="list-group" style="margin-top: 0.5vw;">
                            <li v-for="(contact, index) in filteredList2" :key="contact.id">
                                <div v-if="index == 0" style="margin-top: 0.3vw">
                                    <i class="fas fa-comments float-left bordericon" style="margin-left: 0.2vw;margin-top: 0.55vw"></i>
                                    <span style="margin-left: 0.8vw">{{contact.name}}</span>
                                    <span style="margin-left: 0.8vw; color:white">{{getPhoneContact(contact)}}</span>
                                    <i class="hovericon fas fa-comments float-right" @click="loadChats(contact)"></i>
                                    <div>
                                        <span class="text-muted" style="margin-left: 1.55vw; color:white">{{getLastMessage(contact)}}</span>
                                    </div>
                                </div>
                                <div v-else>
                                    <i class="fas fa-comments float-left bordericon" style="margin-left: 0.2vw;margin-top: 0.55vw"></i>
                                    <span style="margin-left: 0.8vw">{{contact.name}}</span>
                                    <span style="margin-left: 0.8vw; color:white">{{getPhoneContact(contact)}}</span>
                                    <i class="hovericon fas fa-comments float-right" @click="loadChats(contact)"></i>
                                    <div>
                                        <span class="text-muted" style="margin-left: 1.55vw; color:white">{{getLastMessage(contact)}}</span>
                                    </div>
                                </div>
                                <hr />
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
            <div class="handybox3" v-if="setting == 9" style="overflow: auto;scrollbar-width: none;height:25.5vw;">
                <div class="content" style="margin-top: 0.15vw">
                    <div class="card-header text-center" style="font-family: 'Exo', sans-serif;background-color:#485159;margin-left:0.14vw;border-color:#3F6791">
                        Wettervorraussage
                        <i class="fas fa-arrow-left float-left iconback" style="margin-top: 0.2vw" @click="backToHome()"></i>
                    </div>
                    <div class="col-md-12" style="margin-top: 0.70vh;">
                        <div style="display: flex; justify-content: center; align-items: center; margin-top:-1.2vw">
                            <button @click="setting = 7" type="button" style="border-radius: 1vw;margin-left: 0.15vw;margin-top: 1.1vw" :class="[(setting == 7) ? 'btn btn-primary btn-sm':'btn btn-secondary btn-sm']">Heute</button>
                            <button @click="setting = 8" type="button" class="btn btn-primary" style="border-radius: 1vw;margin-left: 0.15vw;margin-top: 1.1vw" :class="[(setting == 8) ? 'btn btn-primary btn-sm':'btn btn-secondary btn-sm']">Morgen</button>
                            <button @click="setting = 9" type="button" class="btn btn-primary" style="border-radius: 1vw;margin-left: 0.15vw;margin-top: 1.1vw" :class="[(setting == 9) ? 'btn btn-primary btn-sm':'btn btn-secondary btn-sm']">Übermorgen</button>
                        </div>
                        <div style="display: flex; justify-content: center; align-items: center;">
                            <img class="weathericon" style="width:6vw;margin-top:0.5vw" :src="getWeatherUrl(weather.daily[1].weather[0].icon)">
                        </div>
                        <div style="display: flex; justify-content: center; align-items: center; margin-top:-1.2vw">
                            <span style="font-size: 0.85vw">{{getWeatherString(weather.daily[1].weather[0].description)}}</span>
                        </div>
                        <div style="display: flex; justify-content: center; align-items: center;; margin-top: 1.5vw">
                            <span style="font-size: 1.1vw">Temperatur: {{parseInt(weather.daily[1].temp.day)-273}} °C</span>
                        </div>
                        <div style="display: flex; justify-content: center; align-items: center;">
                            <span style="font-size: 1.1vw">Gefühlt wie: {{parseInt(weather.daily[1].feels_like.day)-273}}
                                °C</span>
                        </div>
                        <div style="display: flex; justify-content: center; align-items: center;">
                            <span style="font-size: 1.1vw">Luftfeuchtigkeit: {{parseInt(weather.daily[1].humidity)}}%</span>
                        </div>
                        <div style="display: flex; justify-content: center; align-items: center;margin-top:2.5vw">
                            <span style="font-size: 0.6vw">Alle Angaben ohne Gewähr!</span>
                        </div>
                    </div>
                </div>
            </div>
            <div class="handybox3" v-if="setting == 8" style="overflow: auto;scrollbar-width: none;height:25.5vw;">
                <div class="content" style="margin-top: 0.15vw">
                    <div class="card-header text-center" style="font-family: 'Exo', sans-serif;background-color:#485159;margin-left:0.14vw;border-color:#3F6791">
                        Wettervorraussage
                        <i class="fas fa-arrow-left float-left iconback" style="margin-top: 0.2vw" @click="backToHome()"></i>
                    </div>
                    <div class="col-md-12" style="margin-top: 0.70vh;">
                        <div style="display: flex; justify-content: center; align-items: center; margin-top:-1.2vw">
                            <button @click="setting = 7" type="button" style="border-radius: 1vw;margin-left: 0.15vw;margin-top: 1.1vw" :class="[(setting == 7) ? 'btn btn-primary btn-sm':'btn btn-secondary btn-sm']">Heute</button>
                            <button @click="setting = 8" type="button" class="btn btn-primary" style="border-radius: 1vw;margin-left: 0.15vw;margin-top: 1.1vw" :class="[(setting == 8) ? 'btn btn-primary btn-sm':'btn btn-secondary btn-sm']">Morgen</button>
                            <button @click="setting = 9" type="button" class="btn btn-primary" style="border-radius: 1vw;margin-left: 0.15vw;margin-top: 1.1vw" :class="[(setting == 9) ? 'btn btn-primary btn-sm':'btn btn-secondary btn-sm']">Übermorgen</button>
                        </div>
                        <div style="display: flex; justify-content: center; align-items: center;">
                            <img class="weathericon" style="width:6vw;margin-top:0.5vw" :src="getWeatherUrl(weather.daily[0].weather[0].icon)">
                        </div>
                        <div style="display: flex; justify-content: center; align-items: center; margin-top:-1.2vw">
                            <span style="font-size: 0.85vw">{{getWeatherString(weather.daily[0].weather[0].description)}}</span>
                        </div>
                        <div style="display: flex; justify-content: center; align-items: center;; margin-top: 1.5vw">
                            <span style="font-size: 1.1vw">Temperatur: {{parseInt(weather.daily[0].temp.day)-273}} °C</span>
                        </div>
                        <div style="display: flex; justify-content: center; align-items: center;">
                            <span style="font-size: 1.1vw">Gefühlt wie: {{parseInt(weather.daily[0].feels_like.day)-273}}
                                °C</span>
                        </div>
                        <div style="display: flex; justify-content: center; align-items: center;">
                            <span style="font-size: 1.1vw">Luftfeuchtigkeit: {{parseInt(weather.daily[0].humidity)}}%</span>
                        </div>
                        <div style="display: flex; justify-content: center; align-items: center;margin-top:2.5vw">
                            <span style="font-size: 0.6vw">Alle Angaben ohne Gewähr!</span>
                        </div>
                    </div>
                </div>
            </div>
            <div class="handybox3" v-if="setting == 7" style="overflow: auto;scrollbar-width: none;height:25.5vw;">
                <div class="content" style="margin-top: 0.15vw">
                    <div class="card-header text-center" style="font-family: 'Exo', sans-serif;background-color:#485159;margin-left:0.14vw;border-color:#3F6791">
                        Wettervorraussage
                        <i class="fas fa-arrow-left float-left iconback" style="margin-top: 0.2vw" @click="backToHome()"></i>
                    </div>
                    <div class="col-md-12" style="margin-top: 0.70vh;">
                        <div style="display: flex; justify-content: center; align-items: center; margin-top:-1.2vw">
                            <button @click="setting = 7" type="button" style="border-radius: 1vw;margin-left: 0.15vw;margin-top: 1.1vw" :class="[(setting == 7) ? 'btn btn-primary btn-sm':'btn btn-secondary btn-sm']">Heute</button>
                            <button @click="setting = 8" type="button" class="btn btn-primary" style="border-radius: 1vw;margin-left: 0.15vw;margin-top: 1.1vw" :class="[(setting == 8) ? 'btn btn-primary btn-sm':'btn btn-secondary btn-sm']">Morgen</button>
                            <button @click="setting = 9" type="button" class="btn btn-primary" style="border-radius: 1vw;margin-left: 0.15vw;margin-top: 1.1vw" :class="[(setting == 9) ? 'btn btn-primary btn-sm':'btn btn-secondary btn-sm']">Übermorgen</button>
                        </div>
                        <div style="display: flex; justify-content: center; align-items: center;">
                            <img class="weathericon" style="width:6vw;margin-top:0.5vw" :src="getWeatherUrl(weather.current.weather[0].icon)">
                        </div>
                        <div style="display: flex; justify-content: center; align-items: center; margin-top:-1.2vw">
                            <span style="font-size: 0.85vw">{{getWeatherString(weather.current.weather[0].description)}}</span>
                        </div>
                        <div style="display: flex; justify-content: center; align-items: center;; margin-top: 1.5vw">
                            <span style="font-size: 1.1vw">Temperatur: {{parseInt(weather.current.temp)-273}} °C</span>
                        </div>
                        <div style="display: flex; justify-content: center; align-items: center;">
                            <span style="font-size: 1.1vw">Gefühlt wie: {{parseInt(weather.current.feels_like)-273}}
                                °C</span>
                        </div>
                        <div style="display: flex; justify-content: center; align-items: center;">
                            <span style="font-size: 1.1vw">Luftfeuchtigkeit: {{parseInt(weather.current.humidity)}}%</span>
                        </div>
                        <div style="display: flex; justify-content: center; align-items: center;margin-top:2.5vw">
                            <span style="font-size: 0.6vw">Alle Angaben ohne Gewähr!</span>
                        </div>
                    </div>
                </div>
            </div>
            <div class="handybox3" v-if="setting == 3" style="overflow: auto;scrollbar-width: none;height:25.5vw;">
                <div class="content" style="margin-top: 0.15vw">
                    <div class="card-header text-center" style="font-family: 'Exo', sans-serif;background-color:#485159;margin-left:0.14vw;border-color:#3F6791">
                        Kontakte
                        <i class="fas fa-arrow-left float-left iconback" style="margin-top: 0.2vw" @click="backToHome()"></i>
                        <i class="fas fa-user-plus float-right hovericon" @click="addContacts()"></i>
                        <div class="input-group" style="margin-top: 0.7vw">
                            <div class="input-group-prepend">
                                <span class="input-group-text" style="border-radius: 1vw"><i class="fas fa-search"></i></span>
                            </div>
                            <input type="text" v-model="searchelement" v-on:input="searchelement = $event.target.value" maxlength="18" class="form-control" placeholder="Suche..." style="border-radius: 1vw; margin-left: 0.45vw">
                        </div>
                    </div>
                    <div class="col-md-12">
                        <ul class="list-group" style="margin-top: 0.6vw;">
                            <li v-for="(contact, index) in filteredList" :key="contact.id">
                                <div style="margin-top: 0.3vw" v-if="index == 0">
                                    <i class="fas fa-user float-left bordericon" style="margin-left: 0.2vw;margin-top: -0.10vw"></i>
                                    <span style="margin-left: 0.8vw;padding-top: 0.8vw">{{contact.name}}</span>
                                    <i class="hovericon fas fa-phone-volume float-right" @click="setCall(contact.number)"></i>
                                    <i class="hovericon fas fa-comments float-right" @click="loadChats(contact.number)"></i>
                                    <i class="hovericon fas fa-user-times float-right" @click="deleteContact(contact.id)"></i>
                                </div>
                                <div v-else>
                                    <i class="fas fa-user float-left bordericon" style="margin-left: 0.2vw;margin-top: -0.10vw"></i>
                                    <span style="margin-left: 0.8vw;padding-top: 0.8vw">{{contact.name}}</span>
                                    <i class="hovericon fas fa-phone-volume float-right" @click="setCall(contact.number)"></i>
                                    <i class="hovericon fas fa-comments float-right" @click="loadChats(contact.number)"></i>
                                    <i class="hovericon fas fa-user-times float-right" @click="deleteContact(contact.id)"></i>
                                </div>
                                <hr />
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
            <div class="handybox4" v-if="setting == 0 && smartphone.phonenumber">
                <div class="handybox4icons">
                    <img class="appicon" src="../assets/images/smartphone/Phone.png" @click="showPhone()">
                    <img class="appicon" src="../assets/images/smartphone/Kontakte.png" @click="showContacts()">
                    <img class="appicon" src="../assets/images/smartphone/Chat.png" @click="showChatContacts()">
                    <img class="appicon" src="../assets/images/smartphone/Settings.png" @click="showSettings()">
                </div>
            </div>
        </div>
    </div>
    <div class="centering4">
        <img class="cutimage" src="../assets/images/smartphone/Smartphonemookup.png">
    </div>
    <div class="centering5" v-if="((setting != -2 && setting != -3) || setting == 18)">
        <img class="cutimagebackground" :src="getImgUrl(smartphone.wallpaper)">
    </div>
    <div class="centering5" v-else>
        <img class="cutimagebackground" src="../assets/images/smartphone/Blackground.png">
    </div>
    <div class="centering5 cutimagebackground" style="background-color: #3f474e" v-if="setting > 0">
    </div>
</div>
</template>

<script>
var timeoutcheck = null;

export default {
    name: 'Smartphone',
    data: function () {
        return {
            showsmartphoneset: false,
            setting: -2, //-2
            lastsetting: 0,
            premium: 0,
            faction: 0,
            myname: 'test',
            clicked: (Date.now() / 1000),
            clicked2: (Date.now() / 1000),
            clicked3: (Date.now() / 1000),
            handyinfo: '',
            searchelement: '',
            contactname: '',
            charname: '',
            contactnumber: '',
            playerStatus: 'Idle',
            found1: false,
            lifeinvaders: 0,
            lifeinvaderprice: 1500,
            taxidrivers: 0,
            prepaid: 55,
            istaxi: -1,
            islifeinvader: -1,
            ivText: 'Privatrechnung',
            invoices: [],
            services: [],
            ads: [],
            taxijobs: [],
            weather: [],
            smartphone: [],
            tempArray: [],
            messages: [],
            contacts: [],
            calllogs: [],
            banksettings: [],
            bankfiles: [],
            messageContacts: [],
            bankaccounts: [],
            defaultbank: '',
            banknumber: '',
            bankvalue: '',
            banktext: '',
            bankpin: '',
            banksto: '0',
            inCall: '',
            emergency: 0,
            hidden: 0,
            calltime: 0,
            calculator: '',
            getcallnumber: '',
            getcallnumber2: '',
            calltimer: null,
            selectedbank: '',
            selectedNumber: '',
            selectedNumber2: '',
            newmessage: '',
            oldmessage: '',
            time: '',
            date: '',
            lastused: (Date.now() / 1000),
            lastcheck: (Date.now() / 1000),
            lastcheck2: (Date.now() / 1000),
            lastcheck3: (Date.now() / 1000),
            lastcheck4: (Date.now() / 1000),
            lastcheck5: (Date.now() / 1000),
            lastcheck6: (Date.now() / 1000),
            lastcheck7: (Date.now() / 1000),
            lastcheck8: (Date.now() / 1000),
            lastcheck9: (Date.now() / 1000),
            lastcheck10: (Date.now() / 1000),
            lastcheck11: (Date.now() / 1000),
            lastcheck12: (Date.now() / 1000),
            lastcheck13: (Date.now() / 1000),
            save: 0,
            newmessages: 0,
            playringtone: 0,
            callnumber: '',
            lastnumber: '',
            phonecount: 0,
            capacity: 48,
            muted: false,
        }
    },
    computed: {
        filteredList() {
            if (this.contacts) {
                return this.contacts.filter(contacts => {
                    return contacts.name.toLowerCase().includes(this.searchelement.toLowerCase())
                })
            }
            return '';
        },
        filteredList2() {
            return this.messageContacts.filter(msg => {
                return msg.toLowerCase().includes(this.searchelement.toLowerCase())
            })
        },
        filteredList3() {
            return this.services.filter(msg => {
                return msg.text.toLowerCase().includes(this.searchelement.toLowerCase())
            })
        },
        filteredList4() {
            return this.invoices.filter(msg => {
                return msg.text.toLowerCase().includes(this.searchelement.toLowerCase())
            })
        },
        filteredList5() {
            return this.invoices.filter(msg => {
                return msg.text.toLowerCase().includes(this.searchelement.toLowerCase())
            })
        },
        filteredList6() {
            return this.ads.filter(msg => {
                return msg.text.toLowerCase().includes(this.searchelement.toLowerCase())
            })
        }
    },
    mounted() {
        document.body.classList.add("dark-mode");
        var a = new Date();
        var b = a.getHours();
        var c = a.getMinutes();
        if (b < 10) b = '0' + b;
        if (c < 10) c = '0' + c;
        var zeit = b + ':' + c;
        this.time = zeit;
        var months = ['Jan.', 'Feb.', 'Mär.', 'Apr.', 'Mai', 'Jun.', 'Jul.', 'Aug.', 'Sep.', 'Okt.', 'Nov.', 'Dez.'];
        var year = a.getFullYear();
        var month = months[a.getMonth()]
        var date = a.getDate()
        if (date < 10) date = '0' + date;
        this.date = date + ' ' + month + ' ' + year;
    },
    components: {},
    methods: {
        timeConverter: function (UNIX_timestamp) {
            var a = new Date(UNIX_timestamp * 1000);
            var year = a.getYear();
            if (year < 1000) year += 1900;
            var month = a.getMonth() + 1;
            var date = a.getDate()
            var hour = a.getHours()
            var min = a.getMinutes()
            var sec = a.getSeconds()
            if (month < 10) month = '0' + month;
            if (date < 10) date = '0' + date;
            if (hour < 10) hour = '0' + hour;
            if (min < 10) min = '0' + min;
            if (sec < 10) sec = '0' + sec;
            var time = date + '.' + month + '.' + (year + "").substring(2, 4) + ' - ' + hour + ':' + min + ':' + sec;
            return time;
        },
        deleteAd(adnumber) {
            if (this.lastcheck == 0 || (Date.now() / 1000) > this.lastcheck) {
                // eslint-disable-next-line no-undef
                mp.trigger('Client:DeleteAd', adnumber);
                this.lastcheck = (Date.now() / 1000) + (1);
            }
        },
        acceptAd(text, adnumber) {
            if (this.lastcheck == 0 || (Date.now() / 1000) > this.lastcheck) {
                // eslint-disable-next-line no-undef
                mp.trigger('Client:AcceptAd', text, adnumber);
                this.lastcheck = (Date.now() / 1000) + (1);
            }
        },
        claimAd(adnumber) {
            if (this.lastcheck == 0 || (Date.now() / 1000) > this.lastcheck) {
                // eslint-disable-next-line no-undef
                mp.trigger('Client:ClaimAd', adnumber);
                this.lastcheck = (Date.now() / 1000) + (1);
            }
        },
        setStatus() {
            // eslint-disable-next-line no-undef
            mp.trigger("Client:SetStatus", this.playerStatus);
        },
        sendDispatch(to) {
            this.banktext = '';
            this.banknumber = '';
            this.bankto = to;
            this.setting = 27;
        },
        createDispatch() {
            if (!this.fahndungText || !this.banknumber) {
                // eslint-disable-next-line no-undef
                mp.trigger("Client:SendNotificationWithoutButton", 'Bitte alle Felder ausfüllen!', 'error', 'top-left', '4250');
                return;
            }
            if (this.lastcheck11 == 0 || (Date.now() / 1000) > this.lastcheck11) {
                this.lastcheck11 = (Date.now() / 1000) + (45);
                // eslint-disable-next-line no-undef
                mp.trigger("Client:SendDispatch", this.fahndungText, this.banknumber, this.bankto, this.smartphone.shownumber);
                var self = this;
                setTimeout(function () {
                    self.banktext = '';
                    self.banknumber = '';
                    self.bankto = '';
                    self.setting = 26;
                }, 225);
            } else {
                // eslint-disable-next-line no-undef
                mp.trigger("Client:SendNotificationWithoutButton", 'Du hast erst vor kurzem ein Dispatch erstellt!', 'error', 'top-left', '4250');
            }
        },
        closeHandy() {
            if (this.inCall) return;
            this.prepaid = -1;
            // eslint-disable-next-line no-undef
            mp.trigger("Client:CloseHandy");
        },
        muteMicro() {
            if (!this.inCall) return;
            this.muted = !this.muted;
            // eslint-disable-next-line no-undef
            mp.trigger("Client:MuteMicro", this.muted);
        },
        loadCapa() {
            if ((Date.now() / 1000) > this.clicked2) {
                if (this.capacity < 48) {
                    // eslint-disable-next-line no-undef
                    mp.trigger("Client:LoadCapa");
                    this.clicked2 = (Date.now() / 1000) + (3);
                }
            }
        },
        setCapa() {
            this.capacity = 48;
            this.smartphone.phonestatus = 0;
            this.setting = -2;
            this.save = 1;
            this.$forceUpdate();
        },
        scrollToBottom: function () {
            var self = this;
            setTimeout(function () {
                const container = self.$el.querySelector("#message-box");
                container.scrollTop = container.scrollHeight;
            }, 225);
        },
        endCall: function () {
            if (!this.inCall) return;
            if (this.startSound != null) {
                this.stopSound();
                this.stopSound.currentTime = 0;
                this.startstopSoundSound2 = null;
            }
            this.inCall = '';
            this.calltime = 0;
            if (this.lastsetting == 18) {
                this.lastsetting = 0;
            }
            this.setting = this.lastsetting;
            this.getcallnumber = '';
            this.getcallnumber2 = '';
            if (this.calltimer != null) {
                clearInterval(this.calltimer);
                this.calltimer = null;
            }
        },
        endcallServer: function () {
            if (!this.inCall) return;
            this.endCall();
            // eslint-disable-next-line no-undef
            mp.trigger("Client:EndCallServer");
        },
        acceptCall: function () {
            if (this.startSound != null) {
                this.stopSound();
                this.stopSound.currentTime = 0;
                this.startstopSoundSound2 = null;
            }
            this.inCall = this.getcallnumber2;
            // eslint-disable-next-line no-undef
            mp.trigger("Client:AcceptCall");
        },
        declineCall: function () {
            if (this.startSound != null) {
                this.stopSound();
                this.stopSound.currentTime = 0;
                this.startstopSoundSound2 = null;
            }
            this.inCall = '';
            this.calltime = 0;
            this.setting = this.lastsetting;
            if (this.setting == 18) {
                this.setting = 0;
            }
            this.getcallnumber = '';
            this.getcallnumber2 = '';
            if (this.calltimer != null) {
                clearInterval(this.calltimer);
                this.calltimer = null;
            }
            // eslint-disable-next-line no-undef
            mp.trigger("Client:DeclineCall");
        },
        getCall: function (number1, number2, hidden, json, emergency) {
            if (this.inCall == 1 || this.smartphone.phonestatus == 0 || this.capacity <= 0) return;
            var tempdata = [];
            this.emergency = emergency;
            tempdata = JSON.parse(json);
            if (tempdata.phonestatus == 1) {
                let ringtone = "ringtone" + tempdata.ringtone + ".mp3";
                this.playRingtone(ringtone);
                if (tempdata.phonestatus == 2) {
                    this.playRingtone("vibration.mp3");
                }
            }
            if (number2 == this.smartphone.phonenumber) {
                this.calllogs.unshift({
                    "fromnumber": "" + number1,
                    "tonumber": "" + number2,
                    "timestamp": "" + (Date.now() / 1000)
                });
                this.smartphone.phonecalls = this.calllogs.length;
                this.save = 1;
                this.lastsetting = this.setting;
                this.hidden = hidden;
                this.inCall = 1;
                this.calltime = 0;
                this.getcallnumber = number1;
                this.getcallnumber2 = number2;
                var self = this;
                if (this.calltimer != null) {
                    clearInterval(this.calltimer);
                    this.calltimer = null;
                }
                this.calltimer = setInterval(function () {
                    self.calltime = self.calltime + 1;
                }, 1001);
                this.setting = 18;
            } else {
                this.inCallTemp = 1;
            }
        },
        getCallTime: function () {
            var x = parseInt(this.calltime / 60);
            if (x < 10) x = '0' + x;
            var y = parseInt(this.calltime % 60);
            if (y < 10) y = '0' + y;
            return x + ":" + y;
        },
        startTransfer: function () {
            if ((Date.now() / 1000) > this.clicked) {
                if (!this.banknumber || this.bankvalue <= 0 || !this.banktext || !this.bankpin | !this.banksto) {
                    this.sendInfoMessage('Ungültige Eingaben!');
                    return;
                }
                this.retValue = new Object();
                this.retValue.value = this.bankvalue;
                this.retValue.banknumber = this.selectedbank.banknumber;
                this.retValue.empfänger = this.banknumber;
                this.retValue.verwendungszweck = this.banktext;
                this.retValue.tage = this.banksto;
                // eslint-disable-next-line no-undef
                mp.trigger("Client:BankSettings", "transfer2", JSON.stringify(this.retValue));
                this.clicked = (Date.now() / 1000) + (2);
            }
        },
        inVoiceText: function () {
            if (this.ivText == 'Privatrechnung') {
                this.ivText = 'Firmen/Gruppierungsrechnung';
            } else if (this.ivText == 'Firmen/Gruppierungsrechnung') {
                this.ivText = 'Staatsrechnung';
            } else {
                this.ivText = 'Privatrechnung';
            }
        },
        startInvoice: function () {
            if ((Date.now() / 1000) > this.clicked) {
                if (!this.banknumber || this.bankvalue <= 0 || !this.banktext) {
                    this.sendInfoMessage('Ungültige Eingaben!');
                    return;
                }
                this.retValue = new Object();
                this.retValue.value = this.bankvalue;
                this.retValue.empfänger = this.banknumber;
                this.retValue.group = this.ivText;
                this.retValue.banktext = this.banktext;
                // eslint-disable-next-line no-undef
                mp.trigger("Client:BankSettings", "invoice", JSON.stringify(this.retValue));
                this.clicked = (Date.now() / 1000) + (2);
            }
        },
        addSign: function (number) {
            if (this.callnumber.length < 10) {
                this.callnumber = this.callnumber + number;
            }
        },
        addSign2: function (number) {
            try {
                if (this.calculator.length < 20) {
                    if (number != 'C' && number != '=') {
                        this.calculator = this.calculator + number;
                    } else {
                        if (number == 'C') {
                            this.calculator = '';
                        } else if (number == '=') {
                            if (this.calculator.length < 3) return;
                            for (let i = 0; i < this.calculator.length; i++) {
                                if (isNaN(this.calculator[i]) && this.calculator[i] != "/" && this.calculator[i] != "+" && this.calculator[i] != "-" && this.calculator[i] != "*" && this.calculator[i] != ".") {
                                    this.calculator = 'Error';
                                    return;
                                }
                            }
                            let x = this.calculator;
                            let y = eval(x);
                            this.calculator = y.toString();
                        }
                    }
                }
            } catch {
                this.calculator = 'Error';
            }
        },
        setCall: function (number = '0') {
            if (this.smartphone.prepaid != -1 && this.smartphone.prepaid < 5) {
                // eslint-disable-next-line no-undef
                mp.trigger("Client:SendNotificationWithoutButton", 'Nicht genügend Guthaben vorhanden!', 'error', 'top-left', '4250');
                return;
            }
            if (number != '0' || this.callnumber == '') {
                this.callnumber = number;
            }
            if (this.callnumber == '') return;
            if (this.callnumber.length >= 3) {
                let found = this.callnumber.search("0189");
                if (found) {
                    this.callnumber = '0189' + this.callnumber;
                }
                if (this.callnumber == this.smartphone.phonenumber || this.callnumber == 'Unbekannt') return;
                for (let i = 0; i < this.callnumber.length; i++) {
                    if (isNaN(this.callnumber[i]) && this.callnumber[i] != "#" && this.callnumber[i] != "*") {
                        return;
                    }
                }
                this.calllogs.unshift({
                    "fromnumber": "" + this.smartphone.phonenumber,
                    "tonumber": "" + this.callnumber,
                    "timestamp": "" + (Date.now() / 1000)
                });
                this.emergency = 0;
                this.smartphone.phonecalls = this.calllogs.length;
                this.getcallnumber = this.callnumber;
                if (this.smartphone.prepaid > -1) {
                    this.smartphone.prepaid -= 5;
                }
                this.save = 1;
                this.muted = false;
                var hidden = 0;
                if (this.smartphone.shownumber) {
                    hidden = 1;
                }
                this.lastsetting = this.setting;
                // eslint-disable-next-line no-undef
                mp.trigger("Client:SmartPhoneCall", this.smartphone.phonenumber, this.callnumber, hidden);
                this.setting = 18;
                this.inCall = this.callnumber;
                this.calltime = 0;
                var self = this;
                if (this.calltimer != null) {
                    clearInterval(this.calltimer);
                    this.calltimer = null;
                }
                this.calltimer = setInterval(function () {
                    self.calltime = self.calltime + 1;
                }, 1001);
                this.callnumber = '';
            }
        },
        deleteSign: function () {
            if (this.callnumber.length > 0) {
                this.callnumber = this.callnumber.substring(0, this.callnumber.length - 1);
            }
        },
        setNotification: function (notification) {
            this.smartphone.notification = notification;
            this.sendInfoMessage('Benachrichtigungston geändert!');
            if (this.startSound2 != null) {
                this.startSound2.pause();
                this.startSound2.currentTime = 0;
                this.startSound2 = null;
            }
            if (this.startSound != null) {
                this.stopSound();
                this.stopSound.currentTime = 0;
                this.startstopSoundSound2 = null;
            }
            this.setting = 0;
            this.save = 1;
        },
        setRingtone: function (ringtone) {
            this.sendInfoMessage('Klingelton geändert!');
            if (this.startSound2 != null) {
                this.startSound2.pause();
                this.startSound2.currentTime = 0;
                this.startSound2 = null;
            }
            if (this.startSound != null) {
                this.stopSound();
                this.stopSound.currentTime = 0;
                this.startstopSoundSound2 = null;
            }
            this.setting = 0;
            if (this.smartphone.ringtone != ringtone) {
                // eslint-disable-next-line no-undef
                mp.trigger("Client:SaveSmartphone", JSON.stringify(this.smartphone), JSON.stringify(this.contacts), this.smartphone.phonenumber);
                this.save = 1;
                this.smartphone.ringtone = ringtone;
            }
        },
        playMessageSound: function (name) {
            if (this.startSound2 != null) {
                this.startSound2.pause();
                this.startSound2.currentTime = 0;
                this.startSound2 = null;
            }
            var soundata = {
                soundurl: 'https://nemesus-world.de/nwsounds/' + name
            }
            this.startSound2 = new Audio(soundata.soundurl);
            this.startSound2.volume = 0.50;
            this.startSound2.loop = false;
            this.startSound2.play();
        },
        playRingtone: function (name, check) {
            if (this.startSound != null) {
                this.stopSound();
                this.stopSound.currentTime = 0;
                this.startstopSoundSound2 = null;
                if (check == 1 && this.playringtone == name) return;
            }
            var soundata = {
                soundurl: 'https://nemesus-world.de/nwsounds/' + name
            }
            this.playringtone = name;
            this.startSound = new Audio(soundata.soundurl);
            this.startSound.volume = 0.55;
            this.startSound.loop = true;
            this.startSound.play();
        },
        stopSound: function () {
            this.startSound.pause();
            this.startSound.currentTime = 0;
            this.startSound = null;
        },
        turnOff() {
            this.save = 1;
            this.setting = -2;
            this.smartphone.phonestatus = 0;
        },
        turnOn() {
            if (this.setting == -2) {
                this.save = 1;
                this.setting = -3;
                this.playMessageSound("startsoundsmartphone.mp3");
                var self = this;
                setTimeout(function () {
                    self.setting = -1;
                    self.smartphone.phonestatus = 1;
                    timeoutcheck = null;
                }, 5250);
            }
        },
        updateSmartphone(json, sound) {
            if (sound == 2) {
                this.bankaccounts = JSON.parse(json);
                if (this.bankaccounts) {
                    for (let i = 0; i < this.bankaccounts.length; i++) {
                        if (this.bankaccounts[i] && this.selectedbank && this.bankaccounts[i].banknumber == this.selectedbank.banknumber) {
                            this.selectedbank = this.bankaccounts[i];
                            return;
                        }
                    }
                }
            } else {
                if (this.smartphone.phonestatus > 0 && sound == 1 && this.capacity > 0) {
                    if (this.smartphone.phonestatus == 1) {
                        let notificcation = "notification" + this.smartphone.notification + ".mp3";
                        this.playMessageSound(notificcation);
                        if (this.smartphone.phonestatus == 2) {
                            this.playMessageSound("vibration.mp3");
                        }
                    }
                }
                var temp = JSON.parse(json);
                if (sound == 1) {
                    if (temp.tomessage == this.smartphone.phonenumber) {
                        this.messages.push(JSON.parse(json));
                        this.smartphone.messagecount = this.messages.length;
                        this.save = 1;
                        if (this.smartphone.phonestatus > 0 && this.capacity > 0 && this.smartphone.notificationmessages) {
                            // eslint-disable-next-line no-undef
                            mp.trigger("Client:SendNotificationWithoutButton", 'Neue Nachricht von ' + this.getPhoneContact(temp.frommessage) + ' erhalten!', 'info', 'top-left', '4250');
                        }
                        if (this.setting != 5 && this.setting != 6) {
                            this.newmessages = 1;
                        }
                        if (this.setting == 5) {
                            if (this.selectedNumber2) {
                                this.oldmessage = this.newmessage;
                                this.loadChats(this.selectedNumber2);
                                this.$forceUpdate();
                                this.scrollToBottom();
                                this.newmessage = this.oldmessage;
                            }
                        }
                    }
                }
            }
        },
        hideSmartphone() {
            this.showsmartphoneset = false;
            this.handyinfo = '';
            if (this.save == 1) {
                // eslint-disable-next-line no-undef
                mp.trigger("Client:SaveSmartphone", JSON.stringify(this.smartphone), JSON.stringify(this.contacts), this.smartphone.phonenumber);
                this.save = 0;
            }
            if (!this.inCall || this.inCall.length < 7) {
                // eslint-disable-next-line no-undef
                mp.trigger("Client:UnSetSmartphoneObj");
            }
        },
        showSmartphone2() {
            this.showsmartphoneset = !this.showsmartphoneset;
        },
        showSmartphone(json, json2, json3, json4, capacity, hide, premium, prepaid, faction) {
            if (hide == 0) {
                this.showsmartphoneset = !this.showsmartphoneset;
            }
            if (this.showsmartphoneset == true || hide == 1) {
                if (this.setting != -1 && !this.inCall) {
                    if ((this.lastused + (180)) < (Date.now() / 1000)) {
                        this.setting = -1;
                    }
                }
                this.newmessages = 0;
                this.phonecount = 0;
                this.capacity = capacity;
                this.prepaid = prepaid;
                this.smartphone = JSON.parse(json);
                this.premium = premium;
                this.faction = faction;
                if (this.smartphone.wallpaper >= 10 && this.premium <= 0) {
                    this.smartphone.wallpaper = 2;
                    this.save = 1;
                }
                if (this.lastnumber.length > 3 && this.lastnumber != this.smartphone.phonenumber) {
                    this.messages = [];
                    this.messageContacts = [];
                    this.tempArray = [];
                    this.setting = -1;
                    this.lastsetting = -1;
                }
                if (json3 && JSON.parse(json3).length > this.smartphone.messagecount) {
                    this.newmessages = 1;
                    this.smartphone.messagecount = JSON.parse(json3).length;
                    this.save = 1;
                }
                if (json4 && JSON.parse(json4).length > this.smartphone.phonecalls) {
                    this.phonecount = 1;
                    this.smartphone.phonecalls = JSON.parse(json4).length;
                    this.save = 1;
                }
                if (json2) {
                    this.contacts = JSON.parse(json2);
                }
                if (json3) {
                    this.messages = JSON.parse(json3);
                }
                if (json4) {
                    this.calllogs = JSON.parse(json4);
                }
                if (this.capacity > 0) 
                {
                    if (this.smartphone.phonestatus == 0) {
                        this.setting = -2;
                    } else {
                        if (this.setting == -2) {
                            this.setting = -1;
                        }
                    }
                    if (this.setting == 5 || this.setting == 6) {
                        this.newmessages = 0;
                    }
                    if (this.setting == 12) {
                        this.phonecount = 0;
                    }
                    if (this.inCall && this.getcallnumber2 == this.smartphone.phonenumber) {
                        if (!this.calltimer) {
                            this.calltimer = setInterval(function () {
                                self.calltime = self.calltime + 1;
                            }, 1001);
                        }
                        this.setting = 18;
                    }
                }
                else {
                    this.setting = -2;
                    this.smartphone.phonestatus = 0;
                    this.save = 1;
                }
                if (this.lastnumber && this.lastnumber != this.smartphone.phonenumber) {
                    this.lastcheck = (Date.now() / 1000);
                    this.lastcheck2 = (Date.now() / 1000);
                    this.lastcheck3 = (Date.now() / 1000);
                    this.lastcheck4 = (Date.now() / 1000);
                    this.lastcheck5 = (Date.now() / 1000);
                    this.lastcheck6 = (Date.now() / 1000);
                    this.lastcheck7 = (Date.now() / 1000);
                    this.lastcheck8 = (Date.now() / 1000);
                    this.lastcheck9 = (Date.now() / 1000);
                    this.lastcheck10 = (Date.now() / 1000);
                    this.lastcheck11 = (Date.now() / 1000);
                    this.lastcheck12 = (Date.now() / 1000);
                    this.lastcheck13 = (Date.now() / 1000);
                    this.setting = -1;
                }
                this.lastsetting = this.setting;
                this.lastnumber = this.smartphone.phonenumber;
            } else {
                this.handyinfo = '';
                if (this.save == 1) {
                    // eslint-disable-next-line no-undef
                    mp.trigger("Client:SaveSmartphone", JSON.stringify(this.smartphone), JSON.stringify(this.contacts), this.smartphone.phonenumber);
                    this.save = 0;
                }
                if (!this.inCall || this.inCall.length < 7) {
                    // eslint-disable-next-line no-undef
                    mp.trigger("Client:UnSetSmartphoneObj");
                }
            }
            this.lastused = (Date.now() / 1000);
        },
        deleteContact(id) {
            if ((Date.now() / 1000) > this.clicked) {
                this.contacts = this.contacts.filter(item => item.id !== id)
                this.save = 1;
                this.clicked = (Date.now() / 1000) + (2);
            }
        },
        getImgUrl(number) {
            try {
                let ending = '.png';
                if (number >= 10) {
                    ending = '.gif';
                }
                return require('../assets/images/smartphone/Wallpaper' + number + ending)
            } catch (e) {
                return require('../assets/images/smartphone/Wallpaper1.png')
            }
        },
        getWeatherUrl(icon) {
            return ('http://openweathermap.org/img/wn/' + icon + '@2x.png')
        },
        getPhoneContactReverse: function (number) {
            if (number != this.smartphone.phonenumber) {
                if (this.contacts) {
                    for (let i = 0; i < this.contacts.length; i++) {
                        if (this.contacts[i] && (this.contacts[i].number == number || this.contacts[i].name == number)) {
                            return this.contacts[i].number;
                        }
                    }
                }
            }
            return number;
        },
        getLastMessage: function (number) {
            if (number) {
                let temparray = [];
                for (let i = 0; i < this.messages.length; i++) {
                    if (this.messages[i] && ((this.messages[i].frommessage == number && this.messages[i].tomessage == this.smartphone.phonenumber) || (this.messages[i].frommessage == this.smartphone.phonenumber && this.messages[i].tomessage == number))) {
                        temparray.push(this.messages[i].text);
                    }
                }
                if (temparray && temparray.length > 0) {
                    let ret = temparray.slice(-1)[0].substring(0, 23);
                    if (temparray.slice(-1)[0].length > 23) {
                        ret = ret + '...';
                    }
                    return ret;
                }
            }
            return 'Keine Nachrichten!';
        },
        getPhoneContact: function (number) {
            if (number == this.smartphone.phonenumber) {
                return 'Du';
            }
            if (this.contacts) {
                for (let i = 0; i < this.contacts.length; i++) {
                    if (this.contacts[i] && this.contacts[i].number == number) {
                        return this.contacts[i].name;
                    }
                }
            }
            return number;
        },
        backToHome: function () {
            this.setting = 0;
        },
        showChat: function () {
            this.newmessage = '';
            this.newmessages = 0;
            this.setting = 5;
            this.scrollToBottom();
        },
        loadChats: function (contactparam) {
            var contact = this.getPhoneContactReverse(contactparam);
            this.selectedNumber = contact;
            this.selectedNumber2 = contactparam;
            this.tempArray = [];
            for (let i = 0; i < this.messages.length; i++) {
                if (this.messages[i] && this.messages[i].frommessage == contact || this.messages[i].tomessage == contact) {
                    this.tempArray.push(this.messages[i]);
                }
            }
            this.showChat();
        },
        getGPS(text) {
            let position1 = text.indexOf("(")
            let newtext = text.substring(position1 + 1, text.length - 1);
            const posi = newtext.split(',');
            // eslint-disable-next-line no-undef
            mp.trigger("Client:CreateWaypoint", posi[0], posi[1], -1);
            // eslint-disable-next-line no-undef
            mp.trigger("Client:SendNotificationWithoutButton", 'Position markiert!', 'info', 'top-left', '2500');
        },
        sendGPS() {
            if ((Date.now() / 1000) > this.clicked && (Date.now() / 1000) > this.clicked3) {
                // eslint-disable-next-line no-undef
                mp.trigger("Client:SmartphoneMessageGPS");
                this.clicked3 = (Date.now() / 1000) + (12);
            }
        },
        sendGPSMessage(text) {
            this.newmessage = text;
            var self = this;
            setTimeout(function () {
                self.sendMessage();
            }, 125);
        },
        sendMessage() {
            if ((Date.now() / 1000) > this.clicked) {
                if (this.smartphone.prepaid != -1 && this.smartphone.prepaid < 1) {
                    // eslint-disable-next-line no-undef
                    mp.trigger("Client:SendNotificationWithoutButton", 'Nicht genügend Guthaben vorhanden!', 'error', 'top-left', '4250');
                    this.newmessage = '';
                    return;
                }
                if (this.newmessage.length <= 0 || this.newmessage.length > 64) return;
                if (this.newmessage.includes('[') || this.newmessage.includes(']') || this.newmessage.includes('/n')) {
                    // eslint-disable-next-line no-undef
                    mp.trigger("Client:SendNotificationWithoutButton", 'Ungültige Nachricht!', 'error', 'top-left', '4250');
                    return;
                }
                var tempmessage = [];
                tempmessage.timestamp = (Date.now() / 1000);
                tempmessage.tomessage = this.selectedNumber
                tempmessage.frommessage = this.smartphone.phonenumber;
                tempmessage.text = this.newmessage;
                this.tempArray.push({
                    "timestamp": "" + tempmessage.timestamp,
                    "tomessage": "" + tempmessage.tomessage,
                    "frommessage": "" + tempmessage.frommessage,
                    "text": "" + tempmessage.text
                });
                this.messages.push({
                    "timestamp": "" + tempmessage.timestamp,
                    "tomessage": "" + tempmessage.tomessage,
                    "frommessage": "" + tempmessage.frommessage,
                    "text": "" + tempmessage.text
                });
                this.$forceUpdate();
                this.scrollToBottom();
                this.newmessage = '';
                if (this.smartphone.prepaid > -1) {
                    this.smartphone.prepaid -= 1;
                }
                this.save = 1;
                // eslint-disable-next-line no-undef
                mp.trigger("Client:SmartphoneMessage", tempmessage.timestamp, tempmessage.tomessage, tempmessage.frommessage, tempmessage.text);
                this.clicked = (Date.now() / 1000) + (3);
            }
        },
        showChatContacts: function () {
            try {
                if (this.smartphone.prepaid != -1 && this.smartphone.prepaid < 1) {
                    // eslint-disable-next-line no-undef
                    mp.trigger("Client:SendNotificationWithoutButton", 'Nicht genügend Guthaben vorhanden!', 'error', 'top-left', '4250');
                    return;
                }
                if (this.smartphone.prepaid > -1) {
                    this.smartphone.prepaid -= 1;
                    this.save = true;
                }
                for (let i = 0; i < this.messages.length; i++) {
                    if (this.messages[i]) {
                        if (this.messages[i].tomessage != this.smartphone.phonenumber) {
                            this.messageContacts.push(this.messages[i].tomessage);
                        }
                        if (this.messages[i].frommessage != this.smartphone.phonenumber) {
                            this.messageContacts.push(this.messages[i].frommessage);
                        }
                    }
                }
                var uniqueArr = [...new Set(this.messageContacts)]
                this.messageContacts = uniqueArr;
                this.messageContacts = this.messageContacts.sort(function (a, b) {
                    var nameA = a.toUpperCase();
                    var nameB = b.toUpperCase();
                    if (nameA < nameB) {
                        return -1;
                    }
                    if (nameA > nameB) {
                        return 1;
                    }
                    return 0;
                });
                if (this.messageContacts.length >= 2) {
                    this.messageContacts = this.messageContacts.reverse();
                }
                this.newmessages = 0;
                var self = this;
                setTimeout(function () {
                    self.setting = 6;
                }, 175);
            } catch {
                this.sendInfoMessage("Offene Chats konnten nicht geladen werden!")
            }
        },
        showBanking: function () {
            if (this.smartphone.prepaid != -1 && this.smartphone.prepaid < 1) {
                // eslint-disable-next-line no-undef
                mp.trigger("Client:SendNotificationWithoutButton", 'Nicht genügend Guthaben vorhanden!', 'error', 'top-left', '4250');
                return;
            }
            if (this.smartphone.prepaid > -1) {
                this.smartphone.prepaid -= 1;
                this.save = true;
            }
            if (this.lastcheck3 == 0 || (Date.now() / 1000) > this.lastcheck3) {
                // eslint-disable-next-line no-undef
                mp.trigger('Client:ShowSmartphoneBanking');
                this.lastcheck3 = (Date.now() / 1000) + (35);
            } else {
                this.setting = 13;
            }
        },
        showBanking2: function (json, defaultbank) {
            this.handyinfo = '';
            this.bankaccounts = JSON.parse(json);
            this.defaultbank = defaultbank;
            this.setting = 13;
        },
        showBankfiles: function () {
            if (this.lastcheck4 == 0 || (Date.now() / 1000) > this.lastcheck4) {
                // eslint-disable-next-line no-undef
                mp.trigger('Client:ShowSmartphoneBankFiles', this.selectedbank.banknumber);
                this.lastcheck4 = (Date.now() / 1000) + (65);
            } else {
                this.setting = 15;
            }
        },
        showBankfiles2: function (json) {
            this.handyinfo = '';
            this.bankfiles = JSON.parse(json);
            this.setting = 16;
        },
        showBanksettings: function () {
            if (this.lastcheck5 == 0 || (Date.now() / 1000) > this.lastcheck5) {
                // eslint-disable-next-line no-undef
                mp.trigger('Client:ShowSmartphoneBankSettings', this.selectedbank.banknumber);
                this.lastcheck5 = (Date.now() / 1000) + (65);
            } else {
                this.setting = 16;
            }
        },
        showBanksettings2: function (json) {
            this.handyinfo = '';
            this.banksettings = JSON.parse(json);
            this.setting = 15;
        },
        selectBank: function (bank) {
            this.selectedbank = bank;
            this.lastcheck4 = (Date.now() / 1000);
            this.lastcheck5 = (Date.now() / 1000);
            this.banknumber = 'SA3701-';
            this.setting = 14;
        },
        showCalculator: function () {
            this.setting = 17;
        },
        showSettings: function () {
            this.setting = 1;
        },
        showSettings2: function () {
            if (this.startSound2 != null) {
                this.startSound2.pause();
                this.startSound2.currentTime = 0;
                this.startSound2 = null;
            }
            if (this.startSound != null) {
                this.stopSound();
            }
            this.setting = 1;
        },
        getWeatherString: function (description) {
            switch (description) {
                case "clear sky": {
                    return "Klarer Himmel"
                }
                case "few clouds": {
                    return "Bewölkt"
                }
                case "scattered clouds": {
                    return "Bewölkt"
                }
                case "broken clouds": {
                    return "Bewölkt"
                }
                case "overcast clouds": {
                    return "Bewölkt"
                }
                case "shower rain": {
                    return "Regen"
                }
                case "light rain": {
                    return "Regen"
                }
                case "rain": {
                    return "Regen"
                }
                case "thunderstorm": {
                    return "Gewitter"
                }
                case "snow": {
                    return "Schnee"
                }
                case "mist": {
                    return "Nebelig"
                }
                default: {
                    return "Bewölkt"
                }
            }
        },
        setWeatherInfo: function (weatherjson) {
            if(weatherjson != null)
            {
                this.weather = JSON.parse(weatherjson);
            }
            else
            {
                this.weather = null;
            }
            this.setting = 7;
        },
        showWeather: function () {
            if (this.smartphone.prepaid != -1 && this.smartphone.prepaid < 1) {
                // eslint-disable-next-line no-undef
                mp.trigger("Client:SendNotificationWithoutButton", 'Nicht genügend Guthaben vorhanden!', 'error', 'top-left', '4250');
                return;
            }
            if (this.smartphone.prepaid > -1) {
                this.smartphone.prepaid -= 1;
                this.save = true;
            }
            if (this.lastcheck2 == 0 || (Date.now() / 1000) > this.lastcheck2) {
                // eslint-disable-next-line no-undef
                mp.trigger('Client:StartSmartphoneWeather');
                this.lastcheck2 = (Date.now() / 1000) + (60 * 3);
            } else {
                if(this.weather == null)
                {
                    mp.trigger('Client:StartSmartphoneWeather');
                }
                else
                {
                    this.setting = 7;
                }
            }
        },
        showSetNumber: function () {
            this.smartphone.shownumber = !this.smartphone.shownumber;
            this.save = 1;
            this.$forceUpdate();
        },
        showSetNotification: function () {
            this.smartphone.notificationmessages = !this.smartphone.notificationmessages;
            this.save = 1;
            this.$forceUpdate();
        },
        setPhoneStatus: function () {
            if (this.smartphone.phonestatus == 1 || this.smartphone.phonestatus == 0) {
                this.smartphone.phonestatus = 2;
            } else if (this.smartphone.phonestatus == 2) {
                this.smartphone.phonestatus = 3;
            } else if (this.smartphone.phonestatus == 3) {
                this.smartphone.phonestatus = 1;
            }
            this.save = 1;
            this.$forceUpdate();
        },
        showWallpaper: function () {
            this.setting = 2;
        },
        showMusic: function () {
            this.setting = 10;
        },
        showContacts: function () {
            this.searchelement = '';
            this.handyinfo = '';
            this.setting = 3;
        },
        showPhone: function () {
            if (this.inCall) {
                this.setting = 18;
                return;
            }
            this.searchelement = '';
            this.handyinfo = '';
            this.setting = 11;
        },
        addContacts: function () {
            this.contactnumber = '';
            this.contactname = '';
            this.handyinfo = '';
            this.setting = 4;
        },
        getFreeID: function () {
            for (let i = 0; i < 50; i++) {
                if (this.contacts[i]) {
                    continue;
                }
                return i;
            }
        },
        loadCallLogs: function () {
            this.phonecount = 0;
            this.setting = 12;
        },
        loadLifeInvader: function () {
            if (this.smartphone.prepaid != -1 && this.smartphone.prepaid < 1) {
                // eslint-disable-next-line no-undef
                mp.trigger("Client:SendNotificationWithoutButton", 'Nicht genügend Guthaben vorhanden!', 'error', 'top-left', '4250');
                return;
            }
            if (this.smartphone.prepaid > -1) {
                this.smartphone.prepaid -= 1;
                this.save = true;
            }
            if (this.lastcheck13 == 0 || (Date.now() / 1000) > this.lastcheck13) {
                this.searchelement = '';
                // eslint-disable-next-line no-undef
                mp.trigger('Client:LoadLifeInvader');
                this.lastcheck13 = (Date.now() / 1000) + (120);
            } else {
                this.searchelement = '';
                this.setting = 28;
            }
        },
        showLifeInvader: function (json, show, infaction, name, online, price) {
            if (json.length >= 1) {
                try {
                    this.ads = JSON.parse(json);
                } catch {
                    this.ads = json;
                }
            }
            this.islifeinvader = infaction;
            this.myname = name;
            this.lifeinvaders = online;
            this.lifeinvaderprice = price;
            if (show == 1) {
                this.setting = 28;
            }
        },
        createAd: function () {
            if (this.contactname.length < 3 || this.contactnumber.length > 128) {
                // eslint-disable-next-line no-undef
                mp.trigger("Client:SendNotificationWithoutButton", 'Ungültige Eingaben!', 'error', 'top-left', '2250');
                return;
            }
            if (this.lastcheck12 > 0 && (Date.now() / 1000) < this.lastcheck12) {
                // eslint-disable-next-line no-undef
                mp.trigger("Client:SendNotificationWithoutButton", 'Du kannst jetzt keine weitere Anzeige schalten!', 'error', 'top-left', '2250');
                return;
            }
            this.lastcheck12 = (Date.now() / 1000) + (60 * 3);
            // eslint-disable-next-line no-undef
            mp.trigger('Client:CreateAd', this.contactname);
            this.contactname = '';
            this.setting = 28;
        },
        loadService: function () {
            if (this.smartphone.prepaid != -1 && this.smartphone.prepaid < 1) {
                // eslint-disable-next-line no-undef
                mp.trigger("Client:SendNotificationWithoutButton", 'Nicht genügend Guthaben vorhanden!', 'error', 'top-left', '4250');
                return;
            }
            if (this.smartphone.prepaid > -1) {
                this.smartphone.prepaid -= 1;
                this.save = true;
            }
            if (this.lastcheck10 == 0 || (Date.now() / 1000) > this.lastcheck10) {
                this.searchelement = '';
                // eslint-disable-next-line no-undef
                mp.trigger('Client:LoadService');
                this.lastcheck10 = (Date.now() / 1000) + (180);
            } else {
                this.searchelement = '';
                this.setting = 23;
            }
        },
        showService: function (json) {
            if (json.length >= 1) {
                try {
                    this.services = JSON.parse(json);
                } catch {
                    this.services = json;
                }
            }
            this.setting = 23;
        },
        loadDispatch: function () {
            this.setting = 26;
        },
        loadMDC: function () {
            // eslint-disable-next-line no-undef
            mp.trigger("Client:MDCSettings", 'showmdc', 'n/A');
        },
        loadInvoices: function () {
            if (this.smartphone.prepaid != -1 && this.smartphone.prepaid < 1) {
                // eslint-disable-next-line no-undef
                mp.trigger("Client:SendNotificationWithoutButton", 'Nicht genügend Guthaben vorhanden!', 'error', 'top-left', '4250');
                return;
            }
            if (this.smartphone.prepaid > -1) {
                this.smartphone.prepaid -= 1;
                this.save = true;
            }
            if (this.lastcheck10 == 0 || (Date.now() / 1000) > this.lastcheck10) {
                this.searchelement = '';
                // eslint-disable-next-line no-undef
                mp.trigger('Client:LoadInvoices');
                this.banknumber = '';
                this.banktext = '';
                this.bankvalue = '';
                this.ivText = 'Privatrechnung';
                this.lastcheck10 = (Date.now() / 1000) + (180);
            } else {
                this.banknumber = '';
                this.banktext = '';
                this.bankvalue = '';
                this.ivText = 'Privatrechnung';
                this.setting = 24;
            }
        },
        showInvoices: function (json) {
            if (json.length >= 1) {
                try {
                    this.invoices = JSON.parse(json);
                } catch {
                    this.invoices = json;
                }
            }
            this.setting = 24;
        },
        loadTaxi: function () {
            this.contactname = '';
            if (this.smartphone.prepaid != -1 && this.smartphone.prepaid < 1) {
                // eslint-disable-next-line no-undef
                mp.trigger("Client:SendNotificationWithoutButton", 'Nicht genügend Guthaben vorhanden!', 'error', 'top-left', '4250');
                return;
            }
            if (this.smartphone.prepaid > -1) {
                this.smartphone.prepaid -= 1;
                this.save = true;
            }
            if (this.lastcheck8 == 0 || (Date.now() / 1000) > this.lastcheck8) {
                // eslint-disable-next-line no-undef
                mp.trigger('Client:LoadTaxi');
                this.lastcheck8 = (Date.now() / 1000) + (25);
            } else {
                this.setting = 21;
            }
        },
        showTaxi: function (json, charname, taxidrivers, istaxi) {
            if (json.length >= 1) {
                try {
                    this.taxijobs = JSON.parse(json);
                } catch {
                    this.taxijobs = json;
                }
            }
            this.taxidrivers = taxidrivers;
            this.istaxi = istaxi;
            this.charname = charname;
            this.found1 = false;
            if (this.taxijobs.length > 0) {
                if (this.taxijobs.some(t => t.name === charname)) {
                    this.found1 = true;
                }
            }
            this.setting = 21;
        },
        updateTaxiJobs: function (json) {
            if (json.length >= 1) {
                try {
                    this.taxijobs = JSON.parse(json);
                } catch {
                    this.taxijobs = json;
                }
            }
        },
        createTaxi: function () {
            if (this.contactname.length < 6 || this.contactnumber.length > 64) {
                // eslint-disable-next-line no-undef
                mp.trigger("Client:SendNotificationWithoutButton", 'Ungültige Eingaben!', 'error', 'top-left', '2250');
                return;
            }
            if (this.lastcheck9 > 0 && (Date.now() / 1000) < this.lastcheck9) {
                // eslint-disable-next-line no-undef
                mp.trigger("Client:SendNotificationWithoutButton", 'Du kannst jetzt keinen weiteren Taxiauftrag verschicken!', 'error', 'top-left', '2250');
                return;
            }
            this.lastcheck9 = (Date.now() / 1000) + (60 * 2);
            // eslint-disable-next-line no-undef
            mp.trigger('Client:CreateTaxi', this.contactname);
            this.lastcheck8 = (Date.now() / 1000);
            this.contactname = '';
            this.loadTaxi();
        },
        declineTaxi: function (taxiid) {
            if (this.lastcheck == 0 || (Date.now() / 1000) > this.lastcheck) {
                // eslint-disable-next-line no-undef
                mp.trigger('Client:DeclineTaxi', taxiid);
                this.lastcheck = (Date.now() / 1000) + (2);
            }
        },
        acceptTaxi: function (taxiid) {
            if (this.lastcheck == 0 || (Date.now() / 1000) > this.lastcheck) {
                // eslint-disable-next-line no-undef
                mp.trigger('Client:AcceptTaxi', taxiid);
                this.lastcheck = (Date.now() / 1000) + (2);
            }
        },
        acceptInvoice: function (id) {
            if (this.lastcheck == 0 || (Date.now() / 1000) > this.lastcheck) {
                // eslint-disable-next-line no-undef
                mp.trigger('Client:AcceptInvoice', id);
                this.lastcheck = (Date.now() / 1000) + (2);
            }
        },
        payInvoice: function (id) {
            if (this.lastcheck == 0 || (Date.now() / 1000) > this.lastcheck) {
                // eslint-disable-next-line no-undef
                mp.trigger('Client:PayInvoice', id);
                this.lastcheck = (Date.now() / 1000) + (2);
            }
        },
        createContact: function () {
            if(this.capacity <= 0)
            {
                this.turnOff();
                return;
            }
            if (this.contactname.length < 5 || this.contactnumber.length < 6) {
                this.sendInfoMessage('Ungültige Eingaben!');
                return;
            }
            if (this.contacts.length >= 50) {
                this.sendInfoMessage('Max. Speicherplatz erreicht!');
                return;
            }
            var addContact = new Object();
            this.save = 1;
            addContact.id = this.getFreeID();
            addContact.name = this.contactname;
            addContact.number = this.contactnumber;
            this.contacts.push(addContact);
            this.contacts.sort(function (a, b) {
                var nameA = a.name.toUpperCase();
                var nameB = b.name.toUpperCase();
                if (nameA < nameB) {
                    return -1;
                }
                if (nameA > nameB) {
                    return 1;
                }
                return 0;
            });
            // eslint-disable-next-line no-undef
            mp.trigger("Client:SaveSmartphone", JSON.stringify(this.smartphone), JSON.stringify(this.contacts), this.smartphone.phonenumber);
            this.showContacts();
        },
        showUnlockScreen: function () {
            this.setting = -1;
        },
        unlockScreen: function () {
            this.setting = 0;
        },
        selectWallpaper: function (id) {
            if (id >= 10 && this.premium <= 0) {
                this.sendInfoMessage('Du besitzt kein Premium Bronze!');
                this.setting = 0;
                return;
            }
            this.smartphone.wallpaper = id;
            this.sendInfoMessage('Hintergrundbild geändert!');
            this.setting = 0;
            this.save = 1;
        },
        sendInfoMessage: function (msg) {
            this.handyinfo = msg;
            if (timeoutcheck != null) {
                clearTimeout(timeoutcheck)
                timeoutcheck = null;
            }
            var self = this;
            timeoutcheck = setTimeout(function () {
                self.handyinfo = '';
                timeoutcheck = null;
            }, 2750);
            if (msg == 'Überweisung getätigt!') {
                if (this.selectedbank) {
                    this.selectedbank.bankvalue -= this.retValue.value;
                }
            }
        }
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

*::-webkit-scrollbar {
    width: 0px;
}

html * {
    font-size: 1vw;
    scrollbar-width: none;
}

body {
    scrollbar-width: none;
}

body::-webkit-scrollbar {
    width: 0;
    height: 0;
}

.centering {
    width: 100%;
    height: 70%;
    overflow: auto;
    scrollbar-width: none;
    margin: auto;
    position: absolute;
    top: 0;
    left: 0;
    bottom: 0;
    right: 0;
}

@media only screen and (max-height: 1033px) {
    .centering {
        width: 100%;
        height: 75%;
        overflow: auto;
        scrollbar-width: none;
        margin: auto;
        position: absolute;
        top: 0;
        left: 0;
        bottom: 0;
        right: 0;
    }
}

.cutimage {
    width: 19.8vw !important;
    height: 34.5vw !important;
}

.cutimagebackground {
    width: 18.0vw !important;
    height: 30.0vw !important;
    border-radius: 1.5vw;
}

.centering5 {
    margin: 0;
    position: absolute;
    top: 90%;
    right: 2.3%;
    margin-left: -50%;
    transform: translate(-2.5%, -88.5%);
    z-index: -1;
}

.centering6 {
    margin: 0;
    position: absolute;
    top: 90%;
    right: 2.3%;
    margin-left: -50%;
    transform: translate(-2.5%, -88.5%);
    z-index: -1;
}

.centering2 {
    width: 100%;
    height: 100%;
    overflow: hidden;
    scrollbar-width: none;
    margin: auto;
    position: absolute;
    z-index: 1;
}

.centering3 {
    margin: 0;
    position: absolute;
    top: 89%;
    right: 2.7%;
    margin-left: -50%;
    transform: translate(-2.5%, -89%);
    scrollbar-width: none;
}

.centering4 {
    margin: 0;
    position: absolute;
    top: 92%;
    right: 1.4%;
    margin-left: -50%;
    transform: translate(-2.5%, -88.5%)
}

.handybox {
    width: 17.5vw;
    height: 29.35vw;
    font-size: 1vw;
    position: fixed;
    z-index: 2;
}

.handybox2 {
    width: 100%;
    height: 1.3vh;
    padding-top: 0.05vh;
    padding-left: 1.2vh;
    padding-right: 1.2vh;
}

.handybox3 {
    width: 100%;
    height: 100%;
    position: relative;
    z-index: 2;
    scrollbar-width: none;
}

.content {
    margin-top: 2vw;
}

.handybox3icons {
    padding-left: 0.8vw;
    padding-top: 0.7vw;
}

.handybox3icons2 {
    padding-left: 1.8vw;
    padding-top: 0.4vw;
}

.appicon2 {
    width: 3.7vw;
    padding-left: 1.0vw;
    text-shadow: 0 0 9px #000;
    position: relative;
    z-index: 2;
}

.appicon {
    width: 2.0vw;
    margin-left: 1.3vw;
    margin-top: 0.15vw;
    text-shadow: 0 0 9px #000;
}

.handybox4 {
    width: 16.5vw;
    height: 2.8vw;
    margin-left: 0.6vw;
    padding-top: -0.2vw;
    position: absolute;
    bottom: 1.0vw;
    border-radius: 1vw;
    background: rgba(0, 0, 0, 0.5);
    z-index: 2;
}

.handybox4icons {
    padding-left: 1.1vw;
    padding-top: 0.25vw;
}

@media only screen and (max-width: 1200px) {
    .appicon {
        width: 2.0vw;
        margin-left: 1.3vw;
        padding-top: 0.03vw;
        text-shadow: 0 0 9px #000;
    }

    .handybox4 {
        width: 16.5vw;
        height: 2.8vw;
        margin-left: 0.6vw;
        padding-top: -0.2vw;
        position: absolute;
        bottom: 0.4vw;
        border-radius: 1vw;
        background: rgba(0, 0, 0, 0.5);
    }

    .handybox2 {
        width: 100%;
        height: 2.1vw;
        padding-top: 1.30vh;
        padding-left: 1.2vh;
        padding-right: 1.2vh;
    }
}

.hovericon {
    margin-right: 0.4vw;
    margin-top: 0.3vw;
    font-size: 0.9vw;
}

.hovericon:hover {
    color: #3F6791;
}

.iconoff:hover {
    color: red;
}

.iconback:hover {
    color: #3F6791;
}

.iconback2:hover {
    color: orange;
}

.boxheight {
    min-height: 25vw;
}

.bordericon {
    display: inline-block;
    border-radius: 5vw;
    color: white;
    background-color: rgba(0, 0, 0, 0.5);
    box-shadow: 0 0 2px rgba(0, 0, 0, 0.5);
    padding: 0.5em 0.6em;
}

.bordericongreen {
    display: inline-block;
    border-radius: 5vw;
    margin-top: 0.45vw;
    color: green;
    background-color: rgba(0, 0, 0, 0.5);
    box-shadow: 0 0 2px rgba(0, 0, 0, 0.5);
    padding: 0.5em 0.6em;
}

.bordericonred {
    display: inline-block;
    border-radius: 5vw;
    margin-top: 0.45vw;
    color: red;
    background-color: rgba(0, 0, 0, 0.5);
    box-shadow: 0 0 2px rgba(0, 0, 0, 0.5);
    padding: 0.5em 0.6em;
}

.orderphone {
    margin-top: -0.6vw;
    margin-left: 0.5vw;
}

.calculatoritem {
    width: 3.2vw;
    border-radius: 1vw;
    text-shadow: 0 0 9px #000;
    position: relative;
    z-index: 2;
    margin: 0.3vh;
    margin-top: 1.8vh;
}

.ordercalculator {
    margin-top: 0.3vw;
    margin-left: 1.5vw;
}

@media only screen and (max-height: 700px) {
    .ordercalculator {
        margin-top: -0.7vw;
        margin-left: 1.3vw;
    }

    .orderphone {
        margin-top: -0.6vw;
        margin-left: 0.3vw;
    }
}

@media only screen and (max-width: 1024px) {
    .ordercalculator {
        margin-top: -1.35vw;
        margin-left: 0.6vw;
    }

    .orderphone {
        margin-top: -0.6vw;
        margin-left: -0.3vw;
    }
}
</style>
