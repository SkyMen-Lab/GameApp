using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            await _tcpManager.SendPacketAsync(new Packet(Meta.Message, "start"));
        }

        public async Task FinishTheGameAsync(string code)
        {
            await _tcpManager.SendPacketAsync(new Packet(Meta.Message, "finish"));
        }

        public async Task SetupTeamsAsync(IEnumerable<Team> teams)
        {
            var message = JsonConvert.SerializeObject(teams);
            await _tcpManager.SendPacketAsync(new Packet(Meta.Connect, message));
        }

        public async Task MoveThePaddleAsync(string code, int clicks)
        {
            float mov = clicks * 15.6f;
            await _tcpManager.SendPacketAsync(new Packet(Meta.Message, $"{mov} {code}"));
        }
    }
}