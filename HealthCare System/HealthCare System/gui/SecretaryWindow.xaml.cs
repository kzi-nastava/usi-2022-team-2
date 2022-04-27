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

        public SecretaryWindow(HealthCareFactory factory)
        {
            InitializeComponent();
            this.factory = factory;
            this.showingBlocked = false;
            fillListBox();
        }
        
        private void fillListBox()
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

        }

        private void NewPatientBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ShowBlockedBtn_Click(object sender, RoutedEventArgs e)
        {
            showingBlocked = !showingBlocked;
            fillListBox();
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

            factory.PatientController.Patients.Remove(patient);
            fillListBox();

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
    }
}
