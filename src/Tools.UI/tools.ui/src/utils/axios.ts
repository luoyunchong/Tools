import axios, { AxiosResponse, AxiosRequestConfig } from 'axios';

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
        // do something
        return Promise.reject(error);
    }
);

export default service;