import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { Product } from "../_models/product";

@Injectable({
  providedIn: "root",
})
export class ProductsService {
  products: Product[];
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  loadProducts() {
    this.http.get<Product[]>(this.baseUrl + "products").subscribe(products => {
      this.products = products});
    // this.http.get<Product[]>(this.baseUrl + "products/random-list").subscribe(products => {
    //   this.products = products});
  }
}
