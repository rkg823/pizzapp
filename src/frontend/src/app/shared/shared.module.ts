import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductComponent } from './product/product.component';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { NavComponent } from './nav/nav.component';



@NgModule({
  declarations: [
    ProductComponent, 
    HeaderComponent, 
    FooterComponent, 
    NavComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [
    ProductComponent, 
    HeaderComponent, 
    FooterComponent, 
    NavComponent
  ]
})
export class SharedModule { }
