namespace GameService.TCP.Configs
{
    public class ConnectionSettings : IConnectionSettings
    {
        public string StorageServiceAddress { get; set; }
        public string GameGatewayAddress { get; set; }
    }

    public interface IConnectionSettings
    {
        string StorageServiceAddress { get; set; }
        string GameGatewayAddress { get; set; }
    }
}