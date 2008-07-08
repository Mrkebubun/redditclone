using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RedditClone.Models;
using System.Web.Security;

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
            
            RedditMembershipProvider p = (RedditMembershipProvider)Membership.Provider;
            MembershipCreateStatus mcs;
            p.CreateUser(Request.Form["username"], Request.Form["password"],
                string.Empty, string.Empty, string.Empty, true, null, out mcs);

            //Membership.CreateUser(Request.Form["username"], Request.Form["password"]);
     //       new UserDataLayer().AddUser(Request.Form["username"], Request.Form["password"]);
            RedirectToAction("Main", "Item");
        }

        public void LoginPage()
        {
            
            RenderView("Login");
        }

        public void RegisterPage()
        {
            RenderView("Register");
        }

        public void Login(string username, 
            string password, string rememberMe, string returnUrl)
        {
            if(Membership.ValidateUser(username, password))
            {
                FormsAuthentication.SetAuthCookie(username, rememberMe=="checked");
                Response.Redirect(returnUrl);
            }
            else
            {
                ViewData["ErrorMessage"] = "Incorrect user name or password";
            }
        }

        public void CreateUser(string userName, string emailAddress, 
            string password, string returnUrl)
        {
            
            try
            {
                if (Membership.CreateUser(userName, password) == null)
                {
                    throw new MembershipCreateUserException("");

                }
                FormsAuthentication.SetAuthCookie(userName, true);
                Response.Redirect(returnUrl);
            }
            catch(MembershipCreateUserException mcue)
            {
                ViewData["ErrorMessage"] = mcue.Message;
                RenderView("Login");
            }
        }
    }
}
