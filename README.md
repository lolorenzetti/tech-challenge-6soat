# Tech Challenge

## Descrição

O projeto **Tech Challenge** é uma API desenvolvida em C# com .NET 6.0 para gestão de pedidos, onde os clientes podem se identificar e escolher produtos de diversas categorias. A API permite o cadastro, edição, exclusão e consulta de produtos, bem como a realização e acompanhamento de pedidos.

## BrainStorming
- [Miro](https://miro.com/app/board/uXjVKX1L2Zs=/)

## Funcionalidades

- Identificação do cliente via nome e e-mail, CPF ou anonimato.
- Escolha de produtos nas categorias: Lanche, Acompanhamento, Bebida e Sobremesa.
- Realização de pedidos e simulação de pagamento.
- Acompanhamento do status do pedido.
- Endpoints para cadastro, edição, exclusão e consulta de produtos por categoria.

## Arquitetura

A arquitetura utilizada é a **Arquitetura Hexagonal**, também conhecida como Arquitetura de "Ports and Adapters", que divide a aplicação em camadas:

- **Domain**: Contém as entidades e interfaces dos repositórios.
- **Application**: Contém os casos de uso ou comandos.
- **Infrastructure**: Contém as implementações dos repositórios, contexto do banco de dados e integrações.
- **API**: Contém os controladores da API.

## Tecnologias Utilizadas

- **.NET 6.0**: Framework para desenvolvimento da API.
- **Entity Framework Core**: ORM para manipulação de dados.
- **MySQL**: Banco de dados relacional utilizado.
- **Docker**: Para conteinerização da aplicação.
- **Docker Compose**: Para orquestração de contêineres.

## Instruções para Clonar e Executar o Projeto com Docker Compose

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

A API estará disponível em [`http://localhost:8101/swagger/index.html`](http://localhost:8101/swagger/index.html)

---

Este README fornece uma visão geral do projeto, incluindo a descrição das funcionalidades, a arquitetura utilizada, as tecnologias envolvidas e instruções detalhadas para clonar e executar o projeto usando Docker Compose.
