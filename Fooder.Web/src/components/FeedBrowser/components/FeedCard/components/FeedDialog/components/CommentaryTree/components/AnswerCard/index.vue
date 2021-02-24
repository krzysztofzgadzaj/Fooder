<template>
  <div>
    <v-menu
      v-model="isVisible"
      :close-on-content-click="false"
      :nudge-width="500"
      offset-x
    >
      <template v-slot:activator="{ on }">
        <v-btn text min-height="15px" min-width="15px" v-on="on">
          <v-icon>
            mdi-message-text
          </v-icon>
        </v-btn>
      </template>
      <v-card>
        <v-text-field
          class="mx-5 greedy-top"
          :counter="1000"
          v-model="answer"
          label="Type your answer"
          :color="theme.highlightColor"
        />
        <v-btn
          @click="addComment"
          :color="theme.highlightColor"
          class="ml-3 my-3 confirm-button"
          :disabled="isCommentAdditionDisabled"
        >
          <span style="color:white;">Send</span>
        </v-btn>
      </v-card>
    </v-menu>
  </div>
</template>

<script>
import { mapActions, mapGetters } from "vuex";
import { DefaultTheme } from "@/shared/constants";

export default {
  name: "AnswerCard",
  data: () => ({
    isVisible: false,
    answer: "",
    theme: DefaultTheme
  }),
  props: {
    parentCommentId: Number,
    feedId: Number
  },
  computed: {
    ...mapGetters("userModule", ["userInfo"]),
    isCommentAdditionDisabled() {
      return !this.answer || this.answer.length > 1000 || !this.userInfo;
    }
  },
  methods: {
    ...mapActions("feedModule", ["getFeedComments", "addFeedComment"]),
    async addComment() {
      const request = {
        feedId: this.feedId,
        content: this.answer,
        authorName: this.userInfo.login,
        parentCommentId: this.parentCommentId
      };
      await this.addFeedComment(request);
      await this.getFeedComments({
        feedId: this.feedId,
        login: this.userInfo.login
      });
      this.answer = "";
    }
  }
};
</script>

<style lang="scss" scoped>
.greedy-top {
  padding-top: 20px;
}
</style>
