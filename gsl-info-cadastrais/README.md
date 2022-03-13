# Repositório: teste-el-backend

# Teste Prático EL Back-end
Desenvolvido por Lucas Andrade Maciel

Email: lucas.maciel@localiza.com

## Objetivo
Disponibilização de um projeto Web API para atender demandas de cadastro de usuários, agendamento de veículos, simulação de preços e devolução dos veículos.

## Pré Requisitos / Ferramentas
O projeto está desenvolvido em .Net Core e para o seu funcionamento são recomendadas as seguintes ferramentas:

  - Visual studio (executar localmente a aplicação)
  - Docker 
  - Redis (banco de dados utilizado na aplicação)

## Como executar

Para executar o projeto é necessário que uma instância do Redis esteja disponível na porta 6379 juntamente com a aplicação. Caso isso não ocorra, as credenciais de acesso ao redis podem ser alteradas no arquivo **launchSettings.json** do projeto de API.

Em seguida, basta executar a aplicação via Visual Studio ou Docker.

## Disponibilizando imagens docker localmente

Acessar o CMD do computador e executar os seguintes comandos

**Redis**
```sh
docker pull redis
docker run -d --hostname my-redis --name some-redis -p 6379:6379 redis:latest
```

## Requisitos Teste Prático

Para cada um dos itens descatados para o back-end foi criada uma rota específica.

**Back-End**

| Feature | Rota |
| ------ | ------ |
| Cadastro Usuário | POST /api/v1/usuarios |
| Cadastro Cliente | POST /api/v1/usuarios/clientes |
| Cadastro Operador | POST /api/v1/usuarios/operadores |
| Login (Dados Usuário) | GET /api/v1/usuarios |
| Cadastro Veículo | POST /api/v1/veiculos |
| Cadastro Marcas Veículo | POST /api/v1/veiculos/marcas |
| Cadastro Modelos Veículo | POST /api/v1/veiculos/modelos |
| Lista de Veículos | GET /api/v1/veiculos |
| Simulação Locação | GET /api/v1/veiculos/{placa}/simulacoes-locacao |
| Agendamento Veículo | POST /api/v1/veiculos/agendamentos |
| Check-List Devolução | POST /api/v1/veiculos/devolucoes |

## Arquitetura da aplicação
Para o desenvolvimento da aplicação foi desenvolvida uma arquitetura com 4 camadas 

| Nome | Descrição |
| ------ | ------ |
| API | Rotas disponibilizadas ao usuário |
| Application | Regras de negócio desenvolvidas |
| Infrastructure | Acesso a aplicações externas. Ex: Redis |
| Domain | Entidades de domínio do projeto |

Além disso, como decisão arquitetural para auxiliar no desenvolvimento prático da aplicação, foi escolhido como banco de dados o Redis, por trabalhar com informações que não precisam ser estruturados, possuir um melhor desempenho em termos de tempo de processamento em comparação a um SQL tradicional, além de ser facilmente implementado, principalmente ao utilizar o SDK disponiblizado pela Localiza.

Contudo, é possível alterar o serviço utilizado como persistência de dados, realizando ajustes apenas na camada de infraestrutura da aplicação.

## Observações

  - Considerando o objetivo principal do teste prático as entregas relacionadas ao back-end, as etapas de Front-end (Web/Mobile) não foram executadas.
  - A rota de gerar PDF com o contrato não é contemplada na aplicação