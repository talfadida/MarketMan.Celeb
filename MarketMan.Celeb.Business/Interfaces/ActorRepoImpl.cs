using Akka.Actor;
using MarketMan.Celeb.Business.Core;
using MarketMan.Celeb.Business.Utils;
using MarketMan.Celeb.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarketMan.Celeb.Business
{

    public delegate IActorRef ActorProvider();

    public class ActorRepoImpl : IRepository
    {
        private readonly IActorRef _repositoryActor; 

        public ActorRepoImpl(ActorProvider repoActorProvider)
        {
            _repositoryActor = repoActorProvider();
        }


        public void Add(CelebInfo celeb)
        {
            _repositoryActor.Tell(new RepoCommand() { Action = RepoAction.Add, Item = celeb });
        }

        public void DeleteCeleb(int key)
        {
            _repositoryActor.Tell(new RepoCommand() { Action = RepoAction.Delete, CelebKey = key });                  
        }

        public async Task<List<CelebInfo>> GetAll()
        {
            return await _repositoryActor.Ask<List<CelebInfo>>(new RepoCommand() { Action = RepoAction.Get });
        }
 

        public void Reset()
        {
            _repositoryActor.Tell(new RepoCommand() { Action = RepoAction.Reset }); 
        }

        public void Save()
        {
            _repositoryActor.Tell(new RepoCommand() { Action = RepoAction.Save });
        }
    }
}