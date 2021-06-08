import { Injectable } from '@angular/core';
import { Order } from '../api-service/types/order';
import { OrderItem } from '../api-service/types/order-item';
import { ProductSelection } from '../shared/product-customization/types/product-selection';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  constructor() { }

  toOrder(selection: ProductSelection): Order{
    const items = new Array<OrderItem>();

    items.push({
      id: selection.size.id,
      title:  selection.size.title,
      price: selection.size.price,
      description: selection.size.descripion
    });

    items.push({
      id: selection.cheese.id,
      title:  selection.cheese.title,
      price: selection.cheese.price,
      description: selection.cheese.descripion
    });

    items.push(...selection.sauces.map(sauce => ({
      id: sauce.id,
      title:  sauce.title,
      price: sauce.price,
      description: sauce.descripion
    })));

    items.push(...selection.toppings.map(topping => ({
      id: topping.id,
      title:  topping.title,
      price: topping.price,
      description: topping.descripion
    })));

    return {
      id: '',
      productId: selection.product.id,
      title: selection.product.title,
      description: selection.product.descripion,
      items,
      amount: selection.total
    }
  }
}
