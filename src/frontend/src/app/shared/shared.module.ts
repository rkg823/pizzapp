import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { NavComponent } from './nav/nav.component';
import { ProductCardComponent } from './product-card/product-card.component';
import { ProductCustomizationComponent } from './product-customization/product-customization.component';
import { SelectionComponent } from './product-customization/selection.component';
import { RouterModule } from '@angular/router';



@NgModule({
  declarations: [
    ProductCardComponent, 
    HeaderComponent, 
    FooterComponent, 
    NavComponent, 
    ProductCustomizationComponent, 
    SelectionComponent
  ],
  imports: [
    CommonModule,
    RouterModule
  ],
  exports: [
    ProductCardComponent, 
    HeaderComponent, 
    FooterComponent, 
    NavComponent
  ]
})
export class SharedModule { }
