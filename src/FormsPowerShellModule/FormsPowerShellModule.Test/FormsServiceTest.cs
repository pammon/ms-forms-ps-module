using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
        
        [AssemblyInitialize]
        public static void Setup(TestContext context)
        {
            _tenantId = context.Properties["tenantId"] as string;
            _clientId = context.Properties["clientId"] as string;
            _userName = context.Properties["userName"] as string;
            _password = context.Properties["password"] as string;
            _demoUserId = context.Properties["demoUserId"] as string;
        }

        [TestMethod]
        public void TestConnect()
        {   
            try
            {
                FormsService.Connect(_tenantId, _clientId, _userName, _password.ToSecureString());
            }
            catch(Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }


        [TestMethod]
        public void TestGetForms()
        {
            FormsService.Connect(_tenantId, _clientId, _userName, _password.ToSecureString());
            Forms[] forms = FormsService.Get(_demoUserId);
            Assert.IsNotNull(forms);
            Assert.IsTrue(forms.Length > 0);
            foreach (Forms form in forms)
            {
                Assert.IsFalse(string.IsNullOrEmpty(form.Id));
                Assert.IsFalse(string.IsNullOrEmpty(form.Title));
            }
        }

        [TestMethod]
        public void TestGetFormsWithFields()
        {
            FormsService.Connect(_tenantId, _clientId, _userName, _password.ToSecureString());
            Forms[] forms = FormsService.Get(_demoUserId, new[] { "id" });
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
            FormsService.Connect(_tenantId, _clientId, _userName, _password.ToSecureString());
            Forms[] forms = FormsService.Get(_demoUserId, new []{"Id", "Title"});
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
