using System.Collections.Generic;

namespace GameService.Domain.Models
{
    public class Game
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public int Duration { get; set; }
        public List<Team> Teams { get; set; }
        public List<ScoreTiming> ScoreTimings { get; set; }
    }
}