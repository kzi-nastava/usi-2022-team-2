using HealthCare_System.Core.Appointments.Model;
using HealthCare_System.Core.Users;
using HealthCare_System.Core.Users.Model;
using System;
using System.Collections.Generic;

namespace HealthCare_System.Core.Appointments
{
    public interface IAppointmentRecomendationService
    {
        IAppointmentService AppointmentService { get; set; }
        ISchedulingService SchedulingService { get; set; }
        IDoctorService DoctorService { get; set; }
        List<Appointment> RecommendAppointment(DateTime end, int[] from, int[] to, Doctor doctor, bool priorityDoctor, Patient patient);
        List<Appointment> SearchNoPriority(DateTime end, int[] from, int[] to, Patient patient);
    }
}