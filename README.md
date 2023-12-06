### Front-end

```javascript
 const response = api.post('/vendas/criar', dadosDaVenda);

    if(response.StatusCode === 200)
    {
        api.post('/produtos/notificar-venda');
        api.post('/funcionarios/notificar-venda');
        api.post('/clientes/notificar-venda');
    }
```

### Back-end

```csharp
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            // código 
            return Ok($"Produto com ID {id} encontrado.");
        }

        [HttpPost("notificar-venda")]
        public IActionResult NotificarVenda([FromBody] VendaMessage venda)
        {
            // código 
            var produtoId = venda.ProdutoId;

            // Conectar ao RabbitMQ
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                // Declarar a fila de notificações de venda
                channel.QueueDeclare(queue: "fila_notificacoes_venda",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                // Serializar a mensagem de busca por produto
                var buscaProdutoMessage = new BuscaProdutoMessage { ProdutoId = produtoId };
                var messageBody = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(buscaProdutoMessage));

                // Publicar a mensagem na fila de notificações de venda
                channel.BasicPublish(exchange: "",
                                     routingKey: "fila_notificacoes_venda",
                                     basicProperties: null,
                                     body: messageBody);

                Console.WriteLine($"Notificação de Venda Recebida. Buscando produto com ID: {produtoId}");
            }

            // Responder à notificação de venda
            return Ok("Notificação de venda recebida e busca por produto iniciada.");
        }
```