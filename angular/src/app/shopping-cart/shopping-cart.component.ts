import { Component, OnInit } from "@angular/core";
import { ShoppingCartService } from "../_services/shopping-cart.service";

@Component({
  selector: "app-shopping-cart",
  templateUrl: "./shopping-cart.component.html",
  styleUrls: ["./shopping-cart.component.css"],
})
export class ShoppingCartComponent implements OnInit {
  constructor(public shoppingCartService: ShoppingCartService) {}

  ngOnInit() {
    this.shoppingCartService.UpdateCartOnServer();
  }

  TestUpdateCart() {
    this.shoppingCartService.UpdateCartOnServer();
  }
}
