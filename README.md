# 🚀 FinanceFlow — Sistema de Gestão de Ativos (.NET 9, Angular 18 & SQL Server)

---

## 🧭 Visão Geral

O **FinanceFlow** é uma plataforma Full Stack de nível empresarial projetada para o controle e monitoramento de carteiras de investimentos. O sistema permite a gestão de múltiplos portfólios, rastreamento de ativos e análise de patrimônio em tempo real.

O projeto foi construído seguindo rigorosos padrões de engenharia de software, incluindo **Clean Architecture**, **Test-Driven Development (TDD)** e os princípios **SOLID**, garantindo uma base de código sustentável e escalável.

---

## ⚙️ Funcionalidades e Arquitetura

### 🏗️ 1. Arquitetura e Injeção de Dependência (DI)
* **Arquitetura em Camadas:** Separação clara entre *Domain*, *Application*, *Infrastructure* e *API*.
* **Injeção de Dependência:** Uso nativo do DI do .NET para desacoplamento de serviços, facilitando a manutenção e a substituição de componentes (ex: troca de provedores de dados ou serviços de e-mail).
* **Singleton & Scoped:** Gerenciamento inteligente do ciclo de vida de objetos para otimização de memória e performance.

---

### 🧪 2. Qualidade de Software e Testes
* **Testes Unitários (xUnit):** Cobertura de lógica de negócio e serviços de domínio, garantindo que as regras financeiras sejam respeitadas.
* **Mocking (Moq/NSubstitute):** Isolamento de dependências externas para testes rápidos e determinísticos.
* **Fluent Assertions:** Escrita de testes legíveis que servem como documentação viva do comportamento do sistema.

---

### 🔐 3. Autenticação e Segurança (JWT)
* **Identity & JWT:** Fluxo completo de autenticação com emissão de tokens assinados e expiração configurável.
* **Auth Guards (Angular):** Proteção de rotas no front-end, impedindo o acesso de usuários não autenticados a áreas sensíveis.
* **Policy-Based Authorization:** Controle de acesso granular no back-end baseado em claims de usuário.

---

### 📊 4. Front-end Reativo com Angular 18
* **Signals & Control Flow:** Implementação das novas APIs do Angular 18 para detecção de mudanças ultra-eficiente.
* **Master-Detail Pattern:** Navegação fluida entre dashboard geral e detalhes específicos de ativos por portfólio.
* **Interceptors:** Centralização da lógica de autenticação HTTP, anexando automaticamente tokens Bearer em cada request.

---

### 🛠️ 5. Persistência e Infraestrutura
* **Entity Framework Core 9:** Uso de Migrations para versionamento de banco de dados e Fluent API para configurações complexas de esquema.
* **SQL Server:** Armazenamento relacional robusto com integridade referencial garantida.
* **Repository Pattern:** Abstração da camada de dados para facilitar testes e manter a lógica de persistência isolada.

---

## 🧰 Tecnologias Utilizadas

### **Back-end**
* **C# / .NET 9** (Runtime)
* **xUnit & Moq** (Testing Stack)
* **Entity Framework Core** (ORM)
* **SQL Server** (Database)
* **Swagger / OpenAPI** (API Documentation)

### **Front-end**
* **Angular 18** (Framework)
* **TypeScript** (Language)
* **Tailwind CSS** (Styling)
* **RxJS** (Reactive Streams)

---

## 🧠 Conceitos Principais Dominados

* **Injeção de Dependência:** Configuração e resolução de dependências complexas no `Program.cs`.
* **Clean Architecture:** Organização de código focada em domínio e independência de frameworks.
* **TDD / Unit Testing:** Garantia de qualidade e prevenção de regressões em sistemas financeiros.
* **Full Stack Integration:** Sincronia perfeita entre modelos C# e interfaces TypeScript.
* **SQL Server Modeling:** Design de tabelas, relacionamentos 1:N e otimização de queries.

---

## 🏁 Conclusão

O **FinanceFlow** não é apenas um CRUD, mas um ecossistema financeiro completo que demonstra o domínio de tecnologias modernas e boas práticas de desenvolvimento. Desde a infraestrutura de testes até a reatividade do Angular 18, o projeto está pronto para ambientes de produção que exigem segurança, performance e código de alta qualidade.
