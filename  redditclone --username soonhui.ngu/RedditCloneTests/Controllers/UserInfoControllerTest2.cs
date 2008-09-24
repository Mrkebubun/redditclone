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
    public class UserInfoControllerTest2
    {
        public UserInfoControllerTest2()
        {

        }
        private UserInfoController controllerFake;
        private HttpRequestBase requestFake;
        private UserInfoController controller;
        private IFormsAuthentication formAuthenFake;
        private RedditMembershipProvider providerFake;

        [SetUp]
        public void Init()
        {
            controllerFake = Isolate.Fake.Instance<UserInfoController>(Members.CallOriginal);
            Isolate.Swap<UserInfoController>().With(controllerFake);

            requestFake = Isolate.Fake.Instance<HttpRequestBase>(Members.MustSpecifyReturnValues);
            Isolate.WhenCalled(() => requestFake.HttpMethod).WillReturn(HttpMethod.Post);
            Isolate.WhenCalled(() => controllerFake.Request).WillReturn(requestFake);

            formAuthenFake = Isolate.Fake.Instance<IFormsAuthentication>(Members.ReturnNulls);
            Isolate.WhenCalled(() => controllerFake.FormsAuth).WillReturn(formAuthenFake);

            providerFake = Isolate.Fake.Instance<RedditMembershipProvider>(Members.ReturnNulls);
            Isolate.WhenCalled(() => providerFake.Name).WillReturn("RedditMembershipProvider");
            Isolate.WhenCalled(() => controllerFake.Provider).WillReturn(providerFake);

           controller = new UserInfoController();



        }

        [TearDown]
        public void Clean()
        {

        }
        [Test, RollBack]
        public void RegisterGetTest()
        {
            Isolate.WhenCalled(() => requestFake.HttpMethod).WillReturn(HttpMethod.Get);
            ViewResult result = (ViewResult)controller.Register(null, null, null);
            Assert.AreEqual("Registration", controller.ViewData["Title"]);
        }

        [Test, RollBack]
        public void LoginGetTest()
        {
            Isolate.WhenCalled(() => requestFake.HttpMethod).WillReturn(HttpMethod.Get);  
            ViewResult result = (ViewResult)controller.Login(null, null, true);
            Assert.AreEqual("Login", controller.ViewData["Title"]);
            Assert.IsNull(controller.ViewData["errors"]);
            //Assert.AreEqual(0,((List<string>)controller.ViewData["errors"]).Count);
        }

        [RowTest, RollBack]
        [Row("gesghhrs", "rtyhdthdt")]
        public void LoginFailNoUserTest(string username, string password)
        {
            Isolate.WhenCalled(() => providerFake.ValidateUser(username, password)).WillReturn(false);  
            ViewResult result = (ViewResult)controller.Login(username, password, true);
            Assert.Greater(((List<string>)controller.ViewData["errors"]).Count, 0);

        }
        [Isolated]
        [RowTest, RollBack]
        [Row("Joseph", "Joseph")]
        public void LoginTest(string username, string password)
        {

            Isolate.WhenCalled(() => providerFake.ValidateUser(username, password)).WillReturn(true);

            RedirectToRouteResult result = (RedirectToRouteResult)controller.Login(username, password, false);
            Assert.AreEqual("Item", (result).Values["controller"]);
            Assert.AreEqual("Main", (result).Values["action"]);

            Isolate.Verify.WasCalledWithExactArguments(() => providerFake.ValidateUser(username, password));
            Isolate.Verify.WasCalledWithExactArguments(() => formAuthenFake.SetAuthCookie(username, false));


        }
        [RowTest, RollBack]
        [Row("Joseph", "rtyhdthdt")]
        public void LoginFailTest(string username, string password)
        {
            Isolate.WhenCalled(() => providerFake.ValidateUser(username, password)).WillReturn(false);
            controller.Login(username, password, false);
            Assert.Greater(((List<string>)controller.ViewData["errors"]).Count, 0);

            Isolate.Verify.WasCalledWithExactArguments(() => providerFake.ValidateUser(username, password));

        }
        [Isolated]
        [RowTest, RollBack]
        [Row("Dennis2", "Dennis2", "myemail@gmail.com")]
        public void AddUserTest(string username, string password, string email)
        {


            MembershipCreateStatus mcs;
            Isolate.WhenCalled(() => providerFake.CreateUser(null, null, null, string.Empty, string.Empty, false, null, out mcs)).WillReturn(null);

            RedirectToRouteResult result = (RedirectToRouteResult)controller.Register(username, password, email);
            Assert.AreEqual("Item", (result).Values["controller"]);
            Assert.AreEqual("Main", (result).Values["action"]);

            Isolate.Verify.WasCalledWithExactArguments(() => providerFake.CreateUser(username, password, email, string.Empty, string.Empty, false, null, out mcs));

        }
    }
}
