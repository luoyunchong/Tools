import axios, { AxiosResponse, AxiosRequestConfig } from "axios";
import { useMessage } from "naive-ui";

const service = axios.create({
  baseURL: import.meta.env.VITE_APP_URL,
});



// Request interceptors
service.interceptors.request.use(
  (config: AxiosRequestConfig) => {
    // do something
    return config;
  },
  (error: any) => {
    Promise.reject(error);
  }
);

// Response interceptors
service.interceptors.response.use(
  async (response: AxiosResponse) => {
    // do something
    return Promise.resolve(response.data);
  },
  (error: any) => {
    if (error.response.status == 500) {
      window.$message?.error(error.response.data.developerMessage.Message);
    }
    // do something
    return Promise.reject(error);
  }
);

export default service;
