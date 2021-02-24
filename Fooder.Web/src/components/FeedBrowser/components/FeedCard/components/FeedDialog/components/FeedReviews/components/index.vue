<template>
  <v-dialog persistent width="500" v-model="visible">
    <v-card :color="theme.applicationBackgroundColor">
      <v-card-title class="fun-label">
        Your review
      </v-card-title>
      <v-row style="width: 1150px;"> </v-row>
      <v-col cols="9">
        <v-textarea outlined :rules="rules" v-model="review"></v-textarea>
      </v-col>
      <v-card-actions>
        <v-btn
          @click="addReview"
          class="mx-2"
          fab
          :color="theme.highlightColor"
        >
          <v-icon color="white">
            mdi-plus
          </v-icon>
        </v-btn>
        <v-spacer />
        <v-btn
          @click="closeDialog"
          width="100"
          elevation="5"
          :color="theme.highlightColor"
          class="mr-3"
        >
          <span class="button-label">Close</span>
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import { DefaultTheme } from "@/shared/constants";
import { mapActions } from "vuex";
import vm from "@/main";

export default {
  name: "ReviewDialog",
  data: () => ({
    theme: DefaultTheme,
    review: "",
    rules: [v => v.length <= 200 || "Max 200 characters"]
  }),
  methods: {
    ...mapActions("feedModule", ["postReview", "getFeed"]),
    closeDialog() {
      this.$emit("closeDialog");
    },
    async addReview() {
      if (this.review.length > 19) {
        const data = {
          feedId: this.feedId,
          review: this.review
        };
        await this.postReview(data);
        this.closeDialog();
      } else {
        vm.$snotify.error(
          "Your review is too short. It needs to be atleast 20 signs."
        );
      }
    }
  },
  props: {
    visible: Boolean,
    feedId: Number
  }
};
</script>

<style scoped></style>
