<div align=center>

# Tech Challenge

[Overview](#overview)
&nbsp;&bull;&nbsp;[Documentação](#documentação)
&nbsp;&bull;&nbsp;[Executar localmente](#executar-projeto-localmente-com-docker-compose)
&nbsp;&bull;&nbsp;[Guia de execução API](#guia-de-execução-api)
&nbsp;&bull;&nbsp;[Vídeo demonstrativo](#vídeo-demonstrativo-da-arquitetura)

</div>

## Overview
O projeto **Tech Challenge** é uma API desenvolvida em C# com .NET 6.0 para gestão de pedidos, onde os clientes podem se identificar e escolher produtos de diversas categorias. A API permite o cadastro, edição, exclusão e consulta de produtos, bem como a realização e acompanhamento de pedidos.

### Funcionalidades

- Identificação do cliente via nome e e-mail, CPF ou anonimato.
- Escolha de produtos nas categorias: Lanche, Acompanhamento, Bebida e Sobremesa.
- Realização de pedidos e simulação de pagamento.
- Acompanhamento do status do pedido.
- Endpoints para cadastro, edição, exclusão e consulta de produtos por categoria.

### Tecnologias Utilizadas

- **.NET**
- **Entity Framework Core**
- **MySQL**
- **Docker**
- **Kubernetes**

## Documentação

### Event Storming
- Link da documentação do event storming realizado pelo grupo: [Miro](https://miro.com/app/board/uXjVKX1L2Zs=/)

### Arquitetura

A arquitetura utilizada é a **Clean Architecture**, e foi implementada as seguintes camadas no projeto:

> *Core:*
>- **Domain**: Contém as entidades e interfaces dos repositórios.
>- **Application**: Contém os casos de uso e comandos.

> *Infrastructure:*
>- **Infra.Data**: Contém as implementações dos repositórios, contexto do banco de dados.
>- **Infra.GatewayPagamento**: Contém as implementações de integração com pagamento externo.

> *Presentation:*
>- **API**: Contém os controladores da API.

### Desenho da arquitetura
- Diagrama com requisitos de infraestrutura utilizando Minikube:

![Texto Alternativo](https://raw.githubusercontent.com/lolorenzetti/first-tech-challenge/main/Diagrama.png)

## Vídeo demonstrativo da arquitetura:
- Vídeo demonstrando a execução da infrastrutura kubernetes em execução:
[Veja no youtube](https://youtu.be/CKT5Zn2f8B0)


## Executar projeto localmente com docker compose:

### Pré-requisitos

- [Git](https://git-scm.com/)
- [Docker](https://www.docker.com/)
- [Docker Compose](https://docs.docker.com/compose/)

### Passos para Clonar e Executar

1. **Clonar o Repositório**

```bash
git clone https://github.com/lolorenzetti/first-tech-challenge.git
cd first-tech-challenge/TechChallenge
```

2. **Executar com Docker Compose**

```bash
docker compose up --build
```

Este comando irá construir as imagens e iniciar os contêineres definidos no arquivo `docker-compose.yml`.

4. **Acessar a API**

A API estará disponível em [`http://localhost:9101/swagger/index.html`](http://localhost:9101/swagger/index.html)


## Guia de execução API:

Os passos para a execução da nossa aplicação se resumem a:
- Criar um usuário
- Cadastrar um produto
- Criar um pedido
- Receber webhook de pagamento aprovado (Pagamento fake)
- Consultar ou avançar status do pedido

Você pode ver a [collection do postman](postman_collection.json) aqui.

<br/>

### 1. Criando um usuário:

`[POST] /api/cliente`
```json
{ 
    "nome": "José Marcos Barbosa",
    "email": "jose-barbosa83@citadini.imb.br",
    "cpf": "01963161114"
}
```

### 2. Criando um Produto:
`[POST] /api/produto`
```json
{ 
    "nome": "Mac Wopper 1.0",
    "descricao": "Pão, 2x hamburguer 180g, salda, molho especial.",
    "categoria": 0,
    "preco": 35.80
}
```

### 3. Fazendo checkout (criaçao) do pedido:
`[POST] /api/pedido`
```json
{ 
    "clienteId": 1,
    "itens": [
        {
            "id": 1,
            "quantidade": 2,
            "observacao": "Obs: Sem cebola"
        }
    ]
}
```

Ao criar um pedido irá retornar as informações do pedido e o Id do pagamento externo que foi gerado:
```json
{
    "id": 1,
    "valorTotal": 71.60,
    "statusPedido": "PENDENTE_PAGAMENTO",
    "statusPagamento": "PENDENTE",
    "pagamentoExternoId": "87a4ec0d-e021-4c5b-aa30-da190679381d" // Id externo gerado pelo gateway de pagamento
}
```

Esse ID é guardado no banco de dados e é relacionado com o pedido. Ao receber uma atualização no pagamento do pedido via webhook, esse id é consultado para localizar o pedido de referência e atualizar o status do pedido caso o pagamento tenha sido efetuado com sucesso. 

A rota para o gateway de pagamento se comunicar com o a nossa API é está:

`[POST] /webhook`
```json
{
    "pagamentoExternoId": "87a4ec0d-e021-4c5b-aa30-da190679381d"
}
```

O gateway de pagamento envia o id externo gerado por eles para que possamos consultar e atualizar o status do pedido.

### 4. Consultar status do pedido:
`[GET] /api/pedido/{id}`

Response:
```json
{
    "id": 1,
    "clienteNome": "José Marcos Barbosa",
    "statusPedido": "RECEBIDO",
    "statusPagamento": "APROVADO",
    "valorTotal": 71.60,
    "itens": [
        {
            "produtoId": 1,
            "nome": "Mac Wopper 1.0",
            "quantidade": 2,
            "preco": 35.80,
            "observacao": "Obs: Sem cebola"
        }
    ]
}
```

### 5. Avançar o status do pedido:
`[POST] /api/pedido/{id}/next-status`

Response:
```json
{
    "id": 1,
    "clienteNome": "José Marcos Barbosa",
    "statusPedido": "EM_PREPARACAO",
    "statusPagamento": "APROVADO",
    "valorTotal": 71.60,
    "itens": [
        {
            "produtoId": 1,
            "nome": "Mac Wopper 1.0",
            "quantidade": 2,
            "preco": 35.80,
            "observacao": "Obs: Sem cebola"
        }
    ]
}
```

O pedido é avançado somente se o pagamento foi aprovado e seguindo a seguinte ordem: 

- recebido > avança para em preparação.
- em preparação > avança para pronto.
- pronto > avança para finalizado.

Os pedidos já finalizado ou que ainda não foram recebidos, não aparecem na listagem de pedidos, mas é possível consultar individualmente por ID, conforme etapa 4.

<br>

---
Este README fornece uma visão geral do projeto, incluindo a descrição das funcionalidades, a arquitetura utilizada, as tecnologias envolvidas, instruções detalhadas para clonar e executar o projeto usando Docker Compose e demais instruções necessárias.
