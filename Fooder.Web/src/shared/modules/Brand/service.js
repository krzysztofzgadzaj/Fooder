/* eslint-disable prettier/prettier */
import client from "@/shared/http-client";
import { BrandDto } from "./dto";

const service = {
  async addBrand(brand) {
    const resource = "brand";
    await client.post(resource, brand);
  },
  async getBrands() {
    const resource = "brand";
    const response = await client.get(resource);
    const brands = response.data.map(brand => new BrandDto(brand));
    return brands;
  }
};

export default service;
