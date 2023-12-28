<template>
<div class="charactercreator" style="background-color:transparent" v-if="charactercreatorshow">
    <nav class="main-header navbar navbar-expand navbar-white navbar-light bg-primary" style="color:white">
        <ul class="navbar-nav">
            <li class="nav-item">
                <button type="button" role="button" class="btn btn-primary" :class="[(naviSwitch == 'Identität') ? 'active':'']" v-on:click="ResetCharacterCamera();naviSwitch='Identität';reRender();">Identität</button>
            </li>
            <li class="nav-item">
                <button type="button" role="button" class="btn btn-primary" :class="[(naviSwitch == 'Genetik') ? 'active':'']" @click="naviSwitch='Genetik'">Genetik</button>
            </li>
            <li class="nav-item">
                <button type="button" role="button" class="btn btn-primary" :class="[(naviSwitch == 'Haare') ? 'active':'']" @click="naviSwitch='Haare'">Haare</button>
            </li>
            <li class="nav-item">
                <button type="button" role="button" class="btn btn-primary" :class="[(naviSwitch == 'Gesichtsform') ? 'active':'']" @click="naviSwitch='Gesichtsform'">Gesichtsform</button>
            </li>
            <li class="nav-item">
                <button type="button" role="button" class="btn btn-primary" :class="[(naviSwitch == 'Details') ? 'active':'']" @click="naviSwitch='Details'">Details</button>
            </li>
            <li class="nav-item">
                <button type="button" role="button" class="btn btn-primary" :class="[(naviSwitch == 'Kleidung') ? 'active':'']" @click="naviSwitch='Kleidung'">Kleidung</button>
            </li>
            <li class="nav-item">
                <button type="button" role="button" class="btn btn-primary" :class="[(naviSwitch == 'Abschluss') ? 'active':'']" v-on:click="ResetCharacterCamera();naviSwitch='Abschluss'">Abschluss</button>
            </li>
        </ul>
    </nav>
    <div style="overflow: auto">
        <aside class="main-sidebar sidebar-dark-primary elevation-4" style="background-color: #1E282C">
            <a href="#" class="brand-link" style="background-color: #1A2226">
                <img src="../assets/images/logoklein.png" class="brand-image img-circle" style="opacity: .8;color:white">
                <strong><span class="brand-text font-weight" style="font-family: 'Exo', sans-serif;">Nemesus
                        World</span></strong>
            </a>
            <div class="sidebar">
                <div class="user-panel mt-3 pb-3 mb-3 d-flex">
                    <strong><span class="brand-text font-weight" style="font-family: 'Exo', sans-serif;">Charaktererstellung</span></strong>
                </div>
                <nav class="mt-1" v-if="naviSwitch == 'Identität'">
                    <li class="active">
                        <span class="fa fa-venus-mars mr-2"></span>Geschlecht
                        <br />
                        <div v-if="character.gender == true">
                            <button type="button" class="btn btn-primary" style="background-color: #104e6b; border: 0" v-on:click="characterCustomize2('gender',true)">Männlich</button>
                            <button type="button" class="btn btn-secondary ml-2" v-on:click="characterCustomize2('gender',false)">Weiblich</button>
                        </div>
                        <div v-if="character.gender == false">
                            <button type="button" class="btn btn-secondary" v-on:click="characterCustomize2('gender',true)">Männlich</button>
                            <button type="button" class="btn btn-primary ml-2" style="background-color: #104e6b; border: 0" v-on:click="characterCustomize2('gender',false)">Weiblich</button>
                        </div>
                    </li>
                    <li class="active">
                        <span class="fa fa-calendar mt-3 mr-2"></span>Geburtsdatum
                        <input placeholder="01.01.2000" type="text" class="form-control" maxlength="10" v-model="character.birth" autofocus>
                    </li>
                    <li class="active">
                        <span class="fa fa-user mt-3 mr-2"></span><span style="color:green" v-if="nameerror==false">Vorname</span><span style="color:red" v-if="nameerror==true">Vorname</span>
                        <input v-model="firstname" placeholder="Vorname" type="text" class="form-control" maxlength="17">
                    </li>
                    <li class="active">
                        <span class="fa fa-user mt-3 mr-2"></span><span style="color:green" v-if="nameerror==false">Nachname</span><span style="color:red" v-if="nameerror==true">Nachname</span>
                        <input v-model="lastname" placeholder="Nachname" type="text" class="form-control" maxlength="18">
                    </li>
                    <button type="button" class="btn btn-primary mt-2" v-on:click="checkCharacterName()">Name prüfen</button>
                    <li class="active">
                        <span class="fa fa-flag mt-3 mr-2"></span>Herkunft
                        <input v-model="character.origin" type="text" class="form-control" maxlength="30" placeholder="Los-Santos">
                    </li>
                    <li class="active">
                        <span class="fa fa-plane mr-2 mt-3"></span>Einreise
                        <br />
                        <div v-if="legal == 1">
                            <button type="button" class="btn btn-primary" style="background-color: #104e6b; border: 0" v-on:click="selectLegal(1)">Legal</button>
                            <button type="button" class="btn btn-secondary ml-2" v-on:click="selectLegal(2)">Illegal</button>
                        </div>
                        <div v-if="legal == 2">
                            <button type="button" class="btn btn-secondary" v-on:click="selectLegal(1)">Legal</button>
                            <button type="button" class="btn btn-primary ml-2" style="background-color: #104e6b; border: 0" v-on:click="selectLegal(2)">Illegal</button>
                        </div>
                    </li>
                    <div class="mt-4">
                        <hr />
                    </div>
                    <div style="bottom: 0" class="mt-2">
                        <div style="display: flex; justify-content: center; align-items: center;"></div>
                        <span class="text-center">
                            <p>Gamemode mit ❤️ erstellt von Nemesus.de!</p>
                        </span>
                    </div>
                </nav>
                <nav v-if="naviSwitch == 'Genetik'">
                    <ul class="list-unstyled components mb-1">
                        <li class="active mt-2" v-for="(blend, index) in blendData" :key="index">
                            <span class="contenttext">{{blend[0]}} [{{character.blendData[index]}}]</span>
                            <vue-range-slider ref="slider" tooltip="false" dotSize="12" width="100%" height="12" v-oninput="characterCustomize('blendData',index, character.blendData[index])" v-model="character.blendData[index]" :min="blend[1]" :max="blend[2]" :step="blend[3]" />
                        </li>
                        <div class="mt-2">
                            <span class="contenttext">Augenfarbe [{{character.eyeColor}}]</span>
                            <vue-range-slider ref="slider" tooltip="false" dotSize="12" height="12" :min="0" :max="31" value='0' :step="1" v-model="character.eyeColor" v-oninput="characterCustomize2('eyeColor', character.eyeColor)" />
                        </div>
                    </ul>
                </nav>
                <nav v-if="naviSwitch == 'Gesichtsform'">
                    <ul class="list-unstyled components mb-1">
                        <li class="active mt-2" v-for="(faceFeature, index) in faceFeatures" :key="index">
                            <span class="contenttext">{{faceFeatures[index]}} [{{character.faceFeatures[index]}}]</span>
                            <vue-range-slider ref="slider" tooltip="false" dotSize="12" height="12" :min="-1" :max="1" :step="0.1" v-model="character.faceFeatures[index]" v-oninput="characterCustomize('faceFeatures',index, character.faceFeatures[index])" />
                        </li>
                    </ul>
                </nav>
                <nav v-if="naviSwitch == 'Details'">
                    <ul class="list-unstyled components">
                        <li class="active mt-2" v-for="(headOverlay, index) in headOverlays" :key="index">
                            <span class="contenttext">{{headOverlays[index]}}
                                [{{character.headOverlays[index]}},{{character.headOverlaysColors[index]}}]</span><span class="ml-2 fa fa-window-close icons" :key="index+1" v-on:click="character.headOverlays[index] = -1; character.headOverlaysColors[index] = 0; reRender(); characterCustomize3(index, character.headOverlays[index], character.headOverlaysColors[index])" style="color:lightred;cursor: pointer"></span>
                            <vue-range-slider ref="slider" tooltip="false" dotSize="12" height="12" :min="-1" :max="headOverlaysMax[index]" :step="1" v-model="character.headOverlays[index]" v-oninput="characterCustomize3(index, character.headOverlays[index], character.headOverlaysColors[index])" />
                            <vue-range-slider ref="slider" tooltip="false" dotSize="12" height="12" :min="0" :max="63" :step="1" v-model="character.headOverlaysColors[index]" v-oninput="characterCustomize3(index, character.headOverlays[index], character.headOverlaysColors[index])" />
                        </li>
                    </ul>
                </nav>
                <nav v-if="naviSwitch == 'Abschluss'">
                    <ul class="list-unstyled components mb-3">
                        Bist du mit deinem Charakter zufrieden? Im Hintergrund wird dein Charakter überprüft ob er realistisch
                        gestaltet wurde.
                    </ul>
                    <p style="color: white">Bitte beachte, wir sind ein <strong>Roleplay-Server</strong> demzufolge schlüpfst du
                        in die
                        Rolle eines Charakteres und spielst diesen aus. Für einen reibungslosen Spielverlauf gibt es
                        Regeln, diese
                        findest du unter <strong>https://regeln.nemesus-world.de</strong>, bitte lese dir diese aufmerksam
                        durch!</p>
                    <p>Nach dem Klicken von 'Charakter erstellen' wird dein Charakter erstellt, dann beginnt dein IC
                        Leben, außerdem wird ein Foto von deinem Charakter angefertigt. Bitte klicke erst auf 'Charakter
                        erstellen' wenn du mit deinem Charakter und der Position zufrieden bist!</p>
                    <button v-on:click="createCharacter()" type="button" class="btn btn-success mt-2 ml-4 animate__animated animate__heartBeat">Charakter
                        erstellen</button>
                </nav>
                <nav v-if="naviSwitch == 'Kleidung'">
                    <ul class="list-unstyled components mb-2">
                        <li class="active mt-2">
                            <span class="contenttext">{{clothings[0]}} [{{character.clothing[0]}}]</span>
                            <vue-range-slider ref="slider" tooltip="false" dotSize="12" height="12" :min="0" :max="clothingMax[0]" :step="1" v-model="character.clothing[0]" v-oninput="characterCustomize('clothing', 0, character.clothing[0])" />
                        </li>
                        <li class="active mt-2">
                            <span class="contenttext">{{clothings[4]}} [{{character.clothing[4]}}]</span>
                            <vue-range-slider ref="slider" tooltip="false" dotSize="12" height="12" :min="0" :max="clothingMax[4]" :step="1" v-model="character.clothing[4]" v-oninput="characterCustomize('clothing', 4, character.clothing[4])" />
                        </li>
                        <li class="active mt-2">
                            <span class="contenttext">{{clothings[1]}} [{{character.clothing[1]}}]</span>
                            <vue-range-slider ref="slider" tooltip="false" dotSize="12" height="12" :min="0" :max="clothingMax[1]" :step="1" v-model="character.clothing[1]" v-oninput="characterCustomize('clothing', 1, character.clothing[1])" />
                        </li>
                        <li class="active mt-2">
                            <span class="contenttext">{{clothings[2]}} [{{character.clothing[2]}}]</span>
                            <vue-range-slider ref="slider" tooltip="false" dotSize="12" height="12" :min="0" :max="clothingMax[2]" :step="1" v-model="character.clothing[2]" v-oninput="characterCustomize('clothing', 2, character.clothing[2])" />
                        </li>
                        <li class="active mt-2">
                            <span class="contenttext">{{clothings[3]}} [{{character.clothing[3]}}]</span>
                            <vue-range-slider ref="slider" tooltip="false" dotSize="12" height="12" :min="0" :max="clothingMax[3]" :step="1" v-model="character.clothing[3]" v-oninput="characterCustomize('clothing', 3, character.clothing[3])" />
                        </li>
                    </ul>
                    <div style="display: flex; justify-content: center; align-items: center;">
                        <button type="button" class="btn btn-primary" v-on:click="resetClothes()">Kleidung reseten</button>
                    </div>
                    <div class="mt-3">
                        <div style="display: flex; justify-content: center; align-items: center;"></div>
                        <span class="text-center">
                            <p>Weitere Kleidungsstücke können später im Kleidungsladen erworben werden!</p>
                        </span>
                    </div>
                </nav>
                <nav v-if="naviSwitch == 'Haare'">
                    <ul class="list-unstyled components mb-1">
                        <li>
                            <span class="contenttext">Haarstil [{{character.hair[0]}}]</span>
                            <vue-range-slider ref="slider" tooltip="false" dotSize="12" height="12" min="0" :max="hairMax" value="1" v-model="character.hair[0]" v-oninput="characterCustomize('hair', 0, character.hair[0])" />
                            <hr />
                        </li>
                        <li>
                            <span class="contenttext">Haarfarbe</span>
                            <div class="hair">
                                <ul class="hairColors-1">
                                    <li v-for="(hairColor, index) in hairColors" :key="hairColor" :style="{ background: hairColor }" v-on:click="characterCustomize('hair', 1, index)">
                                    </li>
                                </ul>
                            </div>
                            <hr />
                        </li>
                        <li>
                            <span class="contenttext">Haartönung</span>
                            <div class="hair">
                                <ul class="hairColors-2">
                                    <li v-for="(hairColor, index) in hairColors" :key="hairColor" :style="{ background: hairColor }" v-on:click="characterCustomize('hair', 2,index)">
                                    </li>
                                </ul>
                            </div>
                            <hr />
                        </li>
                        <li>
                            <span class="contenttext">Bartstil [{{character.beard[0]}}]</span>
                            <span class="ml-2 fa fa-window-close icons" :key="index+1" v-on:click="character.beard[0] = -1; character.beard[1] = 0; reRender(); characterCustomize('beard', 0, character.beard[0])" style="color:lightred;cursor: pointer"></span>
                            <vue-range-slider ref="slider" tooltip="false" dotSize="12" height="12" min="-1" max="28" value="1" v-model="character.beard[0]" v-oninput="characterCustomize('beard', 0, character.beard[0])" />
                            <hr />
                        </li>
                        <li>
                            <span class="contenttext">Bartfarbe</span>
                            <div class="beard">
                                <ul class="beardColors">
                                    <li v-for="beardColor in beardColors" :key="beardColor" :style="{ background: hairColors[beardColor] }" v-on:click="characterCustomize('beard',1,beardColor)"></li>
                                </ul>
                            </div>
                        </li>
                    </ul>
                </nav>
            </div>
        </aside>
    </div>
