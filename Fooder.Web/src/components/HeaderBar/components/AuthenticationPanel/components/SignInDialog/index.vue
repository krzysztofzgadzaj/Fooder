<template>
  <v-dialog width="420" persistent v-model="dialogVisibility">
    <v-card :color="theme.applicationBackgroundColor">
      <v-card-title class="dialog-title">
        Sign in
      </v-card-title>
      <div class="input-container">
        <v-text-field
          v-model="login"
          :color="theme.highlightColor"
          label="Login"
          class="input"
        ></v-text-field>
        <v-text-field
          v-model="password"
          :color="theme.highlightColor"
          label="Password"
          type="password"
          class="input"
        ></v-text-field>
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
          @click="authenticate"
          width="100"
          class="mr-2 my-2"
          :disabled="isSignInButtonDisabled"
          elevation="5"
          :color="theme.highlightColor"
        >
          <span class="button-label">Sign in</span>
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import { DefaultTheme } from "@/shared/constants";
import { mapActions, mapGetters } from "vuex";

export default {
  name: "SignInDialog",
  data() {
    return {
      theme: DefaultTheme,
      login: null,
      password: null
    };
  },
  props: {
    dialogVisibility: {
      type: Boolean,
      default: false
    }
  },
  computed: {
    ...mapGetters("userModule", ["userInfo"]),
    isSignInButtonDisabled() {
      const isLoginEmpty = !this.login || this.login.length === 0;
      const isPasswordEmpty = !this.password || this.password.length === 0;
      return isLoginEmpty || isPasswordEmpty;
    }
  },
  methods: {
    ...mapActions("userModule", ["signIn"]),
    ...mapActions("feedModule", ["getFeed"]),
    ...mapActions("petModule", ["getUserPets"]),
    closeDialog() {
      this.$emit("closeDialog");
    },
    async authenticate() {
      const credentials = {
        login: this.login,
        password: this.password
      };
      await this.signIn(credentials);
      await this.getFeed();
      await this.getUserPets(this.userInfo.id);
      this.closeDialog();
    }
  }
};
</script>
