namespace GameService.Domain.Configs
{
    public class DbSettings : IDbSettings
    {
        public string CollectionName { get; set; }
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
    }

    public interface IDbSettings
    {
        string CollectionName { get; set; }
        string DatabaseName { get; set; }
        string ConnectionString { get; set; }
    }
}