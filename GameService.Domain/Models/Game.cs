using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GameService.Domain.Models
{
    public class Game
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("code")]
        [BsonRepresentation(BsonType.String)]
        public string Code { get; set; }
        [BsonElement("duration")]
        [BsonRepresentation(BsonType.Int32)]
        public int Duration { get; set; }
        [BsonElement("teams")]
        public List<Team> Teams { get; set; }
        [BsonElement("scoreTimings")]
        public List<ScoreTiming> ScoreTimings { get; set; }
    }
}