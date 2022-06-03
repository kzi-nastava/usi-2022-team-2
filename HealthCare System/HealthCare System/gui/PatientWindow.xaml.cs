﻿using HealthCare_System.controllers;
using HealthCare_System.Model;
using HealthCare_System.Model.Dto;
using HealthCare_System.factory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Input;
using HealthCare_System.Database;
using HealthCare_System.Services.UserServices;
using HealthCare_System.Services.AppointmentServices;
using HealthCare_System.Services.SurveyServices;

namespace HealthCare_System.gui
{
    public partial class PatientWindow : Window
    {
        
        HealthCareFactory factory;
        HealthCareDatabase database;
        Dictionary<int,Doctor> indexedDoctors;
        Dictionary<int,Doctor> indexedSearchedDoctors;
        Dictionary<int,Doctor> indexedDoctorsEditTab;
        Dictionary<int,Appointment> indexedAppointments;
        Dictionary<int,Appointment> indexedAnamneses;
        Dictionary<int,Appointment> indexedRecommendations;
        Dictionary<int,Appointment> indexedAppointmentsHistory;
        DispatcherTimer timer;
        List<DrugNotification> notifications;
        Patient user;
        DoctorService doctorService;
        UserService userService;
        AppointmentService appointmentService;
        SchedulingService schedulingService;
        AppointmentRequestService appointmentRequestService;
        PatientService patientService;
        AppointmentRecomendationService appointmentRecomendationService;
        DoctorSurveyService doctorSurveyService;
        HospitalSurveyService hospitalSurveyService;

