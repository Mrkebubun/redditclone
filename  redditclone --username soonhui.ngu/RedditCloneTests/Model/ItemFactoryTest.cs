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
    public class ItemFactoryTest
    {
        ItemFactory factoryFake;
        ItemFactory factory;
        public ItemFactoryTest()
        {

        }

        [SetUp]
        public void Init()
        {
            factoryFake = Isolate.Fake.Instance<ItemFactory>(Members.CallOriginal);
            Isolate.Swap<ItemFactory>().With(factoryFake);

            factory = new ItemFactory();
        }
        [Test]
        public void GetHotArticle()
        {
            List<Article> articles= factory.GetHotArticles();
            Assert.Greater(articles.Count, 0);
        }

        [Test]
        public void WhatsNew()
        {
            List<Article> articles = factory.GetNewestArticles();
            Assert.IsTrue(articles.Count >= 2);
            for (int i = 0; i < articles.Count - 1; i++)
            {
                Assert.GreaterEqualThan(articles[i].submittedDate, articles[i + 1].submittedDate);
            }
        }

        [RowTest, RollBack]
        [Row(1, "Joseph")]
        public void CastUpVote(int id, string digger)
        {
            factory.CastUpVote(id, digger);
            Assert.AreEqual(3, factory.GetArticleID(id).UpVotes);
        }

        [RowTest, RollBack]
        [Row(1, "Joseph")]
        public void DoubleCastTest(int id, string voter)
        {
            factory.CastUpVote(id, voter);
            int upVoteCount = factory.GetArticleID(id).UpVotes;
            factory.CastUpVote(id, voter);
            Assert.AreEqual(upVoteCount, factory.GetArticleID(id).UpVotes);
           
        }

        [RowTest, RollBack]
        [Row(1, "Joseph")]
        public void CastDownVoteTest(int id, string digger)
        {

            factory.CastDownVote(id, digger);

            Assert.AreEqual(1, factory.GetArticleID(id).DownVotes);
        }
    }
}
