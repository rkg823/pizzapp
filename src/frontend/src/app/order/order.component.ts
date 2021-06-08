import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, Subject } from 'rxjs';
import { map, switchMap, takeUntil } from 'rxjs/operators';
import { OrderApiService } from '../api-service/order-api.service';
import { Order } from '../api-service/types/order';
import { OrderService } from './order.service';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.scss']
})
export class OrderComponent implements OnInit, OnDestroy {
  destroy$ = new Subject();;
  order$: Observable<Order>;
  constructor(private route: ActivatedRoute, private router: Router, private orderApiService: OrderApiService, private orderService: OrderService) { }

  ngOnInit(): void {
    this.order$ = this.route.params
    .pipe(map(_=> {
      const selection = window.history.state;
      if(!selection.product){
        this.router.navigate([
          '/home'
        ]);
      }
      return this.orderService.toOrder(selection);
    }),
    switchMap(order => this.orderApiService.post(order)));
  }

  ngOnDestroy(): void {
    this.destroy$.next();
  }

  confirm(order: Order){
    this.orderApiService
    .put(order)
    .pipe(takeUntil(this.destroy$))
    .subscribe((order)=> {
      this.router.navigate(['/order/confirmation', order.id])
    })
  }
}
