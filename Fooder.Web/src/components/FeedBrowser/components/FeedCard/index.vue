<template>
  <v-card
    @click="showFeedDetailsDialog"
    @mouseenter="darken"
    @mouseleave="lighten"
    elevation="7"
    class="mx-auto my-12 rounded-card"
    max-width="320"
    max-height="455"
  >
    <v-overlay v-if="isHovered" z-index="2" absolute>
      <v-icon large>mdi-magnify</v-icon>
    </v-overlay>
    <v-img class="rounded-top-corners" height="250" :src="feedImage" />
    <v-card-title>{{ feed.name }}</v-card-title>
    <v-card-text>
      <v-row align="center" class="mx-0">
        <v-rating
          :value="feed.averageRating"
          color="amber"
          dense
          half-increments
          readonly
          size="14"
        />
        <div class="grey--text ml-4">
          {{ ratingLabel }}
        </div>
      </v-row>
      <v-row align="center">
        <v-img class="image mx-3 my-3" :src="feedTypeIcon" max-width="30" />
        {{ feed.feedType }}
      </v-row>
      <div class="card-label">
        {{ feed.shortDescription }}
      </div>
    </v-card-text>
    <v-divider class="mx-4"></v-divider>
    <feed-dialog
      v-if="showFeedDetails"
      :dialogVisibility="showFeedDetails"
      :feed="feed"
      @closeDialog="closeFeedDialog"
    />
  </v-card>
</template>

<script>
import { mapActions, mapGetters } from "vuex";
import DefaultFeedImage from "@/assets/images/dogo.png";
import FeedTypeIcon from "@/assets/icons/dog-feed.svg";
import FeedDialog from "./components/FeedDialog";
export default {
  name: "FeedCard",
  data() {
    return {
      feedTypeIcon: FeedTypeIcon,
      isHovered: false,
      showFeedDetails: false
    };
  },
  components: {
    FeedDialog: FeedDialog
  },
  props: {
    feed: Object
  },
  computed: {
    ...mapGetters("feedModule", ["feedImages"]),
    ratingLabel() {
      return `${this.feed.averageRating} (${this.feed.ratingCount})`;
    },
    feedImage() {
      const feedPhoto = this.feedImages.find(
        image => image.feedId === this.feed.id
      );
      return feedPhoto ? feedPhoto.image.content : DefaultFeedImage;
    }
  },
  methods: {
    ...mapActions("feedModule", ["getFeedImage"]),
    darken() {
      this.isHovered = true;
    },
    lighten() {
      this.isHovered = false;
    },
    showFeedDetailsDialog() {
      this.showFeedDetails = true;
    },
    closeFeedDialog() {
      this.showFeedDetails = false;
    },
    async getPhoto() {
      const photoId = this.feed.photosIds[0];
      await this.getFeedImage({
        feedId: this.feed.id,
        photoId: photoId
      });
    }
  },
  async mounted() {
    const hasAnyPhotos =
      Array.isArray(this.feed.photosIds) && this.feed.photosIds.length > 0;
    if (hasAnyPhotos) {
      await this.getPhoto();
    }
  }
};
</script>

<style lang="scss" scoped>
.rounded-card {
  border-radius: 20px;
}
.card-label {
  text-overflow: ellipsis;
  overflow: hidden;
  width: 90%;
  white-space: nowrap;
  margin-top: 10px;
}
.rounded-top-corners {
  border-top-left-radius: 20px;
  border-top-right-radius: 20px;
}
</style>
