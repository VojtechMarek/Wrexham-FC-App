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
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
            BackKeyPress += (BackKey_Click); //this defines what happens when Back key is pressed
        }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {   
            WrexhamFCServiceReference.Service1Client proxy = new WrexhamFCServiceReference.Service1Client();
            //News loading
            //proxy.GetAllNewsCompleted += new EventHandler<WrexhamFCServiceReference.GetAllNewsCompletedEventArgs>(proxy_GetAllNewsCompleted);
            //proxy.GetAllNewsAsync();
            proxy.GetAllNewsInfoCompleted += new EventHandler<WrexhamFCServiceReference.GetAllNewsInfoCompletedEventArgs>(proxy_GetAllNewsInfoCompleted);
            proxy.GetAllNewsInfoAsync();
            //proxy.GetNewsByIDCompleted += new EventHandler<WrexhamFCServiceReference.GetNewsByIDCompletedEventArgs>(proxy_GetNewsByIDCompleted);
            //proxy.GetNewsByIDAsync(int IDNews);

            //Fixtures loading
            proxy.GetAllMatchesInfoCompleted +=new EventHandler<WrexhamFCServiceReference.GetAllMatchesInfoCompletedEventArgs>(proxy_GetAllMatchesInfoCompleted);
            proxy.GetAllMatchesInfoAsync();

            //League Table loading
            proxy.GetLeagueTableCompleted += new EventHandler<WrexhamFCServiceReference.GetLeagueTableCompletedEventArgs>(proxy_GetLeagueTableCompleted);
            proxy.GetLeagueTableAsync();

            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }

        void proxy_GetLeagueTableCompleted(object sender, WrexhamFCServiceReference.GetLeagueTableCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                lbxLeagueTable.ItemsSource = e.Result;
            }
        }

        //void proxy_GetNewsByIDCompleted(object sender, WrexhamFCServiceReference.GetNewsByIDCompletedEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        void proxy_GetAllNewsInfoCompleted(object sender, WrexhamFCServiceReference.GetAllNewsInfoCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                lbxNews.ItemsSource = e.Result;
            } 
        }

        public Int16 CheckedFixturesRdbtn; //variable for filter of fixtures (past, future, all)

        void proxy_GetAllMatchesInfoCompleted(object sender, WrexhamFCServiceReference.GetAllMatchesInfoCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                //if (rbtnAll.IsChecked == true)
                //{
                //    lbxFixtures.ItemsSource = e.Result;
                //}
                //else
                //{
                //    var result = (from matches in e.Result
                //                  where (matches.MatchDate > DateTime.Now.Date)
                //                  orderby matches.MatchDate ascending
                //                  select matches);
                //    lbxFixtures.ItemsSource = result;
                //}
                
                //radio button filter for fixtures
                switch (CheckedFixturesRdbtn)
                {
                    case 1: //all fixtures
                        lbxFixtures.ItemsSource = e.Result;
                        break;
                    case 2: //past fixtures
                        var past = (from matches in e.Result
                                    where (matches.MatchDate < DateTime.Now.Date)
                                  orderby matches.MatchDate descending
                                  select matches);
                        lbxFixtures.ItemsSource = past;
                        break;
                    case 3: //future fixtures
                        var future = (from matches in e.Result
                                  where (matches.MatchDate > DateTime.Now.Date)
                                  orderby matches.MatchDate ascending
                                  select matches);
                        lbxFixtures.ItemsSource = future;
                        lbxTickets.ItemsSource = future;
                        break;
                }

                //filter for booking tickets list
                var futuregame = (from matches in e.Result
                              where (matches.MatchDate > DateTime.Now.Date)
                              orderby matches.MatchDate ascending
                              select matches);
                lbxTickets.ItemsSource = futuregame;
            }
        }

        void proxy_GetAllNewsCompleted(object sender, WrexhamFCServiceReference.GetAllNewsCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                lbxNews.ItemsSource = e.Result;
            }
        }

//kliknutí na učitý textblock v main page posune aplikaci na příslušnou pivot položku
        private void txbNews_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            PivotPage.SelectedIndex = 1;
            lbxMainMenu.SelectedItem = null;
        }
        private void txbFixtures_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            PivotPage.SelectedIndex = 2;
            lbxMainMenu.SelectedItem = null;
        }
        private void txbTickets_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            PivotPage.SelectedIndex = 3;
            lbxMainMenu.SelectedItem = null;
        }
        private void txbPlayers_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            PivotPage.SelectedIndex = 4;
            lbxMainMenu.SelectedItem = null;
        }
        private void txbLeagueTable_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            PivotPage.SelectedIndex = 5;
            lbxMainMenu.SelectedItem = null;
        }

        //when the back button is clicked following actions are executed
        private void BackKey_Click(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (PivotPage.SelectedIndex != 0) //if the App is not in the Main menu, the App returns to it
            {
                e.Cancel = true; //this cancels ending the app
                PivotPage.SelectedIndex = 0; // this returns to the Main menu

            }
            else //if the app is in the Main menu it shows msbx, which offers exit from app
            {
                var exit = MessageBox.Show("Do you want to exit WREXHAM FC APP?", "Exit App?", MessageBoxButton.OKCancel);
                if (exit == MessageBoxResult.Cancel) //if Cancel button is clicked on the msbx, it cancels ending of the App  
                {
                 e.Cancel = true;  
                }
                return; //else, which is ok button, ends the app
            }
        }

