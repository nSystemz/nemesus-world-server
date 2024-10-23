<template>
    <div class="inventory">
        <div style="z-index: 1; overflow-x: hidden; background-color:transparent; scrollbar-width: none;"
            v-if="inventoryshow1||inventoryshow2">
            <div style="height: 100%; background-color: transparent;">
                <div class="row justify-content-center centering">
                    <div class="col-md-12 mt-1 animate__animated animate__bounceInUp">
                        <div class="col-md-12 mt-1">
                            <div class="box box-default">
                                <div class="row">
                                    <div class="  float-left mt-1 mb-2 animate__animated animate__fadeInLeft"
                                        style="scrollbar-width: none; background-color: #343A40; border-top: solid #3c6a99;"
                                        v-if="inventoryshow1">
                                        <h3 class="mt-1 ml-2"
                                            style="font-family: 'Exo', sans-serif; font-size: 0.85vw;">
                                            Deine Taschen
                                            <span class="ml-5" style="font-size: 0.8vw; margin-left: 1.5vw"
                                                v-if="itemSelect!=null">{{itemSelect.description}}<span
                                                    class="ml-1 badge badge-dark" style="font-size: 0.5vw"><span
                                                        v-if="(itemSelect.type != 3 && itemSelect.type != 4 && itemSelect.type != 5) || itemSelect.description == 'Dietrich' || itemSelect.description == 'Zigaretten' || itemSelect.description == '55$-Prepaidkarte' || itemSelect.description == 'Handyvertrag' || itemSelect.description == 'Grippofein-C' || itemSelect.description == 'Antibiotika' || itemSelect.description == 'Ibuprofee-400mg' || itemSelect.description == 'Ibuprofee-800mg' || itemSelect.description == 'Morphin-10mg' || itemSelect.description == 'Bandage' || itemSelect.description == 'Materialien' || itemSelect.description == 'Marihuanasamen' || itemSelect.description == 'Marihuana' || itemSelect.description == 'Papes' || itemSelect.description == 'Joint' || itemSelect.description == 'Kokain' || itemSelect.description == 'Kokablatt' || itemSelect.description == 'Kokainsamen' || itemSelect.description == 'Space-Cookies'">{{itemSelect.amount}}</span><span
                                                        v-if="(itemSelect.props && itemSelect.props.length > 3 && itemSelect.type != 5 && itemSelect.type != 6) || itemSelect.description == 'Feuerzeug'">{{itemSelect.props}}</span><span
                                                        v-if="itemSelect.props && itemSelect.props.length > 2 && itemSelect.type == 5 && !IsNoMelee(itemSelect.description) && itemSelect.description != 'Taser'">{{itemSelect.props.split(',')[0]}}</span></span></span>
                                            <div v-if="itemSelect!=null" class="mr-2 float-right">
                                                <i style="font-size: 0.75vw;"
                                                    v-if="itemSelect.description == 'L-Schein' || itemSelect.description == 'Haustier' || !itemSelect.props.split(',')[1] || itemSelect.props.split(',')[1] == 0"
                                                    class="icon fas fas fa-trash float-right"
                                                    @click="trashItem(itemSelect)"></i>
                                                <i style="font-size: 0.75vw;"
                                                    v-if="itemSelect.type != 5 && itemSelect.type != 6 && (!itemSelect.props.split(',')[1] || itemSelect.props.split(',')[1] == 0 || itemSelect.description == 'Haustier')"
                                                    class="iconresponsive icon fas fas fa-play float-right"
                                                    @click="useItem(itemSelect.itemid)"></i>
                                                <i style="font-size: 0.75vw;" v-if="itemSelect.type == 6"
                                                    class="iconresponsive icon fas fas fa-play float-right"
                                                    @click="useItem2(itemSelect)"></i>
                                                <i style="font-size: 0.75vw;"
                                                    v-if="(!itemSelect.props.split(',')[1] || itemSelect.props.split(',')[1] == 0 || itemSelect.description == 'Haustier') && itemSelect.description != 'Snowball'"
                                                    class="iconresponsive icon fas fa-hand-paper float-right"
                                                    @click="giveItem(itemSelect)"></i>
                                                <i style="font-size: 0.75vw;"
                                                    v-if="itemSelect.type == 5 && itemSelect.description != 'Snowball' && itemSelect.props && itemSelect.description.toLowerCase() != 'feuerlöscher'"
                                                    class="icon3 fa-solid fa-eye"
                                                    @click="showGun(itemSelect.props)"></i>
                                                <i style="font-size: 0.75vw;"
                                                    v-if="itemSelect.type == 5 && itemSelect.props.split(',')[1] == 0"
                                                    class="icon fa-solid fa-gun"
                                                    @click="selectGun(itemSelect.itemid)"></i>
                                                <i style="font-size: 0.75vw;"
                                                    v-if="itemSelect.type == 5 && itemSelect.props.split(',')[1] == 1"
                                                    class="icon2 fa-solid fa-gun"
                                                    @click="selectGun(itemSelect.itemid)"></i>
                                                <i style="font-size: 0.75vw;" v-if="inventoryshow2"
                                                    class="iconresponsive icon fa-solid fa-arrow-right"
                                                    @click="moveItem2(itemSelect, 'left')"></i>
                                            </div>
                                        </h3>
                                        <div class="col-md-12 mb-2">
                                            <div class="progress" style="margin-right: 0.8vw">
                                                <div class="progress-bar progress-bar-striped bg-primary"
                                                    role="progressbar" id="progress-bar" style="width: 0%"
                                                    aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>

                                            </div>
                                        </div>
                                        <div class="col-md-12 mb-3">
                                            <div style="display: flex; justify-content: center; align-items: center; font-family: 'Exo'"
                                                class="mt-1">
                                                <button type="button" @click="selectItems1(-1)"
                                                    style="font-size:0.925vw"
                                                    :class="[(itemselect1 == -1) ? 'active btn btn-primary btn-sm mr-4':'btn btn-primary btn-sm mr-4']"><i
                                                        class="fa-solid fa-border-all"></i></button>
                                                <button type="button" @click="selectItems1(0)" style="font-size:0.925vw"
                                                    :class="[(itemselect1 == 0) ? 'active btn btn-primary btn-sm mr-4':'btn btn-primary btn-sm mr-4']"><i
                                                        class="fa-solid fa-apple-whole"></i></button>
                                                <button type="button" @click="selectItems1(1)" style="font-size:0.925vw"
                                                    :class="[(itemselect1 == 1) ? 'active btn btn-primary btn-sm mr-4':'btn btn-primary btn-sm mr-4']"><i
                                                        class="fa-solid fa-key"></i></button>
                                                <button type="button" @click="selectItems1(2)" style="font-size:0.925vw"
                                                    :class="[(itemselect1 == 2) ? 'active btn btn-primary btn-sm mr-4':'btn btn-primary btn-sm mr-4']"><i
                                                        class="fa-solid fa-gun"></i></button>
                                                <button type="button" @click="selectItems1(3)" style="font-size:0.925vw"
                                                    :class="[(itemselect1 == 3) ? 'active btn btn-primary btn-sm mr-4':'btn btn-primary btn-sm mr-4']"><i
                                                        class="fa-solid fa-microchip"></i></button>
                                            </div>
                                        </div>
                                        <div class="container-fluid3">
                                            <div class="box2 text-center mb-2" v-for="item in inventory1selected"
                                                :key="item.itemid" @click="itemSelected(item)">
                                                <img style="height: 1.5vw;max-width: 4vw" class="mt-2 text-center"
                                                    :src="getImgUrl(item.description)" />
                                                <div class="row text-center">
                                                    <div class="col-md-12">
                                                        <span v-if="itemSelect == item"
                                                            style="font-size: 0.52vw; color:#eee;text-shadow: 1px 0 0 #000, 0 -1px 0 #000, 0 1px 0 #000, -1px 0 0 #000; font-family: 'Exo';border-bottom: 1px solid #3c6a99;">{{cutString(item.description)}}</span>
                                                        <span v-else
                                                            style="font-size: 0.52vw; color:#eee;text-shadow: 1px 0 0 #000, 0 -1px 0 #000, 0 1px 0 #000, -1px 0 0 #000; font-family: 'Exo';">{{cutString(item.description)}}</span>
                                                        <span
                                                            style="font-size: 0.9vw; color:#eee;text-shadow: 1px 0 0 #000, 0 -1px 0 #000, 0 1px 0 #000, -1px 0 0 #000;margin-left:0.1vw;padding-top:1.7vw"><strong
                                                                style="font-size: 0.48vw; font-family: 'Exo'"
                                                                class="badge badge-dark">{{item.amount}}</strong></span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid2 float-left mt-1 mb-2 animate__animated animate__fadeInLeft"
                                style="scrollbar-width: none; background-color: #343A40; border-top: solid #3c6a99; margin-left: 26vw"
                                v-if="inventoryshow2">
                                <h3 class="mt-1 ml-2" style="font-family: 'Exo', sans-serif; font-size: 0.85vw;">
                                    {{textinv2}}
                                    <span class="ml-5" style="font-size: 0.8vw; margin-left: 1.5vw"
                                        v-if="itemSelect2!=null">{{itemSelect2.description}}<span
                                            class="ml-1 badge badge-dark" style="font-size: 0.45vw"><span
                                                v-if="(itemSelect2.type != 3 && itemSelect2.type != 4 && itemSelect2.type != 5) || itemSelect2.description == 'Dietrich' || itemSelect2.description == 'Zigaretten' || itemSelect2.description == '55$-Prepaidkarte' || itemSelect2.description == 'Handyvertrag' || itemSelect2.description == 'Grippofein-C' || itemSelect2.description == 'Antibiotika' || itemSelect2.description == 'Ibuprofee-400mg' || itemSelect2.description == 'Ibuprofee-800mg' || itemSelect2.description == 'Morphin-10mg' || itemSelect2.description == 'Bandage' || itemSelect2.description == 'Materialien' || itemSelect2.description == 'Marihuanasamen' || itemSelect2.description == 'Marihuana' || itemSelect2.description == 'Papes' || itemSelect2.description == 'Joint' || itemSelect2.description == 'Kokain' || itemSelect2.description == 'Kokablatt' || itemSelect2.description == 'Kokainsamen' || itemSelect2.description == 'Space-Cookies'">{{itemSelect2.amount}}</span><span
                                                v-if="(itemSelect2.props && itemSelect2.props.length > 3 && itemSelect2.type != 5 && itemSelect2.type != 6) || itemSelect2.description == 'Feuerzeug'">{{itemSelect2.props}}</span><span
                                                v-if="itemSelect2.props && itemSelect2.props.length > 2 && itemSelect2.type == 5 && !IsNoMelee(itemSelect2.description) && itemSelect2.description != 'Taser'">{{itemSelect2.props.split(',')[0]}}</span></span></span>
                                    <div v-if="itemSelect2!=null" class="mr-2 float-right">
                                        <i style="font-size: 0.75vw;" v-if="inventoryshow2"
                                            class="iconresponsive icon fa-solid fa-arrow-left"
                                            @click="moveItem2(itemSelect2, 'right')"></i>
                                    </div>
                                </h3>
                                <div class="col-md-12 mb-2">
                                    <div class="progress" style="margin-right: 0.8vw">
                                        <div class="progress-bar2 progress-bar-striped bg-primary" role="progressbar"
                                            id="progress-bar2" style="width: 0%" aria-valuenow="25" aria-valuemin="0"
                                            aria-valuemax="100"></div>

                                    </div>
                                </div>
                                <div class="col-md-12 mb-3">
                                    <div style="display: flex; justify-content: center; align-items: center; font-family: 'Exo'"
                                        class="mt-1">
                                        <button type="button" @click="selectItems2(-1)" style="font-size:0.925vw"
                                            :class="[(itemselect2 == -1) ? 'active btn btn-primary btn-sm mr-4':'btn btn-primary btn-sm mr-4']"><i
                                                class="fa-solid fa-border-all"></i></button>
                                        <button type="button" @click="selectItems2(0)" style="font-size:0.925vw"
                                            :class="[(itemselect2 == 0) ? 'active btn btn-primary btn-sm mr-4':'btn btn-primary btn-sm mr-4']"><i
                                                class="fa-solid fa-apple-whole"></i></button>
                                        <button type="button" @click="selectItems2(1)" style="font-size:0.925vw"
                                            :class="[(itemselect2 == 1) ? 'active btn btn-primary btn-sm mr-4':'btn btn-primary btn-sm mr-4']"><i
                                                class="fa-solid fa-key"></i></button>
                                        <button type="button" @click="selectItems2(2)" style="font-size:0.925vw"
                                            :class="[(itemselect2 == 2) ? 'active btn btn-primary btn-sm mr-4':'btn btn-primary btn-sm mr-4']"><i
                                                class="fa-solid fa-gun"></i></button>
                                        <button type="button" @click="selectItems2(3)" style="font-size:0.925vw"
                                            :class="[(itemselect2 == 3) ? 'active btn btn-primary btn-sm mr-4':'btn btn-primary btn-sm mr-4']"><i
                                                class="fa-solid fa-microchip"></i></button>

                                    </div>
                                </div>
                                <div class="box2 text-center mb-2" v-for="item in inventory2selected" :key="item.itemid"
                                    :style="[(itemSelect2 == item) ? 'border: 2px solid #3c6a99;':'']"
                                    @click="itemSelected2(item)">
                                    <img style="height: 1.5vw;max-width: 4vw" class="mt-2 text-center"
                                        :src="getImgUrl(item.description)" />
                                    <div class="row text-center">
                                        <div class="col-md-12">
                                            <span v-if="itemSelect2 == item"
                                                style="font-size: 0.47vw; color:#eee;text-shadow: 1px 0 0 #000, 0 -1px 0 #000, 0 1px 0 #000, -1px 0 0 #000; font-family: 'Exo';border-bottom: 1px solid #3c6a99;">{{cutString(item.description)}}</span>
                                            <span v-else
                                                style="font-size: 0.47vw; color:#eee;text-shadow: 1px 0 0 #000, 0 -1px 0 #000, 0 1px 0 #000, -1px 0 0 #000; font-family: 'Exo';">{{cutString(item.description)}}</span>
                                            <span
                                                style="font-size: 0.9vw; color:#eee;text-shadow: 1px 0 0 #000, 0 -1px 0 #000, 0 1px 0 #000, -1px 0 0 #000;margin-left:0.1vw;padding-top:1.7vw"><strong
                                                    style="font-size: 0.48vw; font-family: 'Exo'"
                                                    class="badge badge-dark">{{item.amount}}</strong></span>
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
    name: 'inventory',
    data: function () {
        return {
            inventoryshow1: false,
            inventoryshow2: false,
            itemSelect: null,
            itemSelect2: null,
            itemselect1: -1,
            itemselect2: -1,
            inventory1: null,
            inventory1selected: [],
            maxweight1: 25000,
            inventory2: [],
            inventory2selected: [],
            maxweight2: 30000,
            valeur: 0,
            valeur2: 0,
            textinv2: '',
            clicked: (Date.now() / 1000)
        }
    },
    components: {},
    mounted() {},
    methods: {
        cutString(text) {
            if(text.length > 10)
            {
                return text.substr(0, 10) + '...';            }
            else
            {
                return text;
            }
        },
        copyToClipboard(text) {
            let dummy = document.createElement("textarea");
            document.body.appendChild(dummy);
            dummy.value = text;
            dummy.select();
            document.execCommand("copy");
            document.body.removeChild(dummy);
        },
        getImgUrl(pic) {
            try {
                return require('../assets/images/inventory/' + pic + '.png')
            } catch (e) {
                return require('../assets/images/inventory/fragezeichen.png')
            }
        },
        coverInventory() {
            let self = this;
            //Inventory 1
            self.inventory1selected = [];
            if (self.itemselect1 <= -1) {
                self.inventory1.forEach(function (element) {
                    self.inventory1selected.push(element);
                });
            } else if (self.itemselect1 == 0) {
                self.inventory1.forEach(function (element) {
                    if (element.type == 1 || element.type == 2) {
                        self.inventory1selected.push(element);
                    }
                });
            } else if (self.itemselect1 == 1) {
                self.inventory1.forEach(function (element) {
                    if (element.description.toLowerCase().includes("schlüssel")) {
                        self.inventory1selected.push(element);
                    }
                });
            } else if (self.itemselect1 == 2) {
                self.inventory1.forEach(function (element) {
                    if (element.type == 5 || element.type == 6) {
                        self.inventory1selected.push(element);
                    }
                });
            } else if (self.itemselect1 >= 2) {
                self.inventory1.forEach(function (element) {
                    if ((element.type == 3 || element.type == 4) && !element.description.toLowerCase().includes("schlüssel")) {
                        self.inventory1selected.push(element);
                    }
                });
            }
            //Inventory 2
            self.inventory2selected = [];
            if (self.itemselect2 <= -1) {
                self.inventory2.forEach(function (element) {
                    self.inventory2selected.push(element);
                });
            } else if (self.itemselect2 == 0) {
                self.inventory2.forEach(function (element) {
                    if (element.type == 1 || element.type == 2) {
                        self.inventory2selected.push(element);
                    }
                });
            } else if (self.itemselect2 == 1) {
                self.inventory2.forEach(function (element) {
                    if (element.description.toLowerCase().includes("schlüssel")) {
                        self.inventory2selected.push(element);
                    }
                });
            } else if (self.itemselect2 == 2) {
                self.inventory2.forEach(function (element) {
                    if (element.type == 5 || element.type == 6) {
                        self.inventory2selected.push(element);
                    }
                });
            } else if (self.itemselect2 >= 2) {
                self.inventory2.forEach(function (element) {
                    if ((element.type == 3 || element.type == 4) && !element.description.toLowerCase().includes("schlüssel")) {
                        self.inventory2selected.push(element);
                    }
                });
            }
            self.$forceUpdate();
        },
        itemSelected(item) {
            this.itemSelect = item;
        },
        itemSelected2(item) {
            this.itemSelect2 = item;
        },
        selectItems1(select) {
            this.itemselect1 = select;
            this.coverInventory();
        },
        selectItems2(select) {
            this.itemselect2 = select;
            this.coverInventory();
        },
        countweightinventory1() {
            let count = 0.0;
            if (this.inventory1) {
                let value = 0;
                this.inventory1.forEach(function (element) {
                    value = element.props.split(",")[0];
                    if (value > 5000) {
                        value = 0;
                    }
                    if (element.type != 5) {
                        count += (element.weight * element.amount);
                    } else {
                        if (element.description == 'Flaregun') {
                            count += (element.weight + (value * 85));
                        } else {
                            count += (element.weight + (value * 3));
                        }
                    }
                });
            }
            return count;
        },
        countweightinventory2() {
            let count = 0.0;
            if (this.inventory2) {
                this.inventory2.forEach(function (element) {
                    if (element.type != 5) {
                        count += (element.weight * element.amount);
                    } else {
                        if (element.description == 'Flaregun') {
                            count += (element.weight + (element.amount * 85));
                        } else {
                            count += (element.weight + (element.amount * 3));
                        }
                    }
                });
            }
            return count;
        },
        moveItem2(item, from) {
            if ((Date.now() / 1000) > this.clicked) {
                this.itemSelect = null;
                this.itemSelect2 = null;
                this.moveItem(item, from);
            }
        },
        async moveItem(item, from) {
            if (this.inventoryshow2 == true) {
                if (from == 'left') {
                    if (item.amount > 1) {
                        const {
                            value: dropvalue
                        } = await Swal.fire({
                            title: 'Bitte eine Menge angeben!',
                            input: 'text',
                            inputPlaceholder: '1',
                            inputAttributes: {
                                value: 1,
                                maxlength: 4,
                                autocapitalize: 'off',
                                autocorrect: 'off'
                            }
                        })
                        this.itemSelect = null;
                        this.itemSelect2 = null;
                        this.clicked = (Date.now() / 1000) + (1);
                        if (!dropvalue) {
                            // eslint-disable-next-line no-undef
                            mp.trigger("Client:UseInventory", "move", item.itemid, 0, "left");
                        } else {
                            // eslint-disable-next-line no-undef
                            mp.trigger("Client:UseInventory", "move", item.itemid, dropvalue, "left");
                        }
                    } else {
                        // eslint-disable-next-line no-undef
                        mp.trigger("Client:UseInventory", "move", item.itemid, 1, "left");
                    }
                } else {
                    if (item.amount > 1) {
                        const {
                            value: dropvalue
                        } = await Swal.fire({
                            title: 'Bitte eine Menge angeben!',
                            input: 'text',
                            inputPlaceholder: '1',
                            inputAttributes: {
                                value: 1,
                                maxlength: 4,
                                autocapitalize: 'off',
                                autocorrect: 'off'
                            }
                        })
                        this.itemSelect = null;
                        this.itemSelect2 = null;
                        if (!dropvalue) {
                            // eslint-disable-next-line no-undef
                            mp.trigger("Client:UseInventory", "move", item.itemid, 0, "right");
                        } else {
                            // eslint-disable-next-line no-undef
                            mp.trigger("Client:UseInventory", "move", item.itemid, dropvalue, "right");
                        }
                    } else {
                        // eslint-disable-next-line no-undef
                        mp.trigger("Client:UseInventory", "move", item.itemid, 1, "right");
                    }
                }
            }
        },
        showInventory(json, maxweight, toogle, json2, maxweight2, textinv2) {
            this.itemSelect = null;
            this.itemSelect2 = null;
            this.clicked = 0;
            this.inventoryshow1 = !this.inventoryshow1;
            if (this.inventoryshow2 == true) {
                this.inventoryshow2 = false;
            }
            if (json2 != null && json2 != 'null') {
                this.inventoryshow2 = !this.inventoryshow2;
            }
            if (!this.inventoryshow1) {
                Swal.close();
            }
            if (toogle) {
                this.inventory1 = JSON.parse(json);
                this.maxweight1 = maxweight;
                if (json2 != null && json2 != 'null') {
                    this.inventory2 = JSON.parse(json2);
                    this.maxweight2 = maxweight2;
                    this.textinv2 = textinv2;
                } else {
                    this.inventory2 = [];
                    this.maxweight2 = 30000;
                    this.textinv2 = '';
                }
                let self = this;
                setTimeout(function () {
                    self.updateProgressbar1();
                    if (json2 != null && json2 != 'null') {
                        self.updateProgressbar2();
                    }
                }, 150);
            }
            this.coverInventory();
        },
        updateInventory(json, json2) {
            if (json) {
                this.inventory1 = JSON.parse(json);
                this.updateProgressbar1();
            } else {
                this.inventory1 = [];
                this.maxweight1 = 25000;
            }
            if (json2) {
                this.inventory2 = JSON.parse(json2);
                this.updateProgressbar2();
            } else {
                this.inventory2 = [];
                this.maxweight2 = 30000;
                this.textinv2 = '';
            }
            this.coverInventory();
        },
        useItem(itemid) {
            if ((Date.now() / 1000) > this.clicked) {
                this.clicked = (Date.now() / 1000) + (1);
                // eslint-disable-next-line no-undef
                mp.trigger("Client:UseInventory", "use", itemid, 1, "nothing");
            }
        },
        async useItem2(item) {
            if ((Date.now() / 1000) > this.clicked) {
                this.clicked = (Date.now() / 1000) + (1);
                if (item.amount > 1) {
                    const {
                        value: dropvalue
                    } = await Swal.fire({
                        title: 'Bitte eine Menge angeben!',
                        input: 'text',
                        inputPlaceholder: '1',
                        inputAttributes: {
                            value: 1,
                            maxlength: 4,
                            autocapitalize: 'off',
                            autocorrect: 'off'
                        }
                    })
                    if (!dropvalue) {
                        // eslint-disable-next-line no-undef
                        mp.trigger("Client:UseInventory", "use", item.itemid, 0, "nothing");
                    } else {
                        // eslint-disable-next-line no-undef
                        mp.trigger("Client:UseInventory", "use", item.itemid, dropvalue, "nothing");
                    }
                } else {
                    // eslint-disable-next-line no-undef
                    mp.trigger("Client:UseInventory", "use", item.itemid, 1, "nothing");
                }
            }
        },
        selectGun(itemid) {
            if ((Date.now() / 1000) > this.clicked) {
                this.clicked = (Date.now() / 1000) + (1);
                // eslint-disable-next-line no-undef
                mp.trigger("Client:SelectGun", itemid);
            }
        },
        showGun(props) {
            if ((Date.now() / 1000) > this.clicked) {
                this.clicked = (Date.now() / 1000) + (1);
                var ident = props.split(',')[3];
                this.copyToClipboard(ident);
                // eslint-disable-next-line no-undef
                mp.trigger("Client:SendNotificationWithoutButton", 'Waffenidentifikationsnummer: ' + ident + ' (( In die Zwischenablage kopiert! ))', 'info', 'top-left', '4250');
            }
        },
        async trashItem(item) {
            if ((Date.now() / 1000) > this.clicked) {
                this.clicked = (Date.now() / 1000) + (1);
                if (item.amount > 1) {
                    const {
                        value: dropvalue
                    } = await Swal.fire({
                        title: 'Bitte eine Menge angeben!',
                        input: 'text',
                        inputPlaceholder: '1',
                        inputAttributes: {
                            value: 1,
                            maxlength: 4,
                            autocapitalize: 'off',
                            autocorrect: 'off'
                        }
                    })
                    if (!dropvalue) {
                        // eslint-disable-next-line no-undef
                        mp.trigger("Client:UseInventory", "trash", item.itemid, 0, "nothing");
                    } else {
                        // eslint-disable-next-line no-undef
                        mp.trigger("Client:UseInventory", "trash", item.itemid, dropvalue, "nothing");
                    }
                } else {
                    // eslint-disable-next-line no-undef
                    mp.trigger("Client:UseInventory", "trash", item.itemid, 1, "nothing");
                }
            }
        },
        async giveItem(item) {
            if ((Date.now() / 1000) > this.clicked) {
                this.clicked = (Date.now() / 1000) + (1);
                if (item.amount > 1) {
                    const {
                        value: dropvalue
                    } = await Swal.fire({
                        title: 'Bitte eine Menge angeben!',
                        input: 'text',
                        inputPlaceholder: '1',
                        inputAttributes: {
                            value: 1,
                            maxlength: 4,
                            autocapitalize: 'off',
                            autocorrect: 'off'
                        }
                    })
                    if (!dropvalue) {
                        // eslint-disable-next-line no-undef
                        mp.trigger("Client:UseInventory", "give", item.itemid, 0, "nothing");
                    } else {
                        // eslint-disable-next-line no-undef
                        mp.trigger("Client:UseInventory", "give", item.itemid, dropvalue, "nothing");
                    }
                } else {
                    // eslint-disable-next-line no-undef
                    mp.trigger("Client:UseInventory", "give", item.itemid, 1, "nothing");
                }
            }
        },
        IsNoMelee: function (itemname) {
            switch (itemname.toLowerCase()) {
                case "dolch": {
                    return 1;
                }
                case "baseballschläger": {
                    return 1;
                }
                case "brechstange": {
                    return 1;
                }
                case "taschenlampe": {
                    return 1;
                }
                case "golfschläger": {
                    return 1;
                }
                case "axt": {
                    return 1;
                }
                case "schlagring": {
                    return 1;
                }
                case "messer": {
                    return 1;
                }
                case "machete": {
                    return 1;
                }
                case "klappmesser": {
                    return 1;
                }
                case "schlagstock": {
                    return 1;
                }
                case "poolcue": {
                    return 1;
                }
                case "feuerlöscher": {
                    return 1;
                }
            }
            return 0;
        },
        updateProgressbar1: function () {
            // eslint-disable-next-line no-undef
            let invweight = this.countweightinventory1();
            if (invweight > 0) {
                this.valeur = ((100 / this.maxweight1) * invweight);
            } else {
                this.valeur = 0;
            }
            // eslint-disable-next-line no-undef
            $('.progress-bar').css('width', this.valeur + '%').attr('aria-valuenow', this.valeur);
        },
        updateProgressbar2: function () {
            // eslint-disable-next-line no-undef
            let invweight2 = this.countweightinventory2();
            if (invweight2 > 0) {
                this.valeur2 = ((100 / this.maxweight2) * invweight2);
            } else {
                this.valeur2 = 0;
            }
            // eslint-disable-next-line no-undef
            $('.progress-bar2').css('width', this.valeur2 + '%').attr('aria-valuenow', this.valeur2);
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

.responsivcard {
    max-height: 17.5vw;
    width: 26.0vw;
    overflow-x: auto;
    font-size: 1vw;
}

@media (max-height: 1024px) {
    .responsivcard {
        max-height: 17.5vw;
        width: 28.0vw;
        overflow-x: auto;
        font-size: 1vw;
    }
}

@media (max-height: 768px) {
    .responsivcard {
        max-height: 17.5vw;
        width: 31.0vw;
        overflow-x: auto;
        font-size: 1vw;
    }
}

@media (max-height: 600px) {
    .responsivcard {
        max-height: 17.5vw;
        width: 34.5vw;
        overflow-x: auto;
        font-size: 1vw;
    }
}

.centering {
    margin: 0;
    position: absolute;
    top: 95%;
    left: 4%;
    margin-right: -50%;
    transform: translate(-4%, -95%)
}

.card2:hover {
    border: 0.5px solid #fff;
    box-shadow: 0 10px 20px rgba(0, 0, 0, .12), 0 4px 8px rgba(0, 0, 0, .06);
    cursor: pointer;
}

.icon {
    color: red;
    font-size: 0.9vw;
    transform: translateY(0.355vw);
    padding-right: 0.45vw;
    float: right;
}

.icon:hover {
    color: green;
}

.icon2 {
    color: green;
    font-size: 0.9vw;
    transform: translateY(0.355vw);
    padding-right: 0.45vw;
    float: right;
}

.icon2:hover {
    color: red;
}

.icon3 {
    color: white;
    font-size: 0.9vw;
    transform: translateY(0.355vw);
    padding-right: 0.45vw;
    float: right;
}

.icon3:hover {
    color: #3F6791;
}

.iconresponsive {
    font-size: 0.9vw;
    transform: translateY(0.355vw);
    padding-right: 0.45vw;
}

.propsresponsive {
    transform: translateY(-0.0345vw);
    font-size: 0.8vw;
}

@media (max-height: 600px) {
    .propsresponsive {
        transform: translateY(-0.1345vw);
        font-size: 0.8vw;
    }

    .inventorytext2 {
        transform: translateY(-0.1345vw);
    }
}

.grenade {
    margin-left: 0.8vh;
    margin-right: 0.55vh;
}

@media (max-height: 600px) {
    .grenade {
        margin-left: 1.5vh;
        margin-right: 0.425vh;
    }
}

@media (min-height: 1600px) {
    .grenade {
        margin-left: 0.8vh;
        margin-right: 0.355vh;
    }
}
.container-fluid2 {
    scrollbar-width: none !important;
    position: absolute;
    margin: 0;
    padding: 0;
    bottom: 0.25vw;
    padding-left: 0.65vw;
    width: 25vw;
    max-height: 33.6vw;
    background-color: transparent;
    overflow: hidden;
}
.container-fluid2 {
    scrollbar-width: none !important;
    position: absolute;
    margin: 0;
    padding: 0;
    bottom: 0.25vw;
    padding-left: 0.65vw;
    width: 25vw;
    max-height: 33.6vw;
    background-color: transparent;
    overflow: hidden;
}
.container-fluid3 {
    scrollbar-width: none !important;
    margin: 0;
    padding: 0;
    padding-left: 0.65vw;
    max-height: 30.25vw;
    width: 25vw;
    overflow: auto;
}
.box2 { 
	float: left; 
	width: 5.5vw; 
	height: 4vw; 
	margin-right: 0.55vw; 
	padding: 0.5vw; 
	margin-top: 0.5vw; 
	background: #3F474E; 
	box-sizing: border-box; 
}
.box2:last-child { margin-right: 0; }
.box2:hover { border: 0.1px solid #3c6a99; }
</style>
