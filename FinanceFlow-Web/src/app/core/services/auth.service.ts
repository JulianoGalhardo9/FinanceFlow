import { Injectable, signal, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private http = inject(HttpClient);
  private router = inject(Router);

  // Verifique se a porta 5054 é a mesma que seu Backend exibe no terminal
  private readonly API_URL = 'http://localhost:5054/api/auth'; 

  // Signal que armazena o token e lê do localStorage ao iniciar
  token = signal<string | null>(localStorage.getItem('token'));

  /**
   * Realiza o login enviando e-mail e senha para a API
   */
  login(credentials: { email: string; password: string }) {
    return this.http.post<{ token: string }>(`${this.API_URL}/login`, credentials).pipe(
      tap(response => {
        // Salva o token no navegador para persistência
        localStorage.setItem('token', response.token);
        // Atualiza o estado reativo (Signal)
        this.token.set(response.token);
      })
    );
  }

  /**
   * Finaliza a sessão do usuário
   */
  logout() {
    // Remove os dados de autenticação
    localStorage.removeItem('token');
    // Reseta o estado para null, o que acionará o AuthGuard se necessário
    this.token.set(null);
    // Redireciona para a tela de login
    this.router.navigate(['/login']);
  }

  /**
   * Helper para verificar se o usuário está autenticado
   */
  isAuthenticated(): boolean {
    return !!this.token();
  }
}