<template>
<div class="inventory">
    <div style="z-index: 1; overflow-x: hidden; overflow-x: hidden; background-color:transparent; scrollbar-width: none;" v-if="inventoryshow1||inventoryshow2">
        <div style="height: 100%; background-color: transparent;">
            <div class="row justify-content-center centering">
                <div class="col-md-12 mt-1 animate__animated animate__bounceInUp">
                    <div class="col-md-12 mt-1">
                        <div class="box box-default">
                            <div class="row">
                                <div class="card card-primary card-outline" v-if="inventoryshow1">
                                    <div class="card-header" style="font-family: 'Exo', sans-serif; font-size: 1.2vw">
                                        Deine Taschen
                                        <div class="progress">
                                            <div class="progress-bar progress-bar-striped bg-primary" role="progressbar" id="progress-bar" style="width: 0%" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                                        </div>
                                        <div style="display: flex; justify-content: center; align-items: center; margin-top:0.65vw">
                                            <button type="button" @click="selectItems1(-1)" :class="[(itemselect1 == -1) ? 'active btn btn-primary btn-sm mr-1':'btn btn-primary btn-sm mr-1']" style="display: flex; justify-content: center; align-items: center;font-size:0.725vw">Alles</button>
                                            <button type="button" @click="selectItems1(0)" style="font-size:0.725vw" :class="[(itemselect1 == 0) ? 'active btn btn-primary btn-sm mr-1':'btn btn-primary btn-sm mr-1']">Nahrung</button>
                                            <button type="button" @click="selectItems1(1)" style="font-size:0.725vw" :class="[(itemselect1 == 1) ? 'active btn btn-primary btn-sm mr-1':'btn btn-primary btn-sm mr-1']">Schlüssel</button>
                                            <button type="button" @click="selectItems1(2)" style="font-size:0.725vw" :class="[(itemselect1 == 2) ? 'active btn btn-primary btn-sm mr-1':'btn btn-primary btn-sm mr-1']">Waffen</button>
                                            <button type="button" @click="selectItems1(3)" style="font-size:0.725vw" :class="[(itemselect1 == 3) ? 'active btn btn-primary btn-sm mr-1':'btn btn-primary btn-sm mr-1']">Sonstiges</button>
                                        </div>
                                    </div>
                                    <div class="responsivcard card-body">
                                        <div class="row">
                                            <div class="col-md-12 col-md-offset-6">
                                                <div class="card2 card card-primary" style="max-height:2vw;" v-for="item in inventory1selected" :key="item.itemid" @click="moveItem2(item, 'left')">
                                                    <div class="inventorytext">
                                                        <img v-if="item.description != 'Granate' && item.description != 'BZGas' && item.description != 'Rauchgranate' && item.description != 'Molotowcocktail' && item.description != 'Snowball'" :src="getImgUrl(item.description)" width="5%" class="ml-2" style="transform: translateY(-0.1vw);" data-toggle="offcanvas">
                                                        <img v-else :src="getImgUrl(item.description)" width="3.25%" class="grenade" style="transform: translateY(-0.1vw);" data-toggle="offcanvas">
                                                        <span class="inventorytext2" style="color:white;font-size:1vw;padding-left: 0.4vw">{{item.description}}<span class="propsresponsive badge badge-dark" style="margin-left: 0.35vw">
                                                                <span v-if="(item.type != 3 && item.type != 4 && item.type != 5) || item.description == 'Dietrich' || item.description == 'Zigaretten' || item.description == '55$-Prepaidkarte' || item.description == 'Handyvertrag' || item.description == 'Grippofein-C' || item.description == 'Antibiotika' || item.description == 'Ibuprofee-400mg' || item.description == 'Ibuprofee-800mg' || item.description == 'Morphin-10mg' || item.description == 'Bandage' || item.description == 'Materialien' || item.description == 'Marihuanasamen' || item.description == 'Marihuana' || item.description == 'Papes' || item.description == 'Joint' || item.description == 'Kokain' || item.description == 'Kokablatt' || item.description == 'Kokainsamen' || item.description == 'Space-Cookies'">{{item.amount}}</span><span v-if="(item.props && item.props.length > 3 && item.type != 5 && item.type != 6) || item.description == 'Feuerzeug'">({{item.props}})</span><span v-if="item.props && item.props.length > 2 && item.type == 5 && !IsNoMelee(item.description) && item.description != 'Taser'">{{item.props.split(',')[0]}}</span></span></span>
                                                        <i v-if="item.description == 'L-Schein' || !item.props.split(',')[1] || item.props.split(',')[1] == 0" class="iconresponsive icon fas fas fa-trash float-right" @click="trashItem(item)"></i>
                                                        <i v-if="item.type != 5 && item.type != 6 && (!item.props.split(',')[1] || item.props.split(',')[1] == 0)" class="iconresponsive icon fas fas fa-play float-right" @click="useItem(item.itemid)"></i>
                                                        <i v-if="item.type == 6" class="iconresponsive icon fas fas fa-play float-right" @click="useItem2(item)"></i>
                                                        <i v-if="(!item.props.split(',')[1] || item.props.split(',')[1] == 0) && item.description != 'Snowball'" class="iconresponsive icon fas fa-hand-paper float-right" @click="giveItem(item)"></i>
                                                        <i v-if="item.type == 5 && item.description != 'Snowball' && item.props && item.description.toLowerCase() != 'feuerlöscher'" class="icon3 fa-solid fa-eye" @click="showGun(item.props)"></i>
                                                        <i v-if="item.type == 5 && item.props.split(',')[1] == 0" class="icon fa-solid fa-gun" @click="selectGun(item.itemid)"></i>
                                                        <i v-if="item.type == 5 && item.props.split(',')[1] == 1" class="icon2 fa-solid fa-gun" @click="selectGun(item.itemid)"></i>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="card card-primary card-outline ml-4 animate__animated animate__bounceInUp" v-if="inventoryshow2">
                                    <div class="card-header" style="font-family: 'Exo', sans-serif; font-size: 1.2vw;">
                                        {{textinv2}}
                                        <div class="progress">
                                            <div class="progress-bar2 progress-bar-striped bg-primary" role="progressbar" id="progress-bar2" style="width: 0%" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                                        </div>
                                        <div style="display: flex; justify-content: center; align-items: center;" class="mt-2">
                                            <button type="button" @click="selectItems2(-1)" :class="[(itemselect2 == -1) ? 'active btn btn-primary btn-sm mr-1':'btn btn-primary btn-sm mr-1']" style="display: flex; justify-content: center; align-items: center;font-size:0.725vw">Alles</button>
                                            <button type="button" @click="selectItems2(0)" style="font-size:0.725vw" :class="[(itemselect2 == 0) ? 'active btn btn-primary btn-sm mr-1':'btn btn-primary btn-sm mr-1']">Nahrung</button>
                                            <button type="button" @click="selectItems2(1)" style="font-size:0.725vw" :class="[(itemselect2 == 1) ? 'active btn btn-primary btn-sm mr-1':'btn btn-primary btn-sm mr-1']">Schlüssel</button>
                                            <button type="button" @click="selectItems2(2)" style="font-size:0.725vw" :class="[(itemselect2 == 2) ? 'active btn btn-primary btn-sm mr-1':'btn btn-primary btn-sm mr-1']">Waffen</button>
                                            <button type="button" @click="selectItems2(3)" style="font-size:0.725vw" :class="[(itemselect2 == 3) ? 'active btn btn-primary btn-sm mr-1':'btn btn-primary btn-sm mr-1']">Sonstiges</button>
                                        </div>
                                    </div>
                                    <div class="card-body responsivcard">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="card2 card card-primary" style="max-height:2vw" v-for="item in inventory2selected" :key="item.itemid" @click="moveItem2(item, 'right')">
                                                    <div class="inventorytext">
                                                        <img v-if="item.description != 'Granate' && item.description != 'BZGas' && item.description != 'Rauchgranate' && item.description != 'Molotowcocktail' && item.description != 'Snowball'" :src="getImgUrl(item.description)" width="5%" class="ml-2" style="transform: translateY(-0.1vw);" data-toggle="offcanvas">
                                                        <img v-else :src="getImgUrl(item.description)" width="3.25%" class="grenade" style="transform: translateY(-0.1vw);" data-toggle="offcanvas">
                                                        <span class="inventorytext2" style="color:white;font-size:1vw;padding-left: 0.4vw">{{item.description}}<span class="propsresponsive badge badge-dark" style="margin-left: 0.35vw">
                                                                <span v-if="(item.type != 3 && item.type != 4 && item.type != 5) || item.description == 'Dietrich' || item.description == 'Zigaretten' || item.description == '55$-Prepaidkarte' || item.description == 'Handyvertrag' || item.description == 'Grippofein-C' || item.description == 'Antibiotika' || item.description == 'Ibuprofee-400mg' || item.description == 'Ibuprofee-800mg' || item.description == 'Morphin-10mg' || item.description == 'Bandage' || item.description == 'Materialien' || item.description == 'Marihuanasamen' || item.description == 'Marihuana' || item.description == 'Papes' || item.description == 'Joint' || item.description == 'Kokain' || item.description == 'Kokablatt' || item.description == 'Kokainsamen' || item.description == 'Space-Cookies'">{{item.amount}}</span><span v-if="(item.props && item.props.length > 3 && item.type != 5 && item.type != 6) || item.description == 'Feuerzeug'">({{item.props}})</span><span v-if="item.props && item.props.length > 2 && item.type == 5 && !IsNoMelee(item.description) && item.description != 'Taser'">{{item.props.split(',')[0]}}</span></span></span>
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
import Swal from 'sweetalert2/dist/sweetalert2.js'

