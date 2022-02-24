import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

const httpOptions = {
  headers: new HttpHeaders(
    {
      'Content-Type': 'application/json',
    }
  )
};

@Injectable({
  providedIn: 'root'
})
export class StockService {
  private url: string = ''
  constructor(
    private http: HttpClient
  ) { }

  getStocks(): Observable<object> {
    return this.http.get(this.url, httpOptions);
  }
}
