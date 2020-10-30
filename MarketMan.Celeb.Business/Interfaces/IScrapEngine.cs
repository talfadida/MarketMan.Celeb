using MarketMan.Celeb.Business.Model;

namespace MarketMan.Celeb.Business
{
    public interface IScrapEngine
    {
        void GoScrap();
        CelebInfo Create(object oNode);
    }
}