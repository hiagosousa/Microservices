using System.Text;
using Cabum.Clientes;
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
            ConfigurarFila(channel, "criacaoVendas", ProcessarMensagemCriacaoVendas);
            ConfigurarFila(channel, "atualizacaoVendas", ProcessarMensagemAtualizacaoVendas);
            ConfigurarFila(channel, "exclusaoVendas", ProcessarMensagemExclusaoVendas);

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

    private void ProcessarMensagemCriacaoVendas(string mensagem)
    {
        using (var scope = _services.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;
            var context = serviceProvider.GetRequiredService<ApplicationDBContext>();

            var venda = JsonSerializer.Deserialize<Venda>(mensagem);

            if(venda != null)
            {
                context.Vendas.Add(venda);
                context.SaveChanges();
            }

            Console.WriteLine($"Mensagem Criacao de Vendas Recebida: {mensagem}");
        }
    }

    private void ProcessarMensagemAtualizacaoVendas(string mensagem)
    {
        using (var scope = _services.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;
            var context = serviceProvider.GetRequiredService<ApplicationDBContext>();

            var venda = JsonSerializer.Deserialize<Venda>(mensagem);

            if(venda != null)
            {
                context.Vendas.Update(venda);
                context.SaveChanges();
            }

            Console.WriteLine($"Mensagem Atualizacao de Vendas Recebida: {mensagem}");
        }
    }

    private void ProcessarMensagemExclusaoVendas(string mensagem)
    {
        using (var scope = _services.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;
            var context = serviceProvider.GetRequiredService<ApplicationDBContext>();

            var venda = JsonSerializer.Deserialize<Venda>(mensagem);

            if(venda != null)
            {
                context.Vendas.Remove(venda);
                context.SaveChanges();
            }

            Console.WriteLine($"Mensagem Exclusao de Vendas Recebida: {mensagem}");
        }
    }
}
