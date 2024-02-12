<template>
<div class="bank" style="z-index: 1; overflow-x: auto; background-color:transparent; scrollbar-width: none;" v-if="bankshow">
    <div class="centering2" style="height: 100%; background-color:transparent;">
        <div class="row justify-content-center centering">
            <div class="col-md-12 mt-1 animate__animated animate__bounceInUp">
                <div class="col-md-12 mt-1">
                    <div class="box box-default">
                        <div class="card card-primary card-outline">
                            <div v-if="atbank==0" class="card-header" style="font-family: 'Exo', sans-serif">Maze Bank - Kontoverwaltung <div class="float-right" style="font-family: 'Exo', sans-serif" @click="newPin()"><b style="cursor:pointer">Neuer Pin</b></div>
                            </div>
                            <div v-if="atbank==1" class="card-header" style="font-family: 'Exo', sans-serif">Fleeca Bank - Kontoverwaltung <div class="float-right" style="font-family: 'Exo', sans-serif" @click="newPin()"><b style="cursor:pointer">Neuer Pin</b></div>
                            </div>
                            <div v-if="atbank==2" class="card-header" style="font-family: 'Exo', sans-serif">Bankautomat - Kontoverwaltung <div class="float-right" style="font-family: 'Exo', sans-serif" @click="newPin()"><b style="cursor:pointer">Neuer Pin</b></div>
                            </div>
                            <div class="card-body" style="max-height:75vh; overflow-x: auto;">
                                <div style="display: flex; justify-content: center; align-items: center;">
                                    <img src="../assets/images/maze.png" style="width: 20%;text-shadow: 0 0 2px #000" class="mb-4" v-if="atbank==0||atbank==2" />
                                    <img src="../assets/images/fleeca.png" style="width: 16%;text-shadow: 0 0 2px #000" class="mb-4" v-else />
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-12">
                                            <div class="card card-primary card-outline">
                                                <div class="card-header p-2">
                                                    <ul class="nav nav-pills">
                                                        <li v-if="konten.length > 0" class="nav-item"><a class="nav-link" data-toggle="tab" v-on:click="selectMenu(1)" :class="[(showbanksetting == 1) ? 'active':'']">Informationen</a>
                                                        </li>
                                                        <li v-if="konten.length > 0 && selectedKonto.number" class="nav-item"><a class="nav-link" data-toggle="tab" v-on:click="selectMenu(2)" :class="[(showbanksetting == 2) ? 'active':'']">Geld
                                                                abheben</a>
                                                        </li>
                                                        <li v-if="konten.length > 0 && selectedKonto.number" class="nav-item"><a class="nav-link" data-toggle="tab" v-on:click="selectMenu(3)" :class="[(showbanksetting == 3) ? 'active':'']">Geld
                                                                einzahlen</a></li>
                                                        <li v-if="konten.length > 0 && selectedKonto.number && atbank != 2" class="nav-item"><a class="nav-link" data-toggle="tab" v-on:click="selectMenu(4)" :class="[(showbanksetting == 4) ? 'active':'']">Überweisung</a>
                                                        </li>
                                                        <li v-if="konten.length > 0 && selectedKonto.number && atbank != 2" class="nav-item"><a class="nav-link" data-toggle="tab" v-on:click="selectMenu(5)" :class="[(showbanksetting == 5) ? 'active':'']">Daueraufträge</a>
                                                        </li>
                                                        <li v-if="konten.length > 0 && selectedKonto.number && atbank != 2" class="nav-item"><a class="nav-link" data-toggle="tab" v-on:click="selectMenu(6)" :class="[(showbanksetting == 6) ? 'active':'']">Kontoauszug</a>
                                                        </li>
                                                        <li v-if="konten.length > 0 && selectedKonto.number && atbank != 2" class="nav-item"><a class="nav-link" data-toggle="tab" v-on:click="selectMenu(7)" :class="[(showbanksetting == 7) ? 'active':'']">Einstellungen</a>
                                                        </li>
                                                        <li v-if="atbank != 2" class="nav-item"><a class="nav-link" data-toggle="tab" v-on:click="selectMenu(0)" :class="[(showbanksetting == 0) ? 'active':'']">Neues Konto</a></li>
                                                    </ul>
                                                </div>
                                                <div class="card-body">
                                                    <div class="tab-content">
                                                        <div class="active tab-pane text-center" v-if="showbanksetting == 0">
                                                            <h5>Sind Sie an einem neuen Konto bei unserer Bank interessiert?</h5>
                                                            <hr />
                                                            <h6 style="font-family: 'Exo', sans-serif;text-shadow: 0 0 2px #000;">
                                                                <ul v-if="atbank == 0" class="mt-3"><u>Unsere Vorteile im Überblick:</u>
                                                                    <li>Kostenfreies Konto</li>
                                                                    <li>Günstige Partnerkarten</li>
                                                                    <li>0.25% Verzinsung Ihres Guthaben</li>
                                                                    <li>3% Abhebungsgebühr bei einer anderen Bank</li>
                                                                    <li>Kostenfreies Onlinebanking</li>
                                                                    <li>Kostenfreie ATMs</li>
                                                                    <li>Sichere Aufbewahrung Ihres Geldes</li>
                                                                </ul>
                                                                <ul v-else class="mt-3"><u>Unsere Vorteile im Überblick:</u>
                                                                    <li>Kostenfreies Konto</li>
                                                                    <li>Günstige Partnerkarten</li>
                                                                    <li>0.45% Verzinsung Ihres Guthaben</li>
                                                                    <li>5% Abhebungsgebühr bei einer anderen Bank</li>
                                                                    <li>Kostenfreies Onlinebanking</li>
                                                                    <li>Kostenpflichtige ATM Benutzung</li>
                                                                    <li>Sichere Aufbewahrung Ihres Geldes</li>
                                                                </ul>
                                                            </h6>
                                                            <button type="submit" class="btn btn-block btn-success animate__animated animate__heartBeat" v-on:click="newBank()">Neues
                                                                Konto eröffnen</button>
                                                        </div>
                                                        <div class="active tab-pane" v-if="showbanksetting == 1">
                                                            <div class="form-group">
                                                                <label>Kontoauswahl:</label>
                                                                <div v-for="(konto, index) in konten" :key="index">
                                                                    <button v-on:click="selectBank(konto)" v-if="selectedKonto.number==konto.banknumber" type="submit" class="btn btn-block btn-primary mb-3">{{konto.banknumber}}
                                                                        -
                                                                        {{konto.bankvalue}}$ <span v-if="defaultKonto == konto.banknumber" class="badge badge-primary">Standardkonto</span></button>
                                                                    <button v-on:click="selectBank(konto)" v-if="selectedKonto.number!=konto.banknumber" type="submit" class="btn btn-block btn-secondary mb-3">{{konto.banknumber}}
                                                                        -
                                                                        {{konto.bankvalue}}$ <span v-if="defaultKonto == konto.banknumber" class="badge badge-primary">Standardkonto</span></button>
                                                                </div>
                                                                <hr v-if="selectedKonto.number" />
                                                                <h3 v-if="selectedKonto.number" class="text-center" style="font-family: 'Exo', sans-serif;text-shadow: 0 0 2px #000;">
                                                                    Guthaben: <span style="color:green" v-if="selectedKonto.value >= 0">{{selectedKonto.value}}$</span><span style="color:red" v-else>{{selectedKonto.value}}$</span></h3>
                                                                <h3 v-if="!selectedKonto.number" class="text-center" style="font-family: 'Exo', sans-serif;text-shadow: 0 0 2px #000;">
                                                                    Bitte ein Konto auswählen!</h3>
                                                            </div>
                                                        </div>
                                                        <div class="active tab-pane" v-if="showbanksetting == 2">
                                                            <div class="card-body box-profile text-center">
                                                                <h3 v-if="selectedKonto.number && inputpinbool == true" style="font-family: 'Exo', sans-serif;text-shadow: 0 0 2px #000;">
                                                                    Guthaben: <span style="color:green" v-if="selectedKonto.value >= 0">{{selectedKonto.value}}$</span><span style="color:red" v-else>{{selectedKonto.value}}$</span></h3>
                                                                <h3 v-if="!selectedKonto.number" class="text-center" style="font-family: 'Exo', sans-serif;text-shadow: 0 0 2px #000;">
                                                                    Bitte ein Konto auswählen!</h3>
                                                                <hr v-if="selectedKonto.number && inputpinbool == true" />
                                                                <div v-if="selectedKonto.number && inputpinbool == false" style="display: flex; justify-content: center; align-items: center;">
                                                                    <input v-on:keyup.enter="checkPin()" type="password" class="form-control text-center" v-model="inputpin" placeholder="Pin" maxlength="4" autocomplete="off" style="width: 15vw">
                                                                </div>
                                                                <div v-if="selectedKonto.number && inputpinbool == false" style="display: flex; justify-content: center; align-items: center;">
                                                                    <button type="submit" class="btn btn-block btn-primary mt-3" style="width: 15vw" @click="checkPin()">Eingeben</button>
                                                                    <hr v-if="selectedKonto.number && inputpinbool == true" />
                                                                </div>
                                                            </div>
                                                            <div class="mb-3" style="display: flex; justify-content: center; align-items: center;" v-if="selectedKonto.number && inputpinbool == true">
                                                                <button type="button" @click="withdraw(5)" class="btn btn-primary">5$</button>
                                                                <button type="button" @click="withdraw(10)" class="btn btn-primary ml-2">10$</button>
                                                                <button type="button" @click="withdraw(20)" class="btn btn-primary ml-2">20$</button>
                                                                <button type="button" @click="withdraw(50)" class="btn btn-primary ml-2">50$</button>
                                                                <button type="button" @click="withdraw(100)" class="btn btn-primary ml-2">100$</button>
                                                                <button type="button" @click="withdraw(500)" class="btn btn-primary ml-2">500$</button>
                                                                <button type="button" @click="withdraw(1000)" class="btn btn-primary ml-2">1000$</button>
                                                                <button type="button" @click="withdraw(5000)" class="btn btn-primary ml-2">5000$</button>
                                                                <button type="button" @click="withdraw(10000)" class="btn btn-primary ml-2">10000$</button>
                                                            </div>
                                                            <div style="display: flex; justify-content: center; align-items: center;">
                                                                <input type="text" class="form-control text-center" v-model="withdrawvalue" placeholder="500$" maxlength="9" autocomplete="off" v-if="selectedKonto.number && inputpinbool == true" style="width: 15vw">
                                                            </div>
                                                            <button type="submit" class="btn btn-block btn-primary mt-3" v-if="selectedKonto.number && inputpinbool == true" @click="withdraw(withdrawvalue)">Geld abheben</button>
                                                        </div>
                                                        <div class="active tab-pane" v-if="showbanksetting == 3">
                                                            <div class="card-body box-profile text-center">
                                                                <h3 v-if="selectedKonto.number" style="font-family: 'Exo', sans-serif;text-shadow: 0 0 2px #000;">
                                                                    Guthaben: <span style="color:green" v-if="selectedKonto.value >= 0">{{selectedKonto.value}}$</span><span style="color:red" v-else>{{selectedKonto.value}}$</span></h3>
                                                                <h3 v-else class="text-center" style="font-family: 'Exo', sans-serif;text-shadow: 0 0 2px #000;">
                                                                    Bitte ein Konto auswählen!</h3>
                                                                <hr v-if="selectedKonto.number" />
                                                            </div>
                                                            <div class="mb-3" style="display: flex; justify-content: center; align-items: center;" v-if="selectedKonto.number">
                                                                <button type="button" @click="deposit(5)" class="btn btn-primary">5$</button>
                                                                <button type="button" @click="deposit(10)" class="btn btn-primary ml-2">10$</button>
                                                                <button type="button" @click="deposit(20)" class="btn btn-primary ml-2">20$</button>
                                                                <button type="button" @click="deposit(50)" class="btn btn-primary ml-2">50$</button>
                                                                <button type="button" @click="deposit(100)" class="btn btn-primary ml-2">100$</button>
                                                                <button type="button" @click="deposit(500)" class="btn btn-primary ml-2">500$</button>
                                                                <button type="button" @click="deposit(1000)" class="btn btn-primary ml-2">1000$</button>
                                                                <button type="button" @click="deposit(5000)" class="btn btn-primary ml-2">5000$</button>
                                                                <button type="button" @click="deposit(10000)" class="btn btn-primary ml-2">10000$</button>
                                                            </div>
                                                            <div style="display: flex; justify-content: center; align-items: center;">
                                                                <input type="text" class="form-control text-center" v-model="depositvalue" placeholder="500$" maxlength="9" autocomplete="off" v-if="selectedKonto.number" style="width: 15vw">
                                                            </div>
                                                            <button v-if="selectedKonto.number" type="submit" class="btn btn-block btn-primary mt-3" @click="deposit(depositvalue)">Geld einzahlen</button>
                                                        </div>
                                                        <div class="active tab-pane" style="overflow-x: auto" v-if="showbanksetting == 4">
                                                            <h3 v-if="selectedKonto.number && inputpinbool == true && showbanksetting == 4" class="text-center" style="font-family: 'Exo', sans-serif;text-shadow: 0 0 2px #000;">
                                                                Guthaben: <span style="color:green" v-if="selectedKonto.value >= 0">{{selectedKonto.value}}$</span><span style="color:red" v-else>{{selectedKonto.value}}$</span></h3>
                                                            <h3 v-if="!selectedKonto.number && showbanksetting == 4" class="text-center" style="font-family: 'Exo', sans-serif;text-shadow: 0 0 2px #000;">
                                                                Bitte ein Konto auswählen!</h3>
                                                            <div v-if="selectedKonto.number && inputpinbool == false && showbanksetting == 4" style="display: flex; justify-content: center; align-items: center;">
                                                                <input v-on:keyup.enter="checkPin()" type="password" class="form-control text-center" v-model="inputpin" placeholder="Pin" maxlength="4" autocomplete="off" style="width: 15vw">
                                                            </div>
                                                            <div v-if="selectedKonto.number && inputpinbool == false && showbanksetting == 4" style="display: flex; justify-content: center; align-items: center;">
                                                                <button type="submit" class="btn btn-block btn-primary mt-3" style="width: 15vw" @click="checkPin()">Eingeben</button>
                                                                <hr v-if="selectedKonto.number && inputpinbool == true && showbanksetting == 4" />
                                                            </div>
                                                            <hr v-if="selectedKonto.number && inputpinbool == true && showbanksetting == 4" />
                                                            <div class="form-group" v-if="selectedKonto.number && inputpinbool == true && showbanksetting == 4">
                                                                <label>Empfängerkonto</label>
                                                                <input type="text" class="form-control" v-model="empfänger" value="SA3701-" placeholder="Kontonummer" maxlength="20" autocomplete="off">
                                                            </div>
                                                            <div class="form-group" v-if="selectedKonto.number && inputpinbool == true && showbanksetting == 4">
                                                                <label>Betrag in $</label>
                                                                <input type="text" class="form-control" v-model="betrag" placeholder="500" maxlength="10" autocomplete="off">
                                                            </div>
                                                            <div class="form-group" v-if="selectedKonto.number && inputpinbool == true && showbanksetting == 4">
                                                                <label>Verwendungszweck</label>
                                                                <input type="text" autocomplete="off" class="form-control" v-model="verwendungszweck" placeholder="Hausverkauf" maxlength="64">
                                                            </div>
                                                            <div class="form-group" v-if="selectedKonto.number && inputpinbool == true && showbanksetting == 4">
                                                                <label>Dauerauftrag? (Für einen Dauerauftrag, die Anzahl der Tage in das Feld
                                                                    schreiben!)</label>
                                                                <input type="text" class="form-control" v-model="tage" placeholder="0" maxlength="3" autocomplete="off">
                                                            </div>
                                                            <button v-on:click="startTransfer()" type="submit" v-if="selectedKonto.number && inputpinbool == true && showbanksetting == 4" class="btn btn-block btn-primary btn-lg">Überweisung
                                                                tätigen</button>
                                                        </div>
                                                    </div>
                                                    <div class="active tab-pane" style="overflow-x: auto" v-if="showbanksetting == 5">
                                                        <div v-if="selectedKonto.number && inputpinbool == false" style="display: flex; justify-content: center; align-items: center;">
                                                            <input v-on:keyup.enter="checkPin()" type="password" class="form-control text-center" v-model="inputpin" placeholder="Pin" maxlength="4" autocomplete="off" style="width: 15vw">
                                                        </div>
                                                        <div v-if="selectedKonto.number && inputpinbool == false" style="display: flex; justify-content: center; align-items: center;">
                                                            <button type="submit" class="btn btn-block btn-primary mt-3" style="width: 15vw" @click="checkPin()">Eingeben</button>
                                                            <hr v-if="selectedKonto.number && inputpinbool == true" />
                                                        </div>
                                                        <h3 v-if="standingorderloading == false && inputpinbool == true" class="text-center" style="font-family: 'Exo', sans-serif;text-shadow: 0 0 2px #000;">
                                                            Daueraufträge werden geladen ...</h3>
                                                        <h3 v-if="(standingorder.length <= 0 && standingorder <= 0 && standingorderloading == true && inputpinbool == true) || !selectedKonto.number" class="text-center" style="font-family: 'Exo', sans-serif;text-shadow: 0 0 2px #000;">
                                                            Keine Daueraufträge vorhanden!</h3>
                                                        <label v-if="standingorder.length > 0 && standingorderloading == true && inputpinbool == true">Daueraufträge:</label>
                                                        <div class="table-responsive-md" v-if="standingorder.length > 0 && standingorderloading == true && inputpinbool == true">
                                                            <table class="table table-bordered">
                                                                <thead class="table-primary">
                                                                    <tr>
                                                                        <th>Sender</th>
                                                                        <th>Empfänger</th>
                                                                        <th>Verwendungszweck</th>
                                                                        <th>Betrag</th>
                                                                        <th>Alle x Tage</th>
                                                                        <th>Datum</th>
                                                                        <th>Aktion</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr v-for="(standingo, index) in standingorder" :key="index">
                                                                        <td>{{standingo.bankfrom}}</td>
                                                                        <td>{{standingo.bankto}}</td>
                                                                        <td>{{standingo.banktext}}</td>
                                                                        <td><span class="badge badge-success right">{{standingo.bankvalue}}$</span></td>
                                                                        <td>{{standingo.days}} Tag/e</td>
                                                                        <td>{{timeConverter(standingo.timestamp)}}</td>
                                                                        <td> <button class="btn btn-info btn-sm" type="submit">
                                                                                <i class="fas fa-times" @click="deleteStandingOrder(standingo)"></i>
                                                                            </button></td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                    <div class="active tab-pane" style="overflow-x: auto" v-if="showbanksetting == 6">
                                                        <h3 v-if="bankfilesloading == false" class="text-center" style="font-family: 'Exo', sans-serif;text-shadow: 0 0 2px #000;">
                                                            Kontoauszüge werden geladen ...</h3>
                                                        <h3 v-if="bankfiles.length <= 0 && banksettings <= 0 && bankfilesloading == true" class="text-center" style="font-family: 'Exo', sans-serif;text-shadow: 0 0 2px #000;">
                                                            Keine Kontoauszüge vorhanden!</h3>
                                                        <label v-if="bankfiles.length > 0 && bankfilesloading == true">Kontoauszüge:</label>
                                                        <div class="table-responsive-md" v-if="bankfiles.length > 0 && bankfilesloading == true">
                                                            <table class="table table-bordered">
                                                                <thead class="table-primary">
                                                                    <tr>
                                                                        <th>Sender</th>
                                                                        <th>Empfänger</th>
                                                                        <th>Verwendungszweck</th>
                                                                        <th>Betrag</th>
                                                                        <th>Datum</th>
                                                                        <th>Aktion</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr v-for="(bankfile, index) in bankfiles" :key="index">
                                                                        <td>{{bankfile.bankfrom}}</td>
                                                                        <td>{{bankfile.bankto}}</td>
                                                                        <td>{{bankfile.banktext}}</td>
                                                                        <td><span class="badge badge-success right">{{bankfile.bankvalue}}$</span>
                                                                        </td>
                                                                        <td>{{timeConverter(bankfile.banktime)}}
                                                                        </td>
                                                                        <td v-if="bankfile.bankto != bankfile.number"><span class="badge badge-info right">Gesendet</span>
                                                                        </td>
                                                                        <td v-else><span class="badge badge-info right">Empfangen</span>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                        <label v-if="banksettings.length > 0 && bankfilesloading == true">Bankaktionen:</label>
                                                        <div class="table-responsive-md" v-if="banksettings.length > 0 && bankfilesloading == true">
                                                            <table class="table table-bordered">
                                                                <thead class="table-primary">
                                                                    <tr>
                                                                        <th>Aktion</th>
                                                                        <th>Betrag</th>
                                                                        <th>Name</th>
                                                                        <th>Datum</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr v-for="(banksetting, index) in banksettings" :key="index">
                                                                        <td>{{banksetting.setting}}</td>
                                                                        <td><span class="badge badge-success right">{{banksetting.value}}$</span></td>
                                                                        <td>{{banksetting.name}}</td>
                                                                        <td>{{timeConverter(banksetting.timestamp)}}
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                    <div class="active tab-pane" style="overflow-x: auto" v-if="showbanksetting == 7 && selectedKonto.owner != myid">
                                                        <label v-if="selectBank.kontotyp == 0 || selectBank.kontotyp == 2">Kontotyp: <span class="badge badge-primary">Maze</span></label>
                                                        <label v-else>Kontotyp: <span class="badge badge-primary">Fleeca</span></label>
                                                        <br /><label>Standardkonto:</label>
                                                        <button v-if="defaultKonto != selectedKonto.number" v-on:click="setDefaultKonto()" type="submit" class="btn btn-block btn-primary">Als Standardkonto setzen</button>
                                                        <button v-if="defaultKonto == selectedKonto.number" type="submit" class="btn btn-block btn-primary" disabled>Als Standardkonto setzen</button>
                                                    </div>
                                                    <div class="active tab-pane" v-if="showbanksetting == 7 && selectedKonto.owner == myid">
                                                        <label v-if="selectBank.kontotyp == 0 || selectBank.kontotyp == 2">Kontotyp: <span class="badge badge-primary">Maze</span></label>
                                                        <label v-else>Kontotyp: <span class="badge badge-primary">Fleeca</span></label>
                                                        <br /><label>Standardkonto:</label>
                                                        <button v-if="defaultKonto != selectedKonto.number" v-on:click="setDefaultKonto()" type="submit" class="btn btn-block btn-primary">Als Standardkonto setzen</button>
                                                        <button v-if="defaultKonto == selectedKonto.number" type="submit" class="btn btn-block btn-primary" disabled>Als Standardkonto setzen</button>
                                                        <label class="mt-4">Neuer Kontoinhaber:</label>
                                                        <div class="input-group input-group">
                                                            <input type="text" class="form-control" placeholder="Neuer Kontoinhaber" v-model="newowner" maxlength="35" autocomplete="off">
                                                            <span class="input-group-append">
                                                                <button v-on:click="newOwner()" type="submit" class="btn btn-primary btn-flat ml-2" style="height: 3.65vh">Setzen</button>
                                                            </span>
                                                        </div>
                                                        <label class="mt-4">Partnerkarten:</label>
                                                        <div class="input-group input-group">
                                                            <button v-if="atbank == 0" v-on:click="newPartnerCard()" type="submit" class="btn btn-block btn-primary">Partnerkarte beantragen - 85$</button>
                                                            <button v-else v-on:click="newPartnerCard()" type="submit" class="btn btn-block btn-primary">Partnerkarte beantragen - 115$</button>
                                                            <button v-on:click="closeAllPartnerCard()" type="submit" class="btn btn-block btn-danger">Alle
                                                                Partnerkarten sperren</button>
                                                        </div>
                                                        <label class="mt-4">Firmenkonto:</label>
                                                        <div class="input-group input-group">
                                                            <button v-on:click="setGroupBank()" type="submit" class="btn btn-block btn-primary">Als
                                                                Firmenkonto setzen</button>
                                                            <button v-on:click="deleteGroupBank()" type="submit" class="btn btn-block btn-danger">Als
                                                                Firmenkonto entfernen</button>
                                                        </div>
                                                        <label class="mt-4">Konto schließen:</label>
                                                        <div class="input-group input-group">
                                                            <button v-on:click="closeBank()" type="submit" class="btn btn-block btn-danger">Konto
                                                                schließen</button>
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
    </div>
