using Autofac;
using Fluendo.FluendoPlatform.Core.App.Cache;
using Fluendo.FluendoPlatform.Core.App.Services;
using Fluendo.FluendoPlatform.Infrastructure.Common;
using AutofacModule = Autofac.Module;

namespace Fluendo.FluendoPlatform.Core.WebApi.Ioc
{
    public class MainModule : AutofacModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HttpUtility>()
            .As<IHttpUtility>()
            .InstancePerLifetimeScope();

            builder.RegisterType<RedisCache>()
            .As<IRedisCache>()
            .InstancePerLifetimeScope();

            builder.RegisterType<LeaderboardService>()
            .As<ILeaderboardService>()
            .InstancePerLifetimeScope();

            builder.RegisterType<PlayerStatsService>()
            .As<IPlayerStatsService>()
            .InstancePerLifetimeScope();
        }
    }
}
