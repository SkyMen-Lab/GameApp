using System.Collections.Generic;
using GameService.Domain.Models;
using Newtonsoft.Json;

namespace GameService.Domain.DTOs
{
    public class GameSummaryDTO
    {
        public string GameCode { get; set; }
        public List<Team> Teams { get; set; }
        public string WinnerCode { get; set; }
        public int MaxSpeedLevel { get; set; }
    }
}