import axiosInstance from './axiosInstance';

interface AxiosBaseQueryType {
  url: string;
  method: 'get' | 'post' | 'put' | 'delete';
  data?: any;
  params?: any;
  headers?: any;
}

const axiosBaseQuery =
  () =>
  async ({ url, method, data, params, headers }: AxiosBaseQueryType) => {
    try {
      console.log('Headers received:', headers);
      const result = await axiosInstance({
        url: url,
        method,
        data,
        params,
        headers
      });
      return { data: result.data };
    } catch (axiosError) {
      return Promise.reject(axiosError?.response?.data);
    }
  };
export default axiosBaseQuery;
