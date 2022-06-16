using System.Windows;
using HealthCare_System.Core.Notifications;
using HealthCare_System.Core.Notifications.Model;
using HealthCare_System.Core.Users.Model;
using HealthCare_System.Database;
using HealthCare_System.GUI.Controller.Notifications;

namespace HealthCare_System.GUI.SecretaryView
{
    /// <summary>
    /// Interaction logic for DelayedAppointmentNotificationWindow.xaml
    /// </summary>
    public partial class DelayedAppointmentNotificationWindow : Window
    {
        Person user;
        HealthCareDatabase database;
        DelayedAppointmentNotificationController delayedAppointmentNotificationController;
        IDaysOffNotificationService daysOffNotificationService;
        public DelayedAppointmentNotificationWindow(DelayedAppointmentNotificationController delayedAppointmentNotificationController, IDaysOffNotificationService daysOffNotificationService,
            Person user)
        {
            InitializeComponent();
            this.user = user;
            this.daysOffNotificationService = daysOffNotificationService;
            this.delayedAppointmentNotificationController = delayedAppointmentNotificationController;
            Show();
            FillListBoxNotifications();
            
        }
        
        private void FillListBoxNotifications()
        {
            int counter = 0;
            counter += AddDelayedAppointmentNotifications();
            counter += AddDaysOffNotifications();
            if (counter == 0)
            {
                Close();
            }
        }

        private int AddDelayedAppointmentNotifications()
        {
            int counter = 0;
            foreach (DelayedAppointmentNotification notification in delayedAppointmentNotificationController.DelayedAppointmentNotifications())
            {
                if ((notification.Appointment is not null) && (notification.Appointment.Doctor == user || notification.Appointment.Patient == user) &&
                    (notification.SeenByPatient == false || notification.SeenByDoctor == false))
                {
                    listBoxNotifications.Items.Add(notification);
                    counter += 1;
                }
            }
            return counter;
        }

        private int AddDaysOffNotifications()
        {
            int counter = 0;
            foreach (DaysOffNotification daysOffNotification in daysOffNotificationService.DaysOffNotifications())
            {
                if (user == daysOffNotification.Doctor && daysOffNotification.SeenByDoctor == false)
                {
                    listBoxNotifications.Items.Add(daysOffNotification);
                    counter += 1;
                }
            }
            return counter;
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            CloseWin();
            Close();
        }

        private void CloseWin()
        {
            foreach (Notification notification in listBoxNotifications.Items)
            {
                if (notification.GetType() == typeof(DelayedAppointmentNotification))
                {
                    DelayedAppointmentNotification delayedAppointmentnotification = (DelayedAppointmentNotification)notification;
                    if (user.GetType() == typeof(Doctor))
                    {
                        delayedAppointmentnotification.SeenByDoctor = true;
                    }
                    else
                    {
                        delayedAppointmentnotification.SeenByPatient = true;
                    }
                }
                else
                {
                    DaysOffNotification daysOffNotification = (DaysOffNotification)notification;
                    daysOffNotification.SeenByDoctor = true;
                }
            }
            delayedAppointmentNotificationController.Serialize();
        }
    }
}
