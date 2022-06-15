using HealthCare_System.Core.Appointments.Model;
using HealthCare_System.Core.Users.Model;
using HealthCare_System.Model.Core.Appointments.Model;
using System;
using System.Collections.Generic;

namespace HealthCare_System.Core.Appointments
{
    public interface IUrgentSchedulingService
    {

        IAppointmentService AppointmentService { get; set; }
        ISchedulingService SchedulingService { get; set; }
        void BookClosestEmergancyAppointment(UrgentAppointmentDto urgentAppointmentDto);
        Dictionary<Appointment, DateTime> GetReplaceableAppointments(List<Doctor> doctors, int duration, Patient patient);
        Appointment ReplaceAppointment(Appointment toReplaceAppointment, UrgentAppointmentDto urgentAppointmentDto);
    }
}