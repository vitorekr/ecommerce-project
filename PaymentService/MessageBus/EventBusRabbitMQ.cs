using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using PaymentService.Events;

namespace PaymentService.MessageBus;

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

    public void Consume(string queueName, Action<OrderCreatedEvent> processMessage)
    {
        var connection = _factory.CreateConnection();
        var channel = connection.CreateModel();

        channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, eventArgs) =>
        {
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var orderEvent = JsonSerializer.Deserialize<OrderCreatedEvent>(message);

            if (orderEvent != null)
                processMessage(orderEvent);
        };

        channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
    }
}
