using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedditClone.Models
{
    public class UserDataLayer
    {
        public UserInfo GetUserInfo(string userName)
        {
            RedditCloneDataContext dc = new RedditCloneDataContext();
            return dc.UserInfos.Single<UserInfo>(uit => uit.Diggers == userName);
        }

        public void AddUser(string userName, string password)
        {
            RedditCloneDataContext dc = new RedditCloneDataContext();
            UserInfo uif = new UserInfo()
            {
                Diggers = userName,
                password = password
            };
            dc.UserInfos.InsertOnSubmit(uif);
            dc.SubmitChanges();
        }
    }
}
