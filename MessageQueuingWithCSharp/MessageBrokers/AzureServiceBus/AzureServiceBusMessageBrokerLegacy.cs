using Common.Contracts;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace MessageBrokers.AzureServiceBus
{
    /// <summary>
    /// Azure Service Bus using Azure.ServiceBus, This is legacy code for azure service bus.
    /// </summary>
    public class AzureServiceBusMessageBrokerLegacy : IMessageBroker
    {
        private QueueClient _client;

        public AzureServiceBusMessageBrokerLegacy(string queueName, string connectionString)
        {
            Console.WriteLine("Configuring Azure Service Bus...");
            _client = new QueueClient(connectionString, queueName);
            Console.WriteLine($"Azure Service Bus Configured. Queue:{queueName}, ConnectionString :{connectionString}");
            Console.WriteLine("========================================================");
        }

        public async void Receive(EventHandler<ReceiveMessageEventArgs> onMessageReceivedEvent)
        {
            var messageHandler = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            _client.RegisterMessageHandler(async (arg, token) =>
            {
                onMessageReceivedEvent.Invoke(this,
                    new ReceiveMessageEventArgs()
                    {
                        Message = arg.Body
                    });

                await _client.CompleteAsync(arg.SystemProperties.LockToken);
            }, messageHandler);

            Console.ReadLine();

            await _client.CloseAsync();
        }

        public async void Send<T>(T message)
        {
            var messageBody = JsonConvert.SerializeObject(message);
            var msg = new Microsoft.Azure.ServiceBus.Message()
            {
                Body = Encoding.UTF8.GetBytes(messageBody),
                ContentType = "application/json"
            };

            await _client.SendAsync(msg);
        }

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }

    }
}
