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

using Rhino.Mocks;
using System.Collections.Specialized;
using MbUnit.Framework;

namespace RedditCloneTests.Controllers
{
    /// <summary>
    /// Summary description for ItemControllerTest
    /// </summary>
    [TestFixture]
    public class ItemControllerTest
    {
        public ItemControllerTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        private MockRepository mocks;
        //private TestContext testContextInstance;

        ///// <summary>
        /////Gets or sets the test context which provides
        /////information about and functionality for the current test run.
        /////</summary>
        //public TestContext TestContext
        //{
        //    get
        //    {
        //        return testContextInstance;
        //    }
        //    set
        //    {
        //        testContextInstance = value;
        //    }
        //}

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [SetUp]
        public void Init()
        {

            mocks = new MockRepository();

        }

        [Test]
        public void DisplayItemTest()
        {
            SubItemController controller = new SubItemController();
            controller.Main();
            Assert.AreEqual("Main", controller.SelectedViewName);
            Assert.IsInstanceOfType(typeof(List<Article>), controller.SelectedViewData);

        }

        //[Test]
        //public void WhatNewTest()
        //{
        //    SubItemController controller = new SubItemController();
        //    controller.WhatNew();
        //    Assert.IsInstanceOfType(typeof(List<Article>),controller.SelectedViewData);
        //    List<Article> viewData = (List<Article>)controller.SelectedViewData;
        //    Assert.IsTrue(viewData.Count>=2);
        //    for (int i = 0; i < viewData.Count-1; i++)
        //    {
        //        Assert.GreaterEqualThan(viewData[i].submittedDate, viewData[i + 1].submittedDate);
        //        //Assert.IsTrue(viewData[i].submittedDate>viewData[i+1].submittedDate);
        //    }
        //}

        [RowTest, RollBack]
        [Row(1, "Joseph")]
        public void DoubleCastTest(int id, string voter)
        {
            NameValueCollection nvm = new NameValueCollection();
            nvm.Add("articleID", id.ToString());
            nvm.Add("diggers", voter);
            SubItemController controller = CreateSubItemController(HttpMethod.Post);
            controller.CastUpVote(id, voter);

            int upVoteCount = controller.GetArticleID(id).UpVotes;
            controller.CastUpVote(id, voter);
            Assert.AreEqual(upVoteCount, controller.GetArticleID(id).UpVotes);
        }

        [RowTest, RollBack]
        [Row(1, "Joseph")]
        public void OppositeCastTest(int id, string voter)
        {

            NameValueCollection nvm = new NameValueCollection();
            nvm.Add("articleID", id.ToString());
            nvm.Add("diggers", voter);
            SubItemController controller = CreateSubItemController(HttpMethod.Post);
            int upVoteCount = controller.GetArticleID(id).UpVotes;
            int downVoteCount = controller.GetArticleID(id).DownVotes;

            controller.CastUpVote(id, voter);
            controller.CastDownVote(id, voter);
            Assert.AreEqual(upVoteCount, controller.GetArticleID(id).UpVotes);
            Assert.AreEqual(downVoteCount + 1, controller.GetArticleID(id).DownVotes);
        }
        [RowTest, RollBack]
        [Row(1, "Joseph")]
        public void CastUpVoteTest(int id, string digger)
        {
            NameValueCollection nvm = new NameValueCollection();
            nvm.Add("articleID", id.ToString());
            nvm.Add("diggers", digger);
            SubItemController controller = CreateSubItemController(HttpMethod.Post);
            controller.CastUpVote(id, digger);

            Assert.AreEqual(3, controller.GetArticleID(id).UpVotes);
        }

        [RowTest, RollBack]
        [Row(1, "Joseph")]
        public void CastDownVoteTest(int id, string digger)
        {
            NameValueCollection nvm = new NameValueCollection();
            nvm.Add("articleID", id.ToString());
            nvm.Add("diggers", digger);
            SubItemController controller = CreateSubItemController(HttpMethod.Post);
            controller.CastDownVote(id, digger);

            Assert.AreEqual(1, controller.GetArticleID(id).DownVotes);
        }
        [RowTest, RollBack]
        [Row("http://itscommonsensestupid.blogspot.com/2008/05/zephyr-test-management-tool.html", "Soon Hui")]
        public void GetLatestVoteHistory(string url,  string voters)
        {
            ItemFactory factory = new ItemFactory();
            VoteHistory vH = factory.GetLatestVoteHistory(url, voters);
            Assert.AreEqual(voters, vH.diggers);
            Assert.AreEqual(1, vH.voteChoice);
            


        }


