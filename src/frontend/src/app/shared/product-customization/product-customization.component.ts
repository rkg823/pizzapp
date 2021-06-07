import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/api-service/types/product';
import { ProductCustomization } from './types/product-customization';

@Component({
  selector: 'app-product-customization',
  templateUrl: './product-customization.component.html',
  styleUrls: ['./product-customization.component.scss']
})
export class ProductCustomizationComponent implements OnInit {
  productCustomization: ProductCustomization;
  constructor() { }

  ngOnInit(): void {
  }

}
