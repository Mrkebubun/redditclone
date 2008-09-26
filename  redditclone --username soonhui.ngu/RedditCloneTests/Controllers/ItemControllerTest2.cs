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
        public ItemControllerTest2()
        {

        }

        [SetUp]
        public void Init()
        {

            controllerFake = Isolate.Fake.Instance<ItemController>(Members.CallOriginal);
            Isolate.Swap<ItemController>().With(controllerFake);

            itemFactoryFake = Isolate.Fake.Instance<ItemFactory>(Members.MustSpecifyReturnValues);
            Isolate.WhenCalled(() => controllerFake.Factory).WillReturn(itemFactoryFake);


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

            Isolate.WhenCalled(() => itemFactoryFake.GetNewestArticles()).WillReturn(new List<Article>());
            ViewResult result = (ViewResult)controller.WhatNew();
            Assert.IsInstanceOfType(typeof(List<Article>), result.ViewData.Model);
            List<Article> viewData = (List<Article>)result.ViewData.Model;

        }



        //[RowTest, RollBack]
        //[Row(1, "Joseph")]
        //public void OppositeCastTest(int id, string voter)
        //{

        //    NameValueCollection nvm = new NameValueCollection();
        //    nvm.Add("articleID", id.ToString());
        //    nvm.Add("diggers", voter);
        //    SubItemController controller = CreateSubItemController(HttpMethod.Post);
        //    int upVoteCount = controller.GetArticleID(id).UpVotes;
        //    int downVoteCount = controller.GetArticleID(id).DownVotes;

        //    controller.CastUpVote(id, voter);
        //    controller.CastDownVote(id, voter);
        //    Assert.AreEqual(upVoteCount, controller.GetArticleID(id).UpVotes);
        //    Assert.AreEqual(downVoteCount + 1, controller.GetArticleID(id).DownVotes);
        //}
        [RowTest, RollBack]
        [Row(1, "Joseph")]
        public void CastUpVoteTest(int id, string digger)
        {
             RedirectToRouteResult result = (RedirectToRouteResult)controller.CastUpVote(id, digger);

            Assert.AreEqual("Item", result.Values["controller"]);
            Assert.AreEqual("Main", result.Values["Action"]);

            Isolate.Verify.WasCalledWithExactArguments(() => itemFactoryFake.CastUpVote(id, digger));


        }

        [RowTest, RollBack]
        [Row(1, "Joseph")]
        public void CastDownVoteTest(int id, string digger)
        {

            RedirectToRouteResult result =(RedirectToRouteResult) controller.CastDownVote(id, digger);
            Assert.AreEqual("Item", result.Values["controller"]);
            Assert.AreEqual("Main", result.Values["Action"]);

            Isolate.Verify.WasCalledWithExactArguments(() => itemFactoryFake.CastDownVote(id, digger));
        }
        //[RowTest, RollBack]
        //[Row("http://itscommonsensestupid.blogspot.com/2008/05/zephyr-test-management-tool.html", "Soon Hui")]
        //public void GetLatestVoteHistory(string url, string voters)
        //{
        //    ItemFactory factory = new ItemFactory();
        //    VoteHistory vH = factory.GetLatestVoteHistory(url, voters);
        //    Assert.AreEqual(voters, vH.diggers);
        //    Assert.AreEqual(1, vH.voteChoice);



        //}


        //[RowTest, RollBack]
        //[Row("http://www.dotnetkicks.com/", 5)]
        //public void DeleteTest(string url, int articleID)
        //{

        //    NameValueCollection nvm = new NameValueCollection();
        //    nvm.Add("id", articleID.ToString());


        //    List<Article> articles1 = new ItemFactory().SearchURL(url);
        //    Assert.AreEqual(1, articles1.Count, "THere should be only 1 article");
        //    SubItemController controller = CreateSubItemController(HttpMethod.Post);
        //    RedirectToRouteResult result = (RedirectToRouteResult)controller.Delete(articleID);
        //    Assert.AreEqual("Main", result.Values["controller"]);
        //    Assert.AreEqual("Item", result.Values["action"]);


        //    List<Article> articles = new ItemFactory().SearchURL(url);
        //    Assert.AreEqual(0, articles.Count, "The article should have been deleted");

        //    List<VoteHistory> vHis = new ItemFactory().GetVoteHistory(articleID);
        //    Assert.AreEqual(0, vHis.Count);

        //}



        //[RowTest, RollBack]
        //[Row("Soon Hui", "http://www.google.com", "Google")]
        //[Row("Soon Hui", "http://www.yahoo.com", "yahoo")]
        //[Row("Soon Hui", "http://www.live.com", "live")]
        //public void SubmitTest(string owner, string url, string title)
        //{


        //    ItemController controller = CreateSubItemController(HttpMethod.Post);
        //    RedirectToRouteResult actionEesult = (RedirectToRouteResult)controller.SubmitNew(url, title, owner);
        //    actionEesult.Values["controller"] = "Main";
        //    actionEesult.Values["action"] = "Item";

        //    List<Article> articles = new ItemFactory().SearchURL(url);
        //    Assert.AreEqual(1, articles.Count);
        //    Assert.AreEqual(owner, articles[0].Diggers);
        //    Assert.AreEqual(title, articles[0].Title);
        //    Assert.AreEqual(1, articles[0].UpVotes);
        //    Assert.AreEqual(0, articles[0].DownVotes);

        //}

        //[RowTest, RollBack]
        //public void SubmitViewTest()
        //{
        //    ItemController controller = CreateSubItemController("GET");
        //    controller.SubmitNew(null, null, null);
        //    Assert.AreEqual("Submit", controller.ViewData["Title"]);
        //}

        //[RowTest, RollBack]
        //[Row(1)]
        //public void UpVoteCalculation(int articleID)
        //{
        //    Article arr = new ItemFactory().GetArticleID(articleID);
        //    Assert.AreEqual(2, arr.UpVotes);
        //}

        //[RowTest, RollBack]
        //[Row(5)]
        //public void DownVoteCalculation(int articleID)
        //{
        //    Article arr = new ItemFactory().GetArticleID(articleID);
        //    Assert.AreEqual(3, arr.DownVotes);
        //}
        //[RowTest, RollBack]
        //[Row(5)]
        //public void CountArticleIDInVoteHistory(int articleID)
        //{
        //    List<VoteHistory> vHis = new ItemFactory().GetVoteHistory(articleID);
        //    Assert.AreEqual(4, vHis.Count);
        //}
    }
}
