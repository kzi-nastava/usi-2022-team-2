using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using HealthCare_System.Core.Anamneses;
using HealthCare_System.Core.Appointments;
using HealthCare_System.Core.Appointments.Model;
using HealthCare_System.Core.DaysOffRequests.Model;
using HealthCare_System.Core.Drugs;
using HealthCare_System.Core.Drugs.Model;
using HealthCare_System.Core.Ingredients.Model;
using HealthCare_System.Core.MedicalRecords;
using HealthCare_System.Core.Prescriptions.Model;
using HealthCare_System.Core.Renovations;
using HealthCare_System.Core.Rooms;
using HealthCare_System.Core.Rooms.Model;
using HealthCare_System.Core.Users.Model;
using HealthCare_System.Database;
using HealthCare_System.GUI.Controller.Anamneses;
using HealthCare_System.GUI.Controller.Appointments;
using HealthCare_System.GUI.Controller.DaysOffRequests;
using HealthCare_System.GUI.Controller.Drugs;
using HealthCare_System.GUI.Controller.Ingredients;
using HealthCare_System.GUI.Controller.MedicalRecords;
using HealthCare_System.GUI.Controller.Users;
using HealthCare_System.GUI.Main;
using HealthCare_System.GUI.SecretaryView;

namespace HealthCare_System.GUI.DoctorView
{
    public partial class DoctorWindow : Window
    {
        ServiceBuilder serviceBuilder;
        Doctor doctor;
        DateTime startPoint;

        Dictionary<string, Appointment> appontmentsDisplay;
        Dictionary<string, Ingredient> ingrediantsDisplay;
        Dictionary<string, Drug> drugsDisplay;

        SchedulingController schedulingController;
        AppointmentController appointmentController;
        AnamnesisController anamnesisController;
        MedicalRecordController medicalRecordController;
        DrugController drugController;
        PatientController patientController;
        IngredientController ingredientController;
        DaysOffRequestController daysOffRequestController;

        public DoctorWindow(Doctor doctor, ServiceBuilder serviceBuilder)
        {
            this.serviceBuilder = serviceBuilder;
            this.doctor = doctor;

            InitializeComponent();

            InitializeControllers();

            InitializeAppointmentType();

            InitializeDrugs();

            InitializeDaysOff();

            InitializeDaysOffRequests();

            appointmentDate.DisplayDateStart = DateTime.Now;
            startDayOffDate.DisplayDateStart = DateTime.Now.AddDays(2);

            DisableComponents();

            DelayedAppointmentNotificationWindow notificationWindow = new(new(serviceBuilder.DelayedAppointmentNotificationService), 
                doctor);
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
            List<Ingredient> ingredients = ingredientController.Ingredients();
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
            List<Drug> drugs = drugController.FillterOnHold();
            List<Drug> sortedDrugs = drugs.OrderBy(x => x.Name).ToList();

            foreach (Drug drug in sortedDrugs)
            {
                string key = drug.Name;
                drugsDisplay.Add(key, drug);
                drugView.Items.Add(key);
            }
        }

        void InitializeControllers()
        {
            schedulingController = new(serviceBuilder.SchedulingService);
            appointmentController = new(serviceBuilder.AppointmentService);
            anamnesisController = new(serviceBuilder.AnamnesisService);
            medicalRecordController = new(serviceBuilder.MedicalRecordService);
            drugController = new(serviceBuilder.DrugService);
            patientController = new(serviceBuilder.PatientService);
            ingredientController = new(serviceBuilder.IngredientService);
            daysOffRequestController = new(serviceBuilder.DaysOffRequestService);
        }

        void InitializeDaysOff()
        {
            daysOffView.Items.Clear();
            doctor.FreeDates.Sort((x, y) => x.Date.CompareTo(y.Date));
            foreach (DateTime date in doctor.FreeDates)
                daysOffView.Items.Add(date.ToString("dd/MM/yyyy"));
        }

        void InitializeDaysOffRequests()
        {
            daysOffRequestsView.Items.Clear();
            List<DaysOffRequest> daysOffRequests = daysOffRequestController.FillterByDoctor(doctor);
            List<DaysOffRequest> sortedDaysOffRequest = daysOffRequests.OrderBy(x => x.Start).ToList();
            foreach (DaysOffRequest daysOffRequest in sortedDaysOffRequest)
                daysOffRequestsView.Items.Add(daysOffRequest.Start.ToString("dd/MM/yyyy") + " - " +
                    daysOffRequest.End.ToString("dd/MM/yyyy") + ": " + daysOffRequest.State.ToString());
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

        private AppointmentDto ValidateAppointment()
        {
            int id = appointmentController.GenerateId(); 
            Patient patient = patientController.FindByJmbg(patientJmbgTb.Text);

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
                AppointmentDto appointmentDto = ValidateAppointment();
                if (appointmentDto is null) return;

                schedulingController.AddAppointment(appointmentDto);
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
                schedulingController.DeleteAppointment(appontmentsDisplay[appointmentView.SelectedItem.ToString()].Id);
                InitializeAppointments();
            }
        }

