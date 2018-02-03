using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;

namespace Henchman
{
    public class HenchmanDAO : IHenchmanDAO
    {
        private IMongoDatabase _database;

        public HenchmanDAO(IMongoClient client)
        {
            _database = client.GetDatabase("CloudFoundry_oumhg86d_nq48b1sc");
        }

        public List<Henchman> GetHenchmen()
        {
            var collection = _database.GetCollection<Henchman>("henchman");
            var filter = Builders<Henchman>.Filter.Empty;
            var result = collection.Find(filter).ToList();

            return result;
        }

        public Henchman GetHenchman(string name)
        {
            var collection = _database.GetCollection<Henchman>("henchman");
            var builder = Builders<Henchman>.Filter;
            var filter = builder.Where(m => m.Name == name);
            var result = collection.Find(filter).FirstOrDefault();

            return result;
        }

        public Henchman InsertHenchman(Henchman henchman)
        {
            var collection = _database.GetCollection<Henchman>("henchman");
            collection.InsertOne(henchman);

            return henchman;
        }

        public Henchman UpdateHenchman(Henchman henchman)
        {
            var collection = _database.GetCollection<Henchman>("henchman");
            var builder = Builders<Henchman>.Filter;
            var filter = builder.Where(m => m.Name == henchman.Name);
            var update = Builders<Henchman>.Update
                .Set("name", henchman.Name)
                .Set("edition", henchman.Edition)
                .Set("rules", henchman.Rules);
                
            collection.UpdateOne(filter, update);
            
            return henchman;
        }
    }
}