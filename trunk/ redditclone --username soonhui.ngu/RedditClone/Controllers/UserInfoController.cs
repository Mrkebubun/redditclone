using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RedditClone.Models;

namespace RedditClone.Controllers
{
    public class UserInfoController : Controller
    {
        public void Index()
        {
            // Add action logic here
        }

        public void AddUser()
        {
            new UserDataLayer().AddUser(Request.Form["username"], Request.Form["password"]);
            RedirectToAction("Main", "Item");
        }
    }
}
