using Common.Contracts;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace MessageBrokers.RabbitMQ
{
    public class RabbitMQMessageBroker : IMessageBroker
    {
        private string _name;
        private string _connectionString;
        private IConnection _connection;
        private IModel _channel;
        private EventingBasicConsumer _consumer;

        public RabbitMQMessageBroker(string name, string connectionString)
        {
            Console.WriteLine("Configuring RabbitMQ...");
            _name = name;
            _connectionString = connectionString;
            Init();
            Console.WriteLine($"RabbitMQ Configured. Queue:{name}, ConnectionString :{connectionString}");
        }

        public void Receive(EventHandler<ReceiveMessageEventArgs> onMessageReceivedEvent)
        {
            _consumer = new EventingBasicConsumer(_channel);
            _consumer.Received += (sender, args) =>
            {
                onMessageReceivedEvent.Invoke(this,
                    new ReceiveMessageEventArgs()
                    {
                        Message = args.Body.ToArray()
                    });
            };
            _channel.BasicConsume(_name, true, _consumer);
        }

        public void Send<T>(T message)
        {
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
            _channel.BasicPublish("", _name, null, body);
        }

        private void Init()
        {
            Console.WriteLine("Establishing Connection...");
            var factory = new ConnectionFactory()
            {
                Uri = new Uri(_connectionString)
            };
            _connection = factory.CreateConnection();
            Console.WriteLine("Connection Established.");

            Console.WriteLine("Creating Channel...");
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(_name,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            Console.WriteLine("Channel Created.");

        }
    }
}
