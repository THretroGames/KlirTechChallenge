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
    console.log("TH");
    this.shoppingCart = { cartProductDtos: [], totalPrice: 0 };
    this.shoppingCart.cartProductDtos = [];
    //this.shoppingCart.cartProducs.length;
    console.log(
      "this.shoppingCart.cartProducs = " +
        this.shoppingCart.cartProductDtos.length
    );
    //this.AddProduct(null);
  }

  AddProduct(product: Product) {
    console.log(
      "this.shoppingCart.cartProducs 2 = " +
        this.shoppingCart.cartProductDtos.length
    );
    let cpFind: CartProduct[] = [];
    if (
      this.shoppingCart.cartProductDtos != null &&
      this.shoppingCart.cartProductDtos.length > 0
    ) {
      console.log("sadsadsadasd");
      cpFind = this.shoppingCart.cartProductDtos.filter(
        (x) => x.productId == product.id
      );
    }
    if (cpFind.length > 0) {
      cpFind[0].quantidy++;
      console.log("sadsadsadasd");
    } else {
      console.log("hhyhyhyhyhyhhyh");
      console.log(
        "this.shoppingCart.cartProducs 3 = " +
          this.shoppingCart.cartProductDtos.length
      );
      let cp: CartProductDto = {
        productId: product.id,
        name: product.name,
        price: product.price,
        quantidy: 1,
      };
      console.log(
        "this.shoppingCart.cartProducs 4 = " +
          this.shoppingCart.cartProductDtos.length
      );
      console.log("shoppingCart" + this.shoppingCart);
      console.log(this.shoppingCart.cartProductDtos);
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
    localStorage.clear();
    let requestList: RequestCartProductDto[] = [];
    requestList = this.getRequestDtoList(this.shoppingCart.cartProductDtos);
    console.log(requestList);
    this.http
      .put<ShoppingCart>(this.baseUrl + "shoppingcart/update", requestList)
      .subscribe((shoppingCart) => {
        console.log("--------------" + shoppingCart.cartProductDtos);
        this.shoppingCart = shoppingCart;
        this.UpdateLocalStorage();
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

  RemoveProduct(id: number) {
    this.shoppingCart.cartProductDtos.forEach((element, index) => {
      if (element.productId == id) {
        this.shoppingCart.cartProductDtos.splice(index, 1);
      }
    });
    this.ClearLocalStorage();
    this.UpdateCartOnServer();
  }

  CheckOut() {
    this.ClearLocalStorage();
    this.shoppingCart = { cartProductDtos: [], totalPrice: 0 };
    this._router.navigate(["/"]);
  }
}
