<template>
  <v-row justify="center">
    <v-dialog persistent v-model="dialogVisibility" width="600">
      <v-card>
        <v-card-title>
          <span class="headline">{{ title }}</span>
        </v-card-title>
        <v-card-text>
          {{ details }}
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn
            @click="deny"
            width="100"
            elevation="5"
            :color="theme.highlightColor"
            class="mr-3"
          >
            <span class="button-label">No</span>
          </v-btn>
          <v-btn
            @click="confirm"
            width="100"
            elevation="5"
            :color="theme.highlightColor"
            class="mr-3"
          >
            <span class="button-label">Yes</span>
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-row>
</template>

<script>
import { DefaultTheme } from "@/shared/constants";

export default {
  name: "ConfirmationDialog",
  data: () => ({
    theme: DefaultTheme
  }),
  props: {
    dialogVisibility: Boolean,
    title: String,
    details: String
  },
  methods: {
    confirm() {
      this.$emit("confirmed");
    },
    deny() {
      this.$emit("denied");
    }
  }
};
</script>
