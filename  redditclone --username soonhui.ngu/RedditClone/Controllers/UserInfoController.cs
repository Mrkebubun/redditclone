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
        public UserInfoController(IFormsAuthentication formsAutho):this (formsAutho, null)
        {

        }
        public UserInfoController():this(null, null)
        {

        }
        public UserInfoController(IFormsAuthentication formsAuth, MembershipProvider provider)
        {
            FormsAuth = formsAuth ?? new FormsAuthenticationWrapper();
            Provider = provider ?? Membership.Provider;
        }
        public void Index()
        {
            // Add action logic here
        }

        public MembershipProvider Provider
        {
            get;
            private set;
        }

        public IFormsAuthentication FormsAuth
        {
            get;
            private set;
        }
        public ActionResult Register()
        {
            
            //RedditMembershipProvider p = (RedditMembershipProvider)Membership.Provider;
            MembershipCreateStatus mcs;
            Provider.CreateUser(Request.Form["username"], Request.Form["password"],
                string.Empty, string.Empty, string.Empty, true, null, out mcs);
   
            return RedirectToAction("Main", "Item");
        }

        public ActionResult LoginPage()
        {
            
            return View("Login");
        }

        public ActionResult RegisterPage()
        {
            return View("Register");
        }

        public ActionResult Login(string username, string password, bool? rememberMe)
        {
            //RedditMembershipProvider Provider = (RedditMembershipProvider)Membership.Provider;
            ViewData["Title"] = "Login";

            // Non-POST requests should just display the Login form 
            if (Request.HttpMethod != "POST")
            {
                return View();
            }

            // Basic parameter validation
            List<string> errors = new List<string>();

            if (String.IsNullOrEmpty(username))
            {
                errors.Add("You must specify a username.");
            }

            if (errors.Count == 0)
            {

                // Attempt to login
                bool loginSuccessful = Provider.ValidateUser(username, password);

                if (loginSuccessful)
                {

                    FormsAuth.SetAuthCookie(username, rememberMe ?? false);
                    return RedirectToAction("Main", "Item");
                }
                else
                {
                    errors.Add("The username or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            ViewData["errors"] = errors;
            ViewData["username"] = username;
            return View();
 
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
