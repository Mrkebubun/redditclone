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
        private UserInfoController controller;
       

        [SetUp]
        public void Init()
        {
            
            controller = Isolate.Fake.Instance<UserInfoController>(Members.CallOriginal);
            Isolate.WhenCalled(() => controller.Provider).WillReturn(Isolate.Fake.Instance<MembershipProvider>(Members.ReturnRecursiveFakes));
            Isolate.WhenCalled(() => controller.FormsAuth).WillReturn(Isolate.Fake.Instance<IFormsAuthentication>(Members.ReturnRecursiveFakes));

        }

        [TearDown]
        public void Clean()
        {

        }


 

        [Test, RollBack, Isolated]
        public void UserInformationShow()
        {

            UserDataLayer udlFake = Isolate.Fake.Instance<UserDataLayer>(Members.MustSpecifyReturnValues);
            UserInfo info = Isolate.Fake.Instance<UserInfo>(Members.ReturnRecursiveFakes);
            Isolate.WhenCalled(()=>udlFake.GetUserInfo(string.Empty)).WillReturn(info);

            Isolate.Swap.NextInstance<UserDataLayer>().With(udlFake);

            ViewResult result =(ViewResult)controller.UserInformation(string.Empty);
            Assert.AreEqual(info, result.ViewData.Model);
            Assert.AreEqual("UserInformation", result.ViewName);
        
        }

        [Test, RollBack, Isolated]
        public void LoginGetTest()
        {

            ViewResult result = (ViewResult)controller.Login();
            Assert.AreEqual("Login", controller.ViewData["Title"]);
            Assert.IsNull(controller.ViewData["errors"]);
            
        }

        [RowTest, RollBack, Isolated]
        [Row("gesghhrs", "rtyhdthdt")]
        public void LoginFailNoUserTest(string username, string password)
        {
            Isolate.WhenCalled(() => controller.Provider.ValidateUser(username, password)).WillReturn(false);  
            ViewResult result = (ViewResult)controller.Login(username, password, true);
            Assert.Greater(((List<string>)controller.ViewData["errors"]).Count, 0);

        }
        [Isolated]
        [RowTest, RollBack]
        [Row("Joseph", "Joseph")]
        public void LoginTest(string username, string password)
        {
 
            Isolate.WhenCalled(() => controller.Provider.ValidateUser(null, password)).WillReturn(true);


            RedirectToRouteResult result = (RedirectToRouteResult)controller.Login(username, password, false);
            Assert.AreEqual("Item", (result).Values["controller"]);
            Assert.AreEqual("Main", (result).Values["action"]);

            Isolate.Verify.WasCalledWithExactArguments(() => controller.Provider.ValidateUser(username, password));
            Isolate.Verify.WasCalledWithExactArguments(() => controller.FormsAuth.SetAuthCookie(username, false));


        }
        [RowTest, RollBack, Isolated]
        [Row("Joseph", "rtyhdthdt")]
        public void LoginFailTest(string username, string password)
        {
            Isolate.WhenCalled(() => controller.Provider.ValidateUser(username, password)).WillReturn(false);
            controller.Login(username, password, false);
            Assert.Greater(((List<string>)controller.ViewData["errors"]).Count, 0);

            Isolate.Verify.WasCalledWithExactArguments(() => controller.Provider.ValidateUser(username, password));

        }
        [Isolated]
        [RowTest, RollBack]
        [Row()]
        public void AddUserGetTest()
        {
            ViewResult result = (ViewResult)controller.Register();
            Assert.AreEqual("Registration", result.ViewData["title"]);

        }
        [Isolated]
        [RowTest, RollBack]
        [Row("Dennis2", "Dennis2", "myemail@gmail.com")]
        public void AddUserTest(string username, string password, string email)
        {


            MembershipCreateStatus mcs;
            Isolate.WhenCalled(() => controller.Provider.CreateUser(username, password, email, string.Empty, string.Empty, false, null, out mcs)).WillReturn(null);

            RedirectToRouteResult result = (RedirectToRouteResult)controller.Register(username, password, email);
            Assert.AreEqual("Item", (result).Values["controller"]);
            Assert.AreEqual("Main", (result).Values["action"]);

            
            Isolate.Verify.WasCalledWithExactArguments(() => controller.Provider.CreateUser(username, password, email, string.Empty, string.Empty, true, null, out mcs));

        }
    }
}
