import { PayloadAction, createReducer, createSlice } from '@reduxjs/toolkit';

interface ShopState {
  shopInfo: Shop | null;
}

const initialState: ShopState = {
  shopInfo: null
};

const shopSlice = createSlice({
  name: 'shop',
  initialState: initialState,
  reducers: {
    saveShop: (state, action: PayloadAction<Shop>) => {
      console.log(action);
      const shop: Shop = action.payload;
      state.shopInfo = shop;
    }
  }
});

export const { saveShop } = shopSlice.actions;
const shopReducer = shopSlice.reducer;

export default shopReducer;
