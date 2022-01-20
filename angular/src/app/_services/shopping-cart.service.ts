import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { CartProduct } from "../_models/cart-product";
import { Product } from "../_models/product";

@Injectable({
  providedIn: "root",
})
export class ShoppingCartService {
  public cartProduct: CartProduct[] = [];

  constructor(private http: HttpClient) {}

  AddProduct(product: Product) {
    let cpFind = this.cartProduct.filter((x) => x.product.id == product.id);
    if (cpFind.length > 0) {
      cpFind[0].quantidy++;
    } else {
      let cp = {
        product: product,
        quantidy: 1,
        promotionApplied: product.promotion,
      };
      this.cartProduct.push(cp);
    }
    this.UpdateLocalStorage();
  }

  UpdateProduct(product: Product, newQuantidy: number) {
    let cpFind = this.cartProduct.filter((x) => x.product.id == product.id);
    if (cpFind.length > 0) {
      cpFind[0].quantidy = newQuantidy;
    }
    this.UpdateLocalStorage();
  }

  GetProductsQuantidy() {
    let quantidy = 0;
    this.cartProduct.forEach((p) => {
      quantidy += p.quantidy;
    });
    return quantidy;
  }

  UpdateLocalStorage() {
    localStorage.setItem("cartProducts", JSON.stringify(this.cartProduct));
  }
}
