import axiosInstance from './axiosInstance';

// interface ApiResponse {
//   isSuccess: boolean;
//   isFailure: boolean;
//   error?: ApiError;
//   value: any;
// }

// interface ApiError {
//   code: string;
//   message: string;
// }

// // Format 2: Standard Error Format
// interface ApiErrorFormat2 {
//   type: string;
//   title: string;
//   status: number;
//   detail: string;
//   errors: ApiError; // Could be more specific if you have information about its structure
// }

// type Error = ApiResponse | ApiErrorFormat2;

interface AxiosBaseQueryType {
  url: string;
  method: 'get' | 'post' | 'put' | 'delete';
  data?: any;
  params?: any;
  headers?: any;
  responseType?:
    | 'json'
    | 'blob'
    | 'text'
    | 'arraybuffer'
    | 'document'
    | 'stream';
}

const axiosBaseQuery =
  () =>
  async ({
    url,
    method,
    data,
    params,
    headers,
    responseType
  }: AxiosBaseQueryType) => {
    try {
      const result = await axiosInstance({
        url: url,
        method,
        data,
        params,
        headers,
        responseType: responseType || 'json',
        withCredentials: true
      });

      if (responseType === 'blob') {
        const blob = result.data;

        const contentDisposition =
          result.headers['content-disposition'] ||
          result.headers['Content-Disposition'];
        let fileName = 'downloaded_file.xlsx';
        if (contentDisposition) {
          const matches = /filename[^;=\n]*=((['"]).*?\2|[^;\n]*)/.exec(
            contentDisposition
          );
          if (matches != null && matches[1]) {
            fileName = matches[1].replace(/['"]/g, '');
          }
        }

        const downloadUrl = window.URL.createObjectURL(blob);
        const link = document.createElement('a');
        link.href = downloadUrl;
        link.setAttribute('download', fileName);
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
        window.URL.revokeObjectURL(downloadUrl);
        return { data: null };
      }

      return { data: result.data };
    } catch (axiosError) {
      return { error: axiosError };
    }
  };
export default axiosBaseQuery;
