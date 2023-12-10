<template>
<div class="registershow" style="overflow: hidden" v-if="registershow">
    <div class="limiter" style="background-image: url('https://nemesus-world.de/images/loginBackground.jpg'); background-repeat: no-repeat; background-size: cover;">
        <div class="container-login100">
            <div class="wrap-login100" style="margin-top: 15vh">
                <div class="login100-form-avatar">
                    <img src="../assets/images/logo.png" alt="Logo">
                </div>
                <div class="wrap-input100 m-b-10 mt-1">
                    <div class="alert alert-danger text-center" role="alert" style="border-radius: 25px;" v-if="warning">
                        <strong>Info: {{warning}}</strong>
                    </div>
                    <div class="wrap-input100 mt-2">
                        <input class="input100" type="text" id="name" name="name" v-model="username" placeholder="Benutzername" maxlength="35" autocomplete="off" v-on:keyup.enter="onEnter" autofocus>
                        <span class="focus-input100"></span>
                        <span class="symbol-input100">
                            <i class="fa fa-user"></i>
                        </span>
                    </div>
                </div>
                <div class="wrap-input100 m-b-10">
                    <input class="input100" type="password" name="password" id="password" v-model="password" placeholder="Passwort" maxlength="35" autocomplete="off" v-on:keyup.enter="onEnter">
                    <span class="focus-input100"></span>
                    <span class="symbol-input100">
                        <i class="fa fa-lock"></i>
                    </span>
                </div>
                <div class="wrap-input100 m-b-10">
                    <input class="input100" type="password" name="password" id="password" v-model="password2" placeholder="Passwort wiederholen" maxlength="35" autocomplete="off" v-on:keyup.enter="onEnter">
                    <span class="focus-input100"></span>
                    <span class="symbol-input100">
                        <i class="fa fa-lock"></i>
                    </span>
                </div>
                <div class="container-login100-form-btn p-t-10" v-if="!clicked">
                    <button type="submit" class="login100-form-btn" @click="register()">
                        Weiter
                    </button>
                </div>
                <div class="container-login100-form-btn p-t-10" v-else>
                    <button type="submit" disabled class="login100-form-btn" @click="register()">
                        Weiter
                    </button>
                </div>
                <div class="text-center w-full p-t-25 p-b-5">
                    <a href="#" class="txt1" @click="showLogin()">
                        Zum Login springen
                    </a>
                </div>
                <div class="text-center w-full p-t-25 p-b-180">
                    <a href="#" class="txt1">
                        Der Gamemode wurde von <strong>Nemesus.de</strong> erstellt!
                    </a>
                </div>
            </div>
        </div>
    </div>
</div>
</template>

<script>
export default {
    name: 'RegisterWindow',
    data: function () {
        return {
            registershow: false,
            username: '',
            password: '',
            password2: '',
            warning: '',
            clicked: false
        }
    },
    methods: {
        onEnter: function () {
            this.register();
        },
        register: function () {
            // eslint-disable-next-line no-undef
            if (this.password.length < 6 || this.username.length < 3) {
                this.warning = 'Name oder Passwort zu kurz!';
                return;
            }
            if (this.password.length > 35 || this.username.length > 35) {
                this.warning = 'Name oder Passwort zu lang!';
                return;
            }
            if (this.password === this.password) {
                // eslint-disable-next-line no-undef
                mp.trigger('Client:AccountRegister', this.username, this.password);
                return;
            } else {
                this.warning = 'Die Passwörter stimmen nicht überein!';
                return;
            }
        },
        showRegister: function () {
            this.registershow = !this.registershow;
            return;
        },
        showLogin: function () {
            this.registershow = !this.registershow;
            mp.trigger('Client:ShowLogin');
            return;
        },
        setWarning: function (text) {
            this.warning = text;
            this.clicked = false;
            return;
        }
    }
}
</script>

<style scoped>
@import '../assets/css/bootstrap.min.css';
@import '../assets/css/main.css';
@import '../assets/css/util.css';
@import '../assets/css/font-awesome.min.css';

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
</style>