        [RowTest, RollBack]
        [Row("http://www.dotnetkicks.com/", 5)]
        public void DeleteTest(string url, int articleID)
        {

            NameValueCollection nvm = new NameValueCollection();
            nvm.Add("id", articleID.ToString());


            List<Article> articles1 = new ItemFactory().SearchURL(url);
            Assert.AreEqual(1, articles1.Count, "THere should be only 1 article");
            SubItemController controller = CreateSubItemController(HttpMethod.Post);
            RedirectToRouteResult result = (RedirectToRouteResult)controller.Delete(articleID);
            Assert.AreEqual("Main",result.Values["controller"]);
            Assert.AreEqual("Item", result.Values["action"]);


            List<Article> articles = new ItemFactory().SearchURL(url);
            Assert.AreEqual(0, articles.Count, "The article should have been deleted");

            List<VoteHistory> vHis = new ItemFactory().GetVoteHistory(articleID);
            Assert.AreEqual(0, vHis.Count);

        }



        [RowTest, RollBack]
        [Row("Soon Hui", "http://www.google.com", "Google")]
        [Row("Soon Hui", "http://www.yahoo.com", "yahoo")]
        [Row("Soon Hui", "http://www.live.com", "live")]
        public void SubmitTest(string owner, string url, string title)
        {


            ItemController controller = CreateSubItemController(HttpMethod.Post);
            RedirectToRouteResult actionEesult = (RedirectToRouteResult)controller.SubmitNew(url, title, owner);
            actionEesult.Values["controller"] = "Main";
            actionEesult.Values["action"] = "Item";

            List<Article> articles= new ItemFactory().SearchURL(url);
            Assert.AreEqual(1, articles.Count);
            Assert.AreEqual(owner, articles[0].Diggers);
            Assert.AreEqual(title, articles[0].Title);
            Assert.AreEqual(1, articles[0].UpVotes);
            Assert.AreEqual(0, articles[0].DownVotes);

        }

        [RowTest, RollBack]
        public void SubmitViewTest()
        {
            ItemController controller = CreateSubItemController("GET");
            controller.SubmitNew(null, null, null);
            Assert.AreEqual("Submit", controller.ViewData["Title"]);
        }

        [RowTest, RollBack]
        [Row(1)]
        public void UpVoteCalculation(int articleID)
        {
            Article arr = new ItemFactory().GetArticleID(articleID);
            Assert.AreEqual(2, arr.UpVotes);
        }

        [RowTest, RollBack]
        [Row(5)]
        public void DownVoteCalculation(int articleID)
        {
            Article arr = new ItemFactory().GetArticleID(articleID);
            Assert.AreEqual(3, arr.DownVotes);
        }
        [RowTest, RollBack]
        [Row(5)]
        public void CountArticleIDInVoteHistory(int articleID)
        {
            List<VoteHistory> vHis = new ItemFactory().GetVoteHistory(articleID);
            Assert.AreEqual(4, vHis.Count);
        }

        private SubItemController CreateSubItemController(string httpMethod)
        {
            SubItemController controller = new SubItemController();
            ControllerTestHelper.CreateMockController(controller, mocks, httpMethod);

            return controller;

        }

    }

    public class SubItemController : ItemController
    {
        public string SelectedViewName { get; private set; }
        public RouteValueDictionary RedirecedAction { get; private set; }

        /// <summary>
        /// following the ugly examples of phill haack
        /// </summary>
        public object SelectedViewData { get; private set; }


        protected override ViewResult View(string viewName
          , string masterName, object viewData)
        {
            this.SelectedViewName = viewName;
            SelectedViewData = viewData;
            return null;
     //       return base.View()
     //       base.RenderView(viewName, masterName, viewData);
        }

       // protected override RedirectToRouteResult RedirectToAction(string actionName, string controllerName, RouteValueDictionary values)
       // {
       //     RedirecedAction = values;
       //     return null;
       ////     return base.RedirectToAction(actionName, controllerName, values);
       // }
        //protected override RedirectToRouteResult RedirectToAction(RouteValueDictionary values)
        //{
        //    RedirecedAction = values;
        //    return null;
        //}
    }
}
