<div align=center>

# Tech Challenge

[Arquitetura](#arquitetura) &nbsp;&bull;&nbsp; [Documentação API](#documentação-api) &nbsp;&bull;&nbsp; [Ordens de Execução](#ordens-de-execução) &nbsp;&bull;&nbsp; [Vídeo Demonstrativo](#vídeo-demonstrativo)

</div>

<br/>
<br/>

## Arquitetura

???

<br/>

## Documentação API

A API estará disponível em [`http://localhost:9101/swagger/index.html`](http://localhost:9101/swagger/index.html)

<br/>


## Ordens de execução

Os passos para a execução da nossa aplicação se resumem a:
- Criar um usuário
- Cadastrar um produto
- Criar um pedido
- Realizar o pagamento
- Avançar no status do pedido


<br/>

### Criando um usuário

`[POST] /api/Cliente`
```json
{ 
    "nome": "João Ferreira da Silva", 
    "email": "joao.silva@provedor.com", 
    "cpf": "12345678910"
}
```

### Adicionando Produtos

Nossa API não suporta cadastramento de produtos em lote, sendo assim é necessário realizar uma requisição por produto.

`[POST] /api/Produto`
```json
{ 
    "nome": "X-Tudo", 
    "descricao": "Tudo o que tiver na cozinha", 
    "categoria": 0, 
    "preco": 27.50
}
```
```json
{ 
    "nome": "Coca Cola", 
    "descricao": "refrigerante", 
    "categoria": 1, 
    "preco": 5.00
}
```

### Montando um pedido

`[POST] /api/Pedido`
```json
{ 
    "clienteId": 1, 
    "itens": [
        { 
            "id": 1, 
            "quantidade": 1, 
            "observacao": "Sem tomate"
        },
        { 
            "id": 2, 
            "quantidade": 1, 
            "observacao": null
        }
    ]
}
```

Após montar o pedido será retornando o token de pagamento do Mercado Pago na entidade `pagamentoExternoId`.

```diff
{
  "id": 1,
  "valorTotal": 32.5,
  "statusPedido": "PENDENTE_PAGAMENTO",
  "statusPagamento": "PENDENTE",
+ "pagamentoExternoId": "429e7c2d-7ffb-4b68-8d5f-83da06668be5"
}
```

### Realizando o pagamento do pedido

Para a realização do pagamento do pedido será necessário enviar a entitade `pagamentoExternoId` recebida na etapa anterior para a rota descrita abaixo:

`[POST] /webhook`
```json
{ 
    "pagamentoExternoId": "429e7c2d-7ffb-4b68-8d5f-83da06668be5"
}
```

Esse envio troca o status do pedido para `RECEBIDO`.

### Avançando o status

`[POST] /api/Pedido/{id}/next-status`

A alteração do status do pedido é feita através da rota acima enviando o ID do pedido diretamente na rota.


<br/>


## Vídeo Demonstrativo

???