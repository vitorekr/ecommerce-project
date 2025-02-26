using System.Text;
using System.Text.Json;
using PaymentService.Events;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace PaymentService.MessageBus;

public class OrderCreatedEventConsumer
{
    private readonly IConnection _connection;
    private readonly RabbitMQ.Client.IModel _channel;

    public OrderCreatedEventConsumer()
    {
        _connection = RabbitMQConnectionFactory.GetConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(queue: "order_created_queue",
                             durable: true,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);
    }

    public void StartListening()
    {
        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += (model, eventArgs) =>
        {
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var orderEvent = JsonSerializer.Deserialize<OrderCreatedEvent>(message);

            if (orderEvent != null)
            {
                Console.WriteLine($"[Pagamento Recebido] Processando pagamento para Pedido ID: {orderEvent.OrderId}, Cliente: {orderEvent.CustomerName}, Valor: {orderEvent.TotalPrice}");

                // Simular processamento de pagamento
                Task.Delay(2000).Wait();

                Console.WriteLine($"[Pagamento Confirmado] Pedido ID {orderEvent.OrderId} foi processado com sucesso.");
            }
        };

        _channel.BasicConsume(queue: "order_created_queue", autoAck: true, consumer: consumer);
    }
}
