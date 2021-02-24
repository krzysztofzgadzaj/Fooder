export class FeedDto {
  constructor(data) {
    this.id = data.id;
    this.name = data.name;
    this.brandName = data.brandName;
    this.userMark = data.userMark;
    this.feedType = data.feedType;
    this.price = data.price;
    this.averageRating = data.averageRating;
    this.ratingCount = data.ratingCount;
    this.minWeight = data.minWeight;
    this.maxWeight = data.maxWeight;
    this.shortDescription = data.shortDescription;
    this.fullDescription = data.fullDescription;
    this.producerSiteLink = data.producerSiteLink;
    this.dogActivityLevel = data.dogActivityLevel;
    this.dogAfflictions = data.dogAfflictions;
    this.photosIds = data.photosIds;
  }
}

export class FeedTypeDto {
  constructor(data = {}) {
    this.key = data.key;
    this.name = data.name;
  }
}

export class ReviewDto {
  constructor(data = {}) {
    this.id = data.id;
    this.author = data.author;
    this.content = data.content;
  }
}

export class AfflictionDto {
  constructor(data = {}) {
    this.id = data.id;
    this.name = data.name;
  }
}

export class CommentDto {
  constructor(data = {}) {
    this.id = data.id;
    this.content = data.content;
    this.commentMark = data.commentMark;
    this.authorName = data.authorName;
    this.level = data.level;
    this.userMark = data.userMark;
    this.relatedComments = data.relatedComments;
  }
}

export class RankingPositionDto {
  constructor(data = {}) {
    this.userName = data.userName;
    this.points = data.points;
  }
}
