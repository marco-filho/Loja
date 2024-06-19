import { createSlice } from '@reduxjs/toolkit';

const initialState = {
  order: {},
  editing: false,
  creating: false,
};

const { reducer, actions } = createSlice({
  name: 'ordersDetail',
  initialState,
  reducers: {
    setOrder(state, action) {
      state.order = action.payload?.order || {};
      state.editing = action.payload?.editing || false;
      state.creating = action.payload?.creating || false;
    },
  },
});

export default reducer;
export const { setOrder } = actions;
