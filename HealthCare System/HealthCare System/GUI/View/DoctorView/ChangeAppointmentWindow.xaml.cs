using HealthCare_System.Model;
using System;
using System.Windows;
using HealthCare_System.Database;
using HealthCare_System.Services.AppointmentServices;
using HealthCare_System.Model.Dto;

namespace HealthCare_System.gui
{
    public partial class ChangeAppointmentWindow : Window
    {
        Appointment appointment;
        HealthCareDatabase database;
        SchedulingService schedulingService;

        public ChangeAppointmentWindow(Appointment appointment, HealthCareDatabase database)
        {
            this.appointment = appointment;
            this.database  = database;

            InitializeComponent();

            schedulingService = new(null, new(database.AppointmentRepo, null), null, null, null);

            patientJmbgTb.Text = appointment.Patient.Jmbg.ToString();
            appointmentDate.SelectedDate = appointment.Start;
            timeTb.Text = appointment.Start.ToString("HH:mm");
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Patient patient = database.PatientRepo.FindByJmbg(patientJmbgTb.Text);     

                int id = appointment.Id;
                int duration = (appointment.End - appointment.Start).Minutes;

                DateTime start = DoctorWindow.ValidateDate(appointmentDate, timeTb);
                if (start == default(DateTime))
                    return;

                AppointmentDto newAppointmentDto = new(id, start, start.AddMinutes(duration), appointment.Doctor, patient,
                    appointment.Room, appointment.Type, appointment.Status, appointment.Anamnesis, false,
                        appointment.Emergency);
                schedulingService.UpdateAppointment(newAppointmentDto);

                MessageBox.Show("Appointment changed!");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}
