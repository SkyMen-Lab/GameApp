using System.Collections.Generic;
using System.Threading.Tasks;
using GameService.Domain.Models;

namespace GameApp.GameConnectors
{
    public interface IGameManager
    {
        public delegate Task SendMessageDelegate(string content);
        public SendMessageDelegate SendMessageHandler { get; set; }
        Task StartTheGameAsync(string code);
        Task FinishTheGameAsync(string code);
        Task RegisterTeamsAsync(IEnumerable<Team> teams);
        Task MoveThePaddleAsync(string code, float move);
    }
}