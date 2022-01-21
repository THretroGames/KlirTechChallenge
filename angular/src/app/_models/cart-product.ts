import { Product } from "./product";

export interface CartProduct {
  productId: number;
  name: string;
  price: number;
  quantidy: number;
  totalPrice?: number;
  promotionApplied?: string;
}
