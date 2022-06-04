using HealthCare_System.Model;
using HealthCare_System.factory;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using HealthCare_System.Database;
using HealthCare_System.Services.ReferralServices;
using HealthCare_System.Model.Dto;

namespace HealthCare_System.gui
{
    public partial class ReferralWindow : Window
    {
        Patient patient;
        HealthCareDatabase database;
        Dictionary<string, Doctor> doctorsDisplay;
        ReferralService referralService;

        public ReferralWindow(Patient patient, HealthCareDatabase database)
        {
            this.patient = patient;
            this.database  =  database;

            InitializeComponent();

            InitializeSpecialization();
            InitializeDoctors();

            referralService = new(database.ReferralRepo);
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
            List<Doctor> doctors = database.DoctorRepo.Doctors;
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

            int id = database.ReferralRepo.GenerateId();
            ReferralDto referralDto = new(id, specialization, doctor, patient.MedicalRecord, false);
            referralService.Add(referralDto);
            MessageBox.Show("Referral issued!");
        }
    }
}
