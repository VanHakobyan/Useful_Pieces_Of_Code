using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
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
using DevExpress.Data.Utils;
using Helpers;
using Newtonsoft.Json;

namespace Test
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
        private const string UrlDefault = "https://www.google.com/";
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Awesome.Visibility = Visibility.Visible;
            if (Url.Text == string.Empty) Url.Text = UrlDefault;
            var response = await Helper.SendGetRequest(Url.Text, IpAddress.Text, Port.Text);
            Content.Text = response;
            Awesome.Visibility = Visibility.Hidden;
            Time.Text = DateTime.Now.ToString(CultureInfo.InvariantCulture);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Awesome.Visibility = Visibility.Visible;
            Content.Text = string.Empty;
            Time.Text = string.Empty;
            Awesome.Visibility = Visibility.Hidden;
        }
        private void Url_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) Button_Click(null, null);
        }
    }
}

