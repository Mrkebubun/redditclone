using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedditClone.Models
{

    public class ItemFactory : RedditClone.Models.IItemFactory
    {
        public ItemFactory()
        {

        }
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

            var article = (from arr1 in dc.Articles
                          where arr1.URL== url
                          orderby arr1.submittedDate descending
                          select arr1);
            Article arrNew = article.ToList()[0];

            VoteHistory vHis = new VoteHistory()
            {
                articleID = arrNew.id,
                diggers = owner,
                voteChoice=(int)VoteChoiceEnum.UpVote

            };
 //           RedditCloneDataContext dc1= new RedditCloneDataContext();
            dc.VoteHistories.InsertOnSubmit(vHis);
            dc.SubmitChanges();

            
        }

        public void DeleteArticle(int id)
        {
            RedditCloneDataContext dc = new RedditCloneDataContext();
            var arr = from p in dc.Articles
                      where p.id == id 
                      select p;

            dc.Articles.DeleteAllOnSubmit(arr);

            var vHis = from vH in dc.VoteHistories
                       where vH.articleID == id
                       select vH;

            dc.VoteHistories.DeleteAllOnSubmit(vHis);
            
            dc.SubmitChanges();

        }

        public void CastUpVote(int id, string digger)
        {
            CastVote(id, digger, (int)VoteChoiceEnum.UpVote);
        }

        public void CastDownVote(int id, string digger)
        {
            int voteChoice1 = (int)VoteChoiceEnum.DownVote;
            CastVote(id, digger, voteChoice1);

        }

        private void CastVote(int id, string digger, int voteChoice1)
        {
            RedditCloneDataContext dc = new RedditCloneDataContext();
            VoteHistory vHis = dc.VoteHistories.SingleOrDefault(prop => (prop.diggers == digger && prop.articleID == id));
            if (vHis == null)
            {
                VoteHistory vh = new VoteHistory()
                    {
                        articleID = id,
                        diggers = digger,
                        voteChoice = voteChoice1

                    };
                dc.VoteHistories.InsertOnSubmit(vh);
            }
            else
            {
                vHis.voteChoice = voteChoice1;
            }
            dc.SubmitChanges();
        }

        public Article GetArticleID(int id)
        {
            RedditCloneDataContext dc = new RedditCloneDataContext();
            return dc.Articles.Single<Article>(a => a.id == id);
        }
     //   public GetUpVote(string )
        public List<Article> SearchURL(string url)
        {
            RedditCloneDataContext dc = new RedditCloneDataContext();

            var articles = from p in dc.Articles
                           where p.URL == url
                           select p;

            return articles.ToList<Article>();
        }

        public List<VoteHistory> GetVoteHistory(int articleID)
        {
            RedditCloneDataContext dc = new RedditCloneDataContext();
            var vHistory = from vHis in dc.VoteHistories
                           where vHis.articleID == articleID
                           select vHis;

            return vHistory.ToList<VoteHistory>();
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


        }
    }
}
