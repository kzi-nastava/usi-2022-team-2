using HealthCare_System.Model;
using HealthCare_System.factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HealthCare_System.gui
{
    /// <summary>
    /// Interaction logic for DelayedAppointmentNotificationWindow.xaml
    /// </summary>
    public partial class DelayedAppointmentNotificationWindow : Window
    {
        HealthCareFactory factory;
        Person user;        
        public DelayedAppointmentNotificationWindow(HealthCareFactory factory)
        {
            InitializeComponent();
            this.factory = factory;
            this.user = factory.User;
            Show();
            FillListBoxNotifications();
            
        }
        
        private void FillListBoxNotifications()
        {
            int counter = 0;
            foreach (DelayedAppointmentNotification notification in factory.DelayedAppointmentNotificationController.DelayedAppointmentNotifications)
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
            factory.DelayedAppointmentNotificationController.Serialize();
        }
    }
}
