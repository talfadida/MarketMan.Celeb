using MarketMan.Celeb.Entities;

namespace MarketMan.Celeb.Business
{
    public interface IScrapEngine
    {
        void GoScrap();
        CelebInfo Create(object oNode);
    }
}