<template>
<div class="mdc">
    <div style="height: 100%; width: 100%; background-color: transparent;" v-if="showMDC">
        <div class="row justify-content-center centering3">
            <div class="card card-primary card-outline animate__animated animate__bounceIn" style="max-width: 75vw; max-height: 38vw">
                <div class="card-header" style="font-family: 'Exo', sans-serif; font-size: 1.2vw">
                    Mobile Data Computer <img style="width: 2vw; ml-2" src="../assets/images/lslogo.png">
                    <div class="col-md-3 float-right" v-if="faction == 1 || faction == 2">
                        <input type="text" class="form-control" placeholder="Personensuche" maxlength="35" autocomplete="off" v-on:keyup.enter="searchPlayer()" v-model="searchelement2">
                    </div>
                </div>
                <div class="card-body" style="height: 100%">
                    <div class="col-md-12">
                        <div class="col-md-2 float-left">
                            <nav>
                                <ul class="nav nav-pills nav-sidebar flex-column" data-widget="treeview" role="menu" data-accordion="false">
                                    <li class="nav-item">
                                        <a class="nav-link">
                                            <a :class="[(selectMDC == 0) ? 'nav-link active' : '']" style="color:white; font-family: 'Exo', sans-serif; font-size: 0.8vw" @click="selectMDC = 0">
                                                Dashboard
                                            </a>
                                        </a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link">
                                            <p :class="[(selectMDC == 17 || selectMDC == 18) ? 'nav-link active':'']" style="color:white; font-family: 'Exo', sans-serif; font-size: 0.8vw" @click="loadDocuments()">
                                                Dokumente
                                            </p>
                                        </a>
                                    </li>
                                    <li class="nav-item" v-if="faction == 1">
                                        <a class="nav-link">
                                            <a :class="[(selectMDC == 1 || selectMDC == 2 || selectMDC == 3) ? 'nav-link active':'']" style="color:white; font-family: 'Exo', sans-serif; font-size: 0.8vw" @click="selectMDC = 1">
                                                Fahndungen
                                            </a>
                                        </a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link">
                                            <a :class="[(selectMDC == 8 || selectMDC == 9) ? 'nav-link active':'']" style="color:white; font-family: 'Exo', sans-serif; font-size: 0.8vw" @click="selectDispatches()">
                                                Dispatches
                                            </a>
                                        </a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link">
                                            <p :class="[(selectMDC == 6) ? 'nav-link active':'']" style="color:white; font-family: 'Exo', sans-serif; font-size: 0.8vw" @click="selectUsers(0)">
                                                Dutyübersicht
                                            </p>
                                        </a>
                                    </li>
                                    <li class="nav-item" v-if="faction == 1">
                                        <a class="nav-link">
                                            <p :class="[(selectMDC == 13) ? 'nav-link active':'']" style="color:white; font-family: 'Exo', sans-serif; font-size: 0.8vw" @click="selectBlitzer()">
                                                Blitzerübersicht
                                            </p>
                                        </a>
                                    </li>
                                    <li class="nav-item" v-if="faction == 1">
                                        <a class="nav-link">
                                            <p :class="[(selectMDC == 15) ? 'nav-link active':'']" style="color:white; font-family: 'Exo', sans-serif; font-size: 0.8vw" @click="selectCCTV()">
                                                CCTVs
                                            </p>
                                        </a>
                                    </li>
                                    <li class="nav-item" v-if="faction == 1">
                                        <a class="nav-link">
                                            <p :class="[(selectMDC == 10) ? 'nav-link active':'']" style="color:white; font-family: 'Exo', sans-serif; font-size: 0.8vw" @click="selectFirmen()">
                                                Firmen
                                            </p>
                                        </a>
                                    </li>
                                    <li class="nav-item" v-if="faction == 1">
                                        <a class="nav-link">
                                            <p :class="[(selectMDC == 14) ? 'nav-link active':'']" style="color:white; font-family: 'Exo', sans-serif; font-size: 0.8vw" @click="selectArrested()">
                                                Gefängnisinsassen
                                            </p>
                                        </a>
                                    </li>
                                    <li class="nav-item" v-if="(faction == 1 || faction == 2 || faction == 3) && factionRang >= 10">
                                        <a class="nav-link">
                                            <p :class="[(selectMDC == 11) ? 'nav-link active':'']" style="color:white; font-family: 'Exo', sans-serif; font-size: 0.8vw" @click="showWeaponOrder()">
                                                Waffenbestellung
                                            </p>
                                        </a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link">
                                            <p :class="[(selectMDC == 7 || selectMDC == 4 || selectMDC == 5 || selectMDC == 12 || selectMDC == 16) ? 'nav-link active':'']" style="color:white; font-family: 'Exo', sans-serif; font-size: 0.8vw" @click="fahndungText = '',allMessage = '',numberplatetext = '',fahndungCreator = '',selectMDC = 7">
                                                Sonstiges
                                            </p>
                                        </a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link">
                                            <p :class="[(selectMDC == -1) ? 'nav-link active':'']" style="color:white; font-family: 'Exo', sans-serif; font-size: 0.8vw">
                                                <span style="color:#f04a26" @click="turnOff()">Ausschalten</span>
                                            </p>
                                        </a>
                                    </li>
                                </ul>
                            </nav>
                        </div>
                        <div style="width: 72vw; margin-right: 13vw">
                            <div class="card card-primary card-outline">
                                <div class="card-header p-2">
                                    <div class="card-body" style="height:100%" v-if="selectMDC == 0">
                                        <div class="row">
                                            <div class="col-12 col-sm-6 col-md-3">
                                                <div class="info-box">
                                                    <span class="info-box-icon bg-primary elevation-1"><i class="fas fa-users"></i></span>
                                                    <div class="info-box-content">
                                                        <span class="info-box-text">Mitglieder</span>
                                                        <span class="info-box-number">
                                                            {{otherInfo[0]}}
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12 col-sm-6 col-md-3">
                                                <div class="info-box">
                                                    <span class="info-box-icon bg-primary elevation-1"><i class="fas fa-user"></i></span>
                                                    <div class="info-box-content">
                                                        <span class="info-box-text">Im Dienst</span>
                                                        <span class="info-box-number">
                                                            {{otherInfo[1]}}
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12 col-sm-6 col-md-3" v-if="faction == 1">
                                                <div class="info-box">
                                                    <span class="info-box-icon bg-danger elevation-1"><i class="fas fa-ban"></i></span>
                                                    <div class="info-box-content">
                                                        <span class="info-box-text">Fahndungen</span>
                                                        <span class="info-box-number">
                                                            {{otherInfo[2]}}
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12 col-sm-6 col-md-3">
                                                <div class="info-box">
                                                    <span class="info-box-icon bg-warning elevation-1"><i class="fas fa-file-alt"></i></span>
                                                    <div class="info-box-content">
                                                        <span class="info-box-text">Dispatches</span>
                                                        <span class="info-box-number">
                                                            {{otherInfo[3]}}
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="card" style="overflow-x: auto; height:100%" v-if="faction == 1">
                                            <div class="card-header border-transparent">
                                                <h3 class="card-title" style="font-family: 'Exo', sans-serif; font-size: 0.8vw">Die letzten
                                                    Fahndungen</h3>
                                            </div>
                                            <div class="card-body p-0" style="height: 18.5vw">
                                                <div class="table-responsive">
                                                    <table class="table">
                                                        <thead>
                                                            <tr>
                                                                <th>#</th>
                                                                <th>Beschreibung</th>
                                                                <th>Status</th>
                                                                <th>Datum</th>
                                                            </tr>
                                                        </thead>
                                                        <tr v-for="fahndung in fahndungen.slice(0, 10)" :key="fahndung.id">
                                                            <td><a href="#" @click="navigate('selectFahndung', fahndung)">FHDNG-{{fahndung.id}}</a>
                                                            </td>
                                                            <td>{{fahndung.text.substring(0, 65)}}...</td>
                                                            <td><span class="badge badge-warning">{{getFahndungsStatus(fahndung.status)}}</span>
                                                            </td>
                                                            <td>
                                                                {{timeConverter(fahndung.timestamp)}}
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="card" style="overflow-x: auto; height:100%" v-if="faction == 2 || faction == 3">
                                            <div class="card-header border-transparent">
                                                <h3 class="card-title" style="font-family: 'Exo', sans-serif; font-size: 0.8vw">Die letzten Dispatches</h3>
                                            </div>
                                            <div class="card-body p-0" style="height: 18.5vw">
                                                <div class="table-responsive">
                                                    <table class="table">
                                                        <thead>
                                                            <tr>
                                                                <th>#</th>
                                                                <th>Beschreibung</th>
                                                                <th>Status</th>
                                                                <th>Von</th>
                                                                <th>Datum</th>
                                                                <th>Aktion</th>
                                                            </tr>
                                                        </thead>
                                                        <tr v-for="dispatch in dispatchInfo.slice(0, 10)" :key="dispatch.id">
                                                            <td><a href="#" @click="navigate('selectDispatch', dispatch)">DSPTH-{{dispatch.id}}</a>
                                                            </td>
                                                            <td>{{dispatch.text.substring(0, 65)}}...</td>
                                                            <td><span class="badge badge-warning">{{dispatch.status}}</span>
                                                            </td>
                                                            <td>
                                                                {{dispatch.name}}
                                                            </td>
                                                            <td>
                                                                {{timeConverter(dispatch.timestamp)}}
                                                            </td>
                                                            <td>
                                                                <i class="nav-icon fa-solid fa-location-dot iconorange" @click="locatePlayer(dispatch.position)"></i><i class="nav-icon fa-solid fa-ban iconred ml-2" @click="deleteDispatch(dispatch.id)"></i>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="card-footer clearfix" v-if="faction == 1">
                                            <a href="javascript:void(0)" class="btn btn-sm btn-primary float-left" @click="navigate('createFahndung')">Neue Fahndung
                                                erstellen</a>
                                            <a href="javascript:void(0)" class="btn btn-sm btn-secondary float-right" @click="navigate('showFahndungen')">Alle Fahndungen
                                                einsehen</a>
                                        </div>
                                        <div class="card-footer clearfix" v-if="faction == 2 || faction == 3">
                                            <a href="javascript:void(0)" class="btn btn-sm btn-secondary float-right" @click="selectDispatches()">Alle Dispatches
                                                einsehen</a>
                                        </div>
                                    </div>
                                    <div class="card-body" style="height:100%" v-if="selectMDC == 1">
                                        <div class="card" style="overflow-x: auto; height:100%">
                                            <div class="card-header border-transparent">
                                                <h3 class="card-title" style="font-family: 'Exo', sans-serif; font-size: 0.8vw">Fahndungsliste
                                                </h3>
                                            </div>
                                            <div class="card-body p-0" style="height: 18.5vw">
                                                <div class="table-responsive">
                                                    <table class="table">
                                                        <thead>
                                                            <tr>
                                                                <th>#</th>
                                                                <th>Beschreibung</th>
                                                                <th>Status</th>
                                                                <th>Datum</th>
                                                            </tr>
                                                        </thead>
                                                        <tr v-for="fahndung in filteredList" :key="fahndung.id">
                                                            <td><a href="#" @click="navigate('selectFahndung', fahndung)">FHDNG-{{fahndung.id}}</a>
                                                            </td>
                                                            <td>{{fahndung.text.substring(0, 65)}}...</td>
                                                            <td><span class="badge badge-warning">{{getFahndungsStatus(fahndung.status)}}</span>
                                                            </td>
                                                            <td>
                                                                {{timeConverter(fahndung.timestamp)}}
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="card-footer clearfix">
                                            <div class="col-md-9">
                                                <input type="text" class="form-control float-left" placeholder="Fahndungssuche" v-bind:value="searchelement" v-on:input="searchelement = $event.target.value" maxlength="128" autocomplete="off">
                                            </div>
                                            <a href="javascript:void(0)" @click="navigate('createFahndung')" class="btn btn-sm btn-primary float-right" style="color:white">Neue
                                                Fahndung
                                                erstellen</a>
                                        </div>
                                    </div>
                                    <div class="card-body" style="height:100%" v-if="selectMDC == 11">
                                        <div class="card" style="overflow-x: auto; height:100%">
                                            <div class="card-header border-transparent">
                                                <h3 class="card-title" style="font-family: 'Exo', sans-serif; font-size: 0.8vw">
                                                    Waffenbestellung
                                                </h3>
                                            </div>
                                            <div class="card-body p-0" style="height: 18.5vw">
                                                <div class="table-responsive">
                                                    <table class="table" v-if="faction == 1">
                                                        <thead>
                                                            <tr>
                                                                <th>Bestellposition</th>
                                                                <th>Gegenstand/Waffe</th>
                                                                <th>Auf Lager</th>
                                                                <th>Menge</th>
                                                            </tr>
                                                        </thead>
                                                        <tr>
                                                            <td>1</td>
                                                            <td>Schlagstock</td>
                                                            <td>{{orderInfo[0]}}/Stck</td>
                                                            <td> <input type="text" maxlength="3" v-on:change="countOrder()" class="col-md-3 form-control text-center" placeholder="Menge" style="border-radius: 1vw" v-model="orderInfo2[0]"></td>
                                                        </tr>
                                                        <tr>
                                                            <td>2</td>
                                                            <td>Taser</td>
                                                            <td>{{orderInfo[1]}}/Stck</td>
                                                            <td> <input type="text" maxlength="3" v-on:change="countOrder()" class="col-md-3 form-control text-center" placeholder="Menge" style="border-radius: 1vw" v-model="orderInfo2[1]"></td>
                                                        </tr>
                                                        <tr>
                                                            <td>3</td>
                                                            <td>Pistole</td>
                                                            <td>{{orderInfo[2]}}/Stck</td>
                                                            <td> <input type="text" maxlength="3" v-on:change="countOrder()" class="col-md-3 form-control text-center" placeholder="Menge" style="border-radius: 1vw" v-model="orderInfo2[2]"></td>
                                                        </tr>
                                                        <tr>
                                                            <td>4</td>
                                                            <td>Pistole-MK2</td>
                                                            <td>{{orderInfo[3]}}/Stck</td>
                                                            <td> <input type="text" maxlength="3" v-on:change="countOrder()" class="col-md-3 form-control text-center" placeholder="Menge" style="border-radius: 1vw" v-model="orderInfo2[3]"></td>
                                                        </tr>
                                                        <tr>
                                                            <td>5</td>
                                                            <td>Pistole-50</td>
                                                            <td>{{orderInfo[4]}}/Stck</td>
                                                            <td> <input type="text" maxlength="3" v-on:change="countOrder()" class="col-md-3 form-control text-center" placeholder="Menge" style="border-radius: 1vw" v-model="orderInfo2[4]"></td>
                                                        </tr>
                                                        <tr>
                                                            <td>6</td>
                                                            <td>MP5-MK2</td>
                                                            <td>{{orderInfo[5]}}/Stck</td>
                                                            <td> <input type="text" maxlength="3" v-on:change="countOrder()" class="col-md-3 form-control text-center" placeholder="Menge" style="border-radius: 1vw" v-model="orderInfo2[5]"></td>
                                                        </tr>
                                                        <tr>
                                                            <td>7</td>
                                                            <td>Karabiner-Gewehr-MK2</td>
                                                            <td>{{orderInfo[6]}}/Stck</td>
                                                            <td> <input type="text" maxlength="3" v-on:change="countOrder()" class="col-md-3 form-control text-center" placeholder="Menge" style="border-radius: 1vw" v-model="orderInfo2[6]"></td>
                                                        </tr>
                                                        <tr>
                                                            <td>8</td>
                                                            <td>Sniper</td>
                                                            <td>{{orderInfo[7]}}/Stck</td>
                                                            <td> <input type="text" maxlength="3" v-on:change="countOrder()" class="col-md-3 form-control text-center" placeholder="Menge" style="border-radius: 1vw" v-model="orderInfo2[7]"></td>
                                                        </tr>
                                                        <tr>
                                                            <td>9</td>
                                                            <td>BZGas</td>
                                                            <td>{{orderInfo[8]}}/Stck</td>
                                                            <td> <input type="text" maxlength="3" v-on:change="countOrder()" class="col-md-3 form-control text-center" placeholder="Menge" style="border-radius: 1vw" v-model="orderInfo2[8]"></td>
                                                        </tr>
                                                        <tr>
                                                            <td>10</td>
                                                            <td>Rauchgranate</td>
                                                            <td>{{orderInfo[9]}}/Stck</td>
                                                            <td> <input type="text" maxlength="3" v-on:change="countOrder()" class="col-md-3 form-control text-center" placeholder="Menge" style="border-radius: 1vw" v-model="orderInfo2[9]"></td>
                                                        </tr>
                                                        <tr>
                                                            <td>11</td>
                                                            <td>Grosse-Schutzweste</td>
                                                            <td>{{orderInfo[10]}}/Stck</td>
                                                            <td> <input type="text" maxlength="3" v-on:change="countOrder()" class="col-md-3 form-control text-center" placeholder="Menge" style="border-radius: 1vw" v-model="orderInfo2[10]"></td>
                                                        </tr>
                                                        <tr>
                                                            <td>12</td>
                                                            <td>9mm-Munition</td>
                                                            <td>{{orderInfo[11]}}/Stck</td>
                                                            <td> <input type="text" maxlength="4" v-on:change="countOrder()" class="col-md-3 form-control text-center" placeholder="Menge" style="border-radius: 1vw" v-model="orderInfo2[11]"></td>
                                                        </tr>
                                                        <tr>
                                                            <td>13</td>
                                                            <td>5.56-Munition</td>
                                                            <td>{{orderInfo[12]}}/Stck</td>
                                                            <td> <input type="text" maxlength="4" v-on:change="countOrder()" class="col-md-3 form-control text-center" placeholder="Menge" style="border-radius: 1vw" v-model="orderInfo2[12]"></td>
                                                        </tr>
                                                        <tr>
                                                            <td>14</td>
                                                            <td>7.62-Munition</td>
                                                            <td>{{orderInfo[13]}}/Stck</td>
                                                            <td> <input type="text" maxlength="4" v-on:change="countOrder()" class="col-md-3 form-control text-center" placeholder="Menge" style="border-radius: 1vw" v-model="orderInfo2[13]"></td>
                                                        </tr>
                                                        <tr>
                                                            <td>15</td>
                                                            <td>Dietrich</td>
                                                            <td>{{orderInfo[14]}}/Stck</td>
                                                            <td> <input type="text" maxlength="3" v-on:change="countOrder()" class="col-md-3 form-control text-center" placeholder="Menge" style="border-radius: 1vw" v-model="orderInfo2[14]"></td>
                                                        </tr>
                                                        <tr>
                                                            <td>16</td>
                                                            <td>Drohne</td>
                                                            <td>{{orderInfo[15]}}/Stck</td>
                                                            <td> <input type="text" maxlength="3" v-on:change="countOrder()" class="col-md-3 form-control text-center" placeholder="Menge" style="border-radius: 1vw" v-model="orderInfo2[15]"></td>
                                                        </tr>
                                                    </table>
                                                    <table class="table" v-if="faction == 2">
                                                        <thead>
                                                            <tr>
                                                                <th>Bestellposition</th>
                                                                <th>Gegenstand/Waffe</th>
                                                                <th>Auf Lager</th>
                                                                <th>Menge</th>
                                                            </tr>
                                                        </thead>
                                                        <tr>
                                                            <td>1</td>
                                                            <td>Defribrilator</td>
                                                            <td>{{orderInfo[0]}}/Stck</td>
                                                            <td> <input type="text" maxlength="3" v-on:change="countOrder()" class="col-md-3 form-control text-center" placeholder="Menge" style="border-radius: 1vw" v-model="orderInfo2[0]"></td>
                                                        </tr>
                                                        <tr>
                                                            <td>2</td>
                                                            <td>Taser</td>
                                                            <td>{{orderInfo[1]}}/Stck</td>
                                                            <td> <input type="text" maxlength="3" v-on:change="countOrder()" class="col-md-3 form-control text-center" placeholder="Menge" style="border-radius: 1vw" v-model="orderInfo2[1]"></td>
                                                        </tr>
                                                        <tr>
                                                            <td>3</td>
                                                            <td>Stethoskop</td>
                                                            <td>{{orderInfo[2]}}/Stck</td>
                                                            <td> <input type="text" maxlength="3" v-on:change="countOrder()" class="col-md-3 form-control text-center" placeholder="Menge" style="border-radius: 1vw" v-model="orderInfo2[2]"></td>
                                                        </tr>
                                                        <tr>
                                                            <td>4</td>
                                                            <td>Drohne</td>
                                                            <td>{{orderInfo[3]}}/Stck</td>
                                                            <td> <input type="text" maxlength="3" v-on:change="countOrder()" class="col-md-3 form-control text-center" placeholder="Menge" style="border-radius: 1vw" v-model="orderInfo2[3]"></td>
                                                        </tr>
                                                        <tr>
                                                            <td>5</td>
                                                            <td>Grippofein-C</td>
                                                            <td>{{orderInfo[4]}}/Stck</td>
                                                            <td> <input type="text" maxlength="3" v-on:change="countOrder()" class="col-md-3 form-control text-center" placeholder="Menge" style="border-radius: 1vw" v-model="orderInfo2[4]"></td>
                                                        </tr>
                                                        <tr>
                                                            <td>6</td>
                                                            <td>Antibiotika</td>
                                                            <td>{{orderInfo[5]}}/Stck</td>
                                                            <td> <input type="text" maxlength="3" v-on:change="countOrder()" class="col-md-3 form-control text-center" placeholder="Menge" style="border-radius: 1vw" v-model="orderInfo2[5]"></td>
                                                        </tr>
                                                        <tr>
                                                            <td>7</td>
                                                            <td>Ibuprofee-400mg</td>
                                                            <td>{{orderInfo[6]}}/Stck</td>
                                                            <td> <input type="text" maxlength="3" v-on:change="countOrder()" class="col-md-3 form-control text-center" placeholder="Menge" style="border-radius: 1vw" v-model="orderInfo2[6]"></td>
                                                        </tr>
                                                        <tr>
                                                            <td>8</td>
                                                            <td>Ibuprofee-800mg</td>
                                                            <td>{{orderInfo[7]}}/Stck</td>
                                                            <td> <input type="text" maxlength="3" v-on:change="countOrder()" class="col-md-3 form-control text-center" placeholder="Menge" style="border-radius: 1vw" v-model="orderInfo2[7]"></td>
                                                        </tr>
                                                        <tr>
                                                            <td>9</td>
                                                            <td>Morphin-10mg</td>
                                                            <td>{{orderInfo[8]}}/Stck</td>
                                                            <td> <input type="text" maxlength="3" v-on:change="countOrder()" class="col-md-3 form-control text-center" placeholder="Menge" style="border-radius: 1vw" v-model="orderInfo2[8]"></td>
                                                        </tr>
                                                        <tr>
                                                            <td>10</td>
                                                            <td>Bandage</td>
                                                            <td>{{orderInfo[9]}}/Stck</td>
                                                            <td> <input type="text" maxlength="3" v-on:change="countOrder()" class="col-md-3 form-control text-center" placeholder="Menge" style="border-radius: 1vw" v-model="orderInfo2[9]"></td>
                                                        </tr>
                                                        <tr>
                                                            <td>11</td>
                                                            <td>Feuerlöscher</td>
                                                            <td>{{orderInfo[10]}}/Stck</td>
                                                            <td> <input type="text" maxlength="3" v-on:change="countOrder()" class="col-md-3 form-control text-center" placeholder="Menge" style="border-radius: 1vw" v-model="orderInfo2[10]"></td>
                                                        </tr>
                                                        <tr>
                                                            <td>12</td>
                                                            <td>Axt</td>
                                                            <td>{{orderInfo[11]}}/Stck</td>
                                                            <td> <input type="text" maxlength="3" v-on:change="countOrder()" class="col-md-3 form-control text-center" placeholder="Menge" style="border-radius: 1vw" v-model="orderInfo2[11]"></td>
                                                        </tr>
                                                        <tr>
                                                            <td>13</td>
                                                            <td>Dietrich</td>
                                                            <td>{{orderInfo[12]}}/Stck</td>
                                                            <td> <input type="text" maxlength="3" v-on:change="countOrder()" class="col-md-3 form-control text-center" placeholder="Menge" style="border-radius: 1vw" v-model="orderInfo2[12]"></td>
                                                        </tr>
                                                    </table>
                                                    <table class="table" v-if="faction == 3">
                                                        <thead>
                                                            <tr>
                                                                <th>Bestellposition</th>
                                                                <th>Gegenstand/Waffe</th>
                                                                <th>Auf Lager</th>
                                                                <th>Menge</th>
                                                            </tr>
                                                        </thead>
                                                        <tr>
                                                            <td>1</td>
                                                            <td>Taser</td>
                                                            <td>{{orderInfo[0]}}/Stck</td>
                                                            <td> <input type="text" maxlength="3" v-on:change="countOrder()" class="col-md-3 form-control text-center" placeholder="Menge" style="border-radius: 1vw" v-model="orderInfo2[0]"></td>
                                                        </tr>
                                                        <tr>
                                                            <td>2</td>
                                                            <td>Parkkralle</td>
                                                            <td>{{orderInfo[1]}}/Stck</td>
                                                            <td> <input type="text" maxlength="3" v-on:change="countOrder()" class="col-md-3 form-control text-center" placeholder="Menge" style="border-radius: 1vw" v-model="orderInfo2[1]"></td>
                                                        </tr>
                                                        <tr>
                                                            <td>3</td>
                                                            <td>Reparaturwerkzeug</td>
                                                            <td>{{orderInfo[2]}}/Stck</td>
                                                            <td> <input type="text" maxlength="3" v-on:change="countOrder()" class="col-md-3 form-control text-center" placeholder="Menge" style="border-radius: 1vw" v-model="orderInfo2[2]"></td>
                                                        </tr>
                                                        <tr>
                                                            <td>4</td>
                                                            <td>Dietrich</td>
                                                            <td>{{orderInfo[3]}}/Stck</td>
                                                            <td> <input type="text" maxlength="3" v-on:change="countOrder()" class="col-md-3 form-control text-center" placeholder="Menge" style="border-radius: 1vw" v-model="orderInfo2[3]"></td>
                                                        </tr>
                                                        <tr>
                                                            <td>5</td>
                                                            <td>Taschenlampe</td>
                                                            <td>{{orderInfo[4]}}/Stck</td>
                                                            <td> <input type="text" maxlength="3" v-on:change="countOrder()" class="col-md-3 form-control text-center" placeholder="Menge" style="border-radius: 1vw" v-model="orderInfo2[4]"></td>
                                                        </tr>
                                                        <tr>
                                                            <td>6</td>
                                                            <td>Brechstange</td>
                                                            <td>{{orderInfo[5]}}/Stck</td>
                                                            <td> <input type="text" maxlength="3" v-on:change="countOrder()" class="col-md-3 form-control text-center" placeholder="Menge" style="border-radius: 1vw" v-model="orderInfo2[5]"></td>
                                                        </tr>
                                                        <tr>
                                                            <td>7</td>
                                                            <td>Benzinkanister</td>
                                                            <td>{{orderInfo[6]}}/Stck</td>
                                                            <td> <input type="text" maxlength="3" v-on:change="countOrder()" class="col-md-3 form-control text-center" placeholder="Menge" style="border-radius: 1vw" v-model="orderInfo2[6]"></td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="card-footer clearfix">
                                            <a style="color:white" href="#" class="btn btn-sm btn-primary float-right" @click="orderWeapons()">Bestellung aufgeben</a>
                                            <div class="text-center">
                                                <h5>Bestellgewicht: {{weight}}g/135000g</h5>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card-body" style="height:100%" v-if="selectMDC == 10">
                                        <div class="card" style="overflow-x: auto; height:100%">
                                            <div class="card-header border-transparent">
                                                <h3 class="card-title" style="font-family: 'Exo', sans-serif; font-size: 0.8vw">
                                                    Firmenübersicht
                                                </h3>
                                            </div>
                                            <div class="card-body p-0" style="height: 18.5vw">
                                                <div class="table-responsive">
                                                    <table class="table">
                                                        <thead>
                                                            <tr>
                                                                <th>Firmenname</th>
                                                                <th>Inhaber</th>
                                                                <th>Mitarbeiter</th>
                                                                <th>Lizenzen</th>
                                                                <th>Gründung</th>
                                                            </tr>
                                                        </thead>
                                                        <tr v-for="firma in filteredList3" :key="firma.id">
                                                            <td v-if="firma.name.length > 65">{{firma.name.substring(0, 65)}}</td>
                                                            <td v-else>{{firma.name}}</td>
                                                            <td>{{firma.leadername}}</td>
                                                            <td>{{firma.members}}</td>
                                                            <td>
                                                                <span v-if="firma.licenses.split('|')[0] == 1"></span> <a><i style="color:green" v-if="firma.licenses.split('|')[0] == 1" class="fas fa-truck mr-1"></i><i v-if="firma.licenses.split('|')[0] != 1" style="color:red" class="fas fa-truck mr-1"></i></a>
                                                                <span v-if="firma.licenses.split('|')[1] == 1"></span> <a><i style="color:green" v-if="firma.licenses.split('|')[1] == 1" class="fa-solid fa-wrench mr-1"></i><i v-if="firma.licenses.split('|')[1] != 1" style="color:red" class="fa-solid fa-wrench mr-1"></i></a>
                                                                <span v-if="firma.licenses.split('|')[2] == 1"></span> <a><i style="color:green" v-if="firma.licenses.split('|')[2] == 1" class="fa-solid fa-car mr-1"></i><i v-if="firma.licenses.split('|')[2] != 1" style="color:red" class="fa-solid fa-car mr-1"></i></a>
                                                                <span v-if="firma.licenses.split('|')[3] == 1"></span> <a><i style="color:green" v-if="firma.licenses.split('|')[3] == 1" class="fa-solid fa-bus mr-1"></i><i v-if="firma.licenses.split('|')[3] != 1" style="color:red" class="fa-solid fa-bus mr-1"></i></a>
                                                                <span v-if="firma.licenses.split('|')[4] == 1"></span> <a><i style="color:green" v-if="firma.licenses.split('|')[4] == 1" class="fa-solid fa-file-shield mr-1"></i><i v-if="firma.licenses.split('|')[4] != 1" style="color:red" class="fa-solid fa-file-shield mr-1"></i></a>
                                                            </td>
                                                            <td>
                                                                {{timeConverter(firma.created)}}
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="card-footer clearfix">
                                            <div class="col-md-9">
                                                <input type="text" class="form-control float-left" placeholder="Firmensuche" v-bind:value="searchelement" v-on:input="searchelement = $event.target.value" maxlength="128" autocomplete="off">
                                            </div>
                                            <a href="javascript:void(0)" @click="selectMDC = 0" class="btn btn-sm btn-primary float-right" style="color:white">Zurück zum Dashboard</a>
                                        </div>
                                    </div>
                                    <div class="card-body" style="height:100%" v-if="selectMDC == 14">
                                        <div class="card" style="overflow-x: auto; height:100%">
                                            <div class="card-header border-transparent">
                                                <h3 class="card-title" style="font-family: 'Exo', sans-serif; font-size: 0.8vw">
                                                    Gefängnisinsassenübersicht
                                                </h3>
                                            </div>
                                            <div class="card-body p-0" style="height: 18.5vw">
                                                <div class="table-responsive">
                                                    <table class="table">
                                                        <thead>
                                                            <tr>
                                                                <th>Name</th>
                                                                <th>Haftzeit</th>
                                                                <th>Zelle</th>
                                                            </tr>
                                                        </thead>
                                                        <tr v-for="(player, index) in arrestInfo" :key="index">
                                                            <td>{{player.Name}}</td>
                                                            <td>{{player.ID}} Minuten</td>
                                                            <td>{{player.Screen}}</td>
                                                        </tr>
                                                    </table>
                                                    <h5 class="text-center" v-if="arrestInfo.length <= 0">Aktuell wurde noch keiner inhaftiert!
                                                    </h5>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="card-footer clearfix">
                                            <a href="javascript:void(0)" @click="selectMDC = 0" class="btn btn-sm btn-primary float-right" style="color:white">Zurück zum Dashboard</a>
                                        </div>
                                    </div>
                                    <div class="card-body" style="height:100%" v-if="selectMDC == 12">
                                        <div class="card" style="overflow-x: auto; height:100%">
                                            <div class="card-header border-transparent">
                                                <h3 class="card-title" style="font-family: 'Exo', sans-serif; font-size: 0.8vw">
                                                    Waffendatenbankeintrag - WEPN-{{weaponInfo.Screen}}
                                                </h3>
                                            </div>
                                            <div class="card-body p-0" style="height: 18.5vw">
                                                <ul class="list-group list-group-bordered" style="font-size: 0.9vw">
                                                    <li class="ml-5 mt-5">
                                                        <b>Waffenname</b> <a class="float-right mr-5">
                                                            <div class="col-12 float-right">{{weaponInfo.weaponname}}
                                                            </div>
                                                        </a>
                                                    </li>
                                                    <li class="ml-5 mt-1">
                                                        <b>Identifikationsnummer</b> <a class="float-right mr-5">
                                                            <div class="col-12 float-right">WEPN-{{weaponInfo.Screen}}
                                                            </div>
                                                        </a>
                                                    </li>
                                                    <li class="ml-5 mt-1">
                                                        <b>Ausgestellt an</b> <a class="float-right mr-5">
                                                            <div class="col-12 float-right">{{weaponInfo.name}}
                                                            </div>
                                                        </a>
                                                    </li>
                                                    <li class="ml-5 mt-1">
                                                        <b>Ausgestellt von</b> <a class="float-right mr-5">
                                                            <div class="col-12 float-right">{{weaponInfo.shop}}
                                                            </div>
                                                        </a>
                                                    </li>
                                                    <li class="ml-5 mt-1">
                                                        <b>Austellungsdatum</b> <a class="float-right mr-5">
                                                            <div class="col-12 float-right">{{timeConverter(weaponInfo.timestamp)}}
                                                            </div>
                                                        </a>
                                                    </li>
                                                </ul>
                                                <img style="height: 2vw; z-index:" class="float-right mr-5 mt-5" :src="getImgUrl(weaponInfo.weaponname)">
                                            </div>
                                        </div>
                                        <div class="card-footer clearfix">
                                            <a href="javascript:void(0)" @click="selectMDC = 0" class="btn btn-sm btn-primary float-right" style="color:white">Zurück zum Dashboard</a>
                                        </div>
                                    </div>
                                    <div class="card-body" style="height:100%" v-if="selectMDC == 16">
                                        <div class="card" style="overflow-x: auto; height:100%">
                                            <div class="card-header border-transparent">
                                                <h3 class="card-title" style="font-family: 'Exo', sans-serif; font-size: 0.8vw">
                                                    Suchergebnisse zum Nummernschild: {{plateInfo[0].Job}}
                                                </h3>
                                            </div>
                                            <div class="card-body p-0" style="height: 18.5vw">
                                                <ul class="list-group list-group-bordered" style="font-size: 0.9vw">
                                                    <li class="ml-5 mt-5">
                                                        <b>Fahrzeugname</b> <a class="float-right mr-5">
                                                            <div class="col-12 float-right">{{plateInfo[0].Screen}}
                                                            </div>
                                                        </a>
                                                    </li>
                                                    <li class="ml-5 mt-1">
                                                        <b>Identifikationsnummer</b> <a class="float-right mr-5">
                                                            <div class="col-12 float-right">VHCLE-{{plateInfo[0].Closed}}
                                                            </div>
                                                        </a>
                                                    </li>
                                                    <li class="ml-5 mt-1">
                                                        <b>Besitzer</b> <a class="float-right mr-5">
                                                            <div class="col-12 float-right">{{plateInfo[0].Name}}
                                                            </div>
                                                        </a>
                                                    </li>
                                                    <li class="ml-5 mt-1">
                                                        <b>TÜV</b> <a class="float-right mr-5">
                                                            <div v-if="plateInfo[0].ID > 0" class="col-12 float-right">
                                                                {{timeConverter(plateInfo[0].ID)}}
                                                            </div>
                                                            <div v-else class="col-12 float-right">Nicht vorhanden
                                                            </div>
                                                        </a>
                                                    </li>
                                                </ul>
                                                <img style="height: 4vw; z-index:" class="float-right mr-5 mt-5" src="../assets/images/numberplate.png">
                                            </div>
                                        </div>
                                        <div class="card-footer clearfix">
                                            <a href="javascript:void(0)" @click="selectMDC = 0" class="btn btn-sm btn-primary float-right" style="color:white">Zurück zum Dashboard</a>
                                        </div>
                                    </div>
                                    <div class="card-body" style="height:100%" v-if="selectMDC == 8">
                                        <div class="card" style="overflow-x: auto; height:100%">
                                            <div class="card-header border-transparent">
                                                <h3 class="card-title" style="font-family: 'Exo', sans-serif; font-size: 0.8vw">Dispatches
                                                </h3>
                                            </div>
                                            <div class="card-body p-0" style="height: 18.5vw">
                                                <div class="table-responsive">
                                                    <table class="table">
                                                        <thead>
                                                            <tr>
                                                                <th>#</th>
                                                                <th>Beschreibung</th>
                                                                <th>Status</th>
                                                                <th>Von</th>
                                                                <th>Datum</th>
                                                                <th>Aktion</th>
                                                            </tr>
                                                        </thead>
                                                        <tr v-for="dispatch in filteredList2" :key="dispatch.id">
                                                            <td><a href="#" @click="navigate('selectDispatch', dispatch)">DSPTH-{{dispatch.id}}</a>
                                                            </td>
                                                            <td>{{dispatch.text.substring(0, 65)}}...</td>
                                                            <td><span class="badge badge-warning">{{dispatch.status}}</span>
                                                            </td>
                                                            <td>
                                                                {{dispatch.name}}
                                                            </td>
                                                            <td>
                                                                {{timeConverter(dispatch.timestamp)}}
                                                            </td>
                                                            <td>
                                                                <i class="nav-icon fa-solid fa-location-dot iconorange" @click="locatePlayer(dispatch.position)"></i><i class="nav-icon fa-solid fa-ban iconred ml-2" @click="deleteDispatch(dispatch.id)"></i>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="card-footer clearfix">
                                            <div class="col-md-9">
                                                <input type="text" class="form-control float-left" placeholder="Dispatchsuche" v-bind:value="searchelement" v-on:input="searchelement = $event.target.value" maxlength="128" autocomplete="off">
                                            </div>
                                            <a href="javascript:void(0)" @click="selectMDC = 0" class="btn btn-sm btn-primary float-right" style="color:white">Zurück zum Dashboard</a>
                                        </div>
                                    </div>
                                    <div class="card-body" style="height:100%" v-if="selectMDC == 13">
                                        <div class="card" style="overflow-x: auto; height:100%">
                                            <div class="card-header border-transparent">
                                                <h3 class="card-title" style="font-family: 'Exo', sans-serif; font-size: 0.8vw">
                                                    Blitzerübersicht
                                                </h3>
                                            </div>
                                            <div class="card-body p-0" style="height: 18.5vw">
                                                <div class="table-responsive">
                                                    <table class="table">
                                                        <thead>
                                                            <tr>
                                                                <th>#</th>
                                                                <th>Standort</th>
                                                                <th>Max. Geschwindigkeit</th>
                                                                <th>Erstellt von</th>
                                                                <th>Aktion</th>
                                                            </tr>
                                                        </thead>
                                                        <tr v-for="(blitzer, index) in blitzerInfo" :key="index">
                                                            <td>BLTZR-{{blitzer.id}}</td>
                                                            <td>{{blitzer.who}}</td>
                                                            <td>{{blitzer.maxspeed}} KM/H</td>
                                                            <td>{{blitzer.from}}</td>
                                                            <td>
                                                                <i class="nav-icon fa-solid fa-location-dot iconorange ml-3" @click="locatePlayer(blitzer.position)"></i>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <h5 class="text-center" v-if="blitzerInfo.length <= 0">Es wurden noch keine Blitzer
                                                        erstellt!</h5>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="card-footer clearfix">
                                            <div class="text-center">
                                                <h5>Erstellte Blitzer: {{blitzerInfo.length}}</h5>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card-body" style="height:100%" v-if="selectMDC == 15">
                                        <div class="card" style="overflow-x: auto; height:100%">
                                            <div class="card-header border-transparent">
                                                <h3 class="card-title" style="font-family: 'Exo', sans-serif; font-size: 0.8vw">Closed-Circuit
                                                    Television
                                                </h3>
                                            </div>
                                            <div class="card-body p-0" style="height: 18.5vw">
                                                <div class="table-responsive">
                                                    <table class="table">
                                                        <thead>
                                                            <tr>
                                                                <th>#</th>
                                                                <th>Standort</th>
                                                                <th>Erstellt von</th>
                                                                <th>Aktion</th>
                                                            </tr>
                                                        </thead>
                                                        <tr v-for="(cctv, index) in cctvInfo" :key="index">
                                                            <td>CCTV-{{cctv.id}}</td>
                                                            <td>{{cctv.who}}</td>
                                                            <td>{{cctv.from}}</td>
                                                            <td>
                                                                <i class="nav-icon fa-solid fa-location-dot iconorange ml-3" @click="locatePlayer(cctv.position)"></i>
                                                                <i class="nav-icon fa-solid fa-video icongreen ml-3" @click="lookCCTV(cctv.id)"></i>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <h5 class="text-center" v-if="cctvInfo.length <= 0">Es wurden noch keine CCTVs erstellt!
                                                    </h5>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="card-footer clearfix">
                                            <div class="text-center">
                                                <h5>Erstellte CCTVs: {{cctvInfo.length}}</h5>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card-body" style="height:100%" v-if="selectMDC == 17">
                                        <div class="card" style="overflow-x: auto; height:100%">
                                            <div class="card-header border-transparent">
                                                <h3 class="card-title" style="font-family: 'Exo', sans-serif; font-size: 0.8vw">Dokumente
                                                </h3>
                                            </div>
                                            <div class="card-body p-0" style="height: 18.5vw">
                                                <div class="table-responsive">
                                                    <table class="table">
                                                        <thead>
                                                            <tr>
                                                                <th>#</th>
                                                                <th>Name</th>
                                                                <th>Erstellt von</th>
                                                                <th>Erstellt am</th>
                                                                <th>Aktion</th>
                                                            </tr>
                                                        </thead>
                                                        <tr v-for="doc in documents" :key="doc.id">
                                                            <td>{{doc.id}}</td>
                                                            <td>{{doc.title}}</td>
                                                            <td>{{doc.creator}}</td>
                                                            <td>{{timeConverter(doc.timestamp)}}</td>
                                                            <td>
                                                                <i class="nav-icon fa-solid fa-calendar icongreen ml-3" @click="openDocument(doc.link)"></i>
                                                                <i v-if="factionRang >= 10" class="nav-icon fa-solid fa-ban iconred ml-3" @click="deleteDocument(doc.id)"></i>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="card-footer clearfix">
                                            <div class="text-center" v-if="factionRang >= 10">
                                                <input type="text" class="form-control float-left col-md-4" placeholder="Link zum Dokument" maxlength="250" autocomplete="off" v-model="fahndungText">
                                                <input type="text" class="form-control float-left col-md-3 ml-3" placeholder="Titel" maxlength="35" autocomplete="off" v-model="fahndungCreator">
                                                <a style="color:white" href="#" class="btn btn-sm btn-primary float-right" @click="createDocument()">Dokument eintragen</a>
                                            </div>
                                            <div v-else class="text-center">
                                                <a style="color:white" href="#" class="btn btn-sm btn-primary float-right" @click="selectMDC = 0">Zurück zum Dashboard</a>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card-body" style="height:100%" v-if="selectMDC == 18">
                                        <div class="card" style="overflow-x: auto; height:100%">
                                            <div class="card-header border-transparent">
                                                <h3 class="card-title" style="font-family: 'Exo', sans-serif; font-size: 0.8vw">Dokument einsehen
                                                </h3>
                                            </div>
                                            <div class="card-body" style="height: 24.5vw">
                                                <iframe :src="doclink" width="98%" height="98%" frameborder="0">
                                                </iframe>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="card-body" style="height:100%" v-if="selectMDC == 6">
                                        <div class="card" style="overflow-x: auto; height:100%">
                                            <div class="card-header border-transparent">
                                                <h3 class="card-title" style="font-family: 'Exo', sans-serif; font-size: 0.8vw">Dutyübersicht
                                                </h3>
                                            </div>
                                            <div class="card-body p-0" style="height: 18.5vw">
                                                <div class="table-responsive">
                                                    <table class="table">
                                                        <thead>
                                                            <tr>
                                                                <th>Kennung</th>
                                                                <th>Name</th>
                                                                <th>Status</th>
                                                                <th>Position</th>
                                                            </tr>
                                                        </thead>
                                                        <tr v-for="userinfo in userInfo" :key="userinfo.ID">
                                                            <td>{{userinfo.Job}}</td>
                                                            <td>{{userinfo.Name}}</td>
                                                            <td><span class="badge badge-primary">{{userinfo.Faction}}</span>
                                                            </td>
                                                            <td>
                                                                <i class="nav-icon fa-solid fa-location-dot iconorange ml-3" @click="locatePlayer(userinfo.Screen)"></i>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <h5 class="text-center" v-if="userInfo.length <= 0">Keine Mitglieder im Dienst!</h5>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="card-footer clearfix">
                                            <a style="color:white" href="#" class="btn btn-sm btn-primary float-right" @click="selectUsers(1)">Ansicht aktualisieren</a>
                                            <div class="text-center">
                                                <h5>Aktuelle Mitarbeiter im Dienst: {{userInfo.length}}</h5>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card-body" style="height:100%" v-if="selectMDC == 7">
                                        <div class="card" style="overflow-x: auto; height:100%">
                                            <div class="card-header border-transparent">
                                                <h3 class="card-title" style="font-family: 'Exo', sans-serif; font-size: 0.8vw">Sonstiges
                                                </h3>
                                            </div>
                                            <div class="card-body p-0" style="height: 18.5vw">
                                                <ul class="list-group list-group-bordered" style="font-size: 0.9vw">
                                                    <li class="ml-5 mt-1" v-if="faction == 1 || faction == 2">
                                                        <b>Personensuche</b> <a class="float-right mr-5">
                                                            <div class="col-12 float-right"><input type="text" maxlength="15" class="float-right form-control text-center" placeholder="Personenname" style="border-radius: 1vw" v-model="searchelement2" v-on:keyup.enter="searchPlayer()">
                                                            </div>
                                                        </a>
                                                    </li>
                                                    <li class="ml-5 mt-4" v-if="faction == 1 || faction == 2 || faction == 3">
                                                        <b>Nummernschildsuche</b> <a class="float-right mr-5">
                                                            <div class="col-12 float-right"><input type="text" maxlength="15" class="float-right form-control text-center" placeholder="Nummernschild" style="border-radius: 1vw" v-model="numberplatetext" v-on:keyup.enter="searchNumberPlate()">
                                                            </div>
                                                        </a>
                                                    </li>
                                                    <li class="ml-5 mt-4" v-if="faction == 1">
                                                        <b>Handynummer orten</b> <a class="float-right mr-5">
                                                            <div class="col-12 float-right"><input type="text" maxlength="15" class="float-right form-control text-center" placeholder="Handynummer" style="border-radius: 1vw" v-model="fahndungText" v-on:keyup.enter="locateHandy(fahndungText)">
                                                            </div>
                                                        </a>
                                                    </li>
                                                    <li class="ml-5 mt-4" v-if="faction == 1">
                                                        <b>Waffenidentifikationsnummer suchen</b> <a class="float-right mr-5">
                                                            <div class="col-12 float-right"><input type="text" maxlength="17" class="float-right form-control text-center" placeholder="Identifikationsnummer" style="border-radius: 1vw" v-model="weaponText" v-on:keyup.enter="locateWeapon(weaponText)">
                                                            </div>
                                                        </a>
                                                    </li>
                                                    <li class="ml-5 mt-4" v-if="faction == 1">
                                                        <b>Undercover Identität</b> <a class="float-right mr-5">
                                                            <div class="col-12 float-right"><input type="text" maxlength="35" class="float-right form-control text-center" placeholder="Max Mustermann" style="border-radius: 1vw" v-model="undercoverText" v-on:keyup.enter="underCover(undercoverText)">
                                                            </div>
                                                        </a>
                                                    </li>
                                                    <li class="ml-5 mt-4" v-if="faction == 1 || faction == 2">
                                                        <b>Staatsankündigung</b> <a class="float-right mr-5">
                                                            <div class="col-12 float-right"><textarea class="form-control text-center" style="border-radius: 1vw;resize:none" v-model="allMessage" maxlength="128" rows="3" v-on:keyup.enter="createMessageToAll()" placeholder="Ankündigung"></textarea>
                                                            </div>
                                                        </a>
                                                    </li>
                                                    <li class="ml-5 mt-4" v-if="faction == 1 || faction == 2 || faction == 3">
                                                        <b>Sperrzonengröße</b> <a class="float-right mr-5">
                                                            <a href="javascript:void(0)" class="btn btn-sm btn-primary float-left ml-2 mr-3" @click="closedZone('Klein')">Klein</a>
                                                            <a href="javascript:void(0)" class="btn btn-sm btn-primary float-left ml-2 mr-3" @click="closedZone('Mittel')">Mittel</a>
                                                            <a href="javascript:void(0)" class="btn btn-sm btn-primary float-left ml-2 mb-3 mr-3" @click="closedZone('Gross')">Groß</a>
                                                        </a>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                        <div class="card-footer clearfix">
                                            <a style="color:white" href="#" class="btn btn-sm btn-primary float-left" @click="liveMap()">Livemap an/ausschalten</a>
                                            <a style="color:white" href="#" class="btn btn-sm btn-primary float-right" @click="selectMDC = 0">Zurück zum Dashboard</a>
                                        </div>
                                    </div>
                                    <div class="card-body" style="height:100%" v-if="selectMDC == 5">
                                        <div class="card" style="height:100%">
                                            <div class="card-header border-transparent">
                                                <h3 class="card-title" style="font-family: 'Exo', sans-serif; font-size: 0.8vw">
                                                    Personeninformationen - {{selectedPersonalInfo.name}}
                                                </h3>
                                            </div>
                                            <div class="card-body p-0 mb-2" style="max-height: 23.5vw">
                                                <div class="col-md-12">
                                                    <div class="col-md-4 float-left">
                                                        <div class="card card-primary card-outline">
                                                            <div class="card-body box-profile ">
                                                                <div class="row d-flex justify-content-center">
                                                                    <img v-bind:src="selectedPersonalInfo.screen" style="max-height: 112px;border-radius: 10%;border: 1px solid #adb5bd; width: 10vw;">
                                                                </div>
                                                                <hr />
                                                                <h6 class="mt-2" style="font-size: 0.8vw"><strong><u>Allgemeine Infos:</u></strong>
                                                                </h6>
                                                                <ul class="list-group list-group-bordered" style="font-size: 0.7vw">
                                                                    <li>
                                                                        <b>Name</b> <a class="float-right">{{selectedPersonalInfo.name}}</a>
                                                                    </li>
                                                                    <li>
                                                                        <b>Geburtsdatum</b> <a class="float-right">{{selectedPersonalInfo.birth}}</a>
                                                                    </li>
                                                                    <li>
                                                                        <b>Größe</b> <a class="float-right">{{selectedPersonalInfo.size}}</a>
                                                                    </li>
                                                                    <li>
                                                                        <b>Augenfarbe</b> <a class="float-right">{{selectedPersonalInfo.eyecolor}}</a>
                                                                    </li>
                                                                    <li>
                                                                        <b>Herkunft</b> <a class="float-right">{{selectedPersonalInfo.origin}}</a>
                                                                    </li>
                                                                    <li>
                                                                        <b>Abschluss</b> <a class="float-right">{{selectedPersonalInfo.education}}</a>
                                                                    </li>
                                                                    <li v-if="faction == 1">
                                                                        <b>Punkte in SA</b> <a class="float-right">{{selectedPersonalInfo.sapoints}}</a>
                                                                    </li>
                                                                </ul>
                                                                <h6 v-if="handyInfo.length > 0" class="mt-2" style="font-size: 0.8vw">
                                                                    <strong><u>Handynummern:</u></strong>
                                                                </h6>
                                                                <ul class="list-group list-group-bordered mt-2" style="font-size: 0.7vw">
                                                                    <li v-for="handy in handyInfo" :key="handy.id">
                                                                        <b class="float-right" style="color:white;font-size: 0.7vw">{{handy.Name}}</b>
                                                                    </li>
                                                                </ul>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="card card-primary card-outline" style="overflow-x: auto; height:100%">
                                                            <div class="card-header p-2">
                                                                <ul class="nav nav-pills">
                                                                    <li class="nav-item" v-if="faction == 1"><a :class="[(mdcSelectPersonal == 0) ? 'nav-link active':' nav-link']" @click="mdcSelectPersonal = 0" href="#">Strafakte</a></li>
                                                                    <li class="nav-item" v-if="faction == 2"><a :class="[(mdcSelectPersonal == 7) ? 'nav-link active':' nav-link']" @click="mdcSelectPersonal = 7" href="#">Krankenakte</a></li>
                                                                    <li class="nav-item" v-if="faction == 1"><a :class="[(mdcSelectPersonal == 1) ? 'nav-link active':' nav-link']" @click="mdcSelectPersonal = 1" href="#">Kommentare</a></li>
                                                                    <li class="nav-item" v-if="faction == 1"><a :class="[(mdcSelectPersonal == 2) ? 'nav-link active':' nav-link']" @click="mdcSelectPersonal = 2" href="#">Lizenzen</a></li>
                                                                    <li class="nav-item" v-if="faction == 1"><a :class="[(mdcSelectPersonal == 3) ? 'nav-link active':' nav-link']" @click="selectCars(selectedPersonalInfo.id)" href="#">Fahrzeuge</a></li>
                                                                    <li class="nav-item" v-if="faction == 1"><a :class="[(mdcSelectPersonal == 4) ? 'nav-link active':' nav-link']" @click="selectHouses(selectedPersonalInfo.name)" href="#">Häuser</a></li>
                                                                    <li class="nav-item" v-if="faction == 1"><a :class="[(mdcSelectPersonal == 5) ? 'nav-link active':' nav-link']" @click="selectBizz(selectedPersonalInfo.name)" href="#">Businesse</a></li>
                                                                    <li class="nav-item" v-if="faction == 1"><a :class="[(mdcSelectPersonal == 8) ? 'nav-link active':' nav-link']" @click="selectWeapon(selectedPersonalInfo.name)" href="#">Waffen</a></li>
                                                                    <li class="nav-item" v-if="faction == 1"><a :class="[(mdcSelectPersonal == 6) ? 'nav-link active':' nav-link']" @click="selectInvoice(selectedPersonalInfo.name)" href="#">Offene Rechnungen</a>
                                                                    </li>
                                                                </ul>
                                                            </div>
                                                            <div class="card-body">
                                                                <div v-if="mdcSelectPersonal == 0">
                                                                    <div style="display: flex; justify-content: center; align-items: center;">
                                                                        <div class="card-body p-0" style="max-height: 15.9vw">
                                                                            <div class="table-responsive">
                                                                                <table class="table">
                                                                                    <thead>
                                                                                        <tr>
                                                                                            <th>Eintrag</th>
                                                                                            <th>Von</th>
                                                                                            <th>Datum</th>
                                                                                            <th>Aktion</th>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tr v-for="policef in policeFile" :key="policef.id">
                                                                                        <td v-if="policef.commentary == 0">{{policef.text}}</td>
                                                                                        <td v-if="policef.commentary == 0">{{policef.police}}</td>
                                                                                        <td v-if="policef.commentary == 0">{{timeConverter(policef.timestamp)}}
                                                                                        </td>
                                                                                        <td v-if="policef.commentary == 0"><i class="fa-solid fa-ban ml-2 iconred" @click="deleteStrafaktenEintrag(policef.id, policef.name, policef.text)"></i>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div v-if="mdcSelectPersonal == 7">
                                                                    <div style="display: flex; justify-content: center; align-items: center;">
                                                                        <div class="card-body p-0" style="max-height: 15.9vw">
                                                                            <div class="table-responsive">
                                                                                <table class="table">
                                                                                    <thead>
                                                                                        <tr>
                                                                                            <th>Eintrag</th>
                                                                                            <th>Von</th>
                                                                                            <th>Datum</th>
                                                                                            <th>Aktion</th>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tr v-for="policef in policeFile" :key="policef.id">
                                                                                        <td v-if="policef.commentary == 2">{{policef.text}}</td>
                                                                                        <td v-if="policef.commentary == 2">{{policef.police}}</td>
                                                                                        <td v-if="policef.commentary == 2">{{timeConverter(policef.timestamp)}}
                                                                                        </td>
                                                                                        <td v-if="policef.commentary == 2"><i class="fa-solid fa-ban ml-2 iconred" @click="deleteStrafaktenEintrag(policef.id, policef.name, policef.text)"></i>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div v-if="mdcSelectPersonal == 1">
                                                                    <div style="display: flex; justify-content: center; align-items: center;">
                                                                        <div class="card-body p-0" style="max-height: 15.9vw">
                                                                            <div class="table-responsive">
                                                                                <table class="table">
                                                                                    <thead>
                                                                                        <tr>
                                                                                            <th>Eintrag</th>
                                                                                            <th>Von</th>
                                                                                            <th>Datum</th>
                                                                                            <th>Aktion</th>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tr v-for="policef in policeFile" :key="policef.id">
                                                                                        <td v-if="policef.commentary == 1">{{policef.text}}</td>
                                                                                        <td v-if="policef.commentary == 1">{{policef.police}}</td>
                                                                                        <td v-if="policef.commentary == 1">{{timeConverter(policef.timestamp)}}
                                                                                        </td>
                                                                                        <td v-if="policef.commentary == 1"><i class="fa-solid fa-ban ml-2 iconred" @click="deleteStrafaktenEintrag(policef.id, policef.name, policef.text)"></i>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div v-if="mdcSelectPersonal == 2">
                                                                    <div style="display: flex; justify-content: center; align-items: center;">
                                                                        <div class="card-body p-0" style="max-height: 15.9vw">
                                                                            <div class="table-responsive">
                                                                                <table class="table">
                                                                                    <thead>
                                                                                        <tr>
                                                                                            <th>Lizenz</th>
                                                                                            <th>Status</th>
                                                                                            <th>Aktion</th>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tr>
                                                                                        <td>Führerschein</td>
                                                                                        <td><span class="badge badge-primary">{{getLicStatus(this.licStatus[0])}}</span>
                                                                                        </td>
                                                                                        <td><i class="fa-solid fa-ban ml-2 iconred" @click="setLic(0, 0, selectedPersonalInfo.name)"></i>
                                                                                            <i class="fa-solid fa-circle-plus ml-2 iconred" @click="setLic(0, 1, selectedPersonalInfo.name)"></i>
                                                                                            <i class="fa-solid fa-circle-minus ml-2 icongreen" @click="setLic(0, 2, selectedPersonalInfo.name)"></i>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>Motorradschein</td>
                                                                                        <td><span class="badge badge-primary">{{getLicStatus(this.licStatus[1])}}</span>
                                                                                        </td>
                                                                                        <td><i class="fa-solid fa-ban ml-2 iconred" @click="setLic(0, 0, selectedPersonalInfo.name)"></i>
                                                                                            <i class="fa-solid fa-circle-plus ml-2 iconred" @click="setLic(0, 1, selectedPersonalInfo.name)"></i>
                                                                                            <i class="fa-solid fa-circle-minus ml-2 icongreen" @click="setLic(0, 2, selectedPersonalInfo.name)"></i>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>Truckerschein</td>
                                                                                        <td><span class="badge badge-primary">{{getLicStatus(this.licStatus[2])}}</span>
                                                                                        </td>
                                                                                        <td><i class="fa-solid fa-ban ml-2 iconred" @click="setLic(2, 0, selectedPersonalInfo.name)"></i>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>Bootsschein</td>
                                                                                        <td><span class="badge badge-primary">{{getLicStatus(this.licStatus[3])}}</span>
                                                                                        </td>
                                                                                        <td><i class="fa-solid fa-ban ml-2 iconred" @click="setLic(3, 0, selectedPersonalInfo.name)"></i>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>Flugschein</td>
                                                                                        <td><span class="badge badge-primary">{{getLicStatus(this.licStatus[4])}}</span>
                                                                                        </td>
                                                                                        <td><i class="fa-solid fa-ban ml-2 iconred" @click="setLic(4, 0, selectedPersonalInfo.name)"></i>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>Waffenschein</td>
                                                                                        <td><span class="badge badge-primary">{{getLicStatus(this.licStatus[5])}}</span>
                                                                                        </td>
                                                                                        <td><i class="fa-solid fa-ban ml-2 iconred" @click="setLic(5, 0, selectedPersonalInfo.name)"></i>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div v-if="mdcSelectPersonal == 3">
                                                                    <div style="display: flex; justify-content: center; align-items: center;">
                                                                        <div class="card-body p-0" style="max-height: 15.9vw">
                                                                            <div class="table-responsive">
                                                                                <table class="table">
                                                                                    <thead>
                                                                                        <tr>
                                                                                            <th>Fahrzeugname</th>
                                                                                            <th>Kennzeichen</th>
                                                                                            <th>TÜV</th>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tr v-for="car in carInfo" :key="car.id">
                                                                                        <td>{{car.Name}}</td>
                                                                                        <td><span class="badge badge-primary">{{car.Screen}}</span></td>
                                                                                        <td v-if="car.Faction > 0">{{timeConverter(car.Faction)}}</td>
                                                                                        <td v-else>Nicht vorhanden</td>
                                                                                    </tr>
                                                                                </table>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div v-if="mdcSelectPersonal == 4">
                                                                    <div style="display: flex; justify-content: center; align-items: center;">
                                                                        <div class="card-body p-0" style="max-height: 15.9vw">
                                                                            <div class="table-responsive">
                                                                                <table class="table">
                                                                                    <thead>
                                                                                        <tr>
                                                                                            <th>Adresse</th>
                                                                                            <th>Hausnummer</th>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tr v-for="house in houseInfo" :key="house.id">
                                                                                        <td>{{house.Name}}</td>
                                                                                        <td>{{house.ID}}</td>
                                                                                    </tr>
                                                                                </table>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div v-if="mdcSelectPersonal == 5">
                                                                    <div style="display: flex; justify-content: center; align-items: center;">
                                                                        <div class="card-body p-0" style="max-height: 15.9vw">
                                                                            <div class="table-responsive">
                                                                                <table class="table">
                                                                                    <thead>
                                                                                        <tr>
                                                                                            <th>Geschäftsnummer</th>
                                                                                            <th>Geschäftsname</th>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tr v-for="bizz in bizzInfo" :key="bizz.id">
                                                                                        <td>{{bizz.ID}}</td>
                                                                                        <td>{{bizz.Name}}</td>
                                                                                    </tr>
                                                                                </table>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div v-if="mdcSelectPersonal == 8">
                                                                    <div style="display: flex; justify-content: center; align-items: center;">
                                                                        <div class="card-body p-0" style="max-height: 15.9vw">
                                                                            <div class="table-responsive">
                                                                                <table class="table">
                                                                                    <thead>
                                                                                        <tr>
                                                                                            <th>Identifikationsnummer</th>
                                                                                            <th>Waffenname</th>
                                                                                            <th>Ausgestellt von</th>
                                                                                            <th>Aktion</th>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tr v-for="weapon in weaponInfo" :key="weapon.Screen">
                                                                                        <td>WEPN-{{weapon.Screen}}</td>
                                                                                        <td>{{weapon.Job}}</td>
                                                                                        <td>{{weapon.Name}}</td>
                                                                                        <td><i class="nav-icon fa-solid fa-copy icongreen ml-2" @click="locateWeapon(String(weapon.Screen))"></i></td>
                                                                                    </tr>
                                                                                </table>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div v-if="mdcSelectPersonal == 6">
                                                                    <div style="display: flex; justify-content: center; align-items: center;">
                                                                        <div class="card-body p-0" style="max-height: 15.9vw">
                                                                            <div class="table-responsive">
                                                                                <table class="table">
                                                                                    <thead>
                                                                                        <tr>
                                                                                            <th>Rechnungsnummer</th>
                                                                                            <th>Rechnungstext</th>
                                                                                            <th>Rechnungssumme</th>
                                                                                            <th>Rechnungsdatum</th>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tr v-for="invoice in invoiceInfo" :key="invoice.id">
                                                                                        <td>{{invoice.id}}</td>
                                                                                        <td>{{invoice.text}}</td>
                                                                                        <td>{{invoice.value}}$</td>
                                                                                        <td>{{timeConverter(invoice.timestamp)}}</td>
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
                                        <div class="card-footer clearfix" v-if="mdcSelectPersonal != 0 && mdcSelectPersonal != 1 && mdcSelectPersonal != 2 && mdcSelectPersonal != 7">
                                            <a href="javascript:void(0)" class="btn btn-sm btn-primary float-right" style="color:white" @click="selectMDC = 0">Zurück zum Dashboard</a>
                                        </div>
                                        <div class="card-footer clearfix" v-if="mdcSelectPersonal == 0">
                                            <a href="javascript:void(0)" class="btn btn-sm btn-primary float-right" style="color:white" @click="selectMDC = 0">Zurück zum Dashboard</a>
                                            <div class="col-md-5">
                                                <input type="text" class="form-control float-left" placeholder="Strafakteneintrag hinzufügen" maxlength="225" autocomplete="off" v-model="fahndungText">
                                            </div>
                                            <i class="fa-solid fa-circle-plus ml-2 icongreen" style="font-size: 1.35vw; margin-top: 0.3vw" @click="createStrafakteneintrag(selectedPersonalInfo.name, 0)"></i>
                                        </div>
                                        <div class="card-footer clearfix" v-if="mdcSelectPersonal == 7">
                                            <div class="col-md-5">
                                                <input type="text" class="form-control float-left" placeholder="Krankenakteneintrag hinzufügen" maxlength="225" autocomplete="off" v-model="fahndungText">
                                            </div>
                                            <i class="fa-solid fa-circle-plus ml-2 icongreen" style="font-size: 1.35vw; margin-top: 0.3vw" @click="createStrafakteneintrag(selectedPersonalInfo.name, 2)"></i>
                                            <a href="javascript:void(0)" class="btn btn-sm btn-primary float-right" style="color:white" @click="selectMDC = 0">Zurück zum Dashboard</a>
                                        </div>
                                        <div class="card-footer clearfix" v-if="mdcSelectPersonal == 1">
                                            <a href="javascript:void(0)" class="btn btn-sm btn-primary float-right" style="color:white" @click="selectMDC = 0">Zurück zum Dashboard</a>
                                            <div class="col-md-5">
                                                <input type="text" class="form-control float-left" placeholder="Kommentar hinzufügen" maxlength="225" autocomplete="off" v-model="fahndungText">
                                            </div>
                                            <i class="fa-solid fa-circle-plus ml-2 icongreen" style="font-size: 1.35vw; margin-top: 0.3vw" @click="createStrafakteneintrag(selectedPersonalInfo.name, 1)"></i>
                                        </div>
                                        <div class="card-footer clearfix" v-if="mdcSelectPersonal == 2">
                                            <a href="javascript:void(0)" class="btn btn-sm btn-primary float-right" style="color:white" @click="selectMDC = 0">Zurück zum Dashboard</a>
                                            <div class="col-md-5">
                                                <input type="text" class="form-control float-left" placeholder="Grund für Punkt in SA/Sperre" maxlength="225" autocomplete="off" v-model="fahndungText">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card-body" style="height:100%" v-if="selectMDC == 4">
                                        <div class="card animate__animated animate__pulse" style="height:100%">
                                            <div class="card-header border-transparent">
                                                <h3 class="card-title" style="font-family: 'Exo', sans-serif; font-size: 0.8vw">Personensuche
                                                    - Ergebnisse</h3>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="card-body p-0" style="height: 18.5vw" v-if="personalInfo.length > 0">
                                                        <ul class="users-list clearfix">
                                                            <li v-for="personal in personalInfo" :key="personal.ID">
                                                                <img v-bind:src="personal.Screen" style="max-height: 112px;border-radius: 10%;border: 1px solid #adb5bd; width: 10vw;">
                                                                <a class="users-list-name" style="display: block;font-size: 0.875rem;" href="#" @click="navigate('selectPersonal', personal.ID)"><strong>{{personal.Name}}</strong></a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                    <div class="card-body p-0 text-center" style="height: 18.5vw" v-else>
                                                        <h5 style="margin-top: 1.5vw">Keine Ergebnisse gefunden!</h5>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card-body" style="height:100%" v-if="selectMDC == 3">
                                        <div class="card" style="height:100%">
                                            <div class="card-header border-transparent">
                                                <h3 class="card-title" style="font-family: 'Exo', sans-serif; font-size: 0.8vw">Neue Fahndung
                                                    erstellen</h3>
                                            </div>
                                            <div class="card-body p-0 mb-2" style="max-height: 18.5vw">
                                                <div class="col-md-12">
                                                    <div class="col-md-12 float-left">
                                                        <div class="card card-primary card-outline">
                                                            <div class="card-body box-profile">
                                                                <ul class="list-group list-group-bordered">
                                                                    <li>
                                                                        <b>Ersteller</b> <a class="float-right">{{fahndungCreator}}</a>
                                                                    </li>
                                                                    <li>
                                                                        <b>Datum</b> <a class="float-right">{{timeConverter(fahndungTimestamp)}}</a>
                                                                    </li>
                                                                    <li>
                                                                        <b>Bearbeiter</b> <a class="float-right">n/A</a>
                                                                    </li>
                                                                    <li class="mt-2">
                                                                        <textarea style="resize: none;" class="form-control" maxlength="575" rows="9" v-model="fahndungText" placeholder="Text ..."></textarea>
                                                                    </li>
                                                                </ul>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="card-footer clearfix">
                                            <a href="javascript:void(0)" class="btn btn-sm btn-primary float-right" style="color:white" @click="createNewFahndung()">Fahndung erstellen</a>
                                        </div>
                                    </div>
                                    <div class="card-body" style="height:100%" v-if="selectMDC == 2">
                                        <div class="card" style="height:100%">
                                            <div class="card-header border-transparent">
                                                <h3 class="card-title" style="font-family: 'Exo', sans-serif; font-size: 0.8vw">
                                                    Fahndungsinformationen - FHDNG-{{this.selectedFahndung.id}} <span class="badge badge-warning">{{getFahndungsStatus(this.selectedFahndung.status)}}</span>
                                                </h3>
                                            </div>
                                            <div class="card-body p-0 mb-2" style="max-height: 18.5vw">
                                                <div class="col-md-12">
                                                    <div class="col-md-4 float-left">
                                                        <div class="card card-primary card-outline">
                                                            <div class="card-body box-profile">
                                                                <ul class="list-group list-group-bordered">
                                                                    <li>
                                                                        <b>Erstellt von</b> <a class="float-right">{{this.selectedFahndung.creator}}</a>
                                                                    </li>
                                                                    <li>
                                                                        <b>Datum</b> <a class="float-right">{{timeConverter(this.selectedFahndung.timestamp)}}</a>
                                                                    </li>
                                                                    <li>
                                                                        <b>Bearbeiter</b> <a class="float-right">{{this.selectedFahndung.editor}}</a>
                                                                    </li>
                                                                    <li class="mt-2">
                                                                        <textarea style="resize: none;" class="form-control" rows="9" placeholder="Text ..." v-model="this.selectedFahndung.text" disabled></textarea>
                                                                    </li>
                                                                </ul>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="card" style="overflow-x: auto;max-height:auto">
                                                            <div class="card-header border-transparent">
                                                                <h3 class="card-title" style="font-family: 'Exo', sans-serif; font-size: 0.8vw">
                                                                    Kommentare</h3>
                                                            </div>
                                                            <div class="card-body p-0" style="max-height: 15.9vw">
                                                                <div class="table-responsive">
                                                                    <table class="table">
                                                                        <thead>
                                                                            <tr>
                                                                                <th>Kommentar</th>
                                                                                <th>Datum</th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tr v-for="fahndungskommentar in fahndungsKommentare" :key="fahndungskommentar.id">
                                                                            <td>{{fahndungskommentar.text}}</td>
                                                                            <td>{{timeConverter(fahndungskommentar.timestamp)}}</td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="card-footer clearfix" v-if="this.selectedFahndung.status != 2">
                                            <div class="col-md-5">
                                                <input type="text" class="form-control float-left" placeholder="Kommentar" maxlength="225" autocomplete="off" v-model="fahndungText">
                                            </div>
                                            <i class="fa-solid fa-circle-plus ml-2 icongreen" style="font-size: 1.35vw; margin-top: 0.4vw" @click="createFahndungKommentar()"></i>
                                            <a href="javascript:void(0)" class="btn btn-sm btn-primary float-right ml-3" style="color:white" @click="updateFahndungsStatus(1)">Bearbeitung übernehmen</a>
                                            <a href="javascript:void(0)" class="btn btn-sm btn-primary float-right" style="color:white" @click="updateFahndungsStatus(2)">Fahndung als erledigt setzen</a>
                                        </div>
                                        <div class="card-footer clearfix text-center" v-if="this.selectedFahndung.status == 2 && selectMDC == 2">
                                            <h5>Fahndung wurde abgeschlossen!</h5>
                                        </div>
                                    </div>
                                    <div class="card-body" style="height:100%" v-if="selectMDC == 9">
                                        <div class="card" style="height:100%">
                                            <div class="card-header border-transparent">
                                                <h3 class="card-title" style="font-family: 'Exo', sans-serif; font-size: 0.8vw">
                                                    Dispatchinformationen - DSPTH-{{selectedDispatch.id}} <span class="badge badge-warning">{{selectedDispatch.status}}</span>
                                                </h3>
                                            </div>
                                            <div class="card-body p-0 mb-2" style="max-height: 18.5vw">
                                                <div class="col-md-12">
                                                    <div class="col-md-4 float-left">
                                                        <div class="card card-primary card-outline">
                                                            <div class="card-body box-profile">
                                                                <ul class="list-group list-group-bordered">
                                                                    <li>
                                                                        <b>Erstellt von</b> <a class="float-right">{{selectedDispatch.name}}</a>
                                                                    </li>
                                                                    <li>
                                                                        <b>Datum</b> <a class="float-right">{{timeConverter(selectedDispatch.timestamp)}}</a>
                                                                    </li>
                                                                    <li>
                                                                        <b>Bearbeiter</b> <a class="float-right">{{selectedDispatch.editor}}</a>
                                                                    </li>
                                                                    <li class="mt-2">
                                                                        <textarea style="resize: none;" class="form-control" maxlength="225" rows="8" placeholder="Text ..." v-model="selectedDispatch.text" disabled></textarea>
                                                                    </li>
                                                                </ul>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="card" style="overflow-x: auto;max-height:auto">
                                                            <div class="card-header border-transparent">
                                                                <h3 class="card-title" style="font-family: 'Exo', sans-serif; font-size: 0.8vw">
                                                                    Kommentare</h3>
                                                            </div>
                                                            <div class="card-body p-0" style="max-height: 15.9vw">
                                                                <div class="table-responsive">
                                                                    <table class="table">
                                                                        <thead>
                                                                            <tr>
                                                                                <th>Kommentar</th>
                                                                                <th>Datum</th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tr v-for="fahndungskommentar in fahndungsKommentare" :key="fahndungskommentar.id">
                                                                            <td>{{fahndungskommentar.text}}</td>
                                                                            <td>{{timeConverter(fahndungskommentar.timestamp)}}</td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="card-footer clearfix" v-if="selectedDispatch.status != 'Abgeschlossen'">
                                            <div class="col-md-5">
                                                <input type="text" class="form-control float-left" placeholder="Kommentar" maxlength="225" autocomplete="off" v-model="fahndungText">
                                            </div>
                                            <i class="fa-solid fa-circle-plus ml-2 icongreen" style="font-size: 1.35vw; margin-top: 0.4vw" @click="createDispatchKommentar()"></i>
                                            <a href="javascript:void(0)" class="btn btn-sm btn-primary float-right ml-3" style="color:white" @click="updateDispatchStatus('In Bearbeitung')">Bearbeitung übernehmen</a>
                                            <a href="javascript:void(0)" class="btn btn-sm btn-primary float-right" style="color:white" @click="updateDispatchStatus('Abgeschlossen')">Dispatch als erledigt setzen</a>
                                        </div>
                                        <div class="card-footer clearfix text-center" v-if="selectedDispatch.status == 'Abgeschlossen' && selectMDC == 9">
                                            <h5>Dispatch wurde bearbeitet!</h5>
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
    name: 'mdc',
    data: function () {
        return {
            //Allgemeines
            showMDC: false,
            selectMDC: 0,
            mdcSelectPersonal: 0,
            searchelement: '',
            searchelement2: '',
            otherInfo: [],
            factionRang: 0,
            faction: 0,
            weaponText: '',
            //Fahndung
            fahndungen: [],
            selectedFahndung: '',
            fahndungsKommentare: [],
            fahndungText: '',
            fahndungTimestamp: '',
            fahndungCreator: '',
            //Undercover
            undercoverText: '',
            //Dispatches
            dispatchInfo: [],
            selectedDispatch: '',
            //Personen
            personalInfo: [],
            selectedPersonalInfo: [],
            policeFile: [],
            licStatus: [],
            carInfo: [],
            houseInfo: [],
            bizzInfo: [],
            handyInfo: [],
            weaponInfo: [],
            //Nummernschild
            plateInfo: [],
            numberplatetext: [],
            //Arrested
            arrestInfo: [],
            //Blitzer
            blitzerInfo: [],
            //Firmen
            firmenInfo: [],
            //Waffenbestellung
            orderInfo: [],
            orderInfo2: [],
            weight: 0,
            //Duty
            userInfo: [],
            //Staatsankündigung
            allMessage: '',
            //Documents
            documents: [],
            doclink: '',
            //Cooldowns
            clicked: (Date.now() / 1000),
            fahndungenLoaded: (Date.now() / 1000),
            fahndungenKommentareLoaded: (Date.now() / 1000),
            dispatchKommentareLoaded: (Date.now() / 1000),
            playerLoaded: (Date.now() / 1000),
            plateLoaded: (Date.now() / 1000),
            usersLoaded: (Date.now() / 1000),
            carsLoaded: (Date.now() / 1000),
            housesLoaded: (Date.now() / 1000),
            weaponsLoaded: (Date.now() / 1000),
            docLoaded: (Date.now() / 1000),
            bizzLoaded: (Date.now() / 1000),
            weaponLoaded: (Date.now() / 1000),
            invoiceLoaded: (Date.now() / 1000),
            handyLocated: (Date.now() / 1000),
            weaponLocated: (Date.now() / 1000),
            dispatchesLoaded: (Date.now() / 1000),
            firmenLoaded: (Date.now() / 1000),
            blitzerLoaded: (Date.now() / 1000),
            cctvLoaded: (Date.now() / 1000),
            arrestedLoaded: (Date.now() / 1000),
        }
    },
    mounted() {
        document.body.classList.add("dark-mode");
    },
    computed: {
        filteredList() {
            return this.filter1(this.fahndungen)
        },
        filteredList2() {
            return this.filter2(this.dispatchInfo)
        },
        filteredList3() {
            return this.filter3(this.firmenInfo)
        },
    },
    methods: {
        filter1: function (fahndungen) {
            return fahndungen.filter(fahndung => {
                return fahndung.text.toLowerCase().includes(this.searchelement.toLowerCase())
            })
        },
        filter2: function (dispatches) {
            return dispatches.filter(dispatch => {
                return dispatch.text.toLowerCase().includes(this.searchelement.toLowerCase())
            })
        },
        filter3: function (firmen) {
            return firmen.filter(firma => {
                return firma.name.toLowerCase().includes(this.searchelement.toLowerCase())
            })
        },
        getImgUrl(pic) {
            try {
                return require('../assets/images/inventory/' + pic + '.png')
            } catch (e) {
                return require('../assets/images/inventory/fragezeichen.png')
            }
        },
        turnOff: function () {
            // eslint-disable-next-line no-undef
            mp.trigger("Client:MDCSettings", 'showmdc', 'off');
        },
        liveMap: function () {
            if ((Date.now() / 1000) > this.clicked) {
                // eslint-disable-next-line no-undef
                mp.trigger('Client:MDCSettings', 'setlivemap', '');
                this.clicked = (Date.now() / 1000) + (2);
            }
        },
        closedZone: function (zoneText) {
            if ((Date.now() / 1000) > this.clicked) {
                // eslint-disable-next-line no-undef
                mp.trigger('Client:MDCSettings', 'closedzone', '' + zoneText);
                this.clicked = (Date.now() / 1000) + (2);
            }
        },
        createMessageToAll: function () {
            if ((Date.now() / 1000) > this.clicked) {
                if (!this.allMessage) return;
                // eslint-disable-next-line no-undef
                mp.trigger('Client:MDCSettings', 'messagetoall', this.allMessage);
                this.clicked = (Date.now() / 1000) + (1);
                this.allMessage = '';
            }
        },
        countOrder: function () {
            this.weight = 0;
            if (this.faction == 1) {
                if (this.orderInfo2[0]) {
                    this.weight += this.orderInfo2[0] * 475;
                }
                if (this.orderInfo2[1]) {
                    this.weight += this.orderInfo2[1] * 650;
                }
                if (this.orderInfo2[2]) {
                    this.weight += this.orderInfo2[2] * 900;
                }
                if (this.orderInfo2[3]) {
                    this.weight += this.orderInfo2[3] * 975;
                }
                if (this.orderInfo2[4]) {
                    this.weight += this.orderInfo2[4] * 925;
                }
                if (this.orderInfo2[5]) {
                    this.weight += this.orderInfo2[5] * 2850;
                }
                if (this.orderInfo2[6]) {
                    this.weight += this.orderInfo2[6] * 3850;
                }
                if (this.orderInfo2[7]) {
                    this.weight += this.orderInfo2[7] * 3500;
                }
                if (this.orderInfo2[8]) {
                    this.weight += this.orderInfo2[8] * 850;
                }
                if (this.orderInfo2[9]) {
                    this.weight += this.orderInfo2[9] * 700;
                }
                if (this.orderInfo2[10]) {
                    this.weight += this.orderInfo2[10] * 1850;
                }
                if (this.orderInfo2[11]) {
                    this.weight += this.orderInfo2[11] * 3;
                }
                if (this.orderInfo2[12]) {
                    this.weight += this.orderInfo2[12] * 4;
                }
                if (this.orderInfo2[13]) {
                    this.weight += this.orderInfo2[13] * 5;
                }
                if (this.orderInfo2[14]) {
                    this.weight += this.orderInfo2[14] * 65;
                }
                if (this.orderInfo2[15]) {
                    this.weight += this.orderInfo2[15] * 2500;
                }
            } else if (this.faction == 2) {
                if (this.orderInfo2[0]) {
                    this.weight += this.orderInfo2[0] * 2500;
                }
                if (this.orderInfo2[1]) {
                    this.weight += this.orderInfo2[1] * 650;
                }
                if (this.orderInfo2[2]) {
                    this.weight += this.orderInfo2[2] * 2500;
                }
                if (this.orderInfo2[3]) {
                    this.weight += this.orderInfo2[3] * 50;
                }
                if (this.orderInfo2[4]) {
                    this.weight += this.orderInfo2[4] * 50;
                }
                if (this.orderInfo2[5]) {
                    this.weight += this.orderInfo2[5] * 50;
                }
                if (this.orderInfo2[6]) {
                    this.weight += this.orderInfo2[6] * 50;
                }
                if (this.orderInfo2[7]) {
                    this.weight += this.orderInfo2[7] * 50;
                }
                if (this.orderInfo2[8]) {
                    this.weight += this.orderInfo2[8] * 25;
                }
                if (this.orderInfo2[9]) {
                    this.weight += this.orderInfo2[9] * 3000;
                }
                if (this.orderInfo2[10]) {
                    this.weight += this.orderInfo2[10] * 1850;
                }
                if (this.orderInfo2[11]) {
                    this.weight += this.orderInfo2[11] * 65;
                }
                if (this.orderInfo2[12]) {
                    this.weight += this.orderInfo2[12] * 475;
                }
            } else if (this.faction == 3) {
                if (this.orderInfo2[0]) {
                    this.weight += this.orderInfo2[0] * 650;
                }
                if (this.orderInfo2[1]) {
                    this.weight += this.orderInfo2[1] * 4500;
                }
                if (this.orderInfo2[2]) {
                    this.weight += this.orderInfo2[2] * 1500;
                }
                if (this.orderInfo2[3]) {
                    this.weight += this.orderInfo2[3] * 65;
                }
                if (this.orderInfo2[4]) {
                    this.weight += this.orderInfo2[4] * 350;
                }
                if (this.orderInfo2[5]) {
                    this.weight += this.orderInfo2[5] * 700;
                }
                if (this.orderInfo2[6]) {
                    this.weight += this.orderInfo2[6] * 2500;
                }

            }
        },
        openDocument: function (link) {
            if ((Date.now() / 1000) > this.clicked) {
                this.doclink = link;
                this.selectMDC = 18;
                this.clicked = (Date.now() / 1000) + (1);
            }
        },
        createDocument: function () {
            if ((Date.now() / 1000) > this.clicked) {
                if (this.fahndungText.length < 10 || this.fahndungCreator.length < 3) {
                    // eslint-disable-next-line no-undef
                    mp.trigger("Client:SendNotificationWithoutButton", 'Ungültige Eingaben!', 'error', 'top-left', '2250');
                    return;
                }
                var params = this.fahndungText + '|' + this.fahndungCreator;
                // eslint-disable-next-line no-undef
                mp.trigger('Client:MDCSettings', 'createdocument', params);
                this.clicked = (Date.now() / 1000) + (2);
                this.fahndungText = '';
                this.fahndungCreator = '';
            }
        },
        deleteDocument: function (id) {
            if ((Date.now() / 1000) > this.clicked) {
                // eslint-disable-next-line no-undef
                mp.trigger('Client:MDCSettings', 'deletedocument', "" + id);
                this.clicked = (Date.now() / 1000) + (1);
            }
        },
        loadDocuments: function () {
            if ((Date.now() / 1000) > this.clicked) {
                if ((Date.now() / 1000) > this.docLoaded) {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:MDCSettings', 'getdocuments');
                    this.docLoaded = (Date.now() / 1000) + (60);
                }
                this.fahndungText = '';
                this.fahndungCreator = '';
                this.selectMDC = 17;
                this.clicked = (Date.now() / 1000) + (1);
            }
        },
        showdocuments: function (data) {
            if (data) {
                this.documents = JSON.parse(data);
            } else {
                this.documents = [];
            }
        },
        showWeaponOrder: function () {
            if ((Date.now() / 1000) > this.clicked) {
                if ((Date.now() / 1000) > this.weaponsLoaded) {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:MDCSettings', 'getweapons');
                    this.weaponsLoaded = (Date.now() / 1000) + (10);
                }
                this.selectMDC = 11;
                this.clicked = (Date.now() / 1000) + (1);
            }
        },
        showweapons: function (data) {
            if (data) {
                this.orderInfo = data.split(',');
                for (let i = 0; i < this.orderInfo.length; i++) {
                    this.orderInfo2.push('0');
                }
                this.$forceUpdate();
            }
        },
        orderWeapons: function () {
            if ((Date.now() / 1000) > this.clicked) {
                if (!this.weight) {
                    // eslint-disable-next-line no-undef
                    mp.trigger("Client:SendNotificationWithoutButton", 'Ungültige Bestellung!', 'error', 'top-left', '2250');
                    return;
                }
                if (this.weight > 135000) {
                    // eslint-disable-next-line no-undef
                    mp.trigger("Client:SendNotificationWithoutButton", 'Ungültiges Bestellgewicht!', 'error', 'top-left', '2250');
                    return;
                }
                if (this.faction == 1) {
                    for (let i = 0; i < this.orderInfo.length; i++) {
                        if (this.orderInfo2[i].length <= 0) {
                            // eslint-disable-next-line no-undef
                            mp.trigger("Client:SendNotificationWithoutButton", 'Ungültige Bestellposition ' + (i + 1) + '!', 'error', 'top-left', '3250');
                            return;
                        }
                        if ((parseInt(this.orderInfo[i]) + parseInt(this.orderInfo2[i])) > 50 && i != 7 && i != 6 && i != 5 && i != 13 && i != 12 && i != 11 && i != 15) {
                            // eslint-disable-next-line no-undef
                            mp.trigger("Client:SendNotificationWithoutButton", 'Ungültige Bestellposition ' + (i + 1) + ', die Waffenkammer hat max. Platz für 50/Stck!', 'error', 'top-left', '3250');
                            return;
                        }
                        if ((parseInt(this.orderInfo[i]) + parseInt(this.orderInfo2[i])) > 25 && (i == 7 || i == 6 || i == 5)) {
                            // eslint-disable-next-line no-undef
                            mp.trigger("Client:SendNotificationWithoutButton", 'Ungültige Bestellposition ' + (i + 1) + ', die Waffenkammer hat max. Platz für 25/Stck!', 'error', 'top-left', '3250');
                            return;
                        }
                        if ((parseInt(this.orderInfo[i]) + parseInt(this.orderInfo2[i])) > 1250 && (i == 13 || i == 12 || i == 11)) {
                            // eslint-disable-next-line no-undef
                            mp.trigger("Client:SendNotificationWithoutButton", 'Ungültige Bestellposition ' + (i + 1) + ', die Waffenkammer hat max. Platz für 1250/Stck!', 'error', 'top-left', '3250');
                            return;
                        }
                        if ((parseInt(this.orderInfo[i]) + parseInt(this.orderInfo2[i])) > 10 && i == 15) {
                            // eslint-disable-next-line no-undef
                            mp.trigger("Client:SendNotificationWithoutButton", 'Ungültige Bestellposition ' + (i + 1) + ', die Waffenkammer hat max. Platz für 10/Stck!', 'error', 'top-left', '3250');
                            return;
                        }
                    }
                } else if (this.faction == 2) {
                    for (let i = 0; i < this.orderInfo.length; i++) {
                        if (this.orderInfo2[i].length <= 0) {
                            // eslint-disable-next-line no-undef
                            mp.trigger("Client:SendNotificationWithoutButton", 'Ungültige Bestellposition ' + (i + 1) + '!', 'error', 'top-left', '3250');
                            return;
                        }
                        if ((parseInt(this.orderInfo[i]) + parseInt(this.orderInfo2[i])) > 35 && (i == 0 || i == 1 || i == 2 || i == 11 || i == 10)) {
                            // eslint-disable-next-line no-undef
                            mp.trigger("Client:SendNotificationWithoutButton", 'Ungültige Bestellposition ' + (i + 1) + ', die Waffenkammer hat max. Platz für 35/Stck!', 'error', 'top-left', '3250');
                            return;
                        }

                        if ((parseInt(this.orderInfo[i]) + parseInt(this.orderInfo2[i])) > 5 && (i == 3)) {
                            // eslint-disable-next-line no-undef
                            mp.trigger("Client:SendNotificationWithoutButton", 'Ungültige Bestellposition ' + (i + 1) + ', die Waffenkammer hat max. Platz für 5/Stck!', 'error', 'top-left', '3250');
                            return;
                        }

                        if ((parseInt(this.orderInfo[i]) + parseInt(this.orderInfo2[i])) > 25 && (i == 12)) {
                            // eslint-disable-next-line no-undef
                            mp.trigger("Client:SendNotificationWithoutButton", 'Ungültige Bestellposition ' + (i + 1) + ', die Waffenkammer hat max. Platz für 25/Stck!', 'error', 'top-left', '3250');
                            return;
                        }

                        if ((parseInt(this.orderInfo[i]) + parseInt(this.orderInfo2[i])) > 115) {
                            // eslint-disable-next-line no-undef
                            mp.trigger("Client:SendNotificationWithoutButton", 'Ungültige Bestellposition ' + (i + 1) + ', die Waffenkammer hat max. Platz für 115/Stck!', 'error', 'top-left', '3250');
                            return;
                        }
                    }
                } else if (this.faction == 3) {
                    for (let i = 0; i < this.orderInfo.length; i++) {
                        if (this.orderInfo2[i].length <= 0) {
                            // eslint-disable-next-line no-undef
                            mp.trigger("Client:SendNotificationWithoutButton", 'Ungültige Bestellposition ' + (i + 1) + '!', 'error', 'top-left', '3250');
                            return;
                        }
                        if ((parseInt(this.orderInfo[i]) + parseInt(this.orderInfo2[i])) > 25 && (i == 0 || i == 1 || i == 2 || i == 3 || i == 4 || i == 5 | i == 6)) {
                            // eslint-disable-next-line no-undef
                            mp.trigger("Client:SendNotificationWithoutButton", 'Ungültige Bestellposition ' + (i + 1) + ', die Waffenkammer hat max. Platz für 25/Stck!', 'error', 'top-left', '3250');
                            return;
                        }
                    }
                }
                // eslint-disable-next-line no-undef
                mp.trigger('Client:MDCSettings', 'orderweapons', this.orderInfo2.join(','));
                this.clicked = (Date.now() / 1000) + (2);
            }
        },
        deleteDispatch: function (id) {
            if ((Date.now() / 1000) > this.clicked) {
                // eslint-disable-next-line no-undef
                mp.trigger('Client:MDCSettings', 'deletedispatch', id);
                this.clicked = (Date.now() / 1000) + (1);
            }
        },
        createDispatchKommentar: function () {
            if ((Date.now() / 1000) > this.clicked) {
                if (!this.fahndungText || this.fahndungText.length > 225) {
                    // eslint-disable-next-line no-undef
                    mp.trigger("Client:SendNotificationWithoutButton", 'Ungültige Eingabe!', 'error', 'top-left', '2250');
                    return;
                }
                if (this.selectedDispatch.status != 'In Bearbeitung') {
                    // eslint-disable-next-line no-undef
                    mp.trigger("Client:SendNotificationWithoutButton", 'Das Dispatch befindet sich nicht in Bearbeitung!', 'error', 'top-left', '2250');
                    return;
                }
                var params = this.fahndungText + '|' + this.selectedDispatch.id;
                // eslint-disable-next-line no-undef
                mp.trigger('Client:MDCSettings', 'createdispatchkommentar', params);
                this.clicked = (Date.now() / 1000) + (2);
            }
        },
        updateDispatchStatus: function (update) {
            if ((Date.now() / 1000) > this.clicked) {
                if (update == 1 && this.selectedDispatch.status == 'Abgeschlossen') {
                    // eslint-disable-next-line no-undef
                    mp.trigger("Client:SendNotificationWithoutButton", 'Du kannst die Bearbeitung jetzt nicht mehr übernehmen!', 'error', 'top-left', '2250');
                    return;
                }
                if (update == 2 && this.selectedDispatch.status == 'Abgeschlossen') {
                    // eslint-disable-next-line no-undef
                    mp.trigger("Client:SendNotificationWithoutButton", 'Die Fahndung wurde bereits als erledigt markiert!', 'error', 'top-left', '2250');
                    return;
                }
                var params = update + '|' + this.selectedDispatch.id;
                // eslint-disable-next-line no-undef
                mp.trigger('Client:MDCSettings', 'updatedispatchstatus', params);
                this.selectedDispatch.status = update;
                this.clicked = (Date.now() / 1000) + (2);
                this.$forceUpdate();
            }
        },
        selectDispatches: function () {
            if ((Date.now() / 1000) > this.clicked) {
                this.searchelement = '';
                this.selectMDC = 8;
                if ((Date.now() / 1000) > this.dispatchesLoaded) {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:MDCSettings', 'loaddispatches', '');
                    this.dispatchesLoaded = (Date.now() / 1000) + (5);
                    this.dispatchKommentareLoaded = (Date.now() / 1000);
                }
                this.clicked = (Date.now() / 1000) + (1);
            }
        },
        resetDispatchCooldown: function () {
            this.dispatchesLoaded = (Date.now() / 1000);
            this.dispatchKommentareLoaded = (Date.now() / 1000);
        },
        showdispatches: function (data) {
            if (data != 'n/A') {
                this.dispatchInfo = JSON.parse(data);
                this.otherInfo[3] = this.dispatchInfo.length;
            } else {
                this.dispatchInfo = [];
                this.otherInfo[3] = 0;
            }
        },
        selectFirmen: function () {
            if ((Date.now() / 1000) > this.clicked) {
                this.searchelement = '';
                this.selectMDC = 10;
                if ((Date.now() / 1000) > this.firmenLoaded) {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:MDCSettings', 'selectfirmen', '');
                    this.firmenLoaded = (Date.now() / 1000) + (60);
                }
                this.clicked = (Date.now() / 1000) + (1);
            }
        },
        showfirmen: function (data) {
            if (data != 'n/A') {
                this.firmenInfo = JSON.parse(data);
            } else {
                this.firmenInfo = [];
            }
        },
        selectArrested: function () {
            if ((Date.now() / 1000) > this.clicked) {
                this.selectMDC = 14;
                if ((Date.now() / 1000) > this.arrestedLoaded) {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:MDCSettings', 'selectarrested', '');
                    this.arrestedLoaded = (Date.now() / 1000) + (30);
                }
                this.clicked = (Date.now() / 1000) + (1);
            }
        },
        showarrested: function (data) {
            if (data != 'n/A') {
                this.arrestInfo = JSON.parse(data);
            } else {
                this.arrestInfo = [];
            }
        },
        selectCars: function (id) {
            if ((Date.now() / 1000) > this.clicked) {
                this.mdcSelectPersonal = 3;
                if ((Date.now() / 1000) > this.carsLoaded) {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:MDCSettings', 'selectcars', '' + id);
                    this.carsLoaded = (Date.now() / 1000) + (30);
                }
                this.clicked = (Date.now() / 1000) + (1);
            }
        },
        showcars: function (data) {
            if (data != 'n/A') {
                this.carInfo = JSON.parse(data);
            } else {
                this.carInfo = [];
            }
        },
        selectHouses: function (name) {
            if ((Date.now() / 1000) > this.clicked) {
                this.mdcSelectPersonal = 4;
                if ((Date.now() / 1000) > this.housesLoaded) {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:MDCSettings', 'selecthouses', name);
                    this.housesLoaded = (Date.now() / 1000) + (60);
                }
                this.clicked = (Date.now() / 1000) + (1);
            }
        },
        showhouses: function (data) {
            if (data != 'n/A') {
                this.houseInfo = JSON.parse(data);
            } else {
                this.houseInfo = [];
            }
        },
        selectBizz: function (name) {
            if ((Date.now() / 1000) > this.clicked) {
                this.mdcSelectPersonal = 5;
                if ((Date.now() / 1000) > this.bizzLoaded) {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:MDCSettings', 'selectbizz', name);
                    this.bizzLoaded = (Date.now() / 1000) + (60);
                }
                this.clicked = (Date.now() / 1000) + (1);
            }
        },
        showbizz: function (data) {
            if (data != 'n/A') {
                this.bizzInfo = JSON.parse(data);
            } else {
                this.bizzInfo = [];
            }
        },
        selectWeapon: function (name) {
            if ((Date.now() / 1000) > this.clicked) {
                this.mdcSelectPersonal = 8;
                if ((Date.now() / 1000) > this.weaponLoaded) {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:MDCSettings', 'selectweapon', name);
                    this.weaponLoaded = (Date.now() / 1000) + (30);
                }
                this.clicked = (Date.now() / 1000) + (1);
            }
        },
        showweapon2: function (data) {
            if (data != 'n/A') {
                this.weaponInfo = JSON.parse(data);
            } else {
                this.weaponInfo = [];
            }
        },
        selectInvoice: function (name) {
            if ((Date.now() / 1000) > this.clicked) {
                this.mdcSelectPersonal = 6;
                if ((Date.now() / 1000) > this.invoiceLoaded) {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:MDCSettings', 'selectinvoice', name);
                    this.invoiceLoaded = (Date.now() / 1000) + (60);
                }
                this.clicked = (Date.now() / 1000) + (1);
            }
        },
        showinvoice: function (data) {
            if (data != 'n/A') {
                this.invoiceInfo = JSON.parse(data);
                this.$forceUpdate();
            } else {
                this.invoiceInfo = [];
            }
        },
        selectBlitzer: function () {
            if ((Date.now() / 1000) > this.clicked) {
                if ((Date.now() / 1000) > this.blitzerLoaded) {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:MDCSettings', 'selectblitzer', 'n/A');
                    this.blitzerLoaded = (Date.now() / 1000) + (60);
                }
                this.selectMDC = 13;
                this.clicked = (Date.now() / 1000) + (1);
            }
        },
        showblitzer: function (data) {
            if (data != 'n/A') {
                this.blitzerInfo = JSON.parse(data);
            } else {
                this.blitzerInfo = [];
            }
        },
        selectCCTV: function () {
            if ((Date.now() / 1000) > this.clicked) {
                if ((Date.now() / 1000) > this.cctvLoaded) {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:MDCSettings', 'selectcctv', 'n/A');
                    this.cctvLoaded = (Date.now() / 1000) + (60);
                }
                this.selectMDC = 15;
                this.clicked = (Date.now() / 1000) + (1);
            }
        },
        showcctv: function (data) {
            if (data != 'n/A') {
                this.cctvInfo = JSON.parse(data);
                this.$forceUpdate();
            } else {
                this.cctvInfo = [];
            }
        },
        selectUsers: function (check = 0) {
            if ((Date.now() / 1000) > this.clicked) {
                if ((Date.now() / 1000) > this.usersLoaded) {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:MDCSettings', 'selectusers', 'n/A');
                    this.usersLoaded = (Date.now() / 1000) + (15);
                    if (check == 1) {
                        // eslint-disable-next-line no-undef
                        mp.trigger('Client:SendNotificationWithoutButton', 'Ansicht aktualisiert!', 'success', 'top-left', 2750);
                    }
                } else {
                    if (check == 1) {
                        // eslint-disable-next-line no-undef
                        mp.trigger('Client:SendNotificationWithoutButton', 'Die Ansicht kann nur alle 30 Sekunden aktualisiert werden!', 'error', 'top-left', 2750);
                    }
                }
                this.carsLoaded = (Date.now() / 1000);
                this.housesLoaded = (Date.now() / 1000);
                this.bizzLoaded = (Date.now() / 1000);
                this.weaponLoaded = (Date.now() / 1000);
                this.invoiceLoaded = (Date.now() / 1000);
                this.selectMDC = 6;
                this.clicked = (Date.now() / 1000) + (1);
            }
        },
        showusers: function (data) {
            if (data != 'n/A') {
                this.userInfo = JSON.parse(data);
                this.otherInfo[1] = this.userInfo.length;
            } else {
                this.userInfo = [];
            }
        },
        locateWeapon(ident) {
            if ((Date.now() / 1000) > this.clicked) {
                if (!ident) return;
                if ((Date.now() / 1000) > this.weaponLocated) {
                    this.weaponLoaded = (Date.now() / 1000);
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:MDCSettings', 'locateweapon', ident);
                    this.clicked = (Date.now() / 1000) + (2);
                    this.weaponLocated = (Date.now() / 1000) + (10);
                } else {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:SendNotificationWithoutButton', 'Du kannst nur alle 10 Sekunden nach einer Waffe suchen!', 'error', 'top-left', 2750);
                }
            }
        },
        underCover(ident) {
            if ((Date.now() / 1000) > this.clicked) {
                if (!ident) return;
                // eslint-disable-next-line no-undef
                mp.trigger('Client:MDCSettings', 'undercover', ident);
                this.clicked = (Date.now() / 1000) + (2);
            }
        },
        showweapon: function (data) {
            try {
                this.selectMDC = 12;
                this.weaponInfo = JSON.parse(data);
            } catch (e) {
                // eslint-disable-next-line no-undef
                mp.trigger('Client:SendNotificationWithoutButton', 'Die Waffe wurde nicht in der Waffendatenbank gefunden!', 'error', 'top-left', 2750);
            }
        },
        locateHandy(name) {
            if ((Date.now() / 1000) > this.clicked) {
                if (!name) return;
                if ((Date.now() / 1000) > this.handyLocated) {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:MDCSettings', 'locatehandy', name);
                    this.clicked = (Date.now() / 1000) + (2);
                    this.handyLocated = (Date.now() / 1000) + (45);
                } else {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:SendNotificationWithoutButton', 'Du kannst nur alle 45 Sekunden ein Handy orten!', 'error', 'top-left', 2750);
                }
            }
        },
        lookCCTV(id) {
            if ((Date.now() / 1000) > this.clicked) {
                // eslint-disable-next-line no-undef
                mp.trigger('Client:MDCSettings', 'lookcctv', '' + id);
                this.clicked = (Date.now() / 1000) + (2);
            }
        },
        locatePlayer(position) {
            if ((Date.now() / 1000) > this.clicked) {
                let pos = position.split(',');
                // eslint-disable-next-line no-undef
                mp.trigger('Client:CreateWaypoint', pos[0], pos[1], -1);
                // eslint-disable-next-line no-undef
                mp.trigger('Client:SendNotificationWithoutButton', 'GPS Lokalisierung erfolgreich!', 'success', 'top-left', 2750);
                this.clicked = (Date.now() / 1000) + (2);
            }
        },
        setLic: function (lic, status, name) {
            if ((Date.now() / 1000) > this.clicked) {
                if (!this.fahndungText) {
                    // eslint-disable-next-line no-undef
                    mp.trigger("Client:SendNotificationWithoutButton", 'Bitte einen Grund eingeben!', 'error', 'top-left', '2250');
                    return;
                }
                var params = lic + ',' + status + ',' + name + ',' + this.fahndungText;
                // eslint-disable-next-line no-undef
                mp.trigger('Client:MDCSettings', 'setlic', params);
                this.clicked = (Date.now() / 1000) + (1);
            }
        },
        getLicStatus: function (status) {
            if (!status || status == 0) {
                return 'Nicht vorhanden';
            } else if (status == 1) {
                return 'Vorhanden';
            } else if (status > 1) {
                return 'Sperre bis: ' + this.timeConverter(status);
            }
        },
        createStrafakteneintrag(name, check) {
            if ((Date.now() / 1000) > this.clicked) {
                if (!this.fahndungText || this.fahndungText.length > 225) {
                    // eslint-disable-next-line no-undef
                    mp.trigger("Client:SendNotificationWithoutButton", 'Ungültige Eingabe!', 'error', 'top-left', '2250');
                }
                var params = name + '|' + this.fahndungText + '|' + check;
                // eslint-disable-next-line no-undef
                mp.trigger('Client:MDCSettings', 'createakteneintrag', params);
                var self = this;
                setTimeout(function () {
                    self.fahndungText = '';
                }, 225);
                this.clicked = (Date.now() / 1000) + (2);
            }
        },
        deleteStrafaktenEintrag(id, name, text) {
            if ((Date.now() / 1000) > this.clicked) {
                var params = id + '|' + name + '|' + text;
                // eslint-disable-next-line no-undef
                mp.trigger('Client:MDCSettings', 'deleteakteneintrag', params);
                this.clicked = (Date.now() / 1000) + (1);
            }
        },
        searchNumberPlate() {
            if ((Date.now() / 1000) > this.plateLoaded) {
                if (!this.numberplatetext || this.numberplatetext.length < 3 || this.numberplatetext.length > 15 || this.numberplatetext == 'n/A') {
                    // eslint-disable-next-line no-undef
                    mp.trigger("Client:SendNotificationWithoutButton", 'Ungültige Eingabe!', 'error', 'top-left', '2250');
                    return;
                }
                // eslint-disable-next-line no-undef
                mp.trigger('Client:MDCSettings', 'searchplate', this.numberplatetext);
                this.selectMDC = 16;
                this.plateLoaded = (Date.now() / 1000) + (3);
            }
        },
        showplate: function (data) {
            if (data != 'n/A') {
                this.plateInfo = JSON.parse(data);
                this.$forceUpdate();
            } else {
                this.plateInfo = [];
                this.selectMDC = 7;
            }
        },
        searchPlayer() {
            if ((Date.now() / 1000) > this.playerLoaded) {
                if (!this.searchelement2 || this.searchelement2.length <= 3 || this.searchelement2.length > 35) {
                    // eslint-disable-next-line no-undef
                    mp.trigger("Client:SendNotificationWithoutButton", 'Ungültige Eingabe!', 'error', 'top-left', '2250');
                    return;
                }
                // eslint-disable-next-line no-undef
                mp.trigger('Client:MDCSettings', 'searchplayer', this.searchelement2);
                this.selectMDC = 4;
                this.playerLoaded = (Date.now() / 1000) + (3);
            }
        },
        showplayer: function (data) {
            if (data != 'n/A') {
                this.personalInfo = JSON.parse(data);
            } else {
                this.personalInfo = [];
            }
        },
        showhandy: function (data) {
            if (data != 'n/A') {
                this.handyInfo = JSON.parse(data);
            } else {
                this.handyInfo = [];
            }
        },
        showpersonal: function (data) {
            if (data != 'n/A') {
                this.selectedPersonalInfo = JSON.parse(data);
                this.licStatus = this.selectedPersonalInfo.licenses.split('|');
                this.fahndungText = '';
                if (this.faction == 1) {
                    this.mdcSelectPersonal = 0;
                } else if (this.faction == 2) {
                    this.mdcSelectPersonal = 7;
                }
                this.selectMDC = 5;
            } else {
                this.selectedPersonalInfo = [];
            }
        },
        showpolicefile: function (data) {
            if (data != 'n/A') {
                this.policeFile = JSON.parse(data);
            } else {
                this.policeFile = [];
            }
        },
        updateeditor: function (data) {
            if (data != 'n/A') {
                var splitData = data.split(',');
                if (splitData[1] == 1) {
                    this.selectedFahndung.editor = splitData[0];
                } else if (splitData[1] == 2) {
                    this.selectedDispatch.editor = splitData[0];
                }
            }
        },
        showHideMDC: function () {
            this.showMDC = !this.showMDC;
        },
        showTheMDC: function (data, factionRang, faction) {
            this.faction = faction;
            this.factionRang = factionRang;
            if ((Date.now() / 1000) > this.fahndungenLoaded && this.faction == 1) {
                this.loadfahndungen();
                this.fahndungenLoaded = (Date.now() / 1000) + (5);
            }
            if ((Date.now() / 1000) > this.dispatchesLoaded && (this.faction == 2 || this.faction == 3)) {
                // eslint-disable-next-line no-undef
                mp.trigger('Client:MDCSettings', 'loaddispatches', '');
                this.dispatchesLoaded = (Date.now() / 1000) + (5);
                this.dispatchKommentareLoaded = (Date.now() / 1000);
            }
            if (data && data.length > 3) {
                this.otherInfo = data.split(',');
            }
            this.showMDC = !this.showMDC;
        },
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
        createNewFahndung: function () {
            if ((Date.now() / 1000) > this.clicked) {
                if (!this.fahndungText || this.fahndungText.length > 575) {
                    // eslint-disable-next-line no-undef
                    mp.trigger("Client:SendNotificationWithoutButton", 'Ungültige Eingabe!', 'error', 'top-left', '2250');
                    return;
                }
                var params = this.fahndungText + '|' + this.fahndungTimestamp;
                // eslint-disable-next-line no-undef
                mp.trigger('Client:MDCSettings', 'createnewfahndung', params);
                this.clicked = (Date.now() / 1000) + (2);
                var self = this;
                setTimeout(function () {
                    self.fahndungText = '';
                    self.selectMDC = 1;
                }, 225);
            }
        },
        createFahndungKommentar: function () {
            if ((Date.now() / 1000) > this.clicked) {
                if (!this.fahndungText || this.fahndungText.length > 225) {
                    // eslint-disable-next-line no-undef
                    mp.trigger("Client:SendNotificationWithoutButton", 'Ungültige Eingabe!', 'error', 'top-left', '2250');
                    return;
                }
                if (this.selectedFahndung.status != 1) {
                    // eslint-disable-next-line no-undef
                    mp.trigger("Client:SendNotificationWithoutButton", 'Die Fahndung befindet sich nicht in Bearbeitung!', 'error', 'top-left', '2250');
                    return;
                }
                var params = this.fahndungText + '|' + this.selectedFahndung.id;
                // eslint-disable-next-line no-undef
                mp.trigger('Client:MDCSettings', 'createfahndungskommentar', params);
                this.clicked = (Date.now() / 1000) + (2);
            }
        },
        updateFahndungsStatus: function (update) {
            if ((Date.now() / 1000) > this.clicked) {
                if (update == 1 && this.selectedFahndung.status == 2) {
                    // eslint-disable-next-line no-undef
                    mp.trigger("Client:SendNotificationWithoutButton", 'Du kannst die Bearbeitung jetzt nicht mehr übernehmen!', 'error', 'top-left', '2250');
                    return;
                }
                if (update == 2 && this.selectedFahndung.status == 2) {
                    // eslint-disable-next-line no-undef
                    mp.trigger("Client:SendNotificationWithoutButton", 'Die Fahndung wurde bereits als erledigt markiert!', 'error', 'top-left', '2250');
                    return;
                }
                var params = update + ',' + this.selectedFahndung.id;
                // eslint-disable-next-line no-undef
                mp.trigger('Client:MDCSettings', 'updatefahndungsstatus', params);
                this.selectedFahndung.status = update;
                this.clicked = (Date.now() / 1000) + (2);
                this.$forceUpdate();
            }
        },
        loadfahndungen: function () {
            // eslint-disable-next-line no-undef
            mp.trigger('Client:MDCSettings', 'loadfahndungen', 'n/A');
            this.fahndungenKommentareLoaded = (Date.now() / 1000);
        },
        showfahndungen: function (data) {
            this.fahndungen = JSON.parse(data);
            this.otherInfo[2] = this.fahndungen.length;
        },
        getFahndungsStatus: function (status) {
            if (status == 0) {
                return 'Offen';
            } else if (status == 1) {
                return 'In Bearbeitung';
            } else if (status == 2) {
                return 'Abgeschlossen';
            }
        },
        showkommentare: function (data) {
            this.fahndungsKommentare = JSON.parse(data);
        },
        navigate: function (navi, data = null) {
            if ((Date.now() / 1000) > this.clicked) {
                if (navi == 'createFahndung') {
                    this.fahndungText = '';
                    this.fahndungCreator = 'Du';
                    this.fahndungTimestamp = (Date.now() / 1000);
                    this.selectMDC = 3;
                } else if (navi == 'selectDispatch') {
                    if ((Date.now() / 1000) > this.dispatchKommentareLoaded) {
                        // eslint-disable-next-line no-undef
                        mp.trigger('Client:MDCSettings', 'loaddispatchkommentare', '' + data.id);
                        this.dispatchKommentareLoaded = (Date.now() / 1000) + (45);
                    }
                    this.fahndungText = '';
                    this.selectedDispatch = data;
                    this.selectMDC = 9;
                } else if (navi == 'showFahndungen') {
                    if ((Date.now() / 1000) > this.fahndungenLoaded) {
                        this.loadfahndungen();
                        this.fahndungenLoaded = (Date.now() / 1000) + (5);
                    }
                    this.searchelement = '';
                    this.selectMDC = 1;
                } else if (navi == 'selectFahndung') {
                    if ((Date.now() / 1000) > this.fahndungenKommentareLoaded) {
                        // eslint-disable-next-line no-undef
                        mp.trigger('Client:MDCSettings', 'loadfahndungenkommentare', '' + data.id);
                        this.fahndungenKommentareLoaded = (Date.now() / 1000) + (45);
                    }
                    this.fahndungText = '';
                    this.selectedFahndung = data;
                    this.selectMDC = 2;
                } else if (navi == 'selectPersonal') {
                    // eslint-disable-next-line no-undef
                    mp.trigger('Client:MDCSettings', 'selectpersonal', '' + data);
                }
                this.clicked = (Date.now() / 1000) + (1);
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

.centering3 {
    margin: 0;
    position: absolute;
    top: 39%;
    right: 1%;
    margin-left: -35%;
    transform: translate(-14%, -35%)
}

.iconred:hover {
    color: red;
}

.icongreen:hover {
    color: green;
}

.iconorange:hover {
    color: orange;
}

.iconblue {
    color: #007bff;
}

.iconwhite {
    color: #ffffff;
}

iframe {
    width: 100% !important;
    height: 100% !important;
    -webkit-transform: scale(1.0);
    transform: scale(1.0);
    -webkit-transform-origin: 0 0;
    transform-origin: 0 0;
}
</style>
