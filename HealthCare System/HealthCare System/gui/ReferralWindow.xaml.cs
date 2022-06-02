using HealthCare_System.Model;
using HealthCare_System.factory;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace HealthCare_System.gui
{
    public partial class ReferralWindow : Window
    {
        Patient patient;
        HealthCareFactory factory;
        Dictionary<string, Doctor> doctorsDisplay;

        public ReferralWindow(Patient patient, HealthCareFactory factory)
        {
            this.patient = patient;
            this.factory = factory;

            InitializeComponent();

            InitializeSpecialization();
            InitializeDoctors();
        }

        private void InitializeSpecialization()
        {
            specializationCb.Items.Add(Specialization.GENERAL);
            specializationCb.Items.Add(Specialization.GYNECOLOGIST);
            specializationCb.Items.Add(Specialization.SURGEON);
            specializationCb.SelectedItem = Specialization.GENERAL;
        }

        private void InitializeDoctors()
        {
            doctorCb.Items.Clear();
            doctorsDisplay = new Dictionary<string, Doctor>();
            List<Doctor> doctors = factory.DoctorController.Doctors;
            List<Doctor> sortedDoctors = doctors.OrderBy(x => x.Jmbg).ToList();

            foreach (Doctor doctor in sortedDoctors)
            {
                doctorCb.Items.Add(doctor.Jmbg + " - " + doctor.FirstName + " " + doctor.LastName);
                doctorsDisplay.Add(doctor.Jmbg + " - " + doctor.FirstName + " " + doctor.LastName, doctor);
            }
        }

        private void specializationCb_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (specializationCb.SelectedIndex != -1)
                doctorCb.SelectedIndex = -1;
        }

        private void doctorCb_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (doctorCb.SelectedIndex != -1)
                specializationCb.SelectedIndex = -1;
        }

        private void IssueBtn_Click(object sender, RoutedEventArgs e)
        {
            Specialization specialization = Specialization.GENERAL;
            if (specializationCb.SelectedIndex != -1)
                specialization = (Specialization)specializationCb.SelectedItem;

            Doctor doctor = null;
            if (doctorCb.SelectedIndex != -1)
                doctor = doctorsDisplay[doctorCb.SelectedItem.ToString()];

            int id = factory.ReferralController.GenerateId();
            Referral referral = new(id, specialization, doctor, patient.MedicalRecord, false);
            factory.ReferralController.Add(referral);
            MessageBox.Show("Referral issued!");
        }
    }
}
