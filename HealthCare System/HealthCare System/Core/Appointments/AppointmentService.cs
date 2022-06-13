using HealthCare_System.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using HealthCare_System.Core.Appointments.Repository;
using HealthCare_System.Core.Appointments.Model;
using HealthCare_System.Core.Users.Model;

namespace HealthCare_System.Core.Appointments
{
    public class AppointmentService : IAppointmentService
    {
        AppointmentRepo appointmentRepo;
        SchedulingService schedulingService;

        public AppointmentService(AppointmentRepo appointmentRepo, SchedulingService schedulingService)
        {
            this.appointmentRepo = appointmentRepo;
            this.SchedulingService = schedulingService;
        }

        internal AppointmentRepo AppointmentRepo { get => appointmentRepo; }
        internal SchedulingService SchedulingService { get => schedulingService; set => schedulingService = value; }

        public List<Appointment> Appointments()
        {
            return appointmentRepo.Appointments;
        }

        public List<Appointment> SortByDate(List<Appointment> unsortedAppointments, SortDirection direction)
        {
            if (direction == SortDirection.ASCENDING)
                return unsortedAppointments.OrderBy(x => x.Start).ToList();
            else
                return unsortedAppointments.OrderByDescending(x => x.Start).ToList();

        }

        public List<Appointment> SortByDoctor(List<Appointment> unsortedAppointments, SortDirection direction)
        {
            if (direction == SortDirection.ASCENDING)
                return unsortedAppointments.OrderBy(x => x.Doctor.FirstName).ThenBy(x => x.Doctor.LastName).ToList();
            else
                return unsortedAppointments.OrderByDescending(x => x.Doctor.FirstName).ThenByDescending(x => x.Doctor.LastName).ToList();

        }

        public List<Appointment> SortBySpecialization(List<Appointment> unsortedAppointments, SortDirection direction)
        {
            if (direction == SortDirection.ASCENDING)
                return unsortedAppointments.OrderBy(x => x.Doctor.Specialization).ToList();
            else
                return unsortedAppointments.OrderByDescending(x => x.Doctor.Specialization).ToList();

        }

        public List<Appointment> SortAnamneses(Patient patient, string word, AnamnesesSortCriterium criterium,
            SortDirection direction)
        {
            List<Appointment> unsortedAnamneses = appointmentRepo.FindByWord(patient, word);
            if (criterium == AnamnesesSortCriterium.DATE)
            {
                return SortByDate(unsortedAnamneses, direction);
            }
            else if (criterium == AnamnesesSortCriterium.DOCTOR)
            {
                return SortByDoctor(unsortedAnamneses, direction);
            }
            else
            {
                return SortBySpecialization(unsortedAnamneses, direction);
            }
        }

        private void DeleteAppointmens(Patient patient)
        {
            for (int i = AppointmentRepo.Appointments.Count - 1; i >= 0; i--)
            {
                if (AppointmentRepo.Appointments[i].Patient == patient)
                {
                    if (AppointmentRepo.Appointments[i].Start > DateTime.Now)
                    {
                        throw new Exception("Can't delete selected patient, because of it's future appointments.");
                    }
                    SchedulingService.DeleteAppointment(AppointmentRepo.Appointments[i].Id);
                }
            }
        }




    }
}
