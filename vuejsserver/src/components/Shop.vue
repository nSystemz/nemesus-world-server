<template>
  <div class="shop" style="z-index: 1; overflow: hidden; background-color:transparent; scrollbar-width: none;">
    <div style="height: 100%; background-color: transparent;" v-if="showshopmenu2">
      <div class="row justify-content-center centering4">
        <div class="col-md-12 mt-1 animate__animated animate__bounceIn">
          <div class="col-md-12 mt-1">
            <div class="box box-default">
              <div class="row">
                <div class="card card-primary card-outline">
                  <div class="card-header" style="font-family: 'Exo', sans-serif; font-size: 1.2vw">
                    {{shopname2}}
                  </div>
                  <div class="card-body" style="max-height:60vh; width: 25vw; overflow-x: auto">
                    <div class="row">
                      <div class="col-md-12">
                        <div v-for="(shopitem, index) in shoptext" :key="index"
                          class="card2 card card-primary card-outline text-center">
                          <div class="settext ml-2" style="cursor:pointer; margin-top: -0.1vw" @click="buyItem2(index)">
                            {{shoptext[index]}} <span class="badge badge-dark"
                              v-if="shopname2 != 'Laufstilauswahl' && shoptext2 && shoptext2 != '0' && shoptext2[index] != '0' && shoptext2[index] != ''">{{shoptext2[index]}}{{dollar}}</span>
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
    <div class="centering2" style="height: 100%;" v-if="showshopmenu">
      <div class="row justify-content-center animate__animated animate__bounceInUp centering">
        <div class="col-md-12">
          <div class="col-md-12">
            <div class="box box-default" v-if="showshopmenu">
              <div class="card card-primary card-outline"
                style="min-height: 25vw;max-height: 83vh; scrollbar-width: thin;overflow-x: auto;">
                <div class="card-header" style="font-family: 'Exo', sans-serif">{{shopname}}
                  <button type="button" @click="buyItem('n/A',0,3)" class="btn btn-primary float-right"
                    v-if="shopname == 'Waffenkammer LSPD' || shopname == 'Lager LSRC' || shopname == 'Lager ACLS'">Alles zurücklegen</button>
                </div>
                <div class="card-body">
                  <div class="col-md-12">
                    <div class="row">
                      <div class="col-md-2" v-for="item in items" :key="item.id">
                        <div class="card card-primary card-outline"
                          style="min-width: 150px !important;min-height: 125px !important">
                          <div class="card-body" style="min-height: 18vh; max-height: 32vh">
                            <div class="text-center">
                              <img
                                v-if="item.itemname != 'Taschenlampe' && item.itemname != 'Brechstange' && item.itemname != 'Axt' && item.itemname != 'Messer' && item.itemname != 'Schlagstock' && item.itemname != 'Baseballschläger' && item.itemname != 'Pumpshotgun' && item.itemname != 'Karabiner-Gewehr-MK2' && item.itemname != 'Sniper' && item.itemname != 'Musket' && item.itemname != 'BZGas' && item.itemname != 'Rauchgranate'"
                                class="cutimage profile-user-img img-fluid img-circle" height="25px"
                                :src="getImgUrl(item.itemname)">
                              <img v-else-if="item.itemname == 'BZGas' || item.itemname == 'Rauchgranate'"
                                class="cutimage3 profile-user-img img-fluid img-circle" height="30px" width="20px"
                                :src="getImgUrl(item.itemname)">
                              <img v-else class="cutimage2 profile-user-img img-fluid img-circle"
                                style="margin-top: 1.7vw" height="16px" :src="getImgUrl(item.itemname)">
                            </div>
                            <h3 v-if="item.itemname == 'Haustier' && shopname == 'Waffenkammer LSPD'" class="text-center mt-3" style="font-size: 1vw">
                              K9-Shepherd</h3>
                            <h3 v-else class="text-center mt-3" style="font-size: 1vw">
                              {{item.itemname}}</h3>
                            <p class="text-muted text-center" v-if="shopname != 'Waffenkammer LSPD' && shopname != 'Lager LSRC' && shopname != 'Lager ACLS'" style="font-size: 0.7vw">
                              Preis: {{item.itemprice}}$</p>
                            <p class="text-muted text-center" v-if="item.itemamount != -1" style="font-size: 0.7vw">
                              Menge: {{item.itemamount}}/Stck</p>
                            <p class="text-muted text-center" v-if="shopname == 'Waffenkammer LSPD' || shopname == 'Lager LSRC' || shopname == 'Lager ACLS'" style="font-size: 0.7vw">
                              Menge: {{item.itemprice}}/Stck</p>
                            <div style="display: flex; justify-content: center; align-items: center;margin-top:0.6vw">
                            <input
                              v-if="item.itemname != 'Pfandflasche' && item.itemname != 'Tankrechnung' && item.itemname != 'Smartphone' && item.itemname != 'Benzinkanister' && item.itemname != 'Handyvertrag'"
                              type="text" v-model="itemSize[item.itemname]" placeholder="1" maxlength="4"
                              class="form-control text-center col-sm-6" style="border-radius: 1vw;font-size:0.8vw">
                            <input
                              v-else disabled
                              type="text" v-model="itemSize[item.itemname]" placeholder="1" maxlength="4"
                              class="form-control text-center col-sm-6" style="border-radius: 1vw;font-size:0.8vw">
                          </div>
                            <div v-if="shopname != 'Waffenkammer LSPD' && shopname != 'Lager LSRC' && shopname != 'Lager ACLS'" style="margin-top:1vw">
                              <div style="display: flex; justify-content: center; align-items: center;margin-top:0.6vw">
                                  <button v-if="item.itemname == 'Pfandflasche'"
                                    @click="buyItem(item.itemname,item.itemprice,1)" type="button"
                                    class="btn btn-primary" style="font-size: 0.7vw">Verkaufen</button>
                                  <button v-if="item.itemname != 'Pfandflasche'"
                                    @click="buyItem(item.itemname,item.itemprice,1)" type="button"
                                    class="btn btn-primary" style="font-size: 0.7vw">Bar</button>
                                  <button v-if="item.itemname != 'Pfandflasche' && shopname != 'Waffendealer'"
                                    @click="buyItem(item.itemname,item.itemprice,2)" type="button"
                                    class="btn btn-primary ml-2" style="font-size: 0.7vw">EC-Karte</button>
                              </div>
                            </div>
                            <div v-else>
                              <div style="display: flex; justify-content: center; align-items: center;margin-top:0.6vw">
                                <button @click="buyItem(item.itemname,item.itemprice,1)" style="font-size: 0.7vw" type="button"
                                  class="btn btn-primary ml-1"><i class="fa-solid fa-arrow-down"></i></button>
                                <button @click="buyItem(item.itemname,item.itemprice,2)" style="font-size: 0.7vw" type="button"
                                  class="btn btn-primary ml-1"><i class="fa-solid fa-arrow-up"></i></button>
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
  name: 'Shop',
  data: function() {
    return {
      dollar: '$',
      itemSize: [],
      showshopmenu: false,
      showshopmenu2: false,
      shoptext: [],
      shoptext2: [],
      items: [],
      shopname: '',
      shopname2: '',
      clicked: (Date.now()/1000)
    }
  },
  mounted() {
    document.body.classList.add("dark-mode");
  },
  components: {
  },
  methods: {
    getImgUrl(pic) {
      return require('../assets/images/inventory/' + pic + '.png')
    },
    showShop: function (json, update, shopname) 
    {
      if(parseInt(update) == 0)
      {
        this.showshopmenu = !this.showshopmenu;
      }
      this.itemSize = [];
      this.clicked = 0;
      this.shopname = shopname;
      this.items = JSON.parse(json);
    },
    showShopMenu2: function (text1, text2, shopname, update) 
    {
      if(parseInt(update) == 0)
      {
        this.showshopmenu2 = !this.showshopmenu2;
      }
      this.clicked = 0;
      this.shoptext = text1.split(',');
      this.shoptext2 = text2.split(',');
      this.shopname2 = shopname;
      if(shopname.toLowerCase() != "animationsmenü" && shopname.toLowerCase() != "animationsauswahl")
      {
        if(shopname.toLowerCase() != "mechatronikermenü" && shopname.toLowerCase() != "routenauswahl")
        {
          this.dollar = '$';
        }
        else
        {
          if(shopname.toLowerCase() == "mechatronikermenü")
          {
            this.dollar = ' Utensilien';
          }
          else
          {
            this.dollar = ' (Skill)';
          }
        }
      }
      else
      {
        this.dollar = '';
      }
    },
    buyItem: function (itemname, price, choose)
    {
      if ((Date.now() / 1000) > this.clicked) {
        var value = 1;
        if(this.itemSize[itemname])
        {
          value = this.itemSize[itemname];
        }
        // eslint-disable-next-line no-undef
        mp.trigger("Client:BuyShopItem", itemname, price*value, choose, this.shopname, value);
        this.clicked = (Date.now() / 1000) + (2);
      }
    },
    buyItem2: function (index)
    {
      if ((Date.now() / 1000) > this.clicked) {
        // eslint-disable-next-line no-undef
        mp.trigger("Client:BuyShopItem2", this.shoptext[index], this.shoptext2[index]);
        this.clicked = (Date.now() / 1000) + (2);
      }
    }
  }
}
</script>

<style scoped>
html, body, template, * {
  -webkit-user-select: none;
  -khtml-user-select: none;
  -moz-user-select: none;
  -o-user-select: none;
  user-select: none;
}

html {
  scrollbar-width: none;
  /* For Firefox */
  -ms-overflow-style: none;
  /* For Internet Explorer and Edge */
}

html::-webkit-scrollbar {
  width: 0px;
  /* For Chrome, Safari, and Opera */
}

.centering {
  width: 100%;
  height: 95%;
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
    height: 95%;
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
  width: 4vw !important;
  height: 4vw !important;
}

.cutimage2 {
  width: 4vw !important;
  height: 2vw !important;
}

.cutimage3 {
  width: 2vw !important;
  height: 4vw !important;
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

.centering4 {
  margin: 0;
  position: absolute;
  top: 50%;
  right: 50%;
  margin-left: -50%;
  transform: translate(50%, -50%)
}
</style>
