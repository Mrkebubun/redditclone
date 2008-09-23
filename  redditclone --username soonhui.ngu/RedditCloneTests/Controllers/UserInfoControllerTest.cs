using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Configuration;

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
            //System.Configuration.ConfigurationManager.ConnectionStrings["RedditCloneConnectionString"].ConnectionString =
            //    @"D:\C#\Test\RedditClone\RedditClone\App_Data\RedditClone.mdf";

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
        [Row("Dennis2", "Dennis2", "myemail@gmail.com")]
        public void AddUserTest(string username, string password, string email)
        {
            NameValueCollection nvm = new NameValueCollection();
            //nvm.Add("username", username);
            //nvm.Add("password", password);
            
            UserInfoController controller =CreateSubUserInfoController("POST");
            controller.Register(username, password, email);

            UserInfo information = new UserDataLayer().GetUserInfo(username);
            Assert.AreEqual(username, information.Diggers);
            Assert.AreEqual(password, information.password);
            
        }

        [RowTest, RollBack]
        [Row("Joseph", "Joseph")]
        public void LoginTest(string username, string password)
        {
            SubUserInfoController controller = CreateSubUserInfoController("POST");
            RedirectToRouteResult result = (RedirectToRouteResult)controller.Login(username, password, false);
            Assert.AreEqual("Item", (result).Values["controller"]);
            Assert.AreEqual("Main", (result).Values["action"]);

        }

        [RowTest, RollBack]
        [Row("Joseph", "rtyhdthdt")]
        public void LoginFailTest(string username, string password)
        {
            SubUserInfoController controller = CreateSubUserInfoController("POST");
            ViewResult result = (ViewResult)controller.Login(username, password, true);
            Assert.Greater(((List<string>)controller.ViewData["errors"]).Count, 0);
            
        }

        [Test, RollBack]
        public void RegisterGetTest()
        {
            SubUserInfoController controller = CreateSubUserInfoController("GET");
            ViewResult result =(ViewResult)controller.Register(null, null, null);
            Assert.AreEqual("Registration", controller.ViewData["Title"]);
        }
        [Test, RollBack]
        public void LoginGetTest()
        {
            SubUserInfoController controller = CreateSubUserInfoController("GET");
            ViewResult result = (ViewResult)controller.Login(null, null, true);
            Assert.AreEqual("Login", controller.ViewData["Title"]);
            //Assert.Greater(((List<string>)controller.ViewData["errors"]).Count, 0);
        }

        [RowTest, RollBack]
        [Row("gesghhrs", "rtyhdthdt")]
        public void LoginFailNoUserTest(string username, string password)
        {
            //NameValueCollection nvm = new NameValueCollection();
            //nvm.Add("username", username);
            //nvm.Add("password", password);
            UserInfoController controller = CreateSubUserInfoController("POST");
            ViewResult result = (ViewResult)controller.Login(username, password, true);
           
            
            //Assert.AreEqual(controller.RedirecedAction["action"], "LoginPage");
           
            Assert.Greater(((List<string>)controller.ViewData["errors"]).Count, 0);
           
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

        private SubUserInfoController CreateSubUserInfoController(string httpMethod)
        {
            IFormsAuthentication frmAuthentication = mocks.DynamicMock<IFormsAuthentication>();
            SubUserInfoController controller = new SubUserInfoController(frmAuthentication);
            ControllerTestHelper.CreateMockController(controller, mocks, httpMethod);            
            return controller;

        }
    }

    public class SubUserInfoController : UserInfoController
    {
        public SubUserInfoController(IFormsAuthentication formsAutho):base (formsAutho)
        {

        }
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
            //       base.RenderView(viewName, masterName, viewData);
        }
        //protected override RedirectToRouteResult RedirectToAction(string actionName, string controllerName, RouteValueDictionary values)
        //{
        //    RedirecedAction = values;
        //    return null;
        //}
        //protected override RedirectToRouteResult RedirectToAction(RouteValueDictionary values)
        //{
        //    RedirecedAction = values;
        //    return null;
        //}
    }
}
