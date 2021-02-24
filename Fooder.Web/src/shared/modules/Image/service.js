import client from "@/shared/http-client";

const imageService = {
  async getImage(photoId) {
    const resource = `image/${photoId}`;
    const image = (await client.get(resource)).data;
    return image;
  }
};

export default imageService;
