using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Specialized;

using RedditClone.Controllers;
using RedditClone.Models;
using MbUnit.Framework;

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
        [Row("Dennis", "Dennis")]
        public void AddUserTest(string username, string password)
        {
            NameValueCollection nvm = new NameValueCollection();
            nvm.Add("username", username);
            nvm.Add("password", password);
            UserInfoController controller = new UserInfoController();
            controller.AddUser();

            new UserDataLayer().GetUserInfo(username);
            throw new NotImplementedException();
            
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
    }
}
