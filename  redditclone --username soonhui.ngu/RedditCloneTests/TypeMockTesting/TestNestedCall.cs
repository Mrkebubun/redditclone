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
            Isolate.WhenCalled(()=>compFake.sim).WillReturn(simFake);

            Isolate.Swap<ClassComp>().With(compFake);
            ClassComp comp = new ClassComp();
            comp.CallClassSim();
            Isolate.Verify.WasCalledWithExactArguments(()=>simFake.DoNothing());
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

            Isolate.Swap<ClassComp>().With(compFake);
            ClassComp comp = new ClassComp();
            comp.CallMemberShip("user", "password");
            Isolate.Verify.WasCalledWithExactArguments(() => membershipFake.CreateUser("user", "password", string.Empty, string.Empty, string.Empty, false, null, out mcs));
        }
    }
}
