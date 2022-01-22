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
    this.cartProduct.quantidy = Number.parseInt(
      this.cartProduct.quantidy.toString()
    );
    this.quantidy = Number.parseInt(e.target.value);
    console.log("this.quantidy = " + this.quantidy);
    if (this.timer) {
      clearTimeout(this.timer);
    }
    this.timer = setTimeout(this.updateShoopingCart.bind(this), 600, this);
  }

  checkQuantidy(value: string) {
    value = value.replace("/r", "/");
    let qtd = Number.parseInt(value);

    if (qtd < this.minValue) this.cartProduct.quantidy = this.minValue;
    if (qtd > this.maxValue) this.cartProduct.quantidy = this.maxValue;
  }

  updateShoopingCart() {
    console.log("this.quantidy 2 = " + this.quantidy);
    this.cartProduct.quantidy = this.quantidy;
    this.disabled = true;
    this.shoppingCartService.UpdateCartOnServer();
  }

  addQuantidy(quantidy: number) {
    this.cartProduct.quantidy += quantidy;
    this.checkQuantidy(this.cartProduct.quantidy.toString());
    this.shoppingCartService.UpdateCartOnServer();
  }

  RemoveProduct() {
    this.shoppingCartService.RemoveProduct(this.cartProduct.productId);
    this.cartProduct = null;
  }
}
