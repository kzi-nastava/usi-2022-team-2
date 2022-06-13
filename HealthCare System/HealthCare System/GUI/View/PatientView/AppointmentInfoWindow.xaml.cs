using HealthCare_System.Model;
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
    /// Interaction logic for AppointmentInfo.xaml
    /// </summary>
    public partial class AppointmentInfo : Window
    {
        Appointment appointment;
        public AppointmentInfo(Appointment appointment)
        {
            this.appointment = appointment;
            InitializeComponent();
            startL.Content = appointment.Start.ToString("yyyy/MM/dd hh:mm");
            endL.Content = appointment.End.ToString("yyyy/MM/dd hh:mm");
            doctorL.Content = appointment.Doctor.FirstName + " " + appointment.Doctor.LastName;
            roomL.Content = appointment.Room.Name;
            typeL.Content = appointment.Type.ToString();
            anamnesisL.Content = appointment.Anamnesis.Description;
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
