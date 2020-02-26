using System.Collections.Generic;
using System.Threading.Tasks;
using GameService.Domain.Models;

namespace GameService.TCP
{
    public interface IGameManager
    {
        Task StartTheGameAsync(string code);
        Task FinishTheGameAsync(string code);
        Task SetupTeamsAsync(IEnumerable<Team> teams);
        Task MoveThePaddleAsync(string code, int clicks);
    }
}