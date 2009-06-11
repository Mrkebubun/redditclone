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

using RedditClone.Models;

using MbUnit.Framework;
using TypeMock;
using TypeMock.ArrangeActAssert;

namespace RedditCloneTests.Model
{
    [TestFixture]
    public class RedditMembershipTest
    {
        MembershipProvider provider;
        public RedditMembershipTest()
        {

        }

        [SetUp]
        public void Init()
        {
            provider = Membership.Provider;
        }

        [RowTest, RollBack]
        [Row("Joseph", "rtyhdthdt")]
        public void ValidationTest_Failed_WrongPassword(string username, string password)
        {
            Assert.IsFalse(provider.ValidateUser(username, password));
        }

        [RowTest, RollBack]
        [Row("Joseph", "Joseph")]
        public void ValidationTest_Pass(string username, string password)
        {
            Assert.IsTrue(provider.ValidateUser(username, password));
        }

        [RowTest, RollBack]
        [Row("Dennis2", "Dennis2", "myemail@gmail.com")]
        public void Register_Succeed(string username, string password, string email)
        {
            MembershipCreateStatus mcs;
            MembershipUser user = provider.CreateUser(username, password, email, string.Empty, string.Empty, false, null, out mcs);
            Assert.AreEqual(email, user.Email);
            Assert.AreEqual(username, user.UserName);
            Assert.AreEqual(password, user.GetPassword());
        }
    }
}
