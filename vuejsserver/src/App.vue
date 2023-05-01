<template>
<div id="app">
    <Loading ref="loading" />
    <Login ref="login" />
    <Register ref="register" />
    <CharacterSwitch ref="characterswitch" />
    <CharacterCreator ref="charactercreator" />
    <Bank ref="bank" />
    <Hud ref="hud" />
    <Menu ref="menu" />
    <RPQuiz ref="rpquiz" />
    <Inventory ref="inventory" />
    <SelectWheel ref="selectwheel" />
    <Speedometer ref="speedometer" />
    <Furniture ref="furniture" />
    <Spediteur ref="spediteur" />
    <Shop ref="shop" />
    <Smartphone ref="smartphone" />
    <Progressbar ref="progressbar" />
    <MDC ref="mdc" />
</div>
</template>

<script>
import Loading from './components/Loading.vue'
import Login from './components/Login.vue'
import Register from './components/Register.vue'
import CharacterSwitch from './components/CharacterSwitch.vue'
import CharacterCreator from './components/CharacterCreator.vue'
import Hud from './components/Hud.vue'
import Menu from './components/Menu.vue'
import RPQuiz from './components/RPQuiz.vue'
import Inventory from './components/Inventory.vue'
import SelectWheel from './components/SelectWheel.vue'
import Speedometer from './components/Speedometer.vue'
import Furniture from './components/Furniture.vue'
import Bank from './components/Bank.vue'
import Spediteur from './components/Spediteur.vue'
import Shop from './components/Shop.vue'
import Smartphone from './components/Smartphone.vue'
import Progressbar from './components/Progressbar.vue'
import MDC from './components/MDC.vue'

export default {
    name: 'App',
    components: {
        Loading,
        Login,
        Register,
        CharacterSwitch,
        CharacterCreator,
        Hud,
        Menu,
        RPQuiz,
        Inventory,
        SelectWheel,
        Speedometer,
        Furniture,
        Bank,
        Spediteur,
        Shop,
        Smartphone,
        Progressbar,
        MDC
    },
    mounted() {
        global.gui.loading = this.$refs.loading;
        global.gui.login = this.$refs.login;
        global.gui.register = this.$refs.register;
        global.gui.characterswitch = this.$refs.characterswitch;
        global.gui.charactercreator = this.$refs.charactercreator;
        global.gui.hud = this.$refs.hud;
        global.gui.menu = this.$refs.menu;
        global.gui.rpquiz = this.$refs.rpquiz;
        global.gui.inventory = this.$refs.inventory;
        global.gui.selectwheel = this.$refs.selectwheel;
        global.gui.speedometer = this.$refs.speedometer;
        global.gui.furniture = this.$refs.furniture;
        global.gui.bank = this.$refs.bank;
        global.gui.spediteur = this.$refs.spediteur;
        global.gui.shop = this.$refs.shop;
        global.gui.smartphone = this.$refs.smartphone;
        global.gui.progressbar = this.$refs.progressbar;
        global.gui.mdc = this.$refs.mdc;
    },
}
</script>

<style>
@import './assets/adminlte/dist/css/adminlte.css';
@import './assets/adminlte/plugins/fontawesome-free/css/all.css';

html,
body,
* {
    -webkit-user-select: none;
    -khtml-user-select: none;
    -moz-user-select: none;
    -o-user-select: none;
    user-select: none;
}

::-webkit-scrollbar-thumb {
    background-color: #a9a9a9;
}

::-webkit-scrollbar-track {
    background-color: transparent;
}

::-webkit-scrollbar-corner {
    background-color: transparent;
}

::-webkit-scrollbar {
    width: 0;
}

::-webkit-scrollbar {
    width: .1rem;
    height: .1rem;
}
</style>
