import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ProductCard } from './types/product-card';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.scss']
})
export class ProductCardComponent implements OnInit {
  @Input()
  productCard: ProductCard;
  @Output()
  added =new EventEmitter();
  constructor() { }

  ngOnInit(): void {
  }

  add(productCard: ProductCard){
    this.added.emit(productCard.product);
  }
}
