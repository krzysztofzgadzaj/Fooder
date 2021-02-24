<template>
  <div class="main-container">
    <!--<div class="title">
      {{ feed.name }}
    </div>
    <v-row no-gutters justify="space-between">
      <v-col cols="3">
        <v-rating
          :value="feed.averageRating"
          color="gold"
          half-increments
          :readonly="!userInfo"
          size="28"
        />
      </v-col>
      <v-col cols="3">
        <span>
          {{ ratingLabel }}
        </span>
      </v-col>
    </v-row>-->
    <v-textarea
      full-width
      no-resize
      outlined
      :background-color="theme.highlightColor"
      dark
      :value="feed.fullDescription"
      readonly
    >
    </v-textarea>
    <v-row no-gutters>
      <v-col>
        <h2 class="title mb-2">Afflictions</h2>
        <v-chip-group column dark readonly>
          <v-chip
            v-for="affliction in feedAfflictions"
            :key="affliction"
            dark
            :color="theme.highlightColor"
          >
            {{ affliction }}
          </v-chip>
        </v-chip-group>
      </v-col>
      <v-col>
        <h2 class="title mb-2">Informations</h2>
        <v-chip-group column dark readonly>
          <v-chip readonly dark :color="theme.highlightColor">
            {{ feed.brandName }}
          </v-chip>
          <v-chip readonly dark :color="theme.highlightColor">
            {{ weightLabel }}
          </v-chip>
          <v-chip readonly dark :color="theme.highlightColor">
            {{ priceLabel }}
          </v-chip>
          <v-chip readonly dark :color="theme.highlightColor">
            {{ activityLevelLabel }}
          </v-chip>
        </v-chip-group>
      </v-col>
    </v-row>
    <v-row>
      <v-col>
        <v-btn
          dark
          :color="theme.highlightColor"
          large
          @click="goToProducerSite"
        >
          Order now on producer site!
        </v-btn>
      </v-col>
    </v-row>
  </div>
</template>

<script>
import { DefaultTheme } from "@/shared/constants";
import { mapGetters } from "vuex";

export default {
  name: "FeedReviews",
  data: () => ({
    theme: DefaultTheme,
    isReviewAdditionDialogVisible: false
  }),
  props: {
    feed: Object
  },
  computed: {
    ...mapGetters("userModule", ["userInfo"]),
    ...mapGetters("feedModule", ["afflictions"]),
    ...mapGetters("petModule", ["activityLevels"]),
    ratingLabel() {
      return `${this.feed.averageRating} (${this.feed.ratingCount})`;
    },
    weightLabel() {
      return `from ${this.feed.minWeight} kg to ${this.feed.maxWeight} kg`;
    },
    priceLabel() {
      return `Price: ${this.feed.price}$`;
    },
    feedAfflictions() {
      return this.afflictions
        .map(x => x.name)
        .filter(x => this.feed.dogAfflictions.includes(x));
    },
    activityLevelLabel() {
      return this.activityLevels.find(x => x.key === this.feed.dogActivityLevel)
        .name;
    }
  },
  methods: {
    openReviewAdditionDialog() {
      this.isReviewAdditionDialogVisible = true;
    },
    closeReviewAdditionDialog() {
      this.isReviewAdditionDialogVisible = false;
    },
    goToProducerSite() {
      window.location = this.feed.producerSiteLink;
    }
  },
  async mounted() {}
};
</script>

<style lang="scss" scoped>
.title {
  overflow-y: auto;
  max-height: 18em;
  font-size: 40px;
  justify-items: center;
}
.main-container {
  max-height: 25em;
}
.text-field {
  background-color: darkred;
}
</style>
