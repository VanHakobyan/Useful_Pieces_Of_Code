using System;
using System.Globalization;
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
        private const string CancelContent = "Cancel";
        private const string ClearContent = "Clear";

        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void TestClick(object sender, RoutedEventArgs e)
        {
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
    }
}

