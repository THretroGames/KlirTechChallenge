import { CartProductDto } from "../_DTOs/cart-produtc-dto";
import { CartProduct } from "./cart-product";

export interface ShoppingCart {
  cartProductDtos: CartProductDto[];
  totalPrice?: number;
  saved?: number;
  originalPrice?: number;
  quantidy?: number;
}
