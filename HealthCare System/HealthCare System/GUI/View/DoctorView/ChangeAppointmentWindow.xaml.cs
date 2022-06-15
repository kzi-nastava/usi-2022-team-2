using System;
using System.Windows;
using HealthCare_System.Core.Appointments.Model;
using HealthCare_System.Core.Users.Model;
using HealthCare_System.GUI.Controller.Appointments;
using HealthCare_System.Core.Appointments;
using HealthCare_System.GUI.Controller.Users;
using HealthCare_System.Core.Users;

namespace HealthCare_System.GUI.DoctorView
{
    public partial class ChangeAppointmentWindow : Window
    {
        Appointment appointment;
        SchedulingController schedulingController;
        PatientController patientController;

        public ChangeAppointmentWindow(Appointment appointment, ISchedulingService schedulingService, IPatientService patientService)
        {
            this.appointment = appointment;

            InitializeComponent();

            schedulingController = new(schedulingService);
            patientController = new(patientService);

            patientJmbgTb.Text = appointment.Patient.Jmbg.ToString();
            appointmentDate.SelectedDate = appointment.Start;
            timeTb.Text = appointment.Start.ToString("HH:mm");
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Patient patient = patientController.FindByJmbg(patientJmbgTb.Text);     

                int id = appointment.Id;
                int duration = (appointment.End - appointment.Start).Minutes;

                DateTime start = DoctorWindow.ValidateDate(appointmentDate, timeTb);
                if (start == default(DateTime))
                    return;

                AppointmentDto newAppointmentDto = new(id, start, start.AddMinutes(duration), appointment.Doctor, patient,
                    appointment.Room, appointment.Type, appointment.Status, appointment.Anamnesis, false,
                        appointment.Emergency);
                schedulingController.UpdateAppointment(newAppointmentDto);

                MessageBox.Show("Appointment changed!");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}
