<template>
    <div class="menu" style="background-color:transparent">
        <div class="centering5" style="height: 100%;" v-if="showhack">
            <div class="row d-flex justify-content-center"
                style="margin-left: 15rem !important; margin-top: 7.0vh; max-height:85vh; overflow-x: hidden; scrollbar-width: none;">
                <div class="col-md-12">
                    <div class="col-md-12 mt-1 animate__animated animate__bounceInUp">
                        <div class="box box-default">
                            <div class="card card-primary card-outline">
                                <div class="card-body">
                                    <div>
                                        <h5 style="font-family: 'Exo', sans-serif;">Hackwerkzeug</h5>
                                        <strong style="margin-top: 1vw">Gerätecode wird gescannt...</strong>
                                        <div class="row text-center d-flex justify-content-center animate__animated animate__flash"
                                            style="margin-top: 1vw">
                                            <div class="numbericon mr-4" style="font-size: 1.5vw">{{terminalarray[0]}}
                                            </div>
                                            <div class="numbericon mr-4" style="font-size: 1.5vw">{{terminalarray[1]}}
                                            </div>
                                            <div class="numbericon mr-4" style="font-size: 1.5vw">{{terminalarray[2]}}
                                            </div>
                                            <div class="numbericon mr-4" style="font-size: 1.5vw">{{terminalarray[3]}}
                                            </div>
                                            <div class="numbericon mr-4" style="font-size: 1.5vw">{{terminalarray[4]}}
                                            </div>
                                            <div class="numbericon mr-4" style="font-size: 1.5vw">{{terminalarray[5]}}
                                            </div>
                                        </div>
                                        <hr />
                                        <div class="mt-3"
                                            style="background-color: black; padding: 0.5vw; height: 45vw; max-height:32vw; width: 42vw;">
                                            <span style="color:#4AF626" v-html="terminaltext"></span></div>
                                    </div>
                                    <p class="float-right mt-2" style="font-family: 'Exo', sans-serif;">Hackwerkzeug
                                        v.5.3</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="centering5" style="height: 100%;" v-if="showgov">
            <div class="row d-flex justify-content-center"
                style="margin-left: 15rem !important; margin-top: 12.0vh; height:83vh; overflow: none; scrollbar-width: none;">
                <div class="col-md-12">
                    <div class="col-md-12 mt-1 animate__animated animate__bounceInUp">
                        <div class="box box-default">
                            <div class="card card-primary card-outline">
                                <div class="card-header" style="font-family: 'Exo', sans-serif;">Allgemeine Verwaltung
                                    <button @click="govsetting = 2" type="button" class="btn btn-primary float-right"
                                        style="border-radius: 1vw;margin-left: 0.15vw"
                                        :class="[(govsetting == 2) ? 'btn btn-primary btn-sm':'btn btn-secondary btn-sm']">Sonstiges</button>
                                    <button @click="govsetting = 1" type="button" class="btn btn-primary float-right"
                                        style="border-radius: 1vw;margin-left: 0.15vw"
                                        :class="[(govsetting == 1) ? 'btn btn-primary btn-sm':'btn btn-secondary btn-sm']">Budgets</button>
                                    <button @click="govsetting = 0" type="button" class="btn btn-primary float-right"
                                        style="border-radius: 1vw;margin-left: 0.15vw"
                                        :class="[(govsetting == 0) ? 'btn btn-primary btn-sm':'btn btn-secondary btn-sm']">Steuern</button>
                                </div>
                                <div class="card-body" style="scrollbar-width: none;" v-if="govsetting == 0">
                                    <div>
                                        Hier können verschiedene Einstellungen vorgenommen werden wie z.B Lohnsteuer,
                                        Budgets und
                                        vieles mehr ...
                                        <hr />
                                        <strong style="margin-top: 1vw">Lohnsteuer (%)</strong>
                                        <input type="text" class="form-control" placeholder="Steuer in Prozent"
                                            v-model="govvalues1[0]" maxlength="3" name="lohnsteuer" autocomplete="off">
                                        <br />
                                        <strong style="margin-top: 1vw">Gewerbesteuer (%)</strong>
                                        <input type="text" class="form-control" placeholder="Steuer in Prozent"
                                            v-model="govvalues1[1]" maxlength="3" name="gewerbesteuer"
                                            autocomplete="off">
                                        <br />
                                        <strong style="margin-top: 1vw">KFZ-Steuer (%)</strong>
                                        <input type="text" class="form-control" placeholder="Steuer in Prozent"
                                            v-model="govvalues1[2]" maxlength="4" name="kfzsteuer" autocomplete="off">
                                        <br />
                                        <strong style="margin-top: 1vw">Abschleppkosten pro Tag ($)</strong>
                                        <input type="text" class="form-control" placeholder="Angabe in Dollar"
                                            v-model="govvalues1[3]" maxlength="4" name="abschleppkosten"
                                            autocomplete="off">
                                        <br />
                                        <button class="btn btn-block btn-primary btn-sm mb-1" type="submit"
                                            v-on:click="saveGov(1)">Speichern</button>
                                    </div>
                                </div>
                                <div class="card-body" style="scrollbar-width: none;" v-if="govsetting == 1">
                                    <div>
                                        Hier können verschiedene Einstellungen vorgenommen werden wie z.B Lohnsteuer,
                                        Budgets und
                                        vieles mehr ...
                                        <hr />
                                        <strong style="margin-top: 1vw">Los Santos Police Department ($)</strong>
                                        <input type="text" class="form-control" placeholder="Budget in Dollar"
                                            v-model="govvalues2[0]" maxlength="6" name="budget1" autocomplete="off">
                                        <br />
                                        <strong style="margin-top: 1vw">Los Santos Rescue Center ($)</strong>
                                        <input type="text" class="form-control" placeholder="Budget in Dollar"
                                            v-model="govvalues2[1]" maxlength="6" name="budget2" autocomplete="off">
                                        <br />
                                        <strong style="margin-top: 1vw">Automobile Club Los Santos ($)</strong>
                                        <input type="text" class="form-control" placeholder="Budget in Dollar"
                                            v-model="govvalues2[2]" maxlength="6" name="budget3" autocomplete="off">
                                        <br />
                                        <strong style="margin-top: 1vw">Los Santos Goverment ($)</strong>
                                        <input type="text" class="form-control" placeholder="Budget in Dollar"
                                            v-model="govvalues2[3]" maxlength="6" name="budget4" autocomplete="off">
                                        <br />
                                        <strong style="margin-top: 1vw">Lifeinvader ($)</strong>
                                        <input type="text" class="form-control" placeholder="Budget in Dollar"
                                            v-model="govvalues2[4]" maxlength="6" name="budget5" autocomplete="off">
                                        <br />
                                        <button class="btn btn-block btn-primary btn-sm mb-1" type="submit"
                                            v-on:click="saveGov(2)">Speichern</button>
                                    </div>
                                </div>
                                <div class="card-body" style="scrollbar-width: none;" v-if="govsetting == 2">
                                    <div>
                                        Hier können verschiedene Einstellungen vorgenommen werden wie z.B Lohnsteuer,
                                        Budgets und
                                        vieles mehr ...
                                        <hr />
                                        <strong style="margin-top: 1vw">Arbeitslosengeld ($)</strong>
                                        <input type="text" class="form-control" placeholder="Budget in Dollar"
                                            v-model="govvalues3[0]" maxlength="5" name="sonstiges0" autocomplete="off">
                                        <br />
                                        <strong style="margin-top: 1vw">Neue Gruppierung eröffnen ($)</strong>
                                        <input type="text" class="form-control" placeholder="Budget in Dollar"
                                            v-model="govvalues3[1]" maxlength="6" name="sonstiges1" autocomplete="off">
                                        <br />
                                        <strong style="margin-top: 1vw">Gruppierung als Firma setzen ($)</strong>
                                        <input type="text" class="form-control" placeholder="Budget in Dollar"
                                            v-model="govvalues3[2]" maxlength="6" name="sonstiges2" autocomplete="off">
                                        <br />
                                        <strong style="margin-top: 1vw">Speditionslizenz ($)</strong>
                                        <input type="text" class="form-control" placeholder="Budget in Dollar"
                                            v-model="govvalues3[3]" maxlength="6" name="sonstiges4" autocomplete="off">
                                        <br />
                                        <strong style="margin-top: 1vw">Tuninglizenz ($)</strong>
                                        <input type="text" class="form-control" placeholder="Budget in Dollar"
                                            v-model="govvalues3[4]" maxlength="6" name="sonstiges5" autocomplete="off">
                                        <br />
                                        <strong style="margin-top: 1vw">Mechatronikerlizenz ($)</strong>
                                        <input type="text" class="form-control" placeholder="Budget in Dollar"
                                            v-model="govvalues3[5]" maxlength="6" name="sonstiges6" autocomplete="off">
                                        <br />
                                        <strong style="margin-top: 1vw">Personenbeförderungslizenz ($)</strong>
                                        <input type="text" class="form-control" placeholder="Budget in Dollar"
                                            v-model="govvalues3[6]" maxlength="6" name="sonstiges7" autocomplete="off">
                                        <br />
                                        <strong style="margin-top: 1vw">Sicherheitslizenz ($)</strong>
                                        <input type="text" class="form-control" placeholder="Budget in Dollar"
                                            v-model="govvalues3[7]" maxlength="6" name="sonstiges8" autocomplete="off">
                                        <br />
                                        <button class="btn btn-block btn-primary btn-sm mb-1" type="submit"
                                            v-on:click="saveGov(3)">Speichern</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div style="overflow: hidden" v-if="menushow">
            <aside class="main-sidebar sidebar-dark-primary elevation-4" style="background-color: #1E282C">
                <a href="#" class="brand-link" style="background-color: #1A2226">
                    <img src="../assets/images/logoklein.png" class="brand-image img-circle"
                        style="opacity: .8;color:white">
                    <strong><span class="brand-text font-weight" style="font-family: 'Exo', sans-serif;">Nemesus
                            World</span></strong>
                </a>
                <div class="sidebar">
                    <div class="user-panel mt-3 pb-3 mb-3 d-flex">
                        <strong><span class="brand-text font-weight"
                                style="font-family: 'Exo', sans-serif;">Verwaltungsmenü</span></strong>
                        <strong><span class="brand-text font-weight floag-right text-muted"
                                style="font-family: 'Exo', sans-serif; font-size: 0.5vw">&nbsp;&nbsp;&nbsp;&nbsp;by
                                Nemesus</span></strong>
                    </div>
                    <nav class="mt-2">
                        <ul class="mt-1 nav nav-pills nav-sidebar flex-column" data-widget="treeview" role="menu"
                            data-accordion="false">
                            <li class="nav-header">HILFE</li>
                            <li class="nav-item" v-on:click="startFAQ()">
                                <a class="nav-link" v-if="level <= 3">
                                    <i class="nav-icon fas fa-shoe-prints"></i>
                                    <p style="color:white">
                                        Erste Schritte
                                    </p>
                                </a>
                            </li>
                            <li class="nav-item" v-on:click="startHelp()">
                                <a class="nav-link">
                                    <i class="nav-icon fas fa-question-circle"></i> <p style="color:white">
                                        Befehle & Tasten
                                        </p>
                                </a>
                            </li>
                            <li class="nav-item" v-on:click="startSettings()">
                                <a class="nav-link">
                                    <i class="nav-icon fas fa-hammer"></i>
                                    <p style="color:white">
                                        Einstellungen
                                    </p>
                                </a>
                            </li>
                            <li class="nav-item" v-if="admin == 0 && tester >= 1" v-on:click="startAdminHelp()">
                                <a class="nav-link">
                                    <i class="nav-icon fas fa-question"></i>
                                    <p style="color:white">
                                        Testerbefehle
                                    </p>
                                </a>
                            </li>
                            <li class="nav-item" v-if="admin >= 1" v-on:click="startAdminHelp()">
                                <a class="nav-link">
                                    <i class="nav-icon fas fa-question"></i>
                                    <p style="color:white">
                                        Adminbefehle
                                    </p>
                                </a>
                            </li>
                            <li class="nav-item" v-if="job > 0" v-on:click="startJobHelp()">
                                <a class="nav-link">
                                    <i class="nav-icon fas fa-info-circle"></i>
                                    <p style="color:white">
                                        Jobhilfe
                                    </p>
                                </a>
                            </li>
                            <li class="nav-header">ALLGEMEIN</li>
                            <li class="nav-item" v-on:click="startStats()">
                                <a class="nav-link">
                                    <i class="nav-icon fas fa-users"></i>
                                    <p style="color:white">
                                        Statistiken
                                    </p>
                                </a>
                            </li>
                            <li class="nav-item" v-on:click="startPaydays()">
                                <a class="nav-link">
                                    <i class="nav-icon fa-solid fa-file-invoice"></i>
                                    <p style="color:white">
                                        Paydays
                                    </p>
                                </a>
                            </li>
                            <li class="nav-item" v-on:click="startCars('character')">
                                <a class="nav-link">
                                    <i class="nav-icon fa-solid fa-car"></i>
                                    <p style="color:white">
                                        Fahrzeuge
                                    </p>
                                </a>
                            </li>
                            <li class="nav-item" v-on:click="startShop()">
                                <a class="nav-link">
                                    <i class="nav-icon fa-solid fa-dollar-sign"></i>
                                    <p style="color:white">
                                        NW Shop
                                    </p>
                                </a>
                            </li>
                            <li class="nav-header">TICKETS</li>
                            <li class="nav-item" v-on:click="startTicket()">
                                <a class="nav-link">
                                    <i class="nav-icon fas fa-ticket-alt"></i>
                                    <p style="color:white">
                                        Ticket erstellen
                                    </p>
                                </a>
                            </li>
                            <li class="nav-item" v-on:click="myTickets()">
                                <a class="nav-link">
                                    <i class="nav-icon fas fa-clipboard-list"></i>
                                    <p style="color:white">
                                        Meine Tickets
                                        <span class="badge badge-info right">{{ticketCount}}</span>
                                    </p>
                                </a>
                            </li>
                            <li class="nav-header" v-if="faction > 0">FRAKTION</li>
                            <li class="nav-item" v-if="faction > 0" v-on:click="startFaction()">
                                <a class="nav-link">
                                    <i class="nav-icon fas fa-user-tie"></i>
                                    <p style="color:white">
                                        Fraktionsinfos
                                    </p>
                                </a>
                            </li>
                            <li class="nav-item" v-if="faction > 0" v-on:click="startCars('faction')">
                                <a class="nav-link">
                                    <i class="nav-icon fa-solid fa-car"></i>
                                    <p style="color:white">
                                        Fahrzeuge
                                    </p>
                                </a>
                            </li>
                            <li class="nav-item" v-if="faction > 0 && factionrang >= 10" v-on:click="factionLog()">
                                <a class="nav-link">
                                    <i class="nav-icon fas fa-circle nav-icon"></i>
                                    <p style="color:white">
                                        Fraktionslog
                                    </p>
                                </a>
                            </li>
                            <li class="nav-item" v-if="faction > 0 && factionrang >= 10" v-on:click="weaponLog()">
                                <a class="nav-link">
                                    <i class="nav-icon fas fa-circle nav-icon"></i>
                                    <p style="color:white" v-if="faction == 1">
                                        Waffenkammerlog
                                    </p>
                                    <p style="color:white" v-if="faction == 2 || faction == 3">
                                        Waffenkammerlog
                                    </p>
                                    <p style="color:white" v-if="faction == 4">
                                        Staatskassenlog
                                    </p>
                                </a>
                            </li>
                            <li class="nav-item" v-if="faction == 1 && factionrang >= 10" v-on:click="asservatenLog()">
                                <a class="nav-link">
                                    <i class="nav-icon fas fa-circle nav-icon"></i>
                                    <p style="color:white">
                                        Asservatenkammerlog
                                    </p>
                                </a>
                            </li>
                            <li class="nav-item" v-if="faction > 0 && factionrang >= 10"
                                v-on:click="startFactionSettings()">
                                <a class="nav-link">
                                    <i class="nav-icon fas fa-circle nav-icon"></i>
                                    <p style="color:white">
                                        Einstellungen
                                    </p>
                                </a>
                            </li>
                            <li class="nav-header" v-if="group > -1">GRUPPIERUNG</li>
                            <li class="nav-item" v-if="group > -1" v-on:click="startGroup()">
                                <a class="nav-link">
                                    <i class="nav-icon fas fa-user-tie"></i>
                                    <p style="color:white">
                                        Gruppierungsinfos
                                    </p>
                                </a>
                            </li>
                            <li class="nav-item" v-if="group > -1" v-on:click="startCars('group')">
                                <a class="nav-link">
                                    <i class="nav-icon fa-solid fa-car"></i>
                                    <p style="color:white">
                                        Fahrzeuge
                                    </p>
                                </a>
                            </li>
                            <li class="nav-item" v-if="group > -1 && grouprang >= 10" v-on:click="startGroupSettings()">
                                <a class="nav-link">
                                    <i class="nav-icon fas fa-circle nav-icon"></i>
                                    <p style="color:white">
                                        Einstellungen
                                    </p>
                                </a>
                            </li>
                            <li class="nav-item" v-if="group > -1 && grouprang >= 10" v-on:click="groupLog1()">
                                <a class="nav-link">
                                    <i class="nav-icon fas fa-circle nav-icon"></i>
                                    <p style="color:white">
                                        Gruppierungslog
                                    </p>
                                </a>
                            </li>
                            <li class="nav-item" v-if="group > -1 && grouprang >= 10" v-on:click="groupLog2()">
                                <a class="nav-link">
                                    <i class="nav-icon fas fa-circle nav-icon"></i>
                                    <p style="color:white">
                                        Wirtschaftslog
                                    </p>
                                </a>
                            </li>
                            <li class="nav-header">HAUS</li>
                            <li class="nav-item" v-on:click="startHouse()">
                                <a class="nav-link">
                                    <i class="nav-icon fas fa-house-user"></i>
                                    <p style="color:white">
                                        Hausverwaltung
                                    </p>
                                </a>
                            </li>
                            <li class="nav-item" v-if="menushowhouse == true">
                                <a class="nav-link" v-on:click="startMoebel()">
                                    <i class="nav-icon fas fa-chair"></i>
                                    <p style="color:white">
                                        Möbelverwaltung
                                    </p>
                                </a>
                            </li>
                            <li class="nav-header">BUSINESS</li>
                            <li class="nav-item" v-on:click="startBizz()">
                                <a class="nav-link">
                                    <i class="nav-icon fas fa-store"></i>
                                    <p style="color:white">
                                        Businessverwaltung
                                    </p>
                                </a>
                            </li>
                        </ul>
                    </nav>
                </div>
            </aside>
            <div class="centering2" style="height: 100%; background-color: rgba(69, 77, 85, .8);">
                <div class="row d-flex justify-content-center"
                    style="margin-left: 15rem !important; margin-top: 5.0vh; height:90vh; overflow-x: auto;"
                    v-if="menushowcharacter">
                    <div class="col-md-12">
                        <div class="col-md-12 mt-1">
                            <div class="box box-default">
                                <div class="card card-primary card-outline">
                                    <div class="card-header" style="font-family: 'Exo', sans-serif;">Statistiken</div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="card card-primary">
                                                    <div class="card-header">
                                                        <h3 class="card-title">Charakterinfos</h3>
                                                    </div>
                                                    <div class="card-body box-profile">
                                                        <div class="text-center">
                                                            <img class="profile-user-img img-fluid img-circle"
                                                                v-bind:src="characterinfos.screen">
                                                        </div>
                                                        <h3 class="profile-username text-center">
                                                            {{characterinfos.name}}
                                                        </h3>
                                                        <p class="text-muted text-center"
                                                            v-if="characterinfos.gender == 1">
                                                            Männlich
                                                        </p>
                                                        <p class="text-muted text-center" v-else>
                                                            Weiblich
                                                        </p>
                                                        <p class="text-muted text-center">
                                                            {{factionname}}
                                                            -
                                                            {{rangname}}
                                                        </p>
                                                        <p class="text-muted text-center"
                                                            v-if="(Date.now()/1000) < characterinfos.ck && characterinfos.ck != 0">
                                                            <b style="color:green">Aktive CK-Erlaubnis bis:
                                                                {{timeConverter(characterinfos.ck)}}</b>
                                                        </p>
                                                        <ul class="list-group list-group-bordered">
                                                            <li class="list-group-item ">
                                                                <b>Geburtsdatum</b> <a
                                                                    class="float-right">{{characterinfos.birth}}</a>
                                                            </li>
                                                            <li class="list-group-item">
                                                                <b>Geld</b> <a
                                                                    class="float-right">{{characterinfos.cash}}$</a>
                                                            </li>
                                                            <li class="list-group-item">
                                                                <b>Konto</b> <a
                                                                    class="float-right">{{characterinfos.bank}}$</a>
                                                            </li>
                                                            <li class="list-group-item">
                                                                <b>Größe</b> <a
                                                                    class="float-right">{{characterinfos.size}}</a>
                                                            </li>
                                                            <li class="list-group-item">
                                                                <b>Augenfarbe</b> <a
                                                                    class="float-right">{{characterinfos.eyecolor}}</a>
                                                            </li>
                                                            <li class="list-group-item">
                                                                <b>Herkunft</b> <a
                                                                    class="float-right">{{characterinfos.origin}}</a>
                                                            </li>
                                                            <li class="list-group-item">
                                                                <b>Job</b> <a class="float-right">{{jobname}}</a>
                                                            </li>
                                                            <li class="list-group-item"
                                                                v-if="characterinfos.arrested > 0">
                                                                <b style="color:#FF0000">Im Gefängnis [Zelle
                                                                    {{characterinfos.cell}}]
                                                                    für</b> <a style="color:#FF0000"
                                                                    class="float-right">{{characterinfos.arrested}}
                                                                    Minuten</a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="card card-primary">
                                                    <div class="card-header">
                                                        <h3 class="card-title">Sonstiges</h3>
                                                    </div>
                                                    <div class="card-body">
                                                        <strong><i class="fas fa-book mr-1"></i> Abschluss</strong>
                                                        <p class="text-muted">
                                                            {{characterinfos.education}}
                                                        </p>
                                                        <hr>
                                                        <strong><i class="fas fa-map-marker-alt mr-1"></i>
                                                            Herkunft</strong>
                                                        <p class="text-muted">{{characterinfos.origin}}</p>
                                                        <hr>
                                                        <strong><i class="fas fa-pencil-alt mr-1"></i> Besondere
                                                            Fähigkeiten</strong>
                                                        <p class="text-muted">
                                                            <span class="tag tag-info">{{characterinfos.skills}}</span>
                                                        </p>
                                                        <hr>
                                                        <strong><i class="far fa-file-alt mr-1"></i> Aussehen</strong>
                                                        <p class="text-muted">{{characterinfos.appearance}}
                                                        </p>
                                                        <hr>
                                                        <strong><i class="far fa-file-alt mr-1 mb-2"></i>
                                                            Skills</strong>
                                                        <div>
                                                            <i class="fas fa-truck mr-1"></i>Truckerskill:
                                                            {{getSkillName(parseInt(characterinfos.truckerskill/45))}}
                                                            <div class="progress progress-sm active mt-1">
                                                                <div class="progress-bar progress-bar-success progress-bar-striped"
                                                                    role="progressbar" aria-valuenow="0"
                                                                    aria-valuemin="0" aria-valuemax="100"
                                                                    :style="{ 'width': parseInt(characterinfos.truckerskill/225*100) + '%' }">
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="mt-2">
                                                            <i class="fa-solid fa-user-secret mr-1"></i>Diebesskill:
                                                            {{getSkillName(parseInt(characterinfos.thiefskill/25))}}
                                                            <div class="progress progress-sm active mt-1">
                                                                <div class="progress-bar progress-bar-success progress-bar-striped"
                                                                    role="progressbar" aria-valuenow="100"
                                                                    aria-valuemin="0" aria-valuemax="100"
                                                                    :style="{ 'width': parseInt(characterinfos.thiefskill/150*100) + '%' }">
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="mt-2">
                                                            <i class="fa-solid fa-solid fa-fish mr-1"></i>Angelskill:
                                                            {{getSkillName(parseInt(characterinfos.fishingskill/35))}}
                                                            <div class="progress progress-sm active mt-1">
                                                                <div class="progress-bar progress-bar-success progress-bar-striped"
                                                                    role="progressbar" aria-valuenow="100"
                                                                    aria-valuemin="0" aria-valuemax="100"
                                                                    :style="{ 'width': parseInt(characterinfos.fishingskill/175*100) + '%' }">
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="mt-2">
                                                            <i class="fa-solid fa-solid fa-bus mr-1"></i>Busskill:
                                                            {{getSkillName(parseInt(characterinfos.busskill/35))}}
                                                            <div class="progress progress-sm active mt-1">
                                                                <div class="progress-bar progress-bar-success progress-bar-striped"
                                                                    role="progressbar" aria-valuenow="100"
                                                                    aria-valuemin="0" aria-valuemax="100"
                                                                    :style="{ 'width': parseInt(characterinfos.busskill/175*100) + '%' }">
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="mt-2">
                                                            <i class="fa-solid fa-cow mr-1"></i>Landwirtskill:
                                                            {{getSkillName(parseInt(characterinfos.farmingskill/25))}}
                                                            <div class="progress progress-sm active mt-1">
                                                                <div class="progress-bar progress-bar-success progress-bar-striped"
                                                                    role="progressbar" aria-valuenow="100"
                                                                    aria-valuemin="0" aria-valuemax="100"
                                                                    :style="{ 'width': parseInt(characterinfos.farmingskill/150*100) + '%' }">
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="mt-2">
                                                            <i class="fa-solid fa-gun mr-1"></i>Craftingskill:
                                                            {{getSkillName(parseInt(characterinfos.craftingskill/25))}}
                                                            <div class="progress progress-sm active mt-1">
                                                                <div class="progress-bar progress-bar-success progress-bar-striped"
                                                                    role="progressbar" aria-valuenow="100"
                                                                    aria-valuemin="0" aria-valuemax="100"
                                                                    :style="{ 'width': parseInt(characterinfos.craftingskill/75*100) + '%' }">
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div v-if="groups.length > 0">
                                                            <hr>
                                                            <strong><i class="fas fa-user-tie mr-1 mb-2"></i>
                                                                Gruppierungen</strong>
                                                            <div v-for="getgroup in groups" :key="getgroup.id">
                                                                <div v-if="getgroup.id == group">
                                                                    <button
                                                                        class="btn btn-block btn-primary btn-sm mb-1"
                                                                        type="submit"
                                                                        disabled>{{getgroup.name}}</button>
                                                                </div>
                                                                <div v-else>
                                                                    <button
                                                                        class="btn btn-block btn-primary btn-sm mb-1"
                                                                        type="submit"
                                                                        v-on:click="groupSwitch(getgroup.id)">{{getgroup.name}}</button>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div>
                                                            <hr>
                                                            <strong><i class="fas fa-user mr-1 mb-2"></i> Geworben
                                                                von</strong>
                                                            <div
                                                                v-if="accountinfos.level < 3 && accountinfos.geworben == 'Keiner'">
                                                                <input type="text" class="form-control"
                                                                    placeholder="Spielername" v-model="geworben"
                                                                    maxlength="35" name="mietpreis" autocomplete="off">
                                                                <button
                                                                    class="btn btn-block btn-primary btn-sm mb-1 mt-2"
                                                                    type="submit"
                                                                    v-on:click="saveGeworben(geworben)">Speichern</button>
                                                            </div>
                                                            <div v-else>
                                                                <p class="text-muted">
                                                                    {{accountinfos.geworben}}
                                                                </p>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-5">
                                                <div class="card card-primary card-outline">
                                                    <div class="card-header">Account Informationen</div>
                                                    <div class="card-body">
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <ul class="list-group list-group-bordered mb-3">
                                                                    <li class="list-group-item">
                                                                        <b>Account-ID</b> <a class="float-right">
                                                                            {{accountinfos.id}}</a>
                                                                    </li>
                                                                    <li class="list-group-item">
                                                                        <b>Accountname</b> <a
                                                                            class="float-right">{{accountinfos.name}}</a>
                                                                    </li>
                                                                    <li class="list-group-item">
                                                                        <b>Level</b> <a class="float-right"><span
                                                                                id="level">{{accountinfos.level}}</span></a>
                                                                        <div
                                                                            class="progress progress-sm active level-bar">
                                                                            <div class="progress-bar progress-bar-success progress-bar-striped"
                                                                                role="progressbar" aria-valuenow="100"
                                                                                aria-valuemin="0" aria-valuemax="100"
                                                                                :style="{ 'width': parseInt(accountinfos.play_points/nextlevelExp*100) + '%' }">
                                                                            </div>
                                                                        </div>
                                                                    </li>
                                                                    <li class="list-group-item">
                                                                        <b>Spielstunden</b> <a
                                                                            class="float-right">{{accountinfos.play_time}}h</a>
                                                                    </li>
                                                                    <li class="list-group-item">
                                                                        <b>Erfahrungspunkte</b> <a
                                                                            class="float-right"><span
                                                                                id="play_points">{{accountinfos.play_points}}</span></a>
                                                                    </li>
                                                                    <li class="list-group-item">
                                                                        <b>Kills</b> <a
                                                                            class="float-right">{{accountinfos.kills}}</a>
                                                                    </li>
                                                                    <li class="list-group-item">
                                                                        <b>Tode</b> <a
                                                                            class="float-right">{{accountinfos.deaths}}</a>
                                                                    </li>
                                                                    <li class="list-group-item"
                                                                        v-if="accountinfos.deaths > 0">
                                                                        <b>K/D</b> <a
                                                                            class="float-right">{{accountinfos.kills/accountinfos.deaths}}</a>
                                                                    </li>
                                                                    <li class="list-group-item" v-else>
                                                                        <b>K/D</b> <a class="float-right">0</a>
                                                                    </li>
                                                                    <li class="list-group-item">
                                                                        <b>Gehaltsscheck in</b> <a
                                                                            class="float-right">{{60-characterinfos.payday_points}}
                                                                            Minuten</a>
                                                                    </li>
                                                                    <li class="list-group-item">
                                                                        <b>Verbrechen</b> <a
                                                                            class="float-right">{{accountinfos.crimes}}</a>
                                                                    </li>
                                                                    <li class="list-group-item"
                                                                        v-if="accountinfos.premium == 1 && accountinfos.premium_time > (Date.now()/1000)">
                                                                        <b><span
                                                                                style="color:#bf8970">Premium</span></b>
                                                                        <a class="float-right">{{timeConverter(accountinfos.premium_time)}}
                                                                        </a>
                                                                    </li>
                                                                    <li class="list-group-item"
                                                                        v-if="accountinfos.premium == 2 && accountinfos.premium_time > (Date.now()/1000)">
                                                                        <b><span
                                                                                style="color:#c0c0c0">Premium</span></b>
                                                                        <a class="float-right">{{timeConverter(accountinfos.premium_time)}}
                                                                        </a>
                                                                    </li>
                                                                    <li class="list-group-item"
                                                                        v-if="accountinfos.premium == 3 && accountinfos.premium_time > (Date.now()/1000)">
                                                                        <b><span
                                                                                style="color:#ffd700">Premium</span></b>
                                                                        <a class="float-right">{{timeConverter(accountinfos.premium_time)}}
                                                                        </a>
                                                                    </li>
                                                                    <li class="list-group-item"
                                                                        v-if="accountinfos.premium == 0">
                                                                        <b><span>Premium</span></b>
                                                                        <a class="float-right">Nein
                                                                        </a>
                                                                    </li>
                                                                    <li class="list-group-item">
                                                                        <b>Coins</b> <a
                                                                            class="float-right">{{accountinfos.coins}}</a>
                                                                    </li>
                                                                    <li class="list-group-item">
                                                                        <b>Loginbonus</b> <a
                                                                            class="float-right">{{accountinfos.login_bonus}}
                                                                            Tage</a>
                                                                    </li>
                                                                    <li class="list-group-item">
                                                                        <b>Letzter Login</b> <a
                                                                            class="float-right">{{timeConverter(accountinfos.last_login)}}</a>
                                                                    </li>
                                                                    <li class="list-group-item">
                                                                        <b>Registriert seit</b> <a
                                                                            class="float-right">{{timeConverter(accountinfos.account_created)}}</a>
                                                                    </li>
                                                                    <li class="list-group-item">
                                                                        <b>Letzte Speicherung</b> <a
                                                                            class="float-right">{{timeConverter(accountinfos.last_save)}}</a>
                                                                    </li>
                                                                    <li class="list-group-item">
                                                                        <b>Namechanges</b> <a
                                                                            class="float-right">{{accountinfos.namechanges}}</a>
                                                                    </li>
                                                                    <li class="list-group-item"
                                                                        v-if="accountinfos.warns > 0">
                                                                        <b>Verwarnungen</b> <a class="float-right"><span
                                                                                style="color: red">{{accountinfos.warns}}/5
                                                                                - (<a
                                                                                    v-if="accountinfos.warns >= 1">{{warntext[0]}}</a><a
                                                                                    v-if="accountinfos.warns >= 2"> ,
                                                                                    {{warntext[1]}}</a><a
                                                                                    v-if="accountinfos.warns >= 3"> ,
                                                                                    {{warntext[2]}}</a><a
                                                                                    v-if="accountinfos.warns >= 4"> ,
                                                                                    {{warntext[3]}}</a><a
                                                                                    v-if="accountinfos.warns >= 5"> ,
                                                                                    {{warntext[4]}}</a>)</span></a>
                                                                    </li>
                                                                </ul>
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
                <div class="centering2" style="height: 100%;" v-if="showshop">
                    <div class="row d-flex justify-content-center"
                        style="margin-left: 15rem !important; margin-top: 5.0vh; height:90vh; overflow-x: auto;">
                        <div class="col-md-12">
                            <div class="col-md-12 mt-1">
                                <div class="box box-default">
                                    <div class="card card-primary card-outline">
                                        <div class="card-header" style="font-family: 'Exo', sans-serif;">NW Shop</div>
                                        <div class="card-body">
                                            <div style="display: flex; justify-content: center; align-items: center">
                                                <h4 style="font-family: 'Exo', sans-serif;">Verfügbare Coins:
                                                    {{shopCoins}}
                                                    Coins</h4>
                                            </div>
                                            <hr />
                                            <div class="col-xs-12 table-responsive">
                                                <table id="logtable" class="table table-bordered" style="width:100%">
                                                    <thead class="table-primary">
                                                        <tr id="tablehead" class="tablehead">
                                                            <th>Beschreibung</th>
                                                            <th>Coins</th>
                                                            <th>AKtion</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr>
                                                            <td>1x Namechange</td>
                                                            <td>50 Coins</td>
                                                            <td><a @click="getShop(1)">Freischalten</a></td>
                                                        </tr>
                                                        <tr>
                                                            <td>12h Erfahrungspunkte Boost</td>
                                                            <td>50 Coins</td>
                                                            <td><a @click="getShop(2)">Freischalten</a></td>
                                                        </tr>
                                                        <tr>
                                                            <td>24h Erfahrungspunkte Boost</td>
                                                            <td>75 Coins</td>
                                                            <td><a @click="getShop(3)">Freischalten</a></td>
                                                        </tr>
                                                        <tr>
                                                            <td>+1 Hausslot</td>
                                                            <td>150 Coins</td>
                                                            <td><a @click="getShop(4)">Freischalten</a></td>
                                                        </tr>
                                                        <tr>
                                                            <td>+1 Fahrzeugslot</td>
                                                            <td>150 Coins</td>
                                                            <td><a @click="getShop(5)">Freischalten</a></td>
                                                        </tr>
                                                        <tr>
                                                            <td>Premium Bronze 30 Tage</td>
                                                            <td>100 Coins</td>
                                                            <td><a @click="getShop(6)">Freischalten</a></td>
                                                        </tr>
                                                        <tr>
                                                            <td>Premium Silber 30 Tage</td>
                                                            <td>200 Coins</td>
                                                            <td><a @click="getShop(7)">Freischalten</a></td>
                                                        </tr>
                                                        <tr>
                                                            <td>Premium Gold 30 Tage</td>
                                                            <td>300 Coins</td>
                                                            <td><a @click="getShop(8)">Freischalten</a></td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                            <div class="alert alert-primary"
                                                style="display: flex; justify-content: center; align-items: center; margin-top: 2vw"
                                                v-if="percentage < 100">
                                                Info: Coins können ingame erspielt werden, z.B durch ein Levelup, werben
                                                von anderen
                                                Spielern und vielem mehr! Weitere Informationen findest du im UCP unter:
                                                Premium
                                                Informationen.
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="centering2" style="height: 100%;" v-if="showfaq">
                    <div class="row d-flex justify-content-center"
                        style="margin-left: 15rem !important; margin-top: 5.0vh; height:90vh; overflow-x: auto;">
                        <div class="col-md-12">
                            <div class="col-md-12 mt-1">
                                <div class="box box-default">
                                    <div class="card card-primary card-outline">
                                        <div class="card-header" style="font-family: 'Exo', sans-serif;">Erste Schritte
                                        </div>
                                        <div class="card-body">
                                            <div style="display: flex; justify-content: center; align-items: center">
                                                <h4 style="font-family: 'Exo', sans-serif;">{{percentage}}%
                                                    abgeschlossen</h4>
                                            </div>
                                            <div style="display: flex; justify-content: center; align-items: center">
                                                <div class="progress-bar progress-bar-info progress-bar-striped progress-bar1"
                                                    role="progressbar" aria-valuenow="40" aria-valuemin="0"
                                                    aria-valuemax="100" :style="{ 'width': percentage+'%' }">
                                                </div>
                                            </div>
                                            <div
                                                style="display: flex; justify-content: center; align-items: center; margin-top: 2vw">
                                                Für den Anfang findest du unten einige wichtige Todos, welche dir das
                                                Leben auf dem
                                                Server erleichert! Solltest du alle Todos abschließen, erhältst du
                                                <strong style="color:#bf8970">&nbsp;3 Tage Premium Bronze&nbsp;</strong>
                                                als Geschenk
                                                (Beim Login)!
                                            </div>
                                            <hr />
                                            <div class="row"
                                                style="margin-top: 1vw;display: flex; justify-content: center; align-items: center;">
                                                <ul class="todo-list">
                                                    <li class="mt-1">
                                                        <span class="handle">
                                                        </span>
                                                        <input v-if="faqlist[0] == '1'" type="checkbox" value="" checked
                                                            disabled>
                                                        <input v-else type="checkbox" value="" disabled>
                                                        <span class="text">Lade dir den Nemesus-World Gamemode von
                                                            <strong>Nemesus.de</strong> runter und starte diesen!</span>
                                                    </li>
                                                    <li class="mt-1">
                                                        <span class="handle">
                                                        </span>
                                                        <input v-if="faqlist[1] == '1'" type="checkbox" value="" checked
                                                            disabled>
                                                        <input v-else type="checkbox" value="" disabled>
                                                        <span class="text">Miete dir vorne am Brunnen einen
                                                            Roller!</span>
                                                    </li>
                                                    <li class="mt-1">
                                                        <span class="handle">
                                                        </span>
                                                        <input v-if="faqlist[2] == '1'" type="checkbox" value="" checked
                                                            disabled>
                                                        <input v-else type="checkbox" value="" disabled>
                                                        <span class="text">Fahre zu einer Bank und erstelle dein erstes
                                                            eigenes
                                                            Bankkonto!</span>
                                                    </li>
                                                    <li class="mt-1">
                                                        <span class="handle">
                                                        </span>
                                                        <input v-if="faqlist[3] == '1'" type="checkbox" value="" checked
                                                            disabled>
                                                        <input v-else type="checkbox" value="" disabled>
                                                        <span class="text">Kaufe im 24/7 ein Smartphone!</span>
                                                    </li>
                                                    <li class="mt-1">
                                                        <span class="handle">
                                                        </span>
                                                        <input v-if="faqlist[4] == '1'" type="checkbox" value="" checked
                                                            disabled>
                                                        <input v-else type="checkbox" value="" disabled>
                                                        <span class="text">Besuche einen Kleidungsladen und kleide dich
                                                            ein!</span>
                                                    </li>
                                                    <li class="mt-1">
                                                        <span class="handle">
                                                        </span>
                                                        <input v-if="faqlist[5] == '1'" type="checkbox" value="" checked
                                                            disabled>
                                                        <input v-else type="checkbox" value="" disabled>
                                                        <span class="text">Besuche einen Friseur!</span>
                                                    </li>
                                                    <li class="mt-1">
                                                        <span class="handle">
                                                        </span>
                                                        <input v-if="faqlist[6] == '1'" type="checkbox" value="" checked
                                                            disabled>
                                                        <input v-else type="checkbox" value="" disabled>
                                                        <span class="text">Bestehe den Führerschein bei der
                                                            Fahrschule!</span>
                                                    </li>
                                                    <li class="mt-1">
                                                        <span class="handle">
                                                        </span>
                                                        <input v-if="faqlist[7] == '1'" type="checkbox" value="" checked
                                                            disabled>
                                                        <input v-else type="checkbox" value="" disabled>
                                                        <span class="text">Such nach einem Job im Rathaus!</span>
                                                    </li>
                                                    <li class="mt-1">
                                                        <span class="handle">
                                                        </span>
                                                        <input v-if="faqlist[8] == '1'" type="checkbox" value="" checked
                                                            disabled>
                                                        <input v-else type="checkbox" value="" disabled>
                                                        <span class="text">Verdiene dein erstes eigenes Geld - (Payday
                                                            erhalten)!</span>
                                                    </li>
                                                    <li class="mt-1">
                                                        <span class="handle">
                                                        </span>
                                                        <input v-if="faqlist[9] == '1'" type="checkbox" value="" checked
                                                            disabled>
                                                        <input v-else type="checkbox" value="" disabled>
                                                        <span class="text">Kaufe dein erstes eigenes Fahrzeug!</span>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div class="alert alert-primary"
                                                style="display: flex; justify-content: center; align-items: center; margin-top: 2vw"
                                                v-if="percentage < 100">
                                                Info: Sämtliche Orte findest du auf der Karte (ESC), dort kannst du mit
                                                einem
                                                Mausklick beliebige Orte markieren. Solltest du dennoch mal Hilfe
                                                benötigen, so
                                                kannst du links gerne ein Ticket erstellen!
                                            </div>
                                            <div class="alert alert-success"
                                                style="display: flex; justify-content: center; align-items: center; margin-top: 2vw"
                                                v-else>
                                                Du hast deinen Einstieg auf dem Server geschafft! Alle Wege stehen dir
                                                offen: Trete
                                                einer Fraktion bei, gründe deine eigene Firma und vieles mehr ...
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="centering2" style="height: 100%;" v-if="showpayday">
                    <div class="row d-flex justify-content-center"
                        style="margin-left: 15rem !important; margin-top: 5.0vh; height:90vh; overflow-x: auto;">
                        <div class="col-md-12">
                            <div class="col-md-12 mt-1">
                                <div class="box box-default">
                                    <div class="card card-primary card-outline">
                                        <div class="card-header" style="font-family: 'Exo', sans-serif;">Payday
                                            Übersicht</div>
                                        <div class="card-body">
                                            <div class="row">
                                                <div class="col-xs-12">
                                                    <h5 class="page-header">
                                                        <i class="fa-solid fa-file-invoice"></i> Gehaltsscheck vom
                                                        {{timeConverter(timestamp)}}
                                                    </h5>
                                                </div>
                                            </div>
                                            <div class="row" style="margin-top: 1vw">
                                                <div class="col-xs-12 table-responsive">
                                                    <table id="logtable" class="table table-bordered"
                                                        style="width:100%">
                                                        <thead class="table-primary">
                                                            <tr id="tablehead" class="tablehead">
                                                                <th>#</th>
                                                                <th>Beschreibung</th>
                                                                <th>Summe</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr v-for="(payday, index) in selectedpayday" :key="index">
                                                                <td>{{payday.modus}}</td>
                                                                <td>{{payday.setting}}</td>
                                                                <td v-if="payday.value >= 0"
                                                                    style="color:#90ee90;font-family: 'Exo', sans-serif;">
                                                                    +{{payday.value}}$</td>
                                                                <td v-else
                                                                    style="color:#FFCCCB;font-family: 'Exo', sans-serif;">
                                                                    {{payday.value}}$</td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <div class="row float-right mt-2 mr-5">
                                                        <div class="col-xs-6">
                                                            <div class="table-responsive">
                                                                <table class="table">
                                                                    <tr>
                                                                        <th>Total:</th>
                                                                        <td v-if="total >= 0" style="color:#90ee90">
                                                                            +{{total}}$</td>
                                                                        <td v-else style="color:#FFCCCB">{{total}}$</td>
                                                                    </tr>
                                                                </table>
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
                <div class="centering2" style="height: 100%;" v-if="showpayday2">
                    <div class="row d-flex justify-content-center"
                        style="margin-left: 15rem !important; margin-top: 5.0vh; height:90vh; overflow-x: auto;">
                        <div class="col-md-12">
                            <div class="col-md-12 mt-1">
                                <div class="box box-default">
                                    <div class="card card-primary card-outline">
                                        <div class="card-header" style="font-family: 'Exo', sans-serif;">Payday
                                            Übersicht</div>
                                        <div class="card-body">
                                            <div class="row" style="margin-top: 1vw">
                                                <div class="col-xs-12 table-responsive">
                                                    <table id="logtable" class="table table-bordered" style="width:100%"
                                                        v-if="paydays.length > 0">
                                                        <thead class="table-primary">
                                                            <tr id="tablehead" class="tablehead">
                                                                <th>#</th>
                                                                <th>Datum</th>
                                                                <th>Total</th>
                                                                <th>Aktion</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr v-for="(payday, index) in paydays" :key="index">
                                                                <td>{{payday.id}}</td>
                                                                <td>Gehaltscheck vom {{timeConverter(payday.timestamp)}}
                                                                </td>
                                                                <td v-if="payday.total >= 0"
                                                                    style="color:#90ee90;font-family: 'Exo', sans-serif;">
                                                                    +{{payday.total}}$</td>
                                                                <td v-else
                                                                    style="color:#FFCCCB;font-family: 'Exo', sans-serif;">
                                                                    {{payday.total}}$</td>
                                                                <td><a
                                                                        v-on:click="setPayday(payday.total, payday.timestamp, payday.id)">Öffnen</a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <h3 v-if="paydays.length <= 0">Keine Paydays vorhanden!</h3>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row d-flex justify-content-center"
                    style="margin-left: 15rem !important; margin-top: 5.0vh; height:90vh; overflow-x: auto;"
                    v-if="menushowhouse">
                    <div class="col-md-12">
                        <div class="col-md-12 mt-1">
                            <div class="box box-default">
                                <div class="card card-primary card-outline">
                                    <div class="card-header" style="font-family: 'Exo', sans-serif;">Hausverwaltung
                                    </div>
                                    <div v-if="houseinfos != null">
                                        <div class="card-body">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="card card-primary">
                                                        <div class="card-header">
                                                            <h3 class="card-title">Allgemeine Informationen</h3>
                                                        </div>
                                                        <div class="card-body box-profile">
                                                            <h3 class="profile-username text-center">
                                                                Hausnummer: {{houseinfos.id}}
                                                            </h3>
                                                            <ul class="list-group list-group-bordered">
                                                                <li class="list-group-item ">
                                                                    <b>Eigentümer</b> <a
                                                                        class="float-right">{{houseinfos.owner}}</a>
                                                                </li>
                                                                <li class="list-group-item ">
                                                                    <b>Aktueller Preis</b> <a
                                                                        class="float-right">{{houseinfos.price}}$</a>
                                                                </li>
                                                                <li class="list-group-item ">
                                                                    <b>Energielieferant</b> <a
                                                                        class="float-right">{{houseinfos.elecname}}</a>
                                                                </li>
                                                                <li class="list-group-item ">
                                                                    <b>Türe</b> <a class="float-right"><strong><span
                                                                                style="color:red"
                                                                                v-if="houseinfos.locked == 1">Verschlossen</span><span
                                                                                style="color:green"
                                                                                v-else>Offen</span></strong></a>
                                                                </li>
                                                                <li class="list-group-item ">
                                                                    <b>Interior</b> <a
                                                                        class="float-right">{{interiorart}} -
                                                                        {{houseinfos.interior}}
                                                                        <button class="btn btn-success btn-sm"
                                                                            type="submit"
                                                                            v-if="name == houseinfos.owner"
                                                                            v-on:click="newInterior()">Interiorausbau</button></a>
                                                                </li>
                                                                <li class="list-group-item">
                                                                    <b>Mietpreis</b> <a
                                                                        class="float-right">{{houseinfos.rental}}$
                                                                        <button v-if="name != houseinfos.owner"
                                                                            class="btn btn-danger btn-sm" type="submit"
                                                                            v-on:click="endRent()">Miete
                                                                            beenden</button></a>
                                                                </li>
                                                                <li class="list-group-item">
                                                                    <b>Mieter</b> <a
                                                                        class="float-right">{{houseinfos.tenants}}</a>
                                                                </li>
                                                                <li class="list-group-item"
                                                                    v-if="houseinfos.housegroup != -1">
                                                                    <b>Lagerkapazität</b> <a
                                                                        class="float-right">{{houseinfos.stock}}/3500</a>
                                                                </li>
                                                            </ul>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-3"
                                                    v-if="name == houseinfos.owner && (houseinfos.tenants > 0 && tenants)">
                                                    <div class="card card-primary">
                                                        <div class="card-header">
                                                            <h3 class="card-title">Mieter</h3>
                                                        </div>
                                                        <div class="card-body">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <div class="table-responsive-md">
                                                                        <table class="table table-bordered">
                                                                            <thead class="table-primary">
                                                                                <tr>
                                                                                    <th>Name</th>
                                                                                    <th>Rauswerfen</th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                                <tr v-for="tenant in tenants"
                                                                                    :key="tenant.ID">
                                                                                    <td>{{tenant.Name}}</td>
                                                                                    <td><button
                                                                                            class="btn btn-success btn-sm"
                                                                                            type="submit"
                                                                                            v-on:click="kickTenant(tenant.ID)">
                                                                                            <i
                                                                                                class="fas fa-check-circle"></i></button>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-3"
                                                    v-if="name == houseinfos.owner && (houseinfos.tenants <= 0 || !tenants)">
                                                    <div class="card card-primary">
                                                        <div class="card-header">
                                                            <h3 class="card-title">Mieterinformationen</h3>
                                                        </div>
                                                        <div class="card-body">
                                                            <div class="row">
                                                                <div
                                                                    class="col-md-12 justify-content-center text-center">
                                                                    <h3>Keine Mieter!</h3>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-9" v-if="name == houseinfos.owner">
                                                    <div class="card card-primary">
                                                        <div class="card-header">
                                                            <h3 class="card-title">Hausorganisation</h3>
                                                        </div>
                                                        <div class="card-body">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <div class="table-responsive-md">
                                                                        <table class="table table-bordered">
                                                                            <thead class="table-primary">
                                                                                <tr>
                                                                                    <th>Aktionsname</th>
                                                                                    <th>Eingabe</th>
                                                                                    <th>Aktion</th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td>Mietpreis einstellen</td>
                                                                                    <td><input type="text"
                                                                                            class="form-control"
                                                                                            placeholder="Mietpreis in Dollar"
                                                                                            v-model="mietpreis"
                                                                                            maxlength="6"
                                                                                            name="mietpreis"
                                                                                            autocomplete="off">
                                                                                    </td>
                                                                                    <td> <button
                                                                                            class="btn btn-success btn-sm"
                                                                                            type="submit"
                                                                                            v-on:click="changeMietpreis()">
                                                                                            <i
                                                                                                class="fas fa-check-circle"></i>
                                                                                        </button>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>Neuen Eigentümer festlegen</td>
                                                                                    <td><input type="text"
                                                                                            class="form-control"
                                                                                            placeholder="Name vom neuen Eigentümer"
                                                                                            v-model="newname"
                                                                                            maxlength="35"
                                                                                            name="newname"
                                                                                            autocomplete="off">
                                                                                    </td>
                                                                                    <td> <button
                                                                                            class="btn btn-success btn-sm"
                                                                                            type="submit"
                                                                                            v-on:click="changeEigentuemer()">
                                                                                            <i
                                                                                                class="fas fa-check-circle"></i>
                                                                                        </button>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>Schloss auswechseln</td>
                                                                                    <td>1250$</td>
                                                                                    <td> <button
                                                                                            class="btn btn-success btn-sm"
                                                                                            type="submit"
                                                                                            v-on:click="newHouseDoor()">
                                                                                            <i
                                                                                                class="fas fa-check-circle"></i>
                                                                                        </button>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>Klingelschild anbringen</td>
                                                                                    <td>/</td>
                                                                                    <td v-if="houseinfos.noshield == 0">
                                                                                        <button
                                                                                            class="btn btn-success btn-sm"
                                                                                            type="submit"
                                                                                            v-on:click="changeKlingelschild()">
                                                                                            <strong>Ja</strong>
                                                                                        </button> <button
                                                                                            class="btn btn-secondary btn-sm"
                                                                                            type="submit"
                                                                                            v-on:click="changeKlingelschild()">
                                                                                            <strong>Nein</strong>
                                                                                        </button>
                                                                                    </td>
                                                                                    <td v-if="houseinfos.noshield == 1">
                                                                                        <button
                                                                                            class="btn btn-secondary btn-sm"
                                                                                            type="submit"
                                                                                            v-on:click="changeKlingelschild()">
                                                                                            <strong>Ja</strong>
                                                                                        </button> <button
                                                                                            class="btn btn-success btn-sm"
                                                                                            type="submit"
                                                                                            v-on:click="changeKlingelschild()">
                                                                                            <strong>Nein</strong>
                                                                                        </button>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>Alle Mieter rauswerfen</td>
                                                                                    <td>/</td>
                                                                                    <td> <button
                                                                                            class="btn btn-success btn-sm"
                                                                                            type="submit">
                                                                                            <i class="fas fa-check-circle"
                                                                                                v-on:click="kickAllTenants()"></i>
                                                                                        </button>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>Hausschlüssel nachmachen lassen
                                                                                    </td>
                                                                                    <td>250$</td>
                                                                                    <td> <button
                                                                                            class="btn btn-success btn-sm"
                                                                                            type="submit">
                                                                                            <i class="fas fa-check-circle"
                                                                                                v-on:click="newKey()"></i>
                                                                                        </button>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>Als Gruppierungshaus
                                                                                        setzen/wegsetzen</td>
                                                                                    <td>/</td>
                                                                                    <td> <button
                                                                                            class="btn btn-success btn-sm"
                                                                                            type="submit">
                                                                                            <i class="fas fa-check-circle"
                                                                                                v-on:click="groupHouse()"></i>
                                                                                        </button>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr v-if="houseinfos.housegroup != -1">
                                                                                    <td>Blip setzen</td>
                                                                                    <td><input type="text"
                                                                                            class="form-control"
                                                                                            placeholder="Blip"
                                                                                            v-model="houseblip"
                                                                                            maxlength="3" name="blip"
                                                                                            autocomplete="off">
                                                                                    </td>
                                                                                    <td> <button
                                                                                            class="btn btn-success btn-sm"
                                                                                            type="submit">
                                                                                            <i class="fas fa-check-circle"
                                                                                                v-on:click="setBlip()"></i>
                                                                                        </button>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr v-if="houseinfos.housegroup != -1">
                                                                                    <td>Produktpreis setzen</td>
                                                                                    <td><input type="text"
                                                                                            class="form-control"
                                                                                            placeholder="50"
                                                                                            v-model="houseinfos.stockprice"
                                                                                            maxlength="3"
                                                                                            name="stockprice"
                                                                                            autocomplete="off">
                                                                                    </td>
                                                                                    <td> <button
                                                                                            class="btn btn-success btn-sm"
                                                                                            type="submit">
                                                                                            <i class="fas fa-check-circle"
                                                                                                v-on:click="setStock()"></i>
                                                                                        </button>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>Haus verkaufen</td>
                                                                                    <td>Aktueller Hauspreis:
                                                                                        {{Math.floor(houseinfos.price)}}$
                                                                                    </td>
                                                                                    <td> <button
                                                                                            class="btn btn-danger btn-sm"
                                                                                            type="submit">
                                                                                            <i class="fas fa-check-circle"
                                                                                                v-on:click="sellHouse()"></i>
                                                                                        </button>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
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
                <div class="row d-flex justify-content-center"
                    style="margin-left: 15rem !important; margin-top: 5.0vh; height:90vh; overflow-x: auto;"
                    v-if="menubizzshow">
                    <div class="col-md-12">
                        <div class="col-md-12 mt-1">
                            <div class="box box-default">
                                <div class="card card-primary card-outline">
                                    <div class="card-header" style="font-family: 'Exo', sans-serif;">Businessverwaltung
                                    </div>
                                    <div v-if="bizzinfos != null">
                                        <div class="card-body">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="card card-primary">
                                                        <div class="card-header">
                                                            <h3 class="card-title">Allgemeine Informationen</h3>
                                                        </div>
                                                        <div class="card-body box-profile">
                                                            <h3 class="profile-username text-center">
                                                                Business: {{bizzinfos.name}}[{{bizzinfos.id}}]
                                                            </h3>
                                                            <ul class="list-group list-group-bordered">
                                                                <li class="list-group-item ">
                                                                    <b>Eigentümer</b> <a
                                                                        class="float-right">{{bizzinfos.owner}}</a>
                                                                </li>
                                                                <li class="list-group-item ">
                                                                    <b>Energielieferant</b> <a
                                                                        class="float-right">{{bizzinfos.elecname}}</a>
                                                                </li>
                                                                <li class="list-group-item ">
                                                                    <b>Aktueller Preis</b> <a
                                                                        class="float-right">{{bizzinfos.price}}$</a>
                                                                </li>
                                                                <li class="list-group-item">
                                                                    <b>Preismultiplizerer</b> <a
                                                                        class="float-right">{{bizzinfos.multiplier.toFixed(1)}}</a>
                                                                </li>
                                                                <li class="list-group-item">
                                                                    <b>Produkte</b> <a
                                                                        class="float-right">{{bizzinfos.products}}/2000</a>
                                                                </li>
                                                                <li class="list-group-item"
                                                                    v-if="!IsAFuelStation(bizzinfos.id)">
                                                                    <b>Produktpreis</b> <a
                                                                        class="float-right">{{bizzinfos.prodprice}}$</a>
                                                                </li>
                                                                <li class="list-group-item"
                                                                    v-if="IsAFuelStation(bizzinfos.id)">
                                                                    <b>Produktpreis</b> <a
                                                                        class="float-right">{{bizzinfos.prodprice}}$/{{parseInt((bizzinfos.prodprice/4)-2)}}$</a>
                                                                </li>
                                                                <li class="list-group-item">
                                                                    <b>Kasseninhalt</b> <a
                                                                        class="float-right">{{bizzinfos.cash}}$</a>
                                                                </li>
                                                                <li class="list-group-item">
                                                                    <b>Steuern</b> <a
                                                                        class="float-right">{{bizzinfos.govcash}}$</a>
                                                                </li>
                                                                <li class="list-group-item">
                                                                    <b>Heutige Verkäufe</b> <a
                                                                        class="float-right">{{bizzinfos.funds}}</a>
                                                                </li>
                                                            </ul>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="card card-primary">
                                                        <div class="card-header">
                                                            <h3 class="card-title">Businessorganisation</h3>
                                                        </div>
                                                        <div class="card-body">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <div class="table-responsive-md">
                                                                        <table class="table table-bordered">
                                                                            <thead class="table-primary">
                                                                                <tr>
                                                                                    <th>Aktionsname</th>
                                                                                    <th>Eingabe</th>
                                                                                    <th>Aktion</th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td>Kassenverwaltung</td>
                                                                                    <td>{{bizzinfos.cash}}$</td>
                                                                                    <td> <button
                                                                                            class="btn btn-success btn-sm"
                                                                                            type="submit"
                                                                                            @click="insertBizz()"><i
                                                                                                class="fas fa-arrow-up"></i></button><button
                                                                                            class="btn btn-secondary btn-sm ml-1"
                                                                                            type="submit"
                                                                                            @click="castoutBizz()"><i
                                                                                                class="fas fa-arrow-down"></i></button>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>Produktpreis einstellen <span
                                                                                            v-if="bizzinfos.buyproducts == 0"
                                                                                            class="badge badge-success mb-1">Produktkauf
                                                                                            aktiviert</span><span
                                                                                            v-if="bizzinfos.buyproducts == 1"
                                                                                            class="badge badge-danger mb-1">Produktkauf
                                                                                            deaktiviert</span></td>
                                                                                    <td><input type="text"
                                                                                            class="form-control"
                                                                                            placeholder="Produktpreis in Dollar"
                                                                                            v-model="produktpreis"
                                                                                            maxlength="6"
                                                                                            name="mietpreis"
                                                                                            autocomplete="off">
                                                                                    </td>
                                                                                    <td> <button
                                                                                            class="btn btn-success btn-sm"
                                                                                            type="submit"
                                                                                            v-on:click="changeProduktpreis()">
                                                                                            <i
                                                                                                class="fas fa-check-circle"></i></button>
                                                                                        <button
                                                                                            class="btn btn-success btn-sm ml-1"
                                                                                            type="submit"
                                                                                            v-on:click="changeProduktStatus()">
                                                                                            <i class="fas fa-tag"></i>
                                                                                        </button>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr v-if="name == bizzinfos.owner">
                                                                                    <td>Geld Abholung ab
                                                                                        ({{bizzinfos.getmoney}}$)</td>
                                                                                    <td><input type="text"
                                                                                            class="form-control"
                                                                                            placeholder="Limit in Dollar"
                                                                                            v-model="getmoney"
                                                                                            maxlength="64"
                                                                                            name="bizzname"
                                                                                            autocomplete="off">
                                                                                    </td>
                                                                                    <td> <button
                                                                                            class="btn btn-success btn-sm"
                                                                                            type="submit"
                                                                                            v-on:click="changeGetMoney()">
                                                                                            <i
                                                                                                class="fas fa-check-circle"></i>
                                                                                        </button>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr v-if="name == bizzinfos.owner">
                                                                                    <td>Business Namen festlegen</td>
                                                                                    <td><input type="text"
                                                                                            class="form-control"
                                                                                            placeholder="Name vom Business"
                                                                                            v-model="bizzname"
                                                                                            maxlength="64"
                                                                                            name="bizzname"
                                                                                            autocomplete="off">
                                                                                    </td>
                                                                                    <td> <button
                                                                                            class="btn btn-success btn-sm"
                                                                                            type="submit"
                                                                                            v-on:click="changeBizzName()">
                                                                                            <i
                                                                                                class="fas fa-check-circle"></i>
                                                                                        </button>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr v-if="name == bizzinfos.owner">
                                                                                    <td>Neuen Eigentümer festlegen</td>
                                                                                    <td><input type="text"
                                                                                            class="form-control"
                                                                                            placeholder="Name vom neuen Eigentümer"
                                                                                            v-model="newname"
                                                                                            maxlength="35"
                                                                                            name="newname"
                                                                                            autocomplete="off">
                                                                                    </td>
                                                                                    <td> <button
                                                                                            class="btn btn-success btn-sm"
                                                                                            type="submit"
                                                                                            v-on:click="changeEigentuemerBizz()">
                                                                                            <i
                                                                                                class="fas fa-check-circle"></i>
                                                                                        </button>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr v-if="name == bizzinfos.owner">
                                                                                    <td>Schloss auswechseln</td>
                                                                                    <td>1250$</td>
                                                                                    <td> <button
                                                                                            class="btn btn-success btn-sm"
                                                                                            type="submit"
                                                                                            v-on:click="newBizzDoor()">
                                                                                            <i
                                                                                                class="fas fa-check-circle"></i>
                                                                                        </button>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>Preismultiplizierer einstellen
                                                                                        (1.0 - 3.0)</td>
                                                                                    <td><input type="text"
                                                                                            class="form-control"
                                                                                            placeholder="Preismultiplizerer"
                                                                                            v-model="setmultiplier"
                                                                                            maxlength="6"
                                                                                            name="setmultiplier"
                                                                                            autocomplete="off">
                                                                                    </td>
                                                                                    <td> <button
                                                                                            class="btn btn-success btn-sm"
                                                                                            type="submit"
                                                                                            v-on:click="changeMultiplier()">
                                                                                            <i
                                                                                                class="fas fa-check-circle"></i>
                                                                                        </button>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr v-if="name == bizzinfos.owner">
                                                                                    <td>Businessschlüssel nachmachen
                                                                                        lassen</td>
                                                                                    <td>250$</td>
                                                                                    <td> <button
                                                                                            class="btn btn-success btn-sm"
                                                                                            type="submit">
                                                                                            <i class="fas fa-check-circle"
                                                                                                v-on:click="newBizzKey()"></i>
                                                                                        </button>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr v-if="name == bizzinfos.owner">
                                                                                    <td>Business verkaufen</td>
                                                                                    <td>Aktueller Businesspreis:
                                                                                        {{Math.floor(bizzinfos.price)}}$
                                                                                    </td>
                                                                                    <td> <button
                                                                                            class="btn btn-danger btn-sm"
                                                                                            type="submit">
                                                                                            <i class="fas fa-check-circle"
                                                                                                v-on:click="sellBizz()"></i>
                                                                                        </button>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
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
                <div class="row d-flex justify-content-center"
                    style="margin-left: 15rem !important; margin-top: 5.0vh; height:90vh; overflow-x: auto;"
                    v-if="menushowcars">
                    <div class="col-md-12">
                        <div class="col-md-12 mt-1">
                            <div class="box box-default">
                                <div class="card card-primary card-outline">
                                    <div class="card-header" style="font-family: 'Exo', sans-serif;">Fahrzeuge</div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="text-center mt-1" v-if="cars.length <= 0">
                                                <h3>Keine Fahrzeuge vorhanden!</h3>
                                            </div>
                                        </div>
                                        <div v-if="cars.length > 0">
                                            <div class="row">
                                                <div v-for="car in cars" :key="car.id"
                                                    class="card card-primary card-outline ml-2 mr-4">
                                                    <div class="card-body box-profile">
                                                        <h3 v-if="car.ownname != 'n/A'"
                                                            class="profile-username text-center mt-3">
                                                            {{car.ownname}}[{{car.id}}]
                                                        </h3>
                                                        <h3 v-else class="profile-username text-center mt-3">
                                                            {{car.vehiclename}}[{{car.id}}]
                                                        </h3>
                                                        <p v-if="car.plate.length > 3" class="text-muted text-center">
                                                            Kennzeichen:
                                                            {{car.plate}} -
                                                            <span style="color: green">Zugelassen</span>
                                                        </p>
                                                        <p v-else class="text-muted text-center">Kennzeichen: Nicht
                                                            vorhanden - <span style="color: red">Nicht zugelassen</span>
                                                        </p>
                                                        <ul class="list-group list-group-bordered mb-3">
                                                            <li class="list-group-item">
                                                                <b>Zustand</b> <a class="float-right"
                                                                    id="health">{{(parseInt(car.health.split('|')[2])/1000*100).toFixed(0)}}%</a>
                                                                <div class="progress">
                                                                    <div class="progress-bar progress-bar-error progress-bar-striped progress-bar1"
                                                                        role="progressbar" aria-valuenow="40"
                                                                        aria-valuemin="0" aria-valuemax="100"
                                                                        :style="{ 'width': parseInt(parseInt(car.health.split('|')[2])/1000*100) + '%' }">
                                                                    </div>
                                                                </div>
                                                            </li>
                                                            <li class="list-group-item">
                                                                <b>Tank </b> <a class="float-right"
                                                                    id="fuel">{{car.fuel}}l</a>
                                                            </li>
                                                            <li class="list-group-item">
                                                                <b>Öl </b> <a class="float-right"
                                                                    id="oel">{{(parseInt(car.oel/100*100)).toFixed(0)}}%</a>
                                                                <div class="progress">
                                                                    <div class="progress-bar progress-bar-warning progress-bar-striped progress-bar3"
                                                                        role="progressbar" aria-valuenow="40"
                                                                        aria-valuemin="0" aria-valuemax="100"
                                                                        :style="{ 'width': parseInt(car.oel/100*100) + '%' }">
                                                                    </div>
                                                                </div>
                                                            </li>
                                                            <li class="list-group-item">
                                                                <b>Batterie</b> <a class="float-right"
                                                                    id="battery">{{(parseInt(car.battery/100*100)).toFixed(0)}}%</a>
                                                                <div class="progress">
                                                                    <div class="progress-bar progress-bar-warning progress-bar-striped progress-bar2"
                                                                        role="progressbar" aria-valuenow="40"
                                                                        aria-valuemin="0" aria-valuemax="100"
                                                                        :style="{ 'width': parseInt(car.battery/100*100) + '%' }">
                                                                    </div>
                                                                </div>
                                                            </li>
                                                            <li class="list-group-item">
                                                                <b v-if="car.status==0">Status </b> <a
                                                                    v-if="car.status==0" style="color:green"
                                                                    class="float-right">Offen</a>
                                                                <b v-if="car.status==1">Status </b> <a
                                                                    v-if="car.status==1" style="color:red"
                                                                    class="float-right">Verschlossen</a>
                                                            </li>
                                                            <li class="list-group-item">
                                                                <b v-if="car.engine==0">Motor </b> <a
                                                                    v-if="car.engine==0" class="float-right">Aus</a>
                                                                <b v-if="car.engine==1">Motor </b> <a
                                                                    v-if="car.engine==1" class="float-right">An</a>
                                                            </li>
                                                            <li class="list-group-item">
                                                                <b>Kilometer</b> <a
                                                                    class="float-right">{{car.kilometre.toFixed(2)}}
                                                                    KM</a>
                                                            </li>
                                                            <li class="list-group-item" v-if="car.tuev != -50">
                                                                <b
                                                                    v-if="car.tuev > 5 && car.tuev >= (Date.now() / 1000)">TÜV
                                                                </b> <a
                                                                    v-if="car.tuev > 5 && car.tuev >= (Date.now() / 1000)"
                                                                    class="float-right"
                                                                    style="color:green">{{timeConverter(car.tuev)}}</a>
                                                                <a v-if="car.tuev > 5 && car.tuev < (Date.now() / 1000)"
                                                                    class="float-right" style="color:red">Kein TÜV</a>
                                                                <a v-if="car.tuev == 2" class="float-right"
                                                                    style="color:green">Ja</a>
                                                                <b
                                                                    v-if="(car.tuev != 2 && car.tuev <= 5) || car.tuev < (Date.now() / 1000)">TÜV</b>
                                                                <a v-if="car.tuev != 2 && car.tuev <= 5"
                                                                    class="float-right" style="color:red">Nein</a>
                                                            </li>
                                                            <li class="list-group-item" v-if="car.garage != 'admin'">
                                                                <b>Ind. Name</b> <a
                                                                    class="float-right text-center"><input
                                                                        style="width: 45%" type="text"
                                                                        class="float-right form-control text-center"
                                                                        :placeholder="car.ownname" v-model="car.ownname"
                                                                        maxlength="60" autocomplete="off"></a>
                                                            </li>
                                                            <li class="list-group-item"
                                                                v-if="!car.owner.includes('character') && car.garage != 'admin'">
                                                                <b>Ab Rang</b> <a class="float-right text-center"><input
                                                                        style="width: 20%" type="text"
                                                                        class="float-right form-control text-center"
                                                                        :placeholder="car.rang" v-model="car.rang"
                                                                        maxlength="2" autocomplete="off"></a>
                                                            </li>
                                                            <button v-if="car.garage != 'admin'"
                                                                class="btn btn-primary btn-sm mt-2" type="submit"
                                                                v-on:click="setCarName(car.id,car.ownname,car.owner)">Ind.
                                                                Name
                                                                setzen</button>
                                                            <button
                                                                v-if="!car.owner.includes('character') && car.garage != 'admin'"
                                                                class="btn btn-primary btn-sm mt-2" type="submit"
                                                                v-on:click="setCarRang(car.id,car.rang,car.owner)">Rang
                                                                setzen</button>
                                                            <button v-if="car.battery > 0 && car.garage != 'admin'"
                                                                class="btn btn-primary btn-sm mt-2" type="submit"
                                                                v-on:click="gpsCar(car.position, car.id)">GPS
                                                                Ortung</button>
                                                            <button v-if="car.garage == 'admin'" disabled
                                                                class="btn btn-danger btn-sm mt-2"
                                                                type="submit">Gesperrt</button>
                                                        </ul>
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
                <div class="row d-flex justify-content-center"
                    style="margin-left: 15rem !important; margin-top: 5.0vh; height:90vh; overflow-x: auto;"
                    v-if="menushowtticket == 1">
                    <div class="col-md-12">
                        <div class="col-md-12 mt-1">
                            <div class="box box-default">
                                <div class="card card-primary card-outline">
                                    <div class="card-header" style="font-family: 'Exo', sans-serif;">Ticket erstellen
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>Titel deines Tickets</label>
                                                    <input class="form-control title" placeholder="Titel" name="title"
                                                        id="title" type="text" v-model="ticketTitel">
                                                </div>
                                                <div class="form-group">
                                                    <label>Priorität</label>
                                                    <div
                                                        style="display: flex; justify-content: center; align-items: center">
                                                        <button v-if="ticketPrio == 'low'"
                                                            class="btn btn-block btn-primary btn-sm mt-2 mr-3"
                                                            type="submit" style="width: 25%" disabled>Niedrig</button>
                                                        <button v-if="ticketPrio != 'low'"
                                                            class="btn btn-block btn-primary btn-sm mt-2 mr-3"
                                                            type="submit" style="width: 25%"
                                                            @click="changePrio('low')">Niedrig</button>
                                                        <button v-if="ticketPrio == 'middle'"
                                                            class="btn btn-block btn-warning btn-sm mr-3" type="submit"
                                                            style="width: 25%" disabled>Mittel</button>
                                                        <button v-if="ticketPrio != 'middle'"
                                                            class="btn btn-block btn-warning btn-sm mr-3" type="submit"
                                                            style="width: 25%"
                                                            @click="changePrio('middle')">Mittel</button>
                                                        <button v-if="ticketPrio == 'high'"
                                                            class="btn btn-block btn-danger btn-sm mr-3" type="submit"
                                                            style="width: 25%" disabled>Hoch</button>
                                                        <button v-if="ticketPrio != 'high'"
                                                            class="btn btn-block btn-danger btn-sm mr-3" type="submit"
                                                            style="width: 25%" @click="changePrio('high')">Hoch</button>
                                                    </div>
                                                    <div class="form-group mt-3">
                                                        <label>Beschreibung</label>
                                                        <textarea id="summernote" name="summernote"
                                                            class="summernote form-control"
                                                            placeholder="Inhalt deines Tickets"
                                                            maxlength="3500"></textarea>
                                                    </div>
                                                    <button type="submit" class="btn btn-primary" style="width:100%;"
                                                        @click="createTicket()">Ticket
                                                        erstellen</button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row d-flex justify-content-center"
                    style="margin-left: 15rem !important; margin-top: 5.0vh; height:90vh; overflow-x: auto;"
                    v-if="menushowtticket == 2">
                    <div class="col-md-12">
                        <div class="col-md-12 mt-1">
                            <div class="box box-default">
                                <div class="card card-primary card-outline">
                                    <div class="card-header" style="font-family: 'Exo', sans-serif;">Meine Tickets</div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div v-if="tickets.length > 0">
                                                    <div class="input-group mb-3">
                                                        <div class="input-group-prepend">
                                                            <span class="input-group-text"><i
                                                                    class="fas fa-search"></i></span>
                                                        </div>
                                                        <input type="text" class="form-control"
                                                            v-bind:value="searchelement"
                                                            v-on:input="searchelement = $event.target.value"
                                                            placeholder="Suche" maxlength="55">
                                                    </div>
                                                </div>
                                                <div v-if="tickets.length > 0">
                                                    <table id="logtable" name="logtable" class="table table-bordered"
                                                        style="width:100%">
                                                        <thead class="table-primary">
                                                            <tr id="tablehead" class="tablehead">
                                                                <th>ID</th>
                                                                <th>Titel</th>
                                                                <th>Prio</th>
                                                                <th>Ersteller</th>
                                                                <th>Bearbeiter</th>
                                                                <th>Status</th>
                                                                <th>Erstellungsdatum</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr v-for="ticket in filteredList2" :key="ticket.id">
                                                                <td>{{ticket.id}}</td>
                                                                <td><a href="#" style="color:rgba(255, 255, 255, 0.685)"
                                                                        @click="showTicket(ticket)">{{ticket.title}}</a>
                                                                </td>
                                                                <td v-if="ticket.prio == 'low'"><span
                                                                        class="badge bg-primary ml-1">Niedrig</span>
                                                                </td>
                                                                <td v-if="ticket.prio == 'middle'"><span
                                                                        class="badge bg-warning ml-1">Mittel</span>
                                                                </td>
                                                                <td v-if="ticket.prio == 'high'"><span
                                                                        class="badge bg-danger ml-1">Hoch</span></td>
                                                                <td>{{ticket.user}}</td>
                                                                <td>{{ticket.admin}}</td>
                                                                <td><span
                                                                        class="badge bg-primary">{{getTicketStatus(ticket.status)}}</span>
                                                                </td>
                                                                <td>{{timeConverter(ticket.timestamp)}}</td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                                <div v-else>
                                                    <div class="text-center">
                                                        <h3>Keine Tickets vorhanden!</h3>
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
                <div class="row d-flex justify-content-center"
                    style="margin-left: 15rem !important; margin-top: 5.0vh; height:90vh; overflow-x: auto;"
                    v-if="menushowtticket == 3">
                    <div class="col-md-12">
                        <div class="col-md-12 mt-1">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="box box-default">
                                        <div class="card card-primary card-outline">
                                            <div class="card-header">Ticket: {{selectedTicket.title}}
                                                ({{selectedTicket.id}}) |
                                                Ersteller:
                                                {{selectedTicket.user}}
                                                <button v-if="selectedTicket.status == 1"
                                                    class="float-right btn btn-primary btn-sm" type="submit"
                                                    v-on:click="updateTicket(0)">Ticket aktualisieren</button>
                                            </div>
                                            <div class="card-body">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div
                                                            style="display: flex; justify-content: center; align-items: center">
                                                            <div class="row justify-content-center">
                                                                <div
                                                                    v-if="selectedTicket.admin != 'Warte auf Bearbeitung'">
                                                                    <span
                                                                        class="badge bg-primary float-right">{{getTicketStatus(selectedTicket.status)}}
                                                                        | Bearbeiter: {{selectedTicket.admin}}</span>
                                                                </div>
                                                                <div v-else>
                                                                    <span
                                                                        class="badge bg-primary float-right">{{getTicketStatus(selectedTicket.status)}}</span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="card card-primary card-outline mt-4" autofocus>
                                                            <div class="card-header">
                                                                <span class="float-left">{{selectedTicket.user}}</span>
                                                                <span
                                                                    class="float-right">{{timeConverter(selectedTicket.timestamp)}}</span>
                                                            </div>
                                                            <div class="card-body cardScroll" id="cardScroll"
                                                                name="cardScroll" style="margin-top:0.5vw">
                                                                <span v-html="selectedTicket.text"></span>
                                                            </div>
                                                        </div>
                                                        <div
                                                            style="display: flex; justify-content: center; align-items: center">
                                                            <div class="row">
                                                                <div class="card-body" v-for="answer in ticketAnswers"
                                                                    :key="answer.id" style="width: 70vw">
                                                                    <div
                                                                        :class="[(ticketAnswers.user == selectedTicket.user) ? 'card card-primary card-outline':'card card-warning card-outline']">
                                                                        <div class="card-header">
                                                                            <span
                                                                                class="float-left">{{answer.user}}</span>
                                                                            <span
                                                                                class="float-right">{{timeConverter(answer.timestamp)}}</span>
                                                                        </div>
                                                                        <div class="card-body" style="margin-top:0.5vw">
                                                                            <span v-html="answer.text"></span>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div v-if="selectedTicket.status != 9">
                                                            <hr />
                                                            <div class="card card-success card-outline mt-4">
                                                                <div class="card-header">
                                                                    Antworten
                                                                </div>
                                                                <div class="card-body">
                                                                    <button
                                                                        v-if="selectedTicket.user == oocName && selectedTicket.status <= 1"
                                                                        type="submit"
                                                                        class="btn btn-block btn-success btn-sm mb-3"
                                                                        @click="finishTicket()">Ticket
                                                                        als
                                                                        'erledigt' markieren</button>
                                                                    <div class="col-md-12 mb-3"
                                                                        v-if="admin > 0 && adminduty">
                                                                        <div
                                                                            style="display: flex; justify-content: center; align-items: center">
                                                                            <button v-if="selectedTicket.status == 1"
                                                                                class="btn btn-block btn-primary btn-sm mt-2 mr-3"
                                                                                type="submit" style="width: 15%"
                                                                                disabled>In
                                                                                Bearbeitung</button>
                                                                            <button v-if="selectedTicket.status != 1"
                                                                                class="btn btn-block btn-primary btn-sm mt-2 mr-3"
                                                                                type="submit" style="width: 15%"
                                                                                @click="changeStatus(1)">In
                                                                                Bearbeitung</button>
                                                                            <button v-if="selectedTicket.status == 2"
                                                                                class="btn btn-block btn-primary btn-sm mt-2 mr-3"
                                                                                type="submit" style="width: 15%"
                                                                                disabled>Geschlossen</button>
                                                                            <button v-if="selectedTicket.status != 2"
                                                                                class="btn btn-block btn-primary btn-sm mt-2 mr-3"
                                                                                type="submit" style="width: 15%"
                                                                                @click="changeStatus(2)">Geschlossen</button>
                                                                            <button v-if="selectedTicket.status == 9"
                                                                                class="btn btn-block btn-primary btn-sm mt-2 mr-3"
                                                                                type="submit" style="width: 15%"
                                                                                disabled>Archiviert</button>
                                                                            <button v-if="selectedTicket.status != 9"
                                                                                class="btn btn-block btn-primary btn-sm mt-2 mr-3"
                                                                                type="submit" style="width: 15%"
                                                                                @click="changeStatus(9)">Archiviert</button>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group row mt-3"
                                                                        v-if="selectedTicket.status <= 1 && admin > 0 && adminduty">
                                                                        <div class="input-group input-group">
                                                                            <input type="text" class="form-control"
                                                                                placeholder="Spieler hinzufügen"
                                                                                id="name" name="name" maxlength="35"
                                                                                autocomplete="off"
                                                                                v-model="ticketTempText">
                                                                            <span class="input-group-append">
                                                                                <button type="submit"
                                                                                    class="btn btn-primary ml-2"
                                                                                    @click="addUser()">Hinzufügen</button>
                                                                            </span>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group row"
                                                                        v-if="selectedTicket.status <= 1 && admin > 0 && adminduty">
                                                                        <div class="input-group input-group">
                                                                            <input type="text" class="form-control"
                                                                                placeholder="Bearbeiter setzen"
                                                                                id="name" name="name" maxlength="35"
                                                                                autocomplete="off"
                                                                                v-model="ticketTempText2">
                                                                            <span class="input-group-append">
                                                                                <button type="submit"
                                                                                    class="btn btn-primary ml-2"
                                                                                    @click="setEditor()">Setzen</button>
                                                                            </span>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group mt-3"
                                                                        v-if="selectedTicket.status == 1">
                                                                        <textarea id="summernote" name="summernote"
                                                                            class="summernote form-control"
                                                                            placeholder="Inhalt deines Tickets"
                                                                            maxlength="3500"></textarea>
                                                                    </div>
                                                                    <button type="submit"
                                                                        class="btn btn-primary mb-1 mt-4"
                                                                        style="width:100%;"
                                                                        v-if="selectedTicket.status == 1"
                                                                        @click="answerTicket()">Antworten</button>
                                                                    <div class="text-center mt-1"
                                                                        v-if="selectedTicket.status == 2">
                                                                        <h3>Das Ticket befindet sich nicht(mehr) in
                                                                            Bearbeitung!</h3>
                                                                    </div>
                                                                    <h3 v-if="selectedTicket.status > 2"
                                                                        class="text-center">Auf dieses
                                                                        Ticket
                                                                        kann nichtmehr geantwortet
                                                                        werden!
                                                                    </h3>
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
                <div class="row d-flex justify-content-center"
                    style="margin-left: 15rem !important; margin-top: 5.0vh; height:90vh; overflow-x: auto;"
                    v-if="menushowfaction">
                    <div class="col-md-12">
                        <div class="col-md-12 mt-1">
                            <div class="box box-default">
                                <div class="card card-primary card-outline">
                                    <div class="card-header" style="font-family: 'Exo', sans-serif;">Fraktionsinfo</div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card card-primary">
                                                    <div class="card-header">
                                                        <h3 class="card-title">Fraktionsinfo</h3>
                                                    </div>
                                                    <div class="card-body box-profile">
                                                        <h3 class="profile-username text-center">
                                                            {{factioninfo.name}}
                                                        </h3>
                                                        <p class="text-muted text-center">
                                                            Gegründet am: {{timeConverter(factioninfo.created)}}
                                                        </p>
                                                        <ul class="list-group list-group-bordered">
                                                            <li class="list-group-item ">
                                                                <b>Leitung</b> <a
                                                                    class="float-right">{{factionleader}}</a>
                                                            </li>
                                                            <li class="list-group-item ">
                                                                <b>Aktuelle Mitglieder</b> <a
                                                                    class="float-right">{{factionmemberscount}}</a>
                                                            </li>
                                                            <li class="list-group-item ">
                                                                <b>Mitglieder seid Gründung</b> <a
                                                                    class="float-right">{{factioninfo.members}}</a>
                                                            </li>
                                                            <li class="list-group-item ">
                                                                <b>Fahrzeuge</b> <a
                                                                    class="float-right">{{factionvehiclecount}}</a>
                                                            </li>
                                                            <li class="list-group-item"
                                                                v-if="faction == 1 || faction == 2 || faction == 3 || faction == 4">
                                                                <b>Fraktionshilfe</b> <a class="float-right"><button
                                                                        class="btn btn-primary btn-sm" type="submit"
                                                                        v-on:click="startFactionHelp()">Einsehen</button></a>
                                                            </li>
                                                            <li class="list-group-item"
                                                                v-if="faction == 1 || faction == 2 || faction == 3">
                                                                <b>Leitstelle</b> <a
                                                                    class="float-right">{{factioninfo.numbername}}
                                                                    <button class="btn btn-success btn-sm" type="submit"
                                                                        v-on:click="factionPhone()">Übernehmen</button></a>
                                                            </li>
                                                            <li class="list-group-item " v-if="factionrang >= 10">
                                                                <b>Neue Mitglieder einladen</b> <a
                                                                    class="float-right"><button
                                                                        class="btn btn-primary btn-sm" type="submit"
                                                                        v-on:click="factionInvite()">
                                                                        Einladen</button></a>
                                                            </li>
                                                            <li class="list-group-item"
                                                                v-if="myid != factioninfo.leader">
                                                                <b>Fraktion verlassen</b> <a class="float-right"><button
                                                                        class="btn btn-danger btn-sm" type="submit"
                                                                        v-on:click="factionLeave()">
                                                                        <i class="fas fa-sign-out-alt"></i>
                                                                    </button></a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="table-responsive-sm">
                                                    <table class="table table-bordered">
                                                        <thead class="table-primary">
                                                            <tr>
                                                                <th>Name</th>
                                                                <th v-if="windowWidth >= 1280">Mitglied seit</th>
                                                                <th>Dienstzeit (Woche)</th>
                                                                <th>Rang</th>
                                                                <th>Verwaltung</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr v-for="fmember in factionmembers" :key="fmember.id">
                                                                <td>{{ fmember.name }} <span v-if="fmember.online == 1"
                                                                        class="badge badge-success">Online</span><span
                                                                        v-else
                                                                        class="badge badge-danger">Offline</span><span
                                                                        v-if="fmember.swat > 0"
                                                                        class="badge badge-primary ml-1">SWAT</span>
                                                                </td>
                                                                <td v-if="windowWidth >= 1280">
                                                                    {{ timeConverter(fmember.membersince) }}
                                                                </td>
                                                                <td><span
                                                                        class="badge badge-info">{{ fmember.dutytime }}h</span>
                                                                </td>
                                                                <td><span
                                                                        class="badge badge-primary">{{ getRang2(fmember.rang) }}
                                                                        ({{fmember.rang}})</span>
                                                                </td>
                                                                <td>
                                                                    <div v-if="((factionrang >= 10 && factionrang > fmember.rang && myid
                                                     != factioninfo.leader) || myid == factioninfo.leader)">
                                                                        <div class="row">
                                                                            <button v-if="factioninfo.id == 1"
                                                                                class="btn btn-info btn-sm mr-1"
                                                                                type="submit"
                                                                                @click="factionSwat(fmember.charid)">
                                                                                <i class="fa-solid fa-gun"></i>
                                                                            </button>
                                                                            <button class="btn btn-info btn-sm mr-1"
                                                                                type="submit"
                                                                                @click="factionUprank(fmember.charid)">
                                                                                <i class="fas fa-plus"></i>
                                                                            </button>
                                                                            <button class="btn btn-info btn-sm mr-1"
                                                                                type="submit"
                                                                                @click="factionDownrank(fmember.charid)">
                                                                                <i class="fas fa-minus"></i>
                                                                            </button>
                                                                            <button class="btn btn-info btn-sm mr-1"
                                                                                type="submit"
                                                                                @click="factionKick(fmember.charid)">
                                                                                <i class="fas fa-times"></i>
                                                                            </button>
                                                                        </div>
                                                                    </div>
                                                                    <div v-else>
                                                                        Keine Berechtigung
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row d-flex justify-content-center"
                    style="margin-left: 15rem !important; margin-top: 5.0vh; height:90vh; overflow-x: auto;"
                    v-if="menushowfactionsettings">
                    <div class="col-md-12">
                        <div class="col-md-12 mt-1">
                            <div class="box box-default">
                                <div class="card card-primary card-outline">
                                    <div class="card-header" style="font-family: 'Exo', sans-serif;">
                                        Fraktionseinstellungen</div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card card-primary">
                                                    <div class="card-header">
                                                        <h3 class="card-title">Fraktionseinstellungen</h3>
                                                    </div>
                                                    <div class="card-body">
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="card card-primary card-outline">
                                                                    <div class="col-md-12">
                                                                        <label class="mt-2">Rangnamen:</label>
                                                                        <table id="logtable"
                                                                            class="table table-bordered mt-2"
                                                                            style="width:100%">
                                                                            <thead class="table-primary">
                                                                                <tr>
                                                                                    <th>Aktueller Rangname</th>
                                                                                    <th>Neuer Rangname</th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td>{{factionrangs.rang1}}</td>
                                                                                    <td><input type="text"
                                                                                            class="form-control"
                                                                                            v-model="factionrangs.rang1"
                                                                                            maxlength="50"
                                                                                            autocomplete="off"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>{{factionrangs.rang2}}</td>
                                                                                    <td><input type="text"
                                                                                            class="form-control"
                                                                                            v-model="factionrangs.rang2"
                                                                                            maxlength="50"
                                                                                            autocomplete="off"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>{{factionrangs.rang3}}</td>
                                                                                    <td><input type="text"
                                                                                            class="form-control"
                                                                                            v-model="factionrangs.rang3"
                                                                                            maxlength="50"
                                                                                            autocomplete="off"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>{{factionrangs.rang4}}</td>
                                                                                    <td><input type="text"
                                                                                            class="form-control"
                                                                                            v-model="factionrangs.rang4"
                                                                                            maxlength="50"
                                                                                            autocomplete="off"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>{{factionrangs.rang5}}</td>
                                                                                    <td><input type="text"
                                                                                            class="form-control"
                                                                                            v-model="factionrangs.rang5"
                                                                                            maxlength="50"
                                                                                            autocomplete="off"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>{{factionrangs.rang6}}</td>
                                                                                    <td><input type="text"
                                                                                            class="form-control"
                                                                                            v-model="factionrangs.rang6"
                                                                                            maxlength="50"
                                                                                            autocomplete="off"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>{{factionrangs.rang7}}</td>
                                                                                    <td><input type="text"
                                                                                            class="form-control"
                                                                                            v-model="factionrangs.rang7"
                                                                                            maxlength="50"
                                                                                            autocomplete="off"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>{{factionrangs.rang8}}</td>
                                                                                    <td><input type="text"
                                                                                            class="form-control"
                                                                                            v-model="factionrangs.rang8"
                                                                                            maxlength="50"
                                                                                            autocomplete="off"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>{{factionrangs.rang9}}</td>
                                                                                    <td><input type="text"
                                                                                            class="form-control"
                                                                                            v-model="factionrangs.rang9"
                                                                                            maxlength="50"
                                                                                            autocomplete="off"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>{{factionrangs.rang10}}</td>
                                                                                    <td><input type="text"
                                                                                            class="form-control"
                                                                                            v-model="factionrangs.rang10"
                                                                                            maxlength="50"
                                                                                            autocomplete="off"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>{{factionrangs.rang11}}</td>
                                                                                    <td><input type="text"
                                                                                            class="form-control"
                                                                                            v-model="factionrangs.rang11"
                                                                                            maxlength="50"
                                                                                            autocomplete="off"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>{{factionrangs.rang12}}</td>
                                                                                    <td><input type="text"
                                                                                            class="form-control"
                                                                                            v-model="factionrangs.rang12"
                                                                                            maxlength="50"
                                                                                            autocomplete="off"></td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                        <button @click="factionRangUpdate()"
                                                                            class="btn btn-block btn-primary mb-2 mt-3">Rangnamen
                                                                            aktualisieren</button>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="card card-primary card-outline">
                                                                    <div class="col-md-12">
                                                                        <label class="mt-2">Lohneinstellungen: (Verf.
                                                                            Lohnbudget:
                                                                            {{factionbudget}}$):</label>
                                                                        <table id="logtable"
                                                                            class="table table-bordered mt-2"
                                                                            style="width:100%">
                                                                            <thead class="table-primary">
                                                                                <tr>
                                                                                    <th>Rangname</th>
                                                                                    <th>Lohn</th>
                                                                                    <th>Neuer Lohn</th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td>{{factionrangs.rang1}}</td>
                                                                                    <td>{{factionsalarys.rang1}}$</td>
                                                                                    <td><input type="text"
                                                                                            class="form-control"
                                                                                            v-model="factionsalarys.rang1"
                                                                                            maxlength="6"
                                                                                            autocomplete="off"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>{{factionrangs.rang2}}</td>
                                                                                    <td>{{factionsalarys.rang2}}$</td>
                                                                                    <td><input type="text"
                                                                                            class="form-control"
                                                                                            v-model="factionsalarys.rang2"
                                                                                            maxlength="6"
                                                                                            autocomplete="off"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>{{factionrangs.rang3}}</td>
                                                                                    <td>{{factionsalarys.rang3}}$</td>
                                                                                    <td><input type="text"
                                                                                            class="form-control"
                                                                                            v-model="factionsalarys.rang3"
                                                                                            maxlength="6"
                                                                                            autocomplete="off"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>{{factionrangs.rang4}}</td>
                                                                                    <td>{{factionsalarys.rang4}}$</td>
                                                                                    <td><input type="text"
                                                                                            class="form-control"
                                                                                            v-model="factionsalarys.rang4"
                                                                                            maxlength="6"
                                                                                            autocomplete="off"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>{{factionrangs.rang5}}</td>
                                                                                    <td>{{factionsalarys.rang5}}$</td>
                                                                                    <td><input type="text"
                                                                                            class="form-control"
                                                                                            v-model="factionsalarys.rang5"
                                                                                            maxlength="6"
                                                                                            autocomplete="off"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>{{factionrangs.rang6}}</td>
                                                                                    <td>{{factionsalarys.rang6}}$</td>
                                                                                    <td><input type="text"
                                                                                            class="form-control"
                                                                                            v-model="factionsalarys.rang6"
                                                                                            maxlength="6"
                                                                                            autocomplete="off"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>{{factionrangs.rang7}}</td>
                                                                                    <td>{{factionsalarys.rang7}}$</td>
                                                                                    <td><input type="text"
                                                                                            class="form-control"
                                                                                            v-model="factionsalarys.rang7"
                                                                                            maxlength="6"
                                                                                            autocomplete="off"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>{{factionrangs.rang8}}</td>
                                                                                    <td>{{factionsalarys.rang8}}$</td>
                                                                                    <td><input type="text"
                                                                                            class="form-control"
                                                                                            v-model="factionsalarys.rang8"
                                                                                            maxlength="6"
                                                                                            autocomplete="off"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>{{factionrangs.rang9}}</td>
                                                                                    <td>{{factionsalarys.rang9}}$</td>
                                                                                    <td><input type="text"
                                                                                            class="form-control"
                                                                                            v-model="factionsalarys.rang9"
                                                                                            maxlength="6"
                                                                                            autocomplete="off"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>{{factionrangs.rang10}}</td>
                                                                                    <td>{{factionsalarys.rang10}}$</td>
                                                                                    <td><input type="text"
                                                                                            class="form-control"
                                                                                            v-model="factionsalarys.rang10"
                                                                                            maxlength="6"
                                                                                            autocomplete="off"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>{{factionrangs.rang11}}</td>
                                                                                    <td>{{factionsalarys.rang11}}$</td>
                                                                                    <td><input type="text"
                                                                                            class="form-control"
                                                                                            v-model="factionsalarys.rang11"
                                                                                            maxlength="6"
                                                                                            autocomplete="off"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>{{factionrangs.rang12}}</td>
                                                                                    <td>{{factionsalarys.rang12}}$</td>
                                                                                    <td><input type="text"
                                                                                            class="form-control"
                                                                                            v-model="factionsalarys.rang12"
                                                                                            maxlength="6"
                                                                                            autocomplete="off"></td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                        <button @click="factionSalaryUpdate()"
                                                                            class="btn btn-block btn-primary mb-2 mt-3">Lohneinstellungen
                                                                            aktualisieren</button>
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
                <div class="row d-flex justify-content-center"
                    style="margin-left: 15rem !important; margin-top: 5.0vh; height:90vh; overflow-x: auto;"
                    v-if="menushowgroup">
                    <div class="col-md-12">
                        <div class="col-md-12 mt-1">
                            <div class="box box-default">
                                <div class="card card-primary card-outline">
                                    <div class="card-header" style="font-family: 'Exo', sans-serif;">Gruppierungsinfo
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card card-primary">
                                                    <div class="card-header">
                                                        <h3 class="card-title">Gruppierungsinfo</h3>
                                                    </div>
                                                    <div class="card-body box-profile">
                                                        <h3 class="profile-username text-center">
                                                            {{groupinfo.name}}
                                                        </h3>
                                                        <p class="text-muted text-center">
                                                            Gegründet am: {{timeConverter(groupinfo.created)}}
                                                        </p>
                                                        <ul class="list-group list-group-bordered">
                                                            <li class="list-group-item ">
                                                                <b>Leitung</b> <a
                                                                    class="float-right">{{groupleader}}</a>
                                                            </li>
                                                            <li class="list-group-item ">
                                                                <b>Aktuelle Mitglieder</b> <a
                                                                    class="float-right">{{groupmemberscount}}</a>
                                                            </li>
                                                            <li class="list-group-item ">
                                                                <b>Mitglieder seid Gründung</b> <a
                                                                    class="float-right">{{groupinfo.members}}</a>
                                                            </li>
                                                            <li class="list-group-item ">
                                                                <b>Fahrzeuge</b> <a
                                                                    class="float-right">{{groupvehiclescount}}</a>
                                                            </li>
                                                            <li class="list-group-item"
                                                                v-if="groupinfo.banknumber != 'n/A'">
                                                                <b>Konto</b> <a
                                                                    class="float-right">{{groupinfo.banknumber}}</a>
                                                            </li>
                                                            <li class="list-group-item">
                                                                <b>Provision</b> <a
                                                                    class="float-right">{{groupinfo.provision}}%</a>
                                                            </li>
                                                            <li class="list-group-item ">
                                                                <b>Status</b> <a class="float-right">{{groupstatus}}</a>
                                                            </li>
                                                            <li class="list-group-item" v-if="groupstatus == 'Firma'">
                                                                <b>Telefonservice</b> <a
                                                                    class="float-right">{{groupinfo.numbername}}
                                                                    <button class="btn btn-success btn-sm" type="submit"
                                                                        v-on:click="groupPhone()">Übernehmen</button></a>
                                                            </li>
                                                            <li class="list-group-item ">
                                                                <b>Lizenzen</b> <span
                                                                    v-if="groupinfo.licenses.split('|')[0] == 1"></span>
                                                                <a class="float-right"><i @click="showLicHelp()"
                                                                        style="color:green"
                                                                        v-if="groupinfo.licenses.split('|')[0] == 1"
                                                                        class="fas fa-truck mr-1"></i><i
                                                                        v-if="groupinfo.licenses.split('|')[0] != 1"
                                                                        style="color:red"
                                                                        class="fas fa-truck mr-1"></i></a>
                                                                <span
                                                                    v-if="groupinfo.licenses.split('|')[1] == 1"></span>
                                                                <a class="float-right"><i @click="showLicHelp()"
                                                                        style="color:green"
                                                                        v-if="groupinfo.licenses.split('|')[1] == 1"
                                                                        class="fa-solid fa-wrench mr-1"></i><i
                                                                        v-if="groupinfo.licenses.split('|')[1] != 1"
                                                                        style="color:red"
                                                                        class="fa-solid fa-wrench mr-1"></i></a>
                                                                <span
                                                                    v-if="groupinfo.licenses.split('|')[2] == 1"></span>
                                                                <a class="float-right"><i @click="showLicHelp()"
                                                                        style="color:green"
                                                                        v-if="groupinfo.licenses.split('|')[2] == 1"
                                                                        class="fa-solid fa-car mr-1"></i><i
                                                                        v-if="groupinfo.licenses.split('|')[2] != 1"
                                                                        style="color:red"
                                                                        class="fa-solid fa-car mr-1"></i></a>
                                                                <span
                                                                    v-if="groupinfo.licenses.split('|')[3] == 1"></span>
                                                                <a class="float-right"><i @click="showLicHelp()"
                                                                        style="color:green"
                                                                        v-if="groupinfo.licenses.split('|')[3] == 1"
                                                                        class="fa-solid fa-bus mr-1"></i><i
                                                                        v-if="groupinfo.licenses.split('|')[3] != 1"
                                                                        style="color:red"
                                                                        class="fa-solid fa-bus mr-1"></i></a>
                                                                <span
                                                                    v-if="groupinfo.licenses.split('|')[4] == 1"></span>
                                                                <a class="float-right"><i @click="showLicHelp()"
                                                                        style="color:green"
                                                                        v-if="groupinfo.licenses.split('|')[4] == 1"
                                                                        class="fa-solid fa-file-shield mr-1"></i><i
                                                                        v-if="groupinfo.licenses.split('|')[4] != 1"
                                                                        style="color:red"
                                                                        class="fa-solid fa-file-shield mr-1"></i></a>
                                                            </li>
                                                            <li class="list-group-item " v-if="grouprang >= 10">
                                                                <b>Neue Mitglieder einladen</b> <a
                                                                    class="float-right"><button
                                                                        class="btn btn-primary btn-sm" type="submit"
                                                                        v-on:click="groupInvite()">
                                                                        Einladen</button></a>
                                                            </li>
                                                            <li class="list-group-item" v-if="myid != groupinfo.leader">
                                                                <b>Gruppierung verlassen</b> <a
                                                                    class="float-right"><button
                                                                        class="btn btn-danger btn-sm" type="submit"
                                                                        v-on:click="groupLeave()">
                                                                        <i class="fas fa-sign-out-alt"></i>
                                                                    </button></a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="table-responsive-sm">
                                                    <table class="table table-bordered">
                                                        <thead class="table-primary">
                                                            <tr>
                                                                <th>Name</th>
                                                                <th v-if="windowWidth >= 1280">Mitglied seit</th>
                                                                <th>Onlinezeit</th>
                                                                <th>Rang</th>
                                                                <th>Lohn</th>
                                                                <th>Verwaltung</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr v-for="gmember in groupmembers" :key="gmember.id">
                                                                <td>{{ gmember.name }} <span v-if="gmember.online == 1"
                                                                        class="badge badge-success">Online</span><span
                                                                        v-else class="badge badge-danger">Offline</span>
                                                                </td>
                                                                <td v-if="windowWidth >= 1280">
                                                                    {{ timeConverter(gmember.since) }}</td>
                                                                <td><span
                                                                        class="badge badge-info">{{ gmember.duty_time }}h</span>
                                                                </td>
                                                                <td><span
                                                                        class="badge badge-primary">{{ getRang(gmember.rang) }}
                                                                        ({{gmember.rang}})</span>
                                                                </td>
                                                                <td>
                                                                    <div
                                                                        v-if="((grouprang >= 10 && grouprang > gmember.rang && myid
                                                     != groupinfo.leader) || myid == groupinfo.leader || myid == gmember.charid)">
                                                                        <span
                                                                            class="badge badge-primary">{{ gmember.payday }}$
                                                                            jeden
                                                                            {{gmember.payday_day}}ten Payday</span>
                                                                    </div>
                                                                    <div v-else>
                                                                        Keine Berechtigung
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div v-if="((grouprang >= 10 && grouprang > gmember.rang && myid
                                                     != groupinfo.leader) || myid == groupinfo.leader)">
                                                                        <div class="row">
                                                                            <button class="btn btn-info btn-sm mr-1"
                                                                                type="submit"
                                                                                @click="groupUprank(gmember.charid)">
                                                                                <i class="fas fa-plus"></i>
                                                                            </button>
                                                                            <button class="btn btn-info btn-sm mr-1"
                                                                                type="submit"
                                                                                @click="groupDownrank(gmember.charid)">
                                                                                <i class="fas fa-minus"></i>
                                                                            </button>
                                                                            <button class="btn btn-info btn-sm mr-1"
                                                                                type="submit"
                                                                                @click="groupMoney(gmember.charid)">
                                                                                <i class="fas fa-money"></i>
                                                                            </button>
                                                                            <button class="btn btn-info btn-sm mr-1"
                                                                                type="submit"
                                                                                @click="groupKick(gmember.charid)">
                                                                                <i class="fas fa-times"></i>
                                                                            </button>
                                                                            <button class="btn btn-info btn-sm mr-1"
                                                                                type="submit"
                                                                                @click="groupLeader(gmember.charid)">
                                                                                <i class="fas fa-user-tie"></i>
                                                                            </button>
                                                                        </div>
                                                                    </div>
                                                                    <div v-else>
                                                                        Keine Berechtigung
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row d-flex justify-content-center"
                    style="margin-left: 15rem !important; margin-top: 5.0vh; height:90vh; overflow-x: auto;"
                    v-if="menushowgroupsettings">
                    <div class="col-md-12">
                        <div class="col-md-12 mt-1">
                            <div class="box box-default">
                                <div class="card card-primary card-outline">
                                    <div class="card-header" style="font-family: 'Exo', sans-serif;">
                                        Gruppierungseinstellungen
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card card-primary">
                                                    <div class="card-header">
                                                        <h3 class="card-title">Gruppierungseinstellungen</h3>
                                                    </div>
                                                    <div class="card-body">
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="card card-primary card-outline">
                                                                    <div class="col-md-12">
                                                                        <label class="mt-1">Provision:</label>
                                                                        <div class="input-group input-group">
                                                                            <input type="text" class="form-control"
                                                                                placeholder="Provision in Prozent"
                                                                                v-model="groupprovision" maxlength="35"
                                                                                autocomplete="off">
                                                                            <span class="input-group-append">
                                                                                <button v-on:click="groupProvision()"
                                                                                    type="submit"
                                                                                    class="btn btn-primary btn-flat ml-2"
                                                                                    style="height: 3.65vh">Setzen</button>
                                                                            </span>
                                                                        </div>
                                                                        <label class="mt-2">Rangnamen:</label>
                                                                        <table id="logtable"
                                                                            class="table table-bordered mt-2"
                                                                            style="width:100%">
                                                                            <thead class="table-primary">
                                                                                <tr>
                                                                                    <th>Aktueller Rangname</th>
                                                                                    <th>Neuer Rangname</th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td>{{grouprangs.rang1}}</td>
                                                                                    <td><input type="text"
                                                                                            class="form-control"
                                                                                            v-model="grouprangs.rang1"
                                                                                            maxlength="50"
                                                                                            autocomplete="off"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>{{grouprangs.rang2}}</td>
                                                                                    <td><input type="text"
                                                                                            class="form-control"
                                                                                            v-model="grouprangs.rang2"
                                                                                            maxlength="50"
                                                                                            autocomplete="off"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>{{grouprangs.rang3}}</td>
                                                                                    <td><input type="text"
                                                                                            class="form-control"
                                                                                            v-model="grouprangs.rang3"
                                                                                            maxlength="50"
                                                                                            autocomplete="off"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>{{grouprangs.rang4}}</td>
                                                                                    <td><input type="text"
                                                                                            class="form-control"
                                                                                            v-model="grouprangs.rang4"
                                                                                            maxlength="50"
                                                                                            autocomplete="off"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>{{grouprangs.rang5}}</td>
                                                                                    <td><input type="text"
                                                                                            class="form-control"
                                                                                            v-model="grouprangs.rang5"
                                                                                            maxlength="50"
                                                                                            autocomplete="off"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>{{grouprangs.rang6}}</td>
                                                                                    <td><input type="text"
                                                                                            class="form-control"
                                                                                            v-model="grouprangs.rang6"
                                                                                            maxlength="50"
                                                                                            autocomplete="off"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>{{grouprangs.rang7}}</td>
                                                                                    <td><input type="text"
                                                                                            class="form-control"
                                                                                            v-model="grouprangs.rang7"
                                                                                            maxlength="50"
                                                                                            autocomplete="off"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>{{grouprangs.rang8}}</td>
                                                                                    <td><input type="text"
                                                                                            class="form-control"
                                                                                            v-model="grouprangs.rang8"
                                                                                            maxlength="50"
                                                                                            autocomplete="off"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>{{grouprangs.rang9}}</td>
                                                                                    <td><input type="text"
                                                                                            class="form-control"
                                                                                            v-model="grouprangs.rang9"
                                                                                            maxlength="50"
                                                                                            autocomplete="off"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>{{grouprangs.rang10}}</td>
                                                                                    <td><input type="text"
                                                                                            class="form-control"
                                                                                            v-model="grouprangs.rang10"
                                                                                            maxlength="50"
                                                                                            autocomplete="off"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>{{grouprangs.rang11}}</td>
                                                                                    <td><input type="text"
                                                                                            class="form-control"
                                                                                            v-model="grouprangs.rang11"
                                                                                            maxlength="50"
                                                                                            autocomplete="off"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>{{grouprangs.rang12}}</td>
                                                                                    <td><input type="text"
                                                                                            class="form-control"
                                                                                            v-model="grouprangs.rang12"
                                                                                            maxlength="50"
                                                                                            autocomplete="off"></td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                        <button @click="groupRangUpdate()"
                                                                            class="btn btn-block btn-primary mb-2 mt-3">Rangnamen
                                                                            aktualisieren</button>
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
                <div class="row d-flex justify-content-center"
                    style="margin-left: 15rem !important; margin-top: 5.0vh; height:90vh; overflow-x: auto;"
                    v-if="menushowlog">
                    <div class="col-md-12">
                        <div class="col-md-12 mt-1">
                            <div class="box box-default">
                                <div class="card card-primary card-outline">
                                    <div class="card-header" style="font-family: 'Exo', sans-serif;"
                                        v-if="logname != 'Waffenkammerlog'">{{logname}}</div>
                                    <div class="card-header" style="font-family: 'Exo', sans-serif;"
                                        v-if="logname == 'Waffenkammerlog' && faction != 4">{{logname}}</div>
                                    <div class="card-header" style="font-family: 'Exo', sans-serif;"
                                        v-if="logname == 'Waffenkammerlog' && faction == 4">Staatskassenlog</div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card card-primary">
                                                    <div class="card-header">
                                                        <h3 class="card-title" v-if="logname != 'Waffenkammerlog'">
                                                            {{logname}}</h3>
                                                        <h3 class="card-title"
                                                            v-if="logname == 'Waffenkammerlog' && faction != 4">
                                                            {{logname}}</h3>
                                                        <h3 class="card-title"
                                                            v-if="logname == 'Waffenkammerlog' && faction == 4">
                                                            Staatskassenlog</h3>
                                                    </div>
                                                    <div class="card-body">
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="input-group mb-3">
                                                                    <div class="input-group-prepend">
                                                                        <span class="input-group-text"><i
                                                                                class="fas fa-search"></i></span>
                                                                    </div>
                                                                    <input type="text" class="form-control col-md-12"
                                                                        v-bind:value="searchelement"
                                                                        v-on:input="searchelement = $event.target.value"
                                                                        placeholder="Suche" maxlength="55">
                                                                </div>
                                                                <div class="card card-primary card-outline">
                                                                    <div class="col-md-12">
                                                                        <div class="table-responsive-md mt-2">
                                                                            <table id="logtable"
                                                                                class="table table-bordered"
                                                                                style="width:100%"
                                                                                v-if="logentry && logentry.length > 0">
                                                                                <thead class="table-primary">
                                                                                    <tr id="tablehead"
                                                                                        class="tablehead">
                                                                                        <th>Eintrag</th>
                                                                                        <th>Datum</th>
                                                                                    </tr>
                                                                                </thead>
                                                                                <tbody>
                                                                                    <tr v-for="log in filteredList"
                                                                                        :key="log.id">
                                                                                        <td>{{log.text}}</td>
                                                                                        <td>{{timeConverter(log.timestamp)}}
                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                            <div class="text-center mt-1"
                                                                                v-if="!logentry || logentry.length <= 0">
                                                                                <h3>Keine Logeinträge gefunden!</h3>
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
                </div>
                <div class="row d-flex justify-content-center"
                    style="margin-left: 15rem !important; margin-top: 5.0vh; height:90vh; overflow-x: auto;"
                    v-if="menushowlichelp">
                    <div class="col-md-12">
                        <div class="col-md-12 mt-1">
                            <div class="box box-default">
                                <div class="card card-primary card-outline"
                                    v-if="groupinfo.licenses.split('|')[0] == 1">
                                    <div class="card-header" style="font-family: 'Exo', sans-serif;">Speditionslizenz
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card card-primary">
                                                    <div class="card-header">
                                                        <h3 class="card-title">Tasten/Befehle & Informationen</h3>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="table-responsive-md mt-2">
                                                            <table id="logtable" name="logtable"
                                                                class="table table-bordered" style="width:100%">
                                                                <thead class="table-primary">
                                                                    <tr id="tablehead" class="tablehead">
                                                                        <th>Taste/Befehle</th>
                                                                        <th>Beschreibung</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>F3</td>
                                                                        <td>Produkte aufladen/verkaufen</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>F3</td>
                                                                        <td>Business Belieferungsübersicht</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>F4</td>
                                                                        <td>Auftragsliste</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>STRG</td>
                                                                        <td>Anhänger abkoppeln</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>/products</td>
                                                                        <td>Anzahl geladener Produkte</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>/share</td>
                                                                        <td>Auftrag teilen</td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                        <div class="table-responsive-md mt-2">
                                                            <table class="table table-bordered" style="width:100%">
                                                                <thead class="table-primary">
                                                                    <tr id="tablehead" class="tablehead">
                                                                        <th>Information</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>Die Hauptspedition ist auf der Karte mit
                                                                            einem grauen Truck
                                                                            markiert.</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Produkte können bei der Aufladestelle
                                                                            aufgeladen werden, hinter
                                                                            dem
                                                                            Speditionsgebäude.
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Alle 5 Minuten erscheint ein neuer Auftrag
                                                                            für die
                                                                            Auftragsliste.</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Speditionsfahrzeuge können beim Truck
                                                                            Autohaus erworben werden.
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Info: Die Anhängersync in RageMP ist noch
                                                                            nicht perfekt, der
                                                                            Anhänger kann
                                                                            'nachziehen'.
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="card card-primary card-outline"
                                    v-if="groupinfo.licenses.split('|')[1] == 1">
                                    <div class="card-header" style="font-family: 'Exo', sans-serif;">Tuninglizenz</div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card card-primary">
                                                    <div class="card-header">
                                                        <h3 class="card-title">Tasten/Befehle & Informationen</h3>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="table-responsive-md mt-2">
                                                            <table id="logtable" name="logtable"
                                                                class="table table-bordered" style="width:100%">
                                                                <thead class="table-primary">
                                                                    <tr id="tablehead" class="tablehead">
                                                                        <th>Taste/Befehle</th>
                                                                        <th>Beschreibung</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>F8</td>
                                                                        <td>Tuningmenü öffnen</td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                        <div class="table-responsive-md mt-2">
                                                            <table class="table table-bordered" style="width:100%">
                                                                <thead class="table-primary">
                                                                    <tr id="tablehead" class="tablehead">
                                                                        <th>Information</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>Das Tuningmenü kann beim und im
                                                                            Gruppierungshaus benutzt
                                                                            werden.</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Für das Tunen werden Tuningteile benötigt,
                                                                            diese können durch
                                                                            einen Spediteur
                                                                            erworben werden (Produkte)!
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="card card-primary card-outline"
                                    v-if="groupinfo.licenses.split('|')[3] == 1">
                                    <div class="card-header" style="font-family: 'Exo', sans-serif;">
                                        Personenbeförderungslizenz
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card card-primary">
                                                    <div class="card-header">
                                                        <h3 class="card-title">Tasten/Befehle & Informationen</h3>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="table-responsive-md mt-2">
                                                            <table id="logtable" name="logtable"
                                                                class="table table-bordered" style="width:100%">
                                                                <thead class="table-primary">
                                                                    <tr id="tablehead" class="tablehead">
                                                                        <th>Taste/Befehle</th>
                                                                        <th>Beschreibung</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>F4</td>
                                                                        <td>Routenauswahl (Bus) öffnen</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>F3</td>
                                                                        <td>Botaufträge (Taxi) erledigen</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>F4</td>
                                                                        <td>Dienstfahrt starten & Dienstpreis einstellen
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>F6</td>
                                                                        <td>Taxameter an/ausschalten</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>STRG</td>
                                                                        <td>Taxameter reseten</td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                        <div class="table-responsive-md mt-2">
                                                            <table class="table table-bordered" style="width:100%">
                                                                <thead class="table-primary">
                                                                    <tr id="tablehead" class="tablehead">
                                                                        <th>Information</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>Das Busdepot ist auf der Karte mit einem
                                                                            orangenen Bus
                                                                            markiert.</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Der Dienstpreis des Taxameters zählt pro
                                                                            Kilometer/Minute.</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Taxiaufträge können über die nTaxi Handyapp
                                                                            verwaltet werden.
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="card card-primary card-outline"
                                    v-if="groupinfo.licenses.split('|')[2] == 1">
                                    <div class="card-header" style="font-family: 'Exo', sans-serif;">Mechatronikerlizenz
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card card-primary">
                                                    <div class="card-header">
                                                        <h3 class="card-title">Tasten/Befehle & Informationen</h3>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="table-responsive-md mt-2">
                                                            <table id="logtable" name="logtable"
                                                                class="table table-bordered" style="width:100%">
                                                                <thead class="table-primary">
                                                                    <tr id="tablehead" class="tablehead">
                                                                        <th>Taste/Befehle</th>
                                                                        <th>Beschreibung</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>F8</td>
                                                                        <td>Mechatronikermenü öffnen</td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                        <div class="table-responsive-md mt-2">
                                                            <table class="table table-bordered" style="width:100%">
                                                                <thead class="table-primary">
                                                                    <tr id="tablehead" class="tablehead">
                                                                        <th>Information</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>Das Mechatronikermenü kann beim und im
                                                                            Gruppierungshaus benutzt
                                                                            werden.</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Für das Reparieren werden Utensilien
                                                                            benötigt, diese können
                                                                            durch einen
                                                                            Spediteur
                                                                            erworben werden (Produkte)!
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Das Mechatronikermenü kann mobil mit dem
                                                                            Utilitytruck2 und
                                                                            Utilitytruck3
                                                                            aufgerufen werden, kaufbar im Truck
                                                                            Autohaus!
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="card card-primary card-outline"
                                    v-if="groupinfo.licenses.split('|')[4] == 1">
                                    <div class="card-header" style="font-family: 'Exo', sans-serif;">Sicherheitslizenz
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card card-primary">
                                                    <div class="card-header">
                                                        <h3 class="card-title">Tasten/Befehle & Informationen</h3>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="table-responsive-md mt-2">
                                                            <table id="logtable" name="logtable"
                                                                class="table table-bordered" style="width:100%">
                                                                <thead class="table-primary">
                                                                    <tr id="tablehead" class="tablehead">
                                                                        <th>Taste/Befehle</th>
                                                                        <th>Beschreibung</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>F3</td>
                                                                        <td>Bankautomaten Übersicht</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>F4</td>
                                                                        <td>Business Übersicht</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>E</td>
                                                                        <td>Geldkoffer abholen, in den Transporter
                                                                            legen/rausnehmen,
                                                                            Bankautomat befüllen</td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                        <div class="table-responsive-md mt-2">
                                                            <table class="table table-bordered" style="width:100%">
                                                                <thead class="table-primary">
                                                                    <tr id="tablehead" class="tablehead">
                                                                        <th>Information</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>Den Geldkoffer erhält man beim Schalter in
                                                                            der Maze Bank.
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
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
                <div class="row d-flex justify-content-center"
                    style="margin-left: 15rem !important; margin-top: 5.0vh; height:90vh; overflow-x: auto;"
                    v-if="menushowjob">
                    <div class="col-md-12">
                        <div class="col-md-12 mt-1">
                            <div class="box box-default">
                                <div class="card card-primary card-outline" v-if="job == 0">
                                    <div class="card-header" style="font-family: 'Exo', sans-serif;">Befehle, Tasten &
                                        Sonstiges
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card card-primary">
                                                    <div class="card-header">
                                                        <h3 class="card-title">Tasten/Befehle & Informationen</h3>
                                                    </div>
                                                    <div class="col-md-12" style="margin-top: 0.3vw">
                                                        <h3>Kein Job vorhanden!</h3>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="card card-primary card-outline" v-if="job == 1">
                                    <div class="card-header" style="font-family: 'Exo', sans-serif;">Befehle, Tasten &
                                        Sonstiges
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card card-primary">
                                                    <div class="card-header">
                                                        <h3 class="card-title">Tasten/Befehle & Informationen</h3>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="table-responsive-md mt-2">
                                                            <table id="logtable" name="logtable"
                                                                class="table table-bordered" style="width:100%">
                                                                <thead class="table-primary">
                                                                    <tr id="tablehead" class="tablehead">
                                                                        <th>Taste/Befehle</th>
                                                                        <th>Beschreibung</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>F3</td>
                                                                        <td>Produkte aufladen/verkaufen</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>F3</td>
                                                                        <td>Business Belieferungsübersicht</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>F4</td>
                                                                        <td>Auftragsliste</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>STRG</td>
                                                                        <td>Anhänger abkoppeln</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>/products</td>
                                                                        <td>Anzahl geladener Produkte</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>/share</td>
                                                                        <td>Auftrag teilen</td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                        <div class="table-responsive-md mt-2">
                                                            <table class="table table-bordered" style="width:100%">
                                                                <thead class="table-primary">
                                                                    <tr id="tablehead" class="tablehead">
                                                                        <th>Information</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>Der Jobstandort ist auf der Karte mit einem
                                                                            grauen Truck
                                                                            markiert.</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Produkte können bei der Aufladestelle
                                                                            aufgeladen werden, hinter
                                                                            dem
                                                                            Speditionsgebäude.
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Alle 5 Minuten erscheint ein neuer Auftrag
                                                                            für die
                                                                            Auftragsliste.</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Info: Die Anhängersync in RageMP ist noch
                                                                            nicht perfekt, der
                                                                            Anhänger kann
                                                                            'nachziehen'.
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="card card-primary card-outline" v-if="job == 2">
                                    <div class="card-header" style="font-family: 'Exo', sans-serif;">Befehle, Tasten &
                                        Sonstiges
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card card-primary">
                                                    <div class="card-header">
                                                        <h3 class="card-title">Tasten/Befehle & Informationen</h3>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="table-responsive-md mt-2">
                                                            <table id="logtable" name="logtable"
                                                                class="table table-bordered" style="width:100%">
                                                                <thead class="table-primary">
                                                                    <tr id="tablehead" class="tablehead">
                                                                        <th>Taste/Befehle</th>
                                                                        <th>Beschreibung</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>P</td>
                                                                        <td>Fleisch sammeln (Messer in der Hand)</td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                        <div class="table-responsive-md mt-2">
                                                            <table class="table table-bordered" style="width:100%">
                                                                <thead class="table-primary">
                                                                    <tr id="tablehead" class="tablehead">
                                                                        <th>Information</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>Der Jobstandort ist auf der Karte mit einem
                                                                            grauen Crosshair
                                                                            markiert.</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Für das Jagen benötigt man ein Jagtgewehr
                                                                            (Musket) und ein
                                                                            Messer, kaufbar im
                                                                            Ammunation.
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Das Jagtgebiet ist auf der Karte mit einem
                                                                            grünen Crosshair
                                                                            markiert!</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Jäger erhalten einen 10% Bonus beim Verkauf
                                                                            von Fleisch!</td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="card card-primary card-outline" v-if="job == 3">
                                    <div class="card-header" style="font-family: 'Exo', sans-serif;">Befehle, Tasten &
                                        Sonstiges
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card card-primary">
                                                    <div class="card-header">
                                                        <h3 class="card-title">Tasten/Befehle & Informationen</h3>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="table-responsive-md mt-2">
                                                            <table id="logtable" name="logtable"
                                                                class="table table-bordered" style="width:100%">
                                                                <thead class="table-primary">
                                                                    <tr id="tablehead" class="tablehead">
                                                                        <th>Taste/Befehle</th>
                                                                        <th>Beschreibung</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>F4</td>
                                                                        <td>Routenauswahl öffnen</td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                        <div class="table-responsive-md mt-2">
                                                            <table class="table table-bordered" style="width:100%">
                                                                <thead class="table-primary">
                                                                    <tr id="tablehead" class="tablehead">
                                                                        <th>Information</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>Der Jobstandort ist auf der Karte mit einem
                                                                            orangenen Bus
                                                                            markiert.</td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="card card-primary card-outline" v-if="job == 4">
                                    <div class="card-header" style="font-family: 'Exo', sans-serif;">Befehle, Tasten &
                                        Sonstiges
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card card-primary">
                                                    <div class="card-header">
                                                        <h3 class="card-title">Tasten/Befehle & Informationen</h3>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="table-responsive-md mt-2">
                                                            <table id="logtable" name="logtable"
                                                                class="table table-bordered" style="width:100%">
                                                                <thead class="table-primary">
                                                                    <tr id="tablehead" class="tablehead">
                                                                        <th>Taste/Befehle</th>
                                                                        <th>Beschreibung</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>F4</td>
                                                                        <td>Müllroutenauswahl öffnen/Müllposition
                                                                            anzeigen lassen (Sweeper)
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>F/G</td>
                                                                        <td>Am Müllwagen dranhängen</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>E</td>
                                                                        <td>Müllbeutel entsorgen</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>STRG</td>
                                                                        <td>Boden reinigen (Sweeper)</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>/share</td>
                                                                        <td>Müllroute sharen (COOP)</td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                        <div class="table-responsive-md mt-2">
                                                            <table class="table table-bordered" style="width:100%">
                                                                <thead class="table-primary">
                                                                    <tr id="tablehead" class="tablehead">
                                                                        <th>Information</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>Der Jobstandort ist auf der Karte mit einem
                                                                            türkisen Müllwagen
                                                                            markiert.</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Mit dem Sweeper kann der Boden gereinigt
                                                                            werden.</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Mülltonnen werden während einer Müllroute,
                                                                            rot als Müllwagen
                                                                            auf der Karte markiert!</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Beim COOP Modus, ist der Anfragesteller der
                                                                            Müllabholer und der
                                                                            andere Spieler der Fahrer!</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Beim COOP Modus, gibt es einen 3% Lohnbonus!
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Mit dem Sweeper kann der Boden gereinigt
                                                                            werden, der Müll liegt
                                                                            verteilt in LS.</td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="card card-primary card-outline" v-if="job == 5">
                                    <div class="card-header" style="font-family: 'Exo', sans-serif;">Befehle, Tasten &
                                        Sonstiges
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card card-primary">
                                                    <div class="card-header">
                                                        <h3 class="card-title">Tasten/Befehle & Informationen</h3>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="table-responsive-md mt-2">
                                                            <table id="logtable" name="logtable"
                                                                class="table table-bordered" style="width:100%">
                                                                <thead class="table-primary">
                                                                    <tr id="tablehead" class="tablehead">
                                                                        <th>Taste/Befehle</th>
                                                                        <th>Beschreibung</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>F4</td>
                                                                        <td>Zu reinigende Kanalisation anzeigen lassen!
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>STRG</td>
                                                                        <td>Reinigungspunkt säubern</td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                        <div class="table-responsive-md mt-2">
                                                            <table class="table table-bordered" style="width:100%">
                                                                <thead class="table-primary">
                                                                    <tr id="tablehead" class="tablehead">
                                                                        <th>Information</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>Der Jobstandort ist auf der Karte mit einem
                                                                            kleinen
                                                                            dunkelblauen Anhänger markiert.</td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="card card-primary card-outline" v-if="job == 6">
                                    <div class="card-header" style="font-family: 'Exo', sans-serif;">Befehle, Tasten &
                                        Sonstiges
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card card-primary">
                                                    <div class="card-header">
                                                        <h3 class="card-title">Tasten/Befehle & Informationen</h3>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="table-responsive-md mt-2">
                                                            <table id="logtable" name="logtable"
                                                                class="table table-bordered" style="width:100%">
                                                                <thead class="table-primary">
                                                                    <tr id="tablehead" class="tablehead">
                                                                        <th>Taste/Befehle</th>
                                                                        <th>Beschreibung</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>F3</td>
                                                                        <td>Botaufträge erledigen</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>F4</td>
                                                                        <td>Dienstfahrt starten & Dienstpreis einstellen
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>F6</td>
                                                                        <td>Taxameter an/ausschalten</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>STRG</td>
                                                                        <td>Taxameter reseten</td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                        <div class="table-responsive-md mt-2">
                                                            <table class="table table-bordered" style="width:100%">
                                                                <thead class="table-primary">
                                                                    <tr id="tablehead" class="tablehead">
                                                                        <th>Information</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>Der Jobstandort (Yellow Cab Co.) ist auf der
                                                                            Karte mit einem
                                                                            gelben Taxi markiert.</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Der Dienstpreis des Taxameters zählt pro
                                                                            Kilometer/Minute.</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Taxiaufträge können über die nTaxi Handyapp
                                                                            verwaltet werden.
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="card card-primary card-outline" v-if="job == 7">
                                    <div class="card-header" style="font-family: 'Exo', sans-serif;">Befehle, Tasten &
                                        Sonstiges
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card card-primary">
                                                    <div class="card-header">
                                                        <h3 class="card-title">Tasten/Befehle & Informationen</h3>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="table-responsive-md mt-2">
                                                            <table id="logtable" name="logtable"
                                                                class="table table-bordered" style="width:100%">
                                                                <thead class="table-primary">
                                                                    <tr id="tablehead" class="tablehead">
                                                                        <th>Taste/Befehle</th>
                                                                        <th>Beschreibung</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>F3</td>
                                                                        <td>Kühe melken (Ab Skill 2)</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>F4</td>
                                                                        <td>Weizen sähen/ernten (Ab Skill 1)</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>F6</td>
                                                                        <td>Tomaten ernten (Ab Skill 4)</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>STRG</td>
                                                                        <td>Anhänger Abkoppeln</td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                        <div class="table-responsive-md mt-2">
                                                            <table class="table table-bordered" style="width:100%">
                                                                <thead class="table-primary">
                                                                    <tr id="tablehead" class="tablehead">
                                                                        <th>Information</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>Der Jobstandort ist auf der Karte mit einer
                                                                            grünen Pflanze
                                                                            markiert.</td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="card card-primary card-outline" v-if="job == 8">
                                    <div class="card-header" style="font-family: 'Exo', sans-serif;">Befehle, Tasten &
                                        Sonstiges
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card card-primary">
                                                    <div class="card-header">
                                                        <h3 class="card-title">Tasten/Befehle & Informationen</h3>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="table-responsive-md mt-2">
                                                            <table id="logtable" name="logtable"
                                                                class="table table-bordered" style="width:100%">
                                                                <thead class="table-primary">
                                                                    <tr id="tablehead" class="tablehead">
                                                                        <th>Taste/Befehle</th>
                                                                        <th>Beschreibung</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>F3</td>
                                                                        <td>Bankautomaten Übersicht</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>F4</td>
                                                                        <td>Business Übersicht</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>E</td>
                                                                        <td>Geldkoffer abholen, in den Transporter
                                                                            legen/rausnehmen,
                                                                            Bankautomat befüllen</td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                        <div class="table-responsive-md mt-2">
                                                            <table class="table table-bordered" style="width:100%">
                                                                <thead class="table-primary">
                                                                    <tr id="tablehead" class="tablehead">
                                                                        <th>Information</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>Der Jobstandort ist auf der Karte mit einem
                                                                            braunen Transporter
                                                                            markiert.</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Für das Arbeiten als Geldlieferant benötigt
                                                                            man einen
                                                                            Schlagstock, kaufbar im
                                                                            Ammunation.
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Den Geldkoffer erhält man beim Schalter in
                                                                            der Maze Bank.
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
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
                <div class="row d-flex justify-content-center"
                    style="margin-left: 15rem !important; margin-top: 5.0vh; height:90vh; overflow-x: auto;"
                    v-if="menushowfactionhelp">
                    <div class="col-md-12">
                        <div class="col-md-12 mt-1">
                            <div class="box box-default">
                                <div class="card card-primary card-outline" v-if="faction == 0">
                                    <div class="card-header" style="font-family: 'Exo', sans-serif;">Befehle, Tasten &
                                        Sonstiges
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card card-primary">
                                                    <div class="card-header">
                                                        <h3 class="card-title">Tasten/Befehle & Informationen</h3>
                                                    </div>
                                                    <div class="col-md-12" style="margin-top: 0.3vw">
                                                        <h3>Kein Fraktion vorhanden!</h3>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="card card-primary card-outline" v-if="faction == 1">
                                    <div class="card-header" style="font-family: 'Exo', sans-serif;">Befehle, Tasten &
                                        Sonstiges
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card card-primary">
                                                    <div class="card-header">
                                                        <h3 class="card-title">Tasten/Befehle & Informationen</h3>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="table-responsive-md mt-2">
                                                            <table id="logtable" name="logtable"
                                                                class="table table-bordered" style="width:100%">
                                                                <thead class="table-primary">
                                                                    <tr id="tablehead" class="tablehead">
                                                                        <th>Taste/Befehle</th>
                                                                        <th>Beschreibung</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>F2</td>
                                                                        <td>Fraktionsinfos einsehen</td>
                                                                    </tr>
                                                                    <tr v-if="voicerp == 1">
                                                                        <td>N (GTA 5 Push to Talk Taste)</td>
                                                                        <td>Funk (gedrückt halten)</td>
                                                                    </tr>
                                                                    <tr v-if="voicerp == 2">
                                                                        <td>N</td>
                                                                        <td>Funk (gedrückt halten)</td>
                                                                    </tr>
                                                                    <tr v-if="voicerp == 0">
                                                                        <td>/radio</td>
                                                                        <td>Funk</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>M</td>
                                                                        <td>Fraktionaktionsmenü/Interaktionsmenü öffnen
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Q</td>
                                                                        <td>Sirenenmodus umschalten</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>STRG + Q</td>
                                                                        <td>Sirenenmodus zurückschalten</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>1-5</td>
                                                                        <td>Sirene (akustisch) an/ausschalten</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Leertaste (Space)</td>
                                                                        <td>Drohnenmodus switchen</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>F6</td>
                                                                        <td>Waffentransport/Asservatentransport
                                                                            abholen/abliefern</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>F6</td>
                                                                        <td>Lasermessgerät an/ausschalten</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>F6</td>
                                                                        <td>Helikopterkamera benutzen</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>F8</td>
                                                                        <td>Strafzettel ausstellen</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>/createspeedcam/deletespeedcam</td>
                                                                        <td>Blitzer auf/abbauen</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>/createcctv/deletecctv</td>
                                                                        <td>CCTV auf/abbauen</td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                        <div class="table-responsive-md mt-2">
                                                            <table class="table table-bordered" style="width:100%">
                                                                <thead class="table-primary">
                                                                    <tr id="tablehead" class="tablehead">
                                                                        <th>Information</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>Der Fraktionsstandort ist auf der Karte mit
                                                                            einem blauen
                                                                            Polizei Stern markiert.</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Das MDC kann über die MDC Smartphone App
                                                                            geöffnet werden -
                                                                            [F5]!</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Über das [X] Menü, können
                                                                            gefesselte/gezogene Spieler in/aus
                                                                            einem Fahrzeug geworfen werden.</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Im Helikopter kann man sich mit der
                                                                            Leertaste (Space) abseilen.
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Helikopterkamera: Leertaste (Space) = Kamera
                                                                            Lock, RMB - Modus
                                                                            switchen</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Visuelle Schutzwesten (Mann): 59, 60, 62,
                                                                            65, 63, 61</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Visuelle Schutzwesten (Frau): 59, 58, 60,
                                                                            61, 62</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Verfügbare Funkkanäle: 900mHz-925mHz</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Verfügbare Funkkanäle (Staatsfunk):
                                                                            951mHz-960mHz</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Rechnungen wie z.B TÜV, Reparaturen können
                                                                            durch die Firma via
                                                                            nPayment an den Staat ausgestellt werden!
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="card card-primary card-outline" v-if="faction == 2">
                                    <div class="card-header" style="font-family: 'Exo', sans-serif;">Befehle, Tasten &
                                        Sonstiges
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card card-primary">
                                                    <div class="card-header">
                                                        <h3 class="card-title">Tasten/Befehle & Informationen</h3>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="table-responsive-md mt-2">
                                                            <table id="logtable" name="logtable"
                                                                class="table table-bordered" style="width:100%">
                                                                <thead class="table-primary">
                                                                    <tr id="tablehead" class="tablehead">
                                                                        <th>Taste/Befehle</th>
                                                                        <th>Beschreibung</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>F2</td>
                                                                        <td>Fraktionsinfos einsehen</td>
                                                                    </tr>
                                                                    <tr v-if="voicerp == 1">
                                                                        <td>N (GTA 5 Push to Talk Taste)</td>
                                                                        <td>Funk (gedrückt halten)</td>
                                                                    </tr>
                                                                    <tr v-if="voicerp == 2">
                                                                        <td>N</td>
                                                                        <td>Funk (gedrückt halten)</td>
                                                                    </tr>
                                                                    <tr v-if="voicerp == 0">
                                                                        <td>/radio</td>
                                                                        <td>Funk</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>/gwc</td>
                                                                        <td>Fahrzeug aus dem Wasser ziehen</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>M</td>
                                                                        <td>Fraktionaktionsmenü/Interaktionsmenü öffnen
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Q</td>
                                                                        <td>Sirenenmodus umschalten</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>STRG + Q</td>
                                                                        <td>Sirenenmodus zurückschalten</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>1-5</td>
                                                                        <td>Sirene (akustisch) an/ausschalten</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>F6</td>
                                                                        <td>Feuerlöscher auffüllen (Beim Feuerwehrwagen)
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>F8</td>
                                                                        <td>Rezepte ausstellen</td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                        <div class="table-responsive-md mt-2">
                                                            <table class="table table-bordered" style="width:100%">
                                                                <thead class="table-primary">
                                                                    <tr id="tablehead" class="tablehead">
                                                                        <th>Information</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>Der Fraktionsstandort ist auf der Karte mit
                                                                            einem roten
                                                                            Krankenhaussymbol markiert.</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Das MDC kann über die MDC Smartphone App
                                                                            geöffnet werden -
                                                                            [F5]!</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Helikopterkamera: Leertaste (Space) = Kamera
                                                                            Lock, RMB - Modus
                                                                            switchen</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Verfügbare Funkkanäle: 926mHz-941mHz</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Verfügbare Funkkanäle (Staatsfunk):
                                                                            951mHz-960mHz</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Rechnungen wie z.B TÜV, Reparaturen können
                                                                            durch die Firma via
                                                                            nPayment an den Staat ausgestellt werden!
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Umgefallene Bäume können mit der Axt
                                                                            beseitigt werden!</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Feuer ist nicht zu 100% Sync, wird beim
                                                                            Löschvorgang trotzdem
                                                                            gelöscht!</td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="card card-primary card-outline" v-if="faction == 3">
                                    <div class="card-header" style="font-family: 'Exo', sans-serif;">Befehle, Tasten &
                                        Sonstiges
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card card-primary">
                                                    <div class="card-header">
                                                        <h3 class="card-title">Tasten/Befehle & Informationen</h3>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="table-responsive-md mt-2">
                                                            <table id="logtable" name="logtable"
                                                                class="table table-bordered" style="width:100%">
                                                                <thead class="table-primary">
                                                                    <tr id="tablehead" class="tablehead">
                                                                        <th>Taste/Befehle</th>
                                                                        <th>Beschreibung</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>F2</td>
                                                                        <td>Fraktionsinfos einsehen</td>
                                                                    </tr>
                                                                    <tr v-if="voicerp == 1">
                                                                        <td>N (GTA 5 Push to Talk Taste)</td>
                                                                        <td>Funk (gedrückt halten)</td>
                                                                    </tr>
                                                                    <tr v-if="voicerp == 2">
                                                                        <td>N</td>
                                                                        <td>Funk (gedrückt halten)</td>
                                                                    </tr>
                                                                    <tr v-if="voicerp == 0">
                                                                        <td>/radio</td>
                                                                        <td>Funk</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>M</td>
                                                                        <td>Fraktionaktionsmenü/Interaktionsmenü öffnen
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Q</td>
                                                                        <td>Sirenenmodus umschalten</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>STRG + Q</td>
                                                                        <td>Sirenenmodus zurückschalten</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>1-5</td>
                                                                        <td>Sirene (akustisch) an/ausschalten</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>F6</td>
                                                                        <td>Strafzettel (Fahrzeug) ausstellen</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>F8</td>
                                                                        <td>Tuningmenü aufrufen</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>F8</td>
                                                                        <td>Mechatronikermenü aufrufen</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>STRG/Shift (Towtruck)</td>
                                                                        <td>Abschlepphaken benutzen</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>H (Gedrückt halten - Towtruck)</td>
                                                                        <td>Abschlepphaken entfernen</td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                        <div class="table-responsive-md mt-2">
                                                            <table class="table table-bordered" style="width:100%">
                                                                <thead class="table-primary">
                                                                    <tr id="tablehead" class="tablehead">
                                                                        <th>Information</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>Der Fraktionsstandort ist auf der Karte mit
                                                                            einem schwarzen
                                                                            Werkzeugsymbol markiert.</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Der Verwahrplatz ist auf der Karte mit einem
                                                                            grauen
                                                                            Auto/Dollarsymbol markiert.</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Das MDC kann über die MDC Smartphone App
                                                                            geöffnet werden -
                                                                            [F5]!</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Verfügbare Funkkanäle: 942mHz-945mHz</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Verfügbare Funkkanäle (Staatsfunk):
                                                                            951mHz-960mHz</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Im Flatbed können Fahrzeuge mit der Taste
                                                                            [Q] gezogen werden,
                                                                            die Rampe kann mit der Taste [Shift] bedient
                                                                            werden!</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Rechnungen wie z.B TÜV, Reparaturen können
                                                                            durch die Firma via
                                                                            nPayment an den Staat ausgestellt werden!
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="card card-primary card-outline" v-if="faction == 4">
                                    <div class="card-header" style="font-family: 'Exo', sans-serif;">Befehle, Tasten &
                                        Sonstiges
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card card-primary">
                                                    <div class="card-header">
                                                        <h3 class="card-title">Tasten/Befehle & Informationen</h3>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="table-responsive-md mt-2">
                                                            <table id="logtable" name="logtable"
                                                                class="table table-bordered" style="width:100%">
                                                                <thead class="table-primary">
                                                                    <tr id="tablehead" class="tablehead">
                                                                        <th>Taste/Befehle</th>
                                                                        <th>Beschreibung</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>F2</td>
                                                                        <td>Fraktionsinfos einsehen</td>
                                                                    </tr>
                                                                    <tr v-if="voicerp == 1">
                                                                        <td>N (GTA 5 Push to Talk Taste)</td>
                                                                        <td>Funk (gedrückt halten)</td>
                                                                    </tr>
                                                                     <tr v-if="voicerp == 2">
                                                                        <td>N</td>
                                                                        <td>Funk (gedrückt halten)</td>
                                                                    </tr>
                                                                    <tr v-if="voicerp == 0">
                                                                        <td>/radio</td>
                                                                        <td>Funk</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Q</td>
                                                                        <td>Sirenenmodus umschalten</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>STRG + Q</td>
                                                                        <td>Sirenenmodus zurückschalten</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>1-5</td>
                                                                        <td>Sirene (akustisch) an/ausschalten</td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                        <div class="table-responsive-md mt-2">
                                                            <table class="table table-bordered" style="width:100%">
                                                                <thead class="table-primary">
                                                                    <tr id="tablehead" class="tablehead">
                                                                        <th>Information</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>Der Fraktionsstandort ist bei dem Rathaus.
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Verfügbare Funkkanäle: 946mHz-950mHz</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Verfügbare Funkkanäle (Staatsfunk):
                                                                            951mHz-960mHz</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Die allgemeine Verwaltung (z.B
                                                                            Steuerverwaltung) befindet sich
                                                                            im Präsidentenbüro!</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Rechnungen wie z.B TÜV, Reparaturen können
                                                                            durch die Firma via
                                                                            nPayment an den Staat ausgestellt werden!
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Rechnungen an den Staat können über die
                                                                            nPayment App verwaltet
                                                                            werden!</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Auf das Staatskonto (Staatskasse) kann über
                                                                            die Maze Bank/Bank
                                                                            zugegriffen werden!</td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
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
                <div class="row d-flex justify-content-center"
                    style="margin-left: 15rem !important; margin-top: 5.0vh; height:90vh; overflow-x: auto;"
                    v-if="menushowsettings">
                    <div class="col-md-12">
                        <div class="col-md-12 mt-1">
                            <div class="box box-default">
                                <div class="card card-primary card-outline">
                                    <div class="card-header" style="font-family: 'Exo', sans-serif;">Einstellungen</div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card card-primary">
                                                    <div class="card-header">
                                                        <h3 class="card-title">Einstellungen</h3>
                                                    </div>
                                                    <div class="col-md-12 justify-content-center text-center"
                                                        style="margin-top: 0.5vw">
                                                        <div class="row">
                                                            <div class="col-md-9">
                                                                <label>Allgemeine Einstellungen</label>
                                                                <button style="margin-top: 0.7vw"
                                                                    :class="[(autologin == 0) ? 'btn btn-block btn-secondary btn-sm':'btn btn-block btn-primary btn-sm']"
                                                                    type="submit"
                                                                    v-on:click="selectSetting(1)">Autologin</button>
                                                                <button v-if="crosshair != 0"
                                                                    :class="[(crosshair != 0) ? 'btn btn-block btn-secondary btn-sm':'btn btn-block btn-primary btn-sm']"
                                                                    style="margin-top: 1vw" type="submit"
                                                                    v-on:click="selectCrosshair(0)">Crosshair
                                                                    ausschalten</button>
                                                                <button class="btn btn-block btn-primary btn-sm"
                                                                    style="margin-top: 1vw" type="submit"
                                                                    v-on:click="selectWalkingStyle">Laufstil
                                                                    auswählen</button>
                                                                <div class="col-md-12">
                                                                    <label style="margin-top: 1.45vw">Crosshair</label>
                                                                    <div class="row justify-content-center">
                                                                        <div v-for="index in 18" :key="index"
                                                                            style="margin-left:0.35vw">
                                                                            <img v-if="index <= 8"
                                                                                :src="getCrosshair(index)"
                                                                                alt="Crosshair"
                                                                                style="margin-top: 0.5vw; margin-left: 0.2vw"
                                                                                class="crosshair">
                                                                            <img v-else :src="getCrosshair(index)"
                                                                                alt="Crosshair"
                                                                                style="margin-top: 0.6vw; margin-left: 0.2vw"
                                                                                class="crosshair">
                                                                            <button
                                                                                class="btn btn-block btn-primary btn-sm"
                                                                                style="margin-top: 0.5vw" type="submit"
                                                                                disabled
                                                                                v-if="crosshair == index">Auswahl</button>
                                                                            <button
                                                                                class="btn btn-block btn-primary btn-sm"
                                                                                style="margin-top: 0.5vw" type="submit"
                                                                                v-else
                                                                                v-on:click="selectCrosshair(index)">Auswahl</button>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <label style="margin-top: 1.45vw">Visuelle Schutzweste
                                                                    einstellen</label>
                                                                <input type="text" class="form-control"
                                                                    style="margin-top: 0.7vw"
                                                                    placeholder="ID der sichtbaren Schutzweste/0 zum deaktivieren"
                                                                    v-model="setarmor" maxlength="2" name="setarmor"
                                                                    autocomplete="off">
                                                                <button class="btn btn-block btn-primary btn-sm"
                                                                    style="margin-top: 1vw" type="submit"
                                                                    v-on:click="selectSetting(3)">Setzen</button>
                                                                <label style="margin-top: 1.2vw">Gutschein
                                                                    einlösen</label>
                                                                <input type="text" class="form-control"
                                                                    style="margin-top: 0.5vw" placeholder="Gutschein"
                                                                    v-model="gutschein" maxlength="8" name="gutschein"
                                                                    autocomplete="off">
                                                                <button class="btn btn-block btn-primary btn-sm"
                                                                    style="margin-top: 1vw" type="submit"
                                                                    v-on:click="selectSetting(4)">Einlösen</button>
                                                            </div>
                                                            <div class="col-md-3" v-if="windowWidth > 800">
                                                                <label>Animations Hotkeys</label>
                                                                <div class="col-md-12">
                                                                    <table id="logtable"
                                                                        class="table table-bordered mt-2"
                                                                        style="width:100%">
                                                                        <thead class="table-primary">
                                                                            <tr>
                                                                                <th>NUM Taste</th>
                                                                                <th>Animation</th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <tr>
                                                                                <td>NUM 1</td>
                                                                                <td><input type="text"
                                                                                        class="form-control"
                                                                                        v-model="animations[0]"
                                                                                        maxlength="35"
                                                                                        autocomplete="off"></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>NUM 2</td>
                                                                                <td><input type="text"
                                                                                        class="form-control"
                                                                                        v-model="animations[1]"
                                                                                        maxlength="35"
                                                                                        autocomplete="off"></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>NUM 3</td>
                                                                                <td><input type="text"
                                                                                        class="form-control"
                                                                                        v-model="animations[2]"
                                                                                        maxlength="35"
                                                                                        autocomplete="off"></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>NUM 4</td>
                                                                                <td><input type="text"
                                                                                        class="form-control"
                                                                                        v-model="animations[3]"
                                                                                        maxlength="35"
                                                                                        autocomplete="off"></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>NUM 5</td>
                                                                                <td><input type="text"
                                                                                        class="form-control"
                                                                                        v-model="animations[4]"
                                                                                        maxlength="35"
                                                                                        autocomplete="off"></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>NUM 6</td>
                                                                                <td><input type="text"
                                                                                        class="form-control"
                                                                                        v-model="animations[5]"
                                                                                        maxlength="35"
                                                                                        autocomplete="off"></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>NUM 7</td>
                                                                                <td><input type="text"
                                                                                        class="form-control"
                                                                                        v-model="animations[6]"
                                                                                        maxlength="35"
                                                                                        autocomplete="off"></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>NUM 8</td>
                                                                                <td><input type="text"
                                                                                        class="form-control"
                                                                                        v-model="animations[7]"
                                                                                        maxlength="35"
                                                                                        autocomplete="off"></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>NUM 9</td>
                                                                                <td><input type="text"
                                                                                        class="form-control"
                                                                                        v-model="animations[8]"
                                                                                        maxlength="35"
                                                                                        autocomplete="off"></td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                    <button @click="updateAnimations()"
                                                                        class="btn btn-block btn-primary mb-2 mt-3">Speichern</button>
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
                <div class="row d-flex justify-content-center"
                    style="margin-left: 15rem !important; margin-top: 5.0vh; height:90vh; overflow-x: auto;"
                    v-if="menushowhelp">
                    <div class="col-md-12">
                        <div class="col-md-12 mt-1">
                            <div class="box box-default">
                                <div class="card card-primary card-outline">
                                    <div class="card-header" style="font-family: 'Exo', sans-serif;">Befehle & Tasten
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="card card-primary">
                                                    <div class="card-header">
                                                        <h3 class="card-title">Tasten</h3>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="table-responsive-md mt-2">
                                                            <table id="logtable" name="logtable"
                                                                class="table table-bordered" style="width:100%">
                                                                <thead class="table-primary">
                                                                    <tr id="tablehead" class="tablehead">
                                                                        <th>Taste</th>
                                                                        <th>Beschreibung</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>Bild auf/Bild runter</td>
                                                                        <td>Chat scrollen</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>F2</td>
                                                                        <td>Verwaltungsmenü</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>F4</td>
                                                                        <td>Garagen benutzen</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>F5</td>
                                                                        <td>Handy</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>F6</td>
                                                                        <td>Werkbank benutzen (Craften)</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>F6</td>
                                                                        <td>Kleiderschrank benutzen</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>F7</td>
                                                                        <td>Animationsliste anzeigen</td>
                                                                    </tr>
                                                                    <tr v-if="voicerp == 2">
                                                                        <td>F9</td>
                                                                        <td>Voice-Chat aktivieren/deaktivieren</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>F10</td>
                                                                        <td>Cursor anzeigen/ausblenden</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>F</td>
                                                                        <td>Interaktionen ausführen</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>F/G</td>
                                                                        <td>In ein Fahrzeug einsteigen</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>ESC</td>
                                                                        <td>Interaktionen abbrechen</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Backspace</td>
                                                                        <td>Animation stoppen
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>1</td>
                                                                        <td>Fenster öffnen/schließen</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>I</td>
                                                                        <td>Inventar verwalten</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>K</td>
                                                                        <td>Kleidung verwalten</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>X</td>
                                                                        <td>Auswahlmenü öffnen</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>P</td>
                                                                        <td>Gegenstände aufheben</td>
                                                                    </tr>
                                                                    <tr v-if="voicerp == 1">
                                                                        <td>^</td>
                                                                        <td>Voice-Range ändern</td>
                                                                    </tr>
                                                                    <tr v-if="voicerp == 2">
                                                                        <td>ROLLEN</td>
                                                                        <td>Voice-Range ändern</td>
                                                                    </tr>
                                                                    <tr v-if="voicerp == 1">
                                                                        <td>N (GTA 5 Push to Talk Taste)</td>
                                                                        <td>Funk (gedrückt halten)</td>
                                                                    </tr>
                                                                    <tr v-if="voicerp == 2">
                                                                        <td>N</td>
                                                                        <td>Funk (gedrückt halten)</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>EINF</td>
                                                                        <td>Hände hoch</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>G</td>
                                                                        <td>An/Abschnallen</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>L</td>
                                                                        <td>Sachen auf/abschließen</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>M</td>
                                                                        <td>Motor starten/stoppen</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>O</td>
                                                                        <td>Chat an/ausmachen</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>M (Möbelmodus)</td>
                                                                        <td>Möbelverwaltung öffnen</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>TAB</td>
                                                                        <td>Spielerliste anzeigen</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>ALT (Rechts)</td>
                                                                        <td>Crouchen</td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="card card-primary">
                                                    <div class="card-header">
                                                        <h3 class="card-title">Befehle</h3>
                                                    </div>
                                                    <div class="card-body">
                                                        <table id="logtable" name="logtable"
                                                            class="table table-bordered" style="width:100%">
                                                            <thead class="table-primary">
                                                                <tr id="tablehead" class="tablehead">
                                                                    <th>Taste</th>
                                                                    <th>Beschreibung</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr>
                                                                    <td>/credits</td>
                                                                    <td>Credits anzeigen lassen</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/report</td>
                                                                    <td>Einen Spieler melden</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/vehicleinfo (/dl)</td>
                                                                    <td>Fahrzeuginformationen anzeigen</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/sellvehicle</td>
                                                                    <td>Fahrzeug verkaufen</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/socialclubid</td>
                                                                    <td>Socialclub-ID anzeigen</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/fuel</td>
                                                                    <td>Tank einsehen</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/checktüv</td>
                                                                    <td>TÜV Plakette anschauen</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/animation (/a)</td>
                                                                    <td>Animation abspielen</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/stopanimation</td>
                                                                    <td>Animation beenden</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/resync</td>
                                                                    <td>Spieler neu synchronisieren</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/abort</td>
                                                                    <td>Missionen/Aufträge abbrechen</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/afk</td>
                                                                    <td>AFK Modus aktivieren</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/dimension</td>
                                                                    <td>Dimension anzeigen</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/reloadhud</td>
                                                                    <td>HUD reloaden</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/lastvehicle</td>
                                                                    <td>Letzte Fahrzeug-ID anzeigen</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/vehicleclass</td>
                                                                    <td>Fahrzeug Klasse anzeigen</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/settorso</td>
                                                                    <td>Torso setzen</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/möbelmodus</td>
                                                                    <td>Möbelmodus ak/deaktivieren</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/möbel</td>
                                                                    <td>Möbelmenü aufrufen, auch mit Möbel-ID</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/position & /rotation</td>
                                                                    <td>Möbelposition/rotation anpassen</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/logout</td>
                                                                    <td>Charakterauswahl aufrufen</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/addfriend & /deletefriend</td>
                                                                    <td>Freund hinzufügen/löschen</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/pipe</td>
                                                                    <td>Haustier rufen</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/petname</td>
                                                                    <td>Haustiernamen festlegen</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/disguise</td>
                                                                    <td>Identität verschleiern</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/clearmychat</td>
                                                                    <td>Chatverlauf löschen</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/fontsize</td>
                                                                    <td>Chatgröße anpassen</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/timestamp</td>
                                                                    <td>Chat Timestamp an / ausschalten</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/overlaymodus</td>
                                                                    <td>Overlaymodus aktivieren/deaktivieren</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/q</td>
                                                                    <td>Spiel beenden</td>
                                                                </tr>
                                                                <tr v-if="voicerp == 0">
                                                                    <td>/condition</td>
                                                                    <td>Zustand setzen/entfernen</td>
                                                                </tr>
                                                                <tr v-if="voicerp == 0">
                                                                    <td>/addinfo</td>
                                                                    <td>Hinweis erstellen</td>
                                                                </tr>
                                                                <tr v-if="voicerp == 0">
                                                                    <td>/deleteinfo</td>
                                                                    <td>Hinweis löschen</td>
                                                                </tr>
                                                                <tr v-if="voicerp == 0">
                                                                    <td>/me - /do</td>
                                                                    <td>Roleplay Befehle</td>
                                                                </tr>
                                                                <tr v-if="voicerp == 0">
                                                                    <td>[T] Text</td>
                                                                    <td>Roleplay (normaler) Chat</td>
                                                                </tr>
                                                                <tr v-if="voicerp == 0">
                                                                    <td>/b</td>
                                                                    <td>OOC Chat</td>
                                                                </tr>
                                                                <tr v-if="voicerp == 0">
                                                                    <td>/sq</td>
                                                                    <td>Leise reden</td>
                                                                </tr>
                                                                <tr v-if="voicerp == 0">
                                                                    <td>! Text</td>
                                                                    <td>Schreien</td>
                                                                </tr>
                                                                <tr v-if="voicerp == 0">
                                                                    <td>/radio</td>
                                                                    <td>Funk</td>
                                                                </tr>
                                                                <tr v-if="voicerp == 0">
                                                                    <td>/speaker</td>
                                                                    <td>Handy Lautsprecher an/ausschalten</td>
                                                                </tr>
                                                                <tr v-if="voicerp == 2">
                                                                    <td>/reloadvoicechat</td>
                                                                    <td>Voice-Chat reloaden</td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
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
                <div class="row d-flex justify-content-center"
                    style="margin-left: 15rem !important; margin-top: 5.0vh; height:90vh; overflow-x: auto;"
                    v-if="menushowadminhelp">
                    <div class="col-md-12">
                        <div class="col-md-12 mt-1">
                            <div class="box box-default">
                                <div class="card card-primary card-outline">
                                    <div class="card-header" style="font-family: 'Exo', sans-serif;" v-if="tester>=1">
                                        Testerbefehle</div>
                                    <div class="card-body" v-if="tester>=1">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card card-primary">
                                                    <div class="card-header">
                                                        <h3 class="card-title">Testerbefehle</h3>
                                                    </div>
                                                    <div class="card-body">
                                                        <table id="logtable" name="logtable"
                                                            class="table table-bordered" style="width:100%">
                                                            <thead class="table-primary">
                                                                <tr id="tablehead" class="tablehead">
                                                                    <th>Befehl/Taste</th>
                                                                    <th>Beschreibung</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr>
                                                                    <td>? Nachricht</td>
                                                                    <td>Testerchat benutzen</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/revive</td>
                                                                    <td>Spieler wiederbeleben</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/set</td>
                                                                    <td>Sachen, Eigenschaften setzen</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/giveitem</td>
                                                                    <td>Items vergeben</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/agl</td>
                                                                    <td>Lizenzen vergeben</td>

                                                                </tr>
                                                                <tr>
                                                                    <td>/teleport</td>
                                                                    <td>Zu einer Position teleportieren</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/veh</td>
                                                                    <td>Ein Fahrzeug spawnen</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/fixveh</td>
                                                                    <td>Fahrzeug reparieren</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/fillveh</td>
                                                                    <td>Fahrzeugtank befüllen</td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card-header" style="font-family: 'Exo', sans-serif;" v-if="admin>=1">
                                        Adminbefehle</div>
                                    <div class="card-body" v-if="admin>=1">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card card-primary">
                                                    <div class="card-header">
                                                        <h3 class="card-title">Adminbefehle</h3>
                                                    </div>
                                                    <div class="card-body">
                                                        <table id="logtable" name="logtable"
                                                            class="table table-bordered" style="width:100%">
                                                            <thead class="table-primary">
                                                                <tr id="tablehead" class="tablehead">
                                                                    <th>Befehl/Taste</th>
                                                                    <th>Beschreibung</th>
                                                                    <th>Ab Rang</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody v-if="admin >= 1">
                                                                <tr>
                                                                    <td>@ Nachricht</td>
                                                                    <td>Adminchat benutzen</td>
                                                                    <td>Probe Moderator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/toggleachat</td>
                                                                    <td>Admin-Benachrichtigungen aktivieren/deaktivieren
                                                                    </td>
                                                                    <td>Probe Moderator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>[STRG]</td>
                                                                    <td>Spieler tragen</td>
                                                                    <td>Probe Moderator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/adminduty</td>
                                                                    <td>Admindienst beginnen/beenden</td>
                                                                    <td>Probe Moderator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/freeze</td>
                                                                    <td>Einen Spieler einfrieren/entfrieren</td>
                                                                    <td>Probe Moderator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/whitelist</td>
                                                                    <td>Einen Spieler auf die Whitelist setzen/entfernen
                                                                    </td>
                                                                    <td>Probe Moderator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/revive</td>
                                                                    <td>Spieler wiederbeleben</td>
                                                                    <td>Probe Moderator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/goto</td>
                                                                    <td>Zu einem Spieler teleportieren</td>
                                                                    <td>Probe Moderator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/gethere</td>
                                                                    <td>Einen Spieler zu sich teleportieren</td>
                                                                    <td>Probe Moderator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/getcar</td>
                                                                    <td>Ein Fahrzeug zu sich teleportieren</td>
                                                                    <td>Probe Moderator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/vehiclegarage</td>
                                                                    <td>Fahrzeuge in Garage einparken</td>
                                                                    <td>Probe Moderator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/admingarage</td>
                                                                    <td>Admingarage aufrufen</td>
                                                                    <td>Probe Moderator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/checkvehicles</td>
                                                                    <td>Fahrzeuge checken</td>
                                                                    <td>Probe Moderator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/checkplayer</td>
                                                                    <td>Spielerprofil checken</td>
                                                                    <td>Probe Moderator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/checkinventory</td>
                                                                    <td>Inventar checken</td>
                                                                    <td>Probe Moderator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/getvehicleid</td>
                                                                    <td>Server Fahrzeug-ID rausfinden</td>
                                                                    <td>Probe Moderator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/gointocar</td>
                                                                    <td>In ein Fahrzeug teleportieren</td>
                                                                    <td>Probe Moderator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/gotohouse</td>
                                                                    <td>Zu einem Haus teleportieren</td>
                                                                    <td>Probe Moderator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/veh(2)</td>
                                                                    <td>Ein Adminfahrzeug spawnen/löschen</td>
                                                                    <td>Probe Moderator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/prison</td>
                                                                    <td>Einen Spieler ins Prison stecken</td>
                                                                    <td>Probe Moderator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/spectate & /stopspec</td>
                                                                    <td>Einen Spieler beobachten/Beobachtung beenden
                                                                    </td>
                                                                    <td>Probe Moderator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/nametag</td>
                                                                    <td>Nametag verstecken</td>
                                                                    <td>Probe Moderator</td>
                                                                </tr>
                                                                <tr v-if="voicerp == 0">
                                                                    <td>/checkinfo</td>
                                                                    <td>Hinweis checken</td>
                                                                    <td>Probe Moderator</td>
                                                                </tr>
                                                            </tbody>
                                                            <tbody v-if="admin >= 2">
                                                                <tr>
                                                                    <td>/resetweapons</td>
                                                                    <td>Waffen reseten</td>
                                                                    <td>Moderator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/kick</td>
                                                                    <td>Einen Spieler kicken</td>
                                                                    <td>Moderator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/save</td>
                                                                    <td>Eine Position speichern</td>
                                                                    <td>Moderator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/teleport</td>
                                                                    <td>Zu einer Position teleportieren</td>
                                                                    <td>Moderator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/telexyz</td>
                                                                    <td>Zu Koordinaten teleportieren</td>
                                                                    <td>Moderator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/loadipl</td>
                                                                    <td>IPL laden</td>
                                                                    <td>Moderator</td>
                                                                </tr>
                                                            </tbody>
                                                            <tbody v-if="admin >= 3">
                                                                <tr>
                                                                    <td>/ban</td>
                                                                    <td>Einen Spieler bannen</td>
                                                                    <td>Supporter</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/setskin</td>
                                                                    <td>Spieler einen Skin setzen</td>
                                                                    <td>Supporter</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/reconnect</td>
                                                                    <td>Spieler reconnecten lassen</td>
                                                                    <td>Supporter</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/fillveh</td>
                                                                    <td>Fahrzeugtank befüllen</td>
                                                                    <td>Supporter</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/fixveh</td>
                                                                    <td>Fahrzeug reparieren</td>
                                                                    <td>Supporter</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/createcoupon</td>
                                                                    <td>Gutschein erstellen</td>
                                                                    <td>Supporter</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/testcloth</td>
                                                                    <td>Kleidung testen</td>
                                                                    <td>Supporter</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/testeupoutfit</td>
                                                                    <td>EUP Outfits testen</td>
                                                                    <td>Supporter</td>
                                                                </tr>
                                                            </tbody>
                                                            <tbody v-if="admin >= 4">
                                                                <tr>
                                                                    <td>/unban</td>
                                                                    <td>Einen Spieler/Identifizierer entbannen</td>
                                                                    <td>Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/unprison</td>
                                                                    <td>Einen Spieler aus dem Prison holen</td>
                                                                    <td>Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/set</td>
                                                                    <td>Sachen, Eigenschaften setzen</td>
                                                                    <td>Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/setweather</td>
                                                                    <td>Wetter setzen</td>
                                                                    <td>Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/editgroup</td>
                                                                    <td>Gruppierungen bearbeiten</td>
                                                                    <td>Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/groupcheck</td>
                                                                    <td>Gruppierung überprüfen</td>
                                                                    <td>Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/giveitem</td>
                                                                    <td>Items vergeben</td>
                                                                    <td>Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/itemcheck</td>
                                                                    <td>Items überprüfen</td>
                                                                    <td>Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/deleteitem</td>
                                                                    <td>Items löschen</td>
                                                                    <td>Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/createfactionsdoor</td>
                                                                    <td>Fraktions Tür erstellen</td>
                                                                    <td>Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/testobject</td>
                                                                    <td>Testobjekt erstellen</td>
                                                                    <td>Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/createanimation</td>
                                                                    <td>Neue Animation hinzufügen</td>
                                                                    <td>Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/editanimation</td>
                                                                    <td>Animation bearbeiten</td>
                                                                    <td>Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/testanim</td>
                                                                    <td>Animation testen</td>
                                                                    <td>Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/createszenario</td>
                                                                    <td>Neue Szenario hinzufügen</td>
                                                                    <td>Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/createhouseinterior</td>
                                                                    <td>Neues Hausinterior erstellen</td>
                                                                    <td>Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/updatehouseinterior</td>
                                                                    <td>Hausinterior bearbeiten</td>
                                                                    <td>Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/createhouse</td>
                                                                    <td>Ein neues Haus erstellen</td>
                                                                    <td>Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/asellhouse</td>
                                                                    <td>Haus administrativ verkaufen</td>
                                                                    <td>Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/edithouse</td>
                                                                    <td>Ein Haus bearbeiten</td>
                                                                    <td>Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/housecheck</td>
                                                                    <td>Haus überprüfen</td>
                                                                    <td>Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/gethouse</td>
                                                                    <td>Ein Haus zu dir teleportieren</td>
                                                                    <td>Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/createhousedoor - /chd</td>
                                                                    <td>Haus-Tür erstellen</td>
                                                                    <td>Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/createfurniture</td>
                                                                    <td>Neues Möbelstück erstellen</td>
                                                                    <td>Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/furniturecheck</td>
                                                                    <td>Möbelstück überprüfen</td>
                                                                    <td>Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/deletefurniture</td>
                                                                    <td>Möbelstück abbauen/löschen</td>
                                                                    <td>Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/createbizz</td>
                                                                    <td>Ein neues Business erstellen</td>
                                                                    <td>Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/asellbizz</td>
                                                                    <td>Business administrativ verkaufen</td>
                                                                    <td>Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/editbizz</td>
                                                                    <td>Ein Business bearbeiten</td>
                                                                    <td>Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/createbizzdoor - /cbd</td>
                                                                    <td>Business-Tür erstellen</td>
                                                                    <td>Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/bizzcheck</td>
                                                                    <td>Business überprüfen</td>
                                                                    <td>Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/getbizz</td>
                                                                    <td>Ein Business zu dir teleportieren</td>
                                                                    <td>Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/createbusroute</td>
                                                                    <td>Neue Busroute erstellen</td>
                                                                    <td>Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/createbusstation</td>
                                                                    <td>Haltestelle zur Busroute hinzufügen</td>
                                                                    <td>Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/creategarbageroute</td>
                                                                    <td>Neue Müllroute erstellen</td>
                                                                    <td>Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/createtaxiposition</td>
                                                                    <td>Neue Taxiposition erstellen</td>
                                                                    <td>Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/creategangzone</td>
                                                                    <td>Neue Gangzone erstellen</td>
                                                                    <td>Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/updategangzone</td>
                                                                    <td>Gangzone aktualisieren</td>
                                                                    <td>Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/createvehicle</td>
                                                                    <td>Ein Fahrzeug zu einem Business hinzufügen</td>
                                                                    <td>Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/deleteallveh2</td>
                                                                    <td>Alle Adminfahrzeuge löschen</td>
                                                                    <td>Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/createfactionvehicle - /cfv</td>
                                                                    <td>Fraktionsfahrzeug erstellen</td>
                                                                    <td>Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/deletefactionvehicle - /dfv</td>
                                                                    <td>Fraktionsfahrzeug löschen</td>
                                                                    <td>Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/changefactionvehiclecolor - /cfvc</td>
                                                                    <td>Fraktionsfahrzeugfarbe anpassen</td>
                                                                    <td>Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/createfactiondoor - /cfd</td>
                                                                    <td>Fraktionstür erstellen</td>
                                                                    <td>Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/closestdoorhash - /cdh</td>
                                                                    <td>Tür Hash ermitteln</td>
                                                                    <td>Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/gotodoor</td>
                                                                    <td>Zu einer Tür teleportieren</td>
                                                                    <td>Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/vehicleengine</td>
                                                                    <td>Fahrzeug einen Motorschaden geben</td>
                                                                    <td>Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/invisible</td>
                                                                    <td>Unsichtbar machen</td>
                                                                    <td>Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/agl</td>
                                                                    <td>Lizenzen vergeben</td>
                                                                    <td>Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/aggl</td>
                                                                    <td>Gruppierungs/Firmenlizenzen vergeben</td>
                                                                    <td>Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/eventucn</td>
                                                                    <td>Event Undercover Identität annehmen/ablegen</td>
                                                                    <td>Administrator</td>
                                                                </tr>
                                                            </tbody>
                                                            <tbody v-if="admin >= 5">
                                                                <tr>
                                                                    <td>/aban</td>
                                                                    <td>Anticheat Ban</td>
                                                                    <td>High Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/screenshot</td>
                                                                    <td>Screenshot von einem Spieler anfertigen</td>
                                                                    <td>High Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/devmodus</td>
                                                                    <td>Devmodus aktivieren/deaktivieren</td>
                                                                    <td>High Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/sonic</td>
                                                                    <td>Sonicmodus aktivieren/deaktivieren</td>
                                                                    <td>High Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/rtune</td>
                                                                    <td>Fahrzeug zufällig tunen lassen</td>
                                                                    <td>High Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/funmodus</td>
                                                                    <td>Funmodus Modus aktivieren/deaktivieren</td>
                                                                    <td>High Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/saveall</td>
                                                                    <td>Den kompletten Server speichern</td>
                                                                    <td>High Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/createfire</td>
                                                                    <td>Feuerwehreinsatz erstellen</td>
                                                                    <td>High Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/startfire</td>
                                                                    <td>Feuerwehreinsatz starten</td>
                                                                    <td>High Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/startlotto</td>
                                                                    <td>Lottoausschüttung starten</td>
                                                                    <td>High Administrator</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>/clearchat</td>
                                                                    <td>Chatverlauf löschen</td>
                                                                    <td>High Administrator</td>
                                                                </tr>
                                                            </tbody>
                                                            <tbody v-if="admin >= 6">
                                                                <tr>
                                                                    <td>/restart</td>
                                                                    <td>Server neustarten</td>
                                                                    <td>Manager</td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
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
        <div style="height: 100%; background-color: transparent;" v-if="showStadthallenMenu3">
            <div class="row justify-content-center centering4">
                <div class="col-md-12 mt-1 animate__animated animate__bounceIn">
                    <div class="col-md-12 mt-1">
                        <div class="box box-default">
                            <div class="row">
                                <div class="card card-primary card-outline">
                                    <div class="card-header" style="font-family: 'Exo', sans-serif; font-size: 1.2vw">
                                        Firmenlizenzen
                                    </div>
                                    <div class="card-body" style="max-height:50vh; width: 25vw; overflow-x: auto">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card2 card card-primary card-outline text-center">
                                                    <div class="settext ml-2" style="cursor:pointer" @click="getLic(1)">
                                                        Speditionslizenz beantragen <span
                                                            class="badge badge-primary mb-1">{{prices[9]}}$</span>
                                                    </div>
                                                </div>
                                                <div class="card2 card card-primary card-outline text-center">
                                                    <div class="settext ml-2" style="cursor:pointer" @click="getLic(2)">
                                                        Tuninglizenz beantragen <span
                                                            class="badge badge-primary mb-1">{{prices[10]}}$</span>
                                                    </div>
                                                </div>
                                                <div class="card2 card card-primary card-outline text-center">
                                                    <div class="settext ml-2" style="cursor:pointer" @click="getLic(3)">
                                                        Mechatronikerlizenz beantragen <span
                                                            class="badge badge-primary mb-1">{{prices[11]}}$</span>
                                                    </div>
                                                </div>
                                                <div class="card2 card card-primary card-outline text-center">
                                                    <div class="settext ml-2" style="cursor:pointer" @click="getLic(4)">
                                                        Personenbeförderungslizenz beantragen <span
                                                            class="badge badge-primary mb-1">{{prices[12]}}$</span>
                                                    </div>
                                                </div>
                                                <div class="card2 card card-primary card-outline text-center">
                                                    <div class="settext ml-2" style="cursor:pointer" @click="getLic(5)">
                                                        Sicherheitslizenz beantragen <span
                                                            class="badge badge-primary mb-1">{{prices[13]}}$</span>
                                                    </div>
                                                </div>
                                                <div class="card2 card card-primary card-outline text-center">
                                                    <div class="settext ml-2" style="cursor:pointer"
                                                        @click="hideMenu3()">
                                                        Zurück
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
        <div style="height: 100%; background-color: transparent;" v-if="showTabMenu">
            <div class="row justify-content-center centering4">
                <div class="col-md-12 mt-1 animate__animated animate__bounceInUp">
                    <div class="col-md-12 mt-1">
                        <div class="box box-default">
                            <div class="row">
                                <div class="card card-primary card-outline">
                                    <div class="card-header" style="font-family: 'Exo', sans-serif; font-size: 1.2vw">
                                        <span>Spielerübersicht - {{tabMenu.length}} Spieler {{getOnlineStatus()}}
                                            online</span>
                                    </div>
                                    <div class="card-body" style="max-height:50vh; width: 85vw; overflow-x: auto">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card2 card card-primary card-outline text-center">
                                                    <div class="table-responsive-md mt-2 mr-2 ml-2">
                                                        <table class="table table-bordered" style="width:100%">
                                                            <thead class="table-primary ">
                                                                <tr id="tablehead" class="tablehead">
                                                                    <th>ID</th>
                                                                    <th>Name</th>
                                                                    <th>Level</th>
                                                                    <th>Ping</th>
                                                                    <th>Aktion</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr v-for="tab in tabMenu" :key="tab.id">
                                                                    <td>{{tab.id}}</td>
                                                                    <td v-if="!tab.admin">{{tab.name}}</td>
                                                                    <td v-else style="color: #ffcccb">{{tab.name}}</td>
                                                                    <td>{{tab.level}}</td>
                                                                    <td>{{tab.ping}}ms</td>
                                                                    <td style="text-align: center"><i
                                                                            class="nav-icon icon fa-solid fa-user-xmark"
                                                                            @click="reportPlayer(tab.id)"></i></td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
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
        <div style="height: 100%; background-color: transparent;" v-if="showCarSettingUi">
            <div class="row justify-content-center centering4">
                <div class="col-md-12 mt-1 animate__animated animate__bounceInUp">
                    <div class="col-md-12 mt-1">
                        <div class="box box-default">
                            <div class="row">
                                <div class="card card-primary card-outline">
                                    <div class="card-header" style="font-family: 'Exo', sans-serif; font-size: 1.2vw">
                                        <span>Fahrzeugverwaltung</span>
                                    </div>
                                    <div class="card-body" style="max-height:45vh; width: 85vw; overflow-x: auto">
                                        <div class="row">
                                            <div class="col-md-3 col-md-offset-1-and-half" v-for="vehicle in vehicles"
                                                :key="vehicle.id">
                                                <div class="card card-primary card-outline mr-2 ml-1"
                                                    style="min-width: 150px !important;min-height: 285px;max-height:285px !important">
                                                    <div class="card-body">
                                                        <h3 class="profile-username text-center mt-3">
                                                            {{vehicle.vehiclename}}[{{vehicle.id}}]
                                                        </h3>
                                                        <p class="text-muted text-center">
                                                            <b style="color:green"
                                                                v-if="vehicle.plate.length > 3">Zugelassen</b>
                                                            <b style="color:red" v-if="vehicle.plate.length <= 3">Nicht
                                                                zugelassen</b>
                                                        </p>
                                                        <p class="text-muted text-center"
                                                            v-if="vehicle.plate.length > 3">
                                                            <b> <strong>{{vehicle.plate}}</strong></b>
                                                        </p>
                                                    </div>
                                                    <button type="button" class="btn btn-primary mr-3 ml-3 mb-2"
                                                        @click="registerVehicle1(vehicle.id)"
                                                        v-if="vehicle.plate.length <= 3">Anmelden
                                                        (Normales
                                                        Kennzeichen) - 500$</button>
                                                    <button type="button" class="btn btn-primary mr-3 ml-3 mb-2"
                                                        @click="registerVehicle2(vehicle.id)"
                                                        v-if="vehicle.plate.length <= 3 && premium > 0">Anmelden
                                                        (Individuelles
                                                        Kennzeichen) - 750$</button>
                                                    <button type="button" class="btn btn-primary mr-3 ml-3 mb-2"
                                                        @click="unregisterVehicle(vehicle.id)"
                                                        v-if="vehicle.plate.length > 3">Abmelden</button>
                                                    <button type="button" class="btn btn-primary mr-3 ml-3 mb-2"
                                                        @click="changeVehicle(vehicle.id)">Fahrzeug umschreiben -
                                                        350$</button>
                                                    <button type="button" class="btn btn-primary mr-3 ml-3 mb-2"
                                                        @click="keyVehicle(vehicle.id)">Schlüssel nachmachen lassen -
                                                        175$</button>
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
        <div style="height: 100%; background-color: transparent; overflow-x: hidden" v-if="showCenterList">
            <div class="row justify-content-center centering4">
                <div class="col-md-12 mt-1 animate__animated animate__bounceInUp">
                    <div class="col-md-12 mt-1">
                        <div class="box box-default">
                            <div class="row">
                                <div class="card card-primary card-outline">
                                    <div class="card-header" style="font-family: 'Exo', sans-serif; font-size: 1.2vw">
                                        <span>{{centerHeader}}</span>
                                        <input
                                            v-if="centerHeader.toLowerCase().includes('garage') || centerHeader.toLowerCase().includes('verwahrplatz')"
                                            type="text" class="form-control mt-1" v-bind:value="searchelement"
                                            v-on:input="searchelement = $event.target.value" placeholder="Suche"
                                            maxlength="60">
                                    </div>
                                    <div class="card-body" style="max-height:66vh; width: 85vw; overflow-x: hidden">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card2 card card-primary card-outline text-center">
                                                    <div class="table-responsive-md mr-2 ml-2">
                                                        <table class="table table-bordered" style="width:100%">
                                                            <thead class="table-primary ">
                                                                <tr id="tablehead" class="tablehead">
                                                                    <th v-for="(rule, index) in rules" :key="index">
                                                                        {{rule}}</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody style="overflow-x: auto; max-height: 5vw;">
                                                                <tr v-for="(obj, index) in filteredList3" :key="index">
                                                                    <td v-if="obj.var1">{{obj.var1}}</td>
                                                                    <td v-else>{{index}}</td>
                                                                    <td v-if="obj.var2">{{obj.var2}}</td>
                                                                    <td v-if="obj.var3 && centerHeader != 'Führungszeugnis'">{{obj.var3}}</td>
                                                                    <td v-else>{{timeConverter(obj.var3)}}</td>
                                                                    <td
                                                                        v-if="obj.var4 && !centerHeader.toLowerCase().includes('garage') && !centerHeader.toLowerCase().includes('verwahrplatz')">
                                                                        {{obj.var4}}
                                                                    </td>
                                                                    <td v-if="obj.var4 && (centerHeader.toLowerCase().includes('garage') || centerHeader.toLowerCase().includes('verwahrplatz'))"
                                                                        style="text-align: center"><i
                                                                            class="nav-icon icon2 fa-solid fa-solid fa-car"
                                                                            @click="exitGarage(obj.var4)"></i>
                                                                    </td>
                                                                    <td v-if="obj.var5">{{obj.var5}}</td>
                                                                    <td v-if="obj.var6">{{obj.var6}}</td>
                                                                    <td v-if="obj.var7">{{obj.var7}}</td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div
                                        v-if="(centerHeader.toLowerCase().includes('garage') || centerHeader.toLowerCase().includes('verwahrplatz')) && !centerHeader.toLowerCase().includes('admingarage')">
                                        <div class="col-md-8">
                                        <button class="btn btn-block btn-primary btn-sm mb-2 mt-2 float-right" style="width: 50%; color:white" type="submit"
                                            v-on:click="enterGarage()">Fahrzeug einparken</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div v-if="saltyChatError" class="centering2" style="height: 100%;">
            <div class="row justify-content-center centering4">
                <div class="col-md-12 mt-1">
                    <div class="col-md-12 mt-1">
                        <div class="box box-default">
                            <div class="row">
                                <div class="card card-primary card-outline">
                                    <div class="card-header" style="font-family: 'Exo', sans-serif; font-size: 1.2vw">
                                        SaltyChat ist offline!
                                    </div>
                                    <div class="card-body" style="max-height:50vh; width: 25vw; overflow-x: auto">
                                        Um auf diesem Server spielen zu können, benötigst du Teamspeak³ + SaltyChat
                                        (Teamspeak³
                                        Plugin)
                                        in
                                        der Version <strong style="color: #3F6791">2.3.6</strong>, du kannst das Plugin
                                        auf unserer
                                        Webseite unter <strong
                                            style="color: #3F6791">https://saltychat.nemesus-world.de</strong>
                                        herunterladen!
                                        <br /><br />
                                        In unserem Forum auf <strong
                                            style="color: #3F6791">https://forum.nemesus-world.de</strong>
                                        findest du ein Tutorial zur Installation von SaltyChat, solltest du weiterhin
                                        Probleme
                                        haben,
                                        melde dich gerne bei uns!
                                        <br />
                                        <hr />
                                        <div class="text-center">
                                            <p style="color:white">Homepage/Forum: https://nemesus-world.de</p>
                                            <p style="color:white">UCP: https://ucp.nemesus-world.de</p>
                                            <p style="color:white">Discord: https://discord.nemesus-world.de</p>
                                            <p style="color:white">Teamspeak: https://ts3.nemesus-world.de</p>
                                            <p style="color:white">Download: https://saltychat.nemesus-world.de</p>
                                            <p style="color:white">Nemesus.de: https://nemesus.de</p>
                                            <p style="color:white">Trinkgeld: https://trinkgeld.nemesus.de</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div v-if="showafk" class="centering2" style="height: 100%; background-color: rgba(69, 77, 85, .8);">
            <div class="row justify-content-center centering4">
                <div class="col-md-12 mt-1">
                    <div class="col-md-12 mt-1">
                        <div class="box box-default">
                            <div class="row">
                                <div class="card card-primary card-outline">
                                    <div class="card-header" style="font-family: 'Exo', sans-serif; font-size: 1.2vw">
                                        Du bist AFK!
                                    </div>
                                    <div class="card-body" style="max-height:50vh; width: 25vw; overflow-x: auto">
                                        Du wurdest vom System als <strong style="color: #3F6791">AFK</strong>
                                        identifiziert, den
                                        AFK Modus kannst du mit der 'ESC' Taste beenden!
                                        <br /><br />
                                        In unserem Forum auf <strong
                                            style="color: #3F6791">https://forum.nemesus-world.de</strong>
                                        findest du nützliche Informationen und Neuigkeiten rund um Nemesus World!
                                        <br />
                                        <hr />
                                        <div class="text-center">
                                            <p style="color:white">Homepage/Forum: https://nemesus-world.de</p>
                                            <p style="color:white">UCP: https://ucp.nemesus-world.de</p>
                                            <p style="color:white">Discord: https://discord.nemesus-world.de</p>
                                            <p style="color:white">Teamspeak: https://ts3.nemesus-world.de</p>
                                            <p style="color:white">Nemesus.de: https://nemesus.de</p>
                                            <p style="color:white">Trinkgeld: https://trinkgeld.nemesus.de</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div v-if="showping" class="centering2" style="height: 100%; background-color: rgba(69, 77, 85, .8);">
            <div class="row justify-content-center centering4">
                <div class="col-md-12 mt-1">
                    <div class="col-md-12 mt-1">
                        <div class="box box-default">
                            <div class="row">
                                <div class="card card-primary card-outline">
                                    <div class="card-header" style="font-family: 'Exo', sans-serif; font-size: 1.2vw">
                                        Dein Ping ist zu hoch!
                                    </div>
                                    <div class="card-body" style="max-height:50vh; width: 25vw; overflow-x: auto">
                                        Dein <strong style="color: #3F6791">Ping</strong> ist zu hoch, bitte versuche
                                        diesen zu
                                        verringern!
                                        <br /><br />
                                        In unserem Forum auf <strong
                                            style="color: #3F6791">https://forum.nemesus-world.de</strong>
                                        findest du Tipps wie man seinen Ping verbessern kann, deaktiviere auch
                                        Anwendungen
                                        welche deine Internetleitung beanspruchen!
                                        <br />
                                        <hr />
                                        <div class="text-center">
                                            <p style="color:white">Homepage/Forum: https://nemesus-world.de</p>
                                            <p style="color:white">UCP: https://ucp.nemesus-world.de</p>
                                            <p style="color:white">Discord: https://discord.nemesus-world.de</p>
                                            <p style="color:white">Teamspeak: https://ts3.nemesus-world.de</p>
                                            <p style="color:white">Nemesus.de: https://nemesus.de</p>
                                            <p style="color:white">Trinkgeld: https://trinkgeld.nemesus.de</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
        <div style="height: 100%; background-color: transparent;" v-if="showStadthallenMenu2">
            <div class="row justify-content-center centering4">
                <div class="col-md-12 mt-1 animate__animated animate__bounceIn">
                    <div class="col-md-12 mt-1">
                        <div class="box box-default">
                            <div class="row">
                                <div class="card card-primary card-outline">
                                    <div class="card-header" style="font-family: 'Exo', sans-serif; font-size: 1.2vw">
                                        Offene Stellenanzeigen
                                    </div>
                                    <div class="card-body" style="max-height:50vh; width: 25vw; overflow-x: auto">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card2 card card-primary card-outline text-center">
                                                    <div class="settext ml-2" style="cursor:pointer"
                                                        @click="getJob(-1)">
                                                        Arbeitslosengeld beantragen
                                                    </div>
                                                </div>
                                                <div class="card2 card card-primary card-outline text-center">
                                                    <div class="settext ml-2" style="cursor:pointer"
                                                        @click="getJob(-2)">
                                                        <span style="color:#FFCCCB">Arbeitslosengeld kündigen</span>
                                                    </div>
                                                </div>
                                                <div class="card2 card card-primary card-outline text-center">
                                                    <div class="settext ml-2" style="cursor:pointer" @click="getJob(1)">
                                                        Spediteur
                                                    </div>
                                                </div>
                                                <div class="card2 card card-primary card-outline text-center">
                                                    <div class="settext ml-2" style="cursor:pointer" @click="getJob(2)">
                                                        Jäger
                                                    </div>
                                                </div>
                                                <div class="card2 card card-primary card-outline text-center">
                                                    <div class="settext ml-2" style="cursor:pointer" @click="getJob(3)">
                                                        Busfahrer
                                                    </div>
                                                </div>
                                                <div class="card2 card card-primary card-outline text-center">
                                                    <div class="settext ml-2" style="cursor:pointer" @click="getJob(4)">
                                                        Müllmann
                                                    </div>
                                                </div>
                                                <div class="card2 card card-primary card-outline text-center">
                                                    <div class="settext ml-2" style="cursor:pointer" @click="getJob(5)">
                                                        Kanalreiniger
                                                    </div>
                                                </div>
                                                <div class="card2 card card-primary card-outline text-center">
                                                    <div class="settext ml-2" style="cursor:pointer" @click="getJob(6)">
                                                        Taxifahrer
                                                    </div>
                                                </div>
                                                <div class="card2 card card-primary card-outline text-center">
                                                    <div class="settext ml-2" style="cursor:pointer" @click="getJob(7)">
                                                        Landwirt
                                                    </div>
                                                </div>
                                                <div class="card2 card card-primary card-outline text-center">
                                                    <div class="settext ml-2" style="cursor:pointer" @click="getJob(8)">
                                                        Geldlieferant
                                                    </div>
                                                </div>
                                                <div class="card2 card card-primary card-outline text-center">
                                                    <div class="settext ml-2" style="cursor:pointer" @click="getJob(0)">
                                                        <span style="color:#FFCCCB">Job kündigen</span>
                                                    </div>
                                                </div>
                                                <div class="card2 card card-primary card-outline text-center">
                                                    <div class="settext ml-2" style="cursor:pointer"
                                                        @click="hideMenu2()">
                                                        Zurück
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
        <div style="height: 100%; background-color: transparent;" v-if="showStadthallenMenu">
            <div class="row justify-content-center centering4">
                <div class="col-md-12 mt-1 animate__animated animate__bounceIn">
                    <div class="col-md-12 mt-1">
                        <div class="box box-default">
                            <div class="row">
                                <div class="card card-primary card-outline">
                                    <div class="card-header" style="font-family: 'Exo', sans-serif; font-size: 1.2vw">
                                        Stadthalle
                                    </div>
                                    <div class="card-body" style="max-height:50vh; width: 25vw; overflow-x: auto">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card2 card card-primary card-outline text-center">
                                                    <div class="settext ml-2" style="cursor:pointer"
                                                        @click="showJobs()">
                                                        Offene Stellenanzeigen (Jobs)
                                                    </div>
                                                </div>
                                                <div class="card2 card card-primary card-outline text-center">
                                                    <div class="settext ml-2" style="cursor:pointer"
                                                        @click="showVehicle()">
                                                        Fahrzeugverwaltung
                                                    </div>
                                                </div>
                                                <div class="card2 card card-primary card-outline text-center">
                                                    <div class="settext ml-2" style="cursor:pointer"
                                                        @click="createCompany()">
                                                        Neue Gruppierung eröffnen <span
                                                            class="badge badge-primary mb-1">{{prices[5]}}$</span>
                                                    </div>
                                                </div>
                                                <div class="card2 card card-primary card-outline text-center">
                                                    <div class="settext ml-2" style="cursor:pointer"
                                                        @click="nameCompany()">
                                                        Gruppierung umbenennen <span
                                                            class="badge badge-primary mb-1">{{prices[6]}}$</span>
                                                    </div>
                                                </div>
                                                <div class="card2 card card-primary card-outline text-center">
                                                    <div class="settext ml-2" style="cursor:pointer"
                                                        @click="deleteCompany()">
                                                        Gruppierung schließen
                                                    </div>
                                                </div>
                                                <div class="card2 card card-primary card-outline text-center">
                                                    <div class="settext ml-2" style="cursor:pointer"
                                                        @click="firmaCompany()">
                                                        Gruppierung als Firma setzen <span
                                                            class="badge badge-primary mb-1">{{prices[7]}}$</span>
                                                    </div>
                                                </div>
                                                <div class="card2 card card-primary card-outline text-center">
                                                    <div class="settext ml-2" style="cursor:pointer"
                                                        @click="firmaService()">
                                                        Firma als Service eintragen/austragen <span
                                                            class="badge badge-primary mb-1">{{prices[8]}}$</span>
                                                    </div>
                                                </div>
                                                <div class="card2 card card-primary card-outline text-center">
                                                    <div class="settext ml-2" style="cursor:pointer"
                                                        @click="showLizenzen()">
                                                        Firmenlizenzen
                                                    </div>
                                                </div>
                                                <div class="card2 card card-primary card-outline text-center">
                                                    <div class="settext ml-2" style="cursor:pointer"
                                                        @click="hideMenu()">
                                                        Abbrechen
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
    require('../assets/adminlte/plugins/summernote/custom.css');
    export default {
        name: 'Menu',
        data: function() {
            return {
                //Hacking
                showhack: false,
                hacktimer: null,
                terminalarray: ['?', '?', '?', '?', '?', '?'],
                terminaltext: 'STARTING HACKINGKIT v.5.3 ...',
                terms: [
                    "STARTING HACKINGKIT v.5.3 ...",
                    "SSL HANDSHAKE NEMESUS.DE",
                    "STARTING PROGRESS ...",
                    "$(function(){\n\t$.database.get('Password')\n});",
                    "exec({\n\t'Password' : 'kbnrp2013c31f2',\n\t'Username' : 'Administrator'\n});",
                    ">>> Command Returned 2",
                    ">>> Return 0;",
                    "## Server Connected",
                    "## Server Disconnected",
                    "## Connection Refused",
                    "ACCESS DENIED",
                    "ACCESS GRANTED",
                    "Password: ********\n>>> Incorrect Password\n## Server Disconnected",
                    "\n\nNMAP\n\n\tIP: 198.162.0.24\n\tPlatform: Linux armv7\n\tUsers Connected: 27\n\tNetmask: 255.255.255.0\n\t0 flags up\n\n\t",
                    "Reconnecting...",
                    ">>> Reconnecting again...",
                    "/bin ~ # sudo chown /system/",
                    "$.system.on();\n\t[SYSTEM]:Antivirus Protocol Overridden\n\t[SERVER]:Firewall Disabled\n\t",
                    "[ROOT]:Filesystem Formatted",
                    "oauth: a2gnworld2023af3g8",
                    "GREP FOR VENDING CODE",
                    "GETTING AND SETTING VENDING CODE ...",
                    "EXIT ..."
                ],
                //Voicerp
                voicerp: 1,
                //Shop
                showshop: false,
                shopCoins: 0,
                //FAQ
                level: 1,
                percentage: 10,
                showfaq: false,
                faqlist: [1, 0, 0, 0, 0, 0, 0, 0, 0, 0],
                //Ping
                showping: false,
                //AFK
                showafk: false,
                //GOV
                showgov: false,
                govsetting: 0,
                govvalues1: [],
                govvalues2: [],
                govvalues3: [],
                //Payday
                showpayday: false,
                showpayday2: false,
                paydays: [],
                selectedpayday: [],
                total: 0,
                timestamp: 0,
                //Ticket
                oocName: '',
                ticketAnswers: '',
                tickets: '',
                ticketTempText: '',
                ticketTempText2: '',
                selectedTicket: '',
                ticketCount: 0,
                menushowtticket: 0,
                ticketText: '',
                ticketTitel: '',
                ticketPrio: 'low',
                //Faction
                faction: 0,
                factioninfo: [],
                factionmembers: [],
                factionrangs: [],
                factionsalarys: [],
                factionbudget: 0,
                factionmemberscount: [],
                factionvehiclecount: [],
                factionleader: '',
                factionrang: 1,
                //Menu
                animations: [],
                geworben: 'Keiner',
                menushowfaction: false,
                menushowfactionsettings: false,
                showCarSettingUi: false,
                premium: 0,
                vehicles: [],
                centerHeader: '',
                centerList: [],
                rules: [],
                showCenterList: false,
                showTab: [],
                showTabMenu: false,
                saltyChatError: false,
                menushow: false,
                menushowcharacter: false,
                menushowsettings: false,
                menushowhelp: false,
                menushowadminhelp: false,
                menushowjob: false,
                menushowfactionhelp: false,
                menushowhouse: false,
                menushowgroup: false,
                menushowgroupsettings: false,
                menushowlog: false,
                menushowcars: false,
                menushowcarsold: '',
                menushowlichelp: false,
                houseblip: 40,
                mietpreis: 0,
                characterinfos: [],
                accountinfos: [],
                factionname: '',
                rangname: '',
                jobname: '',
                group: -1,
                houseinfos: null,
                tenants: [],
                interiorart: 'Klein',
                bizzinfos: null,
                getmoney: 0,
                produktpreis: 25,
                setmultiplier: 1.0,
                menubizzshow: false,
                lastcheck0: 0,
                lastcheck: 0,
                lastcheck2: 0,
                lastcheck3: 0,
                lastcheck4: 0,
                lastcheck5: 0,
                lastcheck6: 0,
                lastcheck7: 0,
                lastcheck8: 0,
                lastcheck9: 0,
                lastcheck10: 0,
                lastcheck11: 0,
                lastcheck12: 0,
                lastcheck13: 0,
                lastcheck14: 0,
                lastcheck15: 0,
                lastcheck16: 0,
                lastcheck17: 0,
                lastcheck18: 0,
                lastcheck19: 0,
                warntext: '',
                admin: 0,
                adminduty: false,
                job: 0,
                name: 'n/A',
                newname: '',
                bizzname: '',
                groupinfo: [],
                groupmembers: [],
                grouprangs: [],
                groupmemberscount: [],
                groupvehiclescount: [],
                groupleader: 'n/A',
                groupstatus: 'Gruppierung',
                grouprang: 1,
                groups: [],
                groupprovision: 0,
                nextlevelExp: 1,
                myid: -1,
                prices: [],
                showStadthallenMenu: false,
                showStadthallenMenu2: false,
                showStadthallenMenu3: false,
                licenses: [],
                logentry: [],
                grouplog1entry: [],
                grouplog2entry: [],
                grouplog3entry: [],
                grouplog4entry: [],
                grouplog5entry: [],
                lastlog: 'n/A',
                logname: 'n/A',
                searchelement: '',
                clicked: (Date.now() / 1000),
                windowWidth: window.innerWidth,
                crosshair: 1,
                autologin: 0,
                setarmor: 0,
                gutschein: ''
            }
        },
        computed: {
            filteredList() {
                return this.filter1(this.logentry)
            },
            filteredList2() {
                return this.filter2(this.tickets)
            },
            filteredList3() {
                return this.filter3(this.centerList)
            }
        },
        methods: {
            setvoicerp: function(voicerp) {
                this.voicerp = voicerp;
            },
            startHack: function(secs) {
                let count = 1;
                this.terminalarray = ['?', '?', '?', '?', '?', '?'];
                this.terminaltext = 'STARTING HACKINGKIT v.5.3 ...';
                this.showhack = true;
                this.hacktimer = setInterval(() => {
                    if (count < 23) {
                        // eslint-disable-next-line no-undef
                        mp.trigger('Client:PlayHackBeep');
                        this.terminaltext += '<br>' + this.terms[count];
                        if (count == 4) this.terminalarray[0] = Math.floor(Math.random() * 9);
                        if (count == 8) this.terminalarray[1] = Math.floor(Math.random() * 9);
                        if (count == 12) this.terminalarray[2] = Math.floor(Math.random() * 9);
                        if (count == 16) this.terminalarray[3] = Math.floor(Math.random() * 9);
                        if (count == 19) this.terminalarray[4] = Math.floor(Math.random() * 9);
                        if (count == 21) this.terminalarray[5] = Math.floor(Math.random() * 9);
                        count++;
                    }
                    if (count == 23) {
                        if (this.hacktimer) {
                            clearInterval(this.hacktimer);
                        }
                        var self = this;
                        setTimeout(function() {
                            self.stopHack(1);
                        }, 1250);
                    }
                }, secs);
            },
            stopHack: function(set) {
                this.showhack = false;
                if (this.hacktimer) {
                    clearInterval(this.hacktimer)
                }
                this.terminalarray = ['?', '?', '?', '?', '?', '?'];
                // eslint-disable-next-line no-undef
                mp.trigger('Client:StopHack', set);
            },
            getShop: function(id) {
                if ((Date.now() / 1000) > this.clicked) {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:GetShop', id);
                    this.clicked = (Date.now() / 1000) + (2);
                }
            },
            saveGeworben: function(name) {
                if ((Date.now() / 1000) > this.clicked) {
                    this.lastcheck0 = 0;
                    this.menushowcharacter = false;
                    this.menushow = false;
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:SetGeworben', name);
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:ShowHud');
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:HideMenu');
                    this.clicked = (Date.now() / 1000) + (2);
                }
            },
            setafk: function() {
                this.showafk = !this.showafk;
            },
            setping: function() {
                this.showping = !this.showping;
            },
            saveGov: function(modus) {
                this.showgov = false;
                if (modus == 1) {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:SaveGov', this.govvalues1.join(','), 1);
                } else if (modus == 2) {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:SaveGov', this.govvalues2.join(','), 2);
                } else if (modus == 3) {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:SaveGov', this.govvalues3.join(','), 3);
                }
            },
            showGov: function(p0, p1, p2) {
                this.showgov = !this.showgov;
                if (this.showgov == true) {
                    this.govsetting = 0;
                    this.govvalues1 = p0.split(',');
                    this.govvalues2 = p1.split(',');
                    this.govvalues3 = p2.split(',');
                }
            },
            setPayday: function(total, timestamp, id) {
                mp.trigger('Client:GetPaydayText', id);
                this.total = total;
                this.timestamp = timestamp;
            },
            showPaydayText: function(text) {
                this.selectedpayday = JSON.parse(text);
                this.showpayday2 = false;
                this.showpayday = true;
            },
            updateAnimations: function() {
                if ((Date.now() / 1000) > this.clicked) {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:UpdateAnimations', JSON.stringify(this.animations));
                    this.clicked = (Date.now() / 1000) + (2);
                }
            },
            //Ticketsystem
            getTicketCount: function(ticketCount) {
                this.ticketCount = ticketCount;
            },
            changePrio: function(prio) {
                this.ticketPrio = prio;
            },
            startTicket: function() {
                this.menushowhelp = false;
                this.menushowcars = false;
                this.menushowsettings = false;
                this.menushowjob = false;
                this.menushowfactionhelp = false;
                this.menushowadminhelp = false;
                this.menubizzshow = false;
                this.menushowhouse = false;
                this.menushowfaction = false;
                this.menushowfactionsettings = false;
                this.menushowgroup = false;
                this.menushowgroupsettings = false;
                this.menushowlog = false;
                this.menushowlichelp = false;
                this.menushowcharacter = false;
                this.showshop = false;
                this.showpayday2 = false;
                this.showpayday = false;
                this.showfaq = false;
                this.ticketTitel = '';
                this.ticketPrio = 'low';
                if (this.menushowtticket != 1) {
                    this.menushowtticket = 1;
                } else {
                    this.menushowtticket = 0;
                }
                // eslint-disable-next-line no-undef
                $('#summernote').text("");
                setTimeout(function() {
                    // eslint-disable-next-line no-undef
                    $('.summernote').summernote({
                        toolbar: [
                            ['style', ['style']],
                            ['font', ['bold', 'italic', 'subscript', 'superscript', 'clear']],
                            ['para', ['ol', 'ul', 'paragraph']],
                            ['color', ['color']],
                            ['insert', ['link', 'picture', 'video', 'hr']],
                            ['view', ['help']]
                        ],
                        height: 190,
                        width: "100%",
                        minHeight: null,
                        maxHeight: null,
                        dialogsInBody: true,
                        lang: "de-DE",
                        disableResizeEditor: false
                    }).summernote('lineHeight', 0.5);
                    // eslint-disable-next-line no-undef
                    var imageUploadDiv = $('div.note-group-select-from-files');
                    if (imageUploadDiv.length) {
                        imageUploadDiv.remove();
                    }
                }, 10);
            },
            myTickets: function() {
                if ((Date.now() / 1000) > this.clicked) {
                    this.menushowhelp = false;
                    this.menushowcars = false;
                    this.menushowsettings = false;
                    this.menushowjob = false;
                    this.menushowfactionhelp = false;
                    this.menushowadminhelp = false;
                    this.menubizzshow = false;
                    this.menushowhouse = false;
                    this.menushowfaction = false;
                    this.menushowfactionsettings = false;
                    this.menushowgroup = false;
                    this.menushowgroupsettings = false;
                    this.menushowlog = false;
                    this.menushowlichelp = false;
                    this.menushowcharacter = false;
                    this.showshop = false;
                    this.showpayday2 = false;
                    this.showpayday = false;
                    this.showfaq = false;
                    this.searchelement = '';
                    this.clicked = (Date.now() / 1000) + (1);
                    if ((this.lastcheck13 == 0 || (Date.now() / 1000) > this.lastcheck13 || !this.tickets)) {
                        // eslint-disable-next-line no-undef
                        mp.trigger('Client:LoadAllTickets', 1);
                        this.lastcheck13 = (Date.now() / 1000) + (60);
                    } else {
                        if (this.menushowtticket != 2) {
                            this.menushowtticket = 2;
                        } else {
                            this.menushowtticket = 0;
                        }
                    }
                }
            },
            getAllTickets: function(tickets, check) {
                this.tickets = JSON.parse(tickets);
                var self = this;
                if(check == 1)
                {
                    setTimeout(function() {
                        self.menushowtticket = 2;
                    }, 115);
                }
            },
            showTicket: function(ticket) {
                if ((Date.now() / 1000) > this.clicked) {
                    this.selectedTicket = ticket;
                    this.ticketTempText = '';
                    this.ticketTempText2 = '';
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:LoadTicketAnswers', this.selectedTicket.id);
                    this.clicked = (Date.now() / 1000) + (2);
                }
            },
            getTicket: function(answer, oocname) {
                const base64regex = /^([0-9a-zA-Z+/]{4})*(([0-9a-zA-Z+/]{2}==)|([0-9a-zA-Z+/]{3}=))?$/;
                this.oocName = oocname;
                this.ticketAnswers = JSON.parse(answer);
                try {
                    if (base64regex.test(this.selectedTicket.text)) {
                        this.selectedTicket.text = window.atob(this.selectedTicket.text);
                    }
                    this.ticketAnswers.forEach(element => element.text = window.atob(element.text));
                } catch (e) {
                    null;
                }
                var self = this;
                self.menushowtticket = 3;
                setTimeout(function() {
                    // eslint-disable-next-line no-undef
                    $('.summernote').summernote({
                        toolbar: [
                            ['style', ['style']],
                            ['font', ['bold', 'italic', 'subscript', 'superscript', 'clear']],
                            ['para', ['ol', 'ul', 'paragraph']],
                            ['color', ['color']],
                            ['insert', ['link', 'picture', 'video', 'hr']],
                            ['view', ['help']]
                        ],
                        height: 190,
                        width: "100%",
                        minHeight: null,
                        maxHeight: null,
                        dialogsInBody: true,
                        lang: "de-DE",
                        disableResizeEditor: false
                    }).summernote('lineHeight', 0.5);
                    // eslint-disable-next-line no-undef
                    $('.note-video-clip').each(function() {
                        // eslint-disable-next-line no-undef
                        var tmp = $(this).wrap('<p/>').parent().html();
                        // eslint-disable-next-line no-undef
                        $(this).parent().html(
                            '<div class="embed-responsive embed-responsive-16by9" style="max-width: 600px">' +
                            tmp +
                            '</div>');
                    });
                    // eslint-disable-next-line no-undef
                    $(document).ready(function() {
                        // eslint-disable-next-line no-undef
                        $("img").addClass("img-fluid");
                    });
                    // eslint-disable-next-line no-undef
                    var imageUploadDiv = $('div.note-group-select-from-files');
                    if (imageUploadDiv.length) {
                        imageUploadDiv.remove();
                    }
                }, 10);
            },
            updateTicket: function(status) {
                if ((status == 1 || this.lastcheck14 == 0 || (Date.now() / 1000) > this.lastcheck14)) {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:UpdateTicket', this.selectedTicket.id);
                    this.lastcheck14 = (Date.now() / 1000) + (30);
                    if (status == 0) {
                        // eslint-disable-next-line no-undef
                        mp.trigger('Client:SendNotificationWithoutButton2', 'Ticket aktualisiert!', 'success',
                            'center', 1250);
                    }
                } else {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:SendNotificationWithoutButton2',
                        'Du kannst das Ticket nur alle 30 Sekunden aktualisieren!', 'error', 'center', 1500);
                }
            },
            ticketUpdate: function(answer, ticket) {
                this.ticketAnswers = JSON.parse(answer);
                this.selectedTicket = JSON.parse(ticket);
                try {
                    this.selectedTicket.text = window.atob(this.selectedTicket.text);
                    this.ticketAnswers.forEach(element => element.text = window.atob(element.text));
                } catch (e) {
                    null;
                }
                setTimeout(function() {
                    // eslint-disable-next-line no-undef
                    $('.summernote').summernote({
                        toolbar: [
                            ['style', ['style']],
                            ['font', ['bold', 'italic', 'subscript', 'superscript', 'clear']],
                            ['para', ['ol', 'ul', 'paragraph']],
                            ['color', ['color']],
                            ['insert', ['link', 'picture', 'video', 'hr']],
                            ['view', ['help']]
                        ],
                        height: 190,
                        width: "100%",
                        minHeight: null,
                        maxHeight: null,
                        dialogsInBody: true,
                        lang: "de-DE",
                        disableResizeEditor: false
                    }).summernote('lineHeight', 0.5);
                    // eslint-disable-next-line no-undef
                    $('.note-video-clip').each(function() {
                        // eslint-disable-next-line no-undef
                        var tmp = $(this).wrap('<p/>').parent().html();
                        // eslint-disable-next-line no-undef
                        $(this).parent().html(
                            '<div class="embed-responsive embed-responsive-16by9" style="max-width: 600px">' +
                            tmp +
                            '</div>');
                    });
                    // eslint-disable-next-line no-undef
                    $(document).ready(function() {
                        // eslint-disable-next-line no-undef
                        $("img").addClass("img-fluid");
                    });
                    // eslint-disable-next-line no-undef
                    var imageUploadDiv = $('div.note-group-select-from-files');
                    if (imageUploadDiv.length) {
                        imageUploadDiv.remove();
                    }
                }, 10);
            },
            getTicketStatus: function(status) {
                var name = "Warte auf Bearbeitung";
                if (status == 0) name = "Warte auf Bearbeitung";
                else if (status == 1) name = "In Bearbeitung";
                else if (status == 2) name = "Abgeschlossen";
                else if (status == 9) name = "Archiviert";
                return name;
            },
            changeStatus: function(status) {
                if ((Date.now() / 1000) > this.clicked) {
                    var ticketttext = this.selectedTicket.id + '|' + status + '|' + this.selectedTicket.status +
                        '|' + this.selectedTicket.admin;
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:TicketSystem', 'changeStatus', ticketttext);
                    this.clicked = (Date.now() / 1000) + (2);
                    this.lastcheck13 = 0;
                    var self = this;
                    setTimeout(function() {
                        self.updateTicket(1);
                        self.ticketTempText = '';
                        self.ticketTempText2 = '';
                    }, 375);
                }
            },
            answerTicket: function() {
                if ((Date.now() / 1000) > this.clicked) {
                    if ((this.lastcheck15 == 0 || (Date.now() / 1000) > this.lastcheck15)) {
                        // eslint-disable-next-line no-undef
                        var ticketttext = this.selectedTicket.id + '|' + $('#summernote').val();
                        // eslint-disable-next-line no-undef
                        mp.trigger('Client:TicketSystem', 'answer', ticketttext);
                        this.clicked = (Date.now() / 1000) + (2);
                        this.lastcheck13 = 0;
                        var self = this;
                        setTimeout(function() {
                            self.updateTicket(1);
                            self.ticketTempText = '';
                            self.ticketTempText2 = '';
                            // eslint-disable-next-line no-undef
                            $('#summernote').text("");
                            // eslint-disable-next-line no-undef
                            $('#summernote').val("");
                            self.ticketttext = '';
                        }, 375);
                        this.lastcheck15 = (Date.now() / 1000) + (10);
                    } else {
                        // eslint-disable-next-line no-undef
                        mp.trigger('Client:SendNotificationWithoutButton2',
                            'Du musst noch kurz warten, bevor du auf das Ticket wieder antworten kannst!',
                            'error', 'center', 1250);
                    }
                }
            },
            addUser: function() {
                if ((Date.now() / 1000) > this.clicked) {
                    var ticketttext = this.selectedTicket.id + '|' + this.ticketTempText;
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:TicketSystem', 'adduser', ticketttext);
                    this.clicked = (Date.now() / 1000) + (2);
                    this.lastcheck13 = 0;
                    var self = this;
                    setTimeout(function() {
                        self.updateTicket(1);
                        self.ticketTempText = '';
                        self.ticketTempText2 = '';
                    }, 375);
                }
            },
            setEditor: function() {
                if ((Date.now() / 1000) > this.clicked) {
                    var ticketttext = this.selectedTicket.id + '|' + this.ticketTempText2 + '|' + this
                        .selectedTicket.admin;
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:TicketSystem', 'setEditor', ticketttext);
                    this.clicked = (Date.now() / 1000) + (2);
                    this.lastcheck13 = 0;
                    var self = this;
                    setTimeout(function() {
                        self.updateTicket(1);
                        self.ticketTempText = '';
                        self.ticketTempText2 = '';
                    }, 375);
                }
            },
            finishTicket: function() {
                if ((Date.now() / 1000) > this.clicked) {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:TicketSystem', 'finish', this.selectedTicket.id);
                    this.clicked = (Date.now() / 1000) + (2);
                    this.lastcheck13 = 0;
                    var self = this;
                    setTimeout(function() {
                        self.updateTicket(1);
                        self.ticketTempText = '';
                        self.ticketTempText2 = '';
                    }, 375);
                }
            },
            createTicket: function() {
                if ((Date.now() / 1000) > this.clicked) {
                    if (this.ticketTitel.length >= 3) {
                        // eslint-disable-next-line no-undef
                        var ticketttext = this.ticketTitel + '|' + this.ticketPrio + '|' + $('#summernote').val();
                        // eslint-disable-next-line no-undef
                        mp.trigger('Client:TicketSystem', 'create', ticketttext);
                        this.clicked = 0;
                        this.lastcheck13 = 0;
                        var self = this;
                        setTimeout(function() {
                            self.myTickets();
                        }, 375);
                    }
                }
            },
            factionInvite: function() {
                if (this.lastcheck4 == 0 || (Date.now() / 1000) > this.lastcheck4) {
                    this.lastcheck4 = (Date.now() / 1000) + (2);
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:FactionSettings', 'invite', '0');
                }
            },
            enterGarage() {
                if ((Date.now() / 1000) > this.clicked) {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:HouseSettings', 'entergarage', 1);
                    this.clicked = (Date.now() / 1000) + (1);
                }
            },
            exitGarage(id) {
                if ((Date.now() / 1000) > this.clicked) {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:HouseSettings', 'exitgarage', id);
                    this.clicked = (Date.now() / 1000) + (1);
                }
            },
            registerVehicle1(id) {
                if ((Date.now() / 1000) > this.clicked) {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:VehicleSettings', 'register1', id);
                    this.clicked = (Date.now() / 1000) + (2);
                }
            },
            registerVehicle2(id) {
                if ((Date.now() / 1000) > this.clicked) {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:VehicleSettings', 'register2', id);
                    this.clicked = (Date.now() / 1000) + (2);
                }
            },
            unregisterVehicle(id) {
                if ((Date.now() / 1000) > this.clicked) {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:VehicleSettings', 'unregister', id);
                    this.clicked = (Date.now() / 1000) + (2);
                }
            },
            changeVehicle(id) {
                if ((Date.now() / 1000) > this.clicked) {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:VehicleSettings', 'changevehicle', id);
                    this.clicked = (Date.now() / 1000) + (2);
                }
            },
            keyVehicle(id) {
                if ((Date.now() / 1000) > this.clicked) {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:VehicleSettings', 'keyvehicle', id);
                    this.clicked = (Date.now() / 1000) + (2);
                }
            },
            showCarSetting(vehicles, premium) {
                this.showCarSettingUi = !this.showCarSettingUi;
                if (this.showCarSettingUi == true) {
                    this.vehicles = JSON.parse(vehicles);
                    this.premium = premium;
                }
            },
            setCarRang(id, rang, owner) {
                if ((Date.now() / 1000) > this.clicked) {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:SetCarRang', id, rang, owner);
                    this.clicked = (Date.now() / 1000) + (2);
                }
            },
            setCarName(id, name, owner) {
                if ((Date.now() / 1000) > this.clicked) {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:SetCarName', id, name, owner);
                    this.clicked = (Date.now() / 1000) + (2);
                    this.$forceUpdate();
                }
            },
            gpsCar(position, carid) {
                if ((Date.now() / 1000) > this.clicked) {
                    let pos = position.split('|');
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:CreateWaypoint', pos[0], pos[1], carid);
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:SendNotificationWithoutButton', 'Fahrzeug wurde geortet!', 'success', 'top-left',
                        2750);
                    this.clicked = (Date.now() / 1000) + (2);
                }
            },
            reportPlayer(id) {
                if ((Date.now() / 1000) > this.clicked) {
                    // eslint-disable-next-line no-undef
                    this.showTabMenu = false;
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:ReportPlayer', id);
                    this.clicked = (Date.now() / 1000) + (2);
                }
            },
            getOnlineStatus() {
                return this.tabMenu.length != 1 ? "sind" : "ist";
            },
            showCenterMenu(rules, json, header) {
                this.searchelement = '';
                this.showCenterList = !this.showCenterList;
                if (this.showCenterList == true) {
                    this.centerHeader = header;
                    this.centerList = JSON.parse(json);
                    this.rules = rules.split(',');
                }
            },
            showTabList(json) {
                // eslint-disable-next-line no-undef
                mp.trigger('Client:StopAllNotifications');
                this.showTabMenu = !this.showTabMenu;
                if (this.showTabMenu == true) {
                    this.tabMenu = JSON.parse(json);
                }
            },
            selectCrosshair(crosshair) {
                this.crosshair = crosshair;
                this.showMenu();
                // eslint-disable-next-line no-undef
                mp.trigger('Client:SelectCrosshair', crosshair);
            },
            selectWalkingStyle() {
                this.showMenu();
                // eslint-disable-next-line no-undef
                mp.trigger('Client:SelectWalkingStyle');
            },
            getCrosshair(crosshair) {
                return require('../assets/images/crosshair/' + crosshair + '.png')
            },
            selectSetting(setting) {
                if ((Date.now() / 1000) > this.clicked) {
                    var settingsvalue = 0;
                    if (setting == 1) {
                        if (this.autologin == 0) {
                            this.autologin = 1;
                            settingsvalue = 1;
                        } else {
                            this.autologin = 0;
                            settingsvalue = 0;
                        }
                    } else if (setting == 3) {
                        settingsvalue = this.setarmor;
                    } else if (setting == 4) {
                        settingsvalue = this.gutschein;
                    }
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:SelectSetting', setting, settingsvalue);
                    this.clicked = (Date.now() / 1000);
                }
            },
            showSaltyError() {
                this.saltyChatError = !this.saltyChatError;
            },
            filter1: function(logentry) {
                return logentry.filter(log => {
                    return log.text.toLowerCase().includes(this.searchelement.toLowerCase())
                })
            },
            filter2: function(logentry) {
                return logentry.filter(t => {
                    return t.title.toLowerCase().includes(this.searchelement.toLowerCase())
                })
            },
            filter3: function(centerlist) {
                return centerlist.filter(c => {
                    return c.var2.toLowerCase().includes(this.searchelement.toLowerCase())
                })
            },
            getSkillName: function(skill) {
                let retSkill = 'Anfänger';
                if (skill <= 1) {
                    retSkill = 'Anfänger';
                } else if (skill == 2) {
                    retSkill = 'Erfahrener';
                } else if (skill == 3) {
                    retSkill = 'Profi';
                } else if (skill == 4) {
                    retSkill = 'Meister';
                } else {
                    retSkill = 'Experte';
                }
                return retSkill;
            },
            timeConverter: function(UNIX_timestamp) {
                var a = new Date(UNIX_timestamp * 1000);
                var months = ['Jan.', 'Feb.', 'Mär.', 'Apr.', 'Mai', 'Jun.', 'Jul.', 'Aug.', 'Sep.', 'Okt.', 'Nov.',
                    'Dez.'
                ];
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
            showCharacterStats: function(json1, json2, factionname, rangname, jobname, json3) {
                this.menushow = true;
                this.menushowtticket = 0;
                this.menushowhelp = false;
                this.menushowcars = false;
                this.showpayday = false;
                this.showpayday2 = false;
                this.showfaq = false;
                this.showshop = false;
                this.menushowsettings = false;
                this.menushowjob = false;
                this.menushowfactionhelp = false;
                this.menushowadminhelp = false;
                this.menubizzshow = false;
                this.menushowhouse = false;
                this.menushowfaction = false;
                this.menushowfactionsettings = false;
                this.menushowgroup = false;
                this.menushowgroupsettings = false;
                this.menushowlog = false;
                this.menushowlichelp = false;
                this.menushowcharacter = !this.menushowcharacter;
                this.characterinfos = JSON.parse(json1);
                this.licenses = this.characterinfos.licenses.split('|');
                this.factionname = factionname;
                this.rangname = rangname;
                this.jobname = jobname;
                this.accountinfos = JSON.parse(json2);
                this.warntext = this.accountinfos.warns_text.split("|");
                this.groups = JSON.parse(json3);
                this.nextlevelExp = (this.accountinfos.level + 1) * 4;
                return;
            },
            startStats: function() {
                if (this.lastcheck0 == 0 || (Date.now() / 1000) > this.lastcheck0 || !this.characterinfos) {
                    this.lastcheck0 = (Date.now() / 1000) + (180);
                    this.geworben = 'Keiner';
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:StartStats');
                    return;
                } else {
                    this.menushowtticket = 0;
                    this.menushowhelp = false;
                    this.menushowcars = false;
                    this.showpayday = false;
                    this.showpayday2 = false;
                    this.showfaq = false;
                    this.showshop = false;
                    this.menushowsettings = false;
                    this.menushowjob = false;
                    this.menushowfactionhelp = false;
                    this.menushowadminhelp = false;
                    this.menubizzshow = false;
                    this.menushowhouse = false;
                    this.menushowfaction = false;
                    this.menushowfactionsettings = false;
                    this.menushowgroup = false;
                    this.menushowgroupsettings = false;
                    this.menushowlog = false;
                    this.menushowlichelp = false;
                    this.geworben = 'Keiner';
                    this.menushowcharacter = !this.menushowcharacter;
                }
            },
            showPaydays: function(json) {
                this.menushowtticket = 0;
                this.menushowhelp = false;
                this.menushowsettings = false;
                this.menushowjob = false;
                this.menushowfactionhelp = false;
                this.menushowadminhelp = false;
                this.menubizzshow = false;
                this.menushowhouse = false;
                this.menushowfaction = false;
                this.menushowfactionsettings = false;
                this.menushowgroup = false;
                this.menushowgroupsettings = false;
                this.menushowlog = false;
                this.menushowlichelp = false;
                this.menushowcharacter = false;
                this.menushowcars = false;
                this.showfaq = false;
                this.showshop = false;
                this.paydays = JSON.parse(json);
                this.showpayday = false;
                this.showpayday2 = !this.showpayday2;
                return;
            },
            showFAQ: function(csv, percent) {
                this.menushowtticket = 0;
                this.menushowhelp = false;
                this.menushowsettings = false;
                this.menushowjob = false;
                this.menushowfactionhelp = false;
                this.menushowadminhelp = false;
                this.menubizzshow = false;
                this.menushowhouse = false;
                this.menushowfaction = false;
                this.menushowfactionsettings = false;
                this.menushowgroup = false;
                this.menushowgroupsettings = false;
                this.menushowlog = false;
                this.menushowlichelp = false;
                this.menushowcharacter = false;
                this.menushowcars = false;
                this.showpayday = false;
                this.showpayday2 = false;
                this.showshop = false;
                this.faqlist = csv.split(',');
                this.percentage = percent;
                this.showfaq = !this.showfaq;
                return;
            },
            showCoins: function(getCoins) {
                this.shopCoins = getCoins;
            },
            startPaydays: function() {
                if (this.lastcheck18 == 0 || (Date.now() / 1000) > this.lastcheck18) {
                    this.lastcheck18 = (Date.now() / 1000) + (30);
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:StartPaydays');
                    return;
                } else {
                    this.menushowtticket = 0;
                    this.menushowhelp = false;
                    this.menushowsettings = false;
                    this.menushowjob = false;
                    this.menushowfactionhelp = false;
                    this.menushowadminhelp = false;
                    this.menubizzshow = false;
                    this.menushowhouse = false;
                    this.menushowfaction = false;
                    this.menushowfactionsettings = false;
                    this.menushowgroup = false;
                    this.menushowgroupsettings = false;
                    this.menushowlog = false;
                    this.menushowlichelp = false;
                    this.menushowcharacter = false;
                    this.menushowcars = false;
                    this.showfaq = false;
                    this.showshop = false;
                    this.showpayday = false;
                    this.showpayday2 = !this.showpayday2;
                }
            },
            startFAQ: function() {
                if (this.lastcheck19 == 0 || (Date.now() / 1000) > this.lastcheck19) {
                    this.lastcheck19 = (Date.now() / 1000) + (60);
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:StartFAQ');
                    return;
                } else {
                    this.menushowtticket = 0;
                    this.menushowhelp = false;
                    this.menushowsettings = false;
                    this.menushowjob = false;
                    this.menushowfactionhelp = false;
                    this.menushowadminhelp = false;
                    this.menubizzshow = false;
                    this.menushowhouse = false;
                    this.menushowfaction = false;
                    this.menushowfactionsettings = false;
                    this.menushowgroup = false;
                    this.menushowgroupsettings = false;
                    this.menushowlog = false;
                    this.menushowlichelp = false;
                    this.menushowcharacter = false;
                    this.menushowcars = false;
                    this.showpayday = false;
                    this.showpayday2 = false;
                    this.showshop = false;
                    this.showfaq = !this.showfaq;
                }
            },
            startShop: function() {
                this.menushowtticket = 0;
                this.menushowhelp = false;
                this.menushowsettings = false;
                this.menushowjob = false;
                this.menushowfactionhelp = false;
                this.menushowadminhelp = false;
                this.menubizzshow = false;
                this.menushowhouse = false;
                this.menushowfaction = false;
                this.menushowfactionsettings = false;
                this.menushowgroup = false;
                this.menushowgroupsettings = false;
                this.menushowlog = false;
                this.menushowlichelp = false;
                this.menushowcharacter = false;
                this.menushowcars = false;
                this.showpayday = false;
                this.showpayday2 = false;
                this.showfaq = false;
                this.showshop = !this.showshop
            },
            showCars: function(json) {
                this.menushowtticket = 0;
                this.menushowhelp = false;
                this.menushowsettings = false;
                this.showpayday = false;
                this.showpayday2 = false;
                this.menushowjob = false;
                this.menushowfactionhelp = false;
                this.menushowadminhelp = false;
                this.menubizzshow = false;
                this.menushowhouse = false;
                this.menushowfaction = false;
                this.menushowfactionsettings = false;
                this.menushowgroup = false;
                this.menushowgroupsettings = false;
                this.menushowlog = false;
                this.menushowlichelp = false;
                this.menushowcharacter = false;
                this.showfaq = false;
                this.showshop = false;
                this.cars = JSON.parse(json);
                this.menushowcars = !this.menushowcars;
                return;
            },
            startCars: function(input) {
                if (this.lastcheck0 == 0 || (Date.now() / 1000) > this.lastcheck0 || input != this
                    .menushowcarsold) {
                    this.lastcheck0 = (Date.now() / 1000) + (180);
                    if (input != this.menushowcarsold) {
                        this.menushowcars = false;
                    }
                    this.menushowcarsold = input;
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:StartCars', input);
                    return;
                } else {
                    this.menushowtticket = 0;
                    this.menushowhelp = false;
                    this.menushowsettings = false;
                    this.menushowjob = false;
                    this.showpayday = false;
                    this.showpayday2 = false;
                    this.showfaq = false;
                    this.showshop = false;
                    this.menushowfactionhelp = false;
                    this.menushowadminhelp = false;
                    this.menubizzshow = false;
                    this.menushowhouse = false;
                    this.menushowfaction = false;
                    this.menushowfactionsettings = false;
                    this.menushowgroup = false;
                    this.menushowgroupsettings = false;
                    this.menushowlog = false;
                    this.menushowlichelp = false;
                    this.menushowcharacter = false;
                    this.menushowcars = !this.menushowcars;
                }
            },
            startSettings: function() {
                if (this.lastcheck8 == 0 || (Date.now() / 1000) > this.lastcheck8) {
                    this.lastcheck8 = (Date.now() / 1000) + (30);
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:StartSettings');
                    return;
                } else {
                    this.menushowtticket = 0;
                    this.menushowhelp = false;
                    this.menushowcars = false;
                    this.showpayday = false;
                    this.showpayday2 = false;
                    this.menushowjob = false;
                    this.showfaq = false;
                    this.showshop = false;
                    this.menushowfactionhelp = false;
                    this.menushowadminhelp = false;
                    this.menubizzshow = false;
                    this.menushowhouse = false;
                    this.menushowfaction = false;
                    this.menushowfactionsettings = false;
                    this.menushowgroup = false;
                    this.menushowgroupsettings = false;
                    this.menushowlog = false;
                    this.menushowlichelp = false;
                    this.menushowcharacter = false;
                    this.menushowcars = false;
                    this.menushowsettings = !this.menushowsettings;
                }
            },
            showSettingsMenu: function(crosshair, autologin, armor, animationshotkeys) {
                this.menushowtticket = 0;
                this.menushowhelp = false;
                this.menushowcars = false;
                this.menushowjob = false;
                this.showpayday = false;
                this.showpayday2 = false;
                this.showfaq = false;
                this.showshop = false;
                this.menushowfactionhelp = false;
                this.menushowadminhelp = false;
                this.menubizzshow = false;
                this.menushowhouse = false;
                this.menushowfaction = false;
                this.menushowfactionsettings = false;
                this.menushowgroup = false;
                this.menushowgroupsettings = false;
                this.menushowlog = false;
                this.menushowlichelp = false;
                this.menushowcharacter = false;
                this.menushowcars = false;
                this.crosshair = crosshair;
                this.autologin = autologin;
                this.setarmor = armor;
                if (animationshotkeys) {
                    try {
                        this.animations = JSON.parse(animationshotkeys);
                    } catch (e) {
                        this.animations = animationshotkeys;
                    }
                }
                this.menushowsettings = !this.menushowsettings;
            },
            startFaction: function() {
                if (this.faction <= 0) return;
                if (this.lastcheck10 == 0 || (Date.now() / 1000) > this.lastcheck10) {
                    this.lastcheck10 = (Date.now() / 1000) + (30);
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:StartFaction');
                    return;
                } else {
                    this.menushowtticket = 0;
                    this.menushowcharacter = false;
                    this.menushowhelp = false;
                    this.menushowcars = false;
                    this.showpayday = false;
                    this.showpayday2 = false;
                    this.menushowjob = false;
                    this.showfaq = false;
                    this.showshop = false;
                    this.menushowfactionhelp = false;
                    this.menushowsettings = false;
                    this.menushowadminhelp = false;
                    this.menubizzshow = false;
                    this.menushowhouse = false;
                    this.menushowlog = false;
                    this.menushowlichelp = false;
                    this.menushowfactionsettings = false;
                    this.menushowgroup = false;
                    this.menushowgroupsettings = false;
                    this.menushowfaction = !this.menushowfaction;
                }
            },
            showFactionStats: function(json1, json2, count, count2, leadername, json3, charid) {
                this.menushowcharacter = false;
                if (count > 0) {
                    this.factioninfo = JSON.parse(json1);
                    this.factionmembers = JSON.parse(json2);
                    this.factionrangs = JSON.parse(json3);
                    this.factionmemberscount = count;
                    this.factionvehiclecount = count2;
                    this.factionleader = leadername;
                    this.myid = charid;
                } else {
                    this.factionmembers = null;
                }
                var self = this;
                setTimeout(function() {
                    self.menushowfaction = !self.menushowfaction;
                    self.menushowfactionsettings = false;
                }, 375);
                return;
            },
            factionSwat: function(charid) {
                if (this.lastcheck4 == 0 || (Date.now() / 1000) > this.lastcheck4) {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:FactionSettings', 'swat', '' + charid);
                    this.lastcheck4 = (Date.now() / 1000) + (2);
                }
            },
            factionUprank: function(charid) {
                if (this.lastcheck4 == 0 || (Date.now() / 1000) > this.lastcheck4) {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:FactionSettings', 'uprank', '' + charid);
                    this.lastcheck4 = (Date.now() / 1000) + (2);
                }
            },
            factionDownrank: function(charid) {
                if (this.lastcheck4 == 0 || (Date.now() / 1000) > this.lastcheck4) {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:FactionSettings', 'downrank', '' + charid);
                    this.lastcheck4 = (Date.now() / 1000) + (2);
                }
            },
            factionKick: function(charid) {
                if (this.lastcheck4 == 0 || (Date.now() / 1000) > this.lastcheck4) {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:FactionSettings', 'kick', '' + charid);
                    this.lastcheck4 = (Date.now() / 1000) + (2);
                }
            },
            updateFactionRang: function(rang, faction) {
                this.factionrang = rang;
                this.faction = faction;
                if (this.faction == 0) {
                    this.showFactionStats = false;
                    if (this.faction < 10) {
                        this.showFactionSettings = false;
                        this.showLog = false;
                    }
                } else {
                    this.menushowfaction = false;
                    this.showFactionSettings = false;
                    this.menushowlog = false;
                }
                if (this.menushow == true) {
                    this.$forceUpdate();
                }
                return;
            },
            factionLog: function() {
                this.menushowtticket = 0;
                this.menushowcharacter = false;
                this.menushowhelp = false;
                this.menushowcars = false;
                this.showpayday = false;
                this.showpayday2 = false;
                this.showfaq = false;
                this.showshop = false;
                this.menushowsettings = false;
                this.menushowlichelp = false;
                this.menushowjob = false;
                this.menushowfactionhelp = false;
                this.menushowhouse = false;
                this.menushowgroup = false;
                this.menushowfaction = false;
                this.menushowfactionsettings = false;
                this.menushowgroupsettings = false;
                if (this.lastcheck11 == 0 || (Date.now() / 1000) > this.lastcheck11) {
                    this.lastcheck11 = (Date.now() / 1000) + (180);
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:FactionSettings', 'factionlog', '0');
                } else {
                    this.logentry = this.grouplog3entry;
                    this.logname = 'Fraktionslog';
                }
                this.menushowlog = !this.menushowlog;
                if (this.lastlog != 'Fraktionslog' && this.lastlog.length > 3 && this.menushowlog == false) {
                    this.menushowlog = true;
                }
                this.lastlog = 'Fraktionslog';
            },
            weaponLog: function() {
                this.menushowtticket = 0;
                this.menushowcharacter = false;
                this.menushowhelp = false;
                this.menushowcars = false;
                this.showpayday = false;
                this.showpayday2 = false;
                this.showfaq = false;
                this.showshop = false;
                this.menushowsettings = false;
                this.menushowlichelp = false;
                this.menushowjob = false;
                this.menushowfactionhelp = false;
                this.menushowhouse = false;
                this.menushowgroup = false;
                this.menushowfaction = false;
                this.menushowfactionsettings = false;
                this.menushowgroupsettings = false;
                if (this.lastcheck16 == 0 || (Date.now() / 1000) > this.lastcheck16) {
                    this.lastcheck16 = (Date.now() / 1000) + (180);
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:FactionSettings', 'weaponlog', '0');
                } else {
                    this.logentry = this.grouplog4entry;
                    this.logname = 'Waffenkammerlog';
                }
                this.menushowlog = !this.menushowlog;
                if (this.lastlog != 'Waffenkammerlog' && this.lastlog.length > 3 && this.menushowlog == false) {
                    this.menushowlog = true;
                }
                this.lastlog = 'Waffenkammerlog';
            },
            asservatenLog: function() {
                this.menushowtticket = 0;
                this.menushowcharacter = false;
                this.menushowhelp = false;
                this.menushowcars = false;
                this.showpayday = false;
                this.showpayday2 = false;
                this.showfaq = false;
                this.showshop = false;
                this.menushowsettings = false;
                this.menushowlichelp = false;
                this.menushowjob = false;
                this.menushowfactionhelp = false;
                this.menushowhouse = false;
                this.menushowgroup = false;
                this.menushowfaction = false;
                this.menushowfactionsettings = false;
                this.menushowgroupsettings = false;
                if (this.lastcheck17 == 0 || (Date.now() / 1000) > this.lastcheck17) {
                    this.lastcheck17 = (Date.now() / 1000) + (180);
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:FactionSettings', 'asservatenlog', '0');
                } else {
                    this.logentry = this.grouplog5entry;
                    this.logname = 'Asservatenkammerlog';
                }
                this.menushowlog = !this.menushowlog;
                if (this.lastlog != 'Asservatenkammerlog' && this.lastlog.length > 3 && this.menushowlog == false) {
                    this.menushowlog = true;
                }
                this.lastlog = 'Asservatenkammerlog';
            },
            factionPhone: function() {
                if (this.lastcheck4 == 0 || (Date.now() / 1000) > this.lastcheck4) {
                    this.lastcheck4 = (Date.now() / 1000) + (2);
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:FactionSettings', 'factionphone', 'n/A');
                }
            },
            factionLeave: function() {
                if (this.lastcheck4 == 0 || (Date.now() / 1000) > this.lastcheck4) {
                    this.lastcheck4 = (Date.now() / 1000) + (2);
                    this.menushow = false;
                    this.menushowfaction = false;
                    this.menushowfactionsettings = false;
                    this.menushowlog = false;
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:FactionSettings', 'leave', '' + this.myid);
                }
            },
            factionRangUpdate: function() {
                if (this.lastcheck == 0 || (Date.now() / 1000) > this.lastcheck) {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:FactionSettings', 'updaterangs', JSON.stringify(this.factionrangs));
                    this.lastcheck = Date.now() / 1000 + (3);
                }
            },
            factionSalaryUpdate: function() {
                if (this.lastcheck == 0 || (Date.now() / 1000) > this.lastcheck) {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:FactionSettings', 'factionsalary', JSON.stringify(this.factionsalarys));
                    this.lastcheck = Date.now() / 1000 + (3);
                }
            },
            showFactionSettings: function(json, json2, budget, update) {
                if (update == 0) {
                    this.menushowfactionsettings = !this.menushowfactionsettings;
                    this.factionbudget = budget;
                    this.factionrangs = JSON.parse(json);
                    this.factionsalarys = JSON.parse(json2);
                } else {
                    this.factionbudget = budget;
                    this.factionrangs = JSON.parse(json);
                    this.factionsalarys = JSON.parse(json2);
                }
            },
            updateFactionStats: function(json1, json2, count, count2, leadername, json3, charid) {
                if (count > 0) {
                    this.factioninfo = JSON.parse(json1);
                    this.factionmembers = JSON.parse(json2);
                    this.factionrangs = JSON.parse(json3);
                    this.factionmemberscount = count;
                    this.factionvehiclecount = count2;
                    this.factionleader = leadername;
                    this.myid = charid;
                } else {
                    this.groupmembers = null;
                }
                this.$forceUpdate();
                return;
            },
            getRang2: function(rang) {
                switch (rang) {
                    case 0:
                        return 'Kein Rang';
                    case 1:
                        return this.factionrangs.rang1;
                    case 2:
                        return this.factionrangs.rang2;
                    case 3:
                        return this.factionrangs.rang3;
                    case 4:
                        return this.factionrangs.rang4;
                    case 5:
                        return this.factionrangs.rang5;
                    case 6:
                        return this.factionrangs.rang6;
                    case 7:
                        return this.factionrangs.rang7;
                    case 8:
                        return this.factionrangs.rang8;
                    case 9:
                        return this.factionrangs.rang9;
                    case 10:
                        return this.factionrangs.rang10;
                    case 11:
                        return this.factionrangs.rang11;
                    case 12:
                        return this.factionrangs.rang12;
                }
            },
            startFactionSettings: function() {
                this.menushowtticket = 0;
                this.menushowcharacter = false;
                this.menushowhelp = false;
                this.menushowcars = false;
                this.showpayday = false;
                this.showpayday2 = false;
                this.showfaq = false;
                this.showshop = false;
                this.menushowsettings = false;
                this.menushowjob = false;
                this.menushowfactionhelp = false;
                this.menushowadminhelp = false;
                this.menubizzshow = false;
                this.menushowhouse = false;
                this.menushowgroup = false;
                this.menushowfaction = false;
                this.menushowgroupsettings = false;
                this.menushowlog = false;
                this.menushowlichelp = false;
                if (this.lastcheck12 == 0 || (Date.now() / 1000) > this.lastcheck12) {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:FactionSettings', 'showfactionrangs', '1');
                    this.lastcheck12 = (Date.now() / 1000) + (180);
                } else {
                    this.menushowfactionsettings = !this.menushowfactionsettings;
                }
            },
            startGroup: function() {
                if (this.lastcheck3 == 0 || (Date.now() / 1000) > this.lastcheck3) {
                    this.lastcheck3 = (Date.now() / 1000) + (180);
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:StartGroup');
                    return;
                } else {
                    this.menushowtticket = 0;
                    this.menushowcharacter = false;
                    this.menushowhelp = false;
                    this.menushowcars = false;
                    this.showpayday = false;
                    this.showpayday2 = false;
                    this.menushowjob = false;
                    this.showfaq = false;
                    this.showshop = false;
                    this.menushowfactionhelp = false;
                    this.menushowsettings = false;
                    this.menushowadminhelp = false;
                    this.menubizzshow = false;
                    this.menushowhouse = false;
                    this.menushowlog = false;
                    this.menushowlichelp = false;
                    this.menushowfaction = false;
                    this.menushowfactionsettings = false;
                    this.menushowgroup = !this.menushowgroup;
                    this.menushowgroupsettings = false;
                }
            },
            showGroupStats: function(json1, json2, count, count2, leadername, status, json3, charid) {
                this.menushowcharacter = false;
                this.menushowfaction = false;
                if (count > 0) {
                    this.groupinfo = JSON.parse(json1);
                    this.groupmembers = JSON.parse(json2);
                    this.grouprangs = JSON.parse(json3);
                    this.groupmemberscount = count;
                    this.groupvehiclescount = count2;
                    this.groupleader = leadername;
                    this.groupstatus = status;
                    this.myid = charid;
                } else {
                    this.groupmembers = null;
                }
                var self = this;
                setTimeout(function() {
                    self.menushowgroup = !self.menushowgroup;
                    self.menushowgroupsettings = false;
                    self.menushowfaction = false;
                }, 375);
                return;
            },
            updateGroupRang: function(rang, group) {
                this.grouprang = rang;
                this.group = group;
                if (this.group == -1) {
                    this.showGroupStats = false;
                    if (this.grouprang < 10) {
                        this.showGroupSettings = false;
                        this.showLog = false;
                    }
                } else {
                    this.menushowgroup = false;
                    this.menushowgroupsettings = false;
                    this.menushowlog = false;
                }
                if (this.menushow == true) {
                    this.$forceUpdate();
                }
                return;
            },
            updateGroupStats: function(json1, json2, count, count2, leadername, status, json3, charid) {
                if (count > 0) {
                    this.groupinfo = JSON.parse(json1);
                    this.groupmembers = JSON.parse(json2);
                    this.groupmemberscount = count;
                    this.groupvehiclescount = count2;
                    this.groupleader = leadername;
                    this.groupstatus = status;
                    this.myid = charid;
                    this.grouprangs = JSON.parse(json3);
                } else {
                    this.groupmembers = null;
                }
                this.$forceUpdate();
                return;
            },
            startGroupSettings: function() {
                this.menushowtticket = 0;
                this.menushowcharacter = false;
                this.menushowhelp = false;
                this.menushowcars = false;
                this.showpayday = false;
                this.showpayday2 = false;
                this.showfaq = false;
                this.showshop = false;
                this.menushowsettings = false;
                this.menushowjob = false;
                this.menushowfactionhelp = false;
                this.menushowadminhelp = false;
                this.menubizzshow = false;
                this.menushowhouse = false;
                this.menushowgroup = false;
                this.menushowfaction = false;
                this.menushowfactionsettings = false;
                this.menushowlog = false;
                this.menushowlichelp = false;
                if (this.lastcheck7 == 0 || (Date.now() / 1000) > this.lastcheck7) {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:GroupSettings', 'showgrouprangs', '1');
                    this.lastcheck7 = (Date.now() / 1000) + (180);
                } else {
                    this.menushowgroupsettings = !this.menushowgroupsettings;
                }
            },
            showGroupSettings: function(json, update, provision) {
                if (update == 0) {
                    this.menushowgroupsettings = !this.menushowgroupsettings;
                    this.grouprangs = JSON.parse(json);
                    this.groupprovision = provision;
                } else {
                    this.grouprangs = JSON.parse(json);
                }
            },
            getRang: function(rang) {
                switch (rang) {
                    case 0:
                        return 'Kein Rang';
                    case 1:
                        return this.grouprangs.rang1;
                    case 2:
                        return this.grouprangs.rang2;
                    case 3:
                        return this.grouprangs.rang3;
                    case 4:
                        return this.grouprangs.rang4;
                    case 5:
                        return this.grouprangs.rang5;
                    case 6:
                        return this.grouprangs.rang6;
                    case 7:
                        return this.grouprangs.rang7;
                    case 8:
                        return this.grouprangs.rang8;
                    case 9:
                        return this.grouprangs.rang9;
                    case 10:
                        return this.grouprangs.rang10;
                    case 11:
                        return this.grouprangs.rang11;
                    case 12:
                        return this.grouprangs.rang12;
                }
            },
            startHouse: function() {
                if (this.lastcheck2 == 0 || (Date.now() / 1000) > this.lastcheck2) {
                    this.lastcheck2 = (Date.now() / 1000) + (1);
                    this.clicked = (Date.now() / 1000);
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:StartHouse');
                    return;
                } else {
                    if (this.menushowhouse == false) {
                        Swal.fire({
                            icon: 'error',
                            title: 'Fehler',
                            text: 'Du musst noch kurz warten, bevor du das Menü neu aufrufen kannst!',
                            showConfirmButton: false,
                            timer: 2500
                        })
                    }
                }
            },
            showBizzStats: function(json1, charactername, setshow) {
                this.menushowtticket = 0;
                this.menushowhelp = false;
                this.menushowcars = false;
                this.showpayday = false;
                this.showpayday2 = false;
                this.showfaq = false;
                this.showshop = false;
                this.menushowsettings = false;
                this.menushowjob = false;
                this.menushowfactionhelp = false;
                this.menushowadminhelp = false;
                this.menushowfaction = false;
                this.menushowfactionsettings = false;
                this.menushowgroup = false;
                this.menushowgroupsettings = false;
                this.menushowlog = false;
                this.menushowhouse = false;
                if (setshow == 1) {
                    this.menubizzshow = !this.menubizzshow;
                }
                this.bizzinfos = JSON.parse(json1);
                this.name = charactername;
                return;
            },
            startBizz: function() {
                if (this.lastcheck2 == 0 || (Date.now() / 1000) > this.lastcheck2) {
                    this.clicked = (Date.now() / 1000);
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:StartBizz');
                    this.lastcheck2 = (Date.now() / 1000) + (1);
                    return;
                } else {
                    if (this.menubizzshow == false) {
                        Swal.fire({
                            icon: 'error',
                            title: 'Fehler',
                            text: 'Du musst noch kurz warten, bevor du das Menü neu aufrufen kannst!',
                            showConfirmButton: false,
                            timer: 2500
                        })
                    }
                }
            },
            IsAFuelStation(bizzid) {
                if (bizzid == 5) {
                    return true;
                }
                return false;
            },
            newBizzKey() {
                if ((Date.now() / 1000) > this.clicked) {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:BizzSettings', 'newbizzkey', 'n/A');
                    this.clicked = (Date.now() / 1000) + (2);
                }
            },
            newBizzDoor() {
                if ((Date.now() / 1000) > this.clicked) {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:BizzSettings', 'newbizzdoor', 'n/A');
                    this.clicked = (Date.now() / 1000) + (2);
                }
            },
            changeBizzName() {
                if ((Date.now() / 1000) > this.clicked) {
                    if (!this.bizzname || this.bizzname.length < 5) {
                        return;
                    }
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:BizzSettings', 'bizzname', this.bizzname);
                    this.clicked = (Date.now() / 1000) + (2);
                }
            },
            changeGetMoney() {
                if ((Date.now() / 1000) > this.clicked) {
                    if (this.getmoney < 0 || this.getmoney > 999999999) {
                        return;
                    }
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:BizzSettings', 'getmoney', this.getmoney);
                    this.clicked = (Date.now() / 1000) + (2);
                }
            },
            changeEigentuemerBizz() {
                if ((Date.now() / 1000) > this.clicked) {
                    if (!this.newname || this.newname.length < 3) {
                        return;
                    }
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:BizzSettings', 'bizzowner', this.newname);
                    this.clicked = (Date.now() / 1000) + (2);
                }
            },
            changeMultiplier() {
                if ((Date.now() / 1000) > this.clicked) {
                    if (!this.setmultiplier) {
                        this.setmultiplier = 0;
                    }
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:BizzSettings', 'multiplier', this.setmultiplier);
                    this.clicked = (Date.now() / 1000) + (2);
                }
            },
            changeProduktpreis() {
                if ((Date.now() / 1000) > this.clicked) {
                    if (!this.produktpreis) {
                        this.produktpreis = 0;
                    }
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:BizzSettings', 'produktpreis', this.produktpreis);
                    this.clicked = (Date.now() / 1000) + (2);
                }
            },
            changeProduktStatus() {
                if ((Date.now() / 1000) > this.clicked) {
                    if (!this.produktpreis) {
                        this.produktpreis = 0;
                    }
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:BizzSettings', 'produktstatus', '1');
                    this.clicked = (Date.now() / 1000) + (2);
                }
            },
            castoutBizz() {
                if ((Date.now() / 1000) > this.clicked) {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:BizzSettings', 'cashout', 'n/A');
                    this.clicked = (Date.now() / 1000) + (2);
                }
            },
            insertBizz() {
                if ((Date.now() / 1000) > this.clicked) {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:BizzSettings', 'insert', 'n/A');
                }
            },
            sellBizz() {
                if ((Date.now() / 1000) > this.clicked) {
                    this.menubizzshow = false;
                    this.menushow = false;
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:ShowHud');
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:HideMenu');
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:BizzSettings', 'sellbizz', 'n/A');
                    this.clicked = (Date.now() / 1000) + (2);
                }
            },
            startMoebel: function() {
                if (this.lastcheck2 == 0 || (Date.now() / 1000) > this.lastcheck2) {
                    this.menushow = false;
                    this.menushowcharacter = false;
                    this.menushowlichelp = false;
                    this.menushowadminhelp = false;
                    this.menubizzshow = false;
                    this.menushowtticket = 0;
                    this.menushowhelp = false;
                    this.menushowcars = false;
                    this.showpayday = false;
                    this.showpayday2 = false;
                    this.showfaq = false;
                    this.showshop = false;
                    this.menushowsettings = false;
                    this.menushowjob = false;
                    this.menushowfactionhelp = false;
                    this.menushowhouse = false;
                    this.menushowfaction = false;
                    this.menushowfactionsettings = false;
                    this.menushowgroup = false;
                    this.menushowgroupsettings = false;
                    this.menushowlog = false;
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:ShowHud');
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:StartMoebel');
                    this.lastcheck2 = (Date.now() / 1000) + (1);
                    this.clicked = (Date.now() / 1000);
                    return;
                } else {
                    if (this.menushowhouse == false) {
                        Swal.fire({
                            icon: 'error',
                            title: 'Fehler',
                            text: 'Du musst noch kurz warten, bevor du das Menü neu aufrufen kannst!',
                            showConfirmButton: false,
                            timer: 2500
                        })
                    }
                }
            },
            showHouseStats: function(json1, interiorart, charactername, json2) {
                this.clicked = (Date.now() / 1000);
                this.menushowtticket = 0;
                this.menushowhelp = false;
                this.menushowcars = false;
                this.showpayday = false;
                this.showpayday2 = false;
                this.showfaq = false;
                this.showshop = false;
                this.menushowsettings = false;
                this.menushowjob = false;
                this.menushowfactionhelp = false;
                this.menushowadminhelp = false;
                this.menubizzshow = false;
                this.menushowfaction = false;
                this.menushowfactionsettings = false;
                this.menushowgroup = false;
                this.menushowgroupsettings = false;
                this.menushowlog = false;
                this.menushowhouse = !this.menushowhouse;
                if (json1.length > 4) {
                    this.houseinfos = JSON.parse(json1);
                    this.mietpreis = this.houseinfos.rental;
                    this.tenants = JSON.parse(json2);
                } else {
                    this.houseinfos = null;
                }
                this.houseblip = this.houseinfos.blip;
                this.interiorart = interiorart;
                this.name = charactername;
                return;
            },
            updateHouseStats: function(json1, interiorart, charactername, json2) {
                if (json1.length > 4) {
                    this.houseinfos = JSON.parse(json1);
                    this.mietpreis = this.houseinfos.rental;
                    this.tenants = JSON.parse(json2);
                } else {
                    this.houseinfos = null;
                }
                this.interiorart = interiorart;
                this.name = charactername;
                return;
            },
            newInterior() {
                if ((Date.now() / 1000) > this.clicked) {
                    Swal.close();
                    this.menushowcharacter = false;
                    this.menushowlichelp = false;
                    this.menushowadminhelp = false;
                    this.menubizzshow = false;
                    this.menushowtticket = 0;
                    this.menushowhelp = false;
                    this.menushowcars = false;
                    this.showpayday = false;
                    this.showpayday2 = false;
                    this.showfaq = false;
                    this.showshop = false;
                    this.menushowsettings = false;
                    this.menushowjob = false;
                    this.menushowfactionhelp = false;
                    this.menushowhouse = false;
                    this.menushowfaction = false;
                    this.menushowfactionsettings = false;
                    this.menushowgroup = false;
                    this.menushowgroupsettings = false;
                    this.menushowlog = false;
                    this.menushow = false;
                    this.clicked = (Date.now() / 1000) + (2);
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:ShowHud');
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:HideMenu');
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:HouseSettings', 'newinterior', 1);
                }
            },
            changeMietpreis() {
                if ((Date.now() / 1000) > this.clicked) {
                    if (!this.mietpreis) {
                        this.mietpreis = 0;
                    }
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:HouseSettings', 'mietpreis', this.mietpreis);
                    this.clicked = (Date.now() / 1000) + (2);
                }
            },
            changeEigentuemer() {
                if ((Date.now() / 1000) > this.clicked) {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:HouseSettings', 'newowner', this.newname);
                    this.clicked = (Date.now() / 1000) + (2);
                }
            },
            newHouseDoor() {
                if ((Date.now() / 1000) > this.clicked) {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:HouseSettings', 'newhousedoor', 1);
                    this.clicked = (Date.now() / 1000) + (2);
                }
            },
            sellHouse() {
                if ((Date.now() / 1000) > this.clicked) {
                    this.menushowhouse = false;
                    this.menushow = false;
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:ShowHud');
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:HideMenu');
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:HouseSettings', 'sellhouse', (this.houseinfos.price / 2));
                    this.clicked = (Date.now() / 1000) + (2);
                }
            },
            kickAllTenants() {
                if ((Date.now() / 1000) > this.clicked) {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:HouseSettings', 'kicktenants', 1);
                    this.clicked = (Date.now() / 1000) + (2);
                }
            },
            newKey() {
                if ((Date.now() / 1000) > this.clicked) {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:HouseSettings', 'newkey', 1);
                    this.clicked = (Date.now() / 1000) + (2);
                }
            },
            groupHouse() {
                if ((Date.now() / 1000) > this.clicked) {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:HouseSettings', 'grouphouse', 1);
                    this.clicked = (Date.now() / 1000) + (2);
                }
            },
            setStock() {
                if ((Date.now() / 1000) > this.clicked) {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:HouseSettings', 'setstock', this.houseinfos.stockprice);
                    this.clicked = (Date.now() / 1000) + (2);
                }
            },
            setBlip() {
                if ((Date.now() / 1000) > this.clicked) {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:HouseSettings', 'setblip', this.houseblip);
                    this.clicked = (Date.now() / 1000) + (2);
                }
            },
            endRent() {
                if ((Date.now() / 1000) > this.clicked) {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:HouseSettings', 'endrent', 1);
                    this.clicked = (Date.now() / 1000) + (2);
                }
            },
            kickTenant(id) {
                if ((Date.now() / 1000) > this.clicked) {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:HouseSettings', 'kicktenant', id);
                    this.clicked = (Date.now() / 1000) + (2);
                }
            },
            changeKlingelschild() {
                if ((Date.now() / 1000) > this.clicked) {
                    if (this.houseinfos.noshield == 0) {
                        this.houseinfos.noshield = 1;
                    } else {
                        this.houseinfos.noshield = 0;
                    }
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:HouseSettings', 'klingelschild', this.houseinfos.noshield);
                    this.$forceUpdate();
                    this.clicked = (Date.now() / 1000) + (2);
                }
            },
            showLicHelp: function() {
                this.menushowcharacter = false;
                this.menushowadminhelp = false;
                this.menubizzshow = false;
                this.menushowhouse = false;
                this.menushowfaction = false;
                this.menushowfactionsettings = false;
                this.menushowgroup = false;
                this.menushowgroupsettings = false;
                this.menushowlog = false;
                this.menushowjob = false;
                this.menushowfactionhelp = false;
                this.menushowtticket = 0;
                this.menushowhelp = false;
                this.menushowcars = false;
                this.showpayday = false;
                this.showpayday2 = false;
                this.showfaq = false;
                this.showshop = false;
                this.menushowsettings = false;
                this.menushowlichelp = !this.menushowlichelp;
            },
            startHelp: function() {
                this.menushowcharacter = false;
                this.menushowadminhelp = false;
                this.menushowlichelp = false;
                this.menubizzshow = false;
                this.menushowhouse = false;
                this.menushowfaction = false;
                this.menushowfactionsettings = false;
                this.menushowgroup = false;
                this.menushowgroupsettings = false;
                this.menushowlog = false;
                this.menushowjob = false;
                this.menushowfactionhelp = false;
                this.menushowsettings = false;
                this.menushowcars = false;
                this.showpayday = false;
                this.showpayday2 = false;
                this.showfaq = false;
                this.showshop = false;
                this.menushowhelp = !this.menushowhelp;
            },
            startAdminHelp: function() {
                this.menushowcharacter = false;
                this.menushowtticket = 0;
                this.menushowhelp = false;
                this.menushowcars = false;
                this.showpayday = false;
                this.showpayday2 = false;
                this.showfaq = false;
                this.showshop = false;
                this.menushowsettings = false;
                this.menushowjob = false;
                this.menushowfactionhelp = false;
                this.menushowhouse = false;
                this.menushowlichelp = false;
                this.menushowfaction = false;
                this.menushowfactionsettings = false;
                this.menushowgroup = false;
                this.menushowgroupsettings = false;
                this.menushowlog = false;
                this.menubizzshow = false;
                this.menushowadminhelp = !this.menushowadminhelp;
            },
            startJobHelp: function() {
                this.menushowcharacter = false;
                this.menushowtticket = 0;
                this.menushowhelp = false;
                this.menushowcars = false;
                this.showpayday = false;
                this.showpayday2 = false;
                this.showfaq = false;
                this.showshop = false;
                this.menushowsettings = false;
                this.menushowlichelp = false;
                this.menushowhouse = false;
                this.menushowfaction = false;
                this.menushowfactionsettings = false;
                this.menushowgroup = false;
                this.menushowgroupsettings = false;
                this.menushowlog = false;
                this.menubizzshow = false;
                this.menushowadminhelp = false;
                this.menushowfactionhelp = false;
                this.menushowjob = !this.menushowjob;
            },
            startFactionHelp: function() {
                this.menushowcharacter = false;
                this.menushowtticket = 0;
                this.menushowhelp = false;
                this.menushowcars = false;
                this.showpayday = false;
                this.showpayday2 = false;
                this.showfaq = false;
                this.showshop = false;
                this.menushowsettings = false;
                this.menushowlichelp = false;
                this.menushowhouse = false;
                this.menushowfaction = false;
                this.menushowfactionsettings = false;
                this.menushowgroup = false;
                this.menushowgroupsettings = false;
                this.menushowlog = false;
                this.menubizzshow = false;
                this.menushowadminhelp = false;
                this.menushowjob = false;
                this.menushowfaction = false;
                this.menushowfactionhelp = !this.menushowfactionhelp;
            },
            showMenu: function(admin, adminduty, group, grouprang, faction, factionrang, job, level, tester) {
                Swal.close();
                // eslint-disable-next-line no-undef
                this.admin = admin;
                this.tester = tester;
                if (adminduty == 0) {
                    this.adminduty = false;
                } else {
                    this.adminduty = true;
                }
                this.group = group;
                this.grouprang = grouprang;
                this.faction = faction;
                this.factionrang = factionrang;
                this.job = job;
                this.level = level;
                this.menushow = !this.menushow;
                // eslint-disable-next-line no-undef
                mp.trigger('Client:ShowHud');
                if (this.menushow == false) {
                    this.menushowcharacter = false;
                    this.menushowadminhelp = false;
                    this.menushowlichelp = false;
                    this.menubizzshow = false;
                    this.menushowtticket = 0;
                    this.menushowhelp = false;
                    this.menushowcars = false;
                    this.showpayday = false;
                    this.showpayday2 = false;
                    this.showfaq = false;
                    this.showshop = false;
                    this.menushowsettings = false;
                    this.menushowjob = false;
                    this.menushowfactionhelp = false;
                    this.menushowhouse = false;
                    this.menushowfaction = false;
                    this.menushowfactionsettings = false;
                    this.menushowgroup = false;
                    this.menushowgroupsettings = false;
                    this.menushowlog = false;
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:HideMenu');
                }
                this.lastcheck2 = 0;
            },
            showStadthalle: function(prices) {
                Swal.close();
                this.lastcheck4 = 0;
                this.prices = prices.split(',');
                this.showStadthallenMenu = !this.showStadthallenMenu;
            },
            hideStadthalle: function() {
                Swal.close();
                this.showStadthallenMenu = false;
                this.showStadthallenMenu2 = false;
                this.showStadthallenMenu3 = false;
                this.lastcheck4 = 0;
            },
            groupSwitch: function(groupid) {
                this.menushowcharacter = false;
                this.menushowadminhelp = false;
                this.menushowlichelp = false;
                this.menubizzshow = false;
                this.menushowtticket = 0;
                this.menushowhelp = false;
                this.menushowcars = false;
                this.showpayday = false;
                this.showpayday2 = false;
                this.showfaq = false;
                this.showshop = false;
                this.menushowsettings = false;
                this.menushowjob = false;
                this.menushowfactionhelp = false;
                this.menushowhouse = false;
                this.menushowfaction = false;
                this.menushowfactionsettings = false;
                this.menushowgroup = false;
                this.menushowgroupsettings = false;
                this.menushowlog = false;
                this.menushow = false;
                this.lastcheck0 = 0;
                this.lastcheck = 0;
                this.lastcheck2 = 0;
                this.lastcheck3 = 0;
                this.lastcheck4 = 0;
                this.lastcheck5 = 0;
                this.lastcheck6 = 0;
                this.lastcheck7 = 0;
                this.lastcheck8 = 0;
                this.lastcheck9 = 0;
                this.lastcheck10 = 0;
                this.lastcheck11 = 0;
                this.lastcheck12 = 0;
                this.lastcheck13 = 0;
                this.lastcheck14 = 0;
                this.lastcheck15 = 0;
                this.lastcheck16 = 0;
                // eslint-disable-next-line no-undef
                mp.trigger('Client:GroupSettings', 'switchgroup', '' + groupid);
                // eslint-disable-next-line no-undef
                mp.trigger('Client:ShowHud');
                // eslint-disable-next-line no-undef
                mp.trigger('Client:HideMenu');
            },
            showLog: function(jsoninput, name) {
                this.logentry = JSON.parse(jsoninput);
                if (name == 'Gruppierungslog') {
                    this.grouplog1entry = this.logentry;
                } else if (name == 'Wirtschaftslog') {
                    this.grouplog2entry = this.logentry;
                } else if (name == 'Fraktionslog') {
                    this.grouplog3entry = this.logentry;
                } else if (name == 'Waffenkammerlog') {
                    this.grouplog4entry = this.logentry;
                } else if (name == 'Asservatenkammerlog') {
                    this.grouplog5entry = this.logentry;
                }
                this.lastlog = name;
                this.logname = name;
            },
            groupRangUpdate: function() {
                if (this.lastcheck == 0 || (Date.now() / 1000) > this.lastcheck) {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:GroupSettings', 'updaterangs', JSON.stringify(this.grouprangs));
                    this.lastcheck = Date.now() / 1000 + (3);
                }
            },
            groupPhone: function() {
                if (this.lastcheck4 == 0 || (Date.now() / 1000) > this.lastcheck4) {
                    this.lastcheck4 = (Date.now() / 1000) + (2);
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:GroupSettings', 'groupphone', 'n/A');
                }
            },
            groupLog1: function() {
                this.menushowcharacter = false;
                this.menushowtticket = 0;
                this.menushowhelp = false;
                this.menushowcars = false;
                this.showpayday = false;
                this.showpayday2 = false;
                this.showfaq = false;
                this.showshop = false;
                this.menushowsettings = false;
                this.menushowlichelp = false;
                this.menushowjob = false;
                this.menushowfactionhelp = false;
                this.menushowhouse = false;
                this.menushowfaction = false;
                this.menushowfactionsettings = false;
                this.menushowgroup = false;
                this.menushowgroupsettings = false;
                if (this.lastcheck5 == 0 || (Date.now() / 1000) > this.lastcheck5) {
                    this.lastcheck5 = (Date.now() / 1000) + (180);
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:GroupSettings', 'grouplog1', '0');
                } else {
                    this.logentry = this.grouplog1entry;
                    this.logname = 'Gruppierungslog';
                }
                this.menushowlog = !this.menushowlog;
                if (this.lastlog != 'Gruppierungslog' && this.lastlog.length > 3 && this.menushowlog == false) {
                    this.menushowlog = true;
                }
                this.lastlog = 'Gruppierungslog';
            },
            groupLog2: function() {
                this.menushowcharacter = false;
                this.menushowtticket = 0;
                this.menushowhelp = false;
                this.menushowcars = false;
                this.showpayday = false;
                this.showpayday2 = false;
                this.showfaq = false;
                this.showshop = false;
                this.menushowsettings = false;
                this.menushowlichelp = false;
                this.menushowjob = false;
                this.menushowfactionhelp = false;
                this.menushowhouse = false;
                this.menushowfaction = false;
                this.menushowfactionsettings = false;
                this.menushowgroup = false;
                this.menushowgroupsettings = false;
                if (this.lastcheck6 == 0 || (Date.now() / 1000) > this.lastcheck6) {
                    this.lastcheck6 = (Date.now() / 1000) + (180);
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:GroupSettings', 'grouplog2', '0');
                } else {
                    this.logentry = this.grouplog2entry;
                    this.logname = 'Wirtschaftslog';
                }
                this.menushowlog = !this.menushowlog;
                if (this.lastlog != 'Wirtschaftslog' && this.lastlog.length > 3 && this.menushowlog == false) {
                    this.menushowlog = true;
                }
                this.lastlog = 'Wirtschaftslog';
            },
            groupProvision: function() {
                if (this.lastcheck4 == 0 || (Date.now() / 1000) > this.lastcheck4) {
                    if (!this.groupprovision || this.groupprovision < 0) {
                        return;
                    }
                    this.lastcheck4 = (Date.now() / 1000) + (2);
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:GroupSettings', 'provision', '' + this.groupprovision);
                }
            },
            groupUprank: function(charid) {
                if (this.lastcheck4 == 0 || (Date.now() / 1000) > this.lastcheck4) {
                    this.lastcheck4 = (Date.now() / 1000) + (2);
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:GroupSettings', 'uprank', '' + charid);
                }
            },
            groupDownrank: function(charid) {
                if (this.lastcheck4 == 0 || (Date.now() / 1000) > this.lastcheck4) {
                    this.lastcheck4 = (Date.now() / 1000) + (2);
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:GroupSettings', 'downrank', '' + charid);
                }
            },
            groupKick: function(charid) {
                if (this.lastcheck4 == 0 || (Date.now() / 1000) > this.lastcheck4) {
                    this.lastcheck4 = (Date.now() / 1000) + (2);
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:GroupSettings', 'kick', '' + charid);
                }
            },
            groupLeave: function() {
                if (this.lastcheck4 == 0 || (Date.now() / 1000) > this.lastcheck4) {
                    this.lastcheck4 = (Date.now() / 1000) + (2);
                    this.menushow = false;
                    this.menushowgroup = false;
                    this.menushowgroupsettings = false;
                    this.menushowlog = false;
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:GroupSettings', 'leave', '' + this.myid);
                }
            },
            groupLeader: function(charid) {
                if (this.lastcheck4 == 0 || (Date.now() / 1000) > this.lastcheck4) {
                    this.lastcheck4 = (Date.now() / 1000) + (2);
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:GroupSettings', 'leader', '' + charid);
                }
            },
            groupMoney: function(charid) {
                if (this.lastcheck4 == 0 || (Date.now() / 1000) > this.lastcheck4) {
                    this.lastcheck4 = (Date.now() / 1000) + (2);
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:GroupSettings', 'money', '' + charid);
                }
            },
            groupInvite: function() {
                if (this.lastcheck4 == 0 || (Date.now() / 1000) > this.lastcheck4) {
                    this.lastcheck4 = (Date.now() / 1000) + (2);
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:GroupSettings', 'invite', '0');
                }
            },
            createCompany: function() {
                this.showStadthallenMenu = false;
                if (this.lastcheck4 == 0 || (Date.now() / 1000) > this.lastcheck4) {
                    this.lastcheck4 = (Date.now() / 1000) + (2);
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:GroupSettings', 'creategroupbefore', 'n/A');
                }
            },
            nameCompany: function() {
                this.showStadthallenMenu = false;
                if (this.lastcheck4 == 0 || (Date.now() / 1000) > this.lastcheck4) {
                    this.lastcheck4 = (Date.now() / 1000) + (2);
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:GroupSettings', 'namecompany', 'n/A');
                }
            },
            deleteCompany: function() {
                this.showStadthallenMenu = false;
                if (this.lastcheck4 == 0 || (Date.now() / 1000) > this.lastcheck4) {
                    this.lastcheck4 = (Date.now() / 1000) + (2);
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:GroupSettings', 'deletecompany', 'n/A');
                }
            },
            firmaCompany: function() {
                this.showStadthallenMenu = false;
                if (this.lastcheck4 == 0 || (Date.now() / 1000) > this.lastcheck4) {
                    this.lastcheck4 = (Date.now() / 1000) + (2);
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:GroupSettings', 'firmacompany', 'n/A');
                }
            },
            firmaService: function() {
                this.showStadthallenMenu = false;
                if (this.lastcheck4 == 0 || (Date.now() / 1000) > this.lastcheck4) {
                    this.lastcheck4 = (Date.now() / 1000) + (2);
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:GroupSettings', 'firmaservice', 'n/A');
                }
            },
            showVehicle: function() {
                this.showStadthallenMenu = false;
                if (this.lastcheck4 == 0 || (Date.now() / 1000) > this.lastcheck4) {
                    this.lastcheck4 = (Date.now() / 1000) + (2);
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:GroupSettings', 'hidemenu', 'n/A');
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:VehicleSettings', 'preshow', 0);
                }
            },
            hideMenu: function() {
                this.showStadthallenMenu = false;
                if (this.lastcheck4 == 0 || (Date.now() / 1000) > this.lastcheck4) {
                    this.lastcheck4 = (Date.now() / 1000) + (2);
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:GroupSettings', 'hidemenu', 'n/A');
                }
            },
            showJobs: function() {
                this.showStadthallenMenu = false;
                this.showStadthallenMenu2 = true;
                this.$forceUpdate();
            },
            showLizenzen: function() {
                this.showStadthallenMenu = false;
                this.showStadthallenMenu3 = true;
            },
            getJob: function(jobid) {
                if (this.lastcheck4 == 0 || (Date.now() / 1000) > this.lastcheck4) {
                    this.lastcheck4 = (Date.now() / 1000) + (2);
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:JobSettings', 'getjob', '' + jobid);
                }
            },
            hideMenu2: function() {
                this.showStadthallenMenu2 = false;
                this.showStadthallenMenu = true;
            },
            getLic: function(licid) {
                if (this.lastcheck4 == 0 || (Date.now() / 1000) > this.lastcheck4) {
                    this.lastcheck4 = (Date.now() / 1000) + (2);
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:GroupSettings', 'getlic', '' + licid);
                }
            },
            hideMenu3: function() {
                this.showStadthallenMenu3 = false;
                this.showStadthallenMenu = true;
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

    .note-icon-arrows-alt {
        visibility: hidden;
    }

    .icon {
        color: white;
    }

    .icon:hover {
        color: red;
    }

    .icon2 {
        color: white;
    }

    .icon2:hover {
        color: green;
    }

    .centering2 {
        width: 100%;
        height: 100%;
        overflow: hidden;
        margin: auto;
        position: absolute;
        z-index: 1;
    }

    .centering4 {
        margin: 0;
        position: absolute;
        top: 50%;
        right: 50%;
        margin-left: -50%;
        transform: translate(50%, -50%)
    }

    .centering5 {
        margin: 0;
        position: absolute;
        top: 50%;
        right: 67%;
        margin-left: -50%;
        transform: translate(67%, -50%)
    }

    .crosshair {
        margin-left: 30%;
    }

    .numbericon {
        max-width: 8vw;
        border: 3px solid #4AF626;
        border-radius: 1vw;
        padding: 4px;
        text-shadow: 0 0 2px #000;
    }
</style>