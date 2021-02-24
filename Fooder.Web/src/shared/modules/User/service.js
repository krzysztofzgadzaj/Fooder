import client from "@/shared/http-client";
import { UserDto } from "./dto";
import {
  UnauthorizedMessage,
  AuthenticatedMessage,
  UserCreatedMessage
} from "./constants";
import vm from "@/main";

const service = {
  async signIn(credentials) {
    let user = null;
    try {
      const resource = "authentication";
      const result = await client.post(resource, credentials, null, false);
      user = result.data ? new UserDto(result.data) : null;
      vm.$snotify.success(AuthenticatedMessage);
    } catch (ex) {
      vm.$snotify.error(UnauthorizedMessage);
    }
    return user;
  },
  async signUp(credentials) {
    const resource = "user";
    const response = await client.post(resource, credentials);
    const isCreated =
      response && (response.status === 200 || response.status === 201);
    if (isCreated) {
      vm.$snotify.success(UserCreatedMessage);
    }
  },
  async authorize(permissionCode) {
    const resource = `authorization?permissionCode=${permissionCode}`;
    const isAuthorized = (await client.get(resource)).data.isAuthorized;
    return isAuthorized;
  }
};

export default service;
