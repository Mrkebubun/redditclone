using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedditClone.Models
{

    public class ItemFactory
    {
        public List<Article> GetArticle()
        {
            RedditCloneDataContext dc = new RedditCloneDataContext();
            return dc.Articles.ToList<Article>();
        }
    }
}
