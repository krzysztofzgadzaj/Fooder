/* eslint-disable prettier/prettier */
import client from "@/shared/http-client";
import {DogActivityLevel, PetDto} from "./dto";

const service = {
  async getUserPets(ownerId) {
    const resource = `user/${ownerId}/pets`;
    const response = await client.get(resource);
    return response.data.map(pet => new PetDto(pet));
  },
  async addPet(request) {
    let resource = `pet?name=${request.name}&dateOfBirth=${request.dateOfBirth}`
      + `&activityLevel=${request.activityLevel}&ownerId=${request.ownerId}`;
    if (request.bodyWeight) {
      resource = resource.concat(`&bodyWeight=${request.bodyWeight}`);
    }
    if (request.heightInCentimetres) {
      resource = resource.concat(`&heightInCentimetres=${request.heightInCentimetres}`);
    }
    request.petAfflictionsId.forEach(x => {
      resource += `&petAfflictionsId=${x}`;
    });
    await client.post(resource, request.form);
  },
  async deletePet(petId) {
    const resource = `pet/${petId}`;
    await client.delete(resource);
  },
  async getDogActivityLevels() {
    const resource = "pet/activity-levels";
    const activityLevels = (await client.get(resource)).data.map(level => new DogActivityLevel(level));
    return activityLevels;
  }
};

export default service;
