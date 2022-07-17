using System;

namespace Common.Contracts
{
    public class Message
    {
        public string Sender { get; set; }
        public string Value { get; set; }

        public override string ToString()
        {
            return $"{Sender}: {Value}";
        }
    }

    public class ReceiveMessageEventArgs : EventArgs
    {
        public byte[] Message { get; set; }
    }

    public class SendMessageArgs: EventArgs
    {
        public Message Message { get; set; }
    }
}
