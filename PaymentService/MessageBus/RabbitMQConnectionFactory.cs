using RabbitMQ.Client;

namespace PaymentService.MessageBus;

public class RabbitMQConnectionFactory
{
    private static readonly ConnectionFactory _factory = new()
    {
        HostName = "rabbitmq",
        UserName = "guest",
        Password = "guest"
    };

    private static IConnection? _connection;

    public static IConnection GetConnection()
    {
        if (_connection == null || !_connection.IsOpen)
        {
            _connection = _factory.CreateConnection();
        }
        return _connection;
    }
}
