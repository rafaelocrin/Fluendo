using Fluendo.FluendoPlatform.Infrastructure.Common.Config;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fluendo.FluendoPlatform.StatsService.Persistence.Repositories
{
    public class BaseRepository
    {
        protected IOptions<ApplicationOptions> AppOptions;
        protected MongoClient DbClient;
        protected IMongoDatabase Database;

        public BaseRepository(MongoClient dbClient, IOptions<ApplicationOptions> appOptions)
        {
            AppOptions = appOptions;
            DbClient = dbClient;

            Database = DbClient.GetDatabase(AppOptions.Value.ConnectionString["Database"]);
        }
    }
}
