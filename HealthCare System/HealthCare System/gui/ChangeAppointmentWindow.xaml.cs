using HealthCare_System.Model;
using HealthCare_System.factory;
using System;
using System.Windows;
using HealthCare_System.Database;

namespace HealthCare_System.gui
{
    public partial class ChangeAppointmentWindow : Window
    {
        Appointment appointment;
        HealthCareFactory factory;
        HealthCareDatabase database;

        public ChangeAppointmentWindow(Appointment appointment, HealthCareFactory factory, HealthCareDatabase database)
        {
            this.appointment = appointment;
            this.factory = factory;
            this.database  = database;

            InitializeComponent();

            patientJmbgTb.Text = appointment.Patient.Jmbg.ToString();
            appointmentDate.SelectedDate = appointment.Start;
            timeTb.Text = appointment.Start.ToString("HH:mm");
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Patient patient = factory.PatientController.FindByJmbg(patientJmbgTb.Text);     

                int id = appointment.Id;
                int duration = (appointment.End - appointment.Start).Minutes;

                DateTime start = DoctorWindow.ValidateDate(appointmentDate, timeTb);
                if (start == default(DateTime))
                    return;

                Appointment newAppointment = new(id, start, start.AddMinutes(duration), appointment.Doctor, patient,
                    appointment.Room, appointment.Type, appointment.Status, appointment.Anamnesis, false,
                        appointment.Emergency);
                factory.UpdateAppointment(newAppointment);

                MessageBox.Show("Appointment changed!");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}
