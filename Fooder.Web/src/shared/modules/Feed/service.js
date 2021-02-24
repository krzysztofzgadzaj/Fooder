import {
  AfflictionDto,
  CommentDto,
  FeedDto,
  FeedTypeDto,
  RankingPositionDto,
  ReviewDto
} from "./dto";
import client from "@/shared/http-client";
import { LocalStorageUserKey } from "@/shared/constants";

const service = {
  async getFeedAsync() {
    let resource = "/Feed";
    const user = JSON.parse(localStorage.getItem(LocalStorageUserKey));
    if (user) {
      resource += `?userName=${user.login}`;
    }
    const response = await client.get(resource);
    return response.data.map(data => new FeedDto(data));
  },
  async getFeedTypesAsync() {
    const resource = "/Feed/types";
    const response = await client.get(resource);
    return response.data.map(data => new FeedTypeDto(data));
  },
  async createFeed(request) {
    let resource =
      `feed?name=${request.name}&brandId=${request.brandId}` +
      `&dogActivityLevel=${request.dogActivityLevel}&feedType=${request.feedType}` +
      `&minWeight=${request.minWeight}&maxWeight=${request.maxWeight}&price=${request.price}` +
      `&producerSiteLink=${request.producerSiteLink}&shortDescription=${request.shortDescription}` +
      `&fullDescription=${request.fullDescription}`;

    request.dogAfflictionsId.forEach(x => {
      resource += `&dogAfflictionsId=${x}`;
    });

    await client.post(resource, request.form);
  },
  async getFeedReviews(feedId) {
    const resource = `feed/${feedId}/reviews`;
    const reviews = (await client.get(resource)).data.map(
      review => new ReviewDto(review)
    );
    return reviews;
  },
  async getAfflictionsAsync() {
    const resource = "affliction";
    const response = await client.get(resource);
    return response.data.map(x => new AfflictionDto(x));
  },
  async postReview(feedId, review) {
    let resource = `feed/reviews?content=${review}&feedId=${feedId}`;
    const user = JSON.parse(localStorage.getItem(LocalStorageUserKey));
    if (user) {
      resource += `&authorId=${user.id}`;
    }
    await client.post(resource);
  },
  async getFeedComments(request) {
    const resource = `feed/${request.feedId}/comments?userName=${request.login}`;
    const comments = (await client.get(resource)).data.map(
      comment => new CommentDto(comment)
    );
    return comments;
  },
  async addFeedComment(request) {
    const resource = `feed/${request.feedId}/comments`;
    await client.post(resource, request);
  },
  async addCommentMark(request) {
    const resource = `feed/${request.feedId}/commentMarks`;
    await client.post(resource, request);
  },
  async getUserRanking() {
    const resource = "feed/ranking";
    const ranking = (await client.get(resource)).data.map(
      position => new RankingPositionDto(position)
    );
    return ranking;
  }
};

export default service;
