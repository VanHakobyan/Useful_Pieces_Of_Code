﻿using System.Linq;
using System.Windows;
using Helpers;

namespace Test
{
    public partial class Proxies : Window
    {
        public Proxies()
        {
            InitializeComponent();
            Maker();
        }

        public void Maker()
        {
            var proxies = new SimpleProxys();
            var proxyModels = proxies.GetProxys().ToList();
            ProxyDataGrid.ItemsSource = proxyModels;
        }
    }
}