        private void ChangeBtn_Click(object sender, RoutedEventArgs e)
        {
            Appointment appointment = appontmentsDisplay[appointmentView.SelectedItem.ToString()];
            Window window = new ChangeAppointmentWindow(appointment, serviceBuilder.SchedulingService, serviceBuilder.PatientService);
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

            medicalRecordController.Update(appointment.Patient.MedicalRecord.Id,
                height, weight, diseaseHisory);

            anamnesisController.Update(appointment.Anamnesis.Id, anamnesis);
            MessageBox.Show("Appointment finished!");

            appointment.Status = AppointmentStatus.FINISHED;
            appointmentController.Serialize();

            Window dynamicEquipmentWindow = new DynamicEquipmentWindow(appointment.Room, serviceBuilder.EquipmentTransferService, 
                serviceBuilder.RoomService);
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
            Window window = new PrescriptionWindow(patient, serviceBuilder.PrescriptionService, serviceBuilder.DrugService);
            window.Show();
        }

        private void ReferralBtn_Click(object sender, RoutedEventArgs e)
        {
            Patient patient = appontmentsDisplay[appointmentView.SelectedItem.ToString()].Patient;
            Window window = new ReferralWindow(patient, serviceBuilder.ReferralService, serviceBuilder.DoctorService);
            window.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Log out?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                MainWindow main = new MainWindow(serviceBuilder);
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
            drugController.AcceptDrug(drug);
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

            drugController.RejectDrug(drug, message);
            MessageBox.Show("Drug rejected!");

            AcceptBtn.IsEnabled = false;
            RejectBtn.IsEnabled = false;

            InitializeDrugs();
        }

        private void DrugView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (drugView.SelectedIndex != -1)
            {
                drugIngridientView.Items.Clear();
                Drug drug = drugsDisplay[drugView.SelectedItem.ToString()];
                List<Ingredient> ingredients = drug.Ingredients.OrderBy(x => x.Id).ToList();
                foreach (Ingredient ingredient in ingredients)
                    drugIngridientView.Items.Add(ingredient.Id + " - " + ingredient.Name);

                AcceptBtn.IsEnabled = true;
                RejectBtn.IsEnabled = true;
            }
        }

        private DaysOffRequestDto ValidateDaysOffRequest()
        {
            int id = daysOffRequestController.GenerateId();

            DateTime start = PrescriptionWindow.ValidateDate(startDayOffDate, "start");
            if (start == default(DateTime))
                return null;

            DateTime end = PrescriptionWindow.ValidateDate(endDayOffDate, "end");
            if (end == default(DateTime))
                return null;

            string description = reasoningTb.Text;
            if (description == "")
            {
                MessageBox.Show("You have to give reasoning!");
                return null;
            }

            DaysOffRequestState state = DaysOffRequestState.WAITING;
            bool urgent = false;
            if ((bool)urgentChb.IsChecked)
            {
                state = DaysOffRequestState.ACCEPTED;
                urgent = true;
            }

            return new(id, start, end, description, state, urgent, doctor);
            
        }

        private void RequestBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DaysOffRequestDto daysOffRequestDto = ValidateDaysOffRequest();
                if (daysOffRequestDto == null) return;

                if ((bool)urgentChb.IsChecked)
                    daysOffRequestController.UrgentRequest(daysOffRequestDto);
                else
                    daysOffRequestController.Request(daysOffRequestDto);

                MessageBox.Show("Request added!");
                InitializeDaysOffRequests();
                InitializeDaysOff();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void StartDayOffDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            endDayOffDate.DisplayDateStart = startDayOffDate.SelectedDate.Value.AddDays(1);
            endDayOffDate.SelectedDate = startDayOffDate.SelectedDate.Value.AddDays(1);

            if ((bool)urgentChb.IsChecked)
                endDayOffDate.DisplayDateEnd = startDayOffDate.SelectedDate.Value.AddDays(4);
        }

        private void UrgentChb_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((bool)urgentChb.IsChecked)
                    endDayOffDate.DisplayDateEnd = startDayOffDate.SelectedDate.Value.AddDays(4);
                else
                    endDayOffDate.DisplayDateEnd = startDayOffDate.SelectedDate.Value.AddDays(1000);
            }
            catch (Exception)
            {

            }
        }
    }
}
