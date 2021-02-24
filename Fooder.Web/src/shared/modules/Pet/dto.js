export class PetDto {
  constructor(data = {}) {
    this.id = data.id;
    this.uniqueId = data.uniqueId;
    this.name = data.name;
    this.dateOfBirth = data.dateOfBirth;
    this.activityLevel = data.activityLevel;
    this.ownerId = data.ownerId;
    this.bodyWeight = data.bodyWeight;
    this.heightInCentimetres = data.heightInCentimetres;
    this.photosIds = data.photosIds;
    this.petAfflictions = data.petAfflictions;
  }
}

export class DogActivityLevel {
  constructor(data = {}) {
    this.name = data.name;
    this.key = data.key;
  }
}
