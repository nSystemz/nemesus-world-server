<template>
  <div class="hud" style="overflow-y: auto;background-color:transparent; height:100%; width:100%">
    <div class="centering2" style="height: 100%; background-color: rgba(69, 77, 85, .8);" v-if="showBlack">
      <div style="display: flex; justify-content: center; align-items: center;" class="text-center">
        <h1 style="margin-top: 40vh; font-family: 'Exo', sans-serif;">{{blackText}}</h1>
      </div>
    </div>
    <div>
      <div class="info server" v-if="showhud">
        <div class="info player">
          <div class="money float-right mr-2" style="color:#4477ad">
            <span>{{("00000000" + money).slice(-8)}}$</span>
          </div>
          <div id="status" style="font-size: 1.9vh;margin-top: 0.2vh !important">
            <ul>
              <progress-bar class="float-right mr-2" style="width:3.5vh;padding-bottom: 0.4vh" type="circle"
                ref="healthbar" color="#e03a3a" strokeWidth="0.5" duration="2000" :options="options1">
              </progress-bar>
              <progress-bar class="float-right mr-2" style="width:3.5vh;padding-bottom: 0.2vh" type="circle"
                ref="hungerbar" color="#e03a3a" strokeWidth="0.5" duration="2000" :options="options2">
              </progress-bar>
              <progress-bar class="float-right mr-2" style="width:3.5vh;padding-bottom: 0.4vh" type="circle"
                ref="thirstbar" color="#e03a3a" strokeWidth="0.5" duration="2000" :options="options3">
              </progress-bar>
              <progress-bar class="float-right mr-2" style="width:3.5vh;padding-bottom: 0.4vh" type="circle"
                ref="shieldbar" color="#e03a3a" strokeWidth="0.5" duration="2000" :options="options4">
              </progress-bar>
            </ul>
          </div>
        </div>
      </div>
    </div>
    <div class="centering2" style="height: 100%; background-color: rgba(69, 77, 85, .8);" v-if="tutorialStadthalle2">
      <div class="row justify-content-center centering">
        <div class="col-md-12 mt-1 animate__animated animate__bounceInUp">
          <div class="col-md-12 mt-1">
            <div class="box box-default">
              <div class="card card-primary card-outline">
                <div class="card-header" style="font-family: 'Exo', sans-serif;"><strong class="ml-2">Gefälschte
                    Staatsbürgerschaft</strong>
                  <button type="button" @click="checkEinreise2()" class="btn btn-primary float-right"
                    v-if="!einreisefinish">Dokument
                    nehmen</button>
                </div>
                <div class="card-body">
                  <div class="row">
                    <div class="col-md-12 mt-1">
                      <div style="display: flex; justify-content: center; align-items: center;">
                        <h6>Ey <b style="color:#3F6791;font-family: 'Exo', sans-serif;">{{name.split(" ")[0]}}</b>,
                          warum hast du mich hier solange warten lassen? Ich hab kein Bock auf
                          Bullen, die suchen mich sowieso schon. Hier gib mir noch ein paar Daten von dir, damit wir
                          deine gefälschten Papiere vervollständigen können!</h6>
                      </div>
                      <div style="display: flex; justify-content: center; align-items: center;">
                        <h6>Hier füll das selber aus, was du dort reinschreibst ist mir scheiss egal!</h6>
                      </div>
                      <hr />
                      <li class="active">
                        <span class="fa fa-search-plus mt-3 mr-2"></span>Größe in cm
                        <input placeholder="180" value="180" v-model="size" type="text"
                          class="form-control mt-1" maxlength="3">
                      </li>
                      <li class="active">
                        <span class="fa fa-tint mt-3 mr-2"></span>Augenfarbe
                        <input placeholder="Grün" v-model="eyecolor" type="text" class="form-control mt-1"
                          maxlength="10">
                      </li>
                      <li class="active">
                        <span class="fa fa-book mt-3 mr-2"></span>Abschluss
                        <input placeholder="Realschulabschluss" v-model="education" type="text"
                          class="form-control mt-1" maxlength="64">
                      </li>
                      <li class="active">
                        <span class="fas fa-pencil-alt mt-3 mr-2"></span>Besondere Fähigkeiten
                        <input placeholder="Schwimmen, Autoaffin, Geduldig" v-model="skills" type="text"
                          class="form-control mt-1" maxlength="64">
                      </li>
                      <li class="active">
                        <span class="fa fa-file-alt mt-3 mr-2"></span>Aussehen
                        <input placeholder="Normal gebaut, kurze Gesichtsbehaarung" v-model="appearance" type="text"
                          class="form-control mt-1" maxlength="64">
                      </li>
                      <div class="mt-3" style="display: flex; justify-content: center; align-items: center;">
                        <h6 style="color:yellow">Achja, ich hab wie versprochen einen Roller gemietet für dich, setzt
                          dich drauf und hau hier ab ich verpiss mich auch direkt!
                        </h6>
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
    <div style="height: 100%; background-color: transparent;" v-if="showradiosystem">
      <div class="row justify-content-center centering4">
        <div class="col-md-12 mt-1 animate__animated animate__bounceInUp">
          <div class="col-md-12 mt-1">
            <div class="box box-default">
              <div class="row">
                <div class="card card-primary card-outline">
                  <div class="card-header" style="font-family: 'Exo', sans-serif; font-size: 1.05vw">
                    <span>Funkgerät</span>
                    <button v-if="voicerp == 1" @click="setRadioFreq('LS')" type="button"
                        class="btn btn-primary float-right">Lautsprecher an/aus</button>
                  </div>
                  <div class="card-body" style="max-height:25vh; width: 25vw; overflow-x: auto">
                    <div class="row">
                      <div class="col-md-12">
                        <div style="display: flex; justify-content: center; align-items: center;">
                          <div class="infoboxtext ml-2">
                            <div class="col-md-4">
                              <img style="width: 5vw; z-index:" class="float-left" src="../assets/images/inventory/Funkgerät.png">
                            </div>
                            <div class="col-md-9">
                            <span class="ml-3" style="visibility: hidden">Frequenz:</span>
                            <input type="text" maxlength="3" class="col-md-5 mt-2 form-control text-center float-right"
                              placeholder="125 mHz" style="border-radius: 1vw" v-model="frequenz" autofocus>
                            </div>
                          </div>
                        </div>
                        <hr />
                      </div>
                    </div>
                    <div style="display: flex; justify-content: center; align-items: center;">
                      <button style="margin-top: 0.3vw" @click="setRadioFreq(frequenz)" type="button"
                        class="btn btn-primary">Einstellen</button>
                      <button style="display: flex; justify-content: center; align-items: center; margin-top: 0.3vw"
                        @click="setRadioFreq('-1')" type="button"
                        class="btn btn-danger ml-3">Ausschalten</button>
                      <button style="display: flex; justify-content: center; align-items: center; margin-top: 0.3vw"
                        @click="setRadioFreq('Aus')" type="button"
                        class="btn btn-secondary ml-3">Abbrechen</button>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div style="height: 100%; background-color: transparent;" v-if="showmusic">
      <div class="row justify-content-center centering4">
        <div class="col-md-12 mt-1 animate__animated animate__bounceInUp">
          <div class="col-md-12 mt-1">
            <div class="box box-default">
              <div class="row">
                <div class="card card-primary card-outline">
                  <div class="card-header" style="font-family: 'Exo', sans-serif; font-size: 1.05vw">
                    <span>Musik abspielen</span>
                    <button @click="play3DSound(musiclink, 2)" type="button"
                        class="btn btn-danger float-right">Musik ausschalten</button>
                  </div>
                  <div class="card-body" style="max-height:35vh; width: 25vw; overflow-x: auto">
                      <div class="col-md-12">
                        <div style="display: flex; justify-content: center; align-items: center;">
                            <div class="col-md-4">
                              <img v-if="showmusicstatus == 1" style="width: 5vw; z-index:" class="float-left" src="../assets/images/turntable.png">
                              <img v-else style="width: 4vw; z-index:" class="float-left" src="../assets/images/inventory/Ghettoblaster.png">
                            </div>
                          <div class="infoboxtext">
                            <div style="display: flex; justify-content: center; align-items: center;">
                            <input type="text" maxlength="128" class="col-md-12 mt-2 form-control text-center float-left"
                              placeholder="Musiklink" style="border-radius: 1vw" v-model="musiclink" autofocus>
                            </div>
                          </div>
                        </div>
                        <hr />
                    </div>
                    <div style="display: flex; justify-content: center; align-items: center;">
                      <button style="margin-top: 0.3vw" @click="play3DSound(musiclink, 0)" type="button"
                        class="btn btn-primary">Abspielen</button>
                      <button style="display: flex; justify-content: center; align-items: center; margin-top: 0.3vw"
                        @click="play3DSound(musiclink, 1)" type="button"
                        class="btn btn-success ml-3">Pause/Weiter</button>
                      <button style="display: flex; justify-content: center; align-items: center; margin-top: 0.3vw"
                        @click="play3DSound(musiclink, -1)" type="button"
                        class="btn btn-secondary ml-3">Abbrechen</button>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div style="height: 100%; background-color: transparent;" v-if="showfuel">
      <div class="row justify-content-center centering3">
        <div class="col-md-12 mt-1 animate__animated animate__bounceInUp">
          <div class="col-md-12 mt-1">
            <div class="box box-default">
              <div class="row">
                <div class="card card-primary card-outline">
                  <div class="card-header" style="font-family: 'Exo', sans-serif; font-size: 1.2vw">
                    Zapfsäule
                  </div>
                  <div class="card-body" style="max-height:50vh; width: 25vw; overflow-x: auto">
                    <div class="row">
                      <div class="col-md-12">
                        <div class="card2 card card-primary card-outline">
                          <div style="display: flex; justify-content: center; align-items: center;">
                            <i class="fas fa-gas-pump fa-4x mt-3"
                              style="color:#3F6791; text-shadow: 1px 0 0 #000, 0 -1px 0 #000, 0 1px 0 #000, -1px 0 0 #000;"></i>
                          </div>
                          <div class="progress mr-4 ml-4 mt-3">
                            <div class="progress-bar progress-bar-striped progress-bar-animated" role="progressbar"
                              aria-valuenow="75" aria-valuemin="0" aria-valuemax="100" style="width: 0%"></div>
                          </div>
                          <div class="mb-3" style="display: flex; justify-content: center; align-items: center;">
                            Verfügbar: {{bizzproducts}}l für {{getfuelprice}}$ pro Liter | Aktueller Preis:
                            {{fuelprice}}$
                            <hr />
                            <br />
                          </div>
                          <button type="button" class="btn btn-primary mb-2 mr-4 ml-4"
                            @click="startStopFuel()">{{getFuelText()}}</button>
                          <button type="button" class="btn btn-secondary mb-4 mr-4 ml-4"
                            @click="hideFuelStation(0)">Abbrechen</button>
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
    <div style="height: 100%; background-color: transparent;" v-if="showticket">
      <div class="row justify-content-center centering34">
        <div class="col-md-12 mt-1 animate__animated animate__bounceInUp">
          <div class="col-md-12 mt-1">
            <div class="box box-default">
              <div class="row">
                <div class="card card-primary card-outline">
                  <div class="card-header" style="font-family: 'Exo', sans-serif; font-size: 1.05vw">
                    <span>Strafzettel ausstellen an {{arrestName}}</span>
                    <img style="width: 2vw; z-index:" class="float-right" src="../assets/images/lslogo.png">
                  </div>
                  <div class="card-body" style="max-height:50vh; width: 25vw; overflow-x: auto">
                    <div class="row">
                      <div class="col-md-12">
                        <div style="display: flex; justify-content: center; align-items: center;">
                          <div class="infoboxtext ml-2">
                            <label>Grund:</label>
                            <input type="text" maxlength="64" class="col-md-12 form-control text-center"
                              placeholder="Grund" style="border-radius: 1vw" v-model="arrestInfo[0]">
                          </div>
                        </div>
                        <div style="display: flex; justify-content: center; align-items: center;">
                          <div class="infoboxtext ml-2 mt-3">
                            <label>Summe:</label>
                            <input type="text" maxlength="5" class="col-md-12 form-control text-center"
                              placeholder="Angabe in Dollar" style="border-radius: 1vw" v-model="arrestInfo[1]">
                          </div>
                        </div>
                        <hr />
                      </div>
                    </div>
                    <div style="display: flex; justify-content: center; align-items: center;">
                      <button style="margin-top: 0.3vw" @click="setTicket()" type="button"
                        class="btn btn-primary">Ausstellen</button>
                      <button style="display: flex; justify-content: center; align-items: center; margin-top: 0.3vw"
                        @click="hideTicket()" type="button"
                        class="btn btn-secondary ml-3">Abbrechen</button>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div style="height: 100%; background-color: transparent;" v-if="showrecept">
      <div class="row justify-content-center centering33">
        <div class="col-md-12 mt-1 animate__animated animate__bounceInUp">
          <div class="col-md-12 mt-1">
            <div class="box box-default">
              <div class="row">
                <div class="card card-primary card-outline">
                  <div class="card-header" style="font-family: 'Exo', sans-serif; font-size: 1.05vw">
                    <span>Rezept ausstellen an {{arrestName}}</span>
                    <img style="width: 2vw; z-index:" class="float-right" src="../assets/images/inventory/Rezept.png">
                  </div>
                  <div class="card-body" style="max-height:50vh; width: 25vw; overflow-x: auto">
                    <div class="row">
                      <div class="col-md-12">
                        <div style="display: flex; justify-content: center; align-items: center;">
                          <div class="infoboxtext ml-2">
                              <label for="exampleSelectBorder">Medikament auswählen:</label>
                              <button @click="setRecept('Ibuprofee-800mg')" type="button" class="btn btn-outline-light btn-block"><i class="fa-solid fa-tablets"></i> Ibuprofee-800mg</button>
                              <button @click="setRecept('Antibiotika')" type="button" class="btn btn-outline-light btn-block"><i class="fa-solid fa-tablets"></i> Antibiotika</button>
                              <button @click="setRecept('Morphin-10mg')" type="button" class="btn btn-outline-danger btn-block"><i class="fa-solid fa-tablets"></i> Morphin-10mg</button>
                          </div>
                        </div>
                        <hr />
                      </div>
                    </div>
                    <div style="display: flex; justify-content: center; align-items: center;">
                      <button style="display: flex; justify-content: center; align-items: center; margin-top: 0.3vw"
                        @click="hideRecept()" type="button"
                        class="btn btn-secondary ml-3">Abbrechen</button>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div style="height: 100%; background-color: transparent;" v-if="showcrafting">
      <div class="row justify-content-center centering4">
        <div class="col-md-12 mt-1 animate__animated animate__bounceInUp">
          <div class="col-md-12 mt-1">
            <div class="box box-default">
              <div class="row">
                <div class="card card-primary card-outline">
                  <div class="card-header" style="font-family: 'Exo', sans-serif; font-size: 1.05vw">
                    <span>Craftingtisch</span>
                    <span class="float-right">{{mats}} Materialien</span>
                    <span class="float-right">{{gangzone.value}}</span>
                  </div>
                  <div class="card-body" style="max-height:75vh; width: 30vw; overflow-x: auto">
                      <div class="col-md-12">
                        <div style="display: flex; justify-content: center; align-items: center;">
                          <div class="infoboxtext">
                            <div class="col-md-12">
                              <img class="mr-2 weaponicon" style="width: 3.0vw" src="../assets/images/inventory/Bandage.png" @click="weaponinfo='Bandage,10,5,1'">
                              <img class="mr-2 weaponicon" style="width: 3.0vw" src="../assets/images/inventory/Schlagring.png" @click="weaponinfo='Schlagring,20,7,1'">
                              <img class="mr-2 weaponicon" style="width: 5.0vw" src="../assets/images/inventory/Baseballschläger.png" @click="weaponinfo='Baseballschläger,30,8,1'">
                              <img class="mr-2 weaponicon" style="width: 5.0vw" src="../assets/images/inventory/Messer.png" @click="weaponinfo='Messer,35,8,1'">
                              <img class="mr-2 weaponicon" style="width: 5.0vw" src="../assets/images/inventory/Klappmesser.png" @click="weaponinfo='Klappmesser,35,8,1'">
                            </div>
                            <hr/>
                            <div class="col-md-12">
                              <img class="mr-4 weaponicon" style="width: 3.0vw" src="../assets/images/inventory/Dietrich.png" @click="weaponinfo='Dietrich,15,10,2'">
                              <img class="mr-4 weaponicon" style="width: 3.8vw" src="../assets/images/inventory/Pistole.png" @click="weaponinfo='Pistole,120,13,2'">
                              <img class="mr-4 weaponicon" style="width: 3.8vw" src="../assets/images/inventory/Pistole-MK2.png" @click="weaponinfo='Pistole-MK2,150,15,2'">
                              <img class="mr-4 weaponicon" style="width: 3.8vw" src="../assets/images/inventory/Pistole-50.png" @click="weaponinfo='Pistole-50,135,14,2'">
                              <img class="mr-4 weaponicon" style="width: 2.8vw" src="../assets/images/inventory/9mm-Munition.png" @click="weaponinfo='9mm-Munition,60,9,2'">
                            </div>
                            <hr/>
                            <div class="col-md-12">
                              <img class="mr-4 weaponicon" style="width: 3.8vw" src="../assets/images/inventory/Mini-SMG.png" @click="weaponinfo='Mini-SMG,240,20,3'">
                              <img class="mr-4 weaponicon" style="width: 3.8vw" src="../assets/images/inventory/TEC9.png" @click="weaponinfo='TEC9,260,25,3'">
                              <img class="mr-4 weaponicon" style="width: 3.1vw" src="../assets/images/inventory/Kleine-Schutzweste.png" @click="weaponinfo='Kleine-Schutzweste,300,30,3'">
                              <img class="mr-4 weaponicon" style="width: 2.8vw" src="../assets/images/inventory/5.56-Munition.png" @click="weaponinfo='5.56-Munition,100,15,3'">
                              <img class="mr-4 weaponicon" style="width: 2.8vw" src="../assets/images/inventory/Schweissgerät.png" @click="weaponinfo='Schweissgerät,450,45,3'">
                            </div>
                            <hr/>
                            <div class="col-md-12">
                              <img class="mr-4 weaponicon" style="width: 3.1vw" src="../assets/images/inventory/Kokain.png" @click="weaponinfo='Kokain,10,10,2'">
                              <img class="mr-4 weaponicon" style="width: 3.1vw" src="../assets/images/inventory/Crystal-Meth.png" @click="weaponinfo='Crystal-Meth,15,5,3'">
                              <img class="mr-4 weaponicon" style="width: 3.1vw" src="../assets/images/inventory/Space-Cookies.png" @click="weaponinfo='Space-Cookies,12,10,3'"></img>
                            </div>
                          </div>
                        </div>
                        <hr/> 
                        <div v-if="weaponinfo">
                        <div style="display: flex; justify-content: center; align-items: center;">
                          <span v-if="weaponinfo.split(',')[0] != 'Kokain' && weaponinfo.split(',')[0] != 'Crystal-Meth' && weaponinfo.split(',')[0] != 'Space-Cookies'" style="font-family: 'Exo', sans-serif">{{weaponinfo.split(',')[0]}}, Benötigte Materialien: {{weaponinfo.split(',')[1]}}/Stck</span>
                          <span v-if="weaponinfo.split(',')[0] == 'Kokain'" style="font-family: 'Exo', sans-serif">{{weaponinfo.split(',')[0]}}, Benötigte Materialien: Kokablätter, Schwefelsäure</span>
                          <span v-if="weaponinfo.split(',')[0] == 'Crystal-Meth'" style="font-family: 'Exo', sans-serif">{{weaponinfo.split(',')[0]}}, Benötigte Materialien: Kokain, Ibuprofee-400mg, Frostschutzmittel</span>
                        </div>
                          <div style="display: flex; justify-content: center; align-items: center;">
                            <span class="text-muted" v-if="weaponinfo.split(',')[0] == 'Kokain'" style="font-family: 'Exo', sans-serif">1x Schwefelsäure + 5 Kokablätter = 10g Kokain</span>
                            <span class="text-muted" v-if="weaponinfo.split(',')[0] == 'Crystal-Meth'" style="font-family: 'Exo', sans-serif">3g Kokain + 1x Ibuprofee-400mg + 1x Frostschutzmittel = 5g Crystal-Meth</span>
                            <span class="text-muted" v-if="weaponinfo.split(',')[0] == 'Space-Cookies'" style="font-family: 'Exo', sans-serif">5g Marihuana + 1x Backmischung = 10 Space-Cookies</span>
                        </div>
                        <div style="display: flex; justify-content: center; align-items: center;">
                          <img class="mt-4 weaponiconactive" style="width: 4.8vw" :src="getImgUrl(weaponinfo.split(',')[0])">
                        </div>
                        <div style="display: flex; justify-content: center; align-items: center;">
                        <button style="margin-top: 1.25vw;color:white" @click="startCraft()" type="button"
                        class="btn btn-primary">Craften</button>
                      </div>
                    </div>
                    <div v-if="!weaponinfo">
                        <div class="alert alert-info alert-dismissible text-center">
                          Gegenstand zum craften auswählen ...
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
    <div style="height: 100%; background-color: transparent;" v-if="showgangzone">
      <div class="row justify-content-center centering4">
        <div class="col-md-12 mt-1 animate__animated animate__bounceInUp">
          <div class="col-md-12 mt-1">
            <div class="box box-default">
              <div class="row">
                <div class="card card-primary card-outline">
                  <div class="card-header" style="font-family: 'Exo', sans-serif; font-size: 1.05vw">
                    <span>Gangzone: {{gangzone.name}}</span>
                    <span class="float-right">{{gangzone.value}}</span>
                  </div>
                  <div class="card-body" style="max-height:45vh; width: 30vw; overflow-x: auto">
                      <div class="col-md-12">
                        <div style="display: flex; justify-content: center; align-items: center;">
                          <div class="infoboxtext" v-if="gangzoneprop.length > 0">
                            <strong>Diese Gangzone wird gehalten durch:</strong>
                            <table class="table table-bordered mt-3">
                              <thead>
                              <tr>
                              <th style="width: 10px">#</th>
                              <th>Gang</th>
                              <th>Respektanzeige</th>
                              <th style="width: 40px">Respekt</th>
                              </tr>
                              </thead>
                              <tbody>
                              <tr v-for="(prop, index) in gangzoneprop" :key="index">
                              <td>1.</td>
                              <td>{{prop.groupname}}</td>
                              <td>
                              <div class="progress progress-xs mt-2">
                              <div class="progress-bar progress-bar-primary" :style="{ 'width': prop.respect + '%' }"></div>
                              </div>
                              </td>
                              <td><span class="badge bg-primary">{{prop.respect}}%</span></td>
                              </tr>
                              </tbody>
                              </table>
                            <hr/>
                            <button style="margin-top: 0.25vw;color:white" @click="getGangzone()" type="button"
                            class="btn btn-primary float-right">{{gangzone.value}} rausnehmen</button>
                          </div>
                          <div class="infoboxtext" v-else>
                            Die Gangzone wird durch keine Gang gehalten!
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
    <div style="height: 100%; background-color: transparent;" v-if="showarrest">
      <div class="row justify-content-center centering34">
        <div class="col-md-12 mt-1 animate__animated animate__bounceInUp">
          <div class="col-md-12 mt-1">
            <div class="box box-default">
              <div class="row">
                <div class="card card-primary card-outline">
                  <div class="card-header" style="font-family: 'Exo', sans-serif; font-size: 1.05vw">
                    <span>Inhaftierung von {{arrestName}}</span>
                    <img style="width: 2vw; z-index:" class="float-right" src="../assets/images/lslogo.png">
                  </div>
                  <div class="card-body" style="max-height:50vh; width: 25vw; overflow-x: auto">
                    <div class="row">
                      <div class="col-md-12">
                        <div style="display: flex; justify-content: center; align-items: center;">
                          <div class="infoboxtext ml-2">
                            <label>Grund:</label>
                            <input type="text" maxlength="64" class="col-md-12 form-control text-center"
                              placeholder="Grund" style="border-radius: 1vw" v-model="arrestInfo[0]">
                          </div>
                        </div>
                        <div style="display: flex; justify-content: center; align-items: center;">
                          <div class="infoboxtext ml-2 mt-3">
                            <label>Zeit:</label>
                            <input type="text" maxlength="4" class="col-md-12 form-control text-center"
                              placeholder="Zeit in Minuten" style="border-radius: 1vw" v-model="arrestInfo[1]">
                          </div>
                        </div>
                        <div style="display: flex; justify-content: center; align-items: center;">
                          <div class="infobt ml-2 mt-3">
                            <label>Zelle:</label>
                            <input type="text" maxlength="1" class="col-md-12 form-control text-center"
                              placeholder="Zellennummer" style="border-radius: 1vw" v-model="arrestInfo[2]">
                          </div>
                        </div>
                        <hr />
                      </div>
                    </div>
                    <div style="display: flex; justify-content: center; align-items: center;">
                      <button style="margin-top: 0.3vw" @click="setArrest()" type="button"
                        class="btn btn-primary">Inhaftieren</button>
                      <button style="display: flex; justify-content: center; align-items: center; margin-top: 0.3vw"
                        @click="hideArrest()" type="button"
                        class="btn btn-secondary ml-3">Abbrechen</button>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div style="height: 100%; background-color: transparent;display:flex" v-if="showdeath">
      <div class="row justify-content-center centering">
        <div class="col-md-12 mt-1 animate__animated animate__bounceInUp">
          <div class="passport" style="margin-top: 32vw">
            <div class="container">
              <div class="text-center"
              style="width: 47vw;max-height:7.55vw;background-color: rgba(0, 0, 0, 0.5);border-radius: 0.5vw;text-shadow: 0 0 0.3px #000">
                <p v-if="respawnTime > 0" style="color:white;font-family: 'Exo', sans-serif;font-size: 1.5vw;text-shadow: 0 0 3px #000;"><strong>Du bist außer Gefecht, Respawn verfügbar in {{getRespawnTime()}} Minuten ...</strong></p>
                <p v-if="respawnTime <= 0" style="color:white;font-family: 'Exo', sans-serif;font-size: 1.5vw;text-shadow: 0 0 3px #000;"><strong>Respawn möglich ...</strong></p>
                <button v-if="respawnTime <= 0" style="font-family: 'Exo', sans-serif;"
                @click="respawn()" type="button" class="btn btn-primary mb-4">Respawn</button>
            </div>
            </div> 
          </div>
        </div>
      </div>
    </div>
    <div style="height: 100%; background-color: transparent;display:flex" v-if="showperso">
      <div class="row justify-content-center centering33">
        <div class="col-md-12 mt-1 animate__animated animate__bounceInUp">
          <div class="passport" style="margin-top: 25vw">
            <div class="container">
              <img src="../assets/images/idcard/bg.svg" style="max-width: 500px; border-radius: .5rem">   
              <div class="persohead" style="font-family: 'Exo', sans-serif;font-size: 20px"><strong>Personalausweis</strong></div>
                <p class="personame" style="font-family: 'Exo', sans-serif;font-size: 12px">Nachname/Lastname</p>
                <p class="personame2" style="color: #cacfcb; font-family: 'Exo', sans-serif;font-size: 12px">{{persoData.name.split(' ')[1]}}</p>
                <p class="personame3" style="font-family: 'Exo', sans-serif;font-size: 12px">Vorname/Firstname</p>
                <p class="personame4" style="color: #cacfcb; font-family: 'Exo', sans-serif;font-size: 12px">{{persoData.name.split(' ')[0]}}</p>
                <p class="personame5" style="font-family: 'Exo', sans-serif;font-size: 12px">Geburtsdatum/Birth of date</p>
                <p class="personame6" style="color: #cacfcb; font-family: 'Exo', sans-serif;font-size: 12px">{{persoData.birthday}}</p>
                <p class="personame7" style="font-family: 'Exo', sans-serif;font-size: 12px">Unterschrift/Signature</p>
                <p class="personame8" style="color: #cacfcb; font-family: 'Alex Brush';font-size: 32px">{{persoData.name}}</p>
                <p class="personame9" style="font-family: 'Exo', sans-serif;font-size: 12px">Augenfarbe/Eyecolor</p>
                <p class="personame10" style="color: #cacfcb; font-family: 'Exo', sans-serif;font-size: 12px">{{persoData.eyecolor}}</p>
                <p class="personame11" style="font-family: 'Exo', sans-serif;font-size: 12px">Wohnort/Residence</p>
                <p class="personame12" style="color: #cacfcb; font-family: 'Exo', sans-serif;font-size: 12px">{{persoData.from}}</p>
                <p class="personame13" style="font-family: 'Exo', sans-serif;font-size: 12px">Körpergröße/Size</p>
                <p class="personame14" style="color: #cacfcb; font-family: 'Exo', sans-serif;font-size: 12px">{{persoData.size}}</p>
                <p class="personame15" style="color: #cacfcb; font-family: 'Exo', sans-serif;font-size: 12px">IDD--NW220001{{persoData.id}}</p>
            </div> 
          </div>
        </div>
      </div>
    </div>
    <div style="height: 100%; background-color: transparent;" v-if="showlic">
      <div class="row justify-content-center centering33">
        <div class="col-md-12 mt-1 animate__animated animate__bounceInUp">
          <div class="passport" style="margin-top: 25vw">
            <div class="container">
              <img src="../assets/images/idcard/bg.svg" style="max-width: 500px; border-radius: .5rem">   
              <div class="persohead" style="font-family: 'Exo', sans-serif;font-size: 20px"><strong>Lizenzen</strong></div>
                <p class="personame" style="font-family: 'Exo', sans-serif;font-size: 12px">Nachname/Lastname</p>
                <p class="personame2" style="color: #cacfcb; font-family: 'Exo', sans-serif;font-size: 12px">{{licData2.name.split(' ')[1]}}</p>
                <p class="personame3" style="font-family: 'Exo', sans-serif;font-size: 12px">Vorname/Firstname</p>
                <p class="personame4" style="color: #cacfcb; font-family: 'Exo', sans-serif;font-size: 12px">{{licData2.name.split(' ')[0]}}</p>
                <p class="personame5" style="font-family: 'Exo', sans-serif;font-size: 12px">Geburtsdatum/Birth of date</p>
                <p class="personame6" style="color: #cacfcb; font-family: 'Exo', sans-serif;font-size: 12px">{{licData2.birthday}}</p>
                <p class="personame7" style="font-family: 'Exo', sans-serif;font-size: 12px">Unterschrift/Signature</p>
                <p class="personame8" style="color: #cacfcb; font-family: 'Alex Brush';font-size: 32px">{{licData2.name}}</p>
                <p v-if="licData[0] != '0'" class="personame99" style="font-family: 'Exo', sans-serif;font-size: 18px">Führerschein</p>
                <p v-else class="personame99" style="color: #cacfcb; font-family: 'Exo', sans-serif;font-size: 18px">Führerschein</p>
                <p v-if="licData[1] != '0'" class="personame99" style="font-family: 'Exo', sans-serif;font-size: 18px;margin-top:30px">Motorradschein</p>
                <p v-else class="personame99" style="color: #cacfcb; font-family: 'Exo', sans-serif;font-size: 18px;margin-top:30px">Motorradschein</p>
                <p v-if="licData[2] != '0'" class="personame99" style="font-family: 'Exo', sans-serif;font-size: 18px;margin-top:60px">Truckerschein</p>
                <p v-else class="personame99" style="color: #cacfcb; font-family: 'Exo', sans-serif;font-size: 18px;margin-top:60px">Truckerschein</p>
                <p v-if="licData[3] != '0'" class="personame99" style="font-family: 'Exo', sans-serif;font-size: 18px;margin-top:90px">Bootsschein</p>
                <p v-else class="personame99" style="color: #cacfcb; font-family: 'Exo', sans-serif;font-size: 18px;margin-top:90px">Bootsschein</p>
                <p v-if="licData[4] != '0'" class="personame99" style="font-family: 'Exo', sans-serif;font-size: 18px;margin-top:120px">Flugschein</p>
                <p v-else class="personame99" style="color: #cacfcb; font-family: 'Exo', sans-serif;font-size: 18px;margin-top:120px">Flugschein</p>
                <p v-if="licData[5] != '0'" class="personame99" style="font-family: 'Exo', sans-serif;font-size: 18px;margin-top:150px">Waffenschein</p>
                <p v-else class="personame99" style="color: #cacfcb; font-family: 'Exo', sans-serif;font-size: 18px;margin-top:150px">Waffenschein</p>
            </div> 
          </div>
        </div>
      </div>
    </div>
    <div style="height: 100%; background-color: transparent;" v-if="showinfobox">
      <div class="row justify-content-center centering3">
        <div class="col-md-12 mt-1 animate__animated animate__bounceInUp">
          <div class="col-md-12 mt-1">
            <div class="box box-default">
              <div class="row">
                <div class="card card-primary card-outline">
                  <div class="card-header" style="font-family: 'Exo', sans-serif; font-size: 1.2vw">
                    {{infoboxtextheader}}
                  </div>
                  <div class="card-body" style="max-height:50vh; width: 25vw; overflow-x: auto">
                    <div class="row">
                      <div class="col-md-12">
                        <div class="card2 card card-primary card-outline"
                          v-if="infoboxtextheader1 || infoboxtextheader2 || infoboxtextheader3 || infoboxtextheader4">
                          <div class="infoboxtext ml-2">
                            <span><strong><u>Informationen:</u></strong></span>
                          </div>
                          <div class="infoboxtext ml-2" v-if="infoboxtextheader1 && infoboxtextheader1 != 'null'">
                            {{infoboxtextheader1}}
                          </div>
                          <div class="infoboxtext ml-2" v-if="infoboxtextheader2 && infoboxtextheader2 != 'null'">
                            {{infoboxtextheader2}}
                          </div>
                          <div class="infoboxtext ml-2" v-if="infoboxtextheader3 && infoboxtextheader3 != 'null'">
                            {{infoboxtextheader3}}
                          </div>
                          <div class="infoboxtext ml-2" v-if="infoboxtextheader4 && infoboxtextheader4 != 'null'">
                            {{infoboxtextheader4}}
                          </div>
                        </div>
                        <div class="card2 card card-primary card-outline">
                          <div class="infoboxtext ml-2" v-if="infoboxtestmodus == 0">
                            <span><strong><u>Steuerung:</u></strong></span>
                          </div>
                          <div class="infoboxtext ml-2">
                            {{infoboxtext1}}
                          </div>
                          <div class="infoboxtext ml-2">
                            {{infoboxtext2}}
                          </div>
                          <div class="infoboxtext ml-2">
                            {{infoboxtext3}}
                          </div>
                          <div class="infoboxtext ml-2">
                            {{infoboxtext4}}
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
    <div style="height: 100%; background-color: transparent;" v-if="ammunationshow">
      <div class="row justify-content-center centering5">
        <div class="col-md-12 mt-1 ">
          <div class="col-md-12 mt-1">
            <div class="box box-default">
              <div class="row animate__animated animate__bounceInUp">
                <div class="card card-primary card-outline">
                  <div class="card-header" style="font-family: 'Exo', sans-serif; font-size: 1.2vw" v-if="ammuCheck == 0">
                    Ammunation
                  </div>
                  <div class="card-header" style="font-family: 'Exo', sans-serif; font-size: 1.2vw" v-else>
                    Waffenkomponenten
                  </div>
                  <div class="card-body" style="max-height:65vh; width: 27.5vw; overflow-x: auto">
                    <h5>Komponente auswählen:</h5>
                    <div class="row mt-3">
                      <div class="col-md-12">
                        <div style="display: flex; justify-content: center; align-items: center;">
                          <img src="../assets/images/ammunation/Ammu-1.png"
                            :class="[(selectedWeapon == 'Tint') ? 'clothiconactive mr-2':'clothicon mr-2']"
                            @click="selectWeapon('Tint')">
                          <img src="../assets/images/ammunation/Ammu-2.png"
                            :class="[(selectedWeapon == 'Components') ? 'clothiconactive mr-2':'clothicon mr-2']"
                            @click="selectWeapon('Components')">
                        </div>
                      </div>
                    </div>
                    <hr v-if="selectedWeapon">
                    <div v-if="selectedWeapon == 'Tint'">
                      Waffenfarbton auswählen: <span class="badge badge-secondary">{{weaponNames[weaponNumber]}}</span>
                      <span class="ml-2 badge badge-primary">{{ammuPrice1}}$</span>
                      <vue-range-slider
                        v-if="!weaponItem.description || !weaponItem.description.toLowerCase().incluides('mk2')"
                        ref="slider" tooltip="false" dotSize="14" height="13" :min="0" max="7" value='0' :step="1"
                        v-model="weaponNumber" v-oninput="weaponPreview(1, weaponNumber)" />
                      <vue-range-slider v-else ref="slider" tooltip="false" dotSize="14" height="13" :min="0" max="31"
                        value='0' :step="1" v-model="weaponNumber" v-oninput="weaponPreview(1, weaponNumber)" />
                    </div>
                    <div v-if="selectedWeapon == 'Components'">
                      Waffenkomponente auswählen: <span
                        class="badge badge-secondary">{{weaponComponents[weaponNumber2].Name}}</span>
                      <span class="ml-2 badge badge-primary">{{ammuPrice2}}$</span>
                      <vue-range-slider ref="slider" tooltip="false" dotSize="14" height="13" :min="0"
                        :max="weaponComponents.length > 1 ? weaponComponents.length-1 : 1" value='0' :step="1"
                        v-model="weaponNumber2" v-oninput="weaponPreview(2, weaponComponents[weaponNumber2].HashKey)" />
                      <div style="display: flex; justify-content: center; align-items: center;">
                        <span>{{weaponComponents[weaponNumber2].Description}}</span>
                      </div>
                    </div>
                    <hr />
                    <div v-if="selectedWeapon == 'Tint'">
                      <div v-if="ammuCheck == 0">
                        <div style="display: flex; justify-content: center; align-items: center;">
                          <button style="display: flex; justify-content: center; align-items: center; margin-top: 0.3vw"
                            @click="setWeaponComponent(1,weaponNumber, 0)" type="button" class="btn btn-success">Farbton
                            kaufen (Bar)</button>
                        </div>
                        <div style="display: flex; justify-content: center; align-items: center;">
                          <button style="display: flex; justify-content: center; align-items: center; margin-top: 0.3vw"
                            @click="setWeaponComponent(1,weaponNumber, 1)" type="button" class="btn btn-success">Farbton
                            kaufen (EC-Karte)</button>
                        </div>
                        <div style="display: flex; justify-content: center; align-items: center;">
                          <button style="display: flex; justify-content: center; align-items: center; margin-top: 0.3vw"
                            @click="resetWeaponComponent(1)" type="button" class="btn btn-warning">Farbton
                            reseten</button>
                        </div>
                      </div>
                      <div v-if="ammuCheck == 1">
                        <div style="display: flex; justify-content: center; align-items: center;">
                          <button style="display: flex; justify-content: center; align-items: center; margin-top: 0.3vw"
                            @click="setWeaponComponent(1,weaponNumber, 3)" type="button" class="btn btn-success">Farbton
                            auswählen</button>
                        </div>
                        <div style="display: flex; justify-content: center; align-items: center;">
                          <button style="display: flex; justify-content: center; align-items: center; margin-top: 0.3vw"
                            @click="resetWeaponComponent(1)" type="button" class="btn btn-warning">Farbton
                            reseten</button>
                        </div>
                      </div>
                    </div>
                    <div v-else>
                      <div v-if="ammuCheck == 0">
                        <div v-if="!this.compNow.includes(weaponComponents[weaponNumber2].HashKey)">
                          <div style="display: flex; justify-content: center; align-items: center;">
                            <button style="display: flex; justify-content: center; align-items: center; margin-top: 0.3vw"
                              @click="setWeaponComponent(2,weaponComponents[weaponNumber2].HashKey, 0)" type="button"
                              class="btn btn-success">Komponente kaufen (Bar)</button>
                          </div>
                          <div style="display: flex; justify-content: center; align-items: center;">
                            <button style="display: flex; justify-content: center; align-items: center; margin-top: 0.3vw"
                              @click="setWeaponComponent(2,weaponComponents[weaponNumber2].HashKey, 1)" type="button"
                              class="btn btn-success">Komponente kaufen (EC-Karte)</button>
                          </div>
                        </div>
                        <div v-else>
                          <div style="display: flex; justify-content: center; align-items: center;">
                            <button style="display: flex; justify-content: center; align-items: center; margin-top: 0.3vw"
                              @click="setWeaponComponent(3,weaponComponents[weaponNumber2].HashKey, 0)" type="button"
                              class="btn btn-danger">Komponente entfernen</button>
                          </div>
                        </div>
                        <div style="display: flex; justify-content: center; align-items: center;">
                          <button style="display: flex; justify-content: center; align-items: center; margin-top: 0.3vw"
                            @click="resetWeaponComponent(2)" type="button" class="btn btn-warning">Komponenten
                            reseten</button>
                        </div>
                      </div>
                      <div v-if="ammuCheck == 1">
                        <div v-if="!this.compNow.includes(weaponComponents[weaponNumber2].HashKey)">
                          <div style="display: flex; justify-content: center; align-items: center;">
                            <button style="display: flex; justify-content: center; align-items: center; margin-top: 0.3vw"
                              @click="setWeaponComponent(2,weaponComponents[weaponNumber2].HashKey, 3)" type="button"
                              class="btn btn-success">Komponente auswählen</button>
                          </div>
                        </div>
                        <div v-else>
                          <div style="display: flex; justify-content: center; align-items: center;">
                            <button style="display: flex; justify-content: center; align-items: center; margin-top: 0.3vw"
                              @click="setWeaponComponent(3,weaponComponents[weaponNumber2].HashKey, 0)" type="button"
                              class="btn btn-danger">Komponente entfernen</button>
                          </div>
                        </div>
                        <div style="display: flex; justify-content: center; align-items: center;">
                          <button style="display: flex; justify-content: center; align-items: center; margin-top: 0.3vw"
                            @click="resetWeaponComponent(2)" type="button" class="btn btn-warning">Komponenten
                            reseten</button>
                        </div>
                      </div>
                    </div>
                  <hr>
                    <span class="text-center" style="display: flex; justify-content: center; align-items: center;">Benutze
                      Taste [K] um die
                      Kamera frei bewegen zu können!</span>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div style="height: 100%; background-color: transparent;" v-if="tuningshow">
      <div class="row justify-content-center centering5">
        <div class="col-md-12 mt-1 animate__animated animate__bounceInUp">
          <div class="col-md-12 mt-1">
            <div class="box box-default">
              <div class="row">
                <div class="card card-primary card-outline">
                  <div class="card-header" style="font-family: 'Exo', sans-serif; font-size: 1.2vw">
                    Tuning
                    <i v-if="perlmutt1 && selectedTuning == 'Farbe 1'" @click="setPerlmutt()"
                      class="fa-solid fa-sun float-right"
                      style="color:#3F6791; text-shadow: 1px 0 0 #000, 0 -1px 0 #000, 0 1px 0 #000, -1px 0 0 #000;"></i>
                    <i v-if="!perlmutt1 && selectedTuning == 'Farbe 1'" @click="setPerlmutt()"
                      class="fa-solid fa-sun float-right"
                      style="color:#FFFFFF; text-shadow: 1px 0 0 #000, 0 -1px 0 #000, 0 1px 0 #000, -1px 0 0 #000;"></i>
                    <i v-if="perlmutt1 && componentNumbers[69] != 255 && selectedTuning == 'Farbe 1'"
                      @click="resetPerlmutt()" class="fa-solid fa-ban float-right mr-2"
                      style="color:#FFFFFF; text-shadow: 1px 0 0 #000, 0 -1px 0 #000, 0 1px 0 #000, -1px 0 0 #000;"></i>
                  </div>
                  <div class="card-body" style="max-height:65vh; width: 27.5vw; overflow-x: auto">
                    <h5>Tuningkomponente auswählen:</h5>
                    <div class="row mt-3">
                      <div class="col-md-12">
                        <div style="display: flex; justify-content: center; align-items: center;">
                          <img src="../assets/images/tuning/1.png"
                            :class="[(selectedTuning == 'Farbe 1') ? 'clothiconactive mr-2':'clothicon mr-2']"
                            @click="selectTuning('Farbe 1')">
                          <img src="../assets/images/tuning/1.png"
                            :class="[(selectedTuning == 'Farbe 2') ? 'clothiconactive mr-2':'clothicon mr-2']"
                            @click="selectTuning('Farbe 2')">
                          <img src="../assets/images/tuning/5.png"
                            :class="[(selectedTuning == 'Farbe 3') ? 'clothiconactive mr-2':'clothicon mr-2']"
                            @click="selectTuning('Farbe 3')">
                          <img src="../assets/images/tuning/2.png"
                            :class="[(selectedTuning == 'Tuning 1') ? 'clothiconactive mr-2':'clothicon mr-2']"
                            @click="selectTuning('Tuning 1')">
                          <img src="../assets/images/tuning/3.png"
                            :class="[(selectedTuning == 'Tuning 2') ? 'clothiconactive mr-2':'clothicon mr-2']"
                            @click="selectTuning('Tuning 2')">
                          <img src="../assets/images/tuning/4.png"
                            :class="[(selectedTuning == 'Tuning 3') ? 'clothiconactive mr-2':'clothicon mr-2']"
                            @click="selectTuning('Tuning 3')">
                          <img src="../assets/images/tuning/6.png"
                            :class="[(selectedTuning == 'Tuning 4') ? 'clothiconactive mr-2':'clothicon mr-2']"
                            @click="selectTuning('Tuning 4')">
                          <img src="../assets/images/tuning/7.png"
                            :class="[(selectedTuning == 'Analyse') ? 'clothiconactive mr-2':'clothicon mr-2']"
                            @click="selectTuning('Analyse')">
                        </div>
                      </div>
                    </div>
                    <hr v-if="selectedTuning">
                    <div class="hair"
                      v-if="selectedTuning == 'Farbe 1' || selectedTuning == 'Farbe 2' || selectedTuning == 'Farbe 3'">
                      <span class="badge badge-primary mb-3 ml-1">8 Tuningteile</span>
                      <ul class="vehicleColorsList" v-if="selectedTuning == 'Farbe 1'">
                        <li v-for="n in 159" :key="n" :style="{ background: vehicleColors[n].Hex }"
                          @click="tuningPreview(66, n)">
                        </li>
                      </ul>
                      <ul class="vehicleColorsList" v-if="selectedTuning == 'Farbe 2'">
                        <li v-for="n in 159" :key="n" :style="{ background: vehicleColors[n].Hex }"
                          @click="tuningPreview(67, n)">
                        </li>
                      </ul>
                      <ul class="vehicleColorsList" v-if="selectedTuning == 'Farbe 3'">
                        <li v-for="n in 159" :key="n" :style="{ background: vehicleColors[n].Hex }"
                          @click="tuningPreview(68, n)">
                        </li>
                      </ul>
                    </div>
                    <div v-if="selectedTuning == 'Tuning 1'">
                      <div v-if="checkTuning(1) == 1">
                        <div v-for="(component, index) in tuningComponents" :key="index">
                          <div v-if="component != 'n/A' && index <= 10">
                            <span v-if="maxTuning[index] > -1">{{component}} <span
                                class="badge badge-secondary">{{componentNumbers[index]}}</span><span
                                class="badge badge-primary ml-1 mr-1">8 Tuningteile</span></span>
                            <vue-range-slider v-if="maxTuning[index] > -1" ref="slider" tooltip="false" dotSize="14"
                              height="13" :min="-1" :max="maxTuning[index]" value='0' :step="1"
                              v-model="componentNumbers[index]"
                              v-oninput="tuningPreview(index, componentNumbers[index])" />
                          </div>
                        </div>
                      </div>
                      <div v-else>
                        <span style="display: flex; justify-content: center; align-items: center;">Keine kompatiblen
                          Tuningteile vorhanden!</span>
                      </div>
                    </div>
                    <div v-if="selectedTuning == 'Tuning 2'">
                      <div v-if="checkTuning(2) == 1">
                        <div v-for="(component, index2) in tuningComponents" :key="index2">
                          <div v-if="component != 'n/A' && index2 > 10 && index2 <= 24">
                            <span v-if="maxTuning[index2] > -1 && component != 'Panzerung'">{{component}} <span
                                class="badge badge-secondary">{{getComponentName(index2)}}</span><span
                                class="badge badge-primary ml-1 mr-1">{{tuningCost[index2]}} Tuningteile</span></span>
                            <vue-range-slider v-if="maxTuning[index2] > -1 && component != 'Panzerung'" ref="slider"
                              tooltip="false" dotSize="14" height="13" :min="-1" :max="maxTuning[index2]" value='0'
                              :step="1" v-model="componentNumbers[index2]"
                              v-oninput="tuningPreview(index2, componentNumbers[index2])" />
                          </div>
                        </div>
                      </div>
                      <div v-else>
                        <span style="display: flex; justify-content: center; align-items: center;">Keine kompatiblen
                          Tuningteile vorhanden!</span>
                      </div>
                    </div>
                    <div v-if="selectedTuning == 'Tuning 3'">
                      <div v-if="checkTuning(3) == 1">
                        <div v-for="(component, index3) in tuningComponents" :key="index3">
                          <div v-if="component != 'n/A' && index3 > 24">
                            <span v-if="maxTuning[index3] > -1">{{component}} <span
                                class="badge badge-secondary">{{getComponentName(index3)}}</span><span
                                class="badge badge-primary ml-1 mr-1">{{tuningCost[index3]}} Tuningteile</span></span>
                            <vue-range-slider v-if="maxTuning[index3] > -1" ref="slider" tooltip="false" dotSize="14"
                              height="13" :min="-1" :max="maxTuning[index3]" value='0' :step="1"
                              v-model="componentNumbers[index3]"
                              v-oninput="tuningPreview(index3, componentNumbers[index3])" />
                          </div>
                        </div>
                      </div>
                      <div v-else>
                        <span style="display: flex; justify-content: center; align-items: center;">Keine kompatiblen
                          Tuningteile vorhanden!</span>
                      </div>
                    </div>
                    <div v-if="selectedTuning == 'Tuning 4'">
                      <span>Turbo PSI <span
                          class="badge badge-secondary">{{parseFloat(componentNumbers[58]).toFixed(2)}}</span><span
                          class="badge badge-primary ml-1 mr-1">{{tuningCost[58]}} Tuningteile</span></span>
                      <vue-range-slider ref="slider" tooltip="false" dotSize="14" height="13" :min="0.01" :max="2.0"
                        value='0.25' :step="0.01" v-model="componentNumbers[58]"
                        v-oninput="tuningPreview(58, componentNumbers[58])" />
                      <span>Zündzeitpunkt <span
                          class="badge badge-secondary">{{parseFloat(componentNumbers[59]).toFixed(2)}}</span><span
                          class="badge badge-primary ml-1 mr-1">{{tuningCost[59]}} Tuningteile</span></span>
                      <vue-range-slider ref="slider" tooltip="false" dotSize="14" height="13" :min="0.01" :max="2.0"
                        value='1.3' :step="0.01" v-model="componentNumbers[59]"
                        v-oninput="tuningPreview(59, componentNumbers[59])" />
                      <span>Transmission <span
                          class="badge badge-secondary">{{parseFloat(componentNumbers[60]).toFixed(2)}}</span><span
                          class="badge badge-primary ml-1 mr-1">{{tuningCost[60]}} Tuningteile</span></span>
                      <vue-range-slider ref="slider" tooltip="false" dotSize="14" height="13" :min="1" :max="50"
                        value='10' :step="1" v-model="componentNumbers[60]"
                        v-oninput="tuningPreview(60, componentNumbers[60])" />
                      <span>Bremskraft (R/F) <span
                          class="badge badge-secondary">{{parseFloat(componentNumbers[61]).toFixed(2)}}</span><span
                          class="badge badge-primary ml-1 mr-1">{{tuningCost[61]}} Tuningteile</span></span>
                      <vue-range-slider ref="slider" tooltip="false" dotSize="14" height="13" :min="0.0" :max="2"
                        value='0.5' :step="0.01" v-model="componentNumbers[61]"
                        v-oninput="tuningPreview(61, componentNumbers[61])" />
                      <span>Bremskraft (W/S) <span
                          class="badge badge-secondary">{{parseFloat(componentNumbers[62]).toFixed(2)}}</span><span
                          class="badge badge-primary ml-1 mr-1">{{tuningCost[62]}} Tuningteile</span></span>
                      <vue-range-slider ref="slider" tooltip="false" dotSize="14" height="13" :min="0.01" :max="2"
                        value='1.4' :step="0.01" v-model="componentNumbers[62]"
                        v-oninput="tuningPreview(62, componentNumbers[62])" />
                      <span>Laufwerksvorspannung <span
                          class="badge badge-secondary">{{parseFloat(componentNumbers[63]).toFixed(2)}}</span><span
                          class="badge badge-primary ml-1 mr-1">{{tuningCost[63]}} Tuningteile</span></span>
                      <vue-range-slider ref="slider" tooltip="false" dotSize="14" height="13" :min="0.00" :max="1"
                        value='0.5' :step="0.01" v-model="componentNumbers[63]"
                        v-oninput="tuningPreview(63, componentNumbers[63])" />
                    </div>
                    <div v-if="selectedTuning == 'Analyse'">
                      <div style="display: flex; justify-content: center; align-items: center; font-size: 1.3vw">
                        <span>Benötigte Tuningteile: {{tuningCosts}}</span>
                      </div>
                      <div style="display: flex; justify-content: center; align-items: center; font-size: 1.3vw">
                        <span>Vorhandene Tuningteile: {{stockTuning}}</span>
                      </div>
                      <hr />
                      <div style="display: flex; justify-content: center; align-items: center;">
                        <button style="display: flex; justify-content: center; align-items: center; margin-top: 0.3vw"
                          type="button" @click="syncTuningFunc(1)" class="btn btn-warning">Tuning
                          Synchronisieren</button>
                      </div>
                      <div style="display: flex; justify-content: center; align-items: center;">
                        <button style="display: flex; justify-content: center; align-items: center; margin-top: 0.3vw"
                          @click="resetTuning(1)" type="button" class="btn btn-danger">Tuning reseten</button>
                      </div>
                      <div style="display: flex; justify-content: center; align-items: center;">
                        <button style="display: flex; justify-content: center; align-items: center; margin-top: 0.3vw"
                          @click="setTuning()" type="button" class="btn btn-success">Tuning anbringen</button>
                      </div>
                    </div>
                    <hr>
                    <span style="display: flex; justify-content: center; align-items: center;">Benutze Taste [K] um die
                      Kamera frei bewegen zu können!</span>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
       <div style="height: 100%; background-color: transparent;" v-if="busPlanShow">
      <div class="row justify-content-center centering4">
        <div class="col-md-12 mt-1 animate__animated animate__bounceInUp">
          <div class="col-md-12 mt-1">
            <div class="box box-default">
              <div class="row">
                <div class="card card-primary card-outline">
                  <div class="card-header" style="font-family: 'Exo', sans-serif; font-size: 1.2vw">
                    Fahrplan - {{routeName}}
                  </div>
                  <div class="card-body" style="max-height:60vh; width: 27.5vw; overflow-x: auto">
                    <div v-for="station in allStations" :key="station" style="display: flex; justify-content: center; align-items: center;">
                      <h6 v-if="station == stationName" style="color:#3F6791">{{station}}</h6>
                      <h6 v-else>{{station}}</h6>
                    </div>
                    <hr>
                    <span style="display: flex; justify-content: center; align-items: center;">Aktive Busfahrer auf dieser Route: {{busDriver}}</span>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div style="height: 100%; background-color: transparent;" v-if="tattooshow">
      <div class="row justify-content-center centering4">
        <div class="col-md-12 mt-1 animate__animated animate__bounceInUp">
          <div class="col-md-12 mt-1">
            <div class="box box-default">
              <div class="row">
                <div class="card card-primary card-outline">
                  <div class="card-header" style="font-family: 'Exo', sans-serif; font-size: 1.2vw">
                    Tattooladen
                  </div>
                  <div class="card-body" style="max-height:50vh; width: 28.7vw; overflow-x: auto">
                    <h5>Tattoo auswählen:</h5>
                    <div class="row mt-3">
                      <div class="col-md-12">
                        <div style="display: flex; justify-content: center; align-items: center;">
                          <img src="../assets/images/tattoo.png"
                            :class="[(selectTattoo == 'Tattoo1') ? 'clothiconactive mr-2':'clothicon mr-2']"
                            @click="tattooSelect('Tattoo1')">
                        </div>
                        <div v-if="selectTattoo == 'Tattoo1'"
                          style="display: flex; justify-content: center; align-items: center;" class="mt-3">
                          <button type="button" @click="selectTattoCheck(-1)"
                            :class="[(zoneId == -1) ? 'active btn btn-primary btn-sm mr-1':'btn btn-primary btn-sm mr-1']"
                            style="display: flex; justify-content: center; align-items: center;">Alle</button>
                          <button type="button" @click="selectTattoCheck(0)"
                            :class="[(zoneId == 0) ? 'active btn btn-primary btn-sm mr-1':'btn btn-primary btn-sm mr-1']">Körper</button>
                          <button type="button" @click="selectTattoCheck(1)"
                            :class="[(zoneId == 1) ? 'active btn btn-primary btn-sm mr-1':'btn btn-primary btn-sm mr-1']">Kopf</button>
                          <button type="button" @click="selectTattoCheck(2)"
                            :class="[(zoneId == 2) ? 'active btn btn-primary btn-sm mr-1':'btn btn-primary btn-sm mr-1']">L.
                            Arm</button>
                          <button type="button" @click="selectTattoCheck(3)"
                            :class="[(zoneId == 3) ? 'active btn btn-primary btn-sm mr-1':'btn btn-primary btn-sm mr-1']">R.
                            Arm</button>
                          <button type="button" @click="selectTattoCheck(4)"
                            :class="[(zoneId == 4) ? 'active btn btn-primary btn-sm mr-1':'btn btn-primary btn-sm mr-1']">L.
                            Bein</button>
                          <button type="button" @click="selectTattoCheck(5)"
                            :class="[(zoneId == 5) ? 'active btn btn-primary btn-sm mr-1':'btn btn-primary btn-sm mr-1']">R.
                            Bein</button>
                        </div>
                      </div>
                    </div>
                  <div>
                    <hr />
                    <div v-if="selectTattoo == 'Tattoo1'">
                      <h5>Tattooauswahl ({{tattooJson[selectedclothid].LocalizedName}} - {{selectedclothid}}):</h5>
                      <vue-range-slider ref="slider" tooltip="false" dotSize="14" height="13" :min="0"
                        :max="tattooJson.length-1" value='0' :step="1" v-model="selectedclothid"
                        v-oninput="changeTattoo()" />
                      <hr />
                    </div>
                    <div style="display: flex; justify-content: center; align-items: center;" class="mt-2">
                      <button type="button" v-if="selectedclothid >= 0" class="btn btn-success mr-1"
                        @click="buyTattoo(selectedclothid)">Tattoo stechen/entfernen -
                        {{parseInt(750*multiplier)}}$</button>
                      <button type="button" v-if="selectedclothid >= 0" class="btn btn-danger mr-1"
                        @click="resetTattoo()">Entfernen - {{parseInt(1250*multiplier)}}$</button>
                      <button type="button" class="btn btn-secondary" @click="abortTattoo()">Abbrechen</button>
                    </div>
                    <hr>
                    <span class="text-center" style="display: flex; justify-content: center; align-items: center;">Benutze
                      Taste [K] um die
                      Kamera frei bewegen zu können, ebenfalls kannst du mit der rechten/linken Pfeiltaste die Auswahl steuern!</span>
                  </div>
                </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div style="height: 100%; background-color: transparent;" v-if="barbershow">
      <div class="row justify-content-center centering4">
        <div class="col-md-12 mt-1 animate__animated animate__bounceInUp">
          <div class="col-md-12 mt-1">
            <div class="box box-default">
              <div class="row">
                <div class="card card-primary card-outline">
                  <div class="card-header" style="font-family: 'Exo', sans-serif; font-size: 1.2vw" v-if="bizzid != 38">
                    Barber Shop
                  </div>
                  <div class="card-header" style="font-family: 'Exo', sans-serif; font-size: 1.2vw" v-if="bizzid == 38">
                    Kosmetik & Friseurladen
                  </div>
                  <div class="card-body" style="max-height:54vh; width: 27.5vw; overflow-x: auto">
                    <h5>Dienstleistung auswählen:</h5>
                    <div class="row mt-3">
                      <div class="col-md-12">
                        <div style="display: flex; justify-content: center; align-items: center;">
                          <img src="../assets/images/haare.png"
                            :class="[(selectedBarber == 'Haare') ? 'clothiconactive mr-2':'clothicon mr-2']"
                            @click="selectedBarber = 'Haare'">
                          <img src="../assets/images/haare.png"
                            :class="[(selectedBarber == 'Haare2') ? 'clothiconactive mr-2':'clothicon mr-2']"
                            @click="selectedBarber = 'Haare2'">
                          <img src="../assets/images/kamm.png" v-if="gender == 1"
                            :class="[(selectedBarber == 'Bart') ? 'clothiconactive mr-2':'clothicon mr-2']"
                            @click="selectedBarber = 'Bart'">
                          <img v-if="bizzid == 38" src="../assets/images/makeup.png"
                            :class="[(selectedBarber == 'Kosmetik') ? 'clothiconactive mr-2':'clothicon mr-2']"
                            @click="selectedBarber = 'Kosmetik'">
                        </div>
                      </div>
                    </div>
                    <hr />
                    <div v-if="selectedBarber == 'Haare'">
                      <h5>Frisurenauswahl ({{selectedclothid}}):</h5>
                      <vue-range-slider ref="slider" tooltip="false" dotSize="14" height="13" :min="0" :max="maxhair"
                        value='0' :step="1" v-model="selectedclothid"
                        v-oninput="hairCustomize(1, selectedclothcolor)" />
                      <hr />
                      <div class="hair">
                        <ul class="hairColors-1">
                          <li v-for="(hairColor, index) in hairColors" :key="hairColor"
                            :style="{ background: hairColor }" v-on:click="hairCustomize(1, index)">
                          </li>
                        </ul>
                      </div>
                    </div>
                    <div v-if="selectedBarber == 'Haare2'">
                      <h5>Haartönung:</h5>
                      <hr />
                      <div class="hair">
                        <ul class="hairColors-1">
                          <li v-for="(hairColor, index) in hairColors" :key="hairColor"
                            :style="{ background: hairColor }" v-on:click="hairCustomize(4, index)">
                          </li>
                        </ul>
                      </div>
                      <hr />
                    </div>
                    <div v-if="selectedBarber == 'Bart'">
                      <h5>Bartauswahl ({{selectedclothid2}}):</h5>
                      <vue-range-slider ref="slider" tooltip="false" dotSize="14" height="13" :min="-1" :max="28"
                        value='0' :step="1" v-model="selectedclothid2"
                        v-oninput="hairCustomize(2, selectedclothcolor2)" />
                      <hr />
                      <div class="beard">
                        <ul class="beardColors">
                          <li v-for="beardColor in beardColors" :key="beardColor"
                            :style="{ background: hairColors[beardColor] }" v-on:click="hairCustomize(2, beardColor)">
                          </li>
                        </ul>
                      </div>
                    </div>
                    <div v-if="selectedBarber == 'Kosmetik'">
                      <h5>Augenbraunauswahl ({{headOverlays[0]}}):</h5>
                      <vue-range-slider ref="slider" tooltip="false" dotSize="14" height="13" :min="-1" :max="33"
                        value='0' :step="1" v-model="headOverlays[0]"
                        v-oninput="miscCustomize(3)" />
                      <vue-range-slider ref="slider" tooltip="false" dotSize="14" height="13" :min="0" :max="63"
                        value='0' :step="1" v-model="headOverlaysColors[0]"
                        v-oninput="miscCustomize(3)" />
                      <h5 class="mt-2">Make-Up Auswahl ({{headOverlays[1]}}):</h5>
                      <vue-range-slider ref="slider" tooltip="false" dotSize="14" height="13" :min="-1" :max="74"
                        value='0' :step="1" v-model="headOverlays[1]"
                        v-oninput="miscCustomize(4)" />
                      <vue-range-slider ref="slider" tooltip="false" dotSize="14" height="13" :min="0" :max="63"
                        value='0' :step="1" v-model="headOverlaysColors[1]"
                        v-oninput="miscCustomize(4)" />
                      <h5 class="mt-2">Lippenstift Auswahl ({{headOverlays[2]}}):</h5>
                      <vue-range-slider ref="slider" tooltip="false" dotSize="14" height="13" :min="-1" :max="9"
                        value='0' :step="1" v-model="headOverlays[2]"
                        v-oninput="miscCustomize(5, headOverlaysColors[2])" />
                      <vue-range-slider ref="slider" tooltip="false" dotSize="14" height="13" :min="0" :max="63"
                        value='0' :step="1" v-model="headOverlaysColors[2]"
                        v-oninput="miscCustomize(5)" />
                      <hr />
                    </div>
                    <div style="display: flex; justify-content: center; align-items: center;" class="mt-2">
                      <button v-if="getBarberPrice() > 0" type="button"
                        class="btn btn-success mr-1" @click="buyHair()">Dienstleistung durchführen - ({{getBarberPrice()}}$)</button>
                      <button type="button"
                        class="btn btn-danger mr-1" @click="resetHair()">Reset</button>
                      <button type="button" class="btn btn-secondary" @click="abortHair()">Abbrechen</button>
                    </div>
                    <hr>
                    <span class="text-center" style="display: flex; justify-content: center; align-items: center;" v-if="selectedBarber == 'Kosmetik'">Benutze
                      Taste [K] um die
                      Kamera frei bewegen zu können!</span>
                    <span class="text-center" style="display: flex; justify-content: center; align-items: center;" v-if="selectedBarber != 'Kosmetik'">Benutze
                      Taste [K] um die
                      Kamera frei bewegen zu können, ebenfalls kannst du mit der rechten/linken Pfeiltaste die Auswahl steuern!</span>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div style="height: 100%; background-color: transparent;" v-if="clothshow3">
      <div class="row justify-content-center centering4">
        <div class="col-md-12 mt-1 animate__animated animate__bounceInUp">
          <div class="col-md-12 mt-1">
            <div class="box box-default">
              <div class="row">
                <div class="card card-primary card-outline">
                  <div class="card-header" style="font-family: 'Exo', sans-serif; font-size: 1.2vw">
                    Maskenhändler
                  </div>
                  <div class="card-body" style="max-height:50vh; width: 27.5vw; overflow-x: auto">
                    <div class="row mt-3">
                      <div class="col-md-12">
                        <div style="display: flex; justify-content: center; align-items: center;">
                          <img src="../assets/images/clothshop/maske.png"
                            :class="[(selectedcloth == 'Maske') ? 'clothiconactive mr-2':'clothicon mr-2']"
                            @click="selectCloth('Maske')">
                        </div>
                      </div>
                    </div>
                    <hr v-if="selectedcloth != 'n/A'" />
                    <h5 v-if="selectedcloth != 'n/A'">{{selectedcloth}}nauswahl ({{selectedclothid}}):</h5>
                    <vue-range-slider
                      v-if="selectedcloth != 'n/A' && selectedcloth != 'Torso'"
                      ref="slider" tooltip="false" dotSize="14" height="13" :min="0" :max="maxcloth" value='0'
                      :step="1" v-model="selectedclothid" v-oninput="clothingCustomize(1)" />
                    <h5 class="mt-1" v-if="selectedcloth != 'n/A' && maxclothColor > 0">{{selectedcloth}}nvariante ({{selectedclothcolor}}):
                    </h5>
                    <vue-range-slider
                      v-if="selectedcloth != 'n/A' && maxclothColor > 1"
                      ref="slider" tooltip="false" dotSize="14" height="13" :min="0" :max="maxclothColor" value='0' :step="1"
                      v-model="selectedclothcolor" v-oninput="clothingCustomize(2)" />
                    <hr v-if="selectedcloth != 'n/A'" />
                    <div v-if="selectedcloth != 'n/A'"
                      style="display: flex; justify-content: center; align-items: center;">
                      <h5>Aktuelle Kosten: {{clothcost}}$</h5>
                    </div>
                    <div style="display: flex; justify-content: center; align-items: center;" class="mt-2">
                      <button v-if="selectedcloth != 'n/A' && clothcost > 1" type="button" class="btn btn-success mr-1"
                        @click="buyCloth()">Maske kaufen</button>
                      <button v-if="selectedcloth != 'n/A'" type="button" class="btn btn-danger mr-1"
                        @click="resetCloth()">Reset</button>
                      <button type="button" class="btn btn-secondary" @click="abortCloth()">Abbrechen</button>
                    </div>
                    <hr>
                    <span class="text-center" style="display: flex; justify-content: center; align-items: center;">Benutze die rechte/linke Pfeiltaste um die Auswahl steuern!</span>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div style="height: 100%; background-color: transparent;" v-if="showwardrobe">
      <div class="row justify-content-center centering4">
        <div class="col-md-12 mt-1 animate__animated animate__bounceInUp">
          <div class="card card-primary card-outline">
            <div class="card-header" style="font-family: 'Exo', sans-serif; font-size: 1.2vw">
              Kleiderschrank
            </div>
            <div class="card-body" style="max-height:75vh; width: 27.5vw; overflow-x: auto">
              <div>
                  <div>
                    <div v-for="(wdrobe, index) in wardrobe" :key="index">
                      <div class="mt-1 col-md-12 col-sm-12 col-xs-12">
                        <button @click="selectOutfitW(wdrobe)" type="button" class="btn btn-primary btn-block"> <i class="fa-solid fa-shirt mr-3"
                            style="color:white; text-shadow: 1px 0 0 #000, 0 -1px 0 #000, 0 1px 0 #000, -1px 0 0 #000;font-size: 0.7vw"></i>
                          {{wdrobe.name}}</button>
                      </div>
                    </div>
                    <div v-for="(wdrobe2, index) in wardrobe2" :key="index">
                      <div class="mt-1 col-md-12 col-sm-12 col-xs-12">
                        <button type="button" class="btn btn-primary btn-block"> <i class="fa-solid fa-shirt mr-3"
                            style="color:white; text-shadow: 1px 0 0 #000, 0 -1px 0 #000, 0 1px 0 #000, -1px 0 0 #000;font-size: 0.7vw"></i>
                          Leer</button>
                      </div>
                    </div>
                  </div>        
              </div>
              <hr />
              <div>
                <div style="display: flex; justify-content: center; align-items: center;" class="mt-3">
                  <input type="text" maxlength="10" class="col-md-5 mr-3 form-control text-center"
                    placeholder="Outfitname" style="border-radius: 1vw" v-model="outfitname">
                  <button type="button" class="btn btn-secondary mr-1" @click="createOutfitW()">Akt. Outfit
                    speichern</button>
                  <button type="button" class="btn btn-danger mr-1" @click="deleteOutfitW()">Outfit löschen</button>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div style="height: 100%; background-color: transparent;" v-if="clothshow4">
      <div class="row justify-content-center centering4">
        <div class="col-md-12 mt-1 animate__animated animate__bounceInUp">
          <div class="col-md-12 mt-1">
            <div class="box box-default">
              <div class="row">
                <div class="card card-primary card-outline" v-if="faction != 1 && faction != 2 && faction != 3 && faction != 1337">
                  <div class="card-header" style="font-family: 'Exo', sans-serif; font-size: 1.2vw">
                    Umkleidekabine
                  </div>
                  <div class="card-body" style="max-height:60vh; width: 27.5vw; overflow-x: auto">
                    <h5>Kleidungstyp auswählen:</h5>
                    <div class="row mt-3">
                      <div class="col-md-12">
                        <div style="display: flex; justify-content: center; align-items: center;">
                          <img src="../assets/images/clothshop/kopfbedeckung.png"
                            :class="[(selectedcloth == 'Kopfbedeckung') ? 'clothiconactive mr-2':'clothicon mr-2']"
                            @click="selectCloth('Kopfbedeckung')">
                          <img src="../assets/images/clothshop/brillen.png"
                            :class="[(selectedcloth == 'Brillen') ? 'clothiconactive mr-2':'clothicon mr-2']"
                            @click="selectCloth('Brillen')">
                          <img src="../assets/images/clothshop/torso.png"
                            :class="[(selectedcloth == 'Torso') ? 'clothiconactive mr-2':'clothicon mr-2']"
                            @click="selectCloth('Torso')">
                          <img src="../assets/images/clothshop/oberbekleidung.png"
                            :class="[(selectedcloth == 'Oberbekleidung') ? 'clothiconactive mr-2':'clothicon mr-2']"
                            @click="selectCloth('Oberbekleidung')">
                          <img src="../assets/images/clothshop/tshirt.png"
                            :class="[(selectedcloth == 'T-Shirt') ? 'clothiconactive mr-2':'clothicon mr-2']"
                            @click="selectCloth('T-Shirt')">
                          <img src="../assets/images/clothshop/hosen.png"
                            :class="[(selectedcloth == 'Hosen') ? 'clothiconactive mr-2':'clothicon mr-2']"
                            @click="selectCloth('Hosen')">
                          <img src="../assets/images/clothshop/schuh.png"
                            :class="[(selectedcloth == 'Schuh') ? 'clothiconactive mr-2':'clothicon mr-2']"
                            @click="selectCloth('Schuh')">
                          <img src="../assets/images/clothshop/krawatte.png"
                            :class="[(selectedcloth == 'Accessoires') ? 'clothiconactive mr-2':'clothicon mr-2']"
                            @click="selectCloth('Accessoires')">
                        </div>
                      </div>
                    </div>
                    <hr v-if="selectedcloth != 'n/A'" />
                    <h5 v-if="selectedcloth != 'n/A'">{{selectedcloth}}auswahl ({{selectedclothid}}):</h5>
                    <vue-range-slider
                      v-if="selectedcloth != 'n/A' && (selectedcloth == 'Brillen' || selectedcloth == 'Rucksack' || selectedcloth == 'Brillen' || selectedcloth == 'Kopfbedeckung')"
                      ref="slider" tooltip="false" dotSize="14" height="13" :min="0" :max="maxcloth" value='0'
                      :step="1" v-model="selectedclothid" v-oninput="clothingCustomize(1)" />
                    <vue-range-slider
                      v-if="selectedcloth != 'n/A' && selectedcloth != 'Brillen' && selectedcloth != 'Rucksack' && selectedcloth != 'Brillen' && selectedcloth != 'Kopfbedeckung'"
                      ref="slider" tooltip="false" dotSize="14" height="13" :min="0" :max="maxcloth" value='0' :step="1"
                      v-model="selectedclothid" v-oninput="clothingCustomize(1)" />
                    <h5 class="mt-1" v-if="selectedclothid != 'n/A'">{{selectedcloth}}variante ({{selectedclothcolor}}):
                    </h5>
                    <vue-range-slider
                      v-if="selectedcloth != 'n/A' && this.selectedcloth != 'Torso' && maxclothColor > 0" ref="slider"
                      tooltip="false" dotSize="14" height="13" :min="0" :max="maxclothColor" value='0' :step="1"
                      v-model="selectedclothcolor" v-oninput="clothingCustomize(2)" />
                    <vue-range-slider
                      v-if="selectedcloth != 'n/A' && (this.selectedcloth == 'Torso' || maxclothColor <= 0)"
                      ref="slider" tooltip="false" dotSize="14" height="13" :min="0" :max="0" value='0' :step="1"
                      v-model="clothSetNull" :disabled="true" />
                    <hr v-if="selectedcloth != 'n/A'" />
                    <div style="display: flex; justify-content: center; align-items: center;" class="mt-2">
                      <button v-if="selectedcloth != 'n/A'" type="button" class="btn btn-success mr-1"
                        @click="buyCloth('dutyCloths')">Dienst beginnen</button>
                      <button v-if="selectedcloth != 'n/A'" type="button" class="btn btn-warning mr-1"
                        @click="endCloth(true)">Dienst beenden</button>
                      <button v-if="selectedcloth != 'n/A'" type="button" class="btn btn-danger mr-1"
                        @click="resetCloth()">Reset</button>
                      <button type="button" class="btn btn-secondary" @click="endCloth(false)">Abbrechen</button>
                    </div>
                    <hr>
                    <h5>Gespeicherte Outfits:</h5>
                    <div>
                      <div style="display: flex; justify-content: center; align-items: center;" class="mt-2">
                        <div v-for="(outfit, index) in outfits" :key="index">
                          <button type="button" class="btn btn-primary mr-1" @click="selectOutfit(outfit, 'n/A')">Outfit: {{outfit.name}}</button>
                          <i @click="deleteOutfit(outfit.id)" class="fa-solid fa-trash mr-1" 
                              style="color:rgb(219, 73, 73); text-shadow: 1px 0 0 #000, 0 -1px 0 #000, 0 1px 0 #000, -1px 0 0 #000;font-size: 0.7vw"></i>
                        </div>
                      </div>
                    </div>
                    <div>
                      <div style="display: flex; justify-content: center; align-items: center;" class="mt-3">
                        <input type="text" maxlength="10" class="col-md-4 mr-3 form-control text-center"
                          placeholder="Outfitname" style="border-radius: 1vw" v-model="outfitname">
                        <button type="button" class="btn btn-secondary mr-1" @click="createOutfit()">Aktuelle Auswahl speichern</button>
                      </div>
                    </div>
                    <hr>
                    <span class="text-center"
                      style="display: flex; justify-content: center; align-items: center;">Benutze
                      Taste [K] um die
                      Kamera frei bewegen zu können, ebenfalls kannst du mit der rechten/linken Pfeiltaste die Auswahl
                      steuern!</span>
                  </div>
                </div>
                <div class="card card-primary card-outline" v-else>
                  <div class="card-header" style="font-family: 'Exo', sans-serif; font-size: 1.2vw">
                    Umkleidekabine
                    <input type="text" class="form-control float-left" placeholder="Suche" v-bind:value="searchelement" v-on:input="searchelement = $event.target.value" maxlength="128" autocomplete="off">
                  </div>
                  <div class="card-body" style="max-height:60vh; width: 27.5vw; overflow-x: auto">
                    <h5 v-if="faction != 1337"><u>Fraktionsoutfit auswählen:</u></h5>
                    <h5 v-else><u>EUP Testoutfit auswählen:</u></h5>
                    <div class="row mt-3">
                      <div class="col-md-12">
                        <div style="display: flex; justify-content: center; align-items: center; overflow:auto; display:block; max-height: 30vh">
                          <div v-for="outfit in filteredList1" :key="outfit.id">
                            <span class="iconblue" style="font-family: 'Exo', sans-serif; cursor:pointer" @click="selectOutfit(outfit, 'EUP')">{{outfit.name}}</span>
                          </div>
                        </div>
                      </div>
                    </div>
                    <hr/>
                    <div style="display: flex; justify-content: center; align-items: center;" class="mt-1" v-if="faction != 1337">
                      <button type="button" class="btn btn-success mr-1"
                        @click="buyCloth('dutyCloths')">Dienst beginnen</button>
                      <button type="button" class="btn btn-warning mr-1"
                        @click="endCloth(true)">Dienst beenden</button>
                      <button type="button" class="btn btn-secondary" @click="endCloth(false)">Abbrechen</button>
                    </div>
                    <div style="display: flex; justify-content: center; align-items: center;" class="mt-1" v-if="faction == 1337">
                      <button type="button" class="btn btn-success mr-1"
                        @click="buyCloth('dutyCloths')">Outfit testen</button>
                      <button type="button" class="btn btn-secondary" @click="endCloth(false)">Abbrechen</button>
                    </div>
                    <hr/>
                    <span class="text-center"
                      style="display: flex; justify-content: center; align-items: center;">Benutze
                      Taste [K] um die
                      Kamera frei bewegen zu können!</span>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div style="height: 100%; background-color: transparent;" v-if="clothshow">
      <div class="row justify-content-center centering4">
        <div class="col-md-12 mt-1 animate__animated animate__bounceInUp">
          <div class="col-md-12 mt-1">
            <div class="box box-default">
              <div class="row">
                <div class="card card-primary card-outline">
                  <div class="card-header" style="font-family: 'Exo', sans-serif; font-size: 1.2vw">
                    Kleidungsladen
                  </div>
                  <div class="card-body" style="max-height:50vh; width: 27.5vw; overflow-x: auto">
                    <h5>Kleidungstyp auswählen:</h5>
                    <div class="row mt-3">
                      <div class="col-md-12">
                        <div style="display: flex; justify-content: center; align-items: center;">
                          <img src="../assets/images/clothshop/kopfbedeckung.png"
                            :class="[(selectedcloth == 'Kopfbedeckung') ? 'clothiconactive mr-2':'clothicon mr-2']"
                            @click="selectCloth('Kopfbedeckung')">
                          <img src="../assets/images/clothshop/brillen.png"
                            :class="[(selectedcloth == 'Brillen') ? 'clothiconactive mr-2':'clothicon mr-2']"
                            @click="selectCloth('Brillen')">
                          <img src="../assets/images/clothshop/torso.png"
                            :class="[(selectedcloth == 'Torso') ? 'clothiconactive mr-2':'clothicon mr-2']"
                            @click="selectCloth('Torso')">
                          <img src="../assets/images/clothshop/oberbekleidung.png"
                            :class="[(selectedcloth == 'Oberbekleidung') ? 'clothiconactive mr-2':'clothicon mr-2']"
                            @click="selectCloth('Oberbekleidung')">
                          <img src="../assets/images/clothshop/tshirt.png"
                            :class="[(selectedcloth == 'T-Shirt') ? 'clothiconactive mr-2':'clothicon mr-2']"
                            @click="selectCloth('T-Shirt')">
                          <img src="../assets/images/clothshop/rucksack.png"
                            :class="[(selectedcloth == 'Rucksack') ? 'clothiconactive mr-2':'clothicon mr-2']"
                            @click="selectCloth('Rucksack')">
                          <img src="../assets/images/clothshop/hosen.png"
                            :class="[(selectedcloth == 'Hosen') ? 'clothiconactive mr-2':'clothicon mr-2']"
                            @click="selectCloth('Hosen')">
                          <img src="../assets/images/clothshop/schuh.png"
                            :class="[(selectedcloth == 'Schuh') ? 'clothiconactive mr-2':'clothicon mr-2']"
                            @click="selectCloth('Schuh')">
                        </div>
                      </div>
                    </div>
                    <hr v-if="selectedcloth != 'n/A'" />
                    <h5 v-if="selectedcloth != 'n/A'">{{selectedcloth}}auswahl ({{selectedclothid}}):</h5>
                    <vue-range-slider
                      v-if="selectedcloth != 'n/A' && (selectedcloth == 'Brillen' || selectedcloth == 'Rucksack' || selectedcloth == 'Brillen' || selectedcloth == 'Kopfbedeckung')"
                      ref="slider" tooltip="false" dotSize="14" height="13" :min="-1" :max="maxcloth" value='0'
                      :step="1" v-model="selectedclothid" v-oninput="clothingCustomize(1)" />
                    <vue-range-slider
                      v-if="selectedcloth != 'n/A' && selectedcloth != 'Brillen' && selectedcloth != 'Rucksack' && selectedcloth != 'Brillen' && selectedcloth != 'Kopfbedeckung'"
                      ref="slider" tooltip="false" dotSize="14" height="13" :min="0" :max="maxcloth" value='0' :step="1"
                      v-model="selectedclothid" v-oninput="clothingCustomize(1)" />
                    <h5 class="mt-1" v-if="selectedcloth != 'n/A'">{{selectedcloth}}variante ({{selectedclothcolor}}):
                    </h5>
                    <vue-range-slider
                      v-if="selectedcloth != 'n/A' && this.selectedcloth != 'Torso' && maxclothColor > 0" ref="slider"
                      tooltip="false" dotSize="14" height="13" :min="0" :max="maxclothColor" value='0' :step="1"
                      v-model="selectedclothcolor" v-oninput="clothingCustomize(2)" />
                    <vue-range-slider
                      v-if="selectedcloth != 'n/A' && (this.selectedcloth == 'Torso' || maxclothColor <= 0)"
                      ref="slider" tooltip="false" dotSize="14" height="13" :min="0" :max="0" value='0' :step="1"
                      v-model="clothSetNull" :disabled="true" />
                    <hr v-if="selectedcloth != 'n/A'" />
                    <div v-if="selectedcloth != 'n/A'"
                      style="display: flex; justify-content: center; align-items: center;">
                      <h5>Aktuelle Kosten: {{clothcost}}$</h5>
                    </div>
                    <div style="display: flex; justify-content: center; align-items: center;" class="mt-2">
                      <button v-if="selectedcloth != 'n/A' && clothcost > 0" type="button" class="btn btn-success mr-1"
                        @click="buyCloth()">Outfit kaufen</button>
                      <button v-if="selectedcloth != 'n/A'" type="button" class="btn btn-danger mr-1"
                        @click="resetCloth()">Reset</button>
                      <button type="button" class="btn btn-secondary" @click="abortCloth()">Abbrechen</button>
                    </div>
                    <hr>
                    <span class="text-center" style="display: flex; justify-content: center; align-items: center;">Benutze
                      Taste [K] um die
                      Kamera frei bewegen zu können, ebenfalls kannst du mit der rechten/linken Pfeiltaste die Auswahl steuern!</span>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div style="height: 100%; background-color: transparent;" v-if="clothshow2">
      <div class="row justify-content-center centering4">
        <div class="col-md-12 mt-1 animate__animated animate__bounceInUp">
          <div class="col-md-12 mt-1">
            <div class="box box-default">
              <div class="row">
                <div class="card card-primary card-outline">
                  <div class="card-header" style="font-family: 'Exo', sans-serif; font-size: 1.2vw">
                    Juwelier
                  </div>
                  <div class="card-body" style="max-height:50vh; width: 27.5vw; overflow-x: auto">
                    <h5>Accessoiretyp auswählen:</h5>
                    <div class="row mt-3">
                      <div class="col-md-12">
                        <div style="display: flex; justify-content: center; align-items: center;">
                          <img src="../assets/images/clothshop/ohrring.png"
                            :class="[(selectedcloth == 'Ohrring') ? 'clothiconactive mr-2':'clothicon mr-2']"
                            @click="selectCloth('Ohrring')">
                          <img src="../assets/images/clothshop/armbanduhr.png"
                            :class="[(selectedcloth == 'Armbanduhr') ? 'clothiconactive mr-2':'clothicon mr-2']"
                            @click="selectCloth('Armbanduhr')">
                          <img src="../assets/images/clothshop/armring.png"
                            :class="[(selectedcloth == 'Armring') ? 'clothiconactive mr-2':'clothicon mr-2']"
                            @click="selectCloth('Armring')">
                          <img src="../assets/images/clothshop/krawatte.png"
                            :class="[(selectedcloth == 'Accessoires') ? 'clothiconactive mr-2':'clothicon mr-2']"
                            @click="selectCloth('Accessoires')">
                        </div>
                      </div>
                    </div>
                    <hr v-if="selectedcloth != 'n/A'" />
                    <h5 v-if="selectedcloth != 'n/A'">{{selectedcloth}}auswahl ({{selectedclothid}}):</h5>
                    <vue-range-slider
                      v-if="selectedcloth != 'n/A'"
                      ref="slider" tooltip="false" dotSize="14" height="13" :min="-1" :max="maxcloth" value='0' :step="1"
                      v-model="selectedclothid" v-oninput="clothingCustomize(1)" />
                    <h5 class="mt-1" v-if="selectedcloth != 'n/A' && maxclothColor > 0">{{selectedcloth}}variante ({{selectedclothcolor}}):
                    </h5>
                    <vue-range-slider
                      v-if="selectedcloth != 'n/A' && maxclothColor > 0" ref="slider"
                      tooltip="false" dotSize="14" height="13" :min="0" :max="maxclothColor" value='0' :step="1"
                      v-model="selectedclothcolor" v-oninput="clothingCustomize(2)" />
                    <hr v-if="selectedcloth != 'n/A'" />
                    <div v-if="selectedcloth != 'n/A'"
                      style="display: flex; justify-content: center; align-items: center;">
                      <h5>Aktuelle Kosten: {{clothcost}}$</h5>
                    </div>
                    <div style="display: flex; justify-content: center; align-items: center;" class="mt-2">
                      <button v-if="selectedcloth != 'n/A' && clothcost > 0" type="button" class="btn btn-success mr-1"
                        @click="buyCloth()">Sachen kaufen</button>
                      <button v-if="selectedcloth != 'n/A'" type="button" class="btn btn-danger mr-1"
                        @click="resetCloth()">Reset</button>
                      <button type="button" class="btn btn-secondary" @click="abortCloth()">Abbrechen</button>
                    </div>
                    <hr>
                    <span class="text-center" style="display: flex; justify-content: center; align-items: center;">Benutze
                      Taste [K] um die
                      Kamera frei bewegen zu können, ebenfalls kannst du mit der rechten/linken Pfeiltaste die Auswahl steuern!</span>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="centering2" style="height: 100%; background-color: rgba(69, 77, 85, .8);" v-if="tutorialStadthalle">
      <div class="row justify-content-center centering">
        <div class="col-md-12 mt-1 animate__animated animate__bounceInUp">
          <div class="col-md-12 mt-1">
            <div class="box box-default">
              <div class="card card-primary card-outline">
                <div class="card-header" style="font-family: 'Exo', sans-serif;"><strong class="ml-2">Beantragung der
                    Staatsbürgerschaft San Andreas</strong>
                  <button type="button" @click="checkEinreise()" class="btn btn-primary float-right"
                    v-if="!einreisefinish">Dokument
                    abgeben</button>
                </div>
                <div class="card-body">
                  <div class="row">
                    <div class="col-md-12 mt-1">
                      <div style="display: flex; justify-content: center; align-items: center;">
                        <h6>Hallo <span v-if="gender == 1">Herr</span> <span v-else>Frau</span> <b style="color:#3F6791;font-family: 'Exo', sans-serif;">{{name}}</b>,
                          Willkommen
                          in Los Santos dem Herzstück von San Andreas! Sie hatten eben ja schon ein nettes Gespräch
                          mit
                          meinem Kollegen, um Ihre Staatsbürgerschaft abschließen zu können benötige ich noch einige
                          Daten von Ihnen.</h6>
                      </div>
                      <div style="display: flex; justify-content: center; align-items: center;">
                        <h6>Könnten Sie dieses Dokument, bitte einmal Wahrheitsgemäß ausfüllen, Danke! Ich werde es
                          mir
                          danach einmal anschauen und Ihren Personalausweis vorbereiten!</h6>
                      </div>
                      <hr />
                      <li class="active">
                        <span class="fa fa-search-plus mt-3 mr-2"></span>Größe in cm
                        <input placeholder="180" value="180" v-model="size" type="text"
                          class="form-control mt-1" maxlength="3">
                      </li>
                      <li class="active">
                        <span class="fa fa-tint mt-3 mr-2"></span>Augenfarbe
                        <input placeholder="Grün" v-model="eyecolor" type="text" class="form-control mt-1"
                          maxlength="10">
                      </li>
                      <li class="active">
                        <span class="fa fa-book mt-3 mr-2"></span>Abschluss
                        <input placeholder="Realschulabschluss" v-model="education" type="text"
                          class="form-control mt-1" maxlength="64">
                      </li>
                      <li class="active">
                        <span class="fas fa-pencil-alt mt-3 mr-2"></span>Besondere Fähigkeiten
                        <input placeholder="Schwimmen, Autoaffin, Geduldig" v-model="skills" type="text"
                          class="form-control mt-1" maxlength="64">
                      </li>
                      <li class="active">
                        <span class="fa fa-file-alt mt-3 mr-2"></span>Aussehen
                        <input placeholder="Normal gebaut, kurze Gesichtsbehaarung" v-model="appearance" type="text"
                          class="form-control mt-1" maxlength="64">
                      </li>
                      <div class="mt-3" style="display: flex; justify-content: center; align-items: center;">
                        <h6 style="color:yellow">Sollten Sie noch auf der Suche nach einem Job sein, sprechen Sie mich
                          gerne gleich nochmal an. Wir haben hier einige attraktive Angebote für unsere Staatsbürger!
                        </h6>
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
    <div class="charactercreator" style="background-color:transparent" v-if="showdealershipMenu">
      <nav
        class="main-header navbar navbar-expand navbar-white navbar-light bg-primary animate__animated animate__fadeIn"
        style="color:white">
        <div class="hair">
          <ul class="vehicleColorsList">
            <li v-for="(color, index) in dealerColors" :key="index" @click="selectColor(color.ID)"
              :style="{ background: color.Hex }">
            </li>
          </ul>
        </div>
      </nav>
    </div>
    <div style="overflow: auto" v-if="showdealershipMenu">
      <aside class="main-sidebar sidebar-dark-primary elevation-4 animate__animated animate__fadeIn"
        style="background-color: #1E282C">
        <a href="#" class="brand-link" style="background-color: #1A2226">
          <strong><span class="brand-text font-weight"
              style="font-family: 'Exo', sans-serif; display: flex; justify-content: center; align-items: center; font-size: 22px">{{dealerShipName.substring(0,18)}}</span></strong>
        </a>
        <div class="sidebar">
          <div class="user-panel mt-3 pb-3 mb-3 d-flex" v-for="(vehicle, index) in dealerShipVehicles" :key="index"
            style="display: flex; justify-content: center; align-items: center;">
            <strong><a href="#"><span class="brand-text font-weight"
                  style="font-family: 'Exo', sans-serif; font-size: 20px; color: #3F6791"
                  v-if="vehicle.vehiclename == dealerShipVehicle.vehiclename">{{vehicle.vehiclename}} <span
                    style="font-size: 15px"
                    class="badge badge-dark ml-3">{{parseInt(vehicle.price*multiplier)}}$</span></span><span
                  class="brand-text font-weight" style="font-family: 'Exo', sans-serif; font-size: 20px"
                  @click="selectDealerShipVehicle(vehicle.vehiclename)" v-else>{{vehicle.vehiclename}} <span
                    style="font-size: 15px"
                    class="badge badge-dark ml-3">{{parseInt(vehicle.price*multiplier)}}$</span></span></a></strong>
          </div>
        </div>
      </aside>
    </div>
    <div style="height: 100%; background-color: transparent;" v-if="showdealershipMenu">
      <div class="row justify-content-center centering6">
        <div class="col-md-12 mt-1 animate__animated animate__fadeIn">
          <div class="col-md-12 mt-1">
            <div class="box box-default">
              <div class="row">
                <div class="card card-primary card-outline">
                  <div class="card-header" style="font-family: 'Exo', sans-serif; font-size: 1.1vw">
                    {{dealerShipVehicle.vehiclename}}
                    <button type="button" class="btn btn-danger float-right" style="color:white"
                      @click="abortDealerShip()">Abbrechen</button>
                  </div>
                  <div class="card-body" style="max-height:50vh; width: 25vw; overflow-x: auto">
                    <div class="row">
                      <div class="col-md-12">
                        <label
                          v-if="dealerShipVehicle.maxspeed > 1 && dealerShipVehicle.bizzid != 31 && dealerShipVehicle.bizzid != 32">Geschwindigkeit:</label>
                        <div
                          v-if="dealerShipVehicle.maxspeed > 1 && dealerShipVehicle.bizzid != 31 && dealerShipVehicle.bizzid != 32"
                          class="progress-bar progress-bar-striped progress-bar-animated" role="progressbar"
                          aria-valuenow="75" aria-valuemin="0" aria-valuemax="100"
                          :style="{ 'width': parseInt(100/dealerShipMax[0]*dealerShipVehicle.maxspeed) + '%' }"></div>
                        <label
                          v-if="dealerShipVehicle.maxspeed > 1 && dealerShipVehicle.bizzid != 31 && dealerShipVehicle.bizzid != 32"
                          style="margin-top: 0.1vw">Beschleunigung:</label>
                        <div
                          v-if="dealerShipVehicle.maxspeed > 1 && dealerShipVehicle.bizzid != 31 && dealerShipVehicle.bizzid != 32"
                          class="progress-bar progress-bar-striped progress-bar-animated" role="progressbar"
                          aria-valuenow="75" aria-valuemin="0" aria-valuemax="100"
                          :style="{ 'width': parseInt((100/parseFloat(dealerShipMax[1])*parseFloat(dealerShipVehicle.maxacceleration))) + '%' }">
                        </div>
                        <label
                          v-if="dealerShipVehicle.maxspeed > 1 && dealerShipVehicle.bizzid != 31 && dealerShipVehicle.bizzid != 32"
                          style="margin-top: 0.1vw">Bremsen:</label>
                        <div
                          v-if="dealerShipVehicle.maxspeed > 1 && dealerShipVehicle.bizzid != 31 && dealerShipVehicle.bizzid != 32"
                          class="progress-bar progress-bar-striped progress-bar-animated" role="progressbar"
                          aria-valuenow="75" aria-valuemin="0" aria-valuemax="100"
                          :style="{ 'width': parseInt((100/parseFloat(dealerShipMax[2])*parseFloat(dealerShipVehicle.maxbraking))) + '%' }">
                        </div>
                        <label v-if="dealerShipVehicle.maxspeed > 1 && dealerShipVehicle.maxhandling > 0"
                          style="margin-top: 0.1vw">Handling:</label>
                        <div
                          v-if="dealerShipVehicle.maxspeed > 1 && dealerShipVehicle.bizzid != 31 && dealerShipVehicle.bizzid != 32"
                          class="progress-bar progress-bar-striped progress-bar-animated" role="progressbar"
                          aria-valuenow="75" aria-valuemin="0" aria-valuemax="100"
                          :style="{ 'width': parseInt((100/parseFloat(dealerShipMax[3])*parseFloat(dealerShipVehicle.maxhandling))) + '%' }">
                        </div>
                        <hr
                          v-if="dealerShipVehicle.maxspeed > 1 && dealerShipVehicle.bizzid != 31 && dealerShipVehicle.bizzid != 32" />
                        <div>
                          <div class="row">
                            <div class="col-md-12">

                              <div class="infoboxtext ml-2">
                                Preis: {{parseInt(dealerShipVehicle.price*multiplier)}}$
                              </div>
                              <div v-if="dealerShipVehicle.maxspeed > 1" class="infoboxtext ml-2">
                                Höchstgeschwindigkeit: {{dealerShipVehicle.maxspeed}} KM/H
                              </div>
                              <div class="infoboxtext ml-2">
                                Kofferraumvolumen: {{dealerShipVehicle.trunk}}KG
                              </div>
                              <div v-if="dealerShipVehicle.fuel > 0" class="infoboxtext ml-2">
                                Tankvolumen: {{dealerShipVehicle.fuel}}l
                              </div>
                            </div>
                          </div>
                        </div>
                        <hr />
                        <div style="display: flex; justify-content: center; align-items: center;">
                          <button type="button" class="btn btn-success" @click="buyVehicle(1)">Kaufen (Bar)</button>
                          <button type="button" class="btn btn-success ml-3" @click="buyVehicle(2)">Kaufen
                            (EC-Karte)</button>
                          <button v-if="dealerShipVehicle.fuel > 0" type="button" class="btn btn-primary ml-3"
                            @click="testDealerShip()">Probefahrt</button>
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
import Vue from "vue";
import VueProgress from 'vue-progress';
import Toast from "vue-toastification";
import "vue-toastification/dist/index.css";
import Swal from 'sweetalert2/dist/sweetalert2.js'
import 'vue-range-component/dist/vue-range-slider.css'
import VueRangeSlider from 'vue-range-component'
import {
  maxShoesMen,
  maxShoesWomen,
  maxLegsMen,
  maxLegsWomen,
  maxTorsoMen,
  maxTorsoWomen,
  maxOberbekleidungMen,
  maxOberbekleidungWomen,
  maxTShirtMen,
  maxTShirtWomen,
  maxBagsMen,
  maxBagsWomen,
  maxGlassesMen,
  maxGlassesWomen,
  maxKopfbedeckungMen,
  maxKopfbedeckungWomen,
  maxOhrringMen,
  maxOhrringWomen,
  maxArmbanduhrMen,
  maxArmbanduhrWomen,
  maxArmringMen,
  maxArmringWoman,
  maxAccessoiresMen,
  maxAccessoiresWoman,
  invalidHairMen,
  invalidHairWomen,
  invalidMenTop,
  invalidWomanTop,
  invalidWomanLeg,
  invalidMenLeg,
  invalidMenTorso,
  invalidWomanTorso,
  invalidMenShoe,
  invalidWomanShoe,
  invalidMenTShirt,
  invalidWomanTShirt,
  invalidMenBags,
  invalidWomanBags,
  invalidMenGlasses,
  invalidWomanGlasses,
  invalidMenHats,
  invalidWomanHats,
  invalidMenOhrring,
  invalidWomanOhrring,
  invalidMenArmbanduhr,
  invalidWomanArmbanduhr,
  invalidMenArmring,
  invalidWomanArmring,
  invalidAccessoiresMen,
  invalidAccessoiresWoman,
  invalidMask,
  getVehicleColors,
  maxMaskMen,
  maxMaskWoman,
} from './helper/externals/misc.js'

