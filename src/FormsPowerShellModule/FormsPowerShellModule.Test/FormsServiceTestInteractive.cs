using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using FormsPowerShellModule.Models;

namespace FormsPowerShellModule.Test
{
    [TestClass]
    public class FormsServiceTestInteractive
    {
        private string _tenantId;
        private string _clientId;
        private string _demoUserId;
        private string _formId;

        private FormsService _formsService;

        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void Setup()
        {
            //_tenantId = TestContext.Properties["tenantId"] as string;
            //_clientId = TestContext.Properties["clientId"] as string;
            _demoUserId = TestContext.Properties["demoUserId"] as string;
            _formId = TestContext.Properties["formId"] as string;
            _formsService = new FormsService(_tenantId, _clientId);
        }
        
        [TestMethod]
        public void TestConnectInteractive()
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
        public void TestGetFormsInteractive()
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
        public void TestDownloadInteractive()
        {
            _formsService.Connect();
            FormsService.Instance.DownloadDownloadExcelFile(_formId, $"{_formId}.xlsx");
        }


        [TestMethod]
        public void TestGeAllFormsInteractive()
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
        public void TestGeAllFormsFromDeletedUsersInteractive()
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
        public void TestGetUsersInteractive()
        {
            UserService userService = new UserService(_tenantId, _clientId);
            userService.Connect();
            User[] users = userService.GetUsers(2);
            Assert.IsNotNull(users);
            Assert.IsTrue(users.Length > 2);
        }

        [TestMethod]
        public void TestUpdateFormInteractive()
        {
            _formsService.Connect();
            Assert.IsTrue(FormsService.Instance.UpdateFormSettings(_demoUserId, _formId, true, "thanks ms for nothing")
                .GetAwaiter().GetResult());
        }


        [TestMethod]
        public void TestGetFormQuestionsInteractive()
        {
            _formsService.Connect();
            var questions = FormsService.Instance.GetFormQuestions(_demoUserId, _formId).GetAwaiter().GetResult();
            Assert.IsNotNull(questions);
            Assert.IsTrue(questions.Length >= 1);
        }


        [TestMethod]
        public void TestGetFormResponsesInteractive()
        {
            _formsService.Connect();
            var responses = FormsService.Instance.GetFormResponses(_demoUserId, _formId).GetAwaiter().GetResult();
            Assert.IsNotNull(responses);
        }

        [TestMethod]
        public void TestGetDeletedUsersInteractive()
        {
            UserService userService = new UserService(_tenantId, _clientId);
            userService.Connect();
            User[] users = userService.GetDeletedUsers(2);
            Assert.IsNotNull(users);
            Assert.IsTrue(users.Length > 2);
        }

        [TestMethod]
        public void TestGetFormsWithFieldsInteractive()
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
        public void TestGetFormsWithFieldsUpperCaseInteractive()
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
