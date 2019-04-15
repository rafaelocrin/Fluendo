using Fluendo.FluendoPlatform.Infrastructure.Common.Config;
using Fluendo.FluendoPlatform.Infrastructure.Resources;
using Microsoft.Extensions.Localization;
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
        private readonly IStringLocalizer<SharedResource> _resources;

        public LeaderboardRepository(MongoClient dbClient, IOptions<ApplicationOptions> appOptions, 
                                    IStringLocalizer<SharedResource> resources) : base (dbClient, appOptions)
        {
            _resources = resources;
        }

        public int Update(string leaderboardTopList)
        {
            int ret = -1;

            try
            {
                var leaderboardCol = Database.GetCollection<BsonDocument>(_resources["Repository_Database_Leaderboard"]);

                var leaderboardTopListDoc = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(leaderboardTopList);

                leaderboardCol.InsertOne(leaderboardTopListDoc);

                ret = 0; // OK

                throw new Exception();
            }
            catch (Exception ex)
            {
                throw new Exception(_resources["Error_Repository_Update_Leaderboard"]);
            }

            return ret;
        }
    }
}
