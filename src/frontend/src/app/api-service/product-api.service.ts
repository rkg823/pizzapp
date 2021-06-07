import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Product } from './types/product';

@Injectable({
  providedIn: 'root'
})
export class ProductApiService {

  private cacheRequestOption = {
    headers: new HttpHeaders({
      'app-client-cache': 'true',
    })
  };

  private productApiUrls = {
    get: `${environment.apis.appApiBaseUrl}/products`,
  }

  constructor(private http: HttpClient) { 

  }

  get(){
    return this.http.get<Product[]>(`${this.productApiUrls.get}`, this.cacheRequestOption);
  }
}
