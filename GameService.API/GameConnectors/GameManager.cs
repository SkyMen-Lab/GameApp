using System.Collections.Generic;
using System.Threading.Tasks;
using GameService.Domain.Models;

namespace GameApp.GameConnectors
{
    public class GameManager : IGameManager
    {
        public IGameManager.SendMessageDelegate SendMessageHandler { get; set; }
        public async Task StartTheGameAsync(string code)
        {
            await SendMessageHandler("start");
        }

        public Task FinishTheGameAsync(string code)
        {
            throw new System.NotImplementedException();
        }

        public Task RegisterTeamsAsync(IEnumerable<Team> teams)
        {
            throw new System.NotImplementedException();
        }

        public Task MoveThePaddleAsync(string code, float move)
        {
            throw new System.NotImplementedException();
        }
    }
}