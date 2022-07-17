using System;

namespace Common.Contracts
{
    public interface IMessageBroker
    {
        void Receive(EventHandler<ReceiveMessageEventArgs> onMessageReceivedEvent);

        void Send<T>(T message);
    }

}
