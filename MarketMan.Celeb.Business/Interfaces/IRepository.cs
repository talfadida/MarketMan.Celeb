using MarketMan.Celeb.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarketMan.Celeb.Business
{
    public interface IRepository
    {
         
        void Add(CelebInfo celeb);         
        void Save();
        Task<List<CelebInfo>> GetAll();
        void DeleteCeleb(int key);
        void Reset();
    }
}