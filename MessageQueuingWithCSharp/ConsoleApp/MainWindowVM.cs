using Common;
using Common.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace ConsoleApp
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public EventHandler<SendMessageArgs> MessageHandler { get; }

        public MainWindowVM()
        {
            MessagesOnDisplay = new ObservableCollection<Message>();
            var messageBroker = MessageBrokerFactory.GetMessageBroker();

            EventHandler<ReceiveMessageEventArgs> receiverHandler = (sender, args) =>
            {
                var message = JsonConvert.DeserializeObject<Common.Contracts.Message>(Encoding.UTF8.GetString(args.Message));
                Application.Current.Dispatcher.Invoke(() =>
                {
                    MessagesOnDisplay.Add(message);
                });
               
            };

            messageBroker.Receive(receiverHandler);

            MessageHandler = (sender, args) =>
            {
                var messageToPublish = new Message() { Sender = args.Message.Sender, Value = args.Message.Value };
                messageBroker.Send(messageToPublish);
            };
        }


        public ObservableCollection<Message> MessagesOnDisplay { get; set; }
    }

   

}
