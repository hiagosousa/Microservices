```csharp
 const response = api.post('/vendas/criar', dadosDaVenda);

    if(response.StatusCode === 200)
    {
        api.post('/produtos/notificar-venda');
        api.post('/funcionarios/notificar-venda');
        api.post('/clientes/notificar-venda');
    }
```