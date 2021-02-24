<template>
  <v-app class="app-wrapper">
    <header-bar />
    <feed-filter-drawer />
    <v-main>
      <feed-browser />
      <router-view />
      <loader />
    </v-main>
    <vue-snotify />
    <footer-bar />
  </v-app>
</template>

<script>
import Loader from "@/shared/components/Loader";
import HeaderBar from "@/components/HeaderBar";
import FeedFilterDrawer from "@/components/FeedFilterDrawer";
import FeedBrowser from "@/components/FeedBrowser";
import FooterBar from "@/components/FooterBar";
import { DefaultTheme } from "@/shared/constants";
import { mapActions } from "vuex";

export default {
  name: "App",
  data() {
    return {
      theme: DefaultTheme
    };
  },
  methods: {
    ...mapActions("userModule", ["synchronizeUserInfo"])
  },
  components: {
    Loader,
    HeaderBar,
    FeedFilterDrawer,
    FeedBrowser,
    FooterBar
  },
  created() {
    this.synchronizeUserInfo();
  }
};
</script>

<style lang="scss" scoped>
@import "@/assets/styles/_colors.scss";

.app-wrapper {
  background: $pale-pink;
}
</style>
