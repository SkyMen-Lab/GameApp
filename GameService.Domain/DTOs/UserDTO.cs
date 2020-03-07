namespace GameService.Domain.DTOs
{
    /// <summary>
    /// DTO model class for context data of leaving/joining players
    /// </summary>
    public class UserDTO
    {
        public string SchoolCode { get; set; }
        public string GameCode { get; set; }
    }
}