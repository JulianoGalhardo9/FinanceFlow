# 🚀 FinanceFlow — Ecossistema Full Stack de Gestão de Ativos (.NET 9, Angular 18 & SQL Server)

---

## 🧭 Visão Geral

O **FinanceFlow** é uma plataforma de alta performance para controle de investimentos, permitindo a gestão de múltiplos portfólios e ativos financeiros. O projeto foi concebido sob a ótica de sistemas críticos, priorizando **segurança**, **testabilidade** e **rastreabilidade**.

A arquitetura segue os padrões de **Clean Architecture** e **SOLID**, garantindo que a lógica de negócio esteja isolada de preocupações de infraestrutura, facilitando a evolução tecnológica e a manutenção a longo prazo.

---

## ⚙️ Funcionalidades e Engenharia de Software

### 🏗️ 1. Injeção de Dependência & Ciclo de Vida
* **DI Nativa (Microsoft.Extensions.DependencyInjection):** Implementação rigorosa de Injeção de Dependência para desacoplamento de interfaces e implementações.
* **Service Lifetimes:** Gerenciamento estratégico de ciclos de vida:
    * **Scoped:** Para Contextos de Banco de Dados (EF Core) e Repositórios, garantindo consistência por requisição.
    * **Singleton:** Para serviços de configuração e cache global.
    * **Transient:** Para lógica de mapeamento e utilitários leves.

---

### 📝 2. Observabilidade e Logging Estruturado
* **Serilog Integration:** Substituição do log padrão por uma solução de logging estruturado.
* **Sinks & Tracing:** Configuração de logs para console e arquivos rotativos, permitindo o rastreio de erros em tempo real e auditoria de operações financeiras.
* **Contextual Logging:** Captura de metadados das requisições (User ID, Endpoint, Timestamp) para facilitar o debug em ambientes de produção.

---

### 🧪 3. Qualidade de Software e Testes (QA)
* **Testes Unitários (xUnit):** Validação das regras de negócio no core da aplicação.
* **Mocking Framework (Moq):** Simulação de dependências (Banco de Dados e APIs externas) para garantir a pureza dos testes de unidade.
* **Testes de Integração:** Validação do fluxo completo entre Controllers e a camada de persistência.

---

### 🔐 4. Segurança e Identidade (JWT)
* **Bearer Authentication:** Implementação de JWT com chaves assimétricas para autorização segura.
* **Middleware de Autenticação:** Pipeline customizado no .NET para validação de tokens em cada request.
* **Angular Auth Guards:** Proteção de rotas no front-end e tratamento de expiração de token com redirecionamento automático.

---

### 📊 5. Front-end Reativo (Angular 18)
* **Signals & Control Flow:** Aproveitamento máximo das novas funcionalidades do Angular 18 para uma UI extremamente fluida e sem gargalos de detecção de mudanças.
* **HTTP Interceptors:** Centralização da lógica de segurança e tratamento global de erros de rede e logs de client-side.
* **Tailwind Architecture:** Design System responsivo baseado em utilitários para um Dashboard Dark Mode consistente.

---

### 🛠️ 6. Persistência e Infraestrutura
* **EF Core 9 (Fluent API):** Modelagem detalhada do banco de dados, incluindo índices, chaves estrangeiras e relacionamentos complexos.
* **SQL Server:** Motor de banco de dados relacional para armazenamento seguro e transacional.
* **Repository Pattern:** Camada de abstração que isola o acesso a dados da lógica de aplicação.

---

## 🧰 Tecnologias Utilizadas

### **Back-end**
* **C# / .NET 9** (Runtime)
* **Serilog** (Structured Logging)
* **xUnit / Moq** (Testing)
* **Entity Framework Core 9** (ORM)
* **SQL Server** (Database)

### **Front-end**
* **Angular 18** (Framework)
* **TypeScript** (Language)
* **Tailwind CSS** (Styling)
* **RxJS** (Reactive Extensions)

---

## 🧠 Conceitos Principais Dominados

* **Injeção de Dependência Profunda:** Configuração de contêineres de serviço e inversão de controle.
* **Observabilidade:** Implementação de logs estruturados para monitoramento de saúde do sistema.
* **Clean Architecture:** Organização de projetos em Domain, Application, Infrastructure e Web API.
* **TDD (Test Driven Development):** Mentalidade focada em qualidade e cobertura de código.
* **Segurança JWT:** Ciclo completo de vida do token (Issue, Validation, Interception).
* **Gestão de Estado Reativo:** Uso de Signals para interfaces de alta performance.

---

## 🏁 Conclusão

O **FinanceFlow** é o resultado de uma engenharia moderna, unindo a robustez do .NET 9 com a agilidade do Angular 18. O projeto não entrega apenas telas, mas uma infraestrutura completa com logs, testes e injeção de dependência, estando pronto para ser escalado e mantido em cenários reais de mercado financeiro.
