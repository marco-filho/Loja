export interface GenericApi<T> {
  getAll: () => Promise<T[]>;
  get: (id: number) => Promise<T>;
  add: (order: T) => Promise<void>;
  edit: (order: T) => Promise<void>;
  delete: (id: number) => Promise<void>;
}
