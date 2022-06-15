using HealthCare_System.Core.AppointmentRequests.Model;
using System.Windows;

namespace HealthCare_System.GUI.SecretaryView
{
    /// <summary>
    /// Interaction logic for RequestDetailsWindow.xaml
    /// </summary>
    public partial class RequestDetailsWindow : Window
    {
        public RequestDetailsWindow(AppointmentRequest request)
        {
            InitializeComponent();
            textBlockDoctor.Text = request. OldAppointment.Doctor.FirstName + " " + request.OldAppointment.Doctor.LastName;
            textBlockPatient.Text = request.Patient.FirstName + " " + request.Patient.LastName;
            textBlockTime.Text = request.RequestCreated.ToString();
            textBlockType.Text = request.Type.ToString();
            textBlockStatus.Text = request.State.ToString();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
