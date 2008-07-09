using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RedditClone.Models;


namespace RedditClone.Controllers
{
    public class ItemController : Controller
    {
        public void Index()
        {
            // Add action logic here
        }

        public ActionResult Main()
        {
            ItemFactory factory = new ItemFactory();

            return View("Main", factory.GetHotArticles());
        }

        public ActionResult WhatNew()
        {
            ItemFactory factory = new ItemFactory();
            return View("WhatNew", factory.GetNewestArticles());
        }

        public ActionResult SubmitView()
        {
            return View("Submit", ViewData);
        }

        public void SubmitNew()
        {
            ItemFactory factory = new ItemFactory();
            factory.SubmitArticle(Request.Form["URL"],
                Request.Form["Title"], Request.Form["Diggers"]);
            RedirectToAction("Main","Item");
        }

        public void Delete()
        {
         
            ItemFactory factory = new ItemFactory();
            factory.DeleteArticle(int.Parse(Request.Form["id"]));
            RedirectToAction("Main", "Item");
        }


        [NonAction]
        public Article GetArticleID(int id)
        {
            return new ItemFactory().GetArticleID(id);
        }

        [RequiresAuthentication]
        public void CastUpVote(int articleID, string digger)
        {

            new ItemFactory().CastUpVote(articleID, digger);
            RedirectToAction("Main", "Item");
        }

        public void CastDownVote(int articleID, string digger)
        {
            new ItemFactory().CastDownVote(articleID, digger);
            RedirectToAction("Main", "Item"); 
        }

        
    }
}
