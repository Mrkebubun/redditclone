using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Configuration;
using System.Web.Security;

using RedditClone.Controllers;
using RedditClone.Models;

using MbUnit.Framework;
using TypeMock;
using TypeMock.ArrangeActAssert;

namespace RedditCloneTests.Controllers
{
    [TestFixture]
    [ClearMocks]
    public class UserInfoControllerTest2
    {
        public UserInfoControllerTest2()
        {

        }
        private UserInfoController controllerFake;
        private UserInfoController controller;

        [SetUp]
        public void Init()
        {
            
            controllerFake = Isolate.Fake.Instance<UserInfoController>(Members.CallOriginal);
            Isolate.SwapNextInstance<UserInfoController>().With(controllerFake);
            Isolate.WhenCalled(() => controllerFake.Request.HttpMethod).WillReturn(HttpMethod.Post);

            
           controller = new UserInfoController();



        }

        [TearDown]
        public void Clean()
        {

        }


        [Test, RollBack, Isolated]
        public void RegisterGetTest()
        {
            Isolate.WhenCalled(() => controllerFake.Request.HttpMethod).WillReturn(HttpMethod.Get);
            ViewResult result = (ViewResult)controller.Register(null, null, null);
            Assert.AreEqual("Registration", controller.ViewData["Title"]);
        }

        [Test, RollBack, Isolated]
        public void UserInformationShow()
        {
            Isolate.NonPublic.WhenCalled(controllerFake, "View").WithGenericArguments(typeof(string), typeof(object)).WillReturn(null);
            UserDataLayer udlFake = Isolate.Fake.Instance<UserDataLayer>(Members.ReturnNulls);
            Isolate.SwapNextInstance<UserDataLayer>().With(udlFake);

            controller.UserInformation(string.Empty);

            Isolate.Verify.WasCalledWithAnyArguments(() => udlFake.GetUserInfo(string.Empty));
            Isolate.Verify.NonPublic.WasCalled(controllerFake, "View").WithArguments("UserInformation", null);
        }

        [Test, RollBack, Isolated]
        public void LoginGetTest()
        {

            Isolate.WhenCalled(() => controllerFake.Request.HttpMethod).WillReturn(HttpMethod.Get);  
            ViewResult result = (ViewResult)controller.Login(null, null, true);
            Assert.AreEqual("Login", controller.ViewData["Title"]);
            Assert.IsNull(controller.ViewData["errors"]);
            
        }

        [RowTest, RollBack, Isolated]
        [Row("gesghhrs", "rtyhdthdt")]
        public void LoginFailNoUserTest(string username, string password)
        {
            Isolate.WhenCalled(() => controllerFake.Provider.ValidateUser(username, password)).WillReturn(false);  
            ViewResult result = (ViewResult)controller.Login(username, password, true);
            Assert.Greater(((List<string>)controller.ViewData["errors"]).Count, 0);

        }
        [Isolated]
        [RowTest, RollBack]
        [Row("Joseph", "Joseph")]
        public void LoginTest(string username, string password)
        {
            Isolate.WhenCalled(() => controllerFake.FormsAuth.SetAuthCookie(username, false)).IgnoreCall();
            Isolate.WhenCalled(() => controllerFake.Provider.ValidateUser(null, password)).WillReturn(true);

            RedirectToRouteResult result = (RedirectToRouteResult)controller.Login(username, password, false);
            Assert.AreEqual("Item", (result).Values["controller"]);
            Assert.AreEqual("Main", (result).Values["action"]);

            Isolate.Verify.WasCalledWithExactArguments(() => controllerFake.Provider.ValidateUser(username, password));
            Isolate.Verify.WasCalledWithExactArguments(() => controllerFake.FormsAuth.SetAuthCookie(username, false));


        }
        [RowTest, RollBack, Isolated]
        [Row("Joseph", "rtyhdthdt")]
        public void LoginFailTest(string username, string password)
        {
            Isolate.WhenCalled(() => controllerFake.Provider.ValidateUser(username, password)).WillReturn(false);
            controller.Login(username, password, false);
            Assert.Greater(((List<string>)controller.ViewData["errors"]).Count, 0);

            Isolate.Verify.WasCalledWithExactArguments(() => controllerFake.Provider.ValidateUser(username, password));

        }
        [Isolated]
        [RowTest, RollBack]
        [Row("Dennis2", "Dennis2", "myemail@gmail.com")]
        public void AddUserTest(string username, string password, string email)
        {


            MembershipCreateStatus mcs;
            Isolate.WhenCalled(() => controllerFake.Provider.CreateUser(username, password, email, string.Empty, string.Empty, false, null, out mcs)).WillReturn(null);

            RedirectToRouteResult result = (RedirectToRouteResult)controller.Register(username, password, email);
            Assert.AreEqual("Item", (result).Values["controller"]);
            Assert.AreEqual("Main", (result).Values["action"]);

            
            Isolate.Verify.WasCalledWithExactArguments(() => controllerFake.Provider.CreateUser(username, password, email, string.Empty, string.Empty, true, null, out mcs));

        }
    }
}
