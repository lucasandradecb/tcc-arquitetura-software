# Módulo: Gsl-Gestao-Estrategica

Desenvolvido por Lucas Andrade Maciel

## Objetivo
Disponibilização de um projeto Web API para atender demandas relacionadas a gestão estrátegica da empresa com registro de entregas, pedidos e estoques.

## Pré Requisitos / Ferramentas
O projeto está desenvolvido em .Net Core e para o seu funcionamento são recomendadas as seguintes ferramentas:

  - Visual studio (executar localmente a aplicação)
  - Sql Server (banco de dados utilizado na aplicação)

## Como executar

Para executar o projeto é necessário que uma instância do Sql Server esteja disponível na porta 1433 juntamente com a aplicação. Caso isso não ocorra, as credenciais de acesso ao banco podem ser alteradas no arquivo **launchSettings.json** do projeto de API.

Além disso é importante executar o script **/scripts/CriarBancoGestaoEstrategica.sql** para criar o banco e todas as tabelas relacionadas ao projeto.

Em seguida, basta executar a aplicação via Visual Studio.

## Rotas do projeto

Para cada um dos itens descatados para o back-end foi criada uma rota específica.

**Back-End**

| Estoque | Rota |
| ------ | ------ |
| Cadastro Estoque | POST /api/v1/estoques |
| Obter Estoque | GET /api/v1/estoques/{codigo} |
| Obter Todos Estoques | GET /api/v1/estoques |
| Atualizar Estoque | PUT /api/v1/estoques |
| Deletar Estoque | DELETE /api/v1/estoques/{codigo} |

| Pedido | Rota |
| ------ | ------ |
| Cadastro Pedido | POST /api/v1/pedidos |
| Obter Pedido | GET /api/v1/pedidos/{codigo} |
| Obter Todos Pedidos | GET /api/v1/pedidos |
| Atualizar Pedido | PUT /api/v1/pedidos |
| Deletar Pedido | DELETE /api/v1/pedidos/{codigo} |

| Entrega | Rota |
| ------ | ------ |
| Cadastro Entrega | POST /api/v1/entregas |
| Obter Entrega | GET /api/v1/entregas/{codigo} |
| Obter Todos Entregas | GET /api/v1/entregas |
| Atualizar Entrega | PUT /api/v1/entregas |
| Deletar Entrega | DELETE /api/v1/entregas/{codigo} |



## Arquitetura da aplicação
Para o desenvolvimento da aplicação foi desenvolvida uma arquitetura com 4 camadas 

| Nome | Descrição |
| ------ | ------ |
| API | Rotas disponibilizadas ao usuário |
| Application | Regras de negócio desenvolvidas |
| Infrastructure | Acesso a aplicações externas. Ex: SqlServer |
| Domain | Entidades de domínio do projeto |
