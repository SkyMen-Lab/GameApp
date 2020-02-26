using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GameService.Domain.DTOs;
using GameService.Domain.Models;
using GameService.Domain.Repositories;
using GameService.TCP;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using Serilog;

namespace GameApp.Controllers
{
    [ApiController]
    [Route("/v1a/")]
    public class MainController : Controller
    {

        private readonly MongoRepository _mongoRepository;
        private readonly IGameManager _gameManager;

        public MainController(MongoRepository mongoRepository, IGameManager gameManager)
        {
            _mongoRepository = mongoRepository;
            _gameManager = gameManager;
        }

        // GET
        public IActionResult Index()
        {
            Log.Information("Loading index");
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
            Log.Information($"Requesting list #{page}");
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
                Log.Information($"Attempt to create game with code {game.Code}");
                _mongoRepository.CreateOne(game);
            }
            catch (MongoWriteException e)
            {
                Log.Warning(e,$"Error occured while creating new game {game.Code}");
                return BadRequest(e);
            }


            _gameManager.SetupTeamsAsync(game.Teams);
            
            Log.Information($"The game {game.Code} has been created successfully");
            return Ok();
        }

        [HttpPost("start")]
        public async Task<IActionResult> StartGameAsync([FromBody] string code)
        {
            if (_mongoRepository.GetOne(code) != null)
                await _gameManager.StartTheGameAsync(code);
            else
                return NotFound();
            
            return Ok();
        }
        
        //TOBE replaced by Event Bus
        [HttpPost("user_joined")]
        public IActionResult UserJoined([FromBody] UserDTO info)
        {
            Log.Information("Attempting to register new player");
            var result = ChangeNumberOfUsers(info, UserAction.Left);

            if (!result)
            {
                return BadRequest();
            }
            Log.Information($"New player has successfully joined {info.SchoolCode}");

            return Ok();
        }

        [HttpPost("user_left")]
        public IActionResult UserLeft([FromBody] UserDTO userLeft)
        {
            Log.Information("Attempting to remove a player from the game");

            var result = ChangeNumberOfUsers(userLeft, UserAction.Left);

            if (!result)
            {
                return BadRequest();
            }
            
            Log.Information($"The player has successfully been removed {userLeft.SchoolCode}");

            return Ok();
        }

        private bool ChangeNumberOfUsers(UserDTO info, UserAction userAction)
        {
            var currentGame = _mongoRepository.GetOne(info.GameCode);
            if (currentGame == null)
            {
                Log.Warning($"The game {info.GameCode} does not exist");
                return false;
            }

            var currentTeam = currentGame.Teams.FirstOrDefault(x => string.Equals(x.Code, info.SchoolCode));
            if (currentTeam == null)
            {
                Log.Warning($"The team {info.SchoolCode} does not exist");
                return false;
            }
            
            //find a team in a game by codes
            Expression<Func<Game, bool>> filter = x => string.Equals(x.Code, info.GameCode) && x.Teams.Any(y => string.Equals(y.Code, info.SchoolCode));

            //update number of players
            switch (userAction)
            {
                case UserAction.Left:
                    currentTeam.NumberOfPlayers--;
                    break;
                case UserAction.Joined:
                    currentTeam.NumberOfPlayers++;
                    break;
            }
            
            UpdateDefinition<Game> updatePlayersDefinition = Builders<Game>.Update.Set(
                x => x.Teams[-1].NumberOfPlayers, currentTeam.NumberOfPlayers);

            //recalculate constant
            var constant = 1.0 / currentTeam.NumberOfPlayers;
            UpdateDefinition<Game> updateConstantDefinition = Builders<Game>.Update.Set(
                x => x.Teams[-1].Constant, constant);

            _mongoRepository.Update(filter, updatePlayersDefinition);
            _mongoRepository.Update(filter, updateConstantDefinition);
            return true;
        }

        enum UserAction
        {
            Left,
            Joined
        }
    }
}