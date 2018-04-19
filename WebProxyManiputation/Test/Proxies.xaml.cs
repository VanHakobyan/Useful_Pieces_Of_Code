using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using Helpers;

namespace Test
{
    /// <summary>
    /// Interaction logic for Proxies.xaml
    /// </summary>
    public partial class Proxies : Window
    {

        public Proxies()
        {
            InitializeComponent();
            Maker();
        }


        public void Maker()
        {
            var s = new SimpleProxys();
            var proxyModels = s.GetProxys().ToList();
            ProxyDataGrid.ItemsSource = proxyModels.Select(x => new { x.Ip, x.Port, x.Country, x.Type }).ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            //DataGrid.Columns[3].Visibility = Visibility.Collapsed;
        }
    }
}
