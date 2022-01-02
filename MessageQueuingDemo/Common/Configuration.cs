using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Common
{
    public class Configuration
    {
        public static MessageBrokerConfiguration MessageBrokerConfiguration { get; set; }

        public static void Init()
        {
            var config = File.ReadAllText(@"config.json");
            MessageBrokerConfiguration = JsonConvert.DeserializeObject<MessageBrokerConfiguration>(config);
            MessageBrokerConfiguration.MessageBrokers = MessageBrokerConfiguration.MessageBrokers.Where(x => x.Name.Equals(MessageBrokerConfiguration.MessageBroker.ToString())).ToList();
        }
    }

    public class MessageBrokerConfiguration
    {
        public MessageBrokerType MessageBroker { get; set; }

        public List<MessageBroker> MessageBrokers { get; set; }
    }
}
