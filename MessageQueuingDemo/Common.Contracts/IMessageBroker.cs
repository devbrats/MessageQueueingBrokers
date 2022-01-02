using System;

namespace Common.Contracts
{
    public interface IMessageBroker
    {
        void Receive(EventHandler<MessageEventArgs> onMessageReceivedEvent);

        void Send<T>(T message);
    }

}
