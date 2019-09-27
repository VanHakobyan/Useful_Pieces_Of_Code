using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Timers;
using HtmlAgilityPack;
using NLog;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace FxTrading
{
    public class FxScrapper
    {
        private Logger Logger = LogManager.GetLogger("Fx");
        private static Model _model = new Model { Date = "" };
        private static string FirefoxProfilePath = $@"{ConfigurationManager.AppSettings["Profile"]}";
        public static FirefoxDriver _driver = new FirefoxDriver(new FirefoxOptions { Profile = InitFirefox() });
        private static System.Timers.Timer Timer = new System.Timers.Timer(TimeSpan.FromMinutes(30).TotalMilliseconds);


        public FxScrapper()
        {
            CloseUnnecessaryTabs();
            Thread.Sleep(5000);
            Login();
            Timer.Elapsed += TimerOnElapsed;
            Timer.Start();
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            CheckPrice();
        }

        private static FirefoxProfile InitFirefox()
        {
            var profile = new FirefoxProfile(FirefoxProfilePath);
            return profile;
        }
        public void CheckPrice()
        {
            try
            {
                GetContentAndSendEmail();
            }
            catch (Exception e)
            {
                Login();
                Logger.Error(e, "CheckPrice");
            }
        }
        public void GetContentAndSendEmail()
        {
            _driver.Navigate().GoToUrl(@"https://office.fxtradingcorp.com/pay/history/");
            HtmlDocument document = new HtmlDocument();
            Thread.Sleep(5000);
            document.LoadHtml(_driver.PageSource);
            var table = document.DocumentNode.SelectSingleNode(".//table[@class='table']");
            var trs = table.SelectNodes(".//tr");
            var lastPriceDiv = trs[1];
            var items = lastPriceDiv.SelectNodes(".//td").Select(x => x.InnerText).ToList();
            var model = new Model { Daily = items[0], Date = items[1], Price = items[2] };
            if (model.Date != _model.Date)
            {
                SendEmail($"{model.Daily}");
                _model = model;
            }
        }

        private void SendEmail(string content)
        {
            Logger.Info("Start email send !!!");
            using (var msg = new MailMessage())
            {
                msg.From = new MailAddress("fxtradingcorparmenia@gmail.com");
                foreach (var mail in File.ReadAllLines(@"D:\fxmails.txt"))
                {
                    msg.To.Add(mail);
                }

                msg.Subject = "FX-ի մամենտով";
                msg.IsBodyHtml = false;
                msg.Body = content;

                var client = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("fxtradingcorparmenia@gmail.com", "VAN606580$$$$")
                };

                try
                {
                    client.Send(msg);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex, "Email");
                }

            }

        }

        public void Login()
        {
            try
            {
                var credential = LoadCredential();
                HtmlDocument document = new HtmlDocument();
                _driver.Navigate().GoToUrl(@"https://office.fxtradingcorp.com/account/login/");
                Thread.Sleep(5000);
                document.LoadHtml(_driver.PageSource);
                var email = _driver.FindElementByXPath(".//input[@id='id_auth-username']");
                var password = _driver.FindElementByXPath(".//input[@id='id_auth-password']");
                email?.SendKeys(credential.Item1);
                password?.SendKeys(credential.Item2);
                var signin = _driver.FindElementByXPath(".//button[contains(@class,'btn-register')]");

                signin?.Click();
            }
            catch (Exception e)
            {
                Logger.Error(e,"login");
            }
        }

        public (string, string) LoadCredential()
        {
            var credential = File.ReadAllText(@"D:\fx.txt").Split('\r');
            return (credential[0].Trim(), credential[1].Trim());
        }
        private static void CloseUnnecessaryTabs()
        {
            if (_driver.WindowHandles.Count > 1)
            {
                for (int i = _driver.WindowHandles.Count - 1; i > 0; i--)
                {
                    _driver.SwitchTo().Window(_driver.WindowHandles[i]);
                    _driver.Close();
                }
            }

            _driver.SwitchTo().Window(_driver.WindowHandles[0]); // <-- The solution
        }
    }
}
