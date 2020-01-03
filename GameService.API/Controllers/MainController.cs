using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GameService.Domain.DTOs;
using GameService.Domain.Models;
using GameService.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

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
            return Ok(
                _mongoRepository
                    .GetListSorted(x => true, 
                        new BsonDocumentSortDefinition<Game>(new BsonDocument("$natural", -1)))
                    .ToList()
            );
        }
        
        [HttpGet("list/{page:int}")]
        public IActionResult GetList(int page = 1)
        {
            if (page < 1) return BadRequest();
            var list = _mongoRepository
                .GetListSorted(x => true,
                    new BsonDocumentSortDefinition<Game>(new BsonDocument("$natural", -1)))
                .Skip((page - 1) * 10)
                .Take(10)
                .ToList();
            return Ok(list);
        }

        [HttpPost("create")]
        
        public IActionResult Create([FromBody]Game game)
        {
            var existingGame = _mongoRepository.GetOne(game.Code);
            if (existingGame != null) return Conflict();

            try
            {
                _mongoRepository.CreateOne(game);
            }
            catch (MongoWriteException e)
            {
                return BadRequest(e);
            }
            
            //TODO: set messages to frontend to start
            
            return Ok();
        }

        //TOBE replaced by Event Bus
        [HttpPost("user_joined")]
        public IActionResult UserJoined([FromBody] UserJoinedDTO info)
        {
            var currentGame = _mongoRepository.GetOne(info.GameCode);
            if (currentGame == null) return BadRequest();

            var currentTeam = currentGame.Teams.FirstOrDefault(x => string.Equals(x.Code, info.SchoolCode));
            if (currentTeam == null) return BadRequest();

            currentTeam.NumberOfPlayers++;

            Expression<Func<Game, bool>> filter = x => string.Equals(x.Code, info.GameCode) && x.Teams.Any(y => string.Equals(y.Code, info.SchoolCode));

            UpdateDefinition<Game> updateDefinition = Builders<Game>.Update.Set(
                x => x.Teams[-1].NumberOfPlayers, currentTeam.NumberOfPlayers);

            //TODO: recalculate constant

            _mongoRepository.Update(filter, updateDefinition);

            return Ok();
        }
    }
}