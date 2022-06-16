using System.Collections.Generic;
using System.Linq;
using System.Windows;
using HealthCare_System.Core.Referrals;
using HealthCare_System.Core.Referrals.Model;
using HealthCare_System.Core.Users;
using HealthCare_System.Core.Users.Model;
using HealthCare_System.GUI.Controller.Referrals;
using HealthCare_System.GUI.Controller.Users;

namespace HealthCare_System.GUI.DoctorView
{
    public partial class ReferralWindow : Window
    {
        Patient patient;
        Dictionary<string, Doctor> doctorsDisplay;
        ReferralController referralController;
        DoctorController doctorController;

        public ReferralWindow(Patient patient, IReferralService referralService, IDoctorService doctorService)
        {
            this.patient = patient;

            InitializeComponent();

            referralController = new(referralService);
            doctorController = new(doctorService);

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
            List<Doctor> doctors = doctorController.Doctors();
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

            int id = referralController.GenerateId();
            ReferralDto referralDto = new(id, specialization, doctor, patient.MedicalRecord, false);
            referralController.Add(referralDto);
            MessageBox.Show("Referral issued!");
        }
    }
}
