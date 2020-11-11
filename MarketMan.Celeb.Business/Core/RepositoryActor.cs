using Akka.Actor;
using MarketMan.Celeb.Entities;
using Microsoft.Extensions.Logging;
using System;

namespace MarketMan.Celeb.Business.Core
{

    public enum RepoAction
    {
        Add,
        Get,
        Save,
        Delete,
        Reset
    }

    public class RepoCommand
    {
        public RepoAction Action { get; set; }
        public int? CelebKey { get; set; } //relevant for Add Action         
        public CelebInfo? Item { get; set; }//relevant for Delete Action
    }


    public class RepositoryActor :  UntypedActor
    {

        protected override void PreStart() => Console.WriteLine($"Starting Actor");

        protected override void PostStop() => Console.WriteLine($"Stopping Actor");

        private readonly CelebJsonFileRepository _repo;

        public RepositoryActor(ILogger<CelebJsonFileRepository> logger, CelebJsonFileRepository celebJsonRepo)
        {
            System.Diagnostics.Trace.WriteLine("*** Init RepositoryActor() ***");
            this._repo = celebJsonRepo;             
        }

        protected override void OnReceive(object message)
        {
            var command = message as RepoCommand;
            if (command == null) return;
            switch (command.Action)
            {
                case RepoAction.Add:
                    _repo.Add(command.Item);
                    break;
                case RepoAction.Save:
                    _repo.Save();
                    break;
                case RepoAction.Delete:
                    _repo.DeleteCeleb(command.CelebKey ?? 0);
                    break;
                case RepoAction.Get:
                    Sender.Tell(_repo.GetAll());
                    break;
            }

        }
    }
}
