import { Component, OnInit } from '@angular/core';
import { ProductApiService } from '../api-service/product-api.service';
import { map } from 'rxjs/operators';
import { ProductCard } from '../shared/product-card/types/product-card';

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
    this.products$ = this.productApi.get().pipe(map(product => {
      return {
        title: product.title,
        description: product.description,
        image: product.medies.find(m => m.isPrimary)?.url
      } as ProductCard;
    }));
  }

}
