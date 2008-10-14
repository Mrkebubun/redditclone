using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using System.Web.Mvc;
using System.Web;

using System.Web.Routing;
using RedditClone;
using TypeMock;
using TypeMock.ArrangeActAssert;

namespace RedditCloneTests
{
    [TestFixture]
     [Isolated]
    public class RouteTest
    {
        private RouteCollection routes;
        private HttpContextBase contextFake;

        [SetUp]
        public void Init()
        {
            routes = new RouteCollection();
            GlobalApplication.RegisterRoutes(routes);

           contextFake = Isolate.Fake.Instance<HttpContextBase>(Members.ReturnRecursiveFakes);
            Isolate.SwapNextInstance<HttpContextBase>().With(contextFake);


        }

        [RowTest, Isolated]
        [Row("~/Login", "UserInfo", "Login")]
        [Row("~/", "Item", "Main")]
        [Row("~/Main", "Item", "Main")]
        [Row("~/Register", "UserInfo", "Register")]
        [Row("~/submit", "Item", "SubmitNew")]
        [Row("~/Users/123", "UserInfo", "UserInformation")]
        public void LoginRoute(string url, string controller, string action)
        {
            Isolate.WhenCalled(()=>contextFake.Request.AppRelativeCurrentExecutionFilePath).WillReturn(url);
            RouteData routeData = routes.GetRouteData(contextFake);

            Assert.IsNotNull(routeData);
            Assert.AreEqual(controller, routeData.Values["controller"]);
            Assert.AreEqual(action, routeData.Values["action"]);

        }

    }
}
