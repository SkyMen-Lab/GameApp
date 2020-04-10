namespace GameService.Domain.DTOs
{
    /// <summary>
    /// DTO model class which is sent to GameServer
    /// to update the number of current players
    /// </summary>
    public class UpdateNumberOfPlayersDTO
    {
        public string GameCode { get; set; }
        public string TeamCode { get; set; }
        public int NumberOfPlayers { get; set; }
    }
}