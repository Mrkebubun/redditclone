﻿using System;
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
    public class AccountControllerTest2
    {
        public AccountControllerTest2()
        {

        }
        private AccountController controller;
       

        [SetUp]
        public void Init()
        {

            controller = Isolate.Fake.Instance<AccountController>(Members.CallOriginal);
            Isolate.WhenCalled(() => controller.Provider)
                .ReturnRecursiveFake();
            Isolate.WhenCalled(() => controller.FormsAuth)
                .ReturnRecursiveFake();
        }

        [TearDown]
        public void Clean()
        {

        }


 

        [Test, RollBack, Isolated]
        public void UserInformationShow()
        {

            UserDataLayer udlFake = Isolate.Fake.Instance<UserDataLayer>(Members.MustSpecifyReturnValues);
            UserInfo info = Isolate.Fake.Instance<UserInfo>();
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

        [Test, RollBack, Isolated]
        public void Login_InvalidUser_EmptyString()
        {
            string username="";
            string password = "";
  
            ViewResult result = (ViewResult)controller.Login(username, password, true);
            Assert.Greater(((List<string>)result.ViewData["errors"]).Count, 0);
            Isolate.Verify.WasNotCalled(() => controller.Provider.ValidateUser("", ""));

        }        
        [Test, RollBack, Isolated]       
        public void Login_Succeed()
        {
            string username="hello";
            string password = "hello";
            Isolate.WhenCalled(() => controller.Provider.ValidateUser(username, password))
                .WillReturn(true);


            var result = (RedirectToRouteResult)controller.Login(username, password, false);
            Assert.AreEqual("Item", (result).RouteValues["controller"]);
            Assert.AreEqual("Main", (result).RouteValues["action"]);

            Isolate.Verify.WasCalledWithExactArguments(() => controller.FormsAuth.SetAuthCookie(username, false));


        }
        [Test, RollBack, Isolated]
        public void Login_InvalidUser_WrongPassword()
        {
            string username = "hello";
            string password = "hello";
            Isolate.WhenCalled(() => controller.Provider.ValidateUser(username, password))
                .WillReturn(false);
            var result= (ViewResult)controller.Login(username, password, false);
            Assert.Greater(((List<string>)result.ViewData["errors"]).Count, 0);
            

        }
        [Isolated]
        [Test, RollBack]
   
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
            Isolate.WhenCalled
                (() => controller.Provider.CreateUser(username, password, email,
                     string.Empty, string.Empty, true, null, out mcs))
                .ReturnRecursiveFake();

            RedirectToRouteResult result = (RedirectToRouteResult)controller.Register(username, email,
                password, password);
            Assert.AreEqual("Item", (result).RouteValues["controller"]);
            Assert.AreEqual("Main", (result).RouteValues["action"]);

          

        }
    }
}
