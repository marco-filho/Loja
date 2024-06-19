export type OrderItem = {
  id: number;
  orderId: number;
  product: string;
  amount: number;
  price: number;
};

export type Order = {
  id: number;
  clientName: string;
  orderedAt: Date;
  total: number;
  items: OrderItem[];
};
