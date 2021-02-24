<template>
  <v-dialog width="800" persistent v-model="dialogVisibility">
    <v-card :color="theme.applicationBackgroundColor">
      <v-card-title class="fun-label">
        Add pet
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
              <v-menu
                ref="menu"
                v-model="menuVisibility"
                :close-on-content-click="false"
                transition="scale-transition"
                offset-y
                min-width="auto"
              >
                <template v-slot:activator="{ on, attrs }">
                  <v-text-field
                    :color="theme.highlightColor"
                    class="input"
                    v-model="date"
                    label="Date of birth"
                    prepend-icon="mdi-calendar"
                    readonly
                    v-bind="attrs"
                    v-on="on"
                  ></v-text-field>
                </template>
                <v-date-picker
                  :color="theme.highlightColor"
                  ref="picker"
                  v-model="date"
                  :max="new Date().toISOString().substr(0, 10)"
                  min="2000-01-01"
                  @change="saveDate"
                ></v-date-picker>
              </v-menu>
              <v-select
                :rules="activityRules"
                class="input"
                :color="theme.highlightColor"
                item-color="red"
                :items="dogActivityLevels"
                label="Activity level"
                v-model="selectedActivityLevel"
                item-text="name"
                item-value="key"
              />
            </v-form>
            <v-text-field
              :rules="bodyWeightRules"
              v-model="bodyWeight"
              :color="theme.highlightColor"
              label="Body weight (optional)"
              type="number"
              step="0.01"
              class="input"
            ></v-text-field>
            <v-text-field
              :rules="heightRules"
              v-model="height"
              :color="theme.highlightColor"
              label="Height in centimetres (optional)"
              type="number"
              step="0.01"
              class="input"
            ></v-text-field>
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
          @click="createPet()"
          :disabled="isSavingDisabled"
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
import { mapGetters, mapActions } from "vuex";
import { DefaultTheme } from "@/shared/constants";
import {
  DogActivityLevels,
  FormValidationMessages,
  AcceptedFileExtensions
} from "./constants";
import { getFileExtension, prepareFormData } from "@/shared/helpers/file";

export default {
  name: "PetCreationDialog",
  data: () => ({
    theme: DefaultTheme,
    isValid: false,
    name: "",
    bodyWeight: null,
    height: null,
    date: null,
    menuVisibility: false,
    dogActivityLevels: DogActivityLevels,
    selectedActivityLevel: null,
    nameRules: [
      v => !!v || FormValidationMessages.InvalidNameMessage,
      v => v.length <= 20 || FormValidationMessages.InvalidNameMessage
    ],
    bodyWeightRules: [
      v => v > 0 || FormValidationMessages.InvalidBodyWeightMessage,
      v => v < 100 || FormValidationMessages.InvalidBodyWeightMessage
    ],
    heightRules: [
      v => v > 0 || FormValidationMessages.InvalidHeightMessage,
      v => v < 100 || FormValidationMessages.InvalidHeightMessage
    ],
    activityRules: [
      v => !!v || FormValidationMessages.InvalidActivityLevelMessage
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
  watch: {
    menuVisibility(val) {
      val && setTimeout(() => (this.$refs.picker.activePicker = "YEAR"));
    }
  },
  computed: {
    ...mapGetters("userModule", ["userInfo"]),
    ...mapGetters("feedModule", ["afflictions"]),
    isSavingDisabled() {
      const isDisabled = !this.isValid || !this.date;
      return isDisabled;
    }
  },
  methods: {
    ...mapActions("petModule", ["addPet", "getUserPets"]),
    closeDialog() {
      this.$emit("closeDialog");
    },
    saveDate(date) {
      this.$refs.menu.save(date);
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
    async createPet() {
      const formData = prepareFormData(this.photos);
      const createPetRequest = {
        name: this.name,
        dateOfBirth: this.date,
        activityLevel: this.selectedActivityLevel,
        ownerId: this.userInfo.id,
        bodyWeight: this.bodyWeight,
        heightInCentimetres: this.height,
        petAfflictionsId: this.selectedAfflictions,
        form: formData
      };
      await this.addPet(createPetRequest);
      await this.getUserPets(this.userInfo.id);
      this.closeDialog();
    }
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
