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
    public partial class PlayersListPage : PhoneApplicationPage
    {
        public PlayersListPage()
        {
            InitializeComponent();
        }

        private void PlayersListPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (Application.Current.Resources["index"] != null) //prevents from errors when going back from PlayerPage, because when we on PlayerPage, it clears current resources
            { 
            int index = (int)Application.Current.Resources["index"];
            PivotPage.SelectedIndex = index;
            }
            
            WrexhamFCServiceReference.Service1Client proxy = new WrexhamFCServiceReference.Service1Client();
            proxy.GetAllPlayersCompleted += new EventHandler<WrexhamFCServiceReference.GetAllPlayersCompletedEventArgs>(proxy_GetAllPlayersCompleted);
            proxy.GetAllPlayersAsync();
        }



        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            WrexhamFCServiceReference.Service1Client proxy = new WrexhamFCServiceReference.Service1Client();
            proxy.GetAllPlayersCompleted += new EventHandler<WrexhamFCServiceReference.GetAllPlayersCompletedEventArgs>(proxy_GetAllPlayersCompleted);
            proxy.GetAllPlayersAsync();
        }


        void proxy_GetAllPlayersCompleted(object sender, WrexhamFCServiceReference.GetAllPlayersCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                var result = (from players in e.Result
                              where (players.Injured == true)
                              orderby players.DressNumber_ID_ ascending
                              select players);
                lbxInjuredPlayers.ItemsSource = result;
                lbxListOfPlayers.ItemsSource = e.Result;
                lbxStatistics.ItemsSource = e.Result;
                //search button
                if (txtSearch.Text != "Search player")
                {
                    this.lbxListOfPlayers.ItemsSource = e.Result.Where(w => w.FirstName.ToUpper().Contains(txtSearch.Text.ToUpper()) || w.LastName.ToUpper().Contains(txtSearch.Text.ToUpper()));
                    //this.listBox.ItemsSource = carList.Where(w => w.carMake.ToUpper().Contains(txtSearch.Text.ToUpper()));
                }
            }
        }

        private void lbxListOfPlayers_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Application.Current.Resources.Clear();//clearing current resources in order to allow add new DressNumber_ID_ value to class System.String "Player"
            WrexhamFCServiceReference.TablePlayers selectedPlayer = new WrexhamFCServiceReference.TablePlayers();
            selectedPlayer = (WrexhamFCServiceReference.TablePlayers)lbxListOfPlayers.SelectedItem;
            if (selectedPlayer != null) //exception if we click somewhere else than displayed items
            {
                Application.Current.Resources.Add("Player", selectedPlayer.DressNumber_ID_);
                NavigationService.Navigate(new Uri("/PlayerPage.xaml", UriKind.RelativeOrAbsolute));
                txtSearch.Text = "Search player"; // this restores original txtSearch text
                txtSearch.Foreground = new SolidColorBrush(Colors.Gray); // this restores original txtSearch color
            }
        }

        private void lbxInjuredPlayers_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Application.Current.Resources.Clear();//clearing current resources in order to allow add new DressNumber_ID_ value to class System.String "Player"
            WrexhamFCServiceReference.TablePlayers selectedPlayer = new WrexhamFCServiceReference.TablePlayers();
            selectedPlayer = (WrexhamFCServiceReference.TablePlayers)lbxInjuredPlayers.SelectedItem;
            if (selectedPlayer != null) //exception if we click somewhere else than displayed items
            {
                Application.Current.Resources.Add("Player", selectedPlayer.DressNumber_ID_);
                NavigationService.Navigate(new Uri("/PlayerPage.xaml", UriKind.RelativeOrAbsolute));
            }
        }

        private void lbxStatistics_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Application.Current.Resources.Clear();//clearing current resources in order to allow add new DressNumber_ID_ value to class System.String "Player"
            WrexhamFCServiceReference.TablePlayers selectedPlayer = new WrexhamFCServiceReference.TablePlayers();
            selectedPlayer = (WrexhamFCServiceReference.TablePlayers)lbxStatistics.SelectedItem;
            if (selectedPlayer != null) //exception if we click somewhere else than displayed items
            {
                Application.Current.Resources.Add("Player", selectedPlayer.DressNumber_ID_);
                NavigationService.Navigate(new Uri("/PlayerPage.xaml", UriKind.RelativeOrAbsolute));
            }
        }

        // txtSearch is cleared and has chańged color when it got focus
        private void txtSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtSearch.Text == "Search player")
            {
                txtSearch.Text = "";
                txtSearch.Foreground = new SolidColorBrush(Colors.Black);
            }
        }
        // txtSearch is filled by "Search player" and has chańged color when it losts focus
        private void txtSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtSearch.Text == "")
            {
                txtSearch.Text = "Search player";
                txtSearch.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }
    }
}