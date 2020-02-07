namespace GameService.TCP
{
    public class TcpSettings : ITcpSettings
    {
        public int Port { get; set; }
    }

    public interface ITcpSettings
    {
        public int Port { get; set; }
    }
}