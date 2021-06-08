import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Order } from './types/order';

@Injectable({
  providedIn: 'root'
})
export class OrderApiService {
  private orderApiUrl = `${environment.apis.appApiBaseUrl}/order`;

  constructor(private http: HttpClient) { }

  post(order: Order){
    return this.http.post<Order>(`${this.orderApiUrl}`, order);
  }

  put(order: Order){
    return this.http.put<Order>(`${this.orderApiUrl}`, order);
  }
}
