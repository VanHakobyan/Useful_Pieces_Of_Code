using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Input;
using Helpers;
using static System.String;

namespace Test
{
    /// <inheritdoc cref="" />
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private const string UrlDefault = "https://www.google.com/";
        private const string FooMessage = "Please input Ip or/and Port";
        private const string IpErrorMessage = "Please input Correct Ip";
        private const string PortErrorMessage = "Please input Correct Port 1 to 65535";
        private const string CancelContent = "Cancel";
        private const string ClearContent = "Clear";

        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void TestClick(object sender, RoutedEventArgs e)
        {
           
            var toParse = IpAddress.Text.Split(':')[0];
            IPAddress.TryParse(toParse, out var parseIpAddress);
            if (parseIpAddress is null)
            {
                Content.Text = IpErrorMessage;
                return;
            }
            if (IpAddress.Text == Empty)
            {
                Content.Text = FooMessage;
                return;
            }
            if (Port.Text == Empty)
            {
                if (IpAddress.Text.Contains(":"))
                {
                    Port.Text = IpAddress.Text.Split(':')[1];
                    IpAddress.Text = IpAddress.Text.Split(':')[0];
                }
                else
                {
                    Content.Text = FooMessage;
                    return;
                }
            }
            if (Port.Text != Empty && IpAddress.Text.Contains(":"))
            {
                Port.Text = IpAddress.Text.Split(':')[1];
                IpAddress.Text = IpAddress.Text.Split(':')[0];
            }
            int.TryParse(Port.Text, out var parsePort);
            if (parsePort < 0 && parsePort > 65535)
            {
                Content.Text = PortErrorMessage;
                return;
            }
            Awesome.Visibility = Visibility.Visible;
            TestButton.IsEnabled = false;
            ClearButton.Content = CancelContent;
            if (Url.Text == Empty) Url.Text = UrlDefault;
            var response = await Helper.SendGetRequest(Url.Text, IpAddress.Text, Port.Text);
            Content.Text = response;
            ClearButton.Content = ClearContent;
            Awesome.Visibility = Visibility.Hidden;
            TestButton.IsEnabled = true;
            Time.Text = DateTime.Now.ToString(CultureInfo.InvariantCulture);
        }

        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Clear(object sender, RoutedEventArgs e)
        {
            Awesome.Visibility = Visibility.Visible;
            Content.Text = Empty;
            Time.Text = Empty;
            Awesome.Visibility = Visibility.Hidden;
        }

        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Url_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) TestClick(null, null);
        }

        private void GetProxy_Click(object sender, RoutedEventArgs e)=> new Proxies().Show();
        
        
        
    }
}

