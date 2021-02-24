<template>
  <v-dialog width="700" persistent v-model="dialogVisibility">
    <v-card :color="theme.applicationBackgroundColor">
      <v-card-title class="fun-label">
        Add brand
      </v-card-title>
      <div>
        <v-row style="width: 715px;" align="center" justify="center">
          <v-col>
            <v-form v-model="isValid">
              <v-text-field
                :counter="20"
                :rules="[
                  isBrandNameUniqueRule,
                  isBrandNameNotEmptyRule,
                  isBrandNameCorrectlyLongRule
                ]"
                v-model="name"
                :color="theme.highlightColor"
                label="Name"
                class="input"
              ></v-text-field>
            </v-form>
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
          @click="createBrand"
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
import { FormValidationMessages } from "./constants";

export default {
  name: "BrandAdditionDialog",
  data: () => ({
    theme: DefaultTheme,
    isValid: false,
    name: ""
  }),
  props: {
    dialogVisibility: {
      type: Boolean,
      default: false
    }
  },
  computed: {
    ...mapGetters("brandModule", ["brands"])
  },
  methods: {
    ...mapActions("brandModule", ["getBrands", "addBrand"]),
    closeDialog() {
      this.$emit("closeDialog");
    },
    async createBrand() {
      await this.addBrand({
        name: this.name
      });
      this.closeDialog();
    },
    isBrandNameUniqueRule() {
      const isUnique =
        this.brands.filter(brand => brand.name === this.name).length === 0;
      return isUnique ? true : FormValidationMessages.InvalidNameMessage;
    },
    isBrandNameNotEmptyRule() {
      return this.name ? true : FormValidationMessages.InvalidNameMessage;
    },
    isBrandNameCorrectlyLongRule() {
      return this.name.length <= 20
        ? true
        : FormValidationMessages.InvalidNameMessage;
    }
  },
  async mounted() {
    await this.getBrands();
  }
};
</script>

<style lang="scss" scoped>
.input {
  margin-left: 35px;
  margin-right: 35px;
}
</style>
