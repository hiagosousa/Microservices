using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQMensageiro;

public class MensagemService
{
    public MensagemService()
    {
    }

    public void Publicar(string nome, string mensagem)
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost"
        };

        using (var connection = factory.CreateConnection())
        {
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: nome,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );

                var body = Encoding.UTF8.GetBytes(mensagem);

                channel.BasicPublish(
                    exchange: "",
                    routingKey: nome,
                    basicProperties: null,
                    body: body);
            }
        }
    }

    public void Consumir(string nome)
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost"
        };

        using (var connection = factory.CreateConnection())
        {
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: nome,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var mensagem = Encoding.UTF8.GetString(body);
                };

                channel.BasicConsume(
                    queue: nome,
                    autoAck: true,
                    consumer: consumer
                    );
            }
        }
    }
}
