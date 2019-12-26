using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using GameService.Domain.Configs;
using GameService.Domain.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GameService.Domain.Repositories
{
    public class MongoRepository : IMongoRepository<Game>
    {
        private readonly IMongoCollection<Game> _games;

        public MongoRepository(IDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var db = client.GetDatabase(settings.DatabaseName);
            _games = db.GetCollection<Game>(settings.CollectionName);
        }

        public IEnumerable<Game> GetAll() => _games.Find(game => true).ToEnumerable();
        public IEnumerable<Game> GetList(Expression<Func<Game, bool>> expression) => _games.Find(expression).ToEnumerable();
        
        public IEnumerable<Game> GetListSorted(Expression<Func<Game, bool>> exp, SortDefinition<Game> sortDefinition) => 
            _games
                .Find(exp)
                .Sort(sortDefinition)
                .ToEnumerable();

        public Game GetOne(string code) => _games.Find(game => string.Equals(game.Code, code)).FirstOrDefault();

        public Game CreateOne(Game game)
        {
            var code = game.Code;
            //TODO: custom exceptions
            if (GetOne(code) != null) throw new ArgumentException();
            _games.InsertOne(game);
            return game;
        }

        public bool Update(string code, Game game)
        {
            var result = _games.ReplaceOne(x => string.Equals(x.Code, code), game).IsAcknowledged;
            return result;
        }

        public bool Delete(string code)
        {
            var result = _games.DeleteOne(game => string.Equals(game.Code, code)).IsAcknowledged;
            return result;
        }
    }
}