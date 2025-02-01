using RabbitMQ.Client;
using System.Text.Json;
using System.Text;

namespace UserService.Infrastructure;

public class RabbitMQMessagePublisher
{
    private readonly IModel _channel;
    private readonly string _exchangeName = "user_events";

    public RabbitMQMessagePublisher(IConfiguration configuration)
    {
        var factory = new ConnectionFactory
        {
            HostName = configuration["RabbitMQ:Hostname"],
            UserName = configuration["RabbitMQ:Username"],
            Password = configuration["RabbitMQ:Password"]
        };
        var connection = factory.CreateConnection();
        _channel = connection.CreateModel();
        _channel.ExchangeDeclare(_exchangeName, ExchangeType.Fanout);
    }

    public void PublishUserCreatedEvent(string userId, string email)
    {
        var message = new { UserId = userId, Email = email };
        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
        _channel.BasicPublish(_exchangeName, "", null, body);
    }
}