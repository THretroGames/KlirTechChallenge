import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { Product } from "../_models/product";

@Injectable({
  providedIn: "root",
})
export class ProductsService {
  products: Product;
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getProducts() {
    return this.http.get<Product[]>(this.baseUrl + "products");
  }

  getProduct() {
    return this.http.get<Product[]>(this.baseUrl + "products/1");
  }
}
