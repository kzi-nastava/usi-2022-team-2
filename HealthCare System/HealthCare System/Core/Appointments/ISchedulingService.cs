using HealthCare_System.Core.Appointments.Model;
using HealthCare_System.Core.Referrals.Model;
using HealthCare_System.Core.Rooms.Model;
using HealthCare_System.Core.Users.Model;
using System;

namespace HealthCare_System.Core.Appointments
{
    public interface ISchedulingService
    {
        Appointment AddAppointment(AppointmentDto appointmentDto);
        Room AvailableRoom(AppointmentType type, DateTime start, DateTime end);
        Appointment BookAppointmentByReferral(Referral referral);
        void DeleteAppointmens(Patient patient);
        void DeleteAppointment(int id);
        void UpdateAppointment(AppointmentDto newAppointmentDto);
    }
}