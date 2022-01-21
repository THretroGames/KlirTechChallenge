import { Component, Input, OnInit } from '@angular/core';
import { Product } from 'src/app/_models/product';
import { ShoppingCartService } from 'src/app/_services/shopping-cart.service';

@Component({
  selector: "app-product-card",
  templateUrl: "./product-card.component.html",
  styleUrls: ["./product-card.component.css"],
})
export class ProductCardComponent implements OnInit {
  @Input() product: Product;

  constructor(public shoppingCartService: ShoppingCartService) {}

  ngOnInit() {}

  addProductToCart() {
    this.shoppingCartService.AddProduct(this.product);
  }
}
