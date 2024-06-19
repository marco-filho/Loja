import type { AppDispatch, RootState } from '../../../store';
import { setLoading } from '../../../store';
import LojaApi from '../../../services/loja-api';

import { setList, setError } from './slice';

const OrderActions = {
  getAll: () => async (dispatch: AppDispatch) => {
    try {
      dispatch(setLoading(true));

      const data = await LojaApi.getAll();

      dispatch(setList(data));
    } catch (error) {
      dispatch(setError(error));
    } finally {
      dispatch(setLoading(false));
    }
  },
  deleteOrder: (id: number) => async (dispatch: AppDispatch, getState: () => RootState) => {
    try {
      dispatch(setLoading(true));

      await LojaApi.delete(id);

      const orders = getState().reducers.orders;
      dispatch(setList(orders.list.filter((x) => x.id !== id)));
    } catch (error) {
      dispatch(setError(error));
    } finally {
      dispatch(setLoading(false));
    }
  },
};

export default OrderActions;
