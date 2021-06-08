import { Component, OnInit } from '@angular/core';
import { NavigationExtras, Router } from '@angular/router';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Product } from 'src/app/api-service/types/product';
import { ProductCustomization } from './types/product-customization';
import { ProductCustomizationOption } from './types/product-customization-option';
import { ProductSelection } from './types/product-selection';

@Component({
  selector: 'app-product-customization',
  templateUrl: './product-customization.component.html',
  styleUrls: ['./product-customization.component.scss']
})
export class ProductCustomizationComponent implements OnInit {
  productCustomization: ProductCustomization;
  selection: ProductSelection;
  constructor(public activeModal: NgbActiveModal, private router: Router) { 
  }

  ngOnInit(): void {
    this.selection.total = this.sum();
  }

  selectMany(option: ProductCustomizationOption, field: string){
    if(option.selected){
      option.selected  = false;
      this.selection[field] = [... this.selection[field].filter(o=> o.id !== option.id)];
    } else {
      option.selected  = true;
      this.selection[field] = [... this.selection[field],option]
    }
    this.selection.total = this.sum();
  }

  selectOne(option: ProductCustomizationOption,options: ProductCustomizationOption[], field: string){
    if(option.selected){
      return;
    }
    options.forEach(option => option.selected = false);
    this.selection[field] = option;
    option.selected = true;
    this.selection.total = this.sum();
  }

  order(){
    this.activeModal.close();
    const extra = {
      state: this.selection
    } as NavigationExtras;
    this.router.navigate([
      '/order'
    ], extra);
  }

  private sum(){
    return [
      this.selection.size.price, 
      this.selection.cheese.price, 
      ...this.selection.sauces.map(sauce => sauce.price), 
      ...this.selection.toppings.map(topping => topping.price)
    ].reduce((pv,cv)=> pv+cv);
  }
}
