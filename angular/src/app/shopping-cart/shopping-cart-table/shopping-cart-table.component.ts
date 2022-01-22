import { Component, Input, OnInit } from "@angular/core";
import { CartProduct } from "src/app/_models/cart-product";
import { ShoppingCartService } from "src/app/_services/shopping-cart.service";

@Component({
  selector: "[app-shopping-cart-table]",
  templateUrl: "./shopping-cart-table.component.html",
  styleUrls: ["./shopping-cart-table.component.css"],
})
export class ShoppingCartTableComponent implements OnInit {
  @Input() cartProduct: CartProduct;
  private timer: any;
  disabled = false;
  minValue = 1;
  maxValue = 99;

  constructor(public shoppingCartService: ShoppingCartService) {}
  quantidy: number;

  ngOnInit() {}

  priceKeyUp(e: any) {
    this.checkQuantidy(e.target.value);
    this.quantidy = this.cartProduct.quantidy;
    if (this.timer) {
      clearTimeout(this.timer);
    }
    this.timer = setTimeout(this.updateShoopingCart.bind(this), 600, this);
  }

  checkQuantidy(value: string) {
    value = value.replace("/r", "/");
    let qtd = Number.parseInt(value);

    this.cartProduct.quantidy = qtd;

    if (qtd < this.minValue) this.cartProduct.quantidy = this.minValue;
    if (qtd > this.maxValue) this.cartProduct.quantidy = this.maxValue;
  }

  updateShoopingCart() {
    this.cartProduct.quantidy = this.quantidy;
    this.disabled = true;
    this.shoppingCartService.updateCartOnServer();
  }

  addQuantidy(quantidy: number) {
    this.cartProduct.quantidy += quantidy;
    this.checkQuantidy(this.cartProduct.quantidy.toString());
    this.shoppingCartService.updateCartOnServer();
  }

  removeProduct() {
    this.shoppingCartService.removeProduct(this.cartProduct.productId);
    this.cartProduct = null;
  }
}
