<template>
<div class="loginshow" style="overflow: hidden" v-if="loginshow">
    <div class="limiter" style="background-image: url('https://nemesus-world.de/images/loginBackground.jpg'); background-repeat: no-repeat; background-size: cover;">
        <div class="container-login100">
            <div class="wrap-login100" style="margin-top: 18vh">
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
                <div class="container-login100-form-btn p-t-10" v-if="!clicked">
                    <button type="submit" class="login100-form-btn" @click="login()">
                        Login
                    </button>
                </div>
                <div class="container-login100-form-btn p-t-10" v-else>
                    <button type="submit" disabled class="login100-form-btn" @click="login()">
                        Login
                    </button>
                </div>
                <div class="text-center w-full p-t-25 p-b-185">
                    <a href="#" class="txt1" @click="showRegister()">
                        Zur Registrierung springen
                    </a>
                </div>
            </div>
        </div>
    </div>
</div>
</template>

<script>
export default {
    name: 'LoginWindow',
    data: function () {
        return {
            loginshow: false,
            username: '',
            password: '',
            warning: '',
            clicked: false
        }
    },
    methods: {
        onEnter: function () {
            this.login();
        },
        login: function () {
            if (this.password.length < 6 || this.username.length < 3) {
                this.warning = 'Name oder Passwort zu kurz!';
                return;
            }
            if (this.password.length > 35 || this.username.length > 35) {
                this.warning = 'Name oder Passwort zu kurz!';
                return;
            }
            // eslint-disable-next-line no-undef
            mp.trigger('Client:AccountLogin', this.username, this.password);
            this.clicked = true;
        },
        showLogin: function () {
            this.loginshow = !this.loginshow;
            return;
        },
        showRegister: function () {
            this.loginshow = !this.loginshow;
            mp.trigger('Client:ShowRegister');
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
