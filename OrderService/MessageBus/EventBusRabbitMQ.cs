using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using OrderService.Events;

namespace OrderService.MessageBus;

public class EventBusRabbitMQ
{
    private readonly ConnectionFactory _factory;

    public EventBusRabbitMQ()
    {
        _factory = new ConnectionFactory()
        {
            HostName = "rabbitmq",
            UserName = "guest",
            Password = "guest"
        };
    }

    public void Publish<T>(T message, string queueName)
    {
        using var connection = _factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

        var messageBody = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

        channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: messageBody);
    }
}
