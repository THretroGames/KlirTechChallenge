import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { environment } from "src/environments/environment";
import { ShoppingCartTableComponent } from "../shopping-cart/shopping-cart-table/shopping-cart-table.component";
import { CartProductDto } from "../_DTOs/cart-produtc-dto";
import { RequestCartProductDto } from "../_DTOs/request-cart-product-dto";
import { CartProduct } from "../_models/cart-product";
import { Product } from "../_models/product";
import { ShoppingCart } from "../_models/shopping-cart";

@Injectable({
  providedIn: "root",
})
export class ShoppingCartService {
  //public cartProduct: CartProduct[] = [];
  shoppingCart: ShoppingCart;
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient, private _router: Router) {
    this.shoppingCart = { cartProductDtos: [], totalPrice: 0 };
    this.shoppingCart.cartProductDtos = [];
  }

  addProduct(product: Product) {
    let cpFind: CartProduct[] = [];
    if (
      this.shoppingCart.cartProductDtos != null &&
      this.shoppingCart.cartProductDtos.length > 0
    ) {
      cpFind = this.shoppingCart.cartProductDtos.filter(
        (x) => x.productId == product.id
      );
    }
    if (cpFind.length > 0) {
      cpFind[0].quantidy++;

    } else {
      let cp: CartProductDto = {
        productId: product.id,
        name: product.name,
        price: product.price,
        quantidy: 1,
      };
      this.shoppingCart.cartProductDtos.push(cp);
    }
    //this.UpdateCartOnServer();
    this.updateLocalStorage();
  }

  updateProduct(product: Product, newQuantidy: number) {
    let cpFind = this.shoppingCart.cartProductDtos.filter(
      (x) => x.productId == product.id
    );
    if (cpFind.length > 0) {
      cpFind[0].quantidy = newQuantidy;
    }
    this.updateLocalStorage();
  }

  updateCartOnServer() {
    localStorage.clear();
    let requestList: RequestCartProductDto[] = [];
    requestList = this.getRequestDtoList(this.shoppingCart.cartProductDtos);
    this.http
      .put<ShoppingCart>(this.baseUrl + "shoppingcart/update", requestList)
      .subscribe((shoppingCart) => {
        this.shoppingCart = shoppingCart;
        this.updateLocalStorage();
      });
  }

  getRequestDtoList(list: CartProductDto[]): RequestCartProductDto[] {
    let request: RequestCartProductDto[] = [];
    if (list != null && list.length > 0) {
      list.forEach((p) => {
        request.push(this.getRequestDto(p));
      });
    }
    return request;
  }

  getRequestDto(c: CartProduct): RequestCartProductDto {
    let r: RequestCartProductDto = {
      productId: c.productId,
      quantidy: c.quantidy,
    };
    return r;
  }

  getProductsQuantidy() {
    let quantidy = 0;
    this.shoppingCart.cartProductDtos.forEach((p) => {
      quantidy += p.quantidy;
    });
    return quantidy;
  }

  updateLocalStorage() {
    localStorage.setItem("cart", JSON.stringify(this.shoppingCart));
  }

  clearLocalStorage() {
    localStorage.clear();
  }

  removeProduct(id: number) {
    this.shoppingCart.cartProductDtos.forEach((element, index) => {
      if (element.productId == id) {
        this.shoppingCart.cartProductDtos.splice(index, 1);
      }
    });
    this.clearLocalStorage();
    this.updateCartOnServer();
  }

  checkOut() {
    this.clearLocalStorage();
    this.shoppingCart = { cartProductDtos: [], totalPrice: 0 };
    this._router.navigate(["/"]);
  }
}