        public PatientWindow(HealthCareFactory factory, HealthCareDatabase database)
        {
            Title = factory.User.FirstName + " " + factory.User.LastName;
            indexedAppointments = new Dictionary<int, Appointment>();
            indexedAppointmentsHistory = new Dictionary<int, Appointment>();
            indexedAnamneses= new Dictionary<int, Appointment>();
            indexedRecommendations = new Dictionary<int, Appointment>();
            indexedDoctorsEditTab = new Dictionary<int, Doctor>();
            indexedDoctors = new Dictionary<int, Doctor>();
            notifications = new();
            indexedSearchedDoctors = new Dictionary<int, Doctor>();
            this.factory = factory;
            this.database =  database;
            user =(Patient) factory.User;

            InitializeComponent();
            InitializeServices();
            InitializeSearchEngineDoctors();
            InitializeDoctors();
            InitializeAnamnesesTab();
            UpdateUpcomingAppointments();
            UpdateAppointmentHistory();
            StartTimer();
            InitializeSurveys();

            recommendedDoctorCb.SelectedItem = indexedDoctors[0];
            datePicker.DisplayDateStart = DateTime.Now;
            datePickerEdit.DisplayDateStart = DateTime.Now;
            reccomendedEndDateDp.DisplayDateStart = DateTime.Now;
            minutesBeforeDrugSl.Value = user.MinutesBeforeDrug;

            DelayedAppointmentNotificationWindow notificationWindow = new(factory, database);
            
            
        }
        void InitializeServices()
        {
            doctorSurveyService = new(database.DoctorSurveyRepo);
            hospitalSurveyService = new(database.HospitalSurveyRepo);
            doctorService = new DoctorService(database.DoctorRepo, doctorSurveyService);
            appointmentService = new(database.AppointmentRepo, null);
            schedulingService = new(null, appointmentService, null, doctorService, null);
            appointmentRequestService = new(database.AppointmentRequestRepo, appointmentService);
            patientService = new(database.PatientRepo, schedulingService, null, null, null);
            userService = new(patientService, doctorService, null, null, appointmentRequestService);
            appointmentRecomendationService = new(appointmentService, schedulingService, doctorService);
            
        }
        void StartTimer()
        {
            CreateNotifications();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMinutes(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }
        void timer_Tick(object sender, EventArgs e)
        {
            CheckNotifications();
        }
        void CheckNotifications()
        {
            foreach (DrugNotification notification in notifications)
            {
                if (DateTime.Now.AddMinutes(user.MinutesBeforeDrug) > notification.Time.AddMinutes(-1) && DateTime.Now.AddMinutes(user.MinutesBeforeDrug) < notification.Time.AddMinutes(1) && !notification.Seen)
                {
                    notification.Seen = true;
                    MessageBox.Show(notification.Message);
                }
            }
        }
        void CreateNotifications()
        {
            foreach (Prescription prescription in user.MedicalRecord.Prescriptions)
            {
                if ((prescription.Start <= DateTime.Now && prescription.End >= DateTime.Now) || prescription.Start >= DateTime.Now)
                {
                    DateTime time = prescription.Start;
                    while (time < prescription.End)
                    {
                        string message = "It's time to drink " + prescription.Drug.Name;
                        notifications.Add(new DrugNotification(-1, message, user, prescription.Drug, time));
                        time = time.AddHours(24 / prescription.Frequency);
                    }
                }
            }
        }

        public void UpdateAppointmentHistory()
        {

            indexedAppointmentsHistory.Clear();
            appointmentHistoryLb.Items.Clear();

            List<Appointment> sortedAppoinments = user.MedicalRecord.Appointments.OrderBy(x => x.Start).ToList();
            int iterationNum = 0;

            foreach (Appointment appointment in user.MedicalRecord.Appointments)
            {
                indexedAppointmentsHistory.Add(iterationNum, appointment);

                
                if (DateTime.Now > appointment.Start && appointment.Status != AppointmentStatus.ON_HOLD)
                {
                    string appointmentSummary = appointment.Start.ToString("dd/MM/yyyy HH:mm") + ", "
                        + appointment.Doctor.FirstName + " " + appointment.Doctor.LastName;
                    appointmentHistoryLb.Items.Add(appointmentSummary);
                }
                iterationNum++;
            }
        }

        public void InitializeSurveys()
        {
            for (int i = 0; i < 5; i++)
            {
                hospitalServiceQualityCb.Items.Add(i + 1);
                hospitalSatisfactionCb.Items.Add(i + 1);
                hospitalRecommendCb.Items.Add(i + 1);
                hospitalHygieneCb.Items.Add(i + 1);
                doctorRecommendCb.Items.Add(i + 1);
                doctorServiceQualityCb.Items.Add(i + 1);
            }
        }
        public void InitializeAnamnesesTab()
        {
            sortingDirectionCb.ItemsSource = Enum.GetValues(typeof(SortDirection));
            sortingDirectionCb.SelectedIndex = 0;
            sortCriteriumCb.ItemsSource = Enum.GetValues(typeof(AnamnesesSortCriterium));
            sortCriteriumCb.SelectedIndex = 0;
        }
        public void InitializeSearchEngineDoctors()
        {
            sortDirectionDoctorCb.ItemsSource = Enum.GetValues(typeof(SortDirection));
            sortDirectionDoctorCb.SelectedIndex = 0;
            doctorShowPriorityCb.ItemsSource = Enum.GetValues(typeof(DoctorSortPriority));
            doctorShowPriorityCb.SelectedIndex = 0;
            doctorSpecializationCb.ItemsSource = Enum.GetValues(typeof(Specialization));
            doctorSpecializationCb.SelectedIndex = 3;


        }
        public void InitializeDoctors()
        {
            List<Doctor> doctors = doctorService.DoctorRepo.FindBySpecialization(Specialization.GENERAL);

            int index = 0;
            foreach (Doctor doctor in doctors)
            {
                string credentials = doctor.FirstName + " " + doctor.LastName;
                indexedDoctors.Add(index,doctor);
                doctorCb.Items.Add(credentials);
                recommendedDoctorCb.Items.Add(credentials);

                index++;
            }
        }
        public void UpdateAnamneses( List<Appointment> appointments)
        {
            indexedAnamneses.Clear();
            sortedAnamnesesLb.Items.Clear();

            int iterationNum = 0;

            foreach (Appointment appointment in appointments)
            {
                indexedAnamneses.Add(iterationNum, appointment);

                sortedAnamnesesLb.Items.Add(appointment.Anamnesis.Description);
                
                iterationNum++;
            }

        }
        public void UpdateDoctors(Specialization specialization)
        {
            indexedDoctorsEditTab.Clear();
            doctorEditCb.Items.Clear();

            List<Doctor> doctors = doctorService.DoctorRepo.FindBySpecialization(specialization);

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
            if (user.Blocked)
            {
                Close();
                return;
            }

            if (doctorCb.SelectedIndex == -1)
            {
                MessageBox.Show("Please choose doctor.");
                return;
            }
            Doctor doctor = indexedDoctors[doctorCb.SelectedIndex];

            DateTime date = datePicker.SelectedDate.Value;
            int[] time = ValidateTime(timeTb.Text);
            if (time == null)
            {
                return;
            }

            DateTime start= new DateTime(date.Year, date.Month, date.Day, time[0], time[1], 0);
            DateTime end = start.AddMinutes(15);

            try
            {
                int id = appointmentService.AppointmentRepo.GenerateId();
                AppointmentDto newAppointment = new(id, start, end, doctor, user, null,
                    AppointmentType.EXAMINATION, AppointmentStatus.BOOKED, null, false, false);
                Appointment appointment = schedulingService.AddAppointment(newAppointment);
                AppointmentRequestDto request = new AppointmentRequestDto(appointmentRequestService.AppointmentRequestRepo.GenerateId(),
                    AppointmentState.ACCEPTED, user, appointment, null, RequestType.CREATE, DateTime.Now);
               appointmentRequestService.Add(request);


                MessageBox.Show("Appointment booked");


                datePicker.SelectedDate = DateTime.Now;
                timeTb.Clear();
                doctorCb.SelectedIndex = -1;
                UpdateUpcomingAppointments();
            }
            catch(Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
        private void UpdateUpcomingAppointments()
        {
            indexedAppointments.Clear();
            myAppointmentsLb.Items.Clear();

            List<Appointment> sortedAppoinments = user.MedicalRecord.Appointments.OrderBy(x => x.Start).ToList();


            int index = 0;
            foreach (Appointment appointment in sortedAppoinments)
            {
                if (DateTime.Now < appointment.Start && appointment.Status!=AppointmentStatus.ON_HOLD)
                {
                    string roomSummary = "";
                    if (appointment.Room != null)
                        roomSummary = ", room: " + appointment.Room.Name;
                    indexedAppointments.Add(index, appointment);

                    string appointmentSummary = appointment.Start.ToString("dd/MM/yyyy HH:mm") +
                  ", doctor: " + appointment.Doctor.FirstName + " " + appointment.Doctor.LastName +
                ", type: " + appointment.Type.ToString().ToLower() + roomSummary;
                    myAppointmentsLb.Items.Add(appointmentSummary);
                    index++;
                }
            }
        }

        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            CheckAntiTroll();
            if (user.Blocked)
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
            Doctor doctor = indexedDoctors[doctorEditCb.SelectedIndex];
            DateTime date = datePickerEdit.SelectedDate.Value;
            int[] time = ValidateTime(timeEditTb.Text);
            if (time == null)
            {
                return;
            }

            DateTime start = CombineDateTime(date, time);
            DateTime end = start.AddMinutes((appointment.End - appointment.Start).TotalMinutes);

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
            
            Appointment requestedAppointment = null;
            try
            {
                AppointmentState state = AppointmentState.ACCEPTED;
                AppointmentDto newAppointment = new(appointment.Id, start, end, appointment.Doctor,
                        user, appointment.Room, appointment.Type, appointment.Status, appointment.Anamnesis, false,
                        appointment.Emergency);
                if (needConfirmation)
                {

                    requestedAppointment=schedulingService.AddAppointment(newAppointment);
                    state = AppointmentState.WAITING;
                }
                else
                {
                    
                    schedulingService.UpdateAppointment(newAppointment);
                }
                
                AppointmentRequestDto request = new AppointmentRequestDto(appointmentRequestService.AppointmentRequestRepo.GenerateId(),
                    state, user, appointment, requestedAppointment, RequestType.UPDATE, DateTime.Now);
                appointmentRequestService.Add(request);
                UpdateUpcomingAppointments();


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
                timeEditTb.Text = appointment.Start.ToString("HH:mm");

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
            userService.RunAntiTrollCheck(user);
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            CheckAntiTroll();
            if (user.Blocked)
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

                AppointmentRequestDto request = new AppointmentRequestDto(appointmentRequestService.AppointmentRequestRepo.GenerateId(),
                    state, user, appointment, null, RequestType.DELETE, DateTime.Now);
                factory.AppointmentRequestController.Add(request);
                UpdateUpcomingAppointments();
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
            if (!((Patient) user).Blocked)
            {
                factory.User = null;
                if (MessageBox.Show("Log out?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    MainWindow main = new MainWindow(factory, database);
                    main.Show();
                }
                else e.Cancel = true;
            }
            else
            {
                factory.User = null;
                MessageBox.Show("Account blocked. Contact secretary for more informations!");
                MainWindow main = new MainWindow(factory, database);
                main.Show();
            }
            
        }

        private void refreshHistory_Click(object sender, RoutedEventArgs e)
        {
            UpdateAppointmentHistory();
        }
        private void anamnesesSearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {

            sortedAnamnesesLb.Items.Clear();
            if (anamnesesSearchTb.Text.Length >= 3 && sortCriteriumCb.SelectedIndex != -1 && sortingDirectionCb.SelectedIndex != -1)
            {
                List<Appointment> sortedAppointments = appointmentService.SortAnamneses(user, anamnesesSearchTb.Text,
                   (AnamnesesSortCriterium)Enum.Parse(typeof(AnamnesesSortCriterium), sortCriteriumCb.SelectedItem.ToString()),
                   (SortDirection)Enum.Parse(typeof(SortDirection), sortingDirectionCb.SelectedItem.ToString()));


                UpdateAnamneses(sortedAppointments);
            }
        }

        private void sortCriteriumCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sortedAnamnesesLb.Items.Clear();
            if (anamnesesSearchTb.Text.Length >= 3 && sortCriteriumCb.SelectedIndex != -1 && sortingDirectionCb.SelectedIndex != -1)
            {
                List<Appointment> sortedAppointments = appointmentService.SortAnamneses(user, anamnesesSearchTb.Text,
                   (AnamnesesSortCriterium)Enum.Parse(typeof(AnamnesesSortCriterium), sortCriteriumCb.SelectedItem.ToString()),
                   (SortDirection)Enum.Parse(typeof(SortDirection), sortingDirectionCb.SelectedItem.ToString()));


                UpdateAnamneses(sortedAppointments);
            }
        }

        private void sortingDirectionCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sortedAnamnesesLb.Items.Clear();
            if (anamnesesSearchTb.Text.Length >= 3 && sortCriteriumCb.SelectedIndex != -1 && sortingDirectionCb.SelectedIndex != -1)
            {
                List<Appointment> sortedAppointments = appointmentService.SortAnamneses(user, anamnesesSearchTb.Text,
                   (AnamnesesSortCriterium)Enum.Parse(typeof(AnamnesesSortCriterium), sortCriteriumCb.SelectedItem.ToString()),
                   (SortDirection)Enum.Parse(typeof(SortDirection), sortingDirectionCb.SelectedItem.ToString()));


                UpdateAnamneses(sortedAppointments);
            }
        }
        
        private void SearchRecommendationBtn_Click(object sender, RoutedEventArgs e)
        {
            DateTime recommendedEndDate = reccomendedEndDateDp.SelectedDate.Value;
            indexedRecommendations.Clear();
            recommendedAppointmentsLb.Items.Clear();
            int[] timeTupleFrom = ValidateTime(recommendedFromTb.Text);
            int[] timeTupleTo = ValidateTime(recommendedToTb.Text);

            Doctor doctor = indexedDoctors[recommendedDoctorCb.SelectedIndex];
            List<Appointment> appointments=appointmentRecomendationService.RecommendAppointment(recommendedEndDate, timeTupleFrom,
                timeTupleTo, doctor,(bool) priorityDoctorRb.IsChecked,user);
            if (appointments.Count==1)
            {
                doctorCb.SelectedItem = appointments[0].Doctor.FirstName + " " + appointments[0].Doctor.LastName;
                timeTb.Text = appointments[0].Start.ToString("HH:mm");
                datePicker.SelectedDate = appointments[0].Start.Date;
            }
            else
            {
                for (int i = 0; i < appointments.Count; i++)
                {
                    indexedRecommendations.Add(i, appointments[i]);
                    string appointmentSummary = appointments[i].Doctor.FirstName + appointments[i].Doctor.LastName + "\t" + appointments[i].Start.ToString("dd/MM/yyyy HH:mm");
                    recommendedAppointmentsLb.Items.Add(appointmentSummary);
                }

            }
        }
        private void PlaceholdersListBox_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var item = ItemsControl.ContainerFromElement(sender as ListBox, e.OriginalSource as DependencyObject) as ListBoxItem;
            if (item != null)
            {
                if (recommendedAppointmentsLb.SelectedIndex != -1)
                {
                    Appointment appointment = indexedRecommendations[recommendedAppointmentsLb.SelectedIndex];
                    doctorCb.SelectedItem = appointment.Doctor.FirstName + " " + appointment.Doctor.LastName;
                    timeTb.Text = appointment.Start.ToString("HH:mm");
                    datePicker.SelectedDate = appointment.Start.Date;
                }

            }
        }


        private int[] ValidateTime(string time)
        {
            try
            {
                int hour = Convert.ToInt32(time.Split(':')[0]);
                int minute = Convert.ToInt32(time.Split(':')[1].Trim());
                int[] timeTuple = { hour, minute };
                return timeTuple;
            }
            catch
            {
                MessageBox.Show("Invalid time format.");
                return null;
            }

        }
        private DateTime CombineDateTime(DateTime date,int[] time)
        {
            return new DateTime(date.Year, date.Month, date.Day, time[0], time[1], 0);
        }

        private void searchAnamneses_Click(object sender, RoutedEventArgs e)
        {
            if (sortedAnamnesesLb.SelectedIndex != -1)
            {
                Appointment appointment = indexedAnamneses[sortedAnamnesesLb.SelectedIndex];
                AppointmentInfo infoWindow = new AppointmentInfo(appointment);
                infoWindow.Show();
            }

        }



        void updateSearchListDoctors()
        {
           
            if (doctorShowPriorityCb.SelectedIndex!=-1 && sortDirectionDoctorCb.SelectedIndex != -1 && doctorSpecializationCb.SelectedIndex!=-1)
            {
                doctorsLb.Items.Clear();
                indexedSearchedDoctors.Clear();
                DoctorSortPriority priority = (DoctorSortPriority)Enum.Parse(typeof(DoctorSortPriority), doctorShowPriorityCb.SelectedItem.ToString());
                SortDirection direction = (SortDirection)Enum.Parse(typeof(SortDirection), sortDirectionDoctorCb.SelectedItem.ToString());
                List<Doctor> doctors = doctorService.FilterDoctors(doctorFirstNameTb.Text,
                    doctorLastNameTb.Text, (Specialization)Enum.Parse(typeof(Specialization), doctorSpecializationCb.SelectedItem.ToString()));
                List<Doctor> sortedDoctors = doctorService.SortDoctors(doctors, priority, direction);
                int index = 0;
                foreach (Doctor doctor in sortedDoctors)
                {
                    indexedSearchedDoctors.Add(index, doctor);
                    doctorsLb.Items.Add(doctor.FirstName + " " + doctor.LastName + ", " + doctor.Specialization.ToString().ToLower());
                    index++;
                }
            }
           

        }
        private void doctorFirstNameTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            updateSearchListDoctors();
        }

