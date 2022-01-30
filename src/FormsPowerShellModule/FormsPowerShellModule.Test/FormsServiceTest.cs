using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using FormsPowerShellModule.Models;

namespace FormsPowerShellModule.Test
{
    [TestClass]
    public class FormsServiceTest
    {
        private static string _tenantId;
        private static string _clientId;
        private static string _userName;
        private static string _password;
        private static string _demoUserId;
        private static string _formId;

        private static FormsService _formsService;
        
        [AssemblyInitialize]
        public static void Setup(TestContext context)
        {
            _tenantId = context.Properties["tenantId"] as string;
            _clientId = context.Properties["clientId"] as string;
            _userName = context.Properties["userName"] as string;
            _password = context.Properties["password"] as string;
            _demoUserId = context.Properties["demoUserId"] as string;
            _formId = context.Properties["formId"] as string;
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
