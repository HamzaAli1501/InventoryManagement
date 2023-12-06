import {HttpClient} from '@angular/common/http';
import {Component, Inject, OnInit} from '@angular/core';

@Component({
  selector: 'app-new-purchase',
  templateUrl: './new-purchase.component.html',
  styleUrls: ['./new-purchase.component.css']
})
export class NewPurchaseComponent implements OnInit {


  purchaseObj: any = {
    "purchaseId": 0,
    "purchaseDate": "2023-09-23T11:00:36.277Z",
    "productId": 0,
    "quantity": 0,
    "supplierName": "",
    "invoiceAmount": 0,
    "invoiceNo": ""
  };
  productList: any[] = [];
  url: string = "";
  private httpService: HttpClient;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.httpService = http;
    this.url = "https://localhost:7206";
  }

  ngOnInit(): void {
    this.getAllProduct();
  }

  getAllProduct() {
    let urlPath = `${this.url}/Inventory/GetAllProducts`;
    this.httpService.get(urlPath).subscribe((res: any) => {
      this.productList = res.data;
    })
  }

  onSave() {
    let urlPath = `${this.url}/Inventory/CreateNewPurchase`;
    this.httpService.post(urlPath, this.purchaseObj).subscribe((res: any) => {
        if (res.result) {
          alert("Purchase Done Success")
        } else {
          alert(res.message)
        }
      },
      error => {
        alert("API Error")
      })
  }

}
