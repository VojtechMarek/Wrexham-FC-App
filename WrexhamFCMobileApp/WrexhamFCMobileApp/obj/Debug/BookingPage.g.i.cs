﻿#pragma checksum "M:\Media\Škola\Visual Studio\Repo2\WrexhamFCMobileApp\WrexhamFCMobileApp\BookingPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D922C7E13D20482BF4F6FF0A518FD9F5"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace WrexhamFCMobileApp {
    
    
    public partial class BookingPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.StackPanel TitlePanel;
        
        internal System.Windows.Controls.TextBlock ApplicationTitle;
        
        internal System.Windows.Controls.TextBlock PageTitle;
        
        internal System.Windows.Controls.StackPanel ContentPanel;
        
        internal System.Windows.Controls.HyperlinkButton hlbCallToBookingOffice;
        
        internal System.Windows.Controls.HyperlinkButton hlbBookTicketsOnline;
        
        internal System.Windows.Controls.CheckBox cbxAddReminder;
        
        internal Microsoft.Phone.Controls.DatePicker DatePickerReminder;
        
        internal Microsoft.Phone.Controls.TimePicker TimePickerReminder;
        
        internal System.Windows.Controls.Button btnAddReminder;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/WrexhamFCMobileApp;component/BookingPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.TitlePanel = ((System.Windows.Controls.StackPanel)(this.FindName("TitlePanel")));
            this.ApplicationTitle = ((System.Windows.Controls.TextBlock)(this.FindName("ApplicationTitle")));
            this.PageTitle = ((System.Windows.Controls.TextBlock)(this.FindName("PageTitle")));
            this.ContentPanel = ((System.Windows.Controls.StackPanel)(this.FindName("ContentPanel")));
            this.hlbCallToBookingOffice = ((System.Windows.Controls.HyperlinkButton)(this.FindName("hlbCallToBookingOffice")));
            this.hlbBookTicketsOnline = ((System.Windows.Controls.HyperlinkButton)(this.FindName("hlbBookTicketsOnline")));
            this.cbxAddReminder = ((System.Windows.Controls.CheckBox)(this.FindName("cbxAddReminder")));
            this.DatePickerReminder = ((Microsoft.Phone.Controls.DatePicker)(this.FindName("DatePickerReminder")));
            this.TimePickerReminder = ((Microsoft.Phone.Controls.TimePicker)(this.FindName("TimePickerReminder")));
            this.btnAddReminder = ((System.Windows.Controls.Button)(this.FindName("btnAddReminder")));
        }
    }
}

