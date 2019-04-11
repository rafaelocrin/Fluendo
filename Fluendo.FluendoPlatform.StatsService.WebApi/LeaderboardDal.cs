using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fluendo.FluendoPlatform.StatsService.WebApi
{
    public class LeaderboardDal
    {
        public void Update(string leaderboardTopList)
        {
            var connectionString = "mongodb://localhost";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("local");
            var leaderboardCol = database.GetCollection<BsonDocument>("Leaderboard");


            BsonDocument personDoc = new BsonDocument();
            MongoDB.Bson.BsonDocument leaderboardTopListDoc = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(leaderboardTopList);

            leaderboardCol.InsertOne(leaderboardTopListDoc);
        }
    }
}
