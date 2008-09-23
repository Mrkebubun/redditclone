using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedditClone.Models
{
    public enum VoteChoiceEnum
    {
        NoVote=0,
        UpVote=1,
        DownVote=2,
    }

    public static class HttpMethod
    {
        public const string Get = "GET";
        public const string Post = "POST";
    }
}
