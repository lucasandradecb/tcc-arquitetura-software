# Módulo: Gsl-Gestao-Estrategica

Desenvolvido por Lucas Andrade Maciel

## Objetivo
Disponibilização de um projeto Web API para atender demandas relacionadas a gestão estrátegica da empresa com regitro de entregas e transações financeiras.

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

| Clientes | Rota |
| ------ | ------ |
| Cadastro Cliente | POST /api/v1/clientes |
| Obter Cliente | GET /api/v1/clientes/{cpf} |
| Obter Todos Clientes | GET /api/v1/clientes |
| Atualizar Cliente | PUT /api/v1/clientes |
| Deletar Cliente | DELETE /api/v1/clientes/{cpf} |



## Arquitetura da aplicação
Para o desenvolvimento da aplicação foi desenvolvida uma arquitetura com 4 camadas 

| Nome | Descrição |
| ------ | ------ |
| API | Rotas disponibilizadas ao usuário |
| Application | Regras de negócio desenvolvidas |
| Infrastructure | Acesso a aplicações externas. Ex: SqlServer |
| Domain | Entidades de domínio do projeto |
