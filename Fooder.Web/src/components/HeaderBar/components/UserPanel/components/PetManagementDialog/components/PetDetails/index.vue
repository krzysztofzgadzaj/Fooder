<template>
  <div class="container">
    <v-row>
      <v-col>
        <v-row>
          <span class="fun-label">Name: {{ selectedPet.name }}</span>
        </v-row>
        <v-row>
          <span class="fun-label"
            >Date of birth: {{ selectedPet.dateOfBirth }}</span
          >
        </v-row>
        <v-row>
          <span class="fun-label"
            >Activity level: {{ selectedPet.activityLevel }}</span
          >
        </v-row>
        <v-row v-if="selectedPet.bodyWeight">
          <span class="fun-label"
            >Body weight: {{ selectedPet.bodyWeight }}</span
          >
        </v-row>
        <v-row v-if="selectedPet.heightInCentimetres">
          <span class="fun-label"
            >Height: {{ selectedPet.heightInCentimetres }}</span
          >
        </v-row>
        <v-row v-if="selectedPet.petAfflictions[0]">
          <v-col>
            <span class="fun-label">Afflictions</span>
            <v-chip-group column multiple dark readonly>
              <v-chip
                v-for="affliction in selectedPet.petAfflictions"
                :key="affliction"
                :color="theme.highlightColor"
                disabled
              >
                {{ affliction }}
              </v-chip>
            </v-chip-group>
          </v-col>
        </v-row>
      </v-col>
      <v-col>
        <v-carousel
          v-if="petImages.length > 0"
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
            v-for="(photo, index) in petImages"
            :key="index"
            :src="photo.content"
          ></v-carousel-item>
        </v-carousel>
        <span v-else class="fun-label">This pet has no photos :(</span>
      </v-col>
    </v-row>
  </div>
</template>

<script>
import { mapGetters } from "vuex";
import { DefaultTheme } from "@/shared/constants";

export default {
  name: "PetDetails",
  data: () => ({
    theme: DefaultTheme
  }),
  computed: {
    ...mapGetters("petModule", ["selectedPet", "petImages"]),
    ...mapGetters("feedModule", ["afflictions"])
  }
};
</script>

<style lang="scss" scoped>
.container {
  margin-top: 20px;
}
.carousel {
  max-width: 300px;
}
</style>
