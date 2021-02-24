import service from "./service";
import imageService from "@/shared/modules/Image/service";

const state = {
  feed: [],
  filteredFeed: [],
  feedTypes: [],
  feedImages: [],
  feedReviews: [],
  afflictions: [],
  feedComments: [],
  userRanking: []
};

const getters = {
  feed: state => state.feed,
  filteredFeed: state => state.filteredFeed,
  feedTypes: state => state.feedTypes,
  feedImages: state => state.feedImages,
  feedReviews: state => state.feedReviews,
  afflictions: state => state.afflictions,
  feedComments: state => state.feedComments,
  userRanking: state => state.userRanking
};

const actions = {
  async getFeed({ commit }) {
    const feed = await service.getFeedAsync();
    commit("setFeed", feed);
    commit("setFilteredFeed", feed);
  },
  async getFeedTypes({ commit }) {
    const types = await service.getFeedTypesAsync();
    commit("setFeedTypes", types);
  },
  async createFeed(state, createFeedRequest) {
    await service.createFeed(createFeedRequest);
  },
  async getFeedImage({ commit }, request) {
    const image = await imageService.getImage(request.photoId);
    commit("addFeedImage", { feedId: request.feedId, image: image });
  },
  async getFeedReviews({ commit }, feedId) {
    const reviews = await service.getFeedReviews(feedId);
    commit("setFeedReviews", reviews);
  },
  async getAfflictions({ commit }) {
    const afflictions = await service.getAfflictionsAsync();
    commit("setAfflictions", afflictions);
  },
  async postReview({ commit }, data) {
    await service.postReview(data.feedId, data.review);
    const result = await service.getFeedReviews(data.feedId);
    commit("setFeedReviews", result);
  },
  async getFeedComments({ commit }, request) {
    const comments = await service.getFeedComments(request);
    commit("setFeedComments", comments);
  },
  async addFeedComment(state, request) {
    await service.addFeedComment(request);
  },
  async addCommentMark(state, request) {
    await service.addCommentMark(request);
  },
  async getUserRanking({ commit }) {
    const ranking = await service.getUserRanking();
    commit("setUserRanking", ranking);
  }
};

const mutations = {
  setFeed(state, feed) {
    state.feed = feed;
  },
  setFilteredFeed(state, feed) {
    state.filteredFeed = feed;
  },
  setFeedTypes(state, types) {
    state.feedTypes = types;
  },
  addFeedImage(state, image) {
    state.feedImages.push(image);
  },
  setFeedReviews(state, reviews) {
    state.feedReviews = reviews;
  },
  setAfflictions(state, afflictions) {
    state.afflictions = afflictions;
  },
  setFeedComments(state, comments) {
    state.feedComments = comments;
  },
  setUserRanking(state, ranking) {
    state.userRanking = ranking;
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
