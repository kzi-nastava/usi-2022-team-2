using System.Windows;
using HealthCare_System.Core.Notifications;
using HealthCare_System.Core.Notifications.Model;
using HealthCare_System.Core.Users.Model;
using HealthCare_System.Database;

namespace HealthCare_System.GUI.SecretaryView
{
    /// <summary>
    /// Interaction logic for DelayedAppointmentNotificationWindow.xaml
    /// </summary>
    public partial class DelayedAppointmentNotificationWindow : Window
    {
        Person user;
        HealthCareDatabase database;
        DelayedAppointmentNotificationService delayedAppointmentNotificationService;
        public DelayedAppointmentNotificationWindow(HealthCareDatabase database, Person user)
        {
            InitializeComponent();
            this.user = user;
            this.database   =   database;
            delayedAppointmentNotificationService = new(database.DelayedAppointmentNotificationRepo);
            Show();
            FillListBoxNotifications();
            
        }
        
        private void FillListBoxNotifications()
        {
            int counter = 0;
            foreach (DelayedAppointmentNotification notification in delayedAppointmentNotificationService.DelayedAppointmentNotifications())
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
            delayedAppointmentNotificationService.DelayedAppointmentNotificationRepo.Serialize();
        }
    }
}
