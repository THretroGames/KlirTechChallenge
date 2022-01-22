export interface CartProductDto {
  id?: number;
  productId: number;
  name: string;
  price: number;
  quantidy: number;
  originalPrice?: number;
  saved?: number;
  totalPrice?: number;
  promotion?: string;
  promotionApplied?: string;
}
