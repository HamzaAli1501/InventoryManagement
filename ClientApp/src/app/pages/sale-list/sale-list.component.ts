import {HttpClient} from '@angular/common/http';
import {Component, Inject} from '@angular/core';

@Component({
  selector: 'app-sale-list',
  templateUrl: './sale-list.component.html',
  styleUrls: ['./sale-list.component.css']
})
export class SaleListComponent {

  saleList: any [] = [];
  url: string = "";
  private httpService: HttpClient;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.httpService = http;
    this.url = "https://localhost:7206";
  }

  ngOnInit(): void {
    this.loadSales();
  }

  loadSales() {
    let urlPath = this.url + "/Inventory/GetAllSale";
    this.httpService.get(urlPath).subscribe((res: any) => {
      this.saleList = res.data;
    })
  }

}
