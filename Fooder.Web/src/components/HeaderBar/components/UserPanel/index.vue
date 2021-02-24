<template>
  <div>
    <v-menu top close-on-click>
      <template v-slot:activator="{ on }">
        <v-btn
          elevation="7"
          :color="theme.highlightColor"
          class="mr-1"
          v-on="on"
        >
          <v-icon :color="theme.iconColor">
            mdi-cog-outline
          </v-icon>
        </v-btn>
      </template>
      <v-list>
        <v-list-item
          v-for="item in menuActions"
          :key="item.key"
          @click="onItemSelected(item.key)"
        >
          <v-icon>{{ item.icon }}</v-icon>
          <v-list-item-title
            v-text="item.label"
            class="mx-2"
          ></v-list-item-title>
        </v-list-item>
      </v-list>
    </v-menu>
    <pet-management-dialog
      v-if="isPetManagementDialogVisible"
      :dialogVisibility="isPetManagementDialogVisible"
      @closeDialog="closePetManagementDialog"
    />
    <feed-addition-dialog
      v-if="isFeedAdditionDialogVisible"
      :dialogVisibility="isFeedAdditionDialogVisible"
      @closeDialog="closeFeedAdditionDialog"
    />
    <brand-addition-dialog
      v-if="isBrandAdditionDialogVisible"
      :dialogVisibility="isBrandAdditionDialogVisible"
      @closeDialog="closeBrandAdditionDialog"
    />
    <user-ranking
      v-if="isUserRankingDialogVisible"
      :dialogVisibility="isUserRankingDialogVisible"
      @closeDialog="closeUserRankingDialog"
    />
  </div>
</template>

<script>
import { DefaultTheme } from "@/shared/constants";
import { UserMenuActions, UserMenuActionsKeys } from "./constants";
import { mapActions, mapGetters, mapMutations } from "vuex";
import PetManagementDialog from "./components/PetManagementDialog";
import FeedAdditionDialog from "./components/FeedAdditionDialog";
import BrandAdditionDialog from "./components/BrandAdditionDialog";
import UserRanking from "./components/UserRanking";
import { Permissions } from "@/shared/constants";

export default {
  name: "UserPanel",
  data() {
    return {
      theme: DefaultTheme,
      isPetManagementDialogVisible: false,
      isFeedAdditionDialogVisible: false,
      isBrandAdditionDialogVisible: false,
      isUserRankingDialogVisible: false
    };
  },
  components: {
    PetManagementDialog,
    FeedAdditionDialog,
    BrandAdditionDialog,
    UserRanking
  },
  computed: {
    ...mapGetters("userModule", [
      "isFeedAdditionAuthorized",
      "isBrandAdditionAuthorized"
    ]),
    menuActions() {
      let permittedActions = this.isFeedAdditionAuthorized
        ? UserMenuActions
        : UserMenuActions.filter(
            action => action.key !== UserMenuActionsKeys.FeedAddition
          );
      permittedActions = this.isBrandAdditionAuthorized
        ? permittedActions
        : permittedActions.filter(
            action => action.key !== UserMenuActionsKeys.BrandAddition
          );
      return permittedActions;
    }
  },
  methods: {
    ...mapActions("userModule", ["logout", "authorize"]),
    ...mapActions("feedModule", ["getFeed"]),
    ...mapMutations("petModule", ["setUserPets"]),
    async onItemSelected(key) {
      switch (key) {
        case UserMenuActionsKeys.PetManagement:
          this.openPetManagementDialog();
          break;
        case UserMenuActionsKeys.UserRanking:
          this.openUserRankingDialog();
          break;
        case UserMenuActionsKeys.FeedAddition:
          this.openFeedAdditionDialog();
          break;
        case UserMenuActionsKeys.BrandAddition:
          this.openBrandAdditionDialog();
          break;
        case UserMenuActionsKeys.Logout:
          this.logout();
          await this.getFeed();
          this.setUserPets([]);
          break;
      }
    },
    openPetManagementDialog() {
      this.isPetManagementDialogVisible = true;
    },
    closePetManagementDialog() {
      this.isPetManagementDialogVisible = false;
    },
    openFeedAdditionDialog() {
      this.isFeedAdditionDialogVisible = true;
    },
    closeFeedAdditionDialog() {
      this.isFeedAdditionDialogVisible = false;
    },
    openBrandAdditionDialog() {
      this.isBrandAdditionDialogVisible = true;
    },
    closeBrandAdditionDialog() {
      this.isBrandAdditionDialogVisible = false;
    },
    openUserRankingDialog() {
      this.isUserRankingDialogVisible = true;
    },
    closeUserRankingDialog() {
      this.isUserRankingDialogVisible = false;
    }
  },
  async mounted() {
    await this.authorize(Permissions.AddFeedPermissionCode);
    await this.authorize(Permissions.AddBrandPermissionCode);
  }
};
</script>
