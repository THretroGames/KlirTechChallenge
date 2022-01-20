import { Component, OnInit } from '@angular/core';
import { Product } from '../../_models/product';
import { ProductsService } from '../../_services/products.service';

@Component({
  selector: "app-products-list",
  templateUrl: "./products-list.component.html",
  styleUrls: ["./products-list.component.css"],
})
export class ProductsListComponent implements OnInit {

  constructor(public productsService: ProductsService) {}

  ngOnInit() {
    this.loadProducts(); 
  }

  loadProducts(){
    this.productsService.loadProducts();
  }
}
