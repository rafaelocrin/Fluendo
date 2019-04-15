using Autofac;
using Autofac.Core;
using Fluendo.FluendoPlatform.Infrastructure.Common;
using Fluendo.FluendoPlatform.Infrastructure.Common.Config;
using Fluendo.FluendoPlatform.StatsService.Persistence.Repositories;
using Fluendo.FluendoPlatform.StatsService.Services;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutofacModule = Autofac.Module;

namespace Fluendo.FluendoPlatform.StatsService.WebApi.Ioc
{
    public class MainModule : AutofacModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HttpUtility>()
            .As<IHttpUtility>()
            .InstancePerLifetimeScope();

            builder.RegisterType<MongoClient>()
            .SingleInstance()
            .WithParameter(new ResolvedParameter(
                (pi, ctx) => pi.ParameterType == typeof(MongoClientSettings) && pi.Name == "connectionString",
                (pi, ctx) =>
                {
                    var config = ctx.Resolve<IOptions<ApplicationOptions>>();
                    return config.Value.ConnectionString;
                }));

            builder.RegisterType<LeaderboardRepository>()
            .As<ILeaderboardRepository>()
            .InstancePerLifetimeScope();


            builder.RegisterType<LeaderboardService>()
            .As<ILeaderboardService>()
            .InstancePerLifetimeScope();

            builder.RegisterType<PlayerStatsRepository>()
            .As<IPlayerStatsRepository>()
            .InstancePerLifetimeScope();


            builder.RegisterType<PlayerStatsService>()
            .As<IPlayerStatsService>()
            .InstancePerLifetimeScope();

        }

    }
}
