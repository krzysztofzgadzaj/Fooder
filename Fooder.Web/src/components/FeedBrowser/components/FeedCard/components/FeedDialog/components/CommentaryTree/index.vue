<template>
  <div class="main-container">
    <div class="treeview-container">
      <v-treeview
        class="commentary-treeview"
        :items="feedComments"
        item-key="id"
        item-text="content"
        item-children="relatedComments"
      >
        <template v-slot:prepend="{ item }">
          <answer-card
            v-if="item.level === 0 && userInfo"
            :parentCommentId="item.id"
            :feedId="feedId"
          />
        </template>
        <template v-slot:append="{ item }">
          <v-icon
            v-if="item.level === 0 && userInfo && !item.userMark"
            class="spacious"
            @click="like(item.id)"
          >
            mdi-thumb-up-outline
          </v-icon>
          <v-icon
            v-if="item.level === 0 && userInfo && item.userMark === likeMessage"
            class="spacious"
          >
            mdi-thumb-up
          </v-icon>
        </template>
      </v-treeview>
    </div>
    <v-row>
      <v-col cols="9">
        <v-text-field
          :counter="1000"
          v-model="newComment"
          label="Type your comment"
          :color="theme.highlightColor"
        />
      </v-col>
      <v-col cols="3">
        <v-btn
          @click="addComment"
          :color="theme.highlightColor"
          class="my-3 confirm-button"
          :disabled="isCommentAdditionDisabled"
        >
          <span style="color:white;">Send</span>
        </v-btn>
      </v-col>
    </v-row>
  </div>
</template>

<script>
import { mapActions, mapGetters } from "vuex";
import { DefaultTheme } from "@/shared/constants";
import AnswerCard from "./components/AnswerCard";
import { LikeMessage } from "./constants";

export default {
  name: "CommentaryTree",
  data: () => ({
    theme: DefaultTheme,
    newComment: "",
    answerCommentCardState: false,
    likeMessage: LikeMessage
  }),
  components: {
    AnswerCard
  },
  props: {
    feedId: Number
  },
  computed: {
    ...mapGetters("feedModule", ["feedComments"]),
    ...mapGetters("userModule", ["userInfo"]),
    isCommentAdditionDisabled() {
      return (
        !this.newComment || this.newComment.length > 1000 || !this.userInfo
      );
    }
  },
  methods: {
    ...mapActions("feedModule", [
      "getFeedComments",
      "addFeedComment",
      "addCommentMark"
    ]),
    async addComment() {
      const request = {
        feedId: this.feedId,
        content: this.newComment,
        authorName: this.userInfo.login
      };
      await this.addFeedComment(request);
      await this.getFeedComments({
        feedId: this.feedId,
        login: this.userInfo.login
      });
      this.newComment = "";
    },
    async like(commentId) {
      const request = {
        userName: this.userInfo.login,
        commentId: commentId,
        mark: LikeMessage,
        feedId: this.feedId
      };
      await this.addCommentMark(request);
      await this.getFeedComments({
        feedId: this.feedId,
        login: this.userInfo.login
      });
    }
  },
  async mounted() {
    await this.getFeedComments({
      feedId: this.feedId,
      login: this.userInfo ? this.userInfo.login : ""
    });
  }
};
</script>

<style lang="scss" scoped>
.treeview-container {
  overflow-y: auto;
  max-height: 28em;
}
.main-container {
  max-height: 35em;
  width: 100%;
}
.spacious {
  margin-left: 200px;
}
.confirm-button {
  width: 100%;
}
.commentary-treeview {
  text-overflow: ellipsis;
  overflow: hidden;
  max-width: 800px;
}
</style>
