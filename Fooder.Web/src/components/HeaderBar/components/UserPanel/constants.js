export const UserMenuActionsKeys = {
  PetManagement: 2,
  UserRanking: 3,
  Logout: 4,
  FeedAddition: 5,
  BrandAddition: 6
};

export const UserMenuActions = [
  {
    key: UserMenuActionsKeys.PetManagement,
    label: "Your pets",
    icon: "mdi-dog-side"
  },
  {
    key: UserMenuActionsKeys.UserRanking,
    label: "User rank",
    icon: "mdi-table-eye"
  },
  {
    key: UserMenuActionsKeys.FeedAddition,
    label: "Add feed",
    icon: "mdi-food-drumstick"
  },
  {
    key: UserMenuActionsKeys.BrandAddition,
    label: "Add brand",
    icon: "mdi-domain"
  },
  {
    key: UserMenuActionsKeys.Logout,
    label: "Logout",
    icon: "mdi-exit-to-app"
  }
];
