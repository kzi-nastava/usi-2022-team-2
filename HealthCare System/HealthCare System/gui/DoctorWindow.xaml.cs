using HealthCare_System.entities;
using HealthCare_System.factory;
using System.Collections.Generic;
using System.Windows;

namespace HealthCare_System.gui
{
    public partial class DoctorWindow : Window
    {
        HealthCareFactory factory;
        Doctor doctor;
        Dictionary<string, Appointment> appontmentsDisplay;

        public DoctorWindow(HealthCareFactory factory)
        {
            this.factory = factory;
            doctor = (Doctor) factory.User;

            InitializeComponent();

            InitializeAppointments();

            EndBtn.IsEnabled = false;
            PrescribeBtn.IsEnabled = false;
            ReferralBtn.IsEnabled = false;

            roomTb.IsEnabled = false;
            patientTb.IsEnabled = false;

            heightTb.IsEnabled = false;
            weightTb.IsEnabled = false;
            diseaseHistoryTb.IsEnabled = false;
            anamnesisTb.IsEnabled = false;
        }

        void InitializeAppointments()
        {
            appointmentView.Items.Clear();
            appontmentsDisplay = new Dictionary<string, Appointment>();
            List<Appointment> appointments = doctor.FilterAppointments();

            foreach (Appointment appointment in appointments)
            {
                string key = appointment.Start.ToString() + ", Room: " + appointment.Room.Id;
                appontmentsDisplay.Add(key, appointment);
                appointmentView.Items.Add(key);
            }

        }

        private void AppointmentView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (appointmentView.SelectedIndex != -1) 
            { 
                Appointment appointment = appontmentsDisplay[appointmentView.SelectedItem.ToString()];

                Room room = appointment.Room;
                roomTb.Text = room.Id + " - " + room.Name;

                Patient patient = appointment.Patient;
                patientTb.Text = patient.FirstName + " " + patient.LastName;

                diseaseHistoryTb.Text = patient.MedicalRecord.DiseaseHistory;

                heightTb.Text = patient.MedicalRecord.Height.ToString();

                weightTb.Text = patient.MedicalRecord.Weight.ToString();

                allergensView.Items.Clear();
                foreach (Ingredient allergen in patient.MedicalRecord.Allergens)
                {
                    allergensView.Items.Add(allergen.Id + " - " + allergen.Name);
                }

                prescriptionView.Items.Clear();
                foreach (Prescription prescription in patient.MedicalRecord.Prescriptions)
                {
                    prescriptionView.Items.Add(prescription.Drug.Name + ": " +
                        prescription.Start.Date.ToString("dd/MM/yyyy") + " - " + prescription.End.Date.ToString("dd/MM/yyyy"));
                }
            }
        }

        private void RefreshBtb_Click(object sender, RoutedEventArgs e)
        {
            InitializeAppointments();
        }
    }
}
