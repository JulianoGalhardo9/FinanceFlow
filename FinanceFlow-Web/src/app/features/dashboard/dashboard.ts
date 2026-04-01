import { Component } from '@angular/core';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  template: `
    <div class="min-h-screen bg-slate-900 text-white p-8">
      <header class="flex justify-between items-center mb-8 border-b border-slate-800 pb-6">
        <h1 class="text-2xl font-bold">Finance<span class="text-blue-500">Flow</span> Dashboard</h1>
        <button class="bg-red-500/10 text-red-500 px-4 py-2 rounded-lg hover:bg-red-500/20 transition-all">Sair</button>
      </header>

      <div class="grid grid-cols-1 md:grid-cols-3 gap-6">
        <div class="bg-slate-800 p-6 rounded-2xl border border-slate-700">
          <p class="text-slate-400 text-sm">Saldo Total</p>
          <h3 class="text-3xl font-bold mt-2">R$ 12.550,00</h3>
        </div>
        <div class="bg-slate-800 p-6 rounded-2xl border border-slate-700">
          <p class="text-slate-400 text-sm">Investimentos</p>
          <h3 class="text-3xl font-bold mt-2 text-green-400">R$ 45.200,00</h3>
        </div>
        <div class="bg-slate-800 p-6 rounded-2xl border border-slate-700">
          <p class="text-slate-400 text-sm">Rentabilidade (Mês)</p>
          <h3 class="text-3xl font-bold mt-2 text-blue-400">+ 2.4%</h3>
        </div>
      </div>
    </div>
  `
})
export class DashboardComponent {}