//kliknutí na určitou položku listboxu záložce players přesměruje na správnou v pivotpage players (načte se číslo stránky do aktuálního zdroje)
        private void txbListOfPlayers_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Application.Current.Resources.Clear();//clearing current resources in order to allow add new index value to class System.String "index"
            int index = 0;
            Application.Current.Resources.Add("index", index);
            NavigationService.Navigate(new Uri("/PlayersListPage.xaml", UriKind.RelativeOrAbsolute));
            lbxPlayers.SelectedItem = null;
            
        }

        private void txbInjuredPlayers_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Application.Current.Resources.Clear();//clearing current resources in order to allow add new index value to class System.String "index"
            int index = 1;
            Application.Current.Resources.Add("index", index);
            NavigationService.Navigate(new Uri("/PlayersListPage.xaml", UriKind.RelativeOrAbsolute));
            lbxPlayers.SelectedItem = null;
        }

        private void txbStatistics_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Application.Current.Resources.Clear();//clearing current resources in order to allow add new index value to class System.String "index"
            int index = 2;
            Application.Current.Resources.Add("index", index);
            NavigationService.Navigate(new Uri("/PlayersListPage.xaml", UriKind.RelativeOrAbsolute));
            lbxPlayers.SelectedItem = null;
        }

//Co se stane při kliknutí na jednu z novinek
        private void lbxNews_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Application.Current.Resources.Clear();//clearing current resources in order to allow add new IDNews value to class System.String "News"
            WrexhamFCServiceReference.TableNewsInfo selectedNews = new WrexhamFCServiceReference.TableNewsInfo();
            selectedNews = (WrexhamFCServiceReference.TableNewsInfo)lbxNews.SelectedItem;
            if (selectedNews != null) //exception if we click somewhere else than displayed items
            {
            Application.Current.Resources.Add("News", selectedNews.IDNews);
            NavigationService.Navigate(new Uri("/NewsPage.xaml", UriKind.RelativeOrAbsolute));
            }
        }

//Co se stane při kliknutí na jedno z utkání
        private void lbxFixtures_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {            
            Application.Current.Resources.Clear();//clearing current resources in order to allow add new IDMatch value to class System.String "Match"
            WrexhamFCServiceReference.TableMatchesInfo selectedMatch = new WrexhamFCServiceReference.TableMatchesInfo();
            selectedMatch = (WrexhamFCServiceReference.TableMatchesInfo)lbxFixtures.SelectedItem;
            if (selectedMatch != null) //exception if we click somewhere else than displayed items
            {
                if (selectedMatch.MatchDate > System.DateTime.Now) //if selected match has not been played yet, it shows messege box to ask for booking tickets
                {
                    var booking = MessageBox.Show("This fixture has not been played yet. Would you like to book a ticket?", "Book tickets?", MessageBoxButton.OKCancel);
                    if (booking == MessageBoxResult.Cancel) //if Cancel button is clicked on the msbx, it cancels booking tickets page transfer
                    {
                        lbxFixtures.SelectedItem = null;
                        return; 
                    }

                    Application.Current.Resources.Add("Match", selectedMatch.IDMatch);//else it redirects to booking tickets page
                    NavigationService.Navigate(new Uri("/BookingPage.xaml", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    Application.Current.Resources.Add("Match", selectedMatch.IDMatch);
                    NavigationService.Navigate(new Uri("/MatchPage.xaml", UriKind.RelativeOrAbsolute));
                }
            }
        }

        //click on fixture in booking tickets
        private void lbxTickets_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Application.Current.Resources.Clear();//clearing current resources in order to allow add new IDMatch value to class System.String "Match"
            WrexhamFCServiceReference.TableMatchesInfo selectedMatch = new WrexhamFCServiceReference.TableMatchesInfo();
            selectedMatch = (WrexhamFCServiceReference.TableMatchesInfo)lbxTickets.SelectedItem;
            if (selectedMatch != null) //exception if we click somewhere else than displayed items
            {
                Application.Current.Resources.Add("Match", selectedMatch.IDMatch);
                NavigationService.Navigate(new Uri("/BookingPage.xaml", UriKind.RelativeOrAbsolute));
            }
        }
// LeagueTable item cannot be selected (because it does nothing)
        private void lbxLeagueTable_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            lbxLeagueTable.SelectedItem = null;
        }

        //radiobutton filtres for Fixtures (all, past or future) / it calls GetAllMatchesCompleted method
        private void rbtnAll_Checked(object sender, RoutedEventArgs e)
        {
            CheckedFixturesRdbtn = 1;
            WrexhamFCServiceReference.Service1Client proxy = new WrexhamFCServiceReference.Service1Client();
            proxy.GetAllMatchesInfoCompleted += new EventHandler<WrexhamFCServiceReference.GetAllMatchesInfoCompletedEventArgs>(proxy_GetAllMatchesInfoCompleted);
            proxy.GetAllMatchesInfoAsync();
        }
        private void rbtnPast_Checked(object sender, RoutedEventArgs e)
        {
            CheckedFixturesRdbtn = 2;
            WrexhamFCServiceReference.Service1Client proxy = new WrexhamFCServiceReference.Service1Client();
            proxy.GetAllMatchesInfoCompleted += new EventHandler<WrexhamFCServiceReference.GetAllMatchesInfoCompletedEventArgs>(proxy_GetAllMatchesInfoCompleted);
            proxy.GetAllMatchesInfoAsync();
        }
        private void rbtnFuture_Checked(object sender, RoutedEventArgs e)
        {
            CheckedFixturesRdbtn = 3;
            WrexhamFCServiceReference.Service1Client proxy = new WrexhamFCServiceReference.Service1Client();
            proxy.GetAllMatchesInfoCompleted += new EventHandler<WrexhamFCServiceReference.GetAllMatchesInfoCompletedEventArgs>(proxy_GetAllMatchesInfoCompleted);
            proxy.GetAllMatchesInfoAsync();
        }
    }
}