# Módulo: Gsl-Info-Cadastrais

Desenvolvido por Lucas Andrade Maciel

## Objetivo
Disponibilização de um projeto Web API para atender demandas de cadastro de clientes, fornecedores, depositos e mercadorias.

## Pré Requisitos / Ferramentas
O projeto está desenvolvido em .Net Core e para o seu funcionamento são recomendadas as seguintes ferramentas:

  - Visual studio (executar localmente a aplicação)
  - Sql Server (banco de dados utilizado na aplicação)

## Como executar

Para executar o projeto é necessário que uma instância do Sql Server esteja disponível na porta 1433 juntamente com a aplicação. Caso isso não ocorra, as credenciais de acesso ao banco podem ser alteradas no arquivo **launchSettings.json** do projeto de API.

Em seguida, basta executar a aplicação via Visual Studio.

## Rotas do projeto

Para cada um dos itens descatados para o back-end foi criada uma rota específica.

**Back-End**

| Clientes | Rota |
| ------ | ------ |
| Cadastro Cliente | POST /api/v1/clientes |
| Obter Cliente | GET /api/v1/clientes/{cpf} |
| Obter Todos Clientes | GET /api/v1/clientes |
| Atualizar Cliente | PUT /api/v1/clientes |
| Deletar Cliente | DELETE /api/v1/clientes/{cpf} |

| Depositos | Rota |
| ------ | ------ |
| Cadastro Deposito | POST /api/v1/depositos |
| Obter Deposito | GET /api/v1/depositos/{codigo} |
| Obter Todos Depositos | GET /api/v1/depositos |
| Atualizar Deposito | PUT /api/v1/depositos |
| Deletar Deposito | DELETE /api/v1/depositos/{codigo} |

| Fornecedores | Rota |
| ------ | ------ |
| Cadastro Fornecedor | POST /api/v1/fornecedores |
| Obter Fornecedor | GET /api/v1/fornecedores/{cnpj} |
| Obter Todos Fornecedores | GET /api/v1/fornecedores |
| Atualizar Fornecedor | PUT /api/v1/fornecedores |
| Deletar Fornecedor | DELETE /api/v1/fornecedores/{cnpj} |

| Mercadorias | Rota |
| ------ | ------ |
| Cadastro Mercadoria | POST /api/v1/mercadorias |
| Obter Mercadoria | GET /api/v1/mercadorias/{codigo} |
| Obter Todos Mercadorias | GET /api/v1/mercadorias |
| Atualizar Mercadoria | PUT /api/v1/mercadorias |
| Deletar Mercadoria | DELETE /api/v1/mercadorias/{codigo} |


## Arquitetura da aplicação
Para o desenvolvimento da aplicação foi desenvolvida uma arquitetura com 4 camadas 

| Nome | Descrição |
| ------ | ------ |
| API | Rotas disponibilizadas ao usuário |
| Application | Regras de negócio desenvolvidas |
| Infrastructure | Acesso a aplicações externas. Ex: SqlServer |
| Domain | Entidades de domínio do projeto |
