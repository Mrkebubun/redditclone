using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using System.Web.Mvc;
using System.Web;

using Rhino.Mocks;
using System.Web.Routing;
using RedditClone;

namespace RedditCloneTests
{
    [TestFixture]
    public class RouteTest
    {
        private RouteCollection routes;
        private MockRepository mocks;
        private HttpContextBase httpContext;
        [SetUp]
        public void Init()
        {
            routes = new RouteCollection();
            GlobalApplication.RegisterRoutes(routes);

            mocks = new MockRepository();

        }

        [Test]
        public void LoginRoute()
        {
            using (mocks.Record())
            {
                httpContext = GetHttpContext(mocks, "~/Login");
            }

            using (mocks.Playback())
            {
                RouteData routeData = routes.GetRouteData(httpContext);

                Assert.IsNotNull(routeData);
                Assert.AreEqual("UserInfo", routeData.Values["controller"]);
                Assert.AreEqual("LoginPage", routeData.Values["action"]);
                //Assert.AreEqual("1", routeData.Values["id"]);
            }

        }

        private static HttpContextBase GetHttpContext(MockRepository mocks, string url)
        {
            HttpContextBase httpContext = mocks.DynamicMock<HttpContextBase>();
            HttpRequestBase httpRequest = mocks.DynamicMock<HttpRequestBase>();
            HttpResponseBase httpResponse = mocks.DynamicMock<HttpResponseBase>();
            HttpSessionStateBase httpSession = mocks.DynamicMock<HttpSessionStateBase>();
            HttpServerUtilityBase httpServer = mocks.DynamicMock<HttpServerUtilityBase>();

            SetupResult.For(httpContext.Request).Return(httpRequest);
            SetupResult.For(httpContext.Response).Return(httpResponse);
            SetupResult.For(httpContext.Session).Return(httpSession);
            SetupResult.For(httpContext.Server).Return(httpServer);

            mocks.Replay(httpContext);

            SetupResult.For(httpContext.Request.AppRelativeCurrentExecutionFilePath).Return(url);

            return httpContext;
        }
    }
}
