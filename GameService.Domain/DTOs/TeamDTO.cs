namespace GameService.Domain.DTOs
{
    /// <summary>
    /// DTO model class for context data of leaving/joining players
    /// </summary>
    public class TeamDTO
    {
        public string TeamCode { get; set; }
        public string GameCode { get; set; }
    }
}