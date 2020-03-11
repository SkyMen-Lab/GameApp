using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameService.Domain.DTOs;
using GameService.Domain.Models;
using GameService.TCP.EventHandling;
using GameService.TCP.Models;
using Newtonsoft.Json;

namespace GameService.TCP
{
    public class GameManager : IGameManager
    {
        private readonly ITcpManager _tcpManager;
        
        public GameManager(ITcpManager tcpManager)
        {
            _tcpManager = tcpManager;
        }

        public async Task StartTheGameAsync(string code)
        {
            await _tcpManager.SendPacketAsync(new Packet(Meta.Message, 
                new Message(GameAction.StartGame, "start").ToJson()));
        }

        public async Task FinishTheGameAsync(string code)
        {
            await _tcpManager.SendPacketAsync(new Packet(Meta.Message, 
                new Message(GameAction.FinishGame, "finish").ToJson()));
        }

        public async Task SetupTeamsAsync(IEnumerable<Team> teams)
        {
            var message = new Message(GameAction.InitTeams,JsonConvert.SerializeObject(teams));
            var packet = new Packet(Meta.Connect, message.ToJson());
            await _tcpManager.SendPacketAsync(packet);
        }

        public async Task MoveThePaddleAsync(string code, int clicks)
        {
            float mov = clicks * 15.6f;
            var message = new Message(GameAction.Movement, $"{mov} {code}");
            await _tcpManager.SendPacketAsync(new Packet(Meta.Message, message.ToJson()));
        }

        public async Task UpdateNumberOfPlayers(UpdateNumberOfPlayersDTO dto)
        {
            var message = JsonConvert.SerializeObject(dto);
            await _tcpManager.SendPacketAsync(new Packet(Meta.Message, message));
        }
    }
}