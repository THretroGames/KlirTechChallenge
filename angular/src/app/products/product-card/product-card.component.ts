import { Component, Input, OnInit } from '@angular/core';
import { Product } from 'src/app/_models/product';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.css']
})
export class ProductCardComponent implements OnInit {
  @Input() product: Product;

  constructor() { }

  ngOnInit() {
  }

  addProductToCart(){
    console.log(this.product.name);    
  }
}
