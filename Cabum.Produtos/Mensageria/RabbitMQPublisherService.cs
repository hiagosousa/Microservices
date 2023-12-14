using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace Cabum.Produtos.Mensageria
{
    public class RabbitMQPublisherService<T> where T: class
    {
        private readonly ConnectionFactory _factory;

        public RabbitMQPublisherService()
        {
            _factory = new ConnectionFactory
            {
                HostName = "localhost",
            };
        }

        public void PublicarMensagem(T message, string queue)
        {
            using (var connection = _factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: queue, durable: false, exclusive: false, autoDelete: false, arguments: null);

                    var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

                    channel.BasicPublish(exchange: "", routingKey: queue, basicProperties: null, body: body);

                    Console.WriteLine($"Mensagem enviada para a fila {queue}: {message}");
                }
            }
        }
    }
}