</div>
</template>

<script>
export default {
    name: 'Bank',
    data: function () {
        return {
            clicked: (Date.now() / 1000),
            clicked2: (Date.now() / 1000),
            clicked3: (Date.now() / 1000),
            bankshow: false,
            showbanksetting: 0,
            atbank: 0,
            selected: '',
            selectedKonto: [],
            defaultKonto: '',
            retValue: [],
            withdrawvalue: '',
            depositvalue: '',
            empfänger: '',
            betrag: 0,
            verwendungszweck: '',
            tage: 0,
            bankfiles: [],
            banksettings: [],
            bankfilesloading: false,
            standingorder: [],
            standingorderloading: false,
            myid: '',
            newpin: '0000',
            newowner: '',
            inputpin: '',
            inputpinbool: false,
            konten: []
        }
    },
    mounted() {
        document.body.classList.add("dark-mode");
        if (this.konten.length > 0) {
            this.showbanksetting = 1;
            this.konten.forEach(element => {
                if (element.banknumber == this.defaultKonto) {
                    this.selectedKonto.number = element.banknumber;
                    this.selectedKonto.value = element.bankvalue;
                    this.selectedKonto.owner = element.ownercharid;
                    this.selectedKonto.pincode = element.pincode;
                    this.selectedKonto.banktype = element.banktype;
                }
            });
        } else {
            this.showbanksetting = 0;
        }
    },
    methods: {
        timeConverter: function (UNIX_timestamp) {
            var a = new Date(UNIX_timestamp * 1000);
            var months = ['Jan.', 'Feb.', 'Mär.', 'Apr.', 'Mai', 'Jun.', 'Jul.', 'Aug.', 'Sep.', 'Okt.', 'Nov.', 'Dez.'];
            var year = a.getFullYear();
            var month = months[a.getMonth()]
            var date = a.getDate()
            var hour = a.getHours()
            var min = a.getMinutes()
            var sec = a.getSeconds()
            if (month < 10) month = '0' + month;
            if (date < 10) date = '0' + date;
            if (hour < 10) hour = '0' + hour;
            if (min < 10) min = '0' + min;
            if (sec < 10) sec = '0' + sec;
            var time = date + ' ' + month + ' ' + year + ' - ' + hour + ':' + min + ':' + sec;
            return time;
        },
        hideBankMenu: function () {
            this.bankshow = false;
        },
        showBankMenu: function (json, atbank, defaultbank, myid) {
            this.selectedKonto = []
            this.atbank = atbank;
            this.bankshow = !this.bankshow;
            this.inputpin = '';
            this.inputpinbool = false;
            if (json) {
                this.konten = JSON.parse(json);
                this.defaultKonto = defaultbank;
                this.myid = myid;
            }
            if (this.konten.length > 0) {
                this.konten.forEach(element => {
                    if (element.banknumber == this.defaultKonto) {
                        this.selectedKonto.number = element.banknumber;
                        this.selectedKonto.value = element.bankvalue;
                        this.selectedKonto.owner = element.ownercharid;
                        this.selectedKonto.pincode = element.pincode;
                        this.selectedKonto.banktype = element.banktype;
                    }
                });
            }
            if (this.konten.length > 0) {
                this.showbanksetting = 1;
            } else {
                this.showbanksetting = 0;
            }
            this.$forceUpdate();
        },
        updateBankMenu: function (json) {
            if (json) {
                this.konten = JSON.parse(json);
                this.konten.forEach(element => {
                    if (element.banknumber == this.selectedKonto.number) {
                        this.selectedKonto.number = element.banknumber;
                        this.selectedKonto.value = element.bankvalue;
                    }
                });
                this.$forceUpdate();
            }
        },
        showBankFiles: function (json, json2) {
            if (json) {
                this.bankfiles = JSON.parse(json);
            }
            if (json2) {
                this.banksettings = JSON.parse(json2);
            }
            this.$forceUpdate();
        },
        showStandingOrder: function (json) {
            if (json) {
                this.standingorder = JSON.parse(json);
            }
            this.$forceUpdate();
        },
        deleteStandingOrder: function (standingorder) {
            var self = this;
            if (standingorder) {
                if ((Date.now() / 1000) > this.clicked) {
                    this.retValue = new Object();
                    this.retValue.value = standingorder.id;
                    this.retValue.banknumber = this.selectedKonto.number;
                    this.retValue.bankat = this.atbank;
                    // eslint-disable-next-line no-undef
                    mp.trigger("Client:BankSettings", "deletestandingorder", JSON.stringify(this.retValue));
                    this.clicked = (Date.now() / 1000) + (2);
                    this.standingorderloading = false;
                    this.retValue = new Object();
                    this.retValue.value = 0;
                    this.retValue.banknumber = this.selectedKonto.number;
                    this.retValue.bankat = this.atbank;
                    // eslint-disable-next-line no-undef
                    mp.trigger("Client:BankSettings", "getstandingorder", JSON.stringify(this.retValue));
                    this.clicked3 = (Date.now() / 1000) + (180);
                }
                setTimeout(() => {
                    self.standingorderloading = true;
                }, 2250);
            }
        },
        selectMenu: function (id) {
            var self = this;
            this.showbanksetting = id;
            if (this.showbanksetting == 5) {
                this.standingorderloading = false;
                if (this.selectedKonto.number) {
                    if ((Date.now() / 1000) > this.clicked3) {
                        this.retValue = new Object();
                        this.retValue.value = 0;
                        this.retValue.banknumber = this.selectedKonto.number;
                        this.retValue.bankat = this.atbank;
                        // eslint-disable-next-line no-undef
                        mp.trigger("Client:BankSettings", "getstandingorder", JSON.stringify(this.retValue));
                        this.clicked3 = (Date.now() / 1000) + (180);
                    }
                    setTimeout(() => {
                        self.standingorderloading = true;
                    }, 2250);
                } else {
                    this.standingorderloading = true;
                }
            }
            if (this.showbanksetting == 6) {
                this.bankfilesloading = false;
                if (this.selectedKonto.number) {
                    if ((Date.now() / 1000) > this.clicked2) {
                        this.retValue = new Object();
                        this.retValue.value = 0;
                        this.retValue.banknumber = this.selectedKonto.number;
                        this.retValue.bankat = this.atbank;
                        // eslint-disable-next-line no-undef
                        mp.trigger("Client:BankSettings", "getbankfiles", JSON.stringify(this.retValue));
                        this.clicked2 = (Date.now() / 1000) + (180);
                    }
                    setTimeout(() => {
                        self.bankfilesloading = true;
                    }, 2250);
                } else {
                    this.bankfilesloading = true;
                }
            }
        },
        selectBank: function (bank) {
            if (this.selectedKonto.number != bank.banknumber) {
                this.clicked = (Date.now() / 1000);
                this.clicked2 = (Date.now() / 1000);
                this.clicked3 = (Date.now() / 1000);
            }
            this.selectedKonto.number = bank.banknumber;
            this.selectedKonto.value = bank.bankvalue;
            this.selectedKonto.pincode = bank.pincode;
            this.selectedKonto.owner = bank.ownercharid;
            this.selectedKonto.banktype = bank.banktype;
            this.inputpin = '';
            this.inputpinbool = false;
            this.$forceUpdate();
        },
        newPin: function () {
            if ((Date.now() / 1000) > this.clicked) {
                if (!this.selectedKonto.number) {
                    return;
                }
                if (this.selectedKonto.owner != this.myid) {
                    // eslint-disable-next-line no-undef
                    mp.trigger("Client:SendNotificationWithoutButton", 'Du bist nicht der Inhaber von diesem Konto!', 'error', 'top-left', '2250');
                    return;
                }
                this.retValue = new Object();
                this.retValue.value = 0;
                this.retValue.banknumber = this.selectedKonto.number;
                this.retValue.bankat = this.atbank;
                // eslint-disable-next-line no-undef
                mp.trigger("Client:BankSettings", "newpin", JSON.stringify(this.retValue));
                this.clicked = (Date.now() / 1000) + (1);
            }
        },
        setDefaultKonto: function () {
            if ((Date.now() / 1000) > this.clicked) {
                if (!this.newpin || this.newpin.length != 4 || !this.selectedKonto.number) {
                    return;
                }
                this.retValue = new Object();
                this.retValue.value = 0;
                this.retValue.banknumber = this.selectedKonto.number;
                this.retValue.bankat = this.atbank;
                this.defaultKonto = this.selectedKonto.number;
                // eslint-disable-next-line no-undef
                mp.trigger("Client:BankSettings", "defaultkonto", JSON.stringify(this.retValue));
                this.konten.forEach(element => {
                    if (element.banknumber == this.selectedKonto.number) {
                        this.selectedKonto.number = element.banknumber;
                        this.selectedKonto.value = element.bankvalue;
                        this.selectedKonto.owner = element.ownercharid;
                        this.selectedKonto.pincode = element.pincode;
                        this.selectedKonto.banktype = element.banktype;
                    }
                });
                this.clicked = (Date.now() / 1000) + (1);
            }
        },
        newOwner: function () {
            if ((Date.now() / 1000) > this.clicked) {
                if (!this.newowner || this.newowner.length <= 3 || !this.selectedKonto.number) {
                    return;
                }
                this.retValue = new Object();
                this.retValue.value = 0;
                this.retValue.owner = this.newowner;
                this.retValue.banknumber = this.selectedKonto.number;
                this.retValue.bankat = this.atbank;
                // eslint-disable-next-line no-undef
                mp.trigger("Client:BankSettings", "newowner", JSON.stringify(this.retValue));
                this.clicked = (Date.now() / 1000) + (1);
            }
        },
        closeBank: function () {
            if ((Date.now() / 1000) > this.clicked) {
                if (!this.selectedKonto.number) {
                    return;
                }
                this.retValue = new Object();
                this.retValue.value = 0;
                this.retValue.banknumber = this.selectedKonto.number;
                this.retValue.bankat = this.atbank;
                // eslint-disable-next-line no-undef
                mp.trigger("Client:BankSettings", "closebank", JSON.stringify(this.retValue));
                this.clicked = (Date.now() / 1000) + (1);
            }
        },
        setGroupBank: function () {
            if ((Date.now() / 1000) > this.clicked) {
                if (!this.selectedKonto.number) {
                    return;
                }
                this.retValue = new Object();
                this.retValue.value = 0;
                this.retValue.banknumber = this.selectedKonto.number;
                this.retValue.bankat = this.atbank;
                // eslint-disable-next-line no-undef
                mp.trigger("Client:BankSettings", "setgroupbank", JSON.stringify(this.retValue));
                this.clicked = (Date.now() / 1000) + (2);
            }
        },
        deleteGroupBank: function () {
            if ((Date.now() / 1000) > this.clicked) {
                if (!this.selectedKonto.number) {
                    return;
                }
                this.retValue = new Object();
                this.retValue.value = 0;
                this.retValue.banknumber = this.selectedKonto.number;
                this.retValue.bankat = this.atbank;
                // eslint-disable-next-line no-undef
                mp.trigger("Client:BankSettings", "deletegroupbank", JSON.stringify(this.retValue));
                this.clicked = (Date.now() / 1000) + (2);
            }
        },
        newPartnerCard: function () {
            if ((Date.now() / 1000) > this.clicked) {
                if (!this.selectedKonto.number) {
                    return;
                }
                this.retValue = new Object();
                this.retValue.value = 0;
                this.retValue.banknumber = this.selectedKonto.number;
                this.retValue.bankat = this.atbank;
                // eslint-disable-next-line no-undef
                mp.trigger("Client:BankSettings", "newpartnercard", JSON.stringify(this.retValue));
                this.clicked = (Date.now() / 1000) + (2);
            }
        },
        closeAllPartnerCard: function () {
            if ((Date.now() / 1000) > this.clicked) {
                if (!this.selectedKonto.number) {
                    return;
                }
                this.retValue = new Object();
                this.retValue.value = 0;
                this.retValue.banknumber = this.selectedKonto.number;
                this.retValue.bankat = this.atbank;
                // eslint-disable-next-line no-undef
                mp.trigger("Client:BankSettings", "deleteallpartnercard", JSON.stringify(this.retValue));
                this.clicked = (Date.now() / 1000) + (3);
            }
        },
        checkPin: function () {
            if ((Date.now() / 1000) > this.clicked) {
                if (!this.inputpin || this.inputpin.length != 4 || !this.selectedKonto.number) {
                    return;
                }
                if (this.inputpin == this.selectedKonto.pincode) {
                    this.inputpinbool = true;
                    this.$forceUpdate();
                } else {
                    this.retValue = new Object();
                    this.retValue.value = 0;
                    this.retValue.pin = this.inputpin;
                    this.retValue.banknumber = this.selectedKonto.number;
                    this.retValue.bankat = this.atbank;
                    // eslint-disable-next-line no-undef
                    mp.trigger("Client:BankSettings", "wrongpin", JSON.stringify(this.retValue));
                }
                this.clicked = (Date.now() / 1000) + (1);
            }
        },
        withdraw: function (value) {
            if ((Date.now() / 1000) > this.clicked) {
                if (!value || value <= 0 || !this.selectedKonto.number) {
                    return;
                }
                this.retValue = new Object();
                this.retValue.value = value;
                this.retValue.banknumber = this.selectedKonto.number;
                this.retValue.bankat = this.atbank;
                this.withdrawvalue = value;
                // eslint-disable-next-line no-undef
                mp.trigger("Client:BankSettings", "withdraw", JSON.stringify(this.retValue));
                this.clicked = (Date.now() / 1000) + (1);
            }
        },
        newBank: function () {
            if ((Date.now() / 1000) > this.clicked) {
                this.retValue = new Object();
                this.retValue.value = 1;
                this.retValue.banknumber = 'n/A';
                this.retValue.bankat = this.atbank;
                // eslint-disable-next-line no-undef
                mp.trigger("Client:BankSettings", "new", JSON.stringify(this.retValue));
                this.clicked = (Date.now() / 1000) + (2);
                this.showbanksetting = 1;
            }
        },
        deposit: function (value) {
            if ((Date.now() / 1000) > this.clicked) {
                if (!value || value <= 0 || !this.selectedKonto.number) {
                    return;
                }
                this.retValue = new Object();
                this.retValue.value = value;
                this.retValue.banknumber = this.selectedKonto.number;
                this.retValue.bankat = this.atbank;
                this.depositvalue = value;
                // eslint-disable-next-line no-undef
                mp.trigger("Client:BankSettings", "deposit", JSON.stringify(this.retValue));
                this.clicked = (Date.now() / 1000) + (1);
            }
        },
        startTransfer: function () {
            if ((Date.now() / 1000) > this.clicked) {
                if (!this.betrag || this.betrag <= 0 || !this.selectedKonto.number) {
                    return;
                }
                this.retValue = new Object();
                this.retValue.value = this.betrag;
                this.retValue.banknumber = this.selectedKonto.number;
                this.retValue.bankat = this.atbank;
                this.retValue.empfänger = this.empfänger;
                this.retValue.verwendungszweck = this.verwendungszweck;
                this.retValue.tage = this.tage;
                this.depositvalue = this.betrag;
                // eslint-disable-next-line no-undef
                mp.trigger("Client:BankSettings", "transfer", JSON.stringify(this.retValue));
                this.clicked = (Date.now() / 1000) + (2);
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

.centering {
    margin: 0;
    position: absolute;
    top: 47%;
    left: 50%;
    margin-right: -50%;
    transform: translate(-50%, -50%)
}

.centering2 {
    width: 100%;
    height: 100%;
    overflow-x: auto;
    scrollbar-width: none;
    margin: auto;
    position: absolute;
    z-index: 1;
}
</style>
