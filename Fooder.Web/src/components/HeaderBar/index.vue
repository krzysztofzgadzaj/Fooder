<template>
  <v-app-bar app clipped-left :color="theme.mainColor">
    <v-toolbar-title class="headline">
      <span class="fun-label app-title">Fooder</span>
    </v-toolbar-title>
    <v-img max-width="50" max-height="50" :src="dogo" />
    <v-spacer />
    <user-panel v-if="isAuthenticated" />
    <authentication-panel v-else />
  </v-app-bar>
</template>

<script>
import { DefaultTheme } from "@/shared/constants";
import AuthenticationPanel from "./components/AuthenticationPanel";
import UserPanel from "./components/UserPanel";
import dogo from "@/assets/images/dogo.png";
import { mapGetters } from "vuex";

export default {
  name: "HeaderBar",
  data() {
    return {
      dogo,
      theme: DefaultTheme
    };
  },
  components: {
    AuthenticationPanel,
    UserPanel
  },
  computed: {
    ...mapGetters("userModule", ["userInfo"]),
    isAuthenticated() {
      return this.userInfo !== null;
    }
  }
};
</script>

<style lang="scss" scoped>
@import "@/assets/styles/_colors.scss";

.app-title {
  font-size: 40px;
  color: $white;
  margin-right: 10px;
  font-style: italic;
  font-weight: bold;
}
</style>
