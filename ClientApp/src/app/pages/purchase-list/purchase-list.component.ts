import {Component, Inject, OnInit} from '@angular/core';
import {HttpClient} from '@angular/common/http';

@Component({
  selector: 'app-purchase-list',
  templateUrl: './purchase-list.component.html',
  styleUrls: ['./purchase-list.component.css']
})
export class PurchaseListComponent implements OnInit {

  purchaseList: any [] = [];
  httpService: HttpClient;
  url: string = "";

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.httpService = http;
    this.url = "https://localhost:7206";
  }

  ngOnInit(): void {
    this.loadPurchase();
  }

  loadPurchase() {
    debugger;
    let urlPath = this.url + "/Inventory/getAllPurchase";
    this.httpService.get(urlPath).subscribe((res: any): void => {
      this.purchaseList = res.data;
    });
    // this.http.get("https://freeapi.miniprojectideas.com/api/Jira/GetAllPurchase").subscribe((res: any) => {
    //   this.purchaseList = res.data;
    // })
  }


}
