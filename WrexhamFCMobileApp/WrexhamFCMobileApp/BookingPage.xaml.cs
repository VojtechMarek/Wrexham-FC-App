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
using Microsoft.Phone.Tasks;
using Microsoft.Phone.Scheduler;

namespace WrexhamFCMobileApp
{
    public partial class BookingPage : PhoneApplicationPage
    {
        public BookingPage()
        {
            InitializeComponent();
        }

        private void hlbCallToBookingOffice_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            PhoneCallTask call = new PhoneCallTask();
            call.PhoneNumber = "07903 666666";
            call.Show();
        }

        private void cbxAddReminder_Checked(object sender, RoutedEventArgs e)
        {
            DatePickerReminder.Visibility = System.Windows.Visibility.Visible;
            TimePickerReminder.Visibility = System.Windows.Visibility.Visible;
            btnAddReminder.Visibility = System.Windows.Visibility.Visible;            
        }

        private void cbxAddReminder_Unchecked(object sender, RoutedEventArgs e)
        {
            DatePickerReminder.Visibility = System.Windows.Visibility.Collapsed;
            TimePickerReminder.Visibility = System.Windows.Visibility.Collapsed;
            btnAddReminder.Visibility = System.Windows.Visibility.Collapsed;  
        }

        private void btnAddReminder_Click(object sender, RoutedEventArgs e)
        {
            String dateTimeString = "";
            DateTime dateTime = DateTime.MinValue;
            dateTimeString = DateTime.Parse(DatePickerReminder.Value.ToString()).ToString("MM/dd/yyyy") + " " + DateTime.Parse(TimePickerReminder.Value.ToString()).ToString("h:mm tt");
            dateTime = DateTime.Parse(dateTimeString);

            Reminder reminder = new Reminder("myreminder")
            {
                Title = " app reminder",
                Content = "Reminder !",
                //BeginTime = DateTime.Now.AddHours(3),
                BeginTime = (System.DateTime)dateTime,
                RecurrenceType = RecurrenceInterval.None,
                ExpirationTime = DateTime.Now.AddSeconds(20),
                //ExpirationTime = DateTime.Today.AddDays(30),
            };
            ScheduledActionService.Remove("myreminder");
            ScheduledActionService.Add(reminder);
            MessageBox.Show("Reminder has been successfully set!");
            //ScheduledActionService.LaunchForTest("myreminder", System.TimeSpan.FromSeconds(3));
            //ScheduledActionService.Remove("myreminder");

            //var appointment = new Microsoft.Phone.Scheduler.Reminder;
            //SaveAppointmentTask saveAppointmentTask = new SaveAppointmentTask();

            //saveAppointmentTask.StartTime = DateTime.Now.AddHours(2);
            //saveAppointmentTask.EndTime = DateTime.Now.AddHours(3);
            //saveAppointmentTask.Subject = "Appointment subject";
            //saveAppointmentTask.Location = "Appointment location";
            //saveAppointmentTask.Details = "Appointment details";
            //saveAppointmentTask.IsAllDayEvent = false;
            //saveAppointmentTask.AppointmentStatus = Microsoft.Phone.UserData.AppointmentStatus.Busy;

            //saveAppointmentTask.Show();
        }
    }
}