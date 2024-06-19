import type { AppDispatch } from '../../../../store';
import { setLoading } from '../../../../store';
import LojaApi from '../../../../services/loja-api';
import type { Order } from '../../../../types/entities';

const OrderActions = {
  addOrder: (model: Order, callback: () => void) => async (dispatch: AppDispatch) => {
    try {
      dispatch(setLoading(true));

      await LojaApi.add(model);

      callback();
    } finally {
      dispatch(setLoading(false));
    }
  },
  editOrder: (model: Order, callback: () => void) => async (dispatch: AppDispatch) => {
    try {
      dispatch(setLoading(true));

      await LojaApi.edit(model);

      callback();
    } finally {
      dispatch(setLoading(false));
    }
  },
};

export default OrderActions;
