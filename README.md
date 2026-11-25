# HomeTask API para estudos

API REST desenvolvida com **C# e ASP.NET Core** para gerenciamento de tarefas e organização familiar.

O HomeTask permite cadastrar membros da família, criar tarefas e atribuí-las aos integrantes, facilitando a organização e o acompanhamento das atividades do dia a dia.

## Tecnologias utilizadas

- C#
- .NET 10
- ASP.NET Core Web API
- Swagger / OpenAPI
- Injeção de Dependência
- Git e GitHub

## Arquitetura do projeto

O projeto utiliza uma organização em camadas, separando as responsabilidades para facilitar a manutenção e a evolução da aplicação.

```text
HomeTask/
│
├── HomeTask.Api/
│   └── Controllers/
│
├── HomeTask.Core/
│   ├── Entities/
│   ├── Enums/
│   ├── Interfaces/
│   └── Services/
│
├── HomeTask.Infra/
│   └── Repositories/
│
├── Program.cs
└── HomeTask.csproj
```

### Responsabilidades das camadas

- **API:** recebe as requisições HTTP (Requests) por meio dos Controllers, encaminha as operações para a camada de serviços e retorna as respostas HTTP (Responses) ao cliente.
- **Core:** concentra as entidades, enums, interfaces e regras de negócio da aplicação.
- **Infra:** contém as implementações dos repositórios responsáveis pelo acesso e gerenciamento dos dados.

## Funcionalidades

### 1. Gerenciamento de membros da família

Permite cadastrar, consultar, atualizar e excluir membros da família.

Operações disponíveis:

- Cadastrar membro.
- Listar todos os membros.
- Consultar membro por ID.
- Atualizar membro.
- Excluir membro.

### 2. Gerenciamento de tarefas

Permite criar e administrar tarefas da família.

Operações disponíveis:

- Criar tarefa.
- Listar todas as tarefas.
- Consultar tarefa por ID.
- Atualizar tarefa.
- Excluir tarefa.

Cada tarefa possui nome, descrição, data de criação, prazo, status e indicador de atividade.

### 3. Atribuição de tarefas

Permite associar tarefas aos membros da família.

Operações disponíveis:

- Criar atribuição.
- Listar todas as atribuições.
- Consultar atribuição por ID.
- Consultar atribuições por tarefa.
- Consultar atribuições por membro da família.
- Atualizar atribuição.
- Excluir atribuição.

## Endpoints da API

Base URL: `http://localhost:5000`

### Gerenciamento de membros da família — FamilyMember

- `GET /api/FamilyMember` — Lista todos os membros.
- `GET /api/FamilyMember/{id}` — Consulta um membro pelo ID.
- `POST /api/FamilyMember` — Cadastra um novo membro.
- `PUT /api/FamilyMember/{id}` — Atualiza os dados de um membro.
- `DELETE /api/FamilyMember/{id}` — Exclui um membro.

### Gerenciamento de tarefas — TaskList

- `GET /api/TaskList` — Lista todas as tarefas.
- `GET /api/TaskList/{id}` — Consulta uma tarefa pelo ID.
- `POST /api/TaskList` — Cria uma nova tarefa.
- `PUT /api/TaskList/{id}` — Atualiza uma tarefa.
- `DELETE /api/TaskList/{id}` — Exclui uma tarefa.

### Atribuição de tarefas — TaskAssignment

- `GET /api/TaskAssignment` — Lista todas as atribuições.
- `GET /api/TaskAssignment/{id}` — Consulta uma atribuição pelo ID.
- `GET /api/TaskAssignment/task/{taskId}` — Lista as atribuições de uma tarefa.
- `GET /api/TaskAssignment/member/{familyMemberId}` — Lista as atribuições de um membro.
- `POST /api/TaskAssignment` — Cria uma atribuição de tarefa.
- `PUT /api/TaskAssignment/{id}` — Atualiza uma atribuição.
- `DELETE /api/TaskAssignment/{id}` — Exclui uma atribuição.

### Status das tarefas

O status de uma tarefa pode assumir os seguintes valores:

- `0` — Pending (Pendente)
- `1` — InProgress (Em andamento)
- `2` — Completed (Concluída)
- `3` — Canceled (Cancelada)

## Como executar o projeto

### Pré-requisitos

- .NET SDK 10 ou superior compatível com o projeto.
- Visual Studio ou Visual Studio Code.
- Git (opcional, para clonar o repositório).

### 1. Clonar o repositório

```bash
git clone URL_DO_REPOSITORIO
```

### 2. Acessar a pasta do projeto

```bash
cd HomeTask
```

### 3. Restaurar as dependências

```bash
dotnet restore
```

### 4. Executar a aplicação

```bash
dotnet run --urls http://localhost:5000
```

## Documentação da API — Swagger

Após iniciar a aplicação, acesse:

[http://localhost:5000/swagger](http://localhost:5000/swagger)

O Swagger permite visualizar os endpoints disponíveis, consultar os parâmetros e executar requisições HTTP diretamente pela interface.

A especificação OpenAPI pode ser acessada em:

[http://localhost:5000/swagger/v1/swagger.json](http://localhost:5000/swagger/v1/swagger.json)

## Testes manuais

As operações da API são verificadas manualmente utilizando o **Insomnia** e o **Swagger**, permitindo validar as requisições, respostas e códigos HTTP.

## Persistência de dados

Atualmente, os dados são armazenados em memória por meio de repositórios utilizando coleções `List<T>`.

Isso significa que os registros permanecem disponíveis enquanto a aplicação está em execução. Ao reiniciar a API, os dados são perdidos.

A implementação de persistência com banco de dados e Entity Framework Core está prevista como evolução do projeto.

## Próximas evoluções

- Implementar persistência com Entity Framework Core.
- Integrar um banco de dados.
- Desenvolver testes automatizados.
- Aprimorar as regras de negócio e validações.
- Evoluir a documentação da API.

---

Desenvolvido como projeto de estudo para praticar desenvolvimento de APIs REST com C#, ASP.NET Core e organização em camadas.
