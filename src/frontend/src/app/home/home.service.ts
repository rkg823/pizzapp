import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Product } from '../api-service/types/product';
import { ProductCustomization } from '../shared/product-customization/types/product-customization';
import { ProductSelection } from '../shared/product-customization/types/product-selection';

@Injectable({
  providedIn: 'root'
})
export class HomeService {

  constructor() { }

  toProductCustomization(product: Product): ProductCustomization {
    return {
      image: `${environment.apis.mediaApiBaseUrl}/${product.medias.find(media => media.isPrimary)?.url}`,
      title: product.title,
      desccription: product.description,
      cheeses: product.cheeses.map((cheese, index) => ({
        id: cheese.id,
        title: cheese.title,
        price: cheese.sizes[0].price,
        descripion: cheese.description,
        selected: index == 0 ? true : false
      })),
      sauces: product.sauces.map((sauce) => ({
        id: sauce.id,
        title: sauce.title,
        price: sauce.sizes[0].price,
        descripion: sauce.description,
        selected: false
      })),
      sizes: product.sizes.map((size, index) => ({
        id: size.id,
        title: size.title,
        price: size.price,
        descripion: size.description,
        selected: index == 0 ? true : false
      })),
      toppings: product.toppings.map((topping) => ({
        id: topping.id,
        title: topping.title,
        price: topping.sizes[0].price,
        descripion: topping.description,
        selected: false
      })),
      product
    }
  }

  toProductSelection(product: Product, productCustomization: ProductCustomization): ProductSelection {
    return {
      cheese: productCustomization.cheeses[0],
      sauces: [],
      size: productCustomization.sizes[0],
      toppings: [],
      total: 0,
      product: {
        id: product.id,
        title: product.title,
        price: product.sizes[0].price,
        descripion: product.description,
        selected: false
      }
    }
  }
}
