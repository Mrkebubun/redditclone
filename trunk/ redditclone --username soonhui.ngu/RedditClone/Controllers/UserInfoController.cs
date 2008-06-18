﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RedditClone.Models;
using System.Web.Security;

namespace RedditClone.Controllers
{

    /// <summary>
    /// Checks the User's authentication using FormsAuthentication
    /// and redirects to the Login Url for the application on fail
    /// </summary>
    public class RequiresAuthenticationAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuting(FilterExecutingContext filterContext)
        {

            //redirect if not authenticated
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {

                //use the current url for the redirect
                string redirectOnSuccess = filterContext.HttpContext.Request.Url.AbsolutePath;

                //send them off to the login page
                string redirectUrl = string.Format("?ReturnUrl={0}", redirectOnSuccess);
                string loginUrl = FormsAuthentication.LoginUrl + redirectUrl;
                filterContext.HttpContext.Response.Redirect(loginUrl, true);

            }

        }
    }
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