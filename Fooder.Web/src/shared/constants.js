import colors from "@/assets/styles/_colors.scss";

export const DefaultTheme = {
  mainColor: colors.lightPink,
  highlightColor: colors.darkPink,
  applicationBackgroundColor: colors.palePink,
  backgroundSecondaryColor: colors.cream,
  commonBackgroundColor: colors.sensualPink,
  iconColor: colors.white
};

export const DefaultErrorMessage = "Something went wrong! Try again later.";

export const LocalStorageUserKey = "fooderUser";

export const Permissions = {
  AddFeedPermissionCode: "Fooder_Add_Feed",
  AddReviewPermissionCode: "Fooder_Add_Review",
  AddBrandPermissionCode: "Fooder_Add_Brand"
};
