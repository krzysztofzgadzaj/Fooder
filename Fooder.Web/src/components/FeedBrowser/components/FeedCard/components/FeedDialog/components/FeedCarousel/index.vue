<template>
  <div class="main-container">
    <v-carousel
      v-if="selectedFeedImages.length > 0"
      cycle
      height="300"
      class="carousel"
      hide-delimiters
    >
      <template v-slot:prev="{ on, attrs }">
        <v-btn fab :color="theme.highlightColor" v-bind="attrs" v-on="on">
          <v-icon>mdi-arrow-left</v-icon>
        </v-btn>
      </template>
      <template v-slot:next="{ on, attrs }">
        <v-btn fab :color="theme.highlightColor" v-bind="attrs" v-on="on">
          <v-icon>mdi-arrow-right</v-icon>
        </v-btn>
      </template>
      <v-carousel-item
        v-for="(photo, index) in selectedFeedImages"
        :key="index"
        :src="photo"
      ></v-carousel-item>
    </v-carousel>
    <span v-else class="fun-label">This pet has no photos :(</span>
  </div>
</template>

<script>
import { mapActions, mapGetters } from "vuex";
import DefaultFeedImage from "@/assets/images/dogo.png";

export default {
  name: "FeedCarousel",
  data: () => ({}),
  props: {
    feed: Object
  },
  computed: {
    ...mapGetters("feedModule", ["feedImages"]),
    selectedFeedImages() {
      const feedPhotos = this.feedImages.filter(
        image => image.feedId === this.feed.id
      );
      return Array.isArray(feedPhotos) && feedPhotos.length > 0
        ? feedPhotos.map(photo => photo.image.content)
        : [DefaultFeedImage];
    }
  },
  methods: {
    ...mapActions("feedModule", ["getFeedImage"])
  },
  async mounted() {
    const photoStreamPromiseCollection = this.feed.photosIds.map(photoId =>
      this.getFeedImage({
        feedId: this.feed.id,
        photoId: photoId
      })
    );
    await Promise.all(photoStreamPromiseCollection);
  }
};
</script>

<style lang="scss" scoped>
.main-container {
  max-height: 35em;
  width: 100%;
}
</style>
