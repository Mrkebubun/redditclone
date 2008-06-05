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

    public partial class Article
    {
        public int UpVotes
        {
            get
            {
                var upVotes = from up in VoteHistories
                              where up.articleID== id &&
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
