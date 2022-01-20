import { Component, OnInit } from "@angular/core";
import { ShoppingCartService } from "../_services/shopping-cart.service";

@Component({
  selector: "app-nav-menu",
  templateUrl: "./nav-menu.component.html",
  styleUrls: ["./nav-menu.component.css"],
})
export class NavMenuComponent implements OnInit {
  isExpanded = false;

  constructor(public shoppingCartService: ShoppingCartService) {}

  ngOnInit(): void {
    this.LoadLocalStorage();
  }

  LoadLocalStorage() {
    let p = localStorage.getItem("cartProducts");
    if (p !== null) {
      this.shoppingCartService.cartProduct = JSON.parse(
        localStorage.getItem("cartProducts")
      );
    }
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
