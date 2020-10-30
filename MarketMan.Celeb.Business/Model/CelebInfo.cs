using HtmlAgilityPack;
using System;
using System.Text;

namespace MarketMan.Celeb.Business.Model
{

    public class CelebInfo
    {
        public int Key { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string ImageUrl { get; set; }
        public string Gender { get; set; }
        public string BirthDate { get; set; }

         

        public CelebInfo()
        {
             
        }

        
    }
}