        private void doctorLastNameTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            updateSearchListDoctors();
        }

        private void doctorSpecializationCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateSearchListDoctors();

        }

        private void doctorShowPriorityCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateSearchListDoctors();

        }

        private void sortDirectionDoctorCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateSearchListDoctors();

        }

        private void saveUserDrugReminderBtn_Click(object sender, RoutedEventArgs e)
        {
            user.MinutesBeforeDrug = (int)minutesBeforeDrugSl.Value;
            factory.PatientController.Serialize();
            notifications.Clear();
            CreateNotifications();
        }
            
        private void bookDoctorCb_Click(object sender, RoutedEventArgs e)
        {
            if (doctorsLb.SelectedIndex==-1)
            {
                MessageBox.Show("Please select doctor");
                return;
            }
            if (indexedSearchedDoctors[doctorsLb.SelectedIndex].Specialization!=Specialization.GENERAL)
            {
                MessageBox.Show("You can only book appointment with doctor whose specialization is general!");
                return;
            }
            for (int i=0; i < indexedDoctors.Count; i++)
            {
                if (indexedDoctors[i]== indexedSearchedDoctors[doctorsLb.SelectedIndex])
                {
                    doctorCb.SelectedIndex = i;
                    tabs.SelectedIndex = 0;
                    return;
                }
            }

        }

        private void rateDoctorBtn_Click(object sender, RoutedEventArgs e)
        {
            if (appointmentHistoryLb.SelectedIndex == -1)
            {
                MessageBox.Show("Select Appointment");
                return;
            }
            if(doctorRecommendCb.SelectedIndex==-1 || doctorServiceQualityCb.SelectedIndex == -1)
            {
                MessageBox.Show("Please Fill All Fields");
                return;
            }
            if (indexedAppointmentsHistory[appointmentHistoryLb.SelectedIndex].Graded)
            {
                MessageBox.Show("Appointment Is Already Graded");
                return;
            }    
            int id = factory.DoctorSurveyController.GenerateId();
            Doctor doctor = indexedAppointmentsHistory[appointmentHistoryLb.SelectedIndex].Doctor;
            DoctorSurveyDto survey = new DoctorSurveyDto(id,doctor, doctorServiceQualityCb.SelectedIndex + 1, doctorRecommendCb.SelectedIndex + 1, doctorCommentTb.Text);
            indexedAppointmentsHistory[appointmentHistoryLb.SelectedIndex].Graded = true;
            doctorSurveyService.Add(survey);
            appointmentService.AppointmentRepo.Serialize();
            doctorServiceQualityCb.SelectedIndex = -1;
            doctorRecommendCb.SelectedIndex = -1;
            doctorCommentTb.Clear();
        }

        private void rateHospitalBtn_Click(object sender, RoutedEventArgs e)
        {
            if (hospitalHygieneCb.SelectedIndex == -1 || hospitalRecommendCb.SelectedIndex == -1||
                hospitalSatisfactionCb.SelectedIndex==-1||hospitalServiceQualityCb.SelectedIndex==-1)
            {
                MessageBox.Show("Please Fill All Fields");
                return;
            }
            int id = factory.HospitalSurveyController.GenerateId();

            HospitalSurveyDto survey = new HospitalSurveyDto(id, hospitalServiceQualityCb.SelectedIndex+1,hospitalHygieneCb.SelectedIndex+1,
                hospitalSatisfactionCb.SelectedIndex+1,hospitalRecommendCb.SelectedIndex+1,hospitalCommentTb.Text);
            hospitalSurveyService.Add(survey);
            hospitalServiceQualityCb.SelectedIndex = -1;
            hospitalHygieneCb.SelectedIndex = -1;
            hospitalSatisfactionCb.SelectedIndex = -1;
            hospitalRecommendCb.SelectedIndex = -1;
            hospitalCommentTb.Clear();
        }
    }
}
