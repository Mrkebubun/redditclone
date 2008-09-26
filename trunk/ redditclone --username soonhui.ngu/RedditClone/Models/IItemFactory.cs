using System;
namespace RedditClone.Models
{
   public  interface IItemFactory
    {
        void CastDownVote(int id, string digger);
        void CastUpVote(int id, string digger);
        void DeleteArticle(int id);
        Article GetArticleID(int id);
        System.Collections.Generic.List<Article> GetHotArticles();
        VoteHistory GetLatestVoteHistory(string url, string voter);
        System.Collections.Generic.List<Article> GetNewestArticles();
        System.Collections.Generic.List<VoteHistory> GetVoteHistory(int articleID);
        System.Collections.Generic.List<Article> SearchURL(string url);
        void SubmitArticle(string url, string title, string owner);
    }
}
