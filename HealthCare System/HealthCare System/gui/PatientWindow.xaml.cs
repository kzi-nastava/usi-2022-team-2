using HealthCare_System.controllers;
using HealthCare_System.entities;
using HealthCare_System.factory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HealthCare_System.gui
{
    public partial class PatientWindow : Window
    {
        HealthCareFactory factory;
        Dictionary<int,Doctor> indexedDoctors;
        Dictionary<int,Doctor> indexedDoctorsEditTab;
        Dictionary<int,Appointment> indexedAppointments;
        Dictionary<int,Appointment> indexedRecommendations;
        Dictionary<int,Appointment> indexedAppointmentsHistory;

        public PatientWindow(HealthCareFactory factory)
        {
            Title = factory.User.FirstName + " " + factory.User.LastName;
            this.factory = factory;
            InitializeComponent();
            InitializeDoctors();
            InitializeAnamnesesTab();
            recommendedDoctorCb.SelectedItem = indexedDoctors[0];
            indexedAppointments = new Dictionary<int, Appointment>();
            indexedAppointmentsHistory = new Dictionary<int, Appointment>();
            indexedRecommendations= new Dictionary<int, Appointment>();
            indexedDoctorsEditTab = new Dictionary<int, Doctor>();
            UpdateLb();
            datePicker.DisplayDateStart = DateTime.Now;
            datePickerEdit.DisplayDateStart = DateTime.Now;
            UpdateAppointmentHistory();

            DelayedAppointmentNotificationWindow notificationWindow = new DelayedAppointmentNotificationWindow(factory);
        }

        public void UpdateAppointmentHistory()
        {
            Patient patient = (Patient)factory.User;
            indexedAppointmentsHistory.Clear();
            appointmentHistoryLb.Items.Clear();
            List<Appointment> sortedAppoinments = patient.MedicalRecord.Appointments.OrderBy(x => x.Start).ToList();
            int iterationNum=0;
            foreach (Appointment appointment in patient.MedicalRecord.Appointments)
            {
                indexedAppointmentsHistory.Add(iterationNum, appointment);
                string roomInfo = "";
                if (appointment.Room != null)
                    roomInfo=", room: " + appointment.Room.Name;
                if (DateTime.Now > appointment.Start && appointment.Status != AppointmentStatus.ON_HOLD)
                {
                    appointmentHistoryLb.Items.Add("Start: "
                        + appointment.Start.ToString("dd/MM/yyyy HH:mm") + ", doctor: "
                        + appointment.Doctor.FirstName + " " + appointment.Doctor.LastName +
                        ", type: " + appointment.Type.ToString().ToLower()
                        + roomInfo);
                }
                iterationNum++;
            }
        }

        public void InitializeAnamnesesTab()
        {
            sortingDirectionCb.ItemsSource = Enum.GetValues(typeof(SortDirection));
            sortingDirectionCb.SelectedIndex = 0;
            sortCriteriumCb.ItemsSource = Enum.GetValues(typeof(AnamnesesSortCriterium));
            sortCriteriumCb.SelectedIndex = 0;
        }
        public void InitializeDoctors()
        {
            indexedDoctors = new Dictionary<int, Doctor>();
            
            int index = 0;
            List<Doctor> doctors = factory.DoctorController.FindBySpecialization(Specialization.GENERAL);
            foreach (Doctor doctor in doctors)
            {
                indexedDoctors.Add(index,doctor);
                doctorCb.Items.Add(doctor.FirstName + " " + doctor.LastName);
                recommendedDoctorCb.Items.Add(doctor.FirstName + " " + doctor.LastName);
                index++;
            }
        }
        public void UpdateDoctors(Specialization specialization)
        {
            indexedDoctorsEditTab.Clear();
            doctorEditCb.Items.Clear();
            List<Doctor> doctors = factory.DoctorController.FindBySpecialization(specialization);
            int index = 0;
            foreach (Doctor doctor in doctors)
            {
                indexedDoctorsEditTab.Add(index, doctor);
                doctorEditCb.Items.Add(doctor.FirstName + " " + doctor.LastName);
                index++;
            }
        }
        private void bookBtn_Click(object sender, RoutedEventArgs e)
        {
            CheckAntiTroll();
            if (((Patient)factory.User).Blocked)
            {
                Close();
                return;
            }
            if (doctorCb.SelectedIndex == -1)
            {
                MessageBox.Show("Please choose doctor.");
                return;
            }
            DateTime start;
            DateTime date = datePicker.SelectedDate.Value;
            if (date < DateTime.Now)
            {
                MessageBox.Show("Invalid Date.");
                return;
            }
            try
            {
                int hour = Convert.ToInt32(timeTb.Text.Split(':')[0]);
                int minute = Convert.ToInt32(timeTb.Text.Split(':')[1]);
                start = new DateTime(date.Year, date.Month, date.Day, hour, minute, 0);
            }
            catch
            {
                MessageBox.Show("Invalid Time.");
                return;
            }
            if (start <= DateTime.Now)
            {
                MessageBox.Show("Appointment booked too soon.");
                return;
            }
            Doctor doctor = indexedDoctors[doctorCb.SelectedIndex];
            DateTime end = start.AddMinutes(15);
            Patient patient = (Patient) factory.User;
            try
            {
                Appointment appointment=factory.AddAppointment(start, end, doctor, patient, 
                    AppointmentType.EXAMINATION, AppointmentStatus.BOOKED, false);
                AppointmentRequest request = new AppointmentRequest(factory.AppointmentRequestController.GenerateId(),
                    AppointmentState.ACCEPTED, patient, appointment, null, RequestType.CREATE, DateTime.Now);
                factory.AppointmentRequestController.Add(request);
                MessageBox.Show("Appointment booked");
                datePicker.SelectedDate = DateTime.Now;
                timeTb.Clear();
                doctorCb.SelectedIndex = -1;
                UpdateLb();
            }
            catch(Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

        }
        private void UpdateLb()
        {
            indexedAppointments.Clear();
            myAppointmentsLb.Items.Clear();
            Patient patient = (Patient)factory.User;
            int index = 0;
            List<Appointment> sortedAppoinments = patient.MedicalRecord.Appointments.OrderBy(x => x.Start).ToList();
            foreach (Appointment appointment in sortedAppoinments)
            {
                if (DateTime.Now < appointment.Start && appointment.Status!=AppointmentStatus.ON_HOLD)
                {
                    string roomInfo = "";
                    if (appointment.Room != null)
                        roomInfo = ", room: " + appointment.Room.Name;
                    indexedAppointments.Add(index, appointment);
                    myAppointmentsLb.Items.Add(appointment.Start.ToString("dd/MM/yyyy HH:mm") +
                  ", doctor: " + appointment.Doctor.FirstName + " " + appointment.Doctor.LastName +
                ", type: " + appointment.Type.ToString().ToLower() + roomInfo);
                    index++;
                }
            }
        }

        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            CheckAntiTroll();
            if (((Patient)factory.User).Blocked)
            {
                Close();
                return;
            }
            if (myAppointmentsLb.SelectedIndex == -1)
            {
                MessageBox.Show("Please select appointment");
                return;
            }
            Appointment appointment = indexedAppointments[myAppointmentsLb.SelectedIndex];
            if (doctorEditCb.SelectedIndex == -1)
            {
                MessageBox.Show("Please choose doctor.");
                return;
            }
            DateTime start;
            DateTime date = datePickerEdit.SelectedDate.Value;
            if (date <= DateTime.Now)
            {
                MessageBox.Show("Invalid Date.");
                return;
            }
            try
            {
                int hour = Convert.ToInt32(timeEditTb.Text.Split(':')[0]);
                int minute = Convert.ToInt32(timeEditTb.Text.Split(':')[1]);
                start = new DateTime(date.Year, date.Month, date.Day, hour, minute, 0);
            }
            catch
            {
                MessageBox.Show("Invalid Time.");
                return;
            }
            bool needConfirmation = false;
            if (start <= DateTime.Now.AddDays(1))
            {
                MessageBox.Show("You cannot update appointment less than 24 hours before start.");
                return;
            }
            else if (start <= DateTime.Now.AddDays(2))
            {
                needConfirmation = true;
            }
            if (doctorEditCb.SelectedIndex == -1)
            {
                MessageBox.Show("Please choose doctor.");
                return;
            }
            
            Doctor doctor = indexedDoctors[doctorEditCb.SelectedIndex];
            DateTime end = start.AddMinutes((appointment.End-appointment.Start).TotalMinutes);
            Patient patient = (Patient)factory.User;
            Appointment requestedAppointment = null;
            try
            {
                AppointmentState state = AppointmentState.ACCEPTED;
                if (needConfirmation)
                {

                    requestedAppointment=factory.AddAppointment(start, end, doctor, patient, appointment.Type, AppointmentStatus.ON_HOLD, false);
                    state = AppointmentState.WAITING;
                }
                else
                {
                    factory.UpdateAppointment(appointment.Id, start, end, doctor, patient, AppointmentStatus.BOOKED);
                }
                
                AppointmentRequest request = new AppointmentRequest(factory.AppointmentRequestController.GenerateId(),
                    state, patient, appointment, requestedAppointment, RequestType.UPDATE, DateTime.Now);
                factory.AppointmentRequestController.Add(request);
                UpdateLb();
                if (needConfirmation)
                {
                    MessageBox.Show("Appointment request made successfully.");
                }
                else
                {
                    MessageBox.Show("Appointment updated successfully.");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void myAppointmentsLb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (myAppointmentsLb.SelectedIndex != -1)
            {
                Appointment appointment = indexedAppointments[myAppointmentsLb.SelectedIndex];
                datePickerEdit.SelectedDate = appointment.Start;
                timeEditTb.Text = appointment.Start.ToString("hh:mm");
                UpdateDoctors(appointment.Doctor.Specialization);
                foreach (KeyValuePair<int, Doctor> entry in indexedDoctorsEditTab)
                {
                    if (entry.Value == appointment.Doctor)
                    {
                        doctorEditCb.SelectedIndex = entry.Key;
                        break;
                    }
                }
            }
            

        }


        public void CheckAntiTroll()
        {
            factory.AppointmentRequestController.RunAntiTrollCheck((Patient)factory.User);
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            CheckAntiTroll();
            if (((Patient)factory.User).Blocked)
            {
                Close();
                return;
            }


            if (myAppointmentsLb.SelectedIndex == -1)
            {
                MessageBox.Show("Please select appointment");
                return;
            }
            Appointment appointment = indexedAppointments[myAppointmentsLb.SelectedIndex];
            
            bool needConfirmation = false;
            if (appointment.Start <= DateTime.Now.AddDays(1))
            {
                MessageBox.Show("You cannot delete appointment less than 24 hours before start.");
                return;
            }
            else if (appointment.Start <= DateTime.Now.AddDays(2))
            {
                needConfirmation = true;
            }
            try
            {
                AppointmentState state = AppointmentState.ACCEPTED;
                if (!needConfirmation)
                {
                    factory.DeleteAppointment(appointment.Id);
                }
                else
                {
                    appointment.Status = AppointmentStatus.ON_HOLD;
                    state = AppointmentState.WAITING;
                }
                Patient patient = (Patient)factory.User;
                AppointmentRequest request = new AppointmentRequest(factory.AppointmentRequestController.GenerateId(),
                    state, patient, appointment, null, RequestType.DELETE, DateTime.Now);
                factory.AppointmentRequestController.Add(request);
                UpdateLb();
                if (needConfirmation)
                {
                    MessageBox.Show("Appointment request made successfully.");
                }
                else
                {
                    MessageBox.Show("Appointment deleted successfully.");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        void DataWindow_Closing(object sender, CancelEventArgs e)
        {
            if (!((Patient) factory.User).Blocked)
            {
                factory.User = null;
                if (MessageBox.Show("Log out?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    MainWindow main = new MainWindow(factory);
                    main.Show();
                }
                else e.Cancel = true;
            }
            else
            {
                factory.User = null;
                MessageBox.Show("Account blocked. Contact secretary for more informations!");
                MainWindow main = new MainWindow(factory);
                main.Show();
            }
            
        }

        private void refreshHistory_Click(object sender, RoutedEventArgs e)
        {
            UpdateAppointmentHistory();
        }
        private void anamnesesSearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (anamnesesSearchTb.Text.Length >= 3 && sortCriteriumCb.SelectedIndex != -1 && sortingDirectionCb.SelectedIndex != -1)
            {
                List<Appointment> sortedAppointments = factory.AppointmentController.SortAnamneses((Patient)factory.User, anamnesesSearchTb.Text,
                   (AnamnesesSortCriterium)Enum.Parse(typeof(AnamnesesSortCriterium), sortCriteriumCb.SelectedItem.ToString()),
                   (SortDirection)Enum.Parse(typeof(SortDirection), sortingDirectionCb.SelectedItem.ToString()));
                sortedAnamnesesLb.Items.Clear();
                foreach (Appointment appointment in sortedAppointments)
                {
                    sortedAnamnesesLb.Items.Add(appointment);
                }
            }
            else
                sortedAnamnesesLb.Items.Clear();
        }

        private void sortCriteriumCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (anamnesesSearchTb.Text.Length >= 3 && sortCriteriumCb.SelectedIndex != -1 && sortingDirectionCb.SelectedIndex != -1)
            {
                List<Appointment> sortedAppointments = factory.AppointmentController.SortAnamneses((Patient)factory.User, anamnesesSearchTb.Text,
                   (AnamnesesSortCriterium)Enum.Parse(typeof(AnamnesesSortCriterium), sortCriteriumCb.SelectedItem.ToString()),
                   (SortDirection)Enum.Parse(typeof(SortDirection), sortingDirectionCb.SelectedItem.ToString()));
                sortedAnamnesesLb.Items.Clear();
                foreach (Appointment appointment in sortedAppointments)
                {
                    sortedAnamnesesLb.Items.Add(appointment);
                }
            }
            else
                sortedAnamnesesLb.Items.Clear();
        }

        private void sortingDirectionCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (anamnesesSearchTb.Text.Length >= 3 && sortCriteriumCb.SelectedIndex != -1 && sortingDirectionCb.SelectedIndex != -1)
            {
                List<Appointment> sortedAppointments = factory.AppointmentController.SortAnamneses((Patient)factory.User, anamnesesSearchTb.Text,
                   (AnamnesesSortCriterium)Enum.Parse(typeof(AnamnesesSortCriterium), sortCriteriumCb.SelectedItem.ToString()),
                   (SortDirection)Enum.Parse(typeof(SortDirection), sortingDirectionCb.SelectedItem.ToString()));
                sortedAnamnesesLb.Items.Clear();
                foreach (Appointment appointment in sortedAppointments)
                {
                    sortedAnamnesesLb.Items.Add(appointment);
                }
            }
            else
                sortedAnamnesesLb.Items.Clear();
        }
        
        private void SearchRecommendationBtn_Click(object sender, RoutedEventArgs e)
        {
            DateTime recommendedEndDate = reccomendedEndDateDp.SelectedDate.Value;

            int[] timeTupleFrom = ValidateTime(recommendedFromTb.Text);
            int[] timeTupleTo = ValidateTime(recommendedToTb.Text);

            Doctor doctor = indexedDoctors[recommendedDoctorCb.SelectedIndex];
            List<Appointment> appointments=factory.RecommendAppointment(recommendedEndDate, timeTupleFrom,
                timeTupleTo, doctor,(bool) priorityDoctorRb.IsChecked);
            if (appointments.Count==1)
            {
                doctorCb.SelectedItem = appointments[0].Doctor.FirstName + " " + appointments[0].Doctor.LastName;
                timeTb.Text = appointments[0].Start.ToString("hh:mm");
                datePicker.SelectedDate = appointments[0].Start.Date;
            }
            else
            {
                for (int i = 0; i < appointments.Count; i++)
                {
                    indexedRecommendations.Add(i, appointments[i]);
                    string appointmentSummary = appointments[i].Doctor + "\t" + appointments[i].Start.ToString("dd/MM/yyyy hh:mm");
                    recommendedAppointmentsLb.Items.Add(appointments[i].Doctor);
                }

            }
        }
        private void PlaceholdersListBox_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var item = ItemsControl.ContainerFromElement(sender as ListBox, e.OriginalSource as DependencyObject) as ListBoxItem;
            if (item != null)
            {
                if (appointmentHistoryLb.SelectedIndex != -1)
                {
                    Appointment appointment = indexedRecommendations[recommendedAppointmentsLb.SelectedIndex];
                    doctorCb.SelectedItem = appointment.Doctor.FirstName + " " + appointment.Doctor.LastName;
                    timeTb.Text = appointment.Start.ToString("hh:mm");
                    datePicker.SelectedDate = appointment.Start.Date;
                }

            }
        }

        private void AppointmentHistoryListBox_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var item = ItemsControl.ContainerFromElement(sender as ListBox, e.OriginalSource as DependencyObject) as ListBoxItem;
            if (item != null)
            {
                if(appointmentHistoryLb.SelectedIndex!=-1)
                {
                    Appointment appointment = indexedAppointmentsHistory[appointmentHistoryLb.SelectedIndex];
                    MessageBox.Show(appointment.Anamnesis.Description);
                }

            }
        }

        private int[] ValidateTime(string time)
        {
            try
            {
                int hour = Convert.ToInt32(time.Split(':')[0]);
                int minute = Convert.ToInt32(time.Split(':')[1]);
                int[] timeTuple = { hour, minute };
                return timeTuple;
            }
            catch
            {
               throw new Exception("Invalid Time.");
            }

        }
        private DateTime CombineDateTime(DateTime date,int[] time)
        {
            return new DateTime(date.Year, date.Month, date.Day, time[0], time[1], 0);
        }
    }
}
