using HealthCare_System.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare_System.Repository.AppointmentRepo;

namespace HealthCare_System.Services.AppointmentService
{
    class AppointmentService
    {
        AppointmentRepo appointmentRepo;

        public AppointmentService()
        {
            AppointmentRepoFactory appointmentRepoFactory = new();
            appointmentRepo = appointmentRepoFactory.CreateAppointmentRepository();
        }

        internal AppointmentRepo AppointmentRepo { get => appointmentRepo; }

        
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
        public List<Appointment> SortAnamneses(Patient patient, string word, AnamnesesSortCriterium criterium, SortDirection direction)
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


    }
}
