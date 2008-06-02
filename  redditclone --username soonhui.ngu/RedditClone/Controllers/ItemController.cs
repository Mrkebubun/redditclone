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

            RenderView("Main", factory.GetArticle());
        }
    }
}
