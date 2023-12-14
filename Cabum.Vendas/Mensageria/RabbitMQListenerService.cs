using System.Text;
using System.Text.Json;
using Cabum.Vendas;
using Cabum.Vendas.Models;
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
            ConfigurarFila(channel, "criacaoClientes", ProcessarMensagemCriacaoClientes);
            ConfigurarFila(channel, "atualizacaoClientes", ProcessarMensagemAtualizacaoClientes);
            ConfigurarFila(channel, "exclusaoClientes", ProcessarMensagemExclusaoClientes);


            ConfigurarFila(channel, "criacaoFuncionarios", ProcessarMensagemCriacaoFuncionarios);
            ConfigurarFila(channel, "atualizacaoFuncionarios", ProcessarMensagemAtualizacaoFuncionarios);
            ConfigurarFila(channel, "exclusaoFuncionarios", ProcessarMensagemExclusaoFuncionarios);

            ConfigurarFila(channel, "criacaoProdutos", ProcessarMensagemCriacaoProdutos);
            ConfigurarFila(channel, "atualizacaoProdutos", ProcessarMensagemAtualizacaoProdutos);
            ConfigurarFila(channel, "exclusaoProdutos", ProcessarMensagemExclusaoProdutos);

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

    private void ProcessarMensagemCriacaoClientes(string mensagem)
    {
        using (var scope = _services.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;
            var context = serviceProvider.GetRequiredService<ApplicationDBContext>();

            var cliente = JsonSerializer.Deserialize<Cliente>(mensagem);

            if(cliente != null)
            {
                context.Clientes.Add(cliente);
                context.SaveChanges();
            }

            Console.WriteLine($"Mensagem Criação de Clientes Recebida: {mensagem}");
        }
    }

    private void ProcessarMensagemAtualizacaoClientes(string mensagem)
    {
        using (var scope = _services.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;
            var context = serviceProvider.GetRequiredService<ApplicationDBContext>();

            var cliente = JsonSerializer.Deserialize<Cliente>(mensagem);

            if(cliente != null)
            {
                context.Clientes.Update(cliente);
                context.SaveChanges();
            }

            Console.WriteLine($"Mensagem Atualização de Clientes Recebida: {mensagem}");
        }
    }

    private void ProcessarMensagemExclusaoClientes(string mensagem)
    {
        using (var scope = _services.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;
            var context = serviceProvider.GetRequiredService<ApplicationDBContext>();

            var cliente = JsonSerializer.Deserialize<Cliente>(mensagem);

            if(cliente != null)
            {
                context.Clientes.Remove(cliente);
                context.SaveChanges();
            }

            Console.WriteLine($"Mensagem Exclusao de Clientes Recebida: {mensagem}");
        }
    }

    private void ProcessarMensagemCriacaoFuncionarios(string mensagem)
    {
        using (var scope = _services.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;
            var context = serviceProvider.GetRequiredService<ApplicationDBContext>();

            var funcionario = JsonSerializer.Deserialize<Funcionario>(mensagem);

            if(funcionario != null)
            {
                context.Funcionarios.Add(funcionario);
                context.SaveChanges();
            }

            Console.WriteLine($"Mensagem Criação de Funcionarios Recebida: {mensagem}");
        }
    }

    private void ProcessarMensagemAtualizacaoFuncionarios(string mensagem)
    {
        using (var scope = _services.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;
            var context = serviceProvider.GetRequiredService<ApplicationDBContext>();

            var funcionario = JsonSerializer.Deserialize<Funcionario>(mensagem);

            if(funcionario != null)
            {
                context.Funcionarios.Update(funcionario);
                context.SaveChanges();
            }

            Console.WriteLine($"Mensagem Atualização de Funcionarios Recebida: {mensagem}");
        }
    }

    private void ProcessarMensagemExclusaoFuncionarios(string mensagem)
    {
        using (var scope = _services.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;
            var context = serviceProvider.GetRequiredService<ApplicationDBContext>();

            var funcionario = JsonSerializer.Deserialize<Funcionario>(mensagem);

            if(funcionario != null)
            {
                context.Funcionarios.Remove(funcionario);
                context.SaveChanges();
            }

            Console.WriteLine($"Mensagem Exclusao de Funcionarios Recebida: {mensagem}");
        }
    }

    private void ProcessarMensagemCriacaoProdutos(string mensagem)
    {
        using (var scope = _services.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;
            var context = serviceProvider.GetRequiredService<ApplicationDBContext>();

            var produto = JsonSerializer.Deserialize<Produto>(mensagem);

            if(produto != null)
            {
                context.Produtos.Add(produto);
                context.SaveChanges();
            }

            Console.WriteLine($"Mensagem Criação de Produtos Recebida: {mensagem}");
        }
    }

    private void ProcessarMensagemAtualizacaoProdutos(string mensagem)
    {
        using (var scope = _services.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;
            var context = serviceProvider.GetRequiredService<ApplicationDBContext>();

            var produto = JsonSerializer.Deserialize<Produto>(mensagem);

            if(produto != null)
            {
                context.Produtos.Update(produto);
                context.SaveChanges();
            }

            Console.WriteLine($"Mensagem Atualização de Produtos Recebida: {mensagem}");
        }
    }

    private void ProcessarMensagemExclusaoProdutos(string mensagem)
    {
        using (var scope = _services.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;
            var context = serviceProvider.GetRequiredService<ApplicationDBContext>();

            var produto = JsonSerializer.Deserialize<Produto>(mensagem);

            if(produto != null)
            {
                context.Produtos.Remove(produto);
                context.SaveChanges();
            }

            Console.WriteLine($"Mensagem Exclusao de Produtos Recebida: {mensagem}");
        }
    }
}
