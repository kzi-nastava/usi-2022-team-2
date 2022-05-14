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
        bool isOperation;
        Dictionary<Appointment, DateTime> replaceableAppointments;


        public SecretaryWindow(HealthCareFactory factory)
        {
            InitializeComponent();
            this.factory = factory;
            this.showingBlocked = false;
            SetEmergencyAppTab();
            setReferralsTab();
            FillListBoxPatients();
            FillListBoxRequests();
            FillListBoxReferrals();
        }

        private void SetEmergencyAppTab()
        {
            this.isOperation = false;
            this.textBoxDuration.IsEnabled = false;

            foreach (Patient patient in factory.PatientController.Patients)
            {
                cmbPatient.Items.Add(patient);
            }

            foreach (int i in Enum.GetValues(typeof(Specialization)))
            {
                cmbSpecialization.Items.Add((Specialization)i);
            }
        }

        private void setReferralsTab()
        {
            foreach (Patient patient in factory.PatientController.Patients)
            {
                cmbPatientInReferrals.Items.Add(patient);
            }
        }

        private void FillListBoxRequests()
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
        
        private void FillListBoxPatients()
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

        private void FillListBoxAppointments(List<Doctor> doctors, int duration)
        {
            listBoxAppointments.Items.Clear();
            replaceableAppointments = factory.AppointmentController.GetReplaceableAppointments(doctors, duration, (Patient)cmbPatient.SelectedItem);
            foreach (KeyValuePair<Appointment, DateTime> item in replaceableAppointments.OrderBy(key => key.Value))
            {
                listBoxAppointments.Items.Add(item.Key);
            }
        }

        private void FillListBoxReferrals()
        {
            listBoxReferrals.Items.Clear();
            Patient patient = (Patient)cmbPatientInReferrals.SelectedItem;
            if (patient is null)
            {
                listBoxReferrals.Items.Add("There isn't any unused referral.");
            }
            else
            {
                foreach (MedicalRecord medicalRecord in factory.MedicalRecordController.MedicalRecords)
                {
                    if (medicalRecord.Patient == patient)
                    {
                        foreach (Referral referral in medicalRecord.Referrals)
                        {
                            if(referral.Used == false)
                            {
                                listBoxReferrals.Items.Add(referral);
                            }
                        }
                        break;
                    }
                }

                if (listBoxReferrals.Items.Count == 0)
                {
                    listBoxReferrals.Items.Add("There isn't any unused referral.");
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
            FillListBoxPatients();
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
                FillListBoxPatients();
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
            FillListBoxPatients();
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
                FillListBoxRequests();
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
                FillListBoxRequests();
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

        int getDuration()
        {
            int duration = 15;
            if (isOperation)
            {
                try
                {
                    duration = Convert.ToInt32(textBoxDuration.Text);
                }
                catch 
                {
                    MessageBox.Show("Duration is in the wrong format. It is automatically set to 15 minutes.");
                }
            }
            return duration;
        }

        private void BookClosestAppointment(object sender, RoutedEventArgs e)
        {
            int duration = getDuration();

            List<Doctor> doctors = factory.DoctorController.FindBySpecialization((Specialization)cmbSpecialization.SelectedItem);
            Appointment bookedAppointment = factory.BookClosestEmergancyAppointment(doctors, duration, factory.AppointmentController.GenerateId());

            if (bookedAppointment == null)
            {
                MessageBox.Show("There is no available appointment in next 2h. Select one booked to be replaced.");
                FillListBoxAppointments(doctors, duration);
            }
            else
            {
                bookedAppointment.Patient = (Patient)cmbPatient.SelectedItem;
                factory.AddAppointment(bookedAppointment);
                MessageBox.Show("You succesefully booked emergency appointment.");
            }
        }


        private void rbOperation_Checked(object sender, RoutedEventArgs e)
        {
            this.isOperation = true;
            this.textBoxDuration.IsEnabled = true;
        }

        private void rbExamination_Checked(object sender, RoutedEventArgs e)
        {
            this.isOperation = false;
            this.textBoxDuration.IsEnabled = false;
        }

        private void bookAndReplaceBtn_Click(object sender, RoutedEventArgs e)
        {
            Appointment toReplaceAppointment = (Appointment)listBoxAppointments.SelectedItem;
            if (toReplaceAppointment is null)
            {
                MessageBox.Show("Select appointment You want to replace!");
            }
            else
            {
                int duration = getDuration();
                Appointment newAppointment = new Appointment(factory.AppointmentController.GenerateId(), toReplaceAppointment.Start, toReplaceAppointment.End,
                                                            Appointment.getTypeByDuration(duration), AppointmentStatus.BOOKED, false, true);
                newAppointment.Doctor = toReplaceAppointment.Doctor;
                newAppointment.Patient = (Patient)cmbPatient.SelectedItem;

                toReplaceAppointment.Start = replaceableAppointments[toReplaceAppointment];
                toReplaceAppointment.End = replaceableAppointments[toReplaceAppointment].AddMinutes(duration);

                newAppointment = factory.AddAppointment(newAppointment);
                factory.AddNotification(toReplaceAppointment, newAppointment.Start);

                MessageBox.Show("Doctor and patient are informed about appointment delay.");
            }
        }

        private void refreshReferralsBtn_Click(object sender, RoutedEventArgs e)
        {
            FillListBoxReferrals();
        }

        private void bookByReferralBtn_Click(object sender, RoutedEventArgs e)
        {
            Referral referral = (Referral)listBoxReferrals.SelectedItem;
            if (referral is null)
            {
                MessageBox.Show("Select referral You want to use for new appointment!");
            }
            else
            {
                Appointment appointment = factory.BookAppointmentByReferral(referral);
                MessageBox.Show("You successfully booked new appointment by selected referral.\nAppointment start: " + appointment.Start);
            }
        }
    }
}
