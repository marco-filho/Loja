import { GenericApi } from '../types/services';
import type { Order } from '../types/entities';

const LojaApi: GenericApi<Order> = {
  getAll: async () => {
    const response = await fetch('order');

    return (await response.json()) as Order[];
  },
  get: async (id: number) => {
    const response = await fetch(`order/${id}`);

    return (await response.json()) as Order;
  },
  add: async (order: Order) => {
    await fetch(`/order`, {
      method: 'POST',
      body: JSON.stringify(order),
      headers: {
        'Content-Type': 'application/json',
      },
    });
  },
  edit: async (order: Order) => {
    await fetch(`/order/${order.id}`, {
      method: 'PUT',
      body: JSON.stringify(order),
      headers: {
        'Content-Type': 'application/json',
      },
    });
  },
  delete: async (id: number) => {
    await fetch(`order/${id}`, { method: 'DELETE' });
  },
};

export default LojaApi;
