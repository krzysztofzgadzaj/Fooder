<template>
  <v-list class="pet-list">
    <v-list-item-group v-model="selectedPet" :color="theme.highlightColor">
      <v-list-item
        v-for="(pet, index) in userPets"
        v-bind:class="{ divided: index < userPets.length - 1 }"
        :key="pet.id"
      >
        <v-list-item-content>
          <v-list-item-title
            v-text="pet.name"
            class="fun-label"
          ></v-list-item-title>
        </v-list-item-content>
      </v-list-item>
    </v-list-item-group>
  </v-list>
</template>

<script>
import { mapGetters, mapMutations, mapActions } from "vuex";
import { DefaultTheme } from "@/shared/constants";

export default {
  name: "PetList",
  data: () => ({
    selectedPet: null,
    theme: DefaultTheme
  }),
  watch: {
    async selectedPet(newValue) {
      const isDefined = typeof newValue !== "undefined" && newValue !== null;
      if (isDefined) {
        const pet = this.userPets[newValue];
        this.setSelectedPet(pet);
        await this.updateImages(pet);
      }
    }
  },
  computed: {
    ...mapGetters("petModule", ["userPets"])
  },
  methods: {
    ...mapActions("petModule", ["getPetImage"]),
    ...mapMutations("petModule", ["setSelectedPet", "clearPetImages"]),
    async updateImages(pet) {
      this.clearPetImages();
      const photoStreamPromiseCollection = pet.photosIds.map(photoId =>
        this.getPetImage(photoId)
      );
      await Promise.all(photoStreamPromiseCollection);
    }
  }
};
</script>

<style lang="scss" scoped>
@import "@/assets/styles/_colors.scss";
.pet-list {
  height: 300px;
  overflow-y: auto;
  margin: 20px;
  background: $pale-pink;
}
</style>
