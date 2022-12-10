
using System.Linq;
using Common;
using MessageBrokers.AzureServiceBus;
using MessageBrokers.RabbitMQ;

namespace ConsoleApp.MessageBrokerConfiguration
{
    public class MessageBrokerFactory
    {
        public static IMessageBroker GetMessageBroker()
        {
            var mbConfiguration = new MessageBrokerConfiguration();
            mbConfiguration.Init();
            var messageBroker = mbConfiguration.MessageBrokers.FirstOrDefault();

            if (mbConfiguration.MessageBrokerType == MessageBrokerType.AzureServiceBus)
            {
                return new AzureServiceBusMessageBroker(messageBroker.QueueName, messageBroker.ConnectionString);
            }
            else
            {
                return new RabbitMQMessageBroker(messageBroker.QueueName, messageBroker.ConnectionString);
            }

        }
    }
}
