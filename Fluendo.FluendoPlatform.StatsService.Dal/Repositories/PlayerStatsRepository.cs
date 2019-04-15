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
    public class PlayerStatsRepository : BaseRepository, IPlayerStatsRepository
    {
        public PlayerStatsRepository(MongoClient dbClient, IOptions<ApplicationOptions> appOptions) : base(dbClient, appOptions)
        {
        }

        public int Update(string playerStats)
        {
            int ret = -1;

            try
            {
                var leaderboardCol = Database.GetCollection<BsonDocument>("PlayerStats");

                var playerStatsDoc = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(playerStats);

                leaderboardCol.InsertOne(playerStatsDoc);

                ret = 0; // OK
            }
            catch (Exception ex)
            {
                throw new Exception("Error when trying to udpate into PlayerStats collection");
            }

            return ret;
        }
    }
}
