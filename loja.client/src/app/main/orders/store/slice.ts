import { createSlice } from '@reduxjs/toolkit';

import type { Order } from '../../../types/entities';

type InitialState = {
  list: Array<Order>;
  error: unknown;
};

const initialState: InitialState = {
  list: [],
  error: {},
};

const { reducer, actions } = createSlice({
  name: 'orders',
  initialState,
  reducers: {
    setList(state, action) {
      state.list = action.payload;
    },
    setError(state, action) {
      state.error = action.payload;
    },
  },
});

export default reducer;
export const { setList, setError } = actions;
