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
    public partial class NewsPage : PhoneApplicationPage
    {
        public NewsPage()
        {
            InitializeComponent();
        }

        private void NewsPage_Loaded(object sender, RoutedEventArgs e)
        {
            WrexhamFCServiceReference.Service1Client proxy = new WrexhamFCServiceReference.Service1Client();
            proxy.GetNewsByIDCompleted += new EventHandler<WrexhamFCServiceReference.GetNewsByIDCompletedEventArgs>(proxy_GetNewsByIDCompleted);
            int IDNews = (int)Application.Current.Resources["News"];
            proxy.GetNewsByIDAsync(IDNews);
            //WrexhamFCServiceReference.TableNews displaySelectedNews = new WrexhamFCServiceReference.TableNews();
            //displaySelectedNews = (WrexhamFCServiceReference.TableNews)Application.Current.Resources["News"];
            //txbNewsTitle.Text = displaySelectedNews.Title.ToString();
            //txbNewsContent.Text = displaySelectedNews.Message.ToString();
            //txbNewsAuthor.Text = "By " + displaySelectedNews.Author.ToString();
            //txbNewsDate.Text = displaySelectedNews.DateTime.ToString("dd/MM/yy hh:mm tt");
            
            //ServiceReference1.News newsOnPage = new ServiceReference1.News();
            //newsOnPage = (ServiceReference1.News)Application.Current.Resources["NewsParam"];
            //TextBlockPName.Text = newsOnPage.title.ToString();
            //TextBlockNews.Text = newsOnPage.text.ToString();
            //TextBlockNewsDate.Text = newsOnPage.date.ToString("dd/MM/yyyy");
        }

        void proxy_GetNewsByIDCompleted(object sender, WrexhamFCServiceReference.GetNewsByIDCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                //txbNewsTitle.Visibility = System.Windows.Visibility.Visible;
                txbNewsTitle.Text = e.Result.Title;
                txbNewsContent.Text = e.Result.Message;
                txbNewsAuthor.Text = "By " + e.Result.Author.ToString();
                txbNewsDate.Text = e.Result.DateTime.ToString("dd/MM/yy hh:mm tt");

            }
        }
    }
}