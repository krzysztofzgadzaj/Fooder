<template>
  <v-dialog width="800" persistent v-model="dialogVisibility">
    <v-card :color="theme.applicationBackgroundColor">
      <v-card-title class="fun-label">
        Add feed
      </v-card-title>
      <div class="input-container">
        <v-row style="width: 800px;" align="center" justify="center">
          <v-col>
            <v-form v-model="isValid">
              <v-text-field
                :counter="20"
                :rules="nameRules"
                v-model="name"
                :color="theme.highlightColor"
                label="Name"
                class="input"
              ></v-text-field>
              <v-select
                :rules="requiredFieldRules"
                class="input"
                :color="theme.highlightColor"
                item-color="red"
                :items="brands"
                label="Brand"
                v-model="selectedBrand"
                item-text="name"
                item-value="id"
              />
              <v-select
                :rules="requiredFieldRules"
                class="input"
                :color="theme.highlightColor"
                item-color="red"
                :items="feedTypes"
                label="Feed type"
                v-model="selectedFeedType"
                item-text="name"
                item-value="key"
              />
              <v-range-slider
                :color="theme.highlightColor"
                v-model="weightRange"
                max="99"
                min="0"
                hide-details
                class="align-center ml-8"
                label="Weight"
              >
                <template v-slot:append>
                  <v-text-field
                    disabled
                    :value="rangeString"
                    class="mt-0 pt-0 mr-9"
                    hide-details
                    single-line
                    style="width: 70px"
                    @change="$set(weightRange, 1, $event)"
                  ></v-text-field>
                </template>
              </v-range-slider>
              <v-text-field
                :rules="requiredFieldRules"
                v-model="price"
                :color="theme.highlightColor"
                label="Price"
                type="number"
                step="0.01"
                class="input"
              ></v-text-field>
              <v-text-field
                :rules="requiredFieldRules"
                v-model="producerWebsite"
                :color="theme.highlightColor"
                label="Producer website"
                class="input"
              ></v-text-field>
              <v-select
                :rules="requiredFieldRules"
                class="input"
                :color="theme.highlightColor"
                item-color="red"
                :items="activityLevels"
                label="Dog activity"
                v-model="dogActivity"
                item-text="name"
                item-value="key"
              />
            </v-form>
          </v-col>
          <v-col align="center">
            <v-row justify="center">
              <v-btn
                @click="$refs.files.click()"
                width="150"
                elevation="5"
                :color="theme.highlightColor"
                class="mr-3"
              >
                <span class="button-label">SELECT PHOTOS</span>
              </v-btn>
              <input
                multiple
                accept=".png,.jpg,.jpeg"
                v-show="false"
                ref="files"
                type="file"
                @change="handlePhotosUpload"
              />
            </v-row>
            <v-row justify="center">
              <span class="prompt mr-3 my-3"
                >Photos have to be in png/jpg format</span
              >
            </v-row>
            <v-row v-if="photos.length > 0" justify="center" class="my-5">
              <span class="fun-label">
                You have selected (
              </span>
              <span class="fun-label bold">
                {{ photos.length }}
              </span>
              <span class="fun-label">
                ) photos
              </span>
            </v-row>
            <v-row>
              <v-textarea
                v-model="shortDescription"
                class="my-5 mx-5"
                counter="100"
                background-color="amber lighten-4"
                color="orange orange-darken-4"
                label="Short description"
                no-resize
                outlined
              ></v-textarea>
            </v-row>
            <v-row>
              <v-textarea
                v-model="fullDescription"
                class="my-5 mx-5"
                counter="250"
                background-color="amber lighten-4"
                color="orange orange-darken-4"
                label="Full description"
                no-resize
                outlined
              ></v-textarea>
            </v-row>
            <v-row>
              <v-chip-group
                v-model="selectedAfflictions"
                column
                multiple
                dark
                :color="theme.highlightColor"
              >
                <v-chip
                  v-for="affliction in afflictions"
                  :key="affliction.id"
                  :value="affliction.id"
                >
                  {{ affliction.name }}
                </v-chip>
              </v-chip-group>
            </v-row>
          </v-col>
        </v-row>
      </div>
      <v-card-actions>
        <v-spacer />
        <v-btn
          @click="closeDialog"
          width="100"
          elevation="5"
          :color="theme.highlightColor"
          class="mr-3"
        >
          <span class="button-label">Cancel</span>
        </v-btn>
        <v-btn
          @click="addFeed"
          :disabled="!isValid"
          width="100"
          elevation="5"
          :color="theme.highlightColor"
          class="mr-3 my-2"
        >
          <span class="button-label">Add</span>
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import { mapActions, mapGetters } from "vuex";
import { DefaultTheme } from "@/shared/constants";
import { FormValidationMessages, AcceptedFileExtensions } from "./constants";
import { getFileExtension, prepareFormData } from "@/shared/helpers/file";

export default {
  name: "FeedAdditionDialog",
  data: () => ({
    theme: DefaultTheme,
    isValid: false,
    name: "",
    selectedBrand: null,
    selectedFeedType: null,
    weightRange: [20, 40],
    price: null,
    producerWebsite: "",
    dogActivity: null,
    shortDescription: "",
    fullDescription: "",
    nameRules: [
      v => !!v || FormValidationMessages.InvalidNameMessage,
      v => v.length <= 20 || FormValidationMessages.InvalidNameMessage
    ],
    requiredFieldRules: [
      v =>
        (v !== null && v !== undefined && v !== "") ||
        FormValidationMessages.RequiredFieldMessage
    ],
    photos: [],
    selectedAfflictions: []
  }),
  props: {
    dialogVisibility: {
      type: Boolean,
      default: false
    }
  },
  computed: {
    ...mapGetters("brandModule", ["brands"]),
    ...mapGetters("feedModule", ["feedTypes", "afflictions"]),
    ...mapGetters("petModule", ["activityLevels"]),
    rangeString() {
      return `${this.weightRange[0]}-${this.weightRange[1]}`;
    }
  },
  methods: {
    ...mapActions("brandModule", ["getBrands"]),
    ...mapActions("feedModule", ["getFeedTypes", "createFeed", "getFeed"]),
    ...mapActions("petModule", ["getDogActivityLevels"]),
    closeDialog() {
      this.$emit("closeDialog");
    },
    handlePhotosUpload() {
      const files = Array.from(this.$refs.files.files);
      this.photos = files.filter(file => {
        const isValid = AcceptedFileExtensions.includes(
          getFileExtension(file.name)
        );
        return isValid;
      });
    },
    async addFeed() {
      const formData = prepareFormData(this.photos);
      const createFeedRequest = {
        name: this.name,
        brandId: this.selectedBrand,
        feedType: this.selectedFeedType,
        minWeight: this.weightRange[0],
        maxWeight: this.weightRange[1],
        price: this.price,
        producerSiteLink: this.producerWebsite,
        shortDescription: this.shortDescription,
        fullDescription: this.fullDescription,
        dogActivityLevel: this.dogActivity,
        dogAfflictionsId: this.selectedAfflictions,
        form: formData
      };
      await this.createFeed(createFeedRequest);
      await this.getFeed();
      this.closeDialog();
    }
  },
  async mounted() {
    await this.getBrands();
    await this.getFeedTypes();
    await this.getDogActivityLevels();
  }
};
</script>

<style lang="scss" scoped>
.input-container {
  margin-top: 30px;
  margin-bottom: 60px;
}
.input {
  margin-left: 35px;
  margin-right: 35px;
}
</style>
