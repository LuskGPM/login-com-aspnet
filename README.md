# ApiAuth - API de Autenticação e Gerenciamento de Usuários

## 📋 Descrição

**ApiAuth** é uma API RESTful desenvolvida com ASP.NET Core 10 que fornece funcionalidades completas de autenticação e gerenciamento de usuários. A solução implementa padrões modernos de segurança, utilizando JWT (JSON Web Tokens) para autenticação e Identity Framework para controle de acesso.

## 🎯 Funcionalidades Principais

- ✅ **Autenticação de Usuários**: Registro e login com email e senha
- ✅ **Gerenciamento de Perfil**: Obter, atualizar e deletar dados do usuário
- ✅ **Controle de Acesso**: Endpoints protegidos com autorização baseada em claims
- ✅ **Validação de Dados**: DTOs para validação de entrada e saída
- ✅ **Persistência em Banco de Dados**: SQLite com migrações Entity Framework
- ✅ **Documentação Interativa**: OpenAPI (Swagger) disponível em desenvolvimento

## 🏗️ Arquitetura e Estrutura

```
ApiAuth/
├── Controller/
│   ├── IdentityController.cs       # Endpoints de autenticação e perfil
│   └── UserProfile.cs               # Definições adicionais de perfil
├── Model/
│   ├── UserDatabase.cs              # Contexto do Entity Framework
│   └── Schemas/
│       ├── User.cs                  # Entidade User (estende IdentityUser)
│       └── UserDto.cs               # DTOs de transferência de dados
├── Migrations/
│   └── [Migrações do banco]          # Histórico de alterações do schema
├── Properties/
│   └── launchSettings.json           # Configurações de execução
├── Program.cs                        # Configuração da aplicação
├── ApiAuth.csproj                    # Definições do projeto
└── appsettings.json                  # Configurações gerais
```

## 🔧 Tecnologias Utilizadas

| Tecnologia | Versão | Propósito |
|---|---|---|
| **.NET SDK** | 10.0 | Framework principal |
| **ASP.NET Core** | 10.0.2 | Framework web |
| **Entity Framework Core** | 10.0.2 | ORM e acesso a dados |
| **Identity Framework** | 10.0.2 | Autenticação e autorização |
| **SQLite** | 10.0.2 | Banco de dados |
| **AutoMapper** | 16.0.0 | Mapeamento de objetos |
| **OpenAPI** | 10.0.2 | Documentação de API |

## 🚀 Como Começar

### Pré-requisitos

- .NET 10 SDK instalado
- Git (opcional)

### Instalação e Execução

1. **Clone o repositório ou acesse a pasta do projeto:**
   ```bash
   cd c:\Projetos\login-com-aspnet\ApiAuth
   ```

2. **Restaure as dependências:**
   ```bash
   dotnet restore
   ```

3. **Execute as migrações do banco de dados:**
   ```bash
   dotnet ef database update
   ```

4. **Inicie a aplicação:**
   ```bash
   dotnet run
   ```

A API estará disponível em `https://localhost:7106`

## 📡 Endpoints da API

### Autenticação

#### Registrar novo usuário
```http
POST /register
Content-Type: application/json

{
  "email": "usuario@example.com",
  "password": "Senha@123456",
  "cpf": "123.456.789-00"
}
```

**Resposta (200 OK):**
```json
{
  "id": "uuid-do-usuario",
  "email": "usuario@example.com"
}
```

#### Fazer login
```http
POST /login
Content-Type: application/json

{
  "email": "usuario@example.com",
  "password": "Senha@123456"
}
```

**Resposta (200 OK):**
```json
{
  "accessToken": "eyJhbGciOiJIUzI1NiIs...",
  "refreshToken": "eyJhbGciOiJIUzI1NiIs..."
}
```

### Gerenciamento de Usuário

#### Obter dados do usuário
```http
GET /user/{userId}
Authorization: Bearer {accessToken}
```

**Resposta (200 OK):**
```json
{
  "id": "uuid-do-usuario",
  "nome": "João Silva",
  "email": "usuario@example.com",
  "cpf": "123.456.789-00",
  "data_nascimento": "1990-05-15"
}
```

#### Atualizar dados do usuário
```http
PATCH /user/{userId}
Authorization: Bearer {accessToken}
Content-Type: application/json

{
  "nome": "João Silva Santos",
  "cpf": "123.456.789-00",
  "data_nascimento": "1990-05-15"
}
```

**Resposta:** 204 No Content

#### Deletar conta de usuário
```http
DELETE /user/{userId}
Authorization: Bearer {accessToken}
```

**Resposta:** 204 No Content

## 🛡️ Segurança

### Autenticação e Autorização

- **Identity Framework**: Gerencia autenticação e identidade de usuários
- **JWT Tokens**: Acesso seguro via tokens com expiração configurável
- **Claims-based Authorization**: Validação de identidade em endpoints protegidos
- **Senha com Hash**: Senhas são armazenadas com hash seguro (PBKDF2)

### Restrições de Acesso

- Endpoints de perfil (`/user/*`) requerem autenticação
- Usuários podem acessar apenas seus próprios dados
- Validação de identidade via claims do token JWT

### Unicidade de CPF

- Campo CPF possui índice único no banco de dados
- Previne duplicação de registros

