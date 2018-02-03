using MongoDB.Bson.Serialization.Attributes;

namespace Henchman
{
    public partial class Rule
    {
        [BsonElement("deck")]
        public string Deck { get; set; }
        
        [BsonElement("type")]
        public string Type { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("amount")]
        public int Amount { get; set; }
    }
}