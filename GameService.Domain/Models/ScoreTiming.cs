using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GameService.Domain.Models
{
    public class ScoreTiming
    {
        [BsonElement("time")]
        public MongoDB.Bson.BsonDateTime Time { get; set; }
        [BsonElement("team")]
        [BsonRepresentation(BsonType.String)]
        public string TeamCode { get; set; }
    }
}