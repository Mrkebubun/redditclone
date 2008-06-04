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

        [Test, RollBack]
        public void SubmitTest()
        {
            SubItemController controller = new SubItemController();
            string owner = "Soon Hui";
            string url = "http://www.google.com";
            string title = "Google";
            NameValueCollection nvm = new NameValueCollection();
            nvm.Add("Title", title);
            nvm.Add("URL", url);
            nvm.Add("Diggers", owner);

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
            controller.SubmitNew();
            Assert.AreEqual(controller.RedirecedAction["Controller"], "Item");
            Assert.AreEqual(controller.RedirecedAction["Action"], "Main");

            List<Article> articles= new ItemFactory().SearchURL(url);
            Assert.AreEqual(1, articles.Count);
            Assert.AreEqual(1, articles[0].UpVotes);
            Assert.AreEqual(0, articles[0].DownVotes);
            Assert.AreEqual(owner, articles[0].Diggers);
            Assert.AreEqual(title, articles[0].Title);
            //throw new NotImplementedException();
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
