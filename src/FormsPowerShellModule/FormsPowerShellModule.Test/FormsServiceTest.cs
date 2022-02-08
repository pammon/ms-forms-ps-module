using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using FormsPowerShellModule.Models;
using Microsoft.Web.WebView2.Core;

namespace FormsPowerShellModule.Test
{
    [TestClass]
    public class FormsServiceTest
    {
        private string _tenantId;
        private string _clientId;
        private string _userName;
        private string _password;
        private string _demoUserId;
        private string _groupId;
        private string _formId;
        private string _newOwnerId;

        private static FormsService _formsService;
        
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void Setup()
        {
            _tenantId = TestContext.Properties["tenantId"] as string;
            _clientId = TestContext.Properties["clientId"] as string;
            _userName = TestContext.Properties["userName"] as string;
            _password = TestContext.Properties["password"] as string;
            _demoUserId = TestContext.Properties["demoUserId"] as string;
            _groupId = TestContext.Properties["groupId"] as string;
            _formId = TestContext.Properties["formId"] as string;
            _newOwnerId = TestContext.Properties["newOwnerId"] as string;
            _formsService = new FormsService(_tenantId, _clientId, _userName, _password.ToSecureString());
        }
        
        [TestMethod]
        public void TestConnect()
        {   
            try
            {
                _formsService.Connect();
            }
            catch(Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void TestModerBrowser()
        {
            try
            {
                WebBrowserFactory mb = new WebBrowserFactory();
                FormsApiAuthenticationInformation formsApiAuthenticationInformation = mb.AcquireToken().GetAwaiter().GetResult();
                Assert.IsNotNull(formsApiAuthenticationInformation.AadAuthForms);
                Assert.IsNotNull(formsApiAuthenticationInformation.AntiForgeryToken);
                Assert.IsNotNull(formsApiAuthenticationInformation.RequestVerificationToken);

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        [TestMethod]
        public void TestUpdateForm()
        {
            _formsService.Connect();
            Assert.IsTrue(FormsService.Instance.UpdateFormSettings(_demoUserId, _formId, true, "thanks ms for nothing. assholes")
                .GetAwaiter().GetResult());
        }

        [TestMethod]
        public void TestMoveFormToGroup()
        {
            _formsService.Connect();
            Assert.IsTrue(FormsService.Instance.MoveFormToGroup( _demoUserId, _formId, _groupId)
                .GetAwaiter().GetResult());
        }

        [TestMethod]
        public void TestMoveFormToUser()
        {
            _formsService.Connect();
            Assert.IsTrue(FormsService.Instance.MoveFormToUser( _demoUserId, _formId, _newOwnerId)
                .GetAwaiter().GetResult());
        }

        [TestMethod]
        public void TestGetForms()
        {
            _formsService.Connect();
            Forms[] forms = FormsService.Instance.GetForms(_demoUserId);
            Assert.IsNotNull(forms);
            Assert.IsTrue(forms.Length > 0);
            foreach (Forms form in forms)
            {
                Assert.IsFalse(string.IsNullOrEmpty(form.Id));
                Assert.IsFalse(string.IsNullOrEmpty(form.Title));
            }
        }

        [TestMethod]
        public void TestDownload()
        {
            _formsService.Connect();
            FormsService.Instance.DownloadDownloadExcelFile(_formId, $"{_formId}.xlsx");
        }


        [TestMethod]
        public void TestGeAllForms()
        {
            _formsService.Connect();
            Forms[] forms = FormsService.Instance.GetForms();
            Assert.IsNotNull(forms);
            Assert.IsTrue(forms.Length > 0);
            foreach (Forms form in forms)
            {
                Assert.IsFalse(string.IsNullOrEmpty(form.Id));
                Assert.IsFalse(string.IsNullOrEmpty(form.Title));
            }
        }

        [TestMethod]
        public void TestGeAllFormsFromDeletedUsers()
        {
            _formsService.Connect();
            Forms[] forms = FormsService.Instance.GetFormsFromDeletedUsers(new string[]{"Id", "Title", "CreatedBy", "OwnerId" }.ToList());
            Assert.IsNotNull(forms);
            Assert.IsTrue(forms.Length > 0);
            foreach (Forms form in forms)
            {
                Assert.IsFalse(string.IsNullOrEmpty(form.Id));
                Assert.IsFalse(string.IsNullOrEmpty(form.Title));
                Assert.IsFalse(string.IsNullOrEmpty(form.CreatedBy));
                Assert.IsFalse(string.IsNullOrEmpty(form.OwnerId));
            }
        }

        [TestMethod]
        public void TestGetUsers()
        {
            UserService userService = new UserService(_tenantId, _clientId, _userName, _password.ToSecureString());
            userService.Connect();
            User[] users = userService.GetUsers(2);
            Assert.IsNotNull(users);
            Assert.IsTrue(users.Length > 2);
        }

        [TestMethod]
        public void TestGetDeletedUsers()
        {
            UserService userService = new UserService(_tenantId, _clientId, _userName, _password.ToSecureString());
            userService.Connect();
            User[] users = userService.GetDeletedUsers(2);
            Assert.IsNotNull(users);
            Assert.IsTrue(users.Length > 2);
        }

        [TestMethod]
        public void TestGetFormsWithFields()
        {
            _formsService.Connect();
            Forms[] forms = _formsService.GetForms(_demoUserId, new[] { "id" }.ToList());
            Assert.IsNotNull(forms);
            Assert.IsTrue(forms.Length > 0);
            foreach (Forms form in forms)
            {
                Assert.IsFalse(string.IsNullOrEmpty(form.Id));
                Assert.IsTrue(string.IsNullOrEmpty(form.Title));
            }
        }

        [TestMethod]
        public void TestGetFormsWithFieldsUpperCase()
        {
            _formsService.Connect();
            Forms[] forms = _formsService.GetForms(_demoUserId, new []{"Id", "Title"}.ToList());
            Assert.IsNotNull(forms);
            Assert.IsTrue(forms.Length > 0);
            foreach (Forms form in forms)
            {
                Assert.IsFalse(string.IsNullOrEmpty(form.Id));
                Assert.IsFalse(string.IsNullOrEmpty(form.Title));
            }
        }
    }
}
