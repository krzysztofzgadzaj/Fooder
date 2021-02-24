import axios from "axios";
import store from "@/store";
import vm from "@/main";
import { DefaultErrorMessage, LocalStorageUserKey } from "./constants";

const api = axios.create({
  baseURL: process.env.VUE_APP_API_URL
});

api.interceptors.request.use(
  config => {
    store.commit("requestModule/incrementPendingRequestCount");
    return config;
  },
  error => Promise.reject(error)
);

api.interceptors.response.use(
  response => {
    store.commit("requestModule/decrementPendingRequestCount");
    return response;
  },
  error => {
    store.commit("requestModule/decrementPendingRequestCount");
    /* const isUnauthorized =
      ((error || {}).response || {}).status === 401 ||
      ((error || {}).response || {}).status === 403;
    if (isUnauthorized) {
      store.commit("userModule/signOut");
      router.push({
        name: "login"
      });
    } */
    // ToDo: handle error
    return Promise.reject(error);
  }
);

const client = {
  async get(resource, params, handleError = true) {
    const user = JSON.parse(localStorage.getItem(LocalStorageUserKey));
    const token = user ? user.token : null;
    const config = {
      params,
      headers: {
        Authorization: `Bearer ${token}`
      }
    };
    try {
      return await api.get(resource, config);
    } catch (ex) {
      if (handleError) {
        vm.$snotify.error(DefaultErrorMessage);
      } else {
        throw ex;
      }
    }
  },
  async post(resource, data, params, handleError = true) {
    const user = JSON.parse(localStorage.getItem(LocalStorageUserKey));
    const token = user ? user.token : null;
    const config = {
      params,
      headers: {
        Authorization: `Bearer ${token}`
      }
    };
    try {
      return await api.post(resource, data, config);
    } catch (ex) {
      if (handleError) {
        vm.$snotify.error(DefaultErrorMessage);
      } else {
        throw ex;
      }
    }
  },
  async put(resource, data, params, handleError = true) {
    const user = JSON.parse(localStorage.getItem(LocalStorageUserKey));
    const token = user ? user.token : null;
    const config = {
      params,
      headers: {
        Authorization: `Bearer ${token}`
      }
    };
    try {
      return await api.put(resource, data, config);
    } catch (ex) {
      if (handleError) {
        vm.$snotify.error(DefaultErrorMessage);
      } else {
        throw ex;
      }
    }
  },
  async delete(resource, params, handleError = true) {
    const user = JSON.parse(localStorage.getItem(LocalStorageUserKey));
    const token = user ? user.token : null;
    const config = {
      params,
      headers: {
        Authorization: `Bearer ${token}`
      }
    };
    try {
      const result = await api.delete(resource, config);
      return result;
    } catch (ex) {
      if (handleError) {
        vm.$snotify.error(DefaultErrorMessage);
      } else {
        throw ex;
      }
    }
  }
};

export default client;
