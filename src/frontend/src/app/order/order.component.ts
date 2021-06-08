import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { map } from 'rxjs/operators';
import { ProductSelection } from '../shared/product-customization/types/product-selection';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.scss']
})
export class OrderComponent implements OnInit {

  constructor(private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    this.route.params
    .pipe(map(_=> {
      return window.history.state;
    }))
    .subscribe((selection: ProductSelection)=>{
    
    });
  }

}
