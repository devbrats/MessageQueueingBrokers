namespace Common
{
    public class MessageBroker
    {
        public string Name { get; set; }
        public string QueueName { get; set; }
        public string ConnectionString { get; set; }
    }

    public enum MessageBrokerType
    {
        RabbitMQ,
        AzureServiceBus
    }

}
