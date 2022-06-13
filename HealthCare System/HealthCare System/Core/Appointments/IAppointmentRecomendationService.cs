using HealthCare_System.Core.Appointments.Model;
using HealthCare_System.Core.Users.Model;
using System;
using System.Collections.Generic;

namespace HealthCare_System.Core.Appointments
{
    public interface IAppointmentRecomendationService
    {
        List<Appointment> RecommendAppointment(DateTime end, int[] from, int[] to, Doctor doctor, bool priorityDoctor, Patient patient);
        List<Appointment> SearchNoPriority(DateTime end, int[] from, int[] to, Patient patient);
    }
}