using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TypeMock;
using TypeMock.ArrangeActAssert;
using MbUnit.Framework;

using RedditClone.Models;

namespace RedditCloneTests.Model
{
    [TestFixture]
    public class UserDataLayerTest
    {
        private UserDataLayer userData;
        public UserDataLayerTest()
        {
            

        }

        [SetUp]
        public void Init()
        {
            userData = new UserDataLayer();
        }
        [RowTest, RollBack]
        [Row("Daniel", 0)]
        [Row("Soon Hui", 3)]
        [Row("Aaron", 1)]
        public void Reputation(string username, int reputation)
        {
            Assert.AreEqual(reputation, userData.GetUserInfo(username).Reputation);

        }


    }
}
