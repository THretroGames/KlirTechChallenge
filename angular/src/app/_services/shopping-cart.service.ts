import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { ShoppingCartTableComponent } from "../shopping-cart/shopping-cart-table/shopping-cart-table.component";
import { CartProduct } from "../_models/cart-product";
import { Product } from "../_models/product";
import { ShoppingCart } from "../_models/shopping-cart";

@Injectable({
  providedIn: "root",
})
export class ShoppingCartService {
  //public cartProduct: CartProduct[] = [];
  public shoppingCart: ShoppingCart;
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {
    this.shoppingCart = { cartProductDtos: [], totalPrice: 0 };
  }

  AddProduct(product: Product) {
    let cpFind = this.shoppingCart.cartProductDtos.filter(
      (x) => x.productId == product.id
    );
    if (cpFind.length > 0) {
      cpFind[0].quantidy++;
    } else {
      let cp = {
        productId: product.id,
        name: product.name,
        price: product.price,
        quantidy: 1,
        promotionApplied: product.promotion,
      };
      this.shoppingCart.cartProductDtos.push(cp);
    }
    //this.UpdateCartOnServer();
    this.UpdateLocalStorage();
  }

  UpdateProduct(product: Product, newQuantidy: number) {
    let cpFind = this.shoppingCart.cartProductDtos.filter(
      (x) => x.productId == product.id
    );
    if (cpFind.length > 0) {
      cpFind[0].quantidy = newQuantidy;
    }
    this.UpdateLocalStorage();
  }

  UpdateCartOnServer() {
    this.http
      .put<ShoppingCart>(
        this.baseUrl + "shoppingcart/update",
        this.shoppingCart.cartProductDtos
      )
      .subscribe((shoppingCart) => {
        this.shoppingCart = shoppingCart;
        this.UpdateLocalStorage();
      });
  }

  GetProductsQuantidy() {
    let quantidy = 0;
    this.shoppingCart.cartProductDtos.forEach((p) => {
      quantidy += p.quantidy;
    });
    return quantidy;
  }

  UpdateLocalStorage() {
    localStorage.setItem("cart", JSON.stringify(this.shoppingCart));
  }

  ClearLocalStorage() {
    localStorage.clear();
  }
}
