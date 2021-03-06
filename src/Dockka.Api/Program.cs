﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Cluster.Tools.PublishSubscribe;
using Akka.Configuration;
using Akka.DI.AutoFac;
using Akka.DI.Core;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Dockka.Api.Actors;
using Dockka.Logging;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace Dockka.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Configure logging from Serilog to sink to Logstash
            // Logstash is an ingestor of logs, and inserts those to elasticsearch
            LogSetup.ConfigureLogger();
            // Elasticsearch can be viewed via kibana dashboard at localhost:5601
            try
            {
                Log.Information("Starting web host");
                var webHost = BuildWebHost(args);
                AkkaSystem = BuildAkkaCluster(webHost);
                webHost.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IActorRef Mediator { get; set; }

        public static ActorSystem AkkaSystem { get; set; }

        private static ActorSystem BuildAkkaCluster(IWebHost webHost)
        {
            var akkaConfFile = Environment.GetEnvironmentVariable("AKKA_CONF_FILENAME");
            // Build our Akka cluster, and associate it with the DI container
            // Start our seed node
            using (var reader = new StreamReader(File.OpenRead(akkaConfFile ?? "akka.conf")))
            {
                var serviceProvider = webHost.Services as AutofacServiceProvider;
                var config = ConfigurationFactory.ParseString(reader.ReadToEnd());
                var system = ActorSystem.Create("dockka-system", config);

                var lifetimeScope = serviceProvider.GetService<ILifetimeScope>();
                var propsResolver = new AutoFacDependencyResolver(lifetimeScope, system);
                Mediator = DistributedPubSub.Get(system).Mediator;
                system.ActorOf(system.DI().Props<UpdateUiActor>(), "updateUi");
                return system;
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(services => services.AddAutofac()) // Use autofac for Akka injection
                .UseStartup<Startup>()
                .Build();
    }
}
