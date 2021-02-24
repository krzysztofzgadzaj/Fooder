<template>
  <v-footer app dark :color="theme.commonBackgroundColor" padless>
    <v-row justify="center">
      <v-card-text class="text-center">
        <strong>2020 - Fooder</strong>
      </v-card-text>
    </v-row>
  </v-footer>
</template>

<script>
import { DefaultTheme } from "@/shared/constants";

export default {
  name: "FooterBar",
  data() {
    return {
      theme: DefaultTheme
    };
  }
};
</script>
