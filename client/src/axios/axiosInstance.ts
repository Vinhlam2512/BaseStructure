import axios from 'axios';
import Cookies from 'js-cookie';
import AuthService from '../services/auth.service';

const axiosInstance = axios.create({
  baseURL: process.env.NEXT_PUBLIC_BASE_URL_API,
  headers: {
    'Content-Type': 'application/json'
  }
});

//set Authorization when logged in
axiosInstance.interceptors.request.use(function (config) {
  const token = Cookies.get('accessToken');
  if (token === undefined) {
    config.headers.Authorization = 'Bearer';
  } else {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

// Add a response interceptor
axiosInstance.interceptors.response.use(
  async function (response) {
    return response;
  },
  async function (error) {
    const prevRequest = error.config;

    if (error?.response?.status === 401 && !prevRequest.sent) {
      // Call the refresh token function from AuthService
      try {
        const tokenRefresh: Token = await AuthService.RefreshToken();
        if (tokenRefresh) {
          prevRequest.headers[
            'Authorization'
          ] = `Bearer ${tokenRefresh.accessToken}`;
          return axiosInstance(prevRequest);
        }
      } catch (refreshError) {
        const returnUrl = encodeURIComponent(window.location.pathname);
        window.location.href = `/login?returnUrl=${returnUrl}`;
      }
    }
    if (error?.response?.status === 403 && !prevRequest.sent) {
      window.location.href = '/access-denied';
    }
    return Promise.reject(error.response.data);
  }
);
export default axiosInstance;