</div>
</template>

<script>
import Swal from 'sweetalert2/dist/sweetalert2.js'
import 'vue-range-component/dist/vue-range-slider.css'
import VueRangeSlider from 'vue-range-component'
import {
    invalidHairMen,
    invalidHairWoman,
    invalidMenTop,
    invalidWomanTop,
    invalidMenLeg,
    invalidWomanLeg,
    invalidMenTorso,
    invalidWomanTorso,
    invalidMenShoe,
    invalidWomanShoe,
    invalidMenTShirt,
    invalidWomanTShirt,
    maxOberbekleidungMen,
    maxTorsoMen,
    maxLegsMen,
    maxShoesMen,
    maxTShirtMen,
    maxTorsoWomen,
    maxLegsWomen,
    maxShoesWomen,
    maxOberbekleidungWomen,
    maxTShirtWomen,
} from './helper/externals/misc.js'
import {
    gueltigesDatum,
    getAge
} from './helper/functions.js'

export default {
    name: 'CharacterCreator',
    data: function () {
        return {
            charactercreatorshow: false,
            firstname: '',
            lastname: '',
            legal: 1,
            namecheck: (Date.now() / 1000),
            createcheck: (Date.now() / 1000),
            character: {
                birth: '',
                origin: '',
                hair: [0, 0, 0],
                beard: [-1, 0],
                blendData: [0, 0, 0, 0, 0, 0],
                faceFeatures: [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
                clothing: [0, 0, 0, 1, 0, 0, 0, 255, 0, 255, 255, 255, 0],
                clothingColor: [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
                headOverlays: [-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1],
                headOverlaysColors: [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
                eyeColor: 0,
                gender: true
            },
            nameerror: true,
            naviSwitch: 'Identität',
            hairColors: [
                '#0c0c0c', '#1d1a17', '#281d18', '#3d1f15', '#682e19', '#954b29', '#a35234', '#9b5f3d', '#b57e54', '#c19167',
                '#af7f53', '#be9560', '#d0ac75', '#b37f43', '#dbac68', '#e4ba7e', '#bd895a', '#83422c', '#8e3a28', '#8a241c',
                '#962b20', '#a7271d', '#c4351f', '#d8421f', '#c35731', '#d24b21', '#816755', '#917660', '#a88c74', '#d0b69e',
                '#513442', '#744557', '#a94663', '#cb1e8e', '#f63f78', '#ed9393', '#0b917e', '#248081', '#1b4d6b', '#578d4b',
                '#235433', '#155146', '#889e2e', '#71881b', '#468f21', '#cc953d', '#ebb010', '#ec971a', '#e76816', '#e64810',
                '#ec4d0e', '#c22313', '#e43315', '#ae1b18', '#6d0c0e', '#281914', '#3d241a', '#4c281a', '#5d3929', '#69402b',
                '#291b16', '#0e0e10', '#e6bb84', '#d8ac74'
            ],
            beardColors: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 26, 27, 28, 29, 55, 56, 57, 58, 59, 60, 61, 62, 63],
            blendData: [
                ['Gesichtsform (Mutter)', 0, 45, 1],
                ['Gesichtsform (Vater)', 0, 45, 1],
                ['Hautfarbe (Mutter)', 0, 45, 1],
                ['Hautfarbe (Vater)', 0, 45, 1],
                ['Gesichtsform mischen', -1, 1, 0.1],
                ['Hautfarbe mischen', -1, 1, 0.1]
            ],
            faceFeatures: [
                'Nasenbreite', 'Nasenhöhe', 'Nasenlänge', 'Nasenbreite Brücke', 'Nasenposition',
                'Nasenrücken', 'Augenbrauenhöhe', 'Augenbrauenbreite', 'Gesässhöhe',
                'Gesässbreite', 'Wangenbreite', 'Augen', 'Lippen', 'Wangenlänge', 'Wangenhöhe',
                'Kinnlänge', 'Kinnposition', 'Bartbreite', 'Bartform', 'Nackenbreite',
            ],
            clothings: ['Oberbekleidung', 'Torso', 'Hose', 'Schuhe', 'T-Shirt'],
            clothingMax: [maxOberbekleidungMen, maxTorsoMen, maxLegsMen, maxShoesMen, maxTShirtMen],
            clothingMaxWoman: [maxOberbekleidungWomen, maxTorsoWomen, maxLegsWomen, maxShoesWomen, maxTShirtWomen],
            hairMax: 80,
            headOverlays: ['Schönheitsfehler', 'Augenbrauen', 'Altersflecken', 'Make-up', 'Rötung', 'Teint', 'Sonnenschaden', 'Lippenstift', 'Sommersprossen', 'Brustbehaarung', 'Hautunreinheiten', 'Körperunreinheiten'],
            headOverlaysMax: [23, 33, 14, 74, 32, 11, 10, 9, 17, 16, 11, 1],
        }
    },
    mounted() {
        document.body.classList.add("dark-mode");
    },
    components: {
        VueRangeSlider
    },
    methods: {
        reRender: function () {
            this.$forceUpdate();
        },
        selectLegal: function (legal) {
            this.legal = legal;
            return;
        },
        showCharacterCreator: function () {
            this.charactercreatorshow = !this.charactercreatorshow;
            return;
        },
        characterCustomize: function (x, id, val) {
            if (x == "beard") {
                if (this.character[x][id] == -1) {
                    this.character[x][id] = 255;
                }
            }
            this.character[x][id] = val;
            // eslint-disable-next-line no-undef
            mp.trigger('Client:CharacterPreview', x, JSON.stringify(this.character[x]));
            if (x == "beard") {
                if (this.character[x][id] == 255) {
                    this.character[x][id] = -1;
                }
            }
        },
        ResetCharacterCamera: function () {
            // eslint-disable-next-line no-undef
            mp.trigger('Client:ResetCharacterCamera');
        },
        createCharacter: function () {
            if ((Date.now() / 1000) < this.createcheck) return;
            if (this.firstname < 3 || this.lastname < 3 || this.firstname > 17 || this.lastname > 18 || this.nameerror == true) {
                Swal.fire({
                    icon: 'error',
                    title: 'Fehler',
                    text: 'Ungültiger Name, Namen prüfen!',
                    showConfirmButton: false,
                    timer: 2500
                })
                return;
            }
            if (!gueltigesDatum(this.character.birth)) {
                Swal.fire({
                    icon: 'error',
                    title: 'Fehler',
                    text: 'Geburtsdatum bitte korrgieren!',
                    showConfirmButton: false,
                    timer: 2500
                })
                return;
            }
            if (getAge(this.character.birth) < 13 || getAge(this.character.birth) > 99) {
                Swal.fire({
                    icon: 'error',
                    title: 'Fehler',
                    text: 'Das IC Mindestalter ist 13 Jahre und das Maximalalter ist 99 Jahre!',
                    showConfirmButton: false,
                    timer: 2500
                })
                return;
            }
            if (this.character.origin.length < 5 || this.character.origin.length > 30) {
                Swal.fire({
                    icon: 'error',
                    title: 'Fehler',
                    text: 'Ungültige Herkunft!',
                    showConfirmButton: false,
                    timer: 2500
                })
                return;
            }
            if (!this.legal) {
                Swal.fire({
                    icon: 'error',
                    title: 'Fehler',
                    text: 'Bitte Einreiseart auswählen!',
                    showConfirmButton: false,
                    timer: 2500
                })
            }
            if (this.character.gender == true) {
                if (invalidHairMen == this.character.hair[0]) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Fehler',
                        text: 'Ungültiger Haarstil, bitte wähle einen anderen!',
                        showConfirmButton: false,
                        timer: 2500
                    })
                    return;
                }
                if (invalidMenTop.includes(this.character.clothing[0])) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Fehler',
                        text: 'Ungültige Oberbekleidung, bitte wähle einen anderen!',
                        showConfirmButton: false,
                        timer: 2500
                    })
                    return;
                }
                if (invalidMenTShirt.includes(this.character.clothing[4])) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Fehler',
                        text: 'Ungültiges T-Shirt, bitte wähle ein anderes aus!',
                        showConfirmButton: false,
                        timer: 2500
                    })
                    return;
                }
                if (invalidMenTorso.includes(this.character.clothing[1])) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Fehler',
                        text: 'Ungültiger Torso, bitte wähle einen anderen!',
                        showConfirmButton: false,
                        timer: 2500
                    })
                    return;
                }
                if (invalidMenLeg.includes(this.character.clothing[2])) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Fehler',
                        text: 'Ungültige Hose, bitte wähle eine andere!',
                        showConfirmButton: false,
                        timer: 2500
                    })
                    return;
                }
                if (invalidMenShoe.includes(this.character.clothing[3])) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Fehler',
                        text: 'Ungültige Schuhe, bitte wähle ein anderes Paar aus!',
                        showConfirmButton: false,
                        timer: 2500
                    })
                    return;
                }
            } else {
                if (invalidHairWoman == this.character.hair[0]) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Fehler',
                        text: 'Ungültiger Haarstil, bitte wähle einen anderen!',
                        showConfirmButton: false,
                        timer: 2500
                    })
                    return;
                }
                if (invalidWomanTop.includes(this.character.clothing[0])) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Fehler',
                        text: 'Ungültige Oberbekleidung, bitte wähle einen anderen!',
                        showConfirmButton: false,
                        timer: 2500
                    })
                    return;
                }
                if (invalidWomanTShirt.includes(this.character.clothing[4])) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Fehler',
                        text: 'Ungültiges T-Shirt, bitte wähle ein anderes aus!',
                        showConfirmButton: false,
                        timer: 2500
                    })
                    return;
                }
                if (invalidWomanTorso.includes(this.character.clothing[1])) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Fehler',
                        text: 'Ungültiger Torso, bitte wähle einen anderen!',
                        showConfirmButton: false,
                        timer: 2500
                    })
                    return;
                }
                if (invalidWomanLeg.includes(this.character.clothing[2])) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Fehler',
                        text: 'Ungültige Hose, bitte wähle eine andere!',
                        showConfirmButton: false,
                        timer: 2500
                    })
                    return;
                }
                if (invalidWomanShoe.includes(this.character.clothing[3])) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Fehler',
                        text: 'Ungültige Schuhe, bitte wähle ein anderes Paar aus!',
                        showConfirmButton: false,
                        timer: 2500
                    })
                    return;
                }
            }
            this.createcheck = (Date.now() / 1000) + (35);
            // eslint-disable-next-line no-undef
            mp.trigger('Client:CreateNewCharacterFinish', JSON.stringify(this.character), this.firstname.trim() + " " + this.lastname.trim(), this.legal);
        },
        checkCharacterName: function () {
            if (this.firstname.length < 3 || this.lastname.length < 3 || this.firstname.length > 17 || this.lastname.length > 18) {
                this.nameerror = true;
                return;
            }
            if ((Date.now() / 1000) > this.namecheck) {
                this.namecheck = (Date.now() / 1000) + (3);
                // eslint-disable-next-line no-undef
                mp.trigger('Client:CheckCharacterName', this.firstname.trim() + " " + this.lastname.trim());
            }
        },
        resetClothes: function () {
            this.character.clothing = [0, 0, 0, 1, 0, 0, 0, 255, 0, 255, 255, 255, 0];
            this.reRender();
            setTimeout(function () {
                // eslint-disable-next-line no-undef
                mp.trigger('Client:CharacterChangeClothes');
            }, 125);
        },
        characterAnswer: function (answer) {
            if (answer == 1) {
                this.nameerror = true;
            } else {
                this.nameerror = false;
            }
        },
        characterCustomize2: function (x, val) {
            if (x == "gender") {
                if (val != this.character.gender) {
                    this.character.hair = [0, 0, 0];
                    this.character.beard = [-1, 0];
                    if (val == true) {
                        this.hairMax = 80;
                        this.character.blendData = [0, 0, 0, 0, 0, 0];
                        // eslint-disable-next-line no-undef
                        mp.trigger('Client:SetCharacterMen');
                        this.clothingMax = [maxOberbekleidungMen, maxTorsoMen, maxLegsMen, maxShoesMen, maxTShirtMen];
                    } else {
                        this.hairMax = 86;
                        this.character.blendData = [36, 0, 0, 0, 0, 0];
                        // eslint-disable-next-line no-undef
                        mp.trigger('Client:SetCharacterWoman');
                        this.clothingMax = [maxOberbekleidungWomen, maxTorsoWomen, maxLegsWomen, maxShoesWomen, maxTShirtWomen];
                    }
                    this.character.faceFeatures = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
                    this.character.clothing = [0, 0, 0, 1, 0, 0, 0, 255, 0, 255, 255, 255, 0];
                    this.character.headOverlays = [-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1];
                    this.character.headOverlaysColors = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
                    this.character.eyeColor = 0;
                    this.reRender();
                }
                this.character.gender = val;
            }
            this.character[x] = val;
            // eslint-disable-next-line no-undef
            mp.trigger('Client:CharacterPreview2', x, val);
        },
        characterCustomize3: function (id, val, val2) {
            if (this.character['headOverlays'][id] == -1) {
                this.character['headOverlays'][id] = 255;
            }
            this.character['headOverlays'][id] = val;
            this.character['headOverlaysColors'][id] = val2;
            // eslint-disable-next-line no-undef
            mp.trigger('Client:CharacterPreview3', 'headOverlays', JSON.stringify(this.character['headOverlays']), JSON.stringify(this.character['headOverlaysColors']));
            if (this.character['headOverlays'][id] == 255) {
                this.character['headOverlays'][id] = -1;
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

.hair {
    width: 100%;
    margin: 0 auto;
    margin-top: 10px;
    display: flex;
    flex-direction: column;
    cursor: pointer;
}

.hair ul {
    display: flex;
    grid-gap: 3px;
    justify-content: center;
    align-items: center;
    flex-wrap: wrap;
    list-style: none;
    padding: 0;
}

.hair ul li {
    width: 25px;
    height: 25px;
    border-radius: 100%;
    transition: all 0.4s ease;
    margin: 3px;
    border: 2px solid transparent;
}

.hair ul li:active {
    background: white !important;
}

.hair ul li:hover {
    box-shadow: 0 5px 10px rgb(0 0 0 / 15%);
    border-color: white;
}

.beard {
    width: 100%;
    margin: 0 auto;
    margin-top: 10px;
    display: flex;
    flex-direction: column;
    align-items: center;
}

.beard ul {
    display: flex;
    grid-gap: 3px;
    justify-content: center;
    align-items: center;
    flex-wrap: wrap;
    list-style: none;
    padding: 0;
}

.beard ul li {
    width: 25px;
    height: 25px;
    transition: all 0.4s ease;
    margin: 2px;
    border: 2px solid transparent;
}

.beard ul li:active {
    background: white !important;
}

.beard ul li:hover {
    box-shadow: 0 5px 10px rgb(0 0 0 / 15%);
    border-color: white;
}

.icons:hover {
    color: rgb(230, 33, 33);
}

.vue-range-slider.slider-component .slider-tooltip-wrap .slider-tooltip {
    display: block;
    font-size: 26px;
    white-space: nowrap;
    padding: 2px 5px;
    min-width: 20px;
    text-align: center;
    color: #fff;
    border-radius: 5px;
    border: 1px solid #3498db;
    background-color: #3498db;
}
</style>
