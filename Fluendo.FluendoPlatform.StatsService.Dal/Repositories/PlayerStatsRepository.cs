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
    public class PlayerStatsRepository : BaseRepository, IPlayerStatsRepository
    {
        private readonly IStringLocalizer<SharedResource> _resources;

        public PlayerStatsRepository(MongoClient dbClient, IOptions<ApplicationOptions> appOptions,
                                    IStringLocalizer<SharedResource> resource) : base(dbClient, appOptions)
        {
            _resources = resource;
        }

        public int Update(string playerStats)
        {
            int ret = -1;

            try
            {
                var leaderboardCol = Database.GetCollection<BsonDocument>(_resources["Repository_Database_PlayerStats"]);

                var playerStatsDoc = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(playerStats);

                leaderboardCol.InsertOne(playerStatsDoc);

                ret = 0; // OK
            }
            catch (Exception ex)
            {
                throw new Exception(_resources["Error_Repository_Update_PlayerStats"]);
            }

            return ret;
        }
    }
}