## 📊 Modelo de Dados

### Entidade: User

Estende `IdentityUser` do Identity Framework com campos adicionais:

| Campo | Tipo | Descrição |
|---|---|---|
| `Id` | string | Identificador único (GUID) |
| `Email` | string | Email único do usuário |
| `PasswordHash` | string | Senha com hash criptografado |
| `Nome` | string | Nome completo |
| `Cpf` | string | CPF (único) |
| `Data_Nascimento` | DateOnly | Data de nascimento |
| `UserName` | string | Nome de usuário |

### DTOs (Data Transfer Objects)

#### UserInsertDto
Utilizado no registro de novos usuários:
```csharp
public class UserInsertDto
{
    public string? Cpf { get; set; }
    public string? Email { get; set; }
    public string? Nome { get; set; }
    public string? Password { get; set; }
    public DateOnly Data_Nascimento { get; set; }
}
```

#### UserUpdateDto
Utilizado na atualização de perfil:
```csharp
public class UserUpdateDto
{
    public string? Cpf { get; set; }
    public string? Nome { get; set; }
    public DateOnly Data_Nascimento { get; set; }
}
```

#### UserGetDto
Retornado nas buscas de usuário:
```csharp
public class UserGetDto
{
    public string? Id { get; set; }
    public string? Cpf { get; set; }
    public string? Email { get; set; }
    public string? Nome { get; set; }
    public DateOnly Data_Nascimento { get; set; }
}
```

## 🔗 Configuração da Aplicação

### Program.cs

A aplicação é configurada em `Program.cs` com os seguintes componentes:

1. **DbContext**: SQLite configurado para armazenar dados de usuário
2. **AutoMapper**: Mapeamento automático entre entidades e DTOs
3. **Identity Framework**: Gerenciamento de autenticação
4. **OpenAPI**: Documentação interativa em desenvolvimento
5. **Endpoints**: Rotas de autenticação e gerenciamento de usuário

### Banco de Dados

- **Provider**: SQLite
- **Arquivo**: `app.db` (criado automaticamente na raiz do projeto)
- **Migrações**: Entity Framework Migrations para controle de versão do schema

## 🧪 Testando a API

### Usando REST Client (VS Code)

O arquivo `rest.http` contém exemplos de requisições prontas para teste. Utilize a extensão REST Client no VS Code para executar as requisições.

### Usando cURL

```bash
# Registrar
curl -X POST https://localhost:7106/register \
  -H "Content-Type: application/json" \
  -d '{"email":"user@example.com","password":"Pass@123","cpf":"123.456.789-00"}'

# Login
curl -X POST https://localhost:7106/login \
  -H "Content-Type: application/json" \
  -d '{"email":"user@example.com","password":"Pass@123"}'

# Obter usuário (substituir token e ID)
curl -X GET https://localhost:7106/user/{userId} \
  -H "Authorization: Bearer {accessToken}"
```

### Usando Postman ou Insomnia

1. Importe o arquivo `rest.http` ou crie uma coleção manualmente
2. Configure as variáveis de ambiente: `host`, `port`, `authToken`
3. Execute as requisições conforme necessário

## 📝 Configurações

### appsettings.json

Arquivo de configuração geral da aplicação:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  },
  "AllowedHosts": "*"
}
```

### appsettings.Development.json

Configurações específicas para ambiente de desenvolvimento.

## 🔄 Entity Framework Migrations

### Criar nova migração

```bash
dotnet ef migrations add NomeDaMigracao
```

### Aplicar migrações

```bash
dotnet ef database update
```

### Remover última migração

```bash
dotnet ef migrations remove
```

## 🏭 Build e Deploy

### Build da aplicação

```bash
dotnet build
```

Gera a saída compilada em `bin/Debug/` ou `bin/Release/`

### Publicar para produção

```bash
dotnet publish -c Release -o ./publish
```

## 📋 Requisitos Funcionais Atendidos

- ✅ Registro de novo usuário com email, senha e CPF
- ✅ Login com geração de tokens JWT
- ✅ Recuperação de dados do usuário autenticado
- ✅ Atualização de informações do perfil
- ✅ Exclusão de conta de usuário
- ✅ Validação e autorização em endpoints protegidos
- ✅ Persistência de dados em banco relacional
- ✅ Mapeamento automatizado de DTOs

## 📋 Requisitos Não-Funcionais Atendidos

- ✅ Autenticação segura com Identity Framework
- ✅ Criptografia de senhas com PBKDF2
- ✅ Persistência de dados com Entity Framework Core
- ✅ Validação de unicidade de CPF
- ✅ Documentação de API com OpenAPI
- ✅ Estrutura de projeto organizada e escalável

## 📖 Referências

- [Documentação ASP.NET Core](https://docs.microsoft.com/pt-br/aspnet/core/)
- [Entity Framework Core](https://docs.microsoft.com/pt-br/ef/core/)
- [ASP.NET Core Identity](https://docs.microsoft.com/pt-br/aspnet/core/security/authentication/identity/)
- [AutoMapper](https://automapper.org/)
- [OpenAPI Specification](https://swagger.io/specification/)

## 📄 Licença

Este projeto está licenciado sob a licença MIT. Veja o arquivo LICENSE para detalhes.

---

**Última atualização**: 9 de fevereiro de 2026
