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
        private ItemController controller;
        private ItemFactory itemFactoryFake;
        private HttpRequestBase httpRequestFake;
        public ItemControllerTest2()
        {

        }

        [SetUp]
        public void Init()
        {

            controllerFake = Isolate.Fake.Instance<ItemController>(Members.CallOriginal);
            Isolate.SwapNextInstance<ItemController>().With(controllerFake);

            itemFactoryFake = Isolate.Fake.Instance<ItemFactory>(Members.MustSpecifyReturnValues);
            Isolate.WhenCalled(() => controllerFake.Factory).WillReturn(itemFactoryFake);

            httpRequestFake = Isolate.Fake.Instance<HttpRequestBase>(Members.MustSpecifyReturnValues);
            Isolate.WhenCalled(() => controllerFake.Request).WillReturn(httpRequestFake);

            controller = new ItemController();
        }

        [Test]
        [Isolated]
        public void DisplayItemTest()
        {
            Isolate.WhenCalled(() => itemFactoryFake.GetHotArticles()).WillReturn(new List<Article>());
            ViewResult result = (ViewResult)controller.Main();
            
            Assert.AreEqual("Main", result.ViewName);
            Assert.IsInstanceOfType(typeof(List<Article>), result.ViewData.Model);
            Isolate.Verify.WasCalledWithAnyArguments(() => itemFactoryFake.GetHotArticles());

        }

        [Test]
        [Isolated]
        public void WhatNewTest()
        {
            //Isolate.WhenCalled(() => httpRequestFake.HttpMethod).WillReturn(HttpMethod.Get);
            Isolate.WhenCalled(() => itemFactoryFake.GetNewestArticles()).WillReturn(new List<Article>());
            ViewResult result = (ViewResult)controller.WhatNew();
            Assert.IsInstanceOfType(typeof(List<Article>), result.ViewData.Model);
            List<Article> viewData = (List<Article>)result.ViewData.Model;

        }



        [RowTest, RollBack]
        [Row(1, "Joseph")]
        [Isolated]
        public void CastUpVoteTest(int id, string digger)
        {
             RedirectToRouteResult result = (RedirectToRouteResult)controller.CastUpVote(id, digger);

            Assert.AreEqual("Item", result.Values["controller"]);
            Assert.AreEqual("Main", result.Values["Action"]);

            Isolate.Verify.WasCalledWithExactArguments(() => itemFactoryFake.CastUpVote(id, digger));


        }

        [RowTest, RollBack]
        [Row(1, "Joseph")]
        [Isolated]
        public void CastDownVoteTest(int id, string digger)
        {

            RedirectToRouteResult result =(RedirectToRouteResult) controller.CastDownVote(id, digger);
            Assert.AreEqual("Item", result.Values["controller"]);
            Assert.AreEqual("Main", result.Values["Action"]);

            Isolate.Verify.WasCalledWithExactArguments(() => itemFactoryFake.CastDownVote(id, digger));
        }



        [RowTest, RollBack]
        [Row(5)]
        [Isolated]
        public void DeleteTest( int articleID)
        {

            Isolate.WhenCalled(() => itemFactoryFake.DeleteArticle(articleID)).IgnoreCall();
            RedirectToRouteResult result = (RedirectToRouteResult)controller.Delete(articleID);
            Assert.AreEqual("Main", result.Values["controller"]);
            Assert.AreEqual("Item", result.Values["action"]);
            Isolate.Verify.WasCalledWithExactArguments(() => itemFactoryFake.DeleteArticle(articleID));


        }



        [RowTest, RollBack, Isolated]
        [Row("Soon Hui", "http://www.google.com", "Google")]
        public void SubmitTest(string owner, string url, string title)
        {

            Isolate.WhenCalled(() => httpRequestFake.HttpMethod).WillReturn(HttpMethod.Post);
            Isolate.WhenCalled(() => itemFactoryFake.SubmitArticle(url, title, owner)).IgnoreCall();
            RedirectToRouteResult actionEesult = (RedirectToRouteResult)controller.SubmitNew(url, title, owner);
            actionEesult.Values["controller"] = "Main";
            actionEesult.Values["action"] = "Item";

            Isolate.Verify.WasCalledWithExactArguments(() => itemFactoryFake.SubmitArticle(url, title, owner));

        }

        [Test, RollBack]
        [Isolated]
        public void SubmitViewTest()
        {
            Isolate.WhenCalled(() => httpRequestFake.HttpMethod).WillReturn(HttpMethod.Get);
            controller.SubmitNew(null, null, null);
            Assert.AreEqual("Submit New Item!", controller.ViewData["Title"]);
        }




    }
}
