import service from "./service";

const state = {
  brands: []
};

const getters = {
  brands: state => state.brands
};

const actions = {
  async addBrand(state, brand) {
    await service.addBrand(brand);
  },
  async getBrands({ commit }) {
    const brands = await service.getBrands();
    commit("setBrands", brands);
  }
};

const mutations = {
  setBrands(state, brands) {
    state.brands = brands;
  }
};

const module = {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
};

export default module;
