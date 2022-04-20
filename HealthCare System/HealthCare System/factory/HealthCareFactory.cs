using HealthCare_System.controllers;
using HealthCare_System.entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_System.factory
{
    class HealthCareFactory
    {
        DoctorController doctorController;
        PatientController patientController;
        ManagerController managerController;
        AppointmentController appointmentController;

        public HealthCareFactory()
        {
            this.doctorController = new();
            this.patientController = new();
            this.appointmentController = new();
            this.managerController = new();

            this.LinkDoctorsAppointments();
            this.LinkPatientsAppointments();
        }

        public DoctorController DoctorController 
        { 
            get { return this.doctorController; }
        }

        public PatientController PatientController
        {
            get { return this.patientController; }
        }

        public AppointmentController AppointmentController
        {
            get { return this.appointmentController; }
        }

        public ManagerController ManagerController
        {
            get { return this.managerController; }
        }

        //TODO add the rest of user types
        public Person Login(string mail, string password)
        {
            foreach (Doctor doctor in this.doctorController.Doctors)
                if (doctor.Mail == mail && doctor.Password == password)
                    return doctor;

            foreach (Patient patient in this.patientController.Patients)
                if (patient.Mail == mail && patient.Password == password)
                    return patient;

            foreach (Manager manager in this.managerController.Managers)
                if (manager.Mail == mail && manager.Password == password)
                    return manager;

            return null;
        }

        void LinkDoctorsAppointments()
        {
            StreamReader doctorAppointment = new StreamReader("data/links/DoctorAppointment.csv");

            while (!doctorAppointment.EndOfStream)
            {
                string line = doctorAppointment.ReadLine();
                string mail = line.Split(";")[0];
                int id = Convert.ToInt32(line.Split(";")[1].Trim());

                Appointment appointment = this.appointmentController.FindById(id);
                Doctor doctor = this.doctorController.FindByMail(mail);

                doctor.Appointments.Add(appointment);
                appointment.Doctor = doctor;
            }

            doctorAppointment.Close();

        }

        void LinkPatientsAppointments()
        {
            StreamReader patientAppointment = new StreamReader("data/links/PatientAppointment.csv");

            while (!patientAppointment.EndOfStream)
            {
                string line = patientAppointment.ReadLine();
                string mail = line.Split(";")[0];
                int id = Convert.ToInt32(line.Split(";")[1].Trim());

                Appointment appointment = this.appointmentController.FindById(id);
                Patient patient = this.patientController.FindByMail(mail);

                patient.Appointments.Add(appointment);
                appointment.Patient = patient;
            }

            patientAppointment.Close();

        }
    }
}
