using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using RedditClone.Controllers;
using RedditClone.Models;
using MbUnit.Framework;
using Rhino.Mocks;

namespace RedditCloneTests.Controllers
{
    /// <summary>
    /// Summary description for LoginControllerTest
    /// </summary>
    [TestFixture]
    public class UserInfoControllerTest
    {
        public UserInfoControllerTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private MockRepository mocks;

        [SetUp]
        public void Init()
        {

            mocks = new MockRepository();

        }
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

        [RowTest, RollBack]
        [Row("Dennis2", "Dennis2")]
        public void AddUserTest(string username, string password)
        {
            NameValueCollection nvm = new NameValueCollection();
            nvm.Add("username", username);
            nvm.Add("password", password);
            
            UserInfoController controller =CreateSubUserInfoController(nvm);
            controller.AddUser();

            UserInfo information = new UserDataLayer().GetUserInfo(username);
            Assert.AreEqual(username, information.Diggers);
            Assert.AreEqual(password, information.password);
            
        }

        [RowTest, RollBack]
        [Row("Joseph")]
        public void GetUserInfoTest(string username)
        {
            UserDataLayer dl = new UserDataLayer();
            UserInfo uif = dl.GetUserInfo(username);
            Assert.IsNotNull(uif);
            Assert.AreEqual(username, uif.password);
        }

        private SubUserInfoController CreateSubUserInfoController(NameValueCollection nvm)
        {
            SubUserInfoController controller = new SubUserInfoController();
            ControllerTestHelper.CreateMockController(controller, nvm, mocks);
            return controller;

        }
    }

    public class SubUserInfoController : UserInfoController
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
