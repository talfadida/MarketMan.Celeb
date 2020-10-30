using System;
using System.Collections.Generic;
using System.Text;

namespace MarketMan.Celeb.Business.Utils
{
    public class ConfigUtil
    {
        public static readonly string IMDB_BASE_URL = "https://www.imdb.com";
        public static readonly string IMDB_PAGE_LIST_URL = $@"{IMDB_BASE_URL}/list/ls052283250/";
        public static readonly string IMDB_XPATH_MAIN_LIST = @"/html/body/div[3]/div/div[2]/div[3]/div[1]/div/div[3]/div[3]";
        public static readonly string IMDB_XPATH_CELEB_IMAGEURL = "div[1]/a/img";
        public static readonly string IMDB_XPATH_CELEB_NAME = "div[2]/h3/a";
        public static readonly string IMDB_XPATH_CELEB_ROLE = "div[2]/p";
        public static readonly string IMDB_XPATH_CELEB_BIRTHDATE = "//time";
        public static readonly string IMDB_XPATH_CELEB_PAGE = "div[2]/h3/a";

        public static readonly string JSON_PATH = @"c:\temp\celeb\imdb.json";
        public static readonly int TOP = 10;
    }
}
