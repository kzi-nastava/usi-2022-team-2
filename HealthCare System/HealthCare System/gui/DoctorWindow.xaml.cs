using HealthCare_System.entities;
using HealthCare_System.factory;
using System;
using System.Collections.Generic;
using System.Linq;
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
            doctor = (Doctor)factory.User;

            InitializeComponent();

            InitializeAppointments();
            InitializeAppointmentType();
            appointmentDate.DisplayDateStart = DateTime.Now;

            StartBtn.IsEnabled = false;
            EndBtn.IsEnabled = false;
            ChangeBtn.IsEnabled = false;
            DeleteBtn.IsEnabled = false;
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
            List<Appointment> sortedAppoinments = appointments.OrderBy(x => x.Start).ToList();

            foreach (Appointment appointment in sortedAppoinments)
            {
                string key = appointment.Start.ToString() + ", Room: " + appointment.Room.Id;
                appontmentsDisplay.Add(key, appointment);
                appointmentView.Items.Add(key);
            }

        }

        void InitializeAppointmentType()
        {
            typeCb.Items.Add(AppointmentType.EXAMINATION);
            typeCb.Items.Add(AppointmentType.OPERATION);
        }

        private void AppointmentView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (appointmentView.SelectedIndex != -1)
            {
                StartBtn.IsEnabled = true;
                ChangeBtn.IsEnabled = true;
                DeleteBtn.IsEnabled = true;

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

            StartBtn.IsEnabled = false;
            EndBtn.IsEnabled = false;
            ChangeBtn.IsEnabled = false;
            DeleteBtn.IsEnabled = false;
            PrescribeBtn.IsEnabled = false;
            ReferralBtn.IsEnabled = false;

            roomTb.IsEnabled = false;
            patientTb.IsEnabled = false;

            heightTb.IsEnabled = false;
            weightTb.IsEnabled = false;
            diseaseHistoryTb.IsEnabled = false;
            anamnesisTb.IsEnabled = false;
        }

        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TypeCb_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (typeCb.SelectedIndex != -1)
            {
                if ((AppointmentType)typeCb.SelectedItem == AppointmentType.EXAMINATION)
                {
                    durationTb.Text = "15";
                    durationTb.IsEnabled = false;
                }
                else
                {
                    durationTb.IsEnabled = true;
                    durationTb.Clear();
                }
            }
        }

        private void BookBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string patientJmbg = patientJmbgTb.Text;
                Patient patient = factory.PatientController.FindByJmbg(patientJmbg);
                if (patient is null)
                {
                    MessageBox.Show("Patient doesn't exist!");
                    return;
                }

                DateTime date = appointmentDate.SelectedDate.Value;
                int hour = Convert.ToInt32(timeTb.Text.Split(":")[0]);
                int min = Convert.ToInt32(timeTb.Text.Split(":")[1]);
                DateTime start = new(date.Year, date.Month, date.Day, hour, min, 0);

                if (typeCb.SelectedIndex == -1)
                {
                    MessageBox.Show("You haven't selected the appointment type!");
                    return;
                }
                AppointmentType type = (AppointmentType)typeCb.SelectedItem;

                int duration;
                try
                {
                    duration = Convert.ToInt32(durationTb.Text);
                }
                catch
                {
                    MessageBox.Show("Duration muth be an integer!");
                    return;
                }

                factory.AddAppointment(start, start.AddMinutes(duration), doctor, patient, type, AppointmentStatus.BOOKED, false);
                MessageBox.Show("Appointment booked.");
                InitializeAppointments();
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("You haven't picked a date!");
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Wrong time format!");
            }
            catch (FormatException)
            {
                MessageBox.Show("Wrong time format!");
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Wrong time format!");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete the appointment?", "Confirm", MessageBoxButton.YesNo) == 
                MessageBoxResult.Yes)
            {
                factory.DeleteAppointment(appontmentsDisplay[appointmentView.SelectedItem.ToString()].Id);
                InitializeAppointments();
            }
        }

        private void ChangeBtn_Click(object sender, RoutedEventArgs e)
        {
            Window window = new ChangeAppointmentWindow(appontmentsDisplay[appointmentView.SelectedItem.ToString()], factory);
            window.Show();
        }
    }
}
