using System;
using Autofac;
using Dockka.Config;
using Dockka.Data.Context;
using Dockka.Data.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Dockka.Actors.Common
{
    public static class DependencyInjection
    {
        public static ContainerBuilder ConfigureDependencyInjection()
        {
            var container = new ContainerBuilder();

            var options = new DbContextOptionsBuilder<DockkaContext>()
                .UseSqlServer(Configuration.DockkaSqlConnection)
                .WaitForActiveServer(TimeSpan.FromSeconds(300))
                .Options;

            container.RegisterInstance(options).AsSelf().SingleInstance();
            container.RegisterType<DockkaContext>().AsSelf().InstancePerLifetimeScope();

            return container;
        }
    }
}
