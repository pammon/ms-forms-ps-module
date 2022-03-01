using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FormsPowerShellModule.Models;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FormsPowerShellModule
{
    public partial class WebBrowserFactory : Form
    {
        private const int DefaultWidth = 571;
        private const int DefaultHeight = 782;


        private TaskCompletionSource<FormsApiAuthenticationInformation> _cookiesTask;

        private WebView2 _webView2;
        private string _url;

        public WebBrowserFactory()
        {
            Opacity = 0.0;
            var defaultHeight = (int)Math.Floor(Screen.PrimaryScreen.WorkingArea.Height * 0.7);
            SuspendLayout();

            ClientSize = new Size(DefaultWidth, defaultHeight);
            StartPosition = FormStartPosition.CenterScreen;
            AutoScaleMode = AutoScaleMode.Font;
            MinimizeBox = false;
            MaximizeBox = false;
            ShowInTaskbar = true;
            ShowIcon = true;
            var webBrowserPanel = new Panel();
            webBrowserPanel.SuspendLayout();
            webBrowserPanel.Dock = DockStyle.Fill;
            webBrowserPanel.BorderStyle = BorderStyle.None;
            webBrowserPanel.Location = new Point(0, 0);
            webBrowserPanel.Name = "browserPanel";
            webBrowserPanel.Size = new Size(DefaultWidth, DefaultHeight);
            webBrowserPanel.TabIndex = 2;

            _webView2 = new WebView2()
            {
                Dock = DockStyle.Fill,
                Location = new Point(0, 26),
                TabIndex = 1,
                MinimumSize = new Size(20, 20),
                BackColor = Color.Aquamarine
            };

            webBrowserPanel.Controls.Add(_webView2);
            Controls.Add(_webView2);
            webBrowserPanel.ResumeLayout(false);
            ResumeLayout(false);
            Closed += OnClosed;
        }

        private void SetBrowserOpacity()
        {
            new Task(() =>
            {
                Thread.Sleep(1000);
                if (!_cookiesTask.Task.IsCompleted || !_cookiesTask.Task.IsCanceled)
                {
                    BeginInvoke((new Action(() => Opacity = 1.0)));
                }
            }).Start();
        }

        private async void InitWebView()
        {
            var userDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\FormsPowerShellCmdlet";
            var env = await CoreWebView2Environment.CreateAsync(null, userDataFolder);
            await _webView2.EnsureCoreWebView2Async(env);
            _webView2.CoreWebView2.NavigationCompleted += CoreWebView2OnNavigationCompleted;
            _webView2.CoreWebView2.Navigate(_url);
        }

        private async void CoreWebView2OnNavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            if (_webView2.CoreWebView2.Source.ToLower().Contains("redirecturl"))
            {
                SetBrowserOpacity();
            }
            else if (_webView2.CoreWebView2.Source.ToLower().Contains("accessdenied.aspx"))
            {
                _cookiesTask.TrySetException(new Exception("access denied"));
                Close();
            }

            else if (_webView2.CoreWebView2.Source.Equals(_url,
                StringComparison.InvariantCultureIgnoreCase))
            {
                List<CoreWebView2Cookie> cookies = await _webView2.CoreWebView2.CookieManager.GetCookiesAsync("https://forms.office.com");

                CoreWebView2Cookie requestverificationtoken = cookies.SingleOrDefault(c =>
                    c.Name.Trim().Equals("__requestverificationtoken", StringComparison.InvariantCultureIgnoreCase));

                CoreWebView2Cookie aadAuthForms = cookies.SingleOrDefault(c =>
                    c.Name.Trim().Equals("AADAuth.forms", StringComparison.InvariantCultureIgnoreCase));

                string html = await _webView2.CoreWebView2.ExecuteScriptAsync("document.documentElement.outerHTML");

                Match antiForgeryTokenMatch = Regex.Match(html, "antiForgeryToken:([^,]*)");
                Match tenantIdMatch = Regex.Match(html, "TenantId([^,]*)");

                if (antiForgeryTokenMatch.Success && antiForgeryTokenMatch.Groups.Count == 2 && tenantIdMatch.Success && tenantIdMatch.Groups.Count == 2)
                {
                    string antiForgeryToken = antiForgeryTokenMatch.Groups[1].Value.Trim(' ', '\\', '"');
                    string tenantId = tenantIdMatch.Groups[1].Value.Trim(' ', '\\', '"', ':');


                    if (requestverificationtoken != null && aadAuthForms != null &&
                        !string.IsNullOrEmpty(antiForgeryToken))
                    {

                        _cookiesTask.SetResult(new FormsApiAuthenticationInformation(antiForgeryToken, requestverificationtoken, aadAuthForms, tenantId));
                         DialogResult = DialogResult.OK;
                        Close();
                    }

                }
            }
        }

        private void OnClosed(object sender, EventArgs e)
        {
            if (this.DialogResult != DialogResult.OK)
            {
                _cookiesTask.TrySetException(new Exception("login canceled by user"));
            }
        }

        public async Task<FormsApiAuthenticationInformation> AcquireToken()
        {
            _url = "https://forms.office.com/Pages/delegatepage.aspx";
            _cookiesTask = new TaskCompletionSource<FormsApiAuthenticationInformation>();
            InitWebView();
            ShowDialog();
            FormsApiAuthenticationInformation frm = await _cookiesTask.Task;
            _webView2.Dispose();
            return frm;
        }

    }
}
