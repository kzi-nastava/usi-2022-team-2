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
        DateTime startPoint;
        Dictionary<string, Appointment> appontmentsDisplay;
        Dictionary<string, Ingredient> ingrediantsDisplay;

        public DoctorWindow(HealthCareFactory factory)
        {
            this.factory = factory;
            doctor = (Doctor)factory.User;
            Title = doctor.FirstName + " " + doctor.LastName;

            InitializeComponent();

            InitializeAppointmentType();

            appointmentDate.DisplayDateStart = DateTime.Now;

            StartBtn.IsEnabled = false;
            EndBtn.IsEnabled = false;
            ChangeBtn.IsEnabled = false;
            DeleteBtn.IsEnabled = false;
            PrescribeBtn.IsEnabled = false;
            ReferralBtn.IsEnabled = false;
            RefreshAllergensBtn.IsEnabled = false;
            RefreshPrescriptionsBtn.IsEnabled = false;
            RefreshBtn.IsEnabled = false;
            AddAllergensBtn.IsEnabled = false;

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
            List<Appointment> appointments = doctor.FilterAppointments(startPoint);
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

        void InitializeAllergens(Patient patient)
        {
            allergensView.Items.Clear();
            List<Ingredient> allergens = patient.MedicalRecord.Allergens;
            List<Ingredient> sortedAllergens = allergens.OrderBy(x => x.Id).ToList();

            foreach (Ingredient allergen in sortedAllergens)
            {
                allergensView.Items.Add(allergen.Id + " - " + allergen.Name);
            }
        }

        void InitializePrescriptions(Patient patient)
        {
            prescriptionView.Items.Clear();
            List<Prescription> prescriptions = patient.MedicalRecord.Prescriptions;
            List<Prescription> sortedPrescriptions = prescriptions.OrderBy(x => x.Start).ToList();

            foreach (Prescription prescription in sortedPrescriptions)
            {
                prescriptionView.Items.Add(prescription.Drug.Name + ": " +
                    prescription.Start.Date.ToString("dd/MM/yyyy") + " - " + prescription.End.Date.ToString("dd/MM/yyyy"));
            }
        }

        void InitializeIngrediants(Patient patient)
        {
            ingrediantCb.Items.Clear();
            ingrediantsDisplay = new Dictionary<string, Ingredient>();
            List<Ingredient> ingredients = factory.IngredientController.Ingredients;
            List<Ingredient> sortedIngrediants = ingredients.OrderBy(x => x.Id).ToList();

            foreach (Ingredient ingredient in sortedIngrediants)
            {
                if (!patient.MedicalRecord.Allergens.Contains(ingredient))
                {
                    ingrediantCb.Items.Add(ingredient.Id + " - " + ingredient.Name);
                    ingrediantsDisplay.Add(ingredient.Id + " - " + ingredient.Name, ingredient);
                }
            }
        }

        private void AppointmentView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (appointmentView.SelectedIndex != -1)
            {
                StartBtn.IsEnabled = true;
                ChangeBtn.IsEnabled = true;
                DeleteBtn.IsEnabled = true;
                RefreshPrescriptionsBtn.IsEnabled = true;
                RefreshAllergensBtn.IsEnabled = true;

                Appointment appointment = appontmentsDisplay[appointmentView.SelectedItem.ToString()];

                Room room = appointment.Room;
                roomTb.Text = room.Id + " - " + room.Name;

                Patient patient = appointment.Patient;
                patientTb.Text = patient.FirstName + " " + patient.LastName;

                diseaseHistoryTb.Text = patient.MedicalRecord.DiseaseHistory;

                heightTb.Text = patient.MedicalRecord.Height.ToString();

                weightTb.Text = patient.MedicalRecord.Weight.ToString();

                InitializeAllergens(patient);

                InitializePrescriptions(patient);

                InitializeIngrediants(patient);
            }
        }

        private void RefreshBtn_Click(object sender, RoutedEventArgs e)
        {
            InitializeAppointments();

            StartBtn.IsEnabled = false;
            EndBtn.IsEnabled = false;
            ChangeBtn.IsEnabled = false;
            DeleteBtn.IsEnabled = false;
            PrescribeBtn.IsEnabled = false;
            ReferralBtn.IsEnabled = false;
            RefreshAllergensBtn.IsEnabled = false;
            RefreshPrescriptionsBtn.IsEnabled = false;

            roomTb.IsEnabled = false;
            patientTb.IsEnabled = false;

            heightTb.IsEnabled = false;
            weightTb.IsEnabled = false;
            diseaseHistoryTb.IsEnabled = false;
            anamnesisTb.IsEnabled = false;
        }

        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            appointmentView.IsEnabled = false;

            StartBtn.IsEnabled = false;
            ChangeBtn.IsEnabled = false;
            DeleteBtn.IsEnabled = false;
            RefreshBtn.IsEnabled = false;

            EndBtn.IsEnabled = true;
            PrescribeBtn.IsEnabled = true;
            ReferralBtn.IsEnabled = true;
            AddAllergensBtn.IsEnabled = true;

            heightTb.IsEnabled = true;
            weightTb.IsEnabled = true;
            diseaseHistoryTb.IsEnabled = true;
            anamnesisTb.IsEnabled = true;
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

        private void EndBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Appointment appointment = appontmentsDisplay[appointmentView.SelectedItem.ToString()];
                int height = Convert.ToInt32(heightTb.Text);
                int weight = Convert.ToInt32(weightTb.Text);
                string diseaseHisory = diseaseHistoryTb.Text;

                string anamnesis = anamnesisTb.Text;
                if (anamnesis == "")
                    if (MessageBox.Show("Are you sure you want to end without anamnesis?", "Confirm", MessageBoxButton.YesNo) ==
                        MessageBoxResult.No) return;

                factory.MedicalRecordController.UpdateMedicalRecord(appointment.Patient.MedicalRecord.Id,
                    height, weight, diseaseHisory);
                factory.AnamnesisController.UpdateAnamnesis(appointment.Anamnesis.Id, anamnesis);
                MessageBox.Show("Appointment finished!");

                appointment.Status = AppointmentStatus.FINISHED;
                factory.AppointmentController.Serialize();

                InitializeAppointments();

                appointmentView.IsEnabled = true;

                EndBtn.IsEnabled = false;
                PrescribeBtn.IsEnabled = false;
                ReferralBtn.IsEnabled = false;
                AddAllergensBtn.IsEnabled = false;

                RefreshBtn.IsEnabled = true;

                heightTb.IsEnabled = false;
                weightTb.IsEnabled = false;
                diseaseHistoryTb.IsEnabled = false;
                anamnesisTb.IsEnabled = false;
            }
            catch (FormatException)
            {
                MessageBox.Show("Height and weight must be numbers!");
            }
        }

        private void RefreshAllergensBtn_Click(object sender, RoutedEventArgs e)
        {
            InitializeAllergens(appontmentsDisplay[appointmentView.SelectedItem.ToString()].Patient);
        }

        private void RefreshPrescriptionsBtn_Click(object sender, RoutedEventArgs e)
        {
            InitializePrescriptions(appontmentsDisplay[appointmentView.SelectedItem.ToString()].Patient);
        }

        private void AddAllergensBtn_Click(object sender, RoutedEventArgs e)
        {
            Patient patient = appontmentsDisplay[appointmentView.SelectedItem.ToString()].Patient;
            if (ingrediantCb.SelectedIndex == -1)
            {
                MessageBox.Show("You haven't selected an ingrediant!");
                return;
            }
            Ingredient allergen = ingrediantsDisplay[ingrediantCb.SelectedItem.ToString()];

            patient.MedicalRecord.Allergens.Add(allergen);

            InitializeAllergens(patient);
            InitializeIngrediants(patient);
            
        }

        private void DisplayBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                startPoint = displayDate.SelectedDate.Value;
                InitializeAppointments();
                RefreshBtn.IsEnabled = true;
            }
            catch
            {
                MessageBox.Show("Display date not selected!");
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