Vue.use(VueProgress);

let height = screen.height;
let fuelTimer = null;

if (height < 900) {
  Vue.use(Toast, {
    transition: "Vue-Toastification__bounce",
    maxToasts: 1,
    newestOnTop: true
  });
} else {
  Vue.use(Toast, {
    transition: "Vue-Toastification__bounce",
    maxToasts: 2,
    newestOnTop: true
  });
}

export default {
  name: 'Hud',
  data: function () {
    return {
      //Searchelement
      searchelement: '',
      //Wardrobe
      wardrobe: [],
      wardrobe2: 0,
      showwardrobe: false,
      //Crafting
      showcrafting: false,
      weaponinfo: '',
      mats: 0,
      //Group
      groupid: 0,
      //Gangzone
      showgangzone: false,
      gangzone: [],
      gangzoneprop: [],
      //Death
      showdeath: false,
      respawnTime: 0,
      deathIntervall: null,
      //Perso/Lizenzen
      showperso: false,
      persoData: [],
      licData: [],
      licData2: [],
      showlic: false,
      //Apotheke
      showrecept: false,
      //Arrestrecord
      showarrest: false,
      arrestName: 'n/A',
      arrestInfo: [],
      //Ticket
      showticket: false,
      //Radio
      frequenz: '',
      showradiosystem: false,
      //Music
      musiclink: '',
      showmusic: false,
      showmusicstatus: 1,
      //Bizz
      bizzid: -1,
      //Busdriver
      busPlanShow: false,
      routeName: '',
      stationName: '',
      allStations: [],
      busDriver: 0,
      //Tatto Shop,
      tattooshow: false,
      selectTattoo: '',
      tattooJson: [],
      tattoos: [],
      zoneId: -1,
      //Barber Shop
      headOverlays: [-1, -1, -1],
      headOverlaysColors: [0, 0, 0],
      headOverlaysB: [-1, -1, -1],
      headOverlaysColorsB: [0, 0, 0],
      selectedBarber: 'Haare',
      oldhair: 0,
      oldhaircolor: 0,
      oldhair2: 0,
      oldhaircolor2: 0,
      oldhair3: -1,
      maxhair: 88,
      barbershow: false,
      beardColors: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 26, 27, 28, 29, 55, 56, 57, 58, 59, 60, 61, 62, 63],
      hairColors: [
        '#0c0c0c', '#1d1a17', '#281d18', '#3d1f15', '#682e19', '#954b29', '#a35234', '#9b5f3d', '#b57e54', '#c19167',
        '#af7f53', '#be9560', '#d0ac75', '#b37f43', '#dbac68', '#e4ba7e', '#bd895a', '#83422c', '#8e3a28', '#8a241c',
        '#962b20', '#a7271d', '#c4351f', '#d8421f', '#c35731', '#d24b21', '#816755', '#917660', '#a88c74', '#d0b69e',
        '#513442', '#744557', '#a94663', '#cb1e8e', '#f63f78', '#ed9393', '#0b917e', '#248081', '#1b4d6b', '#578d4b',
        '#235433', '#155146', '#889e2e', '#71881b', '#468f21', '#cc953d', '#ebb010', '#ec971a', '#e76816', '#e64810',
        '#ec4d0e', '#c22313', '#e43315', '#ae1b18', '#6d0c0e', '#281914', '#3d241a', '#4c281a', '#5d3929', '#69402b',
        '#291b16', '#0e0e10', '#e6bb84', '#d8ac74'
      ],
      //Dealership
      showdealershipMenu: false,
      dealerShipName: '',
      dealerShipVehicle: [],
      dealerShipVehicles: [],
      dealerShipMax: [],
      dealerColors: [],
      //Fuelsystem
      showfuel: false,
      maxfuel: 100,
      fuel: 50.0,
      newfuel: 0,
      fuelprice: 0,
      getfuelprice: 6,
      bizzproducts: 2000,
      fuelstatus: false,
      //Maskenladen
      clothshow3: false,
      //Fraktionen
      faction: 0,
      clothshow4: false,
      outfitname: '',
      factionCloths: [],
      outfits: [],
      //Juwelier
      clothshow2: false,
      //Clothsystem
      clothshow: false,
      selectedcloth: 'n/A',
      clothcost: 0,
      clothing: [0, 0, 0, 0, 0, -1, -1, -1, 0, -1, -1, -1, 0],
      clothingColor: [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
      clothingBackup: [0, 0, 0, 0, 0, -1, -1, -1, -1, -1, -1, -1, 0],
      clothingColorBackup: [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
      clothSetNull: 0,
      multiplier: 1.0,
      maxcloth: 999,
      maxclothColor: 999,
      gender: 1,
      selectedclothid: 0,
      selectedclothid2: -1,
      selectedclothidold: 0,
      selectedclothcolor: 0,
      selectedclothcolor2: 0,
      selectedclothid3: -1,
      clicked: (Date.now() / 1000),
      //Infobox
      showinfobox: false,
      infoboxtext1: '',
      infoboxtext2: '',
      infoboxtext3: '',
      infoboxtext4: '',
      infoboxtextheader: '',
      infoboxtextheader1: '',
      infoboxtextheader2: '',
      infoboxtextheader3: '',
      infoboxtestmodus: -1,
      //VoiceRP
      voicerp: 0,
      //Sound
      startSound: null,
      //Tutorial
      showBlack: false,
      tutorialStadthalle: false,
      tutorialStadthalle2: false,
      blackText: '',
      einreisefinish: false,
      size: '',
      eyecolor: '',
      education: '',
      skills: '',
      appearance: '',
      name: '',
      legal: '',
      //Hud
      showhud: false,
      money: 0,
      thirst: 25,
      hunger: 25,
      shield: 25,
      health: 25,
      options1: {
        color: '#e03a3a',
        fill: 'rgba(0, 0, 0, 0.5)',
        strokeWidth: 8.0,
        warnings: false,
        backgroundColor: '#302f2f',
        svgStyle: {
          width: '100%',
          height: '100%'
        },
        text: {
          value: '&#10084;'
        }
      },
      options2: {
        color: '#a8450c',
        fill: 'rgba(0, 0, 0, 0.5)',
        strokeWidth: 7.0,
        warnings: false,
        backgroundColor: '#302f2f',
        svgStyle: {
          width: '100%',
          height: '100%'
        },
        text: {
          value: '&#127828;'
        }
      },
      options3: {
        color: '#0c6aa8',
        fill: 'rgba(0, 0, 0, 0.5)',
        strokeWidth: 7.0,
        warnings: false,
        backgroundColor: '#302f2f',
        svgStyle: {
          width: '100%',
          height: '100%'
        },
        text: {
          value: '&#127865;'
        }
      },
      options4: {
        color: '#75787d',
        fill: 'rgba(0, 0, 0, 0.5)',
        strokeWidth: 7.0,
        warnings: false,
        backgroundColor: '#302f2f',
        svgStyle: {
          width: '100%',
          height: '100%'
        },
        text: {
          value: '🛡️'
        }
      },
      //Ammunation
      selectedWeapon: 'Tint',
      weaponNumber: 0,
      weaponNumberOld: 0,
      weaponNumber2: 0,
      weaponNumberBackup: 0,
      ammuPrice1: 0,
      ammuPrice2: 0,
      ammuCheck: 0,
      weaponItem: [],
      ammunationshow: false,
      weaponNames: [],
      weaponComponents: [],
      getWeapon: '',
      compNow: [],
      weaponCooldown: (Date.now() / 1000),
      //Tuning
      selectedTuning: 'Farbe 1',
      tuningshow: false,
      maxTuning: [],
      defaultHandling: [],
      tuningComponents: ['Spoiler', 'Vordere Stoßstange', 'Hintere Stoßstange', 'Seitenschweller', 'Auspuff', 'Rahmen', 'Gitter', 'Motorhaube', 'Linker Kotflügel', 'Rechter Kotflügel', 'Dach', 'Motor', 'Bremsen', 'Getriebe', 'Hupe', 'Federung', 'Panzerung', 'n/A', 'n/A', 'n/A', 'n/A', 'n/A', 'Xeon Scheinwerfer', 'Reifen', 'Hinterreifen', 'n/A', 'n/A', 'Beschläge', 'Ornaments', 'Armaturenbrett', 'Tacho', 'Türlautsprecher', 'Sitze', 'Lenkrad', 'Schalthebel', 'Fahrzeugplaketten', 'Lautsprecher', 'Kofferraum', 'Hydraulik', 'Motorblock', 'Nitro', 'Streben', 'Arch-Abdeckung', 'Antenne', 'Fahrzeugausstattung', 'Tank', 'Fenster', 'n/A', 'Paintjob', 'n/A', 'n/A', 'n/A', 'n/A', 'Kennzeichen', 'n/A', 'Fensterfarben', 'Neon', 'Reifenqualm'],
      tuningCost: [8, 9, 9, 7, 7, 8, 8, 9, 8, 8, 6, 105, 30, 40, 5, 7, 60, 0, 0, 0, 0, 0, 6, 10, 10, 0, 0, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 8, 15, 10, 55, 8, 7, 7, 6, 6, 6, 0, 15, 0, 0, 0, 0, 6, 0, 8, 90, 70, 20, 15, 20, 15, 15, 15],
      componentNumbers: [64],
      componentNumbersBackup: [64],
      perlmutt1: false,
      tuningReset: (Date.now() / 1000),
      tuningSync: (Date.now() / 1000),
      tuningSet: (Date.now() / 1000),
      stockTuning: 0,
      tuningCosts: 0,
    }
  },
  components: {
    VueRangeSlider,
  },
  mounted() {
    var self = this;
    //Design
    document.body.classList.add("dark-mode");
    //Keys
    window.addEventListener("keydown", function(e) {
      if(e.key == 'ArrowRight')
      {
        if(self.tattooshow)
        {
          self.clothPlus(self.tattooJson.length-1, 1);
        }
        else if(self.clothshow)
        {
          self.clothPlus(self.maxcloth, 1);
        }
        else if(self.clothshow2)
        {
          self.clothPlus(self.maxcloth, 1);
        }
        else if(self.clothshow3)
        {
          self.clothPlus(self.maxcloth, 1);
        }
        else if(self.clothshow4)
        {
          self.clothPlus(self.maxcloth, 1);
        }
        else if(self.barbershow && self.selectedBarber == 'Haare')
        {
          self.clothPlus(self.maxhair, 1);
        }
        else if(self.barbershow && self.selectedBarber == 'Bart')
        {
          self.clothPlus(28, 2);
        }
        
      }
      else if(e.key == 'ArrowLeft')
      {
        if(self.tattooshow)
        {
          self.clothMinus(0, 1);
        }
        else if(self.clothshow)
        {
          self.clothMinus(0, 1);
        }
        else if(self.clothshow2)
        {
          self.clothMinus(0, 1);
        }
        else if(self.clothshow3)
        {
          self.clothMinus(0, 1);
        }
        else if(self.clothshow4)
        {
          self.clothMinus(0, 1);
        }
        else if(self.barbershow && self.selectedBarber == 'Haare')
        {
          self.clothMinus(0, 1);
        }
        else if(self.barbershow && self.selectedBarber == 'Bart')
        {
          self.clothMinus(-1, 2);
        }
      }
    });
    //Progressbars
    setTimeout(function () {
      self.updateBar(1, this.health);
      self.updateBar(2, this.shield);
      self.updateBar(3, this.hunger);
      self.updateBar(4, this.rhist);
    }, 150);
    },
    computed: {
      filteredList1() {
            return this.filter1(this.outfits)
        },
    },
    methods: {
      filter1: function (outfits) {
            return outfits.filter(outfits => {
                return outfits.name.toLowerCase().includes(this.searchelement.toLowerCase())
            })
        },
      getImgUrl(pic) {
      try {
        return require('../assets/images/inventory/' + pic + '.png')
      }
      catch(e)
      {
        return require('../assets/images/inventory/fragezeichen.png')
      }
    },
    setvoicerp: function (voicerp) {
      this.voicerp = voicerp;
    },
    selectOutfitW(obj)
    {
        if ((Date.now() / 1000) > this.clicked) {
          // eslint-disable-next-line no-undef
          mp.trigger("Client:WardrobeAktion", 'selectoutfit', obj.name, 'n/A');
          this.clicked = (Date.now() / 1000) + (1);
        }
    },
    deleteOutfitW()
    {
        if ((Date.now() / 1000) > this.clicked) {
          if(this.outfitname.length < 3 || this.outfitname.length > 50)
          {
            // eslint-disable-next-line no-undef
            mp.trigger("Client:SendNotificationWithoutButton", 'Ungültiger Outfitname!', 'error', 'top-left', 2500);
            return;
          }
          // eslint-disable-next-line no-undef
          mp.trigger("Client:WardrobeAktion", 'deleteoutfit', this.outfitname);
          this.clicked = (Date.now() / 1000) + (1);
        }
    },
    createOutfitW()
    {
        if ((Date.now() / 1000) > this.clicked) {
          if(this.outfitname.length < 3 || this.outfitname.length > 50)
          {
            // eslint-disable-next-line no-undef
            mp.trigger("Client:SendNotificationWithoutButton", 'Ungültiger Outfitname!', 'error', 'top-left', 2500);
            return;
          }
          // eslint-disable-next-line no-undef
          mp.trigger("Client:WardrobeAktion", 'newoutfit', this.outfitname, 'n/A');
          this.clicked = (Date.now() / 1000) + (1);
        }
    },
    showWardrobe(json) {
      this.outfitname = '';
      if(this.wardrobe != 'null')
      {
        this.wardrobe = JSON.parse(json);
      }
      else
      {
        this.wardrobe = [];
      }
      this.wardrobe2 = 10-this.wardrobe.length;
      this.showwardrobe = true;
    },
    hideWardrobe() {
      this.showwardrobe = false;
    },
    updateWardrobe(json) {
      if(this.wardrobe != 'null')
      {
        this.wardrobe = JSON.parse(json);
      }
      else
      {
        this.wardrobe = [];
      }
      this.wardrobe2 = 10-this.wardrobe.length;
    },
    startCraft()
      {
        if ((Date.now() / 1000) > this.clicked) {
          // eslint-disable-next-line no-undef
          mp.trigger('Client:StartCraft', this.weaponinfo);
          this.clicked = (Date.now() / 1000) + (1);
        }
      },
      showCraft(mats)
      {
        this.weaponinfo = '';
        this.showcrafting = !this.showcrafting;
        this.mats = mats;
      },
      getGangzone()
      {
        if ((Date.now() / 1000) > this.clicked) {
          // eslint-disable-next-line no-undef
          mp.trigger('Client:GetGangzone');
          this.clicked = (Date.now() / 1000) + (1);
        }
      },
      showGangzone(json1, json2, group)
      {
        this.showgangzone = true;
        this.gangzone = JSON.parse(json1);
        this.gangzoneprop = JSON.parse(json2);
        this.groupid = group;
      },
      hideGangzone()
      {
        this.showgangzone = false;
      },
      respawn()
      {
        this.hideDeath();
        // eslint-disable-next-line no-undef
        mp.trigger('Client:RespawnPlayer');
      },
      hideDeath()
      {
        this.showdeath = false;
        this.respawnTime = 0;
        if(this.deathIntervall != null)
        {
          clearInterval(this.deathIntervall);
          this.deathIntervall = null;
        }
      },
      setDeath(time)
      {
        this.showdeath = true;
        this.respawnTime = time;
        if(this.deathIntervall != null)
        {
          clearInterval(self.deathIntervall);
          self.deathIntervall = null;
        }
        var self = this;
        this.deathIntervall = setInterval(() => {      
            if(self.respawnTime > 0)
            {
              self.respawnTime --;
            }
            else
            {
              self.respawnTime = 0;
              clearInterval(self.deathIntervall);
              self.deathIntervall = null;
            }
        }, 1063);
      },
      getRespawnTime()
      {
        var minute = parseInt(this.respawnTime/60%60);
        if(minute < 10)
        {
          minute = "0" + parseInt(minute);
        }
        var second = parseInt(this.respawnTime%60);
        if(second < 10)
        {
          second = "0" + parseInt(second);
        }
        return minute + ":" + second;
      },
      hideRecept()
      {
        // eslint-disable-next-line no-undef
        mp.trigger('Client:ShowRezept', 'n/A');
      },
      setRecept(text)
      {
        // eslint-disable-next-line no-undef
        mp.trigger('Client:MDCSettings', 'setrecept', text);
      },
      showreceptmenu(receptName)
      {
        this.arrestName = receptName;
        this.showrecept = !this.showrecept;
      },
      play3DSound(link, status){
        if ((Date.now() / 1000) > this.clicked) {
          // eslint-disable-next-line no-undef
          mp.trigger("Client:Play3DSound", link, status);
          if(status == -1)
          {
            // eslint-disable-next-line no-undef
            mp.trigger("Client:ShowMusicStation");
          }
          this.clicked = (Date.now() / 1000) + (1);
        }
      },
      getfactioncloth() {
        return this.selectedclothid;
      },
    updateOutfits(outfits)
    {
      this.outfits = JSON.parse(outfits);
      this.$forceUpdate();
    },
    createOutfit()
    {
        if ((Date.now() / 1000) > this.clicked) {
          if(this.outfitname.length < 3 || this.outfitname.length > 35)
          {
            // eslint-disable-next-line no-undef
            mp.trigger("Client:SendNotificationWithoutButton", 'Ungültiger Outfitname!', 'error', 'top-left', 2500);
            return;
          }
          // eslint-disable-next-line no-undef
          mp.trigger("Client:Createoutfit", this.outfitname, JSON.stringify(this.clothing), JSON.stringify(this.clothingColor));
          this.clicked = (Date.now() / 1000) + (1);
        }
    },
    deleteOutfit(id)
    {
      if ((Date.now() / 1000) > this.clicked) {
        // eslint-disable-next-line no-undef
        mp.trigger("Client:Deleteoutfit", id);
        this.outfits = this.outfits.filter(o => o.id !== id);
        // eslint-disable-next-line no-undef
        mp.trigger("Client:SendNotificationWithoutButton", 'Das Outfit wurde gelöscht!', 'success', 'top-left', 2500);
        this.$forceUpdate();
        this.clicked = (Date.now() / 1000) + (1);
      }
    },
    selectOutfit(outfit, bonus)
    {
      if ((Date.now() / 1000) > this.clicked) {
        if(bonus == 'EUP')
        {
          // eslint-disable-next-line no-undef
          mp.trigger("Client:WardrobeAktion", 'selectoutfit', outfit.name, 'EUP');
          this.clicked = (Date.now() / 1000) + (1);
        }
        else
        {
          this.clothing = JSON.parse(outfit.json1);
          this.clothingColor = JSON.parse(outfit.json2);
          this.selectCloth(this.selectedcloth);
          // eslint-disable-next-line no-undef
          mp.trigger("Client:SendNotificationWithoutButton", 'Gespeichertes Outfit ausgewählt!', 'success', 'top-left', 2500);
          // eslint-disable-next-line no-undef
          mp.trigger('Client:ChangeShopClothes', 'Kopfbedeckung', this.clothing[7], this.clothingColor[7]);
          // eslint-disable-next-line no-undef
          mp.trigger('Client:ChangeShopClothes', 'Brillen', this.clothing[6], this.clothingColor[6]);
          // eslint-disable-next-line no-undef
          mp.trigger('Client:ChangeShopClothes', 'Torso', this.clothing[1], this.clothingColor[1]);
          // eslint-disable-next-line no-undef
          mp.trigger('Client:ChangeShopClothes', 'Oberbekleidung', this.clothing[0], this.clothingColor[0]);
          // eslint-disable-next-line no-undef
          mp.trigger('Client:ChangeShopClothes', 'T-Shirt', this.clothing[4], this.clothingColor[4]);
          // eslint-disable-next-line no-undef
          mp.trigger('Client:ChangeShopClothes', 'Hosen', this.clothing[2], this.clothingColor[2]);
          // eslint-disable-next-line no-undef
          mp.trigger('Client:ChangeShopClothes', 'Schuh', this.clothing[3], this.clothingColor[3]);
          this.clicked = (Date.now() / 1000) + (1);
        }
      }
    },
    setRadioFreq(freq)
    {
      if ((Date.now() / 1000) > this.clicked) {
          // eslint-disable-next-line no-undef
          mp.trigger('Client:SetRadioFreq', freq);
          this.clicked = (Date.now() / 1000) + (1);
      }
    },
    showradiomenu(freq)
    {
      this.frequenz = freq;
      this.showradiosystem = !this.showradiosystem;
    },
    showmusicmenu(status)
    {
      this.showmusicstatus = status;
      this.showmusic = !this.showmusic;
    },
    showarrestmenu(arrestName)
    {
      this.arrestInfo = [];
      this.arrestName = arrestName;
      this.showticket = false;
      this.showarrest = !this.showarrest;
    },
    showticketmenu(ticketName)
    {
      this.arrestInfo = [];
      this.arrestName = ticketName;
      this.showarrest = false;
      this.showticket = !this.showticket;
    },
    hideArrest()
    {
      // eslint-disable-next-line no-undef
      mp.trigger('Client:ShowArrest', 'n/A');
    },
    setArrest()
    {
      if(!this.arrestInfo || this.arrestInfo[0].length < 3 || parseInt(this.arrestInfo[2]) > 10 || parseInt(this.arrestInfo[2]) < 1 || parseInt(this.arrestInfo[1]) < 1 || parseInt(this.arrestInfo[1]) > 7200)
      {
        // eslint-disable-next-line no-undef
        mp.trigger("Client:SendNotificationWithoutButton", 'Ungültige Angaben!', 'error', 'top-left', '3250');
        return;
      }
      // eslint-disable-next-line no-undef
      mp.trigger('Client:MDCSettings', 'setarrest', this.arrestInfo.join(','));
    },
    hideTicket()
    {
      // eslint-disable-next-line no-undef
      mp.trigger('Client:ShowTicket', 'n/A');
    },
    setTicket()
    {
      if(!this.arrestInfo || this.arrestInfo[0].length < 3 || parseInt(this.arrestInfo[1]) < 1 || parseInt(this.arrestInfo[1]) > 99999)
      {
        // eslint-disable-next-line no-undef
        mp.trigger("Client:SendNotificationWithoutButton", 'Ungültige Angaben!', 'error', 'top-left', '3250');
        return;
      }
      // eslint-disable-next-line no-undef
      mp.trigger('Client:MDCSettings', 'setticket', this.arrestInfo.join(','));
    },
    showPerso(data)
    {
      this.showperso = !this.showperso;
      this.persoData = JSON.parse(data);
    },
    showLics(lics, data)
    {
      this.showlic = !this.showlic;
      this.licData = lics.split('|');
      this.licData2 = JSON.parse(data);
    },
    clothPlus(max,set)
    {
      if(set == 1)
      {
        if(this.selectedclothid < max)
        {
          this.selectedclothid++;
          if(this.tattooshow)
          {
            this.changeTattoo();
          }
          if(this.clothshow)
          {
            this.clothingCustomize(1);
          }
          this.$forceUpdate();
        }
      }
      else
      {
        if(this.selectedclothid2 < max)
        {
          this.selectedclothid2++;
          if(this.tattooshow)
          {
            this.changeTattoo();
          }
          this.$forceUpdate();
        }
      }
    },
    clothMinus(min,set)
    {
      if(set == 1)
      {
        if(this.selectedclothid > min)
        {
          this.selectedclothid --;
          if(this.tattooshow)
          {
            this.changeTattoo();
          }
          if(this.clothshow)
          {
            self.clothingCustomize(1);
          }
          this.$forceUpdate();
        }
      }
      else
      {
        if(this.selectedclothid2 > min)
        {
          this.selectedclothid2 --;
          if(this.tattooshow)
          {
            this.changeTattoo();
          }
          this.$forceUpdate();
        }
      }
    },
    //Busdriver
    showBusPlan(routeName, stationName, allStations, busDriver)
    {
      this.busPlanShow = !this.busPlanShow;
      this.routeName = routeName;
      this.stationName = stationName;
      this.allStations = allStations.split(',');
      this.busDriver = busDriver;
    },
    //TextToSpeech
    textToSpeech(text)
    {
      var msg = new SpeechSynthesisUtterance();
      var voices = window.speechSynthesis.getVoices();
      msg.voice = voices[10]; 
      msg.volume = 0.6;
      msg.rate = 0.7; 
      msg.pitch = 0.8;
      msg.text = text;
      msg.lang = 'de';
      speechSynthesis.speak(msg);
      msg = null;
      voices = null;
    },
    //Tattoo shop
    selectTattoCheck(check)
    {
      this.selectedclothid = 0;
      this.zoneId = check;
      var json;
      var tempTattos = [];
      json = require('./helper/externals/tattoos.json');
      this.tattooJson = json;
      for (let i = 0; i <= this.tattooJson.length; i++) {
        {
          if (this.tattooJson[i]) {
            if (this.tattooJson[i].ZoneID == check || check == -1) {
              tempTattos.push(this.tattooJson[i]);
            }
          }
        }
      }
      this.tattooJson = tempTattos;
    },
    updateTattoos(json)
    {
      this.tattoos = JSON.parse(json);
    },
    buyTattoo(id)
    {
      if ((Date.now() / 1000) > this.clicked) {
        var hash;
        if(this.gender == 1)
        {
          hash = this.tattooJson[id].HashNameMale;
        }
        else
        {
          hash = this.tattooJson[id].HashNameFemale;
        }
        // eslint-disable-next-line no-undef
        mp.trigger('Client:BuyTattoo', hash, this.getTattooDlc(hash, this.tattooJson[id].LocalizedName), this.tattooJson[id].ZoneID);
        this.clicked = (Date.now() / 1000) + (1);
      }
    },
    resetTattoo() 
    {
      if ((Date.now() / 1000) > this.clicked) {
        // eslint-disable-next-line no-undef
        mp.trigger('Client:ResetTattooDirect');
        this.selectedclothid = 0;
        this.clicked = (Date.now() / 1000) + (1);
      }
    },
    abortTattoo() 
    {
      if ((Date.now() / 1000) > this.clicked) {
        // eslint-disable-next-line no-undef
        mp.trigger('Client:ResetTattoo2');
        // eslint-disable-next-line no-undef
        mp.trigger('Client:BuyTattoo', '-1', '-1', -2);
        this.clicked = (Date.now() / 1000) + (1);
      }
    },
    showTattoo(json, gender, multiplier) {
      this.tattooshow = !this.tattooshow;
      if(this.tattooshow)
      {
        this.tattoos = JSON.parse(json);
        this.selectTattoo = '';
        this.selectedclothid = -1;
        this.gender = gender;
        this.multiplier = multiplier;
        var self = this;
        setTimeout(function () {
          self.tattooSelect('Tattoo1');
        }, 75);
      }
    },
    hideTattoo() {
      this.tattooshow = false;
    },
    changeTattoo() {
      var tattooName;
      if(this.gender == 1)
      {
        tattooName = this.tattooJson[this.selectedclothid].HashNameMale;
      }
      else
      {
        tattooName = this.tattooJson[this.selectedclothid].HashNameFemale;
      }
      // eslint-disable-next-line no-undef
      mp.trigger('Client:ChangeTattoo', JSON.stringify(this.tattoos), tattooName, this.getTattooDlc(tattooName, this.tattooJson[this.selectedclothid].LocalizedName));
    },
    tattooSelect(text) {
      this.selectedclothid = 0;
      this.selectTattoo = text;
      var json;
      var tempTattos = [];
      json = require('./helper/externals/tattoos.json');
      this.tattooJson = json;
      for (let i = 0; i <= this.tattooJson.length; i++) {
        tempTattos.push(this.tattooJson[i]);
      }
      this.tattooJson = tempTattos;
    },
    getTattooDlc(tattooName, tattooName2)
    {
      if(tattooName.includes('MP_Airraces_'))
      {
        return 'mpairraces_overlays';
      }
      else if(tattooName.includes('MP_Bea_'))
      {
        return 'mpbeach_overlays';
      }
      else if(tattooName.includes('MP_MP_Biker_'))
      {
        return 'mpbiker_overlays';
      }
      else if(tattooName.includes('MP_Buis_'))
      {
        return 'mpbusiness_overlays';
      }
      else if(tattooName.includes('MP_Christmas2017_'))
      {
        return 'mpchristmas2017_overlays';
      }
      else if(tattooName.includes('MP_Christmas2018_'))
      {
        return 'mpchristmas2018_overlays';
      }
      else if(tattooName.includes('MP_Xmas2_'))
      {
        return 'mpchristmas2_overlays';
      }
      else if(tattooName.includes('MP_Gunrunning_'))
      {
        return 'mpgunrunning_overlays';
      }
      else if(tattooName.includes('mpHeist3_Tat_'))
      {
        return 'mpheist3_overlays';
      }
      else if(tattooName.includes('MP_Heist4_Tat_'))
      {
        return 'mpheist4_overlays';
      }
      else if(tattooName.includes('FM_Hip_'))
      {
        return 'mphipster_overlays';
      }
      else if(tattooName.includes('MP_MP_ImportExport_'))
      {
        return 'mpimportexport_overlays';
      }
      else if(tattooName2 == 'King Fight' || tattooName2 == 'Holy Mary' || tattooName2 == 'Gun Mic' || tattooName2 == 'No Evil' || tattooName2 == 'LS Serpent' || tattooName2 == 'Amazon' || tattooName2 == 'Bad Angel' || tattooName2 == 'Love Gamble' || tattooName2 == 'Love is Blind' || 
      tattooName2 == 'Seductress' || tattooName2 == 'Ink Me' || tattooName2 == 'Presidents' || tattooName2 == 'Sad Angel' || tattooName2 == 'Dance of Hearts' || tattooName2 == 'Royal Takeover' || tattooName2 == 'Los Santos Life' || tattooName2 == 'City Sorrow')
      {
        return 'mplowrider_overlays';
      }
      else if(tattooName2 == 'SA Assault' || tattooName2 == 'Lady Vamp' || tattooName2 == 'Love Hustle' || tattooName2 == 'Love the Game' || tattooName2 == 'Lady Liberty' || tattooName2 == 'Royal Kiss' || tattooName2 == 'Two Face' || tattooName2 == 'Skeleton Party' || tattooName2 == 'Death Behind' || 
      tattooName2 == 'My Crazy Life' || tattooName2 == 'Loving Los Muertos' || tattooName2 == 'Death Us Do Part' || tattooName2 == 'San Andreas Prayer' || tattooName2 == 'Dead Pretty' || tattooName2 == 'Reign Over' || tattooName2 == 'Black Tears')
      {
        return 'mplowrider2_overlays';
      }
      else if(tattooName2 == 'Serpent of Death' || tattooName2 == 'Elaborate Los Muertos' || tattooName2 == 'Abstract Skull' || tattooName2 == 'Floral Raven' || tattooName2 == 'Adorned Wolf' || tattooName2 == 'Eye of the Griffin' || tattooName2 == 'Flying Eye' || tattooName2 == 'Floral Symmetry' || tattooName2 == 'Mermaid Harpist' || 
      tattooName2 == 'Ancient Queen' || tattooName2 == 'Smoking Sisters' || tattooName2 == 'Geisha Bloom' || tattooName2 == 'Archangel & Mary' || tattooName2 == 'Gabriel' || tattooName2 == 'Feather Mural')
      {
        return 'mpluxe_overlays';
      }
      else if(tattooName2 == 'The Howler' || tattooName2 == 'Fatal Dagger' || tattooName2 == 'Intrometric' || tattooName2 == 'Cross of Roses' || tattooName2 == 'Geometric Galaxy' || tattooName2 == 'Egyptian Mural' || tattooName2 == 'Heavenly Deity' || tattooName2 == 'Divine Goddess' || tattooName2 == 'Cloaked Angel' || 
      tattooName2 == 'Starmetric' || tattooName2 == 'Reaper Sway' || tattooName2 == 'Floral Print' || tattooName2 == 'Cobra Dawn' || tattooName2 == 'Python Skull' || tattooName2 == 'Geometric Design' || tattooName2 == 'Geometric Design' || tattooName2 == 'Geometric Design')
      {
        return 'mpluxe2_overlays';
      }
      else if(tattooName.includes('MP_Security_'))
      {
        return 'mpsecurity_overlays';
      }
      else if(tattooName.includes('MP_Smuggler_'))
      {
        return 'mpsmuggler_overlays';
      }
      else if(tattooName.includes('MP_MP_Stunt_'))
      {
        return 'mpstunt_overlays';
      }
      else if(tattooName.includes('MP_Vinewood_'))
      {
        return 'mpvinewood_overlays';
      }
      else if(tattooName.includes('FM_Tat_'))
      {
        return 'multiplayer_overlays';
      }
    },
    //Dealership
    testDealerShip() {
      if ((Date.now() / 1000) > this.clicked) {
        // eslint-disable-next-line no-undef
        mp.trigger('Client:DealerShipSettings', 'testdrive', this.dealerShipVehicle.vehiclename);
        this.clicked = (Date.now() / 1000) + (1);
      }
    },
    buyVehicle(mod) {
      if ((Date.now() / 1000) > this.clicked) {
        // eslint-disable-next-line no-undef
        mp.trigger('Client:DealerShipSettings', 'buyvehicle', this.dealerShipVehicle.vehiclename + "," + this.dealerShipVehicle.price + "," + mod);
        this.clicked = (Date.now() / 1000) + (1);
      }
    },
    abortDealerShip() {
      if ((Date.now() / 1000) > this.clicked) {
        // eslint-disable-next-line no-undef
        mp.trigger('Client:DealerShipSettings', 'abort', 'n/A');
        this.clicked = (Date.now() / 1000) + (1);
      }
    },
    selectDealerShipVehicle(name) {
      for (const key in this.dealerShipVehicles) {
        if (this.dealerShipVehicles[key].vehiclename == name) {
          this.dealerShipVehicle = this.dealerShipVehicles[key];
        }
      }
      // eslint-disable-next-line no-undef
      mp.trigger('Client:UpdateDealerShipVehicle', name);
    },
    selectColor(color) {
      // eslint-disable-next-line no-undef
      mp.trigger('Client:UpdateDealerShipColor', color);
    },
    showDealerShip(name, json1, csv, multiplier) {
      Swal.close();
      // eslint-disable-next-line no-undef
      mp.trigger('Client:StopAllNotifications');
      this.multiplier = multiplier;
      this.showdealershipMenu = !this.showdealershipMenu;
      if (this.showdealershipMenu == true) {
        this.vehicleColors = getVehicleColors;
        var count = 3;
        for (const key in this.vehicleColors) {
          if (key != 0 && key != 4 && this.dealerColors.length < 48) {
            count++;
            if (count >= 3) {
              this.dealerColors.push(getVehicleColors[key]);
              count = 0;
            }
          }
        }
        this.dealerShipName = name;
        this.dealerShipVehicles = JSON.parse(json1);

        this.dealerShipVehicles.sort(function (a, b) {
          var nameA = a.vehiclename.toUpperCase();
          var nameB = b.vehiclename.toUpperCase();
          if (nameA < nameB) {
            return -1;
          }
          if (nameA > nameB) {
            return 1;
          }
          return 0;
        });

        this.dealerShipVehicle = this.dealerShipVehicles[0];
        this.dealerShipMax = csv.split(',');
      }
    },
    //Ammunation
    updateWeaponComponents(csv) {
      this.compNow = [];
      this.compNow = csv.split('|');
    },
    setWeaponComponent(index, component, choose) {
      if ((Date.now() / 1000) > this.weaponCooldown) {
        if (index == 2) {
          if (component.includes("CLIP")) {
            for (const key2 in this.compNow) {
              if (this.compNow[key2].includes("CLIP")) {
                // eslint-disable-next-line no-undef
                mp.trigger("Client:SendNotificationWithoutButton", 'Von dieser Komponente kann immer nur eine an der Waffe angebracht werden!', 'error', 'top-left', '3250');
                return;
              }
            }
          }
        }
        // eslint-disable-next-line no-undef
        mp.trigger('Client:WeaponSet', index, component, choose);
        this.weaponCooldown = (Date.now() / 1000) + (2);
      }
    },
    showAmmunation(item, price1, price2, tint, weapon, check) {
      this.ammunationshow = !this.ammunationshow;
      if (this.ammunationshow == true) {
        this.item = JSON.parse(item);
        this.ammuPrice1 = price1;
        this.ammuPrice2 = price2;
        this.ammuCheck = check;
        this.getWeapon = weapon;
        this.selectedWeapon = 'Tint';
        this.weaponNumber2 = 0;

        var weaponTints = '';
        if (this.item && this.item.description.toLowerCase().includes('mk2')) {
          weaponTints = 'Schwarz(Standard),Grau,Weiss/Schwarz,Weiss,Beige,Green,Blau,Braun,Braun/Schwarz,Rot Kontrast,Blau Kontrast,Geld Kontrast,Orange Kontrast,Pink,Lila/Gelb,Orange,Grün/Lila,Rot (Features),Grün (Features),Cyan (Features),Gelb (Features),Rot/Weiss,Blau/Weiss,Gold,Platinum,Grau/Lila,Lila/Limette,Metallic Rot,Metallic Grün,Metallic Blau,Weiss/Aqua,Rot/Gelb';
          this.weaponNames = weaponTints.split(',');
        } else {
          weaponTints = 'Schwarz(Standard),Grün,Gold,Pink,Armee,LSPD,Orange,Platinum';
          this.weaponNames = weaponTints.split(',');
        }

        var temp = this.item.props.split(',')[6];
        if (temp && temp.length > 3) {
          this.compNow = temp.split('|');
        }
        this.weaponNumber = tint;
        this.weaponNumberOld = tint;
      }
    },
    resetWeaponComponent(index, component) {
      if ((Date.now() / 1000) > this.weaponCooldown) {
        this.weaponNumber2 = 0;
        if (index == 1) {
          this.weaponNumber = this.weaponNumberOld;
          // eslint-disable-next-line no-undef
          mp.trigger('Client:ResetWeaponComp', index, 'n/A');
        } else {
          // eslint-disable-next-line no-undef
          mp.trigger('Client:ResetWeaponComp', index, component);
        }
        this.weaponCooldown = (Date.now() / 1000) + (2);
        this.$forceUpdate();
      }
    },
    resetComponents(set) {
      this.weaponNumber2 = 0;
      for (const key in this.weaponComponents) {
        if (this.weaponComponents[key].HashKey && !this.compNow.includes(this.weaponComponents[key].HashKey)) {
          // eslint-disable-next-line no-undef
          mp.trigger('Client:RemoveWeaponComponent', this.weaponComponents[key].HashKey);
        }
      }
      if (set == 1) {
        for (const key2 in this.compNow) {
          // eslint-disable-next-line no-undef
          mp.trigger('Client:WeaponPreview', 2, this.compNow[key2]);
        }
      }
    },
    weaponPreview(index, component) {
      // eslint-disable-next-line no-undef
      mp.trigger('Client:WeaponPreview', index, component);
      return;
    },
    selectWeapon(weapon) {
      var self = this;
      setTimeout(function () {
        if (weapon == "Components") {
          var json = require('./helper/externals/weapons.json');
          var obj;
          self.weaponComponents = [];
          for (const key in json) {
            if (json[key].HashKey == "WEAPON_" + self.getWeapon.toUpperCase()) {
              obj = json[key].Components;
              for (const key in obj) {
                if (obj[key].IsDefault == false && !obj[key].Name.includes('Luxury') && !obj[key].Name.includes('FMJ') && !obj[key].Name.includes('TRACER') && !obj[key].Name.includes('INCENDIARY') && !obj[key].Name.includes('ARMORPIERCING') && !obj[key].Name.includes('HOLLOWPOINT')) {
                  self.weaponComponents.push(obj[key]);
                }
              }
            }
          }
          if (self.weaponComponents.length <= 0) {
            self.sendNotificationWithoutButton('Diese Waffe hat keine zusätzlichen Komponenten!', 'error', 'top-left', 2500);
            return;
          } else {
            self.selectedWeapon = weapon;
          }
        } else {
          self.selectedWeapon = weapon;
        }
      }, 35);
    },
    //Tuning
    syncTuningFunc(set) {
      if ((Date.now() / 1000) > this.tuningSync || set == 0) {
        if (this.tuningCosts <= 0 && set == 1) {
          // eslint-disable-next-line no-undef
          mp.trigger("Client:SendNotificationWithoutButton", 'Es wurden keine neuen Tuningteile angebracht!', 'error', 'top-left', '3250');
          return;
        }
        // eslint-disable-next-line no-undef
        mp.trigger('Client:TuningSync', this.componentNumbers.join(','));
        if (set == 1) {
          // eslint-disable-next-line no-undef
          mp.trigger("Client:SendNotificationWithoutButton", 'Das Tuning wurde synchronisiert!', 'success', 'top-left', '3250');
          this.tuningSync = (Date.now() / 1000) + (10);
        }
      } else {
        if (set == 1) {
          // eslint-disable-next-line no-undef
          mp.trigger("Client:SendNotificationWithoutButton", 'Du musst kurz warten, bevor du das Tuning wieder synchronisieren kannst!', 'error', 'top-left', '3250');
        }
      }
    },
    getTuningComp() {
      let count = 0;
      for (let i = 0; i <= 69; i++) {
        if (this.componentNumbers[i] != this.componentNumbersBackup[i] && !isNaN(this.componentNumbersBackup[i])) {
          if (i == 66 || i == 67 || i == 68 || i == 69) {
            count += 8;
          } else {
            count += this.tuningCost[i];
          }
        }
      }
      return count;
    },
    resetTuning(set) {
      if ((Date.now() / 1000) > this.tuningReset || set == 0) {
        if (this.tuningCosts <= 0 && set == 1) {
          // eslint-disable-next-line no-undef
          mp.trigger("Client:SendNotificationWithoutButton", 'Es wurden keine neuen Tuningteile angebracht!', 'error', 'top-left', '3250');
          return;
        }
        if (set == 1) {
          this.tuningReset = (Date.now() / 1000) + (7);
        }
        for (let i = 0; i < this.componentNumbersBackup.length; i++) {
          if (this.componentNumbersBackup[i]) {
            this.componentNumbers[i] = this.componentNumbersBackup[i];
            this.tuningPreview(i, this.componentNumbersBackup[i]);
          }
        }
        this.tuningCosts = this.getTuningComp();
        this.perlmutt1 = false;
        if (parseInt(set) == 1) {
          // eslint-disable-next-line no-undef
          mp.trigger("Client:SendNotificationWithoutButton", 'Das Tuning wurde resetet!', 'success', 'top-left', '3250');
        }
      } else {
        if (parseInt(set) == 1) {
          // eslint-disable-next-line no-undef
          mp.trigger("Client:SendNotificationWithoutButton", 'Du musst kurz warten, bevor du das Tuning wieder reseten kannst!', 'error', 'top-left', '3250');
        }
      }
    },
    setTuning() {
      if ((Date.now() / 1000) > this.tuningSet) {
        if (this.tuningCosts > 0) {
          if (this.componentNumbers[11] > -1) {
            if (this.componentNumbers[11] > this.componentNumbersBackup[11] && (this.componentNumbers[11] - 1 == this.componentNumbers[11])) {
              // eslint-disable-next-line no-undef
              mp.trigger("Client:SendNotificationWithoutButton", 'Du kannst diese Tuningkomponente nur schrittweise erhöhen!', 'error', 'top-left', '3250');
              return;
            }
          }
          if (this.componentNumbers[12] > -1) {
            if (this.componentNumbers[12] > this.componentNumbersBackup[12] && (this.componentNumbers[12] - 1 == this.componentNumbers[12])) {
              // eslint-disable-next-line no-undef
              mp.trigger("Client:SendNotificationWithoutButton", 'Du kannst diese Tuningkomponente nur schrittweise erhöhen!', 'error', 'top-left', '3250');
              return;
            }
          }
          if (this.componentNumbers[13] > -1) {
            if (this.componentNumbers[13] > this.componentNumbersBackup[13] && (this.componentNumbers[13] - 1 == this.componentNumbers[13])) {
              // eslint-disable-next-line no-undef
              mp.trigger("Client:SendNotificationWithoutButton", 'Du kannst diese Tuningkomponente nur schrittweise erhöhen!', 'error', 'top-left', '3250');
              return;
            }
          }
          // eslint-disable-next-line no-undef
          mp.trigger('Client:TuningSet', this.componentNumbers.join(','), this.tuningCosts);
          this.tuningSet = (Date.now() / 1000) + (10);
        } else {
          // eslint-disable-next-line no-undef
          mp.trigger("Client:SendNotificationWithoutButton", 'Es wurden keine neuen Tuningteile angebracht!', 'error', 'top-left', '3250');
        }
      }
    },
    setPerlmutt() {
      this.perlmutt1 = !this.perlmutt1;
    },
    resetPerlmutt() {
      if (this.perlmutt1 == true) {
        this.perlmutt1 = false;
        this.tuningPreview(69, 255);
        // eslint-disable-next-line no-undef
        mp.trigger("Client:SendNotificationWithoutButton", 'Der Perlmutt Effekt wurde resetet!', 'success', 'top-left', '3250');
      } else {
        // eslint-disable-next-line no-undef
        mp.trigger("Client:SendNotificationWithoutButton", 'Kein Perlmutt Effekt vorhanden!', 'error', 'top-left', '3250');
      }
    },
    checkTuning(index) {
      let check = false;
      for (let i = 0; i <= 56; i++) {
        if (index == 1) {
          if (i > 10) break;
          if (this.maxTuning[i] != -1) {
            check = true;
          }
        } else if (index == 2) {
          if (i <= 10) continue;
          if (i > 24) break;
          if (this.maxTuning[i] != -1) {
            check = true;
          }
        } else if (index == 3) {
          if (i <= 24) continue;
          if (this.maxTuning[i] > -1) {
            check = true;
          }
        }
      }
      if (check == true) {
        return 1;
      }
      return 0;
    },
    getComponentName(index) {
      if (index == 11) {
        if (this.componentNumbers[index] == -1) {
          return 'Standard Motor';
        } else if (this.componentNumbers[index] == 0) {
          return 'EMS-Verbesserung 1';
        } else if (this.componentNumbers[index] == 1) {
          return 'EMS-Verbesserung 2';
        } else if (this.componentNumbers[index] == 2) {
          return 'EMS-Verbesserung 3';
        } else if (this.componentNumbers[index] == 3) {
          return 'EMS-Verbesserung 4';
        }
      } else if (index == 12) {
        if (this.componentNumbers[index] == -1) {
          return 'Standardbremsen';
        } else if (this.componentNumbers[index] == 0) {
          return 'Straßenbremsen';
        } else if (this.componentNumbers[index] == 1) {
          return 'Sportbremsen';
        } else if (this.componentNumbers[index] == 2) {
          return 'Rennbremsen';
        }
      } else if (index == 13) {
        if (this.componentNumbers[index] == -1) {
          return 'Standardgetriebe';
        } else if (this.componentNumbers[index] == 0) {
          return 'Straßengetriebe';
        } else if (this.componentNumbers[index] == 1) {
          return 'Sportgetriebe';
        } else if (this.componentNumbers[index] == 2) {
          return 'Renngetriebe';
        }
      } else if (index == 22) {
        if (this.componentNumbers[index] == -1) {
          return 'Keine';
        } else if (this.componentNumbers[index] == 0) {
          return 'Weiss';
        } else if (this.componentNumbers[index] == 1) {
          return 'Blau';
        } else if (this.componentNumbers[index] == 2) {
          return 'Hellblau';
        } else if (this.componentNumbers[index] == 3) {
          return 'Hellgrün';
        } else if (this.componentNumbers[index] == 4) {
          return 'Hellgelb';
        } else if (this.componentNumbers[index] == 5) {
          return 'Gelb';
        } else if (this.componentNumbers[index] == 6) {
          return 'Orange';
        } else if (this.componentNumbers[index] == 7) {
          return 'Rot';
        } else if (this.componentNumbers[index] == 8) {
          return 'Hellpink';
        } else if (this.componentNumbers[index] == 9) {
          return 'Pink';
        } else if (this.componentNumbers[index] == 10) {
          return 'Lila';
        } else if (this.componentNumbers[index] == 11) {
          return 'Helllila';
        }
      } else if (index == 56 || index == 57) {
        if (this.componentNumbers[index] == -1) {
          if (index == 56) {
            return 'Kein Neon';
          } else {
            return 'Kein Reifenqualm';
          }
        } else if (this.componentNumbers[index] == 0) {
          return 'Rot';
        } else if (this.componentNumbers[index] == 1) {
          return 'Blau';
        } else if (this.componentNumbers[index] == 2) {
          return 'Grün';
        } else if (this.componentNumbers[index] == 3) {
          return 'Gelb';
        } else if (this.componentNumbers[index] == 4) {
          return 'Lila';
        } else if (this.componentNumbers[index] == 5) {
          return 'Orange';
        } else if (this.componentNumbers[index] == 6) {
          return 'Türkis';
        } else if (this.componentNumbers[index] == 7) {
          return 'Rosa';
        } else if (this.componentNumbers[index] == 8) {
          return 'Kadet Blau';
        } else if (this.componentNumbers[index] == 9) {
          return 'Weiss';
        } else if (this.componentNumbers[index] == 10) {
          return 'Schwarz';
        }
      } else if (index == 40) {
        if (this.componentNumbers[index] == -1) {
          return 'Kein Nitro';
        } else if (this.componentNumbers[index] == 0) {
          return 'Nitro';
        }
      } else {
        return this.componentNumbers[index];
      }
    },
    showTuning(csvString, stock) {
      if (this.tuningshow == false) {
        this.componentNumbers = csvString.split(',');
        for (let i = 0; i < this.componentNumbers.length; i++) {
          if (this.componentNumbers[i]) {
            this.componentNumbersBackup[i] = this.componentNumbers[i];
          }
        }
        this.vehicleColors = getVehicleColors;
        this.perlmutt1 = false;
        this.selectedTuning = 'Farbe 1';
        this.tuningSync = (Date.now() / 1000);
        this.tuningSet = (Date.now() / 1000);
        this.stockTuning = parseInt(stock);
        this.tuningCosts = 0;
        this.syncTuning = (Date.now() / 1000);
        this.tuningReset = (Date.now() / 1000);
        // eslint-disable-next-line no-undef
        mp.trigger('Client:GetMaxTuning');
        // eslint-disable-next-line no-undef
        mp.trigger('Client:GetDefaultChiptuning');
      }
      var self = this;
      setTimeout(function () {
        self.tuningshow = !self.tuningshow;
      }, 55);
    },
    tuningPreview(tuning, component) {
      let perlm = false;
      if (tuning == 66) {
        perlm = this.perlmutt1;
        if (perlm == true) {
          this.componentNumbers[69] = component;
          this.$forceUpdate();
        } else {
          this.componentNumbers[66] = component;
        }
      } else if (tuning == 67) {
        this.componentNumbers[67] = component;
      } else if (tuning == 68) {
        this.componentNumbers[68] = component;
      } else if (tuning == 69) {
        perlm = this.perlmutt1;
        this.componentNumbers[69] = component;
        this.tuningCosts = this.getTuningComp();
      }
      // eslint-disable-next-line no-undef
      mp.trigger('Client:TuningPreview', tuning, component, perlm);
    },
    selectTuning(tuning) {
      var self = this;
      if (tuning == 'Analyse') {
        this.tuningCosts = this.getTuningComp();
      }
      setTimeout(function () {
        self.selectedTuning = tuning;
      }, 35);
    },
    setMaxTuning(max) {
      this.maxTuning = JSON.parse(max);
    },
    setDefaultChiptuning(handling) {
      this.defaultHandling = JSON.parse(handling);
      this.componentNumbers[58] = this.defaultHandling[0];
      this.componentNumbers[59] = this.defaultHandling[1];
      this.componentNumbers[60] = this.defaultHandling[2];
      this.componentNumbers[61] = this.defaultHandling[3];
      this.componentNumbers[62] = this.defaultHandling[4];
      this.componentNumbers[63] = this.defaultHandling[5];
      this.$forceUpdate();
    },
    showFuelStation: function (price, fuel, maxfuel, bizzproducts) {
      if (this.showfuel == true) return;
      this.fuelstatus = false;
      this.getfuelprice = parseInt(price);
      this.fuel = parseFloat(fuel);
      this.newfuel = 0;
      this.maxfuel = parseFloat(maxfuel);
      this.bizzproducts = parseInt(bizzproducts);
      this.fuelprice = 0;
      this.showfuel = true;
      var count = 0;
      var self = this;
      if (fuelTimer != null) {
        clearInterval(fuelTimer);
        fuelTimer = null;
      }
      fuelTimer = setInterval(function () {
        // eslint-disable-next-line no-undef
        $('.progress-bar').css('width', parseInt(((self.fuel / self.maxfuel) * 100)) + '%').attr('aria-valuenow', parseInt(((self.fuel / self.maxfuel) * 100)));
        if (count >= 2) {
          clearInterval(fuelTimer);
        }
      }, 650);
    },
    hideFuelStation: function (set) {
      if (this.showfuel == false) return;
      if (fuelTimer != null) {
        clearInterval(fuelTimer);
        fuelTimer = null;
      }
      if (set == 0) {
        // eslint-disable-next-line no-undef
        mp.trigger('Client:GetFuel', this.fuelprice, parseFloat(this.fuel), parseFloat(this.newfuel));
      }
      this.showfuel = false;
      this.fuelstatus = false;
      this.newfuel = 0;
      this.fuelprice = 0;
    },
    startStopFuel: function () {
      if ((Date.now() / 1000) > this.clicked) {
        this.clicked = (Date.now() / 1000) + (1);
        var self = this;
        if (self.fuelstatus == false && self.bizzproducts > 0 && self.fuel < self.maxfuel) {
          self.fuelstatus = true;
          if (fuelTimer != null) {
            clearInterval(fuelTimer);
            fuelTimer = null;
          }
          fuelTimer = setInterval(function () {
            self.fuelprice += self.getfuelprice;
            self.fuel += 1.0;
            self.newfuel += 1.0;
            self.bizzproducts = parseInt(self.bizzproducts - 1);
            // eslint-disable-next-line no-undef
            $('.progress-bar').css('width', parseInt(((parseInt(self.fuel) / parseInt(self.maxfuel)) * 100)) + '%').attr('aria-valuenow', parseInt(((parseInt(self.fuel) / parseInt(self.maxfuel)) * 100)));
            if (self.fuel >= self.maxfuel || self.bizzproducts <= 0) {
              self.fuel = self.maxfuel;
              self.fuelstatus = false;
              clearInterval(fuelTimer);
              fuelTimer = null;
            }
          }, 650);
        } else {
          self.fuelstatus = false;
          clearInterval(fuelTimer);
          fuelTimer = null;
        }
      }
    },
    getFuelText: function () {
      if (this.fuelstatus == false) {
        return "Tanken - " + parseFloat(this.maxfuel - this.fuel).toFixed(1) + "l";
      } else {
        return "Tankvorgang abbrechen"
      }
    },
    getBarberPrice: function () {
      var count = 0;
      if (this.multiplier <= 0) {
        this.multiplier = 1;
      }
      if (this.oldhair != this.selectedclothid || this.oldhaircolor != this.selectedclothcolor) {
        count += parseInt(475 * this.multiplier);
      }
      if (this.oldhair2 != this.selectedclothid2 || this.oldhaircolor2 != this.selectedclothcolor2) {
        count += parseInt(475 * this.multiplier);
      }
      if (this.oldhair3 != this.selectedclothid3) {
        count += parseInt(475 * this.multiplier);
      }
      if (this.headOverlays[0] != this.headOverlaysB[0]) {
        count += parseInt(275 * this.multiplier);
      }
      if (this.headOverlays[1] != this.headOverlaysB[1]) {
        count += parseInt(275 * this.multiplier);
      }
      if (this.headOverlays[2] != this.headOverlaysB[2]) {
        count += parseInt(275 * this.multiplier);
      }
      return parseInt(count);
    },
    hairCustomize: function (selected, getColor) {
      if (selected == 1 || selected == 4) {
        if (selected == 1) {
          this.selectedclothcolor = getColor;
        } else {
          this.selectedclothid3 = getColor;
        }
        // eslint-disable-next-line no-undef
        mp.trigger('Client:ChangeShopHair', 1, parseInt(this.selectedclothid), this.selectedclothcolor, this.selectedclothid3);
      } else if (selected == 2) {
        this.selectedclothcolor2 = getColor;
        // eslint-disable-next-line no-undef
        mp.trigger('Client:ChangeShopHair', 2, parseInt(this.selectedclothid2), getColor, -1);
      }
    },
    miscCustomize: function (selected) {
      if (selected == 3) {
        // eslint-disable-next-line no-undef
        mp.trigger('Client:ChangeOverlay', 2, parseInt(this.headOverlays[0]), this.headOverlaysColors[0]);
      } else if (selected == 4) {
        // eslint-disable-next-line no-undef
        mp.trigger('Client:ChangeOverlay', 4, parseInt(this.headOverlays[1]), this.headOverlaysColors[1]);
      } else if (selected == 5) {
        // eslint-disable-next-line no-undef
        mp.trigger('Client:ChangeOverlay', 8, parseInt(this.headOverlays[2]), this.headOverlaysColors[2]);
      }
    },
    resetHair: function () {
      if ((Date.now() / 1000) > this.clicked) {
        this.clicked = (Date.now() / 1000) + (1);
        this.selectedclothid = this.oldhair;
        this.selectedclothcolor = this.oldhaircolor;
        this.selectedclothid2 = this.oldhair2;
        this.selectedclothcolor2 = this.oldhaircolor2;
        this.selectedclothid3 = this.oldhair3;
        this.headOverlays[0] = this.headOverlaysB[0];
        this.headOverlays[1] = this.headOverlaysB[1];
        this.headOverlays[2] = this.headOverlaysB[2];
        this.headOverlaysColors[0] = this.headOverlaysColorsB[0];
        this.headOverlaysColors[1] = this.headOverlaysColorsB[1];
        this.headOverlaysColors[2] = this.headOverlaysColorsB[2];
        this.$forceUpdate();
        // eslint-disable-next-line no-undef
        mp.trigger('Client:ChangeShopHair', 1, parseInt(this.oldhair), this.selectedclothcolor, this.oldhair3);
        // eslint-disable-next-line no-undef
        mp.trigger('Client:ChangeShopHair', 2, parseInt(this.oldhair2), this.oldhaircolor2, -1);
        // eslint-disable-next-line no-undef
        mp.trigger('Client:ChangeOverlay', 2, parseInt(this.headOverlays[0]), this.headOverlaysColors[0]);
        // eslint-disable-next-line no-undef
        mp.trigger('Client:ChangeOverlay', 4, parseInt(this.headOverlays[1]), this.headOverlaysColors[1]);
        // eslint-disable-next-line no-undef
        mp.trigger('Client:ChangeOverlay', 8, parseInt(this.headOverlays[2]), this.headOverlaysColors[2]);
        this.sendNotificationWithoutButton('Haare, Bart und Sonstiges wurden resetet!', 'success', 'top-left', 2500);
      }
    },
    abortHair: function () {
      if ((Date.now() / 1000) > this.clicked) {
        this.clicked = (Date.now() / 1000) + (1);
        this.selectedclothid = this.oldhair;
        this.selectedclothcolor = this.oldhaircolor;
        this.selectedclothid2 = this.oldhair2;
        this.selectedclothcolor2 = this.oldhaircolor2;
        this.selectedclothid3 = this.oldhair3;
        this.headOverlays[0] = this.headOverlaysB[0];
        this.headOverlays[1] = this.headOverlaysB[1];
        this.headOverlays[2] = this.headOverlaysB[2];
        this.headOverlaysColors[0] = this.headOverlaysColorsB[0];
        this.headOverlaysColors[1] = this.headOverlaysColorsB[1];
        this.headOverlaysColors[2] = this.headOverlaysColorsB[2];
        this.$forceUpdate();
        // eslint-disable-next-line no-undef
        mp.trigger('Client:ChangeShopHair', 1, parseInt(this.oldhair), this.oldhaircolor, this.selectedclothid3);
        // eslint-disable-next-line no-undef
        mp.trigger('Client:ChangeShopHair', 2, parseInt(this.oldhair2), this.oldhaircolor2, -1);
        // eslint-disable-next-line no-undef
        mp.trigger('Client:ChangeOverlay', 2, parseInt(this.headOverlays[0]), this.headOverlaysColors[0]);
        // eslint-disable-next-line no-undef
        mp.trigger('Client:ChangeOverlay', 4, parseInt(this.headOverlays[1]), this.headOverlaysColors[1]);
        // eslint-disable-next-line no-undef
        mp.trigger('Client:ChangeOverlay', 8, parseInt(this.headOverlays[2]), this.headOverlaysColors[2]);
        setTimeout(function () {
          // eslint-disable-next-line no-undef
          mp.trigger('Client:ChangeShopHair', 3, -1, -1, -1);
        }, 555);
      }
    },
    buyHair: function () {
      if ((Date.now() / 1000) > this.clicked) {
        if (this.gender == 1) {
          if (invalidHairMen == this.selectedclothid) {
            Swal.fire({
              icon: 'error',
              title: 'Fehler',
              text: 'Ungültiger Haarstil, bitte wähle einen anderen!',
              showConfirmButton: false,
              timer: 2500
            })
            return;
          }
        }
        if (this.gender == 2) {
          if (invalidHairWomen == this.selectedclothid) {
            Swal.fire({
              icon: 'error',
              title: 'Fehler',
              text: 'Ungültiger Haarstil, bitte wähle einen anderen!',
              showConfirmButton: false,
              timer: 2500
            })
            return;
          }
        }
        this.clicked = (Date.now() / 1000) + (1);
        // eslint-disable-next-line no-undef
        mp.trigger('Client:BuyShopHair', parseInt(this.selectedclothid), this.selectedclothcolor, parseInt(this.selectedclothid2), this.selectedclothcolor2, parseInt(this.selectedclothid3), this.getBarberPrice(), this.headOverlays[0], this.headOverlaysColors[0], this.headOverlays[1], this.headOverlaysColors[1], this.headOverlays[2], this.headOverlaysColors[2]);
      }
    },
    buyCloth: function (event = 'none') {
      if ((Date.now() / 1000) > this.clicked) {
        if(event == 'dutyCloths' && (this.faction == 1 || this.faction == 2 || this.faction == 3 || this.faction == 1337))
        {
          // eslint-disable-next-line no-undef
          mp.trigger('Client:BuyCloths', JSON.stringify(this.clothing), JSON.stringify(this.clothingColor), this.clothcost, true);
          return;
        }
        if (this.clothshow4 == true || this.clothcost > 0) {
          if (this.gender == 1) {
            if(this.clothshow == true)
            {
              if (invalidMenTop && invalidMenTop.includes(this.clothing[0])) {
                this.sendNotificationWithoutButton('Ungültige Oberbekleidung, bitte wähle eine andere!', 'error', 'top-left', 2500)
                return;
              }
              if (invalidMenTorso && invalidMenTorso.includes(this.clothing[1])) {
                this.sendNotificationWithoutButton('Ungültiger Torso, bitte wähle eine anderen!', 'error', 'top-left', 2500)
                return;
              }
              if (invalidMenLeg && invalidMenLeg.includes(this.clothing[2])) {
                this.sendNotificationWithoutButton('Ungültige Hose, bitte wähle andere!', 'error', 'top-left', 2500)
                return;
              }
              if (invalidMenShoe && invalidMenShoe.includes(this.clothing[3])) {
                this.sendNotificationWithoutButton('Ungültige Schuhe, bitte wähle eine andere!', 'error', 'top-left', 2500)
                return;
              }
              if (invalidMenTShirt && invalidMenTShirt.includes(this.clothing[4])) {
                this.sendNotificationWithoutButton('Ungültiges T-Shirt, bitte wähle ein anderes!', 'error', 'top-left', 2500)
                return;
              }
              if (invalidMenBags && invalidMenBags.includes(this.clothing[5]) && event != 'dutyCloths') {
                this.sendNotificationWithoutButton('Ungültiger Rucksack, bitte wähle einen anderen!', 'error', 'top-left', 2500)
                return;
              }
              if (invalidMenGlasses && invalidMenGlasses.includes(this.clothing[6])) {
                this.sendNotificationWithoutButton('Ungültige Brille, bitte wähle einen andere!', 'error', 'top-left', 2500)
                return;
              }
              if (invalidMenHats && invalidMenHats.includes(this.clothing[7])) {
                this.sendNotificationWithoutButton('Ungültige Kopfbedeckung, bitte wähle einen andere!', 'error', 'top-left', 2500)
                return;
              }
            }
            if(this.clothshow2 == true)
            {
              if (invalidMenOhrring && invalidMenOhrring.includes(this.clothing[9])) {
                this.sendNotificationWithoutButton('Ungültiger Ohrring, bitte wähle einen anderen!', 'error', 'top-left', 2500)
                return;
              }
              if (invalidMenArmbanduhr && invalidMenArmbanduhr.includes(this.clothing[10])) {
                this.sendNotificationWithoutButton('Ungültige Armbanduhr, bitte wähle eine andere!', 'error', 'top-left', 2500)
                return;
              }
              if (invalidMenArmring && invalidMenArmring.includes(this.clothing[11])) {
                this.sendNotificationWithoutButton('Ungültiger Armring, bitte wähle einen anderen!', 'error', 'top-left', 2500)
                return;
              }
              if (invalidAccessoiresMen && invalidAccessoiresMen.includes(this.clothing[12])) {
                this.sendNotificationWithoutButton('Ungültiges Accessoire, bitte wähle ein anderes!', 'error', 'top-left', 2500)
                return;
              }
            }
            if(this.clothshow3 == true)
            {
              if (invalidMask && invalidMask.includes(this.clothing[8])) {
                this.sendNotificationWithoutButton('Ungültige Maske, bitte wähle einen anderen!', 'error', 'top-left', 2500)
                return;
              }
            }
          } else {
            if(this.clothshow == true)
            {
              if (invalidWomanTop && invalidWomanTop.includes(this.clothing[0])) {
                this.sendNotificationWithoutButton('Ungültige Oberbekleidung, bitte wähle eine andere!', 'error', 'top-left', 2500)
                return;
              }
              if (invalidWomanTorso && invalidWomanTorso.includes(this.clothing[1])) {
                this.sendNotificationWithoutButton('Ungültiger Torso, bitte wähle eine anderen!', 'error', 'top-left', 2500)
                return;
              }
              if (invalidWomanLeg && invalidWomanLeg.includes(this.clothing[2])) {
                this.sendNotificationWithoutButton('Ungültige Hose, bitte wähle andere!', 'error', 'top-left', 2500)
                return;
              }
              if (invalidWomanShoe && invalidWomanShoe.includes(this.clothing[3])) {
                this.sendNotificationWithoutButton('Ungültige Schuhe, bitte wähle eine andere!', 'error', 'top-left', 2500)
                return;
              }
              if (invalidWomanTShirt && invalidWomanTShirt.includes(this.clothing[4])) {
                this.sendNotificationWithoutButton('Ungültiges T-Shirt, bitte wähle ein anderes!', 'error', 'top-left', 2500)
                return;
              }
              if (invalidWomanBags && invalidWomanBags.includes(this.clothing[5]) && event != 'dutyCloths') {
                this.sendNotificationWithoutButton('Ungültiger Rucksack, bitte wähle einen anderen!', 'error', 'top-left', 2500)
                return;
              }
              if (invalidWomanGlasses && invalidWomanGlasses.includes(this.clothing[6])) {
                this.sendNotificationWithoutButton('Ungültige Brille, bitte wähle einen andere!', 'error', 'top-left', 2500)
                return;
              }
              if (invalidWomanHats && invalidWomanHats.includes(this.clothing[7])) {
                this.sendNotificationWithoutButton('Ungültige Kopfbedeckung, bitte wähle einen andere!', 'error', 'top-left', 2500)
                return;
              }
            }
            if(this.clothshow2 == true)
            {
              if (invalidWomanOhrring && invalidWomanOhrring.includes(this.clothing[9])) {
                this.sendNotificationWithoutButton('Ungültiger Ohrring, bitte wähle einen anderen!', 'error', 'top-left', 2500)
                return;
              }
              if (invalidWomanArmbanduhr && invalidWomanArmbanduhr.includes(this.clothing[10])) {
                this.sendNotificationWithoutButton('Ungültige Armbanduhr, bitte wähle eine andere!', 'error', 'top-left', 2500)
                return;
              }
              if (invalidWomanArmring && invalidWomanArmring.includes(this.clothing[11])) {
                this.sendNotificationWithoutButton('Ungültiger Armring, bitte wähle einen anderen!', 'error', 'top-left', 2500)
                return;
              }
              if (invalidAccessoiresWoman && invalidAccessoiresWoman.includes(this.clothing[12])) {
                this.sendNotificationWithoutButton('Ungültiges Accessoire, bitte wähle ein anderes!', 'error', 'top-left', 2500)
                return;
              }
            }
            if(this.clothshow3 == true)
            {
              if (invalidMask && invalidMask.includes(this.clothing[8])) {
                this.sendNotificationWithoutButton('Ungültige Maske, bitte wähle einen anderen!', 'error', 'top-left', 2500)
                return;
              }
            }
            if(this.clothshow4 == true)
            {
            if (this.clothing[3] != 25) {
                this.sendNotificationWithoutButton('Ungültige Arbeitskleidung, bitte wähle eine andere!', 'error', 'top-left', 2500)
                return;
              }
            }
          }
          if(this.clothshow4 == false)
          {
            // eslint-disable-next-line no-undef
            mp.trigger('Client:BuyCloths', JSON.stringify(this.clothing), JSON.stringify(this.clothingColor), this.clothcost, false);
          }
          else
          {
            // eslint-disable-next-line no-undef
            mp.trigger('Client:BuyCloths', JSON.stringify(this.clothing), JSON.stringify(this.clothingColor), this.clothcost, true);
          }
        }
        this.clicked = (Date.now() / 1000) + (1);
      }
    },
    resetCloth: function () {
      if ((Date.now() / 1000) > this.clicked) {
        var clothingcheck, clothingColor;
        if (this.selectedcloth == 'Schuh') {
          this.clothing[3] = this.clothingBackup[3];
          clothingcheck = this.clothing[3];
          this.clothingColor[3] = this.clothingColorBackup[3];
          clothingColor = this.clothingColor[3];
        } else if (this.selectedcloth == 'Hosen') {
          this.clothing[2] = this.clothingBackup[2];
          clothingcheck = this.clothing[2];
          this.clothingColor[2] = this.clothingColorBackup[2];
          clothingColor = this.clothingColor[2];
        } else if (this.selectedcloth == 'Torso') {
          this.clothing[1] = this.clothingBackup[1];
          clothingcheck = this.clothing[1];
          this.clothingColor[1] = this.clothingColorBackup[1];
          clothingColor = this.clothingColor[1];
        } else if (this.selectedcloth == 'Oberbekleidung') {
          this.clothing[0] = this.clothingBackup[0];
          clothingcheck = this.clothing[0];
          this.clothingColor[0] = this.clothingColorBackup[0];
          clothingColor = this.clothingColor[0];
        } else if (this.selectedcloth == 'T-Shirt') {
          this.clothing[4] = this.clothingBackup[4];
          clothingcheck = this.clothing[4];
          this.clothingColor[4] = this.clothingColorBackup[4];
          clothingColor = this.clothingColor[4];
        } else if (this.selectedcloth == 'Rucksack') {
          this.clothing[5] = this.clothingBackup[5];
          clothingcheck = this.clothing[5];
          this.clothingColor[5] = this.clothingColorBackup[5];
          clothingColor = this.clothingColor[5];
        } else if (this.selectedcloth == 'Brillen') {
          this.clothing[6] = this.clothingBackup[6];
          clothingcheck = this.clothing[6];
          this.clothingColor[6] = this.clothingColorBackup[6];
          clothingColor = this.clothingColor[6];
        } else if (this.selectedcloth == 'Kopfbedeckung') {
          this.clothing[7] = this.clothingBackup[7];
          clothingcheck = this.clothing[7];
          this.clothingColor[7] = this.clothingColorBackup[7];
          clothingColor = this.clothingColor[7];
        } else if (this.selectedcloth == 'Ohrring') {
          this.clothing[9] = this.clothingBackup[9];
          clothingcheck = this.clothing[9];
          this.clothingColor[9] = this.clothingColorBackup[9];
          clothingColor = this.clothingColor[9];
        } else if (this.selectedcloth == 'Armbanduhr') {
          this.clothing[10] = this.clothingBackup[10];
          clothingcheck = this.clothing[10];
          this.clothingColor[10] = this.clothingColorBackup[10];
          clothingColor = this.clothingColor[10];
        } else if (this.selectedcloth == 'Armring') {
          this.clothing[11] = this.clothingBackup[11];
          clothingcheck = this.clothing[11];
          this.clothingColor[11] = this.clothingColorBackup[11];
          clothingColor = this.clothingColor[11];
        } else if (this.selectedcloth == 'Accessoires') {
          this.clothing[12] = this.clothingBackup[12];
          clothingcheck = this.clothing[12];
          this.clothingColor[12] = this.clothingColorBackup[12];
          clothingColor = this.clothingColor[12];
        }
        this.selectedclothid = clothingcheck;
        this.selectedclothidold = clothingcheck;
        this.selectedclothcolor = clothingColor;
        // eslint-disable-next-line no-undef
        mp.trigger('Client:GetMaxClothColor', this.selectedcloth, clothingcheck, this.gender);
        // eslint-disable-next-line no-undef
        mp.trigger('Client:ChangeShopClothes', this.selectedcloth, clothingcheck, clothingColor);
        this.clicked = (Date.now() / 1000) + (1);
      }
    },
    clothingCustomize: function (id) {
      let selected = 0;
      if (id == 1) {
        if (this.selectedclothidold != this.selectedclothid) {
          this.selectedclothcolor = 0;
          this.selectedclothidold = this.selectedclothid;
        }
      }
      if (this.selectedcloth == 'Schuh') {
        selected = this.selectedclothid;
        if(this.clothshow4 == true)
        {       
          this.selectedclothid = this.getfactioncloth();
        }
        this.clothing[3] = selected;
        this.clothingColor[3] = this.selectedclothcolor;
      } else if (this.selectedcloth == 'Hosen') {
        selected = this.selectedclothid;
        if(this.clothshow4 == true)
        {
          this.selectedclothid = this.getfactioncloth();
        }
        this.clothing[2] = selected;
        this.clothingColor[2] = this.selectedclothcolor;
      } else if (this.selectedcloth == 'Torso') {
        selected = this.selectedclothid;
        if(this.clothshow4 == true)
        {
          this.selectedclothid = this.getfactioncloth();
        }
        this.clothing[1] = selected;
        this.clothingColor[1] = this.selectedclothcolor;
      } else if (this.selectedcloth == 'Oberbekleidung') {
        selected = this.selectedclothid;
        if(this.clothshow4 == true)
        {
          this.selectedclothid = this.getfactioncloth();
        }
        this.clothing[0] = selected;
        this.clothingColor[0] = this.selectedclothcolor;
      } else if (this.selectedcloth == 'T-Shirt') {
        selected = this.selectedclothid;
        if(this.clothshow4 == true)
        {
          this.selectedclothid = this.getfactioncloth();
        }
        this.clothing[4] = selected;
        this.clothingColor[4] = this.selectedclothcolor;
      } else if (this.selectedcloth == 'Brillen') {
        selected = this.selectedclothid;
        this.clothing[6] = selected;
        this.clothingColor[6] = this.selectedclothcolor;
      } else if (this.selectedcloth == 'Rucksack') {
        selected = this.selectedclothid;
        this.clothing[5] = this.selectedclothid;
        this.clothingColor[5] = this.selectedclothcolor;
      } else if (this.selectedcloth == 'Kopfbedeckung') {
        selected = this.selectedclothid;
        if(this.clothshow4 == true)
        {
          this.selectedclothid = this.getfactioncloth();
        }
        this.clothing[7] = selected;
        this.clothingColor[7] = this.selectedclothcolor;
      } else if (this.selectedcloth == 'Maske') {
        selected = this.selectedclothid;
        this.clothing[8] = selected;
        this.clothingColor[8] = this.selectedclothcolor;
      } else if (this.selectedcloth == 'Ohrring') {
        selected = this.selectedclothid;
        this.clothing[9] = this.selectedclothid;
        this.clothingColor[9] = this.selectedclothcolor;
      } else if (this.selectedcloth == 'Armbanduhr') {
        selected = this.selectedclothid;
        this.clothing[10] = this.selectedclothid;
        this.clothingColor[10] = this.selectedclothcolor;
      } else if (this.selectedcloth == 'Armring') {
        selected = this.selectedclothid;
        this.clothing[11] = this.selectedclothid;
        this.clothingColor[11] = this.selectedclothcolor;
      } else if (this.selectedcloth == 'Accessoires') {
        selected = this.selectedclothid;
        if(this.clothshow4 == true)
        {
          this.selectedclothid = this.getfactioncloth();
        }
        this.clothing[12] = selected;
        this.clothingColor[12] = this.selectedclothcolor;
      }
      if(this.clothshow == true)
      {
        this.countClothCost();
      }
      else if(this.clothshow2 == true)
      {
        this.countClothCost2();
      }
      else if(this.clothshow3 == true)
      {
        this.countClothCost3();
      }
      // eslint-disable-next-line no-undef
      mp.trigger('Client:GetMaxClothColor', this.selectedcloth, selected, this.gender);
      // eslint-disable-next-line no-undef
      mp.trigger('Client:ChangeShopClothes', this.selectedcloth, selected, this.selectedclothcolor);
    },
    countClothCost() {
      var cost = 0;
      for (let i = 0; i < this.clothing.length; i++) {
        if (this.clothing[i] != this.clothingBackup[i] || this.clothingColor[i] != this.clothingColorBackup[i]) {
          cost += (35 * this.multiplier);
        }
      }
      this.clothcost = parseInt(cost);
    },
    countClothCost2() {
      var cost = 0;
      for (let i = 0; i < this.clothing.length; i++) {
        if (this.clothing[i] != this.clothingBackup[i] || this.clothingColor[i] != this.clothingColorBackup[i]) {
          cost += (525 * this.multiplier);
        }
      }
      this.clothcost = parseInt(cost);
    },
    countClothCost3() {
      var cost = 0;
      for (let i = 0; i < this.clothing.length; i++) {
        if (this.clothing[i] != this.clothingBackup[i] || this.clothingColor[i] != this.clothingColorBackup[i]) {
          cost += 1500;
        }
      }
      this.clothcost = parseInt(cost);
    },
    abortCloth: function () {
      if ((Date.now() / 1000) > this.clicked) {
        // eslint-disable-next-line no-undef
        mp.trigger('Client:AbortCloth');
        this.clicked = (Date.now() / 1000) + (1);
      }
    },
    endCloth: function (status) {
      if ((Date.now() / 1000) > this.clicked) {
        // eslint-disable-next-line no-undef
        mp.trigger('Client:AbortCloth2', status);
        this.clicked = (Date.now() / 1000) + (1);
      }
    },
    selectCloth: function (cloth) {
      this.selectedcloth = cloth;
      if(this.clothshow4 == false)
      {
        if (this.selectedcloth == 'Schuh') {
          if (this.gender == 1) {
            this.maxcloth = maxShoesMen;
          } else {
            this.maxcloth = maxShoesWomen;
          }
          this.maxclothColor = 0;
          this.selectedclothid = this.clothing[3];
          this.selectedclothcolor = this.clothingColor[3];
        } else if (this.selectedcloth == 'Hosen') {
          if (this.gender == 1) {
            this.maxcloth = maxLegsMen-1;
          } else {
            this.maxcloth = maxLegsWomen-1;
          }
          this.maxclothColor = 0;
          this.selectedclothid = this.clothing[2];
          this.selectedclothcolor = this.clothingColor[2];
        } else if (this.selectedcloth == 'Torso') {
          if (this.gender == 1) {
            this.maxcloth = maxTorsoMen-1;
          } else {
            this.maxcloth = maxTorsoWomen-1;
          }
          this.maxclothColor = 0;
          this.selectedclothid = this.clothing[1];
          this.selectedclothcolor = this.clothingColor[1];
        } else if (this.selectedcloth == 'Oberbekleidung') {
          if (this.gender == 1) {
            this.maxcloth = maxOberbekleidungMen-1;
          } else {
            this.maxcloth = maxOberbekleidungWomen-1;
          }
          this.maxclothColor = 0;
          this.selectedclothid = this.clothing[0];
          this.selectedclothcolor = this.clothingColor[0];
        } else if (this.selectedcloth == 'T-Shirt') {
          if (this.gender == 1) {
            this.maxcloth = maxTShirtMen;
          } else {
            this.maxcloth = maxTShirtWomen;
          }
          this.maxclothColor = 0;
          this.selectedclothid = this.clothing[4];
          this.selectedclothcolor = this.clothingColor[4];
        } else if (this.selectedcloth == 'Rucksack') {
          if (this.gender == 1) {
            this.maxcloth = maxBagsMen;
          } else {
            this.maxcloth = maxBagsWomen;
          }
          this.maxclothColor = 0;
          this.selectedclothid = this.clothing[5];
          this.selectedclothcolor = this.clothingColor[5];
        } else if (this.selectedcloth == 'Brillen') {
          if (this.gender == 1) {
            this.maxcloth = maxGlassesMen;
          } else {
            this.maxcloth = maxGlassesWomen;
          }
          this.maxclothColor = 0;
          this.selectedclothid = this.clothing[6];
          this.selectedclothcolor = this.clothingColor[6];
        } else if (this.selectedcloth == 'Kopfbedeckung') {
          if (this.gender == 1) {
            this.maxcloth = maxKopfbedeckungMen;
          } else {
            this.maxcloth = maxKopfbedeckungWomen;
          }
          this.maxclothColor = 0;
          this.selectedclothid = this.clothing[7];
          this.selectedclothcolor = this.clothingColor[7];
        } else if (this.selectedcloth == 'Maske') {
          if (this.gender == 1) {
            this.maxcloth = maxMaskMen;
          } else {
            this.maxcloth = maxMaskWoman;
          }
          this.maxclothColor = 0;
          this.selectedclothid = this.clothing[8];
          this.selectedclothcolor = this.clothingColor[8];
        }
        else if (this.selectedcloth == 'Ohrring') {
          if (this.gender == 1) {
            this.maxcloth = maxOhrringMen;
          } else {
            this.maxcloth = maxOhrringWomen;
          }
          this.maxclothColor = 0;
          this.selectedclothid = this.clothing[9];
          this.selectedclothcolor = this.clothingColor[9];
        }
        else if (this.selectedcloth == 'Armbanduhr') {
          if (this.gender == 1) {
            this.maxcloth = maxArmbanduhrMen;
          } else {
            this.maxcloth = maxArmbanduhrWomen;
          }
          this.maxclothColor = 0;
          this.selectedclothid = this.clothing[10];
          this.selectedclothcolor = this.clothingColor[10];
        }
        else if (this.selectedcloth == 'Armring') {
          if (this.gender == 1) {
            this.maxcloth = maxArmringMen;
          } else {
            this.maxcloth = maxArmringWoman;
          }
          this.maxclothColor = 0;
          this.selectedclothid = this.clothing[11];
          this.selectedclothcolor = this.clothingColor[11];
        }
        else if (this.selectedcloth == 'Accessoires') {
          if (this.gender == 1) {
            this.maxcloth = maxAccessoiresMen;
          } else {
            this.maxcloth = maxAccessoiresWoman;
          }
          this.maxclothColor = 0;
          this.selectedclothid = this.clothing[12];
          this.selectedclothcolor = this.clothingColor[12];
        }
      }
      else
      {
        if (this.selectedcloth == 'Schuh') {
          this.maxclothColor = 0;
          this.selectedclothid = this.clothing[3];
          this.selectedclothid = this.getfactioncloth();
          this.selectedclothcolor = this.clothingColor[3];
        }
        else if (this.selectedcloth == 'Kopfbedeckung') {
          this.maxclothColor = 0;
          this.selectedclothid = this.clothing[7];
          this.selectedclothid = this.getfactioncloth();
          this.selectedclothcolor = this.clothingColor[7];
        }
        else if (this.selectedcloth == 'Oberbekleidung') 
        {
          this.maxclothColor = 0;
          this.selectedclothid = this.clothing[0];
          this.selectedclothid = this.getfactioncloth();
          this.selectedclothcolor = this.clothingColor[0];
        }
        else if (this.selectedcloth == 'T-Shirt') {
          this.maxclothColor = 0;
          this.selectedclothid = this.clothing[4];
          this.selectedclothid = this.getfactioncloth();
          this.selectedclothcolor = this.clothingColor[4];
        }
        else if (this.selectedcloth == 'Hosen') {
          this.maxclothColor = 0;
          this.selectedclothid = this.clothing[2];
          this.selectedclothid = this.getfactioncloth();
          this.selectedclothcolor = this.clothingColor[2];
        }
        else if (this.selectedcloth == 'Torso') {
          this.maxclothColor = 0;
          this.selectedclothid = this.clothing[1];
          this.selectedclothid = this.getfactioncloth();
          this.selectedclothcolor = this.clothingColor[1];
        }
        else if (this.selectedcloth == 'Accessoires') {
          this.maxclothColor = 0;
          this.selectedclothid = this.clothing[5];
          this.selectedclothid = this.getfactioncloth();
          this.selectedclothcolor = this.clothingColor[5];
        }
      }
      this.selectedclothidold = this.selectedclothid;
      // eslint-disable-next-line no-undef
      mp.trigger('Client:GetMaxClothColor', cloth, this.selectedclothid, this.gender);
    },
    setMaxClothColor(getcolor) {
      if (getcolor < 0) {
        getcolor = 0;
      }
      this.maxclothColor = getcolor;
    },
    showClothMenu: function (getarray1, getarray2, gender, multiplier) {
      if (getarray1 != 'null') {
        this.selectedclothid = 0;
        this.selectedclothidold = 0;
        this.selectedclothcolor = 0;
        this.selectedcloth = 'n/A';
        this.gender = gender;
        this.clothing = JSON.parse(getarray1);
        this.clothingColor = JSON.parse(getarray2);
        this.clothingBackup = JSON.parse(getarray1);
        this.clothingColorBackup = JSON.parse(getarray2);
        this.multiplier = multiplier;
        this.selectCloth('Kopfbedeckung');
      }
      this.clothshow = !this.clothshow;
    },
    showFactionClothMenu: function (getarray1, getarray2, gender, faction, json) {
      this.searchelement = '';
      this.clothshow4 = !this.clothshow4;
      this.outfitname = '';
      this.clothcost = 1;
      if (getarray1 != 'null') {
        this.faction = faction;
        this.selectedcloth = 'n/A';
        this.gender = gender;
        if(this.outfits != 'null' || this.faction == 1337)
        {
          this.outfits = JSON.parse(json);
        }
        else
        {
          this.outfits = [];
        }
        this.clothing = JSON.parse(getarray1);
        this.clothingColor = JSON.parse(getarray2);
        this.clothingBackup = JSON.parse(getarray1);
        this.clothingColorBackup = JSON.parse(getarray2);
        this.selectCloth('Kopfbedeckung');
      }
    },
    showJuweMenu: function (getarray1, getarray2, gender, multiplier) {
      if (getarray1 != 'null') {
        this.selectedclothid = 0;
        this.selectedclothidold = 0;
        this.selectedclothcolor = 0;
        this.selectedcloth = 'n/A';
        this.gender = gender;
        this.clothing = JSON.parse(getarray1);
        this.clothingColor = JSON.parse(getarray2);
        this.clothingBackup = JSON.parse(getarray1);
        this.clothingColorBackup = JSON.parse(getarray2);
        this.multiplier = multiplier;
        this.selectCloth('Ohrring');
      }
      this.clothshow2 = !this.clothshow2;
    },
    showMaskMenu: function (getarray1, getarray2, gender) {
      if (getarray1 != 'null') {
        this.selectedclothid = 0;
        this.selectedclothidold = 0;
        this.selectedclothcolor = 0;
        this.selectedcloth = 'n/A';
        this.gender = gender;
        this.clothing = JSON.parse(getarray1);
        this.clothingColor = JSON.parse(getarray2);
        this.clothingBackup = JSON.parse(getarray1);
        this.clothingColorBackup = JSON.parse(getarray2);
        this.multiplier = 1;
        this.selectCloth('Maske');
      }
      this.clothshow3 = !this.clothshow3;
    },
    showBarberMenu: function (oldhair, oldhaircolor, oldbeard, oldbeardcolor, oldhighlight, gender, multiplier, bizzid, overlay0, overlaycolor0, overlay1, overlaycolor1, overlay2, overlaycolor2) {
      this.selectedBarber = 'Haare';
      this.selectedclothid = oldhair;
      this.selectedclothcolor = oldhaircolor;
      this.selectedclothid2 = oldbeard;
      this.selectedclothcolor2 = oldbeardcolor;
      this.oldhair = oldhair;
      this.oldhaircolor = oldhaircolor;
      this.oldhair2 = oldbeard;
      this.oldhaircolor2 = oldbeardcolor;
      this.selectedclothid3 = oldhighlight;
      this.oldhair3 = oldhighlight;
      this.gender = gender;
      this.headOverlays[0] = parseInt(overlay0);
      this.headOverlaysColors[0] = parseInt(overlaycolor0);
      this.headOverlays[1] = parseInt(overlay1);
      this.headOverlaysColors[1] = parseInt(overlaycolor1);
      this.headOverlays[2] = parseInt(overlay2);
      this.headOverlaysColors[2] = parseInt(overlaycolor2);
      this.headOverlaysB[0] = this.headOverlays[0];
      this.headOverlaysColorsB[0] = parseInt(overlaycolor0);
      this.headOverlaysB[1] = this.headOverlays[1];
      this.headOverlaysColorsB[1] = parseInt(overlaycolor1);
      this.headOverlaysB[2] = this.headOverlays[2];
      this.headOverlaysColorsB[2] = parseInt(overlaycolor2);
      if (this.gender == 1) {
        this.maxhair = 82;
      } else {
        this.maxhair = 88;
      }
      this.multiplier = multiplier;
      this.barbershow = !this.barbershow;
      this.bizzid = bizzid;
    },
    showInfobox: function (text1, text2, text3, text4, header, header1, header2, header3, header4, modus) {
      this.showinfobox = !this.showinfobox;
      this.infoboxtextheader = header;
      this.infoboxtestmodus = modus;
      if (header && header != 'null') {
        this.infoboxtextheader = header;
      }
      if (header1 && header1 != 'null') {
        this.infoboxtextheader1 = header1;
      }
      if (header2 && header2 != 'null') {
        this.infoboxtextheader2 = header2;
      }
      if (header3 && header3 != 'null') {
        this.infoboxtextheader3 = header3;
      }
      if (header4 && header4 != 'null') {
        this.infoboxtextheader4 = header4;
      }
      if (text1 && text1 != 'null') {
        this.infoboxtext1 = text1;
      }
      if (text2 && text2 != 'null') {
        this.infoboxtext2 = text2;
      }
      if (text3 && text3 != 'null') {
        this.infoboxtext3 = text3;
      }
      if (text4 != 'null') {
        this.infoboxtext4 = text4;
      }
      this.$forceUpdate();
    },
    updateInfobox: function (text1, text2, text3, text4, header, header1, header2, header3, header4, modus) {
      if (header && header != 'null') {
        this.infoboxtextheader = header;
      }
      if (header1 && header1 != 'null') {
        this.infoboxtextheader1 = header1;
      }
      if (header2 && header2 != 'null') {
        this.infoboxtextheader2 = header2;
      }
      if (header3 && header3 != 'null') {
        this.infoboxtextheader3 = header3;
      }
      if (header4 != 'null') {
        this.infoboxtextheader4 = header4;
      }
      if (text1 && text1 != 'null') {
        this.infoboxtext1 = text1;
      }
      if (text2 && text2 != 'null') {
        this.infoboxtext2 = text2;
      }
      if (text3 && text3 != 'null') {
        this.infoboxtext3 = text3;
      }
      if (text4 && text4 != 'null') {
        this.infoboxtext4 = text4;
      }
      this.infoboxtestmodus = modus;
      this.$forceUpdate();
    },
    showHud: function (health, thirst, hunger, shield) {
      this.showhud = !this.showhud;
      this.health = health;
      this.thirst = thirst;
      this.hunger = hunger;
      this.shield = shield;
      var self = this;
      setTimeout(function () {
        self.updateBar(1, health);
        self.updateBar(2, shield);
        self.updateBar(3, hunger);
        self.updateBar(4, thirst);
      }, 150);
    },
    reloadHud: function () {
      this.showhud = false;
      var self = this;
      setTimeout(function () {
        self.showhud = true;
        self.$forceUpdate();
      }, 555);
    },
    hideHud: function () {
      this.showhud = false;
    },
    updateBar: function (bar, wert) {
      var self = this;
      if (bar == 2 && wert >= 99) {
        wert = 100;
      }
      if (wert == -1) return;
      if (wert > 100) wert = 100;
      if (wert < 0) wert = 0;
      if (wert > 0) {
        wert = wert / 100;
      }
      setTimeout(function () {
        if (self.$refs.healthbar) {
          if (bar == 1) self.$refs.healthbar.animate(wert);
          if (bar == 2) self.$refs.shieldbar.animate(wert);
          if (bar == 3) self.$refs.hungerbar.animate(wert);
          if (bar == 4) self.$refs.thirstbar.animate(wert);
        }
      }, 95);
    },
    updateMoney: function (money) {
      if (money == -1) return;
      this.money = money;
    },
    checkEinreise: function () {
      if (this.eyecolor.length < 4 || this.eyecolor.length > 10) {
        Swal.fire({
          icon: 'error',
          title: 'Fehler',
          text: 'Ungültige Augenfarbe!',
          showConfirmButton: false,
          timer: 2500
        })
        return;
      }
      if (this.education.length < 10 || this.education.length > 64) {
        Swal.fire({
          icon: 'error',
          title: 'Fehler',
          text: 'Ungültiger Abschluss!',
          showConfirmButton: false,
          timer: 2500
        })
        return;
      }
      if (this.skills.length < 5 || this.skills.length > 64) {
        Swal.fire({
          icon: 'error',
          title: 'Fehler',
          text: 'Ungültige besondere Fähigkeiten!',
          showConfirmButton: false,
          timer: 2500
        })
        return;
      }
      if (this.appearance.length < 10 || this.appearance.length > 64) {
        Swal.fire({
          icon: 'error',
          title: 'Fehler',
          text: 'Ungültiges Aussehen!',
          showConfirmButton: false,
          timer: 2500
        })
        return;
      }
      if (this.size.length != 3 && !this.size.includes("cm") && !this.size.startsWith('1')) {
        Swal.fire({
          icon: 'error',
          title: 'Fehler',
          text: 'Ungültiges Größe, bitte Format überprüfen - (xxxcm)!',
          showConfirmButton: false,
          timer: 2500
        })
        return;
      }
      // eslint-disable-next-line no-undef
      mp.trigger('Client:PlaySoundSuccess');
      Swal.fire({
        title: 'Geschafft!',
        text: "Sie sind absofort ein offizieller Staatsbürger von Los Santos, hier ist Ihr neuer Personalausweis!",
        icon: 'success',
        showCancelButton: false,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Abschließen'
      }).then(() => {
        // eslint-disable-next-line no-undef
        mp.trigger('Client:FinishEinreise', this.eyecolor, this.education, this.skills, this.appearance, this.size);
        this.tutorialStadthalle2 = false;
        this.tutorialStadthalle = false;
        let timerInterval = 15000;
        Swal.fire({
          title: 'Willkommen!',
          html: 'Wir wünschen dir viel Spass auf Nemesus World! Solltest du Hilfe benötigen, drücke die F2 Taste, dort kannst du dann z.B dir die Ersten Schritte anschauen oder ein Support Ticket erstellen. Weitere Informationen findest du auf unserer Webseite unter https://nemesus-world.de oder bei uns im UCP auf https://ucp.nemesus-world.de! PS: Dieser Gamemode wurde von Nemesus.de entwickelt, alle zukünftigen Updates findest du im Nemesus World Server Github Repo!',
          timer: 12000,
          allowEscapeKey: false,
          timerProgressBar: true,
          didOpen: () => {
            Swal.showLoading()
            const b = Swal.getHtmlContainer().querySelector('b')
            timerInterval = setInterval(() => {
              b.textContent = Swal.getTimerLeft()
            }, 100)
          },
          willClose: () => {
            clearInterval(timerInterval)
          }
        });
      });
    },
    checkEinreise2: function () {
      if (this.eyecolor.length < 4 || this.eyecolor.length > 10) {
        Swal.fire({
          icon: 'error',
          title: 'Fehler',
          text: 'Ungültige Augenfarbe!',
          showConfirmButton: false,
          timer: 2500
        })
        return;
      }
      if (this.education.length < 10 || this.education.length > 64) {
        Swal.fire({
          icon: 'error',
          title: 'Fehler',
          text: 'Ungültiger Abschluss!',
          showConfirmButton: false,
          timer: 2500
        })
        return;
      }
      if (this.skills.length < 10 || this.skills.length > 64) {
        Swal.fire({
          icon: 'error',
          title: 'Fehler',
          text: 'Ungültige besondere Fähigkeiten!',
          showConfirmButton: false,
          timer: 2500
        })
        return;
      }
      if (this.appearance.length < 10 || this.appearance.length > 64) {
        Swal.fire({
          icon: 'error',
          title: 'Fehler',
          text: 'Ungültiges Aussehen!',
          showConfirmButton: false,
          timer: 2500
        })
        return;
      }
      if (this.size.length < 3 || this.size.length > 3 || !this.size.startsWith("1")) {
        Swal.fire({
          icon: 'error',
          title: 'Fehler',
          text: 'Ungültiges Größe, bitte Format überprüfen!',
          showConfirmButton: false,
          timer: 2500
        })
        return;
      }
      // eslint-disable-next-line no-undef
      mp.trigger('Client:PlaySoundSuccess');
      Swal.fire({
        title: 'Geschafft!',
        text: "So hier deine Papiere, du bist jetzt ein 'offizieller' Staatsbürger von Los Santos ;)",
        icon: 'success',
        showCancelButton: false,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Abschließen'
      }).then(() => {
        this.tutorialStadthalle = false;
        this.tutorialStadthalle2 = false;
        // eslint-disable-next-line no-undef
        mp.trigger('Client:FinishEinreise', this.eyecolor, this.education, this.skills, this.appearance, this.size);
        let timerInterval = 15000;
        Swal.fire({
          title: 'Willkommen!',
          html: 'Wir wünschen dir viel Spass auf Nemesus World, solltest du Hilfe benötigen drücke die F2 Taste, dort kannst du dann das Hilfemenü öffnen oder ein Support Ticket erstellen. Weitere Informationen findest du auf unserer Webseite unter https://nemesus-world.de oder bei uns im UCP auf https://ucp.nemesus-world.de!',
          timer: 12000,
          timerProgressBar: true,
          allowEscapeKey: false,
          didOpen: () => {
            Swal.showLoading()
            const b = Swal.getHtmlContainer().querySelector('b')
            timerInterval = setInterval(() => {
              b.textContent = Swal.getTimerLeft()
            }, 100)
          },
          willClose: () => {
            clearInterval(timerInterval)
          }
        });
      });
    },
    showBlackFadeIn: function (blacktext) {
      this.showBlack = true;
      this.blackText = blacktext;
    },
    showTutorialStadthalle: function (legal, name) {
      if (this.startSound != null) {
        this.startSound.pause();
        this.startSound.currentTime = 0;
        this.startSound = null;
      }
      this.legal = legal;
      this.name = name;
      this.showBlack = false;
      if (legal == 1) {
        this.tutorialStadthalle = true;
        this.tutorialStadthalle2 = false;
      } else {
        this.tutorialStadthalle2 = true;
        this.tutorialStadthalle = false;
      }
      return;
    },
    stopAllNotifications: function () {
      this.$toast.clear();
    },
    sendNotificationWithTimer: function (title, text, timer) {
      let timerInterval;
      Swal.fire({
        title: title,
        html: text,
        timer: timer,
        allowEscapeKey: false,
        timerProgressBar: true,
        didOpen: () => {
          Swal.showLoading()
          const b = Swal.getHtmlContainer().querySelector('b')
          timerInterval = setInterval(() => {
            b.textContent = Swal.getTimerLeft()
          }, 100)
        },
        willClose: () => {
          clearInterval(timerInterval)
        }
      });
    },
    sendNotificationWithoutButton: function (title, status, position, timer) {
      if (position == 'top-end') {
        position = 'top-left';
      }
      if (status == 'success') {
        this.$toast.success(title, {
          containerClassName: "my-container-class",
          position: position,
          timeout: timer,
          closeOnClick: true,
          pauseOnFocusLoss: true,
          pauseOnHover: true,
          draggable: true,
          draggablePercent: 0.6,
          showCloseButtonOnHover: false,
          hideProgressBar: false,
          closeButton: false,
          icon: true,
          rtl: false
        });
      }
      if (status == 'error') {
        this.$toast.error(title, {
          position: position,
          timeout: timer,
          closeOnClick: true,
          pauseOnFocusLoss: true,
          pauseOnHover: true,
          draggable: true,
          draggablePercent: 0.6,
          showCloseButtonOnHover: false,
          hideProgressBar: false,
          closeButton: false,
          icon: true,
          rtl: false
        });
      }
      if (status == 'info') {
        this.$toast(title, {
          position: position,
          timeout: timer,
          closeOnClick: true,
          pauseOnFocusLoss: true,
          pauseOnHover: true,
          draggable: true,
          draggablePercent: 0.6,
          showCloseButtonOnHover: false,
          hideProgressBar: false,
          closeButton: false,
          icon: true,
          rtl: false
        });
      }
      return;
    },
    sendNotificationWithoutButton2: function (title, status, position, timer) {
      Swal.fire({
        icon: status,
        title: title,
        position: position,
        showConfirmButton: false,
        timer: timer
      })
    },
    playSound: function (url, lower) {
      if (this.startSound != null) {
        this.stopSound();
      }
      let soundata;
      if(url != 'technobase')
      {
        soundata = {
          soundurl: url
        }
      }
      else
      {
        soundata = {
          soundurl: 'http://listener1.mp3.tb-group.fm/tb.mp3'
        }
      }
      this.startSound = new Audio(soundata.soundurl);
      if (lower == 1) {
        this.startSound.volume = 0.025;
      } else {
        this.startSound.volume = 0.085;
      }
      this.startSound.play();
    },
    stopSound: function () {
      if (this.startSound != null) {
        this.startSound.pause();
        this.startSound.currentTime = 0;
        this.startSound = null;
      }
    },
    showInputMenu: async (title, text, type, placeholder, maxlength, flag) => {
      Swal.close();
      var confirmed = true;
      const {
        value: input
      } = await Swal.fire({
        title: title,
        text: text,
        input: type,
        allowOutsideClick: false,
        allowEnterKey: true,
        allowEscapeKey: true,
        showCancelButton: true,
        cancelButtonText: 'Abbrechen',
        inputPlaceholder: placeholder,
        inputAttributes: {
          maxlength: maxlength,
          autocapitalize: 'off',
          autocorrect: 'off'
        }
      })
      if (!input || input.length <= 0) {
        confirmed = false;
      }
      // eslint-disable-next-line no-undef
      mp.trigger("Client:OnInput", input, flag, confirmed);
    },
    showInputMenu2: async (title, text, flag, button1, button2) => {
      Swal.close();
      Swal.fire({
        title: title,
        text: text,
        showDenyButton: true,
        showCancelButton: false,
        confirmButtonText: button1,
        allowOutsideClick: false,
        allowEnterKey: true,
        allowEscapeKey: true,
        denyButtonText: button2,
      }).then((result) => {
        if (result.isConfirmed) {
          // eslint-disable-next-line no-undef
          mp.trigger("Client:OnInput2", 1, flag);
        } else {
          // eslint-disable-next-line no-undef
          mp.trigger("Client:OnInput2", 2, flag);
        }
      })
    },
  }
}
</script>

<style scoped>

@font-face {
  font-family: "docallisme";
  src: local("docallisme"),   url(../assets/fonts/docallisme/docallisme.ttf) format("truetype");
}

@font-face {
  font-family: "adrip1";
  src: local("adrip1"),   url(../assets/fonts/adrip1/adrip1.ttf) format("truetype");
}

@font-face {
  font-family: "fatboysmiles";
  src: local("fatboysmiles"),   url(../assets/fonts/fatboysmiles/fatboysmiles.ttf) format("truetype");
}

h8 {
  font-family: "adrip1", Arial;
  font-size: 4vw;
  position: fixed;
  top: 100px;
  left: 100px;
}

html, body, template, * {
  -webkit-user-select: none;
  -khtml-user-select: none;
  -moz-user-select: none;
  -o-user-select: none;
  user-select: none;
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

.swal-overlay {
  z-index: 900000 !important;
}

:host ::ng-deep .swal2-container {
  z-index: 900000 !important;
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

#ui {
  position: relative;
  width: 100%;
  height: 100%;
  overflow: hidden;
  z-index: -1;
}

.info.player #status {
  clear: both;
}

.money {
  font-size: 3.51vh;
  font-weight: 450;
  font-family: 'Exo', sans-serif;
  z-index: -1;
  text-shadow: 1px 0 0 #000, 0 -1px 0 #000, 0 1px 0 #000, -1px 0 0 #000;
}

.progressbar.text {
  font-size: 0.9vh;
  margin-top: 0.3vh;
}

.centering3 {
  margin: 0;
  position: absolute;
  top: 95%;
  right: 2.5%;
  margin-left: -50%;
  transform: translate(-2.5%, -95%)
}

.centering33 {
  margin: 0;
  position: absolute;
  top: 62%;
  right: 2.5%;
  margin-left: -50%;
  transform: translate(-2.5%, -95%)
}

.centering34 {
  margin: 0;
  position: absolute;
  top: 72%;
  right: 2.5%;
  margin-left: -50%;
  transform: translate(-2.5%, -95%)
}

.centering4 {
  margin: 0;
  position: absolute;
  top: 35%;
  right: 2.5%;
  margin-left: -50%;
  transform: translate(-2.5%, -35%)
}

.centering5 {
  margin: 0;
  position: absolute;
  top: 40%;
  right: 1.25%;
  margin-left: -49%;
  transform: translate(-2.5%, -35%)
}

.centering6 {
  margin: 0;
  position: absolute;
  top: 60%;
  right: 1.25%;
  margin-left: -49%;
  transform: translate(-2.5%, -35%)
}

.infoboxtext {
  font-size: 1.0vw;
}

.clothiconactive {
  max-width: 42px;
  height: auto;
  border: 3px solid #fff;
  border-radius: 1vw;
  padding: 4px;
  text-shadow: 0 0 2px #000;
}

.clothicon {
  max-width: 42px;
  border: 3px solid #3F6791;
  border-radius: 1vw;
  padding: 4px;
  text-shadow: 0 0 2px #000;
}

.clothicon:hover {
  border: 3px solid #fff;
  text-shadow: 0 0 2px #000;
}

.weaponicon {
  max-width: 8vw;
  border: 3px solid #3F6791;
  border-radius: 1vw;
  padding: 4px;
  text-shadow: 0 0 2px #000;
}

.weaponicon:hover {
  border: 3px solid green;
  text-shadow: 0 0 2px #000;
}

.weaponiconactive {
  max-width: 8vw;
  border: 3px solid green;
  border-radius: 1vw;
  padding: 4px;
  text-shadow: 0 0 2px #000;
}

.cutimage {
  width: 3vw !important;
  height: 3vw !important;
}

#statusHud {
  position: absolute;
}

