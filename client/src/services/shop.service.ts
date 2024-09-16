import { createApi } from '@reduxjs/toolkit/query/react';
import axiosBaseQuery from '../axios/axiosBaseQuery';

export const shopApi = createApi({
  reducerPath: 'shopApi',
  baseQuery: axiosBaseQuery(),
  endpoints: (builder) => ({
    CreateToken: builder.mutation<Shop, any>({
      query: (data) => ({
        url: '/api/v1/shop',
        method: 'post',
        data: data
      })
    }),
    AuthorizeShop: builder.mutation<string, any>({
      query: (data) => ({
        url: '/api/v1/shop/authorize',
        method: 'post',
        data: data
      })
    })
  })
});

export const { useCreateTokenMutation, useAuthorizeShopMutation } = shopApi;
