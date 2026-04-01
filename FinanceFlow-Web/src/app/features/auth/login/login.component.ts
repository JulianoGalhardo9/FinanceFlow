import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../../core/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  template: `
    <div class="min-h-screen flex items-center justify-center bg-slate-900 p-6 font-sans">
      <div class="w-full max-w-md bg-slate-800 rounded-2xl shadow-2xl border border-slate-700 p-10 transform transition-all">
        
        <div class="text-center mb-10">
          <div class="inline-block p-3 bg-blue-600 rounded-xl mb-4 shadow-lg shadow-blue-900/40">
            <svg xmlns="http://www.w3.org/2000/svg" class="h-8 w-8 text-white" fill="none" viewBox="0 0 24 24" stroke="currentColor">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M13 7h8m0 0v8m0-8l-8 8-4-4-6 6" />
            </svg>
          </div>
          <h2 class="text-3xl font-extrabold text-white tracking-tight">Finance<span class="text-blue-500">Flow</span></h2>
          <p class="text-slate-400 mt-2">Gestão inteligente de ativos</p>
        </div>

        <form (submit)="onSubmit()" #loginForm="ngForm" class="space-y-6">
          <div>
            <label class="block text-sm font-semibold text-slate-300 mb-2">E-mail</label>
            <input type="email" name="email" [(ngModel)]="credentials.email" required
              class="w-full px-4 py-3 bg-slate-900 border border-slate-700 rounded-xl text-white focus:ring-2 focus:ring-blue-500 focus:border-transparent outline-none transition-all placeholder:text-slate-600"
              placeholder="exemplo@email.com">
          </div>

          <div>
            <label class="block text-sm font-semibold text-slate-300 mb-2">Senha</label>
            <input type="password" name="password" [(ngModel)]="credentials.password" required
              class="w-full px-4 py-3 bg-slate-900 border border-slate-700 rounded-xl text-white focus:ring-2 focus:ring-blue-500 focus:border-transparent outline-none transition-all placeholder:text-slate-600"
              placeholder="••••••••">
          </div>

          <button type="submit" [disabled]="loginForm.invalid"
            class="w-full bg-blue-600 hover:bg-blue-700 disabled:opacity-50 disabled:cursor-not-allowed text-white font-bold py-4 rounded-xl transition-all shadow-lg shadow-blue-900/30 active:scale-[0.98]">
            Entrar na Plataforma
          </button>
        </form>

        <div class="mt-8 text-center border-t border-slate-700 pt-6">
          <p class="text-slate-500 text-sm">
            Novo por aqui? <a href="#" class="text-blue-400 font-semibold hover:text-blue-300 transition-colors">Criar conta gratuita</a>
          </p>
        </div>
      </div>
    </div>
  `
})
export class LoginComponent {
  private authService = inject(AuthService);
  private router = inject(Router);

  credentials = { email: '', password: '' };

  onSubmit() {
    this.authService.login(this.credentials).subscribe({
      next: () => {
        console.log('✅ Login realizado com sucesso!');
        this.router.navigate(['/dashboard']);
      },
      error: (err) => {
        console.error('❌ Erro no login', err);
        alert('Credenciais inválidas ou servidor offline.');
      }
    });
  }
}