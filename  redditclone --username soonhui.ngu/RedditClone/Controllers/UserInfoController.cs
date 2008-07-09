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

        public void Register()
        {
            
            RedditMembershipProvider p = (RedditMembershipProvider)Membership.Provider;
            MembershipCreateStatus mcs;
            p.CreateUser(Request.Form["username"], Request.Form["password"],
                string.Empty, string.Empty, string.Empty, true, null, out mcs);
   
            RedirectToAction("Main", "Item");
        }

        public ActionResult LoginPage()
        {
            
            return View("Login");
        }

        public ActionResult RegisterPage()
        {
            return View("Register");
        }

        public void Login(string username, 
            string password, string rememberMe, string returnUrl)
        {
            RedditMembershipProvider p = (RedditMembershipProvider)Membership.Provider;
            if(p.ValidateUser(username, password))
            {
               
                FormsAuthentication.SetAuthCookie(username, rememberMe == "checked");
                Response.Redirect(returnUrl);
            }
            else
            {
                ViewData["ErrorMessage"] = "Incorrect user name or password";
                RedirectToAction("LoginPage");
            }
            //if(Membership.ValidateUser(username, password))
            //{
            //    FormsAuthentication.SetAuthCookie(username, rememberMe=="checked");
            //    Response.Redirect(returnUrl);
            //}
            //else
            //{
            //    ViewData["ErrorMessage"] = "Incorrect user name or password";
            //}
        }

        //public void CreateUser(string userName, string emailAddress, 
        //    string password, string returnUrl)
        //{
            
        //    try
        //    {
        //        if (Membership.CreateUser(userName, password) == null)
        //        {
        //            throw new MembershipCreateUserException("");

        //        }
        //        FormsAuthentication.SetAuthCookie(userName, true);
        //        Response.Redirect(returnUrl);
        //    }
        //    catch(MembershipCreateUserException mcue)
        //    {
        //        ViewData["ErrorMessage"] = mcue.Message;
        //        RenderView("Login");
        //    }
        //}
    }
}
