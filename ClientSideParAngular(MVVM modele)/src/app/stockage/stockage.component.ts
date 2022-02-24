import { Component, OnInit } from '@angular/core';
import { Stock } from '../api/models/stock';

@Component({
  selector: 'app-stockage',
  templateUrl: './stockage.component.html',
  styleUrls: ['./stockage.component.scss']
})
export class StockageComponent implements OnInit {
  headers: string[] = ['Nom du produit', 'Quantité']
  stocks: Stock[] = [
    { nomProduit: 'Lit', quantite: 4 },
    { nomProduit: 'Matelas', quantite: 2 },
    { nomProduit: 'Table', quantite: 2 },
    { nomProduit: 'Chauffage', quantite: 8 },

  ]
  constructor() { }

  ngOnInit(): void {
  }


}
