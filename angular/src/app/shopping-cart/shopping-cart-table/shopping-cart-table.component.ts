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
  quantidy = 0;

  constructor(public shoppingCartService: ShoppingCartService) {}

  ngOnInit() {
    this.quantidy =
      this.shoppingCartService.shoppingCart.cartProductDtos.length;
  }

  priceKeyUp(e: any) {
    this.cartProduct.quantidy = Number.parseInt(this.cartProduct.quantidy.toString());
    if (this.timer) {
      clearTimeout(this.timer);
    }
    this.timer = setTimeout(this.updateShoopingCart.bind(this), 600, this);
  }

  updateShoopingCart() {
    this.disabled = true;
    var lala = this.shoppingCartService.UpdateCartOnServer();
  }

  addQuantidy(quantidy: number) {
    quantidy > 0 ? (this.disabled = true) : (this.disabled = false);
    this.cartProduct.quantidy += quantidy;
    this.shoppingCartService.UpdateCartOnServer();
  }
}
