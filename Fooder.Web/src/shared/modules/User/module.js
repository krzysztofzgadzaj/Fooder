import userService from "./service";
import { LocalStorageUserKey } from "@/shared/constants";
import { Permissions } from "@/shared/constants";

const state = {
  userInfo: null,
  isFeedAdditionAuthorized: false,
  isReviewAdditionAuthorized: false,
  isBrandAdditionAuthorized: false
};

const actions = {
  async signIn({ commit }, credentials) {
    const userInfo = await userService.signIn(credentials);
    commit("setAuthenticationResults", userInfo);
  },
  async signUp(state, credentials) {
    await userService.signUp(credentials);
  },
  async authorize({ commit }, permissionCode) {
    const isAuthorized = await userService.authorize(permissionCode);
    commit("setAuthorizationResult", {
      isAuthorized: isAuthorized,
      permissionCode: permissionCode
    });
  },
  synchronizeUserInfo({ commit }) {
    const userInfo = JSON.parse(localStorage.getItem(LocalStorageUserKey));
    if (userInfo) {
      commit("setAuthenticationResults", userInfo);
    }
  },
  logout({ commit }) {
    commit("disposeAuthenticationResults");
  }
};

const mutations = {
  setAuthenticationResults(state, userInfo) {
    state.userInfo = userInfo;
    const serializedUserInfo = JSON.stringify(userInfo);
    localStorage.setItem(LocalStorageUserKey, serializedUserInfo);
  },
  setAuthorizationResult(state, authorizationResult) {
    switch (authorizationResult.permissionCode) {
      case Permissions.AddFeedPermissionCode:
        state.isFeedAdditionAuthorized = authorizationResult.isAuthorized;
        break;
      case Permissions.AddReviewPermissionCode:
        state.isReviewAdditionAuthorized = authorizationResult.isAuthorized;
        break;
      case Permissions.AddBrandPermissionCode:
        state.isBrandAdditionAuthorized = authorizationResult.isAuthorized;
        break;
    }
  },
  disposeAuthenticationResults(state) {
    state.userInfo = null;
    localStorage.removeItem(LocalStorageUserKey);
  }
};

const getters = {
  userInfo: state => state.userInfo,
  isFeedAdditionAuthorized: state => state.isFeedAdditionAuthorized,
  isReviewAdditionAuthorized: state => state.isReviewAdditionAuthorized,
  isBrandAdditionAuthorized: state => state.isBrandAdditionAuthorized
};

const module = {
  namespaced: true,
  state,
  actions,
  mutations,
  getters
};

export default module;