.bar {
  width: calc(12.5% - 4px - 1%);
  min-height: 25.5px;
  float: left;
  margin-right: 0.6%;
  margin-left: 0.6%;
}

.iconred:hover {
  color: rgb(219, 73, 73)
}

.iconblue:hover {
  color: #3F6791
}

.container {
  position: relative;
  text-align: center;
  color: white;
}

/* Bottom left text */
.bottom-left {
  position: absolute;
  bottom: 8px;
  left: 16px;
}

/* Top left text */
.persohead {
  position: absolute;
  top: 70px;
  left: 30px;
}

.personame {
  position: absolute;
  top: 110px;
  left: 30px;
}

.personame2 {
  position: absolute;
  top: 125px;
  left: 30px;
}

.personame3 {
  position: absolute;
  top: 150px;
  left: 30px;
}
.personame4 {
  position: absolute;
  top: 165px;
  left: 30px;
}

.personame5 {
  position: absolute;
  top: 190px;
  left: 30px;
}

.personame6 {
  position: absolute;
  top: 205px;
  left: 30px;
}


.personame7 {
  position: absolute;
  top: 230px;
  left: 30px;
}

.personame8 {
  position: absolute;
  top: 243px;
  left: 30px;
}


.personame9 {
  position: absolute;
  top: 110px;
  right: 95px;
}

.personame99 {
  position: absolute;
  top: 92px;
  right: 115px;
}

.personame10 {
  position: absolute;
  top: 125px;
  right: 95px;
}

.personame11 {
  position: absolute;
  top: 150px;
  right: 95px;
}

.personame12 {
  position: absolute;
  top: 165px;
  right: 95px;
}

.personame13 {
  position: absolute;
  top: 190px;
  right: 95px;
}

.personame14 {
  position: absolute;
  top: 205px;
  right: 95px;
}

.personame15 {
  position: absolute;
  top: 277px;
  right: 95px;
}

/* Top right text */
.top-right {
  position: absolute;
  top: 8px;
  right: 16px;
}

/* Bottom right text */
.bottom-right {
  position: absolute;
  bottom: 8px;
  right: 16px;
}
</style>
