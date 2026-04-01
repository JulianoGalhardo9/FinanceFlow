import { Component, inject } from '@angular/core';
import { AuthService } from '../../core/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  template: `
    <div class="min-h-screen bg-slate-900 text-white p-8">
      <header class="flex justify-between items-center mb-8 border-b border-slate-800 pb-6">
        <h1 class="text-2xl font-bold">Finance<span class="text-blue-500">Flow</span></h1>
        
        <button (click)="logout()" 
          class="bg-red-500/10 text-red-500 px-4 py-2 rounded-lg hover:bg-red-500/20 transition-all font-semibold">
          Sair com segurança
        </button>
      </header>

      <div class="grid grid-cols-1 md:grid-cols-3 gap-6">
         </div>
    </div>
  `
})
export class DashboardComponent {
  private authService = inject(AuthService);
  private router = inject(Router);

  logout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}