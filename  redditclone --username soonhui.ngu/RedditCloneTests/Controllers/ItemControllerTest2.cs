using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Collections;
using RedditClone.Controllers;
using RedditClone.Models;

using System.Collections.Specialized;
using MbUnit.Framework;

using TypeMock;
using TypeMock.ArrangeActAssert;


namespace RedditCloneTests.Controllers
{
    [TestFixture, VerifyMocks]
    public class ItemControllerTest2
    {
        private ItemController controllerFake;
    
   

        public ItemControllerTest2()
        {

        }

        [SetUp]
        public void Init()
        {

            controllerFake = Isolate.Fake.Instance<ItemController>(Members.CallOriginal);
            Isolate.WhenCalled(() => controllerFake.Factory)
                .ReturnRecursiveFake();
                 
            Isolate.WhenCalled(() => controllerFake.Request)
                .ReturnRecursiveFake();

          
        }

        [Test]
        [Isolated]
        public void DisplayItemTest()
        {
            Isolate.WhenCalled(() => controllerFake.Factory.GetHotArticles()).WillReturn(new List<Article>());
            ViewResult result = (ViewResult)controllerFake.Main();
            
            
            Assert.AreEqual("Main", result.ViewName);
            Assert.IsInstanceOfType(typeof(List<Article>), result.ViewData.Model);
            Isolate.Verify.WasCalledWithAnyArguments(() => controllerFake.Factory.GetHotArticles());

        }

        [Test]
        [Isolated]
        public void WhatNewTest()
        {
         
            Isolate.WhenCalled(() => controllerFake.Factory.GetNewestArticles()).WillReturn(new List<Article>());
            ViewResult result = (ViewResult)controllerFake.WhatNew();
            Assert.IsInstanceOfType(typeof(List<Article>), result.ViewData.Model);
            List<Article> viewData = (List<Article>)result.ViewData.Model;

        }



        [RowTest, RollBack]
        [Row(1, "Joseph")]
        [Isolated]
        public void CastUpVoteTest(int id, string digger)
        {
             RedirectToRouteResult result = (RedirectToRouteResult)controllerFake.CastUpVote(id, digger);

            Assert.AreEqual("Item", result.RouteValues["controller"]);
            Assert.AreEqual("Main", result.RouteValues["Action"]);

            Isolate.Verify.WasCalledWithExactArguments(() => controllerFake.Factory.CastUpVote(id, digger));


        }

        [RowTest, RollBack]
        [Row(1, "Joseph")]
        [Isolated]
        public void CastDownVoteTest(int id, string digger)
        {

            RedirectToRouteResult result =(RedirectToRouteResult) controllerFake.CastDownVote(id, digger);
            Assert.AreEqual("Item", result.RouteValues["controller"]);
            Assert.AreEqual("Main", result.RouteValues["Action"]);

            Isolate.Verify.WasCalledWithExactArguments(() => controllerFake.Factory.CastDownVote(id, digger));
        }



        [RowTest, RollBack]
        [Row(5)]
        [Isolated]
        public void DeleteTest( int articleID)
        {

            RedirectToRouteResult result = (RedirectToRouteResult)controllerFake.Delete(articleID);
            Assert.AreEqual("Main", result.RouteValues["controller"]);
            Assert.AreEqual("Item", result.RouteValues["action"]);
            Isolate.Verify.WasCalledWithExactArguments(() => controllerFake.Factory.DeleteArticle(articleID));


        }



        [RowTest, RollBack, Isolated]
        [Row("Soon Hui", "http://www.google.com", "Google")]
        public void SubmitTest(string owner, string url, string title)
        {

       
            RedirectToRouteResult actionEesult = (RedirectToRouteResult)controllerFake.SubmitNew(url, title, owner);
            actionEesult.RouteValues["controller"] = "Main";
            actionEesult.RouteValues["action"] = "Item";

            Isolate.Verify.WasCalledWithExactArguments(() => controllerFake.Factory.SubmitArticle(url, title, owner));

        }

        [Test, RollBack]
        [Isolated]
        public void SubmitViewTest()
        {
   
            controllerFake.SubmitNew();
            Assert.AreEqual("Submit New Item!", controllerFake.ViewData["Title"]);
        }




    }
}
