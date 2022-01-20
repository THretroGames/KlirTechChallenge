import { Product } from "./product";

export interface CartProduct {
  product: Product;
  quantidy: number;
  totalPrice?: number;
  promotionApplied?: string;
}
