import { createApi } from '@reduxjs/toolkit/query/react';
import axiosBaseQuery from '../axios/axiosBaseQuery';

// const UserService = {
//     async GetUser(): Promise<User> {
//         const data = {};
//         try {
//             const response = await AxiosService.post<HttpResponse<User>>(
//                 'api/v1/user/GetUser',
//                 data
//             );
//             return response.data.value;
//         } catch (err: any) {
//             console.log(err);
//             throw new Error(err.detail);
//         }
//     },
// };

// export default UserService;
export const shopApi = createApi({
  reducerPath: 'shopApi',
  baseQuery: axiosBaseQuery(),
  endpoints: (builder) => ({
    getUser: builder.query<Shop, void>({
      query: () => ({
        url: 'api/v1/user/GetUser',
        method: 'get'
      })
    })
  })
});

export const { useLazyGetUserQuery } = shopApi;
