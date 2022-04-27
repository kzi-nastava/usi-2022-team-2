using HealthCare_System.entities;
using HealthCare_System.factory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HealthCare_System.gui
{
    /// <summary>
    /// Interaction logic for PatientWindow.xaml
    /// </summary>
    public partial class PatientWindow : Window
    {
        HealthCareFactory factory;
        Dictionary<int,Doctor> indexedDoctors;
        Dictionary<int,Doctor> indexedDoctorsEditTab;
        Dictionary<int,Appointment> indexedAppointments;
        bool isEditTabSelected;
        public PatientWindow(HealthCareFactory factory)
        {
            this.Title = factory.User.FirstName + " " + factory.User.LastName;
            this.factory = factory;
            InitializeComponent();
            InitializeDoctors();
            indexedAppointments = new Dictionary<int, Appointment>();
            indexedDoctorsEditTab = new Dictionary<int, Doctor>();
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
                index++;
            }
        }
        public void UpdateDoctors(Specialization specialization)
        {
            indexedDoctorsEditTab.Clear();
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
            
            if (doctorCb.SelectedIndex == -1)
            {
                MessageBox.Show("Please choose doctor.");
                return;
            }
            DateTime start;
            DateTime date = datePicker.SelectedDate.Value;
            if (date <= DateTime.Now)
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
            if (start <= DateTime.Now.AddDays(1))
            {
                MessageBox.Show("Appointment booked too soon.");
                return;
            }
            Doctor doctor = indexedDoctors[doctorCb.SelectedIndex];
            DateTime end = start.AddMinutes(15);
            Patient patient = (Patient) factory.User;
            try
            {
                Appointment appointment=factory.AddAppointment(start, end, doctor, patient, AppointmentType.EXAMINATION, AppointmentStatus.BOOKED, false);
                AppointmentRequest request = new AppointmentRequest(factory.AppointmentRequestController.GenerateId(), AppointmentState.ACCEPTED, patient, appointment, RequestType.CREATE, DateTime.Now);
                factory.AppointmentRequestController.Add(request);
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
            foreach (Appointment appointment in patient.MedicalRecord.Appointments)
            {
                if (DateTime.Now < appointment.Start)
                {
                    indexedAppointments.Add(index, appointment);
                    myAppointmentsLb.Items.Add(appointment.Start.ToString("dd/MM/yyyy HH:mm") +
                  ", doctor: " + appointment.Doctor.FirstName + " " + appointment.Doctor.LastName +
                ", type: " + appointment.Type.ToString() +
                ", room: " + appointment.Room.Name);
                    index++;
                }
            }
        }
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (editTab.IsSelected && !isEditTabSelected)
            {
                UpdateLb();
                isEditTabSelected = true;
            }
            else if (!editTab.IsSelected)
            {
                isEditTabSelected = false;
            }
        }

        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (myAppointmentsLb.SelectedIndex == -1)
            {
                MessageBox.Show("Please select appointment");
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
            if (start <= DateTime.Now.AddDays(2))
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
            try
            {
                AppointmentStatus status = AppointmentStatus.BOOKED;
                AppointmentState state = AppointmentState.ACCEPTED;
                if (needConfirmation)
                {
                    status = AppointmentStatus.ON_HOLD;
                    state = AppointmentState.WAITING;
                }
                factory.UpdateAppointment(appointment.Id,start, end, doctor, patient,status);
                AppointmentRequest request = new AppointmentRequest(factory.AppointmentRequestController.GenerateId(), state, patient, appointment, RequestType.UPDATE, DateTime.Now);
                factory.AppointmentRequestController.Add(request);
                UpdateLb();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void myAppointmentsLb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            Appointment appointment = indexedAppointments[myAppointmentsLb.SelectedIndex];
            datePickerEdit.SelectedDate=appointment.Start;
            timeEditTb.Text = appointment.Start.Hour.ToString() + ":" + appointment.Start.Minute.ToString();
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

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (myAppointmentsLb.SelectedIndex == -1)
            {
                MessageBox.Show("Please select appointment");
            }
            Appointment appointment = indexedAppointments[myAppointmentsLb.SelectedIndex];
            
            bool needConfirmation = false;
            if (appointment.Start <= DateTime.Now.AddDays(2))
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
                AppointmentRequest request = new AppointmentRequest(factory.AppointmentRequestController.GenerateId(), state, patient, appointment, RequestType.DELETE, DateTime.Now);
                factory.AppointmentRequestController.Add(request);
                UpdateLb();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        void DataWindow_Closing(object sender, CancelEventArgs e)
        {
            factory.User = null;
            MainWindow main = new MainWindow(factory);
            main.Show();
        }
        }
}
