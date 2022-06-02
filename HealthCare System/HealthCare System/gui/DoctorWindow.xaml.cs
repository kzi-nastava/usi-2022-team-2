using HealthCare_System.Model;
using HealthCare_System.factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace HealthCare_System.gui
{
    public partial class DoctorWindow : Window
    {
        HealthCareFactory factory;
        Doctor doctor;
        DateTime startPoint;
        Dictionary<string, Appointment> appontmentsDisplay;
        Dictionary<string, Ingredient> ingrediantsDisplay;
        Dictionary<string, Drug> drugsDisplay;

        public DoctorWindow(HealthCareFactory factory)
        {
            this.factory = factory;
            doctor = (Doctor)factory.User;
            Title = doctor.FirstName + " " + doctor.LastName;

            InitializeComponent();

            InitializeAppointmentType();

            InitializeDrugs();

            appointmentDate.DisplayDateStart = DateTime.Now;

            DisableComponents();

            DelayedAppointmentNotificationWindow notificationWindow = new DelayedAppointmentNotificationWindow(factory);
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
            typeCb.SelectedItem = AppointmentType.EXAMINATION;
        }

        void InitializeAllergens(Patient patient)
        {
            allergensView.Items.Clear();
            List<Ingredient> allergens = patient.MedicalRecord.Allergens;
            List<Ingredient> sortedAllergens = allergens.OrderBy(x => x.Id).ToList();

            foreach (Ingredient allergen in sortedAllergens)
                allergensView.Items.Add(allergen.Id + " - " + allergen.Name);
        }

        void InitializePrescriptions(Patient patient)
        {
            prescriptionView.Items.Clear();
            List<Prescription> prescriptions = patient.MedicalRecord.Prescriptions;
            List<Prescription> sortedPrescriptions = prescriptions.OrderBy(x => x.Start).ToList();

            foreach (Prescription prescription in sortedPrescriptions)
                prescriptionView.Items.Add(prescription.Drug.Name + ": " +
                    prescription.Start.Date.ToString("dd/MM/yyyy") + " - " + prescription.End.Date.ToString("dd/MM/yyyy"));
        }

        void InitializeIngrediants(Patient patient)
        {
            ingrediantCb.Items.Clear();
            ingrediantsDisplay = new Dictionary<string, Ingredient>();
            List<Ingredient> ingredients = factory.IngredientController.Ingredients;
            List<Ingredient> sortedIngrediants = ingredients.OrderBy(x => x.Id).ToList();

            foreach (Ingredient ingredient in sortedIngrediants)
                if (!patient.MedicalRecord.Allergens.Contains(ingredient))
                {
                    ingrediantCb.Items.Add(ingredient.Id + " - " + ingredient.Name);
                    ingrediantsDisplay.Add(ingredient.Id + " - " + ingredient.Name, ingredient);
                }
        }

        void InitializeDrugs()
        {
            drugView.Items.Clear();
            drugsDisplay = new Dictionary<string, Drug>();
            List<Drug> drugs = factory.DrugController.FillterOnHold();
            List<Drug> sortedDrugs = drugs.OrderBy(x => x.Name).ToList();

            foreach (Drug drug in sortedDrugs)
            {
                string key = drug.Name;
                drugsDisplay.Add(key, drug);
                drugView.Items.Add(key);
            }
        }

        void DisableComponents()
        {
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
            AcceptBtn.IsEnabled = false;
            RejectBtn.IsEnabled = false;

            roomTb.IsEnabled = false;
            patientTb.IsEnabled = false;

            heightTb.IsEnabled = false;
            weightTb.IsEnabled = false;
            diseaseHistoryTb.IsEnabled = false;
            anamnesisTb.IsEnabled = false;

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
            if ((AppointmentType)typeCb.SelectedItem == AppointmentType.EXAMINATION)
            {
                durationTb.Text = "15";
                durationTb.IsEnabled = false;
                return;
            }
                
            durationTb.IsEnabled = true;
            durationTb.Clear();
        }

        public static DateTime ValidateDate(DatePicker datePicker, TextBox textBox)
        {
            try 
            { 
                DateTime date = datePicker.SelectedDate.Value;
                int hour = Convert.ToInt32(textBox.Text.Split(":")[0]);
                int min = Convert.ToInt32(textBox.Text.Split(":")[1]);
                DateTime start = new(date.Year, date.Month, date.Day, hour, min, 0);
                return start;
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

            return default(DateTime);
        }

        public static int ValidateTextBox(TextBox textBox, string message)
        {
            try
            {
                int returnValue = Convert.ToInt32(textBox.Text);

                if (returnValue == 0)
                {
                    MessageBox.Show(message + " must be at least 1!");
                    return -1;
                }

                return returnValue;
            }
            catch
            {
                MessageBox.Show(message + " must be an integer!");
                return -1;
            }
        }

        private Appointment ValidateAppointment()
        {
            int id = factory.AppointmentController.GenerateId();
            Patient patient = factory.PatientController.FindByJmbg(patientJmbgTb.Text);

            DateTime start = ValidateDate(appointmentDate, timeTb);
            if (start == default(DateTime))
                return null;

            AppointmentType type = (AppointmentType)typeCb.SelectedItem;

            int duration = ValidateTextBox(durationTb, "Duration");
            if (duration == -1)
                return null;

            return new(id, start, start.AddMinutes(duration), doctor, patient, null, type,
                AppointmentStatus.BOOKED, null, false, false);            
        }

        private void BookBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Appointment appointment = ValidateAppointment();
                if (appointment is null) return;

                factory.AddAppointment(appointment);
                MessageBox.Show("Appointment booked.");
                InitializeAppointments();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete the appointment?", 
                "Confirm", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                factory.DeleteAppointment(appontmentsDisplay[appointmentView.SelectedItem.ToString()].Id);
                InitializeAppointments();
            }
        }

        private void ChangeBtn_Click(object sender, RoutedEventArgs e)
        {
            Appointment appointment = appontmentsDisplay[appointmentView.SelectedItem.ToString()];
            Window window = new ChangeAppointmentWindow(appointment, factory);
            window.Show();
        }

        private void EndBtn_Click(object sender, RoutedEventArgs e)
        {
            
            Appointment appointment = appontmentsDisplay[appointmentView.SelectedItem.ToString()];

            int height = ValidateTextBox(heightTb, "Height");
            if (height == -1)
                return;

            int weight = ValidateTextBox(weightTb, "Weight");
            if (weight == -1)
                return;

            string diseaseHisory = diseaseHistoryTb.Text;

            string anamnesis = anamnesisTb.Text;
            if (anamnesis == "")
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to end without anamnesis?", 
                    "Confirm", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.No) return;
            }

            factory.MedicalRecordController.UpdateMedicalRecord(appointment.Patient.MedicalRecord.Id,
                height, weight, diseaseHisory);
            factory.AnamnesisController.UpdateAnamnesis(appointment.Anamnesis.Id, anamnesis);
            MessageBox.Show("Appointment finished!");

            appointment.Status = AppointmentStatus.FINISHED;
            factory.AppointmentController.Serialize();

            Window dynamicEquipmentWindow = new DynamicEquipmentWindow(appointment.Room, factory);
            dynamicEquipmentWindow.Show();

            InitializeAppointments();

            appointmentView.IsEnabled = true;

            EndBtn.IsEnabled = false;
            PrescribeBtn.IsEnabled = false;
            ReferralBtn.IsEnabled = false;
            AddAllergensBtn.IsEnabled = false;

            RefreshBtn.IsEnabled = true;
            RefreshAllergensBtn.IsEnabled = false;
            RefreshPrescriptionsBtn.IsEnabled = false;

            heightTb.IsEnabled = false;
            weightTb.IsEnabled = false;
            diseaseHistoryTb.IsEnabled = false;
            anamnesisTb.IsEnabled = false;
            
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

        private void PrescribeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (anamnesisTb.Text == "")
            {
                MessageBox.Show("You have to enter anamnesis before issuing a prescription.");
                return;
            }
            Patient patient = appontmentsDisplay[appointmentView.SelectedItem.ToString()].Patient;
            Window window = new PrescriptionWindow(patient, factory);
            window.Show();
        }

        private void ReferralBtn_Click(object sender, RoutedEventArgs e)
        {
            Patient patient = appontmentsDisplay[appointmentView.SelectedItem.ToString()].Patient;
            Window window = new ReferralWindow(patient, factory);
            window.Show();
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

        private void DurationTb_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void HeightTb_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }        

        private void WeightTb_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void AcceptBtn_Click(object sender, RoutedEventArgs e)
        {
            Drug drug = drugsDisplay[drugView.SelectedItem.ToString()];
            factory.DrugController.AcceptDrug(drug);
            MessageBox.Show("Drug accepted!");

            AcceptBtn.IsEnabled = false;
            RejectBtn.IsEnabled = false;

            InitializeDrugs();
        }

        private void RejectBtn_Click(object sender, RoutedEventArgs e)
        {
            Drug drug = drugsDisplay[drugView.SelectedItem.ToString()];

            string message = rejectionTb.Text;
            if (message == "")
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to reject without a message?",
                    "Confirm", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.No) return;
            }

            factory.DrugController.RejectDrug(drug, message);
            MessageBox.Show("Drug rejected!");

            AcceptBtn.IsEnabled = false;
            RejectBtn.IsEnabled = false;

            InitializeDrugs();
        }

        private void DrugView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (drugView.SelectedIndex != -1)
            {
                Drug drug = drugsDisplay[drugView.SelectedItem.ToString()];
                List<Ingredient> ingredients = drug.Ingredients.OrderBy(x => x.Id).ToList();
                foreach (Ingredient ingredient in ingredients)
                    drugIngridientView.Items.Add(ingredient.Id + " - " + ingredient.Name);

                AcceptBtn.IsEnabled = true;
                RejectBtn.IsEnabled = true;
            }
        }
    }
}
