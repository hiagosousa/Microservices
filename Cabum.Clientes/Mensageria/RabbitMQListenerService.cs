using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

public class RabbitMQListenerService : BackgroundService
{
    private readonly IServiceProvider _services;

    public RabbitMQListenerService(IServiceProvider services)
    {
        _services = services ?? throw new ArgumentNullException(nameof(services));
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("Inicializando Serviço do RabbitMQ...\n");
        var factory = new ConnectionFactory() { HostName = "localhost" };

        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            // ConfigurarFila(channel, "clientes", ProcessarMensagemClientes);
            // ConfigurarFila(channel, "funcionarios", ProcessarMensagemFuncionarios);
            // ConfigurarFila(channel, "produtos", ProcessarMensagemProdutos);

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
    }

    private void ConfigurarFila(IModel channel, string fila, Action<string> processarMensagem)
    {
        channel.QueueDeclare(queue: fila,
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var mensagem = Encoding.UTF8.GetString(body);

            processarMensagem(mensagem);
        };

        channel.BasicConsume(queue: fila, autoAck: true, consumer: consumer);
    }

    private void ProcessarMensagemClientes(string mensagem)
    {
        using (var scope = _services.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;


            Console.WriteLine($"Mensagem de Clientes Recebida: {mensagem}");
        }
    }

    private void ProcessarMensagemFuncionarios(string mensagem)
    {
        using (var scope = _services.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;


            Console.WriteLine($"Mensagem de Funcionarios Recebida: {mensagem}");
        }
    }

    private void ProcessarMensagemProdutos(string mensagem)
    {
        using (var scope = _services.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;

            Console.WriteLine($"Mensagem de Produtos Recebida: {mensagem}");
        }
    }
}
