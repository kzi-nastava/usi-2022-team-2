using HealthCare_System.Core.Appointments.Model;
using System.Windows;

namespace HealthCare_System.GUI.PatientView
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
