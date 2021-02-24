import service from "./service";
import imageService from "@/shared/modules/Image/service";

const state = {
  userPets: [],
  selectedPet: null,
  petImages: [],
  activityLevels: []
};

const getters = {
  userPets: state => state.userPets,
  selectedPet: state => state.selectedPet,
  petImages: state => state.petImages,
  activityLevels: state => state.activityLevels
};

const actions = {
  async getUserPets({ commit }, ownerId) {
    const pets = await service.getUserPets(ownerId);
    commit("setUserPets", pets);
  },
  async addPet(state, request) {
    await service.addPet(request);
  },
  async getPetImage({ commit }, photoId) {
    const image = await imageService.getImage(photoId);
    commit("addPetImage", image);
  },
  async deletePet(state, petId) {
    await service.deletePet(petId);
  },
  async getDogActivityLevels({ commit }) {
    const activityLevels = await service.getDogActivityLevels();
    commit("setDogActivityLevels", activityLevels);
  }
};

const mutations = {
  setUserPets(state, pets) {
    state.userPets = pets;
  },
  setSelectedPet(state, pet) {
    state.selectedPet = pet;
  },
  addPetImage(state, image) {
    state.petImages.push(image);
  },
  clearPetImages(state) {
    state.petImages = [];
  },
  setDogActivityLevels(state, activityLevels) {
    state.activityLevels = activityLevels;
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
