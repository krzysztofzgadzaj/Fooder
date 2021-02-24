<template>
  <div class="button-container">
    <v-btn
      elevation="7"
      :color="theme.highlightColor"
      class="mx-2"
      @click="showSignInDialog"
    >
      <span class="button-label">Sign in</span>
    </v-btn>
    <v-btn
      elevation="7"
      :color="theme.highlightColor"
      class="mx-2"
      @click="showSignUpDialog"
    >
      <span class="button-label">Sign up</span>
    </v-btn>
    <sign-in-dialog
      :dialogVisibility="isSignInDialogVisible"
      @closeDialog="closeSignInDialog"
    />
    <sign-up-dialog
      :dialogVisibility="isSignUpDialogVisible"
      @closeDialog="closeSignUpDialog"
    />
  </div>
</template>

<script>
import { DefaultTheme } from "@/shared/constants";
import SignInDialog from "./components/SignInDialog";
import SignUpDialog from "./components/SignUpDialog";

export default {
  name: "AuthenticationPanel",
  data() {
    return {
      theme: DefaultTheme,
      isSignInDialogVisible: false,
      isSignUpDialogVisible: false
    };
  },
  components: {
    SignInDialog,
    SignUpDialog
  },
  methods: {
    showSignInDialog() {
      this.isSignInDialogVisible = true;
    },
    showSignUpDialog() {
      this.isSignUpDialogVisible = true;
    },
    closeSignInDialog() {
      this.isSignInDialogVisible = false;
    },
    closeSignUpDialog() {
      this.isSignUpDialogVisible = false;
    }
  }
};
</script>

<style lang="scss">
@import "@/assets/styles/_colors.scss";

.input-container {
  margin-top: 30px;
  margin-bottom: 60px;
}
.input {
  margin-left: 35px;
  margin-right: 35px;
}
.button-container {
  float: left;
}
</style>
