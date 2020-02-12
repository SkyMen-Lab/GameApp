namespace GameService.TCP
{
    public class MessageReceived
    {
        public string Ip { get; set; }
        public MessageType MessageType { get; set; }
        public string Content { get; set; }
    }

    public enum MessageType
    {
        Action,
        Error,
        Movement,
    }

    public class MessageToSend<T>
    {
        public MessageType MessageType { get; set; }
        public T Content { get; set; }
    }
}