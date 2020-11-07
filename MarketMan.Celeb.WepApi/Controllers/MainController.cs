using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketMan.Celeb.Business;
using MarketMan.Celeb.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MarketMan.Celeb.WepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IScrapEngine _engine;
        private readonly IRepository _repo;

        public MainController(IRepository repo, IScrapEngine engine, ILogger<MainController> logger)
        {
            logger.LogInformation("Celeb main controller loaded");
            this._engine = engine;
            this._repo = repo;  
            
        }

        // GET: api/<MainController>
        [HttpGet]
        public async Task<IActionResult>  Get()
        {
            var items = await this._repo.GetAll() ;
            return Ok(items);
        }
 
        // POST api/<MainController>
        [HttpPost]
        public ActionResult Post()
        {
            try
            {
                this._engine.GoScrap();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }

         

        // DELETE api/<MainController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this._repo.DeleteCeleb(id);
        }
    }
}
