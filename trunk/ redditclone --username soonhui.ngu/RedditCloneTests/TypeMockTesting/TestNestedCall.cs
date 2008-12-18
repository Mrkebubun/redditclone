using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MbUnit.Framework;
using TypeMock;
using TypeMock.ArrangeActAssert;
using System.Web.Security;
namespace RedditCloneTests.TypeMockTesting
{
     [TestFixture, ClearMocks]
    public class ItemFactoryDifficult
    {
         [Test, Isolated]
         public void TestConcrete()
         {
             ItemController controller = Isolate.Fake.Instance<ItemController>(Members.CallOriginal);
             ItemFactory factory = Isolate.Fake.Instance<ItemFactory>(Members.ReturnRecursiveFakes);
             Isolate.WhenCalled(() => controller.Factory).WillReturn(factory);
             controller.Submit("");
             Isolate.Verify.WasCalledWithExactArguments(()=>controller.Factory.Submit(""));
         }

         [Test, Isolated]
         public void TestAbstract()
         {
             ItemController controller = Isolate.Fake.Instance<ItemController>(Members.CallOriginal);
             IItemFactory factory = Isolate.Fake.Instance<IItemFactory>(Members.ReturnRecursiveFakes);
             Isolate.WhenCalled(() => controller.Factory).WillReturn(factory);
             controller.Submit("");
             Isolate.Verify.WasCalledWithExactArguments(() => controller.Factory.Submit(""));
         }
    }
    [TestFixture, ClearMocks]
    public class TestItemFactory
    {
        private ItemController controllerFake;
        private ItemController controller;
        private ItemFactory itemFactoryFake;
        public TestItemFactory()
        {

        }


        [SetUp]
        public void Init()
        {

            controllerFake = Isolate.Fake.Instance<ItemController>(Members.CallOriginal);
            Isolate.Swap.NextInstance<ItemController>().With(controllerFake);

            controller = new ItemController();
        }

        [Test, Isolated, Ignore("Not relevant")]
        public void TestLogin()
        {
            Isolate.NonPublic.WhenCalled(controllerFake, "View").WithGenericArguments(typeof(string), typeof(object)).WillReturn(null);
            controllerFake.Login();
        }


    }

    [TestFixture]
    public class TestNestedCall
    {
        public TestNestedCall()
        {

        }

        [Test]
        [Isolated]
        public void TestNest()
        {
            ClassComp compFake = Isolate.Fake.Instance<ClassComp>(Members.CallOriginal);
            ClassSim simFake = Isolate.Fake.Instance<ClassSim>();
            Isolate.WhenCalled(() => compFake.sim).WillReturn(simFake);

            Isolate.Swap.NextInstance<ClassComp>().With(compFake);
            ClassComp comp = new ClassComp();
            comp.CallClassSim();
            Isolate.Verify.WasCalledWithExactArguments(() => simFake.DoNothing());
        }

        [Test]
        [Isolated]
        public void TestMembership()
        {
            ClassComp compFake = Isolate.Fake.Instance<ClassComp>(Members.CallOriginal);
            MyMemberShipProvider membershipFake = Isolate.Fake.Instance<MyMemberShipProvider>();
            MembershipCreateStatus mcs;
            Isolate.WhenCalled(() => membershipFake.CreateUser(null, null, null, string.Empty, string.Empty, false, null, out mcs)).WillReturn(null);
            Isolate.WhenCalled(() => compFake.Provider).WillReturn(membershipFake);

            Isolate.Swap.NextInstance<ClassComp>().With(compFake);
            ClassComp comp = new ClassComp();
            comp.CallMemberShip("user", "password");
            Isolate.Verify.WasCalledWithExactArguments(() => membershipFake.CreateUser("user", "password", string.Empty, string.Empty, string.Empty, false, null, out mcs));
        }


    }

    [TestFixture]
    public class TestNaturalMock
    {
        public TestNaturalMock()
        {

        }

        [Test]
        public void Test23()
        {
            using (RecordExpectations recorder = RecorderManager.StartRecording())
            {

            }
            Assert.AreEqual(0, 0);
        }
    }
}
