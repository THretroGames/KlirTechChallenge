import { HttpClient } from "@angular/common/http";
import { Component, OnInit } from "@angular/core";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
})
export class AppComponent implements OnInit {
  title = "KlirTechChallenge";
  products: any;

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.getProducts();
  }

  getProducts() {
    this.http.get("http://localhost:5000/api/products").subscribe(
      (response) => {
        this.products = response;
        console.log(this.products);
      },
      (error) => {
        console.log(error);
      }
    );
  }
}