export default {
    name: 'inventory',
    data: function () {
        return {
            inventoryshow1: false,
            inventoryshow2: false,
            itemselect1: -1,
            itemselect2: -1,
            inventory1: [],
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
        copyToClipboard(text) {
            var dummy = document.createElement("textarea");
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
            var self = this;
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
        selectItems1(select) {
            this.itemselect1 = select;
            this.coverInventory();
        },
        selectItems2(select) {
            this.itemselect2 = select;
            this.coverInventory();
        },
        countweightinventory1() {
            var count = 0.0;
            if (this.inventory1) {
                var value = 0;
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
            var count = 0.0;
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
                this.moveItem(item, from);
                this.clicked = (Date.now() / 1000) + (3);
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
                var self = this;
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
            var invweight = this.countweightinventory1();
            if (invweight > 0) {
                this.valeur = ((100 / this.maxweight1) * invweight);
            } else {
                this.valeur = 0;
            }
            // eslint-disable-next-line no-undef
            console.log("updateProgressbar");
            // eslint-disable-next-line no-undef
            $('.progress-bar').css('width', this.valeur + '%').attr('aria-valuenow', this.valeur);
        },
        updateProgressbar2: function () {
            // eslint-disable-next-line no-undef
            var invweight = this.countweightinventory2();
            if (invweight > 0) {
                this.valeur2 = ((100 / this.maxweight2) * invweight);
            } else {
                this.valeur2 = 0;
            }
            // eslint-disable-next-line no-undef
            console.log("updateProgressbar2");
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
</style>
