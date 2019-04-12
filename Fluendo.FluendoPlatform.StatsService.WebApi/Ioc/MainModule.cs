using Autofac;
using Fluendo.FluendoPlatform.Infrastructure.Common;
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
        }

    }
}
