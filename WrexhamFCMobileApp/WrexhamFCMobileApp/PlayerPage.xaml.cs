using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace WrexhamFCMobileApp
{
    public partial class PlayerPage : PhoneApplicationPage
    {
        public PlayerPage()
        {
            InitializeComponent();
        }

        private void PlayerPage_Loaded(object sender, RoutedEventArgs e)
        {
            WrexhamFCServiceReference.Service1Client proxy = new WrexhamFCServiceReference.Service1Client();
            proxy.GetPlayerByIDCompleted += new EventHandler<WrexhamFCServiceReference.GetPlayerByIDCompletedEventArgs>(proxy_GetPlayerByIDCompleted);
            int IDPlayer = (int)Application.Current.Resources ["Player"];
            proxy.GetPlayerByIDAsync(IDPlayer);
        }

        void proxy_GetPlayerByIDCompleted(object sender, WrexhamFCServiceReference.GetPlayerByIDCompletedEventArgs e)
        {
            grdLayout.DataContext = e.Result;
            txbPlayerName.Text = e.Result.LastName + ", " + e.Result.FirstName;
        }
    }
}