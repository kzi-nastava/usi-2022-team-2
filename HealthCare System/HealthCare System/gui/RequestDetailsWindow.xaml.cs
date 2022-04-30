using HealthCare_System.entities;
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
    /// Interaction logic for RequestDetailsWindow.xaml
    /// </summary>
    public partial class RequestDetailsWindow : Window
    {
        public RequestDetailsWindow(AppointmentRequest request)
        {
            InitializeComponent();
            /*textBlockDoctor.Text = request.Appointment.Doctor.FirstName + " " + request.Appointment.Doctor.LastName;
            textBlockPatient.Text = request.Patient.FirstName + " " + request.Patient.LastName;
            textBlockTime.Text = request.RequestCreated.ToString();
            textBlockType.Text = request.Type.ToString();
            textBlockStatus.Text = request.State.ToString();*/
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
