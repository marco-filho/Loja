import { combineReducers } from '@reduxjs/toolkit';
import orders from './orders/store/slice';
import ordersDetail from './orders/detail/store/slice';

const reducer = combineReducers({
  orders,
  ordersDetail,
});

export default reducer;
