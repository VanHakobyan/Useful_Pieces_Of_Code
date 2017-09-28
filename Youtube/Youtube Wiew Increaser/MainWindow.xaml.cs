using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace Youtube_Wiew_Increaser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Start(object sender, RoutedEventArgs e)
        {
            var iteretion = await Function().ConfigureAwait(true);
            if (iteretion == -1)
            MessageBox.Show("Plese correctly input !!!");
        }

        private async Task<int> Function()
        {
            var url = UrlBox.Text;
            if (string.IsNullOrEmpty(url))
                return -1;
            var list = File.ReadAllLines($@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\ProxyFree.txt").Select(x => x.Replace('\t', ':').TrimEnd(':', ' ')).ToList();
            var index = 0;
            var profile = new FirefoxProfile();
            var driver = new FirefoxDriver(profile);
            Proxy proxys;
            while (index++ != 300)
            {
                try
                {
                    proxys = new Proxy
                    {
                        HttpProxy = list[index],
                        FtpProxy = list[index],
                        SslProxy = list[index]
                    };
                    profile.SetProxyPreferences(proxys);

                    void Action()
                    {
                        driver.Navigate().GoToUrl(url);
                        driver.Manage().Cookies.DeleteAllCookies();
                    }

                    await Task.Run(() => Action()).ConfigureAwait(true);
                    //MessageBox.Show($"View count = {++viewCount}");
                    await Task.Delay(3000).ConfigureAwait(true);
                    await Task.Run(() => { driver.Manage().Cookies.DeleteAllCookies(); }).ConfigureAwait(true);
                    //driver.Quit();
                    await Task.WhenAll().ConfigureAwait(true);
                    lock (this)
                    {
                        WiewBlock.Text = $"Your view + {index}";
                    }
                }
                catch (Exception exception)
                {
                    await Task.Run(() => MessageBox.Show($"{exception.Message}  {exception.StackTrace}")).ConfigureAwait(true);
                }
            }
            return index;
        }

    }
}
