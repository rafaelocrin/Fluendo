using Fluendo.FluendoPlatform.Infrastructure.Common.Config;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fluendo.FluendoPlatform.StatsService.Persistence.Repositories
{
    public class LeaderboardRepository : BaseRepository, ILeaderboardRepository
    {
        public LeaderboardRepository(MongoClient dbClient, IOptions<ApplicationOptions> appOptions): base (dbClient, appOptions)
        {
        }

        public int Update(string leaderboardTopList)
        {
            int ret = -1;

            try
            {
                var leaderboardCol = Database.GetCollection<BsonDocument>("Leaderboard");

                var leaderboardTopListDoc = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(leaderboardTopList);

                leaderboardCol.InsertOne(leaderboardTopListDoc);

                ret = 0; // OK
            }
            catch (Exception ex)
            {
                throw new Exception("Error when trying to udpate into Leaderboard collection");
            }

            return ret;
        }
    }
}
