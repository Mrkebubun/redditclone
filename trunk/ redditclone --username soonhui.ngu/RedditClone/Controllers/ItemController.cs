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

        public RedditClone.Models.IItemFactory Factory
        {
            get;
            private set;
        }
        public ItemController():this(null)
        {

        }
        public ItemController(IItemFactory itemFactory)
        {
            Factory = itemFactory ?? new ItemFactory();
        }
        public void Index()
        {
            
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Main()
        {

            return View("Main", Factory.GetHotArticles());
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult WhatNew()
        {
            return View("WhatNew", Factory.GetNewestArticles());
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult SubmitNew()
        {
            ViewData["title"] = "Submit New Item!";

                return View();

        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SubmitNew(string url, string title, string digger)
        {
            ViewData["title"] = "Submit New Item!";
            Factory.SubmitArticle(url, title,digger);
            return RedirectToAction("Main","Item");

        }

        [AcceptVerbs(HttpVerbs.Post)]
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
