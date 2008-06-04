﻿using System;
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
                submittedDate = DateTime.Now
            };

            RedditCloneDataContext dc = new RedditCloneDataContext();
            dc.Articles.InsertOnSubmit(arr);
            dc.SubmitChanges();
            
        }

        public void DeleteArticle(int id)
        {
            RedditCloneDataContext dc = new RedditCloneDataContext();
            var arr = from p in dc.Articles
                      where p.id == id 
                      select p;

            dc.Articles.DeleteAllOnSubmit(arr);
            dc.SubmitChanges();

        }

        //public void CastUpVote(int id)
        //{
        //    RedditCloneDataContext dc = new RedditCloneDataContext();
        //    Article article = dc.Articles.Single<Article>(a => a.id == id);
        //    article.UpVotes++;
        //    dc.SubmitChanges();
        //}


        public Article GetArticleID(int id)
        {
            RedditCloneDataContext dc = new RedditCloneDataContext();
            return dc.Articles.Single<Article>(a => a.id == id);
        }
        public List<Article> SearchURL(string url)
        {
            RedditCloneDataContext dc = new RedditCloneDataContext();

            var articles = from p in dc.Articles
                           where p.URL == url
                           select p;

            return articles.ToList<Article>();
        }

        public VoteHistory GetLatestVoteHistory(string url, string voter)
        {
            RedditCloneDataContext dc = new RedditCloneDataContext();

            var articleIDs = from vH in dc.Articles
                            where vH.URL==url 
                            orderby vH.submittedDate descending
                            select vH;

            List<Article> articleIDList = articleIDs.ToList<Article>();

            var vHistory = from vH in dc.VoteHistories
                           where vH.articleID == articleIDList[0].id &&
                           vH.diggers == voter
                           select vH;

            return vHistory.ToList<VoteHistory>()[0];

       //     var hHistory = from vH in dc.

       //     var vHistory = from vH in dc.
        }
    }
}
