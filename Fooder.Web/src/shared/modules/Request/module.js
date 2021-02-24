const state = {
  pendingRequestCount: 0
};

const mutations = {
  incrementPendingRequestCount(state) {
    state.pendingRequestCount++;
  },
  decrementPendingRequestCount(state) {
    state.pendingRequestCount--;
  }
};

const getters = {
  anyPendingRequests: state => state.pendingRequestCount > 0
};

const module = {
  namespaced: true,
  state,
  mutations,
  getters
};

export default module;
