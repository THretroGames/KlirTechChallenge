import { promotion } from "./promotion";

export interface Product {
  name: string;
  price: number;
  promotion: promotion;
}
