<template>
<div class="furniture" style="overflow-y: auto;background-color:transparent; height:100%; width:100%">
    <div style="height: 100%; background-color: transparent;" v-if="showmoebeldialog4">
        <div class="row justify-content-center centering4">
            <div class="col-md-12 mt-1">
                <div class="col-md-12 mt-1">
                    <div class="box box-default">
                        <div class="row">
                            <div class="card card-primary card-outline">
                                <div class="card-header" style="font-family: 'Exo', sans-serif; font-size: 1.2vw">
                                    Möbelverwaltung <span style="font-size: 0.7vw">(Slots: {{count}}/{{limit}})</span>
                                </div>
                                <div class="card-body" style="max-height:70vh; width: 25vw; overflow-x: auto">
                                    <div class="row">
                                        <div class="col-md-12" v-if="moebelSet != 'close'">
                                            <input class="form-control mb-2" autocomplete="off" v-bind:value="searchelement" v-on:input="searchelement = $event.target.value" type="text" id="search" maxlength="64" placeholder="Suche..." style="font-size: 1vw">
                                            <div class="card2 card card-primary card-outline text-center" v-for="selectedFurniture in filteredList" :key="selectedFurniture.id">
                                                <div class="settext ml-2" style="cursor:pointer" v-on:click="selectMoebel(selectedFurniture.id)">
                                                    {{selectedFurniture.name}} - [{{selectedFurniture.id}}] <span v-if="selectedFurniture.position == '0.0|0.0|0.0|0.0|0.0|0.0|0'" class="badge badge-warning mb-1 ml-1">Abgebaut</span><span v-else class="badge badge-success mb-1 ml-1">Aufgebaut</span><span v-if="moebelSet == 'delete'" class="badge badge-primary mb-1 ml-1">{{Math.floor(selectedFurniture.price/2)}}$</span>
                                                </div>
                                            </div>
                                            <div class="card2 card card-primary card-outline text-center">
                                                <div class="settext ml-2" style="cursor:pointer" v-on:click="selectMoebel(-2)">
                                                    Zurück
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12" v-else>
                                            <input class="form-control mb-2" autocomplete="off" v-bind:value="searchelement" v-on:input="searchelement = $event.target.value" type="text" id="search" maxlength="64" placeholder="Suche..." style="font-size: 1vw">
                                            <div class="card2 card card-primary card-outline text-center" v-for="selectedFurniture in filteredList" :key="selectedFurniture.id">
                                                <div class="settext ml-2" style="cursor:pointer" v-on:click="selectMoebel(selectedFurniture.id)">
                                                    {{selectedFurniture.name}} - [{{selectedFurniture.id}}] <span v-if="selectedFurniture.position == '0.0|0.0|0.0|0.0|0.0|0.0|0'" class="badge badge-warning mb-1 ml-1">Abgebaut</span><span v-else class="badge badge-success mb-1 ml-1">Aufgebaut</span>
                                                </div>
                                            </div>
                                            <div class="card2 card card-primary card-outline text-center">
                                                <div class="settext ml-2" style="cursor:pointer" v-on:click="selectMoebel(-2)">
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
    <div style="height: 100%; background-color: transparent;" v-if="showmoebeldialog3">
        <div class="row justify-content-center centering4">
            <div class="col-md-12 mt-1">
                <div class="col-md-12 mt-1">
                    <div class="box box-default">
                        <div class="row">
                            <div class="card card-primary card-outline">
                                <div class="card-header" style="font-family: 'Exo', sans-serif; font-size: 1.2vw">
                                    Möbelverwaltung <span style="font-size: 0.7vw">(Slots: {{count}}/{{limit}})</span>
                                </div>
                                <div class="card-body" style="max-height:70vh; width: 25vw; overflow-x: auto">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <input class="form-control mb-2" autocomplete="off" v-bind:value="searchelement" v-on:input="searchelement = $event.target.value" type="text" id="search" maxlength="64" placeholder="Suche..." style="font-size: 1vw">
                                            <div class="card2 card card-primary card-outline text-center" v-for="selectedFurniture in filteredList" :key="selectedFurniture.id">
                                                <div class="settext ml-2" style="cursor:pointer" v-on:click="selectMoebel(selectedFurniture.id)">
                                                    {{selectedFurniture.name}} <span class="badge badge-dark mb-1 ml-1">{{selectedFurniture.price}}$</span>
                                                </div>
                                            </div>
                                            <div class="card2 card card-primary card-outline text-center">
                                                <div class="settext ml-2" style="cursor:pointer" v-on:click="selectMoebel(-1)">
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
    <div style="height: 100%; background-color: transparent;" v-if="showmoebeldialog2">
        <div class="row justify-content-center centering4">
            <div class="col-md-12 mt-1">
                <div class="col-md-12 mt-1">
                    <div class="box box-default">
                        <div class="row">
                            <div class="card card-primary card-outline">
                                <div class="card-header" style="font-family: 'Exo', sans-serif; font-size: 1.2vw">
                                    Möbelverwaltung <span style="font-size: 0.7vw">(Slots: {{count}}/{{limit}})</span>
                                </div>
                                <div class="card-body" style="max-height:70vh; width: 25vw; overflow-x: auto">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="card2 card card-primary card-outline text-center">
                                                <div class="settext ml-2" style="cursor:pointer" v-on:click="selectCategorie(-1)">
                                                    Alle Möbel
                                                </div>
                                            </div>
                                            <div class="card2 card card-primary card-outline text-center" v-for="moebelcategorie in moebelcategories" :key="moebelcategorie.id">
                                                <div class="settext ml-2" style="cursor:pointer" v-on:click="selectCategorie(moebelcategorie.id)">
                                                    {{moebelcategorie.name}}
                                                </div>
                                            </div>
                                            <div class="card2 card card-primary card-outline text-center">
                                                <div class="settext ml-2" style="cursor:pointer" v-on:click="selectCategorie(-2)">
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
    <div style="height: 100%; background-color: transparent;" v-if="showmoebeldialog">
        <div class="row justify-content-center centering4">
            <div class="col-md-12 mt-1 animate__animated animate__bounceInUp">
                <div class="col-md-12 mt-1">
                    <div class="box box-default">
                        <div class="row">
                            <div class="card card-primary card-outline">
                                <div class="card-header" style="font-family: 'Exo', sans-serif; font-size: 1.2vw">
                                    Möbelverwaltung <span style="font-size: 0.7vw">(Slots: {{count}}/{{limit}})</span>
                                </div>
                                <div class="card-body" style="max-height:70vh; width: 25vw; overflow-x: auto">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="card2 card card-primary card-outline text-center">
                                                <div class="settext ml-2" style="cursor:pointer" v-on:click="newMoebel()">
                                                    Neue Möbel aufstellen
                                                </div>
                                            </div>
                                            <div class="card2 card card-primary card-outline text-center">
                                                <div class="settext ml-2" style="cursor:pointer" v-on:click="moebelModus()">
                                                    Möbelmodus ak/deaktivieren
                                                </div>
                                            </div>
                                            <div class="card2 card card-primary card-outline text-center">
                                                <div class="settext ml-2" style="cursor:pointer" v-on:click="moveMoebel()">
                                                    Möbel aufbauen/bewegen
                                                </div>
                                            </div>
                                            <div class="card2 card card-primary card-outline text-center">
                                                <div class="settext ml-2" style="cursor:pointer" v-on:click="closeMoebel()">
                                                    Möbel abbauen
                                                </div>
                                            </div>
                                            <div class="card2 card card-primary card-outline text-center">
                                                <div class="settext ml-2" style="cursor:pointer" v-on:click="deleteMoebel()">
                                                    Möbel verkaufen
                                                </div>
                                            </div>
                                            <div class="card2 card card-primary card-outline text-center">
                                                <div class="settext ml-2" style="cursor:pointer" v-on:click="hideMenu()">
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
export default {
    name: 'Hud',
    data: function () {
        return {
            showmoebeldialog: false,
            showmoebeldialog2: false,
            showmoebeldialog3: false,
            showmoebeldialog4: false,
            searchelement: '',
            categorie: 0,
            moebelcategories: [],
            selectedFurnitures: [],
            furniturelist: [],
            moebelSet: '',
            count: 0,
            limit: 0,
        }
    },
    computed: {
        filteredList() {
            return this.selectedFurnitures.filter(furniture => {
                return furniture.name.toLowerCase().includes(this.searchelement.toLowerCase())
            })
        },
    },
    mounted() {},
    methods: {
        selectCategorie: function (id) {
            if (id != -2) {
                this.categorie = id;
                this.showmoebeldialog2 = false;
                this.selectedFurnitures = [];
                this.searchelement = '';
                if (id != -1) {
                    this.allfurniture.forEach(
                        element => {
                            if (element.categorie == id) {
                                this.selectedFurnitures.push(element);
                            }
                        }
                    );
                } else {
                    this.selectedFurnitures = this.allfurniture;
                }
                this.selectedFurnitures.sort(function (a, b) {
                    var nameA = a.name.toUpperCase();
                    var nameB = b.name.toUpperCase();
                    if (nameA < nameB) {
                        return -1;
                    }
                    if (nameA > nameB) {
                        return 1;
                    }
                    return 0;
                });
                this.showmoebeldialog3 = true;
            } else {
                this.showmoebeldialog = true;
                this.showmoebeldialog2 = false;
                this.showmoebeldialog3 = false;
                this.showmoebeldialog4 = false;
            }
        },
        selectMoebel: function (id) {
            this.searchelement = '';
            if (id == -1) {
                this.showmoebeldialog4 = false;
                this.showmoebeldialog3 = false;
                this.showmoebeldialog2 = true;
            } else if (id == -2) {
                this.showmoebeldialog4 = false;
                this.showmoebeldialog3 = false;
                this.showmoebeldialog2 = false;
                this.showmoebeldialog = true;
            } else {
                this.showmoebeldialog = false;
                this.showmoebeldialog2 = false;
                this.showmoebeldialog3 = false;
                this.showmoebeldialog4 = false;
                if (this.moebelSet == 'new') {
                    // eslint-disable-next-line no-undef
                    mp.trigger("Client:FurnitureSettings", "buy", id);
                }
                if (this.moebelSet == 'move') {
                    // eslint-disable-next-line no-undef
                    mp.trigger("Client:FurnitureSettings", "move", id);
                }
                if (this.moebelSet == 'close') {
                    // eslint-disable-next-line no-undef
                    mp.trigger("Client:FurnitureSettings", "close", id);
                }
                if (this.moebelSet == 'delete') {
                    // eslint-disable-next-line no-undef
                    mp.trigger("Client:FurnitureSettings", "delete", id);
                }
                this.moebelSet = '';
            }
        },
        newMoebel: function () {
            this.moebelSet = 'new';
            this.showmoebeldialog = false;
            this.moebelcategories = this.moebelcategories.sort(function (a, b) {
                var nameA = a.name.toUpperCase();
                var nameB = b.name.toUpperCase();
                if (nameA < nameB) {
                    return -1;
                }
                if (nameA > nameB) {
                    return 1;
                }
                return 0;
            });
            this.showmoebeldialog2 = true;
        },
        moebelModus: function () {
            // eslint-disable-next-line no-undef
            mp.trigger("Client:FurnitureSettings", "moebelmodus");
        },
        moveMoebel: function () {
            this.moebelSet = 'move';
            this.showmoebeldialog = false;
            this.selectedFurnitures = this.furniturelist.sort(function (a, b) {
                var nameA = a.name.toUpperCase();
                var nameB = b.name.toUpperCase();
                if (nameA < nameB) {
                    return -1;
                }
                if (nameA > nameB) {
                    return 1;
                }
                return 0;
            });
            this.showmoebeldialog4 = true;
        },
        closeMoebel: function () {
            this.moebelSet = 'close';
            this.showmoebeldialog = false;
            this.showmoebeldialog4 = true;
            this.selectedFurnitures = this.furniturelist.sort(function (a, b) {
                var nameA = a.name.toUpperCase();
                var nameB = b.name.toUpperCase();
                if (nameA < nameB) {
                    return -1;
                }
                if (nameA > nameB) {
                    return 1;
                }
                return 0;
            });
            this.selectedFurnitures = this.selectedFurnitures.filter(function (element) {
                return element.position != '0.0|0.0|0.0|0.0|0.0|0.0|0';
            });
        },
        hideMenu: function () {
            this.showmoebeldialog = false;
            this.showmoebeldialog2 = false;
            this.showmoebeldialog3 = false;
            this.showmoebeldialog4 = false;
            this.moebelSet = 'hide';
            // eslint-disable-next-line no-undef
            mp.trigger("Client:FurnitureSettings", "hide", 1);
        },
        deleteMoebel: function () {
            this.moebelSet = 'delete';
            this.showmoebeldialog = false;
            this.showmoebeldialog2 = false;
            this.showmoebeldialog3 = false;
            this.showmoebeldialog4 = true;
            this.selectedFurnitures = this.furniturelist.sort(function (a, b) {
                var nameA = a.name.toUpperCase();
                var nameB = b.name.toUpperCase();
                if (nameA < nameB) {
                    return -1;
                }
                if (nameA > nameB) {
                    return 1;
                }
                return 0;
            });
        },
        showMoebel: function (json1, json2, json3, count, limit) {
            this.searchelement = '';
            this.showmoebeldialog4 = false;
            this.showmoebeldialog3 = false;
            this.showmoebeldialog2 = false;
            if (json1) {
                this.moebelcategories = JSON.parse(json1);
                this.allfurniture = JSON.parse(json2);
            }
            if (json3) {
                this.furniturelist = JSON.parse(json3);
            }
            if (count) {
                this.count = count;
            }
            if (limit) {
                this.limit = limit;
            }
            this.showmoebeldialog = !this.showmoebeldialog;
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

.centering4 {
    margin: 0;
    position: absolute;
    top: 50%;
    right: 50%;
    margin-left: -50%;
    transform: translate(50%, -50%)
}

.settext {
    font-size: 1.0vw;
}
</style>
