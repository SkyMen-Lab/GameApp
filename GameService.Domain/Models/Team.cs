namespace GameService.Domain.Models
{
    public class Team
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int NumberOfPlayers { get; set; }
        public double Constant { get; set; }
        public int Score { get; set; }
        public string RouterIp { get; set; }
    }
}