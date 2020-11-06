using MarketMan.Celeb.Entities;
using System.Collections.Generic;

namespace MarketMan.Celeb.Business
{
    public interface IRepository
    {
         
        void Add(CelebInfo celeb);
        void Load();
        void Save();
        List<CelebInfo> GetAll();
        void DeleteCeleb(int key);
        void Reset();
    }
}