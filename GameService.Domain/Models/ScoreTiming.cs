namespace GameService.Domain.Models
{
    public class ScoreTiming
    {
        public string Id { get; set; }
        public object Time { get; set; }
        public Team Team { get; set; }
    }
}