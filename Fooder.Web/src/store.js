import Vue from "vue";
import Vuex from "vuex";

import requestModule from "@/shared/modules/Request/module";
import userModule from "@/shared/modules/User/module";
import feedModule from "@/shared/modules/Feed/module";
import petModule from "@/shared/modules/Pet/module";
import brandModule from "@/shared/modules/Brand/module";

Vue.use(Vuex);

const modules = {
  requestModule,
  userModule,
  feedModule,
  petModule,
  brandModule
};

const store = new Vuex.Store({
  modules: modules
});

export default store;
