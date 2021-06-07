import { Component, OnInit } from '@angular/core';
import { ProductApiService } from '../api-service/product-api.service';
import { map } from 'rxjs/operators';
import { ProductCard } from '../shared/product-card/types/product-card';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ProductCustomizationComponent } from '../shared/product-customization/product-customization.component';
import { Product } from '../api-service/types/product';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  loading;
  productCards$: Observable<ProductCard[]>;
  constructor(private productApi: ProductApiService, private modalService: NgbModal) { }

  ngOnInit(): void {
    this.productCards$ = this.productApi.get()
    .pipe(map(products => products.map((product)=> (
      {
        title: product.title,
        description: product.description,
        image: `${environment.apis.mediaApiBaseUrl}/${product.medias.find(m => m.isPrimary)?.url}`,
        price: `Rs ${product.sizes.find(size => size.title == "8 Inch").price.amount}`,
        product
       
      } as ProductCard))));
  }

  onProductAdded(product: Product){
    const modalRef = this.modalService.open(ProductCustomizationComponent, { 
      size: 'xl',
      scrollable: true
    });
    const component  = modalRef.componentInstance as ProductCustomizationComponent;
    component.productCustomization = {
      image:  `${environment.apis.mediaApiBaseUrl}/${product.medias.find(media => media.isPrimary)?.url}`,
      title: product.title,
      desccription: product.description,
      product: product 
    } 
  }

}
