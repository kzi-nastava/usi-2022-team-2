using HealthCare_System.entities;
using HealthCare_System.factory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for SecretaryWindow.xaml
    /// </summary>
    public partial class SecretaryWindow : Window
    {

        HealthCareFactory factory;
        List<Patient> blockedPatients = new List<Patient>();
        bool showingBlocked;
        AddPatientWindow addPatientWin;

        public SecretaryWindow(HealthCareFactory factory)
        {
            InitializeComponent();
            this.factory = factory;
            this.showingBlocked = false;
            fillListBoxPatients();
            fillListBoxRequests();
        }

        private void fillListBoxRequests()
        {
            listBoxRequests.Items.Clear();
            foreach (AppointmentRequest appRequest in factory.AppointmentRequestController.AppointmentRequests)
            {
                if (appRequest.State == AppointmentState.WAITING)
                {
                    listBoxRequests.Items.Add(appRequest);
                }
            }
        }
        
        private void fillListBoxPatients()
        {
            listBoxPatients.Items.Clear();
            foreach (Patient patient in factory.PatientController.Patients)
            {
                if (patient.Blocked == showingBlocked)
                {
                    listBoxPatients.Items.Add(patient);
                }
                
            }
        }

        private void UpdatePatientBtn_Click(object sender, RoutedEventArgs e)
        {
            Patient patient = (Patient)listBoxPatients.SelectedItem;
            if (patient is null)
            {
                MessageBox.Show("Select patient You want to update!");
                return;
            }
            addPatientWin = new AddPatientWindow(factory, true, patient);
            addPatientWin.Show();
        }

        private void NewPatientBtn_Click(object sender, RoutedEventArgs e)
        {
            addPatientWin = new AddPatientWindow(factory, false, null);
            addPatientWin.Show();
        }

        private void ShowBlockedBtn_Click(object sender, RoutedEventArgs e)
        {
            showingBlocked = !showingBlocked;
            fillListBoxPatients();
            if (showingBlocked)
            {
                showBlockedBtn.Content = "View Regular";
                blockBtn.Content = "Unblock";
            }else
            {
                showBlockedBtn.Content = "View Blocked";
                blockBtn.Content = "Block";
            }
        }

        private void DeletePatientBtn_Click(object sender, RoutedEventArgs e)
        {
            Patient patient = (Patient)listBoxPatients.SelectedItem;
            if (patient is null)
            {
                MessageBox.Show("Select patient You want to delete!");

            }
            else
            {
                try
                {
                    factory.DeletePatient(patient);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                fillListBoxPatients();
            }
        }

        private void BlockBtn_Click(object sender, RoutedEventArgs e)
        {
            Patient patient = (Patient)listBoxPatients.SelectedItem;
            if (patient is null)
            {
                MessageBox.Show("Select patient You want to block/unblock!");
            }
            else
            {
                patient.Blocked = !patient.Blocked;
                factory.PatientController.Serialize();
                showBlockedBtn.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
        }

        private void RefreshBtn_Click(object sender, RoutedEventArgs e)
        {
            fillListBoxPatients();
        }

        private void AcceptRequestBtn_Click(object sender, RoutedEventArgs e)
        {
            AppointmentRequest request = (AppointmentRequest)listBoxRequests.SelectedItem;
            if (request is null)
            {
                MessageBox.Show("Select appointment request You want to accept!");
            }
            else
            {
                factory.AcceptRequest(request);
                fillListBoxRequests();
                MessageBox.Show("You succesefully accepted selected request.");
            }
        }

        private void RejectRequestBtn_Click(object sender, RoutedEventArgs e)
        {
            AppointmentRequest request = (AppointmentRequest)listBoxRequests.SelectedItem;
            if (request is null)
            {
                MessageBox.Show("Select appointment request You want to reject!");
            }
            else
            {
                factory.RejectRequest(request);
                fillListBoxRequests();
                MessageBox.Show("You succesefully rejected selected request.");
            }
        }

        private void RequestDetailsBtn_Click(object sender, RoutedEventArgs e)
        {
            AppointmentRequest request = (AppointmentRequest)listBoxRequests.SelectedItem;
            if (request is null)
            {
                MessageBox.Show("Select appointment request You want to see!");
            }
            else
            {
                RequestDetailsWindow requestDetailsWindow = new RequestDetailsWindow(request);
                requestDetailsWindow.Show();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            factory.User = null;
            if (MessageBox.Show("Log out?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                MainWindow main = new MainWindow(factory);
                main.Show();
            }
            else e.Cancel = true;
        }
    }
}
