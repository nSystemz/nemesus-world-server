import Vue from 'vue'
import App from './App.vue'

Vue.config.productionTip = false

global.gui = { loading: null, login: null, register: null, characterswitch: null, charactercreator: null, hud: null, menu: null, rpquiz: null, inventory: null, selectwheel: null, lockpicking: null, mdc: null }

new Vue({
  render: h => h(App),
}).$mount('#app')