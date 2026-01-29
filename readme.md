# 📝 ToDo App

Aplicação completa de **gerenciamento de tarefas (ToDo)** dividida em **backend (.NET Web API)** e **frontend (ASP.NET WebForms)**, com autenticação, controle de usuários e persistência em banco de dados PostgreSQL.

---

## 📁 Estrutura do Projeto

```
/ToDoApp
├─ /backend
│   ├─ TodoApp.API.sln
│   ├─ /TodoApp.API
│   │   ├─ Controllers/
│   │   ├─ Models/
│   │   ├─ DTOs/
│   │   ├─ Data/
│   │   ├─ Migrations/
│   │   ├─ Notifications/
│   │   ├─ Repositories/
│   │   ├─ Services/
│   │   ├─ Program.cs
│   │   └─ appsettings.json
│   └─ docker-compose.yml
├─ /frontend
│   ├─ TodoApp.WebForms.sln
│   └─ TodoApp.WebForms/
│       ├─ Pages/
│       ├─ Controls/
│       ├─ App_Code/
│       ├─ Scripts/
│       └─ Web.config
└─ README.md
```

---

## 🧠 Visão Geral

### Backend

* ASP.NET Core Web API
* Entity Framework Core
* PostgreSQL
* Autenticação por usuário
* Validação de propriedade das tarefas (usuário só altera/deleta suas próprias tasks)
* Arquitetura em camadas:

  * Controllers
  * Services (regras de negócio)
  * Repositories (acesso a dados)
  * Notifications (Domain Notifications)

### Frontend

* ASP.NET WebForms
* Bootstrap
* Comunicação via HTTP com a API
* Autenticação por sessão
* Layout responsivo
* CRUD completo de tarefas

---

## ⚙️ Pré-requisitos

Antes de executar o projeto, você precisa ter instalado:

### Geral

* **.NET SDK 8.0+**
* **Docker** (opcional)
* **Git**

### Backend

* PostgreSQL 14+

### Frontend

* Visual Studio 2022+
* IIS Express (já incluso no Visual Studio)

---

## 🚀 Como executar o projeto

Você pode rodar o backend de **duas formas**:

---

# ▶️ Opção 1 — Usando Docker (recomendado)

### 1️⃣ Acesse a pasta do backend

```bash
cd backend
```

### 2️⃣ Suba o container do PostgreSQL

```bash
docker-compose up -d
```

Isso irá criar automaticamente:

* PostgreSQL
* Banco: `TodoApp`
* Porta padrão: `5432`

As credenciais estão definidas no próprio `docker-compose.yml`.

---

### 3️⃣ Configure o `appsettings.json`

Arquivo:

```
/backend/TodoApp.API/appsettings.json
```

Exemplo:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=TodoApp;Username=postgres;Password=postgres"
  }
}
```

---

### 4️⃣ Execute as migrations

```bash
dotnet ef database update
```

---

### 5️⃣ Execute a API

```bash
dotnet run
```

---

# ▶️ Opção 2 — Usando banco externo

Caso utilize um PostgreSQL local ou remoto, basta alterar a connection string:

```json
"DefaultConnection": "Host=conexao_com_banco;Port=porta;Database=TodoApp;Username=usuario;Password=senha"
```

Depois execute:

```bash
dotnet ef database update
dotnet run
```

---

## 🖥️ Executando o Frontend (WebForms)

### 1️⃣ Abra a solution

```
/frontend/TodoApp.WebForms.sln
```

### 2️⃣ Configure a URL da API

No arquivo `Web.config`:

```md
<appSettings>
    <add key="ApiBaseUrl" value="http://localhost:5067" />
</appSettings>
```

Certifique-se que a porta é a mesma utilizada pela API.

---

### 3️⃣ Execute o projeto

* Clique em **Start (IIS Express)**

Frontend disponível em:

```
https://localhost:443xx
```

---

## 🔐 Autenticação e Segurança

A aplicação utiliza **JWT (JSON Web Token)** para autenticação e autorização.

### Fluxo de autenticação

1. Usuário realiza login via API
2. A API gera um **JWT** contendo as informações do usuário
3. O token é retornado ao frontend
4. O frontend envia o token em todas as requisições protegidas:

```
Authorization: Bearer {token}
```

---

### Claims utilizadas no token

O token JWT contém as seguintes claims:

* `sub` → ID do usuário
* `nameidentifier` → ID do usuário (usado pelo ASP.NET)
* `unique_name` → Username
* `jti` → Identificador único do token

Essas claims permitem que a API identifique com segurança o usuário autenticado.

---

### Regras de segurança

* Cada tarefa pertence a um usuário (`UserId`)
* O backend valida automaticamente:

  * o **TaskId**
  * o **UserId extraído do JWT**

Isso garante que:

* ✅ usuários só visualizam suas próprias tarefas
* ✅ usuários só podem atualizar suas próprias tarefas
* ✅ usuários só podem deletar suas próprias tarefas
* ❌ não é possível acessar dados de outros usuários mesmo conhecendo o ID

---

### Importante

Toda validação de autorização ocorre **no backend**, nunca no frontend.

O frontend apenas envia o token — as regras de acesso são aplicadas exclusivamente pela API.

---

## 📦 Principais Dependências

### Backend

* Microsoft.EntityFrameworkCore
* Microsoft.EntityFrameworkCore.PostgreSQL
* Microsoft.EntityFrameworkCore.Tools
* Swashbuckle (Swagger)

### Frontend

* Bootstrap
* ASP.NET WebForms
* HttpClient

---

## 🧪 Funcionalidades

* ✅ Cadastro de usuário
* ✅ Login
* ✅ Logout
* ✅ Criar tarefa
* ✅ Editar tarefa
* ✅ Excluir tarefa
* ✅ Marcar como concluída
* ✅ Listagem em tabela e cards
* ✅ Controle de sessão

---

## 🧭 Possíveis melhorias futuras

* 🔄 Refresh Token
* 📱 Frontend SPA (Blazor / React)
* 🐳 Docker completo (API + DB)
* 🌙 Dark mode

---
```
