import axiosInstance from './axiosInstance';

interface ApiResponse {
  isSuccess: boolean;
  isFailure: boolean;
  error?: ApiError;
}

interface ApiErrorFormat1 {
  code: string;
  message: string;
}

// Format 2: Standard Error Format
interface ApiErrorFormat2 {
  type: string;
  title: string;
  status: number;
  detail: string;
  errors: any; // Could be more specific if you have information about its structure
}

type ApiError = ApiErrorFormat1 | ApiErrorFormat2;

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
      const responseData: ApiResponse = result.data;

      if (responseData.isSuccess) {
        return { data: responseData };
      } else {
        throw new Error(responseData.error?.message || 'Unknown error');
      }
    } catch (axiosError) {
      const error = axiosError as { response?: { data: ApiError } };

      if (error.response?.data) {
        const apiError = error.response.data;

        if ('code' in apiError && 'message' in apiError) {
          return Promise.reject({
            message: apiError.message,
            code: apiError.code
          });
        } else if ('type' in apiError && 'detail' in apiError) {
          return Promise.reject({
            message: apiError.detail,
            type: apiError.type,
            status: apiError.status
          });
        }
      }

      return Promise.reject({ message: 'An unknown error occurred' });
    }
  };
export default axiosBaseQuery;
