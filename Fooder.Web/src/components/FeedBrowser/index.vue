<template>
  <div>
    <v-row justify="center" v-for="rowIndex in rowsAmount" :key="rowIndex">
      <v-col cols="3">
        <feed-card :feed="obtainCorrespondingFeed(rowIndex, 0)" />
      </v-col>
      <v-col cols="3">
        <feed-card
          v-if="isCardRenderRequired(rowIndex, 1)"
          :feed="obtainCorrespondingFeed(rowIndex, 1)"
        />
      </v-col>
      <v-col cols="3">
        <feed-card
          v-if="isCardRenderRequired(rowIndex, 2)"
          :feed="obtainCorrespondingFeed(rowIndex, 2)"
        />
      </v-col>
    </v-row>
  </div>
</template>

<script>
import { mapGetters, mapActions } from "vuex";
import FeedCard from "./components/FeedCard";

export default {
  name: "FeedBrowser",
  components: {
    FeedCard
  },
  data() {
    return {};
  },
  methods: {
    ...mapActions("feedModule", ["getFeed", "getAfflictions"]),
    ...mapActions("petModule", ["getDogActivityLevels"]),
    isCardRenderRequired(rowIndex, columnIndex) {
      const itemIndex = (rowIndex - 1) * 3 + columnIndex;
      const isContained = itemIndex < this.filteredFeed.length;
      return isContained;
    },
    obtainCorrespondingFeed(rowIndex, columnIndex) {
      const itemIndex = (rowIndex - 1) * 3 + columnIndex;
      const item = this.filteredFeed[itemIndex];
      return item;
    }
  },
  computed: {
    ...mapGetters("feedModule", ["filteredFeed"]),
    rowsAmount() {
      const fullRowsAmount = Math.floor(this.filteredFeed.length / 3);
      const optionalSummand = this.filteredFeed.length % 3 !== 0 ? 1 : 0;
      const sum = fullRowsAmount + optionalSummand;
      return sum;
    }
  },
  async mounted() {
    await this.getFeed();
    await this.getAfflictions();
    await this.getDogActivityLevels();
  }
};
</script>
