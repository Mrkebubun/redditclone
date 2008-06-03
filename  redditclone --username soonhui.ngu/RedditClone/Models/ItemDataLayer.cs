using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedditClone.Models
{

    public class ItemFactory
    {
        public List<Article> GetHotArticles()
        {
            RedditCloneDataContext dc = new RedditCloneDataContext();
            return dc.Articles.ToList<Article>();
        }

        public List<Article> GetNewestArticles()
        {
            RedditCloneDataContext dc = new RedditCloneDataContext();

            var articles = from p in dc.Articles
                           orderby p.submittedDate descending
                           select p;
            return articles.ToList<Article>();
                     
        }

        public void SubmitArticle(string url, string title, string owner)
        {
            Article arr = new Article()
            {
                URL = url,
                Title = title,
                Diggers = owner,
                DownVotes = 0,
                UpVotes = 1,
                submittedDate = DateTime.Now
            };

            RedditCloneDataContext dc = new RedditCloneDataContext();
            dc.Articles.InsertOnSubmit(arr);
            dc.SubmitChanges();
            
        }
        public List<Article> SearchURL(string url)
        {
            RedditCloneDataContext dc = new RedditCloneDataContext();

            var articles = from p in dc.Articles
                           where p.URL == url
                           select p;

            return articles.ToList<Article>();
        }
    }
}
