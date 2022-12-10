using Common;
using ConsoleApp.MessageBrokerConfiguration;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using Message = Common.Message;

namespace ConsoleApp
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public EventHandler<SendMessageArgs> MessageSender { get; }

        public MainWindowVM()
        {
            MessagesOnDisplay = new ObservableCollection<MessageVM>();
            var messageBroker = MessageBrokerFactory.GetMessageBroker();

            EventHandler<ReceiveMessageEventArgs> receiverHandler = (sender, args) =>
            {
                var message = JsonConvert.DeserializeObject<Message>(Encoding.UTF8.GetString(args.Message));
                Application.Current.Dispatcher.Invoke(() =>
                {
                    MessagesOnDisplay.Add(new MessageVM(message.Sender, message.Value));
                });
               
            };

            messageBroker.Receive(receiverHandler);

            MessageSender = (sender, args) =>
            {
                messageBroker.Send(args.Message);
                MessagesOnDisplay.Add(new MessageVM(args.Message.Sender, args.Message.Value, true));
            };
        }


        public ObservableCollection<MessageVM> MessagesOnDisplay { get; set; }
    }

    public class MessageVM
    {
        public MessageVM(string owner, string value, bool isSelfMessage=false)
        {
            Owner = owner;
            Value = value;
            IsSelfMessage = isSelfMessage;
        }

        public string Owner { get; set; }
        public string Value { get; set; }
        public bool IsSelfMessage { get; set; }

        public override string ToString()
        {
            return $"{Owner}: {Value}";
        }
    }

   

}
