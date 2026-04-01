# 🚀 FinanceFlow — Sistema de Gestão de Ativos e Portfólios (.NET 9, Angular 18 & SQL Server)

---

## 🧭 Visão Geral

O **FinanceFlow** é uma plataforma Full Stack robusta projetada para o controle e monitoramento de carteiras de investimentos. A aplicação permite que usuários gerenciem múltiplos portfólios e acompanhem seus ativos financeiros (como ações e FIIs) de forma centralizada e segura.

Desenvolvido com as tecnologias mais recentes do mercado, o projeto utiliza **.NET 9** no back-end e **Angular 18** no front-end, focando em performance, segurança via **JWT** e uma experiência de usuário fluida com **Signals**.

---

## ⚙️ Funcionalidades e Arquitetura

### 🏗️ 1. Arquitetura Profissional & Clean Code
* **Back-end:** API REST desenvolvida em C# utilizando .NET 9, estruturada para suportar operações assíncronas e alta escalabilidade.
* **Front-end:** SPA (Single Page Application) com Angular 18, adotando a nova sintaxe de *Control Flow* e o gerenciamento de estado reativo com **Signals**.
* **Segurança:** Sistema de Login e Registro com proteção de rotas via **Auth Guards** e persistência de sessão segura.

---

### 🔐 2. Autenticação e Autorização (JWT)
* **Identity Management:** Fluxo completo de cadastro de novos usuários e login validado pelo servidor.
* **Bearer Tokens:** Implementação de tokens JWT para proteger endpoints sensíveis de portfólios e ativos.
* **Interceptação Dinâmica:** Configuração de interceptors no front-end para injeção automática de credenciais em todas as requisições HTTP.

---

### 📊 3. Dashboard Financeiro & UX
* **Interface Premium:** Design Dark Mode moderno, focado na legibilidade de dados e usabilidade do investidor.
* **Master-Detail Flow:** Navegação dinâmica entre a listagem de carteiras e a visualização detalhada de ativos específicos.
* **Interatividade:** Adição de ativos em tempo real através de modais integrados e feedback instantâneo de operações.

---

### 🛠️ 4. Persistência e Modelagem de Dados
* **Entity Framework Core:** Uso de ORM para mapear relacionamentos complexos (1:N) entre Usuários, Portfólios e seus respectivos Ativos.
* **SQL Server:** Banco de dados relacional de alto nível para garantir a persistência íntegra de dados críticos.
* **Eager Loading:** Consultas otimizadas utilizando `.Include()` para garantir que todos os dados relacionados sejam carregados de forma eficiente.

---

### 🌐 5. Comunicação e Documentação
* **Swagger UI:** Documentação interativa da API (OpenAPI) que facilita o teste de endpoints e a integração entre as camadas.
* **CORS Policy:** Políticas de acesso rigorosas para garantir a comunicação segura entre o domínio do Angular e a API .NET.
* **Services Layer:** Front-end baseado em serviços desacoplados (`Injectable Services`), facilitando a manutenção e a testabilidade do código.

---

## 🧰 Tecnologias Utilizadas

### **Back-end**
* **C# / .NET 9** (Última versão LTS)
* **Entity Framework Core** (ORM)
* **SQL Server** (Database)
* **JWT (JSON Web Tokens)** (Segurança)
* **Swagger / UI** (Documentação)

### **Front-end**
* **Angular 18** (Control Flow & Signals)
* **TypeScript** (Tipagem Estrita)
* **Tailwind CSS** (Estilização Responsiva)
* **RxJS** (Programação Reativa)

---

## 🧠 Conceitos Principais Dominados

* Desenvolvimento **Full Stack** de ponta a ponta (API + SPA).
* Implementação de segurança **JWT** robusta no ecossistema .NET.
* Modelagem de dados e migrations com **Entity Framework Core**.
* Gerenciamento de rotas e parâmetros dinâmicos no **Angular Router**.
* Arquitetura de software orientada a serviços e responsabilidades claras.
* Consumo e estruturação de APIs RESTful profissionais.

---

## 🏁 Conclusão

O **FinanceFlow** reflete o domínio técnico de ferramentas de nível empresarial para o setor financeiro. Unindo o processamento eficiente do .NET 9 com a reatividade do Angular 18, o projeto entrega uma solução pronta para os desafios de escalabilidade e segurança exigidos no desenvolvimento de software moderno.
