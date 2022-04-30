using HealthCare_System.entities;
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
            /*ListBoxRequests.Items.Clear();
            foreach (AppointmentRequest appRequest in factory.AppointmentRequestController.AppointmentRequests)
            {
                if (appRequest.State == AppointmentState.WAITING)
                {
                    ListBoxRequests.Items.Add(appRequest);
                }
            }*/
        }
        
        private void fillListBoxPatients()
        {
            ListBoxPatients.Items.Clear();
            foreach (Patient patient in factory.PatientController.Patients)
            {
                if (patient.Blocked == showingBlocked)
                {
                    ListBoxPatients.Items.Add(patient);
                }
                
            }
        }

        private void UpdatePatientBtn_Click(object sender, RoutedEventArgs e)
        {
            Patient patient = (Patient)ListBoxPatients.SelectedItem;
            if (patient is null)
            {
                MessageBox.Show("Select patient You want to update!");
                return;
            }else
            {
                addPatientWin = new AddPatientWindow(factory, true, patient);
                addPatientWin.Show();
            }
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
                ShowBlockedBtn.Content = "View Regular";
                BlockBtn.Content = "Unblock";
            }else
            {
                ShowBlockedBtn.Content = "View Blocked";
                BlockBtn.Content = "Block";
            }
        }

        private void DeletePatientBtn_Click(object sender, RoutedEventArgs e)
        {
            Patient patient = (Patient)ListBoxPatients.SelectedItem;
            if (patient is null)
            {
                MessageBox.Show("Select patient You want to delete!");
                return;
            }

            try
            {
                factory.DeletePatient(patient);
            }
            catch
            {
                MessageBox.Show("Can't delete selected patient, because of it's future appointments.");
                return;
            }
            fillListBoxPatients();

        }

        private void BlockBtn_Click(object sender, RoutedEventArgs e)
        {
            Patient patient = (Patient)ListBoxPatients.SelectedItem;
            if (patient is null)
            {
                MessageBox.Show("Select patient You want to block/unblock!");
                return;
            }

            patient.Blocked = !patient.Blocked;
            ShowBlockedBtn.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }

        private void RefreshBtn_Click(object sender, RoutedEventArgs e)
        {
            fillListBoxPatients();
        }

        private void AcceptRequestBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RejectRequestBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RequestDetailsBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
