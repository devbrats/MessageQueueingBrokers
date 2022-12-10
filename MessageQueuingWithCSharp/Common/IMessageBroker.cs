using System;

namespace Common
{
    public interface IMessageBroker
    {
        void Receive(EventHandler<ReceiveMessageEventArgs> onMessageReceivedEvent);

        void Send<T>(T message);
    }

}
