import { Injectable } from '@angular/core';
import { Stock } from '../models/stock';

@Injectable({
  providedIn: 'root'
})
export class FactureService {

  constructor() {

  }

  getStocks(): Stock[] {
    return [];
  }
}
