<template>
  <v-dialog width="1150" persistent v-model="dialogVisibility">
    <v-card :color="theme.applicationBackgroundColor">
      <v-card-title class="fun-label">
        Your pets
      </v-card-title>
      <v-row style="width: 1150px;">
        <v-col cols="5">
          <pet-list />
        </v-col>
        <v-col cols="7">
          <pet-details v-if="selectedPet" />
        </v-col>
      </v-row>
      <v-row style="width: 1150px;"> </v-row>
      <v-card-actions>
        <v-btn
          @click="openPetCreationDialog"
          class="mx-2"
          fab
          :color="theme.highlightColor"
        >
          <v-icon color="white">
            mdi-plus
          </v-icon>
        </v-btn>
        <v-btn
          :disabled="!selectedPet"
          class="mx-2"
          fab
          :color="theme.highlightColor"
          @click="openPetDeletionDialog"
        >
          <v-icon color="white">
            mdi-delete
          </v-icon>
        </v-btn>
        <v-spacer />
        <v-btn
          @click="closeDialog"
          width="100"
          elevation="5"
          :color="theme.highlightColor"
          class="mr-3"
        >
          <span class="button-label">Close</span>
        </v-btn>
      </v-card-actions>
    </v-card>
    <pet-creation-dialog
      v-if="isPetCreationDialogVisible"
      :dialogVisibility="isPetCreationDialogVisible"
      @closeDialog="onPetCreationDialogClosed"
    />
    <confirmation-dialog
      v-if="isPetDeletionDialogVisible"
      :dialogVisibility="isPetDeletionDialogVisible"
      :title="deletionDialogTitle"
      :details="deletionDialogDetails"
      @confirmed="deleteSelectedPet"
      @denied="closePetDeletionDialog"
    />
  </v-dialog>
</template>

<script>
import { mapActions, mapGetters, mapMutations } from "vuex";
import { DefaultTheme } from "@/shared/constants";
import PetList from "./components/PetList";
import PetDetails from "./components/PetDetails";
import PetCreationDialog from "./components/PetCreationDialog";
import ConfirmationDialog from "@/shared/components/ConfirmationDialog";
import { PetDeletionDialogTitle, PetDeletionDialogDetails } from "./constants";

export default {
  name: "PetManagementDialog",
  components: {
    PetList,
    PetDetails,
    PetCreationDialog,
    ConfirmationDialog
  },
  data: () => ({
    theme: DefaultTheme,
    isPetCreationDialogVisible: false,
    isPetDeletionDialogVisible: false,
    deletionDialogTitle: PetDeletionDialogTitle,
    deletionDialogDetails: PetDeletionDialogDetails
  }),
  props: {
    dialogVisibility: {
      type: Boolean,
      default: false
    }
  },
  computed: {
    ...mapGetters("userModule", ["userInfo"]),
    ...mapGetters("petModule", ["selectedPet"])
  },
  methods: {
    ...mapActions("petModule", ["getUserPets", "deletePet"]),
    ...mapMutations("petModule", ["setSelectedPet"]),
    closeDialog() {
      this.$emit("closeDialog");
    },
    openPetCreationDialog() {
      this.isPetCreationDialogVisible = true;
    },
    openPetDeletionDialog() {
      this.isPetDeletionDialogVisible = true;
    },
    onPetCreationDialogClosed() {
      this.isPetCreationDialogVisible = false;
    },
    closePetDeletionDialog() {
      this.isPetDeletionDialogVisible = false;
    },
    async deleteSelectedPet() {
      this.closePetDeletionDialog();
      await this.deletePet(this.selectedPet.id);
      this.setSelectedPet(null);
      await this.getUserPets(this.userInfo.id);
    }
  },
  async mounted() {
    await this.getUserPets(this.userInfo.id);
  }
};
</script>

<style lang="scss" scoped>
.fixed-width {
  width: 200px;
}
</style>
