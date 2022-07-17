using Common.Contracts;
using MessageBrokers.AzureServiceBus;
using MessageBrokers.RabbitMQ;
using System.Linq;

namespace Common
{
    public class MessageBrokerFactory
    {
        public static IMessageBroker GetMessageBroker()
        {
            Configuration.Init();
            var messageBroker = Configuration.MessageBrokerConfiguration.MessageBrokers.FirstOrDefault();

            if (Configuration.MessageBrokerConfiguration.MessageBroker == MessageBrokerType.AzureServiceBus)
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
