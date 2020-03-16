using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using GameService.Domain.DTOs;
using GameService.Domain.Models;
using GameService.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace GameService.TCP.Events
{
    public class GameFinishedEvent : EventBase<GameFinishedEventArgs>
    {
        private GameFinishedEventArgs _args;

        private MongoRepository _mongoRepository;
        private ITcpManager _tcpManager;

        protected override GameFinishedEventArgs Args
        {
            get => _args;
            set => _args = value;
        }

        public override void ResolveDependencies(IServiceProvider serviceProvider)
        {
            _mongoRepository = serviceProvider.GetRequiredService<MongoRepository>();
            _tcpManager = serviceProvider.GetRequiredService<ITcpManager>();
        }

        public override async Task Execute()
        {
            var game = _mongoRepository.GetOne(_args.GameCode);
            if (game != null)
            {
                //make request to StorageService to upload the summary
                var client = new HttpClient();
                client.BaseAddress = new Uri("https://127.0.0.1:5008");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var summary = new GameSummaryDTO()
                {
                    WinnerCode = _args.WinnerCode,
                    Teams = _args.Teams,
                    GameCode = _args.GameCode,
                    MaxSpeedLevel = _args.MaxSpeedLevel
                };
                
                var content = new StringContent(JsonConvert.SerializeObject(summary));
                var response = await client.PostAsync("https://127.0.0.1:5008/api/game/finish", content);
                if (response.IsSuccessStatusCode)
                {
                    //delete a game from db to allow
                    _mongoRepository.Delete(_args.GameCode);
                }
                // close connection with clients
                // to be implemented
                //await _tcpManager.DisconnectAsync();
            }
        }
    }

    public class GameFinishedEventArgs : EventArgs
    {
        public string GameCode { get; set; }
        public List<Team> Teams { get; set; }
        public string WinnerCode { get; set; }
        public int MaxSpeedLevel { get; set; }

        public GameFinishedEventArgs(string gameCode, List<Team> teams, string winnerCode, int maxSpeedLevel)
        {
            GameCode = gameCode;
            Teams = teams;
            WinnerCode = winnerCode;
            MaxSpeedLevel = maxSpeedLevel;
        }

        public GameFinishedEventArgs(GameSummaryDTO dto)
        {
            GameCode = dto.GameCode;
            Teams = dto.Teams;
            WinnerCode = dto.WinnerCode;
            MaxSpeedLevel = dto.MaxSpeedLevel;
        }
    }
}