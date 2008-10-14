using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedditClone.Models
{
    public partial class RedditCloneDataContext
    {
        //public List<Article> GetHotArticles()
        //{
        //    Articles.ToList
        //}
    }

    public partial class UserInfo
    {
        public int Reputation
        {
            get
            {
                var submittedArticle = from arr in Articles
                                       where arr.Diggers == Diggers
                                       select arr.id;

                var voteRecord = from vr in VoteHistories
                                 where submittedArticle.Contains(vr.articleID)
                                 group vr by vr.articleID into articleNumber                             
                                 select new 
                                 {
                                     upVotes = articleNumber.Where(vrs => (VoteChoiceEnum)vrs.voteChoice == VoteChoiceEnum.UpVote).Sum(vrs => vrs.voteChoice),
                                     downVotes = articleNumber.Where(vrs => (VoteChoiceEnum)vrs.voteChoice == VoteChoiceEnum.DownVote).Sum(vrs => vrs.voteChoice)
                                 };

                

                return (int)voteRecord.Sum(vrecord => Math.Max(-1,vrecord.upVotes-vrecord.downVotes));
                //int upVotes =(int)voteRecord.Where(vrs => (VoteChoiceEnum)vrs. == VoteChoiceEnum.UpVote).Sum(vrs => vrs.voteChoice);
                //int downVotes = (int)voteRecord.Where(vrs => (VoteChoiceEnum)vrs.voteChoice == VoteChoiceEnum.DownVote).Sum(vrs => vrs.voteChoice);

                //return Math.Max(upVotes - downVotes, -1);
            }
        }
    }
    public partial class Article
    {
        public int UpVotes
        {
            get
            {
                var upVotes = from up in VoteHistories
                              where up.articleID == id &&
                                up.voteChoice == (int)VoteChoiceEnum.UpVote
                              select up;
                return upVotes.ToList<VoteHistory>().Count;
            }
        }

        public int DownVotes
        {
            get
            {
                var upVotes = from up in VoteHistories
                              where up.articleID == id &&
                                up.voteChoice == (int)VoteChoiceEnum.DownVote
                              select up;
                return upVotes.ToList<VoteHistory>().Count;
            }
        }
    }
}
