import { Component, OnInit } from '@angular/core';
import { ProductApiService } from '../api-service/product-api.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  loading;
  products$;
  constructor(private productApi: ProductApiService) { }

  ngOnInit(): void {
    this.products$ = this.productApi.get();
  }

}
