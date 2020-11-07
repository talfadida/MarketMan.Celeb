using Akka.Actor;
using Akka.DI.Core;
using Akka.DI.Extensions.DependencyInjection;
using MarketMan.Celeb.Business.Core;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace MarketMan.Celeb.Business.Utils
{

 

    public class _ActorSystemFactory
    {
        private readonly ActorSystem _actorSystem;
        private readonly ServiceProvider _serviceProvider;

        public  IActorRef RepoActor { get; private set; }



        public _ActorSystemFactory()
        {

            _actorSystem = ActorSystem.Create("RepositoryActor");
            RepoActor = _actorSystem.ActorOf<RepositoryActor>();

            //var serviceCollection =  new ServiceCollection();             
            ////serviceCollection.AddTransient<IRepository, ActorRepoImpl>();
            //serviceCollection.AddTransient<RepositoryActor>();
            //_serviceProvider = serviceCollection.BuildServiceProvider();
            //_actorSystem = ActorSystem.Create("engine");
            //_actorSystem.UseServiceProvider(_serviceProvider);
            //Props props = _actorSystem.DI().Props<RepositoryActor>();
            //RepoActor = _actorSystem.ActorOf(props, "RepositoryActor") as IActorRef;            

        }

        //internal static IActorRef GetSupervisorActor()
        //{
        //    Props props = _actorSystem.DI().Props<RootActor>();
        //    return _actorSystem.ActorOf(props, "DomainActorsParent") as IActorRef;
        //}

    }
}
