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
    public partial class MatchPage : PhoneApplicationPage
    {
        public MatchPage()
        {
            InitializeComponent();
        }

        private void MatchPage_Loaded(object sender, RoutedEventArgs e)
        {
            WrexhamFCServiceReference.Service1Client proxy = new WrexhamFCServiceReference.Service1Client();
            proxy.GetMatchByIDCompleted += new EventHandler<WrexhamFCServiceReference.GetMatchByIDCompletedEventArgs>(proxy_GetMatchByIDCompleted);
            int IDMatch = (int)Application.Current.Resources["Match"];
            proxy.GetMatchByIDAsync(IDMatch);

           
            proxy.GetMatchSummaryCompleted += new EventHandler<WrexhamFCServiceReference.GetMatchSummaryCompletedEventArgs>(proxy_GetMatchSummaryCompleted);
            proxy.GetMatchSummaryAsync(IDMatch);
            
            //ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            //client.getGameStatsByIdCompleted += new EventHandler<ServiceReference1.getGameStatsByIdCompletedEventArgs>(client_getGameStatsByIdCompleted);
            //int id = (int)Application.Current.Resources["StatParam"];
            //client.getGameStatsByIdAsync(id);

            //client.GetShortGameListCompleted += new EventHandler<ServiceReference1.GetShortGameListCompletedEventArgs>(client_GetShortGameListCompleted);
            //client.GetShortGameListAsync();
        }

        void proxy_GetMatchByIDCompleted(object sender, WrexhamFCServiceReference.GetMatchByIDCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                //txbVs.Visibility = System.Windows.Visibility.Visible;
                //stpStatistics1.DataContext = e.Result;
                //stpStatistics2.DataContext = e.Result;
                //stpStatistics3.DataContext = e.Result;
                //stpStatistics4.DataContext = e.Result;
                //stpStatistics5.DataContext = e.Result;
                //stpStatistics6.DataContext = e.Result;
                //stpStatistics7.DataContext = e.Result;
                //stpStatistics8.DataContext = e.Result;
                stpStatistics.DataContext = e.Result;
                stpSummaryTeamName.DataContext = e.Result;
                txbDate.DataContext = e.Result;
                txbDate1.DataContext = e.Result;
            }
        }
        
        void proxy_GetMatchSummaryCompleted(object sender, WrexhamFCServiceReference.GetMatchSummaryCompletedEventArgs e)
        {
            txbDash.Visibility = System.Windows.Visibility.Visible;
            lbxSummary.ItemsSource = e.Result;          
            //var result = (from summary in e.Result
            //              where (summary.HomeTeam == summary.Team)
            //              select summary);
            //if (lbxSummary.ItemsSource == result)
            //{ 
            //    lbxSummary.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Left;
            //}

            //else
            //{
            //    lbxSummary.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Right;

            //}
           
                //                  where (matches.MatchDate > DateTime.Now.Date)
                //                  orderby matches.MatchDate ascending
                //                  select matches);
                //    lbxFixtures.ItemsSource = result;
        }
    }
}