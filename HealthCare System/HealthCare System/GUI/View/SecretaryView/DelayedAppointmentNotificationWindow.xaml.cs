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
        public DelayedAppointmentNotificationWindow(DelayedAppointmentNotificationController delayedAppointmentNotificationController, Person user)
        {
            InitializeComponent();
            this.user = user;
            this.database   =   database;
            this.delayedAppointmentNotificationController = delayedAppointmentNotificationController;
            Show();
            FillListBoxNotifications();
            
        }
        
        private void FillListBoxNotifications()
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

            if (counter == 0)
            {
                Close();
            }
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            CloseWin();
            Close();
        }

        private void CloseWin()
        {
            foreach(DelayedAppointmentNotification notification in listBoxNotifications.Items)
            {
                if (user.GetType() == typeof(Doctor))
                {
                    notification.SeenByDoctor = true;
                }else
                {
                    notification.SeenByPatient = true;
                }
            }
            delayedAppointmentNotificationController.Serialize();
        }
    }
}
