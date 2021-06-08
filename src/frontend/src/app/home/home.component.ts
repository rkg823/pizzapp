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
      cheeses: product.cheeses.map((cheese, index)=> ({
        id: cheese.id,
        title: cheese.title,
        price: cheese.sizes[0].price.amount,
        descripion: cheese.description,
        selected: index == 0? true: false
      })),
      sauces: product.sauces.map((sauce)=> ({
        id: sauce.id,
        title: sauce.title,
        price: sauce.sizes[0].price.amount,
        descripion: sauce.description,
        selected: false
      })),
      sizes: product.sizes.map((size, index)=> ({
        id: size.id,
        title: size.title,
        price: size.price.amount,
        descripion: size.description,
        selected: index == 0? true: false
      })),
      toppings: product.toppings.map((topping)=> ({
        id: topping.id,
        title: topping.title,
        price: topping.sizes[0].price.amount,
        descripion: topping.description,
        selected: false
      })),
      product: product
    };
    component.selection = {
      cheese: component.productCustomization.cheeses[0],
      sauces: [],
      size: component.productCustomization.sizes[0],
      toppings: [],
      total: 0
    }
  }

}
