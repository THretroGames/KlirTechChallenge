import { CartProduct } from "./cart-product";

export interface ShoppingCart {
  cartProductDtos?: CartProduct[];
  totalPrice?: number;
}
