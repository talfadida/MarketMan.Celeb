using HtmlAgilityPack;
using MarketMan.Celeb.Entities;
using MarketMan.Celeb.Business.Utils;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;

namespace MarketMan.Celeb.Business.Core
{
    //moving to actor model
    public class ImdbScrapEngine: IScrapEngine
    {

        private readonly IActorRef _repositoryActor;// = ActorSystemFactory.RepoActor;
        private readonly ILogger _logger;
        //private readonly IRepository _repo;
        HtmlWeb web = new HtmlWeb();

        public ImdbScrapEngine(ActorProvider repoActorProvider, ILogger<ImdbScrapEngine> logger)
        {
            this._logger = logger;             
            this._repositoryActor = repoActorProvider();
        }

        public  void GoScrap()
        {
            _logger.LogInformation($"Starting Scrap process..");
            try
            {
                _repositoryActor.Tell(new RepoCommand() { Action = RepoAction.Reset });// this._repo.Reset();
                Stopwatch sw = Stopwatch.StartNew();
                HtmlWeb web = new HtmlWeb();
                var htmlDoc =  web.Load(ConfigUtil.IMDB_PAGE_LIST_URL);
                var celebListNode = htmlDoc.DocumentNode.SelectSingleNode(ConfigUtil.IMDB_XPATH_MAIN_LIST);
                                
                int count = 0;
                var topNodes = celebListNode.ChildNodes.Where(node => node.NodeType == HtmlNodeType.Element).Take(ConfigUtil.TOP);
                Parallel.ForEach<HtmlNode>(topNodes, node=>
                {                    
                    var celeb = Create(node);
                    if (celeb != null)
                    {
                        var currentCount = Interlocked.Increment(ref count);                           
                        celeb.Key = currentCount;
                        _repositoryActor.Tell(new RepoCommand() { Action = RepoAction.Add, Item = celeb });//  this._repo.Add(celeb);
                        _logger.LogInformation($"celeb #{currentCount} {celeb.Name} loaded");
                            
                    }

                });

                sw.Stop();
                _repositoryActor.Tell(new RepoCommand() { Action = RepoAction.Save });// his._repo.Save();
                _logger.LogInformation($"Scrap process finished successfully. It took {sw.Elapsed.TotalSeconds} seconds");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Scrapping process failed. exception: {ex}");
                throw ex;
            }

        }

        public  CelebInfo Create(object oNode)
        {

            HtmlNode node = oNode as HtmlNode;
            if (node == null)
                throw new Exception("Error; Are you sure that you are using the HtmlAgilityPack?");

            try
            {
                CelebInfo celeb = new CelebInfo()
                {

                    Name = node.SelectSingleNode(ConfigUtil.IMDB_XPATH_CELEB_NAME).InnerText.Trim(),
                    ImageUrl = node.SelectSingleNode(ConfigUtil.IMDB_XPATH_CELEB_IMAGEURL).Attributes["src"].Value,
                    Role = node.SelectSingleNode(ConfigUtil.IMDB_XPATH_CELEB_ROLE).InnerText.Trim().Split("|", StringSplitOptions.RemoveEmptyEntries)[0].Trim(),
                };

                celeb.Gender = ParseGedner(celeb.Role);
                celeb.BirthDate =  GetBirthDateFromInnerWebCall(node);

                return celeb;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Couldnt create CelebInfo object. Bypassing.. Source HTML: {node?.OuterHtml} Exception: {ex} ");
                return null;
            }
        }

        /// going public for unit testing purpose; prefer to use 
        public virtual string GetBirthDateFromInnerWebCall(HtmlNode node)
        {
            try
            {
                var page = node.SelectSingleNode(ConfigUtil.IMDB_XPATH_CELEB_PAGE).Attributes["href"].Value;
                var celebPage = web.Load(ConfigUtil.IMDB_BASE_URL + page);
                return celebPage.DocumentNode.SelectSingleNode(ConfigUtil.IMDB_XPATH_CELEB_BIRTHDATE).Attributes["datetime"].Value;
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Couldnt extract birthdate; exception: {ex}");
                return "Unknown";
            }
        }

        private  string ParseGedner(string role)
        {
            if (role == "Actress") return "Female";
            if (role == "Actor") return "Male";
            return "Unknown";
        }
    }
}
