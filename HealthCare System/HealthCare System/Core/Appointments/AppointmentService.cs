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
        IAppointmentRepo appointmentRepo;
        ISchedulingService schedulingService;

        public AppointmentService(IAppointmentRepo appointmentRepo, ISchedulingService schedulingService)
        {
            this.appointmentRepo = appointmentRepo;
            this.schedulingService = schedulingService;
        }

        public IAppointmentRepo AppointmentRepo { get => appointmentRepo; }

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
            List<Appointment> unsortedAnamneses = FindByWord(patient, word);
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

        public void DeleteAppointmens(Patient patient)
        {
            for (int i = Appointments().Count - 1; i >= 0; i--)
            {
                if (Appointments()[i].Patient == patient)
                {
                    if (Appointments()[i].Start > DateTime.Now)
                    {
                        throw new Exception("Can't delete selected patient, because of it's future appointments.");
                    }
                    schedulingService.DeleteAppointment(Appointments()[i].Id);
                }
            }
        }
        public Appointment FindById(int id)
        {
            return appointmentRepo.FindById(id);
        }

        public int GenerateId()
        {
            return appointmentRepo.GenerateId();
        }

        public void Serialize(string linkPath = "../../../data/links/AppointmentLinker.csv")
        {
            appointmentRepo.Serialize(linkPath);
        }
        public List<Appointment> FindByWord(Patient patient, string word)
        {
            return appointmentRepo.FindByWord(patient, word);
        }

        public List<Appointment> FindPastAppointments(Patient patient)
        {
            return appointmentRepo.FindPastAppointments(patient);
        }

        public List<Appointment> FindUpcomingAppointments(Patient patient)
        {
            return appointmentRepo.FindUpcomingAppointments(patient);
        }
        public void Add(Appointment appointment)
        {
            appointmentRepo.Add(appointment);
        }
    }
}
