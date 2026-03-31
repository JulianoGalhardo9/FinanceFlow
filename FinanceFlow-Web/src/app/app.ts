import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router'; // 1. Importar o símbolo

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet], // 2. Adicionar aqui para o HTML reconhecer
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class AppComponent {
  title = 'FinanceFlow-Web';
}