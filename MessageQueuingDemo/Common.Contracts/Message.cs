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

    public class MessageEventArgs : EventArgs
    {
        public byte[] Message { get; set; }
    }
}
