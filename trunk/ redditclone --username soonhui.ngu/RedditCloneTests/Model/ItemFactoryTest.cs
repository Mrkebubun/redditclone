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
        ItemFactory factory;
        public ItemFactoryTest()
        {

        }

        [SetUp]
        public void Init()
        {


            factory = new ItemFactory();
        }

        [RowTest, RollBack]
        [Row(1)]
        public void UpVoteCalculation(int articleID)
        {
            Article arr = factory.GetArticleID(articleID);
            Assert.AreEqual(2, arr.UpVotes);
        }

        [RowTest, RollBack]
        [Row(5)]
        public void DownVoteCalculation(int articleID)
        {
            Article arr = factory.GetArticleID(articleID);
            Assert.AreEqual(3, arr.DownVotes);
        }

        [RowTest, RollBack]
        [Row(5)]
        public void CountArticleIDInVoteHistory(int articleID)
        {
            List<VoteHistory> vHis = factory.GetVoteHistory(articleID);
            Assert.AreEqual(4, vHis.Count);
        }
        [RowTest, RollBack]
        [Row(1, "Joseph")]
        public void OppositeCastTest(int id, string voter)
        {

            int upVoteCount = factory.GetArticleID(id).UpVotes;
            int downVoteCount = factory.GetArticleID(id).DownVotes;

            factory.CastUpVote(id, voter);
            factory.CastDownVote(id, voter);
            Assert.AreEqual(upVoteCount, factory.GetArticleID(id).UpVotes);
            Assert.AreEqual(downVoteCount + 1, factory.GetArticleID(id).DownVotes);
        }

        [RowTest, RollBack]
        [Row("http://itscommonsensestupid.blogspot.com/2008/05/zephyr-test-management-tool.html", "Soon Hui")]
        public void GetLatestVoteHistory(string url, string voters)
        {
            VoteHistory vH = factory.GetLatestVoteHistory(url, voters);
            Assert.AreEqual(voters, vH.diggers);
            Assert.AreEqual(1, vH.voteChoice);



        }

        [RowTest, RollBack]
        [Row("http://www.dotnetkicks.com/", 5)]
        public void DeleteTest(string url, int articleID)
        {


            List<Article> articles1 =factory.SearchURL(url);
            Assert.AreEqual(1, articles1.Count, "THere should be only 1 article");
            factory.DeleteArticle(articleID);


            List<Article> articles = factory.SearchURL(url);
            Assert.AreEqual(0, articles.Count, "The article should have been deleted");

            List<VoteHistory> vHis = new ItemFactory().GetVoteHistory(articleID);
            Assert.AreEqual(0, vHis.Count);

        }

        [RowTest, RollBack]
        [Row("Soon Hui")]
        public void GetSubmittedArticles(string username)
        {
            List<Article> articles = factory.GetSubmittedArticles(username);
            Assert.AreEqual(4, articles.Count);
        }

        [RowTest, RollBack]
        [Row("Zephyr the Test Management Tool")]
        public void GetArticleHistoryTest(string articleName)
        {
            List<VoteHistory> vHis = factory.GetArticleHistory(articleName);
            Assert.AreEqual(2, vHis.Count);
            Assert.AreEqual("Aaron", vHis[0].diggers);
            Assert.AreEqual(VoteChoiceEnum.UpVote, (VoteChoiceEnum)vHis[0].voteChoice);

            Assert.AreEqual("Soon Hui", vHis[1].diggers);
            Assert.AreEqual(VoteChoiceEnum.UpVote, (VoteChoiceEnum)vHis[1].voteChoice);
        }

        [RowTest, RollBack]
        [Row("Zephyr the Test Management sgsgr")]
        public void GetArticleNoHistory(string articleName)
        {
            List<VoteHistory> vHis = factory.GetArticleHistory(articleName);
            Assert.AreEqual(0, vHis.Count);
        }




        [RowTest, RollBack]
        [Row("Soon Hui", "http://www.google.com", "Google")]
        [Row("Soon Hui", "http://www.yahoo.com", "yahoo")]
        [Row("Soon Hui", "http://www.live.com", "live")]
        public void SubmitTest(string owner, string url, string title)
        {


            factory.SubmitArticle(url, title, owner);

            List<Article> articles = factory.SearchURL(url);
            Assert.AreEqual(1, articles.Count);
            Assert.AreEqual(owner, articles[0].Diggers);
            Assert.AreEqual(title, articles[0].Title);
            Assert.AreEqual(1, articles[0].UpVotes);
            Assert.AreEqual(0, articles[0].DownVotes);

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
