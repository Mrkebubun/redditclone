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

        public void Main()
        {
            ItemFactory factory = new ItemFactory();

            RenderView("Main", factory.GetHotArticles());
        }

        public void WhatNew()
        {
            ItemFactory factory = new ItemFactory();
            RenderView("WhatNew", factory.GetNewestArticles());
        }

        public void SubmitView()
        {
            RenderView("Submit", ViewData);
        }

        public void SubmitNew()
        {
            ItemFactory factory = new ItemFactory();
            factory.SubmitArticle(Request.Form["URL"],
                Request.Form["Title"], Request.Form["Diggers"]);
            RedirectToAction("Main","Item");
        }

        
    }
}
