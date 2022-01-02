using Common;
using Common.Contracts;
using Newtonsoft.Json;
using System;
using System.Text;

namespace App.Receiver
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome To Message Receiver Console !");
            Console.WriteLine("========================================================");

            EventHandler<MessageEventArgs> responseHandler = (sender, args) =>
            {
                var message = JsonConvert.DeserializeObject<Message>(Encoding.UTF8.GetString(args.Message));
                Console.WriteLine(message.ToString());
            };

            var messageBroker = MessageBrokerFactory.GetMessageBroker();
            messageBroker.Receive(responseHandler);
            Console.ReadLine();
        }
       
    }
}
