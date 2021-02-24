<template>
  <v-dialog width="420" persistent v-model="dialogVisibility">
    <v-card :color="theme.applicationBackgroundColor">
      <v-card-title class="dialog-title">
        Sign up
      </v-card-title>
      <div class="input-container">
        <v-form v-model="isValid">
          <v-text-field
            :counter="10"
            :rules="usernameRules"
            v-model="login"
            :color="theme.highlightColor"
            label="Login"
            class="input"
          ></v-text-field>
          <v-text-field
            :rules="passwordRules"
            v-model="password"
            :color="theme.highlightColor"
            label="Password"
            type="password"
            class="input"
          ></v-text-field>
          <v-text-field
            :rules="mailRules"
            v-model="mailAddress"
            :color="theme.highlightColor"
            label="Mail address"
            class="input"
          ></v-text-field>
        </v-form>
      </div>
      <v-card-actions>
        <v-spacer />
        <v-btn
          @click="closeDialog"
          width="100"
          elevation="5"
          :color="theme.highlightColor"
        >
          <span class="button-label">Cancel</span>
        </v-btn>
        <v-btn
          @click="register"
          width="100"
          class="mr-2 my-2"
          :disabled="!isValid"
          elevation="5"
          :color="theme.highlightColor"
        >
          <span class="button-label">Sign up</span>
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import { DefaultTheme } from "@/shared/constants";
import { mapActions } from "vuex";
import { FormValidationMessages } from "./constants";

export default {
  name: "SignUpDialog",
  data() {
    return {
      isValid: false,
      theme: DefaultTheme,
      login: "",
      password: "",
      mailAddress: "",
      usernameRules: [
        v => !!v || FormValidationMessages.InvalidUsernameMessage,
        v => v.length <= 10 || FormValidationMessages.InvalidUsernameMessage
      ],
      passwordRules: [
        v => !!v || FormValidationMessages.InvalidPasswordMessage,
        v =>
          /^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$/.test(v) ||
          FormValidationMessages.InvalidPasswordMessage
      ],
      mailRules: [
        v => !!v || FormValidationMessages.InvalidMailAddressMessage,
        v => /.+@.+/.test(v) || FormValidationMessages.InvalidMailAddressMessage
      ]
    };
  },
  props: {
    dialogVisibility: {
      type: Boolean,
      default: false
    }
  },
  methods: {
    ...mapActions("userModule", ["signUp"]),
    closeDialog() {
      this.$emit("closeDialog");
    },
    async register() {
      const credentials = {
        login: this.login,
        password: this.password,
        mailAddress: this.mailAddress
      };
      await this.signUp(credentials);
      this.closeDialog();
    }
  }
};
</script>
