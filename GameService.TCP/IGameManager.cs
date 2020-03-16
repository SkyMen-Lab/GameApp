using System.Collections.Generic;
using System.Threading.Tasks;
using GameService.Domain.DTOs;
using GameService.Domain.Models;

namespace GameService.TCP
{
    public interface IGameManager
    {
        Task StartTheGameAsync(string code);
        Task FinishTheGameAsync(string code);
        Task SetupGameAsync(Game game);
        Task MoveThePaddleAsync(string code, int clicks);
        Task UpdateNumberOfPlayers(UpdateNumberOfPlayersDTO dto);
    }
}