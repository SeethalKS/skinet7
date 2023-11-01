//import { HttpBackend, HttpClient } from '@angular/common/http';
import { Component,OnInit } from '@angular/core';
import { BasketService } from './basket/basket.service';
// import { Product } from './shared/models/product';
// import { Pagination } from './shared/models/pagination';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'Skinet';
  // products: Product[] = [];

  constructor(private basketService:BasketService){}
  
  ngOnInit(): void {
     const basketId =localStorage.getItem('basket_id');
     if(basketId) this.basketService.getBasket(basketId);
  }
  
}
