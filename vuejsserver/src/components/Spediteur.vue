<template>
<div class="showspediteur" style="z-index: 1; overflow: hidden; background-color:transparent; scrollbar-width: none;" v-if="speditionbizzshow || speditionvehicleshow || speditionordershow || taxijobsshow || atmshow || secruityshow">
    <div class="centering2" style="height: 100%; background-color: rgba(69, 77, 85, .8);">
        <div class="row justify-content-center centering">
            <div class="col-md-12 animate__animated animate__bounceInUp">
                <div class="col-md-12">
                    <div class="box box-default" v-if="secruityshow && !atmshow">
                        <div class="card card-primary card-outline">
                            <div class="card-header" style="font-family: 'Exo', sans-serif;">Geldabholungsübersicht
                            </div>
                            <div class="card-body" style="min-width: 250px !important">
                                <div class="row">
                                    <div class="col-md-3 col-md-offset-1-and-half" v-for="secruity in secruitySpots" :key="secruity.id">
                                        <div class="card card-primary card-outline mr-2 ml-1" style="min-width: 150px !important;min-height: 125px !important">
                                            <div class="card-body">
                                                <h3 class="profile-username text-center mb-4">
                                                    <strong>{{secruity.name}}</strong>
                                                </h3>
                                                <p class="text-muted text-center">
                                                    Kasse: <strong style="color:green">~{{secruity.money}}$</strong></p>
                                            </div>
                                            <button type="button" class="btn btn-primary mr-3 ml-3 mb-2" @click="acceptSecruity(secruity.id)">Markieren</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="box box-default" v-if="atmshow && !secruityshow">
                        <div class="card card-primary card-outline">
                            <div class="card-header" style="font-family: 'Exo', sans-serif;">Bankautomatenübersicht
                            </div>
                            <div class="card-body" style="min-width: 250px !important">
                                <div class="row">
                                    <div class="col-md-3 col-md-offset-1-and-half" v-for="atmspot in atmSpots" :key="atmspot.id">
                                        <div class="card card-primary card-outline mr-2 ml-1" style="min-width: 150px !important;min-height: 125px !important">
                                            <div class="card-body">
                                                <h3 class="profile-username text-center mb-4">
                                                    <strong>Bankautomat</strong>
                                                </h3>
                                                <p class="text-muted text-center">
                                                    Entfernung: {{atmspot.dist}}m</p>
                                                <p class="text-muted text-center">
                                                    Inhalt: <strong style="color:green">~{{atmspot.value}}$/50000$</strong></p>
                                            </div>
                                            <button type="button" class="btn btn-primary mr-3 ml-3 mb-2" @click="acceptATM(atmspot.id)">Markieren</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="box box-default" v-if="taxijobsshow">
                        <div class="card card-primary card-outline">
                            <div class="card-header" style="font-family: 'Exo', sans-serif;">Auftragsliste
                            </div>
                            <div class="card-body" style="min-width: 250px !important">
                                <div class="row">
                                    <div class="col-md-3 col-md-offset-1-and-half" v-for="taxijob in taxiJobs" :key="taxijob.id">
                                        <div class="card card-primary card-outline mr-2 ml-1" style="min-width: 150px !important;min-height: 125px !important">
                                            <div class="card-body">
                                                <h3 class="profile-username text-center mb-4">
                                                    <strong>Taxiauftrag</strong>
                                                </h3>
                                                <p class="text-muted text-center mt-2">
                                                    Wo: <strong>{{taxijob.from}}</strong></p>
                                                <p class="text-muted text-center mt-2">
                                                    Wohin: <strong>{{taxijob.to}}</strong></p>
                                                <p class="text-muted text-center">
                                                    Gehalt: <strong style="color:green">~{{taxijob.money}}$</strong></p>
                                            </div>
                                            <button type="button" class="btn btn-primary mr-3 ml-3 mb-2" @click="acceptTaxi(taxijob.id)">Annehmen</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="box box-default" v-if="speditionordershow">
                        <div class="card card-primary card-outline">
                            <div class="card-header" style="font-family: 'Exo', sans-serif;">Auftragsliste
                            </div>
                            <div class="card-body" style="min-width: 250px !important">
                                <div class="row">
                                    <div class="col-md-3 col-md-offset-1-and-half" v-for="order in orders" :key="order.id">
                                        <div class="card card-primary card-outline mr-2 ml-1" style="min-width: 150px !important;min-height: 125px !important">
                                            <div class="card-body">
                                                <h3 class="profile-username text-center mb-4">
                                                    <strong>{{order.what}}</strong>
                                                </h3>
                                                <p class="text-muted text-center mt-2">
                                                    Von: <strong>{{order.from}}</strong></p>
                                                <p class="text-muted text-center mt-2">
                                                    Nach: <strong>{{order.to}}</strong></p>
                                                <p class="text-muted text-center" v-if="order.dist > 0">
                                                    Notwendige Kapazität: <strong>~{{order.capa}}KG</strong></p>
                                                <p class="text-muted text-center" v-if="order.dist > 0">
                                                    Gehalt: <strong style="color:green">~{{parseInt(parseInt(order.dist)*0.325)}}$ +
                                                        (Bonus)</strong></p>
                                            </div>
                                            <button type="button" class="btn btn-primary mr-3 ml-3 mb-2" @click="acceptOrder(order.id)">Annehmen</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="box box-default" v-if="speditionbizzshow">
                        <div class="card card-primary card-outline animate__animated animate__bounceInUp">
                            <div class="card-header" style="font-family: 'Exo', sans-serif;">Business Belieferungsübersicht
                            </div>
                            <div class="card-body" style="min-width: 250px !important">
                                <div class="row">
                                    <div class="col-md-3 col-md-offset-1-and-half" v-for="bizz in business" :key="bizz.id">
                                        <div class="card card-primary card-outline mr-2 ml-1" style="min-width: 150px !important;min-height: 125px !important">
                                            <div class="card-body">
                                                <h3 class="profile-username text-center mb-4">
                                                    {{bizz.name}} [{{bizz.id}}]</h3>
                                                <p class="text-muted text-center mt-2">
                                                    Besitzer: {{bizz.owner}}</p>
                                                <p class="text-muted text-center mt-2">
                                                    Benötigte Produkte: {{2000-bizz.products}} Produkte</p>
                                                <p class="text-muted text-center">
                                                    Geld für Produkte: {{Math.floor(bizz.cash/bizz.prodprice)}} Produkte</p>
                                            </div>
                                            <button type="button" class="btn btn-primary mr-3 ml-3 mb-2" @click="markBizz(bizz.posx, bizz.posy)">Markieren</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="box box-default" v-if="speditionvehicleshow">
                        <div class="card card-primary card-outline animate__animated animate__bounceInUp" style="min-height: 200px">
                            <div class="card-header" style="font-family: 'Exo', sans-serif;">Speditions Fuhrpark
                            </div>
                            <div class="card-body" style="min-width: 250px !important">
                                <div class="row">
                                    <div class="col-md-3 col-md-offset-1-and-half" v-for="vehicle in vehicles" :key="vehicle.id">
                                        <div class="card card-primary card-outline mr-2 ml-1" style="min-width: 150px !important;min-height: 125px !important">
                                            <div class="card-body">
                                                <div class="text-center">
                                                    <img class="cutimage profile-user-img img-fluid img-circle" height="25px" :src="getImgUrl(vehicle.name.toLowerCase())">
                                                </div>
                                                <h3 class="profile-username text-center mt-3">
                                                    {{vehicle.name}}</h3>
                                                <p class="text-muted text-center">
                                                    Ladekapazität: {{vehicle.capa}}KG</p>
                                                <p class="text-muted text-center">
                                                    <b>Ab Skill:</b> <strong>{{vehicle.skilltext}}</strong>
                                                </p>
                                            </div>
                                            <button type="button" class="btn btn-primary mr-3 ml-3 mb-2" @click="selectVehicle(vehicle.id)">Auswählen</button>
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
    name: 'Spediteur',
    data: function () {
        return {
            speditionvehicleshow: false,
            speditionbizzshow: false,
            speditionordershow: false,
            taxijobsshow: false,
            atmshow: false,
            secruityshow: false,
            orders: [],
            business: [],
            vehicles: [],
            taxiJobs: [],
            atmSpots: [],
            secruitySpots: [],
            clicked: (Date.now() / 1000)
        }
    },
    mounted() {
        document.body.classList.add("dark-mode");
    },
    components: {},
    methods: {
        getImgUrl(pic) {
            return require('../assets/images/vehicles/' + pic + '.png')
        },
        showVehicles: function (json) {
            this.vehicles = JSON.parse(json);
            this.speditionvehicleshow = !this.speditionvehicleshow;
        },
        showBizzWithNeeds: function (json) {
            this.business = JSON.parse(json);
            this.speditionbizzshow = !this.speditionbizzshow;
        },
        showOrders: function (json) {
            this.orders = JSON.parse(json);
            this.speditionordershow = !this.speditionordershow;
        },
        showTaxiJobs: function (json) {
            this.taxiJobs = JSON.parse(json);
            this.taxijobsshow = !this.taxijobsshow;
        },
        showATMSpots: function (json) {
            this.atmSpots = JSON.parse(json);
            this.atmshow = !this.atmshow;
        },
        showSecruity: function (json) {
            this.secruitySpots = JSON.parse(json);
            this.secruityshow = !this.secruityshow;
        },
        hideAll: function () {
            this.speditionvehicleshow = false;
            this.speditionbizzshow = false;
            this.speditionordershow = false;
            this.taxijobsshow = false;
            this.atmshow = false;
            this.secruityshow = false;
        },
        markBizz: function (posx, posy) {
            // eslint-disable-next-line no-undef
            mp.trigger("Client:CreateWaypoint", posx, posy, -1);
            // eslint-disable-next-line no-undef
            mp.trigger("Client:SendNotificationWithoutButton2", 'Business wurde markiert!', 'success', 'center', 2500);
        },
        acceptOrder: function (id) {
            // eslint-disable-next-line no-undef
            mp.trigger("Client:JobSettings", "acceptorder", '' + id);
        },
        acceptTaxi: function (id) {
            // eslint-disable-next-line no-undef
            mp.trigger("Client:JobSettings", "accepttaxi", '' + id);
        },
        acceptATM: function (id) {
            // eslint-disable-next-line no-undef
            mp.trigger("Client:JobSettings", "acceptatm", '' + id);
        },
        acceptSecruity: function (id) {
            // eslint-disable-next-line no-undef
            mp.trigger("Client:JobSettings", "acceptsecruity", '' + id);
        },
        selectVehicle: function (id) {
            // eslint-disable-next-line no-undef
            mp.trigger("Client:JobSettings", "selectvehiclespediteur", '' + id);
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
    height: 70%;
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
    width: 12vw !important;
    height: 7vw !important;
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
