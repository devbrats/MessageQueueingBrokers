namespace ConsoleApp.MessageBrokerConfiguration
{
    public class MessageBrokerInfo
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
