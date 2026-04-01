// src/app/core/services/auth.service.ts

import { Injectable, signal, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private http = inject(HttpClient);
  
  // O ERRO ESTAVA AQUI: Garanta que o endereço aponta para a porta da sua API .NET
  private readonly API_URL = 'http://localhost:5054/api/auth'; 

  token = signal<string | null>(localStorage.getItem('token'));

  login(credentials: any) {
    // Agora o Angular vai enviar para http://localhost:5054/api/auth/login
    return this.http.post<{ token: string }>(`${this.API_URL}/login`, credentials).pipe(
      tap(response => {
        localStorage.setItem('token', response.token);
        this.token.set(response.token);
      })
    );
  }
}