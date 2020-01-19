using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GameService.Domain.Models
{
    public class Team
    {
        private float _paddleSpeed;
        [BsonElement("code")]
        [BsonRepresentation(BsonType.String)]
        public string Code { get; set; }

        [BsonElement("name")]
        [BsonRepresentation(BsonType.String)]
        public string Name { get; set; }

        [BsonRepresentation(BsonType.Int32)]
        [BsonElement("numberOfPlayers")]
        public int NumberOfPlayers { get; set; }
        [BsonElement("paddleSpeed")]
        public float PaddleSpeed
        {
            get => 10 * (float) Constant;
            set => _paddleSpeed = value;
        }
        
        [BsonElement("puddlePosition")]
        public Position PuddlePosition { get; set; }

        [BsonElement("constant")]
        [BsonRepresentation(BsonType.Double)]
        public double Constant { get; set; } = 1;

        [BsonElement("score")]
        [BsonRepresentation(BsonType.Int32)]
        public int Score { get; set; } = 0;

        [BsonRepresentation(BsonType.String)]
        [BsonElement("ip")]
        public string RouterIp { get; set; }
    }
}