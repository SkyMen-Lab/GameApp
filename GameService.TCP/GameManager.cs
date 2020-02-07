using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameService.Domain.Models;
using GameService.TCP.EventArgs;
using Newtonsoft.Json;

namespace GameService.TCP
{
    public class GameManager : IGameManager
    {
        private readonly ITcpManager _tcpManager;
        public event EventHandler<MovementReceivedEventArgs> OnMovementReceivedEvent;

        public GameManager(ITcpManager tcpManager)
        {
            _tcpManager = tcpManager;
            IEventDisposer eventDisposer = tcpManager;
            eventDisposer.OnMovementReceivedEvent += OnOnMovementReceivedEvent;
        }

        private async void OnOnMovementReceivedEvent(object? sender, MovementReceivedEventArgs e)
        {
            await MoveThePaddleAsync(e.Code, e.Clicks);
        }

        public async Task StartTheGameAsync(string code)
        {
            await _tcpManager.SendMessageAsync("start");
        }

        public Task FinishTheGameAsync(string code)
        {
            throw new System.NotImplementedException();
        }

        public async Task RegisterTeamsAsync(IEnumerable<Team> teams)
        {
            var message = JsonConvert.SerializeObject(teams);
            await _tcpManager.SendMessageAsync(message);
        }

        public async Task MoveThePaddleAsync(string code, int clicks)
        {
            float mov = clicks * 15.6f;
            await _tcpManager.SendMessageAsync($"{mov} {code}");
        }
    }
}