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
        public IItemFactory factory;
        public RedditClone.Models.IItemFactory Factory
        {
            get { return factory; }
        }
        public ItemController():this(null)
        {

        }
        public ItemController(IItemFactory itemFactory)
        {
            factory = itemFactory ?? new ItemFactory();
        }
        public void Index()
        {
            
        }

        public ActionResult Main()
        {


            return View("Main", Factory.GetHotArticles());
        }

        public ActionResult WhatNew()
        {
            return View("WhatNew", Factory.GetNewestArticles());
        }


        public ActionResult SubmitNew(string url, string title, string digger)
        {
            ViewData["title"] = "Submit New Item!";
            if(Request.HttpMethod!=HttpMethod.Post)
            {
                return View();
            }
            Factory.SubmitArticle(url, title,digger);
            return RedirectToAction("Main","Item");

        }

        public ActionResult Delete(int articleID)
        {
         
            Factory.DeleteArticle(articleID);
            return RedirectToAction("Item", "Main");
        }


        [NonAction]
        public Article GetArticleID(int id)
        {
            return Factory.GetArticleID(id);
        }

        [RequiresAuthentication]
        public ActionResult CastUpVote(int articleID, string digger)
        {
            
            Factory.CastUpVote(articleID, digger);
            return RedirectToAction("Main", "Item");
        }

        public ActionResult CastDownVote(int articleID, string digger)
        {
            Factory.CastDownVote(articleID, digger);
            return RedirectToAction("Main", "Item"); 
        }

        
    }
}
