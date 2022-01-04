using Azure.Messaging.ServiceBus;
using Common.Contracts;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace MessageBrokers.AzureServiceBus
{
    /// <summary>
    /// Azure Service Bus using Azure.Messaging.ServiceBus, This is latest code style.
    /// </summary>
    public class AzureServiceBusMessageBroker : IMessageBroker
    {
        private ServiceBusClient _client;
        private ServiceBusSender _sender;
        private string _queueName;

        public AzureServiceBusMessageBroker(string queueName, string connectionString)
        {
            Console.WriteLine("Configuring Azure Service Bus...");
            _client = new ServiceBusClient(connectionString);
            Console.WriteLine($"Azure Service Bus Configured. ConnectionString :{connectionString}");
            _queueName = queueName;
            _sender = _client.CreateSender(queueName);
            Console.WriteLine("========================================================");
        }

        public async void Receive(EventHandler<MessageEventArgs> onMessageReceivedEvent)
        {
            var options = new ServiceBusProcessorOptions
            {
                AutoCompleteMessages = false,
                MaxConcurrentCalls = 2
            };

            ServiceBusProcessor processor = _client.CreateProcessor(_queueName, options);

            processor.ProcessMessageAsync += (args) =>
            {
                onMessageReceivedEvent.Invoke(this,
                  new MessageEventArgs()
                  {
                      Message = args.Message.Body.ToArray()
                  });
                return args.CompleteMessageAsync(args.Message);
            };

            processor.ProcessErrorAsync += (args) =>
            {
                Console.WriteLine(args.ErrorSource);
                Console.WriteLine(args.FullyQualifiedNamespace);
                Console.WriteLine(args.EntityPath);
                Console.WriteLine(args.Exception.ToString());
                return Task.CompletedTask;
            };

            await processor.StartProcessingAsync();

        }

        public async void Send<T>(T message)
        {
            var messageBody = JsonConvert.SerializeObject(message);
            var msg = new ServiceBusMessage(messageBody);
            msg.ContentType = "application/json";

            await _sender.SendMessageAsync(msg);
        }

    }
}
