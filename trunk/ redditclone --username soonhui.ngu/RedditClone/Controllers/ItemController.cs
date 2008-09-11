﻿using System;
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

        public ActionResult SubmitNew(string url, string title, string digger)
        {
            ItemFactory factory = new ItemFactory();
            factory.SubmitArticle(url,
                title,digger);
            return RedirectToAction("Main","Item");
            //return View();
        }

        public ActionResult Delete()
        {
         
            ItemFactory factory = new ItemFactory();
            factory.DeleteArticle(int.Parse(Request.Form["id"]));
            return RedirectToAction("Main", "Item");
        }


        [NonAction]
        public Article GetArticleID(int id)
        {
            return new ItemFactory().GetArticleID(id);
        }

        [RequiresAuthentication]
        public ActionResult CastUpVote(int articleID, string digger)
        {

            new ItemFactory().CastUpVote(articleID, digger);
            return RedirectToAction("Main", "Item");
        }

        public ActionResult CastDownVote(int articleID, string digger)
        {
            new ItemFactory().CastDownVote(articleID, digger);
            return RedirectToAction("Main", "Item"); 
        }

        
    }
}
