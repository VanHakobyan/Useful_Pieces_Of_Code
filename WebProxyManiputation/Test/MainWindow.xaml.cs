using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Timers;
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
            Url.Items.Add(new KeyValuePair<string, string>("Youtube", @"https://www.Youtube.com"));
            Url.Items.Add(new KeyValuePair<string, string>("Google", @"https://www.Google.com"));
            Url.Items.Add(new KeyValuePair<string, string>("SportsBet", @"https://m.sportsbet.com.au/sportsbook/navhierarchy"));
            ComboIpAddress.ItemsSource = _proxys.GetProxys();
            _timer.Elapsed += TimerOnElapsed;
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            Time.Text = $"{_sw.Elapsed.TotalSeconds} second";
        }
        private SimpleProxys _proxys=new SimpleProxys();
        private const string FooMessage = "Please input Ip or/and Port";
        private const string IpErrorMessage = "Please input Correct Ip";
        private const string PortErrorMessage = "Please input Correct Port 1 to 65535";
        private const string CancelContent = "Cancel";
        private const string ClearContent = "Clear";
        private Timer _timer = new Timer(1);
        private Stopwatch _sw = new Stopwatch();
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void TestClick(object sender, RoutedEventArgs e)
        {
            _sw.Start();
            //_timer.Start();
            var toParse = ComboIpAddress.Text.Split(':')[0];
            IPAddress.TryParse(toParse, out var parseIpAddress);
            if (parseIpAddress is null)
            {
                Content.Text = IpErrorMessage;
                return;
            }
            if (ComboIpAddress.Text == Empty)
            {
                Content.Text = FooMessage;
                return;
            }
            if (Port.Text == Empty)
            {
                if (ComboIpAddress.Text.Contains(":"))
                {
                    Port.Text = ComboIpAddress.Text.Split(':')[1];
                    ComboIpAddress.Text = ComboIpAddress.Text.Split(':')[0];
                }
                else
                {
                    Content.Text = FooMessage;
                    return;
                }
            }
            if (Port.Text != Empty && ComboIpAddress.Text.Contains(":"))
            {
                Port.Text = ComboIpAddress.Text.Split(':')[1];
                ComboIpAddress.Text = ComboIpAddress.Text.Split(':')[0];
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
            if (Url.Text == Empty) Url.Text = Url.Items[0].ToString();
            var response = await Helper.SendGetRequest(Url.Text.Split(',').Last().Replace("]", ""), ComboIpAddress.Text, Port.Text);
            Content.Text = response;
            ClearButton.Content = ClearContent;
            Awesome.Visibility = Visibility.Hidden;
            TestButton.IsEnabled = true;
            _timer.Stop();
            Time.Text = $"{_sw.Elapsed.TotalSeconds} second";
            _sw.Reset();
        }

        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Clear(object sender, RoutedEventArgs e)
        {
            Awesome.Visibility = Visibility.Visible;
            Content.Text = Empty;
            Time.Text = Empty;
            Awesome.Visibility = Visibility.Hidden;
            TestButton.IsEnabled = true;
        }

        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Url_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) TestClick(null, null);
        }

        private void GetProxy_Click(object sender, RoutedEventArgs e)
        {
            new Proxies().Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

