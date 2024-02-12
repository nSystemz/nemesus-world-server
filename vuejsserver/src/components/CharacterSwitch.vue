<template>
<div class="characterswitch" style="z-index: 1; overflow: hidden; background-color:transparent; scrollbar-width: none;" v-if="charactershow">
    <div class="centering2" style="height: 100%; background-color: rgba(69, 77, 85, .8);">
        <div class="row justify-content-center centering">
            <div class="col-md-12 animate__animated animate__backInDown">
                <div class="col-md-12">
                    <div class="box box-default">
                        <div class="card card-primary card-outline">
                            <div class="card-header" style="font-family: 'Exo', sans-serif;">Charakterauswahl
                                <button type="button" @click="createNewCharacter()" class="btn btn-primary float-right">Neuen
                                    Charakter
                                    erstellen</button>
                            </div>
                            <div class="card-body" style="min-width: 250px !important" v-if="count > 0">
                                <div class="row">
                                    <div class="col-md-3" v-for="(character, index) in characters" :key="index">
                                        <div class="card card-primary card-outline mr-3 ml-2" style="min-width: 250px !important">
                                            <div class="card-body box-profile" style="min-width: 250px !important">
                                                <div class="text-center">
                                                    <img class="cutimage profile-user-img img-fluid img-circle" v-bind:src="character.Screen">
                                                </div>
                                                <h3 class="profile-username text-center mt-3">
                                                    {{character.Name}}</h3>
                                                <p class="text-muted text-center">
                                                    {{character.Faction}}</p>
                                                <ul class="list-group list-group-bordered">
                                                    <li class="list-group-item">
                                                        <b>Geld</b> <a class="float-right"> {{character.Cash}}$</a>
                                                    </li>
                                                    <li class="list-group-item">
                                                        <b>Konto</b> <a class="float-right"> {{character.Bank}}$</a>
                                                    </li>
                                                    <li class="list-group-item">
                                                        <b>Job</b> <a class="float-right">{{character.Job}}</a>
                                                    </li>
                                                </ul>
                                            </div>
                                            <button @click="selectCharacter(character.ID)" type="button" class="btn btn-danger mr-3 ml-3 mb-2" disabled v-if="character.Closed == 1">Auswählen</button>
                                            <button @click="selectCharacter(character.ID)" type="button" class="btn btn-primary mr-3 ml-3 mb-2" v-if="index == 0" v-on:keyup.enter="selectCharacter(character.ID)">Auswählen</button>
                                            <button @click="selectCharacter(character.ID)" type="button" class="btn btn-primary mr-3 ml-3 mb-2" v-else>Auswählen</button>
                                            <button @click="deleteCharacter(character.ID)" type="button" class="btn btn-danger mr-3 ml-3 mb-2" v-if="character.Closed == 0">Löschen</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="card-body" style="min-width: 250px !important" v-else>
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="card card-primary card-outline mr-3 ml-2" style="min-width: 250px !important">
                                            <div class="card-body box-profile" style="min-width: 250px !important">
                                                <div class="text-center">
                                                    <img class="cutimage profile-user-img img-fluid img-circle" src="https://i.imgur.com/JjpH0qO.jpg">
                                                </div>
                                                <h3 class="profile-username text-center mt-3">
                                                    Max Mustermann</h3>
                                                <p class="text-muted text-center">
                                                    Keine Fraktion</p>
                                                <ul class="list-group list-group-bordered">
                                                    <li class="list-group-item">
                                                        <b>Geld</b> <a class="float-right"> 0$</a>
                                                    </li>
                                                    <li class="list-group-item">
                                                        <b>Konto</b> <a class="float-right"> 0$</a>
                                                    </li>
                                                    <li class="list-group-item">
                                                        <b>Job</b> <a class="float-right">Keinen</a>
                                                    </li>
                                                </ul>
                                            </div>
                                            <button type="button" class="btn btn-primary mr-2 ml-2 mb-2" disabled>Auswählen</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
</template>

<script>
import Swal from 'sweetalert2/dist/sweetalert2.js'

export default {
    name: 'CharactersWitch',
    data: function () {
        return {
            charactershow: false,
            characters: [],
            count: 0,
            clicked: (Date.now() / 1000)
        }
    },
    mounted() {
        document.body.classList.add("dark-mode");
    },
    methods: {
        hideCharacterSwitch: function () {
            this.charactershow = false;
            return;
        },
        showCharacterSwitch: function (characters, characterscount) {
            this.charactershow = !this.charactershow;
            if (characters != -1) {
                this.characters = JSON.parse(characters);
                this.count = characterscount;
            }
            return;
        },
        selectCharacter: function (characterid) {
            if ((Date.now() / 1000) > this.clicked) {
                this.clicked = (Date.now() / 1000) + (5);
                // eslint-disable-next-line no-undef
                mp.trigger('Client:SelectCharacter', characterid);
            }
        },
        deleteCharacter: function (characterid) {
            if ((Date.now() / 1000) > this.clicked) {
                Swal.fire({
                    title: 'Soll dieser Charakter wirklich gelöscht werden?',
                    showDenyButton: true,
                    showCancelButton: false,
                    confirmButtonText: 'Nein',
                    denyButtonText: `Ja`,
                }).then((result) => {
                    if (result.isDenied) {
                        this.clicked = (Date.now() / 1000) + (1);
                        // eslint-disable-next-line no-undef
                        mp.trigger('Client:DeleteCharacter', characterid);
                    }
                })
            }
        },
        createNewCharacter: function () {
            if ((Date.now() / 1000) > this.clicked) {
                if (this.count >= 3) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Fehler',
                        text: 'Du hast keine freien Charakterplätze mehr!',
                    })
                    return;
                }
                this.clicked = (Date.now() / 1000) + (5);
                //eslint-disable-next-line no-undef
                mp.trigger('Client:CreateNewCharacter');
            }
        },
        characterError: function () {
            Swal.fire({
                position: "center",
                icon: "error",
                title: "Ungültige Interaktion!",
                showConfirmButton: false,
                timer: 2500
            })
            return;
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

.centering {
    width: 100%;
    height: 60%;
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
    width: 225px !important;
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
</style>
