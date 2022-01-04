using Common;
using Common.Contracts;
using System;

namespace App.Sender
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome To Message Sender Console !");
            Console.WriteLine("========================================================");
            var messageBroker = MessageBrokerFactory.GetMessageBroker();
            Console.WriteLine("Note: Type 'exit' to exit !");
            Console.Write("Enter User Name: ");
            var sender = Console.ReadLine();
            
            var stopPublishing = false;

            while (!stopPublishing)
            {
                Console.Write("Message: ");
                var message = Console.ReadLine();
                var messageToPublish = new Message() { Sender = sender, Value = message };
                messageBroker.Send(messageToPublish);

                stopPublishing = message.ToUpper() == "EXIT";
            }

            Console.ReadLine();
        }
    }
}
