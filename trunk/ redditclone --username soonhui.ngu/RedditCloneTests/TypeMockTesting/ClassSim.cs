using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;

using System.Web.Mvc;
namespace RedditCloneTests.TypeMockTesting
{
    public interface IItemFactory 
    {
        void Submit(string url);
    }
    public class ItemFactory:IItemFactory
    {

        #region IItemFactory Members

        public void Submit(string url)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public class ItemController:Controller
    {

        public RedditCloneTests.TypeMockTesting.IItemFactory Factory
        {
            get;private set;
        
        }
        public ItemController()
            : this(null)
        {

        }
        public ItemController(IItemFactory itemFactory)
        {
            Factory = itemFactory ?? new ItemFactory();
        }

        public ActionResult Login()
        {
            return View("Hi", 1);
        }

        public ActionResult Submit(string url)
        {
            Factory.Submit(url);
            return RedirectToAction("Main");
        }
    }
    public class ClassSim
    {
        public void DoNothing()
        {

        }

        public ClassSim()
        {

        }
    }

    public class ClassComp
    {
        public ClassSim sim
        {
            get;
            set;
        }

        public MembershipProvider Provider
        {
        get;
        set;
    }
        public ClassComp()
        {
            sim=new ClassSim();
        }
        public void CallClassSim()
        {
            sim.DoNothing();
        }

        public void CallMemberShip(string username, string answer)
        {
            MembershipCreateStatus mcs;
            Provider.CreateUser(username, answer, string.Empty, string.Empty, string.Empty, false, null, out mcs);
        }
    }

    public class MyMemberShipProvider : MembershipProvider
    {
        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }
        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }
        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }
        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }
        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }
        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }
        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }
        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }
        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }
        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }
        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }
        public override bool ValidateUser(string username, string password)
        {
            throw new NotImplementedException();
        }
        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }
        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }
        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }
        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }
        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }
        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            throw new NotImplementedException();
        }
        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }
        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }
        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }
        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }
        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }
        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }
        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {

            throw new NotImplementedException();

        }
    }
}
