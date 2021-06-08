import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { OrderConfirmationComponent } from './order-confirmation.component';
import { OrderComponent } from './order.component';


const routes: Routes = [
  {
    path: '',
    component:  OrderComponent
  },
  {
    path: 'confirmation/:orderId',
    component:  OrderConfirmationComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class OrderRoutingModule { }
