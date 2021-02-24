import Vue from "vue";
import BootstrapVue from "bootstrap-vue";
import Loading from "vue-loading-overlay";
import Snotify from "vue-snotify";
import vuetify from "./plugins/vuetify";
import router from "./router";
import store from "./store";
import App from "./App.vue";

import "@/assets/styles/index.css";
import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap-vue/dist/bootstrap-vue.css";
import "vue-loading-overlay/dist/vue-loading.css";
import "vue-snotify/styles/material.css";

Vue.config.productionTip = false;

Vue.use(BootstrapVue);
Vue.use(Loading);
Vue.use(Snotify, {
  toast: {
    maxOnScreen: 4
  }
});

const vm = new Vue({
  store,
  router,
  Snotify,
  vuetify,
  render: h => h(App)
}).$mount("#app");

export default vm;
