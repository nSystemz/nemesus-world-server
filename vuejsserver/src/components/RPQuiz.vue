<template>
<div class="rpquiz" style="z-index: 1; overflow-x: hidden; overflow-x: hidden; background-color:transparent; scrollbar-width: none;">
    <div v-if="rpquizshow" class="centering2" style="height: 100%; background-color: rgba(69, 77, 85, .8);">
        <div class="row justify-content-center centering">
            <div class="col-md-12 mt-1 animate__animated animate__backInDown">
                <div class="col-md-12 mt-1">
                    <div class="box box-default">
                        <div class="card card-primary card-outline">
                            <div class="card-header" style="font-family: 'Exo', sans-serif;">
                                <img src="../assets/images/logoklein.png" class="brand-image img-circle" style="opacity: .8;color:white;height:35px"><strong class="ml-2">Willkommen auf Nemesus
                                    World</strong>
                                <button type="button" @click="checkRadioButtons()" class="btn btn-primary float-right" v-if="!quizfinish">Antworten
                                    überprüfen</button>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-12 mt-1">
                                        <div style="display: flex; justify-content: center; align-items: center;">
                                            <h6>Willkommen auf <b style="color:#3F6791;font-family: 'Exo', sans-serif;">Nemesus World
                                                    (nWorld)</b>, nWorld ist ein Roleplayserver. D.h du schlüpfst in die
                                                Rolle eines Charakteres und spielst diesen aus. Um sicherzustellen das nur interessierte
                                                Spieler
                                                auf unserem Server spielen, müssen wir kurz deine Eignung zum Thema Roleplay und unserem Server prüfen.
                                                Solltest du weitere Informationen benötigen, findest du diese bei uns im Forum oder im Discord (https://nemesus-world.de). Bitte
                                                beantworte die unteren Fragen, du musst min. <b style="color:green;font-family: 'Exo', sans-serif;">70%</b> korrekt beantworten!</h6>
                                        </div>
                                        <b style="color:yellow">Du kennst unsere Regeln noch nicht? Dann schau doch mal auf
                                            https://regeln.nemesus-world
                                            vorbei, dort sind alle Regelungen einfach und verständlich aufgelistet!</b>
                                        <hr />
                                        <div class="setBox">
                                            <div v-for="(question, index) in questionArray" :key="index">
                                                <hr v-if="index!=0" />
                                                <strong class="mt-1">{{question.text}}</strong><br />
                                                <div class="form-check form-check-inline">
                                                    <input class="form-check-input" type="radio" v-model="radioButtons[index]" :name="question.text" :id="question.text" value="1">
                                                    <label class="form-check-label" for="inlineRadio1">{{question.answertext.split("|")[0]}}</label>
                                                </div>
                                                <br />
                                                <div class="form-check form-check-inline">
                                                    <input class="form-check-input" type="radio" v-model="radioButtons[index]" :name="question.text" :id="question.text" value="2">
                                                    <label class="form-check-label" for="inlineRadio2">{{question.answertext.split("|")[1]}}</label>
                                                </div>
                                                <br />
                                                <div class="form-check form-check-inline">
                                                    <input class="form-check-input" type="radio" v-model="radioButtons[index]" :name="question.text" :id="question.text" value="3">
                                                    <label class="form-check-label" for="inlineRadio3">{{question.answertext.split("|")[2]}}</label>
                                                </div>
                                                <br />
                                                <div class="form-check form-check-inline" v-if="index != 14">
                                                    <input class="form-check-input" type="radio" v-model="radioButtons[index]" :name="question.text" :id="question.text" value="4">
                                                    <label class="form-check-label" for="inlineRadio3">{{question.answertext.split("|")[3]}}</label>
                                                </div>
                                                <div class="form-check form-check-inline mb-5" v-else>
                                                    <input class="form-check-input" type="radio" v-model="radioButtons[index]" :name="question.text" :id="question.text" value="4">
                                                    <label class="form-check-label" for="inlineRadio3">{{question.answertext.split("|")[3]}}</label>
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
    </div>
    <div v-if="planequiz" class="centering2" style="height: 100%; background-color: rgba(69, 77, 85, .8);">
        <div class="row justify-content-center centering">
            <div class="col-md-12 mt-1 animate__animated animate__backInDown">
                <div class="col-md-12 mt-1">
                    <div class="box box-default">
                        <div class="card card-primary card-outline">
                            <div class="card-header" style="font-family: 'Exo', sans-serif;">
                                <img src="../assets/images/inventory/FSchein.png" class="brand-image img-circle" style="opacity: .8;color:white;height:35px"><strong class="ml-2">Flugscheinprüfung</strong>
                                <button type="button" @click="checkRadioButtons()" class="btn btn-primary float-right" v-if="!quizfinish">Antworten
                                    überprüfen</button>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-12 mt-1">
                                        <div style="display: flex; justify-content: center; align-items: center;">
                                            <h6>Willkommen bei der <b style="color:#3F6791;font-family: 'Exo', sans-serif;">Flugscheinprüfung</b>, du musst
                                                einen theoretischen sowie einen praktischen Teil absolvieren.
                                                Solltest du weitere Informationen benötigen, findest du diese hinten in unserer Infoecke (( Forum/Discord )).
                                                Für den theoretischen Teil, werde ich dir gleich ein paar Fragen stellen, versuche diese
                                                korrekt zu beantworten du musst mind. <b style="color:green;font-family: 'Exo', sans-serif;">70%</b> korrekt beantworten!</h6>
                                        </div>
                                        <b style="color:yellow">Oben bleiben ist nicht alles, heil runter kommen ist wichtiger!</b>
                                        <hr />
                                        <div class="setBox">
                                            <div v-for="(question, index) in questionArray" :key="index">
                                                <hr v-if="index!=0" />
                                                <strong class="mt-1">{{question.text}}</strong><br />
                                                <div class="form-check form-check-inline">
                                                    <input class="form-check-input" type="radio" v-model="radioButtons[index]" :name="question.text" :id="question.text" value="1">
                                                    <label class="form-check-label" for="inlineRadio1">{{question.answertext.split("|")[0]}}</label>
                                                </div>
                                                <br />
                                                <div class="form-check form-check-inline">
                                                    <input class="form-check-input" type="radio" v-model="radioButtons[index]" :name="question.text" :id="question.text" value="2">
                                                    <label class="form-check-label" for="inlineRadio2">{{question.answertext.split("|")[1]}}</label>
                                                </div>
                                                <br />
                                                <div class="form-check form-check-inline">
                                                    <input class="form-check-input" type="radio" v-model="radioButtons[index]" :name="question.text" :id="question.text" value="3">
                                                    <label class="form-check-label" for="inlineRadio3">{{question.answertext.split("|")[2]}}</label>
                                                </div>
                                                <br />
                                                <div class="form-check form-check-inline" v-if="index != 14">
                                                    <input class="form-check-input" type="radio" v-model="radioButtons[index]" :name="question.text" :id="question.text" value="4">
                                                    <label class="form-check-label" for="inlineRadio3">{{question.answertext.split("|")[3]}}</label>
                                                </div>
                                                <div class="form-check form-check-inline mb-5" v-else>
                                                    <input class="form-check-input" type="radio" v-model="radioButtons[index]" :name="question.text" :id="question.text" value="4">
                                                    <label class="form-check-label" for="inlineRadio3">{{question.answertext.split("|")[3]}}</label>
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
    </div>
    <div v-if="bootsquiz" class="centering2" style="height: 100%; background-color: rgba(69, 77, 85, .8);">
        <div class="row justify-content-center centering">
            <div class="col-md-12 mt-1 animate__animated animate__backInDown">
                <div class="col-md-12 mt-1">
                    <div class="box box-default">
                        <div class="card card-primary card-outline">
                            <div class="card-header" style="font-family: 'Exo', sans-serif;">
                                <img src="../assets/images/inventory/FSchein.png" class="brand-image img-circle" style="opacity: .8;color:white;height:35px"><strong class="ml-2">Bootsscheinprüfung</strong>
                                <button type="button" @click="checkRadioButtons()" class="btn btn-primary float-right" v-if="!quizfinish">Antworten
                                    überprüfen</button>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-12 mt-1">
                                        <div style="display: flex; justify-content: center; align-items: center;">
                                            <h6>Willkommen bei der <b style="color:#3F6791;font-family: 'Exo', sans-serif;">Bootsscheinprüfung</b>, du musst
                                                einen theoretischen sowie einen praktischen Teil absolvieren.
                                                Solltest du weitere Informationen benötigen, findest du diese hinten in unserer Infoecke (( Forum/Discord )).
                                                Für den theoretischen Teil, werde ich dir gleich ein paar Fragen stellen, versuche diese
                                                korrekt zu beantworten du musst mind. <b style="color:green;font-family: 'Exo', sans-serif;">70%</b> korrekt beantworten!</h6>
                                        </div>
                                        <b style="color:yellow">Mast- und Schotbruch!</b>
                                        <hr />
                                        <div class="setBox">
                                            <div v-for="(question, index) in questionArray" :key="index">
                                                <hr v-if="index!=0" />
                                                <strong class="mt-1">{{question.text}}</strong><br />
                                                <div class="form-check form-check-inline">
                                                    <input class="form-check-input" type="radio" v-model="radioButtons[index]" :name="question.text" :id="question.text" value="1">
                                                    <label class="form-check-label" for="inlineRadio1">{{question.answertext.split("|")[0]}}</label>
                                                </div>
                                                <br />
                                                <div class="form-check form-check-inline">
                                                    <input class="form-check-input" type="radio" v-model="radioButtons[index]" :name="question.text" :id="question.text" value="2">
                                                    <label class="form-check-label" for="inlineRadio2">{{question.answertext.split("|")[1]}}</label>
                                                </div>
                                                <br />
                                                <div class="form-check form-check-inline">
                                                    <input class="form-check-input" type="radio" v-model="radioButtons[index]" :name="question.text" :id="question.text" value="3">
                                                    <label class="form-check-label" for="inlineRadio3">{{question.answertext.split("|")[2]}}</label>
                                                </div>
                                                <br />
                                                <div class="form-check form-check-inline" v-if="index != 14">
                                                    <input class="form-check-input" type="radio" v-model="radioButtons[index]" :name="question.text" :id="question.text" value="4">
                                                    <label class="form-check-label" for="inlineRadio3">{{question.answertext.split("|")[3]}}</label>
                                                </div>
                                                <div class="form-check form-check-inline mb-5" v-else>
                                                    <input class="form-check-input" type="radio" v-model="radioButtons[index]" :name="question.text" :id="question.text" value="4">
                                                    <label class="form-check-label" for="inlineRadio3">{{question.answertext.split("|")[3]}}</label>
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
    </div>
    <div v-if="truckerquiz" class="centering2" style="height: 100%; background-color: rgba(69, 77, 85, .8);">
        <div class="row justify-content-center centering">
            <div class="col-md-12 mt-1 animate__animated animate__backInDown">
                <div class="col-md-12 mt-1">
                    <div class="box box-default">
                        <div class="card card-primary card-outline">
                            <div class="card-header" style="font-family: 'Exo', sans-serif;">
                                <img src="../assets/images/inventory/FSchein.png" class="brand-image img-circle" style="opacity: .8;color:white;height:35px"><strong class="ml-2">Truckerscheinprüfung</strong>
                                <button type="button" @click="checkRadioButtons()" class="btn btn-primary float-right" v-if="!quizfinish">Antworten
                                    überprüfen</button>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-12 mt-1">
                                        <div style="display: flex; justify-content: center; align-items: center;">
                                            <h6>Willkommen bei der <b style="color:#3F6791;font-family: 'Exo', sans-serif;">Truckerscheinprüfung</b>, du musst
                                                einen theoretischen sowie einen praktischen Teil absolvieren.
                                                Solltest du weitere Informationen benötigen, findest du diese hinten in unserer Infoecke (( Forum/Discord )).
                                                Für den theoretischen Teil, werde ich dir gleich ein paar Fragen stellen, versuche diese
                                                korrekt zu beantworten du musst mind. <b style="color:green;font-family: 'Exo', sans-serif;">70%</b> korrekt beantworten!</h6>
                                        </div>
                                        <b style="color:yellow">Wenn Du glaubst, alles unter Kontrolle zu haben, fährst Du zu langsam!</b>
                                        <hr />
                                        <div class="setBox">
                                            <div v-for="(question, index) in questionArray" :key="index">
                                                <hr v-if="index!=0" />
                                                <strong class="mt-1">{{question.text}}</strong><br />
                                                <div class="form-check form-check-inline">
                                                    <input class="form-check-input" type="radio" v-model="radioButtons[index]" :name="question.text" :id="question.text" value="1">
                                                    <label class="form-check-label" for="inlineRadio1">{{question.answertext.split("|")[0]}}</label>
                                                </div>
                                                <br />
                                                <div class="form-check form-check-inline">
                                                    <input class="form-check-input" type="radio" v-model="radioButtons[index]" :name="question.text" :id="question.text" value="2">
                                                    <label class="form-check-label" for="inlineRadio2">{{question.answertext.split("|")[1]}}</label>
                                                </div>
                                                <br />
                                                <div class="form-check form-check-inline">
                                                    <input class="form-check-input" type="radio" v-model="radioButtons[index]" :name="question.text" :id="question.text" value="3">
                                                    <label class="form-check-label" for="inlineRadio3">{{question.answertext.split("|")[2]}}</label>
                                                </div>
                                                <br />
                                                <div class="form-check form-check-inline" v-if="index != 14">
                                                    <input class="form-check-input" type="radio" v-model="radioButtons[index]" :name="question.text" :id="question.text" value="4">
                                                    <label class="form-check-label" for="inlineRadio3">{{question.answertext.split("|")[3]}}</label>
                                                </div>
                                                <div class="form-check form-check-inline mb-5" v-else>
                                                    <input class="form-check-input" type="radio" v-model="radioButtons[index]" :name="question.text" :id="question.text" value="4">
                                                    <label class="form-check-label" for="inlineRadio3">{{question.answertext.split("|")[3]}}</label>
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
    </div>
    <div v-if="bikequiz" class="centering2" style="height: 100%; background-color: rgba(69, 77, 85, .8);">
        <div class="row justify-content-center centering">
            <div class="col-md-12 mt-1 animate__animated animate__backInDown">
                <div class="col-md-12 mt-1">
                    <div class="box box-default">
                        <div class="card card-primary card-outline">
                            <div class="card-header" style="font-family: 'Exo', sans-serif;">
                                <img src="../assets/images/inventory/FSchein.png" class="brand-image img-circle" style="opacity: .8;color:white;height:35px"><strong class="ml-2">Motorradprüfung</strong>
                                <button type="button" @click="checkRadioButtons()" class="btn btn-primary float-right" v-if="!quizfinish">Antworten
                                    überprüfen</button>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-12 mt-1">
                                        <div style="display: flex; justify-content: center; align-items: center;">
                                            <h6>Willkommen bei der <b style="color:#3F6791;font-family: 'Exo', sans-serif;">Motorradscheinprüfung</b>, du musst
                                                einen theoretischen sowie einen praktischen Teil absolvieren.
                                                Solltest du weitere Informationen benötigen, findest du diese hinten in unserer Infoecke (( Forum/Discord )).
                                                Für den theoretischen Teil, werde ich dir gleich ein paar Fragen stellen, versuche diese
                                                korrekt zu beantworten du musst mind. <b style="color:green;font-family: 'Exo', sans-serif;">70%</b> korrekt beantworten!</h6>
                                        </div>
                                        <b style="color:yellow">Wenn Du glaubst, alles unter Kontrolle zu haben, fährst Du zu langsam!</b>
                                        <hr />
                                        <div class="setBox">
                                            <div v-for="(question, index) in questionArray" :key="index">
                                                <hr v-if="index!=0" />
                                                <strong class="mt-1">{{question.text}}</strong><br />
                                                <div class="form-check form-check-inline">
                                                    <input class="form-check-input" type="radio" v-model="radioButtons[index]" :name="question.text" :id="question.text" value="1">
                                                    <label class="form-check-label" for="inlineRadio1">{{question.answertext.split("|")[0]}}</label>
                                                </div>
                                                <br />
                                                <div class="form-check form-check-inline">
                                                    <input class="form-check-input" type="radio" v-model="radioButtons[index]" :name="question.text" :id="question.text" value="2">
                                                    <label class="form-check-label" for="inlineRadio2">{{question.answertext.split("|")[1]}}</label>
                                                </div>
                                                <br />
                                                <div class="form-check form-check-inline">
                                                    <input class="form-check-input" type="radio" v-model="radioButtons[index]" :name="question.text" :id="question.text" value="3">
                                                    <label class="form-check-label" for="inlineRadio3">{{question.answertext.split("|")[2]}}</label>
                                                </div>
                                                <br />
                                                <div class="form-check form-check-inline" v-if="index != 14">
                                                    <input class="form-check-input" type="radio" v-model="radioButtons[index]" :name="question.text" :id="question.text" value="4">
                                                    <label class="form-check-label" for="inlineRadio3">{{question.answertext.split("|")[3]}}</label>
                                                </div>
                                                <div class="form-check form-check-inline mb-5" v-else>
                                                    <input class="form-check-input" type="radio" v-model="radioButtons[index]" :name="question.text" :id="question.text" value="4">
                                                    <label class="form-check-label" for="inlineRadio3">{{question.answertext.split("|")[3]}}</label>
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
    </div>
    <div v-if="carquiz" class="centering2" style="height: 100%; background-color: rgba(69, 77, 85, .8);">
        <div class="row justify-content-center centering">
            <div class="col-md-12 mt-1 animate__animated animate__backInDown">
                <div class="col-md-12 mt-1">
                    <div class="box box-default">
                        <div class="card card-primary card-outline">
                            <div class="card-header" style="font-family: 'Exo', sans-serif;">
                                <img src="../assets/images/inventory/FSchein.png" class="brand-image img-circle" style="opacity: .8;color:white;height:35px"><strong class="ml-2">Führerscheinprüfung</strong>
                                <button type="button" @click="checkRadioButtons()" class="btn btn-primary float-right" v-if="!quizfinish">Antworten
                                    überprüfen</button>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-12 mt-1">
                                        <div style="display: flex; justify-content: center; align-items: center;">
                                            <h6>Willkommen bei der <b style="color:#3F6791;font-family: 'Exo', sans-serif;">Führerscheinprüfung</b>, du musst
                                                einen theoretischen sowie einen praktischen Teil absolvieren.
                                                Solltest du weitere Informationen benötigen, findest du diese hinten in unserer Infoecke (( Forum/Discord )).
                                                Für den theoretischen Teil, werde ich dir gleich ein paar Fragen stellen, versuche diese
                                                korrekt zu beantworten du musst mind. <b style="color:green;font-family: 'Exo', sans-serif;">70%</b> korrekt beantworten!</h6>
                                        </div>
                                        <b style="color:yellow">Drive safe someone loves you!</b>
                                        <hr />
                                        <div class="setBox">
                                            <div v-for="(question, index) in questionArray" :key="index">
                                                <hr v-if="index!=0" />
                                                <strong class="mt-1">{{question.text}}</strong><br />
                                                <div class="form-check form-check-inline">
                                                    <input class="form-check-input" type="radio" v-model="radioButtons[index]" :name="question.text" :id="question.text" value="1">
                                                    <label class="form-check-label" for="inlineRadio1">{{question.answertext.split("|")[0]}}</label>
                                                </div>
                                                <br />
                                                <div class="form-check form-check-inline">
                                                    <input class="form-check-input" type="radio" v-model="radioButtons[index]" :name="question.text" :id="question.text" value="2">
                                                    <label class="form-check-label" for="inlineRadio2">{{question.answertext.split("|")[1]}}</label>
                                                </div>
                                                <br />
                                                <div class="form-check form-check-inline">
                                                    <input class="form-check-input" type="radio" v-model="radioButtons[index]" :name="question.text" :id="question.text" value="3">
                                                    <label class="form-check-label" for="inlineRadio3">{{question.answertext.split("|")[2]}}</label>
                                                </div>
                                                <br />
                                                <div class="form-check form-check-inline" v-if="index != 14">
                                                    <input class="form-check-input" type="radio" v-model="radioButtons[index]" :name="question.text" :id="question.text" value="4">
                                                    <label class="form-check-label" for="inlineRadio3">{{question.answertext.split("|")[3]}}</label>
                                                </div>
                                                <div class="form-check form-check-inline mb-5" v-else>
                                                    <input class="form-check-input" type="radio" v-model="radioButtons[index]" :name="question.text" :id="question.text" value="4">
                                                    <label class="form-check-label" for="inlineRadio3">{{question.answertext.split("|")[3]}}</label>
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
    </div>
    <div v-if="ammuquiz" class="centering2" style="height: 100%; background-color: rgba(69, 77, 85, .8);">
        <div class="row justify-content-center centering">
            <div class="col-md-12 mt-1 animate__animated animate__backInDown">
                <div class="col-md-12 mt-1">
                    <div class="box box-default">
                        <div class="card card-primary card-outline">
                            <div class="card-header" style="font-family: 'Exo', sans-serif;">
                                <img src="../assets/images/inventory/Pistole.png" class="brand-image img-circle" style="opacity: .8;color:white;height:35px"><strong class="ml-2">Waffenscheinprüfung</strong>
                                <button type="button" @click="checkRadioButtons()" class="btn btn-primary float-right" v-if="!quizfinish">Antworten
                                    überprüfen</button>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-12 mt-1">
                                        <div style="display: flex; justify-content: center; align-items: center;">
                                            <h6>Willkommen bei der <b style="color:#3F6791;font-family: 'Exo', sans-serif;">Waffenscheinprüfung</b>, du musst
                                                einen theoretischen sowie einen praktischen Teil absolvieren.
                                                Solltest du weitere Informationen benötigen, findest du diese hinten in unserer Infoecke (( Forum/Discord )).
                                                Für den theoretischen Teil, werde ich dir gleich ein paar Fragen stellen, versuche diese
                                                korrekt zu beantworten du musst mind. <b style="color:green;font-family: 'Exo', sans-serif;">70%</b> korrekt beantworten!</h6>
                                        </div>
                                        <b style="color:yellow">Nicht Waffen töten Menschen, sondern Menschen!</b>
                                        <hr />
                                        <div class="setBox">
                                            <div v-for="(question, index) in questionArray" :key="index">
                                                <hr v-if="index!=0" />
                                                <strong class="mt-1">{{question.text}}</strong><br />
                                                <div class="form-check form-check-inline">
                                                    <input class="form-check-input" type="radio" v-model="radioButtons[index]" :name="question.text" :id="question.text" value="1">
                                                    <label class="form-check-label" for="inlineRadio1">{{question.answertext.split("|")[0]}}</label>
                                                </div>
                                                <br />
                                                <div class="form-check form-check-inline">
                                                    <input class="form-check-input" type="radio" v-model="radioButtons[index]" :name="question.text" :id="question.text" value="2">
                                                    <label class="form-check-label" for="inlineRadio2">{{question.answertext.split("|")[1]}}</label>
                                                </div>
                                                <br />
                                                <div class="form-check form-check-inline">
                                                    <input class="form-check-input" type="radio" v-model="radioButtons[index]" :name="question.text" :id="question.text" value="3">
                                                    <label class="form-check-label" for="inlineRadio3">{{question.answertext.split("|")[2]}}</label>
                                                </div>
                                                <br />
                                                <div class="form-check form-check-inline" v-if="index != 14">
                                                    <input class="form-check-input" type="radio" v-model="radioButtons[index]" :name="question.text" :id="question.text" value="4">
                                                    <label class="form-check-label" for="inlineRadio3">{{question.answertext.split("|")[3]}}</label>
                                                </div>
                                                <div class="form-check form-check-inline mb-5" v-else>
                                                    <input class="form-check-input" type="radio" v-model="radioButtons[index]" :name="question.text" :id="question.text" value="4">
                                                    <label class="form-check-label" for="inlineRadio3">{{question.answertext.split("|")[3]}}</label>
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
    </div>
</div>
</template>

<script>
import Swal from 'sweetalert2/dist/sweetalert2.js'
import questionList from './helper/externals/rpquestions.js'
import questionList2 from './helper/externals/weaponquestions.js'
import questionList3 from './helper/externals/carquestions.js'
import questionList4 from './helper/externals/bikequestions.js'
import questionList5 from './helper/externals/truckerquestions.js'
import questionList6 from './helper/externals/boatsquestions.js'
import questionList7 from './helper/externals/planequestions.js'

export default {
    name: 'RPQuiz',
    data: function () {
        return {
            rpquizshow: false,
            ammuquiz: false,
            carquiz: false,
            bikequiz: false,
            bootsquiz: false,
            planequiz: false,
            truckerquiz: false,
            quizfinish: false,
            questionArray: [],
            radioButtons: [15]
        }
    },
    mounted() {
        document.body.classList.add("dark-mode");
    },
    methods: {
        startRPQuiz: function () {
            this.quizfinish = false;
            this.radioButtons = [15];
            this.rpquizshow = true;
            this.questionArray = questionList.map((value) => ({
                    value,
                    sort: Math.random()
                }))
                .sort((a, b) => a.sort - b.sort)
                .map(({
                    value
                }) => value).slice(0, 12);
        },
        stopRPQuiz: function () {
            this.rpquizshow = false;
        },
        startAmmuQuiz: function () {
            this.quizfinish = false;
            this.radioButtons = [10];
            this.ammuquiz = true;
            this.questionArray = questionList2.map((value) => ({
                    value,
                    sort: Math.random()
                }))
                .sort((a, b) => a.sort - b.sort)
                .map(({
                    value
                }) => value).slice(0, 10);
        },
        stopAmmuQuiz: function () {
            this.ammuquiz = false;
        },
        startCarQuiz: function () {
            this.quizfinish = false;
            this.radioButtons = [10];
            this.carquiz = true;
            this.questionArray = questionList3.map((value) => ({
                    value,
                    sort: Math.random()
                }))
                .sort((a, b) => a.sort - b.sort)
                .map(({
                    value
                }) => value).slice(0, 10);
        },
        startBikeQuiz: function () {
            this.quizfinish = false;
            this.radioButtons = [10];
            this.bikequiz = true;
            this.questionArray = questionList4.map((value) => ({
                    value,
                    sort: Math.random()
                }))
                .sort((a, b) => a.sort - b.sort)
                .map(({
                    value
                }) => value).slice(0, 10);
        },
        startTruckerQuiz: function () {
            this.quizfinish = false;
            this.radioButtons = [10];
            this.truckerquiz = true;
            this.questionArray = questionList5.map((value) => ({
                    value,
                    sort: Math.random()
                }))
                .sort((a, b) => a.sort - b.sort)
                .map(({
                    value
                }) => value).slice(0, 10);
        },
        startBootsQuiz: function () {
            this.quizfinish = false;
            this.radioButtons = [10];
            this.bootsquiz = true;
            this.questionArray = questionList6.map((value) => ({
                    value,
                    sort: Math.random()
                }))
                .sort((a, b) => a.sort - b.sort)
                .map(({
                    value
                }) => value).slice(0, 10);
        },
        startPlaneQuiz: function () {
            this.quizfinish = false;
            this.radioButtons = [10];
            this.planequiz = true;
            this.questionArray = questionList7.map((value) => ({
                    value,
                    sort: Math.random()
                }))
                .sort((a, b) => a.sort - b.sort)
                .map(({
                    value
                }) => value).slice(0, 10);
        },
        stopCarQuiz: function (id) {
            if (id == 1) {
                this.carquiz = false;
            } else if (id == 2) {
                this.bikequiz = false;
            } else if (id == 3) {
                this.truckerquiz = false;
            } else if (id == 4) {
                this.bootsquiz = false;
            } else if (id == 5) {
                this.planequiz = false;
            }
        },
        checkRadioButtons: function () {
            var error = 0;
            var count = 0;
            for (var i = 0; i < this.radioButtons.length; i++) {
                if (this.radioButtons[i])
                    if (this.questionArray[i].answer != this.radioButtons[i]) {
                        error++;
                    }
                count++;
            }
            if (count < this.questionArray.length) {
                Swal.fire({
                    icon: 'error',
                    title: 'Fehler',
                    text: 'Du musst zuerst alle Fragen beantworten!',
                    showConfirmButton: false,
                    timer: 2500
                })
                return;
            }
            if (error > (this.questionArray.length / 100 * 70)) {
                Swal.fire({
                    icon: 'error',
                    title: 'Fehler',
                    text: 'Du hast zuviele Fragen falsch beantwortet!',
                    showConfirmButton: false,
                    timer: 2500
                })
                if (this.ammuquiz == true) {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:StopAmmuQuiz');
                    return;
                } else if (this.carquiz == true) {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:StopCarQuiz', 1);
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:StartCar', -1);
                    return;
                } else if (this.bikequiz == true) {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:StopCarQuiz', 2);
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:StartCar', -1);
                    return;
                } else if (this.truckerquiz == true) {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:StopCarQuiz', 3);
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:StartCar', -1);
                    return;
                } else if (this.bootsquiz == true) {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:StopCarQuiz', 4);
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:StartCar', -1);
                    return;
                } else if (this.planequiz == true) {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:StopCarQuiz', 5);
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:StartCar', -1);
                    return;
                }
                return;
            }
            // eslint-disable-next-line no-undef
            mp.trigger('Client:PlaySoundSuccess');
            if (this.ammuquiz == true) {
                Swal.fire({
                    title: 'Geschafft!',
                    text: "Du hast mehr als 70% der Fragen richtig beantwortet, Glückwunsch! Jetzt folgt die praktische Prüfung, viel Erfolg!",
                    icon: 'success',
                    showCancelButton: false,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Weiter'
                }).then((result) => {
                    if (result.isConfirmed) {
                        // eslint-disable-next-line no-undef
                        mp.trigger('Client:StopAmmuQuiz');
                        // eslint-disable-next-line no-undef
                        mp.trigger('Client:StartRange', 25, 1, 0);
                    } else {
                        // eslint-disable-next-line no-undef
                        mp.trigger('Client:StopAmmuQuiz');
                        // eslint-disable-next-line no-undef
                        mp.trigger('Client:StartRange', 25, 1, 0);
                    }
                })
            } else if (this.carquiz == true) {
                Swal.fire({
                    title: 'Geschafft!',
                    text: "Du hast mehr als 70% der Fragen richtig beantwortet, Glückwunsch! Jetzt folgt die praktische Prüfung, viel Erfolg!",
                    icon: 'success',
                    showCancelButton: false,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Weiter'
                }).then((result) => {
                    if (result.isConfirmed) {
                        // eslint-disable-next-line no-undef
                        mp.trigger('Client:StopCarQuiz', 1);
                        // eslint-disable-next-line no-undef
                        mp.trigger('Client:StartCar', 1);
                    } else {
                        // eslint-disable-next-line no-undef
                        mp.trigger('Client:StopCarQuiz', 1);
                        // eslint-disable-next-line no-undef
                        mp.trigger('Client:StartCar', 1);
                    }
                })
            } else if (this.bikequiz == true) {
                Swal.fire({
                    title: 'Geschafft!',
                    text: "Du hast mehr als 70% der Fragen richtig beantwortet, Glückwunsch! Jetzt folgt die praktische Prüfung, viel Erfolg!",
                    icon: 'success',
                    showCancelButton: false,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Weiter'
                }).then((result) => {
                    if (result.isConfirmed) {
                        // eslint-disable-next-line no-undef
                        mp.trigger('Client:StopCarQuiz', 2);
                        // eslint-disable-next-line no-undef
                        mp.trigger('Client:StartCar', 2);
                    } else {
                        // eslint-disable-next-line no-undef
                        mp.trigger('Client:StopCarQuiz', 2);
                        // eslint-disable-next-line no-undef
                        mp.trigger('Client:StartCar', 2);
                    }
                })
            } else if (this.truckerquiz == true) {
                Swal.fire({
                    title: 'Geschafft!',
                    text: "Du hast mehr als 70% der Fragen richtig beantwortet, Glückwunsch! Jetzt folgt die praktische Prüfung, viel Erfolg!",
                    icon: 'success',
                    showCancelButton: false,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Weiter'
                }).then((result) => {
                    if (result.isConfirmed) {
                        // eslint-disable-next-line no-undef
                        mp.trigger('Client:StopCarQuiz', 3);
                        // eslint-disable-next-line no-undef
                        mp.trigger('Client:StartCar', 3);
                    } else {
                        // eslint-disable-next-line no-undef
                        mp.trigger('Client:StopCarQuiz', 3);
                        // eslint-disable-next-line no-undef
                        mp.trigger('Client:StartCar', 3);
                    }
                })
            } else if (this.bootsquiz == true) {
                Swal.fire({
                    title: 'Geschafft!',
                    text: "Du hast mehr als 70% der Fragen richtig beantwortet, Glückwunsch! Jetzt folgt die praktische Prüfung, viel Erfolg!",
                    icon: 'success',
                    showCancelButton: false,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Weiter'
                }).then((result) => {
                    if (result.isConfirmed) {
                        // eslint-disable-next-line no-undef
                        mp.trigger('Client:StopCarQuiz', 4);
                        // eslint-disable-next-line no-undef
                        mp.trigger('Client:StartCar', 4);
                    } else {
                        // eslint-disable-next-line no-undef
                        mp.trigger('Client:StopCarQuiz', 4);
                        // eslint-disable-next-line no-undef
                        mp.trigger('Client:StartCar', 4);
                    }
                })
            } else if (this.planequiz == true) {
                Swal.fire({
                    title: 'Geschafft!',
                    text: "Du hast mehr als 70% der Fragen richtig beantwortet, Glückwunsch! Jetzt folgt die praktische Prüfung, viel Erfolg!",
                    icon: 'success',
                    showCancelButton: false,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Weiter'
                }).then((result) => {
                    if (result.isConfirmed) {
                        // eslint-disable-next-line no-undef
                        mp.trigger('Client:StopCarQuiz', 5);
                        // eslint-disable-next-line no-undef
                        mp.trigger('Client:StartCar', 5);
                    } else {
                        // eslint-disable-next-line no-undef
                        mp.trigger('Client:StopCarQuiz', 5);
                        // eslint-disable-next-line no-undef
                        mp.trigger('Client:StartCar', 5);
                    }
                })
            } else {
                Swal.fire({
                    title: 'Geschafft!',
                    text: "Du hast mehr als 70% der Fragen richtig beantwortet, Glückwunsch! Als nächstes kannst du deinen Charakter erstellen, viel Erfolg!",
                    icon: 'success',
                    showCancelButton: false,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Weiter'
                }).then((result) => {
                    if (result.isConfirmed) {
                        this.quizfinish = true;
                        // eslint-disable-next-line no-undef
                        mp.trigger('Client:RPQuizFinish', error);
                    } else {
                        this.quizfinish = true;
                        // eslint-disable-next-line no-undef
                        mp.trigger('Client:RPQuizFinish', error);
                    }
                })
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

input[type="radio"] {
    -webkit-appearance: none;
    -moz-appearance: none;
    appearance: none;
    display: inline-block;
    width: 15px;
    height: 15px;
    padding: 2px;
    background-clip: content-box;
    border: 2px solid #bbbbbb;
    background-color: #e7e6e7;
    border-radius: 50%;
}

input[type="radio"]:checked {
    background-color: #3F6791;
}

.centering {
    margin: 0;
    position: absolute;
    top: 50%;
    left: 50%;
    margin-right: -50%;
    transform: translate(-50%, -50%)
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

.setBox {
    height: 62vh;
    overflow-x: auto;
    scrollbar-width: none;
    margin-bottom: 1vh
}

@media only screen and (max-Width: 800px) {
    .setBox {
        height: 49vh;
        overflow-x: auto;
        scrollbar-width: none;
        margin-bottom: 1vh
    }
}
</style>
