import { Component, Input, OnInit } from '@angular/core';
import { ProductSelection } from './types/product-selection';

@Component({
  selector: 'app-selection',
  templateUrl: './selection.component.html',
  styleUrls: ['./selection.component.scss']
})
export class SelectionComponent implements OnInit {
  @Input()
  selection: ProductSelection;
  constructor() { }

  ngOnInit(): void {
  }

}
