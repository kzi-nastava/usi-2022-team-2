using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare_System.Repository.AppointmentRepo;
using HealthCare_System.Model;
using HealthCare_System.Services.UserService;


namespace HealthCare_System.Services.SchedulingService
{
    class AppointmentRecomendationService
    {
        AppointmentService.AppointmentService appointmentService;
        SchedulingService schedulingService;
        DoctorService doctorService;


        private List<Appointment> SearchDoubleCriterium(DateTime end, int[] from, int[] to, Doctor doctor, Patient patient)
        {
            appointmentService = new();
            schedulingService = new();
            List<Appointment> appointments = new();
            DateTime todayStart = DateTime.Now.Date.AddHours(from[0]).AddMinutes(from[1]);
            DateTime todayEnd = DateTime.Now.Date.AddHours(to[0]).AddMinutes(to[1]);
            DateTime date = todayStart;

            int id = appointmentService.AppointmentRepo.GenerateId();

            if (DateTime.Now > todayStart && DateTime.Now < todayEnd)
                date = DateTime.Now.Date.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute + 10);
            else if (DateTime.Now > todayEnd)
                date = date.AddDays(1);

            while (date.Date <= end.Date)
            {
                while (date < todayEnd)
                {
                    try
                    {
                        Room room = schedulingService.AvailableRoom(AppointmentType.EXAMINATION, date, date.AddMinutes(15));
                        Appointment appointment = new Appointment(id, date, date.AddMinutes(15), doctor,
                        patient, room, AppointmentType.EXAMINATION, AppointmentStatus.BOOKED, null, false, false);//? user

                        appointment.Validate();
                        appointments.Add(appointment);
                        return appointments;
                    }
                    catch
                    {
                    }
                    date = date.AddMinutes(1);
                }
                todayStart = todayStart.AddDays(1);
                todayEnd = todayEnd.AddDays(1);
                date = todayStart;
            }
            return null;
        }

        private List<Appointment> SearchPriorityDoctor(DateTime end, Doctor doctor, Patient patient)
        {
            appointmentService = new();
            List<Appointment> appointments = new();
            DateTime todayStart = DateTime.Now.Date;
            DateTime date = todayStart;

            int id = appointmentService.AppointmentRepo.GenerateId();
            if (DateTime.Now > todayStart)
                date = DateTime.Now.Date.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute + 10);

            while (date <= end)
            {
                try
                {
                    Room room = schedulingService.AvailableRoom(AppointmentType.EXAMINATION, date, date.AddMinutes(15));
                    Appointment appointment = new Appointment(id, date, date.AddMinutes(15), doctor,
                    patient, room, AppointmentType.EXAMINATION, AppointmentStatus.BOOKED, null, false, false);//? user

                    appointment.Validate();
                    appointments.Add(appointment);
                    return appointments;
                }
                catch
                {
                }
                date = date.AddMinutes(1);
            }
            return null;
        }

        private List<Appointment> SearchPriorityDate(DateTime end, int[] from, int[] to, Patient patient)
        {
            appointmentService = new();
            doctorService = new();
            schedulingService = new();
            List<Appointment> appointments = new();
            DateTime todayStart = DateTime.Now.Date.AddHours(from[0]).AddMinutes(from[1]);
            DateTime todayEnd = DateTime.Now.Date.AddHours(to[0]).AddMinutes(to[1]);
            DateTime date = todayStart;

            int id = appointmentService.AppointmentRepo.GenerateId();

            if (DateTime.Now > todayStart && DateTime.Now < todayEnd)
                date = DateTime.Now.Date.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute + 10);
            else if (DateTime.Now > todayEnd)
                date = date.AddDays(1);
            List<Doctor> doctors = doctorService.DoctorRepo.FindBySpecialization(Specialization.GENERAL);

            foreach (Doctor doctor in doctors)
            {
                while (date.Date <= end.Date)
                {
                    while (date < todayEnd)
                    {
                        try
                        {
                            Room room = schedulingService.AvailableRoom(AppointmentType.EXAMINATION, date, date.AddMinutes(15));
                            Appointment appointment = new Appointment(id, date, date.AddMinutes(15), doctor,
                            patient, room, AppointmentType.EXAMINATION, AppointmentStatus.BOOKED, null, false, false);//? user

                            appointment.Validate();
                            appointments.Add(appointment);
                            return appointments;
                        }
                        catch { }
                        date = date.AddMinutes(1);
                    }
                    todayStart = todayStart.AddDays(1);
                    todayEnd = todayEnd.AddDays(1);
                    date = todayStart;
                }
            }
            return null;

        }

        public List<Appointment> SearchNoPriority(DateTime end, int[] from, int[] to, Patient patient)
        {
            appointmentService = new();
            doctorService = new();
            schedulingService = new();
            DateTime todayStart = end.AddDays(1);
            DateTime date = todayStart;
            int id = appointmentService.AppointmentRepo.GenerateId();
            List<Doctor> doctors = doctorService.DoctorRepo.FindBySpecialization(Specialization.GENERAL);
            List<Appointment> appointments = new();
            while (true)
            {
                foreach (Doctor doctor in doctors)
                {
                    try
                    {
                        Room room = schedulingService.AvailableRoom(AppointmentType.EXAMINATION, date, date.AddMinutes(15));
                        Appointment appointment = new Appointment(id, date, date.AddMinutes(15), doctor,
                        patient, room, AppointmentType.EXAMINATION, AppointmentStatus.BOOKED, null, false, false);//? user
                        appointment.Validate();
                        appointments.Add(appointment);
                        date = date.AddMinutes(14);
                        if (appointments.Count == 3)
                            return appointments;
                    }
                    catch
                    {
                    }


                    date = date.AddMinutes(1);
                }
            }

        }

        public List<Appointment> RecommendAppointment(DateTime end, int[] from, int[] to, Doctor doctor, bool priorityDoctor, Patient patient)
        {
            List<Appointment> appointments = SearchDoubleCriterium(end, from, to, doctor, patient);
            if (appointments != null)
                return appointments;
            if (priorityDoctor)
            {
                appointments = SearchPriorityDoctor(end, doctor, patient);
                if (appointments != null)
                    return appointments;
                appointments = SearchPriorityDate(end, from, to, patient);
                if (appointments != null)
                    return appointments;
            }
            else
            {
                appointments = SearchPriorityDate(end, from, to, patient);
                if (appointments != null)
                    return appointments;
                appointments = SearchPriorityDoctor(end, doctor, patient);
                if (appointments != null)
                    return appointments;

            }
            return SearchNoPriority(end, from, to, patient);

        }
    }
}
