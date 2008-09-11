using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web.Mvc;
using System.Web.Routing;
using System.Web;
using System.Collections.Specialized;

using RedditClone.Controllers;

using Rhino.Mocks;
namespace RedditCloneTests.Controllers
{
    public static class ControllerTestHelper
    {
        public static void CreateMockController(Controller controller,MockRepository mocks)
        {
            HttpContextBase httpContext;
            HttpRequestBase httpRequest;
            using (mocks.Record())
            {
                httpContext = mocks.DynamicMock<HttpContextBase>();
                httpRequest = mocks.DynamicMock<HttpRequestBase>();


                SetupResult.For(httpRequest.HttpMethod).Return("POST");
                HttpResponseBase httpResponse = mocks.DynamicMock<HttpResponseBase>();

                //SetupResult.For(httpRequest.Form).Return(nvm);
                SetupResult.For(httpContext.Request).Return(httpRequest);
                SetupResult.For(httpContext.Response).Return(httpResponse);

            }
            ControllerContext c = new ControllerContext(httpContext, new RouteData(), controller);
            controller.ControllerContext = c;


        }
    }
}
