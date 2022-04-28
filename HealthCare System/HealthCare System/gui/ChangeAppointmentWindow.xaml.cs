﻿using HealthCare_System.entities;
using HealthCare_System.factory;
using System;
using System.Windows;

namespace HealthCare_System.gui
{
    public partial class ChangeAppointmentWindow : Window
    {
        Appointment appointment;
        HealthCareFactory factory;

        public ChangeAppointmentWindow(Appointment appointment, HealthCareFactory factory)
        {
            this.appointment = appointment;
            this.factory = factory;

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
                if (patient is null)
                {
                    MessageBox.Show("Patient doesn't exist!");
                    return;
                }

                int id = appointment.Id;
                int duration = (appointment.End - appointment.Start).Minutes;
                DateTime date = appointmentDate.SelectedDate.Value;
                int hour = Convert.ToInt32(timeTb.Text.Split(":")[0]);
                int min = Convert.ToInt32(timeTb.Text.Split(":")[1]);
                DateTime start = new(date.Year, date.Month, date.Day, hour, min, 0);
                factory.UpdateAppointment(id, start, start.AddMinutes(duration), appointment.Doctor, patient,
                    AppointmentStatus.BOOKED);
                MessageBox.Show("Appointment changed!");
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
    }
}