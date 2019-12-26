using System;
using System.Collections.Generic;
using GameService.Domain.Models;
using GameService.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace GameApp.Controllers
{
    [ApiController]
    [Route("/v1a/")]
    public class MainController : Controller
    {

        private readonly MongoRepository _mongoRepository;

        public MainController(MongoRepository mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        // GET
        public IActionResult Index()
        {
            return Ok("Hello");
        }

        //TODO: replace by PUT method
        [Route("create")]
        public IActionResult Create()
        {
            var game = _mongoRepository.CreateOne(new Game
            {
                Code = "newcode3",
                //Duration = 10,
                Teams = new List<Team>
                {
                    new Team
                    {
                        Code = "345345",
                        Constant = 2.0,
                        Name = "Bedford School2",
                        NumberOfPlayers = 10,
                        RouterIp = "86.111.139.123",
                        Score = 0
                    },
                    new Team
                    {
                        Code = "35235",
                        Constant = 2.0,
                        Name = "Rugby School2",
                        NumberOfPlayers = 5,
                        RouterIp = "86.111.139.150",
                        Score = 0
                    },
                },
                ScoreTimings = new List<ScoreTiming>()
            });
            return Ok(game);
        }
    }
}