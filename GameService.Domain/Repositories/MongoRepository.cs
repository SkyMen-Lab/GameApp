using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using GameService.Domain.Configs;
using GameService.Domain.Models;
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

        public List<Game> GetAll() => _games.Find(game => true).ToList();
        public List<Game> GetList(Expression<Func<Game, bool>> exp) => _games.Find(exp).ToList();

        public Game GetOne(string code) => _games.Find(game => string.Equals(game.Code, code)).FirstOrDefault();

        public Game CreateOne(Game game)
        {
            var code = game.Code;
            //TODO: custom exceptions
            if (GetOne(code) != null) throw new ArgumentException();
            try
            {
                _games.InsertOne(game);
            }
            catch (MongoWriteException me)
            {
                Console.WriteLine(me);
                return null;
            }

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