using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApp.MessageBrokerConfiguration
{
    public class MessageBrokerConfiguration
    {
        public MessageBrokerType MessageBrokerType { get; set; }

        public List<MessageBrokerInfo> MessageBrokers { get; set; }

        public void Init()
        {
            // config.json contains details such as name, connection string about the configured message broker.
            var config = File.ReadAllText(@"MessageBrokerConfiguration\config.json");
            var mbConfig = Newtonsoft.Json.JsonConvert.DeserializeObject<MessageBrokerConfiguration>(config);
            MessageBrokers = mbConfig.MessageBrokers.Where(x => x.Name.Equals(MessageBrokerType.ToString())).ToList();
        }
    }
}
