using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
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

        [Test]
        public void WhatNewTest()
        {
            SubItemController controller = new SubItemController();
            controller.WhatNew();
            Assert.IsInstanceOfType(typeof(List<Article>),controller.SelectedViewData);
            List<Article> viewData = (List<Article>)controller.SelectedViewData;
            Assert.IsTrue(viewData.Count>=2);
            for (int i = 0; i < viewData.Count-1; i++)
            {
                Assert.IsTrue(viewData[i].submittedDate>viewData[i+1].submittedDate);
            }
        }
        [RowTest, RollBack]
        [Row(1)]
        public void CastUpVoteTest(int id)
        {
   //         Assert.AreEqual(1, new ItemController().GetArticleID(id).UpVotes);

            NameValueCollection nvm = new NameValueCollection();
            nvm.Add("id", id.ToString());
            SubItemController controller = CreateSubItemController(nvm);
       //     Article arr = controller.

            throw new NotImplementedException();
            //controller.CastUpVote();

            //Assert.AreEqual(2, controller.GetArticleID(id).UpVotes);
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
        [Row("http://www.dotnetkicks.com/", 30)]
        public void DeleteTest(string url, int articleID)
        {

            NameValueCollection nvm = new NameValueCollection();
            nvm.Add("id", articleID.ToString());


            List<Article> articles1 = new ItemFactory().SearchURL(url);
            Assert.AreEqual(1, articles1.Count, "THere should be only 1 article");
            SubItemController controller = CreateSubItemController(nvm);
            controller.Delete();

            List<Article> articles = new ItemFactory().SearchURL(url);
            Assert.AreEqual(0, articles.Count, "The article should have been deleted");

            List<VoteHistory> vHis = new ItemFactory().GetVoteHistory(articleID);
            Assert.AreEqual(0, vHis.Count);

        }

        private SubItemController CreateSubItemController(NameValueCollection nvm)
        {
            SubItemController controller = new SubItemController();
            HttpContextBase httpContext;
            HttpRequestBase httpRequest;
            using (mocks.Record())
            {
                httpContext = mocks.DynamicMock<HttpContextBase>();
                httpRequest = mocks.DynamicMock<HttpRequestBase>();
                HttpResponseBase httpResponse = mocks.DynamicMock<HttpResponseBase>();

                SetupResult.For(httpRequest.Form).Return(nvm);
                SetupResult.For(httpContext.Request).Return(httpRequest);
                SetupResult.For(httpContext.Response).Return(httpResponse);

            }
            ControllerContext c = new ControllerContext(httpContext, new RouteData(), controller);
            controller.ControllerContext = c;
            return controller;
        }

        [RowTest, RollBack]
        [Row("Soon Hui", "http://www.google.com", "Google")]
        [Row("Soon Hui", "http://www.yahoo.com", "yahoo")]
        [Row("Soon Hui", "http://www.live.com", "live")]
        public void SubmitTest(string owner, string url, string title)
        {
 //           SubItemController controller = new SubItemController();
            NameValueCollection nvm = new NameValueCollection();
            nvm.Add("Title", title);
            nvm.Add("URL", url);
            nvm.Add("Diggers", owner);

            SubItemController controller =CreateSubItemController(nvm);
            controller.SubmitNew();
            Assert.AreEqual(controller.RedirecedAction["Controller"], "Item");
            Assert.AreEqual(controller.RedirecedAction["Action"], "Main");

            List<Article> articles= new ItemFactory().SearchURL(url);
            Assert.AreEqual(1, articles.Count);
            Assert.AreEqual(owner, articles[0].Diggers);
            Assert.AreEqual(title, articles[0].Title);

        }

        [RowTest, RollBack]
        [Row(1)]
        public void UpVoteCalculation(int articleID)
        {
            Article arr = new ItemFactory().GetArticleID(articleID);
            Assert.AreEqual(2, arr.UpVotes);
        }

        [RowTest, RollBack]
        [Row(30)]
        public void DownVoteCalculation(int articleID)
        {
            Article arr = new ItemFactory().GetArticleID(articleID);
            Assert.AreEqual(3, arr.DownVotes);
        }
        [RowTest, RollBack]
        [Row(30)]
        public void CountArticleIDInVoteHistory(int articleID)
        {
            List<VoteHistory> vHis = new ItemFactory().GetVoteHistory(articleID);
            Assert.AreEqual(4, vHis.Count);
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

        protected override void RenderView(string viewName
          , string masterName, object viewData)
        {
            this.SelectedViewName = viewName;
            SelectedViewData = viewData;
     //       base.RenderView(viewName, masterName, viewData);
        }

        protected override void RedirectToAction(RouteValueDictionary values)
        {
            RedirecedAction = values;
        }
    }
}
