import { HttpClient } from '@angular/common/http';
import { Component, Inject } from '@angular/core';

@Component({
  selector: 'app-new-sale',
  templateUrl: './new-sale.component.html',
  styleUrls: ['./new-sale.component.css']
})
export class NewSaleComponent {
  saleObj: any = {
    "saleId": 0,
    "invoiceNo": "",
    "customerName": "",
    "mobileNo": "",
    "saleDate": "2023-09-23T11:19:38.047Z",
    "productId": 0,
    "quantity": 0,
    "totalAmount": 0
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
    let urlPath = `${this.url}/Inventory/GetAllProducts`

    this.httpService.get(urlPath).subscribe((res: any) => {
      this.productList = res.data;
    })
  }

  checkStock() {
    let urlPath = `${this.url}/Inventory/checkStockByProductId?productId=${this.saleObj.productId}`;

    this.httpService.get(urlPath).subscribe((res: any) => {
      if (!res.result) {
        alert("Stock Not Available");
        this.saleObj.productId = 0;
      }
    })
  }

  onSave() {
    let urlPath = `${this.url}/Inventory/CreateNewSale`;
    this.httpService.post(urlPath, this.saleObj).subscribe(
      {
        next: (res: any) => {
          if (res.result) {
            alert("Sale Done Success")
          } else {
            alert(res.message)
          }
        },
        error: err => {
          alert("API Error");
        }
      });
  }
}
