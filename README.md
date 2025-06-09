# 🌎 SOS Natureza - API de Denúncias de Desastres Naturais

Esta é uma API RESTful desenvolvida com ASP.NET Core 8 e Entity Framework Core, conectada a um banco de dados Oracle. O objetivo do projeto é permitir que usuários cadastrem e consultem denúncias relacionadas a desastres naturais, como enchentes, queimadas, deslizamentos e outros eventos ambientais críticos.
---
## 👥 Integrantes

- Eduardo do Nascimento Barriviera - RM 555309  
- Thiago Lima de Freitas - RM 556795  
- Bruno Centurion Fernandes - RM 556531
---

## 🚀 Funcionalidades

- Cadastro de usuários
- Registro de denúncias de desastres naturais
- Listagem e busca de denúncias
- Atualização e exclusão de denúncias

---

## 🛠️ Tecnologias Utilizadas

- ASP.NET Core 8
- Entity Framework Core
- Oracle Database
- AutoMapper
- Swagger (Swashbuckle)
- RESTful API

---

## ⚙️ Como rodar o projeto localmente

### Pré-requisitos

- [.NET SDK 8+](https://dotnet.microsoft.com/download)
- Oracle Database
- Visual Studio 2022 ou VS Code

### Passos

1. **Clone o repositório:**
   ```bash
   git clone https://github.com/edu1805/GS1-dotnet.git
   cd seu-repo
   ```
---
2. ### 🔧 Configuração da Conexão com o Banco
A aplicação **não usa `appsettings.json` para a string de conexão**. Em vez disso, espera que a variável de ambiente `ORACLE_CONNECTION_STRING` esteja definida. Exemplo de valor:
```bash
Data Source=oracle.fiap.com.br:1521/orcl;User ID=XXXXX;Password=XXXXX;
```
---
3. ### Rodando o projeto:
```bash
dotnet restore
dotnet ef database update
dotnet run
```
---
- Acesse a documentação Swagger em: `https://localhost:{PORT}/swagger`

---
## Endpoints do projeto:
### 📘 Endpoints - Usuário

| Método  | Rota                             | Descrição                                 |
|---------|----------------------------------|--------------------------------------------|
| GET     | `/api/Usuario/getAll`           | Retorna todos os usuários                 |
| GET     | `/api/Usuario/getById/{id}`     | Retorna um usuário pelo ID                |
| GET     | `/api/Usuario/buscaByName`      | Busca usuários pelo nome (via query param)|
| POST    | `/api/Usuario/create`           | Cria um novo usuário                      |
| PUT     | `/api/Usuario/update/{id}`      | Atualiza os dados de um usuário existente |
| DELETE  | `/api/Usuario/delete/{id}`      | Remove um usuário pelo ID                 |

---
### 📙 Endpoints - Denúncia

| Método  | Rota                                      | Descrição                                         |
|---------|-------------------------------------------|----------------------------------------------------|
| GET     | `/api/Denuncia/getAll`                   | Retorna todas as denúncias                        |
| GET     | `/api/Denuncia/getById/{id}`             | Retorna uma denúncia pelo ID                      |
| GET     | `/api/Denuncia/usuario/{usuarioId}`      | Retorna todas as denúncias de um determinado usuário |
| POST    | `/api/Denuncia/create`                   | Cria uma nova denúncia                            |
| PUT     | `/api/Denuncia/update/{id}`              | Atualiza os dados de uma denúncia existente       |
| DELETE  | `/api/Denuncia/delete/{id}`              | Remove uma denúncia pelo ID                       |

