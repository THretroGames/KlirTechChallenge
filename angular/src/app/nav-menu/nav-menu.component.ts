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
    let c = localStorage.getItem("cart");
    if (c !== null) {
      this.shoppingCartService.shoppingCart = JSON.parse(
        localStorage.getItem("cart")
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
