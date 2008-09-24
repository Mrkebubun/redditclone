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

        public void Init()
        {

        }

        public void Clean()
        {

        }

        [Isolated]
        [RowTest, RollBack]
        [Row("Joseph", "Joseph")]
        public void LoginTest(string username, string password)
        {
            UserInfoController controllerFake = Isolate.Fake.Instance<UserInfoController>(Members.CallOriginal);

            HttpRequestBase requestFake = Isolate.Fake.Instance<HttpRequestBase>(Members.MustSpecifyReturnValues);
            Isolate.WhenCalled(() => requestFake.HttpMethod).WillReturn(HttpMethod.Post);
            Isolate.WhenCalled(() => controllerFake.Request).WillReturn(requestFake);

            RedditMembershipProvider providerFake = Isolate.Fake.Instance<RedditMembershipProvider>(Members.MustSpecifyReturnValues);
            Isolate.WhenCalled(() => providerFake.ValidateUser(username, password)).WillReturn(true);
            Isolate.WhenCalled(() => controllerFake.Provider).WillReturn(providerFake);
            Isolate.Swap<UserInfoController>().With(controllerFake);

            IFormsAuthentication formAuthenFake = Isolate.Fake.Instance<IFormsAuthentication>(Members.ReturnNulls);
            Isolate.WhenCalled(() => controllerFake.FormsAuth).WillReturn(formAuthenFake);

            UserInfoController controller = new UserInfoController();
            RedirectToRouteResult result = (RedirectToRouteResult)controller.Login(username, password, false);
            Assert.AreEqual("Item", (result).Values["controller"]);
            Assert.AreEqual("Main", (result).Values["action"]);

            Isolate.Verify.WasCalledWithExactArguments(() => providerFake.ValidateUser(username, password));
            Isolate.Verify.WasCalledWithExactArguments(() => formAuthenFake.SetAuthCookie(username, false));


        }

        [Isolated]
        [RowTest, RollBack]
        [Row("Dennis2", "Dennis2", "myemail@gmail.com")]
        public void AddUserTest(string username, string password, string email)
        {
            UserInfoController controllerFake = Isolate.Fake.Instance<UserInfoController>(Members.CallOriginal);

            HttpRequestBase requestFake = Isolate.Fake.Instance<HttpRequestBase>(Members.MustSpecifyReturnValues);
            Isolate.WhenCalled(() => requestFake.HttpMethod).WillReturn(HttpMethod.Post);

            RedditMembershipProvider providerFake = Isolate.Fake.Instance<RedditMembershipProvider>(Members.MustSpecifyReturnValues);
            MembershipCreateStatus mcs;
            Isolate.WhenCalled(() => providerFake.CreateUser(null, null, null, string.Empty, string.Empty, false, null, out mcs)).WillReturn(null);


            Isolate.WhenCalled(()=>controllerFake.Request).WillReturn(requestFake);
            Isolate.WhenCalled(() => controllerFake.Provider).WillReturn(providerFake);
            Isolate.Swap<UserInfoController>().With(controllerFake);

            UserInfoController controller = new UserInfoController();
            RedirectToRouteResult result = (RedirectToRouteResult)controller.Register(username, password, email);
            Assert.AreEqual("Item", (result).Values["controller"]);
            Assert.AreEqual("Main", (result).Values["action"]);

            Isolate.Verify.WasCalledWithExactArguments(() => providerFake.CreateUser(username, password, email, string.Empty, string.Empty, false, null, out mcs));

        }
    }
}
