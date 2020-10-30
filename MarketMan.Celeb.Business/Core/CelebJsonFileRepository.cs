using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MarketMan.Celeb.Business.Model;
using MarketMan.Celeb.Business.Utils;
using Microsoft.Extensions.Logging;

namespace MarketMan.Celeb.Business.Core
{
    public class CelebJsonFileRepository : IRepository
    {

        
        private readonly ILogger _logger;

        internal List<CelebInfo> Items
        {
            set;
            get;
        }

        public CelebJsonFileRepository(ILogger<CelebJsonFileRepository> logger)
        {
            this._logger = logger;
            Items = new List<CelebInfo>();
            Load(); //try to load on the singeltion initialization
        }

        public void Add(CelebInfo celeb)
        {
            this.Items.Add(celeb);
        }

        public List<CelebInfo> GetAll()
        {
            return Items;
        }


        public async void Save()
        {
            //if (File.Exists(ConfigUtil.JSON_PATH)) File.Delete(ConfigUtil.JSON_PATH); 
            using FileStream writer = new FileStream(ConfigUtil.JSON_PATH, FileMode.Create);
            await System.Text.Json.JsonSerializer.SerializeAsync(writer, this.Items, new System.Text.Json.JsonSerializerOptions() { WriteIndented = true });
            _logger.LogInformation($"IdbmRepository saved");
        }

        public async void Load()
        {
            try
            {
                if (File.Exists(ConfigUtil.JSON_PATH))
                {
                    using FileStream reader = new FileStream(ConfigUtil.JSON_PATH, FileMode.Open);
                    this.Items = await System.Text.Json.JsonSerializer.DeserializeAsync<List<CelebInfo>>(reader);
                    _logger.LogInformation($"IdbmRepository loaded with {this.Items.Count} items from cached file");
                }
            }
            catch(Exception ex)
            {
                //maybe file corrupted or something... just log it 
            }

        }

        public void DeleteCeleb(int key)
        {             
            var celeb = this.Items.FirstOrDefault(c => c.Key == key);
            if (celeb != null)
            {
                _logger.LogInformation($"Removing celeb {celeb.Key} from IdbmRepository..");
                this.Items.Remove(celeb);                 
                this.Save();
            }
        }

        public void Reset()
        {
            this.Items = new List<CelebInfo>();
        }
    }
}
