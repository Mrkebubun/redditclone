using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RedditClone.Controllers;
using RedditClone.Models;
using Rhino.Mocks;

namespace RedditCloneTests.Controllers
{
    /// <summary>
    /// Summary description for ItemControllerTest
    /// </summary>
    [TestClass]
    public class ItemControllerTest
    {
        public ItemControllerTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        private MockRepository mocks;
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
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

        [TestInitialize]
        public void Init()
        {

            mocks = new MockRepository();

        }

        [TestMethod]
        public void DisplayItemTest()
        {
            ItemControllerTester controller = new ItemControllerTester();
            controller.Main();
            Assert.AreEqual("Main", controller.SelectedViewName);
             Assert.IsInstanceOfType(controller.SelectedViewData, typeof(List<Article>));

        }

        [TestMethod]
        public void NonsenseTest()
        {
            System.Collections.ArrayList arr1 = new System.Collections.ArrayList();
            System.Collections.ArrayList arr2 = new System.Collections.ArrayList();
            arr1.Add(1);
            arr1.Add(2);

            arr2.Add(1);
            arr2.Add(2.0);
            Assert.AreEqual(arr1[0], arr2[0]);
            Assert.AreNotEqual(arr1[1], arr2[1]);
        }


    }

    public class ItemControllerTester : ItemController
    {
        public string SelectedViewName { get; private set; }

        /// <summary>
        /// following the ugly examples of phill haack
        /// </summary>
        public object SelectedViewData { get; private set; }

        protected override void RenderView(string viewName
          , string masterName, object viewData)
        {
            this.SelectedViewName = viewName;
            SelectedViewData = viewData;
     //       base.RenderView(viewName, masterName, viewData);
        }
    }
}
