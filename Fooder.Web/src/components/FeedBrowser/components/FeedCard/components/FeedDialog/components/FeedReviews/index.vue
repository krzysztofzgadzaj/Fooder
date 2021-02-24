<template>
  <div class="main-container">
    <div class="timeline-container">
      <v-timeline dense>
        <v-timeline-item
          v-for="review in feedReviews"
          :key="review.id"
          :color="theme.highlightColor"
          icon="mdi-account"
          class="mr-3"
        >
          <v-card class="elevation-2">
            <v-card-title class="headline">
              {{ review.author }}
            </v-card-title>
            <v-card-text>
              {{ review.content }}
            </v-card-text>
          </v-card>
        </v-timeline-item>
      </v-timeline>
    </div>
    <v-btn
      :disabled="!isReviewAdditionAuthorized"
      class="sticked-right my-5 mr-8"
      rounded
      :color="theme.highlightColor"
      dark
      @click="openReviewAdditionDialog"
    >
      ADD REVIEW
    </v-btn>
    <feed-reviews
      v-if="isReviewAdditionDialogVisible"
      :visible="isReviewAdditionDialogVisible"
      :feed-id="feedId"
      @closeDialog="closeReviewAdditionDialog"
    />
  </div>
</template>

<script>
import { DefaultTheme } from "@/shared/constants";
import { mapActions, mapGetters } from "vuex";
import { Permissions } from "@/shared/constants";
import ReviewDialog from "./components/index";

export default {
  name: "FeedReviews",
  data: () => ({
    theme: DefaultTheme,
    isReviewAdditionDialogVisible: false
  }),
  components: {
    feedReviews: ReviewDialog
  },
  props: {
    feedId: Number
  },
  computed: {
    ...mapGetters("feedModule", ["feedReviews"]),
    ...mapGetters("userModule", ["isReviewAdditionAuthorized"])
  },
  methods: {
    ...mapActions("feedModule", ["getFeedReviews"]),
    ...mapActions("userModule", ["authorize"]),
    openReviewAdditionDialog() {
      this.isReviewAdditionDialogVisible = true;
    },
    closeReviewAdditionDialog() {
      this.isReviewAdditionDialogVisible = false;
    }
  },
  async mounted() {
    await this.getFeedReviews(this.feedId);
    await this.authorize(Permissions.AddReviewPermissionCode);
  }
};
</script>

<style lang="scss" scoped>
.timeline-container {
  overflow-y: auto;
  max-height: 18em;
}
.main-container {
  max-height: 25em;
}
.sticked-right {
  float: right;
}
</style>
