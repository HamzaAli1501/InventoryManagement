import {HttpClient} from '@angular/common/http';
import {Component, Inject, OnInit} from '@angular/core';

@Component({
  selector: 'app-stock',
  templateUrl: './stock.component.html',
  styleUrls: ['./stock.component.css']
})
export class StockComponent implements OnInit {

  stockList: any[] = [];
  url: string = "";
  private httpService: HttpClient;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.httpService = http;
    this.url = "https://localhost:7206";
  }

  ngOnInit(): void {
    this.getStock();
  }

  getStock() {
    let urlPath = this.url + "/Inventory/GetAllStock";
    this.httpService.get(urlPath).subscribe((res: any) => {
      this.stockList = res.data;
    })
  }


}
