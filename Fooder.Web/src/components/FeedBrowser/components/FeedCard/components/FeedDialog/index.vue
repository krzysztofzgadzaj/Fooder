<template>
  <v-dialog hide-overlay fullscreen persistent v-model="dialogVisibility">
    <v-card>
      <v-toolbar dark :color="theme.highlightColor">
        <v-btn icon dark @click="closeDialog">
          <v-icon>mdi-close</v-icon>
        </v-btn>
        <v-toolbar-title>{{ feed.name }}</v-toolbar-title>
      </v-toolbar>
      <v-row class="mx-10" style="width: 95%;" justify="center" align="center">
        <v-col cols="6">
          <v-row style="width: 100%;" justify="center" align="center">
            <feed-reviews :feedId="feed.id" />
          </v-row>
          <v-divider
            style="width: 75%;margin-left: 10%;"
            class="my-5"
          ></v-divider>
          <v-row style="width: 100%;" justify="center" align="center">
            <feed-details :feed="feed" />
          </v-row>
        </v-col>
        <v-col cols="6">
          <v-row style="width: 100%;" justify="center" align="center">
            <feed-carousel :feed="feed" />
          </v-row>
          <v-divider class="mr-5 my-3"></v-divider>
          <v-row style="width: 100%;" justify="center" align="center">
            <commentary-tree :feedId="feed.id" />
          </v-row>
        </v-col>
      </v-row>
    </v-card>
  </v-dialog>
</template>

<script>
import { DefaultTheme } from "@/shared/constants";
import FeedReviews from "./components/FeedReviews";
import FeedDetails from "./components/FeedDetails";
import CommentaryTree from "./components/CommentaryTree";
import FeedCarousel from "./components/FeedCarousel";

export default {
  name: "FeedDialog",
  data() {
    return {
      theme: DefaultTheme
    };
  },
  components: {
    FeedReviews,
    FeedDetails,
    CommentaryTree,
    FeedCarousel
  },
  props: {
    feed: Object,
    dialogVisibility: {
      type: Boolean,
      default: false
    }
  },
  methods: {
    closeDialog() {
      this.$emit("closeDialog");
    }
  },
  async mounted() {
    // ToDo: We have to get photos and comments to display them. (Photos might already be stored in module depending on implementation.)
  }
};
</script>
