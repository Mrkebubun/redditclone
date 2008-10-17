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
        //private UserInfoController controllerFake;

        [SetUp]
        public void Init()
        {
            
            controllerFake = Isolate.Fake.Instance<UserInfoController>(Members.ReturnRecursiveFakes);
            //Isolate.SwapNextInstance<UserInfoController>().With(controllerFake);
            Isolate.WhenCalled(() => controllerFake.Login(string.Empty, string.Empty, false)).CallOriginal();
            Isolate.NonPublic.WhenCalled(controllerFake, "RedirectToAction").CallOriginal();
            Isolate.WhenCalled(() => controllerFake.Request.HttpMethod).WillReturn(HttpMethod.Post);

            
           //controllerFake = new UserInfoController();
            



        }

        [TearDown]
        public void Clean()
        {

        }


        [Test, RollBack, Isolated]
        public void RegisterGetTest()
        {
            Isolate.WhenCalled(() => controllerFake.Request.HttpMethod).WillReturn(HttpMethod.Get);
            ViewResult result = (ViewResult)controllerFake.Register(null, null, null);
            Assert.AreEqual("Registration", controllerFake.ViewData["Title"]);
        }

        [Test, RollBack, Isolated]
        public void UserInformationShow()
        {
            Isolate.NonPublic.WhenCalled(controllerFake, "View").WithGenericArguments(typeof(string), typeof(object)).WillReturn(null);
            UserDataLayer udlFake = Isolate.Fake.Instance<UserDataLayer>(Members.ReturnNulls);
            Isolate.SwapNextInstance<UserDataLayer>().With(udlFake);

            controllerFake.UserInformation(string.Empty);

            Isolate.Verify.WasCalledWithAnyArguments(() => udlFake.GetUserInfo(string.Empty));
            Isolate.Verify.NonPublic.WasCalled(controllerFake, "View").WithArguments("UserInformation", null);
        }

        [Test, RollBack, Isolated]
        public void LoginGetTest()
        {

            Isolate.WhenCalled(() => controllerFake.Request.HttpMethod).WillReturn(HttpMethod.Get);  
            ViewResult result = (ViewResult)controllerFake.Login(null, null, true);
            Assert.AreEqual("Login", controllerFake.ViewData["Title"]);
            Assert.IsNull(controllerFake.ViewData["errors"]);
            
        }

        [RowTest, RollBack, Isolated]
        [Row("gesghhrs", "rtyhdthdt")]
        public void LoginFailNoUserTest(string username, string password)
        {
            Isolate.WhenCalled(() => controllerFake.Provider.ValidateUser(username, password)).WillReturn(false);  
            ViewResult result = (ViewResult)controllerFake.Login(username, password, true);
            Assert.Greater(((List<string>)controllerFake.ViewData["errors"]).Count, 0);

        }
        [Isolated]
        [RowTest, RollBack]
        [Row("Joseph", "Joseph")]
        public void LoginTest(string username, string password)
        {
            //Isolate.WhenCalled(() => controllerFake.FormsAuth.SetAuthCookie(username, false)).IgnoreCall();
            MembershipProvider fakeProvided = controllerFake.Provider;
            Isolate.WhenCalled(() => fakeProvided.ValidateUser(null, password)).WillReturn(true);


            RedirectToRouteResult result = (RedirectToRouteResult)controllerFake.Login(username, password, false);
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
            controllerFake.Login(username, password, false);
            Assert.Greater(((List<string>)controllerFake.ViewData["errors"]).Count, 0);

            Isolate.Verify.WasCalledWithExactArguments(() => controllerFake.Provider.ValidateUser(username, password));

        }
        [Isolated]
        [RowTest, RollBack]
        [Row("Dennis2", "Dennis2", "myemail@gmail.com")]
        public void AddUserTest(string username, string password, string email)
        {


            MembershipCreateStatus mcs;
            Isolate.WhenCalled(() => controllerFake.Provider.CreateUser(username, password, email, string.Empty, string.Empty, false, null, out mcs)).WillReturn(null);

            RedirectToRouteResult result = (RedirectToRouteResult)controllerFake.Register(username, password, email);
            Assert.AreEqual("Item", (result).Values["controller"]);
            Assert.AreEqual("Main", (result).Values["action"]);

            
            Isolate.Verify.WasCalledWithExactArguments(() => controllerFake.Provider.CreateUser(username, password, email, string.Empty, string.Empty, true, null, out mcs));

        }
    }
}
